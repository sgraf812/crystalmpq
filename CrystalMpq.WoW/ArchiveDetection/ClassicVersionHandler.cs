using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CrystalMpq.WoW.ArchiveDetection
{
    class ClassicVersionHandler : VersionHandler
    {
        protected override IEnumerable<string> RelevantTopLevelElements
        {
            get
            {
                return new[]
                {
                    "common",
                    "expansion",
                    "lichking",
                    "patch"
                };
            }
        }

        protected override IEnumerable<string> RelevantLanguagePackElements
        {
            get
            {
                return new[]
                {
                    "backup",
			        "base",
			        "locale",
			        "speech",
			        "expansion-locale",
			        "expansion-speech",
			        "lichking-locale",
			        "lichking-speech",
			        "patch"
                };
            }
        }

        public override IList<WoWArchiveInformation> CollectArchives(string dataPath)
        {
            var archiveList = new List<WoWArchiveInformation>();

            foreach (string file in this.RelevantTopLevelElements)
            {
                archiveList.Add(new WoWArchiveInformation(FormatHyphenatedArchiveName(file), WoWArchiveKind.Base));

                for (int i = 1; ; i++)
                {
                    string archiveName = FormatHyphenatedArchiveName(file, i);
                    if (!File.Exists(Path.Combine(dataPath, archiveName))) break;

                    archiveList.Add(new WoWArchiveInformation(archiveName, WoWArchiveKind.Base));
                }
            }

            return archiveList;
        }

        public override IList<WoWArchiveInformation> CollectLanguagePackArchives(string localePath, string wowCultureId)
        {
            var archiveList = new List<WoWArchiveInformation>();

            foreach (string file in this.RelevantLanguagePackElements)
            {
                archiveList.Add(new WoWArchiveInformation(FormatHyphenatedArchiveName(file), WoWArchiveKind.LanguagePack));

                for (int i = 1; ; i++)
                {
                    string archiveName = FormatHyphenatedArchiveName(file, i);
                    if (!File.Exists(Path.Combine(localePath, archiveName))) break;

                    archiveList.Add(new WoWArchiveInformation(archiveName, WoWArchiveKind.LanguagePack));
                }
            }

            archiveList.Reverse();
            return archiveList;
        }
    }
}