using System;

namespace Unity
{
	// Token: 0x02000BCF RID: 3023
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06006B64 RID: 27492 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
