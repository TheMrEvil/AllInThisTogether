using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) by using the <see cref="T:System.Security.Cryptography.SHA256" /> hash function.</summary>
	// Token: 0x02000492 RID: 1170
	[ComVisible(true)]
	public class HMACSHA256 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA256" /> class with a randomly generated key.</summary>
		// Token: 0x06002F00 RID: 12032 RVA: 0x000A7D51 File Offset: 0x000A5F51
		public HMACSHA256() : this(Utils.GenerateRandom(64))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA256" /> class with the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACSHA256" /> encryption. The key can be any length. However, the recommended size is 64 bytes. If the key is more than 64 bytes long, it is hashed (using SHA-256) to derive a 64-byte key. If it is less than 64 bytes long, it is padded to 64 bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002F01 RID: 12033 RVA: 0x000A7D60 File Offset: 0x000A5F60
		public HMACSHA256(byte[] key)
		{
			this.m_hashName = "SHA256";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA256Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider"));
			this.HashSizeValue = 256;
			base.InitializeKey(key);
		}

		// Token: 0x02000493 RID: 1171
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002F02 RID: 12034 RVA: 0x000A7E22 File Offset: 0x000A6022
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002F03 RID: 12035 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06002F04 RID: 12036 RVA: 0x000A7E2E File Offset: 0x000A602E
			internal HashAlgorithm <.ctor>b__1_0()
			{
				return new SHA256Managed();
			}

			// Token: 0x06002F05 RID: 12037 RVA: 0x000A7E35 File Offset: 0x000A6035
			internal HashAlgorithm <.ctor>b__1_1()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider");
			}

			// Token: 0x06002F06 RID: 12038 RVA: 0x000A7E2E File Offset: 0x000A602E
			internal HashAlgorithm <.ctor>b__1_2()
			{
				return new SHA256Managed();
			}

			// Token: 0x06002F07 RID: 12039 RVA: 0x000A7E35 File Offset: 0x000A6035
			internal HashAlgorithm <.ctor>b__1_3()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA256CryptoServiceProvider");
			}

			// Token: 0x0400216C RID: 8556
			public static readonly HMACSHA256.<>c <>9 = new HMACSHA256.<>c();

			// Token: 0x0400216D RID: 8557
			public static Func<HashAlgorithm> <>9__1_0;

			// Token: 0x0400216E RID: 8558
			public static Func<HashAlgorithm> <>9__1_1;

			// Token: 0x0400216F RID: 8559
			public static Func<HashAlgorithm> <>9__1_2;

			// Token: 0x04002170 RID: 8560
			public static Func<HashAlgorithm> <>9__1_3;
		}
	}
}
