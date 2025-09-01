using System;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000673 RID: 1651
	internal sealed class XPathNodePageInfo
	{
		// Token: 0x0600430B RID: 17163 RVA: 0x0016D60B File Offset: 0x0016B80B
		public XPathNodePageInfo(XPathNode[] pagePrev, int pageNum)
		{
			this._pagePrev = pagePrev;
			this._pageNum = pageNum;
			this._nodeCount = 1;
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x0600430C RID: 17164 RVA: 0x0016D628 File Offset: 0x0016B828
		public int PageNumber
		{
			get
			{
				return this._pageNum;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x0016D630 File Offset: 0x0016B830
		// (set) Token: 0x0600430E RID: 17166 RVA: 0x0016D638 File Offset: 0x0016B838
		public int NodeCount
		{
			get
			{
				return this._nodeCount;
			}
			set
			{
				this._nodeCount = value;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x0016D641 File Offset: 0x0016B841
		public XPathNode[] PreviousPage
		{
			get
			{
				return this._pagePrev;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x0016D649 File Offset: 0x0016B849
		// (set) Token: 0x06004311 RID: 17169 RVA: 0x0016D651 File Offset: 0x0016B851
		public XPathNode[] NextPage
		{
			get
			{
				return this._pageNext;
			}
			set
			{
				this._pageNext = value;
			}
		}

		// Token: 0x04002F3A RID: 12090
		private int _pageNum;

		// Token: 0x04002F3B RID: 12091
		private int _nodeCount;

		// Token: 0x04002F3C RID: 12092
		private XPathNode[] _pagePrev;

		// Token: 0x04002F3D RID: 12093
		private XPathNode[] _pageNext;
	}
}
