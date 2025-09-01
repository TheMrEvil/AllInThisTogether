using System;

namespace System.Transactions
{
	/// <summary>Represents a non-rooted transaction that can be delegated. This class cannot be inherited.</summary>
	// Token: 0x0200001C RID: 28
	[Serializable]
	public sealed class SubordinateTransaction : Transaction
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.SubordinateTransaction" /> class.</summary>
		/// <param name="isoLevel">The isolation level of the transaction</param>
		/// <param name="superior">A <see cref="T:System.Transactions.ISimpleTransactionSuperior" /></param>
		// Token: 0x0600004C RID: 76 RVA: 0x000022D4 File Offset: 0x000004D4
		public SubordinateTransaction(IsolationLevel isoLevel, ISimpleTransactionSuperior superior) : base(isoLevel)
		{
			throw new NotImplementedException();
		}
	}
}
