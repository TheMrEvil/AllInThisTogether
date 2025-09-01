using System;
using System.Globalization;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200043D RID: 1085
	internal static class CharUtil
	{
		// Token: 0x06002AF2 RID: 10994 RVA: 0x00102F38 File Offset: 0x00101138
		public static bool IsAlphaNumeric(char ch)
		{
			int unicodeCategory = (int)char.GetUnicodeCategory(ch);
			return unicodeCategory <= 4 || (unicodeCategory <= 10 && unicodeCategory >= 8);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x00102F60 File Offset: 0x00101160
		public static bool IsDecimalDigitOne(char ch)
		{
			return char.GetUnicodeCategory(ch -= '\u0001') == UnicodeCategory.DecimalDigitNumber && char.GetNumericValue(ch) == 0.0;
		}
	}
}
