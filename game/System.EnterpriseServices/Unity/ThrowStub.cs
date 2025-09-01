using System;

namespace Unity
{
	// Token: 0x02000079 RID: 121
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x000027E5 File Offset: 0x000009E5
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
