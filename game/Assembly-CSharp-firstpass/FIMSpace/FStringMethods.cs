using System;

namespace FIMSpace
{
	// Token: 0x02000039 RID: 57
	public static class FStringMethods
	{
		// Token: 0x060000FD RID: 253 RVA: 0x0000A23C File Offset: 0x0000843C
		public static string IntToString(this int value, int signs)
		{
			string text = value.ToString();
			int num = signs - text.Length;
			if (num > 0)
			{
				string str = "0";
				for (int i = 1; i < num; i++)
				{
					str += 0.ToString();
				}
				text = str + text;
			}
			return text;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000A28C File Offset: 0x0000848C
		public static string CapitalizeOnlyFirstLetter(this string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			return text[0].ToString().ToUpper() + ((text.Length > 1) ? text.Substring(1) : "");
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public static string CapitalizeFirstLetter(this string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			return text[0].ToString().ToUpper() + text.Substring(1);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000A30B File Offset: 0x0000850B
		public static string ReplaceSpacesWithUnderline(this string text)
		{
			if (text.Contains(" "))
			{
				text = text.Replace(" ", "_");
			}
			return text;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000A330 File Offset: 0x00008530
		public static string GetEndOfStringFromSeparator(this string source, char[] separators, int which = 1, bool fromEnd = false)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int i;
			for (i = source.Length - 1; i >= 0; i--)
			{
				num2++;
				for (int j = 0; j < separators.Length; j++)
				{
					if (source[i] == separators[j])
					{
						num++;
						if (num == which)
						{
							i++;
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				return "";
			}
			if (!fromEnd)
			{
				return source.Substring(0, source.Length - num2);
			}
			return source.Substring(i, source.Length - i);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000A3BC File Offset: 0x000085BC
		public static string GetEndOfStringFromStringSeparator(this string source, string[] separators, int which = 1, bool rest = false)
		{
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int i;
			for (i = 0; i < source.Length; i++)
			{
				num2++;
				int num3 = 0;
				while (num3 < separators.Length && i + separators[num3].Length <= source.Length)
				{
					if (source.Substring(i, separators[num3].Length) == separators[num3])
					{
						num++;
						if (num == which)
						{
							i++;
							i += separators[num3].Length - 1;
							flag = true;
							break;
						}
					}
					num3++;
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				return "";
			}
			if (rest)
			{
				return source.Substring(0, source.Length - num2);
			}
			return source.Substring(i, source.Length - i);
		}
	}
}
