using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeRealSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200033D RID: 829
	public sealed class TypeRealSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeRealSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002641 RID: 9793 RVA: 0x000AA4E5 File Offset: 0x000A86E5
		public TypeRealSchemaImporterExtension() : base("real", "System.Data.SqlTypes.SqlSingle")
		{
		}
	}
}
