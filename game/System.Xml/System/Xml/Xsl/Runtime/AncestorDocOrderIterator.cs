using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000455 RID: 1109
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct AncestorDocOrderIterator
	{
		// Token: 0x06002B48 RID: 11080 RVA: 0x00103ADC File Offset: 0x00101CDC
		public void Create(XPathNavigator context, XmlNavigatorFilter filter, bool orSelf)
		{
			AncestorIterator ancestorIterator = default(AncestorIterator);
			ancestorIterator.Create(context, filter, orSelf);
			this.stack.Reset();
			while (ancestorIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = ancestorIterator.Current;
				this.stack.Push(xpathNavigator.Clone());
			}
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x00103B28 File Offset: 0x00101D28
		public bool MoveNext()
		{
			if (this.stack.IsEmpty)
			{
				return false;
			}
			this.navCurrent = this.stack.Pop();
			return true;
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06002B4A RID: 11082 RVA: 0x00103B4B File Offset: 0x00101D4B
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002258 RID: 8792
		private XmlNavigatorStack stack;

		// Token: 0x04002259 RID: 8793
		private XPathNavigator navCurrent;
	}
}
