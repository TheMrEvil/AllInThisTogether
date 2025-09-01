using System;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x0200005C RID: 92
	internal static class ThreadingUtilities
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000DD98 File Offset: 0x0000BF98
		public static void Lock(ref object @lock, Action operationToLock)
		{
			object obj = @lock;
			lock (obj)
			{
				operationToLock();
			}
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		public static TResult Lock<TResult>(ref object @lock, Func<TResult> operationToLock)
		{
			TResult result = default(TResult);
			object obj = @lock;
			lock (obj)
			{
				result = operationToLock();
			}
			return result;
		}
	}
}
