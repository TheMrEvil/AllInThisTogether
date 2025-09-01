using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the process model for implementing XML encryption.</summary>
	// Token: 0x02000038 RID: 56
	public class EncryptedXml
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class.</summary>
		// Token: 0x0600013D RID: 317 RVA: 0x000064EC File Offset: 0x000046EC
		public EncryptedXml() : this(new XmlDocument())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class using the specified XML document.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object used to initialize the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object.</param>
		// Token: 0x0600013E RID: 318 RVA: 0x000064F9 File Offset: 0x000046F9
		public EncryptedXml(XmlDocument document) : this(document, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class using the specified XML document and evidence.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object used to initialize the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object.</param>
		/// <param name="evidence">An <see cref="T:System.Security.Policy.Evidence" /> object associated with the <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x0600013F RID: 319 RVA: 0x00006504 File Offset: 0x00004704
		public EncryptedXml(XmlDocument document, Evidence evidence)
		{
			this._document = document;
			this._evidence = evidence;
			this._xmlResolver = null;
			this._padding = PaddingMode.ISO10126;
			this._mode = CipherMode.CBC;
			this._encoding = Encoding.UTF8;
			this._keyNameMapping = new Hashtable(4);
			this._xmlDsigSearchDepth = 20;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006559 File Offset: 0x00004759
		private bool IsOverXmlDsigRecursionLimit()
		{
			return this._xmlDsigSearchDepthCounter > this.XmlDSigSearchDepth;
		}

		/// <summary>Gets or sets the XML digital signature recursion depth to prevent infinite recursion and stack overflow. This might happen if the digital signature XML contains the URI which then points back to the original XML.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000656C File Offset: 0x0000476C
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00006574 File Offset: 0x00004774
		public int XmlDSigSearchDepth
		{
			get
			{
				return this._xmlDsigSearchDepth;
			}
			set
			{
				this._xmlDsigSearchDepth = value;
			}
		}

		/// <summary>Gets or sets the evidence of the <see cref="T:System.Xml.XmlDocument" /> object from which the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object is constructed.</summary>
		/// <returns>An <see cref="T:System.Security.Policy.Evidence" /> object.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000657D File Offset: 0x0000477D
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006585 File Offset: 0x00004785
		public Evidence DocumentEvidence
		{
			get
			{
				return this._evidence;
			}
			set
			{
				this._evidence = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlResolver" /> object used by the Document Object Model (DOM) to resolve external XML references.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> object.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000658E File Offset: 0x0000478E
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00006596 File Offset: 0x00004796
		public XmlResolver Resolver
		{
			get
			{
				return this._xmlResolver;
			}
			set
			{
				this._xmlResolver = value;
			}
		}

		/// <summary>Gets or sets the padding mode used for XML encryption.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.PaddingMode" /> values that specifies the type of padding used for encryption.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000659F File Offset: 0x0000479F
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000065A7 File Offset: 0x000047A7
		public PaddingMode Padding
		{
			get
			{
				return this._padding;
			}
			set
			{
				this._padding = value;
			}
		}

		/// <summary>Gets or sets the cipher mode used for XML encryption.</summary>
		/// <returns>One of the <see cref="T:System.Security.Cryptography.CipherMode" /> values.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000065B0 File Offset: 0x000047B0
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000065B8 File Offset: 0x000047B8
		public CipherMode Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}

		/// <summary>Gets or sets the encoding used for XML encryption.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> object.</returns>
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000065C1 File Offset: 0x000047C1
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000065C9 File Offset: 0x000047C9
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
			}
		}

		/// <summary>Gets or sets the recipient of the encrypted key information.</summary>
		/// <returns>The recipient of the encrypted key information.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000065D2 File Offset: 0x000047D2
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000065ED File Offset: 0x000047ED
		public string Recipient
		{
			get
			{
				if (this._recipient == null)
				{
					this._recipient = string.Empty;
				}
				return this._recipient;
			}
			set
			{
				this._recipient = value;
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000065F8 File Offset: 0x000047F8
		private byte[] GetCipherValue(CipherData cipherData)
		{
			if (cipherData == null)
			{
				throw new ArgumentNullException("cipherData");
			}
			WebResponse webResponse = null;
			Stream stream = null;
			if (cipherData.CipherValue != null)
			{
				return cipherData.CipherValue;
			}
			if (cipherData.CipherReference == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			if (cipherData.CipherReference.CipherValue != null)
			{
				return cipherData.CipherReference.CipherValue;
			}
			if (cipherData.CipherReference.Uri == null)
			{
				throw new CryptographicException(" The specified Uri is not supported.");
			}
			Stream stream2;
			if (cipherData.CipherReference.Uri.Length == 0)
			{
				string baseUri = (this._document == null) ? null : this._document.BaseURI;
				TransformChain transformChain = cipherData.CipherReference.TransformChain;
				if (transformChain == null)
				{
					throw new CryptographicException(" The specified Uri is not supported.");
				}
				stream2 = transformChain.TransformToOctetStream(this._document, this._xmlResolver, baseUri);
			}
			else
			{
				if (cipherData.CipherReference.Uri[0] != '#')
				{
					throw new CryptographicException("Unable to resolve Uri {0}.", cipherData.CipherReference.Uri);
				}
				string idValue = Utils.ExtractIdFromLocalUri(cipherData.CipherReference.Uri);
				XmlElement idElement = this.GetIdElement(this._document, idValue);
				if (idElement == null || idElement.OuterXml == null)
				{
					throw new CryptographicException(" The specified Uri is not supported.");
				}
				stream = new MemoryStream(this._encoding.GetBytes(idElement.OuterXml));
				string baseUri2 = (this._document == null) ? null : this._document.BaseURI;
				TransformChain transformChain2 = cipherData.CipherReference.TransformChain;
				if (transformChain2 == null)
				{
					throw new CryptographicException(" The specified Uri is not supported.");
				}
				stream2 = transformChain2.TransformToOctetStream(stream, this._xmlResolver, baseUri2);
			}
			byte[] array = null;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Utils.Pump(stream2, memoryStream);
				array = memoryStream.ToArray();
				if (webResponse != null)
				{
					webResponse.Close();
				}
				if (stream != null)
				{
					stream.Close();
				}
				stream2.Close();
			}
			cipherData.CipherReference.CipherValue = array;
			return array;
		}

		/// <summary>Determines how to resolve internal Uniform Resource Identifier (URI) references.</summary>
		/// <param name="document">An <see cref="T:System.Xml.XmlDocument" /> object that contains an element with an ID value.</param>
		/// <param name="idValue">A string that represents the ID value.</param>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that contains an ID indicating how internal Uniform Resource Identifiers (URIs) are to be resolved.</returns>
		// Token: 0x06000150 RID: 336 RVA: 0x000067EC File Offset: 0x000049EC
		public virtual XmlElement GetIdElement(XmlDocument document, string idValue)
		{
			return SignedXml.DefaultGetIdElement(document, idValue);
		}

		/// <summary>Retrieves the decryption initialization vector (IV) from an <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that contains the initialization vector (IV) to retrieve.</param>
		/// <param name="symmetricAlgorithmUri">The Uniform Resource Identifier (URI) that describes the cryptographic algorithm associated with the <paramref name="encryptedData" /> value.</param>
		/// <returns>A byte array that contains the decryption initialization vector (IV).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="encryptedData" /> parameter has an <see cref="P:System.Security.Cryptography.Xml.EncryptedType.EncryptionMethod" /> property that is null.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithmUrisymAlgUri" /> parameter is not a supported algorithm.</exception>
		// Token: 0x06000151 RID: 337 RVA: 0x000067F8 File Offset: 0x000049F8
		public virtual byte[] GetDecryptionIV(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symmetricAlgorithmUri == null)
			{
				if (encryptedData.EncryptionMethod == null)
				{
					throw new CryptographicException("Symmetric algorithm is not specified.");
				}
				symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
			}
			int num;
			if (!(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#des-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#tripledes-cbc"))
			{
				if (!(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes128-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes192-cbc") && !(symmetricAlgorithmUri == "http://www.w3.org/2001/04/xmlenc#aes256-cbc"))
				{
					throw new CryptographicException(" The specified Uri is not supported.");
				}
				num = 16;
			}
			else
			{
				num = 8;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(this.GetCipherValue(encryptedData.CipherData), 0, array, 0, array.Length);
			return array;
		}

		/// <summary>Retrieves the decryption key from the specified <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that contains the decryption key to retrieve.</param>
		/// <param name="symmetricAlgorithmUri">The size of the decryption key to retrieve.</param>
		/// <returns>A <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> object associated with the decryption key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encryptedData parameter has an <see cref="P:System.Security.Cryptography.Xml.EncryptedType.EncryptionMethod" /> property that is null.  
		///  -or-  
		///  The encrypted key cannot be retrieved using the specified parameters.</exception>
		// Token: 0x06000152 RID: 338 RVA: 0x000068B0 File Offset: 0x00004AB0
		public virtual SymmetricAlgorithm GetDecryptionKey(EncryptedData encryptedData, string symmetricAlgorithmUri)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (encryptedData.KeyInfo == null)
			{
				return null;
			}
			IEnumerator enumerator = encryptedData.KeyInfo.GetEnumerator();
			EncryptedKey encryptedKey = null;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				KeyInfoName keyInfoName = obj as KeyInfoName;
				if (keyInfoName != null)
				{
					string value = keyInfoName.Value;
					if ((SymmetricAlgorithm)this._keyNameMapping[value] != null)
					{
						return (SymmetricAlgorithm)this._keyNameMapping[value];
					}
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this._document.NameTable);
					xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
					XmlNodeList xmlNodeList = this._document.SelectNodes("//enc:EncryptedKey", xmlNamespaceManager);
					if (xmlNodeList == null)
					{
						break;
					}
					using (IEnumerator enumerator2 = xmlNodeList.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							XmlElement value2 = ((XmlNode)obj2) as XmlElement;
							EncryptedKey encryptedKey2 = new EncryptedKey();
							encryptedKey2.LoadXml(value2);
							if (encryptedKey2.CarriedKeyName == value && encryptedKey2.Recipient == this.Recipient)
							{
								encryptedKey = encryptedKey2;
								break;
							}
						}
						break;
					}
				}
				KeyInfoRetrievalMethod keyInfoRetrievalMethod = enumerator.Current as KeyInfoRetrievalMethod;
				if (keyInfoRetrievalMethod != null)
				{
					string idValue = Utils.ExtractIdFromLocalUri(keyInfoRetrievalMethod.Uri);
					encryptedKey = new EncryptedKey();
					encryptedKey.LoadXml(this.GetIdElement(this._document, idValue));
					break;
				}
				KeyInfoEncryptedKey keyInfoEncryptedKey = enumerator.Current as KeyInfoEncryptedKey;
				if (keyInfoEncryptedKey != null)
				{
					encryptedKey = keyInfoEncryptedKey.EncryptedKey;
					break;
				}
			}
			if (encryptedKey == null)
			{
				return null;
			}
			if (symmetricAlgorithmUri == null)
			{
				if (encryptedData.EncryptionMethod == null)
				{
					throw new CryptographicException("Symmetric algorithm is not specified.");
				}
				symmetricAlgorithmUri = encryptedData.EncryptionMethod.KeyAlgorithm;
			}
			byte[] array = this.DecryptEncryptedKey(encryptedKey);
			if (array == null)
			{
				throw new CryptographicException("Unable to retrieve the decryption key.");
			}
			SymmetricAlgorithm symmetricAlgorithm = CryptoHelpers.CreateFromName<SymmetricAlgorithm>(symmetricAlgorithmUri);
			if (symmetricAlgorithm == null)
			{
				throw new CryptographicException("Symmetric algorithm is not specified.");
			}
			symmetricAlgorithm.Key = array;
			return symmetricAlgorithm;
		}

		/// <summary>Determines the key represented by the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> element.</summary>
		/// <param name="encryptedKey">The <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that contains the key to retrieve.</param>
		/// <returns>A byte array that contains the key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedKey" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="encryptedKey" /> parameter is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael).</exception>
		// Token: 0x06000153 RID: 339 RVA: 0x00006AAC File Offset: 0x00004CAC
		public virtual byte[] DecryptEncryptedKey(EncryptedKey encryptedKey)
		{
			if (encryptedKey == null)
			{
				throw new ArgumentNullException("encryptedKey");
			}
			if (encryptedKey.KeyInfo == null)
			{
				return null;
			}
			foreach (object obj in encryptedKey.KeyInfo)
			{
				KeyInfoName keyInfoName = obj as KeyInfoName;
				bool useOAEP;
				if (keyInfoName == null)
				{
					IEnumerator enumerator;
					KeyInfoX509Data keyInfoX509Data = enumerator.Current as KeyInfoX509Data;
					if (keyInfoX509Data != null)
					{
						foreach (X509Certificate2 certificate in Utils.BuildBagOfCerts(keyInfoX509Data, CertUsageType.Decryption))
						{
							using (RSA rsaprivateKey = certificate.GetRSAPrivateKey())
							{
								if (rsaprivateKey != null)
								{
									if (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null)
									{
										throw new CryptographicException("Symmetric algorithm is not specified.");
									}
									useOAEP = (encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p");
									return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, rsaprivateKey, useOAEP);
								}
							}
						}
						break;
					}
					KeyInfoRetrievalMethod keyInfoRetrievalMethod = enumerator.Current as KeyInfoRetrievalMethod;
					EncryptedKey encryptedKey2;
					if (keyInfoRetrievalMethod != null)
					{
						string idValue = Utils.ExtractIdFromLocalUri(keyInfoRetrievalMethod.Uri);
						encryptedKey2 = new EncryptedKey();
						encryptedKey2.LoadXml(this.GetIdElement(this._document, idValue));
						try
						{
							this._xmlDsigSearchDepthCounter++;
							if (this.IsOverXmlDsigRecursionLimit())
							{
								throw new CryptoSignedXmlRecursionException();
							}
							return this.DecryptEncryptedKey(encryptedKey2);
						}
						finally
						{
							this._xmlDsigSearchDepthCounter--;
						}
					}
					KeyInfoEncryptedKey keyInfoEncryptedKey = enumerator.Current as KeyInfoEncryptedKey;
					if (keyInfoEncryptedKey == null)
					{
						continue;
					}
					encryptedKey2 = keyInfoEncryptedKey.EncryptedKey;
					byte[] array = this.DecryptEncryptedKey(encryptedKey2);
					if (array == null)
					{
						continue;
					}
					SymmetricAlgorithm symmetricAlgorithm = CryptoHelpers.CreateFromName<SymmetricAlgorithm>(encryptedKey.EncryptionMethod.KeyAlgorithm);
					if (symmetricAlgorithm == null)
					{
						throw new CryptographicException("Symmetric algorithm is not specified.");
					}
					symmetricAlgorithm.Key = array;
					if (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null)
					{
						throw new CryptographicException("Symmetric algorithm is not specified.");
					}
					symmetricAlgorithm.Key = array;
					return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, symmetricAlgorithm);
				}
				string value = keyInfoName.Value;
				object obj2 = this._keyNameMapping[value];
				if (obj2 == null)
				{
					break;
				}
				if (encryptedKey.CipherData == null || encryptedKey.CipherData.CipherValue == null)
				{
					throw new CryptographicException("Symmetric algorithm is not specified.");
				}
				if (obj2 is SymmetricAlgorithm)
				{
					return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (SymmetricAlgorithm)obj2);
				}
				useOAEP = (encryptedKey.EncryptionMethod != null && encryptedKey.EncryptionMethod.KeyAlgorithm == "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p");
				return EncryptedXml.DecryptKey(encryptedKey.CipherData.CipherValue, (RSA)obj2, useOAEP);
			}
			return null;
		}

		/// <summary>Defines a mapping between a key name and a symmetric key or an asymmetric key.</summary>
		/// <param name="keyName">The name to map to <paramref name="keyObject" />.</param>
		/// <param name="keyObject">The symmetric key to map to <paramref name="keyName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyName" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="keyObject" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="keyObject" /> parameter is not an RSA algorithm or a symmetric key.</exception>
		// Token: 0x06000154 RID: 340 RVA: 0x00006D70 File Offset: 0x00004F70
		public void AddKeyNameMapping(string keyName, object keyObject)
		{
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			if (keyObject == null)
			{
				throw new ArgumentNullException("keyObject");
			}
			if (!(keyObject is SymmetricAlgorithm) && !(keyObject is RSA))
			{
				throw new CryptographicException("The specified cryptographic transform is not supported.");
			}
			this._keyNameMapping.Add(keyName, keyObject);
		}

		/// <summary>Resets all key name mapping.</summary>
		// Token: 0x06000155 RID: 341 RVA: 0x00006DC1 File Offset: 0x00004FC1
		public void ClearKeyNameMappings()
		{
			this._keyNameMapping.Clear();
		}

		/// <summary>Encrypts the outer XML of an element using the specified X.509 certificate.</summary>
		/// <param name="inputElement">The XML element to encrypt.</param>
		/// <param name="certificate">The X.509 certificate to use for encryption.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> element that represents the encrypted XML data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The value of the <paramref name="certificate" /> parameter does not represent an RSA key algorithm.</exception>
		// Token: 0x06000156 RID: 342 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public EncryptedData Encrypt(XmlElement inputElement, X509Certificate2 certificate)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			EncryptedData result;
			using (RSA rsapublicKey = certificate.GetRSAPublicKey())
			{
				if (rsapublicKey == null)
				{
					throw new NotSupportedException("The certificate key algorithm is not supported.");
				}
				EncryptedData encryptedData = new EncryptedData();
				encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
				encryptedData.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
				EncryptedKey encryptedKey = new EncryptedKey();
				encryptedKey.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#rsa-1_5");
				encryptedKey.KeyInfo.AddClause(new KeyInfoX509Data(certificate));
				RijndaelManaged rijndaelManaged = new RijndaelManaged();
				encryptedKey.CipherData.CipherValue = EncryptedXml.EncryptKey(rijndaelManaged.Key, rsapublicKey, false);
				KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
				encryptedData.KeyInfo.AddClause(clause);
				encryptedData.CipherData.CipherValue = this.EncryptData(inputElement, rijndaelManaged, false);
				result = encryptedData;
			}
			return result;
		}

		/// <summary>Encrypts the outer XML of an element using the specified key in the key mapping table.</summary>
		/// <param name="inputElement">The XML element to encrypt.</param>
		/// <param name="keyName">A key name that can be found in the key mapping table.</param>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object that represents the encrypted XML data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="keyName" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="keyName" /> parameter does not match a registered key name pair.  
		///  -or-  
		///  The cryptographic key described by the <paramref name="keyName" /> parameter is not supported.</exception>
		// Token: 0x06000157 RID: 343 RVA: 0x00006EB8 File Offset: 0x000050B8
		public EncryptedData Encrypt(XmlElement inputElement, string keyName)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (keyName == null)
			{
				throw new ArgumentNullException("keyName");
			}
			object obj = null;
			if (this._keyNameMapping != null)
			{
				obj = this._keyNameMapping[keyName];
			}
			if (obj == null)
			{
				throw new CryptographicException("Unable to retrieve the encryption key.");
			}
			SymmetricAlgorithm symmetricAlgorithm = obj as SymmetricAlgorithm;
			RSA rsa = obj as RSA;
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.Type = "http://www.w3.org/2001/04/xmlenc#Element";
			encryptedData.EncryptionMethod = new EncryptionMethod("http://www.w3.org/2001/04/xmlenc#aes256-cbc");
			string algorithm = null;
			if (symmetricAlgorithm == null)
			{
				algorithm = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
			}
			else if (symmetricAlgorithm is TripleDES)
			{
				algorithm = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";
			}
			else
			{
				if (!(symmetricAlgorithm is Rijndael) && !(symmetricAlgorithm is Aes))
				{
					throw new CryptographicException("The specified cryptographic transform is not supported.");
				}
				int keySize = symmetricAlgorithm.KeySize;
				if (keySize != 128)
				{
					if (keySize != 192)
					{
						if (keySize == 256)
						{
							algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes256";
						}
					}
					else
					{
						algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes192";
					}
				}
				else
				{
					algorithm = "http://www.w3.org/2001/04/xmlenc#kw-aes128";
				}
			}
			EncryptedKey encryptedKey = new EncryptedKey();
			encryptedKey.EncryptionMethod = new EncryptionMethod(algorithm);
			encryptedKey.KeyInfo.AddClause(new KeyInfoName(keyName));
			RijndaelManaged rijndaelManaged = new RijndaelManaged();
			encryptedKey.CipherData.CipherValue = ((symmetricAlgorithm == null) ? EncryptedXml.EncryptKey(rijndaelManaged.Key, rsa, false) : EncryptedXml.EncryptKey(rijndaelManaged.Key, symmetricAlgorithm));
			KeyInfoEncryptedKey clause = new KeyInfoEncryptedKey(encryptedKey);
			encryptedData.KeyInfo.AddClause(clause);
			encryptedData.CipherData.CipherValue = this.EncryptData(inputElement, rijndaelManaged, false);
			return encryptedData;
		}

		/// <summary>Decrypts all <see langword="&lt;EncryptedData&gt;" /> elements of the XML document that were specified during initialization of the <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> class.</summary>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The cryptographic key used to decrypt the document was not found.</exception>
		// Token: 0x06000158 RID: 344 RVA: 0x00007030 File Offset: 0x00005230
		public void DecryptDocument()
		{
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this._document.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNodeList xmlNodeList = this._document.SelectNodes("//enc:EncryptedData", xmlNamespaceManager);
			if (xmlNodeList != null)
			{
				foreach (object obj in xmlNodeList)
				{
					XmlElement xmlElement = ((XmlNode)obj) as XmlElement;
					EncryptedData encryptedData = new EncryptedData();
					encryptedData.LoadXml(xmlElement);
					SymmetricAlgorithm decryptionKey = this.GetDecryptionKey(encryptedData, null);
					if (decryptionKey == null)
					{
						throw new CryptographicException("Unable to retrieve the decryption key.");
					}
					byte[] decryptedData = this.DecryptData(encryptedData, decryptionKey);
					this.ReplaceData(xmlElement, decryptedData);
				}
			}
		}

		/// <summary>Encrypts data in the specified byte array using the specified symmetric algorithm.</summary>
		/// <param name="plaintext">The data to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption.</param>
		/// <returns>A byte array of encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="plaintext" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The data could not be encrypted using the specified parameters.</exception>
		// Token: 0x06000159 RID: 345 RVA: 0x000070FC File Offset: 0x000052FC
		public byte[] EncryptData(byte[] plaintext, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (plaintext == null)
			{
				throw new ArgumentNullException("plaintext");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			CipherMode mode = symmetricAlgorithm.Mode;
			PaddingMode padding = symmetricAlgorithm.Padding;
			byte[] array = null;
			try
			{
				symmetricAlgorithm.Mode = this._mode;
				symmetricAlgorithm.Padding = this._padding;
				array = symmetricAlgorithm.CreateEncryptor().TransformFinalBlock(plaintext, 0, plaintext.Length);
			}
			finally
			{
				symmetricAlgorithm.Mode = mode;
				symmetricAlgorithm.Padding = padding;
			}
			byte[] array2;
			if (this._mode == CipherMode.ECB)
			{
				array2 = array;
			}
			else
			{
				byte[] iv = symmetricAlgorithm.IV;
				array2 = new byte[array.Length + iv.Length];
				Buffer.BlockCopy(iv, 0, array2, 0, iv.Length);
				Buffer.BlockCopy(array, 0, array2, iv.Length, array.Length);
			}
			return array2;
		}

		/// <summary>Encrypts the specified element or its contents using the specified symmetric algorithm.</summary>
		/// <param name="inputElement">The element or its contents to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric algorithm to use for encryption.</param>
		/// <param name="content">
		///   <see langword="true" /> to encrypt only the contents of the element; <see langword="false" /> to encrypt the entire element.</param>
		/// <returns>A byte array that contains the encrypted data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600015A RID: 346 RVA: 0x000071C4 File Offset: 0x000053C4
		public byte[] EncryptData(XmlElement inputElement, SymmetricAlgorithm symmetricAlgorithm, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			byte[] plaintext = content ? this._encoding.GetBytes(inputElement.InnerXml) : this._encoding.GetBytes(inputElement.OuterXml);
			return this.EncryptData(plaintext, symmetricAlgorithm);
		}

		/// <summary>Decrypts an <see langword="&lt;EncryptedData&gt;" /> element using the specified symmetric algorithm.</summary>
		/// <param name="encryptedData">The data to decrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to decrypt <paramref name="encryptedData" />.</param>
		/// <returns>A byte array that contains the raw decrypted plain text.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="encryptedData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600015B RID: 347 RVA: 0x00007220 File Offset: 0x00005420
		public byte[] DecryptData(EncryptedData encryptedData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			byte[] cipherValue = this.GetCipherValue(encryptedData.CipherData);
			CipherMode mode = symmetricAlgorithm.Mode;
			PaddingMode padding = symmetricAlgorithm.Padding;
			byte[] iv = symmetricAlgorithm.IV;
			byte[] array = null;
			if (this._mode != CipherMode.ECB)
			{
				array = this.GetDecryptionIV(encryptedData, null);
			}
			byte[] result = null;
			try
			{
				int num = 0;
				if (array != null)
				{
					symmetricAlgorithm.IV = array;
					num = array.Length;
				}
				symmetricAlgorithm.Mode = this._mode;
				symmetricAlgorithm.Padding = this._padding;
				result = symmetricAlgorithm.CreateDecryptor().TransformFinalBlock(cipherValue, num, cipherValue.Length - num);
			}
			finally
			{
				symmetricAlgorithm.Mode = mode;
				symmetricAlgorithm.Padding = padding;
				symmetricAlgorithm.IV = iv;
			}
			return result;
		}

		/// <summary>Replaces an <see langword="&lt;EncryptedData&gt;" /> element with a specified decrypted sequence of bytes.</summary>
		/// <param name="inputElement">The <see langword="&lt;EncryptedData&gt;" /> element to replace.</param>
		/// <param name="decryptedData">The decrypted data to replace <paramref name="inputElement" /> with.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="decryptedData" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600015C RID: 348 RVA: 0x000072F4 File Offset: 0x000054F4
		public void ReplaceData(XmlElement inputElement, byte[] decryptedData)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (decryptedData == null)
			{
				throw new ArgumentNullException("decryptedData");
			}
			XmlNode parentNode = inputElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				using (StringReader stringReader = new StringReader(this._encoding.GetString(decryptedData)))
				{
					using (XmlReader xmlReader = XmlReader.Create(stringReader, Utils.GetSecureXmlReaderSettings(this._xmlResolver)))
					{
						xmlDocument.Load(xmlReader);
					}
				}
				XmlNode newChild = inputElement.OwnerDocument.ImportNode(xmlDocument.DocumentElement, true);
				parentNode.RemoveChild(inputElement);
				parentNode.AppendChild(newChild);
				return;
			}
			XmlNode xmlNode = parentNode.OwnerDocument.CreateElement(parentNode.Prefix, parentNode.LocalName, parentNode.NamespaceURI);
			try
			{
				parentNode.AppendChild(xmlNode);
				xmlNode.InnerXml = this._encoding.GetString(decryptedData);
				XmlNode xmlNode2 = xmlNode.FirstChild;
				XmlNode nextSibling = inputElement.NextSibling;
				while (xmlNode2 != null)
				{
					XmlNode nextSibling2 = xmlNode2.NextSibling;
					parentNode.InsertBefore(xmlNode2, nextSibling);
					xmlNode2 = nextSibling2;
				}
			}
			finally
			{
				parentNode.RemoveChild(xmlNode);
			}
			parentNode.RemoveChild(inputElement);
		}

		/// <summary>Replaces the specified element with the specified <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <param name="inputElement">The element to replace with an <see langword="&lt;EncryptedData&gt;" /> element.</param>
		/// <param name="encryptedData">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object to replace the <paramref name="inputElement" /> parameter with.</param>
		/// <param name="content">
		///   <see langword="true" /> to replace only the contents of the element; <see langword="false" /> to replace the entire element.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="inputElement" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="encryptedData" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600015D RID: 349 RVA: 0x00007448 File Offset: 0x00005648
		public static void ReplaceElement(XmlElement inputElement, EncryptedData encryptedData, bool content)
		{
			if (inputElement == null)
			{
				throw new ArgumentNullException("inputElement");
			}
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			XmlElement xml = encryptedData.GetXml(inputElement.OwnerDocument);
			if (content)
			{
				Utils.RemoveAllChildren(inputElement);
				inputElement.AppendChild(xml);
				return;
			}
			inputElement.ParentNode.ReplaceChild(xml, inputElement);
		}

		/// <summary>Encrypts a key using a symmetric algorithm that a recipient uses to decrypt an <see langword="&lt;EncryptedData&gt;" /> element.</summary>
		/// <param name="keyData">The key to encrypt.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to encrypt <paramref name="keyData" />.</param>
		/// <returns>A byte array that represents the encrypted value of the <paramref name="keyData" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="symmetricAlgorithm" /> parameter is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael).</exception>
		// Token: 0x0600015E RID: 350 RVA: 0x000074A0 File Offset: 0x000056A0
		public static byte[] EncryptKey(byte[] keyData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			if (symmetricAlgorithm is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			if (symmetricAlgorithm is Rijndael || symmetricAlgorithm is Aes)
			{
				return SymmetricKeyWrap.AESKeyWrapEncrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException("The specified cryptographic transform is not supported.");
		}

		/// <summary>Encrypts the key that a recipient uses to decrypt an <see langword="&lt;EncryptedData&gt;" /> element.</summary>
		/// <param name="keyData">The key to encrypt.</param>
		/// <param name="rsa">The asymmetric key used to encrypt <paramref name="keyData" />.</param>
		/// <param name="useOAEP">A value that specifies whether to use Optimal Asymmetric Encryption Padding (OAEP).</param>
		/// <returns>A byte array that represents the encrypted value of the <paramref name="keyData" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="rsa" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600015F RID: 351 RVA: 0x00007505 File Offset: 0x00005705
		public static byte[] EncryptKey(byte[] keyData, RSA rsa, bool useOAEP)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (useOAEP)
			{
				return new RSAOAEPKeyExchangeFormatter(rsa).CreateKeyExchange(keyData);
			}
			return new RSAPKCS1KeyExchangeFormatter(rsa).CreateKeyExchange(keyData);
		}

		/// <summary>Decrypts an <see langword="&lt;EncryptedKey&gt;" /> element using a symmetric algorithm.</summary>
		/// <param name="keyData">An array of bytes that represents an encrypted <see langword="&lt;EncryptedKey&gt;" /> element.</param>
		/// <param name="symmetricAlgorithm">The symmetric key used to decrypt <paramref name="keyData" />.</param>
		/// <returns>A byte array that contains the plain text key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="symmetricAlgorithm" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The value of the <paramref name="symmetricAlgorithm" /> element is not the Triple DES Key Wrap algorithm or the Advanced Encryption Standard (AES) Key Wrap algorithm (also called Rijndael).</exception>
		// Token: 0x06000160 RID: 352 RVA: 0x00007540 File Offset: 0x00005740
		public static byte[] DecryptKey(byte[] keyData, SymmetricAlgorithm symmetricAlgorithm)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (symmetricAlgorithm == null)
			{
				throw new ArgumentNullException("symmetricAlgorithm");
			}
			if (symmetricAlgorithm is TripleDES)
			{
				return SymmetricKeyWrap.TripleDESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			if (symmetricAlgorithm is Rijndael || symmetricAlgorithm is Aes)
			{
				return SymmetricKeyWrap.AESKeyWrapDecrypt(symmetricAlgorithm.Key, keyData);
			}
			throw new CryptographicException("The specified cryptographic transform is not supported.");
		}

		/// <summary>Decrypts an <see langword="&lt;EncryptedKey&gt;" /> element using an asymmetric algorithm.</summary>
		/// <param name="keyData">An array of bytes that represents an encrypted <see langword="&lt;EncryptedKey&gt;" /> element.</param>
		/// <param name="rsa">The asymmetric key used to decrypt <paramref name="keyData" />.</param>
		/// <param name="useOAEP">A value that specifies whether to use Optimal Asymmetric Encryption Padding (OAEP).</param>
		/// <returns>A byte array that contains the plain text key.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="keyData" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The value of the <paramref name="rsa" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000161 RID: 353 RVA: 0x000075A5 File Offset: 0x000057A5
		public static byte[] DecryptKey(byte[] keyData, RSA rsa, bool useOAEP)
		{
			if (keyData == null)
			{
				throw new ArgumentNullException("keyData");
			}
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (useOAEP)
			{
				return new RSAOAEPKeyExchangeDeformatter(rsa).DecryptKeyExchange(keyData);
			}
			return new RSAPKCS1KeyExchangeDeformatter(rsa).DecryptKeyExchange(keyData);
		}

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for XML encryption syntax and processing. This field is constant.</summary>
		// Token: 0x0400017D RID: 381
		public const string XmlEncNamespaceUrl = "http://www.w3.org/2001/04/xmlenc#";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for an XML encryption element. This field is constant.</summary>
		// Token: 0x0400017E RID: 382
		public const string XmlEncElementUrl = "http://www.w3.org/2001/04/xmlenc#Element";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for XML encryption element content. This field is constant.</summary>
		// Token: 0x0400017F RID: 383
		public const string XmlEncElementContentUrl = "http://www.w3.org/2001/04/xmlenc#Content";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the XML encryption <see langword="&lt;EncryptedKey&gt;" /> element. This field is constant.</summary>
		// Token: 0x04000180 RID: 384
		public const string XmlEncEncryptedKeyUrl = "http://www.w3.org/2001/04/xmlenc#EncryptedKey";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the Digital Encryption Standard (DES) algorithm. This field is constant.</summary>
		// Token: 0x04000181 RID: 385
		public const string XmlEncDESUrl = "http://www.w3.org/2001/04/xmlenc#des-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the Triple DES algorithm. This field is constant.</summary>
		// Token: 0x04000182 RID: 386
		public const string XmlEncTripleDESUrl = "http://www.w3.org/2001/04/xmlenc#tripledes-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 128-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x04000183 RID: 387
		public const string XmlEncAES128Url = "http://www.w3.org/2001/04/xmlenc#aes128-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 256-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x04000184 RID: 388
		public const string XmlEncAES256Url = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 192-bit Advanced Encryption Standard (AES) algorithm (also known as the Rijndael algorithm). This field is constant.</summary>
		// Token: 0x04000185 RID: 389
		public const string XmlEncAES192Url = "http://www.w3.org/2001/04/xmlenc#aes192-cbc";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the RSA Public Key Cryptography Standard (PKCS) Version 1.5 algorithm. This field is constant.</summary>
		// Token: 0x04000186 RID: 390
		public const string XmlEncRSA15Url = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the RSA Optimal Asymmetric Encryption Padding (OAEP) encryption algorithm. This field is constant.</summary>
		// Token: 0x04000187 RID: 391
		public const string XmlEncRSAOAEPUrl = "http://www.w3.org/2001/04/xmlenc#rsa-oaep-mgf1p";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the TRIPLEDES key wrap algorithm. This field is constant.</summary>
		// Token: 0x04000188 RID: 392
		public const string XmlEncTripleDESKeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-tripledes";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 128-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		// Token: 0x04000189 RID: 393
		public const string XmlEncAES128KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes128";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 256-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		// Token: 0x0400018A RID: 394
		public const string XmlEncAES256KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes256";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the 192-bit Advanced Encryption Standard (AES) Key Wrap algorithm (also known as the Rijndael Key Wrap algorithm). This field is constant.</summary>
		// Token: 0x0400018B RID: 395
		public const string XmlEncAES192KeyWrapUrl = "http://www.w3.org/2001/04/xmlenc#kw-aes192";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the SHA-256 algorithm. This field is constant.</summary>
		// Token: 0x0400018C RID: 396
		public const string XmlEncSHA256Url = "http://www.w3.org/2001/04/xmlenc#sha256";

		/// <summary>Represents the namespace Uniform Resource Identifier (URI) for the SHA-512 algorithm. This field is constant.</summary>
		// Token: 0x0400018D RID: 397
		public const string XmlEncSHA512Url = "http://www.w3.org/2001/04/xmlenc#sha512";

		// Token: 0x0400018E RID: 398
		private XmlDocument _document;

		// Token: 0x0400018F RID: 399
		private Evidence _evidence;

		// Token: 0x04000190 RID: 400
		private XmlResolver _xmlResolver;

		// Token: 0x04000191 RID: 401
		private const int _capacity = 4;

		// Token: 0x04000192 RID: 402
		private Hashtable _keyNameMapping;

		// Token: 0x04000193 RID: 403
		private PaddingMode _padding;

		// Token: 0x04000194 RID: 404
		private CipherMode _mode;

		// Token: 0x04000195 RID: 405
		private Encoding _encoding;

		// Token: 0x04000196 RID: 406
		private string _recipient;

		// Token: 0x04000197 RID: 407
		private int _xmlDsigSearchDepthCounter;

		// Token: 0x04000198 RID: 408
		private int _xmlDsigSearchDepth;
	}
}
