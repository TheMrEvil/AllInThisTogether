using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000430 RID: 1072
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct ContentIterator
	{
		// Token: 0x06002AC8 RID: 10952 RVA: 0x001022B8 File Offset: 0x001004B8
		public void Create(XPathNavigator context)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.needFirst = true;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x001022D3 File Offset: 0x001004D3
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !this.navCurrent.MoveToFirstChild();
				return !this.needFirst;
			}
			return this.navCurrent.MoveToNext();
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x00102306 File Offset: 0x00100506
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040021AC RID: 8620
		private XPathNavigator navCurrent;

		// Token: 0x040021AD RID: 8621
		private bool needFirst;
	}
}
