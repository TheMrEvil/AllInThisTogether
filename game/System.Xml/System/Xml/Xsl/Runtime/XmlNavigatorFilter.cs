using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000471 RID: 1137
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class XmlNavigatorFilter
	{
		// Token: 0x06002BFF RID: 11263
		public abstract bool MoveToContent(XPathNavigator navigator);

		// Token: 0x06002C00 RID: 11264
		public abstract bool MoveToNextContent(XPathNavigator navigator);

		// Token: 0x06002C01 RID: 11265
		public abstract bool MoveToFollowingSibling(XPathNavigator navigator);

		// Token: 0x06002C02 RID: 11266
		public abstract bool MoveToPreviousSibling(XPathNavigator navigator);

		// Token: 0x06002C03 RID: 11267
		public abstract bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navigatorEnd);

		// Token: 0x06002C04 RID: 11268
		public abstract bool IsFiltered(XPathNavigator navigator);

		// Token: 0x06002C05 RID: 11269 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlNavigatorFilter()
		{
		}
	}
}
