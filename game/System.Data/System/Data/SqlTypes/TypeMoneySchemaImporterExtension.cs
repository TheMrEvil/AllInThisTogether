using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeMoneySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000340 RID: 832
	public sealed class TypeMoneySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeMoneySchemaImporterExtension" /> class.</summary>
		// Token: 0x06002644 RID: 9796 RVA: 0x000AA51B File Offset: 0x000A871B
		public TypeMoneySchemaImporterExtension() : base("money", "System.Data.SqlTypes.SqlMoney")
		{
		}
	}
}
