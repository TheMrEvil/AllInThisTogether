using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Xml.Xsl.Runtime;

namespace System.Xml
{
	/// <summary>Specifies a set of features to support on the <see cref="T:System.Xml.XmlWriter" /> object created by the <see cref="Overload:System.Xml.XmlWriter.Create" /> method.</summary>
	// Token: 0x02000185 RID: 389
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public sealed class XmlWriterSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlWriterSettings" /> class.</summary>
		// Token: 0x06000DBF RID: 3519 RVA: 0x0005ACA6 File Offset: 0x00058EA6
		public XmlWriterSettings()
		{
			this.Initialize();
		}

		/// <summary>Gets or sets a value that indicates whether asynchronous <see cref="T:System.Xml.XmlWriter" /> methods can be used on a particular <see cref="T:System.Xml.XmlWriter" /> instance.</summary>
		/// <returns>
		///     <see langword="true" /> if asynchronous methods can be used; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0005ACBF File Offset: 0x00058EBF
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0005ACC7 File Offset: 0x00058EC7
		public bool Async
		{
			get
			{
				return this.useAsync;
			}
			set
			{
				this.CheckReadOnly("Async");
				this.useAsync = value;
			}
		}

		/// <summary>Gets or sets the type of text encoding to use.</summary>
		/// <returns>The text encoding to use. The default is <see langword="Encoding.UTF8" />.</returns>
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0005ACDB File Offset: 0x00058EDB
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0005ACE3 File Offset: 0x00058EE3
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.CheckReadOnly("Encoding");
				this.encoding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to omit an XML declaration.</summary>
		/// <returns>
		///     <see langword="true" /> to omit the XML declaration; otherwise, <see langword="false" />. The default is <see langword="false" />, an XML declaration is written.</returns>
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0005ACF7 File Offset: 0x00058EF7
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0005ACFF File Offset: 0x00058EFF
		public bool OmitXmlDeclaration
		{
			get
			{
				return this.omitXmlDecl;
			}
			set
			{
				this.CheckReadOnly("OmitXmlDeclaration");
				this.omitXmlDecl = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to normalize line breaks in the output.</summary>
		/// <returns>One of the <see cref="T:System.Xml.NewLineHandling" /> values. The default is <see cref="F:System.Xml.NewLineHandling.Replace" />.</returns>
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0005AD13 File Offset: 0x00058F13
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0005AD1B File Offset: 0x00058F1B
		public NewLineHandling NewLineHandling
		{
			get
			{
				return this.newLineHandling;
			}
			set
			{
				this.CheckReadOnly("NewLineHandling");
				if (value > NewLineHandling.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.newLineHandling = value;
			}
		}

		/// <summary>Gets or sets the character string to use for line breaks.</summary>
		/// <returns>The character string to use for line breaks. This can be set to any string value. However, to ensure valid XML, you should specify only valid white space characters, such as space characters, tabs, carriage returns, or line feeds. The default is \r\n (carriage return, new line).</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Xml.XmlWriterSettings.NewLineChars" /> is <see langword="null" />.</exception>
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0005AD3E File Offset: 0x00058F3E
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0005AD46 File Offset: 0x00058F46
		public string NewLineChars
		{
			get
			{
				return this.newLineChars;
			}
			set
			{
				this.CheckReadOnly("NewLineChars");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.newLineChars = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to indent elements.</summary>
		/// <returns>
		///     <see langword="true" /> to write individual elements on new lines and indent; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0005AD68 File Offset: 0x00058F68
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0005AD73 File Offset: 0x00058F73
		public bool Indent
		{
			get
			{
				return this.indent == TriState.True;
			}
			set
			{
				this.CheckReadOnly("Indent");
				this.indent = (value ? TriState.True : TriState.False);
			}
		}

		/// <summary>Gets or sets the character string to use when indenting. This setting is used when the <see cref="P:System.Xml.XmlWriterSettings.Indent" /> property is set to <see langword="true" />.</summary>
		/// <returns>The character string to use when indenting. This can be set to any string value. However, to ensure valid XML, you should specify only valid white space characters, such as space characters, tabs, carriage returns, or line feeds. The default is two spaces.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value assigned to the <see cref="P:System.Xml.XmlWriterSettings.IndentChars" /> is <see langword="null" />.</exception>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0005AD8D File Offset: 0x00058F8D
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0005AD95 File Offset: 0x00058F95
		public string IndentChars
		{
			get
			{
				return this.indentChars;
			}
			set
			{
				this.CheckReadOnly("IndentChars");
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.indentChars = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to write attributes on a new line.</summary>
		/// <returns>
		///     <see langword="true" /> to write attributes on individual lines; otherwise, <see langword="false" />. The default is <see langword="false" />.This setting has no effect when the <see cref="P:System.Xml.XmlWriterSettings.Indent" /> property value is <see langword="false" />.When <see cref="P:System.Xml.XmlWriterSettings.NewLineOnAttributes" /> is set to <see langword="true" />, each attribute is pre-pended with a new line and one extra level of indentation.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0005ADB7 File Offset: 0x00058FB7
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0005ADBF File Offset: 0x00058FBF
		public bool NewLineOnAttributes
		{
			get
			{
				return this.newLineOnAttributes;
			}
			set
			{
				this.CheckReadOnly("NewLineOnAttributes");
				this.newLineOnAttributes = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.XmlWriter" /> should also close the underlying stream or <see cref="T:System.IO.TextWriter" /> when the <see cref="M:System.Xml.XmlWriter.Close" /> method is called.</summary>
		/// <returns>
		///     <see langword="true" /> to also close the underlying stream or <see cref="T:System.IO.TextWriter" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0005ADD3 File Offset: 0x00058FD3
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0005ADDB File Offset: 0x00058FDB
		public bool CloseOutput
		{
			get
			{
				return this.closeOutput;
			}
			set
			{
				this.CheckReadOnly("CloseOutput");
				this.closeOutput = value;
			}
		}

		/// <summary>Gets or sets the level of conformance that the XML writer checks the XML output for.</summary>
		/// <returns>One of the enumeration values that specifies the level of conformance (document, fragment, or automatic detection). The default is <see cref="F:System.Xml.ConformanceLevel.Document" />.</returns>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0005ADEF File Offset: 0x00058FEF
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0005ADF7 File Offset: 0x00058FF7
		public ConformanceLevel ConformanceLevel
		{
			get
			{
				return this.conformanceLevel;
			}
			set
			{
				this.CheckReadOnly("ConformanceLevel");
				if (value > ConformanceLevel.Document)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.conformanceLevel = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the XML writer should check to ensure that all characters in the document conform to the "2.2 Characters" section of the W3C XML 1.0 Recommendation.</summary>
		/// <returns>
		///     <see langword="true" /> to do character checking; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0005AE1A File Offset: 0x0005901A
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0005AE22 File Offset: 0x00059022
		public bool CheckCharacters
		{
			get
			{
				return this.checkCharacters;
			}
			set
			{
				this.CheckReadOnly("CheckCharacters");
				this.checkCharacters = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> should remove duplicate namespace declarations when writing XML content. The default behavior is for the writer to output all namespace declarations that are present in the writer's namespace resolver.</summary>
		/// <returns>The <see cref="T:System.Xml.NamespaceHandling" /> enumeration used to specify whether to remove duplicate namespace declarations in the <see cref="T:System.Xml.XmlWriter" />.</returns>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0005AE36 File Offset: 0x00059036
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0005AE3E File Offset: 0x0005903E
		public NamespaceHandling NamespaceHandling
		{
			get
			{
				return this.namespaceHandling;
			}
			set
			{
				this.CheckReadOnly("NamespaceHandling");
				if (value > NamespaceHandling.OmitDuplicates)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.namespaceHandling = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> will add closing tags to all unclosed element tags when the <see cref="M:System.Xml.XmlWriter.Close" /> method is called.</summary>
		/// <returns>
		///     <see langword="true" /> if all unclosed element tags will be closed out; otherwise, <see langword="false" />. The default value is <see langword="true" />. </returns>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0005AE61 File Offset: 0x00059061
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0005AE69 File Offset: 0x00059069
		public bool WriteEndDocumentOnClose
		{
			get
			{
				return this.writeEndDocumentOnClose;
			}
			set
			{
				this.CheckReadOnly("WriteEndDocumentOnClose");
				this.writeEndDocumentOnClose = value;
			}
		}

		/// <summary>Gets the method used to serialize the <see cref="T:System.Xml.XmlWriter" /> output.</summary>
		/// <returns>One of the <see cref="T:System.Xml.XmlOutputMethod" /> values. The default is <see cref="F:System.Xml.XmlOutputMethod.Xml" />.</returns>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0005AE7D File Offset: 0x0005907D
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0005AE85 File Offset: 0x00059085
		public XmlOutputMethod OutputMethod
		{
			get
			{
				return this.outputMethod;
			}
			internal set
			{
				this.outputMethod = value;
			}
		}

		/// <summary>Resets the members of the settings class to their default values.</summary>
		// Token: 0x06000DDC RID: 3548 RVA: 0x0005AE8E File Offset: 0x0005908E
		public void Reset()
		{
			this.CheckReadOnly("Reset");
			this.Initialize();
		}

		/// <summary>Creates a copy of the <see cref="T:System.Xml.XmlWriterSettings" /> instance.</summary>
		/// <returns>The cloned <see cref="T:System.Xml.XmlWriterSettings" /> object.</returns>
		// Token: 0x06000DDD RID: 3549 RVA: 0x0005AEA1 File Offset: 0x000590A1
		public XmlWriterSettings Clone()
		{
			XmlWriterSettings xmlWriterSettings = base.MemberwiseClone() as XmlWriterSettings;
			xmlWriterSettings.cdataSections = new List<XmlQualifiedName>(this.cdataSections);
			xmlWriterSettings.isReadOnly = false;
			return xmlWriterSettings;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0005AEC6 File Offset: 0x000590C6
		internal List<XmlQualifiedName> CDataSectionElements
		{
			get
			{
				return this.cdataSections;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Xml.XmlWriter" /> does not escape URI attributes.</summary>
		/// <returns>
		///     <see langword="true" /> if the <see cref="T:System.Xml.XmlWriter" /> do not escape URI attributes; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0005AECE File Offset: 0x000590CE
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0005AED6 File Offset: 0x000590D6
		public bool DoNotEscapeUriAttributes
		{
			get
			{
				return this.doNotEscapeUriAttributes;
			}
			set
			{
				this.CheckReadOnly("DoNotEscapeUriAttributes");
				this.doNotEscapeUriAttributes = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0005AEEA File Offset: 0x000590EA
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x0005AEF2 File Offset: 0x000590F2
		internal bool MergeCDataSections
		{
			get
			{
				return this.mergeCDataSections;
			}
			set
			{
				this.CheckReadOnly("MergeCDataSections");
				this.mergeCDataSections = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0005AF06 File Offset: 0x00059106
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x0005AF0E File Offset: 0x0005910E
		internal string MediaType
		{
			get
			{
				return this.mediaType;
			}
			set
			{
				this.CheckReadOnly("MediaType");
				this.mediaType = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x0005AF22 File Offset: 0x00059122
		// (set) Token: 0x06000DE6 RID: 3558 RVA: 0x0005AF2A File Offset: 0x0005912A
		internal string DocTypeSystem
		{
			get
			{
				return this.docTypeSystem;
			}
			set
			{
				this.CheckReadOnly("DocTypeSystem");
				this.docTypeSystem = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x0005AF3E File Offset: 0x0005913E
		// (set) Token: 0x06000DE8 RID: 3560 RVA: 0x0005AF46 File Offset: 0x00059146
		internal string DocTypePublic
		{
			get
			{
				return this.docTypePublic;
			}
			set
			{
				this.CheckReadOnly("DocTypePublic");
				this.docTypePublic = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0005AF5A File Offset: 0x0005915A
		// (set) Token: 0x06000DEA RID: 3562 RVA: 0x0005AF62 File Offset: 0x00059162
		internal XmlStandalone Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				this.CheckReadOnly("Standalone");
				this.standalone = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0005AF76 File Offset: 0x00059176
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0005AF7E File Offset: 0x0005917E
		internal bool AutoXmlDeclaration
		{
			get
			{
				return this.autoXmlDecl;
			}
			set
			{
				this.CheckReadOnly("AutoXmlDeclaration");
				this.autoXmlDecl = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0005AF92 File Offset: 0x00059192
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0005AF9A File Offset: 0x0005919A
		internal TriState IndentInternal
		{
			get
			{
				return this.indent;
			}
			set
			{
				this.indent = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0005AFA3 File Offset: 0x000591A3
		internal bool IsQuerySpecific
		{
			get
			{
				return this.cdataSections.Count != 0 || this.docTypePublic != null || this.docTypeSystem != null || this.standalone == XmlStandalone.Yes;
			}
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0005AFD0 File Offset: 0x000591D0
		internal XmlWriter CreateWriter(string outputFileName)
		{
			if (outputFileName == null)
			{
				throw new ArgumentNullException("outputFileName");
			}
			XmlWriterSettings xmlWriterSettings = this;
			if (!xmlWriterSettings.CloseOutput)
			{
				xmlWriterSettings = xmlWriterSettings.Clone();
				xmlWriterSettings.CloseOutput = true;
			}
			FileStream fileStream = null;
			XmlWriter result;
			try
			{
				fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, this.useAsync);
				result = xmlWriterSettings.CreateWriter(fileStream);
			}
			catch
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0005B044 File Offset: 0x00059244
		internal XmlWriter CreateWriter(Stream output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			XmlWriter xmlWriter;
			if (this.Encoding.WebName == "utf-8")
			{
				switch (this.OutputMethod)
				{
				case XmlOutputMethod.Xml:
					if (this.Indent)
					{
						xmlWriter = new XmlUtf8RawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new XmlUtf8RawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Html:
					if (this.Indent)
					{
						xmlWriter = new HtmlUtf8RawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new HtmlUtf8RawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Text:
					xmlWriter = new TextUtf8RawTextWriter(output, this);
					break;
				case XmlOutputMethod.AutoDetect:
					xmlWriter = new XmlAutoDetectWriter(output, this);
					break;
				default:
					return null;
				}
			}
			else
			{
				switch (this.OutputMethod)
				{
				case XmlOutputMethod.Xml:
					if (this.Indent)
					{
						xmlWriter = new XmlEncodedRawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new XmlEncodedRawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Html:
					if (this.Indent)
					{
						xmlWriter = new HtmlEncodedRawTextWriterIndent(output, this);
					}
					else
					{
						xmlWriter = new HtmlEncodedRawTextWriter(output, this);
					}
					break;
				case XmlOutputMethod.Text:
					xmlWriter = new TextEncodedRawTextWriter(output, this);
					break;
				case XmlOutputMethod.AutoDetect:
					xmlWriter = new XmlAutoDetectWriter(output, this);
					break;
				default:
					return null;
				}
			}
			if (this.OutputMethod != XmlOutputMethod.AutoDetect && this.IsQuerySpecific)
			{
				xmlWriter = new QueryOutputWriter((XmlRawWriter)xmlWriter, this);
			}
			xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
			if (this.useAsync)
			{
				xmlWriter = new XmlAsyncCheckWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0005B194 File Offset: 0x00059394
		internal XmlWriter CreateWriter(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			XmlWriter xmlWriter;
			switch (this.OutputMethod)
			{
			case XmlOutputMethod.Xml:
				if (this.Indent)
				{
					xmlWriter = new XmlEncodedRawTextWriterIndent(output, this);
				}
				else
				{
					xmlWriter = new XmlEncodedRawTextWriter(output, this);
				}
				break;
			case XmlOutputMethod.Html:
				if (this.Indent)
				{
					xmlWriter = new HtmlEncodedRawTextWriterIndent(output, this);
				}
				else
				{
					xmlWriter = new HtmlEncodedRawTextWriter(output, this);
				}
				break;
			case XmlOutputMethod.Text:
				xmlWriter = new TextEncodedRawTextWriter(output, this);
				break;
			case XmlOutputMethod.AutoDetect:
				xmlWriter = new XmlAutoDetectWriter(output, this);
				break;
			default:
				return null;
			}
			if (this.OutputMethod != XmlOutputMethod.AutoDetect && this.IsQuerySpecific)
			{
				xmlWriter = new QueryOutputWriter((XmlRawWriter)xmlWriter, this);
			}
			xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
			if (this.useAsync)
			{
				xmlWriter = new XmlAsyncCheckWriter(xmlWriter);
			}
			return xmlWriter;
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0005B252 File Offset: 0x00059452
		internal XmlWriter CreateWriter(XmlWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			return this.AddConformanceWrapper(output);
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0005B269 File Offset: 0x00059469
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0005B271 File Offset: 0x00059471
		internal bool ReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0005B27A File Offset: 0x0005947A
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("The '{0}' property is read only and cannot be set.", base.GetType().Name + "." + propertyName);
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0005B2A8 File Offset: 0x000594A8
		private void Initialize()
		{
			this.encoding = Encoding.UTF8;
			this.omitXmlDecl = false;
			this.newLineHandling = NewLineHandling.Replace;
			this.newLineChars = Environment.NewLine;
			this.indent = TriState.Unknown;
			this.indentChars = "  ";
			this.newLineOnAttributes = false;
			this.closeOutput = false;
			this.namespaceHandling = NamespaceHandling.Default;
			this.conformanceLevel = ConformanceLevel.Document;
			this.checkCharacters = true;
			this.writeEndDocumentOnClose = true;
			this.outputMethod = XmlOutputMethod.Xml;
			this.cdataSections.Clear();
			this.mergeCDataSections = false;
			this.mediaType = null;
			this.docTypeSystem = null;
			this.docTypePublic = null;
			this.standalone = XmlStandalone.Omit;
			this.doNotEscapeUriAttributes = false;
			this.useAsync = false;
			this.isReadOnly = false;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0005B360 File Offset: 0x00059560
		private XmlWriter AddConformanceWrapper(XmlWriter baseWriter)
		{
			ConformanceLevel conformanceLevel = ConformanceLevel.Auto;
			XmlWriterSettings settings = baseWriter.Settings;
			bool flag = false;
			bool checkNames = false;
			bool flag2 = false;
			bool flag3 = false;
			if (settings == null)
			{
				if (this.newLineHandling == NewLineHandling.Replace)
				{
					flag2 = true;
					flag3 = true;
				}
				if (this.checkCharacters)
				{
					flag = true;
					flag3 = true;
				}
			}
			else
			{
				if (this.conformanceLevel != settings.ConformanceLevel)
				{
					conformanceLevel = this.ConformanceLevel;
					flag3 = true;
				}
				if (this.checkCharacters && !settings.CheckCharacters)
				{
					flag = true;
					checkNames = (conformanceLevel == ConformanceLevel.Auto);
					flag3 = true;
				}
				if (this.newLineHandling == NewLineHandling.Replace && settings.NewLineHandling == NewLineHandling.None)
				{
					flag2 = true;
					flag3 = true;
				}
			}
			XmlWriter xmlWriter = baseWriter;
			if (flag3)
			{
				if (conformanceLevel != ConformanceLevel.Auto)
				{
					xmlWriter = new XmlWellFormedWriter(xmlWriter, this);
				}
				if (flag || flag2)
				{
					xmlWriter = new XmlCharCheckingWriter(xmlWriter, flag, checkNames, flag2, this.NewLineChars);
				}
			}
			if (this.IsQuerySpecific && (settings == null || !settings.IsQuerySpecific))
			{
				xmlWriter = new QueryOutputWriterV1(xmlWriter, this);
			}
			return xmlWriter;
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0005B438 File Offset: 0x00059638
		internal void GetObjectData(XmlQueryDataWriter writer)
		{
			writer.Write(this.Encoding.CodePage);
			writer.Write(this.OmitXmlDeclaration);
			writer.Write((sbyte)this.NewLineHandling);
			writer.WriteStringQ(this.NewLineChars);
			writer.Write((sbyte)this.IndentInternal);
			writer.WriteStringQ(this.IndentChars);
			writer.Write(this.NewLineOnAttributes);
			writer.Write(this.CloseOutput);
			writer.Write((sbyte)this.ConformanceLevel);
			writer.Write(this.CheckCharacters);
			writer.Write((sbyte)this.outputMethod);
			writer.Write(this.cdataSections.Count);
			foreach (XmlQualifiedName xmlQualifiedName in this.cdataSections)
			{
				writer.Write(xmlQualifiedName.Name);
				writer.Write(xmlQualifiedName.Namespace);
			}
			writer.Write(this.mergeCDataSections);
			writer.WriteStringQ(this.mediaType);
			writer.WriteStringQ(this.docTypeSystem);
			writer.WriteStringQ(this.docTypePublic);
			writer.Write((sbyte)this.standalone);
			writer.Write(this.autoXmlDecl);
			writer.Write(this.ReadOnly);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0005B590 File Offset: 0x00059790
		internal XmlWriterSettings(XmlQueryDataReader reader)
		{
			this.Encoding = Encoding.GetEncoding(reader.ReadInt32());
			this.OmitXmlDeclaration = reader.ReadBoolean();
			this.NewLineHandling = (NewLineHandling)reader.ReadSByte(0, 2);
			this.NewLineChars = reader.ReadStringQ();
			this.IndentInternal = (TriState)reader.ReadSByte(-1, 1);
			this.IndentChars = reader.ReadStringQ();
			this.NewLineOnAttributes = reader.ReadBoolean();
			this.CloseOutput = reader.ReadBoolean();
			this.ConformanceLevel = (ConformanceLevel)reader.ReadSByte(0, 2);
			this.CheckCharacters = reader.ReadBoolean();
			this.outputMethod = (XmlOutputMethod)reader.ReadSByte(0, 3);
			int num = reader.ReadInt32();
			this.cdataSections = new List<XmlQualifiedName>(num);
			for (int i = 0; i < num; i++)
			{
				this.cdataSections.Add(new XmlQualifiedName(reader.ReadString(), reader.ReadString()));
			}
			this.mergeCDataSections = reader.ReadBoolean();
			this.mediaType = reader.ReadStringQ();
			this.docTypeSystem = reader.ReadStringQ();
			this.docTypePublic = reader.ReadStringQ();
			this.Standalone = (XmlStandalone)reader.ReadSByte(0, 2);
			this.autoXmlDecl = reader.ReadBoolean();
			this.ReadOnly = reader.ReadBoolean();
		}

		// Token: 0x04000EF7 RID: 3831
		private bool useAsync;

		// Token: 0x04000EF8 RID: 3832
		private Encoding encoding;

		// Token: 0x04000EF9 RID: 3833
		private bool omitXmlDecl;

		// Token: 0x04000EFA RID: 3834
		private NewLineHandling newLineHandling;

		// Token: 0x04000EFB RID: 3835
		private string newLineChars;

		// Token: 0x04000EFC RID: 3836
		private TriState indent;

		// Token: 0x04000EFD RID: 3837
		private string indentChars;

		// Token: 0x04000EFE RID: 3838
		private bool newLineOnAttributes;

		// Token: 0x04000EFF RID: 3839
		private bool closeOutput;

		// Token: 0x04000F00 RID: 3840
		private NamespaceHandling namespaceHandling;

		// Token: 0x04000F01 RID: 3841
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000F02 RID: 3842
		private bool checkCharacters;

		// Token: 0x04000F03 RID: 3843
		private bool writeEndDocumentOnClose;

		// Token: 0x04000F04 RID: 3844
		private XmlOutputMethod outputMethod;

		// Token: 0x04000F05 RID: 3845
		private List<XmlQualifiedName> cdataSections = new List<XmlQualifiedName>();

		// Token: 0x04000F06 RID: 3846
		private bool doNotEscapeUriAttributes;

		// Token: 0x04000F07 RID: 3847
		private bool mergeCDataSections;

		// Token: 0x04000F08 RID: 3848
		private string mediaType;

		// Token: 0x04000F09 RID: 3849
		private string docTypeSystem;

		// Token: 0x04000F0A RID: 3850
		private string docTypePublic;

		// Token: 0x04000F0B RID: 3851
		private XmlStandalone standalone;

		// Token: 0x04000F0C RID: 3852
		private bool autoXmlDecl;

		// Token: 0x04000F0D RID: 3853
		private bool isReadOnly;
	}
}
