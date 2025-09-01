using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200025B RID: 603
	internal enum PreLoginOptions
	{
		// Token: 0x04001398 RID: 5016
		VERSION,
		// Token: 0x04001399 RID: 5017
		ENCRYPT,
		// Token: 0x0400139A RID: 5018
		INSTANCE,
		// Token: 0x0400139B RID: 5019
		THREADID,
		// Token: 0x0400139C RID: 5020
		MARS,
		// Token: 0x0400139D RID: 5021
		TRACEID,
		// Token: 0x0400139E RID: 5022
		FEDAUTHREQUIRED,
		// Token: 0x0400139F RID: 5023
		NUMOPT,
		// Token: 0x040013A0 RID: 5024
		LASTOPT = 255
	}
}
