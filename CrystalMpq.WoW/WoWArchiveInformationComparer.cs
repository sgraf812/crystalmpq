using System.Collections.Generic;

namespace CrystalMpq.WoW
{
    internal sealed class WoWArchiveInformationComparer : IComparer<WoWArchiveInformation>
    {
        public static readonly WoWArchiveInformationComparer Default = new WoWArchiveInformationComparer();

        public int Compare(WoWArchiveInformation x, WoWArchiveInformation y)
        {
            int delta = x.PatchNumber - y.PatchNumber;

            if (delta == 0) return (x.Kind & WoWArchiveKind.Global) - (y.Kind & WoWArchiveKind.Global);
            else return delta;
        }
    }
}