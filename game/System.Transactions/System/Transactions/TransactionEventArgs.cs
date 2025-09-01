using System;

namespace System.Transactions
{
	/// <summary>Provides data for the following transaction events: <see cref="E:System.Transactions.TransactionManager.DistributedTransactionStarted" />, <see cref="E:System.Transactions.Transaction.TransactionCompleted" />.</summary>
	// Token: 0x02000020 RID: 32
	public class TransactionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.TransactionEventArgs" /> class.</summary>
		// Token: 0x0600008E RID: 142 RVA: 0x00002BAF File Offset: 0x00000DAF
		public TransactionEventArgs()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002BB7 File Offset: 0x00000DB7
		internal TransactionEventArgs(Transaction transaction) : this()
		{
			this.transaction = transaction;
		}

		/// <summary>Gets the transaction for which event status is provided.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" /> for which event status is provided.</returns>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00002BC6 File Offset: 0x00000DC6
		public Transaction Transaction
		{
			get
			{
				return this.transaction;
			}
		}

		// Token: 0x0400005A RID: 90
		private Transaction transaction;
	}
}
