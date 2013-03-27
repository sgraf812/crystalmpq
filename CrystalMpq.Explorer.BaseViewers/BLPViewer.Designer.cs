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
	partial class BLPViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BLPViewer));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.exportToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.sizeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip.SuspendLayout();
			this.mainToolStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
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
			this.exportToolStripButton.Image = global::CrystalMpq.Explorer.BaseViewers.Properties.Resources.ExportToolbarIcon;
			resources.ApplyResources(this.exportToolStripButton, "exportToolStripButton");
			this.exportToolStripButton.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.exportToolStripButton.MergeIndex = 2;
			this.exportToolStripButton.Name = "exportToolStripButton";
			this.exportToolStripButton.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeToolStripStatusLabel});
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			// 
			// sizeToolStripStatusLabel
			// 
			this.sizeToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.sizeToolStripStatusLabel.Name = "sizeToolStripStatusLabel";
			resources.ApplyResources(this.sizeToolStripStatusLabel, "sizeToolStripStatusLabel");
			// 
			// BLPViewer
			// 
			resources.ApplyResources(this, "$this");
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.menuStrip);
			this.Name = "BLPViewer";
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

		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripButton exportToolStripButton;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel sizeToolStripStatusLabel;
		private System.Windows.Forms.MenuStrip menuStrip;
	}
}

