using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000100 RID: 256
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyChatUpdate_t : ICallbackData
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x000157D2 File Offset: 0x000139D2
		public int DataSize
		{
			get
			{
				return LobbyChatUpdate_t._datasize;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x000157D9 File Offset: 0x000139D9
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyChatUpdate;
			}
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x000157E0 File Offset: 0x000139E0
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyChatUpdate_t()
		{
		}

		// Token: 0x04000873 RID: 2163
		internal ulong SteamIDLobby;

		// Token: 0x04000874 RID: 2164
		internal ulong SteamIDUserChanged;

		// Token: 0x04000875 RID: 2165
		internal ulong SteamIDMakingChange;

		// Token: 0x04000876 RID: 2166
		internal uint GfChatMemberStateChange;

		// Token: 0x04000877 RID: 2167
		public static int _datasize = Marshal.SizeOf(typeof(LobbyChatUpdate_t));
	}
}
