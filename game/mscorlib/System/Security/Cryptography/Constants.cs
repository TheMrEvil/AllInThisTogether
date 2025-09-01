using System;

namespace System.Security.Cryptography
{
	// Token: 0x020004C3 RID: 1219
	internal static class Constants
	{
		// Token: 0x040021FC RID: 8700
		internal const int S_OK = 0;

		// Token: 0x040021FD RID: 8701
		internal const int NTE_FILENOTFOUND = -2147024894;

		// Token: 0x040021FE RID: 8702
		internal const int NTE_NO_KEY = -2146893811;

		// Token: 0x040021FF RID: 8703
		internal const int NTE_BAD_KEYSET = -2146893802;

		// Token: 0x04002200 RID: 8704
		internal const int NTE_KEYSET_NOT_DEF = -2146893799;

		// Token: 0x04002201 RID: 8705
		internal const int KP_IV = 1;

		// Token: 0x04002202 RID: 8706
		internal const int KP_MODE = 4;

		// Token: 0x04002203 RID: 8707
		internal const int KP_MODE_BITS = 5;

		// Token: 0x04002204 RID: 8708
		internal const int KP_EFFECTIVE_KEYLEN = 19;

		// Token: 0x04002205 RID: 8709
		internal const int ALG_CLASS_SIGNATURE = 8192;

		// Token: 0x04002206 RID: 8710
		internal const int ALG_CLASS_DATA_ENCRYPT = 24576;

		// Token: 0x04002207 RID: 8711
		internal const int ALG_CLASS_HASH = 32768;

		// Token: 0x04002208 RID: 8712
		internal const int ALG_CLASS_KEY_EXCHANGE = 40960;

		// Token: 0x04002209 RID: 8713
		internal const int ALG_TYPE_DSS = 512;

		// Token: 0x0400220A RID: 8714
		internal const int ALG_TYPE_RSA = 1024;

		// Token: 0x0400220B RID: 8715
		internal const int ALG_TYPE_BLOCK = 1536;

		// Token: 0x0400220C RID: 8716
		internal const int ALG_TYPE_STREAM = 2048;

		// Token: 0x0400220D RID: 8717
		internal const int ALG_TYPE_ANY = 0;

		// Token: 0x0400220E RID: 8718
		internal const int CALG_MD5 = 32771;

		// Token: 0x0400220F RID: 8719
		internal const int CALG_SHA1 = 32772;

		// Token: 0x04002210 RID: 8720
		internal const int CALG_SHA_256 = 32780;

		// Token: 0x04002211 RID: 8721
		internal const int CALG_SHA_384 = 32781;

		// Token: 0x04002212 RID: 8722
		internal const int CALG_SHA_512 = 32782;

		// Token: 0x04002213 RID: 8723
		internal const int CALG_RSA_KEYX = 41984;

		// Token: 0x04002214 RID: 8724
		internal const int CALG_RSA_SIGN = 9216;

		// Token: 0x04002215 RID: 8725
		internal const int CALG_DSS_SIGN = 8704;

		// Token: 0x04002216 RID: 8726
		internal const int CALG_DES = 26113;

		// Token: 0x04002217 RID: 8727
		internal const int CALG_RC2 = 26114;

		// Token: 0x04002218 RID: 8728
		internal const int CALG_3DES = 26115;

		// Token: 0x04002219 RID: 8729
		internal const int CALG_3DES_112 = 26121;

		// Token: 0x0400221A RID: 8730
		internal const int CALG_AES_128 = 26126;

		// Token: 0x0400221B RID: 8731
		internal const int CALG_AES_192 = 26127;

		// Token: 0x0400221C RID: 8732
		internal const int CALG_AES_256 = 26128;

		// Token: 0x0400221D RID: 8733
		internal const int CALG_RC4 = 26625;

		// Token: 0x0400221E RID: 8734
		internal const int PROV_RSA_FULL = 1;

		// Token: 0x0400221F RID: 8735
		internal const int PROV_DSS_DH = 13;

