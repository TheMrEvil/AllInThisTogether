using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace MagicaCloth2
{
	// Token: 0x020000F1 RID: 241
	internal static class NativeArrayExtensions
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00022600 File Offset: 0x00020800
		public static void DisposeSafe<[IsUnmanaged] T>(this NativeArray<T> array) where T : struct, ValueType
		{
			if (array.IsCreated)
			{
				array.Dispose();
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00022610 File Offset: 0x00020810
		public static void Resize<[IsUnmanaged] T>(this NativeArray<T> array, int size, Allocator allocator = Allocator.Persistent, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct, ValueType
		{
			if (!array.IsCreated || array.Length < size)
			{
				ref array.DisposeSafe<T>();
				array = new NativeArray<T>(size, allocator, options);
			}
		}
	}
}
