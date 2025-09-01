using System;

namespace System.Runtime
{
	// Token: 0x02000012 RID: 18
	internal class CompletedAsyncResult : AsyncResult
	{
		// Token: 0x06000074 RID: 116 RVA: 0x0000328E File Offset: 0x0000148E
		public CompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
			base.Complete(true);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000329F File Offset: 0x0000149F
		public static void End(IAsyncResult result)
		{
			Fx.AssertAndThrowFatal(result.IsCompleted, "CompletedAsyncResult was not completed!");
			AsyncResult.End<CompletedAsyncResult>(result);
		}
	}
}
