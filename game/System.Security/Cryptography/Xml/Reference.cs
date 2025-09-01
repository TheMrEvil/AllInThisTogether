using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see langword="&lt;reference&gt;" /> element of an XML signature.</summary>
	// Token: 0x02000051 RID: 81
	public class Reference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with default properties.</summary>
		// Token: 0x06000203 RID: 515 RVA: 0x0000915F File Offset: 0x0000735F
		public Reference()
		{
			this._transformChain = new TransformChain();
			this._refTarget = null;
			this._refTargetType = ReferenceTargetType.UriReference;
			this._cachedXml = null;
			this._digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with a hash value of the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> with which to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.Reference" />.</param>
		// Token: 0x06000204 RID: 516 RVA: 0x00009192 File Offset: 0x00007392
		public Reference(Stream stream)
		{
			this._transformChain = new TransformChain();
			this._refTarget = stream;
			this._refTargetType = ReferenceTargetType.Stream;
			this._cachedXml = null;
			this._digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> class with the specified <see cref="T:System.Uri" />.</summary>
		/// <param name="uri">The <see cref="T:System.Uri" /> with which to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.Reference" />.</param>
		// Token: 0x06000205 RID: 517 RVA: 0x000091C5 File Offset: 0x000073C5
		public Reference(string uri)
		{
			this._transformChain = new TransformChain();
			this._refTarget = uri;
			this._uri = uri;
			this._refTargetType = ReferenceTargetType.UriReference;
			this._cachedXml = null;
			this._digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000091FF File Offset: 0x000073FF
		internal Reference(XmlElement element)
		{
			this._transformChain = new TransformChain();
			this._refTarget = element;
			this._refTargetType = ReferenceTargetType.XmlElement;
			this._cachedXml = null;
			this._digestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00009232 File Offset: 0x00007432
		// (set) Token: 0x06000208 RID: 520 RVA: 0x0000923A File Offset: 0x0000743A
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Uri" /> of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The <see cref="T:System.Uri" /> of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00009243 File Offset: 0x00007443
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000924B File Offset: 0x0000744B
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the type of the object being signed.</summary>
		/// <returns>The type of the object being signed.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000925B File Offset: 0x0000745B
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00009263 File Offset: 0x00007463
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the digest method Uniform Resource Identifier (URI) of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The digest method URI of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />. The default value is "http://www.w3.org/2000/09/xmldsig#sha1".</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009273 File Offset: 0x00007473
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000927B File Offset: 0x0000747B
		public string DigestMethod
		{
			get
			{
				return this._digestMethod;
			}
			set
			{
				this._digestMethod = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the digest value of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The digest value of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000928B File Offset: 0x0000748B
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00009293 File Offset: 0x00007493
		public byte[] DigestValue
		{
			get
			{
				return this._digestValue;
			}
			set
			{
				this._digestValue = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets the transform chain of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The transform chain of the current <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000092A3 File Offset: 0x000074A3
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000092BE File Offset: 0x000074BE
		public TransformChain TransformChain
		{
			get
			{
				if (this._transformChain == null)
				{
					this._transformChain = new TransformChain();
				}
				return this._transformChain;
			}
			set
			{
				this._transformChain = value;
				this._cachedXml = null;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000213 RID: 531 RVA: 0x000092CE File Offset: 0x000074CE
		internal bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000092D9 File Offset: 0x000074D9
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000092E1 File Offset: 0x000074E1
		internal SignedXml SignedXml
		{
			get
			{
				return this._signedXml;
			}
			set
			{
				this._signedXml = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000092EA File Offset: 0x000074EA
		internal ReferenceTargetType ReferenceTargetType
		{
			get
			{
				return this._refTargetType;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.Reference" />.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Reference" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.Reference.DigestMethod" /> property is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.Reference.DigestValue" /> property is <see langword="null" />.</exception>
		// Token: 0x06000217 RID: 535 RVA: 0x000092F4 File Offset: 0x000074F4
		public XmlElement GetXml()
		{
			if (this.CacheValid)
			{
				return this._cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009324 File Offset: 0x00007524
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Reference", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._id))
			{
				xmlElement.SetAttribute("Id", this._id);
			}
			if (this._uri != null)
			{
				xmlElement.SetAttribute("URI", this._uri);
			}
			if (!string.IsNullOrEmpty(this._type))
			{
				xmlElement.SetAttribute("Type", this._type);
			}
			if (this.TransformChain.Count != 0)
			{
				xmlElement.AppendChild(this.TransformChain.GetXml(document, "http://www.w3.org/2000/09/xmldsig#"));
			}
			if (string.IsNullOrEmpty(this._digestMethod))
			{
				throw new CryptographicException("A DigestMethod must be specified on a Reference prior to generating XML.");
			}
			XmlElement xmlElement2 = document.CreateElement("DigestMethod", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.SetAttribute("Algorithm", this._digestMethod);
			xmlElement.AppendChild(xmlElement2);
			if (this.DigestValue == null)
			{
				if (this._hashAlgorithm.Hash == null)
				{
					throw new CryptographicException("A Reference must contain a DigestValue.");
				}
				this.DigestValue = this._hashAlgorithm.Hash;
			}
			XmlElement xmlElement3 = document.CreateElement("DigestValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(document.CreateTextNode(Convert.ToBase64String(this._digestValue)));
			xmlElement.AppendChild(xmlElement3);
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.Reference" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.Reference" /> state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain any transforms.  
		///  -or-  
		///  The <paramref name="value" /> parameter contains an unknown transform.</exception>
		// Token: 0x06000219 RID: 537 RVA: 0x00009464 File Offset: 0x00007664
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			this._uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			this._type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, new string[]
			{
				"Id",
				"URI",
				"Type"
			}))
			{
				throw new CryptographicException("Malformed element {0}.", "Reference");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			bool flag = false;
			this.TransformChain = new TransformChain();
			XmlNodeList xmlNodeList = value.SelectNodes("ds:Transforms", xmlNamespaceManager);
			if (xmlNodeList != null && xmlNodeList.Count != 0)
			{
				if (xmlNodeList.Count > 1)
				{
					throw new CryptographicException("Malformed element {0}.", "Reference/Transforms");
				}
				flag = true;
				XmlElement xmlElement = xmlNodeList[0] as XmlElement;
				if (!Utils.VerifyAttributes(xmlElement, null))
				{
					throw new CryptographicException("Malformed element {0}.", "Reference/Transforms");
				}
				XmlNodeList xmlNodeList2 = xmlElement.SelectNodes("ds:Transform", xmlNamespaceManager);
				if (xmlNodeList2 != null)
				{
					if (xmlNodeList2.Count != xmlElement.SelectNodes("*").Count)
					{
						throw new CryptographicException("Malformed element {0}.", "Reference/Transforms");
					}
					if (xmlNodeList2.Count > 10)
					{
						throw new CryptographicException("Malformed element {0}.", "Reference/Transforms");
					}
					foreach (object obj in xmlNodeList2)
					{
						XmlElement xmlElement2 = ((XmlNode)obj) as XmlElement;
						string attribute = Utils.GetAttribute(xmlElement2, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
						if (attribute == null || !Utils.VerifyAttributes(xmlElement2, "Algorithm"))
						{
							throw new CryptographicException("Unknown transform has been encountered.");
						}
						Transform transform = CryptoHelpers.CreateFromName<Transform>(attribute);
						if (transform == null)
						{
							throw new CryptographicException("Unknown transform has been encountered.");
						}
						this.AddTransform(transform);
						transform.LoadInnerXml(xmlElement2.ChildNodes);
						if (transform is XmlDsigEnvelopedSignatureTransform)
						{
							XmlNode xmlNode = xmlElement2.SelectSingleNode("ancestor::ds:Signature[1]", xmlNamespaceManager);
							XmlNodeList xmlNodeList3 = xmlElement2.SelectNodes("//ds:Signature", xmlNamespaceManager);
							if (xmlNodeList3 != null)
							{
								int num = 0;
								foreach (object obj2 in xmlNodeList3)
								{
									XmlNode xmlNode2 = (XmlNode)obj2;
									num++;
									if (xmlNode2 == xmlNode)
									{
										((XmlDsigEnvelopedSignatureTransform)transform).SignaturePosition = num;
										break;
									}
								}
							}
						}
					}
				}
			}
			XmlNodeList xmlNodeList4 = value.SelectNodes("ds:DigestMethod", xmlNamespaceManager);
			if (xmlNodeList4 == null || xmlNodeList4.Count == 0 || xmlNodeList4.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestMethod");
			}
			XmlElement element = xmlNodeList4[0] as XmlElement;
			this._digestMethod = Utils.GetAttribute(element, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if (this._digestMethod == null || !Utils.VerifyAttributes(element, "Algorithm"))
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestMethod");
			}
			XmlNodeList xmlNodeList5 = value.SelectNodes("ds:DigestValue", xmlNamespaceManager);
			if (xmlNodeList5 == null || xmlNodeList5.Count == 0 || xmlNodeList5.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestValue");
			}
			XmlElement xmlElement3 = xmlNodeList5[0] as XmlElement;
			this._digestValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlElement3.InnerText));
			if (!Utils.VerifyAttributes(xmlElement3, null))
			{
				throw new CryptographicException("Malformed element {0}.", "Reference/DigestValue");
			}
			int num2 = flag ? 3 : 2;
			if (value.SelectNodes("*").Count != num2)
			{
				throw new CryptographicException("Malformed element {0}.", "Reference");
			}
			this._cachedXml = value;
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to the list of transforms to be performed on the data before passing it to the digest algorithm.</summary>
		/// <param name="transform">The transform to be added to the list of transforms.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="transform" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600021A RID: 538 RVA: 0x00009840 File Offset: 0x00007A40
		public void AddTransform(Transform transform)
		{
			if (transform == null)
			{
				throw new ArgumentNullException("transform");
			}
			transform.Reference = this;
			this.TransformChain.Add(transform);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009863 File Offset: 0x00007A63
		internal void UpdateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			this.DigestValue = this.CalculateHashValue(document, refList);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009874 File Offset: 0x00007A74
		internal byte[] CalculateHashValue(XmlDocument document, CanonicalXmlNodeList refList)
		{
			this._hashAlgorithm = CryptoHelpers.CreateFromName<HashAlgorithm>(this._digestMethod);
			if (this._hashAlgorithm == null)
			{
				throw new CryptographicException("Could not create hash algorithm object.");
			}
			string text = (document == null) ? (Environment.CurrentDirectory + "\\") : document.BaseURI;
			Stream stream = null;
			WebResponse webResponse = null;
			Stream stream2 = null;
			XmlResolver xmlResolver = null;
			byte[] result = null;
			try
			{
				switch (this._refTargetType)
				{
				case ReferenceTargetType.Stream:
					xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = this.TransformChain.TransformToOctetStream((Stream)this._refTarget, xmlResolver, text);
					break;
				case ReferenceTargetType.XmlElement:
					xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
					stream = this.TransformChain.TransformToOctetStream(Utils.PreProcessElementInput((XmlElement)this._refTarget, xmlResolver, text), xmlResolver, text);
					break;
				case ReferenceTargetType.UriReference:
					if (this._uri == null)
					{
						xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						stream = this.TransformChain.TransformToOctetStream(null, xmlResolver, text);
					}
					else if (this._uri.Length == 0)
					{
						if (document == null)
						{
							throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "An XmlDocument context is required to resolve the Reference Uri {0}.", this._uri));
						}
						xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
						XmlDocument document2 = Utils.DiscardComments(Utils.PreProcessDocumentInput(document, xmlResolver, text));
						stream = this.TransformChain.TransformToOctetStream(document2, xmlResolver, text);
					}
					else
					{
						if (this._uri[0] != '#')
						{
							throw new CryptographicException("Unable to resolve Uri {0}.", this._uri);
						}
						bool flag = true;
						string idFromLocalUri = Utils.GetIdFromLocalUri(this._uri, out flag);
						if (idFromLocalUri == "xpointer(/)")
						{
							if (document == null)
							{
								throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "An XmlDocument context is required to resolve the Reference Uri {0}.", this._uri));
							}
							xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
							stream = this.TransformChain.TransformToOctetStream(Utils.PreProcessDocumentInput(document, xmlResolver, text), xmlResolver, text);
						}
						else
						{
							XmlElement xmlElement = this.SignedXml.GetIdElement(document, idFromLocalUri);
							if (xmlElement != null)
							{
								this._namespaces = Utils.GetPropagatedAttributes(xmlElement.ParentNode as XmlElement);
							}
							if (xmlElement == null && refList != null)
							{
								foreach (object obj in refList)
								{
									XmlElement xmlElement2 = ((XmlNode)obj) as XmlElement;
									if (xmlElement2 != null && Utils.HasAttribute(xmlElement2, "Id", "http://www.w3.org/2000/09/xmldsig#") && Utils.GetAttribute(xmlElement2, "Id", "http://www.w3.org/2000/09/xmldsig#").Equals(idFromLocalUri))
									{
										xmlElement = xmlElement2;
										if (this._signedXml._context != null)
										{
											this._namespaces = Utils.GetPropagatedAttributes(this._signedXml._context);
											break;
										}
										break;
									}
								}
							}
							if (xmlElement == null)
							{
								throw new CryptographicException("Malformed reference element.");
							}
							XmlDocument xmlDocument = Utils.PreProcessElementInput(xmlElement, xmlResolver, text);
							Utils.AddNamespaces(xmlDocument.DocumentElement, this._namespaces);
							xmlResolver = (this.SignedXml.ResolverSet ? this.SignedXml._xmlResolver : new XmlSecureResolver(new XmlUrlResolver(), text));
							if (flag)
							{
								XmlDocument document3 = Utils.DiscardComments(xmlDocument);
								stream = this.TransformChain.TransformToOctetStream(document3, xmlResolver, text);
							}
							else
							{
								stream = this.TransformChain.TransformToOctetStream(xmlDocument, xmlResolver, text);
							}
						}
					}
					break;
				default:
					throw new CryptographicException("Unable to resolve Uri {0}.", this._uri);
				}
				stream = SignedXmlDebugLog.LogReferenceData(this, stream);
				result = this._hashAlgorithm.ComputeHash(stream);
			}
			finally
			{
				if (stream != null)
				{
					stream.Close();
				}
				if (webResponse != null)
				{
					webResponse.Close();
				}
				if (stream2 != null)
				{
					stream2.Close();
				}
			}
			return result;
		}

		// Token: 0x040001B7 RID: 439
		internal const string DefaultDigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";

		// Token: 0x040001B8 RID: 440
		private string _id;

		// Token: 0x040001B9 RID: 441
		private string _uri;

		// Token: 0x040001BA RID: 442
		private string _type;

		// Token: 0x040001BB RID: 443
		private TransformChain _transformChain;

		// Token: 0x040001BC RID: 444
		private string _digestMethod;

		// Token: 0x040001BD RID: 445
		private byte[] _digestValue;

		// Token: 0x040001BE RID: 446
		private HashAlgorithm _hashAlgorithm;

		// Token: 0x040001BF RID: 447
		private object _refTarget;

		// Token: 0x040001C0 RID: 448
		private ReferenceTargetType _refTargetType;

		// Token: 0x040001C1 RID: 449
		private XmlElement _cachedXml;

		// Token: 0x040001C2 RID: 450
		private SignedXml _signedXml;

		// Token: 0x040001C3 RID: 451
		internal CanonicalXmlNodeList _namespaces;
	}
}
