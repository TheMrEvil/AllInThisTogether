using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200000F RID: 15
	internal sealed class BackoffTimeoutHelper
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00002B2D File Offset: 0x00000D2D
		internal BackoffTimeoutHelper(TimeSpan timeout) : this(timeout, BackoffTimeoutHelper.defaultMaxWaitTime)
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B3B File Offset: 0x00000D3B
		internal BackoffTimeoutHelper(TimeSpan timeout, TimeSpan maxWaitTime) : this(timeout, maxWaitTime, BackoffTimeoutHelper.defaultInitialWaitTime)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002B4A File Offset: 0x00000D4A
		internal BackoffTimeoutHelper(TimeSpan timeout, TimeSpan maxWaitTime, TimeSpan initialWaitTime)
		{
			this.random = new Random(this.GetHashCode());
			this.maxWaitTime = maxWaitTime;
			this.originalTimeout = timeout;
			this.Reset(timeout, initialWaitTime);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002B79 File Offset: 0x00000D79
		public TimeSpan OriginalTimeout
		{
			get
			{
				return this.originalTimeout;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002B81 File Offset: 0x00000D81
		private void Reset(TimeSpan timeout, TimeSpan initialWaitTime)
		{
			if (timeout == TimeSpan.MaxValue)
			{
				this.deadline = DateTime.MaxValue;
			}
			else
			{
				this.deadline = DateTime.UtcNow + timeout;
			}
			this.waitTime = initialWaitTime;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BB5 File Offset: 0x00000DB5
		public bool IsExpired()
		{
			return !(this.deadline == DateTime.MaxValue) && DateTime.UtcNow >= this.deadline;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void WaitAndBackoff(Action<object> callback, object state)
		{
			if (this.backoffCallback != callback || this.backoffState != state)
			{
				if (this.backoffTimer != null)
				{
					this.backoffTimer.Cancel();
				}
				this.backoffCallback = callback;
				this.backoffState = state;
				this.backoffTimer = new IOThreadTimer(callback, state, false, BackoffTimeoutHelper.maxSkewMilliseconds);
			}
			TimeSpan timeFromNow = this.WaitTimeWithDrift();
			this.Backoff();
			this.backoffTimer.Set(timeFromNow);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C4E File Offset: 0x00000E4E
		public void WaitAndBackoff()
		{
			Thread.Sleep(this.WaitTimeWithDrift());
			this.Backoff();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C64 File Offset: 0x00000E64
		private TimeSpan WaitTimeWithDrift()
		{
			return Ticks.ToTimeSpan(Math.Max(Ticks.FromTimeSpan(BackoffTimeoutHelper.defaultInitialWaitTime), Ticks.Add(Ticks.FromTimeSpan(this.waitTime), (long)((ulong)this.random.Next() % (ulong)(2L * BackoffTimeoutHelper.maxDriftTicks + 1L) - (ulong)BackoffTimeoutHelper.maxDriftTicks))));
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CB4 File Offset: 0x00000EB4
		private void Backoff()
		{
			if (this.waitTime.Ticks >= this.maxWaitTime.Ticks / 2L)
			{
				this.waitTime = this.maxWaitTime;
			}
			else
			{
				this.waitTime = TimeSpan.FromTicks(this.waitTime.Ticks * 2L);
			}
			if (this.deadline != DateTime.MaxValue)
			{
				TimeSpan t = this.deadline - DateTime.UtcNow;
				if (this.waitTime > t)
				{
					this.waitTime = t;
					if (this.waitTime < TimeSpan.Zero)
					{
						this.waitTime = TimeSpan.Zero;
					}
				}
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D58 File Offset: 0x00000F58
		// Note: this type is marked as 'beforefieldinit'.
		static BackoffTimeoutHelper()
		{
		}

		// Token: 0x04000058 RID: 88
		private static readonly int maxSkewMilliseconds = (int)(IOThreadTimer.SystemTimeResolutionTicks / 10000L);

		// Token: 0x04000059 RID: 89
		private static readonly long maxDriftTicks = IOThreadTimer.SystemTimeResolutionTicks * 2L;

		// Token: 0x0400005A RID: 90
		private static readonly TimeSpan defaultInitialWaitTime = TimeSpan.FromMilliseconds(1.0);

		// Token: 0x0400005B RID: 91
		private static readonly TimeSpan defaultMaxWaitTime = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400005C RID: 92
		private DateTime deadline;

		// Token: 0x0400005D RID: 93
		private TimeSpan maxWaitTime;

		// Token: 0x0400005E RID: 94
		private TimeSpan waitTime;

		// Token: 0x0400005F RID: 95
		private IOThreadTimer backoffTimer;

		// Token: 0x04000060 RID: 96
		private Action<object> backoffCallback;

		// Token: 0x04000061 RID: 97
		private object backoffState;

		// Token: 0x04000062 RID: 98
		private Random random;

		// Token: 0x04000063 RID: 99
		private TimeSpan originalTimeout;
	}
}
