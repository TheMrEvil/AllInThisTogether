using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F5 RID: 245
	internal struct UnreadChatMessagesChanged_t : ICallbackData
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00015646 File Offset: 0x00013846
		public int DataSize
		{
			get
			{
				return UnreadChatMessagesChanged_t._datasize;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0001564D File Offset: 0x0001384D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.UnreadChatMessagesChanged;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00015654 File Offset: 0x00013854
		// Note: this type is marked as 'beforefieldinit'.
		static UnreadChatMessagesChanged_t()
		{
		}

		// Token: 0x04000850 RID: 2128
		public static int _datasize = Marshal.SizeOf(typeof(UnreadChatMessagesChanged_t));
	}
}
