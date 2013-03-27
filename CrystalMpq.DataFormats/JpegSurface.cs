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

namespace CrystalMpq.DataFormats
{
	public sealed class JpegSurface : Surface
	{
		private byte[] data;

		public JpegSurface(byte[] rawData, int width, int height, bool shareBuffer = false)
			: base(width, height, 0, false)
		{
			if (rawData == null) throw new ArgumentNullException("rawData");

			data = shareBuffer ? rawData : rawData.Clone() as byte[];
		}

		public override bool CanLock { get { return false; } }

		protected override IntPtr LockInternal(out int stride) { throw new NotSupportedException(); }

		protected override void UnlockInternal() { throw new NotSupportedException(); }

		protected override void CopyToArgbInternal(SurfaceData surfaceData) { throw new NotSupportedException(); }

		public override byte[] ToArray() { return data.Clone() as byte[]; }

		/// <summary>Creates a stream for accessing the surface data.</summary>
		/// <remarks>The returned stream can be used for reading the surface data.</remarks>
		/// <returns>A stream which can be used to access the surface data.</returns>
		public override Stream CreateStream() { return new MemoryStream(data, false); }

		public override object Clone()
		{
			var clone = base.Clone() as JpegSurface;

			clone.data = data.Clone() as byte[];

			return clone;
		}
	}
}
