using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents white space between markup in a mixed content node or white space within an xml:space= 'preserve' scope. This is also referred to as significant white space.</summary>
	// Token: 0x020001DA RID: 474
	public class XmlSignificantWhitespace : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlSignificantWhitespace" /> class.</summary>
		/// <param name="strData">The white space characters of the node.</param>
		/// <param name="doc">The <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x060012D6 RID: 4822 RVA: 0x0007079D File Offset: 0x0006E99D
		protected internal XmlSignificantWhitespace(string strData, XmlDocument doc) : base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For <see langword="XmlSignificantWhitespace" /> nodes, this property returns <see langword="#significant-whitespace" />.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x000707C8 File Offset: 0x0006E9C8
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For <see langword="XmlSignificantWhitespace" /> nodes, this property returns <see langword="#significant-whitespace" />.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060012D8 RID: 4824 RVA: 0x000707C8 File Offset: 0x0006E9C8
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For <see langword="XmlSignificantWhitespace" /> nodes, this value is XmlNodeType.SignificantWhitespace.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000707D5 File Offset: 0x0006E9D5
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.SignificantWhitespace;
			}
		}

		/// <summary>Gets the parent of the current node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> parent node of the current node.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x000707DC File Offset: 0x0006E9DC
		public override XmlNode ParentNode
		{
			get
			{
				XmlNodeType nodeType = this.parentNode.NodeType;
				if (nodeType - XmlNodeType.Text > 1)
				{
					if (nodeType == XmlNodeType.Document)
					{
						return base.ParentNode;
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
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. For significant white space nodes, the cloned node always includes the data value, regardless of the parameter setting. </param>
		/// <returns>The cloned node.</returns>
		// Token: 0x060012DB RID: 4827 RVA: 0x00070835 File Offset: 0x0006EA35
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateSignificantWhitespace(this.Data);
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The white space characters found in the node.</returns>
		/// <exception cref="T:System.ArgumentException">Setting <see langword="Value" /> to invalid white space characters. </exception>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060012DC RID: 4828 RVA: 0x00067404 File Offset: 0x00065604
		// (set) Token: 0x060012DD RID: 4829 RVA: 0x00070848 File Offset: 0x0006EA48
		public override string Value
		{
			get
			{
				return this.Data;
			}
			set
			{
				if (base.CheckOnData(value))
				{
					this.Data = value;
					return;
				}
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060012DE RID: 4830 RVA: 0x0007086A File Offset: 0x0006EA6A
		public override void WriteTo(XmlWriter w)
		{
			w.WriteString(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
		// Token: 0x060012DF RID: 4831 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060012E0 RID: 4832 RVA: 0x00070878 File Offset: 0x0006EA78
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType result = XPathNodeType.SignificantWhitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref result);
				return result;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the text node that immediately precedes this node.</summary>
		/// <returns>Returns <see cref="T:System.Xml.XmlNode" />.</returns>
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x000673DD File Offset: 0x000655DD
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
