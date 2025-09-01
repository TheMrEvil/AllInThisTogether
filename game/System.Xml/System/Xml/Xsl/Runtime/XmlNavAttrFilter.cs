using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000474 RID: 1140
	internal class XmlNavAttrFilter : XmlNavigatorFilter
	{
		// Token: 0x06002C17 RID: 11287 RVA: 0x00105FF5 File Offset: 0x001041F5
		public static XmlNavigatorFilter Create()
		{
			return XmlNavAttrFilter.Singleton;
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x00105FFC File Offset: 0x001041FC
		private XmlNavAttrFilter()
		{
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x00106004 File Offset: 0x00104204
		public override bool MoveToContent(XPathNavigator navigator)
		{
			return navigator.MoveToFirstChild();
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x0010600C File Offset: 0x0010420C
		public override bool MoveToNextContent(XPathNavigator navigator)
		{
			return navigator.MoveToNext();
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0010600C File Offset: 0x0010420C
		public override bool MoveToFollowingSibling(XPathNavigator navigator)
		{
			return navigator.MoveToNext();
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x00106014 File Offset: 0x00104214
		public override bool MoveToPreviousSibling(XPathNavigator navigator)
		{
			return navigator.MoveToPrevious();
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x0010601C File Offset: 0x0010421C
		public override bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navEnd)
		{
			return navigator.MoveToFollowing(XPathNodeType.All, navEnd);
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x00106027 File Offset: 0x00104227
		public override bool IsFiltered(XPathNavigator navigator)
		{
			return navigator.NodeType == XPathNodeType.Attribute;
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x00106032 File Offset: 0x00104232
		// Note: this type is marked as 'beforefieldinit'.
		static XmlNavAttrFilter()
		{
		}

		// Token: 0x040022D5 RID: 8917
		private static XmlNavigatorFilter Singleton = new XmlNavAttrFilter();
	}
}
