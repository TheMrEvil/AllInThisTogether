using System;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x0200042D RID: 1069
	internal class XPathQilFactory : QilPatternFactory
	{
		// Token: 0x06002A84 RID: 10884 RVA: 0x00100FC8 File Offset: 0x000FF1C8
		public XPathQilFactory(QilFactory f, bool debug) : base(f, debug)
		{
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x00100FD2 File Offset: 0x000FF1D2
		public QilNode Error(string res, QilNode args)
		{
			return base.Error(this.InvokeFormatMessage(base.String(res), args));
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00100FE8 File Offset: 0x000FF1E8
		public QilNode Error(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			return base.Error(base.String(XslLoadException.CreateMessage(lineInfo, res, args)));
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x00101000 File Offset: 0x000FF200
		public QilIterator FirstNode(QilNode n)
		{
			QilIterator qilIterator = base.For(base.DocOrderDistinct(n));
			return base.For(base.Filter(qilIterator, base.Eq(base.PositionOf(qilIterator), base.Int32(1))));
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x0010103C File Offset: 0x000FF23C
		public bool IsAnyType(QilNode n)
		{
			XmlQueryType xmlType = n.XmlType;
			return !xmlType.IsStrict && !xmlType.IsNode;
		}

		// Token: 0x06002A89 RID: 10889 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckAny(QilNode n)
		{
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckNode(QilNode n)
		{
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckNodeSet(QilNode n)
		{
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckNodeNotRtf(QilNode n)
		{
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckString(QilNode n)
		{
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckStringS(QilNode n)
		{
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckDouble(QilNode n)
		{
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void CheckBool(QilNode n)
		{
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x00101064 File Offset: 0x000FF264
		public bool CannotBeNodeSet(QilNode n)
		{
			XmlQueryType xmlType = n.XmlType;
			return xmlType.IsAtomicValue && !xmlType.IsEmpty && !(n is QilIterator);
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00101098 File Offset: 0x000FF298
		public QilNode SafeDocOrderDistinct(QilNode n)
		{
			XmlQueryType xmlType = n.XmlType;
			if (xmlType.MaybeMany)
			{
				if (xmlType.IsNode && xmlType.IsNotRtf)
				{
					return base.DocOrderDistinct(n);
				}
				if (!xmlType.IsAtomicValue)
				{
					QilIterator qilIterator;
					return base.Loop(qilIterator = base.Let(n), base.Conditional(base.Gt(base.Length(qilIterator), base.Int32(1)), base.DocOrderDistinct(base.TypeAssert(qilIterator, XmlQueryTypeFactory.NodeNotRtfS)), qilIterator));
				}
			}
			return n;
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x00101112 File Offset: 0x000FF312
		public QilNode InvokeFormatMessage(QilNode res, QilNode args)
		{
			return base.XsltInvokeEarlyBound(base.QName("format-message"), XsltMethods.FormatMessage, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				res,
				args
			});
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x00101140 File Offset: 0x000FF340
		public QilNode InvokeEqualityOperator(QilNodeType op, QilNode left, QilNode right)
		{
			left = base.TypeAssert(left, XmlQueryTypeFactory.ItemS);
			right = base.TypeAssert(right, XmlQueryTypeFactory.ItemS);
			double val;
			if (op == QilNodeType.Eq)
			{
				val = 0.0;
			}
			else
			{
				val = 1.0;
			}
			return base.XsltInvokeEarlyBound(base.QName("EqualityOperator"), XsltMethods.EqualityOperator, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				base.Double(val),
				left,
				right
			});
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x001011B8 File Offset: 0x000FF3B8
		public QilNode InvokeRelationalOperator(QilNodeType op, QilNode left, QilNode right)
		{
			left = base.TypeAssert(left, XmlQueryTypeFactory.ItemS);
			right = base.TypeAssert(right, XmlQueryTypeFactory.ItemS);
			double val;
			switch (op)
			{
			case QilNodeType.Gt:
				val = 4.0;
				goto IL_65;
			case QilNodeType.Lt:
				val = 2.0;
				goto IL_65;
			case QilNodeType.Le:
				val = 3.0;
				goto IL_65;
			}
			val = 5.0;
			IL_65:
			return base.XsltInvokeEarlyBound(base.QName("RelationalOperator"), XsltMethods.RelationalOperator, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				base.Double(val),
				left,
				right
			});
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void ExpectAny(QilNode n)
		{
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x00101260 File Offset: 0x000FF460
		public QilNode ConvertToType(XmlTypeCode requiredType, QilNode n)
		{
			if (requiredType == XmlTypeCode.Item)
			{
				return n;
			}
			if (requiredType != XmlTypeCode.Node)
			{
				switch (requiredType)
				{
				case XmlTypeCode.String:
					return this.ConvertToString(n);
				case XmlTypeCode.Boolean:
					return this.ConvertToBoolean(n);
				case XmlTypeCode.Double:
					return this.ConvertToNumber(n);
				}
				return null;
			}
			return this.EnsureNodeSet(n);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x001012B8 File Offset: 0x000FF4B8
		public QilNode ConvertToString(QilNode n)
		{
			switch (n.XmlType.TypeCode)
			{
			case XmlTypeCode.String:
				return n;
			case XmlTypeCode.Boolean:
				if (n.NodeType == QilNodeType.True)
				{
					return base.String("true");
				}
				if (n.NodeType != QilNodeType.False)
				{
					return base.Conditional(n, base.String("true"), base.String("false"));
				}
				return base.String("false");
			case XmlTypeCode.Double:
				if (n.NodeType != QilNodeType.LiteralDouble)
				{
					return base.XsltConvert(n, XmlQueryTypeFactory.StringX);
				}
				return base.String(XPathConvert.DoubleToString((QilLiteral)n));
			}
			if (n.XmlType.IsNode)
			{
				return base.XPathNodeValue(this.SafeDocOrderDistinct(n));
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.StringX);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x00101394 File Offset: 0x000FF594
		public QilNode ConvertToBoolean(QilNode n)
		{
			switch (n.XmlType.TypeCode)
			{
			case XmlTypeCode.String:
				if (n.NodeType != QilNodeType.LiteralString)
				{
					return base.Ne(base.StrLength(n), base.Int32(0));
				}
				return base.Boolean(((QilLiteral)n).Length != 0);
			case XmlTypeCode.Boolean:
				return n;
			case XmlTypeCode.Double:
				if (n.NodeType != QilNodeType.LiteralDouble)
				{
					QilIterator qilIterator;
					return base.Loop(qilIterator = base.Let(n), base.Or(base.Lt(qilIterator, base.Double(0.0)), base.Lt(base.Double(0.0), qilIterator)));
				}
				return base.Boolean((QilLiteral)n < 0.0 || 0.0 < (QilLiteral)n);
			}
			if (n.XmlType.IsNode)
			{
				return base.Not(base.IsEmpty(n));
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.BooleanX);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x001014B8 File Offset: 0x000FF6B8
		public QilNode ConvertToNumber(QilNode n)
		{
			switch (n.XmlType.TypeCode)
			{
			case XmlTypeCode.String:
				return base.XsltConvert(n, XmlQueryTypeFactory.DoubleX);
			case XmlTypeCode.Boolean:
				if (n.NodeType == QilNodeType.True)
				{
					return base.Double(1.0);
				}
				if (n.NodeType != QilNodeType.False)
				{
					return base.Conditional(n, base.Double(1.0), base.Double(0.0));
				}
				return base.Double(0.0);
			case XmlTypeCode.Double:
				return n;
			}
			if (n.XmlType.IsNode)
			{
				return base.XsltConvert(base.XPathNodeValue(this.SafeDocOrderDistinct(n)), XmlQueryTypeFactory.DoubleX);
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.DoubleX);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x0010158B File Offset: 0x000FF78B
		public QilNode ConvertToNode(QilNode n)
		{
			if (n.XmlType.IsNode && n.XmlType.IsNotRtf && n.XmlType.IsSingleton)
			{
				return n;
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.NodeNotRtf);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x001015C2 File Offset: 0x000FF7C2
		public QilNode ConvertToNodeSet(QilNode n)
		{
			if (n.XmlType.IsNode && n.XmlType.IsNotRtf)
			{
				return n;
			}
			return base.XsltConvert(n, XmlQueryTypeFactory.NodeNotRtfS);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x001015EC File Offset: 0x000FF7EC
		public QilNode TryEnsureNodeSet(QilNode n)
		{
			if (n.XmlType.IsNode && n.XmlType.IsNotRtf)
			{
				return n;
			}
			if (this.CannotBeNodeSet(n))
			{
				return null;
			}
			return this.InvokeEnsureNodeSet(n);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x0010161C File Offset: 0x000FF81C
		public QilNode EnsureNodeSet(QilNode n)
		{
			QilNode qilNode = this.TryEnsureNodeSet(n);
			if (qilNode == null)
			{
				throw new XPathCompileException("Expression must evaluate to a node-set.", Array.Empty<string>());
			}
			return qilNode;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x00101638 File Offset: 0x000FF838
		public QilNode InvokeEnsureNodeSet(QilNode n)
		{
			return base.XsltInvokeEarlyBound(base.QName("ensure-node-set"), XsltMethods.EnsureNodeSet, XmlQueryTypeFactory.NodeSDod, new QilNode[]
			{
				n
			});
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x00101660 File Offset: 0x000FF860
		public QilNode Id(QilNode context, QilNode id)
		{
			if (id.XmlType.IsSingleton)
			{
				return base.Deref(context, this.ConvertToString(id));
			}
			QilIterator n;
			return base.Loop(n = base.For(id), base.Deref(context, this.ConvertToString(n)));
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x001016A6 File Offset: 0x000FF8A6
		public QilNode InvokeStartsWith(QilNode str1, QilNode str2)
		{
			return base.XsltInvokeEarlyBound(base.QName("starts-with"), XsltMethods.StartsWith, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				str1,
				str2
			});
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x001016D1 File Offset: 0x000FF8D1
		public QilNode InvokeContains(QilNode str1, QilNode str2)
		{
			return base.XsltInvokeEarlyBound(base.QName("contains"), XsltMethods.Contains, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				str1,
				str2
			});
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x001016FC File Offset: 0x000FF8FC
		public QilNode InvokeSubstringBefore(QilNode str1, QilNode str2)
		{
			return base.XsltInvokeEarlyBound(base.QName("substring-before"), XsltMethods.SubstringBefore, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				str1,
				str2
			});
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x00101727 File Offset: 0x000FF927
		public QilNode InvokeSubstringAfter(QilNode str1, QilNode str2)
		{
			return base.XsltInvokeEarlyBound(base.QName("substring-after"), XsltMethods.SubstringAfter, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				str1,
				str2
			});
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x00101752 File Offset: 0x000FF952
		public QilNode InvokeSubstring(QilNode str, QilNode start)
		{
			return base.XsltInvokeEarlyBound(base.QName("substring"), XsltMethods.Substring2, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				str,
				start
			});
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0010177D File Offset: 0x000FF97D
		public QilNode InvokeSubstring(QilNode str, QilNode start, QilNode length)
		{
			return base.XsltInvokeEarlyBound(base.QName("substring"), XsltMethods.Substring3, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				str,
				start,
				length
			});
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x001017AC File Offset: 0x000FF9AC
		public QilNode InvokeNormalizeSpace(QilNode str)
		{
			return base.XsltInvokeEarlyBound(base.QName("normalize-space"), XsltMethods.NormalizeSpace, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				str
			});
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x001017D3 File Offset: 0x000FF9D3
		public QilNode InvokeTranslate(QilNode str1, QilNode str2, QilNode str3)
		{
			return base.XsltInvokeEarlyBound(base.QName("translate"), XsltMethods.Translate, XmlQueryTypeFactory.StringX, new QilNode[]
			{
				str1,
				str2,
				str3
			});
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x00101802 File Offset: 0x000FFA02
		public QilNode InvokeLang(QilNode lang, QilNode context)
		{
			return base.XsltInvokeEarlyBound(base.QName("lang"), XsltMethods.Lang, XmlQueryTypeFactory.BooleanX, new QilNode[]
			{
				lang,
				context
			});
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x0010182D File Offset: 0x000FFA2D
		public QilNode InvokeFloor(QilNode value)
		{
			return base.XsltInvokeEarlyBound(base.QName("floor"), XsltMethods.Floor, XmlQueryTypeFactory.DoubleX, new QilNode[]
			{
				value
			});
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x00101854 File Offset: 0x000FFA54
		public QilNode InvokeCeiling(QilNode value)
		{
			return base.XsltInvokeEarlyBound(base.QName("ceiling"), XsltMethods.Ceiling, XmlQueryTypeFactory.DoubleX, new QilNode[]
			{
				value
			});
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x0010187B File Offset: 0x000FFA7B
		public QilNode InvokeRound(QilNode value)
		{
			return base.XsltInvokeEarlyBound(base.QName("round"), XsltMethods.Round, XmlQueryTypeFactory.DoubleX, new QilNode[]
			{
				value
			});
		}
	}
}
