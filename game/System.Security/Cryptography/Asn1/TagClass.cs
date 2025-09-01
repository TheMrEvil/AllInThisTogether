using System;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000F6 RID: 246
	internal enum TagClass : byte
	{
		// Token: 0x040003C5 RID: 965
		Universal,
		// Token: 0x040003C6 RID: 966
		Application = 64,
		// Token: 0x040003C7 RID: 967
		ContextSpecific = 128,
		// Token: 0x040003C8 RID: 968
		Private = 192
	}
}
