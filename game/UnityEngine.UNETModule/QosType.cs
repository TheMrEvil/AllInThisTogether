using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000004 RID: 4
	public enum QosType
	{
		// Token: 0x04000009 RID: 9
		Unreliable,
		// Token: 0x0400000A RID: 10
		UnreliableFragmented,
		// Token: 0x0400000B RID: 11
		UnreliableSequenced,
		// Token: 0x0400000C RID: 12
		Reliable,
		// Token: 0x0400000D RID: 13
		ReliableFragmented,
		// Token: 0x0400000E RID: 14
		ReliableSequenced,
		// Token: 0x0400000F RID: 15
		StateUpdate,
		// Token: 0x04000010 RID: 16
		ReliableStateUpdate,
		// Token: 0x04000011 RID: 17
		AllCostDelivery,
		// Token: 0x04000012 RID: 18
		UnreliableFragmentedSequenced,
		// Token: 0x04000013 RID: 19
		ReliableFragmentedSequenced
	}
}
