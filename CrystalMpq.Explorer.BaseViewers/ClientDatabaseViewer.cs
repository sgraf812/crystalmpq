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
using System.IO;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using CrystalMpq.DataFormats;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer.BaseViewers
{
	public partial class ClientDatabaseViewer : FileViewer
	{
		RawClientDatabase database;
		int[] fieldType;
		bool[] stringCompatible;
		int clickedColumnIndex;
		int firstCachedItemIndex;
		List<ListViewItem> cachedItems;

		public ClientDatabaseViewer(IHost host)
			: base(host)
		{
			cachedItems = new List<ListViewItem>(256);
			InitializeComponent();
			ApplySettings();
		}

		public override void ApplySettings()
		{
		}

		public override MenuStrip Menu { get { return menuStrip; } }
		public override ToolStrip MainToolStrip { get { return mainToolStrip; } }
		public override StatusStrip StatusStrip { get { return statusStrip; } }

		public RawClientDatabase Database
		{
			get
			{
				return database;
			}
			set
			{
				if (value != database)
				{
					File = null;
					database = value;
					UpdateListView();
					UpdateStatusInformation();
				}
			}
		}

		protected override void OnFileChanged()
		{
			if (File == null)
			{
				Database = null;
				return;
			}
			else
			{
				Stream stream;

				if (File.Size <= 0x400000
					|| MessageBox.Show(
						Host, Properties.Resources.DatabaseSizeWarningMessage,
						string.Format(Properties.Resources.Culture, Properties.Resources.DatabaseSizeWarningTitle, Host.SelectedFileName),
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Exclamation) == DialogResult.Yes)
				{
					stream = File.Open();
					try { Database = new RawClientDatabase(stream); }
					finally { stream.Close(); }
				}
				else
				{
					Database = null;
				}
			}
		}

		private int BitCount(int value)
		{
			int count = 0;

			for (int n = 32; n > 0; n--, value >>= 1)
				if ((value & 1) != 0)
					count++;

			return count;
		}

		private void EnableExportButtons(bool enable)
		{
			exportToolStripMenuItem.Enabled = enable;
			exportToolStripButton.Enabled = enable;
		}

		private void ShowStatusInformation(bool show)
		{
			fieldCountToolStripStatusLabel.Visible = show;
			recordCountToolStripStatusLabel.Visible = show;
		}

		private void UpdateStatusInformation()
		{
			if (database == null)
			{
				ShowStatusInformation(false);
			}
			else
			{
				if (database.FieldCount == database.ClaimedFieldCount)
					fieldCountToolStripStatusLabel.Text = string.Format(Properties.Resources.Culture,
						Properties.Resources.StandardFieldCountFormat, database.FieldCount);
				else
					fieldCountToolStripStatusLabel.Text = string.Format(Properties.Resources.Culture,
						Properties.Resources.WrongFieldCountFormat, database.FieldCount, database.ClaimedFieldCount);
				recordCountToolStripStatusLabel.Text = string.Format(Properties.Resources.Culture,
						Properties.Resources.RecordCountFormat, database.Records.Count);
				ShowStatusInformation(true);
			}
		}

		private void UpdateListView()
		{
			EnableExportButtons(false);

			listView.BeginUpdate();

			listView.VirtualListSize = 0;
			listView.Columns.Clear();
			ClearCache();

			if (database != null)
			{
				fieldType = new int[database.FieldCount]; // Array for datatype information
				stringCompatible = new bool[database.FieldCount]; // Array for string compatibility information

				// Detect cell types
				DetectColumnTypes();

				// Add columns
				for (int i = 0; i < database.FieldCount; i++)
					listView.Columns.Add(i.ToString());

				listView.VirtualListSize = database.Records.Count;

				// Enable export buttons
				EnableExportButtons(true);
			}

			listView.EndUpdate();
		}

		public void DetectColumnTypes()
		{
			// For each field we look if it is compatible with different types
			for (int i = 0; i < database.FieldCount; i++)
			{
				fieldType[i] = 15; // Enable 4 possibilities first
				// 1: string
				// 2: float
				// 4: boolean ( 0 / 1 )
				// 8: bit mask
				bool zero = false,
					one = false;
				int mask = 0,
					fullMask = 0;

				for (int j = 0; j < database.Records.Count; j++)
				{
					int value = database.Records[j][i];

					if (value == 0)
						zero = true;
					else
					{
						if (value == 1)
							one = true;
						else
							fieldType[i] &= ~4;
						if (value >= 0)
							mask |= value;
						fullMask |= value;
						if ((value & 0x7F100000) == 0)
						{
							// We assume exponent bits used in float are not used for common integer or string offsets
							// At least for string offsets, this is always true.
							// For integer, the use of these bits seems to be limited to bitmasks.
							// Therefore if we find a non null value where all of these "potential" exponent bits are clear,
							// we assume it is NOT a floating point value
							fieldType[i] &= ~2;
						}
						else if (Math.Abs(RawClientDatabase.GetFloat(value)) < 1e-5) // Too small values are suspicious
							fieldType[i] &= ~2;
						if (!database.HasStringWithOffset(value))
						{
							// If for ONE record, we cannot match the field with a string,
							// then the field is not of type string
							fieldType[i] &= ~1; // Masks out the string flag
						}
					}
				}
				// Set string compatibility flag
				stringCompatible[i] = (fieldType[i] & 1) != 0;

				if (fullMask == -1)
					fieldType[i] &= ~2; // Discard float format if mask is -1
				if ((fieldType[i] & 4) != 0 && mask != 1 && ((zero && !one) || (!zero && one)))
					fieldType[i] &= ~4;
				// Must have at least 16 bits set to consider a bit field
				// The first field should *never* be a bitfield
				int bitCount = BitCount(mask);
				if (i == 0 || mask != fullMask || bitCount < 16)
					fieldType[i] &= ~8;
				if (fieldType[i] == 5 || fieldType[i] == 4) // If we hesitate between string and boolean, choose boolean
					fieldType[i] = 0; // 4 <=> 0 are displayed the same way
				if (fieldType[i] == 10) // If we hesitate between float ant bitfield, choose float
					fieldType[i] = 2;
				if (fieldType[i] == 3 || fieldType[i] == 7 || fieldType[i] == 9 || fieldType[i] == 12 || fieldType[i] == 15) // If we cannot determine a field type
					fieldType[i] = 1; // Then choose string over float, boolean or bitfield
			}
		}

		private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				clickedColumnIndex = e.ColumnIndex;
				integerToolStripMenuItem.Checked = fieldType[clickedColumnIndex] == 0;
				stringToolStripMenuItem.Checked = fieldType[clickedColumnIndex] == 1;
				floatToolStripMenuItem.Checked = fieldType[clickedColumnIndex] == 2;
				hexadecimalToolStripMenuItem.Checked = fieldType[clickedColumnIndex] == 8;
				stringToolStripMenuItem.Enabled = stringCompatible[clickedColumnIndex];
				columnTypeContextMenuStrip.Show(Cursor.Position);
			}
		}

		private void UpdateColumn(int clickedColumnIndex)
		{
			listView.Refresh();
		}

		private void integerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fieldType[clickedColumnIndex] = 0;
			UpdateColumn(clickedColumnIndex);
		}

		private void stringToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fieldType[clickedColumnIndex] = 1;
			UpdateColumn(clickedColumnIndex);
		}

		private void floatToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fieldType[clickedColumnIndex] = 2;
			UpdateColumn(clickedColumnIndex);
		}

		private void hexadecimalToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fieldType[clickedColumnIndex] = 8;
			UpdateColumn(clickedColumnIndex);
		}

		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog(Host) == DialogResult.OK)
			{
				// It seems Excel will only accept ANSI or UTF16-LE, and we need to have unicode support
				using (var writer = new StreamWriter(saveFileDialog.FileName, false, new UnicodeEncoding(false, false)))
				{
					foreach (var record in database.Records)
					{
						for (int i = 0; i < database.FieldCount; i++)
						{
							if (i > 0)
								writer.Write(';');

							switch (fieldType[i])
							{
								case 0: // Integer
									writer.Write(record[i].ToString(CultureInfo.InvariantCulture));
									break;
								case 1: // String
									writer.Write('"');
									writer.Write(database.GetStringWithOffset(record[i]));
									writer.Write('"');
									break;
								case 2: // Float
									writer.Write(RawClientDatabase.GetFloat(record[i]).ToString(CultureInfo.InvariantCulture));
									break;
								case 4: // Boolean
									goto case 0;
								case 8: // Bit field
									writer.Write("0x");
									writer.Write(record[i].ToString("X8", CultureInfo.InvariantCulture));
									break;
								default:
									goto case 0;
							}
						}
						writer.WriteLine();
					}
				}
			}
		}

		private void listView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			if (e.ItemIndex >= firstCachedItemIndex
				&& e.ItemIndex < firstCachedItemIndex + cachedItems.Count)
				e.Item = cachedItems[e.ItemIndex - firstCachedItemIndex];
			else
				e.Item = GetItem(e.ItemIndex);
		}

		private void listView_CacheVirtualItems(object sender, CacheVirtualItemsEventArgs e)
		{
			int length = e.EndIndex - e.StartIndex;

			if (e.StartIndex >= firstCachedItemIndex
				&& e.EndIndex < firstCachedItemIndex + cachedItems.Count)
				return;

			cachedItems.Clear();
			firstCachedItemIndex = e.StartIndex;

			for (int i = 0; i < length; i++)
				cachedItems.Add(GetItem(firstCachedItemIndex + i));
		}

		private void ClearCache()
		{
			cachedItems.Clear();
			firstCachedItemIndex = 0;
		}

		private ListViewItem GetItem(int index)
		{
			ListViewItem item = new ListViewItem();

			for (int i = 0; i < database.FieldCount; i++)
			{
				string value;

				switch (fieldType[i])
				{
					case 0: // Integer
						value = database.Records[index][i].ToString();
						break;
					case 1: // String
						value = database.GetStringWithOffset(database.Records[index][i]);
						break;
					case 2: // Float
						value = RawClientDatabase.GetFloat(database.Records[index][i]).ToString();
						break;
					case 4: // Boolean
						goto case 0;
					case 8: // Bit field
						value = "0x" + database.Records[index][i].ToString("X8");
						break;
					default:
						goto case 0;
				}

				if (i == 0)
					item.Text = value;
				else
					item.SubItems.Add(value);
			}

			return item;
		}
	}
}
