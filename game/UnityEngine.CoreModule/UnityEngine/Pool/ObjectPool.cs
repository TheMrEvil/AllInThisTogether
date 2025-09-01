using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Pool
{
	// Token: 0x02000383 RID: 899
	public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
	{
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x00031E09 File Offset: 0x00030009
		// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x00031E11 File Offset: 0x00030011
		public int CountAll
		{
			[CompilerGenerated]
			get
			{
				return this.<CountAll>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<CountAll>k__BackingField = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x00031E1C File Offset: 0x0003001C
		public int CountActive
		{
			get
			{
				return this.CountAll - this.CountInactive;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00031E3C File Offset: 0x0003003C
		public int CountInactive
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00031E5C File Offset: 0x0003005C
		public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
		{
			bool flag = createFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("createFunc");
			}
			bool flag2 = maxSize <= 0;
			if (flag2)
			{
				throw new ArgumentException("Max Size must be greater than 0", "maxSize");
			}
			this.m_List = new List<T>(defaultCapacity);
			this.m_CreateFunc = createFunc;
			this.m_MaxSize = maxSize;
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_ActionOnDestroy = actionOnDestroy;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x00031EDC File Offset: 0x000300DC
		public T Get()
		{
			bool flag = this.m_List.Count == 0;
			T t;
			if (flag)
			{
				t = this.m_CreateFunc();
				int countAll = this.CountAll;
				this.CountAll = countAll + 1;
			}
			else
			{
				int index = this.m_List.Count - 1;
				t = this.m_List[index];
				this.m_List.RemoveAt(index);
			}
			Action<T> actionOnGet = this.m_ActionOnGet;
			if (actionOnGet != null)
			{
				actionOnGet(t);
			}
			return t;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00031F64 File Offset: 0x00030164
		public PooledObject<T> Get(out T v)
		{
			return new PooledObject<T>(v = this.Get(), this);
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00031F88 File Offset: 0x00030188
		public void Release(T element)
		{
			Action<T> actionOnRelease = this.m_ActionOnRelease;
			if (actionOnRelease != null)
			{
				actionOnRelease(element);
			}
			bool flag = this.CountInactive < this.m_MaxSize;
			if (flag)
			{
				this.m_List.Add(element);
			}
			else
			{
				int countAll = this.CountAll;
				this.CountAll = countAll - 1;
				Action<T> actionOnDestroy = this.m_ActionOnDestroy;
				if (actionOnDestroy != null)
				{
					actionOnDestroy(element);
				}
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00031FF4 File Offset: 0x000301F4
		public void Clear()
		{
			bool flag = this.m_ActionOnDestroy != null;
			if (flag)
			{
				foreach (T obj in this.m_List)
				{
					this.m_ActionOnDestroy(obj);
				}
			}
			this.m_List.Clear();
			this.CountAll = 0;
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00032078 File Offset: 0x00030278
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x04000A11 RID: 2577
		internal readonly List<T> m_List;

		// Token: 0x04000A12 RID: 2578
		private readonly Func<T> m_CreateFunc;

		// Token: 0x04000A13 RID: 2579
		private readonly Action<T> m_ActionOnGet;

		// Token: 0x04000A14 RID: 2580
		private readonly Action<T> m_ActionOnRelease;

		// Token: 0x04000A15 RID: 2581
		private readonly Action<T> m_ActionOnDestroy;

		// Token: 0x04000A16 RID: 2582
		private readonly int m_MaxSize;

		// Token: 0x04000A17 RID: 2583
		internal bool m_CollectionCheck;

		// Token: 0x04000A18 RID: 2584
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <CountAll>k__BackingField;
	}
}
