#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

namespace CrystalMpq.Explorer.AudioVideo
{
	partial class MediaPlayer
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.playPauseToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.stopToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.trackBar = new System.Windows.Forms.TrackBar();
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.renderPanel = new System.Windows.Forms.Panel();
			this.timer = new System.Windows.Forms.Timer(this.components);
			tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel.SuspendLayout();
			this.toolStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel
			// 
			tableLayoutPanel.ColumnCount = 1;
			tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel.Controls.Add(this.toolStrip, 0, 3);
			tableLayoutPanel.Controls.Add(this.trackBar, 0, 2);
			tableLayoutPanel.Controls.Add(this.fileNameLabel, 0, 1);
			tableLayoutPanel.Controls.Add(this.renderPanel, 0, 0);
			tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel.Name = "tableLayoutPanel";
			tableLayoutPanel.RowCount = 4;
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel.Size = new System.Drawing.Size(150, 150);
			tableLayoutPanel.TabIndex = 0;
			// 
			// toolStrip
			// 
			this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playPauseToolStripButton,
            this.stopToolStripButton});
			this.toolStrip.Location = new System.Drawing.Point(0, 125);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(150, 25);
			this.toolStrip.TabIndex = 0;
			this.toolStrip.Text = "Player controls";
			// 
			// playPauseToolStripButton
			// 
			this.playPauseToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.playPauseToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.playPauseToolStripButton.Name = "playPauseToolStripButton";
			this.playPauseToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.playPauseToolStripButton.Text = "Play/ Pause";
			this.playPauseToolStripButton.Click += new System.EventHandler(this.playPauseToolStripButton_Click);
			// 
			// stopToolStripButton
			// 
			this.stopToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.stopToolStripButton.Image = global::CrystalMpq.Explorer.AudioVideo.Properties.Resources.StopIcon;
			this.stopToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.stopToolStripButton.Name = "stopToolStripButton";
			this.stopToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.stopToolStripButton.Text = "Stop";
			this.stopToolStripButton.Click += new System.EventHandler(this.stopToolStripButton_Click);
			// 
			// trackBar
			// 
			this.trackBar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trackBar.LargeChange = 10;
			this.trackBar.Location = new System.Drawing.Point(3, 77);
			this.trackBar.Name = "trackBar";
			this.trackBar.Size = new System.Drawing.Size(144, 45);
			this.trackBar.SmallChange = 5;
			this.trackBar.TabIndex = 1;
			this.trackBar.TickFrequency = 100;
			this.trackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.trackBar.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
			// 
			// fileNameLabel
			// 
			this.fileNameLabel.AutoSize = true;
			this.fileNameLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.fileNameLabel.Location = new System.Drawing.Point(3, 61);
			this.fileNameLabel.Name = "fileNameLabel";
			this.fileNameLabel.Size = new System.Drawing.Size(144, 13);
			this.fileNameLabel.TabIndex = 2;
			this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			// 
			// renderPanel
			// 
			this.renderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.renderPanel.Location = new System.Drawing.Point(3, 3);
			this.renderPanel.Name = "renderPanel";
			this.renderPanel.Size = new System.Drawing.Size(144, 55);
			this.renderPanel.TabIndex = 3;
			// 
			// timer
			// 
			this.timer.Interval = 10;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// Player
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(tableLayoutPanel);
			this.Name = "Player";
			tableLayoutPanel.ResumeLayout(false);
			tableLayoutPanel.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.TrackBar trackBar;
		private System.Windows.Forms.Label fileNameLabel;
		private System.Windows.Forms.ToolStripButton playPauseToolStripButton;
		private System.Windows.Forms.ToolStripButton stopToolStripButton;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Panel renderPanel;


	}
}
