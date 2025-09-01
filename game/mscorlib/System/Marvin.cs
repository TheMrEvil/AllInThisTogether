using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000153 RID: 339
	internal static class Marvin
	{
		// Token: 0x06000CC1 RID: 3265 RVA: 0x00032DF0 File Offset: 0x00030FF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ComputeHash32(ReadOnlySpan<byte> data, ulong seed)
		{
			return Marvin.ComputeHash32(MemoryMarshal.GetReference<byte>(data), data.Length, seed);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00032E08 File Offset: 0x00031008
		public unsafe static int ComputeHash32(ref byte data, int count, ulong seed)
		{
			ulong num = (ulong)((long)count);
			uint num2 = (uint)seed;
			uint num3 = (uint)(seed >> 32);
			ulong num4 = 0UL;
			while (num >= 8UL)
			{
				num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
				Marvin.Block(ref num2, ref num3);
				num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4 + 4UL));
				Marvin.Block(ref num2, ref num3);
				num4 += 8UL;
				num -= 8UL;
			}
			ulong num5 = num;
			if (num5 <= 7UL)
			{
				switch ((uint)num5)
				{
				case 0U:
					break;
				case 1U:
					goto IL_CC;
				case 2U:
					goto IL_FC;
				case 3U:
					goto IL_130;
				case 4U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					Marvin.Block(ref num2, ref num3);
					break;
				case 5U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_CC;
				case 6U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_FC;
				case 7U:
					num2 += Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, num4));
					num4 += 4UL;
					Marvin.Block(ref num2, ref num3);
					goto IL_130;
				default:
					goto IL_154;
				}
				num2 += 128U;
				goto IL_154;
				IL_CC:
				num2 += (32768U | (uint)(*Unsafe.AddByteOffset<byte>(ref data, num4)));
				goto IL_154;
				IL_FC:
				num2 += (8388608U | (uint)Unsafe.ReadUnaligned<ushort>(Unsafe.AddByteOffset<byte>(ref data, num4)));
				goto IL_154;
				IL_130:
				num2 += (uint)(int.MinValue | (int)(*Unsafe.AddByteOffset<byte>(ref data, num4 + 2UL)) << 16 | (int)Unsafe.ReadUnaligned<ushort>(Unsafe.AddByteOffset<byte>(ref data, num4)));
			}
			IL_154:
			Marvin.Block(ref num2, ref num3);
			Marvin.Block(ref num2, ref num3);
			return (int)(num3 ^ num2);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x00032F80 File Offset: 0x00031180
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Block(ref uint rp0, ref uint rp1)
		{
			uint num = rp0;
			uint num2 = rp1;
			num2 ^= num;
			num = Marvin._rotl(num, 20);
			num += num2;
			num2 = Marvin._rotl(num2, 9);
			num2 ^= num;
			num = Marvin._rotl(num, 27);
			num += num2;
			num2 = Marvin._rotl(num2, 19);
			rp0 = num;
			rp1 = num2;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00031B0B File Offset: 0x0002FD0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint _rotl(uint value, int shift)
		{
			return value << shift | value >> 32 - shift;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x00032FCD File Offset: 0x000311CD
		public static ulong DefaultSeed
		{
			[CompilerGenerated]
			get
			{
				return Marvin.<DefaultSeed>k__BackingField;
			}
		} = Marvin.GenerateSeed();

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00032FD4 File Offset: 0x000311D4
		private static ulong GenerateSeed()
		{
			return 12874512UL;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00032FDC File Offset: 0x000311DC
		// Note: this type is marked as 'beforefieldinit'.
		static Marvin()
		{
		}

		// Token: 0x04001271 RID: 4721
		[CompilerGenerated]
		private static readonly ulong <DefaultSeed>k__BackingField;
	}
}
