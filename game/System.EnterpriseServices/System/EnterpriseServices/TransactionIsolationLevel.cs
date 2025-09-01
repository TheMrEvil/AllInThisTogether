using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the value of the <see cref="T:System.EnterpriseServices.TransactionAttribute" />.</summary>
	// Token: 0x02000052 RID: 82
	[Serializable]
	public enum TransactionIsolationLevel
	{
		/// <summary>The isolation level for the component is obtained from the calling component's isolation level. If this is the root component, the isolation level used is <see cref="F:System.EnterpriseServices.TransactionIsolationLevel.Serializable" />.</summary>
		// Token: 0x0400009C RID: 156
		Any,
		/// <summary>Shared locks are held while the data is being read to avoid reading modified data, but the data can be changed before the end of the transaction, resulting in non-repeatable reads or phantom data.</summary>
		// Token: 0x0400009D RID: 157
		ReadCommitted = 2,
		/// <summary>Shared locks are issued and no exclusive locks are honored.</summary>
		// Token: 0x0400009E RID: 158
		ReadUncommitted = 1,
		/// <summary>Locks are placed on all data that is used in a query, preventing other users from updating the data. Prevents non-repeatable reads, but phantom rows are still possible.</summary>
		// Token: 0x0400009F RID: 159
		RepeatableRead = 3,
		/// <summary>Prevents updating or inserting until the transaction is complete.</summary>
		// Token: 0x040000A0 RID: 160
		Serializable
	}
}
