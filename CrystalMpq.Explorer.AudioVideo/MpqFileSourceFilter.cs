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
using System.IO;
using DirectShowLib;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace CrystalMpq.Explorer.AudioVideo
{
	//[Guid("a116a7a1-1f52-4be4-bb6a-08a71090771f")]
	sealed class MpqFileSourceFilter : IBaseFilter, IFileSourceFilter
	{
		const int E_FAIL = unchecked((int)0x80004005);
		const int E_POINTER = unchecked((int)0x80004003);
		const int E_UNEXPECTED = unchecked((int)0x8000FFFF);
		const int S_OK = 0;
		const int S_FALSE = 1;

		private static readonly AMMediaType defaultMediaType = new AMMediaType()
		{
			majorType = MediaType.Stream,
			subType = MediaSubType.None,
			fixedSizeSamples = false,
			temporalCompression = false,
			sampleSize = 0,
			formatType = FormatType.None,
			unkPtr = IntPtr.Zero,
			formatSize = 0,
			formatPtr = IntPtr.Zero
		};

		private static void CopyMediaTypes(AMMediaType destination, AMMediaType source)
		{
			destination.majorType = source.majorType;
			destination.subType = source.subType;
			destination.fixedSizeSamples = source.fixedSizeSamples;
			destination.temporalCompression = source.temporalCompression;
			destination.sampleSize = source.sampleSize;
			destination.formatType = source.formatType;
			destination.unkPtr = source.unkPtr;
			destination.formatSize = source.formatSize;
			destination.formatPtr = source.formatPtr;
		}

		#region StreamOutputPin Class

		sealed class StreamOutputPin : IPin, IAsyncReader
		{
			#region MediaTypeEnumerator Class

			sealed class MediaTypeEnumerator : IEnumMediaTypes
			{
				StreamOutputPin pin;
				int index;

				public MediaTypeEnumerator(StreamOutputPin pin) { this.pin = pin; }

				public int Clone(out IEnumMediaTypes ppEnum)
				{
					ppEnum = new MediaTypeEnumerator(pin) { index = index };
					return S_OK;
				}

				public unsafe int Next(int cMediaTypes, AMMediaType[] ppMediaTypes, IntPtr pcFetched)
				{
					if (cMediaTypes > 0 && index++ == 0 && ppMediaTypes != null)
					{
						if (pcFetched != IntPtr.Zero)
							*((int*)pcFetched) = 1;
						ppMediaTypes[0] = new AMMediaType();
						CopyMediaTypes(ppMediaTypes[0], pin.mediaType);
						return cMediaTypes == 1 ? S_OK : S_FALSE;
					}
					else
					{
						if (pcFetched != IntPtr.Zero)
							*((int*)pcFetched) = 0;
						return S_FALSE;
					}
				}

				public int Reset() { index = 0; return S_OK; }
				public int Skip(int cMediaTypes) { index += cMediaTypes; return S_FALSE; }
			}

			#endregion

			MpqFileSourceFilter filter;
			string name;
			IPin connectedPin;
			AMMediaType mediaType;

			public StreamOutputPin(MpqFileSourceFilter filter)
			{
				this.filter = filter; name = "Output";

				mediaType = new AMMediaType();

				CopyMediaTypes(mediaType, filter.detectedMediaType);
			}

			#region IPin Implementation

			int IPin.Connect(IPin pReceivePin, AMMediaType pmt)
			{
				IEnumMediaTypes emt;

				if (connectedPin != null)
					return DsResults.E_AlreadyConnected;
				if (pReceivePin == null)
					return E_POINTER;
				if (pReceivePin.EnumMediaTypes(out emt) == S_OK)
				{
					var mediaTypes = new AMMediaType[1];

					while (emt.Next(1, mediaTypes, IntPtr.Zero) == S_OK)
						if (mediaTypes[0].majorType == MediaType.Stream && pReceivePin.ReceiveConnection(this, mediaTypes[0]) == S_OK)
						{
							emt = null;
							connectedPin = pReceivePin;
							return S_OK;
						}
					emt = null;
				}
				if (pmt != null && pmt.majorType != MediaType.Null && pmt.majorType != MediaType.Stream)
					return DsResults.E_TypeNotAccepted;
				if (pReceivePin.ReceiveConnection(this, mediaType) == S_OK)
				{
					connectedPin = pReceivePin;
					return S_OK;
				}
				return DsResults.E_NoAcceptableTypes;
			}

			int IPin.ReceiveConnection(IPin pReceivePin, AMMediaType pmt) { return E_FAIL; }

			int IPin.Disconnect()
			{
				if (connectedPin == null) return S_FALSE;

				connectedPin = null;
				CopyMediaTypes(mediaType, filter.detectedMediaType);

				return S_OK;
			}

			int IPin.ConnectedTo(out IPin ppPin)
			{
				if ((ppPin = connectedPin) == null)
					return DsResults.E_NotConnected;
				else
					return S_OK;
			}

			int IPin.ConnectionMediaType(AMMediaType pmt)
			{
				if (connectedPin == null) return DsResults.E_NotConnected;
				if (pmt == null) return E_POINTER;

				CopyMediaTypes(pmt, mediaType);

				return S_OK;
			}

			int IPin.QueryPinInfo(out PinInfo pInfo)
			{
				pInfo.dir = PinDirection.Output;
				pInfo.filter = filter;
				pInfo.name = name;

				return S_OK;
			}

			int IPin.QueryId(out string Id) { Id = name; return S_OK; }

			int IPin.QueryAccept(AMMediaType pmt)
			{
				if (pmt == null) return E_POINTER;
				else if (pmt.majorType == MediaType.Stream) return S_OK;
				else return S_FALSE;
			}

			int IPin.EnumMediaTypes(out IEnumMediaTypes ppEnum)
			{
				ppEnum = new MediaTypeEnumerator(this);
				return S_OK;
			}

			int IPin.QueryInternalConnections(IPin[] ppPins, ref int nPin) { nPin = 0; return S_OK; }

			int IPin.EndOfStream() { return E_UNEXPECTED; }
			int IPin.BeginFlush() { return E_UNEXPECTED; }
			int IPin.EndFlush() { return E_UNEXPECTED; }
			int IPin.NewSegment(long tStart, long tStop, double dRate) { return E_UNEXPECTED; }

			int IPin.QueryDirection(out PinDirection pPinDir) { pPinDir = PinDirection.Output; return S_OK; }

			#endregion

			#region IAsyncReader Implementation

			int IAsyncReader.BeginFlush()
			{
				throw new NotImplementedException();
			}

			int IAsyncReader.EndFlush()
			{
				throw new NotImplementedException();
			}

			int IAsyncReader.Length(out long pTotal, out long pAvailable)
			{
				pTotal = filter.stream.Length;
				pAvailable = filter.stream.Length;
				return S_OK;
			}

			int IAsyncReader.RequestAllocator(IMemAllocator pPreferred, AllocatorProperties pProps, out IMemAllocator ppActual)
			{
				if (pPreferred != null)
				{
					ppActual = pPreferred;
					if (pProps != null)
						pPreferred.GetProperties(pProps);
					return S_OK;
				}
				ppActual = null;
				return E_FAIL;
			}

			int IAsyncReader.Request(IMediaSample pSample, IntPtr dwUser)
			{
				throw new NotImplementedException();
			}

			int IAsyncReader.SyncReadAligned(IMediaSample pSample)
			{
				throw new NotImplementedException();
			}

			int IAsyncReader.SyncRead(long llPosition, int lLength, IntPtr pBuffer)
			{
				throw new NotImplementedException();
			}

			int IAsyncReader.WaitForNext(int dwTimeout, out IMediaSample ppSample, out IntPtr pdwUser)
			{
				throw new NotImplementedException();
			}

			#endregion
		}

		#endregion

		#region PinEnumerator Class

		sealed class PinEnumerator : IEnumPins
		{
			MpqFileSourceFilter filter;
			int index;

			public PinEnumerator(MpqFileSourceFilter filter) { this.filter = filter; }

			public int Clone(out IEnumPins ppEnum)
			{
				ppEnum = new PinEnumerator(filter) { index = index };
				return S_OK;
			}

			public unsafe int Next(int cPins, IPin[] ppPins, IntPtr pcFetched)
			{
				if (cPins > 0 && index++ == 0 && ppPins != null)
				{
					if (pcFetched != IntPtr.Zero)
						*((int*)pcFetched) = 1;
					ppPins[0] = filter.outputPin;
					return cPins == 1 ? S_OK : S_FALSE;
				}
				else
				{
					if (pcFetched != IntPtr.Zero)
						*((int*)pcFetched) = 0;
					return S_FALSE;
				}
			}

			public int Reset()
			{
				index = 0;
				return S_OK;
			}

			public int Skip(int cPins)
			{
				index += cPins;
				return S_FALSE;
			}
		}

		#endregion

		string name;
		MpqFile file;
		MpqFileStream stream;
		FilterState filterState;
		IFilterGraph filterGraph;
		IReferenceClock syncSource;
		StreamOutputPin outputPin;
		AMMediaType detectedMediaType = new AMMediaType();

		public MpqFileSourceFilter(MpqFile file)
		{
			CopyMediaTypes(detectedMediaType, defaultMediaType);
			name = "Stream Source Filter";
			filterState = FilterState.Stopped;
			var extension = Path.GetExtension(file.Name);
			try
			{
				using (var mediaTypeKey = Registry.ClassesRoot.OpenSubKey(@"Media Type"))
				{
					if (extension != null && extension.Length > 1)
						try
						{
							using (var extensionsKey = mediaTypeKey.OpenSubKey(@"Extensions"))
							using (var extensionKey = extensionsKey.OpenSubKey(extension))
							{
								var mediaType = new Guid(extensionKey.GetValue("Media Type", MediaType.Stream.ToString()) as string);
								var subType = new Guid(extensionKey.GetValue("SubType", MediaSubType.None.ToString()) as string);

								if (mediaType == MediaType.Stream && subType != MediaSubType.None && subType != MediaSubType.Null)
								{
									detectedMediaType.majorType = mediaType;
									detectedMediaType.subType = subType;

									return;
								}
							}
						}
						catch (Exception) { }
					using (var streamMediaKey = mediaTypeKey.OpenSubKey(MediaType.Stream.ToString()))
						foreach (var subTypeKeyName in streamMediaKey.GetSubKeyNames())
							try
							{
								using (var subTypeKey = streamMediaKey.OpenSubKey(subTypeKeyName))
									;
							}
							catch (Exception) { }
				}
			}
			catch (Exception) { }
			finally { outputPin = new StreamOutputPin(this); }
		}

		public string Name { get { return name; } }
		public IPin OutputPin { get { return (IPin)outputPin; } }

		#region IBaseFilter Implementation

		int IBaseFilter.EnumPins(out IEnumPins ppEnum)
		{
			ppEnum = new PinEnumerator(this);
			return S_OK;
		}

		int IBaseFilter.FindPin(string Id, out IPin ppPin)
		{
			if (Id == "Output")
			{
				ppPin = outputPin;
				return S_OK;
			}
			else
			{
				ppPin = null;
				return DsResults.E_NotFound;
			}
		}

		public int GetClassID(out Guid pClassID)
		{
			pClassID = typeof(MpqFileSourceFilter).GUID;
			return S_OK;
		}

		public int GetState(int dwMilliSecsTimeout, out FilterState filtState)
		{
			filtState = filterState;
			return S_OK;
		}

		public int GetSyncSource(out IReferenceClock pClock)
		{
			pClock = syncSource;
			return S_OK;
		}

		public int SetSyncSource(IReferenceClock pClock)
		{
			syncSource = pClock;
			return S_OK;
		}

		int IBaseFilter.JoinFilterGraph(IFilterGraph pGraph, string pName)
		{
			if ((filterGraph = pGraph) == null)
				name = "Stream Source Filter";
			else
				name = pName;
			return S_OK;
		}

		public int Run(long tStart) { return S_OK; }
		public int Pause() { return S_OK; }
		public int Stop() { return S_OK; }

		int IBaseFilter.QueryFilterInfo(out FilterInfo pInfo)
		{
			pInfo.achName = name;
			pInfo.pGraph = filterGraph;
			return S_OK;
		}

		int IBaseFilter.QueryVendorInfo(out string pVendorInfo)
		{
			pVendorInfo = "CrystalMpq";
			return S_OK;
		}

		#endregion

		#region IFileSourceFilter Implementation

		int IFileSourceFilter.GetCurFile(out string pszFileName, AMMediaType pmt)
		{
			pszFileName = file.Name;

			if (pmt != null)
				CopyMediaTypes(pmt, detectedMediaType);

			return S_OK;
		}

		int IFileSourceFilter.Load(string pszFileName, AMMediaType pmt)
		{
			return E_FAIL;
		}

		#endregion
	}
}
