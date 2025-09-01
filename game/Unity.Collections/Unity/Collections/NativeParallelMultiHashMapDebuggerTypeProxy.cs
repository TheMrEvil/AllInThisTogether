using System;
using System.Collections.Generic;

namespace Unity.Collections
{
	// Token: 0x020000C7 RID: 199
	internal sealed class NativeParallelMultiHashMapDebuggerTypeProxy<TKey, TValue> where TKey : struct, IEquatable<TKey>, IComparable<TKey> where TValue : struct
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x00017333 File Offset: 0x00015533
		public NativeParallelMultiHashMapDebuggerTypeProxy(NativeParallelMultiHashMap<TKey, TValue> target)
		{
			this.m_Target = target;
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x00017344 File Offset: 0x00015544
		public List<ListPair<TKey, List<TValue>>> Items
		{
			get
			{
				List<ListPair<TKey, List<TValue>>> list = new List<ListPair<TKey, List<TValue>>>();
				ValueTuple<NativeArray<TKey>, int> uniqueKeyArray = this.m_Target.GetUniqueKeyArray(Allocator.Temp);
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

		// Token: 0x0400029A RID: 666
		private NativeParallelMultiHashMap<TKey, TValue> m_Target;
	}
}
