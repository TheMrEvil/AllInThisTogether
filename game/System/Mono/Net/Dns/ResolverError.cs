using System;

namespace Mono.Net.Dns
{
	// Token: 0x020000C7 RID: 199
	internal enum ResolverError
	{
		// Token: 0x04000374 RID: 884
		NoError,
		// Token: 0x04000375 RID: 885
		FormatError,
		// Token: 0x04000376 RID: 886
		ServerFailure,
		// Token: 0x04000377 RID: 887
		NameError,
		// Token: 0x04000378 RID: 888
		NotImplemented,
		// Token: 0x04000379 RID: 889
		Refused,
		// Token: 0x0400037A RID: 890
		ResponseHeaderError,
		// Token: 0x0400037B RID: 891
		ResponseFormatError,
		// Token: 0x0400037C RID: 892
		Timeout
	}
}
