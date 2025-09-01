using System;
using System.Data.Common;
using System.Numerics;

namespace System.Data
{
	// Token: 0x02000087 RID: 135
	internal sealed class AutoIncrementBigInteger : AutoIncrementValue
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0001A406 File Offset: 0x00018606
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x0001A413 File Offset: 0x00018613
		internal override object Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = (BigInteger)value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001A421 File Offset: 0x00018621
		internal override Type DataType
		{
			get
			{
				return typeof(BigInteger);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0001A42D File Offset: 0x0001862D
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0001A435 File Offset: 0x00018635
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

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0001A46B File Offset: 0x0001866B
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0001A478 File Offset: 0x00018678
		internal override long Step
		{
			get
			{
				return (long)this._step;
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

		// Token: 0x060006CF RID: 1743 RVA: 0x0001A4DD File Offset: 0x000186DD
		internal override void MoveAfter()
		{
			this._current += this._step;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001A4F6 File Offset: 0x000186F6
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this._current = BigIntegerStorage.ConvertToBigInteger(value, formatProvider);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001A508 File Offset: 0x00018708
		internal override void SetCurrentAndIncrement(object value)
		{
			BigInteger bigInteger = (BigInteger)value;
			if (this.BoundaryCheck(bigInteger))
			{
				this._current = bigInteger + this._step;
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001A537 File Offset: 0x00018737
		private bool BoundaryCheck(BigInteger value)
		{
			return (this._step < 0L && value <= this._current) || (0L < this._step && this._current <= value);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001A575 File Offset: 0x00018775
		public AutoIncrementBigInteger()
		{
		}

		// Token: 0x04000697 RID: 1687
		private BigInteger _current;

		// Token: 0x04000698 RID: 1688
		private long _seed;

		// Token: 0x04000699 RID: 1689
		private BigInteger _step = 1;
	}
}
