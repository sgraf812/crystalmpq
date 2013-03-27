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
using System.Globalization;

namespace CrystalMpq.WoW
{
	/// <summary>This exception is thrown when a culture is not supported.</summary>
	/// <remarks>Refer to the <see cref="Culture"/> field for information on the unsupported culture.</remarks>
	public sealed class CultureNotSupportedException : NotSupportedException
	{
		CultureInfo cultureInfo;

		/// <summary>Initializes a new instance of the <see cref="CultureNotSupportedException"/> class.</summary>
		/// <param name="culture">The unsupported culture.</param>
		public CultureNotSupportedException(CultureInfo culture)
			: base(string.Format(Properties.Resources.Culture, Properties.Resources.UnsupportedCultureMessage, culture.DisplayName, culture.Name))
		{ this.cultureInfo = culture; }

		/// <summary>Initializes a new instance of the <see cref="CultureNotSupportedException"/> class.</summary>
		/// <param name="culture">The unsupported culture.</param>
		/// <param name="innerException">The inner exception.</param>
		public CultureNotSupportedException(CultureInfo culture, Exception innerException)
			: base(string.Format(Properties.Resources.Culture, Properties.Resources.UnsupportedCultureMessage, culture.DisplayName, culture.Name), innerException)
		{ this.cultureInfo = culture; }

		/// <summary>Gets the unsupported culture.</summary>
		/// <value>The unsupported culture.</value>
		public CultureInfo Culture { get { return cultureInfo; } }
	}
}
