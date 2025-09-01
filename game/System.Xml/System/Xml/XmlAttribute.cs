using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an attribute. Valid and default values for the attribute are defined in a document type definition (DTD) or schema.</summary>
	// Token: 0x020001B4 RID: 436
	public class XmlAttribute : XmlNode
	{
		// Token: 0x06000FD3 RID: 4051 RVA: 0x00066624 File Offset: 0x00064824
		internal XmlAttribute(XmlName name, XmlDocument doc) : base(doc)
		{
			this.parentNode = null;
			if (!doc.IsLoading)
			{
				XmlDocument.CheckName(name.Prefix);
				XmlDocument.CheckName(name.LocalName);
			}
			if (name.LocalName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The attribute local name cannot be empty."));
			}
			this.name = name;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00066681 File Offset: 0x00064881
		internal int LocalNameHash
		{
			get
			{
				return this.name.HashCode;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlAttribute" /> class.</summary>
		/// <param name="prefix">The namespace prefix.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace uniform resource identifier (URI).</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06000FD5 RID: 4053 RVA: 0x0006668E File Offset: 0x0006488E
		protected internal XmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc) : this(doc.AddAttrXmlName(prefix, localName, namespaceURI, null), doc)
		{
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x000666A3 File Offset: 0x000648A3
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x000666AB File Offset: 0x000648AB
		internal XmlName XmlName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself </param>
		/// <returns>The duplicate node.</returns>
		// Token: 0x06000FD8 RID: 4056 RVA: 0x000666B4 File Offset: 0x000648B4
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlAttribute xmlAttribute = ownerDocument.CreateAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			xmlAttribute.CopyChildren(ownerDocument, this, true);
			return xmlAttribute;
		}

		/// <summary>Gets the parent of this node. For <see langword="XmlAttribute" /> nodes, this property always returns <see langword="null" />.</summary>
		/// <returns>For <see langword="XmlAttribute" /> nodes, this property always returns <see langword="null" />.</returns>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000FD9 RID: 4057 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>The qualified name of the attribute node.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x000666E9 File Offset: 0x000648E9
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>The name of the attribute node with the prefix removed. In the following example &lt;book bk:genre= 'novel'&gt;, the <see langword="LocalName" /> of the attribute is <see langword="genre" />.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x000666F6 File Offset: 0x000648F6
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If the attribute is not explicitly given a namespace, this property returns String.Empty.</returns>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00066703 File Offset: 0x00064903
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only.</exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The namespaceURI of this node is <see langword="null" />.The specified prefix is "xml", and the namespaceURI of this node is different from "http://www.w3.org/XML/1998/namespace".This node is an attribute, the specified prefix is "xmlns", and the namespaceURI of this node is different from "http://www.w3.org/2000/xmlns/".This node is an attribute, and the qualifiedName of this node is "xmlns" [Namespaces].</exception>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00066710 File Offset: 0x00064910
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0006671D File Offset: 0x0006491D
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				this.name = this.name.OwnerDocument.AddAttrXmlName(value, this.LocalName, this.NamespaceURI, this.SchemaInfo);
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type for <see langword="XmlAttribute" /> nodes is XmlNodeType.Attribute.</returns>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00066748 File Offset: 0x00064948
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Attribute;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>An XML document to which this node belongs.</returns>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0006674B File Offset: 0x0006494B
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node. For <see langword="XmlAttribute" /> nodes, this property is the value of attribute.</returns>
		/// <exception cref="T:System.ArgumentException">The node is read-only and a set operation is called.</exception>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x00066758 File Offset: 0x00064958
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00066760 File Offset: 0x00064960
		public override string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		/// <summary>Gets the post-schema-validation-infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> containing the post-schema-validation-infoset of this node.</returns>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x000666A3 File Offset: 0x000648A3
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children. For attribute nodes, this property has the same functionality as the <see cref="P:System.Xml.XmlAttribute.Value" /> property.</returns>
		// Token: 0x1700027F RID: 639
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x0006676C File Offset: 0x0006496C
		public override string InnerText
		{
			set
			{
				if (this.PrepareOwnerElementInElementIdAttrMap())
				{
					string innerText = base.InnerText;
					base.InnerText = value;
					this.ResetOwnerElementInElementIdAttrMap(innerText);
					return;
				}
				base.InnerText = value;
			}
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000667A0 File Offset: 0x000649A0
		internal bool PrepareOwnerElementInElementIdAttrMap()
		{
			if (this.OwnerDocument.DtdSchemaInfo != null)
			{
				XmlElement ownerElement = this.OwnerElement;
				if (ownerElement != null)
				{
					return ownerElement.Attributes.PrepareParentInElementIdAttrMap(this.Prefix, this.LocalName);
				}
			}
			return false;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000667E0 File Offset: 0x000649E0
		internal void ResetOwnerElementInElementIdAttrMap(string oldInnerText)
		{
			XmlElement ownerElement = this.OwnerElement;
			if (ownerElement != null)
			{
				ownerElement.Attributes.ResetParentInElementIdAttrMap(oldInnerText, this.InnerText);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0006680C File Offset: 0x00064A0C
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null)
			{
				xmlLinkedNode.next = xmlLinkedNode;
				this.lastChild = xmlLinkedNode;
				xmlLinkedNode.SetParentForLoad(this);
			}
			else
			{
				XmlLinkedNode xmlLinkedNode2 = this.lastChild;
				xmlLinkedNode.next = xmlLinkedNode2.next;
				xmlLinkedNode2.next = xmlLinkedNode;
				this.lastChild = xmlLinkedNode;
				if (xmlLinkedNode2.IsText && xmlLinkedNode.IsText)
				{
					XmlNode.NestTextNodes(xmlLinkedNode2, xmlLinkedNode);
				}
				else
				{
					xmlLinkedNode.SetParentForLoad(this);
				}
			}
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return xmlLinkedNode;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0006689E File Offset: 0x00064A9E
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x000668A6 File Offset: 0x00064AA6
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

		// Token: 0x06000FEB RID: 4075 RVA: 0x000668AF File Offset: 0x00064AAF
		internal override bool IsValidChildType(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.EntityReference;
		}

		/// <summary>Gets a value indicating whether the attribute value was explicitly set.</summary>
		/// <returns>
		///     <see langword="true" /> if this attribute was explicitly given a value in the original instance document; otherwise, <see langword="false" />. A value of <see langword="false" /> indicates that the value of the attribute came from the DTD.</returns>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0001222F File Offset: 0x0001042F
		public virtual bool Specified
		{
			get
			{
				return true;
			}
		}

		/// <summary>Inserts the specified node immediately before the specified reference node.</summary>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to insert.</param>
		/// <param name="refChild">The <see cref="T:System.Xml.XmlNode" /> that is the reference node. The <paramref name="newChild" /> is placed before this node.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> inserted.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only.</exception>
		// Token: 0x06000FED RID: 4077 RVA: 0x000668BC File Offset: 0x00064ABC
		public override XmlNode InsertBefore(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.InsertBefore(newChild, refChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.InsertBefore(newChild, refChild);
			}
			return result;
		}

		/// <summary>Inserts the specified node immediately after the specified reference node.</summary>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to insert.</param>
		/// <param name="refChild">The <see cref="T:System.Xml.XmlNode" /> that is the reference node. The <paramref name="newChild" /> is placed after the <paramref name="refChild" />.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> inserted.</returns>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.The <paramref name="refChild" /> is not a child of this node.This node is read-only.</exception>
		// Token: 0x06000FEE RID: 4078 RVA: 0x000668F4 File Offset: 0x00064AF4
		public override XmlNode InsertAfter(XmlNode newChild, XmlNode refChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.InsertAfter(newChild, refChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.InsertAfter(newChild, refChild);
			}
			return result;
		}

		/// <summary>Replaces the child node specified with the new child node specified.</summary>
		/// <param name="newChild">The new child <see cref="T:System.Xml.XmlNode" />.</param>
		/// <param name="oldChild">The <see cref="T:System.Xml.XmlNode" /> to replace.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> replaced.</returns>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.The <paramref name="oldChild" /> is not a child of this node.</exception>
		// Token: 0x06000FEF RID: 4079 RVA: 0x0006692C File Offset: 0x00064B2C
		public override XmlNode ReplaceChild(XmlNode newChild, XmlNode oldChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.ReplaceChild(newChild, oldChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.ReplaceChild(newChild, oldChild);
			}
			return result;
		}

		/// <summary>Removes the specified child node.</summary>
		/// <param name="oldChild">The <see cref="T:System.Xml.XmlNode" /> to remove.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> removed.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="oldChild" /> is not a child of this node. Or this node is read-only.</exception>
		// Token: 0x06000FF0 RID: 4080 RVA: 0x00066964 File Offset: 0x00064B64
		public override XmlNode RemoveChild(XmlNode oldChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.RemoveChild(oldChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.RemoveChild(oldChild);
			}
			return result;
		}

		/// <summary>Adds the specified node to the beginning of the list of child nodes for this node.</summary>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to add. If it is an <see cref="T:System.Xml.XmlDocumentFragment" />, the entire contents of the document fragment are moved into the child list of this node.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> added.</returns>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.</exception>
		// Token: 0x06000FF1 RID: 4081 RVA: 0x0006699C File Offset: 0x00064B9C
		public override XmlNode PrependChild(XmlNode newChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.PrependChild(newChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.PrependChild(newChild);
			}
			return result;
		}

		/// <summary>Adds the specified node to the end of the list of child nodes, of this node.</summary>
		/// <param name="newChild">The <see cref="T:System.Xml.XmlNode" /> to add.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> added.</returns>
		/// <exception cref="T:System.InvalidOperationException">This node is of a type that does not allow child nodes of the type of the <paramref name="newChild" /> node.The <paramref name="newChild" /> is an ancestor of this node.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newChild" /> was created from a different document than the one that created this node.This node is read-only.</exception>
		// Token: 0x06000FF2 RID: 4082 RVA: 0x000669D4 File Offset: 0x00064BD4
		public override XmlNode AppendChild(XmlNode newChild)
		{
			XmlNode result;
			if (this.PrepareOwnerElementInElementIdAttrMap())
			{
				string innerText = this.InnerText;
				result = base.AppendChild(newChild);
				this.ResetOwnerElementInElementIdAttrMap(innerText);
			}
			else
			{
				result = base.AppendChild(newChild);
			}
			return result;
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlElement" /> to which the attribute belongs.</summary>
		/// <returns>The <see langword="XmlElement" /> that the attribute belongs to or <see langword="null" /> if this attribute is not part of an <see langword="XmlElement" />.</returns>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00066A0A File Offset: 0x00064C0A
		public virtual XmlElement OwnerElement
		{
			get
			{
				return this.parentNode as XmlElement;
			}
		}

		/// <summary>Sets the value of the attribute.</summary>
		/// <returns>The attribute value.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed.</exception>
		// Token: 0x17000284 RID: 644
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x00066A17 File Offset: 0x00064C17
		public override string InnerXml
		{
			set
			{
				this.RemoveAll();
				new XmlLoader().LoadInnerXmlAttribute(this, value);
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save.</param>
		// Token: 0x06000FF5 RID: 4085 RVA: 0x00066A2B File Offset: 0x00064C2B
		public override void WriteTo(XmlWriter w)
		{
			w.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
			this.WriteContentTo(w);
			w.WriteEndAttribute();
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save.</param>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x00066A54 File Offset: 0x00064C54
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		/// <summary>Gets the base Uniform Resource Identifier (URI) of the node.</summary>
		/// <returns>The location from which the node was loaded or String.Empty if the node has no base URI. Attribute nodes have the same base URI as their owner element. If an attribute node does not have an owner element, <see langword="BaseURI" /> returns String.Empty.</returns>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x00066A7B File Offset: 0x00064C7B
		public override string BaseURI
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.BaseURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x00066A96 File Offset: 0x00064C96
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x00066A9F File Offset: 0x00064C9F
		internal override XmlSpace XmlSpace
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.XmlSpace;
				}
				return XmlSpace.None;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00066AB6 File Offset: 0x00064CB6
		internal override string XmlLang
		{
			get
			{
				if (this.OwnerElement != null)
				{
					return this.OwnerElement.XmlLang;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x00066AD1 File Offset: 0x00064CD1
		internal override XPathNodeType XPNodeType
		{
			get
			{
				if (this.IsNamespace)
				{
					return XPathNodeType.Namespace;
				}
				return XPathNodeType.Attribute;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x00066ADE File Offset: 0x00064CDE
		internal override string XPLocalName
		{
			get
			{
				if (this.name.Prefix.Length == 0 && this.name.LocalName == "xmlns")
				{
					return string.Empty;
				}
				return this.name.LocalName;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00066B1A File Offset: 0x00064D1A
		internal bool IsNamespace
		{
			get
			{
				return Ref.Equal(this.name.NamespaceURI, this.name.OwnerDocument.strReservedXmlns);
			}
		}

		// Token: 0x0400103A RID: 4154
		private XmlName name;

		// Token: 0x0400103B RID: 4155
		private XmlLinkedNode lastChild;
	}
}
