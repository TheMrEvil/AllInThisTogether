using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200043A RID: 1082
	internal class DocumentOrderComparer : IComparer<XPathNavigator>
	{
		// Token: 0x06002AE4 RID: 10980 RVA: 0x00102BF4 File Offset: 0x00100DF4
		public int Compare(XPathNavigator navThis, XPathNavigator navThat)
		{
			switch (navThis.ComparePosition(navThat))
			{
			case XmlNodeOrder.Before:
				return -1;
			case XmlNodeOrder.After:
				return 1;
			case XmlNodeOrder.Same:
				return 0;
			default:
				if (this.roots == null)
				{
					this.roots = new List<XPathNavigator>();
				}
				if (this.GetDocumentIndex(navThis) >= this.GetDocumentIndex(navThat))
				{
					return 1;
				}
				return -1;
			}
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x00102C4C File Offset: 0x00100E4C
		public int GetDocumentIndex(XPathNavigator nav)
		{
			if (this.roots == null)
			{
				this.roots = new List<XPathNavigator>();
			}
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToRoot();
			for (int i = 0; i < this.roots.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(this.roots[i]))
				{
					return i;
				}
			}
			this.roots.Add(xpathNavigator);
			return this.roots.Count - 1;
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x0000216B File Offset: 0x0000036B
		public DocumentOrderComparer()
		{
		}

		// Token: 0x040021D0 RID: 8656
		private List<XPathNavigator> roots;
	}
}
