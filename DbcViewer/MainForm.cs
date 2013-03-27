#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalMpq.DataFormats;
using CrystalMpq.Explorer.BaseViewers;
using CrystalMpq.Explorer.Extensibility;

namespace DbcViewer
{
	public partial class MainForm : Form
	{
		#region PluginHost Class
		private sealed class PluginHost : IHost
		{
			MainForm mainForm;

			public PluginHost(MainForm mainForm) { this.mainForm = mainForm; }

			public string SelectedFileName { get { return ""; } }

			public Color ViewerBackColor { get { return Color.SlateGray; } }

			public void StatusMessage(string text)
			{
			}

			public IntPtr Handle { get { return mainForm.Handle; } }
		}
		#endregion

		PluginHost host;

		public MainForm()
		{
			host = new PluginHost(this);

			InitializeComponent();

			if (databaseViewer.Menu != null)
				ToolStripManager.Merge(databaseViewer.Menu, menuStrip);
			if (databaseViewer.MainToolStrip != null)
				ToolStripManager.Merge(databaseViewer.MainToolStrip, mainToolStrip);
			if (databaseViewer.StatusStrip != null)
				ToolStripManager.Merge(databaseViewer.StatusStrip, statusStrip);
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (openFileDialog.ShowDialog(this) == DialogResult.OK)
			{
				Stream fileStream = null;
				RawClientDatabase database;

				try
				{
					fileStream = File.Open(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
					database = new RawClientDatabase(fileStream);
					databaseViewer.Database = database;
					fileNameToolStripStatusLabel.Text = openFileDialog.FileName;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, Properties.Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					fileNameToolStripStatusLabel.Text = "";
				}
				finally
				{
					if (fileStream != null)
						fileStream.Close();
				}
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}