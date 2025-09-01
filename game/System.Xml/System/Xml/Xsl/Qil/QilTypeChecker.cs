using System;
using System.Diagnostics;
using System.Xml.Schema;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004D2 RID: 1234
	internal class QilTypeChecker
	{
		// Token: 0x0600322A RID: 12842 RVA: 0x0000216B File Offset: 0x0000036B
		public QilTypeChecker()
		{
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x00122C38 File Offset: 0x00120E38
		public XmlQueryType Check(QilNode n)
		{
			switch (n.NodeType)
			{
			case QilNodeType.QilExpression:
				return this.CheckQilExpression((QilExpression)n);
			case QilNodeType.FunctionList:
				return this.CheckFunctionList((QilList)n);
			case QilNodeType.GlobalVariableList:
				return this.CheckGlobalVariableList((QilList)n);
			case QilNodeType.GlobalParameterList:
				return this.CheckGlobalParameterList((QilList)n);
			case QilNodeType.ActualParameterList:
				return this.CheckActualParameterList((QilList)n);
			case QilNodeType.FormalParameterList:
				return this.CheckFormalParameterList((QilList)n);
			case QilNodeType.SortKeyList:
				return this.CheckSortKeyList((QilList)n);
			case QilNodeType.BranchList:
				return this.CheckBranchList((QilList)n);
			case QilNodeType.OptimizeBarrier:
				return this.CheckOptimizeBarrier((QilUnary)n);
			case QilNodeType.Unknown:
				return this.CheckUnknown(n);
			case QilNodeType.DataSource:
				return this.CheckDataSource((QilDataSource)n);
			case QilNodeType.Nop:
				return this.CheckNop((QilUnary)n);
			case QilNodeType.Error:
				return this.CheckError((QilUnary)n);
			case QilNodeType.Warning:
				return this.CheckWarning((QilUnary)n);
			case QilNodeType.For:
				return this.CheckFor((QilIterator)n);
			case QilNodeType.Let:
				return this.CheckLet((QilIterator)n);
			case QilNodeType.Parameter:
				return this.CheckParameter((QilParameter)n);
			case QilNodeType.PositionOf:
				return this.CheckPositionOf((QilUnary)n);
			case QilNodeType.True:
				return this.CheckTrue(n);
			case QilNodeType.False:
				return this.CheckFalse(n);
			case QilNodeType.LiteralString:
				return this.CheckLiteralString((QilLiteral)n);
			case QilNodeType.LiteralInt32:
				return this.CheckLiteralInt32((QilLiteral)n);
			case QilNodeType.LiteralInt64:
				return this.CheckLiteralInt64((QilLiteral)n);
			case QilNodeType.LiteralDouble:
				return this.CheckLiteralDouble((QilLiteral)n);
			case QilNodeType.LiteralDecimal:
				return this.CheckLiteralDecimal((QilLiteral)n);
			case QilNodeType.LiteralQName:
				return this.CheckLiteralQName((QilName)n);
			case QilNodeType.LiteralType:
				return this.CheckLiteralType((QilLiteral)n);
			case QilNodeType.LiteralObject:
				return this.CheckLiteralObject((QilLiteral)n);
			case QilNodeType.And:
				return this.CheckAnd((QilBinary)n);
			case QilNodeType.Or:
				return this.CheckOr((QilBinary)n);
			case QilNodeType.Not:
				return this.CheckNot((QilUnary)n);
			case QilNodeType.Conditional:
				return this.CheckConditional((QilTernary)n);
			case QilNodeType.Choice:
				return this.CheckChoice((QilChoice)n);
			case QilNodeType.Length:
				return this.CheckLength((QilUnary)n);
			case QilNodeType.Sequence:
				return this.CheckSequence((QilList)n);
			case QilNodeType.Union:
				return this.CheckUnion((QilBinary)n);
			case QilNodeType.Intersection:
				return this.CheckIntersection((QilBinary)n);
			case QilNodeType.Difference:
				return this.CheckDifference((QilBinary)n);
			case QilNodeType.Average:
				return this.CheckAverage((QilUnary)n);
			case QilNodeType.Sum:
				return this.CheckSum((QilUnary)n);
			case QilNodeType.Minimum:
				return this.CheckMinimum((QilUnary)n);
			case QilNodeType.Maximum:
				return this.CheckMaximum((QilUnary)n);
			case QilNodeType.Negate:
				return this.CheckNegate((QilUnary)n);
			case QilNodeType.Add:
				return this.CheckAdd((QilBinary)n);
			case QilNodeType.Subtract:
				return this.CheckSubtract((QilBinary)n);
			case QilNodeType.Multiply:
				return this.CheckMultiply((QilBinary)n);
			case QilNodeType.Divide:
				return this.CheckDivide((QilBinary)n);
			case QilNodeType.Modulo:
				return this.CheckModulo((QilBinary)n);
			case QilNodeType.StrLength:
				return this.CheckStrLength((QilUnary)n);
			case QilNodeType.StrConcat:
				return this.CheckStrConcat((QilStrConcat)n);
			case QilNodeType.StrParseQName:
				return this.CheckStrParseQName((QilBinary)n);
			case QilNodeType.Ne:
				return this.CheckNe((QilBinary)n);
			case QilNodeType.Eq:
				return this.CheckEq((QilBinary)n);
			case QilNodeType.Gt:
				return this.CheckGt((QilBinary)n);
			case QilNodeType.Ge:
				return this.CheckGe((QilBinary)n);
			case QilNodeType.Lt:
				return this.CheckLt((QilBinary)n);
			case QilNodeType.Le:
				return this.CheckLe((QilBinary)n);
			case QilNodeType.Is:
				return this.CheckIs((QilBinary)n);
			case QilNodeType.After:
				return this.CheckAfter((QilBinary)n);
			case QilNodeType.Before:
				return this.CheckBefore((QilBinary)n);
			case QilNodeType.Loop:
				return this.CheckLoop((QilLoop)n);
			case QilNodeType.Filter:
				return this.CheckFilter((QilLoop)n);
			case QilNodeType.Sort:
				return this.CheckSort((QilLoop)n);
			case QilNodeType.SortKey:
				return this.CheckSortKey((QilSortKey)n);
			case QilNodeType.DocOrderDistinct:
				return this.CheckDocOrderDistinct((QilUnary)n);
			case QilNodeType.Function:
				return this.CheckFunction((QilFunction)n);
			case QilNodeType.Invoke:
				return this.CheckInvoke((QilInvoke)n);
			case QilNodeType.Content:
				return this.CheckContent((QilUnary)n);
			case QilNodeType.Attribute:
				return this.CheckAttribute((QilBinary)n);
			case QilNodeType.Parent:
				return this.CheckParent((QilUnary)n);
			case QilNodeType.Root:
				return this.CheckRoot((QilUnary)n);
			case QilNodeType.XmlContext:
				return this.CheckXmlContext(n);
			case QilNodeType.Descendant:
				return this.CheckDescendant((QilUnary)n);
			case QilNodeType.DescendantOrSelf:
				return this.CheckDescendantOrSelf((QilUnary)n);
			case QilNodeType.Ancestor:
				return this.CheckAncestor((QilUnary)n);
			case QilNodeType.AncestorOrSelf:
				return this.CheckAncestorOrSelf((QilUnary)n);
			case QilNodeType.Preceding:
				return this.CheckPreceding((QilUnary)n);
			case QilNodeType.FollowingSibling:
				return this.CheckFollowingSibling((QilUnary)n);
			case QilNodeType.PrecedingSibling:
				return this.CheckPrecedingSibling((QilUnary)n);
			case QilNodeType.NodeRange:
				return this.CheckNodeRange((QilBinary)n);
			case QilNodeType.Deref:
				return this.CheckDeref((QilBinary)n);
			case QilNodeType.ElementCtor:
				return this.CheckElementCtor((QilBinary)n);
			case QilNodeType.AttributeCtor:
				return this.CheckAttributeCtor((QilBinary)n);
			case QilNodeType.CommentCtor:
				return this.CheckCommentCtor((QilUnary)n);
			case QilNodeType.PICtor:
				return this.CheckPICtor((QilBinary)n);
			case QilNodeType.TextCtor:
				return this.CheckTextCtor((QilUnary)n);
			case QilNodeType.RawTextCtor:
				return this.CheckRawTextCtor((QilUnary)n);
			case QilNodeType.DocumentCtor:
				return this.CheckDocumentCtor((QilUnary)n);
			case QilNodeType.NamespaceDecl:
				return this.CheckNamespaceDecl((QilBinary)n);
			case QilNodeType.RtfCtor:
				return this.CheckRtfCtor((QilBinary)n);
			case QilNodeType.NameOf:
				return this.CheckNameOf((QilUnary)n);
			case QilNodeType.LocalNameOf:
				return this.CheckLocalNameOf((QilUnary)n);
			case QilNodeType.NamespaceUriOf:
				return this.CheckNamespaceUriOf((QilUnary)n);
			case QilNodeType.PrefixOf:
				return this.CheckPrefixOf((QilUnary)n);
			case QilNodeType.TypeAssert:
				return this.CheckTypeAssert((QilTargetType)n);
			case QilNodeType.IsType:
				return this.CheckIsType((QilTargetType)n);
			case QilNodeType.IsEmpty:
				return this.CheckIsEmpty((QilUnary)n);
			case QilNodeType.XPathNodeValue:
				return this.CheckXPathNodeValue((QilUnary)n);
			case QilNodeType.XPathFollowing:
				return this.CheckXPathFollowing((QilUnary)n);
			case QilNodeType.XPathPreceding:
				return this.CheckXPathPreceding((QilUnary)n);
			case QilNodeType.XPathNamespace:
				return this.CheckXPathNamespace((QilUnary)n);
			case QilNodeType.XsltGenerateId:
				return this.CheckXsltGenerateId((QilUnary)n);
			case QilNodeType.XsltInvokeLateBound:
				return this.CheckXsltInvokeLateBound((QilInvokeLateBound)n);
			case QilNodeType.XsltInvokeEarlyBound:
				return this.CheckXsltInvokeEarlyBound((QilInvokeEarlyBound)n);
			case QilNodeType.XsltCopy:
				return this.CheckXsltCopy((QilBinary)n);
			case QilNodeType.XsltCopyOf:
				return this.CheckXsltCopyOf((QilUnary)n);
			case QilNodeType.XsltConvert:
				return this.CheckXsltConvert((QilTargetType)n);
			default:
				return this.CheckUnknown(n);
			}
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x00123365 File Offset: 0x00121565
		public XmlQueryType CheckQilExpression(QilExpression node)
		{
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x0012336C File Offset: 0x0012156C
		public XmlQueryType CheckFunctionList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x001233B4 File Offset: 0x001215B4
		public XmlQueryType CheckGlobalVariableList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x001233FC File Offset: 0x001215FC
		public XmlQueryType CheckGlobalParameterList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00123444 File Offset: 0x00121644
		public XmlQueryType CheckActualParameterList(QilList node)
		{
			return node.XmlType;
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x0012344C File Offset: 0x0012164C
		public XmlQueryType CheckFormalParameterList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x00123494 File Offset: 0x00121694
		public XmlQueryType CheckSortKeyList(QilList node)
		{
			foreach (QilNode qilNode in node)
			{
			}
			return node.XmlType;
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x00123444 File Offset: 0x00121644
		public XmlQueryType CheckBranchList(QilList node)
		{
			return node.XmlType;
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x001234DC File Offset: 0x001216DC
		public XmlQueryType CheckOptimizeBarrier(QilUnary node)
		{
			return node.Child.XmlType;
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x00123444 File Offset: 0x00121644
		public XmlQueryType CheckUnknown(QilNode node)
		{
			return node.XmlType;
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x001234E9 File Offset: 0x001216E9
		public XmlQueryType CheckDataSource(QilDataSource node)
		{
			return XmlQueryTypeFactory.NodeNotRtfQ;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x001234DC File Offset: 0x001216DC
		public XmlQueryType CheckNop(QilUnary node)
		{
			return node.Child.XmlType;
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x001234F0 File Offset: 0x001216F0
		public XmlQueryType CheckError(QilUnary node)
		{
			return XmlQueryTypeFactory.None;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x001234F7 File Offset: 0x001216F7
		public XmlQueryType CheckWarning(QilUnary node)
		{
			return XmlQueryTypeFactory.Empty;
		}

		// Token: 0x0600323A RID: 12858 RVA: 0x001234FE File Offset: 0x001216FE
		public XmlQueryType CheckFor(QilIterator node)
		{
			return node.Binding.XmlType.Prime;
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x00123510 File Offset: 0x00121710
		public XmlQueryType CheckLet(QilIterator node)
		{
			return node.Binding.XmlType;
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x00123444 File Offset: 0x00121644
		public XmlQueryType CheckParameter(QilParameter node)
		{
			return node.XmlType;
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x0012351D File Offset: 0x0012171D
		public XmlQueryType CheckPositionOf(QilUnary node)
		{
			return XmlQueryTypeFactory.IntX;
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x00123524 File Offset: 0x00121724
		public XmlQueryType CheckTrue(QilNode node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x00123524 File Offset: 0x00121724
		public XmlQueryType CheckFalse(QilNode node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x0012352B File Offset: 0x0012172B
		public XmlQueryType CheckLiteralString(QilLiteral node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x0012351D File Offset: 0x0012171D
		public XmlQueryType CheckLiteralInt32(QilLiteral node)
		{
			return XmlQueryTypeFactory.IntX;
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x00123532 File Offset: 0x00121732
		public XmlQueryType CheckLiteralInt64(QilLiteral node)
		{
			return XmlQueryTypeFactory.IntegerX;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x00123539 File Offset: 0x00121739
		public XmlQueryType CheckLiteralDouble(QilLiteral node)
		{
			return XmlQueryTypeFactory.DoubleX;
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x00123540 File Offset: 0x00121740
		public XmlQueryType CheckLiteralDecimal(QilLiteral node)
		{
			return XmlQueryTypeFactory.DecimalX;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x00123547 File Offset: 0x00121747
		public XmlQueryType CheckLiteralQName(QilName node)
		{
			return XmlQueryTypeFactory.QNameX;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x0012354E File Offset: 0x0012174E
		public XmlQueryType CheckLiteralType(QilLiteral node)
		{
			return node;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x00123365 File Offset: 0x00121565
		public XmlQueryType CheckLiteralObject(QilLiteral node)
		{
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x00123524 File Offset: 0x00121724
		public XmlQueryType CheckAnd(QilBinary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x00123556 File Offset: 0x00121756
		public XmlQueryType CheckOr(QilBinary node)
		{
			return this.CheckAnd(node);
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x00123524 File Offset: 0x00121724
		public XmlQueryType CheckNot(QilUnary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0012355F File Offset: 0x0012175F
		public XmlQueryType CheckConditional(QilTernary node)
		{
			return XmlQueryTypeFactory.Choice(node.Center.XmlType, node.Right.XmlType);
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x0012357C File Offset: 0x0012177C
		public XmlQueryType CheckChoice(QilChoice node)
		{
			return node.Branches.XmlType;
		}

		// Token: 0x0600324D RID: 12877 RVA: 0x0012351D File Offset: 0x0012171D
		public XmlQueryType CheckLength(QilUnary node)
		{
			return XmlQueryTypeFactory.IntX;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x00123444 File Offset: 0x00121644
		public XmlQueryType CheckSequence(QilList node)
		{
			return node.XmlType;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x00123589 File Offset: 0x00121789
		public XmlQueryType CheckUnion(QilBinary node)
		{
			return this.DistinctType(XmlQueryTypeFactory.Sequence(node.Left.XmlType, node.Right.XmlType));
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x001235AC File Offset: 0x001217AC
		public XmlQueryType CheckIntersection(QilBinary node)
		{
			return this.CheckUnion(node);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x001235B5 File Offset: 0x001217B5
		public XmlQueryType CheckDifference(QilBinary node)
		{
			return XmlQueryTypeFactory.AtMost(node.Left.XmlType, node.Left.XmlType.Cardinality);
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x001235D7 File Offset: 0x001217D7
		public XmlQueryType CheckAverage(QilUnary node)
		{
			XmlQueryType xmlType = node.Child.XmlType;
			return XmlQueryTypeFactory.PrimeProduct(xmlType, xmlType.MaybeEmpty ? XmlQueryCardinality.ZeroOrOne : XmlQueryCardinality.One);
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x001235FD File Offset: 0x001217FD
		public XmlQueryType CheckSum(QilUnary node)
		{
			return this.CheckAverage(node);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x001235FD File Offset: 0x001217FD
		public XmlQueryType CheckMinimum(QilUnary node)
		{
			return this.CheckAverage(node);
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x001235FD File Offset: 0x001217FD
		public XmlQueryType CheckMaximum(QilUnary node)
		{
			return this.CheckAverage(node);
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x001234DC File Offset: 0x001216DC
		public XmlQueryType CheckNegate(QilUnary node)
		{
			return node.Child.XmlType;
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x00123606 File Offset: 0x00121806
		public XmlQueryType CheckAdd(QilBinary node)
		{
			if (node.Left.XmlType.TypeCode != XmlTypeCode.None)
			{
				return node.Left.XmlType;
			}
			return node.Right.XmlType;
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x00123631 File Offset: 0x00121831
		public XmlQueryType CheckSubtract(QilBinary node)
		{
			return this.CheckAdd(node);
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x00123631 File Offset: 0x00121831
		public XmlQueryType CheckMultiply(QilBinary node)
		{
			return this.CheckAdd(node);
		}

		// Token: 0x0600325A RID: 12890 RVA: 0x00123631 File Offset: 0x00121831
		public XmlQueryType CheckDivide(QilBinary node)
		{
			return this.CheckAdd(node);
		}

		// Token: 0x0600325B RID: 12891 RVA: 0x00123631 File Offset: 0x00121831
		public XmlQueryType CheckModulo(QilBinary node)
		{
			return this.CheckAdd(node);
		}

		// Token: 0x0600325C RID: 12892 RVA: 0x0012351D File Offset: 0x0012171D
		public XmlQueryType CheckStrLength(QilUnary node)
		{
			return XmlQueryTypeFactory.IntX;
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x0012352B File Offset: 0x0012172B
		public XmlQueryType CheckStrConcat(QilStrConcat node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x0600325E RID: 12894 RVA: 0x00123547 File Offset: 0x00121747
		public XmlQueryType CheckStrParseQName(QilBinary node)
		{
			return XmlQueryTypeFactory.QNameX;
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x00123524 File Offset: 0x00121724
		public XmlQueryType CheckNe(QilBinary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x0012363A File Offset: 0x0012183A
		public XmlQueryType CheckEq(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x0012363A File Offset: 0x0012183A
		public XmlQueryType CheckGt(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x0012363A File Offset: 0x0012183A
		public XmlQueryType CheckGe(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x0012363A File Offset: 0x0012183A
		public XmlQueryType CheckLt(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x0012363A File Offset: 0x0012183A
		public XmlQueryType CheckLe(QilBinary node)
		{
			return this.CheckNe(node);
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x00123524 File Offset: 0x00121724
		public XmlQueryType CheckIs(QilBinary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x00123643 File Offset: 0x00121843
		public XmlQueryType CheckAfter(QilBinary node)
		{
			return this.CheckIs(node);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x00123643 File Offset: 0x00121843
		public XmlQueryType CheckBefore(QilBinary node)
		{
			return this.CheckIs(node);
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x0012364C File Offset: 0x0012184C
		public XmlQueryType CheckLoop(QilLoop node)
		{
			XmlQueryType xmlType = node.Body.XmlType;
			XmlQueryCardinality left = (node.Variable.NodeType == QilNodeType.Let) ? XmlQueryCardinality.One : node.Variable.Binding.XmlType.Cardinality;
			return XmlQueryTypeFactory.PrimeProduct(xmlType, left * xmlType.Cardinality);
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x001236A4 File Offset: 0x001218A4
		public XmlQueryType CheckFilter(QilLoop node)
		{
			XmlQueryType xmlQueryType = this.FindFilterType(node.Variable, node.Body);
			if (xmlQueryType != null)
			{
				return xmlQueryType;
			}
			return XmlQueryTypeFactory.AtMost(node.Variable.Binding.XmlType, node.Variable.Binding.XmlType.Cardinality);
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x001236F9 File Offset: 0x001218F9
		public XmlQueryType CheckSort(QilLoop node)
		{
			XmlQueryType xmlType = node.Variable.Binding.XmlType;
			return XmlQueryTypeFactory.PrimeProduct(xmlType, xmlType.Cardinality);
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x00123716 File Offset: 0x00121916
		public XmlQueryType CheckSortKey(QilSortKey node)
		{
			return node.Key.XmlType;
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x00123723 File Offset: 0x00121923
		public XmlQueryType CheckDocOrderDistinct(QilUnary node)
		{
			return this.DistinctType(node.Child.XmlType);
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x00123444 File Offset: 0x00121644
		public XmlQueryType CheckFunction(QilFunction node)
		{
			return node.XmlType;
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x00123736 File Offset: 0x00121936
		public XmlQueryType CheckInvoke(QilInvoke node)
		{
			return node.Function.XmlType;
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x00123743 File Offset: 0x00121943
		public XmlQueryType CheckContent(QilUnary node)
		{
			return XmlQueryTypeFactory.AttributeOrContentS;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x0012374A File Offset: 0x0012194A
		public XmlQueryType CheckAttribute(QilBinary node)
		{
			return XmlQueryTypeFactory.AttributeQ;
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x00123751 File Offset: 0x00121951
		public XmlQueryType CheckParent(QilUnary node)
		{
			return XmlQueryTypeFactory.DocumentOrElementQ;
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x00123758 File Offset: 0x00121958
		public XmlQueryType CheckRoot(QilUnary node)
		{
			return XmlQueryTypeFactory.NodeNotRtf;
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x00123758 File Offset: 0x00121958
		public XmlQueryType CheckXmlContext(QilNode node)
		{
			return XmlQueryTypeFactory.NodeNotRtf;
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x0012375F File Offset: 0x0012195F
		public XmlQueryType CheckDescendant(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x00123766 File Offset: 0x00121966
		public XmlQueryType CheckDescendantOrSelf(QilUnary node)
		{
			return XmlQueryTypeFactory.Choice(node.Child.XmlType, XmlQueryTypeFactory.ContentS);
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x0012377D File Offset: 0x0012197D
		public XmlQueryType CheckAncestor(QilUnary node)
		{
			return XmlQueryTypeFactory.DocumentOrElementS;
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x00123784 File Offset: 0x00121984
		public XmlQueryType CheckAncestorOrSelf(QilUnary node)
		{
			return XmlQueryTypeFactory.Choice(node.Child.XmlType, XmlQueryTypeFactory.DocumentOrElementS);
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x0012379B File Offset: 0x0012199B
		public XmlQueryType CheckPreceding(QilUnary node)
		{
			return XmlQueryTypeFactory.DocumentOrContentS;
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x0012375F File Offset: 0x0012195F
		public XmlQueryType CheckFollowingSibling(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x0012375F File Offset: 0x0012195F
		public XmlQueryType CheckPrecedingSibling(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x001237A2 File Offset: 0x001219A2
		public XmlQueryType CheckNodeRange(QilBinary node)
		{
			return XmlQueryTypeFactory.Choice(new XmlQueryType[]
			{
				node.Left.XmlType,
				XmlQueryTypeFactory.ContentS,
				node.Right.XmlType
			});
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x001237D3 File Offset: 0x001219D3
		public XmlQueryType CheckDeref(QilBinary node)
		{
			return XmlQueryTypeFactory.ElementS;
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x001237DA File Offset: 0x001219DA
		public XmlQueryType CheckElementCtor(QilBinary node)
		{
			return XmlQueryTypeFactory.UntypedElement;
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x001237E1 File Offset: 0x001219E1
		public XmlQueryType CheckAttributeCtor(QilBinary node)
		{
			return XmlQueryTypeFactory.UntypedAttribute;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x001237E8 File Offset: 0x001219E8
		public XmlQueryType CheckCommentCtor(QilUnary node)
		{
			return XmlQueryTypeFactory.Comment;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x001237EF File Offset: 0x001219EF
		public XmlQueryType CheckPICtor(QilBinary node)
		{
			return XmlQueryTypeFactory.PI;
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x001237F6 File Offset: 0x001219F6
		public XmlQueryType CheckTextCtor(QilUnary node)
		{
			return XmlQueryTypeFactory.Text;
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x001237F6 File Offset: 0x001219F6
		public XmlQueryType CheckRawTextCtor(QilUnary node)
		{
			return XmlQueryTypeFactory.Text;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x001237FD File Offset: 0x001219FD
		public XmlQueryType CheckDocumentCtor(QilUnary node)
		{
			return XmlQueryTypeFactory.UntypedDocument;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x00123804 File Offset: 0x00121A04
		public XmlQueryType CheckNamespaceDecl(QilBinary node)
		{
			return XmlQueryTypeFactory.Namespace;
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x0012380B File Offset: 0x00121A0B
		public XmlQueryType CheckRtfCtor(QilBinary node)
		{
			return XmlQueryTypeFactory.Node;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x00123547 File Offset: 0x00121747
		public XmlQueryType CheckNameOf(QilUnary node)
		{
			return XmlQueryTypeFactory.QNameX;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x0012352B File Offset: 0x0012172B
		public XmlQueryType CheckLocalNameOf(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x0012352B File Offset: 0x0012172B
		public XmlQueryType CheckNamespaceUriOf(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x0012352B File Offset: 0x0012172B
		public XmlQueryType CheckPrefixOf(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x00123812 File Offset: 0x00121A12
		public XmlQueryType CheckTypeAssert(QilTargetType node)
		{
			return node.TargetType;
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x00123524 File Offset: 0x00121724
		public XmlQueryType CheckIsType(QilTargetType node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x00123524 File Offset: 0x00121724
		public XmlQueryType CheckIsEmpty(QilUnary node)
		{
			return XmlQueryTypeFactory.BooleanX;
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x0012352B File Offset: 0x0012172B
		public XmlQueryType CheckXPathNodeValue(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x0012375F File Offset: 0x0012195F
		public XmlQueryType CheckXPathFollowing(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x0012375F File Offset: 0x0012195F
		public XmlQueryType CheckXPathPreceding(QilUnary node)
		{
			return XmlQueryTypeFactory.ContentS;
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x0012381A File Offset: 0x00121A1A
		public XmlQueryType CheckXPathNamespace(QilUnary node)
		{
			return XmlQueryTypeFactory.NamespaceS;
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x0012352B File Offset: 0x0012172B
		public XmlQueryType CheckXsltGenerateId(QilUnary node)
		{
			return XmlQueryTypeFactory.StringX;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x00123365 File Offset: 0x00121565
		public XmlQueryType CheckXsltInvokeLateBound(QilInvokeLateBound node)
		{
			return XmlQueryTypeFactory.ItemS;
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x00123444 File Offset: 0x00121644
		public XmlQueryType CheckXsltInvokeEarlyBound(QilInvokeEarlyBound node)
		{
			return node.XmlType;
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x00123821 File Offset: 0x00121A21
		public XmlQueryType CheckXsltCopy(QilBinary node)
		{
			return XmlQueryTypeFactory.Choice(node.Left.XmlType, node.Right.XmlType);
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x0012383E File Offset: 0x00121A3E
		public XmlQueryType CheckXsltCopyOf(QilUnary node)
		{
			if ((node.Child.XmlType.NodeKinds & XmlNodeKindFlags.Document) != XmlNodeKindFlags.None)
			{
				return XmlQueryTypeFactory.NodeNotRtfS;
			}
			return node.Child.XmlType;
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x00123812 File Offset: 0x00121A12
		public XmlQueryType CheckXsltConvert(QilTargetType node)
		{
			return node.TargetType;
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x00123865 File Offset: 0x00121A65
		[Conditional("DEBUG")]
		private void Check(bool value, QilNode node, string message)
		{
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x00123869 File Offset: 0x00121A69
		[Conditional("DEBUG")]
		private void CheckLiteralValue(QilNode node, Type clrTypeValue)
		{
			((QilLiteral)node).Value.GetType();
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckClass(QilNode node, Type clrTypeClass)
		{
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckClassAndNodeType(QilNode node, Type clrTypeClass, QilNodeType nodeType)
		{
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckXmlType(QilNode node, XmlQueryType xmlType)
		{
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckNumericX(QilNode node)
		{
		}

		// Token: 0x0600329D RID: 12957 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckNumericXS(QilNode node)
		{
		}

		// Token: 0x0600329E RID: 12958 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckAtomicX(QilNode node)
		{
		}

		// Token: 0x0600329F RID: 12959 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		private void CheckNotDisjoint(QilBinary node)
		{
		}

		// Token: 0x060032A0 RID: 12960 RVA: 0x0012387C File Offset: 0x00121A7C
		private XmlQueryType DistinctType(XmlQueryType type)
		{
			if (type.Cardinality == XmlQueryCardinality.More)
			{
				return XmlQueryTypeFactory.PrimeProduct(type, XmlQueryCardinality.OneOrMore);
			}
			if (type.Cardinality == XmlQueryCardinality.NotOne)
			{
				return XmlQueryTypeFactory.PrimeProduct(type, XmlQueryCardinality.ZeroOrMore);
			}
			return type;
		}

		// Token: 0x060032A1 RID: 12961 RVA: 0x001238BC File Offset: 0x00121ABC
		private XmlQueryType FindFilterType(QilIterator variable, QilNode body)
		{
			if (body.XmlType.TypeCode == XmlTypeCode.None)
			{
				return XmlQueryTypeFactory.None;
			}
			QilNodeType nodeType = body.NodeType;
			if (nodeType <= QilNodeType.And)
			{
				if (nodeType == QilNodeType.False)
				{
					return XmlQueryTypeFactory.Empty;
				}
				if (nodeType == QilNodeType.And)
				{
					XmlQueryType xmlQueryType = this.FindFilterType(variable, ((QilBinary)body).Left);
					if (xmlQueryType != null)
					{
						return xmlQueryType;
					}
					return this.FindFilterType(variable, ((QilBinary)body).Right);
				}
			}
			else if (nodeType != QilNodeType.Eq)
			{
				if (nodeType == QilNodeType.IsType)
				{
					if (((QilTargetType)body).Source == variable)
					{
						return XmlQueryTypeFactory.AtMost(((QilTargetType)body).TargetType, variable.Binding.XmlType.Cardinality);
					}
				}
			}
			else
			{
				QilBinary qilBinary = (QilBinary)body;
				if (qilBinary.Left.NodeType == QilNodeType.PositionOf && ((QilUnary)qilBinary.Left).Child == variable)
				{
					return XmlQueryTypeFactory.AtMost(variable.Binding.XmlType, XmlQueryCardinality.ZeroOrOne);
				}
			}
			return null;
		}
	}
}
