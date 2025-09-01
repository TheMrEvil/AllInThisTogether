using System;
using System.Globalization;
using System.Text;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000439 RID: 1081
	internal class DecimalFormatter
	{
		// Token: 0x06002AE0 RID: 10976 RVA: 0x00102740 File Offset: 0x00100940
		public DecimalFormatter(string formatPicture, DecimalFormat decimalFormat)
		{
			if (formatPicture.Length == 0)
			{
				throw XsltException.Create("Format cannot be empty.", Array.Empty<string>());
			}
			this.zeroDigit = decimalFormat.zeroDigit;
			this.posFormatInfo = (NumberFormatInfo)decimalFormat.info.Clone();
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			char c = this.posFormatInfo.NumberDecimalSeparator[0];
			char c2 = this.posFormatInfo.NumberGroupSeparator[0];
			char c3 = this.posFormatInfo.PercentSymbol[0];
			char c4 = this.posFormatInfo.PerMilleSymbol[0];
			int num = 0;
			int num2 = -1;
			int num3 = -1;
			int num4;
			for (int i = 0; i < formatPicture.Length; i++)
			{
				char c5 = formatPicture[i];
				if (c5 == decimalFormat.digit)
				{
					if (flag3 && flag)
					{
						throw XsltException.Create("Format '{0}' cannot have digit symbol after zero digit symbol before a decimal point.", new string[]
						{
							formatPicture
						});
					}
					num3 = stringBuilder.Length;
					flag6 = (flag4 = true);
					stringBuilder.Append('#');
				}
				else if (c5 == decimalFormat.zeroDigit)
				{
					if (flag4 && !flag)
					{
						throw XsltException.Create("Format '{0}' cannot have zero digit symbol after digit symbol after decimal point.", new string[]
						{
							formatPicture
						});
					}
					num3 = stringBuilder.Length;
					flag6 = (flag3 = true);
					stringBuilder.Append('0');
				}
				else if (c5 == decimalFormat.patternSeparator)
				{
					if (!flag6)
					{
						throw XsltException.Create("Format string should have at least one digit or zero digit.", Array.Empty<string>());
					}
					if (flag2)
					{
						throw XsltException.Create("Format '{0}' has two pattern separators.", new string[]
						{
							formatPicture
						});
					}
					flag2 = true;
					if (num2 < 0)
					{
						num2 = num3 + 1;
					}
					num4 = DecimalFormatter.RemoveTrailingComma(stringBuilder, num, num2);
					if (num4 > 9)
					{
						num4 = 0;
					}
					this.posFormatInfo.NumberGroupSizes = new int[]
					{
						num4
					};
					if (!flag5)
					{
						this.posFormatInfo.NumberDecimalDigits = 0;
					}
					this.posFormat = stringBuilder.ToString();
					stringBuilder.Length = 0;
					num2 = -1;
					num3 = -1;
					num = 0;
					flag3 = (flag4 = (flag6 = false));
					flag5 = false;
					flag = true;
					this.negFormatInfo = (NumberFormatInfo)decimalFormat.info.Clone();
					this.negFormatInfo.NegativeSign = string.Empty;
				}
				else if (c5 == c)
				{
					if (flag5)
					{
						throw XsltException.Create("Format '{0}' cannot have two decimal separators.", new string[]
						{
							formatPicture
						});
					}
					num2 = stringBuilder.Length;
					flag5 = true;
					flag3 = (flag4 = (flag = false));
					stringBuilder.Append('.');
				}
				else if (c5 == c2)
				{
					num = stringBuilder.Length;
					num3 = num;
					stringBuilder.Append(',');
				}
				else if (c5 == c3)
				{
					stringBuilder.Append('%');
				}
				else if (c5 == c4)
				{
					stringBuilder.Append('‰');
				}
				else if (c5 == '\'')
				{
					int num5 = formatPicture.IndexOf('\'', i + 1);
					if (num5 < 0)
					{
						num5 = formatPicture.Length - 1;
					}
					stringBuilder.Append(formatPicture, i, num5 - i + 1);
					i = num5;
				}
				else
				{
					if ((('0' <= c5 && c5 <= '9') || c5 == '\a') && decimalFormat.zeroDigit != '0')
					{
						stringBuilder.Append('\a');
					}
					if ("0#.,%‰Ee\\'\";".IndexOf(c5) >= 0)
					{
						stringBuilder.Append('\\');
					}
					stringBuilder.Append(c5);
				}
			}
			if (!flag6)
			{
				throw XsltException.Create("Format string should have at least one digit or zero digit.", Array.Empty<string>());
			}
			NumberFormatInfo numberFormatInfo = flag2 ? this.negFormatInfo : this.posFormatInfo;
			if (num2 < 0)
			{
				num2 = num3 + 1;
			}
			num4 = DecimalFormatter.RemoveTrailingComma(stringBuilder, num, num2);
			if (num4 > 9)
			{
				num4 = 0;
			}
			numberFormatInfo.NumberGroupSizes = new int[]
			{
				num4
			};
			if (!flag5)
			{
				numberFormatInfo.NumberDecimalDigits = 0;
			}
			if (flag2)
			{
				this.negFormat = stringBuilder.ToString();
				return;
			}
			this.posFormat = stringBuilder.ToString();
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x00102AFB File Offset: 0x00100CFB
		private static int RemoveTrailingComma(StringBuilder builder, int commaIndex, int decimalIndex)
		{
			if (commaIndex > 0 && commaIndex == decimalIndex - 1)
			{
				builder.Remove(decimalIndex - 1, 1);
			}
			else if (decimalIndex > commaIndex)
			{
				return decimalIndex - commaIndex - 1;
			}
			return 0;
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x00102B20 File Offset: 0x00100D20
		public string Format(double value)
		{
			NumberFormatInfo provider;
			string format;
			if (value < 0.0 && this.negFormatInfo != null)
			{
				provider = this.negFormatInfo;
				format = this.negFormat;
			}
			else
			{
				provider = this.posFormatInfo;
				format = this.posFormat;
			}
			string text = value.ToString(format, provider);
			if (this.zeroDigit != '0')
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length);
				int num = (int)(this.zeroDigit - '0');
				for (int i = 0; i < text.Length; i++)
				{
					char c = text[i];
					if (c - '0' <= '\t')
					{
						c += (char)num;
					}
					else if (c == '\a')
					{
						c = text[++i];
					}
					stringBuilder.Append(c);
				}
				text = stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x00102BE2 File Offset: 0x00100DE2
		public static string Format(double value, string formatPicture, DecimalFormat decimalFormat)
		{
			return new DecimalFormatter(formatPicture, decimalFormat).Format(value);
		}

		// Token: 0x040021C9 RID: 8649
		private NumberFormatInfo posFormatInfo;

		// Token: 0x040021CA RID: 8650
		private NumberFormatInfo negFormatInfo;

		// Token: 0x040021CB RID: 8651
		private string posFormat;

		// Token: 0x040021CC RID: 8652
		private string negFormat;

		// Token: 0x040021CD RID: 8653
		private char zeroDigit;

		// Token: 0x040021CE RID: 8654
		private const string ClrSpecialChars = "0#.,%‰Ee\\'\";";

		// Token: 0x040021CF RID: 8655
		private const char EscChar = '\a';
	}
}
