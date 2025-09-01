using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Asn1;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using Internal.Cryptography;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> class represents a signer associated with a <see cref="T:System.Security.Cryptography.Pkcs.SignedCms" /> object that represents a CMS/PKCS #7 message.</summary>
	// Token: 0x02000086 RID: 134
	public sealed class SignerInfo
	{
		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Version" /> property retrieves the signer information version.</summary>
		/// <returns>An int value that specifies the signer information version.</returns>
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000139C2 File Offset: 0x00011BC2
		public int Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignerIdentifier" /> property retrieves the certificate identifier of the signer associated with the signer information.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifier" /> object that uniquely identifies the certificate associated with the signer information.</returns>
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x000139CA File Offset: 0x00011BCA
		public SubjectIdentifier SignerIdentifier
		{
			[CompilerGenerated]
			get
			{
				return this.<SignerIdentifier>k__BackingField;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000139D4 File Offset: 0x00011BD4
		internal SignerInfo(ref SignerInfoAsn parsedData, SignedCms ownerDocument)
		{
			this.Version = parsedData.Version;
			this.SignerIdentifier = new SubjectIdentifier(parsedData.Sid);
			this._digestAlgorithm = parsedData.DigestAlgorithm.Algorithm;
			this._signedAttributesMemory = parsedData.SignedAttributes;
			this._signatureAlgorithm = parsedData.SignatureAlgorithm.Algorithm;
			this._signatureAlgorithmParameters = parsedData.SignatureAlgorithm.Parameters;
			this._signature = parsedData.SignatureValue;
			this._unsignedAttributes = parsedData.UnsignedAttributes;
			if (this._signedAttributesMemory != null)
			{
				SignedAttributesSet signedAttributesSet = AsnSerializer.Deserialize<SignedAttributesSet>(this._signedAttributesMemory.Value, AsnEncodingRules.BER);
				this._signedAttributes = signedAttributesSet.SignedAttributes;
			}
			this._document = ownerDocument;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.SignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of signed attributes that is associated with the signer information. Signed attributes are signed along with the rest of the message content.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the signed attributes. If there are no signed attributes, the property is an empty collection.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00013A8D File Offset: 0x00011C8D
		public CryptographicAttributeObjectCollection SignedAttributes
		{
			get
			{
				if (this._parsedSignedAttrs == null)
				{
					this._parsedSignedAttrs = SignerInfo.MakeAttributeCollection(this._signedAttributes);
				}
				return this._parsedSignedAttrs;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.UnsignedAttributes" /> property retrieves the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection of unsigned attributes that is associated with the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> content. Unsigned attributes can be modified without invalidating the signature.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unsigned attributes. If there are no unsigned attributes, the property is an empty collection.</returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00013AAE File Offset: 0x00011CAE
		public CryptographicAttributeObjectCollection UnsignedAttributes
		{
			get
			{
				if (this._parsedUnsignedAttrs == null)
				{
					this._parsedUnsignedAttrs = SignerInfo.MakeAttributeCollection(this._unsignedAttributes);
				}
				return this._parsedUnsignedAttrs;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00013ACF File Offset: 0x00011CCF
		internal ReadOnlyMemory<byte> GetSignatureMemory()
		{
			return this._signature;
		}

		/// <summary>Retrieves the signature for the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <returns>The signature for the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</returns>
		// Token: 0x06000469 RID: 1129 RVA: 0x00013AD7 File Offset: 0x00011CD7
		public byte[] GetSignature()
		{
			return this._signature.ToArray();
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.Certificate" /> property retrieves the signing certificate associated with the signer information.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object that represents the signing certificate.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00013AE4 File Offset: 0x00011CE4
		public X509Certificate2 Certificate
		{
			get
			{
				if (this._signerCertificate == null)
				{
					this._signerCertificate = this.FindSignerCertificate();
				}
				return this._signerCertificate;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos" /> property retrieves the set of counter signers associated with the signer information.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection that represents the counter signers for the signer information. If there are no counter signers, the property is an empty collection.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00013B00 File Offset: 0x00011D00
		public SignerInfoCollection CounterSignerInfos
		{
			get
			{
				if (this._parentSignerInfo != null || this._unsignedAttributes == null || this._unsignedAttributes.Length == 0)
				{
					return new SignerInfoCollection();
				}
				return this.GetCounterSigners(this._unsignedAttributes);
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.DigestAlgorithm" /> property retrieves the <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used in the computation of the signatures.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" /> object that represents the hash algorithm used with the signature.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00013B2D File Offset: 0x00011D2D
		public Oid DigestAlgorithm
		{
			get
			{
				return new Oid(this._digestAlgorithm);
			}
		}

		/// <summary>Gets the identifier for the signature algorithm used by the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <returns>The identifier for the signature algorithm used by the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00013B3A File Offset: 0x00011D3A
		public Oid SignatureAlgorithm
		{
			get
			{
				return new Oid(this._signatureAlgorithm);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00013B48 File Offset: 0x00011D48
		private SignerInfoCollection GetCounterSigners(AttributeAsn[] unsignedAttrs)
		{
			List<SignerInfo> list = new List<SignerInfo>();
			foreach (AttributeAsn attributeAsn in unsignedAttrs)
			{
				if (attributeAsn.AttrType.Value == "1.2.840.113549.1.9.6")
				{
					AsnReader asnReader = new AsnReader(attributeAsn.AttrValues, AsnEncodingRules.BER);
					AsnReader asnReader2 = asnReader.ReadSetOf(false);
					if (asnReader.HasData)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					while (asnReader2.HasData)
					{
						SignerInfoAsn signerInfoAsn = AsnSerializer.Deserialize<SignerInfoAsn>(asnReader2.GetEncodedValue(), AsnEncodingRules.BER);
						SignerInfo item = new SignerInfo(ref signerInfoAsn, this._document)
						{
							_parentSignerInfo = this
						};
						list.Add(item);
					}
				}
			}
			return new SignerInfoCollection(list.ToArray());
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature" /> method prompts the user to select a signing certificate, creates a countersignature, and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x0600046F RID: 1135 RVA: 0x000132F8 File Offset: 0x000114F8
		public void ComputeCounterSignature()
		{
			throw new PlatformNotSupportedException("No signer certificate was provided. This platform does not implement the certificate picker UI.");
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.ComputeCounterSignature(System.Security.Cryptography.Pkcs.CmsSigner)" /> method creates a countersignature by using the specified signer and adds the signature to the CMS/PKCS #7 message. Countersignatures are restricted to one level.</summary>
		/// <param name="signer">A <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object that represents the counter signer.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000470 RID: 1136 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public void ComputeCounterSignature(CmsSigner signer)
		{
			if (this._parentSignerInfo != null)
			{
				throw new CryptographicException("Only one level of counter-signatures are supported on this platform.");
			}
			if (signer == null)
			{
				throw new ArgumentNullException("signer");
			}
			signer.CheckCertificateValue();
			int num = this._document.SignerInfos.FindIndexForSigner(this);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			SignerInfo signerInfo = this._document.SignerInfos[num];
			X509Certificate2Collection newCerts;
			SignerInfoAsn value = signer.Sign(signerInfo._signature, null, false, out newCerts);
			AttributeAsn attributeAsn;
			using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
			{
				asnWriter.PushSetOf();
				AsnSerializer.Serialize<SignerInfoAsn>(value, asnWriter);
				asnWriter.PopSetOf();
				attributeAsn = new AttributeAsn
				{
					AttrType = new Oid("1.2.840.113549.1.9.6", "1.2.840.113549.1.9.6"),
					AttrValues = asnWriter.Encode()
				};
			}
			ref SignerInfoAsn ptr = ref this._document.GetRawData().SignerInfos[num];
			int num2;
			if (ptr.UnsignedAttributes == null)
			{
				ptr.UnsignedAttributes = new AttributeAsn[1];
				num2 = 0;
			}
			else
			{
				num2 = ptr.UnsignedAttributes.Length;
				Array.Resize<AttributeAsn>(ref ptr.UnsignedAttributes, num2 + 1);
			}
			ptr.UnsignedAttributes[num2] = attributeAsn;
			this._document.UpdateCertificatesFromAddition(newCerts);
			this._document.Reencode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Int32)" /> method removes the countersignature at the specified index of the <see cref="P:System.Security.Cryptography.Pkcs.SignerInfo.CounterSignerInfos" /> collection.</summary>
		/// <param name="index">The zero-based index of the countersignature to remove.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000471 RID: 1137 RVA: 0x00013D58 File Offset: 0x00011F58
		public void RemoveCounterSignature(int index)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("childIndex");
			}
			int num = this._document.SignerInfos.FindIndexForSigner(this);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			ref SignerInfoAsn ptr = ref this._document.GetRawData().SignerInfos[num];
			if (ptr.UnsignedAttributes == null)
			{
				throw new CryptographicException("The signed cryptographic message does not have a signer for the specified signer index.");
			}
			int num2 = -1;
			int num3 = -1;
			bool flag = false;
			int num4 = 0;
			AttributeAsn[] unsignedAttributes = ptr.UnsignedAttributes;
			for (int i = 0; i < unsignedAttributes.Length; i++)
			{
				AttributeAsn attributeAsn = unsignedAttributes[i];
				if (attributeAsn.AttrType.Value == "1.2.840.113549.1.9.6")
				{
					AsnReader asnReader = new AsnReader(attributeAsn.AttrValues, AsnEncodingRules.BER);
					AsnReader asnReader2 = asnReader.ReadSetOf(false);
					if (asnReader.HasData)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					int num5 = 0;
					while (asnReader2.HasData)
					{
						asnReader2.GetEncodedValue();
						if (num4 == index)
						{
							num2 = i;
							num3 = num5;
						}
						num4++;
						num5++;
					}
					if (num3 == 0 && num5 == 1)
					{
						flag = true;
					}
					if (num2 >= 0)
					{
						break;
					}
				}
			}
			if (num2 < 0)
			{
				throw new CryptographicException("The signed cryptographic message does not have a signer for the specified signer index.");
			}
			if (!flag)
			{
				ref AttributeAsn ptr2 = ref unsignedAttributes[num2];
				using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.BER))
				{
					asnWriter.PushSetOf();
					AsnReader asnReader3 = new AsnReader(ptr2.AttrValues, asnWriter.RuleSet);
					AsnReader asnReader4 = asnReader3.ReadSetOf(false);
					asnReader3.ThrowIfNotEmpty();
					int num6 = 0;
					while (asnReader4.HasData)
					{
						ReadOnlyMemory<byte> encodedValue = asnReader4.GetEncodedValue();
						if (num6 != num3)
						{
							asnWriter.WriteEncodedValue(encodedValue);
						}
						num6++;
					}
					asnWriter.PopSetOf();
					ptr2.AttrValues = asnWriter.Encode();
				}
				return;
			}
			if (unsignedAttributes.Length == 1)
			{
				ptr.UnsignedAttributes = null;
				return;
			}
			Helpers.RemoveAt<AttributeAsn>(ref ptr.UnsignedAttributes, num2);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.RemoveCounterSignature(System.Security.Cryptography.Pkcs.SignerInfo)" /> method removes the countersignature for the specified <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object.</summary>
		/// <param name="counterSignerInfo">A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the countersignature being removed.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of an argument was outside the allowable range of values as defined by the called method.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000472 RID: 1138 RVA: 0x00013F40 File Offset: 0x00012140
		public void RemoveCounterSignature(SignerInfo counterSignerInfo)
		{
			if (counterSignerInfo == null)
			{
				throw new ArgumentNullException("counterSignerInfo");
			}
			SignerInfoCollection signerInfos = this._document.SignerInfos;
			int num = signerInfos.FindIndexForSigner(this);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			num = signerInfos[num].CounterSignerInfos.FindIndexForSigner(counterSignerInfo);
			if (num < 0)
			{
				throw new CryptographicException("Cannot find the original signer.");
			}
			this.RemoveCounterSignature(num);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> method verifies the digital signature of the message and, optionally, validates the certificate.</summary>
		/// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly" /> is <see langword="true" />, only the signature is verified. If <paramref name="verifySignatureOnly" /> is <see langword="false" />, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x06000473 RID: 1139 RVA: 0x00013FA5 File Offset: 0x000121A5
		public void CheckSignature(bool verifySignatureOnly)
		{
			this.CheckSignature(new X509Certificate2Collection(), verifySignatureOnly);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> method verifies the digital signature of the message by using the specified collection of certificates and, optionally, validates the certificate.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object that can be used to validate the chain. If no additional certificates are to be used to validate the chain, use <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> instead of <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" />.</param>
		/// <param name="verifySignatureOnly">A bool value that specifies whether only the digital signature is verified. If <paramref name="verifySignatureOnly" /> is <see langword="true" />, only the signature is verified. If <paramref name="verifySignatureOnly" /> is <see langword="false" />, the digital signature is verified, the certificate chain is validated, and the purposes of the certificates are validated. The purposes of the certificate are considered valid if the certificate has no key usage or if the key usage supports digital signature or nonrepudiation.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x06000474 RID: 1140 RVA: 0x00013FB4 File Offset: 0x000121B4
		public void CheckSignature(X509Certificate2Collection extraStore, bool verifySignatureOnly)
		{
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			X509Certificate2 x509Certificate = this.Certificate;
			if (x509Certificate == null)
			{
				x509Certificate = SignerInfo.FindSignerCertificate(this.SignerIdentifier, extraStore);
				if (x509Certificate == null)
				{
					throw new CryptographicException("Cannot find the original signer.");
				}
			}
			this.Verify(extraStore, x509Certificate, verifySignatureOnly);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> method verifies the data integrity of the CMS/PKCS #7 message signer information. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> is a specialized method used in specific security infrastructure applications in which the subject uses the HashOnly member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration when setting up a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" /> object. <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckHash" /> does not authenticate the signer information because this method does not involve verifying a digital signature. For general-purpose checking of the integrity and authenticity of CMS/PKCS #7 message signer information and countersignatures, use the <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Boolean)" /> or <see cref="M:System.Security.Cryptography.Pkcs.SignerInfo.CheckSignature(System.Security.Cryptography.X509Certificates.X509Certificate2Collection,System.Boolean)" /> methods.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x06000475 RID: 1141 RVA: 0x00013FFD File Offset: 0x000121FD
		public void CheckHash()
		{
			if (!this.CheckHash(false) && !this.CheckHash(true))
			{
				throw new CryptographicException("Invalid signature.");
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001401C File Offset: 0x0001221C
		private bool CheckHash(bool compatMode)
		{
			bool result;
			using (IncrementalHash incrementalHash = this.PrepareDigest(compatMode))
			{
				if (incrementalHash == null)
				{
					result = false;
				}
				else
				{
					byte[] hashAndReset = incrementalHash.GetHashAndReset();
					result = this._signature.Span.SequenceEqual(hashAndReset);
				}
			}
			return result;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00014074 File Offset: 0x00012274
		private X509Certificate2 FindSignerCertificate()
		{
			return SignerInfo.FindSignerCertificate(this.SignerIdentifier, this._document.Certificates);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001408C File Offset: 0x0001228C
		private static X509Certificate2 FindSignerCertificate(SubjectIdentifier signerIdentifier, X509Certificate2Collection extraStore)
		{
			if (extraStore == null || extraStore.Count == 0)
			{
				return null;
			}
			X509Certificate2Collection x509Certificate2Collection = null;
			X509Certificate2 x509Certificate = null;
			SubjectIdentifierType type = signerIdentifier.Type;
			if (type != SubjectIdentifierType.IssuerAndSerialNumber)
			{
				if (type == SubjectIdentifierType.SubjectKeyIdentifier)
				{
					x509Certificate2Collection = extraStore.Find(X509FindType.FindBySubjectKeyIdentifier, signerIdentifier.Value, false);
					if (x509Certificate2Collection.Count > 0)
					{
						x509Certificate = x509Certificate2Collection[0];
					}
				}
			}
			else
			{
				X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)signerIdentifier.Value;
				x509Certificate2Collection = extraStore.Find(X509FindType.FindBySerialNumber, x509IssuerSerial.SerialNumber, false);
				foreach (X509Certificate2 x509Certificate2 in x509Certificate2Collection)
				{
					if (x509Certificate2.IssuerName.Name == x509IssuerSerial.IssuerName)
					{
						x509Certificate = x509Certificate2;
						break;
					}
				}
			}
			if (x509Certificate2Collection != null)
			{
				foreach (X509Certificate2 x509Certificate3 in x509Certificate2Collection)
				{
					if (x509Certificate3 != x509Certificate)
					{
						x509Certificate3.Dispose();
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00014164 File Offset: 0x00012364
		private IncrementalHash PrepareDigest(bool compatMode)
		{
			IncrementalHash incrementalHash = IncrementalHash.CreateHash(this.GetDigestAlgorithm());
			if (this._parentSignerInfo == null)
			{
				if (this._document.Detached)
				{
					ref SignedDataAsn rawData = ref this._document.GetRawData();
					ReadOnlyMemory<byte>? content = rawData.EncapContentInfo.Content;
					if (content != null)
					{
						incrementalHash.AppendData(SignedCms.GetContent(content.Value, rawData.EncapContentInfo.ContentType).Span);
					}
				}
				incrementalHash.AppendData(this._document.GetHashableContentSpan());
			}
			else
			{
				incrementalHash.AppendData(this._parentSignerInfo._signature.Span);
			}
			bool flag = this._parentSignerInfo != null || this._signedAttributes != null;
			if (this._signedAttributes != null)
			{
				byte[] hashAndReset = incrementalHash.GetHashAndReset();
				using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
				{
					if (compatMode)
					{
						asnWriter.PushSequence();
					}
					else
					{
						asnWriter.PushSetOf();
					}
					foreach (AttributeAsn attributeAsn in this._signedAttributes)
					{
						AsnSerializer.Serialize<AttributeAsn>(attributeAsn, asnWriter);
						if (attributeAsn.AttrType.Value == "1.2.840.113549.1.9.4")
						{
							CryptographicAttributeObject cryptographicAttributeObject = SignerInfo.MakeAttribute(attributeAsn);
							if (cryptographicAttributeObject.Values.Count != 1)
							{
								throw new CryptographicException("The hash value is not correct.");
							}
							Pkcs9MessageDigest pkcs9MessageDigest = (Pkcs9MessageDigest)cryptographicAttributeObject.Values[0];
							if (!hashAndReset.AsSpan<byte>().SequenceEqual(pkcs9MessageDigest.MessageDigest))
							{
								throw new CryptographicException("The hash value is not correct.");
							}
							flag = false;
						}
					}
					if (compatMode)
					{
						asnWriter.PopSequence();
						byte[] array = asnWriter.Encode();
						array[0] = 49;
						incrementalHash.AppendData(array);
						goto IL_1C4;
					}
					asnWriter.PopSetOf();
					incrementalHash.AppendData(asnWriter.Encode());
					goto IL_1C4;
				}
			}
			if (compatMode)
			{
				return null;
			}
			IL_1C4:
			if (flag)
			{
				throw new CryptographicException("The cryptographic message does not contain an expected authenticated attribute.");
			}
			return incrementalHash;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00014354 File Offset: 0x00012554
		private void Verify(X509Certificate2Collection extraStore, X509Certificate2 certificate, bool verifySignatureOnly)
		{
			CmsSignature cmsSignature = CmsSignature.Resolve(this.SignatureAlgorithm.Value);
			if (cmsSignature == null)
			{
				throw new CryptographicException("Unknown algorithm '{0}'.", this.SignatureAlgorithm.Value);
			}
			if (!this.VerifySignature(cmsSignature, certificate, false) && !this.VerifySignature(cmsSignature, certificate, true))
			{
				throw new CryptographicException("Invalid signature.");
			}
			if (!verifySignatureOnly)
			{
				X509Chain x509Chain = new X509Chain();
				x509Chain.ChainPolicy.ExtraStore.AddRange(extraStore);
				x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
				x509Chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
				if (!x509Chain.Build(certificate))
				{
					throw new CryptographicException("Certificate trust could not be established. The first reported error is: {0}", x509Chain.ChainStatus.FirstOrDefault<X509ChainStatus>().StatusInformation);
				}
				foreach (X509Extension x509Extension in certificate.Extensions)
				{
					if (x509Extension.Oid.Value == "2.5.29.15")
					{
						X509KeyUsageExtension x509KeyUsageExtension = x509Extension as X509KeyUsageExtension;
						if (x509KeyUsageExtension == null)
						{
							x509KeyUsageExtension = new X509KeyUsageExtension();
							x509KeyUsageExtension.CopyFrom(x509Extension);
						}
						if ((x509KeyUsageExtension.KeyUsages & (X509KeyUsageFlags.NonRepudiation | X509KeyUsageFlags.DigitalSignature)) == X509KeyUsageFlags.None)
						{
							throw new CryptographicException("The certificate is not valid for the requested usage.");
						}
					}
				}
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014478 File Offset: 0x00012678
		private bool VerifySignature(CmsSignature signatureProcessor, X509Certificate2 certificate, bool compatMode)
		{
			bool result;
			using (IncrementalHash incrementalHash = this.PrepareDigest(compatMode))
			{
				if (incrementalHash == null)
				{
					result = false;
				}
				else
				{
					byte[] hashAndReset = incrementalHash.GetHashAndReset();
					byte[] signature = this._signature.ToArray();
					result = signatureProcessor.VerifySignature(hashAndReset, signature, this.DigestAlgorithm.Value, incrementalHash.AlgorithmName, this._signatureAlgorithmParameters, certificate);
				}
			}
			return result;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000144E8 File Offset: 0x000126E8
		private HashAlgorithmName GetDigestAlgorithm()
		{
			return Helpers.GetDigestAlgorithm(this.DigestAlgorithm.Value);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x000144FC File Offset: 0x000126FC
		internal static CryptographicAttributeObjectCollection MakeAttributeCollection(AttributeAsn[] attributes)
		{
			CryptographicAttributeObjectCollection cryptographicAttributeObjectCollection = new CryptographicAttributeObjectCollection();
			if (attributes == null)
			{
				return cryptographicAttributeObjectCollection;
			}
			foreach (AttributeAsn attribute in attributes)
			{
				cryptographicAttributeObjectCollection.AddWithoutMerge(SignerInfo.MakeAttribute(attribute));
			}
			return cryptographicAttributeObjectCollection;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001453C File Offset: 0x0001273C
		private static CryptographicAttributeObject MakeAttribute(AttributeAsn attribute)
		{
			Oid oid = new Oid(attribute.AttrType);
			AsnReader asnReader = new AsnReader(attribute.AttrValues, AsnEncodingRules.BER);
			AsnReader asnReader2 = asnReader.ReadSetOf(false);
			if (asnReader.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			AsnEncodedDataCollection asnEncodedDataCollection = new AsnEncodedDataCollection();
			while (asnReader2.HasData)
			{
				byte[] encodedAttribute = asnReader2.GetEncodedValue().ToArray();
				asnEncodedDataCollection.Add(Helpers.CreateBestPkcs9AttributeObjectAvailable(oid, encodedAttribute));
			}
			return new CryptographicAttributeObject(oid, asnEncodedDataCollection);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal SignerInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040002AE RID: 686
		[CompilerGenerated]
		private readonly int <Version>k__BackingField;

		// Token: 0x040002AF RID: 687
		[CompilerGenerated]
		private readonly SubjectIdentifier <SignerIdentifier>k__BackingField;

		// Token: 0x040002B0 RID: 688
		private readonly Oid _digestAlgorithm;

		// Token: 0x040002B1 RID: 689
		private readonly AttributeAsn[] _signedAttributes;

		// Token: 0x040002B2 RID: 690
		private readonly ReadOnlyMemory<byte>? _signedAttributesMemory;

		// Token: 0x040002B3 RID: 691
		private readonly Oid _signatureAlgorithm;

		// Token: 0x040002B4 RID: 692
		private readonly ReadOnlyMemory<byte>? _signatureAlgorithmParameters;

		// Token: 0x040002B5 RID: 693
		private readonly ReadOnlyMemory<byte> _signature;

		// Token: 0x040002B6 RID: 694
		private readonly AttributeAsn[] _unsignedAttributes;

		// Token: 0x040002B7 RID: 695
		private readonly SignedCms _document;

		// Token: 0x040002B8 RID: 696
		private X509Certificate2 _signerCertificate;

		// Token: 0x040002B9 RID: 697
		private SignerInfo _parentSignerInfo;

		// Token: 0x040002BA RID: 698
		private CryptographicAttributeObjectCollection _parsedSignedAttrs;

		// Token: 0x040002BB RID: 699
		private CryptographicAttributeObjectCollection _parsedUnsignedAttrs;
	}
}
