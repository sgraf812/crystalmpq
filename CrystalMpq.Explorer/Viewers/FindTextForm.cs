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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CrystalMpq.Explorer.Viewers
{
	partial class FindTextForm : Form
	{
		public FindTextForm()
		{
			InitializeComponent();
		}

		public string SearchText
		{
			get
			{
				return textBox.Text;
			}
			set
			{
				textBox.Text = value;
			}
		}

		public bool CaseSensitive
		{
			get
			{
				return caseSensitiveCheckBox.Checked;
			}
			set
			{
				caseSensitiveCheckBox.Checked = value;
			}
		}

		public bool EntireWord
		{
			get
			{
				return entireWordCheckBox.Checked;
			}
			set
			{
				entireWordCheckBox.Checked = value;
			}
		}
	}
}