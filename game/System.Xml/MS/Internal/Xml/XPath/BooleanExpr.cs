using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200061B RID: 1563
	internal sealed class BooleanExpr : ValueQuery
	{
		// Token: 0x06004010 RID: 16400 RVA: 0x001639FC File Offset: 0x00161BFC
		public BooleanExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			if (opnd1.StaticType != XPathResultType.Boolean)
			{
				opnd1 = new BooleanFunctions(Function.FunctionType.FuncBoolean, opnd1);
			}
			if (opnd2.StaticType != XPathResultType.Boolean)
			{
				opnd2 = new BooleanFunctions(Function.FunctionType.FuncBoolean, opnd2);
			}
			this._opnd1 = opnd1;
			this._opnd2 = opnd2;
			this._isOr = (op == Operator.Op.OR);
		}

		// Token: 0x06004011 RID: 16401 RVA: 0x00163A4B File Offset: 0x00161C4B
		private BooleanExpr(BooleanExpr other) : base(other)
		{
			this._opnd1 = Query.Clone(other._opnd1);
			this._opnd2 = Query.Clone(other._opnd2);
			this._isOr = other._isOr;
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x00163A82 File Offset: 0x00161C82
		public override void SetXsltContext(XsltContext context)
		{
			this._opnd1.SetXsltContext(context);
			this._opnd2.SetXsltContext(context);
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x00163A9C File Offset: 0x00161C9C
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			object obj = this._opnd1.Evaluate(nodeIterator);
			if ((bool)obj == this._isOr)
			{
				return obj;
			}
			return this._opnd2.Evaluate(nodeIterator);
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x00163AD2 File Offset: 0x00161CD2
		public override XPathNodeIterator Clone()
		{
			return new BooleanExpr(this);
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x00066748 File Offset: 0x00064948
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x04002DF6 RID: 11766
		private Query _opnd1;

		// Token: 0x04002DF7 RID: 11767
		private Query _opnd2;

		// Token: 0x04002DF8 RID: 11768
		private bool _isOr;
	}
}
