using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000638 RID: 1592
	internal sealed class LogicalExpr : ValueQuery
	{
		// Token: 0x060040D8 RID: 16600 RVA: 0x00165866 File Offset: 0x00163A66
		public LogicalExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			this._op = op;
			this._opnd1 = opnd1;
			this._opnd2 = opnd2;
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x00165883 File Offset: 0x00163A83
		private LogicalExpr(LogicalExpr other) : base(other)
		{
			this._op = other._op;
			this._opnd1 = Query.Clone(other._opnd1);
			this._opnd2 = Query.Clone(other._opnd2);
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x001658BA File Offset: 0x00163ABA
		public override void SetXsltContext(XsltContext context)
		{
			this._opnd1.SetXsltContext(context);
			this._opnd2.SetXsltContext(context);
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x001658D4 File Offset: 0x00163AD4
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Operator.Op op = this._op;
			object obj = this._opnd1.Evaluate(nodeIterator);
			object obj2 = this._opnd2.Evaluate(nodeIterator);
			int num = (int)base.GetXPathType(obj);
			int num2 = (int)base.GetXPathType(obj2);
			if (num < num2)
			{
				op = Operator.InvertOperator(op);
				object obj3 = obj;
				obj = obj2;
				obj2 = obj3;
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			if (op == Operator.Op.EQ || op == Operator.Op.NE)
			{
				return LogicalExpr.s_CompXsltE[num][num2](op, obj, obj2);
			}
			return LogicalExpr.s_CompXsltO[num][num2](op, obj, obj2);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x00165960 File Offset: 0x00163B60
		private static bool cmpQueryQueryE(Operator.Op op, object val1, object val2)
		{
			bool flag = op == Operator.Op.EQ;
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			LogicalExpr.NodeSet nodeSet2 = new LogicalExpr.NodeSet(val2);
			IL_15:
			while (nodeSet.MoveNext())
			{
				if (!nodeSet2.MoveNext())
				{
					return false;
				}
				string value = nodeSet.Value;
				while (value == nodeSet2.Value != flag)
				{
					if (!nodeSet2.MoveNext())
					{
						nodeSet2.Reset();
						goto IL_15;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x001659C4 File Offset: 0x00163BC4
		private static bool cmpQueryQueryO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			LogicalExpr.NodeSet nodeSet2 = new LogicalExpr.NodeSet(val2);
			IL_10:
			while (nodeSet.MoveNext())
			{
				if (!nodeSet2.MoveNext())
				{
					return false;
				}
				double n = NumberFunctions.Number(nodeSet.Value);
				while (!LogicalExpr.cmpNumberNumber(op, n, NumberFunctions.Number(nodeSet2.Value)))
				{
					if (!nodeSet2.MoveNext())
					{
						nodeSet2.Reset();
						goto IL_10;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x00165A2C File Offset: 0x00163C2C
		private static bool cmpQueryNumber(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double n = (double)val2;
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumber(op, NumberFunctions.Number(nodeSet.Value), n))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00165A6C File Offset: 0x00163C6C
		private static bool cmpQueryStringE(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			string n = (string)val2;
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpStringStringE(op, nodeSet.Value, n))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x00165AA8 File Offset: 0x00163CA8
		private static bool cmpQueryStringO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double n = NumberFunctions.Number((string)val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number(nodeSet.Value), n))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x00165AEC File Offset: 0x00163CEC
		private static bool cmpRtfQueryE(Operator.Op op, object val1, object val2)
		{
			string n = LogicalExpr.Rtf(val1);
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpStringStringE(op, n, nodeSet.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x00165B28 File Offset: 0x00163D28
		private static bool cmpRtfQueryO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumberO(op, n, NumberFunctions.Number(nodeSet.Value)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x00165B6C File Offset: 0x00163D6C
		private static bool cmpQueryBoolE(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			bool n = nodeSet.MoveNext();
			bool n2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x00165B98 File Offset: 0x00163D98
		private static bool cmpQueryBoolO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double n = nodeSet.MoveNext() ? 1.0 : 0.0;
			double n2 = NumberFunctions.Number((bool)val2);
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x00165BDF File Offset: 0x00163DDF
		private static bool cmpBoolBoolE(Operator.Op op, bool n1, bool n2)
		{
			return op == Operator.Op.EQ == (n1 == n2);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x00165BEC File Offset: 0x00163DEC
		private static bool cmpBoolBoolE(Operator.Op op, object val1, object val2)
		{
			bool n = (bool)val1;
			bool n2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x00165C10 File Offset: 0x00163E10
		private static bool cmpBoolBoolO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number((bool)val1);
			double n2 = NumberFunctions.Number((bool)val2);
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x00165C40 File Offset: 0x00163E40
		private static bool cmpBoolNumberE(Operator.Op op, object val1, object val2)
		{
			bool n = (bool)val1;
			bool n2 = BooleanFunctions.toBoolean((double)val2);
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x00165C68 File Offset: 0x00163E68
		private static bool cmpBoolNumberO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number((bool)val1);
			double n2 = (double)val2;
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x00165C90 File Offset: 0x00163E90
		private static bool cmpBoolStringE(Operator.Op op, object val1, object val2)
		{
			bool n = (bool)val1;
			bool n2 = BooleanFunctions.toBoolean((string)val2);
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x00165CB8 File Offset: 0x00163EB8
		private static bool cmpRtfBoolE(Operator.Op op, object val1, object val2)
		{
			bool n = BooleanFunctions.toBoolean(LogicalExpr.Rtf(val1));
			bool n2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, n, n2);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x00165CE0 File Offset: 0x00163EE0
		private static bool cmpBoolStringO(Operator.Op op, object val1, object val2)
		{
			return LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number((bool)val1), NumberFunctions.Number((string)val2));
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x00165CFE File Offset: 0x00163EFE
		private static bool cmpRtfBoolO(Operator.Op op, object val1, object val2)
		{
			return LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number(LogicalExpr.Rtf(val1)), NumberFunctions.Number((bool)val2));
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x00165D1C File Offset: 0x00163F1C
		private static bool cmpNumberNumber(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.EQ:
				return n1 == n2;
			case Operator.Op.NE:
				return n1 != n2;
			case Operator.Op.LT:
				return n1 < n2;
			case Operator.Op.LE:
				return n1 <= n2;
			case Operator.Op.GT:
				return n1 > n2;
			case Operator.Op.GE:
				return n1 >= n2;
			default:
				return false;
			}
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x00165D73 File Offset: 0x00163F73
		private static bool cmpNumberNumberO(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.LT:
				return n1 < n2;
			case Operator.Op.LE:
				return n1 <= n2;
			case Operator.Op.GT:
				return n1 > n2;
			case Operator.Op.GE:
				return n1 >= n2;
			default:
				return false;
			}
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x00165DAC File Offset: 0x00163FAC
		private static bool cmpNumberNumber(Operator.Op op, object val1, object val2)
		{
			double n = (double)val1;
			double n2 = (double)val2;
			return LogicalExpr.cmpNumberNumber(op, n, n2);
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x00165DD0 File Offset: 0x00163FD0
		private static bool cmpStringNumber(Operator.Op op, object val1, object val2)
		{
			double n = (double)val2;
			double n2 = NumberFunctions.Number((string)val1);
			return LogicalExpr.cmpNumberNumber(op, n2, n);
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x00165DF8 File Offset: 0x00163FF8
		private static bool cmpRtfNumber(Operator.Op op, object val1, object val2)
		{
			double n = (double)val2;
			double n2 = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			return LogicalExpr.cmpNumberNumber(op, n2, n);
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x00165E20 File Offset: 0x00164020
		private static bool cmpStringStringE(Operator.Op op, string n1, string n2)
		{
			return op == Operator.Op.EQ == (n1 == n2);
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x00165E30 File Offset: 0x00164030
		private static bool cmpStringStringE(Operator.Op op, object val1, object val2)
		{
			string n = (string)val1;
			string n2 = (string)val2;
			return LogicalExpr.cmpStringStringE(op, n, n2);
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x00165E54 File Offset: 0x00164054
		private static bool cmpRtfStringE(Operator.Op op, object val1, object val2)
		{
			string n = LogicalExpr.Rtf(val1);
			string n2 = (string)val2;
			return LogicalExpr.cmpStringStringE(op, n, n2);
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x00165E78 File Offset: 0x00164078
		private static bool cmpRtfRtfE(Operator.Op op, object val1, object val2)
		{
			string n = LogicalExpr.Rtf(val1);
			string n2 = LogicalExpr.Rtf(val2);
			return LogicalExpr.cmpStringStringE(op, n, n2);
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x00165E9C File Offset: 0x0016409C
		private static bool cmpStringStringO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number((string)val1);
			double n2 = NumberFunctions.Number((string)val2);
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x00165ECC File Offset: 0x001640CC
		private static bool cmpRtfStringO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			double n2 = NumberFunctions.Number((string)val2);
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x00165EFC File Offset: 0x001640FC
		private static bool cmpRtfRtfO(Operator.Op op, object val1, object val2)
		{
			double n = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			double n2 = NumberFunctions.Number(LogicalExpr.Rtf(val2));
			return LogicalExpr.cmpNumberNumberO(op, n, n2);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x00165F29 File Offset: 0x00164129
		public override XPathNodeIterator Clone()
		{
			return new LogicalExpr(this);
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x00165F31 File Offset: 0x00164131
		private static string Rtf(object o)
		{
			return ((XPathNavigator)o).Value;
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x00066748 File Offset: 0x00064948
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x00165F40 File Offset: 0x00164140
		// Note: this type is marked as 'beforefieldinit'.
		static LogicalExpr()
		{
			LogicalExpr.cmpXslt[][] array = new LogicalExpr.cmpXslt[5][];
			int num = 0;
			LogicalExpr.cmpXslt[] array2 = new LogicalExpr.cmpXslt[5];
			array2[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpNumberNumber);
			array[num] = array2;
			int num2 = 1;
			LogicalExpr.cmpXslt[] array3 = new LogicalExpr.cmpXslt[5];
			array3[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringNumber);
			array3[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringStringE);
			array[num2] = array3;
			int num3 = 2;
			LogicalExpr.cmpXslt[] array4 = new LogicalExpr.cmpXslt[5];
			array4[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolNumberE);
			array4[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolStringE);
			array4[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolBoolE);
			array[num3] = array4;
			int num4 = 3;
			LogicalExpr.cmpXslt[] array5 = new LogicalExpr.cmpXslt[5];
			array5[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryNumber);
			array5[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryStringE);
			array5[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryBoolE);
			array5[3] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryQueryE);
			array[num4] = array5;
			array[4] = new LogicalExpr.cmpXslt[]
			{
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfNumber),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfStringE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfBoolE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfQueryE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfRtfE)
			};
			LogicalExpr.s_CompXsltE = array;
			LogicalExpr.cmpXslt[][] array6 = new LogicalExpr.cmpXslt[5][];
			int num5 = 0;
			LogicalExpr.cmpXslt[] array7 = new LogicalExpr.cmpXslt[5];
			array7[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpNumberNumber);
			array6[num5] = array7;
			int num6 = 1;
			LogicalExpr.cmpXslt[] array8 = new LogicalExpr.cmpXslt[5];
			array8[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringNumber);
			array8[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringStringO);
			array6[num6] = array8;
			int num7 = 2;
			LogicalExpr.cmpXslt[] array9 = new LogicalExpr.cmpXslt[5];
			array9[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolNumberO);
			array9[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolStringO);
			array9[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolBoolO);
			array6[num7] = array9;
			int num8 = 3;
			LogicalExpr.cmpXslt[] array10 = new LogicalExpr.cmpXslt[5];
			array10[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryNumber);
			array10[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryStringO);
			array10[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryBoolO);
			array10[3] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryQueryO);
			array6[num8] = array10;
			array6[4] = new LogicalExpr.cmpXslt[]
			{
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfNumber),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfStringO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfBoolO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfQueryO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfRtfO)
			};
			LogicalExpr.s_CompXsltO = array6;
		}

		// Token: 0x04002E45 RID: 11845
		private Operator.Op _op;

		// Token: 0x04002E46 RID: 11846
		private Query _opnd1;

		// Token: 0x04002E47 RID: 11847
		private Query _opnd2;

		// Token: 0x04002E48 RID: 11848
		private static readonly LogicalExpr.cmpXslt[][] s_CompXsltE;

		// Token: 0x04002E49 RID: 11849
		private static readonly LogicalExpr.cmpXslt[][] s_CompXsltO;

		// Token: 0x02000639 RID: 1593
		// (Invoke) Token: 0x060040FF RID: 16639
		private delegate bool cmpXslt(Operator.Op op, object val1, object val2);

		// Token: 0x0200063A RID: 1594
		private struct NodeSet
		{
			// Token: 0x06004102 RID: 16642 RVA: 0x0016617F File Offset: 0x0016437F
			public NodeSet(object opnd)
			{
				this._opnd = (Query)opnd;
				this._current = null;
			}

			// Token: 0x06004103 RID: 16643 RVA: 0x00166194 File Offset: 0x00164394
			public bool MoveNext()
			{
				this._current = this._opnd.Advance();
				return this._current != null;
			}

			// Token: 0x06004104 RID: 16644 RVA: 0x001661B0 File Offset: 0x001643B0
			public void Reset()
			{
				this._opnd.Reset();
			}

			// Token: 0x17000C5E RID: 3166
			// (get) Token: 0x06004105 RID: 16645 RVA: 0x001661BD File Offset: 0x001643BD
			public string Value
			{
				get
				{
					return this._current.Value;
				}
			}

			// Token: 0x04002E4A RID: 11850
			private Query _opnd;

			// Token: 0x04002E4B RID: 11851
			private XPathNavigator _current;
		}
	}
}
