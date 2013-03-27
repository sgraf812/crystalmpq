namespace CrystalMpq.Explorer
{
	partial class ExtractionProgressionForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractionProgressionForm));
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.statusLabel = new System.Windows.Forms.Label();
			this.abortButton = new System.Windows.Forms.Button();
			this.fileInformationLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// progressBar
			// 
			resources.ApplyResources(this.progressBar, "progressBar");
			this.progressBar.Name = "progressBar";
			// 
			// statusLabel
			// 
			resources.ApplyResources(this.statusLabel, "statusLabel");
			this.statusLabel.Name = "statusLabel";
			// 
			// abortButton
			// 
			resources.ApplyResources(this.abortButton, "abortButton");
			this.abortButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.abortButton.Name = "abortButton";
			this.abortButton.UseVisualStyleBackColor = true;
			this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
			// 
			// fileInformationLabel
			// 
			resources.ApplyResources(this.fileInformationLabel, "fileInformationLabel");
			this.fileInformationLabel.Name = "fileInformationLabel";
			// 
			// ExtractionProgressionForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.abortButton;
			this.ControlBox = false;
			this.Controls.Add(this.fileInformationLabel);
			this.Controls.Add(this.abortButton);
			this.Controls.Add(this.statusLabel);
			this.Controls.Add(this.progressBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ExtractionProgressionForm";
			this.ShowInTaskbar = false;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.Label statusLabel;
		private System.Windows.Forms.Button abortButton;
		private System.Windows.Forms.Label fileInformationLabel;
	}
}