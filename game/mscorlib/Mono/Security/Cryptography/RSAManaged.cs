using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000090 RID: 144
	internal class RSAManaged : RSA
	{
		// Token: 0x06000337 RID: 823 RVA: 0x00010E4C File Offset: 0x0000F04C
		public RSAManaged() : this(1024)
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010E59 File Offset: 0x0000F059
		public RSAManaged(int keySize)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = keySize;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010E94 File Offset: 0x0000F094
		~RSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010EC4 File Offset: 0x0000F0C4
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00011030 File Offset: 0x0000F230
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0001107E File Offset: 0x0000F27E
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00011085 File Offset: 0x0000F285
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && (this.d == null || this.n == null);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600033E RID: 830 RVA: 0x000110AD File Offset: 0x0000F2AD
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000110B4 File Offset: 0x0000F2B4
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

		// Token: 0x06000340 RID: 832 RVA: 0x00011254 File Offset: 0x0000F454
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

		// Token: 0x06000341 RID: 833 RVA: 0x000112B8 File Offset: 0x0000F4B8
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

		// Token: 0x06000342 RID: 834 RVA: 0x00011450 File Offset: 0x0000F650
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

		// Token: 0x06000343 RID: 835 RVA: 0x000116C8 File Offset: 0x0000F8C8
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
		// (add) Token: 0x06000344 RID: 836 RVA: 0x000117EC File Offset: 0x0000F9EC
		// (remove) Token: 0x06000345 RID: 837 RVA: 0x00011824 File Offset: 0x0000FA24
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

		// Token: 0x06000346 RID: 838 RVA: 0x0001185C File Offset: 0x0000FA5C
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00011AE8 File Offset: 0x0000FCE8
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

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00011AF1 File Offset: 0x0000FCF1
		public bool IsCrtPossible
		{
			get
			{
				return !this.keypairGenerated || this.isCRTpossible;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00011B04 File Offset: 0x0000FD04
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

		// Token: 0x04000F01 RID: 3841
		private const int defaultKeySize = 1024;

		// Token: 0x04000F02 RID: 3842
		private bool isCRTpossible;

		// Token: 0x04000F03 RID: 3843
		private bool keyBlinding = true;

		// Token: 0x04000F04 RID: 3844
		private bool keypairGenerated;

		// Token: 0x04000F05 RID: 3845
		private bool m_disposed;

		// Token: 0x04000F06 RID: 3846
		private BigInteger d;

		// Token: 0x04000F07 RID: 3847
		private BigInteger p;

		// Token: 0x04000F08 RID: 3848
		private BigInteger q;

		// Token: 0x04000F09 RID: 3849
		private BigInteger dp;

		// Token: 0x04000F0A RID: 3850
		private BigInteger dq;

		// Token: 0x04000F0B RID: 3851
		private BigInteger qInv;

		// Token: 0x04000F0C RID: 3852
		private BigInteger n;

		// Token: 0x04000F0D RID: 3853
		private BigInteger e;

		// Token: 0x04000F0E RID: 3854
		[CompilerGenerated]
		private RSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x0600034C RID: 844
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
