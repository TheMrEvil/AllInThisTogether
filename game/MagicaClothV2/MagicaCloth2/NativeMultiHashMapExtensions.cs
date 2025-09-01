using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace MagicaCloth2
{
	// Token: 0x020000F2 RID: 242
	internal static class NativeMultiHashMapExtensions
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00022638 File Offset: 0x00020838
		public static bool Contains<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> map, TKey key, TValue value) where TKey : struct, IEquatable<TKey> where TValue : struct, IEquatable<TValue>
		{
			foreach (TValue tvalue in map.GetValuesForKey(key))
			{
				if (tvalue.Equals(value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000226A0 File Offset: 0x000208A0
		public static void UniqueAdd<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> map, TKey key, TValue value) where TKey : struct, IEquatable<TKey> where TValue : struct, IEquatable<TValue>
		{
			if (!ref map.Contains(key, value))
			{
				map.Add(key, value);
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000226B4 File Offset: 0x000208B4
		public static FixedList512Bytes<TValue> ToFixedList512Bytes<TKey, [IsUnmanaged] TValue>(this NativeParallelMultiHashMap<TKey, TValue> map, TKey key) where TKey : struct, IEquatable<TKey> where TValue : struct, ValueType, IEquatable<TValue>
		{
			FixedList512Bytes<TValue> result = default(FixedList512Bytes<TValue>);
			if (map.ContainsKey(key))
			{
				foreach (TValue tvalue in map.GetValuesForKey(key))
				{
					result.Add(tvalue);
				}
			}
			return result;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00022720 File Offset: 0x00020920
		public static FixedList128Bytes<TValue> ToFixedList128Bytes<TKey, [IsUnmanaged] TValue>(this NativeParallelMultiHashMap<TKey, TValue> map, TKey key) where TKey : struct, IEquatable<TKey> where TValue : struct, ValueType, IEquatable<TValue>
		{
			FixedList128Bytes<TValue> result = default(FixedList128Bytes<TValue>);
			if (map.ContainsKey(key))
			{
				foreach (TValue tvalue in map.GetValuesForKey(key))
				{
					result.Add(tvalue);
				}
			}
			return result;
		}
	}
}
