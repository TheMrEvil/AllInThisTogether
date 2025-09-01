using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020000D8 RID: 216
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ClientGameServerDeny_t : ICallbackData
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x000151A0 File Offset: 0x000133A0
		public int DataSize
		{
			get
			{
				return ClientGameServerDeny_t._datasize;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x000151A7 File Offset: 0x000133A7
		public CallbackType CallbackType
		{
			get
			{
				return CallbackType.ClientGameServerDeny;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000151AB File Offset: 0x000133AB
		// Note: this type is marked as 'beforefieldinit'.
		static ClientGameServerDeny_t()
		{
		}

		// Token: 0x040007E8 RID: 2024
		internal uint AppID;

		// Token: 0x040007E9 RID: 2025
		internal uint GameServerIP;

		// Token: 0x040007EA RID: 2026
		internal ushort GameServerPort;

		// Token: 0x040007EB RID: 2027
		internal ushort Secure;

		// Token: 0x040007EC RID: 2028
		internal uint Reason;

		// Token: 0x040007ED RID: 2029
		public static int _datasize = Marshal.SizeOf(typeof(ClientGameServerDeny_t));
	}
}
