using System;

namespace System.Data
{
	/// <summary>Identifies a list of connection string parameters identified by the <see langword="KeyRestrictions" /> property that are either allowed or not allowed.</summary>
	// Token: 0x0200010B RID: 267
	public enum KeyRestrictionBehavior
	{
		/// <summary>Default. Identifies the only additional connection string parameters that are allowed.</summary>
		// Token: 0x04000975 RID: 2421
		AllowOnly,
		/// <summary>Identifies additional connection string parameters that are not allowed.</summary>
		// Token: 0x04000976 RID: 2422
		PreventUsage
	}
}
