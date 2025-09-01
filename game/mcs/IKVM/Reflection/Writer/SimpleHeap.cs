using System;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200007E RID: 126
	internal abstract class SimpleHeap : Heap
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x00014536 File Offset: 0x00012736
		internal void Freeze()
		{
			if (this.frozen)
			{
				throw new InvalidOperationException();
			}
			this.frozen = true;
			this.unalignedlength = this.GetLength();
		}

		// Token: 0x060006C4 RID: 1732
		protected abstract int GetLength();

		// Token: 0x060006C5 RID: 1733 RVA: 0x00014559 File Offset: 0x00012759
		protected SimpleHeap()
		{
		}
	}
}
