using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005C RID: 92
	public static class ListPool<T>
	{
		// Token: 0x060002F5 RID: 757 RVA: 0x0000EB96 File Offset: 0x0000CD96
		public static List<T> Get()
		{
			return ListPool<T>.s_Pool.Get();
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000EBA2 File Offset: 0x0000CDA2
		public static ObjectPool<List<T>>.PooledObject Get(out List<T> value)
		{
			return ListPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000EBAF File Offset: 0x0000CDAF
		public static void Release(List<T> toRelease)
		{
			ListPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000EBBC File Offset: 0x0000CDBC
		// Note: this type is marked as 'beforefieldinit'.
		static ListPool()
		{
		}

		// Token: 0x04000201 RID: 513
		private static readonly ObjectPool<List<T>> s_Pool = new ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		}, true);

		// Token: 0x02000141 RID: 321
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000847 RID: 2119 RVA: 0x00022D35 File Offset: 0x00020F35
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000848 RID: 2120 RVA: 0x00022D41 File Offset: 0x00020F41
			public <>c()
			{
			}

			// Token: 0x06000849 RID: 2121 RVA: 0x00022D49 File Offset: 0x00020F49
			internal void <.cctor>b__4_0(List<T> l)
			{
				l.Clear();
			}

			// Token: 0x04000509 RID: 1289
			public static readonly ListPool<T>.<>c <>9 = new ListPool<T>.<>c();
		}
	}
}
