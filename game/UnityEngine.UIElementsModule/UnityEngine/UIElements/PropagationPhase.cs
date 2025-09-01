using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E7 RID: 487
	public enum PropagationPhase
	{
		// Token: 0x0400070E RID: 1806
		None,
		// Token: 0x0400070F RID: 1807
		TrickleDown,
		// Token: 0x04000710 RID: 1808
		AtTarget,
		// Token: 0x04000711 RID: 1809
		DefaultActionAtTarget = 5,
		// Token: 0x04000712 RID: 1810
		BubbleUp = 3,
		// Token: 0x04000713 RID: 1811
		DefaultAction
	}
}
