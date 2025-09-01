using System;

namespace System.Buffers
{
	// Token: 0x02000AD7 RID: 2775
	public interface IPinnable
	{
		// Token: 0x060062E5 RID: 25317
		MemoryHandle Pin(int elementIndex);

		// Token: 0x060062E6 RID: 25318
		void Unpin();
	}
}
