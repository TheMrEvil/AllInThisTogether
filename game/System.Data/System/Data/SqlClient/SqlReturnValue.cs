using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000269 RID: 617
	internal sealed class SqlReturnValue : SqlMetaDataPriv
	{
		// Token: 0x06001CDF RID: 7391 RVA: 0x0008974E File Offset: 0x0008794E
		internal SqlReturnValue()
		{
			this.value = new SqlBuffer();
		}

		// Token: 0x04001418 RID: 5144
		internal string parameter;

		// Token: 0x04001419 RID: 5145
		internal readonly SqlBuffer value;
	}
}
