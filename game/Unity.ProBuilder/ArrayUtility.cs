using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000003 RID: 3
	internal static class ArrayUtility
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000020E8 File Offset: 0x000002E8
		public static T[] ValuesWithIndexes<T>(this T[] arr, int[] indexes)
		{
			T[] array = new T[indexes.Length];
			for (int i = 0; i < indexes.Length; i++)
			{
				array[i] = arr[indexes[i]];
			}
			return array;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002120 File Offset: 0x00000320
		public static List<T> ValuesWithIndexes<T>(this List<T> arr, IList<int> indexes)
		{
			List<T> list = new List<T>(indexes.Count);
			foreach (int index in indexes)
			{
				list.Add(arr[index]);
			}
			return list;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000217C File Offset: 0x0000037C
		public static IEnumerable<int> AllIndexesOf<T>(this IList<T> list, Func<T, bool> lambda)
		{
			List<int> list2 = new List<int>();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (lambda(list[i]))
				{
					list2.Add(i);
				}
				i++;
			}
			return list2;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021BC File Offset: 0x000003BC
		public static T[] Add<T>(this T[] arr, T val)
		{
			T[] array = new T[arr.Length + 1];
			Array.ConstrainedCopy(arr, 0, array, 0, arr.Length);
			array[arr.Length] = val;
			return array;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021EC File Offset: 0x000003EC
		public static T[] AddRange<T>(this T[] arr, T[] val)
		{
			T[] array = new T[arr.Length + val.Length];
			Array.ConstrainedCopy(arr, 0, array, 0, arr.Length);
			Array.ConstrainedCopy(val, 0, array, arr.Length, val.Length);
			return array;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002221 File Offset: 0x00000421
		public static T[] Remove<T>(this T[] arr, T val)
		{
			List<T> list = new List<T>(arr);
			list.Remove(val);
			return list.ToArray();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002236 File Offset: 0x00000436
		public static T[] Remove<T>(this T[] arr, IEnumerable<T> val)
		{
			return arr.Except(val).ToArray<T>();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002244 File Offset: 0x00000444
		public static T[] RemoveAt<T>(this T[] arr, int index)
		{
			T[] array = new T[arr.Length - 1];
			int num = 0;
			for (int i = 0; i < arr.Length; i++)
			{
				if (i != index)
				{
					array[num] = arr[i];
					num++;
				}
			}
			return array;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002284 File Offset: 0x00000484
		public static T[] RemoveAt<T>(this IList<T> list, IEnumerable<int> indexes)
		{
			List<int> list2 = new List<int>(indexes);
			list2.Sort();
			return list.SortedRemoveAt(list2);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022A8 File Offset: 0x000004A8
		public static T[] SortedRemoveAt<T>(this IList<T> list, IList<int> sorted)
		{
			int count = sorted.Count;
			int count2 = list.Count;
			T[] array = new T[count2 - count];
			int i = 0;
			for (int j = 0; j < count2; j++)
			{
				if (i < count && sorted[i] == j)
				{
					while (i < count)
					{
						if (sorted[i] != j)
						{
							break;
						}
						i++;
					}
				}
				else
				{
					array[j - i] = list[j];
				}
			}
			return array;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002318 File Offset: 0x00000518
		public static int NearestIndexPriorToValue<T>(IList<T> sorted_list, T value) where T : IComparable<T>
		{
			int count = sorted_list.Count;
			if (count < 1)
			{
				return -1;
			}
			ArrayUtility.SearchRange searchRange = new ArrayUtility.SearchRange(0, count - 1);
			if (value.CompareTo(sorted_list[0]) < 0)
			{
				return -1;
			}
			if (value.CompareTo(sorted_list[count - 1]) > 0)
			{
				return count - 1;
			}
			while (searchRange.Valid())
			{
				T t = sorted_list[searchRange.Center()];
				if (t.CompareTo(value) > 0)
				{
					searchRange.end = searchRange.Center();
				}
				else
				{
					searchRange.begin = searchRange.Center();
					t = sorted_list[searchRange.begin + 1];
					if (t.CompareTo(value) >= 0)
					{
						return searchRange.begin;
					}
				}
			}
			return 0;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023E4 File Offset: 0x000005E4
		public static List<T> Fill<T>(Func<int, T> ctor, int length)
		{
			List<T> list = new List<T>(length);
			for (int i = 0; i < length; i++)
			{
				list.Add(ctor(i));
			}
			return list;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002414 File Offset: 0x00000614
		public static T[] Fill<T>(T val, int length)
		{
			T[] array = new T[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = val;
			}
			return array;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002440 File Offset: 0x00000640
		public static bool ContainsMatch<T>(this T[] a, T[] b)
		{
			for (int i = 0; i < a.Length; i++)
			{
				if (Array.IndexOf<T>(b, a[i]) > -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000246E File Offset: 0x0000066E
		public static bool ContainsMatch<T>(this T[] a, T[] b, out int index_a, out int index_b)
		{
			index_b = -1;
			for (index_a = 0; index_a < a.Length; index_a++)
			{
				index_b = Array.IndexOf<T>(b, a[index_a]);
				if (index_b > -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024A0 File Offset: 0x000006A0
		public static T[] Concat<T>(this T[] x, T[] y)
		{
			if (x == null)
			{
				throw new ArgumentNullException("x");
			}
			if (y == null)
			{
				throw new ArgumentNullException("y");
			}
			int destinationIndex = x.Length;
			Array.Resize<T>(ref x, x.Length + y.Length);
			Array.Copy(y, 0, x, destinationIndex, y.Length);
			return x;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024E8 File Offset: 0x000006E8
		public static int IndexOf<T>(this List<List<T>> InList, T InValue)
		{
			for (int i = 0; i < InList.Count; i++)
			{
				for (int j = 0; j < InList[i].Count; j++)
				{
					T t = InList[i][j];
					if (t.Equals(InValue))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002544 File Offset: 0x00000744
		public static T[] Fill<T>(int count, Func<int, T> ctor)
		{
			T[] array = new T[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = ctor(i);
			}
			return array;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002574 File Offset: 0x00000774
		public static void AddOrAppend<T, K>(this Dictionary<T, List<K>> dictionary, T key, K value)
		{
			List<K> list;
			if (dictionary.TryGetValue(key, out list))
			{
				list.Add(value);
				return;
			}
			dictionary.Add(key, new List<K>
			{
				value
			});
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025A8 File Offset: 0x000007A8
		public static void AddOrAppendRange<T, K>(this Dictionary<T, List<K>> dictionary, T key, List<K> value)
		{
			List<K> list;
			if (dictionary.TryGetValue(key, out list))
			{
				list.AddRange(value);
				return;
			}
			dictionary.Add(key, value);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025D0 File Offset: 0x000007D0
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> knownKeys = new HashSet<TKey>();
			return from x in source
			where knownKeys.Add(keySelector(x))
			select x;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002608 File Offset: 0x00000808
		public static string ToString<TKey, TValue>(this Dictionary<TKey, TValue> dict)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dict)
			{
				stringBuilder.AppendLine(string.Format("Key: {0}  Value: {1}", keyValuePair.Key, keyValuePair.Value));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002684 File Offset: 0x00000884
		public static string ToString<T>(this IEnumerable<T> arr, string separator = ", ")
		{
			return string.Join(separator, (from x in arr
			select x.ToString()).ToArray<string>());
		}

		// Token: 0x0200008E RID: 142
		private struct SearchRange
		{
			// Token: 0x06000528 RID: 1320 RVA: 0x00035A33 File Offset: 0x00033C33
			public SearchRange(int begin, int end)
			{
				this.begin = begin;
				this.end = end;
			}

			// Token: 0x06000529 RID: 1321 RVA: 0x00035A43 File Offset: 0x00033C43
			public bool Valid()
			{
				return this.end - this.begin > 1;
			}

			// Token: 0x0600052A RID: 1322 RVA: 0x00035A55 File Offset: 0x00033C55
			public int Center()
			{
				return this.begin + (this.end - this.begin) / 2;
			}

			// Token: 0x0600052B RID: 1323 RVA: 0x00035A70 File Offset: 0x00033C70
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"{",
					this.begin.ToString(),
					", ",
					this.end.ToString(),
					"} : ",
					this.Center().ToString()
				});
			}

			// Token: 0x0400027D RID: 637
			public int begin;

			// Token: 0x0400027E RID: 638
			public int end;
		}

		// Token: 0x0200008F RID: 143
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0<TSource, TKey>
		{
			// Token: 0x0600052C RID: 1324 RVA: 0x00035ACD File Offset: 0x00033CCD
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x0600052D RID: 1325 RVA: 0x00035AD5 File Offset: 0x00033CD5
			internal bool <DistinctBy>b__0(TSource x)
			{
				return this.knownKeys.Add(this.keySelector(x));
			}

			// Token: 0x0400027F RID: 639
			public HashSet<TKey> knownKeys;

			// Token: 0x04000280 RID: 640
			public Func<TSource, TKey> keySelector;
		}

		// Token: 0x02000090 RID: 144
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__23<T>
		{
			// Token: 0x0600052E RID: 1326 RVA: 0x00035AEE File Offset: 0x00033CEE
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__23()
			{
			}

			// Token: 0x0600052F RID: 1327 RVA: 0x00035AFA File Offset: 0x00033CFA
			public <>c__23()
			{
			}

			// Token: 0x06000530 RID: 1328 RVA: 0x00035B02 File Offset: 0x00033D02
			internal string <ToString>b__23_0(T x)
			{
				return x.ToString();
			}

			// Token: 0x04000281 RID: 641
			public static readonly ArrayUtility.<>c__23<T> <>9 = new ArrayUtility.<>c__23<T>();

			// Token: 0x04000282 RID: 642
			public static Func<T, string> <>9__23_0;
		}
	}
}
