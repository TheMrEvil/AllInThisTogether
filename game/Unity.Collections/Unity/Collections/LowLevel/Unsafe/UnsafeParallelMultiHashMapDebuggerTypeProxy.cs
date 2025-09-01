using System;
using System.Collections.Generic;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000145 RID: 325
	internal sealed class UnsafeParallelMultiHashMapDebuggerTypeProxy<TKey, TValue> where TKey : struct, IEquatable<TKey>, IComparable<TKey> where TValue : struct
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x00023219 File Offset: 0x00021419
		public UnsafeParallelMultiHashMapDebuggerTypeProxy(UnsafeParallelMultiHashMap<TKey, TValue> target)
		{
			this.m_Target = target;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00023228 File Offset: 0x00021428
		public static ValueTuple<NativeArray<TKey>, int> GetUniqueKeyArray(ref UnsafeParallelMultiHashMap<TKey, TValue> hashMap, AllocatorManager.AllocatorHandle allocator)
		{
			NativeArray<TKey> keyArray = hashMap.GetKeyArray(allocator);
			keyArray.Sort<TKey>();
			int item = keyArray.Unique<TKey>();
			return new ValueTuple<NativeArray<TKey>, int>(keyArray, item);
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00023250 File Offset: 0x00021450
		public List<ListPair<TKey, List<TValue>>> Items
		{
			get
			{
				List<ListPair<TKey, List<TValue>>> list = new List<ListPair<TKey, List<TValue>>>();
				ValueTuple<NativeArray<TKey>, int> uniqueKeyArray = UnsafeParallelMultiHashMapDebuggerTypeProxy<TKey, TValue>.GetUniqueKeyArray(ref this.m_Target, Allocator.Temp);
				using (uniqueKeyArray.Item1)
				{
					for (int i = 0; i < uniqueKeyArray.Item2; i++)
					{
						List<TValue> list2 = new List<TValue>();
						TValue item2;
						NativeParallelMultiHashMapIterator<TKey> nativeParallelMultiHashMapIterator;
						if (this.m_Target.TryGetFirstValue(uniqueKeyArray.Item1[i], out item2, out nativeParallelMultiHashMapIterator))
						{
							do
							{
								list2.Add(item2);
							}
							while (this.m_Target.TryGetNextValue(out item2, ref nativeParallelMultiHashMapIterator));
						}
						list.Add(new ListPair<TKey, List<TValue>>(uniqueKeyArray.Item1[i], list2));
					}
				}
				return list;
			}
		}

		// Token: 0x040003C9 RID: 969
		private UnsafeParallelMultiHashMap<TKey, TValue> m_Target;
	}
}
