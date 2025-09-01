using System;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000ABD RID: 2749
	internal class GenericArraySortHelper<T> where T : IComparable<T>
	{
		// Token: 0x06006252 RID: 25170 RVA: 0x00148D8C File Offset: 0x00146F8C
		public void Sort(T[] keys, int index, int length, IComparer<T> comparer)
		{
			try
			{
				if (comparer == null || comparer == Comparer<T>.Default)
				{
					GenericArraySortHelper<T>.IntrospectiveSort(keys, index, length);
				}
				else
				{
					ArraySortHelper<T>.IntrospectiveSort(keys, index, length, new Comparison<T>(comparer.Compare));
				}
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

		// Token: 0x06006253 RID: 25171 RVA: 0x00148E0C File Offset: 0x0014700C
		public int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int result;
			try
			{
				if (comparer == null || comparer == Comparer<T>.Default)
				{
					result = GenericArraySortHelper<T>.BinarySearch(array, index, length, value);
				}
				else
				{
					result = ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
				}
			}
			catch (ThreadAbortException)
			{
				throw;
			}
			catch (Exception innerException)
			{
				throw new InvalidOperationException("Failed to compare two elements in the array.", innerException);
			}
			return result;
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x00148E70 File Offset: 0x00147070
		private static int BinarySearch(T[] array, int index, int length, T value)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3;
				if (array[num2] == null)
				{
					num3 = ((value == null) ? 0 : -1);
				}
				else
				{
					num3 = array[num2].CompareTo(value);
				}
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x00148EDC File Offset: 0x001470DC
		private static void SwapIfGreaterWithItems(T[] keys, int a, int b)
		{
			if (a != b && keys[a] != null && keys[a].CompareTo(keys[b]) > 0)
			{
				T t = keys[a];
				keys[a] = keys[b];
				keys[b] = t;
			}
		}

		// Token: 0x06006256 RID: 25174 RVA: 0x00148F38 File Offset: 0x00147138
		private static void Swap(T[] a, int i, int j)
		{
			if (i != j)
			{
				T t = a[i];
				a[i] = a[j];
				a[j] = t;
			}
		}

		// Token: 0x06006257 RID: 25175 RVA: 0x00148F67 File Offset: 0x00147167
		internal static void IntrospectiveSort(T[] keys, int left, int length)
		{
			if (length < 2)
			{
				return;
			}
			GenericArraySortHelper<T>.IntroSort(keys, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2PlusOne(length));
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x00148F84 File Offset: 0x00147184
		private static void IntroSort(T[] keys, int lo, int hi, int depthLimit)
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
						GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, hi);
						return;
					}
					if (num == 3)
					{
						GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, hi - 1);
						GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, hi);
						GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, hi - 1, hi);
						return;
					}
					GenericArraySortHelper<T>.InsertionSort(keys, lo, hi);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						GenericArraySortHelper<T>.Heapsort(keys, lo, hi);
						return;
					}
					depthLimit--;
					int num2 = GenericArraySortHelper<T>.PickPivotAndPartition(keys, lo, hi);
					GenericArraySortHelper<T>.IntroSort(keys, num2 + 1, hi, depthLimit);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x00149008 File Offset: 0x00147208
		private static int PickPivotAndPartition(T[] keys, int lo, int hi)
		{
			int num = lo + (hi - lo) / 2;
			GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, num);
			GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, lo, hi);
			GenericArraySortHelper<T>.SwapIfGreaterWithItems(keys, num, hi);
			T t = keys[num];
			GenericArraySortHelper<T>.Swap(keys, num, hi - 1);
			int i = lo;
			int j = hi - 1;
			while (i < j)
			{
				if (t == null)
				{
					while (i < hi - 1 && keys[++i] == null)
					{
					}
					while (j > lo)
					{
						if (keys[--j] == null)
						{
							break;
						}
					}
				}
				else
				{
					while (t.CompareTo(keys[++i]) > 0)
					{
					}
					while (t.CompareTo(keys[--j]) < 0)
					{
					}
				}
				if (i >= j)
				{
					break;
				}
				GenericArraySortHelper<T>.Swap(keys, i, j);
			}
			GenericArraySortHelper<T>.Swap(keys, i, hi - 1);
			return i;
		}

		// Token: 0x0600625A RID: 25178 RVA: 0x001490D8 File Offset: 0x001472D8
		private static void Heapsort(T[] keys, int lo, int hi)
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				GenericArraySortHelper<T>.DownHeap(keys, i, num, lo);
			}
			for (int j = num; j > 1; j--)
			{
				GenericArraySortHelper<T>.Swap(keys, lo, lo + j - 1);
				GenericArraySortHelper<T>.DownHeap(keys, 1, j - 1, lo);
			}
		}

		// Token: 0x0600625B RID: 25179 RVA: 0x00149128 File Offset: 0x00147328
		private static void DownHeap(T[] keys, int i, int n, int lo)
		{
			T t = keys[lo + i - 1];
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && (keys[lo + num - 1] == null || keys[lo + num - 1].CompareTo(keys[lo + num]) < 0))
				{
					num++;
				}
				if (keys[lo + num - 1] == null || keys[lo + num - 1].CompareTo(t) < 0)
				{
					break;
				}
				keys[lo + i - 1] = keys[lo + num - 1];
				i = num;
			}
			keys[lo + i - 1] = t;
		}

		// Token: 0x0600625C RID: 25180 RVA: 0x001491E4 File Offset: 0x001473E4
		private static void InsertionSort(T[] keys, int lo, int hi)
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T t = keys[i + 1];
				while (num >= lo && (t == null || t.CompareTo(keys[num]) < 0))
				{
					keys[num + 1] = keys[num];
					num--;
				}
				keys[num + 1] = t;
			}
		}

		// Token: 0x0600625D RID: 25181 RVA: 0x0000259F File Offset: 0x0000079F
		public GenericArraySortHelper()
		{
		}
	}
}
