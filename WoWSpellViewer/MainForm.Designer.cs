#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

namespace WoWSpellViewer
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
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.Label label6;
			this.spellIconPictureBox = new System.Windows.Forms.PictureBox();
			this.spellDescriptionLabel = new System.Windows.Forms.Label();
			this.spellNameLabel = new System.Windows.Forms.Label();
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.navigationToolStrip = new System.Windows.Forms.ToolStrip();
			this.firstItemToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.previousItemToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.indexToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.countToolStripLabel = new System.Windows.Forms.ToolStripLabel();
			this.nextItemToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.lastItemToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.statusStrip = new System.Windows.Forms.StatusStrip();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.spellEffect3Label = new System.Windows.Forms.Label();
			this.spellEffect2Label = new System.Windows.Forms.Label();
			this.spellEffect1Label = new System.Windows.Forms.Label();
			this.spellIdLabel = new System.Windows.Forms.Label();
			this.spellLevelLabel = new System.Windows.Forms.Label();
			this.manaCostLabel = new System.Windows.Forms.Label();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.spellIconPictureBox)).BeginInit();
			this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			this.navigationToolStrip.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(4, 1);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(28, 13);
			label1.TabIndex = 0;
			label1.Text = "Id: ";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(4, 15);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(108, 13);
			label2.TabIndex = 2;
			label2.Text = "Learned at level: ";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(4, 29);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(73, 13);
			label3.TabIndex = 4;
			label3.Text = "Mana cost: ";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(4, 43);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(50, 13);
			label4.TabIndex = 6;
			label4.Text = "Effect 1";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(4, 57);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(50, 13);
			label5.TabIndex = 7;
			label5.Text = "Effect 2";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(4, 71);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(50, 13);
			label6.TabIndex = 8;
			label6.Text = "Effect 3";
			// 
			// spellIconPictureBox
			// 
			this.spellIconPictureBox.Location = new System.Drawing.Point(3, 3);
			this.spellIconPictureBox.Name = "spellIconPictureBox";
			this.spellIconPictureBox.Size = new System.Drawing.Size(66, 66);
			this.spellIconPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.spellIconPictureBox.TabIndex = 1;
			this.spellIconPictureBox.TabStop = false;
			// 
			// spellDescriptionLabel
			// 
			this.spellDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.spellDescriptionLabel.Location = new System.Drawing.Point(3, 72);
			this.spellDescriptionLabel.Name = "spellDescriptionLabel";
			this.spellDescriptionLabel.Size = new System.Drawing.Size(278, 31);
			this.spellDescriptionLabel.TabIndex = 3;
			// 
			// spellNameLabel
			// 
			this.spellNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.spellNameLabel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.spellNameLabel.Location = new System.Drawing.Point(75, 3);
			this.spellNameLabel.Name = "spellNameLabel";
			this.spellNameLabel.Size = new System.Drawing.Size(214, 66);
			this.spellNameLabel.TabIndex = 0;
			this.spellNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.BottomToolStripPanel
			// 
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.navigationToolStrip);
			this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip);
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.tableLayoutPanel);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.spellDescriptionLabel);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.spellIconPictureBox);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.spellNameLabel);
			this.toolStripContainer1.ContentPanel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(284, 193);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.Size = new System.Drawing.Size(284, 264);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip);
			// 
			// navigationToolStrip
			// 
			this.navigationToolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.navigationToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstItemToolStripButton,
            this.previousItemToolStripButton,
            this.indexToolStripTextBox,
            this.countToolStripLabel,
            this.nextItemToolStripButton,
            this.lastItemToolStripButton});
			this.navigationToolStrip.Location = new System.Drawing.Point(3, 0);
			this.navigationToolStrip.Name = "navigationToolStrip";
			this.navigationToolStrip.Size = new System.Drawing.Size(177, 25);
			this.navigationToolStrip.TabIndex = 1;
			this.navigationToolStrip.Text = "Navigation";
			// 
			// firstItemToolStripButton
			// 
			this.firstItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.firstItemToolStripButton.Image = global::WoWSpellViewer.Properties.Resources.FirstItemToolbarIcon;
			this.firstItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.firstItemToolStripButton.Name = "firstItemToolStripButton";
			this.firstItemToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.firstItemToolStripButton.Text = "toolStripButton1";
			this.firstItemToolStripButton.ToolTipText = "First";
			this.firstItemToolStripButton.Click += new System.EventHandler(this.firstItemToolStripButton_Click);
			// 
			// previousItemToolStripButton
			// 
			this.previousItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.previousItemToolStripButton.Image = global::WoWSpellViewer.Properties.Resources.PreviousItemToolbarIcon;
			this.previousItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.previousItemToolStripButton.Name = "previousItemToolStripButton";
			this.previousItemToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.previousItemToolStripButton.Text = "toolStripButton2";
			this.previousItemToolStripButton.ToolTipText = "Previous";
			this.previousItemToolStripButton.Click += new System.EventHandler(this.previousItemToolStripButton_Click);
			// 
			// indexToolStripTextBox
			// 
			this.indexToolStripTextBox.Name = "indexToolStripTextBox";
			this.indexToolStripTextBox.Size = new System.Drawing.Size(50, 25);
			this.indexToolStripTextBox.Text = "0";
			this.indexToolStripTextBox.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.indexToolStripTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.indexToolStripTextBox_Validating);
			this.indexToolStripTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.indexToolStripTextBox_KeyUp);
			// 
			// countToolStripLabel
			// 
			this.countToolStripLabel.Name = "countToolStripLabel";
			this.countToolStripLabel.Size = new System.Drawing.Size(21, 22);
			this.countToolStripLabel.Text = "/ 0";
			// 
			// nextItemToolStripButton
			// 
			this.nextItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.nextItemToolStripButton.Image = global::WoWSpellViewer.Properties.Resources.NextItemToolbarIcon;
			this.nextItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.nextItemToolStripButton.Name = "nextItemToolStripButton";
			this.nextItemToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.nextItemToolStripButton.Text = "toolStripButton3";
			this.nextItemToolStripButton.ToolTipText = "Next";
			this.nextItemToolStripButton.Click += new System.EventHandler(this.nextItemToolStripButton_Click);
			// 
			// lastItemToolStripButton
			// 
			this.lastItemToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.lastItemToolStripButton.Image = global::WoWSpellViewer.Properties.Resources.LastItemToolbarIcon;
			this.lastItemToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.lastItemToolStripButton.Name = "lastItemToolStripButton";
			this.lastItemToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.lastItemToolStripButton.Text = "toolStripButton4";
			this.lastItemToolStripButton.ToolTipText = "Last";
			this.lastItemToolStripButton.Click += new System.EventHandler(this.lastItemToolStripButton_Click);
			// 
			// statusStrip
			// 
			this.statusStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.statusStrip.Location = new System.Drawing.Point(0, 25);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new System.Drawing.Size(284, 22);
			this.statusStrip.TabIndex = 0;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.Controls.Add(this.spellEffect3Label, 1, 5);
			this.tableLayoutPanel.Controls.Add(this.spellEffect2Label, 1, 4);
			this.tableLayoutPanel.Controls.Add(this.spellEffect1Label, 1, 3);
			this.tableLayoutPanel.Controls.Add(label1, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.spellIdLabel, 1, 0);
			this.tableLayoutPanel.Controls.Add(label2, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.spellLevelLabel, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.manaCostLabel, 1, 2);
			this.tableLayoutPanel.Controls.Add(label4, 0, 3);
			this.tableLayoutPanel.Controls.Add(label5, 0, 4);
			this.tableLayoutPanel.Controls.Add(label3, 0, 2);
			this.tableLayoutPanel.Controls.Add(label6, 0, 5);
			this.tableLayoutPanel.Location = new System.Drawing.Point(3, 106);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 6;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.Size = new System.Drawing.Size(278, 85);
			this.tableLayoutPanel.TabIndex = 4;
			// 
			// spellEffect3Label
			// 
			this.spellEffect3Label.AutoSize = true;
			this.spellEffect3Label.Location = new System.Drawing.Point(119, 71);
			this.spellEffect3Label.Name = "spellEffect3Label";
			this.spellEffect3Label.Size = new System.Drawing.Size(0, 13);
			this.spellEffect3Label.TabIndex = 11;
			// 
			// spellEffect2Label
			// 
			this.spellEffect2Label.AutoSize = true;
			this.spellEffect2Label.Location = new System.Drawing.Point(119, 57);
			this.spellEffect2Label.Name = "spellEffect2Label";
			this.spellEffect2Label.Size = new System.Drawing.Size(0, 13);
			this.spellEffect2Label.TabIndex = 10;
			// 
			// spellEffect1Label
			// 
			this.spellEffect1Label.AutoSize = true;
			this.spellEffect1Label.Location = new System.Drawing.Point(119, 43);
			this.spellEffect1Label.Name = "spellEffect1Label";
			this.spellEffect1Label.Size = new System.Drawing.Size(0, 13);
			this.spellEffect1Label.TabIndex = 9;
			// 
			// spellIdLabel
			// 
			this.spellIdLabel.AutoSize = true;
			this.spellIdLabel.Location = new System.Drawing.Point(119, 1);
			this.spellIdLabel.Name = "spellIdLabel";
			this.spellIdLabel.Size = new System.Drawing.Size(0, 13);
			this.spellIdLabel.TabIndex = 1;
			// 
			// spellLevelLabel
			// 
			this.spellLevelLabel.AutoSize = true;
			this.spellLevelLabel.Location = new System.Drawing.Point(119, 15);
			this.spellLevelLabel.Name = "spellLevelLabel";
			this.spellLevelLabel.Size = new System.Drawing.Size(0, 13);
			this.spellLevelLabel.TabIndex = 3;
			// 
			// manaCostLabel
			// 
			this.manaCostLabel.AutoSize = true;
			this.manaCostLabel.Location = new System.Drawing.Point(119, 29);
			this.manaCostLabel.Name = "manaCostLabel";
			this.manaCostLabel.Size = new System.Drawing.Size(0, 13);
			this.manaCostLabel.TabIndex = 5;
			// 
			// menuStrip
			// 
			this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(284, 24);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 264);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip;
			this.Name = "MainForm";
			this.Text = "Spell Viewer";
			((System.ComponentModel.ISupportInitialize)(this.spellIconPictureBox)).EndInit();
			this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.PerformLayout();
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.navigationToolStrip.ResumeLayout(false);
			this.navigationToolStrip.PerformLayout();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.Label spellNameLabel;
		private System.Windows.Forms.StatusStrip statusStrip;
		private System.Windows.Forms.ToolStrip navigationToolStrip;
		private System.Windows.Forms.ToolStripButton firstItemToolStripButton;
		private System.Windows.Forms.ToolStripButton previousItemToolStripButton;
		private System.Windows.Forms.ToolStripButton nextItemToolStripButton;
		private System.Windows.Forms.ToolStripButton lastItemToolStripButton;
		private System.Windows.Forms.ToolStripTextBox indexToolStripTextBox;
		private System.Windows.Forms.ToolStripLabel countToolStripLabel;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.PictureBox spellIconPictureBox;
		private System.Windows.Forms.Label spellDescriptionLabel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label spellIdLabel;
		private System.Windows.Forms.Label spellLevelLabel;
		private System.Windows.Forms.Label manaCostLabel;
		private System.Windows.Forms.Label spellEffect3Label;
		private System.Windows.Forms.Label spellEffect2Label;
		private System.Windows.Forms.Label spellEffect1Label;
	}
}

