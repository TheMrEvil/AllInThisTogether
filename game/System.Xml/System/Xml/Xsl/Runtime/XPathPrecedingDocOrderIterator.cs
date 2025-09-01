using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200045B RID: 1115
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathPrecedingDocOrderIterator
	{
		// Token: 0x06002B5A RID: 11098 RVA: 0x00103E8A File Offset: 0x0010208A
		public void Create(XPathNavigator input, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
			this.filter = filter;
			this.PushAncestors();
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x00103EAC File Offset: 0x001020AC
		public bool MoveNext()
		{
			if (!this.navStack.IsEmpty)
			{
				while (!this.filter.MoveToFollowing(this.navCurrent, this.navStack.Peek()))
				{
					this.navCurrent.MoveTo(this.navStack.Pop());
					if (this.navStack.IsEmpty)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x00103F0B File Offset: 0x0010210B
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x00103F13 File Offset: 0x00102113
		private void PushAncestors()
		{
			this.navStack.Reset();
			do
			{
				this.navStack.Push(this.navCurrent.Clone());
			}
			while (this.navCurrent.MoveToParent());
			this.navStack.Pop();
		}

		// Token: 0x0400226B RID: 8811
		private XmlNavigatorFilter filter;

		// Token: 0x0400226C RID: 8812
		private XPathNavigator navCurrent;

		// Token: 0x0400226D RID: 8813
		private XmlNavigatorStack navStack;
	}
}
