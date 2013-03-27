using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CrystalMpq.WoW.ArchiveDetection
{
    class CataclysmicVersionHandler : CataclysmAndUpwardsVersionHandler
    {
        protected override int ExpansionNumber { get { return 4; } }

        protected override IEnumerable<string> RelevantTopLevelElements
        {
            get
            {
                return new[]
                {
                    "sound",
                    "art",
                    "world",
                };
            }
        }
    }
}