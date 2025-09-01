using System;
using System.Globalization;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000438 RID: 1080
	internal class DecimalFormat
	{
		// Token: 0x06002ADF RID: 10975 RVA: 0x00102718 File Offset: 0x00100918
		internal DecimalFormat(NumberFormatInfo info, char digit, char zeroDigit, char patternSeparator)
		{
			this.info = info;
			this.digit = digit;
			this.zeroDigit = zeroDigit;
			this.patternSeparator = patternSeparator;
		}

		// Token: 0x040021C5 RID: 8645
		public NumberFormatInfo info;

		// Token: 0x040021C6 RID: 8646
		public char digit;

		// Token: 0x040021C7 RID: 8647
		public char zeroDigit;

		// Token: 0x040021C8 RID: 8648
		public char patternSeparator;
	}
}
