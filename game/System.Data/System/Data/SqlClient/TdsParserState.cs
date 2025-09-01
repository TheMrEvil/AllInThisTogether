using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200025D RID: 605
	internal enum TdsParserState
	{
		// Token: 0x040013A7 RID: 5031
		Closed,
		// Token: 0x040013A8 RID: 5032
		OpenNotLoggedIn,
		// Token: 0x040013A9 RID: 5033
		OpenLoggedIn,
		// Token: 0x040013AA RID: 5034
		Broken
	}
}
