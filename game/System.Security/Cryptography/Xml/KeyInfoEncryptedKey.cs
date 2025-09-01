using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Wraps the <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> class, it to be placed as a subelement of the <see cref="T:System.Security.Cryptography.Xml.KeyInfo" /> class.</summary>
	// Token: 0x02000042 RID: 66
	public class KeyInfoEncryptedKey : KeyInfoClause
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> class.</summary>
		// Token: 0x060001B0 RID: 432 RVA: 0x00008215 File Offset: 0x00006415
		public KeyInfoEncryptedKey()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> class using an <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object.</summary>
		/// <param name="encryptedKey">An <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</param>
		// Token: 0x060001B1 RID: 433 RVA: 0x0000821D File Offset: 0x0000641D
		public KeyInfoEncryptedKey(EncryptedKey encryptedKey)
		{
			this._encryptedKey = encryptedKey;
		}

		/// <summary>Gets or sets an <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Xml.EncryptedKey" /> object that encapsulates an encrypted key.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <see cref="P:System.Security.Cryptography.Xml.KeyInfoEncryptedKey.EncryptedKey" /> property is <see langword="null" />.</exception>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000822C File Offset: 0x0000642C
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x00008234 File Offset: 0x00006434
		public EncryptedKey EncryptedKey
		{
			get
			{
				return this._encryptedKey;
			}
			set
			{
				this._encryptedKey = value;
			}
		}

		/// <summary>Returns an XML representation of a <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</summary>
		/// <returns>An XML representation of a <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The encrypted key is <see langword="null" />.</exception>
		// Token: 0x060001B4 RID: 436 RVA: 0x0000823D File Offset: 0x0000643D
		public override XmlElement GetXml()
		{
			if (this._encryptedKey == null)
			{
				throw new CryptographicException("Malformed element {0}.", "KeyInfoEncryptedKey");
			}
			return this._encryptedKey.GetXml();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008262 File Offset: 0x00006462
		internal override XmlElement GetXml(XmlDocument xmlDocument)
		{
			if (this._encryptedKey == null)
			{
				throw new CryptographicException("Malformed element {0}.", "KeyInfoEncryptedKey");
			}
			return this._encryptedKey.GetXml(xmlDocument);
		}

		/// <summary>Parses the input <see cref="T:System.Xml.XmlElement" /> object and configures the internal state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object to match.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlElement" /> object that specifies the state of the <see cref="T:System.Security.Cryptography.Xml.KeyInfoEncryptedKey" /> object.</param>
		// Token: 0x060001B6 RID: 438 RVA: 0x00008288 File Offset: 0x00006488
		public override void LoadXml(XmlElement value)
		{
			this._encryptedKey = new EncryptedKey();
			this._encryptedKey.LoadXml(value);
		}

		// Token: 0x040001A6 RID: 422
		private EncryptedKey _encryptedKey;
	}
}
