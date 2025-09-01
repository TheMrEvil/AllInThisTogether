using System;
using System.Security;

namespace System.Data.SqlClient
{
	// Token: 0x02000262 RID: 610
	internal sealed class SqlLogin
	{
		// Token: 0x06001CC7 RID: 7367 RVA: 0x000891D0 File Offset: 0x000873D0
		public SqlLogin()
		{
		}

		// Token: 0x040013C9 RID: 5065
		internal int timeout;

		// Token: 0x040013CA RID: 5066
		internal bool userInstance;

		// Token: 0x040013CB RID: 5067
		internal string hostName = "";

		// Token: 0x040013CC RID: 5068
		internal string userName = "";

		// Token: 0x040013CD RID: 5069
		internal string password = "";

		// Token: 0x040013CE RID: 5070
		internal string applicationName = "";

		// Token: 0x040013CF RID: 5071
		internal string serverName = "";

		// Token: 0x040013D0 RID: 5072
		internal string language = "";

		// Token: 0x040013D1 RID: 5073
		internal string database = "";

		// Token: 0x040013D2 RID: 5074
		internal string attachDBFilename = "";

		// Token: 0x040013D3 RID: 5075
		internal bool useReplication;

		// Token: 0x040013D4 RID: 5076
		internal string newPassword = "";

		// Token: 0x040013D5 RID: 5077
		internal bool useSSPI;

		// Token: 0x040013D6 RID: 5078
		internal int packetSize = 8000;

		// Token: 0x040013D7 RID: 5079
		internal bool readOnlyIntent;

		// Token: 0x040013D8 RID: 5080
		internal SqlCredential credential;

		// Token: 0x040013D9 RID: 5081
		internal SecureString newSecurePassword;
	}
}
