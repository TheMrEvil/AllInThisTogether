using System;

namespace Unity
{
	// Token: 0x02000090 RID: 144
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x0000B44C File Offset: 0x0000964C
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
