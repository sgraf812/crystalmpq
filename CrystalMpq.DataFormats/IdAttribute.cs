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

namespace CrystalMpq.DataFormats
{
	/// <summary>
	/// Specifies the presence of an ID key on a field in a record structure. This class cannot be inherited.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class IdAttribute : KeyAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="IdAttribute"/> class.
		/// </summary>
		public IdAttribute() : base("Id") { }
	}
}
