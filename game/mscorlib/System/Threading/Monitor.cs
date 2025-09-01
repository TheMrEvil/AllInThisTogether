using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;
using System.Security;

namespace System.Threading
{
	/// <summary>Provides a mechanism that synchronizes access to objects.</summary>
	// Token: 0x020002CF RID: 719
	public static class Monitor
	{
		/// <summary>Acquires an exclusive lock on the specified object.</summary>
		/// <param name="obj">The object on which to acquire the monitor lock.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F26 RID: 7974
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Enter(object obj);

		/// <summary>Acquires an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken.</summary>
		/// <param name="obj">The object on which to wait.</param>
		/// <param name="lockTaken">The result of the attempt to acquire the lock, passed by reference. The input must be <see langword="false" />. The output is <see langword="true" /> if the lock is acquired; otherwise, the output is <see langword="false" />. The output is set even if an exception occurs during the attempt to acquire the lock.  
		///  Note   If no exception occurs, the output of this method is always <see langword="true" />.</param>
		/// <exception cref="T:System.ArgumentException">The input to <paramref name="lockTaken" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F27 RID: 7975 RVA: 0x00073769 File Offset: 0x00071969
		public static void Enter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnter(obj, ref lockTaken);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0007377B File Offset: 0x0007197B
		private static void ThrowLockTakenException()
		{
			throw new ArgumentException(Environment.GetResourceString("Argument must be initialized to false"), "lockTaken");
		}

		/// <summary>Releases an exclusive lock on the specified object.</summary>
		/// <param name="obj">The object on which to release the lock.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread does not own the lock for the specified object.</exception>
		// Token: 0x06001F29 RID: 7977
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Exit(object obj);

		/// <summary>Attempts to acquire an exclusive lock on the specified object.</summary>
		/// <param name="obj">The object on which to acquire the lock.</param>
		/// <returns>
		///   <see langword="true" /> if the current thread acquires the lock; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F2A RID: 7978 RVA: 0x00073794 File Offset: 0x00071994
		public static bool TryEnter(object obj)
		{
			bool result = false;
			Monitor.TryEnter(obj, 0, ref result);
			return result;
		}

