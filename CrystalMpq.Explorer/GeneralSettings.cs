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
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms.Design;
using CrystalMpq.Explorer.Properties;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer
{
	[LocalizedDisplayName("GeneralSettings")]
	[LocalizedDescription("GeneralSettings")]
	sealed class GeneralSettings : IPluginSettings
	{
		Color viewerBackColor;
		string pluginsDirectory;
		List<ViewerAssociation> associations;

		public GeneralSettings()
		{
			associations = new List<ViewerAssociation>();
			Reset();
		}

		[Category("Appearance")]
		[LocalizedDisplayName("ViewerBackColor")]
		[LocalizedDescription("ViewerBackColor")]
		[DefaultValue(typeof(Color), "SlateGray")]
		public Color ViewerBackColor
		{
			get
			{
				return viewerBackColor;
			}
			set
			{
				viewerBackColor = value;
			}
		}

		[LocalizedDisplayName("PluginsDirectory")]
		[LocalizedDescription("PluginsDirectory")]
		[Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
		[DefaultValue("Plugins")]
		public string PluginsDirectory
		{
			get
			{
				return pluginsDirectory;
			}
			set
			{
				if (Directory.Exists(Path.Combine(Path.GetDirectoryName(typeof(GeneralSettings).Assembly.Location), value)))
					pluginsDirectory = value;
				else
					throw new DirectoryNotFoundException();
			}
		}

		public void Reset()
		{
			viewerBackColor = Settings.Default.ViewerBackColor;
			pluginsDirectory = Settings.Default.PluginsDirectory;
		}

		public void Save()
		{
			Settings.Default.ViewerBackColor = viewerBackColor;
			Settings.Default.PluginsDirectory = pluginsDirectory;
			Settings.Default.Save();
		}

		public override string ToString()
		{
			return null;
		}
	}
}
