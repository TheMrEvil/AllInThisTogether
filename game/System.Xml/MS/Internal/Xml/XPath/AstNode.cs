using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000615 RID: 1557
	internal abstract class AstNode
	{
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x06003FEB RID: 16363
		public abstract AstNode.AstType Type { get; }

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x06003FEC RID: 16364
		public abstract XPathResultType ReturnType { get; }

		// Token: 0x06003FED RID: 16365 RVA: 0x0000216B File Offset: 0x0000036B
		protected AstNode()
		{
		}

		// Token: 0x02000616 RID: 1558
		public enum AstType
		{
			// Token: 0x04002DCE RID: 11726
			Axis,
			// Token: 0x04002DCF RID: 11727
			Operator,
			// Token: 0x04002DD0 RID: 11728
			Filter,
			// Token: 0x04002DD1 RID: 11729
			ConstantOperand,
			// Token: 0x04002DD2 RID: 11730
			Function,
			// Token: 0x04002DD3 RID: 11731
			Group,
			// Token: 0x04002DD4 RID: 11732
			Root,
			// Token: 0x04002DD5 RID: 11733
			Variable,
			// Token: 0x04002DD6 RID: 11734
			Error
		}
	}
}
