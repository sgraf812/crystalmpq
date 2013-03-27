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
	public struct SpellRecord
	{
		// A field is missing somewhere, i messed up...
		/* 001 */ [Id] public int Id;

		/* 002 */ public int SpellCategory; // Checked
		/* 003 */ public int SpellDispelType; // Checked
		/* 004 */ public int SpellMechanic; // Checked

		/* 005 */ public int Unknown1;
		/* 006 */ public int Unknown2;
		/* 007 */ public int Targets;
		/* 008 */ public int Unknown3;
		/* 009 */ public int Unknown4;
		/* 010 */ public int Unknown5;
		/* 011 */ public int Unknown6;
		/* 012 */ public int Unknown7;

		/* 013 */ public int ValidSpellShapeShiftForms; // ±Checked
		/* 014 */ public float Unknown8;
		/* 015 */ public int InvalidSpellShapeShiftForms; // ±Checked
		/* 016 */ public float Unknown9;

		/* 017 */ public int ItemClass; // Mask, need better check

		/* 018 */ public int CreatureType; // Checked (Mask)

		/* 019 */ public int SpellFocusObject; // Checked

		/* 020 */ public int FacingCaster; // Or direct damage ?

		/* 021 */ public int CasterAuraState;
		/* 022 */ public int TargetAuraState; // ±Checked

		/* 023 */ public int CasterInvisibility; // Not sure
		/* 024 */ public int TargetInvisibility; // Not sure
		/* 025 */ public int Unknown10;
		/* 026 */ public int Unknown11;
		/* 027 */ public int CasterApplySpell;
		/* 028 */ public int TargetApplySpell;
		
		/* 029 */ public int SpellCastTimes; // Checked
		/* 030 */ public int CategoryRecoveryTime; // Checked
		/* 031 */ public int RecoveryTime; // Checked

		/* 032 */ public int InterruptFlags; // ±Checked
		/* 033 */ public int AuraInterruptFlags;
		/* 034 */ public int ChannelInterruptFlags;
		
		/* 035 */ public int ProcFlags; // Checked
		/* 036 */ public int ProcChance;
		/* 037 */ public int ProcCharges;

		/* 038 */ public int LevelMax; // Checked
		/* 039 */ public int LevelBase;
		/* 040 */ public int LevelSpell;

		/* 041 */ public int SpellDuration; // Checked

		/* 042 */ public int PowerType;

		/* 043 */ public int ManaCost;
		/* 044 */ public int ManaCostPerLevel;
		/* 045 */ public int ManaPerSecond;
		/* 046 */ public int ManaPerSecondPerLevel;

		/* 047 */ public int SpellRange; // Checked

		/* 048 */ public float ProjectileSpeed; // Checked

		/* 049 */ public int ModalNextSpell; // ±Checked

		/* 050 */ public int MaxStacks; // ±Checked

		/* 051 */ public int Tool1; // Checked
		/* 052 */ public int Tool2; // Checked
		
		/* 053 */ public int Reagent1; // Checked
		/* 054 */ public int Reagent2;
		/* 055 */ public int Reagent3;
		/* 056 */ public int Reagent4;
		/* 057 */ public int Reagent5;
		/* 058 */ public int Reagent6;
		/* 059 */ public int Reagent7;
		/* 060 */ public int Reagent8;

		/* 061 */ public int ReagentCount1; // Checked
		/* 062 */ public int ReagentCount2;
		/* 063 */ public int ReagentCount3;
		/* 064 */ public int ReagentCount4;
		/* 065 */ public int ReagentCount5;
		/* 066 */ public int ReagentCount6;
		/* 067 */ public int ReagentCount7;
		/* 068 */ public int ReagentCount8;

		/* 069 */ public int RequiredItemClass; // ±Checked
		/* 070 */ public int RequiredItemSubClass; // ±Checked 
		/* 071 */ public int InventorySlots; // Checked

		/* 072 */ public int Effect1; // Checked
		/* 073 */ public int Effect2;
		/* 074 */ public int Effect3;

		/* 075 */ public int Effect1DieSides;
		/* 076 */ public int Effect2DieSides;
		/* 077 */ public int Effect3DieSides;

		/* 078 */ public int Effect1BaseDice;
		/* 079 */ public int Effect2BaseDice;
		/* 080 */ public int Effect3BaseDice;

		/* 081 */ public int Effect1DicePerLevel;
		/* 082 */ public int Effect2DicePerLevel;
		/* 083 */ public int Effect3DicePerLevel;

		/* 084 */ public float Effect1RealPointsPerLevel;
		/* 085 */ public float Effect2RealPointsPerLevel;
		/* 086 */ public float Effect3RealPointsPerLevel;

		/* 087 */ public int Effect1BasePoints;
		/* 088 */ public int Effect2BasePoints;
		/* 089 */ public int Effect3BasePoints;

		/* 090 */ public int Effect1Mechanic; // Checked
		/* 091 */ public int Effect2Mechanic; // Checked
		/* 092 */ public int Effect3Mechanic; // Checked

		/* 093 */ public int Effect1ImplicitTargetA;
		/* 094 */ public int Effect2ImplicitTargetA;
		/* 095 */ public int Effect3ImplicitTargetA;

		/* 096 */ public int Effect1ImplicitTargetB;
		/* 097 */ public int Effect2ImplicitTargetB;
		/* 098 */ public int Effect3ImplicitTargetB;

		/* 099 */ public int Effect1Radius;
		/* 100 */ public int Effect2Radius;
		/* 101 */ public int Effect3Radius;

		/* 102 */ public int Effect1Aura; // Checked
		/* 103 */ public int Effect2Aura; // Checked
		/* 104 */ public int Effect3Aura; // Checked

		/* 105 */ public int Effect1Amplitude;
		/* 106 */ public int Effect2Amplitude;
		/* 107 */ public int Effect3Amplitude;

		/* 108 */ public float Effect1ProcValue;
		/* 109 */ public float Effect2ProcValue;
		/* 110 */ public float Effect3ProcValue;

		/* 111 */ public int Effect1ChainTarget;
		/* 112 */ public int Effect2ChainTarget;
		/* 113 */ public int Effect3ChainTarget;

		/* 114 */ public int Effect1ItemType;
		/* 115 */ public int Effect2ItemType;
		/* 116 */ public int Effect3ItemType;

		// Modifiers:
		// 01 = Duration
		// 08 = Value ?
		// 10 = Casting Time
		// 11 = Cooldown
		// 14 = Mana Cost
		// 23 = Bonus Damage ?
		/* 117 */ public int Effect1MiscValue1; // Id Langage
		/* 118 */ public int Effect2MiscValue1;
		/* 119 */ public int Effect3MiscValue1;

		/* 120 */ public int Effect1MiscValue2;
		/* 121 */ public int Effect2MiscValue2;
		/* 122 */ public int Effect3MiscValue2;

		/* 123 */ public int Effect1TriggerSpell; // Checked
		/* 124 */ public int Effect2TriggerSpell; // Checked
		/* 125 */ public int Effect3TriggerSpell; // Checked

		/* 126 */ public float Effect1ProcChance;
		/* 127 */ public float Effect2ProcChance;
		/* 128 */ public float Effect3ProcChance;

		/* 129 */ public int Effect1SpellClassMask1;
		/* 130 */ public int Effect2SpellClassMask1;
		/* 131 */ public int Effect3SpellClassMask1;

		/* 132 */ public int Effect1SpellClassMask2;
		/* 133 */ public int Effect2SpellClassMask2;
		/* 134 */ public int Effect3SpellClassMask2;

		/* 135 */ public int Effect1SpellClassMask3;
		/* 136 */ public int Effect2SpellClassMask3;
		/* 137 */ public int Effect3SpellClassMask3;

		/* 138 */ public int SpellVisual1;

		/* 139 */ public int SpellVisual2;

		/* 140 */ public int BookSpellIcon;
		/* 141 */ public int TargetSpellIcon;

		/* 142 */ public int SpellPriority;

		/* 143-159 */ [Localized] public string Name;
		/* 160-176 */ [Localized] public string Rank;
		/* 177-193 */ [Localized] public string BookDescription;
		/* 194-210 */ [Localized] public string TargetDescription;

		/* 211 */ public int ManaCostPercent; // Checked

		/* 212 */ public int StartRecoveryCategory;
		/* 213 */ public int StartRecoveryTime;

		/* 214 */ public int MaxTargetLevel; // Checked

		/* 215 */ public int ChrClasses; // Checked

		/* 216 */ public int Unknown12;
		/* 217 */ public int Unknown13;

		/* 218 */ public int MaxTargets; // Wrong ?

		/* 219 */ public int Unknown14;

		/* 220 */ public int Effect1DamageClass;
		/* 221 */ public int Effect2DamageClass;
		/* 222 */ public int Effect3DamageClass;
		/* 223 */ public float Effect1DamageMultiplier;
		/* 224 */ public float Effect2DamageMultiplier;
		/* 225 */ public float Effect3DamageMultiplier;

		/* 226 */ public int Faction;
		/* 227 */ public int MinimumReputation;
		/* 228 */ public int Unknown17;

		/* 229 */ public int TotemCategory1;
		/* 230 */ public int TotemCategory2;

		/* 231 */ public int RequiredArea;

		/* 232 */ public int Resistances;

		/* 233 */ public int RuneCost;
		/* 234 */ public int Missile;
		/* 235 */ public int Unknown18;

		/* 236 */ public float Unknown19;
		/* 237 */ public float Unknown20;
		/* 238 */ public int Unknown21;
		/* 239 */ public int Unknown22;
		/* 240 */ public int SpellDifficulty;
	}
}
