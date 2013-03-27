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
	internal class EnhancedTreeView : TreeView
	{
		private bool explorerStyle;
		private bool fadePlusMinus;
		private bool autoHorizontalScroll;

		public EnhancedTreeView() { }

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

		[DefaultValue(false)]
		[Category("Appearance")]
		public bool FadePlusMinus
		{
			get { return fadePlusMinus; }
			set
			{
				if (value != fadePlusMinus)
				{
					fadePlusMinus = value;
					UpdateExtendedStyles();
				}
			}
		}

		[DefaultValue(false)]
		[Category("Appearance")]
		public bool AutoHorizontalScroll
		{
			get { return autoHorizontalScroll; }
			set
			{
				if (value != autoHorizontalScroll)
				{
					autoHorizontalScroll = value;
					UpdateExtendedStyles();
				}
			}
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			UpdateExtendedStyles();
			UpdateVisualStyle();

			base.OnHandleCreated(e);
		}

		private void UpdateExtendedStyles()
		{
			if (NativeMethods.IsVista)
			{
				int styles = 0;

				styles |= NativeMethods.TVS_EX_DOUBLEBUFFER;
				if (autoHorizontalScroll) styles |= NativeMethods.TVS_EX_AUTOHSCROLL;
				if (fadePlusMinus) styles |= NativeMethods.TVS_EX_FADEINOUTEXPANDOS;
				styles |= NativeMethods.TVS_EX_NOINDENTSTATE;

				NativeMethods.SendMessage(Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, (IntPtr)(NativeMethods.TVS_EX_DOUBLEBUFFER | NativeMethods.TVS_EX_AUTOHSCROLL | NativeMethods.TVS_EX_FADEINOUTEXPANDOS | NativeMethods.TVS_EX_NOINDENTSTATE), (IntPtr)styles);
			}
		}

		private void UpdateVisualStyle() { if (NativeMethods.IsVista) NativeMethods.SetWindowTheme(Handle, explorerStyle ? "explorer" : null, null); }
	}
}
