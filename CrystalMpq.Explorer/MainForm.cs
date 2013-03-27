#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using CrystalMpq;
using System.IO;
using CrystalMpq.DataFormats;
using CrystalMpq.Explorer.Extensibility;
using CrystalMpq.Explorer.Properties;
using CrystalMpq.Explorer.Viewers;
using CrystalMpq.WoW;
using System.Threading;
#endregion

namespace CrystalMpq.Explorer
{
	internal sealed partial class MainForm : Form
	{
		#region PluginHost Class

		private sealed class PluginHost : IHost
		{
			private MainForm mainForm;

			public PluginHost(MainForm mainForm) { this.mainForm = mainForm; }

			public string SelectedFileName
			{
				get
				{
					if (mainForm.treeView.SelectedNode != null)
						return mainForm.treeView.SelectedNode.FullPath;
					else
						return null;
				}
			}

			public Color ViewerBackColor { get { return Settings.Default.ViewerBackColor; } }

			public void StatusMessage(string text) { mainForm.statusStrip.Text = text; }

			public IntPtr Handle { get { return mainForm.Handle; } }
		}

		#endregion

		private PluginHost pluginsHost;
		private IMpqFileSystem fileSystem;
		private Dictionary<string, TreeNode> nodeDictionnary;
		private List<TreeNode> temporaryNodeList;
		private Dictionary<string, FileViewer> fileViewerAssociations, fileViewers;
		private FileViewer currentViewer;
		private DirectoryViewer directoryViewer;
		private NodePropertiesForm nodePropertiesForm;
		private LanguagePackDialog languagePackDialog;
		private ExtractionSettingsDialog extractionSettingsDialog;
		private ExtractionProgressionDialog extractionProgressionDialog;
		private OptionsForm optionsForm;
		private Stack<TreeNode> extractionStack;

		public MainForm()
		{
			pluginsHost = new PluginHost(this);
			InitializeComponent();
			Text = Resources.AppTitle;
			languagePackDialog = new LanguagePackDialog();
			extractionSettingsDialog = new ExtractionSettingsDialog();
			extractionSettingsDialog.DestinationDirectory = Settings.Default.ExtractionDirectory;
			extractionSettingsDialog.Recurse = Settings.Default.ExtractionRecurse;
			extractionSettingsDialog.OverwriteFiles = Settings.Default.ExtractionOverwriteFiles;
			extractionProgressionDialog = new ExtractionProgressionDialog();
			nodePropertiesForm = new NodePropertiesForm(this);
			nodeDictionnary = new Dictionary<string, TreeNode>();
			temporaryNodeList = new List<TreeNode>();
			fileViewers = new Dictionary<string, FileViewer>();
			fileViewerAssociations = new Dictionary<string, FileViewer>();
			LoadEmbeddedViewers();
			LoadPlugins();
			ResolveAssociations();
			LoadIcons();
			AdjustStyles();
			ApplySettings();
		}

		#region Settings Management

		List<IPluginSettings> GetSettingsList()
		{
			List<IPluginSettings> settingsList = new List<IPluginSettings>();

			settingsList.Add(new GeneralSettings());

			foreach (FileViewer viewer in fileViewers.Values)
				if (viewer.Settings != null)
					settingsList.Add(viewer.Settings);

			return settingsList;
		}

		void ResetSettings(IList<IPluginSettings> settingsList)
		{
			foreach (IPluginSettings item in settingsList)
				item.Reset();
		}

		void SaveSettings(IList<IPluginSettings> settingsList)
		{
			foreach (IPluginSettings item in settingsList)
				item.Save();
		}

		void ApplySettings()
		{
			foreach (FileViewer viewer in fileViewers.Values)
				viewer.ApplySettings();
		}

		void ShowOptionsForm()
		{
			List<IPluginSettings> settingsList = GetSettingsList();

			if (optionsForm == null)
				optionsForm = new OptionsForm();

			optionsForm.Settings = settingsList;

			ResetSettings(settingsList);

			if (optionsForm.ShowDialog(this) == DialogResult.OK)
			{
				SaveSettings(settingsList);
				ApplySettings();
			}
		}

