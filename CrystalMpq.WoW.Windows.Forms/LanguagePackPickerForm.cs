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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace CrystalMpq.WoW
{
	partial class LanguagePackPickerForm : Form
	{
		WoWInstallation wowInstallation;

		public LanguagePackPickerForm()
		{
			InitializeComponent();
		}

		public WoWInstallation WoWInstallation
		{
			get
			{
				return wowInstallation;
			}
			set
			{
				if (wowInstallation != value)
				{
					wowInstallation = value;
					languageComboBox.Items.Clear();
					if (wowInstallation != null)
					{
						WoWLanguagePack selectedLanguagePack = null;

						foreach (WoWLanguagePack languagePack in wowInstallation.LanguagePacks)
						{
							if (languagePack.Culture.TwoLetterISOLanguageName == CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
								selectedLanguagePack = languagePack;
							languageComboBox.Items.Add(languagePack);
						}
						if (selectedLanguagePack == null && wowInstallation.LanguagePacks.Count > 0)
							selectedLanguagePack = wowInstallation.LanguagePacks[0];
						languageComboBox.SelectedItem = selectedLanguagePack;
					}
				}
			}
		}

		public WoWLanguagePack SelectedLanguagePack
		{
			get
			{
				return languageComboBox.SelectedItem as WoWLanguagePack;
			}
			set
			{
				if (value != null && value.WoWInstallation != wowInstallation)
					throw new ArgumentException();
				languageComboBox.SelectedItem = value;
			}
		}
	}
}