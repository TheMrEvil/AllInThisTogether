using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeDecimalSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000335 RID: 821
	public sealed class TypeDecimalSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeDecimalSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002639 RID: 9785 RVA: 0x000AA453 File Offset: 0x000A8653
		public TypeDecimalSchemaImporterExtension() : base("decimal", "System.Data.SqlTypes.SqlDecimal", false)
		{
		}
	}
}
