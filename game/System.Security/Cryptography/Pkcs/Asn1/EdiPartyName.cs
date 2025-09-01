using System;
using System.Security.Cryptography.Asn1;

namespace System.Security.Cryptography.Pkcs.Asn1
{
	// Token: 0x020000A7 RID: 167
	internal struct EdiPartyName
	{
		// Token: 0x040002FA RID: 762
		[OptionalValue]
		internal DirectoryString? NameAssigner;

		// Token: 0x040002FB RID: 763
		internal DirectoryString PartyName;
	}
}
