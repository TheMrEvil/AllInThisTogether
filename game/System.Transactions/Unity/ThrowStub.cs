using System;

namespace Unity
{
	// Token: 0x02000032 RID: 50
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000034B7 File Offset: 0x000016B7
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
