using System;
using System.Buffers;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.Asn1;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.Pkcs.Asn1;
using System.Security.Cryptography.X509Certificates;

namespace Internal.Cryptography.Pal.AnyOS
{
	// Token: 0x02000114 RID: 276
	internal sealed class ManagedPkcsPal : PkcsPal
	{
		// Token: 0x06000702 RID: 1794 RVA: 0x0001CB88 File Offset: 0x0001AD88
		public override byte[] EncodeOctetString(byte[] octets)
		{
			byte[] result;
			using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
			{
				asnWriter.WriteOctetString(octets);
				result = asnWriter.Encode();
			}
			return result;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		public unsafe override byte[] DecodeOctetString(byte[] encodedOctets)
		{
			AsnReader asnReader = new AsnReader(encodedOctets, AsnEncodingRules.BER);
			Span<byte> destination = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
			ReadOnlySpan<byte> readOnlySpan = default(Span<byte>);
			byte[] array = null;
			byte[] result;
			try
			{
				ReadOnlyMemory<byte> readOnlyMemory;
				if (!asnReader.TryGetPrimitiveOctetStringBytes(out readOnlyMemory))
				{
					int length;
					if (asnReader.TryCopyOctetStringBytes(destination, out length))
					{
						readOnlySpan = destination.Slice(0, length);
					}
					else
					{
						array = ArrayPool<byte>.Shared.Rent(asnReader.PeekContentBytes().Length);
						if (!asnReader.TryCopyOctetStringBytes(array, out length))
						{
							throw new CryptographicException();
						}
						readOnlySpan = new ReadOnlySpan<byte>(array, 0, length);
					}
				}
				else
				{
					readOnlySpan = readOnlyMemory.Span;
				}
				if (asnReader.HasData)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				result = readOnlySpan.ToArray();
			}
			finally
			{
				if (array != null)
				{
					Array.Clear(array, 0, readOnlySpan.Length);
					ArrayPool<byte>.Shared.Return(array, false);
				}
			}
			return result;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
		public override byte[] EncodeUtcTime(DateTime utcTime)
		{
			byte[] result;
			using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.DER))
			{
				try
				{
					if (utcTime.Kind == DateTimeKind.Unspecified)
					{
						asnWriter.WriteUtcTime(utcTime.ToLocalTime(), 1950);
					}
					else
					{
						asnWriter.WriteUtcTime(utcTime, 1950);
					}
					result = asnWriter.Encode();
				}
				catch (ArgumentException ex)
				{
					throw new CryptographicException(ex.Message, ex);
				}
			}
			return result;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001CD4C File Offset: 0x0001AF4C
		public override DateTime DecodeUtcTime(byte[] encodedUtcTime)
		{
			AsnReader asnReader = new AsnReader(encodedUtcTime, AsnEncodingRules.BER);
			DateTimeOffset utcTime = asnReader.GetUtcTime(2049);
			if (asnReader.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return utcTime.UtcDateTime;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		public override string DecodeOid(byte[] encodedOid)
		{
			if (ManagedPkcsPal.s_invalidEmptyOid.AsSpan<byte>().SequenceEqual(encodedOid))
			{
				return string.Empty;
			}
			AsnReader asnReader = new AsnReader(encodedOid, AsnEncodingRules.BER);
			string result = asnReader.ReadObjectIdentifierAsString();
			if (asnReader.HasData)
			{
				throw new CryptographicException("ASN1 corrupted data.");
			}
			return result;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		public override Oid GetEncodedMessageType(byte[] encodedMessage)
		{
			ContentInfoAsn contentInfoAsn = AsnSerializer.Deserialize<ContentInfoAsn>(new AsnReader(encodedMessage, AsnEncodingRules.BER).GetEncodedValue(), AsnEncodingRules.BER);
			string contentType = contentInfoAsn.ContentType;
			if (contentType == "1.2.840.113549.1.7.1" || contentType == "1.2.840.113549.1.7.2" || contentType == "1.2.840.113549.1.7.3" || contentType == "1.2.840.113549.1.7.4" || contentType == "1.2.840.113549.1.7.5" || contentType == "1.2.840.113549.1.7.6")
			{
				return new Oid(contentInfoAsn.ContentType);
			}
			throw new CryptographicException("Invalid cryptographic message type.");
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001CE6C File Offset: 0x0001B06C
		public override DecryptorPal Decode(byte[] encodedMessage, out int version, out ContentInfo contentInfo, out AlgorithmIdentifier contentEncryptionAlgorithm, out X509Certificate2Collection originatorCerts, out CryptographicAttributeObjectCollection unprotectedAttributes)
		{
			ContentInfoAsn contentInfoAsn = AsnSerializer.Deserialize<ContentInfoAsn>(new AsnReader(encodedMessage, AsnEncodingRules.BER).GetEncodedValue(), AsnEncodingRules.BER);
			if (contentInfoAsn.ContentType != "1.2.840.113549.1.7.3")
			{
				throw new CryptographicException("Invalid cryptographic message type.");
			}
			byte[] array = contentInfoAsn.Content.ToArray();
			EnvelopedDataAsn envelopedDataAsn = AsnSerializer.Deserialize<EnvelopedDataAsn>(array, AsnEncodingRules.BER);
			version = envelopedDataAsn.Version;
			contentInfo = new ContentInfo(new Oid(envelopedDataAsn.EncryptedContentInfo.ContentType), ((envelopedDataAsn.EncryptedContentInfo.EncryptedContent != null) ? envelopedDataAsn.EncryptedContentInfo.EncryptedContent.GetValueOrDefault().ToArray() : null) ?? Array.Empty<byte>());
			contentEncryptionAlgorithm = envelopedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm.ToPresentationObject();
			originatorCerts = new X509Certificate2Collection();
			if (envelopedDataAsn.OriginatorInfo != null && envelopedDataAsn.OriginatorInfo.CertificateSet != null)
			{
				foreach (CertificateChoiceAsn certificateChoiceAsn in envelopedDataAsn.OriginatorInfo.CertificateSet)
				{
					if (certificateChoiceAsn.Certificate != null)
					{
						originatorCerts.Add(new X509Certificate2(certificateChoiceAsn.Certificate.Value.ToArray()));
					}
				}
			}
			unprotectedAttributes = SignerInfo.MakeAttributeCollection(envelopedDataAsn.UnprotectedAttributes);
			List<RecipientInfo> list = new List<RecipientInfo>();
			foreach (RecipientInfoAsn recipientInfoAsn in envelopedDataAsn.RecipientInfos)
			{
				if (recipientInfoAsn.Ktri != null)
				{
					list.Add(new KeyTransRecipientInfo(new ManagedPkcsPal.ManagedKeyTransPal(recipientInfoAsn.Ktri)));
				}
				else
				{
					if (recipientInfoAsn.Kari == null)
					{
						throw new CryptographicException();
					}
					for (int j = 0; j < recipientInfoAsn.Kari.RecipientEncryptedKeys.Length; j++)
					{
						list.Add(new KeyAgreeRecipientInfo(new ManagedPkcsPal.ManagedKeyAgreePal(recipientInfoAsn.Kari, j)));
					}
				}
			}
			return new ManagedPkcsPal.ManagedDecryptorPal(array, envelopedDataAsn, new RecipientInfoCollection(list));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001D058 File Offset: 0x0001B258
		public unsafe override byte[] Encrypt(CmsRecipientCollection recipients, ContentInfo contentInfo, AlgorithmIdentifier contentEncryptionAlgorithm, X509Certificate2Collection originatorCerts, CryptographicAttributeObjectCollection unprotectedAttributes)
		{
			byte[] array;
			byte[] parameterBytes;
			byte[] encryptedContent = this.EncryptContent(contentInfo, contentEncryptionAlgorithm, out array, out parameterBytes);
			byte[] array2;
			if ((array2 = array) == null || array2.Length == 0)
			{
				byte* ptr = null;
			}
			else
			{
				byte* ptr = &array2[0];
			}
			byte[] result;
			try
			{
				result = ManagedPkcsPal.Encrypt(recipients, contentInfo, contentEncryptionAlgorithm, originatorCerts, unprotectedAttributes, encryptedContent, array, parameterBytes);
			}
			finally
			{
				Array.Clear(array, 0, array.Length);
			}
			return result;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001D0C0 File Offset: 0x0001B2C0
		private static byte[] Encrypt(CmsRecipientCollection recipients, ContentInfo contentInfo, AlgorithmIdentifier contentEncryptionAlgorithm, X509Certificate2Collection originatorCerts, CryptographicAttributeObjectCollection unprotectedAttributes, byte[] encryptedContent, byte[] cek, byte[] parameterBytes)
		{
			EnvelopedDataAsn envelopedDataAsn = default(EnvelopedDataAsn);
			envelopedDataAsn.EncryptedContentInfo.ContentType = contentInfo.ContentType.Value;
			envelopedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm.Algorithm = contentEncryptionAlgorithm.Oid;
			envelopedDataAsn.EncryptedContentInfo.ContentEncryptionAlgorithm.Parameters = new ReadOnlyMemory<byte>?(parameterBytes);
			envelopedDataAsn.EncryptedContentInfo.EncryptedContent = new ReadOnlyMemory<byte>?(encryptedContent);
			EnvelopedDataAsn envelopedDataAsn2 = envelopedDataAsn;
			if (unprotectedAttributes != null && unprotectedAttributes.Count > 0)
			{
				List<AttributeAsn> list = CmsSigner.BuildAttributes(unprotectedAttributes);
				envelopedDataAsn2.UnprotectedAttributes = Helpers.NormalizeSet<AttributeAsn>(list.ToArray(), null);
			}
			if (originatorCerts != null && originatorCerts.Count > 0)
			{
				CertificateChoiceAsn[] array = new CertificateChoiceAsn[originatorCerts.Count];
				for (int i = 0; i < originatorCerts.Count; i++)
				{
					array[i].Certificate = new ReadOnlyMemory<byte>?(originatorCerts[i].RawData);
				}
				envelopedDataAsn2.OriginatorInfo = new OriginatorInfoAsn
				{
					CertificateSet = array
				};
			}
			envelopedDataAsn2.RecipientInfos = new RecipientInfoAsn[recipients.Count];
			bool flag = true;
			for (int j = 0; j < recipients.Count; j++)
			{
				CmsRecipient cmsRecipient = recipients[j];
				if (!(cmsRecipient.Certificate.GetKeyAlgorithm() == "1.2.840.113549.1.1.1"))
				{
					throw new CryptographicException("Unknown algorithm '{0}'.", cmsRecipient.Certificate.GetKeyAlgorithm());
				}
				bool flag2;
				envelopedDataAsn2.RecipientInfos[j].Ktri = ManagedPkcsPal.MakeKtri(cek, cmsRecipient, out flag2);
				flag = (flag && flag2);
			}
			if (envelopedDataAsn2.OriginatorInfo != null || !flag || envelopedDataAsn2.UnprotectedAttributes != null)
			{
				envelopedDataAsn2.Version = 2;
			}
			return Helpers.EncodeContentInfo<EnvelopedDataAsn>(envelopedDataAsn2, "1.2.840.113549.1.7.3", AsnEncodingRules.DER);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001D278 File Offset: 0x0001B478
		private byte[] EncryptContent(ContentInfo contentInfo, AlgorithmIdentifier contentEncryptionAlgorithm, out byte[] cek, out byte[] parameterBytes)
		{
			byte[] result;
			using (SymmetricAlgorithm symmetricAlgorithm = ManagedPkcsPal.OpenAlgorithm(contentEncryptionAlgorithm))
			{
				using (ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateEncryptor())
				{
					cek = symmetricAlgorithm.Key;
					if (symmetricAlgorithm is RC2)
					{
						using (AsnWriter asnWriter = AsnSerializer.Serialize<Rc2CbcParameters>(new Rc2CbcParameters(symmetricAlgorithm.IV, symmetricAlgorithm.KeySize), AsnEncodingRules.DER))
						{
							parameterBytes = asnWriter.Encode();
							goto IL_5F;
						}
					}
					parameterBytes = this.EncodeOctetString(symmetricAlgorithm.IV);
					IL_5F:
					byte[] array = contentInfo.Content;
					if (contentInfo.ContentType.Value == "1.2.840.113549.1.7.1")
					{
						array = this.EncodeOctetString(array);
						result = cryptoTransform.OneShot(array);
					}
					else if (contentInfo.Content.Length == 0)
					{
						result = cryptoTransform.OneShot(contentInfo.Content);
					}
					else
					{
						AsnReader asnReader = new AsnReader(contentInfo.Content, AsnEncodingRules.BER);
						result = cryptoTransform.OneShot(asnReader.PeekContentBytes().ToArray());
					}
				}
			}
			return result;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001D39C File Offset: 0x0001B59C
		public override Exception CreateRecipientsNotFoundException()
		{
			return new CryptographicException("The enveloped-data message does not contain the specified recipient.");
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public override Exception CreateRecipientInfosAfterEncryptException()
		{
			return ManagedPkcsPal.CreateInvalidMessageTypeException();
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public override Exception CreateDecryptAfterEncryptException()
		{
			return ManagedPkcsPal.CreateInvalidMessageTypeException();
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public override Exception CreateDecryptTwiceException()
		{
			return ManagedPkcsPal.CreateInvalidMessageTypeException();
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001D3AF File Offset: 0x0001B5AF
		private static Exception CreateInvalidMessageTypeException()
		{
			return new CryptographicException("Invalid cryptographic message type.");
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		private static KeyTransRecipientInfoAsn MakeKtri(byte[] cek, CmsRecipient recipient, out bool v0Recipient)
		{
			KeyTransRecipientInfoAsn keyTransRecipientInfoAsn = new KeyTransRecipientInfoAsn();
			if (recipient.RecipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				keyTransRecipientInfoAsn.Version = 2;
				keyTransRecipientInfoAsn.Rid.SubjectKeyIdentifier = new ReadOnlyMemory<byte>?(recipient.Certificate.GetSubjectKeyIdentifier());
			}
			else
			{
				if (recipient.RecipientIdentifierType != SubjectIdentifierType.IssuerAndSerialNumber)
				{
					throw new CryptographicException("The subject identifier type {0} is not valid.", recipient.RecipientIdentifierType.ToString());
				}
				byte[] serialNumber = recipient.Certificate.GetSerialNumber();
				Array.Reverse<byte>(serialNumber);
				IssuerAndSerialNumberAsn value = new IssuerAndSerialNumberAsn
				{
					Issuer = recipient.Certificate.IssuerName.RawData,
					SerialNumber = serialNumber
				};
				keyTransRecipientInfoAsn.Rid.IssuerAndSerialNumber = new IssuerAndSerialNumberAsn?(value);
			}
			RSAEncryptionPadding padding;
			if (recipient.Certificate.GetKeyAlgorithm() == "1.2.840.113549.1.1.7")
			{
				padding = RSAEncryptionPadding.OaepSHA1;
				keyTransRecipientInfoAsn.KeyEncryptionAlgorithm.Algorithm = new Oid("1.2.840.113549.1.1.7", "1.2.840.113549.1.1.7");
				keyTransRecipientInfoAsn.KeyEncryptionAlgorithm.Parameters = new ReadOnlyMemory<byte>?(ManagedPkcsPal.s_rsaOaepSha1Parameters);
			}
			else
			{
				padding = RSAEncryptionPadding.Pkcs1;
				keyTransRecipientInfoAsn.KeyEncryptionAlgorithm.Algorithm = new Oid("1.2.840.113549.1.1.1", "1.2.840.113549.1.1.1");
				keyTransRecipientInfoAsn.KeyEncryptionAlgorithm.Parameters = new ReadOnlyMemory<byte>?(ManagedPkcsPal.s_rsaPkcsParameters);
			}
			using (RSA rsapublicKey = recipient.Certificate.GetRSAPublicKey())
			{
				keyTransRecipientInfoAsn.EncryptedKey = rsapublicKey.Encrypt(cek, padding);
			}
			v0Recipient = (keyTransRecipientInfoAsn.Version == 0);
			return keyTransRecipientInfoAsn;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001D560 File Offset: 0x0001B760
		public override void AddCertsFromStoreForDecryption(X509Certificate2Collection certs)
		{
			certs.AddRange(Helpers.GetStoreCertificates(StoreName.My, StoreLocation.CurrentUser, false));
			try
			{
				certs.AddRange(Helpers.GetStoreCertificates(StoreName.My, StoreLocation.LocalMachine, false));
			}
			catch (CryptographicException)
			{
			}
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001D5A0 File Offset: 0x0001B7A0
		public override byte[] GetSubjectKeyIdentifier(X509Certificate2 certificate)
		{
			return certificate.GetSubjectKeyIdentifier();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		public override T GetPrivateKeyForSigning<T>(X509Certificate2 certificate, bool silent)
		{
			return this.GetPrivateKey<T>(certificate);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		public override T GetPrivateKeyForDecryption<T>(X509Certificate2 certificate, bool silent)
		{
			return this.GetPrivateKey<T>(certificate);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001D5B4 File Offset: 0x0001B7B4
		private T GetPrivateKey<T>(X509Certificate2 certificate) where T : AsymmetricAlgorithm
		{
			if (typeof(T) == typeof(RSA))
			{
				return (T)((object)certificate.GetRSAPrivateKey());
			}
			if (typeof(T) == typeof(ECDsa))
			{
				return (T)((object)certificate.GetECDsaPrivateKey());
			}
			return default(T);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001D618 File Offset: 0x0001B818
		private static SymmetricAlgorithm OpenAlgorithm(AlgorithmIdentifierAsn contentEncryptionAlgorithm)
		{
			SymmetricAlgorithm symmetricAlgorithm = ManagedPkcsPal.OpenAlgorithm(contentEncryptionAlgorithm.Algorithm);
			if (symmetricAlgorithm is RC2)
			{
				if (contentEncryptionAlgorithm.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				Rc2CbcParameters rc2CbcParameters = AsnSerializer.Deserialize<Rc2CbcParameters>(contentEncryptionAlgorithm.Parameters.Value, AsnEncodingRules.BER);
				symmetricAlgorithm.KeySize = rc2CbcParameters.GetEffectiveKeyBits();
				symmetricAlgorithm.IV = rc2CbcParameters.Iv.ToArray();
			}
			else
			{
				if (contentEncryptionAlgorithm.Parameters == null)
				{
					throw new CryptographicException("ASN1 corrupted data.");
				}
				AsnReader asnReader = new AsnReader(contentEncryptionAlgorithm.Parameters.Value, AsnEncodingRules.BER);
				ReadOnlyMemory<byte> readOnlyMemory;
				if (asnReader.TryGetPrimitiveOctetStringBytes(out readOnlyMemory))
				{
					symmetricAlgorithm.IV = readOnlyMemory.ToArray();
				}
				else
				{
					byte[] array = new byte[symmetricAlgorithm.BlockSize / 8];
					int num;
					if (!asnReader.TryCopyOctetStringBytes(array, out num) || num != array.Length)
					{
						throw new CryptographicException("ASN1 corrupted data.");
					}
					symmetricAlgorithm.IV = array;
				}
			}
			return symmetricAlgorithm;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001D70C File Offset: 0x0001B90C
		private static SymmetricAlgorithm OpenAlgorithm(AlgorithmIdentifier algorithmIdentifier)
		{
			SymmetricAlgorithm symmetricAlgorithm = ManagedPkcsPal.OpenAlgorithm(algorithmIdentifier.Oid);
			if (symmetricAlgorithm is RC2)
			{
				if (algorithmIdentifier.KeyLength != 0)
				{
					symmetricAlgorithm.KeySize = algorithmIdentifier.KeyLength;
				}
				else
				{
					symmetricAlgorithm.KeySize = 128;
				}
			}
			return symmetricAlgorithm;
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001D750 File Offset: 0x0001B950
		private static SymmetricAlgorithm OpenAlgorithm(Oid algorithmIdentifier)
		{
			string value = algorithmIdentifier.Value;
			SymmetricAlgorithm symmetricAlgorithm;
			if (!(value == "1.2.840.113549.3.2"))
			{
				if (!(value == "1.3.14.3.2.7"))
				{
					if (!(value == "1.2.840.113549.3.7"))
					{
						if (!(value == "2.16.840.1.101.3.4.1.2"))
						{
							if (!(value == "2.16.840.1.101.3.4.1.22"))
							{
								if (!(value == "2.16.840.1.101.3.4.1.42"))
								{
									throw new CryptographicException("Unknown algorithm '{0}'.", algorithmIdentifier.Value);
								}
								symmetricAlgorithm = Aes.Create();
								symmetricAlgorithm.KeySize = 256;
							}
							else
							{
								symmetricAlgorithm = Aes.Create();
								symmetricAlgorithm.KeySize = 192;
							}
						}
						else
						{
							symmetricAlgorithm = Aes.Create();
							symmetricAlgorithm.KeySize = 128;
						}
					}
					else
					{
						symmetricAlgorithm = TripleDES.Create();
					}
				}
				else
				{
					symmetricAlgorithm = DES.Create();
				}
			}
			else
			{
				symmetricAlgorithm = RC2.Create();
			}
			symmetricAlgorithm.Padding = PaddingMode.PKCS7;
			symmetricAlgorithm.Mode = CipherMode.CBC;
			return symmetricAlgorithm;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001D825 File Offset: 0x0001BA25
		public ManagedPkcsPal()
		{
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001D82D File Offset: 0x0001BA2D
		// Note: this type is marked as 'beforefieldinit'.
		static ManagedPkcsPal()
		{
			byte[] array = new byte[2];
			array[0] = 6;
			ManagedPkcsPal.s_invalidEmptyOid = array;
			byte[] array2 = new byte[2];
			array2[0] = 5;
			ManagedPkcsPal.s_rsaPkcsParameters = array2;
			byte[] array3 = new byte[2];
			array3[0] = 48;
			ManagedPkcsPal.s_rsaOaepSha1Parameters = array3;
		}

		// Token: 0x04000467 RID: 1127
		private static readonly byte[] s_invalidEmptyOid;

		// Token: 0x04000468 RID: 1128
		private static readonly byte[] s_rsaPkcsParameters;

		// Token: 0x04000469 RID: 1129
		private static readonly byte[] s_rsaOaepSha1Parameters;

		// Token: 0x02000115 RID: 277
		private sealed class ManagedDecryptorPal : DecryptorPal
		{
			// Token: 0x0600071C RID: 1820 RVA: 0x0001D85D File Offset: 0x0001BA5D
			public ManagedDecryptorPal(byte[] dataCopy, EnvelopedDataAsn envelopedDataAsn, RecipientInfoCollection recipientInfos) : base(recipientInfos)
			{
				this._dataCopy = dataCopy;
				this._envelopedData = envelopedDataAsn;
			}

			// Token: 0x0600071D RID: 1821 RVA: 0x0001D874 File Offset: 0x0001BA74
			public unsafe override ContentInfo TryDecrypt(RecipientInfo recipientInfo, X509Certificate2 cert, X509Certificate2Collection originatorCerts, X509Certificate2Collection extraStore, out Exception exception)
			{
				ManagedPkcsPal.ManagedKeyTransPal managedKeyTransPal = recipientInfo.Pal as ManagedPkcsPal.ManagedKeyTransPal;
				if (managedKeyTransPal != null)
				{
					byte[] array = managedKeyTransPal.DecryptCek(cert, out exception);
					byte[] array2;
					if ((array2 = array) == null || array2.Length == 0)
					{
						byte* ptr = null;
					}
					else
					{
						byte* ptr = &array2[0];
					}
					byte[] array3;
					try
					{
						if (exception != null)
						{
							return null;
						}
						ReadOnlyMemory<byte>? encryptedContent = this._envelopedData.EncryptedContentInfo.EncryptedContent;
						if (encryptedContent == null)
						{
							exception = null;
							return new ContentInfo(new Oid(this._envelopedData.EncryptedContentInfo.ContentType), Array.Empty<byte>());
						}
						array3 = this.DecryptContent(encryptedContent.Value, array, out exception);
					}
					finally
					{
						if (array != null)
						{
							Array.Clear(array, 0, array.Length);
						}
					}
					array2 = null;
					if (exception != null)
					{
						return null;
					}
					if (this._envelopedData.EncryptedContentInfo.ContentType == "1.2.840.113549.1.7.1")
					{
						byte[] array4 = null;
						try
						{
							AsnReader asnReader = new AsnReader(array3, AsnEncodingRules.BER);
							ReadOnlyMemory<byte> readOnlyMemory;
							if (asnReader.TryGetPrimitiveOctetStringBytes(out readOnlyMemory))
							{
								array3 = readOnlyMemory.ToArray();
							}
							else
							{
								array4 = ArrayPool<byte>.Shared.Rent(array3.Length);
								int length;
								if (asnReader.TryCopyOctetStringBytes(array4, out length))
								{
									Span<byte> span = new Span<byte>(array4, 0, length);
									array3 = span.ToArray();
									span.Clear();
								}
							}
							goto IL_17A;
						}
						catch (CryptographicException)
						{
							goto IL_17A;
						}
						finally
						{
							if (array4 != null)
							{
								ArrayPool<byte>.Shared.Return(array4, false);
							}
						}
					}
					array3 = ManagedPkcsPal.ManagedDecryptorPal.GetAsnSequenceWithContentNoValidation(array3);
					IL_17A:
					exception = null;
					return new ContentInfo(new Oid(this._envelopedData.EncryptedContentInfo.ContentType), array3);
				}
				exception = new CryptographicException("The recipient type '{0}' is not supported for encryption or decryption on this platform.", recipientInfo.Type.ToString());
				return null;
			}

			// Token: 0x0600071E RID: 1822 RVA: 0x0001DA48 File Offset: 0x0001BC48
			private static byte[] GetAsnSequenceWithContentNoValidation(ReadOnlySpan<byte> content)
			{
				byte[] result;
				using (AsnWriter asnWriter = new AsnWriter(AsnEncodingRules.BER))
				{
					asnWriter.WriteOctetString(content);
					byte[] array = asnWriter.Encode();
					array[0] = 48;
					result = array;
				}
				return result;
			}

			// Token: 0x0600071F RID: 1823 RVA: 0x0001DA8C File Offset: 0x0001BC8C
			private byte[] DecryptContent(ReadOnlyMemory<byte> encryptedContent, byte[] cek, out Exception exception)
			{
				exception = null;
				int length = encryptedContent.Length;
				byte[] array = ArrayPool<byte>.Shared.Rent(length);
				byte[] result;
				try
				{
					encryptedContent.CopyTo(array);
					using (SymmetricAlgorithm symmetricAlgorithm = ManagedPkcsPal.OpenAlgorithm(this._envelopedData.EncryptedContentInfo.ContentEncryptionAlgorithm))
					{
						using (ICryptoTransform cryptoTransform = symmetricAlgorithm.CreateDecryptor(cek, symmetricAlgorithm.IV))
						{
							result = cryptoTransform.OneShot(array, 0, length);
						}
					}
				}
				catch (CryptographicException ex)
				{
					exception = ex;
					result = null;
				}
				finally
				{
					Array.Clear(array, 0, length);
					ArrayPool<byte>.Shared.Return(array, false);
					array = null;
				}
				return result;
			}

			// Token: 0x06000720 RID: 1824 RVA: 0x0000C9F1 File Offset: 0x0000ABF1
			public override void Dispose()
			{
			}

			// Token: 0x0400046A RID: 1130
			private byte[] _dataCopy;

			// Token: 0x0400046B RID: 1131
			private EnvelopedDataAsn _envelopedData;
		}

		// Token: 0x02000116 RID: 278
		private sealed class ManagedKeyAgreePal : KeyAgreeRecipientInfoPal
		{
			// Token: 0x06000721 RID: 1825 RVA: 0x0001DB5C File Offset: 0x0001BD5C
			internal ManagedKeyAgreePal(KeyAgreeRecipientInfoAsn asn, int index)
			{
				this._asn = asn;
				this._index = index;
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001DB72 File Offset: 0x0001BD72
			public override byte[] EncryptedKey
			{
				get
				{
					return this._asn.RecipientEncryptedKeys[this._index].EncryptedKey.ToArray();
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x06000723 RID: 1827 RVA: 0x0001DB94 File Offset: 0x0001BD94
			public override AlgorithmIdentifier KeyEncryptionAlgorithm
			{
				get
				{
					return this._asn.KeyEncryptionAlgorithm.ToPresentationObject();
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
			public override SubjectIdentifier RecipientIdentifier
			{
				get
				{
					IssuerAndSerialNumberAsn? issuerAndSerialNumber = this._asn.RecipientEncryptedKeys[this._index].Rid.IssuerAndSerialNumber;
					RecipientKeyIdentifier rkeyId = this._asn.RecipientEncryptedKeys[this._index].Rid.RKeyId;
					return new SubjectIdentifier(issuerAndSerialNumber, (rkeyId != null) ? new ReadOnlyMemory<byte>?(rkeyId.SubjectKeyIdentifier) : null);
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001DC13 File Offset: 0x0001BE13
			public override int Version
			{
				get
				{
					return this._asn.Version;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001DC20 File Offset: 0x0001BE20
			public override DateTime Date
			{
				get
				{
					KeyAgreeRecipientIdentifierAsn rid = this._asn.RecipientEncryptedKeys[this._index].Rid;
					if (rid.RKeyId == null)
					{
						throw new InvalidOperationException("The Date property is not available for none KID key agree recipient.");
					}
					if (rid.RKeyId.Date == null)
					{
						return DateTime.FromFileTimeUtc(0L);
					}
					return rid.RKeyId.Date.Value.LocalDateTime;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001DC8E File Offset: 0x0001BE8E
			public override SubjectIdentifierOrKey OriginatorIdentifierOrKey
			{
				get
				{
					return this._asn.Originator.ToSubjectIdentifierOrKey();
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001DCA0 File Offset: 0x0001BEA0
			public override CryptographicAttributeObject OtherKeyAttribute
			{
				get
				{
					KeyAgreeRecipientIdentifierAsn rid = this._asn.RecipientEncryptedKeys[this._index].Rid;
					if (rid.RKeyId == null)
					{
						throw new InvalidOperationException("The Date property is not available for none KID key agree recipient.");
					}
					if (rid.RKeyId.Other == null)
					{
						return null;
					}
					Oid oid = new Oid(rid.RKeyId.Other.Value.KeyAttrId);
					byte[] encodedData = Array.Empty<byte>();
					OtherKeyAttributeAsn value = rid.RKeyId.Other.Value;
					if (value.KeyAttr != null)
					{
						value = rid.RKeyId.Other.Value;
						encodedData = value.KeyAttr.Value.ToArray();
					}
					AsnEncodedDataCollection values = new AsnEncodedDataCollection(new Pkcs9AttributeObject(oid, encodedData));
					return new CryptographicAttributeObject(oid, values);
				}
			}

			// Token: 0x0400046C RID: 1132
			private readonly KeyAgreeRecipientInfoAsn _asn;

			// Token: 0x0400046D RID: 1133
			private readonly int _index;
		}

		// Token: 0x02000117 RID: 279
		private sealed class ManagedKeyTransPal : KeyTransRecipientInfoPal
		{
			// Token: 0x06000729 RID: 1833 RVA: 0x0001DD69 File Offset: 0x0001BF69
			internal ManagedKeyTransPal(KeyTransRecipientInfoAsn asn)
			{
				this._asn = asn;
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001DD78 File Offset: 0x0001BF78
			public override byte[] EncryptedKey
			{
				get
				{
					return this._asn.EncryptedKey.ToArray();
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x0600072B RID: 1835 RVA: 0x0001DD8A File Offset: 0x0001BF8A
			public override AlgorithmIdentifier KeyEncryptionAlgorithm
			{
				get
				{
					return this._asn.KeyEncryptionAlgorithm.ToPresentationObject();
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001DD9C File Offset: 0x0001BF9C
			public override SubjectIdentifier RecipientIdentifier
			{
				get
				{
					return new SubjectIdentifier(this._asn.Rid.IssuerAndSerialNumber, this._asn.Rid.SubjectKeyIdentifier);
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x0600072D RID: 1837 RVA: 0x0001DDC3 File Offset: 0x0001BFC3
			public override int Version
			{
				get
				{
					return this._asn.Version;
				}
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x0001DDD0 File Offset: 0x0001BFD0
			internal byte[] DecryptCek(X509Certificate2 cert, out Exception exception)
			{
				ReadOnlyMemory<byte>? parameters = this._asn.KeyEncryptionAlgorithm.Parameters;
				string value = this._asn.KeyEncryptionAlgorithm.Algorithm.Value;
				RSAEncryptionPadding padding;
				if (!(value == "1.2.840.113549.1.1.1"))
				{
					if (!(value == "1.2.840.113549.1.1.7"))
					{
						exception = new CryptographicException("Unknown algorithm '{0}'.", this._asn.KeyEncryptionAlgorithm.Algorithm.Value);
						return null;
					}
					if (parameters != null && !parameters.Value.Span.SequenceEqual(ManagedPkcsPal.s_rsaOaepSha1Parameters))
					{
						exception = new CryptographicException("ASN1 corrupted data.");
						return null;
					}
					padding = RSAEncryptionPadding.OaepSHA1;
				}
				else
				{
					if (parameters != null && !parameters.Value.Span.SequenceEqual(ManagedPkcsPal.s_rsaPkcsParameters))
					{
						exception = new CryptographicException("ASN1 corrupted data.");
						return null;
					}
					padding = RSAEncryptionPadding.Pkcs1;
				}
				byte[] array = null;
				int length = 0;
				byte[] result;
				try
				{
					using (RSA rsaprivateKey = cert.GetRSAPrivateKey())
					{
						if (rsaprivateKey == null)
						{
							exception = new CryptographicException("A certificate with a private key is required.");
							result = null;
						}
						else
						{
							exception = null;
							result = rsaprivateKey.Decrypt(this._asn.EncryptedKey.Span.ToArray(), padding);
						}
					}
				}
				catch (CryptographicException ex)
				{
					exception = ex;
					result = null;
				}
				finally
				{
					if (array != null)
					{
						Array.Clear(array, 0, length);
						ArrayPool<byte>.Shared.Return(array, false);
					}
				}
				return result;
			}

			// Token: 0x0400046E RID: 1134
			private readonly KeyTransRecipientInfoAsn _asn;
		}
	}
}
