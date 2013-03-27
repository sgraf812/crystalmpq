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
	[DebuggerDisplay("WorldMapAreaRecord: Id={Id}, DataName={DataName}, Name={Name}")]
	public struct MapRecord
	{
		/* 000 */ [Id] public int Id;
		/* 001 */ public string DataName;
		/* 002 */ public int AreaType;
		/* 003 */ public int ExtraInformation;
		/* 004 */ public int Unknown1;
		/* 005 */ public bool IsBattleground;
		/* 006 */ public string Name;
		/* 007 */ public int AreaId;
		/* 008 */ public string HordeDescription;
		/* 009 */ public string AllianceDescription;
		/* 010 */ public int LoadingScreen;
		/* 011 */ public float MapIconScaling;
		/* 012 */ public int ParentMapId;
		/* 013 */ public float EntryCoordX;
		/* 014 */ public float EntryCoordY;
		/* 015 */ public int TimeOfDayOverride;
		/* 016 */ public int ExpansionNumber;
		/* 017 */ public int Unknown2;
		/* 018 */ public int MaximumPlayerCount;
		/* 019 */ public int PhaseMapId;
	}
}
