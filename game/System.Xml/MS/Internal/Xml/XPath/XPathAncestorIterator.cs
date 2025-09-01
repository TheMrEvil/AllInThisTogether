using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000657 RID: 1623
	internal class XPathAncestorIterator : XPathAxisIterator
	{
		// Token: 0x060041CA RID: 16842 RVA: 0x00168805 File Offset: 0x00166A05
		public XPathAncestorIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : base(nav, type, matchSelf)
		{
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x00168810 File Offset: 0x00166A10
		public XPathAncestorIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf) : base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x0016881D File Offset: 0x00166A1D
		public XPathAncestorIterator(XPathAncestorIterator other) : base(other)
		{
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x00168828 File Offset: 0x00166A28
		public override bool MoveNext()
		{
			if (this.first)
			{
				this.first = false;
				if (this.matchSelf && this.Matches)
				{
					this.position = 1;
					return true;
				}
			}
			while (this.nav.MoveToParent())
			{
				if (this.Matches)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x00168883 File Offset: 0x00166A83
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorIterator(this);
		}
	}
}
