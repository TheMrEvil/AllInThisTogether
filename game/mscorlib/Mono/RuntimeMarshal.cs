using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono
{
	// Token: 0x0200004E RID: 78
	internal static class RuntimeMarshal
	{
		// Token: 0x06000120 RID: 288 RVA: 0x000049D0 File Offset: 0x00002BD0
		internal unsafe static string PtrToUtf8String(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return string.Empty;
			}
			byte* ptr2 = (byte*)((void*)ptr);
			int num = 0;
			try
			{
				while (*(ptr2++) != 0)
				{
					num++;
				}
			}
			catch (NullReferenceException)
			{
				throw new ArgumentOutOfRangeException("ptr", "Value does not refer to a valid string.");
			}
			return new string((sbyte*)((void*)ptr), 0, num, Encoding.UTF8);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004A3C File Offset: 0x00002C3C
		internal static SafeStringMarshal MarshalString(string str)
		{
			return new SafeStringMarshal(str);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004A44 File Offset: 0x00002C44
		private unsafe static int DecodeBlobSize(IntPtr in_ptr, out IntPtr out_ptr)
		{
			byte* ptr = (byte*)((void*)in_ptr);
			uint result;
			if ((*ptr & 128) == 0)
			{
				result = (uint)(*ptr & 127);
				ptr++;
			}
			else if ((*ptr & 64) == 0)
			{
				result = (uint)(((int)(*ptr & 63) << 8) + (int)ptr[1]);
				ptr += 2;
			}
			else
			{
				result = (uint)(((int)(*ptr & 31) << 24) + ((int)ptr[1] << 16) + ((int)ptr[2] << 8) + (int)ptr[3]);
				ptr += 4;
			}
			out_ptr = (IntPtr)((void*)ptr);
			return (int)result;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004AB4 File Offset: 0x00002CB4
		internal static byte[] DecodeBlobArray(IntPtr ptr)
		{
			IntPtr source;
			int num = RuntimeMarshal.DecodeBlobSize(ptr, out source);
			byte[] array = new byte[num];
			Marshal.Copy(source, array, 0, num);
			return array;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004ADB File Offset: 0x00002CDB
		internal static int AsciHexDigitValue(int c)
		{
			if (c >= 48 && c <= 57)
			{
				return c - 48;
			}
			if (c >= 97 && c <= 102)
			{
				return c - 97 + 10;
			}
			return c - 65 + 10;
		}

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void FreeAssemblyName(ref MonoAssemblyName name, bool freeStruct);
	}
}
