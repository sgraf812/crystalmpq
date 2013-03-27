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
using System.Windows.Forms;
using System.Collections.Generic;

namespace CrystalMpq.Explorer.Extensibility
{
	/// <summary>
	/// Defines an host for a FileViewer plugin
	/// </summary>
	public interface IHost : IWin32Window
	{
		/// <summary>
		/// The filename of the selected file in the host interface
		/// </summary>
		String SelectedFileName { get; }
		/// <summary>
		/// The color to use as a back color for the control
		/// </summary>
		/// <remarks>
		/// This color can be anything choosen by the used, so use it only where you feel it should be used.
		/// </remarks>
		Color ViewerBackColor { get; }
		/// <summary>
		/// Displays a message in the status bar of the host application
		/// </summary>
		/// <param name="text">Text of the message</param>
		void StatusMessage(string text);
	}
}
