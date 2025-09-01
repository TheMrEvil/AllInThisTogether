using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001F8 RID: 504
	internal struct Pair<T, U>
	{
		// Token: 0x06000C5D RID: 3165 RVA: 0x0002B304 File Offset: 0x00029504
		public Pair(T first, U second)
		{
			this._first = first;
			this._second = second;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0002B314 File Offset: 0x00029514
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0002B31C File Offset: 0x0002951C
		public T First
		{
			get
			{
				return this._first;
			}
			set
			{
				this._first = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0002B325 File Offset: 0x00029525
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0002B32D File Offset: 0x0002952D
		public U Second
		{
			get
			{
				return this._second;
			}
			set
			{
				this._second = value;
			}
		}

		// Token: 0x040008BD RID: 2237
		internal T _first;

		// Token: 0x040008BE RID: 2238
		internal U _second;
	}
}
