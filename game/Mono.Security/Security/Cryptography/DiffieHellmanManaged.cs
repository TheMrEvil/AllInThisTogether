using System;
using System.Security.Cryptography;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000055 RID: 85
	public sealed class DiffieHellmanManaged : DiffieHellman
	{
		// Token: 0x0600032E RID: 814 RVA: 0x00010B28 File Offset: 0x0000ED28
		public DiffieHellmanManaged() : this(1024, 160, DHKeyGeneration.Static)
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00010B3C File Offset: 0x0000ED3C
		public DiffieHellmanManaged(int bitLength, int l, DHKeyGeneration method)
		{
			if (bitLength < 256 || l < 0)
			{
				throw new ArgumentException();
			}
			BigInteger p;
			BigInteger g;
			this.GenerateKey(bitLength, method, out p, out g);
			this.Initialize(p, g, null, l, false);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010B78 File Offset: 0x0000ED78
		public DiffieHellmanManaged(byte[] p, byte[] g, byte[] x)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (x == null)
			{
				this.Initialize(new BigInteger(p), new BigInteger(g), null, 0, true);
				return;
			}
			this.Initialize(new BigInteger(p), new BigInteger(g), new BigInteger(x), 0, true);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00010BCA File Offset: 0x0000EDCA
		public DiffieHellmanManaged(byte[] p, byte[] g, int l)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (l < 0)
			{
				throw new ArgumentException();
			}
			this.Initialize(new BigInteger(p), new BigInteger(g), null, l, true);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00010C00 File Offset: 0x0000EE00
		private void Initialize(BigInteger p, BigInteger g, BigInteger x, int secretLen, bool checkInput)
		{
			if (checkInput && (!p.IsProbablePrime() || g <= 0 || g >= p || (x != null && (x <= 0 || x > p - 2))))
			{
				throw new CryptographicException();
			}
			if (secretLen == 0)
			{
				secretLen = p.BitCount();
			}
			this.m_P = p;
			this.m_G = g;
			if (x == null)
			{
				BigInteger bi = this.m_P - 1;
				this.m_X = BigInteger.GenerateRandom(secretLen);
				while (this.m_X >= bi || this.m_X == 0U)
				{
					this.m_X = BigInteger.GenerateRandom(secretLen);
				}
				return;
			}
			this.m_X = x;
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010CD8 File Offset: 0x0000EED8
		public override byte[] CreateKeyExchange()
		{
			BigInteger bigInteger = this.m_G.ModPow(this.m_X, this.m_P);
			byte[] bytes = bigInteger.GetBytes();
			bigInteger.Clear();
			return bytes;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010D0C File Offset: 0x0000EF0C
		public override byte[] DecryptKeyExchange(byte[] keyEx)
		{
			BigInteger bigInteger = new BigInteger(keyEx).ModPow(this.m_X, this.m_P);
			byte[] bytes = bigInteger.GetBytes();
			bigInteger.Clear();
			return bytes;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00010D3D File Offset: 0x0000EF3D
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "1.2.840.113549.1.3";
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00010D44 File Offset: 0x0000EF44
		public override string SignatureAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010D48 File Offset: 0x0000EF48
		protected override void Dispose(bool disposing)
		{
			if (!this.m_Disposed)
			{
				if (this.m_P != null)
				{
					this.m_P.Clear();
				}
				if (this.m_G != null)
				{
					this.m_G.Clear();
				}
				if (this.m_X != null)
				{
					this.m_X.Clear();
				}
			}
			this.m_Disposed = true;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public override DHParameters ExportParameters(bool includePrivateParameters)
		{
			DHParameters result = default(DHParameters);
			result.P = this.m_P.GetBytes();
			result.G = this.m_G.GetBytes();
			if (includePrivateParameters)
			{
				result.X = this.m_X.GetBytes();
			}
			return result;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010E00 File Offset: 0x0000F000
		public override void ImportParameters(DHParameters parameters)
		{
			if (parameters.P == null)
			{
				throw new CryptographicException("Missing P value.");
			}
			if (parameters.G == null)
			{
				throw new CryptographicException("Missing G value.");
			}
			BigInteger p = new BigInteger(parameters.P);
			BigInteger g = new BigInteger(parameters.G);
			BigInteger x = null;
			if (parameters.X != null)
			{
				x = new BigInteger(parameters.X);
			}
			this.Initialize(p, g, x, 0, true);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010E6C File Offset: 0x0000F06C
		~DiffieHellmanManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010E9C File Offset: 0x0000F09C
		private void GenerateKey(int bitlen, DHKeyGeneration keygen, out BigInteger p, out BigInteger g)
		{
			if (keygen == DHKeyGeneration.Static)
			{
				if (bitlen == 768)
				{
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY768);
				}
				else if (bitlen == 1024)
				{
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY1024);
				}
				else
				{
					if (bitlen != 1536)
					{
						throw new ArgumentException("Invalid bit size.");
					}
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY1536);
				}
				g = new BigInteger(22U);
				return;
			}
			p = BigInteger.GeneratePseudoPrime(bitlen);
			g = new BigInteger(3U);
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010F18 File Offset: 0x0000F118
		// Note: this type is marked as 'beforefieldinit'.
		static DiffieHellmanManaged()
		{
		}

		// Token: 0x040002AF RID: 687
		private BigInteger m_P;

		// Token: 0x040002B0 RID: 688
		private BigInteger m_G;

		// Token: 0x040002B1 RID: 689
		private BigInteger m_X;

		// Token: 0x040002B2 RID: 690
		private bool m_Disposed;

		// Token: 0x040002B3 RID: 691
		private static byte[] m_OAKLEY768 = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			201,
			15,
			218,
			162,
			33,
			104,
			194,
			52,
			196,
			198,
			98,
			139,
			128,
			220,
			28,
			209,
			41,
			2,
			78,
			8,
			138,
			103,
			204,
			116,
			2,
			11,
			190,
			166,
			59,
			19,
			155,
			34,
			81,
			74,
			8,
			121,
			142,
			52,
			4,
			221,
			239,
			149,
			25,
			179,
			205,
			58,
			67,
			27,
			48,
			43,
			10,
			109,
			242,
			95,
			20,
			55,
			79,
			225,
			53,
			109,
			109,
			81,
			194,
			69,
			228,
			133,
			181,
			118,
			98,
			94,
			126,
			198,
			244,
			76,
			66,
			233,
			166,
			58,
			54,
			32,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x040002B4 RID: 692
		private static byte[] m_OAKLEY1024 = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			201,
			15,
			218,
			162,
			33,
			104,
			194,
			52,
			196,
			198,
			98,
			139,
			128,
			220,
			28,
			209,
			41,
			2,
			78,
			8,
			138,
			103,
			204,
			116,
			2,
			11,
			190,
			166,
			59,
			19,
			155,
			34,
			81,
			74,
			8,
			121,
			142,
			52,
			4,
			221,
			239,
			149,
			25,
			179,
			205,
			58,
			67,
			27,
			48,
			43,
			10,
			109,
			242,
			95,
			20,
			55,
			79,
			225,
			53,
			109,
			109,
			81,
			194,
			69,
			228,
			133,
			181,
			118,
			98,
			94,
			126,
			198,
			244,
			76,
			66,
			233,
			166,
			55,
			237,
			107,
			11,
			byte.MaxValue,
			92,
			182,
			244,
			6,
			183,
			237,
			238,
			56,
			107,
			251,
			90,
			137,
			159,
			165,
			174,
			159,
			36,
			17,
			124,
			75,
			31,
			230,
			73,
			40,
			102,
			81,
			236,
			230,
			83,
			129,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x040002B5 RID: 693
		private static byte[] m_OAKLEY1536 = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			201,
			15,
			218,
			162,
			33,
			104,
			194,
			52,
			196,
			198,
			98,
			139,
			128,
			220,
			28,
			209,
			41,
			2,
			78,
			8,
			138,
			103,
			204,
			116,
			2,
			11,
			190,
			166,
			59,
			19,
			155,
			34,
			81,
			74,
			8,
			121,
			142,
			52,
			4,
			221,
			239,
			149,
			25,
			179,
			205,
			58,
			67,
			27,
			48,
			43,
			10,
			109,
			242,
			95,
			20,
			55,
			79,
			225,
			53,
			109,
			109,
			81,
			194,
			69,
			228,
			133,
			181,
			118,
			98,
			94,
			126,
			198,
			244,
			76,
			66,
			233,
			166,
			55,
			237,
			107,
			11,
			byte.MaxValue,
			92,
			182,
			244,
			6,
			183,
			237,
			238,
			56,
			107,
			251,
			90,
			137,
			159,
			165,
			174,
			159,
			36,
			17,
			124,
			75,
			31,
			230,
			73,
			40,
			102,
			81,
			236,
			228,
			91,
			61,
			194,
			0,
			124,
			184,
			161,
			99,
			191,
			5,
			152,
			218,
			72,
			54,
			28,
			85,
			211,
			154,
			105,
			22,
			63,
			168,
			253,
			36,
			207,
			95,
			131,
			101,
			93,
			35,
			220,
			163,
			173,
			150,
			28,
			98,
			243,
			86,
			32,
			133,
			82,
			187,
			158,
			213,
			41,
			7,
			112,
			150,
			150,
			109,
			103,
			12,
			53,
			78,
			74,
			188,
			152,
			4,
			241,
			116,
			108,
			8,
			202,
			35,
			115,
			39,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};
	}
}
