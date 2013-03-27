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
using System.Drawing;
using Stream = System.IO.Stream;
using CrystalMpq.DataFormats;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer.Viewers
{
	internal sealed class BitmapViewer : FileViewer
	{
		private Bitmap bitmap;

		public BitmapViewer(IHost host)
			: base(host)
		{
			DoubleBuffered = true;
			BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			ApplySettings();
		}

		public override void ApplySettings()
		{
			BackColor = Host.ViewerBackColor;
		}

		public Bitmap Bitmap
		{
			get
			{
				return bitmap;
			}
			set
			{
				if (value != bitmap)
				{
					bitmap = value;
					this.BackgroundImage = bitmap;
				}
			}
		}

		protected override void OnFileChanged()
		{
			if (File == null)
			{
				Bitmap = null;
				return;
			}
			else
			{
				Stream stream;

				stream = File.Open();
				try
				{
					Bitmap = new Bitmap(stream);
				}
				finally
				{
					stream.Close();
				}
			}
		}
	}
}
