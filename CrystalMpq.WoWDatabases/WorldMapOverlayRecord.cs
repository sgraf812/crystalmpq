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
using System.Diagnostics;

namespace CrystalMpq.WoWDatabases
{
	[StructLayout(LayoutKind.Sequential)]
	[DebuggerDisplay("WorldMapOverlayRecord: Id={Id}, DataName={DataName}")]
	public struct WorldMapOverlayRecord
	{
		/* 000 */ [Id] public int Id;
		/* 001 */ public int WorldMapArea;
		/* 002 */ public int Area1;
		/* 003 */ public int Area2;
		/* 004 */ public int Area3;
		/* 005 */ public int Area4;
		/* 006 */ public string DataName;
		/* 007 */ public int Width;
		/* 008 */ public int Height;
		/* 009 */ public int Left;
		/* 010 */ public int Top;
		/* 011 */ public int BoxTop;
		/* 012 */ public int BoxLeft;
		/* 013 */ public int BoxBottom;
		/* 014 */ public int BoxRight;
	}
}
