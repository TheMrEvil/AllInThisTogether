using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeNumericSchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000336 RID: 822
	public sealed class TypeNumericSchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeNumericSchemaImporterExtension" /> class.</summary>
		// Token: 0x0600263A RID: 9786 RVA: 0x000AA466 File Offset: 0x000A8666
		public TypeNumericSchemaImporterExtension() : base("numeric", "System.Data.SqlTypes.SqlDecimal", false)
		{
		}
	}
}
