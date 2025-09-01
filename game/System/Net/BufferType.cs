using System;

namespace System.Net
{
	// Token: 0x020005E8 RID: 1512
	internal enum BufferType
	{
		// Token: 0x04001B5F RID: 7007
		Empty,
		// Token: 0x04001B60 RID: 7008
		Data,
		// Token: 0x04001B61 RID: 7009
		Token,
		// Token: 0x04001B62 RID: 7010
		Parameters,
		// Token: 0x04001B63 RID: 7011
		Missing,
		// Token: 0x04001B64 RID: 7012
		Extra,
		// Token: 0x04001B65 RID: 7013
		Trailer,
		// Token: 0x04001B66 RID: 7014
		Header,
		// Token: 0x04001B67 RID: 7015
		Padding = 9,
		// Token: 0x04001B68 RID: 7016
		Stream,
		// Token: 0x04001B69 RID: 7017
		ChannelBindings = 14,
		// Token: 0x04001B6A RID: 7018
		TargetHost = 16,
		// Token: 0x04001B6B RID: 7019
		ReadOnlyFlag = -2147483648,
		// Token: 0x04001B6C RID: 7020
		ReadOnlyWithChecksum = 268435456
	}
}
