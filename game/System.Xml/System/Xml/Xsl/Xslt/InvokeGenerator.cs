using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020003E4 RID: 996
	internal class InvokeGenerator : QilCloneVisitor
	{
		// Token: 0x060027A4 RID: 10148 RVA: 0x000EB7B8 File Offset: 0x000E99B8
		public InvokeGenerator(XsltQilFactory f, bool debug) : base(f.BaseFactory)
		{
			this.debug = debug;
			this.fac = f;
			this.iterStack = new Stack<QilIterator>();
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x000EB7E0 File Offset: 0x000E99E0
		public QilNode GenerateInvoke(QilFunction func, IList<XslNode> actualArgs)
		{
			this.iterStack.Clear();
			this.formalArgs = func.Arguments;
			this.invokeArgs = this.fac.ActualParameterList();
			this.curArg = 0;
			while (this.curArg < this.formalArgs.Count)
			{
				QilParameter qilParameter = (QilParameter)this.formalArgs[this.curArg];
				QilNode qilNode = this.FindActualArg(qilParameter, actualArgs);
				if (qilNode == null)
				{
					if (this.debug)
					{
						if (qilParameter.Name.NamespaceUri == "urn:schemas-microsoft-com:xslt-debug")
						{
							qilNode = base.Clone(qilParameter.DefaultValue);
						}
						else
						{
							qilNode = this.fac.DefaultValueMarker();
						}
					}
					else
					{
						qilNode = base.Clone(qilParameter.DefaultValue);
					}
				}
				XmlQueryType xmlType = qilParameter.XmlType;
				if (!qilNode.XmlType.IsSubtypeOf(xmlType))
				{
					qilNode = this.fac.TypeAssert(qilNode, xmlType);
				}
				this.invokeArgs.Add(qilNode);
				this.curArg++;
			}
			QilNode qilNode2 = this.fac.Invoke(func, this.invokeArgs);
			while (this.iterStack.Count != 0)
			{
				qilNode2 = this.fac.Loop(this.iterStack.Pop(), qilNode2);
			}
			return qilNode2;
		}

		// Token: 0x060027A6 RID: 10150 RVA: 0x000EB91C File Offset: 0x000E9B1C
		private QilNode FindActualArg(QilParameter formalArg, IList<XslNode> actualArgs)
		{
			QilName name = formalArg.Name;
			foreach (XslNode xslNode in actualArgs)
			{
				if (xslNode.Name.Equals(name))
				{
					return ((VarPar)xslNode).Value;
				}
			}
			return null;
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000EB984 File Offset: 0x000E9B84
		protected override QilNode VisitReference(QilNode n)
		{
			QilNode qilNode = base.FindClonedReference(n);
			if (qilNode != null)
			{
				return qilNode;
			}
			int i = 0;
			while (i < this.curArg)
			{
				if (n == this.formalArgs[i])
				{
					if (this.invokeArgs[i] is QilLiteral)
					{
						return this.invokeArgs[i].ShallowClone(this.fac.BaseFactory);
					}
					if (!(this.invokeArgs[i] is QilIterator))
					{
						QilIterator qilIterator = this.fac.BaseFactory.Let(this.invokeArgs[i]);
						this.iterStack.Push(qilIterator);
						this.invokeArgs[i] = qilIterator;
					}
					return this.invokeArgs[i];
				}
				else
				{
					i++;
				}
			}
			return n;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0000206B File Offset: 0x0000026B
		protected override QilNode VisitFunction(QilFunction n)
		{
			return n;
		}

		// Token: 0x04001F0F RID: 7951
		private bool debug;

		// Token: 0x04001F10 RID: 7952
		private Stack<QilIterator> iterStack;

		// Token: 0x04001F11 RID: 7953
		private QilList formalArgs;

		// Token: 0x04001F12 RID: 7954
		private QilList invokeArgs;

		// Token: 0x04001F13 RID: 7955
		private int curArg;

		// Token: 0x04001F14 RID: 7956
		private XsltQilFactory fac;
	}
}
