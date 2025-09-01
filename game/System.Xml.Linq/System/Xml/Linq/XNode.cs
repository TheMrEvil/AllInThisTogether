using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq
{
	/// <summary>Represents the abstract concept of a node (element, comment, document type, processing instruction, or text node) in the XML tree.</summary>
	// Token: 0x0200004F RID: 79
	public abstract class XNode : XObject
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000CFB6 File Offset: 0x0000B1B6
		internal XNode()
		{
		}

		/// <summary>Gets the next sibling node of this node.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNode" /> that contains the next sibling node.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000CFBE File Offset: 0x0000B1BE
		public XNode NextNode
		{
			get
			{
				if (this.parent != null && this != this.parent.content)
				{
					return this.next;
				}
				return null;
			}
		}

		/// <summary>Gets the previous sibling node of this node.</summary>
		/// <returns>The <see cref="T:System.Xml.Linq.XNode" /> that contains the previous sibling node.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000CFE0 File Offset: 0x0000B1E0
		public XNode PreviousNode
		{
			get
			{
				if (this.parent == null)
				{
					return null;
				}
				XNode xnode = ((XNode)this.parent.content).next;
				XNode result = null;
				while (xnode != this)
				{
					result = xnode;
					xnode = xnode.next;
				}
				return result;
			}
		}

		/// <summary>Gets a comparer that can compare the relative position of two nodes.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XNodeDocumentOrderComparer" /> that can compare the relative position of two nodes.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000D01F File Offset: 0x0000B21F
		public static XNodeDocumentOrderComparer DocumentOrderComparer
		{
			get
			{
				if (XNode.s_documentOrderComparer == null)
				{
					XNode.s_documentOrderComparer = new XNodeDocumentOrderComparer();
				}
				return XNode.s_documentOrderComparer;
			}
		}

		/// <summary>Gets a comparer that can compare two nodes for value equality.</summary>
		/// <returns>A <see cref="T:System.Xml.Linq.XNodeEqualityComparer" /> that can compare two nodes for value equality.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000D037 File Offset: 0x0000B237
		public static XNodeEqualityComparer EqualityComparer
		{
			get
			{
				if (XNode.s_equalityComparer == null)
				{
					XNode.s_equalityComparer = new XNodeEqualityComparer();
				}
				return XNode.s_equalityComparer;
			}
		}

		/// <summary>Adds the specified content immediately after this node.</summary>
		/// <param name="content">A content object that contains simple content or a collection of content objects to be added after this node.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		// Token: 0x060002A3 RID: 675 RVA: 0x0000D050 File Offset: 0x0000B250
		public void AddAfterSelf(object content)
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			new Inserter(this.parent, this).Add(content);
		}

		/// <summary>Adds the specified content immediately after this node.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		// Token: 0x060002A4 RID: 676 RVA: 0x0000D085 File Offset: 0x0000B285
		public void AddAfterSelf(params object[] content)
		{
			this.AddAfterSelf(content);
		}

		/// <summary>Adds the specified content immediately before this node.</summary>
		/// <param name="content">A content object that contains simple content or a collection of content objects to be added before this node.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		// Token: 0x060002A5 RID: 677 RVA: 0x0000D090 File Offset: 0x0000B290
		public void AddBeforeSelf(object content)
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			XNode xnode = (XNode)this.parent.content;
			while (xnode.next != this)
			{
				xnode = xnode.next;
			}
			if (xnode == this.parent.content)
			{
				xnode = null;
			}
			new Inserter(this.parent, xnode).Add(content);
		}

		/// <summary>Adds the specified content immediately before this node.</summary>
		/// <param name="content">A parameter list of content objects.</param>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		// Token: 0x060002A6 RID: 678 RVA: 0x0000D0F8 File Offset: 0x0000B2F8
		public void AddBeforeSelf(params object[] content)
		{
			this.AddBeforeSelf(content);
		}

		/// <summary>Returns a collection of the ancestor elements of this node.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the ancestor elements of this node.</returns>
		// Token: 0x060002A7 RID: 679 RVA: 0x0000D101 File Offset: 0x0000B301
		public IEnumerable<XElement> Ancestors()
		{
			return this.GetAncestors(null, false);
		}

		/// <summary>Returns a filtered collection of the ancestor elements of this node. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the ancestor elements of this node. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.  
		///  The nodes in the returned collection are in reverse document order.  
		///  This method uses deferred execution.</returns>
		// Token: 0x060002A8 RID: 680 RVA: 0x0000D10B File Offset: 0x0000B30B
		public IEnumerable<XElement> Ancestors(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return this.GetAncestors(name, false);
		}

		/// <summary>Compares two nodes to determine their relative XML document order.</summary>
		/// <param name="n1">First <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="n2">Second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>An <see langword="int" /> containing 0 if the nodes are equal; -1 if <paramref name="n1" /> is before <paramref name="n2" />; 1 if <paramref name="n1" /> is after <paramref name="n2" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The two nodes do not share a common ancestor.</exception>
		// Token: 0x060002A9 RID: 681 RVA: 0x0000D124 File Offset: 0x0000B324
		public static int CompareDocumentOrder(XNode n1, XNode n2)
		{
			if (n1 == n2)
			{
				return 0;
			}
			if (n1 == null)
			{
				return -1;
			}
			if (n2 == null)
			{
				return 1;
			}
			if (n1.parent != n2.parent)
			{
				int num = 0;
				XNode xnode = n1;
				while (xnode.parent != null)
				{
					xnode = xnode.parent;
					num++;
				}
				XNode xnode2 = n2;
				while (xnode2.parent != null)
				{
					xnode2 = xnode2.parent;
					num--;
				}
				if (xnode != xnode2)
				{
					throw new InvalidOperationException("A common ancestor is missing.");
				}
				if (num < 0)
				{
					do
					{
						n2 = n2.parent;
						num++;
					}
					while (num != 0);
					if (n1 == n2)
					{
						return -1;
					}
				}
				else if (num > 0)
				{
					do
					{
						n1 = n1.parent;
						num--;
					}
					while (num != 0);
					if (n1 == n2)
					{
						return 1;
					}
				}
				while (n1.parent != n2.parent)
				{
					n1 = n1.parent;
					n2 = n2.parent;
				}
			}
			else if (n1.parent == null)
			{
				throw new InvalidOperationException("A common ancestor is missing.");
			}
			XNode xnode3 = (XNode)n1.parent.content;
			for (;;)
			{
				xnode3 = xnode3.next;
				if (xnode3 == n1)
				{
					break;
				}
				if (xnode3 == n2)
				{
					return 1;
				}
			}
			return -1;
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlReader" /> for this node.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> that can be used to read this node and its descendants.</returns>
		// Token: 0x060002AA RID: 682 RVA: 0x0000D219 File Offset: 0x0000B419
		public XmlReader CreateReader()
		{
			return new XNodeReader(this, null);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlReader" /> with the options specified by the <paramref name="readerOptions" /> parameter.</summary>
		/// <param name="readerOptions">A <see cref="T:System.Xml.Linq.ReaderOptions" /> object that specifies whether to omit duplicate namespaces.</param>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object.</returns>
		// Token: 0x060002AB RID: 683 RVA: 0x0000D222 File Offset: 0x0000B422
		public XmlReader CreateReader(ReaderOptions readerOptions)
		{
			return new XNodeReader(this, null, readerOptions);
		}

		/// <summary>Returns a collection of the sibling nodes after this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the sibling nodes after this node, in document order.</returns>
		// Token: 0x060002AC RID: 684 RVA: 0x0000D22C File Offset: 0x0000B42C
		public IEnumerable<XNode> NodesAfterSelf()
		{
			XNode i = this;
			while (i.parent != null && i != i.parent.content)
			{
				i = i.next;
				yield return i;
			}
			yield break;
		}

		/// <summary>Returns a collection of the sibling nodes before this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XNode" /> of the sibling nodes before this node, in document order.</returns>
		// Token: 0x060002AD RID: 685 RVA: 0x0000D23C File Offset: 0x0000B43C
		public IEnumerable<XNode> NodesBeforeSelf()
		{
			if (this.parent != null)
			{
				XNode i = (XNode)this.parent.content;
				do
				{
					i = i.next;
					if (i == this)
					{
						break;
					}
					yield return i;
				}
				while (this.parent != null && this.parent == i.parent);
				i = null;
			}
			yield break;
		}

		/// <summary>Returns a collection of the sibling elements after this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements after this node, in document order.</returns>
		// Token: 0x060002AE RID: 686 RVA: 0x0000D24C File Offset: 0x0000B44C
		public IEnumerable<XElement> ElementsAfterSelf()
		{
			return this.GetElementsAfterSelf(null);
		}

		/// <summary>Returns a filtered collection of the sibling elements after this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements after this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		// Token: 0x060002AF RID: 687 RVA: 0x0000D255 File Offset: 0x0000B455
		public IEnumerable<XElement> ElementsAfterSelf(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return this.GetElementsAfterSelf(name);
		}

		/// <summary>Returns a collection of the sibling elements before this node, in document order.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements before this node, in document order.</returns>
		// Token: 0x060002B0 RID: 688 RVA: 0x0000D26D File Offset: 0x0000B46D
		public IEnumerable<XElement> ElementsBeforeSelf()
		{
			return this.GetElementsBeforeSelf(null);
		}

		/// <summary>Returns a filtered collection of the sibling elements before this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> to match.</param>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XElement" /> of the sibling elements before this node, in document order. Only elements that have a matching <see cref="T:System.Xml.Linq.XName" /> are included in the collection.</returns>
		// Token: 0x060002B1 RID: 689 RVA: 0x0000D276 File Offset: 0x0000B476
		public IEnumerable<XElement> ElementsBeforeSelf(XName name)
		{
			if (!(name != null))
			{
				return XElement.EmptySequence;
			}
			return this.GetElementsBeforeSelf(name);
		}

		/// <summary>Determines if the current node appears after a specified node in terms of document order.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> to compare for document order.</param>
		/// <returns>
		///   <see langword="true" /> if this node appears after the specified node; otherwise <see langword="false" />.</returns>
		// Token: 0x060002B2 RID: 690 RVA: 0x0000D28E File Offset: 0x0000B48E
		public bool IsAfter(XNode node)
		{
			return XNode.CompareDocumentOrder(this, node) > 0;
		}

		/// <summary>Determines if the current node appears before a specified node in terms of document order.</summary>
		/// <param name="node">The <see cref="T:System.Xml.Linq.XNode" /> to compare for document order.</param>
		/// <returns>
		///   <see langword="true" /> if this node appears before the specified node; otherwise <see langword="false" />.</returns>
		// Token: 0x060002B3 RID: 691 RVA: 0x0000D29A File Offset: 0x0000B49A
		public bool IsBefore(XNode node)
		{
			return XNode.CompareDocumentOrder(this, node) < 0;
		}

		/// <summary>Creates an <see cref="T:System.Xml.Linq.XNode" /> from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">An <see cref="T:System.Xml.XmlReader" /> positioned at the node to read into this <see cref="T:System.Xml.Linq.XNode" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XNode" /> that contains the node and its descendant nodes that were read from the reader. The runtime type of the node is determined by the node type (<see cref="P:System.Xml.Linq.XObject.NodeType" />) of the first node encountered in the reader.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on a recognized node type.</exception>
		/// <exception cref="T:System.Xml.XmlException">The underlying <see cref="T:System.Xml.XmlReader" /> throws an exception.</exception>
		// Token: 0x060002B4 RID: 692 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		public static XNode ReadFrom(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadState != ReadState.Interactive)
			{
				throw new InvalidOperationException("The XmlReader state should be Interactive.");
			}
			switch (reader.NodeType)
			{
			case XmlNodeType.Element:
				return new XElement(reader);
			case XmlNodeType.Text:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
				return new XText(reader);
			case XmlNodeType.CDATA:
				return new XCData(reader);
			case XmlNodeType.ProcessingInstruction:
				return new XProcessingInstruction(reader);
			case XmlNodeType.Comment:
				return new XComment(reader);
			case XmlNodeType.DocumentType:
				return new XDocumentType(reader);
			}
			throw new InvalidOperationException(SR.Format("The XmlReader should not be on a node of type {0}.", reader.NodeType));
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D364 File Offset: 0x0000B564
		public static Task<XNode> ReadFromAsync(XmlReader reader, CancellationToken cancellationToken)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<XNode>(cancellationToken);
			}
			return XNode.ReadFromAsyncInternal(reader, cancellationToken);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000D38C File Offset: 0x0000B58C
		private static Task<XNode> ReadFromAsyncInternal(XmlReader reader, CancellationToken cancellationToken)
		{
			XNode.<ReadFromAsyncInternal>d__31 <ReadFromAsyncInternal>d__;
			<ReadFromAsyncInternal>d__.reader = reader;
			<ReadFromAsyncInternal>d__.cancellationToken = cancellationToken;
			<ReadFromAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder<XNode>.Create();
			<ReadFromAsyncInternal>d__.<>1__state = -1;
			<ReadFromAsyncInternal>d__.<>t__builder.Start<XNode.<ReadFromAsyncInternal>d__31>(ref <ReadFromAsyncInternal>d__);
			return <ReadFromAsyncInternal>d__.<>t__builder.Task;
		}

		/// <summary>Removes this node from its parent.</summary>
		/// <exception cref="T:System.InvalidOperationException">The parent is <see langword="null" />.</exception>
		// Token: 0x060002B7 RID: 695 RVA: 0x0000D3D7 File Offset: 0x0000B5D7
		public void Remove()
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			this.parent.RemoveNode(this);
		}

		/// <summary>Replaces this node with the specified content.</summary>
		/// <param name="content">Content that replaces this node.</param>
		// Token: 0x060002B8 RID: 696 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		public void ReplaceWith(object content)
		{
			if (this.parent == null)
			{
				throw new InvalidOperationException("The parent is missing.");
			}
			XContainer parent = this.parent;
			XNode xnode = (XNode)this.parent.content;
			while (xnode.next != this)
			{
				xnode = xnode.next;
			}
			if (xnode == this.parent.content)
			{
				xnode = null;
			}
			this.parent.RemoveNode(this);
			if (xnode != null && xnode.parent != parent)
			{
				throw new InvalidOperationException("This operation was corrupted by external code.");
			}
			new Inserter(parent, xnode).Add(content);
		}

		/// <summary>Replaces this node with the specified content.</summary>
		/// <param name="content">A parameter list of the new content.</param>
		// Token: 0x060002B9 RID: 697 RVA: 0x0000D485 File Offset: 0x0000B685
		public void ReplaceWith(params object[] content)
		{
			this.ReplaceWith(content);
		}

		/// <summary>Returns the indented XML for this node.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the indented XML.</returns>
		// Token: 0x060002BA RID: 698 RVA: 0x0000D48E File Offset: 0x0000B68E
		public override string ToString()
		{
			return this.GetXmlString(base.GetSaveOptionsFromAnnotations());
		}

		/// <summary>Returns the XML for this node, optionally disabling formatting.</summary>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		/// <returns>A <see cref="T:System.String" /> containing the XML.</returns>
		// Token: 0x060002BB RID: 699 RVA: 0x0000D49C File Offset: 0x0000B69C
		public string ToString(SaveOptions options)
		{
			return this.GetXmlString(options);
		}

		/// <summary>Compares the values of two nodes, including the values of all descendant nodes.</summary>
		/// <param name="n1">The first <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <param name="n2">The second <see cref="T:System.Xml.Linq.XNode" /> to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the nodes are equal; otherwise <see langword="false" />.</returns>
		// Token: 0x060002BC RID: 700 RVA: 0x0000D4A5 File Offset: 0x0000B6A5
		public static bool DeepEquals(XNode n1, XNode n2)
		{
			return n1 == n2 || (n1 != null && n2 != null && n1.DeepEquals(n2));
		}

		/// <summary>Writes this node to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		// Token: 0x060002BD RID: 701
		public abstract void WriteTo(XmlWriter writer);

		// Token: 0x060002BE RID: 702
		public abstract Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken);

		// Token: 0x060002BF RID: 703 RVA: 0x000043E9 File Offset: 0x000025E9
		internal virtual void AppendText(StringBuilder sb)
		{
		}

		// Token: 0x060002C0 RID: 704
		internal abstract XNode CloneNode();

		// Token: 0x060002C1 RID: 705
		internal abstract bool DeepEquals(XNode node);

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D4BC File Offset: 0x0000B6BC
		internal IEnumerable<XElement> GetAncestors(XName name, bool self)
		{
			for (XElement e = (self ? this : this.parent) as XElement; e != null; e = (e.parent as XElement))
			{
				if (name == null || e.name == name)
				{
					yield return e;
				}
			}
			yield break;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D4DA File Offset: 0x0000B6DA
		private IEnumerable<XElement> GetElementsAfterSelf(XName name)
		{
			XNode i = this;
			while (i.parent != null && i != i.parent.content)
			{
				i = i.next;
				XElement xelement = i as XElement;
				if (xelement != null && (name == null || xelement.name == name))
				{
					yield return xelement;
				}
			}
			yield break;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D4F1 File Offset: 0x0000B6F1
		private IEnumerable<XElement> GetElementsBeforeSelf(XName name)
		{
			if (this.parent != null)
			{
				XNode i = (XNode)this.parent.content;
				do
				{
					i = i.next;
					if (i == this)
					{
						break;
					}
					XElement xelement = i as XElement;
					if (xelement != null && (name == null || xelement.name == name))
					{
						yield return xelement;
					}
				}
				while (this.parent != null && this.parent == i.parent);
				i = null;
			}
			yield break;
		}

		// Token: 0x060002C5 RID: 709
		internal abstract int GetDeepHashCode();

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D508 File Offset: 0x0000B708
		internal static XmlReaderSettings GetXmlReaderSettings(LoadOptions o)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			if ((o & LoadOptions.PreserveWhitespace) == LoadOptions.None)
			{
				xmlReaderSettings.IgnoreWhitespace = true;
			}
			xmlReaderSettings.DtdProcessing = DtdProcessing.Parse;
			xmlReaderSettings.MaxCharactersFromEntities = 10000000L;
			return xmlReaderSettings;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D53C File Offset: 0x0000B73C
		internal static XmlWriterSettings GetXmlWriterSettings(SaveOptions o)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			if ((o & SaveOptions.DisableFormatting) == SaveOptions.None)
			{
				xmlWriterSettings.Indent = true;
			}
			if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
			{
				xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
			}
			return xmlWriterSettings;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D570 File Offset: 0x0000B770
		private string GetXmlString(SaveOptions o)
		{
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
				xmlWriterSettings.OmitXmlDeclaration = true;
				if ((o & SaveOptions.DisableFormatting) == SaveOptions.None)
				{
					xmlWriterSettings.Indent = true;
				}
				if ((o & SaveOptions.OmitDuplicateNamespaces) != SaveOptions.None)
				{
					xmlWriterSettings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
				}
				if (this is XText)
				{
					xmlWriterSettings.ConformanceLevel = ConformanceLevel.Fragment;
				}
				using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlWriterSettings))
				{
					XDocument xdocument = this as XDocument;
					if (xdocument != null)
					{
						xdocument.WriteContentTo(xmlWriter);
					}
					else
					{
						this.WriteTo(xmlWriter);
					}
				}
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x04000190 RID: 400
		private static XNodeDocumentOrderComparer s_documentOrderComparer;

		// Token: 0x04000191 RID: 401
		private static XNodeEqualityComparer s_equalityComparer;

		// Token: 0x04000192 RID: 402
		internal XNode next;

		// Token: 0x02000050 RID: 80
		[CompilerGenerated]
		private sealed class <NodesAfterSelf>d__21 : IEnumerable<XNode>, IEnumerable, IEnumerator<XNode>, IDisposable, IEnumerator
		{
			// Token: 0x060002C9 RID: 713 RVA: 0x0000D624 File Offset: 0x0000B824
			[DebuggerHidden]
			public <NodesAfterSelf>d__21(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002CA RID: 714 RVA: 0x000043E9 File Offset: 0x000025E9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002CB RID: 715 RVA: 0x0000D640 File Offset: 0x0000B840
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				XNode xnode = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					i = xnode;
				}
				if (i.parent == null || i == i.parent.content)
				{
					return false;
				}
				i = i.next;
				this.<>2__current = i;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060002CC RID: 716 RVA: 0x0000D6C7 File Offset: 0x0000B8C7
			XNode IEnumerator<XNode>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002CD RID: 717 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060002CE RID: 718 RVA: 0x0000D6C7 File Offset: 0x0000B8C7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002CF RID: 719 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
			[DebuggerHidden]
			IEnumerator<XNode> IEnumerable<XNode>.GetEnumerator()
			{
				XNode.<NodesAfterSelf>d__21 <NodesAfterSelf>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<NodesAfterSelf>d__ = this;
				}
				else
				{
					<NodesAfterSelf>d__ = new XNode.<NodesAfterSelf>d__21(0);
					<NodesAfterSelf>d__.<>4__this = this;
				}
				return <NodesAfterSelf>d__;
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x0000D713 File Offset: 0x0000B913
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XNode>.GetEnumerator();
			}

			// Token: 0x04000193 RID: 403
			private int <>1__state;

			// Token: 0x04000194 RID: 404
			private XNode <>2__current;

			// Token: 0x04000195 RID: 405
			private int <>l__initialThreadId;

			// Token: 0x04000196 RID: 406
			public XNode <>4__this;

			// Token: 0x04000197 RID: 407
			private XNode <n>5__2;
		}

		// Token: 0x02000051 RID: 81
		[CompilerGenerated]
		private sealed class <NodesBeforeSelf>d__22 : IEnumerable<XNode>, IEnumerable, IEnumerator<XNode>, IDisposable, IEnumerator
		{
			// Token: 0x060002D1 RID: 721 RVA: 0x0000D71B File Offset: 0x0000B91B
			[DebuggerHidden]
			public <NodesBeforeSelf>d__22(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x000043E9 File Offset: 0x000025E9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x0000D738 File Offset: 0x0000B938
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				XNode xnode = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					if (xnode.parent == null || xnode.parent != i.parent)
					{
						goto IL_8D;
					}
				}
				else
				{
					this.<>1__state = -1;
					if (xnode.parent == null)
					{
						return false;
					}
					i = (XNode)xnode.parent.content;
				}
				i = i.next;
				if (i != xnode)
				{
					this.<>2__current = i;
					this.<>1__state = 1;
					return true;
				}
				IL_8D:
				i = null;
				return false;
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000D7DA File Offset: 0x0000B9DA
			XNode IEnumerator<XNode>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000058 RID: 88
			// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000D7DA File Offset: 0x0000B9DA
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
			[DebuggerHidden]
			IEnumerator<XNode> IEnumerable<XNode>.GetEnumerator()
			{
				XNode.<NodesBeforeSelf>d__22 <NodesBeforeSelf>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<NodesBeforeSelf>d__ = this;
				}
				else
				{
					<NodesBeforeSelf>d__ = new XNode.<NodesBeforeSelf>d__22(0);
					<NodesBeforeSelf>d__.<>4__this = this;
				}
				return <NodesBeforeSelf>d__;
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000D827 File Offset: 0x0000BA27
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XNode>.GetEnumerator();
			}

			// Token: 0x04000198 RID: 408
			private int <>1__state;

			// Token: 0x04000199 RID: 409
			private XNode <>2__current;

			// Token: 0x0400019A RID: 410
			private int <>l__initialThreadId;

			// Token: 0x0400019B RID: 411
			public XNode <>4__this;

			// Token: 0x0400019C RID: 412
			private XNode <n>5__2;
		}

		// Token: 0x02000052 RID: 82
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadFromAsyncInternal>d__31 : IAsyncStateMachine
		{
			// Token: 0x060002D9 RID: 729 RVA: 0x0000D830 File Offset: 0x0000BA30
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XNode result;
				try
				{
					ConfiguredTaskAwaitable<XElement>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
						if (num != 1)
						{
							if (this.reader.ReadState != ReadState.Interactive)
							{
								throw new InvalidOperationException("The XmlReader state should be Interactive.");
							}
							switch (this.reader.NodeType)
							{
							case XmlNodeType.Element:
								awaiter = XElement.CreateAsync(this.reader, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<XElement>.ConfiguredTaskAwaiter, XNode.<ReadFromAsyncInternal>d__31>(ref awaiter, ref this);
									return;
								}
								goto IL_18F;
							case XmlNodeType.Text:
							case XmlNodeType.Whitespace:
							case XmlNodeType.SignificantWhitespace:
								this.<ret>5__2 = new XText(this.reader.Value);
								goto IL_1E7;
							case XmlNodeType.CDATA:
								this.<ret>5__2 = new XCData(this.reader.Value);
								goto IL_1E7;
							case XmlNodeType.ProcessingInstruction:
							{
								string name = this.reader.Name;
								string value = this.reader.Value;
								this.<ret>5__2 = new XProcessingInstruction(name, value);
								goto IL_1E7;
							}
							case XmlNodeType.Comment:
								this.<ret>5__2 = new XComment(this.reader.Value);
								goto IL_1E7;
							case XmlNodeType.DocumentType:
							{
								string name2 = this.reader.Name;
								string attribute = this.reader.GetAttribute("PUBLIC");
								string attribute2 = this.reader.GetAttribute("SYSTEM");
								string value2 = this.reader.Value;
								this.<ret>5__2 = new XDocumentType(name2, attribute, attribute2, value2);
								goto IL_1E7;
							}
							}
							throw new InvalidOperationException(SR.Format("The XmlReader should not be on a node of type {0}.", this.reader.NodeType));
							IL_1E7:
							this.cancellationToken.ThrowIfCancellationRequested();
							awaiter2 = this.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XNode.<ReadFromAsyncInternal>d__31>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter2.GetResult();
						result = this.<ret>5__2;
						goto IL_286;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<XElement>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_18F:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<ret>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_286:
				this.<>1__state = -2;
				this.<ret>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000DAFC File Offset: 0x0000BCFC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400019D RID: 413
			public int <>1__state;

			// Token: 0x0400019E RID: 414
			public AsyncTaskMethodBuilder<XNode> <>t__builder;

			// Token: 0x0400019F RID: 415
			public XmlReader reader;

			// Token: 0x040001A0 RID: 416
			public CancellationToken cancellationToken;

			// Token: 0x040001A1 RID: 417
			private XNode <ret>5__2;

			// Token: 0x040001A2 RID: 418
			private ConfiguredTaskAwaitable<XElement>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040001A3 RID: 419
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000053 RID: 83
		[CompilerGenerated]
		private sealed class <GetAncestors>d__43 : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IDisposable, IEnumerator
		{
			// Token: 0x060002DB RID: 731 RVA: 0x0000DB0A File Offset: 0x0000BD0A
			[DebuggerHidden]
			public <GetAncestors>d__43(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002DC RID: 732 RVA: 0x000043E9 File Offset: 0x000025E9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002DD RID: 733 RVA: 0x0000DB24 File Offset: 0x0000BD24
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				XNode xnode = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					e = ((self ? xnode : xnode.parent) as XElement);
					goto IL_94;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_7E:
				e = (e.parent as XElement);
				IL_94:
				if (e == null)
				{
					return false;
				}
				if (name == null || e.name == name)
				{
					this.<>2__current = e;
					this.<>1__state = 1;
					return true;
				}
				goto IL_7E;
			}

			// Token: 0x17000059 RID: 89
			// (get) Token: 0x060002DE RID: 734 RVA: 0x0000DBCE File Offset: 0x0000BDCE
			XElement IEnumerator<XElement>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002DF RID: 735 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000DBCE File Offset: 0x0000BDCE
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002E1 RID: 737 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
			[DebuggerHidden]
			IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
			{
				XNode.<GetAncestors>d__43 <GetAncestors>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetAncestors>d__ = this;
				}
				else
				{
					<GetAncestors>d__ = new XNode.<GetAncestors>d__43(0);
					<GetAncestors>d__.<>4__this = this;
				}
				<GetAncestors>d__.name = name;
				<GetAncestors>d__.self = self;
				return <GetAncestors>d__;
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x0000DC33 File Offset: 0x0000BE33
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
			}

			// Token: 0x040001A4 RID: 420
			private int <>1__state;

			// Token: 0x040001A5 RID: 421
			private XElement <>2__current;

			// Token: 0x040001A6 RID: 422
			private int <>l__initialThreadId;

			// Token: 0x040001A7 RID: 423
			private bool self;

			// Token: 0x040001A8 RID: 424
			public bool <>3__self;

			// Token: 0x040001A9 RID: 425
			public XNode <>4__this;

			// Token: 0x040001AA RID: 426
			private XName name;

			// Token: 0x040001AB RID: 427
			public XName <>3__name;

			// Token: 0x040001AC RID: 428
			private XElement <e>5__2;
		}

		// Token: 0x02000054 RID: 84
		[CompilerGenerated]
		private sealed class <GetElementsAfterSelf>d__44 : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IDisposable, IEnumerator
		{
			// Token: 0x060002E3 RID: 739 RVA: 0x0000DC3B File Offset: 0x0000BE3B
			[DebuggerHidden]
			public <GetElementsAfterSelf>d__44(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x000043E9 File Offset: 0x000025E9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000DC58 File Offset: 0x0000BE58
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				XNode xnode = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
				}
				else
				{
					this.<>1__state = -1;
					i = xnode;
				}
				while (i.parent != null && i != i.parent.content)
				{
					i = i.next;
					XElement xelement = i as XElement;
					if (xelement != null && (name == null || xelement.name == name))
					{
						this.<>2__current = xelement;
						this.<>1__state = 1;
						return true;
					}
				}
				return false;
			}

			// Token: 0x1700005B RID: 91
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000DD0A File Offset: 0x0000BF0A
			XElement IEnumerator<XElement>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700005C RID: 92
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000DD0A File Offset: 0x0000BF0A
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x0000DD14 File Offset: 0x0000BF14
			[DebuggerHidden]
			IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
			{
				XNode.<GetElementsAfterSelf>d__44 <GetElementsAfterSelf>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetElementsAfterSelf>d__ = this;
				}
				else
				{
					<GetElementsAfterSelf>d__ = new XNode.<GetElementsAfterSelf>d__44(0);
					<GetElementsAfterSelf>d__.<>4__this = this;
				}
				<GetElementsAfterSelf>d__.name = name;
				return <GetElementsAfterSelf>d__;
			}

			// Token: 0x060002EA RID: 746 RVA: 0x0000DD63 File Offset: 0x0000BF63
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
			}

			// Token: 0x040001AD RID: 429
			private int <>1__state;

			// Token: 0x040001AE RID: 430
			private XElement <>2__current;

			// Token: 0x040001AF RID: 431
			private int <>l__initialThreadId;

			// Token: 0x040001B0 RID: 432
			public XNode <>4__this;

			// Token: 0x040001B1 RID: 433
			private XName name;

			// Token: 0x040001B2 RID: 434
			public XName <>3__name;

			// Token: 0x040001B3 RID: 435
			private XNode <n>5__2;
		}

		// Token: 0x02000055 RID: 85
		[CompilerGenerated]
		private sealed class <GetElementsBeforeSelf>d__45 : IEnumerable<XElement>, IEnumerable, IEnumerator<XElement>, IDisposable, IEnumerator
		{
			// Token: 0x060002EB RID: 747 RVA: 0x0000DD6B File Offset: 0x0000BF6B
			[DebuggerHidden]
			public <GetElementsBeforeSelf>d__45(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002EC RID: 748 RVA: 0x000043E9 File Offset: 0x000025E9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002ED RID: 749 RVA: 0x0000DD88 File Offset: 0x0000BF88
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				XNode xnode = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					goto IL_A3;
				}
				else
				{
					this.<>1__state = -1;
					if (xnode.parent == null)
					{
						return false;
					}
					i = (XNode)xnode.parent.content;
				}
				IL_42:
				i = i.next;
				if (i == xnode)
				{
					goto IL_BE;
				}
				XElement xelement = i as XElement;
				if (xelement != null && (name == null || xelement.name == name))
				{
					this.<>2__current = xelement;
					this.<>1__state = 1;
					return true;
				}
				IL_A3:
				if (xnode.parent != null && xnode.parent == i.parent)
				{
					goto IL_42;
				}
				IL_BE:
				i = null;
				return false;
			}

			// Token: 0x1700005D RID: 93
			// (get) Token: 0x060002EE RID: 750 RVA: 0x0000DE5B File Offset: 0x0000C05B
			XElement IEnumerator<XElement>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002EF RID: 751 RVA: 0x000032E5 File Offset: 0x000014E5
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700005E RID: 94
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000DE5B File Offset: 0x0000C05B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002F1 RID: 753 RVA: 0x0000DE64 File Offset: 0x0000C064
			[DebuggerHidden]
			IEnumerator<XElement> IEnumerable<XElement>.GetEnumerator()
			{
				XNode.<GetElementsBeforeSelf>d__45 <GetElementsBeforeSelf>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetElementsBeforeSelf>d__ = this;
				}
				else
				{
					<GetElementsBeforeSelf>d__ = new XNode.<GetElementsBeforeSelf>d__45(0);
					<GetElementsBeforeSelf>d__.<>4__this = this;
				}
				<GetElementsBeforeSelf>d__.name = name;
				return <GetElementsBeforeSelf>d__;
			}

			// Token: 0x060002F2 RID: 754 RVA: 0x0000DEB3 File Offset: 0x0000C0B3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Xml.Linq.XElement>.GetEnumerator();
			}

			// Token: 0x040001B4 RID: 436
			private int <>1__state;

			// Token: 0x040001B5 RID: 437
			private XElement <>2__current;

			// Token: 0x040001B6 RID: 438
			private int <>l__initialThreadId;

			// Token: 0x040001B7 RID: 439
			public XNode <>4__this;

			// Token: 0x040001B8 RID: 440
			private XName name;

			// Token: 0x040001B9 RID: 441
			public XName <>3__name;

			// Token: 0x040001BA RID: 442
			private XNode <n>5__2;
		}
	}
}
