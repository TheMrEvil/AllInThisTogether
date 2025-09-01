using System;

namespace System.Transactions
{
	/// <summary>Represents a transaction that is not a root transaction, but can be escalated to be managed by the MSDTC.</summary>
	// Token: 0x02000016 RID: 22
	public interface ISimpleTransactionSuperior : ITransactionPromoter
	{
		/// <summary>Notifies an enlisted object that the transaction is being rolled back.</summary>
		// Token: 0x06000036 RID: 54
		void Rollback();
	}
}
