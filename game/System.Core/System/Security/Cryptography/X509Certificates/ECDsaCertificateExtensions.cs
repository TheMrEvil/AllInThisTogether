using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides extension methods for retrieving <see cref="T:System.Security.Cryptography.ECDsa" /> implementations for the
	///     public and private keys of a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificate.</summary>
	// Token: 0x02000063 RID: 99
	public static class ECDsaCertificateExtensions
	{
		/// <summary>Gets the <see cref="T:System.Security.Cryptography.ECDsa" /> private key from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificate.</summary>
		/// <param name="certificate">The certificate. </param>
		/// <returns>The private key, or <see langword="null" /> if the certificate does not have an ECDsa private key. </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="certificate" /> is <see langword="null" />. </exception>
		// Token: 0x06000221 RID: 545 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public static ECDsa GetECDsaPrivateKey(this X509Certificate2 certificate)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.ECDsa" /> public key from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificate.</summary>
		/// <param name="certificate">The certificate. </param>
		/// <returns>The public key, or <see langword="null" /> if the certificate does not have an ECDsa public key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="certificate" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The handle is invalid. </exception>
		// Token: 0x06000222 RID: 546 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public static ECDsa GetECDsaPublicKey(this X509Certificate2 certificate)
		{
			throw new NotImplementedException();
		}

		/// <summary>Combines a private key with the public key of an <see cref="T:System.Security.Cryptography.ECDsa" /> certificate to generate a new ECDSA certificate.</summary>
		/// <param name="certificate">The ECDSA certificate.</param>
		/// <param name="privateKey">The private ECDSA key.</param>
		/// <returns>A new ECDSA certificate with the <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.HasPrivateKey" /> property set to <see langword="true" />. The input ECDSA certificate object isn't modified.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> or <paramref name="privateKey" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The certificate already has an associated private key.</exception>
		/// <exception cref="T:System.ArgumentException">The certificate doesn't have a public key.-or-The specified private key doesn't match the public key for the specified certificate.</exception>
		// Token: 0x06000223 RID: 547 RVA: 0x000023CA File Offset: 0x000005CA
		[MonoTODO]
		public static X509Certificate2 CopyWithPrivateKey(this X509Certificate2 certificate, ECDsa privateKey)
		{
			throw new NotImplementedException();
		}
	}
}
