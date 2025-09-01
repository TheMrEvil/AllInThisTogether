using System;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000817 RID: 2071
	internal static class DotAtomReader
	{
		// Token: 0x060041F9 RID: 16889 RVA: 0x000E3DA8 File Offset: 0x000E1FA8
		internal static int ReadReverse(string data, int index)
		{
			int num = index;
			while (0 <= index && ((int)data[index] > MailBnfHelper.Ascii7bitMaxValue || data[index] == MailBnfHelper.Dot || MailBnfHelper.Atext[(int)data[index]]))
			{
				index--;
			}
			if (num == index)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", data[index]));
			}
			if (data[index + 1] == MailBnfHelper.Dot)
			{
				throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", MailBnfHelper.Dot));
			}
			return index;
		}
	}
}
