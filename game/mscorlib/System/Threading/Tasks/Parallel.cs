using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks
{
	/// <summary>Provides support for parallel loops and regions.</summary>
	// Token: 0x0200031D RID: 797
	public static class Parallel
	{
		/// <summary>Executes each of the provided actions, possibly in parallel.</summary>
		/// <param name="actions">An array of <see cref="T:System.Action" /> to execute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="actions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that is thrown when any action in the <paramref name="actions" /> array throws an exception.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="actions" /> array contains a <see langword="null" /> element.</exception>
		// Token: 0x060021E6 RID: 8678 RVA: 0x00079414 File Offset: 0x00077614
		public static void Invoke(params Action[] actions)
		{
			Parallel.Invoke(Parallel.s_defaultParallelOptions, actions);
		}

		/// <summary>Executes each of the provided actions, possibly in parallel, unless the operation is cancelled by the user.</summary>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="actions">An array of actions to execute.</param>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> is set.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="actions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that is thrown when any action in the <paramref name="actions" /> array throws an exception.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="actions" /> array contains a <see langword="null" /> element.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x060021E7 RID: 8679 RVA: 0x00079424 File Offset: 0x00077624
		public static void Invoke(ParallelOptions parallelOptions, params Action[] actions)
		{
			if (actions == null)
			{
				throw new ArgumentNullException("actions");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			Action[] actionsCopy = new Action[actions.Length];
			for (int i = 0; i < actionsCopy.Length; i++)
			{
				actionsCopy[i] = actions[i];
				if (actionsCopy[i] == null)
				{
					throw new ArgumentException("One of the actions was null.");
				}
			}
			int forkJoinContextID = 0;
			if (ParallelEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				ParallelEtwProvider.Log.ParallelInvokeBegin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, ParallelEtwProvider.ForkJoinOperationType.ParallelInvoke, actionsCopy.Length);
			}
			if (actionsCopy.Length < 1)
			{
				return;
			}
			try
			{
				if (actionsCopy.Length > 10 || (parallelOptions.MaxDegreeOfParallelism != -1 && parallelOptions.MaxDegreeOfParallelism < actionsCopy.Length))
				{
					ConcurrentQueue<Exception> exceptionQ = null;
					int actionIndex = 0;
					try
					{
						TaskReplicator.Run<object>(delegate(ref object state, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
						{
							replicationDelegateYieldedBeforeCompletion = false;
							for (int k = Interlocked.Increment(ref actionIndex); k <= actionsCopy.Length; k = Interlocked.Increment(ref actionIndex))
							{
								try
								{
									actionsCopy[k - 1]();
								}
								catch (Exception item2)
								{
									LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
									exceptionQ.Enqueue(item2);
								}
								parallelOptions.CancellationToken.ThrowIfCancellationRequested();
							}
						}, parallelOptions, false);
					}
					catch (Exception ex)
					{
						LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref exceptionQ, () => new ConcurrentQueue<Exception>());
						if (ex is ObjectDisposedException)
						{
							throw;
						}
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							using (IEnumerator<Exception> enumerator = ex2.InnerExceptions.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Exception item = enumerator.Current;
									exceptionQ.Enqueue(item);
								}
								goto IL_1C3;
							}
						}
						exceptionQ.Enqueue(ex);
						IL_1C3:;
					}
					if (exceptionQ != null && exceptionQ.Count > 0)
					{
						Parallel.ThrowSingleCancellationExceptionOrOtherException(exceptionQ, parallelOptions.CancellationToken, new AggregateException(exceptionQ));
					}
				}
				else
				{
					Task[] array = new Task[actionsCopy.Length];
					parallelOptions.CancellationToken.ThrowIfCancellationRequested();
					for (int j = 1; j < array.Length; j++)
					{
						array[j] = Task.Factory.StartNew(actionsCopy[j], parallelOptions.CancellationToken, TaskCreationOptions.None, parallelOptions.EffectiveTaskScheduler);
					}
					array[0] = new Task(actionsCopy[0], parallelOptions.CancellationToken, TaskCreationOptions.None);
					array[0].RunSynchronously(parallelOptions.EffectiveTaskScheduler);
					try
					{
						Task.WaitAll(array);
					}
					catch (AggregateException ex3)
					{
						Parallel.ThrowSingleCancellationExceptionOrOtherException(ex3.InnerExceptions, parallelOptions.CancellationToken, ex3);
					}
				}
			}
			finally
			{
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelInvokeEnd(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
				}
			}
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021E8 RID: 8680 RVA: 0x0007978C File Offset: 0x0007798C
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021E9 RID: 8681 RVA: 0x000797AD File Offset: 0x000779AD
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel and loop options can be configured.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x060021EA RID: 8682 RVA: 0x000797CE File Offset: 0x000779CE
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel and loop options can be configured.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x060021EB RID: 8683 RVA: 0x000797F9 File Offset: 0x000779F9
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021EC RID: 8684 RVA: 0x00079824 File Offset: 0x00077A24
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with 64-bit indexes in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A <see cref="T:System.Threading.Tasks.ParallelLoopResult" /> structure that contains information on what portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021ED RID: 8685 RVA: 0x00079845 File Offset: 0x00077A45
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x060021EE RID: 8686 RVA: 0x00079866 File Offset: 0x00077A66
		public static ParallelLoopResult For(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic)  loop with 64-bit indexes in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x060021EF RID: 8687 RVA: 0x00079891 File Offset: 0x00077A91
		public static ParallelLoopResult For(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long, ParallelLoopState> body)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<object>(fromInclusive, toExclusive, parallelOptions, null, body, null, null, null);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with thread-local data in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021F0 RID: 8688 RVA: 0x000798BC File Offset: 0x00077ABC
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic)  loop with 64-bit indexes and thread-local data in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021F1 RID: 8689 RVA: 0x000798FB File Offset: 0x00077AFB
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, Parallel.s_defaultParallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic)  loop with thread-local data in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021F2 RID: 8690 RVA: 0x0007993C File Offset: 0x00077B3C
		public static ParallelLoopResult For<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<int, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop with 64-bit indexes and thread-local data in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="fromInclusive">The start index, inclusive.</param>
		/// <param name="toExclusive">The end index, exclusive.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each thread.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each thread.</param>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021F3 RID: 8691 RVA: 0x00079994 File Offset: 0x00077B94
		public static ParallelLoopResult For<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<long, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForWorker64<TLocal>(fromInclusive, toExclusive, parallelOptions, null, null, body, localInit, localFinally);
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000799EC File Offset: 0x00077BEC
		private static bool CheckTimeoutReached(int timeoutOccursAt)
		{
			int tickCount = Environment.TickCount;
			return tickCount >= timeoutOccursAt && (0 <= timeoutOccursAt || 0 >= tickCount);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00079A10 File Offset: 0x00077C10
		private static int ComputeTimeoutPoint(int timeoutLength)
		{
			return Environment.TickCount + timeoutLength;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00079A1C File Offset: 0x00077C1C
		private static ParallelLoopResult ForWorker<TLocal>(int fromInclusive, int toExclusive, ParallelOptions parallelOptions, Action<int> body, Action<int, ParallelLoopState> bodyWithState, Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult result = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				result._completed = true;
				return result;
			}
			ParallelLoopStateFlags32 sharedPStateFlags = new ParallelLoopStateFlags32();
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			int nNumExpectedWorkers = (parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel;
			RangeManager rangeManager = new RangeManager((long)fromInclusive, (long)toExclusive, 1L, nNumExpectedWorkers);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = (!parallelOptions.CancellationToken.CanBeCanceled) ? default(CancellationTokenRegistration) : parallelOptions.CancellationToken.Register(delegate(object o)
			{
				oce = new OperationCanceledException(parallelOptions.CancellationToken);
				sharedPStateFlags.Cancel();
			}, null, false);
			int forkJoinContextID = 0;
			if (ParallelEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				ParallelEtwProvider.Log.ParallelLoopBegin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, ParallelEtwProvider.ForkJoinOperationType.ParallelFor, (long)fromInclusive, (long)toExclusive);
			}
			try
			{
				try
				{
					TaskReplicator.Run<RangeWorker>(delegate(ref RangeWorker currentWorker, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
					{
						if (!currentWorker.IsInitialized)
						{
							currentWorker = rangeManager.RegisterNewWorker();
						}
						replicationDelegateYieldedBeforeCompletion = false;
						int num2;
						int num3;
						if (!currentWorker.FindNewWork32(out num2, out num3) || sharedPStateFlags.ShouldExitLoop(num2))
						{
							return;
						}
						if (ParallelEtwProvider.Log.IsEnabled())
						{
							ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
						}
						TLocal tlocal = default(TLocal);
						bool flag = false;
						try
						{
							ParallelLoopState32 parallelLoopState = null;
							if (bodyWithState != null)
							{
								parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
							}
							else if (bodyWithLocal != null)
							{
								parallelLoopState = new ParallelLoopState32(sharedPStateFlags);
								if (localInit != null)
								{
									tlocal = localInit();
									flag = true;
								}
							}
							int timeoutOccursAt = Parallel.ComputeTimeoutPoint(timeout);
							for (;;)
							{
								if (body != null)
								{
									for (int i = num2; i < num3; i++)
									{
										if (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop())
										{
											break;
										}
										body(i);
									}
								}
								else if (bodyWithState != null)
								{
									for (int j = num2; j < num3; j++)
									{
										if (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop(j))
										{
											break;
										}
										parallelLoopState.CurrentIteration = j;
										bodyWithState(j, parallelLoopState);
									}
								}
								else
								{
									int num4 = num2;
									while (num4 < num3 && (sharedPStateFlags.LoopStateFlags == 0 || !sharedPStateFlags.ShouldExitLoop(num4)))
									{
										parallelLoopState.CurrentIteration = num4;
										tlocal = bodyWithLocal(num4, parallelLoopState, tlocal);
										num4++;
									}
								}
								if (Parallel.CheckTimeoutReached(timeoutOccursAt))
								{
									break;
								}
								if (!currentWorker.FindNewWork32(out num2, out num3) || (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop(num2)))
								{
									goto IL_1D8;
								}
							}
							replicationDelegateYieldedBeforeCompletion = true;
							IL_1D8:;
						}
						catch (Exception source)
						{
							sharedPStateFlags.SetExceptional();
							ExceptionDispatchInfo.Throw(source);
						}
						finally
						{
							if (localFinally != null && flag)
							{
								localFinally(tlocal);
							}
							if (ParallelEtwProvider.Log.IsEnabled())
							{
								ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
							}
						}
					}, parallelOptions, true);
				}
				finally
				{
					if (parallelOptions.CancellationToken.CanBeCanceled)
					{
						cancellationTokenRegistration.Dispose();
					}
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				Parallel.ThrowSingleCancellationExceptionOrOtherException(ex.InnerExceptions, parallelOptions.CancellationToken, ex);
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				result._completed = (loopStateFlags == 0);
				if ((loopStateFlags & 2) != 0)
				{
					result._lowestBreakIteration = new long?((long)sharedPStateFlags.LowestBreakIteration);
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					int num;
					if (loopStateFlags == 0)
					{
						num = toExclusive - fromInclusive;
					}
					else if ((loopStateFlags & 2) != 0)
					{
						num = sharedPStateFlags.LowestBreakIteration - fromInclusive;
					}
					else
					{
						num = -1;
					}
					ParallelEtwProvider.Log.ParallelLoopEnd(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, (long)num);
				}
			}
			return result;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00079C90 File Offset: 0x00077E90
		private static ParallelLoopResult ForWorker64<TLocal>(long fromInclusive, long toExclusive, ParallelOptions parallelOptions, Action<long> body, Action<long, ParallelLoopState> bodyWithState, Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			ParallelLoopResult result = default(ParallelLoopResult);
			if (toExclusive <= fromInclusive)
			{
				result._completed = true;
				return result;
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			int nNumExpectedWorkers = (parallelOptions.EffectiveMaxConcurrencyLevel == -1) ? PlatformHelper.ProcessorCount : parallelOptions.EffectiveMaxConcurrencyLevel;
			RangeManager rangeManager = new RangeManager(fromInclusive, toExclusive, 1L, nNumExpectedWorkers);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = (!parallelOptions.CancellationToken.CanBeCanceled) ? default(CancellationTokenRegistration) : parallelOptions.CancellationToken.Register(delegate(object o)
			{
				oce = new OperationCanceledException(parallelOptions.CancellationToken);
				sharedPStateFlags.Cancel();
			}, null, false);
			int forkJoinContextID = 0;
			if (ParallelEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				ParallelEtwProvider.Log.ParallelLoopBegin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, ParallelEtwProvider.ForkJoinOperationType.ParallelFor, fromInclusive, toExclusive);
			}
			try
			{
				try
				{
					TaskReplicator.Run<RangeWorker>(delegate(ref RangeWorker currentWorker, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
					{
						if (!currentWorker.IsInitialized)
						{
							currentWorker = rangeManager.RegisterNewWorker();
						}
						replicationDelegateYieldedBeforeCompletion = false;
						long num;
						long num2;
						if (!currentWorker.FindNewWork(out num, out num2) || sharedPStateFlags.ShouldExitLoop(num))
						{
							return;
						}
						if (ParallelEtwProvider.Log.IsEnabled())
						{
							ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
						}
						TLocal tlocal = default(TLocal);
						bool flag = false;
						try
						{
							ParallelLoopState64 parallelLoopState = null;
							if (bodyWithState != null)
							{
								parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
							}
							else if (bodyWithLocal != null)
							{
								parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
								if (localInit != null)
								{
									tlocal = localInit();
									flag = true;
								}
							}
							int timeoutOccursAt = Parallel.ComputeTimeoutPoint(timeout);
							for (;;)
							{
								if (body != null)
								{
									for (long num3 = num; num3 < num2; num3 += 1L)
									{
										if (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop())
										{
											break;
										}
										body(num3);
									}
								}
								else if (bodyWithState != null)
								{
									for (long num4 = num; num4 < num2; num4 += 1L)
									{
										if (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop(num4))
										{
											break;
										}
										parallelLoopState.CurrentIteration = num4;
										bodyWithState(num4, parallelLoopState);
									}
								}
								else
								{
									long num5 = num;
									while (num5 < num2 && (sharedPStateFlags.LoopStateFlags == 0 || !sharedPStateFlags.ShouldExitLoop(num5)))
									{
										parallelLoopState.CurrentIteration = num5;
										tlocal = bodyWithLocal(num5, parallelLoopState, tlocal);
										num5 += 1L;
									}
								}
								if (Parallel.CheckTimeoutReached(timeoutOccursAt))
								{
									break;
								}
								if (!currentWorker.FindNewWork(out num, out num2) || (sharedPStateFlags.LoopStateFlags != 0 && sharedPStateFlags.ShouldExitLoop(num)))
								{
									goto IL_1DB;
								}
							}
							replicationDelegateYieldedBeforeCompletion = true;
							IL_1DB:;
						}
						catch (Exception source)
						{
							sharedPStateFlags.SetExceptional();
							ExceptionDispatchInfo.Throw(source);
						}
						finally
						{
							if (localFinally != null && flag)
							{
								localFinally(tlocal);
							}
							if (ParallelEtwProvider.Log.IsEnabled())
							{
								ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
							}
						}
					}, parallelOptions, true);
				}
				finally
				{
					if (parallelOptions.CancellationToken.CanBeCanceled)
					{
						cancellationTokenRegistration.Dispose();
					}
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				Parallel.ThrowSingleCancellationExceptionOrOtherException(ex.InnerExceptions, parallelOptions.CancellationToken, ex);
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				result._completed = (loopStateFlags == 0);
				if ((loopStateFlags & 2) != 0)
				{
					result._lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					long totalIterations;
					if (loopStateFlags == 0)
					{
						totalIterations = toExclusive - fromInclusive;
					}
					else if ((loopStateFlags & 2) != 0)
					{
						totalIterations = sharedPStateFlags.LowestBreakIteration - fromInclusive;
					}
					else
					{
						totalIterations = -1L;
					}
					ParallelEtwProvider.Log.ParallelLoopEnd(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, totalIterations);
				}
			}
			return result;
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021F8 RID: 8696 RVA: 0x00079F00 File Offset: 0x00078100
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel and loop options can be configured.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x060021F9 RID: 8697 RVA: 0x00079F3C File Offset: 0x0007813C
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021FA RID: 8698 RVA: 0x00079F84 File Offset: 0x00078184
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x060021FB RID: 8699 RVA: 0x00079FC0 File Offset: 0x000781C0
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021FC RID: 8700 RVA: 0x0007A008 File Offset: 0x00078208
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.ForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x060021FD RID: 8701 RVA: 0x0007A044 File Offset: 0x00078244
		public static ParallelLoopResult ForEach<TSource>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021FE RID: 8702 RVA: 0x0007A08C File Offset: 0x0007828C
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x060021FF RID: 8703 RVA: 0x0007A0E4 File Offset: 0x000782E4
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06002200 RID: 8704 RVA: 0x0007A148 File Offset: 0x00078348
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data and 64-bit indexes on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">An enumerable data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the data in the source.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06002201 RID: 8705 RVA: 0x0007A1A0 File Offset: 0x000783A0
		public static ParallelLoopResult ForEach<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.ForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x0007A204 File Offset: 0x00078404
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IEnumerable<TSource> source, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			TSource[] array = source as TSource[];
			if (array != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(array, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			IList<TSource> list = source as IList<TSource>;
			if (list != null)
			{
				return Parallel.ForEachWorker<TSource, TLocal>(list, parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(Partitioner.Create<TSource>(source), parallelOptions, body, bodyWithState, bodyWithStateAndIndex, bodyWithStateAndLocal, bodyWithEverything, localInit, localFinally);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x0007A274 File Offset: 0x00078474
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(TSource[] array, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			int lowerBound = array.GetLowerBound(0);
			int toExclusive = array.GetUpperBound(0) + 1;
			if (body != null)
			{
				return Parallel.ForWorker<object>(lowerBound, toExclusive, parallelOptions, delegate(int i)
				{
					body(array[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(lowerBound, toExclusive, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(array[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(lowerBound, toExclusive, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(array[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(lowerBound, toExclusive, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(array[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(lowerBound, toExclusive, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(array[i], state, (long)i, local), localInit, localFinally);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x0007A370 File Offset: 0x00078570
		private static ParallelLoopResult ForEachWorker<TSource, TLocal>(IList<TSource> list, ParallelOptions parallelOptions, Action<TSource> body, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			if (body != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, delegate(int i)
				{
					body(list[i]);
				}, null, null, null, null);
			}
			if (bodyWithState != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithState(list[i], state);
				}, null, null, null);
			}
			if (bodyWithStateAndIndex != null)
			{
				return Parallel.ForWorker<object>(0, list.Count, parallelOptions, null, delegate(int i, ParallelLoopState state)
				{
					bodyWithStateAndIndex(list[i], state, (long)i);
				}, null, null, null);
			}
			if (bodyWithStateAndLocal != null)
			{
				return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithStateAndLocal(list[i], state, local), localInit, localFinally);
			}
			return Parallel.ForWorker<TLocal>(0, list.Count, parallelOptions, null, null, (int i, ParallelLoopState state, TLocal local) => bodyWithEverything(list[i], state, (long)i, local), localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is  <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns <see langword="false" />.  
		///  -or-  
		///  The exception that is thrown when any methods in the <paramref name="source" /> partitioner return <see langword="null" />.  
		///  -or-  
		///  The <see cref="M:System.Collections.Concurrent.Partitioner`1.GetPartitions(System.Int32)" /> method in the <paramref name="source" /> partitioner does not return the correct number of partitions.</exception>
		// Token: 0x06002205 RID: 8709 RVA: 0x0007A484 File Offset: 0x00078684
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns <see langword="false" />.  
		///  -or-  
		///  A method in the <paramref name="source" /> partitioner returns <see langword="null" />.  
		///  -or-  
		///  The <see cref="M:System.Collections.Concurrent.Partitioner`1.GetPartitions(System.Int32)" /> method in the <paramref name="source" /> partitioner does not return the correct number of partitions.</exception>
		// Token: 0x06002206 RID: 8710 RVA: 0x0007A4C0 File Offset: 0x000786C0
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> orderable partitioner returns <see langword="false" />.  
		///  -or-  
		///  The <see cref="P:System.Collections.Concurrent.OrderablePartitioner`1.KeysNormalized" /> property in the source orderable partitioner returns <see langword="false" />.  
		///  -or-  
		///  Any methods in the source orderable partitioner return <see langword="null" />.</exception>
		// Token: 0x06002207 RID: 8711 RVA: 0x0007A4FC File Offset: 0x000786FC
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, Parallel.s_defaultParallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns <see langword="false" /> or the partitioner returns <see langword="null" /> partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06002208 RID: 8712 RVA: 0x0007A54C File Offset: 0x0007874C
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with thread-local data on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns <see langword="false" /> or the partitioner returns <see langword="null" /> partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		// Token: 0x06002209 RID: 8713 RVA: 0x0007A5A4 File Offset: 0x000787A4
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, Parallel.s_defaultParallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel and loop options can be configured.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns <see langword="false" />.  
		///  -or-  
		///  The exception that is thrown when any methods in the <paramref name="source" /> partitioner return <see langword="null" />.</exception>
		// Token: 0x0600220A RID: 8714 RVA: 0x0007A610 File Offset: 0x00078810
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, body, null, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A  structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> partitioner returns <see langword="false" />.  
		///  -or-  
		///  The exception that is thrown when any methods in the <paramref name="source" /> partitioner return <see langword="null" />.</exception>
		// Token: 0x0600220B RID: 8715 RVA: 0x0007A658 File Offset: 0x00078858
		public static ParallelLoopResult ForEach<TSource>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, body, null, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is  <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /> orderable partitioner returns <see langword="false" />.  
		///  -or-  
		///  The <see cref="P:System.Collections.Concurrent.OrderablePartitioner`1.KeysNormalized" /> property in the <paramref name="source" /> orderable partitioner returns <see langword="false" />.  
		///  -or-  
		///  The exception that is thrown when any methods in the <paramref name="source" /> orderable partitioner return <see langword="null" />.</exception>
		// Token: 0x0600220C RID: 8716 RVA: 0x0007A6A0 File Offset: 0x000788A0
		public static ParallelLoopResult ForEach<TSource>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource, ParallelLoopState, long> body)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.");
			}
			return Parallel.PartitionerForEachWorker<TSource, object>(source, parallelOptions, null, null, body, null, null, null, null);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation  with thread-local data on a <see cref="T:System.Collections.Concurrent.Partitioner" /> in which iterations may run in parallel, loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns <see langword="false" /> or the partitioner returns <see langword="null" /> partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x0600220D RID: 8717 RVA: 0x0007A6F8 File Offset: 0x000788F8
		public static ParallelLoopResult ForEach<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, body, null, localInit, localFinally);
		}

		/// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation with 64-bit indexes and  with thread-local data on a <see cref="T:System.Collections.Concurrent.OrderablePartitioner`1" /> in which iterations may run in parallel , loop options can be configured, and the state of the loop can be monitored and manipulated.</summary>
		/// <param name="source">The orderable partitioner that contains the original data source.</param>
		/// <param name="parallelOptions">An object that configures the behavior of this operation.</param>
		/// <param name="localInit">The function delegate that returns the initial state of the local data for each task.</param>
		/// <param name="body">The delegate that is invoked once per iteration.</param>
		/// <param name="localFinally">The delegate that performs a final action on the local state of each task.</param>
		/// <typeparam name="TSource">The type of the elements in <paramref name="source" />.</typeparam>
		/// <typeparam name="TLocal">The type of the thread-local data.</typeparam>
		/// <returns>A structure that contains information about which portion of the loop completed.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="parallelOptions" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="body" /> argument is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="localInit" /> or <paramref name="localFinally" /> argument is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Collections.Concurrent.Partitioner`1.SupportsDynamicPartitions" /> property in the <paramref name="source" /><see cref="T:System.Collections.Concurrent.Partitioner" /> returns <see langword="false" /> or the partitioner returns <see langword="null" /> partitions.</exception>
		/// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
		/// <exception cref="T:System.OperationCanceledException">The <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> argument is canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.CancellationTokenSource" /> associated with the <see cref="T:System.Threading.CancellationToken" /> in the <paramref name="parallelOptions" /> has been disposed.</exception>
		// Token: 0x0600220E RID: 8718 RVA: 0x0007A75C File Offset: 0x0007895C
		public static ParallelLoopResult ForEach<TSource, TLocal>(OrderablePartitioner<TSource> source, ParallelOptions parallelOptions, Func<TLocal> localInit, Func<TSource, ParallelLoopState, long, TLocal, TLocal> body, Action<TLocal> localFinally)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (body == null)
			{
				throw new ArgumentNullException("body");
			}
			if (localInit == null)
			{
				throw new ArgumentNullException("localInit");
			}
			if (localFinally == null)
			{
				throw new ArgumentNullException("localFinally");
			}
			if (parallelOptions == null)
			{
				throw new ArgumentNullException("parallelOptions");
			}
			if (!source.KeysNormalized)
			{
				throw new InvalidOperationException("This method requires the use of an OrderedPartitioner with the KeysNormalized property set to true.");
			}
			return Parallel.PartitionerForEachWorker<TSource, TLocal>(source, parallelOptions, null, null, null, null, body, localInit, localFinally);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x0007A7D4 File Offset: 0x000789D4
		private static ParallelLoopResult PartitionerForEachWorker<TSource, TLocal>(Partitioner<TSource> source, ParallelOptions parallelOptions, Action<TSource> simpleBody, Action<TSource, ParallelLoopState> bodyWithState, Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex, Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal, Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything, Func<TLocal> localInit, Action<TLocal> localFinally)
		{
			OrderablePartitioner<TSource> orderedSource = source as OrderablePartitioner<TSource>;
			if (!source.SupportsDynamicPartitions)
			{
				throw new InvalidOperationException("The Partitioner used here must support dynamic partitioning.");
			}
			parallelOptions.CancellationToken.ThrowIfCancellationRequested();
			int forkJoinContextID = 0;
			if (ParallelEtwProvider.Log.IsEnabled())
			{
				forkJoinContextID = Interlocked.Increment(ref Parallel.s_forkJoinContextID);
				ParallelEtwProvider.Log.ParallelLoopBegin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, ParallelEtwProvider.ForkJoinOperationType.ParallelForEach, 0L, 0L);
			}
			ParallelLoopStateFlags64 sharedPStateFlags = new ParallelLoopStateFlags64();
			ParallelLoopResult result = default(ParallelLoopResult);
			OperationCanceledException oce = null;
			CancellationTokenRegistration cancellationTokenRegistration = (!parallelOptions.CancellationToken.CanBeCanceled) ? default(CancellationTokenRegistration) : parallelOptions.CancellationToken.Register(delegate(object o)
			{
				oce = new OperationCanceledException(parallelOptions.CancellationToken);
				sharedPStateFlags.Cancel();
			}, null, false);
			IEnumerable<TSource> partitionerSource = null;
			IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource = null;
			if (orderedSource != null)
			{
				orderablePartitionerSource = orderedSource.GetOrderableDynamicPartitions();
				if (orderablePartitionerSource == null)
				{
					throw new InvalidOperationException("The Partitioner used here returned a null partitioner source.");
				}
			}
			else
			{
				partitionerSource = source.GetDynamicPartitions();
				if (partitionerSource == null)
				{
					throw new InvalidOperationException("The Partitioner used here returned a null partitioner source.");
				}
			}
			try
			{
				try
				{
					TaskReplicator.Run<IEnumerator>(delegate(ref IEnumerator partitionState, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
					{
						replicationDelegateYieldedBeforeCompletion = false;
						if (ParallelEtwProvider.Log.IsEnabled())
						{
							ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
						}
						TLocal tlocal = default(TLocal);
						bool flag = false;
						try
						{
							ParallelLoopState64 parallelLoopState = null;
							if (bodyWithState != null || bodyWithStateAndIndex != null)
							{
								parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
							}
							else if (bodyWithStateAndLocal != null || bodyWithEverything != null)
							{
								parallelLoopState = new ParallelLoopState64(sharedPStateFlags);
								if (localInit != null)
								{
									tlocal = localInit();
									flag = true;
								}
							}
							int timeoutOccursAt = Parallel.ComputeTimeoutPoint(timeout);
							if (orderedSource != null)
							{
								IEnumerator<KeyValuePair<long, TSource>> enumerator = partitionState as IEnumerator<KeyValuePair<long, TSource>>;
								if (enumerator == null)
								{
									enumerator = orderablePartitionerSource.GetEnumerator();
									partitionState = enumerator;
								}
								if (enumerator == null)
								{
									throw new InvalidOperationException("The Partitioner source returned a null enumerator.");
								}
								while (enumerator.MoveNext())
								{
									KeyValuePair<long, TSource> keyValuePair = enumerator.Current;
									long key = keyValuePair.Key;
									TSource value = keyValuePair.Value;
									if (parallelLoopState != null)
									{
										parallelLoopState.CurrentIteration = key;
									}
									if (simpleBody != null)
									{
										simpleBody(value);
									}
									else if (bodyWithState != null)
									{
										bodyWithState(value, parallelLoopState);
									}
									else if (bodyWithStateAndIndex != null)
									{
										bodyWithStateAndIndex(value, parallelLoopState, key);
									}
									else if (bodyWithStateAndLocal != null)
									{
										tlocal = bodyWithStateAndLocal(value, parallelLoopState, tlocal);
									}
									else
									{
										tlocal = bodyWithEverything(value, parallelLoopState, key, tlocal);
									}
									if (sharedPStateFlags.ShouldExitLoop(key))
									{
										break;
									}
									if (Parallel.CheckTimeoutReached(timeoutOccursAt))
									{
										replicationDelegateYieldedBeforeCompletion = true;
										break;
									}
								}
							}
							else
							{
								IEnumerator<TSource> enumerator2 = partitionState as IEnumerator<TSource>;
								if (enumerator2 == null)
								{
									enumerator2 = partitionerSource.GetEnumerator();
									partitionState = enumerator2;
								}
								if (enumerator2 == null)
								{
									throw new InvalidOperationException("The Partitioner source returned a null enumerator.");
								}
								if (parallelLoopState != null)
								{
									parallelLoopState.CurrentIteration = 0L;
								}
								while (enumerator2.MoveNext())
								{
									TSource tsource = enumerator2.Current;
									if (simpleBody != null)
									{
										simpleBody(tsource);
									}
									else if (bodyWithState != null)
									{
										bodyWithState(tsource, parallelLoopState);
									}
									else if (bodyWithStateAndLocal != null)
									{
										tlocal = bodyWithStateAndLocal(tsource, parallelLoopState, tlocal);
									}
									if (sharedPStateFlags.LoopStateFlags != 0)
									{
										break;
									}
									if (Parallel.CheckTimeoutReached(timeoutOccursAt))
									{
										replicationDelegateYieldedBeforeCompletion = true;
										break;
									}
								}
							}
						}
						catch (Exception source2)
						{
							sharedPStateFlags.SetExceptional();
							ExceptionDispatchInfo.Throw(source2);
						}
						finally
						{
							if (localFinally != null && flag)
							{
								localFinally(tlocal);
							}
							if (!replicationDelegateYieldedBeforeCompletion)
							{
								IDisposable disposable2 = partitionState as IDisposable;
								if (disposable2 != null)
								{
									disposable2.Dispose();
								}
							}
							if (ParallelEtwProvider.Log.IsEnabled())
							{
								ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID);
							}
						}
					}, parallelOptions, true);
				}
				finally
				{
					if (parallelOptions.CancellationToken.CanBeCanceled)
					{
						cancellationTokenRegistration.Dispose();
					}
				}
				if (oce != null)
				{
					throw oce;
				}
			}
			catch (AggregateException ex)
			{
				Parallel.ThrowSingleCancellationExceptionOrOtherException(ex.InnerExceptions, parallelOptions.CancellationToken, ex);
			}
			finally
			{
				int loopStateFlags = sharedPStateFlags.LoopStateFlags;
				result._completed = (loopStateFlags == 0);
				if ((loopStateFlags & 2) != 0)
				{
					result._lowestBreakIteration = new long?(sharedPStateFlags.LowestBreakIteration);
				}
				IDisposable disposable;
				if (orderablePartitionerSource != null)
				{
					disposable = (orderablePartitionerSource as IDisposable);
				}
				else
				{
					disposable = (partitionerSource as IDisposable);
				}
				if (disposable != null)
				{
					disposable.Dispose();
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelLoopEnd(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), forkJoinContextID, 0L);
				}
			}
			return result;
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x0007AA90 File Offset: 0x00078C90
		private static OperationCanceledException ReduceToSingleCancellationException(ICollection exceptions, CancellationToken cancelToken)
		{
			if (exceptions == null || exceptions.Count == 0)
			{
				return null;
			}
			if (!cancelToken.IsCancellationRequested)
			{
				return null;
			}
			Exception ex = null;
			foreach (object obj in exceptions)
			{
				Exception ex2 = (Exception)obj;
				if (ex == null)
				{
					ex = ex2;
				}
				OperationCanceledException ex3 = ex2 as OperationCanceledException;
				if (ex3 == null || !cancelToken.Equals(ex3.CancellationToken))
				{
					return null;
				}
			}
			return (OperationCanceledException)ex;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x0007AB28 File Offset: 0x00078D28
		private static void ThrowSingleCancellationExceptionOrOtherException(ICollection exceptions, CancellationToken cancelToken, Exception otherException)
		{
			ExceptionDispatchInfo.Throw(Parallel.ReduceToSingleCancellationException(exceptions, cancelToken) ?? otherException);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x0007AB3B File Offset: 0x00078D3B
		// Note: this type is marked as 'beforefieldinit'.
		static Parallel()
		{
		}

		// Token: 0x04001BEC RID: 7148
		internal static int s_forkJoinContextID;

		// Token: 0x04001BED RID: 7149
		internal const int DEFAULT_LOOP_STRIDE = 16;

		// Token: 0x04001BEE RID: 7150
		internal static readonly ParallelOptions s_defaultParallelOptions = new ParallelOptions();

		// Token: 0x0200031E RID: 798
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x06002213 RID: 8723 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x06002214 RID: 8724 RVA: 0x0007AB48 File Offset: 0x00078D48
			internal void <Invoke>b__0(ref object state, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
			{
				replicationDelegateYieldedBeforeCompletion = false;
				for (int i = Interlocked.Increment(ref this.actionIndex); i <= this.actionsCopy.Length; i = Interlocked.Increment(ref this.actionIndex))
				{
					try
					{
						this.actionsCopy[i - 1]();
					}
					catch (Exception item)
					{
						LazyInitializer.EnsureInitialized<ConcurrentQueue<Exception>>(ref this.exceptionQ, new Func<ConcurrentQueue<Exception>>(Parallel.<>c.<>9.<Invoke>b__4_1));
						this.exceptionQ.Enqueue(item);
					}
					this.parallelOptions.CancellationToken.ThrowIfCancellationRequested();
				}
			}

			// Token: 0x04001BEF RID: 7151
			public Action[] actionsCopy;

			// Token: 0x04001BF0 RID: 7152
			public ParallelOptions parallelOptions;

			// Token: 0x04001BF1 RID: 7153
			public int actionIndex;

			// Token: 0x04001BF2 RID: 7154
			public ConcurrentQueue<Exception> exceptionQ;
		}

		// Token: 0x0200031F RID: 799
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002215 RID: 8725 RVA: 0x0007ABEC File Offset: 0x00078DEC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002216 RID: 8726 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06002217 RID: 8727 RVA: 0x0007ABF8 File Offset: 0x00078DF8
			internal ConcurrentQueue<Exception> <Invoke>b__4_1()
			{
				return new ConcurrentQueue<Exception>();
			}

			// Token: 0x06002218 RID: 8728 RVA: 0x0007ABF8 File Offset: 0x00078DF8
			internal ConcurrentQueue<Exception> <Invoke>b__4_2()
			{
				return new ConcurrentQueue<Exception>();
			}

			// Token: 0x04001BF3 RID: 7155
			public static readonly Parallel.<>c <>9 = new Parallel.<>c();

			// Token: 0x04001BF4 RID: 7156
			public static Func<ConcurrentQueue<Exception>> <>9__4_1;

			// Token: 0x04001BF5 RID: 7157
			public static Func<ConcurrentQueue<Exception>> <>9__4_2;
		}

		// Token: 0x02000320 RID: 800
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0<TLocal>
		{
			// Token: 0x06002219 RID: 8729 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x0600221A RID: 8730 RVA: 0x0007ABFF File Offset: 0x00078DFF
			internal void <ForWorker>b__0(object o)
			{
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
				this.sharedPStateFlags.Cancel();
			}

			// Token: 0x0600221B RID: 8731 RVA: 0x0007AC24 File Offset: 0x00078E24
			internal void <ForWorker>b__1(ref RangeWorker currentWorker, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
			{
				if (!currentWorker.IsInitialized)
				{
					currentWorker = this.rangeManager.RegisterNewWorker();
				}
				replicationDelegateYieldedBeforeCompletion = false;
				int num;
				int num2;
				if (!currentWorker.FindNewWork32(out num, out num2) || this.sharedPStateFlags.ShouldExitLoop(num))
				{
					return;
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				try
				{
					ParallelLoopState32 parallelLoopState = null;
					if (this.bodyWithState != null)
					{
						parallelLoopState = new ParallelLoopState32(this.sharedPStateFlags);
					}
					else if (this.bodyWithLocal != null)
					{
						parallelLoopState = new ParallelLoopState32(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag = true;
						}
					}
					int timeoutOccursAt = Parallel.ComputeTimeoutPoint(timeout);
					for (;;)
					{
						if (this.body != null)
						{
							for (int i = num; i < num2; i++)
							{
								if (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop())
								{
									break;
								}
								this.body(i);
							}
						}
						else if (this.bodyWithState != null)
						{
							for (int j = num; j < num2; j++)
							{
								if (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop(j))
								{
									break;
								}
								parallelLoopState.CurrentIteration = j;
								this.bodyWithState(j, parallelLoopState);
							}
						}
						else
						{
							int num3 = num;
							while (num3 < num2 && (this.sharedPStateFlags.LoopStateFlags == 0 || !this.sharedPStateFlags.ShouldExitLoop(num3)))
							{
								parallelLoopState.CurrentIteration = num3;
								tlocal = this.bodyWithLocal(num3, parallelLoopState, tlocal);
								num3++;
							}
						}
						if (Parallel.CheckTimeoutReached(timeoutOccursAt))
						{
							break;
						}
						if (!currentWorker.FindNewWork32(out num, out num2) || (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop(num)))
						{
							goto IL_1D8;
						}
					}
					replicationDelegateYieldedBeforeCompletion = true;
					IL_1D8:;
				}
				catch (Exception source)
				{
					this.sharedPStateFlags.SetExceptional();
					ExceptionDispatchInfo.Throw(source);
				}
				finally
				{
					if (this.localFinally != null && flag)
					{
						this.localFinally(tlocal);
					}
					if (ParallelEtwProvider.Log.IsEnabled())
					{
						ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
					}
				}
			}

			// Token: 0x04001BF6 RID: 7158
			public OperationCanceledException oce;

			// Token: 0x04001BF7 RID: 7159
			public ParallelOptions parallelOptions;

			// Token: 0x04001BF8 RID: 7160
			public ParallelLoopStateFlags32 sharedPStateFlags;

			// Token: 0x04001BF9 RID: 7161
			public RangeManager rangeManager;

			// Token: 0x04001BFA RID: 7162
			public int forkJoinContextID;

			// Token: 0x04001BFB RID: 7163
			public Action<int, ParallelLoopState> bodyWithState;

			// Token: 0x04001BFC RID: 7164
			public Func<int, ParallelLoopState, TLocal, TLocal> bodyWithLocal;

			// Token: 0x04001BFD RID: 7165
			public Func<TLocal> localInit;

			// Token: 0x04001BFE RID: 7166
			public Action<int> body;

			// Token: 0x04001BFF RID: 7167
			public Action<TLocal> localFinally;
		}

		// Token: 0x02000321 RID: 801
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0<TLocal>
		{
			// Token: 0x0600221C RID: 8732 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x0600221D RID: 8733 RVA: 0x0007AEA0 File Offset: 0x000790A0
			internal void <ForWorker64>b__0(object o)
			{
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
				this.sharedPStateFlags.Cancel();
			}

			// Token: 0x0600221E RID: 8734 RVA: 0x0007AEC4 File Offset: 0x000790C4
			internal void <ForWorker64>b__1(ref RangeWorker currentWorker, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
			{
				if (!currentWorker.IsInitialized)
				{
					currentWorker = this.rangeManager.RegisterNewWorker();
				}
				replicationDelegateYieldedBeforeCompletion = false;
				long num;
				long num2;
				if (!currentWorker.FindNewWork(out num, out num2) || this.sharedPStateFlags.ShouldExitLoop(num))
				{
					return;
				}
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (this.bodyWithState != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
					}
					else if (this.bodyWithLocal != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag = true;
						}
					}
					int timeoutOccursAt = Parallel.ComputeTimeoutPoint(timeout);
					for (;;)
					{
						if (this.body != null)
						{
							for (long num3 = num; num3 < num2; num3 += 1L)
							{
								if (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop())
								{
									break;
								}
								this.body(num3);
							}
						}
						else if (this.bodyWithState != null)
						{
							for (long num4 = num; num4 < num2; num4 += 1L)
							{
								if (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop(num4))
								{
									break;
								}
								parallelLoopState.CurrentIteration = num4;
								this.bodyWithState(num4, parallelLoopState);
							}
						}
						else
						{
							long num5 = num;
							while (num5 < num2 && (this.sharedPStateFlags.LoopStateFlags == 0 || !this.sharedPStateFlags.ShouldExitLoop(num5)))
							{
								parallelLoopState.CurrentIteration = num5;
								tlocal = this.bodyWithLocal(num5, parallelLoopState, tlocal);
								num5 += 1L;
							}
						}
						if (Parallel.CheckTimeoutReached(timeoutOccursAt))
						{
							break;
						}
						if (!currentWorker.FindNewWork(out num, out num2) || (this.sharedPStateFlags.LoopStateFlags != 0 && this.sharedPStateFlags.ShouldExitLoop(num)))
						{
							goto IL_1DB;
						}
					}
					replicationDelegateYieldedBeforeCompletion = true;
					IL_1DB:;
				}
				catch (Exception source)
				{
					this.sharedPStateFlags.SetExceptional();
					ExceptionDispatchInfo.Throw(source);
				}
				finally
				{
					if (this.localFinally != null && flag)
					{
						this.localFinally(tlocal);
					}
					if (ParallelEtwProvider.Log.IsEnabled())
					{
						ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
					}
				}
			}

			// Token: 0x04001C00 RID: 7168
			public OperationCanceledException oce;

			// Token: 0x04001C01 RID: 7169
			public ParallelOptions parallelOptions;

			// Token: 0x04001C02 RID: 7170
			public ParallelLoopStateFlags64 sharedPStateFlags;

			// Token: 0x04001C03 RID: 7171
			public RangeManager rangeManager;

			// Token: 0x04001C04 RID: 7172
			public int forkJoinContextID;

			// Token: 0x04001C05 RID: 7173
			public Action<long, ParallelLoopState> bodyWithState;

			// Token: 0x04001C06 RID: 7174
			public Func<long, ParallelLoopState, TLocal, TLocal> bodyWithLocal;

			// Token: 0x04001C07 RID: 7175
			public Func<TLocal> localInit;

			// Token: 0x04001C08 RID: 7176
			public Action<long> body;

			// Token: 0x04001C09 RID: 7177
			public Action<TLocal> localFinally;
		}

		// Token: 0x02000322 RID: 802
		[CompilerGenerated]
		private sealed class <>c__DisplayClass32_0<TSource, TLocal>
		{
			// Token: 0x0600221F RID: 8735 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass32_0()
			{
			}

			// Token: 0x06002220 RID: 8736 RVA: 0x0007B144 File Offset: 0x00079344
			internal void <ForEachWorker>b__0(int i)
			{
				this.body(this.array[i]);
			}

			// Token: 0x06002221 RID: 8737 RVA: 0x0007B15D File Offset: 0x0007935D
			internal void <ForEachWorker>b__1(int i, ParallelLoopState state)
			{
				this.bodyWithState(this.array[i], state);
			}

			// Token: 0x06002222 RID: 8738 RVA: 0x0007B177 File Offset: 0x00079377
			internal void <ForEachWorker>b__2(int i, ParallelLoopState state)
			{
				this.bodyWithStateAndIndex(this.array[i], state, (long)i);
			}

			// Token: 0x06002223 RID: 8739 RVA: 0x0007B193 File Offset: 0x00079393
			internal TLocal <ForEachWorker>b__3(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithStateAndLocal(this.array[i], state, local);
			}

			// Token: 0x06002224 RID: 8740 RVA: 0x0007B1AE File Offset: 0x000793AE
			internal TLocal <ForEachWorker>b__4(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithEverything(this.array[i], state, (long)i, local);
			}

			// Token: 0x04001C0A RID: 7178
			public Action<TSource> body;

			// Token: 0x04001C0B RID: 7179
			public TSource[] array;

			// Token: 0x04001C0C RID: 7180
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x04001C0D RID: 7181
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x04001C0E RID: 7182
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x04001C0F RID: 7183
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;
		}

		// Token: 0x02000323 RID: 803
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0<TSource, TLocal>
		{
			// Token: 0x06002225 RID: 8741 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x06002226 RID: 8742 RVA: 0x0007B1CB File Offset: 0x000793CB
			internal void <ForEachWorker>b__0(int i)
			{
				this.body(this.list[i]);
			}

			// Token: 0x06002227 RID: 8743 RVA: 0x0007B1E4 File Offset: 0x000793E4
			internal void <ForEachWorker>b__1(int i, ParallelLoopState state)
			{
				this.bodyWithState(this.list[i], state);
			}

			// Token: 0x06002228 RID: 8744 RVA: 0x0007B1FE File Offset: 0x000793FE
			internal void <ForEachWorker>b__2(int i, ParallelLoopState state)
			{
				this.bodyWithStateAndIndex(this.list[i], state, (long)i);
			}

			// Token: 0x06002229 RID: 8745 RVA: 0x0007B21A File Offset: 0x0007941A
			internal TLocal <ForEachWorker>b__3(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithStateAndLocal(this.list[i], state, local);
			}

			// Token: 0x0600222A RID: 8746 RVA: 0x0007B235 File Offset: 0x00079435
			internal TLocal <ForEachWorker>b__4(int i, ParallelLoopState state, TLocal local)
			{
				return this.bodyWithEverything(this.list[i], state, (long)i, local);
			}

			// Token: 0x04001C10 RID: 7184
			public Action<TSource> body;

			// Token: 0x04001C11 RID: 7185
			public IList<TSource> list;

			// Token: 0x04001C12 RID: 7186
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x04001C13 RID: 7187
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x04001C14 RID: 7188
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x04001C15 RID: 7189
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;
		}

		// Token: 0x02000324 RID: 804
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_0<TSource, TLocal>
		{
			// Token: 0x0600222B RID: 8747 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass44_0()
			{
			}

			// Token: 0x0600222C RID: 8748 RVA: 0x0007B252 File Offset: 0x00079452
			internal void <PartitionerForEachWorker>b__0(object o)
			{
				this.oce = new OperationCanceledException(this.parallelOptions.CancellationToken);
				this.sharedPStateFlags.Cancel();
			}

			// Token: 0x0600222D RID: 8749 RVA: 0x0007B278 File Offset: 0x00079478
			internal void <PartitionerForEachWorker>b__1(ref IEnumerator partitionState, int timeout, out bool replicationDelegateYieldedBeforeCompletion)
			{
				replicationDelegateYieldedBeforeCompletion = false;
				if (ParallelEtwProvider.Log.IsEnabled())
				{
					ParallelEtwProvider.Log.ParallelFork(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
				}
				TLocal tlocal = default(TLocal);
				bool flag = false;
				try
				{
					ParallelLoopState64 parallelLoopState = null;
					if (this.bodyWithState != null || this.bodyWithStateAndIndex != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
					}
					else if (this.bodyWithStateAndLocal != null || this.bodyWithEverything != null)
					{
						parallelLoopState = new ParallelLoopState64(this.sharedPStateFlags);
						if (this.localInit != null)
						{
							tlocal = this.localInit();
							flag = true;
						}
					}
					int timeoutOccursAt = Parallel.ComputeTimeoutPoint(timeout);
					if (this.orderedSource != null)
					{
						IEnumerator<KeyValuePair<long, TSource>> enumerator = partitionState as IEnumerator<KeyValuePair<long, TSource>>;
						if (enumerator == null)
						{
							enumerator = this.orderablePartitionerSource.GetEnumerator();
							partitionState = enumerator;
						}
						if (enumerator == null)
						{
							throw new InvalidOperationException("The Partitioner source returned a null enumerator.");
						}
						while (enumerator.MoveNext())
						{
							KeyValuePair<long, TSource> keyValuePair = enumerator.Current;
							long key = keyValuePair.Key;
							TSource value = keyValuePair.Value;
							if (parallelLoopState != null)
							{
								parallelLoopState.CurrentIteration = key;
							}
							if (this.simpleBody != null)
							{
								this.simpleBody(value);
							}
							else if (this.bodyWithState != null)
							{
								this.bodyWithState(value, parallelLoopState);
							}
							else if (this.bodyWithStateAndIndex != null)
							{
								this.bodyWithStateAndIndex(value, parallelLoopState, key);
							}
							else if (this.bodyWithStateAndLocal != null)
							{
								tlocal = this.bodyWithStateAndLocal(value, parallelLoopState, tlocal);
							}
							else
							{
								tlocal = this.bodyWithEverything(value, parallelLoopState, key, tlocal);
							}
							if (this.sharedPStateFlags.ShouldExitLoop(key))
							{
								break;
							}
							if (Parallel.CheckTimeoutReached(timeoutOccursAt))
							{
								replicationDelegateYieldedBeforeCompletion = true;
								break;
							}
						}
					}
					else
					{
						IEnumerator<TSource> enumerator2 = partitionState as IEnumerator<TSource>;
						if (enumerator2 == null)
						{
							enumerator2 = this.partitionerSource.GetEnumerator();
							partitionState = enumerator2;
						}
						if (enumerator2 == null)
						{
							throw new InvalidOperationException("The Partitioner source returned a null enumerator.");
						}
						if (parallelLoopState != null)
						{
							parallelLoopState.CurrentIteration = 0L;
						}
						while (enumerator2.MoveNext())
						{
							TSource tsource = enumerator2.Current;
							if (this.simpleBody != null)
							{
								this.simpleBody(tsource);
							}
							else if (this.bodyWithState != null)
							{
								this.bodyWithState(tsource, parallelLoopState);
							}
							else if (this.bodyWithStateAndLocal != null)
							{
								tlocal = this.bodyWithStateAndLocal(tsource, parallelLoopState, tlocal);
							}
							if (this.sharedPStateFlags.LoopStateFlags != 0)
							{
								break;
							}
							if (Parallel.CheckTimeoutReached(timeoutOccursAt))
							{
								replicationDelegateYieldedBeforeCompletion = true;
								break;
							}
						}
					}
				}
				catch (Exception source)
				{
					this.sharedPStateFlags.SetExceptional();
					ExceptionDispatchInfo.Throw(source);
				}
				finally
				{
					if (this.localFinally != null && flag)
					{
						this.localFinally(tlocal);
					}
					if (!replicationDelegateYieldedBeforeCompletion)
					{
						IDisposable disposable = partitionState as IDisposable;
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					if (ParallelEtwProvider.Log.IsEnabled())
					{
						ParallelEtwProvider.Log.ParallelJoin(TaskScheduler.Current.Id, Task.CurrentId.GetValueOrDefault(), this.forkJoinContextID);
					}
				}
			}

			// Token: 0x04001C16 RID: 7190
			public OperationCanceledException oce;

			// Token: 0x04001C17 RID: 7191
			public ParallelOptions parallelOptions;

			// Token: 0x04001C18 RID: 7192
			public ParallelLoopStateFlags64 sharedPStateFlags;

			// Token: 0x04001C19 RID: 7193
			public int forkJoinContextID;

			// Token: 0x04001C1A RID: 7194
			public Action<TSource, ParallelLoopState> bodyWithState;

			// Token: 0x04001C1B RID: 7195
			public Action<TSource, ParallelLoopState, long> bodyWithStateAndIndex;

			// Token: 0x04001C1C RID: 7196
			public Func<TSource, ParallelLoopState, TLocal, TLocal> bodyWithStateAndLocal;

			// Token: 0x04001C1D RID: 7197
			public Func<TSource, ParallelLoopState, long, TLocal, TLocal> bodyWithEverything;

			// Token: 0x04001C1E RID: 7198
			public Func<TLocal> localInit;

			// Token: 0x04001C1F RID: 7199
			public OrderablePartitioner<TSource> orderedSource;

			// Token: 0x04001C20 RID: 7200
			public IEnumerable<KeyValuePair<long, TSource>> orderablePartitionerSource;

			// Token: 0x04001C21 RID: 7201
			public Action<TSource> simpleBody;

			// Token: 0x04001C22 RID: 7202
			public IEnumerable<TSource> partitionerSource;

			// Token: 0x04001C23 RID: 7203
			public Action<TLocal> localFinally;
		}
	}
}
