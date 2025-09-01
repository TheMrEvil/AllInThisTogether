using System;

namespace System.Transactions
{
	/// <summary>Provides a mechanism for the hosting environment to supply its own default notion of <see cref="P:System.Transactions.Transaction.Current" />.</summary>
	/// <returns>A <see cref="T:System.Transactions.Transaction" /> object.</returns>
	// Token: 0x0200000B RID: 11
	// (Invoke) Token: 0x06000019 RID: 25
	public delegate Transaction HostCurrentTransactionCallback();
}
