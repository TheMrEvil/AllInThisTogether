using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Jobs;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000FF RID: 255
	public static class UnsafeListExtension
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x0001D4DC File Offset: 0x0001B6DC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		internal static ref UnsafeList ListData<[IsUnmanaged] T>(this UnsafeList<T> from) where T : struct, ValueType
		{
			return UnsafeUtility.As<UnsafeList<T>, UnsafeList>(ref from);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001D4E4 File Offset: 0x0001B6E4
		public static void Sort<[IsUnmanaged] T>(this UnsafeList list) where T : struct, ValueType, IComparable<T>
		{
			list.Sort(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001D500 File Offset: 0x0001B700
		public static void Sort<[IsUnmanaged] T, U>(this UnsafeList list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>(list.Ptr, list.Length, comp);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001D514 File Offset: 0x0001B714
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this UnsafeList).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public static JobHandle Sort<[IsUnmanaged] T>(this UnsafeList container, JobHandle inputDeps) where T : struct, ValueType, IComparable<T>
		{
			return container.Sort(default(NativeSortExtension.DefaultComparer<T>), inputDeps);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001D534 File Offset: 0x0001B734
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[IsUnmanaged] T>(this UnsafeList list) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>((T*)list.Ptr, list.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001D55B File Offset: 0x0001B75B
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this UnsafeList, U).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public unsafe static JobHandle Sort<[IsUnmanaged] T, U>(this UnsafeList container, U comp, JobHandle inputDeps) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.Sort<T, U>((T*)container.Ptr, container.Length, comp, inputDeps);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001D570 File Offset: 0x0001B770
		public unsafe static SortJob<T, U> SortJob<[IsUnmanaged] T, U>(this UnsafeList list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.SortJob<T, U>((T*)list.Ptr, list.Length, comp);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0001D584 File Offset: 0x0001B784
		public static int BinarySearch<[IsUnmanaged] T>(this UnsafeList container, T value) where T : struct, ValueType, IComparable<T>
		{
			return container.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0001D5A1 File Offset: 0x0001B7A1
		public unsafe static int BinarySearch<[IsUnmanaged] T, U>(this UnsafeList container, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>((T*)container.Ptr, container.Length, value, comp);
		}
	}
}
