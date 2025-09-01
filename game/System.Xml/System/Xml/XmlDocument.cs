using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an XML document. You can use this class to load, validate, edit, add, and position XML in a document.</summary>
	// Token: 0x020001BC RID: 444
	public class XmlDocument : XmlNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocument" /> class.</summary>
		// Token: 0x0600105A RID: 4186 RVA: 0x00067BBC File Offset: 0x00065DBC
		public XmlDocument() : this(new XmlImplementation())
		{
		}

		/// <summary>Initializes a new instance of the <see langword="XmlDocument" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nt">The <see langword="XmlNameTable" /> to use. </param>
		// Token: 0x0600105B RID: 4187 RVA: 0x00067BC9 File Offset: 0x00065DC9
		public XmlDocument(XmlNameTable nt) : this(new XmlImplementation(nt))
		{
		}

		/// <summary>Initializes a new instance of the <see langword="XmlDocument" /> class with the specified <see cref="T:System.Xml.XmlImplementation" />.</summary>
		/// <param name="imp">The <see langword="XmlImplementation" /> to use. </param>
		// Token: 0x0600105C RID: 4188 RVA: 0x00067BD8 File Offset: 0x00065DD8
		protected internal XmlDocument(XmlImplementation imp)
		{
			this.implementation = imp;
			this.domNameTable = new DomNameTable(this);
			XmlNameTable nameTable = this.NameTable;
			nameTable.Add(string.Empty);
			this.strDocumentName = nameTable.Add("#document");
			this.strDocumentFragmentName = nameTable.Add("#document-fragment");
			this.strCommentName = nameTable.Add("#comment");
			this.strTextName = nameTable.Add("#text");
			this.strCDataSectionName = nameTable.Add("#cdata-section");
			this.strEntityName = nameTable.Add("#entity");
			this.strID = nameTable.Add("id");
			this.strNonSignificantWhitespaceName = nameTable.Add("#whitespace");
			this.strSignificantWhitespaceName = nameTable.Add("#significant-whitespace");
			this.strXmlns = nameTable.Add("xmlns");
			this.strXml = nameTable.Add("xml");
			this.strSpace = nameTable.Add("space");
			this.strLang = nameTable.Add("lang");
			this.strReservedXmlns = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.strReservedXml = nameTable.Add("http://www.w3.org/XML/1998/namespace");
			this.strEmpty = nameTable.Add(string.Empty);
			this.baseURI = string.Empty;
			this.objLock = new object();
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00067D37 File Offset: 0x00065F37
		// (set) Token: 0x0600105E RID: 4190 RVA: 0x00067D3F File Offset: 0x00065F3F
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

		// Token: 0x0600105F RID: 4191 RVA: 0x00067D48 File Offset: 0x00065F48
		internal static void CheckName(string name)
		{
			int num = ValidateNames.ParseNmtoken(name, 0);
			if (num < name.Length)
			{
				throw new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num));
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00067D78 File Offset: 0x00065F78
		internal XmlName AddXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			return this.domNameTable.AddName(prefix, localName, namespaceURI, schemaInfo);
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00067D8A File Offset: 0x00065F8A
		internal XmlName GetXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			return this.domNameTable.GetName(prefix, localName, namespaceURI, schemaInfo);
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00067D9C File Offset: 0x00065F9C
		internal XmlName AddAttrXmlName(string prefix, string localName, string namespaceURI, IXmlSchemaInfo schemaInfo)
		{
			XmlName xmlName = this.AddXmlName(prefix, localName, namespaceURI, schemaInfo);
			if (!this.IsLoading)
			{
				object prefix2 = xmlName.Prefix;
				object namespaceURI2 = xmlName.NamespaceURI;
				object localName2 = xmlName.LocalName;
				if ((prefix2 == this.strXmlns || (prefix2 == this.strEmpty && localName2 == this.strXmlns)) ^ namespaceURI2 == this.strReservedXmlns)
				{
					throw new ArgumentException(Res.GetString("The namespace declaration attribute has an incorrect 'namespaceURI': '{0}'.", new object[]
					{
						namespaceURI
					}));
				}
			}
			return xmlName;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00067E1A File Offset: 0x0006601A
		internal bool AddIdInfo(XmlName eleName, XmlName attrName)
		{
			if (this.htElementIDAttrDecl == null || this.htElementIDAttrDecl[eleName] == null)
			{
				if (this.htElementIDAttrDecl == null)
				{
					this.htElementIDAttrDecl = new Hashtable();
				}
				this.htElementIDAttrDecl.Add(eleName, attrName);
				return true;
			}
			return false;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00067E58 File Offset: 0x00066058
		private XmlName GetIDInfoByElement_(XmlName eleName)
		{
			XmlName xmlName = this.GetXmlName(eleName.Prefix, eleName.LocalName, string.Empty, null);
			if (xmlName != null)
			{
				return (XmlName)this.htElementIDAttrDecl[xmlName];
			}
			return null;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00067E94 File Offset: 0x00066094
		internal XmlName GetIDInfoByElement(XmlName eleName)
		{
			if (this.htElementIDAttrDecl == null)
			{
				return null;
			}
			return this.GetIDInfoByElement_(eleName);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00067EA8 File Offset: 0x000660A8
		private WeakReference GetElement(ArrayList elementList, XmlElement elem)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in elementList)
			{
				WeakReference weakReference = (WeakReference)obj;
				if (!weakReference.IsAlive)
				{
					arrayList.Add(weakReference);
				}
				else if ((XmlElement)weakReference.Target == elem)
				{
					return weakReference;
				}
			}
			foreach (object obj2 in arrayList)
			{
				WeakReference obj3 = (WeakReference)obj2;
				elementList.Remove(obj3);
			}
			return null;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00067F70 File Offset: 0x00066170
		internal void AddElementWithId(string id, XmlElement elem)
		{
			if (this.htElementIdMap == null || !this.htElementIdMap.Contains(id))
			{
				if (this.htElementIdMap == null)
				{
					this.htElementIdMap = new Hashtable();
				}
				ArrayList arrayList = new ArrayList();
				arrayList.Add(new WeakReference(elem));
				this.htElementIdMap.Add(id, arrayList);
				return;
			}
			ArrayList arrayList2 = (ArrayList)this.htElementIdMap[id];
			if (this.GetElement(arrayList2, elem) == null)
			{
				arrayList2.Add(new WeakReference(elem));
			}
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00067FF0 File Offset: 0x000661F0
		internal void RemoveElementWithId(string id, XmlElement elem)
		{
			if (this.htElementIdMap != null && this.htElementIdMap.Contains(id))
			{
				ArrayList arrayList = (ArrayList)this.htElementIdMap[id];
				WeakReference element = this.GetElement(arrayList, elem);
				if (element != null)
				{
					arrayList.Remove(element);
					if (arrayList.Count == 0)
					{
						this.htElementIdMap.Remove(id);
					}
				}
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. </param>
		/// <returns>The cloned <see langword="XmlDocument" /> node.</returns>
		// Token: 0x06001069 RID: 4201 RVA: 0x0006804C File Offset: 0x0006624C
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument xmlDocument = this.Implementation.CreateDocument();
			xmlDocument.SetBaseURI(this.baseURI);
			if (deep)
			{
				xmlDocument.ImportChildren(this, xmlDocument, deep);
			}
			return xmlDocument;
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For <see langword="XmlDocument" /> nodes, this value is XmlNodeType.Document.</returns>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x0600106A RID: 4202 RVA: 0x0006807E File Offset: 0x0006627E
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Document;
			}
		}

		/// <summary>Gets the parent node of this node (for nodes that can have parents).</summary>
		/// <returns>Always returns <see langword="null" />.</returns>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the node containing the DOCTYPE declaration.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> containing the DocumentType (DOCTYPE declaration).</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x00068082 File Offset: 0x00066282
		public virtual XmlDocumentType DocumentType
		{
			get
			{
				return (XmlDocumentType)this.FindChild(XmlNodeType.DocumentType);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x00068091 File Offset: 0x00066291
		internal virtual XmlDeclaration Declaration
		{
			get
			{
				if (this.HasChildNodes)
				{
					return this.FirstChild as XmlDeclaration;
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlImplementation" /> object for the current document.</summary>
		/// <returns>The <see langword="XmlImplementation" /> object for the current document.</returns>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000680A8 File Offset: 0x000662A8
		public XmlImplementation Implementation
		{
			get
			{
				return this.implementation;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For <see langword="XmlDocument" /> nodes, the name is #document.</returns>
		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x000680B0 File Offset: 0x000662B0
		public override string Name
		{
			get
			{
				return this.strDocumentName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For <see langword="XmlDocument" /> nodes, the local name is #document.</returns>
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x000680B0 File Offset: 0x000662B0
		public override string LocalName
		{
			get
			{
				return this.strDocumentName;
			}
		}

		/// <summary>Gets the root <see cref="T:System.Xml.XmlElement" /> for the document.</summary>
		/// <returns>The <see langword="XmlElement" /> that represents the root of the XML document tree. If no root exists, <see langword="null" /> is returned.</returns>
		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x000680B8 File Offset: 0x000662B8
		public XmlElement DocumentElement
		{
			get
			{
				return (XmlElement)this.FindChild(XmlNodeType.Element);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x000680C6 File Offset: 0x000662C6
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x000680CE File Offset: 0x000662CE
		internal override XmlLinkedNode LastNode
		{
			get
			{
				return this.lastChild;
			}
			set
			{
				this.lastChild = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which the current node belongs.</summary>
		/// <returns>For <see langword="XmlDocument" /> nodes (<see cref="P:System.Xml.XmlDocument.NodeType" /> equals XmlNodeType.Document), this property always returns <see langword="null" />.</returns>
		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override XmlDocument OwnerDocument
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object associated with this <see cref="T:System.Xml.XmlDocument" />.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object containing the XML Schema Definition Language (XSD) schemas associated with this <see cref="T:System.Xml.XmlDocument" />; otherwise, an empty <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</returns>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x000680D7 File Offset: 0x000662D7
		// (set) Token: 0x06001077 RID: 4215 RVA: 0x000680F8 File Offset: 0x000662F8
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaSet(this.NameTable);
				}
				return this.schemas;
			}
			set
			{
				this.schemas = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001078 RID: 4216 RVA: 0x00068101 File Offset: 0x00066301
		internal bool CanReportValidity
		{
			get
			{
				return this.reportValidity;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00068109 File Offset: 0x00066309
		internal bool HasSetResolver
		{
			get
			{
				return this.bSetResolver;
			}
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x00068111 File Offset: 0x00066311
		internal XmlResolver GetResolver()
		{
			return this.resolver;
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> to use for resolving external resources.</summary>
		/// <returns>The <see langword="XmlResolver" /> to use.In version 1.1 of the.NET Framework, the caller must be fully trusted in order to specify an <see langword="XmlResolver" />.</returns>
		/// <exception cref="T:System.Xml.XmlException">This property is set to <see langword="null" /> and an external DTD or entity is encountered. </exception>
		// Token: 0x170002BA RID: 698
		// (set) Token: 0x0600107B RID: 4219 RVA: 0x0006811C File Offset: 0x0006631C
		public virtual XmlResolver XmlResolver
		{
			set
			{
				if (value != null)
				{
					try
					{
						new NamedPermissionSet("FullTrust").Demand();
					}
					catch (SecurityException inner)
					{
						throw new SecurityException(Res.GetString("XmlResolver can be set only by fully trusted code."), inner);
					}
				}
				this.resolver = value;
				if (!this.bSetResolver)
				{
					this.bSetResolver = true;
				}
				XmlDocumentType documentType = this.DocumentType;
				if (documentType != null)
				{
					documentType.DtdSchemaInfo = null;
				}
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x00068188 File Offset: 0x00066388
		internal override bool IsValidChildType(XmlNodeType type)
		{
			if (type != XmlNodeType.Element)
			{
				switch (type)
				{
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					return true;
				case XmlNodeType.DocumentType:
					if (this.DocumentType != null)
					{
						throw new InvalidOperationException(Res.GetString("This document already has a 'DocumentType' node."));
					}
					return true;
				case XmlNodeType.XmlDeclaration:
					if (this.Declaration != null)
					{
						throw new InvalidOperationException(Res.GetString("This document already has an 'XmlDeclaration' node."));
					}
					return true;
				}
				return false;
			}
			if (this.DocumentElement != null)
			{
				throw new InvalidOperationException(Res.GetString("This document already has a 'DocumentElement' node."));
			}
			return true;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x00068220 File Offset: 0x00066420
		private bool HasNodeTypeInPrevSiblings(XmlNodeType nt, XmlNode refNode)
		{
			if (refNode == null)
			{
				return false;
			}
			XmlNode xmlNode = null;
			if (refNode.ParentNode != null)
			{
				xmlNode = refNode.ParentNode.FirstChild;
			}
			while (xmlNode != null)
			{
				if (xmlNode.NodeType == nt)
				{
					return true;
				}
				if (xmlNode == refNode)
				{
					break;
				}
				xmlNode = xmlNode.NextSibling;
			}
			return false;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x00068264 File Offset: 0x00066464
		private bool HasNodeTypeInNextSiblings(XmlNodeType nt, XmlNode refNode)
		{
			for (XmlNode xmlNode = refNode; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == nt)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0006828C File Offset: 0x0006648C
		internal override bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			if (refChild == null)
			{
				refChild = this.FirstChild;
			}
			if (refChild == null)
			{
				return true;
			}
			XmlNodeType nodeType = newChild.NodeType;
			if (nodeType <= XmlNodeType.Comment)
			{
				if (nodeType != XmlNodeType.Element)
				{
					if (nodeType - XmlNodeType.ProcessingInstruction <= 1)
					{
						return refChild.NodeType != XmlNodeType.XmlDeclaration;
					}
				}
				else if (refChild.NodeType != XmlNodeType.XmlDeclaration)
				{
					return !this.HasNodeTypeInNextSiblings(XmlNodeType.DocumentType, refChild);
				}
			}
			else if (nodeType != XmlNodeType.DocumentType)
			{
				if (nodeType == XmlNodeType.XmlDeclaration)
				{
					return refChild == this.FirstChild;
				}
			}
			else if (refChild.NodeType != XmlNodeType.XmlDeclaration)
			{
				return !this.HasNodeTypeInPrevSiblings(XmlNodeType.Element, refChild.PreviousSibling);
			}
			return false;
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x00068318 File Offset: 0x00066518
		internal override bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			if (refChild == null)
			{
				refChild = this.LastChild;
			}
			if (refChild == null)
			{
				return true;
			}
			XmlNodeType nodeType = newChild.NodeType;
			if (nodeType != XmlNodeType.Element)
			{
				switch (nodeType)
				{
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					return true;
				case XmlNodeType.DocumentType:
					return !this.HasNodeTypeInPrevSiblings(XmlNodeType.Element, refChild);
				}
				return false;
			}
			return !this.HasNodeTypeInNextSiblings(XmlNodeType.DocumentType, refChild.NextSibling);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlAttribute" /> with the specified <see cref="P:System.Xml.XmlDocument.Name" />.</summary>
		/// <param name="name">The qualified name of the attribute. If the name contains a colon, the <see cref="P:System.Xml.XmlNode.Prefix" /> property reflects the part of the name preceding the first colon and the <see cref="P:System.Xml.XmlDocument.LocalName" /> property reflects the part of the name following the first colon. The <see cref="P:System.Xml.XmlNode.NamespaceURI" /> remains empty unless the prefix is a recognized built-in prefix such as xmlns. In this case <see langword="NamespaceURI" /> has a value of http://www.w3.org/2000/xmlns/. </param>
		/// <returns>The new <see langword="XmlAttribute" />.</returns>
		// Token: 0x06001081 RID: 4225 RVA: 0x0006838C File Offset: 0x0006658C
		public XmlAttribute CreateAttribute(string name)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			string empty3 = string.Empty;
			XmlNode.SplitName(name, out empty, out empty2);
			this.SetDefaultNamespace(empty, empty2, ref empty3);
			return this.CreateAttribute(empty, empty2, empty3);
		}

		// Token: 0x06001082 RID: 4226 RVA: 0x000683C8 File Offset: 0x000665C8
		internal void SetDefaultNamespace(string prefix, string localName, ref string namespaceURI)
		{
			if (prefix == this.strXmlns || (prefix.Length == 0 && localName == this.strXmlns))
			{
				namespaceURI = this.strReservedXmlns;
				return;
			}
			if (prefix == this.strXml)
			{
				namespaceURI = this.strReservedXml;
			}
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlCDataSection" /> containing the specified data.</summary>
		/// <param name="data">The content of the new <see langword="XmlCDataSection" />. </param>
		/// <returns>The new <see langword="XmlCDataSection" />.</returns>
		// Token: 0x06001083 RID: 4227 RVA: 0x00068418 File Offset: 0x00066618
		public virtual XmlCDataSection CreateCDataSection(string data)
		{
			this.fCDataNodesPresent = true;
			return new XmlCDataSection(data, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlComment" /> containing the specified data.</summary>
		/// <param name="data">The content of the new <see langword="XmlComment" />. </param>
		/// <returns>The new <see langword="XmlComment" />.</returns>
		// Token: 0x06001084 RID: 4228 RVA: 0x00068428 File Offset: 0x00066628
		public virtual XmlComment CreateComment(string data)
		{
			return new XmlComment(data, this);
		}

		/// <summary>Returns a new <see cref="T:System.Xml.XmlDocumentType" /> object.</summary>
		/// <param name="name">Name of the document type. </param>
		/// <param name="publicId">The public identifier of the document type or <see langword="null" />. You can specify a public URI and also a system identifier to identify the location of the external DTD subset.</param>
		/// <param name="systemId">The system identifier of the document type or <see langword="null" />. Specifies the URL of the file location for the external DTD subset.</param>
		/// <param name="internalSubset">The DTD internal subset of the document type or <see langword="null" />. </param>
		/// <returns>The new <see langword="XmlDocumentType" />.</returns>
		// Token: 0x06001085 RID: 4229 RVA: 0x00068431 File Offset: 0x00066631
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public virtual XmlDocumentType CreateDocumentType(string name, string publicId, string systemId, string internalSubset)
		{
			return new XmlDocumentType(name, publicId, systemId, internalSubset, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlDocumentFragment" />.</summary>
		/// <returns>The new <see langword="XmlDocumentFragment" />.</returns>
		// Token: 0x06001086 RID: 4230 RVA: 0x0006843E File Offset: 0x0006663E
		public virtual XmlDocumentFragment CreateDocumentFragment()
		{
			return new XmlDocumentFragment(this);
		}

		/// <summary>Creates an element with the specified name.</summary>
		/// <param name="name">The qualified name of the element. If the name contains a colon then the <see cref="P:System.Xml.XmlNode.Prefix" /> property reflects the part of the name preceding the colon and the <see cref="P:System.Xml.XmlDocument.LocalName" /> property reflects the part of the name after the colon. The qualified name cannot include a prefix of'xmlns'. </param>
		/// <returns>The new <see langword="XmlElement" />.</returns>
		// Token: 0x06001087 RID: 4231 RVA: 0x00068448 File Offset: 0x00066648
		public XmlElement CreateElement(string name)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(name, out empty, out empty2);
			return this.CreateElement(empty, empty2, string.Empty);
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00068478 File Offset: 0x00066678
		internal void AddDefaultAttributes(XmlElement elem)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			SchemaElementDecl schemaElementDecl = this.GetSchemaElementDecl(elem);
			if (schemaElementDecl != null && schemaElementDecl.AttDefs != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = schemaElementDecl.AttDefs.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)dictionaryEnumerator.Value;
					if (schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed)
					{
						string attrPrefix = string.Empty;
						string name = schemaAttDef.Name.Name;
						string attrNamespaceURI = string.Empty;
						if (dtdSchemaInfo.SchemaType == SchemaType.DTD)
						{
							attrPrefix = schemaAttDef.Name.Namespace;
						}
						else
						{
							attrPrefix = schemaAttDef.Prefix;
							attrNamespaceURI = schemaAttDef.Name.Namespace;
						}
						XmlAttribute attributeNode = this.PrepareDefaultAttribute(schemaAttDef, attrPrefix, name, attrNamespaceURI);
						elem.SetAttributeNode(attributeNode);
					}
				}
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00068540 File Offset: 0x00066740
		private SchemaElementDecl GetSchemaElementDecl(XmlElement elem)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			if (dtdSchemaInfo != null)
			{
				XmlQualifiedName key = new XmlQualifiedName(elem.LocalName, (dtdSchemaInfo.SchemaType == SchemaType.DTD) ? elem.Prefix : elem.NamespaceURI);
				SchemaElementDecl result;
				if (dtdSchemaInfo.ElementDecls.TryGetValue(key, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00068590 File Offset: 0x00066790
		private XmlAttribute PrepareDefaultAttribute(SchemaAttDef attdef, string attrPrefix, string attrLocalname, string attrNamespaceURI)
		{
			this.SetDefaultNamespace(attrPrefix, attrLocalname, ref attrNamespaceURI);
			XmlAttribute xmlAttribute = this.CreateDefaultAttribute(attrPrefix, attrLocalname, attrNamespaceURI);
			xmlAttribute.InnerXml = attdef.DefaultValueRaw;
			XmlUnspecifiedAttribute xmlUnspecifiedAttribute = xmlAttribute as XmlUnspecifiedAttribute;
			if (xmlUnspecifiedAttribute != null)
			{
				xmlUnspecifiedAttribute.SetSpecified(false);
			}
			return xmlAttribute;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlEntityReference" /> with the specified name.</summary>
		/// <param name="name">The name of the entity reference. </param>
		/// <returns>The new <see langword="XmlEntityReference" />.</returns>
		/// <exception cref="T:System.ArgumentException">The name is invalid (for example, names starting with'#' are invalid.) </exception>
		// Token: 0x0600108B RID: 4235 RVA: 0x000685CE File Offset: 0x000667CE
		public virtual XmlEntityReference CreateEntityReference(string name)
		{
			return new XmlEntityReference(name, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlProcessingInstruction" /> with the specified name and data.</summary>
		/// <param name="target">The name of the processing instruction. </param>
		/// <param name="data">The data for the processing instruction. </param>
		/// <returns>The new <see langword="XmlProcessingInstruction" />.</returns>
		// Token: 0x0600108C RID: 4236 RVA: 0x000685D7 File Offset: 0x000667D7
		public virtual XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new XmlProcessingInstruction(target, data, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlDeclaration" /> node with the specified values.</summary>
		/// <param name="version">The version must be "1.0". </param>
		/// <param name="encoding">The value of the encoding attribute. This is the encoding that is used when you save the <see cref="T:System.Xml.XmlDocument" /> to a file or a stream; therefore, it must be set to a string supported by the <see cref="T:System.Text.Encoding" /> class, otherwise <see cref="M:System.Xml.XmlDocument.Save(System.String)" /> fails. If this is <see langword="null" /> or String.Empty, the <see langword="Save" /> method does not write an encoding attribute on the XML declaration and therefore the default encoding, UTF-8, is used.Note: If the <see langword="XmlDocument" /> is saved to either a <see cref="T:System.IO.TextWriter" /> or an <see cref="T:System.Xml.XmlTextWriter" />, this encoding value is discarded. Instead, the encoding of the <see langword="TextWriter" /> or the <see langword="XmlTextWriter" /> is used. This ensures that the XML written out can be read back using the correct encoding. </param>
		/// <param name="standalone">The value must be either "yes" or "no". If this is <see langword="null" /> or String.Empty, the <see langword="Save" /> method does not write a standalone attribute on the XML declaration. </param>
		/// <returns>The new <see langword="XmlDeclaration" /> node.</returns>
		/// <exception cref="T:System.ArgumentException">The values of <paramref name="version" /> or <paramref name="standalone" /> are something other than the ones specified above. </exception>
		// Token: 0x0600108D RID: 4237 RVA: 0x000685E1 File Offset: 0x000667E1
		public virtual XmlDeclaration CreateXmlDeclaration(string version, string encoding, string standalone)
		{
			return new XmlDeclaration(version, encoding, standalone, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlText" /> with the specified text.</summary>
		/// <param name="text">The text for the Text node. </param>
		/// <returns>The new <see langword="XmlText" /> node.</returns>
		// Token: 0x0600108E RID: 4238 RVA: 0x000685EC File Offset: 0x000667EC
		public virtual XmlText CreateTextNode(string text)
		{
			return new XmlText(text, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlSignificantWhitespace" /> node.</summary>
		/// <param name="text">The string must contain only the following characters &amp;#20; &amp;#10; &amp;#13; and &amp;#9; </param>
		/// <returns>A new <see langword="XmlSignificantWhitespace" /> node.</returns>
		// Token: 0x0600108F RID: 4239 RVA: 0x000685F5 File Offset: 0x000667F5
		public virtual XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new XmlSignificantWhitespace(text, this);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating this document.</summary>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x06001090 RID: 4240 RVA: 0x000685FE File Offset: 0x000667FE
		public override XPathNavigator CreateNavigator()
		{
			return this.CreateNavigator(this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating this document positioned on the <see cref="T:System.Xml.XmlNode" /> specified.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> you want the navigator initially positioned on. </param>
		/// <returns>An <see cref="T:System.Xml.XPath.XPathNavigator" /> object.</returns>
		// Token: 0x06001091 RID: 4241 RVA: 0x00068608 File Offset: 0x00066808
		protected internal virtual XPathNavigator CreateNavigator(XmlNode node)
		{
			switch (node.NodeType)
			{
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.SignificantWhitespace:
			{
				XmlNode parentNode = node.ParentNode;
				if (parentNode != null)
				{
					for (;;)
					{
						XmlNodeType nodeType = parentNode.NodeType;
						if (nodeType == XmlNodeType.Attribute)
						{
							break;
						}
						if (nodeType != XmlNodeType.EntityReference)
						{
							goto IL_74;
						}
						parentNode = parentNode.ParentNode;
						if (parentNode == null)
						{
							goto IL_74;
						}
					}
					return null;
				}
				IL_74:
				node = this.NormalizeText(node);
				break;
			}
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return null;
			case XmlNodeType.Whitespace:
			{
				XmlNode parentNode = node.ParentNode;
				if (parentNode != null)
				{
					for (;;)
					{
						XmlNodeType nodeType = parentNode.NodeType;
						if (nodeType == XmlNodeType.Document || nodeType == XmlNodeType.Attribute)
						{
							break;
						}
						if (nodeType != XmlNodeType.EntityReference)
						{
							goto IL_A9;
						}
						parentNode = parentNode.ParentNode;
						if (parentNode == null)
						{
							goto IL_A9;
						}
					}
					return null;
				}
				IL_A9:
				node = this.NormalizeText(node);
				break;
			}
			}
			return new DocumentXPathNavigator(this, node);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x000686CE File Offset: 0x000668CE
		internal static bool IsTextNode(XmlNodeType nt)
		{
			return nt - XmlNodeType.Text <= 1 || nt - XmlNodeType.Whitespace <= 1;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000686E0 File Offset: 0x000668E0
		private XmlNode NormalizeText(XmlNode n)
		{
			XmlNode xmlNode = null;
			while (XmlDocument.IsTextNode(n.NodeType))
			{
				xmlNode = n;
				n = n.PreviousSibling;
				if (n == null)
				{
					XmlNode xmlNode2 = xmlNode;
					while (xmlNode2.ParentNode != null && xmlNode2.ParentNode.NodeType == XmlNodeType.EntityReference)
					{
						if (xmlNode2.ParentNode.PreviousSibling != null)
						{
							n = xmlNode2.ParentNode.PreviousSibling;
							break;
						}
						xmlNode2 = xmlNode2.ParentNode;
						if (xmlNode2 == null)
						{
							break;
						}
					}
				}
				if (n == null)
				{
					break;
				}
				while (n.NodeType == XmlNodeType.EntityReference)
				{
					n = n.LastChild;
				}
			}
			return xmlNode;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlWhitespace" /> node.</summary>
		/// <param name="text">The string must contain only the following characters &amp;#20; &amp;#10; &amp;#13; and &amp;#9; </param>
		/// <returns>A new <see langword="XmlWhitespace" /> node.</returns>
		// Token: 0x06001094 RID: 4244 RVA: 0x00068760 File Offset: 0x00066960
		public virtual XmlWhitespace CreateWhitespace(string text)
		{
			return new XmlWhitespace(text, this);
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlDocument.Name" />.</summary>
		/// <param name="name">The qualified name to match. It is matched against the <see langword="Name" /> property of the matching node. The special value "*" matches all tags. </param>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes. If no nodes match <paramref name="name" />, the returned collection will be empty.</returns>
		// Token: 0x06001095 RID: 4245 RVA: 0x00068769 File Offset: 0x00066969
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			return new XmlElementList(this, name);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlAttribute" /> with the specified qualified name and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="qualifiedName">The qualified name of the attribute. If the name contains a colon then the <see cref="P:System.Xml.XmlNode.Prefix" /> property will reflect the part of the name preceding the colon and the <see cref="P:System.Xml.XmlDocument.LocalName" /> property will reflect the part of the name after the colon. </param>
		/// <param name="namespaceURI">The namespaceURI of the attribute. If the qualified name includes a prefix of xmlns, then this parameter must be http://www.w3.org/2000/xmlns/. </param>
		/// <returns>The new <see langword="XmlAttribute" />.</returns>
		// Token: 0x06001096 RID: 4246 RVA: 0x00068774 File Offset: 0x00066974
		public XmlAttribute CreateAttribute(string qualifiedName, string namespaceURI)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(qualifiedName, out empty, out empty2);
			return this.CreateAttribute(empty, empty2, namespaceURI);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlElement" /> with the qualified name and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="qualifiedName">The qualified name of the element. If the name contains a colon then the <see cref="P:System.Xml.XmlNode.Prefix" /> property will reflect the part of the name preceding the colon and the <see cref="P:System.Xml.XmlDocument.LocalName" /> property will reflect the part of the name after the colon. The qualified name cannot include a prefix of'xmlns'. </param>
		/// <param name="namespaceURI">The namespace URI of the element. </param>
		/// <returns>The new <see langword="XmlElement" />.</returns>
		// Token: 0x06001097 RID: 4247 RVA: 0x000687A0 File Offset: 0x000669A0
		public XmlElement CreateElement(string qualifiedName, string namespaceURI)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			XmlNode.SplitName(qualifiedName, out empty, out empty2);
			return this.CreateElement(empty, empty2, namespaceURI);
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlDocument.LocalName" /> and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="localName">The LocalName to match. The special value "*" matches all tags. </param>
		/// <param name="namespaceURI">NamespaceURI to match. </param>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes. If no nodes match the specified <paramref name="localName" /> and <paramref name="namespaceURI" />, the returned collection will be empty.</returns>
		// Token: 0x06001098 RID: 4248 RVA: 0x000687CC File Offset: 0x000669CC
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			return new XmlElementList(this, localName, namespaceURI);
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlElement" /> with the specified ID.</summary>
		/// <param name="elementId">The attribute ID to match. </param>
		/// <returns>The <see langword="XmlElement" /> with the matching ID or <see langword="null" /> if no matching element is found.</returns>
		// Token: 0x06001099 RID: 4249 RVA: 0x000687D8 File Offset: 0x000669D8
		public virtual XmlElement GetElementById(string elementId)
		{
			if (this.htElementIdMap != null)
			{
				ArrayList arrayList = (ArrayList)this.htElementIdMap[elementId];
				if (arrayList != null)
				{
					foreach (object obj in arrayList)
					{
						XmlElement xmlElement = (XmlElement)((WeakReference)obj).Target;
						if (xmlElement != null && xmlElement.IsConnected())
						{
							return xmlElement;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Imports a node from another document to the current document.</summary>
		/// <param name="node">The node being imported. </param>
		/// <param name="deep">
		///       <see langword="true" /> to perform a deep clone; otherwise, <see langword="false" />. </param>
		/// <returns>The imported <see cref="T:System.Xml.XmlNode" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Calling this method on a node type which cannot be imported. </exception>
		// Token: 0x0600109A RID: 4250 RVA: 0x00068864 File Offset: 0x00066A64
		public virtual XmlNode ImportNode(XmlNode node, bool deep)
		{
			return this.ImportNodeInternal(node, deep);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x00068870 File Offset: 0x00066A70
		private XmlNode ImportNodeInternal(XmlNode node, bool deep)
		{
			if (node == null)
			{
				throw new InvalidOperationException(Res.GetString("Cannot import a null node."));
			}
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
			{
				XmlNode xmlNode = this.CreateElement(node.Prefix, node.LocalName, node.NamespaceURI);
				this.ImportAttributes(node, xmlNode);
				if (deep)
				{
					this.ImportChildren(node, xmlNode, deep);
					return xmlNode;
				}
				return xmlNode;
			}
			case XmlNodeType.Attribute:
			{
				XmlNode xmlNode = this.CreateAttribute(node.Prefix, node.LocalName, node.NamespaceURI);
				this.ImportChildren(node, xmlNode, true);
				return xmlNode;
			}
			case XmlNodeType.Text:
				return this.CreateTextNode(node.Value);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(node.Value);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(node.Name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(node.Name, node.Value);
			case XmlNodeType.Comment:
				return this.CreateComment(node.Value);
			case XmlNodeType.DocumentType:
			{
				XmlDocumentType xmlDocumentType = (XmlDocumentType)node;
				return this.CreateDocumentType(xmlDocumentType.Name, xmlDocumentType.PublicId, xmlDocumentType.SystemId, xmlDocumentType.InternalSubset);
			}
			case XmlNodeType.DocumentFragment:
			{
				XmlNode xmlNode = this.CreateDocumentFragment();
				if (deep)
				{
					this.ImportChildren(node, xmlNode, deep);
					return xmlNode;
				}
				return xmlNode;
			}
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(node.Value);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(node.Value);
			case XmlNodeType.XmlDeclaration:
			{
				XmlDeclaration xmlDeclaration = (XmlDeclaration)node;
				return this.CreateXmlDeclaration(xmlDeclaration.Version, xmlDeclaration.Encoding, xmlDeclaration.Standalone);
			}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Res.GetString("Cannot import nodes of type '{0}'."), node.NodeType.ToString()));
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x00068A54 File Offset: 0x00066C54
		private void ImportAttributes(XmlNode fromElem, XmlNode toElem)
		{
			int count = fromElem.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				if (fromElem.Attributes[i].Specified)
				{
					toElem.Attributes.SetNamedItem(this.ImportNodeInternal(fromElem.Attributes[i], true));
				}
			}
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x00068AAC File Offset: 0x00066CAC
		private void ImportChildren(XmlNode fromNode, XmlNode toNode, bool deep)
		{
			for (XmlNode xmlNode = fromNode.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				toNode.AppendChild(this.ImportNodeInternal(xmlNode, deep));
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>An <see langword="XmlNameTable" /> enabling you to get the atomized version of a string within the document.</returns>
		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x00068ADB File Offset: 0x00066CDB
		public XmlNameTable NameTable
		{
			get
			{
				return this.implementation.NameTable;
			}
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlAttribute" /> with the specified <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.LocalName" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="prefix">The prefix of the attribute (if any). String.Empty and <see langword="null" /> are equivalent. </param>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute (if any). String.Empty and <see langword="null" /> are equivalent. If <paramref name="prefix" /> is xmlns, then this parameter must be http://www.w3.org/2000/xmlns/; otherwise an exception is thrown. </param>
		/// <returns>The new <see langword="XmlAttribute" />.</returns>
		// Token: 0x0600109F RID: 4255 RVA: 0x00068AE8 File Offset: 0x00066CE8
		public virtual XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new XmlAttribute(this.AddAttrXmlName(prefix, localName, namespaceURI, null), this);
		}

		/// <summary>Creates a default attribute with the specified prefix, local name and namespace URI.</summary>
		/// <param name="prefix">The prefix of the attribute (if any). </param>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute (if any). </param>
		/// <returns>The new <see cref="T:System.Xml.XmlAttribute" />.</returns>
		// Token: 0x060010A0 RID: 4256 RVA: 0x00068AFA File Offset: 0x00066CFA
		protected internal virtual XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new XmlUnspecifiedAttribute(prefix, localName, namespaceURI, this);
		}

		/// <summary>Creates an element with the specified <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.LocalName" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="prefix">The prefix of the new element (if any). String.Empty and <see langword="null" /> are equivalent. </param>
		/// <param name="localName">The local name of the new element. </param>
		/// <param name="namespaceURI">The namespace URI of the new element (if any). String.Empty and <see langword="null" /> are equivalent. </param>
		/// <returns>The new <see cref="T:System.Xml.XmlElement" />.</returns>
		// Token: 0x060010A1 RID: 4257 RVA: 0x00068B08 File Offset: 0x00066D08
		public virtual XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			XmlElement xmlElement = new XmlElement(this.AddXmlName(prefix, localName, namespaceURI, null), true, this);
			if (!this.IsLoading)
			{
				this.AddDefaultAttributes(xmlElement);
			}
			return xmlElement;
		}

		/// <summary>Gets or sets a value indicating whether to preserve white space in element content.</summary>
		/// <returns>
		///     <see langword="true" /> to preserve white space; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x00068B37 File Offset: 0x00066D37
		// (set) Token: 0x060010A3 RID: 4259 RVA: 0x00068B3F File Offset: 0x00066D3F
		public bool PreserveWhitespace
		{
			get
			{
				return this.preserveWhitespace;
			}
			set
			{
				this.preserveWhitespace = value;
			}
		}

		/// <summary>Gets a value indicating whether the current node is read-only.</summary>
		/// <returns>
		///     <see langword="true" /> if the current node is read-only; otherwise <see langword="false" />. <see langword="XmlDocument" /> nodes always return <see langword="false" />.</returns>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00068B48 File Offset: 0x00066D48
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x00068B64 File Offset: 0x00066D64
		internal XmlNamedNodeMap Entities
		{
			get
			{
				if (this.entities == null)
				{
					this.entities = new XmlNamedNodeMap(this);
				}
				return this.entities;
			}
			set
			{
				this.entities = value;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00068B6D File Offset: 0x00066D6D
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00068B75 File Offset: 0x00066D75
		internal bool IsLoading
		{
			get
			{
				return this.isLoading;
			}
			set
			{
				this.isLoading = value;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x00068B7E File Offset: 0x00066D7E
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00068B86 File Offset: 0x00066D86
		internal bool ActualLoadingStatus
		{
			get
			{
				return this.actualLoadingStatus;
			}
			set
			{
				this.actualLoadingStatus = value;
			}
		}

		/// <summary>Creates a <see cref="T:System.Xml.XmlNode" /> with the specified <see cref="T:System.Xml.XmlNodeType" />, <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.Name" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="type">The <see langword="XmlNodeType" /> of the new node. </param>
		/// <param name="prefix">The prefix of the new node. </param>
		/// <param name="name">The local name of the new node. </param>
		/// <param name="namespaceURI">The namespace URI of the new node. </param>
		/// <returns>The new <see langword="XmlNode" />.</returns>
		/// <exception cref="T:System.ArgumentException">The name was not provided and the <see langword="XmlNodeType" /> requires a name. </exception>
		// Token: 0x060010AB RID: 4267 RVA: 0x00068B90 File Offset: 0x00066D90
		public virtual XmlNode CreateNode(XmlNodeType type, string prefix, string name, string namespaceURI)
		{
			switch (type)
			{
			case XmlNodeType.Element:
				if (prefix != null)
				{
					return this.CreateElement(prefix, name, namespaceURI);
				}
				return this.CreateElement(name, namespaceURI);
			case XmlNodeType.Attribute:
				if (prefix != null)
				{
					return this.CreateAttribute(prefix, name, namespaceURI);
				}
				return this.CreateAttribute(name, namespaceURI);
			case XmlNodeType.Text:
				return this.CreateTextNode(string.Empty);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(string.Empty);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(name, string.Empty);
			case XmlNodeType.Comment:
				return this.CreateComment(string.Empty);
			case XmlNodeType.Document:
				return new XmlDocument();
			case XmlNodeType.DocumentType:
				return this.CreateDocumentType(name, string.Empty, string.Empty, string.Empty);
			case XmlNodeType.DocumentFragment:
				return this.CreateDocumentFragment();
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(string.Empty);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(string.Empty);
			case XmlNodeType.XmlDeclaration:
				return this.CreateXmlDeclaration("1.0", null, null);
			}
			throw new ArgumentException(Res.GetString("Cannot create node of type {0}.", new object[]
			{
				type
			}));
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlNode" /> with the specified node type, <see cref="P:System.Xml.XmlDocument.Name" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="nodeTypeString">String version of the <see cref="T:System.Xml.XmlNodeType" /> of the new node. This parameter must be one of the values listed in the table below. </param>
		/// <param name="name">The qualified name of the new node. If the name contains a colon, it is parsed into <see cref="P:System.Xml.XmlNode.Prefix" /> and <see cref="P:System.Xml.XmlDocument.LocalName" /> components. </param>
		/// <param name="namespaceURI">The namespace URI of the new node. </param>
		/// <returns>The new <see langword="XmlNode" />.</returns>
		/// <exception cref="T:System.ArgumentException">The name was not provided and the <see langword="XmlNodeType" /> requires a name; or <paramref name="nodeTypeString" /> is not one of the strings listed below. </exception>
		// Token: 0x060010AC RID: 4268 RVA: 0x00068CBF File Offset: 0x00066EBF
		public virtual XmlNode CreateNode(string nodeTypeString, string name, string namespaceURI)
		{
			return this.CreateNode(this.ConvertToNodeType(nodeTypeString), name, namespaceURI);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlNode" /> with the specified <see cref="T:System.Xml.XmlNodeType" />, <see cref="P:System.Xml.XmlDocument.Name" />, and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="type">The <see langword="XmlNodeType" /> of the new node. </param>
		/// <param name="name">The qualified name of the new node. If the name contains a colon then it is parsed into <see cref="P:System.Xml.XmlNode.Prefix" /> and <see cref="P:System.Xml.XmlDocument.LocalName" /> components. </param>
		/// <param name="namespaceURI">The namespace URI of the new node. </param>
		/// <returns>The new <see langword="XmlNode" />.</returns>
		/// <exception cref="T:System.ArgumentException">The name was not provided and the <see langword="XmlNodeType" /> requires a name. </exception>
		// Token: 0x060010AD RID: 4269 RVA: 0x00068CD0 File Offset: 0x00066ED0
		public virtual XmlNode CreateNode(XmlNodeType type, string name, string namespaceURI)
		{
			return this.CreateNode(type, null, name, namespaceURI);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlNode" /> object based on the information in the <see cref="T:System.Xml.XmlReader" />. The reader must be positioned on a node or attribute.</summary>
		/// <param name="reader">The XML source </param>
		/// <returns>The new <see langword="XmlNode" /> or <see langword="null" /> if no more nodes exist.</returns>
		/// <exception cref="T:System.NullReferenceException">The reader is positioned on a node type that does not translate to a valid DOM node (for example, EndElement or EndEntity). </exception>
		// Token: 0x060010AE RID: 4270 RVA: 0x00068CDC File Offset: 0x00066EDC
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		public virtual XmlNode ReadNode(XmlReader reader)
		{
			XmlNode result = null;
			try
			{
				this.IsLoading = true;
				result = new XmlLoader().ReadCurrentNode(this, reader);
			}
			finally
			{
				this.IsLoading = false;
			}
			return result;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00068D1C File Offset: 0x00066F1C
		internal XmlNodeType ConvertToNodeType(string nodeTypeString)
		{
			if (nodeTypeString == "element")
			{
				return XmlNodeType.Element;
			}
			if (nodeTypeString == "attribute")
			{
				return XmlNodeType.Attribute;
			}
			if (nodeTypeString == "text")
			{
				return XmlNodeType.Text;
			}
			if (nodeTypeString == "cdatasection")
			{
				return XmlNodeType.CDATA;
			}
			if (nodeTypeString == "entityreference")
			{
				return XmlNodeType.EntityReference;
			}
			if (nodeTypeString == "entity")
			{
				return XmlNodeType.Entity;
			}
			if (nodeTypeString == "processinginstruction")
			{
				return XmlNodeType.ProcessingInstruction;
			}
			if (nodeTypeString == "comment")
			{
				return XmlNodeType.Comment;
			}
			if (nodeTypeString == "document")
			{
				return XmlNodeType.Document;
			}
			if (nodeTypeString == "documenttype")
			{
				return XmlNodeType.DocumentType;
			}
			if (nodeTypeString == "documentfragment")
			{
				return XmlNodeType.DocumentFragment;
			}
			if (nodeTypeString == "notation")
			{
				return XmlNodeType.Notation;
			}
			if (nodeTypeString == "significantwhitespace")
			{
				return XmlNodeType.SignificantWhitespace;
			}
			if (nodeTypeString == "whitespace")
			{
				return XmlNodeType.Whitespace;
			}
			throw new ArgumentException(Res.GetString("'{0}' does not represent any 'XmlNodeType'.", new object[]
			{
				nodeTypeString
			}));
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00068E1A File Offset: 0x0006701A
		private XmlTextReader SetupReader(XmlTextReader tr)
		{
			tr.XmlValidatingReaderCompatibilityMode = true;
			tr.EntityHandling = EntityHandling.ExpandCharEntities;
			if (this.HasSetResolver)
			{
				tr.XmlResolver = this.GetResolver();
			}
			return tr;
		}

		/// <summary>Loads the XML document from the specified URL.</summary>
		/// <param name="filename">URL for the file containing the XML document to load. The URL can be either a local file or an HTTP URL (a Web address).</param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, a <see cref="T:System.IO.FileNotFoundException" /> is raised. </exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="filename" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="filename" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive). </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">
		///         <paramref name="filename" /> specified a file that is read-only.-or- This operation is not supported on the current platform.-or- 
		///         <paramref name="filename" /> specified a directory.-or- The caller does not have the required permission. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified in <paramref name="filename" /> was not found. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///         <paramref name="filename" /> is in an invalid format. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x060010B1 RID: 4273 RVA: 0x00068E40 File Offset: 0x00067040
		public virtual void Load(string filename)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(filename, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Loads the XML document from the specified stream.</summary>
		/// <param name="inStream">The stream containing the XML document to load. </param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, a <see cref="T:System.IO.FileNotFoundException" /> is raised. </exception>
		// Token: 0x060010B2 RID: 4274 RVA: 0x00068E80 File Offset: 0x00067080
		public virtual void Load(Stream inStream)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(inStream, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Impl.Close(false);
			}
		}

		/// <summary>Loads the XML document from the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="txtReader">The <see langword="TextReader" /> used to feed the XML data into the document. </param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, the document remains empty. </exception>
		// Token: 0x060010B3 RID: 4275 RVA: 0x00068EC8 File Offset: 0x000670C8
		public virtual void Load(TextReader txtReader)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(txtReader, this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Impl.Close(false);
			}
		}

		/// <summary>Loads the XML document from the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see langword="XmlReader" /> used to feed the XML data into the document. </param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, the document remains empty. </exception>
		// Token: 0x060010B4 RID: 4276 RVA: 0x00068F10 File Offset: 0x00067110
		public virtual void Load(XmlReader reader)
		{
			try
			{
				this.IsLoading = true;
				this.actualLoadingStatus = true;
				this.RemoveAll();
				this.fEntRefNodesPresent = false;
				this.fCDataNodesPresent = false;
				this.reportValidity = true;
				new XmlLoader().Load(this, reader, this.preserveWhitespace);
			}
			finally
			{
				this.IsLoading = false;
				this.actualLoadingStatus = false;
				this.reportValidity = true;
			}
		}

		/// <summary>Loads the XML document from the specified string.</summary>
		/// <param name="xml">String containing the XML document to load. </param>
		/// <exception cref="T:System.Xml.XmlException">There is a load or parse error in the XML. In this case, the document remains empty. </exception>
		// Token: 0x060010B5 RID: 4277 RVA: 0x00068F80 File Offset: 0x00067180
		public virtual void LoadXml(string xml)
		{
			XmlTextReader xmlTextReader = this.SetupReader(new XmlTextReader(new StringReader(xml), this.NameTable));
			try
			{
				this.Load(xmlTextReader);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00068FC8 File Offset: 0x000671C8
		internal Encoding TextEncoding
		{
			get
			{
				if (this.Declaration != null)
				{
					string encoding = this.Declaration.Encoding;
					if (encoding.Length > 0)
					{
						return System.Text.Encoding.GetEncoding(encoding);
					}
				}
				return null;
			}
		}

		/// <summary>
		///     Throws an <see cref="T:System.InvalidOperationException" /> in all cases.</summary>
		/// <returns>The values of the node and all its child nodes.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x170002C2 RID: 706
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x00068FFA File Offset: 0x000671FA
		public override string InnerText
		{
			set
			{
				throw new InvalidOperationException(Res.GetString("The 'InnerText' of a 'Document' node is read-only and cannot be set."));
			}
		}

		/// <summary>Gets or sets the markup representing the children of the current node.</summary>
		/// <returns>The markup of the children of the current node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0006900B File Offset: 0x0006720B
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x00069013 File Offset: 0x00067213
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.LoadXml(value);
			}
		}

		/// <summary>Saves the XML document to the specified file. If the specified file exists, this method overwrites it.</summary>
		/// <param name="filename">The location of the file where you want to save the document. </param>
		/// <exception cref="T:System.Xml.XmlException">The operation would not result in a well formed XML document (for example, no document element or duplicate XML declarations). </exception>
		// Token: 0x060010BA RID: 4282 RVA: 0x0006901C File Offset: 0x0006721C
		public virtual void Save(string filename)
		{
			if (this.DocumentElement == null)
			{
				throw new XmlException("Invalid XML document. {0}", Res.GetString("The document does not have a root element."));
			}
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(filename, this.TextEncoding);
			try
			{
				if (!this.preserveWhitespace)
				{
					xmlDOMTextWriter.Formatting = Formatting.Indented;
				}
				this.WriteTo(xmlDOMTextWriter);
				xmlDOMTextWriter.Flush();
			}
			finally
			{
				xmlDOMTextWriter.Close();
			}
		}

		/// <summary>Saves the XML document to the specified stream.</summary>
		/// <param name="outStream">The stream to which you want to save. </param>
		/// <exception cref="T:System.Xml.XmlException">The operation would not result in a well formed XML document (for example, no document element or duplicate XML declarations). </exception>
		// Token: 0x060010BB RID: 4283 RVA: 0x00069088 File Offset: 0x00067288
		public virtual void Save(Stream outStream)
		{
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(outStream, this.TextEncoding);
			if (!this.preserveWhitespace)
			{
				xmlDOMTextWriter.Formatting = Formatting.Indented;
			}
			this.WriteTo(xmlDOMTextWriter);
			xmlDOMTextWriter.Flush();
		}

		/// <summary>Saves the XML document to the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see langword="TextWriter" /> to which you want to save. </param>
		/// <exception cref="T:System.Xml.XmlException">The operation would not result in a well formed XML document (for example, no document element or duplicate XML declarations). </exception>
		// Token: 0x060010BC RID: 4284 RVA: 0x000690C0 File Offset: 0x000672C0
		public virtual void Save(TextWriter writer)
		{
			XmlDOMTextWriter xmlDOMTextWriter = new XmlDOMTextWriter(writer);
			if (!this.preserveWhitespace)
			{
				xmlDOMTextWriter.Formatting = Formatting.Indented;
			}
			this.Save(xmlDOMTextWriter);
		}

		/// <summary>Saves the XML document to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		/// <exception cref="T:System.Xml.XmlException">The operation would not result in a well formed XML document (for example, no document element or duplicate XML declarations). </exception>
		// Token: 0x060010BD RID: 4285 RVA: 0x000690EC File Offset: 0x000672EC
		public virtual void Save(XmlWriter w)
		{
			XmlNode xmlNode = this.FirstChild;
			if (xmlNode == null)
			{
				return;
			}
			if (w.WriteState == WriteState.Start)
			{
				if (xmlNode is XmlDeclaration)
				{
					if (this.Standalone.Length == 0)
					{
						w.WriteStartDocument();
					}
					else if (this.Standalone == "yes")
					{
						w.WriteStartDocument(true);
					}
					else if (this.Standalone == "no")
					{
						w.WriteStartDocument(false);
					}
					xmlNode = xmlNode.NextSibling;
				}
				else
				{
					w.WriteStartDocument();
				}
			}
			while (xmlNode != null)
			{
				xmlNode.WriteTo(w);
				xmlNode = xmlNode.NextSibling;
			}
			w.Flush();
		}

		/// <summary>Saves the <see langword="XmlDocument" /> node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060010BE RID: 4286 RVA: 0x00069185 File Offset: 0x00067385
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		/// <summary>Saves all the children of the <see langword="XmlDocument" /> node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="xw">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060010BF RID: 4287 RVA: 0x00069190 File Offset: 0x00067390
		public override void WriteContentTo(XmlWriter xw)
		{
			foreach (object obj in this)
			{
				((XmlNode)obj).WriteTo(xw);
			}
		}

		/// <summary>Validates the <see cref="T:System.Xml.XmlDocument" /> against the XML Schema Definition Language (XSD) schemas contained in the <see cref="P:System.Xml.XmlDocument.Schemas" /> property.</summary>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> object that receives information about schema validation warnings and errors.</param>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">A schema validation event occurred and no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> object was specified.</exception>
		// Token: 0x060010C0 RID: 4288 RVA: 0x000691E4 File Offset: 0x000673E4
		public void Validate(ValidationEventHandler validationEventHandler)
		{
			this.Validate(validationEventHandler, this);
		}

		/// <summary>Validates the <see cref="T:System.Xml.XmlNode" /> object specified against the XML Schema Definition Language (XSD) schemas in the <see cref="P:System.Xml.XmlDocument.Schemas" /> property.</summary>
		/// <param name="validationEventHandler">The <see cref="T:System.Xml.Schema.ValidationEventHandler" /> object that receives information about schema validation warnings and errors.</param>
		/// <param name="nodeToValidate">The <see cref="T:System.Xml.XmlNode" /> object created from an <see cref="T:System.Xml.XmlDocument" /> to validate.</param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Xml.XmlNode" /> object parameter was not created from an <see cref="T:System.Xml.XmlDocument" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlNode" /> object parameter is not an element, attribute, document fragment, or the root node.</exception>
		/// <exception cref="T:System.Xml.Schema.XmlSchemaValidationException">A schema validation event occurred and no <see cref="T:System.Xml.Schema.ValidationEventHandler" /> object was specified.</exception>
		// Token: 0x060010C1 RID: 4289 RVA: 0x000691F0 File Offset: 0x000673F0
		public void Validate(ValidationEventHandler validationEventHandler, XmlNode nodeToValidate)
		{
			if (this.schemas == null || this.schemas.Count == 0)
			{
				throw new InvalidOperationException(Res.GetString("The XmlSchemaSet on the document is either null or has no schemas in it. Provide schema information before calling Validate."));
			}
			if (nodeToValidate.Document != this)
			{
				throw new ArgumentException(Res.GetString("Cannot validate '{0}' because its owner document is not the current document.", new object[]
				{
					"nodeToValidate"
				}));
			}
			if (nodeToValidate == this)
			{
				this.reportValidity = false;
			}
			new DocumentSchemaValidator(this, this.schemas, validationEventHandler).Validate(nodeToValidate);
			if (nodeToValidate == this)
			{
				this.reportValidity = true;
			}
		}

		/// <summary>Occurs when a node belonging to this document is about to be inserted into another node.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060010C2 RID: 4290 RVA: 0x00069273 File Offset: 0x00067473
		// (remove) Token: 0x060010C3 RID: 4291 RVA: 0x0006928C File Offset: 0x0006748C
		public event XmlNodeChangedEventHandler NodeInserting
		{
			add
			{
				this.onNodeInsertingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeInsertingDelegate, value);
			}
			remove
			{
				this.onNodeInsertingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeInsertingDelegate, value);
			}
		}

		/// <summary>Occurs when a node belonging to this document has been inserted into another node.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060010C4 RID: 4292 RVA: 0x000692A5 File Offset: 0x000674A5
		// (remove) Token: 0x060010C5 RID: 4293 RVA: 0x000692BE File Offset: 0x000674BE
		public event XmlNodeChangedEventHandler NodeInserted
		{
			add
			{
				this.onNodeInsertedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeInsertedDelegate, value);
			}
			remove
			{
				this.onNodeInsertedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeInsertedDelegate, value);
			}
		}

		/// <summary>Occurs when a node belonging to this document is about to be removed from the document.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060010C6 RID: 4294 RVA: 0x000692D7 File Offset: 0x000674D7
		// (remove) Token: 0x060010C7 RID: 4295 RVA: 0x000692F0 File Offset: 0x000674F0
		public event XmlNodeChangedEventHandler NodeRemoving
		{
			add
			{
				this.onNodeRemovingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeRemovingDelegate, value);
			}
			remove
			{
				this.onNodeRemovingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeRemovingDelegate, value);
			}
		}

		/// <summary>Occurs when a node belonging to this document has been removed from its parent.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060010C8 RID: 4296 RVA: 0x00069309 File Offset: 0x00067509
		// (remove) Token: 0x060010C9 RID: 4297 RVA: 0x00069322 File Offset: 0x00067522
		public event XmlNodeChangedEventHandler NodeRemoved
		{
			add
			{
				this.onNodeRemovedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeRemovedDelegate, value);
			}
			remove
			{
				this.onNodeRemovedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeRemovedDelegate, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Xml.XmlNode.Value" /> of a node belonging to this document is about to be changed.</summary>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060010CA RID: 4298 RVA: 0x0006933B File Offset: 0x0006753B
		// (remove) Token: 0x060010CB RID: 4299 RVA: 0x00069354 File Offset: 0x00067554
		public event XmlNodeChangedEventHandler NodeChanging
		{
			add
			{
				this.onNodeChangingDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeChangingDelegate, value);
			}
			remove
			{
				this.onNodeChangingDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeChangingDelegate, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Xml.XmlNode.Value" /> of a node belonging to this document has been changed.</summary>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060010CC RID: 4300 RVA: 0x0006936D File Offset: 0x0006756D
		// (remove) Token: 0x060010CD RID: 4301 RVA: 0x00069386 File Offset: 0x00067586
		public event XmlNodeChangedEventHandler NodeChanged
		{
			add
			{
				this.onNodeChangedDelegate = (XmlNodeChangedEventHandler)Delegate.Combine(this.onNodeChangedDelegate, value);
			}
			remove
			{
				this.onNodeChangedDelegate = (XmlNodeChangedEventHandler)Delegate.Remove(this.onNodeChangedDelegate, value);
			}
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000693A0 File Offset: 0x000675A0
		internal override XmlNodeChangedEventArgs GetEventArgs(XmlNode node, XmlNode oldParent, XmlNode newParent, string oldValue, string newValue, XmlNodeChangedAction action)
		{
			this.reportValidity = false;
			switch (action)
			{
			case XmlNodeChangedAction.Insert:
				if (this.onNodeInsertingDelegate == null && this.onNodeInsertedDelegate == null)
				{
					return null;
				}
				break;
			case XmlNodeChangedAction.Remove:
				if (this.onNodeRemovingDelegate == null && this.onNodeRemovedDelegate == null)
				{
					return null;
				}
				break;
			case XmlNodeChangedAction.Change:
				if (this.onNodeChangingDelegate == null && this.onNodeChangedDelegate == null)
				{
					return null;
				}
				break;
			}
			return new XmlNodeChangedEventArgs(node, oldParent, newParent, oldValue, newValue, action);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00069410 File Offset: 0x00067610
		internal XmlNodeChangedEventArgs GetInsertEventArgsForLoad(XmlNode node, XmlNode newParent)
		{
			if (this.onNodeInsertingDelegate == null && this.onNodeInsertedDelegate == null)
			{
				return null;
			}
			string value = node.Value;
			return new XmlNodeChangedEventArgs(node, null, newParent, value, value, XmlNodeChangedAction.Insert);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x00069444 File Offset: 0x00067644
		internal override void BeforeEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				switch (args.Action)
				{
				case XmlNodeChangedAction.Insert:
					if (this.onNodeInsertingDelegate != null)
					{
						this.onNodeInsertingDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Remove:
					if (this.onNodeRemovingDelegate != null)
					{
						this.onNodeRemovingDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Change:
					if (this.onNodeChangingDelegate != null)
					{
						this.onNodeChangingDelegate(this, args);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000694B0 File Offset: 0x000676B0
		internal override void AfterEvent(XmlNodeChangedEventArgs args)
		{
			if (args != null)
			{
				switch (args.Action)
				{
				case XmlNodeChangedAction.Insert:
					if (this.onNodeInsertedDelegate != null)
					{
						this.onNodeInsertedDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Remove:
					if (this.onNodeRemovedDelegate != null)
					{
						this.onNodeRemovedDelegate(this, args);
						return;
					}
					break;
				case XmlNodeChangedAction.Change:
					if (this.onNodeChangedDelegate != null)
					{
						this.onNodeChangedDelegate(this, args);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0006951C File Offset: 0x0006771C
		internal XmlAttribute GetDefaultAttribute(XmlElement elem, string attrPrefix, string attrLocalname, string attrNamespaceURI)
		{
			SchemaInfo dtdSchemaInfo = this.DtdSchemaInfo;
			SchemaElementDecl schemaElementDecl = this.GetSchemaElementDecl(elem);
			if (schemaElementDecl != null && schemaElementDecl.AttDefs != null)
			{
				IDictionaryEnumerator dictionaryEnumerator = schemaElementDecl.AttDefs.GetEnumerator();
				while (dictionaryEnumerator.MoveNext())
				{
					SchemaAttDef schemaAttDef = (SchemaAttDef)dictionaryEnumerator.Value;
					if ((schemaAttDef.Presence == SchemaDeclBase.Use.Default || schemaAttDef.Presence == SchemaDeclBase.Use.Fixed) && schemaAttDef.Name.Name == attrLocalname && ((dtdSchemaInfo.SchemaType == SchemaType.DTD && schemaAttDef.Name.Namespace == attrPrefix) || (dtdSchemaInfo.SchemaType != SchemaType.DTD && schemaAttDef.Name.Namespace == attrNamespaceURI)))
					{
						return this.PrepareDefaultAttribute(schemaAttDef, attrPrefix, attrLocalname, attrNamespaceURI);
					}
				}
			}
			return null;
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x000695DC File Offset: 0x000677DC
		internal string Version
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Version;
				}
				return null;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x000695FC File Offset: 0x000677FC
		internal string Encoding
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Encoding;
				}
				return null;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x0006961C File Offset: 0x0006781C
		internal string Standalone
		{
			get
			{
				XmlDeclaration declaration = this.Declaration;
				if (declaration != null)
				{
					return declaration.Standalone;
				}
				return null;
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0006963C File Offset: 0x0006783C
		internal XmlEntity GetEntityNode(string name)
		{
			if (this.DocumentType != null)
			{
				XmlNamedNodeMap xmlNamedNodeMap = this.DocumentType.Entities;
				if (xmlNamedNodeMap != null)
				{
					return (XmlEntity)xmlNamedNodeMap.GetNamedItem(name);
				}
			}
			return null;
		}

		/// <summary>Returns the Post-Schema-Validation-Infoset (PSVI) of the node.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object representing the PSVI of the node.</returns>
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x00069670 File Offset: 0x00067870
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				if (this.reportValidity)
				{
					XmlElement documentElement = this.DocumentElement;
					if (documentElement != null)
					{
						XmlSchemaValidity validity = documentElement.SchemaInfo.Validity;
						if (validity == XmlSchemaValidity.Valid)
						{
							return XmlDocument.ValidSchemaInfo;
						}
						if (validity == XmlSchemaValidity.Invalid)
						{
							return XmlDocument.InvalidSchemaInfo;
						}
					}
				}
				return XmlDocument.NotKnownSchemaInfo;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The location from which the node was loaded.</returns>
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x000696B6 File Offset: 0x000678B6
		public override string BaseURI
		{
			get
			{
				return this.baseURI;
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000696BE File Offset: 0x000678BE
		internal void SetBaseURI(string inBaseURI)
		{
			this.baseURI = inBaseURI;
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000696C8 File Offset: 0x000678C8
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			if (!this.IsValidChildType(newChild.NodeType))
			{
				throw new InvalidOperationException(Res.GetString("The specified node cannot be inserted as the valid child of this node, because the specified node is the wrong type."));
			}
			if (!this.CanInsertAfter(newChild, this.LastChild))
			{
				throw new InvalidOperationException(Res.GetString("Cannot insert the node in the specified location."));
			}
			XmlNodeChangedEventArgs insertEventArgsForLoad = this.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				this.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
			}
			else
			{
				xmlLinkedNode.next = this.lastChild.next;
				this.lastChild.next = xmlLinkedNode;
			}
			this.lastChild = xmlLinkedNode;
			xmlLinkedNode.SetParentForLoad(this);
			if (insertEventArgsForLoad != null)
			{
				this.AfterEvent(insertEventArgsForLoad);
			}
			return xmlLinkedNode;
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00069773 File Offset: 0x00067973
		internal bool HasEntityReferences
		{
			get
			{
				return this.fEntRefNodesPresent;
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x0006977C File Offset: 0x0006797C
		internal XmlAttribute NamespaceXml
		{
			get
			{
				if (this.namespaceXml == null)
				{
					this.namespaceXml = new XmlAttribute(this.AddAttrXmlName(this.strXmlns, this.strXml, this.strReservedXmlns, null), this);
					this.namespaceXml.Value = this.strReservedXml;
				}
				return this.namespaceXml;
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000697CD File Offset: 0x000679CD
		// Note: this type is marked as 'beforefieldinit'.
		static XmlDocument()
		{
		}

		// Token: 0x04001046 RID: 4166
		private XmlImplementation implementation;

		// Token: 0x04001047 RID: 4167
		private DomNameTable domNameTable;

		// Token: 0x04001048 RID: 4168
		private XmlLinkedNode lastChild;

		// Token: 0x04001049 RID: 4169
		private XmlNamedNodeMap entities;

		// Token: 0x0400104A RID: 4170
		private Hashtable htElementIdMap;

		// Token: 0x0400104B RID: 4171
		private Hashtable htElementIDAttrDecl;

		// Token: 0x0400104C RID: 4172
		private SchemaInfo schemaInfo;

		// Token: 0x0400104D RID: 4173
		private XmlSchemaSet schemas;

		// Token: 0x0400104E RID: 4174
		private bool reportValidity;

		// Token: 0x0400104F RID: 4175
		private bool actualLoadingStatus;

		// Token: 0x04001050 RID: 4176
		private XmlNodeChangedEventHandler onNodeInsertingDelegate;

		// Token: 0x04001051 RID: 4177
		private XmlNodeChangedEventHandler onNodeInsertedDelegate;

		// Token: 0x04001052 RID: 4178
		private XmlNodeChangedEventHandler onNodeRemovingDelegate;

		// Token: 0x04001053 RID: 4179
		private XmlNodeChangedEventHandler onNodeRemovedDelegate;

		// Token: 0x04001054 RID: 4180
		private XmlNodeChangedEventHandler onNodeChangingDelegate;

		// Token: 0x04001055 RID: 4181
		private XmlNodeChangedEventHandler onNodeChangedDelegate;

		// Token: 0x04001056 RID: 4182
		internal bool fEntRefNodesPresent;

		// Token: 0x04001057 RID: 4183
		internal bool fCDataNodesPresent;

		// Token: 0x04001058 RID: 4184
		private bool preserveWhitespace;

		// Token: 0x04001059 RID: 4185
		private bool isLoading;

		// Token: 0x0400105A RID: 4186
		internal string strDocumentName;

		// Token: 0x0400105B RID: 4187
		internal string strDocumentFragmentName;

		// Token: 0x0400105C RID: 4188
		internal string strCommentName;

		// Token: 0x0400105D RID: 4189
		internal string strTextName;

		// Token: 0x0400105E RID: 4190
		internal string strCDataSectionName;

		// Token: 0x0400105F RID: 4191
		internal string strEntityName;

		// Token: 0x04001060 RID: 4192
		internal string strID;

		// Token: 0x04001061 RID: 4193
		internal string strXmlns;

		// Token: 0x04001062 RID: 4194
		internal string strXml;

		// Token: 0x04001063 RID: 4195
		internal string strSpace;

		// Token: 0x04001064 RID: 4196
		internal string strLang;

		// Token: 0x04001065 RID: 4197
		internal string strEmpty;

		// Token: 0x04001066 RID: 4198
		internal string strNonSignificantWhitespaceName;

		// Token: 0x04001067 RID: 4199
		internal string strSignificantWhitespaceName;

		// Token: 0x04001068 RID: 4200
		internal string strReservedXmlns;

		// Token: 0x04001069 RID: 4201
		internal string strReservedXml;

		// Token: 0x0400106A RID: 4202
		internal string baseURI;

		// Token: 0x0400106B RID: 4203
		private XmlResolver resolver;

		// Token: 0x0400106C RID: 4204
		internal bool bSetResolver;

		// Token: 0x0400106D RID: 4205
		internal object objLock;

		// Token: 0x0400106E RID: 4206
		private XmlAttribute namespaceXml;

		// Token: 0x0400106F RID: 4207
		internal static EmptyEnumerator EmptyEnumerator = new EmptyEnumerator();

		// Token: 0x04001070 RID: 4208
		internal static IXmlSchemaInfo NotKnownSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.NotKnown);

		// Token: 0x04001071 RID: 4209
		internal static IXmlSchemaInfo ValidSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.Valid);

		// Token: 0x04001072 RID: 4210
		internal static IXmlSchemaInfo InvalidSchemaInfo = new XmlSchemaInfo(XmlSchemaValidity.Invalid);
	}
}
