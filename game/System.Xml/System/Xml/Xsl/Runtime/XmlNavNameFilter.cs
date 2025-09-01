using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000472 RID: 1138
	internal class XmlNavNameFilter : XmlNavigatorFilter
	{
		// Token: 0x06002C06 RID: 11270 RVA: 0x00105E99 File Offset: 0x00104099
		public static XmlNavigatorFilter Create(string localName, string namespaceUri)
		{
			return new XmlNavNameFilter(localName, namespaceUri);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x00105EA2 File Offset: 0x001040A2
		private XmlNavNameFilter(string localName, string namespaceUri)
		{
			this.localName = localName;
			this.namespaceUri = namespaceUri;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00105EB8 File Offset: 0x001040B8
		public override bool MoveToContent(XPathNavigator navigator)
		{
			return navigator.MoveToChild(this.localName, this.namespaceUri);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x00105ECC File Offset: 0x001040CC
		public override bool MoveToNextContent(XPathNavigator navigator)
		{
			return navigator.MoveToNext(this.localName, this.namespaceUri);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x00105ECC File Offset: 0x001040CC
		public override bool MoveToFollowingSibling(XPathNavigator navigator)
		{
			return navigator.MoveToNext(this.localName, this.namespaceUri);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00105EE0 File Offset: 0x001040E0
		public override bool MoveToPreviousSibling(XPathNavigator navigator)
		{
			return navigator.MoveToPrevious(this.localName, this.namespaceUri);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x00105EF4 File Offset: 0x001040F4
		public override bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navEnd)
		{
			return navigator.MoveToFollowing(this.localName, this.namespaceUri, navEnd);
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x00105F09 File Offset: 0x00104109
		public override bool IsFiltered(XPathNavigator navigator)
		{
			return navigator.LocalName != this.localName || navigator.NamespaceURI != this.namespaceUri;
		}

		// Token: 0x040022D0 RID: 8912
		private string localName;

		// Token: 0x040022D1 RID: 8913
		private string namespaceUri;
	}
}
