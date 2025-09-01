using System;

namespace System.Runtime
{
	// Token: 0x02000037 RID: 55
	internal abstract class TypedAsyncResult<T> : AsyncResult
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x000075E4 File Offset: 0x000057E4
		public TypedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x000075EE File Offset: 0x000057EE
		public T Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000075F6 File Offset: 0x000057F6
		protected void Complete(T data, bool completedSynchronously)
		{
			this.data = data;
			base.Complete(completedSynchronously);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007606 File Offset: 0x00005806
		public static T End(IAsyncResult result)
		{
			return AsyncResult.End<TypedAsyncResult<T>>(result).Data;
		}

		// Token: 0x04000112 RID: 274
		private T data;
	}
}
