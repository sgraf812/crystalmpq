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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Stream = System.IO.Stream;
using Path = System.IO.Path;
using FileIO = System.IO.File;
using CrystalMpq.Explorer.Extensibility;
using DirectShowLib;
using System.Runtime.InteropServices;

namespace CrystalMpq.Explorer.AudioVideo
{
	public sealed partial class MediaPlayer : FileViewer
	{
		private IGraphBuilder graphBuilder;
		private IMediaControl mediaControl;
		private IMediaSeeking mediaSeeking;
		private IMediaEventEx mediaEvent;
		private AMSeekingSeekingCapabilities seekingCapabilities;

		private IBaseFilter vmr9;
		private IVMRFilterConfig9 filterConfig;
		private IVMRSurfaceAllocatorNotify9 surfaceAllocatorNotify;
		private IVMRMixerControl9 mixerControl;

		private const int WM_APP = 0x8000;
		private const int WM_GRAPHNOTIFY = WM_APP + 1;
		private const int timeScalingFactor = 100000; // Units of 100 nanoseconds

		private string tempFileName;
		private bool updating, hasVideo, fileDeleted;
		private Image playIcon, pauseIcon;

		public MediaPlayer(IHost host)
			: base(host)
		{
			InitializeComponent();
			playIcon = playPauseToolStripButton.Image = Properties.Resources.PlayIcon;
			pauseIcon = playPauseToolStripButton.Image = Properties.Resources.PauseIcon;
			fileDeleted = true;
			ApplySettings();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}

			DisposeGraph();
			DeleteFile();

			base.Dispose(disposing);
		}

		public override void ApplySettings() { renderPanel.BackColor = Host.ViewerBackColor; }

		#region Object Creation and Disposing

