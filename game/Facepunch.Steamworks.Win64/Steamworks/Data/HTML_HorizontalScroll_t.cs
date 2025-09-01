using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000176 RID: 374
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_HorizontalScroll_t : ICallbackData
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000169FD File Offset: 0x00014BFD
		public int DataSize
		{
			get
			{
				return HTML_HorizontalScroll_t._datasize;
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x00016A04 File Offset: 0x00014C04
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_HorizontalScroll;
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00016A0B File Offset: 0x00014C0B
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_HorizontalScroll_t()
		{
		}

		// Token: 0x04000A35 RID: 2613
		internal uint UnBrowserHandle;

		// Token: 0x04000A36 RID: 2614
		internal uint UnScrollMax;

		// Token: 0x04000A37 RID: 2615
		internal uint UnScrollCurrent;

		// Token: 0x04000A38 RID: 2616
		internal float FlPageScale;

		// Token: 0x04000A39 RID: 2617
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible;

		// Token: 0x04000A3A RID: 2618
		internal uint UnPageSize;

		// Token: 0x04000A3B RID: 2619
		public static int _datasize = Marshal.SizeOf(typeof(HTML_HorizontalScroll_t));
	}
}
