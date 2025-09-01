using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.Authenticode
{
	// Token: 0x02000068 RID: 104
	public class PrivateKey
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x000166ED File Offset: 0x000148ED
		public PrivateKey()
		{
			this.keyType = 2;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000166FC File Offset: 0x000148FC
		public PrivateKey(byte[] data, string password)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			if (!this.Decode(data, password))
			{
				throw new CryptographicException(Locale.GetText("Invalid data and/or password"));
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001672C File Offset: 0x0001492C
		public bool Encrypted
		{
			get
			{
				return this.encrypted;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00016734 File Offset: 0x00014934
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0001673C File Offset: 0x0001493C
		public int KeyType
		{
			get
			{
				return this.keyType;
			}
			set
			{
				this.keyType = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00016745 File Offset: 0x00014945
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0001674D File Offset: 0x0001494D
		public RSA RSA
		{
			get
			{
				return this.rsa;
			}
			set
			{
				this.rsa = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00016756 File Offset: 0x00014956
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00016768 File Offset: 0x00014968
		public bool Weak
		{
			get
			{
				return !this.encrypted || this.weak;
			}
			set
			{
				this.weak = value;
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00016774 File Offset: 0x00014974
		private byte[] DeriveKey(byte[] salt, string password)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(password);
			SHA1 sha = SHA1.Create();
			sha.TransformBlock(salt, 0, salt.Length, salt, 0);
			sha.TransformFinalBlock(bytes, 0, bytes.Length);
			byte[] array = new byte[16];
			Buffer.BlockCopy(sha.Hash, 0, array, 0, 16);
			sha.Clear();
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000167D4 File Offset: 0x000149D4
		private bool Decode(byte[] pvk, string password)
		{
			if (BitConverterLE.ToUInt32(pvk, 0) != 2964713758U)
			{
				return false;
			}
			if (BitConverterLE.ToUInt32(pvk, 4) != 0U)
			{
				return false;
			}
			this.keyType = BitConverterLE.ToInt32(pvk, 8);
			this.encrypted = (BitConverterLE.ToUInt32(pvk, 12) == 1U);
			int num = BitConverterLE.ToInt32(pvk, 16);
			int num2 = BitConverterLE.ToInt32(pvk, 20);
			byte[] array = new byte[num2];
			Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
			if (num > 0)
			{
				if (password == null)
				{
					return false;
				}
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(pvk, 24, array2, 0, num);
				byte[] array3 = this.DeriveKey(array2, password);
				RC4.Create().CreateDecryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
				try
				{
					this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
					this.weak = false;
				}
				catch (CryptographicException)
				{
					this.weak = true;
					Buffer.BlockCopy(pvk, 24 + num, array, 0, num2);
					Array.Clear(array3, 5, 11);
					RC4.Create().CreateDecryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
					this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				}
				Array.Clear(array3, 0, array3.Length);
			}
			else
			{
				this.weak = true;
				this.rsa = CryptoConvert.FromCapiPrivateKeyBlob(array);
				Array.Clear(array, 0, array.Length);
			}
			Array.Clear(pvk, 0, pvk.Length);
			return this.rsa != null;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00016930 File Offset: 0x00014B30
		public void Save(string filename)
		{
			this.Save(filename, null);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001693C File Offset: 0x00014B3C
		public void Save(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			FileStream fileStream = File.Open(filename, FileMode.Create, FileAccess.Write);
			try
			{
				byte[] buffer = new byte[4];
				byte[] bytes = BitConverterLE.GetBytes(2964713758U);
				fileStream.Write(bytes, 0, 4);
				fileStream.Write(buffer, 0, 4);
				bytes = BitConverterLE.GetBytes(this.keyType);
				fileStream.Write(bytes, 0, 4);
				this.encrypted = (password != null);
				array = CryptoConvert.ToCapiPrivateKeyBlob(this.rsa);
				if (this.encrypted)
				{
					bytes = BitConverterLE.GetBytes(1);
					fileStream.Write(bytes, 0, 4);
					bytes = BitConverterLE.GetBytes(16);
					fileStream.Write(bytes, 0, 4);
					bytes = BitConverterLE.GetBytes(array.Length);
					fileStream.Write(bytes, 0, 4);
					byte[] array2 = new byte[16];
					RC4 rc = RC4.Create();
					byte[] array3 = null;
					try
					{
						RandomNumberGenerator.Create().GetBytes(array2);
						fileStream.Write(array2, 0, array2.Length);
						array3 = this.DeriveKey(array2, password);
						if (this.Weak)
						{
							Array.Clear(array3, 5, 11);
						}
						rc.CreateEncryptor(array3, null).TransformBlock(array, 8, array.Length - 8, array, 8);
						goto IL_14E;
					}
					finally
					{
						Array.Clear(array2, 0, array2.Length);
						Array.Clear(array3, 0, array3.Length);
						rc.Clear();
					}
				}
				fileStream.Write(buffer, 0, 4);
				fileStream.Write(buffer, 0, 4);
				bytes = BitConverterLE.GetBytes(array.Length);
				fileStream.Write(bytes, 0, 4);
				IL_14E:
				fileStream.Write(array, 0, array.Length);
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
				fileStream.Close();
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00016AEC File Offset: 0x00014CEC
		public static PrivateKey CreateFromFile(string filename)
		{
			return PrivateKey.CreateFromFile(filename, null);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00016AF8 File Offset: 0x00014CF8
		public static PrivateKey CreateFromFile(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			byte[] array = null;
			using (FileStream fileStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return new PrivateKey(array, password);
		}

		// Token: 0x0400032E RID: 814
		private const uint magic = 2964713758U;

		// Token: 0x0400032F RID: 815
		private bool encrypted;

		// Token: 0x04000330 RID: 816
		private RSA rsa;

		// Token: 0x04000331 RID: 817
		private bool weak;

		// Token: 0x04000332 RID: 818
		private int keyType;
	}
}