		private void CreateGraph()
		{
			graphBuilder = (IGraphBuilder)new FilterGraph();
			graphBuilder.RenderFile(tempFileName, null);
			mediaControl = (IMediaControl)graphBuilder;
			mediaSeeking = (IMediaSeeking)graphBuilder;
			mediaEvent = (IMediaEventEx)graphBuilder;
			//var filter = new MpqFileSourceFilter(File);
			//DsError.ThrowExceptionForHR(graphBuilder.AddFilter(filter, filter.Name));
			//DsError.ThrowExceptionForHR(graphBuilder.Render(filter.OutputPin));
			mediaSeeking.GetCapabilities(out seekingCapabilities);
			mediaSeeking.SetTimeFormat(TimeFormat.MediaTime);
			mediaEvent.SetNotifyWindow(Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
		}

		private void DisposeGraph()
		{
			if (mediaEvent != null)
			{
				mediaEvent.SetNotifyWindow(IntPtr.Zero, 0, IntPtr.Zero);
				mediaEvent = null;
			}
			if (mediaSeeking != null) mediaSeeking = null;
			if (mediaControl != null)
			{
				mediaControl.Stop();
				mediaControl = null;
			}
			if (graphBuilder != null)
			{
				graphBuilder.Abort();
				int a = Marshal.ReleaseComObject(graphBuilder);
				graphBuilder = null;
			}
		}

		#endregion

		#region Temporary File Creation and Deletion

		private void CreateFile(byte[] data)
		{
			DeleteFile();
			tempFileName = Path.GetTempFileName();
			FileIO.WriteAllBytes(tempFileName, data);
			fileDeleted = false;
		}

		private void DeleteFile()
		{
			if (!fileDeleted && FileIO.Exists(tempFileName))
				FileIO.Delete(tempFileName);
			fileDeleted = true;
		}

		#endregion

		#region Media Functions

		private void UpdateSeekingCapabilities() { if (mediaSeeking != null) mediaSeeking.GetCapabilities(out seekingCapabilities); }

		public void ResetMedia() { if (mediaSeeking != null) mediaSeeking.SetPositions(DsLong.FromInt64(0), AMSeekingSeekingFlags.AbsolutePositioning, null, AMSeekingSeekingFlags.NoPositioning); }

		public void Play()
		{
			if (mediaControl != null)
			{
				mediaControl.Run();
				UpdateSeekingCapabilities();
				OnPlay(this, EventArgs.Empty);
			}
		}

		public void Pause()
		{
			if (mediaControl != null)
			{
				mediaControl.Pause();
				UpdateSeekingCapabilities();
				OnPause(this, EventArgs.Empty);
			}
		}

		public void Stop()
		{
			if (mediaControl != null)
			{
				mediaControl.Stop();
				UpdateSeekingCapabilities();
				ResetMedia();
				OnStop(this, EventArgs.Empty);
			}
		}

		public bool Playing
		{
			get
			{
				if (mediaControl == null) return false;
				FilterState filterState;
				int result = mediaControl.GetState(1000, out filterState);
				if (result == 0 || result == DsResults.S_StateIntermediate) return filterState == FilterState.Running;
				else if (result != DsResults.S_CantCue) DsError.ThrowExceptionForHR(result);

				return false;
			}
		}

		#endregion

		protected override void OnFileChanged()
		{
			Stream inputStream;

			DisposeGraph();
			DeleteFile();
			if (File != null)
			{
				using (inputStream = File.Open())
				{
				    try
				    {
				        byte[] buffer;
				        int length;

				        checked { length = (int)inputStream.Length; }
				        buffer = new byte[length];
				        if (inputStream.Read(buffer, 0, length) != length)
				            throw new InvalidOperationException();
				        CreateFile(buffer);
						CreateGraph();
						UpdateInterface();
						Play();
					}
					catch
					{
						if (FileIO.Exists(tempFileName))
							FileIO.Delete(tempFileName);
						UpdateInterface();
						DisposeGraph();
						throw;
					}
				}
			}
		}

		private void UpdateInterface()
		{
			if (File != null)
				fileNameLabel.Text = File.Name;
			else
				fileNameLabel.Text = null;

			if (mediaSeeking != null && (seekingCapabilities & AMSeekingSeekingCapabilities.CanGetDuration) != 0)
			{
				long duration;

				mediaSeeking.GetDuration(out duration);

				trackBar.Maximum = checked((int)(duration / timeScalingFactor));
				trackBar.Enabled = (seekingCapabilities & AMSeekingSeekingCapabilities.CanSeekAbsolute) != 0;
			}
			else
			{
				trackBar.Maximum = 0;
				trackBar.Enabled = false;
			}
		}

		private void OnPlay(object sender, EventArgs e)
		{
			playPauseToolStripButton.Image = pauseIcon;
			playPauseToolStripButton.Checked = true;
			stopToolStripButton.Checked = false;
			timer.Enabled = true;
		}

		private void OnPause(object sender, EventArgs e)
		{
			playPauseToolStripButton.Image = playIcon;
			playPauseToolStripButton.Checked = false;
			stopToolStripButton.Checked = false;
			timer.Enabled = false;
		}

		private void OnStop(object sender, EventArgs e)
		{
			playPauseToolStripButton.Image = playIcon;
			playPauseToolStripButton.Checked = false;
			stopToolStripButton.Checked = true;
			timer.Enabled = false;
		}

		private void OnEnd(object sender, EventArgs e) { Stop(); }

		private void timer_Tick(object sender, EventArgs e)
		{
			if (updating) return;

			updating = true;

			if (mediaSeeking != null && trackBar.Enabled)
			{
				long currentPosition;

				mediaSeeking.GetCurrentPosition(out currentPosition);
				trackBar.Value = checked((int)(currentPosition / timeScalingFactor));
			}
			else trackBar.Value = 0;

			updating = false;
		}

		private void trackBar_ValueChanged(object sender, EventArgs e)
		{
			if (updating) return;

			updating = true;

			if (mediaSeeking != null && (seekingCapabilities & AMSeekingSeekingCapabilities.CanSeekAbsolute) != 0)
				mediaSeeking.SetPositions(DsLong.FromInt64(trackBar.Value * timeScalingFactor), AMSeekingSeekingFlags.AbsolutePositioning, null, AMSeekingSeekingFlags.NoPositioning);

			updating = false;
		}

		private void playPauseToolStripButton_Click(object sender, EventArgs e)
		{
			if (Playing)
				Pause();
			else
				Play();
		}

		private void stopToolStripButton_Click(object sender, EventArgs e)
		{
			Stop();
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			if (graphBuilder != null)
			{
				//if (renderPanel.ClientSize.Width > 0 && renderPanel.ClientSize.Height > 0)
				//    video.Size = renderPanel.Size;
			}
			base.OnSizeChanged(e);
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			if (!Visible)
				Stop();
			base.OnVisibleChanged(e);
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_GRAPHNOTIFY)
			{
				EventCode eventCode;
				IntPtr param1, param2;

				while (mediaEvent.GetEvent(out eventCode, out param1, out param2, 0) == 0)
				{
					if (eventCode == EventCode.Complete)
						OnEnd(this, EventArgs.Empty);
					System.Diagnostics.Debug.WriteLine("MediaPlayer: Event " + eventCode.ToString());
				}
			}
			else
				base.WndProc(ref m);
		}
	}
}
