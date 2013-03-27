using System;
using System.Collections.Generic;
using System.Text;

namespace CrystalMpq.WoW
{
	/// <summary>Represents the kind of installation.</summary>
	public enum WoWInstallationKind
	{
		/// <summary>The installation is a classic World of Warcraft installation.</summary>
		Classic,
		/// <summary>The installation is a World of Warcraft installation from Cataclysm</summary>
        Cataclysmic,
        /// <summary>The installation is a World of Warcraft installation from Mists Of Pandaria or newer.</summary>
        Pandarian,
	}
}
