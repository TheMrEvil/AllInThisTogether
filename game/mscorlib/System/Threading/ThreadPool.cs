using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	/// <summary>Provides a pool of threads that can be used to execute tasks, post work items, process asynchronous I/O, wait on behalf of other threads, and process timers.</summary>
	// Token: 0x020002E6 RID: 742
	public static class ThreadPool
	{
		/// <summary>Sets the number of requests to the thread pool that can be active concurrently. All requests above that number remain queued until thread pool threads become available.</summary>
		/// <param name="workerThreads">The maximum number of worker threads in the thread pool.</param>
		/// <param name="completionPortThreads">The maximum number of asynchronous I/O threads in the thread pool.</param>
		/// <returns>
		///   <see langword="true" /> if the change is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002035 RID: 8245 RVA: 0x000757AC File Offset: 0x000739AC
		[SecuritySafeCritical]
		public static bool SetMaxThreads(int workerThreads, int completionPortThreads)
		{
			return ThreadPool.SetMaxThreadsNative(workerThreads, completionPortThreads);
		}

		/// <summary>Retrieves the number of requests to the thread pool that can be active concurrently. All requests above that number remain queued until thread pool threads become available.</summary>
		/// <param name="workerThreads">The maximum number of worker threads in the thread pool.</param>
		/// <param name="completionPortThreads">The maximum number of asynchronous I/O threads in the thread pool.</param>
		// Token: 0x06002036 RID: 8246 RVA: 0x000757B5 File Offset: 0x000739B5
		[SecuritySafeCritical]
		public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMaxThreadsNative(out workerThreads, out completionPortThreads);
		}

		/// <summary>Sets the minimum number of threads the thread pool creates on demand, as new requests are made, before switching to an algorithm for managing thread creation and destruction.</summary>
		/// <param name="workerThreads">The minimum number of worker threads that the thread pool creates on demand.</param>
		/// <param name="completionPortThreads">The minimum number of asynchronous I/O threads that the thread pool creates on demand.</param>
		/// <returns>
		///   <see langword="true" /> if the change is successful; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002037 RID: 8247 RVA: 0x000757BE File Offset: 0x000739BE
		[SecuritySafeCritical]
		public static bool SetMinThreads(int workerThreads, int completionPortThreads)
		{
			return ThreadPool.SetMinThreadsNative(workerThreads, completionPortThreads);
		}

		/// <summary>Retrieves the minimum number of threads the thread pool creates on demand, as new requests are made, before switching to an algorithm for managing thread creation and destruction.</summary>
		/// <param name="workerThreads">When this method returns, contains the minimum number of worker threads that the thread pool creates on demand.</param>
		/// <param name="completionPortThreads">When this method returns, contains the minimum number of asynchronous I/O threads that the thread pool creates on demand.</param>
		// Token: 0x06002038 RID: 8248 RVA: 0x000757C7 File Offset: 0x000739C7
		[SecuritySafeCritical]
		public static void GetMinThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMinThreadsNative(out workerThreads, out completionPortThreads);
		}

		/// <summary>Retrieves the difference between the maximum number of thread pool threads returned by the <see cref="M:System.Threading.ThreadPool.GetMaxThreads(System.Int32@,System.Int32@)" /> method, and the number currently active.</summary>
		/// <param name="workerThreads">The number of available worker threads.</param>
		/// <param name="completionPortThreads">The number of available asynchronous I/O threads.</param>
		// Token: 0x06002039 RID: 8249 RVA: 0x000757D0 File Offset: 0x000739D0
		[SecuritySafeCritical]
		public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetAvailableThreadsNative(out workerThreads, out completionPortThreads);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit unsigned integer for the time-out in milliseconds.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses.</param>
		/// <param name="executeOnlyOnce">
		///   <see langword="true" /> to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; <see langword="false" /> to indicate that the timer is reset every time the wait operation completes until the wait is unregistered.</param>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that can be used to cancel the registered wait operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1.</exception>
		// Token: 0x0600203A RID: 8250 RVA: 0x000757DC File Offset: 0x000739DC
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, true);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit unsigned integer for the time-out in milliseconds. This method does not propagate the calling stack to the worker thread.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled.</param>
		/// <param name="state">The object that is passed to the delegate.</param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses.</param>
		/// <param name="executeOnlyOnce">
		///   <see langword="true" /> to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; <see langword="false" /> to indicate that the timer is reset every time the wait operation completes until the wait is unregistered.</param>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600203B RID: 8251 RVA: 0x000757FC File Offset: 0x000739FC
		[SecurityCritical]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0007581C File Offset: 0x00073A1C
		[SecurityCritical]
		private static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce, ref StackCrawlMark stackMark, bool compressStack)
		{
			if (waitObject == null)
			{
				throw new ArgumentNullException("waitObject");
			}
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			if (millisecondsTimeOutInterval != 4294967295U && millisecondsTimeOutInterval > 2147483647U)
			{
				throw new NotSupportedException("Timeout is too big. Maximum is Int32.MaxValue");
			}
			RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle(waitObject, callBack, state, new TimeSpan(0, 0, 0, 0, (int)millisecondsTimeOutInterval), executeOnlyOnce);
			if (compressStack)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(registeredWaitHandle.Wait), null);
			}
			else
			{
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(registeredWaitHandle.Wait), null);
			}
			return registeredWaitHandle;
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit signed integer for the time-out in milliseconds.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled.</param>
		/// <param name="state">The object that is passed to the delegate.</param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses.</param>
		/// <param name="executeOnlyOnce">
		///   <see langword="true" /> to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; <see langword="false" /> to indicate that the timer is reset every time the wait operation completes until the wait is unregistered.</param>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1.</exception>
		// Token: 0x0600203D RID: 8253 RVA: 0x000758A0 File Offset: 0x00073AA0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)((millisecondsTimeOutInterval == -1) ? -1 : millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, true);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, using a 32-bit signed integer for the time-out in milliseconds. This method does not propagate the calling stack to the worker thread.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled.</param>
		/// <param name="state">The object that is passed to the delegate.</param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses.</param>
		/// <param name="executeOnlyOnce">
		///   <see langword="true" /> to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; <see langword="false" /> to indicate that the timer is reset every time the wait operation completes until the wait is unregistered.</param>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x0600203E RID: 8254 RVA: 0x000758E0 File Offset: 0x00073AE0
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)((millisecondsTimeOutInterval == -1) ? -1 : millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, false);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 64-bit signed integer for the time-out in milliseconds.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses.</param>
		/// <param name="executeOnlyOnce">
		///   <see langword="true" /> to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; <see langword="false" /> to indicate that the timer is reset every time the wait operation completes until the wait is unregistered.</param>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1.</exception>
		// Token: 0x0600203F RID: 8255 RVA: 0x00075920 File Offset: 0x00073B20
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (millisecondsTimeOutInterval == -1L) ? uint.MaxValue : ((uint)millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, true);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 64-bit signed integer for the time-out in milliseconds. This method does not propagate the calling stack to the worker thread.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled.</param>
		/// <param name="state">The object that is passed to the delegate.</param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses.</param>
		/// <param name="executeOnlyOnce">
		///   <see langword="true" /> to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; <see langword="false" /> to indicate that the timer is reset every time the wait operation completes until the wait is unregistered.</param>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06002040 RID: 8256 RVA: 0x00075960 File Offset: 0x00073B60
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (millisecondsTimeOutInterval == -1L) ? uint.MaxValue : ((uint)millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, false);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <param name="timeout">The time-out represented by a <see cref="T:System.TimeSpan" />. If <paramref name="timeout" /> is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="timeout" /> is -1, the function's time-out interval never elapses.</param>
		/// <param name="executeOnlyOnce">
		///   <see langword="true" /> to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; <see langword="false" /> to indicate that the timer is reset every time the wait operation completes until the wait is unregistered.</param>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="timeout" /> parameter is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06002041 RID: 8257 RVA: 0x000759A0 File Offset: 0x00073BA0
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Argument must be less than or equal to 2^31 - 1 milliseconds."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, ref stackCrawlMark, true);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a <see cref="T:System.TimeSpan" /> value for the time-out. This method does not propagate the calling stack to the worker thread.</summary>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled.</param>
		/// <param name="state">The object that is passed to the delegate.</param>
		/// <param name="timeout">The time-out represented by a <see cref="T:System.TimeSpan" />. If <paramref name="timeout" /> is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="timeout" /> is -1, the function's time-out interval never elapses.</param>
		/// <param name="executeOnlyOnce">
		///   <see langword="true" /> to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; <see langword="false" /> to indicate that the timer is reset every time the wait operation completes until the wait is unregistered.</param>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="timeout" /> parameter is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06002042 RID: 8258 RVA: 0x00075A00 File Offset: 0x00073C00
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Argument must be less than or equal to 2^31 - 1 milliseconds."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, ref stackCrawlMark, false);
		}

		/// <summary>Queues a method for execution, and specifies an object containing data to be used by the method. The method executes when a thread pool thread becomes available.</summary>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> representing the method to execute.</param>
		/// <param name="state">An object containing data to be used by the method.</param>
		/// <returns>
		///   <see langword="true" /> if the method is successfully queued; <see cref="T:System.NotSupportedException" /> is thrown if the work item could not be queued.</returns>
		/// <exception cref="T:System.NotSupportedException">The common language runtime (CLR) is hosted, and the host does not support this action.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is <see langword="null" />.</exception>
		// Token: 0x06002043 RID: 8259 RVA: 0x00075A60 File Offset: 0x00073C60
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool QueueUserWorkItem(WaitCallback callBack, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackCrawlMark, true, true);
		}

		/// <summary>Queues a method for execution. The method executes when a thread pool thread becomes available.</summary>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> that represents the method to be executed.</param>
		/// <returns>
		///   <see langword="true" /> if the method is successfully queued; <see cref="T:System.NotSupportedException" /> is thrown if the work item could not be queued.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The common language runtime (CLR) is hosted, and the host does not support this action.</exception>
		// Token: 0x06002044 RID: 8260 RVA: 0x00075A7C File Offset: 0x00073C7C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool QueueUserWorkItem(WaitCallback callBack)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, null, ref stackCrawlMark, true, true);
		}

		/// <summary>Queues the specified delegate to the thread pool, but does not propagate the calling stack to the worker thread.</summary>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> that represents the delegate to invoke when a thread in the thread pool picks up the work item.</param>
		/// <param name="state">The object that is passed to the delegate when serviced from the thread pool.</param>
		/// <returns>
		///   <see langword="true" /> if the method succeeds; <see cref="T:System.OutOfMemoryException" /> is thrown if the work item could not be queued.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		/// <exception cref="T:System.ApplicationException">An out-of-memory condition was encountered.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The work item could not be queued.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is <see langword="null" />.</exception>
		// Token: 0x06002045 RID: 8261 RVA: 0x00075A98 File Offset: 0x00073C98
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackCrawlMark, false, true);
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x00075AB4 File Offset: 0x00073CB4
		public static bool QueueUserWorkItem<TState>(Action<TState> callBack, TState state, bool preferLocal)
		{
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(delegate(object x)
			{
				callBack((TState)((object)x));
			}, state, ref stackCrawlMark, true, !preferLocal);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x00075B00 File Offset: 0x00073D00
		public static bool UnsafeQueueUserWorkItem<TState>(Action<TState> callBack, TState state, bool preferLocal)
		{
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(delegate(object x)
			{
				callBack((TState)((object)x));
			}, state, ref stackCrawlMark, false, !preferLocal);
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x00075B4C File Offset: 0x00073D4C
		[SecurityCritical]
		private static bool QueueUserWorkItemHelper(WaitCallback callBack, object state, ref StackCrawlMark stackMark, bool compressStack, bool forceGlobal = true)
		{
			bool result = true;
			if (callBack != null)
			{
				ThreadPool.EnsureVMInitialized();
				try
				{
					return result;
				}
				finally
				{
					QueueUserWorkItemCallback callback = new QueueUserWorkItemCallback(callBack, state, compressStack, ref stackMark);
					ThreadPoolGlobals.workQueue.Enqueue(callback, forceGlobal);
					result = true;
				}
			}
			throw new ArgumentNullException("WaitCallback");
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00075B9C File Offset: 0x00073D9C
		[SecurityCritical]
		internal static void UnsafeQueueCustomWorkItem(IThreadPoolWorkItem workItem, bool forceGlobal)
		{
			ThreadPool.EnsureVMInitialized();
			try
			{
			}
			finally
			{
				ThreadPoolGlobals.workQueue.Enqueue(workItem, forceGlobal);
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00075BD0 File Offset: 0x00073DD0
		[SecurityCritical]
		internal static bool TryPopCustomWorkItem(IThreadPoolWorkItem workItem)
		{
			return ThreadPoolGlobals.vmTpInitialized && ThreadPoolGlobals.workQueue.LocalFindAndPop(workItem);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00075BE8 File Offset: 0x00073DE8
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(ThreadPoolWorkQueue.allThreadQueues.Current, ThreadPoolGlobals.workQueue.queueTail);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00075C05 File Offset: 0x00073E05
		internal static IEnumerable<IThreadPoolWorkItem> EnumerateQueuedWorkItems(ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues, ThreadPoolWorkQueue.QueueSegment globalQueueTail)
		{
			if (wsQueues != null)
			{
				foreach (ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue in wsQueues)
				{
					if (workStealingQueue != null && workStealingQueue.m_array != null)
					{
						IThreadPoolWorkItem[] items = workStealingQueue.m_array;
						int num;
						for (int i = 0; i < items.Length; i = num + 1)
						{
							IThreadPoolWorkItem threadPoolWorkItem = items[i];
							if (threadPoolWorkItem != null)
							{
								yield return threadPoolWorkItem;
							}
							num = i;
						}
						items = null;
					}
				}
				ThreadPoolWorkQueue.WorkStealingQueue[] array = null;
			}
			if (globalQueueTail != null)
			{
				ThreadPoolWorkQueue.QueueSegment segment;
				for (segment = globalQueueTail; segment != null; segment = segment.Next)
				{
					IThreadPoolWorkItem[] items = segment.nodes;
					int num;
					for (int j = 0; j < items.Length; j = num + 1)
					{
						IThreadPoolWorkItem threadPoolWorkItem2 = items[j];
						if (threadPoolWorkItem2 != null)
						{
							yield return threadPoolWorkItem2;
						}
						num = j;
					}
					items = null;
				}
				segment = null;
			}
			yield break;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00075C1C File Offset: 0x00073E1C
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetLocallyQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(new ThreadPoolWorkQueue.WorkStealingQueue[]
			{
				ThreadPoolWorkQueueThreadLocals.threadLocals.workStealingQueue
			}, null);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00075C37 File Offset: 0x00073E37
		[SecurityCritical]
		internal static IEnumerable<IThreadPoolWorkItem> GetGloballyQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(null, ThreadPoolGlobals.workQueue.queueTail);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00075C4C File Offset: 0x00073E4C
		private static object[] ToObjectArray(IEnumerable<IThreadPoolWorkItem> workitems)
		{
			int num = 0;
			foreach (IThreadPoolWorkItem threadPoolWorkItem in workitems)
			{
				num++;
			}
			object[] array = new object[num];
			num = 0;
			foreach (IThreadPoolWorkItem threadPoolWorkItem2 in workitems)
			{
				if (num < array.Length)
				{
					array[num] = threadPoolWorkItem2;
				}
				num++;
			}
			return array;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00075CDC File Offset: 0x00073EDC
		[SecurityCritical]
		internal static object[] GetQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00075CE8 File Offset: 0x00073EE8
		[SecurityCritical]
		internal static object[] GetGloballyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetGloballyQueuedWorkItems());
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00075CF4 File Offset: 0x00073EF4
		[SecurityCritical]
		internal static object[] GetLocallyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetLocallyQueuedWorkItems());
		}

		// Token: 0x06002053 RID: 8275
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool RequestWorkerThread();

		// Token: 0x06002054 RID: 8276
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool PostQueuedCompletionStatus(NativeOverlapped* overlapped);

		/// <summary>Queues an overlapped I/O operation for execution.</summary>
		/// <param name="overlapped">The <see cref="T:System.Threading.NativeOverlapped" /> structure to queue.</param>
		/// <returns>
		///   <see langword="true" /> if the operation was successfully queued to an I/O completion port; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002055 RID: 8277 RVA: 0x00075D00 File Offset: 0x00073F00
		[SecurityCritical]
		[CLSCompliant(false)]
		public unsafe static bool UnsafeQueueNativeOverlapped(NativeOverlapped* overlapped)
		{
			throw new NotImplementedException("");
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00075D0C File Offset: 0x00073F0C
		[SecurityCritical]
		private static void EnsureVMInitialized()
		{
			if (!ThreadPoolGlobals.vmTpInitialized)
			{
				ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
				ThreadPoolGlobals.vmTpInitialized = true;
			}
		}

		// Token: 0x06002057 RID: 8279
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMinThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x06002058 RID: 8280
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMaxThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x06002059 RID: 8281
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMinThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x0600205A RID: 8282
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMaxThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x0600205B RID: 8283
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAvailableThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x0600205C RID: 8284
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool NotifyWorkItemComplete();

		// Token: 0x0600205D RID: 8285
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportThreadStatus(bool isWorking);

		// Token: 0x0600205E RID: 8286 RVA: 0x00075D29 File Offset: 0x00073F29
		[SecuritySafeCritical]
		internal static void NotifyWorkItemProgress()
		{
			ThreadPool.EnsureVMInitialized();
			ThreadPool.NotifyWorkItemProgressNative();
		}

		// Token: 0x0600205F RID: 8287
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NotifyWorkItemProgressNative();

		// Token: 0x06002060 RID: 8288
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NotifyWorkItemQueued();

		// Token: 0x06002061 RID: 8289 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[SecurityCritical]
		internal static bool IsThreadPoolHosted()
		{
			return false;
		}

		// Token: 0x06002062 RID: 8290
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeVMTp(ref bool enableWorkerTracking);

		/// <summary>Binds an operating system handle to the <see cref="T:System.Threading.ThreadPool" />.</summary>
		/// <param name="osHandle">An <see cref="T:System.IntPtr" /> that holds the handle. The handle must have been opened for overlapped I/O on the unmanaged side.</param>
		/// <returns>
		///   <see langword="true" /> if the handle is bound; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
		// Token: 0x06002063 RID: 8291 RVA: 0x00075D35 File Offset: 0x00073F35
		[Obsolete("ThreadPool.BindHandle(IntPtr) has been deprecated.  Please use ThreadPool.BindHandle(SafeHandle) instead.", false)]
		[SecuritySafeCritical]
		public static bool BindHandle(IntPtr osHandle)
		{
			return ThreadPool.BindIOCompletionCallbackNative(osHandle);
		}

		/// <summary>Binds an operating system handle to the <see cref="T:System.Threading.ThreadPool" />.</summary>
		/// <param name="osHandle">A <see cref="T:System.Runtime.InteropServices.SafeHandle" /> that holds the operating system handle. The handle must have been opened for overlapped I/O on the unmanaged side.</param>
		/// <returns>
		///   <see langword="true" /> if the handle is bound; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="osHandle" /> is <see langword="null" />.</exception>
		// Token: 0x06002064 RID: 8292 RVA: 0x00075D40 File Offset: 0x00073F40
		[SecuritySafeCritical]
		public static bool BindHandle(SafeHandle osHandle)
		{
			if (osHandle == null)
			{
				throw new ArgumentNullException("osHandle");
			}
			bool result = false;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				osHandle.DangerousAddRef(ref flag);
				result = ThreadPool.BindIOCompletionCallbackNative(osHandle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					osHandle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x000040F7 File Offset: 0x000022F7
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityCritical]
		private static bool BindIOCompletionCallbackNative(IntPtr fileHandle)
		{
			return true;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06002066 RID: 8294 RVA: 0x00075D98 File Offset: 0x00073F98
		internal static bool IsThreadPoolThread
		{
			get
			{
				return Thread.CurrentThread.IsThreadPoolThread;
			}
		}

		// Token: 0x020002E7 RID: 743
		[CompilerGenerated]
		private sealed class <>c__DisplayClass17_0<TState>
		{
			// Token: 0x06002067 RID: 8295 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass17_0()
			{
			}

			// Token: 0x06002068 RID: 8296 RVA: 0x00075DA4 File Offset: 0x00073FA4
			internal void <QueueUserWorkItem>b__0(object x)
			{
				this.callBack((TState)((object)x));
			}

			// Token: 0x04001B4C RID: 6988
			public Action<TState> callBack;
		}

		// Token: 0x020002E8 RID: 744
		[CompilerGenerated]
		private sealed class <>c__DisplayClass18_0<TState>
		{
			// Token: 0x06002069 RID: 8297 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c__DisplayClass18_0()
			{
			}

			// Token: 0x0600206A RID: 8298 RVA: 0x00075DB7 File Offset: 0x00073FB7
			internal void <UnsafeQueueUserWorkItem>b__0(object x)
			{
				this.callBack((TState)((object)x));
			}

			// Token: 0x04001B4D RID: 6989
			public Action<TState> callBack;
		}

		// Token: 0x020002E9 RID: 745
		[CompilerGenerated]
		private sealed class <EnumerateQueuedWorkItems>d__23 : IEnumerable<IThreadPoolWorkItem>, IEnumerable, IEnumerator<IThreadPoolWorkItem>, IDisposable, IEnumerator
		{
			// Token: 0x0600206B RID: 8299 RVA: 0x00075DCA File Offset: 0x00073FCA
			[DebuggerHidden]
			public <EnumerateQueuedWorkItems>d__23(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600206C RID: 8300 RVA: 0x00004BF9 File Offset: 0x00002DF9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600206D RID: 8301 RVA: 0x00075DE4 File Offset: 0x00073FE4
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
					this.<>1__state = -1;
					if (wsQueues != null)
					{
						array = wsQueues;
						j = 0;
						goto IL_D4;
					}
					goto IL_EE;
				case 1:
					this.<>1__state = -1;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_14C;
				default:
					return false;
				}
				IL_9F:
				int num = i;
				i = num + 1;
				IL_AF:
				if (i >= items.Length)
				{
					items = null;
				}
				else
				{
					IThreadPoolWorkItem threadPoolWorkItem = items[i];
					if (threadPoolWorkItem != null)
					{
						this.<>2__current = threadPoolWorkItem;
						this.<>1__state = 1;
						return true;
					}
					goto IL_9F;
				}
				IL_C6:
				j++;
				IL_D4:
				if (j >= array.Length)
				{
					array = null;
				}
				else
				{
					ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = array[j];
					if (workStealingQueue != null && workStealingQueue.m_array != null)
					{
						items = workStealingQueue.m_array;
						i = 0;
						goto IL_AF;
					}
					goto IL_C6;
				}
				IL_EE:
				if (globalQueueTail != null)
				{
					segment = globalQueueTail;
					goto IL_186;
				}
				return false;
				IL_14C:
				num = j;
				j = num + 1;
				IL_15C:
				if (j >= items.Length)
				{
					items = null;
					segment = segment.Next;
				}
				else
				{
					IThreadPoolWorkItem threadPoolWorkItem2 = items[j];
					if (threadPoolWorkItem2 != null)
					{
						this.<>2__current = threadPoolWorkItem2;
						this.<>1__state = 2;
						return true;
					}
					goto IL_14C;
				}
				IL_186:
				if (segment != null)
				{
					items = segment.nodes;
					j = 0;
					goto IL_15C;
				}
				segment = null;
				return false;
			}

			// Token: 0x170003D2 RID: 978
			// (get) Token: 0x0600206E RID: 8302 RVA: 0x00075F8A File Offset: 0x0007418A
			IThreadPoolWorkItem IEnumerator<IThreadPoolWorkItem>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600206F RID: 8303 RVA: 0x000472C8 File Offset: 0x000454C8
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06002070 RID: 8304 RVA: 0x00075F8A File Offset: 0x0007418A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06002071 RID: 8305 RVA: 0x00075F94 File Offset: 0x00074194
			[DebuggerHidden]
			IEnumerator<IThreadPoolWorkItem> IEnumerable<IThreadPoolWorkItem>.GetEnumerator()
			{
				ThreadPool.<EnumerateQueuedWorkItems>d__23 <EnumerateQueuedWorkItems>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<EnumerateQueuedWorkItems>d__ = this;
				}
				else
				{
					<EnumerateQueuedWorkItems>d__ = new ThreadPool.<EnumerateQueuedWorkItems>d__23(0);
				}
				<EnumerateQueuedWorkItems>d__.wsQueues = wsQueues;
				<EnumerateQueuedWorkItems>d__.globalQueueTail = globalQueueTail;
				return <EnumerateQueuedWorkItems>d__;
			}

			// Token: 0x06002072 RID: 8306 RVA: 0x00075FE3 File Offset: 0x000741E3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Threading.IThreadPoolWorkItem>.GetEnumerator();
			}

			// Token: 0x04001B4E RID: 6990
			private int <>1__state;

			// Token: 0x04001B4F RID: 6991
			private IThreadPoolWorkItem <>2__current;

			// Token: 0x04001B50 RID: 6992
			private int <>l__initialThreadId;

			// Token: 0x04001B51 RID: 6993
			private ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues;

			// Token: 0x04001B52 RID: 6994
			public ThreadPoolWorkQueue.WorkStealingQueue[] <>3__wsQueues;

			// Token: 0x04001B53 RID: 6995
			private ThreadPoolWorkQueue.QueueSegment globalQueueTail;

			// Token: 0x04001B54 RID: 6996
			public ThreadPoolWorkQueue.QueueSegment <>3__globalQueueTail;

			// Token: 0x04001B55 RID: 6997
			private ThreadPoolWorkQueue.WorkStealingQueue[] <>7__wrap1;

			// Token: 0x04001B56 RID: 6998
			private int <>7__wrap2;

			// Token: 0x04001B57 RID: 6999
			private IThreadPoolWorkItem[] <items>5__4;

			// Token: 0x04001B58 RID: 7000
			private int <i>5__5;

			// Token: 0x04001B59 RID: 7001
			private ThreadPoolWorkQueue.QueueSegment <segment>5__6;
		}
	}
}
