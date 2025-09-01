using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000B4 RID: 180
	internal struct RecipientEncryptedKeyAsn
	{
		// Token: 0x04000320 RID: 800
		internal KeyAgreeRecipientIdentifierAsn Rid;

		// Token: 0x04000321 RID: 801
		[OctetString]
		internal ReadOnlyMemory<byte> EncryptedKey;
	}
}
