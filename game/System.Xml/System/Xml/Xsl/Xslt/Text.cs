using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200040D RID: 1037
	internal class Text : XslNode
	{
		// Token: 0x060028CB RID: 10443 RVA: 0x000F52ED File Offset: 0x000F34ED
		public Text(string data, SerializationHints hints, XslVersion xslVer) : base(XslNodeType.Text, null, data, xslVer)
		{
			this.Hints = hints;
		}

		// Token: 0x04002070 RID: 8304
		public readonly SerializationHints Hints;
	}
}
