using System;

namespace System.Text
{
	// Token: 0x02000067 RID: 103
	internal static class StringBuilderCache
	{
		// Token: 0x060003DC RID: 988 RVA: 0x00010F34 File Offset: 0x0000F134
		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder stringBuilder = StringBuilderCache.t_cachedInstance;
				if (stringBuilder != null && capacity <= stringBuilder.Capacity)
				{
					StringBuilderCache.t_cachedInstance = null;
					stringBuilder.Clear();
					return stringBuilder;
				}
			}
			return new StringBuilder(capacity);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00010F70 File Offset: 0x0000F170
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.t_cachedInstance = sb;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00010F85 File Offset: 0x0000F185
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string result = sb.ToString();
			StringBuilderCache.Release(sb);
			return result;
		}

		// Token: 0x040001F4 RID: 500
		private const int MaxBuilderSize = 360;

		// Token: 0x040001F5 RID: 501
		private const int DefaultCapacity = 16;

		// Token: 0x040001F6 RID: 502
		[ThreadStatic]
		private static StringBuilder t_cachedInstance;
	}
}
