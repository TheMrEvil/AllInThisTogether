using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000175 RID: 373
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_CanGoBackAndForward_t : ICallbackData
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x000169D9 File Offset: 0x00014BD9
		public int DataSize
		{
			get
			{
				return HTML_CanGoBackAndForward_t._datasize;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x000169E0 File Offset: 0x00014BE0
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_CanGoBackAndForward;
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000169E7 File Offset: 0x00014BE7
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_CanGoBackAndForward_t()
		{
		}

		// Token: 0x04000A31 RID: 2609
		internal uint UnBrowserHandle;

		// Token: 0x04000A32 RID: 2610
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoBack;

		// Token: 0x04000A33 RID: 2611
		[MarshalAs(UnmanagedType.I1)]
		internal bool BCanGoForward;

		// Token: 0x04000A34 RID: 2612
		public static int _datasize = Marshal.SizeOf(typeof(HTML_CanGoBackAndForward_t));
	}
}
