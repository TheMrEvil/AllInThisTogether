using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000454 RID: 1108
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct AncestorIterator
	{
		// Token: 0x06002B45 RID: 11077 RVA: 0x00103A60 File Offset: 0x00101C60
		public void Create(XPathNavigator context, XmlNavigatorFilter filter, bool orSelf)
		{
			this.filter = filter;
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.haveCurrent = (orSelf && !this.filter.IsFiltered(this.navCurrent));
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x00103A9B File Offset: 0x00101C9B
		public bool MoveNext()
		{
			if (this.haveCurrent)
			{
				this.haveCurrent = false;
				return true;
			}
			while (this.navCurrent.MoveToParent())
			{
				if (!this.filter.IsFiltered(this.navCurrent))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x00103AD1 File Offset: 0x00101CD1
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002255 RID: 8789
		private XmlNavigatorFilter filter;

		// Token: 0x04002256 RID: 8790
		private XPathNavigator navCurrent;

		// Token: 0x04002257 RID: 8791
		private bool haveCurrent;
	}
}
