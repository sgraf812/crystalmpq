#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Stream = System.IO.Stream;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer.Viewers
{
	internal sealed partial class TextViewer : FileViewer
	{
		private byte[] data;
		private Encoding encoding;
		private FindTextForm findTextForm;
		private string searchText;
		private bool caseSensitive, entireWord;

		public TextViewer(IHost host)
			: base(host)
		{
			EncodingInfo[] encodingInfoArray;

			InitializeComponent();
			encodingInfoArray = Encoding.GetEncodings();
			for (int i = 0; i < encodingInfoArray.Length; i++)
				encodingToolStripComboBox.Items.Add(string.Intern(encodingInfoArray[i].Name));

			// Sets the default encoding according to the settings
			ResetEncoding();
		}

		public override MenuStrip Menu { get { return menuStrip; } }
		public override ToolStrip MainToolStrip { get { return mainToolStrip; } }
		public override StatusStrip StatusStrip { get { return statusStrip; } }
		protected override IPluginSettings CreatePluginSettings() { return new TextViewerSettings(); }

		/// <summary>
		/// Gets the FindTextForm object associated with the text viewer
		/// </summary>
		private FindTextForm FindTextForm
		{
			get
			{
				// Create the object if it isn't already done
				if (findTextForm == null)
					findTextForm = new FindTextForm();
				return findTextForm;
			}
		}

		public byte[] Data
		{
			get
			{
				return data;
			}
			set
			{
				if (value != data)
				{
					data = (byte[])value;
					UpdateText(encoding);
				}
			}
		}

		protected override void OnFileChanged()
		{
			if (File == null)
			{
				Data = null;
				return;
			}
			else
			{
				Stream stream;

				stream = File.Open();
				try
				{
					byte[] buffer;
					int length;

					length = (int)stream.Length;
					buffer = new byte[length];
					if (stream.Read(buffer, 0, length) != length)
						throw new Exception();
					Data = buffer;
					ResetEncoding();
				}
				finally { stream.Close(); }
			}
		}

		private void ResetEncoding()
		{
			try { encodingToolStripComboBox.SelectedItem = CrystalMpq.Explorer.Properties.Settings.Default.TextViewerDefaultEncoding; }
			catch { encodingToolStripComboBox.SelectedItem = "utf-8"; }
		}

		private void UpdateText(Encoding encoding)
		{
			textBox.Text = encoding.GetString(data);
		}

		private void FindError(string text)
		{
			searchStatusToolStripStatusLabel.Text = text;
			searchStatusToolStripStatusLabel.Visible = !string.IsNullOrEmpty(text);
		}

		private void Find()
		{
			StringComparison comparisonType;
			int startPos, pos;

			FindError(null);

			startPos = textBox.SelectionStart;

			if (caseSensitive)
				comparisonType = StringComparison.CurrentCulture;
			else
				comparisonType = StringComparison.CurrentCultureIgnoreCase;

			if (string.Compare(searchText, textBox.SelectedText, comparisonType) == 0 && textBox.SelectionLength > 0)
				startPos++;

			pos = textBox.Text.IndexOf(searchText, startPos, comparisonType);


			if (pos == -1)
			{
				if (textBox.SelectionStart != 0)
				{
					pos = textBox.Text.IndexOf(searchText, 0, comparisonType);
					FindError(Properties.Resources.ReachedEndMessage);
				}
			}

			if (pos == textBox.SelectionStart)
			{
				FindError(Properties.Resources.CannotFindMoreMessage);
			}
			else if (pos == -1)
			{
				FindError(Properties.Resources.NotFoundMessage);
			}
			else
			{
				textBox.SelectionStart = pos;
				textBox.SelectionLength = searchText.Length;
			}
			textBox.ScrollToCaret();
			textBox.Focus();
		}

		private void encodingToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			try { encoding = Encoding.GetEncoding((string)encodingToolStripComboBox.SelectedItem); }
			catch { encoding = Encoding.UTF8; }
			if (data != null)
				UpdateText(encoding);
		}

		private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			findNextToolStripMenuItem.Enabled = !string.IsNullOrEmpty(searchText);
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textBox.Copy();
		}

		private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			textBox.SelectAll();
		}

		private void findToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FindTextForm.SearchText = searchText;
			FindTextForm.CaseSensitive = caseSensitive;
			FindTextForm.EntireWord = entireWord;

			if (FindTextForm.ShowDialog(this) == DialogResult.OK)
			{
				searchText = FindTextForm.SearchText;
				caseSensitive = FindTextForm.CaseSensitive;
				entireWord = FindTextForm.EntireWord;
				if (!string.IsNullOrEmpty(searchText))
					Find();
			}
		}

		private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(searchText))
				Find();
		}
	}
}
