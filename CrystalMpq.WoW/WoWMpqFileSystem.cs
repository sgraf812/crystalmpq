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
using CrystalMpq;

namespace CrystalMpq.WoW
{
	/// <summary>Represents a file system composed of multiple MPQ archives.</summary>
	/// <remarks>When searching a file, the first archives are always searched first.</remarks>
	public sealed class WoWMpqFileSystem : IMpqFileSystem
	{
		public struct WoWArchiveEnumerator : IEnumerator<WoWArchive>
		{
			private readonly WoWArchive[] archiveArray;
			private int index;

			internal WoWArchiveEnumerator(WoWArchive[] archiveArray)
			{
				this.archiveArray = archiveArray;
				this.index = -1;
			}

			public void Dispose() { }

			public WoWArchive Current { get { return archiveArray[index]; } }

			object System.Collections.IEnumerator.Current { get { return Current; } }

			public bool MoveNext()
			{
				if (index + 1 >= archiveArray.Length) return false;

				index++;

				return true;
			}

			public void Reset() { index = -1; }
		}

		public sealed class WoWArchiveCollection : IList<WoWArchive>
		{
			private readonly WoWMpqFileSystem fileSystem;

			internal WoWArchiveCollection(WoWMpqFileSystem fileSystem)
			{
				this.fileSystem = fileSystem;
			}

			public WoWArchive this[int index] { get { return fileSystem.wowArchiveArray[index]; } }

			public int Count { get { return fileSystem.wowArchiveArray.Length; } }

			public int IndexOf(WoWArchive item) { return Array.IndexOf(fileSystem.wowArchiveArray, item); }
			public bool Contains(WoWArchive item) { return Array.IndexOf(fileSystem.wowArchiveArray, item) >= 0; }

			public void CopyTo(WoWArchive[] array, int arrayIndex) { fileSystem.wowArchiveArray.CopyTo(array, arrayIndex); }

			public WoWArchiveEnumerator GetEnumerator() { return new WoWArchiveEnumerator(fileSystem.wowArchiveArray); }

			bool ICollection<WoWArchive>.IsReadOnly { get { return true; } }

			WoWArchive IList<WoWArchive>.this[int index] { get { return fileSystem.wowArchiveArray[index]; } set { throw new NotSupportedException(); } }

			void ICollection<WoWArchive>.Add(WoWArchive item) { throw new NotSupportedException(); }
			void IList<WoWArchive>.Insert(int index, WoWArchive item) { throw new NotSupportedException(); }

			bool ICollection<WoWArchive>.Remove(WoWArchive item) { throw new NotSupportedException(); }
			void IList<WoWArchive>.RemoveAt(int index) { throw new NotSupportedException(); }
			void ICollection<WoWArchive>.Clear() { throw new NotSupportedException(); }

			IEnumerator<WoWArchive> IEnumerable<WoWArchive>.GetEnumerator() { return GetEnumerator(); }
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
		}

		private sealed class MpqArchiveCollection : IList<MpqArchive>
		{
			private readonly WoWMpqFileSystem fileSystem;

			internal MpqArchiveCollection(WoWMpqFileSystem fileSystem) { this.fileSystem = fileSystem; }

			public MpqArchive this[int index] { get { return fileSystem.wowArchiveArray[index].Archive; } }

			public int Count { get { return fileSystem.wowArchiveArray.Length; } }

			public int IndexOf(MpqArchive item) { return Array.FindIndex(fileSystem.wowArchiveArray, archiveEnty => archiveEnty.Archive == item); }
			public bool Contains(MpqArchive item) { return Array.Exists(fileSystem.wowArchiveArray, archiveEnty => archiveEnty.Archive == item); }

			public void CopyTo(MpqArchive[] array, int arrayIndex)
			{
				if (array.Length < fileSystem.wowArchiveArray.Length) throw new ArgumentException();
				if (array.Length < arrayIndex + fileSystem.wowArchiveArray.Length) throw new ArgumentOutOfRangeException();

				for (int i = 0; i < fileSystem.wowArchiveArray.Length; i++)
					array[arrayIndex + i] = fileSystem.wowArchiveArray[i].Archive;
			}

			public IEnumerator<MpqArchive> GetEnumerator() { foreach (var archiveEntry in fileSystem.wowArchiveArray) yield return archiveEntry.Archive; }

