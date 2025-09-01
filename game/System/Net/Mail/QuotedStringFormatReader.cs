using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x0200081A RID: 2074
	internal static class QuotedStringFormatReader
	{
		// Token: 0x06004204 RID: 16900 RVA: 0x000E4230 File Offset: 0x000E2430
		internal static int ReadReverseQuoted(string data, int index, bool permitUnicode)
		{
			index--;
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					goto IL_6C;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, permitUnicode);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (data[index] == MailBnfHelper.Quote)
					{
						break;
					}
					if (!QuotedStringFormatReader.IsValidQtext(permitUnicode, data[index]))
					{
						goto Block_4;
					}
					index--;
				}
				if (index < 0)
				{
					goto IL_6C;
				}
			}
			return index - 1;
			Block_4:
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
			IL_6C:
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", MailBnfHelper.Quote));
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000E42C4 File Offset: 0x000E24C4
		internal static int ReadReverseUnQuoted(string data, int index, bool permitUnicode, bool expectCommaDelimiter)
		{
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					return index;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, permitUnicode);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (expectCommaDelimiter && data[index] == MailBnfHelper.Comma)
					{
						return index;
					}
					if (!QuotedStringFormatReader.IsValidQtext(permitUnicode, data[index]))
					{
						break;
					}
					index--;
				}
				if (index < 0)
				{
					return index;
				}
			}
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000E4338 File Offset: 0x000E2538
		private static bool IsValidQtext(bool allowUnicode, char ch)
		{
			if ((int)ch > MailBnfHelper.Ascii7bitMaxValue)
			{
				return allowUnicode;
			}
			return MailBnfHelper.Qtext[(int)ch];
		}
	}
}
