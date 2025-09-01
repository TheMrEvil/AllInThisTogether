using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200044E RID: 1102
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct PrecedingSiblingDocOrderIterator
	{
		// Token: 0x06002B32 RID: 11058 RVA: 0x001035F4 File Offset: 0x001017F4
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.filter = filter;
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.navEnd = XmlQueryRuntime.SyncToNavigator(this.navEnd, context);
			this.needFirst = true;
			this.useCompPos = this.filter.IsFiltered(context);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x00103648 File Offset: 0x00101848
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				if (!this.navCurrent.MoveToParent())
				{
					return false;
				}
				if (!this.filter.MoveToContent(this.navCurrent))
				{
					return false;
				}
				this.needFirst = false;
			}
			else if (!this.filter.MoveToFollowingSibling(this.navCurrent))
			{
				return false;
			}
			if (this.useCompPos)
			{
				return this.navCurrent.ComparePosition(this.navEnd) == XmlNodeOrder.Before;
			}
			if (this.navCurrent.IsSamePosition(this.navEnd))
			{
				this.useCompPos = true;
				return false;
			}
			return true;
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x001036D9 File Offset: 0x001018D9
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002239 RID: 8761
		private XmlNavigatorFilter filter;

		// Token: 0x0400223A RID: 8762
		private XPathNavigator navCurrent;

		// Token: 0x0400223B RID: 8763
		private XPathNavigator navEnd;

		// Token: 0x0400223C RID: 8764
		private bool needFirst;

		// Token: 0x0400223D RID: 8765
		private bool useCompPos;
	}
}
