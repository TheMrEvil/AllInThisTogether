using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeSmallIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000339 RID: 825
	public sealed class TypeSmallIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeSmallIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600263D RID: 9789 RVA: 0x000AA49D File Offset: 0x000A869D
		public TypeSmallIntSchemaImporterExtension() : base("smallint", "System.Data.SqlTypes.SqlInt16")
		{
		}
	}
}
