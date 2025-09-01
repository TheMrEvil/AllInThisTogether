using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x0200000A RID: 10
	internal static class CancellationHelper
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002820 File Offset: 0x00000A20
		internal static bool ShouldWrapInOperationCanceledException(Exception exception, CancellationToken cancellationToken)
		{
			return !(exception is OperationCanceledException) && cancellationToken.IsCancellationRequested;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002833 File Offset: 0x00000A33
		internal static Exception CreateOperationCanceledException(Exception innerException, CancellationToken cancellationToken)
		{
			return new TaskCanceledException(CancellationHelper.s_cancellationMessage, innerException, cancellationToken);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002841 File Offset: 0x00000A41
		private static void ThrowOperationCanceledException(Exception innerException, CancellationToken cancellationToken)
		{
			throw CancellationHelper.CreateOperationCanceledException(innerException, cancellationToken);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000284A File Offset: 0x00000A4A
		internal static void ThrowIfCancellationRequested(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				CancellationHelper.ThrowOperationCanceledException(null, cancellationToken);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000285C File Offset: 0x00000A5C
		// Note: this type is marked as 'beforefieldinit'.
		static CancellationHelper()
		{
		}

		// Token: 0x0400001B RID: 27
		private static readonly string s_cancellationMessage = new OperationCanceledException().Message;
	}
}
