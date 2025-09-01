using System;
using Internal.Runtime.Augments;

namespace System.Threading
{
	/// <summary>Provides support for spin-based waiting.</summary>
	// Token: 0x020002A4 RID: 676
	public struct SpinWait
	{
		/// <summary>Gets the number of times <see cref="M:System.Threading.SpinWait.SpinOnce" /> has been called on this instance.</summary>
		/// <returns>Returns an integer that represents the number of times <see cref="M:System.Threading.SpinWait.SpinOnce" /> has been called on this instance.</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x0006F480 File Offset: 0x0006D680
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x0006F488 File Offset: 0x0006D688
		public int Count
		{
			get
			{
				return this._count;
			}
			internal set
			{
				this._count = value;
			}
		}

		/// <summary>Gets whether the next call to <see cref="M:System.Threading.SpinWait.SpinOnce" /> will yield the processor, triggering a forced context switch.</summary>
		/// <returns>Whether the next call to <see cref="M:System.Threading.SpinWait.SpinOnce" /> will yield the processor, triggering a forced context switch.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0006F491 File Offset: 0x0006D691
		public bool NextSpinWillYield
		{
			get
			{
				return this._count >= 10 || PlatformHelper.IsSingleProcessor;
			}
		}

		/// <summary>Performs a single spin.</summary>
		// Token: 0x06001DFE RID: 7678 RVA: 0x0006F4A4 File Offset: 0x0006D6A4
		public void SpinOnce()
		{
			this.SpinOnceCore(20);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x0006F4AE File Offset: 0x0006D6AE
		public void SpinOnce(int sleep1Threshold)
		{
			if (sleep1Threshold < -1)
			{
				throw new ArgumentOutOfRangeException("sleep1Threshold", sleep1Threshold, "Number must be either non-negative and less than or equal to Int32.MaxValue or -1.");
			}
			if (sleep1Threshold >= 0 && sleep1Threshold < 10)
			{
				sleep1Threshold = 10;
			}
			this.SpinOnceCore(sleep1Threshold);
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x0006F4E0 File Offset: 0x0006D6E0
		private void SpinOnceCore(int sleep1Threshold)
		{
			if ((this._count >= 10 && ((this._count >= sleep1Threshold && sleep1Threshold >= 0) || (this._count - 10) % 2 == 0)) || PlatformHelper.IsSingleProcessor)
			{
				if (this._count >= sleep1Threshold && sleep1Threshold >= 0)
				{
					RuntimeThread.Sleep(1);
				}
				else if (((this._count >= 10) ? ((this._count - 10) / 2) : this._count) % 5 == 4)
				{
					RuntimeThread.Sleep(0);
				}
				else
				{
					RuntimeThread.Yield();
				}
			}
			else
			{
				int num = RuntimeThread.OptimalMaxSpinWaitsPerSpinIteration;
				if (this._count <= 30 && 1 << this._count < num)
				{
					num = 1 << this._count;
				}
				RuntimeThread.SpinWait(num);
			}
			this._count = ((this._count == int.MaxValue) ? 10 : (this._count + 1));
		}

		/// <summary>Resets the spin counter.</summary>
		// Token: 0x06001E01 RID: 7681 RVA: 0x0006F5AF File Offset: 0x0006D7AF
		public void Reset()
		{
			this._count = 0;
		}

		/// <summary>Spins until the specified condition is satisfied.</summary>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		// Token: 0x06001E02 RID: 7682 RVA: 0x0006F5B8 File Offset: 0x0006D7B8
		public static void SpinUntil(Func<bool> condition)
		{
			SpinWait.SpinUntil(condition, -1);
		}

		/// <summary>Spins until the specified condition is satisfied or until the specified timeout is expired.</summary>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a TimeSpan that represents -1 milliseconds to wait indefinitely.</param>
		/// <returns>True if the condition is satisfied within the timeout; otherwise, false</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001E03 RID: 7683 RVA: 0x0006F5C4 File Offset: 0x0006D7C4
		public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.");
			}
			return SpinWait.SpinUntil(condition, (int)num);
		}

		/// <summary>Spins until the specified condition is satisfied or until the specified timeout is expired.</summary>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <returns>True if the condition is satisfied within the timeout; otherwise, false</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x06001E04 RID: 7684 RVA: 0x0006F608 File Offset: 0x0006D808
		public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition", "The condition argument is null.");
			}
			uint num = 0U;
			if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
			{
				num = TimeoutHelper.GetTime();
			}
			SpinWait spinWait = default(SpinWait);
			while (!condition())
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				spinWait.SpinOnce();
				if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long)millisecondsTimeout <= (long)((ulong)(TimeoutHelper.GetTime() - num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0006F68B File Offset: 0x0006D88B
		// Note: this type is marked as 'beforefieldinit'.
		static SpinWait()
		{
		}

		// Token: 0x04001A6D RID: 6765
		internal const int YieldThreshold = 10;

		// Token: 0x04001A6E RID: 6766
		private const int Sleep0EveryHowManyYields = 5;

		// Token: 0x04001A6F RID: 6767
		internal const int DefaultSleep1Threshold = 20;

		// Token: 0x04001A70 RID: 6768
		internal static readonly int SpinCountforSpinBeforeWait = PlatformHelper.IsSingleProcessor ? 1 : 35;

		// Token: 0x04001A71 RID: 6769
		internal const int Sleep1ThresholdForLongSpinBeforeWait = 40;

		// Token: 0x04001A72 RID: 6770
		private int _count;
	}
}
