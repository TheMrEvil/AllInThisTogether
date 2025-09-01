using System;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	/// <summary>Performs symmetric encryption and decryption using the Cryptographic Application Programming Interfaces (CAPI) implementation of the Advanced Encryption Standard (AES) algorithm. </summary>
	// Token: 0x02000056 RID: 86
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class AesCryptoServiceProvider : Aes
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesCryptoServiceProvider" /> class. </summary>
		/// <exception cref="T:System.PlatformNotSupportedException">There is no supported key size for the current platform.</exception>
		// Token: 0x060001C4 RID: 452 RVA: 0x00003F1A File Offset: 0x0000211A
		public AesCryptoServiceProvider()
		{
			this.FeedbackSizeValue = 8;
		}

		/// <summary>Generates a random initialization vector (IV) to use for the algorithm.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The initialization vector (IV) could not be generated. </exception>
		// Token: 0x060001C5 RID: 453 RVA: 0x00003F29 File Offset: 0x00002129
		public override void GenerateIV()
		{
			this.IVValue = KeyBuilder.IV(this.BlockSizeValue >> 3);
		}

		/// <summary>Generates a random key to use for the algorithm. </summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key could not be generated.</exception>
		// Token: 0x060001C6 RID: 454 RVA: 0x00003F3E File Offset: 0x0000213E
		public override void GenerateKey()
		{
			this.KeyValue = KeyBuilder.Key(this.KeySizeValue >> 3);
		}

		/// <summary>Creates a symmetric AES decryptor object using the specified key and initialization vector (IV).</summary>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="key" /> or <paramref name="iv" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="key" /> is invalid.</exception>
		// Token: 0x060001C7 RID: 455 RVA: 0x00003F53 File Offset: 0x00002153
		public override ICryptoTransform CreateDecryptor(byte[] key, byte[] iv)
		{
			if (this.Mode == CipherMode.CFB && this.FeedbackSize > 64)
			{
				throw new CryptographicException("CFB with Feedbaack > 64 bits");
			}
			return new AesTransform(this, false, key, iv);
		}

		/// <summary>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</summary>
		/// <param name="key">The secret key to use for the symmetric algorithm.</param>
		/// <param name="iv">The initialization vector to use for the symmetric algorithm.</param>
		/// <returns>A symmetric AES encryptor object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="iv" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="key" /> is invalid.</exception>
		// Token: 0x060001C8 RID: 456 RVA: 0x00003F7C File Offset: 0x0000217C
		public override ICryptoTransform CreateEncryptor(byte[] key, byte[] iv)
		{
			if (this.Mode == CipherMode.CFB && this.FeedbackSize > 64)
			{
				throw new CryptographicException("CFB with Feedbaack > 64 bits");
			}
			return new AesTransform(this, true, key, iv);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00003FA5 File Offset: 0x000021A5
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00003FAD File Offset: 0x000021AD
		public override byte[] IV
		{
			get
			{
				return base.IV;
			}
			set
			{
				base.IV = value;
			}
		}

		/// <summary>Gets or sets the symmetric key that is used for encryption and decryption.</summary>
		/// <returns>The symmetric key that is used for encryption and decryption.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value for the key is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The size of the key is invalid.</exception>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00003FB6 File Offset: 0x000021B6
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00003FBE File Offset: 0x000021BE
		public override byte[] Key
		{
			get
			{
				return base.Key;
			}
			set
			{
				base.Key = value;
			}
		}

		/// <summary>Gets or sets the size, in bits, of the secret key. </summary>
		/// <returns>The size, in bits, of the key.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00003FC7 File Offset: 0x000021C7
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00003FCF File Offset: 0x000021CF
		public override int KeySize
		{
			get
			{
				return base.KeySize;
			}
			set
			{
				base.KeySize = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00003FD8 File Offset: 0x000021D8
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x00003FE0 File Offset: 0x000021E0
		public override int FeedbackSize
		{
			get
			{
				return base.FeedbackSize;
			}
			set
			{
				base.FeedbackSize = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00003FE9 File Offset: 0x000021E9
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x00003FF1 File Offset: 0x000021F1
		public override CipherMode Mode
		{
			get
			{
				return base.Mode;
			}
			set
			{
				if (value == CipherMode.CTS)
				{
					throw new CryptographicException("CTS is not supported");
				}
				base.Mode = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004009 File Offset: 0x00002209
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x00004011 File Offset: 0x00002211
		public override PaddingMode Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Creates a symmetric AES decryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES decryptor object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The current key is invalid or missing.</exception>
		// Token: 0x060001D5 RID: 469 RVA: 0x0000401A File Offset: 0x0000221A
		public override ICryptoTransform CreateDecryptor()
		{
			return this.CreateDecryptor(this.Key, this.IV);
		}

		/// <summary>Creates a symmetric AES encryptor object using the current key and initialization vector (IV).</summary>
		/// <returns>A symmetric AES encryptor object.</returns>
		// Token: 0x060001D6 RID: 470 RVA: 0x0000402E File Offset: 0x0000222E
		public override ICryptoTransform CreateEncryptor()
		{
			return this.CreateEncryptor(this.Key, this.IV);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00004042 File Offset: 0x00002242
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
