using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Pool
{
	// Token: 0x02000381 RID: 897
	public class LinkedPool<T> : IDisposable, IObjectPool<T> where T : class
	{
		// Token: 0x06001EAD RID: 7853 RVA: 0x00031BB8 File Offset: 0x0002FDB8
		public LinkedPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int maxSize = 10000)
		{
			bool flag = createFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("createFunc");
			}
			bool flag2 = maxSize <= 0;
			if (flag2)
			{
				throw new ArgumentException("maxSize", "Max size must be greater than 0");
			}
			this.m_CreateFunc = createFunc;
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_ActionOnDestroy = actionOnDestroy;
			this.m_Limit = maxSize;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001EAE RID: 7854 RVA: 0x00031C29 File Offset: 0x0002FE29
		// (set) Token: 0x06001EAF RID: 7855 RVA: 0x00031C31 File Offset: 0x0002FE31
		public int CountInactive
		{
			[CompilerGenerated]
			get
			{
				return this.<CountInactive>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CountInactive>k__BackingField = value;
			}
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00031C3C File Offset: 0x0002FE3C
		public T Get()
		{
			T t = default(T);
			bool flag = this.m_PoolFirst == null;
			if (flag)
			{
				t = this.m_CreateFunc();
			}
			else
			{
				LinkedPool<T>.LinkedPoolItem poolFirst = this.m_PoolFirst;
				t = poolFirst.value;
				this.m_PoolFirst = poolFirst.poolNext;
				poolFirst.poolNext = this.m_NextAvailableListItem;
				this.m_NextAvailableListItem = poolFirst;
				this.m_NextAvailableListItem.value = default(T);
				int countInactive = this.CountInactive - 1;
				this.CountInactive = countInactive;
			}
			Action<T> actionOnGet = this.m_ActionOnGet;
			if (actionOnGet != null)
			{
				actionOnGet(t);
			}
			return t;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x00031CDC File Offset: 0x0002FEDC
		public PooledObject<T> Get(out T v)
		{
			return new PooledObject<T>(v = this.Get(), this);
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x00031D00 File Offset: 0x0002FF00
		public void Release(T item)
		{
			Action<T> actionOnRelease = this.m_ActionOnRelease;
			if (actionOnRelease != null)
			{
				actionOnRelease(item);
			}
			bool flag = this.CountInactive < this.m_Limit;
			if (flag)
			{
				LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_NextAvailableListItem;
				bool flag2 = linkedPoolItem == null;
				if (flag2)
				{
					linkedPoolItem = new LinkedPool<T>.LinkedPoolItem();
				}
				else
				{
					this.m_NextAvailableListItem = linkedPoolItem.poolNext;
				}
				linkedPoolItem.value = item;
				linkedPoolItem.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = linkedPoolItem;
				int countInactive = this.CountInactive + 1;
				this.CountInactive = countInactive;
			}
			else
			{
				Action<T> actionOnDestroy = this.m_ActionOnDestroy;
				if (actionOnDestroy != null)
				{
					actionOnDestroy(item);
				}
			}
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00031DA0 File Offset: 0x0002FFA0
		public void Clear()
		{
			bool flag = this.m_ActionOnDestroy != null;
			if (flag)
			{
				for (LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_PoolFirst; linkedPoolItem != null; linkedPoolItem = linkedPoolItem.poolNext)
				{
					this.m_ActionOnDestroy(linkedPoolItem.value);
				}
			}
			this.m_PoolFirst = null;
			this.m_NextAvailableListItem = null;
			this.CountInactive = 0;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00031DFF File Offset: 0x0002FFFF
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x04000A06 RID: 2566
		private readonly Func<T> m_CreateFunc;

		// Token: 0x04000A07 RID: 2567
		private readonly Action<T> m_ActionOnGet;

		// Token: 0x04000A08 RID: 2568
		private readonly Action<T> m_ActionOnRelease;

		// Token: 0x04000A09 RID: 2569
		private readonly Action<T> m_ActionOnDestroy;

		// Token: 0x04000A0A RID: 2570
		private readonly int m_Limit;

		// Token: 0x04000A0B RID: 2571
		internal LinkedPool<T>.LinkedPoolItem m_PoolFirst;

		// Token: 0x04000A0C RID: 2572
		internal LinkedPool<T>.LinkedPoolItem m_NextAvailableListItem;

		// Token: 0x04000A0D RID: 2573
		private bool m_CollectionCheck;

		// Token: 0x04000A0E RID: 2574
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <CountInactive>k__BackingField;

		// Token: 0x02000382 RID: 898
		internal class LinkedPoolItem
		{
			// Token: 0x06001EB5 RID: 7861 RVA: 0x00002072 File Offset: 0x00000272
			public LinkedPoolItem()
			{
			}

			// Token: 0x04000A0F RID: 2575
			internal LinkedPool<T>.LinkedPoolItem poolNext;

			// Token: 0x04000A10 RID: 2576
			internal T value;
		}
	}
}
