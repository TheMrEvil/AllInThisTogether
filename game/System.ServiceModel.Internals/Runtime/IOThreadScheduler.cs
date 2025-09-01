using System;
using System.Security;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000020 RID: 32
	internal class IOThreadScheduler
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004200 File Offset: 0x00002400
		[SecuritySafeCritical]
		private IOThreadScheduler(int capacity, int capacityLowPri)
		{
			this.slots = new IOThreadScheduler.Slot[capacity];
			this.slotsLowPri = new IOThreadScheduler.Slot[capacityLowPri];
			this.overlapped = new IOThreadScheduler.ScheduledOverlapped();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000424C File Offset: 0x0000244C
		[SecurityCritical]
		public static void ScheduleCallbackNoFlow(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			bool flag = false;
			while (!flag)
			{
				try
				{
				}
				finally
				{
					flag = IOThreadScheduler.current.ScheduleCallbackHelper(callback, state);
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004294 File Offset: 0x00002494
		[SecurityCritical]
		public static void ScheduleCallbackLowPriNoFlow(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			bool flag = false;
			while (!flag)
			{
				try
				{
				}
				finally
				{
					flag = IOThreadScheduler.current.ScheduleCallbackLowPriHelper(callback, state);
				}
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000042DC File Offset: 0x000024DC
		[SecurityCritical]
		private bool ScheduleCallbackHelper(Action<object> callback, object state)
		{
			int num = Interlocked.Add(ref this.headTail, 65536);
			bool flag = IOThreadScheduler.Bits.Count(num) == 0;
			if (flag)
			{
				num = Interlocked.Add(ref this.headTail, 65536);
			}
			if (IOThreadScheduler.Bits.Count(num) == -1)
			{
				throw Fx.AssertAndThrowFatal("Head/Tail overflow!");
			}
			bool flag2;
			bool result = this.slots[num >> 16 & this.SlotMask].TryEnqueueWorkItem(callback, state, out flag2);
			if (flag2)
			{
				IOThreadScheduler value = new IOThreadScheduler(Math.Min(this.slots.Length * 2, 32768), this.slotsLowPri.Length);
				Interlocked.CompareExchange<IOThreadScheduler>(ref IOThreadScheduler.current, value, this);
			}
			if (flag)
			{
				this.overlapped.Post(this);
			}
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000438C File Offset: 0x0000258C
		[SecurityCritical]
		private bool ScheduleCallbackLowPriHelper(Action<object> callback, object state)
		{
			int num = Interlocked.Add(ref this.headTailLowPri, 65536);
			bool flag = false;
			if (IOThreadScheduler.Bits.CountNoIdle(num) == 1)
			{
				int num2 = this.headTail;
				if (IOThreadScheduler.Bits.Count(num2) == -1)
				{
					int num3 = Interlocked.CompareExchange(ref this.headTail, num2 + 65536, num2);
					if (num2 == num3)
					{
						flag = true;
					}
				}
			}
			if (IOThreadScheduler.Bits.CountNoIdle(num) == 0)
			{
				throw Fx.AssertAndThrowFatal("Low-priority Head/Tail overflow!");
			}
			bool flag2;
			bool result = this.slotsLowPri[num >> 16 & this.SlotMaskLowPri].TryEnqueueWorkItem(callback, state, out flag2);
			if (flag2)
			{
				IOThreadScheduler value = new IOThreadScheduler(this.slots.Length, Math.Min(this.slotsLowPri.Length * 2, 32768));
				Interlocked.CompareExchange<IOThreadScheduler>(ref IOThreadScheduler.current, value, this);
			}
			if (flag)
			{
				this.overlapped.Post(this);
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004458 File Offset: 0x00002658
		[SecurityCritical]
		private void CompletionCallback(out Action<object> callback, out object state)
		{
			int num = this.headTail;
			int num2;
			for (;;)
			{
				bool flag = IOThreadScheduler.Bits.Count(num) == 0;
				if (flag)
				{
					num2 = this.headTailLowPri;
					while (IOThreadScheduler.Bits.CountNoIdle(num2) != 0)
					{
						if (num2 == (num2 = Interlocked.CompareExchange(ref this.headTailLowPri, IOThreadScheduler.Bits.IncrementLo(num2), num2)))
						{
							goto Block_2;
						}
					}
				}
				if (num == (num = Interlocked.CompareExchange(ref this.headTail, IOThreadScheduler.Bits.IncrementLo(num), num)))
				{
					if (!flag)
					{
						goto Block_4;
					}
					num2 = this.headTailLowPri;
					if (IOThreadScheduler.Bits.CountNoIdle(num2) == 0)
					{
						goto IL_DD;
					}
					num = IOThreadScheduler.Bits.IncrementLo(num);
					if (num != Interlocked.CompareExchange(ref this.headTail, num + 65536, num))
					{
						goto IL_DD;
					}
					num += 65536;
				}
			}
			Block_2:
			this.overlapped.Post(this);
			this.slotsLowPri[num2 & this.SlotMaskLowPri].DequeueWorkItem(out callback, out state);
			return;
			Block_4:
			this.overlapped.Post(this);
			this.slots[num & this.SlotMask].DequeueWorkItem(out callback, out state);
			return;
			IL_DD:
			callback = null;
			state = null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004548 File Offset: 0x00002748
		[SecurityCritical]
		private bool TryCoalesce(out Action<object> callback, out object state)
		{
			int num = this.headTail;
			int num2;
			for (;;)
			{
				if (IOThreadScheduler.Bits.Count(num) > 0)
				{
					if (num == (num = Interlocked.CompareExchange(ref this.headTail, IOThreadScheduler.Bits.IncrementLo(num), num)))
					{
						break;
					}
				}
				else
				{
					num2 = this.headTailLowPri;
					if (IOThreadScheduler.Bits.CountNoIdle(num2) <= 0)
					{
						goto IL_92;
					}
					if (num2 == (num2 = Interlocked.CompareExchange(ref this.headTailLowPri, IOThreadScheduler.Bits.IncrementLo(num2), num2)))
					{
						goto Block_4;
					}
					num = this.headTail;
				}
			}
			this.slots[num & this.SlotMask].DequeueWorkItem(out callback, out state);
			return true;
			Block_4:
			this.slotsLowPri[num2 & this.SlotMaskLowPri].DequeueWorkItem(out callback, out state);
			return true;
			IL_92:
			callback = null;
			state = null;
			return false;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000045EE File Offset: 0x000027EE
		private int SlotMask
		{
			[SecurityCritical]
			get
			{
				return this.slots.Length - 1;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000045FA File Offset: 0x000027FA
		private int SlotMaskLowPri
		{
			[SecurityCritical]
			get
			{
				return this.slotsLowPri.Length - 1;
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004608 File Offset: 0x00002808
		~IOThreadScheduler()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.Cleanup();
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004648 File Offset: 0x00002848
		[SecuritySafeCritical]
		private void Cleanup()
		{
			if (this.overlapped != null)
			{
				this.overlapped.Cleanup();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000465D File Offset: 0x0000285D
		// Note: this type is marked as 'beforefieldinit'.
		static IOThreadScheduler()
		{
		}

		// Token: 0x040000A2 RID: 162
		private const int MaximumCapacity = 32768;

		// Token: 0x040000A3 RID: 163
		private static IOThreadScheduler current = new IOThreadScheduler(32, 32);

		// Token: 0x040000A4 RID: 164
		private readonly IOThreadScheduler.ScheduledOverlapped overlapped;

		// Token: 0x040000A5 RID: 165
		[SecurityCritical]
		private readonly IOThreadScheduler.Slot[] slots;

		// Token: 0x040000A6 RID: 166
		[SecurityCritical]
		private readonly IOThreadScheduler.Slot[] slotsLowPri;

		// Token: 0x040000A7 RID: 167
		private int headTail = -131072;

		// Token: 0x040000A8 RID: 168
		private int headTailLowPri = -65536;

		// Token: 0x0200006D RID: 109
		private static class Bits
		{
			// Token: 0x0600037F RID: 895 RVA: 0x00011266 File Offset: 0x0000F466
			public static int Count(int slot)
			{
				return ((slot >> 16) - slot + 2 & 65535) - 1;
			}

			// Token: 0x06000380 RID: 896 RVA: 0x00011278 File Offset: 0x0000F478
			public static int CountNoIdle(int slot)
			{
				return (slot >> 16) - slot + 1 & 65535;
			}

			// Token: 0x06000381 RID: 897 RVA: 0x00011288 File Offset: 0x0000F488
			public static int IncrementLo(int slot)
			{
				return (slot + 1 & 65535) | (slot & -65536);
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0001129B File Offset: 0x0000F49B
			public static bool IsComplete(int gate)
			{
				return (gate & -65536) == gate << 16;
			}

			// Token: 0x04000276 RID: 630
			public const int HiShift = 16;

			// Token: 0x04000277 RID: 631
			public const int HiOne = 65536;

			// Token: 0x04000278 RID: 632
			public const int LoHiBit = 32768;

			// Token: 0x04000279 RID: 633
			public const int HiHiBit = -2147483648;

			// Token: 0x0400027A RID: 634
			public const int LoCountMask = 32767;

			// Token: 0x0400027B RID: 635
			public const int HiCountMask = 2147418112;

			// Token: 0x0400027C RID: 636
			public const int LoMask = 65535;

			// Token: 0x0400027D RID: 637
			public const int HiMask = -65536;

			// Token: 0x0400027E RID: 638
			public const int HiBits = -2147450880;
		}

		// Token: 0x0200006E RID: 110
		private struct Slot
		{
			// Token: 0x06000383 RID: 899 RVA: 0x000112AC File Offset: 0x0000F4AC
			public bool TryEnqueueWorkItem(Action<object> callback, object state, out bool wrapped)
			{
				int num = Interlocked.Increment(ref this.gate);
				wrapped = ((num & 32767) != 1);
				if (wrapped)
				{
					if ((num & 32768) != 0 && IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
					return false;
				}
				this.state = state;
				this.callback = callback;
				num = Interlocked.Add(ref this.gate, 32768);
				if ((num & 2147418112) == 0)
				{
					return true;
				}
				this.state = null;
				this.callback = null;
				if (num >> 16 != (num & 32767) || Interlocked.CompareExchange(ref this.gate, 0, num) != num)
				{
					num = Interlocked.Add(ref this.gate, int.MinValue);
					if (IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
				}
				return false;
			}

			// Token: 0x06000384 RID: 900 RVA: 0x00011378 File Offset: 0x0000F578
			public void DequeueWorkItem(out Action<object> callback, out object state)
			{
				int num = Interlocked.Add(ref this.gate, 65536);
				if ((num & 32768) == 0)
				{
					callback = null;
					state = null;
					return;
				}
				if ((num & 2147418112) == 65536)
				{
					callback = this.callback;
					state = this.state;
					this.state = null;
					this.callback = null;
					if ((num & 32767) != 1 || Interlocked.CompareExchange(ref this.gate, 0, num) != num)
					{
						num = Interlocked.Add(ref this.gate, int.MinValue);
						if (IOThreadScheduler.Bits.IsComplete(num))
						{
							Interlocked.CompareExchange(ref this.gate, 0, num);
							return;
						}
					}
				}
				else
				{
					callback = null;
					state = null;
					if (IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
				}
			}

			// Token: 0x0400027F RID: 639
			private int gate;

			// Token: 0x04000280 RID: 640
			private Action<object> callback;

			// Token: 0x04000281 RID: 641
			private object state;
		}

		// Token: 0x0200006F RID: 111
		[SecurityCritical]
		private class ScheduledOverlapped
		{
			// Token: 0x06000385 RID: 901 RVA: 0x00011430 File Offset: 0x0000F630
			public ScheduledOverlapped()
			{
				this.nativeOverlapped = new Overlapped().UnsafePack(Fx.ThunkCallback(new IOCompletionCallback(this.IOCallback)), null);
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0001145C File Offset: 0x0000F65C
			private unsafe void IOCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				IOThreadScheduler iothreadScheduler = this.scheduler;
				this.scheduler = null;
				Action<object> action;
				object obj;
				try
				{
				}
				finally
				{
					iothreadScheduler.CompletionCallback(out action, out obj);
				}
				bool flag = true;
				while (flag)
				{
					if (action != null)
					{
						action(obj);
					}
					try
					{
					}
					finally
					{
						flag = iothreadScheduler.TryCoalesce(out action, out obj);
					}
				}
			}

			// Token: 0x06000387 RID: 903 RVA: 0x000114C0 File Offset: 0x0000F6C0
			public void Post(IOThreadScheduler iots)
			{
				this.scheduler = iots;
				ThreadPool.UnsafeQueueNativeOverlapped(this.nativeOverlapped);
			}

			// Token: 0x06000388 RID: 904 RVA: 0x000114D5 File Offset: 0x0000F6D5
			public void Cleanup()
			{
				if (this.scheduler != null)
				{
					throw Fx.AssertAndThrowFatal("Cleanup called on an overlapped that is in-flight.");
				}
				Overlapped.Free(this.nativeOverlapped);
			}

			// Token: 0x04000282 RID: 642
			private unsafe readonly NativeOverlapped* nativeOverlapped;

			// Token: 0x04000283 RID: 643
			private IOThreadScheduler scheduler;
		}
	}
}
