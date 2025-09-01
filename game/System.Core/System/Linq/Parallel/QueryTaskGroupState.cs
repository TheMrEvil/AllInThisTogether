using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001DB RID: 475
	internal class QueryTaskGroupState
	{
		// Token: 0x06000BD1 RID: 3025 RVA: 0x00029A0A File Offset: 0x00027C0A
		internal QueryTaskGroupState(CancellationState cancellationState, int queryId)
		{
			this._cancellationState = cancellationState;
			this._queryId = queryId;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00029A20 File Offset: 0x00027C20
		internal bool IsAlreadyEnded
		{
			get
			{
				return this._alreadyEnded == 1;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00029A2B File Offset: 0x00027C2B
		internal CancellationState CancellationState
		{
			get
			{
				return this._cancellationState;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00029A33 File Offset: 0x00027C33
		internal int QueryId
		{
			get
			{
				return this._queryId;
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00029A3B File Offset: 0x00027C3B
		internal void QueryBegin(Task rootTask)
		{
			this._rootTask = rootTask;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00029A44 File Offset: 0x00027C44
		internal void QueryEnd(bool userInitiatedDispose)
		{
			if (Interlocked.Exchange(ref this._alreadyEnded, 1) == 0)
			{
				try
				{
					this._rootTask.Wait();
				}
				catch (AggregateException ex)
				{
					AggregateException ex2 = ex.Flatten();
					bool flag = true;
					for (int i = 0; i < ex2.InnerExceptions.Count; i++)
					{
						OperationCanceledException ex3 = ex2.InnerExceptions[i] as OperationCanceledException;
						if (ex3 == null || !ex3.CancellationToken.IsCancellationRequested || ex3.CancellationToken != this._cancellationState.ExternalCancellationToken)
						{
							flag = false;
							break;
						}
					}
					if (!flag)
					{
						throw ex2;
					}
				}
				finally
				{
					IDisposable rootTask = this._rootTask;
					if (rootTask != null)
					{
						rootTask.Dispose();
					}
				}
				if (this._cancellationState.MergedCancellationToken.IsCancellationRequested)
				{
					if (!this._cancellationState.TopLevelDisposedFlag.Value)
					{
						CancellationState.ThrowWithStandardMessageIfCanceled(this._cancellationState.ExternalCancellationToken);
					}
					if (!userInitiatedDispose)
					{
						throw new ObjectDisposedException("enumerator", "The query enumerator has been disposed.");
					}
				}
			}
		}

		// Token: 0x0400085C RID: 2140
		private Task _rootTask;

		// Token: 0x0400085D RID: 2141
		private int _alreadyEnded;

		// Token: 0x0400085E RID: 2142
		private CancellationState _cancellationState;

		// Token: 0x0400085F RID: 2143
		private int _queryId;
	}
}
