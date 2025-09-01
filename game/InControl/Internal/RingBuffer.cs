using System;

namespace InControl.Internal
{
	// Token: 0x02000077 RID: 119
	public class RingBuffer<T>
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x00014FF0 File Offset: 0x000131F0
		public RingBuffer(int size)
		{
			if (size <= 0)
			{
				throw new ArgumentException("RingBuffer size must be 1 or greater.");
			}
			this.size = size + 1;
			this.data = new T[this.size];
			this.head = 0;
			this.tail = 0;
			this.sync = new object();
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00015048 File Offset: 0x00013248
		public void Enqueue(T value)
		{
			object obj = this.sync;
			lock (obj)
			{
				if (this.size > 1)
				{
					this.head = (this.head + 1) % this.size;
					if (this.head == this.tail)
					{
						this.tail = (this.tail + 1) % this.size;
					}
				}
				this.data[this.head] = value;
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x000150D8 File Offset: 0x000132D8
		public T Dequeue()
		{
			object obj = this.sync;
			T result;
			lock (obj)
			{
				if (this.size > 1 && this.tail != this.head)
				{
					this.tail = (this.tail + 1) % this.size;
				}
				result = this.data[this.tail];
			}
			return result;
		}

		// Token: 0x04000432 RID: 1074
		private readonly int size;

		// Token: 0x04000433 RID: 1075
		private readonly T[] data;

		// Token: 0x04000434 RID: 1076
		private int head;

		// Token: 0x04000435 RID: 1077
		private int tail;

		// Token: 0x04000436 RID: 1078
		private readonly object sync;
	}
}
