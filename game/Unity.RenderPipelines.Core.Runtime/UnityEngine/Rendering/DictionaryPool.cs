using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005E RID: 94
	public static class DictionaryPool<TKey, TValue>
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000EC1E File Offset: 0x0000CE1E
		public static Dictionary<TKey, TValue> Get()
		{
			return DictionaryPool<TKey, TValue>.s_Pool.Get();
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		public static ObjectPool<Dictionary<TKey, TValue>>.PooledObject Get(out Dictionary<TKey, TValue> value)
		{
			return DictionaryPool<TKey, TValue>.s_Pool.Get(out value);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000EC37 File Offset: 0x0000CE37
		public static void Release(Dictionary<TKey, TValue> toRelease)
		{
			DictionaryPool<TKey, TValue>.s_Pool.Release(toRelease);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000EC44 File Offset: 0x0000CE44
		// Note: this type is marked as 'beforefieldinit'.
		static DictionaryPool()
		{
		}

		// Token: 0x04000203 RID: 515
		private static readonly ObjectPool<Dictionary<TKey, TValue>> s_Pool = new ObjectPool<Dictionary<TKey, TValue>>(null, delegate(Dictionary<TKey, TValue> l)
		{
			l.Clear();
		}, true);

		// Token: 0x02000143 RID: 323
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600084D RID: 2125 RVA: 0x00022D6D File Offset: 0x00020F6D
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600084E RID: 2126 RVA: 0x00022D79 File Offset: 0x00020F79
			public <>c()
			{
			}

			// Token: 0x0600084F RID: 2127 RVA: 0x00022D81 File Offset: 0x00020F81
			internal void <.cctor>b__4_0(Dictionary<TKey, TValue> l)
			{
				l.Clear();
			}

			// Token: 0x0400050B RID: 1291
			public static readonly DictionaryPool<TKey, TValue>.<>c <>9 = new DictionaryPool<TKey, TValue>.<>c();
		}
	}
}
