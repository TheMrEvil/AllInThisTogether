using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x02000003 RID: 3
	internal interface IKdTree<TKey, TValue> : IEnumerable<KdTreeNode<TKey, TValue>>, IEnumerable
	{
		// Token: 0x06000008 RID: 8
		bool Add(TKey[] point, TValue value);

		// Token: 0x06000009 RID: 9
		bool TryFindValueAt(TKey[] point, out TValue value);

		// Token: 0x0600000A RID: 10
		TValue FindValueAt(TKey[] point);

		// Token: 0x0600000B RID: 11
		bool TryFindValue(TValue value, out TKey[] point);

		// Token: 0x0600000C RID: 12
		TKey[] FindValue(TValue value);

		// Token: 0x0600000D RID: 13
		KdTreeNode<TKey, TValue>[] RadialSearch(TKey[] center, TKey radius, int count);

		// Token: 0x0600000E RID: 14
		void RemoveAt(TKey[] point);

		// Token: 0x0600000F RID: 15
		void Clear();

		// Token: 0x06000010 RID: 16
		KdTreeNode<TKey, TValue>[] GetNearestNeighbours(TKey[] point, int count = 2147483647);

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17
		int Count { get; }
	}
}
