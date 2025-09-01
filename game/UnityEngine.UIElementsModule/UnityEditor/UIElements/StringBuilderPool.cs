using System;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Pool;

namespace UnityEditor.UIElements
{
	// Token: 0x02000004 RID: 4
	internal static class StringBuilderPool
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002059 File Offset: 0x00000259
		public static StringBuilder Get()
		{
			return StringBuilderPool.s_Pool.Get();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002065 File Offset: 0x00000265
		public static PooledObject<StringBuilder> Get(out StringBuilder value)
		{
			return StringBuilderPool.s_Pool.Get(out value);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		public static void Release(StringBuilder toRelease)
		{
			StringBuilderPool.s_Pool.Release(toRelease);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002080 File Offset: 0x00000280
		// Note: this type is marked as 'beforefieldinit'.
		static StringBuilderPool()
		{
		}

		// Token: 0x04000001 RID: 1
		internal static readonly ObjectPool<StringBuilder> s_Pool = new ObjectPool<StringBuilder>(() => new StringBuilder(), null, delegate(StringBuilder sb)
		{
			sb.Length = 0;
		}, null, true, 10, 10000);

		// Token: 0x02000005 RID: 5
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000007 RID: 7 RVA: 0x000020B6 File Offset: 0x000002B6
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000008 RID: 8 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06000009 RID: 9 RVA: 0x000020CB File Offset: 0x000002CB
			internal StringBuilder <.cctor>b__4_0()
			{
				return new StringBuilder();
			}

			// Token: 0x0600000A RID: 10 RVA: 0x000020D2 File Offset: 0x000002D2
			internal void <.cctor>b__4_1(StringBuilder sb)
			{
				sb.Length = 0;
			}

			// Token: 0x04000002 RID: 2
			public static readonly StringBuilderPool.<>c <>9 = new StringBuilderPool.<>c();
		}
	}
}
