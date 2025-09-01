using System;

namespace System.Threading
{
	// Token: 0x020002A0 RID: 672
	internal interface IDeferredDisposable
	{
		// Token: 0x06001DD8 RID: 7640
		void OnFinalRelease(bool disposed);
	}
}
