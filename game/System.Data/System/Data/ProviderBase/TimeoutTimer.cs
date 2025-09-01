using System;
using System.Data.Common;

namespace System.Data.ProviderBase
{
	// Token: 0x02000354 RID: 852
	internal class TimeoutTimer
	{
		// Token: 0x060026FD RID: 9981 RVA: 0x000ACB8A File Offset: 0x000AAD8A
		internal static TimeoutTimer StartSecondsTimeout(int seconds)
		{
			TimeoutTimer timeoutTimer = new TimeoutTimer();
			timeoutTimer.SetTimeoutSeconds(seconds);
			return timeoutTimer;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000ACB98 File Offset: 0x000AAD98
		internal static TimeoutTimer StartMillisecondsTimeout(long milliseconds)
		{
			return new TimeoutTimer
			{
				_timerExpire = checked(ADP.TimerCurrent() + milliseconds * 10000L),
				_isInfiniteTimeout = false
			};
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000ACBBA File Offset: 0x000AADBA
		internal void SetTimeoutSeconds(int seconds)
		{
			if (TimeoutTimer.InfiniteTimeout == (long)seconds)
			{
				this._isInfiniteTimeout = true;
				return;
			}
			this._timerExpire = checked(ADP.TimerCurrent() + ADP.TimerFromSeconds(seconds));
			this._isInfiniteTimeout = false;
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x000ACBE6 File Offset: 0x000AADE6
		internal bool IsExpired
		{
			get
			{
				return !this.IsInfinite && ADP.TimerHasExpired(this._timerExpire);
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x000ACBFD File Offset: 0x000AADFD
		internal bool IsInfinite
		{
			get
			{
				return this._isInfiniteTimeout;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x000ACC05 File Offset: 0x000AAE05
		internal long LegacyTimerExpire
		{
			get
			{
				if (!this._isInfiniteTimeout)
				{
					return this._timerExpire;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002703 RID: 9987 RVA: 0x000ACC20 File Offset: 0x000AAE20
		internal long MillisecondsRemaining
		{
			get
			{
				long num;
				if (this._isInfiniteTimeout)
				{
					num = long.MaxValue;
				}
				else
				{
					num = ADP.TimerRemainingMilliseconds(this._timerExpire);
					if (0L > num)
					{
						num = 0L;
					}
				}
				return num;
			}
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x00003D93 File Offset: 0x00001F93
		public TimeoutTimer()
		{
		}

		// Token: 0x04001978 RID: 6520
		private long _timerExpire;

		// Token: 0x04001979 RID: 6521
		private bool _isInfiniteTimeout;

		// Token: 0x0400197A RID: 6522
		internal static readonly long InfiniteTimeout;
	}
}
