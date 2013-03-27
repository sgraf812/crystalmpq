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

namespace CrystalMpq.Explorer
{
	[Serializable]
	struct ViewerAssociation
	{
		string extension;
		string typeName;

		public ViewerAssociation(string extension, string typeName) { this.extension = extension; this.typeName = typeName; }

		public string Extension { get { return extension; } set { extension = value; } }
		public string TypeName { get { return typeName; } set { typeName = value; } }
	}
}
