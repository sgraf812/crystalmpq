using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace CrystalMpq.Explorer
{
	internal delegate void ExtractionProcedure(ExtractionProgressionDialog progresionDialog, object state);

	internal sealed class ExtractionProgressionDialog
	{
		private ExtractionProgressionForm form;

		public ExtractionProgressionDialog() { form = new ExtractionProgressionForm(this); }

		public ulong TotalSize
		{
			get { return form.TotalSize; }
			set { form.TotalSize = value; }
		}

		public ulong ProcessedSize
		{
			get { return form.ProcessedSize; }
			set { form.ProcessedSize = value; }
		}

		public int TotalFileCount
		{
			get { return form.TotalFileCount; }
			set { form.TotalFileCount = value; }
		}

		public int ProcessedFileCount
		{
			get { return form.ProcessedFileCount; }
			set { form.ProcessedFileCount = value; }
		}

		public string CurrentFileName
		{
			get { return form.CurrentFileName; }
			set { form.CurrentFileName = value; }
		}

		public bool Cancelled { get { return form.Cancelled; } }

		public void UpdateFileInformation(int index, string filename) { form.UpdateFileInformation(index, filename); }

		public void ErrorDialog(string message)
		{
			form.ErrorDialog(message);
		}

		public DialogResult AskForOverwrite(string file, string directory, ulong newSize)
		{
			return form.AskForOverwrite(file, directory, newSize);
		}

		public DialogResult ShowDialog(IWin32Window owner, ExtractionProcedure extractionProcedure, object state = null)
		{
			return form.ShowDialog(owner, extractionProcedure, state);
		}
	}
}
