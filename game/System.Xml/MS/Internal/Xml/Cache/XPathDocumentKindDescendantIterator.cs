using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200066E RID: 1646
	internal class XPathDocumentKindDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06004295 RID: 17045 RVA: 0x0016BB15 File Offset: 0x00169D15
		public XPathDocumentKindDescendantIterator(XPathDocumentNavigator root, XPathNodeType typ, bool matchSelf) : base(root)
		{
			this._typ = typ;
			this._matchSelf = matchSelf;
			if (root.NodeType != XPathNodeType.Root)
			{
				this._end = new XPathDocumentNavigator(root);
				this._end.MoveToNonDescendant();
			}
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x0016BB4C File Offset: 0x00169D4C
		public XPathDocumentKindDescendantIterator(XPathDocumentKindDescendantIterator iter) : base(iter)
		{
			this._end = iter._end;
			this._typ = iter._typ;
			this._matchSelf = iter._matchSelf;
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x0016BB79 File Offset: 0x00169D79
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindDescendantIterator(this);
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x0016BB84 File Offset: 0x00169D84
		public override bool MoveNext()
		{
			if (this._matchSelf)
			{
				this._matchSelf = false;
				if (this.ctxt.IsKindMatch(this._typ))
				{
					this.pos++;
					return true;
				}
			}
			if (!this.ctxt.MoveToFollowing(this._typ, this._end))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04002F1C RID: 12060
		private XPathDocumentNavigator _end;

		// Token: 0x04002F1D RID: 12061
		private XPathNodeType _typ;

		// Token: 0x04002F1E RID: 12062
		private bool _matchSelf;
	}
}
