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
using System.IO;
using System.Runtime.InteropServices;

namespace CrystalMpq.DataFormats
{
	public sealed class PaletteSurface : Surface
	{
		private GCHandle dataHandle;
		private ArgbColor[] palette;
		private byte[] data;
		private bool separateAlpha;

		public PaletteSurface(ArgbColor[] palette, int width, int height, bool opaque = true, byte separateAlphaBitCount = 0, bool alphaPremultiplied = false, bool sharePalette = false)
			: base(width, height, GetAlphaBitCount(opaque, separateAlphaBitCount), alphaPremultiplied)
		{
			if (palette == null) throw new ArgumentNullException("palette");
			if (palette.Length != 256) throw new ArgumentException();

			int length = Width * Height;

			if (this.separateAlpha = separateAlphaBitCount != 0)
			{
				int divisor = 8 / separateAlphaBitCount;
				int remainder = length % divisor;
				length = length / divisor;
				if (remainder != 0) length++;
			}

			this.data = new byte[length];
			this.palette = sharePalette ? palette : palette.Clone() as ArgbColor[];
		}

		public PaletteSurface(byte[] rawData, ArgbColor[] palette, int width, int height, bool opaque = true, byte separateAlphaBitCount = 0, bool alphaPremultiplied = false, bool shareBuffer = false, bool sharePalette = false)
			: base(width, height, GetAlphaBitCount(opaque, separateAlphaBitCount), alphaPremultiplied)
		{
			if (rawData == null) throw new ArgumentNullException("rawData");
			if (palette == null) throw new ArgumentNullException("palette");
			if (palette.Length != 256) throw new ArgumentException();

			int length = Width * Height;

			if (this.separateAlpha = separateAlphaBitCount != 0)
			{
				int divisor = 8 / separateAlphaBitCount;
				int remainder = length % divisor;
				length += length / divisor;
				if (remainder != 0) length++;
			}

			if (rawData.Length != length) throw new ArgumentException();

			this.data = shareBuffer ? rawData : rawData.Clone() as byte[];
			this.palette = sharePalette ? palette : palette.Clone() as ArgbColor[];
		}

		private static byte GetAlphaBitCount(bool opaque, byte separateAlphaBitCount)
		{
			if (opaque && separateAlphaBitCount != 0) throw new ArgumentException();

			switch (separateAlphaBitCount)
			{
				case 0: return opaque ? (byte)0 : (byte)8;
				case 1: return 1;
				case 4: return 4;
				case 8: return 8;
				default: throw new ArgumentOutOfRangeException("separateAlphaBitCount");
			}
		}

		public bool SeparateAlpha { get { return separateAlpha; } }

		public override bool CanLock { get { return true; } }

		protected override IntPtr LockInternal(out int stride)
		{
			dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);

			stride = Width * sizeof(uint);

			return dataHandle.AddrOfPinnedObject();
		}

		protected override void UnlockInternal() { dataHandle.Free(); }

		protected unsafe override void CopyToArgbInternal(SurfaceData surfaceData)
		{
			switch (AlphaBitCount)
			{
				case 0: CopyToArgbOpaque(surfaceData); break;
				case 1: CopyToArgbAlpha1(surfaceData); break;
				case 4: CopyToArgbAlpha4(surfaceData); break;
				case 8: if (separateAlpha) CopyToArgbAlpha8(surfaceData); else CopyToArgbTransparent(surfaceData); break;
				default: throw new NotSupportedException(); // Should never happen…
			}
		}

		private unsafe void CopyToArgbOpaque(SurfaceData surfaceData)
		{
			int rowLength = Width;

			fixed (byte* dataPointer = data)
			fixed (ArgbColor* palettePointer = palette)
			{
				byte* destinationRowPointer = (byte*)surfaceData.DataPointer;
				byte* sourcePointer = dataPointer;

				for (int i = Height; i-- != 0; destinationRowPointer += surfaceData.Stride)
				{
					ArgbColor* destinationPointer = (ArgbColor*)destinationRowPointer;

					for (int j = Width; j-- != 0; ) ArgbColor.CopyOpaque(destinationPointer++, palettePointer + *sourcePointer++);
				}
			}
		}

