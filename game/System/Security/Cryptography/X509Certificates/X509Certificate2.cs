using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Internal.Cryptography;
using Microsoft.Win32.SafeHandles;
using Mono;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X.509 certificate.</summary>
	// Token: 0x020002D0 RID: 720
	[Serializable]
	public class X509Certificate2 : X509Certificate
	{
		/// <summary>Resets the state of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		// Token: 0x06001632 RID: 5682 RVA: 0x000590C4 File Offset: 0x000572C4
		public override void Reset()
		{
			this.lazyRawData = null;
			this.lazySignatureAlgorithm = null;
			this.lazyVersion = 0;
			this.lazySubjectName = null;
			this.lazyIssuerName = null;
			this.lazyPublicKey = null;
			this.lazyPrivateKey = null;
			this.lazyExtensions = null;
			base.Reset();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class.</summary>
		// Token: 0x06001633 RID: 5683 RVA: 0x0005911F File Offset: 0x0005731F
		public X509Certificate2()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using information from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x06001634 RID: 5684 RVA: 0x00059128 File Offset: 0x00057328
		public X509Certificate2(byte[] rawData) : base(rawData)
		{
			if (rawData != null && rawData.Length != 0)
			{
				using (SafePasswordHandle safePasswordHandle = new SafePasswordHandle(null))
				{
					X509CertificateImpl impl = X509Helper.Import(rawData, safePasswordHandle, X509KeyStorageFlags.DefaultKeySet);
					base.ImportHandle(impl);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x06001635 RID: 5685 RVA: 0x00059178 File Offset: 0x00057378
		public X509Certificate2(byte[] rawData, string password) : base(rawData, password)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array and a password.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x06001636 RID: 5686 RVA: 0x00059182 File Offset: 0x00057382
		[CLSCompliant(false)]
		public X509Certificate2(byte[] rawData, SecureString password) : base(rawData, password)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x06001637 RID: 5687 RVA: 0x0005918C File Offset: 0x0005738C
		public X509Certificate2(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags) : base(rawData, password, keyStorageFlags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x06001638 RID: 5688 RVA: 0x00059197 File Offset: 0x00057397
		[CLSCompliant(false)]
		public X509Certificate2(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags) : base(rawData, password, keyStorageFlags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using an unmanaged handle.</summary>
		/// <param name="handle">A pointer to a certificate context in unmanaged code. The C structure is called <see langword="PCCERT_CONTEXT" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x06001639 RID: 5689 RVA: 0x000591A2 File Offset: 0x000573A2
		public X509Certificate2(IntPtr handle) : base(handle)
		{
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000591AB File Offset: 0x000573AB
		internal X509Certificate2(X509Certificate2Impl impl) : base(impl)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x0600163B RID: 5691 RVA: 0x000591B4 File Offset: 0x000573B4
		public X509Certificate2(string fileName) : base(fileName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name and a password used to access the certificate.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x0600163C RID: 5692 RVA: 0x000591BD File Offset: 0x000573BD
		public X509Certificate2(string fileName, string password) : base(fileName, password)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name and a password.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x0600163D RID: 5693 RVA: 0x000591C7 File Offset: 0x000573C7
		public X509Certificate2(string fileName, SecureString password) : base(fileName, password)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name, a password used to access the certificate, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x0600163E RID: 5694 RVA: 0x000591D1 File Offset: 0x000573D1
		public X509Certificate2(string fileName, string password, X509KeyStorageFlags keyStorageFlags) : base(fileName, password, keyStorageFlags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using a certificate file name, a password, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x0600163F RID: 5695 RVA: 0x000591DC File Offset: 0x000573DC
		public X509Certificate2(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags) : base(fileName, password, keyStorageFlags)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error with the certificate occurs. For example:  
		///
		/// The certificate file does not exist.  
		///
		/// The certificate is invalid.  
		///
		/// The certificate's password is incorrect.</exception>
		// Token: 0x06001640 RID: 5696 RVA: 0x000591E7 File Offset: 0x000573E7
		public X509Certificate2(X509Certificate certificate) : base(certificate)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class using the specified serialization and stream context information.</summary>
		/// <param name="info">The serialization information required to deserialize the new <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" />.</param>
		/// <param name="context">Contextual information about the source of the stream to be deserialized.</param>
		// Token: 0x06001641 RID: 5697 RVA: 0x000591F0 File Offset: 0x000573F0
		protected X509Certificate2(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Gets or sets a value indicating that an X.509 certificate is archived.</summary>
		/// <returns>
		///   <see langword="true" /> if the certificate is archived, <see langword="false" /> if the certificate is not archived.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x000591FF File Offset: 0x000573FF
		// (set) Token: 0x06001643 RID: 5699 RVA: 0x00059212 File Offset: 0x00057412
		public bool Archived
		{
			get
			{
				base.ThrowIfInvalid();
				return this.Impl.Archived;
			}
			set
			{
				base.ThrowIfInvalid();
				this.Impl.Archived = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x00059228 File Offset: 0x00057428
		public X509ExtensionCollection Extensions
		{
			get
			{
				base.ThrowIfInvalid();
				X509ExtensionCollection x509ExtensionCollection = this.lazyExtensions;
				if (x509ExtensionCollection == null)
				{
					x509ExtensionCollection = new X509ExtensionCollection();
					foreach (X509Extension x509Extension in this.Impl.Extensions)
					{
						X509Extension x509Extension2 = X509Certificate2.CreateCustomExtensionIfAny(x509Extension.Oid);
						if (x509Extension2 == null)
						{
							x509ExtensionCollection.Add(x509Extension);
						}
						else
						{
							x509Extension2.CopyFrom(x509Extension);
							x509ExtensionCollection.Add(x509Extension2);
						}
					}
					this.lazyExtensions = x509ExtensionCollection;
				}
				return x509ExtensionCollection;
			}
		}

		/// <summary>Gets or sets the associated alias for a certificate.</summary>
		/// <returns>The certificate's friendly name.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x000592C0 File Offset: 0x000574C0
		// (set) Token: 0x06001646 RID: 5702 RVA: 0x000592D3 File Offset: 0x000574D3
		public string FriendlyName
		{
			get
			{
				base.ThrowIfInvalid();
				return this.Impl.FriendlyName;
			}
			set
			{
				base.ThrowIfInvalid();
				this.Impl.FriendlyName = value;
			}
		}

		/// <summary>Gets a value that indicates whether an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object contains a private key.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object contains a private key; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x000592E7 File Offset: 0x000574E7
		public bool HasPrivateKey
		{
			get
			{
				base.ThrowIfInvalid();
				return this.Impl.HasPrivateKey;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object that represents the private key associated with a certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.AsymmetricAlgorithm" /> object, which is either an RSA or DSA cryptographic service provider.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key value is not an RSA or DSA key, or the key is unreadable.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The key algorithm for this private key is not supported.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">The X.509 keys do not match.</exception>
		/// <exception cref="T:System.ArgumentException">The cryptographic service provider key is <see langword="null" />.</exception>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x000592FC File Offset: 0x000574FC
		// (set) Token: 0x06001649 RID: 5705 RVA: 0x00011F54 File Offset: 0x00010154
		public AsymmetricAlgorithm PrivateKey
		{
			get
			{
				base.ThrowIfInvalid();
				if (!this.HasPrivateKey)
				{
					return null;
				}
				if (this.lazyPrivateKey == null)
				{
					string keyAlgorithm = this.GetKeyAlgorithm();
					if (!(keyAlgorithm == "1.2.840.113549.1.1.1"))
					{
						if (!(keyAlgorithm == "1.2.840.10040.4.1"))
						{
							throw new NotSupportedException("The certificate key algorithm is not supported.");
						}
						this.lazyPrivateKey = this.Impl.GetDSAPrivateKey();
					}
					else
					{
						this.lazyPrivateKey = this.Impl.GetRSAPrivateKey();
					}
				}
				return this.lazyPrivateKey;
			}
			set
			{
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Gets the distinguished name of the certificate issuer.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that contains the name of the certificate issuer.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00059384 File Offset: 0x00057584
		public X500DistinguishedName IssuerName
		{
			get
			{
				base.ThrowIfInvalid();
				X500DistinguishedName x500DistinguishedName = this.lazyIssuerName;
				if (x500DistinguishedName == null)
				{
					x500DistinguishedName = (this.lazyIssuerName = this.Impl.IssuerName);
				}
				return x500DistinguishedName;
			}
		}

		/// <summary>Gets the date in local time after which a certificate is no longer valid.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the expiration date for the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x000593BB File Offset: 0x000575BB
		public DateTime NotAfter
		{
			get
			{
				return base.GetNotAfter();
			}
		}

		/// <summary>Gets the date in local time on which a certificate becomes valid.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that represents the effective date of the certificate.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x000593C3 File Offset: 0x000575C3
		public DateTime NotBefore
		{
			get
			{
				return base.GetNotBefore();
			}
		}

		/// <summary>Gets a <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.PublicKey" /> object associated with a certificate.</summary>
		/// <returns>A <see cref="P:System.Security.Cryptography.X509Certificates.X509Certificate2.PublicKey" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The key value is not an RSA or DSA key, or the key is unreadable.</exception>
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x000593CC File Offset: 0x000575CC
		public PublicKey PublicKey
		{
			get
			{
				base.ThrowIfInvalid();
				PublicKey publicKey = this.lazyPublicKey;
				if (publicKey == null)
				{
					string keyAlgorithm = this.GetKeyAlgorithm();
					byte[] keyAlgorithmParameters = this.GetKeyAlgorithmParameters();
					byte[] publicKey2 = this.GetPublicKey();
					Oid oid = new Oid(keyAlgorithm);
					publicKey = (this.lazyPublicKey = new PublicKey(oid, new AsnEncodedData(oid, keyAlgorithmParameters), new AsnEncodedData(oid, publicKey2)));
				}
				return publicKey;
			}
		}

		/// <summary>Gets the raw data of a certificate.</summary>
		/// <returns>The raw data of the certificate as a byte array.</returns>
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00059428 File Offset: 0x00057628
		public byte[] RawData
		{
			get
			{
				base.ThrowIfInvalid();
				byte[] array = this.lazyRawData;
				if (array == null)
				{
					array = (this.lazyRawData = this.Impl.RawData);
				}
				return array.CloneByteArray();
			}
		}

		/// <summary>Gets the serial number of a certificate as a big-endian hexadecimal string.</summary>
		/// <returns>The serial number of the certificate as a big-endian hexadecimal string.</returns>
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00059464 File Offset: 0x00057664
		public string SerialNumber
		{
			get
			{
				return this.GetSerialNumberString();
			}
		}

		/// <summary>Gets the algorithm used to create the signature of a certificate.</summary>
		/// <returns>The object identifier of the signature algorithm.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x0005946C File Offset: 0x0005766C
		public Oid SignatureAlgorithm
		{
			get
			{
				base.ThrowIfInvalid();
				Oid oid = this.lazySignatureAlgorithm;
				if (oid == null)
				{
					string signatureAlgorithm = this.Impl.SignatureAlgorithm;
					oid = (this.lazySignatureAlgorithm = Oid.FromOidValue(signatureAlgorithm, OidGroup.SignatureAlgorithm));
				}
				return oid;
			}
		}

		/// <summary>Gets the subject distinguished name from a certificate.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X500DistinguishedName" /> object that represents the name of the certificate subject.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate context is invalid.</exception>
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x000594AC File Offset: 0x000576AC
		public X500DistinguishedName SubjectName
		{
			get
			{
				base.ThrowIfInvalid();
				X500DistinguishedName x500DistinguishedName = this.lazySubjectName;
				if (x500DistinguishedName == null)
				{
					x500DistinguishedName = (this.lazySubjectName = this.Impl.SubjectName);
				}
				return x500DistinguishedName;
			}
		}

		/// <summary>Gets the thumbprint of a certificate.</summary>
		/// <returns>The thumbprint of the certificate.</returns>
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x000594E3 File Offset: 0x000576E3
		public string Thumbprint
		{
			get
			{
				return this.GetCertHash().ToHexStringUpper();
			}
		}

		/// <summary>Gets the X.509 format version of a certificate.</summary>
		/// <returns>The certificate format.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x000594F0 File Offset: 0x000576F0
		public int Version
		{
			get
			{
				base.ThrowIfInvalid();
				int num = this.lazyVersion;
				if (num == 0)
				{
					num = (this.lazyVersion = this.Impl.Version);
				}
				return num;
			}
		}

		/// <summary>Indicates the type of certificate contained in a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rawData" /> has a zero length or is <see langword="null" />.</exception>
		// Token: 0x06001654 RID: 5716 RVA: 0x00059527 File Offset: 0x00057727
		public static X509ContentType GetCertContentType(byte[] rawData)
		{
			if (rawData == null || rawData.Length == 0)
			{
				throw new ArgumentException("Array cannot be empty or null.", "rawData");
			}
			return X509Pal.Instance.GetCertContentType(rawData);
		}

		/// <summary>Indicates the type of certificate contained in a file.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is <see langword="null" />.</exception>
		// Token: 0x06001655 RID: 5717 RVA: 0x0005954B File Offset: 0x0005774B
		public static X509ContentType GetCertContentType(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			Path.GetFullPath(fileName);
			return X509Pal.Instance.GetCertContentType(fileName);
		}

		/// <summary>Gets the subject and issuer names from a certificate.</summary>
		/// <param name="nameType">The <see cref="T:System.Security.Cryptography.X509Certificates.X509NameType" /> value for the subject.</param>
		/// <param name="forIssuer">
		///   <see langword="true" /> to include the issuer name; otherwise, <see langword="false" />.</param>
		/// <returns>The name of the certificate.</returns>
		// Token: 0x06001656 RID: 5718 RVA: 0x0005956D File Offset: 0x0005776D
		public string GetNameInfo(X509NameType nameType, bool forIssuer)
		{
			return this.Impl.GetNameInfo(nameType, forIssuer);
		}

		/// <summary>Displays an X.509 certificate in text format.</summary>
		/// <returns>The certificate information.</returns>
		// Token: 0x06001657 RID: 5719 RVA: 0x0005957C File Offset: 0x0005777C
		public override string ToString()
		{
			return base.ToString(true);
		}

		/// <summary>Displays an X.509 certificate in text format.</summary>
		/// <param name="verbose">
		///   <see langword="true" /> to display the public key, private key, extensions, and so forth; <see langword="false" /> to display information that is similar to the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> class, including thumbprint, serial number, subject and issuer names, and so on.</param>
		/// <returns>The certificate information.</returns>
		// Token: 0x06001658 RID: 5720 RVA: 0x00059588 File Offset: 0x00057788
		public override string ToString(bool verbose)
		{
			if (!verbose || !base.IsValid)
			{
				return this.ToString();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("[Version]");
			stringBuilder.Append("  V");
			stringBuilder.Append(this.Version);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Subject]");
			stringBuilder.Append("  ");
			stringBuilder.Append(this.SubjectName.Name);
			string nameInfo = this.GetNameInfo(X509NameType.SimpleName, false);
			if (nameInfo.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Simple Name: ");
				stringBuilder.Append(nameInfo);
			}
			string nameInfo2 = this.GetNameInfo(X509NameType.EmailName, false);
			if (nameInfo2.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Email Name: ");
				stringBuilder.Append(nameInfo2);
			}
			string nameInfo3 = this.GetNameInfo(X509NameType.UpnName, false);
			if (nameInfo3.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("UPN Name: ");
				stringBuilder.Append(nameInfo3);
			}
			string nameInfo4 = this.GetNameInfo(X509NameType.DnsName, false);
			if (nameInfo4.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("DNS Name: ");
				stringBuilder.Append(nameInfo4);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Issuer]");
			stringBuilder.Append("  ");
			stringBuilder.Append(this.IssuerName.Name);
			nameInfo = this.GetNameInfo(X509NameType.SimpleName, true);
			if (nameInfo.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Simple Name: ");
				stringBuilder.Append(nameInfo);
			}
			nameInfo2 = this.GetNameInfo(X509NameType.EmailName, true);
			if (nameInfo2.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Email Name: ");
				stringBuilder.Append(nameInfo2);
			}
			nameInfo3 = this.GetNameInfo(X509NameType.UpnName, true);
			if (nameInfo3.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("UPN Name: ");
				stringBuilder.Append(nameInfo3);
			}
			nameInfo4 = this.GetNameInfo(X509NameType.DnsName, true);
			if (nameInfo4.Length > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("DNS Name: ");
				stringBuilder.Append(nameInfo4);
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Serial Number]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(this.SerialNumber);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Not Before]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(X509Certificate.FormatDate(this.NotBefore));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Not After]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(X509Certificate.FormatDate(this.NotAfter));
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Thumbprint]");
			stringBuilder.Append("  ");
			stringBuilder.AppendLine(this.Thumbprint);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("[Signature Algorithm]");
			stringBuilder.Append("  ");
			stringBuilder.Append(this.SignatureAlgorithm.FriendlyName);
			stringBuilder.Append('(');
			stringBuilder.Append(this.SignatureAlgorithm.Value);
			stringBuilder.AppendLine(")");
			stringBuilder.AppendLine();
			stringBuilder.Append("[Public Key]");
			try
			{
				PublicKey publicKey = this.PublicKey;
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Algorithm: ");
				stringBuilder.Append(publicKey.Oid.FriendlyName);
				try
				{
					stringBuilder.AppendLine();
					stringBuilder.Append("  ");
					stringBuilder.Append("Length: ");
					using (RSA rsapublicKey = this.GetRSAPublicKey())
					{
						if (rsapublicKey != null)
						{
							stringBuilder.Append(rsapublicKey.KeySize);
						}
					}
				}
				catch (NotSupportedException)
				{
				}
				stringBuilder.AppendLine();
				stringBuilder.Append("  ");
				stringBuilder.Append("Key Blob: ");
				stringBuilder.AppendLine(publicKey.EncodedKeyValue.Format(true));
				stringBuilder.Append("  ");
				stringBuilder.Append("Parameters: ");
				stringBuilder.Append(publicKey.EncodedParameters.Format(true));
			}
			catch (CryptographicException)
			{
			}
			this.Impl.AppendPrivateKeyInfo(stringBuilder);
			X509ExtensionCollection extensions = this.Extensions;
			if (extensions.Count > 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendLine();
				stringBuilder.Append("[Extensions]");
				foreach (X509Extension x509Extension in extensions)
				{
					try
					{
						stringBuilder.AppendLine();
						stringBuilder.Append("* ");
						stringBuilder.Append(x509Extension.Oid.FriendlyName);
						stringBuilder.Append('(');
						stringBuilder.Append(x509Extension.Oid.Value);
						stringBuilder.Append("):");
						stringBuilder.AppendLine();
						stringBuilder.Append("  ");
						stringBuilder.Append(x509Extension.Format(true));
					}
					catch (CryptographicException)
					{
					}
				}
			}
			stringBuilder.AppendLine();
			return stringBuilder.ToString();
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with data from a byte array.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		// Token: 0x06001659 RID: 5721 RVA: 0x00059B38 File Offset: 0x00057D38
		public override void Import(byte[] rawData)
		{
			base.Import(rawData);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object using data from a byte array, a password, and flags for determining how to import the private key.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		// Token: 0x0600165A RID: 5722 RVA: 0x00059B41 File Offset: 0x00057D41
		public override void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			base.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object using data from a byte array, a password, and a key storage flag.</summary>
		/// <param name="rawData">A byte array that contains data from an X.509 certificate.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		// Token: 0x0600165B RID: 5723 RVA: 0x00059B4C File Offset: 0x00057D4C
		[CLSCompliant(false)]
		public override void Import(byte[] rawData, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			base.Import(rawData, password, keyStorageFlags);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file.</summary>
		/// <param name="fileName">The name of a certificate.</param>
		// Token: 0x0600165C RID: 5724 RVA: 0x00059B57 File Offset: 0x00057D57
		public override void Import(string fileName)
		{
			base.Import(fileName);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file, a password, and a <see cref="T:System.Security.Cryptography.X509Certificates.X509KeyStorageFlags" /> value.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		// Token: 0x0600165D RID: 5725 RVA: 0x00059B60 File Offset: 0x00057D60
		public override void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			base.Import(fileName, password, keyStorageFlags);
		}

		/// <summary>Populates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object with information from a certificate file, a password, and a key storage flag.</summary>
		/// <param name="fileName">The name of a certificate file.</param>
		/// <param name="password">The password required to access the X.509 certificate data.</param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control where and how to import the certificate.</param>
		// Token: 0x0600165E RID: 5726 RVA: 0x00059B6B File Offset: 0x00057D6B
		[CLSCompliant(false)]
		public override void Import(string fileName, SecureString password, X509KeyStorageFlags keyStorageFlags)
		{
			base.Import(fileName, password, keyStorageFlags);
		}

		/// <summary>Performs a X.509 chain validation using basic validation policy.</summary>
		/// <returns>
		///   <see langword="true" /> if the validation succeeds; <see langword="false" /> if the validation fails.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable.</exception>
		// Token: 0x0600165F RID: 5727 RVA: 0x00059B76 File Offset: 0x00057D76
		public bool Verify()
		{
			return this.Impl.Verify(this);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00059B84 File Offset: 0x00057D84
		private static X509Extension CreateCustomExtensionIfAny(Oid oid)
		{
			string value = oid.Value;
			if (!(value == "2.5.29.10"))
			{
				if (value == "2.5.29.19")
				{
					return new X509BasicConstraintsExtension();
				}
				if (value == "2.5.29.15")
				{
					return new X509KeyUsageExtension();
				}
				if (value == "2.5.29.37")
				{
					return new X509EnhancedKeyUsageExtension();
				}
				if (!(value == "2.5.29.14"))
				{
					return null;
				}
				return new X509SubjectKeyIdentifierExtension();
			}
			else
			{
				if (!X509Pal.Instance.SupportsLegacyBasicConstraintsExtension)
				{
					return null;
				}
				return new X509BasicConstraintsExtension();
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001661 RID: 5729 RVA: 0x00059C08 File Offset: 0x00057E08
		internal new X509Certificate2Impl Impl
		{
			get
			{
				X509Certificate2Impl x509Certificate2Impl = base.Impl as X509Certificate2Impl;
				X509Helper.ThrowIfContextInvalid(x509Certificate2Impl);
				return x509Certificate2Impl;
			}
		}

		// Token: 0x04000CEF RID: 3311
		private volatile byte[] lazyRawData;

		// Token: 0x04000CF0 RID: 3312
		private volatile Oid lazySignatureAlgorithm;

		// Token: 0x04000CF1 RID: 3313
		private volatile int lazyVersion;

		// Token: 0x04000CF2 RID: 3314
		private volatile X500DistinguishedName lazySubjectName;

		// Token: 0x04000CF3 RID: 3315
		private volatile X500DistinguishedName lazyIssuerName;

		// Token: 0x04000CF4 RID: 3316
		private volatile PublicKey lazyPublicKey;

		// Token: 0x04000CF5 RID: 3317
		private volatile AsymmetricAlgorithm lazyPrivateKey;

		// Token: 0x04000CF6 RID: 3318
		private volatile X509ExtensionCollection lazyExtensions;
	}
}
