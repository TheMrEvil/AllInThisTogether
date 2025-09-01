using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x02000194 RID: 404
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GSClientDeny_t : ICallbackData
	{
		// Token: 0x06000D3C RID: 3388 RVA: 0x00016EB1 File Offset: 0x000150B1
		internal string OptionalTextUTF8()
		{
			return Encoding.UTF8.GetString(this.OptionalText, 0, Array.IndexOf<byte>(this.OptionalText, 0));
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x00016ED0 File Offset: 0x000150D0
		public int DataSize
		{
			get
			{
				return GSClientDeny_t._datasize;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x00016ED7 File Offset: 0x000150D7
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.GSClientDeny;
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00016EDE File Offset: 0x000150DE
		// Note: this type is marked as 'beforefieldinit'.
		static GSClientDeny_t()
		{
		}

		// Token: 0x04000A9F RID: 2719
		internal ulong SteamID;

		// Token: 0x04000AA0 RID: 2720
		internal DenyReason DenyReason;

		// Token: 0x04000AA1 RID: 2721
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		internal byte[] OptionalText;

		// Token: 0x04000AA2 RID: 2722
		public static int _datasize = Marshal.SizeOf(typeof(GSClientDeny_t));
	}
}
