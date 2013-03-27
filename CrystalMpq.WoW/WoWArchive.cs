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
	public struct WoWArchive
	{
		/// <summary>Gets the <see cref="MpqArchive"/> associated with this entry.</summary>
		public readonly MpqArchive Archive;
		/// <summary>Gets the kind of archive represented by this entry.</summary>
		public readonly WoWArchiveKind Kind;

		internal WoWArchive(MpqArchive archive, WoWArchiveKind kind)
		{
			Archive = archive;
			Kind = kind;
		}
	}
}
