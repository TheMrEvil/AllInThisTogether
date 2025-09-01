using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using Internal.Runtime.Augments;
using Internal.Threading.Tasks.Tracing;

namespace System.Threading.Tasks
{
	/// <summary>Represents an asynchronous operation.</summary>
	// Token: 0x02000357 RID: 855
	[DebuggerTypeProxy(typeof(SystemThreadingTasks_TaskDebugView))]
	[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}")]
	public class Task : IThreadPoolWorkItem, IAsyncResult, IDisposable
	{
		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600237C RID: 9084 RVA: 0x0007F1B5 File Offset: 0x0007D3B5
		private Task ParentForDebugger
		{
			get
			{
				return this.m_parent;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x0007F1BD File Offset: 0x0007D3BD
		private int StateFlagsForDebugger
		{
			get
			{
				return this.m_stateFlags;
			}
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x0007F1C8 File Offset: 0x0007D3C8
		internal Task(bool canceled, TaskCreationOptions creationOptions, CancellationToken ct)
		{
			if (canceled)
			{
				this.m_stateFlags = (int)((TaskCreationOptions)5242880 | creationOptions);
				Task.ContingentProperties contingentProperties = this.m_contingentProperties = new Task.ContingentProperties();
				contingentProperties.m_cancellationToken = ct;
				contingentProperties.m_internalCancellationRequested = 1;
				return;
			}
			this.m_stateFlags = (int)((TaskCreationOptions)16777216 | creationOptions);
		}

		// Token: 0x0600237F RID: 9087 RVA: 0x0007F21E File Offset: 0x0007D41E
		internal Task()
		{
			this.m_stateFlags = 33555456;
		}

		// Token: 0x06002380 RID: 9088 RVA: 0x0007F234 File Offset: 0x0007D434
		internal Task(object state, TaskCreationOptions creationOptions, bool promiseStyle)
		{
			if ((creationOptions & ~(TaskCreationOptions.AttachedToParent | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
			{
				this.m_parent = Task.InternalCurrent;
			}
			this.TaskConstructorCore(null, state, default(CancellationToken), creationOptions, InternalTaskOptions.PromiseTask, null);
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is <see langword="null" />.</exception>
		// Token: 0x06002381 RID: 9089 RVA: 0x0007F280 File Offset: 0x0007D480
		public Task(Action action) : this(action, null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that the new  task will observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x06002382 RID: 9090 RVA: 0x0007F2A2 File Offset: 0x0007D4A2
		public Task(Action action, CancellationToken cancellationToken) : this(action, null, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and creation options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		// Token: 0x06002383 RID: 9091 RVA: 0x0007F2B4 File Offset: 0x0007D4B4
		public Task(Action action, TaskCreationOptions creationOptions) : this(action, null, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and creation options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that the new task will observe.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		// Token: 0x06002384 RID: 9092 RVA: 0x0007F2DB File Offset: 0x0007D4DB
		public Task(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(action, null, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action and state.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x06002385 RID: 9093 RVA: 0x0007F2F0 File Offset: 0x0007D4F0
		public Task(Action<object> action, object state) : this(action, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action, state, and options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that the new task will observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x06002386 RID: 9094 RVA: 0x0007F312 File Offset: 0x0007D512
		public Task(Action<object> action, object state, CancellationToken cancellationToken) : this(action, state, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action, state, and options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		// Token: 0x06002387 RID: 9095 RVA: 0x0007F324 File Offset: 0x0007D524
		public Task(Action<object> action, object state, TaskCreationOptions creationOptions) : this(action, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Threading.Tasks.Task" /> with the specified action, state, and options.</summary>
		/// <param name="action">The delegate that represents the code to execute in the task.</param>
		/// <param name="state">An object representing data to be used by the action.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that the new task will observe.</param>
		/// <param name="creationOptions">The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to customize the task's behavior.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskCreationOptions" />.</exception>
		// Token: 0x06002388 RID: 9096 RVA: 0x0007F34B File Offset: 0x0007D54B
		public Task(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(action, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x0007F361 File Offset: 0x0007D561
		internal Task(Delegate action, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if ((creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None)
			{
				this.m_parent = parent;
			}
			this.TaskConstructorCore(action, state, cancellationToken, creationOptions, internalOptions, scheduler);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x0007F394 File Offset: 0x0007D594
		internal void TaskConstructorCore(Delegate action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			this.m_action = action;
			this.m_stateObject = state;
			this.m_taskScheduler = scheduler;
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
			int num = (int)(creationOptions | (TaskCreationOptions)internalOptions);
			if (this.m_action == null || (internalOptions & InternalTaskOptions.ContinuationTask) != InternalTaskOptions.None)
			{
				num |= 33554432;
			}
			this.m_stateFlags = num;
			if (this.m_parent != null && (creationOptions & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
			{
				this.m_parent.AddNewChild();
			}
			if (cancellationToken.CanBeCanceled)
			{
				this.AssignCancellationToken(cancellationToken, null, null);
			}
			this.CapturedContext = ExecutionContext.Capture();
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x0007F438 File Offset: 0x0007D638
		private void AssignCancellationToken(CancellationToken cancellationToken, Task antecedent, TaskContinuation continuation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(false);
			contingentProperties.m_cancellationToken = cancellationToken;
			try
			{
				if ((this.Options & (TaskCreationOptions)13312) == TaskCreationOptions.None)
				{
					if (cancellationToken.IsCancellationRequested)
					{
						this.InternalCancel(false);
					}
					else
					{
						CancellationTokenRegistration cancellationTokenRegistration;
						if (antecedent == null)
						{
							cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, this);
						}
						else
						{
							cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(Task.s_taskCancelCallback, new Tuple<Task, Task, TaskContinuation>(this, antecedent, continuation));
						}
						contingentProperties.m_cancellationRegistration = cancellationTokenRegistration;
					}
				}
			}
			catch
			{
				if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.Options & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None)
				{
					this.m_parent.DisregardChild();
				}
				throw;
			}
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x0007F4EC File Offset: 0x0007D6EC
		private static void TaskCancelCallback(object o)
		{
			Task task = o as Task;
			if (task == null)
			{
				Tuple<Task, Task, TaskContinuation> tuple = o as Tuple<Task, Task, TaskContinuation>;
				if (tuple != null)
				{
					task = tuple.Item1;
					Task item = tuple.Item2;
					TaskContinuation item2 = tuple.Item3;
					item.RemoveContinuation(item2);
				}
			}
			task.InternalCancel(false);
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x0007F531 File Offset: 0x0007D731
		internal bool TrySetCanceled(CancellationToken tokenToRecord)
		{
			return this.TrySetCanceled(tokenToRecord, null);
		}

		// Token: 0x0600238E RID: 9102 RVA: 0x0007F53C File Offset: 0x0007D73C
		internal bool TrySetCanceled(CancellationToken tokenToRecord, object cancellationException)
		{
			bool result = false;
			if (this.AtomicStateUpdate(67108864, 90177536))
			{
				this.RecordInternalCancellationRequest(tokenToRecord, cancellationException);
				this.CancellationCleanupLogic();
				result = true;
			}
			return result;
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x0007F570 File Offset: 0x0007D770
		internal bool TrySetException(object exceptionObject)
		{
			bool result = false;
			this.EnsureContingentPropertiesInitialized(true);
			if (this.AtomicStateUpdate(67108864, 90177536))
			{
				this.AddException(exceptionObject);
				this.Finish(false);
				result = true;
			}
			return result;
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x0007F5AC File Offset: 0x0007D7AC
		private string DebuggerDisplayMethodDescription
		{
			get
			{
				Delegate action = this.m_action;
				if (action == null)
				{
					return "{null}";
				}
				return "0x" + action.GetNativeFunctionPointer().ToString("x");
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06002391 RID: 9105 RVA: 0x0007F5E6 File Offset: 0x0007D7E6
		internal TaskCreationOptions Options
		{
			get
			{
				return Task.OptionsMethod(this.m_stateFlags);
			}
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x0007F5F5 File Offset: 0x0007D7F5
		internal static TaskCreationOptions OptionsMethod(int flags)
		{
			return (TaskCreationOptions)(flags & 65535);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x0007F600 File Offset: 0x0007D800
		internal bool AtomicStateUpdate(int newBits, int illegalBits)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int stateFlags = this.m_stateFlags;
				if ((stateFlags & illegalBits) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_stateFlags, stateFlags | newBits, stateFlags) == stateFlags)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0007F644 File Offset: 0x0007D844
		internal bool AtomicStateUpdate(int newBits, int illegalBits, ref int oldFlags)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldFlags = this.m_stateFlags;
				if ((oldFlags & illegalBits) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_stateFlags, oldFlags | newBits, oldFlags) == oldFlags)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0007F68C File Offset: 0x0007D88C
		internal void SetNotificationForWaitCompletion(bool enabled)
		{
			if (enabled)
			{
				this.AtomicStateUpdate(268435456, 90177536);
				return;
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int stateFlags = this.m_stateFlags;
				int value = stateFlags & -268435457;
				if (Interlocked.CompareExchange(ref this.m_stateFlags, value, stateFlags) == stateFlags)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x0007F6E0 File Offset: 0x0007D8E0
		internal bool NotifyDebuggerOfWaitCompletionIfNecessary()
		{
			if (this.IsWaitNotificationEnabled && this.ShouldNotifyDebuggerOfWaitCompletion)
			{
				this.NotifyDebuggerOfWaitCompletion();
				return true;
			}
			return false;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x0007F6FC File Offset: 0x0007D8FC
		internal static bool AnyTaskRequiresNotifyDebuggerOfWaitCompletion(Task[] tasks)
		{
			foreach (Task task in tasks)
			{
				if (task != null && task.IsWaitNotificationEnabled && task.ShouldNotifyDebuggerOfWaitCompletion)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x0007F733 File Offset: 0x0007D933
		internal bool IsWaitNotificationEnabledOrNotRanToCompletion
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.m_stateFlags & 285212672) != 16777216;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x0007F74D File Offset: 0x0007D94D
		internal virtual bool ShouldNotifyDebuggerOfWaitCompletion
		{
			get
			{
				return this.IsWaitNotificationEnabled;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x0007F755 File Offset: 0x0007D955
		internal bool IsWaitNotificationEnabled
		{
			get
			{
				return (this.m_stateFlags & 268435456) != 0;
			}
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x0007F768 File Offset: 0x0007D968
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private void NotifyDebuggerOfWaitCompletion()
		{
			this.SetNotificationForWaitCompletion(false);
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0007F771 File Offset: 0x0007D971
		internal bool MarkStarted()
		{
			return this.AtomicStateUpdate(65536, 4259840);
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x0007F784 File Offset: 0x0007D984
		internal void AddNewChild()
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			if (contingentProperties.m_completionCountdown == 1)
			{
				contingentProperties.m_completionCountdown++;
				return;
			}
			Interlocked.Increment(ref contingentProperties.m_completionCountdown);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0007F7C3 File Offset: 0x0007D9C3
		internal void DisregardChild()
		{
			Interlocked.Decrement(ref this.EnsureContingentPropertiesInitialized(true).m_completionCountdown);
		}

		/// <summary>Starts the <see cref="T:System.Threading.Tasks.Task" />, scheduling it for execution to the current <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> instance has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.Tasks.Task" /> is not in a valid state to be started. It may have already been started, executed, or canceled, or it may have been created in a manner that doesn't support direct scheduling.</exception>
		// Token: 0x0600239F RID: 9119 RVA: 0x0007F7D7 File Offset: 0x0007D9D7
		public void Start()
		{
			this.Start(TaskScheduler.Current);
		}

		/// <summary>Starts the <see cref="T:System.Threading.Tasks.Task" />, scheduling it for execution to the specified <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> with which to associate and execute this task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.Tasks.Task" /> is not in a valid state to be started. It may have already been started, executed, or canceled, or it may have been created in a manner that doesn't support direct scheduling.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> instance has been disposed.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskSchedulerException">The scheduler was unable to queue this task.</exception>
		// Token: 0x060023A0 RID: 9120 RVA: 0x0007F7E4 File Offset: 0x0007D9E4
		public void Start(TaskScheduler scheduler)
		{
			int stateFlags = this.m_stateFlags;
			if (Task.IsCompletedMethod(stateFlags))
			{
				throw new InvalidOperationException("Start may not be called on a task that has completed.");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
			if ((taskCreationOptions & (TaskCreationOptions)1024) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException("Start may not be called on a promise-style task.");
			}
			if ((taskCreationOptions & (TaskCreationOptions)512) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException("Start may not be called on a continuation task.");
			}
			if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, null) != null)
			{
				throw new InvalidOperationException("Start may not be called on a task that was already started.");
			}
			this.ScheduleAndStart(true);
		}

		/// <summary>Runs the <see cref="T:System.Threading.Tasks.Task" /> synchronously on the current <see cref="T:System.Threading.Tasks.TaskScheduler" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> instance has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.Tasks.Task" /> is not in a valid state to be started. It may have already been started, executed, or canceled, or it may have been created in a manner that doesn't support direct scheduling.</exception>
		// Token: 0x060023A1 RID: 9121 RVA: 0x0007F867 File Offset: 0x0007DA67
		public void RunSynchronously()
		{
			this.InternalRunSynchronously(TaskScheduler.Current, true);
		}

		/// <summary>Runs the <see cref="T:System.Threading.Tasks.Task" /> synchronously on the <see cref="T:System.Threading.Tasks.TaskScheduler" /> provided.</summary>
		/// <param name="scheduler">The scheduler on which to attempt to run this task inline.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> instance has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.Tasks.Task" /> is not in a valid state to be started. It may have already been started, executed, or canceled, or it may have been created in a manner that doesn't support direct scheduling.</exception>
		// Token: 0x060023A2 RID: 9122 RVA: 0x0007F875 File Offset: 0x0007DA75
		public void RunSynchronously(TaskScheduler scheduler)
		{
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			this.InternalRunSynchronously(scheduler, true);
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0007F890 File Offset: 0x0007DA90
		internal void InternalRunSynchronously(TaskScheduler scheduler, bool waitForCompletion)
		{
			int stateFlags = this.m_stateFlags;
			TaskCreationOptions taskCreationOptions = Task.OptionsMethod(stateFlags);
			if ((taskCreationOptions & (TaskCreationOptions)512) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException("RunSynchronously may not be called on a continuation task.");
			}
			if ((taskCreationOptions & (TaskCreationOptions)1024) != TaskCreationOptions.None)
			{
				throw new InvalidOperationException("RunSynchronously may not be called on a task not bound to a delegate, such as the task returned from an asynchronous method.");
			}
			if (Task.IsCompletedMethod(stateFlags))
			{
				throw new InvalidOperationException("RunSynchronously may not be called on a task that has already completed.");
			}
			if (Interlocked.CompareExchange<TaskScheduler>(ref this.m_taskScheduler, scheduler, null) != null)
			{
				throw new InvalidOperationException("RunSynchronously may not be called on a task that was already started.");
			}
			if (this.MarkStarted())
			{
				bool flag = false;
				try
				{
					if (!scheduler.TryRunInline(this, false))
					{
						scheduler.QueueTask(this);
						flag = true;
					}
					if (waitForCompletion && !this.IsCompleted)
					{
						this.SpinThenBlockingWait(-1, default(CancellationToken));
					}
					return;
				}
				catch (Exception innerException)
				{
					if (!flag)
					{
						TaskSchedulerException ex = new TaskSchedulerException(innerException);
						this.AddException(ex);
						this.Finish(false);
						this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
						throw ex;
					}
					throw;
				}
			}
			throw new InvalidOperationException("RunSynchronously may not be called on a task that has already completed.");
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x0007F988 File Offset: 0x0007DB88
		internal static Task InternalStartNew(Task creatingTask, Delegate action, object state, CancellationToken cancellationToken, TaskScheduler scheduler, TaskCreationOptions options, InternalTaskOptions internalOptions)
		{
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task task = new Task(action, state, creatingTask, cancellationToken, options, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler);
			task.ScheduleAndStart(false);
			return task;
		}

		/// <summary>Gets an ID for this <see cref="T:System.Threading.Tasks.Task" /> instance.</summary>
		/// <returns>The identifier that is assigned by the system to this <see cref="T:System.Threading.Tasks.Task" /> instance.</returns>
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x0007F9B8 File Offset: 0x0007DBB8
		public int Id
		{
			get
			{
				if (this.m_taskId == 0)
				{
					int num;
					do
					{
						num = Interlocked.Increment(ref Task.s_taskIdCounter);
					}
					while (num == 0);
					Interlocked.CompareExchange(ref this.m_taskId, num, 0);
				}
				return this.m_taskId;
			}
		}

		/// <summary>Returns the ID of the currently executing <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>An integer that was assigned by the system to the currently-executing task.</returns>
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0007F9F8 File Offset: 0x0007DBF8
		public static int? CurrentId
		{
			get
			{
				Task internalCurrent = Task.InternalCurrent;
				if (internalCurrent != null)
				{
					return new int?(internalCurrent.Id);
				}
				return null;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x0007FA23 File Offset: 0x0007DC23
		internal static Task InternalCurrent
		{
			get
			{
				return Task.t_currentTask;
			}
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x0007FA2A File Offset: 0x0007DC2A
		internal static Task InternalCurrentIfAttached(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & TaskCreationOptions.AttachedToParent) == TaskCreationOptions.None)
			{
				return null;
			}
			return Task.InternalCurrent;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x0007FA38 File Offset: 0x0007DC38
		internal static StackGuard CurrentStackGuard
		{
			get
			{
				StackGuard stackGuard = Task.t_stackGuard;
				if (stackGuard == null)
				{
					stackGuard = (Task.t_stackGuard = new StackGuard());
				}
				return stackGuard;
			}
		}

		/// <summary>Gets the <see cref="T:System.AggregateException" /> that caused the <see cref="T:System.Threading.Tasks.Task" /> to end prematurely. If the <see cref="T:System.Threading.Tasks.Task" /> completed successfully or has not yet thrown any exceptions, this will return <see langword="null" />.</summary>
		/// <returns>The <see cref="T:System.AggregateException" /> that caused the <see cref="T:System.Threading.Tasks.Task" /> to end prematurely.</returns>
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x0007FA5C File Offset: 0x0007DC5C
		public AggregateException Exception
		{
			get
			{
				AggregateException result = null;
				if (this.IsFaulted)
				{
					result = this.GetExceptions(false);
				}
				return result;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskStatus" /> of this task.</summary>
		/// <returns>The current <see cref="T:System.Threading.Tasks.TaskStatus" /> of this task instance.</returns>
		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x0007FA7C File Offset: 0x0007DC7C
		public TaskStatus Status
		{
			get
			{
				int stateFlags = this.m_stateFlags;
				TaskStatus result;
				if ((stateFlags & 2097152) != 0)
				{
					result = TaskStatus.Faulted;
				}
				else if ((stateFlags & 4194304) != 0)
				{
					result = TaskStatus.Canceled;
				}
				else if ((stateFlags & 16777216) != 0)
				{
					result = TaskStatus.RanToCompletion;
				}
				else if ((stateFlags & 8388608) != 0)
				{
					result = TaskStatus.WaitingForChildrenToComplete;
				}
				else if ((stateFlags & 131072) != 0)
				{
					result = TaskStatus.Running;
				}
				else if ((stateFlags & 65536) != 0)
				{
					result = TaskStatus.WaitingToRun;
				}
				else if ((stateFlags & 33554432) != 0)
				{
					result = TaskStatus.WaitingForActivation;
				}
				else
				{
					result = TaskStatus.Created;
				}
				return result;
			}
		}

		/// <summary>Gets whether this <see cref="T:System.Threading.Tasks.Task" /> instance has completed execution due to being canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the task has completed due to being canceled; otherwise <see langword="false" />.</returns>
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060023AC RID: 9132 RVA: 0x0007FAF0 File Offset: 0x0007DCF0
		public bool IsCanceled
		{
			get
			{
				return (this.m_stateFlags & 6291456) == 4194304;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x0007FB08 File Offset: 0x0007DD08
		internal bool IsCancellationRequested
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return contingentProperties != null && (contingentProperties.m_internalCancellationRequested == 1 || contingentProperties.m_cancellationToken.IsCancellationRequested);
			}
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x0007FB3C File Offset: 0x0007DD3C
		internal Task.ContingentProperties EnsureContingentPropertiesInitialized(bool needsProtection)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null)
			{
				return this.EnsureContingentPropertiesInitializedCore(needsProtection);
			}
			return contingentProperties;
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x0007FB60 File Offset: 0x0007DD60
		private Task.ContingentProperties EnsureContingentPropertiesInitializedCore(bool needsProtection)
		{
			if (needsProtection)
			{
				return LazyInitializer.EnsureInitialized<Task.ContingentProperties>(ref this.m_contingentProperties, Task.s_createContingentProperties);
			}
			return this.m_contingentProperties = new Task.ContingentProperties();
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x0007FB94 File Offset: 0x0007DD94
		internal CancellationToken CancellationToken
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					return contingentProperties.m_cancellationToken;
				}
				return default(CancellationToken);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x0007FBBD File Offset: 0x0007DDBD
		internal bool IsCancellationAcknowledged
		{
			get
			{
				return (this.m_stateFlags & 1048576) != 0;
			}
		}

		/// <summary>Gets whether this <see cref="T:System.Threading.Tasks.Task" /> has completed.</summary>
		/// <returns>
		///   <see langword="true" /> if the task has completed; otherwise <see langword="false" />.</returns>
		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x0007FBD0 File Offset: 0x0007DDD0
		public bool IsCompleted
		{
			get
			{
				return Task.IsCompletedMethod(this.m_stateFlags);
			}
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x0007FBDF File Offset: 0x0007DDDF
		private static bool IsCompletedMethod(int flags)
		{
			return (flags & 23068672) != 0;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x0007FBEB File Offset: 0x0007DDEB
		public bool IsCompletedSuccessfully
		{
			get
			{
				return (this.m_stateFlags & 23068672) == 16777216;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to create this task.</summary>
		/// <returns>The <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> used to create this task.</returns>
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x0007FC02 File Offset: 0x0007DE02
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.Options & (TaskCreationOptions)(-65281);
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that can be used to wait for the task to complete.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that can be used to wait for the task to complete.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x0007FC10 File Offset: 0x0007DE10
		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get
			{
				if ((this.m_stateFlags & 262144) != 0)
				{
					throw new ObjectDisposedException(null, "The task has been disposed.");
				}
				return this.CompletedEvent.WaitHandle;
			}
		}

		/// <summary>Gets the state object supplied when the <see cref="T:System.Threading.Tasks.Task" /> was created, or null if none was supplied.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the state data that was passed in to the task when it was created.</returns>
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060023B7 RID: 9143 RVA: 0x0007FC3C File Offset: 0x0007DE3C
		public object AsyncState
		{
			get
			{
				return this.m_stateObject;
			}
		}

		/// <summary>Gets an indication of whether the operation completed synchronously.</summary>
		/// <returns>
		///   <see langword="true" /> if the operation completed synchronously; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		bool IAsyncResult.CompletedSynchronously
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060023B9 RID: 9145 RVA: 0x0007FC44 File Offset: 0x0007DE44
		internal TaskScheduler ExecutingTaskScheduler
		{
			get
			{
				return this.m_taskScheduler;
			}
		}

		/// <summary>Provides access to factory methods for creating and configuring <see cref="T:System.Threading.Tasks.Task" /> and <see cref="T:System.Threading.Tasks.Task`1" /> instances.</summary>
		/// <returns>A factory object that can create a variety of <see cref="T:System.Threading.Tasks.Task" /> and <see cref="T:System.Threading.Tasks.Task`1" /> objects.</returns>
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x0007FC4C File Offset: 0x0007DE4C
		public static TaskFactory Factory
		{
			[CompilerGenerated]
			get
			{
				return Task.<Factory>k__BackingField;
			}
		} = new TaskFactory();

		/// <summary>Gets a task that has already completed successfully.</summary>
		/// <returns>The successfully completed task.</returns>
		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x060023BB RID: 9147 RVA: 0x0007FC53 File Offset: 0x0007DE53
		public static Task CompletedTask
		{
			[CompilerGenerated]
			get
			{
				return Task.<CompletedTask>k__BackingField;
			}
		} = new Task(false, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x0007FC5C File Offset: 0x0007DE5C
		internal ManualResetEventSlim CompletedEvent
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
				if (contingentProperties.m_completionEvent == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEventSlim manualResetEventSlim = new ManualResetEventSlim(isCompleted);
					if (Interlocked.CompareExchange<ManualResetEventSlim>(ref contingentProperties.m_completionEvent, manualResetEventSlim, null) != null)
					{
						manualResetEventSlim.Dispose();
					}
					else if (!isCompleted && this.IsCompleted)
					{
						manualResetEventSlim.Set();
					}
				}
				return contingentProperties.m_completionEvent;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x0007FCBC File Offset: 0x0007DEBC
		internal bool ExceptionRecorded
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				return contingentProperties != null && contingentProperties.m_exceptionsHolder != null && contingentProperties.m_exceptionsHolder.ContainsFaultList;
			}
		}

		/// <summary>Gets whether the <see cref="T:System.Threading.Tasks.Task" /> completed due to an unhandled exception.</summary>
		/// <returns>
		///   <see langword="true" /> if the task has thrown an unhandled exception; otherwise <see langword="false" />.</returns>
		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x060023BE RID: 9150 RVA: 0x0007FCEE File Offset: 0x0007DEEE
		public bool IsFaulted
		{
			get
			{
				return (this.m_stateFlags & 2097152) != 0;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x0007FD04 File Offset: 0x0007DF04
		// (set) Token: 0x060023C0 RID: 9152 RVA: 0x0007FD31 File Offset: 0x0007DF31
		internal ExecutionContext CapturedContext
		{
			get
			{
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null && contingentProperties.m_capturedContext != null)
				{
					return contingentProperties.m_capturedContext;
				}
				return ExecutionContext.Default;
			}
			set
			{
				if (value != ExecutionContext.Default)
				{
					this.EnsureContingentPropertiesInitialized(false).m_capturedContext = value;
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.Tasks.Task" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The task is not in one of the final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x060023C1 RID: 9153 RVA: 0x0007FD48 File Offset: 0x0007DF48
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Disposes the <see cref="T:System.Threading.Tasks.Task" />, releasing all of its unmanaged resources.</summary>
		/// <param name="disposing">A Boolean value that indicates whether this method is being called due to a call to <see cref="M:System.Threading.Tasks.Task.Dispose" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The task is not in one of the final states: <see cref="F:System.Threading.Tasks.TaskStatus.RanToCompletion" />, <see cref="F:System.Threading.Tasks.TaskStatus.Faulted" />, or <see cref="F:System.Threading.Tasks.TaskStatus.Canceled" />.</exception>
		// Token: 0x060023C2 RID: 9154 RVA: 0x0007FD58 File Offset: 0x0007DF58
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if ((this.Options & (TaskCreationOptions)16384) != TaskCreationOptions.None)
				{
					return;
				}
				if (!this.IsCompleted)
				{
					throw new InvalidOperationException("A task may only be disposed if it is in a completion state (RanToCompletion, Faulted or Canceled).");
				}
				Task.ContingentProperties contingentProperties = Volatile.Read<Task.ContingentProperties>(ref this.m_contingentProperties);
				if (contingentProperties != null)
				{
					ManualResetEventSlim completionEvent = contingentProperties.m_completionEvent;
					if (completionEvent != null)
					{
						contingentProperties.m_completionEvent = null;
						if (!completionEvent.IsSet)
						{
							completionEvent.Set();
						}
						completionEvent.Dispose();
					}
				}
			}
			this.m_stateFlags |= 262144;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x0007FDD8 File Offset: 0x0007DFD8
		internal void ScheduleAndStart(bool needsProtection)
		{
			if (needsProtection)
			{
				if (!this.MarkStarted())
				{
					return;
				}
			}
			else
			{
				this.m_stateFlags |= 65536;
			}
			DebuggerSupport.AddToActiveTasks(this);
			if (DebuggerSupport.LoggingOn && (this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
			{
				CausalityTraceLevel traceLevel = CausalityTraceLevel.Required;
				string str = "Task: ";
				Delegate action = this.m_action;
				DebuggerSupport.TraceOperationCreation(traceLevel, this, str + ((action != null) ? action.ToString() : null), 0UL);
			}
			try
			{
				this.m_taskScheduler.QueueTask(this);
			}
			catch (Exception innerException)
			{
				TaskSchedulerException ex = new TaskSchedulerException(innerException);
				this.AddException(ex);
				this.Finish(false);
				if ((this.Options & (TaskCreationOptions)512) == TaskCreationOptions.None)
				{
					this.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
				}
				throw ex;
			}
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x0007FEA0 File Offset: 0x0007E0A0
		internal void AddException(object exceptionObject)
		{
			this.AddException(exceptionObject, false);
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x0007FEAC File Offset: 0x0007E0AC
		internal void AddException(object exceptionObject, bool representsCancellation)
		{
			Task.ContingentProperties contingentProperties = this.EnsureContingentPropertiesInitialized(true);
			if (contingentProperties.m_exceptionsHolder == null)
			{
				TaskExceptionHolder taskExceptionHolder = new TaskExceptionHolder(this);
				if (Interlocked.CompareExchange<TaskExceptionHolder>(ref contingentProperties.m_exceptionsHolder, taskExceptionHolder, null) != null)
				{
					taskExceptionHolder.MarkAsHandled(false);
				}
			}
			Task.ContingentProperties obj = contingentProperties;
			lock (obj)
			{
				contingentProperties.m_exceptionsHolder.Add(exceptionObject, representsCancellation);
			}
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x0007FF20 File Offset: 0x0007E120
		private AggregateException GetExceptions(bool includeTaskCanceledExceptions)
		{
			Exception ex = null;
			if (includeTaskCanceledExceptions && this.IsCanceled)
			{
				ex = new TaskCanceledException(this);
			}
			if (this.ExceptionRecorded)
			{
				return this.m_contingentProperties.m_exceptionsHolder.CreateExceptionObject(false, ex);
			}
			if (ex != null)
			{
				return new AggregateException(new Exception[]
				{
					ex
				});
			}
			return null;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x0007FF74 File Offset: 0x0007E174
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			if (!this.IsFaulted || !this.ExceptionRecorded)
			{
				return new ReadOnlyCollection<ExceptionDispatchInfo>(Array.Empty<ExceptionDispatchInfo>());
			}
			return this.m_contingentProperties.m_exceptionsHolder.GetExceptionDispatchInfos();
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0007FFA8 File Offset: 0x0007E1A8
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null)
			{
				return null;
			}
			TaskExceptionHolder exceptionsHolder = contingentProperties.m_exceptionsHolder;
			if (exceptionsHolder == null)
			{
				return null;
			}
			return exceptionsHolder.GetCancellationExceptionDispatchInfo();
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x0007FFD8 File Offset: 0x0007E1D8
		internal void ThrowIfExceptional(bool includeTaskCanceledExceptions)
		{
			Exception exceptions = this.GetExceptions(includeTaskCanceledExceptions);
			if (exceptions != null)
			{
				this.UpdateExceptionObservedStatus();
				throw exceptions;
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x0007FFF8 File Offset: 0x0007E1F8
		internal void UpdateExceptionObservedStatus()
		{
			if (this.m_parent != null && (this.Options & TaskCreationOptions.AttachedToParent) != TaskCreationOptions.None && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && Task.InternalCurrent == this.m_parent)
			{
				this.m_stateFlags |= 524288;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060023CB RID: 9163 RVA: 0x00080049 File Offset: 0x0007E249
		internal bool IsExceptionObservedByParent
		{
			get
			{
				return (this.m_stateFlags & 524288) != 0;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x0008005C File Offset: 0x0007E25C
		internal bool IsDelegateInvoked
		{
			get
			{
				return (this.m_stateFlags & 131072) != 0;
			}
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x00080070 File Offset: 0x0007E270
		internal void Finish(bool bUserDelegateExecuted)
		{
			if (!bUserDelegateExecuted)
			{
				this.FinishStageTwo();
				return;
			}
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties == null || contingentProperties.m_completionCountdown == 1 || Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
			else
			{
				this.AtomicStateUpdate(8388608, 23068672);
			}
			LowLevelListWithIList<Task> lowLevelListWithIList = (contingentProperties != null) ? contingentProperties.m_exceptionalChildren : null;
			if (lowLevelListWithIList != null)
			{
				LowLevelListWithIList<Task> obj = lowLevelListWithIList;
				lock (obj)
				{
					lowLevelListWithIList.RemoveAll(Task.s_IsExceptionObservedByParentPredicate);
				}
			}
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x0008010C File Offset: 0x0007E30C
		internal void FinishStageTwo()
		{
			this.AddExceptionsFromChildren();
			int num;
			if (this.ExceptionRecorded)
			{
				num = 2097152;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(this);
			}
			else if (this.IsCancellationRequested && this.IsCancellationAcknowledged)
			{
				num = 4194304;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Canceled);
				}
				DebuggerSupport.RemoveFromActiveTasks(this);
			}
			else
			{
				num = 16777216;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
				}
				DebuggerSupport.RemoveFromActiveTasks(this);
			}
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | num);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.UnregisterCancellationCallback();
			}
			this.FinishStageThree();
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x000801C0 File Offset: 0x0007E3C0
		internal void FinishStageThree()
		{
			this.m_action = null;
			if (this.m_parent != null && (this.m_parent.CreationOptions & TaskCreationOptions.DenyChildAttach) == TaskCreationOptions.None && (this.m_stateFlags & 65535 & 4) != 0)
			{
				this.m_parent.ProcessChildCompletion(this);
			}
			this.FinishContinuations();
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00080210 File Offset: 0x0007E410
		internal void ProcessChildCompletion(Task childTask)
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (childTask.IsFaulted && !childTask.IsExceptionObservedByParent)
			{
				if (contingentProperties.m_exceptionalChildren == null)
				{
					Interlocked.CompareExchange<LowLevelListWithIList<Task>>(ref contingentProperties.m_exceptionalChildren, new LowLevelListWithIList<Task>(), null);
				}
				LowLevelListWithIList<Task> exceptionalChildren = contingentProperties.m_exceptionalChildren;
				if (exceptionalChildren != null)
				{
					LowLevelListWithIList<Task> obj = exceptionalChildren;
					lock (obj)
					{
						exceptionalChildren.Add(childTask);
					}
				}
			}
			if (Interlocked.Decrement(ref contingentProperties.m_completionCountdown) == 0)
			{
				this.FinishStageTwo();
			}
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x000802A0 File Offset: 0x0007E4A0
		internal void AddExceptionsFromChildren()
		{
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			LowLevelListWithIList<Task> lowLevelListWithIList = (contingentProperties != null) ? contingentProperties.m_exceptionalChildren : null;
			if (lowLevelListWithIList != null)
			{
				LowLevelListWithIList<Task> obj = lowLevelListWithIList;
				lock (obj)
				{
					foreach (Task task in ((IEnumerable<Task>)lowLevelListWithIList))
					{
						if (task.IsFaulted && !task.IsExceptionObservedByParent)
						{
							TaskExceptionHolder exceptionsHolder = task.m_contingentProperties.m_exceptionsHolder;
							this.AddException(exceptionsHolder.CreateExceptionObject(false, null));
						}
					}
				}
				contingentProperties.m_exceptionalChildren = null;
			}
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00080360 File Offset: 0x0007E560
		private void Execute()
		{
			try
			{
				this.InnerInvoke();
			}
			catch (Exception unhandledException)
			{
				this.HandleException(unhandledException);
			}
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00080390 File Offset: 0x0007E590
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.ExecuteEntry(false);
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x0008039C File Offset: 0x0007E59C
		internal bool ExecuteEntry(bool bPreventDoubleExecution)
		{
			if (bPreventDoubleExecution)
			{
				int num = 0;
				if (!this.AtomicStateUpdate(131072, 23199744, ref num) && (num & 4194304) == 0)
				{
					return false;
				}
			}
			else
			{
				this.m_stateFlags |= 131072;
			}
			if (!this.IsCancellationRequested && !this.IsCanceled)
			{
				this.ExecuteWithThreadLocal(ref Task.t_currentTask);
			}
			else if (!this.IsCanceled && (Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304) & 4194304) == 0)
			{
				this.CancellationCleanupLogic();
			}
			return true;
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00080430 File Offset: 0x0007E630
		private static void ExecutionContextCallback(object obj)
		{
			(obj as Task).Execute();
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00080440 File Offset: 0x0007E640
		internal virtual void InnerInvoke()
		{
			Action action = this.m_action as Action;
			if (action != null)
			{
				action();
				return;
			}
			Action<object> action2 = this.m_action as Action<object>;
			if (action2 != null)
			{
				action2(this.m_stateObject);
				return;
			}
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00080480 File Offset: 0x0007E680
		private void HandleException(Exception unhandledException)
		{
			OperationCanceledException ex = unhandledException as OperationCanceledException;
			if (ex != null && this.IsCancellationRequested && this.m_contingentProperties.m_cancellationToken == ex.CancellationToken)
			{
				this.SetCancellationAcknowledged();
				this.AddException(ex, true);
				return;
			}
			this.AddException(unhandledException);
		}

		/// <summary>Gets an awaiter used to await this <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <returns>An awaiter instance.</returns>
		// Token: 0x060023D8 RID: 9176 RVA: 0x000804CF File Offset: 0x0007E6CF
		public TaskAwaiter GetAwaiter()
		{
			return new TaskAwaiter(this);
		}

		/// <summary>Configures an awaiter used to await this <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="continueOnCapturedContext">
		///   <see langword="true" /> to attempt to marshal the continuation back to the original context captured; otherwise, <see langword="false" />.</param>
		/// <returns>An object used to await this task.</returns>
		// Token: 0x060023D9 RID: 9177 RVA: 0x000804D7 File Offset: 0x0007E6D7
		public ConfiguredTaskAwaitable ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredTaskAwaitable(this, continueOnCapturedContext);
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x000804E0 File Offset: 0x0007E6E0
		internal void SetContinuationForAwait(Action continuationAction, bool continueOnCapturedContext, bool flowExecutionContext)
		{
			TaskContinuation taskContinuation = null;
			if (continueOnCapturedContext)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					taskContinuation = new SynchronizationContextAwaitTaskContinuation(synchronizationContext, continuationAction, flowExecutionContext);
				}
				else
				{
					TaskScheduler internalCurrent = TaskScheduler.InternalCurrent;
					if (internalCurrent != null && internalCurrent != TaskScheduler.Default)
					{
						taskContinuation = new TaskSchedulerAwaitTaskContinuation(internalCurrent, continuationAction, flowExecutionContext);
					}
				}
			}
			if (taskContinuation != null)
			{
				if (!this.AddTaskContinuation(taskContinuation, false))
				{
					taskContinuation.Run(this, false);
					return;
				}
			}
			else if (!this.AddTaskContinuation(continuationAction, false))
			{
				AwaitTaskContinuation.UnsafeScheduleAction(continuationAction);
			}
		}

		/// <summary>Creates an awaitable task that asynchronously yields back to the current context when awaited.</summary>
		/// <returns>A context that, when awaited, will asynchronously transition back into the current context at the time of the await. If the current <see cref="T:System.Threading.SynchronizationContext" /> is non-null, it is treated as the current context. Otherwise, the task scheduler that is associated with the currently executing task is treated as the current context.</returns>
		// Token: 0x060023DB RID: 9179 RVA: 0x00080560 File Offset: 0x0007E760
		public static YieldAwaitable Yield()
		{
			return default(YieldAwaitable);
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The task was canceled. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains a <see cref="T:System.Threading.Tasks.TaskCanceledException" /> object.  
		///  -or-  
		///  An exception was thrown during the execution of the task. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains information about the exception or exceptions.</exception>
		// Token: 0x060023DC RID: 9180 RVA: 0x00080578 File Offset: 0x0007E778
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution within a specified time interval.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.Tasks.Task" /> completed execution within the allotted time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.AggregateException">The task was canceled. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains a <see cref="T:System.Threading.Tasks.TaskCanceledException" /> object.  
		///  -or-  
		///  An exception was thrown during the execution of the task. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains information about the exception or exceptions.</exception>
		// Token: 0x060023DD RID: 9181 RVA: 0x00080598 File Offset: 0x0007E798
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution. The wait terminates if a cancellation token is canceled before the task completes.</summary>
		/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The task has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The task was canceled. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains a <see cref="T:System.Threading.Tasks.TaskCanceledException" /> object.  
		///  -or-  
		///  An exception was thrown during the execution of the task. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains information about the exception or exceptions.</exception>
		// Token: 0x060023DE RID: 9182 RVA: 0x000805D8 File Offset: 0x0007E7D8
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution within a specified number of milliseconds.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.Tasks.Task" /> completed execution within the allotted time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.AggregateException">The task was canceled. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains a <see cref="T:System.Threading.Tasks.TaskCanceledException" /> object.  
		///  -or-  
		///  An exception was thrown during the execution of the task. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains information about the exception or exceptions.</exception>
		// Token: 0x060023DF RID: 9183 RVA: 0x000805E4 File Offset: 0x0007E7E4
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Waits for the <see cref="T:System.Threading.Tasks.Task" /> to complete execution. The wait terminates if a timeout interval elapses or a cancellation token is canceled before the task completes.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.Tasks.Task" /> completed execution within the allotted time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.AggregateException">The task was canceled. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains a <see cref="T:System.Threading.Tasks.TaskCanceledException" /> object.  
		///  -or-  
		///  An exception was thrown during the execution of the task. The <see cref="P:System.AggregateException.InnerExceptions" /> collection contains information about the exception or exceptions.</exception>
		// Token: 0x060023E0 RID: 9184 RVA: 0x00080604 File Offset: 0x0007E804
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				return true;
			}
			if (!this.InternalWait(millisecondsTimeout, cancellationToken))
			{
				return false;
			}
			if (this.IsWaitNotificationEnabledOrNotRanToCompletion)
			{
				this.NotifyDebuggerOfWaitCompletionIfNecessary();
				if (this.IsCanceled)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				this.ThrowIfExceptional(true);
			}
			return true;
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0008065C File Offset: 0x0007E85C
		private bool WrappedTryRunInline()
		{
			if (this.m_taskScheduler == null)
			{
				return false;
			}
			bool result;
			try
			{
				result = this.m_taskScheduler.TryRunInline(this, true);
			}
			catch (Exception innerException)
			{
				throw new TaskSchedulerException(innerException);
			}
			return result;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0008069C File Offset: 0x0007E89C
		[MethodImpl(MethodImplOptions.NoOptimization)]
		internal bool InternalWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (TaskTrace.Enabled)
			{
				Task internalCurrent = Task.InternalCurrent;
				TaskTrace.TaskWaitBegin_Synchronous((internalCurrent != null) ? internalCurrent.m_taskScheduler.Id : TaskScheduler.Default.Id, (internalCurrent != null) ? internalCurrent.Id : 0, this.Id);
			}
			bool flag = this.IsCompleted;
			if (!flag)
			{
				flag = ((millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled && this.WrappedTryRunInline() && this.IsCompleted) || this.SpinThenBlockingWait(millisecondsTimeout, cancellationToken));
			}
			if (TaskTrace.Enabled)
			{
				Task internalCurrent2 = Task.InternalCurrent;
				if (internalCurrent2 != null)
				{
					TaskTrace.TaskWaitEnd(internalCurrent2.m_taskScheduler.Id, internalCurrent2.Id, this.Id);
				}
				else
				{
					TaskTrace.TaskWaitEnd(TaskScheduler.Default.Id, 0, this.Id);
				}
			}
			return flag;
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x00080760 File Offset: 0x0007E960
		private bool SpinThenBlockingWait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = millisecondsTimeout == -1;
			uint num = (uint)(flag ? 0 : Environment.TickCount);
			bool flag2 = this.SpinWait(millisecondsTimeout);
			if (!flag2)
			{
				Task.SetOnInvokeMres setOnInvokeMres = new Task.SetOnInvokeMres();
				try
				{
					this.AddCompletionAction(setOnInvokeMres, true);
					if (flag)
					{
						flag2 = setOnInvokeMres.Wait(-1, cancellationToken);
					}
					else
					{
						uint num2 = (uint)(Environment.TickCount - (int)num);
						if ((ulong)num2 < (ulong)((long)millisecondsTimeout))
						{
							flag2 = setOnInvokeMres.Wait((int)((long)millisecondsTimeout - (long)((ulong)num2)), cancellationToken);
						}
					}
				}
				finally
				{
					if (!this.IsCompleted)
					{
						this.RemoveContinuation(setOnInvokeMres);
					}
				}
			}
			return flag2;
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x000807E8 File Offset: 0x0007E9E8
		private bool SpinWait(int millisecondsTimeout)
		{
			if (this.IsCompleted)
			{
				return true;
			}
			if (millisecondsTimeout == 0)
			{
				return false;
			}
			int spinCountforSpinBeforeWait = System.Threading.SpinWait.SpinCountforSpinBeforeWait;
			SpinWait spinWait = default(SpinWait);
			while (spinWait.Count < spinCountforSpinBeforeWait)
			{
				spinWait.SpinOnce(-1);
				if (this.IsCompleted)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x00080834 File Offset: 0x0007EA34
		internal bool InternalCancel(bool bCancelNonExecutingOnly)
		{
			bool flag = false;
			bool flag2 = false;
			TaskSchedulerException ex = null;
			if ((this.m_stateFlags & 65536) != 0)
			{
				TaskScheduler taskScheduler = this.m_taskScheduler;
				try
				{
					flag = (taskScheduler != null && taskScheduler.TryDequeue(this));
				}
				catch (Exception innerException)
				{
					ex = new TaskSchedulerException(innerException);
				}
				bool flag3 = taskScheduler != null && taskScheduler.RequiresAtomicStartTransition;
				if (!flag && bCancelNonExecutingOnly && flag3)
				{
					flag2 = this.AtomicStateUpdate(4194304, 4325376);
				}
			}
			if (!bCancelNonExecutingOnly || flag || flag2)
			{
				this.RecordInternalCancellationRequest();
				if (flag)
				{
					flag2 = this.AtomicStateUpdate(4194304, 4325376);
				}
				else if (!flag2 && (this.m_stateFlags & 65536) == 0)
				{
					flag2 = this.AtomicStateUpdate(4194304, 23265280);
				}
				if (flag2)
				{
					this.CancellationCleanupLogic();
				}
			}
			if (ex != null)
			{
				throw ex;
			}
			return flag2;
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0008090C File Offset: 0x0007EB0C
		internal void RecordInternalCancellationRequest()
		{
			this.EnsureContingentPropertiesInitialized(true).m_internalCancellationRequested = 1;
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x00080920 File Offset: 0x0007EB20
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord)
		{
			this.RecordInternalCancellationRequest();
			if (tokenToRecord != default(CancellationToken))
			{
				this.m_contingentProperties.m_cancellationToken = tokenToRecord;
			}
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00080952 File Offset: 0x0007EB52
		internal void RecordInternalCancellationRequest(CancellationToken tokenToRecord, object cancellationException)
		{
			this.RecordInternalCancellationRequest(tokenToRecord);
			if (cancellationException != null)
			{
				this.AddException(cancellationException, true);
			}
		}

		// Token: 0x060023E9 RID: 9193 RVA: 0x00080968 File Offset: 0x0007EB68
		internal void CancellationCleanupLogic()
		{
			Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 4194304);
			Task.ContingentProperties contingentProperties = this.m_contingentProperties;
			if (contingentProperties != null)
			{
				contingentProperties.SetCompleted();
				contingentProperties.UnregisterCancellationCallback();
			}
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Canceled);
			}
			DebuggerSupport.RemoveFromActiveTasks(this);
			this.FinishStageThree();
		}

		// Token: 0x060023EA RID: 9194 RVA: 0x000809C2 File Offset: 0x0007EBC2
		private void SetCancellationAcknowledged()
		{
			this.m_stateFlags |= 1048576;
		}

		// Token: 0x060023EB RID: 9195 RVA: 0x000809DC File Offset: 0x0007EBDC
		internal void FinishContinuations()
		{
			object obj = Interlocked.Exchange(ref this.m_continuationObject, Task.s_taskCompletionSentinel);
			if (obj != null)
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceSynchronousWorkStart(CausalityTraceLevel.Required, this, CausalitySynchronousWork.CompletionNotification);
				}
				bool flag = (this.m_stateFlags & 134217728) == 0 && (this.m_stateFlags & 64) == 0;
				Action action = obj as Action;
				if (action != null)
				{
					AwaitTaskContinuation.RunOrScheduleAction(action, flag, ref Task.t_currentTask);
					this.LogFinishCompletionNotification();
					return;
				}
				ITaskCompletionAction taskCompletionAction = obj as ITaskCompletionAction;
				if (taskCompletionAction != null)
				{
					if (flag || !taskCompletionAction.InvokeMayRunArbitraryCode)
					{
						taskCompletionAction.Invoke(this);
					}
					else
					{
						ThreadPool.UnsafeQueueCustomWorkItem(new CompletionActionInvoker(taskCompletionAction, this), false);
					}
					this.LogFinishCompletionNotification();
					return;
				}
				TaskContinuation taskContinuation = obj as TaskContinuation;
				if (taskContinuation != null)
				{
					taskContinuation.Run(this, flag);
					this.LogFinishCompletionNotification();
					return;
				}
				LowLevelListWithIList<object> lowLevelListWithIList = obj as LowLevelListWithIList<object>;
				if (lowLevelListWithIList == null)
				{
					this.LogFinishCompletionNotification();
					return;
				}
				LowLevelListWithIList<object> obj2 = lowLevelListWithIList;
				lock (obj2)
				{
				}
				int count = lowLevelListWithIList.Count;
				for (int i = 0; i < count; i++)
				{
					StandardTaskContinuation standardTaskContinuation = lowLevelListWithIList[i] as StandardTaskContinuation;
					if (standardTaskContinuation != null && (standardTaskContinuation.m_options & TaskContinuationOptions.ExecuteSynchronously) == TaskContinuationOptions.None)
					{
						lowLevelListWithIList[i] = null;
						standardTaskContinuation.Run(this, flag);
					}
				}
				for (int j = 0; j < count; j++)
				{
					object obj3 = lowLevelListWithIList[j];
					if (obj3 != null)
					{
						lowLevelListWithIList[j] = null;
						Action action2 = obj3 as Action;
						if (action2 != null)
						{
							AwaitTaskContinuation.RunOrScheduleAction(action2, flag, ref Task.t_currentTask);
						}
						else
						{
							TaskContinuation taskContinuation2 = obj3 as TaskContinuation;
							if (taskContinuation2 != null)
							{
								taskContinuation2.Run(this, flag);
							}
							else
							{
								ITaskCompletionAction taskCompletionAction2 = (ITaskCompletionAction)obj3;
								if (flag || !taskCompletionAction2.InvokeMayRunArbitraryCode)
								{
									taskCompletionAction2.Invoke(this);
								}
								else
								{
									ThreadPool.UnsafeQueueCustomWorkItem(new CompletionActionInvoker(taskCompletionAction2, this), false);
								}
							}
						}
					}
				}
				this.LogFinishCompletionNotification();
			}
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x00080BC4 File Offset: 0x0007EDC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void LogFinishCompletionNotification()
		{
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.CompletionNotification);
			}
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		// Token: 0x060023ED RID: 9197 RVA: 0x00080BD4 File Offset: 0x0007EDD4
		public Task ContinueWith(Action<Task> continuationAction)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that receives a cancellation token and executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		// Token: 0x060023EE RID: 9198 RVA: 0x00080BF7 File Offset: 0x0007EDF7
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes. The continuation uses a specified scheduler.</summary>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is null.</exception>
		// Token: 0x060023EF RID: 9199 RVA: 0x00080C08 File Offset: 0x0007EE08
		public Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes when the target task completes according to the specified <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</summary>
		/// <param name="continuationAction">An action to run according to the specified <paramref name="continuationOptions" />. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x060023F0 RID: 9200 RVA: 0x00080C28 File Offset: 0x0007EE28
		public Task ContinueWith(Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		/// <summary>Creates a continuation that executes when the target task competes according to the specified <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />. The continuation receives a cancellation token and uses a specified scheduler.</summary>
		/// <param name="continuationAction">An action to run according to the specified <paramref name="continuationOptions" />. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is null.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x060023F1 RID: 9201 RVA: 0x00080C4B File Offset: 0x0007EE4B
		public Task ContinueWith(Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060023F2 RID: 9202 RVA: 0x00080C58 File Offset: 0x0007EE58
		private Task ContinueWith(Action<Task> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task task = new ContinuationTaskFromTask(this, continuationAction, null, creationOptions, internalOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		/// <summary>Creates a continuation that receives caller-supplied state information and executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <param name="continuationAction">An action to run when the task completes. When run, the delegate is passed the completed task and a caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <returns>A new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		// Token: 0x060023F3 RID: 9203 RVA: 0x00080CA4 File Offset: 0x0007EEA4
		public Task ContinueWith(Action<Task, object> continuationAction, object state)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that receives caller-supplied state information and a cancellation token and that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes.</summary>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x060023F4 RID: 9204 RVA: 0x00080CC8 File Offset: 0x0007EEC8
		public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that receives caller-supplied state information and executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes. The continuation uses a specified scheduler.</summary>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes.  When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		// Token: 0x060023F5 RID: 9205 RVA: 0x00080CDC File Offset: 0x0007EEDC
		public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that receives caller-supplied state information and executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes. The continuation executes based on a set of specified conditions.</summary>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x060023F6 RID: 9206 RVA: 0x00080CFC File Offset: 0x0007EEFC
		public Task ContinueWith(Action<Task, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		/// <summary>Creates a continuation that receives caller-supplied state information and a cancellation token and that executes when the target <see cref="T:System.Threading.Tasks.Task" /> completes. The continuation executes based on a set of specified conditions and uses a specified scheduler.</summary>
		/// <param name="continuationAction">An action to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation action.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its  execution.</param>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x060023F7 RID: 9207 RVA: 0x00080D20 File Offset: 0x0007EF20
		public Task ContinueWith(Action<Task, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060023F8 RID: 9208 RVA: 0x00080D30 File Offset: 0x0007EF30
		private Task ContinueWith(Action<Task, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task task = new ContinuationTaskFromTask(this, continuationAction, state, creationOptions, internalOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task`1" /> completes and returns a value.</summary>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task`1" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		// Token: 0x060023F9 RID: 9209 RVA: 0x00080D7C File Offset: 0x0007EF7C
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction)
		{
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes and returns a value. The continuation receives a cancellation token.</summary>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		// Token: 0x060023FA RID: 9210 RVA: 0x00080D9F File Offset: 0x0007EF9F
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes and returns a value. The continuation uses a specified scheduler.</summary>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is null.</exception>
		// Token: 0x060023FB RID: 9211 RVA: 0x00080DB0 File Offset: 0x0007EFB0
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler)
		{
			return this.ContinueWith<TResult>(continuationFunction, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes according to the specified continuation options and returns a value.</summary>
		/// <param name="continuationFunction">A function to run according to the condition specified in <paramref name="continuationOptions" />. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x060023FC RID: 9212 RVA: 0x00080DD0 File Offset: 0x0007EFD0
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith<TResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		/// <summary>Creates a continuation that executes according to the specified continuation options and returns a value. The continuation is passed a cancellation token and uses a specified scheduler.</summary>
		/// <param name="continuationFunction">A function to run according to the specified continuationOptions. When run, the delegate will be passed the completed task as an argument.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created the token has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is null.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x060023FD RID: 9213 RVA: 0x00080DF3 File Offset: 0x0007EFF3
		public Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith<TResult>(continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x00080E00 File Offset: 0x0007F000
		private Task<TResult> ContinueWith<TResult>(Func<Task, TResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task<TResult> task = new ContinuationResultTaskFromTask<TResult>(this, continuationFunction, null, creationOptions, internalOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		/// <summary>Creates a continuation that receives caller-supplied state information and executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes and returns a value.</summary>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		// Token: 0x060023FF RID: 9215 RVA: 0x00080E4C File Offset: 0x0007F04C
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes and returns a value. The continuation receives caller-supplied state information and a cancellation token.</summary>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x06002400 RID: 9216 RVA: 0x00080E70 File Offset: 0x0007F070
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes asynchronously when the target <see cref="T:System.Threading.Tasks.Task" /> completes. The continuation receives caller-supplied state information and uses a specified scheduler.</summary>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes.  When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its execution.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		// Token: 0x06002401 RID: 9217 RVA: 0x00080E84 File Offset: 0x0007F084
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None);
		}

		/// <summary>Creates a continuation that executes based on the specified task continuation options when the target <see cref="T:System.Threading.Tasks.Task" /> completes. The continuation receives caller-supplied state information.</summary>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		// Token: 0x06002402 RID: 9218 RVA: 0x00080EA4 File Offset: 0x0007F0A4
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions);
		}

		/// <summary>Creates a continuation that executes based on the specified task continuation options when the target <see cref="T:System.Threading.Tasks.Task" /> completes and returns a value. The continuation receives caller-supplied state information and a cancellation token and uses the specified scheduler.</summary>
		/// <param name="continuationFunction">A function to run when the <see cref="T:System.Threading.Tasks.Task" /> completes. When run, the delegate will be  passed the completed task and the caller-supplied state object as arguments.</param>
		/// <param name="state">An object representing data to be used by the continuation function.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">Options for when the continuation is scheduled and how it behaves. This includes criteria, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.OnlyOnCanceled" />, as well as execution options, such as <see cref="F:System.Threading.Tasks.TaskContinuationOptions.ExecuteSynchronously" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to associate with the continuation task and to use for its  execution.</param>
		/// <typeparam name="TResult">The type of the result produced by the continuation.</typeparam>
		/// <returns>A new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value for <see cref="T:System.Threading.Tasks.TaskContinuationOptions" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		// Token: 0x06002403 RID: 9219 RVA: 0x00080EC8 File Offset: 0x0007F0C8
		public Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			return this.ContinueWith<TResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00080ED8 File Offset: 0x0007F0D8
		private Task<TResult> ContinueWith<TResult>(Func<Task, object, TResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task<TResult> task = new ContinuationResultTaskFromTask<TResult>(this, continuationFunction, state, creationOptions, internalOptions);
			this.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00080F24 File Offset: 0x0007F124
		internal static void CreationOptionsFromContinuationOptions(TaskContinuationOptions continuationOptions, out TaskCreationOptions creationOptions, out InternalTaskOptions internalOptions)
		{
			TaskContinuationOptions taskContinuationOptions = TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled;
			TaskContinuationOptions taskContinuationOptions2 = TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.RunContinuationsAsynchronously;
			TaskContinuationOptions taskContinuationOptions3 = TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously;
			if ((continuationOptions & taskContinuationOptions3) == taskContinuationOptions3)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", "The specified TaskContinuationOptions combined LongRunning and ExecuteSynchronously.  Synchronous continuations should not be long running.");
			}
			if ((continuationOptions & ~((taskContinuationOptions2 | taskContinuationOptions | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.ExecuteSynchronously) != TaskContinuationOptions.None)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & taskContinuationOptions) == taskContinuationOptions)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", "The specified TaskContinuationOptions excluded all continuation kinds.");
			}
			creationOptions = (TaskCreationOptions)(continuationOptions & taskContinuationOptions2);
			internalOptions = InternalTaskOptions.ContinuationTask;
			if ((continuationOptions & TaskContinuationOptions.LazyCancellation) != TaskContinuationOptions.None)
			{
				internalOptions |= InternalTaskOptions.LazyCancellation;
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x00080FA4 File Offset: 0x0007F1A4
		internal void ContinueWithCore(Task continuationTask, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions options)
		{
			TaskContinuation taskContinuation = new StandardTaskContinuation(continuationTask, options, scheduler);
			if (cancellationToken.CanBeCanceled)
			{
				if (this.IsCompleted || cancellationToken.IsCancellationRequested)
				{
					continuationTask.AssignCancellationToken(cancellationToken, null, null);
				}
				else
				{
					continuationTask.AssignCancellationToken(cancellationToken, this, taskContinuation);
				}
			}
			if (!continuationTask.IsCompleted && !this.AddTaskContinuation(taskContinuation, false))
			{
				taskContinuation.Run(this, true);
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x00081003 File Offset: 0x0007F203
		internal void AddCompletionAction(ITaskCompletionAction action)
		{
			this.AddCompletionAction(action, false);
		}

		// Token: 0x06002408 RID: 9224 RVA: 0x0008100D File Offset: 0x0007F20D
		private void AddCompletionAction(ITaskCompletionAction action, bool addBeforeOthers)
		{
			if (!this.AddTaskContinuation(action, addBeforeOthers))
			{
				action.Invoke(this);
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x00081020 File Offset: 0x0007F220
		private bool AddTaskContinuationComplex(object tc, bool addBeforeOthers)
		{
			object continuationObject = this.m_continuationObject;
			if (continuationObject != Task.s_taskCompletionSentinel && !(continuationObject is LowLevelListWithIList<object>))
			{
				Interlocked.CompareExchange(ref this.m_continuationObject, new LowLevelListWithIList<object>
				{
					continuationObject
				}, continuationObject);
			}
			LowLevelListWithIList<object> lowLevelListWithIList = this.m_continuationObject as LowLevelListWithIList<object>;
			if (lowLevelListWithIList != null)
			{
				LowLevelListWithIList<object> obj = lowLevelListWithIList;
				lock (obj)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						if (lowLevelListWithIList.Count == lowLevelListWithIList.Capacity)
						{
							lowLevelListWithIList.RemoveAll(Task.s_IsTaskContinuationNullPredicate);
						}
						if (addBeforeOthers)
						{
							lowLevelListWithIList.Insert(0, tc);
						}
						else
						{
							lowLevelListWithIList.Add(tc);
						}
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x000810E4 File Offset: 0x0007F2E4
		private bool AddTaskContinuation(object tc, bool addBeforeOthers)
		{
			return !this.IsCompleted && ((this.m_continuationObject == null && Interlocked.CompareExchange(ref this.m_continuationObject, tc, null) == null) || this.AddTaskContinuationComplex(tc, addBeforeOthers));
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x00081114 File Offset: 0x0007F314
		internal void RemoveContinuation(object continuationObject)
		{
			object continuationObject2 = this.m_continuationObject;
			if (continuationObject2 == Task.s_taskCompletionSentinel)
			{
				return;
			}
			LowLevelListWithIList<object> lowLevelListWithIList = continuationObject2 as LowLevelListWithIList<object>;
			if (lowLevelListWithIList == null)
			{
				if (Interlocked.CompareExchange(ref this.m_continuationObject, new LowLevelListWithIList<object>(), continuationObject) == continuationObject)
				{
					return;
				}
				lowLevelListWithIList = (this.m_continuationObject as LowLevelListWithIList<object>);
			}
			if (lowLevelListWithIList != null)
			{
				LowLevelListWithIList<object> obj = lowLevelListWithIList;
				lock (obj)
				{
					if (this.m_continuationObject != Task.s_taskCompletionSentinel)
					{
						int num = lowLevelListWithIList.IndexOf(continuationObject);
						if (num != -1)
						{
							lowLevelListWithIList[num] = null;
						}
					}
				}
			}
		}

		/// <summary>Waits for all of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.  
		///  -or-  
		///  The <paramref name="tasks" /> argument is an empty array.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled. If a task was canceled, the <see cref="T:System.AggregateException" /> exception contains an <see cref="T:System.OperationCanceledException" /> exception in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.  
		///  -or-  
		///  An exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances.</exception>
		// Token: 0x0600240C RID: 9228 RVA: 0x000811B8 File Offset: 0x0007F3B8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static void WaitAll(params Task[] tasks)
		{
			Task.WaitAll(tasks, -1);
		}

		/// <summary>Waits for all of the provided cancellable <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified time interval.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if all of the <see cref="T:System.Threading.Tasks.Task" /> instances completed execution within the allotted time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.  
		///  -or-  
		///  An exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.  
		///  -or-  
		///  The <paramref name="tasks" /> argument is an empty array.</exception>
		// Token: 0x0600240D RID: 9229 RVA: 0x000811C4 File Offset: 0x0007F3C4
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return Task.WaitAll(tasks, (int)num);
		}

		/// <summary>Waits for all of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified number of milliseconds.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if all of the <see cref="T:System.Threading.Tasks.Task" /> instances completed execution within the allotted time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.  
		///  -or-  
		///  An exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.  
		///  -or-  
		///  The <paramref name="tasks" /> argument is an empty array.</exception>
		// Token: 0x0600240E RID: 9230 RVA: 0x000811FC File Offset: 0x0007F3FC
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, int millisecondsTimeout)
		{
			return Task.WaitAll(tasks, millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Waits for all of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution unless the wait is cancelled.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for the tasks to complete.</param>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.  
		///  -or-  
		///  An exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.  
		///  -or-  
		///  The <paramref name="tasks" /> argument is an empty array.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		// Token: 0x0600240F RID: 9231 RVA: 0x00081219 File Offset: 0x0007F419
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static void WaitAll(Task[] tasks, CancellationToken cancellationToken)
		{
			Task.WaitAll(tasks, -1, cancellationToken);
		}

		/// <summary>Waits for all of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified number of milliseconds or until the wait is cancelled.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for the tasks to complete.</param>
		/// <returns>
		///   <see langword="true" /> if all of the <see cref="T:System.Threading.Tasks.Task" /> instances completed execution within the allotted time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One or more of the <see cref="T:System.Threading.Tasks.Task" /> objects in <paramref name="tasks" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">At least one of the <see cref="T:System.Threading.Tasks.Task" /> instances was canceled. If a task was canceled, the <see cref="T:System.AggregateException" /> contains an <see cref="T:System.OperationCanceledException" /> in its <see cref="P:System.AggregateException.InnerExceptions" /> collection.  
		///  -or-  
		///  An exception was thrown during the execution of at least one of the <see cref="T:System.Threading.Tasks.Task" /> instances.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.  
		///  -or-  
		///  The <paramref name="tasks" /> argument is an empty array.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		// Token: 0x06002410 RID: 9232 RVA: 0x00081224 File Offset: 0x0007F424
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static bool WaitAll(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			cancellationToken.ThrowIfCancellationRequested();
			LowLevelListWithIList<Exception> innerExceptions = null;
			LowLevelListWithIList<Task> lowLevelListWithIList = null;
			LowLevelListWithIList<Task> lowLevelListWithIList2 = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			for (int i = tasks.Length - 1; i >= 0; i--)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException("The tasks array included at least one null element.", "tasks");
				}
				bool flag4 = task.IsCompleted;
				if (!flag4)
				{
					if (millisecondsTimeout != -1 || cancellationToken.CanBeCanceled)
					{
						Task.AddToList<Task>(task, ref lowLevelListWithIList, tasks.Length);
					}
					else
					{
						flag4 = (task.WrappedTryRunInline() && task.IsCompleted);
						if (!flag4)
						{
							Task.AddToList<Task>(task, ref lowLevelListWithIList, tasks.Length);
						}
					}
				}
				if (flag4)
				{
					if (task.IsFaulted)
					{
						flag = true;
					}
					else if (task.IsCanceled)
					{
						flag2 = true;
					}
					if (task.IsWaitNotificationEnabled)
					{
						Task.AddToList<Task>(task, ref lowLevelListWithIList2, 1);
					}
				}
			}
			if (lowLevelListWithIList != null)
			{
				flag3 = Task.WaitAllBlockingCore(lowLevelListWithIList, millisecondsTimeout, cancellationToken);
				if (flag3)
				{
					foreach (Task task2 in ((IEnumerable<Task>)lowLevelListWithIList))
					{
						if (task2.IsFaulted)
						{
							flag = true;
						}
						else if (task2.IsCanceled)
						{
							flag2 = true;
						}
						if (task2.IsWaitNotificationEnabled)
						{
							Task.AddToList<Task>(task2, ref lowLevelListWithIList2, 1);
						}
					}
				}
				GC.KeepAlive(tasks);
			}
			if (flag3 && lowLevelListWithIList2 != null)
			{
				using (IEnumerator<Task> enumerator = ((IEnumerable<Task>)lowLevelListWithIList2).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.NotifyDebuggerOfWaitCompletionIfNecessary())
						{
							break;
						}
					}
				}
			}
			if (flag3 && (flag || flag2))
			{
				if (!flag)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				foreach (Task t in tasks)
				{
					Task.AddExceptionsForCompletedTask(ref innerExceptions, t);
				}
				throw new AggregateException(innerExceptions);
			}
			return flag3;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00081418 File Offset: 0x0007F618
		private static void AddToList<T>(T item, ref LowLevelListWithIList<T> list, int initSize)
		{
			if (list == null)
			{
				list = new LowLevelListWithIList<T>(initSize);
			}
			list.Add(item);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00081430 File Offset: 0x0007F630
		private static bool WaitAllBlockingCore(LowLevelListWithIList<Task> tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			bool flag = false;
			Task.SetOnCountdownMres setOnCountdownMres = new Task.SetOnCountdownMres(tasks.Count);
			try
			{
				foreach (Task task in ((IEnumerable<Task>)tasks))
				{
					task.AddCompletionAction(setOnCountdownMres, true);
				}
				flag = setOnCountdownMres.Wait(millisecondsTimeout, cancellationToken);
			}
			finally
			{
				if (!flag)
				{
					foreach (Task task2 in ((IEnumerable<Task>)tasks))
					{
						if (!task2.IsCompleted)
						{
							task2.RemoveContinuation(setOnCountdownMres);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x000814E0 File Offset: 0x0007F6E0
		internal static void AddExceptionsForCompletedTask(ref LowLevelListWithIList<Exception> exceptions, Task t)
		{
			AggregateException exceptions2 = t.GetExceptions(true);
			if (exceptions2 != null)
			{
				t.UpdateExceptionObservedStatus();
				if (exceptions == null)
				{
					exceptions = new LowLevelListWithIList<Exception>(exceptions2.InnerExceptions.Count);
				}
				exceptions.AddRange(exceptions2.InnerExceptions);
			}
		}

		/// <summary>Waits for any of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <returns>The index of the completed <see cref="T:System.Threading.Tasks.Task" /> object in the <paramref name="tasks" /> array.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		// Token: 0x06002414 RID: 9236 RVA: 0x00081521 File Offset: 0x0007F721
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(params Task[] tasks)
		{
			return Task.WaitAny(tasks, -1);
		}

		/// <summary>Waits for any of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified time interval.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument, or -1 if the timeout occurred.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		// Token: 0x06002415 RID: 9237 RVA: 0x0008152C File Offset: 0x0007F72C
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return Task.WaitAny(tasks, (int)num);
		}

		/// <summary>Waits for any of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution unless the wait is cancelled.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for a task to complete.</param>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		// Token: 0x06002416 RID: 9238 RVA: 0x00081563 File Offset: 0x0007F763
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, CancellationToken cancellationToken)
		{
			return Task.WaitAny(tasks, -1, cancellationToken);
		}

		/// <summary>Waits for any of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified number of milliseconds.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument, or -1 if the timeout occurred.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		// Token: 0x06002417 RID: 9239 RVA: 0x00081570 File Offset: 0x0007F770
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, int millisecondsTimeout)
		{
			return Task.WaitAny(tasks, millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Waits for any of the provided <see cref="T:System.Threading.Tasks.Task" /> objects to complete execution within a specified number of milliseconds or until a cancellation token is cancelled.</summary>
		/// <param name="tasks">An array of <see cref="T:System.Threading.Tasks.Task" /> instances on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">A <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> to observe while waiting for a task to complete.</param>
		/// <returns>The index of the completed task in the <paramref name="tasks" /> array argument, or -1 if the timeout occurred.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Tasks.Task" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> argument contains a null element.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <paramref name="cancellationToken" /> was canceled.</exception>
		// Token: 0x06002418 RID: 9240 RVA: 0x00081590 File Offset: 0x0007F790
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public static int WaitAny(Task[] tasks, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			cancellationToken.ThrowIfCancellationRequested();
			int num = -1;
			for (int i = 0; i < tasks.Length; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException("The tasks array included at least one null element.", "tasks");
				}
				if (num == -1 && task.IsCompleted)
				{
					num = i;
				}
			}
			if (num == -1 && tasks.Length != 0)
			{
				Task<Task> task2 = TaskFactory.CommonCWAnyLogic(tasks);
				if (task2.Wait(millisecondsTimeout, cancellationToken))
				{
					num = Array.IndexOf<Task>(tasks, task2.Result);
				}
			}
			GC.KeepAlive(tasks);
			return num;
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that's completed successfully with the specified result.</summary>
		/// <param name="result">The result to store into the completed task.</param>
		/// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
		/// <returns>The successfully completed task.</returns>
		// Token: 0x06002419 RID: 9241 RVA: 0x00081623 File Offset: 0x0007F823
		public static Task<TResult> FromResult<TResult>(TResult result)
		{
			return new Task<TResult>(result);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that has completed with a specified exception.</summary>
		/// <param name="exception">The exception with which to complete the task.</param>
		/// <returns>The faulted task.</returns>
		// Token: 0x0600241A RID: 9242 RVA: 0x0008162B File Offset: 0x0007F82B
		public static Task FromException(Exception exception)
		{
			return Task.FromException<VoidTaskResult>(exception);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that's completed with a specified exception.</summary>
		/// <param name="exception">The exception with which to complete the task.</param>
		/// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
		/// <returns>The faulted task.</returns>
		// Token: 0x0600241B RID: 9243 RVA: 0x00081633 File Offset: 0x0007F833
		public static Task<TResult> FromException<TResult>(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = new Task<TResult>();
			task.TrySetException(exception);
			return task;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x00081650 File Offset: 0x0007F850
		internal static Task FromCancellation(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				throw new ArgumentOutOfRangeException("cancellationToken");
			}
			return new Task(true, TaskCreationOptions.None, cancellationToken);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that's completed due to cancellation with a specified cancellation token.</summary>
		/// <param name="cancellationToken">The cancellation token with which to complete the task.</param>
		/// <returns>The canceled task.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Cancellation has not been requested for <paramref name="cancellationToken" />; its <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> property is <see langword="false" />.</exception>
		// Token: 0x0600241D RID: 9245 RVA: 0x0008166E File Offset: 0x0007F86E
		public static Task FromCanceled(CancellationToken cancellationToken)
		{
			return Task.FromCancellation(cancellationToken);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x00081678 File Offset: 0x0007F878
		internal static Task<TResult> FromCancellation<TResult>(CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				throw new ArgumentOutOfRangeException("cancellationToken");
			}
			return new Task<TResult>(true, default(TResult), TaskCreationOptions.None, cancellationToken);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that's completed due to cancellation with a specified cancellation token.</summary>
		/// <param name="cancellationToken">The cancellation token with which to complete the task.</param>
		/// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
		/// <returns>The canceled task.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Cancellation has not been requested for <paramref name="cancellationToken" />; its <see cref="P:System.Threading.CancellationToken.IsCancellationRequested" /> property is <see langword="false" />.</exception>
		// Token: 0x0600241F RID: 9247 RVA: 0x000816AA File Offset: 0x0007F8AA
		public static Task<TResult> FromCanceled<TResult>(CancellationToken cancellationToken)
		{
			return Task.FromCancellation<TResult>(cancellationToken);
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000816B2 File Offset: 0x0007F8B2
		internal static Task<TResult> FromCancellation<TResult>(OperationCanceledException exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			Task<TResult> task = new Task<TResult>();
			task.TrySetCanceled(exception.CancellationToken, exception);
			return task;
		}

		/// <summary>Queues the specified work to run on the thread pool and returns a <see cref="T:System.Threading.Tasks.Task" /> object that represents that work.</summary>
		/// <param name="action">The work to execute asynchronously</param>
		/// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> parameter was <see langword="null" />.</exception>
		// Token: 0x06002421 RID: 9249 RVA: 0x000816D8 File Offset: 0x0007F8D8
		public static Task Run(Action action)
		{
			return Task.InternalStartNew(null, action, null, default(CancellationToken), TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None);
		}

		/// <summary>Queues the specified work to run on the thread pool and returns a <see cref="T:System.Threading.Tasks.Task" /> object that represents that work. A cancellation token allows the work to be cancelled.</summary>
		/// <param name="action">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that can be used to cancel the work</param>
		/// <returns>A task that represents the work queued to execute in the thread pool.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> parameter was <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x06002422 RID: 9250 RVA: 0x000816FD File Offset: 0x0007F8FD
		public static Task Run(Action action, CancellationToken cancellationToken)
		{
			return Task.InternalStartNew(null, action, null, cancellationToken, TaskScheduler.Default, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None);
		}

		/// <summary>Queues the specified work to run on the thread pool and returns a <see cref="T:System.Threading.Tasks.Task`1" /> object that represents that work.</summary>
		/// <param name="function">The work to execute asynchronously.</param>
		/// <typeparam name="TResult">The return type of the task.</typeparam>
		/// <returns>A task object that represents the work queued to execute in the thread pool.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002423 RID: 9251 RVA: 0x00081710 File Offset: 0x0007F910
		public static Task<TResult> Run<TResult>(Func<TResult> function)
		{
			return Task<TResult>.StartNew(null, function, default(CancellationToken), TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default);
		}

		/// <summary>Queues the specified work to run on the thread pool and returns a <see langword="Task(TResult)" /> object that represents that work. A cancellation token allows the work to be cancelled.</summary>
		/// <param name="function">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
		/// <typeparam name="TResult">The result type of the task.</typeparam>
		/// <returns>A <see langword="Task(TResult)" /> that represents the work queued to execute in the thread pool.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x06002424 RID: 9252 RVA: 0x00081734 File Offset: 0x0007F934
		public static Task<TResult> Run<TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			return Task<TResult>.StartNew(null, function, cancellationToken, TaskCreationOptions.DenyChildAttach, InternalTaskOptions.None, TaskScheduler.Default);
		}

		/// <summary>Queues the specified work to run on the thread pool and returns a proxy for the  task returned by <paramref name="function" />.</summary>
		/// <param name="function">The work to execute asynchronously</param>
		/// <returns>A task that represents a proxy for the task returned by <paramref name="function" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was <see langword="null" />.</exception>
		// Token: 0x06002425 RID: 9253 RVA: 0x00081748 File Offset: 0x0007F948
		public static Task Run(Func<Task> function)
		{
			return Task.Run(function, default(CancellationToken));
		}

		/// <summary>Queues the specified work to run on the thread pool and returns a proxy for the task returned by <paramref name="function" />.</summary>
		/// <param name="function">The work to execute asynchronously.</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work.</param>
		/// <returns>A task that represents a proxy for the task returned by <paramref name="function" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x06002426 RID: 9254 RVA: 0x00081764 File Offset: 0x0007F964
		public static Task Run(Func<Task> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			return new UnwrapPromise<VoidTaskResult>(Task.Factory.StartNew<Task>(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
		}

		/// <summary>Queues the specified work to run on the thread pool and returns a proxy for the <see langword="Task(TResult)" /> returned by <paramref name="function" />.</summary>
		/// <param name="function">The work to execute asynchronously</param>
		/// <typeparam name="TResult">The type of the result returned by the proxy task.</typeparam>
		/// <returns>A <see langword="Task(TResult)" /> that represents a proxy for the <see langword="Task(TResult)" /> returned by <paramref name="function" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was <see langword="null" />.</exception>
		// Token: 0x06002427 RID: 9255 RVA: 0x0008179C File Offset: 0x0007F99C
		public static Task<TResult> Run<TResult>(Func<Task<TResult>> function)
		{
			return Task.Run<TResult>(function, default(CancellationToken));
		}

		/// <summary>Queues the specified work to run on the thread pool and returns a proxy for the <see langword="Task(TResult)" /> returned by <paramref name="function" />.</summary>
		/// <param name="function">The work to execute asynchronously</param>
		/// <param name="cancellationToken">A cancellation token that should be used to cancel the work</param>
		/// <typeparam name="TResult">The type of the result returned by the proxy task.</typeparam>
		/// <returns>A <see langword="Task(TResult)" /> that represents a proxy for the <see langword="Task(TResult)" /> returned by <paramref name="function" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> parameter was <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with <paramref name="cancellationToken" /> was disposed.</exception>
		// Token: 0x06002428 RID: 9256 RVA: 0x000817B8 File Offset: 0x0007F9B8
		public static Task<TResult> Run<TResult>(Func<Task<TResult>> function, CancellationToken cancellationToken)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<TResult>(cancellationToken);
			}
			return new UnwrapPromise<TResult>(Task.Factory.StartNew<Task<TResult>>(function, cancellationToken, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default), true);
		}

		/// <summary>Creates a task that completes after a specified time interval.</summary>
		/// <param name="delay">The time span to wait before completing the returned task, or <see langword="TimeSpan.FromMilliseconds(-1)" /> to wait indefinitely.</param>
		/// <returns>A task that represents the time delay.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delay" /> represents a negative time interval other than <see langword="TimeSpan.FromMillseconds(-1)" />.  
		/// -or-  
		/// The <paramref name="delay" /> argument's <see cref="P:System.TimeSpan.TotalMilliseconds" /> property is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06002429 RID: 9257 RVA: 0x000817F0 File Offset: 0x0007F9F0
		public static Task Delay(TimeSpan delay)
		{
			return Task.Delay(delay, default(CancellationToken));
		}

		/// <summary>Creates a cancellable task that completes after a specified time interval.</summary>
		/// <param name="delay">The time span to wait before completing the returned task, or <see langword="TimeSpan.FromMilliseconds(-1)" /> to wait indefinitely.</param>
		/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the time delay.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delay" /> represents a negative time interval other than <see langword="TimeSpan.FromMillseconds(-1)" />.  
		/// -or-  
		/// The <paramref name="delay" /> argument's <see cref="P:System.TimeSpan.TotalMilliseconds" /> property is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x0600242A RID: 9258 RVA: 0x0008180C File Offset: 0x0007FA0C
		public static Task Delay(TimeSpan delay, CancellationToken cancellationToken)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay", "The value needs to translate in milliseconds to -1 (signifying an infinite timeout), 0 or a positive integer less than or equal to Int32.MaxValue.");
			}
			return Task.Delay((int)num, cancellationToken);
		}

		/// <summary>Creates a task that completes after a specified number of milliseconds.</summary>
		/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned task, or -1 to wait indefinitely.</param>
		/// <returns>A task that represents the time delay.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsDelay" /> argument is less than -1.</exception>
		// Token: 0x0600242B RID: 9259 RVA: 0x00081848 File Offset: 0x0007FA48
		public static Task Delay(int millisecondsDelay)
		{
			return Task.Delay(millisecondsDelay, default(CancellationToken));
		}

		/// <summary>Creates a cancellable task that completes after a specified number of milliseconds.</summary>
		/// <param name="millisecondsDelay">The number of milliseconds to wait before completing the returned task, or -1 to wait indefinitely.</param>
		/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
		/// <returns>A task that represents the time delay.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsDelay" /> argument is less than -1.</exception>
		/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The provided <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x0600242C RID: 9260 RVA: 0x00081864 File Offset: 0x0007FA64
		public static Task Delay(int millisecondsDelay, CancellationToken cancellationToken)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay", "The value needs to be either -1 (signifying an infinite timeout), 0 or a positive integer.");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation(cancellationToken);
			}
			if (millisecondsDelay == 0)
			{
				return Task.CompletedTask;
			}
			Task.DelayPromise delayPromise = new Task.DelayPromise(cancellationToken);
			if (cancellationToken.CanBeCanceled)
			{
				delayPromise.Registration = cancellationToken.InternalRegisterWithoutEC(delegate(object state)
				{
					((Task.DelayPromise)state).Complete();
				}, delayPromise);
			}
			if (millisecondsDelay != -1)
			{
				delayPromise.Timer = new Timer(delegate(object state)
				{
					((Task.DelayPromise)state).Complete();
				}, delayPromise, millisecondsDelay, -1);
				delayPromise.Timer.KeepRootedWhileScheduled();
			}
			return delayPromise;
		}

		/// <summary>Creates a task that will complete when all of the <see cref="T:System.Threading.Tasks.Task" /> objects in an enumerable collection have completed.</summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> collection contained a <see langword="null" /> task.</exception>
		// Token: 0x0600242D RID: 9261 RVA: 0x00081918 File Offset: 0x0007FB18
		public static Task WhenAll(IEnumerable<Task> tasks)
		{
			Task[] array = tasks as Task[];
			if (array != null)
			{
				return Task.WhenAll(array);
			}
			ICollection<Task> collection = tasks as ICollection<Task>;
			if (collection != null)
			{
				int num = 0;
				array = new Task[collection.Count];
				foreach (Task task in tasks)
				{
					if (task == null)
					{
						throw new ArgumentException("The tasks argument included a null value.", "tasks");
					}
					array[num++] = task;
				}
				return Task.InternalWhenAll(array);
			}
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			LowLevelListWithIList<Task> lowLevelListWithIList = new LowLevelListWithIList<Task>();
			foreach (Task task2 in tasks)
			{
				if (task2 == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				lowLevelListWithIList.Add(task2);
			}
			return Task.InternalWhenAll(lowLevelListWithIList.ToArray());
		}

		/// <summary>Creates a task that will complete when all of the <see cref="T:System.Threading.Tasks.Task" /> objects in an array have completed.</summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a <see langword="null" /> task.</exception>
		// Token: 0x0600242E RID: 9262 RVA: 0x00081A20 File Offset: 0x0007FC20
		public static Task WhenAll(params Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			int num = tasks.Length;
			if (num == 0)
			{
				return Task.InternalWhenAll(tasks);
			}
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				array[i] = task;
			}
			return Task.InternalWhenAll(array);
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x00081A7D File Offset: 0x0007FC7D
		private static Task InternalWhenAll(Task[] tasks)
		{
			if (tasks.Length != 0)
			{
				return new Task.WhenAllPromise(tasks);
			}
			return Task.CompletedTask;
		}

		/// <summary>Creates a task that will complete when all of the <see cref="T:System.Threading.Tasks.Task`1" /> objects in an enumerable collection have completed.</summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <typeparam name="TResult">The type of the completed task.</typeparam>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> collection contained a <see langword="null" /> task.</exception>
		// Token: 0x06002430 RID: 9264 RVA: 0x00081A90 File Offset: 0x0007FC90
		public static Task<TResult[]> WhenAll<TResult>(IEnumerable<Task<TResult>> tasks)
		{
			Task<TResult>[] array = tasks as Task<TResult>[];
			if (array != null)
			{
				return Task.WhenAll<TResult>(array);
			}
			ICollection<Task<TResult>> collection = tasks as ICollection<Task<TResult>>;
			if (collection != null)
			{
				int num = 0;
				array = new Task<TResult>[collection.Count];
				foreach (Task<TResult> task in tasks)
				{
					if (task == null)
					{
						throw new ArgumentException("The tasks argument included a null value.", "tasks");
					}
					array[num++] = task;
				}
				return Task.InternalWhenAll<TResult>(array);
			}
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			LowLevelListWithIList<Task<TResult>> lowLevelListWithIList = new LowLevelListWithIList<Task<TResult>>();
			foreach (Task<TResult> task2 in tasks)
			{
				if (task2 == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				lowLevelListWithIList.Add(task2);
			}
			return Task.InternalWhenAll<TResult>(lowLevelListWithIList.ToArray());
		}

		/// <summary>Creates a task that will complete when all of the <see cref="T:System.Threading.Tasks.Task`1" /> objects in an array have completed.</summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <typeparam name="TResult">The type of the completed task.</typeparam>
		/// <returns>A task that represents the completion of all of the supplied tasks.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a <see langword="null" /> task.</exception>
		// Token: 0x06002431 RID: 9265 RVA: 0x00081B98 File Offset: 0x0007FD98
		public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			int num = tasks.Length;
			if (num == 0)
			{
				return Task.InternalWhenAll<TResult>(tasks);
			}
			Task<TResult>[] array = new Task<TResult>[num];
			for (int i = 0; i < num; i++)
			{
				Task<TResult> task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				array[i] = task;
			}
			return Task.InternalWhenAll<TResult>(array);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x00081BF8 File Offset: 0x0007FDF8
		private static Task<TResult[]> InternalWhenAll<TResult>(Task<TResult>[] tasks)
		{
			if (tasks.Length != 0)
			{
				return new Task.WhenAllPromise<TResult>(tasks);
			}
			return new Task<TResult[]>(false, Array.Empty<TResult>(), TaskCreationOptions.None, default(CancellationToken));
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06002433 RID: 9267 RVA: 0x00081C28 File Offset: 0x0007FE28
		public static Task<Task> WhenAny(params Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			int num = tasks.Length;
			Task[] array = new Task[num];
			for (int i = 0; i < num; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				array[i] = task;
			}
			return TaskFactory.CommonCWAnyLogic(array);
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06002434 RID: 9268 RVA: 0x00081C90 File Offset: 0x0007FE90
		public static Task<Task> WhenAny(IEnumerable<Task> tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			LowLevelListWithIList<Task> lowLevelListWithIList = new LowLevelListWithIList<Task>();
			foreach (Task task in tasks)
			{
				if (task == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				lowLevelListWithIList.Add(task);
			}
			if (lowLevelListWithIList.Count == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			return TaskFactory.CommonCWAnyLogic(lowLevelListWithIList);
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <typeparam name="TResult">The type of the completed task.</typeparam>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06002435 RID: 9269 RVA: 0x00081D20 File Offset: 0x0007FF20
		public static Task<Task<TResult>> WhenAny<TResult>(params Task<TResult>[] tasks)
		{
			return Task.WhenAny(tasks).ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast.Value, default(CancellationToken), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		/// <summary>Creates a task that will complete when any of the supplied tasks have completed.</summary>
		/// <param name="tasks">The tasks to wait on for completion.</param>
		/// <typeparam name="TResult">The type of the completed task.</typeparam>
		/// <returns>A task that represents the completion of one of the supplied tasks.  The return task's Result is the task that completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> argument was <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contained a null task, or was empty.</exception>
		// Token: 0x06002436 RID: 9270 RVA: 0x00081D54 File Offset: 0x0007FF54
		public static Task<Task<TResult>> WhenAny<TResult>(IEnumerable<Task<TResult>> tasks)
		{
			return Task.WhenAny(tasks).ContinueWith<Task<TResult>>(Task<TResult>.TaskWhenAnyCast.Value, default(CancellationToken), TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00081D84 File Offset: 0x0007FF84
		public static Task<TResult> CreateUnwrapPromise<TResult>(Task outerTask, bool lookForOce)
		{
			return new UnwrapPromise<TResult>(outerTask, lookForOce);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x00081D8D File Offset: 0x0007FF8D
		internal virtual Delegate[] GetDelegateContinuationsForDebugger()
		{
			return Task.GetDelegatesFromContinuationObject(this.m_continuationObject);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00081D9C File Offset: 0x0007FF9C
		private static Delegate[] GetDelegatesFromContinuationObject(object continuationObject)
		{
			if (continuationObject != null)
			{
				Action action = continuationObject as Action;
				if (action != null)
				{
					return new Delegate[]
					{
						AsyncMethodBuilderCore.TryGetStateMachineForDebugger(action)
					};
				}
				TaskContinuation taskContinuation = continuationObject as TaskContinuation;
				if (taskContinuation != null)
				{
					return taskContinuation.GetDelegateContinuationsForDebugger();
				}
				Task task = continuationObject as Task;
				if (task != null)
				{
					return task.GetDelegateContinuationsForDebugger();
				}
				ITaskCompletionAction taskCompletionAction = continuationObject as ITaskCompletionAction;
				if (taskCompletionAction != null)
				{
					return new Delegate[]
					{
						new Action<Task>(taskCompletionAction.Invoke)
					};
				}
				LowLevelListWithIList<object> lowLevelListWithIList = continuationObject as LowLevelListWithIList<object>;
				if (lowLevelListWithIList != null)
				{
					LowLevelListWithIList<Delegate> lowLevelListWithIList2 = new LowLevelListWithIList<Delegate>();
					foreach (object continuationObject2 in ((IEnumerable<object>)lowLevelListWithIList))
					{
						Delegate[] delegatesFromContinuationObject = Task.GetDelegatesFromContinuationObject(continuationObject2);
						if (delegatesFromContinuationObject != null)
						{
							foreach (Delegate @delegate in delegatesFromContinuationObject)
							{
								if (@delegate != null)
								{
									lowLevelListWithIList2.Add(@delegate);
								}
							}
						}
					}
					return lowLevelListWithIList2.ToArray();
				}
			}
			return null;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x00081E98 File Offset: 0x00080098
		private static Task GetActiveTaskFromId(int taskId)
		{
			return DebuggerSupport.GetActiveTaskFromId(taskId);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x00081EA0 File Offset: 0x000800A0
		[FriendAccessAllowed]
		internal static bool AddToActiveTasks(Task task)
		{
			object obj = Task.s_activeTasksLock;
			lock (obj)
			{
				Task.s_currentActiveTasks[task.Id] = task;
			}
			return true;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x00081EEC File Offset: 0x000800EC
		[FriendAccessAllowed]
		internal static void RemoveFromActiveTasks(int taskId)
		{
			object obj = Task.s_activeTasksLock;
			lock (obj)
			{
				Task.s_currentActiveTasks.Remove(taskId);
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void MarkAborted(ThreadAbortException e)
		{
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x00081F34 File Offset: 0x00080134
		[SecurityCritical]
		private void ExecuteWithThreadLocal(ref Task currentTaskSlot)
		{
			Task task = currentTaskSlot;
			try
			{
				currentTaskSlot = this;
				ExecutionContext capturedContext = this.CapturedContext;
				if (capturedContext == null)
				{
					this.Execute();
				}
				else
				{
					ContextCallback contextCallback = Task.s_ecCallback;
					if (contextCallback == null)
					{
						contextCallback = (Task.s_ecCallback = new ContextCallback(Task.ExecutionContextCallback));
					}
					ExecutionContext.Run(capturedContext, contextCallback, this, true);
				}
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceSynchronousWorkCompletion(CausalityTraceLevel.Required, CausalitySynchronousWork.Execution);
				}
				this.Finish(true);
			}
			finally
			{
				currentTaskSlot = task;
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00081FAC File Offset: 0x000801AC
		// Note: this type is marked as 'beforefieldinit'.
		static Task()
		{
		}

		// Token: 0x04001CC9 RID: 7369
		internal static int s_taskIdCounter;

		// Token: 0x04001CCA RID: 7370
		private volatile int m_taskId;

		// Token: 0x04001CCB RID: 7371
		internal Delegate m_action;

		// Token: 0x04001CCC RID: 7372
		internal object m_stateObject;

		// Token: 0x04001CCD RID: 7373
		internal TaskScheduler m_taskScheduler;

		// Token: 0x04001CCE RID: 7374
		internal readonly Task m_parent;

		// Token: 0x04001CCF RID: 7375
		internal volatile int m_stateFlags;

		// Token: 0x04001CD0 RID: 7376
		private const int OptionsMask = 65535;

		// Token: 0x04001CD1 RID: 7377
		internal const int TASK_STATE_STARTED = 65536;

		// Token: 0x04001CD2 RID: 7378
		internal const int TASK_STATE_DELEGATE_INVOKED = 131072;

		// Token: 0x04001CD3 RID: 7379
		internal const int TASK_STATE_DISPOSED = 262144;

		// Token: 0x04001CD4 RID: 7380
		internal const int TASK_STATE_EXCEPTIONOBSERVEDBYPARENT = 524288;

		// Token: 0x04001CD5 RID: 7381
		internal const int TASK_STATE_CANCELLATIONACKNOWLEDGED = 1048576;

		// Token: 0x04001CD6 RID: 7382
		internal const int TASK_STATE_FAULTED = 2097152;

		// Token: 0x04001CD7 RID: 7383
		internal const int TASK_STATE_CANCELED = 4194304;

		// Token: 0x04001CD8 RID: 7384
		internal const int TASK_STATE_WAITING_ON_CHILDREN = 8388608;

		// Token: 0x04001CD9 RID: 7385
		internal const int TASK_STATE_RAN_TO_COMPLETION = 16777216;

		// Token: 0x04001CDA RID: 7386
		internal const int TASK_STATE_WAITINGFORACTIVATION = 33554432;

		// Token: 0x04001CDB RID: 7387
		internal const int TASK_STATE_COMPLETION_RESERVED = 67108864;

		// Token: 0x04001CDC RID: 7388
		internal const int TASK_STATE_THREAD_WAS_ABORTED = 134217728;

		// Token: 0x04001CDD RID: 7389
		internal const int TASK_STATE_WAIT_COMPLETION_NOTIFICATION = 268435456;

		// Token: 0x04001CDE RID: 7390
		private const int TASK_STATE_COMPLETED_MASK = 23068672;

		// Token: 0x04001CDF RID: 7391
		private const int CANCELLATION_REQUESTED = 1;

		// Token: 0x04001CE0 RID: 7392
		private volatile object m_continuationObject;

		// Token: 0x04001CE1 RID: 7393
		private static readonly object s_taskCompletionSentinel = new object();

		// Token: 0x04001CE2 RID: 7394
		internal static bool s_asyncDebuggingEnabled;

		// Token: 0x04001CE3 RID: 7395
		internal volatile Task.ContingentProperties m_contingentProperties;

		// Token: 0x04001CE4 RID: 7396
		private static readonly Action<object> s_taskCancelCallback = new Action<object>(Task.TaskCancelCallback);

		// Token: 0x04001CE5 RID: 7397
		[ThreadStatic]
		internal static Task t_currentTask;

		// Token: 0x04001CE6 RID: 7398
		[ThreadStatic]
		private static StackGuard t_stackGuard;

		// Token: 0x04001CE7 RID: 7399
		private static readonly Func<Task.ContingentProperties> s_createContingentProperties = () => new Task.ContingentProperties();

		// Token: 0x04001CE8 RID: 7400
		[CompilerGenerated]
		private static readonly TaskFactory <Factory>k__BackingField;

		// Token: 0x04001CE9 RID: 7401
		[CompilerGenerated]
		private static readonly Task <CompletedTask>k__BackingField;

		// Token: 0x04001CEA RID: 7402
		private static readonly Predicate<Task> s_IsExceptionObservedByParentPredicate = (Task t) => t.IsExceptionObservedByParent;

		// Token: 0x04001CEB RID: 7403
		private static ContextCallback s_ecCallback;

		// Token: 0x04001CEC RID: 7404
		private static readonly Predicate<object> s_IsTaskContinuationNullPredicate = (object tc) => tc == null;

		// Token: 0x04001CED RID: 7405
		private static readonly Dictionary<int, Task> s_currentActiveTasks = new Dictionary<int, Task>();

		// Token: 0x04001CEE RID: 7406
		private static readonly object s_activeTasksLock = new object();

		// Token: 0x02000358 RID: 856
		internal class ContingentProperties
		{
			// Token: 0x06002440 RID: 9280 RVA: 0x0008204C File Offset: 0x0008024C
			internal void SetCompleted()
			{
				ManualResetEventSlim completionEvent = this.m_completionEvent;
				if (completionEvent != null)
				{
					completionEvent.Set();
				}
			}

			// Token: 0x06002441 RID: 9281 RVA: 0x0008206C File Offset: 0x0008026C
			internal void UnregisterCancellationCallback()
			{
				if (this.m_cancellationRegistration != null)
				{
					try
					{
						((CancellationTokenRegistration)this.m_cancellationRegistration).Dispose();
					}
					catch (ObjectDisposedException)
					{
					}
					this.m_cancellationRegistration = null;
				}
			}

			// Token: 0x06002442 RID: 9282 RVA: 0x000820B0 File Offset: 0x000802B0
			public ContingentProperties()
			{
			}

			// Token: 0x04001CEF RID: 7407
			internal ExecutionContext m_capturedContext;

			// Token: 0x04001CF0 RID: 7408
			internal volatile ManualResetEventSlim m_completionEvent;

			// Token: 0x04001CF1 RID: 7409
			internal volatile TaskExceptionHolder m_exceptionsHolder;

			// Token: 0x04001CF2 RID: 7410
			internal CancellationToken m_cancellationToken;

			// Token: 0x04001CF3 RID: 7411
			internal object m_cancellationRegistration;

			// Token: 0x04001CF4 RID: 7412
			internal volatile int m_internalCancellationRequested;

			// Token: 0x04001CF5 RID: 7413
			internal volatile int m_completionCountdown = 1;

			// Token: 0x04001CF6 RID: 7414
			internal volatile LowLevelListWithIList<Task> m_exceptionalChildren;
		}

		// Token: 0x02000359 RID: 857
		private sealed class SetOnInvokeMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x06002443 RID: 9283 RVA: 0x000820C1 File Offset: 0x000802C1
			internal SetOnInvokeMres() : base(false, 0)
			{
			}

			// Token: 0x06002444 RID: 9284 RVA: 0x000820CB File Offset: 0x000802CB
			public void Invoke(Task completingTask)
			{
				base.Set();
			}

			// Token: 0x1700045E RID: 1118
			// (get) Token: 0x06002445 RID: 9285 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return false;
				}
			}
		}

		// Token: 0x0200035A RID: 858
		private sealed class SetOnCountdownMres : ManualResetEventSlim, ITaskCompletionAction
		{
			// Token: 0x06002446 RID: 9286 RVA: 0x000820D3 File Offset: 0x000802D3
			internal SetOnCountdownMres(int count)
			{
				this._count = count;
			}

			// Token: 0x06002447 RID: 9287 RVA: 0x000820E2 File Offset: 0x000802E2
			public void Invoke(Task completingTask)
			{
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					base.Set();
				}
			}

			// Token: 0x1700045F RID: 1119
			// (get) Token: 0x06002448 RID: 9288 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04001CF7 RID: 7415
			private int _count;
		}

		// Token: 0x0200035B RID: 859
		private sealed class DelayPromise : Task<VoidTaskResult>
		{
			// Token: 0x06002449 RID: 9289 RVA: 0x000820F7 File Offset: 0x000802F7
			internal DelayPromise(CancellationToken token)
			{
				this.Token = token;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "Task.Delay", 0UL);
				}
				DebuggerSupport.AddToActiveTasks(this);
			}

			// Token: 0x0600244A RID: 9290 RVA: 0x00082124 File Offset: 0x00080324
			internal void Complete()
			{
				bool flag;
				if (this.Token.IsCancellationRequested)
				{
					flag = base.TrySetCanceled(this.Token);
				}
				else
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(this);
					flag = base.TrySetResult(default(VoidTaskResult));
				}
				if (flag)
				{
					if (this.Timer != null)
					{
						this.Timer.Dispose();
					}
					this.Registration.Dispose();
				}
			}

			// Token: 0x04001CF8 RID: 7416
			internal readonly CancellationToken Token;

			// Token: 0x04001CF9 RID: 7417
			internal CancellationTokenRegistration Registration;

			// Token: 0x04001CFA RID: 7418
			internal Timer Timer;
		}

		// Token: 0x0200035C RID: 860
		private sealed class WhenAllPromise : Task<VoidTaskResult>, ITaskCompletionAction
		{
			// Token: 0x0600244B RID: 9291 RVA: 0x00082194 File Offset: 0x00080394
			internal WhenAllPromise(Task[] tasks)
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "Task.WhenAll", 0UL);
				}
				DebuggerSupport.AddToActiveTasks(this);
				this.m_tasks = tasks;
				this.m_count = tasks.Length;
				foreach (Task task in tasks)
				{
					if (task.IsCompleted)
					{
						this.Invoke(task);
					}
					else
					{
						task.AddCompletionAction(this);
					}
				}
			}

			// Token: 0x0600244C RID: 9292 RVA: 0x00082200 File Offset: 0x00080400
			public void Invoke(Task ignored)
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, this, CausalityRelation.Join);
				}
				if (Interlocked.Decrement(ref this.m_count) == 0)
				{
					LowLevelListWithIList<ExceptionDispatchInfo> lowLevelListWithIList = null;
					Task task = null;
					for (int i = 0; i < this.m_tasks.Length; i++)
					{
						Task task2 = this.m_tasks[i];
						if (task2.IsFaulted)
						{
							if (lowLevelListWithIList == null)
							{
								lowLevelListWithIList = new LowLevelListWithIList<ExceptionDispatchInfo>();
							}
							lowLevelListWithIList.AddRange(task2.GetExceptionDispatchInfos());
						}
						else if (task2.IsCanceled && task == null)
						{
							task = task2;
						}
						if (task2.IsWaitNotificationEnabled)
						{
							base.SetNotificationForWaitCompletion(true);
						}
						else
						{
							this.m_tasks[i] = null;
						}
					}
					if (lowLevelListWithIList != null)
					{
						base.TrySetException(lowLevelListWithIList);
						return;
					}
					if (task != null)
					{
						base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
						return;
					}
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(this);
					base.TrySetResult(default(VoidTaskResult));
				}
			}

			// Token: 0x17000460 RID: 1120
			// (get) Token: 0x0600244D RID: 9293 RVA: 0x000822DB File Offset: 0x000804DB
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this.m_tasks);
				}
			}

			// Token: 0x17000461 RID: 1121
			// (get) Token: 0x0600244E RID: 9294 RVA: 0x000040F7 File Offset: 0x000022F7
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04001CFB RID: 7419
			private readonly Task[] m_tasks;

			// Token: 0x04001CFC RID: 7420
			private int m_count;
		}

		// Token: 0x0200035D RID: 861
		private sealed class WhenAllPromise<T> : Task<T[]>, ITaskCompletionAction
		{
			// Token: 0x0600244F RID: 9295 RVA: 0x000822F4 File Offset: 0x000804F4
			internal WhenAllPromise(Task<T>[] tasks)
			{
				this.m_tasks = tasks;
				this.m_count = tasks.Length;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "Task.WhenAll", 0UL);
				}
				DebuggerSupport.AddToActiveTasks(this);
				foreach (Task<T> task in tasks)
				{
					if (task.IsCompleted)
					{
						this.Invoke(task);
					}
					else
					{
						task.AddCompletionAction(this);
					}
				}
			}

			// Token: 0x06002450 RID: 9296 RVA: 0x00082360 File Offset: 0x00080560
			public void Invoke(Task ignored)
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, this, CausalityRelation.Join);
				}
				if (Interlocked.Decrement(ref this.m_count) == 0)
				{
					T[] array = new T[this.m_tasks.Length];
					LowLevelListWithIList<ExceptionDispatchInfo> lowLevelListWithIList = null;
					Task task = null;
					for (int i = 0; i < this.m_tasks.Length; i++)
					{
						Task<T> task2 = this.m_tasks[i];
						if (task2.IsFaulted)
						{
							if (lowLevelListWithIList == null)
							{
								lowLevelListWithIList = new LowLevelListWithIList<ExceptionDispatchInfo>();
							}
							lowLevelListWithIList.AddRange(task2.GetExceptionDispatchInfos());
						}
						else if (task2.IsCanceled)
						{
							if (task == null)
							{
								task = task2;
							}
						}
						else
						{
							array[i] = task2.GetResultCore(false);
						}
						if (task2.IsWaitNotificationEnabled)
						{
							base.SetNotificationForWaitCompletion(true);
						}
						else
						{
							this.m_tasks[i] = null;
						}
					}
					if (lowLevelListWithIList != null)
					{
						base.TrySetException(lowLevelListWithIList);
						return;
					}
					if (task != null)
					{
						base.TrySetCanceled(task.CancellationToken, task.GetCancellationExceptionDispatchInfo());
						return;
					}
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(this);
					base.TrySetResult(array);
				}
			}

			// Token: 0x17000462 RID: 1122
			// (get) Token: 0x06002451 RID: 9297 RVA: 0x00082458 File Offset: 0x00080658
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					if (base.ShouldNotifyDebuggerOfWaitCompletion)
					{
						Task[] tasks = this.m_tasks;
						return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(tasks);
					}
					return false;
				}
			}

			// Token: 0x17000463 RID: 1123
			// (get) Token: 0x06002452 RID: 9298 RVA: 0x000040F7 File Offset: 0x000022F7
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04001CFD RID: 7421
			private readonly Task<T>[] m_tasks;

			// Token: 0x04001CFE RID: 7422
			private int m_count;
		}

		// Token: 0x0200035E RID: 862
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002453 RID: 9299 RVA: 0x0008247C File Offset: 0x0008067C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002454 RID: 9300 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06002455 RID: 9301 RVA: 0x00082488 File Offset: 0x00080688
			internal void <Delay>b__247_0(object state)
			{
				((Task.DelayPromise)state).Complete();
			}

			// Token: 0x06002456 RID: 9302 RVA: 0x00082488 File Offset: 0x00080688
			internal void <Delay>b__247_1(object state)
			{
				((Task.DelayPromise)state).Complete();
			}

			// Token: 0x06002457 RID: 9303 RVA: 0x00082495 File Offset: 0x00080695
			internal Task.ContingentProperties <.cctor>b__271_0()
			{
				return new Task.ContingentProperties();
			}

			// Token: 0x06002458 RID: 9304 RVA: 0x0008249C File Offset: 0x0008069C
			internal bool <.cctor>b__271_1(Task t)
			{
				return t.IsExceptionObservedByParent;
			}

			// Token: 0x06002459 RID: 9305 RVA: 0x000824A4 File Offset: 0x000806A4
			internal bool <.cctor>b__271_2(object tc)
			{
				return tc == null;
			}

			// Token: 0x04001CFF RID: 7423
			public static readonly Task.<>c <>9 = new Task.<>c();

			// Token: 0x04001D00 RID: 7424
			public static Action<object> <>9__247_0;

			// Token: 0x04001D01 RID: 7425
			public static TimerCallback <>9__247_1;
		}
	}
}
