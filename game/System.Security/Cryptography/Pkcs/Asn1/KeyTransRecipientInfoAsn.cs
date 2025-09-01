using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class KeyTransRecipientInfoAsn
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00002145 File Offset: 0x00000345
		public KeyTransRecipientInfoAsn()
		{
		}

		// Token: 0x0400030A RID: 778
		internal int Version;

		// Token: 0x0400030B RID: 779
		internal RecipientIdentifierAsn Rid;

		// Token: 0x0400030C RID: 780
		internal AlgorithmIdentifierAsn KeyEncryptionAlgorithm;

		// Token: 0x0400030D RID: 781
		[OctetString]
		internal ReadOnlyMemory<byte> EncryptedKey;
	}
}
