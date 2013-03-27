#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

namespace CrystalMpq.WoW
{
	partial class LanguagePackPickerForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguagePackPickerForm));
			this.informationLabel = new System.Windows.Forms.Label();
			this.languageComboBox = new System.Windows.Forms.ComboBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// informationLabel
			// 
			this.informationLabel.AccessibleDescription = null;
			this.informationLabel.AccessibleName = null;
			resources.ApplyResources(this.informationLabel, "informationLabel");
			this.informationLabel.Font = null;
			this.informationLabel.Name = "informationLabel";
			// 
			// languageComboBox
			// 
			this.languageComboBox.AccessibleDescription = null;
			this.languageComboBox.AccessibleName = null;
			resources.ApplyResources(this.languageComboBox, "languageComboBox");
			this.languageComboBox.BackgroundImage = null;
			this.languageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.languageComboBox.Font = null;
			this.languageComboBox.Name = "languageComboBox";
			this.languageComboBox.Sorted = true;
			// 
			// okButton
			// 
			this.okButton.AccessibleDescription = null;
			this.okButton.AccessibleName = null;
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.BackgroundImage = null;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Font = null;
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.AccessibleDescription = null;
			this.cancelButton.AccessibleName = null;
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.BackgroundImage = null;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Font = null;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// LanguagePackPickerForm
			// 
			this.AcceptButton = this.okButton;
			this.AccessibleDescription = null;
			this.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = null;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.languageComboBox);
			this.Controls.Add(this.informationLabel);
			this.Font = null;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = null;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LanguagePackPickerForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label informationLabel;
		private System.Windows.Forms.ComboBox languageComboBox;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}