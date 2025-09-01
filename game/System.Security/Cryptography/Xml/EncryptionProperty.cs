using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the <see langword="&lt;EncryptionProperty&gt;" /> element used in XML encryption. This class cannot be inherited.</summary>
	// Token: 0x0200003A RID: 58
	public sealed class EncryptionProperty
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</summary>
		// Token: 0x0600016C RID: 364 RVA: 0x00002145 File Offset: 0x00000345
		public EncryptionProperty()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class using an <see cref="T:System.Xml.XmlElement" /> object.</summary>
		/// <param name="elementProperty">An <see cref="T:System.Xml.XmlElement" /> object to use for initialization.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="elementProperty" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the <paramref name="elementProperty" /> parameter is not "EncryptionProperty".  
		///  -or-  
		///  The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the <paramref name="elementProperty" /> parameter is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x0600016D RID: 365 RVA: 0x00007784 File Offset: 0x00005984
		public EncryptionProperty(XmlElement elementProperty)
		{
			if (elementProperty == null)
			{
				throw new ArgumentNullException("elementProperty");
			}
			if (elementProperty.LocalName != "EncryptionProperty" || elementProperty.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed encryption property element.");
			}
			this._elemProp = elementProperty;
			this._cachedXml = null;
		}

		/// <summary>Gets the ID of the current <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>The ID of the current <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000077E2 File Offset: 0x000059E2
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		/// <summary>Gets the target of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>The target of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000077EA File Offset: 0x000059EA
		public string Target
		{
			get
			{
				return this._target;
			}
		}

		/// <summary>Gets or sets an <see cref="T:System.Xml.XmlElement" /> object that represents an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents an <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property was set to <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the value set to the <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property is not "EncryptionProperty".  
		///  -or-  
		///  The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the value set to the <see cref="P:System.Security.Cryptography.Xml.EncryptionProperty.PropertyElement" /> property is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000077F2 File Offset: 0x000059F2
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000077FC File Offset: 0x000059FC
		public XmlElement PropertyElement
		{
			get
			{
				return this._elemProp;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
				{
					throw new CryptographicException("Malformed encryption property element.");
				}
				this._elemProp = value;
				this._cachedXml = null;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007854 File Offset: 0x00005A54
		private bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that encapsulates an instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> class.</returns>
		// Token: 0x06000173 RID: 371 RVA: 0x00007860 File Offset: 0x00005A60
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

		// Token: 0x06000174 RID: 372 RVA: 0x00007890 File Offset: 0x00005A90
		internal XmlElement GetXml(XmlDocument document)
		{
			return document.ImportNode(this._elemProp, true) as XmlElement;
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object to match.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object to parse.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Xml.XmlElement.LocalName" /> property of the <paramref name="value" /> parameter is not "EncryptionProperty".  
		///  -or-  
		///  The <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property of the <paramref name="value" /> parameter is not "http://www.w3.org/2001/04/xmlenc#".</exception>
		// Token: 0x06000175 RID: 373 RVA: 0x000078A4 File Offset: 0x00005AA4
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.LocalName != "EncryptionProperty" || value.NamespaceURI != "http://www.w3.org/2001/04/xmlenc#")
			{
				throw new CryptographicException("Malformed encryption property element.");
			}
			this._cachedXml = value;
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2001/04/xmlenc#");
			this._target = Utils.GetAttribute(value, "Target", "http://www.w3.org/2001/04/xmlenc#");
			this._elemProp = value;
		}

		// Token: 0x0400019C RID: 412
		private string _target;

		// Token: 0x0400019D RID: 413
		private string _id;

		// Token: 0x0400019E RID: 414
		private XmlElement _elemProp;

		// Token: 0x0400019F RID: 415
		private XmlElement _cachedXml;
	}
}
