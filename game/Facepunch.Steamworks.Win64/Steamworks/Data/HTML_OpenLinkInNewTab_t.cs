using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000172 RID: 370
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_OpenLinkInNewTab_t : ICallbackData
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0001696D File Offset: 0x00014B6D
		public int DataSize
		{
			get
			{
				return HTML_OpenLinkInNewTab_t._datasize;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00016974 File Offset: 0x00014B74
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_OpenLinkInNewTab;
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0001697B File Offset: 0x00014B7B
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_OpenLinkInNewTab_t()
		{
		}

		// Token: 0x04000A27 RID: 2599
		internal uint UnBrowserHandle;

		// Token: 0x04000A28 RID: 2600
		internal string PchURL;

		// Token: 0x04000A29 RID: 2601
		public static int _datasize = Marshal.SizeOf(typeof(HTML_OpenLinkInNewTab_t));
	}
}
