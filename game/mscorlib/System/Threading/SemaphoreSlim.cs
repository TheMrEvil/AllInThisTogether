using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Threading
{
	/// <summary>Represents a lightweight alternative to <see cref="T:System.Threading.Semaphore" /> that limits the number of threads that can access a resource or pool of resources concurrently.</summary>
	// Token: 0x020002BB RID: 699
	[ComVisible(false)]
	[DebuggerDisplay("Current Count = {m_currentCount}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class SemaphoreSlim : IDisposable
	{
		/// <summary>Gets the number of remaining threads that can enter the <see cref="T:System.Threading.SemaphoreSlim" /> object.</summary>
		/// <returns>The number of remaining threads that can enter the semaphore.</returns>
		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00070DA5 File Offset: 0x0006EFA5
		public int CurrentCount
		{
			get
			{
				return this.m_currentCount;
			}
		}

		/// <summary>Returns a <see cref="T:System.Threading.WaitHandle" /> that can be used to wait on the semaphore.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that can be used to wait on the semaphore.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.SemaphoreSlim" /> has been disposed.</exception>
		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x00070DB0 File Offset: 0x0006EFB0
		public WaitHandle AvailableWaitHandle
		{
			get
			{
				this.CheckDispose();
				if (this.m_waitHandle != null)
				{
					return this.m_waitHandle;
				}
				object lockObj = this.m_lockObj;
				lock (lockObj)
				{
					if (this.m_waitHandle == null)
					{
						this.m_waitHandle = new ManualResetEvent(this.m_currentCount != 0);
					}
				}
				return this.m_waitHandle;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreSlim" /> class, specifying the initial number of requests that can be granted concurrently.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCount" /> is less than 0.</exception>
		// Token: 0x06001E7C RID: 7804 RVA: 0x00070E30 File Offset: 0x0006F030
		public SemaphoreSlim(int initialCount) : this(initialCount, int.MaxValue)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SemaphoreSlim" /> class, specifying the initial and maximum number of requests that can be granted concurrently.</summary>
		/// <param name="initialCount">The initial number of requests for the semaphore that can be granted concurrently.</param>
		/// <param name="maxCount">The maximum number of requests for the semaphore that can be granted concurrently.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCount" /> is less than 0, or <paramref name="initialCount" /> is greater than <paramref name="maxCount" />, or <paramref name="maxCount" /> is equal to or less than 0.</exception>
		// Token: 0x06001E7D RID: 7805 RVA: 0x00070E40 File Offset: 0x0006F040
		public SemaphoreSlim(int initialCount, int maxCount)
		{
			if (initialCount < 0 || initialCount > maxCount)
			{
				throw new ArgumentOutOfRangeException("initialCount", initialCount, SemaphoreSlim.GetResourceString("The initialCount argument must be non-negative and less than or equal to the maximumCount."));
			}
			if (maxCount <= 0)
			{
				throw new ArgumentOutOfRangeException("maxCount", maxCount, SemaphoreSlim.GetResourceString("The maximumCount argument must be a positive number. If a maximum is not required, use the constructor without a maxCount parameter."));
			}
			this.m_maxCount = maxCount;
			this.m_lockObj = new object();
			this.m_currentCount = initialCount;
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x06001E7E RID: 7806 RVA: 0x00070EB0 File Offset: 0x0006F0B0
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> token to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.  
		///  -or-  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06001E7F RID: 7807 RVA: 0x00070ECE File Offset: 0x0006F0CE
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a <see cref="T:System.TimeSpan" /> to specify the timeout.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely, or a <see cref="T:System.TimeSpan" /> that represents 0 milliseconds to test the wait handle and return immediately.</param>
		/// <returns>
		///   <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The semaphoreSlim instance has been disposed <paramref name="." /></exception>
		// Token: 0x06001E80 RID: 7808 RVA: 0x00070EDC File Offset: 0x0006F0DC
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.Wait((int)timeout.TotalMilliseconds, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a <see cref="T:System.TimeSpan" /> that specifies the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely, or a <see cref="T:System.TimeSpan" /> that represents 0 milliseconds to test the wait handle and return immediately.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>
		///   <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The semaphoreSlim instance has been disposed <paramref name="." /><paramref name="-or-" />  
		///  The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06001E81 RID: 7809 RVA: 0x00070F34 File Offset: 0x0006F134
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.Wait((int)timeout.TotalMilliseconds, cancellationToken);
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer that specifies the timeout.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely, or zero to test the state of the wait handle and return immediately.</param>
		/// <returns>
		///   <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.SemaphoreSlim" /> has been disposed.</exception>
		// Token: 0x06001E82 RID: 7810 RVA: 0x00070F84 File Offset: 0x0006F184
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until it can enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer that specifies the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely, or zero to test the state of the wait handle and return immediately.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>
		///   <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.SemaphoreSlim" /> instance has been disposed, or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06001E83 RID: 7811 RVA: 0x00070FA4 File Offset: 0x0006F1A4
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout == 0 && this.m_currentCount == 0)
			{
				return false;
			}
			uint startTime = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout > 0)
			{
				startTime = TimeoutHelper.GetTime();
			}
			bool result = false;
			Task<bool> task = null;
			bool flag = false;
			CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.InternalRegisterWithoutEC(SemaphoreSlim.s_cancellationTokenCanceledEventHandler, this);
			try
			{
				SpinWait spinWait = default(SpinWait);
				while (this.m_currentCount == 0 && !spinWait.NextSpinWillYield)
				{
					spinWait.SpinOnce();
				}
				try
				{
				}
				finally
				{
					Monitor.Enter(this.m_lockObj, ref flag);
					if (flag)
					{
						this.m_waitCount++;
					}
				}
				if (this.m_asyncHead != null)
				{
					task = this.WaitAsync(millisecondsTimeout, cancellationToken);
				}
				else
				{
					OperationCanceledException ex = null;
					if (this.m_currentCount == 0)
					{
						if (millisecondsTimeout == 0)
						{
							return false;
						}
						try
						{
							result = this.WaitUntilCountOrTimeout(millisecondsTimeout, startTime, cancellationToken);
						}
						catch (OperationCanceledException ex)
						{
						}
					}
					if (this.m_currentCount > 0)
					{
						result = true;
						this.m_currentCount--;
					}
					else if (ex != null)
					{
						throw ex;
					}
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
				}
			}
			finally
			{
				if (flag)
				{
					this.m_waitCount--;
					Monitor.Exit(this.m_lockObj);
				}
				cancellationTokenRegistration.Dispose();
			}
			if (task == null)
			{
				return result;
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00071150 File Offset: 0x0006F350
		private bool WaitUntilCountOrTimeout(int millisecondsTimeout, uint startTime, CancellationToken cancellationToken)
		{
			int num = -1;
			while (this.m_currentCount == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (millisecondsTimeout != -1)
				{
					num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
					if (num <= 0)
					{
						return false;
					}
				}
				if (!Monitor.Wait(this.m_lockObj, num))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />.</summary>
		/// <returns>A task that will complete when the semaphore has been entered.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.SemaphoreSlim" /> has been disposed.</exception>
		// Token: 0x06001E85 RID: 7813 RVA: 0x00071198 File Offset: 0x0006F398
		public Task WaitAsync()
		{
			return this.WaitAsync(-1, default(CancellationToken));
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> token to observe.</param>
		/// <returns>A task that will complete when the semaphore has been entered.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		// Token: 0x06001E86 RID: 7814 RVA: 0x000711B5 File Offset: 0x0006F3B5
		public Task WaitAsync(CancellationToken cancellationToken)
		{
			return this.WaitAsync(-1, cancellationToken);
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer to measure the time interval.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely, or zero to test the state of the wait handle and return immediately.</param>
		/// <returns>A task that will complete with a result of <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001E87 RID: 7815 RVA: 0x000711C0 File Offset: 0x0006F3C0
		public Task<bool> WaitAsync(int millisecondsTimeout)
		{
			return this.WaitAsync(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a <see cref="T:System.TimeSpan" /> to measure the time interval.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely, or a <see cref="T:System.TimeSpan" /> that represents 0 milliseconds to test the wait handle and return immediately.</param>
		/// <returns>A task that will complete with a result of <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001E88 RID: 7816 RVA: 0x000711E0 File Offset: 0x0006F3E0
		public Task<bool> WaitAsync(TimeSpan timeout)
		{
			return this.WaitAsync(timeout, default(CancellationToken));
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a <see cref="T:System.TimeSpan" /> to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely, or a <see cref="T:System.TimeSpan" /> that represents 0 milliseconds to test the wait handle and return immediately.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> token to observe.</param>
		/// <returns>A task that will complete with a result of <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.SemaphoreSlim" /> has been disposed.</exception>
		// Token: 0x06001E89 RID: 7817 RVA: 0x00071200 File Offset: 0x0006F400
		public Task<bool> WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			return this.WaitAsync((int)timeout.TotalMilliseconds, cancellationToken);
		}

		/// <summary>Asynchronously waits to enter the <see cref="T:System.Threading.SemaphoreSlim" />, using a 32-bit signed integer to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely, or zero to test the state of the wait handle and return immediately.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>A task that will complete with a result of <see langword="true" /> if the current thread successfully entered the <see cref="T:System.Threading.SemaphoreSlim" />, otherwise with a result of <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a number other than -1, which represents an infinite timeout -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		// Token: 0x06001E8A RID: 7818 RVA: 0x00071250 File Offset: 0x0006F450
		public Task<bool> WaitAsync(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.CheckDispose();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("totalMilliSeconds", millisecondsTimeout, SemaphoreSlim.GetResourceString("The timeout must represent a value between -1 and Int32.MaxValue, inclusive."));
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCancellation<bool>(cancellationToken);
			}
			object lockObj = this.m_lockObj;
			Task<bool> result;
			lock (lockObj)
			{
				if (this.m_currentCount > 0)
				{
					this.m_currentCount--;
					if (this.m_waitHandle != null && this.m_currentCount == 0)
					{
						this.m_waitHandle.Reset();
					}
					result = SemaphoreSlim.s_trueTask;
				}
				else if (millisecondsTimeout == 0)
				{
					result = SemaphoreSlim.s_falseTask;
				}
				else
				{
					SemaphoreSlim.TaskNode taskNode = this.CreateAndAddAsyncWaiter();
					result = ((millisecondsTimeout == -1 && !cancellationToken.CanBeCanceled) ? taskNode : this.WaitUntilCountOrTimeoutAsync(taskNode, millisecondsTimeout, cancellationToken));
				}
			}
			return result;
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00071334 File Offset: 0x0006F534
		private SemaphoreSlim.TaskNode CreateAndAddAsyncWaiter()
		{
			SemaphoreSlim.TaskNode taskNode = new SemaphoreSlim.TaskNode();
			if (this.m_asyncHead == null)
			{
				this.m_asyncHead = taskNode;
				this.m_asyncTail = taskNode;
			}
			else
			{
				this.m_asyncTail.Next = taskNode;
				taskNode.Prev = this.m_asyncTail;
				this.m_asyncTail = taskNode;
			}
			return taskNode;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00071380 File Offset: 0x0006F580
		private bool RemoveAsyncWaiter(SemaphoreSlim.TaskNode task)
		{
			bool result = this.m_asyncHead == task || task.Prev != null;
			if (task.Next != null)
			{
				task.Next.Prev = task.Prev;
			}
			if (task.Prev != null)
			{
				task.Prev.Next = task.Next;
			}
			if (this.m_asyncHead == task)
			{
				this.m_asyncHead = task.Next;
			}
			if (this.m_asyncTail == task)
			{
				this.m_asyncTail = task.Prev;
			}
			task.Next = (task.Prev = null);
			return result;
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00071410 File Offset: 0x0006F610
		private Task<bool> WaitUntilCountOrTimeoutAsync(SemaphoreSlim.TaskNode asyncWaiter, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__32 <WaitUntilCountOrTimeoutAsync>d__;
			<WaitUntilCountOrTimeoutAsync>d__.<>4__this = this;
			<WaitUntilCountOrTimeoutAsync>d__.asyncWaiter = asyncWaiter;
			<WaitUntilCountOrTimeoutAsync>d__.millisecondsTimeout = millisecondsTimeout;
			<WaitUntilCountOrTimeoutAsync>d__.cancellationToken = cancellationToken;
			<WaitUntilCountOrTimeoutAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<WaitUntilCountOrTimeoutAsync>d__.<>1__state = -1;
			<WaitUntilCountOrTimeoutAsync>d__.<>t__builder.Start<SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__32>(ref <WaitUntilCountOrTimeoutAsync>d__);
			return <WaitUntilCountOrTimeoutAsync>d__.<>t__builder.Task;
		}

		/// <summary>Releases the <see cref="T:System.Threading.SemaphoreSlim" /> object once.</summary>
		/// <returns>The previous count of the <see cref="T:System.Threading.SemaphoreSlim" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.Threading.SemaphoreFullException">The <see cref="T:System.Threading.SemaphoreSlim" /> has already reached its maximum size.</exception>
		// Token: 0x06001E8E RID: 7822 RVA: 0x0007146B File Offset: 0x0006F66B
		public int Release()
		{
			return this.Release(1);
		}

		/// <summary>Releases the <see cref="T:System.Threading.SemaphoreSlim" /> object a specified number of times.</summary>
		/// <param name="releaseCount">The number of times to exit the semaphore.</param>
		/// <returns>The previous count of the <see cref="T:System.Threading.SemaphoreSlim" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="releaseCount" /> is less than 1.</exception>
		/// <exception cref="T:System.Threading.SemaphoreFullException">The <see cref="T:System.Threading.SemaphoreSlim" /> has already reached its maximum size.</exception>
		// Token: 0x06001E8F RID: 7823 RVA: 0x00071474 File Offset: 0x0006F674
		public int Release(int releaseCount)
		{
			this.CheckDispose();
			if (releaseCount < 1)
			{
				throw new ArgumentOutOfRangeException("releaseCount", releaseCount, SemaphoreSlim.GetResourceString("The releaseCount argument must be greater than zero."));
			}
			object lockObj = this.m_lockObj;
			int num2;
			lock (lockObj)
			{
				int num = this.m_currentCount;
				num2 = num;
				if (this.m_maxCount - num < releaseCount)
				{
					throw new SemaphoreFullException();
				}
				num += releaseCount;
				int waitCount = this.m_waitCount;
				if (num == 1 || waitCount == 1)
				{
					Monitor.Pulse(this.m_lockObj);
				}
				else if (waitCount > 1)
				{
					Monitor.PulseAll(this.m_lockObj);
				}
				if (this.m_asyncHead != null)
				{
					int num3 = num - waitCount;
					while (num3 > 0 && this.m_asyncHead != null)
					{
						num--;
						num3--;
						SemaphoreSlim.TaskNode asyncHead = this.m_asyncHead;
						this.RemoveAsyncWaiter(asyncHead);
						SemaphoreSlim.QueueWaiterTask(asyncHead);
					}
				}
				this.m_currentCount = num;
				if (this.m_waitHandle != null && num2 == 0 && num > 0)
				{
					this.m_waitHandle.Set();
				}
			}
			return num2;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x0007158C File Offset: 0x0006F78C
		[SecuritySafeCritical]
		private static void QueueWaiterTask(SemaphoreSlim.TaskNode waiterTask)
		{
			ThreadPool.UnsafeQueueCustomWorkItem(waiterTask, false);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.SemaphoreSlim" /> class.</summary>
		// Token: 0x06001E91 RID: 7825 RVA: 0x00071595 File Offset: 0x0006F795
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.SemaphoreSlim" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06001E92 RID: 7826 RVA: 0x000715A4 File Offset: 0x0006F7A4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_waitHandle != null)
				{
					this.m_waitHandle.Close();
					this.m_waitHandle = null;
				}
				this.m_lockObj = null;
				this.m_asyncHead = null;
				this.m_asyncTail = null;
			}
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000715E0 File Offset: 0x0006F7E0
		private static void CancellationTokenCanceledEventHandler(object obj)
		{
			SemaphoreSlim semaphoreSlim = obj as SemaphoreSlim;
			object lockObj = semaphoreSlim.m_lockObj;
			lock (lockObj)
			{
				Monitor.PulseAll(semaphoreSlim.m_lockObj);
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x0007162C File Offset: 0x0006F82C
		private void CheckDispose()
		{
			if (this.m_lockObj == null)
			{
				throw new ObjectDisposedException(null, SemaphoreSlim.GetResourceString("The semaphore has been disposed."));
			}
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00071647 File Offset: 0x0006F847
		private static string GetResourceString(string str)
		{
			return Environment.GetResourceString(str);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00071650 File Offset: 0x0006F850
		// Note: this type is marked as 'beforefieldinit'.
		static SemaphoreSlim()
		{
		}

		// Token: 0x04001AB9 RID: 6841
		private volatile int m_currentCount;

		// Token: 0x04001ABA RID: 6842
		private readonly int m_maxCount;

		// Token: 0x04001ABB RID: 6843
		private volatile int m_waitCount;

		// Token: 0x04001ABC RID: 6844
		private object m_lockObj;

		// Token: 0x04001ABD RID: 6845
		private volatile ManualResetEvent m_waitHandle;

		// Token: 0x04001ABE RID: 6846
		private SemaphoreSlim.TaskNode m_asyncHead;

		// Token: 0x04001ABF RID: 6847
		private SemaphoreSlim.TaskNode m_asyncTail;

		// Token: 0x04001AC0 RID: 6848
		private static readonly Task<bool> s_trueTask = new Task<bool>(false, true, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x04001AC1 RID: 6849
		private static readonly Task<bool> s_falseTask = new Task<bool>(false, false, (TaskCreationOptions)16384, default(CancellationToken));

		// Token: 0x04001AC2 RID: 6850
		private const int NO_MAXIMUM = 2147483647;

		// Token: 0x04001AC3 RID: 6851
		private static Action<object> s_cancellationTokenCanceledEventHandler = new Action<object>(SemaphoreSlim.CancellationTokenCanceledEventHandler);

		// Token: 0x020002BC RID: 700
		private sealed class TaskNode : Task<bool>, IThreadPoolWorkItem
		{
			// Token: 0x06001E97 RID: 7831 RVA: 0x000716A2 File Offset: 0x0006F8A2
			internal TaskNode()
			{
			}

			// Token: 0x06001E98 RID: 7832 RVA: 0x000716AA File Offset: 0x0006F8AA
			[SecurityCritical]
			void IThreadPoolWorkItem.ExecuteWorkItem()
			{
				base.TrySetResult(true);
			}

			// Token: 0x06001E99 RID: 7833 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[SecurityCritical]
			void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
			{
			}

			// Token: 0x04001AC4 RID: 6852
			internal SemaphoreSlim.TaskNode Prev;

			// Token: 0x04001AC5 RID: 6853
			internal SemaphoreSlim.TaskNode Next;
		}

		// Token: 0x020002BD RID: 701
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitUntilCountOrTimeoutAsync>d__32 : IAsyncStateMachine
		{
			// Token: 0x06001E9A RID: 7834 RVA: 0x000716B4 File Offset: 0x0006F8B4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				SemaphoreSlim semaphoreSlim = this.<>4__this;
				bool result2;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
							goto IL_1CF;
						}
						this.<cts>5__2 = (this.cancellationToken.CanBeCanceled ? CancellationTokenSource.CreateLinkedTokenSource(this.cancellationToken, default(CancellationToken)) : new CancellationTokenSource());
					}
					try
					{
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter awaiter2;
						if (num != 0)
						{
							Task<Task> task = Task.WhenAny(new Task[]
							{
								this.asyncWaiter,
								Task.Delay(this.millisecondsTimeout, this.<cts>5__2.Token)
							});
							this.<>7__wrap2 = this.asyncWaiter;
							awaiter2 = task.ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter, SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__32>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						Task result = awaiter2.GetResult();
						if (this.<>7__wrap2 == result)
						{
							this.<>7__wrap2 = null;
							this.<cts>5__2.Cancel();
							result2 = true;
							goto IL_1F2;
						}
					}
					finally
					{
						if (num < 0 && this.<cts>5__2 != null)
						{
							((IDisposable)this.<cts>5__2).Dispose();
						}
					}
					this.<cts>5__2 = null;
					object lockObj = semaphoreSlim.m_lockObj;
					bool flag = false;
					try
					{
						Monitor.Enter(lockObj, ref flag);
						if (semaphoreSlim.RemoveAsyncWaiter(this.asyncWaiter))
						{
							this.cancellationToken.ThrowIfCancellationRequested();
							result2 = false;
							goto IL_1F2;
						}
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(lockObj);
						}
					}
					awaiter = this.asyncWaiter.ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 1);
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, SemaphoreSlim.<WaitUntilCountOrTimeoutAsync>d__32>(ref awaiter, ref this);
						return;
					}
					IL_1CF:
					result2 = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1F2:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06001E9B RID: 7835 RVA: 0x00071914 File Offset: 0x0006FB14
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04001AC6 RID: 6854
			public int <>1__state;

			// Token: 0x04001AC7 RID: 6855
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04001AC8 RID: 6856
			public CancellationToken cancellationToken;

			// Token: 0x04001AC9 RID: 6857
			public SemaphoreSlim.TaskNode asyncWaiter;

			// Token: 0x04001ACA RID: 6858
			public int millisecondsTimeout;

			// Token: 0x04001ACB RID: 6859
			public SemaphoreSlim <>4__this;

			// Token: 0x04001ACC RID: 6860
			private CancellationTokenSource <cts>5__2;

			// Token: 0x04001ACD RID: 6861
			private object <>7__wrap2;

			// Token: 0x04001ACE RID: 6862
			private ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04001ACF RID: 6863
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
