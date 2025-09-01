using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.Internal.Collections
{
	// Token: 0x02000017 RID: 23
	internal static class CollectionServices
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000038B8 File Offset: 0x00001AB8
		public static ICollection<object> GetCollectionWrapper(Type itemType, object collectionObject)
		{
			Assumes.NotNull<Type, object>(itemType, collectionObject);
			Type underlyingSystemType = itemType.UnderlyingSystemType;
			if (underlyingSystemType == typeof(object))
			{
				return (ICollection<object>)collectionObject;
			}
			if (typeof(IList).IsAssignableFrom(collectionObject.GetType()))
			{
				return new CollectionServices.CollectionOfObjectList((IList)collectionObject);
			}
			return (ICollection<object>)Activator.CreateInstance(typeof(CollectionServices.CollectionOfObject<>).MakeGenericType(new Type[]
			{
				underlyingSystemType
			}), new object[]
			{
				collectionObject
			});
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000393C File Offset: 0x00001B3C
		public static bool IsEnumerableOfT(Type type)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition().UnderlyingSystemType == CollectionServices.IEnumerableOfTType;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003960 File Offset: 0x00001B60
		public static Type GetEnumerableElementType(Type type)
		{
			if (type.UnderlyingSystemType == CollectionServices.StringType || !CollectionServices.IEnumerableType.IsAssignableFrom(type))
			{
				return null;
			}
			Type type2;
			if (ReflectionServices.TryGetGenericInterfaceType(type, CollectionServices.IEnumerableOfTType, out type2))
			{
				return type2.GetGenericArguments()[0];
			}
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000039A8 File Offset: 0x00001BA8
		public static Type GetCollectionElementType(Type type)
		{
			Type type2;
			if (ReflectionServices.TryGetGenericInterfaceType(type, CollectionServices.ICollectionOfTType, out type2))
			{
				return type2.GetGenericArguments()[0];
			}
			return null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000039CE File Offset: 0x00001BCE
		public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
		{
			Assumes.NotNull<IEnumerable<T>>(source);
			return new ReadOnlyCollection<T>(source.AsArray<T>());
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000039E1 File Offset: 0x00001BE1
		public static IEnumerable<T> ConcatAllowingNull<T>(this IEnumerable<T> source, IEnumerable<T> second)
		{
			if (second == null || !second.FastAny<T>())
			{
				return source;
			}
			if (source == null || !source.FastAny<T>())
			{
				return second;
			}
			return source.Concat(second);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003A04 File Offset: 0x00001C04
		public static ICollection<T> ConcatAllowingNull<T>(this ICollection<T> source, ICollection<T> second)
		{
			if (second == null || second.Count == 0)
			{
				return source;
			}
			if (source == null || source.Count == 0)
			{
				return second;
			}
			List<T> list = new List<T>(source);
			list.AddRange(second);
			return list;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003A30 File Offset: 0x00001C30
		public static List<T> FastAppendToListAllowNulls<T>(this List<T> source, IEnumerable<T> second)
		{
			if (second == null)
			{
				return source;
			}
			if (source == null || source.Count == 0)
			{
				return second.AsList<T>();
			}
			List<T> list = second as List<T>;
			if (list != null)
			{
				if (list.Count == 0)
				{
					return source;
				}
				if (list.Count == 1)
				{
					source.Add(list[0]);
					return source;
				}
			}
			source.AddRange(second);
			return source;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003A88 File Offset: 0x00001C88
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			foreach (T obj in source)
			{
				action(obj);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003AD0 File Offset: 0x00001CD0
		public static EnumerableCardinality GetCardinality<T>(this IEnumerable<T> source)
		{
			Assumes.NotNull<IEnumerable<T>>(source);
			ICollection collection = source as ICollection;
			if (collection == null)
			{
				EnumerableCardinality result;
				using (IEnumerator<T> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						result = EnumerableCardinality.Zero;
					}
					else if (!enumerator.MoveNext())
					{
						result = EnumerableCardinality.One;
					}
					else
					{
						result = EnumerableCardinality.TwoOrMore;
					}
				}
				return result;
			}
			int count = collection.Count;
			if (count == 0)
			{
				return EnumerableCardinality.Zero;
			}
			if (count != 1)
			{
				return EnumerableCardinality.TwoOrMore;
			}
			return EnumerableCardinality.One;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003B44 File Offset: 0x00001D44
		public static bool FastAny<T>(this IEnumerable<T> source)
		{
			ICollection collection = source as ICollection;
			if (collection != null)
			{
				return collection.Count > 0;
			}
			return source.Any<T>();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003B6B File Offset: 0x00001D6B
		public static Stack<T> Copy<T>(this Stack<T> stack)
		{
			Assumes.NotNull<Stack<T>>(stack);
			return new Stack<T>(stack.Reverse<T>());
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003B80 File Offset: 0x00001D80
		public static T[] AsArray<T>(this IEnumerable<T> enumerable)
		{
			T[] array = enumerable as T[];
			if (array != null)
			{
				return array;
			}
			return enumerable.ToArray<T>();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public static List<T> AsList<T>(this IEnumerable<T> enumerable)
		{
			List<T> list = enumerable as List<T>;
			if (list != null)
			{
				return list;
			}
			return enumerable.ToList<T>();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public static bool IsArrayEqual<T>(this T[] thisArray, T[] thatArray)
		{
			if (thisArray.Length != thatArray.Length)
			{
				return false;
			}
			for (int i = 0; i < thisArray.Length; i++)
			{
				if (!thisArray[i].Equals(thatArray[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003C0C File Offset: 0x00001E0C
		public static bool IsCollectionEqual<T>(this IList<T> thisList, IList<T> thatList)
		{
			if (thisList.Count != thatList.Count)
			{
				return false;
			}
			for (int i = 0; i < thisList.Count; i++)
			{
				T t = thisList[i];
				if (!t.Equals(thatList[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003C60 File Offset: 0x00001E60
		// Note: this type is marked as 'beforefieldinit'.
		static CollectionServices()
		{
		}

		// Token: 0x0400005A RID: 90
		private static readonly Type StringType = typeof(string);

		// Token: 0x0400005B RID: 91
		private static readonly Type IEnumerableType = typeof(IEnumerable);

		// Token: 0x0400005C RID: 92
		private static readonly Type IEnumerableOfTType = typeof(IEnumerable<>);

		// Token: 0x0400005D RID: 93
		private static readonly Type ICollectionOfTType = typeof(ICollection<>);

		// Token: 0x02000018 RID: 24
		private class CollectionOfObjectList : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			// Token: 0x060000E7 RID: 231 RVA: 0x00003C9E File Offset: 0x00001E9E
			public CollectionOfObjectList(IList list)
			{
				this._list = list;
			}

			// Token: 0x060000E8 RID: 232 RVA: 0x00003CAD File Offset: 0x00001EAD
			public void Add(object item)
			{
				this._list.Add(item);
			}

			// Token: 0x060000E9 RID: 233 RVA: 0x00003CBC File Offset: 0x00001EBC
			public void Clear()
			{
				this._list.Clear();
			}

			// Token: 0x060000EA RID: 234 RVA: 0x00003CC9 File Offset: 0x00001EC9
			public bool Contains(object item)
			{
				return Assumes.NotReachable<bool>();
			}

			// Token: 0x060000EB RID: 235 RVA: 0x00003CD0 File Offset: 0x00001ED0
			public void CopyTo(object[] array, int arrayIndex)
			{
				Assumes.NotReachable<object>();
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060000EC RID: 236 RVA: 0x00003CD8 File Offset: 0x00001ED8
			public int Count
			{
				get
				{
					return Assumes.NotReachable<int>();
				}
			}

			// Token: 0x17000077 RID: 119
			// (get) Token: 0x060000ED RID: 237 RVA: 0x00003CDF File Offset: 0x00001EDF
			public bool IsReadOnly
			{
				get
				{
					return this._list.IsReadOnly;
				}
			}

			// Token: 0x060000EE RID: 238 RVA: 0x00003CC9 File Offset: 0x00001EC9
			public bool Remove(object item)
			{
				return Assumes.NotReachable<bool>();
			}

			// Token: 0x060000EF RID: 239 RVA: 0x00003CEC File Offset: 0x00001EEC
			public IEnumerator<object> GetEnumerator()
			{
				return Assumes.NotReachable<IEnumerator<object>>();
			}

			// Token: 0x060000F0 RID: 240 RVA: 0x00003CF3 File Offset: 0x00001EF3
			IEnumerator IEnumerable.GetEnumerator()
			{
				return Assumes.NotReachable<IEnumerator>();
			}

			// Token: 0x0400005E RID: 94
			private readonly IList _list;
		}

		// Token: 0x02000019 RID: 25
		private class CollectionOfObject<T> : ICollection<object>, IEnumerable<object>, IEnumerable
		{
			// Token: 0x060000F1 RID: 241 RVA: 0x00003CFA File Offset: 0x00001EFA
			public CollectionOfObject(object collectionOfT)
			{
				this._collectionOfT = (ICollection<T>)collectionOfT;
			}

			// Token: 0x060000F2 RID: 242 RVA: 0x00003D0E File Offset: 0x00001F0E
			public void Add(object item)
			{
				this._collectionOfT.Add((T)((object)item));
			}

			// Token: 0x060000F3 RID: 243 RVA: 0x00003D21 File Offset: 0x00001F21
			public void Clear()
			{
				this._collectionOfT.Clear();
			}

			// Token: 0x060000F4 RID: 244 RVA: 0x00003CC9 File Offset: 0x00001EC9
			public bool Contains(object item)
			{
				return Assumes.NotReachable<bool>();
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x00003CD0 File Offset: 0x00001ED0
			public void CopyTo(object[] array, int arrayIndex)
			{
				Assumes.NotReachable<object>();
			}

			// Token: 0x17000078 RID: 120
			// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003CD8 File Offset: 0x00001ED8
			public int Count
			{
				get
				{
					return Assumes.NotReachable<int>();
				}
			}

			// Token: 0x17000079 RID: 121
			// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003D2E File Offset: 0x00001F2E
			public bool IsReadOnly
			{
				get
				{
					return this._collectionOfT.IsReadOnly;
				}
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x00003CC9 File Offset: 0x00001EC9
			public bool Remove(object item)
			{
				return Assumes.NotReachable<bool>();
			}

			// Token: 0x060000F9 RID: 249 RVA: 0x00003CEC File Offset: 0x00001EEC
			public IEnumerator<object> GetEnumerator()
			{
				return Assumes.NotReachable<IEnumerator<object>>();
			}

			// Token: 0x060000FA RID: 250 RVA: 0x00003CF3 File Offset: 0x00001EF3
			IEnumerator IEnumerable.GetEnumerator()
			{
				return Assumes.NotReachable<IEnumerator>();
			}

			// Token: 0x0400005F RID: 95
			private readonly ICollection<T> _collectionOfT;
		}
	}
}
