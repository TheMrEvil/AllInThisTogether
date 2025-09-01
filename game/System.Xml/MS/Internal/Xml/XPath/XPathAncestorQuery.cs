using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000658 RID: 1624
	internal sealed class XPathAncestorQuery : CacheAxisQuery
	{
		// Token: 0x060041CF RID: 16847 RVA: 0x0016888B File Offset: 0x00166A8B
		public XPathAncestorQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest, bool matchSelf) : base(qyInput, name, prefix, typeTest)
		{
			this._matchSelf = matchSelf;
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x001688A0 File Offset: 0x00166AA0
		private XPathAncestorQuery(XPathAncestorQuery other) : base(other)
		{
			this._matchSelf = other._matchSelf;
		}

		// Token: 0x060041D1 RID: 16849 RVA: 0x001688B8 File Offset: 0x00166AB8
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			XPathNavigator xpathNavigator = null;
			XPathNavigator xpathNavigator2;
			while ((xpathNavigator2 = this.qyInput.Advance()) != null)
			{
				if (!this._matchSelf || !this.matches(xpathNavigator2) || Query.Insert(this.outputBuffer, xpathNavigator2))
				{
					if (xpathNavigator == null || !xpathNavigator.MoveTo(xpathNavigator2))
					{
						xpathNavigator = xpathNavigator2.Clone();
					}
					while (xpathNavigator.MoveToParent() && (!this.matches(xpathNavigator) || Query.Insert(this.outputBuffer, xpathNavigator)))
					{
					}
				}
			}
			return this;
		}

		// Token: 0x060041D2 RID: 16850 RVA: 0x00168934 File Offset: 0x00166B34
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorQuery(this);
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x0016893C File Offset: 0x00166B3C
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x00166ADD File Offset: 0x00164CDD
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x04002EA1 RID: 11937
		private bool _matchSelf;
	}
}
