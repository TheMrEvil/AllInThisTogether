using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000470 RID: 1136
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct IdIterator
	{
		// Token: 0x06002BFC RID: 11260 RVA: 0x00105E2D File Offset: 0x0010402D
		public void Create(XPathNavigator context, string value)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.idrefs = XmlConvert.SplitString(value);
			this.idx = -1;
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x00105E54 File Offset: 0x00104054
		public bool MoveNext()
		{
			for (;;)
			{
				this.idx++;
				if (this.idx >= this.idrefs.Length)
				{
					break;
				}
				if (this.navCurrent.MoveToId(this.idrefs[this.idx]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x00105E91 File Offset: 0x00104091
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040022CD RID: 8909
		private XPathNavigator navCurrent;

		// Token: 0x040022CE RID: 8910
		private string[] idrefs;

		// Token: 0x040022CF RID: 8911
		private int idx;
	}
}
