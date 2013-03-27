#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

namespace CrystalMpq.Explorer
{
	partial class MainForm
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.fileNameToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.treeView = new CrystalMpq.Explorer.EnhancedTreeView();
			this.file16ImageList = new System.Windows.Forms.ImageList(this.components);
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.wowFileSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.selectFileSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.saveAsToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.propertiesToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.fileContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.file32ImageList = new System.Windows.Forms.ImageList(this.components);
			this.toolStripContainer.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.fileContextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.BottomToolStripPanel
			// 
			this.toolStripContainer.BottomToolStripPanel.Controls.Add(this.statusStrip);
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.splitContainer);
			resources.ApplyResources(this.toolStripContainer.ContentPanel, "toolStripContainer.ContentPanel");
			resources.ApplyResources(this.toolStripContainer, "toolStripContainer");
			this.toolStripContainer.Name = "toolStripContainer";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuStrip);
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.mainToolStrip);
			// 
			// statusStrip
			// 
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNameToolStripStatusLabel});
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
			// 
			// fileNameToolStripStatusLabel
			// 
			this.fileNameToolStripStatusLabel.Name = "fileNameToolStripStatusLabel";
			resources.ApplyResources(this.fileNameToolStripStatusLabel, "fileNameToolStripStatusLabel");
			this.fileNameToolStripStatusLabel.Spring = true;
			// 
			// splitContainer
			// 
			resources.ApplyResources(this.splitContainer, "splitContainer");
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.treeView);
			// 
			// treeView
			// 
			resources.ApplyResources(this.treeView, "treeView");
			this.treeView.ExplorerStyle = true;
			this.treeView.FadePlusMinus = true;
			this.treeView.ImageList = this.file16ImageList;
			this.treeView.Name = "treeView";
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			this.treeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseClick);
			// 
			// file16ImageList
			// 
			this.file16ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			resources.ApplyResources(this.file16ImageList, "file16ImageList");
			this.file16ImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// menuStrip
			// 
			resources.ApplyResources(this.menuStrip, "menuStrip");
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
			this.menuStrip.Name = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openCollectionToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.propertiesToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Image = global::CrystalMpq.Explorer.Properties.Resources.OpenToolbarIcon;
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// openCollectionToolStripMenuItem
			// 
			this.openCollectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wowFileSystemToolStripMenuItem,
            this.selectFileSystemToolStripMenuItem});
			this.openCollectionToolStripMenuItem.Name = "openCollectionToolStripMenuItem";
			resources.ApplyResources(this.openCollectionToolStripMenuItem, "openCollectionToolStripMenuItem");
			// 
			// wowFileSystemToolStripMenuItem
			// 
			this.wowFileSystemToolStripMenuItem.Name = "wowFileSystemToolStripMenuItem";
			resources.ApplyResources(this.wowFileSystemToolStripMenuItem, "wowFileSystemToolStripMenuItem");
			this.wowFileSystemToolStripMenuItem.Click += new System.EventHandler(this.wowMpqFileSystemToolStripMenuItem_Click);
			// 
			// selectFileSystemToolStripMenuItem
			// 
			this.selectFileSystemToolStripMenuItem.Name = "selectFileSystemToolStripMenuItem";
			resources.ApplyResources(this.selectFileSystemToolStripMenuItem, "selectFileSystemToolStripMenuItem");
			// 
			// saveAsToolStripMenuItem
			// 
			resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
			this.saveAsToolStripMenuItem.Image = global::CrystalMpq.Explorer.Properties.Resources.SaveToolbarIcon;
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// propertiesToolStripMenuItem1
			// 
			resources.ApplyResources(this.propertiesToolStripMenuItem1, "propertiesToolStripMenuItem1");
			this.propertiesToolStripMenuItem1.Image = global::CrystalMpq.Explorer.Properties.Resources.PropertiesToolbarIcon;
			this.propertiesToolStripMenuItem1.Name = "propertiesToolStripMenuItem1";
			this.propertiesToolStripMenuItem1.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// mainToolStrip
			// 
			resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.saveAsToolStripButton,
            this.propertiesToolStripButton});
			this.mainToolStrip.Name = "mainToolStrip";
			// 
			// openToolStripButton
			// 
			this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = global::CrystalMpq.Explorer.Properties.Resources.OpenToolbarIcon;
			this.openToolStripButton.Name = "openToolStripButton";
			resources.ApplyResources(this.openToolStripButton, "openToolStripButton");
			this.openToolStripButton.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveAsToolStripButton
			// 
			this.saveAsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.saveAsToolStripButton, "saveAsToolStripButton");
			this.saveAsToolStripButton.Image = global::CrystalMpq.Explorer.Properties.Resources.SaveToolbarIcon;
			this.saveAsToolStripButton.Name = "saveAsToolStripButton";
			this.saveAsToolStripButton.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// propertiesToolStripButton
			// 
			this.propertiesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.propertiesToolStripButton, "propertiesToolStripButton");
			this.propertiesToolStripButton.Image = global::CrystalMpq.Explorer.Properties.Resources.PropertiesToolbarIcon;
			this.propertiesToolStripButton.Name = "propertiesToolStripButton";
			this.propertiesToolStripButton.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// fileContextMenuStrip
			// 
			this.fileContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractToolStripMenuItem,
            this.propertiesToolStripMenuItem});
			this.fileContextMenuStrip.Name = "fileContextMenuStrip";
			resources.ApplyResources(this.fileContextMenuStrip, "fileContextMenuStrip");
			this.fileContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.fileContextMenuStrip_Opening);
			// 
			// extractToolStripMenuItem
			// 
			this.extractToolStripMenuItem.Image = global::CrystalMpq.Explorer.Properties.Resources.SaveToolbarIcon;
			this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
			resources.ApplyResources(this.extractToolStripMenuItem, "extractToolStripMenuItem");
			this.extractToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
			// 
			// propertiesToolStripMenuItem
			// 
			this.propertiesToolStripMenuItem.Image = global::CrystalMpq.Explorer.Properties.Resources.PropertiesToolbarIcon;
			this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
			resources.ApplyResources(this.propertiesToolStripMenuItem, "propertiesToolStripMenuItem");
			this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
			// 
			// openFileDialog
			// 
			resources.ApplyResources(this.openFileDialog, "openFileDialog");
			this.openFileDialog.RestoreDirectory = true;
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.RestoreDirectory = true;
			// 
			// file32ImageList
			// 
			this.file32ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			resources.ApplyResources(this.file32ImageList, "file32ImageList");
			this.file32ImageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// MainForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.toolStripContainer);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.toolStripContainer.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.fileContextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.SplitContainer splitContainer;
		private EnhancedTreeView treeView;
		private System.Windows.Forms.ContextMenuStrip fileContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton openToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton saveAsToolStripButton;
		private System.Windows.Forms.ToolStripStatusLabel fileNameToolStripStatusLabel;
		internal System.Windows.Forms.ImageList file32ImageList;
		internal System.Windows.Forms.ImageList file16ImageList;
		private System.Windows.Forms.ToolStripButton propertiesToolStripButton;
		private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem openCollectionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wowFileSystemToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem selectFileSystemToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
	}
}