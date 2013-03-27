#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

namespace CrystalMpq.Explorer.BaseViewers
{
	partial class ClientDatabaseViewer
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

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientDatabaseViewer));
			this.fieldCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.columnTypeContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.integerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.floatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hexadecimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.exportToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.recordCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.listView = new System.Windows.Forms.ListView();
			this.columnTypeContextMenuStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// fieldCountToolStripStatusLabel
			// 
			this.fieldCountToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.fieldCountToolStripStatusLabel.Name = "fieldCountToolStripStatusLabel";
			resources.ApplyResources(this.fieldCountToolStripStatusLabel, "fieldCountToolStripStatusLabel");
			// 
			// columnTypeContextMenuStrip
			// 
			this.columnTypeContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.integerToolStripMenuItem,
            this.stringToolStripMenuItem,
            this.floatToolStripMenuItem,
            this.hexadecimalToolStripMenuItem});
			this.columnTypeContextMenuStrip.Name = "columnTypeContextMenuStrip";
			resources.ApplyResources(this.columnTypeContextMenuStrip, "columnTypeContextMenuStrip");
			// 
			// integerToolStripMenuItem
			// 
			this.integerToolStripMenuItem.Name = "integerToolStripMenuItem";
			resources.ApplyResources(this.integerToolStripMenuItem, "integerToolStripMenuItem");
			this.integerToolStripMenuItem.Click += new System.EventHandler(this.integerToolStripMenuItem_Click);
			// 
			// stringToolStripMenuItem
			// 
			this.stringToolStripMenuItem.Name = "stringToolStripMenuItem";
			resources.ApplyResources(this.stringToolStripMenuItem, "stringToolStripMenuItem");
			this.stringToolStripMenuItem.Click += new System.EventHandler(this.stringToolStripMenuItem_Click);
			// 
			// floatToolStripMenuItem
			// 
			this.floatToolStripMenuItem.Name = "floatToolStripMenuItem";
			resources.ApplyResources(this.floatToolStripMenuItem, "floatToolStripMenuItem");
			this.floatToolStripMenuItem.Click += new System.EventHandler(this.floatToolStripMenuItem_Click);
			// 
			// hexadecimalToolStripMenuItem
			// 
			this.hexadecimalToolStripMenuItem.Name = "hexadecimalToolStripMenuItem";
			resources.ApplyResources(this.hexadecimalToolStripMenuItem, "hexadecimalToolStripMenuItem");
			this.hexadecimalToolStripMenuItem.Click += new System.EventHandler(this.hexadecimalToolStripMenuItem_Click);
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			resources.ApplyResources(this.menuStrip, "menuStrip");
			this.menuStrip.Name = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem});
			this.fileToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
			this.fileToolStripMenuItem.MergeIndex = 0;
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
			// 
			// exportToolStripMenuItem
			// 
			resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
			this.exportToolStripMenuItem.Image = global::CrystalMpq.Explorer.BaseViewers.Properties.Resources.ExportToolbarIcon;
			this.exportToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.exportToolStripMenuItem.MergeIndex = 3;
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "csv";
			resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
			this.saveFileDialog.RestoreDirectory = true;
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripButton});
			resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
			this.mainToolStrip.Name = "mainToolStrip";
			// 
			// exportToolStripButton
			// 
			this.exportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			resources.ApplyResources(this.exportToolStripButton, "exportToolStripButton");
			this.exportToolStripButton.Image = global::CrystalMpq.Explorer.BaseViewers.Properties.Resources.ExportToolbarIcon;
			this.exportToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.exportToolStripButton.MergeIndex = 2;
			this.exportToolStripButton.Name = "exportToolStripButton";
			this.exportToolStripButton.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fieldCountToolStripStatusLabel,
            this.recordCountToolStripStatusLabel});
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			// 
			// recordCountToolStripStatusLabel
			// 
			this.recordCountToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.recordCountToolStripStatusLabel.Name = "recordCountToolStripStatusLabel";
			resources.ApplyResources(this.recordCountToolStripStatusLabel, "recordCountToolStripStatusLabel");
			// 
			// listView
			// 
			resources.ApplyResources(this.listView, "listView");
			this.listView.FullRowSelect = true;
			this.listView.GridLines = true;
			this.listView.HideSelection = false;
			this.listView.Name = "listView";
			this.listView.ShowGroups = false;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.View = System.Windows.Forms.View.Details;
			this.listView.VirtualMode = true;
			this.listView.CacheVirtualItems += new System.Windows.Forms.CacheVirtualItemsEventHandler(this.listView_CacheVirtualItems);
			this.listView.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listView_RetrieveVirtualItem);
			// 
			// ClientDatabaseViewer
			// 
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.listView);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.statusStrip);
			this.Name = "ClientDatabaseViewer";
			resources.ApplyResources(this, "$this");
			this.columnTypeContextMenuStrip.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip columnTypeContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem integerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stringToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem floatToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem hexadecimalToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripButton exportToolStripButton;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel recordCountToolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel fieldCountToolStripStatusLabel;
		private System.Windows.Forms.ListView listView;
	}
}
