using System;

namespace System.Net
{
	// Token: 0x0200054E RID: 1358
	internal static class GlobalSSPI
	{
		// Token: 0x06002C21 RID: 11297 RVA: 0x00096314 File Offset: 0x00094514
		// Note: this type is marked as 'beforefieldinit'.
		static GlobalSSPI()
		{
		}

		// Token: 0x040017C1 RID: 6081
		internal static readonly SSPIInterface SSPIAuth = new SSPIAuthType();

		// Token: 0x040017C2 RID: 6082
		internal static readonly SSPIInterface SSPISecureChannel = new SSPISecureChannelType();
	}
}
