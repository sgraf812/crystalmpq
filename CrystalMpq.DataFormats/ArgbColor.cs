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

namespace CrystalMpq.DataFormats
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ArgbColor
	{
		public byte B;
		public byte G;
		public byte R;
		public byte A;

		public ArgbColor(ushort color)
		{
			// Optimization used here:
			// x / 31 = x * 8457 >> 18 (for 0 ≤ x ≤ 31 * 255)
			// x / 63 = x * 16645 >> 20 (for 0 ≤ x ≤ 63 * 255)
			// Since a multiplication is needed anyway, it is probably useless to try optimizing the * 255…
			B = (byte)((color & 0x001F) * (255 * 8457) >> 18);
			G = (byte)(((color & 0x07E0) >> 5) * (255 * 16645) >> 20);
			R = (byte)((color >> 11) * (255 * 8457) >> 18);
			A = 255;
		}
		
		public ArgbColor(byte r, byte g, byte b)
			: this(r, g, b, 255) { }

		public ArgbColor(byte r, byte g, byte b, byte a)
		{
			B = b;
			G = g;
			R = r;
			A = a;
		}

		/// <summary>Merges two colors for DXT decompression.</summary>
		/// <param name="result">The storage to be used for the result.</param>
		/// <param name="color1">A color.</param>
		/// <param name="color2">A color.</param>
		internal static unsafe void DxtMergeHalves(ArgbColor* result, ArgbColor* color1, ArgbColor* color2)
		{
			result->B = (byte)((color1->B + color2->B) >> 1);
			result->G = (byte)((color1->G + color2->G) >> 1);
			result->R = (byte)((color1->R + color2->R) >> 1);
			result->A = 255;
		}

		/// <summary>Merges two colors for DXT decompression.</summary>
		/// <param name="result">The storage to be used for the result.</param>
		/// <param name="minColor">The color whose weight will be 1/3.</param>
		/// <param name="maxColor">The color whose weight will be 2/3.</param>
		internal static unsafe void DxtMergeThirds(ArgbColor* result, ArgbColor* minColor, ArgbColor* maxColor)
		{
			// Formula used here:
			// x / 3 = x * 683 >> 11 (for 0 ≤ x ≤ 3 * 255)
			// Need to verify that this is indeed faster, but it'll do the work for now.
			result->B = (byte)((minColor->B + maxColor->B + maxColor->B) * 683 >> 11);
			result->G = (byte)((minColor->G + maxColor->G + maxColor->G) * 683 >> 11);
			result->R = (byte)((minColor->R + maxColor->R + maxColor->R) * 683 >> 11);
			result->A = 255;
		}

		/// <summary>Copies an <see cref="ArgbColor"/> into another, with forced opaque alpha.</summary>
		/// <param name="destination">The destination color.</param>
		/// <param name="source">The source color.</param>
		internal static unsafe void CopyOpaque(ArgbColor* destination, ArgbColor* source)
		{
			destination->B = source->B;
			destination->G = source->G;
			destination->R = source->R;
			destination->A = 255;
		}

		/// <summary>Copies an <see cref="ArgbColor"/> into another, with forced opaque alpha.</summary>
		/// <param name="destination">The destination color.</param>
		/// <param name="source">The source color.</param>
		internal static unsafe void CopyWithAlpha(ArgbColor* destination, ArgbColor* source, byte alpha)
		{
			destination->B = source->B;
			destination->G = source->G;
			destination->R = source->R;
			destination->A = alpha;
		}
	}
}
