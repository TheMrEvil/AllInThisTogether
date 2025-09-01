using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see cref="T:System.Security.Cryptography.DSA" /> private key of the <see langword="&lt;KeyInfo&gt;" /> element.</summary>
	// Token: 0x0200002F RID: 47
	public class DSAKeyValue : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> class with a new, randomly-generated <see cref="T:System.Security.Cryptography.DSA" /> public key.</summary>
		// Token: 0x060000F0 RID: 240 RVA: 0x000050C5 File Offset: 0x000032C5
		public DSAKeyValue()
		{
			this._key = DSA.Create();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> class with the specified <see cref="T:System.Security.Cryptography.DSA" /> public key.</summary>
		/// <param name="key">The instance of an implementation of the <see cref="T:System.Security.Cryptography.DSA" /> class that holds the public key.</param>
		// Token: 0x060000F1 RID: 241 RVA: 0x000050D8 File Offset: 0x000032D8
		public DSAKeyValue(DSA key)
		{
			this._key = key;
		}

		/// <summary>Gets or sets the key value represented by a <see cref="T:System.Security.Cryptography.DSA" /> object.</summary>
		/// <returns>The public key represented by a <see cref="T:System.Security.Cryptography.DSA" /> object.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000050E7 File Offset: 0x000032E7
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000050EF File Offset: 0x000032EF
		public DSA Key
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

		/// <summary>Returns the XML representation of a <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> element.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> element.</returns>
		// Token: 0x060000F4 RID: 244 RVA: 0x000050F8 File Offset: 0x000032F8
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000511C File Offset: 0x0000331C
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			DSAParameters dsaparameters = this._key.ExportParameters(false);
			XmlElement xmlElement = xmlDocument.CreateElement("KeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement2 = xmlDocument.CreateElement("DSAKeyValue", "http://www.w3.org/2000/09/xmldsig#");
			XmlElement xmlElement3 = xmlDocument.CreateElement("P", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement3.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.P)));
			xmlElement2.AppendChild(xmlElement3);
			XmlElement xmlElement4 = xmlDocument.CreateElement("Q", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement4.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Q)));
			xmlElement2.AppendChild(xmlElement4);
			XmlElement xmlElement5 = xmlDocument.CreateElement("G", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement5.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.G)));
			xmlElement2.AppendChild(xmlElement5);
			XmlElement xmlElement6 = xmlDocument.CreateElement("Y", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement6.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Y)));
			xmlElement2.AppendChild(xmlElement6);
			if (dsaparameters.J != null)
			{
				XmlElement xmlElement7 = xmlDocument.CreateElement("J", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement7.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.J)));
				xmlElement2.AppendChild(xmlElement7);
			}
			if (dsaparameters.Seed != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("Seed", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(dsaparameters.Seed)));
				xmlElement2.AppendChild(xmlElement8);
				XmlElement xmlElement9 = xmlDocument.CreateElement("PgenCounter", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement9.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(Utils.ConvertIntToByteArray(dsaparameters.Counter))));
				xmlElement2.AppendChild(xmlElement9);
			}
			xmlElement.AppendChild(xmlElement2);
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> state from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="value" /> parameter is not a valid <see cref="T:System.Security.Cryptography.Xml.DSAKeyValue" /> XML element.</exception>
		// Token: 0x060000F6 RID: 246 RVA: 0x000052DC File Offset: 0x000034DC
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Name != "KeyValue" || value.NamespaceURI != "http://www.w3.org/2000/09/xmldsig#")
			{
				throw new CryptographicException("Root element must be KeyValue element in namepsace http://www.w3.org/2000/09/xmldsig#");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(value.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("dsig", "http://www.w3.org/2000/09/xmldsig#");
			XmlNode xmlNode = value.SelectSingleNode("dsig:DSAKeyValue", xmlNamespaceManager);
			if (xmlNode == null)
			{
				throw new CryptographicException("KeyValue must contain child element DSAKeyValue");
			}
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("dsig:Y", xmlNamespaceManager);
			if (xmlNode2 == null)
			{
				throw new CryptographicException("Y is missing");
			}
			XmlNode xmlNode3 = xmlNode.SelectSingleNode("dsig:P", xmlNamespaceManager);
			XmlNode xmlNode4 = xmlNode.SelectSingleNode("dsig:Q", xmlNamespaceManager);
			if ((xmlNode3 == null && xmlNode4 != null) || (xmlNode3 != null && xmlNode4 == null))
			{
				throw new CryptographicException("P and Q can only occour in combination");
			}
			XmlNode xmlNode5 = xmlNode.SelectSingleNode("dsig:G", xmlNamespaceManager);
			XmlNode xmlNode6 = xmlNode.SelectSingleNode("dsig:J", xmlNamespaceManager);
			XmlNode xmlNode7 = xmlNode.SelectSingleNode("dsig:Seed", xmlNamespaceManager);
			XmlNode xmlNode8 = xmlNode.SelectSingleNode("dsig:PgenCounter", xmlNamespaceManager);
			if ((xmlNode7 == null && xmlNode8 != null) || (xmlNode7 != null && xmlNode8 == null))
			{
				throw new CryptographicException("Seed and PgenCounter can only occur in combination");
			}
			try
			{
				this.Key.ImportParameters(new DSAParameters
				{
					P = ((xmlNode3 != null) ? Convert.FromBase64String(xmlNode3.InnerText) : null),
					Q = ((xmlNode4 != null) ? Convert.FromBase64String(xmlNode4.InnerText) : null),
					G = ((xmlNode5 != null) ? Convert.FromBase64String(xmlNode5.InnerText) : null),
					Y = Convert.FromBase64String(xmlNode2.InnerText),
					J = ((xmlNode6 != null) ? Convert.FromBase64String(xmlNode6.InnerText) : null),
					Seed = ((xmlNode7 != null) ? Convert.FromBase64String(xmlNode7.InnerText) : null),
					Counter = ((xmlNode8 != null) ? Utils.ConvertByteArrayToInt(Convert.FromBase64String(xmlNode8.InnerText)) : 0)
				});
			}
			catch (Exception inner)
			{
				throw new CryptographicException("An error occurred parsing the key components", inner);
			}
		}

		// Token: 0x04000159 RID: 345
		private DSA _key;

		// Token: 0x0400015A RID: 346
		private const string KeyValueElementName = "KeyValue";

		// Token: 0x0400015B RID: 347
		private const string DSAKeyValueElementName = "DSAKeyValue";

		// Token: 0x0400015C RID: 348
		private const string PElementName = "P";

		// Token: 0x0400015D RID: 349
		private const string QElementName = "Q";

		// Token: 0x0400015E RID: 350
		private const string GElementName = "G";

		// Token: 0x0400015F RID: 351
		private const string JElementName = "J";

		// Token: 0x04000160 RID: 352
		private const string YElementName = "Y";

		// Token: 0x04000161 RID: 353
		private const string SeedElementName = "Seed";

		// Token: 0x04000162 RID: 354
		private const string PgenCounterElementName = "PgenCounter";
	}
}
