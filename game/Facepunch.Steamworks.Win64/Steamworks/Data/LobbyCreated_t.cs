using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000105 RID: 261
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyCreated_t : ICallbackData
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00015886 File Offset: 0x00013A86
		public int DataSize
		{
			get
			{
				return LobbyCreated_t._datasize;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0001588D File Offset: 0x00013A8D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.LobbyCreated;
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00015894 File Offset: 0x00013A94
		// Note: this type is marked as 'beforefieldinit'.
		static LobbyCreated_t()
		{
		}

		// Token: 0x04000888 RID: 2184
		internal Result Result;

		// Token: 0x04000889 RID: 2185
		internal ulong SteamIDLobby;

		// Token: 0x0400088A RID: 2186
		public static int _datasize = Marshal.SizeOf(typeof(LobbyCreated_t));
	}
}
