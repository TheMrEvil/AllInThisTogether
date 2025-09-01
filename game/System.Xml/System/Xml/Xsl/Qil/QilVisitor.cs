using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004D5 RID: 1237
	internal abstract class QilVisitor
	{
		// Token: 0x060032AB RID: 12971 RVA: 0x00123A5E File Offset: 0x00121C5E
		protected virtual QilNode VisitAssumeReference(QilNode expr)
		{
			if (expr is QilReference)
			{
				return this.VisitReference(expr);
			}
			return this.Visit(expr);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x00123A78 File Offset: 0x00121C78
		protected virtual QilNode VisitChildren(QilNode parent)
		{
			for (int i = 0; i < parent.Count; i++)
			{
				if (this.IsReference(parent, i))
				{
					this.VisitReference(parent[i]);
				}
				else
				{
					this.Visit(parent[i]);
				}
			}
			return parent;
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x00123AC0 File Offset: 0x00121CC0
		protected virtual bool IsReference(QilNode parent, int childNum)
		{
			QilNode qilNode = parent[childNum];
			if (qilNode != null)
			{
				QilNodeType nodeType = qilNode.NodeType;
				if (nodeType - QilNodeType.For <= 2)
				{
					QilNodeType nodeType2 = parent.NodeType;
					return nodeType2 - QilNodeType.GlobalVariableList > 1 && nodeType2 != QilNodeType.FormalParameterList && (nodeType2 - QilNodeType.Loop > 2 || childNum == 1);
				}
				if (nodeType == QilNodeType.Function)
				{
					return parent.NodeType == QilNodeType.Invoke;
				}
			}
			return false;
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x00123B1C File Offset: 0x00121D1C
		protected virtual QilNode Visit(QilNode n)
		{
			if (n == null)
			{
				return this.VisitNull();
			}
			switch (n.NodeType)
			{
			case QilNodeType.QilExpression:
				return this.VisitQilExpression((QilExpression)n);
			case QilNodeType.FunctionList:
				return this.VisitFunctionList((QilList)n);
			case QilNodeType.GlobalVariableList:
				return this.VisitGlobalVariableList((QilList)n);
			case QilNodeType.GlobalParameterList:
				return this.VisitGlobalParameterList((QilList)n);
			case QilNodeType.ActualParameterList:
				return this.VisitActualParameterList((QilList)n);
			case QilNodeType.FormalParameterList:
				return this.VisitFormalParameterList((QilList)n);
			case QilNodeType.SortKeyList:
				return this.VisitSortKeyList((QilList)n);
			case QilNodeType.BranchList:
				return this.VisitBranchList((QilList)n);
			case QilNodeType.OptimizeBarrier:
				return this.VisitOptimizeBarrier((QilUnary)n);
			case QilNodeType.Unknown:
				return this.VisitUnknown(n);
			case QilNodeType.DataSource:
				return this.VisitDataSource((QilDataSource)n);
			case QilNodeType.Nop:
				return this.VisitNop((QilUnary)n);
			case QilNodeType.Error:
				return this.VisitError((QilUnary)n);
			case QilNodeType.Warning:
				return this.VisitWarning((QilUnary)n);
			case QilNodeType.For:
				return this.VisitFor((QilIterator)n);
			case QilNodeType.Let:
				return this.VisitLet((QilIterator)n);
			case QilNodeType.Parameter:
				return this.VisitParameter((QilParameter)n);
			case QilNodeType.PositionOf:
				return this.VisitPositionOf((QilUnary)n);
			case QilNodeType.True:
				return this.VisitTrue(n);
			case QilNodeType.False:
				return this.VisitFalse(n);
			case QilNodeType.LiteralString:
				return this.VisitLiteralString((QilLiteral)n);
			case QilNodeType.LiteralInt32:
				return this.VisitLiteralInt32((QilLiteral)n);
			case QilNodeType.LiteralInt64:
				return this.VisitLiteralInt64((QilLiteral)n);
			case QilNodeType.LiteralDouble:
				return this.VisitLiteralDouble((QilLiteral)n);
			case QilNodeType.LiteralDecimal:
				return this.VisitLiteralDecimal((QilLiteral)n);
			case QilNodeType.LiteralQName:
				return this.VisitLiteralQName((QilName)n);
			case QilNodeType.LiteralType:
				return this.VisitLiteralType((QilLiteral)n);
			case QilNodeType.LiteralObject:
				return this.VisitLiteralObject((QilLiteral)n);
			case QilNodeType.And:
				return this.VisitAnd((QilBinary)n);
			case QilNodeType.Or:
				return this.VisitOr((QilBinary)n);
			case QilNodeType.Not:
				return this.VisitNot((QilUnary)n);
			case QilNodeType.Conditional:
				return this.VisitConditional((QilTernary)n);
			case QilNodeType.Choice:
				return this.VisitChoice((QilChoice)n);
			case QilNodeType.Length:
				return this.VisitLength((QilUnary)n);
			case QilNodeType.Sequence:
				return this.VisitSequence((QilList)n);
			case QilNodeType.Union:
				return this.VisitUnion((QilBinary)n);
			case QilNodeType.Intersection:
				return this.VisitIntersection((QilBinary)n);
			case QilNodeType.Difference:
				return this.VisitDifference((QilBinary)n);
			case QilNodeType.Average:
				return this.VisitAverage((QilUnary)n);
			case QilNodeType.Sum:
				return this.VisitSum((QilUnary)n);
			case QilNodeType.Minimum:
				return this.VisitMinimum((QilUnary)n);
			case QilNodeType.Maximum:
				return this.VisitMaximum((QilUnary)n);
			case QilNodeType.Negate:
				return this.VisitNegate((QilUnary)n);
			case QilNodeType.Add:
				return this.VisitAdd((QilBinary)n);
			case QilNodeType.Subtract:
				return this.VisitSubtract((QilBinary)n);
			case QilNodeType.Multiply:
				return this.VisitMultiply((QilBinary)n);
			case QilNodeType.Divide:
				return this.VisitDivide((QilBinary)n);
			case QilNodeType.Modulo:
				return this.VisitModulo((QilBinary)n);
			case QilNodeType.StrLength:
				return this.VisitStrLength((QilUnary)n);
			case QilNodeType.StrConcat:
				return this.VisitStrConcat((QilStrConcat)n);
			case QilNodeType.StrParseQName:
				return this.VisitStrParseQName((QilBinary)n);
			case QilNodeType.Ne:
				return this.VisitNe((QilBinary)n);
			case QilNodeType.Eq:
				return this.VisitEq((QilBinary)n);
			case QilNodeType.Gt:
				return this.VisitGt((QilBinary)n);
			case QilNodeType.Ge:
				return this.VisitGe((QilBinary)n);
			case QilNodeType.Lt:
				return this.VisitLt((QilBinary)n);
			case QilNodeType.Le:
				return this.VisitLe((QilBinary)n);
			case QilNodeType.Is:
				return this.VisitIs((QilBinary)n);
			case QilNodeType.After:
				return this.VisitAfter((QilBinary)n);
			case QilNodeType.Before:
				return this.VisitBefore((QilBinary)n);
			case QilNodeType.Loop:
				return this.VisitLoop((QilLoop)n);
			case QilNodeType.Filter:
				return this.VisitFilter((QilLoop)n);
			case QilNodeType.Sort:
				return this.VisitSort((QilLoop)n);
			case QilNodeType.SortKey:
				return this.VisitSortKey((QilSortKey)n);
			case QilNodeType.DocOrderDistinct:
				return this.VisitDocOrderDistinct((QilUnary)n);
			case QilNodeType.Function:
				return this.VisitFunction((QilFunction)n);
			case QilNodeType.Invoke:
				return this.VisitInvoke((QilInvoke)n);
			case QilNodeType.Content:
				return this.VisitContent((QilUnary)n);
			case QilNodeType.Attribute:
				return this.VisitAttribute((QilBinary)n);
			case QilNodeType.Parent:
				return this.VisitParent((QilUnary)n);
			case QilNodeType.Root:
				return this.VisitRoot((QilUnary)n);
			case QilNodeType.XmlContext:
				return this.VisitXmlContext(n);
			case QilNodeType.Descendant:
				return this.VisitDescendant((QilUnary)n);
			case QilNodeType.DescendantOrSelf:
				return this.VisitDescendantOrSelf((QilUnary)n);
			case QilNodeType.Ancestor:
				return this.VisitAncestor((QilUnary)n);
			case QilNodeType.AncestorOrSelf:
				return this.VisitAncestorOrSelf((QilUnary)n);
			case QilNodeType.Preceding:
				return this.VisitPreceding((QilUnary)n);
			case QilNodeType.FollowingSibling:
				return this.VisitFollowingSibling((QilUnary)n);
			case QilNodeType.PrecedingSibling:
				return this.VisitPrecedingSibling((QilUnary)n);
			case QilNodeType.NodeRange:
				return this.VisitNodeRange((QilBinary)n);
			case QilNodeType.Deref:
				return this.VisitDeref((QilBinary)n);
			case QilNodeType.ElementCtor:
				return this.VisitElementCtor((QilBinary)n);
			case QilNodeType.AttributeCtor:
				return this.VisitAttributeCtor((QilBinary)n);
			case QilNodeType.CommentCtor:
				return this.VisitCommentCtor((QilUnary)n);
			case QilNodeType.PICtor:
				return this.VisitPICtor((QilBinary)n);
			case QilNodeType.TextCtor:
				return this.VisitTextCtor((QilUnary)n);
			case QilNodeType.RawTextCtor:
				return this.VisitRawTextCtor((QilUnary)n);
			case QilNodeType.DocumentCtor:
				return this.VisitDocumentCtor((QilUnary)n);
			case QilNodeType.NamespaceDecl:
				return this.VisitNamespaceDecl((QilBinary)n);
			case QilNodeType.RtfCtor:
				return this.VisitRtfCtor((QilBinary)n);
			case QilNodeType.NameOf:
				return this.VisitNameOf((QilUnary)n);
			case QilNodeType.LocalNameOf:
				return this.VisitLocalNameOf((QilUnary)n);
			case QilNodeType.NamespaceUriOf:
				return this.VisitNamespaceUriOf((QilUnary)n);
			case QilNodeType.PrefixOf:
				return this.VisitPrefixOf((QilUnary)n);
			case QilNodeType.TypeAssert:
				return this.VisitTypeAssert((QilTargetType)n);
			case QilNodeType.IsType:
				return this.VisitIsType((QilTargetType)n);
			case QilNodeType.IsEmpty:
				return this.VisitIsEmpty((QilUnary)n);
			case QilNodeType.XPathNodeValue:
				return this.VisitXPathNodeValue((QilUnary)n);
			case QilNodeType.XPathFollowing:
				return this.VisitXPathFollowing((QilUnary)n);
			case QilNodeType.XPathPreceding:
				return this.VisitXPathPreceding((QilUnary)n);
			case QilNodeType.XPathNamespace:
				return this.VisitXPathNamespace((QilUnary)n);
			case QilNodeType.XsltGenerateId:
				return this.VisitXsltGenerateId((QilUnary)n);
			case QilNodeType.XsltInvokeLateBound:
				return this.VisitXsltInvokeLateBound((QilInvokeLateBound)n);
			case QilNodeType.XsltInvokeEarlyBound:
				return this.VisitXsltInvokeEarlyBound((QilInvokeEarlyBound)n);
			case QilNodeType.XsltCopy:
				return this.VisitXsltCopy((QilBinary)n);
			case QilNodeType.XsltCopyOf:
				return this.VisitXsltCopyOf((QilUnary)n);
			case QilNodeType.XsltConvert:
				return this.VisitXsltConvert((QilTargetType)n);
			default:
				return this.VisitUnknown(n);
			}
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x00124254 File Offset: 0x00122454
		protected virtual QilNode VisitReference(QilNode n)
		{
			if (n == null)
			{
				return this.VisitNull();
			}
			QilNodeType nodeType = n.NodeType;
			switch (nodeType)
			{
			case QilNodeType.For:
				return this.VisitForReference((QilIterator)n);
			case QilNodeType.Let:
				return this.VisitLetReference((QilIterator)n);
			case QilNodeType.Parameter:
				return this.VisitParameterReference((QilParameter)n);
			default:
				if (nodeType != QilNodeType.Function)
				{
					return this.VisitUnknown(n);
				}
				return this.VisitFunctionReference((QilFunction)n);
			}
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x0001DA42 File Offset: 0x0001BC42
		protected virtual QilNode VisitNull()
		{
			return null;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitQilExpression(QilExpression n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitFunctionList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitGlobalVariableList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitGlobalParameterList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitActualParameterList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitFormalParameterList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitSortKeyList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitBranchList(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitOptimizeBarrier(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitUnknown(QilNode n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitDataSource(QilDataSource n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitNop(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitError(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitWarning(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitFor(QilIterator n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode VisitForReference(QilIterator n)
		{
			return n;
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLet(QilIterator n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode VisitLetReference(QilIterator n)
		{
			return n;
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitParameter(QilParameter n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode VisitParameterReference(QilParameter n)
		{
			return n;
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitPositionOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitTrue(QilNode n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitFalse(QilNode n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLiteralString(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLiteralInt32(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLiteralInt64(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLiteralDouble(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLiteralDecimal(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLiteralQName(QilName n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLiteralType(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLiteralObject(QilLiteral n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitAnd(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitOr(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitNot(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitConditional(QilTernary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitChoice(QilChoice n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLength(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitSequence(QilList n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitUnion(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitIntersection(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitDifference(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitAverage(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitSum(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitMinimum(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitMaximum(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitNegate(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitAdd(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitSubtract(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitMultiply(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitDivide(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitModulo(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitStrLength(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitStrConcat(QilStrConcat n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitStrParseQName(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitNe(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitEq(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitGt(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitGe(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLt(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLe(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitIs(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitAfter(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitBefore(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLoop(QilLoop n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitFilter(QilLoop n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitSort(QilLoop n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitSortKey(QilSortKey n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitDocOrderDistinct(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitFunction(QilFunction n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode VisitFunctionReference(QilFunction n)
		{
			return n;
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitInvoke(QilInvoke n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitContent(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitAttribute(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitParent(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032FB RID: 13051 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitRoot(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXmlContext(QilNode n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitDescendant(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitDescendantOrSelf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitAncestor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitAncestorOrSelf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitPreceding(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitFollowingSibling(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitPrecedingSibling(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitNodeRange(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003305 RID: 13061 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitDeref(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003306 RID: 13062 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitElementCtor(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003307 RID: 13063 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitAttributeCtor(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003308 RID: 13064 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitCommentCtor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003309 RID: 13065 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitPICtor(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600330A RID: 13066 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitTextCtor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600330B RID: 13067 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitRawTextCtor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600330C RID: 13068 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitDocumentCtor(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600330D RID: 13069 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitNamespaceDecl(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600330E RID: 13070 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitRtfCtor(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitNameOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitLocalNameOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitNamespaceUriOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitPrefixOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitTypeAssert(QilTargetType n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003314 RID: 13076 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitIsType(QilTargetType n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003315 RID: 13077 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitIsEmpty(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003316 RID: 13078 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXPathNodeValue(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003317 RID: 13079 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXPathFollowing(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003318 RID: 13080 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXPathPreceding(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXPathNamespace(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600331A RID: 13082 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXsltGenerateId(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXsltInvokeLateBound(QilInvokeLateBound n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXsltInvokeEarlyBound(QilInvokeEarlyBound n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXsltCopy(QilBinary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXsltCopyOf(QilUnary n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x001242C9 File Offset: 0x001224C9
		protected virtual QilNode VisitXsltConvert(QilTargetType n)
		{
			return this.VisitChildren(n);
		}

		// Token: 0x06003320 RID: 13088 RVA: 0x0000216B File Offset: 0x0000036B
		protected QilVisitor()
		{
		}
	}
}
