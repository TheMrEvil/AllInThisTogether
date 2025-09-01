using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200064E RID: 1614
	internal class Root : AstNode
	{
		// Token: 0x06004179 RID: 16761 RVA: 0x00167964 File Offset: 0x00165B64
		public Root()
		{
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600417A RID: 16762 RVA: 0x0006AAC4 File Offset: 0x00068CC4
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Root;
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}
	}
}
