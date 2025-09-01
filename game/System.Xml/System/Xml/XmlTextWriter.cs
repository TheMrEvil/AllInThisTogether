using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents a writer that provides a fast, non-cached, forward-only way of generating streams or files containing XML data that conforms to the W3C Extensible Markup Language (XML) 1.0 and the Namespaces in XML recommendations. Starting with the .NET Framework 2.0, we recommend that you use the <see cref="T:System.Xml.XmlWriter" /> class instead.</summary>
	// Token: 0x02000117 RID: 279
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class XmlTextWriter : XmlWriter
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x00042E40 File Offset: 0x00041040
		internal XmlTextWriter()
		{
			this.namespaces = true;
			this.formatting = Formatting.None;
			this.indentation = 2;
			this.indentChar = ' ';
			this.nsStack = new XmlTextWriter.Namespace[8];
			this.nsTop = -1;
			this.stack = new XmlTextWriter.TagInfo[10];
			this.top = 0;
			this.stack[this.top].Init(-1);
			this.quoteChar = '"';
			this.stateTable = XmlTextWriter.stateTableDefault;
			this.currentState = XmlTextWriter.State.Start;
			this.lastToken = XmlTextWriter.Token.Empty;
		}

		/// <summary>Creates an instance of the <see langword="XmlTextWriter" /> class using the specified stream and encoding.</summary>
		/// <param name="w">The stream to which you want to write. </param>
		/// <param name="encoding">The encoding to generate. If encoding is <see langword="null" /> it writes out the stream as UTF-8 and omits the encoding attribute from the <see langword="ProcessingInstruction" />. </param>
		/// <exception cref="T:System.ArgumentException">The encoding is not supported or the stream cannot be written to. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="w" /> is <see langword="null" />. </exception>
		// Token: 0x06000A2E RID: 2606 RVA: 0x00042EDC File Offset: 0x000410DC
		public XmlTextWriter(Stream w, Encoding encoding) : this()
		{
			this.encoding = encoding;
			if (encoding != null)
			{
				this.textWriter = new StreamWriter(w, encoding);
			}
			else
			{
				this.textWriter = new StreamWriter(w);
			}
			this.xmlEncoder = new XmlTextEncoder(this.textWriter);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Xml.XmlTextWriter" /> class using the specified file.</summary>
		/// <param name="filename">The filename to write to. If the file exists, it truncates it and overwrites it with the new content. </param>
		/// <param name="encoding">The encoding to generate. If encoding is <see langword="null" /> it writes the file out as UTF-8, and omits the encoding attribute from the <see langword="ProcessingInstruction" />. </param>
		/// <exception cref="T:System.ArgumentException">The encoding is not supported; the filename is empty, contains only white space, or contains one or more invalid characters. </exception>
		/// <exception cref="T:System.UnauthorizedAccessException">Access is denied. </exception>
		/// <exception cref="T:System.ArgumentNullException">The filename is <see langword="null" />. </exception>
		/// <exception cref="T:System.IO.DirectoryNotFoundException">The directory to write to is not found. </exception>
		/// <exception cref="T:System.IO.IOException">The filename includes an incorrect or invalid syntax for file name, directory name, or volume label syntax. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06000A2F RID: 2607 RVA: 0x00042F36 File Offset: 0x00041136
		public XmlTextWriter(string filename, Encoding encoding) : this(new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read), encoding)
		{
		}

		/// <summary>Creates an instance of the <see langword="XmlTextWriter" /> class using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="w">The <see langword="TextWriter" /> to write to. It is assumed that the <see langword="TextWriter" /> is already set to the correct encoding. </param>
		// Token: 0x06000A30 RID: 2608 RVA: 0x00042F48 File Offset: 0x00041148
		public XmlTextWriter(TextWriter w) : this()
		{
			this.textWriter = w;
			this.encoding = w.Encoding;
			this.xmlEncoder = new XmlTextEncoder(w);
			this.xmlEncoder.QuoteChar = this.quoteChar;
		}

		/// <summary>Gets the underlying stream object.</summary>
		/// <returns>The stream to which the <see langword="XmlTextWriter" /> is writing or <see langword="null" /> if the <see langword="XmlTextWriter" /> was constructed using a <see cref="T:System.IO.TextWriter" /> that does not inherit from the <see cref="T:System.IO.StreamWriter" /> class.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00042F80 File Offset: 0x00041180
		public Stream BaseStream
		{
			get
			{
				StreamWriter streamWriter = this.textWriter as StreamWriter;
				if (streamWriter != null)
				{
					return streamWriter.BaseStream;
				}
				return null;
			}
		}

		/// <summary>Gets or sets a value indicating whether to do namespace support.</summary>
		/// <returns>
		///     <see langword="true" /> to support namespaces; otherwise, <see langword="false" />.The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">You can only change this property when in the <see langword="WriteState.Start" /> state. </exception>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00042FA4 File Offset: 0x000411A4
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x00042FAC File Offset: 0x000411AC
		public bool Namespaces
		{
			get
			{
				return this.namespaces;
			}
			set
			{
				if (this.currentState != XmlTextWriter.State.Start)
				{
					throw new InvalidOperationException(Res.GetString("NotInWriteState."));
				}
				this.namespaces = value;
			}
		}

		/// <summary>Indicates how the output is formatted.</summary>
		/// <returns>One of the <see cref="T:System.Xml.Formatting" /> values. The default is <see langword="Formatting.None" /> (no special formatting).</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x00042FCD File Offset: 0x000411CD
		// (set) Token: 0x06000A35 RID: 2613 RVA: 0x00042FD5 File Offset: 0x000411D5
		public Formatting Formatting
		{
			get
			{
				return this.formatting;
			}
			set
			{
				this.formatting = value;
				this.indented = (value == Formatting.Indented);
			}
		}

		/// <summary>Gets or sets how many IndentChars to write for each level in the hierarchy when <see cref="P:System.Xml.XmlTextWriter.Formatting" /> is set to <see langword="Formatting.Indented" />.</summary>
		/// <returns>Number of <see langword="IndentChars" /> for each level. The default is 2.</returns>
		/// <exception cref="T:System.ArgumentException">Setting this property to a negative value. </exception>
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x00042FE8 File Offset: 0x000411E8
		// (set) Token: 0x06000A37 RID: 2615 RVA: 0x00042FF0 File Offset: 0x000411F0
		public int Indentation
		{
			get
			{
				return this.indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Res.GetString("Indentation value must be greater than 0."));
				}
				this.indentation = value;
			}
		}

		/// <summary>Gets or sets which character to use for indenting when <see cref="P:System.Xml.XmlTextWriter.Formatting" /> is set to <see langword="Formatting.Indented" />.</summary>
		/// <returns>The character to use for indenting. The default is space.The <see langword="XmlTextWriter" /> allows you to set this property to any character. To ensure valid XML, you must specify a valid white space character, 0x9, 0x10, 0x13 or 0x20.</returns>
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x0004300D File Offset: 0x0004120D
		// (set) Token: 0x06000A39 RID: 2617 RVA: 0x00043015 File Offset: 0x00041215
		public char IndentChar
		{
			get
			{
				return this.indentChar;
			}
			set
			{
				this.indentChar = value;
			}
		}

		/// <summary>Gets or sets which character to use to quote attribute values.</summary>
		/// <returns>The character to use to quote attribute values. This must be a single quote (&amp;#39;) or a double quote (&amp;#34;). The default is a double quote.</returns>
		/// <exception cref="T:System.ArgumentException">Setting this property to something other than either a single or double quote. </exception>
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x0004301E File Offset: 0x0004121E
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x00043026 File Offset: 0x00041226
		public char QuoteChar
		{
			get
			{
				return this.quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException(Res.GetString("Invalid XML attribute quote character. Valid attribute quote characters are ' and \"."));
				}
				this.quoteChar = value;
				this.xmlEncoder.QuoteChar = value;
			}
		}

		/// <summary>Writes the XML declaration with the version "1.0".</summary>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000A3C RID: 2620 RVA: 0x00043055 File Offset: 0x00041255
		public override void WriteStartDocument()
		{
			this.StartDocument(-1);
		}

		/// <summary>Writes the XML declaration with the version "1.0" and the standalone attribute.</summary>
		/// <param name="standalone">If <see langword="true" />, it writes "standalone=yes"; if <see langword="false" />, it writes "standalone=no". </param>
		/// <exception cref="T:System.InvalidOperationException">This is not the first write method called after the constructor. </exception>
		// Token: 0x06000A3D RID: 2621 RVA: 0x0004305E File Offset: 0x0004125E
		public override void WriteStartDocument(bool standalone)
		{
			this.StartDocument(standalone ? 1 : 0);
		}

		/// <summary>Closes any open elements or attributes and puts the writer back in the Start state.</summary>
		/// <exception cref="T:System.ArgumentException">The XML document is invalid. </exception>
		// Token: 0x06000A3E RID: 2622 RVA: 0x00043070 File Offset: 0x00041270
		public override void WriteEndDocument()
		{
			try
			{
				this.AutoCompleteAll();
				if (this.currentState != XmlTextWriter.State.Epilog)
				{
					if (this.currentState == XmlTextWriter.State.Closed)
					{
						throw new ArgumentException(Res.GetString("The Writer is closed or in error state."));
					}
					throw new ArgumentException(Res.GetString("Document does not have a root element."));
				}
				else
				{
					this.stateTable = XmlTextWriter.stateTableDefault;
					this.currentState = XmlTextWriter.State.Start;
					this.lastToken = XmlTextWriter.Token.Empty;
				}
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the DOCTYPE declaration with the specified name and optional attributes.</summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty. </param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid" /> and <paramref name="sysid" /> are replaced with the value of the given arguments. </param>
		/// <param name="sysid">If <paramref name="pubid" /> is null and <paramref name="sysid" /> is non-null it writes SYSTEM "sysid" where <paramref name="sysid" /> is replaced with the value of this argument. </param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument. </param>
		/// <exception cref="T:System.InvalidOperationException">This method was called outside the prolog (after the root element). </exception>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="name" /> is <see langword="null" /> or <see langword="String.Empty" />-or- the value for <paramref name="name" /> would result in invalid XML. </exception>
		// Token: 0x06000A3F RID: 2623 RVA: 0x000430EC File Offset: 0x000412EC
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			try
			{
				this.ValidateName(name, false);
				this.AutoComplete(XmlTextWriter.Token.Doctype);
				this.textWriter.Write("<!DOCTYPE ");
				this.textWriter.Write(name);
				if (pubid != null)
				{
					this.textWriter.Write(" PUBLIC " + this.quoteChar.ToString());
					this.textWriter.Write(pubid);
					this.textWriter.Write(this.quoteChar.ToString() + " " + this.quoteChar.ToString());
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				else if (sysid != null)
				{
					this.textWriter.Write(" SYSTEM " + this.quoteChar.ToString());
					this.textWriter.Write(sysid);
					this.textWriter.Write(this.quoteChar);
				}
				if (subset != null)
				{
					this.textWriter.Write("[");
					this.textWriter.Write(subset);
					this.textWriter.Write("]");
				}
				this.textWriter.Write('>');
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the specified start tag and associates it with the given namespace and prefix.</summary>
		/// <param name="prefix">The namespace prefix of the element. </param>
		/// <param name="localName">The local name of the element. </param>
		/// <param name="ns">The namespace URI to associate with the element. If this namespace is already in scope and has an associated prefix then the writer automatically writes that prefix also. </param>
		/// <exception cref="T:System.InvalidOperationException">The writer is closed. </exception>
		// Token: 0x06000A40 RID: 2624 RVA: 0x00043244 File Offset: 0x00041444
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.StartElement);
				this.PushStack();
				this.textWriter.Write('<');
				if (this.namespaces)
				{
					this.stack[this.top].defaultNs = this.stack[this.top - 1].defaultNs;
					if (this.stack[this.top - 1].defaultNsState != XmlTextWriter.NamespaceState.Uninitialized)
					{
						this.stack[this.top].defaultNsState = XmlTextWriter.NamespaceState.NotDeclaredButInScope;
					}
					this.stack[this.top].mixed = this.stack[this.top - 1].mixed;
					if (ns == null)
					{
						if (prefix != null && prefix.Length != 0 && this.LookupNamespace(prefix) == -1)
						{
							throw new ArgumentException(Res.GetString("An undefined prefix is in use."));
						}
					}
					else if (prefix == null)
					{
						string text = this.FindPrefix(ns);
						if (text != null)
						{
							prefix = text;
						}
						else
						{
							this.PushNamespace(null, ns, false);
						}
					}
					else if (prefix.Length == 0)
					{
						this.PushNamespace(null, ns, false);
					}
					else
					{
						if (ns.Length == 0)
						{
							prefix = null;
						}
						this.VerifyPrefixXml(prefix, ns);
						this.PushNamespace(prefix, ns, false);
					}
					this.stack[this.top].prefix = null;
					if (prefix != null && prefix.Length != 0)
					{
						this.stack[this.top].prefix = prefix;
						this.textWriter.Write(prefix);
						this.textWriter.Write(':');
					}
				}
				else if ((ns != null && ns.Length != 0) || (prefix != null && prefix.Length != 0))
				{
					throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
				}
				this.stack[this.top].name = localName;
				this.textWriter.Write(localName);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000A41 RID: 2625 RVA: 0x00043440 File Offset: 0x00041640
		public override void WriteEndElement()
		{
			this.InternalWriteEndElement(false);
		}

		/// <summary>Closes one element and pops the corresponding namespace scope.</summary>
		// Token: 0x06000A42 RID: 2626 RVA: 0x00043449 File Offset: 0x00041649
		public override void WriteFullEndElement()
		{
			this.InternalWriteEndElement(true);
		}

		/// <summary>Writes the start of an attribute.</summary>
		/// <param name="prefix">
		///       <see langword="Namespace" /> prefix of the attribute. </param>
		/// <param name="localName">
		///       <see langword="LocalName" /> of the attribute. </param>
		/// <param name="ns">
		///       <see langword="NamespaceURI" /> of the attribute </param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="localName" /> is either <see langword="null" /> or <see langword="String.Empty" />. </exception>
		// Token: 0x06000A43 RID: 2627 RVA: 0x00043454 File Offset: 0x00041654
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.StartAttribute);
				this.specialAttr = XmlTextWriter.SpecialAttr.None;
				if (this.namespaces)
				{
					if (prefix != null && prefix.Length == 0)
					{
						prefix = null;
					}
					if (ns == "http://www.w3.org/2000/xmlns/" && prefix == null && localName != "xmlns")
					{
						prefix = "xmlns";
					}
					if (prefix == "xml")
					{
						if (localName == "lang")
						{
							this.specialAttr = XmlTextWriter.SpecialAttr.XmlLang;
						}
						else if (localName == "space")
						{
							this.specialAttr = XmlTextWriter.SpecialAttr.XmlSpace;
						}
					}
					else if (prefix == "xmlns")
					{
						if ("http://www.w3.org/2000/xmlns/" != ns && ns != null)
						{
							throw new ArgumentException(Res.GetString("The 'xmlns' attribute is bound to the reserved namespace 'http://www.w3.org/2000/xmlns/'."));
						}
						if (localName == null || localName.Length == 0)
						{
							localName = prefix;
							prefix = null;
							this.prefixForXmlNs = null;
						}
						else
						{
							this.prefixForXmlNs = localName;
						}
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlNs;
					}
					else if (prefix == null && localName == "xmlns")
					{
						if ("http://www.w3.org/2000/xmlns/" != ns && ns != null)
						{
							throw new ArgumentException(Res.GetString("The 'xmlns' attribute is bound to the reserved namespace 'http://www.w3.org/2000/xmlns/'."));
						}
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlNs;
						this.prefixForXmlNs = null;
					}
					else if (ns == null)
					{
						if (prefix != null && this.LookupNamespace(prefix) == -1)
						{
							throw new ArgumentException(Res.GetString("An undefined prefix is in use."));
						}
					}
					else if (ns.Length == 0)
					{
						prefix = string.Empty;
					}
					else
					{
						this.VerifyPrefixXml(prefix, ns);
						if (prefix != null && this.LookupNamespaceInCurrentScope(prefix) != -1)
						{
							prefix = null;
						}
						string text = this.FindPrefix(ns);
						if (text != null && (prefix == null || prefix == text))
						{
							prefix = text;
						}
						else
						{
							if (prefix == null)
							{
								prefix = this.GeneratePrefix();
							}
							this.PushNamespace(prefix, ns, false);
						}
					}
					if (prefix != null && prefix.Length != 0)
					{
						this.textWriter.Write(prefix);
						this.textWriter.Write(':');
					}
				}
				else
				{
					if ((ns != null && ns.Length != 0) || (prefix != null && prefix.Length != 0))
					{
						throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
					}
					if (localName == "xml:lang")
					{
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlLang;
					}
					else if (localName == "xml:space")
					{
						this.specialAttr = XmlTextWriter.SpecialAttr.XmlSpace;
					}
				}
				this.xmlEncoder.StartAttribute(this.specialAttr > XmlTextWriter.SpecialAttr.None);
				this.textWriter.Write(localName);
				this.textWriter.Write('=');
				if (this.curQuoteChar != this.quoteChar)
				{
					this.curQuoteChar = this.quoteChar;
					this.xmlEncoder.QuoteChar = this.quoteChar;
				}
				this.textWriter.Write(this.curQuoteChar);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Closes the previous <see cref="M:System.Xml.XmlTextWriter.WriteStartAttribute(System.String,System.String,System.String)" /> call.</summary>
		// Token: 0x06000A44 RID: 2628 RVA: 0x00043708 File Offset: 0x00041908
		public override void WriteEndAttribute()
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.EndAttribute);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a &lt;![CDATA[...]]&gt; block containing the specified text.</summary>
		/// <param name="text">Text to place inside the CDATA block. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is <see langword="Closed" />. </exception>
		// Token: 0x06000A45 RID: 2629 RVA: 0x00043738 File Offset: 0x00041938
		public override void WriteCData(string text)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.CData);
				if (text != null && text.IndexOf("]]>", StringComparison.Ordinal) >= 0)
				{
					throw new ArgumentException(Res.GetString("Cannot have ']]>' inside an XML CDATA block."));
				}
				this.textWriter.Write("<![CDATA[");
				if (text != null)
				{
					this.xmlEncoder.WriteRawWithSurrogateChecking(text);
				}
				this.textWriter.Write("]]>");
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a comment &lt;!--...--&gt; containing the specified text.</summary>
		/// <param name="text">Text to place inside the comment. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is <see langword="Closed" />. </exception>
		// Token: 0x06000A46 RID: 2630 RVA: 0x000437BC File Offset: 0x000419BC
		public override void WriteComment(string text)
		{
			try
			{
				if (text != null && (text.IndexOf("--", StringComparison.Ordinal) >= 0 || (text.Length != 0 && text[text.Length - 1] == '-')))
				{
					throw new ArgumentException(Res.GetString("An XML comment cannot contain '--', and '-' cannot be the last character."));
				}
				this.AutoComplete(XmlTextWriter.Token.Comment);
				this.textWriter.Write("<!--");
				if (text != null)
				{
					this.xmlEncoder.WriteRawWithSurrogateChecking(text);
				}
				this.textWriter.Write("-->");
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.</summary>
		/// <param name="name">Name of the processing instruction. </param>
		/// <param name="text">Text to include in the processing instruction. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document.
		///         <paramref name="name" /> is either <see langword="null" /> or <see langword="String.Empty" />.This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlTextWriter.WriteStartDocument" /> has already been called. </exception>
		// Token: 0x06000A47 RID: 2631 RVA: 0x00043858 File Offset: 0x00041A58
		public override void WriteProcessingInstruction(string name, string text)
		{
			try
			{
				if (text != null && text.IndexOf("?>", StringComparison.Ordinal) >= 0)
				{
					throw new ArgumentException(Res.GetString("Cannot have '?>' inside an XML processing instruction."));
				}
				if (string.Compare(name, "xml", StringComparison.OrdinalIgnoreCase) == 0 && this.stateTable == XmlTextWriter.stateTableDocument)
				{
					throw new ArgumentException(Res.GetString("Cannot write XML declaration. WriteStartDocument method has already written it."));
				}
				this.AutoComplete(XmlTextWriter.Token.PI);
				this.InternalWriteProcessingInstruction(name, text);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out an entity reference as <see langword="&amp;name;" />.</summary>
		/// <param name="name">Name of the entity reference. </param>
		/// <exception cref="T:System.ArgumentException">The text would result in a non-well formed XML document or <paramref name="name" /> is either <see langword="null" /> or <see langword="String.Empty" />. </exception>
		// Token: 0x06000A48 RID: 2632 RVA: 0x000438E0 File Offset: 0x00041AE0
		public override void WriteEntityRef(string name)
		{
			try
			{
				this.ValidateName(name, false);
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteEntityRef(name);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Forces the generation of a character entity for the specified Unicode character value.</summary>
		/// <param name="ch">Unicode character for which to generate a character entity. </param>
		/// <exception cref="T:System.ArgumentException">The character is in the surrogate pair character range, <see langword="0xd800" /> - <see langword="0xdfff" />; or the text would result in a non-well formed XML document. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is <see langword="Closed" />. </exception>
		// Token: 0x06000A49 RID: 2633 RVA: 0x00043928 File Offset: 0x00041B28
		public override void WriteCharEntity(char ch)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteCharEntity(ch);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out the given white space.</summary>
		/// <param name="ws">The string of white space characters. </param>
		/// <exception cref="T:System.ArgumentException">The string contains non-white space characters. </exception>
		// Token: 0x06000A4A RID: 2634 RVA: 0x00043968 File Offset: 0x00041B68
		public override void WriteWhitespace(string ws)
		{
			try
			{
				if (ws == null)
				{
					ws = string.Empty;
				}
				if (!this.xmlCharType.IsOnlyWhitespace(ws))
				{
					throw new ArgumentException(Res.GetString("Only white space characters should be used."));
				}
				this.AutoComplete(XmlTextWriter.Token.Whitespace);
				this.xmlEncoder.Write(ws);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes the given text content.</summary>
		/// <param name="text">Text to write. </param>
		/// <exception cref="T:System.ArgumentException">The text string contains an invalid surrogate pair. </exception>
		// Token: 0x06000A4B RID: 2635 RVA: 0x000439D0 File Offset: 0x00041BD0
		public override void WriteString(string text)
		{
			try
			{
				if (text != null && text.Length != 0)
				{
					this.AutoComplete(XmlTextWriter.Token.Content);
					this.xmlEncoder.Write(text);
				}
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Generates and writes the surrogate character entity for the surrogate character pair.</summary>
		/// <param name="lowChar">The low surrogate. This must be a value between <see langword="0xDC00" /> and <see langword="0xDFFF" />. </param>
		/// <param name="highChar">The high surrogate. This must be a value between <see langword="0xD800" /> and <see langword="0xDBFF" />. </param>
		/// <exception cref="T:System.Exception">An invalid surrogate character pair was passed. </exception>
		// Token: 0x06000A4C RID: 2636 RVA: 0x00043A18 File Offset: 0x00041C18
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.WriteSurrogateCharEntity(lowChar, highChar);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes text one buffer at a time.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position in the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="buffer" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> or <paramref name="count" /> is less than zero. -or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />; the call results in surrogate pair characters being split or an invalid surrogate pair being written.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000A4D RID: 2637 RVA: 0x00043A58 File Offset: 0x00041C58
		public override void WriteChars(char[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.xmlEncoder.Write(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes raw markup manually from a character buffer.</summary>
		/// <param name="buffer">Character array containing the text to write. </param>
		/// <param name="index">The position within the buffer indicating the start of the text to write. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="buffer" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		// Token: 0x06000A4E RID: 2638 RVA: 0x00043A98 File Offset: 0x00041C98
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.RawData);
				this.xmlEncoder.WriteRaw(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes raw markup manually from a string.</summary>
		/// <param name="data">String containing the text to write. </param>
		// Token: 0x06000A4F RID: 2639 RVA: 0x00043AD8 File Offset: 0x00041CD8
		public override void WriteRaw(string data)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.RawData);
				this.xmlEncoder.WriteRawWithSurrogateChecking(data);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Encodes the specified binary bytes as base64 and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position within the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="buffer" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is <see langword="Closed" />. </exception>
		// Token: 0x06000A50 RID: 2640 RVA: 0x00043B18 File Offset: 0x00041D18
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			try
			{
				if (!this.flush)
				{
					this.AutoComplete(XmlTextWriter.Token.Base64);
				}
				this.flush = true;
				if (this.base64Encoder == null)
				{
					this.base64Encoder = new XmlTextWriterBase64Encoder(this.xmlEncoder);
				}
				this.base64Encoder.Encode(buffer, index, count);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Encodes the specified binary bytes as binhex and writes out the resulting text.</summary>
		/// <param name="buffer">Byte array to encode. </param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write. </param>
		/// <param name="count">The number of bytes to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///         <paramref name="buffer" /> is <see langword="null" />. </exception>
		/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///         <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Xml.XmlTextWriter.WriteState" /> is Closed. </exception>
		// Token: 0x06000A51 RID: 2641 RVA: 0x00043B80 File Offset: 0x00041D80
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				BinHexEncoder.Encode(buffer, index, count, this);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Gets the state of the writer.</summary>
		/// <returns>One of the <see cref="T:System.Xml.WriteState" /> values.</returns>
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00043BBC File Offset: 0x00041DBC
		public override WriteState WriteState
		{
			get
			{
				switch (this.currentState)
				{
				case XmlTextWriter.State.Start:
					return WriteState.Start;
				case XmlTextWriter.State.Prolog:
				case XmlTextWriter.State.PostDTD:
					return WriteState.Prolog;
				case XmlTextWriter.State.Element:
					return WriteState.Element;
				case XmlTextWriter.State.Attribute:
				case XmlTextWriter.State.AttrOnly:
					return WriteState.Attribute;
				case XmlTextWriter.State.Content:
				case XmlTextWriter.State.Epilog:
					return WriteState.Content;
				case XmlTextWriter.State.Error:
					return WriteState.Error;
				case XmlTextWriter.State.Closed:
					return WriteState.Closed;
				default:
					return WriteState.Error;
				}
			}
		}

		/// <summary>Closes this stream and the underlying stream.</summary>
		// Token: 0x06000A53 RID: 2643 RVA: 0x00043C10 File Offset: 0x00041E10
		public override void Close()
		{
			try
			{
				this.AutoCompleteAll();
			}
			catch
			{
			}
			finally
			{
				this.currentState = XmlTextWriter.State.Closed;
				this.textWriter.Close();
			}
		}

		/// <summary>Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.</summary>
		// Token: 0x06000A54 RID: 2644 RVA: 0x00043C58 File Offset: 0x00041E58
		public override void Flush()
		{
			this.textWriter.Flush();
		}

		/// <summary>Writes out the specified name, ensuring it is a valid name according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">Name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="name" /> is not a valid XML name; or <paramref name="name" /> is either <see langword="null" /> or <see langword="String.Empty" />. </exception>
		// Token: 0x06000A55 RID: 2645 RVA: 0x00043C68 File Offset: 0x00041E68
		public override void WriteName(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				this.InternalWriteName(name, false);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Writes out the namespace-qualified name. This method looks up the prefix that is in scope for the given namespace.</summary>
		/// <param name="localName">The local name to write. </param>
		/// <param name="ns">The namespace URI to associate with the name. </param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="localName" /> is either <see langword="null" /> or <see langword="String.Empty" />.
		///         <paramref name="localName" /> is not a valid name according to the W3C Namespaces spec. </exception>
		// Token: 0x06000A56 RID: 2646 RVA: 0x00043CA4 File Offset: 0x00041EA4
		public override void WriteQualifiedName(string localName, string ns)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (this.namespaces)
				{
					if (ns != null && ns.Length != 0 && ns != this.stack[this.top].defaultNs)
					{
						string text = this.FindPrefix(ns);
						if (text == null)
						{
							if (this.currentState != XmlTextWriter.State.Attribute)
							{
								throw new ArgumentException(Res.GetString("The '{0}' namespace is not defined.", new object[]
								{
									ns
								}));
							}
							text = this.GeneratePrefix();
							this.PushNamespace(text, ns, false);
						}
						if (text.Length != 0)
						{
							this.InternalWriteName(text, true);
							this.textWriter.Write(':');
						}
					}
				}
				else if (ns != null && ns.Length != 0)
				{
					throw new ArgumentException(Res.GetString("Cannot set the namespace if Namespaces is 'false'."));
				}
				this.InternalWriteName(localName, true);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		/// <summary>Returns the closest prefix defined in the current namespace scope for the namespace URI.</summary>
		/// <param name="ns">Namespace URI whose prefix you want to find. </param>
		/// <returns>The matching prefix. Or <see langword="null" /> if no matching namespace URI is found in the current scope.</returns>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="ns" /> is either <see langword="null" /> or <see langword="String.Empty" />. </exception>
		// Token: 0x06000A57 RID: 2647 RVA: 0x00043D90 File Offset: 0x00041F90
		public override string LookupPrefix(string ns)
		{
			if (ns == null || ns.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			string text = this.FindPrefix(ns);
			if (text == null && ns == this.stack[this.top].defaultNs)
			{
				text = string.Empty;
			}
			return text;
		}

		/// <summary>Gets an <see cref="T:System.Xml.XmlSpace" /> representing the current <see langword="xml:space" /> scope.</summary>
		/// <returns>An <see langword="XmlSpace" /> representing the current <see langword="xml:space" /> scope.Value Meaning None This is the default if no <see langword="xml:space" /> scope exists. Default The current scope is <see langword="xml:space" />="default". Preserve The current scope is <see langword="xml:space" />="preserve". </returns>
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00043DE8 File Offset: 0x00041FE8
		public override XmlSpace XmlSpace
		{
			get
			{
				for (int i = this.top; i > 0; i--)
				{
					XmlSpace xmlSpace = this.stack[i].xmlSpace;
					if (xmlSpace != XmlSpace.None)
					{
						return xmlSpace;
					}
				}
				return XmlSpace.None;
			}
		}

		/// <summary>Gets the current <see langword="xml:lang" /> scope.</summary>
		/// <returns>The current <see langword="xml:lang" /> or <see langword="null" /> if there is no <see langword="xml:lang" /> in the current scope.</returns>
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00043E20 File Offset: 0x00042020
		public override string XmlLang
		{
			get
			{
				for (int i = this.top; i > 0; i--)
				{
					string xmlLang = this.stack[i].xmlLang;
					if (xmlLang != null)
					{
						return xmlLang;
					}
				}
				return null;
			}
		}

		/// <summary>Writes out the specified name, ensuring it is a valid <see langword="NmToken" /> according to the W3C XML 1.0 recommendation (http://www.w3.org/TR/1998/REC-xml-19980210#NT-Name).</summary>
		/// <param name="name">Name to write. </param>
		/// <exception cref="T:System.ArgumentException">
		///         <paramref name="name" /> is not a valid <see langword="NmToken" />; or <paramref name="name" /> is either <see langword="null" /> or <see langword="String.Empty" />. </exception>
		// Token: 0x06000A5A RID: 2650 RVA: 0x00043E58 File Offset: 0x00042058
		public override void WriteNmToken(string name)
		{
			try
			{
				this.AutoComplete(XmlTextWriter.Token.Content);
				if (name == null || name.Length == 0)
				{
					throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
				}
				if (!ValidateNames.IsNmtokenNoNamespaces(name))
				{
					throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[]
					{
						name
					}));
				}
				this.textWriter.Write(name);
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00043ED4 File Offset: 0x000420D4
		private void StartDocument(int standalone)
		{
			try
			{
				if (this.currentState != XmlTextWriter.State.Start)
				{
					throw new InvalidOperationException(Res.GetString("WriteStartDocument needs to be the first call."));
				}
				this.stateTable = XmlTextWriter.stateTableDocument;
				this.currentState = XmlTextWriter.State.Prolog;
				StringBuilder stringBuilder = new StringBuilder(128);
				stringBuilder.Append("version=" + this.quoteChar.ToString() + "1.0" + this.quoteChar.ToString());
				if (this.encoding != null)
				{
					stringBuilder.Append(" encoding=");
					stringBuilder.Append(this.quoteChar);
					stringBuilder.Append(this.encoding.WebName);
					stringBuilder.Append(this.quoteChar);
				}
				if (standalone >= 0)
				{
					stringBuilder.Append(" standalone=");
					stringBuilder.Append(this.quoteChar);
					stringBuilder.Append((standalone == 0) ? "no" : "yes");
					stringBuilder.Append(this.quoteChar);
				}
				this.InternalWriteProcessingInstruction("xml", stringBuilder.ToString());
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00043FF0 File Offset: 0x000421F0
		private void AutoComplete(XmlTextWriter.Token token)
		{
			if (this.currentState == XmlTextWriter.State.Closed)
			{
				throw new InvalidOperationException(Res.GetString("The Writer is closed."));
			}
			if (this.currentState == XmlTextWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
				{
					XmlTextWriter.tokenName[(int)token],
					XmlTextWriter.stateName[8]
				}));
			}
			XmlTextWriter.State state = this.stateTable[(int)(token * XmlTextWriter.Token.EndAttribute + (int)this.currentState)];
			if (state == XmlTextWriter.State.Error)
			{
				throw new InvalidOperationException(Res.GetString("Token {0} in state {1} would result in an invalid XML document.", new object[]
				{
					XmlTextWriter.tokenName[(int)token],
					XmlTextWriter.stateName[(int)this.currentState]
				}));
			}
			switch (token)
			{
			case XmlTextWriter.Token.PI:
			case XmlTextWriter.Token.Comment:
			case XmlTextWriter.Token.CData:
			case XmlTextWriter.Token.StartElement:
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
					this.WriteEndStartTag(false);
				}
				else if (this.currentState == XmlTextWriter.State.Element)
				{
					this.WriteEndStartTag(false);
				}
				if (token == XmlTextWriter.Token.CData)
				{
					this.stack[this.top].mixed = true;
				}
				else if (this.indented && this.currentState != XmlTextWriter.State.Start)
				{
					this.Indent(false);
				}
				break;
			case XmlTextWriter.Token.Doctype:
				if (this.indented && this.currentState != XmlTextWriter.State.Start)
				{
					this.Indent(false);
				}
				break;
			case XmlTextWriter.Token.EndElement:
			case XmlTextWriter.Token.LongEndElement:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
				}
				if (this.currentState == XmlTextWriter.State.Content)
				{
					token = XmlTextWriter.Token.LongEndElement;
				}
				else
				{
					this.WriteEndStartTag(token == XmlTextWriter.Token.EndElement);
				}
				if (XmlTextWriter.stateTableDocument == this.stateTable && this.top == 1)
				{
					state = XmlTextWriter.State.Epilog;
				}
				break;
			case XmlTextWriter.Token.StartAttribute:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Attribute)
				{
					this.WriteEndAttributeQuote();
					this.textWriter.Write(' ');
				}
				else if (this.currentState == XmlTextWriter.State.Element)
				{
					this.textWriter.Write(' ');
				}
				break;
			case XmlTextWriter.Token.EndAttribute:
				if (this.flush)
				{
					this.FlushEncoders();
				}
				this.WriteEndAttributeQuote();
				break;
			case XmlTextWriter.Token.Content:
			case XmlTextWriter.Token.Base64:
			case XmlTextWriter.Token.RawData:
			case XmlTextWriter.Token.Whitespace:
				if (token != XmlTextWriter.Token.Base64 && this.flush)
				{
					this.FlushEncoders();
				}
				if (this.currentState == XmlTextWriter.State.Element && this.lastToken != XmlTextWriter.Token.Content)
				{
					this.WriteEndStartTag(false);
				}
				if (state == XmlTextWriter.State.Content)
				{
					this.stack[this.top].mixed = true;
				}
				break;
			default:
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
			this.currentState = state;
			this.lastToken = token;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00044277 File Offset: 0x00042477
		private void AutoCompleteAll()
		{
			if (this.flush)
			{
				this.FlushEncoders();
			}
			while (this.top > 0)
			{
				this.WriteEndElement();
			}
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00044298 File Offset: 0x00042498
		private void InternalWriteEndElement(bool longFormat)
		{
			try
			{
				if (this.top <= 0)
				{
					throw new InvalidOperationException(Res.GetString("There was no XML start tag open."));
				}
				this.AutoComplete(longFormat ? XmlTextWriter.Token.LongEndElement : XmlTextWriter.Token.EndElement);
				if (this.lastToken == XmlTextWriter.Token.LongEndElement)
				{
					if (this.indented)
					{
						this.Indent(true);
					}
					this.textWriter.Write('<');
					this.textWriter.Write('/');
					if (this.namespaces && this.stack[this.top].prefix != null)
					{
						this.textWriter.Write(this.stack[this.top].prefix);
						this.textWriter.Write(':');
					}
					this.textWriter.Write(this.stack[this.top].name);
					this.textWriter.Write('>');
				}
				int prevNsTop = this.stack[this.top].prevNsTop;
				if (this.useNsHashtable && prevNsTop < this.nsTop)
				{
					this.PopNamespaces(prevNsTop + 1, this.nsTop);
				}
				this.nsTop = prevNsTop;
				this.top--;
			}
			catch
			{
				this.currentState = XmlTextWriter.State.Error;
				throw;
			}
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000443F0 File Offset: 0x000425F0
		private void WriteEndStartTag(bool empty)
		{
			this.xmlEncoder.StartAttribute(false);
			for (int i = this.nsTop; i > this.stack[this.top].prevNsTop; i--)
			{
				if (!this.nsStack[i].declared)
				{
					this.textWriter.Write(" xmlns");
					this.textWriter.Write(':');
					this.textWriter.Write(this.nsStack[i].prefix);
					this.textWriter.Write('=');
					this.textWriter.Write(this.quoteChar);
					this.xmlEncoder.Write(this.nsStack[i].ns);
					this.textWriter.Write(this.quoteChar);
				}
			}
			if (this.stack[this.top].defaultNs != this.stack[this.top - 1].defaultNs && this.stack[this.top].defaultNsState == XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut)
			{
				this.textWriter.Write(" xmlns");
				this.textWriter.Write('=');
				this.textWriter.Write(this.quoteChar);
				this.xmlEncoder.Write(this.stack[this.top].defaultNs);
				this.textWriter.Write(this.quoteChar);
				this.stack[this.top].defaultNsState = XmlTextWriter.NamespaceState.DeclaredAndWrittenOut;
			}
			this.xmlEncoder.EndAttribute();
			if (empty)
			{
				this.textWriter.Write(" /");
			}
			this.textWriter.Write('>');
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x000445C2 File Offset: 0x000427C2
		private void WriteEndAttributeQuote()
		{
			if (this.specialAttr != XmlTextWriter.SpecialAttr.None)
			{
				this.HandleSpecialAttribute();
			}
			this.xmlEncoder.EndAttribute();
			this.textWriter.Write(this.curQuoteChar);
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x000445F0 File Offset: 0x000427F0
		private void Indent(bool beforeEndElement)
		{
			if (this.top == 0)
			{
				this.textWriter.WriteLine();
				return;
			}
			if (!this.stack[this.top].mixed)
			{
				this.textWriter.WriteLine();
				int i = beforeEndElement ? (this.top - 1) : this.top;
				for (i *= this.indentation; i > 0; i--)
				{
					this.textWriter.Write(this.indentChar);
				}
			}
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0004466C File Offset: 0x0004286C
		private void PushNamespace(string prefix, string ns, bool declared)
		{
			if ("http://www.w3.org/2000/xmlns/" == ns)
			{
				throw new ArgumentException(Res.GetString("Cannot bind to the reserved namespace."));
			}
			if (prefix == null)
			{
				XmlTextWriter.NamespaceState defaultNsState = this.stack[this.top].defaultNsState;
				if (defaultNsState > XmlTextWriter.NamespaceState.NotDeclaredButInScope)
				{
					if (defaultNsState != XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut)
					{
						return;
					}
				}
				else
				{
					this.stack[this.top].defaultNs = ns;
				}
				this.stack[this.top].defaultNsState = (declared ? XmlTextWriter.NamespaceState.DeclaredAndWrittenOut : XmlTextWriter.NamespaceState.DeclaredButNotWrittenOut);
				return;
			}
			if (prefix.Length != 0 && ns.Length == 0)
			{
				throw new ArgumentException(Res.GetString("Cannot use a prefix with an empty namespace."));
			}
			int num = this.LookupNamespace(prefix);
			if (num != -1 && this.nsStack[num].ns == ns)
			{
				if (declared)
				{
					this.nsStack[num].declared = true;
					return;
				}
			}
			else
			{
				if (declared && num != -1 && num > this.stack[this.top].prevNsTop)
				{
					this.nsStack[num].declared = true;
				}
				this.AddNamespace(prefix, ns, declared);
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00044784 File Offset: 0x00042984
		private void AddNamespace(string prefix, string ns, bool declared)
		{
			int num = this.nsTop + 1;
			this.nsTop = num;
			int num2 = num;
			if (num2 == this.nsStack.Length)
			{
				XmlTextWriter.Namespace[] destinationArray = new XmlTextWriter.Namespace[num2 * 2];
				Array.Copy(this.nsStack, destinationArray, num2);
				this.nsStack = destinationArray;
			}
			this.nsStack[num2].Set(prefix, ns, declared);
			if (this.useNsHashtable)
			{
				this.AddToNamespaceHashtable(num2);
				return;
			}
			if (num2 == 16)
			{
				this.nsHashtable = new Dictionary<string, int>(new SecureStringHasher());
				for (int i = 0; i <= num2; i++)
				{
					this.AddToNamespaceHashtable(i);
				}
				this.useNsHashtable = true;
			}
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00044820 File Offset: 0x00042A20
		private void AddToNamespaceHashtable(int namespaceIndex)
		{
			string prefix = this.nsStack[namespaceIndex].prefix;
			int prevNsIndex;
			if (this.nsHashtable.TryGetValue(prefix, out prevNsIndex))
			{
				this.nsStack[namespaceIndex].prevNsIndex = prevNsIndex;
			}
			this.nsHashtable[prefix] = namespaceIndex;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x00044870 File Offset: 0x00042A70
		private void PopNamespaces(int indexFrom, int indexTo)
		{
			for (int i = indexTo; i >= indexFrom; i--)
			{
				if (this.nsStack[i].prevNsIndex == -1)
				{
					this.nsHashtable.Remove(this.nsStack[i].prefix);
				}
				else
				{
					this.nsHashtable[this.nsStack[i].prefix] = this.nsStack[i].prevNsIndex;
				}
			}
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000448EC File Offset: 0x00042AEC
		private string GeneratePrefix()
		{
			XmlTextWriter.TagInfo[] array = this.stack;
			int num = this.top;
			int prefixCount = array[num].prefixCount;
			array[num].prefixCount = prefixCount + 1;
			int num2 = prefixCount + 1;
			return "d" + this.top.ToString("d", CultureInfo.InvariantCulture) + "p" + num2.ToString("d", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00044950 File Offset: 0x00042B50
		private void InternalWriteProcessingInstruction(string name, string text)
		{
			this.textWriter.Write("<?");
			this.ValidateName(name, false);
			this.textWriter.Write(name);
			this.textWriter.Write(' ');
			if (text != null)
			{
				this.xmlEncoder.WriteRawWithSurrogateChecking(text);
			}
			this.textWriter.Write("?>");
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000449B0 File Offset: 0x00042BB0
		private int LookupNamespace(string prefix)
		{
			if (this.useNsHashtable)
			{
				int result;
				if (this.nsHashtable.TryGetValue(prefix, out result))
				{
					return result;
				}
			}
			else
			{
				for (int i = this.nsTop; i >= 0; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00044A04 File Offset: 0x00042C04
		private int LookupNamespaceInCurrentScope(string prefix)
		{
			if (this.useNsHashtable)
			{
				int num;
				if (this.nsHashtable.TryGetValue(prefix, out num) && num > this.stack[this.top].prevNsTop)
				{
					return num;
				}
			}
			else
			{
				for (int i = this.nsTop; i > this.stack[this.top].prevNsTop; i--)
				{
					if (this.nsStack[i].prefix == prefix)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00044A88 File Offset: 0x00042C88
		private string FindPrefix(string ns)
		{
			for (int i = this.nsTop; i >= 0; i--)
			{
				if (this.nsStack[i].ns == ns && this.LookupNamespace(this.nsStack[i].prefix) == i)
				{
					return this.nsStack[i].prefix;
				}
			}
			return null;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00044AEC File Offset: 0x00042CEC
		private void InternalWriteName(string name, bool isNCName)
		{
			this.ValidateName(name, isNCName);
			this.textWriter.Write(name);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00044B04 File Offset: 0x00042D04
		private void ValidateName(string name, bool isNCName)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The empty string '' is not a valid name."));
			}
			int length = name.Length;
			if (this.namespaces)
			{
				int num = -1;
				for (int num2 = ValidateNames.ParseNCName(name); num2 != length; num2 += ValidateNames.ParseNmtoken(name, num2))
				{
					if (name[num2] != ':' || isNCName || num != -1 || num2 <= 0 || num2 + 1 >= length)
					{
						goto IL_6F;
					}
					num = num2;
					num2++;
				}
				return;
			}
			if (ValidateNames.IsNameNoNamespaces(name))
			{
				return;
			}
			IL_6F:
			throw new ArgumentException(Res.GetString("Invalid name character in '{0}'.", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00044B9C File Offset: 0x00042D9C
		private void HandleSpecialAttribute()
		{
			string text = this.xmlEncoder.AttributeValue;
			switch (this.specialAttr)
			{
			case XmlTextWriter.SpecialAttr.XmlSpace:
				text = XmlConvert.TrimString(text);
				if (text == "default")
				{
					this.stack[this.top].xmlSpace = XmlSpace.Default;
					return;
				}
				if (text == "preserve")
				{
					this.stack[this.top].xmlSpace = XmlSpace.Preserve;
					return;
				}
				throw new ArgumentException(Res.GetString("'{0}' is an invalid xml:space value.", new object[]
				{
					text
				}));
			case XmlTextWriter.SpecialAttr.XmlLang:
				this.stack[this.top].xmlLang = text;
				return;
			case XmlTextWriter.SpecialAttr.XmlNs:
				this.VerifyPrefixXml(this.prefixForXmlNs, text);
				this.PushNamespace(this.prefixForXmlNs, text, true);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00044C70 File Offset: 0x00042E70
		private void VerifyPrefixXml(string prefix, string ns)
		{
			if (prefix != null && prefix.Length == 3 && (prefix[0] == 'x' || prefix[0] == 'X') && (prefix[1] == 'm' || prefix[1] == 'M') && (prefix[2] == 'l' || prefix[2] == 'L') && "http://www.w3.org/XML/1998/namespace" != ns)
			{
				throw new ArgumentException(Res.GetString("Prefixes beginning with \"xml\" (regardless of whether the characters are uppercase, lowercase, or some combination thereof) are reserved for use by XML."));
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00044CE8 File Offset: 0x00042EE8
		private void PushStack()
		{
			if (this.top == this.stack.Length - 1)
			{
				XmlTextWriter.TagInfo[] destinationArray = new XmlTextWriter.TagInfo[this.stack.Length + 10];
				if (this.top > 0)
				{
					Array.Copy(this.stack, destinationArray, this.top + 1);
				}
				this.stack = destinationArray;
			}
			this.top++;
			this.stack[this.top].Init(this.nsTop);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00044D66 File Offset: 0x00042F66
		private void FlushEncoders()
		{
			if (this.base64Encoder != null)
			{
				this.base64Encoder.Flush();
			}
			this.flush = false;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00044D84 File Offset: 0x00042F84
		// Note: this type is marked as 'beforefieldinit'.
		static XmlTextWriter()
		{
		}

		// Token: 0x04000BDD RID: 3037
		private TextWriter textWriter;

		// Token: 0x04000BDE RID: 3038
		private XmlTextEncoder xmlEncoder;

		// Token: 0x04000BDF RID: 3039
		private Encoding encoding;

		// Token: 0x04000BE0 RID: 3040
		private Formatting formatting;

		// Token: 0x04000BE1 RID: 3041
		private bool indented;

		// Token: 0x04000BE2 RID: 3042
		private int indentation;

		// Token: 0x04000BE3 RID: 3043
		private char indentChar;

		// Token: 0x04000BE4 RID: 3044
		private XmlTextWriter.TagInfo[] stack;

		// Token: 0x04000BE5 RID: 3045
		private int top;

		// Token: 0x04000BE6 RID: 3046
		private XmlTextWriter.State[] stateTable;

		// Token: 0x04000BE7 RID: 3047
		private XmlTextWriter.State currentState;

		// Token: 0x04000BE8 RID: 3048
		private XmlTextWriter.Token lastToken;

		// Token: 0x04000BE9 RID: 3049
		private XmlTextWriterBase64Encoder base64Encoder;

		// Token: 0x04000BEA RID: 3050
		private char quoteChar;

		// Token: 0x04000BEB RID: 3051
		private char curQuoteChar;

		// Token: 0x04000BEC RID: 3052
		private bool namespaces;

		// Token: 0x04000BED RID: 3053
		private XmlTextWriter.SpecialAttr specialAttr;

		// Token: 0x04000BEE RID: 3054
		private string prefixForXmlNs;

		// Token: 0x04000BEF RID: 3055
		private bool flush;

		// Token: 0x04000BF0 RID: 3056
		private XmlTextWriter.Namespace[] nsStack;

		// Token: 0x04000BF1 RID: 3057
		private int nsTop;

		// Token: 0x04000BF2 RID: 3058
		private Dictionary<string, int> nsHashtable;

		// Token: 0x04000BF3 RID: 3059
		private bool useNsHashtable;

		// Token: 0x04000BF4 RID: 3060
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000BF5 RID: 3061
		private const int NamespaceStackInitialSize = 8;

		// Token: 0x04000BF6 RID: 3062
		private const int MaxNamespacesWalkCount = 16;

		// Token: 0x04000BF7 RID: 3063
		private static string[] stateName = new string[]
		{
			"Start",
			"Prolog",
			"PostDTD",
			"Element",
			"Attribute",
			"Content",
			"AttrOnly",
			"Epilog",
			"Error",
			"Closed"
		};

		// Token: 0x04000BF8 RID: 3064
		private static string[] tokenName = new string[]
		{
			"PI",
			"Doctype",
			"Comment",
			"CData",
			"StartElement",
			"EndElement",
			"LongEndElement",
			"StartAttribute",
			"EndAttribute",
			"Content",
			"Base64",
			"RawData",
			"Whitespace",
			"Empty"
		};

		// Token: 0x04000BF9 RID: 3065
		private static readonly XmlTextWriter.State[] stateTableDefault = new XmlTextWriter.State[]
		{
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.AttrOnly,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Epilog
		};

		// Token: 0x04000BFA RID: 3066
		private static readonly XmlTextWriter.State[] stateTableDocument = new XmlTextWriter.State[]
		{
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Element,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Prolog,
			XmlTextWriter.State.PostDTD,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Attribute,
			XmlTextWriter.State.Content,
			XmlTextWriter.State.Error,
			XmlTextWriter.State.Epilog
		};

		// Token: 0x02000118 RID: 280
		private enum NamespaceState
		{
			// Token: 0x04000BFC RID: 3068
			Uninitialized,
			// Token: 0x04000BFD RID: 3069
			NotDeclaredButInScope,
			// Token: 0x04000BFE RID: 3070
			DeclaredButNotWrittenOut,
			// Token: 0x04000BFF RID: 3071
			DeclaredAndWrittenOut
		}

		// Token: 0x02000119 RID: 281
		private struct TagInfo
		{
			// Token: 0x06000A72 RID: 2674 RVA: 0x00044E9D File Offset: 0x0004309D
			internal void Init(int nsTop)
			{
				this.name = null;
				this.defaultNs = string.Empty;
				this.defaultNsState = XmlTextWriter.NamespaceState.Uninitialized;
				this.xmlSpace = XmlSpace.None;
				this.xmlLang = null;
				this.prevNsTop = nsTop;
				this.prefixCount = 0;
				this.mixed = false;
			}

			// Token: 0x04000C00 RID: 3072
			internal string name;

			// Token: 0x04000C01 RID: 3073
			internal string prefix;

			// Token: 0x04000C02 RID: 3074
			internal string defaultNs;

			// Token: 0x04000C03 RID: 3075
			internal XmlTextWriter.NamespaceState defaultNsState;

			// Token: 0x04000C04 RID: 3076
			internal XmlSpace xmlSpace;

			// Token: 0x04000C05 RID: 3077
			internal string xmlLang;

			// Token: 0x04000C06 RID: 3078
			internal int prevNsTop;

			// Token: 0x04000C07 RID: 3079
			internal int prefixCount;

			// Token: 0x04000C08 RID: 3080
			internal bool mixed;
		}

		// Token: 0x0200011A RID: 282
		private struct Namespace
		{
			// Token: 0x06000A73 RID: 2675 RVA: 0x00044EDB File Offset: 0x000430DB
			internal void Set(string prefix, string ns, bool declared)
			{
				this.prefix = prefix;
				this.ns = ns;
				this.declared = declared;
				this.prevNsIndex = -1;
			}

			// Token: 0x04000C09 RID: 3081
			internal string prefix;

			// Token: 0x04000C0A RID: 3082
			internal string ns;

			// Token: 0x04000C0B RID: 3083
			internal bool declared;

			// Token: 0x04000C0C RID: 3084
			internal int prevNsIndex;
		}

		// Token: 0x0200011B RID: 283
		private enum SpecialAttr
		{
			// Token: 0x04000C0E RID: 3086
			None,
			// Token: 0x04000C0F RID: 3087
			XmlSpace,
			// Token: 0x04000C10 RID: 3088
			XmlLang,
			// Token: 0x04000C11 RID: 3089
			XmlNs
		}

		// Token: 0x0200011C RID: 284
		private enum State
		{
			// Token: 0x04000C13 RID: 3091
			Start,
			// Token: 0x04000C14 RID: 3092
			Prolog,
			// Token: 0x04000C15 RID: 3093
			PostDTD,
			// Token: 0x04000C16 RID: 3094
			Element,
			// Token: 0x04000C17 RID: 3095
			Attribute,
			// Token: 0x04000C18 RID: 3096
			Content,
			// Token: 0x04000C19 RID: 3097
			AttrOnly,
			// Token: 0x04000C1A RID: 3098
			Epilog,
			// Token: 0x04000C1B RID: 3099
			Error,
			// Token: 0x04000C1C RID: 3100
			Closed
		}

		// Token: 0x0200011D RID: 285
		private enum Token
		{
			// Token: 0x04000C1E RID: 3102
			PI,
			// Token: 0x04000C1F RID: 3103
			Doctype,
			// Token: 0x04000C20 RID: 3104
			Comment,
			// Token: 0x04000C21 RID: 3105
			CData,
			// Token: 0x04000C22 RID: 3106
			StartElement,
			// Token: 0x04000C23 RID: 3107
			EndElement,
			// Token: 0x04000C24 RID: 3108
			LongEndElement,
			// Token: 0x04000C25 RID: 3109
			StartAttribute,
			// Token: 0x04000C26 RID: 3110
			EndAttribute,
			// Token: 0x04000C27 RID: 3111
			Content,
			// Token: 0x04000C28 RID: 3112
			Base64,
			// Token: 0x04000C29 RID: 3113
			RawData,
			// Token: 0x04000C2A RID: 3114
			Whitespace,
			// Token: 0x04000C2B RID: 3115
			Empty
		}
	}
}
