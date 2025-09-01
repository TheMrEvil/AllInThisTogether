using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000456 RID: 1110
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathFollowingIterator
	{
		// Token: 0x06002B4B RID: 11083 RVA: 0x00103B53 File Offset: 0x00101D53
		public void Create(XPathNavigator input, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
			this.filter = filter;
			this.needFirst = true;
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x00103B75 File Offset: 0x00101D75
		public bool MoveNext()
		{
			if (!this.needFirst)
			{
				return this.filter.MoveToFollowing(this.navCurrent, null);
			}
			if (!XPathFollowingIterator.MoveFirst(this.filter, this.navCurrent))
			{
				return false;
			}
			this.needFirst = false;
			return true;
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x00103BAF File Offset: 0x00101DAF
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x00103BB8 File Offset: 0x00101DB8
		internal static bool MoveFirst(XmlNavigatorFilter filter, XPathNavigator nav)
		{
			if (nav.NodeType == XPathNodeType.Attribute || nav.NodeType == XPathNodeType.Namespace)
			{
				if (!nav.MoveToParent())
				{
					return false;
				}
				if (!filter.MoveToFollowing(nav, null))
				{
					return false;
				}
			}
			else
			{
				if (!nav.MoveToNonDescendant())
				{
					return false;
				}
				if (filter.IsFiltered(nav) && !filter.MoveToFollowing(nav, null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400225A RID: 8794
		private XmlNavigatorFilter filter;

		// Token: 0x0400225B RID: 8795
		private XPathNavigator navCurrent;

		// Token: 0x0400225C RID: 8796
		private bool needFirst;
	}
}
