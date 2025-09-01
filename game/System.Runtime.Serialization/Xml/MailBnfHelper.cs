using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000081 RID: 129
	internal static class MailBnfHelper
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001D860 File Offset: 0x0001BA60
		static MailBnfHelper()
		{
			for (int i = 1; i <= 9; i++)
			{
				MailBnfHelper.s_fqtext[i] = true;
			}
			MailBnfHelper.s_fqtext[11] = true;
			MailBnfHelper.s_fqtext[12] = true;
			for (int j = 14; j <= 33; j++)
			{
				MailBnfHelper.s_fqtext[j] = true;
			}
			for (int k = 35; k <= 91; k++)
			{
				MailBnfHelper.s_fqtext[k] = true;
			}
			for (int l = 93; l <= 127; l++)
			{
				MailBnfHelper.s_fqtext[l] = true;
			}
			for (int m = 33; m <= 126; m++)
			{
				MailBnfHelper.s_ttext[m] = true;
			}
			MailBnfHelper.s_ttext[40] = false;
			MailBnfHelper.s_ttext[41] = false;
			MailBnfHelper.s_ttext[60] = false;
			MailBnfHelper.s_ttext[62] = false;
			MailBnfHelper.s_ttext[64] = false;
			MailBnfHelper.s_ttext[44] = false;
			MailBnfHelper.s_ttext[59] = false;
			MailBnfHelper.s_ttext[58] = false;
			MailBnfHelper.s_ttext[92] = false;
			MailBnfHelper.s_ttext[34] = false;
			MailBnfHelper.s_ttext[47] = false;
			MailBnfHelper.s_ttext[91] = false;
			MailBnfHelper.s_ttext[93] = false;
			MailBnfHelper.s_ttext[63] = false;
			MailBnfHelper.s_ttext[61] = false;
			for (int n = 48; n <= 57; n++)
			{
				MailBnfHelper.s_digits[n] = true;
			}
			for (int num = 48; num <= 57; num++)
			{
				MailBnfHelper.s_boundary[num] = true;
			}
			for (int num2 = 65; num2 <= 90; num2++)
			{
				MailBnfHelper.s_boundary[num2] = true;
			}
			for (int num3 = 97; num3 <= 122; num3++)
			{
				MailBnfHelper.s_boundary[num3] = true;
			}
			MailBnfHelper.s_boundary[39] = true;
			MailBnfHelper.s_boundary[40] = true;
			MailBnfHelper.s_boundary[41] = true;
			MailBnfHelper.s_boundary[43] = true;
			MailBnfHelper.s_boundary[95] = true;
			MailBnfHelper.s_boundary[44] = true;
			MailBnfHelper.s_boundary[45] = true;
			MailBnfHelper.s_boundary[46] = true;
			MailBnfHelper.s_boundary[47] = true;
			MailBnfHelper.s_boundary[58] = true;
			MailBnfHelper.s_boundary[61] = true;
			MailBnfHelper.s_boundary[63] = true;
			MailBnfHelper.s_boundary[32] = true;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001DA98 File Offset: 0x0001BC98
		public static bool SkipCFWS(string data, ref int offset)
		{
			int num = 0;
			while (offset < data.Length)
			{
				if (data[offset] > '\u007f')
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME header has an invalid character ('{0}', {1} in hexadecimal value).", new object[]
					{
						data[offset],
						((int)data[offset]).ToString("X", CultureInfo.InvariantCulture)
					})));
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
				offset++;
			}
			return false;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001DB70 File Offset: 0x0001BD70
		public static string ReadQuotedString(string data, ref int offset, StringBuilder builder)
		{
			int num = offset + 1;
			offset = num;
			int num2 = num;
			StringBuilder stringBuilder = (builder != null) ? builder : new StringBuilder();
			while (offset < data.Length)
			{
				if (data[offset] == '\\')
				{
					stringBuilder.Append(data, num2, offset - num2);
					num = offset + 1;
					offset = num;
					num2 = num;
				}
				else if (data[offset] == '"')
				{
					stringBuilder.Append(data, num2, offset - num2);
					offset++;
					if (builder == null)
					{
						return stringBuilder.ToString();
					}
					return null;
				}
				else if ((int)data[offset] >= MailBnfHelper.s_fqtext.Length || !MailBnfHelper.s_fqtext[(int)data[offset]])
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME header has an invalid character ('{0}', {1} in hexadecimal value).", new object[]
					{
						data[offset],
						((int)data[offset]).ToString("X", CultureInfo.InvariantCulture)
					})));
				}
				offset++;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("Malformed MIME header.")));
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001DC7B File Offset: 0x0001BE7B
		public static string ReadParameterAttribute(string data, ref int offset, StringBuilder builder)
		{
			if (!MailBnfHelper.SkipCFWS(data, ref offset))
			{
				return null;
			}
			return MailBnfHelper.ReadToken(data, ref offset, null);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001DC90 File Offset: 0x0001BE90
		public static string ReadParameterValue(string data, ref int offset, StringBuilder builder)
		{
			if (!MailBnfHelper.SkipCFWS(data, ref offset))
			{
				return string.Empty;
			}
			if (offset < data.Length && data[offset] == '"')
			{
				return MailBnfHelper.ReadQuotedString(data, ref offset, builder);
			}
			return MailBnfHelper.ReadToken(data, ref offset, builder);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001DCC8 File Offset: 0x0001BEC8
		public static string ReadToken(string data, ref int offset, StringBuilder builder)
		{
			int num = offset;
			while (offset < data.Length)
			{
				if ((int)data[offset] > MailBnfHelper.s_ttext.Length)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("MIME header has an invalid character ('{0}', {1} in hexadecimal value).", new object[]
					{
						data[offset],
						((int)data[offset]).ToString("X", CultureInfo.InvariantCulture)
					})));
				}
				if (!MailBnfHelper.s_ttext[(int)data[offset]])
				{
					break;
				}
				offset++;
			}
			return data.Substring(num, offset - num);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001DD60 File Offset: 0x0001BF60
		public static string ReadDigits(string data, ref int offset, StringBuilder builder)
		{
			int num = offset;
			StringBuilder stringBuilder = (builder != null) ? builder : new StringBuilder();
			while (offset < data.Length && (int)data[offset] < MailBnfHelper.s_digits.Length && MailBnfHelper.s_digits[(int)data[offset]])
			{
				offset++;
			}
			stringBuilder.Append(data, num, offset - num);
			if (builder == null)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
		public static bool IsValidMimeBoundary(string data)
		{
			int num = (data == null) ? 0 : data.Length;
			if (num == 0 || num > 70 || data[num - 1] == ' ')
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if ((int)data[i] >= MailBnfHelper.s_boundary.Length || !MailBnfHelper.s_boundary[(int)data[i]])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400031A RID: 794
		private static bool[] s_fqtext = new bool[128];

		// Token: 0x0400031B RID: 795
		private static bool[] s_ttext = new bool[128];

		// Token: 0x0400031C RID: 796
		private static bool[] s_digits = new bool[128];

		// Token: 0x0400031D RID: 797
		private static bool[] s_boundary = new bool[128];
	}
}
