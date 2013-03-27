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

namespace CrystalMpq.DataFormats
{
	/// <summary>
	/// Specifies the presence of a key on a field in a record structure.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class KeyAttribute : Attribute
	{
		string name;

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyAttribute"/> class.
		/// </summary>
		public KeyAttribute() { name = null; }

		/// <summary>
		/// Initializes a new instance of the <see cref="KeyAttribute"/> class.
		/// </summary>
		/// <param name="name">The key name.</param>
		/// <remarks>If you do not wish to specify a name, use the parameterless constructor.</remarks>
		/// <exception cref="ArgumentNullException">The key name is <c>null</c>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The key name is empty.</exception>
		public KeyAttribute(string name)
		{
			if (name == null)
				throw new ArgumentNullException("name");
			if (name.Length < 1)
				throw new ArgumentOutOfRangeException("name");
			this.name = name;
		}

		/// <summary>
		/// Gets the key name.
		/// </summary>
		/// <value>The key name.</value>
		/// <remarks>This value may be null.</remarks>
		public string Name { get { return name; } }
	}
}
