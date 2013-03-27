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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer.Viewers
{
	internal sealed partial class HexadecimalViewer : UserControl
	{
		static Font defaultFont = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);

		public HexadecimalViewer()
		{
			InitializeComponent();
			base.Font = defaultFont;
		}

		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (value == null || value == DefaultFont)
					base.Font = defaultFont;
				else
					base.Font = value;
			}
		}

		public bool ShouldSerializeFont()
		{
			return Font != defaultFont;
		}

		public override void ResetFont()
		{
			base.Font = defaultFont;
		}
	}
}
