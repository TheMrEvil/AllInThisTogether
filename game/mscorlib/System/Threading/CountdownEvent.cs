using System;
using System.Diagnostics;

namespace System.Threading
{
	/// <summary>Represents a synchronization primitive that is signaled when its count reaches zero.</summary>
	// Token: 0x0200029D RID: 669
	[DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
	public class CountdownEvent : IDisposable
	{
		/// <summary>Initializes a new instance of <see cref="T:System.Threading.CountdownEvent" /> class with the specified count.</summary>
		/// <param name="initialCount">The number of signals initially required to set the <see cref="T:System.Threading.CountdownEvent" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="initialCount" /> is less than 0.</exception>
		// Token: 0x06001DAB RID: 7595 RVA: 0x0006E7C7 File Offset: 0x0006C9C7
		public CountdownEvent(int initialCount)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount");
			}
			this._initialCount = initialCount;
			this._currentCount = initialCount;
			this._event = new ManualResetEventSlim();
			if (initialCount == 0)
			{
				this._event.Set();
			}
		}

		/// <summary>Gets the number of remaining signals required to set the event.</summary>
		/// <returns>The number of remaining signals required to set the event.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0006E808 File Offset: 0x0006CA08
		public int CurrentCount
		{
			get
			{
				int currentCount = this._currentCount;
				if (currentCount >= 0)
				{
					return currentCount;
				}
				return 0;
			}
		}

		/// <summary>Gets the numbers of signals initially required to set the event.</summary>
		/// <returns>The number of signals initially required to set the event.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001DAD RID: 7597 RVA: 0x0006E825 File Offset: 0x0006CA25
		public int InitialCount
		{
			get
			{
				return this._initialCount;
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Threading.CountdownEvent" /> object's current count has reached zero.</summary>
		/// <returns>
		///   <see langword="true" /> if the current count is zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0006E82D File Offset: 0x0006CA2D
		public bool IsSet
		{
			get
			{
				return this._currentCount <= 0;
			}
		}

		/// <summary>Gets a <see cref="T:System.Threading.WaitHandle" /> that is used to wait for the event to be set.</summary>
		/// <returns>A <see cref="T:System.Threading.WaitHandle" /> that is used to wait for the event to be set.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0006E83D File Offset: 0x0006CA3D
		public WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				return this._event.WaitHandle;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CountdownEvent" /> class.</summary>
		// Token: 0x06001DB0 RID: 7600 RVA: 0x0006E850 File Offset: 0x0006CA50
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.CountdownEvent" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001DB1 RID: 7601 RVA: 0x0006E85F File Offset: 0x0006CA5F
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._event.Dispose();
				this._disposed = true;
			}
		}

		/// <summary>Registers a signal with the <see cref="T:System.Threading.CountdownEvent" />, decrementing the value of <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</summary>
		/// <returns>true if the signal caused the count to reach zero and the event was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.</exception>
		// Token: 0x06001DB2 RID: 7602 RVA: 0x0006E878 File Offset: 0x0006CA78
		public bool Signal()
		{
			this.ThrowIfDisposed();
			if (this._currentCount <= 0)
			{
				throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			}
			int num = Interlocked.Decrement(ref this._currentCount);
			if (num == 0)
			{
				this._event.Set();
				return true;
			}
			if (num < 0)
			{
				throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			}
			return false;
		}

		/// <summary>Registers multiple signals with the <see cref="T:System.Threading.CountdownEvent" />, decrementing the value of <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by the specified amount.</summary>
		/// <param name="signalCount">The number of signals to register.</param>
		/// <returns>true if the signals caused the count to reach zero and the event was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than 1.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set. -or- Or <paramref name="signalCount" /> is greater than <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</exception>
		// Token: 0x06001DB3 RID: 7603 RVA: 0x0006E8D0 File Offset: 0x0006CAD0
		public bool Signal(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			int currentCount;
			for (;;)
			{
				currentCount = this._currentCount;
				if (currentCount < signalCount)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this._currentCount, currentCount - signalCount, currentCount) == currentCount)
				{
					goto IL_50;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException("Invalid attempt made to decrement the event's count below zero.");
			IL_50:
			if (currentCount == signalCount)
			{
				this._event.Set();
				return true;
			}
			return false;
		}

		/// <summary>Increments the <see cref="T:System.Threading.CountdownEvent" />'s current count by one.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.  
		///  -or-  
		///  <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001DB4 RID: 7604 RVA: 0x0006E93F File Offset: 0x0006CB3F
		public void AddCount()
		{
			this.AddCount(1);
		}

