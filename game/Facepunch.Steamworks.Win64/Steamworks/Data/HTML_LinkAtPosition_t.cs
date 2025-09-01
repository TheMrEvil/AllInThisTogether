using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000178 RID: 376
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HTML_LinkAtPosition_t : ICallbackData
	{
		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x00016A45 File Offset: 0x00014C45
		public int DataSize
		{
			get
			{
				return HTML_LinkAtPosition_t._datasize;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x00016A4C File Offset: 0x00014C4C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.HTML_LinkAtPosition;
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00016A53 File Offset: 0x00014C53
		// Note: this type is marked as 'beforefieldinit'.
		static HTML_LinkAtPosition_t()
		{
		}

		// Token: 0x04000A43 RID: 2627
		internal uint UnBrowserHandle;

		// Token: 0x04000A44 RID: 2628
		internal uint X;

		// Token: 0x04000A45 RID: 2629
		internal uint Y;

		// Token: 0x04000A46 RID: 2630
		internal string PchURL;

		// Token: 0x04000A47 RID: 2631
		[MarshalAs(UnmanagedType.I1)]
		internal bool BInput;

		// Token: 0x04000A48 RID: 2632
		[MarshalAs(UnmanagedType.I1)]
		internal bool BLiveLink;

		// Token: 0x04000A49 RID: 2633
		public static int _datasize = Marshal.SizeOf(typeof(HTML_LinkAtPosition_t));
	}
}
