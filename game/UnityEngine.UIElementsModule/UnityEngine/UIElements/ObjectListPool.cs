using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E3 RID: 227
	internal class ObjectListPool<T>
	{
		// Token: 0x06000744 RID: 1860 RVA: 0x0001AB48 File Offset: 0x00018D48
		public static List<T> Get()
		{
			return ObjectListPool<T>.pool.Get();
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001AB64 File Offset: 0x00018D64
		public static void Release(List<T> elements)
		{
			elements.Clear();
			ObjectListPool<T>.pool.Release(elements);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000020C2 File Offset: 0x000002C2
		public ObjectListPool()
		{
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001AB7A File Offset: 0x00018D7A
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectListPool()
		{
		}

		// Token: 0x040002F7 RID: 759
		private static ObjectPool<List<T>> pool = new ObjectPool<List<T>>(20);
	}
}
