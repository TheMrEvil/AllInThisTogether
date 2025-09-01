using System;

namespace System.Xml
{
	/// <summary>Specifies the type of node.</summary>
	// Token: 0x02000241 RID: 577
	public enum XmlNodeType
	{
		/// <summary>This is returned by the <see cref="T:System.Xml.XmlReader" /> if a <see langword="Read" /> method has not been called.</summary>
		// Token: 0x040012F3 RID: 4851
		None,
		/// <summary>An element (for example, &lt;item&gt; ).</summary>
		// Token: 0x040012F4 RID: 4852
		Element,
		/// <summary>An attribute (for example, id='123' ).</summary>
		// Token: 0x040012F5 RID: 4853
		Attribute,
		/// <summary>The text content of a node.</summary>
		// Token: 0x040012F6 RID: 4854
		Text,
		/// <summary>A CDATA section (for example, &lt;![CDATA[my escaped text]]&gt; ).</summary>
		// Token: 0x040012F7 RID: 4855
		CDATA,
		/// <summary>A reference to an entity (for example, &amp;num; ).</summary>
		// Token: 0x040012F8 RID: 4856
		EntityReference,
		/// <summary>An entity declaration (for example, &lt;!ENTITY...&gt; ).</summary>
		// Token: 0x040012F9 RID: 4857
		Entity,
		/// <summary>A processing instruction (for example, &lt;?pi test?&gt; ).</summary>
		// Token: 0x040012FA RID: 4858
		ProcessingInstruction,
		/// <summary>A comment (for example, &lt;!-- my comment --&gt; ).</summary>
		// Token: 0x040012FB RID: 4859
		Comment,
		/// <summary>A document object that, as the root of the document tree, provides access to the entire XML document.</summary>
		// Token: 0x040012FC RID: 4860
		Document,
		/// <summary>The document type declaration, indicated by the following tag (for example, &lt;!DOCTYPE...&gt; ).</summary>
		// Token: 0x040012FD RID: 4861
		DocumentType,
		/// <summary>A document fragment.</summary>
		// Token: 0x040012FE RID: 4862
		DocumentFragment,
		/// <summary>A notation in the document type declaration (for example, &lt;!NOTATION...&gt; ).</summary>
		// Token: 0x040012FF RID: 4863
		Notation,
		/// <summary>White space between markup.</summary>
		// Token: 0x04001300 RID: 4864
		Whitespace,
		/// <summary>White space between markup in a mixed content model or white space within the xml:space="preserve" scope.</summary>
		// Token: 0x04001301 RID: 4865
		SignificantWhitespace,
		/// <summary>An end element tag (for example, &lt;/item&gt; ).</summary>
		// Token: 0x04001302 RID: 4866
		EndElement,
		/// <summary>Returned when <see langword="XmlReader" /> gets to the end of the entity replacement as a result of a call to <see cref="M:System.Xml.XmlReader.ResolveEntity" />.</summary>
		// Token: 0x04001303 RID: 4867
		EndEntity,
		/// <summary>The XML declaration (for example, &lt;?xml version='1.0'?&gt; ).</summary>
		// Token: 0x04001304 RID: 4868
		XmlDeclaration
	}
}
