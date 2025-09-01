using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000C0 RID: 192
	internal sealed class NativeHashSetDebuggerTypeProxy<[IsUnmanaged] T> where T : struct, ValueType, IEquatable<T>
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x0001661A File Offset: 0x0001481A
		public NativeHashSetDebuggerTypeProxy(NativeParallelHashSet<T> data)
		{
			this.Data = data;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001662C File Offset: 0x0001482C
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

		// Token: 0x0400028E RID: 654
		private NativeParallelHashSet<T> Data;
	}
}
