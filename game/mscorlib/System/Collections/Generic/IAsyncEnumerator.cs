using System;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
	// Token: 0x02000A91 RID: 2705
	public interface IAsyncEnumerator<out T> : IAsyncDisposable
	{
		// Token: 0x0600610D RID: 24845
		ValueTask<bool> MoveNextAsync();

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600610E RID: 24846
		T Current { get; }
	}
}
