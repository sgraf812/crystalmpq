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
using System.Collections.ObjectModel;
using System.Text;
using System.IO;

namespace CrystalMpq.DataFormats
{
	/// <summary>
	/// Represents a client database (DBC) file in its raw data format.
	/// </summary>
	public sealed class RawClientDatabase
	{
		int recordCount, fieldCount, claimedFieldCount, recordSize;
		List<int[]> recordList;
		ReadOnlyCollection<int[]> recordCollection;
		Dictionary<int, string> stringDictonnary;

		public RawClientDatabase(Stream stream)
		{
			BinaryReader reader = new BinaryReader(stream);
			long startOffset = stream.Position;
			int stringBlockLength, stringStart = 0;
			int fieldSize;

#if DEBUG
			System.Diagnostics.Debug.WriteLine("Reading DBC File: ");
#endif
			uint header = reader.ReadUInt32();
			if (header != 0x43424457)
				throw new InvalidDataException();
			recordCount = reader.ReadInt32();
			claimedFieldCount = reader.ReadInt32();
			recordSize = reader.ReadInt32();
			stringBlockLength = reader.ReadInt32();
#if DEBUG
			System.Diagnostics.Debug.WriteLine(" " + recordCount.ToString() + " records of " + claimedFieldCount.ToString() + " fields each, with a record size of " + recordSize.ToString());
#endif

			if (recordSize % claimedFieldCount != 0)
				fieldCount = recordSize / 4;
			else
				fieldCount = claimedFieldCount;

			recordList = new List<int[]>(recordCount);
			recordCollection = new ReadOnlyCollection<int[]>(recordList);
			byte[] stringData = new byte[stringBlockLength];
			fieldSize = recordSize / fieldCount;

			for (int i = 0; i < recordCount; i++)
			{
				int[] fields = new int[fieldCount];
				for (int j = 0; j < fieldCount; j++)
					switch (fieldSize)
					{
						case 1:
							fields[j] = reader.ReadByte();
							break;
						case 2:
							fields[j] = reader.ReadUInt16();
							break;
						case 3:
							fields[j] = reader.ReadUInt16() | (reader.ReadByte() << 16);
							break;
						case 4:
							fields[j] = reader.ReadInt32();
							break;
					}
				recordList.Add(fields);
			}

			stream.Read(stringData, 0, stringBlockLength);

			stringDictonnary = new Dictionary<int, string>();

			for (int i = 0; i < stringData.Length; i++)
				if (stringData[i] == 0)
				{
					string text = Encoding.UTF8.GetString(stringData, stringStart, i - stringStart);

					stringDictonnary.Add(stringStart, text);
#if DEBUG
					System.Diagnostics.Debug.WriteLine(text);
#endif
					stringStart = i + 1;
				}
		}

		public ICollection<string> Strings
		{
			get
			{
				return stringDictonnary.Values;
			}
		}

		public bool HasStringWithOffset(int offset)
		{
			return stringDictonnary.ContainsKey(offset);
		}

		public string GetStringWithOffset(int offset)
		{
			return stringDictonnary[offset];
		}

		public unsafe static float GetFloat(int value)
		{
			return *((float*)&value);
		}

		public ReadOnlyCollection<int[]> Records
		{
			get
			{
				return recordCollection;
			}
		}

		public int FieldCount
		{
			get
			{
				return fieldCount;
			}
		}

		public int ClaimedFieldCount
		{
			get
			{
				return claimedFieldCount;
			}
		}
	}
}
