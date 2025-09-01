using System;
using System.ComponentModel;
using System.Runtime.Interop;
using System.Security;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime
{
	// Token: 0x02000021 RID: 33
	internal class IOThreadTimer
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x0000466D File Offset: 0x0000286D
		public IOThreadTimer(Action<object> callback, object callbackState, bool isTypicallyCanceledShortlyAfterBeingSet) : this(callback, callbackState, isTypicallyCanceledShortlyAfterBeingSet, 100)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000467C File Offset: 0x0000287C
		public IOThreadTimer(Action<object> callback, object callbackState, bool isTypicallyCanceledShortlyAfterBeingSet, int maxSkewInMilliseconds)
		{
			this.callback = callback;
			this.callbackState = callbackState;
			this.maxSkew = Ticks.FromMilliseconds(maxSkewInMilliseconds);
			this.timerGroup = (isTypicallyCanceledShortlyAfterBeingSet ? IOThreadTimer.TimerManager.Value.VolatileTimerGroup : IOThreadTimer.TimerManager.Value.StableTimerGroup);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000046C9 File Offset: 0x000028C9
		public static long SystemTimeResolutionTicks
		{
			get
			{
				if (IOThreadTimer.systemTimeResolutionTicks == -1L)
				{
					IOThreadTimer.systemTimeResolutionTicks = IOThreadTimer.GetSystemTimeResolution();
				}
				return IOThreadTimer.systemTimeResolutionTicks;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000046E4 File Offset: 0x000028E4
		[SecuritySafeCritical]
		private static long GetSystemTimeResolution()
		{
			int num;
			uint num2;
			uint num3;
			if (UnsafeNativeMethods.GetSystemTimeAdjustment(out num, out num2, out num3) != 0U)
			{
				return (long)((ulong)num2);
			}
			return 150000L;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00004707 File Offset: 0x00002907
		public bool Cancel()
		{
			return IOThreadTimer.TimerManager.Value.Cancel(this);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004714 File Offset: 0x00002914
		public void Set(TimeSpan timeFromNow)
		{
			if (timeFromNow != TimeSpan.MaxValue)
			{
				this.SetAt(Ticks.Add(Ticks.Now, Ticks.FromTimeSpan(timeFromNow)));
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004739 File Offset: 0x00002939
		public void Set(int millisecondsFromNow)
		{
			this.SetAt(Ticks.Add(Ticks.Now, Ticks.FromMilliseconds(millisecondsFromNow)));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004751 File Offset: 0x00002951
		public void SetAt(long dueTime)
		{
			IOThreadTimer.TimerManager.Value.Set(this, dueTime);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000475F File Offset: 0x0000295F
		// Note: this type is marked as 'beforefieldinit'.
		static IOThreadTimer()
		{
		}

		// Token: 0x040000A9 RID: 169
		private const int maxSkewInMillisecondsDefault = 100;

		// Token: 0x040000AA RID: 170
		private static long systemTimeResolutionTicks = -1L;

		// Token: 0x040000AB RID: 171
		private Action<object> callback;

		// Token: 0x040000AC RID: 172
		private object callbackState;

		// Token: 0x040000AD RID: 173
		private long dueTime;

		// Token: 0x040000AE RID: 174
		private int index;

		// Token: 0x040000AF RID: 175
		private long maxSkew;

		// Token: 0x040000B0 RID: 176
		private IOThreadTimer.TimerGroup timerGroup;

		// Token: 0x02000070 RID: 112
		private class TimerManager
		{
			// Token: 0x06000389 RID: 905 RVA: 0x000114F8 File Offset: 0x0000F6F8
			public TimerManager()
			{
				this.onWaitCallback = new Action<object>(this.OnWaitCallback);
				this.stableTimerGroup = new IOThreadTimer.TimerGroup();
				this.volatileTimerGroup = new IOThreadTimer.TimerGroup();
				this.waitableTimers = new IOThreadTimer.WaitableTimer[]
				{
					this.stableTimerGroup.WaitableTimer,
					this.volatileTimerGroup.WaitableTimer
				};
			}

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x0600038A RID: 906 RVA: 0x0001155B File Offset: 0x0000F75B
			private object ThisLock
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x0600038B RID: 907 RVA: 0x0001155E File Offset: 0x0000F75E
			public static IOThreadTimer.TimerManager Value
			{
				get
				{
					return IOThreadTimer.TimerManager.value;
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x0600038C RID: 908 RVA: 0x00011565 File Offset: 0x0000F765
			public IOThreadTimer.TimerGroup StableTimerGroup
			{
				get
				{
					return this.stableTimerGroup;
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x0600038D RID: 909 RVA: 0x0001156D File Offset: 0x0000F76D
			public IOThreadTimer.TimerGroup VolatileTimerGroup
			{
				get
				{
					return this.volatileTimerGroup;
				}
			}

			// Token: 0x0600038E RID: 910 RVA: 0x00011578 File Offset: 0x0000F778
			public void Set(IOThreadTimer timer, long dueTime)
			{
				long num = dueTime - timer.dueTime;
				if (num < 0L)
				{
					num = -num;
				}
				if (num > timer.maxSkew)
				{
					object thisLock = this.ThisLock;
					lock (thisLock)
					{
						IOThreadTimer.TimerGroup timerGroup = timer.timerGroup;
						IOThreadTimer.TimerQueue timerQueue = timerGroup.TimerQueue;
						if (timer.index > 0)
						{
							if (timerQueue.UpdateTimer(timer, dueTime))
							{
								this.UpdateWaitableTimer(timerGroup);
							}
						}
						else if (timerQueue.InsertTimer(timer, dueTime))
						{
							this.UpdateWaitableTimer(timerGroup);
							if (timerQueue.Count == 1)
							{
								this.EnsureWaitScheduled();
							}
						}
					}
				}
			}

			// Token: 0x0600038F RID: 911 RVA: 0x0001161C File Offset: 0x0000F81C
			public bool Cancel(IOThreadTimer timer)
			{
				object thisLock = this.ThisLock;
				bool result;
				lock (thisLock)
				{
					if (timer.index > 0)
					{
						IOThreadTimer.TimerGroup timerGroup = timer.timerGroup;
						IOThreadTimer.TimerQueue timerQueue = timerGroup.TimerQueue;
						timerQueue.DeleteTimer(timer);
						if (timerQueue.Count > 0)
						{
							this.UpdateWaitableTimer(timerGroup);
						}
						else
						{
							IOThreadTimer.TimerGroup otherTimerGroup = this.GetOtherTimerGroup(timerGroup);
							if (otherTimerGroup.TimerQueue.Count == 0)
							{
								long now = Ticks.Now;
								long num = timerGroup.WaitableTimer.DueTime - now;
								long num2 = otherTimerGroup.WaitableTimer.DueTime - now;
								if (num > 10000000L && num2 > 10000000L)
								{
									timerGroup.WaitableTimer.Set(Ticks.Add(now, 10000000L));
								}
							}
						}
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06000390 RID: 912 RVA: 0x000116F8 File Offset: 0x0000F8F8
			private void EnsureWaitScheduled()
			{
				if (!this.waitScheduled)
				{
					this.ScheduleWait();
				}
			}

			// Token: 0x06000391 RID: 913 RVA: 0x00011708 File Offset: 0x0000F908
			private IOThreadTimer.TimerGroup GetOtherTimerGroup(IOThreadTimer.TimerGroup timerGroup)
			{
				if (timerGroup == this.volatileTimerGroup)
				{
					return this.stableTimerGroup;
				}
				return this.volatileTimerGroup;
			}

			// Token: 0x06000392 RID: 914 RVA: 0x00011720 File Offset: 0x0000F920
			private void OnWaitCallback(object state)
			{
				WaitHandle[] waitHandles = this.waitableTimers;
				WaitHandle.WaitAny(waitHandles);
				long now = Ticks.Now;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.waitScheduled = false;
					this.ScheduleElapsedTimers(now);
					this.ReactivateWaitableTimers();
					this.ScheduleWaitIfAnyTimersLeft();
				}
			}

			// Token: 0x06000393 RID: 915 RVA: 0x00011788 File Offset: 0x0000F988
			private void ReactivateWaitableTimers()
			{
				this.ReactivateWaitableTimer(this.stableTimerGroup);
				this.ReactivateWaitableTimer(this.volatileTimerGroup);
			}

			// Token: 0x06000394 RID: 916 RVA: 0x000117A4 File Offset: 0x0000F9A4
			private void ReactivateWaitableTimer(IOThreadTimer.TimerGroup timerGroup)
			{
				IOThreadTimer.TimerQueue timerQueue = timerGroup.TimerQueue;
				if (timerQueue.Count > 0)
				{
					timerGroup.WaitableTimer.Set(timerQueue.MinTimer.dueTime);
					return;
				}
				timerGroup.WaitableTimer.Set(long.MaxValue);
			}

			// Token: 0x06000395 RID: 917 RVA: 0x000117EC File Offset: 0x0000F9EC
			private void ScheduleElapsedTimers(long now)
			{
				this.ScheduleElapsedTimers(this.stableTimerGroup, now);
				this.ScheduleElapsedTimers(this.volatileTimerGroup, now);
			}

			// Token: 0x06000396 RID: 918 RVA: 0x00011808 File Offset: 0x0000FA08
			private void ScheduleElapsedTimers(IOThreadTimer.TimerGroup timerGroup, long now)
			{
				IOThreadTimer.TimerQueue timerQueue = timerGroup.TimerQueue;
				while (timerQueue.Count > 0)
				{
					IOThreadTimer minTimer = timerQueue.MinTimer;
					if (minTimer.dueTime - now > minTimer.maxSkew)
					{
						break;
					}
					timerQueue.DeleteMinTimer();
					ActionItem.Schedule(minTimer.callback, minTimer.callbackState);
				}
			}

			// Token: 0x06000397 RID: 919 RVA: 0x00011855 File Offset: 0x0000FA55
			private void ScheduleWait()
			{
				ActionItem.Schedule(this.onWaitCallback, null);
				this.waitScheduled = true;
			}

			// Token: 0x06000398 RID: 920 RVA: 0x0001186A File Offset: 0x0000FA6A
			private void ScheduleWaitIfAnyTimersLeft()
			{
				if (this.stableTimerGroup.TimerQueue.Count > 0 || this.volatileTimerGroup.TimerQueue.Count > 0)
				{
					this.ScheduleWait();
				}
			}

			// Token: 0x06000399 RID: 921 RVA: 0x00011898 File Offset: 0x0000FA98
			private void UpdateWaitableTimer(IOThreadTimer.TimerGroup timerGroup)
			{
				IOThreadTimer.WaitableTimer waitableTimer = timerGroup.WaitableTimer;
				IOThreadTimer minTimer = timerGroup.TimerQueue.MinTimer;
				long num = waitableTimer.DueTime - minTimer.dueTime;
				if (num < 0L)
				{
					num = -num;
				}
				if (num > minTimer.maxSkew)
				{
					waitableTimer.Set(minTimer.dueTime);
				}
			}

			// Token: 0x0600039A RID: 922 RVA: 0x000118E3 File Offset: 0x0000FAE3
			// Note: this type is marked as 'beforefieldinit'.
			static TimerManager()
			{
			}

			// Token: 0x04000284 RID: 644
			private const long maxTimeToWaitForMoreTimers = 10000000L;

			// Token: 0x04000285 RID: 645
			private static IOThreadTimer.TimerManager value = new IOThreadTimer.TimerManager();

			// Token: 0x04000286 RID: 646
			private Action<object> onWaitCallback;

			// Token: 0x04000287 RID: 647
			private IOThreadTimer.TimerGroup stableTimerGroup;

			// Token: 0x04000288 RID: 648
			private IOThreadTimer.TimerGroup volatileTimerGroup;

			// Token: 0x04000289 RID: 649
			private IOThreadTimer.WaitableTimer[] waitableTimers;

			// Token: 0x0400028A RID: 650
			private bool waitScheduled;
		}

		// Token: 0x02000071 RID: 113
		private class TimerGroup
		{
			// Token: 0x0600039B RID: 923 RVA: 0x000118EF File Offset: 0x0000FAEF
			public TimerGroup()
			{
				this.waitableTimer = new IOThreadTimer.WaitableTimer();
				this.waitableTimer.Set(long.MaxValue);
				this.timerQueue = new IOThreadTimer.TimerQueue();
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x0600039C RID: 924 RVA: 0x00011921 File Offset: 0x0000FB21
			public IOThreadTimer.TimerQueue TimerQueue
			{
				get
				{
					return this.timerQueue;
				}
			}

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x0600039D RID: 925 RVA: 0x00011929 File Offset: 0x0000FB29
			public IOThreadTimer.WaitableTimer WaitableTimer
			{
				get
				{
					return this.waitableTimer;
				}
			}

			// Token: 0x0400028B RID: 651
			private IOThreadTimer.TimerQueue timerQueue;

			// Token: 0x0400028C RID: 652
			private IOThreadTimer.WaitableTimer waitableTimer;
		}

		// Token: 0x02000072 RID: 114
		private class TimerQueue
		{
			// Token: 0x0600039E RID: 926 RVA: 0x00011931 File Offset: 0x0000FB31
			public TimerQueue()
			{
				this.timers = new IOThreadTimer[4];
			}

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x0600039F RID: 927 RVA: 0x00011945 File Offset: 0x0000FB45
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x060003A0 RID: 928 RVA: 0x0001194D File Offset: 0x0000FB4D
			public IOThreadTimer MinTimer
			{
				get
				{
					return this.timers[1];
				}
			}

			// Token: 0x060003A1 RID: 929 RVA: 0x00011957 File Offset: 0x0000FB57
			public void DeleteMinTimer()
			{
				IOThreadTimer minTimer = this.MinTimer;
				this.DeleteMinTimerCore();
				minTimer.index = 0;
				minTimer.dueTime = 0L;
			}

			// Token: 0x060003A2 RID: 930 RVA: 0x00011974 File Offset: 0x0000FB74
			public void DeleteTimer(IOThreadTimer timer)
			{
				int num = timer.index;
				IOThreadTimer[] array = this.timers;
				for (;;)
				{
					int num2 = num / 2;
					if (num2 < 1)
					{
						break;
					}
					IOThreadTimer iothreadTimer = array[num2];
					array[num] = iothreadTimer;
					iothreadTimer.index = num;
					num = num2;
				}
				timer.index = 0;
				timer.dueTime = 0L;
				array[1] = null;
				this.DeleteMinTimerCore();
			}

			// Token: 0x060003A3 RID: 931 RVA: 0x000119C4 File Offset: 0x0000FBC4
			public bool InsertTimer(IOThreadTimer timer, long dueTime)
			{
				IOThreadTimer[] array = this.timers;
				int num = this.count + 1;
				if (num == array.Length)
				{
					array = new IOThreadTimer[array.Length * 2];
					Array.Copy(this.timers, array, this.timers.Length);
					this.timers = array;
				}
				this.count = num;
				if (num > 1)
				{
					for (;;)
					{
						int num2 = num / 2;
						if (num2 == 0)
						{
							break;
						}
						IOThreadTimer iothreadTimer = array[num2];
						if (iothreadTimer.dueTime <= dueTime)
						{
							break;
						}
						array[num] = iothreadTimer;
						iothreadTimer.index = num;
						num = num2;
					}
				}
				array[num] = timer;
				timer.index = num;
				timer.dueTime = dueTime;
				return num == 1;
			}

			// Token: 0x060003A4 RID: 932 RVA: 0x00011A54 File Offset: 0x0000FC54
			public bool UpdateTimer(IOThreadTimer timer, long dueTime)
			{
				int index = timer.index;
				IOThreadTimer[] array = this.timers;
				int num = this.count;
				int num2 = index / 2;
				if (num2 == 0 || array[num2].dueTime <= dueTime)
				{
					int num3 = index * 2;
					if (num3 > num || array[num3].dueTime >= dueTime)
					{
						int num4 = num3 + 1;
						if (num4 > num || array[num4].dueTime >= dueTime)
						{
							timer.dueTime = dueTime;
							return index == 1;
						}
					}
				}
				this.DeleteTimer(timer);
				this.InsertTimer(timer, dueTime);
				return true;
			}

			// Token: 0x060003A5 RID: 933 RVA: 0x00011AD4 File Offset: 0x0000FCD4
			private void DeleteMinTimerCore()
			{
				int num = this.count;
				if (num == 1)
				{
					this.count = 0;
					this.timers[1] = null;
					return;
				}
				IOThreadTimer[] array = this.timers;
				IOThreadTimer iothreadTimer = array[num];
				num = (this.count = num - 1);
				int num2 = 1;
				int num3;
				do
				{
					num3 = num2 * 2;
					if (num3 > num)
					{
						break;
					}
					IOThreadTimer iothreadTimer4;
					int num5;
					if (num3 < num)
					{
						IOThreadTimer iothreadTimer2 = array[num3];
						int num4 = num3 + 1;
						IOThreadTimer iothreadTimer3 = array[num4];
						if (iothreadTimer3.dueTime < iothreadTimer2.dueTime)
						{
							iothreadTimer4 = iothreadTimer3;
							num5 = num4;
						}
						else
						{
							iothreadTimer4 = iothreadTimer2;
							num5 = num3;
						}
					}
					else
					{
						num5 = num3;
						iothreadTimer4 = array[num5];
					}
					if (iothreadTimer.dueTime <= iothreadTimer4.dueTime)
					{
						break;
					}
					array[num2] = iothreadTimer4;
					iothreadTimer4.index = num2;
					num2 = num5;
				}
				while (num3 < num);
				array[num2] = iothreadTimer;
				iothreadTimer.index = num2;
				array[num + 1] = null;
			}

			// Token: 0x0400028D RID: 653
			private int count;

			// Token: 0x0400028E RID: 654
			private IOThreadTimer[] timers;
		}

		// Token: 0x02000073 RID: 115
		private class WaitableTimer : WaitHandle
		{
			// Token: 0x060003A6 RID: 934 RVA: 0x00011B99 File Offset: 0x0000FD99
			[SecuritySafeCritical]
			public WaitableTimer()
			{
				base.SafeWaitHandle = IOThreadTimer.WaitableTimer.TimerHelper.CreateWaitableTimer();
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060003A7 RID: 935 RVA: 0x00011BAC File Offset: 0x0000FDAC
			public long DueTime
			{
				get
				{
					return this.dueTime;
				}
			}

			// Token: 0x060003A8 RID: 936 RVA: 0x00011BB4 File Offset: 0x0000FDB4
			[SecuritySafeCritical]
			public void Set(long dueTime)
			{
				this.dueTime = IOThreadTimer.WaitableTimer.TimerHelper.Set(base.SafeWaitHandle, dueTime);
			}

			// Token: 0x0400028F RID: 655
			private long dueTime;

			// Token: 0x020000B3 RID: 179
			[SecurityCritical]
			private static class TimerHelper
			{
				// Token: 0x06000491 RID: 1169 RVA: 0x00013AB0 File Offset: 0x00011CB0
				public static SafeWaitHandle CreateWaitableTimer()
				{
					SafeWaitHandle safeWaitHandle = UnsafeNativeMethods.CreateWaitableTimer(IntPtr.Zero, false, null);
					if (safeWaitHandle.IsInvalid)
					{
						Exception exception = new Win32Exception();
						safeWaitHandle.SetHandleAsInvalid();
						throw Fx.Exception.AsError(exception);
					}
					return safeWaitHandle;
				}

				// Token: 0x06000492 RID: 1170 RVA: 0x00013AEB File Offset: 0x00011CEB
				public static long Set(SafeWaitHandle timer, long dueTime)
				{
					if (!UnsafeNativeMethods.SetWaitableTimer(timer, ref dueTime, 0, IntPtr.Zero, IntPtr.Zero, false))
					{
						throw Fx.Exception.AsError(new Win32Exception());
					}
					return dueTime;
				}
			}
		}
	}
}
