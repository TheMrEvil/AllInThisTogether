using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class used in XML encryption from which the <see cref="T:System.Security.Cryptography.Xml.CipherReference" />, <see cref="T:System.Security.Cryptography.Xml.KeyReference" />, and <see cref="T:System.Security.Cryptography.Xml.DataReference" /> classes derive.</summary>
	// Token: 0x02000036 RID: 54
	public abstract class EncryptedReference
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class.</summary>
		// Token: 0x0600011B RID: 283 RVA: 0x000061E4 File Offset: 0x000043E4
		protected EncryptedReference() : this(string.Empty, new TransformChain())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class using the specified Uniform Resource Identifier (URI).</summary>
		/// <param name="uri">The Uniform Resource Identifier (URI) that points to the data to encrypt.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600011C RID: 284 RVA: 0x000061F6 File Offset: 0x000043F6
		protected EncryptedReference(string uri) : this(uri, new TransformChain())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> class using the specified Uniform Resource Identifier (URI) and transform chain.</summary>
		/// <param name="uri">The Uniform Resource Identifier (URI) that points to the data to encrypt.</param>
		/// <param name="transformChain">A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms to be done on the data to encrypt.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="uri" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600011D RID: 285 RVA: 0x00006204 File Offset: 0x00004404
		protected EncryptedReference(string uri, TransformChain transformChain)
		{
			this.TransformChain = transformChain;
			this.Uri = uri;
			this._cachedXml = null;
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>The Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedReference.Uri" /> property was set to <see langword="null" />.</exception>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006221 File Offset: 0x00004421
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00006229 File Offset: 0x00004429
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("A Uri attribute is required for a CipherReference element.");
				}
				this._uri = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the transform chain of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.TransformChain" /> object that describes transforms used on the encrypted data.</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006247 File Offset: 0x00004447
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00006262 File Offset: 0x00004462
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

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to the current transform chain of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <param name="transform">A <see cref="T:System.Security.Cryptography.Xml.Transform" /> object to add to the transform chain.</param>
		// Token: 0x06000122 RID: 290 RVA: 0x00006272 File Offset: 0x00004472
		public void AddTransform(Transform transform)
		{
			this.TransformChain.Add(transform);
		}

		/// <summary>Gets or sets a reference type.</summary>
		/// <returns>The reference type of the encrypted data.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006280 File Offset: 0x00004480
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00006288 File Offset: 0x00004488
		protected string ReferenceType
		{
			get
			{
				return this._referenceType;
			}
			set
			{
				this._referenceType = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets a value that indicates whether the cache is valid.</summary>
		/// <returns>
		///   <see langword="true" /> if the cache is valid; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006298 File Offset: 0x00004498
		protected internal bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Returns the XML representation of an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the values of the <see langword="&lt;EncryptedReference&gt;" /> element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedReference.ReferenceType" /> property is <see langword="null" />.</exception>
		// Token: 0x06000126 RID: 294 RVA: 0x000062A4 File Offset: 0x000044A4
		public virtual XmlElement GetXml()
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

		// Token: 0x06000127 RID: 295 RVA: 0x000062D4 File Offset: 0x000044D4
		internal XmlElement GetXml(XmlDocument document)
		{
			if (this.ReferenceType == null)
			{
				throw new CryptographicException("The Reference type must be set in an EncryptedReference object.");
			}
			XmlElement xmlElement = document.CreateElement(this.ReferenceType, "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(this._uri))
			{
				xmlElement.SetAttribute("URI", this._uri);
			}
			if (this.TransformChain.Count > 0)
			{
				xmlElement.AppendChild(this.TransformChain.GetXml(document, "http://www.w3.org/2000/09/xmldsig#"));
			}
			return xmlElement;
		}

		/// <summary>Loads an XML element into an <see cref="T:System.Security.Cryptography.Xml.EncryptedReference" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object that represents an XML element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000128 RID: 296 RVA: 0x0000634C File Offset: 0x0000454C
		public virtual void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.ReferenceType = value.LocalName;
			string attribute = Utils.GetAttribute(value, "URI", "http://www.w3.org/2001/04/xmlenc#");
			if (attribute == null)
			{
				throw new ArgumentNullException("A Uri attribute is required for a CipherReference element.");
			}
			this.Uri = attribute;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode("ds:Transforms", xmlNamespaceManager);
			if (xmlNode != null)
			{
				this.TransformChain.LoadXml(xmlNode as XmlElement);
			}
			this._cachedXml = value;
		}

		// Token: 0x04000170 RID: 368
		private string _uri;

		// Token: 0x04000171 RID: 369
		private string _referenceType;

		// Token: 0x04000172 RID: 370
		private TransformChain _transformChain;

		// Token: 0x04000173 RID: 371
		internal XmlElement _cachedXml;
	}
}
