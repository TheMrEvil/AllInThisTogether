using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000FE RID: 254
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyEnter_t : ICallbackData
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0001578A File Offset: 0x0001398A
		public int DataSize
		{
			get
			{
				return LobbyEnter_t._datasize;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00015791 File Offset: 0x00013991
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyEnter;
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00015798 File Offset: 0x00013998
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyEnter_t()
		{
		}

		// Token: 0x0400086A RID: 2154
		internal ulong SteamIDLobby;

		// Token: 0x0400086B RID: 2155
		internal uint GfChatPermissions;

		// Token: 0x0400086C RID: 2156
		[MarshalAs(UnmanagedType.I1)]
		internal bool Locked;

		// Token: 0x0400086D RID: 2157
		internal uint EChatRoomEnterResponse;

		// Token: 0x0400086E RID: 2158
		public static int _datasize = Marshal.SizeOf(typeof(LobbyEnter_t));
	}
}
