using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200008B RID: 139
	public static class FixedListExtensions
	{
		// Token: 0x06000358 RID: 856 RVA: 0x00008F2C File Offset: 0x0000712C
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void Sort<[IsUnmanaged] T>(this FixedList32Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00008F60 File Offset: 0x00007160
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[IsUnmanaged] T, U>(this FixedList32Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008F98 File Offset: 0x00007198
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void Sort<[IsUnmanaged] T>(this FixedList64Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00008FCC File Offset: 0x000071CC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[IsUnmanaged] T, U>(this FixedList64Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009004 File Offset: 0x00007204
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void Sort<[IsUnmanaged] T>(this FixedList128Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009038 File Offset: 0x00007238
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[IsUnmanaged] T, U>(this FixedList128Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00009070 File Offset: 0x00007270
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void Sort<[IsUnmanaged] T>(this FixedList512Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000090A4 File Offset: 0x000072A4
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[IsUnmanaged] T, U>(this FixedList512Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000090DC File Offset: 0x000072DC
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int)
		})]
		public unsafe static void Sort<[IsUnmanaged] T>(this FixedList4096Bytes<T> list) where T : struct, ValueType, IComparable<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length);
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009110 File Offset: 0x00007310
		[BurstCompatible(GenericTypeArguments = new Type[]
		{
			typeof(int),
			typeof(NativeSortExtension.DefaultComparer<int>)
		})]
		public unsafe static void Sort<[IsUnmanaged] T, U>(this FixedList4096Bytes<T> list, U comp) where T : struct, ValueType, IComparable<T> where U : IComparer<T>
		{
			fixed (byte* ptr = &list.buffer.offset0000.byte0000)
			{
				NativeSortExtension.Sort<T, U>((T*)ptr + FixedList.PaddingBytes<T>() / sizeof(T), list.Length, comp);
			}
		}
	}
}