		#endregion

		#region Plugin Management

		private void LoadEmbeddedViewers()
		{
			AddViewer(directoryViewer = new DirectoryViewer(this, pluginsHost));
			AddViewer(new BitmapViewer(pluginsHost));
			AddViewer(new TextViewer(pluginsHost));
			AddViewer(new FontViewer(pluginsHost));
		}

		private void LoadPlugins()
		{
			FileViewer[] viewers = PluginManager.LoadPlugins<FileViewer>(new Type[] { typeof(IHost) }, new object[] { pluginsHost });

			for (int i = 0; i < viewers.Length; i++)
				AddViewer(viewers[i]);
		}

		private void AddViewer(FileViewer fileViewer)
		{
			if (fileViewer != null)
			{
				fileViewer.Dock = DockStyle.Fill;
				fileViewer.Visible = false;
				splitContainer.Panel2.Controls.Add(fileViewer);
				fileViewers.Add(fileViewer.GetType().AssemblyQualifiedName, fileViewer);
			}
		}

		public void ResolveAssociations()
		{
			for (int i = 0; i < Settings.Default.ViewerAssociations.Count; i++)
			{
				string formatLoader = Settings.Default.ViewerAssociations[i];
				string[] parts = formatLoader.Split('|');

				if (parts.Length != 2)
				{
					Settings.Default.ViewerAssociations.RemoveAt(i);
					i--;
				}
				else
				{
					FileViewer fileViewer;

					if (fileViewers.TryGetValue(parts[1], out fileViewer))
					    fileViewerAssociations.Add(parts[0], fileViewer);
				}
			}
		}

		#endregion

		private void AdjustStyles()
		{
			if (NativeMethods.IsVista)
			{
				treeView.HotTracking = true;
				treeView.FullRowSelect = true;
				treeView.ShowLines = false;
			}
		}

		private Icon GetSmallIcon(Icon icon) { return icon.Size == new Size(16, 16) ? icon : new Icon(icon, new Size(16, 16)); }

		private Icon GetLargeIcon(Icon icon) { return icon.Size == new Size(32, 32) ? icon : new Icon(icon, new Size(32, 32)); }

		private void AddIcons(Icon baseIcon)
		{
			file16ImageList.Images.Add(GetSmallIcon(baseIcon));
			file32ImageList.Images.Add(GetLargeIcon(baseIcon));
		}

		private void LoadIcons()
		{
			AddIcons(Resources.UnknownFileIcon);
			AddIcons(Resources.ClosedFolderIcon);
			AddIcons(Resources.OpenFolderIcon);
		}

		private void Merge(ToolStrip source, ToolStrip target)
		{
			if (source != null)
				ToolStripManager.Merge(source, target);
		}

		private void RevertMerge(ToolStrip target, ToolStrip source)
		{
			if (source != null)
				ToolStripManager.RevertMerge(target, source);
		}

		private void SetViewer(FileViewer viewer)
		{
			if (viewer == currentViewer)
				return;

			if (currentViewer != null)
			{
				RevertMerge(menuStrip, currentViewer.Menu);
				RevertMerge(mainToolStrip, currentViewer.MainToolStrip);
				RevertMerge(statusStrip, currentViewer.StatusStrip);
				currentViewer.Visible = false;
			}
			currentViewer = viewer;
			if (viewer != null)
			{
				Merge(currentViewer.Menu, menuStrip);
				Merge(currentViewer.MainToolStrip, mainToolStrip);
				Merge(currentViewer.StatusStrip, statusStrip);
				currentViewer.Visible = true;
			}
		}

		private void SetTitle(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				Text = Resources.AppTitle;
			else
				Text = Resources.AppTitle + " - " + fileName;
		}

