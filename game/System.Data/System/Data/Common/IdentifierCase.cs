using System;

namespace System.Data.Common
{
	/// <summary>Specifies how identifiers are treated by the data source when searching the system catalog.</summary>
	// Token: 0x020003C5 RID: 965
	public enum IdentifierCase
	{
		/// <summary>The data source has ambiguous rules regarding identifier case and cannot discern this information.</summary>
		// Token: 0x04001BF1 RID: 7153
		Unknown,
		/// <summary>The data source ignores identifier case when searching the system catalog. The identifiers "ab" and "AB" will match.</summary>
		// Token: 0x04001BF2 RID: 7154
		Insensitive,
		/// <summary>The data source distinguishes identifier case when searching the system catalog. The identifiers "ab" and "AB" will not match.</summary>
		// Token: 0x04001BF3 RID: 7155
		Sensitive
	}
}
