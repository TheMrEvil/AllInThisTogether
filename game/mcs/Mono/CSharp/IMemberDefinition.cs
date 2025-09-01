using System;

namespace Mono.CSharp
{
	// Token: 0x0200018F RID: 399
	public interface IMemberDefinition
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060015AD RID: 5549
		bool? CLSAttributeValue { get; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060015AE RID: 5550
		string Name { get; }

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060015AF RID: 5551
		bool IsImported { get; }

		// Token: 0x060015B0 RID: 5552
		string[] ConditionalConditions();

		// Token: 0x060015B1 RID: 5553
		ObsoleteAttribute GetAttributeObsolete();

		// Token: 0x060015B2 RID: 5554
		void SetIsAssigned();

		// Token: 0x060015B3 RID: 5555
		void SetIsUsed();
	}
}
