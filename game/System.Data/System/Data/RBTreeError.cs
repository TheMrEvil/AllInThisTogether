using System;

namespace System.Data
{
	// Token: 0x02000117 RID: 279
	internal enum RBTreeError
	{
		// Token: 0x0400099A RID: 2458
		InvalidPageSize = 1,
		// Token: 0x0400099B RID: 2459
		PagePositionInSlotInUse = 3,
		// Token: 0x0400099C RID: 2460
		NoFreeSlots,
		// Token: 0x0400099D RID: 2461
		InvalidStateinInsert,
		// Token: 0x0400099E RID: 2462
		InvalidNextSizeInDelete = 7,
		// Token: 0x0400099F RID: 2463
		InvalidStateinDelete,
		// Token: 0x040009A0 RID: 2464
		InvalidNodeSizeinDelete,
		// Token: 0x040009A1 RID: 2465
		InvalidStateinEndDelete,
		// Token: 0x040009A2 RID: 2466
		CannotRotateInvalidsuccessorNodeinDelete,
		// Token: 0x040009A3 RID: 2467
		IndexOutOFRangeinGetNodeByIndex = 13,
		// Token: 0x040009A4 RID: 2468
		RBDeleteFixup,
		// Token: 0x040009A5 RID: 2469
		UnsupportedAccessMethod1,
		// Token: 0x040009A6 RID: 2470
		UnsupportedAccessMethod2,
		// Token: 0x040009A7 RID: 2471
		UnsupportedAccessMethodInNonNillRootSubtree,
		// Token: 0x040009A8 RID: 2472
		AttachedNodeWithZerorbTreeNodeId,
		// Token: 0x040009A9 RID: 2473
		CompareNodeInDataRowTree,
		// Token: 0x040009AA RID: 2474
		CompareSateliteTreeNodeInDataRowTree,
		// Token: 0x040009AB RID: 2475
		NestedSatelliteTreeEnumerator
	}
}
