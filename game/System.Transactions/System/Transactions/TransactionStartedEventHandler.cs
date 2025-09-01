using System;

namespace System.Transactions
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Transactions.TransactionManager.DistributedTransactionStarted" /> event of a <see cref="T:System.Transactions.TransactionManager" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Transactions.TransactionEventArgs" /> that contains the transaction from which transaction information can be retrieved.</param>
	// Token: 0x0200000D RID: 13
	// (Invoke) Token: 0x06000021 RID: 33
	public delegate void TransactionStartedEventHandler(object sender, TransactionEventArgs e);
}
