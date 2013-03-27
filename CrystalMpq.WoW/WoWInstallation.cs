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
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CrystalMpq.WoW.ArchiveDetection;
using Microsoft.Win32;
using IOPath = System.IO.Path;

namespace CrystalMpq.WoW
{
	/// <summary>Represents a WoW installation on the machine.</summary>
	public sealed class WoWInstallation
	{
		#region LanguagePackCollection Class

		/// <summary>Represents a collection of <see cref="WoWLanguagePack"/> associated with a <see cref="WoWInstallation"/>.</summary>
		public sealed class LanguagePackCollection : IList<WoWLanguagePack>
		{
			private WoWInstallation wowInstallation;

			internal LanguagePackCollection(WoWInstallation wowInstallation) { this.wowInstallation = wowInstallation; }

			/// <summary>Gets or sets the <see cref="CrystalMpq.WoW.WoWLanguagePack"/> at the specified index.</summary>
			/// <value></value>
			public WoWLanguagePack this[int index]
			{
				get { return wowInstallation.languagePackArray[index]; }
				set { throw new NotSupportedException(); }
			}

			/// <summary>Gets the number of elements contained in the <see cref="LanguagePackCollection"/>.</summary>
			/// <value>The number of elements contained in the <see cref="LanguagePackCollection"/>.</value>
			public int Count { get { return wowInstallation.languagePackArray.Count; } }
			/// <summary>Gets a value indicating whether this instance is read only.</summary>
			/// <remarks><see cref="LanguagePackCollection"/> will always be read-only.</remarks>
			/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
			public bool IsReadOnly { get { return true; } }

			/// <summary>Determines the index of a specific item in the <see cref="LanguagePackCollection"/>.</summary>
			/// <param name="item">The object to locate in the <see cref="LanguagePackCollection"/>.</param>
			/// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
			public int IndexOf(WoWLanguagePack item) { return ((IList<WoWLanguagePack>)wowInstallation.languagePackArray).IndexOf(item); }
			/// <summary>Determines whether the <see cref="LanguagePackCollection"/> contains a specific value.</summary>
			/// <param name="item">The object to locate in the <see cref="LanguagePackCollection"/>.</param>
			/// <returns><c>true</c> if <paramref name="item"/> is found in the <see cref="LanguagePackCollection"/>; otherwise, <c>false</c>.</returns>
			public bool Contains(WoWLanguagePack item) { return ((IList<WoWLanguagePack>)wowInstallation.languagePackArray).Contains(item); }
			/// <summary>Copies the elements of the <see cref="LanguagePackCollection"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.</summary>
			/// <param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="LanguagePackCollection"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
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
			/// </exception>
			public void CopyTo(WoWLanguagePack[] array, int arrayIndex) { wowInstallation.languagePackArray.CopyTo(array, arrayIndex); }

			void IList<WoWLanguagePack>.Insert(int index, WoWLanguagePack item) { throw new NotSupportedException(); }
			void IList<WoWLanguagePack>.RemoveAt(int index) { throw new NotSupportedException(); }
			void ICollection<WoWLanguagePack>.Add(WoWLanguagePack item) { throw new NotSupportedException(); }
			bool ICollection<WoWLanguagePack>.Remove(WoWLanguagePack item) { throw new NotSupportedException(); }
			void ICollection<WoWLanguagePack>.Clear() { throw new NotSupportedException(); }

			/// <summary>Returns an enumerator that iterates through the collection.</summary>
			/// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.</returns>
			public IEnumerator<WoWLanguagePack> GetEnumerator() { return ((IList<WoWLanguagePack>)wowInstallation.languagePackArray).GetEnumerator(); }
			System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return ((System.Collections.IEnumerable)wowInstallation.languagePackArray).GetEnumerator(); }
		}

		#endregion

		/// <summary>Path to the instalaltion.</summary>
		private string wowPath;
		/// <summary>Path to the data associated with the instalaltion.</summary>
		private string dataPath;
		/// <summary>Array of archives associated with the instalaltion.</summary>
		/// <remarks>The archives are detected based on their filename, during the instantiation of the class.</remarks>
		private IList<WoWArchiveInformation> archiveArray;
		/// <summary>Collection of archives associated with the instalaltion.</summary>
		/// <remarks>This is a wrapper around <seealso cref="F:archiveArray"/>.</remarks>
		private ReadOnlyCollection<WoWArchiveInformation> archiveCollection;
		/// <summary>Array of <see cref="WoWLanguagePack"/> associated with the installation.</summary>
        private IList<WoWLanguagePack> languagePackArray;
		/// <summary>Collection of <see cref="WoWLanguagePack"/> associated with the installation.</summary>
		/// <remarks>This is a wrapper around <seealso cref="F:languagePackArray"/>.</remarks>
		private LanguagePackCollection languagePackCollection;
		/// <summary>Value representing the instllation kind.</summary>
		private WoWInstallationKind installationKind;

