using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Events;

namespace UnityEngine.Rendering
{
	// Token: 0x02000059 RID: 89
	public class ObjectPool<T> where T : new()
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000EA37 File Offset: 0x0000CC37
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000EA3F File Offset: 0x0000CC3F
		public int countAll
		{
			[CompilerGenerated]
			get
			{
				return this.<countAll>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<countAll>k__BackingField = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000EA48 File Offset: 0x0000CC48
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000EA57 File Offset: 0x0000CC57
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000EA64 File Offset: 0x0000CC64
		public ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease, bool collectionCheck = true)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_CollectionCheck = collectionCheck;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000EA94 File Offset: 0x0000CC94
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = Activator.CreateInstance<T>();
				int countAll = this.countAll;
				this.countAll = countAll + 1;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public ObjectPool<T>.PooledObject Get(out T v)
		{
			return new ObjectPool<T>.PooledObject(v = this.Get(), this);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000EB0A File Offset: 0x0000CD0A
		public void Release(T element)
		{
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x040001FA RID: 506
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040001FB RID: 507
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x040001FC RID: 508
		private readonly UnityAction<T> m_ActionOnRelease;

		// Token: 0x040001FD RID: 509
		private readonly bool m_CollectionCheck = true;

		// Token: 0x040001FE RID: 510
		[CompilerGenerated]
		private int <countAll>k__BackingField;

		// Token: 0x02000140 RID: 320
		public struct PooledObject : IDisposable
		{
			// Token: 0x06000845 RID: 2117 RVA: 0x00022D12 File Offset: 0x00020F12
			internal PooledObject(T value, ObjectPool<T> pool)
			{
				this.m_ToReturn = value;
				this.m_Pool = pool;
			}

			// Token: 0x06000846 RID: 2118 RVA: 0x00022D22 File Offset: 0x00020F22
			void IDisposable.Dispose()
			{
				this.m_Pool.Release(this.m_ToReturn);
			}

			// Token: 0x04000507 RID: 1287
			private readonly T m_ToReturn;

			// Token: 0x04000508 RID: 1288
			private readonly ObjectPool<T> m_Pool;
		}
	}
}
