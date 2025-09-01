using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001FF RID: 511
	internal static class Util
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x0002BBDD File Offset: 0x00029DDD
		internal static int Sign(int x)
		{
			if (x < 0)
			{
				return -1;
			}
			if (x != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002BBEC File Offset: 0x00029DEC
		internal static Comparer<TKey> GetDefaultComparer<TKey>()
		{
			if (typeof(TKey) == typeof(int))
			{
				return (Comparer<TKey>)Util.s_fastIntComparer;
			}
			if (typeof(TKey) == typeof(long))
			{
				return (Comparer<TKey>)Util.s_fastLongComparer;
			}
			if (typeof(TKey) == typeof(float))
			{
				return (Comparer<TKey>)Util.s_fastFloatComparer;
			}
			if (typeof(TKey) == typeof(double))
			{
				return (Comparer<TKey>)Util.s_fastDoubleComparer;
			}
			if (typeof(TKey) == typeof(DateTime))
			{
				return (Comparer<TKey>)Util.s_fastDateTimeComparer;
			}
			return Comparer<TKey>.Default;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002BCBC File Offset: 0x00029EBC
		// Note: this type is marked as 'beforefieldinit'.
		static Util()
		{
		}

		// Token: 0x040008CD RID: 2253
		private static Util.FastIntComparer s_fastIntComparer = new Util.FastIntComparer();

		// Token: 0x040008CE RID: 2254
		private static Util.FastLongComparer s_fastLongComparer = new Util.FastLongComparer();

		// Token: 0x040008CF RID: 2255
		private static Util.FastFloatComparer s_fastFloatComparer = new Util.FastFloatComparer();

		// Token: 0x040008D0 RID: 2256
		private static Util.FastDoubleComparer s_fastDoubleComparer = new Util.FastDoubleComparer();

		// Token: 0x040008D1 RID: 2257
		private static Util.FastDateTimeComparer s_fastDateTimeComparer = new Util.FastDateTimeComparer();

		// Token: 0x02000200 RID: 512
		private class FastIntComparer : Comparer<int>
		{
			// Token: 0x06000C76 RID: 3190 RVA: 0x0002BCF0 File Offset: 0x00029EF0
			public override int Compare(int x, int y)
			{
				return x.CompareTo(y);
			}

			// Token: 0x06000C77 RID: 3191 RVA: 0x0002BCFA File Offset: 0x00029EFA
			public FastIntComparer()
			{
			}
		}

		// Token: 0x02000201 RID: 513
		private class FastLongComparer : Comparer<long>
		{
			// Token: 0x06000C78 RID: 3192 RVA: 0x0002BD02 File Offset: 0x00029F02
			public override int Compare(long x, long y)
			{
				return x.CompareTo(y);
			}

			// Token: 0x06000C79 RID: 3193 RVA: 0x0002BD0C File Offset: 0x00029F0C
			public FastLongComparer()
			{
			}
		}

		// Token: 0x02000202 RID: 514
		private class FastFloatComparer : Comparer<float>
		{
			// Token: 0x06000C7A RID: 3194 RVA: 0x0002BD14 File Offset: 0x00029F14
			public override int Compare(float x, float y)
			{
				return x.CompareTo(y);
			}

			// Token: 0x06000C7B RID: 3195 RVA: 0x0002BD1E File Offset: 0x00029F1E
			public FastFloatComparer()
			{
			}
		}

		// Token: 0x02000203 RID: 515
		private class FastDoubleComparer : Comparer<double>
		{
			// Token: 0x06000C7C RID: 3196 RVA: 0x0002BD26 File Offset: 0x00029F26
			public override int Compare(double x, double y)
			{
				return x.CompareTo(y);
			}

			// Token: 0x06000C7D RID: 3197 RVA: 0x0002BD30 File Offset: 0x00029F30
			public FastDoubleComparer()
			{
			}
		}

		// Token: 0x02000204 RID: 516
		private class FastDateTimeComparer : Comparer<DateTime>
		{
			// Token: 0x06000C7E RID: 3198 RVA: 0x0002BD38 File Offset: 0x00029F38
			public override int Compare(DateTime x, DateTime y)
			{
				return x.CompareTo(y);
			}

			// Token: 0x06000C7F RID: 3199 RVA: 0x0002BD42 File Offset: 0x00029F42
			public FastDateTimeComparer()
			{
			}
		}
	}
}
