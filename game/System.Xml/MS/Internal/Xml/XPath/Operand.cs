using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000640 RID: 1600
	internal class Operand : AstNode
	{
		// Token: 0x0600412C RID: 16684 RVA: 0x0016688F File Offset: 0x00164A8F
		public Operand(string val)
		{
			this._type = XPathResultType.String;
			this._val = val;
		}

		// Token: 0x0600412D RID: 16685 RVA: 0x001668A5 File Offset: 0x00164AA5
		public Operand(double val)
		{
			this._type = XPathResultType.Number;
			this._val = val;
		}

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x000708A9 File Offset: 0x0006EAA9
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.ConstantOperand;
			}
		}

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x001668C0 File Offset: 0x00164AC0
		public override XPathResultType ReturnType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x001668C8 File Offset: 0x00164AC8
		public object OperandValue
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002E56 RID: 11862
		private XPathResultType _type;

		// Token: 0x04002E57 RID: 11863
		private object _val;
	}
}
