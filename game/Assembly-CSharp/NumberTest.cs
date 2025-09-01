using System;
using UnityEngine;

// Token: 0x0200032B RID: 811
public enum NumberTest
{
	// Token: 0x04001C18 RID: 7192
	[InspectorName("A = B")]
	Equals,
	// Token: 0x04001C19 RID: 7193
	[InspectorName("A != B")]
	NotEquals,
	// Token: 0x04001C1A RID: 7194
	[InspectorName("A < B")]
	LessThan,
	// Token: 0x04001C1B RID: 7195
	[InspectorName("A > B")]
	GreaterThan,
	// Token: 0x04001C1C RID: 7196
	[InspectorName("B <= A <= C")]
	Between,
	// Token: 0x04001C1D RID: 7197
	[InspectorName("A <= B")]
	LTOrE,
	// Token: 0x04001C1E RID: 7198
	[InspectorName("A >= B")]
	GTOrE,
	// Token: 0x04001C1F RID: 7199
	[InspectorName("A has flag B")]
	HasFlag
}
