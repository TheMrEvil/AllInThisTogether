using System;

namespace System
{
	/// <summary>Specifies whether applicable <see cref="Overload:System.String.Split" /> method overloads include or omit empty substrings from the return value.</summary>
	// Token: 0x0200018D RID: 397
	[Flags]
	public enum StringSplitOptions
	{
		/// <summary>The return value includes array elements that contain an empty string</summary>
		// Token: 0x040012FE RID: 4862
		None = 0,
		/// <summary>The return value does not include array elements that contain an empty string</summary>
		// Token: 0x040012FF RID: 4863
		RemoveEmptyEntries = 1
	}
}
