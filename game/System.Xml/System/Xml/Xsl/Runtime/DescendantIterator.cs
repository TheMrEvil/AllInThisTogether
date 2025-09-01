using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000450 RID: 1104
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DescendantIterator
	{
		// Token: 0x06002B3C RID: 11068 RVA: 0x0010388C File Offset: 0x00101A8C
		public void Create(XPathNavigator input, XmlNavigatorFilter filter, bool orSelf)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
			this.filter = filter;
			if (input.NodeType == XPathNodeType.Root)
			{
				this.navEnd = null;
			}
			else
			{
				this.navEnd = XmlQueryRuntime.SyncToNavigator(this.navEnd, input);
				this.navEnd.MoveToNonDescendant();
			}
			this.hasFirst = (orSelf && !this.filter.IsFiltered(this.navCurrent));
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00103901 File Offset: 0x00101B01
		public bool MoveNext()
		{
			if (this.hasFirst)
			{
				this.hasFirst = false;
				return true;
			}
			return this.filter.MoveToFollowing(this.navCurrent, this.navEnd);
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x0010392B File Offset: 0x00101B2B
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002245 RID: 8773
		private XmlNavigatorFilter filter;

		// Token: 0x04002246 RID: 8774
		private XPathNavigator navCurrent;

		// Token: 0x04002247 RID: 8775
		private XPathNavigator navEnd;

		// Token: 0x04002248 RID: 8776
		private bool hasFirst;
	}
}
