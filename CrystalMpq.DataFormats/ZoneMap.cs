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
using System.IO;

namespace CrystalMpq.DataFormats
{
	public sealed class ZoneMap
	{
		int[,] map;

		public ZoneMap(Stream stream)
		{
			BinaryReader reader = new BinaryReader(stream);

			map = new int[128, 128];

			for (int i = 0; i < 128; i++)
				for (int j = 0; j < 128; j++)
					map[j, i] = reader.ReadInt32();
		}

		public int this[int x, int y]
		{
			get
			{
				return map[x, y];
			}
		}

		public int Height { get { return 128; } }
		public int Width { get { return 128; } }
	}
}
