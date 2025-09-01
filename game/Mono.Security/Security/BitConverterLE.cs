using System;

namespace Mono.Security
{
	// Token: 0x02000009 RID: 9
	internal sealed class BitConverterLE
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000034DA File Offset: 0x000016DA
		private BitConverterLE()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000034E2 File Offset: 0x000016E2
		private unsafe static byte[] GetUShortBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1]
				};
			}
			return new byte[]
			{
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003510 File Offset: 0x00001710
		private unsafe static byte[] GetUIntBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3]
				};
			}
			return new byte[]
			{
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003568 File Offset: 0x00001768
		private unsafe static byte[] GetULongBytes(byte* bytes)
		{
			if (BitConverter.IsLittleEndian)
			{
				return new byte[]
				{
					*bytes,
					bytes[1],
					bytes[2],
					bytes[3],
					bytes[4],
					bytes[5],
					bytes[6],
					bytes[7]
				};
			}
			return new byte[]
			{
				bytes[7],
				bytes[6],
				bytes[5],
				bytes[4],
				bytes[3],
				bytes[2],
				bytes[1],
				*bytes
			};
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000035F5 File Offset: 0x000017F5
		internal static byte[] GetBytes(bool value)
		{
			return new byte[]
			{
				value ? 1 : 0
			};
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003607 File Offset: 0x00001807
		internal unsafe static byte[] GetBytes(char value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003611 File Offset: 0x00001811
		internal unsafe static byte[] GetBytes(short value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000361B File Offset: 0x0000181B
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003625 File Offset: 0x00001825
		internal unsafe static byte[] GetBytes(long value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000362F File Offset: 0x0000182F
		internal unsafe static byte[] GetBytes(ushort value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003639 File Offset: 0x00001839
		internal unsafe static byte[] GetBytes(uint value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003643 File Offset: 0x00001843
		internal unsafe static byte[] GetBytes(ulong value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000364D File Offset: 0x0000184D
		internal unsafe static byte[] GetBytes(float value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003657 File Offset: 0x00001857
		internal unsafe static byte[] GetBytes(double value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003661 File Offset: 0x00001861
		private unsafe static void UShortFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				return;
			}
			*dst = src[startIndex + 1];
			dst[1] = src[startIndex];
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003688 File Offset: 0x00001888
		private unsafe static void UIntFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				*dst = src[startIndex];
				dst[1] = src[startIndex + 1];
				dst[2] = src[startIndex + 2];
				dst[3] = src[startIndex + 3];
				return;
			}
			*dst = src[startIndex + 3];
			dst[1] = src[startIndex + 2];
			dst[2] = src[startIndex + 1];
			dst[3] = src[startIndex];
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000036E0 File Offset: 0x000018E0
		private unsafe static void ULongFromBytes(byte* dst, byte[] src, int startIndex)
		{
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < 8; i++)
				{
					dst[i] = src[startIndex + i];
				}
				return;
			}
			for (int j = 0; j < 8; j++)
			{
				dst[j] = src[startIndex + (7 - j)];
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003721 File Offset: 0x00001921
		internal static bool ToBoolean(byte[] value, int startIndex)
		{
			return value[startIndex] > 0;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000372C File Offset: 0x0000192C
		internal unsafe static char ToChar(byte[] value, int startIndex)
		{
			char result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003744 File Offset: 0x00001944
		internal unsafe static short ToInt16(byte[] value, int startIndex)
		{
			short result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000375C File Offset: 0x0000195C
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003774 File Offset: 0x00001974
		internal unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000378C File Offset: 0x0000198C
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000037A4 File Offset: 0x000019A4
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000037BC File Offset: 0x000019BC
		internal unsafe static ulong ToUInt64(byte[] value, int startIndex)
		{
			ulong result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000037D4 File Offset: 0x000019D4
		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000037EC File Offset: 0x000019EC
		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}
	}
}
