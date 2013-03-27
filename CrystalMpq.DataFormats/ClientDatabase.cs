#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Globalization;
#endregion

namespace CrystalMpq.DataFormats
{
	public abstract class ClientDatabase
	{
		#region Constants

		/// <summary>
		/// The locale field count used by default by the class.
		/// This value is provided as reference and is subject to change.
		/// </summary>
		public const int DefaultLocaleFieldCount = 16;
		/// <summary>
		/// The locale field index used by default by the class.
		/// This value is provided as reference and is subject to change.
		/// </summary>
		public const int DefaultLocaleFieldIndex = 0;

		#endregion

		#region Static Declarations

		protected static readonly Type[] delegateParameterTypeArray = new Type[] { typeof(byte[]), typeof(Dictionary<int, string>) };
		protected static readonly Type[] emptyParameterArray = new Type[0];
		// BitConverter methods
		protected static readonly MethodInfo toInt16 = typeof(BitConverter).GetMethod("ToInt16");
		protected static readonly MethodInfo toUInt16 = typeof(BitConverter).GetMethod("ToUInt16");
		protected static readonly MethodInfo toInt32 = typeof(BitConverter).GetMethod("ToInt32");
		protected static readonly MethodInfo toUInt32 = typeof(BitConverter).GetMethod("ToUInt32");
		protected static readonly MethodInfo toSingle = typeof(BitConverter).GetMethod("ToSingle");
		// Dictionary<int, string> methods
		protected static readonly MethodInfo getItem = typeof(Dictionary<int, string>).GetProperty("Item").GetGetMethod(false);

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="ClientDatabase"/> class.
		/// </summary>
		internal ClientDatabase() { }
	}

	/// <summary>
	/// Represents a structured client database (DBC) file in memory.
	/// </summary>
	/// <typeparam name="T">The type of the records for this database.</typeparam>
	/// <remarks>This class is optimized for efficient loading and handling of data.</remarks>
	public class ClientDatabase<T> : ClientDatabase
		where T : struct
	{
		#region DbcHeader Structure
		
		[StructLayout(LayoutKind.Sequential)]
		private struct DbcHeader
		{
			public int Signature;			// 'WDBC' 0x43424457
			public int RecordCount;			// Number of record in database
			public int FieldCount;			// Number of fields per record
			public int RecordSize;			// Size of one record
			public int StringBlockSize;		// Size of the string block

			public static int DefaultSignature = 0x43424457;
		}

		#endregion

		#region RecordCollection Class

		/// <summary>
		/// Represents a collection of records that can be individually accessed by index.
		/// </summary>
		public sealed class RecordCollection : IList<T>
		{
			ClientDatabase<T> database;

			internal RecordCollection(ClientDatabase<T> database) { this.database = database; }

			/// <summary>
			/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
			/// </summary>
			/// <value></value>
			/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
			/// </returns>
			public bool IsReadOnly { get { return true; } }
			/// <summary>
			/// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
			/// </summary>
			/// <value></value>
			/// <returns>
			/// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
			/// </returns>
			public int Count { get { return database.recordList.Count; } }
			/// <summary>
			/// Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1"/>.
			/// </summary>
			/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.IList`1"/>.</param>
			/// <returns>
			/// The index of <paramref name="item"/> if found in the list; otherwise, -1.
			/// </returns>
			public int IndexOf(T item) { return database.recordList.IndexOf(item); }
			/// <summary>
			/// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"/> contains a specific value.
			/// </summary>
			/// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"/>.</param>
			/// <returns>
			/// true if <paramref name="item"/> is found in the <see cref="T:System.Collections.Generic.ICollection`1"/>; otherwise, false.
			/// </returns>
			public bool Contains(T item) { return database.recordList.Contains(item); }
			/// <summary>
			/// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
			/// </summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
			/// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			/// 	<paramref name="array"/> is null.
			/// </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			/// 	<paramref name="arrayIndex"/> is less than 0.
			/// </exception>
			/// <exception cref="T:System.ArgumentException">
			/// 	<paramref name="array"/> is multidimensional.
			/// -or-
			/// <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
			/// -or-
			/// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.
			/// -or-
			/// Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.
			/// </exception>
			public void CopyTo(T[] array, int arrayIndex) { database.recordList.CopyTo(array, arrayIndex); }
			/// <summary>
			/// Gets the record at the specified index.
			/// </summary>
			/// <value>The record with the specified index.</value>
			public T this[int index]
			{
				get { return database.recordList[index]; }
			}

			T IList<T>.this[int index]
			{
				get { return database.recordList[index]; }
				set { throw new NotSupportedException(); }
			}
			void ICollection<T>.Add(T item) { throw new NotSupportedException(); }
			void IList<T>.Insert(int index, T item) { throw new NotSupportedException(); }
			void ICollection<T>.Clear() { throw new NotSupportedException(); }
			bool ICollection<T>.Remove(T item) { throw new NotSupportedException(); }
			void IList<T>.RemoveAt(int index) { throw new NotSupportedException(); }

			/// <summary>
			/// Returns an enumerator that iterates through the collection.
			/// </summary>
			/// <returns>
			/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
			/// </returns>
			public List<T>.Enumerator GetEnumerator() { return database.recordList.GetEnumerator(); }
			IEnumerator<T> IEnumerable<T>.GetEnumerator() { return database.recordList.GetEnumerator(); }
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return database.recordList.GetEnumerator(); }
		}

