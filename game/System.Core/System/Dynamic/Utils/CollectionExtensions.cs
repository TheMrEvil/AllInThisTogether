using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
	// Token: 0x02000325 RID: 805
	internal static class CollectionExtensions
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x000520CC File Offset: 0x000502CC
		public static TrueReadOnlyCollection<T> AddFirst<T>(this ReadOnlyCollection<T> list, T item)
		{
			T[] array = new T[list.Count + 1];
			array[0] = item;
			list.CopyTo(array, 1);
			return new TrueReadOnlyCollection<T>(array);
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00052100 File Offset: 0x00050300
		public static T[] AddFirst<T>(this T[] array, T item)
		{
			T[] array2 = new T[array.Length + 1];
			array2[0] = item;
			array.CopyTo(array2, 1);
			return array2;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x0005212C File Offset: 0x0005032C
		public static T[] AddLast<T>(this T[] array, T item)
		{
			T[] array2 = new T[array.Length + 1];
			array.CopyTo(array2, 0);
			array2[array.Length] = item;
			return array2;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00052158 File Offset: 0x00050358
		public static T[] RemoveFirst<T>(this T[] array)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, 1, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x00052180 File Offset: 0x00050380
		public static T[] RemoveLast<T>(this T[] array)
		{
			T[] array2 = new T[array.Length - 1];
			Array.Copy(array, 0, array2, 0, array2.Length);
			return array2;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x000521A8 File Offset: 0x000503A8
		public static ReadOnlyCollection<T> ToReadOnly<T>(this IEnumerable<T> enumerable)
		{
			if (enumerable == null)
			{
				return EmptyReadOnlyCollection<T>.Instance;
			}
			TrueReadOnlyCollection<T> trueReadOnlyCollection = enumerable as TrueReadOnlyCollection<T>;
			if (trueReadOnlyCollection != null)
			{
				return trueReadOnlyCollection;
			}
			ReadOnlyCollectionBuilder<T> readOnlyCollectionBuilder = enumerable as ReadOnlyCollectionBuilder<T>;
			if (readOnlyCollectionBuilder != null)
			{
				return readOnlyCollectionBuilder.ToReadOnlyCollection();
			}
			T[] array = enumerable.ToArray<T>();
			if (array.Length != 0)
			{
				return new TrueReadOnlyCollection<T>(array);
			}
			return EmptyReadOnlyCollection<T>.Instance;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000521F4 File Offset: 0x000503F4
		public static int ListHashCode<T>(this ReadOnlyCollection<T> list)
		{
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			int num = 6551;
			foreach (T obj in list)
			{
				num ^= (num << 5 ^ @default.GetHashCode(obj));
			}
			return num;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00052250 File Offset: 0x00050450
		public static bool ListEquals<T>(this ReadOnlyCollection<T> first, ReadOnlyCollection<T> second)
		{
			if (first == second)
			{
				return true;
			}
			int count = first.Count;
			if (count != second.Count)
			{
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			for (int num = 0; num != count; num++)
			{
				if (!@default.Equals(first[num], second[num]))
				{
					return false;
				}
			}
			return true;
		}
	}
}
