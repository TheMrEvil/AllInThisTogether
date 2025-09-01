using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace System.Data.SqlClient
{
	// Token: 0x02000188 RID: 392
	internal class ParameterPeekAheadValue
	{
		// Token: 0x0600141E RID: 5150 RVA: 0x00003D93 File Offset: 0x00001F93
		public ParameterPeekAheadValue()
		{
		}

		// Token: 0x04000CA5 RID: 3237
		internal IEnumerator<SqlDataRecord> Enumerator;

		// Token: 0x04000CA6 RID: 3238
		internal SqlDataRecord FirstRecord;
	}
}
