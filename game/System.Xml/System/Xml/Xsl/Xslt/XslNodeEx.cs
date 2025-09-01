using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200040E RID: 1038
	internal class XslNodeEx : XslNode
	{
		// Token: 0x060028CC RID: 10444 RVA: 0x000F5301 File Offset: 0x000F3501
		public XslNodeEx(XslNodeType t, QilName name, object arg, XsltInput.ContextInfo ctxInfo, XslVersion xslVer) : base(t, name, arg, xslVer)
		{
			this.ElemNameLi = ctxInfo.elemNameLi;
			this.EndTagLi = ctxInfo.endTagLi;
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x000F5167 File Offset: 0x000F3367
		public XslNodeEx(XslNodeType t, QilName name, object arg, XslVersion xslVer) : base(t, name, arg, xslVer)
		{
		}

		// Token: 0x04002071 RID: 8305
		public readonly ISourceLineInfo ElemNameLi;

		// Token: 0x04002072 RID: 8306
		public readonly ISourceLineInfo EndTagLi;
	}
}
