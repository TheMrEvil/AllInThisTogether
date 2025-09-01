using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents a CDATA section.</summary>
	// Token: 0x020001B6 RID: 438
	public class XmlCDataSection : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlCDataSection" /> class.</summary>
		/// <param name="data">
		///       <see cref="T:System.String" /> that contains character data.</param>
		/// <param name="doc">
		///       <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x0600101C RID: 4124 RVA: 0x0006734B File Offset: 0x0006554B
		protected internal XmlCDataSection(string data, XmlDocument doc) : base(data, doc)
		{
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For CDATA nodes, the name is <see langword="#cdata-section" />.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00067355 File Offset: 0x00065555
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For CDATA nodes, the local name is <see langword="#cdata-section" />.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x00067355 File Offset: 0x00065555
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strCDataSectionName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>The node type. For CDATA nodes, the value is XmlNodeType.CDATA.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00067362 File Offset: 0x00065562
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
		/// <returns>The <see langword="XmlNode" /> that is the parent of the current node. If a node has just been created and not yet added to the tree, or if it has been removed from the tree, the parent is <see langword="null" />. For all other nodes, the value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node. The following table describes the possible return values for the <see langword="ParentNode" /> property.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x00067368 File Offset: 0x00065568
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return null;
					}
					if (nodeType - XmlNodeType.Whitespace > 1)
					{
						return this.parentNode;
					}
				}
				XmlNode parentNode = this.parentNode.parentNode;
				while (parentNode.IsText)
				{
					parentNode = parentNode.parentNode;
				}
				return parentNode;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. Because CDATA nodes do not have children, regardless of the parameter setting, the cloned node will always include the data content. </param>
		/// <returns>The cloned node.</returns>
		// Token: 0x06001021 RID: 4129 RVA: 0x000673BC File Offset: 0x000655BC
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateCDataSection(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x06001022 RID: 4130 RVA: 0x000673CF File Offset: 0x000655CF
		public override void WriteTo(XmlWriter w)
		{
			w.WriteCData(this.Data);
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x06001023 RID: 4131 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x00067362 File Offset: 0x00065562
		internal override XPathNodeType XPNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the text node that immediately precedes this node.</summary>
		/// <returns>Returns <see cref="T:System.Xml.XmlNode" />.</returns>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x000673DD File Offset: 0x000655DD
		public override XmlNode PreviousText
		{
			get
			{
				if (this.parentNode.IsText)
				{
					return this.parentNode;
				}
				return null;
			}
		}
	}
}
