using System;

namespace System.Net
{
	// Token: 0x02000562 RID: 1378
	internal static class ExceptionCheck
	{
		// Token: 0x06002C9D RID: 11421 RVA: 0x0009836E File Offset: 0x0009656E
		internal static bool IsFatal(Exception exception)
		{
			return exception is OutOfMemoryException;
		}
	}
}
