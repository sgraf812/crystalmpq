#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

namespace CrystalMpq.Explorer.Viewers
{
	partial class TextViewer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextViewer));
			this.textBox = new System.Windows.Forms.TextBox();
			this.mainToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.findToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.encodingToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.searchStatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.mainToolStrip.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox
			// 
			this.textBox.BackColor = System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.textBox, "textBox");
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			// 
			// mainToolStrip
			// 
			this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.copyToolStripButton,
            this.toolStripSeparator4,
            this.findToolStripButton,
            this.toolStripSeparator5,
            this.encodingToolStripComboBox});
			resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
			this.mainToolStrip.Name = "mainToolStrip";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
			// 
			// copyToolStripButton
			// 
			this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.copyToolStripButton.Image = global::CrystalMpq.Explorer.Properties.Resources.CopyToolbarIcon;
			this.copyToolStripButton.Name = "copyToolStripButton";
			resources.ApplyResources(this.copyToolStripButton, "copyToolStripButton");
			this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			// 
			// findToolStripButton
			// 
			this.findToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.findToolStripButton.Image = global::CrystalMpq.Explorer.Properties.Resources.FindToolbarIcon;
			this.findToolStripButton.Name = "findToolStripButton";
			resources.ApplyResources(this.findToolStripButton, "findToolStripButton");
			this.findToolStripButton.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
			// 
			// encodingToolStripComboBox
			// 
			this.encodingToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.encodingToolStripComboBox.Name = "encodingToolStripComboBox";
			resources.ApplyResources(this.encodingToolStripComboBox, "encodingToolStripComboBox");
			this.encodingToolStripComboBox.Sorted = true;
			this.encodingToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.encodingToolStripComboBox_SelectedIndexChanged);
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
			resources.ApplyResources(this.menuStrip, "menuStrip");
			this.menuStrip.Name = "menuStrip";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.toolStripSeparator1,
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.findToolStripMenuItem,
            this.findNextToolStripMenuItem});
			this.editToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
			this.editToolStripMenuItem.MergeIndex = 1;
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
			this.editToolStripMenuItem.DropDownOpening += new System.EventHandler(this.editToolStripMenuItem_DropDownOpening);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Image = global::CrystalMpq.Explorer.Properties.Resources.CopyToolbarIcon;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			// 
			// selectAllToolStripMenuItem
			// 
			this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
			resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
			this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// findToolStripMenuItem
			// 
			this.findToolStripMenuItem.Image = global::CrystalMpq.Explorer.Properties.Resources.FindToolbarIcon;
			this.findToolStripMenuItem.Name = "findToolStripMenuItem";
			resources.ApplyResources(this.findToolStripMenuItem, "findToolStripMenuItem");
			this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
			// 
			// findNextToolStripMenuItem
			// 
			resources.ApplyResources(this.findNextToolStripMenuItem, "findNextToolStripMenuItem");
			this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
			this.findNextToolStripMenuItem.Click += new System.EventHandler(this.findNextToolStripMenuItem_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchStatusToolStripStatusLabel});
			resources.ApplyResources(this.statusStrip, "statusStrip");
			this.statusStrip.Name = "statusStrip";
			// 
			// searchStatusToolStripStatusLabel
			// 
			this.searchStatusToolStripStatusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
			this.searchStatusToolStripStatusLabel.Image = global::CrystalMpq.Explorer.Properties.Resources.ExclamationToolbarIcon;
			this.searchStatusToolStripStatusLabel.Name = "searchStatusToolStripStatusLabel";
			resources.ApplyResources(this.searchStatusToolStripStatusLabel, "searchStatusToolStripStatusLabel");
			// 
			// TextViewer
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.mainToolStrip);
			this.Controls.Add(this.statusStrip);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.textBox);
			this.Name = "TextViewer";
			this.mainToolStrip.ResumeLayout(false);
			this.mainToolStrip.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.ToolStrip mainToolStrip;
		private System.Windows.Forms.ToolStripComboBox encodingToolStripComboBox;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem findNextToolStripMenuItem;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStripStatusLabel searchStatusToolStripStatusLabel;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton copyToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton findToolStripButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
	}
}
