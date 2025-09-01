using System;

namespace System.Linq
{
	// Token: 0x020000E3 RID: 227
	internal abstract class EnumerableSorter<TElement>
	{
		// Token: 0x060007FF RID: 2047
		internal abstract void ComputeKeys(TElement[] elements, int count);

		// Token: 0x06000800 RID: 2048
		internal abstract int CompareAnyKeys(int index1, int index2);

		// Token: 0x06000801 RID: 2049 RVA: 0x0001C0E8 File Offset: 0x0001A2E8
		private int[] ComputeMap(TElement[] elements, int count)
		{
			this.ComputeKeys(elements, count);
			int[] array = new int[count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i;
			}
			return array;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001C118 File Offset: 0x0001A318
		internal int[] Sort(TElement[] elements, int count)
		{
			int[] array = this.ComputeMap(elements, count);
			this.QuickSort(array, 0, count - 1);
			return array;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001C13C File Offset: 0x0001A33C
		internal int[] Sort(TElement[] elements, int count, int minIdx, int maxIdx)
		{
			int[] array = this.ComputeMap(elements, count);
			this.PartialQuickSort(array, 0, count - 1, minIdx, maxIdx);
			return array;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001C161 File Offset: 0x0001A361
		internal TElement ElementAt(TElement[] elements, int count, int idx)
		{
			return elements[this.QuickSelect(this.ComputeMap(elements, count), count - 1, idx)];
		}

		// Token: 0x06000805 RID: 2053
		protected abstract void QuickSort(int[] map, int left, int right);

		// Token: 0x06000806 RID: 2054
		protected abstract void PartialQuickSort(int[] map, int left, int right, int minIdx, int maxIdx);

		// Token: 0x06000807 RID: 2055
		protected abstract int QuickSelect(int[] map, int right, int idx);

		// Token: 0x06000808 RID: 2056 RVA: 0x00002162 File Offset: 0x00000362
		protected EnumerableSorter()
		{
		}
	}
}
