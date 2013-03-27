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
using System.Xml;

namespace CrystalMpq.WoWDatabases
{
	public static class SpellAuraNameDictionary
	{
		static Dictionary<int, string> spellAuraNameDictionary = BuildDictionary();

		static Dictionary<int, string> BuildDictionary()
		{
			Dictionary<int, string> spellAuraNameDictionary = new Dictionary<int, string>();
			XmlReader xmlReader = XmlReader.Create(
				typeof(SpellAuraNameDictionary).Assembly.GetManifestResourceStream(typeof(SpellAuraNameDictionary), "SpellAuraNames.xml"),
				new XmlReaderSettings()
				{
					IgnoreWhitespace = true,
					IgnoreComments = true,
					CloseInput = true,
					ConformanceLevel = ConformanceLevel.Document,
				});

			xmlReader.Read();
			xmlReader.ReadStartElement("SpellAuras");

			while (xmlReader.NodeType == XmlNodeType.Element)
			{
				int id;
				string name;

				if (xmlReader.Name != "SpellAura")
					if (!xmlReader.ReadToNextSibling("SpellAura"))
						continue;

				xmlReader.MoveToAttribute("Id");
				xmlReader.ReadAttributeValue();
				id = int.Parse(xmlReader.Value);
				xmlReader.MoveToAttribute("Name");
				xmlReader.ReadAttributeValue();
				name = xmlReader.Value;
				xmlReader.Skip();

				spellAuraNameDictionary.Add(id, name);
			}

			xmlReader.ReadEndElement();

			return spellAuraNameDictionary;
		}

		public static string GetAuraName(int id)
		{
			string name;

			if (spellAuraNameDictionary.TryGetValue(id, out name))
				return name;
			else
				return id.ToString();
		}
	}
}
