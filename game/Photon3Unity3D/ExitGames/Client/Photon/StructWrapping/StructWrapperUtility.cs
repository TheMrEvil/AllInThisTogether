using System;
using System.Collections.Generic;

namespace ExitGames.Client.Photon.StructWrapping
{
	// Token: 0x02000046 RID: 70
	public static class StructWrapperUtility
	{
		// Token: 0x06000399 RID: 921 RVA: 0x0001B878 File Offset: 0x00019A78
		public static Type GetWrappedType(this object obj)
		{
			StructWrapper structWrapper = obj as StructWrapper;
			bool flag = structWrapper == null;
			Type result;
			if (flag)
			{
				result = obj.GetType();
			}
			else
			{
				result = structWrapper.ttype;
			}
			return result;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001B8A8 File Offset: 0x00019AA8
		public static StructWrapper<T> Wrap<T>(this T value, bool persistant)
		{
			StructWrapper<T> structWrapper = StructWrapper<T>.staticPool.Acquire(value);
			if (persistant)
			{
				structWrapper.DisconnectFromPool();
			}
			return structWrapper;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001B8D8 File Offset: 0x00019AD8
		public static StructWrapper<T> Wrap<T>(this T value)
		{
			return StructWrapper<T>.staticPool.Acquire(value);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public static StructWrapper<byte> Wrap(this byte value)
		{
			return StructWrapperPools.mappedByteWrappers[(int)value];
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0001B914 File Offset: 0x00019B14
		public static StructWrapper<bool> Wrap(this bool value)
		{
			return StructWrapperPools.mappedBoolWrappers[value ? 1 : 0];
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001B934 File Offset: 0x00019B34
		public static bool IsType<T>(this object obj)
		{
			bool flag = obj is T;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = obj is StructWrapper<T>;
				result = flag2;
			}
			return result;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001B96C File Offset: 0x00019B6C
		public static T DisconnectPooling<T>(this T table) where T : IEnumerable<object>
		{
			foreach (object obj in table)
			{
				StructWrapper structWrapper = obj as StructWrapper;
				bool flag = structWrapper == null;
				if (!flag)
				{
					structWrapper.DisconnectFromPool();
				}
			}
			return table;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001B9D8 File Offset: 0x00019BD8
		public static List<object> ReleaseAllWrappers(this List<object> collection)
		{
			foreach (object obj in collection)
			{
				StructWrapper structWrapper = obj as StructWrapper;
				bool flag = structWrapper == null;
				if (!flag)
				{
					structWrapper.Dispose();
				}
			}
			return collection;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001BA44 File Offset: 0x00019C44
		public static object[] ReleaseAllWrappers(this object[] collection)
		{
			foreach (object obj in collection)
			{
				StructWrapper structWrapper = obj as StructWrapper;
				bool flag = structWrapper == null;
				if (!flag)
				{
					structWrapper.Dispose();
				}
			}
			return collection;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001BA8C File Offset: 0x00019C8C
		public static Hashtable ReleaseAllWrappers(this Hashtable table)
		{
			foreach (object obj in table.Values)
			{
				StructWrapper structWrapper = obj as StructWrapper;
				bool flag = structWrapper == null;
				if (!flag)
				{
					structWrapper.Dispose();
				}
			}
			return table;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001BAFC File Offset: 0x00019CFC
		public static void BoxAll(this Hashtable table, bool recursive = false)
		{
			foreach (object obj in table.Values)
			{
				if (recursive)
				{
					Hashtable hashtable = obj as Hashtable;
					bool flag = hashtable != null;
					if (flag)
					{
						hashtable.BoxAll(false);
					}
				}
				StructWrapper structWrapper = obj as StructWrapper;
				bool flag2 = structWrapper == null;
				if (!flag2)
				{
					structWrapper.Box();
				}
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001BB8C File Offset: 0x00019D8C
		public static T Unwrap<T>(this object obj)
		{
			StructWrapper<T> structWrapper = obj as StructWrapper<T>;
			bool flag = structWrapper == null;
			T result;
			if (flag)
			{
				result = (T)((object)obj);
			}
			else
			{
				T value = structWrapper.value;
				bool flag2 = (structWrapper.pooling & Pooling.ReleaseOnUnwrap) == Pooling.ReleaseOnUnwrap;
				if (flag2)
				{
					structWrapper.Dispose();
				}
				result = structWrapper.value;
			}
			return result;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		public static T Get<T>(this object obj)
		{
			StructWrapper<T> structWrapper = obj as StructWrapper<T>;
			bool flag = structWrapper == null;
			T result;
			if (flag)
			{
				result = (T)((object)obj);
			}
			else
			{
				T value = structWrapper.value;
				result = value;
			}
			return result;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001BC14 File Offset: 0x00019E14
		public static T Unwrap<T>(this Hashtable table, object key)
		{
			object obj = table[key];
			return obj.Unwrap<T>();
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001BC34 File Offset: 0x00019E34
		public static bool TryUnwrapValue<T>(this Hashtable table, byte key, out T value) where T : new()
		{
			object obj;
			bool flag = table.TryGetValue(key, out obj);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				value = default(T);
				result = false;
			}
			else
			{
				value = obj.Unwrap<T>();
				result = true;
			}
			return result;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0001BC78 File Offset: 0x00019E78
		public static bool TryGetValue<T>(this Hashtable table, byte key, out T value) where T : new()
		{
			object obj;
			bool flag = table.TryGetValue(key, out obj);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				value = default(T);
				result = false;
			}
			else
			{
				value = obj.Get<T>();
				result = true;
			}
			return result;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0001BCBC File Offset: 0x00019EBC
		public static bool TryGetValue<T>(this Hashtable table, object key, out T value) where T : new()
		{
			object obj;
			bool flag = table.TryGetValue(key, out obj);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				value = default(T);
				result = false;
			}
			else
			{
				value = obj.Get<T>();
				result = true;
			}
			return result;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001BCFC File Offset: 0x00019EFC
		public static bool TryUnwrapValue<T>(this Hashtable table, object key, out T value) where T : new()
		{
			object obj;
			bool flag = table.TryGetValue(key, out obj);
			bool flag2 = !flag;
			bool result;
			if (flag2)
			{
				value = default(T);
				result = false;
			}
			else
			{
				value = obj.Unwrap<T>();
				result = true;
			}
			return result;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001BD3C File Offset: 0x00019F3C
		public static T Unwrap<T>(this Hashtable table, byte key)
		{
			object obj = table[key];
			return obj.Unwrap<T>();
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001BD5C File Offset: 0x00019F5C
		public static T Get<T>(this Hashtable table, byte key)
		{
			object obj = table[key];
			return obj.Get<T>();
		}
	}
}
