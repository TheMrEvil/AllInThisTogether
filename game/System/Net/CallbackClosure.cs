using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200055E RID: 1374
	internal class CallbackClosure
	{
		// Token: 0x06002C95 RID: 11413 RVA: 0x000980AE File Offset: 0x000962AE
		internal CallbackClosure(ExecutionContext context, AsyncCallback callback)
		{
			if (callback != null)
			{
				this._savedCallback = callback;
				this._savedContext = context;
			}
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x000980C7 File Offset: 0x000962C7
		internal bool IsCompatible(AsyncCallback callback)
		{
			return callback != null && this._savedCallback != null && object.Equals(this._savedCallback, callback);
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000980E7 File Offset: 0x000962E7
		internal AsyncCallback AsyncCallback
		{
			get
			{
				return this._savedCallback;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000980EF File Offset: 0x000962EF
		internal ExecutionContext Context
		{
			get
			{
				return this._savedContext;
			}
		}

		// Token: 0x040017F7 RID: 6135
		private AsyncCallback _savedCallback;

		// Token: 0x040017F8 RID: 6136
		private ExecutionContext _savedContext;
	}
}
