using System;
using Unity.Collections;
using UnityEngine.Jobs;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009A RID: 154
	public static class ArrayExtensions
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x0001780C File Offset: 0x00015A0C
		public static void ResizeArray<T>(this NativeArray<T> array, int capacity) where T : struct
		{
			NativeArray<T> nativeArray = new NativeArray<T>(capacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
			if (array.IsCreated)
			{
				NativeArray<T>.Copy(array, nativeArray, array.Length);
				array.Dispose();
			}
			array = nativeArray;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001784C File Offset: 0x00015A4C
		public static void ResizeArray(this TransformAccessArray array, int capacity)
		{
			TransformAccessArray transformAccessArray = new TransformAccessArray(capacity, -1);
			if (array.isCreated)
			{
				for (int i = 0; i < array.length; i++)
				{
					transformAccessArray.Add(array[i]);
				}
				array.Dispose();
			}
			array = transformAccessArray;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00017896 File Offset: 0x00015A96
		public static void ResizeArray<T>(ref T[] array, int capacity)
		{
			if (array == null)
			{
				array = new T[capacity];
				return;
			}
			Array.Resize<T>(ref array, capacity);
		}
	}
}
