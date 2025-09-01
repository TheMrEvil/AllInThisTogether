using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000F0 RID: 240
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameConnectedFriendChatMsg_t : ICallbackData
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00015592 File Offset: 0x00013792
		public int DataSize
		{
			get
			{
				return GameConnectedFriendChatMsg_t._datasize;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00015599 File Offset: 0x00013799
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameConnectedFriendChatMsg;
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000155A0 File Offset: 0x000137A0
		// Note: this type is marked as 'beforefieldinit'.
		static GameConnectedFriendChatMsg_t()
		{
		}

		// Token: 0x0400083C RID: 2108
		internal ulong SteamIDUser;

		// Token: 0x0400083D RID: 2109
		internal int MessageID;

		// Token: 0x0400083E RID: 2110
		public static int _datasize = Marshal.SizeOf(typeof(GameConnectedFriendChatMsg_t));
	}
}
