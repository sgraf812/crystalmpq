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
	public static class SpellEffectNameDictionary
	{
		static Dictionary<int, string> spellEffectNameDictionary = BuildDictionary();

		static Dictionary<int, string> BuildDictionary()
		{
			Dictionary<int, string> spellEffectNameDictionary = new Dictionary<int, string>();
			XmlReader xmlReader = XmlReader.Create(
				typeof(SpellEffectNameDictionary).Assembly.GetManifestResourceStream(typeof(SpellEffectNameDictionary), "SpellEffectNames.xml"),
				new XmlReaderSettings()
				{
					IgnoreWhitespace = true,
					IgnoreComments = true,
					CloseInput = true,
					ConformanceLevel = ConformanceLevel.Document,
				});

			xmlReader.Read();
			xmlReader.ReadStartElement("SpellEffects");

			while (xmlReader.NodeType == XmlNodeType.Element)
			{
				int id;
				string name;

				if (xmlReader.Name != "SpellEffect")
					if (!xmlReader.ReadToNextSibling("SpellEffect"))
						continue;

				xmlReader.MoveToAttribute("Id");
				xmlReader.ReadAttributeValue();
				id = int.Parse(xmlReader.Value);
				xmlReader.MoveToAttribute("Name");
				xmlReader.ReadAttributeValue();
				name = xmlReader.Value;
				xmlReader.Skip();

				spellEffectNameDictionary.Add(id, name);
			}

			xmlReader.ReadEndElement();

			return spellEffectNameDictionary;
		}

		public static string GetEffectName(int id)
		{
			string name;

			if (spellEffectNameDictionary.TryGetValue(id, out name))
				return name;
			else
				return id.ToString();
		}
	}
}
