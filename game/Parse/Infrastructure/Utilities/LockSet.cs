using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000058 RID: 88
	public class LockSet
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x0000D8B8 File Offset: 0x0000BAB8
		public LockSet(IEnumerable<object> mutexes)
		{
			this.mutexes = (from mutex in mutexes
			orderby LockSet.GetStableId(mutex)
			select mutex).ToList<object>();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		public void Enter()
		{
			foreach (object obj in this.mutexes)
			{
				Monitor.Enter(obj);
			}
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000D93C File Offset: 0x0000BB3C
		public void Exit()
		{
			foreach (object obj in this.mutexes)
			{
				Monitor.Exit(obj);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000D988 File Offset: 0x0000BB88
		private static IComparable GetStableId(object mutex)
		{
			ConditionalWeakTable<object, IComparable> obj = LockSet.stableIds;
			IComparable value;
			lock (obj)
			{
				value = LockSet.stableIds.GetValue(mutex, delegate(object k)
				{
					long num = LockSet.nextStableId;
					LockSet.nextStableId = num + 1L;
					return num;
				});
			}
			return value;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		// Note: this type is marked as 'beforefieldinit'.
		static LockSet()
		{
		}

		// Token: 0x040000D8 RID: 216
		private static readonly ConditionalWeakTable<object, IComparable> stableIds = new ConditionalWeakTable<object, IComparable>();

		// Token: 0x040000D9 RID: 217
		private static long nextStableId = 0L;

		// Token: 0x040000DA RID: 218
		private readonly IEnumerable<object> mutexes;

		// Token: 0x02000129 RID: 297
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600078D RID: 1933 RVA: 0x00017001 File Offset: 0x00015201
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600078E RID: 1934 RVA: 0x0001700D File Offset: 0x0001520D
			public <>c()
			{
			}

			// Token: 0x0600078F RID: 1935 RVA: 0x00017015 File Offset: 0x00015215
			internal IComparable <.ctor>b__3_0(object mutex)
			{
				return LockSet.GetStableId(mutex);
			}

			// Token: 0x06000790 RID: 1936 RVA: 0x0001701D File Offset: 0x0001521D
			internal IComparable <GetStableId>b__6_0(object k)
			{
				long nextStableId = LockSet.nextStableId;
				LockSet.nextStableId = nextStableId + 1L;
				return nextStableId;
			}

			// Token: 0x040002B2 RID: 690
			public static readonly LockSet.<>c <>9 = new LockSet.<>c();

			// Token: 0x040002B3 RID: 691
			public static Func<object, IComparable> <>9__3_0;

			// Token: 0x040002B4 RID: 692
			public static ConditionalWeakTable<object, IComparable>.CreateValueCallback <>9__6_0;
		}
	}
}
