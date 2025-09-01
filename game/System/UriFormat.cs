using System;

namespace System
{
	/// <summary>Controls how URI information is escaped.</summary>
	// Token: 0x02000158 RID: 344
	public enum UriFormat
	{
		/// <summary>Escaping is performed according to the rules in RFC 2396.</summary>
		// Token: 0x04000624 RID: 1572
		UriEscaped = 1,
		/// <summary>No escaping is performed.</summary>
		// Token: 0x04000625 RID: 1573
		Unescaped,
		/// <summary>Characters that have a reserved meaning in the requested URI components remain escaped. All others are not escaped.</summary>
		// Token: 0x04000626 RID: 1574
		SafeUnescaped
	}
}