		// Token: 0x04002220 RID: 8736
		internal const int PROV_RSA_AES = 24;

		// Token: 0x04002221 RID: 8737
		internal const int AT_KEYEXCHANGE = 1;

		// Token: 0x04002222 RID: 8738
		internal const int AT_SIGNATURE = 2;

		// Token: 0x04002223 RID: 8739
		internal const int PUBLICKEYBLOB = 6;

		// Token: 0x04002224 RID: 8740
		internal const int PRIVATEKEYBLOB = 7;

		// Token: 0x04002225 RID: 8741
		internal const int CRYPT_OAEP = 64;

		// Token: 0x04002226 RID: 8742
		internal const uint CRYPT_VERIFYCONTEXT = 4026531840U;

		// Token: 0x04002227 RID: 8743
		internal const uint CRYPT_NEWKEYSET = 8U;

		// Token: 0x04002228 RID: 8744
		internal const uint CRYPT_DELETEKEYSET = 16U;

		// Token: 0x04002229 RID: 8745
		internal const uint CRYPT_MACHINE_KEYSET = 32U;

		// Token: 0x0400222A RID: 8746
		internal const uint CRYPT_SILENT = 64U;

		// Token: 0x0400222B RID: 8747
		internal const uint CRYPT_EXPORTABLE = 1U;

		// Token: 0x0400222C RID: 8748
		internal const uint CLR_KEYLEN = 1U;

		// Token: 0x0400222D RID: 8749
		internal const uint CLR_PUBLICKEYONLY = 2U;

		// Token: 0x0400222E RID: 8750
		internal const uint CLR_EXPORTABLE = 3U;

		// Token: 0x0400222F RID: 8751
		internal const uint CLR_REMOVABLE = 4U;

		// Token: 0x04002230 RID: 8752
		internal const uint CLR_HARDWARE = 5U;

		// Token: 0x04002231 RID: 8753
		internal const uint CLR_ACCESSIBLE = 6U;

		// Token: 0x04002232 RID: 8754
		internal const uint CLR_PROTECTED = 7U;

		// Token: 0x04002233 RID: 8755
		internal const uint CLR_UNIQUE_CONTAINER = 8U;

		// Token: 0x04002234 RID: 8756
		internal const uint CLR_ALGID = 9U;

		// Token: 0x04002235 RID: 8757
		internal const uint CLR_PP_CLIENT_HWND = 10U;

		// Token: 0x04002236 RID: 8758
		internal const uint CLR_PP_PIN = 11U;

		// Token: 0x04002237 RID: 8759
		internal const string OID_RSA_SMIMEalgCMS3DESwrap = "1.2.840.113549.1.9.16.3.6";

		// Token: 0x04002238 RID: 8760
		internal const string OID_RSA_MD5 = "1.2.840.113549.2.5";

		// Token: 0x04002239 RID: 8761
		internal const string OID_RSA_RC2CBC = "1.2.840.113549.3.2";

		// Token: 0x0400223A RID: 8762
		internal const string OID_RSA_DES_EDE3_CBC = "1.2.840.113549.3.7";

		// Token: 0x0400223B RID: 8763
		internal const string OID_OIWSEC_desCBC = "1.3.14.3.2.7";

		// Token: 0x0400223C RID: 8764
		internal const string OID_OIWSEC_SHA1 = "1.3.14.3.2.26";

		// Token: 0x0400223D RID: 8765
		internal const string OID_OIWSEC_SHA256 = "2.16.840.1.101.3.4.2.1";

		// Token: 0x0400223E RID: 8766
		internal const string OID_OIWSEC_SHA384 = "2.16.840.1.101.3.4.2.2";

		// Token: 0x0400223F RID: 8767
		internal const string OID_OIWSEC_SHA512 = "2.16.840.1.101.3.4.2.3";

		// Token: 0x04002240 RID: 8768
		internal const string OID_OIWSEC_RIPEMD160 = "1.3.36.3.2.1";
	}
}
