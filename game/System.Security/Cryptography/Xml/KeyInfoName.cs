using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents a <see langword="&lt;KeyName&gt;" /> subelement of an XMLDSIG or XML Encryption <see langword="&lt;KeyInfo&gt;" /> element.</summary>
	// Token: 0x02000043 RID: 67
	public class KeyInfoName : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> class.</summary>
		// Token: 0x060001B7 RID: 439 RVA: 0x000082A1 File Offset: 0x000064A1
		public KeyInfoName() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> class by specifying the string identifier that is the value of the <see langword="&lt;KeyName&gt;" /> element.</summary>
		/// <param name="keyName">The string identifier that is the value of the <see langword="&lt;KeyName&gt;" /> element.</param>
		// Token: 0x060001B8 RID: 440 RVA: 0x000082AA File Offset: 0x000064AA
		public KeyInfoName(string keyName)
		{
			this.Value = keyName;
		}

		/// <summary>Gets or sets the string identifier contained within a <see langword="&lt;KeyName&gt;" /> element.</summary>
		/// <returns>The string identifier that is the value of the <see langword="&lt;KeyName&gt;" /> element.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x000082B9 File Offset: 0x000064B9
		// (set) Token: 0x060001BA RID: 442 RVA: 0x000082C1 File Offset: 0x000064C1
		public string Value
		{
			get
			{
				return this._keyName;
			}
			set
			{
				this._keyName = value;
			}
		}

		/// <summary>Returns an XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</summary>
		/// <returns>An XML representation of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</returns>
		// Token: 0x060001BB RID: 443 RVA: 0x000082CC File Offset: 0x000064CC
		public override XmlElement GetXml()
		{
			return this.GetXml(new XmlDocument
			{
				PreserveWhitespace = true
			});
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000082ED File Offset: 0x000064ED
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			XmlElement xmlElement = xmlDocument.CreateElement("KeyName", "http://www.w3.org/2000/09/xmldsig#");
			xmlElement.AppendChild(xmlDocument.CreateTextNode(this._keyName));
			return xmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoName" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060001BD RID: 445 RVA: 0x00008314 File Offset: 0x00006514
		public override void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._keyName = value.InnerText.Trim();
		}

		// Token: 0x040001A7 RID: 423
		private string _keyName;
	}
}
