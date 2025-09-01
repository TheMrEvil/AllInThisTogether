using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000400 RID: 1024
	internal class NsDecl
	{
		// Token: 0x060028B0 RID: 10416 RVA: 0x000F4EF4 File Offset: 0x000F30F4
		public NsDecl(NsDecl prev, string prefix, string nsUri)
		{
			this.Prev = prev;
			this.Prefix = prefix;
			this.NsUri = nsUri;
		}

		// Token: 0x04002041 RID: 8257
		public readonly NsDecl Prev;

		// Token: 0x04002042 RID: 8258
		public readonly string Prefix;

		// Token: 0x04002043 RID: 8259
		public readonly string NsUri;
	}
}
