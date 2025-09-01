using System;
using Unity;

namespace System.Xml
{
	/// <summary>Represents a notation declaration, such as &lt;!NOTATION... &gt;.</summary>
	// Token: 0x020001D8 RID: 472
	public class XmlNotation : XmlNode
	{
		// Token: 0x060012B8 RID: 4792 RVA: 0x000706A3 File Offset: 0x0006E8A3
		internal XmlNotation(string name, string publicId, string systemId, XmlDocument doc) : base(doc)
		{
			this.name = doc.NameTable.Add(name);
			this.publicId = publicId;
			this.systemId = systemId;
		}

		/// <summary>Gets the name of the current node.</summary>
		/// <returns>The name of the notation.</returns>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x000706CE File Offset: 0x0006E8CE
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the name of the current node without the namespace prefix.</summary>
		/// <returns>For <see langword="XmlNotation" /> nodes, this property returns the name of the notation.</returns>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x000706CE File Offset: 0x0006E8CE
		public override string LocalName
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For <see langword="XmlNotation" /> nodes, the value is XmlNodeType.Notation.</returns>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0000B2C4 File Offset: 0x000094C4
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Notation;
			}
		}

		/// <summary>Creates a duplicate of this node. Notation nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlNotation" /> object throws an exception.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself.</param>
		/// <returns>Returns a <see cref="T:System.Xml.XmlNode" /> copy of the node from which the method is called.</returns>
		/// <exception cref="T:System.InvalidOperationException">Notation nodes cannot be cloned. Calling this method on an <see cref="T:System.Xml.XmlNotation" /> object throws an exception.</exception>
		// Token: 0x060012BC RID: 4796 RVA: 0x0006AA41 File Offset: 0x00068C41
		public override XmlNode CloneNode(bool deep)
		{
			throw new InvalidOperationException(Res.GetString("'Entity' and 'Notation' nodes cannot be cloned."));
		}

		/// <summary>Gets a value indicating whether the node is read-only.</summary>
		/// <returns>
		///     <see langword="true" /> if the node is read-only; otherwise <see langword="false" />.Because <see langword="XmlNotation" /> nodes are read-only, this property always returns <see langword="true" />.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the value of the public identifier on the notation declaration.</summary>
		/// <returns>The public identifier on the notation. If there is no public identifier, <see langword="null" /> is returned.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x000706D6 File Offset: 0x0006E8D6
		public string PublicId
		{
			get
			{
				return this.publicId;
			}
		}

		/// <summary>Gets the value of the system identifier on the notation declaration.</summary>
		/// <returns>The system identifier on the notation. If there is no system identifier, <see langword="null" /> is returned.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x000706DE File Offset: 0x0006E8DE
		public string SystemId
		{
			get
			{
				return this.systemId;
			}
		}

		/// <summary>Gets the markup representing this node and all its children.</summary>
		/// <returns>For <see langword="XmlNotation" /> nodes, String.Empty is returned.</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0001E51E File Offset: 0x0001C71E
		public override string OuterXml
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the markup representing the children of this node.</summary>
		/// <returns>For <see langword="XmlNotation" /> nodes, String.Empty is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">Attempting to set the property. </exception>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0001E51E File Offset: 0x0001C71E
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x0006AADF File Offset: 0x00068CDF
		public override string InnerXml
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException(Res.GetString("Cannot set the 'InnerXml' for the current node because it is either read-only or cannot have children."));
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />. This method has no effect on <see langword="XmlNotation" /> nodes.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060012C3 RID: 4803 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteTo(XmlWriter w)
		{
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. This method has no effect on <see langword="XmlNotation" /> nodes.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060012C4 RID: 4804 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00067344 File Offset: 0x00065544
		internal XmlNotation()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040010E0 RID: 4320
		private string publicId;

		// Token: 0x040010E1 RID: 4321
		private string systemId;

		// Token: 0x040010E2 RID: 4322
		private string name;
	}
}