		/// <summary>Initializes a new instance of the <see cref="WoWInstallation"/> class.</summary>
		/// <param name="path">The installation path.</param>
		/// <exception cref="DirectoryNotFoundException"><paramref name="path"/> does not exist, or does not contain a directory named <c>Data</c>.</exception>
		/// <exception cref="FileNotFoundException">At least one of the required archives has not been found in the specified directory.</exception>
		private WoWInstallation(string path)
		{
			if (!Directory.Exists(this.wowPath = path))
				throw new DirectoryNotFoundException();

			if (!Directory.Exists(this.dataPath = System.IO.Path.Combine(path, "Data")))
				throw new DirectoryNotFoundException();

		    var detector = new WoWArchiveDetector(this.dataPath);

            archiveArray = detector.CollectArchives();
		    installationKind = detector.DetermineInstallationKind().Value;
            archiveCollection = new ReadOnlyCollection<WoWArchiveInformation>(archiveArray);

		    languagePackArray = detector.CollectLanguagePacks(this);
		    languagePackCollection = new LanguagePackCollection(this);
		}

		/// <summary>Tries to locate the standard WoW installation.</summary>
		/// <returns>A <see cref="WoWInstallation"/> instance representing the standard WoW installation, if found.</returns>
		public static WoWInstallation Find()
		{
			RegistryKey wowKey = null;
			string path;
			try
			{
				if (Environment.OSVersion.Platform == PlatformID.Win32NT)
				{
					if ((wowKey = FindWowRegistryKey()) != null)
						path = (string)wowKey.GetValue("InstallPath");
					else
						throw new FileNotFoundException();
				}
				else if (Environment.OSVersion.Platform == PlatformID.MacOSX || Environment.OSVersion.Platform == PlatformID.Unix)
					path = @"/Applications/World of Warcraft";
				else
					throw new PlatformNotSupportedException("Automatic WoW Installation Discovery Unsupported for your platform.");
			}
			finally
			{
				if (wowKey != null)
					wowKey.Close();
			}
			return new WoWInstallation(path);
		}

	    private static RegistryKey FindWowRegistryKey()
	    {
	        return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Blizzard Entertainment\World of Warcraft") ??
	               Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\Blizzard Entertainment\World of Warcraft");
	    }

	    #region Archive Detection Functions

	    #endregion

		/// <summary>Creates a MpqFileSystem using the specified language pack.</summary>
		/// <param name="languagePack">The language pack.</param>
		/// <param name="shouldParseListFiles">if set to <c>true</c> the list files will be parsed.</param>
		/// <returns>The newly created MpqFileSystem.</returns>
		public WoWMpqFileSystem CreateFileSystem(WoWLanguagePack languagePack, bool shouldParseListFiles)
		{
			return CreateFileSystem(languagePack, true, shouldParseListFiles);
		}

		/// <summary>Creates a MpqFileSystem using the specified language pack.</summary>
		/// <param name="languagePack">The language pack.</param>
		/// <param name="enforceCultureCheck">if set to <c>true</c> the culture checks will be enforced.</param>
		/// <param name="shouldParseListFiles">if set to <c>true</c> the list files will be parsed.</param>
		/// <returns>The newly created MpqFileSystem.</returns>
		public WoWMpqFileSystem CreateFileSystem(WoWLanguagePack languagePack, bool enforceCultureCheck, bool shouldParseListFiles)
		{
			if (languagePack == null)
				throw new ArgumentNullException("languagePack");
			if (languagePack.WoWInstallation != this)
				throw new ArgumentException();
#pragma warning disable 618
			if (enforceCultureCheck && installationKind == WoW.WoWInstallationKind.Classic && languagePack.DatabaseFieldIndex < 0)
#pragma warning restore 618
				throw new CultureNotSupportedException(languagePack.Culture);

			// Process the archive list
			var archiveInformationList = new List<WoWArchiveInformation>();
			archiveInformationList.AddRange(archiveArray);
			archiveInformationList.AddRange(languagePack.Archives);
			archiveInformationList.Sort(WoWArchiveInformationComparer.Default);
			archiveInformationList.Reverse();

			// Load the various archives and create the file system
			var wowArchiveArray = new WoWArchive[archiveInformationList.Count];

			for (int i = 0; i < wowArchiveArray.Length; i++)
			{
				var archiveInformation = archiveInformationList[i];

			    var path = (archiveInformation.Kind & WoWArchiveKind.Global) == WoWArchiveKind.LanguagePack 
                    ? languagePack.Path : DataPath;
			    wowArchiveArray[i] = new WoWArchive(new MpqArchive(IOPath.Combine(path, archiveInformation.Filename), shouldParseListFiles), archiveInformation.Kind);
			}

			return new WoWMpqFileSystem(wowArchiveArray, IOPath.GetFileName(languagePack.Path));
		}

		/// <summary>Gets the path of this WoW installation.</summary>
		public string Path { get { return wowPath; } }
		/// <summary>Gets the path to the data associated with the installation.</summary>
		public string DataPath { get { return dataPath; } }
		/// <summary>Gets a collection of language packs associated with the installation.</summary>
		public LanguagePackCollection LanguagePacks { get { return languagePackCollection; } }
		/// <summary>Gets a collection of string containing the names of the archives detected as part of the installation.</summary>
		public ReadOnlyCollection<WoWArchiveInformation> Archives { get { return archiveCollection; } }
		/// <summary>Gets a value representing the installation kind. </summary>
		/// <remarks>This value is useful to differenciate classic installations from newer installations (Cataclysm or newer).</remarks>
		/// <value>The kind of the installation.</value>
		public WoWInstallationKind InstallationKind { get { return installationKind; } }
	}
}
