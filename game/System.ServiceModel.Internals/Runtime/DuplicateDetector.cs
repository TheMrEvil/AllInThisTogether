using System;
using System.Collections.Generic;

namespace System.Runtime
{
	// Token: 0x02000017 RID: 23
	internal class DuplicateDetector<T> where T : class
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003346 File Offset: 0x00001546
		public DuplicateDetector(int capacity)
		{
			this.capacity = capacity;
			this.items = new Dictionary<T, LinkedListNode<T>>();
			this.fifoList = new LinkedList<T>();
			this.thisLock = new object();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003378 File Offset: 0x00001578
		public bool AddIfNotDuplicate(T value)
		{
			bool result = false;
			object obj = this.thisLock;
			lock (obj)
			{
				if (!this.items.ContainsKey(value))
				{
					this.Add(value);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000033CC File Offset: 0x000015CC
		private void Add(T value)
		{
			if (this.items.Count == this.capacity)
			{
				LinkedListNode<T> last = this.fifoList.Last;
				this.items.Remove(last.Value);
				this.fifoList.Remove(last);
			}
			this.items.Add(value, this.fifoList.AddFirst(value));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003430 File Offset: 0x00001630
		public bool Remove(T value)
		{
			bool result = false;
			object obj = this.thisLock;
			lock (obj)
			{
				LinkedListNode<T> node;
				if (this.items.TryGetValue(value, out node))
				{
					this.items.Remove(value);
					this.fifoList.Remove(node);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003498 File Offset: 0x00001698
		public void Clear()
		{
			object obj = this.thisLock;
			lock (obj)
			{
				this.fifoList.Clear();
				this.items.Clear();
			}
		}

		// Token: 0x04000092 RID: 146
		private LinkedList<T> fifoList;

		// Token: 0x04000093 RID: 147
		private Dictionary<T, LinkedListNode<T>> items;

		// Token: 0x04000094 RID: 148
		private int capacity;

		// Token: 0x04000095 RID: 149
		private object thisLock;
	}
}
