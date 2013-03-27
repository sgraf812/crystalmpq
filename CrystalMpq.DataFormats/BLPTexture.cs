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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CrystalMpq.DataFormats
{
	/// <summary>Encapsulates bitmap data corresponding to a BLP image.</summary>
	public sealed class BlpTexture : IDisposable
	{
		#region Surface Class

		public sealed class Surface : CrystalMpq.DataFormats.Surface.Wrapper
		{
			private BlpTexture texture;

			internal Surface(BlpTexture texture, CrystalMpq.DataFormats.Surface surface) : base(surface) { this.texture = texture; }

			public BlpTexture Texture { get { return texture; } }
		}

		#endregion

		#region BLP Header Structures

		[StructLayout(LayoutKind.Sequential)]
		private unsafe struct Blp1Header
		{
			public int Compression; // 0 for JPEG, 1 for Palette
			public int Alpha; // 8 for 8 bit alpha, 0 for no alpha
			public int Width; // Width of first mip map
			public int Height; // Height of first mip map
			public int PictureType; // Information on compression (if not JPEG)
			public int PictureSubType; // Always 1 ?
			public fixed int Offsets[16]; // Mip map offsets
			public fixed int Lengths[16]; // Mip map data lengths
		}

		[StructLayout(LayoutKind.Sequential)]
		private unsafe struct Blp2Header
		{
			public int Type;
			public byte Compression;
			public byte AlphaDepth;
			public byte AlphaType;
			public bool HasMipMaps;
			public int Width;
			public int Height;
			public fixed int Offsets[16];
			public fixed int Lengths[16];
		}

		#endregion

		#region SurfaceCollection Class

		public sealed class SurfaceCollection : IEnumerable<Surface>
		{
			private BlpTexture owner;

			internal  SurfaceCollection(BlpTexture owner)
			{
				this.owner = owner;
			}

			public Surface this[int index]
			{
				get
				{
					if (index < 0 || index >= owner.mipmaps.Length) throw new ArgumentOutOfRangeException("index");
					return owner.mipmaps[index];
				}
			}

			public int Count { get { return owner.mipmaps.Length; } }

			public IEnumerator<Surface> GetEnumerator() { return (owner.mipmaps as IEnumerable<Surface>).GetEnumerator(); }

			IEnumerator IEnumerable.GetEnumerator() { return owner.mipmaps.GetEnumerator(); }
		}

		#endregion

		#region AlphaOperation Enum

		private enum AlphaOperation
		{
			None,
			SetAlpha,
			InvertAlpha
		}

		#endregion

		private SurfaceCollection mipmapCollection;
		private Surface[] mipmaps;
		private uint version;
		private int width, height;

		public BlpTexture(Stream stream)
			: this(stream, true) { }

		public BlpTexture(Stream stream, bool loadMipMaps)
		{
			long position = 0;

			if (stream.CanSeek) position = stream.Position;
			LoadFromStream(stream, loadMipMaps);
			if (stream.CanSeek) stream.Seek(position, SeekOrigin.Begin);

			mipmapCollection = new SurfaceCollection(this);
		}

		public BlpTexture(string filename)
			: this(filename, true) { }

		public BlpTexture(string filename, bool loadMipMaps)
		{
			using (var stream = File.Open(filename, FileMode.Open))
				LoadFromStream(stream, loadMipMaps);

			mipmapCollection = new SurfaceCollection(this);
		}

		public int Width { get { return width; } }

		public int Height { get { return height; } }

		// This method reads the file signature and, if valid,
		// dispatches the loading process to version-corresponding functions
		// NB: Actually handles BLP1 and BLP2
		// NB: BLP1 is JPEG-based and BLP2 is DXTC-based
		private void LoadFromStream(Stream stream, bool loadMipMaps)
		{
			BinaryReader reader = new BinaryReader(stream);
			uint signature;
			int startOffset;
			int mipMapCount;

			mipMapCount = loadMipMaps ? 16 : 1;

			// Begin the decoding
			startOffset = (int)stream.Position;
			signature = reader.ReadUInt32();
			if ((signature & 0xFFFFFF) != 0x504C42) throw new InvalidDataException();
			else if (signature >> 24 == 0x31) LoadBlp1(reader, startOffset, mipMapCount); // Use BLP1 reading method
			else if (signature >> 24 == 0x32) LoadBlp2(reader, startOffset, mipMapCount); // Use BLP2 reading method
			else throw new InvalidDataException();
		}

		// Incorrect rendering due to Blizzard's use of Intel's Jpeg Library (CMYK vs YMCK…)
		// We need to swap Red and Blue channels to get a correct image
		private unsafe void LoadBlp1(BinaryReader reader, int startOffset, int mipMapCount)
		{
			Stream stream = reader.BaseStream;
			Blp1Header header;

			header = ReadBlp1Header(reader, startOffset);

			width = header.Width;
			height = header.Height;

			if (header.Compression == 0) LoadJpegMipmaps(reader, startOffset, header.Width, header.Height, header.Offsets, header.Lengths, mipMapCount);
			else if (header.Compression == 1) LoadPalettedMipmaps(reader, startOffset, header.Width, header.Height, header.Offsets, header.Lengths, mipMapCount, header.PictureType == 5, header.PictureType == 5 ? (byte)0 : (byte)8, true);
			else mipmaps = new Surface[0];
		}

		private static unsafe Blp1Header ReadBlp1Header(BinaryReader reader, int startOffset)
		{
			Blp1Header header;

			header.Compression = reader.ReadInt32();
			header.Alpha = reader.ReadInt32();
			header.Width = reader.ReadInt32();
			header.Height = reader.ReadInt32();
			header.PictureType = reader.ReadInt32();
			header.PictureSubType = reader.ReadInt32();

			// Read mipmap info
			for (int i = 0; i < 16; i++) header.Offsets[i] = reader.ReadInt32();
			for (int i = 0; i < 16; i++) header.Lengths[i] = reader.ReadInt32();

			return header;
		}

		private unsafe void LoadBlp2(BinaryReader reader, int startOffset, int mipmapCount)
		{
			var header = ReadBlp2Header(reader, startOffset);

			// Check for standard alpha depth values…
			// Alpha depth is ignored for compression type 3.
			if (header.Compression < 3 && header.AlphaDepth > 2 && header.AlphaDepth != 4 && header.AlphaDepth != 8) throw new InvalidDataException();

			width = header.Width;
			height = header.Height;

			mipmapCount = header.HasMipMaps ? mipmapCount : 1;

			switch (header.Compression)
			{
				case 1:
					LoadPalettedMipmaps(reader, startOffset, header.Width, header.Height, header.Offsets, header.Lengths, mipmapCount, header.AlphaDepth == 0, header.AlphaDepth, false);
					break;
				case 2:
					int realAlphaDepth;

					switch (header.AlphaType)
					{
						case 0:
							if (header.AlphaDepth > 1) throw new InvalidDataException();
							realAlphaDepth = header.AlphaDepth;
							break;
						case 1:
							if (header.AlphaDepth != 4 && header.AlphaDepth != 8) throw new InvalidDataException();
							realAlphaDepth = 4;
							break;
						case 7:
							if ((realAlphaDepth = header.AlphaDepth) != 8) throw new InvalidDataException();
							break;
						default: throw new InvalidDataException();
					}
					LoadDxtMipmaps(reader, startOffset, header.Width, header.Height, header.Offsets, header.Lengths, mipmapCount, realAlphaDepth);
					break;
				case 3:
					LoadRawMipmaps(reader, startOffset, header.Width, header.Height, header.Offsets, header.Lengths, mipmapCount);
					break;
				default: throw new InvalidDataException();
			}
		}

		private static unsafe Blp2Header ReadBlp2Header(BinaryReader reader, int startOffset)
		{
			Blp2Header header;

			if ((header.Type = reader.ReadInt32()) != 1) throw new InvalidDataException();

			header.Compression = (byte)reader.ReadByte();
			header.AlphaDepth = (byte)reader.ReadByte();
			header.AlphaType = (byte)reader.ReadByte();
			header.HasMipMaps = reader.ReadByte() != 0;
			header.Width = reader.ReadInt32();
			header.Height = reader.ReadInt32();

			// Read mipmap info
			for (int i = 0; i < 16; i++) header.Offsets[i] = reader.ReadInt32();
			for (int i = 0; i < 16; i++) header.Lengths[i] = reader.ReadInt32();

			return header;
		}

		private int LowerMipmapDimension(int dimension) { return dimension > 1 ? dimension >> 1 : 1; }

		/// <summary>Reads a 256 color palette from a stream</summary>
		/// <param name="reader">The BinaryReader used for reading data in the stream</param>
		/// <param name="alphaOperation">The operation to apply on each palette entry's alpha component</param>
		/// <returns>An array of bytes containing the palette entries</returns>
		private ArgbColor[] ReadPalette(BinaryReader reader, AlphaOperation alphaOperation)
		{
			var palette = new ArgbColor[256];

			for (int i = 0; i < palette.Length; i++)
			{
				byte b = reader.ReadByte();
				byte g = reader.ReadByte();
				byte r = reader.ReadByte();
				byte a = reader.ReadByte();

				palette[i] = new ArgbColor(r, g, b, alphaOperation != AlphaOperation.None ? alphaOperation == AlphaOperation.SetAlpha ? (byte)255 : (byte)~a : a);
			}

			return palette;
		}

		private unsafe void LoadPalettedMipmaps(BinaryReader reader, int startOffset, int width, int height, int* offsets, int* lengths, int mipMapCount, bool opaque, byte separateAlphaBitCount, bool invertAlpha)
		{
			var mipmapList = new List<Surface>(16);
			int mipmapWidth = width;
			int mipmapHeight = height;

			var palette = ReadPalette(reader, !opaque ? invertAlpha ? AlphaOperation.InvertAlpha : AlphaOperation.None : AlphaOperation.SetAlpha);

			for (int i = 0; i < mipMapCount && offsets[i] != 0 && lengths[i] != 0; i++, mipmapWidth = LowerMipmapDimension(mipmapWidth), mipmapHeight = LowerMipmapDimension(mipmapHeight))
			{
				// Create a new buffer for the current mipmap
				var mipmap = new byte[lengths[i]];
				// Seek to the position of the current mipmap
				reader.BaseStream.Seek(startOffset + offsets[i], SeekOrigin.Begin);
				// Read the data into the buffer
				reader.Read(mipmap, 0, mipmap.Length);
				// Add the mipmap to the list
				mipmapList.Add(new Surface(this, new PaletteSurface(mipmap, palette, mipmapWidth, mipmapHeight, opaque, separateAlphaBitCount, false, true, true)));
			}

			this.mipmaps = mipmapList.ToArray();
		}

		private unsafe void LoadJpegMipmaps(BinaryReader reader, int startOffset, int width, int height, int* offsets, int* lengths, int mipMapCount)
		{
			// Read the JPEG header length from the current position in the stream and allocate a buffer…
			var jpegHeader = new byte[reader.ReadInt32()];
			// Read the JPEG header into the buffer.
			reader.Read(jpegHeader, 0, jpegHeader.Length);

			var mipmapList = new List<Surface>(16);
			int mipmapWidth = width;
			int mipmapHeight = height;

			// Read individual mipmaps
			for (int i = 0; i < mipMapCount && offsets[i] != 0 && lengths[i] != 0; i++, mipmapWidth = LowerMipmapDimension(mipmapWidth), mipmapHeight = LowerMipmapDimension(mipmapHeight))
			{
				// Create a new buffer for the current mipmap
				var mipmap = new byte[jpegHeader.Length + lengths[i]];
				// Copy the JPEG header at the beginning of the buffer
				Buffer.BlockCopy(jpegHeader, 0, mipmap, 0, jpegHeader.Length);
				// Seek to the position of the current mipmap
				reader.BaseStream.Seek(startOffset + offsets[i], SeekOrigin.Begin);
				// Read the data into the buffer
				reader.Read(mipmap, jpegHeader.Length, lengths[i]);
				// Add the mipmap to the list
				mipmapList.Add(new Surface(this, new JpegSurface(mipmap, mipmapWidth, mipmapHeight, true)));
			}

			mipmaps = mipmapList.ToArray();
		}

		private unsafe void LoadDxtMipmaps(BinaryReader reader, int startOffset, int width, int height, int* offsets, int* lengths, int mipMapCount, int alphaDepth)
		{
			var mipmapList = new List<Surface>(16);
			int mipmapWidth = width;
			int mipmapHeight = height;

			for (int i = 0; i < mipMapCount && offsets[i] != 0 && lengths[i] != 0; i++, mipmapWidth = LowerMipmapDimension(mipmapWidth), mipmapHeight = LowerMipmapDimension(mipmapHeight))
			{
				// Create a new buffer for the current mipmap
				var mipmap = new byte[lengths[i]];
				// Seek to the position of the current mipmap
				reader.BaseStream.Seek(startOffset + offsets[i], SeekOrigin.Begin);
				// Read the data into the buffer
				reader.Read(mipmap, 0, mipmap.Length);
				// Create the appropriate type of surface
				DxtSurface dxtSurface;
				switch (alphaDepth)
				{
					case 0:
					case 1: dxtSurface = new Dxt1Surface(mipmap, width, height, alphaDepth == 0, false, true); break;
					case 4: dxtSurface = new Dxt3Surface(mipmap, width, height, false, true); break;
					case 8: dxtSurface = new Dxt5Surface(mipmap, width, height, false, true); break;
					default: dxtSurface = null; break;
				}
				// Add the mipmap to the list
				mipmapList.Add(new Surface(this, dxtSurface));
			}

			this.mipmaps = mipmapList.ToArray();
		}

		private unsafe void LoadRawMipmaps(BinaryReader reader, int startOffset, int width, int height, int* offsets, int* lengths, int mipMapCount)
		{
			var mipmapList = new List<Surface>(16);
			int mipmapWidth = width;
			int mipmapHeight = height;
			
			for (int i = 0; i < mipMapCount && offsets[i] != 0 && lengths[i] != 0; i++, mipmapWidth = LowerMipmapDimension(mipmapWidth), mipmapHeight = LowerMipmapDimension(mipmapHeight))
			{
				// Create a new buffer for the current mipmap
				var mipmap = new byte[lengths[i]];
				// Seek to the position of the current mipmap
				reader.BaseStream.Seek(startOffset + offsets[i], SeekOrigin.Begin);
				// Read the data into the buffer
				reader.Read(mipmap, 0, mipmap.Length);
				// Add the mipmap to the list
				mipmapList.Add(new Surface(this, new ArgbSurface(mipmap, mipmapWidth, mipmapHeight, false, true)));
			}

			this.mipmaps = mipmapList.ToArray();
		}

		public SurfaceCollection Mipmaps { get { return mipmapCollection; } }

		public Surface FirstMipmap { get { return mipmaps[0]; } }

		public void Dispose()
		{
			foreach (Surface mipmap in mipmaps)
				if (mipmap != null)
					mipmap.Dispose();
			mipmaps = null;
		}
	}
}
