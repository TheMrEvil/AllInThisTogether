using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x0200051D RID: 1309
	internal struct kevent : IDisposable
	{
		// Token: 0x06002A54 RID: 10836 RVA: 0x00091928 File Offset: 0x0008FB28
		public void Dispose()
		{
			if (this.udata != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.udata);
			}
		}

		// Token: 0x040016BF RID: 5823
		public UIntPtr ident;

		// Token: 0x040016C0 RID: 5824
		public EventFilter filter;

		// Token: 0x040016C1 RID: 5825
		public EventFlags flags;

		// Token: 0x040016C2 RID: 5826
		public FilterFlags fflags;

		// Token: 0x040016C3 RID: 5827
		public IntPtr data;

		// Token: 0x040016C4 RID: 5828
		public IntPtr udata;
	}
}
