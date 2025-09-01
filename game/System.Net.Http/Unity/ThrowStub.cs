using System;

namespace Unity
{
	// Token: 0x0200006F RID: 111
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x00002874 File Offset: 0x00000A74
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
