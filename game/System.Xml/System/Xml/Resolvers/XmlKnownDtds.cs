using System;

namespace System.Xml.Resolvers
{
	/// <summary>The <see cref="T:System.Xml.Resolvers.XmlKnownDtds" /> enumeration is used by the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> and defines which well-known DTDs that the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> recognizes.</summary>
	// Token: 0x0200060E RID: 1550
	[Flags]
	public enum XmlKnownDtds
	{
		/// <summary>Specifies that the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> will not recognize any of the predefined DTDs.</summary>
		// Token: 0x04002DBD RID: 11709
		None = 0,
		/// <summary>Specifies that the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> will recognize DTDs and entities that are defined in XHTML 1.0. </summary>
		// Token: 0x04002DBE RID: 11710
		Xhtml10 = 1,
		/// <summary>Specifies that the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> will recognize DTDs and entities that are defined in RSS 0.91.</summary>
		// Token: 0x04002DBF RID: 11711
		Rss091 = 2,
		/// <summary>Specifies that the <see cref="T:System.Xml.Resolvers.XmlPreloadedResolver" /> will recognize all currently supported DTDs. This is the default behavior.</summary>
		// Token: 0x04002DC0 RID: 11712
		All = 65535
	}
}
