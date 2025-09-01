using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeBinarySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000333 RID: 819
	public sealed class TypeBinarySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeBinarySchemaImporterExtension" /> class.</summary>
		// Token: 0x06002637 RID: 9783 RVA: 0x000AA42D File Offset: 0x000A862D
		public TypeBinarySchemaImporterExtension() : base("binary", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
