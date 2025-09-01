using System;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	/// <summary>Represents the object element of an XML signature that holds data to be signed.</summary>
	// Token: 0x02000031 RID: 49
	public class DataObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> class.</summary>
		// Token: 0x060000FB RID: 251 RVA: 0x00005594 File Offset: 0x00003794
		public DataObject()
		{
			this._cachedXml = null;
			this._elData = new CanonicalXmlNodeList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> class with the specified identification, MIME type, encoding, and data.</summary>
		/// <param name="id">The identification to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" /> with.</param>
		/// <param name="mimeType">The MIME type of the data used to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" />.</param>
		/// <param name="encoding">The encoding of the data used to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" />.</param>
		/// <param name="data">The data to initialize the new instance of <see cref="T:System.Security.Cryptography.Xml.DataObject" /> with.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="data" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060000FC RID: 252 RVA: 0x000055B0 File Offset: 0x000037B0
		public DataObject(string id, string mimeType, string encoding, XmlElement data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this._id = id;
			this._mimeType = mimeType;
			this._encoding = encoding;
			this._elData = new CanonicalXmlNodeList();
			this._elData.Add(data);
			this._cachedXml = null;
		}

		/// <summary>Gets or sets the identification of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The name of the element that contains data to be used.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005607 File Offset: 0x00003807
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000560F File Offset: 0x0000380F
		public string Id
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

		/// <summary>Gets or sets the MIME type of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The MIME type of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object. The default is <see langword="null" />.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000561F File Offset: 0x0000381F
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00005627 File Offset: 0x00003827
		public string MimeType
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

		/// <summary>Gets or sets the encoding of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The type of encoding of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005637 File Offset: 0x00003837
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000563F File Offset: 0x0000383F
		public string Encoding
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

		/// <summary>Gets or sets the data value of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The data of the current <see cref="T:System.Security.Cryptography.Xml.DataObject" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value used to set the property is <see langword="null" />.</exception>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000564F File Offset: 0x0000384F
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00005658 File Offset: 0x00003858
		public XmlNodeList Data
		{
			get
			{
				return this._elData;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._elData = new CanonicalXmlNodeList();
				foreach (object obj in value)
				{
					XmlNode value2 = (XmlNode)obj;
					this._elData.Add(value2);
				}
				this._cachedXml = null;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000105 RID: 261 RVA: 0x000056D4 File Offset: 0x000038D4
		private bool CacheValid
		{
			get
			{
				return this._cachedXml != null;
			}
		}

		/// <summary>Returns the XML representation of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</summary>
		/// <returns>The XML representation of the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> object.</returns>
		// Token: 0x06000106 RID: 262 RVA: 0x000056E0 File Offset: 0x000038E0
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

		// Token: 0x06000107 RID: 263 RVA: 0x00005710 File Offset: 0x00003910
		internal XmlElement GetXml(XmlDocument document)
		{
			XmlElement xmlElement = document.CreateElement("Object", "http://www.w3.org/2000/09/xmldsig#");
			if (!string.IsNullOrEmpty(this._id))
			{
				xmlElement.SetAttribute("Id", this._id);
			}
			if (!string.IsNullOrEmpty(this._mimeType))
			{
				xmlElement.SetAttribute("MimeType", this._mimeType);
			}
			if (!string.IsNullOrEmpty(this._encoding))
			{
				xmlElement.SetAttribute("Encoding", this._encoding);
			}
			if (this._elData != null)
			{
				foreach (object obj in this._elData)
				{
					XmlNode node = (XmlNode)obj;
					xmlElement.AppendChild(document.ImportNode(node, true));
				}
			}
			return xmlElement;
		}

		/// <summary>Loads a <see cref="T:System.Security.Cryptography.Xml.DataObject" /> state from an XML element.</summary>
		/// <param name="value">The XML element to load the <see cref="T:System.Security.Cryptography.Xml.DataObject" /> state from.</param>
		/// <exception cref="T:System.ArgumentNullException">The value from the XML element is <see langword="null" />.</exception>
		// Token: 0x06000108 RID: 264 RVA: 0x000057E8 File Offset: 0x000039E8
		public void LoadXml(XmlElement value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._id = Utils.GetAttribute(value, "Id", "http://www.w3.org/2000/09/xmldsig#");
			this._mimeType = Utils.GetAttribute(value, "MimeType", "http://www.w3.org/2000/09/xmldsig#");
			this._encoding = Utils.GetAttribute(value, "Encoding", "http://www.w3.org/2000/09/xmldsig#");
			foreach (object obj in value.ChildNodes)
			{
				XmlNode value2 = (XmlNode)obj;
				this._elData.Add(value2);
			}
			this._cachedXml = value;
		}

		// Token: 0x04000164 RID: 356
		private string _id;

		// Token: 0x04000165 RID: 357
		private string _mimeType;

		// Token: 0x04000166 RID: 358
		private string _encoding;

		// Token: 0x04000167 RID: 359
		private CanonicalXmlNodeList _elData;

		// Token: 0x04000168 RID: 360
		private XmlElement _cachedXml;
	}
}
