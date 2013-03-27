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
	partial class NodePropertiesForm
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
			System.Windows.Forms.GroupBox compressionGroupBox;
			System.Windows.Forms.TableLayoutPanel compressionTableLayoutPanel;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodePropertiesForm));
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.GroupBox detailsGroupBox;
			System.Windows.Forms.TableLayoutPanel flagsTableLayoutPanel;
			this.multiCompressedRadioButton = new System.Windows.Forms.RadioButton();
			this.notCompressedRadioButton = new System.Windows.Forms.RadioButton();
			this.dclCompressedRadioButton = new System.Windows.Forms.RadioButton();
			this.singleUnitCheckBox = new System.Windows.Forms.CheckBox();
			this.compressedSizeLabel = new System.Windows.Forms.Label();
			this.expandedSizeLabel = new System.Windows.Forms.Label();
			this.encryptedCheckBox = new System.Windows.Forms.CheckBox();
			this.adjustedKeyCheckBox = new System.Windows.Forms.CheckBox();
			this.iconPictureBox = new System.Windows.Forms.PictureBox();
			this.okButton = new System.Windows.Forms.Button();
			this.fileNameLabel = new System.Windows.Forms.Label();
			this.patchCheckBox = new System.Windows.Forms.CheckBox();
			compressionGroupBox = new System.Windows.Forms.GroupBox();
			compressionTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			detailsGroupBox = new System.Windows.Forms.GroupBox();
			flagsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			compressionGroupBox.SuspendLayout();
			compressionTableLayoutPanel.SuspendLayout();
			detailsGroupBox.SuspendLayout();
			flagsTableLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// compressionGroupBox
			// 
			compressionGroupBox.Controls.Add(compressionTableLayoutPanel);
			resources.ApplyResources(compressionGroupBox, "compressionGroupBox");
			compressionGroupBox.Name = "compressionGroupBox";
			compressionGroupBox.TabStop = false;
			// 
			// compressionTableLayoutPanel
			// 
			resources.ApplyResources(compressionTableLayoutPanel, "compressionTableLayoutPanel");
			compressionTableLayoutPanel.Controls.Add(this.multiCompressedRadioButton, 2, 0);
			compressionTableLayoutPanel.Controls.Add(this.notCompressedRadioButton, 0, 0);
			compressionTableLayoutPanel.Controls.Add(this.dclCompressedRadioButton, 1, 0);
			compressionTableLayoutPanel.Name = "compressionTableLayoutPanel";
			// 
			// multiCompressedRadioButton
			// 
			this.multiCompressedRadioButton.AutoCheck = false;
			resources.ApplyResources(this.multiCompressedRadioButton, "multiCompressedRadioButton");
			this.multiCompressedRadioButton.Name = "multiCompressedRadioButton";
			this.multiCompressedRadioButton.TabStop = true;
			this.multiCompressedRadioButton.UseVisualStyleBackColor = true;
			// 
			// notCompressedRadioButton
			// 
			this.notCompressedRadioButton.AutoCheck = false;
			resources.ApplyResources(this.notCompressedRadioButton, "notCompressedRadioButton");
			this.notCompressedRadioButton.Name = "notCompressedRadioButton";
			this.notCompressedRadioButton.TabStop = true;
			this.notCompressedRadioButton.UseVisualStyleBackColor = true;
			// 
			// dclCompressedRadioButton
			// 
			this.dclCompressedRadioButton.AutoCheck = false;
			resources.ApplyResources(this.dclCompressedRadioButton, "dclCompressedRadioButton");
			this.dclCompressedRadioButton.Name = "dclCompressedRadioButton";
			this.dclCompressedRadioButton.TabStop = true;
			this.dclCompressedRadioButton.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			label1.Name = "label1";
			// 
			// label2
			// 
			resources.ApplyResources(label2, "label2");
			label2.Name = "label2";
			// 
			// detailsGroupBox
			// 
			detailsGroupBox.Controls.Add(flagsTableLayoutPanel);
			resources.ApplyResources(detailsGroupBox, "detailsGroupBox");
			detailsGroupBox.Name = "detailsGroupBox";
			detailsGroupBox.TabStop = false;
			// 
			// flagsTableLayoutPanel
			// 
			resources.ApplyResources(flagsTableLayoutPanel, "flagsTableLayoutPanel");
			flagsTableLayoutPanel.Controls.Add(this.singleUnitCheckBox, 0, 3);
			flagsTableLayoutPanel.Controls.Add(this.compressedSizeLabel, 1, 1);
			flagsTableLayoutPanel.Controls.Add(this.expandedSizeLabel, 1, 0);
			flagsTableLayoutPanel.Controls.Add(this.encryptedCheckBox, 0, 2);
			flagsTableLayoutPanel.Controls.Add(this.adjustedKeyCheckBox, 1, 2);
			flagsTableLayoutPanel.Controls.Add(label1, 0, 0);
			flagsTableLayoutPanel.Controls.Add(label2, 0, 1);
			flagsTableLayoutPanel.Controls.Add(this.patchCheckBox, 1, 3);
			flagsTableLayoutPanel.Name = "flagsTableLayoutPanel";
			// 
			// singleUnitCheckBox
			// 
			this.singleUnitCheckBox.AutoCheck = false;
			resources.ApplyResources(this.singleUnitCheckBox, "singleUnitCheckBox");
			this.singleUnitCheckBox.Name = "singleUnitCheckBox";
			this.singleUnitCheckBox.UseVisualStyleBackColor = true;
			// 
			// compressedSizeLabel
			// 
			resources.ApplyResources(this.compressedSizeLabel, "compressedSizeLabel");
			this.compressedSizeLabel.Name = "compressedSizeLabel";
			// 
			// expandedSizeLabel
			// 
			resources.ApplyResources(this.expandedSizeLabel, "expandedSizeLabel");
			this.expandedSizeLabel.Name = "expandedSizeLabel";
			// 
			// encryptedCheckBox
			// 
			this.encryptedCheckBox.AutoCheck = false;
			resources.ApplyResources(this.encryptedCheckBox, "encryptedCheckBox");
			this.encryptedCheckBox.Name = "encryptedCheckBox";
			this.encryptedCheckBox.UseVisualStyleBackColor = true;
			// 
			// adjustedKeyCheckBox
			// 
			this.adjustedKeyCheckBox.AutoCheck = false;
			resources.ApplyResources(this.adjustedKeyCheckBox, "adjustedKeyCheckBox");
			this.adjustedKeyCheckBox.Name = "adjustedKeyCheckBox";
			this.adjustedKeyCheckBox.UseVisualStyleBackColor = true;
			// 
			// iconPictureBox
			// 
			resources.ApplyResources(this.iconPictureBox, "iconPictureBox");
			this.iconPictureBox.Name = "iconPictureBox";
			this.iconPictureBox.TabStop = false;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// fileNameLabel
			// 
			this.fileNameLabel.AutoEllipsis = true;
			resources.ApplyResources(this.fileNameLabel, "fileNameLabel");
			this.fileNameLabel.Name = "fileNameLabel";
			// 
			// patchCheckBox
			// 
			this.patchCheckBox.AutoCheck = false;
			resources.ApplyResources(this.patchCheckBox, "patchCheckBox");
			this.patchCheckBox.Name = "patchCheckBox";
			this.patchCheckBox.UseVisualStyleBackColor = true;
			// 
			// NodePropertiesForm
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(detailsGroupBox);
			this.Controls.Add(this.fileNameLabel);
			this.Controls.Add(compressionGroupBox);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.iconPictureBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NodePropertiesForm";
			this.ShowInTaskbar = false;
			compressionGroupBox.ResumeLayout(false);
			compressionGroupBox.PerformLayout();
			compressionTableLayoutPanel.ResumeLayout(false);
			compressionTableLayoutPanel.PerformLayout();
			detailsGroupBox.ResumeLayout(false);
			detailsGroupBox.PerformLayout();
			flagsTableLayoutPanel.ResumeLayout(false);
			flagsTableLayoutPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox iconPictureBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.RadioButton multiCompressedRadioButton;
		private System.Windows.Forms.RadioButton dclCompressedRadioButton;
		private System.Windows.Forms.RadioButton notCompressedRadioButton;
		private System.Windows.Forms.Label fileNameLabel;
		private System.Windows.Forms.Label expandedSizeLabel;
		private System.Windows.Forms.Label compressedSizeLabel;
		private System.Windows.Forms.CheckBox adjustedKeyCheckBox;
		private System.Windows.Forms.CheckBox encryptedCheckBox;
		private System.Windows.Forms.CheckBox singleUnitCheckBox;
		private System.Windows.Forms.CheckBox patchCheckBox;

	}
}