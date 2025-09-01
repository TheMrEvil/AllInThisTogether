using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000386 RID: 902
	public static class TaskAsyncEnumerableExtensions
	{
		// Token: 0x06002553 RID: 9555 RVA: 0x00084771 File Offset: 0x00082971
		public static ConfiguredAsyncDisposable ConfigureAwait(this IAsyncDisposable source, bool continueOnCapturedContext)
		{
			return new ConfiguredAsyncDisposable(source, continueOnCapturedContext);
		}

		// Token: 0x06002554 RID: 9556 RVA: 0x0008477C File Offset: 0x0008297C
		public static ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait<T>(this IAsyncEnumerable<T> source, bool continueOnCapturedContext)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(source, continueOnCapturedContext, default(CancellationToken));
		}

		// Token: 0x06002555 RID: 9557 RVA: 0x00084799 File Offset: 0x00082999
		public static ConfiguredCancelableAsyncEnumerable<T> WithCancellation<T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(source, true, cancellationToken);
		}
	}
}
