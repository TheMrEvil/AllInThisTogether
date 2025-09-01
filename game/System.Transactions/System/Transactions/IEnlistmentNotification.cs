using System;

namespace System.Transactions
{
	/// <summary>Describes an interface that a resource manager should implement to provide two phase commit notification callbacks for the transaction manager upon enlisting for participation.</summary>
	// Token: 0x02000014 RID: 20
	public interface IEnlistmentNotification
	{
		/// <summary>Notifies an enlisted object that a transaction is being committed.</summary>
		/// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment" /> object used to send a response to the transaction manager.</param>
		// Token: 0x0600002F RID: 47
		void Commit(Enlistment enlistment);

		/// <summary>Notifies an enlisted object that the status of a transaction is in doubt.</summary>
		/// <param name="enlistment">An <see cref="T:System.Transactions.Enlistment" /> object used to send a response to the transaction manager.</param>
		// Token: 0x06000030 RID: 48
		void InDoubt(Enlistment enlistment);

		/// <summary>Notifies an enlisted object that a transaction is being prepared for commitment.</summary>
		/// <param name="preparingEnlistment">A <see cref="T:System.Transactions.PreparingEnlistment" /> object used to send a response to the transaction manager.</param>
		// Token: 0x06000031 RID: 49
		void Prepare(PreparingEnlistment preparingEnlistment);

		/// <summary>Notifies an enlisted object that a transaction is being rolled back (aborted).</summary>
		/// <param name="enlistment">A <see cref="T:System.Transactions.Enlistment" /> object used to send a response to the transaction manager.</param>
		// Token: 0x06000032 RID: 50
		void Rollback(Enlistment enlistment);
	}
}
