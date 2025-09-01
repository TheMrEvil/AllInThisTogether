using System;
using System.Threading.Tasks;

namespace QFSW.QC.Actions
{
	// Token: 0x0200006D RID: 109
	public class Async : ICommandAction
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000A51A File Offset: 0x0000871A
		public bool IsFinished
		{
			get
			{
				return this._task.IsCompleted || this._task.IsCanceled || this._task.IsFaulted;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000A543 File Offset: 0x00008743
		public bool StartsIdle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000A546 File Offset: 0x00008746
		public Async(Task task)
		{
			this._task = task;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000A555 File Offset: 0x00008755
		public void Start(ActionContext context)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000A557 File Offset: 0x00008757
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
		}

		// Token: 0x0400014C RID: 332
		private readonly Task _task;
	}
}
