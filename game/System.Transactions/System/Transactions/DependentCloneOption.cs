using System;

namespace System.Transactions
{
	/// <summary>Controls what kind of dependent transaction to create.</summary>
	// Token: 0x0200000E RID: 14
	public enum DependentCloneOption
	{
		/// <summary>The dependent transaction blocks the commit process of the transaction until the parent transaction times out, or <see cref="M:System.Transactions.DependentTransaction.Complete" /> is called. In this case, additional work can be done on the transaction and new enlistments can be created.</summary>
		// Token: 0x04000030 RID: 48
		BlockCommitUntilComplete,
		/// <summary>The dependent transaction automatically aborts the transaction if Commit is called on the parent transaction before <see cref="M:System.Transactions.DependentTransaction.Complete" /> is called.</summary>
		// Token: 0x04000031 RID: 49
		RollbackIfNotComplete
	}
}
