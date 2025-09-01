using System;
using System.Threading.Tasks;

namespace QFSW.QC.Actions
{
	// Token: 0x0200006E RID: 110
	public class Async<T> : ICommandAction
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A58A File Offset: 0x0000878A
		public bool IsFinished
		{
			get
			{
				return this._task.IsCompleted || this._task.IsCanceled || this._task.IsFaulted;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000A5B3 File Offset: 0x000087B3
		public bool StartsIdle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000A5B6 File Offset: 0x000087B6
		public Async(Task<T> task, Action<T> onResult)
		{
			this._task = task;
			this._onResult = onResult;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000A5CC File Offset: 0x000087CC
		public void Start(ActionContext context)
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000A5D0 File Offset: 0x000087D0
		public void Finalize(ActionContext context)
		{
			if (this._task.IsFaulted)
			{
				throw this._task.Exception.InnerException;
			}
			if (this._task.IsCanceled)
			{
				throw new TaskCanceledException();
			}
			this._onResult(this._task.Result);
		}

		// Token: 0x0400014D RID: 333
		private readonly Task<T> _task;

		// Token: 0x0400014E RID: 334
		private readonly Action<T> _onResult;
	}
}
