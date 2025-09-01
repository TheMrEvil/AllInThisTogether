using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200063D RID: 1597
	internal sealed class NodeFunctions : ValueQuery
	{
		// Token: 0x06004112 RID: 16658 RVA: 0x001663BE File Offset: 0x001645BE
		public NodeFunctions(Function.FunctionType funcType, Query arg)
		{
			this._funcType = funcType;
			this._arg = arg;
		}

		// Token: 0x06004113 RID: 16659 RVA: 0x001663D4 File Offset: 0x001645D4
		public override void SetXsltContext(XsltContext context)
		{
			this._xsltContext = (context.Whitespace ? context : null);
			if (this._arg != null)
			{
				this._arg.SetXsltContext(context);
			}
		}

		// Token: 0x06004114 RID: 16660 RVA: 0x001663FC File Offset: 0x001645FC
		private XPathNavigator EvaluateArg(XPathNodeIterator context)
		{
			if (this._arg == null)
			{
				return context.Current;
			}
			this._arg.Evaluate(context);
			return this._arg.Advance();
		}

		// Token: 0x06004115 RID: 16661 RVA: 0x00166428 File Offset: 0x00164628
		public override object Evaluate(XPathNodeIterator context)
		{
			switch (this._funcType)
			{
			case Function.FunctionType.FuncLast:
				return (double)context.Count;
			case Function.FunctionType.FuncPosition:
				return (double)context.CurrentPosition;
			case Function.FunctionType.FuncCount:
			{
				this._arg.Evaluate(context);
				int num = 0;
				if (this._xsltContext != null)
				{
					XPathNavigator xpathNavigator;
					while ((xpathNavigator = this._arg.Advance()) != null)
					{
						if (xpathNavigator.NodeType != XPathNodeType.Whitespace || this._xsltContext.PreserveWhitespace(xpathNavigator))
						{
							num++;
						}
					}
				}
				else
				{
					while (this._arg.Advance() != null)
					{
						num++;
					}
				}
				return (double)num;
			}
			case Function.FunctionType.FuncLocalName:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.LocalName;
				}
				break;
			}
			case Function.FunctionType.FuncNameSpaceUri:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.NamespaceURI;
				}
				break;
			}
			case Function.FunctionType.FuncName:
			{
				XPathNavigator xpathNavigator2 = this.EvaluateArg(context);
				if (xpathNavigator2 != null)
				{
					return xpathNavigator2.Name;
				}
				break;
			}
			}
			return string.Empty;
		}

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x00166519 File Offset: 0x00164719
		public override XPathResultType StaticType
		{
			get
			{
				return Function.ReturnTypes[(int)this._funcType];
			}
		}

		// Token: 0x06004117 RID: 16663 RVA: 0x00166527 File Offset: 0x00164727
		public override XPathNodeIterator Clone()
		{
			return new NodeFunctions(this._funcType, Query.Clone(this._arg))
			{
				_xsltContext = this._xsltContext
			};
		}

		// Token: 0x04002E4E RID: 11854
		private Query _arg;

		// Token: 0x04002E4F RID: 11855
		private Function.FunctionType _funcType;

		// Token: 0x04002E50 RID: 11856
		private XsltContext _xsltContext;
	}
}
