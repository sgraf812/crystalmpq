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
using CrystalMpq.DataFormats;

namespace CrystalMpq.WoWDatabases
{
	[StructLayout(LayoutKind.Sequential)]
	public struct PhaseRecord
	{
		/* 000 */ [Id] public int Id;
		/* 001 */ public string Name;
		/* 002 */ public int Flags;
	}
}
