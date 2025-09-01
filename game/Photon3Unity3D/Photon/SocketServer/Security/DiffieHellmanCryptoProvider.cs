using System;
using System.Numerics;
using System.Security.Cryptography;

namespace Photon.SocketServer.Security
{
	// Token: 0x02000049 RID: 73
	internal class DiffieHellmanCryptoProvider : ICryptoProvider, IDisposable
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0001BDAD File Offset: 0x00019FAD
		public DiffieHellmanCryptoProvider()
		{
			this.prime = new BigInteger(OakleyGroups.OakleyPrime768);
			this.secret = this.GenerateRandomSecret(160);
			this.publicKey = this.CalculatePublicKey();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		public DiffieHellmanCryptoProvider(byte[] cryptoKey)
		{
			this.crypto = new RijndaelManaged();
			this.crypto.Key = cryptoKey;
			this.crypto.IV = new byte[16];
			this.crypto.Padding = PaddingMode.PKCS7;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001BE34 File Offset: 0x0001A034
		public bool IsInitialized
		{
			get
			{
				return this.crypto != null;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001BE50 File Offset: 0x0001A050
		public byte[] PublicKey
		{
			get
			{
				return this.MsBigIntArrayToPhotonBigIntArray(this.publicKey.ToByteArray());
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001BE78 File Offset: 0x0001A078
		public void DeriveSharedKey(byte[] otherPartyPublicKey)
		{
			otherPartyPublicKey = this.PhotonBigIntArrayToMsBigIntArray(otherPartyPublicKey);
			BigInteger otherPartyPublicKey2 = new BigInteger(otherPartyPublicKey);
			this.sharedKey = this.MsBigIntArrayToPhotonBigIntArray(this.CalculateSharedKey(otherPartyPublicKey2).ToByteArray());
			byte[] key;
			using (SHA256 sha = new SHA256Managed())
			{
				key = sha.ComputeHash(this.sharedKey);
			}
			this.crypto = new RijndaelManaged();
			this.crypto.Key = key;
			this.crypto.IV = new byte[16];
			this.crypto.Padding = PaddingMode.PKCS7;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001BF1C File Offset: 0x0001A11C
		private byte[] PhotonBigIntArrayToMsBigIntArray(byte[] array)
		{
			Array.Reverse(array);
			bool flag = (array[array.Length - 1] & 128) == 128;
			byte[] result;
			if (flag)
			{
				byte[] array2 = new byte[array.Length + 1];
				Buffer.BlockCopy(array, 0, array2, 0, array.Length);
				result = array2;
			}
			else
			{
				result = array;
			}
			return result;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001BF6C File Offset: 0x0001A16C
		private byte[] MsBigIntArrayToPhotonBigIntArray(byte[] array)
		{
			Array.Reverse(array);
			bool flag = array[0] == 0;
			byte[] result;
			if (flag)
			{
				byte[] array2 = new byte[array.Length - 1];
				Buffer.BlockCopy(array, 1, array2, 0, array.Length - 1);
				result = array2;
			}
			else
			{
				result = array;
			}
			return result;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001BFB0 File Offset: 0x0001A1B0
		public byte[] Encrypt(byte[] data)
		{
			return this.Encrypt(data, 0, data.Length);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
		public byte[] Encrypt(byte[] data, int offset, int count)
		{
			byte[] result;
			using (ICryptoTransform cryptoTransform = this.crypto.CreateEncryptor())
			{
				result = cryptoTransform.TransformFinalBlock(data, offset, count);
			}
			return result;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001C014 File Offset: 0x0001A214
		public byte[] Decrypt(byte[] data)
		{
			return this.Decrypt(data, 0, data.Length);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001C034 File Offset: 0x0001A234
		public byte[] Decrypt(byte[] data, int offset, int count)
		{
			byte[] result;
			using (ICryptoTransform cryptoTransform = this.crypto.CreateDecryptor())
			{
				result = cryptoTransform.TransformFinalBlock(data, offset, count);
			}
			return result;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001C078 File Offset: 0x0001A278
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001C08C File Offset: 0x0001A28C
		protected void Dispose(bool disposing)
		{
			bool flag = !disposing;
			if (flag)
			{
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
		private BigInteger CalculatePublicKey()
		{
			return BigInteger.ModPow(DiffieHellmanCryptoProvider.primeRoot, this.secret, this.prime);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001C0D0 File Offset: 0x0001A2D0
		private BigInteger CalculateSharedKey(BigInteger otherPartyPublicKey)
		{
			return BigInteger.ModPow(otherPartyPublicKey, this.secret, this.prime);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001C0F4 File Offset: 0x0001A2F4
		private BigInteger GenerateRandomSecret(int secretLength)
		{
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			byte[] array = new byte[secretLength / 8];
			BigInteger bigInteger;
			do
			{
				rngcryptoServiceProvider.GetBytes(array);
				bigInteger = new BigInteger(array);
			}
			while (bigInteger >= this.prime - 1 || bigInteger < 2L);
			return bigInteger;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001C152 File Offset: 0x0001A352
		// Note: this type is marked as 'beforefieldinit'.
		static DiffieHellmanCryptoProvider()
		{
		}

		// Token: 0x040001FE RID: 510
		private static readonly BigInteger primeRoot = new BigInteger(OakleyGroups.Generator);

		// Token: 0x040001FF RID: 511
		private readonly BigInteger prime;

		// Token: 0x04000200 RID: 512
		private readonly BigInteger secret;

		// Token: 0x04000201 RID: 513
		private readonly BigInteger publicKey;

		// Token: 0x04000202 RID: 514
		private Rijndael crypto;

		// Token: 0x04000203 RID: 515
		private byte[] sharedKey;
	}
}
