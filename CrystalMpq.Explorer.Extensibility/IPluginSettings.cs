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

namespace CrystalMpq.Explorer.Extensibility
{
	/// <summary>
	/// Interface used to implement plugin settings
	/// </summary>
	public interface IPluginSettings
	{
		/// <summary>
		/// Resets the properties to their original values
		/// </summary>
		void Reset();

		/// <summary>
		/// Saves the changes made to the properties
		/// </summary>
		void Save();
	}
}
