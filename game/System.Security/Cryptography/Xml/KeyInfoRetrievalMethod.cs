using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>References <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> objects stored at a different location when using XMLDSIG or XML encryption.</summary>
	// Token: 0x02000045 RID: 69
	public class KeyInfoRetrievalMethod : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class.</summary>
		// Token: 0x060001C5 RID: 453 RVA: 0x00008215 File Offset: 0x00006415
		public KeyInfoRetrievalMethod()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class with the specified Uniform Resource Identifier (URI) pointing to the referenced <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</summary>
		/// <param name="strUri">The Uniform Resource Identifier (URI) of the information to be referenced by the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" />.</param>
		// Token: 0x060001C6 RID: 454 RVA: 0x00008399 File Offset: 0x00006599
		public KeyInfoRetrievalMethod(string strUri)
		{
			this._uri = strUri;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> class with the specified Uniform Resource Identifier (URI) pointing to the referenced <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object and the URI that describes the type of data to retrieve.</summary>
		/// <param name="strUri">The Uniform Resource Identifier (URI) of the information to be referenced by the new instance of <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" />.</param>
		/// <param name="typeName">The URI that describes the type of data to retrieve.</param>
		// Token: 0x060001C7 RID: 455 RVA: 0x000083A8 File Offset: 0x000065A8
		public KeyInfoRetrievalMethod(string strUri, string typeName)
		{
			this._uri = strUri;
			this._type = typeName;
		}

		/// <summary>Gets or sets the Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</summary>
		/// <returns>The Uniform Resource Identifier (URI) of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x000083BE File Offset: 0x000065BE
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x000083C6 File Offset: 0x000065C6
		public string Uri
		{
			get
			{
				return this._uri;
			}
			set
			{
				this._uri = value;
			}
		}

		/// <summary>Gets or sets a Uniform Resource Identifier (URI) that describes the type of data to be retrieved.</summary>
		/// <returns>A Uniform Resource Identifier (URI) that describes the type of data to be retrieved.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001CA RID: 458 RVA: 0x000083CF File Offset: 0x000065CF
		// (set) Token: 0x060001CB RID: 459 RVA: 0x000083D7 File Offset: 0x000065D7
		public string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</returns>
		// Token: 0x060001CC RID: 460 RVA: 0x000083E0 File Offset: 0x000065E0
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008404 File Offset: 0x00006604
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("RetrievalMethod", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._uri))
			{
				xmlElement.SetAttribute("URI", this._uri);
			}
			if (!string.IsNullOrEmpty(this._type))
			{
				xmlElement.SetAttribute("Type", this._type);
			}
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object to match.</summary>
		/// <param name="value">The XML element that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoRetrievalMethod" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060001CE RID: 462 RVA: 0x0000845F File Offset: 0x0000665F
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._uri = Utils.GetAttribute(value, "URI", "http://www.w3.org/2000/09/xmldsig#");
			this._type = Utils.GetAttribute(value, "Type", "http://www.w3.org/2000/09/xmldsig#");
		}

		// Token: 0x040001A9 RID: 425
		private string _uri;

		// Token: 0x040001AA RID: 426
		private string _type;
	}
}
