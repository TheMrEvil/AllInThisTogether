using System;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.XPath;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003E5 RID: 997
	internal class KeyMatchBuilder : XPathBuilder, XPathPatternParser.IPatternBuilder, IXPathBuilder<QilNode>
	{
		// Token: 0x060027A9 RID: 10153 RVA: 0x000EBA4E File Offset: 0x000E9C4E
		public KeyMatchBuilder(IXPathEnvironment env) : base(env)
		{
			this.convertor = new KeyMatchBuilder.PathConvertor(env.Factory);
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000EBA68 File Offset: 0x000E9C68
		public override void StartBuild()
		{
			if (this.depth == 0)
			{
				base.StartBuild();
			}
			this.depth++;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000EBA88 File Offset: 0x000E9C88
		public override QilNode EndBuild(QilNode result)
		{
			this.depth--;
			if (result == null)
			{
				return base.EndBuild(result);
			}
			if (this.depth == 0)
			{
				result = this.convertor.ConvertReletive2Absolute(result, this.fixupCurrent);
				result = base.EndBuild(result);
			}
			return result;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00002068 File Offset: 0x00000268
		public virtual IXPathBuilder<QilNode> GetPredicateBuilder(QilNode ctx)
		{
			return this;
		}

		// Token: 0x04001F15 RID: 7957
		private int depth;

		// Token: 0x04001F16 RID: 7958
		private KeyMatchBuilder.PathConvertor convertor;

		// Token: 0x020003E6 RID: 998
		internal class PathConvertor : QilReplaceVisitor
		{
			// Token: 0x060027AD RID: 10157 RVA: 0x000EBAD4 File Offset: 0x000E9CD4
			public PathConvertor(XPathQilFactory f) : base(f.BaseFactory)
			{
				this.f = f;
			}

			// Token: 0x060027AE RID: 10158 RVA: 0x000EBAE9 File Offset: 0x000E9CE9
			public QilNode ConvertReletive2Absolute(QilNode node, QilNode fixup)
			{
				QilDepthChecker.Check(node);
				this.fixup = fixup;
				return this.Visit(node);
			}

			// Token: 0x060027AF RID: 10159 RVA: 0x000EBAFF File Offset: 0x000E9CFF
			protected override QilNode Visit(QilNode n)
			{
				if (n.NodeType == QilNodeType.Union || n.NodeType == QilNodeType.DocOrderDistinct || n.NodeType == QilNodeType.Filter || n.NodeType == QilNodeType.Loop)
				{
					return base.Visit(n);
				}
				return n;
			}

			// Token: 0x060027B0 RID: 10160 RVA: 0x000EBB34 File Offset: 0x000E9D34
			protected override QilNode VisitLoop(QilLoop n)
			{
				if (n.Variable.Binding.NodeType == QilNodeType.Root || n.Variable.Binding.NodeType == QilNodeType.Deref)
				{
					return n;
				}
				if (n.Variable.Binding.NodeType == QilNodeType.Content)
				{
					QilUnary qilUnary = (QilUnary)n.Variable.Binding;
					QilIterator qilIterator = this.f.For(this.f.DescendantOrSelf(this.f.Root(this.fixup)));
					qilUnary.Child = qilIterator;
					n.Variable.Binding = this.f.Loop(qilIterator, qilUnary);
					return n;
				}
				n.Variable.Binding = this.Visit(n.Variable.Binding);
				return n;
			}

			// Token: 0x060027B1 RID: 10161 RVA: 0x000EBBF6 File Offset: 0x000E9DF6
			protected override QilNode VisitFilter(QilLoop n)
			{
				return this.VisitLoop(n);
			}

			// Token: 0x04001F17 RID: 7959
			private new XPathQilFactory f;

			// Token: 0x04001F18 RID: 7960
			private QilNode fixup;
		}
	}
}
