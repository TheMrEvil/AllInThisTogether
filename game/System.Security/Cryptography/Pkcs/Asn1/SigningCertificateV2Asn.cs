using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C8 RID: 200
	internal struct SigningCertificateV2Asn
	{
		// Token: 0x04000380 RID: 896
		public EssCertIdV2[] Certs;

		// Token: 0x04000381 RID: 897
		[OptionalValue]
		public PolicyInformation[] Policies;
	}
}
