using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200066C RID: 1644
	internal class XPathDocumentKindChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x0600428D RID: 17037 RVA: 0x0016B97E File Offset: 0x00169B7E
		public XPathDocumentKindChildIterator(XPathDocumentNavigator parent, XPathNodeType typ) : base(parent)
		{
			this._typ = typ;
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x0016B98E File Offset: 0x00169B8E
		public XPathDocumentKindChildIterator(XPathDocumentKindChildIterator iter) : base(iter)
		{
			this._typ = iter._typ;
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x0016B9A3 File Offset: 0x00169BA3
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindChildIterator(this);
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x0016B9AC File Offset: 0x00169BAC
		public override bool MoveNext()
		{
			if (this.pos == 0)
			{
				if (!this.ctxt.MoveToChild(this._typ))
				{
					return false;
				}
			}
			else if (!this.ctxt.MoveToNext(this._typ))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04002F17 RID: 12055
		private XPathNodeType _typ;
	}
}
