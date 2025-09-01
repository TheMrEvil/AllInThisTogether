using System;
using System.Collections.Generic;

namespace InControl
{
	// Token: 0x02000070 RID: 112
	internal class ThreadSafeQueue<T>
	{
		// Token: 0x0600054C RID: 1356 RVA: 0x00012658 File Offset: 0x00010858
		public ThreadSafeQueue()
		{
			this.sync = new object();
			this.data = new Queue<T>();
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00012676 File Offset: 0x00010876
		public ThreadSafeQueue(int capacity)
		{
			this.sync = new object();
			this.data = new Queue<T>(capacity);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00012698 File Offset: 0x00010898
		public void Enqueue(T item)
		{
			object obj = this.sync;
			lock (obj)
			{
				this.data.Enqueue(item);
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000126E0 File Offset: 0x000108E0
		public bool Dequeue(out T item)
		{
			object obj = this.sync;
			lock (obj)
			{
				if (this.data.Count > 0)
				{
					item = this.data.Dequeue();
					return true;
				}
			}
			item = default(T);
			return false;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00012748 File Offset: 0x00010948
		public T Dequeue()
		{
			object obj = this.sync;
			lock (obj)
			{
				if (this.data.Count > 0)
				{
					return this.data.Dequeue();
				}
			}
			return default(T);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000127AC File Offset: 0x000109AC
		public int Dequeue(ref IList<T> list)
		{
			object obj = this.sync;
			int result;
			lock (obj)
			{
				int count = this.data.Count;
				for (int i = 0; i < count; i++)
				{
					list.Add(this.data.Dequeue());
				}
				result = count;
			}
			return result;
		}

		// Token: 0x04000418 RID: 1048
		private object sync;

		// Token: 0x04000419 RID: 1049
		private Queue<T> data;
	}
}
