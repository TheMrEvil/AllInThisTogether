using System;
using System.Threading;

namespace System.Linq.Parallel
{
	// Token: 0x020001D3 RID: 467
	internal class CancellationState
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x000293C5 File Offset: 0x000275C5
		internal CancellationToken MergedCancellationToken
		{
			get
			{
				if (this.MergedCancellationTokenSource != null)
				{
					return this.MergedCancellationTokenSource.Token;
				}
				return new CancellationToken(false);
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000293E1 File Offset: 0x000275E1
		internal CancellationState(CancellationToken externalCancellationToken)
		{
			this.ExternalCancellationToken = externalCancellationToken;
			this.TopLevelDisposedFlag = new Shared<bool>(false);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000293FC File Offset: 0x000275FC
		internal static void ThrowIfCanceled(CancellationToken token)
		{
			if (token.IsCancellationRequested)
			{
				throw new OperationCanceledException(token);
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002940E File Offset: 0x0002760E
		internal static void ThrowWithStandardMessageIfCanceled(CancellationToken externalCancellationToken)
		{
			if (externalCancellationToken.IsCancellationRequested)
			{
				throw new OperationCanceledException("The query has been canceled via the token supplied to WithCancellation.", externalCancellationToken);
			}
		}

		// Token: 0x04000837 RID: 2103
		internal CancellationTokenSource InternalCancellationTokenSource;

		// Token: 0x04000838 RID: 2104
		internal CancellationToken ExternalCancellationToken;

		// Token: 0x04000839 RID: 2105
		internal CancellationTokenSource MergedCancellationTokenSource;

		// Token: 0x0400083A RID: 2106
		internal Shared<bool> TopLevelDisposedFlag;

		// Token: 0x0400083B RID: 2107
		internal const int POLL_INTERVAL = 63;
	}
}
