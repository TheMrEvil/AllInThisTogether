using System;

namespace System.Runtime
{
	// Token: 0x02000013 RID: 19
	internal class CompletedAsyncResult<T> : AsyncResult
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000032B8 File Offset: 0x000014B8
		public CompletedAsyncResult(T data, AsyncCallback callback, object state) : base(callback, state)
		{
			this.data = data;
			base.Complete(true);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000032D0 File Offset: 0x000014D0
		public static T End(IAsyncResult result)
		{
			Fx.AssertAndThrowFatal(result.IsCompleted, "CompletedAsyncResult<T> was not completed!");
			return AsyncResult.End<CompletedAsyncResult<T>>(result).data;
		}

		// Token: 0x0400006F RID: 111
		private T data;
	}
}
