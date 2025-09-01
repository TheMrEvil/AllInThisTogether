using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200065B RID: 1627
	internal class XPathChildIterator : XPathAxisIterator
	{
		// Token: 0x060041E8 RID: 16872 RVA: 0x00168BFD File Offset: 0x00166DFD
		public XPathChildIterator(XPathNavigator nav, XPathNodeType type) : base(nav, type, false)
		{
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x00168C08 File Offset: 0x00166E08
		public XPathChildIterator(XPathNavigator nav, string name, string namespaceURI) : base(nav, name, namespaceURI, false)
		{
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x0016881D File Offset: 0x00166A1D
		public XPathChildIterator(XPathChildIterator it) : base(it)
		{
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x00168C14 File Offset: 0x00166E14
		public override XPathNodeIterator Clone()
		{
			return new XPathChildIterator(this);
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x00168C1C File Offset: 0x00166E1C
		public override bool MoveNext()
		{
			while (this.first ? this.nav.MoveToFirstChild() : this.nav.MoveToNext())
			{
				this.first = false;
				if (this.Matches)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}
	}
}
