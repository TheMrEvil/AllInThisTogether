using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200020E RID: 526
	internal enum TransactionState
	{
		// Token: 0x0400108E RID: 4238
		Pending,
		// Token: 0x0400108F RID: 4239
		Active,
		// Token: 0x04001090 RID: 4240
		Aborted,
		// Token: 0x04001091 RID: 4241
		Committed,
		// Token: 0x04001092 RID: 4242
		Unknown
	}
}
