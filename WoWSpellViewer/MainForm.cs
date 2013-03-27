#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

#region Using Statements
using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalMpq;
using CrystalMpq.DataFormats;
using CrystalMpq.WoW;
using CrystalMpq.WoWDatabases;
#endregion

namespace WoWSpellViewer
{
	partial class MainForm : Form
	{
		WoWInstallation wowInstallation;
		WoWLanguagePack languagePack;
		WoWMpqFileSystem wowFileSystem;
		KeyedClientDatabase<int, SpellRecord> spellDatabase;
		KeyedClientDatabase<int, SpellIconRecord> spellIconDatabase;
		BlpTexture currentIconTexture;
		SpellInformation spellInformation;
		int currentSpellIndex;

		public MainForm(WoWInstallation wowInstallation, WoWLanguagePack languagePack)
		{
			InitializeComponent();
			this.wowInstallation = wowInstallation;
			this.languagePack = languagePack;
			this.wowFileSystem = wowInstallation.CreateFileSystem(languagePack, false);
			spellDatabase = LoadDatabase<SpellRecord>(@"DBFilesClient\Spell.dbc");
			spellIconDatabase = LoadDatabase<SpellIconRecord>(@"DBFilesClient\SpellIcon.dbc");
			countToolStripLabel.Text = string.Format(CultureInfo.CurrentUICulture, Properties.Resources.SpellCountFormatString, spellDatabase.Records.Count);
			spellInformation = new SpellInformation(spellDatabase);
			UpdateDisplayInfo();
		}

		private KeyedClientDatabase<int, T> LoadDatabase<T>(string filename) where T : struct
		{
			return LoadDatabase<int, T>(filename);
		}

		private KeyedClientDatabase<TKey, TValue> LoadDatabase<TKey, TValue>(string filename) where TValue : struct
		{
			MpqFile file;
			Stream fileStream = null;

			if ((file = wowFileSystem.FindFile(filename)) != null)
				using (fileStream = file.Open())
					return new KeyedClientDatabase<TKey, TValue>(fileStream, languagePack.DatabaseFieldIndex);
			else
				return null;
		}

		private BlpTexture LoadTexture(string filename)
		{
			MpqFile file;
			Stream fileStream = null;
			BlpTexture texture;

			try
			{
				file = wowFileSystem.FindFile(filename);
				fileStream = file.Open();
				texture = new BlpTexture(fileStream, false);
			}
			catch
			{
				texture = null;
			}
			finally
			{
				if (fileStream != null)
					fileStream.Close();
			}

			return texture;
		}

		private void FirstSpell()
		{
			currentSpellIndex = 0;
			UpdateDisplayInfo();
		}

		private void PreviousSpell()
		{
			if (--currentSpellIndex < 0)
				currentSpellIndex += spellDatabase.Records.Count;
			UpdateDisplayInfo();
		}

		private void NextSpell()
		{
			if (++currentSpellIndex >= spellDatabase.Records.Count)
				currentSpellIndex -= spellDatabase.Records.Count;
			UpdateDisplayInfo();
		}

		private void LastSpell()
		{
			currentSpellIndex = spellDatabase.Records.Count - 1;
			UpdateDisplayInfo();
		}

		private void UpdateDisplayInfo()
		{
			indexToolStripTextBox.Text = (currentSpellIndex + 1).ToString();
			UpdateDisplayInfo(spellDatabase.Records[currentSpellIndex]);
		}

		private void UpdateDisplayInfo(SpellRecord spellRecord)
		{
			SpellIconRecord? spellIconRecord;

			spellInformation.SpellId = spellRecord.Id;
			spellIconRecord = spellIconDatabase[spellRecord.BookSpellIcon];
			spellIconPictureBox.Image = null;
			if (spellIconRecord.HasValue)
				try
				{
					currentIconTexture = LoadTexture(spellIconRecord.Value.Path + ".blp");
					//spellIconPictureBox.Image = currentIconTexture.FirstMipMap;
				}
				catch
				{
				}
			else
			{
				currentIconTexture.Dispose();
				currentIconTexture = null;
				spellIconPictureBox.Image = null;
			}
			spellIdLabel.Text = spellRecord.Id.ToString();
			spellLevelLabel.Text = spellRecord.LevelBase.ToString();
			manaCostLabel.Text = spellRecord.ManaCost.ToString();
			if (spellRecord.Rank != null && spellRecord.Rank.Length > 0)
				spellNameLabel.Text = spellRecord.Name + " (" + spellRecord.Rank + ")";
			else
				spellNameLabel.Text = spellRecord.Name;
			spellDescriptionLabel.Text = spellInformation.BookDescription;
			spellEffect1Label.Text = spellInformation.Effect1Text;
			spellEffect2Label.Text = spellInformation.Effect2Text;
			spellEffect3Label.Text = spellInformation.Effect3Text;
		}

		private void firstItemToolStripButton_Click(object sender, EventArgs e)
		{
			FirstSpell();
		}

		private void previousItemToolStripButton_Click(object sender, EventArgs e)
		{
			PreviousSpell();
		}

		private void nextItemToolStripButton_Click(object sender, EventArgs e)
		{
			NextSpell();
		}

		private void lastItemToolStripButton_Click(object sender, EventArgs e)
		{
			LastSpell();
		}

		private string FormatValue(string formatParameter, ref SpellRecord spellRecord)
		{
			if (formatParameter == "d")
				return spellRecord.SpellDuration.ToString() + " sec";
			else if (formatParameter == "s1")
				return spellRecord.Effect1BasePoints.ToString();
			else if (formatParameter == "s2")
				return spellRecord.Effect2BasePoints.ToString();
			else if (formatParameter == "s3")
				return spellRecord.Effect3BasePoints.ToString();
			else
				return "{}";
		}

		private void indexToolStripTextBox_Validating(object sender, CancelEventArgs e)
		{
			try
			{
				int value = int.Parse(indexToolStripTextBox.Text, Properties.Resources.Culture);
				if (value < 0 || value >= spellDatabase.Records.Count)
					throw new ArgumentOutOfRangeException();
				currentSpellIndex = value;
				UpdateDisplayInfo();
			}
			catch
			{
				e.Cancel = true;
			}
		}

		private void indexToolStripTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
				ValidateChildren(ValidationConstraints.Enabled | ValidationConstraints.Selectable | ValidationConstraints.Visible);
		}
	}
}