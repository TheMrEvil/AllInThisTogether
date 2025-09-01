using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see langword="&lt;EncryptedData&gt;" /> element in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x02000034 RID: 52
	public sealed class EncryptedData : EncryptedType
	{
		/// <summary>Loads XML information into the <see langword="&lt;EncryptedData&gt;" /> element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object representing an XML element to use for the <see langword="&lt;EncryptedData&gt;" /> element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter does not contain a &lt;<see langword="CypherData" />&gt; node.</exception>
		// Token: 0x0600010C RID: 268 RVA: 0x000058DC File Offset: 0x00003ADC
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			this.Id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2001/04/xmlenc#");
			this.Type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2001/04/xmlenc#");
			this.MimeType = Utils.GetAttribute(value, "MimeType", "http://www.w3.org/2001/04/xmlenc#");
			this.Encoding = Utils.GetAttribute(value, "Encoding", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:EncryptionMethod", xmlNamespaceManager);
			this.EncryptionMethod = new EncryptionMethod();
			if (xmlNode != null)
			{
				this.EncryptionMethod.LoadXml(xmlNode as XmlElement);
			}
			base.KeyInfo = new KeyInfo();
			XmlNode xmlNode2 = value.SelectSingleNode("ds:KeyInfo", xmlNamespaceManager);
			if (xmlNode2 != null)
			{
				base.KeyInfo.LoadXml(xmlNode2 as XmlElement);
			}
			XmlNode xmlNode3 = value.SelectSingleNode("enc:CipherData", xmlNamespaceManager);
			if (xmlNode3 == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			this.CipherData = new CipherData();
			this.CipherData.LoadXml(xmlNode3 as XmlElement);
			XmlNode xmlNode4 = value.SelectSingleNode("enc:EncryptionProperties", xmlNamespaceManager);
			if (xmlNode4 != null)
			{
				XmlNodeList xmlNodeList = xmlNode4.SelectNodes("enc:EncryptionProperty", xmlNamespaceManager);
				if (xmlNodeList != null)
				{
					foreach (object obj in xmlNodeList)
					{
						XmlNode xmlNode5 = (XmlNode)obj;
						EncryptionProperty encryptionProperty = new EncryptionProperty();
						encryptionProperty.LoadXml(xmlNode5 as XmlElement);
						this.EncryptionProperties.Add(encryptionProperty);
					}
				}
			}
			this._cachedXml = value;
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> that represents the <see langword="&lt;EncryptedData&gt;" /> element in XML encryption.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> value is <see langword="null" />.</exception>
		// Token: 0x0600010D RID: 269 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public override XmlElement GetXml()
		{
			if (base.CacheValid)
			{
				return this._cachedXml;
			}
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00005AD8 File Offset: 0x00003CD8
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("EncryptedData", "http://www.w3.org/2001/04/xmlenc#");
			if (!string.IsNullOrEmpty(this.Id))
			{
				xmlElement.SetAttribute("Id", this.Id);
			}
			if (!string.IsNullOrEmpty(this.Type))
			{
				xmlElement.SetAttribute("Type", this.Type);
			}
			if (!string.IsNullOrEmpty(this.MimeType))
			{
				xmlElement.SetAttribute("MimeType", this.MimeType);
			}
			if (!string.IsNullOrEmpty(this.Encoding))
			{
				xmlElement.SetAttribute("Encoding", this.Encoding);
			}
			if (this.EncryptionMethod != null)
			{
				xmlElement.AppendChild(this.EncryptionMethod.GetXml(document));
			}
			if (base.KeyInfo.Count > 0)
			{
				xmlElement.AppendChild(base.KeyInfo.GetXml(document));
			}
			if (this.CipherData == null)
			{
				throw new CryptographicException("Cipher data is not specified.");
			}
			xmlElement.AppendChild(this.CipherData.GetXml(document));
			if (this.EncryptionProperties.Count > 0)
			{
				XmlElement xmlElement2 = document.CreateElement("EncryptionProperties", "http://www.w3.org/2001/04/xmlenc#");
				for (int i = 0; i < this.EncryptionProperties.Count; i++)
				{
					EncryptionProperty encryptionProperty = this.EncryptionProperties.Item(i);
					xmlElement2.AppendChild(encryptionProperty.GetXml(document));
				}
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlElement;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> class.</summary>
		// Token: 0x0600010F RID: 271 RVA: 0x00005C29 File Offset: 0x00003E29
		public EncryptedData()
		{
		}
	}
}
