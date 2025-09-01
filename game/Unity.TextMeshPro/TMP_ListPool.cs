using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace TMPro
{
	// Token: 0x02000043 RID: 67
	internal static class TMP_ListPool<T>
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00022AC5 File Offset: 0x00020CC5
		public static List<T> Get()
		{
			return TMP_ListPool<T>.s_ListPool.Get();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00022AD1 File Offset: 0x00020CD1
		public static void Release(List<T> toRelease)
		{
			TMP_ListPool<T>.s_ListPool.Release(toRelease);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00022ADE File Offset: 0x00020CDE
		// Note: this type is marked as 'beforefieldinit'.
		static TMP_ListPool()
		{
		}

		// Token: 0x04000273 RID: 627
		private static readonly TMP_ObjectPool<List<T>> s_ListPool = new TMP_ObjectPool<List<T>>(null, delegate(List<T> l)
		{
			l.Clear();
		});

		// Token: 0x02000097 RID: 151
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600062B RID: 1579 RVA: 0x00038A6F File Offset: 0x00036C6F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600062C RID: 1580 RVA: 0x00038A7B File Offset: 0x00036C7B
			public <>c()
			{
			}

			// Token: 0x0600062D RID: 1581 RVA: 0x00038A83 File Offset: 0x00036C83
			internal void <.cctor>b__3_0(List<T> l)
			{
				l.Clear();
			}

			// Token: 0x040005D5 RID: 1493
			public static readonly TMP_ListPool<T>.<>c <>9 = new TMP_ListPool<T>.<>c();
		}
	}
}
