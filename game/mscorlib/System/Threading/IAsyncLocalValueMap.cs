using System;

namespace System.Threading
{
	// Token: 0x02000284 RID: 644
	internal interface IAsyncLocalValueMap
	{
		// Token: 0x06001D64 RID: 7524
		bool TryGetValue(IAsyncLocal key, out object value);

		// Token: 0x06001D65 RID: 7525
		IAsyncLocalValueMap Set(IAsyncLocal key, object value);
	}
}
