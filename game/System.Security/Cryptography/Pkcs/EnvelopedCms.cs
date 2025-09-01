using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Internal.Cryptography;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class represents a CMS/PKCS #7 structure for enveloped data.</summary>
	// Token: 0x02000075 RID: 117
	public sealed class EnvelopedCms
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class.</summary>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060003CF RID: 975 RVA: 0x00012294 File Offset: 0x00010494
		public EnvelopedCms() : this(new ContentInfo(Array.Empty<byte>()))
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified content information as the inner content type.</summary>
		/// <param name="contentInfo">An instance of the <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo" /> class that represents the content and its type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060003D0 RID: 976 RVA: 0x000122A6 File Offset: 0x000104A6
		public EnvelopedCms(ContentInfo contentInfo) : this(contentInfo, new AlgorithmIdentifier(Oid.FromOidValue("1.2.840.113549.3.7", OidGroup.EncryptionAlgorithm)))
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified content information and encryption algorithm. The specified content information is to be used as the inner content type.</summary>
		/// <param name="contentInfo">A  <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that specifies the encryption algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060003D1 RID: 977 RVA: 0x000122C0 File Offset: 0x000104C0
		public EnvelopedCms(ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm)
		{
			if (contentInfo == null)
			{
				throw new ArgumentNullException("contentInfo");
			}
			if (encryptionAlgorithm == null)
			{
				throw new ArgumentNullException("encryptionAlgorithm");
			}
			this.Version = 0;
			this.ContentInfo = contentInfo;
			this.ContentEncryptionAlgorithm = encryptionAlgorithm;
			this.Certificates = new X509Certificate2Collection();
			this.UnprotectedAttributes = new CryptographicAttributeObjectCollection();
			this._decryptorPal = null;
			this._lastCall = EnvelopedCms.LastCall.Ctor;
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Version" /> property retrieves the version of the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>An int value that represents the version of the enveloped CMS/PKCS #7 message.</returns>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00012328 File Offset: 0x00010528
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00012330 File Offset: 0x00010530
		public int Version
		{
			[CompilerGenerated]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Version>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentInfo" /> property retrieves the inner content information for the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the inner content information from the enveloped CMS/PKCS #7 message.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00012339 File Offset: 0x00010539
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00012341 File Offset: 0x00010541
		public ContentInfo ContentInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<ContentInfo>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ContentInfo>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.ContentEncryptionAlgorithm" /> property retrieves the identifier of the algorithm used to encrypt the content.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that represents the algorithm identifier.</returns>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0001234A File Offset: 0x0001054A
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00012352 File Offset: 0x00010552
		public AlgorithmIdentifier ContentEncryptionAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<ContentEncryptionAlgorithm>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ContentEncryptionAlgorithm>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.Certificates" /> property retrieves the set of certificates associated with the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents the X.509 certificates used with the enveloped CMS/PKCS #7 message. If no certificates exist, the property value is an empty collection.</returns>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001235B File Offset: 0x0001055B
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00012363 File Offset: 0x00010563
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

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.UnprotectedAttributes" /> property retrieves the unprotected (unencrypted) attributes associated with the enveloped CMS/PKCS #7 message. Unprotected attributes are not encrypted, and so do not have data confidentiality within an <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection that represents the unprotected attributes. If no unprotected attributes exist, the property value is an empty collection.</returns>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0001236C File Offset: 0x0001056C
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00012374 File Offset: 0x00010574
		public CryptographicAttributeObjectCollection UnprotectedAttributes
		{
			[CompilerGenerated]
			get
			{
				return this.<UnprotectedAttributes>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<UnprotectedAttributes>k__BackingField = value;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.EnvelopedCms.RecipientInfos" /> property retrieves the recipient information associated with the enveloped CMS/PKCS #7 message.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection that represents the recipient information. If no recipients exist, the property value is an empty collection.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00012380 File Offset: 0x00010580
		public RecipientInfoCollection RecipientInfos
		{
			get
			{
				switch (this._lastCall)
				{
				case EnvelopedCms.LastCall.Ctor:
					return new RecipientInfoCollection();
				case EnvelopedCms.LastCall.Encrypt:
					throw PkcsPal.Instance.CreateRecipientInfosAfterEncryptException();
				case EnvelopedCms.LastCall.Decode:
				case EnvelopedCms.LastCall.Decrypt:
					return this._decryptorPal.RecipientInfos;
				default:
					throw new InvalidOperationException();
				}
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipient)" /> method encrypts the contents of the CMS/PKCS #7 message by using the specified recipient information.</summary>
		/// <param name="recipient">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the recipient information.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x060003DD RID: 989 RVA: 0x000123D0 File Offset: 0x000105D0
		public void Encrypt(CmsRecipient recipient)
		{
			if (recipient == null)
			{
				throw new ArgumentNullException("recipient");
			}
			this.Encrypt(new CmsRecipientCollection(recipient));
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt(System.Security.Cryptography.Pkcs.CmsRecipientCollection)" /> method encrypts the contents of the CMS/PKCS #7 message by using the information for the specified list of recipients. The message is encrypted by using a message encryption key with a symmetric encryption algorithm such as triple DES. The message encryption key is then encrypted with the public key of each recipient.</summary>
		/// <param name="recipients">A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection that represents the information for the list of recipients.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x060003DE RID: 990 RVA: 0x000123EC File Offset: 0x000105EC
		public void Encrypt(CmsRecipientCollection recipients)
		{
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			if (recipients.Count == 0)
			{
				throw new PlatformNotSupportedException("The recipients collection is empty. You must specify at least one recipient. This platform does not implement the certificate picker UI.");
			}
			if (this._decryptorPal != null)
			{
				this._decryptorPal.Dispose();
				this._decryptorPal = null;
			}
			this._encodedMessage = PkcsPal.Instance.Encrypt(recipients, this.ContentInfo, this.ContentEncryptionAlgorithm, this.Certificates, this.UnprotectedAttributes);
			this._lastCall = EnvelopedCms.LastCall.Encrypt;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encode" /> method encodes the contents of the enveloped CMS/PKCS #7 message and returns it as an array of byte values. Encryption must be done before encoding.</summary>
		/// <returns>If the method succeeds, the method returns an array of byte values that represent the encoded information.  
		///  If the method fails, it throws an exception.</returns>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060003DF RID: 991 RVA: 0x00012464 File Offset: 0x00010664
		public byte[] Encode()
		{
			if (this._encodedMessage == null)
			{
				throw new InvalidOperationException("The CMS message is not encrypted.");
			}
			return this._encodedMessage.CloneByteArray();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decode(System.Byte[])" /> method decodes the specified enveloped CMS/PKCS #7 message and resets all member variables in the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> object.</summary>
		/// <param name="encodedMessage">An array of byte values that represent the information to be decoded.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060003E0 RID: 992 RVA: 0x00012484 File Offset: 0x00010684
		public void Decode(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("encodedMessage");
			}
			if (this._decryptorPal != null)
			{
				this._decryptorPal.Dispose();
				this._decryptorPal = null;
			}
			int version;
			ContentInfo contentInfo;
			AlgorithmIdentifier contentEncryptionAlgorithm;
			X509Certificate2Collection certificates;
			CryptographicAttributeObjectCollection unprotectedAttributes;
			this._decryptorPal = PkcsPal.Instance.Decode(encodedMessage, out version, out contentInfo, out contentEncryptionAlgorithm, out certificates, out unprotectedAttributes);
			this.Version = version;
			this.ContentInfo = contentInfo;
			this.ContentEncryptionAlgorithm = contentEncryptionAlgorithm;
			this.Certificates = certificates;
			this.UnprotectedAttributes = unprotectedAttributes;
			this._encodedMessage = contentInfo.Content.CloneByteArray();
			this._lastCall = EnvelopedCms.LastCall.Decode;
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt" /> method searches the current user and computer My stores for the appropriate certificate and private key.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060003E1 RID: 993 RVA: 0x00012510 File Offset: 0x00010710
		public void Decrypt()
		{
			this.DecryptContent(this.RecipientInfos, null);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information.</summary>
		/// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the recipient information that identifies the certificate associated with the private key to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060003E2 RID: 994 RVA: 0x0001251F File Offset: 0x0001071F
		public void Decrypt(RecipientInfo recipientInfo)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			this.DecryptContent(new RecipientInfoCollection(recipientInfo), null);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the private key associated with the certificate identified by the specified recipient information and by using the specified certificate collection.  The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.</summary>
		/// <param name="recipientInfo">A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the recipient information to use for the decryption.</param>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.Pkcs.RecipientInfo,System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060003E3 RID: 995 RVA: 0x0001253C File Offset: 0x0001073C
		public void Decrypt(RecipientInfo recipientInfo, X509Certificate2Collection extraStore)
		{
			if (recipientInfo == null)
			{
				throw new ArgumentNullException("recipientInfo");
			}
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			this.DecryptContent(new RecipientInfoCollection(recipientInfo), extraStore);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method decrypts the contents of the decoded enveloped CMS/PKCS #7 message by using the specified certificate collection. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches the specified certificate collection and the My certificate store for the proper certificate to use for the decryption.</summary>
		/// <param name="extraStore">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> collection that represents additional certificates to use for the decryption. The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Decrypt(System.Security.Cryptography.X509Certificates.X509Certificate2Collection)" /> method searches this certificate collection and the My certificate store for the proper certificate to use for the decryption.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		/// <exception cref="T:System.InvalidOperationException">A method call was invalid for the object's current state.</exception>
		// Token: 0x060003E4 RID: 996 RVA: 0x00012567 File Offset: 0x00010767
		public void Decrypt(X509Certificate2Collection extraStore)
		{
			if (extraStore == null)
			{
				throw new ArgumentNullException("extraStore");
			}
			this.DecryptContent(this.RecipientInfos, extraStore);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00012584 File Offset: 0x00010784
		private void DecryptContent(RecipientInfoCollection recipientInfos, X509Certificate2Collection extraStore)
		{
			switch (this._lastCall)
			{
			case EnvelopedCms.LastCall.Ctor:
				throw new InvalidOperationException("The CMS message is not encrypted.");
			case EnvelopedCms.LastCall.Encrypt:
				throw PkcsPal.Instance.CreateDecryptAfterEncryptException();
			case EnvelopedCms.LastCall.Decode:
			{
				extraStore = (extraStore ?? new X509Certificate2Collection());
				X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
				PkcsPal.Instance.AddCertsFromStoreForDecryption(x509Certificate2Collection);
				x509Certificate2Collection.AddRange(extraStore);
				X509Certificate2Collection certificates = this.Certificates;
				ContentInfo contentInfo = null;
				Exception ex = PkcsPal.Instance.CreateRecipientsNotFoundException();
				foreach (RecipientInfo recipientInfo in recipientInfos)
				{
					X509Certificate2 x509Certificate = x509Certificate2Collection.TryFindMatchingCertificate(recipientInfo.RecipientIdentifier);
					if (x509Certificate == null)
					{
						ex = PkcsPal.Instance.CreateRecipientsNotFoundException();
					}
					else
					{
						contentInfo = this._decryptorPal.TryDecrypt(recipientInfo, x509Certificate, certificates, extraStore, out ex);
						if (ex == null)
						{
							break;
						}
					}
				}
				if (ex != null)
				{
					throw ex;
				}
				this.ContentInfo = contentInfo;
				this._encodedMessage = contentInfo.Content.CloneByteArray();
				this._lastCall = EnvelopedCms.LastCall.Decrypt;
				return;
			}
			case EnvelopedCms.LastCall.Decrypt:
				throw PkcsPal.Instance.CreateDecryptTwiceException();
			default:
				throw new InvalidOperationException();
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified subject identifier type and content information. The specified content information is to be used as the inner content type.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the means of identifying the recipient.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060003E6 RID: 998 RVA: 0x0001268A File Offset: 0x0001088A
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo) : this(contentInfo)
		{
			if (recipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				this.Version = 2;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.#ctor(System.Security.Cryptography.Pkcs.SubjectIdentifierType,System.Security.Cryptography.Pkcs.ContentInfo,System.Security.Cryptography.Pkcs.AlgorithmIdentifier)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.EnvelopedCms" /> class by using the specified subject identifier type, content information, and encryption algorithm. The specified content information is to be used as the inner content type.</summary>
		/// <param name="recipientIdentifierType">A member of the <see cref="T:System.Security.Cryptography.Pkcs.SubjectIdentifierType" /> enumeration that specifies the means of identifying the recipient.</param>
		/// <param name="contentInfo">A <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object that represents the content and its type.</param>
		/// <param name="encryptionAlgorithm">An <see cref="T:System.Security.Cryptography.Pkcs.AlgorithmIdentifier" /> object that specifies the encryption algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference was passed to a method that does not accept it as a valid argument.</exception>
		// Token: 0x060003E7 RID: 999 RVA: 0x0001269E File Offset: 0x0001089E
		public EnvelopedCms(SubjectIdentifierType recipientIdentifierType, ContentInfo contentInfo, AlgorithmIdentifier encryptionAlgorithm) : this(contentInfo, encryptionAlgorithm)
		{
			if (recipientIdentifierType == SubjectIdentifierType.SubjectKeyIdentifier)
			{
				this.Version = 2;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.EnvelopedCms.Encrypt" /> method encrypts the contents of the CMS/PKCS #7 message.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A cryptographic operation could not be completed.</exception>
		// Token: 0x060003E8 RID: 1000 RVA: 0x000126B3 File Offset: 0x000108B3
		public void Encrypt()
		{
			this.Encrypt(new CmsRecipientCollection());
		}

		// Token: 0x0400027C RID: 636
		[CompilerGenerated]
		private int <Version>k__BackingField;

		// Token: 0x0400027D RID: 637
		[CompilerGenerated]
		private ContentInfo <ContentInfo>k__BackingField;

		// Token: 0x0400027E RID: 638
		[CompilerGenerated]
		private AlgorithmIdentifier <ContentEncryptionAlgorithm>k__BackingField;

		// Token: 0x0400027F RID: 639
		[CompilerGenerated]
		private X509Certificate2Collection <Certificates>k__BackingField;

		// Token: 0x04000280 RID: 640
		[CompilerGenerated]
		private CryptographicAttributeObjectCollection <UnprotectedAttributes>k__BackingField;

		// Token: 0x04000281 RID: 641
		private DecryptorPal _decryptorPal;

		// Token: 0x04000282 RID: 642
		private byte[] _encodedMessage;

		// Token: 0x04000283 RID: 643
		private EnvelopedCms.LastCall _lastCall;

		// Token: 0x02000076 RID: 118
		private enum LastCall
		{
			// Token: 0x04000285 RID: 645
			Ctor = 1,
			// Token: 0x04000286 RID: 646
			Encrypt,
			// Token: 0x04000287 RID: 647
			Decode,
			// Token: 0x04000288 RID: 648
			Decrypt
		}
	}
}
