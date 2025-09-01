using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200016E RID: 366
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_StartRequest_t : ICallbackData
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x000168DD File Offset: 0x00014ADD
		public int DataSize
		{
			get
			{
				return HTML_StartRequest_t._datasize;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x000168E4 File Offset: 0x00014AE4
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_StartRequest;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000168EB File Offset: 0x00014AEB
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_StartRequest_t()
		{
		}

		// Token: 0x04000A14 RID: 2580
		internal uint UnBrowserHandle;

		// Token: 0x04000A15 RID: 2581
		internal string PchURL;

		// Token: 0x04000A16 RID: 2582
		internal string PchTarget;

		// Token: 0x04000A17 RID: 2583
		internal string PchPostData;

		// Token: 0x04000A18 RID: 2584
		[MarshalAs(UnmanagedType.I1)]
		internal bool BIsRedirect;

		// Token: 0x04000A19 RID: 2585
		public static int _datasize = Marshal.SizeOf(typeof(HTML_StartRequest_t));
	}
}