		private void ErrorDialog(string message)
		{
			MessageBox.Show(this, message, Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		private void OpenArchive(string filename)
		{
			ClearView();

			if (fileSystem != null) fileSystem.Dispose();
			fileSystem = null;

			try
			{

				var mpqFileSystem = new MpqFileSystem();

				mpqFileSystem.Archives.Add(new MpqArchive(filename));

				fileSystem = mpqFileSystem;

				SetTitle(filename);
				FillTreeView();

				saveAsToolStripMenuItem.Enabled = true;
				saveAsToolStripButton.Enabled = true;
			}
			catch (Exception ex) { ErrorDialog(ex.ToString()); }
		}

		private void OpenWoWFileSystem()
		{
			UseWaitCursor = true;

			Application.DoEvents();

			if (fileSystem != null) fileSystem.Dispose();
			fileSystem = null;

			try
			{
				var wowInstallation = WoWInstallation.Find();

				languagePackDialog.WoWInstallation = wowInstallation;

				foreach (var languagePack in wowInstallation.LanguagePacks)
					if (languagePack.Culture == System.Globalization.CultureInfo.CurrentCulture)
						languagePackDialog.LanguagePack = languagePack;

				if (wowInstallation.LanguagePacks.Count > 1)
					if (languagePackDialog.ShowDialog(this) != DialogResult.OK) return;

				ClearView();
				fileSystem = wowInstallation.CreateFileSystem(languagePackDialog.LanguagePack, false, true);
				SetTitle(wowInstallation.Path);
				FillTreeView();

				saveAsToolStripMenuItem.Enabled = true;
				saveAsToolStripButton.Enabled = true;
			}
			catch (Exception ex) { ErrorDialog(ex.ToString()); }
			finally { UseWaitCursor = false; }
		}

		#region Tree View Filling

		private void ClearView()
		{
			SetViewer(null);

			saveAsToolStripMenuItem.Enabled = false;
			saveAsToolStripButton.Enabled = false;
			propertiesToolStripMenuItem1.Enabled = false;
			propertiesToolStripButton.Enabled = false;

			treeView.Nodes.Clear();
			nodeDictionnary.Clear();

			fileNameToolStripStatusLabel.Text = "";

			SetTitle(null);
		}

		private void FillTreeView()
		{
			var nodeList = new List<TreeNode>();

			var mpqFileSystem = fileSystem as MpqFileSystem;
			var wowFileSystem = fileSystem as WoWMpqFileSystem;

			if (mpqFileSystem == null && wowFileSystem == null) throw new InvalidOperationException();

			int archiveCount = mpqFileSystem != null ? mpqFileSystem.Archives.Count : wowFileSystem.Archives.Count;

			for (int i = 0; i < archiveCount; i++)
			{
				var archive = mpqFileSystem != null ? mpqFileSystem.Archives[i] : wowFileSystem.Archives[i].Archive;
				var archiveKind = wowFileSystem != null ? wowFileSystem.Archives[i].Kind : WoWArchiveKind.Regular;

				foreach (var file in archive.Files)
				{
					if (file.Name != null && file.Name.Length > 0 && (file.Flags & MpqFileFlags.Deleted) == 0)
					{
						if (archiveCount > 1 && file.Name == "(listfile)") continue;

						string[] parts = file.Name.Split('\\');
						string assembledPath = "";
						TreeNode currentNode = null;
						bool isGlobalPatch = (archiveKind & WoWArchiveKind.Global) == WoWArchiveKind.Global;

						if (isGlobalPatch && parts[0] != "base" && parts[0] != wowFileSystem.Locale) continue;

						for (int j = isGlobalPatch ? 1 : 0; j < parts.Length; j++)
						{
							string part = parts[j];
							TreeNode newNode;

							if (assembledPath.Length == 0)
								assembledPath = part.ToUpperInvariant();
							else
								// Since MPQ file names are not case-sensitive,
								// we can sort the files case-insensitively
								assembledPath += '\\' + part.ToUpperInvariant();
							if (nodeDictionnary.TryGetValue(assembledPath, out newNode))
							{
								string nodeText = newNode.Text;

								// This code is for detecting case differences between the two names
								if (nodeText[0] != part[0] || nodeText[1] != part[1])
								{
									// If we detect a difference, we try to choose the best one, which probably is not the one ALL IN CAPS
									if (nodeText[1] == char.ToUpperInvariant(nodeText[1])) // If second character is capitalized, assume the name is capitalized
										newNode.Text = part;
								}
								currentNode = newNode;
							}
							else
							{
								newNode = new TreeNode(part);
								newNode.ContextMenuStrip = fileContextMenuStrip;

								if (j == parts.Length - 1)
								{
									newNode.Tag = file;
								}
								else
								{
									newNode.ImageIndex = 1;
									newNode.SelectedImageIndex = 2;
									//newNode.Tag = false;
									newNode.Tag = new List<TreeNode>();
								}

								if (currentNode == null)
								{
									nodeList.Add(newNode);
								}
								else
								{
									//currentNode.Nodes.Add(newNode);
									(currentNode.Tag as List<TreeNode>).Add(newNode);
								}

								nodeDictionnary.Add(assembledPath, newNode);
								currentNode = newNode;
							}
						}
					}
				}
			}
			// Sort top-level nodes alphabetically before adding them to the treeview
			SortNodeList(nodeList);

			Application.DoEvents();
			// Add all the nodes in a single pass, avoiding any bottlenecks caused by repeated Win32 interop
			treeView.BeginUpdate();
			treeView.Nodes.AddRange(nodeList.ToArray());
			treeView.EndUpdate();
			Application.DoEvents();
			// Garbage collection after this memory-intensive operation
			GC.Collect();
		}

		private static int CompareNodes(TreeNode x, TreeNode y)
		{
			if (x.ImageIndex == 1 && y.ImageIndex != 1) // Case where x is a directory but y isn't
				return -1;
			else if (y.ImageIndex == 1 && x.ImageIndex != 1) // Case where y is a directory but x isn't
				return 1;
			else
				return string.Compare(x.Text, y.Text, StringComparison.InvariantCultureIgnoreCase);
		}

		private void SortNodeList(List<TreeNode> nodeList)
		{
			// Sort the root list
			nodeList.Sort(CompareNodes);
			// Sort the sub-lists and build the sub-tree
			foreach (TreeNode treeNode in nodeList)
				if (treeNode.Tag is List<TreeNode>)
				{
					List<TreeNode> subNodeList = (List<TreeNode>)treeNode.Tag;

					treeNode.Tag = null;
					SortNodeList(subNodeList);
					treeNode.Nodes.AddRange(subNodeList.ToArray());
				}
		}

		#endregion

		#region File Extractions

		internal void InteractiveExtractFile(MpqFile file)
		{
			string fileName = Path.GetFileName(file.Name),
					ext = Path.GetExtension(fileName).ToLowerInvariant();

			if (ext == null || ext.Length == 0)
				saveFileDialog.Filter = "";
			else
				saveFileDialog.Filter = ext.ToUpperInvariant() + " Files (*" + ext.ToLowerInvariant() + ")|*" + ext.ToLowerInvariant();
			saveFileDialog.FileName = fileName;
			if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				saveFileDialog.InitialDirectory = Path.GetDirectoryName(saveFileDialog.FileName);
				ExtractFile(file, saveFileDialog.FileName);
			}
		}

		internal void ExtractFile(MpqFile file, string fileName)
		{
			byte[] buffer;

			try
			{
				using (var inputStream = file.Open())
				using (var outputStream = File.OpenWrite(fileName))
				{
					buffer = new byte[4096];

					int length;
					do
					{
						length = inputStream.Read(buffer, 0, 4096);
						outputStream.Write(buffer, 0, length);
					}
					while (length == 4096);
				}
			}
			catch (Exception ex) { ErrorDialog(ex.ToString()); }
		}

		private void InteractiveExtractNodes(TreeNode[] nodes)
		{
			extractionSettingsDialog.AllowRecurse = true;

			if (extractionSettingsDialog.ShowDialog(this) != DialogResult.OK) return;

			// Save the settings now…
			Settings.Default.ExtractionDirectory = extractionSettingsDialog.DestinationDirectory;
			Settings.Default.ExtractionRecurse = extractionSettingsDialog.Recurse;
			Settings.Default.ExtractionOverwriteFiles = extractionSettingsDialog.OverwriteFiles;

			Settings.Default.Save();

			var directoryInfo = new DirectoryInfo(extractionSettingsDialog.DestinationDirectory);
			ulong totalSize = 0;
			int totalFileCount = 0;

			// Initialize the stack.
			extractionStack = extractionStack ?? new Stack<TreeNode>();
			extractionStack.Clear();

			// Proceed to a tree walk for each requested node, in order to compute total file count and total size
			{
				foreach (var node in nodes)
				{
					var currentNode = node;
					do
					{
						if (currentNode.Nodes.Count > 0 && (extractionSettingsDialog.Recurse || extractionStack.Count == 0))
						{
							extractionStack.Push(currentNode);
							currentNode = currentNode.FirstNode;
						}
						else
						{
							var file = currentNode.Tag as MpqFile;

							if (file != null)
							{
								totalSize += unchecked((ulong)file.Size);
								checked { totalFileCount++; } // Maybe this will fail someday, but I don't think the current Win32 ListView can handle more than 2^31 nodes…
							}

							while (currentNode == null || (currentNode != node && (currentNode = currentNode.NextNode) == null) && extractionStack.Count > 0)
								if ((currentNode = extractionStack.Pop()) != node)
									currentNode = currentNode.NextNode;
						}
					} while (currentNode != node);
				}
			}

			// TODO: Make something a little better…
			if (totalFileCount == 0) return;

			// Initialize the extraction progress dialog
			extractionProgressionDialog.TotalSize = totalSize;
			extractionProgressionDialog.ProcessedSize = 0;

			extractionProgressionDialog.TotalFileCount = totalFileCount;
			extractionProgressionDialog.ProcessedFileCount = 0;

			extractionProgressionDialog.CurrentFileName = null;

			// Proceed to a tree walk for each requested node, but this time for doing real work
			// The extraction will be done on a separate thread, with progress displayed by the modal dialog
			extractionProgressionDialog.ShowDialog
			(
				this,
				(dialog, state) =>
				{
					var buffer = new byte[4096];
					int extractedFileCount = 0;

					foreach (var node in nodes)
					{
						string directoryPhysicalPath = directoryInfo.FullName;
						var currentNode = node;
						do
						{
							if (currentNode.Nodes.Count > 0 && (extractionSettingsDialog.Recurse || extractionStack.Count == 0))
							{
								// Store the relative path into the tag, which should be null for directories…
								currentNode.Tag = directoryPhysicalPath;
								directoryPhysicalPath = Path.Combine(directoryPhysicalPath, currentNode.Text);

								if (!Directory.Exists(directoryPhysicalPath))
									try { Directory.CreateDirectory(directoryPhysicalPath); }
									catch (Exception ex)
									{
										dialog.ErrorDialog(ex.Message);
										return;
									}

								extractionStack.Push(currentNode);
								currentNode = currentNode.FirstNode;
							}
							else
							{
								var file = currentNode.Tag as MpqFile;

								if (file != null)
								{
									string filePhysicalPath = Path.Combine(directoryPhysicalPath, currentNode.Text);
									bool canExtract = false;

									dialog.UpdateFileInformation(extractedFileCount + 1, file.Name);

									if (extractionSettingsDialog.OverwriteFiles || !File.Exists(filePhysicalPath)) canExtract = true;
									else
									{
										switch (dialog.AskForOverwrite(currentNode.Text, directoryPhysicalPath, unchecked((ulong)file.Size)))
										{
											case DialogResult.Yes: canExtract = true; break;
											case DialogResult.No: canExtract = false; break;
											case DialogResult.Cancel: return;
										}
									}

									if (canExtract)
									{
										try
										{
											using (var inputStream = file.Open())
											using (var outputStream = File.OpenWrite(filePhysicalPath))
											{
												int byteCounter = 0;
												int length;

												do
												{
													length = inputStream.Read(buffer, 0, 4096);
													outputStream.Write(buffer, 0, length);
													if ((byteCounter += length) >= 0x100000)
													{
														dialog.ProcessedSize += 0x100000UL;
														byteCounter -= 0x100000;
													}
												}
												while (length == 4096);

												if (byteCounter > 0) dialog.ProcessedSize += unchecked((ulong)byteCounter);
												extractedFileCount++;
											}
										}
										catch (Exception ex)
										{
											ErrorDialog(ex.ToString());
											return;
										}
									}
									else
									{
										dialog.TotalSize = totalSize -= unchecked((ulong)file.Size);
										dialog.TotalFileCount = totalFileCount--;
									}
								}

								while (currentNode == null || (currentNode != node && (currentNode = currentNode.NextNode) == null) && extractionStack.Count > 0)
								{
									currentNode = extractionStack.Pop();
									directoryPhysicalPath = currentNode.Tag as string;
									currentNode.Tag = null; // Clean the tag when no longer needed

									if (currentNode != node)
										currentNode = currentNode.NextNode;
								}
							}
						} while (currentNode != node);
					}
				}
			);
		}

		#endregion

		protected override void OnClosed(EventArgs e)
		{
			// Just in case we forgot to save the settings before…
			Settings.Default.Save();
			base.OnClosed(e);
		}

		#region File Menu Actions

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
				OpenArchive(openFileDialog.FileName);
			}
		}

