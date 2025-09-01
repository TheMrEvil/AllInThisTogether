using System;

namespace Unity
{
	// Token: 0x02000692 RID: 1682
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x0600432D RID: 17197 RVA: 0x0016DCBC File Offset: 0x0016BEBC
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
