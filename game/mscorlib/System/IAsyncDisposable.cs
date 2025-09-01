using System;
using System.Threading.Tasks;

namespace System
{
	// Token: 0x02000136 RID: 310
	public interface IAsyncDisposable
	{
		// Token: 0x06000BE3 RID: 3043
		ValueTask DisposeAsync();
	}
}
