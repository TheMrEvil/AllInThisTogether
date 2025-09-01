using System;

namespace Unity
{
	// Token: 0x0200087D RID: 2173
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060044C9 RID: 17609 RVA: 0x00011F54 File Offset: 0x00010154
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
