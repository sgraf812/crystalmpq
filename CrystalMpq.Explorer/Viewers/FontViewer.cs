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
using System.Drawing.Text;
using System.Globalization;
using System.Windows.Forms;
using CrystalMpq.Explorer.Extensibility;

namespace CrystalMpq.Explorer.Viewers
{
	internal sealed class FontViewer : FileViewer
	{
		private static readonly float[] previewFontSizes = { 12, 18, 24, 36, 48, 60, 72 };
		private PrivateFontCollection fontCollection;
		private FontFamily fontFamily;
		private Font bigFont;
		private Font[] demoFonts;
		private Brush foreBrush;
		private Pen forePen;

		public FontViewer(IHost host)
			: base(host)
		{
			base.AutoScroll = true;
			HScroll = false;
			DoubleBuffered = true;
			SetStyle(ControlStyles.ContainerControl, false);
			BorderStyle = BorderStyle.FixedSingle;
			BackColor = SystemColors.Window;
			ForeColor = SystemColors.WindowText;
			demoFonts = new Font[previewFontSizes.Length];
		}

		public FontFamily FontFamily
		{
			get { return fontFamily; }
			private set
			{
				if (value != fontFamily) fontFamily = value;
				for (int i = 0; i < previewFontSizes.Length; i++)
					demoFonts[i] = value != null ? new Font(value, previewFontSizes[i], GraphicsUnit.Point) : null;
				using (var graphics = CreateGraphics())
				{
					var size = DrawAndMeasure(graphics, false);

					AutoScrollMinSize = new Size(0, (int)size.Height);
				}
				Invalidate();
			}
		}

		public sealed override Font Font
		{
			get { return base.Font; }
			set
			{
				base.Font = value;

				if (bigFont != null)
				{
					bigFont.Dispose();
					bigFont = null;
				}
			}
		}

		public Font BigFont { get { return bigFont = bigFont ?? new Font(Font.FontFamily, Font.SizeInPoints * 2.4f, FontStyle.Bold, GraphicsUnit.Point); } }

		public sealed override Color ForeColor
		{
			get { return base.ForeColor; }
			set
			{
				if (foreBrush != null)
				{
					foreBrush.Dispose();
					foreBrush = null;
				}
				if (forePen != null)
				{
					forePen.Dispose();
					forePen = null;
				}

				base.ForeColor = value;

				if (value != null)
				{
					foreBrush = new SolidBrush(value);
					forePen = new Pen(value, 1);
				}
			}
		}

		public sealed override bool AutoScroll
		{
			get { return base.AutoScroll; }
			set { }
		}

		protected sealed override unsafe void OnFileChanged()
		{
			if (fontCollection != null)
			{
				fontCollection.Dispose();
				fontCollection = null;
				fontFamily = null;
			}

			if (File != null)
				using (var stream = File.Open())
				{
					var buffer = new byte[stream.Length];

					fixed (byte* bufferPointer = buffer)
					{
						stream.Read(bufferPointer, (int)stream.Length);

						fontCollection = new PrivateFontCollection();
						try { fontCollection.AddMemoryFont((IntPtr)bufferPointer, buffer.Length); }
						catch { fontCollection = null; throw; }
					}

					FontFamily = fontCollection.Families[fontCollection.Families.Length - 1];
				}
		}

		protected sealed override void OnScroll(ScrollEventArgs se)
		{
			Invalidate();
			base.OnScroll(se);
		}

		protected sealed override void OnPaint(PaintEventArgs e)
		{
			DrawAndMeasure(e.Graphics, true);
			base.OnPaint(e);
		}

		private RectangleF DrawAndMeasure(Graphics graphics, bool draw)
		{
			if (foreBrush == null) return RectangleF.Empty;

			float offsetY = AutoScrollPosition.Y;

			PointF location;
			SizeF size;

			var bigFont = BigFont;
			var smallFont = Font;

			location = new PointF(0, offsetY);
			size = graphics.MeasureString(fontFamily.Name, bigFont);
			if (draw) graphics.DrawString(fontFamily.Name, bigFont, foreBrush, new RectangleF(location, size));

			location.Y += size.Height;
			size = new SizeF(ClientSize.Width, 8);
			if (draw) graphics.DrawLine(forePen, 8, location.Y + 4, size.Width - 9, location.Y + 4);

			foreach (var font in demoFonts)
			{
				location.Y += size.Height;
				size = graphics.MeasureString("The quick brown fox jumps over the lazy dog", font);
				if (draw)
				{
					string infoText = font.SizeInPoints.ToString(CultureInfo.InvariantCulture);
					var infoTextSize = graphics.MeasureString(infoText, smallFont);

					graphics.DrawString(infoText, smallFont, foreBrush, 0, location.Y + 0.5f * (size.Height - infoTextSize.Height));
					location.X = infoTextSize.Width;
					graphics.DrawString("The quick brown fox jumps over the lazy dog", font, foreBrush, new RectangleF(location, size));
				}
			}

			return new RectangleF(0, 0, size.Width, location.Y - offsetY + size.Height);
		}
	}
}
