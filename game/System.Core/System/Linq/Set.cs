using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000E9 RID: 233
	internal sealed class Set<TElement>
	{
		// Token: 0x06000834 RID: 2100 RVA: 0x0001C5F8 File Offset: 0x0001A7F8
		public Set(IEqualityComparer<TElement> comparer)
		{
			this._comparer = (comparer ?? EqualityComparer<TElement>.Default);
			this._buckets = new int[7];
			this._slots = new Set<TElement>.Slot[7];
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001C628 File Offset: 0x0001A828
		public bool Add(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			for (int i = this._buckets[num % this._buckets.Length] - 1; i >= 0; i = this._slots[i]._next)
			{
				if (this._slots[i]._hashCode == num && this._comparer.Equals(this._slots[i]._value, value))
				{
					return false;
				}
			}
			if (this._count == this._slots.Length)
			{
				this.Resize();
			}
			int count = this._count;
			this._count++;
			int num2 = num % this._buckets.Length;
			this._slots[count]._hashCode = num;
			this._slots[count]._value = value;
			this._slots[count]._next = this._buckets[num2] - 1;
			this._buckets[num2] = count + 1;
			return true;
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001C720 File Offset: 0x0001A920
		public bool Remove(TElement value)
		{
			int num = this.InternalGetHashCode(value);
			int num2 = num % this._buckets.Length;
			int num3 = -1;
			for (int i = this._buckets[num2] - 1; i >= 0; i = this._slots[i]._next)
			{
				if (this._slots[i]._hashCode == num && this._comparer.Equals(this._slots[i]._value, value))
				{
					if (num3 < 0)
					{
						this._buckets[num2] = this._slots[i]._next + 1;
					}
					else
					{
						this._slots[num3]._next = this._slots[i]._next;
					}
					this._slots[i]._hashCode = -1;
					this._slots[i]._value = default(TElement);
					this._slots[i]._next = -1;
					return true;
				}
				num3 = i;
			}
			return false;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001C828 File Offset: 0x0001AA28
		private void Resize()
		{
			int num = checked(this._count * 2 + 1);
			int[] array = new int[num];
			Set<TElement>.Slot[] array2 = new Set<TElement>.Slot[num];
			Array.Copy(this._slots, 0, array2, 0, this._count);
			for (int i = 0; i < this._count; i++)
			{
				int num2 = array2[i]._hashCode % num;
				array2[i]._next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this._buckets = array;
			this._slots = array2;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001C8AC File Offset: 0x0001AAAC
		public TElement[] ToArray()
		{
			TElement[] array = new TElement[this._count];
			for (int num = 0; num != array.Length; num++)
			{
				array[num] = this._slots[num]._value;
			}
			return array;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001C8EC File Offset: 0x0001AAEC
		public List<TElement> ToList()
		{
			int count = this._count;
			List<TElement> list = new List<TElement>(count);
			for (int num = 0; num != count; num++)
			{
				list.Add(this._slots[num]._value);
			}
			return list;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001C92B File Offset: 0x0001AB2B
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001C934 File Offset: 0x0001AB34
		public void UnionWith(IEnumerable<TElement> other)
		{
			foreach (TElement value in other)
			{
				this.Add(value);
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001C980 File Offset: 0x0001AB80
		private int InternalGetHashCode(TElement value)
		{
			if (value != null)
			{
				return this._comparer.GetHashCode(value) & int.MaxValue;
			}
			return 0;
		}

		// Token: 0x040005BB RID: 1467
		private readonly IEqualityComparer<TElement> _comparer;

		// Token: 0x040005BC RID: 1468
		private int[] _buckets;

		// Token: 0x040005BD RID: 1469
		private Set<TElement>.Slot[] _slots;

		// Token: 0x040005BE RID: 1470
		private int _count;

		// Token: 0x020000EA RID: 234
		private struct Slot
		{
			// Token: 0x040005BF RID: 1471
			internal int _hashCode;

			// Token: 0x040005C0 RID: 1472
			internal int _next;

			// Token: 0x040005C1 RID: 1473
			internal TElement _value;
		}
	}
}
