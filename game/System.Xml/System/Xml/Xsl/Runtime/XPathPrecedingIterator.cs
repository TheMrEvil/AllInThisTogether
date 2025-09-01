using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200045A RID: 1114
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathPrecedingIterator
	{
		// Token: 0x06002B57 RID: 11095 RVA: 0x00103E14 File Offset: 0x00102014
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			XPathPrecedingDocOrderIterator xpathPrecedingDocOrderIterator = default(XPathPrecedingDocOrderIterator);
			xpathPrecedingDocOrderIterator.Create(context, filter);
			this.stack.Reset();
			while (xpathPrecedingDocOrderIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathPrecedingDocOrderIterator.Current;
				this.stack.Push(xpathNavigator.Clone());
			}
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x00103E5F File Offset: 0x0010205F
		public bool MoveNext()
		{
			if (this.stack.IsEmpty)
			{
				return false;
			}
			this.navCurrent = this.stack.Pop();
			return true;
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002B59 RID: 11097 RVA: 0x00103E82 File Offset: 0x00102082
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002269 RID: 8809
		private XmlNavigatorStack stack;

		// Token: 0x0400226A RID: 8810
		private XPathNavigator navCurrent;
	}
}
