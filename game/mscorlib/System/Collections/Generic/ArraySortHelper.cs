using System;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x02000ABC RID: 2748
	internal class ArraySortHelper<T>
	{
		// Token: 0x06006243 RID: 25155 RVA: 0x00148918 File Offset: 0x00146B18
		public void Sort(T[] keys, int index, int length, IComparer<T> comparer)
		{
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				ArraySortHelper<T>.IntrospectiveSort(keys, index, length, new Comparison<T>(comparer.Compare));
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

		// Token: 0x06006244 RID: 25156 RVA: 0x0014898C File Offset: 0x00146B8C
		public int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int result;
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				result = ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
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

		// Token: 0x06006245 RID: 25157 RVA: 0x001489E0 File Offset: 0x00146BE0
		internal static void Sort(T[] keys, int index, int length, Comparison<T> comparer)
		{
			try
			{
				ArraySortHelper<T>.IntrospectiveSort(keys, index, length, comparer);
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

		// Token: 0x06006246 RID: 25158 RVA: 0x00148A3C File Offset: 0x00146C3C
		internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = comparer.Compare(array[num2], value);
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

		// Token: 0x06006247 RID: 25159 RVA: 0x00148A84 File Offset: 0x00146C84
		private static void SwapIfGreater(T[] keys, Comparison<T> comparer, int a, int b)
		{
			if (a != b && comparer(keys[a], keys[b]) > 0)
			{
				T t = keys[a];
				keys[a] = keys[b];
				keys[b] = t;
			}
		}

		// Token: 0x06006248 RID: 25160 RVA: 0x00148ACC File Offset: 0x00146CCC
		private static void Swap(T[] a, int i, int j)
		{
			if (i != j)
			{
				T t = a[i];
				a[i] = a[j];
				a[j] = t;
			}
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x00148AFB File Offset: 0x00146CFB
		internal static void IntrospectiveSort(T[] keys, int left, int length, Comparison<T> comparer)
		{
			if (length < 2)
			{
				return;
			}
			ArraySortHelper<T>.IntroSort(keys, left, length + left - 1, 2 * IntrospectiveSortUtilities.FloorLog2PlusOne(length), comparer);
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x00148B18 File Offset: 0x00146D18
		private static void IntroSort(T[] keys, int lo, int hi, int depthLimit, Comparison<T> comparer)
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
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
						return;
					}
					if (num == 3)
					{
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi - 1);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, hi - 1, hi);
						return;
					}
					ArraySortHelper<T>.InsertionSort(keys, lo, hi, comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						ArraySortHelper<T>.Heapsort(keys, lo, hi, comparer);
						return;
					}
					depthLimit--;
					int num2 = ArraySortHelper<T>.PickPivotAndPartition(keys, lo, hi, comparer);
					ArraySortHelper<T>.IntroSort(keys, num2 + 1, hi, depthLimit, comparer);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x00148BB4 File Offset: 0x00146DB4
		private static int PickPivotAndPartition(T[] keys, int lo, int hi, Comparison<T> comparer)
		{
			int num = lo + (hi - lo) / 2;
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, num);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, lo, hi);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, num, hi);
			T t = keys[num];
			ArraySortHelper<T>.Swap(keys, num, hi - 1);
			int i = lo;
			int num2 = hi - 1;
			while (i < num2)
			{
				while (comparer(keys[++i], t) < 0)
				{
				}
				while (comparer(t, keys[--num2]) < 0)
				{
				}
				if (i >= num2)
				{
					break;
				}
				ArraySortHelper<T>.Swap(keys, i, num2);
			}
			ArraySortHelper<T>.Swap(keys, i, hi - 1);
			return i;
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x00148C44 File Offset: 0x00146E44
		private static void Heapsort(T[] keys, int lo, int hi, Comparison<T> comparer)
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				ArraySortHelper<T>.DownHeap(keys, i, num, lo, comparer);
			}
			for (int j = num; j > 1; j--)
			{
				ArraySortHelper<T>.Swap(keys, lo, lo + j - 1);
				ArraySortHelper<T>.DownHeap(keys, 1, j - 1, lo, comparer);
			}
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x00148C94 File Offset: 0x00146E94
		private static void DownHeap(T[] keys, int i, int n, int lo, Comparison<T> comparer)
		{
			T t = keys[lo + i - 1];
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && comparer(keys[lo + num - 1], keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer(t, keys[lo + num - 1]) >= 0)
				{
					break;
				}
				keys[lo + i - 1] = keys[lo + num - 1];
				i = num;
			}
			keys[lo + i - 1] = t;
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x00148D1C File Offset: 0x00146F1C
		private static void InsertionSort(T[] keys, int lo, int hi, Comparison<T> comparer)
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T t = keys[i + 1];
				while (num >= lo && comparer(t, keys[num]) < 0)
				{
					keys[num + 1] = keys[num];
					num--;
				}
				keys[num + 1] = t;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x0600624F RID: 25167 RVA: 0x00148D76 File Offset: 0x00146F76
		public static ArraySortHelper<T> Default
		{
			get
			{
				return ArraySortHelper<T>.s_defaultArraySortHelper;
			}
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x0000259F File Offset: 0x0000079F
		public ArraySortHelper()
		{
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x00148D7D File Offset: 0x00146F7D
		// Note: this type is marked as 'beforefieldinit'.
		static ArraySortHelper()
		{
		}

		// Token: 0x04003A32 RID: 14898
		private static readonly ArraySortHelper<T> s_defaultArraySortHelper = new ArraySortHelper<T>();
	}
}
