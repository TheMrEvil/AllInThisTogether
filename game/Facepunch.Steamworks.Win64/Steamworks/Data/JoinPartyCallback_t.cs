using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x0200010F RID: 271
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct JoinPartyCallback_t : ICallbackData
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x000159EE File Offset: 0x00013BEE
		internal string ConnectStringUTF8()
		{
			return Encoding.UTF8.GetString(this.ConnectString, 0, Array.IndexOf<byte>(this.ConnectString, 0));
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x00015A0D File Offset: 0x00013C0D
		public int DataSize
		{
			get
			{
				return JoinPartyCallback_t._datasize;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00015A14 File Offset: 0x00013C14
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.JoinPartyCallback;
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00015A1B File Offset: 0x00013C1B
		// Note: this type is marked as 'beforefieldinit'.
		static JoinPartyCallback_t()
		{
		}

		// Token: 0x040008B7 RID: 2231
		internal Result Result;

		// Token: 0x040008B8 RID: 2232
		internal ulong BeaconID;

		// Token: 0x040008B9 RID: 2233
		internal ulong SteamIDBeaconOwner;

		// Token: 0x040008BA RID: 2234
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] ConnectString;

		// Token: 0x040008BB RID: 2235
		public static int _datasize = Marshal.SizeOf(typeof(JoinPartyCallback_t));
	}
}
