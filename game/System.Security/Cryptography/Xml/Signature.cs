using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see langword="&lt;Signature&gt;" /> element of an XML signature.</summary>
	// Token: 0x02000054 RID: 84
	public class Signature
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00009E49 File Offset: 0x00008049
		// (set) Token: 0x06000232 RID: 562 RVA: 0x00009E51 File Offset: 0x00008051
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.Signature" /> class.</summary>
		// Token: 0x06000233 RID: 563 RVA: 0x00009E5A File Offset: 0x0000805A
		public Signature()
		{
			this._embeddedObjects = new ArrayList();
			this._referencedItems = new CanonicalXmlNodeList();
		}

		/// <summary>Gets or sets the ID of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />. The default is <see langword="null" />.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00009E78 File Offset: 0x00008078
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00009E80 File Offset: 0x00008080
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

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.SignedInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00009E89 File Offset: 0x00008089
		// (set) Token: 0x06000237 RID: 567 RVA: 0x00009E91 File Offset: 0x00008091
		public SignedInfo SignedInfo
		{
			get
			{
				return this._signedInfo;
			}
			set
			{
				this._signedInfo = value;
				if (this.SignedXml != null && this._signedInfo != null)
				{
					this._signedInfo.SignedXml = this.SignedXml;
				}
			}
		}

		/// <summary>Gets or sets the value of the digital signature.</summary>
		/// <returns>A byte array that contains the value of the digital signature.</returns>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00009EBB File Offset: 0x000080BB
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00009EC3 File Offset: 0x000080C3
		public byte[] SignatureValue
		{
			get
			{
				return this._signatureValue;
			}
			set
			{
				this._signatureValue = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> of the current <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00009ECC File Offset: 0x000080CC
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00009EE7 File Offset: 0x000080E7
		public KeyInfo KeyInfo
		{
			get
			{
				if (this._keyInfo == null)
				{
					this._keyInfo = new KeyInfo();
				}
				return this._keyInfo;
			}
			set
			{
				this._keyInfo = value;
			}
		}

		/// <summary>Gets or sets a list of objects to be signed.</summary>
		/// <returns>A list of objects to be signed.</returns>
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009EF0 File Offset: 0x000080F0
		// (set) Token: 0x0600023D RID: 573 RVA: 0x00009EF8 File Offset: 0x000080F8
		public IList ObjectList
		{
			get
			{
				return this._embeddedObjects;
			}
			set
			{
				this._embeddedObjects = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009F01 File Offset: 0x00008101
		internal CanonicalXmlNodeList ReferencedItems
		{
			get
			{
				return this._referencedItems;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" />.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.Signature" />.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.Signature.SignedInfo" /> property is <see langword="null" />.  
		///  -or-  
		///  The <see cref="P:System.Security.Cryptography.Xml.Signature.SignatureValue" /> property is <see langword="null" />.</exception>
		// Token: 0x0600023F RID: 575 RVA: 0x00009F0C File Offset: 0x0000810C
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00009F30 File Offset: 0x00008130
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Signature", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._id))
			{
				xmlElement.SetAttribute("Id", this._id);
			}
			if (this._signedInfo == null)
			{
				throw new CryptographicException("Signature requires a SignedInfo.");
			}
			xmlElement.AppendChild(this._signedInfo.GetXml(document));
			if (this._signatureValue == null)
			{
				throw new CryptographicException("Signature requires a SignatureValue.");
			}
			XmlElement xmlElement2 = document.CreateElement("SignatureValue", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement2.AppendChild(document.CreateTextNode(Convert.ToBase64String(this._signatureValue)));
			if (!string.IsNullOrEmpty(this._signatureValueId))
			{
				xmlElement2.SetAttribute("Id", this._signatureValueId);
			}
			xmlElement.AppendChild(xmlElement2);
			if (this.KeyInfo.Count > 0)
			{
				xmlElement.AppendChild(this.KeyInfo.GetXml(document));
			}
			foreach (object obj in this._embeddedObjects)
			{
				DataObject dataObject = obj as DataObject;
				if (dataObject != null)
				{
					xmlElement.AppendChild(dataObject.GetXml(document));
				}
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.Signature" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.Signature" /> state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.Signature.SignatureValue" />.  
		///  -or-  
		///  The <paramref name="value" /> parameter does not contain a valid <see cref="P:System.Security.Cryptography.Xml.Signature.SignedInfo" />.</exception>
		// Token: 0x06000241 RID: 577 RVA: 0x0000A070 File Offset: 0x00008270
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!value.LocalName.Equals("Signature"))
			{
				throw new CryptographicException("Malformed element {0}.", "Signature");
			}
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, "Id"))
			{
				throw new CryptographicException("Malformed element {0}.", "Signature");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			int num = 0;
			XmlNodeList xmlNodeList = value.SelectNodes("ds:SignedInfo", xmlNamespaceManager);
			if (xmlNodeList == null || xmlNodeList.Count == 0 || xmlNodeList.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "SignedInfo");
			}
			XmlElement value2 = xmlNodeList[0] as XmlElement;
			num += xmlNodeList.Count;
			this.SignedInfo = new SignedInfo();
			this.SignedInfo.LoadXml(value2);
			XmlNodeList xmlNodeList2 = value.SelectNodes("ds:SignatureValue", xmlNamespaceManager);
			if (xmlNodeList2 == null || xmlNodeList2.Count == 0 || xmlNodeList2.Count > 1)
			{
				throw new CryptographicException("Malformed element {0}.", "SignatureValue");
			}
			XmlElement xmlElement = xmlNodeList2[0] as XmlElement;
			num += xmlNodeList2.Count;
			this._signatureValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlElement.InnerText));
			this._signatureValueId = Utils.GetAttribute(xmlElement, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(xmlElement, "Id"))
			{
				throw new CryptographicException("Malformed element {0}.", "SignatureValue");
			}
			XmlNodeList xmlNodeList3 = value.SelectNodes("ds:KeyInfo", xmlNamespaceManager);
			this._keyInfo = new KeyInfo();
			if (xmlNodeList3 != null)
			{
				if (xmlNodeList3.Count > 1)
				{
					throw new CryptographicException("Malformed element {0}.", "KeyInfo");
				}
				foreach (object obj in xmlNodeList3)
				{
					XmlElement xmlElement2 = ((XmlNode)obj) as XmlElement;
					if (xmlElement2 != null)
					{
						this._keyInfo.LoadXml(xmlElement2);
					}
				}
				num += xmlNodeList3.Count;
			}
			XmlNodeList xmlNodeList4 = value.SelectNodes("ds:Object", xmlNamespaceManager);
			this._embeddedObjects.Clear();
			if (xmlNodeList4 != null)
			{
				foreach (object obj2 in xmlNodeList4)
				{
					XmlElement xmlElement3 = ((XmlNode)obj2) as XmlElement;
					if (xmlElement3 != null)
					{
						DataObject dataObject = new DataObject();
						dataObject.LoadXml(xmlElement3);
						this._embeddedObjects.Add(dataObject);
					}
				}
				num += xmlNodeList4.Count;
			}
			XmlNodeList xmlNodeList5 = value.SelectNodes("//*[@Id]", xmlNamespaceManager);
			if (xmlNodeList5 != null)
			{
				foreach (object obj3 in xmlNodeList5)
				{
					XmlNode value3 = (XmlNode)obj3;
					this._referencedItems.Add(value3);
				}
			}
			if (value.SelectNodes("*").Count != num)
			{
				throw new CryptographicException("Malformed element {0}.", "Signature");
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> to the list of objects to be signed.</summary>
		/// <param name="dataObject">The <see cref="T:System.Security.Cryptography.Xml.DataObject" /> to be added to the list of objects to be signed.</param>
		// Token: 0x06000242 RID: 578 RVA: 0x0000A3BC File Offset: 0x000085BC
		public void AddObject(DataObject dataObject)
		{
			this._embeddedObjects.Add(dataObject);
		}

		// Token: 0x040001C9 RID: 457
		private string _id;

		// Token: 0x040001CA RID: 458
		private SignedInfo _signedInfo;

		// Token: 0x040001CB RID: 459
		private byte[] _signatureValue;

		// Token: 0x040001CC RID: 460
		private string _signatureValueId;

		// Token: 0x040001CD RID: 461
		private KeyInfo _keyInfo;

		// Token: 0x040001CE RID: 462
		private IList _embeddedObjects;

		// Token: 0x040001CF RID: 463
		private CanonicalXmlNodeList _referencedItems;

		// Token: 0x040001D0 RID: 464
		private SignedXml _signedXml;
	}
}
