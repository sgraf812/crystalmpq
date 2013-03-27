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

namespace CrystalMpq.DataFormats
{
	public abstract class ClientDatabaseIndex<T>
		where T : struct
	{
		readonly string name;
		readonly ClientDatabase<T> database;

		internal ClientDatabaseIndex(string name, ClientDatabase<T> database)
		{
			if (name == null || name.Length == 0)
				throw new ArgumentNullException("name");
			if (database == null)
				throw new ArgumentNullException("database");
			this.name = name;
			this.database = database;
		}

		public string Name { get { return name; } }

		public ClientDatabase<T> Database { get { return database; } }

		public abstract Type KeyType { get; }

		public abstract T this[object key] { get; }
	}

	public sealed class ClientDatabaseIndex<TKey, TValue>
		: ClientDatabaseIndex<TValue>
		where TValue : struct
	{
		/// <summary>
		/// Returns the key of a given record.
		/// </summary>
		/// <param name="record">Record whose key is requested.</param>
		/// <returns>Returns the record key.</returns>
		/// <remarks>
		/// This is used for building the hash table, therefore this delegate must return a different value for each different record.
		/// If your records cannot be matched by key, then you have no need for this.
		/// </remarks>
		public delegate TKey RecordKeyGetter(TValue record);

		Dictionary<TKey, int> mappingDictionary;

		internal ClientDatabaseIndex(string name, ClientDatabase<TValue> database, RecordKeyGetter recordKeyGetter)
			: base(name, database)
		{
			if (recordKeyGetter == null)
				throw new ArgumentNullException("recordKeyGetter");

			mappingDictionary = new Dictionary<TKey, int>(database.Records.Count);
			for (int i = 0; i < database.Records.Count; i++)
				mappingDictionary.Add(recordKeyGetter(database.Records[i]), i);
		}

		public override TValue this[object key]
		{
			get
			{
				if (key is TKey)
					return Database.Records[mappingDictionary[(TKey)key]];
				else
					throw new KeyNotFoundException();
			}
		}
		public new TValue this[TKey key] { get { return Database.Records[mappingDictionary[key]]; } }
		public bool ContainsKey(TKey key) { return mappingDictionary.ContainsKey(key); }
		public bool TryGetValue(TKey key, out TValue value)
		{
			int index;

			if (mappingDictionary.TryGetValue(key, out index))
			{
				value = Database.Records[index];
				return true;
			}
			else
			{
				value = default(TValue);
				return false;
			}
		}

		public override Type KeyType { get { return typeof(TKey); } }
	}
}
