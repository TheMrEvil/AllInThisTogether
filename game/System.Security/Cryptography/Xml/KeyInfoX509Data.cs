using System;
using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents an <see langword="&lt;X509Data&gt;" /> subelement of an XMLDSIG or XML Encryption <see langword="&lt;KeyInfo&gt;" /> element.</summary>
	// Token: 0x02000046 RID: 70
	public class KeyInfoX509Data : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class.</summary>
		// Token: 0x060001CF RID: 463 RVA: 0x00008215 File Offset: 0x00006415
		public KeyInfoX509Data()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified ASN.1 DER encoding of an X.509v3 certificate.</summary>
		/// <param name="rgbCert">The ASN.1 DER encoding of an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		// Token: 0x060001D0 RID: 464 RVA: 0x0000849C File Offset: 0x0000669C
		public KeyInfoX509Data(byte[] rgbCert)
		{
			X509Certificate2 certificate = new X509Certificate2(rgbCert);
			this.AddCertificate(certificate);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified X.509v3 certificate.</summary>
		/// <param name="cert">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060001D1 RID: 465 RVA: 0x000084BD File Offset: 0x000066BD
		public KeyInfoX509Data(X509Certificate cert)
		{
			this.AddCertificate(cert);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> class from the specified X.509v3 certificate.</summary>
		/// <param name="cert">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> from.</param>
		/// <param name="includeOption">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509IncludeOption" /> values that specifies how much of the certificate chain to include.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate has only a partial certificate chain.</exception>
		// Token: 0x060001D2 RID: 466 RVA: 0x000084CC File Offset: 0x000066CC
		public KeyInfoX509Data(X509Certificate cert, X509IncludeOption includeOption)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			X509Certificate2 certificate = new X509Certificate2(cert);
			switch (includeOption)
			{
			case X509IncludeOption.ExcludeRoot:
			{
				X509Chain x509Chain = new X509Chain();
				x509Chain.Build(certificate);
				if (x509Chain.ChainStatus.Length != 0 && (x509Chain.ChainStatus[0].Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
				{
					throw new CryptographicException("A certificate chain could not be built to a trusted root authority.");
				}
				X509ChainElementCollection chainElements = x509Chain.ChainElements;
				for (int i = 0; i < (Utils.IsSelfSigned(x509Chain) ? 1 : (chainElements.Count - 1)); i++)
				{
					this.AddCertificate(chainElements[i].Certificate);
				}
				return;
			}
			case X509IncludeOption.EndCertOnly:
				this.AddCertificate(certificate);
				return;
			case X509IncludeOption.WholeChain:
			{
				X509Chain x509Chain = new X509Chain();
				x509Chain.Build(certificate);
				if (x509Chain.ChainStatus.Length != 0 && (x509Chain.ChainStatus[0].Status & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.PartialChain)
				{
					throw new CryptographicException("A certificate chain could not be built to a trusted root authority.");
				}
				X509ChainElementCollection chainElements = x509Chain.ChainElements;
				foreach (X509ChainElement x509ChainElement in chainElements)
				{
					this.AddCertificate(x509ChainElement.Certificate);
				}
				return;
			}
			default:
				return;
			}
		}

		/// <summary>Gets a list of the X.509v3 certificates contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the X.509 certificates contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x000085FD File Offset: 0x000067FD
		public ArrayList Certificates
		{
			get
			{
				return this._certificates;
			}
		}

		/// <summary>Adds the specified X.509v3 certificate to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" />.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060001D4 RID: 468 RVA: 0x00008608 File Offset: 0x00006808
		public void AddCertificate(X509Certificate certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (this._certificates == null)
			{
				this._certificates = new ArrayList();
			}
			X509Certificate2 value = new X509Certificate2(certificate);
			this._certificates.Add(value);
		}

		/// <summary>Gets a list of the subject key identifiers (SKIs) contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the subject key identifiers (SKIs) contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000864A File Offset: 0x0000684A
		public ArrayList SubjectKeyIds
		{
			get
			{
				return this._subjectKeyIds;
			}
		}

		/// <summary>Adds the specified subject key identifier (SKI) byte array to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectKeyId">A byte array that represents the subject key identifier (SKI) to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		// Token: 0x060001D6 RID: 470 RVA: 0x00008652 File Offset: 0x00006852
		public void AddSubjectKeyId(byte[] subjectKeyId)
		{
			if (this._subjectKeyIds == null)
			{
				this._subjectKeyIds = new ArrayList();
			}
			this._subjectKeyIds.Add(subjectKeyId);
		}

		/// <summary>Adds the specified subject key identifier (SKI) string to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectKeyId">A string that represents the subject key identifier (SKI) to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		// Token: 0x060001D7 RID: 471 RVA: 0x00008674 File Offset: 0x00006874
		public void AddSubjectKeyId(string subjectKeyId)
		{
			if (this._subjectKeyIds == null)
			{
				this._subjectKeyIds = new ArrayList();
			}
			this._subjectKeyIds.Add(Utils.DecodeHexString(subjectKeyId));
		}

		/// <summary>Gets a list of the subject names of the entities contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>A list of the subject names of the entities contained in the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000869B File Offset: 0x0000689B
		public ArrayList SubjectNames
		{
			get
			{
				return this._subjectNames;
			}
		}

		/// <summary>Adds the subject name of the entity that was issued an X.509v3 certificate to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="subjectName">The name of the entity that was issued an X.509 certificate to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		// Token: 0x060001D9 RID: 473 RVA: 0x000086A3 File Offset: 0x000068A3
		public void AddSubjectName(string subjectName)
		{
			if (this._subjectNames == null)
			{
				this._subjectNames = new ArrayList();
			}
			this._subjectNames.Add(subjectName);
		}

		/// <summary>Gets a list of <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> structures that represent an issuer name and serial number pair.</summary>
		/// <returns>A list of <see cref="T:System.Security.Cryptography.Xml.X509IssuerSerial" /> structures that represent an issuer name and serial number pair.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000086C5 File Offset: 0x000068C5
		public ArrayList IssuerSerials
		{
			get
			{
				return this._issuerSerials;
			}
		}

		/// <summary>Adds the specified issuer name and serial number pair to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <param name="issuerName">The issuer name portion of the pair to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		/// <param name="serialNumber">The serial number portion of the pair to add to the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		// Token: 0x060001DB RID: 475 RVA: 0x000086D0 File Offset: 0x000068D0
		public void AddIssuerSerial(string issuerName, string serialNumber)
		{
			if (string.IsNullOrEmpty(issuerName))
			{
				throw new ArgumentException("String cannot be empty or null.", "issuerName");
			}
			if (string.IsNullOrEmpty(serialNumber))
			{
				throw new ArgumentException("String cannot be empty or null.", "serialNumber");
			}
			BigInteger bigInteger;
			if (!BigInteger.TryParse(serialNumber, NumberStyles.AllowHexSpecifier, NumberFormatInfo.CurrentInfo, out bigInteger))
			{
				throw new ArgumentException("X509 issuer serial number is invalid.", "serialNumber");
			}
			if (this._issuerSerials == null)
			{
				this._issuerSerials = new ArrayList();
			}
			this._issuerSerials.Add(Utils.CreateX509IssuerSerial(issuerName, bigInteger.ToString()));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008768 File Offset: 0x00006968
		internal void InternalAddIssuerSerial(string issuerName, string serialNumber)
		{
			if (this._issuerSerials == null)
			{
				this._issuerSerials = new ArrayList();
			}
			this._issuerSerials.Add(Utils.CreateX509IssuerSerial(issuerName, serialNumber));
		}

		/// <summary>Gets or sets the Certificate Revocation List (CRL) contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>The Certificate Revocation List (CRL) contained within the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00008795 File Offset: 0x00006995
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000879D File Offset: 0x0000699D
		public byte[] CRL
		{
			get
			{
				return this._CRL;
			}
			set
			{
				this._CRL = value;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000087A8 File Offset: 0x000069A8
		private void Clear()
		{
			this._CRL = null;
			if (this._subjectKeyIds != null)
			{
				this._subjectKeyIds.Clear();
			}
			if (this._subjectNames != null)
			{
				this._subjectNames.Clear();
			}
			if (this._issuerSerials != null)
			{
				this._issuerSerials.Clear();
			}
			if (this._certificates != null)
			{
				this._certificates.Clear();
			}
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</returns>
		// Token: 0x060001E0 RID: 480 RVA: 0x00008808 File Offset: 0x00006A08
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000882C File Offset: 0x00006A2C
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("X509Data", "http://www.w3.org/2000/09/xmldsig#");
			if (this._issuerSerials != null)
			{
				foreach (object obj in this._issuerSerials)
				{
					X509IssuerSerial x509IssuerSerial = (X509IssuerSerial)obj;
					XmlElement xmlElement2 = xmlDocument.CreateElement("X509IssuerSerial", "http://www.w3.org/2000/09/xmldsig#");
					XmlElement xmlElement3 = xmlDocument.CreateElement("X509IssuerName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement3.AppendChild(xmlDocument.CreateTextNode(x509IssuerSerial.IssuerName));
					xmlElement2.AppendChild(xmlElement3);
					XmlElement xmlElement4 = xmlDocument.CreateElement("X509SerialNumber", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement4.AppendChild(xmlDocument.CreateTextNode(x509IssuerSerial.SerialNumber));
					xmlElement2.AppendChild(xmlElement4);
					xmlElement.AppendChild(xmlElement2);
				}
			}
			if (this._subjectKeyIds != null)
			{
				foreach (object obj2 in this._subjectKeyIds)
				{
					byte[] inArray = (byte[])obj2;
					XmlElement xmlElement5 = xmlDocument.CreateElement("X509SKI", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement5.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(inArray)));
					xmlElement.AppendChild(xmlElement5);
				}
			}
			if (this._subjectNames != null)
			{
				foreach (object obj3 in this._subjectNames)
				{
					string text = (string)obj3;
					XmlElement xmlElement6 = xmlDocument.CreateElement("X509SubjectName", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement6.AppendChild(xmlDocument.CreateTextNode(text));
					xmlElement.AppendChild(xmlElement6);
				}
			}
			if (this._certificates != null)
			{
				foreach (object obj4 in this._certificates)
				{
					X509Certificate x509Certificate = (X509Certificate)obj4;
					XmlElement xmlElement7 = xmlDocument.CreateElement("X509Certificate", "http://www.w3.org/2000/09/xmldsig#");
					xmlElement7.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(x509Certificate.GetRawCertData())));
					xmlElement.AppendChild(xmlElement7);
				}
			}
			if (this._CRL != null)
			{
				XmlElement xmlElement8 = xmlDocument.CreateElement("X509CRL", "http://www.w3.org/2000/09/xmldsig#");
				xmlElement8.AppendChild(xmlDocument.CreateTextNode(Convert.ToBase64String(this._CRL)));
				xmlElement.AppendChild(xmlElement8);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object to match.</summary>
		/// <param name="element">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoX509Data" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="element" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="element" /> parameter does not contain an &lt;<see langword="X509IssuerName" />&gt; node.  
		///  -or-  
		///  The <paramref name="element" /> parameter does not contain an &lt;<see langword="X509SerialNumber" />&gt; node.</exception>
		// Token: 0x060001E2 RID: 482 RVA: 0x00008AD0 File Offset: 0x00006CD0
		public override void LoadXml(XmlElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(element.OwnerDocument.NameTable);
			xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
			XmlNodeList xmlNodeList = element.SelectNodes("ds:X509IssuerSerial", xmlNamespaceManager);
			XmlNodeList xmlNodeList2 = element.SelectNodes("ds:X509SKI", xmlNamespaceManager);
			XmlNodeList xmlNodeList3 = element.SelectNodes("ds:X509SubjectName", xmlNamespaceManager);
			XmlNodeList xmlNodeList4 = element.SelectNodes("ds:X509Certificate", xmlNamespaceManager);
			XmlNodeList xmlNodeList5 = element.SelectNodes("ds:X509CRL", xmlNamespaceManager);
			if (xmlNodeList5.Count == 0 && xmlNodeList.Count == 0 && xmlNodeList2.Count == 0 && xmlNodeList3.Count == 0 && xmlNodeList4.Count == 0)
			{
				throw new CryptographicException("Malformed element {0}.", "X509Data");
			}
			this.Clear();
			if (xmlNodeList5.Count != 0)
			{
				this._CRL = Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNodeList5.Item(0).InnerText));
			}
			foreach (object obj in xmlNodeList)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode xmlNode2 = xmlNode.SelectSingleNode("ds:X509IssuerName", xmlNamespaceManager);
				XmlNode xmlNode3 = xmlNode.SelectSingleNode("ds:X509SerialNumber", xmlNamespaceManager);
				if (xmlNode2 == null || xmlNode3 == null)
				{
					throw new CryptographicException("Malformed element {0}.", "IssuerSerial");
				}
				this.InternalAddIssuerSerial(xmlNode2.InnerText.Trim(), xmlNode3.InnerText.Trim());
			}
			foreach (object obj2 in xmlNodeList2)
			{
				XmlNode xmlNode4 = (XmlNode)obj2;
				this.AddSubjectKeyId(Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNode4.InnerText)));
			}
			foreach (object obj3 in xmlNodeList3)
			{
				XmlNode xmlNode5 = (XmlNode)obj3;
				this.AddSubjectName(xmlNode5.InnerText.Trim());
			}
			foreach (object obj4 in xmlNodeList4)
			{
				XmlNode xmlNode6 = (XmlNode)obj4;
				this.AddCertificate(new X509Certificate2(Convert.FromBase64String(Utils.DiscardWhiteSpaces(xmlNode6.InnerText))));
			}
		}

		// Token: 0x040001AB RID: 427
		private ArrayList _certificates;

		// Token: 0x040001AC RID: 428
		private ArrayList _issuerSerials;

		// Token: 0x040001AD RID: 429
		private ArrayList _subjectKeyIds;

		// Token: 0x040001AE RID: 430
		private ArrayList _subjectNames;

		// Token: 0x040001AF RID: 431
		private byte[] _CRL;
	}
}
