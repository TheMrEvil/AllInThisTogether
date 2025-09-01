using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000177 RID: 375
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_VerticalScroll_t : ICallbackData
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00016A21 File Offset: 0x00014C21
		public int DataSize
		{
			get
			{
				return HTML_VerticalScroll_t._datasize;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00016A28 File Offset: 0x00014C28
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_VerticalScroll;
			}
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x00016A2F File Offset: 0x00014C2F
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_VerticalScroll_t()
		{
		}

		// Token: 0x04000A3C RID: 2620
		internal uint UnBrowserHandle;

		// Token: 0x04000A3D RID: 2621
		internal uint UnScrollMax;

		// Token: 0x04000A3E RID: 2622
		internal uint UnScrollCurrent;

		// Token: 0x04000A3F RID: 2623
		internal float FlPageScale;

		// Token: 0x04000A40 RID: 2624
		[MarshalAs(UnmanagedType.I1)]
		internal bool BVisible;

		// Token: 0x04000A41 RID: 2625
		internal uint UnPageSize;

		// Token: 0x04000A42 RID: 2626
		public static int _datasize = Marshal.SizeOf(typeof(HTML_VerticalScroll_t));
	}
}
