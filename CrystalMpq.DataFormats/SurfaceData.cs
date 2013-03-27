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

namespace CrystalMpq.DataFormats
{
	public struct SurfaceData
	{
		public readonly IntPtr DataPointer;
		public readonly int Stride;
		public readonly int Width;
		public readonly int Height;

		public SurfaceData(int width, int height, IntPtr dataPointer, int stride)
		{
			Width = width;
			Height = height;
			DataPointer = dataPointer;
			Stride = stride;
		}
	}
}
