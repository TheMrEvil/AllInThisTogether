using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeNCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200032D RID: 813
	public sealed class TypeNCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeNCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002631 RID: 9777 RVA: 0x000AA3BB File Offset: 0x000A85BB
		public TypeNCharSchemaImporterExtension() : base("nchar", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
