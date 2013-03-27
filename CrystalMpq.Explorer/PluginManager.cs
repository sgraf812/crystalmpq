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
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using CrystalMpq.Explorer.Properties;

namespace CrystalMpq.Explorer
{
	// TODO: Make a better plugin loading exception mechanism
	// TODO: Migrate to MEF ?
	internal sealed class PluginManager
	{
		private static List<Assembly> assemblyList;

		static PluginManager() { assemblyList = new List<Assembly>(); }

		public static void LoadPluginAssemblies()
		{
			string assemblyDirectory, pluginsDirectory;

			assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			pluginsDirectory = Path.Combine(assemblyDirectory, Settings.Default.PluginsDirectory);

			LoadDirectoryAssemblies(pluginsDirectory);
		}

		public static T[] LoadPlugins<T>(Type[] parameterTypes, object[] parameters)
		{
			var loadedPlugins = new List<T>();

			for (int i = 0; i < assemblyList.Count; i++)
			{
				var assemblyTypes = assemblyList[i].GetExportedTypes();

				for (int j = 0; j < assemblyTypes.Length; j++)
				{
					var type = assemblyTypes[j];

					try
					{
						if (typeof(T).IsAssignableFrom(type))
						{
							var constructor = type.GetConstructor(parameterTypes);

							if (constructor != null)
								try { loadedPlugins.Add((T)constructor.Invoke(parameters)); }
								catch { }
						}
					}
					catch (TypeLoadException) { }
				}
			}

			return loadedPlugins.ToArray();
		}

		private static void LoadDirectoryAssemblies(string directory)
		{
			foreach (var assemblyFile in Directory.GetFiles(directory, "*.dll", SearchOption.TopDirectoryOnly))
				assemblyList.Add(LoadPluginAssembly(assemblyFile));
		}

		private static Assembly LoadPluginAssembly(string filename) { return Assembly.LoadFrom(filename); }
	}
}
