using System;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008B RID: 139
	internal class MD2Managed : MD2
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000F990 File Offset: 0x0000DB90
		private byte[] Padding(int nLength)
		{
			if (nLength > 0)
			{
				byte[] array = new byte[nLength];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (byte)nLength;
				}
				return array;
			}
			return null;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		public MD2Managed()
		{
			this.state = new byte[16];
			this.checksum = new byte[16];
			this.buffer = new byte[16];
			this.x = new byte[48];
			this.Initialize();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000FA10 File Offset: 0x0000DC10
		public override void Initialize()
		{
			this.count = 0;
			Array.Clear(this.state, 0, 16);
			Array.Clear(this.checksum, 0, 16);
			Array.Clear(this.buffer, 0, 16);
			Array.Clear(this.x, 0, 48);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			int num = this.count;
			this.count = (num + cbSize & 15);
			int num2 = 16 - num;
			int num3;
			if (cbSize >= num2)
			{
				Buffer.BlockCopy(array, ibStart, this.buffer, num, num2);
				this.MD2Transform(this.state, this.checksum, this.buffer, 0);
				num3 = num2;
				while (num3 + 15 < cbSize)
				{
					this.MD2Transform(this.state, this.checksum, array, ibStart + num3);
					num3 += 16;
				}
				num = 0;
			}
			else
			{
				num3 = 0;
			}
			Buffer.BlockCopy(array, ibStart + num3, this.buffer, num, cbSize - num3);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000FAEC File Offset: 0x0000DCEC
		protected override byte[] HashFinal()
		{
			int num = this.count;
			int num2 = 16 - num;
			if (num2 > 0)
			{
				this.HashCore(this.Padding(num2), 0, num2);
			}
			this.HashCore(this.checksum, 0, 16);
			byte[] result = (byte[])this.state.Clone();
			this.Initialize();
			return result;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000FB40 File Offset: 0x0000DD40
		private void MD2Transform(byte[] state, byte[] checksum, byte[] block, int index)
		{
			Buffer.BlockCopy(state, 0, this.x, 0, 16);
			Buffer.BlockCopy(block, index, this.x, 16, 16);
			for (int i = 0; i < 16; i++)
			{
				this.x[i + 32] = (state[i] ^ block[index + i]);
			}
			int num = 0;
			for (int j = 0; j < 18; j++)
			{
				for (int k = 0; k < 48; k++)
				{
					byte[] array = this.x;
					int num2 = k;
					num = (int)(array[num2] ^= MD2Managed.PI_SUBST[num]);
				}
				num = (num + j & 255);
			}
			Buffer.BlockCopy(this.x, 0, state, 0, 16);
			num = (int)checksum[15];
			for (int l = 0; l < 16; l++)
			{
				int num3 = l;
				num = (int)(checksum[num3] ^= MD2Managed.PI_SUBST[(int)block[index + l] ^ num]);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000FC1E File Offset: 0x0000DE1E
		// Note: this type is marked as 'beforefieldinit'.
		static MD2Managed()
		{
		}

		// Token: 0x04000EE4 RID: 3812
		private byte[] state;

		// Token: 0x04000EE5 RID: 3813
		private byte[] checksum;

		// Token: 0x04000EE6 RID: 3814
		private byte[] buffer;

		// Token: 0x04000EE7 RID: 3815
		private int count;

		// Token: 0x04000EE8 RID: 3816
		private byte[] x;

		// Token: 0x04000EE9 RID: 3817
		private static readonly byte[] PI_SUBST = new byte[]
		{
			41,
			46,
			67,
			201,
			162,
			216,
			124,
			1,
			61,
			54,
			84,
			161,
			236,
			240,
			6,
			19,
			98,
			167,
			5,
			243,
			192,
			199,
			115,
			140,
			152,
			147,
			43,
			217,
			188,
			76,
			130,
			202,
			30,
			155,
			87,
			60,
			253,
			212,
			224,
			22,
			103,
			66,
			111,
			24,
			138,
			23,
			229,
			18,
			190,
			78,
			196,
			214,
			218,
			158,
			222,
			73,
			160,
			251,
			245,
			142,
			187,
			47,
			238,
			122,
			169,
			104,
			121,
			145,
			21,
			178,
			7,
			63,
			148,
			194,
			16,
			137,
			11,
			34,
			95,
			33,
			128,
			127,
			93,
			154,
			90,
			144,
			50,
			39,
			53,
			62,
			204,
			231,
			191,
			247,
			151,
			3,
			byte.MaxValue,
			25,
			48,
			179,
			72,
			165,
			181,
			209,
			215,
			94,
			146,
			42,
			172,
			86,
			170,
			198,
			79,
			184,
			56,
			210,
			150,
			164,
			125,
			182,
			118,
			252,
			107,
			226,
			156,
			116,
			4,
			241,
			69,
			157,
			112,
			89,
			100,
			113,
			135,
			32,
			134,
			91,
			207,
			101,
			230,
			45,
			168,
			2,
			27,
			96,
			37,
			173,
			174,
			176,
			185,
			246,
			28,
			70,
			97,
			105,
			52,
			64,
			126,
			15,
			85,
			71,
			163,
			35,
			221,
			81,
			175,
			58,
			195,
			92,
			249,
			206,
			186,
			197,
			234,
			38,
			44,
			83,
			13,
			110,
			133,
			40,
			132,
			9,
			211,
			223,
			205,
			244,
			65,
			129,
			77,
			82,
			106,
			220,
			55,
			200,
			108,
			193,
			171,
			250,
			36,
			225,
			123,
			8,
			12,
			189,
			177,
			74,
			120,
			136,
			149,
			139,
			227,
			99,
			232,
			109,
			233,
			203,
			213,
			254,
			59,
			0,
			29,
			57,
			242,
			239,
			183,
			14,
			102,
			88,
			208,
			228,
			166,
			119,
			114,
			248,
			235,
			117,
			75,
			10,
			49,
			68,
			80,
			180,
			143,
			237,
			31,
			26,
			219,
			153,
			141,
			51,
			159,
			17,
			131,
			20
		};
	}
}
