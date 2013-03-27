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
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer
{
	sealed partial class OptionsForm : Form
	{
		#region SettingsTypeDescriptor Class

		sealed class SettingsTypeDescriptor : CustomTypeDescriptor
		{
			#region PluginSettingsPropertyDescriptor Class

			sealed class PluginSettingsPropertyDescriptor : PropertyDescriptor
			{
				IPluginSettings pluginSettings;

				public PluginSettingsPropertyDescriptor(IPluginSettings pluginSettings)
					: base(pluginSettings.GetType().Name, BuildAttributeArray(pluginSettings))
				{
					this.pluginSettings = pluginSettings;
					
				}

				private static Attribute[] BuildAttributeArray(IPluginSettings pluginSettings)
				{
					AttributeCollection attributeCollection = TypeDescriptor.GetAttributes(pluginSettings);
					Attribute[] attributeArray = new Attribute[attributeCollection.Count];

					for (int i = 0; i < attributeArray.Length; i++)
						attributeArray[i] = attributeCollection[i];

					return attributeArray;
				}

				public override TypeConverter Converter
				{
					get
					{
						return new ExpandableObjectConverter();
					}
				}

				public override bool CanResetValue(object component)
				{
					return false;
				}

				public override Type ComponentType
				{
					get { return typeof(SettingsTypeDescriptor); }
				}

				public override object GetValue(object component)
				{
					return pluginSettings;
				}

				public override bool IsReadOnly { get { return true; } }

				public override Type PropertyType { get { return typeof(IPluginSettings); } }

				public override void ResetValue(object component)
				{
					// We can not modify the value...
				}

				public override void SetValue(object component, object value)
				{
					// We can not modify the value...
				}

				public override bool ShouldSerializeValue(object component)
				{
					return false;
				}
			}

			#endregion

			OptionsForm optionsForm;
			List<PropertyDescriptor> propertyCache;

			public SettingsTypeDescriptor(OptionsForm optionsForm)
			{
				this.optionsForm = optionsForm;
				this.propertyCache = new List<PropertyDescriptor>();
			}

			public override string GetClassName()
			{
				return typeof(SettingsTypeDescriptor).AssemblyQualifiedName;
			}

			public override string GetComponentName()
			{
				return "Settings";
			}

			public void RebuildPropertyCache()
			{
				propertyCache.Clear();

				if (optionsForm.settingsList != null)
					foreach (IPluginSettings pluginSettings in optionsForm.settingsList)
						propertyCache.Add(new PluginSettingsPropertyDescriptor(pluginSettings));
			}

			public override PropertyDescriptorCollection GetProperties()
			{
				return new PropertyDescriptorCollection(propertyCache.ToArray());
			}

			public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
			{
				return new PropertyDescriptorCollection(propertyCache.ToArray());
			}
		}

		#endregion

		IList<IPluginSettings> settingsList;
		SettingsTypeDescriptor settings;

		public OptionsForm()
		{
			InitializeComponent();
			settings = new SettingsTypeDescriptor(this);
			propertyGrid.SelectedObject = settings;
		}

		public IList<IPluginSettings> Settings
		{
			get
			{
				return settingsList;
			}
			set
			{
				settingsList = value;
				settings.RebuildPropertyCache();
				propertyGrid.Refresh();
				propertyGrid.ExpandAllGridItems();
			}
		}
	}
}