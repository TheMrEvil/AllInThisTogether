using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Specifies the order of XML Digital Signature and XML Encryption operations when both are performed on the same document.</summary>
	// Token: 0x0200005E RID: 94
	public class XmlDecryptionTransform : Transform
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> class.</summary>
		// Token: 0x06000312 RID: 786 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		public XmlDecryptionTransform()
		{
			base.Algorithm = "http://www.w3.org/2002/07/decrypt#XML";
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000ED0D File Offset: 0x0000CF0D
		private ArrayList ExceptUris
		{
			get
			{
				if (this._arrayListUri == null)
				{
					this._arrayListUri = new ArrayList();
				}
				return this._arrayListUri;
			}
		}

		/// <summary>Determines whether the ID attribute of an <see cref="T:System.Xml.XmlElement" /> object matches a specified value.</summary>
		/// <param name="inputElement">An <see cref="T:System.Xml.XmlElement" /> object with an ID attribute to compare with <paramref name="idValue" />.</param>
		/// <param name="idValue">The value to compare with the ID attribute of <paramref name="inputElement" />.</param>
		/// <returns>
		///   <see langword="true" /> if the ID attribute of the <paramref name="inputElement" /> parameter matches the <paramref name="idValue" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000314 RID: 788 RVA: 0x0000ED28 File Offset: 0x0000CF28
		protected virtual bool IsTargetElement(XmlElement inputElement, string idValue)
		{
			return inputElement != null && (inputElement.GetAttribute("Id") == idValue || inputElement.GetAttribute("id") == idValue || inputElement.GetAttribute("ID") == idValue);
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that contains information about the keys necessary to decrypt an XML document.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedXml" /> object that contains information about the keys necessary to decrypt an XML document.</returns>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000ED78 File Offset: 0x0000CF78
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000EDDD File Offset: 0x0000CFDD
		public EncryptedXml EncryptedXml
		{
			get
			{
				if (this._exml != null)
				{
					return this._exml;
				}
				Reference reference = base.Reference;
				SignedXml signedXml = (reference == null) ? base.SignedXml : reference.SignedXml;
				if (signedXml == null || signedXml.EncryptedXml == null)
				{
					this._exml = new EncryptedXml(this._containingDocument);
				}
				else
				{
					this._exml = signedXml.EncryptedXml;
				}
				return this._exml;
			}
			set
			{
				this._exml = value;
			}
		}

		/// <summary>Gets an array of types that are valid inputs to the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <returns>An array of valid input types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object; you can pass only objects of one of these types to the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.LoadInput(System.Object)" /> method of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</returns>
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000EDE6 File Offset: 0x0000CFE6
		public override Type[] InputTypes
		{
			get
			{
				return this._inputTypes;
			}
		}

		/// <summary>Gets an array of types that are possible outputs from the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <returns>An array of valid output types for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object; only objects of one of these types are returned from the <see cref="M:System.Security.Cryptography.Xml.XmlDecryptionTransform.GetOutput" /> methods of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</returns>
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0000EDEE File Offset: 0x0000CFEE
		public override Type[] OutputTypes
		{
			get
			{
				return this._outputTypes;
			}
		}

		/// <summary>Adds a Uniform Resource Identifier (URI) to exclude from processing.</summary>
		/// <param name="uri">A Uniform Resource Identifier (URI) to exclude from processing</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000319 RID: 793 RVA: 0x0000EDF6 File Offset: 0x0000CFF6
		public void AddExceptUri(string uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this.ExceptUris.Add(uri);
		}

		/// <summary>Parses the specified <see cref="T:System.Xml.XmlNodeList" /> object as transform-specific content of a <see langword="&lt;Transform&gt;" /> element and configures the internal state of the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object to match the <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <param name="nodeList">An <see cref="T:System.Xml.XmlNodeList" /> object that specifies transform-specific content for the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="nodeList" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The Uniform Resource Identifier (URI) value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> was not found.  
		///  -or-  
		///  The length of the URI value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> is 0.  
		///  -or-  
		///  The first character of the URI value of an <see cref="T:System.Xml.XmlNode" /> object in <paramref name="nodeList" /> is not '#'.</exception>
		// Token: 0x0600031A RID: 794 RVA: 0x0000EE14 File Offset: 0x0000D014
		public override void LoadInnerXml(XmlNodeList nodeList)
		{
			if (nodeList == null)
			{
				throw new CryptographicException("Unknown transform has been encountered.");
			}
			this.ExceptUris.Clear();
			foreach (object obj in nodeList)
			{
				XmlElement xmlElement = ((XmlNode)obj) as XmlElement;
				if (xmlElement != null)
				{
					if (!(xmlElement.LocalName == "Except") || !(xmlElement.NamespaceURI == "http://www.w3.org/2002/07/decrypt#"))
					{
						throw new CryptographicException("Unknown transform has been encountered.");
					}
					string attribute = Utils.GetAttribute(xmlElement, "URI", "http://www.w3.org/2002/07/decrypt#");
					if (attribute == null || attribute.Length == 0 || attribute[0] != '#')
					{
						throw new CryptographicException("A Uri attribute is required for a CipherReference element.");
					}
					if (!Utils.VerifyAttributes(xmlElement, "URI"))
					{
						throw new CryptographicException("Unknown transform has been encountered.");
					}
					string value = Utils.ExtractIdFromLocalUri(attribute);
					this.ExceptUris.Add(value);
				}
			}
		}

		/// <summary>Returns an XML representation of the parameters of an <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object that are suitable to be included as subelements of an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</summary>
		/// <returns>A list of the XML nodes that represent the transform-specific content needed to describe the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object in an XMLDSIG <see langword="&lt;Transform&gt;" /> element.</returns>
		// Token: 0x0600031B RID: 795 RVA: 0x0000EF20 File Offset: 0x0000D120
		protected override XmlNodeList GetInnerXml()
		{
			if (this.ExceptUris.Count == 0)
			{
				return null;
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement("Transform", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(base.Algorithm))
			{
				xmlElement.SetAttribute("Algorithm", base.Algorithm);
			}
			foreach (object obj in this.ExceptUris)
			{
				string value = (string)obj;
				XmlElement xmlElement2 = xmlDocument.CreateElement("Except", "http://www.w3.org/2002/07/decrypt#");
				xmlElement2.SetAttribute("URI", value);
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement.ChildNodes;
		}

		/// <summary>When overridden in a derived class, loads the specified input into the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</summary>
		/// <param name="obj">The input to load into the current <see cref="T:System.Security.Cryptography.Xml.XmlDecryptionTransform" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="obj" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600031C RID: 796 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		public override void LoadInput(object obj)
		{
			if (obj is Stream)
			{
				this.LoadStreamInput((Stream)obj);
				return;
			}
			if (obj is XmlDocument)
			{
				this.LoadXmlDocumentInput((XmlDocument)obj);
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000F014 File Offset: 0x0000D214
		private void LoadStreamInput(Stream stream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			XmlResolver xmlResolver = base.ResolverSet ? this._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), base.BaseURI);
			XmlReader reader = Utils.PreProcessStreamInput(stream, xmlResolver, base.BaseURI);
			xmlDocument.Load(reader);
			this._containingDocument = xmlDocument;
			this._nsm = new XmlNamespaceManager(this._containingDocument.NameTable);
			this._nsm.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this._encryptedDataList = xmlDocument.SelectNodes("//enc:EncryptedData", this._nsm);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		private void LoadXmlDocumentInput(XmlDocument document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this._containingDocument = document;
			this._nsm = new XmlNamespaceManager(document.NameTable);
			this._nsm.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			this._encryptedDataList = document.SelectNodes("//enc:EncryptedData", this._nsm);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F110 File Offset: 0x0000D310
		private void ReplaceEncryptedData(XmlElement encryptedDataElement, byte[] decrypted)
		{
			XmlNode parentNode = encryptedDataElement.ParentNode;
			if (parentNode.NodeType == XmlNodeType.Document)
			{
				parentNode.InnerXml = this.EncryptedXml.Encoding.GetString(decrypted);
				return;
			}
			this.EncryptedXml.ReplaceData(encryptedDataElement, decrypted);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F154 File Offset: 0x0000D354
		private bool ProcessEncryptedDataItem(XmlElement encryptedDataElement)
		{
			if (this.ExceptUris.Count > 0)
			{
				for (int i = 0; i < this.ExceptUris.Count; i++)
				{
					if (this.IsTargetElement(encryptedDataElement, (string)this.ExceptUris[i]))
					{
						return false;
					}
				}
			}
			EncryptedData encryptedData = new EncryptedData();
			encryptedData.LoadXml(encryptedDataElement);
			SymmetricAlgorithm decryptionKey = this.EncryptedXml.GetDecryptionKey(encryptedData, null);
			if (decryptionKey == null)
			{
				throw new CryptographicException("Unable to retrieve the decryption key.");
			}
			byte[] decrypted = this.EncryptedXml.DecryptData(encryptedData, decryptionKey);
			this.ReplaceEncryptedData(encryptedDataElement, decrypted);
			return true;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000F1E4 File Offset: 0x0000D3E4
		private void ProcessElementRecursively(XmlNodeList encryptedDatas)
		{
			if (encryptedDatas == null || encryptedDatas.Count == 0)
			{
				return;
			}
			Queue queue = new Queue();
			foreach (object obj in encryptedDatas)
			{
				XmlNode obj2 = (XmlNode)obj;
				queue.Enqueue(obj2);
			}
			for (XmlNode xmlNode = queue.Dequeue() as XmlNode; xmlNode != null; xmlNode = (queue.Dequeue() as XmlNode))
			{
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null && xmlElement.LocalName == "EncryptedData" && xmlElement.NamespaceURI == "http://www.w3.org/2001/04/xmlenc#")
				{
					XmlNode nextSibling = xmlElement.NextSibling;
					XmlNode parentNode = xmlElement.ParentNode;
					if (this.ProcessEncryptedDataItem(xmlElement))
					{
						XmlNode xmlNode2 = parentNode.FirstChild;
						while (xmlNode2 != null && xmlNode2.NextSibling != nextSibling)
						{
							xmlNode2 = xmlNode2.NextSibling;
						}
						if (xmlNode2 != null)
						{
							XmlNodeList xmlNodeList = xmlNode2.SelectNodes("//enc:EncryptedData", this._nsm);
							if (xmlNodeList.Count > 0)
							{
								foreach (object obj3 in xmlNodeList)
								{
									XmlNode obj4 = (XmlNode)obj3;
									queue.Enqueue(obj4);
								}
							}
						}
					}
				}
				if (queue.Count == 0)
				{
					break;
				}
			}
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">A decryption key could not be found.</exception>
		// Token: 0x06000322 RID: 802 RVA: 0x0000F360 File Offset: 0x0000D560
		public override object GetOutput()
		{
			if (this._encryptedDataList != null)
			{
				this.ProcessElementRecursively(this._encryptedDataList);
			}
			Utils.AddNamespaces(this._containingDocument.DocumentElement, base.PropagatedNamespaces);
			return this._containingDocument;
		}

		/// <summary>Returns the output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</summary>
		/// <param name="type">The type of the output to return. <see cref="T:System.Xml.XmlNodeList" /> is the only valid type for this parameter.</param>
		/// <returns>The output of the current <see cref="T:System.Security.Cryptography.Xml.XmlDsigEnvelopedSignatureTransform" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not an <see cref="T:System.Xml.XmlNodeList" /> object.</exception>
		// Token: 0x06000323 RID: 803 RVA: 0x0000F392 File Offset: 0x0000D592
		public override object GetOutput(Type type)
		{
			if (type == typeof(XmlDocument))
			{
				return (XmlDocument)this.GetOutput();
			}
			throw new ArgumentException("The input type was invalid for this transform.", "type");
		}

		// Token: 0x04000234 RID: 564
		private Type[] _inputTypes = new Type[]
		{
			typeof(Stream),
			typeof(XmlDocument)
		};

		// Token: 0x04000235 RID: 565
		private Type[] _outputTypes = new Type[]
		{
			typeof(XmlDocument)
		};

		// Token: 0x04000236 RID: 566
		private XmlNodeList _encryptedDataList;

		// Token: 0x04000237 RID: 567
		private ArrayList _arrayListUri;

		// Token: 0x04000238 RID: 568
		private EncryptedXml _exml;

		// Token: 0x04000239 RID: 569
		private XmlDocument _containingDocument;

		// Token: 0x0400023A RID: 570
		private XmlNamespaceManager _nsm;

		// Token: 0x0400023B RID: 571
		private const string XmlDecryptionTransformNamespaceUrl = "http://www.w3.org/2002/07/decrypt#";
	}
}
