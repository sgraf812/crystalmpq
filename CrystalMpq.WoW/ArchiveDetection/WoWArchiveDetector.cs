using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CrystalMpq.WoW.ArchiveDetection
{
    /// <summary>
    /// Facade for WoWInstallationKind and archive detection.
    /// </summary>
    internal class WoWArchiveDetector
    {
        /// <summary>Path to the data associated with the instalaltion.</summary>
        private readonly string dataPath;
        /// <summary>Value representing the instllation kind, if recognized. Otherwise null. This field is primary for caching.</summary>
        private WoWInstallationKind? installationKind;

        /// <summary>
        /// Version handlers which do the actual work.
        /// </summary>
        private static readonly IDictionary<WoWInstallationKind, VersionHandler> versionHandlers = new Dictionary<WoWInstallationKind, VersionHandler>
        {
            { WoWInstallationKind.Pandarian, new PandarianVersionHandler() },
            { WoWInstallationKind.Cataclysmic, new CataclysmicVersionHandler() },
            { WoWInstallationKind.Classic, new ClassicVersionHandler() },
        };

        public WoWArchiveDetector(string dataPath)
        {
            this.dataPath = dataPath;
        }

        /// <summary>
        /// Determines the installation kind.
        /// </summary>
        /// <returns>The installation kind or null if not recognizable.</returns>
        /// <exception cref="FileNotFoundException">Couldn't determine installation kind.</exception>
        public WoWInstallationKind? DetermineInstallationKind()
        {
            if (installationKind.HasValue) return installationKind;
            foreach (var versionPair in versionHandlers)
            {
                if (!versionPair.Value.Recognizes(this.dataPath)) continue;

                installationKind = versionPair.Key;
                return installationKind;
            }
            return null;
        }

        /// <summary>
        /// Collects all archives belonging to the current installation.
        /// </summary>
        /// <returns>All archives belonging to the current installation.</returns>
        public IList<WoWArchiveInformation> CollectArchives()
        {
            if (!installationKind.HasValue && !DetermineInstallationKind().HasValue) throw new FileNotFoundException();

            return versionHandlers[this.installationKind.Value].CollectArchives(this.dataPath);
        }

        /// <summary>Finds the <see cref="WoWLanguagePack"/>s associated with this <see cref="WoWInstallation"/>.</summary>
        /// <param name="installation"><see cref="WoWInstallation"/> belonging to the language packs.</param>
        /// <remarks>Each <see cref="WoWLanguagePack"/> itself contains another list of archives.</remarks>
        /// <exception cref="FileNotFoundException">Couldn't determine installation kind.</exception>
        public IList<WoWLanguagePack> CollectLanguagePacks(WoWInstallation installation)
        {
            if (!installationKind.HasValue && !DetermineInstallationKind().HasValue) throw new FileNotFoundException();

            var languagePackList = new List<WoWLanguagePack>();

            foreach (string directoryPath in Directory.GetDirectories(dataPath))
            {
                string directoryName = Path.GetFileName(directoryPath);

                if (directoryName != null && directoryName.Length == 4)
                {
                    try
                    {
                        // Tries to create a CultureInfo object from th directory name
                        // WoW language packs use standard culture identifiers, meaning this should only fail if an invalid directory is found here
                        CultureInfo culture = CultureInfo.GetCultureInfo(directoryName.Substring(0, 2) + '-' + directoryName.Substring(2, 2));
                        var archives = versionHandlers[installationKind.Value].CollectLanguagePackArchives(directoryPath, directoryName);
                        languagePackList.Add(new WoWLanguagePack(installation, culture, archives));
                    }
                    catch (CultureNotFoundException) { } // Catches only CultureNotFoundException, which should only happen when there is no CultureInfo with that name
                }
            }

            return languagePackList;
        }
    }
}