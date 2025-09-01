using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000655 RID: 1621
	internal class Variable : AstNode
	{
		// Token: 0x060041BF RID: 16831 RVA: 0x001686E5 File Offset: 0x001668E5
		public Variable(string name, string prefix)
		{
			this._localname = name;
			this._prefix = prefix;
		}

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x0007076D File Offset: 0x0006E96D
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Variable;
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x0006AB76 File Offset: 0x00068D76
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x001686FB File Offset: 0x001668FB
		public string Localname
		{
			get
			{
				return this._localname;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060041C3 RID: 16835 RVA: 0x00168703 File Offset: 0x00166903
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x04002E9E RID: 11934
		private string _localname;

		// Token: 0x04002E9F RID: 11935
		private string _prefix;
	}
}
