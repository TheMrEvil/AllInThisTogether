using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class KeyAgreeRecipientInfoAsn
	{
		// Token: 0x06000513 RID: 1299 RVA: 0x00002145 File Offset: 0x00000345
		public KeyAgreeRecipientInfoAsn()
		{
		}

		// Token: 0x04000305 RID: 773
		internal int Version;

		// Token: 0x04000306 RID: 774
		[ExpectedTag(0, ExplicitTag = true)]
		internal OriginatorIdentifierOrKeyAsn Originator;

		// Token: 0x04000307 RID: 775
		[ExpectedTag(1, ExplicitTag = true)]
		[OctetString]
		[OptionalValue]
		internal ReadOnlyMemory<byte>? Ukm;

		// Token: 0x04000308 RID: 776
		internal AlgorithmIdentifierAsn KeyEncryptionAlgorithm;

		// Token: 0x04000309 RID: 777
		internal RecipientEncryptedKeyAsn[] RecipientEncryptedKeys;
	}
}
