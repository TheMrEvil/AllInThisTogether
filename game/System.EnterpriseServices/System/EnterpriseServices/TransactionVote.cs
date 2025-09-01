using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the values allowed for transaction outcome voting.</summary>
	// Token: 0x02000055 RID: 85
	[ComVisible(false)]
	[Serializable]
	public enum TransactionVote
	{
		/// <summary>Aborts the current transaction.</summary>
		// Token: 0x040000AE RID: 174
		Abort = 1,
		/// <summary>Commits the current transaction.</summary>
		// Token: 0x040000AF RID: 175
		Commit = 0
	}
}
