using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000473 RID: 1139
	internal class XmlNavTypeFilter : XmlNavigatorFilter
	{
		// Token: 0x06002C0E RID: 11278 RVA: 0x00105F34 File Offset: 0x00104134
		static XmlNavTypeFilter()
		{
			XmlNavTypeFilter.TypeFilters[1] = new XmlNavTypeFilter(XPathNodeType.Element);
			XmlNavTypeFilter.TypeFilters[4] = new XmlNavTypeFilter(XPathNodeType.Text);
			XmlNavTypeFilter.TypeFilters[7] = new XmlNavTypeFilter(XPathNodeType.ProcessingInstruction);
			XmlNavTypeFilter.TypeFilters[8] = new XmlNavTypeFilter(XPathNodeType.Comment);
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x00105F81 File Offset: 0x00104181
		public static XmlNavigatorFilter Create(XPathNodeType nodeType)
		{
			return XmlNavTypeFilter.TypeFilters[(int)nodeType];
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00105F8A File Offset: 0x0010418A
		private XmlNavTypeFilter(XPathNodeType nodeType)
		{
			this.nodeType = nodeType;
			this.mask = XPathNavigator.GetContentKindMask(nodeType);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x00105FA5 File Offset: 0x001041A5
		public override bool MoveToContent(XPathNavigator navigator)
		{
			return navigator.MoveToChild(this.nodeType);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x00105FB3 File Offset: 0x001041B3
		public override bool MoveToNextContent(XPathNavigator navigator)
		{
			return navigator.MoveToNext(this.nodeType);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x00105FB3 File Offset: 0x001041B3
		public override bool MoveToFollowingSibling(XPathNavigator navigator)
		{
			return navigator.MoveToNext(this.nodeType);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x00105FC1 File Offset: 0x001041C1
		public override bool MoveToPreviousSibling(XPathNavigator navigator)
		{
			return navigator.MoveToPrevious(this.nodeType);
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x00105FCF File Offset: 0x001041CF
		public override bool MoveToFollowing(XPathNavigator navigator, XPathNavigator navEnd)
		{
			return navigator.MoveToFollowing(this.nodeType, navEnd);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x00105FDE File Offset: 0x001041DE
		public override bool IsFiltered(XPathNavigator navigator)
		{
			return (1 << (int)navigator.NodeType & this.mask) == 0;
		}

		// Token: 0x040022D2 RID: 8914
		private static XmlNavigatorFilter[] TypeFilters = new XmlNavigatorFilter[9];

		// Token: 0x040022D3 RID: 8915
		private XPathNodeType nodeType;

		// Token: 0x040022D4 RID: 8916
		private int mask;
	}
}
