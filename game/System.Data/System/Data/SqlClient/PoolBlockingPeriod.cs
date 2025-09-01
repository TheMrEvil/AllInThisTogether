using System;

namespace System.Data.SqlClient
{
	/// <summary>Specifies a value for the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.PoolBlockingPeriod" /> property.</summary>
	// Token: 0x02000286 RID: 646
	public enum PoolBlockingPeriod
	{
		/// <summary>Blocking period OFF for Azure SQL servers, but ON for all other SQL servers.</summary>
		// Token: 0x040014BD RID: 5309
		Auto,
		/// <summary>Blocking period ON for all SQL servers including Azure SQL servers.</summary>
		// Token: 0x040014BE RID: 5310
		AlwaysBlock,
		/// <summary>Blocking period OFF for all SQL servers including Azure SQL servers.</summary>
		// Token: 0x040014BF RID: 5311
		NeverBlock
	}
}
