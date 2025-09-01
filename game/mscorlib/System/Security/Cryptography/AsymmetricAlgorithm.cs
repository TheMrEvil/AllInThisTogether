using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all implementations of asymmetric algorithms must inherit.</summary>
	// Token: 0x02000478 RID: 1144
	[ComVisible(true)]
	public abstract class AsymmetricAlgorithm : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The implementation of the derived class is not valid.</exception>
		// Token: 0x06002E49 RID: 11849 RVA: 0x0000259F File Offset: 0x0000079F
		protected AsymmetricAlgorithm()
		{
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		// Token: 0x06002E4A RID: 11850 RVA: 0x000A6143 File Offset: 0x000A4343
		public void Dispose()
		{
			this.Clear();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class.</summary>
		// Token: 0x06002E4B RID: 11851 RVA: 0x000A614B File Offset: 0x000A434B
		public void Clear()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06002E4C RID: 11852 RVA: 0x00004BF9 File Offset: 0x00002DF9
		protected virtual void Dispose(bool disposing)
		{
		}

		/// <summary>Gets or sets the size, in bits, of the key modulus used by the asymmetric algorithm.</summary>
		/// <returns>The size, in bits, of the key modulus used by the asymmetric algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key modulus size is invalid.</exception>
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x000A615A File Offset: 0x000A435A
		// (set) Token: 0x06002E4E RID: 11854 RVA: 0x000A6164 File Offset: 0x000A4364
		public virtual int KeySize
		{
			get
			{
				return this.KeySizeValue;
			}
			set
			{
				for (int i = 0; i < this.LegalKeySizesValue.Length; i++)
				{
					if (this.LegalKeySizesValue[i].SkipSize == 0)
					{
						if (this.LegalKeySizesValue[i].MinSize == value)
						{
							this.KeySizeValue = value;
							return;
						}
					}
					else
					{
						for (int j = this.LegalKeySizesValue[i].MinSize; j <= this.LegalKeySizesValue[i].MaxSize; j += this.LegalKeySizesValue[i].SkipSize)
						{
							if (j == value)
							{
								this.KeySizeValue = value;
								return;
							}
						}
					}
				}
				throw new CryptographicException(Environment.GetResourceString("Specified key is not a valid size for this algorithm."));
			}
		}

		/// <summary>Gets the key sizes that are supported by the asymmetric algorithm.</summary>
		/// <returns>An array that contains the key sizes supported by the asymmetric algorithm.</returns>
		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000A61F6 File Offset: 0x000A43F6
		public virtual KeySizes[] LegalKeySizes
		{
			get
			{
				return (KeySizes[])this.LegalKeySizesValue.Clone();
			}
		}

		/// <summary>When implemented in a derived class, gets the name of the signature algorithm. Otherwise, always throws a <see cref="T:System.NotImplementedException" />.</summary>
		/// <returns>The name of the signature algorithm.</returns>
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual string SignatureAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>When overridden in a derived class, gets the name of the key exchange algorithm. Otherwise, throws an <see cref="T:System.NotImplementedException" />.</summary>
		/// <returns>The name of the key exchange algorithm.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual string KeyExchangeAlgorithm
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Creates a default cryptographic object used to perform the asymmetric algorithm.</summary>
		/// <returns>A new <see cref="T:System.Security.Cryptography.RSACryptoServiceProvider" /> instance, unless the default settings have been changed with the &lt;cryptoClass&gt; element.</returns>
		// Token: 0x06002E52 RID: 11858 RVA: 0x000A6208 File Offset: 0x000A4408
		public static AsymmetricAlgorithm Create()
		{
			return AsymmetricAlgorithm.Create("System.Security.Cryptography.AsymmetricAlgorithm");
		}

		/// <summary>Creates an instance of the specified implementation of an asymmetric algorithm.</summary>
		/// <param name="algName">The asymmetric algorithm implementation to use. The following table shows the valid values for the <paramref name="algName" /> parameter and the algorithms they map to.  
		///   Parameter value  
		///
		///   Implements  
		///
		///   System.Security.Cryptography.AsymmetricAlgorithm  
		///
		///  <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> RSA  
		///
		///  <see cref="T:System.Security.Cryptography.RSA" /> System.Security.Cryptography.RSA  
		///
		///  <see cref="T:System.Security.Cryptography.RSA" /> DSA  
		///
		///  <see cref="T:System.Security.Cryptography.DSA" /> System.Security.Cryptography.DSA  
		///
		///  <see cref="T:System.Security.Cryptography.DSA" /> ECDsa  
		///
		///  <see cref="T:System.Security.Cryptography.ECDsa" /> ECDsaCng  
		///
		///  <see cref="T:System.Security.Cryptography.ECDsaCng" /> System.Security.Cryptography.ECDsaCng  
		///
		///  <see cref="T:System.Security.Cryptography.ECDsaCng" /> ECDH  
		///
		///  <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> ECDiffieHellman  
		///
		///  <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> ECDiffieHellmanCng  
		///
		///  <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> System.Security.Cryptography.ECDiffieHellmanCng  
		///
		///  <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /></param>
		/// <returns>A new instance of the specified asymmetric algorithm implementation.</returns>
		// Token: 0x06002E53 RID: 11859 RVA: 0x000A6214 File Offset: 0x000A4414
		public static AsymmetricAlgorithm Create(string algName)
		{
			return (AsymmetricAlgorithm)CryptoConfig.CreateFromName(algName);
		}

		/// <summary>When overridden in a derived class, reconstructs an <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object from an XML string. Otherwise, throws a <see cref="T:System.NotImplementedException" />.</summary>
		/// <param name="xmlString">The XML string to use to reconstruct the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</param>
		// Token: 0x06002E54 RID: 11860 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual void FromXmlString(string xmlString)
		{
			throw new NotImplementedException();
		}

		/// <summary>When overridden in a derived class, creates and returns an XML string representation of the current <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object. Otherwise, throws a <see cref="T:System.NotImplementedException" />.</summary>
		/// <param name="includePrivateParameters">
		///   <see langword="true" /> to include private parameters; otherwise, <see langword="false" />.</param>
		/// <returns>An XML string encoding of the current <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object.</returns>
		// Token: 0x06002E55 RID: 11861 RVA: 0x000479F8 File Offset: 0x00045BF8
		public virtual string ToXmlString(bool includePrivateParameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual byte[] ExportEncryptedPkcs8PrivateKey(ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual byte[] ExportEncryptedPkcs8PrivateKey(ReadOnlySpan<char> password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual byte[] ExportPkcs8PrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual byte[] ExportSubjectPublicKeyInfo()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual void ImportEncryptedPkcs8PrivateKey(ReadOnlySpan<byte> passwordBytes, ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual void ImportEncryptedPkcs8PrivateKey(ReadOnlySpan<char> password, ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual void ImportPkcs8PrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual void ImportSubjectPublicKeyInfo(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E5E RID: 11870 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual bool TryExportEncryptedPkcs8PrivateKey(ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual bool TryExportEncryptedPkcs8PrivateKey(ReadOnlySpan<char> password, PbeParameters pbeParameters, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual bool TryExportPkcs8PrivateKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x0001B98B File Offset: 0x00019B8B
		public virtual bool TryExportSubjectPublicKeyInfo(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Represents the size, in bits, of the key modulus used by the asymmetric algorithm.</summary>
		// Token: 0x0400212B RID: 8491
		protected int KeySizeValue;

		/// <summary>Specifies the key sizes that are supported by the asymmetric algorithm.</summary>
		// Token: 0x0400212C RID: 8492
		protected KeySizes[] LegalKeySizesValue;
	}
}
