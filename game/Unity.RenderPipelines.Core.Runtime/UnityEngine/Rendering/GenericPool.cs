using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005A RID: 90
	public static class GenericPool<T> where T : new()
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000EB2C File Offset: 0x0000CD2C
		public static T Get()
		{
			return GenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000EB38 File Offset: 0x0000CD38
		public static ObjectPool<T>.PooledObject Get(out T value)
		{
			return GenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000EB45 File Offset: 0x0000CD45
		public static void Release(T toRelease)
		{
			GenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000EB52 File Offset: 0x0000CD52
		// Note: this type is marked as 'beforefieldinit'.
		static GenericPool()
		{
		}

		// Token: 0x040001FF RID: 511
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(null, null, true);
	}
}
