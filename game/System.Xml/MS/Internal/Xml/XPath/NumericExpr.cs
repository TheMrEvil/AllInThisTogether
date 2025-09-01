using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200063F RID: 1599
	internal sealed class NumericExpr : ValueQuery
	{
		// Token: 0x06004125 RID: 16677 RVA: 0x00166778 File Offset: 0x00164978
		public NumericExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			if (opnd1.StaticType != XPathResultType.Number)
			{
				opnd1 = new NumberFunctions(Function.FunctionType.FuncNumber, opnd1);
			}
			if (opnd2.StaticType != XPathResultType.Number)
			{
				opnd2 = new NumberFunctions(Function.FunctionType.FuncNumber, opnd2);
			}
			this._op = op;
			this._opnd1 = opnd1;
			this._opnd2 = opnd2;
		}

		// Token: 0x06004126 RID: 16678 RVA: 0x001667C4 File Offset: 0x001649C4
		private NumericExpr(NumericExpr other) : base(other)
		{
			this._op = other._op;
			this._opnd1 = Query.Clone(other._opnd1);
			this._opnd2 = Query.Clone(other._opnd2);
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x001667FB File Offset: 0x001649FB
		public override void SetXsltContext(XsltContext context)
		{
			this._opnd1.SetXsltContext(context);
			this._opnd2.SetXsltContext(context);
		}

		// Token: 0x06004128 RID: 16680 RVA: 0x00166815 File Offset: 0x00164A15
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return NumericExpr.GetValue(this._op, XmlConvert.ToXPathDouble(this._opnd1.Evaluate(nodeIterator)), XmlConvert.ToXPathDouble(this._opnd2.Evaluate(nodeIterator)));
		}

		// Token: 0x06004129 RID: 16681 RVA: 0x00166849 File Offset: 0x00164A49
		private static double GetValue(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.PLUS:
				return n1 + n2;
			case Operator.Op.MINUS:
				return n1 - n2;
			case Operator.Op.MUL:
				return n1 * n2;
			case Operator.Op.DIV:
				return n1 / n2;
			case Operator.Op.MOD:
				return n1 % n2;
			default:
				return 0.0;
			}
		}

		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x0600412B RID: 16683 RVA: 0x00166887 File Offset: 0x00164A87
		public override XPathNodeIterator Clone()
		{
			return new NumericExpr(this);
		}

		// Token: 0x04002E53 RID: 11859
		private Operator.Op _op;

		// Token: 0x04002E54 RID: 11860
		private Query _opnd1;

		// Token: 0x04002E55 RID: 11861
		private Query _opnd2;
	}
}
