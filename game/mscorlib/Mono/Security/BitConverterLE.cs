using System;

namespace Mono.Security
{
	// Token: 0x0200007C RID: 124
	internal sealed class BitConverterLE
	{
		// Token: 0x0600021B RID: 539 RVA: 0x0000259F File Offset: 0x0000079F
		private BitConverterLE()
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A8A6 File Offset: 0x00008AA6
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

		// Token: 0x0600021D RID: 541 RVA: 0x0000A8D4 File Offset: 0x00008AD4
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

		// Token: 0x0600021E RID: 542 RVA: 0x0000A92C File Offset: 0x00008B2C
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

		// Token: 0x0600021F RID: 543 RVA: 0x0000A9B9 File Offset: 0x00008BB9
		internal static byte[] GetBytes(bool value)
		{
			return new byte[]
			{
				value ? 1 : 0
			};
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000A9CB File Offset: 0x00008BCB
		internal unsafe static byte[] GetBytes(char value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000A9CB File Offset: 0x00008BCB
		internal unsafe static byte[] GetBytes(short value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000A9D5 File Offset: 0x00008BD5
		internal unsafe static byte[] GetBytes(int value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000A9DF File Offset: 0x00008BDF
		internal unsafe static byte[] GetBytes(long value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000A9CB File Offset: 0x00008BCB
		internal unsafe static byte[] GetBytes(ushort value)
		{
			return BitConverterLE.GetUShortBytes((byte*)(&value));
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000A9D5 File Offset: 0x00008BD5
		internal unsafe static byte[] GetBytes(uint value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000A9DF File Offset: 0x00008BDF
		internal unsafe static byte[] GetBytes(ulong value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A9D5 File Offset: 0x00008BD5
		internal unsafe static byte[] GetBytes(float value)
		{
			return BitConverterLE.GetUIntBytes((byte*)(&value));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000A9DF File Offset: 0x00008BDF
		internal unsafe static byte[] GetBytes(double value)
		{
			return BitConverterLE.GetULongBytes((byte*)(&value));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A9E9 File Offset: 0x00008BE9
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

		// Token: 0x0600022A RID: 554 RVA: 0x0000AA10 File Offset: 0x00008C10
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

		// Token: 0x0600022B RID: 555 RVA: 0x0000AA68 File Offset: 0x00008C68
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

		// Token: 0x0600022C RID: 556 RVA: 0x0000AAA9 File Offset: 0x00008CA9
		internal static bool ToBoolean(byte[] value, int startIndex)
		{
			return value[startIndex] > 0;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		internal unsafe static char ToChar(byte[] value, int startIndex)
		{
			char result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AACC File Offset: 0x00008CCC
		internal unsafe static short ToInt16(byte[] value, int startIndex)
		{
			short result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000AAE4 File Offset: 0x00008CE4
		internal unsafe static int ToInt32(byte[] value, int startIndex)
		{
			int result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000AAFC File Offset: 0x00008CFC
		internal unsafe static long ToInt64(byte[] value, int startIndex)
		{
			long result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000AB14 File Offset: 0x00008D14
		internal unsafe static ushort ToUInt16(byte[] value, int startIndex)
		{
			ushort result;
			BitConverterLE.UShortFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000AB2C File Offset: 0x00008D2C
		internal unsafe static uint ToUInt32(byte[] value, int startIndex)
		{
			uint result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000AB44 File Offset: 0x00008D44
		internal unsafe static ulong ToUInt64(byte[] value, int startIndex)
		{
			ulong result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000AB5C File Offset: 0x00008D5C
		internal unsafe static float ToSingle(byte[] value, int startIndex)
		{
			float result;
			BitConverterLE.UIntFromBytes((byte*)(&result), value, startIndex);
			return result;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000AB74 File Offset: 0x00008D74
		internal unsafe static double ToDouble(byte[] value, int startIndex)
		{
			double result;
			BitConverterLE.ULongFromBytes((byte*)(&result), value, startIndex);
			return result;
		}
	}
}
