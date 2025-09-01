using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeNVarCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200032F RID: 815
	public sealed class TypeNVarCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeNVarCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002633 RID: 9779 RVA: 0x000AA3E1 File Offset: 0x000A85E1
		public TypeNVarCharSchemaImporterExtension() : base("nvarchar", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
