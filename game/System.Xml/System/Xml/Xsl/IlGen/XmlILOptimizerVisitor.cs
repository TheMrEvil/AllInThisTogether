using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004AF RID: 1199
	internal class XmlILOptimizerVisitor : QilPatternVisitor
	{
		// Token: 0x06002ED6 RID: 11990 RVA: 0x0011186C File Offset: 0x0010FA6C
		static XmlILOptimizerVisitor()
		{
			XmlILOptimizerVisitor.PatternsNoOpt = new QilPatternVisitor.QilPatterns(141, false);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(104);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(88);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(97);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(71);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(70);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(58);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(96);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(79);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(78);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(91);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(93);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(134);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(118);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(112);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(41);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(48);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(15);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(8);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(23);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(24);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(7);
			XmlILOptimizerVisitor.PatternsNoOpt.Add(18);
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x001119A4 File Offset: 0x0010FBA4
		public XmlILOptimizerVisitor(QilExpression qil, bool optimize) : base(optimize ? XmlILOptimizerVisitor.PatternsOpt : XmlILOptimizerVisitor.PatternsNoOpt, qil.Factory)
		{
			this.qil = qil;
			this.elemAnalyzer = new XmlILElementAnalyzer(qil.Factory);
			this.contentAnalyzer = new XmlILStateAnalyzer(qil.Factory);
			this.nmspAnalyzer = new XmlILNamespaceAnalyzer();
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x00111A18 File Offset: 0x0010FC18
		public QilExpression Optimize()
		{
			QilExpression result = (QilExpression)this.Visit(this.qil);
			if (this[XmlILOptimization.TailCall])
			{
				TailCallAnalyzer.Analyze(result);
			}
			return result;
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x00111A48 File Offset: 0x0010FC48
		protected override QilNode Visit(QilNode nd)
		{
			if (nd != null && this[XmlILOptimization.EliminateNamespaceDecl])
			{
				QilNodeType nodeType = nd.NodeType;
				if (nodeType != QilNodeType.QilExpression)
				{
					if (nodeType != QilNodeType.ElementCtor)
					{
						if (nodeType == QilNodeType.DocumentCtor)
						{
							this.nmspAnalyzer.Analyze(nd, true);
						}
					}
					else if (!XmlILConstructInfo.Read(nd).IsNamespaceInScope)
					{
						this.nmspAnalyzer.Analyze(nd, false);
					}
				}
				else
				{
					this.nmspAnalyzer.Analyze(((QilExpression)nd).Root, true);
				}
			}
			return base.Visit(nd);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x00111AC4 File Offset: 0x0010FCC4
		protected override QilNode VisitReference(QilNode oldNode)
		{
			QilNode qilNode = this.subs.FindReplacement(oldNode);
			if (qilNode == null)
			{
				qilNode = oldNode;
			}
			if (this[XmlILOptimization.EliminateLiteralVariables] && qilNode != null && (qilNode.NodeType == QilNodeType.Let || qilNode.NodeType == QilNodeType.For))
			{
				QilNode binding = ((QilIterator)oldNode).Binding;
				if (this.IsLiteral(binding))
				{
					return this.Replace(XmlILOptimization.EliminateLiteralVariables, qilNode, binding.ShallowClone(this.f));
				}
			}
			if (this[XmlILOptimization.EliminateUnusedGlobals] && this.IsGlobalValue(qilNode))
			{
				OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.IsReferenced);
			}
			return base.VisitReference(qilNode);
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x00111B54 File Offset: 0x0010FD54
		protected bool AllowReplace(XmlILOptimization pattern, QilNode original)
		{
			return base.AllowReplace((int)pattern, original);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x00111B5E File Offset: 0x0010FD5E
		protected QilNode Replace(XmlILOptimization pattern, QilNode original, QilNode replacement)
		{
			return base.Replace((int)pattern, original, replacement);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x00111B6C File Offset: 0x0010FD6C
		protected override QilNode NoReplace(QilNode node)
		{
			if (node != null)
			{
				QilNodeType nodeType = node.NodeType;
				if (nodeType <= QilNodeType.Invoke)
				{
					if (nodeType - QilNodeType.Error > 1)
					{
						if (nodeType != QilNodeType.Invoke)
						{
							goto IL_66;
						}
						if (!((QilInvoke)node).Function.MaybeSideEffects)
						{
							goto IL_66;
						}
					}
				}
				else if (nodeType != QilNodeType.XsltInvokeLateBound)
				{
					if (nodeType != QilNodeType.XsltInvokeEarlyBound)
					{
						goto IL_66;
					}
					if (((QilInvokeEarlyBound)node).Name.NamespaceUri.Length == 0)
					{
						goto IL_66;
					}
				}
				IL_2C:
				OptimizerPatterns.Write(node).AddPattern(OptimizerPatternName.MaybeSideEffects);
				return node;
				IL_66:
				for (int i = 0; i < node.Count; i++)
				{
					if (node[i] != null && OptimizerPatterns.Read(node[i]).MatchesPattern(OptimizerPatternName.MaybeSideEffects))
					{
						goto IL_2C;
					}
				}
			}
			return node;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x00111C0F File Offset: 0x0010FE0F
		protected override void RecalculateType(QilNode node, XmlQueryType oldType)
		{
			if (node.NodeType != QilNodeType.Let || !this.qil.GlobalVariableList.Contains(node))
			{
				base.RecalculateType(node, oldType);
			}
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x00111C38 File Offset: 0x0010FE38
		protected override QilNode VisitQilExpression(QilExpression local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.EliminateUnusedGlobals] && this.AllowReplace(XmlILOptimization.EliminateUnusedGlobals, local0))
			{
				XmlILOptimizerVisitor.EliminateUnusedGlobals(local0.GlobalVariableList);
				XmlILOptimizerVisitor.EliminateUnusedGlobals(local0.GlobalParameterList);
				XmlILOptimizerVisitor.EliminateUnusedGlobals(local0.FunctionList);
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				foreach (QilNode qilNode2 in local0.FunctionList)
				{
					QilFunction qilFunction = (QilFunction)qilNode2;
					if (this.IsConstructedExpression(qilFunction.Definition))
					{
						qilFunction.Definition = this.contentAnalyzer.Analyze(qilFunction, qilFunction.Definition);
					}
				}
				local0.Root = this.contentAnalyzer.Analyze(null, local0.Root);
				XmlILConstructInfo.Write(local0.Root).PushToWriterLast = true;
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x00111D30 File Offset: 0x0010FF30
		protected override QilNode VisitOptimizeBarrier(QilUnary local0)
		{
			QilNode ndSrc = local0[0];
			if (this[XmlILOptimization.AnnotateBarrier] && this.AllowReplace(XmlILOptimization.AnnotateBarrier, local0))
			{
				OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x00111D70 File Offset: 0x0010FF70
		protected override QilNode VisitDataSource(QilDataSource local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x00111E10 File Offset: 0x00110010
		protected override QilNode VisitNop(QilUnary local0)
		{
			QilNode replacement = local0[0];
			if (this[XmlILOptimization.EliminateNop] && this.AllowReplace(XmlILOptimization.EliminateNop, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNop, local0, replacement);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x00111E4C File Offset: 0x0011004C
		protected override QilNode VisitError(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x00111EA8 File Offset: 0x001100A8
		protected override QilNode VisitWarning(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x00111F04 File Offset: 0x00110104
		protected override QilNode VisitLet(QilIterator local0)
		{
			QilNode ndSrc = local0[0];
			if (local0.XmlType.IsSingleton && !this.IsGlobalVariable(local0) && this[XmlILOptimization.NormalizeSingletonLet] && this.AllowReplace(XmlILOptimization.NormalizeSingletonLet, local0))
			{
				local0.NodeType = QilNodeType.For;
				this.VisitFor(local0);
			}
			if (this[XmlILOptimization.AnnotateLet] && this.AllowReplace(XmlILOptimization.AnnotateLet, local0))
			{
				OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.Step);
				OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x00111F90 File Offset: 0x00110190
		protected override QilNode VisitPositionOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.EliminatePositionOf] && qilNode.NodeType != QilNodeType.For && this.AllowReplace(XmlILOptimization.EliminatePositionOf, local0))
			{
				return this.Replace(XmlILOptimization.EliminatePositionOf, local0, this.VisitLiteralInt32(this.f.LiteralInt32(1)));
			}
			if (this[XmlILOptimization.EliminatePositionOf] && qilNode.NodeType == QilNodeType.For && qilNode[0].XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.EliminatePositionOf, local0))
			{
				return this.Replace(XmlILOptimization.EliminatePositionOf, local0, this.VisitLiteralInt32(this.f.LiteralInt32(1)));
			}
			if (this[XmlILOptimization.AnnotatePositionalIterator] && this.AllowReplace(XmlILOptimization.AnnotatePositionalIterator, local0))
			{
				OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.IsPositional);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x00112058 File Offset: 0x00110258
		protected override QilNode VisitAnd(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateAnd] && qilNode.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateAnd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAnd, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateAnd] && qilNode.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateAnd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAnd, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateAnd] && qilNode2.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateAnd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAnd, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateAnd] && qilNode2.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateAnd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAnd, local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x001121A0 File Offset: 0x001103A0
		protected override QilNode VisitOr(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateOr] && qilNode.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateOr, local0))
			{
				return this.Replace(XmlILOptimization.EliminateOr, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateOr] && qilNode.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateOr, local0))
			{
				return this.Replace(XmlILOptimization.EliminateOr, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateOr] && qilNode2.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateOr, local0))
			{
				return this.Replace(XmlILOptimization.EliminateOr, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateOr] && qilNode2.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateOr, local0))
			{
				return this.Replace(XmlILOptimization.EliminateOr, local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x001122E8 File Offset: 0x001104E8
		protected override QilNode VisitNot(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateNot] && qilNode.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateNot, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNot, local0, this.VisitFalse(this.f.False()));
			}
			if (this[XmlILOptimization.EliminateNot] && qilNode.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateNot, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNot, local0, this.VisitTrue(this.f.True()));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x001123B8 File Offset: 0x001105B8
		protected override QilNode VisitConditional(QilTernary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			QilNode qilNode3 = local0[2];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateConditional] && qilNode.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateConditional, local0))
			{
				return this.Replace(XmlILOptimization.EliminateConditional, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateConditional] && qilNode.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateConditional, local0))
			{
				return this.Replace(XmlILOptimization.EliminateConditional, local0, qilNode3);
			}
			if (this[XmlILOptimization.EliminateConditional] && qilNode2.NodeType == QilNodeType.True && qilNode3.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateConditional, local0))
			{
				return this.Replace(XmlILOptimization.EliminateConditional, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateConditional] && qilNode2.NodeType == QilNodeType.False && qilNode3.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateConditional, local0))
			{
				return this.Replace(XmlILOptimization.EliminateConditional, local0, this.VisitNot(this.f.Not(qilNode)));
			}
			if (this[XmlILOptimization.FoldConditionalNot] && qilNode.NodeType == QilNodeType.Not)
			{
				QilNode left = qilNode[0];
				if (this.AllowReplace(XmlILOptimization.FoldConditionalNot, local0))
				{
					return this.Replace(XmlILOptimization.FoldConditionalNot, local0, this.VisitConditional(this.f.Conditional(left, qilNode3, qilNode2)));
				}
			}
			if (this[XmlILOptimization.NormalizeConditionalText] && qilNode2.NodeType == QilNodeType.TextCtor)
			{
				QilNode center = qilNode2[0];
				if (qilNode3.NodeType == QilNodeType.TextCtor)
				{
					QilNode right = qilNode3[0];
					if (this.AllowReplace(XmlILOptimization.NormalizeConditionalText, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeConditionalText, local0, this.VisitTextCtor(this.f.TextCtor(this.VisitConditional(this.f.Conditional(qilNode, center, right)))));
					}
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x001125A0 File Offset: 0x001107A0
		protected override QilNode VisitChoice(QilChoice local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x001125DC File Offset: 0x001107DC
		protected override QilNode VisitLength(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateLength] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateLength, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLength, local0, this.VisitLiteralInt32(this.f.LiteralInt32(0)));
			}
			if (this[XmlILOptimization.EliminateLength] && qilNode.XmlType.IsSingleton && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && this.AllowReplace(XmlILOptimization.EliminateLength, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLength, local0, this.VisitLiteralInt32(this.f.LiteralInt32(1)));
			}
			if (this[XmlILOptimization.IntroducePrecedingDod] && !this.IsDocOrderDistinct(qilNode) && (this.IsStepPattern(qilNode, QilNodeType.XPathPreceding) || this.IsStepPattern(qilNode, QilNodeType.PrecedingSibling)) && this.AllowReplace(XmlILOptimization.IntroducePrecedingDod, local0))
			{
				return this.Replace(XmlILOptimization.IntroducePrecedingDod, local0, this.VisitLength(this.f.Length(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)))));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x00112728 File Offset: 0x00110928
		protected override QilNode VisitSequence(QilList local0)
		{
			if (local0.Count == 1 && this[XmlILOptimization.EliminateSequence] && this.AllowReplace(XmlILOptimization.EliminateSequence, local0))
			{
				return this.Replace(XmlILOptimization.EliminateSequence, local0, local0[0]);
			}
			if (this.HasNestedSequence(local0) && this[XmlILOptimization.NormalizeNestedSequences] && this.AllowReplace(XmlILOptimization.NormalizeNestedSequences, local0))
			{
				QilNode qilNode = this.VisitSequence(this.f.Sequence());
				foreach (QilNode qilNode2 in local0)
				{
					if (qilNode2.NodeType == QilNodeType.Sequence)
					{
						qilNode.Add(qilNode2);
					}
					else
					{
						qilNode.Add(qilNode2);
					}
				}
				qilNode = this.VisitSequence((QilList)qilNode);
				return this.Replace(XmlILOptimization.NormalizeNestedSequences, local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x00112810 File Offset: 0x00110A10
		protected override QilNode VisitUnion(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateUnion] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateUnion, local0))
			{
				return this.Replace(XmlILOptimization.EliminateUnion, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)));
			}
			if (this[XmlILOptimization.EliminateUnion] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateUnion, local0))
			{
				return this.Replace(XmlILOptimization.EliminateUnion, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateUnion] && qilNode2.NodeType == QilNodeType.Sequence && qilNode2.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateUnion, local0))
			{
				return this.Replace(XmlILOptimization.EliminateUnion, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)));
			}
			if (this[XmlILOptimization.EliminateUnion] && qilNode.NodeType == QilNodeType.XmlContext && qilNode2.NodeType == QilNodeType.XmlContext && this.AllowReplace(XmlILOptimization.EliminateUnion, local0))
			{
				return this.Replace(XmlILOptimization.EliminateUnion, local0, qilNode);
			}
			if (this[XmlILOptimization.NormalizeUnion] && (!this.IsDocOrderDistinct(qilNode) || !this.IsDocOrderDistinct(qilNode2)) && this.AllowReplace(XmlILOptimization.NormalizeUnion, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeUnion, local0, this.VisitUnion(this.f.Union(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)), this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode2)))));
			}
			if (this[XmlILOptimization.AnnotateUnion] && this.AllowReplace(XmlILOptimization.AnnotateUnion, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			if (this[XmlILOptimization.AnnotateUnionContent] && (this.IsStepPattern(qilNode, QilNodeType.Content) || this.IsStepPattern(qilNode, QilNodeType.Union)) && (this.IsStepPattern(qilNode2, QilNodeType.Content) || this.IsStepPattern(qilNode2, QilNodeType.Union)) && OptimizerPatterns.Read(qilNode).GetArgument(OptimizerPatternArgument.StepInput) == OptimizerPatterns.Read(qilNode2).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateUnionContent, local0))
			{
				this.AddStepPattern(local0, (QilNode)OptimizerPatterns.Read(qilNode).GetArgument(OptimizerPatternArgument.StepInput));
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x00112AB0 File Offset: 0x00110CB0
		protected override QilNode VisitIntersection(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateIntersection] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateIntersection, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIntersection, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)));
			}
			if (this[XmlILOptimization.EliminateIntersection] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateIntersection, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIntersection, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateIntersection] && qilNode2.NodeType == QilNodeType.Sequence && qilNode2.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateIntersection, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIntersection, local0, qilNode2);
			}
			if (this[XmlILOptimization.EliminateIntersection] && qilNode.NodeType == QilNodeType.XmlContext && qilNode2.NodeType == QilNodeType.XmlContext && this.AllowReplace(XmlILOptimization.EliminateIntersection, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIntersection, local0, qilNode);
			}
			if (this[XmlILOptimization.NormalizeIntersect] && (!this.IsDocOrderDistinct(qilNode) || !this.IsDocOrderDistinct(qilNode2)) && this.AllowReplace(XmlILOptimization.NormalizeIntersect, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeIntersect, local0, this.VisitIntersection(this.f.Intersection(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)), this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode2)))));
			}
			if (this[XmlILOptimization.AnnotateIntersect] && this.AllowReplace(XmlILOptimization.AnnotateIntersect, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x00112CA4 File Offset: 0x00110EA4
		protected override QilNode VisitDifference(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateDifference] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateDifference, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDifference, local0, qilNode);
			}
			if (this[XmlILOptimization.EliminateDifference] && qilNode2.NodeType == QilNodeType.Sequence && qilNode2.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateDifference, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDifference, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)));
			}
			if (this[XmlILOptimization.EliminateDifference] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateDifference, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDifference, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.EliminateDifference] && qilNode.NodeType == QilNodeType.XmlContext && qilNode2.NodeType == QilNodeType.XmlContext && this.AllowReplace(XmlILOptimization.EliminateDifference, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDifference, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.NormalizeDifference] && (!this.IsDocOrderDistinct(qilNode) || !this.IsDocOrderDistinct(qilNode2)) && this.AllowReplace(XmlILOptimization.NormalizeDifference, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeDifference, local0, this.VisitDifference(this.f.Difference(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode)), this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode2)))));
			}
			if (this[XmlILOptimization.AnnotateDifference] && this.AllowReplace(XmlILOptimization.AnnotateDifference, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x00112EB8 File Offset: 0x001110B8
		protected override QilNode VisitAverage(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateAverage] && qilNode.XmlType.Cardinality == XmlQueryCardinality.Zero && this.AllowReplace(XmlILOptimization.EliminateAverage, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAverage, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x00112F5C File Offset: 0x0011115C
		protected override QilNode VisitSum(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateSum] && qilNode.XmlType.Cardinality == XmlQueryCardinality.Zero && this.AllowReplace(XmlILOptimization.EliminateSum, local0))
			{
				return this.Replace(XmlILOptimization.EliminateSum, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x00113000 File Offset: 0x00111200
		protected override QilNode VisitMinimum(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateMinimum] && qilNode.XmlType.Cardinality == XmlQueryCardinality.Zero && this.AllowReplace(XmlILOptimization.EliminateMinimum, local0))
			{
				return this.Replace(XmlILOptimization.EliminateMinimum, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x001130A4 File Offset: 0x001112A4
		protected override QilNode VisitMaximum(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateMaximum] && qilNode.XmlType.Cardinality == XmlQueryCardinality.Zero && this.AllowReplace(XmlILOptimization.EliminateMaximum, local0))
			{
				return this.Replace(XmlILOptimization.EliminateMaximum, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x00113148 File Offset: 0x00111348
		protected override QilNode VisitNegate(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateNegate] && qilNode.NodeType == QilNodeType.LiteralDecimal)
			{
				decimal d = (decimal)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateNegate, local0))
				{
					return this.Replace(XmlILOptimization.EliminateNegate, local0, this.VisitLiteralDecimal(this.f.LiteralDecimal(-d)));
				}
			}
			if (this[XmlILOptimization.EliminateNegate] && qilNode.NodeType == QilNodeType.LiteralDouble)
			{
				double num = (double)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateNegate, local0))
				{
					return this.Replace(XmlILOptimization.EliminateNegate, local0, this.VisitLiteralDouble(this.f.LiteralDouble(-num)));
				}
			}
			if (this[XmlILOptimization.EliminateNegate] && qilNode.NodeType == QilNodeType.LiteralInt32)
			{
				int num2 = (int)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateNegate, local0))
				{
					return this.Replace(XmlILOptimization.EliminateNegate, local0, this.VisitLiteralInt32(this.f.LiteralInt32(-num2)));
				}
			}
			if (this[XmlILOptimization.EliminateNegate] && qilNode.NodeType == QilNodeType.LiteralInt64)
			{
				long num3 = (long)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateNegate, local0))
				{
					return this.Replace(XmlILOptimization.EliminateNegate, local0, this.VisitLiteralInt64(this.f.LiteralInt64(-num3)));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x001132DC File Offset: 0x001114DC
		protected override QilNode VisitAdd(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateAdd] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Add, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateAdd, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAdd, local0, this.FoldArithmetic(QilNodeType.Add, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			if (this[XmlILOptimization.NormalizeAddLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeAddLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeAddLiteral, local0, this.VisitAdd(this.f.Add(qilNode2, qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0011341C File Offset: 0x0011161C
		protected override QilNode VisitSubtract(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateSubtract] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Subtract, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateSubtract, local0))
			{
				return this.Replace(XmlILOptimization.EliminateSubtract, local0, this.FoldArithmetic(QilNodeType.Subtract, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x00113518 File Offset: 0x00111718
		protected override QilNode VisitMultiply(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateMultiply] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Multiply, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateMultiply, local0))
			{
				return this.Replace(XmlILOptimization.EliminateMultiply, local0, this.FoldArithmetic(QilNodeType.Multiply, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			if (this[XmlILOptimization.NormalizeMultiplyLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeMultiplyLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeMultiplyLiteral, local0, this.VisitMultiply(this.f.Multiply(qilNode2, qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x00113660 File Offset: 0x00111860
		protected override QilNode VisitDivide(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateDivide] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Divide, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateDivide, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDivide, local0, this.FoldArithmetic(QilNodeType.Divide, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0011375C File Offset: 0x0011195C
		protected override QilNode VisitModulo(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateModulo] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Modulo, (QilLiteral)qilNode, (QilLiteral)qilNode2) && this.AllowReplace(XmlILOptimization.EliminateModulo, local0))
			{
				return this.Replace(XmlILOptimization.EliminateModulo, local0, this.FoldArithmetic(QilNodeType.Modulo, (QilLiteral)qilNode, (QilLiteral)qilNode2));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x00113858 File Offset: 0x00111A58
		protected override QilNode VisitStrLength(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateStrLength] && qilNode.NodeType == QilNodeType.LiteralString)
			{
				string text = (string)((QilLiteral)qilNode).Value;
				if (this.AllowReplace(XmlILOptimization.EliminateStrLength, local0))
				{
					return this.Replace(XmlILOptimization.EliminateStrLength, local0, this.VisitLiteralInt32(this.f.LiteralInt32(text.Length)));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x00113904 File Offset: 0x00111B04
		protected override QilNode VisitStrConcat(QilStrConcat local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (qilNode2.XmlType.IsSingleton && this[XmlILOptimization.EliminateStrConcatSingle] && this.AllowReplace(XmlILOptimization.EliminateStrConcatSingle, local0))
			{
				return this.Replace(XmlILOptimization.EliminateStrConcatSingle, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateStrConcat] && qilNode.NodeType == QilNodeType.LiteralString)
			{
				string delimiter = (string)((QilLiteral)qilNode).Value;
				if (qilNode2.NodeType == QilNodeType.Sequence && this.AreLiteralArgs(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateStrConcat, local0))
				{
					StringConcat stringConcat = default(StringConcat);
					stringConcat.Delimiter = delimiter;
					foreach (QilNode qilNode3 in qilNode2)
					{
						QilLiteral literal = (QilLiteral)qilNode3;
						stringConcat.Concat(literal);
					}
					return this.Replace(XmlILOptimization.EliminateStrConcat, local0, this.VisitLiteralString(this.f.LiteralString(stringConcat.GetResult())));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x00113AB0 File Offset: 0x00111CB0
		protected override QilNode VisitStrParseQName(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x00113B50 File Offset: 0x00111D50
		protected override QilNode VisitNe(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateNe] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateNe, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNe, local0, this.FoldComparison(QilNodeType.Ne, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeNeLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeNeLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeNeLiteral, local0, this.VisitNe(this.f.Ne(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertNe] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType typ = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(typ) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertNe, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertNe, local0, this.VisitNe(this.f.Ne(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeIdNe] && qilNode.NodeType == QilNodeType.XsltGenerateId)
			{
				QilNode qilNode5 = qilNode[0];
				if (qilNode5.XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.XsltGenerateId)
				{
					QilNode qilNode6 = qilNode2[0];
					if (qilNode6.XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.NormalizeIdNe, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeIdNe, local0, this.VisitNot(this.f.Not(this.VisitIs(this.f.Is(qilNode5, qilNode6)))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeLengthNe] && qilNode.NodeType == QilNodeType.Length)
			{
				QilNode child = qilNode[0];
				if (qilNode2.NodeType == QilNodeType.LiteralInt32 && (int)((QilLiteral)qilNode2).Value == 0 && this.AllowReplace(XmlILOptimization.NormalizeLengthNe, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLengthNe, local0, this.VisitNot(this.f.Not(this.VisitIsEmpty(this.f.IsEmpty(child)))));
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthNe] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthNe, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x00113E80 File Offset: 0x00112080
		protected override QilNode VisitEq(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateEq] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateEq, local0))
			{
				return this.Replace(XmlILOptimization.EliminateEq, local0, this.FoldComparison(QilNodeType.Eq, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeEqLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeEqLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeEqLiteral, local0, this.VisitEq(this.f.Eq(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertEq] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType typ = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(typ) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertEq, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertEq, local0, this.VisitEq(this.f.Eq(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeAddEq] && qilNode.NodeType == QilNodeType.Add)
			{
				QilNode left = qilNode[0];
				QilNode qilNode5 = qilNode[1];
				if (this.IsLiteral(qilNode5) && this.IsLiteral(qilNode2) && this.CanFoldArithmetic(QilNodeType.Subtract, (QilLiteral)qilNode2, (QilLiteral)qilNode5) && this.AllowReplace(XmlILOptimization.NormalizeAddEq, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeAddEq, local0, this.VisitEq(this.f.Eq(left, this.FoldArithmetic(QilNodeType.Subtract, (QilLiteral)qilNode2, (QilLiteral)qilNode5))));
				}
			}
			if (this[XmlILOptimization.NormalizeIdEq] && qilNode.NodeType == QilNodeType.XsltGenerateId)
			{
				QilNode qilNode6 = qilNode[0];
				if (qilNode6.XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.XsltGenerateId)
				{
					QilNode qilNode7 = qilNode2[0];
					if (qilNode7.XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.NormalizeIdEq, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeIdEq, local0, this.VisitIs(this.f.Is(qilNode6, qilNode7)));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeIdEq] && qilNode.NodeType == QilNodeType.XsltGenerateId)
			{
				QilNode qilNode8 = qilNode[0];
				if (qilNode8.XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.StrConcat)
				{
					QilNode qilNode9 = qilNode2[1];
					if (qilNode9.NodeType == QilNodeType.Loop)
					{
						QilNode qilNode10 = qilNode9[0];
						QilNode qilNode11 = qilNode9[1];
						if (qilNode10.NodeType == QilNodeType.For)
						{
							QilNode qilNode12 = qilNode10[0];
							if (!qilNode12.XmlType.MaybeMany && qilNode11.NodeType == QilNodeType.XsltGenerateId && qilNode11[0] == qilNode10 && this.AllowReplace(XmlILOptimization.NormalizeIdEq, local0))
							{
								QilNode qilNode13 = this.VisitFor(this.f.For(qilNode12));
								return this.Replace(XmlILOptimization.NormalizeIdEq, local0, this.VisitNot(this.f.Not(this.VisitIsEmpty(this.f.IsEmpty(this.VisitFilter(this.f.Filter(qilNode13, this.VisitIs(this.f.Is(qilNode8, qilNode13)))))))));
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.NormalizeIdEq] && qilNode.NodeType == QilNodeType.StrConcat)
			{
				QilNode qilNode14 = qilNode[1];
				if (qilNode14.NodeType == QilNodeType.Loop)
				{
					QilNode qilNode15 = qilNode14[0];
					QilNode qilNode16 = qilNode14[1];
					if (qilNode15.NodeType == QilNodeType.For)
					{
						QilNode qilNode17 = qilNode15[0];
						if (!qilNode17.XmlType.MaybeMany && qilNode16.NodeType == QilNodeType.XsltGenerateId && qilNode16[0] == qilNode15 && qilNode2.NodeType == QilNodeType.XsltGenerateId)
						{
							QilNode qilNode18 = qilNode2[0];
							if (qilNode18.XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.NormalizeIdEq, local0))
							{
								QilNode qilNode19 = this.VisitFor(this.f.For(qilNode17));
								return this.Replace(XmlILOptimization.NormalizeIdEq, local0, this.VisitNot(this.f.Not(this.VisitIsEmpty(this.f.IsEmpty(this.VisitFilter(this.f.Filter(qilNode19, this.VisitIs(this.f.Is(qilNode18, qilNode19)))))))));
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.NormalizeMuenchian] && qilNode.NodeType == QilNodeType.Length)
			{
				QilNode qilNode20 = qilNode[0];
				if (qilNode20.NodeType == QilNodeType.Union)
				{
					QilNode qilNode21 = qilNode20[0];
					QilNode qilNode22 = qilNode20[1];
					if (qilNode21.XmlType.IsSingleton && !qilNode22.XmlType.MaybeMany && qilNode2.NodeType == QilNodeType.LiteralInt32 && (int)((QilLiteral)qilNode2).Value == 1 && this.AllowReplace(XmlILOptimization.NormalizeMuenchian, local0))
					{
						QilNode qilNode23 = this.VisitFor(this.f.For(qilNode22));
						return this.Replace(XmlILOptimization.NormalizeMuenchian, local0, this.VisitIsEmpty(this.f.IsEmpty(this.VisitFilter(this.f.Filter(qilNode23, this.VisitNot(this.f.Not(this.VisitIs(this.f.Is(qilNode21, qilNode23)))))))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeMuenchian] && qilNode.NodeType == QilNodeType.Length)
			{
				QilNode qilNode24 = qilNode[0];
				if (qilNode24.NodeType == QilNodeType.Union)
				{
					QilNode qilNode25 = qilNode24[0];
					QilNode qilNode26 = qilNode24[1];
					if (!qilNode25.XmlType.MaybeMany && qilNode26.XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.LiteralInt32 && (int)((QilLiteral)qilNode2).Value == 1 && this.AllowReplace(XmlILOptimization.NormalizeMuenchian, local0))
					{
						QilNode qilNode27 = this.VisitFor(this.f.For(qilNode25));
						return this.Replace(XmlILOptimization.NormalizeMuenchian, local0, this.VisitIsEmpty(this.f.IsEmpty(this.VisitFilter(this.f.Filter(qilNode27, this.VisitNot(this.f.Not(this.VisitIs(this.f.Is(qilNode27, qilNode26)))))))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthEq] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthEq, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x00114604 File Offset: 0x00112804
		protected override QilNode VisitGt(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateGt] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateGt, local0))
			{
				return this.Replace(XmlILOptimization.EliminateGt, local0, this.FoldComparison(QilNodeType.Gt, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeGtLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeGtLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeGtLiteral, local0, this.VisitLt(this.f.Lt(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertGt] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType typ = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(typ) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertGt, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertGt, local0, this.VisitGt(this.f.Gt(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeLengthGt] && qilNode.NodeType == QilNodeType.Length)
			{
				QilNode child = qilNode[0];
				if (qilNode2.NodeType == QilNodeType.LiteralInt32 && (int)((QilLiteral)qilNode2).Value == 0 && this.AllowReplace(XmlILOptimization.NormalizeLengthGt, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLengthGt, local0, this.VisitNot(this.f.Not(this.VisitIsEmpty(this.f.IsEmpty(child)))));
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthGt] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthGt, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x001148A4 File Offset: 0x00112AA4
		protected override QilNode VisitGe(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateGe] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateGe, local0))
			{
				return this.Replace(XmlILOptimization.EliminateGe, local0, this.FoldComparison(QilNodeType.Ge, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeGeLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeGeLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeGeLiteral, local0, this.VisitLe(this.f.Le(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertGe] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType typ = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(typ) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertGe, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertGe, local0, this.VisitGe(this.f.Ge(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthGe] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthGe, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x00114AD4 File Offset: 0x00112CD4
		protected override QilNode VisitLt(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateLt] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateLt, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLt, local0, this.FoldComparison(QilNodeType.Lt, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeLtLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeLtLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeLtLiteral, local0, this.VisitGt(this.f.Gt(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertLt] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType typ = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(typ) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertLt, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertLt, local0, this.VisitLt(this.f.Lt(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthLt] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthLt, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00114D04 File Offset: 0x00112F04
		protected override QilNode VisitLe(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateLe] && this.IsLiteral(qilNode) && this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.EliminateLe, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLe, local0, this.FoldComparison(QilNodeType.Le, qilNode, qilNode2));
			}
			if (this[XmlILOptimization.NormalizeLeLiteral] && this.IsLiteral(qilNode) && !this.IsLiteral(qilNode2) && this.AllowReplace(XmlILOptimization.NormalizeLeLiteral, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeLeLiteral, local0, this.VisitGe(this.f.Ge(qilNode2, qilNode)));
			}
			if (this[XmlILOptimization.NormalizeXsltConvertLe] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode3 = qilNode[0];
				QilNode qilNode4 = qilNode[1];
				if (qilNode4.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType typ = (XmlQueryType)((QilLiteral)qilNode4).Value;
					if (this.IsPrimitiveNumeric(qilNode3.XmlType) && this.IsPrimitiveNumeric(typ) && this.IsLiteral(qilNode2) && this.CanFoldXsltConvertNonLossy(qilNode2, qilNode3.XmlType) && this.AllowReplace(XmlILOptimization.NormalizeXsltConvertLe, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeXsltConvertLe, local0, this.VisitLe(this.f.Le(qilNode3, this.FoldXsltConvert(qilNode2, qilNode3.XmlType))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxLengthLe] && qilNode.NodeType == QilNodeType.Length && qilNode2.NodeType == QilNodeType.LiteralInt32)
			{
				int num = (int)((QilLiteral)qilNode2).Value;
				if (this.AllowReplace(XmlILOptimization.AnnotateMaxLengthLe, local0))
				{
					OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
					OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x00114F34 File Offset: 0x00113134
		protected override QilNode VisitIs(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateIs] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateIs, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIs, local0, this.VisitTrue(this.f.True()));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x00115008 File Offset: 0x00113208
		protected override QilNode VisitAfter(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateAfter] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateAfter, local0))
			{
				return this.Replace(XmlILOptimization.EliminateAfter, local0, this.VisitFalse(this.f.False()));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x001150DC File Offset: 0x001132DC
		protected override QilNode VisitBefore(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.EliminateBefore] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateBefore, local0))
			{
				return this.Replace(XmlILOptimization.EliminateBefore, local0, this.VisitFalse(this.f.False()));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x001151B0 File Offset: 0x001133B0
		protected override QilNode VisitLoop(QilLoop local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode[0])));
			}
			if (this[XmlILOptimization.EliminateIterator] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode3 = qilNode[0];
				if (qilNode3.NodeType == QilNodeType.For && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.IsPositional) && this.AllowReplace(XmlILOptimization.EliminateIterator, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIterator, local0, this.Subs(qilNode2, qilNode, qilNode3));
				}
			}
			if (this[XmlILOptimization.EliminateLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode4 = qilNode[0];
				if (qilNode4.NodeType == QilNodeType.Sequence && qilNode4.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateLoop, local0))
				{
					return this.Replace(XmlILOptimization.EliminateLoop, local0, this.VisitSequence(this.f.Sequence()));
				}
			}
			if (this[XmlILOptimization.EliminateLoop] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.Sequence && qilNode2.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateLoop, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLoop, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.EliminateLoop] && qilNode2 == qilNode && this.AllowReplace(XmlILOptimization.EliminateLoop, local0))
			{
				return this.Replace(XmlILOptimization.EliminateLoop, local0, qilNode[0]);
			}
			if (this[XmlILOptimization.NormalizeLoopText] && qilNode.NodeType == QilNodeType.For && qilNode[0].XmlType.IsSingleton && qilNode2.NodeType == QilNodeType.TextCtor)
			{
				QilNode body = qilNode2[0];
				if (this.AllowReplace(XmlILOptimization.NormalizeLoopText, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLoopText, local0, this.VisitTextCtor(this.f.TextCtor(this.VisitLoop(this.f.Loop(qilNode, body)))));
				}
			}
			if (this[XmlILOptimization.EliminateIteratorUsedAtMostOnce] && (qilNode.NodeType == QilNodeType.Let || qilNode[0].XmlType.IsSingleton) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && this.nodeCounter.Count(qilNode2, qilNode) <= 1 && this.AllowReplace(XmlILOptimization.EliminateIteratorUsedAtMostOnce, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIteratorUsedAtMostOnce, local0, this.Subs(qilNode2, qilNode, qilNode[0]));
			}
			if (this[XmlILOptimization.NormalizeLoopConditional] && qilNode2.NodeType == QilNodeType.Conditional)
			{
				QilNode child = qilNode2[0];
				QilNode qilNode5 = qilNode2[1];
				QilNode qilNode6 = qilNode2[2];
				if (qilNode5.NodeType == QilNodeType.Sequence && qilNode5.Count == 0 && qilNode6 == qilNode && this.AllowReplace(XmlILOptimization.NormalizeLoopConditional, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLoopConditional, local0, this.VisitFilter(this.f.Filter(qilNode, this.VisitNot(this.f.Not(child)))));
				}
			}
			if (this[XmlILOptimization.NormalizeLoopConditional] && qilNode2.NodeType == QilNodeType.Conditional)
			{
				QilNode body2 = qilNode2[0];
				QilNode qilNode7 = qilNode2[1];
				QilNode qilNode8 = qilNode2[2];
				if (qilNode7 == qilNode && qilNode8.NodeType == QilNodeType.Sequence && qilNode8.Count == 0 && this.AllowReplace(XmlILOptimization.NormalizeLoopConditional, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeLoopConditional, local0, this.VisitFilter(this.f.Filter(qilNode, body2)));
				}
			}
			if (this[XmlILOptimization.NormalizeLoopConditional] && qilNode.NodeType == QilNodeType.For && qilNode2.NodeType == QilNodeType.Conditional)
			{
				QilNode child2 = qilNode2[0];
				QilNode qilNode9 = qilNode2[1];
				QilNode expr = qilNode2[2];
				if (qilNode9.NodeType == QilNodeType.Sequence && qilNode9.Count == 0 && this.NonPositional(expr, qilNode) && this.AllowReplace(XmlILOptimization.NormalizeLoopConditional, local0))
				{
					QilNode qilNode10 = this.VisitFor(this.f.For(this.VisitFilter(this.f.Filter(qilNode, this.VisitNot(this.f.Not(child2))))));
					return this.Replace(XmlILOptimization.NormalizeLoopConditional, local0, this.VisitLoop(this.f.Loop(qilNode10, this.Subs(expr, qilNode, qilNode10))));
				}
			}
			if (this[XmlILOptimization.NormalizeLoopConditional] && qilNode.NodeType == QilNodeType.For && qilNode2.NodeType == QilNodeType.Conditional)
			{
				QilNode body3 = qilNode2[0];
				QilNode expr2 = qilNode2[1];
				QilNode qilNode11 = qilNode2[2];
				if (this.NonPositional(expr2, qilNode) && qilNode11.NodeType == QilNodeType.Sequence && qilNode11.Count == 0 && this.AllowReplace(XmlILOptimization.NormalizeLoopConditional, local0))
				{
					QilNode qilNode12 = this.VisitFor(this.f.For(this.VisitFilter(this.f.Filter(qilNode, body3))));
					return this.Replace(XmlILOptimization.NormalizeLoopConditional, local0, this.VisitLoop(this.f.Loop(qilNode12, this.Subs(expr2, qilNode, qilNode12))));
				}
			}
			if (this[XmlILOptimization.NormalizeLoopLoop] && qilNode2.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode13 = qilNode2[0];
				QilNode expr3 = qilNode2[1];
				if (qilNode13.NodeType == QilNodeType.For)
				{
					QilNode body4 = qilNode13[0];
					if (!this.DependsOn(expr3, qilNode) && this.NonPositional(expr3, qilNode13) && this.AllowReplace(XmlILOptimization.NormalizeLoopLoop, local0))
					{
						QilNode qilNode14 = this.VisitFor(this.f.For(this.VisitLoop(this.f.Loop(qilNode, body4))));
						return this.Replace(XmlILOptimization.NormalizeLoopLoop, local0, this.VisitLoop(this.f.Loop(qilNode14, this.Subs(expr3, qilNode13, qilNode14))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateSingletonLoop] && qilNode.NodeType == QilNodeType.For && !qilNode[0].XmlType.MaybeMany && this.AllowReplace(XmlILOptimization.AnnotateSingletonLoop, local0))
			{
				OptimizerPatterns.Inherit(qilNode2, local0, OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Inherit(qilNode2, local0, OptimizerPatternName.SameDepth);
			}
			if (this[XmlILOptimization.AnnotateRootLoop] && this.IsStepPattern(qilNode2, QilNodeType.Root) && this.AllowReplace(XmlILOptimization.AnnotateRootLoop, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			if (this[XmlILOptimization.AnnotateContentLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode15 = qilNode[0];
				if (OptimizerPatterns.Read(qilNode15).MatchesPattern(OptimizerPatternName.SameDepth) && (this.IsStepPattern(qilNode2, QilNodeType.Content) || this.IsStepPattern(qilNode2, QilNodeType.Union)) && qilNode == OptimizerPatterns.Read(qilNode2).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateContentLoop, local0))
				{
					OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
					OptimizerPatterns.Inherit(qilNode15, local0, OptimizerPatternName.IsDocOrderDistinct);
				}
			}
			if (this[XmlILOptimization.AnnotateAttrNmspLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode ndSrc = qilNode[0];
				if ((this.IsStepPattern(qilNode2, QilNodeType.Attribute) || this.IsStepPattern(qilNode2, QilNodeType.XPathNamespace) || OptimizerPatterns.Read(qilNode2).MatchesPattern(OptimizerPatternName.FilterAttributeKind)) && qilNode == OptimizerPatterns.Read(qilNode2).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateAttrNmspLoop, local0))
				{
					OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.SameDepth);
					OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.IsDocOrderDistinct);
				}
			}
			if (this[XmlILOptimization.AnnotateDescendantLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode16 = qilNode[0];
				if (OptimizerPatterns.Read(qilNode16).MatchesPattern(OptimizerPatternName.SameDepth) && (this.IsStepPattern(qilNode2, QilNodeType.Descendant) || this.IsStepPattern(qilNode2, QilNodeType.DescendantOrSelf)) && qilNode == OptimizerPatterns.Read(qilNode2).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateDescendantLoop, local0))
				{
					OptimizerPatterns.Inherit(qilNode16, local0, OptimizerPatternName.IsDocOrderDistinct);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x001158F8 File Offset: 0x00113AF8
		protected override QilNode VisitFilter(QilLoop local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode[0])));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitLoop(this.f.Loop(qilNode, qilNode2)));
			}
			if (this[XmlILOptimization.EliminateFilter] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.False && this.AllowReplace(XmlILOptimization.EliminateFilter, local0))
			{
				return this.Replace(XmlILOptimization.EliminateFilter, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.EliminateFilter] && qilNode2.NodeType == QilNodeType.True && this.AllowReplace(XmlILOptimization.EliminateFilter, local0))
			{
				return this.Replace(XmlILOptimization.EliminateFilter, local0, qilNode[0]);
			}
			if (this[XmlILOptimization.NormalizeAttribute] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode3 = qilNode[0];
				if (qilNode3.NodeType == QilNodeType.Content)
				{
					QilNode left = qilNode3[0];
					if (qilNode2.NodeType == QilNodeType.And)
					{
						QilNode qilNode4 = qilNode2[0];
						QilNode qilNode5 = qilNode2[1];
						if (qilNode4.NodeType == QilNodeType.IsType)
						{
							QilNode qilNode6 = qilNode4[0];
							QilNode qilNode7 = qilNode4[1];
							if (qilNode6 == qilNode && qilNode7.NodeType == QilNodeType.LiteralType && (XmlQueryType)((QilLiteral)qilNode7).Value == XmlQueryTypeFactory.Attribute && qilNode5.NodeType == QilNodeType.Eq)
							{
								QilNode qilNode8 = qilNode5[0];
								QilNode qilNode9 = qilNode5[1];
								if (qilNode8.NodeType == QilNodeType.NameOf && qilNode8[0] == qilNode && qilNode9.NodeType == QilNodeType.LiteralQName && this.AllowReplace(XmlILOptimization.NormalizeAttribute, local0))
								{
									return this.Replace(XmlILOptimization.NormalizeAttribute, local0, this.VisitAttribute(this.f.Attribute(left, qilNode9)));
								}
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.CommuteFilterLoop] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode10 = qilNode[0];
				if (qilNode10.NodeType == QilNodeType.Loop)
				{
					QilNode variable = qilNode10[0];
					QilNode binding = qilNode10[1];
					if (this.NonPositional(qilNode2, qilNode) && !this.IsDocOrderDistinct(qilNode10) && this.AllowReplace(XmlILOptimization.CommuteFilterLoop, local0))
					{
						QilNode qilNode11 = this.VisitFor(this.f.For(binding));
						return this.Replace(XmlILOptimization.CommuteFilterLoop, local0, this.VisitLoop(this.f.Loop(variable, this.VisitFilter(this.f.Filter(qilNode11, this.Subs(qilNode2, qilNode, qilNode11))))));
					}
				}
			}
			if (this[XmlILOptimization.NormalizeLoopInvariant] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode[0].NodeType != QilNodeType.OptimizeBarrier && !this.DependsOn(qilNode2, qilNode) && !OptimizerPatterns.Read(qilNode2).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && this.AllowReplace(XmlILOptimization.NormalizeLoopInvariant, local0))
			{
				return this.Replace(XmlILOptimization.NormalizeLoopInvariant, local0, this.VisitConditional(this.f.Conditional(qilNode2, qilNode[0], this.VisitSequence(this.f.Sequence()))));
			}
			if (this[XmlILOptimization.AnnotateMaxPositionEq] && qilNode2.NodeType == QilNodeType.Eq)
			{
				QilNode qilNode12 = qilNode2[0];
				QilNode qilNode13 = qilNode2[1];
				if (qilNode12.NodeType == QilNodeType.PositionOf && qilNode12[0] == qilNode && qilNode13.NodeType == QilNodeType.LiteralInt32)
				{
					int num = (int)((QilLiteral)qilNode13).Value;
					if (this.AllowReplace(XmlILOptimization.AnnotateMaxPositionEq, local0))
					{
						OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
						OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxPositionLe] && qilNode2.NodeType == QilNodeType.Le)
			{
				QilNode qilNode14 = qilNode2[0];
				QilNode qilNode15 = qilNode2[1];
				if (qilNode14.NodeType == QilNodeType.PositionOf && qilNode14[0] == qilNode && qilNode15.NodeType == QilNodeType.LiteralInt32)
				{
					int num2 = (int)((QilLiteral)qilNode15).Value;
					if (this.AllowReplace(XmlILOptimization.AnnotateMaxPositionLe, local0))
					{
						OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
						OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num2);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateMaxPositionLt] && qilNode2.NodeType == QilNodeType.Lt)
			{
				QilNode qilNode16 = qilNode2[0];
				QilNode qilNode17 = qilNode2[1];
				if (qilNode16.NodeType == QilNodeType.PositionOf && qilNode16[0] == qilNode && qilNode17.NodeType == QilNodeType.LiteralInt32)
				{
					int num3 = (int)((QilLiteral)qilNode17).Value;
					if (this.AllowReplace(XmlILOptimization.AnnotateMaxPositionLt, local0))
					{
						OptimizerPatterns.Write(qilNode).AddPattern(OptimizerPatternName.MaxPosition);
						OptimizerPatterns.Write(qilNode).AddArgument(OptimizerPatternArgument.ElementQName, num3 - 1);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateFilter] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode ndSrc = qilNode[0];
				if (this.AllowReplace(XmlILOptimization.AnnotateFilter, local0))
				{
					OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.Step);
					OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.IsDocOrderDistinct);
					OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.SameDepth);
				}
			}
			if (this[XmlILOptimization.AnnotateFilterElements] && qilNode.NodeType == QilNodeType.For && OptimizerPatterns.Read(qilNode[0]).MatchesPattern(OptimizerPatternName.Axis) && qilNode2.NodeType == QilNodeType.And)
			{
				QilNode qilNode18 = qilNode2[0];
				QilNode qilNode19 = qilNode2[1];
				if (qilNode18.NodeType == QilNodeType.IsType)
				{
					QilNode qilNode20 = qilNode18[0];
					QilNode qilNode21 = qilNode18[1];
					if (qilNode20 == qilNode && qilNode21.NodeType == QilNodeType.LiteralType && (XmlQueryType)((QilLiteral)qilNode21).Value == XmlQueryTypeFactory.Element && qilNode19.NodeType == QilNodeType.Eq)
					{
						QilNode qilNode22 = qilNode19[0];
						QilNode qilNode23 = qilNode19[1];
						if (qilNode22.NodeType == QilNodeType.NameOf && qilNode22[0] == qilNode && qilNode23.NodeType == QilNodeType.LiteralQName && this.AllowReplace(XmlILOptimization.AnnotateFilterElements, local0))
						{
							OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.FilterElements);
							OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, qilNode23);
						}
					}
				}
			}
			if (this[XmlILOptimization.AnnotateFilterContentKind] && qilNode.NodeType == QilNodeType.For && OptimizerPatterns.Read(qilNode[0]).MatchesPattern(OptimizerPatternName.Axis) && qilNode2.NodeType == QilNodeType.IsType)
			{
				QilNode qilNode24 = qilNode2[0];
				QilNode qilNode25 = qilNode2[1];
				if (qilNode24 == qilNode && qilNode25.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode25).Value;
					if (this.MatchesContentTest(xmlQueryType) && this.AllowReplace(XmlILOptimization.AnnotateFilterContentKind, local0))
					{
						OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.FilterContentKind);
						OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, xmlQueryType);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateFilterAttributeKind] && qilNode.NodeType == QilNodeType.For && qilNode[0].NodeType == QilNodeType.Content && qilNode2.NodeType == QilNodeType.IsType)
			{
				QilNode qilNode26 = qilNode2[0];
				QilNode qilNode27 = qilNode2[1];
				if (qilNode26 == qilNode && qilNode27.NodeType == QilNodeType.LiteralType && (XmlQueryType)((QilLiteral)qilNode27).Value == XmlQueryTypeFactory.Attribute && this.AllowReplace(XmlILOptimization.AnnotateFilterAttributeKind, local0))
				{
					OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.FilterAttributeKind);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x00116058 File Offset: 0x00114258
		protected override QilNode VisitSort(QilLoop local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode[0])));
			}
			if (this[XmlILOptimization.EliminateSort] && qilNode.NodeType == QilNodeType.For)
			{
				QilNode qilNode3 = qilNode[0];
				if (qilNode3.XmlType.IsSingleton && this.AllowReplace(XmlILOptimization.EliminateSort, local0))
				{
					return this.Replace(XmlILOptimization.EliminateSort, local0, this.VisitNop(this.f.Nop(qilNode3)));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x00116110 File Offset: 0x00114310
		protected override QilNode VisitSortKey(QilSortKey local0)
		{
			QilNode qilNode = local0[0];
			QilNode collation = local0[1];
			if (this[XmlILOptimization.NormalizeSortXsltConvert] && qilNode.NodeType == QilNodeType.XsltConvert)
			{
				QilNode qilNode2 = qilNode[0];
				QilNode qilNode3 = qilNode[1];
				if (qilNode3.NodeType == QilNodeType.LiteralType)
				{
					XmlQueryType left = (XmlQueryType)((QilLiteral)qilNode3).Value;
					if (qilNode2.XmlType == XmlQueryTypeFactory.IntX && left == XmlQueryTypeFactory.DoubleX && this.AllowReplace(XmlILOptimization.NormalizeSortXsltConvert, local0))
					{
						return this.Replace(XmlILOptimization.NormalizeSortXsltConvert, local0, this.VisitSortKey(this.f.SortKey(qilNode2, collation)));
					}
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x001161C8 File Offset: 0x001143C8
		protected override QilNode VisitDocOrderDistinct(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateDod] && this.IsDocOrderDistinct(qilNode) && this.AllowReplace(XmlILOptimization.EliminateDod, local0))
			{
				return this.Replace(XmlILOptimization.EliminateDod, local0, qilNode);
			}
			if (this[XmlILOptimization.FoldNamedDescendants] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode2 = qilNode[0];
				QilNode qilNode3 = qilNode[1];
				if (qilNode2.NodeType == QilNodeType.For)
				{
					QilNode qilNode4 = qilNode2[0];
					if (qilNode4.NodeType == QilNodeType.Loop)
					{
						QilNode variable = qilNode4[0];
						QilNode qilNode5 = qilNode4[1];
						if (qilNode5.NodeType == QilNodeType.DescendantOrSelf)
						{
							QilNode child = qilNode5[0];
							if (qilNode3.NodeType == QilNodeType.Filter)
							{
								QilNode refOld = qilNode3[0];
								QilNode expr = qilNode3[1];
								if ((OptimizerPatterns.Read(qilNode3).MatchesPattern(OptimizerPatternName.FilterElements) || OptimizerPatterns.Read(qilNode3).MatchesPattern(OptimizerPatternName.FilterContentKind)) && this.IsStepPattern(qilNode3, QilNodeType.Content) && this.AllowReplace(XmlILOptimization.FoldNamedDescendants, local0))
								{
									QilNode qilNode6 = this.VisitFor(this.f.For(this.VisitDescendant(this.f.Descendant(child))));
									return this.Replace(XmlILOptimization.FoldNamedDescendants, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(variable, this.VisitFilter(this.f.Filter(qilNode6, this.Subs(expr, refOld, qilNode6))))))));
								}
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.FoldNamedDescendants] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode7 = qilNode[0];
				QilNode qilNode8 = qilNode[1];
				if (qilNode7.NodeType == QilNodeType.For)
				{
					QilNode qilNode9 = qilNode7[0];
					if (qilNode9.NodeType == QilNodeType.DescendantOrSelf)
					{
						QilNode child2 = qilNode9[0];
						if (qilNode8.NodeType == QilNodeType.Filter)
						{
							QilNode refOld2 = qilNode8[0];
							QilNode expr2 = qilNode8[1];
							if ((OptimizerPatterns.Read(qilNode8).MatchesPattern(OptimizerPatternName.FilterElements) || OptimizerPatterns.Read(qilNode8).MatchesPattern(OptimizerPatternName.FilterContentKind)) && this.IsStepPattern(qilNode8, QilNodeType.Content) && this.AllowReplace(XmlILOptimization.FoldNamedDescendants, local0))
							{
								QilNode qilNode10 = this.VisitFor(this.f.For(this.VisitDescendant(this.f.Descendant(child2))));
								return this.Replace(XmlILOptimization.FoldNamedDescendants, local0, this.VisitFilter(this.f.Filter(qilNode10, this.Subs(expr2, refOld2, qilNode10))));
							}
						}
					}
				}
			}
			if (this[XmlILOptimization.CommuteDodFilter] && qilNode.NodeType == QilNodeType.Filter)
			{
				QilNode qilNode11 = qilNode[0];
				QilNode expr3 = qilNode[1];
				if (qilNode11.NodeType == QilNodeType.For)
				{
					QilNode child3 = qilNode11[0];
					if (!OptimizerPatterns.Read(qilNode11).MatchesPattern(OptimizerPatternName.IsPositional) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterElements) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterContentKind) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterAttributeKind) && this.AllowReplace(XmlILOptimization.CommuteDodFilter, local0))
					{
						QilNode qilNode12 = this.VisitFor(this.f.For(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(child3))));
						return this.Replace(XmlILOptimization.CommuteDodFilter, local0, this.VisitFilter(this.f.Filter(qilNode12, this.Subs(expr3, qilNode11, qilNode12))));
					}
				}
			}
			if (this[XmlILOptimization.CommuteDodFilter] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode13 = qilNode[0];
				QilNode qilNode14 = qilNode[1];
				if (qilNode14.NodeType == QilNodeType.Filter)
				{
					QilNode qilNode15 = qilNode14[0];
					QilNode expr4 = qilNode14[1];
					if (qilNode15.NodeType == QilNodeType.For)
					{
						QilNode body = qilNode15[0];
						if (!OptimizerPatterns.Read(qilNode15).MatchesPattern(OptimizerPatternName.IsPositional) && !this.DependsOn(expr4, qilNode13) && !OptimizerPatterns.Read(qilNode14).MatchesPattern(OptimizerPatternName.FilterElements) && !OptimizerPatterns.Read(qilNode14).MatchesPattern(OptimizerPatternName.FilterContentKind) && !OptimizerPatterns.Read(qilNode14).MatchesPattern(OptimizerPatternName.FilterAttributeKind) && this.AllowReplace(XmlILOptimization.CommuteDodFilter, local0))
						{
							QilNode qilNode16 = this.VisitFor(this.f.For(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(qilNode13, body))))));
							return this.Replace(XmlILOptimization.CommuteDodFilter, local0, this.VisitFilter(this.f.Filter(qilNode16, this.Subs(expr4, qilNode15, qilNode16))));
						}
					}
				}
			}
			if (this[XmlILOptimization.IntroduceDod] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode17 = qilNode[0];
				QilNode expr5 = qilNode[1];
				if (qilNode17.NodeType == QilNodeType.For)
				{
					QilNode qilNode18 = qilNode17[0];
					if (!this.IsDocOrderDistinct(qilNode18) && !OptimizerPatterns.Read(qilNode17).MatchesPattern(OptimizerPatternName.IsPositional) && qilNode18.XmlType.IsSubtypeOf(XmlQueryTypeFactory.NodeNotRtfS) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterElements) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterContentKind) && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.FilterAttributeKind) && this.AllowReplace(XmlILOptimization.IntroduceDod, local0))
					{
						QilNode qilNode19 = this.VisitFor(this.f.For(this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode18))));
						return this.Replace(XmlILOptimization.IntroduceDod, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(qilNode19, this.Subs(expr5, qilNode17, qilNode19))))));
					}
				}
			}
			if (this[XmlILOptimization.IntroducePrecedingDod] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode variable2 = qilNode[0];
				QilNode qilNode20 = qilNode[1];
				if (!this.IsDocOrderDistinct(qilNode20) && this.IsStepPattern(qilNode20, QilNodeType.PrecedingSibling) && this.AllowReplace(XmlILOptimization.IntroducePrecedingDod, local0))
				{
					return this.Replace(XmlILOptimization.IntroducePrecedingDod, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(variable2, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(qilNode20)))))));
				}
			}
			if (this[XmlILOptimization.EliminateReturnDod] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode variable3 = qilNode[0];
				QilNode qilNode21 = qilNode[1];
				if (qilNode21.NodeType == QilNodeType.DocOrderDistinct)
				{
					QilNode qilNode22 = qilNode21[0];
					if (!this.IsStepPattern(qilNode22, QilNodeType.PrecedingSibling) && this.AllowReplace(XmlILOptimization.EliminateReturnDod, local0))
					{
						return this.Replace(XmlILOptimization.EliminateReturnDod, local0, this.VisitDocOrderDistinct(this.f.DocOrderDistinct(this.VisitLoop(this.f.Loop(variable3, qilNode22)))));
					}
				}
			}
			if (this[XmlILOptimization.AnnotateDod] && this.AllowReplace(XmlILOptimization.AnnotateDod, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Inherit(qilNode, local0, OptimizerPatternName.SameDepth);
			}
			if (this[XmlILOptimization.AnnotateDodReverse] && this.AllowDodReverse(qilNode) && this.AllowReplace(XmlILOptimization.AnnotateDodReverse, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.DodReverse);
				OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, qilNode);
			}
			if (this[XmlILOptimization.AnnotateJoinAndDod] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode23 = qilNode[0];
				QilNode qilNode24 = qilNode[1];
				if (qilNode23.NodeType == QilNodeType.For)
				{
					QilNode nd = qilNode23[0];
					if (this.IsDocOrderDistinct(nd) && this.AllowJoinAndDod(qilNode24) && qilNode23 == OptimizerPatterns.Read(qilNode24).GetArgument(OptimizerPatternArgument.StepInput) && this.AllowReplace(XmlILOptimization.AnnotateJoinAndDod, local0))
					{
						OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.JoinAndDod);
						OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, qilNode24);
					}
				}
			}
			if (this[XmlILOptimization.AnnotateDodMerge] && qilNode.NodeType == QilNodeType.Loop)
			{
				QilNode qilNode25 = qilNode[1];
				if (qilNode25.NodeType == QilNodeType.Invoke && this.IsDocOrderDistinct(qilNode25) && this.AllowReplace(XmlILOptimization.AnnotateDodMerge, local0))
				{
					OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.DodMerge);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x001169D4 File Offset: 0x00114BD4
		protected override QilNode VisitFunction(QilFunction local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			QilNode qilNode3 = local0[2];
			XmlQueryType xmlType = local0.XmlType;
			if (local0.XmlType.IsSubtypeOf(XmlQueryTypeFactory.NodeS) && this[XmlILOptimization.AnnotateIndex1] && qilNode.Count == 2 && qilNode[0].XmlType.IsSubtypeOf(XmlQueryTypeFactory.Node) && qilNode[1].XmlType == XmlQueryTypeFactory.StringX && qilNode2.NodeType == QilNodeType.Filter)
			{
				QilNode qilNode4 = qilNode2[0];
				QilNode qilNode5 = qilNode2[1];
				if (qilNode4.NodeType == QilNodeType.For)
				{
					QilNode expr = qilNode4[0];
					if (qilNode5.NodeType == QilNodeType.Not)
					{
						QilNode qilNode6 = qilNode5[0];
						if (qilNode6.NodeType == QilNodeType.IsEmpty)
						{
							QilNode qilNode7 = qilNode6[0];
							if (qilNode7.NodeType == QilNodeType.Filter)
							{
								QilNode qilNode8 = qilNode7[0];
								QilNode qilNode9 = qilNode7[1];
								if (qilNode8.NodeType == QilNodeType.For)
								{
									QilNode qilNode10 = qilNode8[0];
									if (qilNode9.NodeType == QilNodeType.Eq)
									{
										QilNode qilNode11 = qilNode9[0];
										QilNode qilNode12 = qilNode9[1];
										if (qilNode11 == qilNode8 && qilNode12.NodeType == QilNodeType.Parameter && qilNode12 == qilNode[1] && this.IsDocOrderDistinct(qilNode2) && this.AllowReplace(XmlILOptimization.AnnotateIndex1, local0))
										{
											XmlILOptimizerVisitor.EqualityIndexVisitor equalityIndexVisitor = new XmlILOptimizerVisitor.EqualityIndexVisitor();
											if (equalityIndexVisitor.Scan(expr, qilNode[0], qilNode12) && equalityIndexVisitor.Scan(qilNode10, qilNode[0], qilNode12))
											{
												OptimizerPatterns optimizerPatterns = OptimizerPatterns.Write(qilNode2);
												optimizerPatterns.AddPattern(OptimizerPatternName.EqualityIndex);
												optimizerPatterns.AddArgument(OptimizerPatternArgument.StepNode, qilNode4);
												optimizerPatterns.AddArgument(OptimizerPatternArgument.StepInput, qilNode10);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (local0.XmlType.IsSubtypeOf(XmlQueryTypeFactory.NodeS) && this[XmlILOptimization.AnnotateIndex2] && qilNode.Count == 2 && qilNode[0].XmlType == XmlQueryTypeFactory.Node && qilNode[1].XmlType == XmlQueryTypeFactory.StringX && qilNode2.NodeType == QilNodeType.Filter)
			{
				QilNode qilNode13 = qilNode2[0];
				QilNode qilNode14 = qilNode2[1];
				if (qilNode13.NodeType == QilNodeType.For)
				{
					QilNode expr2 = qilNode13[0];
					if (qilNode14.NodeType == QilNodeType.Eq)
					{
						QilNode qilNode15 = qilNode14[0];
						QilNode qilNode16 = qilNode14[1];
						if (qilNode16.NodeType == QilNodeType.Parameter && qilNode16 == qilNode[1] && this.IsDocOrderDistinct(qilNode2) && this.AllowReplace(XmlILOptimization.AnnotateIndex2, local0))
						{
							XmlILOptimizerVisitor.EqualityIndexVisitor equalityIndexVisitor2 = new XmlILOptimizerVisitor.EqualityIndexVisitor();
							if (equalityIndexVisitor2.Scan(expr2, qilNode[0], qilNode16) && equalityIndexVisitor2.Scan(qilNode15, qilNode[0], qilNode16))
							{
								OptimizerPatterns optimizerPatterns2 = OptimizerPatterns.Write(qilNode2);
								optimizerPatterns2.AddPattern(OptimizerPatternName.EqualityIndex);
								optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepNode, qilNode13);
								optimizerPatterns2.AddArgument(OptimizerPatternArgument.StepInput, qilNode15);
							}
						}
					}
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F0D RID: 12045 RVA: 0x00116CD8 File Offset: 0x00114ED8
		protected override QilNode VisitInvoke(QilInvoke local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.NormalizeInvokeEmpty] && qilNode.NodeType == QilNodeType.Function)
			{
				QilNode qilNode3 = qilNode[1];
				if (qilNode3.NodeType == QilNodeType.Sequence && qilNode3.Count == 0 && this.AllowReplace(XmlILOptimization.NormalizeInvokeEmpty, local0))
				{
					return this.Replace(XmlILOptimization.NormalizeInvokeEmpty, local0, this.VisitSequence(this.f.Sequence()));
				}
			}
			if (this[XmlILOptimization.AnnotateTrackCallers] && this.AllowReplace(XmlILOptimization.AnnotateTrackCallers, local0))
			{
				XmlILConstructInfo.Write(qilNode).CallersInfo.Add(XmlILConstructInfo.Write(local0));
			}
			if (this[XmlILOptimization.AnnotateInvoke] && qilNode.NodeType == QilNodeType.Function)
			{
				QilNode ndSrc = qilNode[1];
				if (this.AllowReplace(XmlILOptimization.AnnotateInvoke, local0))
				{
					OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.IsDocOrderDistinct);
					OptimizerPatterns.Inherit(ndSrc, local0, OptimizerPatternName.SameDepth);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x00116DB4 File Offset: 0x00114FB4
		protected override QilNode VisitContent(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateContent] && this.AllowReplace(XmlILOptimization.AnnotateContent, local0))
			{
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x00116E54 File Offset: 0x00115054
		protected override QilNode VisitAttribute(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateAttribute] && this.AllowReplace(XmlILOptimization.AnnotateAttribute, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x00116F38 File Offset: 0x00115138
		protected override QilNode VisitParent(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateParent] && this.AllowReplace(XmlILOptimization.AnnotateParent, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x00116FD8 File Offset: 0x001151D8
		protected override QilNode VisitRoot(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateRoot] && this.AllowReplace(XmlILOptimization.AnnotateRoot, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x00117078 File Offset: 0x00115278
		protected override QilNode VisitDescendant(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateDescendant] && this.AllowReplace(XmlILOptimization.AnnotateDescendant, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x00117108 File Offset: 0x00115308
		protected override QilNode VisitDescendantOrSelf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateDescendantSelf] && this.AllowReplace(XmlILOptimization.AnnotateDescendantSelf, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x00117198 File Offset: 0x00115398
		protected override QilNode VisitAncestor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateAncestor] && this.AllowReplace(XmlILOptimization.AnnotateAncestor, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x0011721C File Offset: 0x0011541C
		protected override QilNode VisitAncestorOrSelf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateAncestorSelf] && this.AllowReplace(XmlILOptimization.AnnotateAncestorSelf, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x001172A0 File Offset: 0x001154A0
		protected override QilNode VisitPreceding(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotatePreceding] && this.AllowReplace(XmlILOptimization.AnnotatePreceding, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x00117324 File Offset: 0x00115524
		protected override QilNode VisitFollowingSibling(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateFollowingSibling] && this.AllowReplace(XmlILOptimization.AnnotateFollowingSibling, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x001173C4 File Offset: 0x001155C4
		protected override QilNode VisitPrecedingSibling(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotatePrecedingSibling] && this.AllowReplace(XmlILOptimization.AnnotatePrecedingSibling, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x00117458 File Offset: 0x00115658
		protected override QilNode VisitNodeRange(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateNodeRange] && this.AllowReplace(XmlILOptimization.AnnotateNodeRange, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x00117530 File Offset: 0x00115730
		protected override QilNode VisitDeref(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x001175D0 File Offset: 0x001157D0
		protected override QilNode VisitElementCtor(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Right = this.elemAnalyzer.Analyze(local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F1C RID: 12060 RVA: 0x00117698 File Offset: 0x00115898
		protected override QilNode VisitAttributeCtor(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Right = this.contentAnalyzer.Analyze(local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F1D RID: 12061 RVA: 0x00117760 File Offset: 0x00115960
		protected override QilNode VisitCommentCtor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Child = this.contentAnalyzer.Analyze(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x001177E0 File Offset: 0x001159E0
		protected override QilNode VisitPICtor(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Right = this.contentAnalyzer.Analyze(local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x001178A8 File Offset: 0x00115AA8
		protected override QilNode VisitTextCtor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x00117924 File Offset: 0x00115B24
		protected override QilNode VisitRawTextCtor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F21 RID: 12065 RVA: 0x001179A0 File Offset: 0x00115BA0
		protected override QilNode VisitDocumentCtor(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Child = this.contentAnalyzer.Analyze(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F22 RID: 12066 RVA: 0x00117A20 File Offset: 0x00115C20
		protected override QilNode VisitNamespaceDecl(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (XmlILConstructInfo.Read(local0).IsNamespaceInScope && this[XmlILOptimization.EliminateNamespaceDecl] && this.AllowReplace(XmlILOptimization.EliminateNamespaceDecl, local0))
			{
				return this.Replace(XmlILOptimization.EliminateNamespaceDecl, local0, this.VisitSequence(this.f.Sequence()));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F23 RID: 12067 RVA: 0x00117B20 File Offset: 0x00115D20
		protected override QilNode VisitRtfCtor(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Left = this.contentAnalyzer.Analyze(local0, qilNode);
			}
			if (this[XmlILOptimization.AnnotateSingleTextRtf] && qilNode.NodeType == QilNodeType.TextCtor)
			{
				QilNode arg = qilNode[0];
				if (this.AllowReplace(XmlILOptimization.AnnotateSingleTextRtf, local0))
				{
					OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SingleTextRtf);
					OptimizerPatterns.Write(local0).AddArgument(OptimizerPatternArgument.ElementQName, arg);
					XmlILConstructInfo.Write(local0).PullFromIteratorFirst = true;
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x00117BF8 File Offset: 0x00115DF8
		protected override QilNode VisitNameOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x00117C54 File Offset: 0x00115E54
		protected override QilNode VisitLocalNameOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x00117CB0 File Offset: 0x00115EB0
		protected override QilNode VisitNamespaceUriOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x00117D0C File Offset: 0x00115F0C
		protected override QilNode VisitPrefixOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x00117D68 File Offset: 0x00115F68
		protected override QilNode VisitTypeAssert(QilTargetType local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateTypeAssert] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType baseType = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.NeverSubtypeOf(baseType) && this.AllowReplace(XmlILOptimization.EliminateTypeAssert, local0))
				{
					return this.Replace(XmlILOptimization.EliminateTypeAssert, local0, this.VisitError(this.f.Error(this.VisitLiteralString(this.f.LiteralString(string.Empty)))));
				}
			}
			if (this[XmlILOptimization.EliminateTypeAssert] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.Prime.NeverSubtypeOf(xmlQueryType.Prime) && this.AllowReplace(XmlILOptimization.EliminateTypeAssert, local0))
				{
					return this.Replace(XmlILOptimization.EliminateTypeAssert, local0, this.VisitConditional(this.f.Conditional(this.VisitIsEmpty(this.f.IsEmpty(qilNode)), this.VisitSequence(this.f.Sequence()), this.VisitError(this.f.Error(this.VisitLiteralString(this.f.LiteralString(string.Empty)))))));
				}
			}
			if (this[XmlILOptimization.EliminateTypeAssertOptional] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType baseType2 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.IsSubtypeOf(baseType2) && this.AllowReplace(XmlILOptimization.EliminateTypeAssertOptional, local0))
				{
					return this.Replace(XmlILOptimization.EliminateTypeAssertOptional, local0, qilNode);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x00117F38 File Offset: 0x00116138
		protected override QilNode VisitIsType(QilTargetType local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateIsType] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType baseType = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.IsSubtypeOf(baseType) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitTrue(this.f.True()));
				}
			}
			if (this[XmlILOptimization.EliminateIsType] && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType baseType2 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.NeverSubtypeOf(baseType2) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitFalse(this.f.False()));
				}
			}
			if (this[XmlILOptimization.EliminateIsType] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType xmlQueryType = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.Prime.NeverSubtypeOf(xmlQueryType.Prime) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitIsEmpty(this.f.IsEmpty(qilNode)));
				}
			}
			if (this[XmlILOptimization.EliminateIsType] && OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType baseType3 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.IsSubtypeOf(baseType3) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitLoop(this.f.Loop(this.VisitLet(this.f.Let(qilNode)), this.VisitTrue(this.f.True()))));
				}
			}
			if (this[XmlILOptimization.EliminateIsType] && OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType baseType4 = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType.NeverSubtypeOf(baseType4) && this.AllowReplace(XmlILOptimization.EliminateIsType, local0))
				{
					return this.Replace(XmlILOptimization.EliminateIsType, local0, this.VisitLoop(this.f.Loop(this.VisitLet(this.f.Let(qilNode)), this.VisitFalse(this.f.False()))));
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x001181F0 File Offset: 0x001163F0
		protected override QilNode VisitIsEmpty(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.EliminateIsEmpty] && qilNode.NodeType == QilNodeType.Sequence && qilNode.Count == 0 && this.AllowReplace(XmlILOptimization.EliminateIsEmpty, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIsEmpty, local0, this.VisitTrue(this.f.True()));
			}
			if (this[XmlILOptimization.EliminateIsEmpty] && !qilNode.XmlType.MaybeEmpty && !OptimizerPatterns.Read(qilNode).MatchesPattern(OptimizerPatternName.MaybeSideEffects) && this.AllowReplace(XmlILOptimization.EliminateIsEmpty, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIsEmpty, local0, this.VisitFalse(this.f.False()));
			}
			if (this[XmlILOptimization.EliminateIsEmpty] && !qilNode.XmlType.MaybeEmpty && this.AllowReplace(XmlILOptimization.EliminateIsEmpty, local0))
			{
				return this.Replace(XmlILOptimization.EliminateIsEmpty, local0, this.VisitLoop(this.f.Loop(this.VisitLet(this.f.Let(qilNode)), this.VisitFalse(this.f.False()))));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x00118338 File Offset: 0x00116538
		protected override QilNode VisitXPathNodeValue(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x00118394 File Offset: 0x00116594
		protected override QilNode VisitXPathFollowing(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateXPathFollowing] && this.AllowReplace(XmlILOptimization.AnnotateXPathFollowing, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x00118424 File Offset: 0x00116624
		protected override QilNode VisitXPathPreceding(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateXPathPreceding] && this.AllowReplace(XmlILOptimization.AnnotateXPathPreceding, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x001184A8 File Offset: 0x001166A8
		protected override QilNode VisitXPathNamespace(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateNamespace] && this.AllowReplace(XmlILOptimization.AnnotateNamespace, local0))
			{
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.Axis);
				this.AddStepPattern(local0, qilNode);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.IsDocOrderDistinct);
				OptimizerPatterns.Write(local0).AddPattern(OptimizerPatternName.SameDepth);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x00118548 File Offset: 0x00116748
		protected override QilNode VisitXsltGenerateId(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x001185A4 File Offset: 0x001167A4
		protected override QilNode VisitXsltCopy(QilBinary local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldNone] && qilNode2.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode2)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				local0.Right = this.contentAnalyzer.Analyze(local0, qilNode2);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x0011866C File Offset: 0x0011686C
		protected override QilNode VisitXsltCopyOf(QilUnary local0)
		{
			QilNode qilNode = local0[0];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.AnnotateConstruction] && this.AllowReplace(XmlILOptimization.AnnotateConstruction, local0))
			{
				this.contentAnalyzer.Analyze(local0, null);
			}
			return this.NoReplace(local0);
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x001186E8 File Offset: 0x001168E8
		protected override QilNode VisitXsltConvert(QilTargetType local0)
		{
			QilNode qilNode = local0[0];
			QilNode qilNode2 = local0[1];
			if (this[XmlILOptimization.FoldNone] && qilNode.XmlType == XmlQueryTypeFactory.None && this.AllowReplace(XmlILOptimization.FoldNone, local0))
			{
				return this.Replace(XmlILOptimization.FoldNone, local0, this.VisitNop(this.f.Nop(qilNode)));
			}
			if (this[XmlILOptimization.FoldXsltConvertLiteral] && this.IsLiteral(qilNode) && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType typTarget = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (this.CanFoldXsltConvert(qilNode, typTarget) && this.AllowReplace(XmlILOptimization.FoldXsltConvertLiteral, local0))
				{
					return this.Replace(XmlILOptimization.FoldXsltConvertLiteral, local0, this.FoldXsltConvert(qilNode, typTarget));
				}
			}
			if (this[XmlILOptimization.EliminateXsltConvert] && qilNode2.NodeType == QilNodeType.LiteralType)
			{
				XmlQueryType right = (XmlQueryType)((QilLiteral)qilNode2).Value;
				if (qilNode.XmlType == right && this.AllowReplace(XmlILOptimization.EliminateXsltConvert, local0))
				{
					return this.Replace(XmlILOptimization.EliminateXsltConvert, local0, qilNode);
				}
			}
			return this.NoReplace(local0);
		}

		// Token: 0x170008AF RID: 2223
		private bool this[XmlILOptimization ann]
		{
			get
			{
				return base.Patterns.IsSet((int)ann);
			}
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x001187F6 File Offset: 0x001169F6
		private bool DependsOn(QilNode expr, QilNode target)
		{
			return new XmlILOptimizerVisitor.NodeFinder().Find(expr, target);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x00118804 File Offset: 0x00116A04
		protected bool NonPositional(QilNode expr, QilNode iter)
		{
			return !new XmlILOptimizerVisitor.PositionOfFinder().Find(expr, iter);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x00118818 File Offset: 0x00116A18
		private QilNode Subs(QilNode expr, QilNode refOld, QilNode refNew)
		{
			this.subs.AddSubstitutionPair(refOld, refNew);
			QilNode result;
			if (expr is QilReference)
			{
				result = this.VisitReference(expr);
			}
			else
			{
				result = this.Visit(expr);
			}
			this.subs.RemoveLastSubstitutionPair();
			return result;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x00118858 File Offset: 0x00116A58
		private bool IsGlobalVariable(QilIterator iter)
		{
			return this.qil.GlobalVariableList.Contains(iter);
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x0011886B File Offset: 0x00116A6B
		private bool IsGlobalValue(QilNode nd)
		{
			if (nd.NodeType == QilNodeType.Let)
			{
				return this.qil.GlobalVariableList.Contains(nd);
			}
			return nd.NodeType == QilNodeType.Parameter && this.qil.GlobalParameterList.Contains(nd);
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x001188A8 File Offset: 0x00116AA8
		private bool IsPrimitiveNumeric(XmlQueryType typ)
		{
			return typ == XmlQueryTypeFactory.IntX || typ == XmlQueryTypeFactory.IntegerX || typ == XmlQueryTypeFactory.DecimalX || typ == XmlQueryTypeFactory.FloatX || typ == XmlQueryTypeFactory.DoubleX;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x00118904 File Offset: 0x00116B04
		private bool MatchesContentTest(XmlQueryType typ)
		{
			return typ == XmlQueryTypeFactory.Element || typ == XmlQueryTypeFactory.Text || typ == XmlQueryTypeFactory.Comment || typ == XmlQueryTypeFactory.PI || typ == XmlQueryTypeFactory.Content;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x00118960 File Offset: 0x00116B60
		private bool IsConstructedExpression(QilNode nd)
		{
			if (this.qil.IsDebug)
			{
				return true;
			}
			if (nd.XmlType.IsNode)
			{
				QilNodeType nodeType = nd.NodeType;
				if (nodeType <= QilNodeType.Loop)
				{
					switch (nodeType)
					{
					case QilNodeType.Conditional:
						break;
					case QilNodeType.Choice:
						return true;
					case QilNodeType.Length:
						return false;
					case QilNodeType.Sequence:
						if (nd.Count == 0)
						{
							return true;
						}
						using (IEnumerator<QilNode> enumerator = nd.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								QilNode nd2 = enumerator.Current;
								if (this.IsConstructedExpression(nd2))
								{
									return true;
								}
							}
							return false;
						}
						break;
					default:
						if (nodeType != QilNodeType.Loop)
						{
							return false;
						}
						return this.IsConstructedExpression(((QilLoop)nd).Body);
					}
					QilTernary qilTernary = (QilTernary)nd;
					return this.IsConstructedExpression(qilTernary.Center) || this.IsConstructedExpression(qilTernary.Right);
				}
				if (nodeType == QilNodeType.Invoke)
				{
					return !((QilInvoke)nd).Function.XmlType.IsAtomicValue;
				}
				if (nodeType - QilNodeType.ElementCtor > 7 && nodeType - QilNodeType.XsltCopy > 1)
				{
					return false;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x00118A78 File Offset: 0x00116C78
		private bool IsLiteral(QilNode nd)
		{
			QilNodeType nodeType = nd.NodeType;
			return nodeType - QilNodeType.True <= 7;
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x00118A98 File Offset: 0x00116C98
		private bool AreLiteralArgs(QilNode nd)
		{
			foreach (QilNode nd2 in nd)
			{
				if (!this.IsLiteral(nd2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x00118AEC File Offset: 0x00116CEC
		private object ExtractLiteralValue(QilNode nd)
		{
			if (nd.NodeType == QilNodeType.True)
			{
				return true;
			}
			if (nd.NodeType == QilNodeType.False)
			{
				return false;
			}
			if (nd.NodeType == QilNodeType.LiteralQName)
			{
				return nd;
			}
			return ((QilLiteral)nd).Value;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x00118B28 File Offset: 0x00116D28
		private bool HasNestedSequence(QilNode nd)
		{
			using (IEnumerator<QilNode> enumerator = nd.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.NodeType == QilNodeType.Sequence)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x00118B78 File Offset: 0x00116D78
		private bool AllowJoinAndDod(QilNode nd)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(nd);
			return (optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterElements) || optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterContentKind)) && (this.IsStepPattern(optimizerPatterns, QilNodeType.DescendantOrSelf) || this.IsStepPattern(optimizerPatterns, QilNodeType.Descendant) || this.IsStepPattern(optimizerPatterns, QilNodeType.Content) || this.IsStepPattern(optimizerPatterns, QilNodeType.XPathPreceding) || this.IsStepPattern(optimizerPatterns, QilNodeType.XPathFollowing) || this.IsStepPattern(optimizerPatterns, QilNodeType.FollowingSibling));
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x00118BE4 File Offset: 0x00116DE4
		private bool AllowDodReverse(QilNode nd)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(nd);
			return (optimizerPatterns.MatchesPattern(OptimizerPatternName.Axis) || optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterElements) || optimizerPatterns.MatchesPattern(OptimizerPatternName.FilterContentKind)) && (this.IsStepPattern(optimizerPatterns, QilNodeType.Ancestor) || this.IsStepPattern(optimizerPatterns, QilNodeType.AncestorOrSelf) || this.IsStepPattern(optimizerPatterns, QilNodeType.XPathPreceding) || this.IsStepPattern(optimizerPatterns, QilNodeType.PrecedingSibling));
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x00118C43 File Offset: 0x00116E43
		private bool CanFoldXsltConvert(QilNode ndLiteral, XmlQueryType typTarget)
		{
			return this.FoldXsltConvert(ndLiteral, typTarget).NodeType != QilNodeType.XsltConvert;
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x00118C5C File Offset: 0x00116E5C
		private bool CanFoldXsltConvertNonLossy(QilNode ndLiteral, XmlQueryType typTarget)
		{
			QilNode qilNode = this.FoldXsltConvert(ndLiteral, typTarget);
			if (qilNode.NodeType == QilNodeType.XsltConvert)
			{
				return false;
			}
			qilNode = this.FoldXsltConvert(qilNode, ndLiteral.XmlType);
			return qilNode.NodeType != QilNodeType.XsltConvert && this.ExtractLiteralValue(ndLiteral).Equals(this.ExtractLiteralValue(qilNode));
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x00118CAC File Offset: 0x00116EAC
		private QilNode FoldXsltConvert(QilNode ndLiteral, XmlQueryType typTarget)
		{
			try
			{
				if (typTarget.IsAtomicValue)
				{
					XmlAtomicValue xmlAtomicValue = new XmlAtomicValue(ndLiteral.XmlType.SchemaType, this.ExtractLiteralValue(ndLiteral));
					xmlAtomicValue = XsltConvert.ConvertToType(xmlAtomicValue, typTarget);
					if (typTarget == XmlQueryTypeFactory.StringX)
					{
						return this.f.LiteralString(xmlAtomicValue.Value);
					}
					if (typTarget == XmlQueryTypeFactory.IntX)
					{
						return this.f.LiteralInt32(xmlAtomicValue.ValueAsInt);
					}
					if (typTarget == XmlQueryTypeFactory.IntegerX)
					{
						return this.f.LiteralInt64(xmlAtomicValue.ValueAsLong);
					}
					if (typTarget == XmlQueryTypeFactory.DecimalX)
					{
						return this.f.LiteralDecimal((decimal)xmlAtomicValue.ValueAs(XsltConvert.DecimalType));
					}
					if (typTarget == XmlQueryTypeFactory.DoubleX)
					{
						return this.f.LiteralDouble(xmlAtomicValue.ValueAsDouble);
					}
					if (typTarget == XmlQueryTypeFactory.BooleanX)
					{
						return xmlAtomicValue.ValueAsBoolean ? this.f.True() : this.f.False();
					}
				}
			}
			catch (OverflowException)
			{
			}
			catch (FormatException)
			{
			}
			return this.f.XsltConvert(ndLiteral, typTarget);
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x00118E18 File Offset: 0x00117018
		private QilNode FoldComparison(QilNodeType opType, QilNode left, QilNode right)
		{
			object obj = this.ExtractLiteralValue(left);
			object obj2 = this.ExtractLiteralValue(right);
			if (left.NodeType == QilNodeType.LiteralDouble && (double.IsNaN((double)obj) || double.IsNaN((double)obj2)))
			{
				if (opType != QilNodeType.Ne)
				{
					return this.f.False();
				}
				return this.f.True();
			}
			else if (opType == QilNodeType.Eq)
			{
				if (!obj.Equals(obj2))
				{
					return this.f.False();
				}
				return this.f.True();
			}
			else if (opType == QilNodeType.Ne)
			{
				if (!obj.Equals(obj2))
				{
					return this.f.True();
				}
				return this.f.False();
			}
			else
			{
				int num;
				if (left.NodeType == QilNodeType.LiteralString)
				{
					num = string.CompareOrdinal((string)obj, (string)obj2);
				}
				else
				{
					num = ((IComparable)obj).CompareTo(obj2);
				}
				switch (opType)
				{
				case QilNodeType.Gt:
					if (num <= 0)
					{
						return this.f.False();
					}
					return this.f.True();
				case QilNodeType.Ge:
					if (num < 0)
					{
						return this.f.False();
					}
					return this.f.True();
				case QilNodeType.Lt:
					if (num >= 0)
					{
						return this.f.False();
					}
					return this.f.True();
				case QilNodeType.Le:
					if (num > 0)
					{
						return this.f.False();
					}
					return this.f.True();
				default:
					return null;
				}
			}
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x00118F79 File Offset: 0x00117179
		private bool CanFoldArithmetic(QilNodeType opType, QilLiteral left, QilLiteral right)
		{
			return this.FoldArithmetic(opType, left, right) is QilLiteral;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x00118F8C File Offset: 0x0011718C
		private QilNode FoldArithmetic(QilNodeType opType, QilLiteral left, QilLiteral right)
		{
			checked
			{
				try
				{
					switch (left.NodeType)
					{
					case QilNodeType.LiteralInt32:
					{
						int num = left;
						int num2 = right;
						switch (opType)
						{
						case QilNodeType.Add:
							return this.f.LiteralInt32(num + num2);
						case QilNodeType.Subtract:
							return this.f.LiteralInt32(num - num2);
						case QilNodeType.Multiply:
							return this.f.LiteralInt32(num * num2);
						case QilNodeType.Divide:
							return this.f.LiteralInt32(num / num2);
						case QilNodeType.Modulo:
							return this.f.LiteralInt32(num % num2);
						}
						break;
					}
					case QilNodeType.LiteralInt64:
					{
						long num3 = left;
						long num4 = right;
						switch (opType)
						{
						case QilNodeType.Add:
							return this.f.LiteralInt64(num3 + num4);
						case QilNodeType.Subtract:
							return this.f.LiteralInt64(num3 - num4);
						case QilNodeType.Multiply:
							return this.f.LiteralInt64(num3 * num4);
						case QilNodeType.Divide:
							return this.f.LiteralInt64(num3 / num4);
						case QilNodeType.Modulo:
							return this.f.LiteralInt64(num3 % num4);
						}
						break;
					}
					case QilNodeType.LiteralDouble:
					{
						double num5 = left;
						double num6 = right;
						unchecked
						{
							switch (opType)
							{
							case QilNodeType.Add:
								return this.f.LiteralDouble(num5 + num6);
							case QilNodeType.Subtract:
								return this.f.LiteralDouble(num5 - num6);
							case QilNodeType.Multiply:
								return this.f.LiteralDouble(num5 * num6);
							case QilNodeType.Divide:
								return this.f.LiteralDouble(num5 / num6);
							case QilNodeType.Modulo:
								return this.f.LiteralDouble(num5 % num6);
							}
							break;
						}
					}
					case QilNodeType.LiteralDecimal:
					{
						decimal d = left;
						decimal d2 = right;
						switch (opType)
						{
						case QilNodeType.Add:
							return this.f.LiteralDecimal(d + d2);
						case QilNodeType.Subtract:
							return this.f.LiteralDecimal(d - d2);
						case QilNodeType.Multiply:
							return this.f.LiteralDecimal(d * d2);
						case QilNodeType.Divide:
							return this.f.LiteralDecimal(d / d2);
						case QilNodeType.Modulo:
							return this.f.LiteralDecimal(d % d2);
						}
						break;
					}
					}
				}
				catch (OverflowException)
				{
				}
				catch (DivideByZeroException)
				{
				}
				switch (opType)
				{
				case QilNodeType.Add:
					return this.f.Add(left, right);
				case QilNodeType.Subtract:
					return this.f.Subtract(left, right);
				case QilNodeType.Multiply:
					return this.f.Multiply(left, right);
				case QilNodeType.Divide:
					return this.f.Divide(left, right);
				case QilNodeType.Modulo:
					return this.f.Modulo(left, right);
				default:
					return null;
				}
				QilNode result;
				return result;
			}
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x001192E4 File Offset: 0x001174E4
		private void AddStepPattern(QilNode nd, QilNode input)
		{
			OptimizerPatterns optimizerPatterns = OptimizerPatterns.Write(nd);
			optimizerPatterns.AddPattern(OptimizerPatternName.Step);
			optimizerPatterns.AddArgument(OptimizerPatternArgument.StepNode, nd);
			optimizerPatterns.AddArgument(OptimizerPatternArgument.StepInput, input);
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x00119303 File Offset: 0x00117503
		private bool IsDocOrderDistinct(QilNode nd)
		{
			return OptimizerPatterns.Read(nd).MatchesPattern(OptimizerPatternName.IsDocOrderDistinct);
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x00119311 File Offset: 0x00117511
		private bool IsStepPattern(QilNode nd, QilNodeType stepType)
		{
			return this.IsStepPattern(OptimizerPatterns.Read(nd), stepType);
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x00119320 File Offset: 0x00117520
		private bool IsStepPattern(OptimizerPatterns patt, QilNodeType stepType)
		{
			return patt.MatchesPattern(OptimizerPatternName.Step) && ((QilNode)patt.GetArgument(OptimizerPatternArgument.StepNode)).NodeType == stepType;
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x00119344 File Offset: 0x00117544
		private static void EliminateUnusedGlobals(IList<QilNode> globals)
		{
			int num = 0;
			for (int i = 0; i < globals.Count; i++)
			{
				QilNode qilNode = globals[i];
				bool flag;
				if (qilNode.NodeType == QilNodeType.Function)
				{
					flag = (XmlILConstructInfo.Read(qilNode).CallersInfo.Count != 0);
				}
				else
				{
					OptimizerPatterns optimizerPatterns = OptimizerPatterns.Read(qilNode);
					flag = (optimizerPatterns.MatchesPattern(OptimizerPatternName.IsReferenced) || optimizerPatterns.MatchesPattern(OptimizerPatternName.MaybeSideEffects));
				}
				if (flag)
				{
					if (num < i)
					{
						globals[num] = globals[i];
					}
					num++;
				}
			}
			for (int j = globals.Count - 1; j >= num; j--)
			{
				globals.RemoveAt(j);
			}
		}

		// Token: 0x040025A2 RID: 9634
		private static readonly QilPatternVisitor.QilPatterns PatternsNoOpt;

		// Token: 0x040025A3 RID: 9635
		private static readonly QilPatternVisitor.QilPatterns PatternsOpt = new QilPatternVisitor.QilPatterns(141, true);

		// Token: 0x040025A4 RID: 9636
		private QilExpression qil;

		// Token: 0x040025A5 RID: 9637
		private XmlILElementAnalyzer elemAnalyzer;

		// Token: 0x040025A6 RID: 9638
		private XmlILStateAnalyzer contentAnalyzer;

		// Token: 0x040025A7 RID: 9639
		private XmlILNamespaceAnalyzer nmspAnalyzer;

		// Token: 0x040025A8 RID: 9640
		private XmlILOptimizerVisitor.NodeCounter nodeCounter = new XmlILOptimizerVisitor.NodeCounter();

		// Token: 0x040025A9 RID: 9641
		private SubstitutionList subs = new SubstitutionList();

		// Token: 0x020004B0 RID: 1200
		private class NodeCounter : QilVisitor
		{
			// Token: 0x06002F4D RID: 12109 RVA: 0x001193E4 File Offset: 0x001175E4
			public int Count(QilNode expr, QilNode target)
			{
				this.cnt = 0;
				this.target = target;
				this.Visit(expr);
				return this.cnt;
			}

			// Token: 0x06002F4E RID: 12110 RVA: 0x00119402 File Offset: 0x00117602
			protected override QilNode Visit(QilNode n)
			{
				if (n == null)
				{
					return null;
				}
				if (n == this.target)
				{
					this.cnt++;
				}
				return this.VisitChildren(n);
			}

			// Token: 0x06002F4F RID: 12111 RVA: 0x00119427 File Offset: 0x00117627
			protected override QilNode VisitReference(QilNode n)
			{
				if (n == this.target)
				{
					this.cnt++;
				}
				return n;
			}

			// Token: 0x06002F50 RID: 12112 RVA: 0x00119441 File Offset: 0x00117641
			public NodeCounter()
			{
			}

			// Token: 0x040025AA RID: 9642
			protected QilNode target;

			// Token: 0x040025AB RID: 9643
			protected int cnt;
		}

		// Token: 0x020004B1 RID: 1201
		private class NodeFinder : QilVisitor
		{
			// Token: 0x06002F51 RID: 12113 RVA: 0x00119449 File Offset: 0x00117649
			public bool Find(QilNode expr, QilNode target)
			{
				this.result = false;
				this.target = target;
				this.parent = null;
				this.VisitAssumeReference(expr);
				return this.result;
			}

			// Token: 0x06002F52 RID: 12114 RVA: 0x00119470 File Offset: 0x00117670
			protected override QilNode Visit(QilNode expr)
			{
				if (!this.result)
				{
					if (expr == this.target)
					{
						this.result = this.OnFound(expr);
					}
					if (!this.result)
					{
						QilNode qilNode = this.parent;
						this.parent = expr;
						this.VisitChildren(expr);
						this.parent = qilNode;
					}
				}
				return expr;
			}

			// Token: 0x06002F53 RID: 12115 RVA: 0x001194C1 File Offset: 0x001176C1
			protected override QilNode VisitReference(QilNode expr)
			{
				if (expr == this.target)
				{
					this.result = this.OnFound(expr);
				}
				return expr;
			}

			// Token: 0x06002F54 RID: 12116 RVA: 0x0001222F File Offset: 0x0001042F
			protected virtual bool OnFound(QilNode expr)
			{
				return true;
			}

			// Token: 0x06002F55 RID: 12117 RVA: 0x00119441 File Offset: 0x00117641
			public NodeFinder()
			{
			}

			// Token: 0x040025AC RID: 9644
			protected bool result;

			// Token: 0x040025AD RID: 9645
			protected QilNode target;

			// Token: 0x040025AE RID: 9646
			protected QilNode parent;
		}

		// Token: 0x020004B2 RID: 1202
		private class PositionOfFinder : XmlILOptimizerVisitor.NodeFinder
		{
			// Token: 0x06002F56 RID: 12118 RVA: 0x001194DA File Offset: 0x001176DA
			protected override bool OnFound(QilNode expr)
			{
				return this.parent != null && this.parent.NodeType == QilNodeType.PositionOf;
			}

			// Token: 0x06002F57 RID: 12119 RVA: 0x001194F5 File Offset: 0x001176F5
			public PositionOfFinder()
			{
			}
		}

		// Token: 0x020004B3 RID: 1203
		private class EqualityIndexVisitor : QilVisitor
		{
			// Token: 0x06002F58 RID: 12120 RVA: 0x001194FD File Offset: 0x001176FD
			public bool Scan(QilNode expr, QilNode ctxt, QilNode key)
			{
				this.result = true;
				this.ctxt = ctxt;
				this.key = key;
				this.Visit(expr);
				return this.result;
			}

			// Token: 0x06002F59 RID: 12121 RVA: 0x00119522 File Offset: 0x00117722
			protected override QilNode VisitReference(QilNode expr)
			{
				if (this.result && (expr == this.key || expr == this.ctxt))
				{
					this.result = false;
					return expr;
				}
				return expr;
			}

			// Token: 0x06002F5A RID: 12122 RVA: 0x00119548 File Offset: 0x00117748
			protected override QilNode VisitRoot(QilUnary root)
			{
				if (root.Child == this.ctxt)
				{
					return root;
				}
				return this.VisitChildren(root);
			}

			// Token: 0x06002F5B RID: 12123 RVA: 0x00119441 File Offset: 0x00117641
			public EqualityIndexVisitor()
			{
			}

			// Token: 0x040025AF RID: 9647
			protected bool result;

			// Token: 0x040025B0 RID: 9648
			protected QilNode ctxt;

			// Token: 0x040025B1 RID: 9649
			protected QilNode key;
		}
	}
}
