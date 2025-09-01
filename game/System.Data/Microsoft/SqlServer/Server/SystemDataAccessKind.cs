using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Describes the type of access to system data for a user-defined method or function.</summary>
	// Token: 0x02000058 RID: 88
	[Serializable]
	public enum SystemDataAccessKind
	{
		/// <summary>The method or function does not access system data.</summary>
		// Token: 0x04000546 RID: 1350
		None,
		/// <summary>The method or function reads system data.</summary>
		// Token: 0x04000547 RID: 1351
		Read
	}
}
