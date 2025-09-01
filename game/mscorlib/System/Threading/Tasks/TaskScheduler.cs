using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	/// <summary>Represents an object that handles the low-level work of queuing tasks onto threads.</summary>
	// Token: 0x02000379 RID: 889
	[DebuggerDisplay("Id={Id}")]
	[DebuggerTypeProxy(typeof(TaskScheduler.SystemThreadingTasks_TaskSchedulerDebugView))]
	public abstract class TaskScheduler
	{
		/// <summary>Queues a <see cref="T:System.Threading.Tasks.Task" /> to the scheduler.</summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be queued.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		// Token: 0x06002510 RID: 9488
		protected internal abstract void QueueTask(Task task);

		/// <summary>Determines whether the provided <see cref="T:System.Threading.Tasks.Task" /> can be executed synchronously in this call, and if it can, executes it.</summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be executed.</param>
		/// <param name="taskWasPreviouslyQueued">A Boolean denoting whether or not task has previously been queued. If this parameter is True, then the task may have been previously queued (scheduled); if False, then the task is known not to have been queued, and this call is being made in order to execute the task inline without queuing it.</param>
		/// <returns>A Boolean value indicating whether the task was executed inline.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> was already executed.</exception>
		// Token: 0x06002511 RID: 9489
		protected abstract bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued);

		/// <summary>For debugger support only, generates an enumerable of <see cref="T:System.Threading.Tasks.Task" /> instances currently queued to the scheduler waiting to be executed.</summary>
		/// <returns>An enumerable that allows a debugger to traverse the tasks currently queued to this scheduler.</returns>
		/// <exception cref="T:System.NotSupportedException">This scheduler is unable to generate a list of queued tasks at this time.</exception>
		// Token: 0x06002512 RID: 9490
		protected abstract IEnumerable<Task> GetScheduledTasks();

		/// <summary>Indicates the maximum concurrency level this <see cref="T:System.Threading.Tasks.TaskScheduler" /> is able to support.</summary>
		/// <returns>Returns an integer that represents the maximum concurrency level. The default scheduler returns <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x0008406A File Offset: 0x0008226A
		public virtual int MaximumConcurrencyLevel
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x00084074 File Offset: 0x00082274
		internal bool TryRunInline(Task task, bool taskWasPreviouslyQueued)
		{
			TaskScheduler executingTaskScheduler = task.ExecutingTaskScheduler;
			if (executingTaskScheduler != this && executingTaskScheduler != null)
			{
				return executingTaskScheduler.TryRunInline(task, taskWasPreviouslyQueued);
			}
			StackGuard currentStackGuard;
			if (executingTaskScheduler == null || task.m_action == null || task.IsDelegateInvoked || task.IsCanceled || !(currentStackGuard = Task.CurrentStackGuard).TryBeginInliningScope())
			{
				return false;
			}
			bool flag = false;
			try
			{
				flag = this.TryExecuteTaskInline(task, taskWasPreviouslyQueued);
			}
			finally
			{
				currentStackGuard.EndInliningScope();
			}
			if (flag && !task.IsDelegateInvoked && !task.IsCanceled)
			{
				throw new InvalidOperationException("The TryExecuteTaskInline call to the underlying scheduler succeeded, but the task body was not invoked.");
			}
			return flag;
		}

		/// <summary>Attempts to dequeue a <see cref="T:System.Threading.Tasks.Task" /> that was previously queued to this scheduler.</summary>
		/// <param name="task">The <see cref="T:System.Threading.Tasks.Task" /> to be dequeued.</param>
		/// <returns>A Boolean denoting whether the <paramref name="task" /> argument was successfully dequeued.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="task" /> argument is null.</exception>
		// Token: 0x06002515 RID: 9493 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected internal virtual bool TryDequeue(Task task)
		{
			return false;
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal virtual void NotifyWorkItemProgress()
		{
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000040F7 File Offset: 0x000022F7
		internal virtual bool RequiresAtomicStartTransition
		{
			get
			{
				return true;
			}
		}

		/// <summary>Initializes the <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		// Token: 0x06002518 RID: 9496 RVA: 0x0000259F File Offset: 0x0000079F
		protected TaskScheduler()
		{
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x00084108 File Offset: 0x00082308
		private void AddToActiveTaskSchedulers()
		{
			ConditionalWeakTable<TaskScheduler, object> conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			if (conditionalWeakTable == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<TaskScheduler, object>>(ref TaskScheduler.s_activeTaskSchedulers, new ConditionalWeakTable<TaskScheduler, object>(), null);
				conditionalWeakTable = TaskScheduler.s_activeTaskSchedulers;
			}
			conditionalWeakTable.Add(this, null);
		}

		/// <summary>Gets the default <see cref="T:System.Threading.Tasks.TaskScheduler" /> instance that is provided by the .NET Framework.</summary>
		/// <returns>Returns the default <see cref="T:System.Threading.Tasks.TaskScheduler" /> instance.</returns>
		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600251A RID: 9498 RVA: 0x0008413D File Offset: 0x0008233D
		public static TaskScheduler Default
		{
			get
			{
				return TaskScheduler.s_defaultTaskScheduler;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the currently executing task.</summary>
		/// <returns>Returns the <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the currently executing task.</returns>
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600251B RID: 9499 RVA: 0x00084144 File Offset: 0x00082344
		public static TaskScheduler Current
		{
			get
			{
				return TaskScheduler.InternalCurrent ?? TaskScheduler.Default;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600251C RID: 9500 RVA: 0x00084154 File Offset: 0x00082354
		internal static TaskScheduler InternalCurrent
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent == null || (internalCurrent.CreationOptions & TaskCreationOptions.HideScheduler) != TaskCreationOptions.None)
				{
					return null;
				}
				return internalCurrent.ExecutingTaskScheduler;
			}
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the current <see cref="T:System.Threading.SynchronizationContext" />.</summary>
		/// <returns>A <see cref="T:System.Threading.Tasks.TaskScheduler" /> associated with the current <see cref="T:System.Threading.SynchronizationContext" />, as determined by <see cref="P:System.Threading.SynchronizationContext.Current" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current SynchronizationContext may not be used as a TaskScheduler.</exception>
		// Token: 0x0600251D RID: 9501 RVA: 0x0008417D File Offset: 0x0008237D
		public static TaskScheduler FromCurrentSynchronizationContext()
		{
			return new SynchronizationContextTaskScheduler();
		}

		/// <summary>Gets the unique ID for this <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <returns>Returns the unique ID for this <see cref="T:System.Threading.Tasks.TaskScheduler" />.</returns>
		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600251E RID: 9502 RVA: 0x00084184 File Offset: 0x00082384
		public int Id
		{
			get
			{
				if (this.m_taskSchedulerId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref TaskScheduler.s_taskSchedulerIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskSchedulerId, num, 0);
				}
				return this.m_taskSchedulerId;
			}
		}

		/// <summary>Attempts to execute the provided <see cref="T:System.Threading.Tasks.Task" /> on this scheduler.</summary>
		/// <param name="task">A <see cref="T:System.Threading.Tasks.Task" /> object to be executed.</param>
		/// <returns>A Boolean that is true if <paramref name="task" /> was successfully executed, false if it was not. A common reason for execution failure is that the task had previously been executed or is in the process of being executed by another thread.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="task" /> is not associated with this scheduler.</exception>
		// Token: 0x0600251F RID: 9503 RVA: 0x000841C1 File Offset: 0x000823C1
		protected bool TryExecuteTask(Task task)
		{
			if (task.ExecutingTaskScheduler != this)
			{
				throw new InvalidOperationException("ExecuteTask may not be called for a task which was previously queued to a different TaskScheduler.");
			}
			return task.ExecuteEntry(true);
		}

		/// <summary>Occurs when a faulted task's unobserved exception is about to trigger exception escalation policy, which, by default, would terminate the process.</summary>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06002520 RID: 9504 RVA: 0x000841E0 File Offset: 0x000823E0
		// (remove) Token: 0x06002521 RID: 9505 RVA: 0x00084230 File Offset: 0x00082430
		public static event EventHandler<UnobservedTaskExceptionEventArgs> UnobservedTaskException
		{
			add
			{
				if (value != null)
				{
					using (LockHolder.Hold(TaskScheduler._unobservedTaskExceptionLockObject))
					{
						TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Combine(TaskScheduler._unobservedTaskException, value);
					}
				}
			}
			remove
			{
				using (LockHolder.Hold(TaskScheduler._unobservedTaskExceptionLockObject))
				{
					TaskScheduler._unobservedTaskException = (EventHandler<UnobservedTaskExceptionEventArgs>)Delegate.Remove(TaskScheduler._unobservedTaskException, value);
				}
			}
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x00084280 File Offset: 0x00082480
		internal static void PublishUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs ueea)
		{
			using (LockHolder.Hold(TaskScheduler._unobservedTaskExceptionLockObject))
			{
				EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskException = TaskScheduler._unobservedTaskException;
				if (unobservedTaskException != null)
				{
					unobservedTaskException(sender, ueea);
				}
			}
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000842CC File Offset: 0x000824CC
		internal Task[] GetScheduledTasksForDebugger()
		{
			IEnumerable<Task> scheduledTasks = this.GetScheduledTasks();
			if (scheduledTasks == null)
			{
				return null;
			}
			Task[] array = scheduledTasks as Task[];
			if (array == null)
			{
				array = new LowLevelList<Task>(scheduledTasks).ToArray();
			}
			Task[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int id = array2[i].Id;
			}
			return array;
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x00084318 File Offset: 0x00082518
		internal static TaskScheduler[] GetTaskSchedulersForDebugger()
		{
			if (TaskScheduler.s_activeTaskSchedulers == null)
			{
				return new TaskScheduler[]
				{
					TaskScheduler.s_defaultTaskScheduler
				};
			}
			LowLevelList<TaskScheduler> lowLevelList = new LowLevelList<TaskScheduler>();
			foreach (KeyValuePair<TaskScheduler, object> keyValuePair in ((IEnumerable<KeyValuePair<TaskScheduler, object>>)TaskScheduler.s_activeTaskSchedulers))
			{
				lowLevelList.Add(keyValuePair.Key);
			}
			if (!lowLevelList.Contains(TaskScheduler.s_defaultTaskScheduler))
			{
				lowLevelList.Add(TaskScheduler.s_defaultTaskScheduler);
			}
			TaskScheduler[] array = lowLevelList.ToArray();
			TaskScheduler[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				int id = array2[i].Id;
			}
			return array;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000843C8 File Offset: 0x000825C8
		// Note: this type is marked as 'beforefieldinit'.
		static TaskScheduler()
		{
		}

		// Token: 0x04001D4D RID: 7501
		private static ConditionalWeakTable<TaskScheduler, object> s_activeTaskSchedulers;

		// Token: 0x04001D4E RID: 7502
		private static readonly TaskScheduler s_defaultTaskScheduler = new ThreadPoolTaskScheduler();

		// Token: 0x04001D4F RID: 7503
		internal static int s_taskSchedulerIdCounter;

		// Token: 0x04001D50 RID: 7504
		private volatile int m_taskSchedulerId;

		// Token: 0x04001D51 RID: 7505
		private static EventHandler<UnobservedTaskExceptionEventArgs> _unobservedTaskException;

		// Token: 0x04001D52 RID: 7506
		private static readonly Lock _unobservedTaskExceptionLockObject = new Lock();

		// Token: 0x0200037A RID: 890
		internal sealed class SystemThreadingTasks_TaskSchedulerDebugView
		{
			// Token: 0x06002526 RID: 9510 RVA: 0x000843DE File Offset: 0x000825DE
			public SystemThreadingTasks_TaskSchedulerDebugView(TaskScheduler scheduler)
			{
				this.m_taskScheduler = scheduler;
			}

			// Token: 0x1700047E RID: 1150
			// (get) Token: 0x06002527 RID: 9511 RVA: 0x000843ED File Offset: 0x000825ED
			public int Id
			{
				get
				{
					return this.m_taskScheduler.Id;
				}
			}

			// Token: 0x1700047F RID: 1151
			// (get) Token: 0x06002528 RID: 9512 RVA: 0x000843FA File Offset: 0x000825FA
			public IEnumerable<Task> ScheduledTasks
			{
				get
				{
					return this.m_taskScheduler.GetScheduledTasks();
				}
			}

			// Token: 0x04001D53 RID: 7507
			private readonly TaskScheduler m_taskScheduler;
		}
	}
}
