using System;
using System.Collections;
using Unity;

namespace System.Xml
{
	/// <summary>Represents a collection of nodes that can be accessed by name or index.</summary>
	// Token: 0x020001CD RID: 461
	public class XmlNamedNodeMap : IEnumerable
	{
		// Token: 0x060011C6 RID: 4550 RVA: 0x0006C93F File Offset: 0x0006AB3F
		internal XmlNamedNodeMap(XmlNode parent)
		{
			this.parent = parent;
		}

		/// <summary>Retrieves an <see cref="T:System.Xml.XmlNode" /> specified by name.</summary>
		/// <param name="name">The qualified name of the node to retrieve. It is matched against the <see cref="P:System.Xml.XmlNode.Name" /> property of the matching node.</param>
		/// <returns>An <see langword="XmlNode" /> with the specified name or <see langword="null" /> if a matching node is not found.</returns>
		// Token: 0x060011C7 RID: 4551 RVA: 0x0006C950 File Offset: 0x0006AB50
		public virtual XmlNode GetNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return (XmlNode)this.nodes[num];
			}
			return null;
		}

		/// <summary>Adds an <see cref="T:System.Xml.XmlNode" /> using its <see cref="P:System.Xml.XmlNode.Name" /> property.</summary>
		/// <param name="node">An <see langword="XmlNode" /> to store in the <see langword="XmlNamedNodeMap" />. If a node with that name is already present in the map, it is replaced by the new one.</param>
		/// <returns>If the <paramref name="node" /> replaces an existing node with the same name, the old node is returned; otherwise, <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="node" /> was created from a different <see cref="T:System.Xml.XmlDocument" /> than the one that created the <see langword="XmlNamedNodeMap" />; or the <see langword="XmlNamedNodeMap" /> is read-only.</exception>
		// Token: 0x060011C8 RID: 4552 RVA: 0x0006C97C File Offset: 0x0006AB7C
		public virtual XmlNode SetNamedItem(XmlNode node)
		{
			if (node == null)
			{
				return null;
			}
			int num = this.FindNodeOffset(node.LocalName, node.NamespaceURI);
			if (num == -1)
			{
				this.AddNode(node);
				return null;
			}
			return this.ReplaceNodeAt(num, node);
		}

		/// <summary>Removes the node from the <see langword="XmlNamedNodeMap" />.</summary>
		/// <param name="name">The qualified name of the node to remove. The name is matched against the <see cref="P:System.Xml.XmlNode.Name" /> property of the matching node.</param>
		/// <returns>The <see langword="XmlNode" /> removed from this <see langword="XmlNamedNodeMap" /> or <see langword="null" /> if a matching node was not found.</returns>
		// Token: 0x060011C9 RID: 4553 RVA: 0x0006C9B8 File Offset: 0x0006ABB8
		public virtual XmlNode RemoveNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return this.RemoveNodeAt(num);
			}
			return null;
		}

		/// <summary>Gets the number of nodes in the <see langword="XmlNamedNodeMap" />.</summary>
		/// <returns>The number of nodes.</returns>
		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0006C9DA File Offset: 0x0006ABDA
		public virtual int Count
		{
			get
			{
				return this.nodes.Count;
			}
		}

		/// <summary>Retrieves the node at the specified index in the <see langword="XmlNamedNodeMap" />.</summary>
		/// <param name="index">The index position of the node to retrieve from the <see langword="XmlNamedNodeMap" />. The index is zero-based; therefore, the index of the first node is 0 and the index of the last node is <see cref="P:System.Xml.XmlNamedNodeMap.Count" /> -1.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> at the specified index. If <paramref name="index" /> is less than 0 or greater than or equal to the <see cref="P:System.Xml.XmlNamedNodeMap.Count" /> property, <see langword="null" /> is returned.</returns>
		// Token: 0x060011CB RID: 4555 RVA: 0x0006C9E8 File Offset: 0x0006ABE8
		public virtual XmlNode Item(int index)
		{
			if (index < 0 || index >= this.nodes.Count)
			{
				return null;
			}
			XmlNode result;
			try
			{
				result = (XmlNode)this.nodes[index];
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new IndexOutOfRangeException(Res.GetString("The index being passed in is out of range."));
			}
			return result;
		}

		/// <summary>Retrieves a node with the matching <see cref="P:System.Xml.XmlNode.LocalName" /> and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="localName">The local name of the node to retrieve.</param>
		/// <param name="namespaceURI">The namespace Uniform Resource Identifier (URI) of the node to retrieve.</param>
		/// <returns>An <see cref="T:System.Xml.XmlNode" /> with the matching local name and namespace URI or <see langword="null" /> if a matching node was not found.</returns>
		// Token: 0x060011CC RID: 4556 RVA: 0x0006CA40 File Offset: 0x0006AC40
		public virtual XmlNode GetNamedItem(string localName, string namespaceURI)
		{
			int num = this.FindNodeOffset(localName, namespaceURI);
			if (num >= 0)
			{
				return (XmlNode)this.nodes[num];
			}
			return null;
		}

		/// <summary>Removes a node with the matching <see cref="P:System.Xml.XmlNode.LocalName" /> and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="localName">The local name of the node to remove.</param>
		/// <param name="namespaceURI">The namespace URI of the node to remove.</param>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> removed or <see langword="null" /> if a matching node was not found.</returns>
		// Token: 0x060011CD RID: 4557 RVA: 0x0006CA70 File Offset: 0x0006AC70
		public virtual XmlNode RemoveNamedItem(string localName, string namespaceURI)
		{
			int num = this.FindNodeOffset(localName, namespaceURI);
			if (num >= 0)
			{
				return this.RemoveNodeAt(num);
			}
			return null;
		}

		/// <summary>Provides support for the "foreach" style iteration over the collection of nodes in the <see langword="XmlNamedNodeMap" />.</summary>
		/// <returns>An enumerator object.</returns>
		// Token: 0x060011CE RID: 4558 RVA: 0x0006CA93 File Offset: 0x0006AC93
		public virtual IEnumerator GetEnumerator()
		{
			return this.nodes.GetEnumerator();
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0006CAA0 File Offset: 0x0006ACA0
		internal int FindNodeOffset(string name)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.nodes[i];
				if (name == xmlNode.Name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0006CAE4 File Offset: 0x0006ACE4
		internal int FindNodeOffset(string localName, string namespaceURI)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.nodes[i];
				if (xmlNode.LocalName == localName && xmlNode.NamespaceURI == namespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0006CB38 File Offset: 0x0006AD38
		internal virtual XmlNode AddNode(XmlNode node)
		{
			XmlNode oldParent;
			if (node.NodeType == XmlNodeType.Attribute)
			{
				oldParent = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				oldParent = node.ParentNode;
			}
			string value = node.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(node, oldParent, this.parent, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.Add(node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0006CBB8 File Offset: 0x0006ADB8
		internal virtual XmlNode AddNodeForLoad(XmlNode node, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(node, this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			this.nodes.Add(node);
			node.SetParent(this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return node;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x0006CC00 File Offset: 0x0006AE00
		internal virtual XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = (XmlNode)this.nodes[i];
			string value = xmlNode.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(xmlNode, this.parent, null, value, value, XmlNodeChangedAction.Remove);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.RemoveAt(i);
			xmlNode.SetParent(null);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return xmlNode;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x0006CC6F File Offset: 0x0006AE6F
		internal XmlNode ReplaceNodeAt(int i, XmlNode node)
		{
			XmlNode result = this.RemoveNodeAt(i);
			this.InsertNodeAt(i, node);
			return result;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0006CC84 File Offset: 0x0006AE84
		internal virtual XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode oldParent;
			if (node.NodeType == XmlNodeType.Attribute)
			{
				oldParent = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				oldParent = node.ParentNode;
			}
			string value = node.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(node, oldParent, this.parent, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.Insert(i, node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlNamedNodeMap()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040010B2 RID: 4274
		internal XmlNode parent;

		// Token: 0x040010B3 RID: 4275
		internal XmlNamedNodeMap.SmallXmlNodeList nodes;

		// Token: 0x020001CE RID: 462
		internal struct SmallXmlNodeList
		{
			// Token: 0x17000325 RID: 805
			// (get) Token: 0x060011D7 RID: 4567 RVA: 0x0006CD08 File Offset: 0x0006AF08
			public int Count
			{
				get
				{
					if (this.field == null)
					{
						return 0;
					}
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						return arrayList.Count;
					}
					return 1;
				}
			}

			// Token: 0x17000326 RID: 806
			public object this[int index]
			{
				get
				{
					if (this.field == null)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						return arrayList[index];
					}
					if (index != 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.field;
				}
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x0006CD84 File Offset: 0x0006AF84
			public void Add(object value)
			{
				if (this.field == null)
				{
					if (value == null)
					{
						this.field = new ArrayList
						{
							null
						};
						return;
					}
					this.field = value;
					return;
				}
				else
				{
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						arrayList.Add(value);
						return;
					}
					this.field = new ArrayList
					{
						this.field,
						value
					};
					return;
				}
			}

			// Token: 0x060011DA RID: 4570 RVA: 0x0006CDF4 File Offset: 0x0006AFF4
			public void RemoveAt(int index)
			{
				if (this.field == null)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				ArrayList arrayList = this.field as ArrayList;
				if (arrayList != null)
				{
					arrayList.RemoveAt(index);
					return;
				}
				if (index != 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.field = null;
			}

			// Token: 0x060011DB RID: 4571 RVA: 0x0006CE40 File Offset: 0x0006B040
			public void Insert(int index, object value)
			{
				if (this.field == null)
				{
					if (index != 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.Add(value);
					return;
				}
				else
				{
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						arrayList.Insert(index, value);
						return;
					}
					if (index == 0)
					{
						this.field = new ArrayList
						{
							value,
							this.field
						};
						return;
					}
					if (index == 1)
					{
						this.field = new ArrayList
						{
							this.field,
							value
						};
						return;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x060011DC RID: 4572 RVA: 0x0006CEDC File Offset: 0x0006B0DC
			public IEnumerator GetEnumerator()
			{
				if (this.field == null)
				{
					return XmlDocument.EmptyEnumerator;
				}
				ArrayList arrayList = this.field as ArrayList;
				if (arrayList != null)
				{
					return arrayList.GetEnumerator();
				}
				return new XmlNamedNodeMap.SmallXmlNodeList.SingleObjectEnumerator(this.field);
			}

			// Token: 0x040010B4 RID: 4276
			private object field;

			// Token: 0x020001CF RID: 463
			private class SingleObjectEnumerator : IEnumerator
			{
				// Token: 0x060011DD RID: 4573 RVA: 0x0006CF18 File Offset: 0x0006B118
				public SingleObjectEnumerator(object value)
				{
					this.loneValue = value;
				}

				// Token: 0x17000327 RID: 807
				// (get) Token: 0x060011DE RID: 4574 RVA: 0x0006CF2E File Offset: 0x0006B12E
				public object Current
				{
					get
					{
						if (this.position != 0)
						{
							throw new InvalidOperationException();
						}
						return this.loneValue;
					}
				}

				// Token: 0x060011DF RID: 4575 RVA: 0x0006CF44 File Offset: 0x0006B144
				public bool MoveNext()
				{
					if (this.position < 0)
					{
						this.position = 0;
						return true;
					}
					this.position = 1;
					return false;
				}

				// Token: 0x060011E0 RID: 4576 RVA: 0x0006CF60 File Offset: 0x0006B160
				public void Reset()
				{
					this.position = -1;
				}

				// Token: 0x040010B5 RID: 4277
				private object loneValue;

				// Token: 0x040010B6 RID: 4278
				private int position = -1;
			}
		}
	}
}
