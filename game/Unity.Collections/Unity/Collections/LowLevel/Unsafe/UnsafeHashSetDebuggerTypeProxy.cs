using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200013F RID: 319
	internal sealed class UnsafeHashSetDebuggerTypeProxy<[IsUnmanaged] T> where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x00021FE9 File Offset: 0x000201E9
		public UnsafeHashSetDebuggerTypeProxy(UnsafeParallelHashSet<T> data)
		{
			this.Data = data;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00021FF8 File Offset: 0x000201F8
		public List<T> Items
		{
			get
			{
				List<T> list = new List<T>();
				using (NativeArray<T> nativeArray = this.Data.ToNativeArray(Allocator.Temp))
				{
					for (int i = 0; i < nativeArray.Length; i++)
					{
						list.Add(nativeArray[i]);
					}
				}
				return list;
			}
		}

		// Token: 0x040003BE RID: 958
		private UnsafeParallelHashSet<T> Data;
	}
}
