using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200044B RID: 1099
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct FollowingSiblingIterator
	{
		// Token: 0x06002B29 RID: 11049 RVA: 0x0010355E File Offset: 0x0010175E
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.filter = filter;
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x00103579 File Offset: 0x00101779
		public bool MoveNext()
		{
			return this.filter.MoveToFollowingSibling(this.navCurrent);
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x0010358C File Offset: 0x0010178C
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002234 RID: 8756
		private XmlNavigatorFilter filter;

		// Token: 0x04002235 RID: 8757
		private XPathNavigator navCurrent;
	}
}
