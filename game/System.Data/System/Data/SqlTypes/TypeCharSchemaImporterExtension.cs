using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeCharSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x0200032C RID: 812
	public sealed class TypeCharSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeCharSchemaImporterExtension" /> class.</summary>
		// Token: 0x06002630 RID: 9776 RVA: 0x000AA3A8 File Offset: 0x000A85A8
		public TypeCharSchemaImporterExtension() : base("char", "System.Data.SqlTypes.SqlString", false)
		{
		}
	}
}
