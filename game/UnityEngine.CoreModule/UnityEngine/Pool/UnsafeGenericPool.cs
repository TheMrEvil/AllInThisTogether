using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Pool
{
	// Token: 0x02000385 RID: 901
	public static class UnsafeGenericPool<T> where T : class, new()
	{
		// Token: 0x06001EC2 RID: 7874 RVA: 0x000320A7 File Offset: 0x000302A7
		public static T Get()
		{
			return UnsafeGenericPool<T>.s_Pool.Get();
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x000320B3 File Offset: 0x000302B3
		public static PooledObject<T> Get(out T value)
		{
			return UnsafeGenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000320C0 File Offset: 0x000302C0
		public static void Release(T toRelease)
		{
			UnsafeGenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000320CE File Offset: 0x000302CE
		// Note: this type is marked as 'beforefieldinit'.
		static UnsafeGenericPool()
		{
		}

		// Token: 0x04000A1B RID: 2587
		internal static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => Activator.CreateInstance<T>(), null, null, null, false, 10, 10000);

		// Token: 0x02000386 RID: 902
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001EC6 RID: 7878 RVA: 0x000320F5 File Offset: 0x000302F5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001EC7 RID: 7879 RVA: 0x00002072 File Offset: 0x00000272
			public <>c()
			{
			}

			// Token: 0x06001EC8 RID: 7880 RVA: 0x00031B2E File Offset: 0x0002FD2E
			internal T <.cctor>b__4_0()
			{
				return Activator.CreateInstance<T>();
			}

			// Token: 0x04000A1C RID: 2588
			public static readonly UnsafeGenericPool<T>.<>c <>9 = new UnsafeGenericPool<T>.<>c();
		}
	}
}
