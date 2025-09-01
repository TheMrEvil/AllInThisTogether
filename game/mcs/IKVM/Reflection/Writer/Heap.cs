using System;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200007D RID: 125
	internal abstract class Heap
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x000144D0 File Offset: 0x000126D0
		internal void Write(MetadataWriter mw)
		{
			int position = mw.Position;
			this.WriteImpl(mw);
			int num = this.Length - this.unalignedlength;
			for (int i = 0; i < num; i++)
			{
				mw.Write(0);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001450C File Offset: 0x0001270C
		internal bool IsBig
		{
			get
			{
				return this.Length > 65535;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001451B File Offset: 0x0001271B
		internal int Length
		{
			get
			{
				if (!this.frozen)
				{
					throw new InvalidOperationException();
				}
				return this.unalignedlength + 3 & -4;
			}
		}

		// Token: 0x060006C1 RID: 1729
		protected abstract void WriteImpl(MetadataWriter mw);

		// Token: 0x060006C2 RID: 1730 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected Heap()
		{
		}

		// Token: 0x04000284 RID: 644
		protected bool frozen;

		// Token: 0x04000285 RID: 645
		protected int unalignedlength;
	}
}
