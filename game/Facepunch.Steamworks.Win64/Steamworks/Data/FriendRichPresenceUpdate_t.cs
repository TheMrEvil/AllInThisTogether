using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000E9 RID: 233
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct FriendRichPresenceUpdate_t : ICallbackData
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000B29 RID: 2857 RVA: 0x00015477 File Offset: 0x00013677
		public int DataSize
		{
			get
			{
				return FriendRichPresenceUpdate_t._datasize;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001547E File Offset: 0x0001367E
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.FriendRichPresenceUpdate;
			}
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00015485 File Offset: 0x00013685
		// Note: this type is marked as 'beforefieldinit'.
		static FriendRichPresenceUpdate_t()
		{
		}

		// Token: 0x04000825 RID: 2085
		internal ulong SteamIDFriend;

		// Token: 0x04000826 RID: 2086
		internal AppId AppID;

		// Token: 0x04000827 RID: 2087
		public static int _datasize = Marshal.SizeOf(typeof(FriendRichPresenceUpdate_t));
	}
}
