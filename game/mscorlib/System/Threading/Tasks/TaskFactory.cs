using System;
using System.Runtime.CompilerServices;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for creating and scheduling <see cref="T:System.Threading.Tasks.Task`1" /> objects.</summary>
	/// <typeparam name="TResult">The return value of the <see cref="T:System.Threading.Tasks.Task`1" /> objects that the methods of this class create.</typeparam>
	// Token: 0x02000343 RID: 835
	public class TaskFactory<TResult>
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x0007D5D5 File Offset: 0x0007B7D5
		private TaskScheduler DefaultScheduler
		{
			get
			{
				if (this.m_defaultScheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x0007D5EB File Offset: 0x0007B7EB
		private TaskScheduler GetDefaultScheduler(Task currTask)
		{
			if (this.m_defaultScheduler != null)
			{
				return this.m_defaultScheduler;
			}
			if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
			{
				return currTask.ExecutingTaskScheduler;
			}
			return TaskScheduler.Default;
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the default configuration.</summary>
		// Token: 0x06002301 RID: 8961 RVA: 0x0007D618 File Offset: 0x0007B818
		public TaskFactory() : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the default configuration.</summary>
		/// <param name="cancellationToken">The default cancellation token that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another cancellation token is explicitly specified when calling the factory methods.</param>
		// Token: 0x06002302 RID: 8962 RVA: 0x0007D637 File Offset: 0x0007B837
		public TaskFactory(CancellationToken cancellationToken) : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the specified configuration.</summary>
		/// <param name="scheduler">The scheduler to use to schedule any tasks created with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />. A null value indicates that the current <see cref="T:System.Threading.Tasks.TaskScheduler" /> should be used.</param>
		// Token: 0x06002303 RID: 8963 RVA: 0x0007D644 File Offset: 0x0007B844
		public TaskFactory(TaskScheduler scheduler) : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the specified configuration.</summary>
		/// <param name="creationOptions">The default options to use when creating tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <param name="continuationOptions">The default options to use when creating continuation tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationOptions" /> or <paramref name="continuationOptions" /> specifies an invalid value.</exception>
		// Token: 0x06002304 RID: 8964 RVA: 0x0007D664 File Offset: 0x0007B864
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions) : this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory`1" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The default cancellation token that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another cancellation token is explicitly specified when calling the factory methods.</param>
		/// <param name="creationOptions">The default options to use when creating tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <param name="continuationOptions">The default options to use when creating continuation tasks with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />.</param>
		/// <param name="scheduler">The default scheduler to use to schedule any tasks created with this <see cref="T:System.Threading.Tasks.TaskFactory`1" />. A null value indicates that <see cref="P:System.Threading.Tasks.TaskScheduler.Current" /> should be used.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="creationOptions" /> or <paramref name="continuationOptions" /> specifies an invalid value.</exception>
		// Token: 0x06002305 RID: 8965 RVA: 0x0007D683 File Offset: 0x0007B883
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		/// <summary>Gets the default cancellation token for this task factory.</summary>
		/// <returns>The default cancellation token for this task factory.</returns>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0007D6B4 File Offset: 0x0007B8B4
		public CancellationToken CancellationToken
		{
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		/// <summary>Gets the task scheduler for this task factory.</summary>
		/// <returns>The task scheduler for this task factory.</returns>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0007D6BC File Offset: 0x0007B8BC
		public TaskScheduler Scheduler
		{
			get
			{
				return this.m_defaultScheduler;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> enumeration value for this task factory.</summary>
		/// <returns>One of the enumeration values that specifies the default creation options for this task factory.</returns>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x0007D6C4 File Offset: 0x0007B8C4
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> enumeration value for this task factory.</summary>
		/// <returns>One of the enumeration values that specifies the default continuation options for this task factory.</returns>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x0007D6CC File Offset: 0x0007B8CC
		public TaskContinuationOptions ContinuationOptions
		{
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600230A RID: 8970 RVA: 0x0007D6D4 File Offset: 0x0007B8D4
		public Task<TResult> StartNew(Func<TResult> function)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600230B RID: 8971 RVA: 0x0007D704 File Offset: 0x0007B904
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x0600230C RID: 8972 RVA: 0x0007D730 File Offset: 0x0007B930
		public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the created task.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x0600230D RID: 8973 RVA: 0x0007D759 File Offset: 0x0007B959
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600230E RID: 8974 RVA: 0x0007D76C File Offset: 0x0007B96C
		public Task<TResult> StartNew(Func<object, TResult> function, object state)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600230F RID: 8975 RVA: 0x0007D79C File Offset: 0x0007B99C
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06002310 RID: 8976 RVA: 0x0007D7C8 File Offset: 0x0007B9C8
		public Task<TResult> StartNew(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the task.</param>
		/// <param name="state">An object that contains data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new task.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the created task.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The cancellation token source that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06002311 RID: 8977 RVA: 0x0007D7F2 File Offset: 0x0007B9F2
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x0007D808 File Offset: 0x0007BA08
		private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
		{
			Exception ex = null;
			OperationCanceledException ex2 = null;
			TResult result = default(TResult);
			try
			{
				if (endFunction != null)
				{
					result = endFunction(iar);
				}
				else
				{
					endAction(iar);
				}
			}
			catch (OperationCanceledException ex2)
			{
			}
			catch (Exception ex)
			{
			}
			finally
			{
				if (ex2 != null)
				{
					promise.TrySetCanceled(ex2.CancellationToken, ex2);
				}
				else if (ex != null)
				{
					promise.TrySetException(ex);
				}
				else
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(promise);
					if (requiresSynchronization)
					{
						promise.TrySetResult(result);
					}
					else
					{
						promise.DangerousSetResult(result);
					}
				}
			}
		}

		/// <summary>Creates a task that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		// Token: 0x06002313 RID: 8979 RVA: 0x0007D8B0 File Offset: 0x0007BAB0
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler);
		}

		/// <summary>Creates a task that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value.</exception>
		// Token: 0x06002314 RID: 8980 RVA: 0x0007D8C6 File Offset: 0x0007BAC6
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler);
		}

		/// <summary>Creates a task that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the task that executes the end method.</param>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06002315 RID: 8981 RVA: 0x0007D8D7 File Offset: 0x0007BAD7
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler);
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x0007D8E4 File Offset: 0x0007BAE4
		internal static Task<TResult> FromAsyncImpl(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, false);
			Task<TResult> promise = new Task<TResult>(null, creationOptions);
			Task t = new Task(new Action<object>(delegate(object <p0>)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, true);
			}), null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null);
			if (asyncResult.IsCompleted)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
					goto IL_EE;
				}
				catch (Exception exceptionObject)
				{
					promise.TrySetException(exceptionObject);
					goto IL_EE;
				}
			}
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, delegate(object <p0>, bool <p1>)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
				}
				catch (Exception exceptionObject2)
				{
					promise.TrySetException(exceptionObject2);
				}
			}, null, -1, true);
			IL_EE:
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		// Token: 0x06002317 RID: 8983 RVA: 0x0007D9F8 File Offset: 0x0007BBF8
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid value.</exception>
		// Token: 0x06002318 RID: 8984 RVA: 0x0007DA09 File Offset: 0x0007BC09
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x0007DA18 File Offset: 0x0007BC18
		internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600231A RID: 8986 RVA: 0x0007DB34 File Offset: 0x0007BD34
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">One of the enumeration values that controls the behavior of the created task.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x0600231B RID: 8987 RVA: 0x0007DB47 File Offset: 0x0007BD47
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x0007DB58 File Offset: 0x0007BD58
		internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endFunction");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		// Token: 0x0600231D RID: 8989 RVA: 0x0007DC78 File Offset: 0x0007BE78
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">An object that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x0600231E RID: 8990 RVA: 0x0007DC8D File Offset: 0x0007BE8D
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x0007DCA0 File Offset: 0x0007BEA0
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		// Token: 0x06002320 RID: 8992 RVA: 0x0007DDC0 File Offset: 0x0007BFC0
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a task that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">An object that controls the behavior of the created task.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created task that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginMethod" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="endMethod" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> parameter specifies an invalid value.</exception>
		// Token: 0x06002321 RID: 8993 RVA: 0x0007DDD7 File Offset: 0x0007BFD7
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x0007DDEC File Offset: 0x0007BFEC
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (DebuggerSupport.LoggingOn)
			{
				DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, promise, "TaskFactory.FromAsync: " + ((beginMethod != null) ? beginMethod.ToString() : null), 0UL);
			}
			DebuggerSupport.AddToActiveTasks(promise);
			try
			{
				IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
				{
					if (!iar.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}
				}, state);
				if (asyncResult.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
				}
			}
			catch
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, promise, AsyncStatus.Error);
				}
				DebuggerSupport.RemoveFromActiveTasks(promise);
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x0007DF10 File Offset: 0x0007C110
		internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
		{
			TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
			IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, fromAsyncTrimPromise);
			if (asyncResult.CompletedSynchronously)
			{
				fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
			}
			return fromAsyncTrimPromise;
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0007DF48 File Offset: 0x0007C148
		private static Task<TResult> CreateCanceledTask(TaskContinuationOptions continuationOptions, CancellationToken ct)
		{
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalTaskOptions);
			return new Task<TResult>(true, default(TResult), creationOptions, ct);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tasks" /> array is <see langword="null" />.  
		/// -or-  
		/// The <paramref name="continuationFunction" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06002325 RID: 8997 RVA: 0x0007DF70 File Offset: 0x0007C170
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  <paramref name="continuationFunction" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06002326 RID: 8998 RVA: 0x0007DF99 File Offset: 0x0007C199
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided Tasks.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06002327 RID: 8999 RVA: 0x0007DFBD File Offset: 0x0007C1BD
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided Tasks.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <param name="scheduler">The scheduler that is used to schedule the created continuation task.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="continuationOptions" /> specifies an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06002328 RID: 9000 RVA: 0x0007DFE1 File Offset: 0x0007C1E1
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06002329 RID: 9001 RVA: 0x0007DFFD File Offset: 0x0007C1FD
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x0600232A RID: 9002 RVA: 0x0007E026 File Offset: 0x0007C226
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x0600232B RID: 9003 RVA: 0x0007E04A File Offset: 0x0007C24A
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of a set of provided tasks.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The NotOn* or OnlyOn* values are not valid.</param>
		/// <param name="scheduler">The scheduler that is used to schedule the created continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x0600232C RID: 9004 RVA: 0x0007E06E File Offset: 0x0007C26E
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0007E08C File Offset: 0x0007C28C
		internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TAntecedentResult>[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return TaskFactory.CommonCWAllLogic<TAntecedentResult>(tasksCopy).ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x0007E0F0 File Offset: 0x0007C2F0
		internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TAntecedentResult>[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return TaskFactory.CommonCWAllLogic<TAntecedentResult>(tasksCopy).ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x0007E154 File Offset: 0x0007C354
		internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return TaskFactory.CommonCWAllLogic(tasksCopy).ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				return ((Func<Task[], TResult>)state)(completedTasks.Result);
			}, continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x0007E1D4 File Offset: 0x0007C3D4
		internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return TaskFactory.CommonCWAllLogic(tasksCopy).ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value or is empty.</exception>
		// Token: 0x06002331 RID: 9009 RVA: 0x0007E251 File Offset: 0x0007C451
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06002332 RID: 9010 RVA: 0x0007E27A File Offset: 0x0007C47A
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The <see langword="NotOn" /> or <see langword="OnlyOn" /> values are not valid.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid enumeration value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06002333 RID: 9011 RVA: 0x0007E29E File Offset: 0x0007C49E
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The <see langword="NotOn" /> or <see langword="OnlyOn" /> values are not valid.</param>
		/// <param name="scheduler">The task scheduler that is used to schedule the created continuation task.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06002334 RID: 9012 RVA: 0x0007E2C2 File Offset: 0x0007C4C2
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06002335 RID: 9013 RVA: 0x0007E2DE File Offset: 0x0007C4DE
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06002336 RID: 9014 RVA: 0x0007E307 File Offset: 0x0007C507
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The <see langword="NotOn" /> or <see langword="OnlyOn" /> values are not valid.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid enumeration value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06002337 RID: 9015 RVA: 0x0007E32B File Offset: 0x0007C52B
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that will be started upon the completion of any task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The cancellation token that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">One of the enumeration values that controls the behavior of the created continuation task. The <see langword="NotOn" /> or <see langword="OnlyOn" /> values are not valid.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06002338 RID: 9016 RVA: 0x0007E34F File Offset: 0x0007C54F
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x0007E36C File Offset: 0x0007C56C
		internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x0007E3F8 File Offset: 0x0007C5F8
		internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return task.ContinueWith<TResult>((Task<Task> completedTask, object state) => ((Func<Task, TResult>)state)(completedTask.Result), continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0007E484 File Offset: 0x0007C684
		internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0007E4F8 File Offset: 0x0007C6F8
		internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions);
		}

		// Token: 0x04001C88 RID: 7304
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04001C89 RID: 7305
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04001C8A RID: 7306
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001C8B RID: 7307
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x02000344 RID: 836
		private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
		{
			// Token: 0x0600233D RID: 9021 RVA: 0x0007E56A File Offset: 0x0007C76A
			internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
			{
				this.m_thisRef = thisRef;
				this.m_endMethod = endMethod;
			}

			// Token: 0x0600233E RID: 9022 RVA: 0x0007E580 File Offset: 0x0007C780
			internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
				if (fromAsyncTrimPromise == null)
				{
					throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult.", "asyncResult");
				}
				TInstance thisRef = fromAsyncTrimPromise.m_thisRef;
				Func<TInstance, IAsyncResult, TResult> endMethod = fromAsyncTrimPromise.m_endMethod;
				fromAsyncTrimPromise.m_thisRef = default(TInstance);
				fromAsyncTrimPromise.m_endMethod = null;
				if (endMethod == null)
				{
					throw new ArgumentException("Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult.", "asyncResult");
				}
				if (!asyncResult.CompletedSynchronously)
				{
					fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, true);
				}
			}

			// Token: 0x0600233F RID: 9023 RVA: 0x0007E600 File Offset: 0x0007C800
			internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
			{
				try
				{
					TResult result = endMethod(thisRef, asyncResult);
					if (requiresSynchronization)
					{
						base.TrySetResult(result);
					}
					else
					{
						base.DangerousSetResult(result);
					}
				}
				catch (OperationCanceledException ex)
				{
					base.TrySetCanceled(ex.CancellationToken, ex);
				}
				catch (Exception exceptionObject)
				{
					base.TrySetException(exceptionObject);
				}
			}

			// Token: 0x06002340 RID: 9024 RVA: 0x0007E668 File Offset: 0x0007C868
			// Note: this type is marked as 'beforefieldinit'.
			static FromAsyncTrimPromise()
			{
			}

			// Token: 0x04001C8C RID: 7308
			internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);

			// Token: 0x04001C8D RID: 7309
			private TInstance m_thisRef;

			// Token: 0x04001C8E RID: 7310
			private Func<TInstance, IAsyncResult, TResult> m_endMethod;
		}

		// Token: 0x02000345 RID: 837
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0
		{
			// Token: 0x06002341 RID: 9025 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x06002342 RID: 9026 RVA: 0x0007E67B File Offset: 0x0007C87B
			internal void <FromAsyncImpl>b__0(object <p0>)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(this.asyncResult, this.endFunction, this.endAction, this.promise, true);
			}

			// Token: 0x06002343 RID: 9027 RVA: 0x0007E69C File Offset: 0x0007C89C
			internal void <FromAsyncImpl>b__1(object <p0>, bool <p1>)
			{
				try
				{
					this.t.InternalRunSynchronously(this.scheduler, false);
				}
				catch (Exception exceptionObject)
				{
					this.promise.TrySetException(exceptionObject);
				}
			}

			// Token: 0x04001C8F RID: 7311
			public IAsyncResult asyncResult;

			// Token: 0x04001C90 RID: 7312
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001C91 RID: 7313
			public Action<IAsyncResult> endAction;

			// Token: 0x04001C92 RID: 7314
			public Task<TResult> promise;

			// Token: 0x04001C93 RID: 7315
			public Task t;

			// Token: 0x04001C94 RID: 7316
			public TaskScheduler scheduler;
		}

		// Token: 0x02000346 RID: 838
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_0
		{
			// Token: 0x06002344 RID: 9028 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass35_0()
			{
			}

			// Token: 0x06002345 RID: 9029 RVA: 0x0007E6E0 File Offset: 0x0007C8E0
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x04001C95 RID: 7317
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001C96 RID: 7318
			public Action<IAsyncResult> endAction;

			// Token: 0x04001C97 RID: 7319
			public Task<TResult> promise;
		}

		// Token: 0x02000347 RID: 839
		[CompilerGenerated]
		private sealed class <>c__DisplayClass38_0<TArg1>
		{
			// Token: 0x06002346 RID: 9030 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass38_0()
			{
			}

			// Token: 0x06002347 RID: 9031 RVA: 0x0007E703 File Offset: 0x0007C903
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x04001C98 RID: 7320
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001C99 RID: 7321
			public Action<IAsyncResult> endAction;

			// Token: 0x04001C9A RID: 7322
			public Task<TResult> promise;
		}

		// Token: 0x02000348 RID: 840
		[CompilerGenerated]
		private sealed class <>c__DisplayClass41_0<TArg1, TArg2>
		{
			// Token: 0x06002348 RID: 9032 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass41_0()
			{
			}

			// Token: 0x06002349 RID: 9033 RVA: 0x0007E726 File Offset: 0x0007C926
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x04001C9B RID: 7323
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001C9C RID: 7324
			public Action<IAsyncResult> endAction;

			// Token: 0x04001C9D RID: 7325
			public Task<TResult> promise;
		}

		// Token: 0x02000349 RID: 841
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_0<TArg1, TArg2, TArg3>
		{
			// Token: 0x0600234A RID: 9034 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass44_0()
			{
			}

			// Token: 0x0600234B RID: 9035 RVA: 0x0007E749 File Offset: 0x0007C949
			internal void <FromAsyncImpl>b__0(IAsyncResult iar)
			{
				if (!iar.CompletedSynchronously)
				{
					TaskFactory<TResult>.FromAsyncCoreLogic(iar, this.endFunction, this.endAction, this.promise, true);
				}
			}

			// Token: 0x04001C9E RID: 7326
			public Func<IAsyncResult, TResult> endFunction;

			// Token: 0x04001C9F RID: 7327
			public Action<IAsyncResult> endAction;

			// Token: 0x04001CA0 RID: 7328
			public Task<TResult> promise;
		}

		// Token: 0x0200034A RID: 842
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600234C RID: 9036 RVA: 0x0007E76C File Offset: 0x0007C96C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600234D RID: 9037 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x0600234E RID: 9038 RVA: 0x0007E778 File Offset: 0x0007C978
			internal TResult <ContinueWhenAllImpl>b__58_0(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				return ((Func<Task[], TResult>)state)(completedTasks.Result);
			}

			// Token: 0x0600234F RID: 9039 RVA: 0x0007E794 File Offset: 0x0007C994
			internal TResult <ContinueWhenAllImpl>b__59_0(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}

			// Token: 0x06002350 RID: 9040 RVA: 0x0007E7C4 File Offset: 0x0007C9C4
			internal TResult <ContinueWhenAnyImpl>b__68_0(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}

			// Token: 0x06002351 RID: 9041 RVA: 0x0007E7EB File Offset: 0x0007C9EB
			internal TResult <ContinueWhenAnyImpl>b__69_0(Task<Task> completedTask, object state)
			{
				return ((Func<Task, TResult>)state)(completedTask.Result);
			}

			// Token: 0x04001CA1 RID: 7329
			public static readonly TaskFactory<TResult>.<>c <>9 = new TaskFactory<TResult>.<>c();

			// Token: 0x04001CA2 RID: 7330
			public static Func<Task<Task[]>, object, TResult> <>9__58_0;

			// Token: 0x04001CA3 RID: 7331
			public static Func<Task<Task[]>, object, TResult> <>9__59_0;

			// Token: 0x04001CA4 RID: 7332
			public static Func<Task<Task>, object, TResult> <>9__68_0;

			// Token: 0x04001CA5 RID: 7333
			public static Func<Task<Task>, object, TResult> <>9__69_0;
		}
	}
}
