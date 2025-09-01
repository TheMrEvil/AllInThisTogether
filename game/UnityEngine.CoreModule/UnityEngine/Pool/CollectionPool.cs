using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Pool
{
	// Token: 0x02000379 RID: 889
	public class CollectionPool<TCollection, TItem> where TCollection : class, ICollection<TItem>, new()
	{
		// Token: 0x06001E94 RID: 7828 RVA: 0x00031AC5 File Offset: 0x0002FCC5
		public static TCollection Get()
		{
			return CollectionPool<TCollection, TItem>.s_Pool.Get();
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00031AD1 File Offset: 0x0002FCD1
		public static PooledObject<TCollection> Get(out TCollection value)
		{
			return CollectionPool<TCollection, TItem>.s_Pool.Get(out value);
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00031ADE File Offset: 0x0002FCDE
		public static void Release(TCollection toRelease)
		{
			CollectionPool<TCollection, TItem>.s_Pool.Release(toRelease);
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00002072 File Offset: 0x00000272
		public CollectionPool()
		{
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x00031AEC File Offset: 0x0002FCEC
		// Note: this type is marked as 'beforefieldinit'.
		static CollectionPool()
		{
		}

		// Token: 0x04000A02 RID: 2562
		internal static readonly ObjectPool<TCollection> s_Pool = new ObjectPool<TCollection>(() => Activator.CreateInstance<TCollection>(), null, delegate(TCollection l)
		{
			l.Clear();
		}, null, true, 10, 10000);

		// Token: 0x0200037A RID: 890
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001E99 RID: 7833 RVA: 0x00031B22 File Offset: 0x0002FD22
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001E9A RID: 7834 RVA: 0x00002072 File Offset: 0x00000272
			public <>c()
			{
			}

			// Token: 0x06001E9B RID: 7835 RVA: 0x00031B2E File Offset: 0x0002FD2E
			internal TCollection <.cctor>b__5_0()
			{
				return Activator.CreateInstance<TCollection>();
			}

			// Token: 0x06001E9C RID: 7836 RVA: 0x00031B35 File Offset: 0x0002FD35
			internal void <.cctor>b__5_1(TCollection l)
			{
				l.Clear();
			}

			// Token: 0x04000A03 RID: 2563
			public static readonly CollectionPool<TCollection, TItem>.<>c <>9 = new CollectionPool<TCollection, TItem>.<>c();
		}
	}
}
