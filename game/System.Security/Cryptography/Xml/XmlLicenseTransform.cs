using System;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the license transform algorithm used to normalize XrML licenses for signatures.</summary>
	// Token: 0x02000067 RID: 103
	public class XmlLicenseTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> class.</summary>
		// Token: 0x0600036A RID: 874 RVA: 0x000109D8 File Offset: 0x0000EBD8
		public XmlLicenseTransform()
		{
			base.Algorithm = "urn:mpeg:mpeg21:2003:01-REL-R-NS:licenseTransform";
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>An array of types that are valid inputs to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object; you can pass only objects of one of these types to the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00010A28 File Offset: 0x0000EC28
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are valid outputs from the <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.OutputTypes" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlLicenseTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00010A30 File Offset: 0x0000EC30
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Gets or sets the decryptor of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The decryptor of the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00010A38 File Offset: 0x0000EC38
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00010A40 File Offset: 0x0000EC40
		public IRelDecryptor Decryptor
		{
			get
			{
				return this._relDecryptor;
			}
			set
			{
				this._relDecryptor = value;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00010A4C File Offset: 0x0000EC4C
		private void DecryptEncryptedGrants(XmlNodeList encryptedGrantList, IRelDecryptor decryptor)
		{
			int i = 0;
			int count = encryptedGrantList.Count;
			while (i < count)
			{
				XmlElement xmlElement = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/enc:EncryptionMethod", this._namespaceManager) as XmlElement;
				XmlElement xmlElement2 = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/dsig:KeyInfo", this._namespaceManager) as XmlElement;
				XmlElement xmlElement3 = encryptedGrantList[i].SelectSingleNode("//r:encryptedGrant/enc:CipherData", this._namespaceManager) as XmlElement;
				if (xmlElement != null && xmlElement2 != null && xmlElement3 != null)
				{
					EncryptionMethod encryptionMethod = new EncryptionMethod();
					KeyInfo keyInfo = new KeyInfo();
					CipherData cipherData = new CipherData();
					encryptionMethod.LoadXml(xmlElement);
					keyInfo.LoadXml(xmlElement2);
					cipherData.LoadXml(xmlElement3);
					MemoryStream memoryStream = null;
					Stream stream = null;
					StreamReader streamReader = null;
					try
					{
						memoryStream = new MemoryStream(cipherData.CipherValue);
						stream = this._relDecryptor.Decrypt(encryptionMethod, keyInfo, memoryStream);
						if (stream == null || stream.Length == 0L)
						{
							throw new CryptographicException("Unable to decrypt grant content.");
						}
						streamReader = new StreamReader(stream);
						string innerXml = streamReader.ReadToEnd();
						encryptedGrantList[i].ParentNode.InnerXml = innerXml;
					}
					finally
					{
						if (memoryStream != null)
						{
							memoryStream.Close();
						}
						if (stream != null)
						{
							stream.Close();
						}
						if (streamReader != null)
						{
							streamReader.Close();
						}
					}
				}
				i++;
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		// Token: 0x06000370 RID: 880 RVA: 0x0000F43E File Offset: 0x0000D63E
		protected override XmlNodeList GetInnerXml()
		{
			return null;
		}

		/// <summary>Returns the output of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <returns>The output of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		// Token: 0x06000371 RID: 881 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		public override object GetOutput()
		{
			return this._license;
		}

		/// <summary>Returns the output of an <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlDocument" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlDocument" /> object.</exception>
		// Token: 0x06000372 RID: 882 RVA: 0x00010BD0 File Offset: 0x0000EDD0
		public override object GetOutput(Type type)
		{
			if (type != typeof(XmlDocument) && !type.IsSubclassOf(typeof(XmlDocument)))
			{
				throw new ArgumentException("The input type was invalid for this transform.", "type");
			}
			return this.GetOutput();
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element; this method is not supported because the <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object has no inner XML elements.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that encapsulates the transform to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</param>
		// Token: 0x06000373 RID: 883 RVA: 0x0000F76F File Offset: 0x0000D96F
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList != null && nodeList.Count > 0)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
		}

		/// <summary>Loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlLicenseTransform" /> object. The type of the input object must be <see cref="T:System.Xml.XmlDocument" />.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The context was not set before this transform was invoked.  
		///  -or-  
		///  The <see langword="&lt;issuer&gt;" /> element was not set before this transform was invoked.  
		///  -or-  
		///  The <see langword="&lt;license&gt;" /> element was not set before this transform was invoked.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.XmlLicenseTransform.Decryptor" /> property was not set before this transform was invoked.</exception>
		// Token: 0x06000374 RID: 884 RVA: 0x00010C0C File Offset: 0x0000EE0C
		public override void LoadInput(object obj)
		{
			if (base.Context == null)
			{
				throw new CryptographicException("Null Context property encountered.");
			}
			this._license = new XmlDocument();
			this._license.PreserveWhitespace = true;
			this._namespaceManager = new XmlNamespaceManager(this._license.NameTable);
			this._namespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			this._namespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this._namespaceManager.AddNamespace("r", "urn:mpeg:mpeg21:2003:01-REL-R-NS");
			XmlElement xmlElement = base.Context.SelectSingleNode("ancestor-or-self::r:issuer[1]", this._namespaceManager) as XmlElement;
			if (xmlElement == null)
			{
				throw new CryptographicException("Issuer node is required.");
			}
			XmlNode xmlNode = xmlElement.SelectSingleNode("descendant-or-self::dsig:Signature[1]", this._namespaceManager) as XmlElement;
			if (xmlNode != null)
			{
				xmlNode.ParentNode.RemoveChild(xmlNode);
			}
			XmlElement xmlElement2 = xmlElement.SelectSingleNode("ancestor-or-self::r:license[1]", this._namespaceManager) as XmlElement;
			if (xmlElement2 == null)
			{
				throw new CryptographicException("License node is required.");
			}
			XmlNodeList xmlNodeList = xmlElement2.SelectNodes("descendant-or-self::r:license[1]/r:issuer", this._namespaceManager);
			int i = 0;
			int count = xmlNodeList.Count;
			while (i < count)
			{
				if (xmlNodeList[i] != xmlElement && xmlNodeList[i].LocalName == "issuer" && xmlNodeList[i].NamespaceURI == "urn:mpeg:mpeg21:2003:01-REL-R-NS")
				{
					xmlNodeList[i].ParentNode.RemoveChild(xmlNodeList[i]);
				}
				i++;
			}
			XmlNodeList xmlNodeList2 = xmlElement2.SelectNodes("/r:license/r:grant/r:encryptedGrant", this._namespaceManager);
			if (xmlNodeList2.Count > 0)
			{
				if (this._relDecryptor == null)
				{
					throw new CryptographicException("IRelDecryptor is required.");
				}
				this.DecryptEncryptedGrants(xmlNodeList2, this._relDecryptor);
			}
			this._license.InnerXml = xmlElement2.OuterXml;
		}

		// Token: 0x0400025A RID: 602
		private Type[] _inputTypes = new Type[]
		{
			typeof(XmlDocument)
		};

		// Token: 0x0400025B RID: 603
		private Type[] _outputTypes = new Type[]
		{
			typeof(XmlDocument)
		};

		// Token: 0x0400025C RID: 604
		private XmlNamespaceManager _namespaceManager;

		// Token: 0x0400025D RID: 605
		private XmlDocument _license;

		// Token: 0x0400025E RID: 606
		private IRelDecryptor _relDecryptor;

		// Token: 0x0400025F RID: 607
		private const string ElementIssuer = "issuer";

		// Token: 0x04000260 RID: 608
		private const string NamespaceUriCore = "urn:mpeg:mpeg21:2003:01-REL-R-NS";
	}
}
