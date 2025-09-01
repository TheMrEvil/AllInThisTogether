using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000332 RID: 818
	internal class TaskReplicator
	{
		// Token: 0x06002269 RID: 8809 RVA: 0x0007C060 File Offset: 0x0007A260
		private TaskReplicator(ParallelOptions options, bool stopOnFirstFailure)
		{
			this._scheduler = (options.TaskScheduler ?? TaskScheduler.Current);
			this._stopOnFirstFailure = stopOnFirstFailure;
		}

		// Token: 0x0600226A RID: 8810 RVA: 0x0007C090 File Offset: 0x0007A290
		public static void Run<TState>(TaskReplicator.ReplicatableUserAction<TState> action, ParallelOptions options, bool stopOnFirstFailure)
		{
			int maxConcurrency = (options.EffectiveMaxConcurrencyLevel > 0) ? options.EffectiveMaxConcurrencyLevel : int.MaxValue;
			TaskReplicator taskReplicator = new TaskReplicator(options, stopOnFirstFailure);
			new TaskReplicator.Replica<TState>(taskReplicator, maxConcurrency, 1073741823, action).Start();
			TaskReplicator.Replica replica;
			while (taskReplicator._pendingReplicas.TryDequeue(out replica))
			{
				replica.Wait();
			}
			if (taskReplicator._exceptions != null)
			{
				throw new AggregateException(taskReplicator._exceptions);
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0007C0FC File Offset: 0x0007A2FC
		private static int GenerateCooperativeMultitaskingTaskTimeout()
		{
			int processorCount = PlatformHelper.ProcessorCount;
			int tickCount = Environment.TickCount;
			return 100 + tickCount % processorCount * 50;
		}

		// Token: 0x04001C50 RID: 7248
		private readonly TaskScheduler _scheduler;

		// Token: 0x04001C51 RID: 7249
		private readonly bool _stopOnFirstFailure;

		// Token: 0x04001C52 RID: 7250
		private readonly ConcurrentQueue<TaskReplicator.Replica> _pendingReplicas = new ConcurrentQueue<TaskReplicator.Replica>();

		// Token: 0x04001C53 RID: 7251
		private ConcurrentQueue<Exception> _exceptions;

		// Token: 0x04001C54 RID: 7252
		private bool _stopReplicating;

		// Token: 0x04001C55 RID: 7253
		private const int CooperativeMultitaskingTaskTimeout_Min = 100;

		// Token: 0x04001C56 RID: 7254
		private const int CooperativeMultitaskingTaskTimeout_Increment = 50;

		// Token: 0x04001C57 RID: 7255
		private const int CooperativeMultitaskingTaskTimeout_RootTask = 1073741823;

		// Token: 0x02000333 RID: 819
		// (Invoke) Token: 0x0600226D RID: 8813
		public delegate void ReplicatableUserAction<TState>(ref TState replicaState, int timeout, out bool yieldedBeforeCompletion);

		// Token: 0x02000334 RID: 820
		private abstract class Replica
		{
			// Token: 0x06002270 RID: 8816 RVA: 0x0007C120 File Offset: 0x0007A320
			protected Replica(TaskReplicator replicator, int maxConcurrency, int timeout)
			{
				this._replicator = replicator;
				this._timeout = timeout;
				this._remainingConcurrency = maxConcurrency - 1;
				this._pendingTask = new Task(delegate(object s)
				{
					((TaskReplicator.Replica)s).Execute();
				}, this);
				this._replicator._pendingReplicas.Enqueue(this);
			}

			// Token: 0x06002271 RID: 8817 RVA: 0x0007C188 File Offset: 0x0007A388
			public void Start()
			{
				this._pendingTask.RunSynchronously(this._replicator._scheduler);
			}

			// Token: 0x06002272 RID: 8818 RVA: 0x0007C1A4 File Offset: 0x0007A3A4
			public void Wait()
			{
				Task pendingTask;
				while ((pendingTask = this._pendingTask) != null)
				{
					pendingTask.Wait();
				}
			}

			// Token: 0x06002273 RID: 8819 RVA: 0x0007C1C8 File Offset: 0x0007A3C8
			public void Execute()
			{
				try
				{
					if (!this._replicator._stopReplicating && this._remainingConcurrency > 0)
					{
						this.CreateNewReplica();
						this._remainingConcurrency = 0;
					}
					bool flag;
					this.ExecuteAction(out flag);
					if (flag)
					{
						this._pendingTask = new Task(delegate(object s)
						{
							((TaskReplicator.Replica)s).Execute();
						}, this, CancellationToken.None, TaskCreationOptions.None);
						this._pendingTask.Start(this._replicator._scheduler);
					}
					else
					{
						this._replicator._stopReplicating = true;
						this._pendingTask = null;
					}
				}
				catch (Exception item)
				{
					LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref this._replicator._exceptions).Enqueue(item);
					if (this._replicator._stopOnFirstFailure)
					{
						this._replicator._stopReplicating = true;
					}
					this._pendingTask = null;
				}
			}

			// Token: 0x06002274 RID: 8820
			protected abstract void CreateNewReplica();

			// Token: 0x06002275 RID: 8821
			protected abstract void ExecuteAction(out bool yieldedBeforeCompletion);

			// Token: 0x04001C58 RID: 7256
			protected readonly TaskReplicator _replicator;

			// Token: 0x04001C59 RID: 7257
			protected readonly int _timeout;

			// Token: 0x04001C5A RID: 7258
			protected int _remainingConcurrency;

			// Token: 0x04001C5B RID: 7259
			protected volatile Task _pendingTask;

			// Token: 0x02000335 RID: 821
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06002276 RID: 8822 RVA: 0x0007C2B4 File Offset: 0x0007A4B4
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06002277 RID: 8823 RVA: 0x0000259F File Offset: 0x0000079F
				public <>c()
				{
				}

				// Token: 0x06002278 RID: 8824 RVA: 0x0007C2C0 File Offset: 0x0007A4C0
				internal void <.ctor>b__4_0(object s)
				{
					((TaskReplicator.Replica)s).Execute();
				}

				// Token: 0x06002279 RID: 8825 RVA: 0x0007C2C0 File Offset: 0x0007A4C0
				internal void <Execute>b__7_0(object s)
				{
					((TaskReplicator.Replica)s).Execute();
				}

				// Token: 0x04001C5C RID: 7260
				public static readonly TaskReplicator.Replica.<>c <>9 = new TaskReplicator.Replica.<>c();

				// Token: 0x04001C5D RID: 7261
				public static Action<object> <>9__4_0;

				// Token: 0x04001C5E RID: 7262
				public static Action<object> <>9__7_0;
			}
		}

		// Token: 0x02000336 RID: 822
		private sealed class Replica<TState> : TaskReplicator.Replica
		{
			// Token: 0x0600227A RID: 8826 RVA: 0x0007C2CD File Offset: 0x0007A4CD
			public Replica(TaskReplicator replicator, int maxConcurrency, int timeout, TaskReplicator.ReplicatableUserAction<TState> action) : base(replicator, maxConcurrency, timeout)
			{
				this._action = action;
			}

			// Token: 0x0600227B RID: 8827 RVA: 0x0007C2E0 File Offset: 0x0007A4E0
			protected override void CreateNewReplica()
			{
				new TaskReplicator.Replica<TState>(this._replicator, this._remainingConcurrency, TaskReplicator.GenerateCooperativeMultitaskingTaskTimeout(), this._action)._pendingTask.Start(this._replicator._scheduler);
			}

			// Token: 0x0600227C RID: 8828 RVA: 0x0007C315 File Offset: 0x0007A515
			protected override void ExecuteAction(out bool yieldedBeforeCompletion)
			{
				this._action(ref this._state, this._timeout, out yieldedBeforeCompletion);
			}

			// Token: 0x04001C5F RID: 7263
			private readonly TaskReplicator.ReplicatableUserAction<TState> _action;

			// Token: 0x04001C60 RID: 7264
			private TState _state;
		}
	}
}
