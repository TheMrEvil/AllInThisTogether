using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;
using Internal.Threading.Tasks.Tracing;

namespace System.Threading.Tasks
{
	// Token: 0x0200037E RID: 894
	internal sealed class ThreadPoolTaskScheduler : TaskScheduler
	{
		// Token: 0x06002536 RID: 9526 RVA: 0x000844BB File Offset: 0x000826BB
		internal ThreadPoolTaskScheduler()
		{
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000844C4 File Offset: 0x000826C4
		protected internal override void QueueTask(Task task)
		{
			if (TaskTrace.Enabled)
			{
				Task internalCurrent = Task.InternalCurrent;
				Task parent = task.m_parent;
				TaskTrace.TaskScheduled(base.Id, (internalCurrent == null) ? 0 : internalCurrent.Id, task.Id, (parent == null) ? 0 : parent.Id, (int)task.Options);
			}
			if ((task.Options & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
			{
				RuntimeThread runtimeThread = RuntimeThread.Create(ThreadPoolTaskScheduler.s_longRunningThreadWork, 0);
				runtimeThread.IsBackground = true;
				runtimeThread.Start(task);
				return;
			}
			bool forceGlobal = (task.Options & TaskCreationOptions.PreferFairness) > TaskCreationOptions.None;
			ThreadPool.UnsafeQueueCustomWorkItem(task, forceGlobal);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0008454C File Offset: 0x0008274C
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			if (taskWasPreviouslyQueued && !ThreadPool.TryPopCustomWorkItem(task))
			{
				return false;
			}
			bool result = false;
			try
			{
				result = task.ExecuteEntry(false);
			}
			finally
			{
				if (taskWasPreviouslyQueued)
				{
					this.NotifyWorkItemProgress();
				}
			}
			return result;
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00084590 File Offset: 0x00082790
		protected internal override bool TryDequeue(Task task)
		{
			return ThreadPool.TryPopCustomWorkItem(task);
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00084598 File Offset: 0x00082798
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return this.FilterTasksFromWorkItems(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x000845A5 File Offset: 0x000827A5
		private IEnumerable<Task> FilterTasksFromWorkItems(IEnumerable<IThreadPoolWorkItem> tpwItems)
		{
			foreach (IThreadPoolWorkItem threadPoolWorkItem in tpwItems)
			{
				if (threadPoolWorkItem is Task)
				{
					yield return (Task)threadPoolWorkItem;
				}
			}
			IEnumerator<IThreadPoolWorkItem> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x000845B5 File Offset: 0x000827B5
		internal override void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgress();
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600253D RID: 9533 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal override bool RequiresAtomicStartTransition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x000845BC File Offset: 0x000827BC
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadPoolTaskScheduler()
		{
		}

		// Token: 0x04001D59 RID: 7513
		private static readonly ParameterizedThreadStart s_longRunningThreadWork = delegate(object s)
		{
			((Task)s).ExecuteEntry(false);
		};

		// Token: 0x0200037F RID: 895
		[CompilerGenerated]
		private sealed class <FilterTasksFromWorkItems>d__6 : IEnumerable<Task>, IEnumerable, IEnumerator<Task>, IDisposable, IEnumerator
		{
			// Token: 0x0600253F RID: 9535 RVA: 0x000845D3 File Offset: 0x000827D3
			[DebuggerHidden]
			public <FilterTasksFromWorkItems>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06002540 RID: 9536 RVA: 0x000845F0 File Offset: 0x000827F0
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06002541 RID: 9537 RVA: 0x00084628 File Offset: 0x00082828
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = tpwItems.GetEnumerator();
						this.<>1__state = -3;
					}
					while (enumerator.MoveNext())
					{
						IThreadPoolWorkItem threadPoolWorkItem = enumerator.Current;
						if (threadPoolWorkItem is Task)
						{
							this.<>2__current = (Task)threadPoolWorkItem;
							this.<>1__state = 1;
							return true;
						}
					}
					this.<>m__Finally1();
					enumerator = null;
					result = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x06002542 RID: 9538 RVA: 0x000846D4 File Offset: 0x000828D4
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17000484 RID: 1156
			// (get) Token: 0x06002543 RID: 9539 RVA: 0x000846F0 File Offset: 0x000828F0
			Task IEnumerator<Task>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002544 RID: 9540 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000485 RID: 1157
			// (get) Token: 0x06002545 RID: 9541 RVA: 0x000846F0 File Offset: 0x000828F0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002546 RID: 9542 RVA: 0x000846F8 File Offset: 0x000828F8
			[DebuggerHidden]
			IEnumerator<Task> IEnumerable<Task>.GetEnumerator()
			{
				ThreadPoolTaskScheduler.<FilterTasksFromWorkItems>d__6 <FilterTasksFromWorkItems>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<FilterTasksFromWorkItems>d__ = this;
				}
				else
				{
					<FilterTasksFromWorkItems>d__ = new ThreadPoolTaskScheduler.<FilterTasksFromWorkItems>d__6(0);
				}
				<FilterTasksFromWorkItems>d__.tpwItems = tpwItems;
				return <FilterTasksFromWorkItems>d__;
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x0008473B File Offset: 0x0008293B
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Threading.Tasks.Task>.GetEnumerator();
			}

			// Token: 0x04001D5A RID: 7514
			private int <>1__state;

			// Token: 0x04001D5B RID: 7515
			private Task <>2__current;

			// Token: 0x04001D5C RID: 7516
			private int <>l__initialThreadId;

			// Token: 0x04001D5D RID: 7517
			private IEnumerable<IThreadPoolWorkItem> tpwItems;

			// Token: 0x04001D5E RID: 7518
			public IEnumerable<IThreadPoolWorkItem> <>3__tpwItems;

			// Token: 0x04001D5F RID: 7519
			private IEnumerator<IThreadPoolWorkItem> <>7__wrap1;
		}

		// Token: 0x02000380 RID: 896
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002548 RID: 9544 RVA: 0x00084743 File Offset: 0x00082943
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002549 RID: 9545 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x0600254A RID: 9546 RVA: 0x0008474F File Offset: 0x0008294F
			internal void <.cctor>b__10_0(object s)
			{
				((Task)s).ExecuteEntry(false);
			}

			// Token: 0x04001D60 RID: 7520
			public static readonly ThreadPoolTaskScheduler.<>c <>9 = new ThreadPoolTaskScheduler.<>c();
		}
	}
}
