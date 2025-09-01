using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000EB RID: 235
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameConnectedClanChatMsg_t : ICallbackData
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x000154DE File Offset: 0x000136DE
		public int DataSize
		{
			get
			{
				return GameConnectedClanChatMsg_t._datasize;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x000154E5 File Offset: 0x000136E5
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameConnectedClanChatMsg;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000154EC File Offset: 0x000136EC
		// Note: this type is marked as 'beforefieldinit'.
		static GameConnectedClanChatMsg_t()
		{
		}

		// Token: 0x0400082B RID: 2091
		internal ulong SteamIDClanChat;

		// Token: 0x0400082C RID: 2092
		internal ulong SteamIDUser;

		// Token: 0x0400082D RID: 2093
		internal int MessageID;

		// Token: 0x0400082E RID: 2094
		public static int _datasize = Marshal.SizeOf(typeof(GameConnectedClanChatMsg_t));
	}
}
