using System;

namespace System.Net
{
	// Token: 0x020005EC RID: 1516
	internal enum CertificateEncoding
	{
		// Token: 0x04001B88 RID: 7048
		Zero,
		// Token: 0x04001B89 RID: 7049
		X509AsnEncoding,
		// Token: 0x04001B8A RID: 7050
		X509NdrEncoding,
		// Token: 0x04001B8B RID: 7051
		Pkcs7AsnEncoding = 65536,
		// Token: 0x04001B8C RID: 7052
		Pkcs7NdrEncoding = 131072,
		// Token: 0x04001B8D RID: 7053
		AnyAsnEncoding = 65537
	}
}
