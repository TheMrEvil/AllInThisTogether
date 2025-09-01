using System;

namespace Unity
{
	// Token: 0x0200006A RID: 106
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0000CD4B File Offset: 0x0000AF4B
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
