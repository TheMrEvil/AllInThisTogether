using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000453 RID: 1107
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct ParentIterator
	{
		// Token: 0x06002B42 RID: 11074 RVA: 0x00103A0B File Offset: 0x00101C0B
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.haveCurrent = (this.navCurrent.MoveToParent() && !filter.IsFiltered(this.navCurrent));
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x00103A44 File Offset: 0x00101C44
		public bool MoveNext()
		{
			if (this.haveCurrent)
			{
				this.haveCurrent = false;
				return true;
			}
			return false;
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002B44 RID: 11076 RVA: 0x00103A58 File Offset: 0x00101C58
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002253 RID: 8787
		private XPathNavigator navCurrent;

		// Token: 0x04002254 RID: 8788
		private bool haveCurrent;
	}
}
