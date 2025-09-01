using System;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x0200003D RID: 61
	public interface INativeDisposable : IDisposable
	{
		// Token: 0x0600010F RID: 271
		JobHandle Dispose(JobHandle inputDeps);
	}
}
