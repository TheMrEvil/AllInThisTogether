using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000C8 RID: 200
	[BurstCompatible]
	public static class NativeParallelMultiHashMapExtensions
	{
		// Token: 0x0600079A RID: 1946 RVA: 0x000173FC File Offset: 0x000155FC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal static void Initialize<TKey, TValue, [IsUnmanaged] U>(this NativeParallelMultiHashMap<TKey, TValue> container, int capacity, ref U allocator, int disposeSentinelStackDepth = 2) where TKey : struct, IEquatable<TKey> where TValue : struct where U : struct, ValueType, AllocatorManager.IAllocator
		{
			container.m_MultiHashMapData = new UnsafeParallelMultiHashMap<TKey, TValue>(capacity, allocator.Handle);
		}
	}
}
