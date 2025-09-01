using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Elliptic Curve Digital Signature Algorithm (ECDSA). </summary>
	// Token: 0x0200004B RID: 75
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class ECDsaCng : ECDsa
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDsaCng" /> class with a random key pair.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) classes are not supported on this system.</exception>
		// Token: 0x0600019C RID: 412 RVA: 0x00003EC8 File Offset: 0x000020C8
		public ECDsaCng() : this(521)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDsaCng" /> class with a random key pair, using the specified key size.</summary>
		/// <param name="keySize">The size of the key. Valid key sizes are 256, 384, and 521 bits.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) classes are not supported on this system.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="keySize" /> specifies an invalid length. </exception>
		// Token: 0x0600019D RID: 413 RVA: 0x00003ED5 File Offset: 0x000020D5
		public ECDsaCng(int keySize)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDsaCng" /> class by using the specified <see cref="T:System.Security.Cryptography.CngKey" /> object.</summary>
		/// <param name="key">The key that will be used as input to the cryptographic operations performed by the current object.</param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="key" /> does not specify an Elliptic Curve Digital Signature Algorithm (ECDSA) group.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">Cryptography Next Generation (CNG) classes are not supported on this system.</exception>
		// Token: 0x0600019E RID: 414 RVA: 0x00003ED5 File Offset: 0x000020D5
		[SecuritySafeCritical]
		public ECDsaCng(CngKey key)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.ECDsaCng" /> class whose public/private key pair is generated over the specified curve.</summary>
		/// <param name="curve">The curve used to generate the public/private key pair. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///         <paramref name="curve" /> does not validate.</exception>
		// Token: 0x0600019F RID: 415 RVA: 0x00003ED5 File Offset: 0x000020D5
		public ECDsaCng(ECCurve curve)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the hash algorithm to use when signing and verifying data.</summary>
		/// <returns>An object that specifies the hash algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is <see langword="null" />.</exception>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00003EE2 File Offset: 0x000020E2
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00003EEA File Offset: 0x000020EA
		public CngAlgorithm HashAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<HashAlgorithm>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<HashAlgorithm>k__BackingField = value;
			}
		}

		/// <summary>Gets or sets the key to use when signing and verifying data.</summary>
		/// <returns>An object that specifies the key.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000023CA File Offset: 0x000005CA
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000023CA File Offset: 0x000005CA
		public CngKey Key
		{
			get
			{
				throw new NotImplementedException();
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Generates a signature for the specified hash value.</summary>
		/// <param name="hash">The hash value of the data to be signed.</param>
		/// <returns>A digital signature for the specified hash value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="hash" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key information that is associated with the instance does not have a private key.</exception>
		// Token: 0x060001A4 RID: 420 RVA: 0x000023CA File Offset: 0x000005CA
		public override byte[] SignHash(byte[] hash)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies the specified digital signature against a specified hash value.</summary>
		/// <param name="hash">The hash value of the data to be verified.</param>
		/// <param name="signature">The digital signature of the data to be verified against the hash value.</param>
		/// <returns>
		///     <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="hash" /> or <paramref name="signature" /> is <see langword="null" />.</exception>
		// Token: 0x060001A5 RID: 421 RVA: 0x000023CA File Offset: 0x000005CA
		public override bool VerifyHash(byte[] hash, byte[] signature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Deserializes the key information from an XML string by using the specified format.</summary>
		/// <param name="xml">The XML-based key information to be deserialized.</param>
		/// <param name="format">One of the enumeration values that specifies the format of the XML string. The only currently accepted format is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="xml" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="format" /> specifies an invalid format. The only accepted value is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x060001A6 RID: 422 RVA: 0x000023CA File Offset: 0x000005CA
		public void FromXmlString(string xml, ECKeyXmlFormat format)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a signature for the specified data.</summary>
		/// <param name="data">The message data to be signed.</param>
		/// <returns>A digital signature for the specified data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="data" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key information that is associated with the instance does not have a private key.</exception>
		// Token: 0x060001A7 RID: 423 RVA: 0x000023CA File Offset: 0x000005CA
		public byte[] SignData(byte[] data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a signature for the specified data stream, reading to the end of the stream.</summary>
		/// <param name="data">The data stream to be signed.</param>
		/// <returns>A digital signature for the specified data stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="data" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key information that is associated with the instance does not have a private key.</exception>
		// Token: 0x060001A8 RID: 424 RVA: 0x000023CA File Offset: 0x000005CA
		public byte[] SignData(Stream data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates a digital signature for the specified length of data, beginning at the specified offset. </summary>
		/// <param name="data">The message data to be signed.</param>
		/// <param name="offset">The location in the string at which to start signing.</param>
		/// <param name="count">The length of the string, in characters, following <paramref name="offset" /> that will be signed.</param>
		/// <returns>A digital signature for the specified length of data.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="data" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="count" /> or <paramref name="offset" /> caused reading outside the bounds of the data string. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key information that is associated with the instance does not have a private key.</exception>
		// Token: 0x060001A9 RID: 425 RVA: 0x000023CA File Offset: 0x000005CA
		public byte[] SignData(byte[] data, int offset, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>Serializes the key information to an XML string by using the specified format.</summary>
		/// <param name="format">One of the enumeration values that specifies the format of the XML string. The only currently accepted format is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</param>
		/// <returns>A string object that contains the key information, serialized to an XML string according to the requested format.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="format" /> specifies an invalid format. The only accepted value is <see cref="F:System.Security.Cryptography.ECKeyXmlFormat.Rfc4050" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">All other errors.</exception>
		// Token: 0x060001AA RID: 426 RVA: 0x000023CA File Offset: 0x000005CA
		public string ToXmlString(ECKeyXmlFormat format)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies the digital signature of the specified data. </summary>
		/// <param name="data">The data that was signed.</param>
		/// <param name="signature">The signature to be verified.</param>
		/// <returns>
		///     <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="data" /> or <paramref name="signature" /> is <see langword="null" />.</exception>
		// Token: 0x060001AB RID: 427 RVA: 0x000023CA File Offset: 0x000005CA
		public bool VerifyData(byte[] data, byte[] signature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies the digital signature of the specified data stream, reading to the end of the stream.</summary>
		/// <param name="data">The data stream that was signed.</param>
		/// <param name="signature">The signature to be verified.</param>
		/// <returns>
		///     <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="data" /> or <paramref name="signature" /> is <see langword="null" />.</exception>
		// Token: 0x060001AC RID: 428 RVA: 0x000023CA File Offset: 0x000005CA
		public bool VerifyData(Stream data, byte[] signature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Verifies a signature for the specified length of data, beginning at the specified offset.</summary>
		/// <param name="data">The data that was signed.</param>
		/// <param name="offset">The location in the data at which the signed data begins.</param>
		/// <param name="count">The length of the data, in characters, following <paramref name="offset" /> that will be signed.</param>
		/// <param name="signature">The signature to be verified.</param>
		/// <returns>
		///     <see langword="true" /> if the signature is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="offset" /> or <paramref name="count" /> is less then zero. -or-
		///         <paramref name="offset" /> or <paramref name="count" /> is larger than the length of the byte array passed in the <paramref name="data" /> parameter.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="data" /> or <paramref name="signature" /> is <see langword="null" />.</exception>
		// Token: 0x060001AD RID: 429 RVA: 0x000023CA File Offset: 0x000005CA
		public bool VerifyData(byte[] data, int offset, int count, byte[] signature)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400032A RID: 810
		[CompilerGenerated]
		private CngAlgorithm <HashAlgorithm>k__BackingField;
	}
}
