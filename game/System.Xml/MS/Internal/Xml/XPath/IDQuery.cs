using System;
using System.Xml;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000636 RID: 1590
	internal sealed class IDQuery : CacheOutputQuery
	{
		// Token: 0x060040CC RID: 16588 RVA: 0x00164887 File Offset: 0x00162A87
		public IDQuery(Query arg) : base(arg)
		{
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x00164890 File Offset: 0x00162A90
		private IDQuery(IDQuery other) : base(other)
		{
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x0016569C File Offset: 0x0016389C
		public override object Evaluate(XPathNodeIterator context)
		{
			object obj = base.Evaluate(context);
			XPathNavigator contextNode = context.Current.Clone();
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.Number:
				this.ProcessIds(contextNode, StringFunctions.toString((double)obj));
				break;
			case XPathResultType.String:
				this.ProcessIds(contextNode, (string)obj);
				break;
			case XPathResultType.Boolean:
				this.ProcessIds(contextNode, StringFunctions.toString((bool)obj));
				break;
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator;
				while ((xpathNavigator = this.input.Advance()) != null)
				{
					this.ProcessIds(contextNode, xpathNavigator.Value);
				}
				break;
			}
			case (XPathResultType)4:
				this.ProcessIds(contextNode, ((XPathNavigator)obj).Value);
				break;
			}
			return this;
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x0016574C File Offset: 0x0016394C
		private void ProcessIds(XPathNavigator contextNode, string val)
		{
			string[] array = XmlConvert.SplitString(val);
			for (int i = 0; i < array.Length; i++)
			{
				if (contextNode.MoveToId(array[i]))
				{
					Query.Insert(this.outputBuffer, contextNode);
				}
			}
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x00165788 File Offset: 0x00163988
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			this.Evaluate(new XPathSingletonIterator(context, true));
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.Advance()) != null)
			{
				if (xpathNavigator.IsSamePosition(context))
				{
					return context;
				}
			}
			return null;
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x001657BB File Offset: 0x001639BB
		public override XPathNodeIterator Clone()
		{
			return new IDQuery(this);
		}
	}
}
