using System;

namespace System.Data.SqlClient
{
	/// <summary>Specifies a value for <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.ApplicationIntent" />. Possible values are <see langword="ReadWrite" /> and <see langword="ReadOnly" />.</summary>
	// Token: 0x02000186 RID: 390
	public enum ApplicationIntent
	{
		/// <summary>The application workload type when connecting to a server is read write.</summary>
		// Token: 0x04000CA3 RID: 3235
		ReadWrite,
		/// <summary>The application workload type when connecting to a server is read only.</summary>
		// Token: 0x04000CA4 RID: 3236
		ReadOnly
	}
}
