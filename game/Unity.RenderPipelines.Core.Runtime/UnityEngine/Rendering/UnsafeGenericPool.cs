using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005B RID: 91
	public static class UnsafeGenericPool<T> where T : new()
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x0000EB61 File Offset: 0x0000CD61
		public static T Get()
		{
			return UnsafeGenericPool<T>.s_Pool.Get();
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000EB6D File Offset: 0x0000CD6D
		public static ObjectPool<T>.PooledObject Get(out T value)
		{
			return UnsafeGenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000EB7A File Offset: 0x0000CD7A
		public static void Release(T toRelease)
		{
			UnsafeGenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000EB87 File Offset: 0x0000CD87
		// Note: this type is marked as 'beforefieldinit'.
		static UnsafeGenericPool()
		{
		}

		// Token: 0x04000200 RID: 512
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(null, null, false);
	}
}