		private void wowMpqFileSystemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenWoWFileSystem();
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var file = treeView.SelectedNode.Tag as MpqFile;

			if (file != null) InteractiveExtractFile(file);
			else InteractiveExtractNodes(new[] { treeView.SelectedNode });
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		#endregion

		#region Tools Menu Actions

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowOptionsForm();
		}

		#endregion

		#region TreeView Context Menu Actions

		private void fileContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			//if (treeView.SelectedNode == null || treeView.SelectedNode.Tag == null)
			//    extractToolStripMenuItem.Enabled = false;
			//else
			//    extractToolStripMenuItem.Enabled = true;
		}

		private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treeView.SelectedNode != null)
			{
				nodePropertiesForm.Node = treeView.SelectedNode;
				nodePropertiesForm.ShowDialog(this);
			}
		}

		#endregion

		#region TreeView Actions

		private void treeView_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TreeNode node = treeView.HitTest(e.X, e.Y).Node;
				if (node != null)
					treeView.SelectedNode = node;
			}
		}

		private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			MpqFile file = e.Node.Tag as MpqFile;

			if (file == null)
			{
				directoryViewer.RootNode = e.Node;

				SetViewer(directoryViewer);

				//saveAsToolStripMenuItem.Enabled = false;
				//saveAsToolStripButton.Enabled = false;

				fileNameToolStripStatusLabel.Text = "";
			}
			else
			{
				string ext = Path.GetExtension(file.Name).ToLowerInvariant();
				FileViewer fileViewer;

				//saveAsToolStripMenuItem.Enabled = true;
				//saveAsToolStripButton.Enabled = true;
				if (fileViewerAssociations.TryGetValue(ext, out fileViewer))
				{
					try
					{
						SetViewer(fileViewer);
						fileViewer.File = file;
					}
					catch (Exception ex) { ErrorDialog(ex.ToString()); }
				}
				else
					SetViewer(null);
				fileNameToolStripStatusLabel.Text = file.Name;
			}
			propertiesToolStripMenuItem1.Enabled = true;
			propertiesToolStripButton.Enabled = true;
		}

		//private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		//{
		//    TreeNode treeNode = e.Node;

		//    if (treeNode.Tag is bool && (bool)treeNode.Tag == false)
		//    {
		//        if (treeNode.Nodes.Count > 1)
		//        {
		//            temporaryNodeList.Clear();
		//            temporaryNodeList.Capacity = treeNode.Nodes.Count;

		//            foreach (TreeNode node in treeNode.Nodes)
		//                temporaryNodeList.Add(node);

		//            SortNodeList(temporaryNodeList);

		//            //treeView.BeginUpdate();
		//            e.Node.Nodes.Clear();
		//            e.Node.Nodes.AddRange(temporaryNodeList.ToArray());
		//            //treeView.EndUpdate();
		//            //treeView.BeginUpdate();
		//            //for (int i = 0; i < temporaryNodeList.Count; i++)
		//            //    treeNode.Nodes[i] = temporaryNodeList[i];
		//            //treeView.EndUpdate();
		//        }
		//        e.Node.Tag = true;
		//    }
		//}

		#endregion
	}
}