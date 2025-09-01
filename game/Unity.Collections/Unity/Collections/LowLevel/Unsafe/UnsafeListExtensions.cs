using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x02000127 RID: 295
	[BurstCompatible]
	public static class UnsafeListExtensions
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x00020508 File Offset: 0x0001E708
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[IsUnmanaged] T, U>(this UnsafeList<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Ptr, list.Length, value);
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002051D File Offset: 0x0001E71D
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[IsUnmanaged] T, U>(this UnsafeList<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return list.IndexOf(value) != -1;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002052C File Offset: 0x0001E72C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[IsUnmanaged] T, U>(this UnsafeList<T>.ParallelReader list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Ptr, list.Length, value);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00020540 File Offset: 0x0001E740
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[IsUnmanaged] T, U>(this UnsafeList<T>.ParallelReader list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return list.IndexOf(value) != -1;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00020550 File Offset: 0x0001E750
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public static bool ArraysEqual<[IsUnmanaged] T>(this UnsafeList<T> array, UnsafeList<T> other) where T : struct, ValueType, IEquatable<T>
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
	}
}
