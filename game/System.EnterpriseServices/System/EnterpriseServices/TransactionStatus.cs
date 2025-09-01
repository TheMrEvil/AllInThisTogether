using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Indicates the transaction status.</summary>
	// Token: 0x02000054 RID: 84
	[ComVisible(false)]
	[Serializable]
	public enum TransactionStatus
	{
		/// <summary>The transaction has committed.</summary>
		// Token: 0x040000A8 RID: 168
		Commited,
		/// <summary>The transaction has neither committed nor aborted.</summary>
		// Token: 0x040000A9 RID: 169
		LocallyOk,
		/// <summary>No transactions are being used through <see cref="M:System.EnterpriseServices.ServiceDomain.Enter(System.EnterpriseServices.ServiceConfig)" />.</summary>
		// Token: 0x040000AA RID: 170
		NoTransaction,
		/// <summary>The transaction is in the process of aborting.</summary>
		// Token: 0x040000AB RID: 171
		Aborting,
		/// <summary>The transaction is aborted.</summary>
		// Token: 0x040000AC RID: 172
		Aborted
	}
}
