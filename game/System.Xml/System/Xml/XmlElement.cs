using System;
using System.Xml.Schema;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents an element.</summary>
	// Token: 0x020001C0 RID: 448
	public class XmlElement : XmlLinkedNode
	{
		// Token: 0x06001107 RID: 4359 RVA: 0x00069B14 File Offset: 0x00067D14
		internal XmlElement(XmlName name, bool empty, XmlDocument doc) : base(doc)
		{
			this.parentNode = null;
			if (!doc.IsLoading)
			{
				XmlDocument.CheckName(name.Prefix);
				XmlDocument.CheckName(name.LocalName);
			}
			if (name.LocalName.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The local name for elements or attributes cannot be null or an empty string."));
			}
			this.name = name;
			if (empty)
			{
				this.lastChild = this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlElement" /> class.</summary>
		/// <param name="prefix">The namespace prefix; see the <see cref="P:System.Xml.XmlElement.Prefix" /> property.</param>
		/// <param name="localName">The local name; see the <see cref="P:System.Xml.XmlElement.LocalName" /> property.</param>
		/// <param name="namespaceURI">The namespace URI; see the <see cref="P:System.Xml.XmlElement.NamespaceURI" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06001108 RID: 4360 RVA: 0x00069B7B File Offset: 0x00067D7B
		protected internal XmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc) : this(doc.AddXmlName(prefix, localName, namespaceURI, null), true, doc)
		{
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001109 RID: 4361 RVA: 0x00069B91 File Offset: 0x00067D91
		// (set) Token: 0x0600110A RID: 4362 RVA: 0x00069B99 File Offset: 0x00067D99
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
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself (and its attributes if the node is an <see langword="XmlElement" />). </param>
		/// <returns>The cloned node.</returns>
		// Token: 0x0600110B RID: 4363 RVA: 0x00069BA4 File Offset: 0x00067DA4
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			bool isLoading = ownerDocument.IsLoading;
			ownerDocument.IsLoading = true;
			XmlElement xmlElement = ownerDocument.CreateElement(this.Prefix, this.LocalName, this.NamespaceURI);
			ownerDocument.IsLoading = isLoading;
			if (xmlElement.IsEmpty != this.IsEmpty)
			{
				xmlElement.IsEmpty = this.IsEmpty;
			}
			if (this.HasAttributes)
			{
				foreach (object obj in this.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					XmlAttribute xmlAttribute2 = (XmlAttribute)xmlAttribute.CloneNode(true);
					if (xmlAttribute is XmlUnspecifiedAttribute && !xmlAttribute.Specified)
					{
						((XmlUnspecifiedAttribute)xmlAttribute2).SetSpecified(false);
					}
					xmlElement.Attributes.InternalAppendAttribute(xmlAttribute2);
				}
			}
			if (deep)
			{
				xmlElement.CopyChildren(ownerDocument, this, deep);
			}
			return xmlElement;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>The qualified name of the node. For <see langword="XmlElement" /> nodes, this is the tag name of the element.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600110C RID: 4364 RVA: 0x00069C9C File Offset: 0x00067E9C
		public override string Name
		{
			get
			{
				return this.name.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, <see langword="LocalName" /> is book for the element &lt;bk:book&gt;.</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x00069CA9 File Offset: 0x00067EA9
		public override string LocalName
		{
			get
			{
				return this.name.LocalName;
			}
		}

		/// <summary>Gets the namespace URI of this node.</summary>
		/// <returns>The namespace URI of this node. If there is no namespace URI, this property returns String.Empty.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00069CB6 File Offset: 0x00067EB6
		public override string NamespaceURI
		{
			get
			{
				return this.name.NamespaceURI;
			}
		}

		/// <summary>Gets or sets the namespace prefix of this node.</summary>
		/// <returns>The namespace prefix of this node. If there is no prefix, this property returns String.Empty.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only </exception>
		/// <exception cref="T:System.Xml.XmlException">The specified prefix contains an invalid character.The specified prefix is malformed.The namespaceURI of this node is <see langword="null" />.The specified prefix is "xml" and the namespaceURI of this node is different from http://www.w3.org/XML/1998/namespace. </exception>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00069CC3 File Offset: 0x00067EC3
		// (set) Token: 0x06001110 RID: 4368 RVA: 0x00069CD0 File Offset: 0x00067ED0
		public override string Prefix
		{
			get
			{
				return this.name.Prefix;
			}
			set
			{
				this.name = this.name.OwnerDocument.AddXmlName(value, this.LocalName, this.NamespaceURI, this.SchemaInfo);
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For <see langword="XmlElement" /> nodes, this value is XmlNodeType.Element.</returns>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0001222F File Offset: 0x0001042F
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Element;
			}
		}

		/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
		/// <returns>The <see langword="XmlNode" /> that is the parent of the current node. If a node has just been created and not yet added to the tree, or if it has been removed from the tree, the parent is <see langword="null" />. For all other nodes, the value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node. The following table describes the possible return values for the <see langword="ParentNode" /> property.</returns>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00069CFB File Offset: 0x00067EFB
		public override XmlNode ParentNode
		{
			get
			{
				return this.parentNode;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The <see langword="XmlDocument" /> to which this element belongs.</returns>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00069D03 File Offset: 0x00067F03
		public override XmlDocument OwnerDocument
		{
			get
			{
				return this.name.OwnerDocument;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00069D10 File Offset: 0x00067F10
		internal override XmlNode AppendChildForLoad(XmlNode newChild, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(newChild, this);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			XmlLinkedNode xmlLinkedNode = (XmlLinkedNode)newChild;
			if (this.lastChild == null || this.lastChild == this)
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

		/// <summary>Gets or sets the tag format of the element.</summary>
		/// <returns>Returns <see langword="true" /> if the element is to be serialized in the short tag format "&lt;item/&gt;"; <see langword="false" /> for the long format "&lt;item&gt;&lt;/item&gt;".When setting this property, if set to <see langword="true" />, the children of the element are removed and the element is serialized in the short tag format. If set to <see langword="false" />, the value of the property is changed (regardless of whether or not the element has content); if the element is empty, it is serialized in the long format.This property is a Microsoft extension to the Document Object Model (DOM).</returns>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00069DAB File Offset: 0x00067FAB
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00069DB6 File Offset: 0x00067FB6
		public bool IsEmpty
		{
			get
			{
				return this.lastChild == this;
			}
			set
			{
				if (value)
				{
					if (this.lastChild != this)
					{
						this.RemoveAllChildren();
						this.lastChild = this;
						return;
					}
				}
				else if (this.lastChild == this)
				{
					this.lastChild = null;
				}
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00069DE2 File Offset: 0x00067FE2
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00069DF5 File Offset: 0x00067FF5
		internal override XmlLinkedNode LastNode
		{
			get
			{
				if (this.lastChild != this)
				{
					return this.lastChild;
				}
				return null;
			}
			set
			{
				this.lastChild = value;
			}
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00069E00 File Offset: 0x00068000
		internal override bool IsValidChildType(XmlNodeType type)
		{
			switch (type)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.EntityReference:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return true;
			}
			return false;
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlAttributeCollection" /> containing the list of attributes for this node.</summary>
		/// <returns>
		///     <see cref="T:System.Xml.XmlAttributeCollection" /> containing the list of attributes for this node.</returns>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00069E54 File Offset: 0x00068054
		public override XmlAttributeCollection Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					object objLock = this.OwnerDocument.objLock;
					lock (objLock)
					{
						if (this.attributes == null)
						{
							this.attributes = new XmlAttributeCollection(this);
						}
					}
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a <see langword="boolean" /> value indicating whether the current node has any attributes.</summary>
		/// <returns>
		///     <see langword="true" /> if the current node has attributes; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x00069EB8 File Offset: 0x000680B8
		public virtual bool HasAttributes
		{
			get
			{
				return this.attributes != null && this.attributes.Count > 0;
			}
		}

		/// <summary>Returns the value for the attribute with the specified name.</summary>
		/// <param name="name">The name of the attribute to retrieve. This is a qualified name. It is matched against the <see langword="Name" /> property of the matching node. </param>
		/// <returns>The value of the specified attribute. An empty string is returned if a matching attribute is not found or if the attribute does not have a specified or default value.</returns>
		// Token: 0x0600111D RID: 4381 RVA: 0x00069ED4 File Offset: 0x000680D4
		public virtual string GetAttribute(string name)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(name);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		/// <summary>Sets the value of the attribute with the specified name.</summary>
		/// <param name="name">The name of the attribute to create or alter. This is a qualified name. If the name contains a colon it is parsed into prefix and local name components. </param>
		/// <param name="value">The value to set for the attribute. </param>
		/// <exception cref="T:System.Xml.XmlException">The specified name contains an invalid character. </exception>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x0600111E RID: 4382 RVA: 0x00069EF8 File Offset: 0x000680F8
		public virtual void SetAttribute(string name, string value)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(name);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute.Value = value;
				this.Attributes.InternalAppendAttribute(xmlAttribute);
				return;
			}
			xmlAttribute.Value = value;
		}

		/// <summary>Removes an attribute by name.</summary>
		/// <param name="name">The name of the attribute to remove.This is a qualified name. It is matched against the <see langword="Name" /> property of the matching node. </param>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x0600111F RID: 4383 RVA: 0x00069F39 File Offset: 0x00068139
		public virtual void RemoveAttribute(string name)
		{
			if (this.HasAttributes)
			{
				this.Attributes.RemoveNamedItem(name);
			}
		}

		/// <summary>Returns the <see langword="XmlAttribute" /> with the specified name.</summary>
		/// <param name="name">The name of the attribute to retrieve. This is a qualified name. It is matched against the <see langword="Name" /> property of the matching node. </param>
		/// <returns>The specified <see langword="XmlAttribute" /> or <see langword="null" /> if a matching attribute was not found.</returns>
		// Token: 0x06001120 RID: 4384 RVA: 0x00069F50 File Offset: 0x00068150
		public virtual XmlAttribute GetAttributeNode(string name)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[name];
			}
			return null;
		}

		/// <summary>Adds the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <param name="newAttr">The <see langword="XmlAttribute" /> node to add to the attribute collection for this element. </param>
		/// <returns>If the attribute replaces an existing attribute with the same name, the old <see langword="XmlAttribute" /> is returned; otherwise, <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newAttr" /> was created from a different document than the one that created this node. Or this node is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="newAttr" /> is already an attribute of another <see langword="XmlElement" /> object. You must explicitly clone <see langword="XmlAttribute" /> nodes to re-use them in other <see langword="XmlElement" /> objects. </exception>
		// Token: 0x06001121 RID: 4385 RVA: 0x00069F68 File Offset: 0x00068168
		public virtual XmlAttribute SetAttributeNode(XmlAttribute newAttr)
		{
			if (newAttr.OwnerElement != null)
			{
				throw new InvalidOperationException(Res.GetString("The 'Attribute' node cannot be inserted because it is already an attribute of another element."));
			}
			return (XmlAttribute)this.Attributes.SetNamedItem(newAttr);
		}

		/// <summary>Removes the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <param name="oldAttr">The <see langword="XmlAttribute" /> node to remove. If the removed attribute has a default value, it is immediately replaced. </param>
		/// <returns>The removed <see langword="XmlAttribute" /> or <see langword="null" /> if <paramref name="oldAttr" /> is not an attribute node of the <see langword="XmlElement" />.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only. </exception>
		// Token: 0x06001122 RID: 4386 RVA: 0x00069F93 File Offset: 0x00068193
		public virtual XmlAttribute RemoveAttributeNode(XmlAttribute oldAttr)
		{
			if (this.HasAttributes)
			{
				return this.Attributes.Remove(oldAttr);
			}
			return null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlElement.Name" />.</summary>
		/// <param name="name">The name tag to match. This is a qualified name. It is matched against the <see langword="Name" /> property of the matching node. The asterisk (*) is a special value that matches all tags. </param>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes. The list is empty if there are no matching nodes.</returns>
		// Token: 0x06001123 RID: 4387 RVA: 0x00068769 File Offset: 0x00066969
		public virtual XmlNodeList GetElementsByTagName(string name)
		{
			return new XmlElementList(this, name);
		}

		/// <summary>Returns the value for the attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute to retrieve. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to retrieve. </param>
		/// <returns>The value of the specified attribute. An empty string is returned if a matching attribute is not found or if the attribute does not have a specified or default value.</returns>
		// Token: 0x06001124 RID: 4388 RVA: 0x00069FAC File Offset: 0x000681AC
		public virtual string GetAttribute(string localName, string namespaceURI)
		{
			XmlAttribute attributeNode = this.GetAttributeNode(localName, namespaceURI);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		/// <summary>Sets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <param name="value">The value to set for the attribute. </param>
		/// <returns>The attribute value.</returns>
		// Token: 0x06001125 RID: 4389 RVA: 0x00069FD4 File Offset: 0x000681D4
		public virtual string SetAttribute(string localName, string namespaceURI, string value)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(localName, namespaceURI);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI);
				xmlAttribute.Value = value;
				this.Attributes.InternalAppendAttribute(xmlAttribute);
			}
			else
			{
				xmlAttribute.Value = value;
			}
			return value;
		}

		/// <summary>Removes an attribute with the specified local name and namespace URI. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <param name="localName">The local name of the attribute to remove. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to remove. </param>
		/// <exception cref="T:System.ArgumentException">The node is read-only. </exception>
		// Token: 0x06001126 RID: 4390 RVA: 0x0006A01E File Offset: 0x0006821E
		public virtual void RemoveAttribute(string localName, string namespaceURI)
		{
			this.RemoveAttributeNode(localName, namespaceURI);
		}

		/// <summary>Returns the <see cref="T:System.Xml.XmlAttribute" /> with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <returns>The specified <see langword="XmlAttribute" /> or <see langword="null" /> if a matching attribute was not found.</returns>
		// Token: 0x06001127 RID: 4391 RVA: 0x0006A029 File Offset: 0x00068229
		public virtual XmlAttribute GetAttributeNode(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				return this.Attributes[localName, namespaceURI];
			}
			return null;
		}

		/// <summary>Adds the specified <see cref="T:System.Xml.XmlAttribute" />.</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <returns>The <see langword="XmlAttribute" /> to add.</returns>
		// Token: 0x06001128 RID: 4392 RVA: 0x0006A044 File Offset: 0x00068244
		public virtual XmlAttribute SetAttributeNode(string localName, string namespaceURI)
		{
			XmlAttribute xmlAttribute = this.GetAttributeNode(localName, namespaceURI);
			if (xmlAttribute == null)
			{
				xmlAttribute = this.OwnerDocument.CreateAttribute(string.Empty, localName, namespaceURI);
				this.Attributes.InternalAppendAttribute(xmlAttribute);
			}
			return xmlAttribute;
		}

		/// <summary>Removes the <see cref="T:System.Xml.XmlAttribute" /> specified by the local name and namespace URI. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <returns>The removed <see langword="XmlAttribute" /> or <see langword="null" /> if the <see langword="XmlElement" /> does not have a matching attribute node.</returns>
		/// <exception cref="T:System.ArgumentException">This node is read-only. </exception>
		// Token: 0x06001129 RID: 4393 RVA: 0x0006A080 File Offset: 0x00068280
		public virtual XmlAttribute RemoveAttributeNode(string localName, string namespaceURI)
		{
			if (this.HasAttributes)
			{
				XmlAttribute attributeNode = this.GetAttributeNode(localName, namespaceURI);
				this.Attributes.Remove(attributeNode);
				return attributeNode;
			}
			return null;
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlElement.LocalName" /> and <see cref="P:System.Xml.XmlElement.NamespaceURI" />.</summary>
		/// <param name="localName">The local name to match. The asterisk (*) is a special value that matches all tags. </param>
		/// <param name="namespaceURI">The namespace URI to match. </param>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes. The list is empty if there are no matching nodes.</returns>
		// Token: 0x0600112A RID: 4394 RVA: 0x000687CC File Offset: 0x000669CC
		public virtual XmlNodeList GetElementsByTagName(string localName, string namespaceURI)
		{
			return new XmlElementList(this, localName, namespaceURI);
		}

		/// <summary>Determines whether the current node has an attribute with the specified name.</summary>
		/// <param name="name">The name of the attribute to find. This is a qualified name. It is matched against the <see langword="Name" /> property of the matching node. </param>
		/// <returns>
		///     <see langword="true" /> if the current node has the specified attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600112B RID: 4395 RVA: 0x0006A0AE File Offset: 0x000682AE
		public virtual bool HasAttribute(string name)
		{
			return this.GetAttributeNode(name) != null;
		}

		/// <summary>Determines whether the current node has an attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute to find. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute to find. </param>
		/// <returns>
		///     <see langword="true" /> if the current node has the specified attribute; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600112C RID: 4396 RVA: 0x0006A0BA File Offset: 0x000682BA
		public virtual bool HasAttribute(string localName, string namespaceURI)
		{
			return this.GetAttributeNode(localName, namespaceURI) != null;
		}

		/// <summary>Saves the current node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x0600112D RID: 4397 RVA: 0x0006A0C8 File Offset: 0x000682C8
		public override void WriteTo(XmlWriter w)
		{
			if (base.GetType() == typeof(XmlElement))
			{
				XmlElement.WriteElementTo(w, this);
				return;
			}
			this.WriteStartElement(w);
			if (this.IsEmpty)
			{
				w.WriteEndElement();
				return;
			}
			this.WriteContentTo(w);
			w.WriteFullEndElement();
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0006A118 File Offset: 0x00068318
		private static void WriteElementTo(XmlWriter writer, XmlElement e)
		{
			XmlNode xmlNode = e;
			XmlNode xmlNode2 = e;
			for (;;)
			{
				e = (xmlNode2 as XmlElement);
				if (e != null && e.GetType() == typeof(XmlElement))
				{
					e.WriteStartElement(writer);
					if (e.IsEmpty)
					{
						writer.WriteEndElement();
					}
					else
					{
						if (e.lastChild != null)
						{
							xmlNode2 = e.FirstChild;
							continue;
						}
						writer.WriteFullEndElement();
					}
				}
				else
				{
					xmlNode2.WriteTo(writer);
				}
				while (xmlNode2 != xmlNode && xmlNode2 == xmlNode2.ParentNode.LastChild)
				{
					xmlNode2 = xmlNode2.ParentNode;
					writer.WriteFullEndElement();
				}
				if (xmlNode2 == xmlNode)
				{
					break;
				}
				xmlNode2 = xmlNode2.NextSibling;
			}
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0006A1B4 File Offset: 0x000683B4
		private void WriteStartElement(XmlWriter w)
		{
			w.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceURI);
			if (this.HasAttributes)
			{
				XmlAttributeCollection xmlAttributeCollection = this.Attributes;
				for (int i = 0; i < xmlAttributeCollection.Count; i++)
				{
					xmlAttributeCollection[i].WriteTo(w);
				}
			}
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x06001130 RID: 4400 RVA: 0x0006A208 File Offset: 0x00068408
		public override void WriteContentTo(XmlWriter w)
		{
			for (XmlNode xmlNode = this.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				xmlNode.WriteTo(w);
			}
		}

		/// <summary>Removes the attribute node with the specified index from the element. (If the removed attribute has a default value, it is immediately replaced).</summary>
		/// <param name="i">The index of the node to remove. The first node has index 0. </param>
		/// <returns>The attribute node removed or <see langword="null" /> if there is no node at the given index.</returns>
		// Token: 0x06001131 RID: 4401 RVA: 0x0006A22F File Offset: 0x0006842F
		public virtual XmlNode RemoveAttributeAt(int i)
		{
			if (this.HasAttributes)
			{
				return this.attributes.RemoveAt(i);
			}
			return null;
		}

		/// <summary>Removes all specified attributes from the element. Default attributes are not removed.</summary>
		// Token: 0x06001132 RID: 4402 RVA: 0x0006A247 File Offset: 0x00068447
		public virtual void RemoveAllAttributes()
		{
			if (this.HasAttributes)
			{
				this.attributes.RemoveAll();
			}
		}

		/// <summary>Removes all specified attributes and children of the current node. Default attributes are not removed.</summary>
		// Token: 0x06001133 RID: 4403 RVA: 0x0006A25C File Offset: 0x0006845C
		public override void RemoveAll()
		{
			base.RemoveAll();
			this.RemoveAllAttributes();
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x0006A26A File Offset: 0x0006846A
		internal void RemoveAllChildren()
		{
			base.RemoveAll();
		}

		/// <summary>Gets the post schema validation infoset that has been assigned to this node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the post schema validation infoset of this node.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x00069B91 File Offset: 0x00067D91
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets or sets the markup representing just the children of this node.</summary>
		/// <returns>The markup of the children of this node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001136 RID: 4406 RVA: 0x0006900B File Offset: 0x0006720B
		// (set) Token: 0x06001137 RID: 4407 RVA: 0x0006A272 File Offset: 0x00068472
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAllChildren();
				new XmlLoader().LoadInnerXmlElement(this, value);
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all its children.</summary>
		/// <returns>The concatenated values of the node and all its children.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001138 RID: 4408 RVA: 0x0006A286 File Offset: 0x00068486
		// (set) Token: 0x06001139 RID: 4409 RVA: 0x0006A290 File Offset: 0x00068490
		public override string InnerText
		{
			get
			{
				return base.InnerText;
			}
			set
			{
				XmlLinkedNode lastNode = this.LastNode;
				if (lastNode != null && lastNode.NodeType == XmlNodeType.Text && lastNode.next == lastNode)
				{
					lastNode.Value = value;
					return;
				}
				this.RemoveAllChildren();
				this.AppendChild(this.OwnerDocument.CreateTextNode(value));
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNode" /> immediately following this element.</summary>
		/// <returns>The <see langword="XmlNode" /> immediately following this element.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600113A RID: 4410 RVA: 0x0006A2DA File Offset: 0x000684DA
		public override XmlNode NextSibling
		{
			get
			{
				if (this.parentNode != null && this.parentNode.LastNode != this)
				{
					return this.next;
				}
				return null;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00066A96 File Offset: 0x00064C96
		internal override void SetParent(XmlNode node)
		{
			this.parentNode = node;
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x0001222F File Offset: 0x0001042F
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Element;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x0006A2FA File Offset: 0x000684FA
		internal override string XPLocalName
		{
			get
			{
				return this.LocalName;
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0006A304 File Offset: 0x00068504
		internal override string GetXPAttribute(string localName, string ns)
		{
			if (ns == this.OwnerDocument.strReservedXmlns)
			{
				return null;
			}
			XmlAttribute attributeNode = this.GetAttributeNode(localName, ns);
			if (attributeNode != null)
			{
				return attributeNode.Value;
			}
			return string.Empty;
		}

		// Token: 0x0400107C RID: 4220
		private XmlName name;

		// Token: 0x0400107D RID: 4221
		private XmlAttributeCollection attributes;

		// Token: 0x0400107E RID: 4222
		private XmlLinkedNode lastChild;
	}
}
