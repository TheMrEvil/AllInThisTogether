using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeNTextSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000331 RID: 817
	public sealed class TypeNTextSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeNTextSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002635 RID: 9781 RVA: 0x000AA407 File Offset: 0x000A8607
		public TypeNTextSchemaImporterExtension() : base("ntext", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
