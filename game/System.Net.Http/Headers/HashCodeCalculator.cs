using System;
using System.Collections.Generic;

namespace System.Net.Http.Headers
{
	// Token: 0x0200003C RID: 60
	internal static class HashCodeCalculator
	{
		// Token: 0x06000220 RID: 544 RVA: 0x00008B30 File Offset: 0x00006D30
		public static int Calculate<T>(ICollection<T> list)
		{
			if (list == null)
			{
				return 0;
			}
			int num = 17;
			foreach (T t in list)
			{
				num = num * 29 + t.GetHashCode();
			}
			return num;
		}
	}
}
