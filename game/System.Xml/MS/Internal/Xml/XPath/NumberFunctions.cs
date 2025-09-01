using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200063E RID: 1598
	internal sealed class NumberFunctions : ValueQuery
	{
		// Token: 0x06004118 RID: 16664 RVA: 0x0016654B File Offset: 0x0016474B
		public NumberFunctions(Function.FunctionType ftype, Query arg)
		{
			this._arg = arg;
			this._ftype = ftype;
		}

		// Token: 0x06004119 RID: 16665 RVA: 0x00166561 File Offset: 0x00164761
		private NumberFunctions(NumberFunctions other) : base(other)
		{
			this._arg = Query.Clone(other._arg);
			this._ftype = other._ftype;
		}

		// Token: 0x0600411A RID: 16666 RVA: 0x00166587 File Offset: 0x00164787
		public override void SetXsltContext(XsltContext context)
		{
			if (this._arg != null)
			{
				this._arg.SetXsltContext(context);
			}
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x0016659D File Offset: 0x0016479D
		internal static double Number(bool arg)
		{
			if (!arg)
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x001665B5 File Offset: 0x001647B5
		internal static double Number(string arg)
		{
			return XmlConvert.ToXPathDouble(arg);
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x001665C0 File Offset: 0x001647C0
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Function.FunctionType ftype = this._ftype;
			if (ftype == Function.FunctionType.FuncNumber)
			{
				return this.Number(nodeIterator);
			}
			switch (ftype)
			{
			case Function.FunctionType.FuncSum:
				return this.Sum(nodeIterator);
			case Function.FunctionType.FuncFloor:
				return this.Floor(nodeIterator);
			case Function.FunctionType.FuncCeiling:
				return this.Ceiling(nodeIterator);
			case Function.FunctionType.FuncRound:
				return this.Round(nodeIterator);
			default:
				return null;
			}
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x00166638 File Offset: 0x00164838
		private double Number(XPathNodeIterator nodeIterator)
		{
			if (this._arg == null)
			{
				return XmlConvert.ToXPathDouble(nodeIterator.Current.Value);
			}
			object obj = this._arg.Evaluate(nodeIterator);
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.Number:
				return (double)obj;
			case XPathResultType.String:
				return NumberFunctions.Number((string)obj);
			case XPathResultType.Boolean:
				return NumberFunctions.Number((bool)obj);
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator = this._arg.Advance();
				if (xpathNavigator != null)
				{
					return NumberFunctions.Number(xpathNavigator.Value);
				}
				break;
			}
			case (XPathResultType)4:
				return NumberFunctions.Number(((XPathNavigator)obj).Value);
			}
			return double.NaN;
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x001666E4 File Offset: 0x001648E4
		private double Sum(XPathNodeIterator nodeIterator)
		{
			double num = 0.0;
			this._arg.Evaluate(nodeIterator);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this._arg.Advance()) != null)
			{
				num += NumberFunctions.Number(xpathNavigator.Value);
			}
			return num;
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x00166728 File Offset: 0x00164928
		private double Floor(XPathNodeIterator nodeIterator)
		{
			return Math.Floor((double)this._arg.Evaluate(nodeIterator));
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x00166740 File Offset: 0x00164940
		private double Ceiling(XPathNodeIterator nodeIterator)
		{
			return Math.Ceiling((double)this._arg.Evaluate(nodeIterator));
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x00166758 File Offset: 0x00164958
		private double Round(XPathNodeIterator nodeIterator)
		{
			return XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this._arg.Evaluate(nodeIterator)));
		}

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06004123 RID: 16675 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Number;
			}
		}

		// Token: 0x06004124 RID: 16676 RVA: 0x00166770 File Offset: 0x00164970
		public override XPathNodeIterator Clone()
		{
			return new NumberFunctions(this);
		}

		// Token: 0x04002E51 RID: 11857
		private Query _arg;

		// Token: 0x04002E52 RID: 11858
		private Function.FunctionType _ftype;
	}
}
