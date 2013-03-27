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
using System.Collections.Generic;
using System.Text;

namespace WoWSpellViewer
{
	struct SpellValue
	{
		public static SpellValue Empty = new SpellValue();

		float rangeMin, rangeMax;

		public SpellValue(float value)
		{
			rangeMin = value;
			rangeMax = value;
		}

		public SpellValue(float min, float max)
		{
			rangeMin = min;
			rangeMax = max;
		}

		public float RangeMin
		{
			get
			{
				return rangeMin;
			}
			set
			{
				rangeMin = value;
			}
		}

		public float RangeMax
		{
			get
			{
				return rangeMax;
			}
			set
			{
				rangeMax = value;
			}
		}

		public static implicit operator SpellValue(int value)
		{
			return new SpellValue(value);
		}

		public static implicit operator SpellValue(float value)
		{
			return new SpellValue(value);
		}
	}
}
