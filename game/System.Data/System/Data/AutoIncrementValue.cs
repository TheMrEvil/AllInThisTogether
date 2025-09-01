using System;
using System.Runtime.CompilerServices;

namespace System.Data
{
	// Token: 0x02000085 RID: 133
	internal abstract class AutoIncrementValue
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001A253 File Offset: 0x00018453
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x0001A25B File Offset: 0x0001845B
		internal bool Auto
		{
			[CompilerGenerated]
			get
			{
				return this.<Auto>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Auto>k__BackingField = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006B0 RID: 1712
		// (set) Token: 0x060006B1 RID: 1713
		internal abstract object Current { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006B2 RID: 1714
		// (set) Token: 0x060006B3 RID: 1715
		internal abstract long Seed { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006B4 RID: 1716
		// (set) Token: 0x060006B5 RID: 1717
		internal abstract long Step { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006B6 RID: 1718
		internal abstract Type DataType { get; }

		// Token: 0x060006B7 RID: 1719
		internal abstract void SetCurrent(object value, IFormatProvider formatProvider);

		// Token: 0x060006B8 RID: 1720
		internal abstract void SetCurrentAndIncrement(object value);

		// Token: 0x060006B9 RID: 1721
		internal abstract void MoveAfter();

		// Token: 0x060006BA RID: 1722 RVA: 0x0001A264 File Offset: 0x00018464
		internal AutoIncrementValue Clone()
		{
			AutoIncrementInt64 autoIncrementInt = (this is AutoIncrementInt64) ? new AutoIncrementInt64() : new AutoIncrementBigInteger();
			autoIncrementInt.Auto = this.Auto;
			autoIncrementInt.Seed = this.Seed;
			autoIncrementInt.Step = this.Step;
			autoIncrementInt.Current = this.Current;
			return autoIncrementInt;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00003D93 File Offset: 0x00001F93
		protected AutoIncrementValue()
		{
		}

		// Token: 0x04000693 RID: 1683
		[CompilerGenerated]
		private bool <Auto>k__BackingField;
	}
}
