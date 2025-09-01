using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000039 RID: 57
	public class Pool<T> where T : class
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0001834E File Offset: 0x0001654E
		public Pool(Func<T> createFunction, Action<T> resetFunction, int poolCapacity)
		{
			this.createFunction = createFunction;
			this.resetFunction = resetFunction;
			this.pool = new Queue<T>();
			this.CreatePoolItems(poolCapacity);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00018379 File Offset: 0x00016579
		public Pool(Func<T> createFunction, int poolCapacity) : this(createFunction, null, poolCapacity)
		{
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00018388 File Offset: 0x00016588
		public int Count
		{
			get
			{
				Queue<T> obj = this.pool;
				int count;
				lock (obj)
				{
					count = this.pool.Count;
				}
				return count;
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000183D4 File Offset: 0x000165D4
		private void CreatePoolItems(int numItems)
		{
			for (int i = 0; i < numItems; i++)
			{
				T item = this.createFunction();
				this.pool.Enqueue(item);
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00018410 File Offset: 0x00016610
		[Obsolete("Use Release() rather than Push()")]
		public void Push(T item)
		{
			bool flag = item == null;
			if (flag)
			{
				throw new ArgumentNullException("Pushing null as item is not allowed.");
			}
			bool flag2 = this.resetFunction != null;
			if (flag2)
			{
				this.resetFunction(item);
			}
			Queue<T> obj = this.pool;
			lock (obj)
			{
				this.pool.Enqueue(item);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00018494 File Offset: 0x00016694
		public void Release(T item)
		{
			bool flag = item == null;
			if (flag)
			{
				throw new ArgumentNullException("Pushing null as item is not allowed.");
			}
			bool flag2 = this.resetFunction != null;
			if (flag2)
			{
				this.resetFunction(item);
			}
			Queue<T> obj = this.pool;
			lock (obj)
			{
				this.pool.Enqueue(item);
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00018518 File Offset: 0x00016718
		[Obsolete("Use Acquire() rather than Pop()")]
		public T Pop()
		{
			Queue<T> obj = this.pool;
			T result;
			lock (obj)
			{
				bool flag2 = this.pool.Count == 0;
				if (flag2)
				{
					return this.createFunction();
				}
				result = this.pool.Dequeue();
			}
			return result;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001858C File Offset: 0x0001678C
		public T Acquire()
		{
			Queue<T> obj = this.pool;
			T result;
			lock (obj)
			{
				bool flag2 = this.pool.Count == 0;
				if (flag2)
				{
					return this.createFunction();
				}
				result = this.pool.Dequeue();
			}
			return result;
		}

		// Token: 0x040001AC RID: 428
		private readonly Func<T> createFunction;

		// Token: 0x040001AD RID: 429
		private readonly Queue<T> pool;

		// Token: 0x040001AE RID: 430
		private readonly Action<T> resetFunction;
	}
}
