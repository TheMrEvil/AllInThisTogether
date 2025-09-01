using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeDateTimeSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200033E RID: 830
	public sealed class TypeDateTimeSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeDateTimeSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002642 RID: 9794 RVA: 0x000AA4F7 File Offset: 0x000A86F7
		public TypeDateTimeSchemaImporterExtension() : base("datetime", "System.Data.SqlTypes.SqlDateTime")
		{
		}
	}
}
