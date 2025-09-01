using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000671 RID: 1649
	internal struct XPathNodeRef
	{
		// Token: 0x060042F3 RID: 17139 RVA: 0x0016CE9B File Offset: 0x0016B09B
		public XPathNodeRef(XPathNode[] page, int idx)
		{
			this._page = page;
			this._idx = idx;
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060042F4 RID: 17140 RVA: 0x0016CEAB File Offset: 0x0016B0AB
		public XPathNode[] Page
		{
			get
			{
				return this._page;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060042F5 RID: 17141 RVA: 0x0016CEB3 File Offset: 0x0016B0B3
		public int Index
		{
			get
			{
				return this._idx;
			}
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x0016CEBB File Offset: 0x0016B0BB
		public override int GetHashCode()
		{
			return XPathNodeHelper.GetLocation(this._page, this._idx);
		}

		// Token: 0x04002F38 RID: 12088
		private XPathNode[] _page;

		// Token: 0x04002F39 RID: 12089
		private int _idx;
	}
}
