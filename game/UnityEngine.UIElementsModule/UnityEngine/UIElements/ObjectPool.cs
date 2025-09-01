using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000050 RID: 80
	internal class ObjectPool<T> where T : new()
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008B14 File Offset: 0x00006D14
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00008B2C File Offset: 0x00006D2C
		public int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
			set
			{
				this.m_MaxSize = Math.Max(0, value);
				while (this.Size() > this.m_MaxSize)
				{
					this.Get();
				}
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00008B64 File Offset: 0x00006D64
		public ObjectPool(int maxSize = 100)
		{
			this.maxSize = maxSize;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00008B84 File Offset: 0x00006D84
		public int Size()
		{
			return this.m_Stack.Count;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008BA1 File Offset: 0x00006DA1
		public void Clear()
		{
			this.m_Stack.Clear();
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public T Get()
		{
			return (this.m_Stack.Count == 0) ? Activator.CreateInstance<T>() : this.m_Stack.Pop();
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public void Release(T element)
		{
			bool flag = this.m_Stack.Count > 0 && this.m_Stack.Peek() == element;
			if (flag)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			bool flag2 = this.m_Stack.Count < this.maxSize;
			if (flag2)
			{
				this.m_Stack.Push(element);
			}
		}

		// Token: 0x040000DC RID: 220
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040000DD RID: 221
		private int m_MaxSize;
	}
}
