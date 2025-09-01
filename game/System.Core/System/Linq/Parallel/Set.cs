using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020000FF RID: 255
	internal class Set<TElement>
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x0001D40E File Offset: 0x0001B60E
		public Set(IEqualityComparer<TElement> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<TElement>.Default;
			}
			this._comparer = comparer;
			this._buckets = new int[7];
			this._slots = new Set<TElement>.Slot[7];
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001D43F File Offset: 0x0001B63F
		public bool Add(TElement value)
		{
			return !this.Find(value, true);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001D44C File Offset: 0x0001B64C
		public bool Contains(TElement value)
		{
			return this.Find(value, false);
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001D458 File Offset: 0x0001B658
		public bool Remove(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			int num3 = -1;
			for (int i = this._buckets[num2] - 1; i >= 0; i = this._slots[i].next)
			{
				if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, value))
				{
					if (num3 < 0)
					{
						this._buckets[num2] = this._slots[i].next + 1;
					}
					else
					{
						this._slots[num3].next = this._slots[i].next;
					}
					this._slots[i].hashCode = -1;
					this._slots[i].value = default(TElement);
					this._slots[i].next = -1;
					return true;
				}
				num3 = i;
			}
			return false;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001D560 File Offset: 0x0001B760
		private bool Find(TElement value, bool add)
		{
			int num = this.InternalGetHashCode(value);
			for (int i = this._buckets[num % this._buckets.Length] - 1; i >= 0; i = this._slots[i].next)
			{
				if (this._slots[i].hashCode == num && this._comparer.Equals(this._slots[i].value, value))
				{
					return true;
				}
			}
			if (add)
			{
				if (this._count == this._slots.Length)
				{
					this.Resize();
				}
				int count = this._count;
				this._count++;
				int num2 = num % this._buckets.Length;
				this._slots[count].hashCode = num;
				this._slots[count].value = value;
				this._slots[count].next = this._buckets[num2] - 1;
				this._buckets[num2] = count + 1;
			}
			return false;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001D660 File Offset: 0x0001B860
		private void Resize()
		{
			int num = checked(this._count * 2 + 1);
			int[] array = new int[num];
			Set<TElement>.Slot[] array2 = new Set<TElement>.Slot[num];
			Array.Copy(this._slots, 0, array2, 0, this._count);
			for (int i = 0; i < this._count; i++)
			{
				int num2 = array2[i].hashCode % num;
				array2[i].next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this._buckets = array;
			this._slots = array2;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001D6E2 File Offset: 0x0001B8E2
		internal int InternalGetHashCode(TElement value)
		{
			if (value != null)
			{
				return this._comparer.GetHashCode(value) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x040005EF RID: 1519
		private int[] _buckets;

		// Token: 0x040005F0 RID: 1520
		private Set<TElement>.Slot[] _slots;

		// Token: 0x040005F1 RID: 1521
		private int _count;

		// Token: 0x040005F2 RID: 1522
		private readonly IEqualityComparer<TElement> _comparer;

		// Token: 0x040005F3 RID: 1523
		private const int InitialSize = 7;

		// Token: 0x040005F4 RID: 1524
		private const int HashCodeMask = 2147483647;

		// Token: 0x02000100 RID: 256
		internal struct Slot
		{
			// Token: 0x040005F5 RID: 1525
			internal int hashCode;

			// Token: 0x040005F6 RID: 1526
			internal int next;

			// Token: 0x040005F7 RID: 1527
			internal TElement value;
		}
	}
}
