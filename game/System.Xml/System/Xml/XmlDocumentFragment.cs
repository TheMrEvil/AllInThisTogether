using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a lightweight object that is useful for tree insert operations.</summary>
	// Token: 0x020001BD RID: 445
	public class XmlDocumentFragment : XmlNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDocumentFragment" /> class.</summary>
		/// <param name="ownerDocument">The XML document that is the source of the fragment.</param>
		// Token: 0x060010DF RID: 4319 RVA: 0x000697FA File Offset: 0x000679FA
		protected internal XmlDocumentFragment(XmlDocument ownerDocument)
		{
			if (ownerDocument == null)
			{
				throw new ArgumentException(Res.GetString("Cannot create a node without an owner document."));
			}
			this.parentNode = ownerDocument;
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For <see langword="XmlDocumentFragment" />, the name is <see langword="#document-fragment" />.</returns>
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0006981C File Offset: 0x00067A1C
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For <see langword="XmlDocumentFragment" /> nodes, the local name is <see langword="#document-fragment" />.</returns>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0006981C File Offset: 0x00067A1C
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strDocumentFragmentName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For <see langword="XmlDocumentFragment" /> nodes, this value is XmlNodeType.DocumentFragment.</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00069829 File Offset: 0x00067A29
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.DocumentFragment;
			}
		}

		/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
		/// <returns>The parent of this node.For <see langword="XmlDocumentFragment" /> nodes, this property is always <see langword="null" />.</returns>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public override XmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlDocument" /> to which this node belongs.</summary>
		/// <returns>The <see langword="XmlDocument" /> to which this node belongs.</returns>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0006982D File Offset: 0x00067A2D
		public override XmlDocument OwnerDocument
		{
			get
			{
				return (XmlDocument)this.parentNode;
			}
		}

		/// <summary>Gets or sets the markup representing the children of this node.</summary>
		/// <returns>The markup of the children of this node.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML specified when setting this property is not well-formed. </exception>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x0006900B File Offset: 0x0006720B
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x0006983A File Offset: 0x00067A3A
		public override string InnerXml
		{
			get
			{
				return base.InnerXml;
			}
			set
			{
				this.RemoveAll();
				new XmlLoader().ParsePartialContent(this, value, XmlNodeType.Element);
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. </param>
		/// <returns>The cloned node.</returns>
		// Token: 0x060010E7 RID: 4327 RVA: 0x00069850 File Offset: 0x00067A50
		public override XmlNode CloneNode(bool deep)
		{
			XmlDocument ownerDocument = this.OwnerDocument;
			XmlDocumentFragment xmlDocumentFragment = ownerDocument.CreateDocumentFragment();
			if (deep)
			{
				xmlDocumentFragment.CopyChildren(ownerDocument, this, deep);
			}
			return xmlDocumentFragment;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsContainer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x00069878 File Offset: 0x00067A78
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x00069880 File Offset: 0x00067A80
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

		// Token: 0x060010EB RID: 4331 RVA: 0x0006988C File Offset: 0x00067A8C
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
			case XmlNodeType.XmlDeclaration:
			{
				XmlNode firstChild = this.FirstChild;
				return firstChild == null || firstChild.NodeType != XmlNodeType.XmlDeclaration;
			}
			}
			return false;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00069902 File Offset: 0x00067B02
		internal override bool CanInsertAfter(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || (refChild == null && this.LastNode == null);
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0006991E File Offset: 0x00067B1E
		internal override bool CanInsertBefore(XmlNode newChild, XmlNode refChild)
		{
			return newChild.NodeType != XmlNodeType.XmlDeclaration || refChild == null || refChild == this.FirstChild;
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060010EE RID: 4334 RVA: 0x00069185 File Offset: 0x00067385
		public override void WriteTo(XmlWriter w)
		{
			this.WriteContentTo(w);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060010EF RID: 4335 RVA: 0x0006993C File Offset: 0x00067B3C
		public override void WriteContentTo(XmlWriter w)
		{
			foreach (object obj in this)
			{
				((XmlNode)obj).WriteTo(w);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Root;
			}
		}

		// Token: 0x04001073 RID: 4211
		private XmlLinkedNode lastChild;
	}
}
