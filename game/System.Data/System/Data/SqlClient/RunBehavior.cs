using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200025C RID: 604
	internal enum RunBehavior
	{
		// Token: 0x040013A2 RID: 5026
		UntilDone = 1,
		// Token: 0x040013A3 RID: 5027
		ReturnImmediately,
		// Token: 0x040013A4 RID: 5028
		Clean = 5,
		// Token: 0x040013A5 RID: 5029
		Attention = 13
	}
}
