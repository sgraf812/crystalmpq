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
using DirectShowLib;

namespace CrystalMpq.Explorer.AudioVideo
{
	class ManagedMemAllocator : IMemAllocator
	{
		#region IMemAllocator Implementation

		int IMemAllocator.Commit()
		{
			throw new NotImplementedException();
		}

		int IMemAllocator.Decommit()
		{
			throw new NotImplementedException();
		}

		int IMemAllocator.GetBuffer(out IMediaSample ppBuffer, long pStartTime, long pEndTime, AMGBF dwFlags)
		{
			throw new NotImplementedException();
		}

		int IMemAllocator.GetProperties(AllocatorProperties pProps)
		{
			throw new NotImplementedException();
		}

		int IMemAllocator.ReleaseBuffer(IMediaSample pBuffer)
		{
			throw new NotImplementedException();
		}

		int IMemAllocator.SetProperties(AllocatorProperties pRequest, AllocatorProperties pActual)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
