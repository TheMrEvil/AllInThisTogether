using System;

namespace System.Buffers
{
	// Token: 0x02000AE2 RID: 2786
	public interface IBufferWriter<T>
	{
		// Token: 0x06006316 RID: 25366
		void Advance(int count);

		// Token: 0x06006317 RID: 25367
		Memory<T> GetMemory(int sizeHint = 0);

		// Token: 0x06006318 RID: 25368
		Span<T> GetSpan(int sizeHint = 0);
	}
}
