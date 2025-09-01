using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML Document Type Definition (DTD).</summary>
	// Token: 0x0200002E RID: 46
	public class XDocumentType : XNode
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Linq.XDocumentType" /> class.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that contains the qualified name of the DTD, which is the same as the qualified name of the root element of the XML document.</param>
		/// <param name="publicId">A <see cref="T:System.String" /> that contains the public identifier of an external public DTD.</param>
		/// <param name="systemId">A <see cref="T:System.String" /> that contains the system identifier of an external private DTD.</param>
		/// <param name="internalSubset">A <see cref="T:System.String" /> that contains the internal subset for an internal DTD.</param>
		// Token: 0x060001A9 RID: 425 RVA: 0x00008C3A File Offset: 0x00006E3A
		public XDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			this._name = XmlConvert.VerifyName(name);
			this._publicId = publicId;
			this._systemId = systemId;
			this._internalSubset = internalSubset;
		}

		/// <summary>Initializes an instance of the <see cref="T:System.Xml.Linq.XDocumentType" /> class from another <see cref="T:System.Xml.Linq.XDocumentType" /> object.</summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XDocumentType" /> object to copy from.</param>
		// Token: 0x060001AA RID: 426 RVA: 0x00008C64 File Offset: 0x00006E64
		public XDocumentType(XDocumentType other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this._name = other._name;
			this._publicId = other._publicId;
			this._systemId = other._systemId;
			this._internalSubset = other._internalSubset;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008CB8 File Offset: 0x00006EB8
		internal XDocumentType(XmlReader r)
		{
			this._name = r.Name;
			this._publicId = r.GetAttribute("PUBLIC");
			this._systemId = r.GetAttribute("SYSTEM");
			this._internalSubset = r.Value;
			r.Read();
		}

		/// <summary>Gets or sets the internal subset for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the internal subset for this Document Type Definition (DTD).</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00008D0C File Offset: 0x00006F0C
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00008D14 File Offset: 0x00006F14
		public string InternalSubset
		{
			get
			{
				return this._internalSubset;
			}
			set
			{
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this._internalSubset = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Gets or sets the name for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name for this Document Type Definition (DTD).</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008D38 File Offset: 0x00006F38
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00008D40 File Offset: 0x00006F40
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				value = XmlConvert.VerifyName(value);
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Name);
				this._name = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Name);
				}
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XDocumentType" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.DocumentType" />.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00008D6C File Offset: 0x00006F6C
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		/// <summary>Gets or sets the public identifier for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the public identifier for this Document Type Definition (DTD).</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00008D70 File Offset: 0x00006F70
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00008D78 File Offset: 0x00006F78
		public string PublicId
		{
			get
			{
				return this._publicId;
			}
			set
			{
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this._publicId = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Gets or sets the system identifier for this Document Type Definition (DTD).</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the system identifier for this Document Type Definition (DTD).</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008D9C File Offset: 0x00006F9C
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public string SystemId
		{
			get
			{
				return this._systemId;
			}
			set
			{
				bool flag = base.NotifyChanging(this, XObjectChangeEventArgs.Value);
				this._systemId = value;
				if (flag)
				{
					base.NotifyChanged(this, XObjectChangeEventArgs.Value);
				}
			}
		}

		/// <summary>Write this <see cref="T:System.Xml.Linq.XDocumentType" /> to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		// Token: 0x060001B5 RID: 437 RVA: 0x00008DC8 File Offset: 0x00006FC8
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteDocType(this._name, this._publicId, this._systemId, this._internalSubset);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008DF6 File Offset: 0x00006FF6
		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return writer.WriteDocTypeAsync(this._name, this._publicId, this._systemId, this._internalSubset);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008E34 File Offset: 0x00007034
		internal override XNode CloneNode()
		{
			return new XDocumentType(this);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008E3C File Offset: 0x0000703C
		internal override bool DeepEquals(XNode node)
		{
			XDocumentType xdocumentType = node as XDocumentType;
			return xdocumentType != null && this._name == xdocumentType._name && this._publicId == xdocumentType._publicId && this._systemId == xdocumentType.SystemId && this._internalSubset == xdocumentType._internalSubset;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008EA0 File Offset: 0x000070A0
		internal override int GetDeepHashCode()
		{
			return this._name.GetHashCode() ^ ((this._publicId != null) ? this._publicId.GetHashCode() : 0) ^ ((this._systemId != null) ? this._systemId.GetHashCode() : 0) ^ ((this._internalSubset != null) ? this._internalSubset.GetHashCode() : 0);
		}

		// Token: 0x040000F5 RID: 245
		private string _name;

		// Token: 0x040000F6 RID: 246
		private string _publicId;

		// Token: 0x040000F7 RID: 247
		private string _systemId;

		// Token: 0x040000F8 RID: 248
		private string _internalSubset;
	}
}
