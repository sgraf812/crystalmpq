using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CrystalMpq.WoW.ArchiveDetection
{
    internal abstract class CataclysmAndUpwardsVersionHandler : VersionHandler
    {
        /// <summary>
        /// Expansion archive name format string.
        /// </summary>
        private const string expansionArchivePrefix = "expansion{0}";
        /// <summary>Format of the filename for cataclysm patch archives.</summary>
        private const string globalPatchArchivePattern = "wow-update-?????.MPQ";
        /// <summary>Format of the filename for cataclysm patch archives.</summary>
        private const string basePatchArchivePattern = "wow-update-base-?????.MPQ";
        /// <summary>Format of the filename for cataclysm patch archives.</summary>
        private const string patchArchivePattern = "wow-update-{0}-?????.MPQ";

        protected abstract int ExpansionNumber { get; }

        protected override IEnumerable<string> RelevantLanguagePackElements
        {
            get
            {
                return new[]
                {
                    "backup",
                    "base",
                    "locale",
                    "speech"
                };
            }
        }

        public override IList<WoWArchiveInformation> CollectArchives(string dataPath)
        {
            var archiveList = new List<WoWArchiveInformation>();

            foreach (string file in RelevantTopLevelElements)
            {
                archiveList.Add(new WoWArchiveInformation(FormatHyphenatedArchiveName(file), WoWArchiveKind.Base));
            }

            archiveList.AddRange(ExpansionArchivesUpTo(ExpansionNumber, dataPath, FormatHyphenatedArchiveName(expansionArchivePrefix), WoWArchiveKind.Base));

            var globalPatches = GetPatchArchives(dataPath, WoWArchiveKind.Global | WoWArchiveKind.Patch, globalPatchArchivePattern);
            var basePatches = GetPatchArchives(dataPath, WoWArchiveKind.Base | WoWArchiveKind.Patch, basePatchArchivePattern);
            var patches = new List<WoWArchiveInformation>(globalPatches);
            patches.AddRange(basePatches);

            patches.Sort(WoWArchiveInformationComparer.Default);

            archiveList.AddRange(patches);

            return archiveList;
        }

        public override IList<WoWArchiveInformation> CollectLanguagePackArchives(string localePath, string wowCultureId)
        {
            var archiveList = new List<WoWArchiveInformation>();

            foreach (string file in RelevantLanguagePackElements)
            {
                // {locale,speech,etc.}-{enGB,deDE,etc.}.MPQ
                string archiveName = FormatHyphenatedArchiveName(file, wowCultureId);
                if (File.Exists(Path.Combine(localePath, archiveName))) 
                    archiveList.Add(new WoWArchiveInformation(archiveName, WoWArchiveKind.LanguagePack));
            }

            foreach (string file in RelevantLanguagePackElements)
            {
                // expansion{0}-{locale,speech,etc.}-{enGB,deDE,etc.}.MPQ
                string archiveFormat = FormatHyphenatedArchiveName(expansionArchivePrefix, file, wowCultureId);
                archiveList.AddRange(ExpansionArchivesUpTo(ExpansionNumber, localePath, archiveFormat, WoWArchiveKind.LanguagePack));
            }

            var patchFilePattern = string.Format(CultureInfo.InvariantCulture, patchArchivePattern, wowCultureId);
            archiveList.AddRange(
                GetPatchArchives(localePath, WoWArchiveKind.LanguagePack | WoWArchiveKind.Patch, patchFilePattern));

            return archiveList;
        }

        /// <summary>
        /// Provides archive information for all current expansion archives.
        /// </summary>
        /// <param name="currentExpansion">The number of the current expansion, i.e. 4 for Cataclysm.</param>
        /// <param name="dataPath">Path to WoW's Data folder.</param>
        /// <param name="archiveFormat">Archive file format string used to expand the file name.</param>
        /// <param name="archiveKind"> </param>
        /// <returns>Archive information of all expansion archives iff there were currentExpansion-1 matching files, or an empty enumerable.</returns>
        protected static IEnumerable<WoWArchiveInformation> ExpansionArchivesUpTo(int currentExpansion, string dataPath, string archiveFormat, WoWArchiveKind archiveKind)
        {
            var archives = new List<WoWArchiveInformation>(currentExpansion - 1);
            for (int i = 1; i < currentExpansion; i++)
            {
                string archiveName = String.Format(CultureInfo.InvariantCulture, archiveFormat, i);
                string path = Path.Combine(dataPath, archiveName);
                if (!File.Exists(path)) return new WoWArchiveInformation[0];
                archives.Add(new WoWArchiveInformation(archiveName, archiveKind));
            }
            return archives;
        }

        protected static IEnumerable<WoWArchiveInformation> GetPatchArchives(string dataPath, WoWArchiveKind archiveKind, string patchFilePattern)
        {
            var patchArchives = Directory.GetFiles(dataPath, patchFilePattern, SearchOption.TopDirectoryOnly);

            foreach (var patchArchive in patchArchives)
            {
                string archiveName = Path.GetFileName(patchArchive);
                yield return new WoWArchiveInformation(archiveName, archiveKind, DetectArchiveNumber(archiveName));
            }
        }
    }
}