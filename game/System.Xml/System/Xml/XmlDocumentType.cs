using System;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents the document type declaration.</summary>
	// Token: 0x020001BE RID: 446
	public class XmlDocumentType : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocumentType" /> class.</summary>
		/// <param name="name">The qualified name; see the <see cref="P:System.Xml.XmlDocumentType.Name" /> property.</param>
		/// <param name="publicId">The public identifier; see the <see cref="P:System.Xml.XmlDocumentType.PublicId" /> property.</param>
		/// <param name="systemId">The system identifier; see the <see cref="P:System.Xml.XmlDocumentType.SystemId" /> property.</param>
		/// <param name="internalSubset">The DTD internal subset; see the <see cref="P:System.Xml.XmlDocumentType.InternalSubset" /> property.</param>
		/// <param name="doc">The parent document.</param>
		// Token: 0x060010F1 RID: 4337 RVA: 0x00069990 File Offset: 0x00067B90
		protected internal XmlDocumentType(string name, string publicId, string systemId, string internalSubset, XmlDocument doc) : base(doc)
		{
			this.name = name;
			this.publicId = publicId;
			this.systemId = systemId;
			this.namespaces = true;
			this.internalSubset = internalSubset;
			if (!doc.IsLoading)
			{
				doc.IsLoading = true;
				new XmlLoader().ParseDocumentType(this);
				doc.IsLoading = false;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For DocumentType nodes, this property returns the name of the document type.</returns>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x000699ED File Offset: 0x00067BED
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For DocumentType nodes, this property returns the name of the document type.</returns>
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x000699ED File Offset: 0x00067BED
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For DocumentType nodes, this value is XmlNodeType.DocumentType.</returns>
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x000699F5 File Offset: 0x00067BF5
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentType;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. For document type nodes, the cloned node always includes the subtree, regardless of the parameter setting. </param>
		/// <returns>The cloned node.</returns>
		// Token: 0x060010F5 RID: 4341 RVA: 0x000699F9 File Offset: 0x00067BF9
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateDocumentType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>
		///     <see langword="true" /> if the node is read-only; otherwise <see langword="false" />.Because DocumentType nodes are read-only, this property always returns <see langword="true" />.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.XmlEntity" /> nodes declared in the document type declaration.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNamedNodeMap" /> containing the <see langword="XmlEntity" /> nodes. The returned <see langword="XmlNamedNodeMap" /> is read-only.</returns>
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00069A1E File Offset: 0x00067C1E
		public XmlNamedNodeMap Entities
		{
			get
			{
				if (this.entities == null)
				{
					this.entities = new XmlNamedNodeMap(this);
				}
				return this.entities;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Xml.XmlNotation" /> nodes present in the document type declaration.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlNamedNodeMap" /> containing the <see langword="XmlNotation" /> nodes. The returned <see langword="XmlNamedNodeMap" /> is read-only.</returns>
		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00069A3A File Offset: 0x00067C3A
		public XmlNamedNodeMap Notations
		{
			get
			{
				if (this.notations == null)
				{
					this.notations = new XmlNamedNodeMap(this);
				}
				return this.notations;
			}
		}

		/// <summary>Gets the value of the public identifier on the DOCTYPE declaration.</summary>
		/// <returns>The public identifier on the DOCTYPE. If there is no public identifier, <see langword="null" /> is returned.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00069A56 File Offset: 0x00067C56
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		/// <summary>Gets the value of the system identifier on the DOCTYPE declaration.</summary>
		/// <returns>The system identifier on the DOCTYPE. If there is no system identifier, <see langword="null" /> is returned.</returns>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00069A5E File Offset: 0x00067C5E
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		/// <summary>Gets the value of the document type definition (DTD) internal subset on the DOCTYPE declaration.</summary>
		/// <returns>The DTD internal subset on the DOCTYPE. If there is no DTD internal subset, String.Empty is returned.</returns>
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00069A66 File Offset: 0x00067C66
		public string InternalSubset
		{
			get
			{
				return this.internalSubset;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00069A6E File Offset: 0x00067C6E
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00069A76 File Offset: 0x00067C76
		internal bool ParseWithNamespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				this.namespaces = value;
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060010FE RID: 4350 RVA: 0x00069A7F File Offset: 0x00067C7F
		public override void WriteTo(XmlWriter w)
		{
			w.WriteDocType(this.name, this.publicId, this.systemId, this.internalSubset);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. For <see langword="XmlDocumentType" /> nodes, this method has no effect.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060010FF RID: 4351 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x00069A9F File Offset: 0x00067C9F
		// (set) Token: 0x06001101 RID: 4353 RVA: 0x00069AA7 File Offset: 0x00067CA7
		internal SchemaInfo DtdSchemaInfo
		{
			get
			{
				return this.schemaInfo;
			}
			set
			{
				this.schemaInfo = value;
			}
		}

		// Token: 0x04001074 RID: 4212
		private string name;

		// Token: 0x04001075 RID: 4213
		private string publicId;

		// Token: 0x04001076 RID: 4214
		private string systemId;

		// Token: 0x04001077 RID: 4215
		private string internalSubset;

		// Token: 0x04001078 RID: 4216
		private bool namespaces;

		// Token: 0x04001079 RID: 4217
		private XmlNamedNodeMap entities;

		// Token: 0x0400107A RID: 4218
		private XmlNamedNodeMap notations;

		// Token: 0x0400107B RID: 4219
		private SchemaInfo schemaInfo;
	}
}
