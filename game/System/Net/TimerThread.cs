using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200063C RID: 1596
	internal static class TimerThread
	{
		// Token: 0x0600324D RID: 12877 RVA: 0x000AE0C8 File Offset: 0x000AC2C8
		static TimerThread()
		{
			TimerThread.s_ThreadEvents = new WaitHandle[]
			{
				TimerThread.s_ThreadShutdownEvent,
				TimerThread.s_ThreadReadyEvent
			};
			AppDomain.CurrentDomain.DomainUnload += TimerThread.OnDomainUnload;
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000AE140 File Offset: 0x000AC340
		internal static TimerThread.Queue CreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			LinkedList<WeakReference> obj = TimerThread.s_NewQueues;
			TimerThread.TimerQueue timerQueue;
			lock (obj)
			{
				timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
				WeakReference value = new WeakReference(timerQueue);
				TimerThread.s_NewQueues.AddLast(value);
			}
			return timerQueue;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000AE1B0 File Offset: 0x000AC3B0
		internal static TimerThread.Queue GetOrCreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			WeakReference weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
			TimerThread.TimerQueue timerQueue;
			if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
			{
				LinkedList<WeakReference> obj = TimerThread.s_NewQueues;
				lock (obj)
				{
					weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
					if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
					{
						timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
						weakReference = new WeakReference(timerQueue);
						TimerThread.s_NewQueues.AddLast(weakReference);
						TimerThread.s_QueuesCache[durationMilliseconds] = weakReference;
						if (++TimerThread.s_CacheScanIteration % 32 == 0)
						{
							List<int> list = new List<int>();
							foreach (object obj2 in TimerThread.s_QueuesCache)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
								if (((WeakReference)dictionaryEntry.Value).Target == null)
								{
									list.Add((int)dictionaryEntry.Key);
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								TimerThread.s_QueuesCache.Remove(list[i]);
							}
						}
					}
				}
			}
			return timerQueue;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000AE354 File Offset: 0x000AC554
		private static void Prod()
		{
			TimerThread.s_ThreadReadyEvent.Set();
			if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) == 0)
			{
				new Thread(new ThreadStart(TimerThread.ThreadProc)).Start();
			}
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000AE388 File Offset: 0x000AC588
		private static void ThreadProc()
		{
			Thread.CurrentThread.IsBackground = true;
			LinkedList<WeakReference> obj = TimerThread.s_Queues;
			lock (obj)
			{
				if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 1) == 1)
				{
					bool flag2 = true;
					while (flag2)
					{
						try
						{
							TimerThread.s_ThreadReadyEvent.Reset();
							for (;;)
							{
								if (TimerThread.s_NewQueues.Count > 0)
								{
									LinkedList<WeakReference> obj2 = TimerThread.s_NewQueues;
									lock (obj2)
									{
										for (LinkedListNode<WeakReference> first = TimerThread.s_NewQueues.First; first != null; first = TimerThread.s_NewQueues.First)
										{
											TimerThread.s_NewQueues.Remove(first);
											TimerThread.s_Queues.AddLast(first);
										}
									}
								}
								int tickCount = Environment.TickCount;
								int num = 0;
								bool flag4 = false;
								LinkedListNode<WeakReference> linkedListNode = TimerThread.s_Queues.First;
								while (linkedListNode != null)
								{
									TimerThread.TimerQueue timerQueue = (TimerThread.TimerQueue)linkedListNode.Value.Target;
									if (timerQueue == null)
									{
										LinkedListNode<WeakReference> next = linkedListNode.Next;
										TimerThread.s_Queues.Remove(linkedListNode);
										linkedListNode = next;
									}
									else
									{
										int num2;
										if (timerQueue.Fire(out num2) && (!flag4 || TimerThread.IsTickBetween(tickCount, num, num2)))
										{
											num = num2;
											flag4 = true;
										}
										linkedListNode = linkedListNode.Next;
									}
								}
								int tickCount2 = Environment.TickCount;
								int millisecondsTimeout = (int)(flag4 ? (TimerThread.IsTickBetween(tickCount, num, tickCount2) ? (Math.Min((uint)(num - tickCount2), 2147483632U) + 15U) : 0U) : 30000U);
								int num3 = WaitHandle.WaitAny(TimerThread.s_ThreadEvents, millisecondsTimeout, false);
								if (num3 == 0)
								{
									break;
								}
								if (num3 == 258 && !flag4)
								{
									Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 0, 1);
									if (!TimerThread.s_ThreadReadyEvent.WaitOne(0, false) || Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) != 0)
									{
										goto IL_1A8;
									}
								}
							}
							flag2 = false;
							continue;
							IL_1A8:
							flag2 = false;
						}
						catch (Exception exception)
						{
							if (NclUtilities.IsFatal(exception))
							{
								throw;
							}
							bool on = Logging.On;
							Thread.Sleep(1000);
						}
					}
				}
			}
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000AE5BC File Offset: 0x000AC7BC
		private static void StopTimerThread()
		{
			Interlocked.Exchange(ref TimerThread.s_ThreadState, 2);
			TimerThread.s_ThreadShutdownEvent.Set();
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000AE5D5 File Offset: 0x000AC7D5
		private static bool IsTickBetween(int start, int end, int comparand)
		{
			return start <= comparand == end <= comparand != start <= end;
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000AE5F4 File Offset: 0x000AC7F4
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			try
			{
				TimerThread.StopTimerThread();
			}
			catch
			{
			}
		}

		// Token: 0x04001D65 RID: 7525
		private const int c_ThreadIdleTimeoutMilliseconds = 30000;

		// Token: 0x04001D66 RID: 7526
		private const int c_CacheScanPerIterations = 32;

		// Token: 0x04001D67 RID: 7527
		private const int c_TickCountResolution = 15;

		// Token: 0x04001D68 RID: 7528
		private static LinkedList<WeakReference> s_Queues = new LinkedList<WeakReference>();

		// Token: 0x04001D69 RID: 7529
		private static LinkedList<WeakReference> s_NewQueues = new LinkedList<WeakReference>();

		// Token: 0x04001D6A RID: 7530
		private static int s_ThreadState = 0;

		// Token: 0x04001D6B RID: 7531
		private static AutoResetEvent s_ThreadReadyEvent = new AutoResetEvent(false);

		// Token: 0x04001D6C RID: 7532
		private static ManualResetEvent s_ThreadShutdownEvent = new ManualResetEvent(false);

		// Token: 0x04001D6D RID: 7533
		private static WaitHandle[] s_ThreadEvents;

		// Token: 0x04001D6E RID: 7534
		private static int s_CacheScanIteration;

		// Token: 0x04001D6F RID: 7535
		private static Hashtable s_QueuesCache = new Hashtable();

		// Token: 0x0200063D RID: 1597
		internal abstract class Queue
		{
			// Token: 0x06003255 RID: 12885 RVA: 0x000AE61C File Offset: 0x000AC81C
			internal Queue(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
			}

			// Token: 0x17000A16 RID: 2582
			// (get) Token: 0x06003256 RID: 12886 RVA: 0x000AE62B File Offset: 0x000AC82B
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x06003257 RID: 12887 RVA: 0x000AE633 File Offset: 0x000AC833
			internal TimerThread.Timer CreateTimer()
			{
				return this.CreateTimer(null, null);
			}

			// Token: 0x06003258 RID: 12888
			internal abstract TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context);

			// Token: 0x04001D70 RID: 7536
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x0200063E RID: 1598
		internal abstract class Timer : IDisposable
		{
			// Token: 0x06003259 RID: 12889 RVA: 0x000AE63D File Offset: 0x000AC83D
			internal Timer(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
				this.m_StartTimeMilliseconds = Environment.TickCount;
			}

			// Token: 0x17000A17 RID: 2583
			// (get) Token: 0x0600325A RID: 12890 RVA: 0x000AE657 File Offset: 0x000AC857
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000A18 RID: 2584
			// (get) Token: 0x0600325B RID: 12891 RVA: 0x000AE65F File Offset: 0x000AC85F
			internal int StartTime
			{
				get
				{
					return this.m_StartTimeMilliseconds;
				}
			}

			// Token: 0x17000A19 RID: 2585
			// (get) Token: 0x0600325C RID: 12892 RVA: 0x000AE667 File Offset: 0x000AC867
			internal int Expiration
			{
				get
				{
					return this.m_StartTimeMilliseconds + this.m_DurationMilliseconds;
				}
			}

			// Token: 0x17000A1A RID: 2586
			// (get) Token: 0x0600325D RID: 12893 RVA: 0x000AE678 File Offset: 0x000AC878
			internal int TimeRemaining
			{
				get
				{
					if (this.HasExpired)
					{
						return 0;
					}
					if (this.Duration == -1)
					{
						return -1;
					}
					int tickCount = Environment.TickCount;
					int num = (int)(TimerThread.IsTickBetween(this.StartTime, this.Expiration, tickCount) ? Math.Min((uint)(this.Expiration - tickCount), 2147483647U) : 0U);
					if (num >= 2)
					{
						return num;
					}
					return num + 1;
				}
			}

			// Token: 0x0600325E RID: 12894
			internal abstract bool Cancel();

			// Token: 0x17000A1B RID: 2587
			// (get) Token: 0x0600325F RID: 12895
			internal abstract bool HasExpired { get; }

			// Token: 0x06003260 RID: 12896 RVA: 0x000AE6D3 File Offset: 0x000AC8D3
			public void Dispose()
			{
				this.Cancel();
			}

			// Token: 0x04001D71 RID: 7537
			private readonly int m_StartTimeMilliseconds;

			// Token: 0x04001D72 RID: 7538
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x0200063F RID: 1599
		// (Invoke) Token: 0x06003262 RID: 12898
		internal delegate void Callback(TimerThread.Timer timer, int timeNoticed, object context);

		// Token: 0x02000640 RID: 1600
		private enum TimerThreadState
		{
			// Token: 0x04001D74 RID: 7540
			Idle,
			// Token: 0x04001D75 RID: 7541
			Running,
			// Token: 0x04001D76 RID: 7542
			Stopped
		}

		// Token: 0x02000641 RID: 1601
		private class TimerQueue : TimerThread.Queue
		{
			// Token: 0x06003265 RID: 12901 RVA: 0x000AE6DC File Offset: 0x000AC8DC
			internal TimerQueue(int durationMilliseconds) : base(durationMilliseconds)
			{
				this.m_Timers = new TimerThread.TimerNode();
				this.m_Timers.Next = this.m_Timers;
				this.m_Timers.Prev = this.m_Timers;
			}

			// Token: 0x06003266 RID: 12902 RVA: 0x000AE714 File Offset: 0x000AC914
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				TimerThread.TimerNode timerNode = new TimerThread.TimerNode(callback, context, base.Duration, this.m_Timers);
				bool flag = false;
				TimerThread.TimerNode timers = this.m_Timers;
				lock (timers)
				{
					if (this.m_Timers.Next == this.m_Timers)
					{
						if (this.m_ThisHandle == IntPtr.Zero)
						{
							this.m_ThisHandle = (IntPtr)GCHandle.Alloc(this);
						}
						flag = true;
					}
					timerNode.Next = this.m_Timers;
					timerNode.Prev = this.m_Timers.Prev;
					this.m_Timers.Prev.Next = timerNode;
					this.m_Timers.Prev = timerNode;
				}
				if (flag)
				{
					TimerThread.Prod();
				}
				return timerNode;
			}

			// Token: 0x06003267 RID: 12903 RVA: 0x000AE7E0 File Offset: 0x000AC9E0
			internal bool Fire(out int nextExpiration)
			{
				TimerThread.TimerNode next;
				do
				{
					next = this.m_Timers.Next;
					if (next == this.m_Timers)
					{
						TimerThread.TimerNode timers = this.m_Timers;
						lock (timers)
						{
							next = this.m_Timers.Next;
							if (next == this.m_Timers)
							{
								if (this.m_ThisHandle != IntPtr.Zero)
								{
									((GCHandle)this.m_ThisHandle).Free();
									this.m_ThisHandle = IntPtr.Zero;
								}
								nextExpiration = 0;
								return false;
							}
						}
					}
				}
				while (next.Fire());
				nextExpiration = next.Expiration;
				return true;
			}

			// Token: 0x04001D77 RID: 7543
			private IntPtr m_ThisHandle;

			// Token: 0x04001D78 RID: 7544
			private readonly TimerThread.TimerNode m_Timers;
		}

		// Token: 0x02000642 RID: 1602
		private class InfiniteTimerQueue : TimerThread.Queue
		{
			// Token: 0x06003268 RID: 12904 RVA: 0x000AE894 File Offset: 0x000ACA94
			internal InfiniteTimerQueue() : base(-1)
			{
			}

			// Token: 0x06003269 RID: 12905 RVA: 0x000AE89D File Offset: 0x000ACA9D
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				return new TimerThread.InfiniteTimer();
			}
		}

		// Token: 0x02000643 RID: 1603
		private class TimerNode : TimerThread.Timer
		{
			// Token: 0x0600326A RID: 12906 RVA: 0x000AE8A4 File Offset: 0x000ACAA4
			internal TimerNode(TimerThread.Callback callback, object context, int durationMilliseconds, object queueLock) : base(durationMilliseconds)
			{
				if (callback != null)
				{
					this.m_Callback = callback;
					this.m_Context = context;
				}
				this.m_TimerState = TimerThread.TimerNode.TimerState.Ready;
				this.m_QueueLock = queueLock;
			}

			// Token: 0x0600326B RID: 12907 RVA: 0x000AE8CD File Offset: 0x000ACACD
			internal TimerNode() : base(0)
			{
				this.m_TimerState = TimerThread.TimerNode.TimerState.Sentinel;
			}

			// Token: 0x17000A1C RID: 2588
			// (get) Token: 0x0600326C RID: 12908 RVA: 0x000AE8DD File Offset: 0x000ACADD
			internal override bool HasExpired
			{
				get
				{
					return this.m_TimerState == TimerThread.TimerNode.TimerState.Fired;
				}
			}

			// Token: 0x17000A1D RID: 2589
			// (get) Token: 0x0600326D RID: 12909 RVA: 0x000AE8E8 File Offset: 0x000ACAE8
			// (set) Token: 0x0600326E RID: 12910 RVA: 0x000AE8F0 File Offset: 0x000ACAF0
			internal TimerThread.TimerNode Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x17000A1E RID: 2590
			// (get) Token: 0x0600326F RID: 12911 RVA: 0x000AE8F9 File Offset: 0x000ACAF9
			// (set) Token: 0x06003270 RID: 12912 RVA: 0x000AE901 File Offset: 0x000ACB01
			internal TimerThread.TimerNode Prev
			{
				get
				{
					return this.prev;
				}
				set
				{
					this.prev = value;
				}
			}

			// Token: 0x06003271 RID: 12913 RVA: 0x000AE90C File Offset: 0x000ACB0C
			internal override bool Cancel()
			{
				if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
				{
					object queueLock = this.m_QueueLock;
					lock (queueLock)
					{
						if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
						{
							this.Next.Prev = this.Prev;
							this.Prev.Next = this.Next;
							this.Next = null;
							this.Prev = null;
							this.m_Callback = null;
							this.m_Context = null;
							this.m_TimerState = TimerThread.TimerNode.TimerState.Cancelled;
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x06003272 RID: 12914 RVA: 0x000AE9A4 File Offset: 0x000ACBA4
			internal bool Fire()
			{
				if (this.m_TimerState != TimerThread.TimerNode.TimerState.Ready)
				{
					return true;
				}
				int tickCount = Environment.TickCount;
				if (TimerThread.IsTickBetween(base.StartTime, base.Expiration, tickCount))
				{
					return false;
				}
				bool flag = false;
				object queueLock = this.m_QueueLock;
				lock (queueLock)
				{
					if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
					{
						this.m_TimerState = TimerThread.TimerNode.TimerState.Fired;
						this.Next.Prev = this.Prev;
						this.Prev.Next = this.Next;
						this.Next = null;
						this.Prev = null;
						flag = (this.m_Callback != null);
					}
				}
				if (flag)
				{
					try
					{
						TimerThread.Callback callback = this.m_Callback;
						object context = this.m_Context;
						this.m_Callback = null;
						this.m_Context = null;
						callback(this, tickCount, context);
					}
					catch (Exception exception)
					{
						if (NclUtilities.IsFatal(exception))
						{
							throw;
						}
						bool on = Logging.On;
					}
				}
				return true;
			}

			// Token: 0x04001D79 RID: 7545
			private TimerThread.TimerNode.TimerState m_TimerState;

			// Token: 0x04001D7A RID: 7546
			private TimerThread.Callback m_Callback;

			// Token: 0x04001D7B RID: 7547
			private object m_Context;

			// Token: 0x04001D7C RID: 7548
			private object m_QueueLock;

			// Token: 0x04001D7D RID: 7549
			private TimerThread.TimerNode next;

			// Token: 0x04001D7E RID: 7550
			private TimerThread.TimerNode prev;

			// Token: 0x02000644 RID: 1604
			private enum TimerState
			{
				// Token: 0x04001D80 RID: 7552
				Ready,
				// Token: 0x04001D81 RID: 7553
				Fired,
				// Token: 0x04001D82 RID: 7554
				Cancelled,
				// Token: 0x04001D83 RID: 7555
				Sentinel
			}
		}

		// Token: 0x02000645 RID: 1605
		private class InfiniteTimer : TimerThread.Timer
		{
			// Token: 0x06003273 RID: 12915 RVA: 0x000AEA98 File Offset: 0x000ACC98
			internal InfiniteTimer() : base(-1)
			{
			}

			// Token: 0x17000A1F RID: 2591
			// (get) Token: 0x06003274 RID: 12916 RVA: 0x00003062 File Offset: 0x00001262
			internal override bool HasExpired
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003275 RID: 12917 RVA: 0x000AEAA1 File Offset: 0x000ACCA1
			internal override bool Cancel()
			{
				return Interlocked.Exchange(ref this.cancelled, 1) == 0;
			}

			// Token: 0x04001D84 RID: 7556
			private int cancelled;
		}
	}
}
