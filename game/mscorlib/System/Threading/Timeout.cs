using System;

namespace System.Threading
{
	/// <summary>Contains constants that specify infinite time-out intervals. This class cannot be inherited.</summary>
	// Token: 0x0200029B RID: 667
	public static class Timeout
	{
		// Token: 0x06001DA6 RID: 7590 RVA: 0x0006E76B File Offset: 0x0006C96B
		// Note: this type is marked as 'beforefieldinit'.
		static Timeout()
		{
		}

		/// <summary>A constant used to specify an infinite waiting period, for methods that accept a <see cref="T:System.TimeSpan" /> parameter.</summary>
		// Token: 0x04001A4F RID: 6735
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		/// <summary>A constant used to specify an infinite waiting period, for threading methods that accept an <see cref="T:System.Int32" /> parameter.</summary>
		// Token: 0x04001A50 RID: 6736
		public const int Infinite = -1;

		// Token: 0x04001A51 RID: 6737
		internal const uint UnsignedInfinite = 4294967295U;
	}
}
