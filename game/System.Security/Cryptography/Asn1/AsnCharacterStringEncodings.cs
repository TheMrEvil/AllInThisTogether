using System;
using System.Text;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000F9 RID: 249
	internal static class AsnCharacterStringEncodings
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x0001779C File Offset: 0x0001599C
		internal static Encoding GetEncoding(UniversalTagNumber encodingType)
		{
			if (encodingType <= UniversalTagNumber.PrintableString)
			{
				if (encodingType == UniversalTagNumber.UTF8String)
				{
					return AsnCharacterStringEncodings.s_utf8Encoding;
				}
				if (encodingType == UniversalTagNumber.PrintableString)
				{
					return AsnCharacterStringEncodings.s_printableStringEncoding;
				}
			}
			else
			{
				if (encodingType == UniversalTagNumber.IA5String)
				{
					return AsnCharacterStringEncodings.s_ia5Encoding;
				}
				if (encodingType == UniversalTagNumber.VisibleString)
				{
					return AsnCharacterStringEncodings.s_visibleStringEncoding;
				}
				if (encodingType == UniversalTagNumber.BMPString)
				{
					return AsnCharacterStringEncodings.s_bmpEncoding;
				}
			}
			throw new ArgumentOutOfRangeException("encodingType", encodingType, null);
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000177FA File Offset: 0x000159FA
		// Note: this type is marked as 'beforefieldinit'.
		static AsnCharacterStringEncodings()
		{
		}

		// Token: 0x04000407 RID: 1031
		private static readonly Encoding s_utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x04000408 RID: 1032
		private static readonly Encoding s_bmpEncoding = new BMPEncoding();

		// Token: 0x04000409 RID: 1033
		private static readonly Encoding s_ia5Encoding = new IA5Encoding();

		// Token: 0x0400040A RID: 1034
		private static readonly Encoding s_visibleStringEncoding = new VisibleStringEncoding();

		// Token: 0x0400040B RID: 1035
		private static readonly Encoding s_printableStringEncoding = new PrintableStringEncoding();
	}
}
