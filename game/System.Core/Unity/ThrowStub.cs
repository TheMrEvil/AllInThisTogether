using System;

namespace Unity
{
	// Token: 0x020003B9 RID: 953
	internal sealed class ThrowStub : ObjectDisposedException
	{
		// Token: 0x06001C58 RID: 7256 RVA: 0x00003A06 File Offset: 0x00001C06
		public static void ThrowNotSupportedException()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
