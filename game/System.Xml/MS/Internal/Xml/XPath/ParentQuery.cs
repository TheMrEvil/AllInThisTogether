using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000644 RID: 1604
	internal sealed class ParentQuery : CacheAxisQuery
	{
		// Token: 0x0600413D RID: 16701 RVA: 0x00166966 File Offset: 0x00164B66
		public ParentQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type) : base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x00166973 File Offset: 0x00164B73
		private ParentQuery(ParentQuery other) : base(other)
		{
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x0016697C File Offset: 0x00164B7C
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator;
			while ((xpathNavigator = this.qyInput.Advance()) != null)
			{
				xpathNavigator = xpathNavigator.Clone();
				if (xpathNavigator.MoveToParent() && this.matches(xpathNavigator))
				{
					Query.Insert(this.outputBuffer, xpathNavigator);
				}
			}
			return this;
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x001669C8 File Offset: 0x00164BC8
		public override XPathNodeIterator Clone()
		{
			return new ParentQuery(this);
		}
	}
}
