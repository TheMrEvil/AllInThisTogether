using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000104 RID: 260
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyKicked_t : ICallbackData
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00015862 File Offset: 0x00013A62
		public int DataSize
		{
			get
			{
				return LobbyKicked_t._datasize;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00015869 File Offset: 0x00013A69
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyKicked;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00015870 File Offset: 0x00013A70
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyKicked_t()
		{
		}

		// Token: 0x04000884 RID: 2180
		internal ulong SteamIDLobby;

		// Token: 0x04000885 RID: 2181
		internal ulong SteamIDAdmin;

		// Token: 0x04000886 RID: 2182
		internal byte KickedDueToDisconnect;

		// Token: 0x04000887 RID: 2183
		public static int _datasize = Marshal.SizeOf(typeof(LobbyKicked_t));
	}
}
