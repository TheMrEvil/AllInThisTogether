using System;

namespace Internal.Cryptography
{
	// Token: 0x02000110 RID: 272
	internal static class Oids
	{
		// Token: 0x04000433 RID: 1075
		public const string Rc2Cbc = "1.2.840.113549.3.2";

		// Token: 0x04000434 RID: 1076
		public const string Rc4 = "1.2.840.113549.3.4";

		// Token: 0x04000435 RID: 1077
		public const string TripleDesCbc = "1.2.840.113549.3.7";

		// Token: 0x04000436 RID: 1078
		public const string DesCbc = "1.3.14.3.2.7";

		// Token: 0x04000437 RID: 1079
		public const string Aes128Cbc = "2.16.840.1.101.3.4.1.2";

		// Token: 0x04000438 RID: 1080
		public const string Aes192Cbc = "2.16.840.1.101.3.4.1.22";

		// Token: 0x04000439 RID: 1081
		public const string Aes256Cbc = "2.16.840.1.101.3.4.1.42";

		// Token: 0x0400043A RID: 1082
		public const string Rsa = "1.2.840.113549.1.1.1";

		// Token: 0x0400043B RID: 1083
		public const string RsaOaep = "1.2.840.113549.1.1.7";

		// Token: 0x0400043C RID: 1084
		public const string RsaPss = "1.2.840.113549.1.1.10";

		// Token: 0x0400043D RID: 1085
		public const string RsaPkcs1Sha1 = "1.2.840.113549.1.1.5";

		// Token: 0x0400043E RID: 1086
		public const string RsaPkcs1Sha256 = "1.2.840.113549.1.1.11";

		// Token: 0x0400043F RID: 1087
		public const string RsaPkcs1Sha384 = "1.2.840.113549.1.1.12";

		// Token: 0x04000440 RID: 1088
		public const string RsaPkcs1Sha512 = "1.2.840.113549.1.1.13";

		// Token: 0x04000441 RID: 1089
		public const string Esdh = "1.2.840.113549.1.9.16.3.5";

		// Token: 0x04000442 RID: 1090
		public const string SigningTime = "1.2.840.113549.1.9.5";

		// Token: 0x04000443 RID: 1091
		public const string ContentType = "1.2.840.113549.1.9.3";

		// Token: 0x04000444 RID: 1092
		public const string DocumentDescription = "1.3.6.1.4.1.311.88.2.2";

		// Token: 0x04000445 RID: 1093
		public const string MessageDigest = "1.2.840.113549.1.9.4";

		// Token: 0x04000446 RID: 1094
		public const string CounterSigner = "1.2.840.113549.1.9.6";

		// Token: 0x04000447 RID: 1095
		public const string SigningCertificate = "1.2.840.113549.1.9.16.2.12";

		// Token: 0x04000448 RID: 1096
		public const string SigningCertificateV2 = "1.2.840.113549.1.9.16.2.47";

		// Token: 0x04000449 RID: 1097
		public const string DocumentName = "1.3.6.1.4.1.311.88.2.1";

		// Token: 0x0400044A RID: 1098
		public const string CmsRc2Wrap = "1.2.840.113549.1.9.16.3.7";

		// Token: 0x0400044B RID: 1099
		public const string Cms3DesWrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x0400044C RID: 1100
		public const string Pkcs7Data = "1.2.840.113549.1.7.1";

		// Token: 0x0400044D RID: 1101
		public const string Pkcs7Signed = "1.2.840.113549.1.7.2";

		// Token: 0x0400044E RID: 1102
		public const string Pkcs7Enveloped = "1.2.840.113549.1.7.3";

		// Token: 0x0400044F RID: 1103
		public const string Pkcs7SignedEnveloped = "1.2.840.113549.1.7.4";

		// Token: 0x04000450 RID: 1104
		public const string Pkcs7Hashed = "1.2.840.113549.1.7.5";

		// Token: 0x04000451 RID: 1105
		public const string Pkcs7Encrypted = "1.2.840.113549.1.7.6";

		// Token: 0x04000452 RID: 1106
		public const string Md5 = "1.2.840.113549.2.5";

		// Token: 0x04000453 RID: 1107
		public const string Sha1 = "1.3.14.3.2.26";

		// Token: 0x04000454 RID: 1108
		public const string Sha256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x04000455 RID: 1109
		public const string Sha384 = "2.16.840.1.101.3.4.2.2";

		// Token: 0x04000456 RID: 1110
		public const string Sha512 = "2.16.840.1.101.3.4.2.3";

		// Token: 0x04000457 RID: 1111
		public const string DsaPublicKey = "1.2.840.10040.4.1";

		// Token: 0x04000458 RID: 1112
		public const string DsaWithSha1 = "1.2.840.10040.4.3";

		// Token: 0x04000459 RID: 1113
		public const string DsaWithSha256 = "2.16.840.1.101.3.4.3.2";

		// Token: 0x0400045A RID: 1114
		public const string DsaWithSha384 = "2.16.840.1.101.3.4.3.3";

		// Token: 0x0400045B RID: 1115
		public const string DsaWithSha512 = "2.16.840.1.101.3.4.3.4";

		// Token: 0x0400045C RID: 1116
		public const string EcPublicKey = "1.2.840.10045.2.1";

		// Token: 0x0400045D RID: 1117
		public const string ECDsaWithSha1 = "1.2.840.10045.4.1";

		// Token: 0x0400045E RID: 1118
		public const string ECDsaWithSha256 = "1.2.840.10045.4.3.2";

		// Token: 0x0400045F RID: 1119
		public const string ECDsaWithSha384 = "1.2.840.10045.4.3.3";

		// Token: 0x04000460 RID: 1120
		public const string ECDsaWithSha512 = "1.2.840.10045.4.3.4";

		// Token: 0x04000461 RID: 1121
		public const string Mgf1 = "1.2.840.113549.1.1.8";

		// Token: 0x04000462 RID: 1122
		public const string SubjectKeyIdentifier = "2.5.29.14";

		// Token: 0x04000463 RID: 1123
		public const string KeyUsage = "2.5.29.15";

		// Token: 0x04000464 RID: 1124
		public const string TstInfo = "1.2.840.113549.1.9.16.1.4";

		// Token: 0x04000465 RID: 1125
		public const string TimeStampingPurpose = "1.3.6.1.5.5.7.3.8";
	}
}
