using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000816 RID: 2070
	internal static class DomainLiteralReader
	{
		// Token: 0x060041F8 RID: 16888 RVA: 0x000E3D08 File Offset: 0x000E1F08
		internal static int ReadReverse(string data, int index)
		{
			index--;
			for (;;)
			{
				index = WhitespaceReader.ReadFwsReverse(data, index);
				if (index < 0)
				{
					goto IL_7A;
				}
				int num = QuotedPairReader.CountQuotedChars(data, index, false);
				if (num > 0)
				{
					index -= num;
				}
				else
				{
					if (data[index] == MailBnfHelper.StartSquareBracket)
					{
						break;
					}
					if ((int)data[index] > MailBnfHelper.Ascii7bitMaxValue || !MailBnfHelper.Dtext[(int)data[index]])
					{
						goto IL_55;
					}
					index--;
				}
				if (index < 0)
				{
					goto IL_7A;
				}
			}
			return index - 1;
			IL_55:
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
			IL_7A:
			throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", MailBnfHelper.EndSquareBracket));
		}
	}
}
