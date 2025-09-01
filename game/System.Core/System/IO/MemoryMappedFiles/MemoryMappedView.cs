using System;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace System.IO.MemoryMappedFiles
{
	// Token: 0x0200033C RID: 828
	internal class MemoryMappedView : IDisposable
	{
		// Token: 0x06001913 RID: 6419 RVA: 0x0005426B File Offset: 0x0005246B
		[SecurityCritical]
		private MemoryMappedView(SafeMemoryMappedViewHandle viewHandle, long pointerOffset, long size, MemoryMappedFileAccess access)
		{
			this.m_viewHandle = viewHandle;
			this.m_pointerOffset = pointerOffset;
			this.m_size = size;
			this.m_access = access;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x00054290 File Offset: 0x00052490
		internal SafeMemoryMappedViewHandle ViewHandle
		{
			[SecurityCritical]
			get
			{
				return this.m_viewHandle;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x00054298 File Offset: 0x00052498
		internal long PointerOffset
		{
			get
			{
				return this.m_pointerOffset;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x000542A0 File Offset: 0x000524A0
		internal long Size
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x000542A8 File Offset: 0x000524A8
		internal MemoryMappedFileAccess Access
		{
			get
			{
				return this.m_access;
			}
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x000542B0 File Offset: 0x000524B0
		internal static MemoryMappedView Create(IntPtr handle, long offset, long size, MemoryMappedFileAccess access)
		{
			IntPtr mmap_handle;
			IntPtr base_address;
			MemoryMapImpl.Map(handle, offset, ref size, access, out mmap_handle, out base_address);
			return new MemoryMappedView(new SafeMemoryMappedViewHandle(mmap_handle, base_address, size), 0L, size, access);
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x000542DC File Offset: 0x000524DC
		public void Flush(IntPtr capacity)
		{
			this.m_viewHandle.Flush();
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000542E9 File Offset: 0x000524E9
		protected virtual void Dispose(bool disposing)
		{
			if (this.m_viewHandle != null && !this.m_viewHandle.IsClosed)
			{
				this.m_viewHandle.Dispose();
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0005430B File Offset: 0x0005250B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600191C RID: 6428 RVA: 0x0005431A File Offset: 0x0005251A
		internal bool IsClosed
		{
			get
			{
				return this.m_viewHandle == null || this.m_viewHandle.IsClosed;
			}
		}

		// Token: 0x04000C0B RID: 3083
		private SafeMemoryMappedViewHandle m_viewHandle;

		// Token: 0x04000C0C RID: 3084
		private long m_pointerOffset;

		// Token: 0x04000C0D RID: 3085
		private long m_size;

		// Token: 0x04000C0E RID: 3086
		private MemoryMappedFileAccess m_access;
	}
}
