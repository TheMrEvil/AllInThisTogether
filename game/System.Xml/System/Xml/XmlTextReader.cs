using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, non-cached, forward-only access to XML data.Starting with the .NET Framework 2.0, we recommend that you use the <see cref="T:System.Xml.XmlReader" /> class instead.</summary>
	// Token: 0x020000BB RID: 187
	[EditorBrowsable(EditorBrowsableState.Never)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class XmlTextReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		/// <summary>Initializes a new instance of the <see langword="XmlTextReader" />.</summary>
		// Token: 0x06000744 RID: 1860 RVA: 0x000268C5 File Offset: 0x00024AC5
		protected XmlTextReader()
		{
			this.impl = new XmlTextReaderImpl();
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="nt">The <see langword="XmlNameTable" /> to use. </param>
		// Token: 0x06000745 RID: 1861 RVA: 0x000268E4 File Offset: 0x00024AE4
		protected XmlTextReader(XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream.</summary>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="input" /> is <see langword="null" />. </exception>
		// Token: 0x06000746 RID: 1862 RVA: 0x00026904 File Offset: 0x00024B04
		public XmlTextReader(Stream input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL and stream.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="input" /> is <see langword="null" />. </exception>
		// Token: 0x06000747 RID: 1863 RVA: 0x00026924 File Offset: 0x00024B24
		public XmlTextReader(string url, Stream input)
		{
			this.impl = new XmlTextReaderImpl(url, input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <param name="nt">The <see langword="XmlNameTable" /> to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="input" /> or <paramref name="nt" /> value is <see langword="null" />. </exception>
		// Token: 0x06000748 RID: 1864 RVA: 0x00026945 File Offset: 0x00024B45
		public XmlTextReader(Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL, stream and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. If <paramref name="url" /> is <see langword="null" />, <see langword="BaseURI" /> is set to <see langword="String.Empty" />. </param>
		/// <param name="input">The stream containing the XML data to read. </param>
		/// <param name="nt">The <see langword="XmlNameTable" /> to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="input" /> or <paramref name="nt" /> value is <see langword="null" />. </exception>
		// Token: 0x06000749 RID: 1865 RVA: 0x00026966 File Offset: 0x00024B66
		public XmlTextReader(string url, Stream input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="input">The <see langword="TextReader" /> containing the XML data to read. </param>
		// Token: 0x0600074A RID: 1866 RVA: 0x00026988 File Offset: 0x00024B88
		public XmlTextReader(TextReader input)
		{
			this.impl = new XmlTextReaderImpl(input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL and <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <param name="input">The <see langword="TextReader" /> containing the XML data to read. </param>
		// Token: 0x0600074B RID: 1867 RVA: 0x000269A8 File Offset: 0x00024BA8
		public XmlTextReader(string url, TextReader input)
		{
			this.impl = new XmlTextReaderImpl(url, input);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="input">The <see langword="TextReader" /> containing the XML data to read. </param>
		/// <param name="nt">The <see langword="XmlNameTable" /> to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="nt" /> value is <see langword="null" />. </exception>
		// Token: 0x0600074C RID: 1868 RVA: 0x000269C9 File Offset: 0x00024BC9
		public XmlTextReader(TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified URL, <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL to use for resolving external resources. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. If <paramref name="url" /> is <see langword="null" />, <see langword="BaseURI" /> is set to <see langword="String.Empty" />. </param>
		/// <param name="input">The <see langword="TextReader" /> containing the XML data to read. </param>
		/// <param name="nt">The <see langword="XmlNameTable" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="nt" /> value is <see langword="null" />. </exception>
		// Token: 0x0600074D RID: 1869 RVA: 0x000269EA File Offset: 0x00024BEA
		public XmlTextReader(string url, TextReader input, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, input, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified stream, <see cref="T:System.Xml.XmlNodeType" />, and <see cref="T:System.Xml.XmlParserContext" />.</summary>
		/// <param name="xmlFragment">The stream containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This also determines what the fragment can contain. (See table below.) </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the <paramref name="xmlFragment" /> is to be parsed. This includes the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang, and the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///         <paramref name="fragType" /> is not an Element, Attribute, or Document <see langword="XmlNodeType" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="xmlFragment" /> is <see langword="null" />. </exception>
		// Token: 0x0600074E RID: 1870 RVA: 0x00026A0C File Offset: 0x00024C0C
		public XmlTextReader(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlTextReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified string, <see cref="T:System.Xml.XmlNodeType" />, and <see cref="T:System.Xml.XmlParserContext" />.</summary>
		/// <param name="xmlFragment">The string containing the XML fragment to parse. </param>
		/// <param name="fragType">The <see cref="T:System.Xml.XmlNodeType" /> of the XML fragment. This also determines what the fragment string can contain. (See table below.) </param>
		/// <param name="context">The <see cref="T:System.Xml.XmlParserContext" /> in which the <paramref name="xmlFragment" /> is to be parsed. This includes the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang, and the xml:space scope. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///         <paramref name="fragType" /> is not an <see langword="Element" />, <see langword="Attribute" />, or <see langword="Document" /><see langword="XmlNodeType" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="xmlFragment" /> is <see langword="null" />. </exception>
		// Token: 0x0600074F RID: 1871 RVA: 0x00026A2E File Offset: 0x00024C2E
		public XmlTextReader(string xmlFragment, XmlNodeType fragType, XmlParserContext context)
		{
			this.impl = new XmlTextReaderImpl(xmlFragment, fragType, context);
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified file.</summary>
		/// <param name="url">The URL for the file containing the XML data. The <see cref="P:System.Xml.XmlTextReader.BaseURI" /> is set to this value. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Part of the filename or directory cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="url" /> is an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">The remote filename cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="url" /> is not a valid URI.</exception>
		// Token: 0x06000750 RID: 1872 RVA: 0x00026A50 File Offset: 0x00024C50
		public XmlTextReader(string url)
		{
			this.impl = new XmlTextReaderImpl(url, new NameTable());
			this.impl.OuterReader = this;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlTextReader" /> class with the specified file and <see cref="T:System.Xml.XmlNameTable" />.</summary>
		/// <param name="url">The URL for the file containing the XML data to read. </param>
		/// <param name="nt">The <see langword="XmlNameTable" /> to use. </param>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="nt" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file cannot be found.</exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">Part of the filename or directory cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <paramref name="url" /> is an empty string.</exception>
		/// <exception cref="T:System.Net.WebException">The remote filename cannot be resolved.-or-An error occurred while processing the request.</exception>
		/// <exception cref="T:System.UriFormatException">
		///         <paramref name="url" /> is not a valid URI.</exception>
		// Token: 0x06000751 RID: 1873 RVA: 0x00026A75 File Offset: 0x00024C75
		public XmlTextReader(string url, XmlNameTable nt)
		{
			this.impl = new XmlTextReaderImpl(url, nt);
			this.impl.OuterReader = this;
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlNodeType" /> values representing the type of the current node.</returns>
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x00026A96 File Offset: 0x00024C96
		public override XmlNodeType NodeType
		{
			get
			{
				return this.impl.NodeType;
			}
		}

		/// <summary>Gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, <see langword="Name" /> is <see langword="bk:book" /> for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlTextReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node Type Name 
		///             <see langword="Attribute" />
		///           The name of the attribute. 
		///             <see langword="DocumentType" />
		///           The document type name. 
		///             <see langword="Element" />
		///           The tag name. 
		///             <see langword="EntityReference" />
		///           The name of the entity referenced. 
		///             <see langword="ProcessingInstruction" />
		///           The target of the processing instruction. 
		///             <see langword="XmlDeclaration" />
		///           The literal string <see langword="xml" />. </returns>
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00026AA3 File Offset: 0x00024CA3
		public override string Name
		{
			get
			{
				return this.impl.Name;
			}
		}

		/// <summary>Gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, <see langword="LocalName" /> is <see langword="book" /> for the element &lt;bk:book&gt;.For node types that do not have a name (like <see langword="Text" />, <see langword="Comment" />, and so on), this property returns <see langword="String.Empty" />.</returns>
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00026AB0 File Offset: 0x00024CB0
		public override string LocalName
		{
			get
			{
				return this.impl.LocalName;
			}
		}

		/// <summary>Gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00026ABD File Offset: 0x00024CBD
		public override string NamespaceURI
		{
			get
			{
				return this.impl.NamespaceURI;
			}
		}

		/// <summary>Gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00026ACA File Offset: 0x00024CCA
		public override string Prefix
		{
			get
			{
				return this.impl.Prefix;
			}
		}

		/// <summary>Gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlTextReader.Value" /> other than <see langword="String.Empty" />.</summary>
		/// <returns>
		///     <see langword="true" /> if the node on which the reader is currently positioned can have a <see langword="Value" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00026AD7 File Offset: 0x00024CD7
		public override bool HasValue
		{
			get
			{
				return this.impl.HasValue;
			}
		}

		/// <summary>Gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlTextReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return <see langword="String.Empty" />.Node Type Value 
		///             <see langword="Attribute" />
		///           The value of the attribute. 
		///             <see langword="CDATA" />
		///           The content of the CDATA section. 
		///             <see langword="Comment" />
		///           The content of the comment. 
		///             <see langword="DocumentType" />
		///           The internal subset. 
		///             <see langword="ProcessingInstruction" />
		///           The entire content, excluding the target. 
		///             <see langword="SignificantWhitespace" />
		///           The white space within an <see langword="xml:space" />= 'preserve' scope. 
		///             <see langword="Text" />
		///           The content of the text node. 
		///             <see langword="Whitespace" />
		///           The white space between markup. 
		///             <see langword="XmlDeclaration" />
		///           The content of the declaration. </returns>
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00026AE4 File Offset: 0x00024CE4
		public override string Value
		{
			get
			{
				return this.impl.Value;
			}
		}

		/// <summary>Gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x00026AF1 File Offset: 0x00024CF1
		public override int Depth
		{
			get
			{
				return this.impl.Depth;
			}
		}

		/// <summary>Gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00026AFE File Offset: 0x00024CFE
		public override string BaseURI
		{
			get
			{
				return this.impl.BaseURI;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>
		///     <see langword="true" /> if the current node is an element (<see cref="P:System.Xml.XmlTextReader.NodeType" /> equals <see langword="XmlNodeType.Element" />) that ends with /&gt;; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00026B0B File Offset: 0x00024D0B
		public override bool IsEmptyElement
		{
			get
			{
				return this.impl.IsEmptyElement;
			}
		}

		/// <summary>Gets a value indicating whether the current node is an attribute that was generated from the default value defined in the DTD or schema.</summary>
		/// <returns>This property always returns <see langword="false" />. (<see cref="T:System.Xml.XmlTextReader" /> does not expand default attributes.) </returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00026B18 File Offset: 0x00024D18
		public override bool IsDefault
		{
			get
			{
				return this.impl.IsDefault;
			}
		}

		/// <summary>Gets the quotation mark character used to enclose the value of an attribute node.</summary>
		/// <returns>The quotation mark character (" or ') used to enclose the value of an attribute node.</returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00026B25 File Offset: 0x00024D25
		public override char QuoteChar
		{
			get
			{
				return this.impl.QuoteChar;
			}
		}

		/// <summary>Gets the current <see langword="xml:space" /> scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no <see langword="xml:space" /> scope exists, this property defaults to <see langword="XmlSpace.None" />.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00026B32 File Offset: 0x00024D32
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.impl.XmlSpace;
			}
		}

		/// <summary>Gets the current <see langword="xml:lang" /> scope.</summary>
		/// <returns>The current <see langword="xml:lang" /> scope.</returns>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00026B3F File Offset: 0x00024D3F
		public override string XmlLang
		{
			get
			{
				return this.impl.XmlLang;
			}
		}

		/// <summary>Gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node.</returns>
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00026B4C File Offset: 0x00024D4C
		public override int AttributeCount
		{
			get
			{
				return this.impl.AttributeCount;
			}
		}

		/// <summary>Gets the value of the attribute with the specified name.</summary>
		/// <param name="name">The qualified name of the attribute. </param>
		/// <returns>The value of the specified attribute. If the attribute is not found, <see langword="null" /> is returned.</returns>
		// Token: 0x06000761 RID: 1889 RVA: 0x00026B59 File Offset: 0x00024D59
		public override string GetAttribute(string name)
		{
			return this.impl.GetAttribute(name);
		}

		/// <summary>Gets the value of the attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <returns>The value of the specified attribute. If the attribute is not found, <see langword="null" /> is returned. This method does not move the reader.</returns>
		// Token: 0x06000762 RID: 1890 RVA: 0x00026B67 File Offset: 0x00024D67
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.impl.GetAttribute(localName, namespaceURI);
		}

		/// <summary>Gets the value of the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.) </param>
		/// <returns>The value of the specified attribute.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlTextReader.AttributeCount" />. </exception>
		// Token: 0x06000763 RID: 1891 RVA: 0x00026B76 File Offset: 0x00024D76
		public override string GetAttribute(int i)
		{
			return this.impl.GetAttribute(i);
		}

		/// <summary>Moves to the attribute with the specified name.</summary>
		/// <param name="name">The qualified name of the attribute. </param>
		/// <returns>
		///     <see langword="true" /> if the attribute is found; otherwise, <see langword="false" />. If <see langword="false" />, the reader's position does not change.</returns>
		// Token: 0x06000764 RID: 1892 RVA: 0x00026B84 File Offset: 0x00024D84
		public override bool MoveToAttribute(string name)
		{
			return this.impl.MoveToAttribute(name);
		}

		/// <summary>Moves to the attribute with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the attribute. </param>
		/// <param name="namespaceURI">The namespace URI of the attribute. </param>
		/// <returns>
		///     <see langword="true" /> if the attribute is found; otherwise, <see langword="false" />. If <see langword="false" />, the reader's position does not change.</returns>
		// Token: 0x06000765 RID: 1893 RVA: 0x00026B92 File Offset: 0x00024D92
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.impl.MoveToAttribute(localName, namespaceURI);
		}

		/// <summary>Moves to the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="i" /> parameter is less than 0 or greater than or equal to <see cref="P:System.Xml.XmlReader.AttributeCount" />. </exception>
		// Token: 0x06000766 RID: 1894 RVA: 0x00026BA1 File Offset: 0x00024DA1
		public override void MoveToAttribute(int i)
		{
			this.impl.MoveToAttribute(i);
		}

		/// <summary>Moves to the first attribute.</summary>
		/// <returns>
		///     <see langword="true" /> if an attribute exists (the reader moves to the first attribute); otherwise, <see langword="false" /> (the position of the reader does not change).</returns>
		// Token: 0x06000767 RID: 1895 RVA: 0x00026BAF File Offset: 0x00024DAF
		public override bool MoveToFirstAttribute()
		{
			return this.impl.MoveToFirstAttribute();
		}

		/// <summary>Moves to the next attribute.</summary>
		/// <returns>
		///     <see langword="true" /> if there is a next attribute; <see langword="false" /> if there are no more attributes.</returns>
		// Token: 0x06000768 RID: 1896 RVA: 0x00026BBC File Offset: 0x00024DBC
		public override bool MoveToNextAttribute()
		{
			return this.impl.MoveToNextAttribute();
		}

		/// <summary>Moves to the element that contains the current attribute node.</summary>
		/// <returns>
		///     <see langword="true" /> if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); <see langword="false" /> if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		// Token: 0x06000769 RID: 1897 RVA: 0x00026BC9 File Offset: 0x00024DC9
		public override bool MoveToElement()
		{
			return this.impl.MoveToElement();
		}

		/// <summary>Parses the attribute value into one or more <see langword="Text" />, <see langword="EntityReference" />, or <see langword="EndEntity" /> nodes.</summary>
		/// <returns>
		///     <see langword="true" /> if there are nodes to return.
		///     <see langword="false" /> if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns <see langword="true" /> with a single node with a value of <see langword="String.Empty" />.</returns>
		// Token: 0x0600076A RID: 1898 RVA: 0x00026BD6 File Offset: 0x00024DD6
		public override bool ReadAttributeValue()
		{
			return this.impl.ReadAttributeValue();
		}

		/// <summary>Reads the next node from the stream.</summary>
		/// <returns>
		///     <see langword="true" /> if the next node was read successfully; <see langword="false" /> if there are no more nodes to read.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML. </exception>
		// Token: 0x0600076B RID: 1899 RVA: 0x00026BE3 File Offset: 0x00024DE3
		public override bool Read()
		{
			return this.impl.Read();
		}

		/// <summary>Gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>
		///     <see langword="true" /> if the reader is positioned at the end of the stream; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00026BF0 File Offset: 0x00024DF0
		public override bool EOF
		{
			get
			{
				return this.impl.EOF;
			}
		}

		/// <summary>Changes the <see cref="P:System.Xml.XmlReader.ReadState" /> to <see langword="Closed" />.</summary>
		// Token: 0x0600076D RID: 1901 RVA: 0x00026BFD File Offset: 0x00024DFD
		public override void Close()
		{
			this.impl.Close();
		}

		/// <summary>Gets the state of the reader.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ReadState" /> values.</returns>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x00026C0A File Offset: 0x00024E0A
		public override ReadState ReadState
		{
			get
			{
				return this.impl.ReadState;
			}
		}

		/// <summary>Skips the children of the current node.</summary>
		// Token: 0x0600076F RID: 1903 RVA: 0x00026C17 File Offset: 0x00024E17
		public override void Skip()
		{
			this.impl.Skip();
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The <see langword="XmlNameTable" /> enabling you to get the atomized version of a string within the node.</returns>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x00026C24 File Offset: 0x00024E24
		public override XmlNameTable NameTable
		{
			get
			{
				return this.impl.NameTable;
			}
		}

		/// <summary>Resolves a namespace prefix in the current element's scope.</summary>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. This string does not have to be atomized. </param>
		/// <returns>The namespace URI to which the prefix maps or <see langword="null" /> if no matching prefix is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Xml.XmlTextReader.Namespaces" /> property is set to <see langword="true" /> and the <paramref name="prefix" /> value is <see langword="null" />. </exception>
		// Token: 0x06000771 RID: 1905 RVA: 0x00026C34 File Offset: 0x00024E34
		public override string LookupNamespace(string prefix)
		{
			string text = this.impl.LookupNamespace(prefix);
			if (text != null && text.Length == 0)
			{
				text = null;
			}
			return text;
		}

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>
		///     <see langword="true" /> if the reader can parse and resolve entities; otherwise, <see langword="false" />. The <see langword="XmlTextReader" /> class always returns <see langword="true" />.</returns>
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		/// <summary>Resolves the entity reference for <see langword="EntityReference" /> nodes.</summary>
		// Token: 0x06000773 RID: 1907 RVA: 0x00026C5C File Offset: 0x00024E5C
		public override void ResolveEntity()
		{
			this.impl.ResolveEntity();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlTextReader" /> implements the binary content read methods.</summary>
		/// <returns>
		///     <see langword="true" /> if the binary content read methods are implemented; otherwise <see langword="false" />. The <see cref="T:System.Xml.XmlTextReader" /> class always returns <see langword="true" />.</returns>
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		/// <summary>Reads the content and returns the <see langword="Base64" /> decoded binary bytes.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <see cref="M:System.Xml.XmlTextReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" />  is not supported in the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		// Token: 0x06000775 RID: 1909 RVA: 0x00026C69 File Offset: 0x00024E69
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBase64(buffer, index, count);
		}

		/// <summary>Reads the element and decodes the Base64 content.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlTextReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000776 RID: 1910 RVA: 0x00026C79 File Offset: 0x00024E79
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBase64(buffer, index, count);
		}

		/// <summary>Reads the content and returns the <see langword="BinHex" /> decoded binary bytes.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <see cref="M:System.Xml.XmlTextReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" />  is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlTextReader" /> implementation does not support this method.</exception>
		// Token: 0x06000777 RID: 1911 RVA: 0x00026C89 File Offset: 0x00024E89
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadContentAsBinHex(buffer, index, count);
		}

		/// <summary>Reads the element and decodes the <see langword="BinHex" /> content.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not an element node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The element contains mixed-content.</exception>
		/// <exception cref="T:System.FormatException">The content cannot be converted to the requested type.</exception>
		// Token: 0x06000778 RID: 1912 RVA: 0x00026C99 File Offset: 0x00024E99
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			return this.impl.ReadElementContentAsBinHex(buffer, index, count);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlTextReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XmlTextReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method; otherwise <see langword="false" />. The <see cref="T:System.Xml.XmlTextReader" /> class always returns <see langword="false" />.</returns>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public override bool CanReadValueChunk
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reads the contents of an element or a text node as a string.</summary>
		/// <returns>The contents of the element or text node. This can be an empty string if the reader is positioned on something other than an element or text node, or if there is no more text content to return in the current context.
		///     <see langword="Note:" /> The text node can be either an element or an attribute text node.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML. </exception>
		/// <exception cref="T:System.InvalidOperationException">An invalid operation was attempted. </exception>
		// Token: 0x0600077A RID: 1914 RVA: 0x00026CA9 File Offset: 0x00024EA9
		public override string ReadString()
		{
			this.impl.MoveOffEntityReference();
			return base.ReadString();
		}

		/// <summary>Gets a value indicating whether the class can return line information.</summary>
		/// <returns>
		///     <see langword="true" /> if the class can return line information; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600077B RID: 1915 RVA: 0x0001222F File Offset: 0x0001042F
		public bool HasLineInfo()
		{
			return true;
		}

		/// <summary>Gets the current line number.</summary>
		/// <returns>The current line number.</returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00026CBC File Offset: 0x00024EBC
		public int LineNumber
		{
			get
			{
				return this.impl.LineNumber;
			}
		}

		/// <summary>Gets the current line position.</summary>
		/// <returns>The current line position.</returns>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00026CC9 File Offset: 0x00024EC9
		public int LinePosition
		{
			get
			{
				return this.impl.LinePosition;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.GetNamespacesInScope(System.Xml.XmlNamespaceScope)" />.</summary>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that contains the current in-scope namespaces.</returns>
		// Token: 0x0600077E RID: 1918 RVA: 0x00026CD6 File Offset: 0x00024ED6
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupNamespace(System.String)" />.</summary>
		/// <param name="prefix">The prefix whose namespace URI you wish to find.</param>
		/// <returns>The namespace URI that is mapped to the prefix; <see langword="null" /> if the prefix is not mapped to a namespace URI.</returns>
		// Token: 0x0600077F RID: 1919 RVA: 0x00026CE4 File Offset: 0x00024EE4
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.impl.LookupNamespace(prefix);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.IXmlNamespaceResolver.LookupPrefix(System.String)" />.</summary>
		/// <param name="namespaceName">The namespace URI whose prefix you wish to find.</param>
		/// <returns>The prefix that is mapped to the namespace URI; <see langword="null" /> if the namespace URI is not mapped to a prefix.</returns>
		// Token: 0x06000780 RID: 1920 RVA: 0x00026CF2 File Offset: 0x00024EF2
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.impl.LookupPrefix(namespaceName);
		}

		/// <summary>Gets a collection that contains all namespaces currently in-scope.</summary>
		/// <param name="scope">An <see cref="T:System.Xml.XmlNamespaceScope" /> value that specifies the type of namespace nodes to return.</param>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> object that contains all the current in-scope namespaces. If the reader is not positioned on an element, an empty dictionary (no namespaces) is returned.</returns>
		// Token: 0x06000781 RID: 1921 RVA: 0x00026CD6 File Offset: 0x00024ED6
		public IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.impl.GetNamespacesInScope(scope);
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>
		///     <see langword="true" /> to do namespace support; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property after a read operation has occurred (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is not <see langword="ReadState.Initial" />). </exception>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x00026D00 File Offset: 0x00024F00
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x00026D0D File Offset: 0x00024F0D
		public bool Namespaces
		{
			get
			{
				return this.impl.Namespaces;
			}
			set
			{
				this.impl.Namespaces = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to normalize white space and attribute values.</summary>
		/// <returns>
		///     <see langword="true" /> to normalize; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">Setting this property when the reader is closed (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is <see langword="ReadState.Closed" />). </exception>
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00026D1B File Offset: 0x00024F1B
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x00026D28 File Offset: 0x00024F28
		public bool Normalization
		{
			get
			{
				return this.impl.Normalization;
			}
			set
			{
				this.impl.Normalization = value;
			}
		}

		/// <summary>Gets the encoding of the document.</summary>
		/// <returns>The encoding value. If no encoding attribute exists, and there is no byte-order mark, this defaults to UTF-8.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00026D36 File Offset: 0x00024F36
		public Encoding Encoding
		{
			get
			{
				return this.impl.Encoding;
			}
		}

		/// <summary>Gets or sets a value that specifies how white space is handled.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WhitespaceHandling" /> values. The default is <see langword="WhitespaceHandling.All" /> (returns <see langword="Whitespace" /> and <see langword="SignificantWhitespace" /> nodes).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">Invalid value specified. </exception>
		/// <exception cref="T:System.InvalidOperationException">Setting this property when the reader is closed (<see cref="P:System.Xml.XmlTextReader.ReadState" /> is <see langword="ReadState.Closed" />). </exception>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x00026D43 File Offset: 0x00024F43
		// (set) Token: 0x06000788 RID: 1928 RVA: 0x00026D50 File Offset: 0x00024F50
		public WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.impl.WhitespaceHandling;
			}
			set
			{
				this.impl.WhitespaceHandling = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to allow DTD processing. This property is obsolete. Use <see cref="P:System.Xml.XmlTextReader.DtdProcessing" /> instead.</summary>
		/// <returns>
		///     <see langword="true" /> to disallow DTD processing; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x00026D5E File Offset: 0x00024F5E
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x00026D6E File Offset: 0x00024F6E
		[Obsolete("Use DtdProcessing property instead.")]
		public bool ProhibitDtd
		{
			get
			{
				return this.impl.DtdProcessing == DtdProcessing.Prohibit;
			}
			set
			{
				this.impl.DtdProcessing = (value ? DtdProcessing.Prohibit : DtdProcessing.Parse);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.DtdProcessing" /> enumeration.</summary>
		/// <returns>The <see cref="T:System.Xml.DtdProcessing" /> enumeration.</returns>
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x00026D82 File Offset: 0x00024F82
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x00026D8F File Offset: 0x00024F8F
		public DtdProcessing DtdProcessing
		{
			get
			{
				return this.impl.DtdProcessing;
			}
			set
			{
				this.impl.DtdProcessing = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how the reader handles entities.</summary>
		/// <returns>One of the <see cref="T:System.Xml.EntityHandling" /> values. If no <see langword="EntityHandling" /> is specified, it defaults to <see langword="EntityHandling.ExpandCharEntities" />.</returns>
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00026D9D File Offset: 0x00024F9D
		// (set) Token: 0x0600078E RID: 1934 RVA: 0x00026DAA File Offset: 0x00024FAA
		public EntityHandling EntityHandling
		{
			get
			{
				return this.impl.EntityHandling;
			}
			set
			{
				this.impl.EntityHandling = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used for resolving DTD references.</summary>
		/// <returns>The <see langword="XmlResolver" /> to use. If set to <see langword="null" />, external resources are not resolved.In version 1.1 of the .NET Framework, the caller must be fully trusted in order to specify an <see langword="XmlResolver" />.</returns>
		// Token: 0x1700011A RID: 282
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00026DB8 File Offset: 0x00024FB8
		public XmlResolver XmlResolver
		{
			set
			{
				this.impl.XmlResolver = value;
			}
		}

		/// <summary>Resets the state of the reader to ReadState.Initial.</summary>
		/// <exception cref="T:System.InvalidOperationException">Calling <see langword="ResetState" /> if the reader was constructed using an <see cref="T:System.Xml.XmlParserContext" />. </exception>
		/// <exception cref="T:System.Xml.XmlException">Documents in a single stream do not share the same encoding.</exception>
		// Token: 0x06000790 RID: 1936 RVA: 0x00026DC6 File Offset: 0x00024FC6
		public void ResetState()
		{
			this.impl.ResetState();
		}

		/// <summary>Gets the remainder of the buffered XML.</summary>
		/// <returns>A <see cref="T:System.IO.TextReader" /> containing the remainder of the buffered XML.</returns>
		// Token: 0x06000791 RID: 1937 RVA: 0x00026DD3 File Offset: 0x00024FD3
		public TextReader GetRemainder()
		{
			return this.impl.GetRemainder();
		}

		/// <summary>Reads the text contents of an element into a character buffer. This method is designed to read large streams of embedded text by calling it successively.</summary>
		/// <param name="buffer">The array of characters that serves as the buffer to which the text contents are written. </param>
		/// <param name="index">The position within <paramref name="buffer" /> where the method can begin writing text contents. </param>
		/// <param name="count">The number of characters to write into <paramref name="buffer" />. </param>
		/// <returns>The number of characters read. This can be <see langword="0" /> if the reader is not positioned on an element or if there is no more text content to return in the current context.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="count" /> is greater than the space specified in the <paramref name="buffer" /> (buffer size - <paramref name="index" />). </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" />
		///         <see langword="&lt; 0" /> or <paramref name="count" /><see langword="&lt; 0" />. </exception>
		// Token: 0x06000792 RID: 1938 RVA: 0x00026DE0 File Offset: 0x00024FE0
		public int ReadChars(char[] buffer, int index, int count)
		{
			return this.impl.ReadChars(buffer, index, count);
		}

		/// <summary>Decodes Base64 and returns the decoded binary bytes.</summary>
		/// <param name="array">The array of characters that serves as the buffer to which the text contents are written. </param>
		/// <param name="offset">The zero-based index into the array specifying where the method can begin to write to the buffer. </param>
		/// <param name="len">The number of bytes to write into the buffer. </param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.Xml.XmlException">The Base64 sequence is not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="array" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="offset" /> &lt; 0, or <paramref name="len" /> &lt; 0, or <paramref name="len" /> &gt; <paramref name="array" />.Length- <paramref name="offset" />. </exception>
		// Token: 0x06000793 RID: 1939 RVA: 0x00026DF0 File Offset: 0x00024FF0
		public int ReadBase64(byte[] array, int offset, int len)
		{
			return this.impl.ReadBase64(array, offset, len);
		}

		/// <summary>Decodes <see langword="BinHex" /> and returns the decoded binary bytes.</summary>
		/// <param name="array">The byte array that serves as the buffer to which the decoded binary bytes are written. </param>
		/// <param name="offset">The zero-based index into the array specifying where the method can begin to write to the buffer. </param>
		/// <param name="len">The number of bytes to write into the buffer. </param>
		/// <returns>The number of bytes written to your buffer.</returns>
		/// <exception cref="T:System.Xml.XmlException">The <see langword="BinHex" /> sequence is not valid. </exception>
		/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="array" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="offset" /> &lt; 0, or <paramref name="len" /> &lt; 0, or <paramref name="len" /> &gt; <paramref name="array" />.Length- <paramref name="offset" />. </exception>
		// Token: 0x06000794 RID: 1940 RVA: 0x00026E00 File Offset: 0x00025000
		public int ReadBinHex(byte[] array, int offset, int len)
		{
			return this.impl.ReadBinHex(array, offset, len);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000795 RID: 1941 RVA: 0x00026E10 File Offset: 0x00025010
		internal XmlTextReaderImpl Impl
		{
			get
			{
				return this.impl;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00026E18 File Offset: 0x00025018
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.impl.NamespaceManager;
			}
		}

		// Token: 0x1700011D RID: 285
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00026E25 File Offset: 0x00025025
		internal bool XmlValidatingReaderCompatibilityMode
		{
			set
			{
				this.impl.XmlValidatingReaderCompatibilityMode = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00026E33 File Offset: 0x00025033
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.impl.DtdInfo;
			}
		}

		// Token: 0x04000917 RID: 2327
		private XmlTextReaderImpl impl;
	}
}
