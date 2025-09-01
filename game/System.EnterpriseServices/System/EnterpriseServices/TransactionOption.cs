using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the automatic transaction type requested by the component.</summary>
	// Token: 0x02000053 RID: 83
	[Serializable]
	public enum TransactionOption
	{
		/// <summary>Ignores any transaction in the current context.</summary>
		// Token: 0x040000A2 RID: 162
		Disabled,
		/// <summary>Creates the component in a context with no governing transaction.</summary>
		// Token: 0x040000A3 RID: 163
		NotSupported,
		/// <summary>Shares a transaction, if one exists.</summary>
		// Token: 0x040000A4 RID: 164
		Supported,
		/// <summary>Shares a transaction, if one exists, and creates a new transaction if necessary.</summary>
		// Token: 0x040000A5 RID: 165
		Required,
		/// <summary>Creates the component with a new transaction, regardless of the state of the current context.</summary>
		// Token: 0x040000A6 RID: 166
		RequiresNew
	}
}
