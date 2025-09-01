using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x0200015B RID: 347
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateItemResult_t : ICallbackData
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00016631 File Offset: 0x00014831
		public int DataSize
		{
			get
			{
				return CreateItemResult_t._datasize;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00016638 File Offset: 0x00014838
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.CreateItemResult;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0001663F File Offset: 0x0001483F
		// Note: this type is marked as 'beforefieldinit'.
		static CreateItemResult_t()
		{
		}

		// Token: 0x040009C7 RID: 2503
		internal Result Result;

		// Token: 0x040009C8 RID: 2504
		internal PublishedFileId PublishedFileId;

		// Token: 0x040009C9 RID: 2505
		[MarshalAs(UnmanagedType.I1)]
		internal bool UserNeedsToAcceptWorkshopLegalAgreement;

		// Token: 0x040009CA RID: 2506
		public static int _datasize = Marshal.SizeOf(typeof(CreateItemResult_t));
	}
}
