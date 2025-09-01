using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000459 RID: 1113
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct PrecedingIterator
	{
		// Token: 0x06002B54 RID: 11092 RVA: 0x00103D58 File Offset: 0x00101F58
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.navCurrent.MoveToRoot();
			this.stack.Reset();
			if (!this.navCurrent.IsSamePosition(context))
			{
				if (!filter.IsFiltered(this.navCurrent))
				{
					this.stack.Push(this.navCurrent.Clone());
				}
				while (filter.MoveToFollowing(this.navCurrent, context))
				{
					this.stack.Push(this.navCurrent.Clone());
				}
			}
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x00103DE6 File Offset: 0x00101FE6
		public bool MoveNext()
		{
			if (this.stack.IsEmpty)
			{
				return false;
			}
			this.navCurrent = this.stack.Pop();
			return true;
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x00103E09 File Offset: 0x00102009
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002267 RID: 8807
		private XmlNavigatorStack stack;

		// Token: 0x04002268 RID: 8808
		private XPathNavigator navCurrent;
	}
}
