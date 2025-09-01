using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Represents a lock that is used to manage access to a resource, allowing multiple threads for reading or exclusive access for writing.</summary>
	// Token: 0x02000364 RID: 868
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class ReaderWriterLockSlim : IDisposable
	{
		// Token: 0x06001A6B RID: 6763 RVA: 0x0005901C File Offset: 0x0005721C
		private void InitializeThreadCounts()
		{
			this.upgradeLockOwnerId = -1;
			this.writeLockOwnerId = -1;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class with default property values.</summary>
		// Token: 0x06001A6C RID: 6764 RVA: 0x0005902C File Offset: 0x0005722C
		public ReaderWriterLockSlim() : this(LockRecursionPolicy.NoRecursion)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class, specifying the lock recursion policy.</summary>
		/// <param name="recursionPolicy">One of the enumeration values that specifies the lock recursion policy.</param>
		// Token: 0x06001A6D RID: 6765 RVA: 0x00059035 File Offset: 0x00057235
		public ReaderWriterLockSlim(LockRecursionPolicy recursionPolicy)
		{
			if (recursionPolicy == LockRecursionPolicy.SupportsRecursion)
			{
				this.fIsReentrant = true;
			}
			this.InitializeThreadCounts();
			this.fNoWaiters = true;
			this.lockID = Interlocked.Increment(ref ReaderWriterLockSlim.s_nextLockID);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x00059065 File Offset: 0x00057265
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsRWEntryEmpty(ReaderWriterCount rwc)
		{
			return rwc.lockID == 0L || (rwc.readercount == 0 && rwc.writercount == 0 && rwc.upgradecount == 0);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0005908C File Offset: 0x0005728C
		private bool IsRwHashEntryChanged(ReaderWriterCount lrwc)
		{
			return lrwc.lockID != this.lockID;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000590A0 File Offset: 0x000572A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ReaderWriterCount GetThreadRWCount(bool dontAllocate)
		{
			ReaderWriterCount next = ReaderWriterLockSlim.t_rwc;
			ReaderWriterCount readerWriterCount = null;
			while (next != null)
			{
				if (next.lockID == this.lockID)
				{
					return next;
				}
				if (!dontAllocate && readerWriterCount == null && ReaderWriterLockSlim.IsRWEntryEmpty(next))
				{
					readerWriterCount = next;
				}
				next = next.next;
			}
			if (dontAllocate)
			{
				return null;
			}
			if (readerWriterCount == null)
			{
				readerWriterCount = new ReaderWriterCount();
				readerWriterCount.next = ReaderWriterLockSlim.t_rwc;
				ReaderWriterLockSlim.t_rwc = readerWriterCount;
			}
			readerWriterCount.lockID = this.lockID;
			return readerWriterCount;
		}

		/// <summary>Tries to enter the lock in read mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The current thread cannot acquire the write lock when it holds the read lock.-or-The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" />, and the current thread has attempted to acquire the read lock when it already holds the read lock. -or-The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" />, and the current thread has attempted to acquire the read lock when it already holds the write lock. -or-The recursion number would exceed the capacity of the counter. This limit is so large that applications should never encounter this exception. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A71 RID: 6769 RVA: 0x0005910D File Offset: 0x0005730D
		public void EnterReadLock()
		{
			this.TryEnterReadLock(-1);
		}

		/// <summary>Tries to enter the lock in read mode, with an optional time-out.</summary>
		/// <param name="timeout">The interval to wait, or -1 milliseconds to wait indefinitely. </param>
		/// <returns>
		///     <see langword="true" /> if the calling thread entered read mode, otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative, but it is not equal to -1 milliseconds, which is the only negative value allowed.-or-The value of <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A72 RID: 6770 RVA: 0x00059117 File Offset: 0x00057317
		public bool TryEnterReadLock(TimeSpan timeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		/// <summary>Tries to enter the lock in read mode, with an optional integer time-out.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <returns>
		///     <see langword="true" /> if the calling thread entered read mode, otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A73 RID: 6771 RVA: 0x00059125 File Offset: 0x00057325
		public bool TryEnterReadLock(int millisecondsTimeout)
		{
			return this.TryEnterReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00059133 File Offset: 0x00057333
		private bool TryEnterReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterReadLockCore(timeout);
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0005913C File Offset: 0x0005733C
		private bool TryEnterReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			ReaderWriterCount threadRWCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(SR.GetString("A read lock may not be acquired with the write lock held in this mode."));
				}
				this.EnterMyLock();
				threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(SR.GetString("Recursive read lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					threadRWCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					return true;
				}
			}
			else
			{
				this.EnterMyLock();
				threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount.readercount > 0)
				{
					threadRWCount.readercount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					threadRWCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					this.fUpgradeThreadHoldingRead = true;
					return true;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					threadRWCount.readercount++;
					this.owners += 1U;
					this.ExitMyLock();
					return true;
				}
			}
			bool flag = true;
			int num = 0;
			while (this.owners >= 268435454U)
			{
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
				else if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
				else
				{
					flag = this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, timeout, false);
					if (!flag)
					{
						return false;
					}
					if (this.IsRwHashEntryChanged(threadRWCount))
					{
						threadRWCount = this.GetThreadRWCount(false);
					}
				}
			}
			this.owners += 1U;
			threadRWCount.readercount++;
			this.ExitMyLock();
			return flag;
		}

		/// <summary>Tries to enter the lock in write mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock in any mode. -or-The current thread has entered read mode, so trying to enter the lock in write mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A76 RID: 6774 RVA: 0x00059344 File Offset: 0x00057544
		public void EnterWriteLock()
		{
			this.TryEnterWriteLock(-1);
		}

		/// <summary>Tries to enter the lock in write mode, with an optional time-out.</summary>
		/// <param name="timeout">The interval to wait, or -1 milliseconds to wait indefinitely.</param>
		/// <returns>
		///     <see langword="true" /> if the calling thread entered write mode, otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter write mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative, but it is not equal to -1 milliseconds, which is the only negative value allowed.-or-The value of <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A77 RID: 6775 RVA: 0x0005934E File Offset: 0x0005754E
		public bool TryEnterWriteLock(TimeSpan timeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		/// <summary>Tries to enter the lock in write mode, with an optional time-out.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <returns>
		///     <see langword="true" /> if the calling thread entered write mode, otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter write mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A78 RID: 6776 RVA: 0x0005935C File Offset: 0x0005755C
		public bool TryEnterWriteLock(int millisecondsTimeout)
		{
			return this.TryEnterWriteLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x0005936A File Offset: 0x0005756A
		private bool TryEnterWriteLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterWriteLockCore(timeout);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00059374 File Offset: 0x00057574
		private bool TryEnterWriteLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			bool flag = false;
			ReaderWriterCount threadRWCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(SR.GetString("Recursive write lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					flag = true;
				}
				this.EnterMyLock();
				threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null && threadRWCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(SR.GetString("Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock."));
				}
			}
			else
			{
				this.EnterMyLock();
				threadRWCount = this.GetThreadRWCount(false);
				if (managedThreadId == this.writeLockOwnerId)
				{
					threadRWCount.writercount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					flag = true;
				}
				else if (threadRWCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(SR.GetString("Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock."));
				}
			}
			int num = 0;
			while (!this.IsWriterAcquired())
			{
				if (flag)
				{
					uint numReaders = this.GetNumReaders();
					if (numReaders == 1U)
					{
						this.SetWriterAcquired();
					}
					else
					{
						if (numReaders != 2U || threadRWCount == null)
						{
							goto IL_12E;
						}
						if (this.IsRwHashEntryChanged(threadRWCount))
						{
							threadRWCount = this.GetThreadRWCount(false);
						}
						if (threadRWCount.readercount <= 0)
						{
							goto IL_12E;
						}
						this.SetWriterAcquired();
					}
					IL_1C6:
					if (this.fIsReentrant)
					{
						if (this.IsRwHashEntryChanged(threadRWCount))
						{
							threadRWCount = this.GetThreadRWCount(false);
						}
						threadRWCount.writercount++;
					}
					this.ExitMyLock();
					this.writeLockOwnerId = managedThreadId;
					return true;
				}
				IL_12E:
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
				}
				else if (flag)
				{
					if (this.waitUpgradeEvent == null)
					{
						this.LazyCreateEvent(ref this.waitUpgradeEvent, true);
					}
					else if (!this.WaitOnEvent(this.waitUpgradeEvent, ref this.numWriteUpgradeWaiters, timeout, true))
					{
						return false;
					}
				}
				else if (this.writeEvent == null)
				{
					this.LazyCreateEvent(ref this.writeEvent, true);
				}
				else if (!this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, timeout, true))
				{
					return false;
				}
			}
			this.SetWriterAcquired();
			goto IL_1C6;
		}

		/// <summary>Tries to enter the lock in upgradeable mode.</summary>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock in any mode. -or-The current thread has entered read mode, so trying to enter upgradeable mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A7B RID: 6779 RVA: 0x0005957C File Offset: 0x0005777C
		public void EnterUpgradeableReadLock()
		{
			this.TryEnterUpgradeableReadLock(-1);
		}

		/// <summary>Tries to enter the lock in upgradeable mode, with an optional time-out.</summary>
		/// <param name="timeout">The interval to wait, or -1 milliseconds to wait indefinitely.</param>
		/// <returns>
		///     <see langword="true" /> if the calling thread entered upgradeable mode, otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter upgradeable mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="timeout" /> is negative, but it is not equal to -1 milliseconds, which is the only negative value allowed.-or-The value of <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" /> milliseconds. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A7C RID: 6780 RVA: 0x00059586 File Offset: 0x00057786
		public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(timeout));
		}

		/// <summary>Tries to enter the lock in upgradeable mode, with an optional time-out.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or -1 (<see cref="F:System.Threading.Timeout.Infinite" />) to wait indefinitely.</param>
		/// <returns>
		///     <see langword="true" /> if the calling thread entered upgradeable mode, otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Threading.LockRecursionException">The <see cref="P:System.Threading.ReaderWriterLockSlim.RecursionPolicy" /> property is <see cref="F:System.Threading.LockRecursionPolicy.NoRecursion" /> and the current thread has already entered the lock. -or-The current thread initially entered the lock in read mode, and therefore trying to enter upgradeable mode would create the possibility of a deadlock. -or-The recursion number would exceed the capacity of the counter. The limit is so large that applications should never encounter it.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="millisecondsTimeout" /> is negative, but it is not equal to <see cref="F:System.Threading.Timeout.Infinite" /> (-1), which is the only negative value allowed. </exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.ReaderWriterLockSlim" /> object has been disposed. </exception>
		// Token: 0x06001A7D RID: 6781 RVA: 0x00059594 File Offset: 0x00057794
		public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
		{
			return this.TryEnterUpgradeableReadLock(new ReaderWriterLockSlim.TimeoutTracker(millisecondsTimeout));
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000595A2 File Offset: 0x000577A2
		private bool TryEnterUpgradeableReadLock(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			return this.TryEnterUpgradeableReadLockCore(timeout);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000595AC File Offset: 0x000577AC
		private bool TryEnterUpgradeableReadLockCore(ReaderWriterLockSlim.TimeoutTracker timeout)
		{
			if (this.fDisposed)
			{
				throw new ObjectDisposedException(null);
			}
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			ReaderWriterCount threadRWCount;
			if (!this.fIsReentrant)
			{
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					throw new LockRecursionException(SR.GetString("Recursive upgradeable lock acquisitions not allowed in this mode."));
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					throw new LockRecursionException(SR.GetString("Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer."));
				}
				this.EnterMyLock();
				threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null && threadRWCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(SR.GetString("Upgradeable lock may not be acquired with read lock held."));
				}
			}
			else
			{
				this.EnterMyLock();
				threadRWCount = this.GetThreadRWCount(false);
				if (managedThreadId == this.upgradeLockOwnerId)
				{
					threadRWCount.upgradecount++;
					this.ExitMyLock();
					return true;
				}
				if (managedThreadId == this.writeLockOwnerId)
				{
					this.owners += 1U;
					this.upgradeLockOwnerId = managedThreadId;
					threadRWCount.upgradecount++;
					if (threadRWCount.readercount > 0)
					{
						this.fUpgradeThreadHoldingRead = true;
					}
					this.ExitMyLock();
					return true;
				}
				if (threadRWCount.readercount > 0)
				{
					this.ExitMyLock();
					throw new LockRecursionException(SR.GetString("Upgradeable lock may not be acquired with read lock held."));
				}
			}
			int num = 0;
			while (this.upgradeLockOwnerId != -1 || this.owners >= 268435454U)
			{
				if (num < 20)
				{
					this.ExitMyLock();
					if (timeout.IsExpired)
					{
						return false;
					}
					num++;
					ReaderWriterLockSlim.SpinWait(num);
					this.EnterMyLock();
				}
				else if (this.upgradeEvent == null)
				{
					this.LazyCreateEvent(ref this.upgradeEvent, true);
				}
				else if (!this.WaitOnEvent(this.upgradeEvent, ref this.numUpgradeWaiters, timeout, false))
				{
					return false;
				}
			}
			this.owners += 1U;
			this.upgradeLockOwnerId = managedThreadId;
			if (this.fIsReentrant)
			{
				if (this.IsRwHashEntryChanged(threadRWCount))
				{
					threadRWCount = this.GetThreadRWCount(false);
				}
				threadRWCount.upgradecount++;
			}
			this.ExitMyLock();
			return true;
		}

		/// <summary>Reduces the recursion count for read mode, and exits read mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in read mode. </exception>
		// Token: 0x06001A80 RID: 6784 RVA: 0x0005978C File Offset: 0x0005798C
		public void ExitReadLock()
		{
			this.EnterMyLock();
			ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
			if (threadRWCount == null || threadRWCount.readercount < 1)
			{
				this.ExitMyLock();
				throw new SynchronizationLockException(SR.GetString("The read lock is being released without being held."));
			}
			if (this.fIsReentrant)
			{
				if (threadRWCount.readercount > 1)
				{
					threadRWCount.readercount--;
					this.ExitMyLock();
					return;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.upgradeLockOwnerId)
				{
					this.fUpgradeThreadHoldingRead = false;
				}
			}
			this.owners -= 1U;
			threadRWCount.readercount--;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		/// <summary>Reduces the recursion count for write mode, and exits write mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in write mode.</exception>
		// Token: 0x06001A81 RID: 6785 RVA: 0x0005982C File Offset: 0x00057A2C
		public void ExitWriteLock()
		{
			if (!this.fIsReentrant)
			{
				if (Thread.CurrentThread.ManagedThreadId != this.writeLockOwnerId)
				{
					throw new SynchronizationLockException(SR.GetString("The write lock is being released without being held."));
				}
				this.EnterMyLock();
			}
			else
			{
				this.EnterMyLock();
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(false);
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(SR.GetString("The write lock is being released without being held."));
				}
				if (threadRWCount.writercount < 1)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(SR.GetString("The write lock is being released without being held."));
				}
				threadRWCount.writercount--;
				if (threadRWCount.writercount > 0)
				{
					this.ExitMyLock();
					return;
				}
			}
			this.ClearWriterAcquired();
			this.writeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		/// <summary>Reduces the recursion count for upgradeable mode, and exits upgradeable mode if the resulting count is 0 (zero).</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">The current thread has not entered the lock in upgradeable mode.</exception>
		// Token: 0x06001A82 RID: 6786 RVA: 0x000598E0 File Offset: 0x00057AE0
		public void ExitUpgradeableReadLock()
		{
			if (!this.fIsReentrant)
			{
				if (Thread.CurrentThread.ManagedThreadId != this.upgradeLockOwnerId)
				{
					throw new SynchronizationLockException(SR.GetString("The upgradeable lock is being released without being held."));
				}
				this.EnterMyLock();
			}
			else
			{
				this.EnterMyLock();
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount == null)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(SR.GetString("The upgradeable lock is being released without being held."));
				}
				if (threadRWCount.upgradecount < 1)
				{
					this.ExitMyLock();
					throw new SynchronizationLockException(SR.GetString("The upgradeable lock is being released without being held."));
				}
				threadRWCount.upgradecount--;
				if (threadRWCount.upgradecount > 0)
				{
					this.ExitMyLock();
					return;
				}
				this.fUpgradeThreadHoldingRead = false;
			}
			this.owners -= 1U;
			this.upgradeLockOwnerId = -1;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000599A4 File Offset: 0x00057BA4
		private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
		{
			this.ExitMyLock();
			EventWaitHandle eventWaitHandle;
			if (makeAutoResetEvent)
			{
				eventWaitHandle = new AutoResetEvent(false);
			}
			else
			{
				eventWaitHandle = new ManualResetEvent(false);
			}
			this.EnterMyLock();
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
				return;
			}
			eventWaitHandle.Close();
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000599E0 File Offset: 0x00057BE0
		private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, ReaderWriterLockSlim.TimeoutTracker timeout, bool isWriteWaiter)
		{
			waitEvent.Reset();
			numWaiters += 1U;
			this.fNoWaiters = false;
			if (this.numWriteWaiters == 1U)
			{
				this.SetWritersWaiting();
			}
			if (this.numWriteUpgradeWaiters == 1U)
			{
				this.SetUpgraderWaiting();
			}
			bool flag = false;
			this.ExitMyLock();
			try
			{
				flag = waitEvent.WaitOne(timeout.RemainingMilliseconds);
			}
			finally
			{
				this.EnterMyLock();
				numWaiters -= 1U;
				if (this.numWriteWaiters == 0U && this.numWriteUpgradeWaiters == 0U && this.numUpgradeWaiters == 0U && this.numReadWaiters == 0U)
				{
					this.fNoWaiters = true;
				}
				if (this.numWriteWaiters == 0U)
				{
					this.ClearWritersWaiting();
				}
				if (this.numWriteUpgradeWaiters == 0U)
				{
					this.ClearUpgraderWaiting();
				}
				if (!flag)
				{
					if (isWriteWaiter)
					{
						this.ExitAndWakeUpAppropriateReadWaiters();
					}
					else
					{
						this.ExitMyLock();
					}
				}
			}
			return flag;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x00059AB0 File Offset: 0x00057CB0
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.fNoWaiters)
			{
				this.ExitMyLock();
				return;
			}
			this.ExitAndWakeUpAppropriateWaitersPreferringWriters();
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00059AC8 File Offset: 0x00057CC8
		private void ExitAndWakeUpAppropriateWaitersPreferringWriters()
		{
			uint numReaders = this.GetNumReaders();
			if (this.fIsReentrant && this.numWriteUpgradeWaiters > 0U && this.fUpgradeThreadHoldingRead && numReaders == 2U)
			{
				this.ExitMyLock();
				this.waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 1U && this.numWriteUpgradeWaiters > 0U)
			{
				this.ExitMyLock();
				this.waitUpgradeEvent.Set();
				return;
			}
			if (numReaders == 0U && this.numWriteWaiters > 0U)
			{
				this.ExitMyLock();
				this.writeEvent.Set();
				return;
			}
			this.ExitAndWakeUpAppropriateReadWaiters();
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00059B54 File Offset: 0x00057D54
		private void ExitAndWakeUpAppropriateReadWaiters()
		{
			if (this.numWriteWaiters != 0U || this.numWriteUpgradeWaiters != 0U || this.fNoWaiters)
			{
				this.ExitMyLock();
				return;
			}
			bool flag = this.numReadWaiters > 0U;
			bool flag2 = this.numUpgradeWaiters != 0U && this.upgradeLockOwnerId == -1;
			this.ExitMyLock();
			if (flag)
			{
				this.readEvent.Set();
			}
			if (flag2)
			{
				this.upgradeEvent.Set();
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00059BC1 File Offset: 0x00057DC1
		private bool IsWriterAcquired()
		{
			return (this.owners & 3221225471U) == 0U;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00059BD2 File Offset: 0x00057DD2
		private void SetWriterAcquired()
		{
			this.owners |= 2147483648U;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00059BE6 File Offset: 0x00057DE6
		private void ClearWriterAcquired()
		{
			this.owners &= 2147483647U;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00059BFA File Offset: 0x00057DFA
		private void SetWritersWaiting()
		{
			this.owners |= 1073741824U;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00059C0E File Offset: 0x00057E0E
		private void ClearWritersWaiting()
		{
			this.owners &= 3221225471U;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00059C22 File Offset: 0x00057E22
		private void SetUpgraderWaiting()
		{
			this.owners |= 536870912U;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00059C36 File Offset: 0x00057E36
		private void ClearUpgraderWaiting()
		{
			this.owners &= 3758096383U;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00059C4A File Offset: 0x00057E4A
		private uint GetNumReaders()
		{
			return this.owners & 268435455U;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00059C58 File Offset: 0x00057E58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void EnterMyLock()
		{
			if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
			{
				this.EnterMyLockSpin();
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00059C70 File Offset: 0x00057E70
		private void EnterMyLockSpin()
		{
			int processorCount = PlatformHelper.ProcessorCount;
			int num = 0;
			for (;;)
			{
				if (num < 10 && processorCount > 1)
				{
					Thread.SpinWait(20 * (num + 1));
				}
				else if (num < 15)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Sleep(1);
				}
				if (this.myLock == 0 && Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
				{
					break;
				}
				num++;
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00059CCB File Offset: 0x00057ECB
		private void ExitMyLock()
		{
			Volatile.Write(ref this.myLock, 0);
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00059CD9 File Offset: 0x00057ED9
		private static void SpinWait(int SpinCount)
		{
			if (SpinCount < 5 && PlatformHelper.ProcessorCount > 1)
			{
				Thread.SpinWait(20 * SpinCount);
				return;
			}
			if (SpinCount < 17)
			{
				Thread.Sleep(0);
				return;
			}
			Thread.Sleep(1);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ReaderWriterLockSlim" /> class.</summary>
		/// <exception cref="T:System.Threading.SynchronizationLockException">
		///         <see cref="P:System.Threading.ReaderWriterLockSlim.WaitingReadCount" /> is greater than zero. -or-
		///         <see cref="P:System.Threading.ReaderWriterLockSlim.WaitingUpgradeCount" /> is greater than zero. -or-
		///         <see cref="P:System.Threading.ReaderWriterLockSlim.WaitingWriteCount" /> is greater than zero. </exception>
		// Token: 0x06001A94 RID: 6804 RVA: 0x00059D03 File Offset: 0x00057F03
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00059D0C File Offset: 0x00057F0C
		private void Dispose(bool disposing)
		{
			if (disposing && !this.fDisposed)
			{
				if (this.WaitingReadCount > 0 || this.WaitingUpgradeCount > 0 || this.WaitingWriteCount > 0)
				{
					throw new SynchronizationLockException(SR.GetString("The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock."));
				}
				if (this.IsReadLockHeld || this.IsUpgradeableReadLockHeld || this.IsWriteLockHeld)
				{
					throw new SynchronizationLockException(SR.GetString("The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock."));
				}
				if (this.writeEvent != null)
				{
					this.writeEvent.Close();
					this.writeEvent = null;
				}
				if (this.readEvent != null)
				{
					this.readEvent.Close();
					this.readEvent = null;
				}
				if (this.upgradeEvent != null)
				{
					this.upgradeEvent.Close();
					this.upgradeEvent = null;
				}
				if (this.waitUpgradeEvent != null)
				{
					this.waitUpgradeEvent.Close();
					this.waitUpgradeEvent = null;
				}
				this.fDisposed = true;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in read mode.</summary>
		/// <returns>
		///     <see langword="true" /> if the current thread has entered read mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x00059DEC File Offset: 0x00057FEC
		public bool IsReadLockHeld
		{
			get
			{
				return this.RecursiveReadCount > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in upgradeable mode. </summary>
		/// <returns>
		///     <see langword="true" /> if the current thread has entered upgradeable mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00059DFA File Offset: 0x00057FFA
		public bool IsUpgradeableReadLockHeld
		{
			get
			{
				return this.RecursiveUpgradeCount > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the current thread has entered the lock in write mode.</summary>
		/// <returns>
		///     <see langword="true" /> if the current thread has entered write mode; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00059E08 File Offset: 0x00058008
		public bool IsWriteLockHeld
		{
			get
			{
				return this.RecursiveWriteCount > 0;
			}
		}

		/// <summary>Gets a value that indicates the recursion policy for the current <see cref="T:System.Threading.ReaderWriterLockSlim" /> object.</summary>
		/// <returns>One of the enumeration values that specifies the lock recursion policy.</returns>
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x00059E16 File Offset: 0x00058016
		public LockRecursionPolicy RecursionPolicy
		{
			get
			{
				if (this.fIsReentrant)
				{
					return LockRecursionPolicy.SupportsRecursion;
				}
				return LockRecursionPolicy.NoRecursion;
			}
		}

		/// <summary>Gets the total number of unique threads that have entered the lock in read mode.</summary>
		/// <returns>The number of unique threads that have entered the lock in read mode.</returns>
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00059E24 File Offset: 0x00058024
		public int CurrentReadCount
		{
			get
			{
				int numReaders = (int)this.GetNumReaders();
				if (this.upgradeLockOwnerId != -1)
				{
					return numReaders - 1;
				}
				return numReaders;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in read mode, as an indication of recursion.</summary>
		/// <returns>0 (zero) if the current thread has not entered read mode, 1 if the thread has entered read mode but has not entered it recursively, or n if the thread has entered the lock recursively n - 1 times.</returns>
		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x00059E48 File Offset: 0x00058048
		public int RecursiveReadCount
		{
			get
			{
				int result = 0;
				ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
				if (threadRWCount != null)
				{
					result = threadRWCount.readercount;
				}
				return result;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in upgradeable mode, as an indication of recursion.</summary>
		/// <returns>0 if the current thread has not entered upgradeable mode, 1 if the thread has entered upgradeable mode but has not entered it recursively, or n if the thread has entered upgradeable mode recursively n - 1 times.</returns>
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00059E6C File Offset: 0x0005806C
		public int RecursiveUpgradeCount
		{
			get
			{
				if (this.fIsReentrant)
				{
					int result = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						result = threadRWCount.upgradecount;
					}
					return result;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.upgradeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Gets the number of times the current thread has entered the lock in write mode, as an indication of recursion.</summary>
		/// <returns>0 if the current thread has not entered write mode, 1 if the thread has entered write mode but has not entered it recursively, or n if the thread has entered write mode recursively n - 1 times.</returns>
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x00059EAC File Offset: 0x000580AC
		public int RecursiveWriteCount
		{
			get
			{
				if (this.fIsReentrant)
				{
					int result = 0;
					ReaderWriterCount threadRWCount = this.GetThreadRWCount(true);
					if (threadRWCount != null)
					{
						result = threadRWCount.writercount;
					}
					return result;
				}
				if (Thread.CurrentThread.ManagedThreadId == this.writeLockOwnerId)
				{
					return 1;
				}
				return 0;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in read mode.</summary>
		/// <returns>The total number of threads that are waiting to enter read mode.</returns>
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x00059EEC File Offset: 0x000580EC
		public int WaitingReadCount
		{
			get
			{
				return (int)this.numReadWaiters;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in upgradeable mode.</summary>
		/// <returns>The total number of threads that are waiting to enter upgradeable mode.</returns>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00059EF4 File Offset: 0x000580F4
		public int WaitingUpgradeCount
		{
			get
			{
				return (int)this.numUpgradeWaiters;
			}
		}

		/// <summary>Gets the total number of threads that are waiting to enter the lock in write mode.</summary>
		/// <returns>The total number of threads that are waiting to enter write mode.</returns>
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x00059EFC File Offset: 0x000580FC
		public int WaitingWriteCount
		{
			get
			{
				return (int)this.numWriteWaiters;
			}
		}

		// Token: 0x04000CAD RID: 3245
		private bool fIsReentrant;

		// Token: 0x04000CAE RID: 3246
		private int myLock;

		// Token: 0x04000CAF RID: 3247
		private const int LockSpinCycles = 20;

		// Token: 0x04000CB0 RID: 3248
		private const int LockSpinCount = 10;

		// Token: 0x04000CB1 RID: 3249
		private const int LockSleep0Count = 5;

		// Token: 0x04000CB2 RID: 3250
		private uint numWriteWaiters;

		// Token: 0x04000CB3 RID: 3251
		private uint numReadWaiters;

		// Token: 0x04000CB4 RID: 3252
		private uint numWriteUpgradeWaiters;

		// Token: 0x04000CB5 RID: 3253
		private uint numUpgradeWaiters;

		// Token: 0x04000CB6 RID: 3254
		private bool fNoWaiters;

		// Token: 0x04000CB7 RID: 3255
		private int upgradeLockOwnerId;

		// Token: 0x04000CB8 RID: 3256
		private int writeLockOwnerId;

		// Token: 0x04000CB9 RID: 3257
		private EventWaitHandle writeEvent;

		// Token: 0x04000CBA RID: 3258
		private EventWaitHandle readEvent;

		// Token: 0x04000CBB RID: 3259
		private EventWaitHandle upgradeEvent;

		// Token: 0x04000CBC RID: 3260
		private EventWaitHandle waitUpgradeEvent;

		// Token: 0x04000CBD RID: 3261
		private static long s_nextLockID;

		// Token: 0x04000CBE RID: 3262
		private long lockID;

		// Token: 0x04000CBF RID: 3263
		[ThreadStatic]
		private static ReaderWriterCount t_rwc;

		// Token: 0x04000CC0 RID: 3264
		private bool fUpgradeThreadHoldingRead;

		// Token: 0x04000CC1 RID: 3265
		private const int MaxSpinCount = 20;

		// Token: 0x04000CC2 RID: 3266
		private uint owners;

		// Token: 0x04000CC3 RID: 3267
		private const uint WRITER_HELD = 2147483648U;

		// Token: 0x04000CC4 RID: 3268
		private const uint WAITING_WRITERS = 1073741824U;

		// Token: 0x04000CC5 RID: 3269
		private const uint WAITING_UPGRADER = 536870912U;

		// Token: 0x04000CC6 RID: 3270
		private const uint MAX_READER = 268435454U;

		// Token: 0x04000CC7 RID: 3271
		private const uint READER_MASK = 268435455U;

		// Token: 0x04000CC8 RID: 3272
		private bool fDisposed;

		// Token: 0x02000365 RID: 869
		private struct TimeoutTracker
		{
			// Token: 0x06001AA1 RID: 6817 RVA: 0x00059F04 File Offset: 0x00058104
			public TimeoutTracker(TimeSpan timeout)
			{
				long num = (long)timeout.TotalMilliseconds;
				if (num < -1L || num > 2147483647L)
				{
					throw new ArgumentOutOfRangeException("timeout");
				}
				this.m_total = (int)num;
				if (this.m_total != -1 && this.m_total != 0)
				{
					this.m_start = Environment.TickCount;
					return;
				}
				this.m_start = 0;
			}

			// Token: 0x06001AA2 RID: 6818 RVA: 0x00059F5F File Offset: 0x0005815F
			public TimeoutTracker(int millisecondsTimeout)
			{
				if (millisecondsTimeout < -1)
				{
					throw new ArgumentOutOfRangeException("millisecondsTimeout");
				}
				this.m_total = millisecondsTimeout;
				if (this.m_total != -1 && this.m_total != 0)
				{
					this.m_start = Environment.TickCount;
					return;
				}
				this.m_start = 0;
			}

			// Token: 0x1700048D RID: 1165
			// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x00059F9C File Offset: 0x0005819C
			public int RemainingMilliseconds
			{
				get
				{
					if (this.m_total == -1 || this.m_total == 0)
					{
						return this.m_total;
					}
					int num = Environment.TickCount - this.m_start;
					if (num < 0 || num >= this.m_total)
					{
						return 0;
					}
					return this.m_total - num;
				}
			}

			// Token: 0x1700048E RID: 1166
			// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x00059FE5 File Offset: 0x000581E5
			public bool IsExpired
			{
				get
				{
					return this.RemainingMilliseconds == 0;
				}
			}

			// Token: 0x04000CC9 RID: 3273
			private int m_total;

			// Token: 0x04000CCA RID: 3274
			private int m_start;
		}
	}
}
