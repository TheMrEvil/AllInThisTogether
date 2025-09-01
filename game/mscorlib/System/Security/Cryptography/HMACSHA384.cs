using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Computes a Hash-based Message Authentication Code (HMAC) using the <see cref="T:System.Security.Cryptography.SHA384" /> hash function.</summary>
	// Token: 0x02000494 RID: 1172
	[ComVisible(true)]
	public class HMACSHA384 : HMAC
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA384" /> class by using a randomly generated key.</summary>
		// Token: 0x06002F08 RID: 12040 RVA: 0x000A7E41 File Offset: 0x000A6041
		public HMACSHA384() : this(Utils.GenerateRandom(128))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.HMACSHA384" /> class by using the specified key data.</summary>
		/// <param name="key">The secret key for <see cref="T:System.Security.Cryptography.HMACSHA384" /> encryption. The key can be any length. However, the recommended size is 128 bytes. If the key is more than 128 bytes long, it is hashed (using SHA-384) to derive a 128-byte key. If it is less than 128 bytes long, it is padded to 128 bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002F09 RID: 12041 RVA: 0x000A7E54 File Offset: 0x000A6054
		[SecuritySafeCritical]
		public HMACSHA384(byte[] key)
		{
			this.m_hashName = "SHA384";
			this.m_hash1 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
			this.m_hash2 = HMAC.GetHashAlgorithmWithFipsFallback(() => new SHA384Managed(), () => HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider"));
			this.HashSizeValue = 384;
			base.BlockSizeValue = this.BlockSize;
			base.InitializeKey(key);
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002F0A RID: 12042 RVA: 0x000A7F2D File Offset: 0x000A612D
		private int BlockSize
		{
			get
			{
				if (!this.m_useLegacyBlockSize)
				{
					return 128;
				}
				return 64;
			}
		}

		/// <summary>Provides a workaround for the .NET Framework 2.0 implementation of the <see cref="T:System.Security.Cryptography.HMACSHA384" /> algorithm, which is inconsistent with the .NET Framework 2.0 Service Pack 1 implementation of the algorithm.</summary>
		/// <returns>
		///   <see langword="true" /> to enable .NET Framework 2.0 Service Pack 1 applications to interact with .NET Framework 2.0 applications; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000A7F3F File Offset: 0x000A613F
		// (set) Token: 0x06002F0C RID: 12044 RVA: 0x000A7F47 File Offset: 0x000A6147
		public bool ProduceLegacyHmacValues
		{
			get
			{
				return this.m_useLegacyBlockSize;
			}
			set
			{
				this.m_useLegacyBlockSize = value;
				base.BlockSizeValue = this.BlockSize;
				base.InitializeKey(this.KeyValue);
			}
		}

		// Token: 0x04002171 RID: 8561
		private bool m_useLegacyBlockSize = Utils._ProduceLegacyHmacValues();

		// Token: 0x02000495 RID: 1173
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06002F0D RID: 12045 RVA: 0x000A7F68 File Offset: 0x000A6168
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06002F0E RID: 12046 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06002F0F RID: 12047 RVA: 0x000A7F74 File Offset: 0x000A6174
			internal HashAlgorithm <.ctor>b__2_0()
			{
				return new SHA384Managed();
			}

			// Token: 0x06002F10 RID: 12048 RVA: 0x000A7F7B File Offset: 0x000A617B
			internal HashAlgorithm <.ctor>b__2_1()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider");
			}

			// Token: 0x06002F11 RID: 12049 RVA: 0x000A7F74 File Offset: 0x000A6174
			internal HashAlgorithm <.ctor>b__2_2()
			{
				return new SHA384Managed();
			}

			// Token: 0x06002F12 RID: 12050 RVA: 0x000A7F7B File Offset: 0x000A617B
			internal HashAlgorithm <.ctor>b__2_3()
			{
				return HashAlgorithm.Create("System.Security.Cryptography.SHA384CryptoServiceProvider");
			}

			// Token: 0x04002172 RID: 8562
			public static readonly HMACSHA384.<>c <>9 = new HMACSHA384.<>c();

			// Token: 0x04002173 RID: 8563
			public static Func<HashAlgorithm> <>9__2_0;

			// Token: 0x04002174 RID: 8564
			public static Func<HashAlgorithm> <>9__2_1;

			// Token: 0x04002175 RID: 8565
			public static Func<HashAlgorithm> <>9__2_2;

			// Token: 0x04002176 RID: 8566
			public static Func<HashAlgorithm> <>9__2_3;
		}
	}
}
