using System;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x02000AD8 RID: 2776
	public struct MemoryHandle : IDisposable
	{
		// Token: 0x060062E7 RID: 25319 RVA: 0x0014AB74 File Offset: 0x00148D74
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060062E8 RID: 25320 RVA: 0x0014AB8B File Offset: 0x00148D8B
		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x060062E9 RID: 25321 RVA: 0x0014AB93 File Offset: 0x00148D93
		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._pinnable != null)
			{
				this._pinnable.Unpin();
				this._pinnable = null;
			}
			this._pointer = null;
		}

		// Token: 0x04003A45 RID: 14917
		private unsafe void* _pointer;

		// Token: 0x04003A46 RID: 14918
		private GCHandle _handle;

		// Token: 0x04003A47 RID: 14919
		private IPinnable _pinnable;
	}
}
