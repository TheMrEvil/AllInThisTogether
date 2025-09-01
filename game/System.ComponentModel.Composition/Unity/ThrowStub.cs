using System;

namespace Unity
{
	// Token: 0x02000109 RID: 265
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x00015F83 File Offset: 0x00014183
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
