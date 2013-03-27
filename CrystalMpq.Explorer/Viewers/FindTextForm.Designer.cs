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
	partial class FindTextForm
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
			this.findTextLabel = new System.Windows.Forms.Label();
			this.textBox = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.optionsGroupBox = new System.Windows.Forms.GroupBox();
			this.entireWordCheckBox = new System.Windows.Forms.CheckBox();
			this.caseSensitiveCheckBox = new System.Windows.Forms.CheckBox();
			this.optionsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// findTextLabel
			// 
			this.findTextLabel.AutoSize = true;
			this.findTextLabel.Location = new System.Drawing.Point(12, 15);
			this.findTextLabel.Name = "findTextLabel";
			this.findTextLabel.Size = new System.Drawing.Size(66, 13);
			this.findTextLabel.TabIndex = 0;
			this.findTextLabel.Text = "&Text to find: ";
			// 
			// textBox
			// 
			this.textBox.Location = new System.Drawing.Point(84, 12);
			this.textBox.Name = "textBox";
			this.textBox.Size = new System.Drawing.Size(196, 20);
			this.textBox.TabIndex = 1;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(123, 109);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(204, 109);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 4;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// optionsGroupBox
			// 
			this.optionsGroupBox.Controls.Add(this.entireWordCheckBox);
			this.optionsGroupBox.Controls.Add(this.caseSensitiveCheckBox);
			this.optionsGroupBox.Location = new System.Drawing.Point(12, 38);
			this.optionsGroupBox.Name = "optionsGroupBox";
			this.optionsGroupBox.Size = new System.Drawing.Size(268, 65);
			this.optionsGroupBox.TabIndex = 2;
			this.optionsGroupBox.TabStop = false;
			this.optionsGroupBox.Text = "O&ptions";
			// 
			// entireWordCheckBox
			// 
			this.entireWordCheckBox.AutoSize = true;
			this.entireWordCheckBox.Location = new System.Drawing.Point(6, 42);
			this.entireWordCheckBox.Name = "entireWordCheckBox";
			this.entireWordCheckBox.Size = new System.Drawing.Size(79, 17);
			this.entireWordCheckBox.TabIndex = 1;
			this.entireWordCheckBox.Text = "Entire &word";
			this.entireWordCheckBox.UseVisualStyleBackColor = true;
			// 
			// caseSensitiveCheckBox
			// 
			this.caseSensitiveCheckBox.AutoSize = true;
			this.caseSensitiveCheckBox.Location = new System.Drawing.Point(6, 19);
			this.caseSensitiveCheckBox.Name = "caseSensitiveCheckBox";
			this.caseSensitiveCheckBox.Size = new System.Drawing.Size(94, 17);
			this.caseSensitiveCheckBox.TabIndex = 0;
			this.caseSensitiveCheckBox.Text = "C&ase sensitive";
			this.caseSensitiveCheckBox.UseVisualStyleBackColor = true;
			// 
			// FindTextForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(292, 144);
			this.Controls.Add(this.optionsGroupBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.textBox);
			this.Controls.Add(this.findTextLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FindTextForm";
			this.Text = "Find...";
			this.optionsGroupBox.ResumeLayout(false);
			this.optionsGroupBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label findTextLabel;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.GroupBox optionsGroupBox;
		private System.Windows.Forms.CheckBox entireWordCheckBox;
		private System.Windows.Forms.CheckBox caseSensitiveCheckBox;
		private System.Windows.Forms.TextBox textBox;
	}
}