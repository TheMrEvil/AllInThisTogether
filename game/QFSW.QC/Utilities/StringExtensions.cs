using System;

namespace QFSW.QC.Utilities
{
	// Token: 0x02000057 RID: 87
	public static class StringExtensions
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00009AF7 File Offset: 0x00007CF7
		public static bool ContainsCaseInsensitive(this string source, string value)
		{
			if (!string.IsNullOrEmpty(source))
			{
				return source.Contains(value, StringComparison.OrdinalIgnoreCase);
			}
			return string.IsNullOrEmpty(value);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00009B10 File Offset: 0x00007D10
		public static bool Contains(this string source, string value, StringComparison comp)
		{
			return source != null && source.IndexOf(value, comp) >= 0;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00009B28 File Offset: 0x00007D28
		public static int CountFromIndex(this string source, char target, int index)
		{
			int num = 0;
			for (int i = index; i < source.Length; i++)
			{
				if (source[i] == target)
				{
					num++;
				}
			}
			return num;
		}
	}
}
