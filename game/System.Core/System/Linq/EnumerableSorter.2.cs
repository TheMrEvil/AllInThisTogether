using System;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x020000E4 RID: 228
	internal sealed class EnumerableSorter<TElement, TKey> : EnumerableSorter<TElement>
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x0001C17B File Offset: 0x0001A37B
		internal EnumerableSorter(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending, EnumerableSorter<TElement> next)
		{
			this._keySelector = keySelector;
			this._comparer = comparer;
			this._descending = descending;
			this._next = next;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001C1A0 File Offset: 0x0001A3A0
		internal override void ComputeKeys(TElement[] elements, int count)
		{
			this._keys = new TKey[count];
			for (int i = 0; i < count; i++)
			{
				this._keys[i] = this._keySelector(elements[i]);
			}
			EnumerableSorter<TElement> next = this._next;
			if (next == null)
			{
				return;
			}
			next.ComputeKeys(elements, count);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
		internal override int CompareAnyKeys(int index1, int index2)
		{
			int num = this._comparer.Compare(this._keys[index1], this._keys[index2]);
			if (num == 0)
			{
				if (this._next == null)
				{
					return index1 - index2;
				}
				return this._next.CompareAnyKeys(index1, index2);
			}
			else
			{
				if (this._descending == num > 0)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001C255 File Offset: 0x0001A455
		private int CompareKeys(int index1, int index2)
		{
			if (index1 != index2)
			{
				return this.CompareAnyKeys(index1, index2);
			}
			return 0;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001C265 File Offset: 0x0001A465
		protected override void QuickSort(int[] keys, int lo, int hi)
		{
			Array.Sort<int>(keys, lo, hi - lo + 1, Comparer<int>.Create(new Comparison<int>(this.CompareAnyKeys)));
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001C288 File Offset: 0x0001A488
		protected override void PartialQuickSort(int[] map, int left, int right, int minIdx, int maxIdx)
		{
			do
			{
				int num = left;
				int num2 = right;
				int index = map[num + (num2 - num >> 1)];
				do
				{
					if (num < map.Length)
					{
						if (this.CompareKeys(index, map[num]) > 0)
						{
							num++;
							continue;
						}
					}
					while (num2 >= 0 && this.CompareKeys(index, map[num2]) < 0)
					{
						num2--;
					}
					if (num > num2)
					{
						break;
					}
					if (num < num2)
					{
						int num3 = map[num];
						map[num] = map[num2];
						map[num2] = num3;
					}
					num++;
					num2--;
				}
				while (num <= num2);
				if (minIdx >= num)
				{
					left = num + 1;
				}
				else if (maxIdx <= num2)
				{
					right = num2 - 1;
				}
				if (num2 - left <= right - num)
				{
					if (left < num2)
					{
						this.PartialQuickSort(map, left, num2, minIdx, maxIdx);
					}
					left = num;
				}
				else
				{
					if (num < right)
					{
						this.PartialQuickSort(map, num, right, minIdx, maxIdx);
					}
					right = num2;
				}
			}
			while (left < right);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001C344 File Offset: 0x0001A544
		protected override int QuickSelect(int[] map, int right, int idx)
		{
			int num = 0;
			do
			{
				int num2 = num;
				int num3 = right;
				int index = map[num2 + (num3 - num2 >> 1)];
				do
				{
					if (num2 < map.Length)
					{
						if (this.CompareKeys(index, map[num2]) > 0)
						{
							num2++;
							continue;
						}
					}
					while (num3 >= 0 && this.CompareKeys(index, map[num3]) < 0)
					{
						num3--;
					}
					if (num2 > num3)
					{
						break;
					}
					if (num2 < num3)
					{
						int num4 = map[num2];
						map[num2] = map[num3];
						map[num3] = num4;
					}
					num2++;
					num3--;
				}
				while (num2 <= num3);
				if (num2 <= idx)
				{
					num = num2 + 1;
				}
				else
				{
					right = num3 - 1;
				}
				if (num3 - num <= right - num2)
				{
					if (num < num3)
					{
						right = num3;
					}
					num = num2;
				}
				else
				{
					if (num2 < right)
					{
						num = num2;
					}
					right = num3;
				}
			}
			while (num < right);
			return map[idx];
		}

		// Token: 0x040005B2 RID: 1458
		private readonly Func<TElement, TKey> _keySelector;

		// Token: 0x040005B3 RID: 1459
		private readonly IComparer<TKey> _comparer;

		// Token: 0x040005B4 RID: 1460
		private readonly bool _descending;

		// Token: 0x040005B5 RID: 1461
		private readonly EnumerableSorter<TElement> _next;

		// Token: 0x040005B6 RID: 1462
		private TKey[] _keys;
	}
}
