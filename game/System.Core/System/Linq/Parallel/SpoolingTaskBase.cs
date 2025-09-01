using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001E4 RID: 484
	internal abstract class SpoolingTaskBase : QueryTask
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x00029FAD File Offset: 0x000281AD
		protected SpoolingTaskBase(int taskIndex, QueryTaskGroupState groupState) : base(taskIndex, groupState)
		{
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00029FB8 File Offset: 0x000281B8
		protected override void Work()
		{
			try
			{
				this.SpoolingWork();
			}
			catch (Exception ex)
			{
				OperationCanceledException ex2 = ex as OperationCanceledException;
				if (ex2 == null || !(ex2.CancellationToken == this._groupState.CancellationState.MergedCancellationToken) || !this._groupState.CancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					this._groupState.CancellationState.InternalCancellationTokenSource.Cancel();
					throw;
				}
			}
			finally
			{
				this.SpoolingFinally();
			}
		}

		// Token: 0x06000BEE RID: 3054
		protected abstract void SpoolingWork();

		// Token: 0x06000BEF RID: 3055 RVA: 0x00003A59 File Offset: 0x00001C59
		protected virtual void SpoolingFinally()
		{
		}
	}
}
