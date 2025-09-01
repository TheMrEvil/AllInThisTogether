using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Represents white space in element content.</summary>
	// Token: 0x020001DD RID: 477
	public class XmlWhitespace : XmlCharacterData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWhitespace" /> class.</summary>
		/// <param name="strData">The white space characters of the node.</param>
		/// <param name="doc">The <see cref="T:System.Xml.XmlDocument" /> object.</param>
		// Token: 0x060012FD RID: 4861 RVA: 0x0007079D File Offset: 0x0006E99D
		protected internal XmlWhitespace(string strData, XmlDocument doc) : base(strData, doc)
		{
			if (!doc.IsLoading && !base.CheckOnData(strData))
			{
				throw new ArgumentException(Res.GetString("The string for white space contains an invalid character."));
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For <see langword="XmlWhitespace" /> nodes, this property returns <see langword="#whitespace" />.</returns>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x00070A8E File Offset: 0x0006EC8E
		public override string Name
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For <see langword="XmlWhitespace" /> nodes, this property returns <see langword="#whitespace" />.</returns>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x00070A8E File Offset: 0x0006EC8E
		public override string LocalName
		{
			get
			{
				return this.OwnerDocument.strNonSignificantWhitespaceName;
			}
		}

		/// <summary>Gets the type of the node.</summary>
		/// <returns>For <see langword="XmlWhitespace" /> nodes, the value is <see cref="F:System.Xml.XmlNodeType.Whitespace" />.</returns>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00070A9B File Offset: 0x0006EC9B
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Whitespace;
			}
		}

		/// <summary>Gets the parent of the current node.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNode" /> parent node of the current node.</returns>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00070AA0 File Offset: 0x0006ECA0
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

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The white space characters found in the node.</returns>
		/// <exception cref="T:System.ArgumentException">Setting <see cref="P:System.Xml.XmlWhitespace.Value" /> to invalid white space characters. </exception>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x00067404 File Offset: 0x00065604
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x00070848 File Offset: 0x0006EA48
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

		/// <summary>Creates a duplicate of this node.</summary>
		/// <param name="deep">
		///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. For white space nodes, the cloned node always includes the data value, regardless of the parameter setting. </param>
		/// <returns>The cloned node.</returns>
		// Token: 0x06001304 RID: 4868 RVA: 0x00070AF9 File Offset: 0x0006ECF9
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateWhitespace(this.Data);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see cref="T:System.Xml.XmlWriter" /> to which you want to save.</param>
		// Token: 0x06001305 RID: 4869 RVA: 0x00070B0C File Offset: 0x0006ED0C
		public override void WriteTo(XmlWriter w)
		{
			w.WriteWhitespace(this.Data);
		}

		/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The <see cref="T:System.Xml.XmlWriter" /> to which you want to save. </param>
		// Token: 0x06001306 RID: 4870 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x00070B1C File Offset: 0x0006ED1C
		internal override XPathNodeType XPNodeType
		{
			get
			{
				XPathNodeType result = XPathNodeType.Whitespace;
				base.DecideXPNodeTypeForTextNodes(this, ref result);
				return result;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the text node that immediately precedes this node.</summary>
		/// <returns>Returns <see cref="T:System.Xml.XmlNode" />.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x000673DD File Offset: 0x000655DD
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
