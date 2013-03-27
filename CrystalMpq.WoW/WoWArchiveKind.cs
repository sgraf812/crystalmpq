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

namespace CrystalMpq.WoW
{
	[Flags]
	public enum WoWArchiveKind
	{
		Regular = 0,
		Base = 1,
		LanguagePack = 2,
		Global = 3,
		Patch = 4
	}
}
