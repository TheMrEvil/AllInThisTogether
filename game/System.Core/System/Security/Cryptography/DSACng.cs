using System;
using System.Security.Permissions;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Digital Signature Algorithm (DSA).</summary>
	// Token: 0x0200036C RID: 876
	public sealed class DSACng : DSA
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACng" /> class with a random 2,048-bit key pair. </summary>
		// Token: 0x06001AA7 RID: 6823 RVA: 0x0000235B File Offset: 0x0000055B
		public DSACng()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACng" /> class with a randomly generated key of the specified size. </summary>
		/// <param name="keySize">The size of the key to generate in bits. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="keySize" /> is not valid. </exception>
		// Token: 0x06001AA8 RID: 6824 RVA: 0x0000235B File Offset: 0x0000055B
		public DSACng(int keySize)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.DSACng" /> class with the specified key. </summary>
		/// <param name="key">The key to use for DSA operations. </param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="key" /> is not a valid DSA key. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="key" /> is <see langword="null" />. </exception>
		// Token: 0x06001AA9 RID: 6825 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public DSACng(CngKey key)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the key that will be used by the <see cref="T:System.Security.Cryptography.DSACng" /> object for any cryptographic operation that it performs. </summary>
		/// <returns>The key used by the <see cref="T:System.Security.Cryptography.DSACng" /> object to perform cryptographic operations. </returns>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0005A05A File Offset: 0x0005825A
		public CngKey Key
		{
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Creates the digital signature for the specified data.</summary>
		/// <param name="rgbHash">The data to be signed.</param>
		/// <returns>The digital signature for the specified data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="rgbHash" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///         <paramref name="rgbHash" /> is shorter in length than the Q value of the DSA key . </exception>
		// Token: 0x06001AAB RID: 6827 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecuritySafeCritical]
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Exports the DSA algorithm parameters. </summary>
		/// <param name="includePrivateParameters">
		///       <see langword="true" /> to include private parameters; otherwise, <see langword="false" />. </param>
		/// <returns>The DSA algorithm parameters. </returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">DSA key is not a valid public or private key.</exception>
		// Token: 0x06001AAC RID: 6828 RVA: 0x0005A064 File Offset: 0x00058264
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(DSAParameters);
		}

		/// <summary>Replaces the existing key that the current instance is working with by creating a new <see cref="T:System.Security.Cryptography.CngKey" /> for the parameters structure. </summary>
		/// <param name="parameters">The DSA parameters. </param>
		/// <exception cref="T:System.ArgumentException">The specified DSA parameters are not valid. </exception>
		// Token: 0x06001AAD RID: 6829 RVA: 0x0000235B File Offset: 0x0000055B
		public override void ImportParameters(DSAParameters parameters)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Verifies if the specified digital signature matches the specified data. </summary>
		/// <param name="rgbHash">The signed data.</param>
		/// <param name="rgbSignature">The digital signature to be verified.</param>
		/// <returns>
		///     <see langword="true" /> if <paramref name="rgbSignature" /> matches the signature computed using the specified data; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="rgbHash" /> parameter is <see langword="null" />.-or- The <paramref name="rgbSignature" /> parameter is <see langword="null" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///         <paramref name="rgbHash" /> is shorter in length than the Q value of the DSA key . </exception>
		// Token: 0x06001AAE RID: 6830 RVA: 0x0005A080 File Offset: 0x00058280
		[SecuritySafeCritical]
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}
	}
}
