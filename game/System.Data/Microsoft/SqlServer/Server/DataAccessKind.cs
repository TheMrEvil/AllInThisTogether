using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Describes the type of access to user data for a user-defined method or function.</summary>
	// Token: 0x02000057 RID: 87
	[Serializable]
	public enum DataAccessKind
	{
		/// <summary>The method or function does not access user data.</summary>
		// Token: 0x04000543 RID: 1347
		None,
		/// <summary>The method or function reads user data.</summary>
		// Token: 0x04000544 RID: 1348
		Read
	}
}
