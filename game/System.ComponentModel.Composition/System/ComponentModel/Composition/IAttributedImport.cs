using System;
using System.ComponentModel.Composition.Primitives;

namespace System.ComponentModel.Composition
{
	// Token: 0x02000041 RID: 65
	internal interface IAttributedImport
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001DB RID: 475
		string ContractName { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001DC RID: 476
		Type ContractType { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001DD RID: 477
		bool AllowRecomposition { get; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001DE RID: 478
		CreationPolicy RequiredCreationPolicy { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001DF RID: 479
		ImportCardinality Cardinality { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001E0 RID: 480
		ImportSource Source { get; }
	}
}
