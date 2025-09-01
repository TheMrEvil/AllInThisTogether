using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001D9 RID: 473
	internal abstract class QueryTask
	{
		// Token: 0x06000BC7 RID: 3015 RVA: 0x00029916 File Offset: 0x00027B16
		protected QueryTask(int taskIndex, QueryTaskGroupState groupState)
		{
			this._taskIndex = taskIndex;
			this._groupState = groupState;
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0002992C File Offset: 0x00027B2C
		private static void RunTaskSynchronously(object o)
		{
			((QueryTask)o).BaseWork(null);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0002993A File Offset: 0x00027B3A
		internal Task RunSynchronously(TaskScheduler taskScheduler)
		{
			Task task = new Task(QueryTask.s_runTaskSynchronouslyDelegate, this, TaskCreationOptions.AttachedToParent);
			task.RunSynchronously(taskScheduler);
			return task;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00029950 File Offset: 0x00027B50
		internal Task RunAsynchronously(TaskScheduler taskScheduler)
		{
			return Task.Factory.StartNew(QueryTask.s_baseWorkDelegate, this, default(CancellationToken), TaskCreationOptions.PreferFairness | TaskCreationOptions.AttachedToParent, taskScheduler);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00029978 File Offset: 0x00027B78
		private void BaseWork(object unused)
		{
			PlinqEtwProvider.Log.ParallelQueryFork(this._groupState.QueryId);
			try
			{
				this.Work();
			}
			finally
			{
				PlinqEtwProvider.Log.ParallelQueryJoin(this._groupState.QueryId);
			}
		}

		// Token: 0x06000BCC RID: 3020
		protected abstract void Work();

		// Token: 0x06000BCD RID: 3021 RVA: 0x000299C8 File Offset: 0x00027BC8
		// Note: this type is marked as 'beforefieldinit'.
		static QueryTask()
		{
		}

		// Token: 0x04000857 RID: 2135
		protected int _taskIndex;

		// Token: 0x04000858 RID: 2136
		protected QueryTaskGroupState _groupState;

		// Token: 0x04000859 RID: 2137
		private static Action<object> s_runTaskSynchronouslyDelegate = new Action<object>(QueryTask.RunTaskSynchronously);

		// Token: 0x0400085A RID: 2138
		private static Action<object> s_baseWorkDelegate = delegate(object o)
		{
			((QueryTask)o).BaseWork(null);
		};

		// Token: 0x020001DA RID: 474
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000BCE RID: 3022 RVA: 0x000299F0 File Offset: 0x00027BF0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000BCF RID: 3023 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x06000BD0 RID: 3024 RVA: 0x000299FC File Offset: 0x00027BFC
			internal void <.cctor>b__10_0(object o)
			{
				((QueryTask)o).BaseWork(null);
			}

			// Token: 0x0400085B RID: 2139
			public static readonly QueryTask.<>c <>9 = new QueryTask.<>c();
		}
	}
}
