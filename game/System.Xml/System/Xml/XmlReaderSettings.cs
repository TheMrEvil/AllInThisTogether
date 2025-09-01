using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Xml.Schema;
using System.Xml.XmlConfiguration;

namespace System.Xml
{
	/// <summary>Specifies a set of features to support on the <see cref="T:System.Xml.XmlReader" /> object created by the <see cref="Overload:System.Xml.XmlReader.Create" /> method. </summary>
	// Token: 0x020000A9 RID: 169
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public sealed class XmlReaderSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlReaderSettings" /> class.</summary>
		// Token: 0x0600068A RID: 1674 RVA: 0x00021BD6 File Offset: 0x0001FDD6
		public XmlReaderSettings()
		{
			this.Initialize();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlReaderSettings" /> class.</summary>
		/// <param name="resolver">The XML resolver.</param>
		// Token: 0x0600068B RID: 1675 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		public XmlReaderSettings(XmlResolver resolver)
		{
			this.Initialize(resolver);
		}

		/// <summary>Gets or sets whether asynchronous <see cref="T:System.Xml.XmlReader" /> methods can be used on a particular <see cref="T:System.Xml.XmlReader" /> instance.</summary>
		/// <returns>
		///     <see langword="true" /> if asynchronous methods can be used; otherwise, <see langword="false" />.</returns>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00021BF3 File Offset: 0x0001FDF3
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00021BFB File Offset: 0x0001FDFB
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

		/// <summary>Gets or sets the <see cref="T:System.Xml.XmlNameTable" /> used for atomized string comparisons.</summary>
		/// <returns>The <see cref="T:System.Xml.XmlNameTable" /> that stores all the atomized strings used by all <see cref="T:System.Xml.XmlReader" /> instances created using this <see cref="T:System.Xml.XmlReaderSettings" /> object.The default is <see langword="null" />. The created <see cref="T:System.Xml.XmlReader" /> instance will use a new empty <see cref="T:System.Xml.NameTable" /> if this value is <see langword="null" />.</returns>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x00021C0F File Offset: 0x0001FE0F
		// (set) Token: 0x0600068F RID: 1679 RVA: 0x00021C17 File Offset: 0x0001FE17
		public XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
			set
			{
				this.CheckReadOnly("NameTable");
				this.nameTable = value;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x00021C2B File Offset: 0x0001FE2B
		// (set) Token: 0x06000691 RID: 1681 RVA: 0x00021C33 File Offset: 0x0001FE33
		internal bool IsXmlResolverSet
		{
			[CompilerGenerated]
			get
			{
				return this.<IsXmlResolverSet>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsXmlResolverSet>k__BackingField = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Xml.XmlResolver" /> used to access external documents.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlResolver" /> used to access external documents. If set to <see langword="null" />, an <see cref="T:System.Xml.XmlException" /> is thrown when the <see cref="T:System.Xml.XmlReader" /> tries to access an external resource. The default is a new <see cref="T:System.Xml.XmlUrlResolver" /> with no credentials.  Starting with the .NET Framework 4.5.2, this setting has a default value of <see langword="null" />.</returns>
		// Token: 0x170000D6 RID: 214
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00021C3C File Offset: 0x0001FE3C
		public XmlResolver XmlResolver
		{
			set
			{
				this.CheckReadOnly("XmlResolver");
				this.xmlResolver = value;
				this.IsXmlResolverSet = true;
			}
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00021C57 File Offset: 0x0001FE57
		internal XmlResolver GetXmlResolver()
		{
			return this.xmlResolver;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00021C5F File Offset: 0x0001FE5F
		internal XmlResolver GetXmlResolver_CheckConfig()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver && !this.IsXmlResolverSet)
			{
				return null;
			}
			return this.xmlResolver;
		}

		/// <summary>Gets or sets line number offset of the <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>The line number offset. The default is 0.</returns>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00021C78 File Offset: 0x0001FE78
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x00021C80 File Offset: 0x0001FE80
		public int LineNumberOffset
		{
			get
			{
				return this.lineNumberOffset;
			}
			set
			{
				this.CheckReadOnly("LineNumberOffset");
				this.lineNumberOffset = value;
			}
		}

		/// <summary>Gets or sets line position offset of the <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>The line position offset. The default is 0.</returns>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00021C94 File Offset: 0x0001FE94
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x00021C9C File Offset: 0x0001FE9C
		public int LinePositionOffset
		{
			get
			{
				return this.linePositionOffset;
			}
			set
			{
				this.CheckReadOnly("LinePositionOffset");
				this.linePositionOffset = value;
			}
		}

		/// <summary>Gets or sets the level of conformance which the <see cref="T:System.Xml.XmlReader" /> will comply.</summary>
		/// <returns>One of the enumeration values that specifies the level of conformance that the XML reader will enforce. The default is <see cref="F:System.Xml.ConformanceLevel.Document" />.</returns>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x00021CB0 File Offset: 0x0001FEB0
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x00021CB8 File Offset: 0x0001FEB8
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

		/// <summary>Gets or sets a value indicating whether to do character checking.</summary>
		/// <returns>
		///     <see langword="true" /> to do character checking; otherwise <see langword="false" />. The default is <see langword="true" />.If the <see cref="T:System.Xml.XmlReader" /> is processing text data, it always checks that the XML names and text content are valid, regardless of the property setting. Setting <see cref="P:System.Xml.XmlReaderSettings.CheckCharacters" /> to <see langword="false" /> turns off character checking for character entity references.</returns>
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00021CDB File Offset: 0x0001FEDB
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x00021CE3 File Offset: 0x0001FEE3
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

		/// <summary>Gets or sets a value indicating the maximum allowable number of characters in an XML document. A zero (0) value means no limits on the size of the XML document. A non-zero value specifies the maximum size, in characters.</summary>
		/// <returns>The maximum allowable number of characters in an XML document. The default is 0.</returns>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x00021CF7 File Offset: 0x0001FEF7
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x00021CFF File Offset: 0x0001FEFF
		public long MaxCharactersInDocument
		{
			get
			{
				return this.maxCharactersInDocument;
			}
			set
			{
				this.CheckReadOnly("MaxCharactersInDocument");
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxCharactersInDocument = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum allowable number of characters in a document that result from expanding entities.</summary>
		/// <returns>The maximum allowable number of characters from expanded entities. The default is 0.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x00021D23 File Offset: 0x0001FF23
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00021D2B File Offset: 0x0001FF2B
		public long MaxCharactersFromEntities
		{
			get
			{
				return this.maxCharactersFromEntities;
			}
			set
			{
				this.CheckReadOnly("MaxCharactersFromEntities");
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.maxCharactersFromEntities = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore insignificant white space.</summary>
		/// <returns>
		///     <see langword="true" /> to ignore white space; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00021D4F File Offset: 0x0001FF4F
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x00021D57 File Offset: 0x0001FF57
		public bool IgnoreWhitespace
		{
			get
			{
				return this.ignoreWhitespace;
			}
			set
			{
				this.CheckReadOnly("IgnoreWhitespace");
				this.ignoreWhitespace = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore processing instructions.</summary>
		/// <returns>
		///     <see langword="true" /> to ignore processing instructions; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00021D6B File Offset: 0x0001FF6B
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x00021D73 File Offset: 0x0001FF73
		public bool IgnoreProcessingInstructions
		{
			get
			{
				return this.ignorePIs;
			}
			set
			{
				this.CheckReadOnly("IgnoreProcessingInstructions");
				this.ignorePIs = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to ignore comments.</summary>
		/// <returns>
		///     <see langword="true" /> to ignore comments; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x00021D87 File Offset: 0x0001FF87
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x00021D8F File Offset: 0x0001FF8F
		public bool IgnoreComments
		{
			get
			{
				return this.ignoreComments;
			}
			set
			{
				this.CheckReadOnly("IgnoreComments");
				this.ignoreComments = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to prohibit document type definition (DTD) processing. This property is obsolete. Use <see cref="P:System.Xml.XmlTextReader.DtdProcessing" /> instead.</summary>
		/// <returns>
		///     <see langword="true" /> to prohibit DTD processing; otherwise <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x00021DA3 File Offset: 0x0001FFA3
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x00021DAE File Offset: 0x0001FFAE
		[Obsolete("Use XmlReaderSettings.DtdProcessing property instead.")]
		public bool ProhibitDtd
		{
			get
			{
				return this.dtdProcessing == DtdProcessing.Prohibit;
			}
			set
			{
				this.CheckReadOnly("ProhibitDtd");
				this.dtdProcessing = (value ? DtdProcessing.Prohibit : DtdProcessing.Parse);
			}
		}

		/// <summary>Gets or sets a value that determines the processing of DTDs.</summary>
		/// <returns>One of the enumeration values that determines the processing of DTDs. The default is <see cref="F:System.Xml.DtdProcessing.Prohibit" />.</returns>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		public DtdProcessing DtdProcessing
		{
			get
			{
				return this.dtdProcessing;
			}
			set
			{
				this.CheckReadOnly("DtdProcessing");
				if (value > DtdProcessing.Parse)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.dtdProcessing = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the underlying stream or <see cref="T:System.IO.TextReader" /> should be closed when the reader is closed.</summary>
		/// <returns>
		///     <see langword="true" /> to close the underlying stream or <see cref="T:System.IO.TextReader" /> when the reader is closed; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x00021DF3 File Offset: 0x0001FFF3
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x00021DFB File Offset: 0x0001FFFB
		public bool CloseInput
		{
			get
			{
				return this.closeInput;
			}
			set
			{
				this.CheckReadOnly("CloseInput");
				this.closeInput = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.XmlReader" /> will perform validation or type assignment when reading.</summary>
		/// <returns>One of the <see cref="T:System.Xml.ValidationType" /> values that indicates whether XmlReader will perform validation or type assignment when reading. The default is <see langword="ValidationType.None" />.</returns>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00021E0F File Offset: 0x0002000F
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00021E17 File Offset: 0x00020017
		public ValidationType ValidationType
		{
			get
			{
				return this.validationType;
			}
			set
			{
				this.CheckReadOnly("ValidationType");
				if (value > ValidationType.Schema)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.validationType = value;
			}
		}

		/// <summary>Gets or sets a value indicating the schema validation settings. This setting applies to <see cref="T:System.Xml.XmlReader" /> objects that validate schemas (<see cref="P:System.Xml.XmlReaderSettings.ValidationType" /> property set to <see langword="ValidationType.Schema" />).</summary>
		/// <returns>A bitwise combination of enumeration values that specify validation options. <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessIdentityConstraints" /> and <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.AllowXmlAttributes" /> are enabled by default. <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessInlineSchema" />, <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation" />, and <see cref="F:System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings" /> are disabled by default.</returns>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x00021E3A File Offset: 0x0002003A
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x00021E42 File Offset: 0x00020042
		public XmlSchemaValidationFlags ValidationFlags
		{
			get
			{
				return this.validationFlags;
			}
			set
			{
				this.CheckReadOnly("ValidationFlags");
				if (value > (XmlSchemaValidationFlags.ProcessInlineSchema | XmlSchemaValidationFlags.ProcessSchemaLocation | XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.AllowXmlAttributes))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.validationFlags = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to use when performing schema validation.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchemaSet" /> to use when performing schema validation. The default is an empty <see cref="T:System.Xml.Schema.XmlSchemaSet" /> object.</returns>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00021E66 File Offset: 0x00020066
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x00021E81 File Offset: 0x00020081
		public XmlSchemaSet Schemas
		{
			get
			{
				if (this.schemas == null)
				{
					this.schemas = new XmlSchemaSet();
				}
				return this.schemas;
			}
			set
			{
				this.CheckReadOnly("Schemas");
				this.schemas = value;
			}
		}

		/// <summary>Occurs when the reader encounters validation errors.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060006B3 RID: 1715 RVA: 0x00021E95 File Offset: 0x00020095
		// (remove) Token: 0x060006B4 RID: 1716 RVA: 0x00021EB9 File Offset: 0x000200B9
		public event ValidationEventHandler ValidationEventHandler
		{
			add
			{
				this.CheckReadOnly("ValidationEventHandler");
				this.valEventHandler = (ValidationEventHandler)Delegate.Combine(this.valEventHandler, value);
			}
			remove
			{
				this.CheckReadOnly("ValidationEventHandler");
				this.valEventHandler = (ValidationEventHandler)Delegate.Remove(this.valEventHandler, value);
			}
		}

		/// <summary>Resets the members of the settings class to their default values.</summary>
		// Token: 0x060006B5 RID: 1717 RVA: 0x00021EDD File Offset: 0x000200DD
		public void Reset()
		{
			this.CheckReadOnly("Reset");
			this.Initialize();
		}

		/// <summary>Creates a copy of the <see cref="T:System.Xml.XmlReaderSettings" /> instance.</summary>
		/// <returns>The cloned <see cref="T:System.Xml.XmlReaderSettings" /> object.</returns>
		// Token: 0x060006B6 RID: 1718 RVA: 0x00021EF0 File Offset: 0x000200F0
		public XmlReaderSettings Clone()
		{
			XmlReaderSettings xmlReaderSettings = base.MemberwiseClone() as XmlReaderSettings;
			xmlReaderSettings.ReadOnly = false;
			return xmlReaderSettings;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00021F04 File Offset: 0x00020104
		internal ValidationEventHandler GetEventHandler()
		{
			return this.valEventHandler;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00021F0C File Offset: 0x0002010C
		internal XmlReader CreateReader(string inputUri, XmlParserContext inputContext)
		{
			if (inputUri == null)
			{
				throw new ArgumentNullException("inputUri");
			}
			if (inputUri.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The string was not recognized as a valid Uri."), "inputUri");
			}
			XmlResolver xmlResolver = this.GetXmlResolver();
			if (xmlResolver == null)
			{
				xmlResolver = XmlReaderSettings.CreateDefaultResolver();
			}
			XmlReader xmlReader = new XmlTextReaderImpl(inputUri, this, inputContext, xmlResolver);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00021F80 File Offset: 0x00020180
		internal XmlReader CreateReader(Stream input, Uri baseUri, string baseUriString, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (baseUriString == null)
			{
				if (baseUri == null)
				{
					baseUriString = string.Empty;
				}
				else
				{
					baseUriString = baseUri.ToString();
				}
			}
			XmlReader xmlReader = new XmlTextReaderImpl(input, null, 0, this, baseUri, baseUriString, inputContext, this.closeInput);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00021FEC File Offset: 0x000201EC
		internal XmlReader CreateReader(TextReader input, string baseUriString, XmlParserContext inputContext)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (baseUriString == null)
			{
				baseUriString = string.Empty;
			}
			XmlReader xmlReader = new XmlTextReaderImpl(input, this, baseUriString, inputContext);
			if (this.ValidationType != ValidationType.None)
			{
				xmlReader = this.AddValidation(xmlReader);
			}
			if (this.useAsync)
			{
				xmlReader = XmlAsyncCheckReader.CreateAsyncCheckWrapper(xmlReader);
			}
			return xmlReader;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0002203B File Offset: 0x0002023B
		internal XmlReader CreateReader(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return this.AddValidationAndConformanceWrapper(reader);
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00022052 File Offset: 0x00020252
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0002205A File Offset: 0x0002025A
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

		// Token: 0x060006BE RID: 1726 RVA: 0x00022063 File Offset: 0x00020263
		private void CheckReadOnly(string propertyName)
		{
			if (this.isReadOnly)
			{
				throw new XmlException("The '{0}' property is read only and cannot be set.", base.GetType().Name + "." + propertyName);
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0002208E File Offset: 0x0002028E
		private void Initialize()
		{
			this.Initialize(null);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00022098 File Offset: 0x00020298
		private void Initialize(XmlResolver resolver)
		{
			this.nameTable = null;
			if (!XmlReaderSettings.EnableLegacyXmlSettings())
			{
				this.xmlResolver = resolver;
				this.maxCharactersFromEntities = 10000000L;
			}
			else
			{
				this.xmlResolver = ((resolver == null) ? XmlReaderSettings.CreateDefaultResolver() : resolver);
				this.maxCharactersFromEntities = 0L;
			}
			this.lineNumberOffset = 0;
			this.linePositionOffset = 0;
			this.checkCharacters = true;
			this.conformanceLevel = ConformanceLevel.Document;
			this.ignoreWhitespace = false;
			this.ignorePIs = false;
			this.ignoreComments = false;
			this.dtdProcessing = DtdProcessing.Prohibit;
			this.closeInput = false;
			this.maxCharactersInDocument = 0L;
			this.schemas = null;
			this.validationType = ValidationType.None;
			this.validationFlags = XmlSchemaValidationFlags.ProcessIdentityConstraints;
			this.validationFlags |= XmlSchemaValidationFlags.AllowXmlAttributes;
			this.useAsync = false;
			this.isReadOnly = false;
			this.IsXmlResolverSet = false;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00022161 File Offset: 0x00020361
		private static XmlResolver CreateDefaultResolver()
		{
			return new XmlUrlResolver();
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00022168 File Offset: 0x00020368
		internal XmlReader AddValidation(XmlReader reader)
		{
			if (this.validationType == ValidationType.Schema)
			{
				XmlResolver xmlResolver = this.GetXmlResolver_CheckConfig();
				if (xmlResolver == null && !this.IsXmlResolverSet && !XmlReaderSettings.EnableLegacyXmlSettings())
				{
					xmlResolver = new XmlUrlResolver();
				}
				reader = new XsdValidatingReader(reader, xmlResolver, this);
			}
			else if (this.validationType == ValidationType.DTD)
			{
				reader = this.CreateDtdValidatingReader(reader);
			}
			return reader;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000221BC File Offset: 0x000203BC
		private XmlReader AddValidationAndConformanceWrapper(XmlReader reader)
		{
			if (this.validationType == ValidationType.DTD)
			{
				reader = this.CreateDtdValidatingReader(reader);
			}
			reader = this.AddConformanceWrapper(reader);
			if (this.validationType == ValidationType.Schema)
			{
				reader = new XsdValidatingReader(reader, this.GetXmlResolver_CheckConfig(), this);
			}
			return reader;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000221F2 File Offset: 0x000203F2
		private XmlValidatingReaderImpl CreateDtdValidatingReader(XmlReader baseReader)
		{
			return new XmlValidatingReaderImpl(baseReader, this.GetEventHandler(), (this.ValidationFlags & XmlSchemaValidationFlags.ProcessIdentityConstraints) > XmlSchemaValidationFlags.None);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0002220C File Offset: 0x0002040C
		internal XmlReader AddConformanceWrapper(XmlReader baseReader)
		{
			XmlReaderSettings settings = baseReader.Settings;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool ignorePis = false;
			DtdProcessing dtdProcessing = (DtdProcessing)(-1);
			bool flag4 = false;
			if (settings == null)
			{
				if (this.conformanceLevel != ConformanceLevel.Auto && this.conformanceLevel != XmlReader.GetV1ConformanceLevel(baseReader))
				{
					throw new InvalidOperationException(Res.GetString("Cannot change conformance checking to {0}. Make sure the ConformanceLevel in XmlReaderSettings is set to Auto for wrapping scenarios.", new object[]
					{
						this.conformanceLevel.ToString()
					}));
				}
				XmlTextReader xmlTextReader = baseReader as XmlTextReader;
				if (xmlTextReader == null)
				{
					XmlValidatingReader xmlValidatingReader = baseReader as XmlValidatingReader;
					if (xmlValidatingReader != null)
					{
						xmlTextReader = (XmlTextReader)xmlValidatingReader.Reader;
					}
				}
				if (this.ignoreWhitespace)
				{
					WhitespaceHandling whitespaceHandling = WhitespaceHandling.All;
					if (xmlTextReader != null)
					{
						whitespaceHandling = xmlTextReader.WhitespaceHandling;
					}
					if (whitespaceHandling == WhitespaceHandling.All)
					{
						flag2 = true;
						flag4 = true;
					}
				}
				if (this.ignoreComments)
				{
					flag3 = true;
					flag4 = true;
				}
				if (this.ignorePIs)
				{
					ignorePis = true;
					flag4 = true;
				}
				DtdProcessing dtdProcessing2 = DtdProcessing.Parse;
				if (xmlTextReader != null)
				{
					dtdProcessing2 = xmlTextReader.DtdProcessing;
				}
				if ((this.dtdProcessing == DtdProcessing.Prohibit && dtdProcessing2 != DtdProcessing.Prohibit) || (this.dtdProcessing == DtdProcessing.Ignore && dtdProcessing2 == DtdProcessing.Parse))
				{
					dtdProcessing = this.dtdProcessing;
					flag4 = true;
				}
			}
			else
			{
				if (this.conformanceLevel != settings.ConformanceLevel && this.conformanceLevel != ConformanceLevel.Auto)
				{
					throw new InvalidOperationException(Res.GetString("Cannot change conformance checking to {0}. Make sure the ConformanceLevel in XmlReaderSettings is set to Auto for wrapping scenarios.", new object[]
					{
						this.conformanceLevel.ToString()
					}));
				}
				if (this.checkCharacters && !settings.CheckCharacters)
				{
					flag = true;
					flag4 = true;
				}
				if (this.ignoreWhitespace && !settings.IgnoreWhitespace)
				{
					flag2 = true;
					flag4 = true;
				}
				if (this.ignoreComments && !settings.IgnoreComments)
				{
					flag3 = true;
					flag4 = true;
				}
				if (this.ignorePIs && !settings.IgnoreProcessingInstructions)
				{
					ignorePis = true;
					flag4 = true;
				}
				if ((this.dtdProcessing == DtdProcessing.Prohibit && settings.DtdProcessing != DtdProcessing.Prohibit) || (this.dtdProcessing == DtdProcessing.Ignore && settings.DtdProcessing == DtdProcessing.Parse))
				{
					dtdProcessing = this.dtdProcessing;
					flag4 = true;
				}
			}
			if (!flag4)
			{
				return baseReader;
			}
			IXmlNamespaceResolver xmlNamespaceResolver = baseReader as IXmlNamespaceResolver;
			if (xmlNamespaceResolver != null)
			{
				return new XmlCharCheckingReaderWithNS(baseReader, xmlNamespaceResolver, flag, flag2, flag3, ignorePis, dtdProcessing);
			}
			return new XmlCharCheckingReader(baseReader, flag, flag2, flag3, ignorePis, dtdProcessing);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00022408 File Offset: 0x00020608
		internal static bool EnableLegacyXmlSettings()
		{
			if (XmlReaderSettings.s_enableLegacyXmlSettings != null)
			{
				return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
			}
			if (!BinaryCompatibility.TargetsAtLeast_Desktop_V4_5_2)
			{
				XmlReaderSettings.s_enableLegacyXmlSettings = new bool?(true);
				return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
			}
			XmlReaderSettings.s_enableLegacyXmlSettings = new bool?(false);
			return XmlReaderSettings.s_enableLegacyXmlSettings.Value;
		}

		// Token: 0x04000890 RID: 2192
		private bool useAsync;

		// Token: 0x04000891 RID: 2193
		private XmlNameTable nameTable;

		// Token: 0x04000892 RID: 2194
		private XmlResolver xmlResolver;

		// Token: 0x04000893 RID: 2195
		private int lineNumberOffset;

		// Token: 0x04000894 RID: 2196
		private int linePositionOffset;

		// Token: 0x04000895 RID: 2197
		private ConformanceLevel conformanceLevel;

		// Token: 0x04000896 RID: 2198
		private bool checkCharacters;

		// Token: 0x04000897 RID: 2199
		private long maxCharactersInDocument;

		// Token: 0x04000898 RID: 2200
		private long maxCharactersFromEntities;

		// Token: 0x04000899 RID: 2201
		private bool ignoreWhitespace;

		// Token: 0x0400089A RID: 2202
		private bool ignorePIs;

		// Token: 0x0400089B RID: 2203
		private bool ignoreComments;

		// Token: 0x0400089C RID: 2204
		private DtdProcessing dtdProcessing;

		// Token: 0x0400089D RID: 2205
		private ValidationType validationType;

		// Token: 0x0400089E RID: 2206
		private XmlSchemaValidationFlags validationFlags;

		// Token: 0x0400089F RID: 2207
		private XmlSchemaSet schemas;

		// Token: 0x040008A0 RID: 2208
		private ValidationEventHandler valEventHandler;

		// Token: 0x040008A1 RID: 2209
		private bool closeInput;

		// Token: 0x040008A2 RID: 2210
		private bool isReadOnly;

		// Token: 0x040008A3 RID: 2211
		[CompilerGenerated]
		private bool <IsXmlResolverSet>k__BackingField;

		// Token: 0x040008A4 RID: 2212
		private static bool? s_enableLegacyXmlSettings;
	}
}
