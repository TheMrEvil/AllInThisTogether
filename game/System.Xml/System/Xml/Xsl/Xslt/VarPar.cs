using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000406 RID: 1030
	internal class VarPar : XslNode
	{
		// Token: 0x060028C3 RID: 10435 RVA: 0x000F5167 File Offset: 0x000F3367
		public VarPar(XslNodeType nt, QilName name, string select, XslVersion xslVer) : base(nt, name, select, xslVer)
		{
		}

		// Token: 0x04002058 RID: 8280
		public XslFlags DefValueFlags;

		// Token: 0x04002059 RID: 8281
		public QilNode Value;
	}
}
