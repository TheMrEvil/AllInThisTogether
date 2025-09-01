using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000307 RID: 775
	internal class LinkedPool<T> where T : LinkedPoolItem<T>
	{
		// Token: 0x06001995 RID: 6549 RVA: 0x0006894B File Offset: 0x00066B4B
		public LinkedPool(Func<T> createFunc, Action<T> resetAction, int limit = 10000)
		{
			Debug.Assert(createFunc != null);
			this.m_CreateFunc = createFunc;
			Debug.Assert(resetAction != null);
			this.m_ResetAction = resetAction;
			Debug.Assert(limit > 0);
			this.m_Limit = limit;
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00068988 File Offset: 0x00066B88
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x00068990 File Offset: 0x00066B90
		public int Count
		{
			[CompilerGenerated]
			get
			{
				return this.<Count>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Count>k__BackingField = value;
			}
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00068999 File Offset: 0x00066B99
		public void Clear()
		{
			this.m_PoolFirst = default(T);
			this.Count = 0;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x000689B0 File Offset: 0x00066BB0
		public T Get()
		{
			T t = this.m_PoolFirst;
			bool flag = this.m_PoolFirst != null;
			if (flag)
			{
				int count = this.Count - 1;
				this.Count = count;
				this.m_PoolFirst = t.poolNext;
				this.m_ResetAction(t);
			}
			else
			{
				t = this.m_CreateFunc();
			}
			return t;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00068A1C File Offset: 0x00066C1C
		public void Return(T item)
		{
			bool flag = this.Count < this.m_Limit;
			if (flag)
			{
				item.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = item;
				int count = this.Count + 1;
				this.Count = count;
			}
		}

		// Token: 0x04000B14 RID: 2836
		private readonly Func<T> m_CreateFunc;

		// Token: 0x04000B15 RID: 2837
		private readonly Action<T> m_ResetAction;

		// Token: 0x04000B16 RID: 2838
		private readonly int m_Limit;

		// Token: 0x04000B17 RID: 2839
		private T m_PoolFirst;

		// Token: 0x04000B18 RID: 2840
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <Count>k__BackingField;
	}
}
