using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000434 RID: 1076
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct NamespaceIterator
	{
		// Token: 0x06002AD4 RID: 10964 RVA: 0x00102458 File Offset: 0x00100658
		public void Create(XPathNavigator context)
		{
			this.navStack.Reset();
			if (context.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				do
				{
					if (context.LocalName.Length != 0 || context.Value.Length != 0)
					{
						this.navStack.Push(context.Clone());
					}
				}
				while (context.MoveToNextNamespace(XPathNamespaceScope.All));
				context.MoveToParent();
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x001024B4 File Offset: 0x001006B4
		public bool MoveNext()
		{
			if (this.navStack.IsEmpty)
			{
				return false;
			}
			this.navCurrent = this.navStack.Pop();
			return true;
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x001024D7 File Offset: 0x001006D7
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040021B7 RID: 8631
		private XPathNavigator navCurrent;

		// Token: 0x040021B8 RID: 8632
		private XmlNavigatorStack navStack;
	}
}
