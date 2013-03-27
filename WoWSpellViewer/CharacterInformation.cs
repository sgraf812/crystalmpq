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
	sealed class CharacterInformation
	{
		int level;
		Genre genre;

		public CharacterInformation()
		{
			level = 1;
			genre = Genre.Male;
		}

		public int Level
		{
			get
			{
				return level;
			}
			set
			{
				if (value < 1)
					throw new ArgumentOutOfRangeException();
				level = value;
			}
		}

		public Genre Genre
		{
			get
			{
				return genre;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Genre), value))
					throw new ArgumentOutOfRangeException("value");
				genre = value;
			}
		}
	}
}
