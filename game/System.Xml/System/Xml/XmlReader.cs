using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Represents a reader that provides fast, noncached, forward-only access to XML data.To browse the .NET Framework source code for this type, see the Reference Source.</summary>
	// Token: 0x0200009B RID: 155
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	[DebuggerDisplay("{debuggerDisplayProxy}")]
	public abstract class XmlReader : IDisposable
	{
		/// <summary>Gets the <see cref="T:System.Xml.XmlReaderSettings" /> object used to create this <see cref="T:System.Xml.XmlReader" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlReaderSettings" /> object used to create this reader instance. If this reader was not created using the <see cref="Overload:System.Xml.XmlReader.Create" /> method, this property returns <see langword="null" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001DA42 File Offset: 0x0001BC42
		public virtual XmlReaderSettings Settings
		{
			get
			{
				return null;
			}
		}

		/// <summary>When overridden in a derived class, gets the type of the current node.</summary>
		/// <returns>One of the enumeration values that specify the type of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060005CA RID: 1482
		public abstract XmlNodeType NodeType { get; }

		/// <summary>When overridden in a derived class, gets the qualified name of the current node.</summary>
		/// <returns>The qualified name of the current node. For example, <see langword="Name" /> is <see langword="bk:book" /> for the element &lt;bk:book&gt;.The name returned is dependent on the <see cref="P:System.Xml.XmlReader.NodeType" /> of the node. The following node types return the listed values. All other node types return an empty string.Node type Name 
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
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0001E4D6 File Offset: 0x0001C6D6
		public virtual string Name
		{
			get
			{
				if (this.Prefix.Length == 0)
				{
					return this.LocalName;
				}
				return this.NameTable.Add(this.Prefix + ":" + this.LocalName);
			}
		}

		/// <summary>When overridden in a derived class, gets the local name of the current node.</summary>
		/// <returns>The name of the current node with the prefix removed. For example, <see langword="LocalName" /> is <see langword="book" /> for the element &lt;bk:book&gt;.For node types that do not have a name (like <see langword="Text" />, <see langword="Comment" />, and so on), this property returns <see langword="String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060005CC RID: 1484
		public abstract string LocalName { get; }

		/// <summary>When overridden in a derived class, gets the namespace URI (as defined in the W3C Namespace specification) of the node on which the reader is positioned.</summary>
		/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060005CD RID: 1485
		public abstract string NamespaceURI { get; }

		/// <summary>When overridden in a derived class, gets the namespace prefix associated with the current node.</summary>
		/// <returns>The namespace prefix associated with the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060005CE RID: 1486
		public abstract string Prefix { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node can have a <see cref="P:System.Xml.XmlReader.Value" />.</summary>
		/// <returns>
		///     <see langword="true" /> if the node on which the reader is currently positioned can have a <see langword="Value" />; otherwise, <see langword="false" />. If <see langword="false" />, the node has a value of <see langword="String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001E50D File Offset: 0x0001C70D
		public virtual bool HasValue
		{
			get
			{
				return XmlReader.HasValueInternal(this.NodeType);
			}
		}

		/// <summary>When overridden in a derived class, gets the text value of the current node.</summary>
		/// <returns>The value returned depends on the <see cref="P:System.Xml.XmlReader.NodeType" /> of the node. The following table lists node types that have a value to return. All other node types return <see langword="String.Empty" />.Node type Value 
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
		///           The white space between markup in a mixed content model. 
		///             <see langword="Text" />
		///           The content of the text node. 
		///             <see langword="Whitespace" />
		///           The white space between markup. 
		///             <see langword="XmlDeclaration" />
		///           The content of the declaration. </returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060005D0 RID: 1488
		public abstract string Value { get; }

		/// <summary>When overridden in a derived class, gets the depth of the current node in the XML document.</summary>
		/// <returns>The depth of the current node in the XML document.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060005D1 RID: 1489
		public abstract int Depth { get; }

		/// <summary>When overridden in a derived class, gets the base URI of the current node.</summary>
		/// <returns>The base URI of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060005D2 RID: 1490
		public abstract string BaseURI { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node is an empty element (for example, &lt;MyElement/&gt;).</summary>
		/// <returns>
		///     <see langword="true" /> if the current node is an element (<see cref="P:System.Xml.XmlReader.NodeType" /> equals <see langword="XmlNodeType.Element" />) that ends with /&gt;; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060005D3 RID: 1491
		public abstract bool IsEmptyElement { get; }

		/// <summary>When overridden in a derived class, gets a value indicating whether the current node is an attribute that was generated from the default value defined in the DTD or schema.</summary>
		/// <returns>
		///     <see langword="true" /> if the current node is an attribute whose value was generated from the default value defined in the DTD or schema; <see langword="false" /> if the attribute value was explicitly set.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool IsDefault
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, gets the quotation mark character used to enclose the value of an attribute node.</summary>
		/// <returns>The quotation mark character (" or ') used to enclose the value of an attribute node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001E51A File Offset: 0x0001C71A
		public virtual char QuoteChar
		{
			get
			{
				return '"';
			}
		}

		/// <summary>When overridden in a derived class, gets the current <see langword="xml:space" /> scope.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlSpace" /> values. If no <see langword="xml:space" /> scope exists, this property defaults to <see langword="XmlSpace.None" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.None;
			}
		}

		/// <summary>When overridden in a derived class, gets the current <see langword="xml:lang" /> scope.</summary>
		/// <returns>The current <see langword="xml:lang" /> scope.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001E51E File Offset: 0x0001C71E
		public virtual string XmlLang
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets the schema information that has been assigned to the current node as a result of schema validation.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.IXmlSchemaInfo" /> object containing the schema information for the current node. Schema information can be set on elements, attributes, or on text nodes with a non-null <see cref="P:System.Xml.XmlReader.ValueType" /> (typed values).If the current node is not one of the above node types, or if the <see langword="XmlReader" /> instance does not report schema information, this property returns <see langword="null" />.If this property is called from an <see cref="T:System.Xml.XmlTextReader" /> or an <see cref="T:System.Xml.XmlValidatingReader" /> object, this property always returns <see langword="null" />. These <see langword="XmlReader" /> implementations do not expose schema information through the <see langword="SchemaInfo" /> property.If you have to get the post-schema-validation information set (PSVI) for an element, position the reader on the end tag of the element, rather than on the start tag. You get the PSVI through the <see langword="SchemaInfo" /> property of a reader. The validating reader that is created through <see cref="Overload:System.Xml.XmlReader.Create" /> with the <see cref="P:System.Xml.XmlReaderSettings.ValidationType" /> property set to <see cref="F:System.Xml.ValidationType.Schema" /> has complete PSVI for an element only when the reader is positioned on the end tag of an element.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0001E525 File Offset: 0x0001C725
		public virtual IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this as IXmlSchemaInfo;
			}
		}

		/// <summary>Gets The Common Language Runtime (CLR) type for the current node.</summary>
		/// <returns>The CLR type that corresponds to the typed value of the node. The default is <see langword="System.String" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001E52D File Offset: 0x0001C72D
		public virtual Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}

		/// <summary>Reads the text content at the current position as an <see cref="T:System.Object" />.</summary>
		/// <returns>The text content as the most appropriate common language runtime (CLR) object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005DA RID: 1498 RVA: 0x0001E539 File Offset: 0x0001C739
		public virtual object ReadContentAsObject()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsObject");
			}
			return this.InternalReadContentAsString();
		}

		/// <summary>Reads the text content at the current position as a <see langword="Boolean" />.</summary>
		/// <returns>The text content as a <see cref="T:System.Boolean" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005DB RID: 1499 RVA: 0x0001E558 File Offset: 0x0001C758
		public virtual bool ReadContentAsBoolean()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsBoolean");
			}
			bool result;
			try
			{
				result = XmlConvert.ToBoolean(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Boolean", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the text content at the current position as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The text content as a <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005DC RID: 1500 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
		public virtual DateTime ReadContentAsDateTime()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDateTime");
			}
			DateTime result;
			try
			{
				result = XmlConvert.ToDateTime(this.InternalReadContentAsString(), XmlDateTimeSerializationMode.RoundtripKind);
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTime", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the text content at the current position as a <see cref="T:System.DateTimeOffset" /> object.</summary>
		/// <returns>The text content as a <see cref="T:System.DateTimeOffset" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005DD RID: 1501 RVA: 0x0001E610 File Offset: 0x0001C810
		public virtual DateTimeOffset ReadContentAsDateTimeOffset()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDateTimeOffset");
			}
			DateTimeOffset result;
			try
			{
				result = XmlConvert.ToDateTimeOffset(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "DateTimeOffset", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the text content at the current position as a double-precision floating-point number.</summary>
		/// <returns>The text content as a double-precision floating-point number.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005DE RID: 1502 RVA: 0x0001E66C File Offset: 0x0001C86C
		public virtual double ReadContentAsDouble()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDouble");
			}
			double result;
			try
			{
				result = XmlConvert.ToDouble(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Double", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the text content at the current position as a single-precision floating point number.</summary>
		/// <returns>The text content at the current position as a single-precision floating point number.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005DF RID: 1503 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public virtual float ReadContentAsFloat()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsFloat");
			}
			float result;
			try
			{
				result = XmlConvert.ToSingle(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Float", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the text content at the current position as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The text content at the current position as a <see cref="T:System.Decimal" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E0 RID: 1504 RVA: 0x0001E724 File Offset: 0x0001C924
		public virtual decimal ReadContentAsDecimal()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsDecimal");
			}
			decimal result;
			try
			{
				result = XmlConvert.ToDecimal(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Decimal", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the text content at the current position as a 32-bit signed integer.</summary>
		/// <returns>The text content as a 32-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E1 RID: 1505 RVA: 0x0001E780 File Offset: 0x0001C980
		public virtual int ReadContentAsInt()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsInt");
			}
			int result;
			try
			{
				result = XmlConvert.ToInt32(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Int", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the text content at the current position as a 64-bit signed integer.</summary>
		/// <returns>The text content as a 64-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E2 RID: 1506 RVA: 0x0001E7DC File Offset: 0x0001C9DC
		public virtual long ReadContentAsLong()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsLong");
			}
			long result;
			try
			{
				result = XmlConvert.ToInt64(this.InternalReadContentAsString());
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", "Long", innerException, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the text content at the current position as a <see cref="T:System.String" /> object.</summary>
		/// <returns>The text content as a <see cref="T:System.String" /> object.</returns>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.FormatException">The string format is not valid.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E3 RID: 1507 RVA: 0x0001E838 File Offset: 0x0001CA38
		public virtual string ReadContentAsString()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsString");
			}
			return this.InternalReadContentAsString();
		}

		/// <summary>Reads the content as an object of the type specified.</summary>
		/// <param name="returnType">The type of the value to be returned.
		///       Note   With the release of the .NET Framework 3.5, the value of the <paramref name="returnType" /> parameter can now be the <see cref="T:System.DateTimeOffset" /> type.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion. For example, this can be used when converting an <see cref="T:System.Xml.XmlQualifiedName" /> object to an xs:string.This value can be <see langword="null" />.</param>
		/// <returns>The concatenated text content or attribute value converted to the requested type.</returns>
		/// <exception cref="T:System.FormatException">The content is not in the correct format for the target type.</exception>
		/// <exception cref="T:System.InvalidCastException">The attempted cast is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="returnType" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current node is not a supported node type. See the table below for details.</exception>
		/// <exception cref="T:System.OverflowException">Read <see langword="Decimal.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E4 RID: 1508 RVA: 0x0001E854 File Offset: 0x0001CA54
		public virtual object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAs");
			}
			string text = this.InternalReadContentAsString();
			if (returnType == typeof(string))
			{
				return text;
			}
			object result;
			try
			{
				result = XmlUntypedConverter.Untyped.ChangeType(text, returnType, (namespaceResolver == null) ? (this as IXmlNamespaceResolver) : namespaceResolver);
			}
			catch (FormatException innerException)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), innerException, this as IXmlLineInfo);
			}
			catch (InvalidCastException innerException2)
			{
				throw new XmlException("Content cannot be converted to the type {0}.", returnType.ToString(), innerException2, this as IXmlLineInfo);
			}
			return result;
		}

		/// <summary>Reads the current element and returns the contents as an <see cref="T:System.Object" />.</summary>
		/// <returns>A boxed common language runtime (CLR) object of the most appropriate type. The <see cref="P:System.Xml.XmlReader.ValueType" /> property determines the appropriate CLR type. If the content is typed as a list type, this method returns an array of boxed objects of the appropriate type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001E8FC File Offset: 0x0001CAFC
		public virtual object ReadElementContentAsObject()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsObject"))
			{
				object result = this.ReadContentAsObject();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return string.Empty;
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as an <see cref="T:System.Object" />.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>A boxed common language runtime (CLR) object of the most appropriate type. The <see cref="P:System.Xml.XmlReader.ValueType" /> property determines the appropriate CLR type. If the content is typed as a list type, this method returns an array of boxed objects of the appropriate type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E6 RID: 1510 RVA: 0x0001E91D File Offset: 0x0001CB1D
		public virtual object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsObject();
		}

		/// <summary>Reads the current element and returns the contents as a <see cref="T:System.Boolean" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.Boolean" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.Boolean" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E7 RID: 1511 RVA: 0x0001E92D File Offset: 0x0001CB2D
		public virtual bool ReadElementContentAsBoolean()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsBoolean"))
			{
				bool result = this.ReadContentAsBoolean();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToBoolean(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a <see cref="T:System.Boolean" /> object.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element content as a <see cref="T:System.Boolean" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E8 RID: 1512 RVA: 0x0001E953 File Offset: 0x0001CB53
		public virtual bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsBoolean();
		}

		/// <summary>Reads the current element and returns the contents as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.DateTime" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005E9 RID: 1513 RVA: 0x0001E963 File Offset: 0x0001CB63
		public virtual DateTime ReadElementContentAsDateTime()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDateTime"))
			{
				DateTime result = this.ReadContentAsDateTime();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToDateTime(string.Empty, XmlDateTimeSerializationMode.RoundtripKind);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element contents as a <see cref="T:System.DateTime" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005EA RID: 1514 RVA: 0x0001E98A File Offset: 0x0001CB8A
		public virtual DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsDateTime();
		}

		/// <summary>Reads the current element and returns the contents as a double-precision floating-point number.</summary>
		/// <returns>The element content as a double-precision floating-point number.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a double-precision floating-point number.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005EB RID: 1515 RVA: 0x0001E99A File Offset: 0x0001CB9A
		public virtual double ReadElementContentAsDouble()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDouble"))
			{
				double result = this.ReadContentAsDouble();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToDouble(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a double-precision floating-point number.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element content as a double-precision floating-point number.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005EC RID: 1516 RVA: 0x0001E9C0 File Offset: 0x0001CBC0
		public virtual double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsDouble();
		}

		/// <summary>Reads the current element and returns the contents as single-precision floating-point number.</summary>
		/// <returns>The element content as a single-precision floating point number.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a single-precision floating-point number.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005ED RID: 1517 RVA: 0x0001E9D0 File Offset: 0x0001CBD0
		public virtual float ReadElementContentAsFloat()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsFloat"))
			{
				float result = this.ReadContentAsFloat();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToSingle(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a single-precision floating-point number.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element content as a single-precision floating point number.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a single-precision floating-point number.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005EE RID: 1518 RVA: 0x0001E9F6 File Offset: 0x0001CBF6
		public virtual float ReadElementContentAsFloat(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsFloat();
		}

		/// <summary>Reads the current element and returns the contents as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.Decimal" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.Decimal" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005EF RID: 1519 RVA: 0x0001EA06 File Offset: 0x0001CC06
		public virtual decimal ReadElementContentAsDecimal()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsDecimal"))
			{
				decimal result = this.ReadContentAsDecimal();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToDecimal(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a <see cref="T:System.Decimal" /> object.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element content as a <see cref="T:System.Decimal" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.Decimal" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F0 RID: 1520 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		public virtual decimal ReadElementContentAsDecimal(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsDecimal();
		}

		/// <summary>Reads the current element and returns the contents as a 32-bit signed integer.</summary>
		/// <returns>The element content as a 32-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a 32-bit signed integer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F1 RID: 1521 RVA: 0x0001EA3C File Offset: 0x0001CC3C
		public virtual int ReadElementContentAsInt()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsInt"))
			{
				int result = this.ReadContentAsInt();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToInt32(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a 32-bit signed integer.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element content as a 32-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a 32-bit signed integer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F2 RID: 1522 RVA: 0x0001EA62 File Offset: 0x0001CC62
		public virtual int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsInt();
		}

		/// <summary>Reads the current element and returns the contents as a 64-bit signed integer.</summary>
		/// <returns>The element content as a 64-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a 64-bit signed integer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F3 RID: 1523 RVA: 0x0001EA72 File Offset: 0x0001CC72
		public virtual long ReadElementContentAsLong()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsLong"))
			{
				long result = this.ReadContentAsLong();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return XmlConvert.ToInt64(string.Empty);
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a 64-bit signed integer.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element content as a 64-bit signed integer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a 64-bit signed integer.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F4 RID: 1524 RVA: 0x0001EA98 File Offset: 0x0001CC98
		public virtual long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsLong();
		}

		/// <summary>Reads the current element and returns the contents as a <see cref="T:System.String" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.String" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.String" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F5 RID: 1525 RVA: 0x0001EAA8 File Offset: 0x0001CCA8
		public virtual string ReadElementContentAsString()
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAsString"))
			{
				string result = this.ReadContentAsString();
				this.FinishReadElementContentAsXxx();
				return result;
			}
			return string.Empty;
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the current element and returns the contents as a <see cref="T:System.String" /> object.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element content as a <see cref="T:System.String" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to a <see cref="T:System.String" /> object.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F6 RID: 1526 RVA: 0x0001EAC9 File Offset: 0x0001CCC9
		public virtual string ReadElementContentAsString(string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAsString();
		}

		/// <summary>Reads the element content as the requested type.</summary>
		/// <param name="returnType">The type of the value to be returned.
		///       Note   With the release of the .NET Framework 3.5, the value of the <paramref name="returnType" /> parameter can now be the <see cref="T:System.DateTimeOffset" /> type.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion.</param>
		/// <returns>The element content converted to the requested typed object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.OverflowException">Read <see langword="Decimal.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F7 RID: 1527 RVA: 0x0001EADC File Offset: 0x0001CCDC
		public virtual object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			if (this.SetupReadElementContentAsXxx("ReadElementContentAs"))
			{
				object result = this.ReadContentAs(returnType, namespaceResolver);
				this.FinishReadElementContentAsXxx();
				return result;
			}
			if (!(returnType == typeof(string)))
			{
				return XmlUntypedConverter.Untyped.ChangeType(string.Empty, returnType, namespaceResolver);
			}
			return string.Empty;
		}

		/// <summary>Checks that the specified local name and namespace URI matches that of the current element, then reads the element content as the requested type.</summary>
		/// <param name="returnType">The type of the value to be returned.
		///       Note   With the release of the .NET Framework 3.5, the value of the <paramref name="returnType" /> parameter can now be the <see cref="T:System.DateTimeOffset" /> type.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>The element content converted to the requested typed object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Xml.XmlReader" /> is not positioned on an element.</exception>
		/// <exception cref="T:System.Xml.XmlException">The current element contains child elements.-or-The element content cannot be converted to the requested type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The method is called with <see langword="null" /> arguments.</exception>
		/// <exception cref="T:System.ArgumentException">The specified local name and namespace URI do not match that of the current element being read.</exception>
		/// <exception cref="T:System.OverflowException">Read <see langword="Decimal.MaxValue" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005F8 RID: 1528 RVA: 0x0001EB2E File Offset: 0x0001CD2E
		public virtual object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			this.CheckElement(localName, namespaceURI);
			return this.ReadElementContentAs(returnType, namespaceResolver);
		}

		/// <summary>When overridden in a derived class, gets the number of attributes on the current node.</summary>
		/// <returns>The number of attributes on the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005F9 RID: 1529
		public abstract int AttributeCount { get; }

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified <see cref="P:System.Xml.XmlReader.Name" />.</summary>
		/// <param name="name">The qualified name of the attribute.</param>
		/// <returns>The value of the specified attribute. If the attribute is not found or the value is <see langword="String.Empty" />, <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005FA RID: 1530
		public abstract string GetAttribute(string name);

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" />.</summary>
		/// <param name="name">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute.</param>
		/// <returns>The value of the specified attribute. If the attribute is not found or the value is <see langword="String.Empty" />, <see langword="null" /> is returned. This method does not move the reader.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005FB RID: 1531
		public abstract string GetAttribute(string name, string namespaceURI);

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute. The index is zero-based. (The first attribute has index 0.)</param>
		/// <returns>The value of the specified attribute. This method does not move the reader.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="i" /> is out of range. It must be non-negative and less than the size of the attribute collection.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x060005FC RID: 1532
		public abstract string GetAttribute(int i);

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute.</param>
		/// <returns>The value of the specified attribute.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C5 RID: 197
		public virtual string this[int i]
		{
			get
			{
				return this.GetAttribute(i);
			}
		}

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified <see cref="P:System.Xml.XmlReader.Name" />.</summary>
		/// <param name="name">The qualified name of the attribute.</param>
		/// <returns>The value of the specified attribute. If the attribute is not found, <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C6 RID: 198
		public virtual string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		/// <summary>When overridden in a derived class, gets the value of the attribute with the specified <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" />.</summary>
		/// <param name="name">The local name of the attribute.</param>
		/// <param name="namespaceURI">The namespace URI of the attribute.</param>
		/// <returns>The value of the specified attribute. If the attribute is not found, <see langword="null" /> is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C7 RID: 199
		public virtual string this[string name, string namespaceURI]
		{
			get
			{
				return this.GetAttribute(name, namespaceURI);
			}
		}

		/// <summary>When overridden in a derived class, moves to the attribute with the specified <see cref="P:System.Xml.XmlReader.Name" />.</summary>
		/// <param name="name">The qualified name of the attribute.</param>
		/// <returns>
		///     <see langword="true" /> if the attribute is found; otherwise, <see langword="false" />. If <see langword="false" />, the reader's position does not change.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentException">The parameter is an empty string.</exception>
		// Token: 0x06000600 RID: 1536
		public abstract bool MoveToAttribute(string name);

		/// <summary>When overridden in a derived class, moves to the attribute with the specified <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" />.</summary>
		/// <param name="name">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI of the attribute.</param>
		/// <returns>
		///     <see langword="true" /> if the attribute is found; otherwise, <see langword="false" />. If <see langword="false" />, the reader's position does not change.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentNullException">Both parameter values are <see langword="null" />.</exception>
		// Token: 0x06000601 RID: 1537
		public abstract bool MoveToAttribute(string name, string ns);

		/// <summary>When overridden in a derived class, moves to the attribute with the specified index.</summary>
		/// <param name="i">The index of the attribute.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The parameter has a negative value.</exception>
		// Token: 0x06000602 RID: 1538 RVA: 0x0001EB60 File Offset: 0x0001CD60
		public virtual void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.AttributeCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			this.MoveToElement();
			this.MoveToFirstAttribute();
			for (int j = 0; j < i; j++)
			{
				this.MoveToNextAttribute();
			}
		}

		/// <summary>When overridden in a derived class, moves to the first attribute.</summary>
		/// <returns>
		///     <see langword="true" /> if an attribute exists (the reader moves to the first attribute); otherwise, <see langword="false" /> (the position of the reader does not change).</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000603 RID: 1539
		public abstract bool MoveToFirstAttribute();

		/// <summary>When overridden in a derived class, moves to the next attribute.</summary>
		/// <returns>
		///     <see langword="true" /> if there is a next attribute; <see langword="false" /> if there are no more attributes.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000604 RID: 1540
		public abstract bool MoveToNextAttribute();

		/// <summary>When overridden in a derived class, moves to the element that contains the current attribute node.</summary>
		/// <returns>
		///     <see langword="true" /> if the reader is positioned on an attribute (the reader moves to the element that owns the attribute); <see langword="false" /> if the reader is not positioned on an attribute (the position of the reader does not change).</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000605 RID: 1541
		public abstract bool MoveToElement();

		/// <summary>When overridden in a derived class, parses the attribute value into one or more <see langword="Text" />, <see langword="EntityReference" />, or <see langword="EndEntity" /> nodes.</summary>
		/// <returns>
		///     <see langword="true" /> if there are nodes to return.
		///     <see langword="false" /> if the reader is not positioned on an attribute node when the initial call is made or if all the attribute values have been read.An empty attribute, such as, misc="", returns <see langword="true" /> with a single node with a value of <see langword="String.Empty" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000606 RID: 1542
		public abstract bool ReadAttributeValue();

		/// <summary>When overridden in a derived class, reads the next node from the stream.</summary>
		/// <returns>
		///     <see langword="true" /> if the next node was read successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000607 RID: 1543
		public abstract bool Read();

		/// <summary>When overridden in a derived class, gets a value indicating whether the reader is positioned at the end of the stream.</summary>
		/// <returns>
		///     <see langword="true" /> if the reader is positioned at the end of the stream; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000608 RID: 1544
		public abstract bool EOF { get; }

		/// <summary>When overridden in a derived class, changes the <see cref="P:System.Xml.XmlReader.ReadState" /> to <see cref="F:System.Xml.ReadState.Closed" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000609 RID: 1545 RVA: 0x0000B528 File Offset: 0x00009728
		public virtual void Close()
		{
		}

		/// <summary>When overridden in a derived class, gets the state of the reader.</summary>
		/// <returns>One of the enumeration values that specifies the state of the reader.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600060A RID: 1546
		public abstract ReadState ReadState { get; }

		/// <summary>Skips the children of the current node.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600060B RID: 1547 RVA: 0x0001EBA6 File Offset: 0x0001CDA6
		public virtual void Skip()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return;
			}
			this.SkipSubtree();
		}

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.Xml.XmlNameTable" /> associated with this implementation.</summary>
		/// <returns>The <see langword="XmlNameTable" /> enabling you to get the atomized version of a string within the node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600060C RID: 1548
		public abstract XmlNameTable NameTable { get; }

		/// <summary>When overridden in a derived class, resolves a namespace prefix in the current element's scope.</summary>
		/// <param name="prefix">The prefix whose namespace URI you want to resolve. To match the default namespace, pass an empty string. </param>
		/// <returns>The namespace URI to which the prefix maps or <see langword="null" /> if no matching prefix is found.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600060D RID: 1549
		public abstract string LookupNamespace(string prefix);

		/// <summary>Gets a value indicating whether this reader can parse and resolve entities.</summary>
		/// <returns>
		///     <see langword="true" /> if the reader can parse and resolve entities; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool CanResolveEntity
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, resolves the entity reference for <see langword="EntityReference" /> nodes.</summary>
		/// <exception cref="T:System.InvalidOperationException">The reader is not positioned on an <see langword="EntityReference" /> node; this implementation of the reader cannot resolve entities (<see cref="P:System.Xml.XmlReader.CanResolveEntity" /> returns <see langword="false" />).</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600060F RID: 1551
		public abstract void ResolveEntity();

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlReader" /> implements the binary content read methods.</summary>
		/// <returns>
		///     <see langword="true" /> if the binary content read methods are implemented; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool CanReadBinaryContent
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <see cref="M:System.Xml.XmlReader.ReadContentAsBase64(System.Byte[],System.Int32,System.Int32)" /> is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000611 RID: 1553 RVA: 0x0001EBB9 File Offset: 0x0001CDB9
		public virtual int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[]
			{
				"ReadContentAsBase64"
			}));
		}

		/// <summary>Reads the element and decodes the <see langword="Base64" /> content.</summary>
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
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000612 RID: 1554 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
		public virtual int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[]
			{
				"ReadElementContentAsBase64"
			}));
		}

		/// <summary>Reads the content and returns the <see langword="BinHex" /> decoded binary bytes.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///         <see cref="M:System.Xml.XmlReader.ReadContentAsBinHex(System.Byte[],System.Int32,System.Int32)" /> is not supported on the current node.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000613 RID: 1555 RVA: 0x0001EBF7 File Offset: 0x0001CDF7
		public virtual int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[]
			{
				"ReadContentAsBinHex"
			}));
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
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000614 RID: 1556 RVA: 0x0001EC16 File Offset: 0x0001CE16
		public virtual int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[]
			{
				"ReadElementContentAsBinHex"
			}));
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Xml.XmlReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XmlReader" /> implements the <see cref="M:System.Xml.XmlReader.ReadValueChunk(System.Char[],System.Int32,System.Int32)" /> method; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public virtual bool CanReadValueChunk
		{
			get
			{
				return false;
			}
		}

		/// <summary>Reads large streams of text embedded in an XML document.</summary>
		/// <param name="buffer">The array of characters that serves as the buffer to which the text contents are written. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset within the buffer where the <see cref="T:System.Xml.XmlReader" /> can start to copy the results.</param>
		/// <param name="count">The maximum number of characters to copy into the buffer. The actual number of characters copied is returned from this method.</param>
		/// <returns>The number of characters read into the buffer. The value zero is returned when there is no more text content.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current node does not have a value (<see cref="P:System.Xml.XmlReader.HasValue" /> is <see langword="false" />).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index into the buffer, or index + count is larger than the allocated buffer size.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Xml.XmlReader" /> implementation does not support this method.</exception>
		/// <exception cref="T:System.Xml.XmlException">The XML data is not well-formed.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000616 RID: 1558 RVA: 0x0001EC35 File Offset: 0x0001CE35
		public virtual int ReadValueChunk(char[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("ReadValueChunk method is not supported on this XmlReader. Use CanReadValueChunk property to find out if an XmlReader implements it."));
		}

		/// <summary>When overridden in a derived class, reads the contents of an element or text node as a string. However, we recommend that you use the <see cref="Overload:System.Xml.XmlReader.ReadElementContentAsString" /> method instead, because it provides a more straightforward way to handle this operation.</summary>
		/// <returns>The contents of the element or an empty string.</returns>
		/// <exception cref="T:System.Xml.XmlException">An error occurred while parsing the XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000617 RID: 1559 RVA: 0x0001EC48 File Offset: 0x0001CE48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadString()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			this.MoveToElement();
			if (this.NodeType == XmlNodeType.Element)
			{
				if (this.IsEmptyElement)
				{
					return string.Empty;
				}
				if (!this.Read())
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				if (this.NodeType == XmlNodeType.EndElement)
				{
					return string.Empty;
				}
			}
			string text = string.Empty;
			while (XmlReader.IsTextualNode(this.NodeType))
			{
				text += this.Value;
				if (!this.Read())
				{
					break;
				}
			}
			return text;
		}

		/// <summary>Checks whether the current node is a content (non-white space text, <see langword="CDATA" />, <see langword="Element" />, <see langword="EndElement" />, <see langword="EntityReference" />, or <see langword="EndEntity" />) node. If the node is not a content node, the reader skips ahead to the next content node or end of file. It skips over nodes of the following type: <see langword="ProcessingInstruction" />, <see langword="DocumentType" />, <see langword="Comment" />, <see langword="Whitespace" />, or <see langword="SignificantWhitespace" />.</summary>
		/// <returns>The <see cref="P:System.Xml.XmlReader.NodeType" /> of the current node found by the method or <see langword="XmlNodeType.None" /> if the reader has reached the end of the input stream.</returns>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000618 RID: 1560 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		public virtual XmlNodeType MoveToContent()
		{
			for (;;)
			{
				XmlNodeType nodeType = this.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.EntityReference:
					goto IL_33;
				case XmlNodeType.Attribute:
					goto IL_2C;
				default:
					if (nodeType - XmlNodeType.EndElement <= 1)
					{
						goto IL_33;
					}
					if (!this.Read())
					{
						goto Block_2;
					}
					break;
				}
			}
			IL_2C:
			this.MoveToElement();
			IL_33:
			return this.NodeType;
			Block_2:
			return this.NodeType;
		}

		/// <summary>Checks that the current node is an element and advances the reader to the next node.</summary>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML was encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000619 RID: 1561 RVA: 0x0001ED30 File Offset: 0x0001CF30
		public virtual void ReadStartElement()
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			this.Read();
		}

		/// <summary>Checks that the current content node is an element with the given <see cref="P:System.Xml.XmlReader.Name" /> and advances the reader to the next node.</summary>
		/// <param name="name">The qualified name of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML was encountered in the input stream. -or- The <see cref="P:System.Xml.XmlReader.Name" /> of the element does not match the given <paramref name="name" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600061A RID: 1562 RVA: 0x0001ED74 File Offset: 0x0001CF74
		public virtual void ReadStartElement(string name)
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.Name == name)
			{
				this.Read();
				return;
			}
			throw new XmlException("Element '{0}' was not found.", name, this as IXmlLineInfo);
		}

		/// <summary>Checks that the current content node is an element with the given <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> and advances the reader to the next node.</summary>
		/// <param name="localname">The local name of the element.</param>
		/// <param name="ns">The namespace URI of the element.</param>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML was encountered in the input stream.-or-The <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element found do not match the given arguments.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600061B RID: 1563 RVA: 0x0001EDD8 File Offset: 0x0001CFD8
		public virtual void ReadStartElement(string localname, string ns)
		{
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName == localname && this.NamespaceURI == ns)
			{
				this.Read();
				return;
			}
			throw new XmlException("Element '{0}' with namespace name '{1}' was not found.", new string[]
			{
				localname,
				ns
			}, this as IXmlLineInfo);
		}

		/// <summary>Reads a text-only element. However, we recommend that you use the <see cref="M:System.Xml.XmlReader.ReadElementContentAsString" /> method instead, because it provides a more straightforward way to handle this operation.</summary>
		/// <returns>The text contained in the element that was read. An empty string if the element is empty.</returns>
		/// <exception cref="T:System.Xml.XmlException">The next content node is not a start tag; or the element found does not contain a simple text value.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600061C RID: 1564 RVA: 0x0001EE58 File Offset: 0x0001D058
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString()
		{
			string result = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				this.Read();
				result = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("Unexpected node type {0}. {1} method can only be called on elements with simple or empty content.", new string[]
					{
						this.NodeType.ToString(),
						"ReadElementString"
					}, this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return result;
		}

		/// <summary>Checks that the <see cref="P:System.Xml.XmlReader.Name" /> property of the element found matches the given string before reading a text-only element. However, we recommend that you use the <see cref="M:System.Xml.XmlReader.ReadElementContentAsString" /> method instead, because it provides a more straightforward way to handle this operation.</summary>
		/// <param name="name">The name to check.</param>
		/// <returns>The text contained in the element that was read. An empty string if the element is empty.</returns>
		/// <exception cref="T:System.Xml.XmlException">If the next content node is not a start tag; if the element <see langword="Name" /> does not match the given argument; or if the element found does not contain a simple text value.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600061D RID: 1565 RVA: 0x0001EF00 File Offset: 0x0001D100
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString(string name)
		{
			string result = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.Name != name)
			{
				throw new XmlException("Element '{0}' was not found.", name, this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				result = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return result;
		}

		/// <summary>Checks that the <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element found matches the given strings before reading a text-only element. However, we recommend that you use the <see cref="M:System.Xml.XmlReader.ReadElementContentAsString(System.String,System.String)" /> method instead, because it provides a more straightforward way to handle this operation.</summary>
		/// <param name="localname">The local name to check.</param>
		/// <param name="ns">The namespace URI to check.</param>
		/// <returns>The text contained in the element that was read. An empty string if the element is empty.</returns>
		/// <exception cref="T:System.Xml.XmlException">If the next content node is not a start tag; if the element <see langword="LocalName" /> or <see langword="NamespaceURI" /> do not match the given arguments; or if the element found does not contain a simple text value.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600061E RID: 1566 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual string ReadElementString(string localname, string ns)
		{
			string result = string.Empty;
			if (this.MoveToContent() != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName != localname || this.NamespaceURI != ns)
			{
				throw new XmlException("Element '{0}' with namespace name '{1}' was not found.", new string[]
				{
					localname,
					ns
				}, this as IXmlLineInfo);
			}
			if (!this.IsEmptyElement)
			{
				result = this.ReadString();
				if (this.NodeType != XmlNodeType.EndElement)
				{
					throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
				}
				this.Read();
			}
			else
			{
				this.Read();
			}
			return result;
		}

		/// <summary>Checks that the current content node is an end tag and advances the reader to the next node.</summary>
		/// <exception cref="T:System.Xml.XmlException">The current node is not an end tag or if incorrect XML is encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600061F RID: 1567 RVA: 0x0001F07C File Offset: 0x0001D27C
		public virtual void ReadEndElement()
		{
			if (this.MoveToContent() != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			this.Read();
		}

		/// <summary>Calls <see cref="M:System.Xml.XmlReader.MoveToContent" /> and tests if the current content node is a start tag or empty element tag.</summary>
		/// <returns>
		///     <see langword="true" /> if <see cref="M:System.Xml.XmlReader.MoveToContent" /> finds a start tag or empty element tag; <see langword="false" /> if a node type other than <see langword="XmlNodeType.Element" /> was found.</returns>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML is encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000620 RID: 1568 RVA: 0x0001F0BF File Offset: 0x0001D2BF
		public virtual bool IsStartElement()
		{
			return this.MoveToContent() == XmlNodeType.Element;
		}

		/// <summary>Calls <see cref="M:System.Xml.XmlReader.MoveToContent" /> and tests if the current content node is a start tag or empty element tag and if the <see cref="P:System.Xml.XmlReader.Name" /> property of the element found matches the given argument.</summary>
		/// <param name="name">The string matched against the <see langword="Name" /> property of the element found.</param>
		/// <returns>
		///     <see langword="true" /> if the resulting node is an element and the <see langword="Name" /> property matches the specified string. <see langword="false" /> if a node type other than <see langword="XmlNodeType.Element" /> was found or if the element <see langword="Name" /> property does not match the specified string.</returns>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML is encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000621 RID: 1569 RVA: 0x0001F0CA File Offset: 0x0001D2CA
		public virtual bool IsStartElement(string name)
		{
			return this.MoveToContent() == XmlNodeType.Element && this.Name == name;
		}

		/// <summary>Calls <see cref="M:System.Xml.XmlReader.MoveToContent" /> and tests if the current content node is a start tag or empty element tag and if the <see cref="P:System.Xml.XmlReader.LocalName" /> and <see cref="P:System.Xml.XmlReader.NamespaceURI" /> properties of the element found match the given strings.</summary>
		/// <param name="localname">The string to match against the <see langword="LocalName" /> property of the element found.</param>
		/// <param name="ns">The string to match against the <see langword="NamespaceURI" /> property of the element found.</param>
		/// <returns>
		///     <see langword="true" /> if the resulting node is an element. <see langword="false" /> if a node type other than <see langword="XmlNodeType.Element" /> was found or if the <see langword="LocalName" /> and <see langword="NamespaceURI" /> properties of the element do not match the specified strings.</returns>
		/// <exception cref="T:System.Xml.XmlException">Incorrect XML is encountered in the input stream.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000622 RID: 1570 RVA: 0x0001F0E3 File Offset: 0x0001D2E3
		public virtual bool IsStartElement(string localname, string ns)
		{
			return this.MoveToContent() == XmlNodeType.Element && this.LocalName == localname && this.NamespaceURI == ns;
		}

		/// <summary>Reads until an element with the specified qualified name is found.</summary>
		/// <param name="name">The qualified name of the element.</param>
		/// <returns>
		///     <see langword="true" /> if a matching element is found; otherwise <see langword="false" /> and the <see cref="T:System.Xml.XmlReader" /> is in an end of file state.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentException">The parameter is an empty string.</exception>
		// Token: 0x06000623 RID: 1571 RVA: 0x0001F10C File Offset: 0x0001D30C
		public virtual bool ReadToFollowing(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			name = this.NameTable.Add(name);
			while (this.Read())
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Reads until an element with the specified local name and namespace URI is found.</summary>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="namespaceURI">The namespace URI of the element.</param>
		/// <returns>
		///     <see langword="true" /> if a matching element is found; otherwise <see langword="false" /> and the <see cref="T:System.Xml.XmlReader" /> is in an end of file state.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentNullException">Both parameter values are <see langword="null" />.</exception>
		// Token: 0x06000624 RID: 1572 RVA: 0x0001F164 File Offset: 0x0001D364
		public virtual bool ReadToFollowing(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.Read())
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Advances the <see cref="T:System.Xml.XmlReader" /> to the next descendant element with the specified qualified name.</summary>
		/// <param name="name">The qualified name of the element you wish to move to.</param>
		/// <returns>
		///     <see langword="true" /> if a matching descendant element is found; otherwise <see langword="false" />. If a matching child element is not found, the <see cref="T:System.Xml.XmlReader" /> is positioned on the end tag (<see cref="P:System.Xml.XmlReader.NodeType" /> is <see langword="XmlNodeType.EndElement" />) of the element.If the <see cref="T:System.Xml.XmlReader" /> is not positioned on an element when <see cref="M:System.Xml.XmlReader.ReadToDescendant(System.String)" /> was called, this method returns <see langword="false" /> and the position of the <see cref="T:System.Xml.XmlReader" /> is not changed.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentException">The parameter is an empty string.</exception>
		// Token: 0x06000625 RID: 1573 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		public virtual bool ReadToDescendant(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			int num = this.Depth;
			if (this.NodeType != XmlNodeType.Element)
			{
				if (this.ReadState != ReadState.Initial)
				{
					return false;
				}
				num--;
			}
			else if (this.IsEmptyElement)
			{
				return false;
			}
			name = this.NameTable.Add(name);
			while (this.Read() && this.Depth > num)
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Advances the <see cref="T:System.Xml.XmlReader" /> to the next descendant element with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the element you wish to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the element you wish to move to.</param>
		/// <returns>
		///     <see langword="true" /> if a matching descendant element is found; otherwise <see langword="false" />. If a matching child element is not found, the <see cref="T:System.Xml.XmlReader" /> is positioned on the end tag (<see cref="P:System.Xml.XmlReader.NodeType" /> is <see langword="XmlNodeType.EndElement" />) of the element.If the <see cref="T:System.Xml.XmlReader" /> is not positioned on an element when <see cref="M:System.Xml.XmlReader.ReadToDescendant(System.String,System.String)" /> was called, this method returns <see langword="false" /> and the position of the <see cref="T:System.Xml.XmlReader" /> is not changed.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentNullException">Both parameter values are <see langword="null" />.</exception>
		// Token: 0x06000626 RID: 1574 RVA: 0x0001F270 File Offset: 0x0001D470
		public virtual bool ReadToDescendant(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			int num = this.Depth;
			if (this.NodeType != XmlNodeType.Element)
			{
				if (this.ReadState != ReadState.Initial)
				{
					return false;
				}
				num--;
			}
			else if (this.IsEmptyElement)
			{
				return false;
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.Read() && this.Depth > num)
			{
				if (this.NodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Advances the <see langword="XmlReader" /> to the next sibling element with the specified qualified name.</summary>
		/// <param name="name">The qualified name of the sibling element you wish to move to.</param>
		/// <returns>
		///     <see langword="true" /> if a matching sibling element is found; otherwise <see langword="false" />. If a matching sibling element is not found, the <see langword="XmlReader" /> is positioned on the end tag (<see cref="P:System.Xml.XmlReader.NodeType" /> is <see langword="XmlNodeType.EndElement" />) of the parent element.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentException">The parameter is an empty string.</exception>
		// Token: 0x06000627 RID: 1575 RVA: 0x0001F324 File Offset: 0x0001D524
		public virtual bool ReadToNextSibling(string name)
		{
			if (name == null || name.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(name, "name");
			}
			name = this.NameTable.Add(name);
			while (this.SkipSubtree())
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element && Ref.Equal(name, this.Name))
				{
					return true;
				}
				if (nodeType == XmlNodeType.EndElement || this.EOF)
				{
					break;
				}
			}
			return false;
		}

		/// <summary>Advances the <see langword="XmlReader" /> to the next sibling element with the specified local name and namespace URI.</summary>
		/// <param name="localName">The local name of the sibling element you wish to move to.</param>
		/// <param name="namespaceURI">The namespace URI of the sibling element you wish to move to.</param>
		/// <returns>
		///     <see langword="true" /> if a matching sibling element is found; otherwise, <see langword="false" />. If a matching sibling element is not found, the <see langword="XmlReader" /> is positioned on the end tag (<see cref="P:System.Xml.XmlReader.NodeType" /> is <see langword="XmlNodeType.EndElement" />) of the parent element.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.ArgumentNullException">Both parameter values are <see langword="null" />.</exception>
		// Token: 0x06000628 RID: 1576 RVA: 0x0001F388 File Offset: 0x0001D588
		public virtual bool ReadToNextSibling(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			localName = this.NameTable.Add(localName);
			namespaceURI = this.NameTable.Add(namespaceURI);
			while (this.SkipSubtree())
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Element && Ref.Equal(localName, this.LocalName) && Ref.Equal(namespaceURI, this.NamespaceURI))
				{
					return true;
				}
				if (nodeType == XmlNodeType.EndElement || this.EOF)
				{
					break;
				}
			}
			return false;
		}

		/// <summary>Returns a value indicating whether the string argument is a valid XML name.</summary>
		/// <param name="str">The name to validate.</param>
		/// <returns>
		///     <see langword="true" /> if the name is valid; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> value is <see langword="null" />.</exception>
		// Token: 0x06000629 RID: 1577 RVA: 0x0001F415 File Offset: 0x0001D615
		public static bool IsName(string str)
		{
			if (str == null)
			{
				throw new NullReferenceException();
			}
			return ValidateNames.IsNameNoNamespaces(str);
		}

		/// <summary>Returns a value indicating whether or not the string argument is a valid XML name token.</summary>
		/// <param name="str">The name token to validate.</param>
		/// <returns>
		///     <see langword="true" /> if it is a valid name token; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> value is <see langword="null" />.</exception>
		// Token: 0x0600062A RID: 1578 RVA: 0x0001F426 File Offset: 0x0001D626
		public static bool IsNameToken(string str)
		{
			if (str == null)
			{
				throw new NullReferenceException();
			}
			return ValidateNames.IsNmtokenNoNamespaces(str);
		}

		/// <summary>When overridden in a derived class, reads all the content, including markup, as a string.</summary>
		/// <returns>All the XML content, including markup, in the current node. If the current node has no children, an empty string is returned.If the current node is neither an element nor attribute, an empty string is returned.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML was not well-formed, or an error occurred while parsing the XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600062B RID: 1579 RVA: 0x0001F438 File Offset: 0x0001D638
		public virtual string ReadInnerXml()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Attribute && this.NodeType != XmlNodeType.Element)
			{
				this.Read();
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = this.CreateWriterForInnerOuterXml(stringWriter);
			try
			{
				if (this.NodeType == XmlNodeType.Attribute)
				{
					((XmlTextWriter)xmlWriter).QuoteChar = this.QuoteChar;
					this.WriteAttributeValue(xmlWriter);
				}
				if (this.NodeType == XmlNodeType.Element)
				{
					this.WriteNode(xmlWriter, false);
				}
			}
			finally
			{
				xmlWriter.Close();
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001F4D8 File Offset: 0x0001D6D8
		private void WriteNode(XmlWriter xtw, bool defattr)
		{
			int num = (this.NodeType == XmlNodeType.None) ? -1 : this.Depth;
			while (this.Read() && num < this.Depth)
			{
				switch (this.NodeType)
				{
				case XmlNodeType.Element:
					xtw.WriteStartElement(this.Prefix, this.LocalName, this.NamespaceURI);
					((XmlTextWriter)xtw).QuoteChar = this.QuoteChar;
					xtw.WriteAttributes(this, defattr);
					if (this.IsEmptyElement)
					{
						xtw.WriteEndElement();
					}
					break;
				case XmlNodeType.Text:
					xtw.WriteString(this.Value);
					break;
				case XmlNodeType.CDATA:
					xtw.WriteCData(this.Value);
					break;
				case XmlNodeType.EntityReference:
					xtw.WriteEntityRef(this.Name);
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.XmlDeclaration:
					xtw.WriteProcessingInstruction(this.Name, this.Value);
					break;
				case XmlNodeType.Comment:
					xtw.WriteComment(this.Value);
					break;
				case XmlNodeType.DocumentType:
					xtw.WriteDocType(this.Name, this.GetAttribute("PUBLIC"), this.GetAttribute("SYSTEM"), this.Value);
					break;
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					xtw.WriteWhitespace(this.Value);
					break;
				case XmlNodeType.EndElement:
					xtw.WriteFullEndElement();
					break;
				}
			}
			if (num == this.Depth && this.NodeType == XmlNodeType.EndElement)
			{
				this.Read();
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001F654 File Offset: 0x0001D854
		private void WriteAttributeValue(XmlWriter xtw)
		{
			string name = this.Name;
			while (this.ReadAttributeValue())
			{
				if (this.NodeType == XmlNodeType.EntityReference)
				{
					xtw.WriteEntityRef(this.Name);
				}
				else
				{
					xtw.WriteString(this.Value);
				}
			}
			this.MoveToAttribute(name);
		}

		/// <summary>When overridden in a derived class, reads the content, including markup, representing this node and all its children.</summary>
		/// <returns>If the reader is positioned on an element or an attribute node, this method returns all the XML content, including markup, of the current node and all its children; otherwise, it returns an empty string.</returns>
		/// <exception cref="T:System.Xml.XmlException">The XML was not well-formed, or an error occurred while parsing the XML.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x0600062E RID: 1582 RVA: 0x0001F6A0 File Offset: 0x0001D8A0
		public virtual string ReadOuterXml()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return string.Empty;
			}
			if (this.NodeType != XmlNodeType.Attribute && this.NodeType != XmlNodeType.Element)
			{
				this.Read();
				return string.Empty;
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = this.CreateWriterForInnerOuterXml(stringWriter);
			try
			{
				if (this.NodeType == XmlNodeType.Attribute)
				{
					xmlWriter.WriteStartAttribute(this.Prefix, this.LocalName, this.NamespaceURI);
					this.WriteAttributeValue(xmlWriter);
					xmlWriter.WriteEndAttribute();
				}
				else
				{
					xmlWriter.WriteNode(this, false);
				}
			}
			finally
			{
				xmlWriter.Close();
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001F748 File Offset: 0x0001D948
		private XmlWriter CreateWriterForInnerOuterXml(StringWriter sw)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(sw);
			this.SetNamespacesFlag(xmlTextWriter);
			return xmlTextWriter;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001F764 File Offset: 0x0001D964
		private void SetNamespacesFlag(XmlTextWriter xtw)
		{
			XmlTextReader xmlTextReader = this as XmlTextReader;
			if (xmlTextReader != null)
			{
				xtw.Namespaces = xmlTextReader.Namespaces;
				return;
			}
			XmlValidatingReader xmlValidatingReader = this as XmlValidatingReader;
			if (xmlValidatingReader != null)
			{
				xtw.Namespaces = xmlValidatingReader.Namespaces;
			}
		}

		/// <summary>Returns a new <see langword="XmlReader" /> instance that can be used to read the current node, and all its descendants.</summary>
		/// <returns>A new XML reader instance set to <see cref="F:System.Xml.ReadState.Initial" />. Calling the <see cref="M:System.Xml.XmlReader.Read" /> method positions the new reader on the node that was current before the call to the <see cref="M:System.Xml.XmlReader.ReadSubtree" /> method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The XML reader isn't positioned on an element when this method is called.</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000631 RID: 1585 RVA: 0x0001F79E File Offset: 0x0001D99E
		public virtual XmlReader ReadSubtree()
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw new InvalidOperationException(Res.GetString("ReadSubtree() can be called only if the reader is on an element node."));
			}
			return new XmlSubtreeReader(this);
		}

		/// <summary>Gets a value indicating whether the current node has any attributes.</summary>
		/// <returns>
		///     <see langword="true" /> if the current node has attributes; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0001F7BF File Offset: 0x0001D9BF
		public virtual bool HasAttributes
		{
			get
			{
				return this.AttributeCount > 0;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Xml.XmlReader" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000633 RID: 1587 RVA: 0x0001F7CA File Offset: 0x0001D9CA
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Xml.XmlReader" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///       <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		// Token: 0x06000634 RID: 1588 RVA: 0x0001F7D3 File Offset: 0x0001D9D3
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.ReadState != ReadState.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual XmlNamespaceManager NamespaceManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001F7E7 File Offset: 0x0001D9E7
		internal static bool IsTextualNode(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.IsTextualNodeBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001F7FB File Offset: 0x0001D9FB
		internal static bool CanReadContentAs(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.CanReadContentAsBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001F80F File Offset: 0x0001DA0F
		internal static bool HasValueInternal(XmlNodeType nodeType)
		{
			return ((ulong)XmlReader.HasValueBitmap & (ulong)(1L << (int)(nodeType & (XmlNodeType)31))) > 0UL;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001F824 File Offset: 0x0001DA24
		private bool SkipSubtree()
		{
			this.MoveToElement();
			if (this.NodeType == XmlNodeType.Element && !this.IsEmptyElement)
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
				return this.NodeType == XmlNodeType.EndElement && this.Read();
			}
			return this.Read();
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001F87C File Offset: 0x0001DA7C
		internal void CheckElement(string localName, string namespaceURI)
		{
			if (localName == null || localName.Length == 0)
			{
				throw XmlConvert.CreateInvalidNameArgumentException(localName, "localName");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			if (this.NodeType != XmlNodeType.Element)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString(), this as IXmlLineInfo);
			}
			if (this.LocalName != localName || this.NamespaceURI != namespaceURI)
			{
				throw new XmlException("Element '{0}' with namespace name '{1}' was not found.", new string[]
				{
					localName,
					namespaceURI
				}, this as IXmlLineInfo);
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001F917 File Offset: 0x0001DB17
		internal Exception CreateReadContentAsException(string methodName)
		{
			return XmlReader.CreateReadContentAsException(methodName, this.NodeType, this as IXmlLineInfo);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001F92B File Offset: 0x0001DB2B
		internal Exception CreateReadElementContentAsException(string methodName)
		{
			return XmlReader.CreateReadElementContentAsException(methodName, this.NodeType, this as IXmlLineInfo);
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001F93F File Offset: 0x0001DB3F
		internal bool CanReadContentAs()
		{
			return XmlReader.CanReadContentAs(this.NodeType);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001F94C File Offset: 0x0001DB4C
		internal static Exception CreateReadContentAsException(string methodName, XmlNodeType nodeType, IXmlLineInfo lineInfo)
		{
			string name = "The {0} method is not supported on node type {1}. If you want to read typed content of an element, use the ReadElementContentAs method.";
			object[] args = new string[]
			{
				methodName,
				nodeType.ToString()
			};
			return new InvalidOperationException(XmlReader.AddLineInfo(Res.GetString(name, args), lineInfo));
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001F98C File Offset: 0x0001DB8C
		internal static Exception CreateReadElementContentAsException(string methodName, XmlNodeType nodeType, IXmlLineInfo lineInfo)
		{
			string name = "The {0} method is not supported on node type {1}.";
			object[] args = new string[]
			{
				methodName,
				nodeType.ToString()
			};
			return new InvalidOperationException(XmlReader.AddLineInfo(Res.GetString(name, args), lineInfo));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001F9CC File Offset: 0x0001DBCC
		private static string AddLineInfo(string message, IXmlLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				string[] array = new string[]
				{
					lineInfo.LineNumber.ToString(CultureInfo.InvariantCulture),
					lineInfo.LinePosition.ToString(CultureInfo.InvariantCulture)
				};
				string str = message;
				string str2 = " ";
				string name = "Line {0}, position {1}.";
				object[] args = array;
				message = str + str2 + Res.GetString(name, args);
			}
			return message;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		internal string InternalReadContentAsString()
		{
			string text = string.Empty;
			StringBuilder stringBuilder = null;
			do
			{
				switch (this.NodeType)
				{
				case XmlNodeType.Attribute:
					goto IL_55;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (text.Length == 0)
					{
						text = this.Value;
						goto IL_9B;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
						stringBuilder.Append(text);
					}
					stringBuilder.Append(this.Value);
					goto IL_9B;
				case XmlNodeType.EntityReference:
					if (this.CanResolveEntity)
					{
						this.ResolveEntity();
						goto IL_9B;
					}
					break;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					goto IL_9B;
				}
				break;
				IL_9B:;
			}
			while ((this.AttributeCount != 0) ? this.ReadAttributeValue() : this.Read());
			goto IL_B6;
			IL_55:
			return this.Value;
			IL_B6:
			if (stringBuilder != null)
			{
				return stringBuilder.ToString();
			}
			return text;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001FAFC File Offset: 0x0001DCFC
		private bool SetupReadElementContentAsXxx(string methodName)
		{
			if (this.NodeType != XmlNodeType.Element)
			{
				throw this.CreateReadElementContentAsException(methodName);
			}
			bool isEmptyElement = this.IsEmptyElement;
			this.Read();
			if (isEmptyElement)
			{
				return false;
			}
			XmlNodeType nodeType = this.NodeType;
			if (nodeType == XmlNodeType.EndElement)
			{
				this.Read();
				return false;
			}
			if (nodeType == XmlNodeType.Element)
			{
				throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, this as IXmlLineInfo);
			}
			return true;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001FB5C File Offset: 0x0001DD5C
		private void FinishReadElementContentAsXxx()
		{
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString());
			}
			this.Read();
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		internal bool IsDefaultInternal
		{
			get
			{
				if (this.IsDefault)
				{
					return true;
				}
				IXmlSchemaInfo schemaInfo = this.SchemaInfo;
				return schemaInfo != null && schemaInfo.IsDefault;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0001DA42 File Offset: 0x0001BC42
		internal virtual IDtdInfo DtdInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		internal static Encoding GetEncoding(XmlReader reader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = XmlReader.GetXmlTextReaderImpl(reader);
			if (xmlTextReaderImpl == null)
			{
				return null;
			}
			return xmlTextReaderImpl.Encoding;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001FBE8 File Offset: 0x0001DDE8
		internal static ConformanceLevel GetV1ConformanceLevel(XmlReader reader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = XmlReader.GetXmlTextReaderImpl(reader);
			if (xmlTextReaderImpl == null)
			{
				return ConformanceLevel.Document;
			}
			return xmlTextReaderImpl.V1ComformanceLevel;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001FC08 File Offset: 0x0001DE08
		private static XmlTextReaderImpl GetXmlTextReaderImpl(XmlReader reader)
		{
			XmlTextReaderImpl xmlTextReaderImpl = reader as XmlTextReaderImpl;
			if (xmlTextReaderImpl != null)
			{
				return xmlTextReaderImpl;
			}
			XmlTextReader xmlTextReader = reader as XmlTextReader;
			if (xmlTextReader != null)
			{
				return xmlTextReader.Impl;
			}
			XmlValidatingReaderImpl xmlValidatingReaderImpl = reader as XmlValidatingReaderImpl;
			if (xmlValidatingReaderImpl != null)
			{
				return xmlValidatingReaderImpl.ReaderImpl;
			}
			XmlValidatingReader xmlValidatingReader = reader as XmlValidatingReader;
			if (xmlValidatingReader != null)
			{
				return xmlValidatingReader.Impl.ReaderImpl;
			}
			return null;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance with specified URI.</summary>
		/// <param name="inputUri">The URI for the file that contains the XML data. The <see cref="T:System.Xml.XmlUrlResolver" /> class is used to convert the path to a canonical data representation.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Xml.XmlReader" /> does not have sufficient permissions to access the location of the XML data.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file identified by the URI does not exist.</exception>
		/// <exception cref="T:System.UriFormatException">
		///           In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The URI format is not correct.</exception>
		// Token: 0x06000649 RID: 1609 RVA: 0x0001FC5A File Offset: 0x0001DE5A
		public static XmlReader Create(string inputUri)
		{
			return XmlReader.Create(inputUri, null, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance by using the specified URI and settings.</summary>
		/// <param name="inputUri">The URI for the file containing the XML data. The <see cref="T:System.Xml.XmlResolver" /> object on the <see cref="T:System.Xml.XmlReaderSettings" /> object is used to convert the path to a canonical data representation. If <see cref="P:System.Xml.XmlReaderSettings.XmlResolver" /> is <see langword="null" />, a new <see cref="T:System.Xml.XmlUrlResolver" /> object is used.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be <see langword="null" />.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="inputUri" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by the URI cannot be found.</exception>
		/// <exception cref="T:System.UriFormatException">
		///           In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.FormatException" />, instead.The URI format is not correct.</exception>
		// Token: 0x0600064A RID: 1610 RVA: 0x0001FC64 File Offset: 0x0001DE64
		public static XmlReader Create(string inputUri, XmlReaderSettings settings)
		{
			return XmlReader.Create(inputUri, settings, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance by using the specified URI, settings, and context information for parsing.</summary>
		/// <param name="inputUri">The URI for the file containing the XML data. The <see cref="T:System.Xml.XmlResolver" /> object on the <see cref="T:System.Xml.XmlReaderSettings" /> object is used to convert the path to a canonical data representation. If <see cref="P:System.Xml.XmlReaderSettings.XmlResolver" /> is <see langword="null" />, a new <see cref="T:System.Xml.XmlUrlResolver" /> object is used.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be <see langword="null" />.</param>
		/// <param name="inputContext">The context information required to parse the XML fragment. The context information can include the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang and xml:space scope, base URI, and document type definition. This value can be <see langword="null" />.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see langword="inputUri" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Xml.XmlReader" /> does not have sufficient permissions to access the location of the XML data.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Xml.XmlReaderSettings.NameTable" />  and <see cref="P:System.Xml.XmlParserContext.NameTable" /> properties both contain values. (Only one of these <see langword="NameTable" /> properties can be set and used).</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by the URI cannot be found.</exception>
		/// <exception cref="T:System.UriFormatException">The URI format is not correct.</exception>
		// Token: 0x0600064B RID: 1611 RVA: 0x0001FC6E File Offset: 0x0001DE6E
		public static XmlReader Create(string inputUri, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(inputUri, inputContext);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance using the specified stream with default settings.</summary>
		/// <param name="input">The stream that contains the XML data.The <see cref="T:System.Xml.XmlReader" /> scans the first bytes of the stream looking for a byte order mark or other sign of encoding. When encoding is determined, the encoding is used to continue reading the stream, and processing continues parsing the input as a stream of (Unicode) characters.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Xml.XmlReader" /> does not have sufficient permissions to access the location of the XML data.</exception>
		// Token: 0x0600064C RID: 1612 RVA: 0x0001FC82 File Offset: 0x0001DE82
		public static XmlReader Create(Stream input)
		{
			return XmlReader.Create(input, null, string.Empty);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance with the specified stream and settings.</summary>
		/// <param name="input">The stream that contains the XML data.The <see cref="T:System.Xml.XmlReader" /> scans the first bytes of the stream looking for a byte order mark or other sign of encoding. When encoding is determined, the encoding is used to continue reading the stream, and processing continues parsing the input as a stream of (Unicode) characters.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be <see langword="null" />.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is <see langword="null" />.</exception>
		// Token: 0x0600064D RID: 1613 RVA: 0x0001FC90 File Offset: 0x0001DE90
		public static XmlReader Create(Stream input, XmlReaderSettings settings)
		{
			return XmlReader.Create(input, settings, string.Empty);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance using the specified stream, base URI, and settings.</summary>
		/// <param name="input">The stream that contains the XML data. The <see cref="T:System.Xml.XmlReader" /> scans the first bytes of the stream looking for a byte order mark or other sign of encoding. When encoding is determined, the encoding is used to continue reading the stream, and processing continues parsing the input as a stream of (Unicode) characters.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be <see langword="null" />.</param>
		/// <param name="baseUri">The base URI for the entity or document being read. This value can be <see langword="null" />.
		///       Security Note   The base URI is used to resolve the relative URI of the XML document. Do not use a base URI from an untrusted source.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is <see langword="null" />.</exception>
		// Token: 0x0600064E RID: 1614 RVA: 0x0001FC9E File Offset: 0x0001DE9E
		public static XmlReader Create(Stream input, XmlReaderSettings settings, string baseUri)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, null, baseUri, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance using the specified stream, settings, and context information for parsing.</summary>
		/// <param name="input">The stream that contains the XML data. The <see cref="T:System.Xml.XmlReader" /> scans the first bytes of the stream looking for a byte order mark or other sign of encoding. When encoding is determined, the encoding is used to continue reading the stream, and processing continues parsing the input as a stream of (Unicode) characters.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be <see langword="null" />.</param>
		/// <param name="inputContext">The context information required to parse the XML fragment. The context information can include the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang and xml:space scope, base URI, and document type definition. This value can be <see langword="null" />.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is <see langword="null" />.</exception>
		// Token: 0x0600064F RID: 1615 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		public static XmlReader Create(Stream input, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, null, string.Empty, inputContext);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance by using the specified text reader.</summary>
		/// <param name="input">The text reader from which to read the XML data. A text reader returns a stream of Unicode characters, so the encoding specified in the XML declaration is not used by the XML reader to decode the data stream.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is <see langword="null" />.</exception>
		// Token: 0x06000650 RID: 1616 RVA: 0x0001FCCE File Offset: 0x0001DECE
		public static XmlReader Create(TextReader input)
		{
			return XmlReader.Create(input, null, string.Empty);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance by using the specified text reader and settings.</summary>
		/// <param name="input">The text reader from which to read the XML data. A text reader returns a stream of Unicode characters, so the encoding specified in the XML declaration isn't used by the XML reader to decode the data stream.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" />. This value can be <see langword="null" />.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is <see langword="null" />.</exception>
		// Token: 0x06000651 RID: 1617 RVA: 0x0001FCDC File Offset: 0x0001DEDC
		public static XmlReader Create(TextReader input, XmlReaderSettings settings)
		{
			return XmlReader.Create(input, settings, string.Empty);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance by using the specified text reader, settings, and base URI.</summary>
		/// <param name="input">The text reader from which to read the XML data. A text reader returns a stream of Unicode characters, so the encoding specified in the XML declaration isn't used by the <see cref="T:System.Xml.XmlReader" /> to decode the data stream.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be <see langword="null" />.</param>
		/// <param name="baseUri">The base URI for the entity or document being read. This value can be <see langword="null" />.
		///       Security Note   The base URI is used to resolve the relative URI of the XML document. Do not use a base URI from an untrusted source.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is <see langword="null" />.</exception>
		// Token: 0x06000652 RID: 1618 RVA: 0x0001FCEA File Offset: 0x0001DEEA
		public static XmlReader Create(TextReader input, XmlReaderSettings settings, string baseUri)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, baseUri, null);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance by using the specified text reader, settings, and context information for parsing.</summary>
		/// <param name="input">The text reader from which to read the XML data. A text reader returns a stream of Unicode characters, so the encoding specified in the XML declaration isn't used by the XML reader to decode the data stream.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" /> instance. This value can be <see langword="null" />.</param>
		/// <param name="inputContext">The context information required to parse the XML fragment. The context information can include the <see cref="T:System.Xml.XmlNameTable" /> to use, encoding, namespace scope, the current xml:lang and xml:space scope, base URI, and document type definition.This value can be <see langword="null" />.</param>
		/// <returns>An object that is used to read the XML data in the stream.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="input" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Xml.XmlReaderSettings.NameTable" />  and <see cref="P:System.Xml.XmlParserContext.NameTable" /> properties both contain values. (Only one of these <see langword="NameTable" /> properties can be set and used).</exception>
		// Token: 0x06000653 RID: 1619 RVA: 0x0001FCFF File Offset: 0x0001DEFF
		public static XmlReader Create(TextReader input, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(input, string.Empty, inputContext);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XmlReader" /> instance by using the specified XML reader and settings.</summary>
		/// <param name="reader">The object that you want to use as the underlying XML reader.</param>
		/// <param name="settings">The settings for the new <see cref="T:System.Xml.XmlReader" /> instance.The conformance level of the <see cref="T:System.Xml.XmlReaderSettings" /> object must either match the conformance level of the underlying reader, or it must be set to <see cref="F:System.Xml.ConformanceLevel.Auto" />.</param>
		/// <returns>An object that is wrapped around the specified <see cref="T:System.Xml.XmlReader" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="reader" /> value is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">If the <see cref="T:System.Xml.XmlReaderSettings" /> object specifies a conformance level that is not consistent with conformance level of the underlying reader.-or-The underlying <see cref="T:System.Xml.XmlReader" /> is in an <see cref="F:System.Xml.ReadState.Error" /> or <see cref="F:System.Xml.ReadState.Closed" /> state.</exception>
		// Token: 0x06000654 RID: 1620 RVA: 0x0001FD18 File Offset: 0x0001DF18
		public static XmlReader Create(XmlReader reader, XmlReaderSettings settings)
		{
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			return settings.CreateReader(reader);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		internal static XmlReader CreateSqlReader(Stream input, XmlReaderSettings settings, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (settings == null)
			{
				settings = new XmlReaderSettings();
			}
			byte[] array = new byte[XmlReader.CalcBufferSize(input)];
			int num = 0;
			int num2;
			do
			{
				num2 = input.Read(array, num, array.Length - num);
				num += num2;
			}
			while (num2 > 0 && num < 2);
			XmlReader xmlReader;
			if (num >= 2 && array[0] == 223 && array[1] == 255)
			{
				if (inputContext != null)
				{
					throw new ArgumentException(Res.GetString("BinaryXml Parser does not support initialization with XmlParserContext."), "inputContext");
				}
				xmlReader = new XmlSqlBinaryReader(input, array, num, string.Empty, settings.CloseInput, settings);
			}
			else
			{
				xmlReader = new XmlTextReaderImpl(input, array, num, settings, null, string.Empty, inputContext, settings.CloseInput);
			}
			if (settings.ValidationType != ValidationType.None)
			{
				xmlReader = settings.AddValidation(xmlReader);
			}
			if (settings.Async)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001FDF8 File Offset: 0x0001DFF8
		internal static int CalcBufferSize(Stream input)
		{
			int num = 4096;
			if (input.CanSeek)
			{
				long length = input.Length;
				if (length < (long)num)
				{
					num = checked((int)length);
				}
				else if (length > 65536L)
				{
					num = 8192;
				}
			}
			return num;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001FE34 File Offset: 0x0001E034
		private object debuggerDisplayProxy
		{
			get
			{
				return new XmlReader.XmlReaderDebuggerDisplayProxy(this);
			}
		}

		/// <summary>Asynchronously gets the value of the current node.</summary>
		/// <returns>The value of the current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000658 RID: 1624 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task<string> GetValueAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously reads the text content at the current position as an <see cref="T:System.Object" />.</summary>
		/// <returns>The text content as the most appropriate common language runtime (CLR) object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000659 RID: 1625 RVA: 0x0001FE44 File Offset: 0x0001E044
		public virtual Task<object> ReadContentAsObjectAsync()
		{
			XmlReader.<ReadContentAsObjectAsync>d__184 <ReadContentAsObjectAsync>d__;
			<ReadContentAsObjectAsync>d__.<>4__this = this;
			<ReadContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadContentAsObjectAsync>d__.<>1__state = -1;
			<ReadContentAsObjectAsync>d__.<>t__builder.Start<XmlReader.<ReadContentAsObjectAsync>d__184>(ref <ReadContentAsObjectAsync>d__);
			return <ReadContentAsObjectAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously reads the text content at the current position as a <see cref="T:System.String" /> object.</summary>
		/// <returns>The text content as a <see cref="T:System.String" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600065A RID: 1626 RVA: 0x0001FE87 File Offset: 0x0001E087
		public virtual Task<string> ReadContentAsStringAsync()
		{
			if (!this.CanReadContentAs())
			{
				throw this.CreateReadContentAsException("ReadContentAsString");
			}
			return this.InternalReadContentAsStringAsync();
		}

		/// <summary>Asynchronously reads the content as an object of the type specified.</summary>
		/// <param name="returnType">The type of the value to be returned.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion.</param>
		/// <returns>The concatenated text content or attribute value converted to the requested type.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600065B RID: 1627 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		public virtual Task<object> ReadContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XmlReader.<ReadContentAsAsync>d__186 <ReadContentAsAsync>d__;
			<ReadContentAsAsync>d__.<>4__this = this;
			<ReadContentAsAsync>d__.returnType = returnType;
			<ReadContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadContentAsAsync>d__.<>1__state = -1;
			<ReadContentAsAsync>d__.<>t__builder.Start<XmlReader.<ReadContentAsAsync>d__186>(ref <ReadContentAsAsync>d__);
			return <ReadContentAsAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously reads the current element and returns the contents as an <see cref="T:System.Object" />.</summary>
		/// <returns>A boxed common language runtime (CLR) object of the most appropriate type. The <see cref="P:System.Xml.XmlReader.ValueType" /> property determines the appropriate CLR type. If the content is typed as a list type, this method returns an array of boxed objects of the appropriate type.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600065C RID: 1628 RVA: 0x0001FEF8 File Offset: 0x0001E0F8
		public virtual Task<object> ReadElementContentAsObjectAsync()
		{
			XmlReader.<ReadElementContentAsObjectAsync>d__187 <ReadElementContentAsObjectAsync>d__;
			<ReadElementContentAsObjectAsync>d__.<>4__this = this;
			<ReadElementContentAsObjectAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadElementContentAsObjectAsync>d__.<>1__state = -1;
			<ReadElementContentAsObjectAsync>d__.<>t__builder.Start<XmlReader.<ReadElementContentAsObjectAsync>d__187>(ref <ReadElementContentAsObjectAsync>d__);
			return <ReadElementContentAsObjectAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously reads the current element and returns the contents as a <see cref="T:System.String" /> object.</summary>
		/// <returns>The element content as a <see cref="T:System.String" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600065D RID: 1629 RVA: 0x0001FF3C File Offset: 0x0001E13C
		public virtual Task<string> ReadElementContentAsStringAsync()
		{
			XmlReader.<ReadElementContentAsStringAsync>d__188 <ReadElementContentAsStringAsync>d__;
			<ReadElementContentAsStringAsync>d__.<>4__this = this;
			<ReadElementContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadElementContentAsStringAsync>d__.<>1__state = -1;
			<ReadElementContentAsStringAsync>d__.<>t__builder.Start<XmlReader.<ReadElementContentAsStringAsync>d__188>(ref <ReadElementContentAsStringAsync>d__);
			return <ReadElementContentAsStringAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously reads the element content as the requested type.</summary>
		/// <param name="returnType">The type of the value to be returned.</param>
		/// <param name="namespaceResolver">An <see cref="T:System.Xml.IXmlNamespaceResolver" /> object that is used to resolve any namespace prefixes related to type conversion.</param>
		/// <returns>The element content converted to the requested typed object.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600065E RID: 1630 RVA: 0x0001FF80 File Offset: 0x0001E180
		public virtual Task<object> ReadElementContentAsAsync(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			XmlReader.<ReadElementContentAsAsync>d__189 <ReadElementContentAsAsync>d__;
			<ReadElementContentAsAsync>d__.<>4__this = this;
			<ReadElementContentAsAsync>d__.returnType = returnType;
			<ReadElementContentAsAsync>d__.namespaceResolver = namespaceResolver;
			<ReadElementContentAsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<object>.Create();
			<ReadElementContentAsAsync>d__.<>1__state = -1;
			<ReadElementContentAsAsync>d__.<>t__builder.Start<XmlReader.<ReadElementContentAsAsync>d__189>(ref <ReadElementContentAsAsync>d__);
			return <ReadElementContentAsAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously reads the next node from the stream.</summary>
		/// <returns>
		///     <see langword="true" /> if the next node was read successfully; <see langword="false" /> if there are no more nodes to read.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x0600065F RID: 1631 RVA: 0x0000349C File Offset: 0x0000169C
		public virtual Task<bool> ReadAsync()
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously skips the children of the current node.</summary>
		/// <returns>The current node.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000660 RID: 1632 RVA: 0x0001FFD3 File Offset: 0x0001E1D3
		public virtual Task SkipAsync()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return AsyncHelper.DoneTask;
			}
			return this.SkipSubtreeAsync();
		}

		/// <summary>Asynchronously reads the content and returns the Base64 decoded binary bytes.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000661 RID: 1633 RVA: 0x0001EBB9 File Offset: 0x0001CDB9
		public virtual Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[]
			{
				"ReadContentAsBase64"
			}));
		}

		/// <summary>Asynchronously reads the element and decodes the <see langword="Base64" /> content.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000662 RID: 1634 RVA: 0x0001EBD8 File Offset: 0x0001CDD8
		public virtual Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[]
			{
				"ReadElementContentAsBase64"
			}));
		}

		/// <summary>Asynchronously reads the content and returns the <see langword="BinHex" /> decoded binary bytes.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000663 RID: 1635 RVA: 0x0001EBF7 File Offset: 0x0001CDF7
		public virtual Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[]
			{
				"ReadContentAsBinHex"
			}));
		}

		/// <summary>Asynchronously reads the element and decodes the <see langword="BinHex" /> content.</summary>
		/// <param name="buffer">The buffer into which to copy the resulting text. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset into the buffer where to start copying the result.</param>
		/// <param name="count">The maximum number of bytes to copy into the buffer. The actual number of bytes copied is returned from this method.</param>
		/// <returns>The number of bytes written to the buffer.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000664 RID: 1636 RVA: 0x0001EC16 File Offset: 0x0001CE16
		public virtual Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("{0} method is not supported on this XmlReader. Use CanReadBinaryContent property to find out if a reader implements it.", new object[]
			{
				"ReadElementContentAsBinHex"
			}));
		}

		/// <summary>Asynchronously reads large streams of text embedded in an XML document.</summary>
		/// <param name="buffer">The array of characters that serves as the buffer to which the text contents are written. This value cannot be <see langword="null" />.</param>
		/// <param name="index">The offset within the buffer where the <see cref="T:System.Xml.XmlReader" /> can start to copy the results.</param>
		/// <param name="count">The maximum number of characters to copy into the buffer. The actual number of characters copied is returned from this method.</param>
		/// <returns>The number of characters read into the buffer. The value zero is returned when there is no more text content.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000665 RID: 1637 RVA: 0x0001EC35 File Offset: 0x0001CE35
		public virtual Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			throw new NotSupportedException(Res.GetString("ReadValueChunk method is not supported on this XmlReader. Use CanReadValueChunk property to find out if an XmlReader implements it."));
		}

		/// <summary>Asynchronously checks whether the current node is a content node. If the node is not a content node, the reader skips ahead to the next content node or end of file.</summary>
		/// <returns>The <see cref="P:System.Xml.XmlReader.NodeType" /> of the current node found by the method or <see langword="XmlNodeType.None" /> if the reader has reached the end of the input stream.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000666 RID: 1638 RVA: 0x0001FFEC File Offset: 0x0001E1EC
		public virtual Task<XmlNodeType> MoveToContentAsync()
		{
			XmlReader.<MoveToContentAsync>d__197 <MoveToContentAsync>d__;
			<MoveToContentAsync>d__.<>4__this = this;
			<MoveToContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<XmlNodeType>.Create();
			<MoveToContentAsync>d__.<>1__state = -1;
			<MoveToContentAsync>d__.<>t__builder.Start<XmlReader.<MoveToContentAsync>d__197>(ref <MoveToContentAsync>d__);
			return <MoveToContentAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously reads all the content, including markup, as a string.</summary>
		/// <returns>All the XML content, including markup, in the current node. If the current node has no children, an empty string is returned.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000667 RID: 1639 RVA: 0x00020030 File Offset: 0x0001E230
		public virtual Task<string> ReadInnerXmlAsync()
		{
			XmlReader.<ReadInnerXmlAsync>d__198 <ReadInnerXmlAsync>d__;
			<ReadInnerXmlAsync>d__.<>4__this = this;
			<ReadInnerXmlAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadInnerXmlAsync>d__.<>1__state = -1;
			<ReadInnerXmlAsync>d__.<>t__builder.Start<XmlReader.<ReadInnerXmlAsync>d__198>(ref <ReadInnerXmlAsync>d__);
			return <ReadInnerXmlAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00020074 File Offset: 0x0001E274
		private Task WriteNodeAsync(XmlWriter xtw, bool defattr)
		{
			XmlReader.<WriteNodeAsync>d__199 <WriteNodeAsync>d__;
			<WriteNodeAsync>d__.<>4__this = this;
			<WriteNodeAsync>d__.xtw = xtw;
			<WriteNodeAsync>d__.defattr = defattr;
			<WriteNodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNodeAsync>d__.<>1__state = -1;
			<WriteNodeAsync>d__.<>t__builder.Start<XmlReader.<WriteNodeAsync>d__199>(ref <WriteNodeAsync>d__);
			return <WriteNodeAsync>d__.<>t__builder.Task;
		}

		/// <summary>Asynchronously reads the content, including markup, representing this node and all its children.</summary>
		/// <returns>If the reader is positioned on an element or an attribute node, this method returns all the XML content, including markup, of the current node and all its children; otherwise, it returns an empty string.</returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> method was called before a previous asynchronous operation finished. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “An asynchronous operation is already in progress.”</exception>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Xml.XmlReader" /> asynchronous method was called without setting the <see cref="P:System.Xml.XmlReaderSettings.Async" /> flag to <see langword="true" />. In this case, <see cref="T:System.InvalidOperationException" /> is thrown with the message “Set XmlReaderSettings.Async to true if you want to use Async Methods.”</exception>
		// Token: 0x06000669 RID: 1641 RVA: 0x000200C8 File Offset: 0x0001E2C8
		public virtual Task<string> ReadOuterXmlAsync()
		{
			XmlReader.<ReadOuterXmlAsync>d__200 <ReadOuterXmlAsync>d__;
			<ReadOuterXmlAsync>d__.<>4__this = this;
			<ReadOuterXmlAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadOuterXmlAsync>d__.<>1__state = -1;
			<ReadOuterXmlAsync>d__.<>t__builder.Start<XmlReader.<ReadOuterXmlAsync>d__200>(ref <ReadOuterXmlAsync>d__);
			return <ReadOuterXmlAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0002010C File Offset: 0x0001E30C
		private Task<bool> SkipSubtreeAsync()
		{
			XmlReader.<SkipSubtreeAsync>d__201 <SkipSubtreeAsync>d__;
			<SkipSubtreeAsync>d__.<>4__this = this;
			<SkipSubtreeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SkipSubtreeAsync>d__.<>1__state = -1;
			<SkipSubtreeAsync>d__.<>t__builder.Start<XmlReader.<SkipSubtreeAsync>d__201>(ref <SkipSubtreeAsync>d__);
			return <SkipSubtreeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00020150 File Offset: 0x0001E350
		internal Task<string> InternalReadContentAsStringAsync()
		{
			XmlReader.<InternalReadContentAsStringAsync>d__202 <InternalReadContentAsStringAsync>d__;
			<InternalReadContentAsStringAsync>d__.<>4__this = this;
			<InternalReadContentAsStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<InternalReadContentAsStringAsync>d__.<>1__state = -1;
			<InternalReadContentAsStringAsync>d__.<>t__builder.Start<XmlReader.<InternalReadContentAsStringAsync>d__202>(ref <InternalReadContentAsStringAsync>d__);
			return <InternalReadContentAsStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00020194 File Offset: 0x0001E394
		private Task<bool> SetupReadElementContentAsXxxAsync(string methodName)
		{
			XmlReader.<SetupReadElementContentAsXxxAsync>d__203 <SetupReadElementContentAsXxxAsync>d__;
			<SetupReadElementContentAsXxxAsync>d__.<>4__this = this;
			<SetupReadElementContentAsXxxAsync>d__.methodName = methodName;
			<SetupReadElementContentAsXxxAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SetupReadElementContentAsXxxAsync>d__.<>1__state = -1;
			<SetupReadElementContentAsXxxAsync>d__.<>t__builder.Start<XmlReader.<SetupReadElementContentAsXxxAsync>d__203>(ref <SetupReadElementContentAsXxxAsync>d__);
			return <SetupReadElementContentAsXxxAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000201E0 File Offset: 0x0001E3E0
		private Task FinishReadElementContentAsXxxAsync()
		{
			if (this.NodeType != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.NodeType.ToString());
			}
			return this.ReadAsync();
		}

		/// <summary>Initializes a new instance of the <see langword="XmlReader" /> class.</summary>
		// Token: 0x0600066E RID: 1646 RVA: 0x0000216B File Offset: 0x0000036B
		protected XmlReader()
		{
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0002021C File Offset: 0x0001E41C
		// Note: this type is marked as 'beforefieldinit'.
		static XmlReader()
		{
		}

		// Token: 0x0400083C RID: 2108
		private static uint IsTextualNodeBitmap = 24600U;

		// Token: 0x0400083D RID: 2109
		private static uint CanReadContentAsBitmap = 123324U;

		// Token: 0x0400083E RID: 2110
		private static uint HasValueBitmap = 157084U;

		// Token: 0x0400083F RID: 2111
		internal const int DefaultBufferSize = 4096;

		// Token: 0x04000840 RID: 2112
		internal const int BiggerBufferSize = 8192;

		// Token: 0x04000841 RID: 2113
		internal const int MaxStreamLengthForDefaultBufferSize = 65536;

		// Token: 0x04000842 RID: 2114
		internal const int AsyncBufferSize = 65536;

		// Token: 0x0200009C RID: 156
		[DebuggerDisplay("{ToString()}")]
		private struct XmlReaderDebuggerDisplayProxy
		{
			// Token: 0x06000670 RID: 1648 RVA: 0x0002023C File Offset: 0x0001E43C
			internal XmlReaderDebuggerDisplayProxy(XmlReader reader)
			{
				this.reader = reader;
			}

			// Token: 0x06000671 RID: 1649 RVA: 0x00020248 File Offset: 0x0001E448
			public override string ToString()
			{
				XmlNodeType nodeType = this.reader.NodeType;
				string text = nodeType.ToString();
				switch (nodeType)
				{
				case XmlNodeType.Element:
				case XmlNodeType.EntityReference:
				case XmlNodeType.EndElement:
				case XmlNodeType.EndEntity:
					text = text + ", Name=\"" + this.reader.Name + "\"";
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.ProcessingInstruction:
					text = string.Concat(new string[]
					{
						text,
						", Name=\"",
						this.reader.Name,
						"\", Value=\"",
						XmlConvert.EscapeValueForDebuggerDisplay(this.reader.Value),
						"\""
					});
					break;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Comment:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
				case XmlNodeType.XmlDeclaration:
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.reader.Value) + "\"";
					break;
				case XmlNodeType.DocumentType:
					text = text + ", Name=\"" + this.reader.Name + "'";
					text = text + ", SYSTEM=\"" + this.reader.GetAttribute("SYSTEM") + "\"";
					text = text + ", PUBLIC=\"" + this.reader.GetAttribute("PUBLIC") + "\"";
					text = text + ", Value=\"" + XmlConvert.EscapeValueForDebuggerDisplay(this.reader.Value) + "\"";
					break;
				}
				return text;
			}

			// Token: 0x04000843 RID: 2115
			private XmlReader reader;
		}

		// Token: 0x0200009D RID: 157
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsObjectAsync>d__184 : IAsyncStateMachine
		{
			// Token: 0x06000672 RID: 1650 RVA: 0x000203D4 File Offset: 0x0001E5D4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				object result;
				try
				{
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (!xmlReader.CanReadContentAs())
						{
							throw xmlReader.CreateReadContentAsException("ReadContentAsObject");
						}
						awaiter = xmlReader.InternalReadContentAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlReader.<ReadContentAsObjectAsync>d__184>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x000204A8 File Offset: 0x0001E6A8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000844 RID: 2116
			public int <>1__state;

			// Token: 0x04000845 RID: 2117
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x04000846 RID: 2118
			public XmlReader <>4__this;

			// Token: 0x04000847 RID: 2119
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsAsync>d__186 : IAsyncStateMachine
		{
			// Token: 0x06000674 RID: 1652 RVA: 0x000204B8 File Offset: 0x0001E6B8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				object result2;
				try
				{
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (!xmlReader.CanReadContentAs())
						{
							throw xmlReader.CreateReadContentAsException("ReadContentAs");
						}
						awaiter = xmlReader.InternalReadContentAsStringAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlReader.<ReadContentAsAsync>d__186>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					string result = awaiter.GetResult();
					if (this.returnType == typeof(string))
					{
						result2 = result;
					}
					else
					{
						try
						{
							result2 = XmlUntypedConverter.Untyped.ChangeType(result, this.returnType, (this.namespaceResolver == null) ? (xmlReader as IXmlNamespaceResolver) : this.namespaceResolver);
						}
						catch (FormatException innerException)
						{
							throw new XmlException("Content cannot be converted to the type {0}.", this.returnType.ToString(), innerException, xmlReader as IXmlLineInfo);
						}
						catch (InvalidCastException innerException2)
						{
							throw new XmlException("Content cannot be converted to the type {0}.", this.returnType.ToString(), innerException2, xmlReader as IXmlLineInfo);
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x00020658 File Offset: 0x0001E858
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000848 RID: 2120
			public int <>1__state;

			// Token: 0x04000849 RID: 2121
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x0400084A RID: 2122
			public XmlReader <>4__this;

			// Token: 0x0400084B RID: 2123
			public Type returnType;

			// Token: 0x0400084C RID: 2124
			public IXmlNamespaceResolver namespaceResolver;

			// Token: 0x0400084D RID: 2125
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsObjectAsync>d__187 : IAsyncStateMachine
		{
			// Token: 0x06000676 RID: 1654 RVA: 0x00020668 File Offset: 0x0001E868
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				object empty;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_F0;
					case 2:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_15F;
					default:
						awaiter = xmlReader.SetupReadElementContentAsXxxAsync("ReadElementContentAsObject").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsObjectAsync>d__187>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (!awaiter.GetResult())
					{
						empty = string.Empty;
						goto IL_190;
					}
					awaiter2 = xmlReader.ReadContentAsObjectAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsObjectAsync>d__187>(ref awaiter2, ref this);
						return;
					}
					IL_F0:
					object result = awaiter2.GetResult();
					this.<value>5__2 = result;
					awaiter3 = xmlReader.FinishReadElementContentAsXxxAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsObjectAsync>d__187>(ref awaiter3, ref this);
						return;
					}
					IL_15F:
					awaiter3.GetResult();
					empty = this.<value>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_190:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(empty);
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x00020838 File Offset: 0x0001EA38
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400084E RID: 2126
			public int <>1__state;

			// Token: 0x0400084F RID: 2127
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x04000850 RID: 2128
			public XmlReader <>4__this;

			// Token: 0x04000851 RID: 2129
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000852 RID: 2130
			private object <value>5__2;

			// Token: 0x04000853 RID: 2131
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000854 RID: 2132
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020000A0 RID: 160
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsStringAsync>d__188 : IAsyncStateMachine
		{
			// Token: 0x06000678 RID: 1656 RVA: 0x00020848 File Offset: 0x0001EA48
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				string empty;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_F0;
					case 2:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_15F;
					default:
						awaiter = xmlReader.SetupReadElementContentAsXxxAsync("ReadElementContentAsString").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsStringAsync>d__188>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (!awaiter.GetResult())
					{
						empty = string.Empty;
						goto IL_190;
					}
					awaiter2 = xmlReader.ReadContentAsStringAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsStringAsync>d__188>(ref awaiter2, ref this);
						return;
					}
					IL_F0:
					string result = awaiter2.GetResult();
					this.<value>5__2 = result;
					awaiter3 = xmlReader.FinishReadElementContentAsXxxAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsStringAsync>d__188>(ref awaiter3, ref this);
						return;
					}
					IL_15F:
					awaiter3.GetResult();
					empty = this.<value>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_190:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(empty);
			}

			// Token: 0x06000679 RID: 1657 RVA: 0x00020A18 File Offset: 0x0001EC18
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000855 RID: 2133
			public int <>1__state;

			// Token: 0x04000856 RID: 2134
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000857 RID: 2135
			public XmlReader <>4__this;

			// Token: 0x04000858 RID: 2136
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000859 RID: 2137
			private string <value>5__2;

			// Token: 0x0400085A RID: 2138
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x0400085B RID: 2139
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020000A1 RID: 161
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsAsync>d__189 : IAsyncStateMachine
		{
			// Token: 0x0600067A RID: 1658 RVA: 0x00020A28 File Offset: 0x0001EC28
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				object result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_FC;
					case 2:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_16E;
					default:
						awaiter = xmlReader.SetupReadElementContentAsXxxAsync("ReadElementContentAs").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsAsync>d__189>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (!awaiter.GetResult())
					{
						result = ((this.returnType == typeof(string)) ? string.Empty : XmlUntypedConverter.Untyped.ChangeType(string.Empty, this.returnType, this.namespaceResolver));
						goto IL_1D3;
					}
					awaiter2 = xmlReader.ReadContentAsAsync(this.returnType, this.namespaceResolver).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsAsync>d__189>(ref awaiter2, ref this);
						return;
					}
					IL_FC:
					object result2 = awaiter2.GetResult();
					this.<value>5__2 = result2;
					awaiter3 = xmlReader.FinishReadElementContentAsXxxAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlReader.<ReadElementContentAsAsync>d__189>(ref awaiter3, ref this);
						return;
					}
					IL_16E:
					awaiter3.GetResult();
					result = this.<value>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1D3:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600067B RID: 1659 RVA: 0x00020C38 File Offset: 0x0001EE38
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400085C RID: 2140
			public int <>1__state;

			// Token: 0x0400085D RID: 2141
			public AsyncTaskMethodBuilder<object> <>t__builder;

			// Token: 0x0400085E RID: 2142
			public XmlReader <>4__this;

			// Token: 0x0400085F RID: 2143
			public Type returnType;

			// Token: 0x04000860 RID: 2144
			public IXmlNamespaceResolver namespaceResolver;

			// Token: 0x04000861 RID: 2145
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000862 RID: 2146
			private object <value>5__2;

			// Token: 0x04000863 RID: 2147
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000864 RID: 2148
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020000A2 RID: 162
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <MoveToContentAsync>d__197 : IAsyncStateMachine
		{
			// Token: 0x0600067C RID: 1660 RVA: 0x00020C48 File Offset: 0x0001EE48
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				XmlNodeType nodeType2;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_B1;
					}
					IL_14:
					XmlNodeType nodeType = xmlReader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.EntityReference:
						break;
					case XmlNodeType.Attribute:
						xmlReader.MoveToElement();
						break;
					default:
						if (nodeType - XmlNodeType.EndElement > 1)
						{
							awaiter = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<MoveToContentAsync>d__197>(ref awaiter, ref this);
								return;
							}
							goto IL_B1;
						}
						break;
					}
					nodeType2 = xmlReader.NodeType;
					goto IL_DF;
					IL_B1:
					if (awaiter.GetResult())
					{
						goto IL_14;
					}
					nodeType2 = xmlReader.NodeType;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_DF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(nodeType2);
			}

			// Token: 0x0600067D RID: 1661 RVA: 0x00020D58 File Offset: 0x0001EF58
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000865 RID: 2149
			public int <>1__state;

			// Token: 0x04000866 RID: 2150
			public AsyncTaskMethodBuilder<XmlNodeType> <>t__builder;

			// Token: 0x04000867 RID: 2151
			public XmlReader <>4__this;

			// Token: 0x04000868 RID: 2152
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000A3 RID: 163
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadInnerXmlAsync>d__198 : IAsyncStateMachine
		{
			// Token: 0x0600067E RID: 1662 RVA: 0x00020D68 File Offset: 0x0001EF68
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				string result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							if (xmlReader.ReadState != ReadState.Interactive)
							{
								result = string.Empty;
								goto IL_1C0;
							}
							if (xmlReader.NodeType != XmlNodeType.Attribute && xmlReader.NodeType != XmlNodeType.Element)
							{
								awaiter = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (this.<>1__state = 0);
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<ReadInnerXmlAsync>d__198>(ref awaiter, ref this);
									return;
								}
								goto IL_9C;
							}
							else
							{
								this.<sw>5__2 = new StringWriter(CultureInfo.InvariantCulture);
								this.<xtw>5__3 = xmlReader.CreateWriterForInnerOuterXml(this.<sw>5__2);
							}
						}
						try
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
							if (num != 1)
							{
								if (xmlReader.NodeType == XmlNodeType.Attribute)
								{
									((XmlTextWriter)this.<xtw>5__3).QuoteChar = xmlReader.QuoteChar;
									xmlReader.WriteAttributeValue(this.<xtw>5__3);
								}
								if (xmlReader.NodeType != XmlNodeType.Element)
								{
									goto IL_179;
								}
								awaiter2 = xmlReader.WriteNodeAsync(this.<xtw>5__3, false).ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									num = (this.<>1__state = 1);
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlReader.<ReadInnerXmlAsync>d__198>(ref awaiter2, ref this);
									return;
								}
							}
							else
							{
								awaiter2 = this.<>u__2;
								this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
							}
							awaiter2.GetResult();
							IL_179:;
						}
						finally
						{
							if (num < 0)
							{
								this.<xtw>5__3.Close();
							}
						}
						result = this.<sw>5__2.ToString();
						goto IL_1C0;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					num = (this.<>1__state = -1);
					IL_9C:
					awaiter.GetResult();
					result = string.Empty;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sw>5__2 = null;
					this.<xtw>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1C0:
				this.<>1__state = -2;
				this.<sw>5__2 = null;
				this.<xtw>5__3 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x00020F8C File Offset: 0x0001F18C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000869 RID: 2153
			public int <>1__state;

			// Token: 0x0400086A RID: 2154
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x0400086B RID: 2155
			public XmlReader <>4__this;

			// Token: 0x0400086C RID: 2156
			private StringWriter <sw>5__2;

			// Token: 0x0400086D RID: 2157
			private XmlWriter <xtw>5__3;

			// Token: 0x0400086E RID: 2158
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x0400086F RID: 2159
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000A4 RID: 164
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteNodeAsync>d__199 : IAsyncStateMachine
		{
			// Token: 0x06000680 RID: 1664 RVA: 0x00020F9C File Offset: 0x0001F19C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1F2;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2FF;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_38F;
					default:
						this.<d>5__2 = ((xmlReader.NodeType == XmlNodeType.None) ? -1 : xmlReader.Depth);
						goto IL_29E;
					}
					IL_165:
					string result = awaiter.GetResult();
					this.<>7__wrap2.WriteString(result);
					this.<>7__wrap2 = null;
					goto IL_29E;
					IL_1F2:
					result = awaiter.GetResult();
					this.<>7__wrap2.WriteWhitespace(result);
					this.<>7__wrap2 = null;
					IL_29E:
					awaiter2 = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<WriteNodeAsync>d__199>(ref awaiter2, ref this);
						return;
					}
					IL_2FF:
					if (!awaiter2.GetResult() || this.<d>5__2 >= xmlReader.Depth)
					{
						if (this.<d>5__2 != xmlReader.Depth || xmlReader.NodeType != XmlNodeType.EndElement)
						{
							goto IL_397;
						}
						awaiter2 = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<WriteNodeAsync>d__199>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						switch (xmlReader.NodeType)
						{
						case XmlNodeType.Element:
							this.xtw.WriteStartElement(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
							((XmlTextWriter)this.xtw).QuoteChar = xmlReader.QuoteChar;
							this.xtw.WriteAttributes(xmlReader, this.defattr);
							if (xmlReader.IsEmptyElement)
							{
								this.xtw.WriteEndElement();
								goto IL_29E;
							}
							goto IL_29E;
						case XmlNodeType.Attribute:
						case XmlNodeType.Entity:
						case XmlNodeType.Document:
						case XmlNodeType.DocumentFragment:
						case XmlNodeType.Notation:
						case XmlNodeType.EndEntity:
							goto IL_29E;
						case XmlNodeType.Text:
							this.<>7__wrap2 = this.xtw;
							awaiter = xmlReader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlReader.<WriteNodeAsync>d__199>(ref awaiter, ref this);
								return;
							}
							goto IL_165;
						case XmlNodeType.CDATA:
							this.xtw.WriteCData(xmlReader.Value);
							goto IL_29E;
						case XmlNodeType.EntityReference:
							this.xtw.WriteEntityRef(xmlReader.Name);
							goto IL_29E;
						case XmlNodeType.ProcessingInstruction:
						case XmlNodeType.XmlDeclaration:
							this.xtw.WriteProcessingInstruction(xmlReader.Name, xmlReader.Value);
							goto IL_29E;
						case XmlNodeType.Comment:
							this.xtw.WriteComment(xmlReader.Value);
							goto IL_29E;
						case XmlNodeType.DocumentType:
							this.xtw.WriteDocType(xmlReader.Name, xmlReader.GetAttribute("PUBLIC"), xmlReader.GetAttribute("SYSTEM"), xmlReader.Value);
							goto IL_29E;
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							this.<>7__wrap2 = this.xtw;
							awaiter = xmlReader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlReader.<WriteNodeAsync>d__199>(ref awaiter, ref this);
								return;
							}
							goto IL_1F2;
						case XmlNodeType.EndElement:
							this.xtw.WriteFullEndElement();
							goto IL_29E;
						default:
							goto IL_29E;
						}
					}
					IL_38F:
					awaiter2.GetResult();
					IL_397:;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000681 RID: 1665 RVA: 0x0002138C File Offset: 0x0001F58C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000870 RID: 2160
			public int <>1__state;

			// Token: 0x04000871 RID: 2161
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000872 RID: 2162
			public XmlReader <>4__this;

			// Token: 0x04000873 RID: 2163
			public XmlWriter xtw;

			// Token: 0x04000874 RID: 2164
			public bool defattr;

			// Token: 0x04000875 RID: 2165
			private int <d>5__2;

			// Token: 0x04000876 RID: 2166
			private XmlWriter <>7__wrap2;

			// Token: 0x04000877 RID: 2167
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000878 RID: 2168
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000A5 RID: 165
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadOuterXmlAsync>d__200 : IAsyncStateMachine
		{
			// Token: 0x06000682 RID: 1666 RVA: 0x0002139C File Offset: 0x0001F59C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				string result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlReader.ReadState != ReadState.Interactive)
						{
							result = string.Empty;
							goto IL_12B;
						}
						if (xmlReader.NodeType == XmlNodeType.Attribute || xmlReader.NodeType == XmlNodeType.Element)
						{
							StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
							XmlWriter xmlWriter = xmlReader.CreateWriterForInnerOuterXml(stringWriter);
							try
							{
								if (xmlReader.NodeType == XmlNodeType.Attribute)
								{
									xmlWriter.WriteStartAttribute(xmlReader.Prefix, xmlReader.LocalName, xmlReader.NamespaceURI);
									xmlReader.WriteAttributeValue(xmlWriter);
									xmlWriter.WriteEndAttribute();
								}
								else
								{
									xmlWriter.WriteNode(xmlReader, false);
								}
							}
							finally
							{
								if (num < 0)
								{
									xmlWriter.Close();
								}
							}
							result = stringWriter.ToString();
							goto IL_12B;
						}
						awaiter = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (this.<>1__state = 0);
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<ReadOuterXmlAsync>d__200>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
					}
					awaiter.GetResult();
					result = string.Empty;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_12B:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x0002151C File Offset: 0x0001F71C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000879 RID: 2169
			public int <>1__state;

			// Token: 0x0400087A RID: 2170
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x0400087B RID: 2171
			public XmlReader <>4__this;

			// Token: 0x0400087C RID: 2172
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000A6 RID: 166
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SkipSubtreeAsync>d__201 : IAsyncStateMachine
		{
			// Token: 0x06000684 RID: 1668 RVA: 0x0002152C File Offset: 0x0001F72C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_A8;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_12A;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_192;
					default:
						xmlReader.MoveToElement();
						if (xmlReader.NodeType == XmlNodeType.Element && !xmlReader.IsEmptyElement)
						{
							this.<depth>5__2 = xmlReader.Depth;
						}
						else
						{
							awaiter = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<SkipSubtreeAsync>d__201>(ref awaiter, ref this);
								return;
							}
							goto IL_192;
						}
						break;
					}
					IL_4A:
					awaiter = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<SkipSubtreeAsync>d__201>(ref awaiter, ref this);
						return;
					}
					IL_A8:
					if (awaiter.GetResult() && this.<depth>5__2 < xmlReader.Depth)
					{
						goto IL_4A;
					}
					if (xmlReader.NodeType != XmlNodeType.EndElement)
					{
						result = false;
						goto IL_1B9;
					}
					awaiter = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<SkipSubtreeAsync>d__201>(ref awaiter, ref this);
						return;
					}
					IL_12A:
					result = awaiter.GetResult();
					goto IL_1B9;
					IL_192:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1B9:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x00021724 File Offset: 0x0001F924
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400087D RID: 2173
			public int <>1__state;

			// Token: 0x0400087E RID: 2174
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400087F RID: 2175
			public XmlReader <>4__this;

			// Token: 0x04000880 RID: 2176
			private int <depth>5__2;

			// Token: 0x04000881 RID: 2177
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000A7 RID: 167
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InternalReadContentAsStringAsync>d__202 : IAsyncStateMachine
		{
			// Token: 0x06000686 RID: 1670 RVA: 0x00021734 File Offset: 0x0001F934
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				string result;
				try
				{
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_FC;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1A4;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_248;
					default:
						this.<value>5__2 = string.Empty;
						this.<sb>5__3 = null;
						break;
					}
					IL_32:
					switch (xmlReader.NodeType)
					{
					case XmlNodeType.Attribute:
						result = xmlReader.Value;
						goto IL_29D;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						if (this.<value>5__2.Length == 0)
						{
							awaiter = xmlReader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlReader.<InternalReadContentAsStringAsync>d__202>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							if (this.<sb>5__3 == null)
							{
								this.<sb>5__3 = new StringBuilder();
								this.<sb>5__3.Append(this.<value>5__2);
							}
							this.<>7__wrap3 = this.<sb>5__3;
							awaiter = xmlReader.GetValueAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlReader.<InternalReadContentAsStringAsync>d__202>(ref awaiter, ref this);
								return;
							}
							goto IL_1A4;
						}
						break;
					case XmlNodeType.EntityReference:
						if (xmlReader.CanResolveEntity)
						{
							xmlReader.ResolveEntity();
							goto IL_1D5;
						}
						goto IL_258;
					case XmlNodeType.Entity:
					case XmlNodeType.Document:
					case XmlNodeType.DocumentType:
					case XmlNodeType.DocumentFragment:
					case XmlNodeType.Notation:
					case XmlNodeType.EndElement:
						goto IL_258;
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.EndEntity:
						goto IL_1D5;
					default:
						goto IL_258;
					}
					IL_FC:
					string result2 = awaiter.GetResult();
					this.<value>5__2 = result2;
					goto IL_1D5;
					IL_1A4:
					result2 = awaiter.GetResult();
					this.<>7__wrap3.Append(result2);
					this.<>7__wrap3 = null;
					IL_1D5:
					bool flag;
					if (xmlReader.AttributeCount != 0)
					{
						flag = xmlReader.ReadAttributeValue();
						goto IL_251;
					}
					awaiter2 = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<InternalReadContentAsStringAsync>d__202>(ref awaiter2, ref this);
						return;
					}
					IL_248:
					flag = awaiter2.GetResult();
					IL_251:
					if (flag)
					{
						goto IL_32;
					}
					IL_258:
					result = ((this.<sb>5__3 == null) ? this.<value>5__2 : this.<sb>5__3.ToString());
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<value>5__2 = null;
					this.<sb>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_29D:
				this.<>1__state = -2;
				this.<value>5__2 = null;
				this.<sb>5__3 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000687 RID: 1671 RVA: 0x00021A1C File Offset: 0x0001FC1C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000882 RID: 2178
			public int <>1__state;

			// Token: 0x04000883 RID: 2179
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000884 RID: 2180
			public XmlReader <>4__this;

			// Token: 0x04000885 RID: 2181
			private string <value>5__2;

			// Token: 0x04000886 RID: 2182
			private StringBuilder <sb>5__3;

			// Token: 0x04000887 RID: 2183
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000888 RID: 2184
			private StringBuilder <>7__wrap3;

			// Token: 0x04000889 RID: 2185
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000A8 RID: 168
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SetupReadElementContentAsXxxAsync>d__203 : IAsyncStateMachine
		{
			// Token: 0x06000688 RID: 1672 RVA: 0x00021A2C File Offset: 0x0001FC2C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlReader xmlReader = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_11C;
						}
						if (xmlReader.NodeType != XmlNodeType.Element)
						{
							throw xmlReader.CreateReadElementContentAsException(this.methodName);
						}
						this.<isEmptyElement>5__2 = xmlReader.IsEmptyElement;
						awaiter = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<SetupReadElementContentAsXxxAsync>d__203>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					if (this.<isEmptyElement>5__2)
					{
						result = false;
						goto IL_15F;
					}
					XmlNodeType nodeType = xmlReader.NodeType;
					if (nodeType == XmlNodeType.EndElement)
					{
						awaiter = xmlReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlReader.<SetupReadElementContentAsXxxAsync>d__203>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (nodeType == XmlNodeType.Element)
						{
							throw new XmlException("ReadElementContentAs() methods cannot be called on an element that has child elements.", string.Empty, xmlReader as IXmlLineInfo);
						}
						result = true;
						goto IL_15F;
					}
					IL_11C:
					awaiter.GetResult();
					result = false;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_15F:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000689 RID: 1673 RVA: 0x00021BC8 File Offset: 0x0001FDC8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400088A RID: 2186
			public int <>1__state;

			// Token: 0x0400088B RID: 2187
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x0400088C RID: 2188
			public XmlReader <>4__this;

			// Token: 0x0400088D RID: 2189
			public string methodName;

			// Token: 0x0400088E RID: 2190
			private bool <isEmptyElement>5__2;

			// Token: 0x0400088F RID: 2191
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
