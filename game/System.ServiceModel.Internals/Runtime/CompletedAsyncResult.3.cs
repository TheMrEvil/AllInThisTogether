using System;

namespace System.Runtime
{
	// Token: 0x02000014 RID: 20
	internal class CompletedAsyncResult<TResult, TParameter> : AsyncResult
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000032ED File Offset: 0x000014ED
		public CompletedAsyncResult(TResult resultData, TParameter parameter, AsyncCallback callback, object state) : base(callback, state)
		{
			this.resultData = resultData;
			this.parameter = parameter;
			base.Complete(true);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003310 File Offset: 0x00001510
		public static TResult End(IAsyncResult result, out TParameter parameter)
		{
			Fx.AssertAndThrowFatal(result.IsCompleted, "CompletedAsyncResult<T> was not completed!");
			CompletedAsyncResult<TResult, TParameter> completedAsyncResult = AsyncResult.End<CompletedAsyncResult<TResult, TParameter>>(result);
			parameter = completedAsyncResult.parameter;
			return completedAsyncResult.resultData;
		}

		// Token: 0x04000070 RID: 112
		private TResult resultData;

		// Token: 0x04000071 RID: 113
		private TParameter parameter;
	}
}
