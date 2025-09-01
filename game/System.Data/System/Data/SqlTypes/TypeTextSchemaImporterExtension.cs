using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeTextSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000330 RID: 816
	public sealed class TypeTextSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeTextSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002634 RID: 9780 RVA: 0x000AA3F4 File Offset: 0x000A85F4
		public TypeTextSchemaImporterExtension() : base("text", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
