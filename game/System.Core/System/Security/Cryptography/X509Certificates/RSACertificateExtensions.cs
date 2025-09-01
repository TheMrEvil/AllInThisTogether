using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides extension methods for retrieving <see cref="T:System.Security.Cryptography.RSA" /> implementations for the public and private keys of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />. </summary>
	// Token: 0x02000064 RID: 100
	public static class RSACertificateExtensions
	{
		/// <summary>Gets the <see cref="T:System.Security.Cryptography.RSA" /> private key from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />.</summary>
		/// <param name="certificate">The certificate. </param>
		/// <returns>The private key, or <see langword="null" /> if the certificate does not have an RSA private key. </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="certificate" /> is <see langword="null" />. </exception>
		// Token: 0x06000224 RID: 548 RVA: 0x000061EF File Offset: 0x000043EF
		public static RSA GetRSAPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!certificate.HasPrivateKey)
			{
				return null;
			}
			return certificate.Impl.GetRSAPrivateKey();
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.RSA" /> public key from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />.</summary>
		/// <param name="certificate">The certificate. </param>
		/// <returns>The public key, or <see langword="null" /> if the certificate does not have an RSA public key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="certificate" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Windows reports an error. See the <see cref="P:System.Exception.Message" /> property for more information. </exception>
		// Token: 0x06000225 RID: 549 RVA: 0x00006214 File Offset: 0x00004414
		public static RSA GetRSAPublicKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PublicKey.Key as RSA;
		}

		/// <summary>Combines a private key with the public key of an <see cref="T:System.Security.Cryptography.RSA" /> certificate to generate a new RSA certificate.</summary>
		/// <param name="certificate">The RSA certificate.</param>
		/// <param name="privateKey">The private RSA key.</param>
		/// <returns>A new RSA certificate with the <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.HasPrivateKey" /> property set to <see langword="true" />. The input RSA certificate object isn't modified.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> or <paramref name="privateKey" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The certificate already has an associated private key.</exception>
		/// <exception cref="T:System.ArgumentException">The certificate doesn't have a public key.-or-The specified private key doesn't match the public key for the specified certificate.</exception>
		// Token: 0x06000226 RID: 550 RVA: 0x00006234 File Offset: 0x00004434
		public static X509Certificate2 CopyWithPrivateKey(this X509Certificate2 certificate, RSA privateKey)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			return (X509Certificate2)certificate.Impl.CopyWithPrivateKey(privateKey).CreateCertificate();
		}
	}
}
