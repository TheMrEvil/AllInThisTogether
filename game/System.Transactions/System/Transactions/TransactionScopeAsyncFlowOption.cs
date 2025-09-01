using System;

namespace System.Transactions
{
	/// <summary>Specifies whether transaction flow across thread continuations is enabled for <see cref="T:System.Transactions.TransactionScope" />.</summary>
	// Token: 0x0200002A RID: 42
	public enum TransactionScopeAsyncFlowOption
	{
		/// <summary>Specifies that transaction flow across thread continuations is suppressed.</summary>
		// Token: 0x04000074 RID: 116
		Suppress,
		/// <summary>Specifies that transaction flow across thread continuations is enabled.</summary>
		// Token: 0x04000075 RID: 117
		Enabled
	}
}
