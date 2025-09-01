using System;

namespace System
{
	/// <summary>Specifies the culture, case, and sort rules to be used by certain overloads of the <see cref="M:System.String.Compare(System.String,System.String)" /> and <see cref="M:System.String.Equals(System.Object)" /> methods.</summary>
	// Token: 0x0200018C RID: 396
	public enum StringComparison
	{
		/// <summary>Compare strings using culture-sensitive sort rules and the current culture.</summary>
		// Token: 0x040012F7 RID: 4855
		CurrentCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the current culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x040012F8 RID: 4856
		CurrentCultureIgnoreCase,
		/// <summary>Compare strings using culture-sensitive sort rules and the invariant culture.</summary>
		// Token: 0x040012F9 RID: 4857
		InvariantCulture,
		/// <summary>Compare strings using culture-sensitive sort rules, the invariant culture, and ignoring the case of the strings being compared.</summary>
		// Token: 0x040012FA RID: 4858
		InvariantCultureIgnoreCase,
		/// <summary>Compare strings using ordinal (binary) sort rules.</summary>
		// Token: 0x040012FB RID: 4859
		Ordinal,
		/// <summary>Compare strings using ordinal (binary) sort rules and ignoring the case of the strings being compared.</summary>
		// Token: 0x040012FC RID: 4860
		OrdinalIgnoreCase
	}
}
