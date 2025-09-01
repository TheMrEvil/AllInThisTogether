using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Provides an abstract base class that Elliptic Curve Diffie-Hellman (ECDH) algorithm implementations can derive from. This class provides the basic set of operations that all ECDH implementations must support.</summary>
	// Token: 0x02000048 RID: 72
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public abstract class ECDiffieHellman : AsymmetricAlgorithm
	{
		/// <summary>Gets the name of the key exchange algorithm.</summary>
		/// <returns>The name of the key exchange algorithm. </returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00003926 File Offset: 0x00001B26
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "ECDiffieHellman";
			}
		}

		/// <summary>Gets the name of the signature algorithm.</summary>
		/// <returns>Always <see langword="null" />.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000392D File Offset: 0x00001B2D
		public override string SignatureAlgorithm
		{
			get
			{
				return null;
			}
		}

		/// <summary>Creates a new instance of the default implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm.</summary>
		/// <returns>A new instance of the default implementation of this class.</returns>
		// Token: 0x06000161 RID: 353 RVA: 0x000023CA File Offset: 0x000005CA
		public new static ECDiffieHellman Create()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new instance of the specified implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm.</summary>
		/// <param name="algorithm">The name of an implementation of the ECDH algorithm.</param>
		/// <returns>A new instance of the specified implementation of this class. If the specified algorithm name does not map to an ECDH implementation, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> parameter is <see langword="null" />. </exception>
		// Token: 0x06000162 RID: 354 RVA: 0x00003930 File Offset: 0x00001B30
		public new static ECDiffieHellman Create(string algorithm)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			return CryptoConfig.CreateFromName(algorithm) as ECDiffieHellman;
		}

		/// <summary>Creates a new instance of the default implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm with a new public/private key-pair generated over the specified curve. </summary>
		/// <param name="curve">The curve to use to generate a new public/private key-pair. </param>
		/// <returns>A new instance of the default implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm. </returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="curve" /> does not validate. </exception>
		// Token: 0x06000163 RID: 355 RVA: 0x0000394C File Offset: 0x00001B4C
		public static ECDiffieHellman Create(ECCurve curve)
		{
			ECDiffieHellman ecdiffieHellman = ECDiffieHellman.Create();
			if (ecdiffieHellman != null)
			{
				try
				{
					ecdiffieHellman.GenerateKey(curve);
				}
				catch
				{
					ecdiffieHellman.Dispose();
					throw;
				}
			}
			return ecdiffieHellman;
		}

		/// <summary>Creates a new instance of the default implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm with the key described by the specified  <see cref="T:System.Security.Cryptography.ECParameters" /> object. </summary>
		/// <param name="parameters">The parameters  for the elliptic curve cryptography (ECC) algorithm. </param>
		/// <returns>A new instance of the default implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm. </returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="parameters" /> does not validate. </exception>
		// Token: 0x06000164 RID: 356 RVA: 0x00003988 File Offset: 0x00001B88
		public static ECDiffieHellman Create(ECParameters parameters)
		{
			ECDiffieHellman ecdiffieHellman = ECDiffieHellman.Create();
			if (ecdiffieHellman != null)
			{
				try
				{
					ecdiffieHellman.ImportParameters(parameters);
				}
				catch
				{
					ecdiffieHellman.Dispose();
					throw;
				}
			}
			return ecdiffieHellman;
		}

		/// <summary>Gets the public key that is being used by the current Elliptic Curve Diffie-Hellman (ECDH) instance.</summary>
		/// <returns>The public part of the ECDH key pair that is being used by this <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> instance.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000165 RID: 357
		public abstract ECDiffieHellmanPublicKey PublicKey { get; }

		/// <summary>Derives bytes that can be used as a key, given another party's public key.</summary>
		/// <param name="otherPartyPublicKey">The other party's public key.</param>
		/// <returns>The key material from the key exchange with the other party’s public key.</returns>
		// Token: 0x06000166 RID: 358 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual byte[] DeriveKeyMaterial(ECDiffieHellmanPublicKey otherPartyPublicKey)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		/// <summary>Derives bytes that can be used as a key using a hash function, given another party's public key and hash algorithm's name.</summary>
		/// <param name="otherPartyPublicKey">The other party's public key.</param>
		/// <param name="hashAlgorithm">The hash algorithm  to use to derive the key material.</param>
		/// <returns>The key material from the key exchange with the other party’s public key.</returns>
		// Token: 0x06000167 RID: 359 RVA: 0x000039CB File Offset: 0x00001BCB
		public byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm)
		{
			return this.DeriveKeyFromHash(otherPartyPublicKey, hashAlgorithm, null, null);
		}

		/// <summary>When implemented in a derived class, derives bytes that can be used as a key using a hash function, given another party's public key, hash algorithm's name, a prepend value and an append value.</summary>
		/// <param name="otherPartyPublicKey">The other party's public key.</param>
		/// <param name="hashAlgorithm">The hash algorithm  to use to derive the key material.</param>
		/// <param name="secretPrepend">A value to prepend to the derived secret before hashing.</param>
		/// <param name="secretAppend">A value to append to the derived secret before hashing.</param>
		/// <returns>The key material from the key exchange with the other party’s public key.</returns>
		/// <exception cref="T:System.NotImplementedException">A derived class must override this method.</exception>
		// Token: 0x06000168 RID: 360 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual byte[] DeriveKeyFromHash(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] secretPrepend, byte[] secretAppend)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		/// <summary>Derives bytes that can be used as a key using a Hash-based Message Authentication Code (HMAC).</summary>
		/// <param name="otherPartyPublicKey">The other party's public key.</param>
		/// <param name="hashAlgorithm">The hash algorithm to use to derive the key material.</param>
		/// <param name="hmacKey">The key for the HMAC.</param>
		/// <returns>The key material from the key exchange with the other party’s public key.</returns>
		// Token: 0x06000169 RID: 361 RVA: 0x000039D7 File Offset: 0x00001BD7
		public byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey)
		{
			return this.DeriveKeyFromHmac(otherPartyPublicKey, hashAlgorithm, hmacKey, null, null);
		}

		/// <summary>When implemented in a derived class, derives bytes that can be used as a key using a Hash-based Message Authentication Code (HMAC).</summary>
		/// <param name="otherPartyPublicKey">The other party's public key.</param>
		/// <param name="hashAlgorithm">The hash algorithm to use to derive the key material.</param>
		/// <param name="hmacKey">The key for the HMAC.</param>
		/// <param name="secretPrepend">A value to prepend to the derived secret before hashing.</param>
		/// <param name="secretAppend">A value to append to the derived secret before hashing.</param>
		/// <returns>The key material from the key exchange with the other party’s public key.</returns>
		/// <exception cref="T:System.NotImplementedException">A derived class must override this method.</exception>
		// Token: 0x0600016A RID: 362 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual byte[] DeriveKeyFromHmac(ECDiffieHellmanPublicKey otherPartyPublicKey, HashAlgorithmName hashAlgorithm, byte[] hmacKey, byte[] secretPrepend, byte[] secretAppend)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		/// <summary>When implemented in a derived class, derives bytes that can be used as a key using a Transport Layer Security (TLS) Pseudo-Random Function (PRF) derivation algorithm.</summary>
		/// <param name="otherPartyPublicKey">The other party's public key.</param>
		/// <param name="prfLabel">The ASCII-encoded PRF label.</param>
		/// <param name="prfSeed">The 64-byte PRF seed.</param>
		/// <returns>The key material from the key exchange with the other party’s public key.</returns>
		/// <exception cref="T:System.NotImplementedException">A derived class must override this method.</exception>
		// Token: 0x0600016B RID: 363 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual byte[] DeriveKeyTls(ECDiffieHellmanPublicKey otherPartyPublicKey, byte[] prfLabel, byte[] prfSeed)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000039E4 File Offset: 0x00001BE4
		private static Exception DerivedClassMustOverride()
		{
			return new NotImplementedException(SR.GetString("Method not supported. Derived class must override."));
		}

		/// <summary>When overridden in a derived class, exports either the public or the public and private key information from a working <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> key to an <see cref="T:System.Security.Cryptography.ECParameters" /> structure so that it can be passed to the <see cref="M:System.Security.Cryptography.ECDiffieHellman.ImportParameters(System.Security.Cryptography.ECParameters)" />   method. </summary>
		/// <param name="includePrivateParameters">
		///       <see langword="true" /> to include private parameters; otehrwise,  <see langword="false" /> to include public parameters only.</param>
		/// <returns>An object that represents the point on the curve for this key. It can be passed to the <see cref="M:System.Security.Cryptography.ECDiffieHellman.ImportParameters(System.Security.Cryptography.ECParameters)" /> method. </returns>
		/// <exception cref="T:System.NotImplementedException">A derived class must override this method. </exception>
		// Token: 0x0600016D RID: 365 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual ECParameters ExportParameters(bool includePrivateParameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		/// <summary>When overridden in a derived class, exports either the public or the public and private key information using the explicit curve form from a working <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> key to an <see cref="T:System.Security.Cryptography.ECParameters" /> structure so that it can be passed to the <see cref="M:System.Security.Cryptography.ECDiffieHellman.ImportParameters(System.Security.Cryptography.ECParameters)" />   method. </summary>
		/// <param name="includePrivateParameters">
		///       <see langword="true" /> to include private parameters; otherwise, <see langword="false" />. </param>
		/// <returns>An object that represents the point on the curve for this key, using the explicit curve format. </returns>
		/// <exception cref="T:System.NotImplementedException">A derived class must override this method. </exception>
		// Token: 0x0600016E RID: 366 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual ECParameters ExportExplicitParameters(bool includePrivateParameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		/// <summary>When overridden in a derived class, imports the specified parameters for an <see cref="T:System.Security.Cryptography.ECCurve" /> as an ephemeral key into the current <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> object. </summary>
		/// <param name="parameters">The curve's parameters to import. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="parameters" /> does not validate. </exception>
		/// <exception cref="T:System.NotImplementedException">A derived class must override this method. </exception>
		// Token: 0x0600016F RID: 367 RVA: 0x000039C4 File Offset: 0x00001BC4
		public virtual void ImportParameters(ECParameters parameters)
		{
			throw ECDiffieHellman.DerivedClassMustOverride();
		}

		/// <summary>When overridden in a derived class, generates a new ephemeral public/private key pair for the specified curve. </summary>
		/// <param name="curve">The curve used to generate an ephemeral public/private key pair. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="curve" /> does not validate. </exception>
		/// <exception cref="T:System.NotImplementedException">A derived class must override this method. </exception>
		// Token: 0x06000170 RID: 368 RVA: 0x000039F5 File Offset: 0x00001BF5
		public virtual void GenerateKey(ECCurve curve)
		{
			throw new NotSupportedException(SR.GetString("Method not supported. Derived class must override."));
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual byte[] ExportECPrivateKey()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual bool TryExportECPrivateKey(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003A06 File Offset: 0x00001C06
		public virtual void ImportECPrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellman" /> class.</summary>
		// Token: 0x06000174 RID: 372 RVA: 0x00003A0D File Offset: 0x00001C0D
		protected ECDiffieHellman()
		{
		}
	}
}
