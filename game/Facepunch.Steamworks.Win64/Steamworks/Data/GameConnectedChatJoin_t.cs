using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000EC RID: 236
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameConnectedChatJoin_t : ICallbackData
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00015502 File Offset: 0x00013702
		public int DataSize
		{
			get
			{
				return GameConnectedChatJoin_t._datasize;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00015509 File Offset: 0x00013709
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameConnectedChatJoin;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00015510 File Offset: 0x00013710
		// Note: this type is marked as 'beforefieldinit'.
		static GameConnectedChatJoin_t()
		{
		}

		// Token: 0x0400082F RID: 2095
		internal ulong SteamIDClanChat;

		// Token: 0x04000830 RID: 2096
		internal ulong SteamIDUser;

		// Token: 0x04000831 RID: 2097
		public static int _datasize = Marshal.SizeOf(typeof(GameConnectedChatJoin_t));
	}
}
