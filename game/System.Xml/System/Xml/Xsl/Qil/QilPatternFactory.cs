using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x020004C8 RID: 1224
	internal class QilPatternFactory
	{
		// Token: 0x06003113 RID: 12563 RVA: 0x00121D4A File Offset: 0x0011FF4A
		public QilPatternFactory(QilFactory f, bool debug)
		{
			this._f = f;
			this._debug = debug;
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x00121D60 File Offset: 0x0011FF60
		public QilFactory BaseFactory
		{
			get
			{
				return this._f;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x00121D68 File Offset: 0x0011FF68
		public bool IsDebug
		{
			get
			{
				return this._debug;
			}
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x00121D70 File Offset: 0x0011FF70
		public QilLiteral String(string val)
		{
			return this._f.LiteralString(val);
		}

		// Token: 0x06003117 RID: 12567 RVA: 0x00121D7E File Offset: 0x0011FF7E
		public QilLiteral Int32(int val)
		{
			return this._f.LiteralInt32(val);
		}

		// Token: 0x06003118 RID: 12568 RVA: 0x00121D8C File Offset: 0x0011FF8C
		public QilLiteral Double(double val)
		{
			return this._f.LiteralDouble(val);
		}

		// Token: 0x06003119 RID: 12569 RVA: 0x00121D9A File Offset: 0x0011FF9A
		public QilName QName(string local, string uri, string prefix)
		{
			return this._f.LiteralQName(local, uri, prefix);
		}

		// Token: 0x0600311A RID: 12570 RVA: 0x00121DAA File Offset: 0x0011FFAA
		public QilName QName(string local, string uri)
		{
			return this._f.LiteralQName(local, uri, string.Empty);
		}

		// Token: 0x0600311B RID: 12571 RVA: 0x00121DBE File Offset: 0x0011FFBE
		public QilName QName(string local)
		{
			return this._f.LiteralQName(local, string.Empty, string.Empty);
		}

		// Token: 0x0600311C RID: 12572 RVA: 0x00121DD6 File Offset: 0x0011FFD6
		public QilNode Unknown(XmlQueryType t)
		{
			return this._f.Unknown(t);
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x00121DE4 File Offset: 0x0011FFE4
		public QilExpression QilExpression(QilNode root, QilFactory factory)
		{
			return this._f.QilExpression(root, factory);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x00121DF3 File Offset: 0x0011FFF3
		public QilList FunctionList()
		{
			return this._f.FunctionList();
		}

		// Token: 0x0600311F RID: 12575 RVA: 0x00121E00 File Offset: 0x00120000
		public QilList GlobalVariableList()
		{
			return this._f.GlobalVariableList();
		}

		// Token: 0x06003120 RID: 12576 RVA: 0x00121E0D File Offset: 0x0012000D
		public QilList GlobalParameterList()
		{
			return this._f.GlobalParameterList();
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x00121E1A File Offset: 0x0012001A
		public QilList ActualParameterList()
		{
			return this._f.ActualParameterList();
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x00121E27 File Offset: 0x00120027
		public QilList ActualParameterList(QilNode arg1, QilNode arg2)
		{
			QilList qilList = this._f.ActualParameterList();
			qilList.Add(arg1);
			qilList.Add(arg2);
			return qilList;
		}

		// Token: 0x06003123 RID: 12579 RVA: 0x00121E42 File Offset: 0x00120042
		public QilList ActualParameterList(params QilNode[] args)
		{
			return this._f.ActualParameterList(args);
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x00121E50 File Offset: 0x00120050
		public QilList FormalParameterList()
		{
			return this._f.FormalParameterList();
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x00121E5D File Offset: 0x0012005D
		public QilList FormalParameterList(QilNode arg1, QilNode arg2)
		{
			QilList qilList = this._f.FormalParameterList();
			qilList.Add(arg1);
			qilList.Add(arg2);
			return qilList;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x00121E78 File Offset: 0x00120078
		public QilList FormalParameterList(params QilNode[] args)
		{
			return this._f.FormalParameterList(args);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x00121E86 File Offset: 0x00120086
		public QilList BranchList(params QilNode[] args)
		{
			return this._f.BranchList(args);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x00121E94 File Offset: 0x00120094
		public QilNode OptimizeBarrier(QilNode child)
		{
			return this._f.OptimizeBarrier(child);
		}

		// Token: 0x06003129 RID: 12585 RVA: 0x00121EA2 File Offset: 0x001200A2
		public QilNode DataSource(QilNode name, QilNode baseUri)
		{
			return this._f.DataSource(name, baseUri);
		}

		// Token: 0x0600312A RID: 12586 RVA: 0x00121EB1 File Offset: 0x001200B1
		public QilNode Nop(QilNode child)
		{
			return this._f.Nop(child);
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x00121EBF File Offset: 0x001200BF
		public QilNode Error(QilNode text)
		{
			return this._f.Error(text);
		}

		// Token: 0x0600312C RID: 12588 RVA: 0x00121ECD File Offset: 0x001200CD
		public QilNode Warning(QilNode text)
		{
			return this._f.Warning(text);
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x00121EDB File Offset: 0x001200DB
		public QilIterator For(QilNode binding)
		{
			return this._f.For(binding);
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x00121EE9 File Offset: 0x001200E9
		public QilIterator Let(QilNode binding)
		{
			return this._f.Let(binding);
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x00121EF7 File Offset: 0x001200F7
		public QilParameter Parameter(XmlQueryType t)
		{
			return this._f.Parameter(t);
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x00121F05 File Offset: 0x00120105
		public QilParameter Parameter(QilNode defaultValue, QilName name, XmlQueryType t)
		{
			return this._f.Parameter(defaultValue, name, t);
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x00121F15 File Offset: 0x00120115
		public QilNode PositionOf(QilIterator expr)
		{
			return this._f.PositionOf(expr);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x00121F23 File Offset: 0x00120123
		public QilNode True()
		{
			return this._f.True();
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x00121F30 File Offset: 0x00120130
		public QilNode False()
		{
			return this._f.False();
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x00121F3D File Offset: 0x0012013D
		public QilNode Boolean(bool b)
		{
			if (!b)
			{
				return this.False();
			}
			return this.True();
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x0000B528 File Offset: 0x00009728
		private static void CheckLogicArg(QilNode arg)
		{
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x00121F50 File Offset: 0x00120150
		public QilNode And(QilNode left, QilNode right)
		{
			QilPatternFactory.CheckLogicArg(left);
			QilPatternFactory.CheckLogicArg(right);
			if (!this._debug)
			{
				if (left.NodeType == QilNodeType.True || right.NodeType == QilNodeType.False)
				{
					return right;
				}
				if (left.NodeType == QilNodeType.False || right.NodeType == QilNodeType.True)
				{
					return left;
				}
			}
			return this._f.And(left, right);
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x00121FAC File Offset: 0x001201AC
		public QilNode Or(QilNode left, QilNode right)
		{
			QilPatternFactory.CheckLogicArg(left);
			QilPatternFactory.CheckLogicArg(right);
			if (!this._debug)
			{
				if (left.NodeType == QilNodeType.True || right.NodeType == QilNodeType.False)
				{
					return left;
				}
				if (left.NodeType == QilNodeType.False || right.NodeType == QilNodeType.True)
				{
					return right;
				}
			}
			return this._f.Or(left, right);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x00122008 File Offset: 0x00120208
		public QilNode Not(QilNode child)
		{
			if (!this._debug)
			{
				QilNodeType nodeType = child.NodeType;
				if (nodeType == QilNodeType.True)
				{
					return this._f.False();
				}
				if (nodeType == QilNodeType.False)
				{
					return this._f.True();
				}
				if (nodeType == QilNodeType.Not)
				{
					return ((QilUnary)child).Child;
				}
			}
			return this._f.Not(child);
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x00122068 File Offset: 0x00120268
		public QilNode Conditional(QilNode condition, QilNode trueBranch, QilNode falseBranch)
		{
			if (!this._debug)
			{
				QilNodeType nodeType = condition.NodeType;
				if (nodeType == QilNodeType.True)
				{
					return trueBranch;
				}
				if (nodeType == QilNodeType.False)
				{
					return falseBranch;
				}
				if (nodeType == QilNodeType.Not)
				{
					return this.Conditional(((QilUnary)condition).Child, falseBranch, trueBranch);
				}
			}
			return this._f.Conditional(condition, trueBranch, falseBranch);
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x001220BC File Offset: 0x001202BC
		public QilNode Choice(QilNode expr, QilList branches)
		{
			if (!this._debug)
			{
				int count = branches.Count;
				if (count == 1)
				{
					return this._f.Loop(this._f.Let(expr), branches[0]);
				}
				if (count == 2)
				{
					return this._f.Conditional(this._f.Eq(expr, this._f.LiteralInt32(0)), branches[0], branches[1]);
				}
			}
			return this._f.Choice(expr, branches);
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x00122140 File Offset: 0x00120340
		public QilNode Length(QilNode child)
		{
			return this._f.Length(child);
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x0012214E File Offset: 0x0012034E
		public QilNode Sequence()
		{
			return this._f.Sequence();
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x0012215B File Offset: 0x0012035B
		public QilNode Sequence(QilNode child)
		{
			if (!this._debug)
			{
				return child;
			}
			QilList qilList = this._f.Sequence();
			qilList.Add(child);
			return qilList;
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x00122179 File Offset: 0x00120379
		public QilNode Sequence(QilNode child1, QilNode child2)
		{
			QilList qilList = this._f.Sequence();
			qilList.Add(child1);
			qilList.Add(child2);
			return qilList;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x00122194 File Offset: 0x00120394
		public QilNode Sequence(params QilNode[] args)
		{
			if (!this._debug)
			{
				int i = args.Length;
				if (i == 0)
				{
					return this._f.Sequence();
				}
				if (i == 1)
				{
					return args[0];
				}
			}
			QilList qilList = this._f.Sequence();
			foreach (QilNode node in args)
			{
				qilList.Add(node);
			}
			return qilList;
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x001221EE File Offset: 0x001203EE
		public QilNode Union(QilNode left, QilNode right)
		{
			return this._f.Union(left, right);
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x001221FD File Offset: 0x001203FD
		public QilNode Sum(QilNode collection)
		{
			return this._f.Sum(collection);
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x0012220B File Offset: 0x0012040B
		public QilNode Negate(QilNode child)
		{
			return this._f.Negate(child);
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x00122219 File Offset: 0x00120419
		public QilNode Add(QilNode left, QilNode right)
		{
			return this._f.Add(left, right);
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x00122228 File Offset: 0x00120428
		public QilNode Subtract(QilNode left, QilNode right)
		{
			return this._f.Subtract(left, right);
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x00122237 File Offset: 0x00120437
		public QilNode Multiply(QilNode left, QilNode right)
		{
			return this._f.Multiply(left, right);
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x00122246 File Offset: 0x00120446
		public QilNode Divide(QilNode left, QilNode right)
		{
			return this._f.Divide(left, right);
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x00122255 File Offset: 0x00120455
		public QilNode Modulo(QilNode left, QilNode right)
		{
			return this._f.Modulo(left, right);
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x00122264 File Offset: 0x00120464
		public QilNode StrLength(QilNode str)
		{
			return this._f.StrLength(str);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x00122272 File Offset: 0x00120472
		public QilNode StrConcat(QilNode values)
		{
			if (!this._debug && values.XmlType.IsSingleton)
			{
				return values;
			}
			return this._f.StrConcat(values);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x00122297 File Offset: 0x00120497
		public QilNode StrConcat(params QilNode[] args)
		{
			return this.StrConcat(args);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x001222A0 File Offset: 0x001204A0
		public QilNode StrConcat(IList<QilNode> args)
		{
			if (!this._debug)
			{
				int count = args.Count;
				if (count == 0)
				{
					return this._f.LiteralString(string.Empty);
				}
				if (count == 1)
				{
					return this.StrConcat(args[0]);
				}
			}
			return this.StrConcat(this._f.Sequence(args));
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x001222F6 File Offset: 0x001204F6
		public QilNode StrParseQName(QilNode str, QilNode ns)
		{
			return this._f.StrParseQName(str, ns);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x00122305 File Offset: 0x00120505
		public QilNode Ne(QilNode left, QilNode right)
		{
			return this._f.Ne(left, right);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x00122314 File Offset: 0x00120514
		public QilNode Eq(QilNode left, QilNode right)
		{
			return this._f.Eq(left, right);
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x00122323 File Offset: 0x00120523
		public QilNode Gt(QilNode left, QilNode right)
		{
			return this._f.Gt(left, right);
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x00122332 File Offset: 0x00120532
		public QilNode Ge(QilNode left, QilNode right)
		{
			return this._f.Ge(left, right);
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x00122341 File Offset: 0x00120541
		public QilNode Lt(QilNode left, QilNode right)
		{
			return this._f.Lt(left, right);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x00122350 File Offset: 0x00120550
		public QilNode Le(QilNode left, QilNode right)
		{
			return this._f.Le(left, right);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x0012235F File Offset: 0x0012055F
		public QilNode Is(QilNode left, QilNode right)
		{
			return this._f.Is(left, right);
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x0012236E File Offset: 0x0012056E
		public QilNode Before(QilNode left, QilNode right)
		{
			return this._f.Before(left, right);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x0012237D File Offset: 0x0012057D
		public QilNode Loop(QilIterator variable, QilNode body)
		{
			if (!this._debug && body == variable.Binding)
			{
				return body;
			}
			return this._f.Loop(variable, body);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x0012239F File Offset: 0x0012059F
		public QilNode Filter(QilIterator variable, QilNode expr)
		{
			if (!this._debug && expr.NodeType == QilNodeType.True)
			{
				return variable.Binding;
			}
			return this._f.Filter(variable, expr);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x001223C7 File Offset: 0x001205C7
		public QilNode Sort(QilIterator iter, QilNode keys)
		{
			return this._f.Sort(iter, keys);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x001223D6 File Offset: 0x001205D6
		public QilSortKey SortKey(QilNode key, QilNode collation)
		{
			return this._f.SortKey(key, collation);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x001223E5 File Offset: 0x001205E5
		public QilNode DocOrderDistinct(QilNode collection)
		{
			if (collection.NodeType == QilNodeType.DocOrderDistinct)
			{
				return collection;
			}
			return this._f.DocOrderDistinct(collection);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x001223FF File Offset: 0x001205FF
		public QilFunction Function(QilList args, QilNode sideEffects, XmlQueryType resultType)
		{
			return this._f.Function(args, sideEffects, resultType);
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x0012240F File Offset: 0x0012060F
		public QilFunction Function(QilList args, QilNode defn, QilNode sideEffects)
		{
			return this._f.Function(args, defn, sideEffects, defn.XmlType);
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x00122425 File Offset: 0x00120625
		public QilNode Invoke(QilFunction func, QilList args)
		{
			return this._f.Invoke(func, args);
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x00122434 File Offset: 0x00120634
		public QilNode Content(QilNode context)
		{
			return this._f.Content(context);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x00122442 File Offset: 0x00120642
		public QilNode Parent(QilNode context)
		{
			return this._f.Parent(context);
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x00122450 File Offset: 0x00120650
		public QilNode Root(QilNode context)
		{
			return this._f.Root(context);
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x0012245E File Offset: 0x0012065E
		public QilNode XmlContext()
		{
			return this._f.XmlContext();
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x0012246B File Offset: 0x0012066B
		public QilNode Descendant(QilNode expr)
		{
			return this._f.Descendant(expr);
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x00122479 File Offset: 0x00120679
		public QilNode DescendantOrSelf(QilNode context)
		{
			return this._f.DescendantOrSelf(context);
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x00122487 File Offset: 0x00120687
		public QilNode Ancestor(QilNode expr)
		{
			return this._f.Ancestor(expr);
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x00122495 File Offset: 0x00120695
		public QilNode AncestorOrSelf(QilNode expr)
		{
			return this._f.AncestorOrSelf(expr);
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x001224A3 File Offset: 0x001206A3
		public QilNode Preceding(QilNode expr)
		{
			return this._f.Preceding(expr);
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x001224B1 File Offset: 0x001206B1
		public QilNode FollowingSibling(QilNode expr)
		{
			return this._f.FollowingSibling(expr);
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x001224BF File Offset: 0x001206BF
		public QilNode PrecedingSibling(QilNode expr)
		{
			return this._f.PrecedingSibling(expr);
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x001224CD File Offset: 0x001206CD
		public QilNode NodeRange(QilNode left, QilNode right)
		{
			return this._f.NodeRange(left, right);
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x001224DC File Offset: 0x001206DC
		public QilBinary Deref(QilNode context, QilNode id)
		{
			return this._f.Deref(context, id);
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x001224EB File Offset: 0x001206EB
		public QilNode ElementCtor(QilNode name, QilNode content)
		{
			return this._f.ElementCtor(name, content);
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x001224FA File Offset: 0x001206FA
		public QilNode AttributeCtor(QilNode name, QilNode val)
		{
			return this._f.AttributeCtor(name, val);
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x00122509 File Offset: 0x00120709
		public QilNode CommentCtor(QilNode content)
		{
			return this._f.CommentCtor(content);
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x00122517 File Offset: 0x00120717
		public QilNode PICtor(QilNode name, QilNode content)
		{
			return this._f.PICtor(name, content);
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00122526 File Offset: 0x00120726
		public QilNode TextCtor(QilNode content)
		{
			return this._f.TextCtor(content);
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00122534 File Offset: 0x00120734
		public QilNode RawTextCtor(QilNode content)
		{
			return this._f.RawTextCtor(content);
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x00122542 File Offset: 0x00120742
		public QilNode DocumentCtor(QilNode child)
		{
			return this._f.DocumentCtor(child);
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x00122550 File Offset: 0x00120750
		public QilNode NamespaceDecl(QilNode prefix, QilNode uri)
		{
			return this._f.NamespaceDecl(prefix, uri);
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x0012255F File Offset: 0x0012075F
		public QilNode RtfCtor(QilNode content, QilNode baseUri)
		{
			return this._f.RtfCtor(content, baseUri);
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x0012256E File Offset: 0x0012076E
		public QilNode NameOf(QilNode expr)
		{
			return this._f.NameOf(expr);
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x0012257C File Offset: 0x0012077C
		public QilNode LocalNameOf(QilNode expr)
		{
			return this._f.LocalNameOf(expr);
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x0012258A File Offset: 0x0012078A
		public QilNode NamespaceUriOf(QilNode expr)
		{
			return this._f.NamespaceUriOf(expr);
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x00122598 File Offset: 0x00120798
		public QilNode PrefixOf(QilNode expr)
		{
			return this._f.PrefixOf(expr);
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x001225A6 File Offset: 0x001207A6
		public QilNode TypeAssert(QilNode expr, XmlQueryType t)
		{
			return this._f.TypeAssert(expr, t);
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x001225B5 File Offset: 0x001207B5
		public QilNode IsType(QilNode expr, XmlQueryType t)
		{
			return this._f.IsType(expr, t);
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x001225C4 File Offset: 0x001207C4
		public QilNode IsEmpty(QilNode set)
		{
			return this._f.IsEmpty(set);
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x001225D2 File Offset: 0x001207D2
		public QilNode XPathNodeValue(QilNode expr)
		{
			return this._f.XPathNodeValue(expr);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x001225E0 File Offset: 0x001207E0
		public QilNode XPathFollowing(QilNode expr)
		{
			return this._f.XPathFollowing(expr);
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x001225EE File Offset: 0x001207EE
		public QilNode XPathNamespace(QilNode expr)
		{
			return this._f.XPathNamespace(expr);
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x001225FC File Offset: 0x001207FC
		public QilNode XPathPreceding(QilNode expr)
		{
			return this._f.XPathPreceding(expr);
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x0012260A File Offset: 0x0012080A
		public QilNode XsltGenerateId(QilNode expr)
		{
			return this._f.XsltGenerateId(expr);
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x00122618 File Offset: 0x00120818
		public QilNode XsltInvokeEarlyBound(QilNode name, MethodInfo d, XmlQueryType t, IList<QilNode> args)
		{
			QilList qilList = this._f.ActualParameterList();
			qilList.Add(args);
			return this._f.XsltInvokeEarlyBound(name, this._f.LiteralObject(d), qilList, t);
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x00122654 File Offset: 0x00120854
		public QilNode XsltInvokeLateBound(QilNode name, IList<QilNode> args)
		{
			QilList qilList = this._f.ActualParameterList();
			qilList.Add(args);
			return this._f.XsltInvokeLateBound(name, qilList);
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00122681 File Offset: 0x00120881
		public QilNode XsltCopy(QilNode expr, QilNode content)
		{
			return this._f.XsltCopy(expr, content);
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x00122690 File Offset: 0x00120890
		public QilNode XsltCopyOf(QilNode expr)
		{
			return this._f.XsltCopyOf(expr);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x0012269E File Offset: 0x0012089E
		public QilNode XsltConvert(QilNode expr, XmlQueryType t)
		{
			return this._f.XsltConvert(expr, t);
		}

		// Token: 0x04002642 RID: 9794
		private bool _debug;

		// Token: 0x04002643 RID: 9795
		private QilFactory _f;
	}
}
