using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020000EA RID: 234
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GameRichPresenceJoinRequested_t : ICallbackData
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x0001549B File Offset: 0x0001369B
		internal string ConnectUTF8()
		{
			return Encoding.UTF8.GetString(this.Connect, 0, Array.IndexOf<byte>(this.Connect, 0));
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x000154BA File Offset: 0x000136BA
		public int DataSize
		{
			get
			{
				return GameRichPresenceJoinRequested_t._datasize;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x000154C1 File Offset: 0x000136C1
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GameRichPresenceJoinRequested;
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000154C8 File Offset: 0x000136C8
		// Note: this type is marked as 'beforefieldinit'.
		static GameRichPresenceJoinRequested_t()
		{
		}

		// Token: 0x04000828 RID: 2088
		internal ulong SteamIDFriend;

		// Token: 0x04000829 RID: 2089
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] Connect;

		// Token: 0x0400082A RID: 2090
		public static int _datasize = Marshal.SizeOf(typeof(GameRichPresenceJoinRequested_t));
	}
}
