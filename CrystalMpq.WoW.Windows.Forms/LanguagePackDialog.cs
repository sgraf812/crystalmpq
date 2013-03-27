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
using System.Windows.Forms;

namespace CrystalMpq.WoW
{
	public sealed class LanguagePackDialog : IDisposable
	{
		LanguagePackPickerForm languagePackPickerForm;

		public LanguagePackDialog() { languagePackPickerForm = new LanguagePackPickerForm(); }

		public WoWInstallation WoWInstallation
		{
			get { return languagePackPickerForm.WoWInstallation; }
			set { languagePackPickerForm.WoWInstallation = value; }
		}

		public WoWLanguagePack LanguagePack
		{
			get { return languagePackPickerForm.SelectedLanguagePack; }
			set { languagePackPickerForm.SelectedLanguagePack = value; }
		}

		public DialogResult ShowDialog() { return languagePackPickerForm.ShowDialog(); }

		public DialogResult ShowDialog(IWin32Window owner) { return languagePackPickerForm.ShowDialog(owner); }

		public void Dispose() { languagePackPickerForm.Dispose(); }
	}
}
