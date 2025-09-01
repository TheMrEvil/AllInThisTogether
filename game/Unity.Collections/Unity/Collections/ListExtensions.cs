using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000A8 RID: 168
	public static class ListExtensions
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x00015328 File Offset: 0x00013528
		public static bool RemoveSwapBack<T>(this List<T> list, T value)
		{
			int num = list.IndexOf(value);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAtSwapBack(num);
			return true;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001534C File Offset: 0x0001354C
		public static bool RemoveSwapBack<T>(this List<T> list, Predicate<T> matcher)
		{
			int num = list.FindIndex(matcher);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAtSwapBack(num);
			return true;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00015370 File Offset: 0x00013570
		public static void RemoveAtSwapBack<T>(this List<T> list, int index)
		{
			int index2 = list.Count - 1;
			list[index] = list[index2];
			list.RemoveAt(index2);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001539C File Offset: 0x0001359C
		public static NativeList<T> ToNativeList<[IsUnmanaged] T>(this List<T> list, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeList<T> result = new NativeList<T>(list.Count, allocator);
			for (int i = 0; i < list.Count; i++)
			{
				result.AddNoResize(list[i]);
			}
			return result;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x000153D8 File Offset: 0x000135D8
		public static NativeArray<T> ToNativeArray<[IsUnmanaged] T>(this List<T> list, AllocatorManager.AllocatorHandle allocator) where T : struct, ValueType
		{
			NativeArray<T> result = CollectionHelper.CreateNativeArray<T>(list.Count, allocator, NativeArrayOptions.ClearMemory);
			for (int i = 0; i < list.Count; i++)
			{
				result[i] = list[i];
			}
			return result;
		}
	}
}
