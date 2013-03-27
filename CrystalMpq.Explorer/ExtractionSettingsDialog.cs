using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CrystalMpq.Explorer
{
	internal sealed class ExtractionSettingsDialog
	{
		private ExtractionSettingsForm form = new ExtractionSettingsForm();

		public string DestinationDirectory { get; set; }
		public bool OverwriteFiles { get; set; }
		public bool Recurse { get; set; }
		public bool AllowRecurse { get; set; }

		private void PrepareForm()
		{
			if (form.Visible) throw new InvalidOperationException();

			form.DestinationDirectory = DestinationDirectory;
			form.OverwriteFiles = OverwriteFiles;
			form.Recurse = Recurse;
			form.AllowRecurse = AllowRecurse;
		}

		private void RetrieveValues()
		{
			DestinationDirectory = form.DestinationDirectory;
			OverwriteFiles = form.OverwriteFiles;
			Recurse = form.Recurse;
			AllowRecurse = form.AllowRecurse;
		}

		public DialogResult ShowDialog()
		{
			PrepareForm();

			var dialogResult = form.ShowDialog();

			if (dialogResult == DialogResult.OK) RetrieveValues();

			return dialogResult;
		}

		public DialogResult ShowDialog(IWin32Window owner)
		{
			PrepareForm();

			var dialogResult = form.ShowDialog(owner);

			if (dialogResult == DialogResult.OK) RetrieveValues();

			return dialogResult;
		}
	}
}
