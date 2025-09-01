using System;

namespace Unity
{
	// Token: 0x0200011D RID: 285
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
