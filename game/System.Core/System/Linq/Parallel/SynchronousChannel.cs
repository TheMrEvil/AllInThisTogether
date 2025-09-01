using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000F0 RID: 240
	internal sealed class SynchronousChannel<T>
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x00002162 File Offset: 0x00000362
		internal SynchronousChannel()
		{
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001CFEC File Offset: 0x0001B1EC
		internal void Init()
		{
			this._queue = new Queue<T>();
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001CFF9 File Offset: 0x0001B1F9
		internal void Enqueue(T item)
		{
			this._queue.Enqueue(item);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001D007 File Offset: 0x0001B207
		internal T Dequeue()
		{
			return this._queue.Dequeue();
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00003A59 File Offset: 0x00001C59
		internal void SetDone()
		{
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001D014 File Offset: 0x0001B214
		internal void CopyTo(T[] array, int arrayIndex)
		{
			this._queue.CopyTo(array, arrayIndex);
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001D023 File Offset: 0x0001B223
		internal int Count
		{
			get
			{
				return this._queue.Count;
			}
		}

		// Token: 0x040005D7 RID: 1495
		private Queue<T> _queue;
	}
}
