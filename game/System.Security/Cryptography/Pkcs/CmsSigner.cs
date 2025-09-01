using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Asn1;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.X509Certificates;
using Internal.Cryptography;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class provides signing functionality.</summary>
	// Token: 0x02000072 RID: 114
	public sealed class CmsSigner
	{
		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificate" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00011A88 File Offset: 0x0000FC88
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x00011A90 File Offset: 0x0000FC90
		public X509Certificate2 Certificate
		{
			[CompilerGenerated]
			get
			{
				return this.<Certificate>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Certificate>k__BackingField = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00011A99 File Offset: 0x0000FC99
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		public AsymmetricAlgorithm PrivateKey
		{
			[CompilerGenerated]
			get
			{
				return this.<PrivateKey>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PrivateKey>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.Certificates" /> property retrieves the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that contains certificates associated with the message to be signed.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the collection of  certificates associated with the message to be signed.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00011AAA File Offset: 0x0000FCAA
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x00011AB2 File Offset: 0x0000FCB2
		public X509Certificate2Collection Certificates
		{
			[CompilerGenerated]
			get
			{
				return this.<Certificates>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Certificates>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.DigestAlgorithm" /> property sets or retrieves the <see cref="T:System.Security.Cryptography.Oid" /> that represents the hash algorithm used with the signature.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used with the signature.</returns>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00011ABB File Offset: 0x0000FCBB
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x00011AC3 File Offset: 0x0000FCC3
		public Oid DigestAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<DigestAlgorithm>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DigestAlgorithm>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption" /> property sets or retrieves the option that controls whether the root and entire chain associated with the signing certificate are included with the created CMS/PKCS #7 message.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> enumeration that specifies how much of the X509 certificate chain should be included in the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object. The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.IncludeOption" /> property can be one of the following <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> members.  
		///   Name  
		///
		///   Value  
		///
		///   Meaning  
		///
		///  <see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.None" /> 0  
		///
		///   The certificate chain is not included.  
		///
		///  <see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.ExcludeRoot" /> 1  
		///
		///   The certificate chain, except for the root certificate, is included.  
		///
		///  <see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.EndCertOnly" /> 2  
		///
		///   Only the end certificate is included.  
		///
		///  <see cref="F:System.Security.Cryptography.X509Certificates.X509IncludeOption.WholeChain" /> 3  
		///
		///   The certificate chain, including the root certificate, is included.</returns>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00011ACC File Offset: 0x0000FCCC
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		public X509IncludeOption IncludeOption
		{
			[CompilerGenerated]
			get
			{
				return this.<IncludeOption>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IncludeOption>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of signed attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Signed attributes are signed along with the specified content.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.</returns>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00011ADD File Offset: 0x0000FCDD
		// (set) Token: 0x060003BA RID: 954 RVA: 0x00011AE5 File Offset: 0x0000FCE5
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			[CompilerGenerated]
			get
			{
				return this.<SignedAttributes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<SignedAttributes>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.UnsignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of unsigned PKCS #9 attributes to be associated with the resulting <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Unsigned attributes can be modified without invalidating the signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.</returns>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00011AEE File Offset: 0x0000FCEE
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00011AF6 File Offset: 0x0000FCF6
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			[CompilerGenerated]
			get
			{
				return this.<UnsignedAttributes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UnsignedAttributes>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsSigner.SignerIdentifierType" /> property sets or retrieves the type of the identifier of the signer.</summary>
		/// <returns>A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the type of the identifier of the signer.</returns>
		/// <exception cref="T:System.ArgumentException">One of the arguments provided to a method was not valid.</exception>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00011AFF File Offset: 0x0000FCFF
		// (set) Token: 0x060003BE RID: 958 RVA: 0x00011B07 File Offset: 0x0000FD07
		public SubjectIdentifierType SignerIdentifierType
		{
			get
			{
				return this._signerIdentifierType;
			}
			set
			{
				if (value < SubjectIdentifierType.IssuerAndSerialNumber || value > SubjectIdentifierType.NoSignature)
				{
					throw new ArgumentException(SR.Format("The subject identifier type {0} is not valid.", value));
				}
				this._signerIdentifierType = value;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class by using a default subject identifier type.</summary>
		// Token: 0x060003BF RID: 959 RVA: 0x00011B2E File Offset: 0x0000FD2E
		public CmsSigner() : this(SubjectIdentifierType.IssuerAndSerialNumber, null)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified subject identifier type.</summary>
		/// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the signer identifier type.</param>
		// Token: 0x060003C0 RID: 960 RVA: 0x00011B38 File Offset: 0x0000FD38
		public CmsSigner(SubjectIdentifierType signerIdentifierType) : this(signerIdentifierType, null)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified signing certificate.</summary>
		/// <param name="certificate">An    <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</param>
		// Token: 0x060003C1 RID: 961 RVA: 0x00011B42 File Offset: 0x0000FD42
		public CmsSigner(X509Certificate2 certificate) : this(SubjectIdentifierType.IssuerAndSerialNumber, certificate)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified cryptographic service provider (CSP) parameters. <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.CspParameters)" /> is useful when you know the specific CSP and private key to use for signing.</summary>
		/// <param name="parameters">A <see cref="T:System.Security.Cryptography.CspParameters" /> object that represents the set of CSP parameters to use.</param>
		// Token: 0x060003C2 RID: 962 RVA: 0x00011B4C File Offset: 0x0000FD4C
		public CmsSigner(CspParameters parameters)
		{
			this.Certificates = new X509Certificate2Collection();
			this.SignedAttributes = new CryptographicAttributeObjectCollection();
			this.UnsignedAttributes = new CryptographicAttributeObjectCollection();
			base..ctor();
			throw new PlatformNotSupportedException();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsSigner.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.X509Certificates.X509Certificate2)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> class with the specified signer identifier type and signing certificate.</summary>
		/// <param name="signerIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the signer identifier type.</param>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</param>
		// Token: 0x060003C3 RID: 963 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public CmsSigner(SubjectIdentifierType signerIdentifierType, X509Certificate2 certificate)
		{
			this.Certificates = new X509Certificate2Collection();
			this.SignedAttributes = new CryptographicAttributeObjectCollection();
			this.UnsignedAttributes = new CryptographicAttributeObjectCollection();
			base..ctor();
			switch (signerIdentifierType)
			{
			case SubjectIdentifierType.Unknown:
				this._signerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				this.IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.IssuerAndSerialNumber:
				this._signerIdentifierType = signerIdentifierType;
				this.IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.SubjectKeyIdentifier:
				this._signerIdentifierType = signerIdentifierType;
				this.IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			case SubjectIdentifierType.NoSignature:
				this._signerIdentifierType = signerIdentifierType;
				this.IncludeOption = X509IncludeOption.None;
				break;
			default:
				this._signerIdentifierType = SubjectIdentifierType.IssuerAndSerialNumber;
				this.IncludeOption = X509IncludeOption.ExcludeRoot;
				break;
			}
			this.Certificate = certificate;
			this.DigestAlgorithm = new Oid(CmsSigner.s_defaultAlgorithm);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00011C2D File Offset: 0x0000FE2D
		internal void CheckCertificateValue()
		{
			if (this.SignerIdentifierType == SubjectIdentifierType.NoSignature)
			{
				return;
			}
			if (this.Certificate == null)
			{
				throw new PlatformNotSupportedException("No signer certificate was provided. This platform does not implement the certificate picker UI.");
			}
			if (!this.Certificate.HasPrivateKey)
			{
				throw new CryptographicException("A certificate with a private key is required.");
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00011C64 File Offset: 0x0000FE64
		internal SignerInfoAsn Sign(ReadOnlyMemory<byte> data, string contentTypeOid, bool silent, out X509Certificate2Collection chainCerts)
		{
			HashAlgorithmName digestAlgorithm = Helpers.GetDigestAlgorithm(this.DigestAlgorithm);
			IncrementalHash hasher = IncrementalHash.CreateHash(digestAlgorithm);
			hasher.AppendData(data.Span);
			byte[] hashAndReset = hasher.GetHashAndReset();
			SignerInfoAsn result = default(SignerInfoAsn);
			result.DigestAlgorithm.Algorithm = this.DigestAlgorithm;
			CryptographicAttributeObjectCollection signedAttributes = this.SignedAttributes;
			if ((signedAttributes != null && signedAttributes.Count > 0) || contentTypeOid != "1.2.840.113549.1.7.1")
			{
				List<AttributeAsn> list = CmsSigner.BuildAttributes(this.SignedAttributes);
				using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
				{
					asnWriter.PushSetOf();
					asnWriter.WriteOctetString(hashAndReset);
					asnWriter.PopSetOf();
					list.Add(new AttributeAsn
					{
						AttrType = new Oid("1.2.840.113549.1.9.4", "1.2.840.113549.1.9.4"),
						AttrValues = asnWriter.Encode()
					});
				}
				if (contentTypeOid != null)
				{
					using (AsnWriter asnWriter2 = new AsnWriter(AsnEncodingRules.DER))
					{
						asnWriter2.PushSetOf();
						asnWriter2.WriteObjectIdentifier(contentTypeOid);
						asnWriter2.PopSetOf();
						list.Add(new AttributeAsn
						{
							AttrType = new Oid("1.2.840.113549.1.9.3", "1.2.840.113549.1.9.3"),
							AttrValues = asnWriter2.Encode()
						});
					}
				}
				using (AsnWriter asnWriter3 = AsnSerializer.Serialize<SignedAttributesSet>(new SignedAttributesSet
				{
					SignedAttributes = Helpers.NormalizeSet<AttributeAsn>(list.ToArray(), delegate(byte[] normalized)
					{
						AsnReader asnReader = new AsnReader(normalized, AsnEncodingRules.DER);
						hasher.AppendData(asnReader.PeekContentBytes().Span);
					})
				}, AsnEncodingRules.BER))
				{
					result.SignedAttributes = new ReadOnlyMemory<byte>?(asnWriter3.Encode());
				}
				hashAndReset = hasher.GetHashAndReset();
			}
			switch (this.SignerIdentifierType)
			{
			case SubjectIdentifierType.IssuerAndSerialNumber:
			{
				byte[] serialNumber = this.Certificate.GetSerialNumber();
				Array.Reverse<byte>(serialNumber);
				result.Sid.IssuerAndSerialNumber = new IssuerAndSerialNumberAsn?(new IssuerAndSerialNumberAsn
				{
					Issuer = this.Certificate.IssuerName.RawData,
					SerialNumber = serialNumber
				});
				result.Version = 1;
				break;
			}
			case SubjectIdentifierType.SubjectKeyIdentifier:
				result.Sid.SubjectKeyIdentifier = new ReadOnlyMemory<byte>?(this.Certificate.GetSubjectKeyIdentifier());
				result.Version = 3;
				break;
			case SubjectIdentifierType.NoSignature:
				result.Sid.IssuerAndSerialNumber = new IssuerAndSerialNumberAsn?(new IssuerAndSerialNumberAsn
				{
					Issuer = SubjectIdentifier.DummySignerEncodedValue,
					SerialNumber = new byte[1]
				});
				result.Version = 1;
				break;
			default:
				throw new CryptographicException();
			}
			if (this.UnsignedAttributes != null && this.UnsignedAttributes.Count > 0)
			{
				List<AttributeAsn> list2 = CmsSigner.BuildAttributes(this.UnsignedAttributes);
				result.UnsignedAttributes = Helpers.NormalizeSet<AttributeAsn>(list2.ToArray(), null);
			}
			Oid algorithm;
			ReadOnlyMemory<byte> signatureValue;
			if (!CmsSignature.Sign(hashAndReset, digestAlgorithm, this.Certificate, silent, out algorithm, out signatureValue))
			{
				throw new CryptographicException("Could not determine signature algorithm for the signer certificate.");
			}
			result.SignatureValue = signatureValue;
			result.SignatureAlgorithm.Algorithm = algorithm;
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			x509Certificate2Collection.AddRange(this.Certificates);
			if (this.SignerIdentifierType != SubjectIdentifierType.NoSignature)
			{
				if (this.IncludeOption == X509IncludeOption.EndCertOnly)
				{
					x509Certificate2Collection.Add(this.Certificate);
				}
				else if (this.IncludeOption != X509IncludeOption.None)
				{
					X509Chain x509Chain = new X509Chain();
					x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
					x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllFlags;
					if (!x509Chain.Build(this.Certificate))
					{
						foreach (X509ChainStatus x509ChainStatus in x509Chain.ChainStatus)
						{
							if (x509ChainStatus.Status == X509ChainStatusFlags.PartialChain)
							{
								throw new CryptographicException("The certificate chain is incomplete, the self-signed root authority could not be determined.");
							}
						}
					}
					X509ChainElementCollection chainElements = x509Chain.ChainElements;
					int count = chainElements.Count;
					int num = count - 1;
					if (num == 0)
					{
						num = -1;
					}
					for (int j = 0; j < count; j++)
					{
						X509Certificate2 certificate = chainElements[j].Certificate;
						if (j == num && this.IncludeOption == X509IncludeOption.ExcludeRoot && certificate.SubjectName.RawData.AsSpan<byte>().SequenceEqual(certificate.IssuerName.RawData))
						{
							break;
						}
						x509Certificate2Collection.Add(certificate);
					}
				}
			}
			chainCerts = x509Certificate2Collection;
			return result;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00012108 File Offset: 0x00010308
		internal static List<AttributeAsn> BuildAttributes(CryptographicAttributeObjectCollection attributes)
		{
			List<AttributeAsn> list = new List<AttributeAsn>();
			if (attributes == null || attributes.Count == 0)
			{
				return list;
			}
			foreach (CryptographicAttributeObject cryptographicAttributeObject in attributes)
			{
				using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
				{
					asnWriter.PushSetOf();
					foreach (AsnEncodedData asnEncodedData in cryptographicAttributeObject.Values)
					{
						asnWriter.WriteEncodedValue(asnEncodedData.RawData);
					}
					asnWriter.PopSetOf();
					AttributeAsn item = new AttributeAsn
					{
						AttrType = cryptographicAttributeObject.Oid,
						AttrValues = asnWriter.Encode()
					};
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000121DC File Offset: 0x000103DC
		// Note: this type is marked as 'beforefieldinit'.
		static CmsSigner()
		{
		}

		// Token: 0x04000270 RID: 624
		private static readonly Oid s_defaultAlgorithm = Oid.FromOidValue("1.3.14.3.2.26", OidGroup.HashAlgorithm);

		// Token: 0x04000271 RID: 625
		private SubjectIdentifierType _signerIdentifierType;

		// Token: 0x04000272 RID: 626
		[CompilerGenerated]
		private X509Certificate2 <Certificate>k__BackingField;

		// Token: 0x04000273 RID: 627
		[CompilerGenerated]
		private AsymmetricAlgorithm <PrivateKey>k__BackingField;

		// Token: 0x04000274 RID: 628
		[CompilerGenerated]
		private X509Certificate2Collection <Certificates>k__BackingField;

		// Token: 0x04000275 RID: 629
		[CompilerGenerated]
		private Oid <DigestAlgorithm>k__BackingField;

		// Token: 0x04000276 RID: 630
		[CompilerGenerated]
		private X509IncludeOption <IncludeOption>k__BackingField;

		// Token: 0x04000277 RID: 631
		[CompilerGenerated]
		private CryptographicAttributeObjectCollection <SignedAttributes>k__BackingField;

		// Token: 0x04000278 RID: 632
		[CompilerGenerated]
		private CryptographicAttributeObjectCollection <UnsignedAttributes>k__BackingField;

		// Token: 0x02000073 RID: 115
		[CompilerGenerated]
		private sealed class <>c__DisplayClass39_0
		{
			// Token: 0x060003C8 RID: 968 RVA: 0x00002145 File Offset: 0x00000345
			public <>c__DisplayClass39_0()
			{
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x000121F0 File Offset: 0x000103F0
			internal void <Sign>b__0(byte[] normalized)
			{
				AsnReader asnReader = new AsnReader(normalized, AsnEncodingRules.DER);
				this.hasher.AppendData(asnReader.PeekContentBytes().Span);
			}

			// Token: 0x04000279 RID: 633
			public IncrementalHash hasher;
		}
	}
}
