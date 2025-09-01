using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000030 RID: 48
	internal struct TimeoutHelper
	{
		// Token: 0x06000174 RID: 372 RVA: 0x000065EE File Offset: 0x000047EE
		public TimeoutHelper(TimeSpan timeout)
		{
			this.originalTimeout = timeout;
			this.deadline = DateTime.MaxValue;
			this.deadlineSet = (timeout == TimeSpan.MaxValue);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00006613 File Offset: 0x00004813
		public TimeSpan OriginalTimeout
		{
			get
			{
				return this.originalTimeout;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000661B File Offset: 0x0000481B
		public static bool IsTooLarge(TimeSpan timeout)
		{
			return timeout > TimeoutHelper.MaxWait && timeout != TimeSpan.MaxValue;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006637 File Offset: 0x00004837
		public static TimeSpan FromMilliseconds(int milliseconds)
		{
			if (milliseconds == -1)
			{
				return TimeSpan.MaxValue;
			}
			return TimeSpan.FromMilliseconds((double)milliseconds);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000664C File Offset: 0x0000484C
		public static int ToMilliseconds(TimeSpan timeout)
		{
			if (timeout == TimeSpan.MaxValue)
			{
				return -1;
			}
			long num = Ticks.FromTimeSpan(timeout);
			if (num / 10000L > 2147483647L)
			{
				return int.MaxValue;
			}
			return Ticks.ToMilliseconds(num);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000668B File Offset: 0x0000488B
		public static TimeSpan Min(TimeSpan val1, TimeSpan val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006699 File Offset: 0x00004899
		public static TimeSpan Add(TimeSpan timeout1, TimeSpan timeout2)
		{
			return Ticks.ToTimeSpan(Ticks.Add(Ticks.FromTimeSpan(timeout1), Ticks.FromTimeSpan(timeout2)));
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000066B4 File Offset: 0x000048B4
		public static DateTime Add(DateTime time, TimeSpan timeout)
		{
			if (timeout >= TimeSpan.Zero && DateTime.MaxValue - time <= timeout)
			{
				return DateTime.MaxValue;
			}
			if (timeout <= TimeSpan.Zero && DateTime.MinValue - time >= timeout)
			{
				return DateTime.MinValue;
			}
			return time + timeout;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006714 File Offset: 0x00004914
		public static DateTime Subtract(DateTime time, TimeSpan timeout)
		{
			return TimeoutHelper.Add(time, TimeSpan.Zero - timeout);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00006727 File Offset: 0x00004927
		public static TimeSpan Divide(TimeSpan timeout, int factor)
		{
			if (timeout == TimeSpan.MaxValue)
			{
				return TimeSpan.MaxValue;
			}
			return Ticks.ToTimeSpan(Ticks.FromTimeSpan(timeout) / (long)factor + 1L);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00006750 File Offset: 0x00004950
		public TimeSpan RemainingTime()
		{
			if (!this.deadlineSet)
			{
				this.SetDeadline();
				return this.originalTimeout;
			}
			if (this.deadline == DateTime.MaxValue)
			{
				return TimeSpan.MaxValue;
			}
			TimeSpan timeSpan = this.deadline - DateTime.UtcNow;
			if (timeSpan <= TimeSpan.Zero)
			{
				return TimeSpan.Zero;
			}
			return timeSpan;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000067AF File Offset: 0x000049AF
		public TimeSpan ElapsedTime()
		{
			return this.originalTimeout - this.RemainingTime();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000067C2 File Offset: 0x000049C2
		private void SetDeadline()
		{
			this.deadline = DateTime.UtcNow + this.originalTimeout;
			this.deadlineSet = true;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000067E1 File Offset: 0x000049E1
		public static void ThrowIfNegativeArgument(TimeSpan timeout)
		{
			TimeoutHelper.ThrowIfNegativeArgument(timeout, "timeout");
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000067EE File Offset: 0x000049EE
		public static void ThrowIfNegativeArgument(TimeSpan timeout, string argumentName)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw Fx.Exception.ArgumentOutOfRange(argumentName, timeout, InternalSR.TimeoutMustBeNonNegative(argumentName, timeout));
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000681B File Offset: 0x00004A1B
		public static void ThrowIfNonPositiveArgument(TimeSpan timeout)
		{
			TimeoutHelper.ThrowIfNonPositiveArgument(timeout, "timeout");
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00006828 File Offset: 0x00004A28
		public static void ThrowIfNonPositiveArgument(TimeSpan timeout, string argumentName)
		{
			if (timeout <= TimeSpan.Zero)
			{
				throw Fx.Exception.ArgumentOutOfRange(argumentName, timeout, InternalSR.TimeoutMustBePositive(argumentName, timeout));
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006855 File Offset: 0x00004A55
		public static bool WaitOne(WaitHandle waitHandle, TimeSpan timeout)
		{
			TimeoutHelper.ThrowIfNegativeArgument(timeout);
			if (timeout == TimeSpan.MaxValue)
			{
				waitHandle.WaitOne();
				return true;
			}
			return waitHandle.WaitOne(timeout, false);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000687B File Offset: 0x00004A7B
		// Note: this type is marked as 'beforefieldinit'.
		static TimeoutHelper()
		{
		}

		// Token: 0x040000DC RID: 220
		private DateTime deadline;

		// Token: 0x040000DD RID: 221
		private bool deadlineSet;

		// Token: 0x040000DE RID: 222
		private TimeSpan originalTimeout;

		// Token: 0x040000DF RID: 223
		public static readonly TimeSpan MaxWait = TimeSpan.FromMilliseconds(2147483647.0);
	}
}
