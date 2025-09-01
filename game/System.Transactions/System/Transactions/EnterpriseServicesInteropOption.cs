using System;

namespace System.Transactions
{
	/// <summary>Specifies how distributed transactions interact with COM+ transactions.</summary>
	// Token: 0x02000012 RID: 18
	public enum EnterpriseServicesInteropOption
	{
		/// <summary>There is no synchronization between <see cref="P:System.EnterpriseServices.ContextUtil.Transaction" /> and <see cref="P:System.Transactions.Transaction.Current" />.</summary>
		// Token: 0x04000038 RID: 56
		None,
		/// <summary>Search for an existing COM+ context and synchronize with it if one exists.</summary>
		// Token: 0x04000039 RID: 57
		Automatic,
		/// <summary>The <see cref="N:System.EnterpriseServices" /> context (which can be retrieved by calling the static method <see cref="P:System.EnterpriseServices.ContextUtil.Transaction" /> of the <see cref="T:System.EnterpriseServices.ContextUtil" /> class) and the <see cref="N:System.Transactions" /> ambient transaction (which can be retrieved by calling the static method <see cref="P:System.Transactions.Transaction.Current" /> of the <see cref="T:System.Transactions.Transaction" /> class) are always synchronized. This introduces a performance penalty because new <see cref="N:System.EnterpriseServices" /> contexts may need to be created.</summary>
		// Token: 0x0400003A RID: 58
		Full
	}
}
