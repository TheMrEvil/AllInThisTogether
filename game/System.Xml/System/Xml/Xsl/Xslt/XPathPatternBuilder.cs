using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml.XPath;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003F9 RID: 1017
	internal class XPathPatternBuilder : XPathPatternParser.IPatternBuilder, IXPathBuilder<QilNode>
	{
		// Token: 0x06002883 RID: 10371 RVA: 0x000F4070 File Offset: 0x000F2270
		public XPathPatternBuilder(IXPathEnvironment environment)
		{
			this.environment = environment;
			this.f = environment.Factory;
			this.predicateEnvironment = new XPathPatternBuilder.XPathPredicateEnvironment(environment);
			this.predicateBuilder = new XPathBuilder(this.predicateEnvironment);
			this.fixupNode = this.f.Unknown(XmlQueryTypeFactory.NodeNotRtfS);
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x000F40C9 File Offset: 0x000F22C9
		public QilNode FixupNode
		{
			get
			{
				return this.fixupNode;
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000F40D1 File Offset: 0x000F22D1
		public virtual void StartBuild()
		{
			this.inTheBuild = true;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0000B528 File Offset: 0x00009728
		[Conditional("DEBUG")]
		public void AssertFilter(QilLoop filter)
		{
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000F40DA File Offset: 0x000F22DA
		private void FixupFilterBinding(QilLoop filter, QilNode newBinding)
		{
			filter.Variable.Binding = newBinding;
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000F40E8 File Offset: 0x000F22E8
		public virtual QilNode EndBuild(QilNode result)
		{
			this.inTheBuild = false;
			return result;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000F40F4 File Offset: 0x000F22F4
		public QilNode Operator(XPathOperator op, QilNode left, QilNode right)
		{
			if (left.NodeType == QilNodeType.Sequence)
			{
				((QilList)left).Add(right);
				return left;
			}
			return this.f.Sequence(left, right);
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000F411C File Offset: 0x000F231C
		private static QilLoop BuildAxisFilter(QilPatternFactory f, QilIterator itr, XPathAxis xpathAxis, XPathNodeType nodeType, string name, string nsUri)
		{
			QilNode right = (name != null && nsUri != null) ? f.Eq(f.NameOf(itr), f.QName(name, nsUri)) : ((nsUri != null) ? f.Eq(f.NamespaceUriOf(itr), f.String(nsUri)) : ((name != null) ? f.Eq(f.LocalNameOf(itr), f.String(name)) : f.True()));
			XmlNodeKindFlags xmlNodeKindFlags = XPathBuilder.AxisTypeMask(itr.XmlType.NodeKinds, nodeType, xpathAxis);
			QilNode left = (xmlNodeKindFlags == XmlNodeKindFlags.None) ? f.False() : ((xmlNodeKindFlags == itr.XmlType.NodeKinds) ? f.True() : f.IsType(itr, XmlQueryTypeFactory.NodeChoice(xmlNodeKindFlags)));
			QilLoop qilLoop = f.BaseFactory.Filter(itr, f.And(left, right));
			qilLoop.XmlType = XmlQueryTypeFactory.PrimeProduct(XmlQueryTypeFactory.NodeChoice(xmlNodeKindFlags), qilLoop.XmlType.Cardinality);
			return qilLoop;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000F41FC File Offset: 0x000F23FC
		public QilNode Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name)
		{
			if (xpathAxis != XPathAxis.DescendantOrSelf)
			{
				QilLoop qilLoop;
				double priority;
				if (xpathAxis != XPathAxis.Root)
				{
					string nsUri = (prefix == null) ? null : this.environment.ResolvePrefix(prefix);
					qilLoop = XPathPatternBuilder.BuildAxisFilter(this.f, this.f.For(this.fixupNode), xpathAxis, nodeType, name, nsUri);
					if (nodeType - XPathNodeType.Element > 1)
					{
						if (nodeType != XPathNodeType.ProcessingInstruction)
						{
							priority = -0.5;
						}
						else
						{
							priority = ((name != null) ? 0.0 : -0.5);
						}
					}
					else if (name != null)
					{
						priority = 0.0;
					}
					else if (prefix != null)
					{
						priority = -0.25;
					}
					else
					{
						priority = -0.5;
					}
				}
				else
				{
					QilIterator expr;
					qilLoop = this.f.BaseFactory.Filter(expr = this.f.For(this.fixupNode), this.f.IsType(expr, XmlQueryTypeFactory.Document));
					priority = 0.5;
				}
				XPathPatternBuilder.SetPriority(qilLoop, priority);
				XPathPatternBuilder.SetLastParent(qilLoop, qilLoop);
				return qilLoop;
			}
			return this.f.Nop(this.fixupNode);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000F430C File Offset: 0x000F250C
		public QilNode JoinStep(QilNode left, QilNode right)
		{
			if (left.NodeType == QilNodeType.Nop)
			{
				QilUnary qilUnary = (QilUnary)left;
				qilUnary.Child = right;
				return qilUnary;
			}
			XPathPatternBuilder.CleanAnnotation(left);
			QilLoop qilLoop = (QilLoop)left;
			bool flag = false;
			if (right.NodeType == QilNodeType.Nop)
			{
				flag = true;
				right = ((QilUnary)right).Child;
			}
			QilLoop lastParent = XPathPatternBuilder.GetLastParent(right);
			this.FixupFilterBinding(qilLoop, flag ? this.f.Ancestor(lastParent.Variable) : this.f.Parent(lastParent.Variable));
			lastParent.Body = this.f.And(lastParent.Body, this.f.Not(this.f.IsEmpty(qilLoop)));
			XPathPatternBuilder.SetPriority(right, 0.5);
			XPathPatternBuilder.SetLastParent(right, qilLoop);
			return right;
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x0001DA42 File Offset: 0x0001BC42
		QilNode IXPathBuilder<QilNode>.Predicate(QilNode node, QilNode condition, bool isReverseStep)
		{
			return null;
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000F43D4 File Offset: 0x000F25D4
		public QilNode BuildPredicates(QilNode nodeset, List<QilNode> predicates)
		{
			List<QilNode> list = new List<QilNode>(predicates.Count);
			foreach (QilNode predicate in predicates)
			{
				list.Add(XPathBuilder.PredicateToBoolean(predicate, this.f, this.predicateEnvironment));
			}
			QilLoop qilLoop = (QilLoop)nodeset;
			QilIterator variable = qilLoop.Variable;
			if (this.predicateEnvironment.numFixupLast == 0 && this.predicateEnvironment.numFixupPosition == 0)
			{
				foreach (QilNode right in list)
				{
					qilLoop.Body = this.f.And(qilLoop.Body, right);
				}
				qilLoop.Body = this.predicateEnvironment.fixupVisitor.Fixup(qilLoop.Body, variable, null);
			}
			else
			{
				QilIterator qilIterator = this.f.For(this.f.Parent(variable));
				QilNode binding = this.f.Content(qilIterator);
				QilLoop qilLoop2 = (QilLoop)nodeset.DeepClone(this.f.BaseFactory);
				qilLoop2.Variable.Binding = binding;
				qilLoop2 = (QilLoop)this.f.Loop(qilIterator, qilLoop2);
				QilNode qilNode = qilLoop2;
				foreach (QilNode predicate2 in list)
				{
					qilNode = XPathBuilder.BuildOnePredicate(qilNode, predicate2, false, this.f, this.predicateEnvironment.fixupVisitor, ref this.predicateEnvironment.numFixupCurrent, ref this.predicateEnvironment.numFixupPosition, ref this.predicateEnvironment.numFixupLast);
				}
				QilIterator qilIterator2 = this.f.For(qilNode);
				QilNode set = this.f.Filter(qilIterator2, this.f.Is(qilIterator2, variable));
				qilLoop.Body = this.f.Not(this.f.IsEmpty(set));
				qilLoop.Body = this.f.And(this.f.IsType(variable, qilLoop.XmlType), qilLoop.Body);
			}
			XPathPatternBuilder.SetPriority(nodeset, 0.5);
			return nodeset;
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000F463C File Offset: 0x000F283C
		public QilNode Function(string prefix, string name, IList<QilNode> args)
		{
			QilIterator qilIterator = this.f.For(this.fixupNode);
			QilNode binding;
			if (name == "id")
			{
				binding = this.f.Id(qilIterator, args[0]);
			}
			else
			{
				binding = this.environment.ResolveFunction(prefix, name, args, new XPathPatternBuilder.XsltFunctionFocus(qilIterator));
			}
			QilIterator left;
			QilLoop qilLoop = this.f.BaseFactory.Filter(qilIterator, this.f.Not(this.f.IsEmpty(this.f.Filter(left = this.f.For(binding), this.f.Is(left, qilIterator)))));
			XPathPatternBuilder.SetPriority(qilLoop, 0.5);
			XPathPatternBuilder.SetLastParent(qilLoop, qilLoop);
			return qilLoop;
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000F46F8 File Offset: 0x000F28F8
		public QilNode String(string value)
		{
			return this.f.String(value);
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000F4706 File Offset: 0x000F2906
		public QilNode Number(double value)
		{
			return this.UnexpectedToken("Literal number");
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000F4713 File Offset: 0x000F2913
		public QilNode Variable(string prefix, string name)
		{
			return this.UnexpectedToken("Variable");
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000F4720 File Offset: 0x000F2920
		private QilNode UnexpectedToken(string tokenName)
		{
			throw new Exception(string.Format(CultureInfo.InvariantCulture, "Internal Error: {0} is not allowed in XSLT pattern outside of predicate.", tokenName));
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x000F4738 File Offset: 0x000F2938
		public static void SetPriority(QilNode node, double priority)
		{
			XPathPatternBuilder.Annotation annotation = ((XPathPatternBuilder.Annotation)node.Annotation) ?? new XPathPatternBuilder.Annotation();
			annotation.Priority = priority;
			node.Annotation = annotation;
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000F4768 File Offset: 0x000F2968
		public static double GetPriority(QilNode node)
		{
			return ((XPathPatternBuilder.Annotation)node.Annotation).Priority;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000F477C File Offset: 0x000F297C
		private static void SetLastParent(QilNode node, QilLoop parent)
		{
			XPathPatternBuilder.Annotation annotation = ((XPathPatternBuilder.Annotation)node.Annotation) ?? new XPathPatternBuilder.Annotation();
			annotation.Parent = parent;
			node.Annotation = annotation;
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000F47AC File Offset: 0x000F29AC
		private static QilLoop GetLastParent(QilNode node)
		{
			return ((XPathPatternBuilder.Annotation)node.Annotation).Parent;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000F47BE File Offset: 0x000F29BE
		public static void CleanAnnotation(QilNode node)
		{
			node.Annotation = null;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000F47C7 File Offset: 0x000F29C7
		public IXPathBuilder<QilNode> GetPredicateBuilder(QilNode ctx)
		{
			QilLoop qilLoop = (QilLoop)ctx;
			return this.predicateBuilder;
		}

		// Token: 0x04002009 RID: 8201
		private XPathPatternBuilder.XPathPredicateEnvironment predicateEnvironment;

		// Token: 0x0400200A RID: 8202
		private XPathBuilder predicateBuilder;

		// Token: 0x0400200B RID: 8203
		private bool inTheBuild;

		// Token: 0x0400200C RID: 8204
		private XPathQilFactory f;

		// Token: 0x0400200D RID: 8205
		private QilNode fixupNode;

		// Token: 0x0400200E RID: 8206
		private IXPathEnvironment environment;

		// Token: 0x020003FA RID: 1018
		private class Annotation
		{
			// Token: 0x0600289A RID: 10394 RVA: 0x0000216B File Offset: 0x0000036B
			public Annotation()
			{
			}

			// Token: 0x0400200F RID: 8207
			public double Priority;

			// Token: 0x04002010 RID: 8208
			public QilLoop Parent;
		}

		// Token: 0x020003FB RID: 1019
		private class XPathPredicateEnvironment : IXPathEnvironment, IFocus
		{
			// Token: 0x0600289B RID: 10395 RVA: 0x000F47D8 File Offset: 0x000F29D8
			public XPathPredicateEnvironment(IXPathEnvironment baseEnvironment)
			{
				this.baseEnvironment = baseEnvironment;
				this.f = baseEnvironment.Factory;
				this.fixupCurrent = this.f.Unknown(XmlQueryTypeFactory.NodeNotRtf);
				this.fixupPosition = this.f.Unknown(XmlQueryTypeFactory.DoubleX);
				this.fixupLast = this.f.Unknown(XmlQueryTypeFactory.DoubleX);
				this.fixupVisitor = new XPathBuilder.FixupVisitor(this.f, this.fixupCurrent, this.fixupPosition, this.fixupLast);
			}

			// Token: 0x170007D8 RID: 2008
			// (get) Token: 0x0600289C RID: 10396 RVA: 0x000F4863 File Offset: 0x000F2A63
			public XPathQilFactory Factory
			{
				get
				{
					return this.f;
				}
			}

			// Token: 0x0600289D RID: 10397 RVA: 0x000F486B File Offset: 0x000F2A6B
			public QilNode ResolveVariable(string prefix, string name)
			{
				return this.baseEnvironment.ResolveVariable(prefix, name);
			}

			// Token: 0x0600289E RID: 10398 RVA: 0x000F487A File Offset: 0x000F2A7A
			public QilNode ResolveFunction(string prefix, string name, IList<QilNode> args, IFocus env)
			{
				return this.baseEnvironment.ResolveFunction(prefix, name, args, env);
			}

			// Token: 0x0600289F RID: 10399 RVA: 0x000F488C File Offset: 0x000F2A8C
			public string ResolvePrefix(string prefix)
			{
				return this.baseEnvironment.ResolvePrefix(prefix);
			}

			// Token: 0x060028A0 RID: 10400 RVA: 0x000F489A File Offset: 0x000F2A9A
			public QilNode GetCurrent()
			{
				this.numFixupCurrent++;
				return this.fixupCurrent;
			}

			// Token: 0x060028A1 RID: 10401 RVA: 0x000F48B0 File Offset: 0x000F2AB0
			public QilNode GetPosition()
			{
				this.numFixupPosition++;
				return this.fixupPosition;
			}

			// Token: 0x060028A2 RID: 10402 RVA: 0x000F48C6 File Offset: 0x000F2AC6
			public QilNode GetLast()
			{
				this.numFixupLast++;
				return this.fixupLast;
			}

			// Token: 0x04002011 RID: 8209
			private readonly IXPathEnvironment baseEnvironment;

			// Token: 0x04002012 RID: 8210
			private readonly XPathQilFactory f;

			// Token: 0x04002013 RID: 8211
			public readonly XPathBuilder.FixupVisitor fixupVisitor;

			// Token: 0x04002014 RID: 8212
			private readonly QilNode fixupCurrent;

			// Token: 0x04002015 RID: 8213
			private readonly QilNode fixupPosition;

			// Token: 0x04002016 RID: 8214
			private readonly QilNode fixupLast;

			// Token: 0x04002017 RID: 8215
			public int numFixupCurrent;

			// Token: 0x04002018 RID: 8216
			public int numFixupPosition;

			// Token: 0x04002019 RID: 8217
			public int numFixupLast;
		}

		// Token: 0x020003FC RID: 1020
		private class XsltFunctionFocus : IFocus
		{
			// Token: 0x060028A3 RID: 10403 RVA: 0x000F48DC File Offset: 0x000F2ADC
			public XsltFunctionFocus(QilIterator current)
			{
				this.current = current;
			}

			// Token: 0x060028A4 RID: 10404 RVA: 0x000F48EB File Offset: 0x000F2AEB
			public QilNode GetCurrent()
			{
				return this.current;
			}

			// Token: 0x060028A5 RID: 10405 RVA: 0x0001DA42 File Offset: 0x0001BC42
			public QilNode GetPosition()
			{
				return null;
			}

			// Token: 0x060028A6 RID: 10406 RVA: 0x0001DA42 File Offset: 0x0001BC42
			public QilNode GetLast()
			{
				return null;
			}

			// Token: 0x0400201A RID: 8218
			private QilIterator current;
		}
	}
}
