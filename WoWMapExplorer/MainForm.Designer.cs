#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

namespace WoWMapExplorer
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
			System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
			this.renderPanel = new System.Windows.Forms.Panel();
			this.menuToolStrip = new System.Windows.Forms.ToolStrip();
			this.continentToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.continentToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.zoneToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.zoneToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.zoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripContainer.ContentPanel.SuspendLayout();
			this.toolStripContainer.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer.SuspendLayout();
			this.menuToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripContainer
			// 
			// 
			// toolStripContainer.ContentPanel
			// 
			this.toolStripContainer.ContentPanel.Controls.Add(this.renderPanel);
			this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(292, 241);
			this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer.LeftToolStripPanelVisible = false;
			this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer.Name = "toolStripContainer";
			this.toolStripContainer.RightToolStripPanelVisible = false;
			this.toolStripContainer.Size = new System.Drawing.Size(292, 266);
			this.toolStripContainer.TabIndex = 0;
			this.toolStripContainer.Text = "toolStripContainer1";
			// 
			// toolStripContainer.TopToolStripPanel
			// 
			this.toolStripContainer.TopToolStripPanel.Controls.Add(this.menuToolStrip);
			// 
			// renderPanel
			// 
			this.renderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.renderPanel.Location = new System.Drawing.Point(0, 0);
			this.renderPanel.Name = "renderPanel";
			this.renderPanel.Size = new System.Drawing.Size(292, 241);
			this.renderPanel.TabIndex = 0;
			this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseMove);
			this.renderPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.renderPanel_MouseClick);
			this.renderPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.renderPanel_Paint);
			// 
			// menuToolStrip
			// 
			this.menuToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.continentToolStripLabel,
            this.continentToolStripComboBox,
            toolStripSeparator1,
            this.zoneToolStripLabel,
            this.zoneToolStripComboBox,
            this.zoomOutToolStripButton});
			this.menuToolStrip.Location = new System.Drawing.Point(3, 0);
			this.menuToolStrip.Name = "menuToolStrip";
			this.menuToolStrip.Size = new System.Drawing.Size(289, 25);
			this.menuToolStrip.TabIndex = 0;
			// 
			// continentToolStripLabel
			// 
			this.continentToolStripLabel.Name = "continentToolStripLabel";
			this.continentToolStripLabel.Size = new System.Drawing.Size(54, 22);
			this.continentToolStripLabel.Text = "Continent";
			// 
			// continentToolStripComboBox
			// 
			this.continentToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.continentToolStripComboBox.Name = "continentToolStripComboBox";
			this.continentToolStripComboBox.Size = new System.Drawing.Size(200, 25);
			this.continentToolStripComboBox.Sorted = true;
			this.continentToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.continentToolStripComboBox_SelectedIndexChanged);
			// 
			// zoneToolStripLabel
			// 
			this.zoneToolStripLabel.Name = "zoneToolStripLabel";
			this.zoneToolStripLabel.Size = new System.Drawing.Size(31, 13);
			this.zoneToolStripLabel.Text = "Zone";
			// 
			// zoneToolStripComboBox
			// 
			this.zoneToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.zoneToolStripComboBox.Name = "zoneToolStripComboBox";
			this.zoneToolStripComboBox.Size = new System.Drawing.Size(200, 21);
			this.zoneToolStripComboBox.Sorted = true;
			this.zoneToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.zoneToolStripComboBox_SelectedIndexChanged);
			// 
			// zoomOutToolStripButton
			// 
			this.zoomOutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("zoomOutToolStripButton.Image")));
			this.zoomOutToolStripButton.Name = "zoomOutToolStripButton";
			this.zoomOutToolStripButton.Size = new System.Drawing.Size(74, 20);
			this.zoomOutToolStripButton.Text = "Zoom Out";
			this.zoomOutToolStripButton.ToolTipText = "Zoom Out";
			this.zoomOutToolStripButton.Click += new System.EventHandler(this.zoomOutToolStripButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.toolStripContainer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "WoW Map Explorer";
			this.toolStripContainer.ContentPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer.TopToolStripPanel.PerformLayout();
			this.toolStripContainer.ResumeLayout(false);
			this.toolStripContainer.PerformLayout();
			this.menuToolStrip.ResumeLayout(false);
			this.menuToolStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer;
		private System.Windows.Forms.ToolStrip menuToolStrip;
		private System.Windows.Forms.ToolStripLabel continentToolStripLabel;
		private System.Windows.Forms.ToolStripComboBox continentToolStripComboBox;
		private System.Windows.Forms.ToolStripLabel zoneToolStripLabel;
		private System.Windows.Forms.ToolStripComboBox zoneToolStripComboBox;
		private System.Windows.Forms.Panel renderPanel;
		private System.Windows.Forms.ToolStripButton zoomOutToolStripButton;

	}
}

