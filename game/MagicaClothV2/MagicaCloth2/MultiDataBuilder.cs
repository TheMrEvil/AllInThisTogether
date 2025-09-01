using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace MagicaCloth2
{
	// Token: 0x020000C2 RID: 194
	public class MultiDataBuilder<[IsUnmanaged] T> : IDisposable where T : struct, ValueType
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0001D11E File Offset: 0x0001B31E
		public MultiDataBuilder(int indexCount, int dataCapacity)
		{
			this.indexCount = indexCount;
			this.Map = new NativeParallelMultiHashMap<int, T>(dataCapacity, Allocator.Persistent);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0001D13F File Offset: 0x0001B33F
		public void Dispose()
		{
			if (this.Map.IsCreated)
			{
				this.Map.Dispose();
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001D159 File Offset: 0x0001B359
		public int Count()
		{
			return this.Map.Count();
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001D166 File Offset: 0x0001B366
		public int GetDataCount(int index)
		{
			if (!this.Map.ContainsKey(index))
			{
				return 0;
			}
			return this.Map.CountValuesForKey(index);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001D184 File Offset: 0x0001B384
		public void Add(int key, T data)
		{
			this.Map.Add(key, data);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001D193 File Offset: 0x0001B393
		public int CountValuesForKey(int key)
		{
			return this.Map.CountValuesForKey(key);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0001D1A4 File Offset: 0x0001B3A4
		public ValueTuple<T[], uint[]> ToArray()
		{
			if (!this.Map.IsCreated || this.indexCount == 0)
			{
				return new ValueTuple<T[], uint[]>(null, null);
			}
			uint[] array = new uint[this.indexCount];
			List<T> list = new List<T>(this.Map.Capacity);
			for (int i = 0; i < this.indexCount; i++)
			{
				int count = list.Count;
				int num = 0;
				if (this.Map.ContainsKey(i))
				{
					foreach (T item in this.Map.GetValuesForKey(i))
					{
						list.Add(item);
						num++;
					}
				}
				array[i] = DataUtility.Pack10_22(num, count);
			}
			return new ValueTuple<T[], uint[]>(list.ToArray(), array);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0001D284 File Offset: 0x0001B484
		public uint[] ToIndexArray()
		{
			return this.ToArray().Item2;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001D294 File Offset: 0x0001B494
		public void ToNativeArray(out NativeArray<uint> indexArray, out NativeArray<T> dataArray)
		{
			indexArray = new NativeArray<uint>(this.indexCount, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			List<T> list = new List<T>(this.Map.Capacity);
			for (int i = 0; i < this.indexCount; i++)
			{
				int count = list.Count;
				int num = 0;
				if (this.Map.ContainsKey(i))
				{
					foreach (T item in this.Map.GetValuesForKey(i))
					{
						list.Add(item);
						num++;
					}
				}
				indexArray[i] = DataUtility.Pack10_22(num, count);
			}
			dataArray = new NativeArray<T>(list.ToArray(), Allocator.Persistent);
		}

		// Token: 0x040005E6 RID: 1510
		private int indexCount;

		// Token: 0x040005E7 RID: 1511
		public NativeParallelMultiHashMap<int, T> Map;
	}
}
