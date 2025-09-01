using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Threading
{
	/// <summary>Provides a mechanism for executing a method on a thread pool thread at specified intervals. This class cannot be inherited.</summary>
	// Token: 0x020002F9 RID: 761
	[ComVisible(true)]
	public sealed class Timer : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06002127 RID: 8487 RVA: 0x00077BF7 File Offset: 0x00075DF7
		private static Timer.Scheduler scheduler
		{
			get
			{
				return Timer.Scheduler.Instance;
			}
		}

		/// <summary>Initializes a new instance of the <see langword="Timer" /> class, using a 32-bit signed integer to specify the time interval.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed.</param>
		/// <param name="state">An object containing information to be used by the callback method, or <see langword="null" />.</param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately.</param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002128 RID: 8488 RVA: 0x00077BFE File Offset: 0x00075DFE
		public Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			this.Init(callback, state, (long)dueTime, (long)period);
		}

		/// <summary>Initializes a new instance of the <see langword="Timer" /> class, using 64-bit signed integers to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed.</param>
		/// <param name="state">An object containing information to be used by the callback method, or <see langword="null" />.</param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately.</param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294.</exception>
		// Token: 0x06002129 RID: 8489 RVA: 0x00077C13 File Offset: 0x00075E13
		public Timer(TimerCallback callback, object state, long dueTime, long period)
		{
			this.Init(callback, state, dueTime, period);
		}

		/// <summary>Initializes a new instance of the <see langword="Timer" /> class, using <see cref="T:System.TimeSpan" /> values to measure time intervals.</summary>
		/// <param name="callback">A delegate representing a method to be executed.</param>
		/// <param name="state">An object containing information to be used by the callback method, or <see langword="null" />.</param>
		/// <param name="dueTime">The amount of time to delay before the <paramref name="callback" /> parameter invokes its methods. Specify negative one (-1) milliseconds to prevent the timer from starting. Specify zero (0) to start the timer immediately.</param>
		/// <param name="period">The time interval between invocations of the methods referenced by <paramref name="callback" />. Specify negative one (-1) milliseconds to disable periodic signaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of milliseconds in the value of <paramref name="dueTime" /> or <paramref name="period" /> is negative and not equal to <see cref="F:System.Threading.Timeout.Infinite" />, or is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600212A RID: 8490 RVA: 0x00077C26 File Offset: 0x00075E26
		public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			this.Init(callback, state, (long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		/// <summary>Initializes a new instance of the <see langword="Timer" /> class, using 32-bit unsigned integers to measure time intervals.</summary>
		/// <param name="callback">A delegate representing a method to be executed.</param>
		/// <param name="state">An object containing information to be used by the callback method, or <see langword="null" />.</param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately.</param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600212B RID: 8491 RVA: 0x00077C48 File Offset: 0x00075E48
		[CLSCompliant(false)]
		public Timer(TimerCallback callback, object state, uint dueTime, uint period)
		{
			long dueTime2 = (long)((dueTime == uint.MaxValue) ? ulong.MaxValue : ((ulong)dueTime));
			long period2 = (long)((period == uint.MaxValue) ? ulong.MaxValue : ((ulong)period));
			this.Init(callback, state, dueTime2, period2);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Timer" /> class with an infinite period and an infinite due time, using the newly created <see cref="T:System.Threading.Timer" /> object as the state object.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed.</param>
		// Token: 0x0600212C RID: 8492 RVA: 0x00077C7D File Offset: 0x00075E7D
		public Timer(TimerCallback callback)
		{
			this.Init(callback, this, -1L, -1L);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00077C91 File Offset: 0x00075E91
		private void Init(TimerCallback callback, object state, long dueTime, long period)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.callback = callback;
			this.state = state;
			this.is_dead = false;
			this.is_added = false;
			this.Change(dueTime, period, true);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 32-bit signed integers to measure time intervals.</summary>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately.</param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling.</param>
		/// <returns>
		///   <see langword="true" /> if the timer was successfully updated; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />.</exception>
		// Token: 0x0600212E RID: 8494 RVA: 0x00077CC8 File Offset: 0x00075EC8
		public bool Change(int dueTime, int period)
		{
			return this.Change((long)dueTime, (long)period, false);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using <see cref="T:System.TimeSpan" /> values to measure time intervals.</summary>
		/// <param name="dueTime">A <see cref="T:System.TimeSpan" /> representing the amount of time to delay before invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed. Specify negative one (-1) milliseconds to prevent the timer from restarting. Specify zero (0) to restart the timer immediately.</param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed. Specify negative one (-1) milliseconds to disable periodic signaling.</param>
		/// <returns>
		///   <see langword="true" /> if the timer was successfully updated; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is less than -1.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is greater than 4294967294.</exception>
		// Token: 0x0600212F RID: 8495 RVA: 0x00077CD5 File Offset: 0x00075ED5
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds, false);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 32-bit unsigned integers to measure time intervals.</summary>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately.</param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling.</param>
		/// <returns>
		///   <see langword="true" /> if the timer was successfully updated; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed.</exception>
		// Token: 0x06002130 RID: 8496 RVA: 0x00077CF0 File Offset: 0x00075EF0
		[CLSCompliant(false)]
		public bool Change(uint dueTime, uint period)
		{
			long dueTime2 = (long)((dueTime == uint.MaxValue) ? ulong.MaxValue : ((ulong)dueTime));
			long period2 = (long)((period == uint.MaxValue) ? ulong.MaxValue : ((ulong)period));
			return this.Change(dueTime2, period2, false);
		}

		/// <summary>Releases all resources used by the current instance of <see cref="T:System.Threading.Timer" />.</summary>
		// Token: 0x06002131 RID: 8497 RVA: 0x00077D1C File Offset: 0x00075F1C
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			Timer.scheduler.Remove(this);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 64-bit signed integers to measure time intervals.</summary>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately.</param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling.</param>
		/// <returns>
		///   <see langword="true" /> if the timer was successfully updated; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is less than -1.</exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294.</exception>
		// Token: 0x06002132 RID: 8498 RVA: 0x00077D39 File Offset: 0x00075F39
		public bool Change(long dueTime, long period)
		{
			return this.Change(dueTime, period, false);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00077D44 File Offset: 0x00075F44
		private bool Change(long dueTime, long period, bool first)
		{
			if (dueTime > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("dueTime", "Due time too large");
			}
			if (period > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("period", "Period too large");
			}
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime");
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			this.due_time_ms = dueTime;
			this.period_ms = period;
			long new_next_run;
			if (dueTime == 0L)
			{
				new_next_run = 0L;
			}
			else if (dueTime < 0L)
			{
				new_next_run = long.MaxValue;
				if (first)
				{
					this.next_run = new_next_run;
					return true;
				}
			}
			else
			{
				new_next_run = dueTime * 10000L + Timer.GetTimeMonotonic();
			}
			Timer.scheduler.Change(this, new_next_run);
			return true;
		}

		/// <summary>Releases all resources used by the current instance of <see cref="T:System.Threading.Timer" /> and signals when the timer has been disposed of.</summary>
		/// <param name="notifyObject">The <see cref="T:System.Threading.WaitHandle" /> to be signaled when the <see langword="Timer" /> has been disposed of.</param>
		/// <returns>
		///   <see langword="true" /> if the function succeeds; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="notifyObject" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002134 RID: 8500 RVA: 0x00077E03 File Offset: 0x00076003
		public bool Dispose(WaitHandle notifyObject)
		{
			if (notifyObject == null)
			{
				throw new ArgumentNullException("notifyObject");
			}
			this.Dispose();
			NativeEventCalls.SetEvent(notifyObject.SafeWaitHandle);
			return true;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00077E26 File Offset: 0x00076026
		public ValueTask DisposeAsync()
		{
			this.Dispose();
			return new ValueTask(Task.FromResult<object>(null));
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00004BF9 File Offset: 0x00002DF9
		internal void KeepRootedWhileScheduled()
		{
		}

		// Token: 0x06002137 RID: 8503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetTimeMonotonic();

		// Token: 0x04001BAE RID: 7086
		private TimerCallback callback;

		// Token: 0x04001BAF RID: 7087
		private object state;

		// Token: 0x04001BB0 RID: 7088
		private long due_time_ms;

		// Token: 0x04001BB1 RID: 7089
		private long period_ms;

		// Token: 0x04001BB2 RID: 7090
		private long next_run;

		// Token: 0x04001BB3 RID: 7091
		private bool disposed;

		// Token: 0x04001BB4 RID: 7092
		private bool is_dead;

		// Token: 0x04001BB5 RID: 7093
		private bool is_added;

		// Token: 0x04001BB6 RID: 7094
		private const long MaxValue = 4294967294L;

		// Token: 0x020002FA RID: 762
		private struct TimerComparer : IComparer, IComparer<Timer>
		{
			// Token: 0x06002138 RID: 8504 RVA: 0x00077E3C File Offset: 0x0007603C
			int IComparer.Compare(object x, object y)
			{
				if (x == y)
				{
					return 0;
				}
				Timer timer = x as Timer;
				if (timer == null)
				{
					return -1;
				}
				Timer timer2 = y as Timer;
				if (timer2 == null)
				{
					return 1;
				}
				return this.Compare(timer, timer2);
			}

			// Token: 0x06002139 RID: 8505 RVA: 0x00077E6F File Offset: 0x0007606F
			public int Compare(Timer tx, Timer ty)
			{
				return Math.Sign(tx.next_run - ty.next_run);
			}
		}

		// Token: 0x020002FB RID: 763
		private sealed class Scheduler
		{
			// Token: 0x0600213A RID: 8506 RVA: 0x00077E83 File Offset: 0x00076083
			private void InitScheduler()
			{
				this.changed = new ManualResetEvent(false);
				new Thread(new ThreadStart(this.SchedulerThread))
				{
					IsBackground = true
				}.Start();
			}

			// Token: 0x0600213B RID: 8507 RVA: 0x00077EAE File Offset: 0x000760AE
			private void WakeupScheduler()
			{
				this.changed.Set();
			}

			// Token: 0x0600213C RID: 8508 RVA: 0x00077EBC File Offset: 0x000760BC
			private void SchedulerThread()
			{
				Thread.CurrentThread.Name = "Timer-Scheduler";
				for (;;)
				{
					int millisecondsTimeout = -1;
					lock (this)
					{
						this.changed.Reset();
						millisecondsTimeout = this.RunSchedulerLoop();
					}
					this.changed.WaitOne(millisecondsTimeout);
				}
			}

			// Token: 0x170003E2 RID: 994
			// (get) Token: 0x0600213D RID: 8509 RVA: 0x00077F24 File Offset: 0x00076124
			public static Timer.Scheduler Instance
			{
				get
				{
					return Timer.Scheduler.instance;
				}
			}

			// Token: 0x0600213E RID: 8510 RVA: 0x00077F2B File Offset: 0x0007612B
			private Scheduler()
			{
				this.list = new List<Timer>(1024);
				this.InitScheduler();
			}

			// Token: 0x0600213F RID: 8511 RVA: 0x00077F64 File Offset: 0x00076164
			public void Remove(Timer timer)
			{
				lock (this)
				{
					this.InternalRemove(timer);
				}
			}

			// Token: 0x06002140 RID: 8512 RVA: 0x00077FA0 File Offset: 0x000761A0
			public void Change(Timer timer, long new_next_run)
			{
				if (timer.is_dead)
				{
					timer.is_dead = false;
				}
				bool flag = false;
				lock (this)
				{
					this.needReSort = true;
					if (!timer.is_added)
					{
						timer.next_run = new_next_run;
						this.Add(timer);
						flag = (this.current_next_run > new_next_run);
					}
					else
					{
						if (new_next_run == 9223372036854775807L)
						{
							timer.next_run = new_next_run;
							this.InternalRemove(timer);
							return;
						}
						if (!timer.disposed)
						{
							timer.next_run = new_next_run;
							flag = (this.current_next_run > new_next_run);
						}
					}
				}
				if (flag)
				{
					this.WakeupScheduler();
				}
			}

			// Token: 0x06002141 RID: 8513 RVA: 0x00078050 File Offset: 0x00076250
			private void Add(Timer timer)
			{
				timer.is_added = true;
				this.needReSort = true;
				this.list.Add(timer);
				if (this.list.Count == 1)
				{
					this.WakeupScheduler();
				}
			}

			// Token: 0x06002142 RID: 8514 RVA: 0x00078082 File Offset: 0x00076282
			private void InternalRemove(Timer timer)
			{
				timer.is_dead = true;
				this.needReSort = true;
			}

			// Token: 0x06002143 RID: 8515 RVA: 0x00078094 File Offset: 0x00076294
			private static void TimerCB(object o)
			{
				Timer timer = (Timer)o;
				timer.callback(timer.state);
			}

			// Token: 0x06002144 RID: 8516 RVA: 0x000780BC File Offset: 0x000762BC
			private void FireTimer(Timer timer)
			{
				long period_ms = timer.period_ms;
				long due_time_ms = timer.due_time_ms;
				if (period_ms == -1L || ((period_ms == 0L || period_ms == -1L) && due_time_ms != -1L))
				{
					timer.next_run = long.MaxValue;
					timer.is_dead = true;
				}
				else
				{
					timer.next_run = Timer.GetTimeMonotonic() + 10000L * timer.period_ms;
					timer.is_dead = false;
				}
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(Timer.Scheduler.TimerCB), timer);
			}

			// Token: 0x06002145 RID: 8517 RVA: 0x00078140 File Offset: 0x00076340
			private int RunSchedulerLoop()
			{
				long timeMonotonic = Timer.GetTimeMonotonic();
				Timer.TimerComparer timerComparer = default(Timer.TimerComparer);
				if (this.needReSort)
				{
					this.list.Sort(timerComparer);
					this.needReSort = false;
				}
				long num = long.MaxValue;
				for (int i = 0; i < this.list.Count; i++)
				{
					Timer timer = this.list[i];
					if (!timer.is_dead)
					{
						if (timer.next_run <= timeMonotonic)
						{
							this.FireTimer(timer);
						}
						num = Math.Min(num, timer.next_run);
						if (timer.next_run > timeMonotonic && timer.next_run < 9223372036854775807L)
						{
							timer.is_dead = false;
						}
					}
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					Timer timer2 = this.list[i];
					if (timer2.is_dead)
					{
						timer2.is_added = false;
						this.needReSort = true;
						this.list[i] = this.list[this.list.Count - 1];
						i--;
						this.list.RemoveAt(this.list.Count - 1);
						if (this.list.Count == 0)
						{
							break;
						}
					}
				}
				if (this.needReSort)
				{
					this.list.Sort(timerComparer);
					this.needReSort = false;
				}
				int num2 = -1;
				this.current_next_run = num;
				if (num != 9223372036854775807L)
				{
					long num3 = (num - Timer.GetTimeMonotonic()) / 10000L;
					if (num3 > 2147483647L)
					{
						num2 = 2147483646;
					}
					else
					{
						num2 = (int)num3;
						if (num2 < 0)
						{
							num2 = 0;
						}
					}
				}
				return num2;
			}

			// Token: 0x06002146 RID: 8518 RVA: 0x000782F6 File Offset: 0x000764F6
			// Note: this type is marked as 'beforefieldinit'.
			static Scheduler()
			{
			}

			// Token: 0x04001BB7 RID: 7095
			private static readonly Timer.Scheduler instance = new Timer.Scheduler();

			// Token: 0x04001BB8 RID: 7096
			private volatile bool needReSort = true;

			// Token: 0x04001BB9 RID: 7097
			private List<Timer> list;

			// Token: 0x04001BBA RID: 7098
			private long current_next_run = long.MaxValue;

			// Token: 0x04001BBB RID: 7099
			private ManualResetEvent changed;
		}
	}
}
