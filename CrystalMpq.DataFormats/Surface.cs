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
	/// <summary>Represents a surface of image data.</summary>
	public abstract class Surface : IDisposable, ICloneable
	{
		private int width;
		private int height;
		private byte alphaBitCount;
		private bool alphaPremultiplied;
		private bool locked;

		/// <summary>Initializes a new instance of the <see cref="Surface"/> class.</summary>
		/// <remarks>
		/// The following restriction is imposed on <paramref name="width"/> and <paramref name="height"/>:
		/// The size in bytes of the uncompressed ARGB data for a given surface must fit into an <see cref="Int32"/>.
		/// </remarks>
		/// <param name="width">The surface width.</param>
		/// <param name="height">The surface height.</param>
		/// <param name="alphaBitCount">The alpha bit count.</param>
		/// <param name="alphaPremultiplied">If set to <c>true</c>, the surface uses premultiplied alpha.</param>
		/// <exception cref="ArgumentOutOfRangeException">Either <paramref name="width"/> or <paramref name="height"/> has a value that is not allowed.</exception>
		/// <exception cref="ArgumentException">
		/// The dimensions specified by <paramref name="width"/> and <paramref name="height"/> are not allowed.
		/// - or -
		/// <paramref name="alphaPremultiplied"/> is true while <paramref name="alphaBitCount"/> is zero.
		/// - or -
		/// Another parameter verification has failed.
		/// </exception>
		public Surface(int width, int height, byte alphaBitCount, bool alphaPremultiplied = false)
		{
			if (width < 0) throw new ArgumentOutOfRangeException("width");
			if (height < 0) throw new ArgumentOutOfRangeException("height");
			if (sizeof(int) * (long)width * (long)height > int.MaxValue) throw new ArgumentException();
			if (alphaPremultiplied && alphaBitCount == 0) throw new ArgumentException();

			ValidateDimensions(width, height);

			this.width = width;
			this.height = height;

			this.alphaBitCount = alphaBitCount;
			this.alphaPremultiplied = alphaPremultiplied;
		}

		/// <summary>Initializes a new instance of the <see cref="Surface"/> class by copying data from another <see cref="Surface"/>.</summary>
		/// <remarks>
		/// The base implementation in <see cref="Surface"/> only copies the common surface characteristics.
		/// Copying the actual surface data needs to be done by subclasses overriding this constructor.
		/// </remarks>
		/// <param name="surface">A reference surface which should be copied.</param>
		/// <exception cref="ArgumentNullException"><paramref name="surface"/> is <c>null</c>.</exception>
		protected Surface(Surface surface)
		{
			if (surface == null) throw new ArgumentNullException("surface");

			this.width = surface.width;
			this.height = surface.height;

			this.alphaBitCount = surface.alphaBitCount;
			this.alphaPremultiplied = surface.alphaPremultiplied;
		}

		/// <summary>Releases unmanaged resources and performs other cleanup operations before the <see cref="Surface"/> is reclaimed by garbage collection.</summary>
		~Surface() { Dispose(false); }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases unmanaged and - optionally - managed resources.</summary>
		/// <remarks>Implementation in the base class do nothing.</remarks>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing) { }

		/// <summary>Gets the width of the surface.</summary>
		/// <value>The width of the surface.</value>
		public int Width { get { return width; } }

		/// <summary>Gets the height of the surface.</summary>
		/// <value>The height of the surface.</value>
		public int Height { get { return height; } }

		/// <summary>Gets the maximum number of bits used for alpha in a pixel.</summary>
		/// <remarks>
		/// This information indicates the number of alpha bits used in the underlying format.
		/// Depending on the actual surface format, there may be no alpha data, or the bit count may vary depending on the pixel.
		/// This information serves only to give a general idea on how much transparency information is stored in the surface.
		/// </remarks>
		/// <value>The maximum number of bits used for alpha.</value>
		public int AlphaBitCount { get { return alphaBitCount; } }

		/// <summary>Gets a value indicating whether this instance has premultiplied alpha.</summary>
		/// <remarks>Note that this property is useless when <see cref="AlphaBitCount"/> is zero.</remarks>
		/// <value><c>true</c> if this instance has premultiplied alpha; otherwise, <c>false</c>.</value>
		public bool IsAlphaPremultiplied { get { return alphaPremultiplied; } }

		/// <summary>Gets a value indicating whether the surface is opaque.</summary>
		/// <remarks>This is exactly the same as checking if <see cref="AlphaBitCount"/> is zero.</remarks>
		/// <value><c>true</c> if the surface is opaque; otherwise, <c>false</c>.</value>
		public bool IsOpaque { get { return alphaBitCount == 0; } }

		/// <summary>Gets a value indicating whether the surface is opaque.</summary>
		/// <remarks>This is exactly the same as checking if <see cref="AlphaBitCount"/> is nonzero.</remarks>
		/// <value><c>true</c> if the surface is opaque; otherwise, <c>false</c>.</value>
		public bool IsTransparent { get { return alphaBitCount != 0; } }

		/// <summary>Validates the specified surface dimensions.</summary>
		/// <remarks>
		/// This method should throw an <see cref="ArgumentOutOfRangeException"/> if any of the two dimensions are not allowed.
		/// If the combination of <paramref name="width"/> and <paramref name="height"/> is not allowed, <see cref="ArgumentException"/> should be thrown.
		/// Note that the method will be called before object initialization. Thus, the instance should not be used at all from this method.
		/// </remarks>
		/// <param name="width">The proposed surface width.</param>
		/// <param name="height">The proposed surface height.</param>
		/// <exception cref="ArgumentOutOfRangeException">Either <paramref name="width"/> or <paramref name="height"/> has a value that is not allowed.</exception>
		/// <exception cref="ArgumentException">The dimensions specified by <paramref name="width"/> and <paramref name="height"/> are not allowed.</exception>
		protected virtual void ValidateDimensions(int width, int height) { }

		/// <summary>Gets a value indicating whether the surface can be locked.</summary>
		/// <remarks>
		/// Locking the surface allows for direct modification of the buffer.
		/// While it will work nicely with uncompressed formats or fixed length compression formats, it is likely not to be a very good idea for variable-length compression formats.
		/// </remarks>
		/// <value><c>true</c> if the surface can be locked; otherwise, <c>false</c>.</value>
		public abstract bool CanLock { get; }

		/// <summary>Locks the surface for direct read/write access.</summary>
		/// <remarks>
		/// <see cref="Unlock"/> must be called once direct buffer access is not needed anymore.
		/// Forgetting to unlock a locked surface is likely to prevent the buffer from being disposed, thus causing a memory leak.
		/// Neither <see cref="Dispose"/> nor <see cref="Finalize"/> will unlock the surface, for safety reasons.
		/// </remarks>
		/// <returns>Information for accessing the surface buffer.</returns>
		public SurfaceData Lock()
		{
			if (locked) throw new InvalidOperationException();

			int stride;
			var dataPointer = LockInternal(out stride);

			locked = true;

			return new SurfaceData(Width, Height, dataPointer, stride);
		}

		/// <summary>Locks the surface for direct read/write access.</summary>
		/// <param name="stride">The stride.</param>
		/// <returns></returns>
		protected abstract IntPtr LockInternal(out int stride);

		public void Unlock()
		{
			if (!locked) throw new InvalidOperationException();

			UnlockInternal();

			locked = false;
		}

		protected abstract void UnlockInternal();

		/// <summary>Copies the contents of the surface to a buffer of same dimensions.</summary>
		/// <remarks>
		/// The destination buffer should use the ARGB format represented by <see cref="ArgbColor"/>.
		/// Some basic checks will be done to disallow invalid buffer informations.
		/// However, it is the responsibility of the caller to provide a valid destination buffer.
		/// </remarks>
		/// <param name="surfaceData">Information on the destination buffer.</param>
		/// <exception cref="ArgumentException">The dimensions of the buffer specified by <paramref name="surfaceData"/> do not match those of the surface.</exception>
		/// <exception cref="InvalidOperationException">The stride in <paramref name="surfaceData"/> does not match the width.</exception>
		public void CopyToArgb(SurfaceData surfaceData)
		{
			if (surfaceData.Width != width || surfaceData.Height != height) throw new ArgumentException();
			if (surfaceData.Stride < sizeof(uint) * surfaceData.Width) throw new InvalidOperationException();

			CopyToArgbInternal(surfaceData);
		}

		protected abstract void CopyToArgbInternal(SurfaceData surfaceData);

		/// <summary>Gets a copy of the buffer's contents.</summary>
		/// <returns>A buffer containing the same data as the surface's internal buffer.</returns>
		public abstract byte[] ToArray();

		/// <summary>Creates a stream for accessing the surface data.</summary>
		/// <remarks>The returned stream can be used for reading the surface data, and, for some surface formats, to modify the surface data.</remarks>
		/// <returns>A stream which can be sued to access the surface data.</returns>
		public abstract Stream CreateStream();

		public virtual object Clone()
		{
			var clone = MemberwiseClone() as Surface;

			clone.locked = false;

			return clone;
		}

		/// <summary>Base class for wrapping a Surface inside another one.</summary>
		/// <remarks>Inherit this class for defining a new type of surface that itself contains no data.</remarks>
		public abstract class Wrapper : Surface
		{
			private Surface @this;

			public Wrapper(Surface surface)
				: base(surface) { this.@this = surface; }

			protected override void Dispose(bool disposing)
			{
				if (disposing && @this != null)
				{
					@this.Dispose();
					@this = null;
				}
			}

			public Surface BaseSurface { get { return @this; } }

			public sealed override bool CanLock { get { return @this.CanLock; } }

			protected sealed override IntPtr LockInternal(out int stride) { return @this.LockInternal(out stride); }

			protected sealed override void UnlockInternal() { @this.UnlockInternal(); }

			protected sealed override void CopyToArgbInternal(SurfaceData surfaceData) { @this.CopyToArgbInternal(surfaceData); }

			public sealed override byte[] ToArray() { return @this.ToArray(); }

			public sealed override Stream CreateStream() { return @this.CreateStream(); }
		}
	}
}
