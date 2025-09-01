using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Xml
{
	/// <summary>Represents a collection of attributes that can be accessed by name or index.</summary>
	// Token: 0x020001B5 RID: 437
	public sealed class XmlAttributeCollection : XmlNamedNodeMap, ICollection, IEnumerable
	{
		// Token: 0x06000FFE RID: 4094 RVA: 0x00066B3C File Offset: 0x00064D3C
		internal XmlAttributeCollection(XmlNode parent) : base(parent)
		{
		}

		/// <summary>Gets the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. </param>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> at the specified index.</returns>
		/// <exception cref="T:System.IndexOutOfRangeException">The index being passed in is out of range. </exception>
		// Token: 0x1700028B RID: 651
		[IndexerName("ItemOf")]
		public XmlAttribute this[int i]
		{
			get
			{
				XmlAttribute result;
				try
				{
					result = (XmlAttribute)this.nodes[i];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new IndexOutOfRangeException(Res.GetString("The index being passed in is out of range."));
				}
				return result;
			}
		}

		/// <summary>Gets the attribute with the specified name.</summary>
		/// <param name="name">The qualified name of the attribute. </param>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> with the specified name. If the attribute does not exist, this property returns <see langword="null" />.</returns>
		// Token: 0x1700028C RID: 652
		[IndexerName("ItemOf")]
		public XmlAttribute this[string name]
		{
			get
			{
				int hashCode = XmlName.GetHashCode(name);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && name == xmlAttribute.Name)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		/// <summary>Gets the attribute with the specified local name and namespace Uniform Resource Identifier (URI).</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <returns>The <see cref="T:System.Xml.XmlAttribute" /> with the specified local name and namespace URI. If the attribute does not exist, this property returns <see langword="null" />.</returns>
		// Token: 0x1700028D RID: 653
		[IndexerName("ItemOf")]
		public XmlAttribute this[string localName, string namespaceURI]
		{
			get
			{
				int hashCode = XmlName.GetHashCode(localName);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && localName == xmlAttribute.LocalName && namespaceURI == xmlAttribute.NamespaceURI)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x00066C48 File Offset: 0x00064E48
		internal int FindNodeOffset(XmlAttribute node)
		{
			for (int i = 0; i < this.nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.Name == node.Name && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x00066CB4 File Offset: 0x00064EB4
		internal int FindNodeOffsetNS(XmlAttribute node)
		{
			for (int i = 0; i < this.nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.LocalName == node.LocalName && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Adds a <see cref="T:System.Xml.XmlNode" /> using its <see cref="P:System.Xml.XmlNode.Name" /> property </summary>
		/// <param name="node">An attribute node to store in this collection. The node will later be accessible using the name of the node. If a node with that name is already present in the collection, it is replaced by the new one; otherwise, the node is appended to the end of the collection. </param>
		/// <returns>If the <paramref name="node" /> replaces an existing node with the same name, the old node is returned; otherwise, the added node is returned.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="node" /> was created from a different <see cref="T:System.Xml.XmlDocument" /> than the one that created this collection.This <see langword="XmlAttributeCollection" /> is read-only. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="node" /> is an <see cref="T:System.Xml.XmlAttribute" /> that is already an attribute of another <see cref="T:System.Xml.XmlElement" /> object. To re-use attributes in other elements, you must clone the <see langword="XmlAttribute" /> objects you want to re-use. </exception>
		// Token: 0x06001004 RID: 4100 RVA: 0x00066D20 File Offset: 0x00064F20
		public override XmlNode SetNamedItem(XmlNode node)
		{
			if (node != null && !(node is XmlAttribute))
			{
				throw new ArgumentException(Res.GetString("An 'Attributes' collection can only contain 'Attribute' objects."));
			}
			int num = base.FindNodeOffset(node.LocalName, node.NamespaceURI);
			if (num == -1)
			{
				return this.InternalAppendAttribute((XmlAttribute)node);
			}
			XmlNode result = base.RemoveNodeAt(num);
			this.InsertNodeAt(num, node);
			return result;
		}

		/// <summary>Inserts the specified attribute as the first node in the collection.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		/// <returns>The <see langword="XmlAttribute" /> added to the collection.</returns>
		// Token: 0x06001005 RID: 4101 RVA: 0x00066D7C File Offset: 0x00064F7C
		public XmlAttribute Prepend(XmlAttribute node)
		{
			if (node.OwnerDocument != null && node.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("The named node is from a different document context."));
			}
			if (node.OwnerElement != null)
			{
				this.Detach(node);
			}
			this.RemoveDuplicateAttribute(node);
			this.InsertNodeAt(0, node);
			return node;
		}

		/// <summary>Inserts the specified attribute as the last node in the collection.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		/// <returns>The <see langword="XmlAttribute" /> to append to the collection.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="node" /> was created from a document different from the one that created this collection. </exception>
		// Token: 0x06001006 RID: 4102 RVA: 0x00066DD8 File Offset: 0x00064FD8
		public XmlAttribute Append(XmlAttribute node)
		{
			XmlDocument ownerDocument = node.OwnerDocument;
			if (ownerDocument == null || !ownerDocument.IsLoading)
			{
				if (ownerDocument != null && ownerDocument != this.parent.OwnerDocument)
				{
					throw new ArgumentException(Res.GetString("The named node is from a different document context."));
				}
				if (node.OwnerElement != null)
				{
					this.Detach(node);
				}
				this.AddNode(node);
			}
			else
			{
				base.AddNodeForLoad(node, ownerDocument);
				this.InsertParentIntoElementIdAttrMap(node);
			}
			return node;
		}

		/// <summary>Inserts the specified attribute immediately before the specified reference attribute.</summary>
		/// <param name="newNode">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		/// <param name="refNode">The <see cref="T:System.Xml.XmlAttribute" /> that is the reference attribute. <paramref name="newNode" /> is placed before the <paramref name="refNode" />. </param>
		/// <returns>The <see langword="XmlAttribute" /> to insert into the collection.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newNode" /> was created from a document different from the one that created this collection. Or the <paramref name="refNode" /> is not a member of this collection. </exception>
		// Token: 0x06001007 RID: 4103 RVA: 0x00066E44 File Offset: 0x00065044
		public XmlAttribute InsertBefore(XmlAttribute newNode, XmlAttribute refNode)
		{
			if (newNode == refNode)
			{
				return newNode;
			}
			if (refNode == null)
			{
				return this.Append(newNode);
			}
			if (refNode.OwnerElement != this.parent)
			{
				throw new ArgumentException(Res.GetString("The reference node must be a child of the current node."));
			}
			if (newNode.OwnerDocument != null && newNode.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("The named node is from a different document context."));
			}
			if (newNode.OwnerElement != null)
			{
				this.Detach(newNode);
			}
			int num = base.FindNodeOffset(refNode.LocalName, refNode.NamespaceURI);
			int num2 = this.RemoveDuplicateAttribute(newNode);
			if (num2 >= 0 && num2 < num)
			{
				num--;
			}
			this.InsertNodeAt(num, newNode);
			return newNode;
		}

		/// <summary>Inserts the specified attribute immediately after the specified reference attribute.</summary>
		/// <param name="newNode">The <see cref="T:System.Xml.XmlAttribute" /> to insert. </param>
		/// <param name="refNode">The <see cref="T:System.Xml.XmlAttribute" /> that is the reference attribute. <paramref name="newNode" /> is placed after the <paramref name="refNode" />. </param>
		/// <returns>The <see langword="XmlAttribute" /> to insert into the collection.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="newNode" /> was created from a document different from the one that created this collection. Or the <paramref name="refNode" /> is not a member of this collection. </exception>
		// Token: 0x06001008 RID: 4104 RVA: 0x00066EEC File Offset: 0x000650EC
		public XmlAttribute InsertAfter(XmlAttribute newNode, XmlAttribute refNode)
		{
			if (newNode == refNode)
			{
				return newNode;
			}
			if (refNode == null)
			{
				return this.Prepend(newNode);
			}
			if (refNode.OwnerElement != this.parent)
			{
				throw new ArgumentException(Res.GetString("The reference node must be a child of the current node."));
			}
			if (newNode.OwnerDocument != null && newNode.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("The named node is from a different document context."));
			}
			if (newNode.OwnerElement != null)
			{
				this.Detach(newNode);
			}
			int num = base.FindNodeOffset(refNode.LocalName, refNode.NamespaceURI);
			int num2 = this.RemoveDuplicateAttribute(newNode);
			if (num2 >= 0 && num2 < num)
			{
				num--;
			}
			this.InsertNodeAt(num + 1, newNode);
			return newNode;
		}

		/// <summary>Removes the specified attribute from the collection.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlAttribute" /> to remove. </param>
		/// <returns>The node removed or <see langword="null" /> if it is not found in the collection.</returns>
		// Token: 0x06001009 RID: 4105 RVA: 0x00066F98 File Offset: 0x00065198
		public XmlAttribute Remove(XmlAttribute node)
		{
			int count = this.nodes.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.nodes[i] == node)
				{
					this.RemoveNodeAt(i);
					return node;
				}
			}
			return null;
		}

		/// <summary>Removes the attribute corresponding to the specified index from the collection.</summary>
		/// <param name="i">The index of the node to remove. The first node has index 0. </param>
		/// <returns>Returns <see langword="null" /> if there is no attribute at the specified index.</returns>
		// Token: 0x0600100A RID: 4106 RVA: 0x00066FD7 File Offset: 0x000651D7
		public XmlAttribute RemoveAt(int i)
		{
			if (i < 0 || i >= this.Count)
			{
				return null;
			}
			return (XmlAttribute)this.RemoveNodeAt(i);
		}

		/// <summary>Removes all attributes from the collection.</summary>
		// Token: 0x0600100B RID: 4107 RVA: 0x00066FF4 File Offset: 0x000651F4
		public void RemoveAll()
		{
			int i = this.Count;
			while (i > 0)
			{
				i--;
				this.RemoveAt(i);
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.XmlAttributeCollection.CopyTo(System.Xml.XmlAttribute[],System.Int32)" />.</summary>
		/// <param name="array">The array that is the destination of the objects copied from this collection. </param>
		/// <param name="index">The index in the array where copying begins. </param>
		// Token: 0x0600100C RID: 4108 RVA: 0x0006701C File Offset: 0x0006521C
		void ICollection.CopyTo(Array array, int index)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				array.SetValue(this.nodes[i], index);
				i++;
				index++;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System#Collections#ICollection#IsSynchronized" />.</summary>
		/// <returns>Returns <see langword="true" /> if the collection is synchronized.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System#Collections#ICollection#SyncRoot" />.</summary>
		/// <returns>Returns the <see cref="T:System.Object" /> that is the root of the collection.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x00002068 File Offset: 0x00000268
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Xml.XmlAttributeCollection.System#Collections#ICollection#Count" />.</summary>
		/// <returns>Returns an <see langword="int" /> that contains the count of the attributes.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600100F RID: 4111 RVA: 0x00067054 File Offset: 0x00065254
		int ICollection.Count
		{
			get
			{
				return base.Count;
			}
		}

		/// <summary>Copies all the <see cref="T:System.Xml.XmlAttribute" /> objects from this collection into the given array.</summary>
		/// <param name="array">The array that is the destination of the objects copied from this collection. </param>
		/// <param name="index">The index in the array where copying begins. </param>
		// Token: 0x06001010 RID: 4112 RVA: 0x0006705C File Offset: 0x0006525C
		public void CopyTo(XmlAttribute[] array, int index)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				array[index] = (XmlAttribute)((XmlNode)this.nodes[i]).CloneNode(true);
				i++;
				index++;
			}
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x000670A0 File Offset: 0x000652A0
		internal override XmlNode AddNode(XmlNode node)
		{
			this.RemoveDuplicateAttribute((XmlAttribute)node);
			XmlNode result = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return result;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x000670C2 File Offset: 0x000652C2
		internal override XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode result = base.InsertNodeAt(i, node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return result;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x000670D8 File Offset: 0x000652D8
		internal override XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = base.RemoveNodeAt(i);
			this.RemoveParentFromElementIdAttrMap((XmlAttribute)xmlNode);
			XmlAttribute defaultAttribute = this.parent.OwnerDocument.GetDefaultAttribute((XmlElement)this.parent, xmlNode.Prefix, xmlNode.LocalName, xmlNode.NamespaceURI);
			if (defaultAttribute != null)
			{
				this.InsertNodeAt(i, defaultAttribute);
			}
			return xmlNode;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00067134 File Offset: 0x00065334
		internal void Detach(XmlAttribute attr)
		{
			attr.OwnerElement.Attributes.Remove(attr);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00067148 File Offset: 0x00065348
		internal void InsertParentIntoElementIdAttrMap(XmlAttribute attr)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			if (xmlElement != null)
			{
				if (this.parent.OwnerDocument == null)
				{
					return;
				}
				XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
				if (idinfoByElement != null && idinfoByElement.Prefix == attr.XmlName.Prefix && idinfoByElement.LocalName == attr.XmlName.LocalName)
				{
					this.parent.OwnerDocument.AddElementWithId(attr.Value, xmlElement);
				}
			}
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000671D4 File Offset: 0x000653D4
		internal void RemoveParentFromElementIdAttrMap(XmlAttribute attr)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			if (xmlElement != null)
			{
				if (this.parent.OwnerDocument == null)
				{
					return;
				}
				XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
				if (idinfoByElement != null && idinfoByElement.Prefix == attr.XmlName.Prefix && idinfoByElement.LocalName == attr.XmlName.LocalName)
				{
					this.parent.OwnerDocument.RemoveElementWithId(attr.Value, xmlElement);
				}
			}
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00067260 File Offset: 0x00065460
		internal int RemoveDuplicateAttribute(XmlAttribute attr)
		{
			int num = base.FindNodeOffset(attr.LocalName, attr.NamespaceURI);
			if (num != -1)
			{
				XmlAttribute attr2 = (XmlAttribute)this.nodes[num];
				base.RemoveNodeAt(num);
				this.RemoveParentFromElementIdAttrMap(attr2);
			}
			return num;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000672A8 File Offset: 0x000654A8
		internal bool PrepareParentInElementIdAttrMap(string attrPrefix, string attrLocalName)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
			return idinfoByElement != null && idinfoByElement.Prefix == attrPrefix && idinfoByElement.LocalName == attrLocalName;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x000672FC File Offset: 0x000654FC
		internal void ResetParentInElementIdAttrMap(string oldVal, string newVal)
		{
			XmlElement elem = this.parent as XmlElement;
			XmlDocument ownerDocument = this.parent.OwnerDocument;
			ownerDocument.RemoveElementWithId(oldVal, elem);
			ownerDocument.AddElementWithId(newVal, elem);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0006732F File Offset: 0x0006552F
		internal XmlAttribute InternalAppendAttribute(XmlAttribute node)
		{
			XmlNode xmlNode = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap(node);
			return (XmlAttribute)xmlNode;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlAttributeCollection()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
