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
using System.IO;

namespace CrystalMpq.DataFormats
{
	public sealed class ArgbSurface : Surface
	{
		private GCHandle dataHandle;
		private byte[] data;

		public ArgbSurface(int width, int height, bool alphaPremultiplied = false)
			: base(width, height, 8, alphaPremultiplied) { data = new byte[sizeof(uint) * Width * Height]; }

		public ArgbSurface(byte[] rawData, int width, int height, bool alphaPremultiplied = false, bool sharedBuffer = false)
			: base(width, height, 8, alphaPremultiplied)
		{
			if (rawData == null) throw new ArgumentNullException("rawData");

			int length = sizeof(uint) * Width * Height;

			if (rawData.Length != length) throw new ArgumentException();

			data = sharedBuffer ? rawData : rawData.Clone() as byte[];
		}

		public unsafe ArgbSurface(Surface surface)
			: base(surface)
		{
			data = new byte[sizeof(uint) * Width * Height];

			fixed (byte* dataPointer = data)
				surface.CopyToArgb(new SurfaceData(Width, Height, (IntPtr)dataPointer, sizeof(uint) * Width));
		}

		public ArgbSurface(SurfaceData surfaceData, bool alphaPremultiplied = false)
			: base(surfaceData.Width, surfaceData.Height, 8, alphaPremultiplied)
		{
			int rowLength = sizeof(uint) * Width;
			int dataLength = rowLength * Height;

			if (surfaceData.Stride < rowLength) throw new ArgumentException();

			data = new byte[dataLength];

			unsafe
			{
				fixed (byte* dataPointer = data)
				{
					byte* destinationRowPointer = dataPointer;
					byte* sourceRowPointer = (byte*)surfaceData.DataPointer;

					for (int i = Height; i-- != 0; destinationRowPointer += rowLength, sourceRowPointer += surfaceData.Stride)
					{
						ArgbColor* destinationPointer = (ArgbColor*)destinationRowPointer;
						ArgbColor* sourcePointer = (ArgbColor*)sourceRowPointer;

						for (int j = Width; j-- != 0; ) *destinationPointer++ = *sourcePointer++;
					}
				}
			}
		}

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
			int rowLength = sizeof(uint) * Width;

			fixed (byte* dataPointer = data)
			{
				byte* destinationRowPointer = (byte*)surfaceData.DataPointer;
				byte* sourceRowPointer = dataPointer;

				for (int i = Height; i-- != 0; destinationRowPointer += surfaceData.Stride, sourceRowPointer += rowLength)
				{
					ArgbColor* destinationPointer = (ArgbColor*)destinationRowPointer;
					ArgbColor* sourcePointer = (ArgbColor*)sourceRowPointer;

					for (int j = Width; j-- != 0; ) *destinationPointer++ = *sourcePointer++;
				}
			}
		}

		public override byte[] ToArray() { return data.Clone() as byte[]; }

		/// <summary>Creates a stream for accessing the surface data.</summary>
		/// <remarks>The returned stream can be used for reading or modifying the surface data.</remarks>
		/// <returns>A stream which can be used to access the surface data.</returns>
		public override Stream CreateStream() { return new MemoryStream(data, true); }

		public override object Clone()
		{
			var clone = base.Clone() as ArgbSurface;

			clone.data = data.Clone() as byte[];

			return clone;
		}
	}
}
