using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200032E RID: 814
	public sealed class TypeVarCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002632 RID: 9778 RVA: 0x000AA3CE File Offset: 0x000A85CE
		public TypeVarCharSchemaImporterExtension() : base("varchar", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