			bool ICollection<MpqArchive>.IsReadOnly { get { return true; } }

			MpqArchive IList<MpqArchive>.this[int index] { get { return fileSystem.wowArchiveArray[index].Archive; } set { throw new NotSupportedException(); } }

			void ICollection<MpqArchive>.Add(MpqArchive item) { throw new NotSupportedException(); }
			void IList<MpqArchive>.Insert(int index, MpqArchive item) { throw new NotSupportedException(); }

			bool ICollection<MpqArchive>.Remove(MpqArchive item) { throw new NotSupportedException(); }
			void IList<MpqArchive>.RemoveAt(int index) { throw new NotSupportedException(); }
			void ICollection<MpqArchive>.Clear() { throw new NotSupportedException(); }

			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }
		}

		private readonly string locale;
		private readonly string localePrefix;
		private readonly WoWArchive[] wowArchiveArray;
		private readonly WoWArchiveCollection wowArchiveCollection;
		private readonly MpqArchiveCollection mpqArchiveCollection;

		/// <summary>Initializes a new instance of the <see cref="MpqFileSystem"/> class.</summary>
		internal WoWMpqFileSystem(WoWArchive[] archiveArray, string locale)
		{

			this.locale = locale;
			this.localePrefix = locale + @"\";
			this.wowArchiveArray = archiveArray;
			this.wowArchiveCollection = new WoWArchiveCollection(this);
			this.mpqArchiveCollection = new MpqArchiveCollection(this);

			EventHandler<ResolveStreamEventArgs> baseFileResolver = ResolveBaseFile;
			foreach (var archiveEntry in archiveArray) archiveEntry.Archive.ResolveBaseFile += baseFileResolver;
		}

		public void Dispose()
		{
			foreach (var wowArchive in wowArchiveArray)
				wowArchive.Archive.Dispose();
		}

		public string Locale { get { return locale; } }

		/// <summary>Gets the collection of <see cref="WoWArchive"/>.</summary>
		/// <value>The archive list.</value>
		public WoWArchiveCollection Archives { get { return wowArchiveCollection; } }
		IList<MpqArchive> IMpqFileSystem.Archives { get { return mpqArchiveCollection; } }

		private void ResolveBaseFile(object sender, ResolveStreamEventArgs e)
		{
			var file = sender as MpqFile;

			if (file == null) throw new InvalidOperationException();

			WoWArchiveKind baseHint = WoWArchiveKind.Regular;
			string filename = null;

			foreach (var archiveEntry in wowArchiveArray)
			{
				if (filename == null)
				{
					if (archiveEntry.Archive == file.Archive)
						if ((archiveEntry.Kind & WoWArchiveKind.Global) == WoWArchiveKind.Global)
							if (file.Name.StartsWith(@"base\"))
							{
								baseHint = WoWArchiveKind.Base;
								filename = file.Name.Substring(5);
							}
							else
							{
								baseHint = WoWArchiveKind.LanguagePack;
								filename = file.Name.Substring(localePrefix.Length);
							}
						else
						{
							baseHint = archiveEntry.Kind & WoWArchiveKind.Global;
							filename = file.Name;
						}
					continue;
				}

				var foundFile = FindFile(archiveEntry, filename, baseHint);

				if (foundFile != null)
				{
					e.Stream = foundFile.Open();
					return;
				}
			}
		}

		public MpqFile FindFile(string filename)
		{
			foreach (var archive in wowArchiveArray)
			{
				var file = FindFile(archive, filename);

				if (file != null) return !file.IsDeleted ? file : null;
			}
			return null;
		}

		private MpqFile FindFile(WoWArchive archiveEntry, string filename, WoWArchiveKind baseHint = WoWArchiveKind.Regular)
		{
			if ((archiveEntry.Kind & WoWArchiveKind.Global) == WoWArchiveKind.Global)
			{
				string firstTry, secondTry;

				if ((baseHint & WoWArchiveKind.Global) == WoWArchiveKind.Base)
				{
					firstTry = @"base\";
					secondTry = localePrefix;
				}
				else
				{
					firstTry = localePrefix; // Always search in the locale first if not asked otherwise…
					secondTry = @"base\";
				}

				return archiveEntry.Archive.FindFile(firstTry + filename) ?? archiveEntry.Archive.FindFile(secondTry + filename);
			}
			else return archiveEntry.Archive.FindFile(filename);
		}
	}
}
