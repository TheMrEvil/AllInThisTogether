using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBigIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000337 RID: 823
	public sealed class TypeBigIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBigIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600263B RID: 9787 RVA: 0x000AA479 File Offset: 0x000A8679
		public TypeBigIntSchemaImporterExtension() : base("bigint", "System.Data.SqlTypes.SqlInt64")
		{
		}
	}
}
