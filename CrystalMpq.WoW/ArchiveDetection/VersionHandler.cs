using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrystalMpq.WoW.ArchiveDetection
{
    abstract class VersionHandler
    {
        public bool Recognizes(string dataPath)
        {
            foreach (var file in this.RelevantTopLevelElements)
            {
                string archiveName = FormatHyphenatedArchiveName(file);
                if (!File.Exists(Path.Combine(dataPath, archiveName))) return false;
            }
            return true;
        }

        protected static string FormatHyphenatedArchiveName(params object[] parts)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            sb.Append(parts[i++]);
            while (i < parts.Length)
            {
                sb.Append("-")
                    .Append(parts[i++]);
            }
            return sb.Append(".MPQ").ToString();
        }

        protected static int DetectArchiveNumber(string name)
        {
            int extensionIndex = name.LastIndexOf(".mpq", StringComparison.OrdinalIgnoreCase);
            int index = extensionIndex;

            while (--index >= 0)
            {
                char c = name[index];

                if (c < '0' || c > '9') { index++; break; } // The incrementation has to be done here
            }

            return Int32.Parse(name.Substring(index, extensionIndex - index));
        }

        protected abstract IEnumerable<string> RelevantTopLevelElements { get; }

        protected abstract IEnumerable<string> RelevantLanguagePackElements { get; } 

        public abstract IList<WoWArchiveInformation> CollectArchives(string dataPath);

        public abstract IList<WoWArchiveInformation> CollectLanguagePackArchives(string localePath, string wowCultureId);
    }
}