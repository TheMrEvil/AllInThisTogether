using System;
using System.Runtime.InteropServices;

namespace Steamworks.Data
{
	// Token: 0x020001E9 RID: 489
	[StructLayout(LayoutKind.Sequential, Size = 696)]
	public struct ConnectionInfo
	{
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x000196F8 File Offset: 0x000178F8
		public ConnectionState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00019700 File Offset: 0x00017900
		public NetAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00019708 File Offset: 0x00017908
		public NetIdentity Identity
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00019715 File Offset: 0x00017915
		public NetConnectionEnd EndReason
		{
			get
			{
				return (NetConnectionEnd)this.endReason;
			}
		}

		// Token: 0x04000BD8 RID: 3032
		internal NetIdentity identity;

		// Token: 0x04000BD9 RID: 3033
		internal long userData;

		// Token: 0x04000BDA RID: 3034
		internal Socket listenSocket;

		// Token: 0x04000BDB RID: 3035
		internal NetAddress address;

		// Token: 0x04000BDC RID: 3036
		internal ushort pad;

		// Token: 0x04000BDD RID: 3037
		internal SteamNetworkingPOPID popRemote;

		// Token: 0x04000BDE RID: 3038
		internal SteamNetworkingPOPID popRelay;

		// Token: 0x04000BDF RID: 3039
		internal ConnectionState state;

		// Token: 0x04000BE0 RID: 3040
		internal int endReason;

		// Token: 0x04000BE1 RID: 3041
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string endDebug;

		// Token: 0x04000BE2 RID: 3042
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		internal string connectionDescription;
	}
}
