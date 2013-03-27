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
using System.ComponentModel;

namespace CrystalMpq.Explorer
{
	internal class EnhancedListView : ListView
	{
		private bool explorerStyle;

		public EnhancedListView() { this.DoubleBuffered = true; }

		[DefaultValue(false)]
		[Category("Appearance")]
		public bool ExplorerStyle
		{
			get { return explorerStyle; }
			set
			{
				if (value != explorerStyle)
				{
					explorerStyle = value;
					UpdateVisualStyle();
				}
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			UpdateVisualStyle();

			base.OnHandleCreated(e);
		}

		private void UpdateVisualStyle() { if (NativeMethods.IsVista) NativeMethods.SetWindowTheme(Handle, explorerStyle ? "explorer" : null, null); }
	}
}
