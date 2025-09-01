using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000181 RID: 385
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_HideToolTip_t : ICallbackData
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x00016B89 File Offset: 0x00014D89
		public int DataSize
		{
			get
			{
				return HTML_HideToolTip_t._datasize;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x00016B90 File Offset: 0x00014D90
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_HideToolTip;
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00016B97 File Offset: 0x00014D97
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_HideToolTip_t()
		{
		}

		// Token: 0x04000A68 RID: 2664
		internal uint UnBrowserHandle;

		// Token: 0x04000A69 RID: 2665
		public static int _datasize = Marshal.SizeOf(typeof(HTML_HideToolTip_t));
	}
}
