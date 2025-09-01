using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents the content of an XML comment.</summary>
	// Token: 0x020001BA RID: 442
	public class XmlComment : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlComment" /> class.</summary>
		/// <param name="comment">The content of the comment element.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x06001040 RID: 4160 RVA: 0x0006734B File Offset: 0x0006554B
		protected internal XmlComment(string comment, XmlDocument doc) : base(comment, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For comment nodes, the value is <see langword="#comment" />.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x000678C8 File Offset: 0x00065AC8
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For comment nodes, the value is <see langword="#comment" />.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06001042 RID: 4162 RVA: 0x000678C8 File Offset: 0x00065AC8
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCommentName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For comment nodes, the value is XmlNodeType.Comment.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x000678D5 File Offset: 0x00065AD5
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Comment;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. Because comment nodes do not have children, the cloned node always includes the text content, regardless of the parameter setting. </param>
		/// <returns>The cloned node.</returns>
		// Token: 0x06001044 RID: 4164 RVA: 0x000678D8 File Offset: 0x00065AD8
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateComment(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x06001045 RID: 4165 RVA: 0x000678EB File Offset: 0x00065AEB
		public override void WriteTo(XmlWriter w)
		{
			w.WriteComment(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because comment nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x06001046 RID: 4166 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x000678D5 File Offset: 0x00065AD5
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Comment;
			}
		}
	}
}
