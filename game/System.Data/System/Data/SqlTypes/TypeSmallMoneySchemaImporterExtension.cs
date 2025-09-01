using System;

namespace System.Data.SqlTypes
{
	/// <summary>The <see cref="T:System.Data.SqlTypes.TypeSmallMoneySchemaImporterExtension" /> class is not intended for use as a stand-alone component, but as a class from which other classes derive standard functionality.</summary>
	// Token: 0x02000341 RID: 833
	public sealed class TypeSmallMoneySchemaImporterExtension : SqlTypesSchemaImporterExtensionHelper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.TypeSmallMoneySchemaImporterExtension" /> class.</summary>
		// Token: 0x06002645 RID: 9797 RVA: 0x000AA52D File Offset: 0x000A872D
		public TypeSmallMoneySchemaImporterExtension() : base("smallmoney", "System.Data.SqlTypes.SqlMoney")
		{
		}
	}
}
