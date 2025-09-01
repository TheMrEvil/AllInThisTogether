using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200020F RID: 527
	internal enum TransactionType
	{
		// Token: 0x04001094 RID: 4244
		LocalFromTSQL = 1,
		// Token: 0x04001095 RID: 4245
		LocalFromAPI,
		// Token: 0x04001096 RID: 4246
		Delegated,
		// Token: 0x04001097 RID: 4247
		Distributed,
		// Token: 0x04001098 RID: 4248
		Context
	}
}
