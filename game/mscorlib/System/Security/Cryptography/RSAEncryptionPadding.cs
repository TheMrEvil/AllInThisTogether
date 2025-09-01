using System;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Specifies the padding mode and parameters to use with RSA encryption or decryption operations.</summary>
	// Token: 0x02000473 RID: 1139
	public sealed class RSAEncryptionPadding : IEquatable<RSAEncryptionPadding>
	{
		/// <summary>Gets an object that represents the PKCS #1 encryption standard.</summary>
		/// <returns>An object that represents the PKCS #1 encryption standard.</returns>
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x000A5E70 File Offset: 0x000A4070
		public static RSAEncryptionPadding Pkcs1
		{
			get
			{
				return RSAEncryptionPadding.s_pkcs1;
			}
		}

		/// <summary>Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA1 hash algorithm.</summary>
		/// <returns>An object that represents the OAEP encryption standard with a SHA1 hash algorithm.</returns>
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x000A5E77 File Offset: 0x000A4077
		public static RSAEncryptionPadding OaepSHA1
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA1;
			}
		}

		/// <summary>Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA256 hash algorithm.</summary>
		/// <returns>An object that represents the OAEP encryption standard with a SHA256 hash algorithm.</returns>
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000A5E7E File Offset: 0x000A407E
		public static RSAEncryptionPadding OaepSHA256
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA256;
			}
		}

		/// <summary>Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA-384 hash algorithm.</summary>
		/// <returns>An object that represents the OAEP encryption standard with a SHA384 hash algorithm.</returns>
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x000A5E85 File Offset: 0x000A4085
		public static RSAEncryptionPadding OaepSHA384
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA384;
			}
		}

		/// <summary>Gets an object that represents the Optimal Asymmetric Encryption Padding (OAEP) encryption standard with a SHA512 hash algorithm.</summary>
		/// <returns>An object that represents the OAEP encryption standard with a SHA512 hash algorithm.</returns>
		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000A5E8C File Offset: 0x000A408C
		public static RSAEncryptionPadding OaepSHA512
		{
			get
			{
				return RSAEncryptionPadding.s_oaepSHA512;
			}
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x000A5E93 File Offset: 0x000A4093
		private RSAEncryptionPadding(RSAEncryptionPaddingMode mode, HashAlgorithmName oaepHashAlgorithm)
		{
			this._mode = mode;
			this._oaepHashAlgorithm = oaepHashAlgorithm;
		}

		/// <summary>Creates a new <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> instance whose <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> is <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" /> with the given hash algorithm.</summary>
		/// <param name="hashAlgorithm">The hash algorithm.</param>
		/// <returns>An object whose mode is <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> is <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" /> with the hash algorithm specified by <paramref name="hashAlgorithm" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> property of <paramref name="hashAlgorithm" /> is either <see langword="null" /> or <see cref="F:System.String.Empty" />.</exception>
		// Token: 0x06002E2D RID: 11821 RVA: 0x000A5EA9 File Offset: 0x000A40A9
		public static RSAEncryptionPadding CreateOaep(HashAlgorithmName hashAlgorithm)
		{
			if (string.IsNullOrEmpty(hashAlgorithm.Name))
			{
				throw new ArgumentException(Environment.GetResourceString("The hash algorithm name cannot be null or empty."), "hashAlgorithm");
			}
			return new RSAEncryptionPadding(RSAEncryptionPaddingMode.Oaep, hashAlgorithm);
		}

		/// <summary>Gets the padding mode represented by this <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> instance.</summary>
		/// <returns>A padding mode.</returns>
		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000A5ED5 File Offset: 0x000A40D5
		public RSAEncryptionPaddingMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		/// <summary>Gets the hash algorithm used in conjunction with the <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" /> padding mode. If the value of the <see cref="P:System.Security.Cryptography.RSAEncryptionPadding.Mode" /> property is not <see cref="F:System.Security.Cryptography.RSAEncryptionPaddingMode.Oaep" />, <see cref="P:System.Security.Cryptography.HashAlgorithmName.Name" /> is <see langword="null" />.</summary>
		/// <returns>The hash algorithm.</returns>
		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002E2F RID: 11823 RVA: 0x000A5EDD File Offset: 0x000A40DD
		public HashAlgorithmName OaepHashAlgorithm
		{
			get
			{
				return this._oaepHashAlgorithm;
			}
		}

		/// <summary>Returns the hash code of this <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> object.</summary>
		/// <returns>The hash code of this instance.</returns>
		// Token: 0x06002E30 RID: 11824 RVA: 0x000A5EE5 File Offset: 0x000A40E5
		public override int GetHashCode()
		{
			return RSAEncryptionPadding.CombineHashCodes(this._mode.GetHashCode(), this._oaepHashAlgorithm.GetHashCode());
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x00033E4B File Offset: 0x0003204B
		private static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		/// <summary>Determines whether the current instance is equal to the specified object.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E32 RID: 11826 RVA: 0x000A5F0E File Offset: 0x000A410E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as RSAEncryptionPadding);
		}

		/// <summary>Determines whether the current instance is equal to the specified <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> object.</summary>
		/// <param name="other">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="other" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E33 RID: 11827 RVA: 0x000A5F1C File Offset: 0x000A411C
		public bool Equals(RSAEncryptionPadding other)
		{
			return other != null && this._mode == other._mode && this._oaepHashAlgorithm == other._oaepHashAlgorithm;
		}

		/// <summary>Indicates whether two specified <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> objects are equal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <see langword="left" /> and <see langword="right" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E34 RID: 11828 RVA: 0x000A5F48 File Offset: 0x000A4148
		public static bool operator ==(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Indicates whether two specified <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> objects are unequal.</summary>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <see langword="left" /> and <see langword="right" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002E35 RID: 11829 RVA: 0x000A5F59 File Offset: 0x000A4159
		public static bool operator !=(RSAEncryptionPadding left, RSAEncryptionPadding right)
		{
			return !(left == right);
		}

		/// <summary>Returns the string representation of the current <see cref="T:System.Security.Cryptography.RSAEncryptionPadding" /> instance.</summary>
		/// <returns>The string representation of the current object.</returns>
		// Token: 0x06002E36 RID: 11830 RVA: 0x000A5F65 File Offset: 0x000A4165
		public override string ToString()
		{
			return this._mode.ToString() + this._oaepHashAlgorithm.Name;
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000A5F88 File Offset: 0x000A4188
		// Note: this type is marked as 'beforefieldinit'.
		static RSAEncryptionPadding()
		{
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000173AD File Offset: 0x000155AD
		internal RSAEncryptionPadding()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002119 RID: 8473
		private static readonly RSAEncryptionPadding s_pkcs1 = new RSAEncryptionPadding(RSAEncryptionPaddingMode.Pkcs1, default(HashAlgorithmName));

		// Token: 0x0400211A RID: 8474
		private static readonly RSAEncryptionPadding s_oaepSHA1 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA1);

		// Token: 0x0400211B RID: 8475
		private static readonly RSAEncryptionPadding s_oaepSHA256 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA256);

		// Token: 0x0400211C RID: 8476
		private static readonly RSAEncryptionPadding s_oaepSHA384 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA384);

		// Token: 0x0400211D RID: 8477
		private static readonly RSAEncryptionPadding s_oaepSHA512 = RSAEncryptionPadding.CreateOaep(HashAlgorithmName.SHA512);

		// Token: 0x0400211E RID: 8478
		private RSAEncryptionPaddingMode _mode;

		// Token: 0x0400211F RID: 8479
		private HashAlgorithmName _oaepHashAlgorithm;
	}
}
