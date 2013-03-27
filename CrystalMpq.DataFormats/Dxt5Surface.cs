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
	public sealed class Dxt5Surface : DxtSurface
	{
		public Dxt5Surface(int width, int height, bool alphaPremultiplied = false)
			: base(width, height, 8, alphaPremultiplied) { }

		public Dxt5Surface(byte[] rawData, int width, int height, bool alphaPremultiplied = false, bool shareBuffer = false)
			: base(rawData, width, height, 8, alphaPremultiplied, shareBuffer) { }

		[CLSCompliant(false)]
		public unsafe Dxt5Surface(byte* rawData, int width, int height, bool alphaPremultiplied = false)
			: base(rawData, width, height, 8, alphaPremultiplied) { }

		protected unsafe override void CopyToArgbInternal(SurfaceData surfaceData)
		{
			var colors = stackalloc ArgbColor[4];
			var alpha = stackalloc byte[8];

			fixed (byte* dataPointer = data)
			{
				var destinationRowPointer = (byte*)surfaceData.DataPointer;
				var sourcePointer = dataPointer;
				int rowBlockStride = surfaceData.Stride << 2;

				for (int i = Height; i > 0; i -= 4, destinationRowPointer += rowBlockStride)
				{
					var destinationPointer = destinationRowPointer;

					for (int j = Width; j > 0; j -= 4)
					{
						alpha[0] = *sourcePointer++;
						alpha[1] = *sourcePointer++;

						// The divisions here have been optimized with multiply/shift
						// Maybe there is way for some optimization in the multiplications, (using shifts and additions/substractions where applicable)
						// but for now I just hope the .NET JIT knows how to do those optimizations when they are possible…
						if (alpha[0] <= alpha[1])
						{
							alpha[2] = (byte)((4 * alpha[0] + alpha[1]) * 1639 >> 13);
							alpha[3] = (byte)((3 * alpha[0] + 2 * alpha[1]) * 1639 >> 13);
							alpha[4] = (byte)((2 * alpha[0] + 3 * alpha[1]) * 1639 >> 13);
							alpha[5] = (byte)((alpha[0] + 4 * alpha[1]) * 1639 >> 13);
							alpha[6] = 0;
							alpha[7] = 255;
						}
						else
						{
							alpha[2] = (byte)((6 * alpha[0] + alpha[1]) * 2341 >> 14);
							alpha[3] = (byte)((5 * alpha[0] + 2 * alpha[1]) * 2341 >> 14);
							alpha[4] = (byte)((4 * alpha[0] + 3 * alpha[1]) * 2341 >> 14);
							alpha[5] = (byte)((3 * alpha[0] + 4 * alpha[1]) * 2341 >> 14);
							alpha[6] = (byte)((2 * alpha[0] + 5 * alpha[1]) * 2341 >> 14);
							alpha[7] = (byte)((alpha[0] + 6 * alpha[1]) * 2341 >> 14);
						}

						// Store the block's alpha data in a 64 bit integer. This will probably be a bit slower on 32-bit CPUs, but who cares… :p
						ulong blockAlphaData = (ulong)(*sourcePointer++ | (uint)*sourcePointer++ << 8 | (uint)*sourcePointer++ << 16 | (uint)*sourcePointer++ << 24) | (ulong)(*sourcePointer++ | (uint)*sourcePointer++ << 8) << 32;

						ushort color0 = (ushort)(*sourcePointer++ | *sourcePointer++ << 8);
						ushort color1 = (ushort)(*sourcePointer++ | *sourcePointer++ << 8);

						colors[0] = new ArgbColor(color0);
						colors[1] = new ArgbColor(color1);

						ArgbColor.DxtMergeThirds(colors + 2, colors + 1, colors);
						ArgbColor.DxtMergeThirds(colors + 3, colors, colors + 1);

						// Handle the case where the surface's width is not a multiple of 4.
						int inverseBlockWidth = j > 4 ? 0 : 4 - j;

						var blockRowDestinationPointer = destinationPointer;

						for (int k = 4; k-- != 0; blockRowDestinationPointer += surfaceData.Stride)
						{
							byte rowData = *sourcePointer++;

							if (i + k < 4) continue; // Handle the case where the surface's height is not a multiple of 4.

							var blockDestinationPointer = (ArgbColor*)blockRowDestinationPointer;

							if (inverseBlockWidth != 0) blockAlphaData >>= 3 * inverseBlockWidth;

							// The small loop here has been unrolled, which shoudl be well worth it:
							//  - No loop variable is needed.
							//  - No useless shift and incrementation for the last step.
							//  - Only one conditional jump.
							//  - The loop will be executed very often, as the blocks are very small.
							switch (inverseBlockWidth)
							{
								case 0:
									ArgbColor.CopyWithAlpha(blockDestinationPointer++, &colors[rowData & 3], alpha[blockAlphaData & 7]);
									rowData >>= 2;
									blockAlphaData >>= 3;
									goto case 1;
								case 1:
									ArgbColor.CopyWithAlpha(blockDestinationPointer++, &colors[rowData & 3], alpha[blockAlphaData & 7]);
									rowData >>= 2;
									blockAlphaData >>= 3;
									goto case 2;
								case 2:
									ArgbColor.CopyWithAlpha(blockDestinationPointer++, &colors[rowData & 3], alpha[blockAlphaData & 7]);
									rowData >>= 2;
									blockAlphaData >>= 3;
									goto case 3;
								case 3:
									ArgbColor.CopyWithAlpha(blockDestinationPointer, &colors[rowData & 3], alpha[blockAlphaData & 7]);
									blockAlphaData >>= 3;
									break;
							}
						}

						destinationPointer += 4 * sizeof(uint); // Skip the 4 processed pixels
					}
				}
			}
		}
	}
}
