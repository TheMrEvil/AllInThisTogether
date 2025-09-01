using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see langword="&lt;CipherData&gt;" /> element in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x0200002B RID: 43
	public sealed class CipherData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class.</summary>
		// Token: 0x060000D6 RID: 214 RVA: 0x00002145 File Offset: 0x00000345
		public CipherData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class using a byte array as the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> value.</summary>
		/// <param name="cipherValue">The encrypted data to use for the <see langword="&lt;CipherValue&gt;" /> element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cipherValue" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property has already been set.</exception>
		// Token: 0x060000D7 RID: 215 RVA: 0x000047DC File Offset: 0x000029DC
		public CipherData(byte[] cipherValue)
		{
			this.CipherValue = cipherValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> class using a <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</summary>
		/// <param name="cipherReference">The <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object to use.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cipherValue" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property has already been set.</exception>
		// Token: 0x060000D8 RID: 216 RVA: 0x000047EB File Offset: 0x000029EB
		public CipherData(CipherReference cipherReference)
		{
			this.CipherReference = cipherReference;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000047FA File Offset: 0x000029FA
		private bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Gets or sets the <see langword="&lt;CipherReference&gt;" /> element.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherReference" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property was set to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property was set more than once.</exception>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004805 File Offset: 0x00002A05
		// (set) Token: 0x060000DB RID: 219 RVA: 0x0000480D File Offset: 0x00002A0D
		public CipherReference CipherReference
		{
			get
			{
				return this._cipherReference;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.CipherValue != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this._cipherReference = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="&lt;CipherValue&gt;" /> element.</summary>
		/// <returns>A byte array that represents the <see langword="&lt;CipherValue&gt;" /> element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property was set to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property was set more than once.</exception>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000DC RID: 220 RVA: 0x0000483E File Offset: 0x00002A3E
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004846 File Offset: 0x00002A46
		public byte[] CipherValue
		{
			get
			{
				return this._cipherValue;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this.CipherReference != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this._cipherValue = (byte[])value.Clone();
				this._cachedXml = null;
			}
		}

		/// <summary>Gets the XML values for the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlElement" /> object that represents the XML information for the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property and the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property are <see langword="null" />.</exception>
		// Token: 0x060000DE RID: 222 RVA: 0x00004884 File Offset: 0x00002A84
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

		// Token: 0x060000DF RID: 223 RVA: 0x000048B4 File Offset: 0x00002AB4
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("CipherData", "http://www.w3.org/2001/04/xmlenc#");
			if (this.CipherValue != null)
			{
				XmlElement xmlElement2 = document.CreateElement("CipherValue", "http://www.w3.org/2001/04/xmlenc#");
				xmlElement2.AppendChild(document.CreateTextNode(Convert.ToBase64String(this.CipherValue)));
				xmlElement.AppendChild(xmlElement2);
			}
			else
			{
				if (this.CipherReference == null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				xmlElement.AppendChild(this.CipherReference.GetXml(document));
			}
			return xmlElement;
		}

		/// <summary>Loads XML data from an <see cref="T:System.Xml.XmlElement" /> into a <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> that represents the XML data to load.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherValue" /> property and the <see cref="P:System.Security.Cryptography.Xml.CipherData.CipherReference" /> property are <see langword="null" />.</exception>
		// Token: 0x060000E0 RID: 224 RVA: 0x00004934 File Offset: 0x00002B34
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("enc", "http://www.w3.org/2001/04/xmlenc#");
			XmlNode xmlNode = value.SelectSingleNode("enc:CipherValue", xmlNamespaceManager);
			XmlNode xmlNode2 = value.SelectSingleNode("enc:CipherReference", xmlNamespaceManager);
			if (xmlNode != null)
			{
				if (xmlNode2 != null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this._cipherValue = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNode.InnerText));
			}
			else
			{
				if (xmlNode2 == null)
				{
					throw new CryptographicException("A Cipher Data element should have either a CipherValue or a CipherReference element.");
				}
				this._cipherReference = new CipherReference();
				this._cipherReference.LoadXml((XmlElement)xmlNode2);
			}
			this._cachedXml = value;
		}

		// Token: 0x04000154 RID: 340
		private XmlElement _cachedXml;

		// Token: 0x04000155 RID: 341
		private CipherReference _cipherReference;

		// Token: 0x04000156 RID: 342
		private byte[] _cipherValue;
	}
}
