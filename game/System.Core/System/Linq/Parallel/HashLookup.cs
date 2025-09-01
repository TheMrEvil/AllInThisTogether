using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x020001EF RID: 495
	internal class HashLookup<TKey, TValue>
	{
		// Token: 0x06000C2A RID: 3114 RVA: 0x0002AAA3 File Offset: 0x00028CA3
		internal HashLookup() : this(null)
		{
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0002AAAC File Offset: 0x00028CAC
		internal HashLookup(IEqualityComparer<TKey> comparer)
		{
			this.comparer = comparer;
			this.buckets = new int[7];
			this.slots = new HashLookup<TKey, TValue>.Slot[7];
			this.freeList = -1;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0002AADA File Offset: 0x00028CDA
		internal bool Add(TKey key, TValue value)
		{
			return !this.Find(key, true, false, ref value);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0002AAEA File Offset: 0x00028CEA
		internal bool TryGetValue(TKey key, ref TValue value)
		{
			return this.Find(key, false, false, ref value);
		}

		// Token: 0x1700016B RID: 363
		internal TValue this[TKey key]
		{
			set
			{
				TValue tvalue = value;
				this.Find(key, false, true, ref tvalue);
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0002AB13 File Offset: 0x00028D13
		private int GetKeyHashCode(TKey key)
		{
			return int.MaxValue & ((this.comparer == null) ? ((key == null) ? 0 : key.GetHashCode()) : this.comparer.GetHashCode(key));
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0002AB4C File Offset: 0x00028D4C
		private bool AreKeysEqual(TKey key1, TKey key2)
		{
			if (this.comparer != null)
			{
				return this.comparer.Equals(key1, key2);
			}
			return (key1 == null && key2 == null) || (key1 != null && key1.Equals(key2));
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x0002ABA0 File Offset: 0x00028DA0
		private bool Find(TKey key, bool add, bool set, ref TValue value)
		{
			int keyHashCode = this.GetKeyHashCode(key);
			int i = this.buckets[keyHashCode % this.buckets.Length] - 1;
			while (i >= 0)
			{
				if (this.slots[i].hashCode == keyHashCode && this.AreKeysEqual(this.slots[i].key, key))
				{
					if (set)
					{
						this.slots[i].value = value;
						return true;
					}
					value = this.slots[i].value;
					return true;
				}
				else
				{
					i = this.slots[i].next;
				}
			}
			if (add)
			{
				int num;
				if (this.freeList >= 0)
				{
					num = this.freeList;
					this.freeList = this.slots[num].next;
				}
				else
				{
					if (this.count == this.slots.Length)
					{
						this.Resize();
					}
					num = this.count;
					this.count++;
				}
				int num2 = keyHashCode % this.buckets.Length;
				this.slots[num].hashCode = keyHashCode;
				this.slots[num].key = key;
				this.slots[num].value = value;
				this.slots[num].next = this.buckets[num2] - 1;
				this.buckets[num2] = num + 1;
			}
			return false;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0002AD10 File Offset: 0x00028F10
		private void Resize()
		{
			int num = checked(this.count * 2 + 1);
			int[] array = new int[num];
			HashLookup<TKey, TValue>.Slot[] array2 = new HashLookup<TKey, TValue>.Slot[num];
			Array.Copy(this.slots, 0, array2, 0, this.count);
			for (int i = 0; i < this.count; i++)
			{
				int num2 = array2[i].hashCode % num;
				array2[i].next = array[num2] - 1;
				array[num2] = i + 1;
			}
			this.buckets = array;
			this.slots = array2;
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002AD92 File Offset: 0x00028F92
		internal int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x1700016D RID: 365
		internal KeyValuePair<TKey, TValue> this[int index]
		{
			get
			{
				return new KeyValuePair<TKey, TValue>(this.slots[index].key, this.slots[index].value);
			}
		}

		// Token: 0x04000898 RID: 2200
		private int[] buckets;

		// Token: 0x04000899 RID: 2201
		private HashLookup<TKey, TValue>.Slot[] slots;

		// Token: 0x0400089A RID: 2202
		private int count;

		// Token: 0x0400089B RID: 2203
		private int freeList;

		// Token: 0x0400089C RID: 2204
		private IEqualityComparer<TKey> comparer;

		// Token: 0x0400089D RID: 2205
		private const int HashCodeMask = 2147483647;

		// Token: 0x020001F0 RID: 496
		internal struct Slot
		{
			// Token: 0x0400089E RID: 2206
			internal int hashCode;

			// Token: 0x0400089F RID: 2207
			internal int next;

			// Token: 0x040008A0 RID: 2208
			internal TKey key;

			// Token: 0x040008A1 RID: 2209
			internal TValue value;
		}
	}
}
