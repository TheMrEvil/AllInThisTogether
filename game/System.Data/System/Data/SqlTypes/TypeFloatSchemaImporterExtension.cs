using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeFloatSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200033C RID: 828
	public sealed class TypeFloatSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeFloatSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002640 RID: 9792 RVA: 0x000AA4D3 File Offset: 0x000A86D3
		public TypeFloatSchemaImporterExtension() : base("float", "System.Data.SqlTypes.SqlDouble")
		{
		}
	}
}
