using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
	// Token: 0x020000AD RID: 173
	[BurstCompatible]
	public static class NativeArrayExtensions
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0001561E File Offset: 0x0001381E
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<T, U>(this NativeArray<T> array, U value) where T : struct, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(array.GetUnsafeReadOnlyPtr<T>(), array.Length, value) != -1;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00015639 File Offset: 0x00013839
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static int IndexOf<T, U>(this NativeArray<T> array, U value) where T : struct, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(array.GetUnsafeReadOnlyPtr<T>(), array.Length, value);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001564E File Offset: 0x0001384E
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<T, U>(this NativeArray<T>.ReadOnly array, U value) where T : struct, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(array.m_Buffer, array.m_Length, value) != -1;
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00015668 File Offset: 0x00013868
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static int IndexOf<T, U>(this NativeArray<T>.ReadOnly array, U value) where T : struct, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(array.m_Buffer, array.m_Length, value);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001567C File Offset: 0x0001387C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[IsUnmanaged] T, U>(this NativeList<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(list.GetUnsafeReadOnlyPtr<T>(), list.Length, value) != -1;
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00015697 File Offset: 0x00013897
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static int IndexOf<[IsUnmanaged] T, U>(this NativeList<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(list.GetUnsafeReadOnlyPtr<T>(), list.Length, value);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x000156AC File Offset: 0x000138AC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static bool Contains<T, U>(void* ptr, int length, U value) where T : struct, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>(ptr, length, value) != -1;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000156BC File Offset: 0x000138BC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<T, U>(void* ptr, int length, U value) where T : struct, IEquatable<U>
		{
			for (int num = 0; num != length; num++)
			{
				T t = UnsafeUtility.ReadArrayElement<T>(ptr, num);
				if (t.Equals(value))
				{
					return num;
				}
			}
			return -1;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000156F0 File Offset: 0x000138F0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static NativeArray<U> Reinterpret<T, U>(this NativeArray<T> array) where T : struct where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = UnsafeUtility.SizeOf<U>();
			long num3 = (long)array.Length * (long)num / (long)num2;
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<U>(NativeArrayUnsafeUtility.GetUnsafeBufferPointerWithoutChecks<T>(array), (int)num3, Allocator.None);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00015728 File Offset: 0x00013928
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static bool ArraysEqual<T>(this NativeArray<T> array, NativeArray<T> other) where T : struct, IEquatable<T>
		{
			if (array.Length != other.Length)
			{
				return false;
			}
			for (int num = 0; num != array.Length; num++)
			{
				T t = array[num];
				if (!t.Equals(other[num]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001577C File Offset: 0x0001397C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static bool ArraysEqual<[IsUnmanaged] T>(this NativeList<T> array, NativeArray<T> other) where T : struct, ValueType, IEquatable<T>
		{
			return array.AsArray().ArraysEqual(other);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001578C File Offset: 0x0001398C
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckReinterpretSize<T, U>(ref NativeArray<T> array) where T : struct where U : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			int num2 = UnsafeUtility.SizeOf<U>();
			long num3 = (long)array.Length * (long)num;
			if (num3 / (long)num2 * (long)num2 != num3)
			{
				throw new InvalidOperationException(string.Format("Types {0} (array length {1}) and {2} cannot be aliased due to size constraints. The size of the types and lengths involved must line up.", typeof(T), array.Length, typeof(U)));
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000157EC File Offset: 0x000139EC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		internal static void Initialize<T>(this NativeArray<T> array, int length, AllocatorManager.AllocatorHandle allocator, NativeArrayOptions options = NativeArrayOptions.UninitializedMemory) where T : struct
		{
			AllocatorManager.AllocatorHandle allocatorHandle = allocator;
			array.m_Buffer = ref allocatorHandle.AllocateStruct(default(T), length);
			array.m_Length = length;
			array.m_AllocatorLabel = Allocator.None;
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear(array.m_Buffer, (long)(array.m_Length * UnsafeUtility.SizeOf<T>()));
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001583C File Offset: 0x00013A3C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(AllocatorManager.AllocatorHandle)
		})]
		internal static void Initialize<T, [IsUnmanaged] U>(this NativeArray<T> array, int length, ref U allocator, NativeArrayOptions options = NativeArrayOptions.ClearMemory) where T : struct where U : struct, ValueType, AllocatorManager.IAllocator
		{
			array.m_Buffer = ref allocator.AllocateStruct(default(T), length);
			array.m_Length = length;
			array.m_AllocatorLabel = Allocator.None;
			if (options == NativeArrayOptions.ClearMemory)
			{
				UnsafeUtility.MemClear(array.m_Buffer, (long)(array.m_Length * UnsafeUtility.SizeOf<T>()));
			}
		}

		// Token: 0x020000AE RID: 174
		public struct NativeArrayStaticId<T> where T : struct
		{
			// Token: 0x060006A6 RID: 1702 RVA: 0x00015889 File Offset: 0x00013A89
			// Note: this type is marked as 'beforefieldinit'.
			static NativeArrayStaticId()
			{
			}

			// Token: 0x0400027D RID: 637
			internal static readonly SharedStatic<int> s_staticSafetyId = SharedStatic<int>.GetOrCreate<NativeArray<T>>(0U);
		}
	}
}
