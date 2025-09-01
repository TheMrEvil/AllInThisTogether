using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000263 RID: 611
	internal sealed class SqlLoginAck
	{
		// Token: 0x06001CC8 RID: 7368 RVA: 0x00003D93 File Offset: 0x00001F93
		public SqlLoginAck()
		{
		}

		// Token: 0x040013DA RID: 5082
		internal byte majorVersion;

		// Token: 0x040013DB RID: 5083
		internal byte minorVersion;

		// Token: 0x040013DC RID: 5084
		internal short buildNum;

		// Token: 0x040013DD RID: 5085
		internal uint tdsVersion;
	}
}
