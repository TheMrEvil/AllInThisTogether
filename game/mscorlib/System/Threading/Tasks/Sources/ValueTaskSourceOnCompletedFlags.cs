using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000387 RID: 903
	[Flags]
	public enum ValueTaskSourceOnCompletedFlags
	{
		// Token: 0x04001D75 RID: 7541
		None = 0,
		// Token: 0x04001D76 RID: 7542
		UseSchedulingContext = 1,
		// Token: 0x04001D77 RID: 7543
		FlowExecutionContext = 2
	}
}
