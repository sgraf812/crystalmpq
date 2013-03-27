using System.Collections.Generic;

namespace CrystalMpq.WoW.ArchiveDetection
{
    class PandarianVersionHandler : CataclysmAndUpwardsVersionHandler
    {
        protected override int ExpansionNumber { get { return 5; } }

        protected override IEnumerable<string> RelevantTopLevelElements
        {
            get
            {
                // There is also base-Win.MPQ, which contains the platform specific binaries of WoW
                return new[]
                {
                    "alternate",
                    "interface",
                    "itemtexture",
                    "misc",
                    "model",
                    "sound",
                    "texture",
                    "world",
                };
            }
        }
    }
}