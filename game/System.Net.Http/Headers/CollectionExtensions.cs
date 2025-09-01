using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net.Http.Headers
{
	// Token: 0x02000036 RID: 54
	internal static class CollectionExtensions
	{
		// Token: 0x060001CD RID: 461 RVA: 0x000079AE File Offset: 0x00005BAE
		public static bool SequenceEqual<TSource>(this List<TSource> first, List<TSource> second)
		{
			if (first == null)
			{
				return second == null || second.Count == 0;
			}
			if (second == null)
			{
				return first == null || first.Count == 0;
			}
			return first.SequenceEqual(second);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000079DC File Offset: 0x00005BDC
		public static void SetValue(this List<NameValueHeaderValue> parameters, string key, string value)
		{
			int i = 0;
			while (i < parameters.Count)
			{
				if (string.Equals(parameters[i].Name, key, StringComparison.OrdinalIgnoreCase))
				{
					if (value == null)
					{
						parameters.RemoveAt(i);
						return;
					}
					parameters[i].Value = value;
					return;
				}
				else
				{
					i++;
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				parameters.Add(new NameValueHeaderValue(key, value));
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007A40 File Offset: 0x00005C40
		public static string ToString<T>(this List<T> list)
		{
			if (list == null || list.Count == 0)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				stringBuilder.Append("; ");
				stringBuilder.Append(list[i]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007A98 File Offset: 0x00005C98
		public static void ToStringBuilder<T>(this List<T> list, StringBuilder sb)
		{
			if (list == null || list.Count == 0)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				if (i > 0)
				{
					sb.Append(", ");
				}
				sb.Append(list[i]);
			}
		}
	}
}
