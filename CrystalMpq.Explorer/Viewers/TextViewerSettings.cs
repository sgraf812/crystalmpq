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
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using CrystalMpq.Explorer;
using CrystalMpq.Explorer.Properties;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer.Viewers
{
	[LocalizedDisplayName("TextViewerSettings")]
	[LocalizedDescription("TextViewerSettings")]
	internal sealed class TextViewerSettings : IPluginSettings
	{
		private Encoding defaultEncoding;

		public TextViewerSettings() { Reset(); }

		[Category("Behavior")]
		[LocalizedDisplayName("DefaultEncoding")]
		[LocalizedDescription("DefaultEncoding")]
		[TypeConverter(typeof(EncodingConverter))]
		public Encoding DefaultEncoding
		{
			get { return defaultEncoding; }
			set
			{
				if (value == null)
					defaultEncoding = Encoding.UTF8;
				else if (value != defaultEncoding)
					defaultEncoding = value;
			}
		}

		public bool ShouldSerializeDefaultEncoding() { return defaultEncoding.WebName != "utf-8"; }

		public void ResetDefaultEncoding() { defaultEncoding = Encoding.UTF8; }

		public void Reset()
		{
			try { defaultEncoding = Encoding.GetEncoding(Settings.Default.TextViewerDefaultEncoding); }
			catch { defaultEncoding = Encoding.UTF8; }
		}

		public void Save()
		{
			Settings.Default.TextViewerDefaultEncoding = defaultEncoding.WebName;
			Settings.Default.Save();
		}

		public override string ToString() { return null; }
	}
}
