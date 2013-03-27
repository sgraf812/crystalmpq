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
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace CrystalMpq.Explorer
{
	sealed class EncodingConverter : TypeConverter
	{
		static string[] encodingNameArray;
		static Encoding[] encodingArray;
		static StandardValuesCollection encodingCollection;

		static EncodingConverter()
		{
			EncodingInfo[] encodingInfo;

			encodingInfo = Encoding.GetEncodings();
			encodingNameArray = new string[encodingInfo.Length];
			encodingArray = new Encoding[encodingInfo.Length];
			for (int i = 0; i < encodingInfo.Length; i++)
			{
				encodingNameArray[i] = encodingInfo[i].Name;
				encodingArray[i] = Encoding.GetEncoding(encodingInfo[i].Name);
			}

			encodingCollection = new StandardValuesCollection(encodingArray);
		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string) || sourceType == typeof(EncodingInfo) || sourceType == typeof(Encoding))
				return true;
			else
				return base.CanConvertFrom(context, sourceType);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(string) || destinationType == typeof(Encoding))
				return true;
			else
				return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			System.Diagnostics.Debug.WriteLine("{ " + value.GetType().FullName + " = " + value.ToString() + " }");
			if (value == null)
				return null;
			else if (value is string)
				try { return Encoding.GetEncoding((string)value); }
				catch { return null; }
			else if (value is EncodingInfo)
				return Encoding.GetEncoding(((EncodingInfo)value).Name);
			else if (value is Encoding)
				return value;
			else
				return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			Encoding encoding = value as Encoding;

			if (destinationType == typeof(string))
			{
				if (encoding != null)
					return encoding.WebName;
				else
					return null;
			}
			else if (destinationType == typeof(Encoding))
				return encoding;
			else
				return base.ConvertTo(context, culture, value, destinationType);
		}

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return encodingCollection;
		}

		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (value is Encoding || value is EncodingInfo)
				return true;
			else if (value is string)
				try
				{
					Encoding.GetEncoding((string)value);
					return true;
				}
				catch
				{
					return false;
				}
			else
				return base.IsValid(context, value);
		}
	}
}
