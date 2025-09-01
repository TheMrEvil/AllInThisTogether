using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000E6 RID: 230
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameLobbyJoinRequested_t : ICallbackData
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0001540B File Offset: 0x0001360B
		public int DataSize
		{
			get
			{
				return GameLobbyJoinRequested_t._datasize;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00015412 File Offset: 0x00013612
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameLobbyJoinRequested;
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00015419 File Offset: 0x00013619
		// Note: this type is marked as 'beforefieldinit'.
		static GameLobbyJoinRequested_t()
		{
		}

		// Token: 0x04000819 RID: 2073
		internal ulong SteamIDLobby;

		// Token: 0x0400081A RID: 2074
		internal ulong SteamIDFriend;

		// Token: 0x0400081B RID: 2075
		public static int _datasize = Marshal.SizeOf(typeof(GameLobbyJoinRequested_t));
	}
}
