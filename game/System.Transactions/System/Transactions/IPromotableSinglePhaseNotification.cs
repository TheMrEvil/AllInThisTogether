using System;

namespace System.Transactions
{
	/// <summary>Describes an object that acts as a commit delegate for a non-distributed transaction internal to a resource manager.</summary>
	// Token: 0x02000015 RID: 21
	public interface IPromotableSinglePhaseNotification : ITransactionPromoter
	{
		/// <summary>Notifies a transaction participant that enlistment has completed successfully.</summary>
		/// <exception cref="T:System.Transactions.TransactionException">An attempt to enlist or serialize a transaction.</exception>
		// Token: 0x06000033 RID: 51
		void Initialize();

		/// <summary>Notifies an enlisted object that the transaction is being rolled back.</summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> object used to send a response to the transaction manager.</param>
		// Token: 0x06000034 RID: 52
		void Rollback(SinglePhaseEnlistment singlePhaseEnlistment);

		/// <summary>Notifies an enlisted object that the transaction is being committed.</summary>
		/// <param name="singlePhaseEnlistment">A <see cref="T:System.Transactions.SinglePhaseEnlistment" /> interface used to send a response to the transaction manager.</param>
		// Token: 0x06000035 RID: 53
		void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment);
	}
}
