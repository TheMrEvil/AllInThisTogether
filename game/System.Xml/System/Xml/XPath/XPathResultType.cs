using System;

namespace System.Xml.XPath
{
	/// <summary>Specifies the return type of the XPath expression.</summary>
	// Token: 0x02000255 RID: 597
	public enum XPathResultType
	{
		/// <summary>A numeric value.</summary>
		// Token: 0x040017F6 RID: 6134
		Number,
		/// <summary>A <see cref="T:System.String" /> value.</summary>
		// Token: 0x040017F7 RID: 6135
		String,
		/// <summary>A <see cref="T:System.Boolean" /><see langword="true" /> or <see langword="false" /> value.</summary>
		// Token: 0x040017F8 RID: 6136
		Boolean,
		/// <summary>A node collection.</summary>
		// Token: 0x040017F9 RID: 6137
		NodeSet,
		/// <summary>A tree fragment.</summary>
		// Token: 0x040017FA RID: 6138
		Navigator = 1,
		/// <summary>Any of the XPath node types.</summary>
		// Token: 0x040017FB RID: 6139
		Any = 5,
		/// <summary>The expression does not evaluate to the correct XPath type.</summary>
		// Token: 0x040017FC RID: 6140
		Error
	}
}
