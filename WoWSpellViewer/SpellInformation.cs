#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using CrystalMpq;
using CrystalMpq.DataFormats;
using CrystalMpq.WoWDatabases;
#endregion

namespace WoWSpellViewer
{
	sealed class SpellInformation
	{
		KeyedClientDatabase<int, SpellRecord> spellDatabase;
		CharacterInformation characterInformation;
		int spellId;
		SpellRecord spellRecord;
		SpellValue lastValue;
		readonly object syncRoot = new object();

		public SpellInformation(KeyedClientDatabase<int, SpellRecord> spellDatabase)
		{
			if (spellDatabase == null)
				throw new ArgumentNullException("spellDatabase");
			characterInformation = new CharacterInformation();
			this.spellDatabase = spellDatabase;
			spellId = -1;
		}

		public int SpellId
		{
			get
			{
				lock (syncRoot)
					return spellId;
			}
			set
			{
				SpellRecord record;

				if (spellDatabase.TryGetValue(value, out record))
				{
					spellId = value;
					spellRecord = record;
				}
			}
		}

		public CharacterInformation CharacterInformation
		{
			get
			{
				return characterInformation;
			}
		}

		public int BookIcon
		{
			get
			{
				lock (syncRoot)
					return spellRecord.BookSpellIcon;
			}
		}

		public int BuffIcon
		{
			get
			{
				lock (syncRoot)
					return spellRecord.TargetSpellIcon;
			}
		}

		public string Name
		{
			get
			{
				lock (syncRoot)
					return spellRecord.Name;
			}
		}

		public string Rank
		{
			get
			{
				lock (syncRoot)
					return spellRecord.Rank;
			}
		}

		public string BookDescription
		{
			get
			{
				lock (syncRoot)
					return Format(spellRecord.BookDescription);
			}
		}

		public string BuffDescription
		{
			get
			{
				lock (syncRoot)
					return Format(spellRecord.BookDescription);
			}
		}

		public string Effect1Text
		{
			get
			{
				return FormatEffect(spellRecord.Effect1, spellRecord.Effect1Aura);
			}
		}

		public string Effect2Text
		{
			get
			{
				return FormatEffect(spellRecord.Effect2, spellRecord.Effect2Aura);
			}
		}

		public string Effect3Text
		{
			get
			{
				return FormatEffect(spellRecord.Effect3, spellRecord.Effect3Aura);
			}
		}

		private string FormatEffect(int effectId, int auraId)
		{
			if (auraId != 0)
				return SpellEffectNameDictionary.GetEffectName(effectId) + " (" + SpellAuraNameDictionary.GetAuraName(auraId) + ")";
			else
				return SpellEffectNameDictionary.GetEffectName(effectId);
		}

		private string Format(string text)
		{
			return text;
		}

		private SpellValue GetVariable(string name)
		{
			if (name == "l")
				return lastValue;
			else
				return GetVariable(ref spellRecord, name);
		}

		public SpellValue GetVariable(int spellId, string name)
		{
			SpellRecord? spellRecord = spellDatabase[spellId];

			if (spellRecord.HasValue)
				return GetVariable(spellRecord.Value, name);
			else
				throw new ArgumentOutOfRangeException("spellId");
		}

		private static SpellValue GetVariable(SpellRecord spellRecord, string name)
		{
			return GetVariable(ref spellRecord, name);
		}

		private static SpellValue GetVariable(ref SpellRecord spellRecord, string name)
		{
			switch (name)
			{
				case "a1": return spellRecord.Effect1Radius;
				case "a2": return spellRecord.Effect2Radius;
				case "a3": return spellRecord.Effect3Radius;

				case "b1": return spellRecord.Effect1ProcChance;
				case "b2": return spellRecord.Effect2ProcChance;
				case "b3": return spellRecord.Effect3ProcChance;

				case "e1": return spellRecord.Effect1ProcValue;
				case "e2": return spellRecord.Effect2ProcValue;
				case "e3": return spellRecord.Effect3ProcValue;

				case "f1": return spellRecord.Effect1DamageMultiplier;
				case "f2": return spellRecord.Effect2DamageMultiplier;
				case "f3": return spellRecord.Effect3DamageMultiplier;

				case "h": return spellRecord.ProcChance;
				case "i": return spellRecord.MaxTargets;

				case "m1": return 0;
				case "M1": return 0;
				case "m2": return 0;
				case "M2": return 0;
				case "m3": return 0;
				case "M3": return 0;

				case "n": return spellRecord.ProcCharges;

				case "o1": return 0;
				case "o2": return 0;
				case "o3": return 0;

				case "q1": return spellRecord.Effect1MiscValue1;
				case "q2": return spellRecord.Effect2MiscValue1;
				case "q3": return spellRecord.Effect3MiscValue1;

				case "r": return 0;
				case "R": return 0;

				case "s1": return 0;
				case "S1": return 0;
				case "s2": return 0;
				case "S2": return 0;
				case "s3": return 0;
				case "S3": return 0;

				case "t1": return spellRecord.Effect1Amplitude;
				case "t2": return spellRecord.Effect2Amplitude;
				case "t3": return spellRecord.Effect3Amplitude;

				case "u": return spellRecord.MaxStacks;

				case "v": return spellRecord.MaxTargetLevel;

				case "x1": return spellRecord.Effect1ChainTarget;
				case "x2": return spellRecord.Effect2ChainTarget;
				case "x3": return spellRecord.Effect3ChainTarget;

				default: throw new Exception("Unsupported variable: " + name); ;
			}
		}
	}
}
