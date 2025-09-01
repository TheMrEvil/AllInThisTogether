using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeUniqueIdentifierSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000342 RID: 834
	public sealed class TypeUniqueIdentifierSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeUniqueIdentifierSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002646 RID: 9798 RVA: 0x000AA53F File Offset: 0x000A873F
		public TypeUniqueIdentifierSchemaImporterExtension() : base("uniqueidentifier", "System.Data.SqlTypes.SqlGuid")
		{
		}
	}
}
