using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x02000B62 RID: 2914
	internal class HGlobalUnmanagedMemoryStream : UnmanagedMemoryStream
	{
		// Token: 0x060069C9 RID: 27081 RVA: 0x0016978B File Offset: 0x0016798B
		public unsafe HGlobalUnmanagedMemoryStream(byte* pointer, long length, IntPtr ptr) : base(pointer, length, length, FileAccess.ReadWrite)
		{
			this.ptr = ptr;
		}

		// Token: 0x060069CA RID: 27082 RVA: 0x0016979E File Offset: 0x0016799E
		protected override void Dispose(bool disposing)
		{
			if (this._isOpen)
			{
				Marshal.FreeHGlobal(this.ptr);
			}
			base.Dispose(disposing);
		}

		// Token: 0x04003D4D RID: 15693
		private IntPtr ptr;
	}
}
