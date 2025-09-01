using System;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x0200080C RID: 2060
	internal sealed class MultiAsyncResult : LazyAsyncResult
	{
		// Token: 0x0600419B RID: 16795 RVA: 0x000E25C4 File Offset: 0x000E07C4
		internal MultiAsyncResult(object context, AsyncCallback callback, object state) : base(context, state, callback)
		{
			this._context = context;
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x000E25D6 File Offset: 0x000E07D6
		internal object Context
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000E25DE File Offset: 0x000E07DE
		internal void Enter()
		{
			this.Increment();
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000E25E6 File Offset: 0x000E07E6
		internal void Leave()
		{
			this.Decrement();
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000E25EE File Offset: 0x000E07EE
		internal void Leave(object result)
		{
			base.Result = result;
			this.Decrement();
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x000E25FD File Offset: 0x000E07FD
		private void Decrement()
		{
			if (Interlocked.Decrement(ref this._outstanding) == -1)
			{
				base.InvokeCallback(base.Result);
			}
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000E2619 File Offset: 0x000E0819
		private void Increment()
		{
			Interlocked.Increment(ref this._outstanding);
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x000E25E6 File Offset: 0x000E07E6
		internal void CompleteSequence()
		{
			this.Decrement();
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x000E2627 File Offset: 0x000E0827
		internal static object End(IAsyncResult result)
		{
			MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result;
			multiAsyncResult.InternalWaitForCompletion();
			return multiAsyncResult.Result;
		}

		// Token: 0x040027D2 RID: 10194
		private readonly object _context;

		// Token: 0x040027D3 RID: 10195
		private int _outstanding;
	}
}
