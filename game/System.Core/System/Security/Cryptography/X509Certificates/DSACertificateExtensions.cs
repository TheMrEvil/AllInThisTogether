using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides extension methods for retrieving <see cref="T:System.Security.Cryptography.DSA" /> implementations for the public and private keys of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />. </summary>
	// Token: 0x02000062 RID: 98
	public static class DSACertificateExtensions
	{
		/// <summary>Gets the <see cref="T:System.Security.Cryptography.DSA" /> public key from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />.</summary>
		/// <param name="certificate">The certificate. </param>
		/// <returns>The public key, or <see langword="null" /> if the certificate does not have a DSA public key. </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="certificate" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Windows reports an error. See the <see cref="P:System.Exception.Message" /> property for more information. </exception>
		// Token: 0x0600021E RID: 542 RVA: 0x00006191 File Offset: 0x00004391
		public static DSA GetDSAPublicKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PrivateKey as DSA;
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.DSA" /> private key from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />.</summary>
		/// <param name="certificate">The certificate. </param>
		/// <returns>The private key, or <see langword="null" /> if the certificate does not have a DSA private key. </returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="certificate" /> is <see langword="null" />. </exception>
		// Token: 0x0600021F RID: 543 RVA: 0x000061AC File Offset: 0x000043AC
		public static DSA GetDSAPrivateKey(this X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return certificate.PublicKey.Key as DSA;
		}

		/// <summary>Combines a private key with the public key of a <see cref="T:System.Security.Cryptography.DSA" /> certificate to generate a new DSA certificate.</summary>
		/// <param name="certificate">The DSA certificate.</param>
		/// <param name="privateKey">The private DSA key.</param>
		/// <returns>A new DSA certificate with the <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.HasPrivateKey" /> property set to <see langword="true" />. The input DSA certificate object isn't modified.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> or <paramref name="privateKey" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The certificate already has an associated private key.</exception>
		/// <exception cref="T:System.ArgumentException">The certificate doesn't have a public key.-or-The specified private key doesn't match the public key for the specified certificate.</exception>
		// Token: 0x06000220 RID: 544 RVA: 0x000061CC File Offset: 0x000043CC
		[MonoTODO]
		public static X509Certificate2 CopyWithPrivateKey(this X509Certificate2 certificate, DSA privateKey)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (privateKey == null)
			{
				throw new ArgumentNullException("privateKey");
			}
			throw new NotImplementedException();
		}
	}
}
