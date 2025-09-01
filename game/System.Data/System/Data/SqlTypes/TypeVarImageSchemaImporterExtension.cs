using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeVarImageSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000334 RID: 820
	public sealed class TypeVarImageSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeVarImageSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002638 RID: 9784 RVA: 0x000AA440 File Offset: 0x000A8640
		public TypeVarImageSchemaImporterExtension() : base("image", "System.Data.SqlTypes.SqlBinary", false)
		{
		}
	}
}