		/// <summary>Attempts to acquire an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken.</summary>
		/// <param name="obj">The object on which to acquire the lock.</param>
		/// <param name="lockTaken">The result of the attempt to acquire the lock, passed by reference. The input must be <see langword="false" />. The output is <see langword="true" /> if the lock is acquired; otherwise, the output is <see langword="false" />. The output is set even if an exception occurs during the attempt to acquire the lock.</param>
		/// <exception cref="T:System.ArgumentException">The input to <paramref name="lockTaken" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06001F2B RID: 7979 RVA: 0x000737AD File Offset: 0x000719AD
		public static void TryEnter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, 0, ref lockTaken);
		}

		/// <summary>Attempts, for the specified number of milliseconds, to acquire an exclusive lock on the specified object.</summary>
		/// <param name="obj">The object on which to acquire the lock.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for the lock.</param>
		/// <returns>
		///   <see langword="true" /> if the current thread acquires the lock; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is negative, and not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x06001F2C RID: 7980 RVA: 0x000737C0 File Offset: 0x000719C0
		public static bool TryEnter(object obj, int millisecondsTimeout)
		{
			bool result = false;
			Monitor.TryEnter(obj, millisecondsTimeout, ref result);
			return result;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000737DC File Offset: 0x000719DC
		private static int MillisecondsTimeoutFromTimeSpan(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return (int)num;
		}

		/// <summary>Attempts, for the specified amount of time, to acquire an exclusive lock on the specified object.</summary>
		/// <param name="obj">The object on which to acquire the lock.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> representing the amount of time to wait for the lock. A value of -1 millisecond specifies an infinite wait.</param>
		/// <returns>
		///   <see langword="true" /> if the current thread acquires the lock; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> in milliseconds is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1 millisecond), or is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001F2E RID: 7982 RVA: 0x00073817 File Offset: 0x00071A17
		public static bool TryEnter(object obj, TimeSpan timeout)
		{
			return Monitor.TryEnter(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout));
		}

		/// <summary>Attempts, for the specified number of milliseconds, to acquire an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken.</summary>
		/// <param name="obj">The object on which to acquire the lock.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait for the lock.</param>
		/// <param name="lockTaken">The result of the attempt to acquire the lock, passed by reference. The input must be <see langword="false" />. The output is <see langword="true" /> if the lock is acquired; otherwise, the output is <see langword="false" />. The output is set even if an exception occurs during the attempt to acquire the lock.</param>
		/// <exception cref="T:System.ArgumentException">The input to <paramref name="lockTaken" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is negative, and not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x06001F2F RID: 7983 RVA: 0x00073825 File Offset: 0x00071A25
		public static void TryEnter(object obj, int millisecondsTimeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, millisecondsTimeout, ref lockTaken);
		}

		/// <summary>Attempts, for the specified amount of time, to acquire an exclusive lock on the specified object, and atomically sets a value that indicates whether the lock was taken.</summary>
		/// <param name="obj">The object on which to acquire the lock.</param>
		/// <param name="timeout">The amount of time to wait for the lock. A value of -1 millisecond specifies an infinite wait.</param>
		/// <param name="lockTaken">The result of the attempt to acquire the lock, passed by reference. The input must be <see langword="false" />. The output is <see langword="true" /> if the lock is acquired; otherwise, the output is <see langword="false" />. The output is set even if an exception occurs during the attempt to acquire the lock.</param>
		/// <exception cref="T:System.ArgumentException">The input to <paramref name="lockTaken" /> is <see langword="true" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> in milliseconds is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1 millisecond), or is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001F30 RID: 7984 RVA: 0x00073838 File Offset: 0x00071A38
		public static void TryEnter(object obj, TimeSpan timeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), ref lockTaken);
		}

		/// <summary>Determines whether the current thread holds the lock on the specified object.</summary>
		/// <param name="obj">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the current thread holds the lock on <paramref name="obj" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06001F31 RID: 7985 RVA: 0x00073850 File Offset: 0x00071A50
		[SecuritySafeCritical]
		public static bool IsEntered(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.IsEnteredNative(obj);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue. This method also specifies whether the synchronization domain for the context (if in a synchronized context) is exited before the wait and reacquired afterward.</summary>
		/// <param name="obj">The object on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait before the thread enters the ready queue.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit and reacquire the synchronization domain for the context (if in a synchronized context) before the wait; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the lock was reacquired before the specified time elapsed; <see langword="false" /> if the lock was reacquired after the specified time elapsed. The method does not return until the lock is reacquired.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">
		///   <see langword="Wait" /> is not invoked from within a synchronized block of code.</exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes <see langword="Wait" /> is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="millisecondsTimeout" /> parameter is negative, and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x06001F32 RID: 7986 RVA: 0x00073866 File Offset: 0x00071A66
		[SecuritySafeCritical]
		public static bool Wait(object obj, int millisecondsTimeout, bool exitContext)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.ObjWait(exitContext, millisecondsTimeout, obj);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue. Optionally exits the synchronization domain for the synchronized context before the wait and reacquires the domain afterward.</summary>
		/// <param name="obj">The object on which to wait.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> representing the amount of time to wait before the thread enters the ready queue.</param>
		/// <param name="exitContext">
		///   <see langword="true" /> to exit and reacquire the synchronization domain for the context (if in a synchronized context) before the wait; otherwise, <see langword="false" />.</param>
		/// <returns>
		///   <see langword="true" /> if the lock was reacquired before the specified time elapsed; <see langword="false" /> if the lock was reacquired after the specified time elapsed. The method does not return until the lock is reacquired.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">
		///   <see langword="Wait" /> is not invoked from within a synchronized block of code.</exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes Wait is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is negative and does not represent <see cref="F:System.Threading.Timeout.Infinite" /> (-1 millisecond), or is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001F33 RID: 7987 RVA: 0x0007387E File Offset: 0x00071A7E
		public static bool Wait(object obj, TimeSpan timeout, bool exitContext)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), exitContext);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue.</summary>
		/// <param name="obj">The object on which to wait.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait before the thread enters the ready queue.</param>
		/// <returns>
		///   <see langword="true" /> if the lock was reacquired before the specified time elapsed; <see langword="false" /> if the lock was reacquired after the specified time elapsed. The method does not return until the lock is reacquired.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object.</exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes <see langword="Wait" /> is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="millisecondsTimeout" /> parameter is negative, and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x06001F34 RID: 7988 RVA: 0x0007388D File Offset: 0x00071A8D
		public static bool Wait(object obj, int millisecondsTimeout)
		{
			return Monitor.Wait(obj, millisecondsTimeout, false);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock. If the specified time-out interval elapses, the thread enters the ready queue.</summary>
		/// <param name="obj">The object on which to wait.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> representing the amount of time to wait before the thread enters the ready queue.</param>
		/// <returns>
		///   <see langword="true" /> if the lock was reacquired before the specified time elapsed; <see langword="false" /> if the lock was reacquired after the specified time elapsed. The method does not return until the lock is reacquired.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object.</exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes <see langword="Wait" /> is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="timeout" /> parameter in milliseconds is negative and does not represent <see cref="F:System.Threading.Timeout.Infinite" /> (-1 millisecond), or is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001F35 RID: 7989 RVA: 0x00073897 File Offset: 0x00071A97
		public static bool Wait(object obj, TimeSpan timeout)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), false);
		}

		/// <summary>Releases the lock on an object and blocks the current thread until it reacquires the lock.</summary>
		/// <param name="obj">The object on which to wait.</param>
		/// <returns>
		///   <see langword="true" /> if the call returned because the caller reacquired the lock for the specified object. This method does not return if the lock is not reacquired.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object.</exception>
		/// <exception cref="T:System.Threading.ThreadInterruptedException">The thread that invokes <see langword="Wait" /> is later interrupted from the waiting state. This happens when another thread calls this thread's <see cref="M:System.Threading.Thread.Interrupt" /> method.</exception>
		// Token: 0x06001F36 RID: 7990 RVA: 0x000738A6 File Offset: 0x00071AA6
		public static bool Wait(object obj)
		{
			return Monitor.Wait(obj, -1, false);
		}

		/// <summary>Notifies a thread in the waiting queue of a change in the locked object's state.</summary>
		/// <param name="obj">The object a thread is waiting for.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object.</exception>
		// Token: 0x06001F37 RID: 7991 RVA: 0x000738B0 File Offset: 0x00071AB0
		[SecuritySafeCritical]
		public static void Pulse(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulse(obj);
		}

		/// <summary>Notifies all waiting threads of a change in the object's state.</summary>
		/// <param name="obj">The object that sends the pulse.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The calling thread does not own the lock for the specified object.</exception>
		// Token: 0x06001F38 RID: 7992 RVA: 0x000738C6 File Offset: 0x00071AC6
		[SecuritySafeCritical]
		public static void PulseAll(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulseAll(obj);
		}

		// Token: 0x06001F39 RID: 7993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Monitor_test_synchronised(object obj);

		// Token: 0x06001F3A RID: 7994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Monitor_pulse(object obj);

		// Token: 0x06001F3B RID: 7995 RVA: 0x000738DC File Offset: 0x00071ADC
		private static void ObjPulse(object obj)
		{
			if (!Monitor.Monitor_test_synchronised(obj))
			{
				throw new SynchronizationLockException("Object is not synchronized");
			}
			Monitor.Monitor_pulse(obj);
		}

		// Token: 0x06001F3C RID: 7996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Monitor_pulse_all(object obj);

		// Token: 0x06001F3D RID: 7997 RVA: 0x000738F7 File Offset: 0x00071AF7
		private static void ObjPulseAll(object obj)
		{
			if (!Monitor.Monitor_test_synchronised(obj))
			{
				throw new SynchronizationLockException("Object is not synchronized");
			}
			Monitor.Monitor_pulse_all(obj);
		}

		// Token: 0x06001F3E RID: 7998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Monitor_wait(object obj, int ms);

		// Token: 0x06001F3F RID: 7999 RVA: 0x00073914 File Offset: 0x00071B14
		private static bool ObjWait(bool exitContext, int millisecondsTimeout, object obj)
		{
			if (millisecondsTimeout < 0 && millisecondsTimeout != -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!Monitor.Monitor_test_synchronised(obj))
			{
				throw new SynchronizationLockException("Object is not synchronized");
			}
			bool result;
			try
			{
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				result = Monitor.Monitor_wait(obj, millisecondsTimeout);
			}
			finally
			{
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return result;
		}

		// Token: 0x06001F40 RID: 8000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void try_enter_with_atomic_var(object obj, int millisecondsTimeout, ref bool lockTaken);

		// Token: 0x06001F41 RID: 8001 RVA: 0x00073974 File Offset: 0x00071B74
		private static void ReliableEnterTimeout(object obj, int timeout, ref bool lockTaken)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (timeout < 0 && timeout != -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			Monitor.try_enter_with_atomic_var(obj, timeout, ref lockTaken);
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0007399F File Offset: 0x00071B9F
		private static void ReliableEnter(object obj, ref bool lockTaken)
		{
			Monitor.ReliableEnterTimeout(obj, -1, ref lockTaken);
		}

		// Token: 0x06001F43 RID: 8003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Monitor_test_owner(object obj);

		// Token: 0x06001F44 RID: 8004 RVA: 0x000739A9 File Offset: 0x00071BA9
		private static bool IsEnteredNative(object obj)
		{
			return Monitor.Monitor_test_owner(obj);
		}
	}
}
