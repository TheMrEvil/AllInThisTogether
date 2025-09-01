using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Pool
{
	// Token: 0x0200037E RID: 894
	public class GenericPool<T> where T : class, new()
	{
		// Token: 0x06001EA0 RID: 7840 RVA: 0x00031B5E File Offset: 0x0002FD5E
		public static T Get()
		{
			return GenericPool<T>.s_Pool.Get();
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00031B6A File Offset: 0x0002FD6A
		public static PooledObject<T> Get(out T value)
		{
			return GenericPool<T>.s_Pool.Get(out value);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00031B77 File Offset: 0x0002FD77
		public static void Release(T toRelease)
		{
			GenericPool<T>.s_Pool.Release(toRelease);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00002072 File Offset: 0x00000272
		public GenericPool()
		{
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00031B85 File Offset: 0x0002FD85
		// Note: this type is marked as 'beforefieldinit'.
		static GenericPool()
		{
		}

		// Token: 0x04000A04 RID: 2564
		internal static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => Activator.CreateInstance<T>(), null, null, null, true, 10, 10000);

		// Token: 0x0200037F RID: 895
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001EA5 RID: 7845 RVA: 0x00031BAC File Offset: 0x0002FDAC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001EA6 RID: 7846 RVA: 0x00002072 File Offset: 0x00000272
			public <>c()
			{
			}

			// Token: 0x06001EA7 RID: 7847 RVA: 0x00031B2E File Offset: 0x0002FD2E
			internal T <.cctor>b__5_0()
			{
				return Activator.CreateInstance<T>();
			}

			// Token: 0x04000A05 RID: 2565
			public static readonly GenericPool<T>.<>c <>9 = new GenericPool<T>.<>c();
		}
	}
}
