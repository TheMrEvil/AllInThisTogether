using System;
using System.Collections;
using System.Globalization;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Contains information about the canonicalization algorithm and signature algorithm used for the XML signature.</summary>
	// Token: 0x02000055 RID: 85
	public class SignedInfo : ICollection, IEnumerable
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000A3CB File Offset: 0x000085CB
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000A3D3 File Offset: 0x000085D3
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> class.</summary>
		// Token: 0x06000245 RID: 581 RVA: 0x0000A3DC File Offset: 0x000085DC
		public SignedInfo()
		{
			this._references = new ArrayList();
		}

		/// <summary>Returns an enumerator that iterates through the collection of references.</summary>
		/// <returns>An enumerator that iterates through the collection of references.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06000246 RID: 582 RVA: 0x0000A3EF File Offset: 0x000085EF
		public IEnumerator GetEnumerator()
		{
			throw new NotSupportedException();
		}

		/// <summary>Copies the elements of this instance into an <see cref="T:System.Array" /> object, starting at a specified index in the array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> object that holds the collection's elements.</param>
		/// <param name="index">The beginning index in the array where the elements are copied.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not supported.</exception>
		// Token: 0x06000247 RID: 583 RVA: 0x0000A3EF File Offset: 0x000085EF
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the number of references in the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The number of references in the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A3EF File Offset: 0x000085EF
		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the collection is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is read-only; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000A3EF File Offset: 0x000085EF
		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the collection is synchronized.</summary>
		/// <returns>
		///   <see langword="true" /> if the collection is synchronized; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A3EF File Offset: 0x000085EF
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets an object to use for synchronization.</summary>
		/// <returns>An object to use for synchronization.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported.</exception>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000A3EF File Offset: 0x000085EF
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A3F6 File Offset: 0x000085F6
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000A3FE File Offset: 0x000085FE
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the canonicalization algorithm that is used before signing for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The canonicalization algorithm used before signing for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A40E File Offset: 0x0000860E
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000A424 File Offset: 0x00008624
		public string CanonicalizationMethod
		{
			get
			{
				if (this._canonicalizationMethod == null)
				{
					return "http://www.w3.org/TR/2001/REC-xml-c14n-20010315";
				}
				return this._canonicalizationMethod;
			}
			set
			{
				this._canonicalizationMethod = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.Xml.Transform" /> object used for canonicalization.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.Transform" /> object used for canonicalization.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <see cref="T:System.Security.Cryptography.Xml.Transform" /> is <see langword="null" />.</exception>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A434 File Offset: 0x00008634
		public Transform CanonicalizationMethodObject
		{
			get
			{
				if (this._canonicalizationMethodTransform == null)
				{
					this._canonicalizationMethodTransform = CryptoHelpers.CreateFromName<Transform>(this.CanonicalizationMethod);
					if (this._canonicalizationMethodTransform == null)
					{
						throw new CryptographicException(string.Format(CultureInfo.CurrentCulture, "Could not create the XML transformation identified by the URI {0}.", this.CanonicalizationMethod));
					}
					this._canonicalizationMethodTransform.SignedXml = this.SignedXml;
					this._canonicalizationMethodTransform.Reference = null;
				}
				return this._canonicalizationMethodTransform;
			}
		}

		/// <summary>Gets or sets the name of the algorithm used for signature generation and validation for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The name of the algorithm used for signature generation and validation for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000A4A0 File Offset: 0x000086A0
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public string SignatureMethod
		{
			get
			{
				return this._signatureMethod;
			}
			set
			{
				this._signatureMethod = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The length of the signature for the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000A4B8 File Offset: 0x000086B8
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000A4C0 File Offset: 0x000086C0
		public string SignatureLength
		{
			get
			{
				return this._signatureLength;
			}
			set
			{
				this._signatureLength = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets a list of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> objects of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>A list of the <see cref="T:System.Security.Cryptography.Xml.Reference" /> elements of the current <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000A4D0 File Offset: 0x000086D0
		public ArrayList References
		{
			get
			{
				return this._references;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000A4D8 File Offset: 0x000086D8
		internal bool CacheValid
		{
			get
			{
				if (this._cachedXml == null)
				{
					return false;
				}
				using (IEnumerator enumerator = this.References.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!((Reference)enumerator.Current).CacheValid)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> instance.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.SignedInfo.SignatureMethod" /> property is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.SignedInfo.References" /> property is empty.</exception>
		// Token: 0x06000257 RID: 599 RVA: 0x0000A544 File Offset: 0x00008744
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

		// Token: 0x06000258 RID: 600 RVA: 0x0000A574 File Offset: 0x00008774
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("SignedInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._id))
			{
				xmlElement.SetAttribute("Id", this._id);
			}
			XmlElement xml = this.CanonicalizationMethodObject.GetXml(document, "CanonicalizationMethod");
			xmlElement.AppendChild(xml);
			if (string.IsNullOrEmpty(this._signatureMethod))
			{
				throw new CryptographicException("A signature method is required.");
			}
			XmlElement xmlElement2 = document.CreateElement("SignatureMethod", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.SetAttribute("Algorithm", this._signatureMethod);
			if (this._signatureLength != null)
			{
				XmlElement xmlElement3 = document.CreateElement(null, "HMACOutputLength", "http://www.w3.org/2000/09/xmldsig#");
				XmlText newChild = document.CreateTextNode(this._signatureLength);
				xmlElement3.AppendChild(newChild);
				xmlElement2.AppendChild(xmlElement3);
			}
			xmlElement.AppendChild(xmlElement2);
			if (this._references.Count == 0)
			{
				throw new CryptographicException("At least one Reference element is required.");
			}
			for (int i = 0; i < this._references.Count; i++)
			{
				Reference reference = (Reference)this._references[i];
				xmlElement.AppendChild(reference.GetXml(document));
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> element.  
		///  -or-  
		///  The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedInfo.CanonicalizationMethod" /> property.  
		///  -or-  
		///  The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.SignedInfo.SignatureMethod" /> property.</exception>
		// Token: 0x06000259 RID: 601 RVA: 0x0000A69C File Offset: 0x0000889C
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!value.LocalName.Equals("SignedInfo"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			int num = 0;
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, "Id"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo");
			}
			XmlNodeList xmlNodeList = value.SelectNodes("ds:CanonicalizationMethod", xmlNamespaceManager);
			if (xmlNodeList == null || xmlNodeList.Count == 0 || xmlNodeList.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo/CanonicalizationMethod");
			}
			XmlElement xmlElement = xmlNodeList.Item(0) as XmlElement;
			num += xmlNodeList.Count;
			this._canonicalizationMethod = Utils.GetAttribute(xmlElement, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if (this._canonicalizationMethod == null || !Utils.VerifyAttributes(xmlElement, "Algorithm"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo/CanonicalizationMethod");
			}
			this._canonicalizationMethodTransform = null;
			if (xmlElement.ChildNodes.Count > 0)
			{
				this.CanonicalizationMethodObject.LoadInnerXml(xmlElement.ChildNodes);
			}
			XmlNodeList xmlNodeList2 = value.SelectNodes("ds:SignatureMethod", xmlNamespaceManager);
			if (xmlNodeList2 == null || xmlNodeList2.Count == 0 || xmlNodeList2.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo/SignatureMethod");
			}
			XmlElement xmlElement2 = xmlNodeList2.Item(0) as XmlElement;
			num += xmlNodeList2.Count;
			this._signatureMethod = Utils.GetAttribute(xmlElement2, "Algorithm", "http://www.w3.org/2000/09/xmldsig#");
			if (this._signatureMethod == null || !Utils.VerifyAttributes(xmlElement2, "Algorithm"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo/SignatureMethod");
			}
			XmlElement xmlElement3 = xmlElement2.SelectSingleNode("ds:HMACOutputLength", xmlNamespaceManager) as XmlElement;
			if (xmlElement3 != null)
			{
				this._signatureLength = xmlElement3.InnerXml;
			}
			this._references.Clear();
			XmlNodeList xmlNodeList3 = value.SelectNodes("ds:Reference", xmlNamespaceManager);
			if (xmlNodeList3 != null)
			{
				if (xmlNodeList3.Count > 100)
				{
					throw new CryptographicException("Malformed element {0}.", "SignedInfo/Reference");
				}
				foreach (object obj in xmlNodeList3)
				{
					XmlElement value2 = ((XmlNode)obj) as XmlElement;
					Reference reference = new Reference();
					this.AddReference(reference);
					reference.LoadXml(value2);
				}
				num += xmlNodeList3.Count;
				if (value.SelectNodes("*").Count != num)
				{
					throw new CryptographicException("Malformed element {0}.", "SignedInfo");
				}
			}
			this._cachedXml = value;
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.Reference" /> object to the list of references to digest and sign.</summary>
		/// <param name="reference">The reference to add to the list of references.</param>
		/// <exception cref="T:System.ArgumentNullException">The reference parameter is <see langword="null" />.</exception>
		// Token: 0x0600025A RID: 602 RVA: 0x0000A964 File Offset: 0x00008B64
		public void AddReference(Reference reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			reference.SignedXml = this.SignedXml;
			this._references.Add(reference);
		}

		// Token: 0x040001D1 RID: 465
		private string _id;

		// Token: 0x040001D2 RID: 466
		private string _canonicalizationMethod;

		// Token: 0x040001D3 RID: 467
		private string _signatureMethod;

		// Token: 0x040001D4 RID: 468
		private string _signatureLength;

		// Token: 0x040001D5 RID: 469
		private ArrayList _references;

		// Token: 0x040001D6 RID: 470
		private XmlElement _cachedXml;

		// Token: 0x040001D7 RID: 471
		private SignedXml _signedXml;

		// Token: 0x040001D8 RID: 472
		private Transform _canonicalizationMethodTransform;
	}
}
