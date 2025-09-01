using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200066D RID: 1645
	internal class XPathDocumentElementDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06004291 RID: 17041 RVA: 0x0016B9FC File Offset: 0x00169BFC
		public XPathDocumentElementDescendantIterator(XPathDocumentNavigator root, string name, string namespaceURI, bool matchSelf) : base(root)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this._localName = root.NameTable.Get(name);
			this._namespaceUri = namespaceURI;
			this._matchSelf = matchSelf;
			if (root.NodeType != XPathNodeType.Root)
			{
				this._end = new XPathDocumentNavigator(root);
				this._end.MoveToNonDescendant();
			}
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x0016BA5F File Offset: 0x00169C5F
		public XPathDocumentElementDescendantIterator(XPathDocumentElementDescendantIterator iter) : base(iter)
		{
			this._end = iter._end;
			this._localName = iter._localName;
			this._namespaceUri = iter._namespaceUri;
			this._matchSelf = iter._matchSelf;
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x0016BA98 File Offset: 0x00169C98
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementDescendantIterator(this);
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x0016BAA0 File Offset: 0x00169CA0
		public override bool MoveNext()
		{
			if (this._matchSelf)
			{
				this._matchSelf = false;
				if (this.ctxt.IsElementMatch(this._localName, this._namespaceUri))
				{
					this.pos++;
					return true;
				}
			}
			if (!this.ctxt.MoveToFollowing(this._localName, this._namespaceUri, this._end))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04002F18 RID: 12056
		private XPathDocumentNavigator _end;

		// Token: 0x04002F19 RID: 12057
		private string _localName;

		// Token: 0x04002F1A RID: 12058
		private string _namespaceUri;

		// Token: 0x04002F1B RID: 12059
		private bool _matchSelf;
	}
}
