using System;

namespace System.Buffers
{
	// Token: 0x02000AD6 RID: 2774
	public interface IMemoryOwner<T> : IDisposable
	{
		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060062E4 RID: 25316
		Memory<T> Memory { get; }
	}
}
