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
	[DebuggerDisplay("WorldMapAreaRecord: Id={Id}, DataName={DataName}")]
	public struct WorldMapAreaRecord
	{
		[Id] public int Id;
		public int Map;
		public int Area;
		public string DataName;
		public float BoxRight;
		public float BoxLeft;
		public float BoxBottom;
		public float BoxTop;
		public int VirtualMap;
		public int DungeonMapId;
		public int ParentId;
		public int Flags;
	}
}
