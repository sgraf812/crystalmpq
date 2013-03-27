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
	/// <summary>Represents an archive in a World of Warcraft installation.</summary>
	public struct WoWArchiveInformation
	{
		/// <summary>Gets the filename of the archive represented by this entry.</summary>
		public readonly string Filename;
		/// <summary>Gets the kind of archive represented by this entry.</summary>
		public readonly WoWArchiveKind Kind;
		/// <summary>Gets the patch number associated with this entry, or zero if there is none.</summary>
		public readonly int PatchNumber;

		internal WoWArchiveInformation(string filename, WoWArchiveKind kind, int patchNumber = 0)
		{
			Filename = filename;
			Kind = kind;
			PatchNumber = patchNumber;
		}
	}
}
