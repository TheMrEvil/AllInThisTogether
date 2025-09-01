using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000402 RID: 1026
	internal abstract class ProtoTemplate : XslNode
	{
		// Token: 0x060028BB RID: 10427 RVA: 0x000F4FE1 File Offset: 0x000F31E1
		public ProtoTemplate(XslNodeType nt, QilName name, XslVersion xslVer) : base(nt, name, null, xslVer)
		{
		}

		// Token: 0x060028BC RID: 10428
		public abstract string GetDebugName();

		// Token: 0x0400204D RID: 8269
		public QilFunction Function;
	}
}