		private unsafe void CopyToArgbTransparent(SurfaceData surfaceData)
		{
			int rowLength = Width;

			fixed (byte* dataPointer = data)
			fixed (ArgbColor* palettePointer = palette)
			{
				byte* destinationRowPointer = (byte*)surfaceData.DataPointer;
				byte* sourcePointer = dataPointer;

				for (int i = Height; i-- != 0; destinationRowPointer += surfaceData.Stride)
				{
					ArgbColor* destinationPointer = (ArgbColor*)destinationRowPointer;

					for (int j = Width; j-- != 0; ) *destinationPointer++ = palettePointer[*sourcePointer++];
				}
			}
		}

		private unsafe void CopyToArgbAlpha8(SurfaceData surfaceData)
		{
			fixed (byte* dataPointer = data)
			fixed (ArgbColor* palettePointer = palette)
			{
				byte* destinationRowPointer = (byte*)surfaceData.DataPointer;
				byte* sourceColorPointer = dataPointer;
				byte* sourceAlphaPointer = dataPointer + Width * Height;

				for (int i = Height; i-- != 0; destinationRowPointer += surfaceData.Stride)
				{
					ArgbColor* destinationPointer = (ArgbColor*)destinationRowPointer;

					for (int j = Width; j-- != 0; ) ArgbColor.CopyWithAlpha(destinationPointer++, palettePointer + *sourceColorPointer++, *sourceAlphaPointer++);
				}
			}
		}

		private unsafe void CopyToArgbAlpha4(SurfaceData surfaceData)
		{
			// Use a precalculated alpha table for very fast conversions.
			var alphaTable = stackalloc byte[16];
			for (int i = 0; i < 16; i++) alphaTable[i] = (byte)(i * (255 * 2185) >> 15); // x / 15 = x * 2185 >> 15 (for 0 ≤ x ≤ 15 * 255)

			fixed (byte* dataPointer = data)
			fixed (ArgbColor* palettePointer = palette)
			{
				byte* destinationRowPointer = (byte*)surfaceData.DataPointer;
				byte* sourceColorPointer = dataPointer;
				byte* sourceAlphaPointer = dataPointer + Width * Height;
				byte alphaData = 0;
				bool alphaState = false;

				for (int i = Height; i-- != 0; destinationRowPointer += surfaceData.Stride)
				{
					ArgbColor* destinationPointer = (ArgbColor*)destinationRowPointer;

					for (int j = Width; j-- != 0; )
						ArgbColor.CopyWithAlpha(destinationPointer++, palettePointer + *sourceColorPointer++, alphaTable[(alphaState = !alphaState) ? (alphaData = *sourceAlphaPointer++) & 0xF : alphaData >> 4]);
				}
			}
		}

		private unsafe void CopyToArgbAlpha1(SurfaceData surfaceData)
		{
			fixed (byte* dataPointer = data)
			fixed (ArgbColor* palettePointer = palette)
			{
				byte* destinationRowPointer = (byte*)surfaceData.DataPointer;
				byte* sourceColorPointer = dataPointer;
				byte* sourceAlphaPointer = dataPointer + Width * Height;
				byte alphaData = 0;
				byte alphaState = 0;

				for (int i = Height; i-- != 0; destinationRowPointer += surfaceData.Stride)
				{
					ArgbColor* destinationPointer = (ArgbColor*)destinationRowPointer;

					for (int j = Width; j-- != 0; alphaState--, alphaData >>= 1)
					{
						if (alphaState == 0)
						{
							alphaState = 8;
							alphaData = *sourceAlphaPointer++;
						}

						ArgbColor.CopyWithAlpha(destinationPointer++, palettePointer + *sourceColorPointer++, (alphaData & 1) != 0 ? (byte)255 : (byte)0);
					}
				}
			}
		}

		/// <summary>Gets a copy of the buffer's contents.</summary>
		/// <returns>A buffer containing the same data as the surface's internal buffer.</returns>
		public override byte[] ToArray() { return data.Clone() as byte[]; }

		/// <summary>Creates a stream for accessing the surface data.</summary>
		/// <remarks>The returned stream can be used for reading or modifying the surface data.</remarks>
		/// <returns>A stream which can be used to access the surface data.</returns>
		public override Stream CreateStream() { return new MemoryStream(data, true); }

		public override object Clone()
		{
			var clone = base.Clone() as PaletteSurface;

			clone.data = data.Clone() as byte[];

			return clone;
		}
	}
}
