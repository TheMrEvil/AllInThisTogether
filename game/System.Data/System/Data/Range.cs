using System;

namespace System.Data
{
	// Token: 0x02000116 RID: 278
	internal struct Range
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x0003FE0C File Offset: 0x0003E00C
		public Range(int min, int max)
		{
			if (min > max)
			{
				throw ExceptionBuilder.RangeArgument(min, max);
			}
			this._min = min;
			this._max = max;
			this._isNotNull = true;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0003FE2F File Offset: 0x0003E02F
		public int Count
		{
			get
			{
				if (!this.IsNull)
				{
					return this._max - this._min + 1;
				}
				return 0;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0003FE4A File Offset: 0x0003E04A
		public bool IsNull
		{
			get
			{
				return !this._isNotNull;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0003FE55 File Offset: 0x0003E055
		public int Max
		{
			get
			{
				this.CheckNull();
				return this._max;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x0003FE63 File Offset: 0x0003E063
		public int Min
		{
			get
			{
				this.CheckNull();
				return this._min;
			}
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003FE71 File Offset: 0x0003E071
		internal void CheckNull()
		{
			if (this.IsNull)
			{
				throw ExceptionBuilder.NullRange();
			}
		}

		// Token: 0x04000996 RID: 2454
		private int _min;

		// Token: 0x04000997 RID: 2455
		private int _max;

		// Token: 0x04000998 RID: 2456
		private bool _isNotNull;
	}
}
