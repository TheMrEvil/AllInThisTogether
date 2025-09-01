using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000FF RID: 255
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDataUpdate_t : ICallbackData
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x000157AE File Offset: 0x000139AE
		public int DataSize
		{
			get
			{
				return LobbyDataUpdate_t._datasize;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x000157B5 File Offset: 0x000139B5
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyDataUpdate;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000157BC File Offset: 0x000139BC
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyDataUpdate_t()
		{
		}

		// Token: 0x0400086F RID: 2159
		internal ulong SteamIDLobby;

		// Token: 0x04000870 RID: 2160
		internal ulong SteamIDMember;

		// Token: 0x04000871 RID: 2161
		internal byte Success;

		// Token: 0x04000872 RID: 2162
		public static int _datasize = Marshal.SizeOf(typeof(LobbyDataUpdate_t));
	}
}
