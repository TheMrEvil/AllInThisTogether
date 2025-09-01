using System;

namespace System.Data
{
	/// <summary>Indicates the schema serialization mode for a typed <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x02000123 RID: 291
	public enum SchemaSerializationMode
	{
		/// <summary>Includes schema serialization for a typed <see cref="T:System.Data.DataSet" />. The default.</summary>
		// Token: 0x040009E2 RID: 2530
		IncludeSchema = 1,
		/// <summary>Skips schema serialization for a typed <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x040009E3 RID: 2531
		ExcludeSchema
	}
}
