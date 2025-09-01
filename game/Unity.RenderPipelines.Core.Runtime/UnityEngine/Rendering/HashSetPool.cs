using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005D RID: 93
	public static class HashSetPool<T>
	{
		// Token: 0x060002F9 RID: 761 RVA: 0x0000EBDA File Offset: 0x0000CDDA
		public static HashSet<T> Get()
		{
			return HashSetPool<T>.s_Pool.Get();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000EBE6 File Offset: 0x0000CDE6
		public static ObjectPool<HashSet<T>>.PooledObject Get(out HashSet<T> value)
		{
			return HashSetPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000EBF3 File Offset: 0x0000CDF3
		public static void Release(HashSet<T> toRelease)
		{
			HashSetPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000EC00 File Offset: 0x0000CE00
		// Note: this type is marked as 'beforefieldinit'.
		static HashSetPool()
		{
		}

		// Token: 0x04000202 RID: 514
		private static readonly ObjectPool<HashSet<T>> s_Pool = new ObjectPool<HashSet<T>>(null, delegate(HashSet<T> l)
		{
			l.Clear();
		}, true);

		// Token: 0x02000142 RID: 322
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600084A RID: 2122 RVA: 0x00022D51 File Offset: 0x00020F51
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600084B RID: 2123 RVA: 0x00022D5D File Offset: 0x00020F5D
			public <>c()
			{
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x00022D65 File Offset: 0x00020F65
			internal void <.cctor>b__4_0(HashSet<T> l)
			{
				l.Clear();
			}

			// Token: 0x0400050A RID: 1290
			public static readonly HashSetPool<T>.<>c <>9 = new HashSetPool<T>.<>c();
		}
	}
}
