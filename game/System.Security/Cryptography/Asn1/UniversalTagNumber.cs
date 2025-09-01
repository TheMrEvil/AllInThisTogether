using System;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000F7 RID: 247
	internal enum UniversalTagNumber
	{
		// Token: 0x040003CA RID: 970
		EndOfContents,
		// Token: 0x040003CB RID: 971
		Boolean,
		// Token: 0x040003CC RID: 972
		Integer,
		// Token: 0x040003CD RID: 973
		BitString,
		// Token: 0x040003CE RID: 974
		OctetString,
		// Token: 0x040003CF RID: 975
		Null,
		// Token: 0x040003D0 RID: 976
		ObjectIdentifier,
		// Token: 0x040003D1 RID: 977
		ObjectDescriptor,
		// Token: 0x040003D2 RID: 978
		External,
		// Token: 0x040003D3 RID: 979
		InstanceOf = 8,
		// Token: 0x040003D4 RID: 980
		Real,
		// Token: 0x040003D5 RID: 981
		Enumerated,
		// Token: 0x040003D6 RID: 982
		Embedded,
		// Token: 0x040003D7 RID: 983
		UTF8String,
		// Token: 0x040003D8 RID: 984
		RelativeObjectIdentifier,
		// Token: 0x040003D9 RID: 985
		Time,
		// Token: 0x040003DA RID: 986
		Sequence = 16,
		// Token: 0x040003DB RID: 987
		SequenceOf = 16,
		// Token: 0x040003DC RID: 988
		Set,
		// Token: 0x040003DD RID: 989
		SetOf = 17,
		// Token: 0x040003DE RID: 990
		NumericString,
		// Token: 0x040003DF RID: 991
		PrintableString,
		// Token: 0x040003E0 RID: 992
		TeletexString,
		// Token: 0x040003E1 RID: 993
		T61String = 20,
		// Token: 0x040003E2 RID: 994
		VideotexString,
		// Token: 0x040003E3 RID: 995
		IA5String,
		// Token: 0x040003E4 RID: 996
		UtcTime,
		// Token: 0x040003E5 RID: 997
		GeneralizedTime,
		// Token: 0x040003E6 RID: 998
		GraphicString,
		// Token: 0x040003E7 RID: 999
		VisibleString,
		// Token: 0x040003E8 RID: 1000
		ISO646String = 26,
		// Token: 0x040003E9 RID: 1001
		GeneralString,
		// Token: 0x040003EA RID: 1002
		UniversalString,
		// Token: 0x040003EB RID: 1003
		UnrestrictedCharacterString,
		// Token: 0x040003EC RID: 1004
		BMPString,
		// Token: 0x040003ED RID: 1005
		Date,
		// Token: 0x040003EE RID: 1006
		TimeOfDay,
		// Token: 0x040003EF RID: 1007
		DateTime,
		// Token: 0x040003F0 RID: 1008
		Duration,
		// Token: 0x040003F1 RID: 1009
		ObjectIdentifierIRI,
		// Token: 0x040003F2 RID: 1010
		RelativeObjectIdentifierIRI
	}
}
