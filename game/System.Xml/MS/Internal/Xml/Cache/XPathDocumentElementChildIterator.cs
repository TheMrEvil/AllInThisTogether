using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200066B RID: 1643
	internal class XPathDocumentElementChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06004289 RID: 17033 RVA: 0x0016B8CA File Offset: 0x00169ACA
		public XPathDocumentElementChildIterator(XPathDocumentNavigator parent, string name, string namespaceURI) : base(parent)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this._localName = parent.NameTable.Get(name);
			this._namespaceUri = namespaceURI;
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x0016B8FA File Offset: 0x00169AFA
		public XPathDocumentElementChildIterator(XPathDocumentElementChildIterator iter) : base(iter)
		{
			this._localName = iter._localName;
			this._namespaceUri = iter._namespaceUri;
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x0016B91B File Offset: 0x00169B1B
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementChildIterator(this);
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x0016B924 File Offset: 0x00169B24
		public override bool MoveNext()
		{
			if (this.pos == 0)
			{
				if (!this.ctxt.MoveToChild(this._localName, this._namespaceUri))
				{
					return false;
				}
			}
			else if (!this.ctxt.MoveToNext(this._localName, this._namespaceUri))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04002F15 RID: 12053
		private string _localName;

		// Token: 0x04002F16 RID: 12054
		private string _namespaceUri;
	}
}
