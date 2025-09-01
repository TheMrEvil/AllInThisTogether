using System;

namespace Mono.CSharp
{
	// Token: 0x020001A1 RID: 417
	public interface IVariableReference : IFixedExpression
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001624 RID: 5668
		bool IsHoisted { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001625 RID: 5669
		string Name { get; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001626 RID: 5670
		VariableInfo VariableInfo { get; }

		// Token: 0x06001627 RID: 5671
		void SetHasAddressTaken();
	}
}
