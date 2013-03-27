using System;
using System.Threading;
using System.Windows.Forms;

namespace CrystalMpq.Explorer
{
	internal sealed partial class ExtractionProgressionForm : Form
    {
#if NET_FX_2
        private delegate void Action();
        private delegate void Action<T>(T t);
        private delegate R Func<T, U, V, R>(T t, U u, V v);
#endif

		[Flags]
		private enum DelayedUpdates
		{
			None = 0,
			Status = 1,
			FileInformation = 2
		}

		private ExtractionProgressionDialog dialog;
		private ExtractionProcedure extractionProcedure;
		private object stateObject;
		private ulong totalSize;
		private ulong processedSize;
		private ulong sizeUnit;
		private volatile int totalFileCount;
		private volatile int processedFileCount;
		private volatile string currentFileName;
		private volatile bool cancelled;
		private DelayedUpdates delayedUpdates;
		private object syncRoot = new object();

		internal ExtractionProgressionForm(ExtractionProgressionDialog dialog)
		{
			this.dialog = dialog;
			this.InitializeComponent();
		}

		public ulong TotalSize
		{
			get { return this.totalSize; }
			set
			{
				if (value < 1) throw new ArgumentOutOfRangeException("value");

				lock (syncRoot)
				{
					this.totalSize = value;
					this.sizeUnit = this.totalSize >= 0x80000000UL ? 2 * (this.totalSize / 0x80000000U) : 1;
				}

				RefreshStatus();
			}
		}

		public ulong ProcessedSize
		{
			get { return this.processedSize; }
			set
			{
				if (value < 0 || value > this.totalSize) throw new ArgumentOutOfRangeException("value");

				lock (syncRoot)
					this.processedSize = value;

				RefreshStatus();
			}
		}

		public int TotalFileCount
		{
			get { return this.totalFileCount; }
			set
			{
				if (value < 0) throw new ArgumentOutOfRangeException("value");

				lock (syncRoot)
				{
					if (this.processedFileCount > (this.totalFileCount = value))
						this.processedFileCount = 0;
				}

				RefreshFileInformation();
			}
		}

		public int ProcessedFileCount
		{
			get { return this.processedFileCount; }
			set
			{
				if (value < 0 || value > this.totalFileCount) throw new ArgumentOutOfRangeException("value");

				lock (syncRoot)
					this.processedFileCount = value;

				RefreshFileInformation();
			}
		}

		public string CurrentFileName
		{
			get { return this.currentFileName; }
			set
			{
				lock (syncRoot)
					this.currentFileName = value;

				RefreshFileInformation();
			}
		}

		public bool Cancelled { get { return cancelled; } }

		public void UpdateFileInformation(int index, string filename)
		{
			if (index < 0 || index > this.totalFileCount) throw new ArgumentOutOfRangeException("value");

			this.processedFileCount = index;
			this.currentFileName = filename;

			RefreshFileInformation();
		}

		private void RefreshStatus()
		{
			if (InvokeRequired) Invoke((Action)RefreshStatus);
			else if (!Visible)
			{
				delayedUpdates |= DelayedUpdates.Status;
				return;
			}
			else lock (syncRoot)
			{
				statusLabel.Text = string.Format(Properties.Resources.Culture, Properties.Resources.ExtractionStatusMessage, Program.FormatFileSize(processedSize), Program.FormatFileSize(totalSize));
				this.progressBar.Maximum = (int)(this.totalSize / this.sizeUnit);
				this.progressBar.Value = (int)(this.processedSize / this.sizeUnit);
			}
		}

		private void RefreshFileInformation()
		{
			if (InvokeRequired) Invoke((Action)RefreshFileInformation);
			else if (!Visible)
			{
				delayedUpdates |= DelayedUpdates.FileInformation;
				return;
			}
			else lock (syncRoot)
				fileInformationLabel.Text = string.Format(Properties.Resources.Culture, Properties.Resources.ExtractionFileMessage, processedFileCount, totalFileCount, currentFileName);
		}

		internal DialogResult ShowDialog(IWin32Window owner, ExtractionProcedure extractionProcedure, object state)
		{
			if (extractionProcedure == null) throw new ArgumentNullException();

			this.extractionProcedure = extractionProcedure;
			this.stateObject = state;
			this.cancelled = false;
			this.abortButton.Enabled = true;

			return ShowDialog(owner);
		}

		internal void ErrorDialog(string message)
		{
			if (InvokeRequired) Invoke((Action<string>)ErrorDialog, message);
			else
			{
				if (!Visible) throw new InvalidOperationException();
				MessageBox.Show(this, message, Properties.Resources.ErrorDialogTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				cancelled = true;
			}
		}

		internal DialogResult AskForOverwrite(string file, string directory, ulong newSize)
		{
			if (InvokeRequired) return (DialogResult)Invoke((Func<string, string, ulong, DialogResult>)AskForOverwrite, file, directory, newSize);

			return MessageBox.Show(this, string.Format(Properties.Resources.Culture, Properties.Resources.OverwriteFileDialogMessage, file, directory), Properties.Resources.OverwriteFileDialogTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			if (Visible)
			{
				if ((delayedUpdates & DelayedUpdates.Status) != DelayedUpdates.None) RefreshStatus();
				if ((delayedUpdates & DelayedUpdates.FileInformation) != DelayedUpdates.None) RefreshFileInformation();
				delayedUpdates = DelayedUpdates.None;

				ThreadPool.QueueUserWorkItem(state =>
				{
					var @this = state as ExtractionProgressionForm;

					@this.extractionProcedure(@this.dialog, @this.stateObject);

					if (@this.Visible)
						@this.Invoke
						(
							(Action)(() =>
							{
								@this.abortButton.Enabled = false;
								@this.DialogResult = DialogResult.OK;
								@this.Hide();
							})
						);
				}, this);
			}
			base.OnVisibleChanged(e);
		}

		private void abortButton_Click(object sender, EventArgs e) { cancelled = true; }
	}
}
