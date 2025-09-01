using System;

namespace System.Transactions
{
	/// <summary>Describes a delegated transaction for an existing transaction that can be escalated to be managed by the MSDTC when needed.</summary>
	// Token: 0x02000018 RID: 24
	public interface ITransactionPromoter
	{
		/// <summary>Notifies an enlisted object that an escalation of the delegated transaction has been requested.</summary>
		/// <returns>A transmitter/receiver propagation token that marshals a distributed transaction. For more information, see <see cref="M:System.Transactions.TransactionInterop.GetTransactionFromTransmitterPropagationToken(System.Byte[])" />.</returns>
		// Token: 0x06000038 RID: 56
		byte[] Promote();
	}
}
