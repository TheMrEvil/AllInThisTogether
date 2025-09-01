using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006F RID: 111
	internal static class StringUtilsExtensions
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000B5EC File Offset: 0x000097EC
		public static string ToPascalCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, StringUtilsExtensions.NoDelimiter, new Func<char, char>(char.ToUpperInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B624 File Offset: 0x00009824
		public static string ToCamelCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, StringUtilsExtensions.NoDelimiter, new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B65C File Offset: 0x0000985C
		public static string ToKebabCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '-', new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToLowerInvariant));
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B690 File Offset: 0x00009890
		public static string ToTrainCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '-', new Func<char, char>(char.ToUpperInvariant), new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public static string ToSnakeCase(this string text)
		{
			return StringUtilsExtensions.ConvertCase(text, '_', new Func<char, char>(char.ToLowerInvariant), new Func<char, char>(char.ToLowerInvariant));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B6F8 File Offset: 0x000098F8
		private static string ConvertCase(string text, char outputWordDelimiter, Func<char, char> startOfStringCaseHandler, Func<char, char> middleStringCaseHandler)
		{
			bool flag = text == null;
			if (flag)
			{
				throw new ArgumentNullException("text");
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			foreach (char c in text)
			{
				bool flag5 = StringUtilsExtensions.WordDelimiters.Contains(c);
				if (flag5)
				{
					bool flag6 = c == outputWordDelimiter;
					if (flag6)
					{
						stringBuilder.Append(outputWordDelimiter);
						flag4 = false;
					}
					flag3 = true;
				}
				else
				{
					bool flag7 = !char.IsLetterOrDigit(c);
					if (flag7)
					{
						flag2 = true;
						flag3 = true;
					}
					else
					{
						bool flag8 = flag3 || char.IsUpper(c);
						if (flag8)
						{
							bool flag9 = flag2;
							if (flag9)
							{
								stringBuilder.Append(startOfStringCaseHandler(c));
							}
							else
							{
								bool flag10 = flag4 && outputWordDelimiter != StringUtilsExtensions.NoDelimiter;
								if (flag10)
								{
									stringBuilder.Append(outputWordDelimiter);
								}
								stringBuilder.Append(middleStringCaseHandler(c));
								flag4 = true;
							}
							flag2 = false;
							flag3 = false;
						}
						else
						{
							stringBuilder.Append(c);
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B824 File Offset: 0x00009A24
		public static bool EndsWithIgnoreCaseFast(this string a, string b)
		{
			int num = a.Length - 1;
			int num2 = b.Length - 1;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			while (num >= 0 && num2 >= 0 && (a[num] == b[num2] || char.ToLower(a[num], invariantCulture) == char.ToLower(b[num2], invariantCulture)))
			{
				num--;
				num2--;
			}
			return num2 < 0;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		public static bool StartsWithIgnoreCaseFast(this string a, string b)
		{
			int length = a.Length;
			int length2 = b.Length;
			int num = 0;
			int num2 = 0;
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			while (num < length && num2 < length2 && (a[num] == b[num2] || char.ToLower(a[num], invariantCulture) == char.ToLower(b[num2], invariantCulture)))
			{
				num++;
				num2++;
			}
			return num2 == length2;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B91F File Offset: 0x00009B1F
		// Note: this type is marked as 'beforefieldinit'.
		static StringUtilsExtensions()
		{
		}

		// Token: 0x04000167 RID: 359
		private static readonly char NoDelimiter = '\0';

		// Token: 0x04000168 RID: 360
		private static readonly char[] WordDelimiters = new char[]
		{
			' ',
			'-',
			'_'
		};
	}
}
