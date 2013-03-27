#region Copyright Notice
// This file is part of CrystalMPQ.
// 
// Copyright (C) 2007-2011 Fabien BARBIER
// 
// CrystalMPQ is licenced under the Microsoft Reciprocal License.
// You should find the licence included with the source of the program,
// or at this URL: http://www.microsoft.com/opensource/licenses.mspx#Ms-RL
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using CrystalMpq;
using CrystalMpq.WoW;
using CrystalMpq.DataFormats;
using CrystalMpq.WoWDatabases;
using System.IO;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Collections.ObjectModel;
#endregion

namespace WoWMapExplorer
{
	public partial class MainForm : Form
	{
		#region Map Information Structures

		private class Continent
		{
			public Continent(int id, int mapId, RectangleF bounds, string name, string dataName, Zone[] zones, ZoneMap zoneMap)
			{
				Id = id;
				MapId = mapId;
				Name = name;
				Bounds = bounds;
				DataName = dataName;
				Zones = new ReadOnlyCollection<Zone>(zones);
				ZoneMap = zoneMap;
			}

			public int Id { get; private set; }
			public int MapId { get; private set; }
			public string Name { get; private set; }
			public string DataName { get; private set; }
			public RectangleF Bounds { get; private set; }
			public ReadOnlyCollection<Zone> Zones { get; private set; }
			public ZoneMap ZoneMap { get; private set; }

			public override string ToString() { return Name; }
		}

		[Flags]
		private enum ZoneFlags
		{
			None = 0,
			// 1 has no use yet
			Phased = 2,
			Unknown1 = 4,
			MælströmContinent = 8, // Only used on the maëlstrom ?
			MælströmZone = 16
		}

		private class Zone
		{
			public Zone(int id, int areaId, int mapId, RectangleF bounds, ZoneFlags flags, string name, string dataName, Overlay[] overlays)
			{
				Id = id;
				AreaId = areaId;
				MapId = mapId;
				Bounds = bounds;
				Flags = flags;
				Name = name;
				DataName = dataName;
				Overlays = new ReadOnlyCollection<Overlay>(overlays);
			}

			public int Id { get; private set; }
			public int AreaId { get; private set; }
			public int MapId { get; private set; }
			public ZoneFlags Flags { get; private set; }
			public string Name { get; private set; }
			public string DataName { get; private set; }
			public RectangleF Bounds { get; private set; }
			public ReadOnlyCollection<Overlay> Overlays { get; private set; }

			public override string ToString() { return Name; }
		}

		private class Overlay
		{
			public Overlay(int id, int zoneId, int areaId, Rectangle bounds, Rectangle boundingRectangle, string name, string dataName)
			{
				Id = id;
				ZoneId = zoneId;
				AreaId = areaId;
				Bounds = bounds;
				BoundingRectangle = boundingRectangle;
				Name = name;
				DataName = dataName;
			}

			public int Id { get; private set; }
			public int ZoneId { get; private set; }
			public int AreaId { get; private set; }
			public Rectangle Bounds { get; private set; }
			public Rectangle BoundingRectangle { get; private set; }
			public string Name { get; private set; }
			public string DataName { get; private set; }

			public override string ToString() { return Name; }
		}

		#endregion

		#region Fields

		// Constants
		private const int databaseLocaleFieldCount = 16;
		// MPQ Archives
		private WoWInstallation wowInstallation;
		private WoWLanguagePack languagePack;
		private WoWMpqFileSystem wowFileSystem;
		// Font for displaying zone information
		private PrivateFontCollection wowFontCollection;
		private Brush zoneInformationBrush;
		private Pen zoneInformationPen;
		private Font zoneInformationFont;
		private Brush zoneErrorBrush;
		private const int zoneInformationFontHeight = 40;
		// World Map Size: 1002x668
		private Bitmap mapBitmap;
		private Bitmap outlandHighlightBitmap;
		private Bitmap azerothHighlightBitmap;
		private Rectangle outlandButtonBounds;
		private Rectangle azerothButtonBounds;
		private bool outlandHighlighted;
		private bool azerothHighlighted;
		// Databases
		private KeyedClientDatabase<int, MapRecord> mapDatabase;
		private KeyedClientDatabase<int, WorldMapContinentRecord> worldMapContinentDatabase;
		private KeyedClientDatabase<int, WorldMapAreaRecord> worldMapAreaDatabase;
		private KeyedClientDatabase<int, AreaTableRecord> areaTableDatabase;
		private KeyedClientDatabase<int, WorldMapOverlayRecord> worldMapOverlayDatabase;
		private KeyedClientDatabase<int, DungeonMapRecord> dungeonMapDatabase;
		// Status
		private int currentContinent, currentZone;
		private List<Continent> continents;
		private IList<Zone> zones;
		private IList<Overlay> overlays;
		private string zoneInformationText;

