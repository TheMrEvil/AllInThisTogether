using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000432 RID: 1074
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct NodeKindContentIterator
	{
		// Token: 0x06002ACE RID: 10958 RVA: 0x00102396 File Offset: 0x00100596
		public void Create(XPathNavigator context, XPathNodeType nodeType)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.nodeType = nodeType;
			this.needFirst = true;
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x001023B8 File Offset: 0x001005B8
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !this.navCurrent.MoveToChild(this.nodeType);
				return !this.needFirst;
			}
			return this.navCurrent.MoveToNext(this.nodeType);
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x001023F7 File Offset: 0x001005F7
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040021B2 RID: 8626
		private XPathNodeType nodeType;

		// Token: 0x040021B3 RID: 8627
		private XPathNavigator navCurrent;

		// Token: 0x040021B4 RID: 8628
		private bool needFirst;
	}
}
