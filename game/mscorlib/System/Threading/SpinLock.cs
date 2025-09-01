using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides a mutual exclusion lock primitive where a thread trying to acquire the lock waits in a loop repeatedly checking until the lock becomes available.</summary>
	// Token: 0x020002BE RID: 702
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SpinLock.SystemThreading_SpinLockDebugView))]
	[DebuggerDisplay("IsHeld = {IsHeld}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct SpinLock
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.SpinLock" /> structure with the option to track thread IDs to improve debugging.</summary>
		/// <param name="enableThreadOwnerTracking">Whether to capture and use thread IDs for debugging purposes.</param>
		// Token: 0x06001E9C RID: 7836 RVA: 0x00071922 File Offset: 0x0006FB22
		public SpinLock(bool enableThreadOwnerTracking)
		{
			this.m_owner = 0;
			if (!enableThreadOwnerTracking)
			{
				this.m_owner |= int.MinValue;
			}
		}

		/// <summary>Acquires the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling Enter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06001E9D RID: 7837 RVA: 0x00071948 File Offset: 0x0006FB48
		public void Enter(ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if (lockTaken || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(-1, ref lockTaken);
			}
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06001E9E RID: 7838 RVA: 0x00071990 File Offset: 0x0006FB90
		public void TryEnter(ref bool lockTaken)
		{
			this.TryEnter(0, ref lockTaken);
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06001E9F RID: 7839 RVA: 0x0007199C File Offset: 0x0006FB9C
		public void TryEnter(TimeSpan timeout, ref bool lockTaken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("The timeout must be a value between -1 and Int32.MaxValue, inclusive."));
			}
			this.TryEnter((int)timeout.TotalMilliseconds, ref lockTaken);
		}

		/// <summary>Attempts to acquire the lock in a reliable manner, such that even if an exception occurs within the method call, <paramref name="lockTaken" /> can be examined reliably to determine whether the lock was acquired.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <param name="lockTaken">True if the lock is acquired; otherwise, false. <paramref name="lockTaken" /> must be initialized to false prior to calling this method.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="lockTaken" /> argument must be initialized to false prior to calling TryEnter.</exception>
		/// <exception cref="T:System.Threading.LockRecursionException">Thread ownership tracking is enabled, and the current thread has already acquired this lock.</exception>
		// Token: 0x06001EA0 RID: 7840 RVA: 0x000719EC File Offset: 0x0006FBEC
		public void TryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if ((millisecondsTimeout < -1 | lockTaken) || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(millisecondsTimeout, ref lockTaken);
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00071A3C File Offset: 0x0006FC3C
		private void ContinueTryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.EndCriticalRegion();
			if (lockTaken)
			{
				lockTaken = false;
				throw new ArgumentException(Environment.GetResourceString("The tookLock argument must be set to false before calling this method."));
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("The timeout must be a value between -1 and Int32.MaxValue, inclusive."));
			}
			uint startTime = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout != 0)
			{
				startTime = TimeoutHelper.GetTime();
			}
			if (this.IsThreadOwnerTrackingEnabled)
			{
				this.ContinueTryEnterWithThreadTracking(millisecondsTimeout, startTime, ref lockTaken);
				return;
			}
			int num = int.MaxValue;
			int owner = this.m_owner;
			if ((owner & 1) == 0)
			{
				Thread.BeginCriticalRegion();
				if (Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) == owner)
				{
					return;
				}
				Thread.EndCriticalRegion();
			}
			else if ((owner & 2147483646) != SpinLock.MAXIMUM_WAITERS)
			{
				num = (Interlocked.Add(ref this.m_owner, 2) & 2147483646) >> 1;
			}
			if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0))
			{
				this.DecrementWaiters();
				return;
			}
			int processorCount = PlatformHelper.ProcessorCount;
			if (num < processorCount)
			{
				int num2 = 1;
				for (int i = 1; i <= num * 100; i++)
				{
					Thread.SpinWait((num + i) * 100 * num2);
					if (num2 < processorCount)
					{
						num2++;
					}
					owner = this.m_owner;
					if ((owner & 1) == 0)
					{
						Thread.BeginCriticalRegion();
						int value = ((owner & 2147483646) == 0) ? (owner | 1) : (owner - 2 | 1);
						if (Interlocked.CompareExchange(ref this.m_owner, value, owner, ref lockTaken) == owner)
						{
							return;
						}
						Thread.EndCriticalRegion();
					}
				}
			}
			if (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0)
			{
				this.DecrementWaiters();
				return;
			}
			int num3 = 0;
			for (;;)
			{
				owner = this.m_owner;
				if ((owner & 1) == 0)
				{
					Thread.BeginCriticalRegion();
					int value2 = ((owner & 2147483646) == 0) ? (owner | 1) : (owner - 2 | 1);
					if (Interlocked.CompareExchange(ref this.m_owner, value2, owner, ref lockTaken) == owner)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (num3 % 40 == 0)
				{
					Thread.Sleep(1);
				}
				else if (num3 % 10 == 0)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
				if (num3 % 10 == 0 && millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0)
				{
					goto Block_25;
				}
				num3++;
			}
			return;
			Block_25:
			this.DecrementWaiters();
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00071C30 File Offset: 0x0006FE30
		private void DecrementWaiters()
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int owner = this.m_owner;
				if ((owner & 2147483646) == 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_owner, owner - 2, owner) == owner)
				{
					return;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00071C74 File Offset: 0x0006FE74
		private void ContinueTryEnterWithThreadTracking(int millisecondsTimeout, uint startTime, ref bool lockTaken)
		{
			int num = 0;
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (this.m_owner == managedThreadId)
			{
				throw new LockRecursionException(Environment.GetResourceString("The calling thread already holds the lock."));
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				spinWait.SpinOnce();
				if (this.m_owner == num)
				{
					Thread.BeginCriticalRegion();
					if (Interlocked.CompareExchange(ref this.m_owner, managedThreadId, num, ref lockTaken) == num)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0))
				{
					return;
				}
			}
		}

		/// <summary>Releases the lock.</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">Thread ownership tracking is enabled, and the current thread is not the owner of this lock.</exception>
		// Token: 0x06001EA4 RID: 7844 RVA: 0x00071CF9 File Offset: 0x0006FEF9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Exit()
		{
			if ((this.m_owner & -2147483648) == 0)
			{
				this.ExitSlowPath(true);
			}
			else
			{
				Interlocked.Decrement(ref this.m_owner);
			}
			Thread.EndCriticalRegion();
		}

		/// <summary>Releases the lock.</summary>
		/// <param name="useMemoryBarrier">A Boolean value that indicates whether a memory fence should be issued in order to immediately publish the exit operation to other threads.</param>
		/// <exception cref="T:System.Threading.SynchronizationLockException">Thread ownership tracking is enabled, and the current thread is not the owner of this lock.</exception>
		// Token: 0x06001EA5 RID: 7845 RVA: 0x00071D28 File Offset: 0x0006FF28
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Exit(bool useMemoryBarrier)
		{
			if ((this.m_owner & -2147483648) != 0 && !useMemoryBarrier)
			{
				int owner = this.m_owner;
				this.m_owner = (owner & -2);
			}
			else
			{
				this.ExitSlowPath(useMemoryBarrier);
			}
			Thread.EndCriticalRegion();
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00071D6C File Offset: 0x0006FF6C
		private void ExitSlowPath(bool useMemoryBarrier)
		{
			bool flag = (this.m_owner & int.MinValue) == 0;
			if (flag && !this.IsHeldByCurrentThread)
			{
				throw new SynchronizationLockException(Environment.GetResourceString("The calling thread does not hold the lock."));
			}
			if (useMemoryBarrier)
			{
				if (flag)
				{
					Interlocked.Exchange(ref this.m_owner, 0);
					return;
				}
				Interlocked.Decrement(ref this.m_owner);
				return;
			}
			else
			{
				if (flag)
				{
					this.m_owner = 0;
					return;
				}
				int owner = this.m_owner;
				this.m_owner = (owner & -2);
				return;
			}
		}

		/// <summary>Gets whether the lock is currently held by any thread.</summary>
		/// <returns>true if the lock is currently held by any thread; otherwise false.</returns>
		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x00071DE9 File Offset: 0x0006FFE9
		public bool IsHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				if (this.IsThreadOwnerTrackingEnabled)
				{
					return this.m_owner != 0;
				}
				return (this.m_owner & 1) != 0;
			}
		}

		/// <summary>Gets whether the lock is held by the current thread.</summary>
		/// <returns>true if the lock is held by the current thread; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">Thread ownership tracking is disabled.</exception>
		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x00071E0C File Offset: 0x0007000C
		public bool IsHeldByCurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				if (!this.IsThreadOwnerTrackingEnabled)
				{
					throw new InvalidOperationException(Environment.GetResourceString("Thread tracking is disabled."));
				}
				return (this.m_owner & int.MaxValue) == Thread.CurrentThread.ManagedThreadId;
			}
		}

		/// <summary>Gets whether thread ownership tracking is enabled for this instance.</summary>
		/// <returns>true if thread ownership tracking is enabled for this instance; otherwise false.</returns>
		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x00071E40 File Offset: 0x00070040
		public bool IsThreadOwnerTrackingEnabled
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (this.m_owner & int.MinValue) == 0;
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00071E53 File Offset: 0x00070053
		// Note: this type is marked as 'beforefieldinit'.
		static SpinLock()
		{
		}

		// Token: 0x04001AD0 RID: 6864
		private volatile int m_owner;

		// Token: 0x04001AD1 RID: 6865
		private const int SPINNING_FACTOR = 100;

		// Token: 0x04001AD2 RID: 6866
		private const int SLEEP_ONE_FREQUENCY = 40;

		// Token: 0x04001AD3 RID: 6867
		private const int SLEEP_ZERO_FREQUENCY = 10;

		// Token: 0x04001AD4 RID: 6868
		private const int TIMEOUT_CHECK_FREQUENCY = 10;

		// Token: 0x04001AD5 RID: 6869
		private const int LOCK_ID_DISABLE_MASK = -2147483648;

		// Token: 0x04001AD6 RID: 6870
		private const int LOCK_ANONYMOUS_OWNED = 1;

		// Token: 0x04001AD7 RID: 6871
		private const int WAITERS_MASK = 2147483646;

		// Token: 0x04001AD8 RID: 6872
		private const int ID_DISABLED_AND_ANONYMOUS_OWNED = -2147483647;

		// Token: 0x04001AD9 RID: 6873
		private const int LOCK_UNOWNED = 0;

		// Token: 0x04001ADA RID: 6874
		private static int MAXIMUM_WAITERS = 2147483646;

		// Token: 0x020002BF RID: 703
		internal class SystemThreading_SpinLockDebugView
		{
			// Token: 0x06001EAB RID: 7851 RVA: 0x00071E5F File Offset: 0x0007005F
			public SystemThreading_SpinLockDebugView(SpinLock spinLock)
			{
				this.m_spinLock = spinLock;
			}

			// Token: 0x1700039D RID: 925
			// (get) Token: 0x06001EAC RID: 7852 RVA: 0x00071E70 File Offset: 0x00070070
			public bool? IsHeldByCurrentThread
			{
				get
				{
					bool? result;
					try
					{
						result = new bool?(this.m_spinLock.IsHeldByCurrentThread);
					}
					catch (InvalidOperationException)
					{
						result = null;
					}
					return result;
				}
			}

			// Token: 0x1700039E RID: 926
			// (get) Token: 0x06001EAD RID: 7853 RVA: 0x00071EB0 File Offset: 0x000700B0
			public int? OwnerThreadID
			{
				get
				{
					if (this.m_spinLock.IsThreadOwnerTrackingEnabled)
					{
						return new int?(this.m_spinLock.m_owner);
					}
					return null;
				}
			}

			// Token: 0x1700039F RID: 927
			// (get) Token: 0x06001EAE RID: 7854 RVA: 0x00071EE6 File Offset: 0x000700E6
			public bool IsHeld
			{
				get
				{
					return this.m_spinLock.IsHeld;
				}
			}

			// Token: 0x04001ADB RID: 6875
			private SpinLock m_spinLock;
		}
	}
}
