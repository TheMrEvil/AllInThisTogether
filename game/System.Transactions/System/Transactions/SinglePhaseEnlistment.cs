using System;

namespace System.Transactions
{
	/// <summary>Provides a set of callbacks that facilitate communication between a participant enlisted for Single Phase Commit and the transaction manager when the <see cref="M:System.Transactions.ISinglePhaseNotification.SinglePhaseCommit(System.Transactions.SinglePhaseEnlistment)" /> notification is received.</summary>
	// Token: 0x0200001B RID: 27
	public class SinglePhaseEnlistment : Enlistment
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000228B File Offset: 0x0000048B
		internal SinglePhaseEnlistment()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002293 File Offset: 0x00000493
		internal SinglePhaseEnlistment(Transaction tx, object abortingEnlisted)
		{
			this.tx = tx;
			this.abortingEnlisted = abortingEnlisted;
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back.</summary>
		// Token: 0x06000047 RID: 71 RVA: 0x000022A9 File Offset: 0x000004A9
		public void Aborted()
		{
			this.Aborted(null);
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back, and provides an explanation.</summary>
		/// <param name="e">An explanation of why a rollback is initiated.</param>
		// Token: 0x06000048 RID: 72 RVA: 0x000022B2 File Offset: 0x000004B2
		public void Aborted(Exception e)
		{
			if (this.tx != null)
			{
				this.tx.Rollback(e, this.abortingEnlisted);
			}
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the SinglePhaseCommit was successful.</summary>
		// Token: 0x06000049 RID: 73 RVA: 0x000021EE File Offset: 0x000003EE
		[MonoTODO]
		public void Committed()
		{
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt.</summary>
		// Token: 0x0600004A RID: 74 RVA: 0x0000216A File Offset: 0x0000036A
		[MonoTODO("Not implemented")]
		public void InDoubt()
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt, and provides an explanation.</summary>
		/// <param name="e">An explanation of why the transaction is in doubt.</param>
		// Token: 0x0600004B RID: 75 RVA: 0x0000216A File Offset: 0x0000036A
		[MonoTODO("Not implemented")]
		public void InDoubt(Exception e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000048 RID: 72
		private Transaction tx;

		// Token: 0x04000049 RID: 73
		private object abortingEnlisted;
	}
}
