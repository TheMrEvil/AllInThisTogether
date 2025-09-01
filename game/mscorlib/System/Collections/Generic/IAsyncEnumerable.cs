using System;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000A90 RID: 2704
	public interface IAsyncEnumerable<out T>
	{
		// Token: 0x0600610C RID: 24844
		IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken));
	}
}
