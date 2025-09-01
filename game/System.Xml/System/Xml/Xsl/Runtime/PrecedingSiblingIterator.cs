using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200044D RID: 1101
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct PrecedingSiblingIterator
	{
		// Token: 0x06002B2F RID: 11055 RVA: 0x001035BE File Offset: 0x001017BE
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.filter = filter;
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x001035D9 File Offset: 0x001017D9
		public bool MoveNext()
		{
			return this.filter.MoveToPreviousSibling(this.navCurrent);
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002B31 RID: 11057 RVA: 0x001035EC File Offset: 0x001017EC
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002237 RID: 8759
		private XmlNavigatorFilter filter;

		// Token: 0x04002238 RID: 8760
		private XPathNavigator navCurrent;
	}
}
