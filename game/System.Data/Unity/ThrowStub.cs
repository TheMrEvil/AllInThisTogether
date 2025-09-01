using System;

namespace Unity
{
	// Token: 0x020003F7 RID: 1015
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06002FB4 RID: 12212 RVA: 0x0004549D File Offset: 0x0004369D
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
