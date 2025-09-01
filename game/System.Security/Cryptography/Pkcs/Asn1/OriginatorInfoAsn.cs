using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000AF RID: 175
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class OriginatorInfoAsn
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00002145 File Offset: 0x00000345
		public OriginatorInfoAsn()
		{
		}

		// Token: 0x04000313 RID: 787
		[OptionalValue]
		[ExpectedTag(0)]
		[SetOf]
		public CertificateChoiceAsn[] CertificateSet;

		// Token: 0x04000314 RID: 788
		[OptionalValue]
		[ExpectedTag(1)]
		[AnyValue]
		public ReadOnlyMemory<byte>? RevocationInfoChoices;
	}
}