		#endregion

		#region IndexDictionary Class

		public sealed class IndexDictionary
			: IDictionary<string, ClientDatabaseIndex<T>>
		{
			ClientDatabase<T> database;

			internal IndexDictionary(ClientDatabase<T> database) { this.database = database; }

			/// <summary>
			/// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only.
			/// </summary>
			/// <value></value>
			/// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only; otherwise, false.
			/// </returns>
			public bool IsReadOnly { get { return true; } }
			/// <summary>
			/// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
			/// </summary>
			/// <value></value>
			/// <returns>
			/// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
			/// </returns>
			public int Count { get { return database.indexDictionary.Count; } }
			void ICollection<KeyValuePair<string, ClientDatabaseIndex<T>>>.CopyTo(KeyValuePair<string, ClientDatabaseIndex<T>>[] array, int arrayIndex) { (database.indexDictionary as ICollection<KeyValuePair<string, ClientDatabaseIndex<T>>>).CopyTo(array, arrayIndex); }
			bool ICollection<KeyValuePair<string, ClientDatabaseIndex<T>>>.Contains(KeyValuePair<string, ClientDatabaseIndex<T>> item) { return (database.indexDictionary as ICollection<KeyValuePair<string, ClientDatabaseIndex<T>>>).Contains(item); }
			/// <summary>
			/// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key.
			/// </summary>
			/// <param name="name">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.</param>
			/// <returns>
			/// true if the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the key; otherwise, false.
			/// </returns>
			/// <exception cref="T:System.ArgumentNullException">
			/// 	<paramref name="key"/> is null.
			/// </exception>
			public bool ContainsKey(string name) { return database.indexDictionary.ContainsKey(name); }
			/// <summary>
			/// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the names of the indexes.
			/// </summary>
			/// <returns>
			/// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the names of the indexes.
			/// </returns>
			public Dictionary<string, ClientDatabaseIndex<T>>.KeyCollection Names { get { return database.indexDictionary.Keys; } }
			ICollection<string> IDictionary<string, ClientDatabaseIndex<T>>.Keys { get { return database.indexDictionary.Keys; } }
			/// <summary>
			/// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the indexes.
			/// </summary>
			/// <returns>
			/// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the indexes.
			/// </returns>
			public Dictionary<string, ClientDatabaseIndex<T>>.ValueCollection Indexes { get { return database.indexDictionary.Values; } }
			ICollection<ClientDatabaseIndex<T>> IDictionary<string, ClientDatabaseIndex<T>>.Values { get { return database.indexDictionary.Values; } }
			/// <summary>
			/// Gets the index with the specified name.
			/// </summary>
			/// <param name="name">The name of the index to get.</param>
			/// <param name="index">When this method returns, the index with the specified name, if the name is found; otherwise, the default value for the type of the <paramref name="index"/> parameter. This parameter is passed uninitialized.</param>
			/// <returns>
			/// true if there is an index with the specified name; otherwise, false.
			/// </returns>
			/// <exception cref="T:System.ArgumentNullException">
			/// 	<paramref name="name"/> is null.
			/// </exception>
			public bool TryGetIndex(string name, out ClientDatabaseIndex<T> index) { return database.indexDictionary.TryGetValue(name, out index); }
			/// <summary>
			/// Gets the index with the specified name.
			/// </summary>
			/// <typeparam name="U">Key type for the requested index.</typeparam>
			/// <param name="name">The name of the index to get.</param>
			/// <param name="index">When this method returns, the index with the specified name, if the name is found; otherwise, the default value for the type of the <paramref name="index"/> parameter. This parameter is passed uninitialized.</param>
			/// <returns>
			/// true if there is an index with the specified name and specified key type; otherwise, false.
			/// </returns>
			/// <exception cref="T:System.ArgumentNullException">
			/// 	<paramref name="name"/> is null.
			/// </exception>
			public bool TryGetIndex<U>(string name, out ClientDatabaseIndex<U, T> index)
			{
				ClientDatabaseIndex<T> rawIndex;

				if (database.indexDictionary.TryGetValue(name, out rawIndex) && rawIndex is ClientDatabaseIndex<U, T>)
				{
					index = rawIndex as ClientDatabaseIndex<U, T>;
					return true;
				}
				else
				{
					index = null;
					return false;
				}
			}
			bool IDictionary<string, ClientDatabaseIndex<T>>.TryGetValue(string key, out ClientDatabaseIndex<T> value) { return database.indexDictionary.TryGetValue(key, out value); }

			/// <summary>
			/// Gets or sets the <see cref="CrystalMpq.DataFormats.ClientDatabaseIndex{T}"/> with the specified key.
			/// </summary>
			/// <value></value>
			public ClientDatabaseIndex<T> this[string key] { get { return database.indexDictionary[key]; } }
			ClientDatabaseIndex<T> IDictionary<string,ClientDatabaseIndex<T>>.this[string key]
			{
				get { return database.indexDictionary[key]; }
				set { throw new NotSupportedException(); }
			}

			void IDictionary<string,ClientDatabaseIndex<T>>.Add(string key, ClientDatabaseIndex<T> value) { throw new NotSupportedException(); }
			void ICollection<KeyValuePair<string,ClientDatabaseIndex<T>>>.Add(KeyValuePair<string, ClientDatabaseIndex<T>> item) { throw new NotSupportedException(); }
			void ICollection<KeyValuePair<string,ClientDatabaseIndex<T>>>.Clear() { throw new NotSupportedException(); }
			bool IDictionary<string,ClientDatabaseIndex<T>>.Remove(string key) { throw new NotSupportedException(); }
			bool ICollection<KeyValuePair<string,ClientDatabaseIndex<T>>>.Remove(KeyValuePair<string, ClientDatabaseIndex<T>> item) { throw new NotSupportedException(); }

			/// <summary>
			/// Returns an enumerator that iterates through the collection.
			/// </summary>
			/// <returns>
			/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
			/// </returns>
			public Dictionary<string, ClientDatabaseIndex<T>>.Enumerator GetEnumerator() { return database.indexDictionary.GetEnumerator(); }
			IEnumerator<KeyValuePair<string, ClientDatabaseIndex<T>>> IEnumerable<KeyValuePair<string, ClientDatabaseIndex<T>>>.GetEnumerator() { return database.indexDictionary.GetEnumerator(); }
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return database.indexDictionary.GetEnumerator(); }
		}

		#endregion

		#region Delegates

		private delegate T RecordReader(byte[] buffer, Dictionary<int, string> stringDictionnary);

		#endregion

		#region Fields

		private List<T> recordList;
		private RecordCollection recordCollection;
		private bool useIdMatching;
		private Dictionary<string, ClientDatabaseIndex<T>> indexDictionary;
		private IndexDictionary indexReadOnlyDictionary;
		private readonly int localeFieldCount,
			localeFieldIndex;

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new instance of the class Database using a stream as input.
		/// </summary>
		/// <param name="stream">Stream containing DBC data.</param>
		public ClientDatabase(Stream stream) : this(stream, DefaultLocaleFieldIndex, DefaultLocaleFieldCount) { }

		/// <summary>
		/// Creates a new instance of the class Database using a stream as input.
		/// </summary>
		/// <param name="stream">Stream containing DBC data.</param>
		/// <param name="createIndexes"><c>true</c> for auto-generation of indexes, <c>false</c> otherwise.</param>
		/// <remarks>
		/// If requested, indexes will be created based on metadata.
		/// If no index metadata is present for the specified record type, no index will be created.
		/// </remarks>
		public ClientDatabase(Stream stream, bool createIndexes) : this(stream, DefaultLocaleFieldIndex, DefaultLocaleFieldCount, createIndexes) { }

		/// <summary>
		/// Creates a new instance of the class Database using a stream as input.
		/// </summary>
		/// <param name="stream">Stream containing DBC data.</param>
		/// <param name="localeFieldIndex">Index of the field to use for localized strings.</param>
		public ClientDatabase(Stream stream, int localeFieldIndex) : this(stream, localeFieldIndex, DefaultLocaleFieldCount) { }

		/// <summary>
		/// Creates a new instance of the class Database using a stream as input.
		/// </summary>
		/// <param name="stream">Stream containing DBC data.</param>
		/// <param name="localeFieldIndex">Index of the field to use for localized strings.</param>
		/// <param name="localeFieldCount">Number of fields to use for localized strings.</param>
		public ClientDatabase(Stream stream, int localeFieldIndex, int localeFieldCount) : this(stream, localeFieldIndex, localeFieldCount, true) { }

		/// <summary>
		/// Creates a new instance of the class Database using a stream as input.
		/// </summary>
		/// <param name="stream">Stream containing DBC data.</param>
		/// <param name="localeFieldIndex">Index of the field to use for localized strings.</param>
		/// <param name="createIndexes"><c>true</c> for auto-generation of indexes, <c>false</c> otherwise.</param>
		/// <remarks>
		/// If requested, indexes will be created based on metadata.
		/// If no index metadata is present for the specified record type, no index will be created.
		/// </remarks>
		public ClientDatabase(Stream stream, int localeFieldIndex, bool createIndexes) : this(stream, localeFieldIndex, DefaultLocaleFieldCount, createIndexes) { }

		/// <summary>
		/// Creates a new instance of the class Database using a stream as input.
		/// </summary>
		/// <param name="stream">Stream containing DBC data.</param>
		/// <param name="localeFieldIndex">Index of the field to use for localized strings.</param>
		/// <param name="localeFieldCount">Number of fields to use for localized strings.</param>
		/// <param name="createIndexes"><c>true</c> for auto-generation of indexes, <c>false</c> otherwise.</param>
		/// <remarks>
		/// If requested, indexes will be created based on metadata.
		/// If no index metadata is present for the specified record type, no index will be created.
		/// </remarks>
		public ClientDatabase(Stream stream, int localeFieldIndex, int localeFieldCount, bool createIndexes)
		{
			DbcHeader header;

			if (localeFieldCount <= 0)
				throw new ArgumentOutOfRangeException("localeFieldCount");
			if (localeFieldIndex < 0 || localeFieldIndex >= localeFieldCount)
				throw new ArgumentOutOfRangeException("localeFieldIndex");
			if (!typeof(T).IsLayoutSequential)
				throw new InvalidCastException();

			this.localeFieldCount = localeFieldCount;
			this.localeFieldIndex = localeFieldIndex;

			using (var reader = new BinaryReader(stream, Encoding.UTF8))
			{
				header = ReadHeader(reader);

				if (header.Signature != DbcHeader.DefaultSignature
					|| header.RecordCount < 0
					|| header.FieldCount < 0
					|| header.RecordSize < 0
					|| header.RecordSize < header.FieldCount
					|| header.StringBlockSize < 0)
					throw new InvalidDataException();

				DynamicMethod readTMethod = BuildReadRecordMethod(header, localeFieldIndex, localeFieldCount);

				RecordReader readRecord = (RecordReader)readTMethod.CreateDelegate(typeof(RecordReader));

				recordList = new List<T>(header.RecordCount);
				recordCollection = new RecordCollection(this);

				ReadData(reader, header, readRecord);
			}

			this.indexDictionary = new Dictionary<string, ClientDatabaseIndex<T>>();
			this.indexReadOnlyDictionary = new ClientDatabase<T>.IndexDictionary(this);

			if (createIndexes)
				CreateIndexes();
		}

		#endregion

		#region Dynamic Method Generation

		/// <summary>
		/// Generates a specialized ReadT method from the type information and the header
		/// </summary>
		/// <param name="header">The database header</param>
		/// <param name="localeFieldIndex">Index of the field to use for localized strings.</param>
		/// <param name="localeFieldCount">Number of fields to use for localized strings.</param>
		/// <returns>A dynamic method capable of reading a T object from a byte array</returns>
		private static DynamicMethod BuildReadRecordMethod(DbcHeader header, int localeFieldIndex, int localeFieldCount)
		{
			int fieldOffset = 0,
				fieldCount = 0;

			// Get the field list
			var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);

			var method = new DynamicMethod("Read" + typeof(T).Name, typeof(T), delegateParameterTypeArray, typeof(T));
			var ilGenerator = method.GetILGenerator(1024);

			// Create code for structure declaration & creation
			ilGenerator.DeclareLocal(typeof(T), false); // The structure
			ilGenerator.Emit(OpCodes.Ldloca_S, 0);
			ilGenerator.Emit(OpCodes.Initobj, typeof(T));

			// Create code for field initialization
			foreach (FieldInfo field in fields)
			{
				ilGenerator.Emit(OpCodes.Ldloca_S, 0); // Push the structure on the stack

				// First handle the string type (common, though not as much as integer and floats)
				// Strings are stored in a list whichs requires usage of an extra parameter on the stack
				if (field.FieldType == typeof(string))
				{
					ilGenerator.Emit(OpCodes.Ldarg_1); // Push the list on the stack
					ilGenerator.Emit(OpCodes.Ldarg_0); // Push the buffer on the stack

					// The string can (an will often) be localized
					// In this case we have special handling
					if (field.GetCustomAttributes(typeof(LocalizedAttribute), false).Length > 0)
					{
						fieldCount += localeFieldCount + 1;
						ilGenerator.Emit(OpCodes.Ldc_I4, fieldOffset + (localeFieldIndex << 2)); // Push the computed field offset on the stack
						fieldOffset += (localeFieldCount + 1) << 2;
					}
					// Otherwise, there is only one string, and no need for complex lookups
					else
					{
						fieldCount++;
						ilGenerator.Emit(OpCodes.Ldc_I4, fieldOffset); // Push the field offset on the stack
						fieldOffset += 4;
					}
					// Get the index of the string
					ilGenerator.Emit(OpCodes.Call, toInt32);
					// Get the string from the list
					ilGenerator.Emit(OpCodes.Call, getItem); // List<String>.GetItem(i);
				}
				else
				{
					fieldCount++;

					ilGenerator.Emit(OpCodes.Ldarg_0); // Push the buffer on the stack
					ilGenerator.Emit(OpCodes.Ldc_I4, fieldOffset); // Push the field offset on the stack

					// 32 bit integer types (most common)
					if (field.FieldType == typeof(int))
					{
						ilGenerator.Emit(OpCodes.Call, toInt32);
						fieldOffset += 4;
					}
					else if (field.FieldType == typeof(uint))
					{
						ilGenerator.Emit(OpCodes.Call, toInt32);
						fieldOffset += 4;
					}
					else if (field.FieldType == typeof(bool))
					{
						ilGenerator.Emit(OpCodes.Call, toInt32);
						ilGenerator.Emit(OpCodes.Ldc_I4_0); // Push 0 on the stack
						ilGenerator.Emit(OpCodes.Ceq); // Compare value with 0 (=> push 1 if value = 0, 0 if value != 0)
						ilGenerator.Emit(OpCodes.Ldc_I4_0); // Push 0 on the stack
						ilGenerator.Emit(OpCodes.Ceq); // Compare previous result with 0 (=> push 1 if result was 0, or 0 if result was 1)
						fieldOffset += 4;
					}
					// 32 bit floating point type
					else if (field.FieldType == typeof(float))
					{
						ilGenerator.Emit(OpCodes.Call, toSingle);
						fieldOffset += 4;
					}
					// 16 bit integer types (don't see an use now, but who knows...)
					else if (field.FieldType == typeof(short))
					{
						ilGenerator.Emit(OpCodes.Call, toInt16);
						fieldOffset += 2;
					}
					else if (field.FieldType == typeof(ushort))
					{
						ilGenerator.Emit(OpCodes.Call, toUInt16);
						fieldOffset += 2;
					}
					// 8 bit integer types (rare)
					else if (field.FieldType == typeof(byte))
					{
						ilGenerator.Emit(OpCodes.Ldelem_U1);
						fieldOffset++;
					}
					else if (field.FieldType == typeof(sbyte))
					{
						ilGenerator.Emit(OpCodes.Ldelem_I1);
						fieldOffset++;
					}
					// Unknown type (this will cause bugs)
					else
					{
						// Don't forget to pop our 3 pushed values if we can't use them now
						// It will still cause a bug somewhere in the program, but at least the code will be valid.
						ilGenerator.Emit(OpCodes.Pop);
						ilGenerator.Emit(OpCodes.Pop);
						ilGenerator.Emit(OpCodes.Pop);
						fieldOffset += 4; // Assume default field size is 4
						continue;
					}
				}
				// Finally store the value in the field
				ilGenerator.Emit(OpCodes.Stfld, field);
			}

			// Create code for return value
			ilGenerator.Emit(OpCodes.Ldloc_0);
			ilGenerator.Emit(OpCodes.Ret);

			return method;
		}

		private static DynamicMethod BuildGetKeyMethod(string keyName, FieldInfo field)
		{
			var method = new DynamicMethod("Get" + typeof(T).Name + keyName, field.FieldType, new Type[] { typeof(T) }, typeof(T));
			var ilGenerator = method.GetILGenerator(1024);

			ilGenerator.Emit(OpCodes.Ldarga_S, 0);
			ilGenerator.Emit(OpCodes.Ldfld, field);
			ilGenerator.Emit(OpCodes.Ret);

			return method;
		}

		#endregion

		#region Data Reading

		private static DbcHeader ReadHeader(BinaryReader reader)
		{
			DbcHeader header;

			header.Signature = reader.ReadInt32();
			header.RecordCount = reader.ReadInt32();
			header.FieldCount = reader.ReadInt32();
			header.RecordSize = reader.ReadInt32();
			header.StringBlockSize = reader.ReadInt32();

			return header;
		}

		private void ReadData(BinaryReader reader, DbcHeader header, RecordReader readRecord)
		{
			Dictionary<int, string> stringDictionnary;
			Stream stream = reader.BaseStream;
			long dataOffset;
			int stringStart = 0;
			byte[] stringData, recordBuffer;

			// Save position and seek to string block
			dataOffset = stream.Position;
			stream.Seek(header.RecordCount * header.RecordSize, SeekOrigin.Current);
			// Read the strings
			stringData = reader.ReadBytes(header.StringBlockSize);
			stringDictionnary = new Dictionary<int, string>(30);
			for (int i = 0; i < stringData.Length; i++)
				if (stringData[i] == 0)
				{
					string text = (i - stringStart != 0) ? Encoding.UTF8.GetString(stringData, stringStart, i - stringStart) : string.Empty;

					stringDictionnary.Add(stringStart, text);
					stringStart = i + 1;
				}
			// Seek back to data
			stream.Seek(dataOffset, SeekOrigin.Begin);
			// Read the records one by one
			recordBuffer = new byte[header.RecordSize];
			for (int i = 0; i < header.RecordCount; i++)
			{
				int read = reader.Read(recordBuffer, 0, header.RecordSize);

				if (read < header.RecordSize)
					throw new Exception();

				recordList.Add(readRecord(recordBuffer, stringDictionnary));
			}
		}

		#endregion

		#region Data Writing

		private static void WriteHeader(BinaryWriter writer, DbcHeader header)
		{
			writer.Write(header.Signature);
			writer.Write(header.RecordCount);
			writer.Write(header.FieldCount);
			writer.Write(header.RecordSize);
			writer.Write(header.StringBlockSize);
		}

		#endregion

		#region Index Creation

		private void AddIndex(ClientDatabaseIndex<T> index) { indexDictionary.Add(index.Name, index); }

		private void CreateIndexes()
		{
			foreach (var field in typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance))
			{
				KeyAttribute[] attributes = (KeyAttribute[])field.GetCustomAttributes(typeof(KeyAttribute), true);

				if (attributes != null && attributes.Length == 1) // A value different from 0 or 1 would be incorrect... It's better to ignore this case.
				{
					var name = attributes[0].Name ?? field.Name;
					var getKeyMethod = BuildGetKeyMethod(name, field);
					var dictionaryTypes = new Type[] { field.FieldType, typeof(T)};

					AddIndex(Activator.CreateInstance(typeof(ClientDatabaseIndex<,>).MakeGenericType(dictionaryTypes), BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { name, this, getKeyMethod.CreateDelegate(typeof(ClientDatabaseIndex<,>.RecordKeyGetter).MakeGenericType(dictionaryTypes)) }, null) as ClientDatabaseIndex<T>);
				}
			}
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets a collection of records in this database
		/// </summary>
		public RecordCollection Records { get { return recordCollection; } }

		public IndexDictionary Indexes { get { return indexReadOnlyDictionary; } }

		#endregion
	}
}
