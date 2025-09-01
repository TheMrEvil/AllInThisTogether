using System;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000ABE RID: 2750
	internal class ArraySortHelper<TKey, TValue>
	{
		// Token: 0x0600625E RID: 25182 RVA: 0x0014924C File Offset: 0x0014744C
		public void Sort(TKey[] keys, TValue[] values, int index, int length, IComparer<TKey> comparer)
		{
			try
			{
				if (comparer == null || comparer == Comparer<TKey>.Default)
				{
					comparer = Comparer<TKey>.Default;
				}
				ArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, index, length, comparer);
			}
			catch (IndexOutOfRangeException)
			{
				IntrospectiveSortUtilities.ThrowOrIgnoreBadComparer(comparer);
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("Failed to compare two elements in the array.", innerException);
			}
		}

		// Token: 0x0600625F RID: 25183 RVA: 0x001492C0 File Offset: 0x001474C0
		private static void SwapIfGreaterWithItems(TKey[] keys, TValue[] values, IComparer<TKey> comparer, int a, int b)
		{
			if (a != b && comparer.Compare(keys[a], keys[b]) > 0)
			{
				TKey tkey = keys[a];
				keys[a] = keys[b];
				keys[b] = tkey;
				TValue tvalue = values[a];
				values[a] = values[b];
				values[b] = tvalue;
			}
		}

		// Token: 0x06006260 RID: 25184 RVA: 0x0014932C File Offset: 0x0014752C
		private static void Swap(TKey[] keys, TValue[] values, int i, int j)
		{
			if (i != j)
			{
				TKey tkey = keys[i];
				keys[i] = keys[j];
				keys[j] = tkey;
				TValue tvalue = values[i];
				values[i] = values[j];
				values[j] = tvalue;
			}
		}

		// Token: 0x06006261 RID: 25185 RVA: 0x00149379 File Offset: 0x00147579
		internal static void IntrospectiveSort(TKey[] keys, TValue[] values, int left, int length, IComparer<TKey> comparer)
		{
			if (length < 2)
			{
				return;
			}
			ArraySortHelper<TKey, TValue>.IntroSort(keys, values, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2PlusOne(length), comparer);
		}

		// Token: 0x06006262 RID: 25186 RVA: 0x00149398 File Offset: 0x00147598
		private static void IntroSort(TKey[] keys, TValue[] values, int lo, int hi, int depthLimit, IComparer<TKey> comparer)
		{
			while (hi > lo)
			{
				int num = hi - lo + 1;
				if (num <= 16)
				{
					if (num == 1)
					{
						return;
					}
					if (num == 2)
					{
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
						return;
					}
					if (num == 3)
					{
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi - 1);
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, hi - 1, hi);
						return;
					}
					ArraySortHelper<TKey, TValue>.InsertionSort(keys, values, lo, hi, comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						ArraySortHelper<TKey, TValue>.Heapsort(keys, values, lo, hi, comparer);
						return;
					}
					depthLimit--;
					int num2 = ArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys, values, lo, hi, comparer);
					ArraySortHelper<TKey, TValue>.IntroSort(keys, values, num2 + 1, hi, depthLimit, comparer);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x00149440 File Offset: 0x00147640
		private static int PickPivotAndPartition(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
		{
			int num = lo + (hi - lo) / 2;
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, num);
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, lo, hi);
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithItems(keys, values, comparer, num, hi);
			TKey tkey = keys[num];
			ArraySortHelper<TKey, TValue>.Swap(keys, values, num, hi - 1);
			int i = lo;
			int num2 = hi - 1;
			while (i < num2)
			{
				while (comparer.Compare(keys[++i], tkey) < 0)
				{
				}
				while (comparer.Compare(tkey, keys[--num2]) < 0)
				{
				}
				if (i >= num2)
				{
					break;
				}
				ArraySortHelper<TKey, TValue>.Swap(keys, values, i, num2);
			}
			ArraySortHelper<TKey, TValue>.Swap(keys, values, i, hi - 1);
			return i;
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x001494DC File Offset: 0x001476DC
		private static void Heapsort(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				ArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, num, lo, comparer);
			}
			for (int j = num; j > 1; j--)
			{
				ArraySortHelper<TKey, TValue>.Swap(keys, values, lo, lo + j - 1);
				ArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, j - 1, lo, comparer);
			}
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x00149530 File Offset: 0x00147730
		private static void DownHeap(TKey[] keys, TValue[] values, int i, int n, int lo, IComparer<TKey> comparer)
		{
			TKey tkey = keys[lo + i - 1];
			TValue tvalue = values[lo + i - 1];
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && comparer.Compare(keys[lo + num - 1], keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer.Compare(tkey, keys[lo + num - 1]) >= 0)
				{
					break;
				}
				keys[lo + i - 1] = keys[lo + num - 1];
				values[lo + i - 1] = values[lo + num - 1];
				i = num;
			}
			keys[lo + i - 1] = tkey;
			values[lo + i - 1] = tvalue;
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x001495F0 File Offset: 0x001477F0
		private static void InsertionSort(TKey[] keys, TValue[] values, int lo, int hi, IComparer<TKey> comparer)
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				TKey tkey = keys[i + 1];
				TValue tvalue = values[i + 1];
				while (num >= lo && comparer.Compare(tkey, keys[num]) < 0)
				{
					keys[num + 1] = keys[num];
					values[num + 1] = values[num];
					num--;
				}
				keys[num + 1] = tkey;
				values[num + 1] = tvalue;
			}
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06006267 RID: 25191 RVA: 0x0014966F File Offset: 0x0014786F
		public static ArraySortHelper<TKey, TValue> Default
		{
			get
			{
				return ArraySortHelper<TKey, TValue>.s_defaultArraySortHelper;
			}
		}

		// Token: 0x06006268 RID: 25192 RVA: 0x0000259F File Offset: 0x0000079F
		public ArraySortHelper()
		{
		}

		// Token: 0x06006269 RID: 25193 RVA: 0x00149676 File Offset: 0x00147876
		// Note: this type is marked as 'beforefieldinit'.
		static ArraySortHelper()
		{
		}

		// Token: 0x04003A33 RID: 14899
		private static readonly ArraySortHelper<TKey, TValue> s_defaultArraySortHelper = new ArraySortHelper<TKey, TValue>();
	}
}
