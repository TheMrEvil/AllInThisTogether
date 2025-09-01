using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Collections
{
	// Token: 0x020000D5 RID: 213
	[BurstCompatible]
	public static class NativeSortExtension
	{
		// Token: 0x060007D4 RID: 2004 RVA: 0x00017F04 File Offset: 0x00016104
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void Sort<[IsUnmanaged] T>(T* array, int length) where T : struct, ValueType, IComparable<T>
		{
			NativeSortExtension.IntroSort<T, NativeSortExtension.DefaultComparer<T>>((void*)array, length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00017F21 File Offset: 0x00016121
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[IsUnmanaged] T, U>(T* array, int length, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>((void*)array, length, comp);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00017F2C File Offset: 0x0001612C
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(T*, int).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public unsafe static JobHandle Sort<[IsUnmanaged] T>(T* array, int length, JobHandle inputDeps) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.Sort<T, NativeSortExtension.DefaultComparer<T>>(array, length, default(NativeSortExtension.DefaultComparer<T>), inputDeps);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00017F4C File Offset: 0x0001614C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[IsUnmanaged] T>(T* array, int length) where T : struct, ValueType, IComparable<T>
		{
			return new SortJob<T, NativeSortExtension.DefaultComparer<T>>
			{
				Data = array,
				Length = length,
				Comp = default(NativeSortExtension.DefaultComparer<T>)
			};
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00017F80 File Offset: 0x00016180
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(T*, int, U).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public unsafe static JobHandle Sort<[IsUnmanaged] T, U>(T* array, int length, U comp, JobHandle inputDeps) where T : struct, ValueType where U : IComparer<T>
		{
			if (length == 0)
			{
				return inputDeps;
			}
			int num = (length + 1023) / 1024;
			int num2 = math.max(1, 128);
			int innerloopBatchCount = num / num2;
			JobHandle dependsOn = new NativeSortExtension.SegmentSort<T, U>
			{
				Data = array,
				Comp = comp,
				Length = length,
				SegmentWidth = 1024
			}.Schedule(num, innerloopBatchCount, inputDeps);
			return new NativeSortExtension.SegmentSortMerge<T, U>
			{
				Data = array,
				Comp = comp,
				Length = length,
				SegmentWidth = 1024
			}.Schedule(dependsOn);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001801C File Offset: 0x0001621C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public unsafe static SortJob<T, U> SortJob<[IsUnmanaged] T, U>(T* array, int length, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return new SortJob<T, U>
			{
				Data = array,
				Length = length,
				Comp = comp
			};
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001804C File Offset: 0x0001624C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static int BinarySearch<[IsUnmanaged] T>(T* ptr, int length, T value) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.BinarySearch<T, NativeSortExtension.DefaultComparer<T>>(ptr, length, value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001806C File Offset: 0x0001626C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<[IsUnmanaged] T, U>(T* ptr, int length, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int num = 0;
			for (int num2 = length; num2 != 0; num2 >>= 1)
			{
				int num3 = num + (num2 >> 1);
				T y = ptr[(IntPtr)num3 * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
				int num4 = comp.Compare(value, y);
				if (num4 == 0)
				{
					return num3;
				}
				if (num4 > 0)
				{
					num = num3 + 1;
					num2--;
				}
			}
			return ~num;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000180C4 File Offset: 0x000162C4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static void Sort<T>(this NativeArray<T> array) where T : struct, IComparable<T>
		{
			NativeSortExtension.IntroSortStruct<T, NativeSortExtension.DefaultComparer<T>>(array.GetUnsafePtr<T>(), array.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000180EC File Offset: 0x000162EC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static void Sort<T, U>(this NativeArray<T> array, U comp) where T : struct where U : IComparer<T>
		{
			NativeSortExtension.IntroSortStruct<T, U>(array.GetUnsafePtr<T>(), array.Length, comp);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00018104 File Offset: 0x00016304
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this NativeArray<T>).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public unsafe static JobHandle Sort<[IsUnmanaged] T>(this NativeArray<T> array, JobHandle inputDeps) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.Sort<T, NativeSortExtension.DefaultComparer<T>>((T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array), array.Length, default(NativeSortExtension.DefaultComparer<T>), inputDeps);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00018130 File Offset: 0x00016330
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[IsUnmanaged] T>(this NativeArray<T> array) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>((T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array), array.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00018158 File Offset: 0x00016358
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this NativeArray<T>, U).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public unsafe static JobHandle Sort<[IsUnmanaged] T, U>(this NativeArray<T> array, U comp, JobHandle inputDeps) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.Sort<T, U>((T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array), array.Length, comp, inputDeps);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00018170 File Offset: 0x00016370
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public unsafe static SortJob<T, U> SortJob<[IsUnmanaged] T, U>(this NativeArray<T> array, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return new SortJob<T, U>
			{
				Data = (T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array),
				Length = array.Length,
				Comp = comp
			};
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000181AC File Offset: 0x000163AC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static int BinarySearch<[IsUnmanaged] T>(this NativeArray<T> array, T value) where T : struct, ValueType, IComparable<T>
		{
			return array.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000181C9 File Offset: 0x000163C9
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<[IsUnmanaged] T, U>(this NativeArray<T> array, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>((T*)NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array), array.Length, value, comp);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000181E0 File Offset: 0x000163E0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static void Sort<[IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			list.Sort(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x000181FC File Offset: 0x000163FC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static void Sort<[IsUnmanaged] T, U>(this NativeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>(list.GetUnsafePtr<T>(), list.Length, comp);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00018214 File Offset: 0x00016414
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this NativeList<T>).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public static JobHandle Sort<[IsUnmanaged] T>(this NativeList<T> array, JobHandle inputDeps) where T : struct, ValueType, IComparable<T>
		{
			return array.Sort(default(NativeSortExtension.DefaultComparer<T>), inputDeps);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00018234 File Offset: 0x00016434
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[IsUnmanaged] T>(this NativeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>((T*)list.GetUnsafePtr<T>(), list.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001825C File Offset: 0x0001645C
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this NativeList<T>, U).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public unsafe static JobHandle Sort<[IsUnmanaged] T, U>(this NativeList<T> list, U comp, JobHandle inputDeps) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.Sort<T, U>((T*)list.GetUnsafePtr<T>(), list.Length, comp, inputDeps);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00018272 File Offset: 0x00016472
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public unsafe static SortJob<T, U> SortJob<[IsUnmanaged] T, U>(this NativeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.SortJob<T, U>((T*)list.GetUnsafePtr<T>(), list.Length, comp);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00018288 File Offset: 0x00016488
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static int BinarySearch<[IsUnmanaged] T>(this NativeList<T> list, T value) where T : struct, ValueType, IComparable<T>
		{
			return list.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000182A5 File Offset: 0x000164A5
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<[IsUnmanaged] T, U>(this NativeList<T> list, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>((T*)list.GetUnsafePtr<T>(), list.Length, value, comp);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000182BC File Offset: 0x000164BC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static void Sort<[IsUnmanaged] T>(this UnsafeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			list.Sort(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000182D8 File Offset: 0x000164D8
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[IsUnmanaged] T, U>(this UnsafeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>((void*)list.Ptr, list.Length, comp);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000182F0 File Offset: 0x000164F0
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this UnsafeList<T>).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public static JobHandle Sort<[IsUnmanaged] T>(this UnsafeList<T> list, JobHandle inputDeps) where T : struct, ValueType, IComparable<T>
		{
			return list.Sort(default(NativeSortExtension.DefaultComparer<T>), inputDeps);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00018310 File Offset: 0x00016510
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[IsUnmanaged] T>(this UnsafeList<T> list) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>(list.Ptr, list.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00018338 File Offset: 0x00016538
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this UnsafeList<T>, U).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public static JobHandle Sort<[IsUnmanaged] T, U>(this UnsafeList<T> list, U comp, JobHandle inputDeps) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.Sort<T, U>(list.Ptr, list.Length, comp, inputDeps);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001834E File Offset: 0x0001654E
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public static SortJob<T, U> SortJob<[IsUnmanaged] T, U>(this UnsafeList<T> list, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.SortJob<T, U>(list.Ptr, list.Length, comp);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00018364 File Offset: 0x00016564
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static int BinarySearch<[IsUnmanaged] T>(this UnsafeList<T> list, T value) where T : struct, ValueType, IComparable<T>
		{
			return list.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00018381 File Offset: 0x00016581
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static int BinarySearch<[IsUnmanaged] T, U>(this UnsafeList<T> list, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>(list.Ptr, list.Length, value, comp);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00018398 File Offset: 0x00016598
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static void Sort<T>(this NativeSlice<T> slice) where T : struct, IComparable<T>
		{
			slice.Sort(default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000183B4 File Offset: 0x000165B4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public static void Sort<T, U>(this NativeSlice<T> slice, U comp) where T : struct where U : IComparer<T>
		{
			NativeSortExtension.IntroSortStruct<T, U>(slice.GetUnsafePtr<T>(), slice.Length, comp);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000183CC File Offset: 0x000165CC
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this NativeSlice<T>).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public static JobHandle Sort<[IsUnmanaged] T>(this NativeSlice<T> slice, JobHandle inputDeps) where T : struct, ValueType, IComparable<T>
		{
			return slice.Sort(default(NativeSortExtension.DefaultComparer<T>), inputDeps);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000183EC File Offset: 0x000165EC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public unsafe static SortJob<T, NativeSortExtension.DefaultComparer<T>> SortJob<[IsUnmanaged] T>(this NativeSlice<T> slice) where T : struct, ValueType, IComparable<T>
		{
			return NativeSortExtension.SortJob<T, NativeSortExtension.DefaultComparer<T>>((T*)slice.GetUnsafePtr<T>(), slice.Length, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00018414 File Offset: 0x00016614
		[NotBurstCompatible]
		[Obsolete("Instead call SortJob(this NativeSlice<T>, U).Schedule(JobHandle). (RemovedAfter 2021-06-20)", false)]
		public unsafe static JobHandle Sort<[IsUnmanaged] T, U>(this NativeSlice<T> slice, U comp, JobHandle inputDeps) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.Sort<T, U>((T*)slice.GetUnsafePtr<T>(), slice.Length, comp, inputDeps);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001842A File Offset: 0x0001662A
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		}, RequiredUnityDefine = "UNITY_2020_2_OR_NEWER")]
		public unsafe static SortJob<T, U> SortJob<[IsUnmanaged] T, U>(this NativeSlice<T> slice, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.SortJob<T, U>((T*)slice.GetUnsafePtr<T>(), slice.Length, comp);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00018440 File Offset: 0x00016640
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static int BinarySearch<[IsUnmanaged] T>(this NativeSlice<T> slice, T value) where T : struct, ValueType, IComparable<T>
		{
			return slice.BinarySearch(value, default(NativeSortExtension.DefaultComparer<T>));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001845D File Offset: 0x0001665D
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static int BinarySearch<[IsUnmanaged] T, U>(this NativeSlice<T> slice, T value, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			return NativeSortExtension.BinarySearch<T, U>((T*)slice.GetUnsafePtr<T>(), slice.Length, value, comp);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00018473 File Offset: 0x00016673
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		internal unsafe static void IntroSort<[IsUnmanaged] T, U>(void* array, int length, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			NativeSortExtension.IntroSort<T, U>(array, 0, length - 1, 2 * CollectionHelper.Log2Floor(length), comp);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00018488 File Offset: 0x00016688
		private unsafe static void IntroSort<[IsUnmanaged] T, U>(void* array, int lo, int hi, int depth, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			while (hi > lo)
			{
				int num = hi - lo + 1;
				if (num <= 16)
				{
					if (num == 1)
					{
						return;
					}
					if (num == 2)
					{
						NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
						return;
					}
					if (num == 3)
					{
						NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, hi - 1, comp);
						NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
						NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, hi - 1, hi, comp);
						return;
					}
					NativeSortExtension.InsertionSort<T, U>(array, lo, hi, comp);
					return;
				}
				else
				{
					if (depth == 0)
					{
						NativeSortExtension.HeapSort<T, U>(array, lo, hi, comp);
						return;
					}
					depth--;
					int num2 = NativeSortExtension.Partition<T, U>(array, lo, hi, comp);
					NativeSortExtension.IntroSort<T, U>(array, num2 + 1, hi, depth, comp);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00018524 File Offset: 0x00016724
		private unsafe static void InsertionSort<[IsUnmanaged] T, U>(void* array, int lo, int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T t = UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (num >= lo && comp.Compare(t, UnsafeUtility.ReadArrayElement<T>(array, num)) < 0)
				{
					UnsafeUtility.WriteArrayElement<T>(array, num + 1, UnsafeUtility.ReadArrayElement<T>(array, num));
					num--;
				}
				UnsafeUtility.WriteArrayElement<T>(array, num + 1, t);
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00018588 File Offset: 0x00016788
		private unsafe static int Partition<[IsUnmanaged] T, U>(void* array, int lo, int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int num = lo + (hi - lo) / 2;
			NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, num, comp);
			NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, lo, hi, comp);
			NativeSortExtension.SwapIfGreaterWithItems<T, U>(array, num, hi, comp);
			T x = UnsafeUtility.ReadArrayElement<T>(array, num);
			NativeSortExtension.Swap<T>(array, num, hi - 1);
			int i = lo;
			int num2 = hi - 1;
			while (i < num2)
			{
				while (comp.Compare(x, UnsafeUtility.ReadArrayElement<T>(array, ++i)) > 0)
				{
				}
				while (comp.Compare(x, UnsafeUtility.ReadArrayElement<T>(array, --num2)) < 0)
				{
				}
				if (i >= num2)
				{
					break;
				}
				NativeSortExtension.Swap<T>(array, i, num2);
			}
			NativeSortExtension.Swap<T>(array, i, hi - 1);
			return i;
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00018628 File Offset: 0x00016828
		private unsafe static void HeapSort<[IsUnmanaged] T, U>(void* array, int lo, int hi, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				NativeSortExtension.Heapify<T, U>(array, i, num, lo, comp);
			}
			for (int j = num; j > 1; j--)
			{
				NativeSortExtension.Swap<T>(array, lo, lo + j - 1);
				NativeSortExtension.Heapify<T, U>(array, 1, j - 1, lo, comp);
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00018678 File Offset: 0x00016878
		private unsafe static void Heapify<[IsUnmanaged] T, U>(void* array, int i, int n, int lo, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			T t = UnsafeUtility.ReadArrayElement<T>(array, lo + i - 1);
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1), UnsafeUtility.ReadArrayElement<T>(array, lo + num)) < 0)
				{
					num++;
				}
				if (comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1), t) < 0)
				{
					break;
				}
				UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1));
				i = num;
			}
			UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, t);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001870C File Offset: 0x0001690C
		private unsafe static void Swap<[IsUnmanaged] T>(void* array, int lhs, int rhs) where T : struct, ValueType
		{
			T value = UnsafeUtility.ReadArrayElement<T>(array, lhs);
			UnsafeUtility.WriteArrayElement<T>(array, lhs, UnsafeUtility.ReadArrayElement<T>(array, rhs));
			UnsafeUtility.WriteArrayElement<T>(array, rhs, value);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00018737 File Offset: 0x00016937
		private unsafe static void SwapIfGreaterWithItems<[IsUnmanaged] T, U>(void* array, int lhs, int rhs, U comp) where T : struct, ValueType where U : IComparer<T>
		{
			if (lhs != rhs && comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lhs), UnsafeUtility.ReadArrayElement<T>(array, rhs)) > 0)
			{
				NativeSortExtension.Swap<T>(array, lhs, rhs);
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00018763 File Offset: 0x00016963
		private unsafe static void IntroSortStruct<T, U>(void* array, int length, U comp) where T : struct where U : IComparer<T>
		{
			NativeSortExtension.IntroSortStruct<T, U>(array, 0, length - 1, 2 * CollectionHelper.Log2Floor(length), comp);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00018778 File Offset: 0x00016978
		private unsafe static void IntroSortStruct<T, U>(void* array, int lo, int hi, int depth, U comp) where T : struct where U : IComparer<T>
		{
			while (hi > lo)
			{
				int num = hi - lo + 1;
				if (num <= 16)
				{
					if (num == 1)
					{
						return;
					}
					if (num == 2)
					{
						NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
						return;
					}
					if (num == 3)
					{
						NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi - 1, comp);
						NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
						NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, hi - 1, hi, comp);
						return;
					}
					NativeSortExtension.InsertionSortStruct<T, U>(array, lo, hi, comp);
					return;
				}
				else
				{
					if (depth == 0)
					{
						NativeSortExtension.HeapSortStruct<T, U>(array, lo, hi, comp);
						return;
					}
					depth--;
					int num2 = NativeSortExtension.PartitionStruct<T, U>(array, lo, hi, comp);
					NativeSortExtension.IntroSortStruct<T, U>(array, num2 + 1, hi, depth, comp);
					hi = num2 - 1;
				}
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00018814 File Offset: 0x00016A14
		private unsafe static void InsertionSortStruct<T, U>(void* array, int lo, int hi, U comp) where T : struct where U : IComparer<T>
		{
			for (int i = lo; i < hi; i++)
			{
				int num = i;
				T t = UnsafeUtility.ReadArrayElement<T>(array, i + 1);
				while (num >= lo && comp.Compare(t, UnsafeUtility.ReadArrayElement<T>(array, num)) < 0)
				{
					UnsafeUtility.WriteArrayElement<T>(array, num + 1, UnsafeUtility.ReadArrayElement<T>(array, num));
					num--;
				}
				UnsafeUtility.WriteArrayElement<T>(array, num + 1, t);
			}
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00018878 File Offset: 0x00016A78
		private unsafe static int PartitionStruct<T, U>(void* array, int lo, int hi, U comp) where T : struct where U : IComparer<T>
		{
			int num = lo + (hi - lo) / 2;
			NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, num, comp);
			NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, lo, hi, comp);
			NativeSortExtension.SwapIfGreaterWithItemsStruct<T, U>(array, num, hi, comp);
			T x = UnsafeUtility.ReadArrayElement<T>(array, num);
			NativeSortExtension.SwapStruct<T>(array, num, hi - 1);
			int i = lo;
			int num2 = hi - 1;
			while (i < num2)
			{
				while (comp.Compare(x, UnsafeUtility.ReadArrayElement<T>(array, ++i)) > 0)
				{
				}
				while (comp.Compare(x, UnsafeUtility.ReadArrayElement<T>(array, --num2)) < 0)
				{
				}
				if (i >= num2)
				{
					break;
				}
				NativeSortExtension.SwapStruct<T>(array, i, num2);
			}
			NativeSortExtension.SwapStruct<T>(array, i, hi - 1);
			return i;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00018918 File Offset: 0x00016B18
		private unsafe static void HeapSortStruct<T, U>(void* array, int lo, int hi, U comp) where T : struct where U : IComparer<T>
		{
			int num = hi - lo + 1;
			for (int i = num / 2; i >= 1; i--)
			{
				NativeSortExtension.HeapifyStruct<T, U>(array, i, num, lo, comp);
			}
			for (int j = num; j > 1; j--)
			{
				NativeSortExtension.SwapStruct<T>(array, lo, lo + j - 1);
				NativeSortExtension.HeapifyStruct<T, U>(array, 1, j - 1, lo, comp);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00018968 File Offset: 0x00016B68
		private unsafe static void HeapifyStruct<T, U>(void* array, int i, int n, int lo, U comp) where T : struct where U : IComparer<T>
		{
			T t = UnsafeUtility.ReadArrayElement<T>(array, lo + i - 1);
			while (i <= n / 2)
			{
				int num = 2 * i;
				if (num < n && comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1), UnsafeUtility.ReadArrayElement<T>(array, lo + num)) < 0)
				{
					num++;
				}
				if (comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1), t) < 0)
				{
					break;
				}
				UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, UnsafeUtility.ReadArrayElement<T>(array, lo + num - 1));
				i = num;
			}
			UnsafeUtility.WriteArrayElement<T>(array, lo + i - 1, t);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000189FC File Offset: 0x00016BFC
		private unsafe static void SwapStruct<T>(void* array, int lhs, int rhs) where T : struct
		{
			T value = UnsafeUtility.ReadArrayElement<T>(array, lhs);
			UnsafeUtility.WriteArrayElement<T>(array, lhs, UnsafeUtility.ReadArrayElement<T>(array, rhs));
			UnsafeUtility.WriteArrayElement<T>(array, rhs, value);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00018A27 File Offset: 0x00016C27
		private unsafe static void SwapIfGreaterWithItemsStruct<T, U>(void* array, int lhs, int rhs, U comp) where T : struct where U : IComparer<T>
		{
			if (lhs != rhs && comp.Compare(UnsafeUtility.ReadArrayElement<T>(array, lhs), UnsafeUtility.ReadArrayElement<T>(array, rhs)) > 0)
			{
				NativeSortExtension.SwapStruct<T>(array, lhs, rhs);
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00018A53 File Offset: 0x00016C53
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckStrideMatchesSize<T>(int stride) where T : struct
		{
			if (stride != UnsafeUtility.SizeOf<T>())
			{
				throw new InvalidOperationException("Sort requires that stride matches the size of the source type");
			}
		}

		// Token: 0x040002B8 RID: 696
		private const int k_IntrosortSizeThreshold = 16;

		// Token: 0x020000D6 RID: 214
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public struct DefaultComparer<T> : IComparer<T> where T : IComparable<T>
		{
			// Token: 0x0600080D RID: 2061 RVA: 0x00018A68 File Offset: 0x00016C68
			public int Compare(T x, T y)
			{
				return x.CompareTo(y);
			}
		}

		// Token: 0x020000D7 RID: 215
		[BurstCompile]
		private struct SegmentSort<[IsUnmanaged] T, U> : IJobParallelFor where T : struct, ValueType where U : IComparer<T>
		{
			// Token: 0x0600080E RID: 2062 RVA: 0x00018A78 File Offset: 0x00016C78
			public void Execute(int index)
			{
				int num = index * this.SegmentWidth;
				int length = (this.Length - num < this.SegmentWidth) ? (this.Length - num) : this.SegmentWidth;
				NativeSortExtension.Sort<T, U>(this.Data + (IntPtr)num * (IntPtr)sizeof(T) / (IntPtr)sizeof(T), length, this.Comp);
			}

			// Token: 0x040002B9 RID: 697
			[NativeDisableUnsafePtrRestriction]
			public unsafe T* Data;

			// Token: 0x040002BA RID: 698
			public U Comp;

			// Token: 0x040002BB RID: 699
			public int Length;

			// Token: 0x040002BC RID: 700
			public int SegmentWidth;
		}

		// Token: 0x020000D8 RID: 216
		[BurstCompile]
		private struct SegmentSortMerge<[IsUnmanaged] T, U> : IJob where T : struct, ValueType where U : IComparer<T>
		{
			// Token: 0x0600080F RID: 2063 RVA: 0x00018ACC File Offset: 0x00016CCC
			public unsafe void Execute()
			{
				int num = (this.Length + (this.SegmentWidth - 1)) / this.SegmentWidth;
				int* ptr = stackalloc int[checked(unchecked((UIntPtr)num) * 4)];
				T* ptr2 = (T*)Memory.Unmanaged.Allocate((long)(UnsafeUtility.SizeOf<T>() * this.Length), 16, Allocator.Temp);
				for (int i = 0; i < this.Length; i++)
				{
					int num2 = -1;
					T t = default(T);
					for (int j = 0; j < num; j++)
					{
						int num3 = j * this.SegmentWidth;
						int num4 = ptr[j];
						int num5 = (this.Length - num3 < this.SegmentWidth) ? (this.Length - num3) : this.SegmentWidth;
						if (num4 != num5)
						{
							T t2 = this.Data[(IntPtr)(num3 + num4) * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)];
							if (num2 == -1 || this.Comp.Compare(t2, t) <= 0)
							{
								t = t2;
								num2 = j;
							}
						}
					}
					ptr[num2]++;
					ptr2[(IntPtr)i * (IntPtr)sizeof(T) / (IntPtr)sizeof(T)] = t;
				}
				UnsafeUtility.MemCpy((void*)this.Data, (void*)ptr2, (long)(UnsafeUtility.SizeOf<T>() * this.Length));
			}

			// Token: 0x040002BD RID: 701
			[NativeDisableUnsafePtrRestriction]
			public unsafe T* Data;

			// Token: 0x040002BE RID: 702
			public U Comp;

			// Token: 0x040002BF RID: 703
			public int Length;

			// Token: 0x040002C0 RID: 704
			public int SegmentWidth;
		}
	}
}
