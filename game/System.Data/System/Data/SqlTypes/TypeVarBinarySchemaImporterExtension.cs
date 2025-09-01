using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarBinarySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000332 RID: 818
	public sealed class TypeVarBinarySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarBinarySchemaImporterExtension" /> class.</summary>
		// Token: 0x06002636 RID: 9782 RVA: 0x000AA41A File Offset: 0x000A861A
		public TypeVarBinarySchemaImporterExtension() : base("varbinary", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
