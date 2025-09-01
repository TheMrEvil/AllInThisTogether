using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections.NotBurstCompatible
{
	// Token: 0x020000FB RID: 251
	public static class Extensions
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		[NotBurstCompatible]
		public static T[] ToArray<[IsUnmanaged] T>(this NativeParallelHashSet<T> set) where T : struct, ValueType, IEquatable<T>
		{
			NativeArray<T> nativeArray = set.ToNativeArray(Allocator.TempJob);
			T[] result = nativeArray.ToArray();
			nativeArray.Dispose();
			return result;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001CA88 File Offset: 0x0001AC88
		[NotBurstCompatible]
		public static T[] ToArrayNBC<[IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType
		{
			return list.AsArray().ToArray();
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001CAA4 File Offset: 0x0001ACA4
		[NotBurstCompatible]
		public static void CopyFromNBC<[IsUnmanaged] T>(this NativeList<T> list, T[] array) where T : struct, ValueType
		{
			list.Clear();
			list.Resize(array.Length, NativeArrayOptions.UninitializedMemory);
			list.AsArray().CopyFrom(array);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0001CAD3 File Offset: 0x0001ACD3
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[Obsolete("Burst now supports tuple, please use `GetUniqueKeyArray` method from `Unity.Collections.UnsafeParallelMultiHashMap` instead.", false)]
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArrayNBC<TKey, TValue>(this UnsafeParallelMultiHashMap<TKey, TValue> hashmap, AllocatorManager.AllocatorHandle allocator) where TKey : struct, IEquatable<TKey>, IComparable<TKey> where TValue : struct
		{
			return hashmap.GetUniqueKeyArray(allocator);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0001CADC File Offset: 0x0001ACDC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		[Obsolete("Burst now supports tuple, please use `GetUniqueKeyArray` method from `Unity.Collections.NativeParallelMultiHashMap` instead.", false)]
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArrayNBC<TKey, TValue>(this NativeParallelMultiHashMap<TKey, TValue> hashmap, AllocatorManager.AllocatorHandle allocator) where TKey : struct, IEquatable<TKey>, IComparable<TKey> where TValue : struct
		{
			return hashmap.GetUniqueKeyArray(allocator);
		}
	}
}
