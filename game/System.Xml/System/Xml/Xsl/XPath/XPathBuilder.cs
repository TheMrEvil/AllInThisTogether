using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x02000424 RID: 1060
	internal class XPathBuilder : IXPathBuilder<QilNode>, IXPathEnvironment, IFocus
	{
		// Token: 0x06002A31 RID: 10801 RVA: 0x000FE701 File Offset: 0x000FC901
		QilNode IFocus.GetCurrent()
		{
			return this.GetCurrentNode();
		}

		// Token: 0x06002A32 RID: 10802 RVA: 0x000FE709 File Offset: 0x000FC909
		QilNode IFocus.GetPosition()
		{
			return this.GetCurrentPosition();
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x000FE711 File Offset: 0x000FC911
		QilNode IFocus.GetLast()
		{
			return this.GetLastPosition();
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x000FE719 File Offset: 0x000FC919
		XPathQilFactory IXPathEnvironment.Factory
		{
			get
			{
				return this.f;
			}
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000FE721 File Offset: 0x000FC921
		QilNode IXPathEnvironment.ResolveVariable(string prefix, string name)
		{
			return this.Variable(prefix, name);
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0001DA42 File Offset: 0x0001BC42
		QilNode IXPathEnvironment.ResolveFunction(string prefix, string name, IList<QilNode> args, IFocus env)
		{
			return null;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000FE72B File Offset: 0x000FC92B
		string IXPathEnvironment.ResolvePrefix(string prefix)
		{
			return this.environment.ResolvePrefix(prefix);
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000FE73C File Offset: 0x000FC93C
		public XPathBuilder(IXPathEnvironment environment)
		{
			this.environment = environment;
			this.f = this.environment.Factory;
			this.fixupCurrent = this.f.Unknown(XmlQueryTypeFactory.NodeNotRtf);
			this.fixupPosition = this.f.Unknown(XmlQueryTypeFactory.DoubleX);
			this.fixupLast = this.f.Unknown(XmlQueryTypeFactory.DoubleX);
			this.fixupVisitor = new XPathBuilder.FixupVisitor(this.f, this.fixupCurrent, this.fixupPosition, this.fixupLast);
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000FE7CC File Offset: 0x000FC9CC
		public virtual void StartBuild()
		{
			this.inTheBuild = true;
			this.numFixupCurrent = (this.numFixupPosition = (this.numFixupLast = 0));
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000FE7FC File Offset: 0x000FC9FC
		public virtual QilNode EndBuild(QilNode result)
		{
			if (result == null)
			{
				this.inTheBuild = false;
				return result;
			}
			if (result.XmlType.MaybeMany && result.XmlType.IsNode && result.XmlType.IsNotRtf)
			{
				result = this.f.DocOrderDistinct(result);
			}
			result = this.fixupVisitor.Fixup(result, this.environment);
			this.numFixupCurrent -= this.fixupVisitor.numCurrent;
			this.numFixupPosition -= this.fixupVisitor.numPosition;
			this.numFixupLast -= this.fixupVisitor.numLast;
			this.inTheBuild = false;
			return result;
		}

		// Token: 0x06002A3B RID: 10811 RVA: 0x000FE8AE File Offset: 0x000FCAAE
		private QilNode GetCurrentNode()
		{
			this.numFixupCurrent++;
			return this.fixupCurrent;
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000FE8C4 File Offset: 0x000FCAC4
		private QilNode GetCurrentPosition()
		{
			this.numFixupPosition++;
			return this.fixupPosition;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000FE8DA File Offset: 0x000FCADA
		private QilNode GetLastPosition()
		{
			this.numFixupLast++;
			return this.fixupLast;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x000FE8F0 File Offset: 0x000FCAF0
		public virtual QilNode String(string value)
		{
			return this.f.String(value);
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x000FE8FE File Offset: 0x000FCAFE
		public virtual QilNode Number(double value)
		{
			return this.f.Double(value);
		}

		// Token: 0x06002A40 RID: 10816 RVA: 0x000FE90C File Offset: 0x000FCB0C
		public virtual QilNode Operator(XPathOperator op, QilNode left, QilNode right)
		{
			switch (XPathBuilder.OperatorGroup[(int)op])
			{
			case XPathBuilder.XPathOperatorGroup.Logical:
				return this.LogicalOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Equality:
				return this.EqualityOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Relational:
				return this.RelationalOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Arithmetic:
				return this.ArithmeticOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Negate:
				return this.NegateOperator(op, left, right);
			case XPathBuilder.XPathOperatorGroup.Union:
				return this.UnionOperator(op, left, right);
			default:
				return null;
			}
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x000FE980 File Offset: 0x000FCB80
		private QilNode LogicalOperator(XPathOperator op, QilNode left, QilNode right)
		{
			left = this.f.ConvertToBoolean(left);
			right = this.f.ConvertToBoolean(right);
			if (op != XPathOperator.Or)
			{
				return this.f.And(left, right);
			}
			return this.f.Or(left, right);
		}

		// Token: 0x06002A42 RID: 10818 RVA: 0x000FE9C0 File Offset: 0x000FCBC0
		private QilNode CompareValues(XPathOperator op, QilNode left, QilNode right, XmlTypeCode compType)
		{
			left = this.f.ConvertToType(compType, left);
			right = this.f.ConvertToType(compType, right);
			switch (op)
			{
			case XPathOperator.Eq:
				return this.f.Eq(left, right);
			case XPathOperator.Ne:
				return this.f.Ne(left, right);
			case XPathOperator.Lt:
				return this.f.Lt(left, right);
			case XPathOperator.Le:
				return this.f.Le(left, right);
			case XPathOperator.Gt:
				return this.f.Gt(left, right);
			case XPathOperator.Ge:
				return this.f.Ge(left, right);
			default:
				return null;
			}
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000FEA64 File Offset: 0x000FCC64
		private QilNode CompareNodeSetAndValue(XPathOperator op, QilNode nodeset, QilNode val, XmlTypeCode compType)
		{
			if (compType == XmlTypeCode.Boolean || nodeset.XmlType.IsSingleton)
			{
				return this.CompareValues(op, nodeset, val, compType);
			}
			QilIterator qilIterator = this.f.For(nodeset);
			return this.f.Not(this.f.IsEmpty(this.f.Filter(qilIterator, this.CompareValues(op, this.f.XPathNodeValue(qilIterator), val, compType))));
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000FEAD5 File Offset: 0x000FCCD5
		private static XPathOperator InvertOp(XPathOperator op)
		{
			if (op == XPathOperator.Lt)
			{
				return XPathOperator.Gt;
			}
			if (op == XPathOperator.Le)
			{
				return XPathOperator.Ge;
			}
			if (op == XPathOperator.Gt)
			{
				return XPathOperator.Lt;
			}
			if (op != XPathOperator.Ge)
			{
				return op;
			}
			return XPathOperator.Le;
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000FEAF0 File Offset: 0x000FCCF0
		private QilNode CompareNodeSetAndNodeSet(XPathOperator op, QilNode left, QilNode right, XmlTypeCode compType)
		{
			if (right.XmlType.IsSingleton)
			{
				return this.CompareNodeSetAndValue(op, left, right, compType);
			}
			if (left.XmlType.IsSingleton)
			{
				op = XPathBuilder.InvertOp(op);
				return this.CompareNodeSetAndValue(op, right, left, compType);
			}
			QilIterator qilIterator = this.f.For(left);
			QilIterator qilIterator2 = this.f.For(right);
			return this.f.Not(this.f.IsEmpty(this.f.Loop(qilIterator, this.f.Filter(qilIterator2, this.CompareValues(op, this.f.XPathNodeValue(qilIterator), this.f.XPathNodeValue(qilIterator2), compType)))));
		}

		// Token: 0x06002A46 RID: 10822 RVA: 0x000FEBA0 File Offset: 0x000FCDA0
		private QilNode EqualityOperator(XPathOperator op, QilNode left, QilNode right)
		{
			XmlQueryType xmlType = left.XmlType;
			XmlQueryType xmlType2 = right.XmlType;
			if (this.f.IsAnyType(left) || this.f.IsAnyType(right))
			{
				return this.f.InvokeEqualityOperator(XPathBuilder.QilOperator[(int)op], left, right);
			}
			if (xmlType.IsNode && xmlType2.IsNode)
			{
				return this.CompareNodeSetAndNodeSet(op, left, right, XmlTypeCode.String);
			}
			if (xmlType.IsNode)
			{
				return this.CompareNodeSetAndValue(op, left, right, xmlType2.TypeCode);
			}
			if (xmlType2.IsNode)
			{
				return this.CompareNodeSetAndValue(op, right, left, xmlType.TypeCode);
			}
			XmlTypeCode compType = (xmlType.TypeCode == XmlTypeCode.Boolean || xmlType2.TypeCode == XmlTypeCode.Boolean) ? XmlTypeCode.Boolean : ((xmlType.TypeCode == XmlTypeCode.Double || xmlType2.TypeCode == XmlTypeCode.Double) ? XmlTypeCode.Double : XmlTypeCode.String);
			return this.CompareValues(op, left, right, compType);
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x000FEC78 File Offset: 0x000FCE78
		private QilNode RelationalOperator(XPathOperator op, QilNode left, QilNode right)
		{
			XmlQueryType xmlType = left.XmlType;
			XmlQueryType xmlType2 = right.XmlType;
			if (this.f.IsAnyType(left) || this.f.IsAnyType(right))
			{
				return this.f.InvokeRelationalOperator(XPathBuilder.QilOperator[(int)op], left, right);
			}
			if (xmlType.IsNode && xmlType2.IsNode)
			{
				return this.CompareNodeSetAndNodeSet(op, left, right, XmlTypeCode.Double);
			}
			if (xmlType.IsNode)
			{
				XmlTypeCode compType = (xmlType2.TypeCode == XmlTypeCode.Boolean) ? XmlTypeCode.Boolean : XmlTypeCode.Double;
				return this.CompareNodeSetAndValue(op, left, right, compType);
			}
			if (xmlType2.IsNode)
			{
				XmlTypeCode compType2 = (xmlType.TypeCode == XmlTypeCode.Boolean) ? XmlTypeCode.Boolean : XmlTypeCode.Double;
				op = XPathBuilder.InvertOp(op);
				return this.CompareNodeSetAndValue(op, right, left, compType2);
			}
			return this.CompareValues(op, left, right, XmlTypeCode.Double);
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x000FED3B File Offset: 0x000FCF3B
		private QilNode NegateOperator(XPathOperator op, QilNode left, QilNode right)
		{
			return this.f.Negate(this.f.ConvertToNumber(left));
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x000FED54 File Offset: 0x000FCF54
		private QilNode ArithmeticOperator(XPathOperator op, QilNode left, QilNode right)
		{
			left = this.f.ConvertToNumber(left);
			right = this.f.ConvertToNumber(right);
			switch (op)
			{
			case XPathOperator.Plus:
				return this.f.Add(left, right);
			case XPathOperator.Minus:
				return this.f.Subtract(left, right);
			case XPathOperator.Multiply:
				return this.f.Multiply(left, right);
			case XPathOperator.Divide:
				return this.f.Divide(left, right);
			case XPathOperator.Modulo:
				return this.f.Modulo(left, right);
			default:
				return null;
			}
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x000FEDE4 File Offset: 0x000FCFE4
		private QilNode UnionOperator(XPathOperator op, QilNode left, QilNode right)
		{
			if (left == null)
			{
				return this.f.EnsureNodeSet(right);
			}
			left = this.f.EnsureNodeSet(left);
			right = this.f.EnsureNodeSet(right);
			if (left.NodeType == QilNodeType.Sequence)
			{
				((QilList)left).Add(right);
				return left;
			}
			return this.f.Union(left, right);
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x000FEE42 File Offset: 0x000FD042
		public static XmlNodeKindFlags AxisTypeMask(XmlNodeKindFlags inputTypeMask, XPathNodeType nodeType, XPathAxis xpathAxis)
		{
			return inputTypeMask & XPathBuilder.XPathNodeType2QilXmlNodeKind[(int)nodeType] & XPathBuilder.XPathAxisMask[(int)xpathAxis];
		}

		// Token: 0x06002A4C RID: 10828 RVA: 0x000FEE58 File Offset: 0x000FD058
		private QilNode BuildAxisFilter(QilNode qilAxis, XPathAxis xpathAxis, XPathNodeType nodeType, string name, string nsUri)
		{
			XmlNodeKindFlags nodeKinds = qilAxis.XmlType.NodeKinds;
			XmlNodeKindFlags xmlNodeKindFlags = XPathBuilder.AxisTypeMask(nodeKinds, nodeType, xpathAxis);
			if (xmlNodeKindFlags == XmlNodeKindFlags.None)
			{
				return this.f.Sequence();
			}
			QilIterator expr;
			if (xmlNodeKindFlags != nodeKinds)
			{
				qilAxis = this.f.Filter(expr = this.f.For(qilAxis), this.f.IsType(expr, XmlQueryTypeFactory.NodeChoice(xmlNodeKindFlags)));
				qilAxis.XmlType = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeChoice(xmlNodeKindFlags), qilAxis.XmlType.Cardinality);
				if (qilAxis.NodeType == QilNodeType.Filter)
				{
					QilLoop qilLoop = (QilLoop)qilAxis;
					qilLoop.Body = this.f.And(qilLoop.Body, (name != null && nsUri != null) ? this.f.Eq(this.f.NameOf(expr), this.f.QName(name, nsUri)) : ((nsUri != null) ? this.f.Eq(this.f.NamespaceUriOf(expr), this.f.String(nsUri)) : ((name != null) ? this.f.Eq(this.f.LocalNameOf(expr), this.f.String(name)) : this.f.True())));
					return qilLoop;
				}
			}
			return this.f.Filter(expr = this.f.For(qilAxis), (name != null && nsUri != null) ? this.f.Eq(this.f.NameOf(expr), this.f.QName(name, nsUri)) : ((nsUri != null) ? this.f.Eq(this.f.NamespaceUriOf(expr), this.f.String(nsUri)) : ((name != null) ? this.f.Eq(this.f.LocalNameOf(expr), this.f.String(name)) : this.f.True())));
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x000FF040 File Offset: 0x000FD240
		private QilNode BuildAxis(XPathAxis xpathAxis, XPathNodeType nodeType, string nsUri, string name)
		{
			QilNode currentNode = this.GetCurrentNode();
			QilNode qilAxis;
			switch (xpathAxis)
			{
			case XPathAxis.Ancestor:
				qilAxis = this.f.Ancestor(currentNode);
				break;
			case XPathAxis.AncestorOrSelf:
				qilAxis = this.f.AncestorOrSelf(currentNode);
				break;
			case XPathAxis.Attribute:
				qilAxis = this.f.Content(currentNode);
				break;
			case XPathAxis.Child:
				qilAxis = this.f.Content(currentNode);
				break;
			case XPathAxis.Descendant:
				qilAxis = this.f.Descendant(currentNode);
				break;
			case XPathAxis.DescendantOrSelf:
				qilAxis = this.f.DescendantOrSelf(currentNode);
				break;
			case XPathAxis.Following:
				qilAxis = this.f.XPathFollowing(currentNode);
				break;
			case XPathAxis.FollowingSibling:
				qilAxis = this.f.FollowingSibling(currentNode);
				break;
			case XPathAxis.Namespace:
				qilAxis = this.f.XPathNamespace(currentNode);
				break;
			case XPathAxis.Parent:
				qilAxis = this.f.Parent(currentNode);
				break;
			case XPathAxis.Preceding:
				qilAxis = this.f.XPathPreceding(currentNode);
				break;
			case XPathAxis.PrecedingSibling:
				qilAxis = this.f.PrecedingSibling(currentNode);
				break;
			case XPathAxis.Self:
				qilAxis = currentNode;
				break;
			case XPathAxis.Root:
				return this.f.Root(currentNode);
			default:
				qilAxis = null;
				break;
			}
			QilNode qilNode = this.BuildAxisFilter(qilAxis, xpathAxis, nodeType, name, nsUri);
			if (xpathAxis == XPathAxis.Ancestor || xpathAxis == XPathAxis.Preceding || xpathAxis == XPathAxis.AncestorOrSelf || xpathAxis == XPathAxis.PrecedingSibling)
			{
				qilNode = this.f.BaseFactory.DocOrderDistinct(qilNode);
			}
			return qilNode;
		}

		// Token: 0x06002A4E RID: 10830 RVA: 0x000FF1A0 File Offset: 0x000FD3A0
		public virtual QilNode Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name)
		{
			string nsUri = (prefix == null) ? null : this.environment.ResolvePrefix(prefix);
			return this.BuildAxis(xpathAxis, nodeType, nsUri, name);
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x000FF1CC File Offset: 0x000FD3CC
		public virtual QilNode JoinStep(QilNode left, QilNode right)
		{
			QilIterator qilIterator = this.f.For(this.f.EnsureNodeSet(left));
			right = this.fixupVisitor.Fixup(right, qilIterator, null);
			this.numFixupCurrent -= this.fixupVisitor.numCurrent;
			this.numFixupPosition -= this.fixupVisitor.numPosition;
			this.numFixupLast -= this.fixupVisitor.numLast;
			return this.f.DocOrderDistinct(this.f.Loop(qilIterator, right));
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x000FF264 File Offset: 0x000FD464
		public virtual QilNode Predicate(QilNode nodeset, QilNode predicate, bool isReverseStep)
		{
			if (isReverseStep)
			{
				nodeset = ((QilUnary)nodeset).Child;
			}
			predicate = XPathBuilder.PredicateToBoolean(predicate, this.f, this);
			return XPathBuilder.BuildOnePredicate(nodeset, predicate, isReverseStep, this.f, this.fixupVisitor, ref this.numFixupCurrent, ref this.numFixupPosition, ref this.numFixupLast);
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x000FF2B8 File Offset: 0x000FD4B8
		public static QilNode PredicateToBoolean(QilNode predicate, XPathQilFactory f, IXPathEnvironment env)
		{
			if (!f.IsAnyType(predicate))
			{
				if (predicate.XmlType.TypeCode == XmlTypeCode.Double)
				{
					predicate = f.Eq(env.GetPosition(), predicate);
				}
				else
				{
					predicate = f.ConvertToBoolean(predicate);
				}
			}
			else
			{
				QilIterator qilIterator;
				predicate = f.Loop(qilIterator = f.Let(predicate), f.Conditional(f.IsType(qilIterator, XmlQueryTypeFactory.Double), f.Eq(env.GetPosition(), f.TypeAssert(qilIterator, XmlQueryTypeFactory.DoubleX)), f.ConvertToBoolean(qilIterator)));
			}
			return predicate;
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x000FF33C File Offset: 0x000FD53C
		public static QilNode BuildOnePredicate(QilNode nodeset, QilNode predicate, bool isReverseStep, XPathQilFactory f, XPathBuilder.FixupVisitor fixupVisitor, ref int numFixupCurrent, ref int numFixupPosition, ref int numFixupLast)
		{
			nodeset = f.EnsureNodeSet(nodeset);
			QilNode qilNode;
			if (numFixupLast != 0 && fixupVisitor.CountUnfixedLast(predicate) != 0)
			{
				QilIterator qilIterator = f.Let(nodeset);
				QilIterator qilIterator2 = f.Let(f.XsltConvert(f.Length(qilIterator), XmlQueryTypeFactory.DoubleX));
				QilIterator qilIterator3 = f.For(qilIterator);
				predicate = fixupVisitor.Fixup(predicate, qilIterator3, qilIterator2);
				numFixupCurrent -= fixupVisitor.numCurrent;
				numFixupPosition -= fixupVisitor.numPosition;
				numFixupLast -= fixupVisitor.numLast;
				qilNode = f.Loop(qilIterator, f.Loop(qilIterator2, f.Filter(qilIterator3, predicate)));
			}
			else
			{
				QilIterator qilIterator4 = f.For(nodeset);
				predicate = fixupVisitor.Fixup(predicate, qilIterator4, null);
				numFixupCurrent -= fixupVisitor.numCurrent;
				numFixupPosition -= fixupVisitor.numPosition;
				numFixupLast -= fixupVisitor.numLast;
				qilNode = f.Filter(qilIterator4, predicate);
			}
			if (isReverseStep)
			{
				qilNode = f.DocOrderDistinct(qilNode);
			}
			return qilNode;
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x000FF432 File Offset: 0x000FD632
		public virtual QilNode Variable(string prefix, string name)
		{
			return this.environment.ResolveVariable(prefix, name);
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x000FF444 File Offset: 0x000FD644
		public virtual QilNode Function(string prefix, string name, IList<QilNode> args)
		{
			XPathBuilder.FunctionInfo<XPathBuilder.FuncId> functionInfo;
			if (prefix.Length != 0 || !XPathBuilder.FunctionTable.TryGetValue(name, out functionInfo))
			{
				return this.environment.ResolveFunction(prefix, name, args, this);
			}
			functionInfo.CastArguments(args, name, this.f);
			switch (functionInfo.id)
			{
			case XPathBuilder.FuncId.Last:
				return this.GetLastPosition();
			case XPathBuilder.FuncId.Position:
				return this.GetCurrentPosition();
			case XPathBuilder.FuncId.Count:
				return this.f.XsltConvert(this.f.Length(this.f.DocOrderDistinct(args[0])), XmlQueryTypeFactory.DoubleX);
			case XPathBuilder.FuncId.LocalName:
				if (args.Count != 0)
				{
					return this.LocalNameOfFirstNode(args[0]);
				}
				return this.f.LocalNameOf(this.GetCurrentNode());
			case XPathBuilder.FuncId.NamespaceUri:
				if (args.Count != 0)
				{
					return this.NamespaceOfFirstNode(args[0]);
				}
				return this.f.NamespaceUriOf(this.GetCurrentNode());
			case XPathBuilder.FuncId.Name:
				if (args.Count != 0)
				{
					return this.NameOfFirstNode(args[0]);
				}
				return this.NameOf(this.GetCurrentNode());
			case XPathBuilder.FuncId.String:
				if (args.Count != 0)
				{
					return this.f.ConvertToString(args[0]);
				}
				return this.f.XPathNodeValue(this.GetCurrentNode());
			case XPathBuilder.FuncId.Number:
				if (args.Count != 0)
				{
					return this.f.ConvertToNumber(args[0]);
				}
				return this.f.XsltConvert(this.f.XPathNodeValue(this.GetCurrentNode()), XmlQueryTypeFactory.DoubleX);
			case XPathBuilder.FuncId.Boolean:
				return this.f.ConvertToBoolean(args[0]);
			case XPathBuilder.FuncId.True:
				return this.f.True();
			case XPathBuilder.FuncId.False:
				return this.f.False();
			case XPathBuilder.FuncId.Not:
				return this.f.Not(args[0]);
			case XPathBuilder.FuncId.Id:
				return this.f.DocOrderDistinct(this.f.Id(this.GetCurrentNode(), args[0]));
			case XPathBuilder.FuncId.Concat:
				return this.f.StrConcat(args);
			case XPathBuilder.FuncId.StartsWith:
				return this.f.InvokeStartsWith(args[0], args[1]);
			case XPathBuilder.FuncId.Contains:
				return this.f.InvokeContains(args[0], args[1]);
			case XPathBuilder.FuncId.SubstringBefore:
				return this.f.InvokeSubstringBefore(args[0], args[1]);
			case XPathBuilder.FuncId.SubstringAfter:
				return this.f.InvokeSubstringAfter(args[0], args[1]);
			case XPathBuilder.FuncId.Substring:
				if (args.Count != 2)
				{
					return this.f.InvokeSubstring(args[0], args[1], args[2]);
				}
				return this.f.InvokeSubstring(args[0], args[1]);
			case XPathBuilder.FuncId.StringLength:
				return this.f.XsltConvert(this.f.StrLength((args.Count == 0) ? this.f.XPathNodeValue(this.GetCurrentNode()) : args[0]), XmlQueryTypeFactory.DoubleX);
			case XPathBuilder.FuncId.Normalize:
				return this.f.InvokeNormalizeSpace((args.Count == 0) ? this.f.XPathNodeValue(this.GetCurrentNode()) : args[0]);
			case XPathBuilder.FuncId.Translate:
				return this.f.InvokeTranslate(args[0], args[1], args[2]);
			case XPathBuilder.FuncId.Lang:
				return this.f.InvokeLang(args[0], this.GetCurrentNode());
			case XPathBuilder.FuncId.Sum:
				return this.Sum(this.f.DocOrderDistinct(args[0]));
			case XPathBuilder.FuncId.Floor:
				return this.f.InvokeFloor(args[0]);
			case XPathBuilder.FuncId.Ceiling:
				return this.f.InvokeCeiling(args[0]);
			case XPathBuilder.FuncId.Round:
				return this.f.InvokeRound(args[0]);
			default:
				return null;
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x000FF838 File Offset: 0x000FDA38
		private QilNode LocalNameOfFirstNode(QilNode arg)
		{
			if (arg.XmlType.IsSingleton)
			{
				return this.f.LocalNameOf(arg);
			}
			QilIterator expr;
			return this.f.StrConcat(this.f.Loop(expr = this.f.FirstNode(arg), this.f.LocalNameOf(expr)));
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x000FF890 File Offset: 0x000FDA90
		private QilNode NamespaceOfFirstNode(QilNode arg)
		{
			if (arg.XmlType.IsSingleton)
			{
				return this.f.NamespaceUriOf(arg);
			}
			QilIterator expr;
			return this.f.StrConcat(this.f.Loop(expr = this.f.FirstNode(arg), this.f.NamespaceUriOf(expr)));
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x000FF8E8 File Offset: 0x000FDAE8
		private QilNode NameOf(QilNode arg)
		{
			if (arg is QilIterator)
			{
				QilIterator qilIterator;
				QilIterator qilIterator2;
				return this.f.Loop(qilIterator = this.f.Let(this.f.PrefixOf(arg)), this.f.Loop(qilIterator2 = this.f.Let(this.f.LocalNameOf(arg)), this.f.Conditional(this.f.Eq(this.f.StrLength(qilIterator), this.f.Int32(0)), qilIterator2, this.f.StrConcat(new QilNode[]
				{
					qilIterator,
					this.f.String(":"),
					qilIterator2
				}))));
			}
			QilIterator qilIterator3 = this.f.Let(arg);
			return this.f.Loop(qilIterator3, this.NameOf(qilIterator3));
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x000FF9C4 File Offset: 0x000FDBC4
		private QilNode NameOfFirstNode(QilNode arg)
		{
			if (arg.XmlType.IsSingleton)
			{
				return this.NameOf(arg);
			}
			QilIterator arg2;
			return this.f.StrConcat(this.f.Loop(arg2 = this.f.FirstNode(arg), this.NameOf(arg2)));
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x000FFA14 File Offset: 0x000FDC14
		private QilNode Sum(QilNode arg)
		{
			QilIterator n;
			return this.f.Sum(this.f.Sequence(this.f.Double(0.0), this.f.Loop(n = this.f.For(arg), this.f.ConvertToNumber(n))));
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x000FFA70 File Offset: 0x000FDC70
		private static Dictionary<string, XPathBuilder.FunctionInfo<XPathBuilder.FuncId>> CreateFunctionTable()
		{
			return new Dictionary<string, XPathBuilder.FunctionInfo<XPathBuilder.FuncId>>(36)
			{
				{
					"last",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Last, 0, 0, null)
				},
				{
					"position",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Position, 0, 0, null)
				},
				{
					"name",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Name, 0, 1, XPathBuilder.argNodeSet)
				},
				{
					"namespace-uri",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.NamespaceUri, 0, 1, XPathBuilder.argNodeSet)
				},
				{
					"local-name",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.LocalName, 0, 1, XPathBuilder.argNodeSet)
				},
				{
					"count",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Count, 1, 1, XPathBuilder.argNodeSet)
				},
				{
					"id",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Id, 1, 1, XPathBuilder.argAny)
				},
				{
					"string",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.String, 0, 1, XPathBuilder.argAny)
				},
				{
					"concat",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Concat, 2, int.MaxValue, null)
				},
				{
					"starts-with",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.StartsWith, 2, 2, XPathBuilder.argString2)
				},
				{
					"contains",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Contains, 2, 2, XPathBuilder.argString2)
				},
				{
					"substring-before",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.SubstringBefore, 2, 2, XPathBuilder.argString2)
				},
				{
					"substring-after",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.SubstringAfter, 2, 2, XPathBuilder.argString2)
				},
				{
					"substring",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Substring, 2, 3, XPathBuilder.argFnSubstr)
				},
				{
					"string-length",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.StringLength, 0, 1, XPathBuilder.argString)
				},
				{
					"normalize-space",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Normalize, 0, 1, XPathBuilder.argString)
				},
				{
					"translate",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Translate, 3, 3, XPathBuilder.argString3)
				},
				{
					"boolean",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Boolean, 1, 1, XPathBuilder.argAny)
				},
				{
					"not",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Not, 1, 1, XPathBuilder.argBoolean)
				},
				{
					"true",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.True, 0, 0, null)
				},
				{
					"false",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.False, 0, 0, null)
				},
				{
					"lang",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Lang, 1, 1, XPathBuilder.argString)
				},
				{
					"number",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Number, 0, 1, XPathBuilder.argAny)
				},
				{
					"sum",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Sum, 1, 1, XPathBuilder.argNodeSet)
				},
				{
					"floor",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Floor, 1, 1, XPathBuilder.argDouble)
				},
				{
					"ceiling",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Ceiling, 1, 1, XPathBuilder.argDouble)
				},
				{
					"round",
					new XPathBuilder.FunctionInfo<XPathBuilder.FuncId>(XPathBuilder.FuncId.Round, 1, 1, XPathBuilder.argDouble)
				}
			};
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x000FFD0E File Offset: 0x000FDF0E
		public static bool IsFunctionAvailable(string localName, string nsUri)
		{
			return nsUri.Length == 0 && XPathBuilder.FunctionTable.ContainsKey(localName);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x000FFD28 File Offset: 0x000FDF28
		// Note: this type is marked as 'beforefieldinit'.
		static XPathBuilder()
		{
		}

		// Token: 0x040020FE RID: 8446
		private XPathQilFactory f;

		// Token: 0x040020FF RID: 8447
		private IXPathEnvironment environment;

		// Token: 0x04002100 RID: 8448
		private bool inTheBuild;

		// Token: 0x04002101 RID: 8449
		protected QilNode fixupCurrent;

		// Token: 0x04002102 RID: 8450
		protected QilNode fixupPosition;

		// Token: 0x04002103 RID: 8451
		protected QilNode fixupLast;

		// Token: 0x04002104 RID: 8452
		protected int numFixupCurrent;

		// Token: 0x04002105 RID: 8453
		protected int numFixupPosition;

		// Token: 0x04002106 RID: 8454
		protected int numFixupLast;

		// Token: 0x04002107 RID: 8455
		private XPathBuilder.FixupVisitor fixupVisitor;

		// Token: 0x04002108 RID: 8456
		private static XmlNodeKindFlags[] XPathNodeType2QilXmlNodeKind = new XmlNodeKindFlags[]
		{
			XmlNodeKindFlags.Document,
			XmlNodeKindFlags.Element,
			XmlNodeKindFlags.Attribute,
			XmlNodeKindFlags.Namespace,
			XmlNodeKindFlags.Text,
			XmlNodeKindFlags.Text,
			XmlNodeKindFlags.Text,
			XmlNodeKindFlags.PI,
			XmlNodeKindFlags.Comment,
			XmlNodeKindFlags.Any
		};

		// Token: 0x04002109 RID: 8457
		private static XPathBuilder.XPathOperatorGroup[] OperatorGroup = new XPathBuilder.XPathOperatorGroup[]
		{
			XPathBuilder.XPathOperatorGroup.Unknown,
			XPathBuilder.XPathOperatorGroup.Logical,
			XPathBuilder.XPathOperatorGroup.Logical,
			XPathBuilder.XPathOperatorGroup.Equality,
			XPathBuilder.XPathOperatorGroup.Equality,
			XPathBuilder.XPathOperatorGroup.Relational,
			XPathBuilder.XPathOperatorGroup.Relational,
			XPathBuilder.XPathOperatorGroup.Relational,
			XPathBuilder.XPathOperatorGroup.Relational,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Arithmetic,
			XPathBuilder.XPathOperatorGroup.Negate,
			XPathBuilder.XPathOperatorGroup.Union
		};

		// Token: 0x0400210A RID: 8458
		private static QilNodeType[] QilOperator = new QilNodeType[]
		{
			QilNodeType.Unknown,
			QilNodeType.Or,
			QilNodeType.And,
			QilNodeType.Eq,
			QilNodeType.Ne,
			QilNodeType.Lt,
			QilNodeType.Le,
			QilNodeType.Gt,
			QilNodeType.Ge,
			QilNodeType.Add,
			QilNodeType.Subtract,
			QilNodeType.Multiply,
			QilNodeType.Divide,
			QilNodeType.Modulo,
			QilNodeType.Negate,
			QilNodeType.Sequence
		};

		// Token: 0x0400210B RID: 8459
		private static XmlNodeKindFlags[] XPathAxisMask = new XmlNodeKindFlags[]
		{
			XmlNodeKindFlags.None,
			XmlNodeKindFlags.Document | XmlNodeKindFlags.Element,
			XmlNodeKindFlags.Any,
			XmlNodeKindFlags.Attribute,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Any,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Namespace,
			XmlNodeKindFlags.Document | XmlNodeKindFlags.Element,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Content,
			XmlNodeKindFlags.Any,
			XmlNodeKindFlags.Document
		};

		// Token: 0x0400210C RID: 8460
		public static readonly XmlTypeCode[] argAny = new XmlTypeCode[]
		{
			XmlTypeCode.Item
		};

		// Token: 0x0400210D RID: 8461
		public static readonly XmlTypeCode[] argNodeSet = new XmlTypeCode[]
		{
			XmlTypeCode.Node
		};

		// Token: 0x0400210E RID: 8462
		public static readonly XmlTypeCode[] argBoolean = new XmlTypeCode[]
		{
			XmlTypeCode.Boolean
		};

		// Token: 0x0400210F RID: 8463
		public static readonly XmlTypeCode[] argDouble = new XmlTypeCode[]
		{
			XmlTypeCode.Double
		};

		// Token: 0x04002110 RID: 8464
		public static readonly XmlTypeCode[] argString = new XmlTypeCode[]
		{
			XmlTypeCode.String
		};

		// Token: 0x04002111 RID: 8465
		public static readonly XmlTypeCode[] argString2 = new XmlTypeCode[]
		{
			XmlTypeCode.String,
			XmlTypeCode.String
		};

		// Token: 0x04002112 RID: 8466
		public static readonly XmlTypeCode[] argString3 = new XmlTypeCode[]
		{
			XmlTypeCode.String,
			XmlTypeCode.String,
			XmlTypeCode.String
		};

		// Token: 0x04002113 RID: 8467
		public static readonly XmlTypeCode[] argFnSubstr = new XmlTypeCode[]
		{
			XmlTypeCode.String,
			XmlTypeCode.Double,
			XmlTypeCode.Double
		};

		// Token: 0x04002114 RID: 8468
		public static Dictionary<string, XPathBuilder.FunctionInfo<XPathBuilder.FuncId>> FunctionTable = XPathBuilder.CreateFunctionTable();

		// Token: 0x02000425 RID: 1061
		private enum XPathOperatorGroup
		{
			// Token: 0x04002116 RID: 8470
			Unknown,
			// Token: 0x04002117 RID: 8471
			Logical,
			// Token: 0x04002118 RID: 8472
			Equality,
			// Token: 0x04002119 RID: 8473
			Relational,
			// Token: 0x0400211A RID: 8474
			Arithmetic,
			// Token: 0x0400211B RID: 8475
			Negate,
			// Token: 0x0400211C RID: 8476
			Union
		}

		// Token: 0x02000426 RID: 1062
		internal enum FuncId
		{
			// Token: 0x0400211E RID: 8478
			Last,
			// Token: 0x0400211F RID: 8479
			Position,
			// Token: 0x04002120 RID: 8480
			Count,
			// Token: 0x04002121 RID: 8481
			LocalName,
			// Token: 0x04002122 RID: 8482
			NamespaceUri,
			// Token: 0x04002123 RID: 8483
			Name,
			// Token: 0x04002124 RID: 8484
			String,
			// Token: 0x04002125 RID: 8485
			Number,
			// Token: 0x04002126 RID: 8486
			Boolean,
			// Token: 0x04002127 RID: 8487
			True,
			// Token: 0x04002128 RID: 8488
			False,
			// Token: 0x04002129 RID: 8489
			Not,
			// Token: 0x0400212A RID: 8490
			Id,
			// Token: 0x0400212B RID: 8491
			Concat,
			// Token: 0x0400212C RID: 8492
			StartsWith,
			// Token: 0x0400212D RID: 8493
			Contains,
			// Token: 0x0400212E RID: 8494
			SubstringBefore,
			// Token: 0x0400212F RID: 8495
			SubstringAfter,
			// Token: 0x04002130 RID: 8496
			Substring,
			// Token: 0x04002131 RID: 8497
			StringLength,
			// Token: 0x04002132 RID: 8498
			Normalize,
			// Token: 0x04002133 RID: 8499
			Translate,
			// Token: 0x04002134 RID: 8500
			Lang,
			// Token: 0x04002135 RID: 8501
			Sum,
			// Token: 0x04002136 RID: 8502
			Floor,
			// Token: 0x04002137 RID: 8503
			Ceiling,
			// Token: 0x04002138 RID: 8504
			Round
		}

		// Token: 0x02000427 RID: 1063
		internal class FixupVisitor : QilReplaceVisitor
		{
			// Token: 0x06002A5D RID: 10845 RVA: 0x000FFE2A File Offset: 0x000FE02A
			public FixupVisitor(QilPatternFactory f, QilNode fixupCurrent, QilNode fixupPosition, QilNode fixupLast) : base(f.BaseFactory)
			{
				this.f = f;
				this.fixupCurrent = fixupCurrent;
				this.fixupPosition = fixupPosition;
				this.fixupLast = fixupLast;
			}

			// Token: 0x06002A5E RID: 10846 RVA: 0x000FFE58 File Offset: 0x000FE058
			public QilNode Fixup(QilNode inExpr, QilIterator current, QilNode last)
			{
				QilDepthChecker.Check(inExpr);
				this.current = current;
				this.last = last;
				this.justCount = false;
				this.environment = null;
				this.numCurrent = (this.numPosition = (this.numLast = 0));
				inExpr = this.VisitAssumeReference(inExpr);
				return inExpr;
			}

			// Token: 0x06002A5F RID: 10847 RVA: 0x000FFEAC File Offset: 0x000FE0AC
			public QilNode Fixup(QilNode inExpr, IXPathEnvironment environment)
			{
				QilDepthChecker.Check(inExpr);
				this.justCount = false;
				this.current = null;
				this.environment = environment;
				this.numCurrent = (this.numPosition = (this.numLast = 0));
				inExpr = this.VisitAssumeReference(inExpr);
				return inExpr;
			}

			// Token: 0x06002A60 RID: 10848 RVA: 0x000FFEF8 File Offset: 0x000FE0F8
			public int CountUnfixedLast(QilNode inExpr)
			{
				this.justCount = true;
				this.numCurrent = (this.numPosition = (this.numLast = 0));
				this.VisitAssumeReference(inExpr);
				return this.numLast;
			}

			// Token: 0x06002A61 RID: 10849 RVA: 0x000FFF34 File Offset: 0x000FE134
			protected override QilNode VisitUnknown(QilNode unknown)
			{
				if (unknown == this.fixupCurrent)
				{
					this.numCurrent++;
					if (!this.justCount)
					{
						if (this.environment != null)
						{
							unknown = this.environment.GetCurrent();
						}
						else if (this.current != null)
						{
							unknown = this.current;
						}
					}
				}
				else if (unknown == this.fixupPosition)
				{
					this.numPosition++;
					if (!this.justCount)
					{
						if (this.environment != null)
						{
							unknown = this.environment.GetPosition();
						}
						else if (this.current != null)
						{
							unknown = this.f.XsltConvert(this.f.PositionOf(this.current), XmlQueryTypeFactory.DoubleX);
						}
					}
				}
				else if (unknown == this.fixupLast)
				{
					this.numLast++;
					if (!this.justCount)
					{
						if (this.environment != null)
						{
							unknown = this.environment.GetLast();
						}
						else if (this.current != null)
						{
							unknown = this.last;
						}
					}
				}
				return unknown;
			}

			// Token: 0x04002139 RID: 8505
			private new QilPatternFactory f;

			// Token: 0x0400213A RID: 8506
			private QilNode fixupCurrent;

			// Token: 0x0400213B RID: 8507
			private QilNode fixupPosition;

			// Token: 0x0400213C RID: 8508
			private QilNode fixupLast;

			// Token: 0x0400213D RID: 8509
			private QilIterator current;

			// Token: 0x0400213E RID: 8510
			private QilNode last;

			// Token: 0x0400213F RID: 8511
			private bool justCount;

			// Token: 0x04002140 RID: 8512
			private IXPathEnvironment environment;

			// Token: 0x04002141 RID: 8513
			public int numCurrent;

			// Token: 0x04002142 RID: 8514
			public int numPosition;

			// Token: 0x04002143 RID: 8515
			public int numLast;
		}

		// Token: 0x02000428 RID: 1064
		internal class FunctionInfo<T>
		{
			// Token: 0x06002A62 RID: 10850 RVA: 0x00100042 File Offset: 0x000FE242
			public FunctionInfo(T id, int minArgs, int maxArgs, XmlTypeCode[] argTypes)
			{
				this.id = id;
				this.minArgs = minArgs;
				this.maxArgs = maxArgs;
				this.argTypes = argTypes;
			}

			// Token: 0x06002A63 RID: 10851 RVA: 0x00100068 File Offset: 0x000FE268
			public static void CheckArity(int minArgs, int maxArgs, string name, int numArgs)
			{
				if (minArgs <= numArgs && numArgs <= maxArgs)
				{
					return;
				}
				string resId;
				if (minArgs == maxArgs)
				{
					resId = "Function '{0}()' must have {1} argument(s).";
				}
				else if (maxArgs == minArgs + 1)
				{
					resId = "Function '{0}()' must have {1} or {2} argument(s).";
				}
				else if (numArgs < minArgs)
				{
					resId = "Function '{0}()' must have at least {1} argument(s).";
				}
				else
				{
					resId = "Function '{0}()' must have no more than {2} arguments.";
				}
				throw new XPathCompileException(resId, new string[]
				{
					name,
					minArgs.ToString(CultureInfo.InvariantCulture),
					maxArgs.ToString(CultureInfo.InvariantCulture)
				});
			}

			// Token: 0x06002A64 RID: 10852 RVA: 0x001000D8 File Offset: 0x000FE2D8
			public void CastArguments(IList<QilNode> args, string name, XPathQilFactory f)
			{
				XPathBuilder.FunctionInfo<T>.CheckArity(this.minArgs, this.maxArgs, name, args.Count);
				if (this.maxArgs == 2147483647)
				{
					for (int i = 0; i < args.Count; i++)
					{
						args[i] = f.ConvertToType(XmlTypeCode.String, args[i]);
					}
					return;
				}
				for (int j = 0; j < args.Count; j++)
				{
					if (this.argTypes[j] == XmlTypeCode.Node && f.CannotBeNodeSet(args[j]))
					{
						throw new XPathCompileException("Argument {1} of function '{0}()' cannot be converted to a node-set.", new string[]
						{
							name,
							(j + 1).ToString(CultureInfo.InvariantCulture)
						});
					}
					args[j] = f.ConvertToType(this.argTypes[j], args[j]);
				}
			}

			// Token: 0x04002144 RID: 8516
			public T id;

			// Token: 0x04002145 RID: 8517
			public int minArgs;

			// Token: 0x04002146 RID: 8518
			public int maxArgs;

			// Token: 0x04002147 RID: 8519
			public XmlTypeCode[] argTypes;

			// Token: 0x04002148 RID: 8520
			public const int Infinity = 2147483647;
		}
	}
}
