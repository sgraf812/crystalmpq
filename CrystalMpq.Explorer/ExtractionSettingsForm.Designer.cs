namespace CrystalMpq.Explorer
{
	partial class ExtractionSettingsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.Label destinationLabel;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractionSettingsForm));
			this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.destinationTextBox = new System.Windows.Forms.TextBox();
			this.overwriteCheckBox = new System.Windows.Forms.CheckBox();
			this.recurseCheckBox = new System.Windows.Forms.CheckBox();
			this.browseButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			destinationLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// destinationLabel
			// 
			resources.ApplyResources(destinationLabel, "destinationLabel");
			destinationLabel.Name = "destinationLabel";
			// 
			// destinationTextBox
			// 
			resources.ApplyResources(this.destinationTextBox, "destinationTextBox");
			this.destinationTextBox.Name = "destinationTextBox";
			// 
			// overwriteCheckBox
			// 
			resources.ApplyResources(this.overwriteCheckBox, "overwriteCheckBox");
			this.overwriteCheckBox.Name = "overwriteCheckBox";
			this.overwriteCheckBox.UseVisualStyleBackColor = true;
			// 
			// recurseCheckBox
			// 
			resources.ApplyResources(this.recurseCheckBox, "recurseCheckBox");
			this.recurseCheckBox.Name = "recurseCheckBox";
			this.recurseCheckBox.UseVisualStyleBackColor = true;
			// 
			// browseButton
			// 
			resources.ApplyResources(this.browseButton, "browseButton");
			this.browseButton.Name = "browseButton";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// okButton
			// 
			resources.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Name = "okButton";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// cancelButton
			// 
			resources.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// ExtractionSettingsForm
			// 
			this.AcceptButton = this.okButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.browseButton);
			this.Controls.Add(this.recurseCheckBox);
			this.Controls.Add(this.overwriteCheckBox);
			this.Controls.Add(destinationLabel);
			this.Controls.Add(this.destinationTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExtractionSettingsForm";
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
		private System.Windows.Forms.TextBox destinationTextBox;
		private System.Windows.Forms.CheckBox overwriteCheckBox;
		private System.Windows.Forms.CheckBox recurseCheckBox;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
	}
}