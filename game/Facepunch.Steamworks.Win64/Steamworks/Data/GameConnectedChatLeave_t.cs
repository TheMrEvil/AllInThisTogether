using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000ED RID: 237
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct GameConnectedChatLeave_t : ICallbackData
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00015526 File Offset: 0x00013726
		public int DataSize
		{
			get
			{
				return GameConnectedChatLeave_t._datasize;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001552D File Offset: 0x0001372D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameConnectedChatLeave;
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00015534 File Offset: 0x00013734
		// Note: this type is marked as 'beforefieldinit'.
		static GameConnectedChatLeave_t()
		{
		}

		// Token: 0x04000832 RID: 2098
		internal ulong SteamIDClanChat;

		// Token: 0x04000833 RID: 2099
		internal ulong SteamIDUser;

		// Token: 0x04000834 RID: 2100
		[MarshalAs(UnmanagedType.I1)]
		internal bool Kicked;

		// Token: 0x04000835 RID: 2101
		[MarshalAs(UnmanagedType.I1)]
		internal bool Dropped;

		// Token: 0x04000836 RID: 2102
		public static int _datasize = Marshal.SizeOf(typeof(GameConnectedChatLeave_t));
	}
}
