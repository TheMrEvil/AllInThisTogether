using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Specifies the state of the current Compensating Resource Manager (CRM) transaction.</summary>
	// Token: 0x02000078 RID: 120
	[Serializable]
	public enum TransactionState
	{
		/// <summary>The transaction is active.</summary>
		// Token: 0x040000CD RID: 205
		Active,
		/// <summary>The transaction is commited.</summary>
		// Token: 0x040000CE RID: 206
		Committed,
		/// <summary>The transaction is aborted.</summary>
		// Token: 0x040000CF RID: 207
		Aborted,
		/// <summary>The transaction is in-doubt.</summary>
		// Token: 0x040000D0 RID: 208
		Indoubt
	}
}
