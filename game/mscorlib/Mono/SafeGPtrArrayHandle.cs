using System;

namespace Mono
{
	// Token: 0x0200005D RID: 93
	internal struct SafeGPtrArrayHandle : IDisposable
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00004B0D File Offset: 0x00002D0D
		internal SafeGPtrArrayHandle(IntPtr ptr)
		{
			this.handle = new RuntimeGPtrArrayHandle(ptr);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004B1B File Offset: 0x00002D1B
		public void Dispose()
		{
			RuntimeGPtrArrayHandle.DestroyAndFree(ref this.handle);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004B28 File Offset: 0x00002D28
		internal int Length
		{
			get
			{
				return this.handle.Length;
			}
		}

		// Token: 0x17000011 RID: 17
		internal IntPtr this[int i]
		{
			get
			{
				return this.handle[i];
			}
		}

		// Token: 0x04000E0F RID: 3599
		private RuntimeGPtrArrayHandle handle;
	}
}