		#endregion

		#region Constructor & Destructor

		public MainForm(WoWInstallation wowInstallation, WoWLanguagePack languagePack)
		{
			InitializeComponent();
			// Initialize WoW file system
			this.wowInstallation = wowInstallation;
			this.languagePack = languagePack;
			// Create resources for drawing text
			zoneInformationBrush = Brushes.White;
			zoneInformationPen = new Pen(Color.Black, 5);
			// Get and create resources for error…
			zoneErrorBrush = Brushes.Red;
			// Creates the bitmap
			mapBitmap = new Bitmap(1002, 668, PixelFormat.Format32bppRgb);
			Size = new Size(this.Width - renderPanel.ClientSize.Width + 1002, this.Height - renderPanel.ClientSize.Height + 668);
			renderPanel.BackgroundImage = mapBitmap;
			// Initialization
			InitializeFileSystem();
			LoadDatabases();
			LoadFonts();
			currentContinent = -1;
			InitializeMapData();
			FillContinents();
			FillZones();
			LoadCosmicHighlights();
			UpdateMap();
		}

		#endregion

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		private void InitializeFileSystem()
		{
			// Create a new instance
			wowFileSystem = wowInstallation.CreateFileSystem(languagePack, false, false);
		}

		private void LoadDatabases()
		{
			mapDatabase = LoadDatabase<MapRecord>(@"DBFilesClient\Map.dbc");
			worldMapContinentDatabase = LoadDatabase<WorldMapContinentRecord>(@"DBFilesClient\WorldMapContinent.dbc");
			worldMapAreaDatabase = LoadDatabase<WorldMapAreaRecord>(@"DBFilesClient\WorldMapArea.dbc");
			areaTableDatabase = LoadDatabase<AreaTableRecord>(@"DBFilesClient\AreaTable.dbc");
			worldMapOverlayDatabase = LoadDatabase<WorldMapOverlayRecord>(@"DBFilesClient\WorldMapOverlay.dbc");
			dungeonMapDatabase = LoadDatabase<DungeonMapRecord>(@"DBFilesClient\DungeonMap.dbc");
		}

		private void LoadFonts()
		{
			FontFamily family = LoadFont(@"Fonts\FRIZQT__.TTF");

			if (family != null)
				try { zoneInformationFont = new Font(family, zoneInformationFontHeight, FontStyle.Regular, GraphicsUnit.Pixel, 0); }
				catch { family.Dispose(); family = null; }
			if (family == null)
				zoneInformationFont = new Font("Arial Narrow", zoneInformationFontHeight, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, 0);
		}

		private void LoadCosmicHighlights()
		{
			using (var texture = LoadBlpTextureAsBitmap(@"Interface\WorldMap\Cosmic\Cosmic-Outland-Highlight.blp"))
				outlandHighlightBitmap = new Bitmap(texture, 856, 605);
			outlandButtonBounds = new Rectangle(115, 90, 320, 320);
			using (var texture = LoadBlpTextureAsBitmap(@"Interface\WorldMap\Cosmic\Cosmic-Azeroth-Highlight.blp"))
				azerothHighlightBitmap = new Bitmap(texture, 898, 647);
			azerothButtonBounds = new Rectangle(593, 255, 366, 366);
		}

		#region Generic Data Loading Functions

		private KeyedClientDatabase<int, T> LoadDatabase<T>(string filename) where T : struct
		{
			return LoadDatabase<int, T>(filename);
		}

		private KeyedClientDatabase<TKey, TValue> LoadDatabase<TKey, TValue>(string filename) where TValue : struct
		{
			MpqFile file;
			Stream fileStream = null;

			if ((file = wowFileSystem.FindFile(filename)) != null)
				using (fileStream = file.Open())
					return new KeyedClientDatabase<TKey, TValue>(fileStream, languagePack.DatabaseFieldIndex);
			else
				return null;
		}

