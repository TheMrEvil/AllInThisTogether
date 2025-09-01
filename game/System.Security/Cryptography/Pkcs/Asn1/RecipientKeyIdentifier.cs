using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000B7 RID: 183
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class RecipientKeyIdentifier
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x00002145 File Offset: 0x00000345
		public RecipientKeyIdentifier()
		{
		}

		// Token: 0x04000326 RID: 806
		[OctetString]
		internal ReadOnlyMemory<byte> SubjectKeyIdentifier;

		// Token: 0x04000327 RID: 807
		[OptionalValue]
		[GeneralizedTime]
		internal DateTimeOffset? Date;

		// Token: 0x04000328 RID: 808
		[OptionalValue]
		internal OtherKeyAttributeAsn? Other;
	}
}
