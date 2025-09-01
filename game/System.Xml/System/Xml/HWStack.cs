using System;

namespace System.Xml
{
	// Token: 0x020001DF RID: 479
	internal class HWStack : ICloneable
	{
		// Token: 0x0600130E RID: 4878 RVA: 0x00070B36 File Offset: 0x0006ED36
		internal HWStack(int GrowthRate) : this(GrowthRate, int.MaxValue)
		{
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00070B44 File Offset: 0x0006ED44
		internal HWStack(int GrowthRate, int limit)
		{
			this.growthRate = GrowthRate;
			this.used = 0;
			this.stack = new object[GrowthRate];
			this.size = GrowthRate;
			this.limit = limit;
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00070B74 File Offset: 0x0006ED74
		internal object Push()
		{
			if (this.used == this.size)
			{
				if (this.limit <= this.used)
				{
					throw new XmlException("Stack overflow.", string.Empty);
				}
				object[] destinationArray = new object[this.size + this.growthRate];
				if (this.used > 0)
				{
					Array.Copy(this.stack, 0, destinationArray, 0, this.used);
				}
				this.stack = destinationArray;
				this.size += this.growthRate;
			}
			object[] array = this.stack;
			int num = this.used;
			this.used = num + 1;
			return array[num];
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00070C0F File Offset: 0x0006EE0F
		internal object Pop()
		{
			if (0 < this.used)
			{
				this.used--;
				return this.stack[this.used];
			}
			return null;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00070C37 File Offset: 0x0006EE37
		internal object Peek()
		{
			if (this.used <= 0)
			{
				return null;
			}
			return this.stack[this.used - 1];
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00070C53 File Offset: 0x0006EE53
		internal void AddToTop(object o)
		{
			if (this.used > 0)
			{
				this.stack[this.used - 1] = o;
			}
		}

		// Token: 0x170003A7 RID: 935
		internal object this[int index]
		{
			get
			{
				if (index >= 0 && index < this.used)
				{
					return this.stack[index];
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < this.used)
				{
					this.stack[index] = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x00070CA9 File Offset: 0x0006EEA9
		internal int Length
		{
			get
			{
				return this.used;
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00070CB1 File Offset: 0x0006EEB1
		private HWStack(object[] stack, int growthRate, int used, int size)
		{
			this.stack = stack;
			this.growthRate = growthRate;
			this.used = used;
			this.size = size;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00070CD6 File Offset: 0x0006EED6
		public object Clone()
		{
			return new HWStack((object[])this.stack.Clone(), this.growthRate, this.used, this.size);
		}

		// Token: 0x040010E6 RID: 4326
		private object[] stack;

		// Token: 0x040010E7 RID: 4327
		private int growthRate;

		// Token: 0x040010E8 RID: 4328
		private int used;

		// Token: 0x040010E9 RID: 4329
		private int size;

		// Token: 0x040010EA RID: 4330
		private int limit;
	}
}
