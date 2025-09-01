using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200017D RID: 381
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_SetCursor_t : ICallbackData
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x00016AF9 File Offset: 0x00014CF9
		public int DataSize
		{
			get
			{
				return HTML_SetCursor_t._datasize;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x00016B00 File Offset: 0x00014D00
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_SetCursor;
			}
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00016B07 File Offset: 0x00014D07
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_SetCursor_t()
		{
		}

		// Token: 0x04000A5C RID: 2652
		internal uint UnBrowserHandle;

		// Token: 0x04000A5D RID: 2653
		internal uint EMouseCursor;

		// Token: 0x04000A5E RID: 2654
		public static int _datasize = Marshal.SizeOf(typeof(HTML_SetCursor_t));
	}
}
