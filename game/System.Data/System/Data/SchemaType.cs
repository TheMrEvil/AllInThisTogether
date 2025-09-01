using System;

namespace System.Data
{
	/// <summary>Specifies how to handle existing schema mappings when performing a <see cref="M:System.Data.Common.DataAdapter.FillSchema(System.Data.DataSet,System.Data.SchemaType)" /> operation.</summary>
	// Token: 0x02000124 RID: 292
	public enum SchemaType
	{
		/// <summary>Ignore any table mappings on the DataAdapter. Configure the <see cref="T:System.Data.DataSet" /> using the incoming schema without applying any transformations.</summary>
		// Token: 0x040009E5 RID: 2533
		Source = 1,
		/// <summary>Apply any existing table mappings to the incoming schema. Configure the <see cref="T:System.Data.DataSet" /> with the transformed schema.</summary>
		// Token: 0x040009E6 RID: 2534
		Mapped
	}
}
