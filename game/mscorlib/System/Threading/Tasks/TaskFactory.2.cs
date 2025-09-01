using System;
using System.Collections.Generic;
using Internal.Runtime.Augments;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for creating and scheduling <see cref="T:System.Threading.Tasks.Task" /> objects.</summary>
	// Token: 0x02000375 RID: 885
	public class TaskFactory
	{
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x00083288 File Offset: 0x00081488
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

		// Token: 0x060024AC RID: 9388 RVA: 0x0008329E File Offset: 0x0008149E
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

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the default configuration.</summary>
		// Token: 0x060024AD RID: 9389 RVA: 0x000832CC File Offset: 0x000814CC
		public TaskFactory() : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another CancellationToken is explicitly specified while calling the factory methods.</param>
		// Token: 0x060024AE RID: 9390 RVA: 0x000832EB File Offset: 0x000814EB
		public TaskFactory(CancellationToken cancellationToken) : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> to use to schedule any tasks created with this TaskFactory. A null value indicates that the current TaskScheduler should be used.</param>
		// Token: 0x060024AF RID: 9391 RVA: 0x000832F8 File Offset: 0x000814F8
		public TaskFactory(TaskScheduler scheduler) : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="creationOptions">The default <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> to use when creating tasks with this TaskFactory.</param>
		/// <param name="continuationOptions">The default <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> to use when creating continuation tasks with this TaskFactory.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />.  
		///  -or-  
		///  The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		// Token: 0x060024B0 RID: 9392 RVA: 0x00083318 File Offset: 0x00081518
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions) : this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		/// <summary>Initializes a <see cref="T:System.Threading.Tasks.TaskFactory" /> instance with the specified configuration.</summary>
		/// <param name="cancellationToken">The default <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to tasks created by this <see cref="T:System.Threading.Tasks.TaskFactory" /> unless another CancellationToken is explicitly specified while calling the factory methods.</param>
		/// <param name="creationOptions">The default <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> to use when creating tasks with this TaskFactory.</param>
		/// <param name="continuationOptions">The default <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> to use when creating continuation tasks with this TaskFactory.</param>
		/// <param name="scheduler">The default <see cref="T:System.Threading.Tasks.TaskScheduler" /> to use to schedule any Tasks created with this TaskFactory. A null value indicates that TaskScheduler.Current should be used.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="creationOptions" /> argument specifies an invalid <see cref="T:System.Threading.Tasks.TaskCreationOptions" /> value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" />.  
		///  -or-  
		///  The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		// Token: 0x060024B1 RID: 9393 RVA: 0x00083337 File Offset: 0x00081537
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00083368 File Offset: 0x00081568
		internal static void CheckCreationOptions(TaskCreationOptions creationOptions)
		{
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler | TaskCreationOptions.RunContinuationsAsynchronously)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		/// <summary>Gets the default cancellation token for this task factory.</summary>
		/// <returns>The default task cancellation token for this task factory.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x0008337B File Offset: 0x0008157B
		public CancellationToken CancellationToken
		{
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		/// <summary>Gets the default task scheduler for this task factory.</summary>
		/// <returns>The default task scheduler for this task factory.</returns>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x00083383 File Offset: 0x00081583
		public TaskScheduler Scheduler
		{
			get
			{
				return this.m_defaultScheduler;
			}
		}

		/// <summary>Gets the default task creation options for this task factory.</summary>
		/// <returns>The default task creation options for this task factory.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x0008338B File Offset: 0x0008158B
		public TaskCreationOptions CreationOptions
		{
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		/// <summary>Gets the default task continuation options for this task factory.</summary>
		/// <returns>The default task continuation options for this task factory.</returns>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x00083393 File Offset: 0x00081593
		public TaskContinuationOptions ContinuationOptions
		{
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		/// <summary>Creates and starts a task.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <returns>The started task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is null.</exception>
		// Token: 0x060024B7 RID: 9399 RVA: 0x0008339C File Offset: 0x0008159C
		public Task StartNew(Action action)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		// Token: 0x060024B8 RID: 9400 RVA: 0x000833CC File Offset: 0x000815CC
		public Task StartNew(Action action, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x060024B9 RID: 9401 RVA: 0x000833F8 File Offset: 0x000815F8
		public Task StartNew(Action action, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, null, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024BA RID: 9402 RVA: 0x00083422 File Offset: 0x00081622
		public Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x00083436 File Offset: 0x00081636
		internal Task StartNew(Action action, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler)
		{
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, null, cancellationToken, scheduler, creationOptions, internalOptions);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> argument is <see langword="null" />.</exception>
		// Token: 0x060024BC RID: 9404 RVA: 0x0008344C File Offset: 0x0008164C
		public Task StartNew(Action<object> action, object state)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		// Token: 0x060024BD RID: 9405 RVA: 0x0008347C File Offset: 0x0008167C
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, cancellationToken, this.GetDefaultScheduler(internalCurrent), this.m_defaultCreationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x060024BE RID: 9406 RVA: 0x000834A8 File Offset: 0x000816A8
		public Task StartNew(Action<object> action, object state, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task.InternalStartNew(internalCurrent, action, state, this.m_defaultCancellationToken, this.GetDefaultScheduler(internalCurrent), creationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task" />.</summary>
		/// <param name="action">The action delegate to execute asynchronously.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="action" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="action" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024BF RID: 9407 RVA: 0x000834D2 File Offset: 0x000816D2
		public Task StartNew(Action<object> action, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task.InternalStartNew(Task.InternalCurrentIfAttached(creationOptions), action, state, cancellationToken, scheduler, creationOptions, InternalTaskOptions.None);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="function" /> argument is <see langword="null" />.</exception>
		// Token: 0x060024C0 RID: 9408 RVA: 0x000834E8 File Offset: 0x000816E8
		public Task<TResult> StartNew<TResult>(Func<TResult> function)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x060024C1 RID: 9409 RVA: 0x00083518 File Offset: 0x00081718
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024C2 RID: 9410 RVA: 0x00083544 File Offset: 0x00081744
		public Task<TResult> StartNew<TResult>(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024C3 RID: 9411 RVA: 0x0008356D File Offset: 0x0008176D
		public Task<TResult> StartNew<TResult>(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x060024C4 RID: 9412 RVA: 0x00083580 File Offset: 0x00081780
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new <see cref="T:System.Threading.Tasks.Task" /></param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		// Token: 0x060024C5 RID: 9413 RVA: 0x000835B0 File Offset: 0x000817B0
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024C6 RID: 9414 RVA: 0x000835DC File Offset: 0x000817DC
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent));
		}

		/// <summary>Creates and starts a <see cref="T:System.Threading.Tasks.Task`1" />.</summary>
		/// <param name="function">A function delegate that returns the future result to be available through the <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="function" /> delegate.</param>
		/// <param name="cancellationToken">The <see cref="P:System.Threading.Tasks.TaskFactory.CancellationToken" /> that will be assigned to the new task.</param>
		/// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The started <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="function" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024C7 RID: 9415 RVA: 0x00083606 File Offset: 0x00081806
		public Task<TResult> StartNew<TResult>(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024C8 RID: 9416 RVA: 0x0008361C File Offset: 0x0008181C
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod)
		{
			return this.FromAsync(asyncResult, endMethod, this.m_defaultCreationOptions, this.DefaultScheduler);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024C9 RID: 9417 RVA: 0x00083632 File Offset: 0x00081832
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions)
		{
			return this.FromAsync(asyncResult, endMethod, creationOptions, this.DefaultScheduler);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that executes an end method action when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The action delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the task that executes the end method.</param>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024CA RID: 9418 RVA: 0x00083643 File Offset: 0x00081843
		public Task FromAsync(IAsyncResult asyncResult, Action<IAsyncResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(asyncResult, null, endMethod, creationOptions, scheduler);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024CB RID: 9419 RVA: 0x00083650 File Offset: 0x00081850
		public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state)
		{
			return this.FromAsync(beginMethod, endMethod, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value.</exception>
		// Token: 0x060024CC RID: 9420 RVA: 0x00083661 File Offset: 0x00081861
		public Task FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl(beginMethod, null, endMethod, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024CD RID: 9421 RVA: 0x0008366E File Offset: 0x0008186E
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state)
		{
			return this.FromAsync<TArg1>(beginMethod, endMethod, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024CE RID: 9422 RVA: 0x00083681 File Offset: 0x00081881
		public Task FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1>(beginMethod, null, endMethod, arg1, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024CF RID: 9423 RVA: 0x00083690 File Offset: 0x00081890
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return this.FromAsync<TArg1, TArg2>(beginMethod, endMethod, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024D0 RID: 9424 RVA: 0x000836A5 File Offset: 0x000818A5
		public Task FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, null, endMethod, arg1, arg2, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024D1 RID: 9425 RVA: 0x000836B6 File Offset: 0x000818B6
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return this.FromAsync<TArg1, TArg2, TArg3>(beginMethod, endMethod, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024D2 RID: 9426 RVA: 0x000836CD File Offset: 0x000818CD
		public Task FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Action<IAsyncResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<VoidTaskResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, null, endMethod, arg1, arg2, arg3, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024D3 RID: 9427 RVA: 0x000836E0 File Offset: 0x000818E0
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024D4 RID: 9428 RVA: 0x000836F6 File Offset: 0x000818F6
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that executes an end method function when a specified <see cref="T:System.IAsyncResult" /> completes.</summary>
		/// <param name="asyncResult">The IAsyncResult whose completion should trigger the processing of the <paramref name="endMethod" />.</param>
		/// <param name="endMethod">The function delegate that processes the completed <paramref name="asyncResult" />.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the task that executes the end method.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="asyncResult" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024D5 RID: 9429 RVA: 0x00083707 File Offset: 0x00081907
		public Task<TResult> FromAsync<TResult>(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024D6 RID: 9430 RVA: 0x00083714 File Offset: 0x00081914
		public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024D7 RID: 9431 RVA: 0x00083725 File Offset: 0x00081925
		public Task<TResult> FromAsync<TResult>(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024D8 RID: 9432 RVA: 0x00083732 File Offset: 0x00081932
		public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024D9 RID: 9433 RVA: 0x00083745 File Offset: 0x00081945
		public Task<TResult> FromAsync<TArg1, TResult>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024DA RID: 9434 RVA: 0x00083754 File Offset: 0x00081954
		public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024DB RID: 9435 RVA: 0x00083769 File Offset: 0x00081969
		public Task<TResult> FromAsync<TArg1, TArg2, TResult>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		// Token: 0x060024DC RID: 9436 RVA: 0x0008377A File Offset: 0x0008197A
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		/// <summary>Creates a <see cref="T:System.Threading.Tasks.Task`1" /> that represents a pair of begin and end methods that conform to the Asynchronous Programming Model pattern.</summary>
		/// <param name="beginMethod">The delegate that begins the asynchronous operation.</param>
		/// <param name="endMethod">The delegate that ends the asynchronous operation.</param>
		/// <param name="arg1">The first argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg2">The second argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="arg3">The third argument passed to the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="state">An object containing data to be used by the <paramref name="beginMethod" /> delegate.</param>
		/// <param name="creationOptions">The TaskCreationOptions value that controls the behavior of the created <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TArg1">The type of the second argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg2">The type of the third argument passed to <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TArg3">The type of the first argument passed to the <paramref name="beginMethod" /> delegate.</typeparam>
		/// <typeparam name="TResult">The type of the result available through the <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The created <see cref="T:System.Threading.Tasks.Task`1" /> that represents the asynchronous operation.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="beginMethod" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="endMethod" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. The exception that is thrown when the <paramref name="creationOptions" /> argument specifies an invalid TaskCreationOptions value. For more information, see the Remarks for <see cref="M:System.Threading.Tasks.TaskFactory.FromAsync(System.Func{System.AsyncCallback,System.Object,System.IAsyncResult},System.Action{System.IAsyncResult},System.Object,System.Threading.Tasks.TaskCreationOptions)" /></exception>
		// Token: 0x060024DD RID: 9437 RVA: 0x00083791 File Offset: 0x00081991
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x000837A4 File Offset: 0x000819A4
		internal static void CheckFromAsyncOptions(TaskCreationOptions creationOptions, bool hasBeginMethod)
		{
			if (hasBeginMethod)
			{
				if ((creationOptions & TaskCreationOptions.LongRunning) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", "It is invalid to specify TaskCreationOptions.LongRunning in calls to FromAsync.");
				}
				if ((creationOptions & TaskCreationOptions.PreferFairness) != TaskCreationOptions.None)
				{
					throw new ArgumentOutOfRangeException("creationOptions", "It is invalid to specify TaskCreationOptions.PreferFairness in calls to FromAsync.");
				}
			}
			if ((creationOptions & ~(TaskCreationOptions.PreferFairness | TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent | TaskCreationOptions.DenyChildAttach | TaskCreationOptions.HideScheduler)) != TaskCreationOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions");
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000837E4 File Offset: 0x000819E4
		internal static Task<Task[]> CommonCWAllLogic(Task[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise);
				}
			}
			return completeOnCountdownPromise;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00083828 File Offset: 0x00081A28
		internal static Task<Task<T>[]> CommonCWAllLogic<T>(Task<T>[] tasksCopy)
		{
			TaskFactory.CompleteOnCountdownPromise<T> completeOnCountdownPromise = new TaskFactory.CompleteOnCountdownPromise<T>(tasksCopy);
			for (int i = 0; i < tasksCopy.Length; i++)
			{
				if (tasksCopy[i].IsCompleted)
				{
					completeOnCountdownPromise.Invoke(tasksCopy[i]);
				}
				else
				{
					tasksCopy[i].AddCompletionAction(completeOnCountdownPromise);
				}
			}
			return completeOnCountdownPromise;
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E1 RID: 9441 RVA: 0x00083869 File Offset: 0x00081A69
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E2 RID: 9442 RVA: 0x00083892 File Offset: 0x00081A92
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E3 RID: 9443 RVA: 0x000838B6 File Offset: 0x00081AB6
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task.</param>
		/// <param name="scheduler">The object that is used to schedule the new continuation task.</param>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E4 RID: 9444 RVA: 0x000838DA File Offset: 0x00081ADA
		public Task ContinueWhenAll(Task[] tasks, Action<Task[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl(tasks, continuationAction, continuationOptions, cancellationToken, scheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E5 RID: 9445 RVA: 0x000838F6 File Offset: 0x00081AF6
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E6 RID: 9446 RVA: 0x0008391F File Offset: 0x00081B1F
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E7 RID: 9447 RVA: 0x00083943 File Offset: 0x00081B43
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationAction">The action delegate to execute when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <param name="scheduler">The object that is used to schedule the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E8 RID: 9448 RVA: 0x00083967 File Offset: 0x00081B67
		public Task ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>[]> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationAction, continuationOptions, cancellationToken, scheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024E9 RID: 9449 RVA: 0x00083983 File Offset: 0x00081B83
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024EA RID: 9450 RVA: 0x000839AC File Offset: 0x00081BAC
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024EB RID: 9451 RVA: 0x000839D0 File Offset: 0x00081BD0
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <param name="scheduler">The object that is used to schedule the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024EC RID: 9452 RVA: 0x000839F4 File Offset: 0x00081BF4
		public Task<TResult> ContinueWhenAll<TResult>(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024ED RID: 9453 RVA: 0x00083A10 File Offset: 0x00081C10
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024EE RID: 9454 RVA: 0x00083A39 File Offset: 0x00081C39
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		// Token: 0x060024EF RID: 9455 RVA: 0x00083A5D File Offset: 0x00081C5D
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation task that starts when a set of specified tasks has completed.</summary>
		/// <param name="tasks">The array of tasks from which to continue.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when all tasks in the <paramref name="tasks" /> array have completed.</param>
		/// <param name="cancellationToken">The cancellation token to assign to the new continuation task.</param>
		/// <param name="continuationOptions">A bitwise combination of the enumeration values that control the behavior of the new continuation task. The NotOn* and OnlyOn* members are not supported.</param>
		/// <param name="scheduler">The object that is used to schedule the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created task.</typeparam>
		/// <returns>The new continuation task.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationFunction" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="scheduler" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array is empty or contains a null value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="continuationOptions" /> argument specifies an invalid value.</exception>
		/// <exception cref="T:System.ObjectDisposedException">An element in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x060024F0 RID: 9456 RVA: 0x00083A81 File Offset: 0x00081C81
		public Task<TResult> ContinueWhenAll<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00083AA0 File Offset: 0x00081CA0
		internal static Task<Task> CommonCWAnyLogic(IList<Task> tasks)
		{
			TaskFactory.CompleteOnInvokePromise completeOnInvokePromise = new TaskFactory.CompleteOnInvokePromise(tasks);
			bool flag = false;
			int count = tasks.Count;
			for (int i = 0; i < count; i++)
			{
				Task task = tasks[i];
				if (task == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
				if (!flag)
				{
					if (completeOnInvokePromise.IsCompleted)
					{
						flag = true;
					}
					else if (task.IsCompleted)
					{
						completeOnInvokePromise.Invoke(task);
						flag = true;
					}
					else
					{
						task.AddCompletionAction(completeOnInvokePromise);
						if (completeOnInvokePromise.IsCompleted)
						{
							task.RemoveContinuation(completeOnInvokePromise);
						}
					}
				}
			}
			return completeOnInvokePromise;
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a <see langword="null" /> value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024F2 RID: 9458 RVA: 0x00083B23 File Offset: 0x00081D23
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">One of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tasks" /> array is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="continuationAction" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tasks" /> array contains a <see langword="null" /> value.  
		///  -or-  
		///  The <paramref name="tasks" /> array is empty .</exception>
		// Token: 0x060024F3 RID: 9459 RVA: 0x00083B4C File Offset: 0x00081D4C
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024F4 RID: 9460 RVA: 0x00083B70 File Offset: 0x00081D70
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024F5 RID: 9461 RVA: 0x00083B94 File Offset: 0x00081D94
		public Task ContinueWhenAny(Task[] tasks, Action<Task> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl(tasks, continuationAction, continuationOptions, cancellationToken, scheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024F6 RID: 9462 RVA: 0x00083BB0 File Offset: 0x00081DB0
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024F7 RID: 9463 RVA: 0x00083BD9 File Offset: 0x00081DD9
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024F8 RID: 9464 RVA: 0x00083BFD File Offset: 0x00081DFD
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024F9 RID: 9465 RVA: 0x00083C21 File Offset: 0x00081E21
		public Task<TResult> ContinueWhenAny<TResult>(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024FA RID: 9466 RVA: 0x00083C3D File Offset: 0x00081E3D
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024FB RID: 9467 RVA: 0x00083C66 File Offset: 0x00081E66
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024FC RID: 9468 RVA: 0x00083C8A File Offset: 0x00081E8A
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task`1" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationFunction">The function delegate to execute asynchronously when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <typeparam name="TResult">The type of the result that is returned by the <paramref name="continuationFunction" /> delegate and associated with the created <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task`1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationFunction" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024FD RID: 9469 RVA: 0x00083CAE File Offset: 0x00081EAE
		public Task<TResult> ContinueWhenAny<TAntecedentResult, TResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, continuationOptions, cancellationToken, scheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024FE RID: 9470 RVA: 0x00083CCA File Offset: 0x00081ECA
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationAction, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.  
		///  -or-  
		///  The provided <see cref="T:System.Threading.CancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x060024FF RID: 9471 RVA: 0x00083CF3 File Offset: 0x00081EF3
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationAction, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The exception that is thrown when one of the elements in the <paramref name="tasks" /> array has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when the <paramref name="continuationOptions" /> argument specifies an invalid TaskContinuationOptions value.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06002500 RID: 9472 RVA: 0x00083D17 File Offset: 0x00081F17
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationAction, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler);
		}

		/// <summary>Creates a continuation <see cref="T:System.Threading.Tasks.Task" /> that will be started upon the completion of any Task in the provided set.</summary>
		/// <param name="tasks">The array of tasks from which to continue when one task completes.</param>
		/// <param name="continuationAction">The action delegate to execute when one task in the <paramref name="tasks" /> array completes.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> that will be assigned to the new continuation task.</param>
		/// <param name="continuationOptions">The <see cref="T:System.Threading.Tasks.TaskContinuationOptions" /> value that controls the behavior of the created continuation <see cref="T:System.Threading.Tasks.Task" />.</param>
		/// <param name="scheduler">The <see cref="T:System.Threading.Tasks.TaskScheduler" /> that is used to schedule the created continuation <see cref="T:System.Threading.Tasks.Task`1" />.</param>
		/// <typeparam name="TAntecedentResult">The type of the result of the antecedent <paramref name="tasks" />.</typeparam>
		/// <returns>The new continuation <see cref="T:System.Threading.Tasks.Task" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The exception that is thrown when the <paramref name="tasks" /> array is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="continuationAction" /> argument is null.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="scheduler" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">The exception that is thrown when the <paramref name="tasks" /> array contains a null value.  
		///  -or-  
		///  The exception that is thrown when the <paramref name="tasks" /> array is empty.</exception>
		// Token: 0x06002501 RID: 9473 RVA: 0x00083D3B File Offset: 0x00081F3B
		public Task ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Action<Task<TAntecedentResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			return TaskFactory<VoidTaskResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationAction, continuationOptions, cancellationToken, scheduler);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x00083D58 File Offset: 0x00081F58
		internal static Task[] CheckMultiContinuationTasksAndCopy(Task[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			Task[] array = new Task[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
			}
			return array;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x00083DBC File Offset: 0x00081FBC
		internal static Task<TResult>[] CheckMultiContinuationTasksAndCopy<TResult>(Task<TResult>[] tasks)
		{
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException("The tasks argument contains no tasks.", "tasks");
			}
			Task<TResult>[] array = new Task<TResult>[tasks.Length];
			for (int i = 0; i < tasks.Length; i++)
			{
				array[i] = tasks[i];
				if (array[i] == null)
				{
					throw new ArgumentException("The tasks argument included a null value.", "tasks");
				}
			}
			return array;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00083E20 File Offset: 0x00082020
		internal static void CheckMultiTaskContinuationOptions(TaskContinuationOptions continuationOptions)
		{
			if ((continuationOptions & (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously)) == (TaskContinuationOptions.LongRunning | TaskContinuationOptions.ExecuteSynchronously))
			{
				throw new ArgumentOutOfRangeException("continuationOptions", "The specified TaskContinuationOptions combined LongRunning and ExecuteSynchronously.  Synchronous continuations should not be long running.");
			}
			if ((continuationOptions & ~(TaskContinuationOptions.PreferFairness | TaskContinuationOptions.LongRunning | TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.DenyChildAttach | TaskContinuationOptions.HideScheduler | TaskContinuationOptions.LazyCancellation | TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions");
			}
			if ((continuationOptions & (TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled)) != TaskContinuationOptions.None)
			{
				throw new ArgumentOutOfRangeException("continuationOptions", "It is invalid to exclude specific continuation kinds for continuations off of multiple tasks.");
			}
		}

		// Token: 0x04001D44 RID: 7492
		private readonly CancellationToken m_defaultCancellationToken;

		// Token: 0x04001D45 RID: 7493
		private readonly TaskScheduler m_defaultScheduler;

		// Token: 0x04001D46 RID: 7494
		private readonly TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001D47 RID: 7495
		private readonly TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x02000376 RID: 886
		private sealed class CompleteOnCountdownPromise : Task<Task[]>, ITaskCompletionAction
		{
			// Token: 0x06002505 RID: 9477 RVA: 0x00083E78 File Offset: 0x00082078
			internal CompleteOnCountdownPromise(Task[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "TaskFactory.ContinueWhenAll", 0UL);
				}
				DebuggerSupport.AddToActiveTasks(this);
			}

			// Token: 0x06002506 RID: 9478 RVA: 0x00083EAC File Offset: 0x000820AC
			public void Invoke(Task completingTask)
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, this, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(this);
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x17000473 RID: 1139
			// (get) Token: 0x06002507 RID: 9479 RVA: 0x000040F7 File Offset: 0x000022F7
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000474 RID: 1140
			// (get) Token: 0x06002508 RID: 9480 RVA: 0x00083F06 File Offset: 0x00082106
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					return base.ShouldNotifyDebuggerOfWaitCompletion && Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(this._tasks);
				}
			}

			// Token: 0x04001D48 RID: 7496
			private readonly Task[] _tasks;

			// Token: 0x04001D49 RID: 7497
			private int _count;
		}

		// Token: 0x02000377 RID: 887
		private sealed class CompleteOnCountdownPromise<T> : Task<Task<T>[]>, ITaskCompletionAction
		{
			// Token: 0x06002509 RID: 9481 RVA: 0x00083F1D File Offset: 0x0008211D
			internal CompleteOnCountdownPromise(Task<T>[] tasksCopy)
			{
				this._tasks = tasksCopy;
				this._count = tasksCopy.Length;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "TaskFactory.ContinueWhenAll<>", 0UL);
				}
				DebuggerSupport.AddToActiveTasks(this);
			}

			// Token: 0x0600250A RID: 9482 RVA: 0x00083F50 File Offset: 0x00082150
			public void Invoke(Task completingTask)
			{
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, this, CausalityRelation.Join);
				}
				if (completingTask.IsWaitNotificationEnabled)
				{
					base.SetNotificationForWaitCompletion(true);
				}
				if (Interlocked.Decrement(ref this._count) == 0)
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(this);
					base.TrySetResult(this._tasks);
				}
			}

			// Token: 0x17000475 RID: 1141
			// (get) Token: 0x0600250B RID: 9483 RVA: 0x000040F7 File Offset: 0x000022F7
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000476 RID: 1142
			// (get) Token: 0x0600250C RID: 9484 RVA: 0x00083FAC File Offset: 0x000821AC
			internal override bool ShouldNotifyDebuggerOfWaitCompletion
			{
				get
				{
					if (base.ShouldNotifyDebuggerOfWaitCompletion)
					{
						Task[] tasks = this._tasks;
						return Task.AnyTaskRequiresNotifyDebuggerOfWaitCompletion(tasks);
					}
					return false;
				}
			}

			// Token: 0x04001D4A RID: 7498
			private readonly Task<T>[] _tasks;

			// Token: 0x04001D4B RID: 7499
			private int _count;
		}

		// Token: 0x02000378 RID: 888
		internal sealed class CompleteOnInvokePromise : Task<Task>, ITaskCompletionAction
		{
			// Token: 0x0600250D RID: 9485 RVA: 0x00083FD0 File Offset: 0x000821D0
			public CompleteOnInvokePromise(IList<Task> tasks)
			{
				this._tasks = tasks;
				if (DebuggerSupport.LoggingOn)
				{
					DebuggerSupport.TraceOperationCreation(CausalityTraceLevel.Required, this, "TaskFactory.ContinueWhenAny", 0UL);
				}
				DebuggerSupport.AddToActiveTasks(this);
			}

			// Token: 0x0600250E RID: 9486 RVA: 0x00083FFC File Offset: 0x000821FC
			public void Invoke(Task completingTask)
			{
				if (base.TrySetResult(completingTask))
				{
					if (DebuggerSupport.LoggingOn)
					{
						DebuggerSupport.TraceOperationRelation(CausalityTraceLevel.Important, this, CausalityRelation.Choice);
						DebuggerSupport.TraceOperationCompletion(CausalityTraceLevel.Required, this, AsyncStatus.Completed);
					}
					DebuggerSupport.RemoveFromActiveTasks(this);
					IList<Task> tasks = this._tasks;
					int count = tasks.Count;
					for (int i = 0; i < count; i++)
					{
						Task task = tasks[i];
						if (task != null && !task.IsCompleted)
						{
							task.RemoveContinuation(this);
						}
					}
					this._tasks = null;
				}
			}

			// Token: 0x17000477 RID: 1143
			// (get) Token: 0x0600250F RID: 9487 RVA: 0x000040F7 File Offset: 0x000022F7
			public bool InvokeMayRunArbitraryCode
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04001D4C RID: 7500
			private IList<Task> _tasks;
		}
	}
}
