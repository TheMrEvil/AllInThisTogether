using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200015C RID: 348
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SubmitItemUpdateResult_t : ICallbackData
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00016655 File Offset: 0x00014855
		public int DataSize
		{
			get
			{
				return SubmitItemUpdateResult_t._datasize;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000C91 RID: 3217 RVA: 0x0001665C File Offset: 0x0001485C
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SubmitItemUpdateResult;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00016663 File Offset: 0x00014863
		// Note: this type is marked as 'beforefieldinit'.
		static SubmitItemUpdateResult_t()
		{
		}

		// Token: 0x040009CB RID: 2507
		internal Result Result;

		// Token: 0x040009CC RID: 2508
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x040009CD RID: 2509
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009CE RID: 2510
		public static int _datasize = Marshal.SizeOf(typeof(SubmitItemUpdateResult_t));
	}
}
