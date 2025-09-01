using System;
using System.Collections;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C9 RID: 1225
	internal abstract class QilPatternVisitor : QilReplaceVisitor
	{
		// Token: 0x06003184 RID: 12676 RVA: 0x001226AD File Offset: 0x001208AD
		public QilPatternVisitor(QilPatternVisitor.QilPatterns patterns, QilFactory f) : base(f)
		{
			this.Patterns = patterns;
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x001226C8 File Offset: 0x001208C8
		// (set) Token: 0x06003186 RID: 12678 RVA: 0x001226D0 File Offset: 0x001208D0
		public QilPatternVisitor.QilPatterns Patterns
		{
			get
			{
				return this._patterns;
			}
			set
			{
				this._patterns = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x001226D9 File Offset: 0x001208D9
		// (set) Token: 0x06003188 RID: 12680 RVA: 0x001226E1 File Offset: 0x001208E1
		public int Threshold
		{
			get
			{
				return this._threshold;
			}
			set
			{
				this._threshold = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x001226EA File Offset: 0x001208EA
		public int ReplacementCount
		{
			get
			{
				return this._replacementCnt;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x001226F2 File Offset: 0x001208F2
		public int LastReplacement
		{
			get
			{
				return this._lastReplacement;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x001226FA File Offset: 0x001208FA
		public bool Matching
		{
			get
			{
				return this.ReplacementCount < this.Threshold;
			}
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x0012270A File Offset: 0x0012090A
		protected virtual bool AllowReplace(int pattern, QilNode original)
		{
			if (this.Matching)
			{
				this._replacementCnt++;
				this._lastReplacement = pattern;
				return true;
			}
			return false;
		}

		// Token: 0x0600318D RID: 12685 RVA: 0x0012272C File Offset: 0x0012092C
		protected virtual QilNode Replace(int pattern, QilNode original, QilNode replacement)
		{
			replacement.SourceLine = original.SourceLine;
			return replacement;
		}

		// Token: 0x0600318E RID: 12686 RVA: 0x0000206B File Offset: 0x0000026B
		protected virtual QilNode NoReplace(QilNode node)
		{
			return node;
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x0012273B File Offset: 0x0012093B
		protected override QilNode Visit(QilNode node)
		{
			if (node == null)
			{
				return this.VisitNull();
			}
			node = this.VisitChildren(node);
			return base.Visit(node);
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitQilExpression(QilExpression n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003191 RID: 12689 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitFunctionList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003192 RID: 12690 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitGlobalVariableList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitGlobalParameterList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitActualParameterList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitFormalParameterList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitSortKeyList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitBranchList(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003198 RID: 12696 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitOptimizeBarrier(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x06003199 RID: 12697 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitUnknown(QilNode n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x0600319A RID: 12698 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitDataSource(QilDataSource n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x0600319B RID: 12699 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitNop(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x0600319C RID: 12700 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitError(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x0600319D RID: 12701 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitWarning(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitFor(QilIterator n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitForReference(QilIterator n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLet(QilIterator n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLetReference(QilIterator n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitParameter(QilParameter n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitParameterReference(QilParameter n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitPositionOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitTrue(QilNode n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitFalse(QilNode n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLiteralString(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLiteralInt32(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLiteralInt64(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLiteralDouble(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLiteralDecimal(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLiteralQName(QilName n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLiteralType(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLiteralObject(QilLiteral n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitAnd(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitOr(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitNot(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitConditional(QilTernary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitChoice(QilChoice n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLength(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitSequence(QilList n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitUnion(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitIntersection(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitDifference(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitAverage(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitSum(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitMinimum(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitMaximum(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitNegate(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitAdd(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitSubtract(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitMultiply(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitDivide(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitModulo(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitStrLength(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitStrConcat(QilStrConcat n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitStrParseQName(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitNe(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitEq(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitGt(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitGe(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLt(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLe(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitIs(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitAfter(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitBefore(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLoop(QilLoop n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitFilter(QilLoop n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitSort(QilLoop n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitSortKey(QilSortKey n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitDocOrderDistinct(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitFunction(QilFunction n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitFunctionReference(QilFunction n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitInvoke(QilInvoke n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitContent(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitAttribute(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitParent(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitRoot(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXmlContext(QilNode n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitDescendant(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitDescendantOrSelf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitAncestor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitAncestorOrSelf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitPreceding(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitFollowingSibling(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitPrecedingSibling(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitNodeRange(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitDeref(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitElementCtor(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitAttributeCtor(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitCommentCtor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitPICtor(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitTextCtor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitRawTextCtor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitDocumentCtor(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitNamespaceDecl(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitRtfCtor(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitNameOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitLocalNameOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitNamespaceUriOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitPrefixOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitTypeAssert(QilTargetType n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F3 RID: 12787 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitIsType(QilTargetType n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitIsEmpty(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F5 RID: 12789 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXPathNodeValue(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXPathFollowing(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXPathPreceding(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXPathNamespace(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXsltGenerateId(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXsltInvokeLateBound(QilInvokeLateBound n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXsltInvokeEarlyBound(QilInvokeEarlyBound n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXsltCopy(QilBinary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXsltCopyOf(QilUnary n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x00122757 File Offset: 0x00120957
		protected override QilNode VisitXsltConvert(QilTargetType n)
		{
			return this.NoReplace(n);
		}

		// Token: 0x04002644 RID: 9796
		private QilPatternVisitor.QilPatterns _patterns;

		// Token: 0x04002645 RID: 9797
		private int _replacementCnt;

		// Token: 0x04002646 RID: 9798
		private int _lastReplacement;

		// Token: 0x04002647 RID: 9799
		private int _threshold = int.MaxValue;

		// Token: 0x020004CA RID: 1226
		internal sealed class QilPatterns
		{
			// Token: 0x060031FF RID: 12799 RVA: 0x00122760 File Offset: 0x00120960
			private QilPatterns(QilPatternVisitor.QilPatterns toCopy)
			{
				this._bits = new BitArray(toCopy._bits);
			}

			// Token: 0x06003200 RID: 12800 RVA: 0x00122779 File Offset: 0x00120979
			public QilPatterns(int szBits, bool allSet)
			{
				this._bits = new BitArray(szBits, allSet);
			}

			// Token: 0x06003201 RID: 12801 RVA: 0x0012278E File Offset: 0x0012098E
			public QilPatternVisitor.QilPatterns Clone()
			{
				return new QilPatternVisitor.QilPatterns(this);
			}

			// Token: 0x06003202 RID: 12802 RVA: 0x00122796 File Offset: 0x00120996
			public void ClearAll()
			{
				this._bits.SetAll(false);
			}

			// Token: 0x06003203 RID: 12803 RVA: 0x001227A4 File Offset: 0x001209A4
			public void Add(int i)
			{
				this._bits.Set(i, true);
			}

			// Token: 0x06003204 RID: 12804 RVA: 0x001227B3 File Offset: 0x001209B3
			public bool IsSet(int i)
			{
				return this._bits[i];
			}

			// Token: 0x04002648 RID: 9800
			private BitArray _bits;
		}
	}
}
