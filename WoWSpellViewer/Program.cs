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
using System.Globalization;
using System.Collections.Generic;
using System.Windows.Forms;
using CrystalMpq.WoW;

namespace WoWSpellViewer
{
	static class Program
	{
		/// <summary>
		/// Point d'entrée principal de l'application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			WoWInstallation wowInstallation;
			WoWLanguagePack languagePack;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			wowInstallation = FindWoWInstallation();

			if (wowInstallation == null)
				return;

			languagePack = ChooseLanguagePack(wowInstallation);

			if (languagePack == null)
				return;

			Properties.Settings.Default.LanguagePackCulture = languagePack.Culture;

			Application.Run(new MainForm(wowInstallation, languagePack));
		}

		static WoWInstallation FindWoWInstallation()
		{
			WoWInstallation wowInstallation = null;

			// Try to find a valid wow installation
			try { wowInstallation = WoWInstallation.Find(); }
			catch (DirectoryNotFoundException)
			{
				// If we can't find wow, terminate here
				MessageBox.Show(Properties.Resources.CannotFindWow, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}

			return wowInstallation;
		}

		static WoWLanguagePack ChooseLanguagePack(WoWInstallation wowInstallation)
		{
			CultureInfo desiredCulture = Properties.Settings.Default.LanguagePackCulture;

			if (wowInstallation.LanguagePacks.Count == 1)
				return wowInstallation.LanguagePacks[0];

			foreach (WoWLanguagePack languagePack in wowInstallation.LanguagePacks)
				if (languagePack.Culture == desiredCulture)
					return languagePack;

			using (LanguagePackDialog languagePackDialog = new LanguagePackDialog())
			{
				languagePackDialog.WoWInstallation = wowInstallation;

				switch (languagePackDialog.ShowDialog())
				{
					case DialogResult.OK:
						return languagePackDialog.LanguagePack;
					default:
						return null;
				}
			}
		}
	}
}