using System;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x020007F5 RID: 2037
	internal static class MailBnfHelper
	{
		// Token: 0x060040E1 RID: 16609 RVA: 0x000DF23C File Offset: 0x000DD43C
		private static bool[] CreateCharactersAllowedInAtoms()
		{
			bool[] array = new bool[128];
			for (int i = 48; i <= 57; i++)
			{
				array[i] = true;
			}
			for (int j = 65; j <= 90; j++)
			{
				array[j] = true;
			}
			for (int k = 97; k <= 122; k++)
			{
				array[k] = true;
			}
			array[33] = true;
			array[35] = true;
			array[36] = true;
			array[37] = true;
			array[38] = true;
			array[39] = true;
			array[42] = true;
			array[43] = true;
			array[45] = true;
			array[47] = true;
			array[61] = true;
			array[63] = true;
			array[94] = true;
			array[95] = true;
			array[96] = true;
			array[123] = true;
			array[124] = true;
			array[125] = true;
			array[126] = true;
			return array;
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x000DF2EC File Offset: 0x000DD4EC
		private static bool[] CreateCharactersAllowedInQuotedStrings()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 9; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 33; j++)
			{
				array[j] = true;
			}
			for (int k = 35; k <= 91; k++)
			{
				array[k] = true;
			}
			for (int l = 93; l <= 127; l++)
			{
				array[l] = true;
			}
			return array;
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x000DF35C File Offset: 0x000DD55C
		private static bool[] CreateCharactersAllowedInDomainLiterals()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 8; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 31; j++)
			{
				array[j] = true;
			}
			for (int k = 33; k <= 90; k++)
			{
				array[k] = true;
			}
			for (int l = 94; l <= 127; l++)
			{
				array[l] = true;
			}
			return array;
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x000DF3CC File Offset: 0x000DD5CC
		private static bool[] CreateCharactersAllowedInHeaderNames()
		{
			bool[] array = new bool[128];
			for (int i = 33; i <= 57; i++)
			{
				array[i] = true;
			}
			for (int j = 59; j <= 126; j++)
			{
				array[j] = true;
			}
			return array;
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x000DF40C File Offset: 0x000DD60C
		private static bool[] CreateCharactersAllowedInTokens()
		{
			bool[] array = new bool[128];
			for (int i = 33; i <= 126; i++)
			{
				array[i] = true;
			}
			array[40] = false;
			array[41] = false;
			array[60] = false;
			array[62] = false;
			array[64] = false;
			array[44] = false;
			array[59] = false;
			array[58] = false;
			array[92] = false;
			array[34] = false;
			array[47] = false;
			array[91] = false;
			array[93] = false;
			array[63] = false;
			array[61] = false;
			return array;
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000DF484 File Offset: 0x000DD684
		private static bool[] CreateCharactersAllowedInComments()
		{
			bool[] array = new bool[128];
			for (int i = 1; i <= 8; i++)
			{
				array[i] = true;
			}
			array[11] = true;
			array[12] = true;
			for (int j = 14; j <= 31; j++)
			{
				array[j] = true;
			}
			for (int k = 33; k <= 39; k++)
			{
				array[k] = true;
			}
			for (int l = 42; l <= 91; l++)
			{
				array[l] = true;
			}
			for (int m = 93; m <= 127; m++)
			{
				array[m] = true;
			}
			return array;
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x000DF50C File Offset: 0x000DD70C
		internal static bool SkipCFWS(string data, ref int offset)
		{
			int num = 0;
			while (offset < data.Length)
			{
				if (data[offset] > '\u007f')
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				if (data[offset] == '\\' && num > 0)
				{
					offset += 2;
				}
				else if (data[offset] == '(')
				{
					num++;
				}
				else if (data[offset] == ')')
				{
					num--;
				}
				else if (data[offset] != ' ' && data[offset] != '\t' && num == 0)
				{
					return true;
				}
				if (num < 0)
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				offset++;
			}
			return false;
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000DF5D8 File Offset: 0x000DD7D8
		internal static void ValidateHeaderName(string data)
		{
			int i;
			for (i = 0; i < data.Length; i++)
			{
				if ((int)data[i] > MailBnfHelper.Ftext.Length || !MailBnfHelper.Ftext[(int)data[i]])
				{
					throw new FormatException("An invalid character was found in header name.");
				}
			}
			if (i == 0)
			{
				throw new FormatException("An invalid character was found in header name.");
			}
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x000DF62E File Offset: 0x000DD82E
		internal static string ReadQuotedString(string data, ref int offset, StringBuilder builder)
		{
			return MailBnfHelper.ReadQuotedString(data, ref offset, builder, false, false);
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x000DF63C File Offset: 0x000DD83C
		internal static string ReadQuotedString(string data, ref int offset, StringBuilder builder, bool doesntRequireQuotes, bool permitUnicodeInDisplayName)
		{
			if (!doesntRequireQuotes)
			{
				offset++;
			}
			int num = offset;
			StringBuilder stringBuilder = (builder != null) ? builder : new StringBuilder();
			while (offset < data.Length)
			{
				if (data[offset] == '\\')
				{
					stringBuilder.Append(data, num, offset - num);
					int num2 = offset + 1;
					offset = num2;
					num = num2;
				}
				else if (data[offset] == '"')
				{
					stringBuilder.Append(data, num, offset - num);
					offset++;
					if (builder == null)
					{
						return stringBuilder.ToString();
					}
					return null;
				}
				else if (data[offset] == '=' && data.Length > offset + 3 && data[offset + 1] == '\r' && data[offset + 2] == '\n' && (data[offset + 3] == ' ' || data[offset + 3] == '\t'))
				{
					offset += 3;
				}
				else if (permitUnicodeInDisplayName)
				{
					if ((int)data[offset] <= MailBnfHelper.Ascii7bitMaxValue && !MailBnfHelper.Qtext[(int)data[offset]])
					{
						throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
					}
				}
				else if ((int)data[offset] > MailBnfHelper.Ascii7bitMaxValue || !MailBnfHelper.Qtext[(int)data[offset]])
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				offset++;
			}
			if (!doesntRequireQuotes)
			{
				throw new FormatException("The mail header is malformed.");
			}
			stringBuilder.Append(data, num, offset - num);
			if (builder == null)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000DF7C6 File Offset: 0x000DD9C6
		internal static string ReadParameterAttribute(string data, ref int offset, StringBuilder builder)
		{
			if (!MailBnfHelper.SkipCFWS(data, ref offset))
			{
				return null;
			}
			return MailBnfHelper.ReadToken(data, ref offset, null);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x000DF7DC File Offset: 0x000DD9DC
		internal static string ReadToken(string data, ref int offset, StringBuilder builder)
		{
			int num = offset;
			while (offset < data.Length)
			{
				if ((int)data[offset] > MailBnfHelper.Ascii7bitMaxValue)
				{
					throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
				}
				if (!MailBnfHelper.Ttext[(int)data[offset]])
				{
					break;
				}
				offset++;
			}
			if (num == offset)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[offset]));
			}
			return data.Substring(num, offset - num);
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x000DF868 File Offset: 0x000DDA68
		internal static string GetDateTimeString(DateTime value, StringBuilder builder)
		{
			StringBuilder stringBuilder = (builder != null) ? builder : new StringBuilder();
			stringBuilder.Append(value.Day);
			stringBuilder.Append(' ');
			stringBuilder.Append(MailBnfHelper.s_months[value.Month]);
			stringBuilder.Append(' ');
			stringBuilder.Append(value.Year);
			stringBuilder.Append(' ');
			if (value.Hour <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Hour);
			stringBuilder.Append(':');
			if (value.Minute <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Minute);
			stringBuilder.Append(':');
			if (value.Second <= 9)
			{
				stringBuilder.Append('0');
			}
			stringBuilder.Append(value.Second);
			string text = TimeZoneInfo.Local.GetUtcOffset(value).ToString();
			if (text[0] != '-')
			{
				stringBuilder.Append(" +");
			}
			else
			{
				stringBuilder.Append(' ');
			}
			string[] array = text.Split(MailBnfHelper.s_colonSeparator);
			stringBuilder.Append(array[0]);
			stringBuilder.Append(array[1]);
			if (builder == null)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x000DF9AC File Offset: 0x000DDBAC
		internal static void GetTokenOrQuotedString(string data, StringBuilder builder, bool allowUnicode)
		{
			int i = 0;
			int num = 0;
			while (i < data.Length)
			{
				if (!MailBnfHelper.CheckForUnicode(data[i], allowUnicode) && (!MailBnfHelper.Ttext[(int)data[i]] || data[i] == ' '))
				{
					builder.Append('"');
					while (i < data.Length)
					{
						if (!MailBnfHelper.CheckForUnicode(data[i], allowUnicode))
						{
							if (MailBnfHelper.IsFWSAt(data, i))
							{
								i += 2;
							}
							else if (!MailBnfHelper.Qtext[(int)data[i]])
							{
								builder.Append(data, num, i - num);
								builder.Append('\\');
								num = i;
							}
						}
						i++;
					}
					builder.Append(data, num, i - num);
					builder.Append('"');
					return;
				}
				i++;
			}
			if (data.Length == 0)
			{
				builder.Append("\"\"");
			}
			builder.Append(data);
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x000DFA8C File Offset: 0x000DDC8C
		private static bool CheckForUnicode(char ch, bool allowUnicode)
		{
			if ((int)ch < MailBnfHelper.Ascii7bitMaxValue)
			{
				return false;
			}
			if (!allowUnicode)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", ch));
			}
			return true;
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000DFAB2 File Offset: 0x000DDCB2
		internal static bool IsAllowedWhiteSpace(char c)
		{
			return c == MailBnfHelper.Tab || c == MailBnfHelper.Space || c == MailBnfHelper.CR || c == MailBnfHelper.LF;
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x000DFAD8 File Offset: 0x000DDCD8
		internal static bool HasCROrLF(string data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] == '\r' || data[i] == '\n')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x000DFB10 File Offset: 0x000DDD10
		internal static bool IsFWSAt(string data, int index)
		{
			return data[index] == MailBnfHelper.CR && index + 2 < data.Length && data[index + 1] == MailBnfHelper.LF && (data[index + 2] == MailBnfHelper.Space || data[index + 2] == MailBnfHelper.Tab);
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x000DFB6C File Offset: 0x000DDD6C
		// Note: this type is marked as 'beforefieldinit'.
		static MailBnfHelper()
		{
		}

		// Token: 0x0400275D RID: 10077
		internal static readonly bool[] Atext = MailBnfHelper.CreateCharactersAllowedInAtoms();

		// Token: 0x0400275E RID: 10078
		internal static readonly bool[] Qtext = MailBnfHelper.CreateCharactersAllowedInQuotedStrings();

		// Token: 0x0400275F RID: 10079
		internal static readonly bool[] Dtext = MailBnfHelper.CreateCharactersAllowedInDomainLiterals();

		// Token: 0x04002760 RID: 10080
		internal static readonly bool[] Ftext = MailBnfHelper.CreateCharactersAllowedInHeaderNames();

		// Token: 0x04002761 RID: 10081
		internal static readonly bool[] Ttext = MailBnfHelper.CreateCharactersAllowedInTokens();

		// Token: 0x04002762 RID: 10082
		internal static readonly bool[] Ctext = MailBnfHelper.CreateCharactersAllowedInComments();

		// Token: 0x04002763 RID: 10083
		internal static readonly int Ascii7bitMaxValue = 127;

		// Token: 0x04002764 RID: 10084
		internal static readonly char Quote = '"';

		// Token: 0x04002765 RID: 10085
		internal static readonly char Space = ' ';

		// Token: 0x04002766 RID: 10086
		internal static readonly char Tab = '\t';

		// Token: 0x04002767 RID: 10087
		internal static readonly char CR = '\r';

		// Token: 0x04002768 RID: 10088
		internal static readonly char LF = '\n';

		// Token: 0x04002769 RID: 10089
		internal static readonly char StartComment = '(';

		// Token: 0x0400276A RID: 10090
		internal static readonly char EndComment = ')';

		// Token: 0x0400276B RID: 10091
		internal static readonly char Backslash = '\\';

		// Token: 0x0400276C RID: 10092
		internal static readonly char At = '@';

		// Token: 0x0400276D RID: 10093
		internal static readonly char EndAngleBracket = '>';

		// Token: 0x0400276E RID: 10094
		internal static readonly char StartAngleBracket = '<';

		// Token: 0x0400276F RID: 10095
		internal static readonly char StartSquareBracket = '[';

		// Token: 0x04002770 RID: 10096
		internal static readonly char EndSquareBracket = ']';

		// Token: 0x04002771 RID: 10097
		internal static readonly char Comma = ',';

		// Token: 0x04002772 RID: 10098
		internal static readonly char Dot = '.';

		// Token: 0x04002773 RID: 10099
		private static readonly char[] s_colonSeparator = new char[]
		{
			':'
		};

		// Token: 0x04002774 RID: 10100
		private static string[] s_months = new string[]
		{
			null,
			"Jan",
			"Feb",
			"Mar",
			"Apr",
			"May",
			"Jun",
			"Jul",
			"Aug",
			"Sep",
			"Oct",
			"Nov",
			"Dec"
		};
	}
}
