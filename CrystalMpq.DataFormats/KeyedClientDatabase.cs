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
using System.Collections.Generic;

namespace CrystalMpq.DataFormats
{
	public sealed class KeyedClientDatabase<TKey, TValue> : ClientDatabase<TValue>
		where TValue : struct
	{
		ClientDatabaseIndex<TKey, TValue> index;

		public KeyedClientDatabase(Stream stream) : base(stream) { Initialize(); }
		public KeyedClientDatabase(Stream stream, int localeFieldIndex) : base(stream, localeFieldIndex) { Initialize(); }
		public KeyedClientDatabase(Stream stream, int localeFieldIndex, int localeFieldCount) : base(stream, localeFieldIndex, localeFieldCount) { Initialize(); }

		private void Initialize() { index = Indexes["Id"] as ClientDatabaseIndex<TKey, TValue>; }

		public TValue this[TKey key] { get { return index[key]; } }
		public bool ContainsKey(TKey key) { return index.ContainsKey(key); }
		public bool TryGetValue(TKey key, out TValue value) { return index.TryGetValue(key, out value); }
	}
}
