using System;

namespace System.Xml.XPath
{
	/// <summary>Defines the XPath node types that can be returned from the <see cref="T:System.Xml.XPath.XPathNavigator" /> class.</summary>
	// Token: 0x02000264 RID: 612
	public enum XPathNodeType
	{
		/// <summary>The root node of the XML document or node tree.</summary>
		// Token: 0x0400182A RID: 6186
		Root,
		/// <summary>An element, such as &lt;element&gt;.</summary>
		// Token: 0x0400182B RID: 6187
		Element,
		/// <summary>An attribute, such as id='123'.</summary>
		// Token: 0x0400182C RID: 6188
		Attribute,
		/// <summary>A namespace, such as xmlns="namespace".</summary>
		// Token: 0x0400182D RID: 6189
		Namespace,
		/// <summary>The text content of a node. Equivalent to the Document Object Model (DOM) Text and CDATA node types. Contains at least one character.</summary>
		// Token: 0x0400182E RID: 6190
		Text,
		/// <summary>A node with white space characters and xml:space set to preserve.</summary>
		// Token: 0x0400182F RID: 6191
		SignificantWhitespace,
		/// <summary>A node with only white space characters and no significant white space. White space characters are #x20, #x9, #xD, or #xA.</summary>
		// Token: 0x04001830 RID: 6192
		Whitespace,
		/// <summary>A processing instruction, such as &lt;?pi test?&gt;. This does not include XML declarations, which are not visible to the <see cref="T:System.Xml.XPath.XPathNavigator" /> class. </summary>
		// Token: 0x04001831 RID: 6193
		ProcessingInstruction,
		/// <summary>A comment, such as &lt;!-- my comment --&gt;</summary>
		// Token: 0x04001832 RID: 6194
		Comment,
		/// <summary>Any of the <see cref="T:System.Xml.XPath.XPathNodeType" /> node types.</summary>
		// Token: 0x04001833 RID: 6195
		All
	}
}
