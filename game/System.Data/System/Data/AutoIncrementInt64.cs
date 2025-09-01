using System;
using System.Data.Common;
using System.Globalization;
using System.Numerics;

namespace System.Data
{
	// Token: 0x02000086 RID: 134
	internal sealed class AutoIncrementInt64 : AutoIncrementValue
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001A2B5 File Offset: 0x000184B5
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0001A2C2 File Offset: 0x000184C2
		internal override object Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = (long)value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001A2D0 File Offset: 0x000184D0
		internal override Type DataType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001A2DC File Offset: 0x000184DC
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0001A2E4 File Offset: 0x000184E4
		internal override long Seed
		{
			get
			{
				return this._seed;
			}
			set
			{
				if (this._current == this._seed || this.BoundaryCheck(value))
				{
					this._current = value;
				}
				this._seed = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001A310 File Offset: 0x00018510
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001A318 File Offset: 0x00018518
		internal override long Step
		{
			get
			{
				return this._step;
			}
			set
			{
				if (value == 0L)
				{
					throw ExceptionBuilder.AutoIncrementSeed();
				}
				if (this._step != value)
				{
					if (this._current != this.Seed)
					{
						this._current = this._current - this._step + value;
					}
					this._step = value;
				}
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001A356 File Offset: 0x00018556
		internal override void MoveAfter()
		{
			this._current += this._step;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001A36B File Offset: 0x0001856B
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this._current = Convert.ToInt64(value, formatProvider);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001A37C File Offset: 0x0001857C
		internal override void SetCurrentAndIncrement(object value)
		{
			long num = (long)SqlConvert.ChangeType2(value, StorageType.Int64, typeof(long), CultureInfo.InvariantCulture);
			if (this.BoundaryCheck(num))
			{
				this._current = num + this._step;
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001A3C2 File Offset: 0x000185C2
		private bool BoundaryCheck(BigInteger value)
		{
			return (this._step < 0L && value <= this._current) || (0L < this._step && this._current <= value);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001A3F6 File Offset: 0x000185F6
		public AutoIncrementInt64()
		{
		}

		// Token: 0x04000694 RID: 1684
		private long _current;

		// Token: 0x04000695 RID: 1685
		private long _seed;

		// Token: 0x04000696 RID: 1686
		private long _step = 1L;
	}
}
