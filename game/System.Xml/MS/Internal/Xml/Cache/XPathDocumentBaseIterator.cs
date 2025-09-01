using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200066A RID: 1642
	internal abstract class XPathDocumentBaseIterator : XPathNodeIterator
	{
		// Token: 0x06004285 RID: 17029 RVA: 0x0016B881 File Offset: 0x00169A81
		protected XPathDocumentBaseIterator(XPathDocumentNavigator ctxt)
		{
			this.ctxt = new XPathDocumentNavigator(ctxt);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x0016B895 File Offset: 0x00169A95
		protected XPathDocumentBaseIterator(XPathDocumentBaseIterator iter)
		{
			this.ctxt = new XPathDocumentNavigator(iter.ctxt);
			this.pos = iter.pos;
		}

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x0016B8BA File Offset: 0x00169ABA
		public override XPathNavigator Current
		{
			get
			{
				return this.ctxt;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004288 RID: 17032 RVA: 0x0016B8C2 File Offset: 0x00169AC2
		public override int CurrentPosition
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x04002F13 RID: 12051
		protected XPathDocumentNavigator ctxt;

		// Token: 0x04002F14 RID: 12052
		protected int pos;
	}
}
