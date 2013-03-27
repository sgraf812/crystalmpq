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
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.ComponentModel;
using Stream = System.IO.Stream;
using CrystalMpq.DataFormats;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer.BaseViewers
{
	public sealed partial class BLPViewer : FileViewer
	{
		private BlpTexture texture;
		private Bitmap bitmap;

		public BLPViewer(IHost host)
			: base(host)
		{
			DoubleBuffered = true;
			InitializeComponent();
			UpdateStatusInformation();
			ApplySettings();
		}

		public override void ApplySettings()
		{
			BackColor = Host.ViewerBackColor;
		}

		public override MenuStrip Menu { get { return menuStrip; } }
		public override ToolStrip MainToolStrip { get { return mainToolStrip; } }
		public override StatusStrip StatusStrip { get { return statusStrip; } }

		public BlpTexture Texture
		{
			get
			{
				return texture;
			}
			set
			{
				if (value != texture)
				{
					texture = value;

					if (bitmap != null)
					{
						bitmap.Dispose();
						bitmap = null;
					}

					if (texture != null)
					{
						bitmap = SurfaceToBitmap(texture.FirstMipmap.BaseSurface);
						this.BackgroundImage = bitmap;
						exportToolStripMenuItem.Enabled = true;
					}
					else
					{
						this.BackgroundImage = null;
						exportToolStripMenuItem.Enabled = false;
					}
					UpdateStatusInformation();
				}
			}
		}

		protected override void OnFileChanged()
		{
			if (File == null)
			{
				Texture = null;
				return;
			}
			else
			{
				Stream stream;
				BlpTexture texture = null; // Avoid the stupid catch-throw with this mini hack

				stream = File.Open();
				try { texture = new BlpTexture(stream, false); }
				finally { stream.Close(); Texture = texture; }
			}
		}

		private void ShowStatusInformation(bool show)
		{
			sizeToolStripStatusLabel.Visible = show;
		}

		private void UpdateStatusInformation()
		{
			if (texture != null)
			{
				sizeToolStripStatusLabel.Text = string.Format(Properties.Resources.Culture,
					Properties.Resources.SizeFormat,
					texture.FirstMipmap.Width, texture.FirstMipmap.Height);
				ShowStatusInformation(true);
			}
			else
				ShowStatusInformation(false);
		}

		private Bitmap SurfaceToBitmap(Surface surface)
		{
			if (surface is JpegSurface)
			{
				var bitmap = new Bitmap(surface.CreateStream());

				SwapRedAndBlueChannels(bitmap);

				return bitmap;
			}
			else
			{
				var bitmap = new Bitmap(surface.Width, surface.Height, PixelFormat.Format32bppArgb);

				try
				{
					var bitmapData = bitmap.LockBits(new Rectangle(0, 0, surface.Width, surface.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

					surface.CopyToArgb(new SurfaceData(bitmapData.Width, bitmapData.Height, bitmapData.Scan0, bitmapData.Stride));

					bitmap.UnlockBits(bitmapData);

					return bitmap;
				}
				catch { bitmap.Dispose(); throw; }
			}
		}

		/// <summary>
		/// Swaps red and blue channels of a bitmap.
		/// This function is useful to recover colors from JPEG mip maps stored in BLP1 images.
		/// </summary>
		/// <param name="bitmap">Bitmap to modify</param>
		private static unsafe void SwapRedAndBlueChannels(Bitmap bitmap)
		{
			int width = bitmap.Width;
			int height = bitmap.Height;

			var bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

			var rowPointer = (byte*)bitmapData.Scan0.ToPointer();

			for (int y = height; y-- != 0; rowPointer += bitmapData.Stride)
			{
				var pixelPointer = rowPointer;

				for (int x = width; x-- != 0; pixelPointer += 4)
				{
					byte tmp;

					tmp = pixelPointer[0];
					pixelPointer[0] = pixelPointer[2];
					pixelPointer[2] = tmp;
				}
			}

			bitmap.UnlockBits(bitmapData);
		}

		private void exportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (saveFileDialog.ShowDialog(Host) == System.Windows.Forms.DialogResult.OK)
			{
				switch (saveFileDialog.FilterIndex)
				{
					case 1:
						bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
						break;
					case 2:
						bitmap.Save(saveFileDialog.FileName, ImageFormat.Bmp);
						break;
				}
			}
		}
	}
}
