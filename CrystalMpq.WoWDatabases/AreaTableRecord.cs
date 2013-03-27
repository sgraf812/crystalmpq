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
	[DebuggerDisplay("AreaTableRecord: Id={Id}, Name={Name}")]
	public struct AreaTableRecord
	{
		/* 000 */ [Id] public int Id;
		/* 001 */ public int Map;
		/* 002 */ public int Parent;
		/* 003 */ public int UnknownId1;
		/* 004 */ public int Flags;
		/* 005 */ public int SoundPreferences;
		/* 006 */ public int SoundPreferencesUnderwater;
		/* 007 */ public int SoundAmbience;
		/* 008 */ public int ZoneMusic;
		/* 009 */ public int ZoneIntroMusic;
		/* 010 */ public int AreaLevel;
		/* 011 */ public string Name;
		/* 012 */ public int FactionGroup;
		/* 013 */ public int LiquidType1;
		/* 014 */ public int LiquidType2; // Never set until now, but the 3 other fields around are confirmed…
		/* 015 */ public int LiquidType3;
		/* 016 */ public int LiquidType4;
		/* 017 */ public float MinimumElevation;
		/* 018 */ public float AmbientMultiplier;
		/* 019 */ public int Unknown1; // 0
		/* 020 */ public int LightId; // Unverified
		/* 021 */ public int Unknown2; // 0
		/* 022 */ public int Unknown3;
		/* 023 */ public int Unknown4; // Either 0 or 675…
		/* 023 */ public int Unknown5;
		/* 023 */ public int UnknownId2;
	}
}
