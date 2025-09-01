using System;

namespace System.Threading
{
	// Token: 0x020002B1 RID: 689
	internal struct SparselyPopulatedArrayAddInfo<T> where T : class
	{
		// Token: 0x06001E46 RID: 7750 RVA: 0x00070406 File Offset: 0x0006E606
		internal SparselyPopulatedArrayAddInfo(SparselyPopulatedArrayFragment<T> source, int index)
		{
			this._source = source;
			this._index = index;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x00070416 File Offset: 0x0006E616
		internal SparselyPopulatedArrayFragment<T> Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0007041E File Offset: 0x0006E61E
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x04001A98 RID: 6808
		private SparselyPopulatedArrayFragment<T> _source;

		// Token: 0x04001A99 RID: 6809
		private int _index;
	}
}
