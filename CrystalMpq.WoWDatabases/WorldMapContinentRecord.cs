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
	public struct WorldMapContinentRecord
	{
		[Id] public int Id;
		public int Map;
		public int Unknown1;
		public int Unknown2;
		public int Unknown3;
		public int Unknown4;
		public float CoordX;
		public float CoordY;
		public float Scaling;
		public float Left;
		public float Top;
		public float Right;
		public float Bottom;
		public bool IsOnAzeroth;		
	}
}
