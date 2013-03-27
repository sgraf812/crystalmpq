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
using System.ComponentModel;

namespace CrystalMpq.Explorer
{
	sealed class LocalizedDescriptionAttribute : DescriptionAttribute
	{
		public LocalizedDescriptionAttribute(string description)
			: base(description)
		{
		}

		private string GetLocalizedString(string value)
		{
			string localizedString = Properties.Resources.ResourceManager.GetString("PropertyDescription" + value, Properties.Resources.Culture);

			if (localizedString == null)
				return value;
			else
				return localizedString;
		}

		public override string Description
		{
			get
			{
				return GetLocalizedString(DescriptionValue);
			}
		}
	}
}
