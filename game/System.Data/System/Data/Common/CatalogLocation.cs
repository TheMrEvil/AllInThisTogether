using System;

namespace System.Data.Common
{
	/// <summary>Indicates the position of the catalog name in a qualified table name in a text command.</summary>
	// Token: 0x02000372 RID: 882
	public enum CatalogLocation
	{
		/// <summary>Indicates that the position of the catalog name occurs before the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x04001A63 RID: 6755
		Start = 1,
		/// <summary>Indicates that the position of the catalog name occurs after the schema portion of a fully qualified table name in a text command.</summary>
		// Token: 0x04001A64 RID: 6756
		End
	}
}
