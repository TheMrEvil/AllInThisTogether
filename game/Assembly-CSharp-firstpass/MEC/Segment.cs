using System;

namespace MEC
{
	// Token: 0x020000A2 RID: 162
	public enum Segment
	{
		// Token: 0x040005EF RID: 1519
		Invalid = -1,
		// Token: 0x040005F0 RID: 1520
		Update,
		// Token: 0x040005F1 RID: 1521
		FixedUpdate,
		// Token: 0x040005F2 RID: 1522
		LateUpdate,
		// Token: 0x040005F3 RID: 1523
		SlowUpdate,
		// Token: 0x040005F4 RID: 1524
		RealtimeUpdate,
		// Token: 0x040005F5 RID: 1525
		EditorUpdate,
		// Token: 0x040005F6 RID: 1526
		EditorSlowUpdate,
		// Token: 0x040005F7 RID: 1527
		EndOfFrame,
		// Token: 0x040005F8 RID: 1528
		ManualTimeframe
	}
}
