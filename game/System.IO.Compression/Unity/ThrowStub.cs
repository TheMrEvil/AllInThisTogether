using System;

namespace Unity
{
	// Token: 0x0200004D RID: 77
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00009DA2 File Offset: 0x00007FA2
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
