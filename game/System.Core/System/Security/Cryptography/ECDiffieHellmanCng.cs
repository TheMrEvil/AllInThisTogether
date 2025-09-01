using System;
using System.Security.Permissions;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Elliptic Curve Diffie-Hellman (ECDH) algorithm. This class is used to perform cryptographic operations.</summary>
	// Token: 0x0200036D RID: 877
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ECDiffieHellmanCng : ECDiffieHellman
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class with a random key pair.</summary>
		// Token: 0x06001AAF RID: 6831 RVA: 0x0000235B File Offset: 0x0000055B
		public ECDiffieHellmanCng()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class with a random key pair, using the specified key size.</summary>
		/// <param name="keySize">The size of the key. Valid key sizes are 256, 384, and 521 bits.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="keySize" /> specifies an invalid length.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) classes are not supported on this system.</exception>
		// Token: 0x06001AB0 RID: 6832 RVA: 0x0000235B File Offset: 0x0000055B
		public ECDiffieHellmanCng(int keySize)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class by using the specified <see cref="T:System.Security.Cryptography.CngKey" /> object.</summary>
		/// <param name="key">The key that will be used as input to the cryptographic operations performed by the current object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="key" /> does not specify an Elliptic Curve Diffie-Hellman (ECDH) algorithm group.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) classes are not supported on this system.</exception>
		// Token: 0x06001AB1 RID: 6833 RVA: 0x0000235B File Offset: 0x0000055B
		[SecuritySafeCritical]
		public ECDiffieHellmanCng(CngKey key)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class whose public/private key pair is generated over the specified curve. </summary>
		/// <param name="curve">The curve used to generate the public/private key pair. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="curve" /> does not validate. </exception>
		// Token: 0x06001AB2 RID: 6834 RVA: 0x0000235B File Offset: 0x0000055B
		public ECDiffieHellmanCng(ECCurve curve)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets the hash algorithm to use when generating key material.</summary>
		/// <returns>An object that specifies the hash algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is <see langword="null." /></exception>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x0000235B File Offset: 0x0000055B
		public CngAlgorithm HashAlgorithm
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the Hash-based Message Authentication Code (HMAC) key to use when deriving key material.</summary>
		/// <returns>The Hash-based Message Authentication Code (HMAC) key to use when deriving key material.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x0000235B File Offset: 0x0000055B
		public byte[] HmacKey
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Specifies the <see cref="T:System.Security.Cryptography.CngKey" /> that is used by the current object for cryptographic operations.</summary>
		/// <returns>The key pair used by this object to perform cryptographic operations.</returns>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x0005A05A File Offset: 0x0005825A
		public CngKey Key
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the key derivation function for the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> class.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction" /> enumeration values: <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Hash" />, <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Hmac" />, or <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Tls" />. The default value is <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Hash" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The enumeration value is out of range.</exception>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0005A09C File Offset: 0x0005829C
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x0000235B File Offset: 0x0000055B
		public ECDiffieHellmanKeyDerivationFunction KeyDerivationFunction
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return ECDiffieHellmanKeyDerivationFunction.Hash;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the label value that is used for key derivation.</summary>
		/// <returns>The label value.</returns>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x0000235B File Offset: 0x0000055B
		public byte[] Label
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the public key that can be used by another <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> object to generate a shared secret agreement.</summary>
		/// <returns>The public key that is associated with this instance of the <see cref="T:System.Security.Cryptography.ECDiffieHellmanCng" /> object.</returns>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001ABC RID: 6844 RVA: 0x0005A05A File Offset: 0x0005825A
		public override ECDiffieHellmanPublicKey PublicKey
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that will be appended to the secret agreement when generating key material.</summary>
		/// <returns>The value that is appended to the secret agreement.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x0000235B File Offset: 0x0000055B
		public byte[] SecretAppend
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets a value that will be added to the beginning of the secret agreement when deriving key material.</summary>
		/// <returns>The value that is appended to the beginning of the secret agreement during key derivation.</returns>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x0000235B File Offset: 0x0000055B
		public byte[] SecretPrepend
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the seed value that will be used when deriving key material.</summary>
		/// <returns>The seed value.</returns>
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0005A05A File Offset: 0x0005825A
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x0000235B File Offset: 0x0000055B
		public byte[] Seed
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the secret agreement is used as a Hash-based Message Authentication Code (HMAC) key to derive key material.</summary>
		/// <returns>
		///     <see langword="true" /> if the secret agreement is used as an HMAC key to derive key material; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0005A0B8 File Offset: 0x000582B8
		public bool UseSecretAgreementAsHmacKey
		{
			get
			{
				ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Derives the key material that is generated from the secret agreement between two parties, given a <see cref="T:System.Security.Cryptography.CngKey" /> object that contains the second party's public key. </summary>
		/// <param name="otherPartyPublicKey">An object that contains the public part of the Elliptic Curve Diffie-Hellman (ECDH) key from the other party in the key exchange.</param>
		/// <returns>A byte array that contains the key material. This information is generated from the secret agreement that is calculated from the current object's private key and the specified public key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="otherPartyPublicKey" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="otherPartyPublicKey" /> is invalid. Either its <see cref="P:System.Security.Cryptography.CngKey.AlgorithmGroup" /> property does not specify <see cref="P:System.Security.Cryptography.CngAlgorithmGroup.ECDiffieHellman" /> or its key size does not match the key size of this instance.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object's <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.KeyDerivationFunction" /> property specifies the <see cref="F:System.Security.Cryptography.ECDiffieHellmanKeyDerivationFunction.Tls" /> key derivation function, but either <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.Label" /> or <see cref="P:System.Security.Cryptography.ECDiffieHellmanCng.Seed" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06001AC4 RID: 6852 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecuritySafeCritical]
		public byte[] DeriveKeyMaterial(CngKey otherPartyPublicKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a handle to the secret agreement generated between two parties, given a <see cref="T:System.Security.Cryptography.CngKey" /> object that contains the second party's public key.</summary>
		/// <param name="otherPartyPublicKey">An object that contains the public part of the Elliptic Curve Diffie-Hellman (ECDH) key from the other party in the key exchange.</param>
		/// <returns>A handle to the secret agreement. This information is calculated from the current object's private key and the specified public key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="otherPartyPublicKey" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="otherPartyPublicKey" /> is not an ECDH key, or it is not the correct size.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06001AC5 RID: 6853 RVA: 0x0005A05A File Offset: 0x0005825A
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public SafeNCryptSecretHandle DeriveSecretAgreementHandle(CngKey otherPartyPublicKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets a handle to the secret agreement generated between two parties, given an <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> object that contains the second party's public key.</summary>
		/// <param name="otherPartyPublicKey">The public key from the other party in the key exchange.</param>
		/// <returns>A handle to the secret agreement. This information is calculated from the current object's private key and the specified public key.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="otherPartyPublicKey" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="otherPartyPublicKey" /> is not an <see cref="T:System.Security.Cryptography.ECDiffieHellmanPublicKey" /> key. </exception>
		// Token: 0x06001AC6 RID: 6854 RVA: 0x0005A05A File Offset: 0x0005825A
		public SafeNCryptSecretHandle DeriveSecretAgreementHandle(ECDiffieHellmanPublicKey otherPartyPublicKey)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Deserializes the key information from an XML string by using the specified format.</summary>
		/// <param name="xml">The XML-based key information to be deserialized.</param>
		/// <param name="format">One of the enumeration values that specifies the format of the XML string. The only currently accepted format is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="xml" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="xml" /> is malformed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="format" /> specifies an invalid format. The only accepted value is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x06001AC7 RID: 6855 RVA: 0x0000235B File Offset: 0x0000055B
		public void FromXmlString(string xml, ECKeyXmlFormat format)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Serializes the key information to an XML string by using the specified format.</summary>
		/// <param name="format">One of the enumeration values that specifies the format of the XML string. The only currently accepted format is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</param>
		/// <returns>A string object that contains the key information, serialized to an XML string, according to the requested format.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="format" /> specifies an invalid format. The only accepted value is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</exception>
		// Token: 0x06001AC8 RID: 6856 RVA: 0x0005A05A File Offset: 0x0005825A
		public string ToXmlString(ECKeyXmlFormat format)
		{
			ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