		/// <summary>Attempts to increment <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by one.</summary>
		/// <returns>true if the increment succeeded; otherwise, false. If <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is already at zero, this method will return false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001DB5 RID: 7605 RVA: 0x0006E948 File Offset: 0x0006CB48
		public bool TryAddCount()
		{
			return this.TryAddCount(1);
		}

		/// <summary>Increments the <see cref="T:System.Threading.CountdownEvent" />'s current count by a specified value.</summary>
		/// <param name="signalCount">The value by which to increase <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is already set.  
		///  -or-  
		///  <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" /> after count is incremented by <paramref name="signalCount." /></exception>
		// Token: 0x06001DB6 RID: 7606 RVA: 0x0006E951 File Offset: 0x0006CB51
		public void AddCount(int signalCount)
		{
			if (!this.TryAddCount(signalCount))
			{
				throw new InvalidOperationException("The event is already signaled and cannot be incremented.");
			}
		}

		/// <summary>Attempts to increment <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> by a specified value.</summary>
		/// <param name="signalCount">The value by which to increase <see cref="P:System.Threading.CountdownEvent.CurrentCount" />.</param>
		/// <returns>true if the increment succeeded; otherwise, false. If <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> is already at zero this will return false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="signalCount" /> is less than or equal to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> + <paramref name="signalCount" /> is equal to or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001DB7 RID: 7607 RVA: 0x0006E968 File Offset: 0x0006CB68
		public bool TryAddCount(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentCount = this._currentCount;
				if (currentCount <= 0)
				{
					break;
				}
				if (currentCount > 2147483647 - signalCount)
				{
					goto Block_3;
				}
				if (Interlocked.CompareExchange(ref this._currentCount, currentCount + signalCount, currentCount) == currentCount)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
			Block_3:
			throw new InvalidOperationException("The increment operation would cause the CurrentCount to overflow.");
		}

		/// <summary>Resets the <see cref="P:System.Threading.CountdownEvent.CurrentCount" /> to the value of <see cref="P:System.Threading.CountdownEvent.InitialCount" />.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x06001DB8 RID: 7608 RVA: 0x0006E9D2 File Offset: 0x0006CBD2
		public void Reset()
		{
			this.Reset(this._initialCount);
		}

		/// <summary>Resets the <see cref="P:System.Threading.CountdownEvent.InitialCount" /> property to a specified value.</summary>
		/// <param name="count">The number of signals required to set the <see cref="T:System.Threading.CountdownEvent" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has alread been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="count" /> is less than 0.</exception>
		// Token: 0x06001DB9 RID: 7609 RVA: 0x0006E9E0 File Offset: 0x0006CBE0
		public void Reset(int count)
		{
			this.ThrowIfDisposed();
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this._currentCount = count;
			this._initialCount = count;
			if (count == 0)
			{
				this._event.Set();
				return;
			}
			this._event.Reset();
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		// Token: 0x06001DBA RID: 7610 RVA: 0x0006EA2C File Offset: 0x0006CC2C
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		// Token: 0x06001DBB RID: 7611 RVA: 0x0006EA4A File Offset: 0x0006CC4A
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the timeout.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001DBC RID: 7612 RVA: 0x0006EA58 File Offset: 0x0006CC58
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001DBD RID: 7613 RVA: 0x0006EA98 File Offset: 0x0006CC98
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a 32-bit signed integer to measure the timeout.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06001DBE RID: 7614 RVA: 0x0006EAD0 File Offset: 0x0006CCD0
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the <see cref="T:System.Threading.CountdownEvent" /> is set, using a 32-bit signed integer to measure the timeout, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>true if the <see cref="T:System.Threading.CountdownEvent" /> was set; otherwise, false.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> has been canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. -or- The <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06001DBF RID: 7615 RVA: 0x0006EAF0 File Offset: 0x0006CCF0
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = this.IsSet;
			if (!flag)
			{
				flag = this._event.Wait(millisecondsTimeout, cancellationToken);
			}
			return flag;
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x0006EB32 File Offset: 0x0006CD32
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("CountdownEvent");
			}
		}

		// Token: 0x04001A52 RID: 6738
		private int _initialCount;

		// Token: 0x04001A53 RID: 6739
		private volatile int _currentCount;

		// Token: 0x04001A54 RID: 6740
		private ManualResetEventSlim _event;

		// Token: 0x04001A55 RID: 6741
		private volatile bool _disposed;
	}
}
