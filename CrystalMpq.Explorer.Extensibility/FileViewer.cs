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
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using CrystalMpq;

namespace CrystalMpq.Explorer.Extensibility
{
	/// <summary>Base class for implementig a FileViewer plugin.</summary>
	public /*abstract */class FileViewer : UserControl // Using abstract breaks the designer :(
	{
		private MpqFile file;
		private IHost host;
		private IPluginSettings pluginSettings;

		/// <summary>Initializes a new instance of the <see cref="FileViewer"/> class.</summary>
		/// <remarks>This will only be used by the Windows Forms designer.</remarks>
		private FileViewer() { }

		/// <summary>Initializes a new instance of the <see cref="FileViewer"/> class.</summary>
		/// <param name="host">The host which will be bound to this instance.</param>
		public FileViewer(IHost host) { this.host = host; }

		/// <summary>Gets the <see cref="System.Windows.Forms.MenuStrip"/> associated with this FileViewer, or <c>null</c> if there is none.</summary>
		public virtual MenuStrip Menu { get { return null; } }

		/// <summary>Gets the <see cref="System.Windows.Forms.ToolStrip"/> associated with this FileViewer, or <c>null</c> if there is none.</summary>
		public virtual ToolStrip MainToolStrip { get { return null; } }

		/// <summary>Gets the <see cref="System.Windows.Forms.StatusStrip"/> associated with this FileViewer, or <c>null</c> if there is none.</summary>
		public virtual StatusStrip StatusStrip { get { return null; } }

		/// <summary>Gets the object that can be used to change the settings of this plugin, or <c>null</c> if there is none.</summary>
		public IPluginSettings Settings
		{
			get
			{
				if (pluginSettings != null)
					return pluginSettings;
				else
					return pluginSettings = CreatePluginSettings();
			}
		}

		/// <summary>Called when creation of the PluginSettings object is requested.</summary>
		/// <returns>The PluginSettings object to use, or <c>null</c> if there is none.</returns>
		protected virtual IPluginSettings CreatePluginSettings() { return null; }

		/// <summary>Gets or sets the MPQFile object to be viewed in this FileViewer.</summary>
		public MpqFile File
		{
			get { return file; }
			set
			{
				if (value != file)
				{
					file = value;
					OnFileChanged();
				}
			}
		}

		/// <summary>Gets the associated host.</summary>
		protected IHost Host { get { return host; } }

		/// <summary>Called when the viewed file has changed.</summary>
		protected virtual void OnFileChanged() { }

		/// <summary>Called when the settings need to be updated.</summary>
		public virtual void ApplySettings() { }
	}
}
