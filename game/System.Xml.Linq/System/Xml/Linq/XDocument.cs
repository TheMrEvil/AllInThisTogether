using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML document. For the components and usage of an <see cref="T:System.Xml.Linq.XDocument" /> object, see XDocument Class Overview.</summary>
	// Token: 0x02000027 RID: 39
	public class XDocument : XContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class.</summary>
		// Token: 0x0600016E RID: 366 RVA: 0x00007A9D File Offset: 0x00005C9D
		public XDocument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class with the specified content.</summary>
		/// <param name="content">A parameter list of content objects to add to this document.</param>
		// Token: 0x0600016F RID: 367 RVA: 0x00007AA5 File Offset: 0x00005CA5
		public XDocument(params object[] content) : this()
		{
			base.AddContentSkipNotify(content);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class with the specified <see cref="T:System.Xml.Linq.XDeclaration" /> and content.</summary>
		/// <param name="declaration">An <see cref="T:System.Xml.Linq.XDeclaration" /> for the document.</param>
		/// <param name="content">The content of the document.</param>
		// Token: 0x06000170 RID: 368 RVA: 0x00007AB4 File Offset: 0x00005CB4
		public XDocument(XDeclaration declaration, params object[] content) : this(content)
		{
			this._declaration = declaration;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XDocument" /> class from an existing <see cref="T:System.Xml.Linq.XDocument" /> object.</summary>
		/// <param name="other">The <see cref="T:System.Xml.Linq.XDocument" /> object that will be copied.</param>
		// Token: 0x06000171 RID: 369 RVA: 0x00007AC4 File Offset: 0x00005CC4
		public XDocument(XDocument other) : base(other)
		{
			if (other._declaration != null)
			{
				this._declaration = new XDeclaration(other._declaration);
			}
		}

		/// <summary>Gets or sets the XML declaration for this document.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XDeclaration" /> that contains the XML declaration for this document.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00007AE6 File Offset: 0x00005CE6
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00007AEE File Offset: 0x00005CEE
		public XDeclaration Declaration
		{
			get
			{
				return this._declaration;
			}
			set
			{
				this._declaration = value;
			}
		}

		/// <summary>Gets the Document Type Definition (DTD) for this document.</summary>
		/// <returns>A <see cref="T:System.Xml.Linq.XDocumentType" /> that contains the DTD for this document.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00007AF7 File Offset: 0x00005CF7
		public XDocumentType DocumentType
		{
			get
			{
				return this.GetFirstNode<XDocumentType>();
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XDocument" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Document" />.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00007AFF File Offset: 0x00005CFF
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Document;
			}
		}

		/// <summary>Gets the root element of the XML Tree for this document.</summary>
		/// <returns>The root <see cref="T:System.Xml.Linq.XElement" /> of the XML tree.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00007B03 File Offset: 0x00005D03
		public XElement Root
		{
			get
			{
				return this.GetFirstNode<XElement>();
			}
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a file.</summary>
		/// <param name="uri">A URI string that references the file to load into a new <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified file.</returns>
		// Token: 0x06000177 RID: 375 RVA: 0x00007B0B File Offset: 0x00005D0B
		public static XDocument Load(string uri)
		{
			return XDocument.Load(uri, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a file, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="uri">A URI string that references the file to load into a new <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified file.</returns>
		// Token: 0x06000178 RID: 376 RVA: 0x00007B14 File Offset: 0x00005D14
		public static XDocument Load(string uri, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XDocument result;
			using (XmlReader xmlReader = XmlReader.Create(uri, xmlReaderSettings))
			{
				result = XDocument.Load(xmlReader, options);
			}
			return result;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> instance by using the specified stream.</summary>
		/// <param name="stream">The stream that contains the XML data.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> object that reads the data that is contained in the stream.</returns>
		// Token: 0x06000179 RID: 377 RVA: 0x00007B58 File Offset: 0x00005D58
		public static XDocument Load(Stream stream)
		{
			return XDocument.Load(stream, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> instance by using the specified stream, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="stream">The stream containing the XML data.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> object that reads the data that is contained in the stream.</returns>
		// Token: 0x0600017A RID: 378 RVA: 0x00007B64 File Offset: 0x00005D64
		public static XDocument Load(Stream stream, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XDocument result;
			using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
			{
				result = XDocument.Load(xmlReader, options);
			}
			return result;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007BA8 File Offset: 0x00005DA8
		public static Task<XDocument> LoadAsync(Stream stream, LoadOptions options, CancellationToken cancellationToken)
		{
			XDocument.<LoadAsync>d__18 <LoadAsync>d__;
			<LoadAsync>d__.stream = stream;
			<LoadAsync>d__.options = options;
			<LoadAsync>d__.cancellationToken = cancellationToken;
			<LoadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<XDocument>.Create();
			<LoadAsync>d__.<>1__state = -1;
			<LoadAsync>d__.<>t__builder.Start<XDocument.<LoadAsync>d__18>(ref <LoadAsync>d__);
			return <LoadAsync>d__.<>t__builder.Task;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified <see cref="T:System.IO.TextReader" />.</returns>
		// Token: 0x0600017C RID: 380 RVA: 0x00007BFB File Offset: 0x00005DFB
		public static XDocument Load(TextReader textReader)
		{
			return XDocument.Load(textReader, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a <see cref="T:System.IO.TextReader" />, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="textReader">A <see cref="T:System.IO.TextReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the XML that was read from the specified <see cref="T:System.IO.TextReader" />.</returns>
		// Token: 0x0600017D RID: 381 RVA: 0x00007C04 File Offset: 0x00005E04
		public static XDocument Load(TextReader textReader, LoadOptions options)
		{
			XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
			XDocument result;
			using (XmlReader xmlReader = XmlReader.Create(textReader, xmlReaderSettings))
			{
				result = XDocument.Load(xmlReader, options);
			}
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007C48 File Offset: 0x00005E48
		public static Task<XDocument> LoadAsync(TextReader textReader, LoadOptions options, CancellationToken cancellationToken)
		{
			XDocument.<LoadAsync>d__21 <LoadAsync>d__;
			<LoadAsync>d__.textReader = textReader;
			<LoadAsync>d__.options = options;
			<LoadAsync>d__.cancellationToken = cancellationToken;
			<LoadAsync>d__.<>t__builder = AsyncTaskMethodBuilder<XDocument>.Create();
			<LoadAsync>d__.<>1__state = -1;
			<LoadAsync>d__.<>t__builder.Start<XDocument.<LoadAsync>d__21>(ref <LoadAsync>d__);
			return <LoadAsync>d__.<>t__builder.Task;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from an <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that contains the content for the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the contents of the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		// Token: 0x0600017F RID: 383 RVA: 0x00007C9B File Offset: 0x00005E9B
		public static XDocument Load(XmlReader reader)
		{
			return XDocument.Load(reader, LoadOptions.None);
		}

		/// <summary>Loads an <see cref="T:System.Xml.Linq.XDocument" /> from an <see cref="T:System.Xml.XmlReader" />, optionally setting the base URI, and retaining line information.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" /> that will be read for the content of the <see cref="T:System.Xml.Linq.XDocument" />.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> that contains the XML that was read from the specified <see cref="T:System.Xml.XmlReader" />.</returns>
		// Token: 0x06000180 RID: 384 RVA: 0x00007CA4 File Offset: 0x00005EA4
		public static XDocument Load(XmlReader reader, LoadOptions options)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (reader.ReadState == ReadState.Initial)
			{
				reader.Read();
			}
			XDocument xdocument = XDocument.InitLoad(reader, options);
			xdocument.ReadContentFrom(reader, options);
			if (!reader.EOF)
			{
				throw new InvalidOperationException("The XmlReader state should be EndOfFile after this operation.");
			}
			if (xdocument.Root == null)
			{
				throw new InvalidOperationException("The root element is missing.");
			}
			return xdocument;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007D03 File Offset: 0x00005F03
		public static Task<XDocument> LoadAsync(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled<XDocument>(cancellationToken);
			}
			return XDocument.LoadAsyncInternal(reader, options, cancellationToken);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007D2C File Offset: 0x00005F2C
		private static Task<XDocument> LoadAsyncInternal(XmlReader reader, LoadOptions options, CancellationToken cancellationToken)
		{
			XDocument.<LoadAsyncInternal>d__25 <LoadAsyncInternal>d__;
			<LoadAsyncInternal>d__.reader = reader;
			<LoadAsyncInternal>d__.options = options;
			<LoadAsyncInternal>d__.cancellationToken = cancellationToken;
			<LoadAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder<XDocument>.Create();
			<LoadAsyncInternal>d__.<>1__state = -1;
			<LoadAsyncInternal>d__.<>t__builder.Start<XDocument.<LoadAsyncInternal>d__25>(ref <LoadAsyncInternal>d__);
			return <LoadAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007D80 File Offset: 0x00005F80
		private static XDocument InitLoad(XmlReader reader, LoadOptions options)
		{
			XDocument xdocument = new XDocument();
			if ((options & LoadOptions.SetBaseUri) != LoadOptions.None)
			{
				string baseURI = reader.BaseURI;
				if (!string.IsNullOrEmpty(baseURI))
				{
					xdocument.SetBaseUri(baseURI);
				}
			}
			if ((options & LoadOptions.SetLineInfo) != LoadOptions.None)
			{
				IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
				if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
				{
					xdocument.SetLineInfo(xmlLineInfo.LineNumber, xmlLineInfo.LinePosition);
				}
			}
			if (reader.NodeType == XmlNodeType.XmlDeclaration)
			{
				xdocument.Declaration = new XDeclaration(reader);
			}
			return xdocument;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a string.</summary>
		/// <param name="text">A string that contains XML.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> populated from the string that contains XML.</returns>
		// Token: 0x06000184 RID: 388 RVA: 0x00007DEE File Offset: 0x00005FEE
		public static XDocument Parse(string text)
		{
			return XDocument.Parse(text, LoadOptions.None);
		}

		/// <summary>Creates a new <see cref="T:System.Xml.Linq.XDocument" /> from a string, optionally preserving white space, setting the base URI, and retaining line information.</summary>
		/// <param name="text">A string that contains XML.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.LoadOptions" /> that specifies white space behavior, and whether to load base URI and line information.</param>
		/// <returns>An <see cref="T:System.Xml.Linq.XDocument" /> populated from the string that contains XML.</returns>
		// Token: 0x06000185 RID: 389 RVA: 0x00007DF8 File Offset: 0x00005FF8
		public static XDocument Parse(string text, LoadOptions options)
		{
			XDocument result;
			using (StringReader stringReader = new StringReader(text))
			{
				XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(options);
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					result = XDocument.Load(xmlReader, options);
				}
			}
			return result;
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XDocument" /> to the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XDocument" /> to.</param>
		// Token: 0x06000186 RID: 390 RVA: 0x00007E58 File Offset: 0x00006058
		public void Save(Stream stream)
		{
			this.Save(stream, base.GetSaveOptionsFromAnnotations());
		}

		/// <summary>Outputs this <see cref="T:System.Xml.Linq.XDocument" /> to the specified <see cref="T:System.IO.Stream" />, optionally specifying formatting behavior.</summary>
		/// <param name="stream">The stream to output this <see cref="T:System.Xml.Linq.XDocument" /> to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x06000187 RID: 391 RVA: 0x00007E68 File Offset: 0x00006068
		public void Save(Stream stream, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			if (this._declaration != null && !string.IsNullOrEmpty(this._declaration.Encoding))
			{
				try
				{
					xmlWriterSettings.Encoding = Encoding.GetEncoding(this._declaration.Encoding);
				}
				catch (ArgumentException)
				{
				}
			}
			using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007EE8 File Offset: 0x000060E8
		public Task SaveAsync(Stream stream, SaveOptions options, CancellationToken cancellationToken)
		{
			XDocument.<SaveAsync>d__31 <SaveAsync>d__;
			<SaveAsync>d__.<>4__this = this;
			<SaveAsync>d__.stream = stream;
			<SaveAsync>d__.options = options;
			<SaveAsync>d__.cancellationToken = cancellationToken;
			<SaveAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveAsync>d__.<>1__state = -1;
			<SaveAsync>d__.<>t__builder.Start<XDocument.<SaveAsync>d__31>(ref <SaveAsync>d__);
			return <SaveAsync>d__.<>t__builder.Task;
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="textWriter">A <see cref="T:System.IO.TextWriter" /> that the <see cref="T:System.Xml.Linq.XDocument" /> will be written to.</param>
		// Token: 0x06000189 RID: 393 RVA: 0x00007F43 File Offset: 0x00006143
		public void Save(TextWriter textWriter)
		{
			this.Save(textWriter, base.GetSaveOptionsFromAnnotations());
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a <see cref="T:System.IO.TextWriter" />, optionally disabling formatting.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> to output the XML to.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x0600018A RID: 394 RVA: 0x00007F54 File Offset: 0x00006154
		public void Save(TextWriter textWriter, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			using (XmlWriter xmlWriter = XmlWriter.Create(textWriter, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> that the <see cref="T:System.Xml.Linq.XDocument" /> will be written to.</param>
		// Token: 0x0600018B RID: 395 RVA: 0x00007F94 File Offset: 0x00006194
		public void Save(XmlWriter writer)
		{
			this.WriteTo(writer);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007FA0 File Offset: 0x000061A0
		public Task SaveAsync(TextWriter textWriter, SaveOptions options, CancellationToken cancellationToken)
		{
			XDocument.<SaveAsync>d__35 <SaveAsync>d__;
			<SaveAsync>d__.<>4__this = this;
			<SaveAsync>d__.textWriter = textWriter;
			<SaveAsync>d__.options = options;
			<SaveAsync>d__.cancellationToken = cancellationToken;
			<SaveAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveAsync>d__.<>1__state = -1;
			<SaveAsync>d__.<>t__builder.Start<XDocument.<SaveAsync>d__35>(ref <SaveAsync>d__);
			return <SaveAsync>d__.<>t__builder.Task;
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a file, overwriting an existing file, if it exists.</summary>
		/// <param name="fileName">A string that contains the name of the file.</param>
		// Token: 0x0600018D RID: 397 RVA: 0x00007FFB File Offset: 0x000061FB
		public void Save(string fileName)
		{
			this.Save(fileName, base.GetSaveOptionsFromAnnotations());
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000800A File Offset: 0x0000620A
		public Task SaveAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			return this.WriteToAsync(writer, cancellationToken);
		}

		/// <summary>Serialize this <see cref="T:System.Xml.Linq.XDocument" /> to a file, optionally disabling formatting.</summary>
		/// <param name="fileName">A string that contains the name of the file.</param>
		/// <param name="options">A <see cref="T:System.Xml.Linq.SaveOptions" /> that specifies formatting behavior.</param>
		// Token: 0x0600018F RID: 399 RVA: 0x00008014 File Offset: 0x00006214
		public void Save(string fileName, SaveOptions options)
		{
			XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(options);
			if (this._declaration != null && !string.IsNullOrEmpty(this._declaration.Encoding))
			{
				try
				{
					xmlWriterSettings.Encoding = Encoding.GetEncoding(this._declaration.Encoding);
				}
				catch (ArgumentException)
				{
				}
			}
			using (XmlWriter xmlWriter = XmlWriter.Create(fileName, xmlWriterSettings))
			{
				this.Save(xmlWriter);
			}
		}

		/// <summary>Write this document to an <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Xml.XmlWriter" /> into which this method will write.</param>
		// Token: 0x06000190 RID: 400 RVA: 0x00008094 File Offset: 0x00006294
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (this._declaration != null && this._declaration.Standalone == "yes")
			{
				writer.WriteStartDocument(true);
			}
			else if (this._declaration != null && this._declaration.Standalone == "no")
			{
				writer.WriteStartDocument(false);
			}
			else
			{
				writer.WriteStartDocument();
			}
			base.WriteContentTo(writer);
			writer.WriteEndDocument();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008112 File Offset: 0x00006312
		public override Task WriteToAsync(XmlWriter writer, CancellationToken cancellationToken)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Task.FromCanceled(cancellationToken);
			}
			return this.WriteToAsyncInternal(writer, cancellationToken);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000813C File Offset: 0x0000633C
		private Task WriteToAsyncInternal(XmlWriter writer, CancellationToken cancellationToken)
		{
			XDocument.<WriteToAsyncInternal>d__41 <WriteToAsyncInternal>d__;
			<WriteToAsyncInternal>d__.<>4__this = this;
			<WriteToAsyncInternal>d__.writer = writer;
			<WriteToAsyncInternal>d__.cancellationToken = cancellationToken;
			<WriteToAsyncInternal>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteToAsyncInternal>d__.<>1__state = -1;
			<WriteToAsyncInternal>d__.<>t__builder.Start<XDocument.<WriteToAsyncInternal>d__41>(ref <WriteToAsyncInternal>d__);
			return <WriteToAsyncInternal>d__.<>t__builder.Task;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000818F File Offset: 0x0000638F
		internal override void AddAttribute(XAttribute a)
		{
			throw new ArgumentException("An attribute cannot be added to content.");
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000818F File Offset: 0x0000638F
		internal override void AddAttributeSkipNotify(XAttribute a)
		{
			throw new ArgumentException("An attribute cannot be added to content.");
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000819B File Offset: 0x0000639B
		internal override XNode CloneNode()
		{
			return new XDocument(this);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000081A4 File Offset: 0x000063A4
		internal override bool DeepEquals(XNode node)
		{
			XDocument xdocument = node as XDocument;
			return xdocument != null && base.ContentsEqual(xdocument);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000081C4 File Offset: 0x000063C4
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000081CC File Offset: 0x000063CC
		private T GetFirstNode<T>() where T : XNode
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				T t;
				for (;;)
				{
					xnode = xnode.next;
					t = (xnode as T);
					if (t != null)
					{
						break;
					}
					if (xnode == this.content)
					{
						goto IL_35;
					}
				}
				return t;
			}
			IL_35:
			return default(T);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008218 File Offset: 0x00006418
		internal static bool IsWhitespace(string s)
		{
			foreach (char c in s)
			{
				if (c != ' ' && c != '\t' && c != '\r' && c != '\n')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008258 File Offset: 0x00006458
		internal override void ValidateNode(XNode node, XNode previous)
		{
			XmlNodeType nodeType = node.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				this.ValidateDocument(previous, XmlNodeType.DocumentType, XmlNodeType.None);
				return;
			case XmlNodeType.Attribute:
				return;
			case XmlNodeType.Text:
				this.ValidateString(((XText)node).Value);
				return;
			case XmlNodeType.CDATA:
				throw new ArgumentException(SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.CDATA));
			default:
				if (nodeType == XmlNodeType.Document)
				{
					throw new ArgumentException(SR.Format("A node of type {0} cannot be added to content.", XmlNodeType.Document));
				}
				if (nodeType != XmlNodeType.DocumentType)
				{
					return;
				}
				this.ValidateDocument(previous, XmlNodeType.None, XmlNodeType.Element);
				return;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000082E4 File Offset: 0x000064E4
		private void ValidateDocument(XNode previous, XmlNodeType allowBefore, XmlNodeType allowAfter)
		{
			XNode xnode = this.content as XNode;
			if (xnode != null)
			{
				if (previous == null)
				{
					allowBefore = allowAfter;
				}
				for (;;)
				{
					xnode = xnode.next;
					XmlNodeType nodeType = xnode.NodeType;
					if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.DocumentType)
					{
						if (nodeType != allowBefore)
						{
							break;
						}
						allowBefore = XmlNodeType.None;
					}
					if (xnode == previous)
					{
						allowBefore = allowAfter;
					}
					if (xnode == this.content)
					{
						return;
					}
				}
				throw new InvalidOperationException("This operation would create an incorrectly structured document.");
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000833F File Offset: 0x0000653F
		internal override void ValidateString(string s)
		{
			if (!XDocument.IsWhitespace(s))
			{
				throw new ArgumentException("Non-whitespace characters cannot be added to content.");
			}
		}

		// Token: 0x040000C8 RID: 200
		private XDeclaration _declaration;

		// Token: 0x02000028 RID: 40
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <LoadAsync>d__18 : IAsyncStateMachine
		{
			// Token: 0x0600019D RID: 413 RVA: 0x00008354 File Offset: 0x00006554
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XDocument result;
				try
				{
					if (num != 0)
					{
						XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(this.options);
						xmlReaderSettings.Async = true;
						this.<r>5__2 = XmlReader.Create(this.stream, xmlReaderSettings);
					}
					try
					{
						ConfiguredTaskAwaitable<XDocument>.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = XDocument.LoadAsync(this.<r>5__2, this.options, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<XDocument>.ConfiguredTaskAwaiter, XDocument.<LoadAsync>d__18>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<XDocument>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						result = awaiter.GetResult();
					}
					finally
					{
						if (num < 0 && this.<r>5__2 != null)
						{
							((IDisposable)this.<r>5__2).Dispose();
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
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600019E RID: 414 RVA: 0x0000846C File Offset: 0x0000666C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000C9 RID: 201
			public int <>1__state;

			// Token: 0x040000CA RID: 202
			public AsyncTaskMethodBuilder<XDocument> <>t__builder;

			// Token: 0x040000CB RID: 203
			public LoadOptions options;

			// Token: 0x040000CC RID: 204
			public Stream stream;

			// Token: 0x040000CD RID: 205
			public CancellationToken cancellationToken;

			// Token: 0x040000CE RID: 206
			private XmlReader <r>5__2;

			// Token: 0x040000CF RID: 207
			private ConfiguredTaskAwaitable<XDocument>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000029 RID: 41
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <LoadAsync>d__21 : IAsyncStateMachine
		{
			// Token: 0x0600019F RID: 415 RVA: 0x0000847C File Offset: 0x0000667C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XDocument result;
				try
				{
					if (num != 0)
					{
						XmlReaderSettings xmlReaderSettings = XNode.GetXmlReaderSettings(this.options);
						xmlReaderSettings.Async = true;
						this.<r>5__2 = XmlReader.Create(this.textReader, xmlReaderSettings);
					}
					try
					{
						ConfiguredTaskAwaitable<XDocument>.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = XDocument.LoadAsync(this.<r>5__2, this.options, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<XDocument>.ConfiguredTaskAwaiter, XDocument.<LoadAsync>d__21>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<XDocument>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						result = awaiter.GetResult();
					}
					finally
					{
						if (num < 0 && this.<r>5__2 != null)
						{
							((IDisposable)this.<r>5__2).Dispose();
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
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060001A0 RID: 416 RVA: 0x00008594 File Offset: 0x00006794
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000D0 RID: 208
			public int <>1__state;

			// Token: 0x040000D1 RID: 209
			public AsyncTaskMethodBuilder<XDocument> <>t__builder;

			// Token: 0x040000D2 RID: 210
			public LoadOptions options;

			// Token: 0x040000D3 RID: 211
			public TextReader textReader;

			// Token: 0x040000D4 RID: 212
			public CancellationToken cancellationToken;

			// Token: 0x040000D5 RID: 213
			private XmlReader <r>5__2;

			// Token: 0x040000D6 RID: 214
			private ConfiguredTaskAwaitable<XDocument>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200002A RID: 42
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <LoadAsyncInternal>d__25 : IAsyncStateMachine
		{
			// Token: 0x060001A1 RID: 417 RVA: 0x000085A4 File Offset: 0x000067A4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XDocument result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_117;
						}
						if (this.reader.ReadState != ReadState.Initial)
						{
							goto IL_88;
						}
						awaiter2 = this.reader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XDocument.<LoadAsyncInternal>d__25>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					IL_88:
					this.<d>5__2 = XDocument.InitLoad(this.reader, this.options);
					awaiter = this.<d>5__2.ReadContentFromAsync(this.reader, this.options, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XDocument.<LoadAsyncInternal>d__25>(ref awaiter, ref this);
						return;
					}
					IL_117:
					awaiter.GetResult();
					if (!this.reader.EOF)
					{
						throw new InvalidOperationException("The XmlReader state should be EndOfFile after this operation.");
					}
					if (this.<d>5__2.Root == null)
					{
						throw new InvalidOperationException("The root element is missing.");
					}
					result = this.<d>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<d>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<d>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060001A2 RID: 418 RVA: 0x00008760 File Offset: 0x00006960
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000D7 RID: 215
			public int <>1__state;

			// Token: 0x040000D8 RID: 216
			public AsyncTaskMethodBuilder<XDocument> <>t__builder;

			// Token: 0x040000D9 RID: 217
			public XmlReader reader;

			// Token: 0x040000DA RID: 218
			public LoadOptions options;

			// Token: 0x040000DB RID: 219
			public CancellationToken cancellationToken;

			// Token: 0x040000DC RID: 220
			private XDocument <d>5__2;

			// Token: 0x040000DD RID: 221
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040000DE RID: 222
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200002B RID: 43
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SaveAsync>d__31 : IAsyncStateMachine
		{
			// Token: 0x060001A3 RID: 419 RVA: 0x00008770 File Offset: 0x00006970
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XDocument xdocument = this.<>4__this;
				try
				{
					if (num != 0)
					{
						XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(this.options);
						xmlWriterSettings.Async = true;
						if (xdocument._declaration != null && !string.IsNullOrEmpty(xdocument._declaration.Encoding))
						{
							try
							{
								xmlWriterSettings.Encoding = Encoding.GetEncoding(xdocument._declaration.Encoding);
							}
							catch (ArgumentException)
							{
							}
						}
						this.<w>5__2 = XmlWriter.Create(this.stream, xmlWriterSettings);
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = xdocument.WriteToAsync(this.<w>5__2, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XDocument.<SaveAsync>d__31>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
					}
					finally
					{
						if (num < 0 && this.<w>5__2 != null)
						{
							((IDisposable)this.<w>5__2).Dispose();
						}
					}
					this.<w>5__2 = null;
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

			// Token: 0x060001A4 RID: 420 RVA: 0x000088D4 File Offset: 0x00006AD4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000DF RID: 223
			public int <>1__state;

			// Token: 0x040000E0 RID: 224
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040000E1 RID: 225
			public SaveOptions options;

			// Token: 0x040000E2 RID: 226
			public XDocument <>4__this;

			// Token: 0x040000E3 RID: 227
			public Stream stream;

			// Token: 0x040000E4 RID: 228
			public CancellationToken cancellationToken;

			// Token: 0x040000E5 RID: 229
			private XmlWriter <w>5__2;

			// Token: 0x040000E6 RID: 230
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200002C RID: 44
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SaveAsync>d__35 : IAsyncStateMachine
		{
			// Token: 0x060001A5 RID: 421 RVA: 0x000088E4 File Offset: 0x00006AE4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XDocument xdocument = this.<>4__this;
				try
				{
					if (num != 0)
					{
						XmlWriterSettings xmlWriterSettings = XNode.GetXmlWriterSettings(this.options);
						xmlWriterSettings.Async = true;
						this.<w>5__2 = XmlWriter.Create(this.textWriter, xmlWriterSettings);
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = xdocument.WriteToAsync(this.<w>5__2, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XDocument.<SaveAsync>d__35>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
					}
					finally
					{
						if (num < 0 && this.<w>5__2 != null)
						{
							((IDisposable)this.<w>5__2).Dispose();
						}
					}
					this.<w>5__2 = null;
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

			// Token: 0x060001A6 RID: 422 RVA: 0x00008A04 File Offset: 0x00006C04
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000E7 RID: 231
			public int <>1__state;

			// Token: 0x040000E8 RID: 232
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040000E9 RID: 233
			public SaveOptions options;

			// Token: 0x040000EA RID: 234
			public TextWriter textWriter;

			// Token: 0x040000EB RID: 235
			public XDocument <>4__this;

			// Token: 0x040000EC RID: 236
			public CancellationToken cancellationToken;

			// Token: 0x040000ED RID: 237
			private XmlWriter <w>5__2;

			// Token: 0x040000EE RID: 238
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200002D RID: 45
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteToAsyncInternal>d__41 : IAsyncStateMachine
		{
			// Token: 0x060001A7 RID: 423 RVA: 0x00008A14 File Offset: 0x00006C14
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XDocument xdocument = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_152;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1B9;
					default:
					{
						Task task;
						if (xdocument._declaration != null && xdocument._declaration.Standalone == "yes")
						{
							task = this.writer.WriteStartDocumentAsync(true);
						}
						else if (xdocument._declaration != null && xdocument._declaration.Standalone == "no")
						{
							task = this.writer.WriteStartDocumentAsync(false);
						}
						else
						{
							task = this.writer.WriteStartDocumentAsync();
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XDocument.<WriteToAsyncInternal>d__41>(ref awaiter, ref this);
							return;
						}
						break;
					}
					}
					awaiter.GetResult();
					awaiter = xdocument.WriteContentToAsync(this.writer, this.cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XDocument.<WriteToAsyncInternal>d__41>(ref awaiter, ref this);
						return;
					}
					IL_152:
					awaiter.GetResult();
					awaiter = this.writer.WriteEndDocumentAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XDocument.<WriteToAsyncInternal>d__41>(ref awaiter, ref this);
						return;
					}
					IL_1B9:
					awaiter.GetResult();
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

			// Token: 0x060001A8 RID: 424 RVA: 0x00008C2C File Offset: 0x00006E2C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040000EF RID: 239
			public int <>1__state;

			// Token: 0x040000F0 RID: 240
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040000F1 RID: 241
			public XDocument <>4__this;

			// Token: 0x040000F2 RID: 242
			public XmlWriter writer;

			// Token: 0x040000F3 RID: 243
			public CancellationToken cancellationToken;

			// Token: 0x040000F4 RID: 244
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
