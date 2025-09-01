using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000645 RID: 1605
	internal class PreSiblingQuery : CacheAxisQuery
	{
		// Token: 0x06004141 RID: 16705 RVA: 0x00166966 File Offset: 0x00164B66
		public PreSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest) : base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x00166973 File Offset: 0x00164B73
		protected PreSiblingQuery(PreSiblingQuery other) : base(other)
		{
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x001669D0 File Offset: 0x00164BD0
		private static bool NotVisited(XPathNavigator nav, List<XPathNavigator> parentStk)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToParent();
			for (int i = 0; i < parentStk.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(parentStk[i]))
				{
					return false;
				}
			}
			parentStk.Add(xpathNavigator);
			return true;
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x00166A18 File Offset: 0x00164C18
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			List<XPathNavigator> parentStk = new List<XPathNavigator>();
			Stack<XPathNavigator> stack = new Stack<XPathNavigator>();
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				stack.Push(this.currentNode.Clone());
			}
			while (stack.Count != 0)
			{
				XPathNavigator xpathNavigator = stack.Pop();
				if (xpathNavigator.NodeType != XPathNodeType.Attribute && xpathNavigator.NodeType != XPathNodeType.Namespace && PreSiblingQuery.NotVisited(xpathNavigator, parentStk))
				{
					XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
					if (xpathNavigator2.MoveToParent())
					{
						xpathNavigator2.MoveToFirstChild();
						while (!xpathNavigator2.IsSamePosition(xpathNavigator))
						{
							if (this.matches(xpathNavigator2))
							{
								Query.Insert(this.outputBuffer, xpathNavigator2);
							}
							if (!xpathNavigator2.MoveToNext())
							{
								break;
							}
						}
					}
				}
			}
			return this;
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x00166AD5 File Offset: 0x00164CD5
		public override XPathNodeIterator Clone()
		{
			return new PreSiblingQuery(this);
		}

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06004146 RID: 16710 RVA: 0x00166ADD File Offset: 0x00164CDD
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
