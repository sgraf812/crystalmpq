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
	public abstract class DxtSurface : Surface
	{
		private GCHandle dataHandle;
		protected byte[] data;

		internal DxtSurface(int width, int height, byte alphaBitCount, bool alphaPremultiplied = false)
			: base(width, height, alphaBitCount, alphaPremultiplied) { }

		internal unsafe DxtSurface(byte[] rawData, int width, int height, byte alphaBitCount, bool alphaPremultiplied = false, bool shareBuffer = false)
			: base(width, height, alphaBitCount, alphaPremultiplied)
		{
			if (rawData == null) throw new ArgumentNullException("rawData");

			int wr = width & 3;
			int hr = height & 3;
			int length = (((wr != 0 ? width + 4 - wr : width) * (hr != 0 ? height + 4 - hr : height)) & ~0xF) >> (alphaBitCount > 1 ? 0 : 1);

			if (rawData.Length != length) throw new ArgumentException();

			data = shareBuffer ? rawData : rawData.Clone() as byte[];
		}

		internal unsafe DxtSurface(byte* rawData, int width, int height, byte alphaBitCount, bool alphaPremultiplied = false)
			: base(width, height, alphaBitCount, alphaPremultiplied)
		{
			if (rawData == null) throw new ArgumentNullException("rawData");

			int wr = width & 3;
			int hr = height & 3;
			int length = (((wr != 0 ? width + 4 - wr : width) * (hr != 0 ? height + 4 - hr : height)) & ~0xF) >> (alphaBitCount > 1 ? 0 : 1);

			data = new byte[length];

			for (int i = 0; i < data.Length; i++) data[i] = *rawData++;
		}

		public override bool CanLock { get { return true; } }

		protected override IntPtr LockInternal(out int stride)
		{
			dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);

			stride = Width * sizeof(uint);

			return dataHandle.AddrOfPinnedObject();
		}

		protected override void UnlockInternal() { dataHandle.Free(); }

		public override byte[] ToArray() { return data.Clone() as byte[]; }

		/// <summary>Creates a stream for accessing the surface data.</summary>
		/// <remarks>The returned stream can be used for reading or modifying the surface data.</remarks>
		/// <returns>A stream which can be used to access the surface data.</returns>
		public override Stream CreateStream() { return new MemoryStream(data, true); }

		public override object Clone()
		{
			var clone = base.Clone() as DxtSurface;

			clone.data = data.Clone() as byte[];

			return clone;
		}
	}
}
