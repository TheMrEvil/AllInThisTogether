using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the &lt;<see langword="RSAKeyValue" />&gt; element of an XML signature.</summary>
	// Token: 0x0200004B RID: 75
	public class RSAKeyValue : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.RSAKeyValue" /> class with a new randomly generated <see cref="T:System.Security.Cryptography.RSA" /> public key.</summary>
		// Token: 0x060001F0 RID: 496 RVA: 0x00008E7B File Offset: 0x0000707B
		public RSAKeyValue()
		{
			this._key = RSA.Create();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.RSAKeyValue" /> class with the specified <see cref="T:System.Security.Cryptography.RSA" /> public key.</summary>
		/// <param name="key">The instance of an implementation of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</param>
		// Token: 0x060001F1 RID: 497 RVA: 0x00008E8E File Offset: 0x0000708E
		public RSAKeyValue(RSA key)
		{
			this._key = key;
		}

		/// <summary>Gets or sets the instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</summary>
		/// <returns>The instance of <see cref="T:System.Security.Cryptography.RSA" /> that holds the public key.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00008E9D File Offset: 0x0000709D
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00008EA5 File Offset: 0x000070A5
		public RSA Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</returns>
		// Token: 0x060001F4 RID: 500 RVA: 0x00008EB0 File Offset: 0x000070B0
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008ED4 File Offset: 0x000070D4
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			RSAParameters rsaparameters = this._key.ExportParameters(false);
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement2 = xmlDocument.CreateElement("RSAKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement3 = xmlDocument.CreateElement("Modulus", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(rsaparameters.Modulus)));
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Exponent", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(rsaparameters.Exponent)));
			xmlElement2.AppendChild(xmlElement4);
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		/// <summary>Loads an <see cref="T:System.Security.Cryptography.RSA" /> key clause from an XML element.</summary>
		/// <param name="value">The XML element from which to load the <see cref="T:System.Security.Cryptography.RSA" /> key clause.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.RSA" /> key clause XML element.</exception>
		// Token: 0x060001F6 RID: 502 RVA: 0x00008F7C File Offset: 0x0000717C
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "KeyValue" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException("Root element must be KeyValue element in namespace http://www.w3.org/2000/09/xmldsig#");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode("dsig:RSAKeyValue", xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new CryptographicException("KeyValue must contain child element RSAKeyValue");
			}
			try
			{
				this.Key.ImportParameters(new RSAParameters
				{
					Modulus = Convert.FromBase64String(xmlNode.SelectSingleNode("dsig:Modulus", xmlNamespaceManager).InnerText),
					Exponent = Convert.FromBase64String(xmlNode.SelectSingleNode("dsig:Exponent", xmlNamespaceManager).InnerText)
				});
			}
			catch (Exception inner)
			{
				throw new CryptographicException("An error occurred parsing the Modulus and Exponent elements", inner);
			}
		}

		// Token: 0x040001B2 RID: 434
		private RSA _key;

		// Token: 0x040001B3 RID: 435
		private const string KeyValueElementName = "KeyValue";

		// Token: 0x040001B4 RID: 436
		private const string RSAKeyValueElementName = "RSAKeyValue";

		// Token: 0x040001B5 RID: 437
		private const string ModulusElementName = "Modulus";

		// Token: 0x040001B6 RID: 438
		private const string ExponentElementName = "Exponent";
	}
}
