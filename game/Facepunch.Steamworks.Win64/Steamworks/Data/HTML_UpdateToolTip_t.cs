using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000180 RID: 384
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_UpdateToolTip_t : ICallbackData
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x00016B65 File Offset: 0x00014D65
		public int DataSize
		{
			get
			{
				return HTML_UpdateToolTip_t._datasize;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x00016B6C File Offset: 0x00014D6C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_UpdateToolTip;
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00016B73 File Offset: 0x00014D73
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_UpdateToolTip_t()
		{
		}

		// Token: 0x04000A65 RID: 2661
		internal uint UnBrowserHandle;

		// Token: 0x04000A66 RID: 2662
		internal string PchMsg;

		// Token: 0x04000A67 RID: 2663
		public static int _datasize = Marshal.SizeOf(typeof(HTML_UpdateToolTip_t));
	}
}
