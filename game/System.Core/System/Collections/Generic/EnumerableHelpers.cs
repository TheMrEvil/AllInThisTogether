using System;
using System.Linq;

namespace System.Collections.Generic
{
	// Token: 0x02000354 RID: 852
	internal static class EnumerableHelpers
	{
		// Token: 0x060019F5 RID: 6645 RVA: 0x00056D40 File Offset: 0x00054F40
		internal static bool TryGetCount<T>(IEnumerable<T> source, out int count)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				count = collection.Count;
				return true;
			}
			IIListProvider<T> iilistProvider = source as IIListProvider<T>;
			if (iilistProvider != null)
			{
				return (count = iilistProvider.GetCount(true)) >= 0;
			}
			count = -1;
			return false;
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00056D84 File Offset: 0x00054F84
		internal static void Copy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				collection.CopyTo(array, arrayIndex);
				return;
			}
			EnumerableHelpers.IterativeCopy<T>(source, array, arrayIndex, count);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00056DB0 File Offset: 0x00054FB0
		internal static void IterativeCopy<T>(IEnumerable<T> source, T[] array, int arrayIndex, int count)
		{
			foreach (T t in source)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00056E00 File Offset: 0x00055000
		internal static T[] ToArray<T>(IEnumerable<T> source)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection == null)
			{
				LargeArrayBuilder<T> largeArrayBuilder = new LargeArrayBuilder<T>(true);
				largeArrayBuilder.AddRange(source);
				return largeArrayBuilder.ToArray();
			}
			int count = collection.Count;
			if (count == 0)
			{
				return Array.Empty<T>();
			}
			T[] array = new T[count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x00056E50 File Offset: 0x00055050
		internal static T[] ToArray<T>(IEnumerable<T> source, out int length)
		{
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				int count = collection.Count;
				if (count != 0)
				{
					T[] array = new T[count];
					collection.CopyTo(array, 0);
					length = count;
					return array;
				}
			}
			else
			{
				using (IEnumerator<T> enumerator = source.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						T[] array2 = new T[4];
						array2[0] = enumerator.Current;
						int num = 1;
						while (enumerator.MoveNext())
						{
							if (num == array2.Length)
							{
								int num2 = num << 1;
								if (num2 > 2146435071)
								{
									num2 = ((2146435071 <= num) ? (num + 1) : 2146435071);
								}
								Array.Resize<T>(ref array2, num2);
							}
							array2[num++] = enumerator.Current;
						}
						length = num;
						return array2;
					}
				}
			}
			length = 0;
			return Array.Empty<T>();
		}
	}
}
