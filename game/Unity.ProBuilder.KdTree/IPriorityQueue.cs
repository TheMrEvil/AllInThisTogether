using System;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x02000004 RID: 4
	internal interface IPriorityQueue<TItem, TPriority>
	{
		// Token: 0x06000012 RID: 18
		void Enqueue(TItem item, TPriority priority);

		// Token: 0x06000013 RID: 19
		TItem Dequeue();

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000014 RID: 20
		int Count { get; }
	}
}
