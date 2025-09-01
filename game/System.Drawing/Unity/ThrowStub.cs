using System;

namespace Unity
{
	// Token: 0x0200015B RID: 347
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x00022B04 File Offset: 0x00020D04
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