		private FontFamily LoadFont(string filename)
		{
			// Initialize the font collection…
			wowFontCollection = wowFontCollection ?? new PrivateFontCollection();

			// Open the file
			var fontFile = wowFileSystem.FindFile(filename);

			// Read the contents of the file
			var buffer = new byte[fontFile.Size]; // Allocate the read buffer
			using (var stream = fontFile.Open())
				stream.Read(buffer, 0, (int)fontFile.Size);

			// Finally add the font
			unsafe
			{
				fixed (byte* bufferPointer = buffer)
					wowFontCollection.AddMemoryFont((IntPtr)bufferPointer, (int)fontFile.Size);
			}

			// Return the result
			return wowFontCollection.Families[wowFontCollection.Families.Length - 1];
		}

		private BlpTexture LoadBlpTexture(string filename)
		{
			if (filename == null) throw new ArgumentNullException("filename");

			var file = wowFileSystem.FindFile(filename);

			if (file == null) throw new FileNotFoundException();

			using (var stream = file.Open())
				return new BlpTexture(stream, false);
		}

		private Bitmap LoadBlpTextureAsBitmap(string filename)
		{
			if (filename == null) throw new ArgumentNullException("filename");

			var file = wowFileSystem.FindFile(filename);

			if (file == null) throw new FileNotFoundException();

			using (var stream = file.Open())
			using (var texture = new BlpTexture(stream, false))
			{
				var bitmap = new Bitmap(texture.Width, texture.Height, PixelFormat.Format32bppArgb);

				try
				{
					var bitmapData = bitmap.LockBits(new Rectangle(0, 0, texture.Width, texture.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

					texture.FirstMipmap.CopyToArgb(new SurfaceData(bitmapData.Width, bitmapData.Height, bitmapData.Scan0, bitmapData.Stride));

					bitmap.UnlockBits(bitmapData);

					return bitmap;
				}
				catch { bitmap.Dispose(); throw; }
			}
		}

		private ZoneMap LoadZoneMap(string filename)
		{
			MpqFile file = wowFileSystem.FindFile(filename);
			Stream stream = null;
			ZoneMap zoneMap;

			if (file == null)
				return null;
			try
			{
				stream = file.Open();
				zoneMap = new ZoneMap(stream);
				stream.Close();
#if DEBUG
				if (zoneMap != null)
				{
					Bitmap b = new Bitmap(128, 128);

					for (int i = 0; i < 128; i++)
						for (int j = 0; j < 128; j++)
							b.SetPixel(i, j, Color.FromArgb(zoneMap[i, j] | ~0xFFFFFF));

					b.Save(Path.GetFileNameWithoutExtension(filename) + ".bmp");
				}
#endif
				return zoneMap;
			}
			catch
			{
				if (stream != null)
					stream.Close();
				return null;
			}
		}
		#endregion

		#region Data Processing Functions

		private void InitializeMapData()
		{
			continents = new List<Continent>();

			LoadContinents();
		}

		private void LoadContinents()
		{
			foreach (var worldMapContinentRecord in worldMapContinentDatabase.Records)
			{
				MapRecord mapRecord;

				if (mapDatabase.TryGetValue(worldMapContinentRecord.Map, out mapRecord))
				{
					RectangleF bounds;
					var zones = LoadZones(worldMapContinentRecord.Map, out bounds);

					Continent continent = new Continent
					(
						worldMapContinentRecord.Id,
						worldMapContinentRecord.Map,
						bounds,
						mapRecord.Name,
						mapRecord.DataName,
						zones,
						LoadZoneMap(@"Interface\WorldMap\" + mapRecord.DataName + ".zmp")
					);

					continents.Add(continent);
				}
			}
		}

		private Zone[] LoadZones(int MapId, out RectangleF continentBounds)
		{
			var zones = new List<Zone>();

			continentBounds = RectangleF.Empty;

			// Look for zones matching this map ID in WorldMapArea database
			foreach (var worldMapAreaRecord in worldMapAreaDatabase.Records)
			{
				// Zone information stored as following
				//  In field 1: ID of the game map containing the zone
				//  In field 8: ID of the map virtually containing the zone (-1 if it is the same as field 1)

				if ((worldMapAreaRecord.VirtualMap == -1 && worldMapAreaRecord.Map == MapId) // Either virtual ID is -1 and we have ID in field 1
					|| worldMapAreaRecord.VirtualMap == MapId) // Or we have ID in field 8
				{
					var bounds = new RectangleF(worldMapAreaRecord.BoxLeft, worldMapAreaRecord.BoxTop, worldMapAreaRecord.BoxRight - worldMapAreaRecord.BoxLeft, worldMapAreaRecord.BoxBottom - worldMapAreaRecord.BoxTop);

					if (worldMapAreaRecord.Area == 0) // For continents
						continentBounds = bounds;
					else // Now look into AreaTable database
					{
						AreaTableRecord areaTableRecord;

						if (areaTableDatabase.TryGetValue(worldMapAreaRecord.Area, out areaTableRecord))
						{
#if DEBUG
							System.Diagnostics.Debug.WriteLine("Bounds of \"" + areaTableRecord.Name + "\": " + bounds.ToString());
#endif
							zones.Add
							(
								new Zone
								(
									worldMapAreaRecord.Id,
									worldMapAreaRecord.Area,
									MapId,
									bounds,
									(ZoneFlags)worldMapAreaRecord.Flags,
									areaTableRecord.Name,
									worldMapAreaRecord.DataName,
									LoadOverlays(worldMapAreaRecord.Id)
								)
							);
						}
					}
				}
			}

			return zones.ToArray();
		}

		private Overlay[] LoadOverlays(int zoneId)
		{
			var overlays = new List<Overlay>();

			foreach (var overlayRecord in worldMapOverlayDatabase.Records)
				if (overlayRecord.WorldMapArea == zoneId)
				{
					var bounds = new Rectangle(overlayRecord.Left, overlayRecord.Top, overlayRecord.Width, overlayRecord.Height);
					var boundingRectangle = new Rectangle(overlayRecord.BoxLeft, overlayRecord.BoxTop, overlayRecord.BoxRight - overlayRecord.BoxLeft, overlayRecord.BoxBottom - overlayRecord.BoxTop);

					AreaTableRecord areaTableRecord;

					var name = (areaTableDatabase.TryGetValue(overlayRecord.Area1, out areaTableRecord)) ? areaTableRecord.Name : null;

					overlays.Add
					(
						new Overlay
						(
							overlayRecord.Id,
							overlayRecord.WorldMapArea,
							overlayRecord.Area1,
							bounds,
							boundingRectangle,
							name,
							overlayRecord.DataName
						)
					);
				}

			return overlays.ToArray();
		}
		#endregion

		private void FillContinents()
		{
			foreach (Continent continent in continents)
				continentToolStripComboBox.Items.Add(continent);
		}

		private void FillZones()
		{
			zoneToolStripComboBox.Items.Clear();

			currentZone = -1;

			if (currentContinent < 1)
				return;

			foreach (Zone zone in continents[currentContinent - 1].Zones)
				zoneToolStripComboBox.Items.Add(zone);
		}

		private void UpdateMap()
		{
			using (var g = Graphics.FromImage(mapBitmap))
			{
				string path = @"Interface\WorldMap\";
				string map;

				zoneInformationText = "";
				overlays = null;

				if (currentContinent == -1) map = @"Cosmic";
				else if (currentContinent == 0) map = @"World";
				else if (currentZone == -1) map = ((Continent)continentToolStripComboBox.SelectedItem).DataName;
				else map = zones[currentZone].DataName;

				path = path + map + @"\";

				for (int i = 0; i < 3; i++)
					for (int j = 0; j < 4; j++)
						try
						{
							using (var texture = LoadBlpTextureAsBitmap(path + map + (4 * i + j + 1) + ".blp"))
								g.DrawImageUnscaled(texture, 256 * j, 256 * i, 256, 256);
						}
						catch { g.FillRectangle(zoneErrorBrush, 256 * j, 256 * i, 256, 256); }

				if (currentContinent > 0 && currentZone >= 0)
				{
					overlays = zones[currentZone].Overlays;

					foreach (Overlay overlay in overlays)
					{
						int x = overlay.Bounds.X,
							y = overlay.Bounds.Y,
							width = overlay.Bounds.Width,
							height = overlay.Bounds.Height,
							rowCount = height > 256 ? (height + 255) / 256 : 1,
							colCount = width > 256 ? (width + 255) / 256 : 1,
							textureCount = rowCount * colCount;

						for (int i = 0; i < textureCount; i++)
							try
							{
								if (overlay.DataName != null && overlay.DataName.Length > 0)
									using (var texture = LoadBlpTextureAsBitmap(path + overlay.DataName + (i + 1) + ".blp"))
										g.DrawImageUnscaled(texture, x + 256 * (i % colCount), y + 256 * (i / colCount));
							}
							catch { }
					}
				}
				//else if (currentContinent == -1)
				//{
				//    if (outlandHighlighted)
				//        g.DrawImageUnscaled(outlandHighlightBitmap, 23, 35);
				//    else if (azerothHighlighted)
				//        g.DrawImageUnscaled(azerothHighlightBitmap, 103, 11);
				//}
			}
		}

		private void renderPanel_Paint(object sender, PaintEventArgs e)
		{
			SizeF infoTextSize;
			GraphicsPath graphicsPath;

			//e.Graphics.DrawImageUnscaled(mapBitmap, new Rectangle(0, 0, 1002, 668));
			if (zoneInformationText != null && zoneInformationText.Length > 0)
			{
				graphicsPath = new GraphicsPath();
				infoTextSize = e.Graphics.MeasureString(zoneInformationText, zoneInformationFont);
				graphicsPath.AddString(zoneInformationText,
					zoneInformationFont.FontFamily, (int)zoneInformationFont.Style, zoneInformationFontHeight,
					new Point((int)((1002 - infoTextSize.Width) / 2), 10),
					new StringFormat());
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.DrawPath(zoneInformationPen, graphicsPath);
				graphicsPath.Transform(new Matrix(1, 0, 0, 1, -3, -3));
				e.Graphics.DrawPath(zoneInformationPen, graphicsPath);
				e.Graphics.FillPath(zoneInformationBrush, graphicsPath);
				graphicsPath.Dispose();
			}
			if (currentContinent == -1)
			{
				if (outlandHighlighted)
					e.Graphics.DrawImageUnscaled(outlandHighlightBitmap, 23, 35);
				if (azerothHighlighted)
					e.Graphics.DrawImageUnscaled(azerothHighlightBitmap, 103, 11);
			}
		}

		private void continentToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Continent continent = continentToolStripComboBox.SelectedItem as Continent;
			int newContinent = continents.IndexOf(continent) + 1;

			if (newContinent > 0 && newContinent != currentContinent)
				SetZoomLevel(newContinent, -1);
		}

		private void zoneToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			Zone zone = zoneToolStripComboBox.SelectedItem as Zone;
			int zoneIndex = zones.IndexOf(zone);

			if (currentZone != zoneIndex)
				SetZoomLevel(currentContinent, zoneIndex);
		}

		private void zoomOutToolStripButton_Click(object sender, EventArgs e)
		{
			ZoomOut();
		}

		private void ZoomOut()
		{
			if (currentContinent <= 0)
			{
				currentContinent = -1;
				currentZone = -1;
			}
			else if (currentZone >= 0)
				currentZone = -1;
			else if (currentContinent == 3)
				currentContinent = -1;
			else
				currentContinent = 0;
			SetZoomLevel(currentContinent, currentZone);
		}

		private void SetZoomLevel(int continent, int zone)
		{
			if (continent < -1 || continent > continentToolStripComboBox.Items.Count)
				continent = -1;
			if (continent <= 0)
			{
				currentContinent = continent;
				currentZone = -1;
				continentToolStripComboBox.SelectedIndex = -1;
				FillZones();
			}
			else
			{
				if (currentContinent != continent)
				{
					currentContinent = continent;
					continentToolStripComboBox.SelectedItem = continents[currentContinent - 1];
					zones = continents[currentContinent - 1].Zones;
					FillZones();
				}
				if (zone <= -1 || zone >= zones.Count)
				{
					currentZone = -1;
					zoneToolStripComboBox.SelectedIndex = -1;
				}
				else
				{
					currentZone = zone;
					zoneToolStripComboBox.SelectedItem = zones[zone];
				}
			}
			UpdateMap();
			renderPanel.Invalidate();
		}

		private Zone GetZone(Point position)
		{
			Zone foundZone = null;

			if (currentContinent > 0 && currentZone == -1)
			{
				Continent continent = continents[currentContinent - 1];
				float x = continent.Bounds.Left,
					y = continent.Bounds.Top,
					width = continent.Bounds.Width,
					height = continent.Bounds.Height;
				float xPos = (1002 - position.X) * width / 1002 + x,
					yPos = (668 - position.Y) * height / 668 + y;

				//for (int i = 0; i < zones.Count; i++)
				for (int i = zones.Count - 1; i >= 0; i--)
				{
					Zone zone = zones[i];

					if (xPos >= zone.Bounds.Left && xPos < zone.Bounds.Right
						&& yPos >= zone.Bounds.Top && yPos < zone.Bounds.Bottom
						&& (foundZone == null || zone.AreaId > foundZone.AreaId))
						foundZone = zone;
				}
//                int zoneId = continent.ZoneMap[127 * position.X / 1002, 127 * position.Y / 668];

//                foreach (Zone zone in zones)
//                    if (zone.AreaId == zoneId)
//                        return zone;
//#if DEBUG
//                System.Diagnostics.Debug.WriteLine(zoneId);
//#endif
			}
			return foundZone;
		}

		private void renderPanel_MouseMove(object sender, MouseEventArgs e)
		{
			if (currentContinent == -1)
			{
				bool needUpdate = false;

				if (e.X >= outlandButtonBounds.X && e.X < outlandButtonBounds.Right
					&& e.Y >= outlandButtonBounds.Y && e.Y < outlandButtonBounds.Bottom)
				{
					if (!outlandHighlighted)
					{
						outlandHighlighted = true;
						needUpdate = true;
					}
				}
				else
				{
					if (outlandHighlighted)
					{
						outlandHighlighted = false;
						needUpdate = true;
					}
				}

				if (e.X >= azerothButtonBounds.X && e.X < azerothButtonBounds.Right
					&& e.Y >= azerothButtonBounds.Y && e.Y < azerothButtonBounds.Bottom)
				{
					if (!azerothHighlighted)
					{
						azerothHighlighted = true;
						needUpdate = true;
					}
				}
				else
				{
					if (azerothHighlighted)
					{
						azerothHighlighted = false;
						needUpdate = true;
					}
				}

				if (needUpdate)
				{
					//UpdateMap();
					renderPanel.Invalidate();
				}
			}
			else if (currentContinent > 0)
			{
				if (currentZone == -1)
				{
					Zone zone = GetZone(e.Location);

					if (zone != null)
					{
						if (zoneInformationText != zone.Name)
						{
							zoneInformationText = zone.Name;
							renderPanel.Invalidate();
						}
						return;
					}
				}
				else if (overlays != null)
				{
					foreach (Overlay overlay in overlays)
					{
						if (e.X >= overlay.BoundingRectangle.Left && e.X < overlay.BoundingRectangle.Right
							&& e.Y >= overlay.BoundingRectangle.Top && e.Y < overlay.BoundingRectangle.Bottom)
						{
							if (zoneInformationText != overlay.Name)
							{
								zoneInformationText = overlay.Name;
								renderPanel.Invalidate();
							}
							return;
						}
					}
				}
			}
			//if (zoneInformationText != null && zoneInformationText.Length > 0)
			//    renderPanel.Invalidate();
			zoneInformationText = null;
		}

		private void renderPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (currentContinent == -1)
				{
					if (e.X >= outlandButtonBounds.X && e.X < outlandButtonBounds.Right
						&& e.Y >= outlandButtonBounds.Y && e.Y < outlandButtonBounds.Bottom)
						SetZoomLevel(3, -1);
					else if (e.X >= azerothButtonBounds.X && e.X < azerothButtonBounds.Right
					&& e.Y >= azerothButtonBounds.Y && e.Y < azerothButtonBounds.Bottom)
						SetZoomLevel(0, -1);
				}
				else
				{
					Zone zone = GetZone(e.Location);

					if (zone != null)
						SetZoomLevel(currentContinent, zones.IndexOf(zone));
				}
			}
			else if (e.Button == MouseButtons.Right)
				ZoomOut();
		}
	}
}