using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Defines a lock that supports single writers and multiple readers.</summary>
	// Token: 0x020002F6 RID: 758
	[ComVisible(true)]
	public sealed class ReaderWriterLock : CriticalFinalizerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLock" /> class.</summary>
		// Token: 0x06002107 RID: 8455 RVA: 0x00077168 File Offset: 0x00075368
		public ReaderWriterLock()
		{
			this.writer_queue = new LockQueue(this);
			this.reader_locks = new Hashtable();
			GC.SuppressFinalize(this);
		}

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see cref="T:System.Threading.ReaderWriterLock" /> object.</summary>
		// Token: 0x06002108 RID: 8456 RVA: 0x00077194 File Offset: 0x00075394
		~ReaderWriterLock()
		{
		}

		/// <summary>Gets a value indicating whether the current thread holds a reader lock.</summary>
		/// <returns>
		///   <see langword="true" /> if the current thread holds a reader lock; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x000771BC File Offset: 0x000753BC
		public bool IsReaderLockHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				bool result;
				lock (this)
				{
					result = this.reader_locks.ContainsKey(Thread.CurrentThreadId);
				}
				return result;
			}
		}

		/// <summary>Gets a value indicating whether the current thread holds the writer lock.</summary>
		/// <returns>
		///   <see langword="true" /> if the current thread holds the writer lock; otherwise, <see langword="false" />.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600210A RID: 8458 RVA: 0x00077208 File Offset: 0x00075408
		public bool IsWriterLockHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				bool result;
				lock (this)
				{
					result = (this.state < 0 && Thread.CurrentThreadId == this.writer_lock_owner);
				}
				return result;
			}
		}

		/// <summary>Gets the current sequence number.</summary>
		/// <returns>The current sequence number.</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x00077258 File Offset: 0x00075458
		public int WriterSeqNum
		{
			get
			{
				int result;
				lock (this)
				{
					result = this.seq_num;
				}
				return result;
			}
		}

		/// <summary>Acquires a reader lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds.</param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted.</exception>
		// Token: 0x0600210C RID: 8460 RVA: 0x00077298 File Offset: 0x00075498
		public void AcquireReaderLock(int millisecondsTimeout)
		{
			this.AcquireReaderLock(millisecondsTimeout, 1);
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x000772A4 File Offset: 0x000754A4
		private void AcquireReaderLock(int millisecondsTimeout, int initialLockCount)
		{
			lock (this)
			{
				if (this.HasWriterLock())
				{
					this.AcquireWriterLock(millisecondsTimeout, initialLockCount);
				}
				else
				{
					object obj = this.reader_locks[Thread.CurrentThreadId];
					if (obj == null)
					{
						this.readers++;
						try
						{
							if (this.state < 0 || !this.writer_queue.IsEmpty)
							{
								while (Monitor.Wait(this, millisecondsTimeout))
								{
									if (this.state >= 0)
									{
										goto IL_7B;
									}
								}
								throw new ApplicationException("Timeout expired");
							}
							IL_7B:;
						}
						finally
						{
							this.readers--;
						}
						this.reader_locks[Thread.CurrentThreadId] = initialLockCount;
						this.state += initialLockCount;
					}
					else
					{
						this.reader_locks[Thread.CurrentThreadId] = (int)obj + 1;
						this.state++;
					}
				}
			}
		}

		/// <summary>Acquires a reader lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">A <see langword="TimeSpan" /> specifying the time-out period.</param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds.</exception>
		// Token: 0x0600210E RID: 8462 RVA: 0x000773C0 File Offset: 0x000755C0
		public void AcquireReaderLock(TimeSpan timeout)
		{
			int millisecondsTimeout = this.CheckTimeout(timeout);
			this.AcquireReaderLock(millisecondsTimeout, 1);
		}

		/// <summary>Acquires the writer lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds.</param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted.</exception>
		// Token: 0x0600210F RID: 8463 RVA: 0x000773DD File Offset: 0x000755DD
		public void AcquireWriterLock(int millisecondsTimeout)
		{
			this.AcquireWriterLock(millisecondsTimeout, 1);
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x000773E8 File Offset: 0x000755E8
		private void AcquireWriterLock(int millisecondsTimeout, int initialLockCount)
		{
			lock (this)
			{
				if (this.HasWriterLock())
				{
					this.state--;
				}
				else
				{
					if (this.state != 0 || !this.writer_queue.IsEmpty)
					{
						while (this.writer_queue.Wait(millisecondsTimeout))
						{
							if (this.state == 0)
							{
								goto IL_5A;
							}
						}
						throw new ApplicationException("Timeout expired");
					}
					IL_5A:
					this.state = -initialLockCount;
					this.writer_lock_owner = Thread.CurrentThreadId;
					this.seq_num++;
				}
			}
		}

		/// <summary>Acquires the writer lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">The <see langword="TimeSpan" /> specifying the time-out period.</param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds.</exception>
		// Token: 0x06002111 RID: 8465 RVA: 0x0007748C File Offset: 0x0007568C
		public void AcquireWriterLock(TimeSpan timeout)
		{
			int millisecondsTimeout = this.CheckTimeout(timeout);
			this.AcquireWriterLock(millisecondsTimeout, 1);
		}

		/// <summary>Indicates whether the writer lock has been granted to any thread since the sequence number was obtained.</summary>
		/// <param name="seqNum">The sequence number.</param>
		/// <returns>
		///   <see langword="true" /> if the writer lock has been granted to any thread since the sequence number was obtained; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002112 RID: 8466 RVA: 0x000774AC File Offset: 0x000756AC
		public bool AnyWritersSince(int seqNum)
		{
			bool result;
			lock (this)
			{
				result = (this.seq_num > seqNum);
			}
			return result;
		}

		/// <summary>Restores the lock status of the thread to what it was before <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" /> was called.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" />.</param>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock.</exception>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer.</exception>
		// Token: 0x06002113 RID: 8467 RVA: 0x000774EC File Offset: 0x000756EC
		public void DowngradeFromWriterLock(ref LockCookie lockCookie)
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					throw new ApplicationException("The thread does not have the writer lock.");
				}
				if (lockCookie.WriterLocks != 0)
				{
					this.state++;
				}
				else
				{
					this.state = lockCookie.ReaderLocks;
					this.reader_locks[Thread.CurrentThreadId] = this.state;
					if (this.readers > 0)
					{
						Monitor.PulseAll(this);
					}
				}
			}
		}

		/// <summary>Releases the lock, regardless of the number of times the thread acquired the lock.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value representing the released lock.</returns>
		// Token: 0x06002114 RID: 8468 RVA: 0x00077588 File Offset: 0x00075788
		public LockCookie ReleaseLock()
		{
			LockCookie lockCookie;
			lock (this)
			{
				lockCookie = this.GetLockCookie();
				if (lockCookie.WriterLocks != 0)
				{
					this.ReleaseWriterLock(lockCookie.WriterLocks);
				}
				else if (lockCookie.ReaderLocks != 0)
				{
					this.ReleaseReaderLock(lockCookie.ReaderLocks, lockCookie.ReaderLocks);
				}
			}
			return lockCookie;
		}

		/// <summary>Decrements the lock count.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have any reader or writer locks.</exception>
		// Token: 0x06002115 RID: 8469 RVA: 0x000775F8 File Offset: 0x000757F8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseReaderLock()
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					if (this.state > 0)
					{
						object obj = this.reader_locks[Thread.CurrentThreadId];
						if (obj != null)
						{
							this.ReleaseReaderLock((int)obj, 1);
							return;
						}
					}
					throw new ApplicationException("The thread does not have any reader or writer locks.");
				}
				this.ReleaseWriterLock();
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x00077678 File Offset: 0x00075878
		private void ReleaseReaderLock(int currentCount, int releaseCount)
		{
			int num = currentCount - releaseCount;
			if (num == 0)
			{
				this.reader_locks.Remove(Thread.CurrentThreadId);
			}
			else
			{
				this.reader_locks[Thread.CurrentThreadId] = num;
			}
			this.state -= releaseCount;
			if (this.state == 0 && !this.writer_queue.IsEmpty)
			{
				this.writer_queue.Pulse();
			}
		}

		/// <summary>Decrements the lock count on the writer lock.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock.</exception>
		// Token: 0x06002117 RID: 8471 RVA: 0x000776EC File Offset: 0x000758EC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseWriterLock()
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					throw new ApplicationException("The thread does not have the writer lock.");
				}
				this.ReleaseWriterLock(1);
			}
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0007773C File Offset: 0x0007593C
		private void ReleaseWriterLock(int releaseCount)
		{
			this.state += releaseCount;
			if (this.state == 0)
			{
				if (this.readers > 0)
				{
					Monitor.PulseAll(this);
					return;
				}
				if (!this.writer_queue.IsEmpty)
				{
					this.writer_queue.Pulse();
				}
			}
		}

		/// <summary>Restores the lock status of the thread to what it was before calling <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.</param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer.</exception>
		// Token: 0x06002119 RID: 8473 RVA: 0x0007777C File Offset: 0x0007597C
		public void RestoreLock(ref LockCookie lockCookie)
		{
			lock (this)
			{
				if (lockCookie.WriterLocks != 0)
				{
					this.AcquireWriterLock(-1, lockCookie.WriterLocks);
				}
				else if (lockCookie.ReaderLocks != 0)
				{
					this.AcquireReaderLock(-1, lockCookie.ReaderLocks);
				}
			}
		}

		/// <summary>Upgrades a reader lock to the writer lock, using an <see langword="Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds.</param>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted.</exception>
		// Token: 0x0600211A RID: 8474 RVA: 0x000777E0 File Offset: 0x000759E0
		public LockCookie UpgradeToWriterLock(int millisecondsTimeout)
		{
			LockCookie lockCookie;
			lock (this)
			{
				lockCookie = this.GetLockCookie();
				if (lockCookie.WriterLocks != 0)
				{
					this.state--;
					return lockCookie;
				}
				if (lockCookie.ReaderLocks != 0)
				{
					this.ReleaseReaderLock(lockCookie.ReaderLocks, lockCookie.ReaderLocks);
				}
			}
			this.AcquireWriterLock(millisecondsTimeout);
			return lockCookie;
		}

		/// <summary>Upgrades a reader lock to the writer lock, using a <see langword="TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">The <see langword="TimeSpan" /> specifying the time-out period.</param>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds.</exception>
		// Token: 0x0600211B RID: 8475 RVA: 0x0007785C File Offset: 0x00075A5C
		public LockCookie UpgradeToWriterLock(TimeSpan timeout)
		{
			int millisecondsTimeout = this.CheckTimeout(timeout);
			return this.UpgradeToWriterLock(millisecondsTimeout);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x00077878 File Offset: 0x00075A78
		private LockCookie GetLockCookie()
		{
			LockCookie result = new LockCookie(Thread.CurrentThreadId);
			if (this.HasWriterLock())
			{
				result.WriterLocks = -this.state;
			}
			else
			{
				object obj = this.reader_locks[Thread.CurrentThreadId];
				if (obj != null)
				{
					result.ReaderLocks = (int)obj;
				}
			}
			return result;
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000778D0 File Offset: 0x00075AD0
		private bool HasWriterLock()
		{
			return this.state < 0 && Thread.CurrentThreadId == this.writer_lock_owner;
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000778EA File Offset: 0x00075AEA
		private int CheckTimeout(TimeSpan timeout)
		{
			int num = (int)timeout.TotalMilliseconds;
			if (num < -1)
			{
				throw new ArgumentOutOfRangeException("timeout", "Number must be either non-negative or -1");
			}
			return num;
		}

		// Token: 0x04001B77 RID: 7031
		private int seq_num = 1;

		// Token: 0x04001B78 RID: 7032
		private int state;

		// Token: 0x04001B79 RID: 7033
		private int readers;

		// Token: 0x04001B7A RID: 7034
		private int writer_lock_owner;

		// Token: 0x04001B7B RID: 7035
		private LockQueue writer_queue;

		// Token: 0x04001B7C RID: 7036
		private Hashtable reader_locks;
	}
}
