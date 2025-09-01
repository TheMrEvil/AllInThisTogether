using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000174 RID: 372
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_SearchResults_t : ICallbackData
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x000169B5 File Offset: 0x00014BB5
		public int DataSize
		{
			get
			{
				return HTML_SearchResults_t._datasize;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x000169BC File Offset: 0x00014BBC
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_SearchResults;
			}
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x000169C3 File Offset: 0x00014BC3
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_SearchResults_t()
		{
		}

		// Token: 0x04000A2D RID: 2605
		internal uint UnBrowserHandle;

		// Token: 0x04000A2E RID: 2606
		internal uint UnResults;

		// Token: 0x04000A2F RID: 2607
		internal uint UnCurrentMatch;

		// Token: 0x04000A30 RID: 2608
		public static int _datasize = Marshal.SizeOf(typeof(HTML_SearchResults_t));
	}
}
