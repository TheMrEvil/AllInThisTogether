using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000061 RID: 97
	[BurstCompatible]
	public static class FixedList128BytesExtensions
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00007588 File Offset: 0x00005788
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public unsafe static int IndexOf<[IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return NativeArrayExtensions.IndexOf<T, U>((void*)list.Buffer, list.Length, value);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000759C File Offset: 0x0000579C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Contains<[IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			return ref list.IndexOf(value) != -1;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000075AC File Offset: 0x000057AC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool Remove<[IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			int num = ref list.IndexOf(value);
			if (num < 0)
			{
				return false;
			}
			list.RemoveAt(num);
			return true;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000075D0 File Offset: 0x000057D0
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(int)
		})]
		public static bool RemoveSwapBack<[IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U value) where T : struct, ValueType, IEquatable<U>
		{
			int num = ref list.IndexOf(value);
			if (num == -1)
			{
				return false;
			}
			list.RemoveAtSwapBack(num);
			return true;
		}
	}
}
