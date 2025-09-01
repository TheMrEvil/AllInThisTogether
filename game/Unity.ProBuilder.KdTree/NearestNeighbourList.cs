using System;

namespace UnityEngine.ProBuilder.KdTree
{
	// Token: 0x0200000B RID: 11
	internal class NearestNeighbourList<TItem, TDistance> : INearestNeighbourList<TItem, TDistance>
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002CAF File Offset: 0x00000EAF
		public NearestNeighbourList(int maxCapacity, ITypeMath<TDistance> distanceMath)
		{
			this.maxCapacity = maxCapacity;
			this.distanceMath = distanceMath;
			this.queue = new PriorityQueue<TItem, TDistance>(maxCapacity, distanceMath);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public int MaxCapacity
		{
			get
			{
				return this.maxCapacity;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002CDA File Offset: 0x00000EDA
		public int Count
		{
			get
			{
				return this.queue.Count;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public bool Add(TItem item, TDistance distance)
		{
			if (this.queue.Count < this.maxCapacity)
			{
				this.queue.Enqueue(item, distance);
				return true;
			}
			if (this.distanceMath.Compare(distance, this.queue.GetHighestPriority()) < 0)
			{
				this.queue.Dequeue();
				this.queue.Enqueue(item, distance);
				return true;
			}
			return false;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D4D File Offset: 0x00000F4D
		public TItem GetFurtherest()
		{
			if (this.Count == 0)
			{
				throw new Exception("List is empty");
			}
			return this.queue.GetHighest();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D6D File Offset: 0x00000F6D
		public TDistance GetFurtherestDistance()
		{
			if (this.Count == 0)
			{
				throw new Exception("List is empty");
			}
			return this.queue.GetHighestPriority();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D8D File Offset: 0x00000F8D
		public TItem RemoveFurtherest()
		{
			return this.queue.Dequeue();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002D9A File Offset: 0x00000F9A
		public bool IsCapacityReached
		{
			get
			{
				return this.Count == this.MaxCapacity;
			}
		}

		// Token: 0x04000012 RID: 18
		private PriorityQueue<TItem, TDistance> queue;

		// Token: 0x04000013 RID: 19
		private ITypeMath<TDistance> distanceMath;

		// Token: 0x04000014 RID: 20
		private int maxCapacity;
	}
}
