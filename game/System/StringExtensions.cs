using System;

namespace System
{
	// Token: 0x02000145 RID: 325
	internal static class StringExtensions
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x0001FE72 File Offset: 0x0001E072
		internal static string SubstringTrim(this string value, int startIndex)
		{
			return value.SubstringTrim(startIndex, value.Length - startIndex);
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001FE84 File Offset: 0x0001E084
		internal static string SubstringTrim(this string value, int startIndex, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			int num = startIndex + length - 1;
			while (startIndex <= num)
			{
				if (!char.IsWhiteSpace(value[startIndex]))
				{
					break;
				}
				startIndex++;
			}
			while (num >= startIndex && char.IsWhiteSpace(value[num]))
			{
				num--;
			}
			int num2 = num - startIndex + 1;
			if (num2 == 0)
			{
				return string.Empty;
			}
			if (num2 != value.Length)
			{
				return value.Substring(startIndex, num2);
			}
			return value;
		}
	}
}
