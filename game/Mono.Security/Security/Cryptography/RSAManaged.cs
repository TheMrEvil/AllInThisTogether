using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200005E RID: 94
	public class RSAManaged : RSA
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00012C84 File Offset: 0x00010E84
		public RSAManaged() : this(1024)
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00012C91 File Offset: 0x00010E91
		public RSAManaged(int keySize)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = keySize;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00012CCC File Offset: 0x00010ECC
		~RSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00012CFC File Offset: 0x00010EFC
		private void GenerateKeyPair()
		{
			int num = this.KeySize + 1 >> 1;
			int bits = this.KeySize - num;
			this.e = 65537U;
			do
			{
				this.p = BigInteger.GeneratePseudoPrime(num);
			}
			while (this.p % 65537U == 1U);
			for (;;)
			{
				this.q = BigInteger.GeneratePseudoPrime(bits);
				if (this.q % 65537U != 1U && this.p != this.q)
				{
					this.n = this.p * this.q;
					if (this.n.BitCount() == this.KeySize)
					{
						break;
					}
					if (this.p < this.q)
					{
						this.p = this.q;
					}
				}
			}
			BigInteger bigInteger = this.p - 1;
			BigInteger bi = this.q - 1;
			BigInteger modulus = bigInteger * bi;
			this.d = this.e.ModInverse(modulus);
			this.dp = this.d % bigInteger;
			this.dq = this.d % bi;
			this.qInv = this.q.ModInverse(this.p);
			this.keypairGenerated = true;
			this.isCRTpossible = true;
			if (this.KeyGenerated != null)
			{
				this.KeyGenerated(this, null);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00012E68 File Offset: 0x00011068
		public override int KeySize
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
				}
				if (this.keypairGenerated)
				{
					int num = this.n.BitCount();
					if ((num & 7) != 0)
					{
						num += 8 - (num & 7);
					}
					return num;
				}
				return base.KeySize;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600039D RID: 925 RVA: 0x00012EB6 File Offset: 0x000110B6
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00012EBD File Offset: 0x000110BD
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && (this.d == null || this.n == null);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00012EE5 File Offset: 0x000110E5
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012EEC File Offset: 0x000110EC
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("private key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = null;
			if (this.keyBlinding)
			{
				bigInteger2 = BigInteger.GenerateRandom(this.n.BitCount());
				bigInteger = bigInteger2.ModPow(this.e, this.n) * bigInteger % this.n;
			}
			BigInteger bigInteger5;
			if (this.isCRTpossible)
			{
				BigInteger bigInteger3 = bigInteger.ModPow(this.dp, this.p);
				BigInteger bigInteger4 = bigInteger.ModPow(this.dq, this.q);
				if (bigInteger4 > bigInteger3)
				{
					BigInteger bi = this.p - (bigInteger4 - bigInteger3) * this.qInv % this.p;
					bigInteger5 = bigInteger4 + this.q * bi;
				}
				else
				{
					BigInteger bi = (bigInteger3 - bigInteger4) * this.qInv % this.p;
					bigInteger5 = bigInteger4 + this.q * bi;
				}
			}
			else
			{
				if (this.PublicOnly)
				{
					throw new CryptographicException(Locale.GetText("Missing private key to decrypt value."));
				}
				bigInteger5 = bigInteger.ModPow(this.d, this.n);
			}
			if (this.keyBlinding)
			{
				bigInteger5 = bigInteger5 * bigInteger2.ModInverse(this.n) % this.n;
				bigInteger2.Clear();
			}
			byte[] paddedValue = this.GetPaddedValue(bigInteger5, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger5.Clear();
			return paddedValue;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001308C File Offset: 0x0001128C
		public override byte[] EncryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("public key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = bigInteger.ModPow(this.e, this.n);
			byte[] paddedValue = this.GetPaddedValue(bigInteger2, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger2.Clear();
			return paddedValue;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000130F0 File Offset: 0x000112F0
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Exponent = this.e.GetBytes();
			rsaparameters.Modulus = this.n.GetBytes();
			if (includePrivateParameters)
			{
				if (this.d == null)
				{
					throw new CryptographicException("Missing private key");
				}
				rsaparameters.D = this.d.GetBytes();
				if (rsaparameters.D.Length != rsaparameters.Modulus.Length)
				{
					byte[] array = new byte[rsaparameters.Modulus.Length];
					Buffer.BlockCopy(rsaparameters.D, 0, array, array.Length - rsaparameters.D.Length, rsaparameters.D.Length);
					rsaparameters.D = array;
				}
				if (this.p != null && this.q != null && this.dp != null && this.dq != null && this.qInv != null)
				{
					int length = this.KeySize >> 4;
					rsaparameters.P = this.GetPaddedValue(this.p, length);
					rsaparameters.Q = this.GetPaddedValue(this.q, length);
					rsaparameters.DP = this.GetPaddedValue(this.dp, length);
					rsaparameters.DQ = this.GetPaddedValue(this.dq, length);
					rsaparameters.InverseQ = this.GetPaddedValue(this.qInv, length);
				}
			}
			return rsaparameters;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00013288 File Offset: 0x00011488
		public override void ImportParameters(RSAParameters parameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (parameters.Exponent == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Exponent"));
			}
			if (parameters.Modulus == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Modulus"));
			}
			this.e = new BigInteger(parameters.Exponent);
			this.n = new BigInteger(parameters.Modulus);
			this.d = (this.dp = (this.dq = (this.qInv = (this.p = (this.q = null)))));
			if (parameters.D != null)
			{
				this.d = new BigInteger(parameters.D);
			}
			if (parameters.DP != null)
			{
				this.dp = new BigInteger(parameters.DP);
			}
			if (parameters.DQ != null)
			{
				this.dq = new BigInteger(parameters.DQ);
			}
			if (parameters.InverseQ != null)
			{
				this.qInv = new BigInteger(parameters.InverseQ);
			}
			if (parameters.P != null)
			{
				this.p = new BigInteger(parameters.P);
			}
			if (parameters.Q != null)
			{
				this.q = new BigInteger(parameters.Q);
			}
			this.keypairGenerated = true;
			bool flag = this.p != null && this.q != null && this.dp != null;
			this.isCRTpossible = (flag && this.dq != null && this.qInv != null);
			if (!flag)
			{
				return;
			}
			bool flag2 = this.n == this.p * this.q;
			if (flag2)
			{
				BigInteger bigInteger = this.p - 1;
				BigInteger bi = this.q - 1;
				BigInteger modulus = bigInteger * bi;
				BigInteger bigInteger2 = this.e.ModInverse(modulus);
				flag2 = (this.d == bigInteger2);
				if (!flag2 && this.isCRTpossible)
				{
					flag2 = (this.dp == bigInteger2 % bigInteger && this.dq == bigInteger2 % bi && this.qInv == this.q.ModInverse(this.p));
				}
			}
			if (!flag2)
			{
				throw new CryptographicException(Locale.GetText("Private/public key mismatch"));
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00013500 File Offset: 0x00011700
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.d != null)
				{
					this.d.Clear();
					this.d = null;
				}
				if (this.p != null)
				{
					this.p.Clear();
					this.p = null;
				}
				if (this.q != null)
				{
					this.q.Clear();
					this.q = null;
				}
				if (this.dp != null)
				{
					this.dp.Clear();
					this.dp = null;
				}
				if (this.dq != null)
				{
					this.dq.Clear();
					this.dq = null;
				}
				if (this.qInv != null)
				{
					this.qInv.Clear();
					this.qInv = null;
				}
				if (disposing)
				{
					if (this.e != null)
					{
						this.e.Clear();
						this.e = null;
					}
					if (this.n != null)
					{
						this.n.Clear();
						this.n = null;
					}
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003A5 RID: 933 RVA: 0x00013624 File Offset: 0x00011824
		// (remove) Token: 0x060003A6 RID: 934 RVA: 0x0001365C File Offset: 0x0001185C
		public event RSAManaged.KeyGeneratedEventHandler KeyGenerated
		{
			[CompilerGenerated]
			add
			{
				RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler = this.KeyGenerated;
				RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler2;
				do
				{
					keyGeneratedEventHandler2 = keyGeneratedEventHandler;
					RSAManaged.KeyGeneratedEventHandler value2 = (RSAManaged.KeyGeneratedEventHandler)Delegate.Combine(keyGeneratedEventHandler2, value);
					keyGeneratedEventHandler = Interlocked.CompareExchange<RSAManaged.KeyGeneratedEventHandler>(ref this.KeyGenerated, value2, keyGeneratedEventHandler2);
				}
				while (keyGeneratedEventHandler != keyGeneratedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler = this.KeyGenerated;
				RSAManaged.KeyGeneratedEventHandler keyGeneratedEventHandler2;
				do
				{
					keyGeneratedEventHandler2 = keyGeneratedEventHandler;
					RSAManaged.KeyGeneratedEventHandler value2 = (RSAManaged.KeyGeneratedEventHandler)Delegate.Remove(keyGeneratedEventHandler2, value);
					keyGeneratedEventHandler = Interlocked.CompareExchange<RSAManaged.KeyGeneratedEventHandler>(ref this.KeyGenerated, value2, keyGeneratedEventHandler2);
				}
				while (keyGeneratedEventHandler != keyGeneratedEventHandler2);
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00013694 File Offset: 0x00011894
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<RSAKeyValue>");
				stringBuilder.Append("<Modulus>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Modulus));
				stringBuilder.Append("</Modulus>");
				stringBuilder.Append("<Exponent>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Exponent));
				stringBuilder.Append("</Exponent>");
				if (includePrivateParameters)
				{
					if (rsaparameters.P != null)
					{
						stringBuilder.Append("<P>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.P));
						stringBuilder.Append("</P>");
					}
					if (rsaparameters.Q != null)
					{
						stringBuilder.Append("<Q>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.Q));
						stringBuilder.Append("</Q>");
					}
					if (rsaparameters.DP != null)
					{
						stringBuilder.Append("<DP>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DP));
						stringBuilder.Append("</DP>");
					}
					if (rsaparameters.DQ != null)
					{
						stringBuilder.Append("<DQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DQ));
						stringBuilder.Append("</DQ>");
					}
					if (rsaparameters.InverseQ != null)
					{
						stringBuilder.Append("<InverseQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.InverseQ));
						stringBuilder.Append("</InverseQ>");
					}
					stringBuilder.Append("<D>");
					stringBuilder.Append(Convert.ToBase64String(rsaparameters.D));
					stringBuilder.Append("</D>");
				}
				stringBuilder.Append("</RSAKeyValue>");
			}
			catch
			{
				if (rsaparameters.P != null)
				{
					Array.Clear(rsaparameters.P, 0, rsaparameters.P.Length);
				}
				if (rsaparameters.Q != null)
				{
					Array.Clear(rsaparameters.Q, 0, rsaparameters.Q.Length);
				}
				if (rsaparameters.DP != null)
				{
					Array.Clear(rsaparameters.DP, 0, rsaparameters.DP.Length);
				}
				if (rsaparameters.DQ != null)
				{
					Array.Clear(rsaparameters.DQ, 0, rsaparameters.DQ.Length);
				}
				if (rsaparameters.InverseQ != null)
				{
					Array.Clear(rsaparameters.InverseQ, 0, rsaparameters.InverseQ.Length);
				}
				if (rsaparameters.D != null)
				{
					Array.Clear(rsaparameters.D, 0, rsaparameters.D.Length);
				}
				throw;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00013918 File Offset: 0x00011B18
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00013920 File Offset: 0x00011B20
		public bool UseKeyBlinding
		{
			get
			{
				return this.keyBlinding;
			}
			set
			{
				this.keyBlinding = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00013929 File Offset: 0x00011B29
		public bool IsCrtPossible
		{
			get
			{
				return !this.keypairGenerated || this.isCRTpossible;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001393C File Offset: 0x00011B3C
		private byte[] GetPaddedValue(BigInteger value, int length)
		{
			byte[] bytes = value.GetBytes();
			if (bytes.Length >= length)
			{
				return bytes;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(bytes, 0, array, length - bytes.Length, bytes.Length);
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x040002DC RID: 732
		private const int defaultKeySize = 1024;

		// Token: 0x040002DD RID: 733
		private bool isCRTpossible;

		// Token: 0x040002DE RID: 734
		private bool keyBlinding = true;

		// Token: 0x040002DF RID: 735
		private bool keypairGenerated;

		// Token: 0x040002E0 RID: 736
		private bool m_disposed;

		// Token: 0x040002E1 RID: 737
		private BigInteger d;

		// Token: 0x040002E2 RID: 738
		private BigInteger p;

		// Token: 0x040002E3 RID: 739
		private BigInteger q;

		// Token: 0x040002E4 RID: 740
		private BigInteger dp;

		// Token: 0x040002E5 RID: 741
		private BigInteger dq;

		// Token: 0x040002E6 RID: 742
		private BigInteger qInv;

		// Token: 0x040002E7 RID: 743
		private BigInteger n;

		// Token: 0x040002E8 RID: 744
		private BigInteger e;

		// Token: 0x040002E9 RID: 745
		[CompilerGenerated]
		private RSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x020000A1 RID: 161
		// (Invoke) Token: 0x0600057C RID: 1404
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
