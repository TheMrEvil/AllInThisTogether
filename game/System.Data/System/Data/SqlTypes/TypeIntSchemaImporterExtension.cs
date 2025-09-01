using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeIntSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000338 RID: 824
	public sealed class TypeIntSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeIntSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600263C RID: 9788 RVA: 0x000AA48B File Offset: 0x000A868B
		public TypeIntSchemaImporterExtension() : base("int", "System.Data.SqlTypes.SqlInt32")
		{
		}
	}
}
