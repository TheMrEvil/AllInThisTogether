using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200062C RID: 1580
	internal class Filter : AstNode
	{
		// Token: 0x06004090 RID: 16528 RVA: 0x00164B67 File Offset: 0x00162D67
		public Filter(AstNode input, AstNode condition)
		{
			this._input = input;
			this._condition = condition;
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06004091 RID: 16529 RVA: 0x00066748 File Offset: 0x00064948
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Filter;
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06004092 RID: 16530 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06004093 RID: 16531 RVA: 0x00164B7D File Offset: 0x00162D7D
		public AstNode Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06004094 RID: 16532 RVA: 0x00164B85 File Offset: 0x00162D85
		public AstNode Condition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x04002E14 RID: 11796
		private AstNode _input;

		// Token: 0x04002E15 RID: 11797
		private AstNode _condition;
	}
}
