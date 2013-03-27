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
using System.Runtime.InteropServices;
using System.Security;

namespace CrystalMpq.Explorer
{
	internal static class NativeMethods
	{
		public static readonly bool IsVista = IsOSVista();
		private static bool IsOSVista() { return Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6; }

		public const int TV_FIRST = 0x1100;
		public const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
		public const int TVS_EX_DOUBLEBUFFER = 0x0004;
		public const int TVS_EX_NOINDENTSTATE = 0x0008;
		public const int TVS_EX_AUTOHSCROLL = 0x0020;
		public const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;

		[DllImport("user32")]
		[SuppressUnmanagedCodeSecurity]
		public static extern int SendMessage(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);
		
		[DllImport("uxtheme", CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		public extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
	}
}
