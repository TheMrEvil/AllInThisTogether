using System;

namespace Unity
{
	// Token: 0x02000013 RID: 19
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002A04 File Offset: 0x00000C04
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
