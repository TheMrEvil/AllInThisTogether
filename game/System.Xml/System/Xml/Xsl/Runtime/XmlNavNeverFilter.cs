using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000475 RID: 1141
	internal class XmlNavNeverFilter : XmlNavigatorFilter
	{
		// Token: 0x06002C20 RID: 11296 RVA: 0x0010603E File Offset: 0x0010423E
		public static XmlNavigatorFilter Create()
		{
			return XmlNavNeverFilter.Singleton;
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x00105FFC File Offset: 0x001041FC
		private XmlNavNeverFilter()
		{
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x00106045 File Offset: 0x00104245
		public override bool MoveToContent(XPathNavigator navigator)
		{
			return XmlNavNeverFilter.MoveToFirstAttributeContent(navigator);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x0010604D File Offset: 0x0010424D
		public override bool MoveToNextContent(XPathNavigator navigator)
		{
			return XmlNavNeverFilter.MoveToNextAttributeContent(navigator);
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0010600C File Offset: 0x0010420C
		public override bool MoveToFollowingSibling(XPathNavigator navigator)
		{
			return navigator.MoveToNext();
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x00106014 File Offset: 0x00104214
		public override bool MoveToPreviousSibling(XPathNavigator navigator)
		{
			return navigator.MoveToPrevious();
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x0010601C File Offset: 0x0010421C
		public override bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navEnd)
		{
			return navigator.MoveToFollowing(XPathNodeType.All, navEnd);
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool IsFiltered(XPathNavigator navigator)
		{
			return false;
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x00106055 File Offset: 0x00104255
		public static bool MoveToFirstAttributeContent(XPathNavigator navigator)
		{
			return navigator.MoveToFirstAttribute() || navigator.MoveToFirstChild();
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x00106067 File Offset: 0x00104267
		public static bool MoveToNextAttributeContent(XPathNavigator navigator)
		{
			if (navigator.NodeType == XPathNodeType.Attribute)
			{
				if (!navigator.MoveToNextAttribute())
				{
					navigator.MoveToParent();
					if (!navigator.MoveToFirstChild())
					{
						navigator.MoveToFirstAttribute();
						while (navigator.MoveToNextAttribute())
						{
						}
						return false;
					}
				}
				return true;
			}
			return navigator.MoveToNext();
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x001060A2 File Offset: 0x001042A2
		// Note: this type is marked as 'beforefieldinit'.
		static XmlNavNeverFilter()
		{
		}

		// Token: 0x040022D6 RID: 8918
		private static XmlNavigatorFilter Singleton = new XmlNavNeverFilter();
	}
}
