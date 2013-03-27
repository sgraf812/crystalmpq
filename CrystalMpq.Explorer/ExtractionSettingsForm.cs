using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CrystalMpq.Explorer
{
	internal sealed partial class ExtractionSettingsForm : Form
	{
		public ExtractionSettingsForm() { InitializeComponent(); }

		public string DestinationDirectory
		{
			get { return destinationTextBox.Text; }
			set { destinationTextBox.Text = value; }
		}

		public bool OverwriteFiles
		{
			get { return overwriteCheckBox.Checked; }
			set { overwriteCheckBox.Checked = value; }
		}

		public bool Recurse
		{
			get { return recurseCheckBox.Checked; }
			set { recurseCheckBox.Checked = value; }
		}

		public bool AllowRecurse
		{
			get { return recurseCheckBox.Enabled; }
			set { recurseCheckBox.Enabled = value; }
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			folderBrowserDialog.SelectedPath = destinationTextBox.Text;
			if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
				destinationTextBox.Text = folderBrowserDialog.SelectedPath;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			string path = destinationTextBox.Text;

			// Check for null or empty by ourselves, as this is cheap to do.
			if (string.IsNullOrEmpty(path))
			{
				DialogResult = DialogResult.None;
				MessageBox.Show(this, Properties.Resources.InvalidDirectoryNullErrorDialogMessage, Properties.Resources.InvalidDirectoryErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			// Otherwise, let the BCL and the system do their verifications
			try
			{
				var directory = new DirectoryInfo(path);

				// If directory seems valid (no exception thrown), check for existence, and propose to create it if necessary
				if (!directory.Exists)
					if (MessageBox.Show(this, string.Format(Properties.Resources.Culture, Properties.Resources.DirectoryCreationDialogMessage, path), Properties.Resources.DirectoryCreationDialogTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
						try { directory.Create(); }
						catch (IOException)
						{
							DialogResult = DialogResult.None;
							MessageBox.Show(this, string.Format(Properties.Resources.Culture, Properties.Resources.DirectoryCreationErrorDialogMessage, path), Properties.Resources.DirectoryCreationErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					else DialogResult = DialogResult.None;
			}
			catch (ArgumentException)
			{
				DialogResult = DialogResult.None;
				MessageBox.Show(this, Properties.Resources.InvalidDirectoryNameErrorDialogMessage, Properties.Resources.InvalidDirectoryErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			catch (PathTooLongException)
			{
				DialogResult = DialogResult.None;
				MessageBox.Show(this, Properties.Resources.InvalidDirectoryPathTooLongErrorDialogMessage, Properties.Resources.InvalidDirectoryErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
