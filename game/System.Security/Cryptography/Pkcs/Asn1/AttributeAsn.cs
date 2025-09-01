using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x0200009F RID: 159
	internal struct AttributeAsn
	{
		// Token: 0x040002E0 RID: 736
		public Oid AttrType;

		// Token: 0x040002E1 RID: 737
		[AnyValue]
		public ReadOnlyMemory<byte> AttrValues;
	}
}
