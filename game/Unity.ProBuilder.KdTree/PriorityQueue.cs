using System;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x0200000D RID: 13
	internal class PriorityQueue<TItem, TPriority> : IPriorityQueue<TItem, TPriority>
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002DAA File Offset: 0x00000FAA
		public PriorityQueue(int capacity, ITypeMath<TPriority> priorityMath)
		{
			if (capacity <= 0)
			{
				throw new ArgumentException("Capacity must be greater than zero");
			}
			this.capacity = capacity;
			this.queue = new ItemPriority<TItem, TPriority>[capacity];
			this.priorityMath = priorityMath;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002DDB File Offset: 0x00000FDB
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DE4 File Offset: 0x00000FE4
		private void ExpandCapacity()
		{
			this.capacity *= 2;
			ItemPriority<TItem, TPriority>[] destinationArray = new ItemPriority<TItem, TPriority>[this.capacity];
			Array.Copy(this.queue, destinationArray, this.queue.Length);
			this.queue = destinationArray;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E28 File Offset: 0x00001028
		public void Enqueue(TItem item, TPriority priority)
		{
			int num = this.count + 1;
			this.count = num;
			if (num > this.capacity)
			{
				this.ExpandCapacity();
			}
			int num2 = this.count - 1;
			this.queue[num2] = new ItemPriority<TItem, TPriority>
			{
				Item = item,
				Priority = priority
			};
			this.ReorderItem(num2, -1);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E8C File Offset: 0x0000108C
		public TItem Dequeue()
		{
			TItem item = this.queue[0].Item;
			this.queue[0].Item = default(TItem);
			this.queue[0].Priority = this.priorityMath.MinValue;
			this.ReorderItem(0, 1);
			this.count--;
			return item;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EF4 File Offset: 0x000010F4
		private void ReorderItem(int index, int direction)
		{
			if (direction != -1 && direction != 1)
			{
				throw new ArgumentException("Invalid Direction");
			}
			ItemPriority<TItem, TPriority> itemPriority = this.queue[index];
			int num = index + direction;
			while (num >= 0 && num < this.count)
			{
				ItemPriority<TItem, TPriority> itemPriority2 = this.queue[num];
				int num2 = this.priorityMath.Compare(itemPriority.Priority, itemPriority2.Priority);
				if ((direction != -1 || num2 <= 0) && (direction != 1 || num2 >= 0))
				{
					break;
				}
				this.queue[index] = itemPriority2;
				this.queue[num] = itemPriority;
				index += direction;
				num += direction;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F8C File Offset: 0x0000118C
		public TItem GetHighest()
		{
			if (this.count == 0)
			{
				throw new Exception("Queue is empty");
			}
			return this.queue[0].Item;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FB2 File Offset: 0x000011B2
		public TPriority GetHighestPriority()
		{
			if (this.count == 0)
			{
				throw new Exception("Queue is empty");
			}
			return this.queue[0].Priority;
		}

		// Token: 0x04000017 RID: 23
		private ITypeMath<TPriority> priorityMath;

		// Token: 0x04000018 RID: 24
		private ItemPriority<TItem, TPriority>[] queue;

		// Token: 0x04000019 RID: 25
		private int capacity;

		// Token: 0x0400001A RID: 26
		private int count;
	}
}
