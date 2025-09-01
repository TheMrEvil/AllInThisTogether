using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000C9 RID: 201
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class EssCertIdV2
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x00002145 File Offset: 0x00000345
		public EssCertIdV2()
		{
		}

		// Token: 0x04000382 RID: 898
		[DefaultValue(new byte[]
		{
			48,
			11,
			6,
			9,
			96,
			134,
			72,
			1,
			101,
			3,
			4,
			2,
			1
		})]
		public AlgorithmIdentifierAsn HashAlgorithm;

		// Token: 0x04000383 RID: 899
		[OctetString]
		public ReadOnlyMemory<byte> Hash;

		// Token: 0x04000384 RID: 900
		[OptionalValue]
		public CadesIssuerSerial? IssuerSerial;
	}
}
