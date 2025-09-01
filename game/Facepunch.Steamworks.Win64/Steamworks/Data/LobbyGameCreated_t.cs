using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000102 RID: 258
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyGameCreated_t : ICallbackData
	{
		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001581A File Offset: 0x00013A1A
		public int DataSize
		{
			get
			{
				return LobbyGameCreated_t._datasize;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x00015821 File Offset: 0x00013A21
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyGameCreated;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x00015828 File Offset: 0x00013A28
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyGameCreated_t()
		{
		}

		// Token: 0x0400087D RID: 2173
		internal ulong SteamIDLobby;

		// Token: 0x0400087E RID: 2174
		internal ulong SteamIDGameServer;

		// Token: 0x0400087F RID: 2175
		internal uint IP;

		// Token: 0x04000880 RID: 2176
		internal ushort Port;

		// Token: 0x04000881 RID: 2177
		public static int _datasize = Marshal.SizeOf(typeof(LobbyGameCreated_t));
	}
}
