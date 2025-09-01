using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200040C RID: 1036
	internal class NodeCtor : XslNode
	{
		// Token: 0x060028CA RID: 10442 RVA: 0x000F52D2 File Offset: 0x000F34D2
		public NodeCtor(XslNodeType nt, string nameAvt, string nsAvt, XslVersion xslVer) : base(nt, null, null, xslVer)
		{
			this.NameAvt = nameAvt;
			this.NsAvt = nsAvt;
		}

		// Token: 0x0400206E RID: 8302
		public readonly string NameAvt;

		// Token: 0x0400206F RID: 8303
		public readonly string NsAvt;
	}
}
