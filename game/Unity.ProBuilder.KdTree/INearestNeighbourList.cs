using System;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x0200000A RID: 10
	internal interface INearestNeighbourList<TItem, TDistance>
	{
		// Token: 0x06000047 RID: 71
		bool Add(TItem item, TDistance distance);

		// Token: 0x06000048 RID: 72
		TItem GetFurtherest();

		// Token: 0x06000049 RID: 73
		TItem RemoveFurtherest();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004A RID: 74
		int MaxCapacity { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004B RID: 75
		int Count { get; }
	}
}
