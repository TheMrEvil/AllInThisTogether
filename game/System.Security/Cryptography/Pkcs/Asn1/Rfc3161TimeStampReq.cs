using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000B9 RID: 185
	internal struct Rfc3161TimeStampReq
	{
		// Token: 0x0400032C RID: 812
		public int Version;

		// Token: 0x0400032D RID: 813
		public MessageImprint MessageImprint;

		// Token: 0x0400032E RID: 814
		[OptionalValue]
		public Oid ReqPolicy;

		// Token: 0x0400032F RID: 815
		[Integer]
		[OptionalValue]
		public ReadOnlyMemory<byte>? Nonce;

		// Token: 0x04000330 RID: 816
		[DefaultValue(new byte[]
		{
			1,
			1,
			0
		})]
		public bool CertReq;

		// Token: 0x04000331 RID: 817
		[ExpectedTag(0)]
		[OptionalValue]
		internal X509ExtensionAsn[] Extensions;
	}
}
