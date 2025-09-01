using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200044C RID: 1100
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct FollowingSiblingMergeIterator
	{
		// Token: 0x06002B2C RID: 11052 RVA: 0x00103594 File Offset: 0x00101794
		public void Create(XmlNavigatorFilter filter)
		{
			this.wrapped.Create(filter);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x001035A2 File Offset: 0x001017A2
		public IteratorResult MoveNext(XPathNavigator navigator)
		{
			return this.wrapped.MoveNext(navigator, false);
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x001035B1 File Offset: 0x001017B1
		public XPathNavigator Current
		{
			get
			{
				return this.wrapped.Current;
			}
		}

		// Token: 0x04002236 RID: 8758
		private ContentMergeIterator wrapped;
	}
}
