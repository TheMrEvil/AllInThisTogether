using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000855 RID: 2133
	internal static class Unsafe
	{
		// Token: 0x06004711 RID: 18193 RVA: 0x000E7EEE File Offset: 0x000E60EE
		public static ref T Add<T>(ref T source, int elementOffset)
		{
			return ref source + (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06004712 RID: 18194 RVA: 0x000E7EFB File Offset: 0x000E60FB
		public static ref T Add<T>(ref T source, IntPtr elementOffset)
		{
			return ref source + elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06004713 RID: 18195 RVA: 0x000E7EEE File Offset: 0x000E60EE
		public unsafe static void* Add<T>(void* source, int elementOffset)
		{
			return (void*)((byte*)source + (IntPtr)elementOffset * (IntPtr)sizeof(T));
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x000E7F07 File Offset: 0x000E6107
		public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
		{
			return ref source + byteOffset;
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x00028426 File Offset: 0x00026626
		public static bool AreSame<T>(ref T left, ref T right)
		{
			return ref left == ref right;
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x0000270D File Offset: 0x0000090D
		public static T As<T>(object o) where T : class
		{
			return o;
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x0000270D File Offset: 0x0000090D
		public static ref TTo As<TFrom, TTo>(ref TFrom source)
		{
			return ref source;
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x000E7F0C File Offset: 0x000E610C
		public unsafe static void* AsPointer<T>(ref T value)
		{
			return (void*)(&value);
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x0000270D File Offset: 0x0000090D
		public unsafe static ref T AsRef<T>(void* source)
		{
			return ref *(T*)source;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x0000270D File Offset: 0x0000090D
		public static ref T AsRef<T>(in T source)
		{
			return ref source;
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x000E7F10 File Offset: 0x000E6110
		public static IntPtr ByteOffset<T>(ref T origin, ref T target)
		{
			return ref target - ref origin;
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x000E7F15 File Offset: 0x000E6115
		public static void CopyBlock(ref byte destination, ref byte source, uint byteCount)
		{
			cpblk(ref destination, ref source, byteCount);
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x000E7F1C File Offset: 0x000E611C
		public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
		{
			initblk(ref startAddress, value, byteCount);
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x000E7F1C File Offset: 0x000E611C
		public unsafe static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
		{
			initblk(startAddress, value, byteCount);
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x000E7F26 File Offset: 0x000E6126
		public unsafe static T Read<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x000E7F2E File Offset: 0x000E612E
		public unsafe static T ReadUnaligned<T>(void* source)
		{
			return *(T*)source;
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x000E7F2E File Offset: 0x000E612E
		public static T ReadUnaligned<T>(ref byte source)
		{
			return source;
		}

		// Token: 0x06004722 RID: 18210 RVA: 0x000E7F39 File Offset: 0x000E6139
		public static int SizeOf<T>()
		{
			return sizeof(T);
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x000E7F41 File Offset: 0x000E6141
		public static ref T Subtract<T>(ref T source, int elementOffset)
		{
			return ref source - (IntPtr)elementOffset * (IntPtr)sizeof(T);
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x000E7F4E File Offset: 0x000E614E
		public static void WriteUnaligned<T>(ref byte destination, T value)
		{
			destination = value;
		}

		// Token: 0x06004725 RID: 18213 RVA: 0x000E7F4E File Offset: 0x000E614E
		public unsafe static void WriteUnaligned<T>(void* destination, T value)
		{
			*(T*)destination = value;
		}

		// Token: 0x06004726 RID: 18214 RVA: 0x000E7F5A File Offset: 0x000E615A
		public static bool IsAddressGreaterThan<T>(ref T left, ref T right)
		{
			return ref left != ref right;
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x000E7F60 File Offset: 0x000E6160
		public static bool IsAddressLessThan<T>(ref T left, ref T right)
		{
			return ref left < ref right;
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x000E7F66 File Offset: 0x000E6166
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T AddByteOffset<T>(ref T source, ulong byteOffset)
		{
			return Unsafe.AddByteOffset<T>(ref source, (IntPtr)byteOffset);
		}
	}
}
