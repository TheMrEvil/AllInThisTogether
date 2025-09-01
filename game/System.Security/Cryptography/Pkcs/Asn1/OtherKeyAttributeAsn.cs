using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000B1 RID: 177
	internal struct OtherKeyAttributeAsn
	{
		// Token: 0x04000317 RID: 791
		[ObjectIdentifier]
		internal string KeyAttrId;

		// Token: 0x04000318 RID: 792
		[OptionalValue]
		[AnyValue]
		internal ReadOnlyMemory<byte>? KeyAttr;
	}
}
