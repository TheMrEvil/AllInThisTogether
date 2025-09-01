using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000BB RID: 187
	internal sealed class NativeParallelHashMapDebuggerTypeProxy<TKey, TValue> where TKey : struct, IEquatable<TKey> where TValue : struct
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0001633E File Offset: 0x0001453E
		public NativeParallelHashMapDebuggerTypeProxy(NativeParallelHashMap<TKey, TValue> target)
		{
			this.m_Target = target.m_HashMapData;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x00016354 File Offset: 0x00014554
		public List<Pair<TKey, TValue>> Items
		{
			get
			{
				List<Pair<TKey, TValue>> list = new List<Pair<TKey, TValue>>();
				using (NativeKeyValueArrays<TKey, TValue> keyValueArrays = this.m_Target.GetKeyValueArrays(Allocator.Temp))
				{
					for (int i = 0; i < keyValueArrays.Length; i++)
					{
						List<Pair<TKey, TValue>> list2 = list;
						NativeArray<TKey> keys = keyValueArrays.Keys;
						TKey k = keys[i];
						NativeArray<TValue> values = keyValueArrays.Values;
						list2.Add(new Pair<TKey, TValue>(k, values[i]));
					}
				}
				return list;
			}
		}

		// Token: 0x0400028A RID: 650
		private UnsafeParallelHashMap<TKey, TValue> m_Target;
	}
}
