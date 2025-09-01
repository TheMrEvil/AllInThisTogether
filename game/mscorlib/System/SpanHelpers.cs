using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000180 RID: 384
	internal static class SpanHelpers
	{
		// Token: 0x06000F61 RID: 3937 RVA: 0x0003D963 File Offset: 0x0003BB63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this ReadOnlySpan<T> span, TComparable comparable) where TComparable : IComparable<T>
		{
			if (comparable == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparable);
			}
			return SpanHelpers.BinarySearch<T, TComparable>(MemoryMarshal.GetReference<T>(span), span.Length, comparable);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003D988 File Offset: 0x0003BB88
		public unsafe static int BinarySearch<T, TComparable>(ref T spanStart, int length, TComparable comparable) where TComparable : IComparable<T>
		{
			int i = 0;
			int num = length - 1;
			while (i <= num)
			{
				int num2 = (int)((uint)(num + i) >> 1);
				int num3 = comparable.CompareTo(*Unsafe.Add<T>(ref spanStart, num2));
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 > 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003D9D8 File Offset: 0x0003BBD8
		public static int IndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			byte value2 = value;
			ref byte second = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf(Unsafe.Add<byte>(ref searchSpace, num2), value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<byte>(Unsafe.Add<byte>(ref searchSpace, num2 + 1), ref second, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003DA40 File Offset: 0x0003BC40
		public unsafe static int IndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003DA80 File Offset: 0x0003BC80
		public unsafe static int LastIndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.LastIndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003DAB8 File Offset: 0x0003BCB8
		public unsafe static int IndexOf(ref byte searchSpace, byte value, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			while ((void*)intPtr2 >= 8)
			{
				intPtr2 -= 8;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					IL_14D:
					return (void*)intPtr;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
				{
					IL_155:
					return (void*)(intPtr + 1);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
				{
					IL_163:
					return (void*)(intPtr + 2);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
				{
					IL_171:
					return (void*)(intPtr + 3);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4))
				{
					return (void*)(intPtr + 4);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5))
				{
					return (void*)(intPtr + 5);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6))
				{
					return (void*)(intPtr + 6);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7))
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if ((void*)intPtr2 >= 4)
			{
				intPtr2 -= 4;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					goto IL_14D;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
				{
					goto IL_155;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
				{
					goto IL_163;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
				{
					goto IL_171;
				}
				intPtr += 4;
			}
			while ((void*)intPtr2 != null)
			{
				intPtr2 -= 1;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					goto IL_14D;
				}
				intPtr += 1;
			}
			return -1;
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003DC7C File Offset: 0x0003BE7C
		public static int LastIndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			byte value2 = value;
			ref byte second = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				num4 = SpanHelpers.LastIndexOf(ref searchSpace, value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				if (SpanHelpers.SequenceEqual<byte>(Unsafe.Add<byte>(ref searchSpace, num4 + 1), ref second, num))
				{
					break;
				}
				num2 += num3 - num4;
			}
			return num4;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003DCDC File Offset: 0x0003BEDC
		public unsafe static int LastIndexOf(ref byte searchSpace, byte value, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			while ((void*)intPtr2 >= 8)
			{
				intPtr2 -= 8;
				intPtr -= 8;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7))
				{
					return (void*)(intPtr + 7);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6))
				{
					return (void*)(intPtr + 6);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5))
				{
					return (void*)(intPtr + 5);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4))
				{
					return (void*)(intPtr + 4);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
				{
					IL_171:
					return (void*)(intPtr + 3);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
				{
					IL_163:
					return (void*)(intPtr + 2);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
				{
					IL_155:
					return (void*)(intPtr + 1);
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					IL_14D:
					return (void*)intPtr;
				}
			}
			if ((void*)intPtr2 >= 4)
			{
				intPtr2 -= 4;
				intPtr -= 4;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3))
				{
					goto IL_171;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2))
				{
					goto IL_163;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1))
				{
					goto IL_155;
				}
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					goto IL_14D;
				}
			}
			while ((void*)intPtr2 != null)
			{
				intPtr2 -= 1;
				intPtr -= 1;
				if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr))
				{
					goto IL_14D;
				}
			}
			return -1;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003DEA0 File Offset: 0x0003C0A0
		public unsafe static int IndexOfAny(ref byte searchSpace, byte value0, byte value1, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			while ((void*)intPtr2 >= 8)
			{
				intPtr2 -= 8;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1E5:
					return (void*)intPtr;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1ED:
					return (void*)(intPtr + 1);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1FB:
					return (void*)(intPtr + 2);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_209:
					return (void*)(intPtr + 3);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (void*)(intPtr + 4);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (void*)(intPtr + 5);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (void*)(intPtr + 6);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if ((void*)intPtr2 >= 4)
			{
				intPtr2 -= 4;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1E5;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1ED;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1FB;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_209;
				}
				intPtr += 4;
			}
			while ((void*)intPtr2 != null)
			{
				intPtr2 -= 1;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1E5;
				}
				intPtr += 1;
			}
			return -1;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0003E0FC File Offset: 0x0003C2FC
		public unsafe static int IndexOfAny(ref byte searchSpace, byte value0, byte value1, byte value2, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			while ((void*)intPtr2 >= 8)
			{
				intPtr2 -= 8;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					IL_25A:
					return (void*)intPtr;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					IL_262:
					return (void*)(intPtr + 1);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					IL_270:
					return (void*)(intPtr + 2);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					IL_27E:
					return (void*)(intPtr + 3);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					return (void*)(intPtr + 4);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					return (void*)(intPtr + 5);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					return (void*)(intPtr + 6);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if ((void*)intPtr2 >= 4)
			{
				intPtr2 -= 4;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_25A;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_262;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_270;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_27E;
				}
				intPtr += 4;
			}
			while ((void*)intPtr2 != null)
			{
				intPtr2 -= 1;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_25A;
				}
				intPtr += 1;
			}
			return -1;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003E3CC File Offset: 0x0003C5CC
		public unsafe static int LastIndexOfAny(ref byte searchSpace, byte value0, byte value1, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			while ((void*)intPtr2 >= 8)
			{
				intPtr2 -= 8;
				intPtr -= 8;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (void*)(intPtr + 7);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (void*)(intPtr + 6);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (void*)(intPtr + 5);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (void*)(intPtr + 4);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_209:
					return (void*)(intPtr + 3);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1FB:
					return (void*)(intPtr + 2);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1ED:
					return (void*)(intPtr + 1);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1E5:
					return (void*)intPtr;
				}
			}
			if ((void*)intPtr2 >= 4)
			{
				intPtr2 -= 4;
				intPtr -= 4;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_209;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1FB;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1ED;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num)
				{
					goto IL_1E5;
				}
				if ((uint)value1 == num)
				{
					goto IL_1E5;
				}
			}
			while ((void*)intPtr2 != null)
			{
				intPtr2 -= 1;
				intPtr -= 1;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1E5;
				}
			}
			return -1;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003E628 File Offset: 0x0003C828
		public unsafe static int LastIndexOfAny(ref byte searchSpace, byte value0, byte value1, byte value2, int length)
		{
			IntPtr intPtr = (IntPtr)length;
			IntPtr intPtr2 = (IntPtr)length;
			while ((void*)intPtr2 >= 8)
			{
				intPtr2 -= 8;
				intPtr -= 8;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 7));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					return (void*)(intPtr + 7);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 6));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					return (void*)(intPtr + 6);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 5));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					return (void*)(intPtr + 5);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 4));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					return (void*)(intPtr + 4);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					IL_27C:
					return (void*)(intPtr + 3);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					IL_26E:
					return (void*)(intPtr + 2);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					IL_260:
					return (void*)(intPtr + 1);
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					IL_258:
					return (void*)intPtr;
				}
			}
			if ((void*)intPtr2 >= 4)
			{
				intPtr2 -= 4;
				intPtr -= 4;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 3));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_27C;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 2));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_26E;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr + 1));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_260;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_258;
				}
				if ((uint)value2 == num)
				{
					goto IL_258;
				}
			}
			while ((void*)intPtr2 != null)
			{
				intPtr2 -= 1;
				intPtr -= 1;
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, intPtr));
				if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
				{
					goto IL_258;
				}
			}
			return -1;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003E8F8 File Offset: 0x0003CAF8
		public unsafe static bool SequenceEqual(ref byte first, ref byte second, ulong length)
		{
			if (!Unsafe.AreSame<byte>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)length;
				if ((void*)intPtr2 >= sizeof(UIntPtr))
				{
					intPtr2 -= sizeof(UIntPtr);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							return false;
						}
						intPtr += sizeof(UIntPtr);
					}
					return Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr2)) == Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr2));
				}
				while ((void*)intPtr2 != (void*)intPtr)
				{
					if (*Unsafe.AddByteOffset<byte>(ref first, intPtr) != *Unsafe.AddByteOffset<byte>(ref second, intPtr))
					{
						return false;
					}
					intPtr += 1;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0003E9C4 File Offset: 0x0003CBC4
		public unsafe static int SequenceCompareTo(ref byte first, int firstLength, ref byte second, int secondLength)
		{
			if (!Unsafe.AreSame<byte>(ref first, ref second))
			{
				IntPtr value = (IntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				IntPtr intPtr = (IntPtr)0;
				IntPtr intPtr2 = (IntPtr)((void*)value);
				if ((void*)intPtr2 != sizeof(UIntPtr))
				{
					intPtr2 -= sizeof(UIntPtr);
					while ((void*)intPtr2 != (void*)intPtr)
					{
						if (Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref first, intPtr)) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref second, intPtr)))
						{
							break;
						}
						intPtr += sizeof(UIntPtr);
					}
				}
				while ((void*)value != (void*)intPtr)
				{
					int num = Unsafe.AddByteOffset<byte>(ref first, intPtr).CompareTo(*Unsafe.AddByteOffset<byte>(ref second, intPtr));
					if (num != 0)
					{
						return num;
					}
					intPtr += 1;
				}
			}
			return firstLength - secondLength;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0003EA8C File Offset: 0x0003CC8C
		public unsafe static int SequenceCompareTo(ref char first, int firstLength, ref char second, int secondLength)
		{
			int result = firstLength - secondLength;
			if (!Unsafe.AreSame<char>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				IntPtr intPtr2 = (IntPtr)0;
				if ((void*)intPtr >= sizeof(UIntPtr) / 2)
				{
					if (Vector.IsHardwareAccelerated && (void*)intPtr >= Vector<ushort>.Count)
					{
						IntPtr value = intPtr - Vector<ushort>.Count;
						while (!(Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) != Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2)))))
						{
							intPtr2 += Vector<ushort>.Count;
							if ((void*)value < (void*)intPtr2)
							{
								break;
							}
						}
					}
					while ((void*)intPtr >= (void*)(intPtr2 + sizeof(UIntPtr) / 2) && !(Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) != Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2)))))
					{
						intPtr2 += sizeof(UIntPtr) / 2;
					}
				}
				if (sizeof(UIntPtr) > 4 && (void*)intPtr >= (void*)(intPtr2 + 2) && Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, intPtr2))) == Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, intPtr2))))
				{
					intPtr2 += 2;
				}
				while ((void*)intPtr2 < (void*)intPtr)
				{
					int num = Unsafe.Add<char>(ref first, intPtr2).CompareTo(*Unsafe.Add<char>(ref second, intPtr2));
					if (num != 0)
					{
						return num;
					}
					intPtr2 += 1;
				}
			}
			return result;
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0003EC10 File Offset: 0x0003CE10
		public unsafe static int IndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2;
				char* ptr4 = ptr3 + length;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					int num = (ptr3 & Unsafe.SizeOf<Vector<ushort>>() - 1) / 2;
					length = (Vector<ushort>.Count - num & Vector<ushort>.Count - 1);
				}
				Vector<ushort> vector;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							if (*ptr3 == value)
							{
								goto IL_127;
							}
							ptr3++;
						}
						if (!Vector.IsHardwareAccelerated || ptr3 >= ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr4 - ptr3) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> left = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							vector = Vector.Equals<ushort>(left, Unsafe.Read<Vector<ushort>>((void*)ptr3));
							if (!Vector<ushort>.Zero.Equals(vector))
							{
								goto IL_F3;
							}
							ptr3 += Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 >= ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr4 - ptr3));
					}
					else
					{
						length -= 4;
						if (*ptr3 == value)
						{
							goto IL_127;
						}
						if (ptr3[1] == value)
						{
							goto IL_123;
						}
						if (ptr3[2] == value)
						{
							goto IL_11F;
						}
						if (ptr3[3] == value)
						{
							goto IL_11B;
						}
						ptr3 += 4;
					}
				}
				IL_F3:
				return (int)((long)(ptr3 - ptr2)) + SpanHelpers.LocateFirstFoundChar(vector);
				IL_11B:
				ptr3++;
				IL_11F:
				ptr3++;
				IL_123:
				ptr3++;
				IL_127:
				return (int)((long)(ptr3 - ptr2));
			}
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003ED4C File Offset: 0x0003CF4C
		public unsafe static int LastIndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + length;
				char* ptr4 = ptr2;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					length = (ptr3 & Unsafe.SizeOf<Vector<ushort>>() - 1) / 2;
				}
				char* ptr5;
				Vector<ushort> vector;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							ptr3--;
							if (*ptr3 == value)
							{
								goto IL_11A;
							}
						}
						if (!Vector.IsHardwareAccelerated || ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> left = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							ptr5 = ptr3 - Vector<ushort>.Count;
							vector = Vector.Equals<ushort>(left, Unsafe.Read<Vector<ushort>>((void*)ptr5));
							if (!Vector<ushort>.Zero.Equals(vector))
							{
								goto IL_F1;
							}
							ptr3 -= Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4));
					}
					else
					{
						length -= 4;
						ptr3 -= 4;
						if (ptr3[3] == value)
						{
							goto IL_136;
						}
						if (ptr3[2] == value)
						{
							goto IL_12C;
						}
						if (ptr3[1] == value)
						{
							goto IL_122;
						}
						if (*ptr3 == value)
						{
							goto IL_11A;
						}
					}
				}
				IL_F1:
				return (int)((long)(ptr5 - ptr4)) + SpanHelpers.LocateLastFoundChar(vector);
				IL_11A:
				return (int)((long)(ptr3 - ptr4));
				IL_122:
				return (int)((long)(ptr3 - ptr4)) + 1;
				IL_12C:
				return (int)((long)(ptr3 - ptr4)) + 2;
				IL_136:
				return (int)((long)(ptr3 - ptr4)) + 3;
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0003EE98 File Offset: 0x0003D098
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateFirstFoundChar(num);
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0003EED5 File Offset: 0x0003D0D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(ulong match)
		{
			return (int)((match ^ match - 1UL) * 4295098372UL >> 49);
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0003EEEC File Offset: 0x0003D0EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = Vector<ulong>.Count - 1; i >= 0; i--)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateLastFoundChar(num);
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0003EF2C File Offset: 0x0003D12C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(ulong match)
		{
			int num = 3;
			while (match > 0UL)
			{
				match <<= 16;
				num--;
			}
			return num;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003EF50 File Offset: 0x0003D150
		public static int IndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			T value2 = value;
			ref T second = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf<T>(Unsafe.Add<T>(ref searchSpace, num2), value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num2 + 1), ref second, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0003EFBC File Offset: 0x0003D1BC
		public unsafe static int IndexOf<T>(ref T searchSpace, T value, int length) where T : IEquatable<T>
		{
			IntPtr intPtr = (IntPtr)0;
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					IL_202:
					return (void*)intPtr;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					IL_20A:
					return (void*)(intPtr + 1);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					IL_218:
					return (void*)(intPtr + 2);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					IL_226:
					return (void*)(intPtr + 3);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 4)))
				{
					return (void*)(intPtr + 4);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 5)))
				{
					return (void*)(intPtr + 5);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 6)))
				{
					return (void*)(intPtr + 6);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 7)))
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_202;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					goto IL_20A;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					goto IL_218;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					goto IL_226;
				}
				intPtr += 4;
			}
			while (length > 0)
			{
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_202;
				}
				intPtr += 1;
				length--;
			}
			return -1;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003F234 File Offset: 0x0003D434
		public unsafe static int IndexOfAny<T>(ref T searchSpace, T value0, T value1, int length) where T : IEquatable<T>
		{
			int i = 0;
			while (length - i >= 8)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2CB:
					return i + 1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2CF:
					return i + 2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2D3:
					return i + 3;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 4);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i + 4;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 5);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i + 5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 6);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i + 6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 7);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i + 7;
				}
				i += 8;
			}
			if (length - i >= 4)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2CB;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2CF;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2D3;
				}
				i += 4;
			}
			while (i < length)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003F528 File Offset: 0x0003D728
		public unsafe static int IndexOfAny<T>(ref T searchSpace, T value0, T value1, T value2, int length) where T : IEquatable<T>
		{
			int i = 0;
			while (length - i >= 8)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3C2:
					return i + 1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3C6:
					return i + 2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3CA:
					return i + 3;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 4);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i + 4;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 5);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i + 5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 6);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i + 6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 7);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i + 7;
				}
				i += 8;
			}
			if (length - i >= 4)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3C2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3C6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3CA;
				}
				i += 4;
			}
			while (i < length)
			{
				T other = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003F914 File Offset: 0x0003DB14
		public unsafe static int IndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003F958 File Offset: 0x0003DB58
		public static int LastIndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			T value2 = value;
			ref T second = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				num4 = SpanHelpers.LastIndexOf<T>(ref searchSpace, value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num4 + 1), ref second, num))
				{
					break;
				}
				num2 += num3 - num4;
			}
			return num4;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0003F9BC File Offset: 0x0003DBBC
		public unsafe static int LastIndexOf<T>(ref T searchSpace, T value, int length) where T : IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 7)))
				{
					return length + 7;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 6)))
				{
					return length + 6;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 5)))
				{
					return length + 5;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 4)))
				{
					return length + 4;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 3)))
				{
					IL_1C2:
					return length + 3;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 2)))
				{
					IL_1BE:
					return length + 2;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 1)))
				{
					IL_1BA:
					return length + 1;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 3)))
				{
					goto IL_1C2;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 2)))
				{
					goto IL_1BE;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 1)))
				{
					goto IL_1BA;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0003FBA0 File Offset: 0x0003DDA0
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, T value0, T value1, int length) where T : IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				T other = *Unsafe.Add<T>(ref searchSpace, length + 7);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length + 7;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 6);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length + 6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 5);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length + 5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 4);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length + 4;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2CD:
					return length + 3;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2C9:
					return length + 2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(other) || value1.Equals(other))
				{
					IL_2C5:
					return length + 1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				T other = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2CD;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2C9;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(other) || value1.Equals(other))
				{
					goto IL_2C5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other))
				{
					return length;
				}
				if (value1.Equals(other))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				T other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0003FE90 File Offset: 0x0003E090
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, T value0, T value1, T value2, int length) where T : IEquatable<T>
		{
			while (length >= 8)
			{
				length -= 8;
				T other = *Unsafe.Add<T>(ref searchSpace, length + 7);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length + 7;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 6);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length + 6;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 5);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length + 5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 4);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length + 4;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3DA:
					return length + 3;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3D5:
					return length + 2;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					IL_3D0:
					return length + 1;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				T other = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3DA;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3D5;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					goto IL_3D0;
				}
				other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other))
				{
					return length;
				}
				if (value2.Equals(other))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				T other = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(other) || value1.Equals(other) || value2.Equals(other))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x00040290 File Offset: 0x0003E490
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.LastIndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x000402CC File Offset: 0x0003E4CC
		public unsafe static bool SequenceEqual<T>(ref T first, ref T second, int length) where T : IEquatable<T>
		{
			if (!Unsafe.AreSame<T>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				while (length >= 8)
				{
					length -= 8;
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)) || !Unsafe.Add<T>(ref first, intPtr + 1).Equals(*Unsafe.Add<T>(ref second, intPtr + 1)) || !Unsafe.Add<T>(ref first, intPtr + 2).Equals(*Unsafe.Add<T>(ref second, intPtr + 2)) || !Unsafe.Add<T>(ref first, intPtr + 3).Equals(*Unsafe.Add<T>(ref second, intPtr + 3)) || !Unsafe.Add<T>(ref first, intPtr + 4).Equals(*Unsafe.Add<T>(ref second, intPtr + 4)) || !Unsafe.Add<T>(ref first, intPtr + 5).Equals(*Unsafe.Add<T>(ref second, intPtr + 5)) || !Unsafe.Add<T>(ref first, intPtr + 6).Equals(*Unsafe.Add<T>(ref second, intPtr + 6)) || !Unsafe.Add<T>(ref first, intPtr + 7).Equals(*Unsafe.Add<T>(ref second, intPtr + 7)))
					{
						return false;
					}
					intPtr += 8;
				}
				if (length >= 4)
				{
					length -= 4;
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)) || !Unsafe.Add<T>(ref first, intPtr + 1).Equals(*Unsafe.Add<T>(ref second, intPtr + 1)) || !Unsafe.Add<T>(ref first, intPtr + 2).Equals(*Unsafe.Add<T>(ref second, intPtr + 2)) || !Unsafe.Add<T>(ref first, intPtr + 3).Equals(*Unsafe.Add<T>(ref second, intPtr + 3)))
					{
						return false;
					}
					intPtr += 4;
				}
				while (length > 0)
				{
					if (!Unsafe.Add<T>(ref first, intPtr).Equals(*Unsafe.Add<T>(ref second, intPtr)))
					{
						return false;
					}
					intPtr += 1;
					length--;
				}
			}
			return true;
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00040568 File Offset: 0x0003E768
		public unsafe static int SequenceCompareTo<T>(ref T first, int firstLength, ref T second, int secondLength) where T : IComparable<T>
		{
			int num = firstLength;
			if (num > secondLength)
			{
				num = secondLength;
			}
			for (int i = 0; i < num; i++)
			{
				int num2 = Unsafe.Add<T>(ref first, i).CompareTo(*Unsafe.Add<T>(ref second, i));
				if (num2 != 0)
				{
					return num2;
				}
			}
			return firstLength.CompareTo(secondLength);
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x000405B5 File Offset: 0x0003E7B5
		public static int IndexOfCultureHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, CompareInfo compareInfo)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantIndexOf(span, value, false);
			}
			return compareInfo.IndexOf(span, value, CompareOptions.None);
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x000405D0 File Offset: 0x0003E7D0
		public static int IndexOfCultureIgnoreCaseHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, CompareInfo compareInfo)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantIndexOf(span, value, true);
			}
			return compareInfo.IndexOf(span, value, CompareOptions.IgnoreCase);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000405EB File Offset: 0x0003E7EB
		public static int IndexOfOrdinalHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, bool ignoreCase)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantIndexOf(span, value, ignoreCase);
			}
			return CompareInfo.Invariant.IndexOfOrdinal(span, value, ignoreCase);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0004060A File Offset: 0x0003E80A
		public static bool StartsWithCultureHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, CompareInfo compareInfo)
		{
			if (GlobalizationMode.Invariant)
			{
				return span.StartsWith(value);
			}
			return span.Length != 0 && compareInfo.IsPrefix(span, value, CompareOptions.None);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0004062F File Offset: 0x0003E82F
		public static bool StartsWithCultureIgnoreCaseHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, CompareInfo compareInfo)
		{
			if (GlobalizationMode.Invariant)
			{
				return SpanHelpers.StartsWithOrdinalIgnoreCaseHelper(span, value);
			}
			return span.Length != 0 && compareInfo.IsPrefix(span, value, CompareOptions.IgnoreCase);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x00040654 File Offset: 0x0003E854
		public static bool StartsWithOrdinalIgnoreCaseHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length >= value.Length && CompareInfo.CompareOrdinalIgnoreCase(span.Slice(0, value.Length), value) == 0;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x00040680 File Offset: 0x0003E880
		public static bool EndsWithCultureHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, CompareInfo compareInfo)
		{
			if (GlobalizationMode.Invariant)
			{
				return span.EndsWith(value);
			}
			return span.Length != 0 && compareInfo.IsSuffix(span, value, CompareOptions.None);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000406A5 File Offset: 0x0003E8A5
		public static bool EndsWithCultureIgnoreCaseHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value, CompareInfo compareInfo)
		{
			if (GlobalizationMode.Invariant)
			{
				return SpanHelpers.EndsWithOrdinalIgnoreCaseHelper(span, value);
			}
			return span.Length != 0 && compareInfo.IsSuffix(span, value, CompareOptions.IgnoreCase);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000406CA File Offset: 0x0003E8CA
		public static bool EndsWithOrdinalIgnoreCaseHelper(ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length >= value.Length && CompareInfo.CompareOrdinalIgnoreCase(span.Slice(span.Length - value.Length), value) == 0;
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x00040700 File Offset: 0x0003E900
		public unsafe static void ClearWithoutReferences(ref byte b, ulong byteLength)
		{
			if (byteLength == 0UL)
			{
				return;
			}
			ulong num = byteLength - 1UL;
			if (num <= 21UL)
			{
				switch ((uint)num)
				{
				case 0U:
					b = 0;
					return;
				case 1U:
					*Unsafe.As<byte, short>(ref b) = 0;
					return;
				case 2U:
					*Unsafe.As<byte, short>(ref b) = 0;
					*Unsafe.Add<byte>(ref b, 2) = 0;
					return;
				case 3U:
					*Unsafe.As<byte, int>(ref b) = 0;
					return;
				case 4U:
					*Unsafe.As<byte, int>(ref b) = 0;
					*Unsafe.Add<byte>(ref b, 4) = 0;
					return;
				case 5U:
					*Unsafe.As<byte, int>(ref b) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 4)) = 0;
					return;
				case 6U:
					*Unsafe.As<byte, int>(ref b) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 4)) = 0;
					*Unsafe.Add<byte>(ref b, 6) = 0;
					return;
				case 7U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					return;
				case 8U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.Add<byte>(ref b, 8) = 0;
					return;
				case 9U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 8)) = 0;
					return;
				case 10U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 8)) = 0;
					*Unsafe.Add<byte>(ref b, 10) = 0;
					return;
				case 11U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 8)) = 0;
					return;
				case 12U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 8)) = 0;
					*Unsafe.Add<byte>(ref b, 12) = 0;
					return;
				case 13U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 8)) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 12)) = 0;
					return;
				case 14U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 8)) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 12)) = 0;
					*Unsafe.Add<byte>(ref b, 14) = 0;
					return;
				case 15U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					return;
				case 16U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.Add<byte>(ref b, 16) = 0;
					return;
				case 17U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 16)) = 0;
					return;
				case 18U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 16)) = 0;
					*Unsafe.Add<byte>(ref b, 18) = 0;
					return;
				case 19U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 16)) = 0;
					return;
				case 20U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 16)) = 0;
					*Unsafe.Add<byte>(ref b, 20) = 0;
					return;
				case 21U:
					*Unsafe.As<byte, long>(ref b) = 0L;
					*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, 8)) = 0L;
					*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, 16)) = 0;
					*Unsafe.As<byte, short>(Unsafe.Add<byte>(ref b, 20)) = 0;
					return;
				}
			}
			if (byteLength < 512UL)
			{
				ulong num2 = 0UL;
				if ((*Unsafe.As<byte, int>(ref b) & 3) != 0)
				{
					if ((*Unsafe.As<byte, int>(ref b) & 1) != 0)
					{
						*Unsafe.AddByteOffset<byte>(ref b, num2) = 0;
						num2 += 1UL;
						if ((*Unsafe.As<byte, int>(ref b) & 2) != 0)
						{
							goto IL_349;
						}
					}
					*Unsafe.As<byte, short>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0;
					num2 += 2UL;
				}
				IL_349:
				if ((*Unsafe.As<byte, int>(ref b) - 1 & 4) == 0)
				{
					*Unsafe.As<byte, int>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0;
					num2 += 4UL;
				}
				ulong num3 = byteLength - 16UL;
				byteLength -= num2;
				ulong num4;
				do
				{
					num4 = num2 + 16UL;
					*Unsafe.As<byte, long>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0L;
					*Unsafe.As<byte, long>(Unsafe.AddByteOffset<byte>(ref b, num2 + 8UL)) = 0L;
					num2 = num4;
				}
				while (num4 <= num3);
				if ((byteLength & 8UL) != 0UL)
				{
					*Unsafe.As<byte, long>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0L;
					num2 += 8UL;
				}
				if ((byteLength & 4UL) != 0UL)
				{
					*Unsafe.As<byte, int>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0;
					num2 += 4UL;
				}
				if ((byteLength & 2UL) != 0UL)
				{
					*Unsafe.As<byte, short>(Unsafe.AddByteOffset<byte>(ref b, num2)) = 0;
					num2 += 2UL;
				}
				if ((byteLength & 1UL) != 0UL)
				{
					*Unsafe.AddByteOffset<byte>(ref b, num2) = 0;
				}
				return;
			}
			RuntimeImports.RhZeroMemory(ref b, byteLength);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00040B14 File Offset: 0x0003ED14
		public unsafe static void ClearWithReferences(ref IntPtr ip, ulong pointerSizeLength)
		{
			while (pointerSizeLength >= 8UL)
			{
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -1) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -2) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -4) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -5) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -6) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -7) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -8) = 0;
				pointerSizeLength -= 8UL;
			}
			if (pointerSizeLength < 4UL)
			{
				if (pointerSizeLength < 2UL)
				{
					if (pointerSizeLength <= 0UL)
					{
						return;
					}
					goto IL_15B;
				}
			}
			else
			{
				*Unsafe.Add<IntPtr>(ref ip, 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, 3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -2) = 0;
			}
			*Unsafe.Add<IntPtr>(ref ip, 1) = 0;
			*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)((long)pointerSizeLength)), -1) = 0;
			IL_15B:
			ip = 0;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x00040C84 File Offset: 0x0003EE84
		public unsafe static void CopyTo<T>(ref T dst, int dstLength, ref T src, int srcLength)
		{
			IntPtr value = Unsafe.ByteOffset<T>(ref src, Unsafe.Add<T>(ref src, srcLength));
			IntPtr value2 = Unsafe.ByteOffset<T>(ref dst, Unsafe.Add<T>(ref dst, dstLength));
			IntPtr value3 = Unsafe.ByteOffset<T>(ref src, ref dst);
			if (!((sizeof(IntPtr) == 4) ? ((int)value3 < (int)value || (int)value3 > -(int)value2) : ((long)value3 < (long)value || (long)value3 > -(long)value2)) && !SpanHelpers.IsReferenceOrContainsReferences<T>())
			{
				ref byte source = ref Unsafe.As<T, byte>(ref dst);
				ref byte source2 = ref Unsafe.As<T, byte>(ref src);
				ulong num = (ulong)((long)value);
				uint num3;
				for (ulong num2 = 0UL; num2 < num; num2 += (ulong)num3)
				{
					num3 = ((num - num2 > (ulong)-1) ? uint.MaxValue : ((uint)(num - num2)));
					Unsafe.CopyBlock(Unsafe.Add<byte>(ref source, (IntPtr)((long)num2)), Unsafe.Add<byte>(ref source2, (IntPtr)((long)num2)), num3);
				}
				return;
			}
			bool flag = (sizeof(IntPtr) == 4) ? ((int)value3 > -(int)value2) : ((long)value3 > -(long)value2);
			int num4 = flag ? 1 : -1;
			int num5 = flag ? 0 : (srcLength - 1);
			int i;
			for (i = 0; i < (srcLength & -8); i += 8)
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				*Unsafe.Add<T>(ref dst, num5 + num4) = *Unsafe.Add<T>(ref src, num5 + num4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 2) = *Unsafe.Add<T>(ref src, num5 + num4 * 2);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 3) = *Unsafe.Add<T>(ref src, num5 + num4 * 3);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 4) = *Unsafe.Add<T>(ref src, num5 + num4 * 4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 5) = *Unsafe.Add<T>(ref src, num5 + num4 * 5);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 6) = *Unsafe.Add<T>(ref src, num5 + num4 * 6);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 7) = *Unsafe.Add<T>(ref src, num5 + num4 * 7);
				num5 += num4 * 8;
			}
			if (i < (srcLength & -4))
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				*Unsafe.Add<T>(ref dst, num5 + num4) = *Unsafe.Add<T>(ref src, num5 + num4);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 2) = *Unsafe.Add<T>(ref src, num5 + num4 * 2);
				*Unsafe.Add<T>(ref dst, num5 + num4 * 3) = *Unsafe.Add<T>(ref src, num5 + num4 * 3);
				num5 += num4 * 4;
				i += 4;
			}
			while (i < srcLength)
			{
				*Unsafe.Add<T>(ref dst, num5) = *Unsafe.Add<T>(ref src, num5);
				num5 += num4;
				i++;
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00040FA8 File Offset: 0x0003F1A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static IntPtr Add<T>(this IntPtr start, int index)
		{
			if (sizeof(IntPtr) == 4)
			{
				uint num = (uint)(index * Unsafe.SizeOf<T>());
				return (IntPtr)((void*)((byte*)((void*)start) + num));
			}
			ulong num2 = (ulong)((long)index * (long)Unsafe.SizeOf<T>());
			return (IntPtr)((void*)((byte*)((void*)start) + num2));
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00040FED File Offset: 0x0003F1ED
		public static bool IsReferenceOrContainsReferences<T>()
		{
			return SpanHelpers.PerTypeValues<T>.IsReferenceOrContainsReferences;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00040FF4 File Offset: 0x0003F1F4
		private static bool IsReferenceOrContainsReferencesCore(Type type)
		{
			if (type.GetTypeInfo().IsPrimitive)
			{
				return false;
			}
			if (!type.GetTypeInfo().IsValueType)
			{
				return true;
			}
			Type underlyingType = Nullable.GetUnderlyingType(type);
			if (underlyingType != null)
			{
				type = underlyingType;
			}
			if (type.GetTypeInfo().IsEnum)
			{
				return false;
			}
			foreach (FieldInfo fieldInfo in type.GetTypeInfo().DeclaredFields)
			{
				if (!fieldInfo.IsStatic && SpanHelpers.IsReferenceOrContainsReferencesCore(fieldInfo.FieldType))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0004109C File Offset: 0x0003F29C
		public unsafe static void ClearLessThanPointerSized(byte* ptr, UIntPtr byteLength)
		{
			if (sizeof(UIntPtr) == 4)
			{
				Unsafe.InitBlockUnaligned((void*)ptr, 0, (uint)byteLength);
				return;
			}
			ulong num = (ulong)byteLength;
			uint num2 = (uint)(num & (ulong)-1);
			Unsafe.InitBlockUnaligned((void*)ptr, 0, num2);
			num -= (ulong)num2;
			ptr += num2;
			while (num > 0UL)
			{
				num2 = ((num >= (ulong)-1) ? uint.MaxValue : ((uint)num));
				Unsafe.InitBlockUnaligned((void*)ptr, 0, num2);
				ptr += num2;
				num -= (ulong)num2;
			}
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00041108 File Offset: 0x0003F308
		public static void ClearLessThanPointerSized(ref byte b, UIntPtr byteLength)
		{
			if (sizeof(UIntPtr) == 4)
			{
				Unsafe.InitBlockUnaligned(ref b, 0, (uint)byteLength);
				return;
			}
			ulong num = (ulong)byteLength;
			uint num2 = (uint)(num & (ulong)-1);
			Unsafe.InitBlockUnaligned(ref b, 0, num2);
			num -= (ulong)num2;
			long num3 = (long)((ulong)num2);
			while (num > 0UL)
			{
				num2 = ((num >= (ulong)-1) ? uint.MaxValue : ((uint)num));
				Unsafe.InitBlockUnaligned(Unsafe.Add<byte>(ref b, (IntPtr)num3), 0, num2);
				num3 += (long)((ulong)num2);
				num -= (ulong)num2;
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00041178 File Offset: 0x0003F378
		public unsafe static void ClearPointerSizedWithoutReferences(ref byte b, UIntPtr byteLength)
		{
			IntPtr intPtr = IntPtr.Zero;
			while (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg64)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg64>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg64);
				intPtr += sizeof(SpanHelpers.Reg64);
			}
			if (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg32)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg32>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg32);
				intPtr += sizeof(SpanHelpers.Reg32);
			}
			if (intPtr.LessThanEqual(byteLength - sizeof(SpanHelpers.Reg16)))
			{
				*Unsafe.As<byte, SpanHelpers.Reg16>(Unsafe.Add<byte>(ref b, intPtr)) = default(SpanHelpers.Reg16);
				intPtr += sizeof(SpanHelpers.Reg16);
			}
			if (intPtr.LessThanEqual(byteLength - 8))
			{
				*Unsafe.As<byte, long>(Unsafe.Add<byte>(ref b, intPtr)) = 0L;
				intPtr += 8;
			}
			if (sizeof(IntPtr) == 4 && intPtr.LessThanEqual(byteLength - 4))
			{
				*Unsafe.As<byte, int>(Unsafe.Add<byte>(ref b, intPtr)) = 0;
				intPtr += 4;
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0004127C File Offset: 0x0003F47C
		public unsafe static void ClearPointerSizedWithReferences(ref IntPtr ip, UIntPtr pointerSizeLength)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			while ((intPtr2 = intPtr + 8).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 3) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 4) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 5) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 6) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 7) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr2 = intPtr + 4).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 3) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr2 = intPtr + 2).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 0) = 0;
				*Unsafe.Add<IntPtr>(ref ip, intPtr + 1) = 0;
				intPtr = intPtr2;
			}
			if ((intPtr + 1).LessThanEqual(pointerSizeLength))
			{
				*Unsafe.Add<IntPtr>(ref ip, intPtr) = 0;
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000413FC File Offset: 0x0003F5FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool LessThanEqual(this IntPtr index, UIntPtr length)
		{
			if (sizeof(UIntPtr) != 4)
			{
				return (long)index <= (long)((ulong)length);
			}
			return (int)index <= (int)((uint)length);
		}

		// Token: 0x040012E8 RID: 4840
		private const ulong XorPowerOfTwoToHighChar = 4295098372UL;

		// Token: 0x02000181 RID: 385
		internal struct ComparerComparable<T, TComparer> : IComparable<T> where TComparer : IComparer<T>
		{
			// Token: 0x06000F96 RID: 3990 RVA: 0x0004142A File Offset: 0x0003F62A
			public ComparerComparable(T value, TComparer comparer)
			{
				this._value = value;
				this._comparer = comparer;
			}

			// Token: 0x06000F97 RID: 3991 RVA: 0x0004143C File Offset: 0x0003F63C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int CompareTo(T other)
			{
				TComparer comparer = this._comparer;
				return comparer.Compare(this._value, other);
			}

			// Token: 0x040012E9 RID: 4841
			private readonly T _value;

			// Token: 0x040012EA RID: 4842
			private readonly TComparer _comparer;
		}

		// Token: 0x02000182 RID: 386
		private struct Reg64
		{
		}

		// Token: 0x02000183 RID: 387
		private struct Reg32
		{
		}

		// Token: 0x02000184 RID: 388
		private struct Reg16
		{
		}

		// Token: 0x02000185 RID: 389
		public static class PerTypeValues<T>
		{
			// Token: 0x06000F98 RID: 3992 RVA: 0x00041464 File Offset: 0x0003F664
			private static IntPtr MeasureArrayAdjustment()
			{
				T[] array = new T[1];
				return Unsafe.ByteOffset<T>(ref Unsafe.As<Pinnable<T>>(array).Data, ref array[0]);
			}

			// Token: 0x06000F99 RID: 3993 RVA: 0x0004148F File Offset: 0x0003F68F
			// Note: this type is marked as 'beforefieldinit'.
			static PerTypeValues()
			{
			}

			// Token: 0x040012EB RID: 4843
			public static readonly bool IsReferenceOrContainsReferences = SpanHelpers.IsReferenceOrContainsReferencesCore(typeof(T));

			// Token: 0x040012EC RID: 4844
			public static readonly T[] EmptyArray = new T[0];

			// Token: 0x040012ED RID: 4845
			public static readonly IntPtr ArrayAdjustment = SpanHelpers.PerTypeValues<T>.MeasureArrayAdjustment();
		}
	}
}
