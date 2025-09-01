using System;

namespace System.Data.Odbc
{
	// Token: 0x020002A8 RID: 680
	internal sealed class DbSchemaInfo
	{
		// Token: 0x06001F41 RID: 8001 RVA: 0x00003D93 File Offset: 0x00001F93
		internal DbSchemaInfo()
		{
		}

		// Token: 0x040015A7 RID: 5543
		internal string _name;

		// Token: 0x040015A8 RID: 5544
		internal string _typename;

		// Token: 0x040015A9 RID: 5545
		internal Type _type;

		// Token: 0x040015AA RID: 5546
		internal ODBC32.SQL_TYPE? _dbtype;
	}
}
