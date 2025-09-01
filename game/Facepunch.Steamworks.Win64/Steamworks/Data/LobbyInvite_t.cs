using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000FD RID: 253
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyInvite_t : ICallbackData
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00015766 File Offset: 0x00013966
		public int DataSize
		{
			get
			{
				return LobbyInvite_t._datasize;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001576D File Offset: 0x0001396D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyInvite;
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00015774 File Offset: 0x00013974
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyInvite_t()
		{
		}

		// Token: 0x04000866 RID: 2150
		internal ulong SteamIDUser;

		// Token: 0x04000867 RID: 2151
		internal ulong SteamIDLobby;

		// Token: 0x04000868 RID: 2152
		internal ulong GameID;

		// Token: 0x04000869 RID: 2153
		public static int _datasize = Marshal.SizeOf(typeof(LobbyInvite_t));
	}
}
