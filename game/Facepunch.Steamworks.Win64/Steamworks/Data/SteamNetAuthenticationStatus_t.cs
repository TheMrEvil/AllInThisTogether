using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000191 RID: 401
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SteamNetAuthenticationStatus_t : ICallbackData
	{
		// Token: 0x06000D31 RID: 3377 RVA: 0x00016E07 File Offset: 0x00015007
		internal string DebugMsgUTF8()
		{
			return Encoding.UTF8.GetString(this.DebugMsg, 0, Array.IndexOf<byte>(this.DebugMsg, 0));
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x00016E26 File Offset: 0x00015026
		public int DataSize
		{
			get
			{
				return SteamNetAuthenticationStatus_t._datasize;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x00016E2D File Offset: 0x0001502D
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.SteamNetAuthenticationStatus;
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00016E34 File Offset: 0x00015034
		// Note: this type is marked as 'beforefieldinit'.
		static SteamNetAuthenticationStatus_t()
		{
		}

		// Token: 0x04000A93 RID: 2707
		internal SteamNetworkingAvailability Avail;

		// Token: 0x04000A94 RID: 2708
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] DebugMsg;

		// Token: 0x04000A95 RID: 2709
		public static int _datasize = Marshal.SizeOf(typeof(SteamNetAuthenticationStatus_t));
	}
}
