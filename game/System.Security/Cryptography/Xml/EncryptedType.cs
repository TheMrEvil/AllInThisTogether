using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the abstract base class from which the classes <see cref="T:System.Security.Cryptography.Xml.EncryptedData" /> and <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> derive.</summary>
	// Token: 0x02000037 RID: 55
	public abstract class EncryptedType
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000063E2 File Offset: 0x000045E2
		internal bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Gets or sets the <see langword="Id" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string of the <see langword="Id" /> attribute of the <see langword="&lt;EncryptedType&gt;" /> element.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000063ED File Offset: 0x000045ED
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000063F5 File Offset: 0x000045F5
		public virtual string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="Type" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the text form of the encrypted data.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006405 File Offset: 0x00004605
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000640D File Offset: 0x0000460D
		public virtual string Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="MimeType" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the media type of the encrypted data.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000641D File Offset: 0x0000461D
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006425 File Offset: 0x00004625
		public virtual string MimeType
		{
			get
			{
				return this._mimeType;
			}
			set
			{
				this._mimeType = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="Encoding" /> attribute of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> instance in XML encryption.</summary>
		/// <returns>A string that describes the encoding of the encrypted data.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006435 File Offset: 0x00004635
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000643D File Offset: 0x0000463D
		public virtual string Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets of sets the <see langword="&lt;KeyInfo&gt;" /> element in XML encryption.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> object.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000644D File Offset: 0x0000464D
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00006468 File Offset: 0x00004668
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

		/// <summary>Gets or sets the <see langword="&lt;EncryptionMethod&gt;" /> element for XML encryption.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionMethod" /> object that represents the <see langword="&lt;EncryptionMethod&gt;" /> element.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006471 File Offset: 0x00004671
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00006479 File Offset: 0x00004679
		public virtual EncryptionMethod EncryptionMethod
		{
			get
			{
				return this._encryptionMethod;
			}
			set
			{
				this._encryptionMethod = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Gets or sets the <see langword="&lt;EncryptionProperties&gt;" /> element in XML encryption.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptionPropertyCollection" /> object.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006489 File Offset: 0x00004689
		public virtual EncryptionPropertyCollection EncryptionProperties
		{
			get
			{
				if (this._props == null)
				{
					this._props = new EncryptionPropertyCollection();
				}
				return this._props;
			}
		}

		/// <summary>Adds an <see langword="&lt;EncryptionProperty&gt;" /> child element to the <see langword="&lt;EncryptedProperties&gt;" /> element in the current <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object in XML encryption.</summary>
		/// <param name="ep">An <see cref="T:System.Security.Cryptography.Xml.EncryptionProperty" /> object.</param>
		// Token: 0x06000137 RID: 311 RVA: 0x000064A4 File Offset: 0x000046A4
		public void AddProperty(EncryptionProperty ep)
		{
			this.EncryptionProperties.Add(ep);
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.Xml.CipherData" /> value for an instance of an <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Xml.CipherData" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Cryptography.Xml.EncryptedType.CipherData" /> property was set to <see langword="null" />.</exception>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000064B3 File Offset: 0x000046B3
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000064CE File Offset: 0x000046CE
		public virtual CipherData CipherData
		{
			get
			{
				if (this._cipherData == null)
				{
					this._cipherData = new CipherData();
				}
				return this._cipherData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._cipherData = value;
				this._cachedXml = null;
			}
		}

		/// <summary>Loads XML information into the <see langword="&lt;EncryptedType&gt;" /> element in XML encryption.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlElement" /> object representing an XML element to use in the <see langword="&lt;EncryptedType&gt;" /> element.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> provided is <see langword="null" />.</exception>
		// Token: 0x0600013A RID: 314
		public abstract void LoadXml(XmlElement value);

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> object that represents the <see langword="&lt;EncryptedType&gt;" /> element in XML encryption.</returns>
		// Token: 0x0600013B RID: 315
		public abstract XmlElement GetXml();

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.EncryptedType" /> class.</summary>
		// Token: 0x0600013C RID: 316 RVA: 0x00002145 File Offset: 0x00000345
		protected EncryptedType()
		{
		}

		// Token: 0x04000174 RID: 372
		private string _id;

		// Token: 0x04000175 RID: 373
		private string _type;

		// Token: 0x04000176 RID: 374
		private string _mimeType;

		// Token: 0x04000177 RID: 375
		private string _encoding;

		// Token: 0x04000178 RID: 376
		private EncryptionMethod _encryptionMethod;

		// Token: 0x04000179 RID: 377
		private CipherData _cipherData;

		// Token: 0x0400017A RID: 378
		private EncryptionPropertyCollection _props;

		// Token: 0x0400017B RID: 379
		private KeyInfo _keyInfo;

		// Token: 0x0400017C RID: 380
		internal XmlElement _cachedXml;
	}
}
