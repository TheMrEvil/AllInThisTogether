using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200028D RID: 653
	[Flags]
	internal enum SNISMUXFlags
	{
		// Token: 0x040014E1 RID: 5345
		SMUX_SYN = 1,
		// Token: 0x040014E2 RID: 5346
		SMUX_ACK = 2,
		// Token: 0x040014E3 RID: 5347
		SMUX_FIN = 4,
		// Token: 0x040014E4 RID: 5348
		SMUX_DATA = 8
	}
}
