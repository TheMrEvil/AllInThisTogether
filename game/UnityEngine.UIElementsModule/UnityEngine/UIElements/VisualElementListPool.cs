using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E2 RID: 226
	internal static class VisualElementListPool
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x0001AAC0 File Offset: 0x00018CC0
		public static List<VisualElement> Copy(List<VisualElement> elements)
		{
			List<VisualElement> list = VisualElementListPool.pool.Get();
			list.AddRange(elements);
			return list;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001AAE8 File Offset: 0x00018CE8
		public static List<VisualElement> Get(int initialCapacity = 0)
		{
			List<VisualElement> list = VisualElementListPool.pool.Get();
			bool flag = initialCapacity > 0 && list.Capacity < initialCapacity;
			if (flag)
			{
				list.Capacity = initialCapacity;
			}
			return list;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001AB24 File Offset: 0x00018D24
		public static void Release(List<VisualElement> elements)
		{
			elements.Clear();
			VisualElementListPool.pool.Release(elements);
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001AB3A File Offset: 0x00018D3A
		// Note: this type is marked as 'beforefieldinit'.
		static VisualElementListPool()
		{
		}

		// Token: 0x040002F6 RID: 758
		private static ObjectPool<List<VisualElement>> pool = new ObjectPool<List<VisualElement>>(20);
	}
}
