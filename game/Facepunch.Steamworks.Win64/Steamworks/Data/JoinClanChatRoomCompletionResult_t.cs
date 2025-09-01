using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000EF RID: 239
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinClanChatRoomCompletionResult_t : ICallbackData
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0001556E File Offset: 0x0001376E
		public int DataSize
		{
			get
			{
				return JoinClanChatRoomCompletionResult_t._datasize;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00015575 File Offset: 0x00013775
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.JoinClanChatRoomCompletionResult;
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0001557C File Offset: 0x0001377C
		// Note: this type is marked as 'beforefieldinit'.
		static JoinClanChatRoomCompletionResult_t()
		{
		}

		// Token: 0x04000839 RID: 2105
		internal ulong SteamIDClanChat;

		// Token: 0x0400083A RID: 2106
		internal RoomEnter ChatRoomEnterResponse;

		// Token: 0x0400083B RID: 2107
		public static int _datasize = Marshal.SizeOf(typeof(JoinClanChatRoomCompletionResult_t));
	}
}
