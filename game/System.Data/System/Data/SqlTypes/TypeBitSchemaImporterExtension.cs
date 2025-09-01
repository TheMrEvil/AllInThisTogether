using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBitSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200033B RID: 827
	public sealed class TypeBitSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBitSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600263F RID: 9791 RVA: 0x000AA4C1 File Offset: 0x000A86C1
		public TypeBitSchemaImporterExtension() : base("bit", "System.Data.SqlTypes.SqlBoolean")
		{
		}
	}
}
