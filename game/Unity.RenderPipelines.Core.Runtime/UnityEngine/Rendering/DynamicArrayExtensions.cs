using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004D RID: 77
	public static class DynamicArrayExtensions
	{
		// Token: 0x060002AC RID: 684 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		private static int Partition<T>(T[] data, int left, int right) where T : IComparable<T>, new()
		{
			T other = data[left];
			left--;
			right++;
			for (;;)
			{
				T t = default(T);
				int num;
				do
				{
					left++;
					t = data[left];
					num = t.CompareTo(other);
				}
				while (num < 0);
				T t2 = default(T);
				do
				{
					right--;
					t2 = data[right];
					num = t2.CompareTo(other);
				}
				while (num > 0);
				if (left >= right)
				{
					break;
				}
				data[right] = t;
				data[left] = t2;
			}
			return right;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000DE2C File Offset: 0x0000C02C
		private static void QuickSort<T>(T[] data, int left, int right) where T : IComparable<T>, new()
		{
			if (left < right)
			{
				int num = DynamicArrayExtensions.Partition<T>(data, left, right);
				if (num >= 1)
				{
					DynamicArrayExtensions.QuickSort<T>(data, left, num);
				}
				if (num + 1 < right)
				{
					DynamicArrayExtensions.QuickSort<T>(data, num + 1, right);
				}
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000DE62 File Offset: 0x0000C062
		public static void QuickSort<T>(this DynamicArray<T> array) where T : IComparable<T>, new()
		{
			DynamicArrayExtensions.QuickSort<T>(array, 0, array.size - 1);
		}
	}
}
