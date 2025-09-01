using System;

namespace System.Net
{
	// Token: 0x020005C7 RID: 1479
	internal class SystemNetworkCredential : NetworkCredential
	{
		// Token: 0x06002FF1 RID: 12273 RVA: 0x000A5779 File Offset: 0x000A3979
		private SystemNetworkCredential() : base(string.Empty, string.Empty, string.Empty)
		{
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000A5790 File Offset: 0x000A3990
		// Note: this type is marked as 'beforefieldinit'.
		static SystemNetworkCredential()
		{
		}

		// Token: 0x04001A69 RID: 6761
		internal static readonly SystemNetworkCredential defaultCredential = new SystemNetworkCredential();
	}
}
