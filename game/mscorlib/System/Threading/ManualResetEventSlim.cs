using System;
using System.Diagnostics;

namespace System.Threading
{
	/// <summary>Provides a slimmed down version of <see cref="T:System.Threading.ManualResetEvent" />.</summary>
	// Token: 0x020002A2 RID: 674
	[DebuggerDisplay("Set = {IsSet}")]
	public class ManualResetEventSlim : IDisposable
	{
		/// <summary>Gets the underlying <see cref="T:System.Threading.WaitHandle" /> object for this <see cref="T:System.Threading.ManualResetEventSlim" />.</summary>
		/// <returns>The underlying <see cref="T:System.Threading.WaitHandle" /> event object fore this <see cref="T:System.Threading.ManualResetEventSlim" />.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x0006EE02 File Offset: 0x0006D002
		public WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.m_eventObj == null)
				{
					this.LazyInitializeEvent();
				}
				return this.m_eventObj;
			}
		}

		/// <summary>Gets whether the event is set.</summary>
		/// <returns>true if the event has is set; otherwise, false.</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x0006EE23 File Offset: 0x0006D023
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x0006EE3A File Offset: 0x0006D03A
		public bool IsSet
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) != 0;
			}
			private set
			{
				this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
			}
		}

		/// <summary>Gets the number of spin waits that will occur before falling back to a kernel-based wait operation.</summary>
		/// <returns>Returns the number of spin waits that will occur before falling back to a kernel-based wait operation.</returns>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x0006EE51 File Offset: 0x0006D051
		// (set) Token: 0x06001DE1 RID: 7649 RVA: 0x0006EE67 File Offset: 0x0006D067
		public int SpinCount
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
			}
			private set
			{
				this.m_combinedState = ((this.m_combinedState & -1073217537) | value << 19);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0006EE84 File Offset: 0x0006D084
		// (set) Token: 0x06001DE3 RID: 7651 RVA: 0x0006EE99 File Offset: 0x0006D099
		private int Waiters
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
			}
			set
			{
				if (value >= 524287)
				{
					throw new InvalidOperationException(string.Format("There are too many threads currently waiting on the event. A maximum of {0} waiting threads are supported.", 524287));
				}
				this.UpdateStateAtomically(value, 524287);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with an initial state of nonsignaled.</summary>
		// Token: 0x06001DE4 RID: 7652 RVA: 0x0006EEC9 File Offset: 0x0006D0C9
		public ManualResetEventSlim() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with a Boolean value indicating whether to set the intial state to signaled.</summary>
		/// <param name="initialState">true to set the initial state signaled; false to set the initial state to nonsignaled.</param>
		// Token: 0x06001DE5 RID: 7653 RVA: 0x0006EED2 File Offset: 0x0006D0D2
		public ManualResetEventSlim(bool initialState)
		{
			this.Initialize(initialState, SpinWait.SpinCountforSpinBeforeWait);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class with a Boolean value indicating whether to set the intial state to signaled and a specified spin count.</summary>
		/// <param name="initialState">true to set the initial state to signaled; false to set the initial state to nonsignaled.</param>
		/// <param name="spinCount">The number of spin waits that will occur before falling back to a kernel-based wait operation.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="spinCount" /> is less than 0 or greater than the maximum allowed value.</exception>
		// Token: 0x06001DE6 RID: 7654 RVA: 0x0006EEE8 File Offset: 0x0006D0E8
		public ManualResetEventSlim(bool initialState, int spinCount)
		{
			if (spinCount < 0)
			{
				throw new ArgumentOutOfRangeException("spinCount");
			}
			if (spinCount > 2047)
			{
				throw new ArgumentOutOfRangeException("spinCount", string.Format("The spinCount argument must be in the range 0 to {0}, inclusive.", 2047));
			}
			this.Initialize(initialState, spinCount);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0006EF39 File Offset: 0x0006D139
		private void Initialize(bool initialState, int spinCount)
		{
			this.m_combinedState = (initialState ? int.MinValue : 0);
			this.SpinCount = (PlatformHelper.IsSingleProcessor ? 1 : spinCount);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0006EF60 File Offset: 0x0006D160
		private void EnsureLockObjectCreated()
		{
			if (this.m_lock != null)
			{
				return;
			}
			object value = new object();
			Interlocked.CompareExchange(ref this.m_lock, value, null);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0006EF8C File Offset: 0x0006D18C
		private bool LazyInitializeEvent()
		{
			bool isSet = this.IsSet;
			ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
			if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, null) != null)
			{
				manualResetEvent.Dispose();
				return false;
			}
			if (this.IsSet != isSet)
			{
				ManualResetEvent obj = manualResetEvent;
				lock (obj)
				{
					if (this.m_eventObj == manualResetEvent)
					{
						manualResetEvent.Set();
					}
				}
			}
			return true;
		}

		/// <summary>Sets the state of the event to signaled, which allows one or more threads waiting on the event to proceed.</summary>
		// Token: 0x06001DEA RID: 7658 RVA: 0x0006F004 File Offset: 0x0006D204
		public void Set()
		{
			this.Set(false);
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0006F010 File Offset: 0x0006D210
		private void Set(bool duringCancellation)
		{
			this.IsSet = true;
			if (this.Waiters > 0)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					Monitor.PulseAll(this.m_lock);
				}
			}
			ManualResetEvent eventObj = this.m_eventObj;
			if (eventObj != null && !duringCancellation)
			{
				ManualResetEvent obj = eventObj;
				lock (obj)
				{
					if (this.m_eventObj != null)
					{
						this.m_eventObj.Set();
					}
				}
			}
		}

		/// <summary>Sets the state of the event to nonsignaled, which causes threads to block.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06001DEC RID: 7660 RVA: 0x0006F0B4 File Offset: 0x0006D2B4
		public void Reset()
		{
			this.ThrowIfDisposed();
			if (this.m_eventObj != null)
			{
				this.m_eventObj.Reset();
			}
			this.IsSet = false;
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set.</summary>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06001DED RID: 7661 RVA: 0x0006F0DC File Offset: 0x0006D2DC
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> receives a signal, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06001DEE RID: 7662 RVA: 0x0006F0FA File Offset: 0x0006D2FA
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the time interval.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// The number of milliseconds in <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06001DEF RID: 7663 RVA: 0x0006F108 File Offset: 0x0006D308
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a <see cref="T:System.TimeSpan" /> to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.  
		/// -or-  
		/// The number of milliseconds in <paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06001DF0 RID: 7664 RVA: 0x0006F148 File Offset: 0x0006D348
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a 32-bit signed integer to measure the time interval.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed.</exception>
		// Token: 0x06001DF1 RID: 7665 RVA: 0x0006F180 File Offset: 0x0006D380
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.ManualResetEventSlim" /> is set, using a 32-bit signed integer to measure the time interval, while observing a <see cref="T:System.Threading.CancellationToken" />.</summary>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" />(-1) to wait indefinitely.</param>
		/// <param name="cancellationToken">The <see cref="T:System.Threading.CancellationToken" /> to observe.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Threading.ManualResetEventSlim" /> was set; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.OperationCanceledException">
		///   <paramref name="cancellationToken" /> was canceled.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of waiters has been exceeded.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The object has already been disposed or the <see cref="T:System.Threading.CancellationTokenSource" /> that created <paramref name="cancellationToken" /> has been disposed.</exception>
		// Token: 0x06001DF2 RID: 7666 RVA: 0x0006F1A0 File Offset: 0x0006D3A0
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsSet)
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				uint startTime = 0U;
				bool flag = false;
				int num = millisecondsTimeout;
				if (millisecondsTimeout != -1)
				{
					startTime = TimeoutHelper.GetTime();
					flag = true;
				}
				int spinCount = this.SpinCount;
				SpinWait spinWait = default(SpinWait);
				while (spinWait.Count < spinCount)
				{
					spinWait.SpinOnce(40);
					if (this.IsSet)
					{
						return true;
					}
					if (spinWait.Count >= 100 && spinWait.Count % 10 == 0)
					{
						cancellationToken.ThrowIfCancellationRequested();
					}
				}
				this.EnsureLockObjectCreated();
				using (cancellationToken.InternalRegisterWithoutEC(ManualResetEventSlim.s_cancellationTokenCallback, this))
				{
					object @lock = this.m_lock;
					lock (@lock)
					{
						while (!this.IsSet)
						{
							cancellationToken.ThrowIfCancellationRequested();
							if (flag)
							{
								num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
								if (num <= 0)
								{
									return false;
								}
							}
							this.Waiters++;
							if (this.IsSet)
							{
								int waiters = this.Waiters;
								this.Waiters = waiters - 1;
								return true;
							}
							try
							{
								if (!Monitor.Wait(this.m_lock, num))
								{
									return false;
								}
							}
							finally
							{
								this.Waiters--;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.ManualResetEventSlim" /> class.</summary>
		// Token: 0x06001DF3 RID: 7667 RVA: 0x0006F324 File Offset: 0x0006D524
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.ManualResetEventSlim" />, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001DF4 RID: 7668 RVA: 0x0006F334 File Offset: 0x0006D534
		protected virtual void Dispose(bool disposing)
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				return;
			}
			this.m_combinedState |= 1073741824;
			if (disposing)
			{
				ManualResetEvent eventObj = this.m_eventObj;
				if (eventObj != null)
				{
					ManualResetEvent obj = eventObj;
					lock (obj)
					{
						eventObj.Dispose();
						this.m_eventObj = null;
					}
				}
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0006F3B0 File Offset: 0x0006D5B0
		private void ThrowIfDisposed()
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				throw new ObjectDisposedException("The event has been disposed.");
			}
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0006F3D0 File Offset: 0x0006D5D0
		private static void CancellationTokenCallback(object obj)
		{
			ManualResetEventSlim manualResetEventSlim = obj as ManualResetEventSlim;
			object @lock = manualResetEventSlim.m_lock;
			lock (@lock)
			{
				Monitor.PulseAll(manualResetEventSlim.m_lock);
			}
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0006F420 File Offset: 0x0006D620
		private void UpdateStateAtomically(int newBits, int updateBitsMask)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int combinedState = this.m_combinedState;
				int value = (combinedState & ~updateBitsMask) | newBits;
				if (Interlocked.CompareExchange(ref this.m_combinedState, value, combinedState) == combinedState)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x0006F45E File Offset: 0x0006D65E
		private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
		{
			return (int)((uint)(state & mask) >> rightBitShiftCount);
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0006F468 File Offset: 0x0006D668
		private static int ExtractStatePortion(int state, int mask)
		{
			return state & mask;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0006F46D File Offset: 0x0006D66D
		// Note: this type is marked as 'beforefieldinit'.
		static ManualResetEventSlim()
		{
		}

		// Token: 0x04001A5A RID: 6746
		private const int DEFAULT_SPIN_SP = 1;

		// Token: 0x04001A5B RID: 6747
		private volatile object m_lock;

		// Token: 0x04001A5C RID: 6748
		private volatile ManualResetEvent m_eventObj;

		// Token: 0x04001A5D RID: 6749
		private volatile int m_combinedState;

		// Token: 0x04001A5E RID: 6750
		private const int SignalledState_BitMask = -2147483648;

		// Token: 0x04001A5F RID: 6751
		private const int SignalledState_ShiftCount = 31;

		// Token: 0x04001A60 RID: 6752
		private const int Dispose_BitMask = 1073741824;

		// Token: 0x04001A61 RID: 6753
		private const int SpinCountState_BitMask = 1073217536;

		// Token: 0x04001A62 RID: 6754
		private const int SpinCountState_ShiftCount = 19;

		// Token: 0x04001A63 RID: 6755
		private const int SpinCountState_MaxValue = 2047;

		// Token: 0x04001A64 RID: 6756
		private const int NumWaitersState_BitMask = 524287;

		// Token: 0x04001A65 RID: 6757
		private const int NumWaitersState_ShiftCount = 0;

		// Token: 0x04001A66 RID: 6758
		private const int NumWaitersState_MaxValue = 524287;

		// Token: 0x04001A67 RID: 6759
		private static Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
	}
}
