using System;

namespace System.Transactions
{
	/// <summary>Represents the method that handles the <see cref="E:System.Transactions.Transaction.TransactionCompleted" /> event of a <see cref="T:System.Transactions.Transaction" /> class.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">The <see cref="T:System.Transactions.TransactionEventArgs" /> that contains the event data.</param>
	// Token: 0x0200000C RID: 12
	// (Invoke) Token: 0x0600001D RID: 29
	public delegate void TransactionCompletedEventHandler(object sender, TransactionEventArgs e);
}
