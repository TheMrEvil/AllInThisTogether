using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000431 RID: 1073
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct ElementContentIterator
	{
		// Token: 0x06002ACB RID: 10955 RVA: 0x0010230E File Offset: 0x0010050E
		public void Create(XPathNavigator context, string localName, string ns)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.localName = localName;
			this.ns = ns;
			this.needFirst = true;
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x00102338 File Offset: 0x00100538
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !this.navCurrent.MoveToChild(this.localName, this.ns);
				return !this.needFirst;
			}
			return this.navCurrent.MoveToNext(this.localName, this.ns);
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x0010238E File Offset: 0x0010058E
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040021AE RID: 8622
		private string localName;

		// Token: 0x040021AF RID: 8623
		private string ns;

		// Token: 0x040021B0 RID: 8624
		private XPathNavigator navCurrent;

		// Token: 0x040021B1 RID: 8625
		private bool needFirst;
	}
}
