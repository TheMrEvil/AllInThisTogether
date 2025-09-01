using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x02000106 RID: 262
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct PSNGameBootInviteResult_t : ICallbackData
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x000158AA File Offset: 0x00013AAA
		public int DataSize
		{
			get
			{
				return PSNGameBootInviteResult_t._datasize;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x000158B1 File Offset: 0x00013AB1
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.PSNGameBootInviteResult;
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000158B8 File Offset: 0x00013AB8
		// Note: this type is marked as 'beforefieldinit'.
		static PSNGameBootInviteResult_t()
		{
		}

		// Token: 0x0400088B RID: 2187
		[MarshalAs(UnmanagedType.I1)]
		internal bool GameBootInviteExists;

		// Token: 0x0400088C RID: 2188
		internal ulong SteamIDLobby;

		// Token: 0x0400088D RID: 2189
		public static int _datasize = Marshal.SizeOf(typeof(PSNGameBootInviteResult_t));
	}
}
