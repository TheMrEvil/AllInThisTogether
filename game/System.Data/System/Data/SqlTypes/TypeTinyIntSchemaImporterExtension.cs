using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeTinyIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200033A RID: 826
	public sealed class TypeTinyIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeTinyIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600263E RID: 9790 RVA: 0x000AA4AF File Offset: 0x000A86AF
		public TypeTinyIntSchemaImporterExtension() : base("tinyint", "System.Data.SqlTypes.SqlByte")
		{
		}
	}
}
