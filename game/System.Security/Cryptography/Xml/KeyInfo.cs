using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents an XML digital signature or XML encryption <see langword="&lt;KeyInfo&gt;" /> element.</summary>
	// Token: 0x02000040 RID: 64
	public class KeyInfo : IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> class.</summary>
		// Token: 0x060001A2 RID: 418 RVA: 0x00007F75 File Offset: 0x00006175
		public KeyInfo()
		{
			this._keyInfoClauses = new ArrayList();
		}

		/// <summary>Gets or sets the key information identity.</summary>
		/// <returns>The key information identity.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00007F88 File Offset: 0x00006188
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00007F90 File Offset: 0x00006190
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

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		// Token: 0x060001A5 RID: 421 RVA: 0x00007F9C File Offset: 0x0000619C
		public XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00007FC0 File Offset: 0x000061C0
		internal XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyInfo", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._id))
			{
				xmlElement.SetAttribute("Id", this._id);
			}
			for (int i = 0; i < this._keyInfoClauses.Count; i++)
			{
				XmlElement xml = ((KeyInfoClause)this._keyInfoClauses[i]).GetXml(xmlDocument);
				if (xml != null)
				{
					xmlElement.AppendChild(xml);
				}
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> state from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> state.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060001A7 RID: 423 RVA: 0x00008038 File Offset: 0x00006238
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			if (!Utils.VerifyAttributes(value, "Id"))
			{
				throw new CryptographicException("Malformed element {0}.", "KeyInfo");
			}
			for (XmlNode xmlNode = value.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				XmlElement xmlElement = xmlNode as XmlElement;
				if (xmlElement != null)
				{
					string text = xmlElement.NamespaceURI + " " + xmlElement.LocalName;
					if (text == "http://www.w3.org/2000/09/xmldsig# KeyValue")
					{
						if (!Utils.VerifyAttributes(xmlElement, null))
						{
							throw new CryptographicException("Malformed element {0}.", "KeyInfo/KeyValue");
						}
						foreach (object obj in xmlElement.ChildNodes)
						{
							XmlElement xmlElement2 = ((XmlNode)obj) as XmlElement;
							if (xmlElement2 != null)
							{
								text = text + "/" + xmlElement2.LocalName;
								break;
							}
						}
					}
					KeyInfoClause keyInfoClause = CryptoHelpers.CreateFromName<KeyInfoClause>(text);
					if (keyInfoClause == null)
					{
						keyInfoClause = new KeyInfoNode();
					}
					keyInfoClause.LoadXml(xmlElement);
					this.AddClause(keyInfoClause);
				}
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>The number of <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000817C File Offset: 0x0000637C
		public int Count
		{
			get
			{
				return this._keyInfoClauses.Count;
			}
		}

		/// <summary>Adds a <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> that represents a particular type of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> information to the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="clause">The <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</param>
		// Token: 0x060001A9 RID: 425 RVA: 0x00008189 File Offset: 0x00006389
		public void AddClause(KeyInfoClause clause)
		{
			this._keyInfoClauses.Add(clause);
		}

		/// <summary>Returns an enumerator of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060001AA RID: 426 RVA: 0x00008198 File Offset: 0x00006398
		public IEnumerator GetEnumerator()
		{
			return this._keyInfoClauses.GetEnumerator();
		}

		/// <summary>Returns an enumerator of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoClause" /> objects of the specified type in the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="requestedObjectType">The type of object to enumerate.</param>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> that can be used to iterate through the collection.</returns>
		// Token: 0x060001AB RID: 427 RVA: 0x000081A8 File Offset: 0x000063A8
		public IEnumerator GetEnumerator(Type requestedObjectType)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._keyInfoClauses)
			{
				if (requestedObjectType.Equals(obj.GetType()))
				{
					arrayList.Add(obj);
				}
			}
			return arrayList.GetEnumerator();
		}

		// Token: 0x040001A4 RID: 420
		private string _id;

		// Token: 0x040001A5 RID: 421
		private ArrayList _keyInfoClauses;
	}
}
