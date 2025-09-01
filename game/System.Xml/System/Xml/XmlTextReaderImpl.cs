using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XmlConfiguration;

namespace System.Xml
{
	// Token: 0x020000BC RID: 188
	internal class XmlTextReaderImpl : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x00026E40 File Offset: 0x00025040
		internal XmlTextReaderImpl()
		{
			this.curNode = new XmlTextReaderImpl.NodeData();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.NoData;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00026EC8 File Offset: 0x000250C8
		internal XmlTextReaderImpl(XmlNameTable nt)
		{
			this.v1Compat = true;
			this.outerReader = this;
			this.nameTable = nt;
			nt.Add(string.Empty);
			if (!XmlReaderSettings.EnableLegacyXmlSettings())
			{
				this.xmlResolver = null;
			}
			else
			{
				this.xmlResolver = new XmlUrlResolver();
			}
			this.Xml = nt.Add("xml");
			this.XmlNs = nt.Add("xmlns");
			this.nodes = new XmlTextReaderImpl.NodeData[8];
			this.nodes[0] = new XmlTextReaderImpl.NodeData();
			this.curNode = this.nodes[0];
			this.stringBuilder = new StringBuilder();
			this.xmlContext = new XmlTextReaderImpl.XmlContext();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl;
			this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			this.entityHandling = EntityHandling.ExpandCharEntities;
			this.whitespaceHandling = WhitespaceHandling.All;
			this.closeInput = true;
			this.maxCharactersInDocument = 0L;
			this.maxCharactersFromEntities = 10000000L;
			this.charactersInDocument = 0L;
			this.charactersFromEntities = 0L;
			this.ps.lineNo = 1;
			this.ps.lineStartPos = -1;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00027038 File Offset: 0x00025238
		private XmlTextReaderImpl(XmlResolver resolver, XmlReaderSettings settings, XmlParserContext context)
		{
			this.useAsync = settings.Async;
			this.v1Compat = false;
			this.outerReader = this;
			this.xmlContext = new XmlTextReaderImpl.XmlContext();
			XmlNameTable xmlNameTable = settings.NameTable;
			if (context == null)
			{
				if (xmlNameTable == null)
				{
					xmlNameTable = new NameTable();
				}
				else
				{
					this.nameTableFromSettings = true;
				}
				this.nameTable = xmlNameTable;
				this.namespaceManager = new XmlNamespaceManager(xmlNameTable);
			}
			else
			{
				this.SetupFromParserContext(context, settings);
				xmlNameTable = this.nameTable;
			}
			xmlNameTable.Add(string.Empty);
			this.Xml = xmlNameTable.Add("xml");
			this.XmlNs = xmlNameTable.Add("xmlns");
			this.xmlResolver = resolver;
			this.nodes = new XmlTextReaderImpl.NodeData[8];
			this.nodes[0] = new XmlTextReaderImpl.NodeData();
			this.curNode = this.nodes[0];
			this.stringBuilder = new StringBuilder();
			this.entityHandling = EntityHandling.ExpandEntities;
			this.xmlResolverIsSet = settings.IsXmlResolverSet;
			this.whitespaceHandling = (settings.IgnoreWhitespace ? WhitespaceHandling.Significant : WhitespaceHandling.All);
			this.normalize = true;
			this.ignorePIs = settings.IgnoreProcessingInstructions;
			this.ignoreComments = settings.IgnoreComments;
			this.checkCharacters = settings.CheckCharacters;
			this.lineNumberOffset = settings.LineNumberOffset;
			this.linePositionOffset = settings.LinePositionOffset;
			this.ps.lineNo = this.lineNumberOffset + 1;
			this.ps.lineStartPos = -this.linePositionOffset - 1;
			this.curNode.SetLineInfo(this.ps.LineNo - 1, this.ps.LinePos - 1);
			this.dtdProcessing = settings.DtdProcessing;
			this.maxCharactersInDocument = settings.MaxCharactersInDocument;
			this.maxCharactersFromEntities = settings.MaxCharactersFromEntities;
			this.charactersInDocument = 0L;
			this.charactersFromEntities = 0L;
			this.fragmentParserContext = context;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl;
			this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			switch (settings.ConformanceLevel)
			{
			case ConformanceLevel.Auto:
				this.fragmentType = XmlNodeType.None;
				this.fragment = true;
				return;
			case ConformanceLevel.Fragment:
				this.fragmentType = XmlNodeType.Element;
				this.fragment = true;
				return;
			}
			this.fragmentType = XmlNodeType.Document;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000272B5 File Offset: 0x000254B5
		internal XmlTextReaderImpl(Stream input) : this(string.Empty, input, new NameTable())
		{
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000272C8 File Offset: 0x000254C8
		internal XmlTextReaderImpl(Stream input, XmlNameTable nt) : this(string.Empty, input, nt)
		{
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000272D7 File Offset: 0x000254D7
		internal XmlTextReaderImpl(string url, Stream input) : this(url, input, new NameTable())
		{
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000272E8 File Offset: 0x000254E8
		internal XmlTextReaderImpl(string url, Stream input, XmlNameTable nt) : this(nt)
		{
			this.namespaceManager = new XmlNamespaceManager(nt);
			if (url == null || url.Length == 0)
			{
				this.InitStreamInput(input, null);
			}
			else
			{
				this.InitStreamInput(url, input, null);
			}
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00027348 File Offset: 0x00025548
		internal XmlTextReaderImpl(TextReader input) : this(string.Empty, input, new NameTable())
		{
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0002735B File Offset: 0x0002555B
		internal XmlTextReaderImpl(TextReader input, XmlNameTable nt) : this(string.Empty, input, nt)
		{
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0002736A File Offset: 0x0002556A
		internal XmlTextReaderImpl(string url, TextReader input) : this(url, input, new NameTable())
		{
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002737C File Offset: 0x0002557C
		internal XmlTextReaderImpl(string url, TextReader input, XmlNameTable nt) : this(nt)
		{
			this.namespaceManager = new XmlNamespaceManager(nt);
			this.reportedBaseUri = ((url != null) ? url : string.Empty);
			this.InitTextReaderInput(this.reportedBaseUri, input);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000273CC File Offset: 0x000255CC
		internal XmlTextReaderImpl(Stream xmlFragment, XmlNodeType fragType, XmlParserContext context) : this((context != null && context.NameTable != null) ? context.NameTable : new NameTable())
		{
			Encoding encoding = (context != null) ? context.Encoding : null;
			if (context == null || context.BaseURI == null || context.BaseURI.Length == 0)
			{
				this.InitStreamInput(xmlFragment, encoding);
			}
			else
			{
				this.InitStreamInput(this.GetTempResolver().ResolveUri(null, context.BaseURI), xmlFragment, encoding);
			}
			this.InitFragmentReader(fragType, context, false);
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0002746C File Offset: 0x0002566C
		internal XmlTextReaderImpl(string xmlFragment, XmlNodeType fragType, XmlParserContext context) : this((context == null || context.NameTable == null) ? new NameTable() : context.NameTable)
		{
			if (xmlFragment == null)
			{
				xmlFragment = string.Empty;
			}
			if (context == null)
			{
				this.InitStringInput(string.Empty, Encoding.Unicode, xmlFragment);
			}
			else
			{
				this.reportedBaseUri = context.BaseURI;
				this.InitStringInput(context.BaseURI, Encoding.Unicode, xmlFragment);
			}
			this.InitFragmentReader(fragType, context, false);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000274F0 File Offset: 0x000256F0
		internal XmlTextReaderImpl(string xmlFragment, XmlParserContext context) : this((context == null || context.NameTable == null) ? new NameTable() : context.NameTable)
		{
			this.InitStringInput((context == null) ? string.Empty : context.BaseURI, Encoding.Unicode, "<?xml " + xmlFragment + "?>");
			this.InitFragmentReader(XmlNodeType.XmlDeclaration, context, true);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00027550 File Offset: 0x00025750
		public XmlTextReaderImpl(string url) : this(url, new NameTable())
		{
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00027560 File Offset: 0x00025760
		public XmlTextReaderImpl(string url, XmlNameTable nt) : this(nt)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new ArgumentException(Res.GetString("The URL cannot be empty."), "url");
			}
			this.namespaceManager = new XmlNamespaceManager(nt);
			this.url = url;
			this.ps.baseUri = this.GetTempResolver().ResolveUri(null, url);
			this.ps.baseUriStr = this.ps.baseUri.ToString();
			this.reportedBaseUri = this.ps.baseUriStr;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.OpenUrl;
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00027600 File Offset: 0x00025800
		internal XmlTextReaderImpl(string uriStr, XmlReaderSettings settings, XmlParserContext context, XmlResolver uriResolver) : this(settings.GetXmlResolver(), settings, context)
		{
			Uri uri = uriResolver.ResolveUri(null, uriStr);
			string text = uri.ToString();
			if (context != null && context.BaseURI != null && context.BaseURI.Length > 0 && !this.UriEqual(uri, text, context.BaseURI, settings.GetXmlResolver()))
			{
				if (text.Length > 0)
				{
					this.Throw("BaseUri must be specified either as an argument of XmlReader.Create or on the XmlParserContext. If it is specified on both, it must be the same base URI.");
				}
				text = context.BaseURI;
			}
			this.reportedBaseUri = text;
			this.closeInput = true;
			this.laterInitParam = new XmlTextReaderImpl.LaterInitParam();
			this.laterInitParam.inputUriStr = uriStr;
			this.laterInitParam.inputbaseUri = uri;
			this.laterInitParam.inputContext = context;
			this.laterInitParam.inputUriResolver = uriResolver;
			this.laterInitParam.initType = XmlTextReaderImpl.InitInputType.UriString;
			if (!settings.Async)
			{
				this.FinishInitUriString();
				return;
			}
			this.laterInitParam.useAsync = true;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x000276E8 File Offset: 0x000258E8
		private void FinishInitUriString()
		{
			Stream stream = null;
			if (this.laterInitParam.useAsync)
			{
				Task<object> entityAsync = this.laterInitParam.inputUriResolver.GetEntityAsync(this.laterInitParam.inputbaseUri, string.Empty, typeof(Stream));
				entityAsync.Wait();
				stream = (Stream)entityAsync.Result;
			}
			else
			{
				stream = (Stream)this.laterInitParam.inputUriResolver.GetEntity(this.laterInitParam.inputbaseUri, string.Empty, typeof(Stream));
			}
			if (stream == null)
			{
				throw new XmlException("Cannot resolve '{0}'.", this.laterInitParam.inputUriStr);
			}
			Encoding encoding = null;
			if (this.laterInitParam.inputContext != null)
			{
				encoding = this.laterInitParam.inputContext.Encoding;
			}
			try
			{
				this.InitStreamInput(this.laterInitParam.inputbaseUri, this.reportedBaseUri, stream, null, 0, encoding);
				this.reportedEncoding = this.ps.encoding;
				if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
				{
					this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
				}
			}
			catch
			{
				stream.Close();
				throw;
			}
			this.laterInitParam = null;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002782C File Offset: 0x00025A2C
		internal XmlTextReaderImpl(Stream stream, byte[] bytes, int byteCount, XmlReaderSettings settings, Uri baseUri, string baseUriStr, XmlParserContext context, bool closeInput) : this(settings.GetXmlResolver(), settings, context)
		{
			if (context != null && context.BaseURI != null && context.BaseURI.Length > 0 && !this.UriEqual(baseUri, baseUriStr, context.BaseURI, settings.GetXmlResolver()))
			{
				if (baseUriStr.Length > 0)
				{
					this.Throw("BaseUri must be specified either as an argument of XmlReader.Create or on the XmlParserContext. If it is specified on both, it must be the same base URI.");
				}
				baseUriStr = context.BaseURI;
			}
			this.reportedBaseUri = baseUriStr;
			this.closeInput = closeInput;
			this.laterInitParam = new XmlTextReaderImpl.LaterInitParam();
			this.laterInitParam.inputStream = stream;
			this.laterInitParam.inputBytes = bytes;
			this.laterInitParam.inputByteCount = byteCount;
			this.laterInitParam.inputbaseUri = baseUri;
			this.laterInitParam.inputContext = context;
			this.laterInitParam.initType = XmlTextReaderImpl.InitInputType.Stream;
			if (!settings.Async)
			{
				this.FinishInitStream();
				return;
			}
			this.laterInitParam.useAsync = true;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00027920 File Offset: 0x00025B20
		private void FinishInitStream()
		{
			Encoding encoding = null;
			if (this.laterInitParam.inputContext != null)
			{
				encoding = this.laterInitParam.inputContext.Encoding;
			}
			this.InitStreamInput(this.laterInitParam.inputbaseUri, this.reportedBaseUri, this.laterInitParam.inputStream, this.laterInitParam.inputBytes, this.laterInitParam.inputByteCount, encoding);
			this.reportedEncoding = this.ps.encoding;
			if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
			{
				this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
			}
			this.laterInitParam = null;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x000279D0 File Offset: 0x00025BD0
		internal XmlTextReaderImpl(TextReader input, XmlReaderSettings settings, string baseUriStr, XmlParserContext context) : this(settings.GetXmlResolver(), settings, context)
		{
			if (context != null && context.BaseURI != null)
			{
				baseUriStr = context.BaseURI;
			}
			this.reportedBaseUri = baseUriStr;
			this.closeInput = settings.CloseInput;
			this.laterInitParam = new XmlTextReaderImpl.LaterInitParam();
			this.laterInitParam.inputTextReader = input;
			this.laterInitParam.inputContext = context;
			this.laterInitParam.initType = XmlTextReaderImpl.InitInputType.TextReader;
			if (!settings.Async)
			{
				this.FinishInitTextReader();
				return;
			}
			this.laterInitParam.useAsync = true;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00027A60 File Offset: 0x00025C60
		private void FinishInitTextReader()
		{
			this.InitTextReaderInput(this.reportedBaseUri, this.laterInitParam.inputTextReader);
			this.reportedEncoding = this.ps.encoding;
			if (this.laterInitParam.inputContext != null && this.laterInitParam.inputContext.HasDtdInfo)
			{
				this.ProcessDtdFromParserContext(this.laterInitParam.inputContext);
			}
			this.laterInitParam = null;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00027ACC File Offset: 0x00025CCC
		internal XmlTextReaderImpl(string xmlFragment, XmlParserContext context, XmlReaderSettings settings) : this(null, settings, context)
		{
			this.InitStringInput(string.Empty, Encoding.Unicode, xmlFragment);
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00027B0C File Offset: 0x00025D0C
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				if (this.nameTableFromSettings)
				{
					xmlReaderSettings.NameTable = this.nameTable;
				}
				XmlNodeType xmlNodeType = this.fragmentType;
				if (xmlNodeType != XmlNodeType.None)
				{
					if (xmlNodeType == XmlNodeType.Element)
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
						goto IL_46;
					}
					if (xmlNodeType == XmlNodeType.Document)
					{
						xmlReaderSettings.ConformanceLevel = ConformanceLevel.Document;
						goto IL_46;
					}
				}
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
				IL_46:
				xmlReaderSettings.CheckCharacters = this.checkCharacters;
				xmlReaderSettings.LineNumberOffset = this.lineNumberOffset;
				xmlReaderSettings.LinePositionOffset = this.linePositionOffset;
				xmlReaderSettings.IgnoreWhitespace = (this.whitespaceHandling == WhitespaceHandling.Significant);
				xmlReaderSettings.IgnoreProcessingInstructions = this.ignorePIs;
				xmlReaderSettings.IgnoreComments = this.ignoreComments;
				xmlReaderSettings.DtdProcessing = this.dtdProcessing;
				xmlReaderSettings.MaxCharactersInDocument = this.maxCharactersInDocument;
				xmlReaderSettings.MaxCharactersFromEntities = this.maxCharactersFromEntities;
				if (!XmlReaderSettings.EnableLegacyXmlSettings())
				{
					xmlReaderSettings.XmlResolver = this.xmlResolver;
				}
				xmlReaderSettings.ReadOnly = true;
				return xmlReaderSettings;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00027BE9 File Offset: 0x00025DE9
		public override XmlNodeType NodeType
		{
			get
			{
				return this.curNode.type;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00027BF6 File Offset: 0x00025DF6
		public override string Name
		{
			get
			{
				return this.curNode.GetNameWPrefix(this.nameTable);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00027C09 File Offset: 0x00025E09
		public override string LocalName
		{
			get
			{
				return this.curNode.localName;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00027C16 File Offset: 0x00025E16
		public override string NamespaceURI
		{
			get
			{
				return this.curNode.ns;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x00027C23 File Offset: 0x00025E23
		public override string Prefix
		{
			get
			{
				return this.curNode.prefix;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00027C30 File Offset: 0x00025E30
		public override string Value
		{
			get
			{
				if (this.parsingFunction >= XmlTextReaderImpl.ParsingFunction.PartialTextValue)
				{
					if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
					{
						this.FinishPartialValue();
						this.parsingFunction = this.nextParsingFunction;
					}
					else
					{
						this.FinishOtherValueIterator();
					}
				}
				return this.curNode.StringValue;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x00027C6B File Offset: 0x00025E6B
		public override int Depth
		{
			get
			{
				return this.curNode.depth;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00027C78 File Offset: 0x00025E78
		public override string BaseURI
		{
			get
			{
				return this.reportedBaseUri;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x00027C80 File Offset: 0x00025E80
		public override bool IsEmptyElement
		{
			get
			{
				return this.curNode.IsEmptyElement;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00027C8D File Offset: 0x00025E8D
		public override bool IsDefault
		{
			get
			{
				return this.curNode.IsDefaultAttribute;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x00027C9A File Offset: 0x00025E9A
		public override char QuoteChar
		{
			get
			{
				if (this.curNode.type != XmlNodeType.Attribute)
				{
					return '"';
				}
				return this.curNode.quoteChar;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00027CB8 File Offset: 0x00025EB8
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xmlContext.xmlSpace;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00027CC5 File Offset: 0x00025EC5
		public override string XmlLang
		{
			get
			{
				return this.xmlContext.xmlLang;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00027CD2 File Offset: 0x00025ED2
		public override ReadState ReadState
		{
			get
			{
				return this.readState;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x00027CDA File Offset: 0x00025EDA
		public override bool EOF
		{
			get
			{
				return this.parsingFunction == XmlTextReaderImpl.ParsingFunction.Eof;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00027CE6 File Offset: 0x00025EE6
		public override XmlNameTable NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanResolveEntity
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00027CEE File Offset: 0x00025EEE
		public override int AttributeCount
		{
			get
			{
				return this.attrCount;
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00027CF8 File Offset: 0x00025EF8
		public override string GetAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetIndexOfAttributeWithoutPrefix(name);
			}
			else
			{
				num = this.GetIndexOfAttributeWithPrefix(name);
			}
			if (num < 0)
			{
				return null;
			}
			return this.nodes[num].StringValue;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00027D38 File Offset: 0x00025F38
		public override string GetAttribute(string localName, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.nameTable.Get(namespaceURI));
			localName = this.nameTable.Get(localName);
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, localName) && Ref.Equal(this.nodes[i].ns, namespaceURI))
				{
					return this.nodes[i].StringValue;
				}
			}
			return null;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00027DC5 File Offset: 0x00025FC5
		public override string GetAttribute(int i)
		{
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			return this.nodes[this.index + i + 1].StringValue;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00027DF8 File Offset: 0x00025FF8
		public override bool MoveToAttribute(string name)
		{
			int num;
			if (name.IndexOf(':') == -1)
			{
				num = this.GetIndexOfAttributeWithoutPrefix(name);
			}
			else
			{
				num = this.GetIndexOfAttributeWithPrefix(name);
			}
			if (num >= 0)
			{
				if (this.InAttributeValueIterator)
				{
					this.FinishAttributeValueIterator();
				}
				this.curAttrIndex = num - this.index - 1;
				this.curNode = this.nodes[num];
				return true;
			}
			return false;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00027E58 File Offset: 0x00026058
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			namespaceURI = ((namespaceURI == null) ? string.Empty : this.nameTable.Get(namespaceURI));
			localName = this.nameTable.Get(localName);
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, localName) && Ref.Equal(this.nodes[i].ns, namespaceURI))
				{
					this.curAttrIndex = i - this.index - 1;
					this.curNode = this.nodes[i];
					if (this.InAttributeValueIterator)
					{
						this.FinishAttributeValueIterator();
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00027F08 File Offset: 0x00026108
		public override void MoveToAttribute(int i)
		{
			if (i < 0 || i >= this.attrCount)
			{
				throw new ArgumentOutOfRangeException("i");
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			this.curAttrIndex = i;
			this.curNode = this.nodes[this.index + 1 + this.curAttrIndex];
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00027F5E File Offset: 0x0002615E
		public override bool MoveToFirstAttribute()
		{
			if (this.attrCount == 0)
			{
				return false;
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			this.curAttrIndex = 0;
			this.curNode = this.nodes[this.index + 1];
			return true;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00027F98 File Offset: 0x00026198
		public override bool MoveToNextAttribute()
		{
			if (this.curAttrIndex + 1 < this.attrCount)
			{
				if (this.InAttributeValueIterator)
				{
					this.FinishAttributeValueIterator();
				}
				XmlTextReaderImpl.NodeData[] array = this.nodes;
				int num = this.index + 1;
				int num2 = this.curAttrIndex + 1;
				this.curAttrIndex = num2;
				this.curNode = array[num + num2];
				return true;
			}
			return false;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00027FED File Offset: 0x000261ED
		public override bool MoveToElement()
		{
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
			}
			else if (this.curNode.type != XmlNodeType.Attribute)
			{
				return false;
			}
			this.curAttrIndex = -1;
			this.curNode = this.nodes[this.index];
			return true;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0002802C File Offset: 0x0002622C
		private void FinishInit()
		{
			switch (this.laterInitParam.initType)
			{
			case XmlTextReaderImpl.InitInputType.UriString:
				this.FinishInitUriString();
				return;
			case XmlTextReaderImpl.InitInputType.Stream:
				this.FinishInitStream();
				return;
			case XmlTextReaderImpl.InitInputType.TextReader:
				this.FinishInitTextReader();
				return;
			default:
				return;
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002806C File Offset: 0x0002626C
		public override bool Read()
		{
			if (this.laterInitParam != null)
			{
				this.FinishInit();
			}
			for (;;)
			{
				switch (this.parsingFunction)
				{
				case XmlTextReaderImpl.ParsingFunction.ElementContent:
					goto IL_85;
				case XmlTextReaderImpl.ParsingFunction.NoData:
					goto IL_2E7;
				case XmlTextReaderImpl.ParsingFunction.OpenUrl:
					this.OpenUrl();
					break;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractive:
					this.readState = ReadState.Interactive;
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl:
					break;
				case XmlTextReaderImpl.ParsingFunction.DocumentContent:
					goto IL_8C;
				case XmlTextReaderImpl.ParsingFunction.MoveToElementContent:
					this.ResetAttributes();
					this.index++;
					this.curNode = this.AddNode(this.index, this.index);
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ElementContent;
					continue;
				case XmlTextReaderImpl.ParsingFunction.PopElementContext:
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext:
					this.curNode = this.nodes[this.index];
					this.curNode.IsEmptyElement = false;
					this.ResetAttributes();
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel:
					this.ResetAttributes();
					this.curNode = this.nodes[this.index];
					this.parsingFunction = ((this.index == 0) ? XmlTextReaderImpl.ParsingFunction.DocumentContent : XmlTextReaderImpl.ParsingFunction.ElementContent);
					continue;
				case XmlTextReaderImpl.ParsingFunction.Error:
				case XmlTextReaderImpl.ParsingFunction.Eof:
				case XmlTextReaderImpl.ParsingFunction.ReaderClosed:
					return false;
				case XmlTextReaderImpl.ParsingFunction.EntityReference:
					goto IL_1B3;
				case XmlTextReaderImpl.ParsingFunction.InIncrementalRead:
					goto IL_2BE;
				case XmlTextReaderImpl.ParsingFunction.FragmentAttribute:
					goto IL_2C6;
				case XmlTextReaderImpl.ParsingFunction.ReportEndEntity:
					goto IL_1C7;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent:
					this.curNode = this.AddNode(this.index, this.index);
					this.reportedEncoding = this.ps.encoding;
					this.reportedBaseUri = this.ps.baseUriStr;
					this.parsingFunction = this.nextParsingFunction;
					continue;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent:
					goto IL_226;
				case XmlTextReaderImpl.ParsingFunction.XmlDeclarationFragment:
					goto IL_2CD;
				case XmlTextReaderImpl.ParsingFunction.GoToEof:
					goto IL_2DD;
				case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
					this.SkipPartialTextValue();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
					this.FinishAttributeValueIterator();
					this.curNode = this.nodes[this.index];
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
					this.FinishReadValueChunk();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
					this.FinishReadContentAsBinary();
					continue;
				case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
					this.FinishReadElementContentAsBinary();
					continue;
				default:
					continue;
				}
				this.readState = ReadState.Interactive;
				this.parsingFunction = this.nextParsingFunction;
				if (this.ParseXmlDeclaration(false))
				{
					goto Block_3;
				}
				this.reportedEncoding = this.ps.encoding;
			}
			IL_85:
			return this.ParseElementContent();
			IL_8C:
			return this.ParseDocumentContent();
			Block_3:
			this.reportedEncoding = this.ps.encoding;
			return true;
			IL_1B3:
			this.parsingFunction = this.nextParsingFunction;
			this.ParseEntityReference();
			return true;
			IL_1C7:
			this.SetupEndEntityNodeInContent();
			this.parsingFunction = this.nextParsingFunction;
			return true;
			IL_226:
			this.curNode = this.AddNode(this.index, this.index);
			this.curNode.SetValueNode(XmlNodeType.Text, string.Empty);
			this.curNode.SetLineInfo(this.ps.lineNo, this.ps.LinePos);
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			this.parsingFunction = this.nextParsingFunction;
			return true;
			IL_2BE:
			this.FinishIncrementalRead();
			return true;
			IL_2C6:
			return this.ParseFragmentAttribute();
			IL_2CD:
			this.ParseXmlDeclarationFragment();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.GoToEof;
			return true;
			IL_2DD:
			this.OnEof();
			return false;
			IL_2E7:
			this.ThrowWithoutLineInfo("Root element is missing.");
			return false;
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00028398 File Offset: 0x00026598
		public override void Close()
		{
			this.Close(this.closeInput);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000283A8 File Offset: 0x000265A8
		public override void Skip()
		{
			if (this.readState != ReadState.Interactive)
			{
				return;
			}
			if (this.InAttributeValueIterator)
			{
				this.FinishAttributeValueIterator();
				this.curNode = this.nodes[this.index];
			}
			else
			{
				XmlTextReaderImpl.ParsingFunction parsingFunction = this.parsingFunction;
				if (parsingFunction != XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
				{
					switch (parsingFunction)
					{
					case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
						this.SkipPartialTextValue();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
						this.FinishReadValueChunk();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
						this.FinishReadContentAsBinary();
						break;
					case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
						this.FinishReadElementContentAsBinary();
						break;
					}
				}
				else
				{
					this.FinishIncrementalRead();
				}
			}
			XmlNodeType type = this.curNode.type;
			if (type != XmlNodeType.Element)
			{
				if (type != XmlNodeType.Attribute)
				{
					goto IL_DC;
				}
				this.outerReader.MoveToElement();
			}
			if (!this.curNode.IsEmptyElement)
			{
				int num = this.index;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipContent;
				while (this.outerReader.Read() && this.index > num)
				{
				}
				this.parsingMode = XmlTextReaderImpl.ParsingMode.Full;
			}
			IL_DC:
			this.outerReader.Read();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002849D File Offset: 0x0002669D
		public override string LookupNamespace(string prefix)
		{
			if (!this.supportNamespaces)
			{
				return null;
			}
			return this.namespaceManager.LookupNamespace(prefix);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000284B8 File Offset: 0x000266B8
		public override bool ReadAttributeValue()
		{
			if (this.parsingFunction != XmlTextReaderImpl.ParsingFunction.InReadAttributeValue)
			{
				if (this.curNode.type != XmlNodeType.Attribute)
				{
					return false;
				}
				if (this.readState != ReadState.Interactive || this.curAttrIndex < 0)
				{
					return false;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
				{
					this.FinishReadValueChunk();
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					this.FinishReadContentAsBinary();
				}
				if (this.curNode.nextAttrValueChunk == null || this.entityHandling == EntityHandling.ExpandEntities)
				{
					XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, this.curNode.depth + 1);
					nodeData.SetValueNode(XmlNodeType.Text, this.curNode.StringValue);
					nodeData.lineInfo = this.curNode.lineInfo2;
					nodeData.depth = this.curNode.depth + 1;
					this.curNode = nodeData;
					nodeData.nextAttrValueChunk = null;
				}
				else
				{
					this.curNode = this.curNode.nextAttrValueChunk;
					this.AddNode(this.index + this.attrCount + 1, this.index + 2);
					this.nodes[this.index + this.attrCount + 1] = this.curNode;
					this.fullAttrCleanup = true;
				}
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
				this.attributeValueBaseEntityId = this.ps.entityId;
				return true;
			}
			else
			{
				if (this.ps.entityId != this.attributeValueBaseEntityId)
				{
					return this.ParseAttributeValueChunk();
				}
				if (this.curNode.nextAttrValueChunk != null)
				{
					this.curNode = this.curNode.nextAttrValueChunk;
					this.nodes[this.index + this.attrCount + 1] = this.curNode;
					return true;
				}
				return false;
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00028668 File Offset: 0x00026868
		public override void ResolveEntity()
		{
			if (this.curNode.type != XmlNodeType.EntityReference)
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadAttributeValue || this.parsingFunction == XmlTextReaderImpl.ParsingFunction.FragmentAttribute)
			{
				switch (this.HandleGeneralEntityReference(this.curNode.localName, true, true, this.curNode.LinePos))
				{
				case XmlTextReaderImpl.EntityType.Expanded:
				case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
					if (this.ps.charsUsed - this.ps.charPos == 0)
					{
						this.emptyEntityInAttributeResolved = true;
						goto IL_164;
					}
					goto IL_164;
				case XmlTextReaderImpl.EntityType.FakeExpanded:
					this.emptyEntityInAttributeResolved = true;
					goto IL_164;
				}
				throw new XmlException("An internal error has occurred.", string.Empty);
			}
			switch (this.HandleGeneralEntityReference(this.curNode.localName, false, true, this.curNode.LinePos))
			{
			case XmlTextReaderImpl.EntityType.Expanded:
			case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
				this.nextParsingFunction = this.parsingFunction;
				if (this.ps.charsUsed - this.ps.charPos == 0 && !this.ps.entity.IsExternal)
				{
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent;
					goto IL_164;
				}
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent;
				goto IL_164;
			case XmlTextReaderImpl.EntityType.FakeExpanded:
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent;
				goto IL_164;
			}
			throw new XmlException("An internal error has occurred.", string.Empty);
			IL_164:
			this.ps.entityResolvedManually = true;
			this.index++;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x000287F3 File Offset: 0x000269F3
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x000287FB File Offset: 0x000269FB
		internal XmlReader OuterReader
		{
			get
			{
				return this.outerReader;
			}
			set
			{
				this.outerReader = value;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00028804 File Offset: 0x00026A04
		internal void MoveOffEntityReference()
		{
			if (this.outerReader.NodeType == XmlNodeType.EntityReference && this.parsingFunction == XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent && !this.outerReader.Read())
			{
				throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002883B File Offset: 0x00026A3B
		public override string ReadString()
		{
			this.MoveOffEntityReference();
			return base.ReadString();
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002884C File Offset: 0x00026A4C
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (!XmlReader.CanReadContentAs(this.curNode.type))
				{
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				}
				if (!this.InitReadContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBase64Decoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00028918 File Offset: 0x00026B18
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				if (this.incReadDecoder == this.binHexDecoder)
				{
					return this.ReadContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (!XmlReader.CanReadContentAs(this.curNode.type))
				{
					throw base.CreateReadContentAsException("ReadContentAsBinHex");
				}
				if (!this.InitReadContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBinHexDecoder();
			return this.ReadContentAsBinary(buffer, index, count);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000289E4 File Offset: 0x00026BE4
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (this.curNode.type != XmlNodeType.Element)
				{
					throw base.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				if (!this.InitReadElementContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBase64Decoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00028AAC File Offset: 0x00026CAC
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
			{
				if (this.incReadDecoder == this.binHexDecoder)
				{
					return this.ReadElementContentAsBinary(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (this.curNode.type != XmlNodeType.Element)
				{
					throw base.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				if (!this.InitReadElementContentAsBinary())
				{
					return 0;
				}
			}
			this.InitBinHexDecoder();
			return this.ReadElementContentAsBinary(buffer, index, count);
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0001222F File Offset: 0x0001042F
		public override bool CanReadValueChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00028B74 File Offset: 0x00026D74
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			if (!XmlReader.HasValueInternal(this.curNode.type))
			{
				throw new InvalidOperationException(Res.GetString("The ReadValueAsChunk method is not supported on node type {0}.", new object[]
				{
					this.curNode.type
				}));
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction != XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
				{
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue;
				}
				else
				{
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue;
					this.nextNextParsingFunction = this.nextParsingFunction;
					this.nextParsingFunction = this.parsingFunction;
				}
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadValueChunk;
				this.readValueOffset = 0;
			}
			if (count == 0)
			{
				return 0;
			}
			int num = 0;
			int num2 = this.curNode.CopyTo(this.readValueOffset, buffer, index + num, count - num);
			num += num2;
			this.readValueOffset += num2;
			if (num == count)
			{
				if (XmlCharType.IsHighSurrogate((int)buffer[index + count - 1]))
				{
					num--;
					this.readValueOffset--;
					if (num == 0)
					{
						this.Throw("The buffer is not large enough to fit a surrogate pair. Please provide a buffer of size at least 2 characters.");
					}
				}
				return num;
			}
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
			{
				this.curNode.SetValue(string.Empty);
				bool flag = false;
				int num3 = 0;
				int num4 = 0;
				while (num < count && !flag)
				{
					int num5 = 0;
					flag = this.ParseText(out num3, out num4, ref num5);
					int num6 = count - num;
					if (num6 > num4 - num3)
					{
						num6 = num4 - num3;
					}
					XmlTextReaderImpl.BlockCopyChars(this.ps.chars, num3, buffer, index + num, num6);
					num += num6;
					num3 += num6;
				}
				this.incReadState = (flag ? XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue : XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue);
				if (num == count && XmlCharType.IsHighSurrogate((int)buffer[index + count - 1]))
				{
					num--;
					num3--;
					if (num == 0)
					{
						this.Throw("The buffer is not large enough to fit a surrogate pair. Please provide a buffer of size at least 2 characters.");
					}
				}
				this.readValueOffset = 0;
				this.curNode.SetValue(this.ps.chars, num3, num4 - num3);
			}
			return num;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001222F File Offset: 0x0001042F
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00028D84 File Offset: 0x00026F84
		public int LineNumber
		{
			get
			{
				return this.curNode.LineNo;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00028D91 File Offset: 0x00026F91
		public int LinePosition
		{
			get
			{
				return this.curNode.LinePos;
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00028D9E File Offset: 0x00026F9E
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.GetNamespacesInScope(scope);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00028DA7 File Offset: 0x00026FA7
		string IXmlNamespaceResolver.LookupNamespace(string prefix)
		{
			return this.LookupNamespace(prefix);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00028DB0 File Offset: 0x00026FB0
		string IXmlNamespaceResolver.LookupPrefix(string namespaceName)
		{
			return this.LookupPrefix(namespaceName);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00028DB9 File Offset: 0x00026FB9
		internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
		{
			return this.namespaceManager.GetNamespacesInScope(scope);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00028DC7 File Offset: 0x00026FC7
		internal string LookupPrefix(string namespaceName)
		{
			return this.namespaceManager.LookupPrefix(namespaceName);
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00028DD5 File Offset: 0x00026FD5
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00028DE0 File Offset: 0x00026FE0
		internal bool Namespaces
		{
			get
			{
				return this.supportNamespaces;
			}
			set
			{
				if (this.readState != ReadState.Initial)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				this.supportNamespaces = value;
				if (value)
				{
					if (this.namespaceManager is XmlTextReaderImpl.NoNamespaceManager)
					{
						if (this.fragment && this.fragmentParserContext != null && this.fragmentParserContext.NamespaceManager != null)
						{
							this.namespaceManager = this.fragmentParserContext.NamespaceManager;
						}
						else
						{
							this.namespaceManager = new XmlNamespaceManager(this.nameTable);
						}
					}
					this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
					return;
				}
				if (!(this.namespaceManager is XmlTextReaderImpl.NoNamespaceManager))
				{
					this.namespaceManager = new XmlTextReaderImpl.NoNamespaceManager();
				}
				this.xmlContext.defaultNamespace = string.Empty;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00028EA1 File Offset: 0x000270A1
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00028EAC File Offset: 0x000270AC
		internal bool Normalization
		{
			get
			{
				return this.normalize;
			}
			set
			{
				if (this.readState == ReadState.Closed)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				this.normalize = value;
				if (this.ps.entity == null || this.ps.entity.IsExternal)
				{
					this.ps.eolNormalized = !value;
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00028F07 File Offset: 0x00027107
		internal Encoding Encoding
		{
			get
			{
				if (this.readState != ReadState.Interactive)
				{
					return null;
				}
				return this.reportedEncoding;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00028F1A File Offset: 0x0002711A
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x00028F22 File Offset: 0x00027122
		internal WhitespaceHandling WhitespaceHandling
		{
			get
			{
				return this.whitespaceHandling;
			}
			set
			{
				if (this.readState == ReadState.Closed)
				{
					throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
				}
				if (value > WhitespaceHandling.None)
				{
					throw new XmlException("Expected WhitespaceHandling.None, or WhitespaceHandling.All, or WhitespaceHandling.Significant.", string.Empty);
				}
				this.whitespaceHandling = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00028F58 File Offset: 0x00027158
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00028F60 File Offset: 0x00027160
		internal DtdProcessing DtdProcessing
		{
			get
			{
				return this.dtdProcessing;
			}
			set
			{
				if (value > DtdProcessing.Parse)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.dtdProcessing = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00028F78 File Offset: 0x00027178
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x00028F80 File Offset: 0x00027180
		internal EntityHandling EntityHandling
		{
			get
			{
				return this.entityHandling;
			}
			set
			{
				if (value != EntityHandling.ExpandEntities && value != EntityHandling.ExpandCharEntities)
				{
					throw new XmlException("Expected EntityHandling.ExpandEntities or EntityHandling.ExpandCharEntities.", string.Empty);
				}
				this.entityHandling = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00028FA1 File Offset: 0x000271A1
		internal bool IsResolverSet
		{
			get
			{
				return this.xmlResolverIsSet;
			}
		}

		// Token: 0x1700013E RID: 318
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00028FAC File Offset: 0x000271AC
		internal XmlResolver XmlResolver
		{
			set
			{
				this.xmlResolver = value;
				this.xmlResolverIsSet = true;
				this.ps.baseUri = null;
				for (int i = 0; i <= this.parsingStatesStackTop; i++)
				{
					this.parsingStatesStack[i].baseUri = null;
				}
			}
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00028FF8 File Offset: 0x000271F8
		internal void ResetState()
		{
			if (this.fragment)
			{
				this.Throw(new InvalidOperationException(Res.GetString("Cannot call ResetState when parsing an XML fragment.")));
			}
			if (this.readState == ReadState.Initial)
			{
				return;
			}
			this.ResetAttributes();
			while (this.namespaceManager.PopScope())
			{
			}
			while (this.InEntity)
			{
				this.HandleEntityEnd(true);
			}
			this.readState = ReadState.Initial;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl;
			this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			this.curNode = this.nodes[0];
			this.curNode.Clear(XmlNodeType.None);
			this.curNode.SetLineInfo(0, 0);
			this.index = 0;
			this.rootElementParsed = false;
			this.charactersInDocument = 0L;
			this.charactersFromEntities = 0L;
			this.afterResetState = true;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000290B4 File Offset: 0x000272B4
		internal TextReader GetRemainder()
		{
			XmlTextReaderImpl.ParsingFunction parsingFunction = this.parsingFunction;
			if (parsingFunction != XmlTextReaderImpl.ParsingFunction.OpenUrl)
			{
				if (parsingFunction - XmlTextReaderImpl.ParsingFunction.Eof <= 1)
				{
					return new StringReader(string.Empty);
				}
				if (parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
				{
					if (!this.InEntity)
					{
						this.stringBuilder.Append(this.ps.chars, this.incReadLeftStartPos, this.incReadLeftEndPos - this.incReadLeftStartPos);
					}
				}
			}
			else
			{
				this.OpenUrl();
			}
			while (this.InEntity)
			{
				this.HandleEntityEnd(true);
			}
			this.ps.appendMode = false;
			do
			{
				this.stringBuilder.Append(this.ps.chars, this.ps.charPos, this.ps.charsUsed - this.ps.charPos);
				this.ps.charPos = this.ps.charsUsed;
			}
			while (this.ReadData() != 0);
			this.OnEof();
			string s = this.stringBuilder.ToString();
			this.stringBuilder.Length = 0;
			return new StringReader(s);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000291B8 File Offset: 0x000273B8
		internal int ReadChars(char[] buffer, int index, int count)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				if (this.incReadDecoder != this.readCharsDecoder)
				{
					if (this.readCharsDecoder == null)
					{
						this.readCharsDecoder = new IncrementalReadCharsDecoder();
					}
					this.readCharsDecoder.Reset();
					this.incReadDecoder = this.readCharsDecoder;
				}
				return this.IncrementalRead(buffer, index, count);
			}
			if (this.curNode.type != XmlNodeType.Element)
			{
				return 0;
			}
			if (this.curNode.IsEmptyElement)
			{
				this.outerReader.Read();
				return 0;
			}
			if (this.readCharsDecoder == null)
			{
				this.readCharsDecoder = new IncrementalReadCharsDecoder();
			}
			this.InitIncrementalRead(this.readCharsDecoder);
			return this.IncrementalRead(buffer, index, count);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00029264 File Offset: 0x00027464
		internal int ReadBase64(byte[] array, int offset, int len)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				if (this.incReadDecoder != this.base64Decoder)
				{
					this.InitBase64Decoder();
				}
				return this.IncrementalRead(array, offset, len);
			}
			if (this.curNode.type != XmlNodeType.Element)
			{
				return 0;
			}
			if (this.curNode.IsEmptyElement)
			{
				this.outerReader.Read();
				return 0;
			}
			if (this.base64Decoder == null)
			{
				this.base64Decoder = new Base64Decoder();
			}
			this.InitIncrementalRead(this.base64Decoder);
			return this.IncrementalRead(array, offset, len);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000292EC File Offset: 0x000274EC
		internal int ReadBinHex(byte[] array, int offset, int len)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				if (this.incReadDecoder != this.binHexDecoder)
				{
					this.InitBinHexDecoder();
				}
				return this.IncrementalRead(array, offset, len);
			}
			if (this.curNode.type != XmlNodeType.Element)
			{
				return 0;
			}
			if (this.curNode.IsEmptyElement)
			{
				this.outerReader.Read();
				return 0;
			}
			if (this.binHexDecoder == null)
			{
				this.binHexDecoder = new BinHexDecoder();
			}
			this.InitIncrementalRead(this.binHexDecoder);
			return this.IncrementalRead(array, offset, len);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00027CE6 File Offset: 0x00025EE6
		internal XmlNameTable DtdParserProxy_NameTable
		{
			get
			{
				return this.nameTable;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00029374 File Offset: 0x00027574
		internal IXmlNamespaceResolver DtdParserProxy_NamespaceResolver
		{
			get
			{
				return this.namespaceManager;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0002937C File Offset: 0x0002757C
		internal bool DtdParserProxy_DtdValidation
		{
			get
			{
				return this.DtdValidation;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00028EA1 File Offset: 0x000270A1
		internal bool DtdParserProxy_Normalization
		{
			get
			{
				return this.normalize;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00028DD5 File Offset: 0x00026FD5
		internal bool DtdParserProxy_Namespaces
		{
			get
			{
				return this.supportNamespaces;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00029384 File Offset: 0x00027584
		internal bool DtdParserProxy_V1CompatibilityMode
		{
			get
			{
				return this.v1Compat;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0002938C File Offset: 0x0002758C
		internal Uri DtdParserProxy_BaseUri
		{
			get
			{
				if (this.ps.baseUriStr.Length > 0 && this.ps.baseUri == null && this.xmlResolver != null)
				{
					this.ps.baseUri = this.xmlResolver.ResolveUri(null, this.ps.baseUriStr);
				}
				return this.ps.baseUri;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x000293F4 File Offset: 0x000275F4
		internal bool DtdParserProxy_IsEof
		{
			get
			{
				return this.ps.isEof;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00029401 File Offset: 0x00027601
		internal char[] DtdParserProxy_ParsingBuffer
		{
			get
			{
				return this.ps.chars;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x0002940E File Offset: 0x0002760E
		internal int DtdParserProxy_ParsingBufferLength
		{
			get
			{
				return this.ps.charsUsed;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002941B File Offset: 0x0002761B
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00029428 File Offset: 0x00027628
		internal int DtdParserProxy_CurrentPosition
		{
			get
			{
				return this.ps.charPos;
			}
			set
			{
				this.ps.charPos = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00029436 File Offset: 0x00027636
		internal int DtdParserProxy_EntityStackLength
		{
			get
			{
				return this.parsingStatesStackTop + 1;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00029440 File Offset: 0x00027640
		internal bool DtdParserProxy_IsEntityEolNormalized
		{
			get
			{
				return this.ps.eolNormalized;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0002944D File Offset: 0x0002764D
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00029455 File Offset: 0x00027655
		internal IValidationEventHandling DtdParserProxy_ValidationEventHandling
		{
			get
			{
				return this.validationEventHandling;
			}
			set
			{
				this.validationEventHandling = value;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002945E File Offset: 0x0002765E
		internal void DtdParserProxy_OnNewLine(int pos)
		{
			this.OnNewLine(pos);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x00029467 File Offset: 0x00027667
		internal int DtdParserProxy_LineNo
		{
			get
			{
				return this.ps.LineNo;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00029474 File Offset: 0x00027674
		internal int DtdParserProxy_LineStartPosition
		{
			get
			{
				return this.ps.lineStartPos;
			}
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00029481 File Offset: 0x00027681
		internal int DtdParserProxy_ReadData()
		{
			return this.ReadData();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002948C File Offset: 0x0002768C
		internal int DtdParserProxy_ParseNumericCharRef(StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.EntityType entityType;
			return this.ParseNumericCharRef(true, internalSubsetBuilder, out entityType);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000294A3 File Offset: 0x000276A3
		internal int DtdParserProxy_ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
		{
			return this.ParseNamedCharRef(expand, internalSubsetBuilder);
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000294B0 File Offset: 0x000276B0
		internal void DtdParserProxy_ParsePI(StringBuilder sb)
		{
			if (sb == null)
			{
				XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
				this.ParsePI(null);
				this.parsingMode = parsingMode;
				return;
			}
			this.ParsePI(sb);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000294E8 File Offset: 0x000276E8
		internal void DtdParserProxy_ParseComment(StringBuilder sb)
		{
			try
			{
				if (sb == null)
				{
					XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
					this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
					this.ParseCDataOrComment(XmlNodeType.Comment);
					this.parsingMode = parsingMode;
				}
				else
				{
					XmlTextReaderImpl.NodeData nodeData = this.curNode;
					this.curNode = this.AddNode(this.index + this.attrCount + 1, this.index);
					this.ParseCDataOrComment(XmlNodeType.Comment);
					this.curNode.CopyTo(0, sb);
					this.curNode = nodeData;
				}
			}
			catch (XmlException ex)
			{
				if (!(ex.ResString == "Unexpected end of file while parsing {0} has occurred.") || this.ps.entity == null)
				{
					throw;
				}
				this.SendValidationEvent(XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", null, this.ps.LineNo, this.ps.LinePos);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000295B4 File Offset: 0x000277B4
		private bool IsResolverNull
		{
			get
			{
				return this.xmlResolver == null || (XmlReaderSection.ProhibitDefaultUrlResolver && !this.xmlResolverIsSet);
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x000295D2 File Offset: 0x000277D2
		private XmlResolver GetTempResolver()
		{
			if (this.xmlResolver != null)
			{
				return this.xmlResolver;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000295E8 File Offset: 0x000277E8
		internal bool DtdParserProxy_PushEntity(IDtdEntityInfo entity, out int entityId)
		{
			bool result;
			if (entity.IsExternal)
			{
				if (this.IsResolverNull)
				{
					entityId = -1;
					return false;
				}
				result = this.PushExternalEntity(entity);
			}
			else
			{
				this.PushInternalEntity(entity);
				result = true;
			}
			entityId = this.ps.entityId;
			return result;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002962B File Offset: 0x0002782B
		internal bool DtdParserProxy_PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId)
		{
			if (this.parsingStatesStackTop == -1)
			{
				oldEntity = null;
				newEntityId = -1;
				return false;
			}
			oldEntity = this.ps.entity;
			this.PopEntity();
			newEntityId = this.ps.entityId;
			return true;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00029660 File Offset: 0x00027860
		internal bool DtdParserProxy_PushExternalSubset(string systemId, string publicId)
		{
			if (this.IsResolverNull)
			{
				return false;
			}
			if (this.ps.baseUri == null && !string.IsNullOrEmpty(this.ps.baseUriStr))
			{
				this.ps.baseUri = this.xmlResolver.ResolveUri(null, this.ps.baseUriStr);
			}
			this.PushExternalEntityOrSubset(publicId, systemId, this.ps.baseUri, null);
			this.ps.entity = null;
			this.ps.entityId = 0;
			int charPos = this.ps.charPos;
			if (this.v1Compat)
			{
				this.EatWhitespaces(null);
			}
			if (!this.ParseXmlDeclaration(true))
			{
				this.ps.charPos = charPos;
			}
			return true;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002971C File Offset: 0x0002791C
		internal void DtdParserProxy_PushInternalDtd(string baseUri, string internalDtd)
		{
			this.PushParsingState();
			this.RegisterConsumedCharacters((long)internalDtd.Length, false);
			this.InitStringInput(baseUri, Encoding.Unicode, internalDtd);
			this.ps.entity = null;
			this.ps.entityId = 0;
			this.ps.eolNormalized = false;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0002976E File Offset: 0x0002796E
		internal void DtdParserProxy_Throw(Exception e)
		{
			this.Throw(e);
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00029777 File Offset: 0x00027977
		internal void DtdParserProxy_OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttributeNoChecks("SYSTEM", this.index + 1);
			nodeData.SetValue(systemId);
			nodeData.lineInfo = keywordLineInfo;
			nodeData.lineInfo2 = systemLiteralLineInfo;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000297A0 File Offset: 0x000279A0
		internal void DtdParserProxy_OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttributeNoChecks("PUBLIC", this.index + 1);
			nodeData.SetValue(publicId);
			nodeData.lineInfo = keywordLineInfo;
			nodeData.lineInfo2 = publicLiteralLineInfo;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000297C9 File Offset: 0x000279C9
		private void Throw(int pos, string res, string arg)
		{
			this.ps.charPos = pos;
			this.Throw(res, arg);
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000297DF File Offset: 0x000279DF
		private void Throw(int pos, string res, string[] args)
		{
			this.ps.charPos = pos;
			this.Throw(res, args);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000297F5 File Offset: 0x000279F5
		private void Throw(int pos, string res)
		{
			this.ps.charPos = pos;
			this.Throw(res, string.Empty);
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0002980F File Offset: 0x00027A0F
		private void Throw(string res)
		{
			this.Throw(res, string.Empty);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002981D File Offset: 0x00027A1D
		private void Throw(string res, int lineNo, int linePos)
		{
			this.Throw(new XmlException(res, string.Empty, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0002983D File Offset: 0x00027A3D
		private void Throw(string res, string arg)
		{
			this.Throw(new XmlException(res, arg, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002986D File Offset: 0x00027A6D
		private void Throw(string res, string arg, int lineNo, int linePos)
		{
			this.Throw(new XmlException(res, arg, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x0002988A File Offset: 0x00027A8A
		private void Throw(string res, string[] args)
		{
			this.Throw(new XmlException(res, args, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000298BA File Offset: 0x00027ABA
		private void Throw(string res, string arg, Exception innerException)
		{
			this.Throw(res, new string[]
			{
				arg
			}, innerException);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x000298CE File Offset: 0x00027ACE
		private void Throw(string res, string[] args, Exception innerException)
		{
			this.Throw(new XmlException(res, args, innerException, this.ps.LineNo, this.ps.LinePos, this.ps.baseUriStr));
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00029900 File Offset: 0x00027B00
		private void Throw(Exception e)
		{
			this.SetErrorState();
			XmlException ex = e as XmlException;
			if (ex != null)
			{
				this.curNode.SetLineInfo(ex.LineNumber, ex.LinePosition);
			}
			throw e;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00029935 File Offset: 0x00027B35
		private void ReThrow(Exception e, int lineNo, int linePos)
		{
			this.Throw(new XmlException(e.Message, null, lineNo, linePos, this.ps.baseUriStr));
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00029956 File Offset: 0x00027B56
		private void ThrowWithoutLineInfo(string res)
		{
			this.Throw(new XmlException(res, string.Empty, this.ps.baseUriStr));
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00029974 File Offset: 0x00027B74
		private void ThrowWithoutLineInfo(string res, string arg)
		{
			this.Throw(new XmlException(res, arg, this.ps.baseUriStr));
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002998E File Offset: 0x00027B8E
		private void ThrowWithoutLineInfo(string res, string[] args, Exception innerException)
		{
			this.Throw(new XmlException(res, args, innerException, 0, 0, this.ps.baseUriStr));
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000299AB File Offset: 0x00027BAB
		private void ThrowInvalidChar(char[] data, int length, int invCharPos)
		{
			this.Throw(invCharPos, "'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, length, invCharPos));
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000299C1 File Offset: 0x00027BC1
		private void SetErrorState()
		{
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.Error;
			this.readState = ReadState.Error;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000299D2 File Offset: 0x00027BD2
		private void SendValidationEvent(XmlSeverityType severity, string code, string arg, int lineNo, int linePos)
		{
			this.SendValidationEvent(severity, new XmlSchemaException(code, arg, this.ps.baseUriStr, lineNo, linePos));
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000299F1 File Offset: 0x00027BF1
		private void SendValidationEvent(XmlSeverityType severity, XmlSchemaException exception)
		{
			if (this.validationEventHandling != null)
			{
				this.validationEventHandling.SendEvent(exception, severity);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00029A08 File Offset: 0x00027C08
		private bool InAttributeValueIterator
		{
			get
			{
				return this.attrCount > 0 && this.parsingFunction >= XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00029A24 File Offset: 0x00027C24
		private void FinishAttributeValueIterator()
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				this.FinishReadValueChunk();
			}
			else if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				this.FinishReadContentAsBinary();
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadAttributeValue)
			{
				while (this.ps.entityId != this.attributeValueBaseEntityId)
				{
					this.HandleEntityEnd(false);
				}
				this.emptyEntityInAttributeResolved = false;
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = ((this.index > 0) ? XmlTextReaderImpl.ParsingFunction.ElementContent : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00029AA0 File Offset: 0x00027CA0
		private bool DtdValidation
		{
			get
			{
				return this.validationEventHandling != null;
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00029AAB File Offset: 0x00027CAB
		private void InitStreamInput(Stream stream, Encoding encoding)
		{
			this.InitStreamInput(null, string.Empty, stream, null, 0, encoding);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00029ABD File Offset: 0x00027CBD
		private void InitStreamInput(string baseUriStr, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(null, baseUriStr, stream, null, 0, encoding);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00029ACB File Offset: 0x00027CCB
		private void InitStreamInput(Uri baseUri, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(baseUri, baseUri.ToString(), stream, null, 0, encoding);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00029ADE File Offset: 0x00027CDE
		private void InitStreamInput(Uri baseUri, string baseUriStr, Stream stream, Encoding encoding)
		{
			this.InitStreamInput(baseUri, baseUriStr, stream, null, 0, encoding);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00029AF0 File Offset: 0x00027CF0
		private void InitStreamInput(Uri baseUri, string baseUriStr, Stream stream, byte[] bytes, int byteCount, Encoding encoding)
		{
			this.ps.stream = stream;
			this.ps.baseUri = baseUri;
			this.ps.baseUriStr = baseUriStr;
			int num;
			if (bytes != null)
			{
				this.ps.bytes = bytes;
				this.ps.bytesUsed = byteCount;
				num = this.ps.bytes.Length;
			}
			else
			{
				if (this.laterInitParam != null && this.laterInitParam.useAsync)
				{
					num = 65536;
				}
				else
				{
					num = XmlReader.CalcBufferSize(stream);
				}
				if (this.ps.bytes == null || this.ps.bytes.Length < num)
				{
					this.ps.bytes = new byte[num];
				}
			}
			if (this.ps.chars == null || this.ps.chars.Length < num + 1)
			{
				this.ps.chars = new char[num + 1];
			}
			this.ps.bytePos = 0;
			while (this.ps.bytesUsed < 4 && this.ps.bytes.Length - this.ps.bytesUsed > 0)
			{
				int num2 = stream.Read(this.ps.bytes, this.ps.bytesUsed, this.ps.bytes.Length - this.ps.bytesUsed);
				if (num2 == 0)
				{
					this.ps.isStreamEof = true;
					break;
				}
				this.ps.bytesUsed = this.ps.bytesUsed + num2;
			}
			if (encoding == null)
			{
				encoding = this.DetectEncoding();
			}
			this.SetupEncoding(encoding);
			byte[] preamble = this.ps.encoding.GetPreamble();
			int num3 = preamble.Length;
			int num4 = 0;
			while (num4 < num3 && num4 < this.ps.bytesUsed && this.ps.bytes[num4] == preamble[num4])
			{
				num4++;
			}
			if (num4 == num3)
			{
				this.ps.bytePos = num3;
			}
			this.documentStartBytePos = this.ps.bytePos;
			this.ps.eolNormalized = !this.normalize;
			this.ps.appendMode = true;
			this.ReadData();
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00029D05 File Offset: 0x00027F05
		private void InitTextReaderInput(string baseUriStr, TextReader input)
		{
			this.InitTextReaderInput(baseUriStr, null, input);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00029D10 File Offset: 0x00027F10
		private void InitTextReaderInput(string baseUriStr, Uri baseUri, TextReader input)
		{
			this.ps.textReader = input;
			this.ps.baseUriStr = baseUriStr;
			this.ps.baseUri = baseUri;
			if (this.ps.chars == null)
			{
				if (this.laterInitParam != null && this.laterInitParam.useAsync)
				{
					this.ps.chars = new char[65537];
				}
				else
				{
					this.ps.chars = new char[4097];
				}
			}
			this.ps.encoding = Encoding.Unicode;
			this.ps.eolNormalized = !this.normalize;
			this.ps.appendMode = true;
			this.ReadData();
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00029DC8 File Offset: 0x00027FC8
		private void InitStringInput(string baseUriStr, Encoding originalEncoding, string str)
		{
			this.ps.baseUriStr = baseUriStr;
			this.ps.baseUri = null;
			int length = str.Length;
			this.ps.chars = new char[length + 1];
			str.CopyTo(0, this.ps.chars, 0, str.Length);
			this.ps.charsUsed = length;
			this.ps.chars[length] = '\0';
			this.ps.encoding = originalEncoding;
			this.ps.eolNormalized = !this.normalize;
			this.ps.isEof = true;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00029E68 File Offset: 0x00028068
		private void InitFragmentReader(XmlNodeType fragmentType, XmlParserContext parserContext, bool allowXmlDeclFragment)
		{
			this.fragmentParserContext = parserContext;
			if (parserContext != null)
			{
				if (parserContext.NamespaceManager != null)
				{
					this.namespaceManager = parserContext.NamespaceManager;
					this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
				}
				else
				{
					this.namespaceManager = new XmlNamespaceManager(this.nameTable);
				}
				this.ps.baseUriStr = parserContext.BaseURI;
				this.ps.baseUri = null;
				this.xmlContext.xmlLang = parserContext.XmlLang;
				this.xmlContext.xmlSpace = parserContext.XmlSpace;
			}
			else
			{
				this.namespaceManager = new XmlNamespaceManager(this.nameTable);
				this.ps.baseUriStr = string.Empty;
				this.ps.baseUri = null;
			}
			this.reportedBaseUri = this.ps.baseUriStr;
			if (fragmentType <= XmlNodeType.Attribute)
			{
				if (fragmentType == XmlNodeType.Element)
				{
					this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
					goto IL_147;
				}
				if (fragmentType == XmlNodeType.Attribute)
				{
					this.ps.appendMode = false;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractive;
					this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.FragmentAttribute;
					goto IL_147;
				}
			}
			else
			{
				if (fragmentType == XmlNodeType.Document)
				{
					goto IL_147;
				}
				if (fragmentType == XmlNodeType.XmlDeclaration)
				{
					if (allowXmlDeclFragment)
					{
						this.ps.appendMode = false;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.SwitchToInteractive;
						this.nextParsingFunction = XmlTextReaderImpl.ParsingFunction.XmlDeclarationFragment;
						goto IL_147;
					}
				}
			}
			this.Throw("XmlNodeType {0} is not supported for partial content parsing.", fragmentType.ToString());
			return;
			IL_147:
			this.fragmentType = fragmentType;
			this.fragment = true;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00029FCC File Offset: 0x000281CC
		private void ProcessDtdFromParserContext(XmlParserContext context)
		{
			switch (this.dtdProcessing)
			{
			case DtdProcessing.Prohibit:
				this.ThrowWithoutLineInfo("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.");
				return;
			case DtdProcessing.Ignore:
				break;
			case DtdProcessing.Parse:
				this.ParseDtdFromParserContext();
				break;
			default:
				return;
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002A008 File Offset: 0x00028208
		private void OpenUrl()
		{
			XmlResolver tempResolver = this.GetTempResolver();
			if (!(this.ps.baseUri != null))
			{
				this.ps.baseUri = tempResolver.ResolveUri(null, this.url);
				this.ps.baseUriStr = this.ps.baseUri.ToString();
			}
			try
			{
				this.OpenUrlDelegate(tempResolver);
			}
			catch
			{
				this.SetErrorState();
				throw;
			}
			if (this.ps.stream == null)
			{
				this.ThrowWithoutLineInfo("Cannot resolve '{0}'.", this.ps.baseUriStr);
			}
			this.InitStreamInput(this.ps.baseUri, this.ps.baseUriStr, this.ps.stream, null);
			this.reportedEncoding = this.ps.encoding;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002A0E4 File Offset: 0x000282E4
		private void OpenUrlDelegate(object xmlResolver)
		{
			this.ps.stream = (Stream)this.GetTempResolver().GetEntity(this.ps.baseUri, null, typeof(Stream));
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002A118 File Offset: 0x00028318
		private Encoding DetectEncoding()
		{
			if (this.ps.bytesUsed < 2)
			{
				return null;
			}
			int num = (int)this.ps.bytes[0] << 8 | (int)this.ps.bytes[1];
			int num2 = (this.ps.bytesUsed >= 4) ? ((int)this.ps.bytes[2] << 8 | (int)this.ps.bytes[3]) : 0;
			if (num <= 15360)
			{
				if (num != 0)
				{
					if (num != 60)
					{
						if (num == 15360)
						{
							if (num2 == 0)
							{
								return Ucs4Encoding.UCS4_Littleendian;
							}
							return Encoding.Unicode;
						}
					}
					else
					{
						if (num2 == 0)
						{
							return Ucs4Encoding.UCS4_3412;
						}
						return Encoding.BigEndianUnicode;
					}
				}
				else if (num2 <= 15360)
				{
					if (num2 == 60)
					{
						return Ucs4Encoding.UCS4_Bigendian;
					}
					if (num2 == 15360)
					{
						return Ucs4Encoding.UCS4_2143;
					}
				}
				else
				{
					if (num2 == 65279)
					{
						return Ucs4Encoding.UCS4_Bigendian;
					}
					if (num2 == 65534)
					{
						return Ucs4Encoding.UCS4_2143;
					}
				}
			}
			else if (num <= 61371)
			{
				if (num != 19567)
				{
					if (num == 61371)
					{
						if ((num2 & 65280) == 48896)
						{
							return new UTF8Encoding(true, true);
						}
					}
				}
				else if (num2 == 42900)
				{
					this.Throw("System does not support '{0}' encoding.", "ebcdic");
				}
			}
			else if (num != 65279)
			{
				if (num == 65534)
				{
					if (num2 == 0)
					{
						return Ucs4Encoding.UCS4_Littleendian;
					}
					return Encoding.Unicode;
				}
			}
			else
			{
				if (num2 == 0)
				{
					return Ucs4Encoding.UCS4_3412;
				}
				return Encoding.BigEndianUnicode;
			}
			return null;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002A294 File Offset: 0x00028494
		private void SetupEncoding(Encoding encoding)
		{
			if (encoding == null)
			{
				this.ps.encoding = Encoding.UTF8;
				this.ps.decoder = new SafeAsciiDecoder();
				return;
			}
			this.ps.encoding = encoding;
			string webName = this.ps.encoding.WebName;
			if (webName == "utf-16")
			{
				this.ps.decoder = new UTF16Decoder(false);
				return;
			}
			if (!(webName == "utf-16BE"))
			{
				this.ps.decoder = encoding.GetDecoder();
				return;
			}
			this.ps.decoder = new UTF16Decoder(true);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002A334 File Offset: 0x00028534
		private void SwitchEncoding(Encoding newEncoding)
		{
			if ((newEncoding.WebName != this.ps.encoding.WebName || this.ps.decoder is SafeAsciiDecoder) && !this.afterResetState)
			{
				this.UnDecodeChars();
				this.ps.appendMode = false;
				this.SetupEncoding(newEncoding);
				this.ReadData();
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002A398 File Offset: 0x00028598
		private Encoding CheckEncoding(string newEncodingName)
		{
			if (this.ps.stream == null)
			{
				return this.ps.encoding;
			}
			if (string.Compare(newEncodingName, "ucs-2", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "utf-16", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "iso-10646-ucs-2", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(newEncodingName, "ucs-4", StringComparison.OrdinalIgnoreCase) == 0)
			{
				if (this.ps.encoding.WebName != "utf-16BE" && this.ps.encoding.WebName != "utf-16" && string.Compare(newEncodingName, "ucs-4", StringComparison.OrdinalIgnoreCase) != 0)
				{
					if (this.afterResetState)
					{
						this.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", newEncodingName);
					}
					else
					{
						this.ThrowWithoutLineInfo("There is no Unicode byte order mark. Cannot switch to Unicode.");
					}
				}
				return this.ps.encoding;
			}
			Encoding encoding = null;
			if (string.Compare(newEncodingName, "utf-8", StringComparison.OrdinalIgnoreCase) == 0)
			{
				encoding = new UTF8Encoding(true, true);
			}
			else
			{
				try
				{
					encoding = Encoding.GetEncoding(newEncodingName);
				}
				catch (NotSupportedException innerException)
				{
					this.Throw("System does not support '{0}' encoding.", newEncodingName, innerException);
				}
				catch (ArgumentException innerException2)
				{
					this.Throw("System does not support '{0}' encoding.", newEncodingName, innerException2);
				}
			}
			if (this.afterResetState && this.ps.encoding.WebName != encoding.WebName)
			{
				this.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", newEncodingName);
			}
			return encoding;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002A4FC File Offset: 0x000286FC
		private void UnDecodeChars()
		{
			if (this.maxCharactersInDocument > 0L)
			{
				this.charactersInDocument -= (long)(this.ps.charsUsed - this.ps.charPos);
			}
			if (this.maxCharactersFromEntities > 0L && this.InEntity)
			{
				this.charactersFromEntities -= (long)(this.ps.charsUsed - this.ps.charPos);
			}
			this.ps.bytePos = this.documentStartBytePos;
			if (this.ps.charPos > 0)
			{
				this.ps.bytePos = this.ps.bytePos + this.ps.encoding.GetByteCount(this.ps.chars, 0, this.ps.charPos);
			}
			this.ps.charsUsed = this.ps.charPos;
			this.ps.isEof = false;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002A5E6 File Offset: 0x000287E6
		private void SwitchEncodingToUTF8()
		{
			this.SwitchEncoding(new UTF8Encoding(true, true));
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002A5F8 File Offset: 0x000287F8
		private int ReadData()
		{
			if (this.ps.isEof)
			{
				return 0;
			}
			int num;
			if (this.ps.appendMode)
			{
				if (this.ps.charsUsed == this.ps.chars.Length - 1)
				{
					for (int i = 0; i < this.attrCount; i++)
					{
						this.nodes[this.index + i + 1].OnBufferInvalidated();
					}
					char[] array = new char[this.ps.chars.Length * 2];
					XmlTextReaderImpl.BlockCopyChars(this.ps.chars, 0, array, 0, this.ps.chars.Length);
					this.ps.chars = array;
				}
				if (this.ps.stream != null && this.ps.bytesUsed - this.ps.bytePos < 6 && this.ps.bytes.Length - this.ps.bytesUsed < 6)
				{
					byte[] array2 = new byte[this.ps.bytes.Length * 2];
					XmlTextReaderImpl.BlockCopy(this.ps.bytes, 0, array2, 0, this.ps.bytesUsed);
					this.ps.bytes = array2;
				}
				num = this.ps.chars.Length - this.ps.charsUsed - 1;
				if (num > 80)
				{
					num = 80;
				}
			}
			else
			{
				int num2 = this.ps.chars.Length;
				if (num2 - this.ps.charsUsed <= num2 / 2)
				{
					for (int j = 0; j < this.attrCount; j++)
					{
						this.nodes[this.index + j + 1].OnBufferInvalidated();
					}
					int num3 = this.ps.charsUsed - this.ps.charPos;
					if (num3 < num2 - 1)
					{
						this.ps.lineStartPos = this.ps.lineStartPos - this.ps.charPos;
						if (num3 > 0)
						{
							XmlTextReaderImpl.BlockCopyChars(this.ps.chars, this.ps.charPos, this.ps.chars, 0, num3);
						}
						this.ps.charPos = 0;
						this.ps.charsUsed = num3;
					}
					else
					{
						char[] array3 = new char[this.ps.chars.Length * 2];
						XmlTextReaderImpl.BlockCopyChars(this.ps.chars, 0, array3, 0, this.ps.chars.Length);
						this.ps.chars = array3;
					}
				}
				if (this.ps.stream != null)
				{
					int num4 = this.ps.bytesUsed - this.ps.bytePos;
					if (num4 <= 128)
					{
						if (num4 == 0)
						{
							this.ps.bytesUsed = 0;
						}
						else
						{
							XmlTextReaderImpl.BlockCopy(this.ps.bytes, this.ps.bytePos, this.ps.bytes, 0, num4);
							this.ps.bytesUsed = num4;
						}
						this.ps.bytePos = 0;
					}
				}
				num = this.ps.chars.Length - this.ps.charsUsed - 1;
			}
			if (this.ps.stream != null)
			{
				if (!this.ps.isStreamEof && this.ps.bytePos == this.ps.bytesUsed && this.ps.bytes.Length - this.ps.bytesUsed > 0)
				{
					int num5 = this.ps.stream.Read(this.ps.bytes, this.ps.bytesUsed, this.ps.bytes.Length - this.ps.bytesUsed);
					if (num5 == 0)
					{
						this.ps.isStreamEof = true;
					}
					this.ps.bytesUsed = this.ps.bytesUsed + num5;
				}
				int bytePos = this.ps.bytePos;
				num = this.GetChars(num);
				if (num == 0 && this.ps.bytePos != bytePos)
				{
					return this.ReadData();
				}
			}
			else if (this.ps.textReader != null)
			{
				num = this.ps.textReader.Read(this.ps.chars, this.ps.charsUsed, this.ps.chars.Length - this.ps.charsUsed - 1);
				this.ps.charsUsed = this.ps.charsUsed + num;
			}
			else
			{
				num = 0;
			}
			this.RegisterConsumedCharacters((long)num, this.InEntity);
			if (num == 0)
			{
				this.ps.isEof = true;
			}
			this.ps.chars[this.ps.charsUsed] = '\0';
			return num;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0002AA94 File Offset: 0x00028C94
		private int GetChars(int maxCharsCount)
		{
			int num = this.ps.bytesUsed - this.ps.bytePos;
			if (num == 0)
			{
				return 0;
			}
			int num2;
			try
			{
				bool flag;
				this.ps.decoder.Convert(this.ps.bytes, this.ps.bytePos, num, this.ps.chars, this.ps.charsUsed, maxCharsCount, false, out num, out num2, out flag);
			}
			catch (ArgumentException)
			{
				this.InvalidCharRecovery(ref num, out num2);
			}
			this.ps.bytePos = this.ps.bytePos + num;
			this.ps.charsUsed = this.ps.charsUsed + num2;
			return num2;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002AB44 File Offset: 0x00028D44
		private void InvalidCharRecovery(ref int bytesCount, out int charsCount)
		{
			int num = 0;
			int i = 0;
			try
			{
				while (i < bytesCount)
				{
					int num2;
					int num3;
					bool flag;
					this.ps.decoder.Convert(this.ps.bytes, this.ps.bytePos + i, 1, this.ps.chars, this.ps.charsUsed + num, 1, false, out num2, out num3, out flag);
					num += num3;
					i += num2;
				}
			}
			catch (ArgumentException)
			{
			}
			if (num == 0)
			{
				this.Throw(this.ps.charsUsed, "Invalid character in the given encoding.");
			}
			charsCount = num;
			bytesCount = i;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002ABE4 File Offset: 0x00028DE4
		internal void Close(bool closeInput)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ReaderClosed)
			{
				return;
			}
			while (this.InEntity)
			{
				this.PopParsingState();
			}
			this.ps.Close(closeInput);
			this.curNode = XmlTextReaderImpl.NodeData.None;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ReaderClosed;
			this.reportedEncoding = null;
			this.reportedBaseUri = string.Empty;
			this.readState = ReadState.Closed;
			this.fullAttrCleanup = false;
			this.ResetAttributes();
			this.laterInitParam = null;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002AC56 File Offset: 0x00028E56
		private void ShiftBuffer(int sourcePos, int destPos, int count)
		{
			XmlTextReaderImpl.BlockCopyChars(this.ps.chars, sourcePos, this.ps.chars, destPos, count);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002AC78 File Offset: 0x00028E78
		private bool ParseXmlDeclaration(bool isTextDecl)
		{
			while (this.ps.charsUsed - this.ps.charPos < 6)
			{
				if (this.ReadData() == 0)
				{
					IL_7E0:
					if (!isTextDecl)
					{
						this.parsingFunction = this.nextParsingFunction;
					}
					if (this.afterResetState)
					{
						string webName = this.ps.encoding.WebName;
						if (webName != "utf-8" && webName != "utf-16" && webName != "utf-16BE" && !(this.ps.encoding is Ucs4Encoding))
						{
							this.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", (this.ps.encoding.GetByteCount("A") == 1) ? "UTF-8" : "UTF-16");
						}
					}
					if (this.ps.decoder is SafeAsciiDecoder)
					{
						this.SwitchEncodingToUTF8();
					}
					this.ps.appendMode = false;
					return false;
				}
			}
			if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 5, "<?xml") && !this.xmlCharType.IsNameSingleChar(this.ps.chars[this.ps.charPos + 5]))
			{
				if (!isTextDecl)
				{
					this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos + 2);
					this.curNode.SetNamedNode(XmlNodeType.XmlDeclaration, this.Xml);
				}
				this.ps.charPos = this.ps.charPos + 5;
				StringBuilder stringBuilder = isTextDecl ? new StringBuilder() : this.stringBuilder;
				int num = 0;
				Encoding encoding = null;
				for (;;)
				{
					int length = stringBuilder.Length;
					int num2 = this.EatWhitespaces((num == 0) ? null : stringBuilder);
					if (this.ps.chars[this.ps.charPos] == '?')
					{
						stringBuilder.Length = length;
						if (this.ps.chars[this.ps.charPos + 1] == '>')
						{
							break;
						}
						if (this.ps.charPos + 1 == this.ps.charsUsed)
						{
							goto IL_7B8;
						}
						this.ThrowUnexpectedToken("'>'");
					}
					if (num2 == 0 && num != 0)
					{
						this.ThrowUnexpectedToken("?>");
					}
					int num3 = this.ParseName();
					XmlTextReaderImpl.NodeData nodeData = null;
					char c = this.ps.chars[this.ps.charPos];
					if (c != 'e')
					{
						if (c != 's')
						{
							if (c != 'v' || !XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "version") || num != 0)
							{
								goto IL_3B5;
							}
							if (!isTextDecl)
							{
								nodeData = this.AddAttributeNoChecks("version", 1);
							}
						}
						else
						{
							if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "standalone") || (num != 1 && num != 2) || isTextDecl)
							{
								goto IL_3B5;
							}
							if (!isTextDecl)
							{
								nodeData = this.AddAttributeNoChecks("standalone", 1);
							}
							num = 2;
						}
					}
					else
					{
						if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos, "encoding") || (num != 1 && (!isTextDecl || num != 0)))
						{
							goto IL_3B5;
						}
						if (!isTextDecl)
						{
							nodeData = this.AddAttributeNoChecks("encoding", 1);
						}
						num = 1;
					}
					IL_3CA:
					if (!isTextDecl)
					{
						nodeData.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
					}
					stringBuilder.Append(this.ps.chars, this.ps.charPos, num3 - this.ps.charPos);
					this.ps.charPos = num3;
					if (this.ps.chars[this.ps.charPos] != '=')
					{
						this.EatWhitespaces(stringBuilder);
						if (this.ps.chars[this.ps.charPos] != '=')
						{
							this.ThrowUnexpectedToken("=");
						}
					}
					stringBuilder.Append('=');
					this.ps.charPos = this.ps.charPos + 1;
					char c2 = this.ps.chars[this.ps.charPos];
					if (c2 != '"' && c2 != '\'')
					{
						this.EatWhitespaces(stringBuilder);
						c2 = this.ps.chars[this.ps.charPos];
						if (c2 != '"' && c2 != '\'')
						{
							this.ThrowUnexpectedToken("\"", "'");
						}
					}
					stringBuilder.Append(c2);
					this.ps.charPos = this.ps.charPos + 1;
					if (!isTextDecl)
					{
						nodeData.quoteChar = c2;
						nodeData.SetLineInfo2(this.ps.LineNo, this.ps.LinePos);
					}
					int num4 = this.ps.charPos;
					char[] chars;
					for (;;)
					{
						chars = this.ps.chars;
						while ((this.xmlCharType.charProperties[(int)chars[num4]] & 128) != 0)
						{
							num4++;
						}
						if (this.ps.chars[num4] == c2)
						{
							break;
						}
						if (num4 != this.ps.charsUsed)
						{
							goto IL_7A3;
						}
						if (this.ReadData() == 0)
						{
							goto Block_57;
						}
					}
					switch (num)
					{
					case 0:
						if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "1.0"))
						{
							if (!isTextDecl)
							{
								nodeData.SetValue(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
							}
							num = 1;
						}
						else
						{
							string arg = new string(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
							this.Throw("Version number '{0}' is invalid.", arg);
						}
						break;
					case 1:
					{
						string text = new string(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
						encoding = this.CheckEncoding(text);
						if (!isTextDecl)
						{
							nodeData.SetValue(text);
						}
						num = 2;
						break;
					}
					case 2:
						if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "yes"))
						{
							this.standalone = true;
						}
						else if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos, "no"))
						{
							this.standalone = false;
						}
						else
						{
							this.Throw("Syntax for an XML declaration is invalid.", this.ps.LineNo, this.ps.LinePos - 1);
						}
						if (!isTextDecl)
						{
							nodeData.SetValue(this.ps.chars, this.ps.charPos, num4 - this.ps.charPos);
						}
						num = 3;
						break;
					}
					stringBuilder.Append(chars, this.ps.charPos, num4 - this.ps.charPos);
					stringBuilder.Append(c2);
					this.ps.charPos = num4 + 1;
					continue;
					Block_57:
					this.Throw("There is an unclosed literal string.");
					goto IL_7B8;
					IL_7A3:
					this.Throw(isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
					goto IL_7B8;
					IL_3B5:
					this.Throw(isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
					goto IL_3CA;
					IL_7B8:
					if (this.ps.isEof || this.ReadData() == 0)
					{
						this.Throw("Unexpected end of file has occurred.");
					}
				}
				if (num == 0)
				{
					this.Throw(isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
				}
				this.ps.charPos = this.ps.charPos + 2;
				if (!isTextDecl)
				{
					this.curNode.SetValue(stringBuilder.ToString());
					stringBuilder.Length = 0;
					this.nextParsingFunction = this.parsingFunction;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel;
				}
				if (encoding == null)
				{
					if (isTextDecl)
					{
						this.Throw("Invalid text declaration.");
					}
					if (this.afterResetState)
					{
						string webName2 = this.ps.encoding.WebName;
						if (webName2 != "utf-8" && webName2 != "utf-16" && webName2 != "utf-16BE" && !(this.ps.encoding is Ucs4Encoding))
						{
							this.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", (this.ps.encoding.GetByteCount("A") == 1) ? "UTF-8" : "UTF-16");
						}
					}
					if (this.ps.decoder is SafeAsciiDecoder)
					{
						this.SwitchEncodingToUTF8();
					}
				}
				else
				{
					this.SwitchEncoding(encoding);
				}
				this.ps.appendMode = false;
				return true;
			}
			goto IL_7E0;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002B520 File Offset: 0x00029720
		private bool ParseDocumentContent()
		{
			bool flag = false;
			int num;
			for (;;)
			{
				bool flag2 = false;
				num = this.ps.charPos;
				char[] chars = this.ps.chars;
				if (chars[num] == '<')
				{
					flag2 = true;
					if (this.ps.charsUsed - num >= 4)
					{
						num++;
						char c = chars[num];
						if (c != '!')
						{
							if (c != '/')
							{
								if (c != '?')
								{
									goto IL_1D3;
								}
								this.ps.charPos = num + 1;
								if (this.ParsePI())
								{
									break;
								}
								continue;
							}
							else
							{
								this.Throw(num + 1, "Unexpected end tag.");
							}
						}
						else
						{
							num++;
							if (this.ps.charsUsed - num >= 2)
							{
								if (chars[num] == '-')
								{
									if (chars[num + 1] == '-')
									{
										this.ps.charPos = num + 2;
										if (this.ParseComment())
										{
											return true;
										}
										continue;
									}
									else
									{
										this.ThrowUnexpectedToken(num + 1, "-");
									}
								}
								else if (chars[num] == '[')
								{
									if (this.fragmentType != XmlNodeType.Document)
									{
										num++;
										if (this.ps.charsUsed - num >= 6)
										{
											if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
											{
												goto Block_14;
											}
											this.ThrowUnexpectedToken(num, "CDATA[");
										}
									}
									else
									{
										this.Throw(this.ps.charPos, "Data at the root level is invalid.");
									}
								}
								else if (this.fragmentType == XmlNodeType.Document || this.fragmentType == XmlNodeType.None)
								{
									this.fragmentType = XmlNodeType.Document;
									this.ps.charPos = num;
									if (this.ParseDoctypeDecl())
									{
										return true;
									}
									continue;
								}
								else if (this.ParseUnexpectedToken(num) == "DOCTYPE")
								{
									this.Throw("Unexpected DTD declaration.");
								}
								else
								{
									this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
								}
							}
						}
					}
				}
				else if (chars[num] == '&')
				{
					if (this.fragmentType == XmlNodeType.Document)
					{
						this.Throw(num, "Data at the root level is invalid.");
					}
					else
					{
						if (this.fragmentType == XmlNodeType.None)
						{
							this.fragmentType = XmlNodeType.Element;
						}
						int num2;
						XmlTextReaderImpl.EntityType entityType = this.HandleEntityReference(false, XmlTextReaderImpl.EntityExpandType.OnlyGeneral, out num2);
						if (entityType > XmlTextReaderImpl.EntityType.CharacterNamed)
						{
							if (entityType == XmlTextReaderImpl.EntityType.Unexpanded)
							{
								goto Block_26;
							}
							chars = this.ps.chars;
							num = this.ps.charPos;
							continue;
						}
						else
						{
							if (this.ParseText())
							{
								return true;
							}
							continue;
						}
					}
				}
				else if (num != this.ps.charsUsed && ((!this.v1Compat && !flag) || chars[num] != '\0'))
				{
					if (this.fragmentType == XmlNodeType.Document)
					{
						if (this.ParseRootLevelWhitespace())
						{
							return true;
						}
						continue;
					}
					else
					{
						if (this.ParseText())
						{
							goto Block_33;
						}
						continue;
					}
				}
				if (this.ReadData() != 0)
				{
					num = this.ps.charPos;
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				else
				{
					if (flag2)
					{
						this.Throw("Data at the root level is invalid.");
					}
					if (!this.InEntity)
					{
						goto IL_34B;
					}
					if (this.HandleEntityEnd(true))
					{
						goto Block_39;
					}
				}
			}
			return true;
			Block_14:
			this.ps.charPos = num + 6;
			this.ParseCData();
			if (this.fragmentType == XmlNodeType.None)
			{
				this.fragmentType = XmlNodeType.Element;
			}
			return true;
			IL_1D3:
			if (this.rootElementParsed)
			{
				if (this.fragmentType == XmlNodeType.Document)
				{
					this.Throw(num, "There are multiple root elements.");
				}
				if (this.fragmentType == XmlNodeType.None)
				{
					this.fragmentType = XmlNodeType.Element;
				}
			}
			this.ps.charPos = num;
			this.rootElementParsed = true;
			this.ParseElement();
			return true;
			Block_26:
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
			{
				this.parsingFunction = this.nextParsingFunction;
			}
			this.ParseEntityReference();
			return true;
			Block_33:
			if (this.fragmentType == XmlNodeType.None && this.curNode.type == XmlNodeType.Text)
			{
				this.fragmentType = XmlNodeType.Element;
			}
			return true;
			Block_39:
			this.SetupEndEntityNodeInContent();
			return true;
			IL_34B:
			if (!this.rootElementParsed && this.fragmentType == XmlNodeType.Document)
			{
				this.ThrowWithoutLineInfo("Root element is missing.");
			}
			if (this.fragmentType == XmlNodeType.None)
			{
				this.fragmentType = (this.rootElementParsed ? XmlNodeType.Document : XmlNodeType.Element);
			}
			this.OnEof();
			return false;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002B8D4 File Offset: 0x00029AD4
		private bool ParseElementContent()
		{
			int num;
			for (;;)
			{
				num = this.ps.charPos;
				char[] chars = this.ps.chars;
				char c = chars[num];
				if (c != '&')
				{
					if (c == '<')
					{
						char c2 = chars[num + 1];
						if (c2 != '!')
						{
							if (c2 == '/')
							{
								goto IL_13B;
							}
							if (c2 == '?')
							{
								this.ps.charPos = num + 2;
								if (this.ParsePI())
								{
									break;
								}
								continue;
							}
							else if (num + 1 != this.ps.charsUsed)
							{
								goto Block_14;
							}
						}
						else
						{
							num += 2;
							if (this.ps.charsUsed - num >= 2)
							{
								if (chars[num] == '-')
								{
									if (chars[num + 1] == '-')
									{
										this.ps.charPos = num + 2;
										if (this.ParseComment())
										{
											return true;
										}
										continue;
									}
									else
									{
										this.ThrowUnexpectedToken(num + 1, "-");
									}
								}
								else if (chars[num] == '[')
								{
									num++;
									if (this.ps.charsUsed - num >= 6)
									{
										if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
										{
											goto Block_12;
										}
										this.ThrowUnexpectedToken(num, "CDATA[");
									}
								}
								else if (this.ParseUnexpectedToken(num) == "DOCTYPE")
								{
									this.Throw("Unexpected DTD declaration.");
								}
								else
								{
									this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
								}
							}
						}
					}
					else if (num != this.ps.charsUsed)
					{
						if (this.ParseText())
						{
							return true;
						}
						continue;
					}
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos != 0)
						{
							this.ThrowUnclosedElements();
						}
						if (!this.InEntity)
						{
							if (this.index == 0 && this.fragmentType != XmlNodeType.Document)
							{
								goto Block_22;
							}
							this.ThrowUnclosedElements();
						}
						if (this.HandleEntityEnd(true))
						{
							goto Block_23;
						}
					}
				}
				else if (this.ParseText())
				{
					return true;
				}
			}
			return true;
			Block_12:
			this.ps.charPos = num + 6;
			this.ParseCData();
			return true;
			IL_13B:
			this.ps.charPos = num + 2;
			this.ParseEndElement();
			return true;
			Block_14:
			this.ps.charPos = num + 1;
			this.ParseElement();
			return true;
			Block_22:
			this.OnEof();
			return false;
			Block_23:
			this.SetupEndEntityNodeInContent();
			return true;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002BAE8 File Offset: 0x00029CE8
		private void ThrowUnclosedElements()
		{
			if (this.index == 0 && this.curNode.type != XmlNodeType.Element)
			{
				this.Throw(this.ps.charsUsed, "Unexpected end of file has occurred.");
				return;
			}
			int i = (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead) ? this.index : (this.index - 1);
			this.stringBuilder.Length = 0;
			while (i >= 0)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				if (nodeData.type == XmlNodeType.Element)
				{
					this.stringBuilder.Append(nodeData.GetNameWPrefix(this.nameTable));
					if (i > 0)
					{
						this.stringBuilder.Append(", ");
					}
					else
					{
						this.stringBuilder.Append(".");
					}
				}
				i--;
			}
			this.Throw(this.ps.charsUsed, "Unexpected end of file has occurred. The following elements are not closed: {0}", this.stringBuilder.ToString());
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002BBC8 File Offset: 0x00029DC8
		private void ParseElement()
		{
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = -1;
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			while ((this.xmlCharType.charProperties[(int)chars[num]] & 4) != 0)
			{
				num++;
				for (;;)
				{
					if ((this.xmlCharType.charProperties[(int)chars[num]] & 8) != 0)
					{
						num++;
					}
					else
					{
						if (chars[num] != ':')
						{
							goto IL_A2;
						}
						if (num2 == -1)
						{
							break;
						}
						if (this.supportNamespaces)
						{
							goto Block_5;
						}
						num++;
					}
				}
				num2 = num;
				num++;
				continue;
				Block_5:
				this.Throw(num, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
				break;
				IL_A2:
				if (num + 1 >= this.ps.charsUsed)
				{
					break;
				}
				IL_C7:
				this.namespaceManager.PushScope();
				if (num2 == -1 || !this.supportNamespaces)
				{
					this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, this.ps.charPos, num - this.ps.charPos));
				}
				else
				{
					int charPos = this.ps.charPos;
					int num3 = num2 - charPos;
					if (num3 == this.lastPrefix.Length && XmlConvert.StrEqual(chars, charPos, num3, this.lastPrefix))
					{
						this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, num2 + 1, num - num2 - 1), this.lastPrefix, null);
					}
					else
					{
						this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, num2 + 1, num - num2 - 1), this.nameTable.Add(chars, this.ps.charPos, num3), null);
						this.lastPrefix = this.curNode.prefix;
					}
				}
				char c = chars[num];
				if ((this.xmlCharType.charProperties[(int)c] & 1) > 0)
				{
					this.ps.charPos = num;
					this.ParseAttributes();
					return;
				}
				if (c == '>')
				{
					this.ps.charPos = num + 1;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
				}
				else if (c == '/')
				{
					if (num + 1 == this.ps.charsUsed)
					{
						this.ps.charPos = num;
						if (this.ReadData() == 0)
						{
							this.Throw(num, "Unexpected end of file while parsing {0} has occurred.", ">");
						}
						num = this.ps.charPos;
						chars = this.ps.chars;
					}
					if (chars[num + 1] == '>')
					{
						this.curNode.IsEmptyElement = true;
						this.nextParsingFunction = this.parsingFunction;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
						this.ps.charPos = num + 2;
					}
					else
					{
						this.ThrowUnexpectedToken(num, ">");
					}
				}
				else
				{
					this.Throw(num, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(chars, this.ps.charsUsed, num));
				}
				if (this.addDefaultAttributesAndNormalize)
				{
					this.AddDefaultAttributesAndNormalize();
				}
				this.ElementNamespaceLookup();
				return;
			}
			num = this.ParseQName(out num2);
			chars = this.ps.chars;
			goto IL_C7;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002BEA8 File Offset: 0x0002A0A8
		private void AddDefaultAttributesAndNormalize()
		{
			IDtdAttributeListInfo dtdAttributeListInfo = this.dtdInfo.LookupAttributeList(this.curNode.localName, this.curNode.prefix);
			if (dtdAttributeListInfo == null)
			{
				return;
			}
			if (this.normalize && dtdAttributeListInfo.HasNonCDataAttributes)
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
					IDtdAttributeInfo dtdAttributeInfo = dtdAttributeListInfo.LookupAttribute(nodeData.prefix, nodeData.localName);
					if (dtdAttributeInfo != null && dtdAttributeInfo.IsNonCDataType)
					{
						if (this.DtdValidation && this.standalone && dtdAttributeInfo.IsDeclaredInExternal)
						{
							string stringValue = nodeData.StringValue;
							nodeData.TrimSpacesInValue();
							if (stringValue != nodeData.StringValue)
							{
								this.SendValidationEvent(XmlSeverityType.Error, "StandAlone is 'yes' and the value of the attribute '{0}' contains a definition in an external document that changes on normalization.", nodeData.GetNameWPrefix(this.nameTable), nodeData.LineNo, nodeData.LinePos);
							}
						}
						else
						{
							nodeData.TrimSpacesInValue();
						}
					}
				}
			}
			IEnumerable<IDtdDefaultAttributeInfo> enumerable = dtdAttributeListInfo.LookupDefaultAttributes();
			if (enumerable != null)
			{
				int num = this.attrCount;
				XmlTextReaderImpl.NodeData[] array = null;
				if (this.attrCount >= 250)
				{
					array = new XmlTextReaderImpl.NodeData[this.attrCount];
					Array.Copy(this.nodes, this.index + 1, array, 0, this.attrCount);
					object[] array2 = array;
					Array.Sort<object>(array2, XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.Instance);
				}
				foreach (IDtdDefaultAttributeInfo dtdDefaultAttributeInfo in enumerable)
				{
					if (this.AddDefaultAttributeDtd(dtdDefaultAttributeInfo, true, array) && this.DtdValidation && this.standalone && dtdDefaultAttributeInfo.IsDeclaredInExternal)
					{
						string prefix = dtdDefaultAttributeInfo.Prefix;
						string arg = (prefix.Length == 0) ? dtdDefaultAttributeInfo.LocalName : (prefix + ":" + dtdDefaultAttributeInfo.LocalName);
						this.SendValidationEvent(XmlSeverityType.Error, "Markup for unspecified default attribute '{0}' is external and standalone='yes'.", arg, this.curNode.LineNo, this.curNode.LinePos);
					}
				}
				if (num == 0 && this.attrNeedNamespaceLookup)
				{
					this.AttributeNamespaceLookup();
					this.attrNeedNamespaceLookup = false;
				}
			}
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002C0D4 File Offset: 0x0002A2D4
		private void ParseEndElement()
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[this.index - 1];
			int length = nodeData.prefix.Length;
			int length2 = nodeData.localName.Length;
			while (this.ps.charsUsed - this.ps.charPos < length + length2 + 1 && this.ReadData() != 0)
			{
			}
			char[] chars = this.ps.chars;
			int num;
			if (nodeData.prefix.Length == 0)
			{
				if (!XmlConvert.StrEqual(chars, this.ps.charPos, length2, nodeData.localName))
				{
					this.ThrowTagMismatch(nodeData);
				}
				num = length2;
			}
			else
			{
				int num2 = this.ps.charPos + length;
				if (!XmlConvert.StrEqual(chars, this.ps.charPos, length, nodeData.prefix) || chars[num2] != ':' || !XmlConvert.StrEqual(chars, num2 + 1, length2, nodeData.localName))
				{
					this.ThrowTagMismatch(nodeData);
				}
				num = length2 + length + 1;
			}
			LineInfo lineInfo = new LineInfo(this.ps.lineNo, this.ps.LinePos);
			int num3;
			for (;;)
			{
				num3 = this.ps.charPos + num;
				chars = this.ps.chars;
				if (num3 != this.ps.charsUsed)
				{
					if ((this.xmlCharType.charProperties[(int)chars[num3]] & 8) != 0 || chars[num3] == ':')
					{
						this.ThrowTagMismatch(nodeData);
					}
					if (chars[num3] != '>')
					{
						char c;
						while (this.xmlCharType.IsWhiteSpace(c = chars[num3]))
						{
							num3++;
							if (c != '\n')
							{
								if (c == '\r')
								{
									if (chars[num3] == '\n')
									{
										num3++;
									}
									else if (num3 == this.ps.charsUsed && !this.ps.isEof)
									{
										continue;
									}
									this.OnNewLine(num3);
								}
							}
							else
							{
								this.OnNewLine(num3);
							}
						}
					}
					if (chars[num3] == '>')
					{
						break;
					}
					if (num3 != this.ps.charsUsed)
					{
						this.ThrowUnexpectedToken(num3, ">");
					}
				}
				if (this.ReadData() == 0)
				{
					this.ThrowUnclosedElements();
				}
			}
			this.index--;
			this.curNode = this.nodes[this.index];
			nodeData.lineInfo = lineInfo;
			nodeData.type = XmlNodeType.EndElement;
			this.ps.charPos = num3 + 1;
			this.nextParsingFunction = ((this.index > 0) ? this.parsingFunction : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopElementContext;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002C344 File Offset: 0x0002A544
		private void ThrowTagMismatch(XmlTextReaderImpl.NodeData startTag)
		{
			if (startTag.type == XmlNodeType.Element)
			{
				int num2;
				int num = this.ParseQName(out num2);
				this.Throw("The '{0}' start tag on line {1} position {2} does not match the end tag of '{3}'.", new string[]
				{
					startTag.GetNameWPrefix(this.nameTable),
					startTag.lineInfo.lineNo.ToString(CultureInfo.InvariantCulture),
					startTag.lineInfo.linePos.ToString(CultureInfo.InvariantCulture),
					new string(this.ps.chars, this.ps.charPos, num - this.ps.charPos)
				});
				return;
			}
			this.Throw("Unexpected end tag.");
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002C3F0 File Offset: 0x0002A5F0
		private void ParseAttributes()
		{
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			for (;;)
			{
				IL_1A:
				int num2 = 0;
				char c;
				while ((this.xmlCharType.charProperties[(int)(c = chars[num])] & 1) != 0)
				{
					if (c == '\n')
					{
						this.OnNewLine(num + 1);
						num2++;
					}
					else if (c == '\r')
					{
						if (chars[num + 1] == '\n')
						{
							this.OnNewLine(num + 2);
							num2++;
							num++;
						}
						else if (num + 1 != this.ps.charsUsed)
						{
							this.OnNewLine(num + 1);
							num2++;
						}
						else
						{
							this.ps.charPos = num;
							IL_42C:
							this.ps.lineNo = this.ps.lineNo - num2;
							if (this.ReadData() != 0)
							{
								num = this.ps.charPos;
								chars = this.ps.chars;
								goto IL_1A;
							}
							this.ThrowUnclosedElements();
							goto IL_1A;
						}
					}
					num++;
				}
				int num3 = 0;
				char c2;
				if ((this.xmlCharType.charProperties[(int)(c2 = chars[num])] & 4) != 0)
				{
					num3 = 1;
				}
				if (num3 == 0)
				{
					if (c2 == '>')
					{
						break;
					}
					if (c2 == '/')
					{
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_42C;
						}
						if (chars[num + 1] == '>')
						{
							goto Block_11;
						}
						this.ThrowUnexpectedToken(num + 1, ">");
					}
					else
					{
						if (num == this.ps.charsUsed)
						{
							goto IL_42C;
						}
						if (c2 != ':' || this.supportNamespaces)
						{
							this.Throw(num, "Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(chars, this.ps.charsUsed, num));
						}
					}
				}
				if (num == this.ps.charPos)
				{
					this.ThrowExpectingWhitespace(num);
				}
				this.ps.charPos = num;
				int linePos = this.ps.LinePos;
				int num4 = -1;
				num += num3;
				for (;;)
				{
					char c3;
					if ((this.xmlCharType.charProperties[(int)(c3 = chars[num])] & 8) != 0)
					{
						num++;
					}
					else
					{
						if (c3 != ':')
						{
							goto IL_23E;
						}
						if (num4 != -1)
						{
							if (this.supportNamespaces)
							{
								goto Block_18;
							}
							num++;
						}
						else
						{
							num4 = num;
							num++;
							if ((this.xmlCharType.charProperties[(int)chars[num]] & 4) == 0)
							{
								goto IL_227;
							}
							num++;
						}
					}
				}
				IL_263:
				XmlTextReaderImpl.NodeData nodeData = this.AddAttribute(num, num4);
				nodeData.SetLineInfo(this.ps.LineNo, linePos);
				if (chars[num] != '=')
				{
					this.ps.charPos = num;
					this.EatWhitespaces(null);
					num = this.ps.charPos;
					if (chars[num] != '=')
					{
						this.ThrowUnexpectedToken("=");
					}
				}
				num++;
				char c4 = chars[num];
				if (c4 != '"' && c4 != '\'')
				{
					this.ps.charPos = num;
					this.EatWhitespaces(null);
					num = this.ps.charPos;
					c4 = chars[num];
					if (c4 != '"' && c4 != '\'')
					{
						this.ThrowUnexpectedToken("\"", "'");
					}
				}
				num++;
				this.ps.charPos = num;
				nodeData.quoteChar = c4;
				nodeData.SetLineInfo2(this.ps.LineNo, this.ps.LinePos);
				char c5;
				while ((this.xmlCharType.charProperties[(int)(c5 = chars[num])] & 128) != 0)
				{
					num++;
				}
				if (c5 == c4)
				{
					nodeData.SetValue(chars, this.ps.charPos, num - this.ps.charPos);
					num++;
					this.ps.charPos = num;
				}
				else
				{
					this.ParseAttributeValueSlow(num, c4, nodeData);
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				if (nodeData.prefix.Length == 0)
				{
					if (Ref.Equal(nodeData.localName, this.XmlNs))
					{
						this.OnDefaultNamespaceDecl(nodeData);
						continue;
					}
					continue;
				}
				else
				{
					if (Ref.Equal(nodeData.prefix, this.XmlNs))
					{
						this.OnNamespaceDecl(nodeData);
						continue;
					}
					if (Ref.Equal(nodeData.prefix, this.Xml))
					{
						this.OnXmlReservedAttribute(nodeData);
						continue;
					}
					continue;
				}
				Block_18:
				this.Throw(num, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
				goto IL_263;
				IL_227:
				num = this.ParseQName(out num4);
				chars = this.ps.chars;
				goto IL_263;
				IL_23E:
				if (num + 1 >= this.ps.charsUsed)
				{
					num = this.ParseQName(out num4);
					chars = this.ps.chars;
					goto IL_263;
				}
				goto IL_263;
			}
			this.ps.charPos = num + 1;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
			goto IL_46C;
			Block_11:
			this.ps.charPos = num + 2;
			this.curNode.IsEmptyElement = true;
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
			IL_46C:
			if (this.addDefaultAttributesAndNormalize)
			{
				this.AddDefaultAttributesAndNormalize();
			}
			this.ElementNamespaceLookup();
			if (this.attrNeedNamespaceLookup)
			{
				this.AttributeNamespaceLookup();
				this.attrNeedNamespaceLookup = false;
			}
			if (this.attrDuplWalkCount >= 250)
			{
				this.AttributeDuplCheck();
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002C8A8 File Offset: 0x0002AAA8
		private void ElementNamespaceLookup()
		{
			if (this.curNode.prefix.Length == 0)
			{
				this.curNode.ns = this.xmlContext.defaultNamespace;
				return;
			}
			this.curNode.ns = this.LookupNamespace(this.curNode);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002C8F8 File Offset: 0x0002AAF8
		private void AttributeNamespaceLookup()
		{
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				if (nodeData.type == XmlNodeType.Attribute && nodeData.prefix.Length > 0)
				{
					nodeData.ns = this.LookupNamespace(nodeData);
				}
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002C954 File Offset: 0x0002AB54
		private void AttributeDuplCheck()
		{
			if (this.attrCount < 250)
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
					for (int j = i + 1; j < this.index + 1 + this.attrCount; j++)
					{
						if (Ref.Equal(nodeData.localName, this.nodes[j].localName) && Ref.Equal(nodeData.ns, this.nodes[j].ns))
						{
							this.Throw("'{0}' is a duplicate attribute name.", this.nodes[j].GetNameWPrefix(this.nameTable), this.nodes[j].LineNo, this.nodes[j].LinePos);
						}
					}
				}
				return;
			}
			if (this.attrDuplSortingArray == null || this.attrDuplSortingArray.Length < this.attrCount)
			{
				this.attrDuplSortingArray = new XmlTextReaderImpl.NodeData[this.attrCount];
			}
			Array.Copy(this.nodes, this.index + 1, this.attrDuplSortingArray, 0, this.attrCount);
			Array.Sort<XmlTextReaderImpl.NodeData>(this.attrDuplSortingArray, 0, this.attrCount);
			XmlTextReaderImpl.NodeData nodeData2 = this.attrDuplSortingArray[0];
			for (int k = 1; k < this.attrCount; k++)
			{
				XmlTextReaderImpl.NodeData nodeData3 = this.attrDuplSortingArray[k];
				if (Ref.Equal(nodeData2.localName, nodeData3.localName) && Ref.Equal(nodeData2.ns, nodeData3.ns))
				{
					this.Throw("'{0}' is a duplicate attribute name.", nodeData3.GetNameWPrefix(this.nameTable), nodeData3.LineNo, nodeData3.LinePos);
				}
				nodeData2 = nodeData3;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002CB04 File Offset: 0x0002AD04
		private void OnDefaultNamespaceDecl(XmlTextReaderImpl.NodeData attr)
		{
			if (!this.supportNamespaces)
			{
				return;
			}
			string text = this.nameTable.Add(attr.StringValue);
			attr.ns = this.nameTable.Add("http://www.w3.org/2000/xmlns/");
			if (!this.curNode.xmlContextPushed)
			{
				this.PushXmlContext();
			}
			this.xmlContext.defaultNamespace = text;
			this.AddNamespace(string.Empty, text, attr);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0002CB70 File Offset: 0x0002AD70
		private void OnNamespaceDecl(XmlTextReaderImpl.NodeData attr)
		{
			if (!this.supportNamespaces)
			{
				return;
			}
			string text = this.nameTable.Add(attr.StringValue);
			if (text.Length == 0)
			{
				this.Throw("Invalid namespace declaration.", attr.lineInfo2.lineNo, attr.lineInfo2.linePos - 1);
			}
			this.AddNamespace(attr.localName, text, attr);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x0002CBD4 File Offset: 0x0002ADD4
		private void OnXmlReservedAttribute(XmlTextReaderImpl.NodeData attr)
		{
			string localName = attr.localName;
			if (!(localName == "space"))
			{
				if (!(localName == "lang"))
				{
					return;
				}
				if (!this.curNode.xmlContextPushed)
				{
					this.PushXmlContext();
				}
				this.xmlContext.xmlLang = attr.StringValue;
				return;
			}
			else
			{
				if (!this.curNode.xmlContextPushed)
				{
					this.PushXmlContext();
				}
				string a = XmlConvert.TrimString(attr.StringValue);
				if (a == "preserve")
				{
					this.xmlContext.xmlSpace = XmlSpace.Preserve;
					return;
				}
				if (!(a == "default"))
				{
					this.Throw("'{0}' is an invalid xml:space value.", attr.StringValue, attr.lineInfo.lineNo, attr.lineInfo.linePos);
					return;
				}
				this.xmlContext.xmlSpace = XmlSpace.Default;
				return;
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0002CCA4 File Offset: 0x0002AEA4
		private void ParseAttributeValueSlow(int curPos, char quoteChar, XmlTextReaderImpl.NodeData attr)
		{
			int num = curPos;
			char[] chars = this.ps.chars;
			int entityId = this.ps.entityId;
			int num2 = 0;
			LineInfo lineInfo = new LineInfo(this.ps.lineNo, this.ps.LinePos);
			XmlTextReaderImpl.NodeData nodeData = null;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)chars[num]] & 128) == 0)
				{
					if (num - this.ps.charPos > 0)
					{
						this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
						this.ps.charPos = num;
					}
					if (chars[num] == quoteChar && entityId == this.ps.entityId)
					{
						goto IL_63F;
					}
					char c = chars[num];
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							num++;
							if (this.normalize)
							{
								this.stringBuilder.Append(' ');
								this.ps.charPos = this.ps.charPos + 1;
								continue;
							}
							continue;
						case '\n':
							num++;
							this.OnNewLine(num);
							if (this.normalize)
							{
								this.stringBuilder.Append(' ');
								this.ps.charPos = this.ps.charPos + 1;
								continue;
							}
							continue;
						case '\v':
						case '\f':
							goto IL_4F8;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								num += 2;
								if (this.normalize)
								{
									this.stringBuilder.Append(this.ps.eolNormalized ? "  " : " ");
									this.ps.charPos = num;
								}
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_54A;
								}
								num++;
								if (this.normalize)
								{
									this.stringBuilder.Append(' ');
									this.ps.charPos = num;
								}
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c != '"')
							{
								if (c != '&')
								{
									goto IL_4F8;
								}
								if (num - this.ps.charPos > 0)
								{
									this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
								}
								this.ps.charPos = num;
								int entityId2 = this.ps.entityId;
								LineInfo lineInfo2 = new LineInfo(this.ps.lineNo, this.ps.LinePos + 1);
								switch (this.HandleEntityReference(true, XmlTextReaderImpl.EntityExpandType.All, out num))
								{
								case XmlTextReaderImpl.EntityType.CharacterDec:
								case XmlTextReaderImpl.EntityType.CharacterHex:
								case XmlTextReaderImpl.EntityType.CharacterNamed:
									break;
								case XmlTextReaderImpl.EntityType.Expanded:
								case XmlTextReaderImpl.EntityType.Skipped:
								case XmlTextReaderImpl.EntityType.FakeExpanded:
									goto IL_4DB;
								case XmlTextReaderImpl.EntityType.Unexpanded:
									if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && this.ps.entityId == entityId)
									{
										int num3 = this.stringBuilder.Length - num2;
										if (num3 > 0)
										{
											XmlTextReaderImpl.NodeData nodeData2 = new XmlTextReaderImpl.NodeData();
											nodeData2.lineInfo = lineInfo;
											nodeData2.depth = attr.depth + 1;
											nodeData2.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num3));
											this.AddAttributeChunkToList(attr, nodeData2, ref nodeData);
										}
										this.ps.charPos = this.ps.charPos + 1;
										string text = this.ParseEntityName();
										XmlTextReaderImpl.NodeData nodeData3 = new XmlTextReaderImpl.NodeData();
										nodeData3.lineInfo = lineInfo2;
										nodeData3.depth = attr.depth + 1;
										nodeData3.SetNamedNode(XmlNodeType.EntityReference, text);
										this.AddAttributeChunkToList(attr, nodeData3, ref nodeData);
										this.stringBuilder.Append('&');
										this.stringBuilder.Append(text);
										this.stringBuilder.Append(';');
										num2 = this.stringBuilder.Length;
										lineInfo.Set(this.ps.LineNo, this.ps.LinePos);
										this.fullAttrCleanup = true;
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
										this.ParseEntityName();
									}
									num = this.ps.charPos;
									break;
								case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
									if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && entityId2 == entityId)
									{
										int num4 = this.stringBuilder.Length - num2;
										if (num4 > 0)
										{
											XmlTextReaderImpl.NodeData nodeData4 = new XmlTextReaderImpl.NodeData();
											nodeData4.lineInfo = lineInfo;
											nodeData4.depth = attr.depth + 1;
											nodeData4.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num4));
											this.AddAttributeChunkToList(attr, nodeData4, ref nodeData);
										}
										XmlTextReaderImpl.NodeData nodeData5 = new XmlTextReaderImpl.NodeData();
										nodeData5.lineInfo = lineInfo2;
										nodeData5.depth = attr.depth + 1;
										nodeData5.SetNamedNode(XmlNodeType.EntityReference, this.ps.entity.Name);
										this.AddAttributeChunkToList(attr, nodeData5, ref nodeData);
										this.fullAttrCleanup = true;
									}
									num = this.ps.charPos;
									break;
								default:
									goto IL_4DB;
								}
								IL_4E7:
								chars = this.ps.chars;
								continue;
								IL_4DB:
								num = this.ps.charPos;
								goto IL_4E7;
							}
							break;
						}
					}
					else if (c != '\'')
					{
						if (c == '<')
						{
							this.Throw(num, "'{0}', hexadecimal value {1}, is an invalid attribute character.", XmlException.BuildCharExceptionArgs('<', '\0'));
							goto IL_54A;
						}
						if (c != '>')
						{
							goto IL_4F8;
						}
					}
					num++;
					continue;
					IL_4F8:
					if (num != this.ps.charsUsed)
					{
						if (XmlCharType.IsHighSurrogate((int)chars[num]))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_54A;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)chars[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
					}
					IL_54A:
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r')
							{
								this.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							if (!this.InEntity)
							{
								if (this.fragmentType == XmlNodeType.Attribute)
								{
									break;
								}
								this.Throw("There is an unclosed literal string.");
							}
							if (this.HandleEntityEnd(true))
							{
								this.Throw("An internal error has occurred.");
							}
							if (entityId == this.ps.entityId)
							{
								num2 = this.stringBuilder.Length;
								lineInfo.Set(this.ps.LineNo, this.ps.LinePos);
							}
						}
					}
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				else
				{
					num++;
				}
			}
			if (entityId != this.ps.entityId)
			{
				this.Throw("Entity replacement text must nest properly within markup declarations.");
			}
			IL_63F:
			if (attr.nextAttrValueChunk != null)
			{
				int num5 = this.stringBuilder.Length - num2;
				if (num5 > 0)
				{
					XmlTextReaderImpl.NodeData nodeData6 = new XmlTextReaderImpl.NodeData();
					nodeData6.lineInfo = lineInfo;
					nodeData6.depth = attr.depth + 1;
					nodeData6.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString(num2, num5));
					this.AddAttributeChunkToList(attr, nodeData6, ref nodeData);
				}
			}
			this.ps.charPos = num + 1;
			attr.SetValue(this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0002D377 File Offset: 0x0002B577
		private void AddAttributeChunkToList(XmlTextReaderImpl.NodeData attr, XmlTextReaderImpl.NodeData chunk, ref XmlTextReaderImpl.NodeData lastChunk)
		{
			if (lastChunk == null)
			{
				lastChunk = chunk;
				attr.nextAttrValueChunk = chunk;
				return;
			}
			lastChunk.nextAttrValueChunk = chunk;
			lastChunk = chunk;
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0002D394 File Offset: 0x0002B594
		private bool ParseText()
		{
			int num = 0;
			if (this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
			{
				int num2;
				int num3;
				while (!this.ParseText(out num2, out num3, ref num))
				{
				}
			}
			else
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				int num2;
				int num3;
				if (this.ParseText(out num2, out num3, ref num))
				{
					if (num3 - num2 != 0)
					{
						XmlNodeType textNodeType = this.GetTextNodeType(num);
						if (textNodeType != XmlNodeType.None)
						{
							this.curNode.SetValueNode(textNodeType, this.ps.chars, num2, num3 - num2);
							return true;
						}
					}
				}
				else if (this.v1Compat)
				{
					do
					{
						if (num3 - num2 > 0)
						{
							this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
						}
					}
					while (!this.ParseText(out num2, out num3, ref num));
					if (num3 - num2 > 0)
					{
						this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
					}
					XmlNodeType textNodeType2 = this.GetTextNodeType(num);
					if (textNodeType2 != XmlNodeType.None)
					{
						this.curNode.SetValueNode(textNodeType2, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						return true;
					}
					this.stringBuilder.Length = 0;
				}
				else
				{
					if (num > 32)
					{
						this.curNode.SetValueNode(XmlNodeType.Text, this.ps.chars, num2, num3 - num2);
						this.nextParsingFunction = this.parsingFunction;
						this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PartialTextValue;
						return true;
					}
					if (num3 - num2 > 0)
					{
						this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
					}
					bool flag;
					do
					{
						flag = this.ParseText(out num2, out num3, ref num);
						if (num3 - num2 > 0)
						{
							this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
						}
					}
					while (!flag && num <= 32 && this.stringBuilder.Length < 4096);
					XmlNodeType xmlNodeType = (this.stringBuilder.Length < 4096) ? this.GetTextNodeType(num) : XmlNodeType.Text;
					if (xmlNodeType != XmlNodeType.None)
					{
						this.curNode.SetValueNode(xmlNodeType, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						if (!flag)
						{
							this.nextParsingFunction = this.parsingFunction;
							this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PartialTextValue;
						}
						return true;
					}
					this.stringBuilder.Length = 0;
					if (!flag)
					{
						while (!this.ParseText(out num2, out num3, ref num))
						{
						}
					}
				}
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ReportEndEntity)
			{
				this.SetupEndEntityNodeInContent();
				this.parsingFunction = this.nextParsingFunction;
				return true;
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
			{
				this.parsingFunction = this.nextNextParsingFunction;
				this.ParseEntityReference();
				return true;
			}
			return false;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002D620 File Offset: 0x0002B820
		private bool ParseText(out int startPos, out int endPos, ref int outOrChars)
		{
			char[] chars = this.ps.chars;
			int num = this.ps.charPos;
			int num2 = 0;
			int num3 = -1;
			int num4 = outOrChars;
			char c;
			int num7;
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)(c = chars[num])] & 64) == 0)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							num++;
							continue;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							break;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
								{
									if (num - this.ps.charPos > 0)
									{
										if (num2 == 0)
										{
											num2 = 1;
											num3 = num;
										}
										else
										{
											this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
											num3 = num - num2;
											num2++;
										}
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
									}
								}
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_366;
								}
								if (!this.ps.eolNormalized)
								{
									chars[num] = '\n';
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c == '&')
							{
								int num6;
								XmlTextReaderImpl.EntityType entityType;
								int num5;
								if ((num5 = this.ParseCharRefInline(num, out num6, out entityType)) > 0)
								{
									if (num2 > 0)
									{
										this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
									}
									num3 = num - num2;
									num2 += num5 - num - num6;
									num = num5;
									if (!this.xmlCharType.IsWhiteSpace(chars[num5 - num6]) || (this.v1Compat && entityType == XmlTextReaderImpl.EntityType.CharacterDec))
									{
										num4 |= 255;
										continue;
									}
									continue;
								}
								else
								{
									if (num > this.ps.charPos)
									{
										goto IL_42F;
									}
									switch (this.HandleEntityReference(false, XmlTextReaderImpl.EntityExpandType.All, out num))
									{
									case XmlTextReaderImpl.EntityType.CharacterDec:
										if (!this.v1Compat)
										{
											goto IL_221;
										}
										num4 |= 255;
										break;
									case XmlTextReaderImpl.EntityType.CharacterHex:
									case XmlTextReaderImpl.EntityType.CharacterNamed:
										goto IL_221;
									case XmlTextReaderImpl.EntityType.Expanded:
									case XmlTextReaderImpl.EntityType.Skipped:
									case XmlTextReaderImpl.EntityType.FakeExpanded:
										goto IL_249;
									case XmlTextReaderImpl.EntityType.Unexpanded:
										goto IL_1F4;
									default:
										goto IL_249;
									}
									IL_255:
									chars = this.ps.chars;
									continue;
									IL_249:
									num = this.ps.charPos;
									goto IL_255;
									IL_221:
									if (!this.xmlCharType.IsWhiteSpace(this.ps.chars[num - 1]))
									{
										num4 |= 255;
										goto IL_255;
									}
									goto IL_255;
								}
							}
							break;
						}
					}
					else
					{
						if (c == '<')
						{
							goto IL_42F;
						}
						if (c == ']')
						{
							if (this.ps.charsUsed - num >= 3 || this.ps.isEof)
							{
								if (chars[num + 1] == ']' && chars[num + 2] == '>')
								{
									this.Throw(num, "']]>' is not allowed in character data.");
								}
								num4 |= 93;
								num++;
								continue;
							}
							goto IL_366;
						}
					}
					if (num != this.ps.charsUsed)
					{
						char c2 = chars[num];
						if (XmlCharType.IsHighSurrogate((int)c2))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_366;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)chars[num]))
							{
								num++;
								num4 |= (int)c2;
								continue;
							}
						}
						num7 = num - this.ps.charPos;
						if (this.ZeroEndingStream(num))
						{
							goto Block_29;
						}
						this.ThrowInvalidChar(this.ps.chars, this.ps.charsUsed, this.ps.charPos + num7);
					}
					IL_366:
					if (num > this.ps.charPos)
					{
						goto IL_42F;
					}
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r' && this.ps.chars[this.ps.charPos] != ']')
							{
								this.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							if (!this.InEntity)
							{
								goto IL_423;
							}
							if (this.HandleEntityEnd(true))
							{
								goto Block_36;
							}
						}
					}
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				else
				{
					num4 |= (int)c;
					num++;
				}
			}
			IL_1F4:
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.EntityReference;
			goto IL_423;
			Block_29:
			chars = this.ps.chars;
			num = this.ps.charPos + num7;
			goto IL_42F;
			Block_36:
			this.nextParsingFunction = this.parsingFunction;
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ReportEndEntity;
			IL_423:
			startPos = (endPos = num);
			return true;
			IL_42F:
			if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
			}
			startPos = this.ps.charPos;
			endPos = num - num2;
			this.ps.charPos = num;
			outOrChars = num4;
			return c == '<';
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0002DAA0 File Offset: 0x0002BCA0
		private void FinishPartialValue()
		{
			this.curNode.CopyTo(this.readValueOffset, this.stringBuilder);
			int num = 0;
			int num2;
			int num3;
			while (!this.ParseText(out num2, out num3, ref num))
			{
				this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
			}
			this.stringBuilder.Append(this.ps.chars, num2, num3 - num2);
			this.curNode.SetValue(this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x0002DB30 File Offset: 0x0002BD30
		private void FinishOtherValueIterator()
		{
			switch (this.parsingFunction)
			{
			case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
				break;
			case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
				if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
				{
					this.FinishPartialValue();
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue;
					return;
				}
				if (this.readValueOffset > 0)
				{
					this.curNode.SetValue(this.curNode.StringValue.Substring(this.readValueOffset));
					this.readValueOffset = 0;
					return;
				}
				break;
			case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
			case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
				switch (this.incReadState)
				{
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue:
					if (this.readValueOffset > 0)
					{
						this.curNode.SetValue(this.curNode.StringValue.Substring(this.readValueOffset));
						this.readValueOffset = 0;
						return;
					}
					break;
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue:
					this.FinishPartialValue();
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue;
					return;
				case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End:
					this.curNode.SetValue(string.Empty);
					break;
				default:
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002DC1C File Offset: 0x0002BE1C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SkipPartialTextValue()
		{
			int num = 0;
			this.parsingFunction = this.nextParsingFunction;
			int num2;
			int num3;
			while (!this.ParseText(out num2, out num3, ref num))
			{
			}
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0002DC45 File Offset: 0x0002BE45
		private void FinishReadValueChunk()
		{
			this.readValueOffset = 0;
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
			{
				this.SkipPartialTextValue();
				return;
			}
			this.parsingFunction = this.nextParsingFunction;
			this.nextParsingFunction = this.nextNextParsingFunction;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x0002DC78 File Offset: 0x0002BE78
		private void FinishReadContentAsBinary()
		{
			this.readValueOffset = 0;
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue)
			{
				this.SkipPartialTextValue();
			}
			else
			{
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.nextNextParsingFunction;
			}
			if (this.incReadState != XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End)
			{
				while (this.MoveToNextContentNode(true))
				{
				}
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002DCCC File Offset: 0x0002BECC
		private void FinishReadElementContentAsBinary()
		{
			this.FinishReadContentAsBinary();
			if (this.curNode.type != XmlNodeType.EndElement)
			{
				this.Throw("'{0}' is an invalid XmlNodeType.", this.curNode.type.ToString());
			}
			this.outerReader.Read();
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002DD1C File Offset: 0x0002BF1C
		private bool ParseRootLevelWhitespace()
		{
			XmlNodeType whitespaceType = this.GetWhitespaceType();
			if (whitespaceType == XmlNodeType.None)
			{
				this.EatWhitespaces(null);
				if (this.ps.chars[this.ps.charPos] == '<' || this.ps.charsUsed - this.ps.charPos == 0 || this.ZeroEndingStream(this.ps.charPos))
				{
					return false;
				}
			}
			else
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				this.EatWhitespaces(this.stringBuilder);
				if (this.ps.chars[this.ps.charPos] == '<' || this.ps.charsUsed - this.ps.charPos == 0 || this.ZeroEndingStream(this.ps.charPos))
				{
					if (this.stringBuilder.Length > 0)
					{
						this.curNode.SetValueNode(whitespaceType, this.stringBuilder.ToString());
						this.stringBuilder.Length = 0;
						return true;
					}
					return false;
				}
			}
			if (this.xmlCharType.IsCharData(this.ps.chars[this.ps.charPos]))
			{
				this.Throw("Data at the root level is invalid.");
			}
			else
			{
				this.ThrowInvalidChar(this.ps.chars, this.ps.charsUsed, this.ps.charPos);
			}
			return false;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002DE8C File Offset: 0x0002C08C
		private void ParseEntityReference()
		{
			this.ps.charPos = this.ps.charPos + 1;
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			this.curNode.SetNamedNode(XmlNodeType.EntityReference, this.ParseEntityName());
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0002DEDC File Offset: 0x0002C0DC
		private XmlTextReaderImpl.EntityType HandleEntityReference(bool isInAttributeValue, XmlTextReaderImpl.EntityExpandType expandType, out int charRefEndPos)
		{
			if (this.ps.charPos + 1 == this.ps.charsUsed && this.ReadData() == 0)
			{
				this.Throw("Unexpected end of file has occurred.");
			}
			if (this.ps.chars[this.ps.charPos + 1] == '#')
			{
				XmlTextReaderImpl.EntityType result;
				charRefEndPos = this.ParseNumericCharRef(expandType != XmlTextReaderImpl.EntityExpandType.OnlyGeneral, null, out result);
				return result;
			}
			charRefEndPos = this.ParseNamedCharRef(expandType != XmlTextReaderImpl.EntityExpandType.OnlyGeneral, null);
			if (charRefEndPos >= 0)
			{
				return XmlTextReaderImpl.EntityType.CharacterNamed;
			}
			if (expandType == XmlTextReaderImpl.EntityExpandType.OnlyCharacter || (this.entityHandling != EntityHandling.ExpandEntities && (!isInAttributeValue || !this.validatingReaderCompatFlag)))
			{
				return XmlTextReaderImpl.EntityType.Unexpanded;
			}
			this.ps.charPos = this.ps.charPos + 1;
			int linePos = this.ps.LinePos;
			int num;
			try
			{
				num = this.ParseName();
			}
			catch (XmlException)
			{
				this.Throw("An error occurred while parsing EntityName.", this.ps.LineNo, linePos);
				return XmlTextReaderImpl.EntityType.Skipped;
			}
			if (this.ps.chars[num] != ';')
			{
				this.ThrowUnexpectedToken(num, ";");
			}
			int linePos2 = this.ps.LinePos;
			string name = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			this.ps.charPos = num + 1;
			charRefEndPos = -1;
			XmlTextReaderImpl.EntityType result2 = this.HandleGeneralEntityReference(name, isInAttributeValue, false, linePos2);
			this.reportedBaseUri = this.ps.baseUriStr;
			this.reportedEncoding = this.ps.encoding;
			return result2;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002E068 File Offset: 0x0002C268
		private XmlTextReaderImpl.EntityType HandleGeneralEntityReference(string name, bool isInAttributeValue, bool pushFakeEntityIfNullResolver, int entityStartLinePos)
		{
			IDtdEntityInfo dtdEntityInfo = null;
			if (this.dtdInfo == null && this.fragmentParserContext != null && this.fragmentParserContext.HasDtdInfo && this.dtdProcessing == DtdProcessing.Parse)
			{
				this.ParseDtdFromParserContext();
			}
			if (this.dtdInfo == null || (dtdEntityInfo = this.dtdInfo.LookupEntity(name)) == null)
			{
				if (this.disableUndeclaredEntityCheck)
				{
					dtdEntityInfo = new SchemaEntity(new XmlQualifiedName(name), false)
					{
						Text = string.Empty
					};
				}
				else
				{
					this.Throw("Reference to undeclared entity '{0}'.", name, this.ps.LineNo, entityStartLinePos);
				}
			}
			if (dtdEntityInfo.IsUnparsedEntity)
			{
				if (this.disableUndeclaredEntityCheck)
				{
					dtdEntityInfo = new SchemaEntity(new XmlQualifiedName(name), false)
					{
						Text = string.Empty
					};
				}
				else
				{
					this.Throw("Reference to unparsed entity '{0}'.", name, this.ps.LineNo, entityStartLinePos);
				}
			}
			if (this.standalone && dtdEntityInfo.IsDeclaredInExternal)
			{
				this.Throw("Standalone document declaration must have a value of 'no' because an external entity '{0}' is referenced.", dtdEntityInfo.Name, this.ps.LineNo, entityStartLinePos);
			}
			if (dtdEntityInfo.IsExternal)
			{
				if (isInAttributeValue)
				{
					this.Throw("External entity '{0}' reference cannot appear in the attribute value.", name, this.ps.LineNo, entityStartLinePos);
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				if (this.parsingMode == XmlTextReaderImpl.ParsingMode.SkipContent)
				{
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				if (this.IsResolverNull)
				{
					if (pushFakeEntityIfNullResolver)
					{
						this.PushExternalEntity(dtdEntityInfo);
						this.curNode.entityId = this.ps.entityId;
						return XmlTextReaderImpl.EntityType.FakeExpanded;
					}
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				else
				{
					this.PushExternalEntity(dtdEntityInfo);
					this.curNode.entityId = this.ps.entityId;
					if (!isInAttributeValue || !this.validatingReaderCompatFlag)
					{
						return XmlTextReaderImpl.EntityType.Expanded;
					}
					return XmlTextReaderImpl.EntityType.ExpandedInAttribute;
				}
			}
			else
			{
				if (this.parsingMode == XmlTextReaderImpl.ParsingMode.SkipContent)
				{
					return XmlTextReaderImpl.EntityType.Skipped;
				}
				this.PushInternalEntity(dtdEntityInfo);
				this.curNode.entityId = this.ps.entityId;
				if (!isInAttributeValue || !this.validatingReaderCompatFlag)
				{
					return XmlTextReaderImpl.EntityType.Expanded;
				}
				return XmlTextReaderImpl.EntityType.ExpandedInAttribute;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002E22B File Offset: 0x0002C42B
		private bool InEntity
		{
			get
			{
				return this.parsingStatesStackTop >= 0;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002E23C File Offset: 0x0002C43C
		private bool HandleEntityEnd(bool checkEntityNesting)
		{
			if (this.parsingStatesStackTop == -1)
			{
				this.Throw("An internal error has occurred.");
			}
			if (this.ps.entityResolvedManually)
			{
				this.index--;
				if (checkEntityNesting && this.ps.entityId != this.nodes[this.index].entityId)
				{
					this.Throw("Incomplete entity contents.");
				}
				this.lastEntity = this.ps.entity;
				this.PopEntity();
				return true;
			}
			if (checkEntityNesting && this.ps.entityId != this.nodes[this.index].entityId)
			{
				this.Throw("Incomplete entity contents.");
			}
			this.PopEntity();
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			return false;
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002E314 File Offset: 0x0002C514
		private void SetupEndEntityNodeInContent()
		{
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			this.curNode = this.nodes[this.index];
			this.curNode.SetNamedNode(XmlNodeType.EndEntity, this.lastEntity.Name);
			this.curNode.lineInfo.Set(this.ps.lineNo, this.ps.LinePos - 1);
			if (this.index == 0 && this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ElementContent)
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.DocumentContent;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		private void SetupEndEntityNodeInAttribute()
		{
			this.curNode = this.nodes[this.index + this.attrCount + 1];
			XmlTextReaderImpl.NodeData nodeData = this.curNode;
			nodeData.lineInfo.linePos = nodeData.lineInfo.linePos + this.curNode.localName.Length;
			this.curNode.type = XmlNodeType.EndEntity;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002E40A File Offset: 0x0002C60A
		private bool ParsePI()
		{
			return this.ParsePI(null);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002E414 File Offset: 0x0002C614
		private bool ParsePI(StringBuilder piInDtdStringBuilder)
		{
			if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			}
			int num = this.ParseName();
			string text = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			if (string.Compare(text, "xml", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this.Throw(text.Equals("xml") ? "Unexpected XML declaration. The XML declaration must be the first node in the document, and no white space characters are allowed to appear before it." : "'{0}' is an invalid name for processing instructions.", text);
			}
			this.ps.charPos = num;
			if (piInDtdStringBuilder == null)
			{
				if (!this.ignorePIs && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
				{
					this.curNode.SetNamedNode(XmlNodeType.ProcessingInstruction, text);
				}
			}
			else
			{
				piInDtdStringBuilder.Append(text);
			}
			char c = this.ps.chars[this.ps.charPos];
			if (this.EatWhitespaces(piInDtdStringBuilder) == 0)
			{
				if (this.ps.charsUsed - this.ps.charPos < 2)
				{
					this.ReadData();
				}
				if (c != '?' || this.ps.chars[this.ps.charPos + 1] != '>')
				{
					this.Throw("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(this.ps.chars, this.ps.charsUsed, this.ps.charPos));
				}
			}
			int num2;
			int num3;
			if (this.ParsePIValue(out num2, out num3))
			{
				if (piInDtdStringBuilder == null)
				{
					if (this.ignorePIs)
					{
						return false;
					}
					if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
					{
						this.curNode.SetValue(this.ps.chars, num2, num3 - num2);
					}
				}
				else
				{
					piInDtdStringBuilder.Append(this.ps.chars, num2, num3 - num2);
				}
			}
			else
			{
				StringBuilder stringBuilder;
				if (piInDtdStringBuilder == null)
				{
					if (this.ignorePIs || this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
					{
						while (!this.ParsePIValue(out num2, out num3))
						{
						}
						return false;
					}
					stringBuilder = this.stringBuilder;
				}
				else
				{
					stringBuilder = piInDtdStringBuilder;
				}
				do
				{
					stringBuilder.Append(this.ps.chars, num2, num3 - num2);
				}
				while (!this.ParsePIValue(out num2, out num3));
				stringBuilder.Append(this.ps.chars, num2, num3 - num2);
				if (piInDtdStringBuilder == null)
				{
					this.curNode.SetValue(this.stringBuilder.ToString());
					this.stringBuilder.Length = 0;
				}
			}
			return true;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0002E66C File Offset: 0x0002C86C
		private bool ParsePIValue(out int outStartPos, out int outEndPos)
		{
			if (this.ps.charsUsed - this.ps.charPos < 2 && this.ReadData() == 0)
			{
				this.Throw(this.ps.charsUsed, "Unexpected end of file while parsing {0} has occurred.", "PI");
			}
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = 0;
			int num3 = -1;
			for (;;)
			{
				char c;
				if ((this.xmlCharType.charProperties[(int)(c = chars[num])] & 64) == 0 || c == '?')
				{
					char c2 = chars[num];
					if (c2 <= '&')
					{
						switch (c2)
						{
						case '\t':
							break;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							goto IL_1F0;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
								{
									if (num - this.ps.charPos > 0)
									{
										if (num2 == 0)
										{
											num2 = 1;
											num3 = num;
										}
										else
										{
											this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
											num3 = num - num2;
											num2++;
										}
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
									}
								}
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_247;
								}
								if (!this.ps.eolNormalized)
								{
									chars[num] = '\n';
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c2 != '&')
							{
								goto IL_1F0;
							}
							break;
						}
					}
					else if (c2 != '<')
					{
						if (c2 != '?')
						{
							if (c2 != ']')
							{
								goto IL_1F0;
							}
						}
						else
						{
							if (chars[num + 1] == '>')
							{
								break;
							}
							if (num + 1 != this.ps.charsUsed)
							{
								num++;
								continue;
							}
							goto IL_247;
						}
					}
					num++;
					continue;
					IL_1F0:
					if (num == this.ps.charsUsed)
					{
						goto IL_247;
					}
					if (XmlCharType.IsHighSurrogate((int)chars[num]))
					{
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_247;
						}
						num++;
						if (XmlCharType.IsLowSurrogate((int)chars[num]))
						{
							num++;
							continue;
						}
					}
					this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
				}
				else
				{
					num++;
				}
			}
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num + 2;
			return true;
			IL_247:
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num;
			return false;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002E8F8 File Offset: 0x0002CAF8
		private bool ParseComment()
		{
			if (this.ignoreComments)
			{
				XmlTextReaderImpl.ParsingMode parsingMode = this.parsingMode;
				this.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
				this.ParseCDataOrComment(XmlNodeType.Comment);
				this.parsingMode = parsingMode;
				return false;
			}
			this.ParseCDataOrComment(XmlNodeType.Comment);
			return true;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002E933 File Offset: 0x0002CB33
		private void ParseCData()
		{
			this.ParseCDataOrComment(XmlNodeType.CDATA);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002E93C File Offset: 0x0002CB3C
		private void ParseCDataOrComment(XmlNodeType type)
		{
			int num;
			int num2;
			if (this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
			{
				while (!this.ParseCDataOrComment(type, out num, out num2))
				{
				}
				return;
			}
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			if (this.ParseCDataOrComment(type, out num, out num2))
			{
				this.curNode.SetValueNode(type, this.ps.chars, num, num2 - num);
				return;
			}
			do
			{
				this.stringBuilder.Append(this.ps.chars, num, num2 - num);
			}
			while (!this.ParseCDataOrComment(type, out num, out num2));
			this.stringBuilder.Append(this.ps.chars, num, num2 - num);
			this.curNode.SetValueNode(type, this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002EA14 File Offset: 0x0002CC14
		private bool ParseCDataOrComment(XmlNodeType type, out int outStartPos, out int outEndPos)
		{
			if (this.ps.charsUsed - this.ps.charPos < 3 && this.ReadData() == 0)
			{
				this.Throw("Unexpected end of file while parsing {0} has occurred.", (type == XmlNodeType.Comment) ? "Comment" : "CDATA");
			}
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = 0;
			int num3 = -1;
			char c = (type == XmlNodeType.Comment) ? '-' : ']';
			for (;;)
			{
				char c2;
				if ((this.xmlCharType.charProperties[(int)(c2 = chars[num])] & 64) == 0 || c2 == c)
				{
					if (chars[num] == c)
					{
						if (chars[num + 1] == c)
						{
							if (chars[num + 2] == '>')
							{
								break;
							}
							if (num + 2 == this.ps.charsUsed)
							{
								goto IL_27D;
							}
							if (type == XmlNodeType.Comment)
							{
								this.Throw(num, "An XML comment cannot contain '--', and '-' cannot be the last character.");
							}
						}
						else if (num + 1 == this.ps.charsUsed)
						{
							goto IL_27D;
						}
						num++;
					}
					else
					{
						char c3 = chars[num];
						if (c3 <= '&')
						{
							switch (c3)
							{
							case '\t':
								break;
							case '\n':
								num++;
								this.OnNewLine(num);
								continue;
							case '\v':
							case '\f':
								goto IL_22B;
							case '\r':
								if (chars[num + 1] == '\n')
								{
									if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
									{
										if (num - this.ps.charPos > 0)
										{
											if (num2 == 0)
											{
												num2 = 1;
												num3 = num;
											}
											else
											{
												this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
												num3 = num - num2;
												num2++;
											}
										}
										else
										{
											this.ps.charPos = this.ps.charPos + 1;
										}
									}
									num += 2;
								}
								else
								{
									if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
									{
										goto IL_27D;
									}
									if (!this.ps.eolNormalized)
									{
										chars[num] = '\n';
									}
									num++;
								}
								this.OnNewLine(num);
								continue;
							default:
								if (c3 != '&')
								{
									goto IL_22B;
								}
								break;
							}
						}
						else if (c3 != '<' && c3 != ']')
						{
							goto IL_22B;
						}
						num++;
						continue;
						IL_22B:
						if (num == this.ps.charsUsed)
						{
							goto IL_27D;
						}
						if (!XmlCharType.IsHighSurrogate((int)chars[num]))
						{
							goto IL_26A;
						}
						if (num + 1 == this.ps.charsUsed)
						{
							goto IL_27D;
						}
						num++;
						if (!XmlCharType.IsLowSurrogate((int)chars[num]))
						{
							goto IL_26A;
						}
						num++;
					}
				}
				else
				{
					num++;
				}
			}
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num + 3;
			return true;
			IL_26A:
			this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
			IL_27D:
			if (num2 > 0)
			{
				this.ShiftBuffer(num3 + num2, num3, num - num3 - num2);
				outEndPos = num - num2;
			}
			else
			{
				outEndPos = num;
			}
			outStartPos = this.ps.charPos;
			this.ps.charPos = num;
			return false;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002ECD8 File Offset: 0x0002CED8
		private bool ParseDoctypeDecl()
		{
			if (this.dtdProcessing == DtdProcessing.Prohibit)
			{
				this.ThrowWithoutLineInfo(this.v1Compat ? "DTD is prohibited in this XML document." : "For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.");
			}
			while (this.ps.charsUsed - this.ps.charPos < 8)
			{
				if (this.ReadData() == 0)
				{
					this.Throw("Unexpected end of file while parsing {0} has occurred.", "DOCTYPE");
				}
			}
			if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 7, "DOCTYPE"))
			{
				this.ThrowUnexpectedToken((!this.rootElementParsed && this.dtdInfo == null) ? "DOCTYPE" : "<!--");
			}
			if (!this.xmlCharType.IsWhiteSpace(this.ps.chars[this.ps.charPos + 7]))
			{
				this.ThrowExpectingWhitespace(this.ps.charPos + 7);
			}
			if (this.dtdInfo != null)
			{
				this.Throw(this.ps.charPos - 2, "Cannot have multiple DTDs.");
			}
			if (this.rootElementParsed)
			{
				this.Throw(this.ps.charPos - 2, "DTD must be defined before the document root element.");
			}
			this.ps.charPos = this.ps.charPos + 8;
			this.EatWhitespaces(null);
			if (this.dtdProcessing == DtdProcessing.Parse)
			{
				this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
				this.ParseDtd();
				this.nextParsingFunction = this.parsingFunction;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel;
				return true;
			}
			this.SkipDtd();
			return false;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002EE60 File Offset: 0x0002D060
		private void ParseDtd()
		{
			IDtdParser dtdParser = DtdParser.Create();
			this.dtdInfo = dtdParser.ParseInternalDtd(new XmlTextReaderImpl.DtdParserProxy(this), true);
			if ((this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
			this.curNode.SetNamedNode(XmlNodeType.DocumentType, this.dtdInfo.Name.ToString(), string.Empty, null);
			this.curNode.SetValue(this.dtdInfo.InternalDtdSubset);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002EEF0 File Offset: 0x0002D0F0
		private void SkipDtd()
		{
			int num;
			int charPos = this.ParseQName(out num);
			this.ps.charPos = charPos;
			this.EatWhitespaces(null);
			if (this.ps.chars[this.ps.charPos] == 'P')
			{
				while (this.ps.charsUsed - this.ps.charPos < 6)
				{
					if (this.ReadData() == 0)
					{
						this.Throw("Unexpected end of file has occurred.");
					}
				}
				if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 6, "PUBLIC"))
				{
					this.ThrowUnexpectedToken("PUBLIC");
				}
				this.ps.charPos = this.ps.charPos + 6;
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				this.EatWhitespaces(null);
			}
			else if (this.ps.chars[this.ps.charPos] == 'S')
			{
				while (this.ps.charsUsed - this.ps.charPos < 6)
				{
					if (this.ReadData() == 0)
					{
						this.Throw("Unexpected end of file has occurred.");
					}
				}
				if (!XmlConvert.StrEqual(this.ps.chars, this.ps.charPos, 6, "SYSTEM"))
				{
					this.ThrowUnexpectedToken("SYSTEM");
				}
				this.ps.charPos = this.ps.charPos + 6;
				if (this.EatWhitespaces(null) == 0)
				{
					this.ThrowExpectingWhitespace(this.ps.charPos);
				}
				this.SkipPublicOrSystemIdLiteral();
				this.EatWhitespaces(null);
			}
			else if (this.ps.chars[this.ps.charPos] != '[' && this.ps.chars[this.ps.charPos] != '>')
			{
				this.Throw("Expecting external ID, '[' or '>'.");
			}
			if (this.ps.chars[this.ps.charPos] == '[')
			{
				this.ps.charPos = this.ps.charPos + 1;
				this.SkipUntil(']', true);
				this.EatWhitespaces(null);
				if (this.ps.chars[this.ps.charPos] != '>')
				{
					this.ThrowUnexpectedToken(">");
				}
			}
			else if (this.ps.chars[this.ps.charPos] == '>')
			{
				this.curNode.SetValue(string.Empty);
			}
			else
			{
				this.Throw("Expecting an internal subset or the end of the DOCTYPE declaration.");
			}
			this.ps.charPos = this.ps.charPos + 1;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002F194 File Offset: 0x0002D394
		private void SkipPublicOrSystemIdLiteral()
		{
			char c = this.ps.chars[this.ps.charPos];
			if (c != '"' && c != '\'')
			{
				this.ThrowUnexpectedToken("\"", "'");
			}
			this.ps.charPos = this.ps.charPos + 1;
			this.SkipUntil(c, false);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002F1EC File Offset: 0x0002D3EC
		private void SkipUntil(char stopChar, bool recognizeLiterals)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			char c = '"';
			char[] chars = this.ps.chars;
			int num = this.ps.charPos;
			for (;;)
			{
				char c2;
				if ((this.xmlCharType.charProperties[(int)(c2 = chars[num])] & 128) == 0 || chars[num] == stopChar || c2 == '-' || c2 == '?')
				{
					if (c2 == stopChar && !flag)
					{
						break;
					}
					this.ps.charPos = num;
					if (c2 <= '&')
					{
						switch (c2)
						{
						case '\t':
							break;
						case '\n':
							num++;
							this.OnNewLine(num);
							continue;
						case '\v':
						case '\f':
							goto IL_2D1;
						case '\r':
							if (chars[num + 1] == '\n')
							{
								num += 2;
							}
							else
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_32F;
								}
								num++;
							}
							this.OnNewLine(num);
							continue;
						default:
							if (c2 == '"')
							{
								goto IL_2AC;
							}
							if (c2 != '&')
							{
								goto IL_2D1;
							}
							break;
						}
					}
					else if (c2 <= '-')
					{
						if (c2 == '\'')
						{
							goto IL_2AC;
						}
						if (c2 != '-')
						{
							goto IL_2D1;
						}
						if (flag2)
						{
							if (num + 2 >= this.ps.charsUsed && !this.ps.isEof)
							{
								goto IL_32F;
							}
							if (chars[num + 1] == '-' && chars[num + 2] == '>')
							{
								flag2 = false;
								num += 2;
								continue;
							}
						}
						num++;
						continue;
					}
					else
					{
						switch (c2)
						{
						case '<':
							if (chars[num + 1] == '?')
							{
								if (recognizeLiterals && !flag && !flag2)
								{
									flag3 = true;
									num += 2;
									continue;
								}
							}
							else if (chars[num + 1] == '!')
							{
								if (num + 3 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_32F;
								}
								if (chars[num + 2] == '-' && chars[num + 3] == '-' && recognizeLiterals && !flag && !flag3)
								{
									flag2 = true;
									num += 4;
									continue;
								}
							}
							else if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
							{
								goto IL_32F;
							}
							num++;
							continue;
						case '=':
							goto IL_2D1;
						case '>':
							break;
						case '?':
							if (flag3)
							{
								if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_32F;
								}
								if (chars[num + 1] == '>')
								{
									flag3 = false;
									num++;
									continue;
								}
							}
							num++;
							continue;
						default:
							if (c2 != ']')
							{
								goto IL_2D1;
							}
							break;
						}
					}
					num++;
					continue;
					IL_2AC:
					if (flag)
					{
						if (c == c2)
						{
							flag = false;
						}
					}
					else if (recognizeLiterals && !flag2 && !flag3)
					{
						flag = true;
						c = c2;
					}
					num++;
					continue;
					IL_2D1:
					if (num != this.ps.charsUsed)
					{
						if (XmlCharType.IsHighSurrogate((int)chars[num]))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_32F;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)chars[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
					}
					IL_32F:
					if (this.ReadData() == 0)
					{
						if (this.ps.charsUsed - this.ps.charPos > 0)
						{
							if (this.ps.chars[this.ps.charPos] != '\r')
							{
								this.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							this.Throw("Unexpected end of file has occurred.");
						}
					}
					chars = this.ps.chars;
					num = this.ps.charPos;
				}
				else
				{
					num++;
				}
			}
			this.ps.charPos = num + 1;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002F59C File Offset: 0x0002D79C
		private int EatWhitespaces(StringBuilder sb)
		{
			int num = this.ps.charPos;
			int num2 = 0;
			char[] chars = this.ps.chars;
			for (;;)
			{
				char c = chars[num];
				switch (c)
				{
				case '\t':
					break;
				case '\n':
					num++;
					this.OnNewLine(num);
					continue;
				case '\v':
				case '\f':
					goto IL_FE;
				case '\r':
					if (chars[num + 1] == '\n')
					{
						int num3 = num - this.ps.charPos;
						if (sb != null && !this.ps.eolNormalized)
						{
							if (num3 > 0)
							{
								sb.Append(chars, this.ps.charPos, num3);
								num2 += num3;
							}
							this.ps.charPos = num + 1;
						}
						num += 2;
					}
					else
					{
						if (num + 1 >= this.ps.charsUsed && !this.ps.isEof)
						{
							goto IL_155;
						}
						if (!this.ps.eolNormalized)
						{
							chars[num] = '\n';
						}
						num++;
					}
					this.OnNewLine(num);
					continue;
				default:
					if (c != ' ')
					{
						goto IL_FE;
					}
					break;
				}
				num++;
				continue;
				IL_155:
				int num4 = num - this.ps.charPos;
				if (num4 > 0)
				{
					if (sb != null)
					{
						sb.Append(this.ps.chars, this.ps.charPos, num4);
					}
					this.ps.charPos = num;
					num2 += num4;
				}
				if (this.ReadData() == 0)
				{
					if (this.ps.charsUsed - this.ps.charPos == 0)
					{
						return num2;
					}
					if (this.ps.chars[this.ps.charPos] != '\r')
					{
						this.Throw("Unexpected end of file has occurred.");
					}
				}
				num = this.ps.charPos;
				chars = this.ps.chars;
				continue;
				IL_FE:
				if (num != this.ps.charsUsed)
				{
					break;
				}
				goto IL_155;
			}
			int num5 = num - this.ps.charPos;
			if (num5 > 0)
			{
				if (sb != null)
				{
					sb.Append(this.ps.chars, this.ps.charPos, num5);
				}
				this.ps.charPos = num;
				num2 += num5;
			}
			return num2;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002F7A6 File Offset: 0x0002D9A6
		private int ParseCharRefInline(int startPos, out int charCount, out XmlTextReaderImpl.EntityType entityType)
		{
			if (this.ps.chars[startPos + 1] == '#')
			{
				return this.ParseNumericCharRefInline(startPos, true, null, out charCount, out entityType);
			}
			charCount = 1;
			entityType = XmlTextReaderImpl.EntityType.CharacterNamed;
			return this.ParseNamedCharRefInline(startPos, true, null);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002F7D8 File Offset: 0x0002D9D8
		private int ParseNumericCharRef(bool expand, StringBuilder internalSubsetBuilder, out XmlTextReaderImpl.EntityType entityType)
		{
			int num2;
			int num;
			while ((num = this.ParseNumericCharRefInline(this.ps.charPos, expand, internalSubsetBuilder, out num2, out entityType)) == -2)
			{
				if (this.ReadData() == 0)
				{
					this.Throw("Unexpected end of file while parsing {0} has occurred.");
				}
			}
			if (expand)
			{
				this.ps.charPos = num - num2;
			}
			return num;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002F828 File Offset: 0x0002DA28
		private int ParseNumericCharRefInline(int startPos, bool expand, StringBuilder internalSubsetBuilder, out int charCount, out XmlTextReaderImpl.EntityType entityType)
		{
			int num = 0;
			string res = null;
			char[] chars = this.ps.chars;
			int num2 = startPos + 2;
			charCount = 0;
			int num3 = 0;
			try
			{
				if (chars[num2] == 'x')
				{
					num2++;
					num3 = num2;
					res = "Invalid syntax for a hexadecimal numeric entity reference.";
					for (;;)
					{
						char c = chars[num2];
						checked
						{
							if (c >= '0' && c <= '9')
							{
								num = num * 16 + (int)c - 48;
							}
							else if (c >= 'a' && c <= 'f')
							{
								num = num * 16 + 10 + (int)c - 97;
							}
							else
							{
								if (c < 'A' || c > 'F')
								{
									break;
								}
								num = num * 16 + 10 + (int)c - 65;
							}
						}
						num2++;
					}
					entityType = XmlTextReaderImpl.EntityType.CharacterHex;
				}
				else
				{
					if (num2 >= this.ps.charsUsed)
					{
						entityType = XmlTextReaderImpl.EntityType.Skipped;
						return -2;
					}
					num3 = num2;
					res = "Invalid syntax for a decimal numeric entity reference.";
					while (chars[num2] >= '0' && chars[num2] <= '9')
					{
						num = checked(num * 10 + (int)chars[num2] - 48);
						num2++;
					}
					entityType = XmlTextReaderImpl.EntityType.CharacterDec;
				}
			}
			catch (OverflowException innerException)
			{
				this.ps.charPos = num2;
				entityType = XmlTextReaderImpl.EntityType.Skipped;
				this.Throw("Invalid value of a character entity reference.", null, innerException);
			}
			if (chars[num2] != ';' || num3 == num2)
			{
				if (num2 == this.ps.charsUsed)
				{
					return -2;
				}
				this.Throw(num2, res);
			}
			if (num <= 65535)
			{
				char c2 = (char)num;
				if (!this.xmlCharType.IsCharData(c2) && ((this.v1Compat && this.normalize) || (!this.v1Compat && this.checkCharacters)))
				{
					this.Throw((this.ps.chars[startPos + 2] == 'x') ? (startPos + 3) : (startPos + 2), "'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(c2, '\0'));
				}
				if (expand)
				{
					if (internalSubsetBuilder != null)
					{
						internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num2 - this.ps.charPos + 1);
					}
					chars[num2] = c2;
				}
				charCount = 1;
				return num2 + 1;
			}
			char c3;
			char c4;
			XmlCharType.SplitSurrogateChar(num, out c3, out c4);
			if (this.normalize && (!XmlCharType.IsHighSurrogate((int)c4) || !XmlCharType.IsLowSurrogate((int)c3)))
			{
				this.Throw((this.ps.chars[startPos + 2] == 'x') ? (startPos + 3) : (startPos + 2), "'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(c4, c3));
			}
			if (expand)
			{
				if (internalSubsetBuilder != null)
				{
					internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num2 - this.ps.charPos + 1);
				}
				chars[num2 - 1] = c4;
				chars[num2] = c3;
			}
			charCount = 2;
			return num2 + 1;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002FAB0 File Offset: 0x0002DCB0
		private int ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
		{
			int num2;
			int num;
			for (;;)
			{
				num = (num2 = this.ParseNamedCharRefInline(this.ps.charPos, expand, internalSubsetBuilder));
				if (num2 != -2)
				{
					break;
				}
				if (this.ReadData() == 0)
				{
					return -1;
				}
			}
			if (num2 == -1)
			{
				return -1;
			}
			if (expand)
			{
				this.ps.charPos = num - 1;
			}
			return num;
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002FAFC File Offset: 0x0002DCFC
		private int ParseNamedCharRefInline(int startPos, bool expand, StringBuilder internalSubsetBuilder)
		{
			int num = startPos + 1;
			char[] chars = this.ps.chars;
			char c = chars[num];
			if (c <= 'g')
			{
				if (c != 'a')
				{
					if (c == 'g')
					{
						if (this.ps.charsUsed - num < 3)
						{
							return -2;
						}
						if (chars[num + 1] == 't' && chars[num + 2] == ';')
						{
							num += 3;
							char c2 = '>';
							goto IL_175;
						}
						return -1;
					}
				}
				else
				{
					num++;
					if (chars[num] == 'm')
					{
						if (this.ps.charsUsed - num < 3)
						{
							return -2;
						}
						if (chars[num + 1] == 'p' && chars[num + 2] == ';')
						{
							num += 3;
							char c2 = '&';
							goto IL_175;
						}
						return -1;
					}
					else if (chars[num] == 'p')
					{
						if (this.ps.charsUsed - num < 4)
						{
							return -2;
						}
						if (chars[num + 1] == 'o' && chars[num + 2] == 's' && chars[num + 3] == ';')
						{
							num += 4;
							char c2 = '\'';
							goto IL_175;
						}
						return -1;
					}
					else
					{
						if (num < this.ps.charsUsed)
						{
							return -1;
						}
						return -2;
					}
				}
			}
			else if (c != 'l')
			{
				if (c == 'q')
				{
					if (this.ps.charsUsed - num < 5)
					{
						return -2;
					}
					if (chars[num + 1] == 'u' && chars[num + 2] == 'o' && chars[num + 3] == 't' && chars[num + 4] == ';')
					{
						num += 5;
						char c2 = '"';
						goto IL_175;
					}
					return -1;
				}
			}
			else
			{
				if (this.ps.charsUsed - num < 3)
				{
					return -2;
				}
				if (chars[num + 1] == 't' && chars[num + 2] == ';')
				{
					num += 3;
					char c2 = '<';
					goto IL_175;
				}
				return -1;
			}
			return -1;
			IL_175:
			if (expand)
			{
				if (internalSubsetBuilder != null)
				{
					internalSubsetBuilder.Append(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
				}
				char c2;
				this.ps.chars[num - 1] = c2;
			}
			return num;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002FCC0 File Offset: 0x0002DEC0
		private int ParseName()
		{
			int num;
			return this.ParseQName(false, 0, out num);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0002FCD7 File Offset: 0x0002DED7
		private int ParseQName(out int colonPos)
		{
			return this.ParseQName(true, 0, out colonPos);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002FCE4 File Offset: 0x0002DEE4
		private int ParseQName(bool isQName, int startOffset, out int colonPos)
		{
			int num = -1;
			int num2 = this.ps.charPos + startOffset;
			for (;;)
			{
				char[] chars = this.ps.chars;
				if ((this.xmlCharType.charProperties[(int)chars[num2]] & 4) != 0)
				{
					num2++;
				}
				else
				{
					if (num2 + 1 >= this.ps.charsUsed)
					{
						if (this.ReadDataInName(ref num2))
						{
							continue;
						}
						this.Throw(num2, "Unexpected end of file while parsing {0} has occurred.", "Name");
					}
					if (chars[num2] != ':' || this.supportNamespaces)
					{
						this.Throw(num2, "Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(chars, this.ps.charsUsed, num2));
					}
				}
				for (;;)
				{
					if ((this.xmlCharType.charProperties[(int)chars[num2]] & 8) != 0)
					{
						num2++;
					}
					else if (chars[num2] == ':')
					{
						if (this.supportNamespaces)
						{
							break;
						}
						num = num2 - this.ps.charPos;
						num2++;
					}
					else
					{
						if (num2 != this.ps.charsUsed)
						{
							goto IL_135;
						}
						if (!this.ReadDataInName(ref num2))
						{
							goto IL_124;
						}
						chars = this.ps.chars;
					}
				}
				if (num != -1 || !isQName)
				{
					this.Throw(num2, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
				}
				num = num2 - this.ps.charPos;
				num2++;
			}
			IL_124:
			this.Throw(num2, "Unexpected end of file while parsing {0} has occurred.", "Name");
			IL_135:
			colonPos = ((num == -1) ? -1 : (this.ps.charPos + num));
			return num2;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002FE40 File Offset: 0x0002E040
		private bool ReadDataInName(ref int pos)
		{
			int num = pos - this.ps.charPos;
			bool result = this.ReadData() != 0;
			pos = this.ps.charPos + num;
			return result;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002FE74 File Offset: 0x0002E074
		private string ParseEntityName()
		{
			int num;
			try
			{
				num = this.ParseName();
			}
			catch (XmlException)
			{
				this.Throw("An error occurred while parsing EntityName.");
				return null;
			}
			if (this.ps.chars[num] != ';')
			{
				this.Throw("An error occurred while parsing EntityName.");
			}
			string result = this.nameTable.Add(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			this.ps.charPos = num + 1;
			return result;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0002FF04 File Offset: 0x0002E104
		private XmlTextReaderImpl.NodeData AddNode(int nodeIndex, int nodeDepth)
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[nodeIndex];
			if (nodeData != null)
			{
				nodeData.depth = nodeDepth;
				return nodeData;
			}
			return this.AllocNode(nodeIndex, nodeDepth);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002FF30 File Offset: 0x0002E130
		private XmlTextReaderImpl.NodeData AllocNode(int nodeIndex, int nodeDepth)
		{
			if (nodeIndex >= this.nodes.Length - 1)
			{
				XmlTextReaderImpl.NodeData[] destinationArray = new XmlTextReaderImpl.NodeData[this.nodes.Length * 2];
				Array.Copy(this.nodes, 0, destinationArray, 0, this.nodes.Length);
				this.nodes = destinationArray;
			}
			XmlTextReaderImpl.NodeData nodeData = this.nodes[nodeIndex];
			if (nodeData == null)
			{
				nodeData = new XmlTextReaderImpl.NodeData();
				this.nodes[nodeIndex] = nodeData;
			}
			nodeData.depth = nodeDepth;
			return nodeData;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002FF9A File Offset: 0x0002E19A
		private XmlTextReaderImpl.NodeData AddAttributeNoChecks(string name, int attrDepth)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, attrDepth);
			nodeData.SetNamedNode(XmlNodeType.Attribute, this.nameTable.Add(name));
			this.attrCount++;
			return nodeData;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0002FFD4 File Offset: 0x0002E1D4
		private XmlTextReaderImpl.NodeData AddAttribute(int endNamePos, int colonPos)
		{
			if (colonPos == -1 || !this.supportNamespaces)
			{
				string text = this.nameTable.Add(this.ps.chars, this.ps.charPos, endNamePos - this.ps.charPos);
				return this.AddAttribute(text, string.Empty, text);
			}
			this.attrNeedNamespaceLookup = true;
			int charPos = this.ps.charPos;
			int num = colonPos - charPos;
			if (num == this.lastPrefix.Length && XmlConvert.StrEqual(this.ps.chars, charPos, num, this.lastPrefix))
			{
				return this.AddAttribute(this.nameTable.Add(this.ps.chars, colonPos + 1, endNamePos - colonPos - 1), this.lastPrefix, null);
			}
			string prefix = this.nameTable.Add(this.ps.chars, charPos, num);
			this.lastPrefix = prefix;
			return this.AddAttribute(this.nameTable.Add(this.ps.chars, colonPos + 1, endNamePos - colonPos - 1), prefix, null);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000300DC File Offset: 0x0002E2DC
		private XmlTextReaderImpl.NodeData AddAttribute(string localName, string prefix, string nameWPrefix)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddNode(this.index + this.attrCount + 1, this.index + 1);
			nodeData.SetNamedNode(XmlNodeType.Attribute, localName, prefix, nameWPrefix);
			int num = 1 << (int)localName[0];
			if ((this.attrHashtable & num) == 0)
			{
				this.attrHashtable |= num;
			}
			else if (this.attrDuplWalkCount < 250)
			{
				this.attrDuplWalkCount++;
				for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
				{
					if (Ref.Equal(this.nodes[i].localName, nodeData.localName))
					{
						this.attrDuplWalkCount = 250;
						break;
					}
				}
			}
			this.attrCount++;
			return nodeData;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000301AD File Offset: 0x0002E3AD
		private void PopElementContext()
		{
			this.namespaceManager.PopScope();
			if (this.curNode.xmlContextPushed)
			{
				this.PopXmlContext();
			}
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000301CE File Offset: 0x0002E3CE
		private void OnNewLine(int pos)
		{
			this.ps.lineNo = this.ps.lineNo + 1;
			this.ps.lineStartPos = pos - 1;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000301F0 File Offset: 0x0002E3F0
		private void OnEof()
		{
			this.curNode = this.nodes[0];
			this.curNode.Clear(XmlNodeType.None);
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.Eof;
			this.readState = ReadState.EndOfFile;
			this.reportedEncoding = null;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00030250 File Offset: 0x0002E450
		private string LookupNamespace(XmlTextReaderImpl.NodeData node)
		{
			string text = this.namespaceManager.LookupNamespace(node.prefix);
			if (text != null)
			{
				return text;
			}
			this.Throw("'{0}' is an undeclared prefix.", node.prefix, node.LineNo, node.LinePos);
			return null;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00030294 File Offset: 0x0002E494
		private void AddNamespace(string prefix, string uri, XmlTextReaderImpl.NodeData attr)
		{
			if (uri == "http://www.w3.org/2000/xmlns/")
			{
				if (Ref.Equal(prefix, this.XmlNs))
				{
					this.Throw("Prefix \"xmlns\" is reserved for use by XML.", attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
				}
				else
				{
					this.Throw("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", prefix, attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
				}
			}
			else if (uri == "http://www.w3.org/XML/1998/namespace" && !Ref.Equal(prefix, this.Xml) && !this.v1Compat)
			{
				this.Throw("Prefix '{0}' cannot be mapped to namespace name reserved for \"xml\" or \"xmlns\".", prefix, attr.lineInfo2.lineNo, attr.lineInfo2.linePos);
			}
			if (uri.Length == 0 && prefix.Length > 0)
			{
				this.Throw("Invalid namespace declaration.", attr.lineInfo.lineNo, attr.lineInfo.linePos);
			}
			try
			{
				this.namespaceManager.AddNamespace(prefix, uri);
			}
			catch (ArgumentException e)
			{
				this.ReThrow(e, attr.lineInfo.lineNo, attr.lineInfo.linePos);
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000303BC File Offset: 0x0002E5BC
		private void ResetAttributes()
		{
			if (this.fullAttrCleanup)
			{
				this.FullAttributeCleanup();
			}
			this.curAttrIndex = -1;
			this.attrCount = 0;
			this.attrHashtable = 0;
			this.attrDuplWalkCount = 0;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000303E8 File Offset: 0x0002E5E8
		private void FullAttributeCleanup()
		{
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				XmlTextReaderImpl.NodeData nodeData = this.nodes[i];
				nodeData.nextAttrValueChunk = null;
				nodeData.IsDefaultAttribute = false;
			}
			this.fullAttrCleanup = false;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00030432 File Offset: 0x0002E632
		private void PushXmlContext()
		{
			this.xmlContext = new XmlTextReaderImpl.XmlContext(this.xmlContext);
			this.curNode.xmlContextPushed = true;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00030451 File Offset: 0x0002E651
		private void PopXmlContext()
		{
			this.xmlContext = this.xmlContext.previousContext;
			this.curNode.xmlContextPushed = false;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00030470 File Offset: 0x0002E670
		private XmlNodeType GetWhitespaceType()
		{
			if (this.whitespaceHandling != WhitespaceHandling.None)
			{
				if (this.xmlContext.xmlSpace == XmlSpace.Preserve)
				{
					return XmlNodeType.SignificantWhitespace;
				}
				if (this.whitespaceHandling == WhitespaceHandling.All)
				{
					return XmlNodeType.Whitespace;
				}
			}
			return XmlNodeType.None;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00030498 File Offset: 0x0002E698
		private XmlNodeType GetTextNodeType(int orChars)
		{
			if (orChars > 32)
			{
				return XmlNodeType.Text;
			}
			return this.GetWhitespaceType();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x000304A8 File Offset: 0x0002E6A8
		private void PushExternalEntityOrSubset(string publicId, string systemId, Uri baseUri, string entityName)
		{
			Uri uri;
			if (!string.IsNullOrEmpty(publicId))
			{
				try
				{
					uri = this.xmlResolver.ResolveUri(baseUri, publicId);
					if (this.OpenAndPush(uri))
					{
						return;
					}
				}
				catch (Exception)
				{
				}
			}
			uri = this.xmlResolver.ResolveUri(baseUri, systemId);
			try
			{
				if (this.OpenAndPush(uri))
				{
					return;
				}
			}
			catch (Exception ex)
			{
				if (this.v1Compat)
				{
					throw;
				}
				string message = ex.Message;
				this.Throw(new XmlException((entityName == null) ? "An error has occurred while opening external DTD '{0}': {1}" : "An error has occurred while opening external entity '{0}': {1}", new string[]
				{
					uri.ToString(),
					message
				}, ex, 0, 0));
			}
			if (entityName == null)
			{
				this.ThrowWithoutLineInfo("Cannot resolve external DTD subset - public ID = '{0}', system ID = '{1}'.", new string[]
				{
					(publicId != null) ? publicId : string.Empty,
					systemId
				}, null);
				return;
			}
			this.Throw((this.dtdProcessing == DtdProcessing.Ignore) ? "Cannot resolve entity reference '{0}' because the DTD has been ignored. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method." : "Cannot resolve entity reference '{0}'.", entityName);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x000305A4 File Offset: 0x0002E7A4
		private bool OpenAndPush(Uri uri)
		{
			if (this.xmlResolver.SupportsType(uri, typeof(TextReader)))
			{
				TextReader textReader = (TextReader)this.xmlResolver.GetEntity(uri, null, typeof(TextReader));
				if (textReader == null)
				{
					return false;
				}
				this.PushParsingState();
				this.InitTextReaderInput(uri.ToString(), uri, textReader);
			}
			else
			{
				Stream stream = (Stream)this.xmlResolver.GetEntity(uri, null, typeof(Stream));
				if (stream == null)
				{
					return false;
				}
				this.PushParsingState();
				this.InitStreamInput(uri, stream, null);
			}
			return true;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00030634 File Offset: 0x0002E834
		private bool PushExternalEntity(IDtdEntityInfo entity)
		{
			if (!this.IsResolverNull)
			{
				Uri baseUri = null;
				if (!string.IsNullOrEmpty(entity.BaseUriString))
				{
					baseUri = this.xmlResolver.ResolveUri(null, entity.BaseUriString);
				}
				this.PushExternalEntityOrSubset(entity.PublicId, entity.SystemId, baseUri, entity.Name);
				this.RegisterEntity(entity);
				int charPos = this.ps.charPos;
				if (this.v1Compat)
				{
					this.EatWhitespaces(null);
				}
				if (!this.ParseXmlDeclaration(true))
				{
					this.ps.charPos = charPos;
				}
				return true;
			}
			Encoding encoding = this.ps.encoding;
			this.PushParsingState();
			this.InitStringInput(entity.SystemId, encoding, string.Empty);
			this.RegisterEntity(entity);
			this.RegisterConsumedCharacters(0L, true);
			return false;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000306F4 File Offset: 0x0002E8F4
		private void PushInternalEntity(IDtdEntityInfo entity)
		{
			Encoding encoding = this.ps.encoding;
			this.PushParsingState();
			this.InitStringInput((entity.DeclaredUriString != null) ? entity.DeclaredUriString : string.Empty, encoding, entity.Text ?? string.Empty);
			this.RegisterEntity(entity);
			this.ps.lineNo = entity.LineNumber;
			this.ps.lineStartPos = -entity.LinePosition - 1;
			this.ps.eolNormalized = true;
			this.RegisterConsumedCharacters((long)entity.Text.Length, true);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0003078C File Offset: 0x0002E98C
		private void PopEntity()
		{
			if (this.ps.stream != null)
			{
				this.ps.stream.Close();
			}
			this.UnregisterEntity();
			this.PopParsingState();
			this.curNode.entityId = this.ps.entityId;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x000307D8 File Offset: 0x0002E9D8
		private void RegisterEntity(IDtdEntityInfo entity)
		{
			if (this.currentEntities != null && this.currentEntities.ContainsKey(entity))
			{
				this.Throw(entity.IsParameterEntity ? "Parameter entity '{0}' references itself." : "General entity '{0}' references itself.", entity.Name, this.parsingStatesStack[this.parsingStatesStackTop].LineNo, this.parsingStatesStack[this.parsingStatesStackTop].LinePos);
			}
			this.ps.entity = entity;
			int num = this.nextEntityId;
			this.nextEntityId = num + 1;
			this.ps.entityId = num;
			if (entity != null)
			{
				if (this.currentEntities == null)
				{
					this.currentEntities = new Dictionary<IDtdEntityInfo, IDtdEntityInfo>();
				}
				this.currentEntities.Add(entity, entity);
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00030892 File Offset: 0x0002EA92
		private void UnregisterEntity()
		{
			if (this.ps.entity != null)
			{
				this.currentEntities.Remove(this.ps.entity);
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000308B8 File Offset: 0x0002EAB8
		private void PushParsingState()
		{
			if (this.parsingStatesStack == null)
			{
				this.parsingStatesStack = new XmlTextReaderImpl.ParsingState[2];
			}
			else if (this.parsingStatesStackTop + 1 == this.parsingStatesStack.Length)
			{
				XmlTextReaderImpl.ParsingState[] destinationArray = new XmlTextReaderImpl.ParsingState[this.parsingStatesStack.Length * 2];
				Array.Copy(this.parsingStatesStack, 0, destinationArray, 0, this.parsingStatesStack.Length);
				this.parsingStatesStack = destinationArray;
			}
			this.parsingStatesStackTop++;
			this.parsingStatesStack[this.parsingStatesStackTop] = this.ps;
			this.ps.Clear();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0003094C File Offset: 0x0002EB4C
		private void PopParsingState()
		{
			this.ps.Close(true);
			XmlTextReaderImpl.ParsingState[] array = this.parsingStatesStack;
			int num = this.parsingStatesStackTop;
			this.parsingStatesStackTop = num - 1;
			this.ps = array[num];
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00030988 File Offset: 0x0002EB88
		private void InitIncrementalRead(IncrementalReadDecoder decoder)
		{
			this.ResetAttributes();
			decoder.Reset();
			this.incReadDecoder = decoder;
			this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
			this.incReadDepth = 1;
			this.incReadLeftStartPos = this.ps.charPos;
			this.incReadLeftEndPos = this.ps.charPos;
			this.incReadLineInfo.Set(this.ps.LineNo, this.ps.LinePos);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InIncrementalRead;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00030A04 File Offset: 0x0002EC04
		private int IncrementalRead(Array array, int index, int count)
		{
			if (array == null)
			{
				throw new ArgumentNullException((this.incReadDecoder is IncrementalReadCharsDecoder) ? "buffer" : "array");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException((this.incReadDecoder is IncrementalReadCharsDecoder) ? "count" : "len");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException((this.incReadDecoder is IncrementalReadCharsDecoder) ? "index" : "offset");
			}
			if (array.Length - index < count)
			{
				throw new ArgumentException((this.incReadDecoder is IncrementalReadCharsDecoder) ? "count" : "len");
			}
			if (count == 0)
			{
				return 0;
			}
			this.curNode.lineInfo = this.incReadLineInfo;
			this.incReadDecoder.SetNextOutputBuffer(array, index, count);
			this.IncrementalRead();
			return this.incReadDecoder.DecodedCount;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00030ADC File Offset: 0x0002ECDC
		private int IncrementalRead()
		{
			int num = 0;
			int num3;
			int num4;
			int num5;
			int num7;
			for (;;)
			{
				int num2 = this.incReadLeftEndPos - this.incReadLeftStartPos;
				if (num2 > 0)
				{
					try
					{
						num3 = this.incReadDecoder.Decode(this.ps.chars, this.incReadLeftStartPos, num2);
					}
					catch (XmlException e)
					{
						this.ReThrow(e, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						return 0;
					}
					if (num3 < num2)
					{
						break;
					}
					this.incReadLeftStartPos = 0;
					this.incReadLeftEndPos = 0;
					this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num3;
					if (this.incReadDecoder.IsFull)
					{
						return num3;
					}
				}
				num4 = 0;
				num5 = 0;
				int num10;
				for (;;)
				{
					switch (this.incReadState)
					{
					case XmlTextReaderImpl.IncrementalReadState.Text:
					case XmlTextReaderImpl.IncrementalReadState.StartTag:
					case XmlTextReaderImpl.IncrementalReadState.Attributes:
					case XmlTextReaderImpl.IncrementalReadState.AttributeValue:
						goto IL_1D7;
					case XmlTextReaderImpl.IncrementalReadState.PI:
						if (this.ParsePIValue(out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 2;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.CDATA:
						if (this.ParseCDataOrComment(XmlNodeType.CDATA, out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 3;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.Comment:
						if (this.ParseCDataOrComment(XmlNodeType.Comment, out num4, out num5))
						{
							this.ps.charPos = this.ps.charPos - 3;
							this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						}
						break;
					case XmlTextReaderImpl.IncrementalReadState.ReadData:
						if (this.ReadData() == 0)
						{
							this.ThrowUnclosedElements();
						}
						this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
						num4 = this.ps.charPos;
						num5 = num4;
						goto IL_1D7;
					case XmlTextReaderImpl.IncrementalReadState.EndElement:
						goto IL_17A;
					case XmlTextReaderImpl.IncrementalReadState.End:
						return num;
					default:
						goto IL_1D7;
					}
					IL_6A4:
					int num6 = num5 - num4;
					if (num6 <= 0)
					{
						continue;
					}
					try
					{
						num7 = this.incReadDecoder.Decode(this.ps.chars, num4, num6);
					}
					catch (XmlException e2)
					{
						this.ReThrow(e2, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						return 0;
					}
					num += num7;
					if (this.incReadDecoder.IsFull)
					{
						goto Block_54;
					}
					continue;
					IL_1D7:
					char[] chars = this.ps.chars;
					num4 = this.ps.charPos;
					num5 = num4;
					int num8;
					for (;;)
					{
						this.incReadLineInfo.Set(this.ps.LineNo, this.ps.LinePos);
						if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
						{
							char c;
							while ((this.xmlCharType.charProperties[(int)(c = chars[num5])] & 128) != 0)
							{
								if (c == '/')
								{
									break;
								}
								num5++;
							}
						}
						else
						{
							while ((this.xmlCharType.charProperties[(int)chars[num5]] & 128) != 0)
							{
								num5++;
							}
						}
						if (chars[num5] == '&' || chars[num5] == '\t')
						{
							num5++;
						}
						else
						{
							if (num5 - num4 > 0)
							{
								break;
							}
							char c2 = chars[num5];
							if (c2 <= '"')
							{
								if (c2 == '\n')
								{
									num5++;
									this.OnNewLine(num5);
									continue;
								}
								if (c2 == '\r')
								{
									if (chars[num5 + 1] == '\n')
									{
										num5 += 2;
									}
									else
									{
										if (num5 + 1 >= this.ps.charsUsed)
										{
											goto IL_691;
										}
										num5++;
									}
									this.OnNewLine(num5);
									continue;
								}
								if (c2 != '"')
								{
									goto IL_67A;
								}
							}
							else if (c2 <= '/')
							{
								if (c2 != '\'')
								{
									if (c2 != '/')
									{
										goto IL_67A;
									}
									if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
									{
										if (this.ps.charsUsed - num5 < 2)
										{
											goto IL_691;
										}
										if (chars[num5 + 1] == '>')
										{
											this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
											this.incReadDepth--;
										}
									}
									num5++;
									continue;
								}
							}
							else if (c2 != '<')
							{
								if (c2 != '>')
								{
									goto IL_67A;
								}
								if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.Attributes)
								{
									this.incReadState = XmlTextReaderImpl.IncrementalReadState.Text;
								}
								num5++;
								continue;
							}
							else
							{
								if (this.incReadState != XmlTextReaderImpl.IncrementalReadState.Text)
								{
									num5++;
									continue;
								}
								if (this.ps.charsUsed - num5 < 2)
								{
									goto IL_691;
								}
								char c3 = chars[num5 + 1];
								if (c3 != '!')
								{
									if (c3 != '/')
									{
										if (c3 == '?')
										{
											goto Block_31;
										}
										int num9;
										num8 = this.ParseQName(true, 1, out num9);
										if (XmlConvert.StrEqual(this.ps.chars, this.ps.charPos + 1, num8 - this.ps.charPos - 1, this.curNode.localName) && (this.ps.chars[num8] == '>' || this.ps.chars[num8] == '/' || this.xmlCharType.IsWhiteSpace(this.ps.chars[num8])))
										{
											goto IL_594;
										}
										num5 = num8;
										num4 = this.ps.charPos;
										chars = this.ps.chars;
										continue;
									}
									else
									{
										int num11;
										num10 = this.ParseQName(true, 2, out num11);
										if (!XmlConvert.StrEqual(chars, this.ps.charPos + 2, num10 - this.ps.charPos - 2, this.curNode.GetNameWPrefix(this.nameTable)) || (this.ps.chars[num10] != '>' && !this.xmlCharType.IsWhiteSpace(this.ps.chars[num10])))
										{
											num5 = num10;
											num4 = this.ps.charPos;
											chars = this.ps.chars;
											continue;
										}
										int num12 = this.incReadDepth - 1;
										this.incReadDepth = num12;
										if (num12 > 0)
										{
											num5 = num10 + 1;
											continue;
										}
										goto IL_47C;
									}
								}
								else
								{
									if (this.ps.charsUsed - num5 < 4)
									{
										goto IL_691;
									}
									if (chars[num5 + 2] == '-' && chars[num5 + 3] == '-')
									{
										goto Block_34;
									}
									if (this.ps.charsUsed - num5 < 9)
									{
										goto IL_691;
									}
									if (XmlConvert.StrEqual(chars, num5 + 2, 7, "[CDATA["))
									{
										goto Block_36;
									}
									continue;
								}
							}
							XmlTextReaderImpl.IncrementalReadState incrementalReadState = this.incReadState;
							if (incrementalReadState != XmlTextReaderImpl.IncrementalReadState.Attributes)
							{
								if (incrementalReadState == XmlTextReaderImpl.IncrementalReadState.AttributeValue && chars[num5] == this.curNode.quoteChar)
								{
									this.incReadState = XmlTextReaderImpl.IncrementalReadState.Attributes;
								}
							}
							else
							{
								this.curNode.quoteChar = chars[num5];
								this.incReadState = XmlTextReaderImpl.IncrementalReadState.AttributeValue;
							}
							num5++;
							continue;
							IL_67A:
							if (num5 == this.ps.charsUsed)
							{
								goto IL_691;
							}
							num5++;
						}
					}
					IL_698:
					this.ps.charPos = num5;
					goto IL_6A4;
					IL_691:
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadData;
					goto IL_698;
					IL_594:
					this.incReadDepth++;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.Attributes;
					num5 = num8;
					goto IL_698;
					Block_36:
					num5 += 9;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.CDATA;
					goto IL_698;
					Block_34:
					num5 += 4;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.Comment;
					goto IL_698;
					Block_31:
					num5 += 2;
					this.incReadState = XmlTextReaderImpl.IncrementalReadState.PI;
					goto IL_698;
				}
				IL_47C:
				this.ps.charPos = num10;
				if (this.xmlCharType.IsWhiteSpace(this.ps.chars[num10]))
				{
					this.EatWhitespaces(null);
				}
				if (this.ps.chars[this.ps.charPos] != '>')
				{
					this.ThrowUnexpectedToken(">");
				}
				this.ps.charPos = this.ps.charPos + 1;
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.EndElement;
			}
			this.incReadLeftStartPos += num3;
			this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num3;
			return num3;
			IL_17A:
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopElementContext;
			this.nextParsingFunction = ((this.index > 0 || this.fragmentType != XmlNodeType.Document) ? XmlTextReaderImpl.ParsingFunction.ElementContent : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			this.outerReader.Read();
			this.incReadState = XmlTextReaderImpl.IncrementalReadState.End;
			return num;
			Block_54:
			this.incReadLeftStartPos = num4 + num7;
			this.incReadLeftEndPos = num5;
			this.incReadLineInfo.linePos = this.incReadLineInfo.linePos + num7;
			return num;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00031234 File Offset: 0x0002F434
		private void FinishIncrementalRead()
		{
			this.incReadDecoder = new IncrementalReadDummyDecoder();
			this.IncrementalRead();
			this.incReadDecoder = null;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00031250 File Offset: 0x0002F450
		private bool ParseFragmentAttribute()
		{
			if (this.curNode.type == XmlNodeType.None)
			{
				this.curNode.type = XmlNodeType.Attribute;
				this.curAttrIndex = 0;
				this.ParseAttributeValueSlow(this.ps.charPos, ' ', this.curNode);
			}
			else
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadAttributeValue;
			}
			if (this.ReadAttributeValue())
			{
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.FragmentAttribute;
				return true;
			}
			this.OnEof();
			return false;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000312BC File Offset: 0x0002F4BC
		private bool ParseAttributeValueChunk()
		{
			char[] chars = this.ps.chars;
			int num = this.ps.charPos;
			this.curNode = this.AddNode(this.index + this.attrCount + 1, this.index + 2);
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			if (this.emptyEntityInAttributeResolved)
			{
				this.curNode.SetValueNode(XmlNodeType.Text, string.Empty);
				this.emptyEntityInAttributeResolved = false;
				return true;
			}
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)chars[num]] & 128) == 0)
				{
					char c = chars[num];
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
						case '\n':
							if (this.normalize)
							{
								chars[num] = ' ';
							}
							num++;
							continue;
						case '\v':
						case '\f':
							goto IL_21F;
						case '\r':
							num++;
							continue;
						default:
							if (c != '"')
							{
								if (c != '&')
								{
									goto IL_21F;
								}
								if (num - this.ps.charPos > 0)
								{
									this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
								}
								this.ps.charPos = num;
								XmlTextReaderImpl.EntityType entityType = this.HandleEntityReference(true, XmlTextReaderImpl.EntityExpandType.OnlyCharacter, out num);
								if (entityType > XmlTextReaderImpl.EntityType.CharacterNamed)
								{
									if (entityType == XmlTextReaderImpl.EntityType.Unexpanded)
									{
										goto IL_1C5;
									}
								}
								else
								{
									chars = this.ps.chars;
									if (this.normalize && this.xmlCharType.IsWhiteSpace(chars[this.ps.charPos]) && num - this.ps.charPos == 1)
									{
										chars[this.ps.charPos] = ' ';
									}
								}
								chars = this.ps.chars;
								continue;
							}
							break;
						}
					}
					else if (c != '\'')
					{
						if (c == '<')
						{
							this.Throw(num, "'{0}', hexadecimal value {1}, is an invalid attribute character.", XmlException.BuildCharExceptionArgs('<', '\0'));
							goto IL_271;
						}
						if (c != '>')
						{
							goto IL_21F;
						}
					}
					num++;
					continue;
					IL_21F:
					if (num != this.ps.charsUsed)
					{
						if (XmlCharType.IsHighSurrogate((int)chars[num]))
						{
							if (num + 1 == this.ps.charsUsed)
							{
								goto IL_271;
							}
							num++;
							if (XmlCharType.IsLowSurrogate((int)chars[num]))
							{
								num++;
								continue;
							}
						}
						this.ThrowInvalidChar(chars, this.ps.charsUsed, num);
					}
					IL_271:
					if (num - this.ps.charPos > 0)
					{
						this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
						this.ps.charPos = num;
					}
					if (this.ReadData() == 0)
					{
						if (this.stringBuilder.Length > 0)
						{
							goto IL_2F6;
						}
						if (this.HandleEntityEnd(false))
						{
							goto Block_25;
						}
					}
					num = this.ps.charPos;
					chars = this.ps.chars;
				}
				else
				{
					num++;
				}
			}
			IL_1C5:
			if (this.stringBuilder.Length == 0)
			{
				XmlTextReaderImpl.NodeData nodeData = this.curNode;
				nodeData.lineInfo.linePos = nodeData.lineInfo.linePos + 1;
				this.ps.charPos = this.ps.charPos + 1;
				this.curNode.SetNamedNode(XmlNodeType.EntityReference, this.ParseEntityName());
				return true;
			}
			goto IL_2F6;
			Block_25:
			this.SetupEndEntityNodeInAttribute();
			return true;
			IL_2F6:
			if (num - this.ps.charPos > 0)
			{
				this.stringBuilder.Append(chars, this.ps.charPos, num - this.ps.charPos);
				this.ps.charPos = num;
			}
			this.curNode.SetValueNode(XmlNodeType.Text, this.stringBuilder.ToString());
			this.stringBuilder.Length = 0;
			return true;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00031624 File Offset: 0x0002F824
		private void ParseXmlDeclarationFragment()
		{
			try
			{
				this.ParseXmlDeclaration(false);
			}
			catch (XmlException ex)
			{
				this.ReThrow(ex, ex.LineNumber, ex.LinePosition - 6);
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00031664 File Offset: 0x0002F864
		private void ThrowUnexpectedToken(int pos, string expectedToken)
		{
			this.ThrowUnexpectedToken(pos, expectedToken, null);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0003166F File Offset: 0x0002F86F
		private void ThrowUnexpectedToken(string expectedToken1)
		{
			this.ThrowUnexpectedToken(expectedToken1, null);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00031679 File Offset: 0x0002F879
		private void ThrowUnexpectedToken(int pos, string expectedToken1, string expectedToken2)
		{
			this.ps.charPos = pos;
			this.ThrowUnexpectedToken(expectedToken1, expectedToken2);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00031690 File Offset: 0x0002F890
		private void ThrowUnexpectedToken(string expectedToken1, string expectedToken2)
		{
			string text = this.ParseUnexpectedToken();
			if (text == null)
			{
				this.Throw("Unexpected end of file has occurred.");
			}
			if (expectedToken2 != null)
			{
				this.Throw("'{0}' is an unexpected token. The expected token is '{1}' or '{2}'.", new string[]
				{
					text,
					expectedToken1,
					expectedToken2
				});
				return;
			}
			this.Throw("'{0}' is an unexpected token. The expected token is '{1}'.", new string[]
			{
				text,
				expectedToken1
			});
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x000316EC File Offset: 0x0002F8EC
		private string ParseUnexpectedToken(int pos)
		{
			this.ps.charPos = pos;
			return this.ParseUnexpectedToken();
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00031700 File Offset: 0x0002F900
		private string ParseUnexpectedToken()
		{
			if (this.ps.charPos == this.ps.charsUsed)
			{
				return null;
			}
			if (this.xmlCharType.IsNCNameSingleChar(this.ps.chars[this.ps.charPos]))
			{
				int num = this.ps.charPos + 1;
				while (this.xmlCharType.IsNCNameSingleChar(this.ps.chars[num]))
				{
					num++;
				}
				return new string(this.ps.chars, this.ps.charPos, num - this.ps.charPos);
			}
			return new string(this.ps.chars, this.ps.charPos, 1);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000317C0 File Offset: 0x0002F9C0
		private void ThrowExpectingWhitespace(int pos)
		{
			string text = this.ParseUnexpectedToken(pos);
			if (text == null)
			{
				this.Throw(pos, "Unexpected end of file has occurred.");
				return;
			}
			this.Throw(pos, "'{0}' is an unexpected token. Expecting white space.", text);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x000317F4 File Offset: 0x0002F9F4
		private int GetIndexOfAttributeWithoutPrefix(string name)
		{
			name = this.nameTable.Get(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].localName, name) && this.nodes[i].prefix.Length == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00031864 File Offset: 0x0002FA64
		private int GetIndexOfAttributeWithPrefix(string name)
		{
			name = this.nameTable.Add(name);
			if (name == null)
			{
				return -1;
			}
			for (int i = this.index + 1; i < this.index + this.attrCount + 1; i++)
			{
				if (Ref.Equal(this.nodes[i].GetNameWPrefix(this.nameTable), name))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000318C4 File Offset: 0x0002FAC4
		private bool ZeroEndingStream(int pos)
		{
			if (this.v1Compat && pos == this.ps.charsUsed - 1 && this.ps.chars[pos] == '\0' && this.ReadData() == 0 && this.ps.isStreamEof)
			{
				this.ps.charsUsed = this.ps.charsUsed - 1;
				return true;
			}
			return false;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00031920 File Offset: 0x0002FB20
		private void ParseDtdFromParserContext()
		{
			IDtdParser dtdParser = DtdParser.Create();
			this.dtdInfo = dtdParser.ParseFreeFloatingDtd(this.fragmentParserContext.BaseURI, this.fragmentParserContext.DocTypeName, this.fragmentParserContext.PublicId, this.fragmentParserContext.SystemId, this.fragmentParserContext.InternalSubset, new XmlTextReaderImpl.DtdParserProxy(this));
			if ((this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000319B0 File Offset: 0x0002FBB0
		private bool InitReadContentAsBinary()
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
			{
				throw new InvalidOperationException(Res.GetString("ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex."));
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
			{
				throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadChars, ReadBase64, and ReadBinHex."));
			}
			if (!XmlReader.IsTextualNode(this.curNode.type) && !this.MoveToNextContentNode(false))
			{
				return false;
			}
			this.SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary);
			this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			return true;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00031A38 File Offset: 0x0002FC38
		private bool InitReadElementContentAsBinary()
		{
			bool isEmptyElement = this.curNode.IsEmptyElement;
			this.outerReader.Read();
			if (isEmptyElement)
			{
				return false;
			}
			if (!this.MoveToNextContentNode(false))
			{
				if (this.curNode.type != XmlNodeType.EndElement)
				{
					this.Throw("'{0}' is an invalid XmlNodeType.", this.curNode.type.ToString());
				}
				this.outerReader.Read();
				return false;
			}
			this.SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary);
			this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			return true;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00031AD4 File Offset: 0x0002FCD4
		private bool MoveToNextContentNode(bool moveIfOnContentNode)
		{
			for (;;)
			{
				switch (this.curNode.type)
				{
				case XmlNodeType.Attribute:
					goto IL_52;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					if (!moveIfOnContentNode)
					{
						return true;
					}
					goto IL_6B;
				case XmlNodeType.EntityReference:
					this.outerReader.ResolveEntity();
					goto IL_6B;
				case XmlNodeType.ProcessingInstruction:
				case XmlNodeType.Comment:
				case XmlNodeType.EndEntity:
					goto IL_6B;
				}
				break;
				IL_6B:
				moveIfOnContentNode = false;
				if (!this.outerReader.Read())
				{
					return false;
				}
			}
			return false;
			IL_52:
			return !moveIfOnContentNode;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00031B60 File Offset: 0x0002FD60
		private void SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction inReadBinaryFunction)
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
			{
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue;
			}
			else
			{
				this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue;
				this.nextNextParsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.parsingFunction;
			}
			this.readValueOffset = 0;
			this.parsingFunction = inReadBinaryFunction;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00031BB0 File Offset: 0x0002FDB0
		private void SetupFromParserContext(XmlParserContext context, XmlReaderSettings settings)
		{
			XmlNameTable xmlNameTable = settings.NameTable;
			this.nameTableFromSettings = (xmlNameTable != null);
			if (context.NamespaceManager != null)
			{
				if (xmlNameTable != null && xmlNameTable != context.NamespaceManager.NameTable)
				{
					throw new XmlException("XmlReaderSettings.XmlNameTable must be the same name table as in XmlParserContext.NameTable or XmlParserContext.NamespaceManager.NameTable, or it must be null.");
				}
				this.namespaceManager = context.NamespaceManager;
				this.xmlContext.defaultNamespace = this.namespaceManager.LookupNamespace(string.Empty);
				xmlNameTable = this.namespaceManager.NameTable;
			}
			else if (context.NameTable != null)
			{
				if (xmlNameTable != null && xmlNameTable != context.NameTable)
				{
					throw new XmlException("XmlReaderSettings.XmlNameTable must be the same name table as in XmlParserContext.NameTable or XmlParserContext.NamespaceManager.NameTable, or it must be null.", string.Empty);
				}
				xmlNameTable = context.NameTable;
			}
			else if (xmlNameTable == null)
			{
				xmlNameTable = new NameTable();
			}
			this.nameTable = xmlNameTable;
			if (this.namespaceManager == null)
			{
				this.namespaceManager = new XmlNamespaceManager(xmlNameTable);
			}
			this.xmlContext.xmlSpace = context.XmlSpace;
			this.xmlContext.xmlLang = context.XmlLang;
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00031C9A File Offset: 0x0002FE9A
		internal override IDtdInfo DtdInfo
		{
			get
			{
				return this.dtdInfo;
			}
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00031CA4 File Offset: 0x0002FEA4
		internal void SetDtdInfo(IDtdInfo newDtdInfo)
		{
			this.dtdInfo = newDtdInfo;
			if (this.dtdInfo != null && (this.validatingReaderCompatFlag || !this.v1Compat) && (this.dtdInfo.HasDefaultAttributes || this.dtdInfo.HasNonCDataAttributes))
			{
				this.addDefaultAttributesAndNormalize = true;
			}
		}

		// Token: 0x17000154 RID: 340
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00029455 File Offset: 0x00027655
		internal IValidationEventHandling ValidationEventHandling
		{
			set
			{
				this.validationEventHandling = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00031CF1 File Offset: 0x0002FEF1
		internal XmlTextReaderImpl.OnDefaultAttributeUseDelegate OnDefaultAttributeUse
		{
			set
			{
				this.onDefaultAttributeUse = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x00031CFA File Offset: 0x0002FEFA
		internal bool XmlValidatingReaderCompatibilityMode
		{
			set
			{
				this.validatingReaderCompatFlag = value;
				if (value)
				{
					this.nameTable.Add("http://www.w3.org/2001/XMLSchema");
					this.nameTable.Add("http://www.w3.org/2001/XMLSchema-instance");
					this.nameTable.Add("urn:schemas-microsoft-com:datatypes");
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00031D39 File Offset: 0x0002FF39
		internal XmlNodeType FragmentType
		{
			get
			{
				return this.fragmentType;
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00031D41 File Offset: 0x0002FF41
		internal void ChangeCurrentNodeType(XmlNodeType newNodeType)
		{
			this.curNode.type = newNodeType;
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00031D4F File Offset: 0x0002FF4F
		internal XmlResolver GetResolver()
		{
			if (this.IsResolverNull)
			{
				return null;
			}
			return this.xmlResolver;
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00031D61 File Offset: 0x0002FF61
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00031D6E File Offset: 0x0002FF6E
		internal object InternalSchemaType
		{
			get
			{
				return this.curNode.schemaType;
			}
			set
			{
				this.curNode.schemaType = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00031D7C File Offset: 0x0002FF7C
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00031D89 File Offset: 0x0002FF89
		internal object InternalTypedValue
		{
			get
			{
				return this.curNode.typedValue;
			}
			set
			{
				this.curNode.typedValue = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00031D97 File Offset: 0x0002FF97
		internal bool StandAlone
		{
			get
			{
				return this.standalone;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00029374 File Offset: 0x00027574
		internal override XmlNamespaceManager NamespaceManager
		{
			get
			{
				return this.namespaceManager;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00029384 File Offset: 0x00027584
		internal bool V1Compat
		{
			get
			{
				return this.v1Compat;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00031D9F File Offset: 0x0002FF9F
		internal ConformanceLevel V1ComformanceLevel
		{
			get
			{
				if (this.fragmentType != XmlNodeType.Element)
				{
					return ConformanceLevel.Document;
				}
				return ConformanceLevel.Fragment;
			}
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00031DB0 File Offset: 0x0002FFB0
		private bool AddDefaultAttributeDtd(IDtdDefaultAttributeInfo defAttrInfo, bool definedInDtd, XmlTextReaderImpl.NodeData[] nameSortedNodeData)
		{
			if (defAttrInfo.Prefix.Length > 0)
			{
				this.attrNeedNamespaceLookup = true;
			}
			string localName = defAttrInfo.LocalName;
			string prefix = defAttrInfo.Prefix;
			if (nameSortedNodeData != null)
			{
				if (Array.BinarySearch<object>(nameSortedNodeData, defAttrInfo, XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.Instance) >= 0)
				{
					return false;
				}
			}
			else
			{
				for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
				{
					if (this.nodes[i].localName == localName && this.nodes[i].prefix == prefix)
					{
						return false;
					}
				}
			}
			XmlTextReaderImpl.NodeData nodeData = this.AddDefaultAttributeInternal(defAttrInfo.LocalName, null, defAttrInfo.Prefix, defAttrInfo.DefaultValueExpanded, defAttrInfo.LineNumber, defAttrInfo.LinePosition, defAttrInfo.ValueLineNumber, defAttrInfo.ValueLinePosition, defAttrInfo.IsXmlAttribute);
			if (this.DtdValidation)
			{
				if (this.onDefaultAttributeUse != null)
				{
					this.onDefaultAttributeUse(defAttrInfo, this);
				}
				nodeData.typedValue = defAttrInfo.DefaultValueTyped;
			}
			return nodeData != null;
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00031EA4 File Offset: 0x000300A4
		internal bool AddDefaultAttributeNonDtd(SchemaAttDef attrDef)
		{
			string text = this.nameTable.Add(attrDef.Name.Name);
			string text2 = this.nameTable.Add(attrDef.Prefix);
			string text3 = this.nameTable.Add(attrDef.Name.Namespace);
			if (text2.Length == 0 && text3.Length > 0)
			{
				text2 = this.namespaceManager.LookupPrefix(text3);
				if (text2 == null)
				{
					text2 = string.Empty;
				}
			}
			for (int i = this.index + 1; i < this.index + 1 + this.attrCount; i++)
			{
				if (this.nodes[i].localName == text && (this.nodes[i].prefix == text2 || (this.nodes[i].ns == text3 && text3 != null)))
				{
					return false;
				}
			}
			XmlTextReaderImpl.NodeData nodeData = this.AddDefaultAttributeInternal(text, text3, text2, attrDef.DefaultValueExpanded, attrDef.LineNumber, attrDef.LinePosition, attrDef.ValueLineNumber, attrDef.ValueLinePosition, attrDef.Reserved > SchemaAttDef.Reserve.None);
			nodeData.schemaType = ((attrDef.SchemaType == null) ? attrDef.Datatype : attrDef.SchemaType);
			nodeData.typedValue = attrDef.DefaultValueTyped;
			return true;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x00031FC8 File Offset: 0x000301C8
		private XmlTextReaderImpl.NodeData AddDefaultAttributeInternal(string localName, string ns, string prefix, string value, int lineNo, int linePos, int valueLineNo, int valueLinePos, bool isXmlAttribute)
		{
			XmlTextReaderImpl.NodeData nodeData = this.AddAttribute(localName, prefix, (prefix.Length > 0) ? null : localName);
			if (ns != null)
			{
				nodeData.ns = ns;
			}
			nodeData.SetValue(value);
			nodeData.IsDefaultAttribute = true;
			nodeData.lineInfo.Set(lineNo, linePos);
			nodeData.lineInfo2.Set(valueLineNo, valueLinePos);
			if (nodeData.prefix.Length == 0)
			{
				if (Ref.Equal(nodeData.localName, this.XmlNs))
				{
					this.OnDefaultNamespaceDecl(nodeData);
					if (!this.attrNeedNamespaceLookup && this.nodes[this.index].prefix.Length == 0)
					{
						this.nodes[this.index].ns = this.xmlContext.defaultNamespace;
					}
				}
			}
			else if (Ref.Equal(nodeData.prefix, this.XmlNs))
			{
				this.OnNamespaceDecl(nodeData);
				if (!this.attrNeedNamespaceLookup)
				{
					string localName2 = nodeData.localName;
					for (int i = this.index; i < this.index + this.attrCount + 1; i++)
					{
						if (this.nodes[i].prefix.Equals(localName2))
						{
							this.nodes[i].ns = this.namespaceManager.LookupNamespace(localName2);
						}
					}
				}
			}
			else if (isXmlAttribute)
			{
				this.OnXmlReservedAttribute(nodeData);
			}
			this.fullAttrCleanup = true;
			return nodeData;
		}

		// Token: 0x1700015E RID: 350
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00032120 File Offset: 0x00030320
		internal bool DisableUndeclaredEntityCheck
		{
			set
			{
				this.disableUndeclaredEntityCheck = value;
			}
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x0003212C File Offset: 0x0003032C
		private int ReadContentAsBinary(byte[] buffer, int index, int count)
		{
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End)
			{
				return 0;
			}
			this.incReadDecoder.SetNextOutputBuffer(buffer, index, count);
			int num;
			int num2;
			int num3;
			XmlTextReaderImpl.ParsingFunction inReadBinaryFunction;
			for (;;)
			{
				num = 0;
				try
				{
					num = this.curNode.CopyToBinary(this.incReadDecoder, this.readValueOffset);
				}
				catch (XmlException e)
				{
					this.curNode.AdjustLineInfo(this.readValueOffset, this.ps.eolNormalized, ref this.incReadLineInfo);
					this.ReThrow(e, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
				}
				this.readValueOffset += num;
				if (this.incReadDecoder.IsFull)
				{
					break;
				}
				if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue)
				{
					this.curNode.SetValue(string.Empty);
					bool flag = false;
					num2 = 0;
					num3 = 0;
					while (!this.incReadDecoder.IsFull && !flag)
					{
						int num4 = 0;
						this.incReadLineInfo.Set(this.ps.LineNo, this.ps.LinePos);
						flag = this.ParseText(out num2, out num3, ref num4);
						try
						{
							num = this.incReadDecoder.Decode(this.ps.chars, num2, num3 - num2);
						}
						catch (XmlException e2)
						{
							this.ReThrow(e2, this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
						}
						num2 += num;
					}
					this.incReadState = (flag ? XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue : XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue);
					this.readValueOffset = 0;
					if (this.incReadDecoder.IsFull)
					{
						goto Block_8;
					}
				}
				inReadBinaryFunction = this.parsingFunction;
				this.parsingFunction = this.nextParsingFunction;
				this.nextParsingFunction = this.nextNextParsingFunction;
				if (!this.MoveToNextContentNode(true))
				{
					goto Block_9;
				}
				this.SetupReadContentAsBinaryState(inReadBinaryFunction);
				this.incReadLineInfo.Set(this.curNode.LineNo, this.curNode.LinePos);
			}
			return this.incReadDecoder.DecodedCount;
			Block_8:
			this.curNode.SetValue(this.ps.chars, num2, num3 - num2);
			XmlTextReaderImpl.AdjustLineInfo(this.ps.chars, num2 - num, num2, this.ps.eolNormalized, ref this.incReadLineInfo);
			this.curNode.SetLineInfo(this.incReadLineInfo.lineNo, this.incReadLineInfo.linePos);
			return this.incReadDecoder.DecodedCount;
			Block_9:
			this.SetupReadContentAsBinaryState(inReadBinaryFunction);
			this.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End;
			return this.incReadDecoder.DecodedCount;
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000323AC File Offset: 0x000305AC
		private int ReadElementContentAsBinary(byte[] buffer, int index, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = this.ReadContentAsBinary(buffer, index, count);
			if (num > 0)
			{
				return num;
			}
			if (this.curNode.type != XmlNodeType.EndElement)
			{
				throw new XmlException("'{0}' is an invalid XmlNodeType.", this.curNode.type.ToString(), this);
			}
			this.parsingFunction = this.nextParsingFunction;
			this.nextParsingFunction = this.nextNextParsingFunction;
			this.outerReader.Read();
			return 0;
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x00032424 File Offset: 0x00030624
		private void InitBase64Decoder()
		{
			if (this.base64Decoder == null)
			{
				this.base64Decoder = new Base64Decoder();
			}
			else
			{
				this.base64Decoder.Reset();
			}
			this.incReadDecoder = this.base64Decoder;
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x00032452 File Offset: 0x00030652
		private void InitBinHexDecoder()
		{
			if (this.binHexDecoder == null)
			{
				this.binHexDecoder = new BinHexDecoder();
			}
			else
			{
				this.binHexDecoder.Reset();
			}
			this.incReadDecoder = this.binHexDecoder;
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x00032480 File Offset: 0x00030680
		private bool UriEqual(Uri uri1, string uri1Str, string uri2Str, XmlResolver resolver)
		{
			if (resolver == null)
			{
				return uri1Str == uri2Str;
			}
			if (uri1 == null)
			{
				uri1 = resolver.ResolveUri(null, uri1Str);
			}
			Uri obj = resolver.ResolveUri(null, uri2Str);
			return uri1.Equals(obj);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x000324C0 File Offset: 0x000306C0
		private void RegisterConsumedCharacters(long characters, bool inEntityReference)
		{
			if (this.maxCharactersInDocument > 0L)
			{
				long num = this.charactersInDocument + characters;
				if (num < this.charactersInDocument)
				{
					this.ThrowWithoutLineInfo("The input document has exceeded a limit set by {0}.", "MaxCharactersInDocument");
				}
				else
				{
					this.charactersInDocument = num;
				}
				if (this.charactersInDocument > this.maxCharactersInDocument)
				{
					this.ThrowWithoutLineInfo("The input document has exceeded a limit set by {0}.", "MaxCharactersInDocument");
				}
			}
			if (this.maxCharactersFromEntities > 0L && inEntityReference)
			{
				long num2 = this.charactersFromEntities + characters;
				if (num2 < this.charactersFromEntities)
				{
					this.ThrowWithoutLineInfo("The input document has exceeded a limit set by {0}.", "MaxCharactersFromEntities");
				}
				else
				{
					this.charactersFromEntities = num2;
				}
				if (this.charactersFromEntities > this.maxCharactersFromEntities)
				{
					this.ThrowWithoutLineInfo("The input document has exceeded a limit set by {0}.", "MaxCharactersFromEntities");
				}
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00032578 File Offset: 0x00030778
		internal unsafe static void AdjustLineInfo(char[] chars, int startPos, int endPos, bool isNormalized, ref LineInfo lineInfo)
		{
			fixed (char* ptr = &chars[startPos])
			{
				XmlTextReaderImpl.AdjustLineInfo(ptr, endPos - startPos, isNormalized, ref lineInfo);
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000325A0 File Offset: 0x000307A0
		internal unsafe static void AdjustLineInfo(string str, int startPos, int endPos, bool isNormalized, ref LineInfo lineInfo)
		{
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				XmlTextReaderImpl.AdjustLineInfo(ptr + startPos, endPos - startPos, isNormalized, ref lineInfo);
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x000325D0 File Offset: 0x000307D0
		internal unsafe static void AdjustLineInfo(char* pChars, int length, bool isNormalized, ref LineInfo lineInfo)
		{
			int num = -1;
			for (int i = 0; i < length; i++)
			{
				char c = pChars[i];
				if (c != '\n')
				{
					if (c == '\r')
					{
						if (!isNormalized)
						{
							lineInfo.lineNo++;
							num = i;
							if (i + 1 < length && pChars[i + 1] == '\n')
							{
								i++;
								num++;
							}
						}
					}
				}
				else
				{
					lineInfo.lineNo++;
					num = i;
				}
			}
			if (num >= 0)
			{
				lineInfo.linePos = length - num;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00032648 File Offset: 0x00030848
		internal static string StripSpaces(string value)
		{
			int length = value.Length;
			if (length <= 0)
			{
				return string.Empty;
			}
			int num = 0;
			StringBuilder stringBuilder = null;
			while (value[num] == ' ')
			{
				num++;
				if (num == length)
				{
					return " ";
				}
			}
			int i;
			for (i = num; i < length; i++)
			{
				if (value[i] == ' ')
				{
					int num2 = i + 1;
					while (num2 < length && value[num2] == ' ')
					{
						num2++;
					}
					if (num2 == length)
					{
						if (stringBuilder == null)
						{
							return value.Substring(num, i - num);
						}
						stringBuilder.Append(value, num, i - num);
						return stringBuilder.ToString();
					}
					else if (num2 > i + 1)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(length);
						}
						stringBuilder.Append(value, num, i - num + 1);
						num = num2;
						i = num2 - 1;
					}
				}
			}
			if (stringBuilder != null)
			{
				if (i > num)
				{
					stringBuilder.Append(value, num, i - num);
				}
				return stringBuilder.ToString();
			}
			if (num != 0)
			{
				return value.Substring(num, length - num);
			}
			return value;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x00032730 File Offset: 0x00030930
		internal static void StripSpaces(char[] value, int index, ref int len)
		{
			if (len <= 0)
			{
				return;
			}
			int num = index;
			int num2 = index + len;
			while (value[num] == ' ')
			{
				num++;
				if (num == num2)
				{
					len = 1;
					return;
				}
			}
			int num3 = num - index;
			for (int i = num; i < num2; i++)
			{
				char c;
				if ((c = value[i]) == ' ')
				{
					int num4 = i + 1;
					while (num4 < num2 && value[num4] == ' ')
					{
						num4++;
					}
					if (num4 == num2)
					{
						num3 += num4 - i;
						break;
					}
					if (num4 > i + 1)
					{
						num3 += num4 - i - 1;
						i = num4 - 1;
					}
				}
				value[i - num3] = c;
			}
			len -= num3;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x000327C3 File Offset: 0x000309C3
		internal static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x000327D6 File Offset: 0x000309D6
		internal static void BlockCopy(byte[] src, int srcOffset, byte[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000327E3 File Offset: 0x000309E3
		private void CheckAsyncCall()
		{
			if (!this.useAsync)
			{
				throw new InvalidOperationException(Res.GetString("Set XmlReaderSettings.Async to true if you want to use Async Methods."));
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000327FD File Offset: 0x000309FD
		public override Task<string> GetValueAsync()
		{
			this.CheckAsyncCall();
			if (this.parsingFunction >= XmlTextReaderImpl.ParsingFunction.PartialTextValue)
			{
				return this._GetValueAsync();
			}
			return Task.FromResult<string>(this.curNode.StringValue);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00032828 File Offset: 0x00030A28
		private Task<string> _GetValueAsync()
		{
			XmlTextReaderImpl.<_GetValueAsync>d__473 <_GetValueAsync>d__;
			<_GetValueAsync>d__.<>4__this = this;
			<_GetValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<_GetValueAsync>d__.<>1__state = -1;
			<_GetValueAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<_GetValueAsync>d__473>(ref <_GetValueAsync>d__);
			return <_GetValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0003286C File Offset: 0x00030A6C
		private Task FinishInitAsync()
		{
			switch (this.laterInitParam.initType)
			{
			case XmlTextReaderImpl.InitInputType.UriString:
				return this.FinishInitUriStringAsync();
			case XmlTextReaderImpl.InitInputType.Stream:
				return this.FinishInitStreamAsync();
			case XmlTextReaderImpl.InitInputType.TextReader:
				return this.FinishInitTextReaderAsync();
			default:
				return AsyncHelper.DoneTask;
			}
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000328B4 File Offset: 0x00030AB4
		private Task FinishInitUriStringAsync()
		{
			XmlTextReaderImpl.<FinishInitUriStringAsync>d__475 <FinishInitUriStringAsync>d__;
			<FinishInitUriStringAsync>d__.<>4__this = this;
			<FinishInitUriStringAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishInitUriStringAsync>d__.<>1__state = -1;
			<FinishInitUriStringAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishInitUriStringAsync>d__475>(ref <FinishInitUriStringAsync>d__);
			return <FinishInitUriStringAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000328F8 File Offset: 0x00030AF8
		private Task FinishInitStreamAsync()
		{
			XmlTextReaderImpl.<FinishInitStreamAsync>d__476 <FinishInitStreamAsync>d__;
			<FinishInitStreamAsync>d__.<>4__this = this;
			<FinishInitStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishInitStreamAsync>d__.<>1__state = -1;
			<FinishInitStreamAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishInitStreamAsync>d__476>(ref <FinishInitStreamAsync>d__);
			return <FinishInitStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0003293C File Offset: 0x00030B3C
		private Task FinishInitTextReaderAsync()
		{
			XmlTextReaderImpl.<FinishInitTextReaderAsync>d__477 <FinishInitTextReaderAsync>d__;
			<FinishInitTextReaderAsync>d__.<>4__this = this;
			<FinishInitTextReaderAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishInitTextReaderAsync>d__.<>1__state = -1;
			<FinishInitTextReaderAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishInitTextReaderAsync>d__477>(ref <FinishInitTextReaderAsync>d__);
			return <FinishInitTextReaderAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00032980 File Offset: 0x00030B80
		public override Task<bool> ReadAsync()
		{
			this.CheckAsyncCall();
			if (this.laterInitParam != null)
			{
				return this.FinishInitAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			}
			for (;;)
			{
				switch (this.parsingFunction)
				{
				case XmlTextReaderImpl.ParsingFunction.ElementContent:
					goto IL_9E;
				case XmlTextReaderImpl.ParsingFunction.NoData:
					goto IL_2DC;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractive:
					this.readState = ReadState.Interactive;
					this.parsingFunction = this.nextParsingFunction;
					break;
				case XmlTextReaderImpl.ParsingFunction.SwitchToInteractiveXmlDecl:
					goto IL_C4;
				case XmlTextReaderImpl.ParsingFunction.DocumentContent:
					goto IL_A5;
				case XmlTextReaderImpl.ParsingFunction.MoveToElementContent:
					this.ResetAttributes();
					this.index++;
					this.curNode = this.AddNode(this.index, this.index);
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.ElementContent;
					break;
				case XmlTextReaderImpl.ParsingFunction.PopElementContext:
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					break;
				case XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext:
					this.curNode = this.nodes[this.index];
					this.curNode.IsEmptyElement = false;
					this.ResetAttributes();
					this.PopElementContext();
					this.parsingFunction = this.nextParsingFunction;
					break;
				case XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel:
					this.ResetAttributes();
					this.curNode = this.nodes[this.index];
					this.parsingFunction = ((this.index == 0) ? XmlTextReaderImpl.ParsingFunction.DocumentContent : XmlTextReaderImpl.ParsingFunction.ElementContent);
					break;
				case XmlTextReaderImpl.ParsingFunction.Error:
				case XmlTextReaderImpl.ParsingFunction.Eof:
				case XmlTextReaderImpl.ParsingFunction.ReaderClosed:
					goto IL_2D6;
				case XmlTextReaderImpl.ParsingFunction.EntityReference:
					goto IL_186;
				case XmlTextReaderImpl.ParsingFunction.InIncrementalRead:
					goto IL_29E;
				case XmlTextReaderImpl.ParsingFunction.FragmentAttribute:
					goto IL_2AA;
				case XmlTextReaderImpl.ParsingFunction.ReportEndEntity:
					goto IL_19F;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEntityInContent:
					this.curNode = this.AddNode(this.index, this.index);
					this.reportedEncoding = this.ps.encoding;
					this.reportedBaseUri = this.ps.baseUriStr;
					this.parsingFunction = this.nextParsingFunction;
					break;
				case XmlTextReaderImpl.ParsingFunction.AfterResolveEmptyEntityInContent:
					goto IL_202;
				case XmlTextReaderImpl.ParsingFunction.XmlDeclarationFragment:
					goto IL_2B6;
				case XmlTextReaderImpl.ParsingFunction.GoToEof:
					goto IL_2CA;
				case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
					goto IL_2ED;
				case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
					this.FinishAttributeValueIterator();
					this.curNode = this.nodes[this.index];
					break;
				case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
					goto IL_306;
				case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
					goto IL_31F;
				case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
					goto IL_338;
				}
			}
			IL_9E:
			return this.ParseElementContentAsync();
			IL_A5:
			return this.ParseDocumentContentAsync();
			IL_C4:
			return this.ReadAsync_SwitchToInteractiveXmlDecl();
			IL_186:
			this.parsingFunction = this.nextParsingFunction;
			return this.ParseEntityReferenceAsync().ReturnTaskBoolWhenFinish(true);
			IL_19F:
			this.SetupEndEntityNodeInContent();
			this.parsingFunction = this.nextParsingFunction;
			return AsyncHelper.DoneTaskTrue;
			IL_202:
			this.curNode = this.AddNode(this.index, this.index);
			this.curNode.SetValueNode(XmlNodeType.Text, string.Empty);
			this.curNode.SetLineInfo(this.ps.lineNo, this.ps.LinePos);
			this.reportedEncoding = this.ps.encoding;
			this.reportedBaseUri = this.ps.baseUriStr;
			this.parsingFunction = this.nextParsingFunction;
			return AsyncHelper.DoneTaskTrue;
			IL_29E:
			this.FinishIncrementalRead();
			return AsyncHelper.DoneTaskTrue;
			IL_2AA:
			return Task.FromResult<bool>(this.ParseFragmentAttribute());
			IL_2B6:
			this.ParseXmlDeclarationFragment();
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.GoToEof;
			return AsyncHelper.DoneTaskTrue;
			IL_2CA:
			this.OnEof();
			return AsyncHelper.DoneTaskFalse;
			IL_2D6:
			return AsyncHelper.DoneTaskFalse;
			IL_2DC:
			this.ThrowWithoutLineInfo("Root element is missing.");
			return AsyncHelper.DoneTaskFalse;
			IL_2ED:
			return this.SkipPartialTextValueAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			IL_306:
			return this.FinishReadValueChunkAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			IL_31F:
			return this.FinishReadContentAsBinaryAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
			IL_338:
			return this.FinishReadElementContentAsBinaryAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ReadAsync));
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00032CE0 File Offset: 0x00030EE0
		private Task<bool> ReadAsync_SwitchToInteractiveXmlDecl()
		{
			this.readState = ReadState.Interactive;
			this.parsingFunction = this.nextParsingFunction;
			Task<bool> task = this.ParseXmlDeclarationAsync(false);
			if (task.IsSuccess())
			{
				return this.ReadAsync_SwitchToInteractiveXmlDecl_Helper(task.Result);
			}
			return this._ReadAsync_SwitchToInteractiveXmlDecl(task);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00032D24 File Offset: 0x00030F24
		private Task<bool> _ReadAsync_SwitchToInteractiveXmlDecl(Task<bool> task)
		{
			XmlTextReaderImpl.<_ReadAsync_SwitchToInteractiveXmlDecl>d__480 <_ReadAsync_SwitchToInteractiveXmlDecl>d__;
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>4__this = this;
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.task = task;
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>1__state = -1;
			<_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>t__builder.Start<XmlTextReaderImpl.<_ReadAsync_SwitchToInteractiveXmlDecl>d__480>(ref <_ReadAsync_SwitchToInteractiveXmlDecl>d__);
			return <_ReadAsync_SwitchToInteractiveXmlDecl>d__.<>t__builder.Task;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00032D6F File Offset: 0x00030F6F
		private Task<bool> ReadAsync_SwitchToInteractiveXmlDecl_Helper(bool finish)
		{
			if (finish)
			{
				this.reportedEncoding = this.ps.encoding;
				return AsyncHelper.DoneTaskTrue;
			}
			this.reportedEncoding = this.ps.encoding;
			return this.ReadAsync();
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00032DA4 File Offset: 0x00030FA4
		public override Task SkipAsync()
		{
			XmlTextReaderImpl.<SkipAsync>d__482 <SkipAsync>d__;
			<SkipAsync>d__.<>4__this = this;
			<SkipAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipAsync>d__.<>1__state = -1;
			<SkipAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<SkipAsync>d__482>(ref <SkipAsync>d__);
			return <SkipAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00032DE8 File Offset: 0x00030FE8
		private Task<int> ReadContentAsBase64_AsyncHelper(Task<bool> task, byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadContentAsBase64_AsyncHelper>d__483 <ReadContentAsBase64_AsyncHelper>d__;
			<ReadContentAsBase64_AsyncHelper>d__.<>4__this = this;
			<ReadContentAsBase64_AsyncHelper>d__.task = task;
			<ReadContentAsBase64_AsyncHelper>d__.buffer = buffer;
			<ReadContentAsBase64_AsyncHelper>d__.index = index;
			<ReadContentAsBase64_AsyncHelper>d__.count = count;
			<ReadContentAsBase64_AsyncHelper>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBase64_AsyncHelper>d__.<>1__state = -1;
			<ReadContentAsBase64_AsyncHelper>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadContentAsBase64_AsyncHelper>d__483>(ref <ReadContentAsBase64_AsyncHelper>d__);
			return <ReadContentAsBase64_AsyncHelper>d__.<>t__builder.Task;
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00032E4C File Offset: 0x0003104C
		public override Task<int> ReadContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadContentAsBinaryAsync(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return AsyncHelper.DoneTaskZero;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (!XmlReader.CanReadContentAs(this.curNode.type))
				{
					throw base.CreateReadContentAsException("ReadContentAsBase64");
				}
				Task<bool> task = this.InitReadContentAsBinaryAsync();
				if (!task.IsSuccess())
				{
					return this.ReadContentAsBase64_AsyncHelper(task, buffer, index, count);
				}
				if (!task.Result)
				{
					return AsyncHelper.DoneTaskZero;
				}
			}
			this.InitBase64Decoder();
			return this.ReadContentAsBinaryAsync(buffer, index, count);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00032F40 File Offset: 0x00031140
		public override Task<int> ReadContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadContentAsBinHexAsync>d__485 <ReadContentAsBinHexAsync>d__;
			<ReadContentAsBinHexAsync>d__.<>4__this = this;
			<ReadContentAsBinHexAsync>d__.buffer = buffer;
			<ReadContentAsBinHexAsync>d__.index = index;
			<ReadContentAsBinHexAsync>d__.count = count;
			<ReadContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadContentAsBinHexAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadContentAsBinHexAsync>d__485>(ref <ReadContentAsBinHexAsync>d__);
			return <ReadContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00032F9C File Offset: 0x0003119C
		private Task<int> ReadElementContentAsBase64Async_Helper(Task<bool> task, byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadElementContentAsBase64Async_Helper>d__486 <ReadElementContentAsBase64Async_Helper>d__;
			<ReadElementContentAsBase64Async_Helper>d__.<>4__this = this;
			<ReadElementContentAsBase64Async_Helper>d__.task = task;
			<ReadElementContentAsBase64Async_Helper>d__.buffer = buffer;
			<ReadElementContentAsBase64Async_Helper>d__.index = index;
			<ReadElementContentAsBase64Async_Helper>d__.count = count;
			<ReadElementContentAsBase64Async_Helper>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBase64Async_Helper>d__.<>1__state = -1;
			<ReadElementContentAsBase64Async_Helper>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadElementContentAsBase64Async_Helper>d__486>(ref <ReadElementContentAsBase64Async_Helper>d__);
			return <ReadElementContentAsBase64Async_Helper>d__.<>t__builder.Task;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00033000 File Offset: 0x00031200
		public override Task<int> ReadElementContentAsBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
			{
				if (this.incReadDecoder == this.base64Decoder)
				{
					return this.ReadElementContentAsBinaryAsync(buffer, index, count);
				}
			}
			else
			{
				if (this.readState != ReadState.Interactive)
				{
					return AsyncHelper.DoneTaskZero;
				}
				if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
				{
					throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
				}
				if (this.curNode.type != XmlNodeType.Element)
				{
					throw base.CreateReadElementContentAsException("ReadElementContentAsBinHex");
				}
				Task<bool> task = this.InitReadElementContentAsBinaryAsync();
				if (!task.IsSuccess())
				{
					return this.ReadElementContentAsBase64Async_Helper(task, buffer, index, count);
				}
				if (!task.Result)
				{
					return AsyncHelper.DoneTaskZero;
				}
			}
			this.InitBase64Decoder();
			return this.ReadElementContentAsBinaryAsync(buffer, index, count);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000330F0 File Offset: 0x000312F0
		public override Task<int> ReadElementContentAsBinHexAsync(byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadElementContentAsBinHexAsync>d__488 <ReadElementContentAsBinHexAsync>d__;
			<ReadElementContentAsBinHexAsync>d__.<>4__this = this;
			<ReadElementContentAsBinHexAsync>d__.buffer = buffer;
			<ReadElementContentAsBinHexAsync>d__.index = index;
			<ReadElementContentAsBinHexAsync>d__.count = count;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinHexAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinHexAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadElementContentAsBinHexAsync>d__488>(ref <ReadElementContentAsBinHexAsync>d__);
			return <ReadElementContentAsBinHexAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0003314C File Offset: 0x0003134C
		public override Task<int> ReadValueChunkAsync(char[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadValueChunkAsync>d__489 <ReadValueChunkAsync>d__;
			<ReadValueChunkAsync>d__.<>4__this = this;
			<ReadValueChunkAsync>d__.buffer = buffer;
			<ReadValueChunkAsync>d__.index = index;
			<ReadValueChunkAsync>d__.count = count;
			<ReadValueChunkAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadValueChunkAsync>d__.<>1__state = -1;
			<ReadValueChunkAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadValueChunkAsync>d__489>(ref <ReadValueChunkAsync>d__);
			return <ReadValueChunkAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000331A7 File Offset: 0x000313A7
		internal Task<int> DtdParserProxy_ReadDataAsync()
		{
			this.CheckAsyncCall();
			return this.ReadDataAsync();
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000331B8 File Offset: 0x000313B8
		internal Task<int> DtdParserProxy_ParseNumericCharRefAsync(StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.<DtdParserProxy_ParseNumericCharRefAsync>d__491 <DtdParserProxy_ParseNumericCharRefAsync>d__;
			<DtdParserProxy_ParseNumericCharRefAsync>d__.<>4__this = this;
			<DtdParserProxy_ParseNumericCharRefAsync>d__.internalSubsetBuilder = internalSubsetBuilder;
			<DtdParserProxy_ParseNumericCharRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<DtdParserProxy_ParseNumericCharRefAsync>d__.<>1__state = -1;
			<DtdParserProxy_ParseNumericCharRefAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_ParseNumericCharRefAsync>d__491>(ref <DtdParserProxy_ParseNumericCharRefAsync>d__);
			return <DtdParserProxy_ParseNumericCharRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00033203 File Offset: 0x00031403
		internal Task<int> DtdParserProxy_ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder)
		{
			this.CheckAsyncCall();
			return this.ParseNamedCharRefAsync(expand, internalSubsetBuilder);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00033214 File Offset: 0x00031414
		internal Task DtdParserProxy_ParsePIAsync(StringBuilder sb)
		{
			XmlTextReaderImpl.<DtdParserProxy_ParsePIAsync>d__493 <DtdParserProxy_ParsePIAsync>d__;
			<DtdParserProxy_ParsePIAsync>d__.<>4__this = this;
			<DtdParserProxy_ParsePIAsync>d__.sb = sb;
			<DtdParserProxy_ParsePIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DtdParserProxy_ParsePIAsync>d__.<>1__state = -1;
			<DtdParserProxy_ParsePIAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_ParsePIAsync>d__493>(ref <DtdParserProxy_ParsePIAsync>d__);
			return <DtdParserProxy_ParsePIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x00033260 File Offset: 0x00031460
		internal Task DtdParserProxy_ParseCommentAsync(StringBuilder sb)
		{
			XmlTextReaderImpl.<DtdParserProxy_ParseCommentAsync>d__494 <DtdParserProxy_ParseCommentAsync>d__;
			<DtdParserProxy_ParseCommentAsync>d__.<>4__this = this;
			<DtdParserProxy_ParseCommentAsync>d__.sb = sb;
			<DtdParserProxy_ParseCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DtdParserProxy_ParseCommentAsync>d__.<>1__state = -1;
			<DtdParserProxy_ParseCommentAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_ParseCommentAsync>d__494>(ref <DtdParserProxy_ParseCommentAsync>d__);
			return <DtdParserProxy_ParseCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x000332AC File Offset: 0x000314AC
		internal Task<Tuple<int, bool>> DtdParserProxy_PushEntityAsync(IDtdEntityInfo entity)
		{
			XmlTextReaderImpl.<DtdParserProxy_PushEntityAsync>d__495 <DtdParserProxy_PushEntityAsync>d__;
			<DtdParserProxy_PushEntityAsync>d__.<>4__this = this;
			<DtdParserProxy_PushEntityAsync>d__.entity = entity;
			<DtdParserProxy_PushEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, bool>>.Create();
			<DtdParserProxy_PushEntityAsync>d__.<>1__state = -1;
			<DtdParserProxy_PushEntityAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_PushEntityAsync>d__495>(ref <DtdParserProxy_PushEntityAsync>d__);
			return <DtdParserProxy_PushEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x000332F8 File Offset: 0x000314F8
		internal Task<bool> DtdParserProxy_PushExternalSubsetAsync(string systemId, string publicId)
		{
			XmlTextReaderImpl.<DtdParserProxy_PushExternalSubsetAsync>d__496 <DtdParserProxy_PushExternalSubsetAsync>d__;
			<DtdParserProxy_PushExternalSubsetAsync>d__.<>4__this = this;
			<DtdParserProxy_PushExternalSubsetAsync>d__.systemId = systemId;
			<DtdParserProxy_PushExternalSubsetAsync>d__.publicId = publicId;
			<DtdParserProxy_PushExternalSubsetAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<DtdParserProxy_PushExternalSubsetAsync>d__.<>1__state = -1;
			<DtdParserProxy_PushExternalSubsetAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<DtdParserProxy_PushExternalSubsetAsync>d__496>(ref <DtdParserProxy_PushExternalSubsetAsync>d__);
			return <DtdParserProxy_PushExternalSubsetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0003334B File Offset: 0x0003154B
		private Task InitStreamInputAsync(Uri baseUri, Stream stream, Encoding encoding)
		{
			return this.InitStreamInputAsync(baseUri, baseUri.ToString(), stream, null, 0, encoding);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0003335E File Offset: 0x0003155E
		private Task InitStreamInputAsync(Uri baseUri, string baseUriStr, Stream stream, Encoding encoding)
		{
			return this.InitStreamInputAsync(baseUri, baseUriStr, stream, null, 0, encoding);
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00033370 File Offset: 0x00031570
		private Task InitStreamInputAsync(Uri baseUri, string baseUriStr, Stream stream, byte[] bytes, int byteCount, Encoding encoding)
		{
			XmlTextReaderImpl.<InitStreamInputAsync>d__499 <InitStreamInputAsync>d__;
			<InitStreamInputAsync>d__.<>4__this = this;
			<InitStreamInputAsync>d__.baseUri = baseUri;
			<InitStreamInputAsync>d__.baseUriStr = baseUriStr;
			<InitStreamInputAsync>d__.stream = stream;
			<InitStreamInputAsync>d__.bytes = bytes;
			<InitStreamInputAsync>d__.byteCount = byteCount;
			<InitStreamInputAsync>d__.encoding = encoding;
			<InitStreamInputAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitStreamInputAsync>d__.<>1__state = -1;
			<InitStreamInputAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<InitStreamInputAsync>d__499>(ref <InitStreamInputAsync>d__);
			return <InitStreamInputAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x000333E6 File Offset: 0x000315E6
		private Task InitTextReaderInputAsync(string baseUriStr, TextReader input)
		{
			return this.InitTextReaderInputAsync(baseUriStr, null, input);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000333F4 File Offset: 0x000315F4
		private Task InitTextReaderInputAsync(string baseUriStr, Uri baseUri, TextReader input)
		{
			this.ps.textReader = input;
			this.ps.baseUriStr = baseUriStr;
			this.ps.baseUri = baseUri;
			if (this.ps.chars == null)
			{
				int num;
				if (this.laterInitParam != null && this.laterInitParam.useAsync)
				{
					num = 65536;
				}
				else
				{
					num = 4096;
				}
				this.ps.chars = new char[num + 1];
			}
			this.ps.encoding = Encoding.Unicode;
			this.ps.eolNormalized = !this.normalize;
			this.ps.appendMode = true;
			return this.ReadDataAsync();
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x000334A0 File Offset: 0x000316A0
		private Task ProcessDtdFromParserContextAsync(XmlParserContext context)
		{
			switch (this.dtdProcessing)
			{
			case DtdProcessing.Prohibit:
				this.ThrowWithoutLineInfo("For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.");
				break;
			case DtdProcessing.Parse:
				return this.ParseDtdFromParserContextAsync();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x000334E4 File Offset: 0x000316E4
		private Task SwitchEncodingAsync(Encoding newEncoding)
		{
			if ((newEncoding.WebName != this.ps.encoding.WebName || this.ps.decoder is SafeAsciiDecoder) && !this.afterResetState)
			{
				this.UnDecodeChars();
				this.ps.appendMode = false;
				this.SetupEncoding(newEncoding);
				return this.ReadDataAsync();
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0003354D File Offset: 0x0003174D
		private Task SwitchEncodingToUTF8Async()
		{
			return this.SwitchEncodingAsync(new UTF8Encoding(true, true));
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0003355C File Offset: 0x0003175C
		private Task<int> ReadDataAsync()
		{
			XmlTextReaderImpl.<ReadDataAsync>d__505 <ReadDataAsync>d__;
			<ReadDataAsync>d__.<>4__this = this;
			<ReadDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadDataAsync>d__.<>1__state = -1;
			<ReadDataAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadDataAsync>d__505>(ref <ReadDataAsync>d__);
			return <ReadDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x000335A0 File Offset: 0x000317A0
		private Task<bool> ParseXmlDeclarationAsync(bool isTextDecl)
		{
			XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506 <ParseXmlDeclarationAsync>d__;
			<ParseXmlDeclarationAsync>d__.<>4__this = this;
			<ParseXmlDeclarationAsync>d__.isTextDecl = isTextDecl;
			<ParseXmlDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseXmlDeclarationAsync>d__.<>1__state = -1;
			<ParseXmlDeclarationAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref <ParseXmlDeclarationAsync>d__);
			return <ParseXmlDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x000335EC File Offset: 0x000317EC
		private Task<bool> ParseDocumentContentAsync()
		{
			bool needMoreChars;
			int num;
			char[] chars;
			char c;
			for (;;)
			{
				needMoreChars = false;
				num = this.ps.charPos;
				chars = this.ps.chars;
				if (chars[num] != '<')
				{
					goto IL_24E;
				}
				needMoreChars = true;
				if (this.ps.charsUsed - num < 4)
				{
					break;
				}
				num++;
				c = chars[num];
				if (c != '!')
				{
					if (c != '/')
					{
						goto Block_3;
					}
					this.Throw(num + 1, "Unexpected end tag.");
				}
				else
				{
					num++;
					if (this.ps.charsUsed - num < 2)
					{
						goto Block_5;
					}
					if (chars[num] == '-')
					{
						if (chars[num + 1] == '-')
						{
							goto Block_7;
						}
						this.ThrowUnexpectedToken(num + 1, "-");
					}
					else if (chars[num] == '[')
					{
						if (this.fragmentType != XmlNodeType.Document)
						{
							num++;
							if (this.ps.charsUsed - num < 6)
							{
								goto Block_10;
							}
							if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
							{
								goto Block_11;
							}
							this.ThrowUnexpectedToken(num, "CDATA[");
						}
						else
						{
							this.Throw(this.ps.charPos, "Data at the root level is invalid.");
						}
					}
					else
					{
						if (this.fragmentType == XmlNodeType.Document || this.fragmentType == XmlNodeType.None)
						{
							goto IL_189;
						}
						if (this.ParseUnexpectedToken(num) == "DOCTYPE")
						{
							this.Throw("Unexpected DTD declaration.");
						}
						else
						{
							this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
						}
					}
				}
			}
			return this.ParseDocumentContentAsync_ReadData(needMoreChars);
			Block_3:
			if (c == '?')
			{
				this.ps.charPos = num + 1;
				return this.ParsePIAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseDocumentContentAsync));
			}
			if (this.rootElementParsed)
			{
				if (this.fragmentType == XmlNodeType.Document)
				{
					this.Throw(num, "There are multiple root elements.");
				}
				if (this.fragmentType == XmlNodeType.None)
				{
					this.fragmentType = XmlNodeType.Element;
				}
			}
			this.ps.charPos = num;
			this.rootElementParsed = true;
			return this.ParseElementAsync().ReturnTaskBoolWhenFinish(true);
			Block_5:
			return this.ParseDocumentContentAsync_ReadData(needMoreChars);
			Block_7:
			this.ps.charPos = num + 2;
			return this.ParseCommentAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseDocumentContentAsync));
			Block_10:
			return this.ParseDocumentContentAsync_ReadData(needMoreChars);
			Block_11:
			this.ps.charPos = num + 6;
			return this.ParseCDataAsync().CallBoolTaskFuncWhenFinish(new Func<Task<bool>>(this.ParseDocumentContentAsync_CData));
			IL_189:
			this.fragmentType = XmlNodeType.Document;
			this.ps.charPos = num;
			return this.ParseDoctypeDeclAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseDocumentContentAsync));
			IL_24E:
			if (chars[num] == '&')
			{
				return this.ParseDocumentContentAsync_ParseEntity();
			}
			if (num == this.ps.charsUsed || (this.v1Compat && chars[num] == '\0'))
			{
				return this.ParseDocumentContentAsync_ReadData(needMoreChars);
			}
			if (this.fragmentType == XmlNodeType.Document)
			{
				return this.ParseRootLevelWhitespaceAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseDocumentContentAsync));
			}
			return this.ParseDocumentContentAsync_WhiteSpace();
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x000338A0 File Offset: 0x00031AA0
		private Task<bool> ParseDocumentContentAsync_CData()
		{
			if (this.fragmentType == XmlNodeType.None)
			{
				this.fragmentType = XmlNodeType.Element;
			}
			return AsyncHelper.DoneTaskTrue;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000338B8 File Offset: 0x00031AB8
		private Task<bool> ParseDocumentContentAsync_ParseEntity()
		{
			XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__509 <ParseDocumentContentAsync_ParseEntity>d__;
			<ParseDocumentContentAsync_ParseEntity>d__.<>4__this = this;
			<ParseDocumentContentAsync_ParseEntity>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseDocumentContentAsync_ParseEntity>d__.<>1__state = -1;
			<ParseDocumentContentAsync_ParseEntity>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__509>(ref <ParseDocumentContentAsync_ParseEntity>d__);
			return <ParseDocumentContentAsync_ParseEntity>d__.<>t__builder.Task;
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x000338FC File Offset: 0x00031AFC
		private Task<bool> ParseDocumentContentAsync_WhiteSpace()
		{
			Task<bool> task = this.ParseTextAsync();
			if (!task.IsSuccess())
			{
				return this._ParseDocumentContentAsync_WhiteSpace(task);
			}
			if (task.Result)
			{
				if (this.fragmentType == XmlNodeType.None && this.curNode.type == XmlNodeType.Text)
				{
					this.fragmentType = XmlNodeType.Element;
				}
				return AsyncHelper.DoneTaskTrue;
			}
			return this.ParseDocumentContentAsync();
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00033954 File Offset: 0x00031B54
		private Task<bool> _ParseDocumentContentAsync_WhiteSpace(Task<bool> task)
		{
			XmlTextReaderImpl.<_ParseDocumentContentAsync_WhiteSpace>d__511 <_ParseDocumentContentAsync_WhiteSpace>d__;
			<_ParseDocumentContentAsync_WhiteSpace>d__.<>4__this = this;
			<_ParseDocumentContentAsync_WhiteSpace>d__.task = task;
			<_ParseDocumentContentAsync_WhiteSpace>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ParseDocumentContentAsync_WhiteSpace>d__.<>1__state = -1;
			<_ParseDocumentContentAsync_WhiteSpace>d__.<>t__builder.Start<XmlTextReaderImpl.<_ParseDocumentContentAsync_WhiteSpace>d__511>(ref <_ParseDocumentContentAsync_WhiteSpace>d__);
			return <_ParseDocumentContentAsync_WhiteSpace>d__.<>t__builder.Task;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000339A0 File Offset: 0x00031BA0
		private Task<bool> ParseDocumentContentAsync_ReadData(bool needMoreChars)
		{
			XmlTextReaderImpl.<ParseDocumentContentAsync_ReadData>d__512 <ParseDocumentContentAsync_ReadData>d__;
			<ParseDocumentContentAsync_ReadData>d__.<>4__this = this;
			<ParseDocumentContentAsync_ReadData>d__.needMoreChars = needMoreChars;
			<ParseDocumentContentAsync_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseDocumentContentAsync_ReadData>d__.<>1__state = -1;
			<ParseDocumentContentAsync_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDocumentContentAsync_ReadData>d__512>(ref <ParseDocumentContentAsync_ReadData>d__);
			return <ParseDocumentContentAsync_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000339EC File Offset: 0x00031BEC
		private Task<bool> ParseElementContentAsync()
		{
			int num;
			char c2;
			for (;;)
			{
				num = this.ps.charPos;
				char[] chars = this.ps.chars;
				char c = chars[num];
				if (c == '&')
				{
					goto IL_1B4;
				}
				if (c != '<')
				{
					goto IL_1CC;
				}
				c2 = chars[num + 1];
				if (c2 != '!')
				{
					break;
				}
				num += 2;
				if (this.ps.charsUsed - num < 2)
				{
					goto Block_5;
				}
				if (chars[num] == '-')
				{
					if (chars[num + 1] == '-')
					{
						goto Block_7;
					}
					this.ThrowUnexpectedToken(num + 1, "-");
				}
				else if (chars[num] == '[')
				{
					num++;
					if (this.ps.charsUsed - num < 6)
					{
						goto Block_9;
					}
					if (XmlConvert.StrEqual(chars, num, 6, "CDATA["))
					{
						goto Block_10;
					}
					this.ThrowUnexpectedToken(num, "CDATA[");
				}
				else if (this.ParseUnexpectedToken(num) == "DOCTYPE")
				{
					this.Throw("Unexpected DTD declaration.");
				}
				else
				{
					this.ThrowUnexpectedToken(num, "<!--", "<[CDATA[");
				}
			}
			if (c2 == '/')
			{
				this.ps.charPos = num + 2;
				return this.ParseEndElementAsync().ReturnTaskBoolWhenFinish(true);
			}
			if (c2 == '?')
			{
				this.ps.charPos = num + 2;
				return this.ParsePIAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseElementContentAsync));
			}
			if (num + 1 == this.ps.charsUsed)
			{
				return this.ParseElementContent_ReadData();
			}
			this.ps.charPos = num + 1;
			return this.ParseElementAsync().ReturnTaskBoolWhenFinish(true);
			Block_5:
			return this.ParseElementContent_ReadData();
			Block_7:
			this.ps.charPos = num + 2;
			return this.ParseCommentAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseElementContentAsync));
			Block_9:
			return this.ParseElementContent_ReadData();
			Block_10:
			this.ps.charPos = num + 6;
			return this.ParseCDataAsync().ReturnTaskBoolWhenFinish(true);
			IL_1B4:
			return this.ParseTextAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseElementContentAsync));
			IL_1CC:
			if (num == this.ps.charsUsed)
			{
				return this.ParseElementContent_ReadData();
			}
			return this.ParseTextAsync().ContinueBoolTaskFuncWhenFalse(new Func<Task<bool>>(this.ParseElementContentAsync));
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00033BF4 File Offset: 0x00031DF4
		private Task<bool> ParseElementContent_ReadData()
		{
			XmlTextReaderImpl.<ParseElementContent_ReadData>d__514 <ParseElementContent_ReadData>d__;
			<ParseElementContent_ReadData>d__.<>4__this = this;
			<ParseElementContent_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseElementContent_ReadData>d__.<>1__state = -1;
			<ParseElementContent_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseElementContent_ReadData>d__514>(ref <ParseElementContent_ReadData>d__);
			return <ParseElementContent_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00033C38 File Offset: 0x00031E38
		private Task ParseElementAsync()
		{
			int num = this.ps.charPos;
			char[] chars = this.ps.chars;
			int num2 = -1;
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			while ((this.xmlCharType.charProperties[(int)chars[num]] & 4) != 0)
			{
				num++;
				for (;;)
				{
					if ((this.xmlCharType.charProperties[(int)chars[num]] & 8) != 0)
					{
						num++;
					}
					else
					{
						if (chars[num] != ':')
						{
							goto IL_A2;
						}
						if (num2 == -1)
						{
							break;
						}
						if (this.supportNamespaces)
						{
							goto Block_5;
						}
						num++;
					}
				}
				num2 = num;
				num++;
				continue;
				Block_5:
				this.Throw(num, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
				break;
				IL_A2:
				if (num + 1 >= this.ps.charsUsed)
				{
					break;
				}
				return this.ParseElementAsync_SetElement(num2, num);
			}
			Task<Tuple<int, int>> task = this.ParseQNameAsync();
			return this.ParseElementAsync_ContinueWithSetElement(task);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00033D10 File Offset: 0x00031F10
		private Task ParseElementAsync_ContinueWithSetElement(Task<Tuple<int, int>> task)
		{
			if (task.IsSuccess())
			{
				Tuple<int, int> result = task.Result;
				int item = result.Item1;
				int item2 = result.Item2;
				return this.ParseElementAsync_SetElement(item, item2);
			}
			return this._ParseElementAsync_ContinueWithSetElement(task);
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x00033D48 File Offset: 0x00031F48
		private Task _ParseElementAsync_ContinueWithSetElement(Task<Tuple<int, int>> task)
		{
			XmlTextReaderImpl.<_ParseElementAsync_ContinueWithSetElement>d__517 <_ParseElementAsync_ContinueWithSetElement>d__;
			<_ParseElementAsync_ContinueWithSetElement>d__.<>4__this = this;
			<_ParseElementAsync_ContinueWithSetElement>d__.task = task;
			<_ParseElementAsync_ContinueWithSetElement>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_ParseElementAsync_ContinueWithSetElement>d__.<>1__state = -1;
			<_ParseElementAsync_ContinueWithSetElement>d__.<>t__builder.Start<XmlTextReaderImpl.<_ParseElementAsync_ContinueWithSetElement>d__517>(ref <_ParseElementAsync_ContinueWithSetElement>d__);
			return <_ParseElementAsync_ContinueWithSetElement>d__.<>t__builder.Task;
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00033D94 File Offset: 0x00031F94
		private Task ParseElementAsync_SetElement(int colonPos, int pos)
		{
			char[] chars = this.ps.chars;
			this.namespaceManager.PushScope();
			if (colonPos == -1 || !this.supportNamespaces)
			{
				this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, this.ps.charPos, pos - this.ps.charPos));
			}
			else
			{
				int charPos = this.ps.charPos;
				int num = colonPos - charPos;
				if (num == this.lastPrefix.Length && XmlConvert.StrEqual(chars, charPos, num, this.lastPrefix))
				{
					this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, colonPos + 1, pos - colonPos - 1), this.lastPrefix, null);
				}
				else
				{
					this.curNode.SetNamedNode(XmlNodeType.Element, this.nameTable.Add(chars, colonPos + 1, pos - colonPos - 1), this.nameTable.Add(chars, this.ps.charPos, num), null);
					this.lastPrefix = this.curNode.prefix;
				}
			}
			char c = chars[pos];
			bool flag = (this.xmlCharType.charProperties[(int)c] & 1) > 0;
			this.ps.charPos = pos;
			if (flag)
			{
				return this.ParseAttributesAsync();
			}
			return this.ParseElementAsync_NoAttributes();
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00033ECC File Offset: 0x000320CC
		private Task ParseElementAsync_NoAttributes()
		{
			int charPos = this.ps.charPos;
			char[] chars = this.ps.chars;
			char c = chars[charPos];
			if (c == '>')
			{
				this.ps.charPos = charPos + 1;
				this.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
			}
			else if (c == '/')
			{
				if (charPos + 1 == this.ps.charsUsed)
				{
					this.ps.charPos = charPos;
					return this.ParseElementAsync_ReadData(charPos);
				}
				if (chars[charPos + 1] == '>')
				{
					this.curNode.IsEmptyElement = true;
					this.nextParsingFunction = this.parsingFunction;
					this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
					this.ps.charPos = charPos + 2;
				}
				else
				{
					this.ThrowUnexpectedToken(charPos, ">");
				}
			}
			else
			{
				this.Throw(charPos, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(chars, this.ps.charsUsed, charPos));
			}
			if (this.addDefaultAttributesAndNormalize)
			{
				this.AddDefaultAttributesAndNormalize();
			}
			this.ElementNamespaceLookup();
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00033FBC File Offset: 0x000321BC
		private Task ParseElementAsync_ReadData(int pos)
		{
			XmlTextReaderImpl.<ParseElementAsync_ReadData>d__520 <ParseElementAsync_ReadData>d__;
			<ParseElementAsync_ReadData>d__.<>4__this = this;
			<ParseElementAsync_ReadData>d__.pos = pos;
			<ParseElementAsync_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseElementAsync_ReadData>d__.<>1__state = -1;
			<ParseElementAsync_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseElementAsync_ReadData>d__520>(ref <ParseElementAsync_ReadData>d__);
			return <ParseElementAsync_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00034008 File Offset: 0x00032208
		private Task ParseEndElementAsync()
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[this.index - 1];
			int length = nodeData.prefix.Length;
			int length2 = nodeData.localName.Length;
			if (this.ps.charsUsed - this.ps.charPos < length + length2 + 1)
			{
				return this._ParseEndElmentAsync();
			}
			return this.ParseEndElementAsync_CheckNameAndParse();
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00034068 File Offset: 0x00032268
		private Task _ParseEndElmentAsync()
		{
			XmlTextReaderImpl.<_ParseEndElmentAsync>d__522 <_ParseEndElmentAsync>d__;
			<_ParseEndElmentAsync>d__.<>4__this = this;
			<_ParseEndElmentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_ParseEndElmentAsync>d__.<>1__state = -1;
			<_ParseEndElmentAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<_ParseEndElmentAsync>d__522>(ref <_ParseEndElmentAsync>d__);
			return <_ParseEndElmentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x000340AC File Offset: 0x000322AC
		private Task ParseEndElmentAsync_PrepareData()
		{
			XmlTextReaderImpl.<ParseEndElmentAsync_PrepareData>d__523 <ParseEndElmentAsync_PrepareData>d__;
			<ParseEndElmentAsync_PrepareData>d__.<>4__this = this;
			<ParseEndElmentAsync_PrepareData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEndElmentAsync_PrepareData>d__.<>1__state = -1;
			<ParseEndElmentAsync_PrepareData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEndElmentAsync_PrepareData>d__523>(ref <ParseEndElmentAsync_PrepareData>d__);
			return <ParseEndElmentAsync_PrepareData>d__.<>t__builder.Task;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000340F0 File Offset: 0x000322F0
		private Task ParseEndElementAsync_CheckNameAndParse()
		{
			XmlTextReaderImpl.NodeData nodeData = this.nodes[this.index - 1];
			int length = nodeData.prefix.Length;
			int length2 = nodeData.localName.Length;
			char[] chars = this.ps.chars;
			int nameLen;
			if (nodeData.prefix.Length == 0)
			{
				if (!XmlConvert.StrEqual(chars, this.ps.charPos, length2, nodeData.localName))
				{
					return this.ThrowTagMismatchAsync(nodeData);
				}
				nameLen = length2;
			}
			else
			{
				int num = this.ps.charPos + length;
				if (!XmlConvert.StrEqual(chars, this.ps.charPos, length, nodeData.prefix) || chars[num] != ':' || !XmlConvert.StrEqual(chars, num + 1, length2, nodeData.localName))
				{
					return this.ThrowTagMismatchAsync(nodeData);
				}
				nameLen = length2 + length + 1;
			}
			LineInfo endTagLineInfo = new LineInfo(this.ps.lineNo, this.ps.LinePos);
			return this.ParseEndElementAsync_Finish(nameLen, nodeData, endTagLineInfo);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000341E4 File Offset: 0x000323E4
		private Task ParseEndElementAsync_Finish(int nameLen, XmlTextReaderImpl.NodeData startTagNode, LineInfo endTagLineInfo)
		{
			Task task = this.ParseEndElementAsync_CheckEndTag(nameLen, startTagNode, endTagLineInfo);
			while (task.IsSuccess())
			{
				switch (this.parseEndElement_NextFunc)
				{
				case XmlTextReaderImpl.ParseEndElementParseFunction.CheckEndTag:
					task = this.ParseEndElementAsync_CheckEndTag(nameLen, startTagNode, endTagLineInfo);
					break;
				case XmlTextReaderImpl.ParseEndElementParseFunction.ReadData:
					task = this.ParseEndElementAsync_ReadData();
					break;
				case XmlTextReaderImpl.ParseEndElementParseFunction.Done:
					return task;
				}
			}
			return this.ParseEndElementAsync_Finish(task, nameLen, startTagNode, endTagLineInfo);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00034240 File Offset: 0x00032440
		private Task ParseEndElementAsync_Finish(Task task, int nameLen, XmlTextReaderImpl.NodeData startTagNode, LineInfo endTagLineInfo)
		{
			XmlTextReaderImpl.<ParseEndElementAsync_Finish>d__528 <ParseEndElementAsync_Finish>d__;
			<ParseEndElementAsync_Finish>d__.<>4__this = this;
			<ParseEndElementAsync_Finish>d__.task = task;
			<ParseEndElementAsync_Finish>d__.nameLen = nameLen;
			<ParseEndElementAsync_Finish>d__.startTagNode = startTagNode;
			<ParseEndElementAsync_Finish>d__.endTagLineInfo = endTagLineInfo;
			<ParseEndElementAsync_Finish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEndElementAsync_Finish>d__.<>1__state = -1;
			<ParseEndElementAsync_Finish>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEndElementAsync_Finish>d__528>(ref <ParseEndElementAsync_Finish>d__);
			return <ParseEndElementAsync_Finish>d__.<>t__builder.Task;
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000342A4 File Offset: 0x000324A4
		private Task ParseEndElementAsync_CheckEndTag(int nameLen, XmlTextReaderImpl.NodeData startTagNode, LineInfo endTagLineInfo)
		{
			int num;
			for (;;)
			{
				num = this.ps.charPos + nameLen;
				char[] chars = this.ps.chars;
				if (num == this.ps.charsUsed)
				{
					break;
				}
				bool flag = false;
				if ((this.xmlCharType.charProperties[(int)chars[num]] & 8) != 0 || chars[num] == ':')
				{
					flag = true;
				}
				if (flag)
				{
					goto Block_2;
				}
				if (chars[num] != '>')
				{
					char c;
					while (this.xmlCharType.IsWhiteSpace(c = chars[num]))
					{
						num++;
						if (c != '\n')
						{
							if (c == '\r')
							{
								if (chars[num] == '\n')
								{
									num++;
								}
								else if (num == this.ps.charsUsed && !this.ps.isEof)
								{
									continue;
								}
								this.OnNewLine(num);
							}
						}
						else
						{
							this.OnNewLine(num);
						}
					}
				}
				if (chars[num] == '>')
				{
					goto IL_F4;
				}
				if (num == this.ps.charsUsed)
				{
					goto Block_9;
				}
				this.ThrowUnexpectedToken(num, ">");
			}
			this.parseEndElement_NextFunc = XmlTextReaderImpl.ParseEndElementParseFunction.ReadData;
			return AsyncHelper.DoneTask;
			Block_2:
			return this.ThrowTagMismatchAsync(startTagNode);
			Block_9:
			this.parseEndElement_NextFunc = XmlTextReaderImpl.ParseEndElementParseFunction.ReadData;
			return AsyncHelper.DoneTask;
			IL_F4:
			this.index--;
			this.curNode = this.nodes[this.index];
			startTagNode.lineInfo = endTagLineInfo;
			startTagNode.type = XmlNodeType.EndElement;
			this.ps.charPos = num + 1;
			this.nextParsingFunction = ((this.index > 0) ? this.parsingFunction : XmlTextReaderImpl.ParsingFunction.DocumentContent);
			this.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopElementContext;
			this.parseEndElement_NextFunc = XmlTextReaderImpl.ParseEndElementParseFunction.Done;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00034410 File Offset: 0x00032610
		private Task ParseEndElementAsync_ReadData()
		{
			XmlTextReaderImpl.<ParseEndElementAsync_ReadData>d__530 <ParseEndElementAsync_ReadData>d__;
			<ParseEndElementAsync_ReadData>d__.<>4__this = this;
			<ParseEndElementAsync_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEndElementAsync_ReadData>d__.<>1__state = -1;
			<ParseEndElementAsync_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEndElementAsync_ReadData>d__530>(ref <ParseEndElementAsync_ReadData>d__);
			return <ParseEndElementAsync_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00034454 File Offset: 0x00032654
		private Task ThrowTagMismatchAsync(XmlTextReaderImpl.NodeData startTag)
		{
			XmlTextReaderImpl.<ThrowTagMismatchAsync>d__531 <ThrowTagMismatchAsync>d__;
			<ThrowTagMismatchAsync>d__.<>4__this = this;
			<ThrowTagMismatchAsync>d__.startTag = startTag;
			<ThrowTagMismatchAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ThrowTagMismatchAsync>d__.<>1__state = -1;
			<ThrowTagMismatchAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ThrowTagMismatchAsync>d__531>(ref <ThrowTagMismatchAsync>d__);
			return <ThrowTagMismatchAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000344A0 File Offset: 0x000326A0
		private Task ParseAttributesAsync()
		{
			XmlTextReaderImpl.<ParseAttributesAsync>d__532 <ParseAttributesAsync>d__;
			<ParseAttributesAsync>d__.<>4__this = this;
			<ParseAttributesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttributesAsync>d__.<>1__state = -1;
			<ParseAttributesAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseAttributesAsync>d__532>(ref <ParseAttributesAsync>d__);
			return <ParseAttributesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x000344E4 File Offset: 0x000326E4
		private Task ParseAttributeValueSlowAsync(int curPos, char quoteChar, XmlTextReaderImpl.NodeData attr)
		{
			XmlTextReaderImpl.<ParseAttributeValueSlowAsync>d__533 <ParseAttributeValueSlowAsync>d__;
			<ParseAttributeValueSlowAsync>d__.<>4__this = this;
			<ParseAttributeValueSlowAsync>d__.curPos = curPos;
			<ParseAttributeValueSlowAsync>d__.quoteChar = quoteChar;
			<ParseAttributeValueSlowAsync>d__.attr = attr;
			<ParseAttributeValueSlowAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAttributeValueSlowAsync>d__.<>1__state = -1;
			<ParseAttributeValueSlowAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseAttributeValueSlowAsync>d__533>(ref <ParseAttributeValueSlowAsync>d__);
			return <ParseAttributeValueSlowAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00034540 File Offset: 0x00032740
		private Task<bool> ParseTextAsync()
		{
			int num = 0;
			if (this.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
			{
				return this._ParseTextAsync(null);
			}
			this.curNode.SetLineInfo(this.ps.LineNo, this.ps.LinePos);
			Task<Tuple<int, int, int, bool>> task = this.ParseTextAsync(num);
			if (!task.IsSuccess())
			{
				return this._ParseTextAsync(task);
			}
			Tuple<int, int, int, bool> result = task.Result;
			int item = result.Item1;
			int item2 = result.Item2;
			num = result.Item3;
			bool item3 = result.Item4;
			if (!item3)
			{
				return this._ParseTextAsync(task);
			}
			if (item2 - item == 0)
			{
				return this.ParseTextAsync_IgnoreNode();
			}
			XmlNodeType textNodeType = this.GetTextNodeType(num);
			if (textNodeType == XmlNodeType.None)
			{
				return this.ParseTextAsync_IgnoreNode();
			}
			this.curNode.SetValueNode(textNodeType, this.ps.chars, item, item2 - item);
			return AsyncHelper.DoneTaskTrue;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0003460C File Offset: 0x0003280C
		private Task<bool> _ParseTextAsync(Task<Tuple<int, int, int, bool>> parseTask)
		{
			XmlTextReaderImpl.<_ParseTextAsync>d__535 <_ParseTextAsync>d__;
			<_ParseTextAsync>d__.<>4__this = this;
			<_ParseTextAsync>d__.parseTask = parseTask;
			<_ParseTextAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ParseTextAsync>d__.<>1__state = -1;
			<_ParseTextAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<_ParseTextAsync>d__535>(ref <_ParseTextAsync>d__);
			return <_ParseTextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00034658 File Offset: 0x00032858
		private Task<bool> ParseTextAsync_IgnoreNode()
		{
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.ReportEndEntity)
			{
				this.SetupEndEntityNodeInContent();
				this.parsingFunction = this.nextParsingFunction;
				return AsyncHelper.DoneTaskTrue;
			}
			if (this.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
			{
				this.parsingFunction = this.nextNextParsingFunction;
				return this.ParseEntityReferenceAsync().ReturnTaskBoolWhenFinish(true);
			}
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000346B0 File Offset: 0x000328B0
		private Task<Tuple<int, int, int, bool>> ParseTextAsync(int outOrChars)
		{
			Task<Tuple<int, int, int, bool>> task = this.ParseTextAsync(outOrChars, this.ps.chars, this.ps.charPos, 0, -1, outOrChars, '\0');
			while (task.IsSuccess())
			{
				outOrChars = this.lastParseTextState.outOrChars;
				char[] chars = this.lastParseTextState.chars;
				int pos = this.lastParseTextState.pos;
				int rcount = this.lastParseTextState.rcount;
				int rpos = this.lastParseTextState.rpos;
				int orChars = this.lastParseTextState.orChars;
				char c = this.lastParseTextState.c;
				switch (this.parseText_NextFunction)
				{
				case XmlTextReaderImpl.ParseTextFunction.ParseText:
					task = this.ParseTextAsync(outOrChars, chars, pos, rcount, rpos, orChars, c);
					break;
				case XmlTextReaderImpl.ParseTextFunction.Entity:
					task = this.ParseTextAsync_ParseEntity(outOrChars, chars, pos, rcount, rpos, orChars, c);
					break;
				case XmlTextReaderImpl.ParseTextFunction.Surrogate:
					task = this.ParseTextAsync_Surrogate(outOrChars, chars, pos, rcount, rpos, orChars, c);
					break;
				case XmlTextReaderImpl.ParseTextFunction.ReadData:
					task = this.ParseTextAsync_ReadData(outOrChars, chars, pos, rcount, rpos, orChars, c);
					break;
				case XmlTextReaderImpl.ParseTextFunction.NoValue:
					return this.ParseTextAsync_NoValue(outOrChars, pos);
				case XmlTextReaderImpl.ParseTextFunction.PartialValue:
					return this.ParseTextAsync_PartialValue(pos, rcount, rpos, orChars, c);
				}
			}
			return this.ParseTextAsync_AsyncFunc(task);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000347E4 File Offset: 0x000329E4
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_AsyncFunc(Task<Tuple<int, int, int, bool>> task)
		{
			XmlTextReaderImpl.<ParseTextAsync_AsyncFunc>d__543 <ParseTextAsync_AsyncFunc>d__;
			<ParseTextAsync_AsyncFunc>d__.<>4__this = this;
			<ParseTextAsync_AsyncFunc>d__.task = task;
			<ParseTextAsync_AsyncFunc>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, int, bool>>.Create();
			<ParseTextAsync_AsyncFunc>d__.<>1__state = -1;
			<ParseTextAsync_AsyncFunc>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseTextAsync_AsyncFunc>d__543>(ref <ParseTextAsync_AsyncFunc>d__);
			return <ParseTextAsync_AsyncFunc>d__.<>t__builder.Task;
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00034830 File Offset: 0x00032A30
		private Task<Tuple<int, int, int, bool>> ParseTextAsync(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
		{
			for (;;)
			{
				if ((this.xmlCharType.charProperties[(int)(c = chars[pos])] & 64) == 0)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							pos++;
							continue;
						case '\n':
							pos++;
							this.OnNewLine(pos);
							continue;
						case '\v':
						case '\f':
							goto IL_214;
						case '\r':
							if (chars[pos + 1] == '\n')
							{
								if (!this.ps.eolNormalized && this.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
								{
									if (pos - this.ps.charPos > 0)
									{
										if (rcount == 0)
										{
											rcount = 1;
											rpos = pos;
										}
										else
										{
											this.ShiftBuffer(rpos + rcount, rpos, pos - rpos - rcount);
											rpos = pos - rcount;
											rcount++;
										}
									}
									else
									{
										this.ps.charPos = this.ps.charPos + 1;
									}
								}
								pos += 2;
							}
							else
							{
								if (pos + 1 >= this.ps.charsUsed && !this.ps.isEof)
								{
									goto IL_12C;
								}
								if (!this.ps.eolNormalized)
								{
									chars[pos] = '\n';
								}
								pos++;
							}
							this.OnNewLine(pos);
							continue;
						}
						break;
					}
					if (c == '<')
					{
						goto IL_15C;
					}
					if (c != ']')
					{
						goto Block_6;
					}
					if (this.ps.charsUsed - pos < 3 && !this.ps.isEof)
					{
						goto Block_15;
					}
					if (chars[pos + 1] == ']' && chars[pos + 2] == '>')
					{
						this.Throw(pos, "']]>' is not allowed in character data.");
					}
					orChars |= 93;
					pos++;
				}
				else
				{
					orChars |= (int)c;
					pos++;
				}
			}
			if (c == '&')
			{
				this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
				this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.Entity;
				return this.parseText_dummyTask;
			}
			Block_6:
			goto IL_214;
			IL_12C:
			this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
			this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ReadData;
			return this.parseText_dummyTask;
			IL_15C:
			this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
			this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.PartialValue;
			return this.parseText_dummyTask;
			Block_15:
			this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
			this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ReadData;
			return this.parseText_dummyTask;
			IL_214:
			if (pos == this.ps.charsUsed)
			{
				this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
				this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ReadData;
				return this.parseText_dummyTask;
			}
			this.lastParseTextState = new XmlTextReaderImpl.ParseTextState(outOrChars, chars, pos, rcount, rpos, orChars, c);
			this.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.Surrogate;
			return this.parseText_dummyTask;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00034AA8 File Offset: 0x00032CA8
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_ParseEntity(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
		{
			XmlTextReaderImpl.<ParseTextAsync_ParseEntity>d__545 <ParseTextAsync_ParseEntity>d__;
			<ParseTextAsync_ParseEntity>d__.<>4__this = this;
			<ParseTextAsync_ParseEntity>d__.outOrChars = outOrChars;
			<ParseTextAsync_ParseEntity>d__.chars = chars;
			<ParseTextAsync_ParseEntity>d__.pos = pos;
			<ParseTextAsync_ParseEntity>d__.rcount = rcount;
			<ParseTextAsync_ParseEntity>d__.rpos = rpos;
			<ParseTextAsync_ParseEntity>d__.orChars = orChars;
			<ParseTextAsync_ParseEntity>d__.c = c;
			<ParseTextAsync_ParseEntity>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, int, bool>>.Create();
			<ParseTextAsync_ParseEntity>d__.<>1__state = -1;
			<ParseTextAsync_ParseEntity>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseTextAsync_ParseEntity>d__545>(ref <ParseTextAsync_ParseEntity>d__);
			return <ParseTextAsync_ParseEntity>d__.<>t__builder.Task;
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00034B28 File Offset: 0x00032D28
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_Surrogate(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
		{
			XmlTextReaderImpl.<ParseTextAsync_Surrogate>d__546 <ParseTextAsync_Surrogate>d__;
			<ParseTextAsync_Surrogate>d__.<>4__this = this;
			<ParseTextAsync_Surrogate>d__.outOrChars = outOrChars;
			<ParseTextAsync_Surrogate>d__.chars = chars;
			<ParseTextAsync_Surrogate>d__.pos = pos;
			<ParseTextAsync_Surrogate>d__.rcount = rcount;
			<ParseTextAsync_Surrogate>d__.rpos = rpos;
			<ParseTextAsync_Surrogate>d__.orChars = orChars;
			<ParseTextAsync_Surrogate>d__.c = c;
			<ParseTextAsync_Surrogate>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, int, bool>>.Create();
			<ParseTextAsync_Surrogate>d__.<>1__state = -1;
			<ParseTextAsync_Surrogate>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseTextAsync_Surrogate>d__546>(ref <ParseTextAsync_Surrogate>d__);
			return <ParseTextAsync_Surrogate>d__.<>t__builder.Task;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00034BA8 File Offset: 0x00032DA8
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_ReadData(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
		{
			XmlTextReaderImpl.<ParseTextAsync_ReadData>d__547 <ParseTextAsync_ReadData>d__;
			<ParseTextAsync_ReadData>d__.<>4__this = this;
			<ParseTextAsync_ReadData>d__.outOrChars = outOrChars;
			<ParseTextAsync_ReadData>d__.chars = chars;
			<ParseTextAsync_ReadData>d__.pos = pos;
			<ParseTextAsync_ReadData>d__.rcount = rcount;
			<ParseTextAsync_ReadData>d__.rpos = rpos;
			<ParseTextAsync_ReadData>d__.orChars = orChars;
			<ParseTextAsync_ReadData>d__.c = c;
			<ParseTextAsync_ReadData>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, int, bool>>.Create();
			<ParseTextAsync_ReadData>d__.<>1__state = -1;
			<ParseTextAsync_ReadData>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseTextAsync_ReadData>d__547>(ref <ParseTextAsync_ReadData>d__);
			return <ParseTextAsync_ReadData>d__.<>t__builder.Task;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00034C27 File Offset: 0x00032E27
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_NoValue(int outOrChars, int pos)
		{
			return Task.FromResult<Tuple<int, int, int, bool>>(new Tuple<int, int, int, bool>(pos, pos, outOrChars, true));
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00034C38 File Offset: 0x00032E38
		private Task<Tuple<int, int, int, bool>> ParseTextAsync_PartialValue(int pos, int rcount, int rpos, int orChars, char c)
		{
			if (this.parsingMode == XmlTextReaderImpl.ParsingMode.Full && rcount > 0)
			{
				this.ShiftBuffer(rpos + rcount, rpos, pos - rpos - rcount);
			}
			int charPos = this.ps.charPos;
			int item = pos - rcount;
			this.ps.charPos = pos;
			return Task.FromResult<Tuple<int, int, int, bool>>(new Tuple<int, int, int, bool>(charPos, item, orChars, c == '<'));
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00034C90 File Offset: 0x00032E90
		private Task FinishPartialValueAsync()
		{
			XmlTextReaderImpl.<FinishPartialValueAsync>d__550 <FinishPartialValueAsync>d__;
			<FinishPartialValueAsync>d__.<>4__this = this;
			<FinishPartialValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishPartialValueAsync>d__.<>1__state = -1;
			<FinishPartialValueAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishPartialValueAsync>d__550>(ref <FinishPartialValueAsync>d__);
			return <FinishPartialValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00034CD4 File Offset: 0x00032ED4
		private Task FinishOtherValueIteratorAsync()
		{
			XmlTextReaderImpl.<FinishOtherValueIteratorAsync>d__551 <FinishOtherValueIteratorAsync>d__;
			<FinishOtherValueIteratorAsync>d__.<>4__this = this;
			<FinishOtherValueIteratorAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishOtherValueIteratorAsync>d__.<>1__state = -1;
			<FinishOtherValueIteratorAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishOtherValueIteratorAsync>d__551>(ref <FinishOtherValueIteratorAsync>d__);
			return <FinishOtherValueIteratorAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00034D18 File Offset: 0x00032F18
		[MethodImpl(MethodImplOptions.NoInlining)]
		private Task SkipPartialTextValueAsync()
		{
			XmlTextReaderImpl.<SkipPartialTextValueAsync>d__552 <SkipPartialTextValueAsync>d__;
			<SkipPartialTextValueAsync>d__.<>4__this = this;
			<SkipPartialTextValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipPartialTextValueAsync>d__.<>1__state = -1;
			<SkipPartialTextValueAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<SkipPartialTextValueAsync>d__552>(ref <SkipPartialTextValueAsync>d__);
			return <SkipPartialTextValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00034D5B File Offset: 0x00032F5B
		private Task FinishReadValueChunkAsync()
		{
			this.readValueOffset = 0;
			if (this.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
			{
				return this.SkipPartialTextValueAsync();
			}
			this.parsingFunction = this.nextParsingFunction;
			this.nextParsingFunction = this.nextNextParsingFunction;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00034D94 File Offset: 0x00032F94
		private Task FinishReadContentAsBinaryAsync()
		{
			XmlTextReaderImpl.<FinishReadContentAsBinaryAsync>d__554 <FinishReadContentAsBinaryAsync>d__;
			<FinishReadContentAsBinaryAsync>d__.<>4__this = this;
			<FinishReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishReadContentAsBinaryAsync>d__.<>1__state = -1;
			<FinishReadContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishReadContentAsBinaryAsync>d__554>(ref <FinishReadContentAsBinaryAsync>d__);
			return <FinishReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00034DD8 File Offset: 0x00032FD8
		private Task FinishReadElementContentAsBinaryAsync()
		{
			XmlTextReaderImpl.<FinishReadElementContentAsBinaryAsync>d__555 <FinishReadElementContentAsBinaryAsync>d__;
			<FinishReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<FinishReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FinishReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<FinishReadElementContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<FinishReadElementContentAsBinaryAsync>d__555>(ref <FinishReadElementContentAsBinaryAsync>d__);
			return <FinishReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00034E1C File Offset: 0x0003301C
		private Task<bool> ParseRootLevelWhitespaceAsync()
		{
			XmlTextReaderImpl.<ParseRootLevelWhitespaceAsync>d__556 <ParseRootLevelWhitespaceAsync>d__;
			<ParseRootLevelWhitespaceAsync>d__.<>4__this = this;
			<ParseRootLevelWhitespaceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseRootLevelWhitespaceAsync>d__.<>1__state = -1;
			<ParseRootLevelWhitespaceAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseRootLevelWhitespaceAsync>d__556>(ref <ParseRootLevelWhitespaceAsync>d__);
			return <ParseRootLevelWhitespaceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00034E60 File Offset: 0x00033060
		private Task ParseEntityReferenceAsync()
		{
			XmlTextReaderImpl.<ParseEntityReferenceAsync>d__557 <ParseEntityReferenceAsync>d__;
			<ParseEntityReferenceAsync>d__.<>4__this = this;
			<ParseEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseEntityReferenceAsync>d__.<>1__state = -1;
			<ParseEntityReferenceAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEntityReferenceAsync>d__557>(ref <ParseEntityReferenceAsync>d__);
			return <ParseEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00034EA4 File Offset: 0x000330A4
		private Task<Tuple<int, XmlTextReaderImpl.EntityType>> HandleEntityReferenceAsync(bool isInAttributeValue, XmlTextReaderImpl.EntityExpandType expandType)
		{
			XmlTextReaderImpl.<HandleEntityReferenceAsync>d__558 <HandleEntityReferenceAsync>d__;
			<HandleEntityReferenceAsync>d__.<>4__this = this;
			<HandleEntityReferenceAsync>d__.isInAttributeValue = isInAttributeValue;
			<HandleEntityReferenceAsync>d__.expandType = expandType;
			<HandleEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, XmlTextReaderImpl.EntityType>>.Create();
			<HandleEntityReferenceAsync>d__.<>1__state = -1;
			<HandleEntityReferenceAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<HandleEntityReferenceAsync>d__558>(ref <HandleEntityReferenceAsync>d__);
			return <HandleEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00034EF8 File Offset: 0x000330F8
		private Task<XmlTextReaderImpl.EntityType> HandleGeneralEntityReferenceAsync(string name, bool isInAttributeValue, bool pushFakeEntityIfNullResolver, int entityStartLinePos)
		{
			XmlTextReaderImpl.<HandleGeneralEntityReferenceAsync>d__559 <HandleGeneralEntityReferenceAsync>d__;
			<HandleGeneralEntityReferenceAsync>d__.<>4__this = this;
			<HandleGeneralEntityReferenceAsync>d__.name = name;
			<HandleGeneralEntityReferenceAsync>d__.isInAttributeValue = isInAttributeValue;
			<HandleGeneralEntityReferenceAsync>d__.pushFakeEntityIfNullResolver = pushFakeEntityIfNullResolver;
			<HandleGeneralEntityReferenceAsync>d__.entityStartLinePos = entityStartLinePos;
			<HandleGeneralEntityReferenceAsync>d__.<>t__builder = AsyncTaskMethodBuilder<XmlTextReaderImpl.EntityType>.Create();
			<HandleGeneralEntityReferenceAsync>d__.<>1__state = -1;
			<HandleGeneralEntityReferenceAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<HandleGeneralEntityReferenceAsync>d__559>(ref <HandleGeneralEntityReferenceAsync>d__);
			return <HandleGeneralEntityReferenceAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00034F5C File Offset: 0x0003315C
		private Task<bool> ParsePIAsync()
		{
			return this.ParsePIAsync(null);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00034F68 File Offset: 0x00033168
		private Task<bool> ParsePIAsync(StringBuilder piInDtdStringBuilder)
		{
			XmlTextReaderImpl.<ParsePIAsync>d__561 <ParsePIAsync>d__;
			<ParsePIAsync>d__.<>4__this = this;
			<ParsePIAsync>d__.piInDtdStringBuilder = piInDtdStringBuilder;
			<ParsePIAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParsePIAsync>d__.<>1__state = -1;
			<ParsePIAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParsePIAsync>d__561>(ref <ParsePIAsync>d__);
			return <ParsePIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00034FB4 File Offset: 0x000331B4
		private Task<Tuple<int, int, bool>> ParsePIValueAsync()
		{
			XmlTextReaderImpl.<ParsePIValueAsync>d__562 <ParsePIValueAsync>d__;
			<ParsePIValueAsync>d__.<>4__this = this;
			<ParsePIValueAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, bool>>.Create();
			<ParsePIValueAsync>d__.<>1__state = -1;
			<ParsePIValueAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParsePIValueAsync>d__562>(ref <ParsePIValueAsync>d__);
			return <ParsePIValueAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00034FF8 File Offset: 0x000331F8
		private Task<bool> ParseCommentAsync()
		{
			XmlTextReaderImpl.<ParseCommentAsync>d__563 <ParseCommentAsync>d__;
			<ParseCommentAsync>d__.<>4__this = this;
			<ParseCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseCommentAsync>d__.<>1__state = -1;
			<ParseCommentAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseCommentAsync>d__563>(ref <ParseCommentAsync>d__);
			return <ParseCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0003503B File Offset: 0x0003323B
		private Task ParseCDataAsync()
		{
			return this.ParseCDataOrCommentAsync(XmlNodeType.CDATA);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00035044 File Offset: 0x00033244
		private Task ParseCDataOrCommentAsync(XmlNodeType type)
		{
			XmlTextReaderImpl.<ParseCDataOrCommentAsync>d__565 <ParseCDataOrCommentAsync>d__;
			<ParseCDataOrCommentAsync>d__.<>4__this = this;
			<ParseCDataOrCommentAsync>d__.type = type;
			<ParseCDataOrCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseCDataOrCommentAsync>d__.<>1__state = -1;
			<ParseCDataOrCommentAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseCDataOrCommentAsync>d__565>(ref <ParseCDataOrCommentAsync>d__);
			return <ParseCDataOrCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00035090 File Offset: 0x00033290
		private Task<Tuple<int, int, bool>> ParseCDataOrCommentTupleAsync(XmlNodeType type)
		{
			XmlTextReaderImpl.<ParseCDataOrCommentTupleAsync>d__566 <ParseCDataOrCommentTupleAsync>d__;
			<ParseCDataOrCommentTupleAsync>d__.<>4__this = this;
			<ParseCDataOrCommentTupleAsync>d__.type = type;
			<ParseCDataOrCommentTupleAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int, bool>>.Create();
			<ParseCDataOrCommentTupleAsync>d__.<>1__state = -1;
			<ParseCDataOrCommentTupleAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseCDataOrCommentTupleAsync>d__566>(ref <ParseCDataOrCommentTupleAsync>d__);
			return <ParseCDataOrCommentTupleAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000350DC File Offset: 0x000332DC
		private Task<bool> ParseDoctypeDeclAsync()
		{
			XmlTextReaderImpl.<ParseDoctypeDeclAsync>d__567 <ParseDoctypeDeclAsync>d__;
			<ParseDoctypeDeclAsync>d__.<>4__this = this;
			<ParseDoctypeDeclAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ParseDoctypeDeclAsync>d__.<>1__state = -1;
			<ParseDoctypeDeclAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDoctypeDeclAsync>d__567>(ref <ParseDoctypeDeclAsync>d__);
			return <ParseDoctypeDeclAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00035120 File Offset: 0x00033320
		private Task ParseDtdAsync()
		{
			XmlTextReaderImpl.<ParseDtdAsync>d__568 <ParseDtdAsync>d__;
			<ParseDtdAsync>d__.<>4__this = this;
			<ParseDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseDtdAsync>d__.<>1__state = -1;
			<ParseDtdAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDtdAsync>d__568>(ref <ParseDtdAsync>d__);
			return <ParseDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00035164 File Offset: 0x00033364
		private Task SkipDtdAsync()
		{
			XmlTextReaderImpl.<SkipDtdAsync>d__569 <SkipDtdAsync>d__;
			<SkipDtdAsync>d__.<>4__this = this;
			<SkipDtdAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipDtdAsync>d__.<>1__state = -1;
			<SkipDtdAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref <SkipDtdAsync>d__);
			return <SkipDtdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000351A8 File Offset: 0x000333A8
		private Task SkipPublicOrSystemIdLiteralAsync()
		{
			char c = this.ps.chars[this.ps.charPos];
			if (c != '"' && c != '\'')
			{
				this.ThrowUnexpectedToken("\"", "'");
			}
			this.ps.charPos = this.ps.charPos + 1;
			return this.SkipUntilAsync(c, false);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00035200 File Offset: 0x00033400
		private Task SkipUntilAsync(char stopChar, bool recognizeLiterals)
		{
			XmlTextReaderImpl.<SkipUntilAsync>d__571 <SkipUntilAsync>d__;
			<SkipUntilAsync>d__.<>4__this = this;
			<SkipUntilAsync>d__.stopChar = stopChar;
			<SkipUntilAsync>d__.recognizeLiterals = recognizeLiterals;
			<SkipUntilAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipUntilAsync>d__.<>1__state = -1;
			<SkipUntilAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<SkipUntilAsync>d__571>(ref <SkipUntilAsync>d__);
			return <SkipUntilAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00035254 File Offset: 0x00033454
		private Task<int> EatWhitespacesAsync(StringBuilder sb)
		{
			XmlTextReaderImpl.<EatWhitespacesAsync>d__572 <EatWhitespacesAsync>d__;
			<EatWhitespacesAsync>d__.<>4__this = this;
			<EatWhitespacesAsync>d__.sb = sb;
			<EatWhitespacesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<EatWhitespacesAsync>d__.<>1__state = -1;
			<EatWhitespacesAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<EatWhitespacesAsync>d__572>(ref <EatWhitespacesAsync>d__);
			return <EatWhitespacesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000352A0 File Offset: 0x000334A0
		private Task<Tuple<XmlTextReaderImpl.EntityType, int>> ParseNumericCharRefAsync(bool expand, StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.<ParseNumericCharRefAsync>d__573 <ParseNumericCharRefAsync>d__;
			<ParseNumericCharRefAsync>d__.<>4__this = this;
			<ParseNumericCharRefAsync>d__.expand = expand;
			<ParseNumericCharRefAsync>d__.internalSubsetBuilder = internalSubsetBuilder;
			<ParseNumericCharRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<XmlTextReaderImpl.EntityType, int>>.Create();
			<ParseNumericCharRefAsync>d__.<>1__state = -1;
			<ParseNumericCharRefAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseNumericCharRefAsync>d__573>(ref <ParseNumericCharRefAsync>d__);
			return <ParseNumericCharRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x000352F4 File Offset: 0x000334F4
		private Task<int> ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder)
		{
			XmlTextReaderImpl.<ParseNamedCharRefAsync>d__574 <ParseNamedCharRefAsync>d__;
			<ParseNamedCharRefAsync>d__.<>4__this = this;
			<ParseNamedCharRefAsync>d__.expand = expand;
			<ParseNamedCharRefAsync>d__.internalSubsetBuilder = internalSubsetBuilder;
			<ParseNamedCharRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ParseNamedCharRefAsync>d__.<>1__state = -1;
			<ParseNamedCharRefAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseNamedCharRefAsync>d__574>(ref <ParseNamedCharRefAsync>d__);
			return <ParseNamedCharRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00035348 File Offset: 0x00033548
		private Task<int> ParseNameAsync()
		{
			XmlTextReaderImpl.<ParseNameAsync>d__575 <ParseNameAsync>d__;
			<ParseNameAsync>d__.<>4__this = this;
			<ParseNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ParseNameAsync>d__.<>1__state = -1;
			<ParseNameAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseNameAsync>d__575>(ref <ParseNameAsync>d__);
			return <ParseNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0003538B File Offset: 0x0003358B
		private Task<Tuple<int, int>> ParseQNameAsync()
		{
			return this.ParseQNameAsync(true, 0);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00035398 File Offset: 0x00033598
		private Task<Tuple<int, int>> ParseQNameAsync(bool isQName, int startOffset)
		{
			XmlTextReaderImpl.<ParseQNameAsync>d__577 <ParseQNameAsync>d__;
			<ParseQNameAsync>d__.<>4__this = this;
			<ParseQNameAsync>d__.isQName = isQName;
			<ParseQNameAsync>d__.startOffset = startOffset;
			<ParseQNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, int>>.Create();
			<ParseQNameAsync>d__.<>1__state = -1;
			<ParseQNameAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseQNameAsync>d__577>(ref <ParseQNameAsync>d__);
			return <ParseQNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000353EC File Offset: 0x000335EC
		private Task<Tuple<int, bool>> ReadDataInNameAsync(int pos)
		{
			XmlTextReaderImpl.<ReadDataInNameAsync>d__578 <ReadDataInNameAsync>d__;
			<ReadDataInNameAsync>d__.<>4__this = this;
			<ReadDataInNameAsync>d__.pos = pos;
			<ReadDataInNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<Tuple<int, bool>>.Create();
			<ReadDataInNameAsync>d__.<>1__state = -1;
			<ReadDataInNameAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadDataInNameAsync>d__578>(ref <ReadDataInNameAsync>d__);
			return <ReadDataInNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00035438 File Offset: 0x00033638
		private Task<string> ParseEntityNameAsync()
		{
			XmlTextReaderImpl.<ParseEntityNameAsync>d__579 <ParseEntityNameAsync>d__;
			<ParseEntityNameAsync>d__.<>4__this = this;
			<ParseEntityNameAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ParseEntityNameAsync>d__.<>1__state = -1;
			<ParseEntityNameAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseEntityNameAsync>d__579>(ref <ParseEntityNameAsync>d__);
			return <ParseEntityNameAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0003547C File Offset: 0x0003367C
		private Task PushExternalEntityOrSubsetAsync(string publicId, string systemId, Uri baseUri, string entityName)
		{
			XmlTextReaderImpl.<PushExternalEntityOrSubsetAsync>d__580 <PushExternalEntityOrSubsetAsync>d__;
			<PushExternalEntityOrSubsetAsync>d__.<>4__this = this;
			<PushExternalEntityOrSubsetAsync>d__.publicId = publicId;
			<PushExternalEntityOrSubsetAsync>d__.systemId = systemId;
			<PushExternalEntityOrSubsetAsync>d__.baseUri = baseUri;
			<PushExternalEntityOrSubsetAsync>d__.entityName = entityName;
			<PushExternalEntityOrSubsetAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PushExternalEntityOrSubsetAsync>d__.<>1__state = -1;
			<PushExternalEntityOrSubsetAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<PushExternalEntityOrSubsetAsync>d__580>(ref <PushExternalEntityOrSubsetAsync>d__);
			return <PushExternalEntityOrSubsetAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x000354E0 File Offset: 0x000336E0
		private Task<bool> OpenAndPushAsync(Uri uri)
		{
			XmlTextReaderImpl.<OpenAndPushAsync>d__581 <OpenAndPushAsync>d__;
			<OpenAndPushAsync>d__.<>4__this = this;
			<OpenAndPushAsync>d__.uri = uri;
			<OpenAndPushAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<OpenAndPushAsync>d__.<>1__state = -1;
			<OpenAndPushAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<OpenAndPushAsync>d__581>(ref <OpenAndPushAsync>d__);
			return <OpenAndPushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0003552C File Offset: 0x0003372C
		private Task<bool> PushExternalEntityAsync(IDtdEntityInfo entity)
		{
			XmlTextReaderImpl.<PushExternalEntityAsync>d__582 <PushExternalEntityAsync>d__;
			<PushExternalEntityAsync>d__.<>4__this = this;
			<PushExternalEntityAsync>d__.entity = entity;
			<PushExternalEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<PushExternalEntityAsync>d__.<>1__state = -1;
			<PushExternalEntityAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<PushExternalEntityAsync>d__582>(ref <PushExternalEntityAsync>d__);
			return <PushExternalEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00035578 File Offset: 0x00033778
		private Task<bool> ZeroEndingStreamAsync(int pos)
		{
			XmlTextReaderImpl.<ZeroEndingStreamAsync>d__583 <ZeroEndingStreamAsync>d__;
			<ZeroEndingStreamAsync>d__.<>4__this = this;
			<ZeroEndingStreamAsync>d__.pos = pos;
			<ZeroEndingStreamAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ZeroEndingStreamAsync>d__.<>1__state = -1;
			<ZeroEndingStreamAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ZeroEndingStreamAsync>d__583>(ref <ZeroEndingStreamAsync>d__);
			return <ZeroEndingStreamAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x000355C4 File Offset: 0x000337C4
		private Task ParseDtdFromParserContextAsync()
		{
			XmlTextReaderImpl.<ParseDtdFromParserContextAsync>d__584 <ParseDtdFromParserContextAsync>d__;
			<ParseDtdFromParserContextAsync>d__.<>4__this = this;
			<ParseDtdFromParserContextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseDtdFromParserContextAsync>d__.<>1__state = -1;
			<ParseDtdFromParserContextAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ParseDtdFromParserContextAsync>d__584>(ref <ParseDtdFromParserContextAsync>d__);
			return <ParseDtdFromParserContextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00035608 File Offset: 0x00033808
		private Task<bool> InitReadContentAsBinaryAsync()
		{
			XmlTextReaderImpl.<InitReadContentAsBinaryAsync>d__585 <InitReadContentAsBinaryAsync>d__;
			<InitReadContentAsBinaryAsync>d__.<>4__this = this;
			<InitReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitReadContentAsBinaryAsync>d__.<>1__state = -1;
			<InitReadContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<InitReadContentAsBinaryAsync>d__585>(ref <InitReadContentAsBinaryAsync>d__);
			return <InitReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0003564C File Offset: 0x0003384C
		private Task<bool> InitReadElementContentAsBinaryAsync()
		{
			XmlTextReaderImpl.<InitReadElementContentAsBinaryAsync>d__586 <InitReadElementContentAsBinaryAsync>d__;
			<InitReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<InitReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<InitReadElementContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<InitReadElementContentAsBinaryAsync>d__586>(ref <InitReadElementContentAsBinaryAsync>d__);
			return <InitReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00035690 File Offset: 0x00033890
		private Task<bool> MoveToNextContentNodeAsync(bool moveIfOnContentNode)
		{
			XmlTextReaderImpl.<MoveToNextContentNodeAsync>d__587 <MoveToNextContentNodeAsync>d__;
			<MoveToNextContentNodeAsync>d__.<>4__this = this;
			<MoveToNextContentNodeAsync>d__.moveIfOnContentNode = moveIfOnContentNode;
			<MoveToNextContentNodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<MoveToNextContentNodeAsync>d__.<>1__state = -1;
			<MoveToNextContentNodeAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<MoveToNextContentNodeAsync>d__587>(ref <MoveToNextContentNodeAsync>d__);
			return <MoveToNextContentNodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000356DC File Offset: 0x000338DC
		private Task<int> ReadContentAsBinaryAsync(byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadContentAsBinaryAsync>d__588 <ReadContentAsBinaryAsync>d__;
			<ReadContentAsBinaryAsync>d__.<>4__this = this;
			<ReadContentAsBinaryAsync>d__.buffer = buffer;
			<ReadContentAsBinaryAsync>d__.index = index;
			<ReadContentAsBinaryAsync>d__.count = count;
			<ReadContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadContentAsBinaryAsync>d__.<>1__state = -1;
			<ReadContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadContentAsBinaryAsync>d__588>(ref <ReadContentAsBinaryAsync>d__);
			return <ReadContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00035738 File Offset: 0x00033938
		private Task<int> ReadElementContentAsBinaryAsync(byte[] buffer, int index, int count)
		{
			XmlTextReaderImpl.<ReadElementContentAsBinaryAsync>d__589 <ReadElementContentAsBinaryAsync>d__;
			<ReadElementContentAsBinaryAsync>d__.<>4__this = this;
			<ReadElementContentAsBinaryAsync>d__.buffer = buffer;
			<ReadElementContentAsBinaryAsync>d__.index = index;
			<ReadElementContentAsBinaryAsync>d__.count = count;
			<ReadElementContentAsBinaryAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<ReadElementContentAsBinaryAsync>d__.<>1__state = -1;
			<ReadElementContentAsBinaryAsync>d__.<>t__builder.Start<XmlTextReaderImpl.<ReadElementContentAsBinaryAsync>d__589>(ref <ReadElementContentAsBinaryAsync>d__);
			return <ReadElementContentAsBinaryAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000918 RID: 2328
		private readonly bool useAsync;

		// Token: 0x04000919 RID: 2329
		private XmlTextReaderImpl.LaterInitParam laterInitParam;

		// Token: 0x0400091A RID: 2330
		private XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x0400091B RID: 2331
		private XmlTextReaderImpl.ParsingState ps;

		// Token: 0x0400091C RID: 2332
		private XmlTextReaderImpl.ParsingFunction parsingFunction;

		// Token: 0x0400091D RID: 2333
		private XmlTextReaderImpl.ParsingFunction nextParsingFunction;

		// Token: 0x0400091E RID: 2334
		private XmlTextReaderImpl.ParsingFunction nextNextParsingFunction;

		// Token: 0x0400091F RID: 2335
		private XmlTextReaderImpl.NodeData[] nodes;

		// Token: 0x04000920 RID: 2336
		private XmlTextReaderImpl.NodeData curNode;

		// Token: 0x04000921 RID: 2337
		private int index;

		// Token: 0x04000922 RID: 2338
		private int curAttrIndex = -1;

		// Token: 0x04000923 RID: 2339
		private int attrCount;

		// Token: 0x04000924 RID: 2340
		private int attrHashtable;

		// Token: 0x04000925 RID: 2341
		private int attrDuplWalkCount;

		// Token: 0x04000926 RID: 2342
		private bool attrNeedNamespaceLookup;

		// Token: 0x04000927 RID: 2343
		private bool fullAttrCleanup;

		// Token: 0x04000928 RID: 2344
		private XmlTextReaderImpl.NodeData[] attrDuplSortingArray;

		// Token: 0x04000929 RID: 2345
		private XmlNameTable nameTable;

		// Token: 0x0400092A RID: 2346
		private bool nameTableFromSettings;

		// Token: 0x0400092B RID: 2347
		private XmlResolver xmlResolver;

		// Token: 0x0400092C RID: 2348
		private string url = string.Empty;

		// Token: 0x0400092D RID: 2349
		private bool normalize;

		// Token: 0x0400092E RID: 2350
		private bool supportNamespaces = true;

		// Token: 0x0400092F RID: 2351
		private WhitespaceHandling whitespaceHandling;

		// Token: 0x04000930 RID: 2352
		private DtdProcessing dtdProcessing = DtdProcessing.Parse;

		// Token: 0x04000931 RID: 2353
		private EntityHandling entityHandling;

		// Token: 0x04000932 RID: 2354
		private bool ignorePIs;

		// Token: 0x04000933 RID: 2355
		private bool ignoreComments;

		// Token: 0x04000934 RID: 2356
		private bool checkCharacters;

		// Token: 0x04000935 RID: 2357
		private int lineNumberOffset;

		// Token: 0x04000936 RID: 2358
		private int linePositionOffset;

		// Token: 0x04000937 RID: 2359
		private bool closeInput;

		// Token: 0x04000938 RID: 2360
		private long maxCharactersInDocument;

		// Token: 0x04000939 RID: 2361
		private long maxCharactersFromEntities;

		// Token: 0x0400093A RID: 2362
		private bool v1Compat;

		// Token: 0x0400093B RID: 2363
		private XmlNamespaceManager namespaceManager;

		// Token: 0x0400093C RID: 2364
		private string lastPrefix = string.Empty;

		// Token: 0x0400093D RID: 2365
		private XmlTextReaderImpl.XmlContext xmlContext;

		// Token: 0x0400093E RID: 2366
		private XmlTextReaderImpl.ParsingState[] parsingStatesStack;

		// Token: 0x0400093F RID: 2367
		private int parsingStatesStackTop = -1;

		// Token: 0x04000940 RID: 2368
		private string reportedBaseUri;

		// Token: 0x04000941 RID: 2369
		private Encoding reportedEncoding;

		// Token: 0x04000942 RID: 2370
		private IDtdInfo dtdInfo;

		// Token: 0x04000943 RID: 2371
		private XmlNodeType fragmentType = XmlNodeType.Document;

		// Token: 0x04000944 RID: 2372
		private XmlParserContext fragmentParserContext;

		// Token: 0x04000945 RID: 2373
		private bool fragment;

		// Token: 0x04000946 RID: 2374
		private IncrementalReadDecoder incReadDecoder;

		// Token: 0x04000947 RID: 2375
		private XmlTextReaderImpl.IncrementalReadState incReadState;

		// Token: 0x04000948 RID: 2376
		private LineInfo incReadLineInfo;

		// Token: 0x04000949 RID: 2377
		private BinHexDecoder binHexDecoder;

		// Token: 0x0400094A RID: 2378
		private Base64Decoder base64Decoder;

		// Token: 0x0400094B RID: 2379
		private int incReadDepth;

		// Token: 0x0400094C RID: 2380
		private int incReadLeftStartPos;

		// Token: 0x0400094D RID: 2381
		private int incReadLeftEndPos;

		// Token: 0x0400094E RID: 2382
		private IncrementalReadCharsDecoder readCharsDecoder;

		// Token: 0x0400094F RID: 2383
		private int attributeValueBaseEntityId;

		// Token: 0x04000950 RID: 2384
		private bool emptyEntityInAttributeResolved;

		// Token: 0x04000951 RID: 2385
		private IValidationEventHandling validationEventHandling;

		// Token: 0x04000952 RID: 2386
		private XmlTextReaderImpl.OnDefaultAttributeUseDelegate onDefaultAttributeUse;

		// Token: 0x04000953 RID: 2387
		private bool validatingReaderCompatFlag;

		// Token: 0x04000954 RID: 2388
		private bool addDefaultAttributesAndNormalize;

		// Token: 0x04000955 RID: 2389
		private StringBuilder stringBuilder;

		// Token: 0x04000956 RID: 2390
		private bool rootElementParsed;

		// Token: 0x04000957 RID: 2391
		private bool standalone;

		// Token: 0x04000958 RID: 2392
		private int nextEntityId = 1;

		// Token: 0x04000959 RID: 2393
		private XmlTextReaderImpl.ParsingMode parsingMode;

		// Token: 0x0400095A RID: 2394
		private ReadState readState;

		// Token: 0x0400095B RID: 2395
		private IDtdEntityInfo lastEntity;

		// Token: 0x0400095C RID: 2396
		private bool afterResetState;

		// Token: 0x0400095D RID: 2397
		private int documentStartBytePos;

		// Token: 0x0400095E RID: 2398
		private int readValueOffset;

		// Token: 0x0400095F RID: 2399
		private long charactersInDocument;

		// Token: 0x04000960 RID: 2400
		private long charactersFromEntities;

		// Token: 0x04000961 RID: 2401
		private Dictionary<IDtdEntityInfo, IDtdEntityInfo> currentEntities;

		// Token: 0x04000962 RID: 2402
		private bool disableUndeclaredEntityCheck;

		// Token: 0x04000963 RID: 2403
		private XmlReader outerReader;

		// Token: 0x04000964 RID: 2404
		private bool xmlResolverIsSet;

		// Token: 0x04000965 RID: 2405
		private string Xml;

		// Token: 0x04000966 RID: 2406
		private string XmlNs;

		// Token: 0x04000967 RID: 2407
		private const int MaxBytesToMove = 128;

		// Token: 0x04000968 RID: 2408
		private const int ApproxXmlDeclLength = 80;

		// Token: 0x04000969 RID: 2409
		private const int NodesInitialSize = 8;

		// Token: 0x0400096A RID: 2410
		private const int InitialAttributesCount = 4;

		// Token: 0x0400096B RID: 2411
		private const int InitialParsingStateStackSize = 2;

		// Token: 0x0400096C RID: 2412
		private const int InitialParsingStatesDepth = 2;

		// Token: 0x0400096D RID: 2413
		private const int DtdChidrenInitialSize = 2;

		// Token: 0x0400096E RID: 2414
		private const int MaxByteSequenceLen = 6;

		// Token: 0x0400096F RID: 2415
		private const int MaxAttrDuplWalkCount = 250;

		// Token: 0x04000970 RID: 2416
		private const int MinWhitespaceLookahedCount = 4096;

		// Token: 0x04000971 RID: 2417
		private const string XmlDeclarationBegining = "<?xml";

		// Token: 0x04000972 RID: 2418
		private XmlTextReaderImpl.ParseEndElementParseFunction parseEndElement_NextFunc;

		// Token: 0x04000973 RID: 2419
		private XmlTextReaderImpl.ParseTextFunction parseText_NextFunction;

		// Token: 0x04000974 RID: 2420
		private XmlTextReaderImpl.ParseTextState lastParseTextState;

		// Token: 0x04000975 RID: 2421
		private Task<Tuple<int, int, int, bool>> parseText_dummyTask = Task.FromResult<Tuple<int, int, int, bool>>(new Tuple<int, int, int, bool>(0, 0, 0, false));

		// Token: 0x020000BD RID: 189
		private enum ParsingFunction
		{
			// Token: 0x04000977 RID: 2423
			ElementContent,
			// Token: 0x04000978 RID: 2424
			NoData,
			// Token: 0x04000979 RID: 2425
			OpenUrl,
			// Token: 0x0400097A RID: 2426
			SwitchToInteractive,
			// Token: 0x0400097B RID: 2427
			SwitchToInteractiveXmlDecl,
			// Token: 0x0400097C RID: 2428
			DocumentContent,
			// Token: 0x0400097D RID: 2429
			MoveToElementContent,
			// Token: 0x0400097E RID: 2430
			PopElementContext,
			// Token: 0x0400097F RID: 2431
			PopEmptyElementContext,
			// Token: 0x04000980 RID: 2432
			ResetAttributesRootLevel,
			// Token: 0x04000981 RID: 2433
			Error,
			// Token: 0x04000982 RID: 2434
			Eof,
			// Token: 0x04000983 RID: 2435
			ReaderClosed,
			// Token: 0x04000984 RID: 2436
			EntityReference,
			// Token: 0x04000985 RID: 2437
			InIncrementalRead,
			// Token: 0x04000986 RID: 2438
			FragmentAttribute,
			// Token: 0x04000987 RID: 2439
			ReportEndEntity,
			// Token: 0x04000988 RID: 2440
			AfterResolveEntityInContent,
			// Token: 0x04000989 RID: 2441
			AfterResolveEmptyEntityInContent,
			// Token: 0x0400098A RID: 2442
			XmlDeclarationFragment,
			// Token: 0x0400098B RID: 2443
			GoToEof,
			// Token: 0x0400098C RID: 2444
			PartialTextValue,
			// Token: 0x0400098D RID: 2445
			InReadAttributeValue,
			// Token: 0x0400098E RID: 2446
			InReadValueChunk,
			// Token: 0x0400098F RID: 2447
			InReadContentAsBinary,
			// Token: 0x04000990 RID: 2448
			InReadElementContentAsBinary
		}

		// Token: 0x020000BE RID: 190
		private enum ParsingMode
		{
			// Token: 0x04000992 RID: 2450
			Full,
			// Token: 0x04000993 RID: 2451
			SkipNode,
			// Token: 0x04000994 RID: 2452
			SkipContent
		}

		// Token: 0x020000BF RID: 191
		private enum EntityType
		{
			// Token: 0x04000996 RID: 2454
			CharacterDec,
			// Token: 0x04000997 RID: 2455
			CharacterHex,
			// Token: 0x04000998 RID: 2456
			CharacterNamed,
			// Token: 0x04000999 RID: 2457
			Expanded,
			// Token: 0x0400099A RID: 2458
			Skipped,
			// Token: 0x0400099B RID: 2459
			FakeExpanded,
			// Token: 0x0400099C RID: 2460
			Unexpanded,
			// Token: 0x0400099D RID: 2461
			ExpandedInAttribute
		}

		// Token: 0x020000C0 RID: 192
		private enum EntityExpandType
		{
			// Token: 0x0400099F RID: 2463
			All,
			// Token: 0x040009A0 RID: 2464
			OnlyGeneral,
			// Token: 0x040009A1 RID: 2465
			OnlyCharacter
		}

		// Token: 0x020000C1 RID: 193
		private enum IncrementalReadState
		{
			// Token: 0x040009A3 RID: 2467
			Text,
			// Token: 0x040009A4 RID: 2468
			StartTag,
			// Token: 0x040009A5 RID: 2469
			PI,
			// Token: 0x040009A6 RID: 2470
			CDATA,
			// Token: 0x040009A7 RID: 2471
			Comment,
			// Token: 0x040009A8 RID: 2472
			Attributes,
			// Token: 0x040009A9 RID: 2473
			AttributeValue,
			// Token: 0x040009AA RID: 2474
			ReadData,
			// Token: 0x040009AB RID: 2475
			EndElement,
			// Token: 0x040009AC RID: 2476
			End,
			// Token: 0x040009AD RID: 2477
			ReadValueChunk_OnCachedValue,
			// Token: 0x040009AE RID: 2478
			ReadValueChunk_OnPartialValue,
			// Token: 0x040009AF RID: 2479
			ReadContentAsBinary_OnCachedValue,
			// Token: 0x040009B0 RID: 2480
			ReadContentAsBinary_OnPartialValue,
			// Token: 0x040009B1 RID: 2481
			ReadContentAsBinary_End
		}

		// Token: 0x020000C2 RID: 194
		private class LaterInitParam
		{
			// Token: 0x0600093F RID: 2367 RVA: 0x00035793 File Offset: 0x00033993
			public LaterInitParam()
			{
			}

			// Token: 0x040009B2 RID: 2482
			public bool useAsync;

			// Token: 0x040009B3 RID: 2483
			public Stream inputStream;

			// Token: 0x040009B4 RID: 2484
			public byte[] inputBytes;

			// Token: 0x040009B5 RID: 2485
			public int inputByteCount;

			// Token: 0x040009B6 RID: 2486
			public Uri inputbaseUri;

			// Token: 0x040009B7 RID: 2487
			public string inputUriStr;

			// Token: 0x040009B8 RID: 2488
			public XmlResolver inputUriResolver;

			// Token: 0x040009B9 RID: 2489
			public XmlParserContext inputContext;

			// Token: 0x040009BA RID: 2490
			public TextReader inputTextReader;

			// Token: 0x040009BB RID: 2491
			public XmlTextReaderImpl.InitInputType initType = XmlTextReaderImpl.InitInputType.Invalid;
		}

		// Token: 0x020000C3 RID: 195
		private enum InitInputType
		{
			// Token: 0x040009BD RID: 2493
			UriString,
			// Token: 0x040009BE RID: 2494
			Stream,
			// Token: 0x040009BF RID: 2495
			TextReader,
			// Token: 0x040009C0 RID: 2496
			Invalid
		}

		// Token: 0x020000C4 RID: 196
		private enum ParseEndElementParseFunction
		{
			// Token: 0x040009C2 RID: 2498
			CheckEndTag,
			// Token: 0x040009C3 RID: 2499
			ReadData,
			// Token: 0x040009C4 RID: 2500
			Done
		}

		// Token: 0x020000C5 RID: 197
		private class ParseTextState
		{
			// Token: 0x06000940 RID: 2368 RVA: 0x000357A2 File Offset: 0x000339A2
			public ParseTextState(int outOrChars, char[] chars, int pos, int rcount, int rpos, int orChars, char c)
			{
				this.outOrChars = outOrChars;
				this.chars = chars;
				this.pos = pos;
				this.rcount = rcount;
				this.rpos = rpos;
				this.orChars = orChars;
				this.c = c;
			}

			// Token: 0x040009C5 RID: 2501
			public int outOrChars;

			// Token: 0x040009C6 RID: 2502
			public char[] chars;

			// Token: 0x040009C7 RID: 2503
			public int pos;

			// Token: 0x040009C8 RID: 2504
			public int rcount;

			// Token: 0x040009C9 RID: 2505
			public int rpos;

			// Token: 0x040009CA RID: 2506
			public int orChars;

			// Token: 0x040009CB RID: 2507
			public char c;
		}

		// Token: 0x020000C6 RID: 198
		private enum ParseTextFunction
		{
			// Token: 0x040009CD RID: 2509
			ParseText,
			// Token: 0x040009CE RID: 2510
			Entity,
			// Token: 0x040009CF RID: 2511
			Surrogate,
			// Token: 0x040009D0 RID: 2512
			ReadData,
			// Token: 0x040009D1 RID: 2513
			NoValue,
			// Token: 0x040009D2 RID: 2514
			PartialValue
		}

		// Token: 0x020000C7 RID: 199
		private struct ParsingState
		{
			// Token: 0x06000941 RID: 2369 RVA: 0x000357E0 File Offset: 0x000339E0
			internal void Clear()
			{
				this.chars = null;
				this.charPos = 0;
				this.charsUsed = 0;
				this.encoding = null;
				this.stream = null;
				this.decoder = null;
				this.bytes = null;
				this.bytePos = 0;
				this.bytesUsed = 0;
				this.textReader = null;
				this.lineNo = 1;
				this.lineStartPos = -1;
				this.baseUriStr = string.Empty;
				this.baseUri = null;
				this.isEof = false;
				this.isStreamEof = false;
				this.eolNormalized = true;
				this.entityResolvedManually = false;
			}

			// Token: 0x06000942 RID: 2370 RVA: 0x0003586F File Offset: 0x00033A6F
			internal void Close(bool closeInput)
			{
				if (closeInput)
				{
					if (this.stream != null)
					{
						this.stream.Close();
						return;
					}
					if (this.textReader != null)
					{
						this.textReader.Close();
					}
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x06000943 RID: 2371 RVA: 0x0003589B File Offset: 0x00033A9B
			internal int LineNo
			{
				get
				{
					return this.lineNo;
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000944 RID: 2372 RVA: 0x000358A3 File Offset: 0x00033AA3
			internal int LinePos
			{
				get
				{
					return this.charPos - this.lineStartPos;
				}
			}

			// Token: 0x040009D3 RID: 2515
			internal char[] chars;

			// Token: 0x040009D4 RID: 2516
			internal int charPos;

			// Token: 0x040009D5 RID: 2517
			internal int charsUsed;

			// Token: 0x040009D6 RID: 2518
			internal Encoding encoding;

			// Token: 0x040009D7 RID: 2519
			internal bool appendMode;

			// Token: 0x040009D8 RID: 2520
			internal Stream stream;

			// Token: 0x040009D9 RID: 2521
			internal Decoder decoder;

			// Token: 0x040009DA RID: 2522
			internal byte[] bytes;

			// Token: 0x040009DB RID: 2523
			internal int bytePos;

			// Token: 0x040009DC RID: 2524
			internal int bytesUsed;

			// Token: 0x040009DD RID: 2525
			internal TextReader textReader;

			// Token: 0x040009DE RID: 2526
			internal int lineNo;

			// Token: 0x040009DF RID: 2527
			internal int lineStartPos;

			// Token: 0x040009E0 RID: 2528
			internal string baseUriStr;

			// Token: 0x040009E1 RID: 2529
			internal Uri baseUri;

			// Token: 0x040009E2 RID: 2530
			internal bool isEof;

			// Token: 0x040009E3 RID: 2531
			internal bool isStreamEof;

			// Token: 0x040009E4 RID: 2532
			internal IDtdEntityInfo entity;

			// Token: 0x040009E5 RID: 2533
			internal int entityId;

			// Token: 0x040009E6 RID: 2534
			internal bool eolNormalized;

			// Token: 0x040009E7 RID: 2535
			internal bool entityResolvedManually;
		}

		// Token: 0x020000C8 RID: 200
		private class XmlContext
		{
			// Token: 0x06000945 RID: 2373 RVA: 0x000358B2 File Offset: 0x00033AB2
			internal XmlContext()
			{
				this.xmlSpace = XmlSpace.None;
				this.xmlLang = string.Empty;
				this.defaultNamespace = string.Empty;
				this.previousContext = null;
			}

			// Token: 0x06000946 RID: 2374 RVA: 0x000358DE File Offset: 0x00033ADE
			internal XmlContext(XmlTextReaderImpl.XmlContext previousContext)
			{
				this.xmlSpace = previousContext.xmlSpace;
				this.xmlLang = previousContext.xmlLang;
				this.defaultNamespace = previousContext.defaultNamespace;
				this.previousContext = previousContext;
			}

			// Token: 0x040009E8 RID: 2536
			internal XmlSpace xmlSpace;

			// Token: 0x040009E9 RID: 2537
			internal string xmlLang;

			// Token: 0x040009EA RID: 2538
			internal string defaultNamespace;

			// Token: 0x040009EB RID: 2539
			internal XmlTextReaderImpl.XmlContext previousContext;
		}

		// Token: 0x020000C9 RID: 201
		private class NoNamespaceManager : XmlNamespaceManager
		{
			// Token: 0x06000947 RID: 2375 RVA: 0x00035911 File Offset: 0x00033B11
			public NoNamespaceManager()
			{
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001E51E File Offset: 0x0001C71E
			public override string DefaultNamespace
			{
				get
				{
					return string.Empty;
				}
			}

			// Token: 0x06000949 RID: 2377 RVA: 0x0000B528 File Offset: 0x00009728
			public override void PushScope()
			{
			}

			// Token: 0x0600094A RID: 2378 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
			public override bool PopScope()
			{
				return false;
			}

			// Token: 0x0600094B RID: 2379 RVA: 0x0000B528 File Offset: 0x00009728
			public override void AddNamespace(string prefix, string uri)
			{
			}

			// Token: 0x0600094C RID: 2380 RVA: 0x0000B528 File Offset: 0x00009728
			public override void RemoveNamespace(string prefix, string uri)
			{
			}

			// Token: 0x0600094D RID: 2381 RVA: 0x0001DA42 File Offset: 0x0001BC42
			public override IEnumerator GetEnumerator()
			{
				return null;
			}

			// Token: 0x0600094E RID: 2382 RVA: 0x0001DA42 File Offset: 0x0001BC42
			public override IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
			{
				return null;
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x0001E51E File Offset: 0x0001C71E
			public override string LookupNamespace(string prefix)
			{
				return string.Empty;
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x0001DA42 File Offset: 0x0001BC42
			public override string LookupPrefix(string uri)
			{
				return null;
			}

			// Token: 0x06000951 RID: 2385 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
			public override bool HasNamespace(string prefix)
			{
				return false;
			}
		}

		// Token: 0x020000CA RID: 202
		internal class DtdParserProxy : IDtdParserAdapterV1, IDtdParserAdapterWithValidation, IDtdParserAdapter
		{
			// Token: 0x06000952 RID: 2386 RVA: 0x00035919 File Offset: 0x00033B19
			internal DtdParserProxy(XmlTextReaderImpl reader)
			{
				this.reader = reader;
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000953 RID: 2387 RVA: 0x00035928 File Offset: 0x00033B28
			XmlNameTable IDtdParserAdapter.NameTable
			{
				get
				{
					return this.reader.DtdParserProxy_NameTable;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000954 RID: 2388 RVA: 0x00035935 File Offset: 0x00033B35
			IXmlNamespaceResolver IDtdParserAdapter.NamespaceResolver
			{
				get
				{
					return this.reader.DtdParserProxy_NamespaceResolver;
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000955 RID: 2389 RVA: 0x00035942 File Offset: 0x00033B42
			Uri IDtdParserAdapter.BaseUri
			{
				get
				{
					return this.reader.DtdParserProxy_BaseUri;
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06000956 RID: 2390 RVA: 0x0003594F File Offset: 0x00033B4F
			bool IDtdParserAdapter.IsEof
			{
				get
				{
					return this.reader.DtdParserProxy_IsEof;
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000957 RID: 2391 RVA: 0x0003595C File Offset: 0x00033B5C
			char[] IDtdParserAdapter.ParsingBuffer
			{
				get
				{
					return this.reader.DtdParserProxy_ParsingBuffer;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000958 RID: 2392 RVA: 0x00035969 File Offset: 0x00033B69
			int IDtdParserAdapter.ParsingBufferLength
			{
				get
				{
					return this.reader.DtdParserProxy_ParsingBufferLength;
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000959 RID: 2393 RVA: 0x00035976 File Offset: 0x00033B76
			// (set) Token: 0x0600095A RID: 2394 RVA: 0x00035983 File Offset: 0x00033B83
			int IDtdParserAdapter.CurrentPosition
			{
				get
				{
					return this.reader.DtdParserProxy_CurrentPosition;
				}
				set
				{
					this.reader.DtdParserProxy_CurrentPosition = value;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x0600095B RID: 2395 RVA: 0x00035991 File Offset: 0x00033B91
			int IDtdParserAdapter.EntityStackLength
			{
				get
				{
					return this.reader.DtdParserProxy_EntityStackLength;
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x0600095C RID: 2396 RVA: 0x0003599E File Offset: 0x00033B9E
			bool IDtdParserAdapter.IsEntityEolNormalized
			{
				get
				{
					return this.reader.DtdParserProxy_IsEntityEolNormalized;
				}
			}

			// Token: 0x0600095D RID: 2397 RVA: 0x000359AB File Offset: 0x00033BAB
			void IDtdParserAdapter.OnNewLine(int pos)
			{
				this.reader.DtdParserProxy_OnNewLine(pos);
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x0600095E RID: 2398 RVA: 0x000359B9 File Offset: 0x00033BB9
			int IDtdParserAdapter.LineNo
			{
				get
				{
					return this.reader.DtdParserProxy_LineNo;
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x0600095F RID: 2399 RVA: 0x000359C6 File Offset: 0x00033BC6
			int IDtdParserAdapter.LineStartPosition
			{
				get
				{
					return this.reader.DtdParserProxy_LineStartPosition;
				}
			}

			// Token: 0x06000960 RID: 2400 RVA: 0x000359D3 File Offset: 0x00033BD3
			int IDtdParserAdapter.ReadData()
			{
				return this.reader.DtdParserProxy_ReadData();
			}

			// Token: 0x06000961 RID: 2401 RVA: 0x000359E0 File Offset: 0x00033BE0
			int IDtdParserAdapter.ParseNumericCharRef(StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNumericCharRef(internalSubsetBuilder);
			}

			// Token: 0x06000962 RID: 2402 RVA: 0x000359EE File Offset: 0x00033BEE
			int IDtdParserAdapter.ParseNamedCharRef(bool expand, StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNamedCharRef(expand, internalSubsetBuilder);
			}

			// Token: 0x06000963 RID: 2403 RVA: 0x000359FD File Offset: 0x00033BFD
			void IDtdParserAdapter.ParsePI(StringBuilder sb)
			{
				this.reader.DtdParserProxy_ParsePI(sb);
			}

			// Token: 0x06000964 RID: 2404 RVA: 0x00035A0B File Offset: 0x00033C0B
			void IDtdParserAdapter.ParseComment(StringBuilder sb)
			{
				this.reader.DtdParserProxy_ParseComment(sb);
			}

			// Token: 0x06000965 RID: 2405 RVA: 0x00035A19 File Offset: 0x00033C19
			bool IDtdParserAdapter.PushEntity(IDtdEntityInfo entity, out int entityId)
			{
				return this.reader.DtdParserProxy_PushEntity(entity, out entityId);
			}

			// Token: 0x06000966 RID: 2406 RVA: 0x00035A28 File Offset: 0x00033C28
			bool IDtdParserAdapter.PopEntity(out IDtdEntityInfo oldEntity, out int newEntityId)
			{
				return this.reader.DtdParserProxy_PopEntity(out oldEntity, out newEntityId);
			}

			// Token: 0x06000967 RID: 2407 RVA: 0x00035A37 File Offset: 0x00033C37
			bool IDtdParserAdapter.PushExternalSubset(string systemId, string publicId)
			{
				return this.reader.DtdParserProxy_PushExternalSubset(systemId, publicId);
			}

			// Token: 0x06000968 RID: 2408 RVA: 0x00035A46 File Offset: 0x00033C46
			void IDtdParserAdapter.PushInternalDtd(string baseUri, string internalDtd)
			{
				this.reader.DtdParserProxy_PushInternalDtd(baseUri, internalDtd);
			}

			// Token: 0x06000969 RID: 2409 RVA: 0x00035A55 File Offset: 0x00033C55
			void IDtdParserAdapter.Throw(Exception e)
			{
				this.reader.DtdParserProxy_Throw(e);
			}

			// Token: 0x0600096A RID: 2410 RVA: 0x00035A63 File Offset: 0x00033C63
			void IDtdParserAdapter.OnSystemId(string systemId, LineInfo keywordLineInfo, LineInfo systemLiteralLineInfo)
			{
				this.reader.DtdParserProxy_OnSystemId(systemId, keywordLineInfo, systemLiteralLineInfo);
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x00035A73 File Offset: 0x00033C73
			void IDtdParserAdapter.OnPublicId(string publicId, LineInfo keywordLineInfo, LineInfo publicLiteralLineInfo)
			{
				this.reader.DtdParserProxy_OnPublicId(publicId, keywordLineInfo, publicLiteralLineInfo);
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x0600096C RID: 2412 RVA: 0x00035A83 File Offset: 0x00033C83
			bool IDtdParserAdapterWithValidation.DtdValidation
			{
				get
				{
					return this.reader.DtdParserProxy_DtdValidation;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x0600096D RID: 2413 RVA: 0x00035A90 File Offset: 0x00033C90
			IValidationEventHandling IDtdParserAdapterWithValidation.ValidationEventHandling
			{
				get
				{
					return this.reader.DtdParserProxy_ValidationEventHandling;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x0600096E RID: 2414 RVA: 0x00035A9D File Offset: 0x00033C9D
			bool IDtdParserAdapterV1.Normalization
			{
				get
				{
					return this.reader.DtdParserProxy_Normalization;
				}
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x0600096F RID: 2415 RVA: 0x00035AAA File Offset: 0x00033CAA
			bool IDtdParserAdapterV1.Namespaces
			{
				get
				{
					return this.reader.DtdParserProxy_Namespaces;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06000970 RID: 2416 RVA: 0x00035AB7 File Offset: 0x00033CB7
			bool IDtdParserAdapterV1.V1CompatibilityMode
			{
				get
				{
					return this.reader.DtdParserProxy_V1CompatibilityMode;
				}
			}

			// Token: 0x06000971 RID: 2417 RVA: 0x00035AC4 File Offset: 0x00033CC4
			Task<int> IDtdParserAdapter.ReadDataAsync()
			{
				return this.reader.DtdParserProxy_ReadDataAsync();
			}

			// Token: 0x06000972 RID: 2418 RVA: 0x00035AD1 File Offset: 0x00033CD1
			Task<int> IDtdParserAdapter.ParseNumericCharRefAsync(StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNumericCharRefAsync(internalSubsetBuilder);
			}

			// Token: 0x06000973 RID: 2419 RVA: 0x00035ADF File Offset: 0x00033CDF
			Task<int> IDtdParserAdapter.ParseNamedCharRefAsync(bool expand, StringBuilder internalSubsetBuilder)
			{
				return this.reader.DtdParserProxy_ParseNamedCharRefAsync(expand, internalSubsetBuilder);
			}

			// Token: 0x06000974 RID: 2420 RVA: 0x00035AEE File Offset: 0x00033CEE
			Task IDtdParserAdapter.ParsePIAsync(StringBuilder sb)
			{
				return this.reader.DtdParserProxy_ParsePIAsync(sb);
			}

			// Token: 0x06000975 RID: 2421 RVA: 0x00035AFC File Offset: 0x00033CFC
			Task IDtdParserAdapter.ParseCommentAsync(StringBuilder sb)
			{
				return this.reader.DtdParserProxy_ParseCommentAsync(sb);
			}

			// Token: 0x06000976 RID: 2422 RVA: 0x00035B0A File Offset: 0x00033D0A
			Task<Tuple<int, bool>> IDtdParserAdapter.PushEntityAsync(IDtdEntityInfo entity)
			{
				return this.reader.DtdParserProxy_PushEntityAsync(entity);
			}

			// Token: 0x06000977 RID: 2423 RVA: 0x00035B18 File Offset: 0x00033D18
			Task<bool> IDtdParserAdapter.PushExternalSubsetAsync(string systemId, string publicId)
			{
				return this.reader.DtdParserProxy_PushExternalSubsetAsync(systemId, publicId);
			}

			// Token: 0x040009EC RID: 2540
			private XmlTextReaderImpl reader;
		}

		// Token: 0x020000CB RID: 203
		private class NodeData : IComparable
		{
			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000978 RID: 2424 RVA: 0x00035B27 File Offset: 0x00033D27
			internal static XmlTextReaderImpl.NodeData None
			{
				get
				{
					if (XmlTextReaderImpl.NodeData.s_None == null)
					{
						XmlTextReaderImpl.NodeData.s_None = new XmlTextReaderImpl.NodeData();
					}
					return XmlTextReaderImpl.NodeData.s_None;
				}
			}

			// Token: 0x06000979 RID: 2425 RVA: 0x00035B45 File Offset: 0x00033D45
			internal NodeData()
			{
				this.Clear(XmlNodeType.None);
				this.xmlContextPushed = false;
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600097A RID: 2426 RVA: 0x00035B5B File Offset: 0x00033D5B
			internal int LineNo
			{
				get
				{
					return this.lineInfo.lineNo;
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x0600097B RID: 2427 RVA: 0x00035B68 File Offset: 0x00033D68
			internal int LinePos
			{
				get
				{
					return this.lineInfo.linePos;
				}
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x0600097C RID: 2428 RVA: 0x00035B75 File Offset: 0x00033D75
			// (set) Token: 0x0600097D RID: 2429 RVA: 0x00035B88 File Offset: 0x00033D88
			internal bool IsEmptyElement
			{
				get
				{
					return this.type == XmlNodeType.Element && this.isEmptyOrDefault;
				}
				set
				{
					this.isEmptyOrDefault = value;
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x0600097E RID: 2430 RVA: 0x00035B91 File Offset: 0x00033D91
			// (set) Token: 0x0600097F RID: 2431 RVA: 0x00035B88 File Offset: 0x00033D88
			internal bool IsDefaultAttribute
			{
				get
				{
					return this.type == XmlNodeType.Attribute && this.isEmptyOrDefault;
				}
				set
				{
					this.isEmptyOrDefault = value;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000980 RID: 2432 RVA: 0x00035BA4 File Offset: 0x00033DA4
			internal bool ValueBuffered
			{
				get
				{
					return this.value == null;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000981 RID: 2433 RVA: 0x00035BAF File Offset: 0x00033DAF
			internal string StringValue
			{
				get
				{
					if (this.value == null)
					{
						this.value = new string(this.chars, this.valueStartPos, this.valueLength);
					}
					return this.value;
				}
			}

			// Token: 0x06000982 RID: 2434 RVA: 0x00035BDC File Offset: 0x00033DDC
			internal void TrimSpacesInValue()
			{
				if (this.ValueBuffered)
				{
					XmlTextReaderImpl.StripSpaces(this.chars, this.valueStartPos, ref this.valueLength);
					return;
				}
				this.value = XmlTextReaderImpl.StripSpaces(this.value);
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x00035C0F File Offset: 0x00033E0F
			internal void Clear(XmlNodeType type)
			{
				this.type = type;
				this.ClearName();
				this.value = string.Empty;
				this.valueStartPos = -1;
				this.nameWPrefix = string.Empty;
				this.schemaType = null;
				this.typedValue = null;
			}

			// Token: 0x06000984 RID: 2436 RVA: 0x00035C49 File Offset: 0x00033E49
			internal void ClearName()
			{
				this.localName = string.Empty;
				this.prefix = string.Empty;
				this.ns = string.Empty;
				this.nameWPrefix = string.Empty;
			}

			// Token: 0x06000985 RID: 2437 RVA: 0x00035C77 File Offset: 0x00033E77
			internal void SetLineInfo(int lineNo, int linePos)
			{
				this.lineInfo.Set(lineNo, linePos);
			}

			// Token: 0x06000986 RID: 2438 RVA: 0x00035C86 File Offset: 0x00033E86
			internal void SetLineInfo2(int lineNo, int linePos)
			{
				this.lineInfo2.Set(lineNo, linePos);
			}

			// Token: 0x06000987 RID: 2439 RVA: 0x00035C95 File Offset: 0x00033E95
			internal void SetValueNode(XmlNodeType type, string value)
			{
				this.type = type;
				this.ClearName();
				this.value = value;
				this.valueStartPos = -1;
			}

			// Token: 0x06000988 RID: 2440 RVA: 0x00035CB2 File Offset: 0x00033EB2
			internal void SetValueNode(XmlNodeType type, char[] chars, int startPos, int len)
			{
				this.type = type;
				this.ClearName();
				this.value = null;
				this.chars = chars;
				this.valueStartPos = startPos;
				this.valueLength = len;
			}

			// Token: 0x06000989 RID: 2441 RVA: 0x00035CDE File Offset: 0x00033EDE
			internal void SetNamedNode(XmlNodeType type, string localName)
			{
				this.SetNamedNode(type, localName, string.Empty, localName);
			}

			// Token: 0x0600098A RID: 2442 RVA: 0x00035CEE File Offset: 0x00033EEE
			internal void SetNamedNode(XmlNodeType type, string localName, string prefix, string nameWPrefix)
			{
				this.type = type;
				this.localName = localName;
				this.prefix = prefix;
				this.nameWPrefix = nameWPrefix;
				this.ns = string.Empty;
				this.value = string.Empty;
				this.valueStartPos = -1;
			}

			// Token: 0x0600098B RID: 2443 RVA: 0x00035D2A File Offset: 0x00033F2A
			internal void SetValue(string value)
			{
				this.valueStartPos = -1;
				this.value = value;
			}

			// Token: 0x0600098C RID: 2444 RVA: 0x00035D3A File Offset: 0x00033F3A
			internal void SetValue(char[] chars, int startPos, int len)
			{
				this.value = null;
				this.chars = chars;
				this.valueStartPos = startPos;
				this.valueLength = len;
			}

			// Token: 0x0600098D RID: 2445 RVA: 0x00035D58 File Offset: 0x00033F58
			internal void OnBufferInvalidated()
			{
				if (this.value == null)
				{
					this.value = new string(this.chars, this.valueStartPos, this.valueLength);
				}
				this.valueStartPos = -1;
			}

			// Token: 0x0600098E RID: 2446 RVA: 0x00035D88 File Offset: 0x00033F88
			internal void CopyTo(int valueOffset, StringBuilder sb)
			{
				if (this.value == null)
				{
					sb.Append(this.chars, this.valueStartPos + valueOffset, this.valueLength - valueOffset);
					return;
				}
				if (valueOffset <= 0)
				{
					sb.Append(this.value);
					return;
				}
				sb.Append(this.value, valueOffset, this.value.Length - valueOffset);
			}

			// Token: 0x0600098F RID: 2447 RVA: 0x00035DE8 File Offset: 0x00033FE8
			internal int CopyTo(int valueOffset, char[] buffer, int offset, int length)
			{
				if (this.value == null)
				{
					int num = this.valueLength - valueOffset;
					if (num > length)
					{
						num = length;
					}
					XmlTextReaderImpl.BlockCopyChars(this.chars, this.valueStartPos + valueOffset, buffer, offset, num);
					return num;
				}
				int num2 = this.value.Length - valueOffset;
				if (num2 > length)
				{
					num2 = length;
				}
				this.value.CopyTo(valueOffset, buffer, offset, num2);
				return num2;
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x00035E4C File Offset: 0x0003404C
			internal int CopyToBinary(IncrementalReadDecoder decoder, int valueOffset)
			{
				if (this.value == null)
				{
					return decoder.Decode(this.chars, this.valueStartPos + valueOffset, this.valueLength - valueOffset);
				}
				return decoder.Decode(this.value, valueOffset, this.value.Length - valueOffset);
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x00035E98 File Offset: 0x00034098
			internal void AdjustLineInfo(int valueOffset, bool isNormalized, ref LineInfo lineInfo)
			{
				if (valueOffset == 0)
				{
					return;
				}
				if (this.valueStartPos != -1)
				{
					XmlTextReaderImpl.AdjustLineInfo(this.chars, this.valueStartPos, this.valueStartPos + valueOffset, isNormalized, ref lineInfo);
					return;
				}
				XmlTextReaderImpl.AdjustLineInfo(this.value, 0, valueOffset, isNormalized, ref lineInfo);
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x00035ED2 File Offset: 0x000340D2
			internal string GetNameWPrefix(XmlNameTable nt)
			{
				if (this.nameWPrefix != null)
				{
					return this.nameWPrefix;
				}
				return this.CreateNameWPrefix(nt);
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x00035EEC File Offset: 0x000340EC
			internal string CreateNameWPrefix(XmlNameTable nt)
			{
				if (this.prefix.Length == 0)
				{
					this.nameWPrefix = this.localName;
				}
				else
				{
					this.nameWPrefix = nt.Add(this.prefix + ":" + this.localName);
				}
				return this.nameWPrefix;
			}

			// Token: 0x06000994 RID: 2452 RVA: 0x00035F3C File Offset: 0x0003413C
			int IComparable.CompareTo(object obj)
			{
				XmlTextReaderImpl.NodeData nodeData = obj as XmlTextReaderImpl.NodeData;
				if (nodeData == null)
				{
					return 1;
				}
				if (!Ref.Equal(this.localName, nodeData.localName))
				{
					return string.CompareOrdinal(this.localName, nodeData.localName);
				}
				if (Ref.Equal(this.ns, nodeData.ns))
				{
					return 0;
				}
				return string.CompareOrdinal(this.ns, nodeData.ns);
			}

			// Token: 0x040009ED RID: 2541
			private static volatile XmlTextReaderImpl.NodeData s_None;

			// Token: 0x040009EE RID: 2542
			internal XmlNodeType type;

			// Token: 0x040009EF RID: 2543
			internal string localName;

			// Token: 0x040009F0 RID: 2544
			internal string prefix;

			// Token: 0x040009F1 RID: 2545
			internal string ns;

			// Token: 0x040009F2 RID: 2546
			internal string nameWPrefix;

			// Token: 0x040009F3 RID: 2547
			private string value;

			// Token: 0x040009F4 RID: 2548
			private char[] chars;

			// Token: 0x040009F5 RID: 2549
			private int valueStartPos;

			// Token: 0x040009F6 RID: 2550
			private int valueLength;

			// Token: 0x040009F7 RID: 2551
			internal LineInfo lineInfo;

			// Token: 0x040009F8 RID: 2552
			internal LineInfo lineInfo2;

			// Token: 0x040009F9 RID: 2553
			internal char quoteChar;

			// Token: 0x040009FA RID: 2554
			internal int depth;

			// Token: 0x040009FB RID: 2555
			private bool isEmptyOrDefault;

			// Token: 0x040009FC RID: 2556
			internal int entityId;

			// Token: 0x040009FD RID: 2557
			internal bool xmlContextPushed;

			// Token: 0x040009FE RID: 2558
			internal XmlTextReaderImpl.NodeData nextAttrValueChunk;

			// Token: 0x040009FF RID: 2559
			internal object schemaType;

			// Token: 0x04000A00 RID: 2560
			internal object typedValue;
		}

		// Token: 0x020000CC RID: 204
		private class DtdDefaultAttributeInfoToNodeDataComparer : IComparer<object>
		{
			// Token: 0x17000179 RID: 377
			// (get) Token: 0x06000995 RID: 2453 RVA: 0x00035FA0 File Offset: 0x000341A0
			internal static IComparer<object> Instance
			{
				get
				{
					return XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer.s_instance;
				}
			}

			// Token: 0x06000996 RID: 2454 RVA: 0x00035FA8 File Offset: 0x000341A8
			public int Compare(object x, object y)
			{
				if (x == null)
				{
					if (y != null)
					{
						return -1;
					}
					return 0;
				}
				else
				{
					if (y == null)
					{
						return 1;
					}
					XmlTextReaderImpl.NodeData nodeData = x as XmlTextReaderImpl.NodeData;
					string localName;
					string prefix;
					if (nodeData != null)
					{
						localName = nodeData.localName;
						prefix = nodeData.prefix;
					}
					else
					{
						IDtdDefaultAttributeInfo dtdDefaultAttributeInfo = x as IDtdDefaultAttributeInfo;
						if (dtdDefaultAttributeInfo == null)
						{
							throw new XmlException("An XML error has occurred.", string.Empty);
						}
						localName = dtdDefaultAttributeInfo.LocalName;
						prefix = dtdDefaultAttributeInfo.Prefix;
					}
					nodeData = (y as XmlTextReaderImpl.NodeData);
					string localName2;
					string prefix2;
					if (nodeData != null)
					{
						localName2 = nodeData.localName;
						prefix2 = nodeData.prefix;
					}
					else
					{
						IDtdDefaultAttributeInfo dtdDefaultAttributeInfo2 = y as IDtdDefaultAttributeInfo;
						if (dtdDefaultAttributeInfo2 == null)
						{
							throw new XmlException("An XML error has occurred.", string.Empty);
						}
						localName2 = dtdDefaultAttributeInfo2.LocalName;
						prefix2 = dtdDefaultAttributeInfo2.Prefix;
					}
					int num = string.Compare(localName, localName2, StringComparison.Ordinal);
					if (num != 0)
					{
						return num;
					}
					return string.Compare(prefix, prefix2, StringComparison.Ordinal);
				}
			}

			// Token: 0x06000997 RID: 2455 RVA: 0x0000216B File Offset: 0x0000036B
			public DtdDefaultAttributeInfoToNodeDataComparer()
			{
			}

			// Token: 0x06000998 RID: 2456 RVA: 0x00036075 File Offset: 0x00034275
			// Note: this type is marked as 'beforefieldinit'.
			static DtdDefaultAttributeInfoToNodeDataComparer()
			{
			}

			// Token: 0x04000A01 RID: 2561
			private static IComparer<object> s_instance = new XmlTextReaderImpl.DtdDefaultAttributeInfoToNodeDataComparer();
		}

		// Token: 0x020000CD RID: 205
		// (Invoke) Token: 0x0600099A RID: 2458
		internal delegate void OnDefaultAttributeUseDelegate(IDtdDefaultAttributeInfo defaultAttribute, XmlTextReaderImpl coreReader);

		// Token: 0x020000CE RID: 206
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_GetValueAsync>d__473 : IAsyncStateMachine
		{
			// Token: 0x0600099D RID: 2461 RVA: 0x00036084 File Offset: 0x00034284
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				string stringValue;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							if (xmlTextReaderImpl.parsingFunction < XmlTextReaderImpl.ParsingFunction.PartialTextValue)
							{
								goto IL_104;
							}
							if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
							{
								awaiter = xmlTextReaderImpl.FinishPartialValueAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_GetValueAsync>d__473>(ref awaiter, ref this);
									return;
								}
								goto IL_8D;
							}
							else
							{
								awaiter = xmlTextReaderImpl.FinishOtherValueIteratorAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_GetValueAsync>d__473>(ref awaiter, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						goto IL_104;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_8D:
					awaiter.GetResult();
					xmlTextReaderImpl.parsingFunction = xmlTextReaderImpl.nextParsingFunction;
					IL_104:
					stringValue = xmlTextReaderImpl.curNode.StringValue;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(stringValue);
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x000361EC File Offset: 0x000343EC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A02 RID: 2562
			public int <>1__state;

			// Token: 0x04000A03 RID: 2563
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000A04 RID: 2564
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A05 RID: 2565
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000CF RID: 207
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishInitUriStringAsync>d__475 : IAsyncStateMachine
		{
			// Token: 0x0600099F RID: 2463 RVA: 0x000361FC File Offset: 0x000343FC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num - 1 <= 1)
						{
							goto IL_F1;
						}
						awaiter = xmlTextReaderImpl.laterInitParam.inputUriResolver.GetEntityAsync(xmlTextReaderImpl.laterInitParam.inputbaseUri, string.Empty, typeof(Stream)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishInitUriStringAsync>d__475>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
					}
					object result = awaiter.GetResult();
					this.<stream>5__2 = (Stream)result;
					if (this.<stream>5__2 == null)
					{
						throw new XmlException("Cannot resolve '{0}'.", xmlTextReaderImpl.laterInitParam.inputUriStr);
					}
					Encoding encoding = null;
					if (xmlTextReaderImpl.laterInitParam.inputContext != null)
					{
						encoding = xmlTextReaderImpl.laterInitParam.inputContext.Encoding;
					}
					IL_F1:
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
						if (num != 1)
						{
							if (num == 2)
							{
								awaiter2 = this.<>u__2;
								this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								this.<>1__state = -1;
								goto IL_21B;
							}
							awaiter2 = xmlTextReaderImpl.InitStreamInputAsync(xmlTextReaderImpl.laterInitParam.inputbaseUri, xmlTextReaderImpl.reportedBaseUri, this.<stream>5__2, null, 0, encoding).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishInitUriStringAsync>d__475>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter2.GetResult();
						xmlTextReaderImpl.reportedEncoding = xmlTextReaderImpl.ps.encoding;
						if (xmlTextReaderImpl.laterInitParam.inputContext == null || !xmlTextReaderImpl.laterInitParam.inputContext.HasDtdInfo)
						{
							goto IL_222;
						}
						awaiter2 = xmlTextReaderImpl.ProcessDtdFromParserContextAsync(xmlTextReaderImpl.laterInitParam.inputContext).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishInitUriStringAsync>d__475>(ref awaiter2, ref this);
							return;
						}
						IL_21B:
						awaiter2.GetResult();
						IL_222:;
					}
					catch
					{
						this.<stream>5__2.Close();
						throw;
					}
					xmlTextReaderImpl.laterInitParam = null;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<stream>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<stream>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060009A0 RID: 2464 RVA: 0x000364B4 File Offset: 0x000346B4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A06 RID: 2566
			public int <>1__state;

			// Token: 0x04000A07 RID: 2567
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000A08 RID: 2568
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A09 RID: 2569
			private Stream <stream>5__2;

			// Token: 0x04000A0A RID: 2570
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A0B RID: 2571
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000D0 RID: 208
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishInitStreamAsync>d__476 : IAsyncStateMachine
		{
			// Token: 0x060009A1 RID: 2465 RVA: 0x000364C4 File Offset: 0x000346C4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_169;
						}
						Encoding encoding = null;
						if (xmlTextReaderImpl.laterInitParam.inputContext != null)
						{
							encoding = xmlTextReaderImpl.laterInitParam.inputContext.Encoding;
						}
						awaiter = xmlTextReaderImpl.InitStreamInputAsync(xmlTextReaderImpl.laterInitParam.inputbaseUri, xmlTextReaderImpl.reportedBaseUri, xmlTextReaderImpl.laterInitParam.inputStream, xmlTextReaderImpl.laterInitParam.inputBytes, xmlTextReaderImpl.laterInitParam.inputByteCount, encoding).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishInitStreamAsync>d__476>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					xmlTextReaderImpl.reportedEncoding = xmlTextReaderImpl.ps.encoding;
					if (xmlTextReaderImpl.laterInitParam.inputContext == null || !xmlTextReaderImpl.laterInitParam.inputContext.HasDtdInfo)
					{
						goto IL_170;
					}
					awaiter = xmlTextReaderImpl.ProcessDtdFromParserContextAsync(xmlTextReaderImpl.laterInitParam.inputContext).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishInitStreamAsync>d__476>(ref awaiter, ref this);
						return;
					}
					IL_169:
					awaiter.GetResult();
					IL_170:
					xmlTextReaderImpl.laterInitParam = null;
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

			// Token: 0x060009A2 RID: 2466 RVA: 0x00036694 File Offset: 0x00034894
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A0C RID: 2572
			public int <>1__state;

			// Token: 0x04000A0D RID: 2573
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000A0E RID: 2574
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A0F RID: 2575
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000D1 RID: 209
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishInitTextReaderAsync>d__477 : IAsyncStateMachine
		{
			// Token: 0x060009A3 RID: 2467 RVA: 0x000366A4 File Offset: 0x000348A4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_122;
						}
						awaiter = xmlTextReaderImpl.InitTextReaderInputAsync(xmlTextReaderImpl.reportedBaseUri, xmlTextReaderImpl.laterInitParam.inputTextReader).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishInitTextReaderAsync>d__477>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					xmlTextReaderImpl.reportedEncoding = xmlTextReaderImpl.ps.encoding;
					if (xmlTextReaderImpl.laterInitParam.inputContext == null || !xmlTextReaderImpl.laterInitParam.inputContext.HasDtdInfo)
					{
						goto IL_129;
					}
					awaiter = xmlTextReaderImpl.ProcessDtdFromParserContextAsync(xmlTextReaderImpl.laterInitParam.inputContext).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishInitTextReaderAsync>d__477>(ref awaiter, ref this);
						return;
					}
					IL_122:
					awaiter.GetResult();
					IL_129:
					xmlTextReaderImpl.laterInitParam = null;
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

			// Token: 0x060009A4 RID: 2468 RVA: 0x0003682C File Offset: 0x00034A2C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A10 RID: 2576
			public int <>1__state;

			// Token: 0x04000A11 RID: 2577
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000A12 RID: 2578
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A13 RID: 2579
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000D2 RID: 210
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ReadAsync_SwitchToInteractiveXmlDecl>d__480 : IAsyncStateMachine
		{
			// Token: 0x060009A5 RID: 2469 RVA: 0x0003683C File Offset: 0x00034A3C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result2;
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
							goto IL_E0;
						}
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ReadAsync_SwitchToInteractiveXmlDecl>d__480>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					bool result = awaiter.GetResult();
					awaiter = xmlTextReaderImpl.ReadAsync_SwitchToInteractiveXmlDecl_Helper(result).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ReadAsync_SwitchToInteractiveXmlDecl>d__480>(ref awaiter, ref this);
						return;
					}
					IL_E0:
					result2 = awaiter.GetResult();
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

			// Token: 0x060009A6 RID: 2470 RVA: 0x00036970 File Offset: 0x00034B70
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A14 RID: 2580
			public int <>1__state;

			// Token: 0x04000A15 RID: 2581
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000A16 RID: 2582
			public Task<bool> task;

			// Token: 0x04000A17 RID: 2583
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A18 RID: 2584
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000D3 RID: 211
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SkipAsync>d__482 : IAsyncStateMachine
		{
			// Token: 0x060009A7 RID: 2471 RVA: 0x00036980 File Offset: 0x00034B80
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
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
						goto IL_167;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1D1;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_238;
					case 4:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2E7;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_37B;
					default:
					{
						xmlTextReaderImpl.CheckAsyncCall();
						if (xmlTextReaderImpl.readState != ReadState.Interactive)
						{
							goto IL_39E;
						}
						if (xmlTextReaderImpl.InAttributeValueIterator)
						{
							xmlTextReaderImpl.FinishAttributeValueIterator();
							xmlTextReaderImpl.curNode = xmlTextReaderImpl.nodes[xmlTextReaderImpl.index];
							goto IL_23F;
						}
						XmlTextReaderImpl.ParsingFunction parsingFunction = xmlTextReaderImpl.parsingFunction;
						if (parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
						{
							xmlTextReaderImpl.FinishIncrementalRead();
							goto IL_23F;
						}
						switch (parsingFunction)
						{
						case XmlTextReaderImpl.ParsingFunction.PartialTextValue:
							awaiter = xmlTextReaderImpl.SkipPartialTextValueAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipAsync>d__482>(ref awaiter, ref this);
								return;
							}
							break;
						case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
							goto IL_23F;
						case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
							awaiter = xmlTextReaderImpl.FinishReadValueChunkAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipAsync>d__482>(ref awaiter, ref this);
								return;
							}
							goto IL_167;
						case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
							awaiter = xmlTextReaderImpl.FinishReadContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipAsync>d__482>(ref awaiter, ref this);
								return;
							}
							goto IL_1D1;
						case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
							awaiter = xmlTextReaderImpl.FinishReadElementContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 3;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipAsync>d__482>(ref awaiter, ref this);
								return;
							}
							goto IL_238;
						default:
							goto IL_23F;
						}
						break;
					}
					}
					awaiter.GetResult();
					goto IL_23F;
					IL_167:
					awaiter.GetResult();
					goto IL_23F;
					IL_1D1:
					awaiter.GetResult();
					goto IL_23F;
					IL_238:
					awaiter.GetResult();
					IL_23F:
					XmlNodeType type = xmlTextReaderImpl.curNode.type;
					if (type != XmlNodeType.Element)
					{
						if (type != XmlNodeType.Attribute)
						{
							goto IL_318;
						}
						xmlTextReaderImpl.outerReader.MoveToElement();
					}
					if (xmlTextReaderImpl.curNode.IsEmptyElement)
					{
						goto IL_318;
					}
					this.<initialDepth>5__2 = xmlTextReaderImpl.index;
					xmlTextReaderImpl.parsingMode = XmlTextReaderImpl.ParsingMode.SkipContent;
					IL_281:
					awaiter2 = xmlTextReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipAsync>d__482>(ref awaiter2, ref this);
						return;
					}
					IL_2E7:
					if (awaiter2.GetResult() && xmlTextReaderImpl.index > this.<initialDepth>5__2)
					{
						goto IL_281;
					}
					xmlTextReaderImpl.parsingMode = XmlTextReaderImpl.ParsingMode.Full;
					IL_318:
					awaiter2 = xmlTextReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipAsync>d__482>(ref awaiter2, ref this);
						return;
					}
					IL_37B:
					awaiter2.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_39E:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060009A8 RID: 2472 RVA: 0x00036D5C File Offset: 0x00034F5C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A19 RID: 2585
			public int <>1__state;

			// Token: 0x04000A1A RID: 2586
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000A1B RID: 2587
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A1C RID: 2588
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A1D RID: 2589
			private int <initialDepth>5__2;

			// Token: 0x04000A1E RID: 2590
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000D4 RID: 212
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBase64_AsyncHelper>d__483 : IAsyncStateMachine
		{
			// Token: 0x060009A9 RID: 2473 RVA: 0x00036D6C File Offset: 0x00034F6C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_108;
						}
						awaiter2 = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadContentAsBase64_AsyncHelper>d__483>(ref awaiter2, ref this);
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
					if (!this.task.Result)
					{
						result = 0;
						goto IL_12B;
					}
					xmlTextReaderImpl.InitBase64Decoder();
					awaiter = xmlTextReaderImpl.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadContentAsBase64_AsyncHelper>d__483>(ref awaiter, ref this);
						return;
					}
					IL_108:
					result = awaiter.GetResult();
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

			// Token: 0x060009AA RID: 2474 RVA: 0x00036ED4 File Offset: 0x000350D4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A1F RID: 2591
			public int <>1__state;

			// Token: 0x04000A20 RID: 2592
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000A21 RID: 2593
			public Task<bool> task;

			// Token: 0x04000A22 RID: 2594
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A23 RID: 2595
			public byte[] buffer;

			// Token: 0x04000A24 RID: 2596
			public int index;

			// Token: 0x04000A25 RID: 2597
			public int count;

			// Token: 0x04000A26 RID: 2598
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A27 RID: 2599
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000D5 RID: 213
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBinHexAsync>d__485 : IAsyncStateMachine
		{
			// Token: 0x060009AB RID: 2475 RVA: 0x00036EE4 File Offset: 0x000350E4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C7;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_24A;
					default:
						xmlTextReaderImpl.CheckAsyncCall();
						if (this.buffer == null)
						{
							throw new ArgumentNullException("buffer");
						}
						if (this.count < 0)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (this.index < 0)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.buffer.Length - this.index < this.count)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
						{
							if (xmlTextReaderImpl.incReadDecoder != xmlTextReaderImpl.binHexDecoder)
							{
								goto IL_1D7;
							}
							awaiter = xmlTextReaderImpl.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadContentAsBinHexAsync>d__485>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							if (xmlTextReaderImpl.readState != ReadState.Interactive)
							{
								result = 0;
								goto IL_26D;
							}
							if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
							{
								throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
							}
							if (!XmlReader.CanReadContentAs(xmlTextReaderImpl.curNode.type))
							{
								throw xmlTextReaderImpl.CreateReadContentAsException("ReadContentAsBinHex");
							}
							awaiter2 = xmlTextReaderImpl.InitReadContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadContentAsBinHexAsync>d__485>(ref awaiter2, ref this);
								return;
							}
							goto IL_1C7;
						}
						break;
					}
					result = awaiter.GetResult();
					goto IL_26D;
					IL_1C7:
					if (!awaiter2.GetResult())
					{
						result = 0;
						goto IL_26D;
					}
					IL_1D7:
					xmlTextReaderImpl.InitBinHexDecoder();
					awaiter = xmlTextReaderImpl.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadContentAsBinHexAsync>d__485>(ref awaiter, ref this);
						return;
					}
					IL_24A:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_26D:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009AC RID: 2476 RVA: 0x00037190 File Offset: 0x00035390
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A28 RID: 2600
			public int <>1__state;

			// Token: 0x04000A29 RID: 2601
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000A2A RID: 2602
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A2B RID: 2603
			public byte[] buffer;

			// Token: 0x04000A2C RID: 2604
			public int count;

			// Token: 0x04000A2D RID: 2605
			public int index;

			// Token: 0x04000A2E RID: 2606
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A2F RID: 2607
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000D6 RID: 214
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBase64Async_Helper>d__486 : IAsyncStateMachine
		{
			// Token: 0x060009AD RID: 2477 RVA: 0x000371A0 File Offset: 0x000353A0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_108;
						}
						awaiter2 = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadElementContentAsBase64Async_Helper>d__486>(ref awaiter2, ref this);
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
					if (!this.task.Result)
					{
						result = 0;
						goto IL_12B;
					}
					xmlTextReaderImpl.InitBase64Decoder();
					awaiter = xmlTextReaderImpl.ReadElementContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadElementContentAsBase64Async_Helper>d__486>(ref awaiter, ref this);
						return;
					}
					IL_108:
					result = awaiter.GetResult();
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

			// Token: 0x060009AE RID: 2478 RVA: 0x00037308 File Offset: 0x00035508
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A30 RID: 2608
			public int <>1__state;

			// Token: 0x04000A31 RID: 2609
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000A32 RID: 2610
			public Task<bool> task;

			// Token: 0x04000A33 RID: 2611
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A34 RID: 2612
			public byte[] buffer;

			// Token: 0x04000A35 RID: 2613
			public int index;

			// Token: 0x04000A36 RID: 2614
			public int count;

			// Token: 0x04000A37 RID: 2615
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A38 RID: 2616
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000D7 RID: 215
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBinHexAsync>d__488 : IAsyncStateMachine
		{
			// Token: 0x060009AF RID: 2479 RVA: 0x00037318 File Offset: 0x00035518
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C3;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_246;
					default:
						xmlTextReaderImpl.CheckAsyncCall();
						if (this.buffer == null)
						{
							throw new ArgumentNullException("buffer");
						}
						if (this.count < 0)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (this.index < 0)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.buffer.Length - this.index < this.count)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary)
						{
							if (xmlTextReaderImpl.incReadDecoder != xmlTextReaderImpl.binHexDecoder)
							{
								goto IL_1D3;
							}
							awaiter = xmlTextReaderImpl.ReadElementContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadElementContentAsBinHexAsync>d__488>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							if (xmlTextReaderImpl.readState != ReadState.Interactive)
							{
								result = 0;
								goto IL_269;
							}
							if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary)
							{
								throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadElementContentAsBase64 and ReadElementContentAsBinHex."));
							}
							if (xmlTextReaderImpl.curNode.type != XmlNodeType.Element)
							{
								throw xmlTextReaderImpl.CreateReadElementContentAsException("ReadElementContentAsBinHex");
							}
							awaiter2 = xmlTextReaderImpl.InitReadElementContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadElementContentAsBinHexAsync>d__488>(ref awaiter2, ref this);
								return;
							}
							goto IL_1C3;
						}
						break;
					}
					result = awaiter.GetResult();
					goto IL_269;
					IL_1C3:
					if (!awaiter2.GetResult())
					{
						result = 0;
						goto IL_269;
					}
					IL_1D3:
					xmlTextReaderImpl.InitBinHexDecoder();
					awaiter = xmlTextReaderImpl.ReadElementContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadElementContentAsBinHexAsync>d__488>(ref awaiter, ref this);
						return;
					}
					IL_246:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_269:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009B0 RID: 2480 RVA: 0x000375C0 File Offset: 0x000357C0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A39 RID: 2617
			public int <>1__state;

			// Token: 0x04000A3A RID: 2618
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000A3B RID: 2619
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A3C RID: 2620
			public byte[] buffer;

			// Token: 0x04000A3D RID: 2621
			public int count;

			// Token: 0x04000A3E RID: 2622
			public int index;

			// Token: 0x04000A3F RID: 2623
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A40 RID: 2624
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000D8 RID: 216
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadValueChunkAsync>d__489 : IAsyncStateMachine
		{
			// Token: 0x060009B1 RID: 2481 RVA: 0x000375D0 File Offset: 0x000357D0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					bool flag;
					int num4;
					int num5;
					ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlTextReaderImpl.CheckAsyncCall();
						if (!XmlReader.HasValueInternal(xmlTextReaderImpl.curNode.type))
						{
							throw new InvalidOperationException(Res.GetString("The ReadValueAsChunk method is not supported on node type {0}.", new object[]
							{
								xmlTextReaderImpl.curNode.type
							}));
						}
						if (this.buffer == null)
						{
							throw new ArgumentNullException("buffer");
						}
						if (this.count < 0)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (this.index < 0)
						{
							throw new ArgumentOutOfRangeException("index");
						}
						if (this.buffer.Length - this.index < this.count)
						{
							throw new ArgumentOutOfRangeException("count");
						}
						if (xmlTextReaderImpl.parsingFunction != XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
						{
							if (xmlTextReaderImpl.readState != ReadState.Interactive)
							{
								result = 0;
								goto IL_3B1;
							}
							if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.PartialTextValue)
							{
								xmlTextReaderImpl.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue;
							}
							else
							{
								xmlTextReaderImpl.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue;
								xmlTextReaderImpl.nextNextParsingFunction = xmlTextReaderImpl.nextParsingFunction;
								xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.parsingFunction;
							}
							xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.InReadValueChunk;
							xmlTextReaderImpl.readValueOffset = 0;
						}
						if (this.count == 0)
						{
							result = 0;
							goto IL_3B1;
						}
						this.<readCount>5__2 = 0;
						int num2 = xmlTextReaderImpl.curNode.CopyTo(xmlTextReaderImpl.readValueOffset, this.buffer, this.index + this.<readCount>5__2, this.count - this.<readCount>5__2);
						this.<readCount>5__2 += num2;
						xmlTextReaderImpl.readValueOffset += num2;
						if (this.<readCount>5__2 == this.count)
						{
							if (XmlCharType.IsHighSurrogate((int)this.buffer[this.index + this.count - 1]))
							{
								int num3 = this.<readCount>5__2;
								this.<readCount>5__2 = num3 - 1;
								xmlTextReaderImpl.readValueOffset--;
								if (this.<readCount>5__2 == 0)
								{
									xmlTextReaderImpl.Throw("The buffer is not large enough to fit a surrogate pair. Please provide a buffer of size at least 2 characters.");
								}
							}
							result = this.<readCount>5__2;
							goto IL_3B1;
						}
						if (xmlTextReaderImpl.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
						{
							xmlTextReaderImpl.curNode.SetValue(string.Empty);
							flag = false;
							num4 = 0;
							num5 = 0;
							goto IL_2F0;
						}
						goto IL_38F;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_26E:
					Tuple<int, int, int, bool> result2 = awaiter.GetResult();
					num4 = result2.Item1;
					num5 = result2.Item2;
					int outOrChars = result2.Item3;
					flag = result2.Item4;
					int num6 = this.count - this.<readCount>5__2;
					if (num6 > num5 - num4)
					{
						num6 = num5 - num4;
					}
					XmlTextReaderImpl.BlockCopyChars(xmlTextReaderImpl.ps.chars, num4, this.buffer, this.index + this.<readCount>5__2, num6);
					this.<readCount>5__2 += num6;
					num4 += num6;
					IL_2F0:
					if (this.<readCount>5__2 >= this.count || flag)
					{
						xmlTextReaderImpl.incReadState = (flag ? XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue : XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue);
						if (this.<readCount>5__2 == this.count && XmlCharType.IsHighSurrogate((int)this.buffer[this.index + this.count - 1]))
						{
							int num3 = this.<readCount>5__2;
							this.<readCount>5__2 = num3 - 1;
							num4--;
							if (this.<readCount>5__2 == 0)
							{
								xmlTextReaderImpl.Throw("The buffer is not large enough to fit a surrogate pair. Please provide a buffer of size at least 2 characters.");
							}
						}
						xmlTextReaderImpl.readValueOffset = 0;
						xmlTextReaderImpl.curNode.SetValue(xmlTextReaderImpl.ps.chars, num4, num5 - num4);
					}
					else
					{
						outOrChars = 0;
						awaiter = xmlTextReaderImpl.ParseTextAsync(outOrChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadValueChunkAsync>d__489>(ref awaiter, ref this);
							return;
						}
						goto IL_26E;
					}
					IL_38F:
					result = this.<readCount>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3B1:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009B2 RID: 2482 RVA: 0x000379C0 File Offset: 0x00035BC0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A41 RID: 2625
			public int <>1__state;

			// Token: 0x04000A42 RID: 2626
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000A43 RID: 2627
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A44 RID: 2628
			public char[] buffer;

			// Token: 0x04000A45 RID: 2629
			public int count;

			// Token: 0x04000A46 RID: 2630
			public int index;

			// Token: 0x04000A47 RID: 2631
			private int <readCount>5__2;

			// Token: 0x04000A48 RID: 2632
			private ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000D9 RID: 217
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DtdParserProxy_ParseNumericCharRefAsync>d__491 : IAsyncStateMachine
		{
			// Token: 0x060009B3 RID: 2483 RVA: 0x000379D0 File Offset: 0x00035BD0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int item;
				try
				{
					ConfiguredTaskAwaitable<Tuple<XmlTextReaderImpl.EntityType, int>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlTextReaderImpl.CheckAsyncCall();
						awaiter = xmlTextReaderImpl.ParseNumericCharRefAsync(true, this.internalSubsetBuilder).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<XmlTextReaderImpl.EntityType, int>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_ParseNumericCharRefAsync>d__491>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<XmlTextReaderImpl.EntityType, int>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					item = awaiter.GetResult().Item2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(item);
			}

			// Token: 0x060009B4 RID: 2484 RVA: 0x00037AA4 File Offset: 0x00035CA4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A49 RID: 2633
			public int <>1__state;

			// Token: 0x04000A4A RID: 2634
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000A4B RID: 2635
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A4C RID: 2636
			public StringBuilder internalSubsetBuilder;

			// Token: 0x04000A4D RID: 2637
			private ConfiguredTaskAwaitable<Tuple<XmlTextReaderImpl.EntityType, int>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000DA RID: 218
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DtdParserProxy_ParsePIAsync>d__493 : IAsyncStateMachine
		{
			// Token: 0x060009B5 RID: 2485 RVA: 0x00037AB4 File Offset: 0x00035CB4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							xmlTextReaderImpl.CheckAsyncCall();
							if (this.sb == null)
							{
								this.<pm>5__2 = xmlTextReaderImpl.parsingMode;
								xmlTextReaderImpl.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
								awaiter = xmlTextReaderImpl.ParsePIAsync(null).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_ParsePIAsync>d__493>(ref awaiter, ref this);
									return;
								}
								goto IL_9A;
							}
							else
							{
								awaiter = xmlTextReaderImpl.ParsePIAsync(this.sb).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_ParsePIAsync>d__493>(ref awaiter, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						goto IL_118;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_9A:
					awaiter.GetResult();
					xmlTextReaderImpl.parsingMode = this.<pm>5__2;
					IL_118:;
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

			// Token: 0x060009B6 RID: 2486 RVA: 0x00037C24 File Offset: 0x00035E24
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A4E RID: 2638
			public int <>1__state;

			// Token: 0x04000A4F RID: 2639
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000A50 RID: 2640
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A51 RID: 2641
			public StringBuilder sb;

			// Token: 0x04000A52 RID: 2642
			private XmlTextReaderImpl.ParsingMode <pm>5__2;

			// Token: 0x04000A53 RID: 2643
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000DB RID: 219
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DtdParserProxy_ParseCommentAsync>d__494 : IAsyncStateMachine
		{
			// Token: 0x060009B7 RID: 2487 RVA: 0x00037C34 File Offset: 0x00035E34
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					if (num > 1)
					{
						xmlTextReaderImpl.CheckAsyncCall();
					}
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							if (num != 1)
							{
								if (this.sb == null)
								{
									this.<savedParsingMode>5__2 = xmlTextReaderImpl.parsingMode;
									xmlTextReaderImpl.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
									awaiter = xmlTextReaderImpl.ParseCDataOrCommentAsync(XmlNodeType.Comment).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 0;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_ParseCommentAsync>d__494>(ref awaiter, ref this);
										return;
									}
									goto IL_9F;
								}
								else
								{
									this.<originalCurNode>5__3 = xmlTextReaderImpl.curNode;
									xmlTextReaderImpl.curNode = xmlTextReaderImpl.AddNode(xmlTextReaderImpl.index + xmlTextReaderImpl.attrCount + 1, xmlTextReaderImpl.index);
									awaiter = xmlTextReaderImpl.ParseCDataOrCommentAsync(XmlNodeType.Comment).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 1;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_ParseCommentAsync>d__494>(ref awaiter, ref this);
										return;
									}
								}
							}
							else
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								this.<>1__state = -1;
							}
							awaiter.GetResult();
							xmlTextReaderImpl.curNode.CopyTo(0, this.sb);
							xmlTextReaderImpl.curNode = this.<originalCurNode>5__3;
							this.<originalCurNode>5__3 = null;
							goto IL_16E;
						}
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						IL_9F:
						awaiter.GetResult();
						xmlTextReaderImpl.parsingMode = this.<savedParsingMode>5__2;
						IL_16E:;
					}
					catch (XmlException ex)
					{
						if (!(ex.ResString == "Unexpected end of file while parsing {0} has occurred.") || xmlTextReaderImpl.ps.entity == null)
						{
							throw;
						}
						xmlTextReaderImpl.SendValidationEvent(XmlSeverityType.Error, "The parameter entity replacement text must nest properly within markup declarations.", null, xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
					}
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

			// Token: 0x060009B8 RID: 2488 RVA: 0x00037E5C File Offset: 0x0003605C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A54 RID: 2644
			public int <>1__state;

			// Token: 0x04000A55 RID: 2645
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000A56 RID: 2646
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A57 RID: 2647
			public StringBuilder sb;

			// Token: 0x04000A58 RID: 2648
			private XmlTextReaderImpl.ParsingMode <savedParsingMode>5__2;

			// Token: 0x04000A59 RID: 2649
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A5A RID: 2650
			private XmlTextReaderImpl.NodeData <originalCurNode>5__3;
		}

		// Token: 0x020000DC RID: 220
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DtdParserProxy_PushEntityAsync>d__495 : IAsyncStateMachine
		{
			// Token: 0x060009B9 RID: 2489 RVA: 0x00037E6C File Offset: 0x0003606C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, bool> result;
				try
				{
					bool item;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlTextReaderImpl.CheckAsyncCall();
						if (!this.entity.IsExternal)
						{
							xmlTextReaderImpl.PushInternalEntity(this.entity);
							item = true;
							goto IL_B8;
						}
						if (xmlTextReaderImpl.IsResolverNull)
						{
							result = new Tuple<int, bool>(-1, false);
							goto IL_E5;
						}
						awaiter = xmlTextReaderImpl.PushExternalEntityAsync(this.entity).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_PushEntityAsync>d__495>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					item = awaiter.GetResult();
					IL_B8:
					result = new Tuple<int, bool>(xmlTextReaderImpl.ps.entityId, item);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E5:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009BA RID: 2490 RVA: 0x00037F84 File Offset: 0x00036184
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A5B RID: 2651
			public int <>1__state;

			// Token: 0x04000A5C RID: 2652
			public AsyncTaskMethodBuilder<Tuple<int, bool>> <>t__builder;

			// Token: 0x04000A5D RID: 2653
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A5E RID: 2654
			public IDtdEntityInfo entity;

			// Token: 0x04000A5F RID: 2655
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000DD RID: 221
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <DtdParserProxy_PushExternalSubsetAsync>d__496 : IAsyncStateMachine
		{
			// Token: 0x060009BB RID: 2491 RVA: 0x00037F94 File Offset: 0x00036194
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_18C;
					case 2:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1F3;
					default:
						xmlTextReaderImpl.CheckAsyncCall();
						if (xmlTextReaderImpl.IsResolverNull)
						{
							result = false;
							goto IL_22A;
						}
						if (xmlTextReaderImpl.ps.baseUri == null && !string.IsNullOrEmpty(xmlTextReaderImpl.ps.baseUriStr))
						{
							xmlTextReaderImpl.ps.baseUri = xmlTextReaderImpl.xmlResolver.ResolveUri(null, xmlTextReaderImpl.ps.baseUriStr);
						}
						awaiter = xmlTextReaderImpl.PushExternalEntityOrSubsetAsync(this.publicId, this.systemId, xmlTextReaderImpl.ps.baseUri, null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_PushExternalSubsetAsync>d__496>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					xmlTextReaderImpl.ps.entity = null;
					xmlTextReaderImpl.ps.entityId = 0;
					this.<initialPos>5__2 = xmlTextReaderImpl.ps.charPos;
					if (!xmlTextReaderImpl.v1Compat)
					{
						goto IL_194;
					}
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_PushExternalSubsetAsync>d__496>(ref awaiter2, ref this);
						return;
					}
					IL_18C:
					awaiter2.GetResult();
					IL_194:
					awaiter3 = xmlTextReaderImpl.ParseXmlDeclarationAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<DtdParserProxy_PushExternalSubsetAsync>d__496>(ref awaiter3, ref this);
						return;
					}
					IL_1F3:
					if (!awaiter3.GetResult())
					{
						xmlTextReaderImpl.ps.charPos = this.<initialPos>5__2;
					}
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_22A:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009BC RID: 2492 RVA: 0x000381FC File Offset: 0x000363FC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A60 RID: 2656
			public int <>1__state;

			// Token: 0x04000A61 RID: 2657
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000A62 RID: 2658
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A63 RID: 2659
			public string publicId;

			// Token: 0x04000A64 RID: 2660
			public string systemId;

			// Token: 0x04000A65 RID: 2661
			private int <initialPos>5__2;

			// Token: 0x04000A66 RID: 2662
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A67 RID: 2663
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000A68 RID: 2664
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020000DE RID: 222
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InitStreamInputAsync>d__499 : IAsyncStateMachine
		{
			// Token: 0x060009BD RID: 2493 RVA: 0x0003820C File Offset: 0x0003640C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							xmlTextReaderImpl.ps.stream = this.stream;
							xmlTextReaderImpl.ps.baseUri = this.baseUri;
							xmlTextReaderImpl.ps.baseUriStr = this.baseUriStr;
							int num2;
							if (this.bytes != null)
							{
								xmlTextReaderImpl.ps.bytes = this.bytes;
								xmlTextReaderImpl.ps.bytesUsed = this.byteCount;
								num2 = xmlTextReaderImpl.ps.bytes.Length;
							}
							else
							{
								if (xmlTextReaderImpl.laterInitParam != null && xmlTextReaderImpl.laterInitParam.useAsync)
								{
									num2 = 65536;
								}
								else
								{
									num2 = XmlReader.CalcBufferSize(this.stream);
								}
								if (xmlTextReaderImpl.ps.bytes == null || xmlTextReaderImpl.ps.bytes.Length < num2)
								{
									xmlTextReaderImpl.ps.bytes = new byte[num2];
								}
							}
							if (xmlTextReaderImpl.ps.chars == null || xmlTextReaderImpl.ps.chars.Length < num2 + 1)
							{
								xmlTextReaderImpl.ps.chars = new char[num2 + 1];
							}
							xmlTextReaderImpl.ps.bytePos = 0;
							goto IL_1E3;
						}
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_31C;
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IL_1B7:
					int result = awaiter.GetResult();
					if (result == 0)
					{
						xmlTextReaderImpl.ps.isStreamEof = true;
						goto IL_210;
					}
					XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
					xmlTextReaderImpl2.ps.bytesUsed = xmlTextReaderImpl2.ps.bytesUsed + result;
					IL_1E3:
					if (xmlTextReaderImpl.ps.bytesUsed < 4 && xmlTextReaderImpl.ps.bytes.Length - xmlTextReaderImpl.ps.bytesUsed > 0)
					{
						awaiter = this.stream.ReadAsync(xmlTextReaderImpl.ps.bytes, xmlTextReaderImpl.ps.bytesUsed, xmlTextReaderImpl.ps.bytes.Length - xmlTextReaderImpl.ps.bytesUsed).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<InitStreamInputAsync>d__499>(ref awaiter, ref this);
							return;
						}
						goto IL_1B7;
					}
					IL_210:
					if (this.encoding == null)
					{
						this.encoding = xmlTextReaderImpl.DetectEncoding();
					}
					xmlTextReaderImpl.SetupEncoding(this.encoding);
					byte[] preamble = xmlTextReaderImpl.ps.encoding.GetPreamble();
					int num3 = preamble.Length;
					int num4 = 0;
					while (num4 < num3 && num4 < xmlTextReaderImpl.ps.bytesUsed && xmlTextReaderImpl.ps.bytes[num4] == preamble[num4])
					{
						num4++;
					}
					if (num4 == num3)
					{
						xmlTextReaderImpl.ps.bytePos = num3;
					}
					xmlTextReaderImpl.documentStartBytePos = xmlTextReaderImpl.ps.bytePos;
					xmlTextReaderImpl.ps.eolNormalized = !xmlTextReaderImpl.normalize;
					xmlTextReaderImpl.ps.appendMode = true;
					awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<InitStreamInputAsync>d__499>(ref awaiter, ref this);
						return;
					}
					IL_31C:
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

			// Token: 0x060009BE RID: 2494 RVA: 0x00038588 File Offset: 0x00036788
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A69 RID: 2665
			public int <>1__state;

			// Token: 0x04000A6A RID: 2666
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000A6B RID: 2667
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A6C RID: 2668
			public Stream stream;

			// Token: 0x04000A6D RID: 2669
			public Uri baseUri;

			// Token: 0x04000A6E RID: 2670
			public string baseUriStr;

			// Token: 0x04000A6F RID: 2671
			public byte[] bytes;

			// Token: 0x04000A70 RID: 2672
			public int byteCount;

			// Token: 0x04000A71 RID: 2673
			public Encoding encoding;

			// Token: 0x04000A72 RID: 2674
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000DF RID: 223
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadDataAsync>d__505 : IAsyncStateMachine
		{
			// Token: 0x060009BF RID: 2495 RVA: 0x00038598 File Offset: 0x00036798
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_50E;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_5C7;
					default:
						if (xmlTextReaderImpl.ps.isEof)
						{
							result = 0;
							goto IL_657;
						}
						if (xmlTextReaderImpl.ps.appendMode)
						{
							if (xmlTextReaderImpl.ps.charsUsed == xmlTextReaderImpl.ps.chars.Length - 1)
							{
								for (int i = 0; i < xmlTextReaderImpl.attrCount; i++)
								{
									xmlTextReaderImpl.nodes[xmlTextReaderImpl.index + i + 1].OnBufferInvalidated();
								}
								char[] array = new char[xmlTextReaderImpl.ps.chars.Length * 2];
								XmlTextReaderImpl.BlockCopyChars(xmlTextReaderImpl.ps.chars, 0, array, 0, xmlTextReaderImpl.ps.chars.Length);
								xmlTextReaderImpl.ps.chars = array;
							}
							if (xmlTextReaderImpl.ps.stream != null && xmlTextReaderImpl.ps.bytesUsed - xmlTextReaderImpl.ps.bytePos < 6 && xmlTextReaderImpl.ps.bytes.Length - xmlTextReaderImpl.ps.bytesUsed < 6)
							{
								byte[] array2 = new byte[xmlTextReaderImpl.ps.bytes.Length * 2];
								XmlTextReaderImpl.BlockCopy(xmlTextReaderImpl.ps.bytes, 0, array2, 0, xmlTextReaderImpl.ps.bytesUsed);
								xmlTextReaderImpl.ps.bytes = array2;
							}
							this.<charsRead>5__2 = xmlTextReaderImpl.ps.chars.Length - xmlTextReaderImpl.ps.charsUsed - 1;
							if (this.<charsRead>5__2 > 80)
							{
								this.<charsRead>5__2 = 80;
							}
						}
						else
						{
							int num2 = xmlTextReaderImpl.ps.chars.Length;
							if (num2 - xmlTextReaderImpl.ps.charsUsed <= num2 / 2)
							{
								for (int j = 0; j < xmlTextReaderImpl.attrCount; j++)
								{
									xmlTextReaderImpl.nodes[xmlTextReaderImpl.index + j + 1].OnBufferInvalidated();
								}
								int num3 = xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos;
								if (num3 < num2 - 1)
								{
									xmlTextReaderImpl.ps.lineStartPos = xmlTextReaderImpl.ps.lineStartPos - xmlTextReaderImpl.ps.charPos;
									if (num3 > 0)
									{
										XmlTextReaderImpl.BlockCopyChars(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, xmlTextReaderImpl.ps.chars, 0, num3);
									}
									xmlTextReaderImpl.ps.charPos = 0;
									xmlTextReaderImpl.ps.charsUsed = num3;
								}
								else
								{
									char[] array3 = new char[xmlTextReaderImpl.ps.chars.Length * 2];
									XmlTextReaderImpl.BlockCopyChars(xmlTextReaderImpl.ps.chars, 0, array3, 0, xmlTextReaderImpl.ps.chars.Length);
									xmlTextReaderImpl.ps.chars = array3;
								}
							}
							if (xmlTextReaderImpl.ps.stream != null)
							{
								int num4 = xmlTextReaderImpl.ps.bytesUsed - xmlTextReaderImpl.ps.bytePos;
								if (num4 <= 128)
								{
									if (num4 == 0)
									{
										xmlTextReaderImpl.ps.bytesUsed = 0;
									}
									else
									{
										XmlTextReaderImpl.BlockCopy(xmlTextReaderImpl.ps.bytes, xmlTextReaderImpl.ps.bytePos, xmlTextReaderImpl.ps.bytes, 0, num4);
										xmlTextReaderImpl.ps.bytesUsed = num4;
									}
									xmlTextReaderImpl.ps.bytePos = 0;
								}
							}
							this.<charsRead>5__2 = xmlTextReaderImpl.ps.chars.Length - xmlTextReaderImpl.ps.charsUsed - 1;
						}
						if (xmlTextReaderImpl.ps.stream != null)
						{
							if (xmlTextReaderImpl.ps.isStreamEof || xmlTextReaderImpl.ps.bytePos != xmlTextReaderImpl.ps.bytesUsed || xmlTextReaderImpl.ps.bytes.Length - xmlTextReaderImpl.ps.bytesUsed <= 0)
							{
								goto IL_471;
							}
							awaiter = xmlTextReaderImpl.ps.stream.ReadAsync(xmlTextReaderImpl.ps.bytes, xmlTextReaderImpl.ps.bytesUsed, xmlTextReaderImpl.ps.bytes.Length - xmlTextReaderImpl.ps.bytesUsed).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadDataAsync>d__505>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							if (xmlTextReaderImpl.ps.textReader == null)
							{
								this.<charsRead>5__2 = 0;
								goto IL_5F6;
							}
							awaiter = xmlTextReaderImpl.ps.textReader.ReadAsync(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charsUsed, xmlTextReaderImpl.ps.chars.Length - xmlTextReaderImpl.ps.charsUsed - 1).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadDataAsync>d__505>(ref awaiter, ref this);
								return;
							}
							goto IL_5C7;
						}
						break;
					}
					int result2 = awaiter.GetResult();
					if (result2 == 0)
					{
						xmlTextReaderImpl.ps.isStreamEof = true;
					}
					XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
					xmlTextReaderImpl2.ps.bytesUsed = xmlTextReaderImpl2.ps.bytesUsed + result2;
					IL_471:
					int bytePos = xmlTextReaderImpl.ps.bytePos;
					this.<charsRead>5__2 = xmlTextReaderImpl.GetChars(this.<charsRead>5__2);
					if (this.<charsRead>5__2 != 0 || xmlTextReaderImpl.ps.bytePos == bytePos)
					{
						goto IL_5F6;
					}
					awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadDataAsync>d__505>(ref awaiter, ref this);
						return;
					}
					IL_50E:
					result = awaiter.GetResult();
					goto IL_657;
					IL_5C7:
					int result3 = awaiter.GetResult();
					this.<charsRead>5__2 = result3;
					XmlTextReaderImpl xmlTextReaderImpl3 = xmlTextReaderImpl;
					xmlTextReaderImpl3.ps.charsUsed = xmlTextReaderImpl3.ps.charsUsed + this.<charsRead>5__2;
					IL_5F6:
					xmlTextReaderImpl.RegisterConsumedCharacters((long)this.<charsRead>5__2, xmlTextReaderImpl.InEntity);
					if (this.<charsRead>5__2 == 0)
					{
						xmlTextReaderImpl.ps.isEof = true;
					}
					xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charsUsed] = '\0';
					result = this.<charsRead>5__2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_657:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009C0 RID: 2496 RVA: 0x00038C2C File Offset: 0x00036E2C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A73 RID: 2675
			public int <>1__state;

			// Token: 0x04000A74 RID: 2676
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000A75 RID: 2677
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A76 RID: 2678
			private int <charsRead>5__2;

			// Token: 0x04000A77 RID: 2679
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000E0 RID: 224
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseXmlDeclarationAsync>d__506 : IAsyncStateMachine
		{
			// Token: 0x060009C1 RID: 2497 RVA: 0x00038C3C File Offset: 0x00036E3C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result2;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_20B;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3EC;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_45C;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_513;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_79B;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_882;
					case 7:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_C72;
					case 8:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_D14;
					case 9:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_E45;
					default:
						goto IL_A8;
					}
					IL_9C:
					if (awaiter.GetResult() == 0)
					{
						goto IL_D3B;
					}
					IL_A8:
					if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos >= 6)
					{
						if (!XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, 5, "<?xml") || xmlTextReaderImpl.xmlCharType.IsNameSingleChar(xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos + 5]))
						{
							goto IL_D3B;
						}
						if (!this.isTextDecl)
						{
							xmlTextReaderImpl.curNode.SetLineInfo(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos + 2);
							xmlTextReaderImpl.curNode.SetNamedNode(XmlNodeType.XmlDeclaration, xmlTextReaderImpl.Xml);
						}
						XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
						xmlTextReaderImpl2.ps.charPos = xmlTextReaderImpl2.ps.charPos + 5;
						this.<sb>5__2 = (this.isTextDecl ? new StringBuilder() : xmlTextReaderImpl.stringBuilder);
						this.<xmlDeclState>5__3 = 0;
						this.<encoding>5__4 = null;
					}
					else
					{
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter, ref this);
							return;
						}
						goto IL_9C;
					}
					IL_18B:
					this.<originalSbLen>5__5 = this.<sb>5__2.Length;
					awaiter = xmlTextReaderImpl.EatWhitespacesAsync((this.<xmlDeclState>5__3 == 0) ? null : this.<sb>5__2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter, ref this);
						return;
					}
					IL_20B:
					int result = awaiter.GetResult();
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] == '?')
					{
						this.<sb>5__2.Length = this.<originalSbLen>5__5;
						if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos + 1] == '>')
						{
							if (this.<xmlDeclState>5__3 == 0)
							{
								xmlTextReaderImpl.Throw(this.isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
							}
							XmlTextReaderImpl xmlTextReaderImpl3 = xmlTextReaderImpl;
							xmlTextReaderImpl3.ps.charPos = xmlTextReaderImpl3.ps.charPos + 2;
							if (!this.isTextDecl)
							{
								xmlTextReaderImpl.curNode.SetValue(this.<sb>5__2.ToString());
								this.<sb>5__2.Length = 0;
								xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.parsingFunction;
								xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel;
							}
							if (this.<encoding>5__4 == null)
							{
								if (this.isTextDecl)
								{
									xmlTextReaderImpl.Throw("Invalid text declaration.");
								}
								if (xmlTextReaderImpl.afterResetState)
								{
									string webName = xmlTextReaderImpl.ps.encoding.WebName;
									if (webName != "utf-8" && webName != "utf-16" && webName != "utf-16BE" && !(xmlTextReaderImpl.ps.encoding is Ucs4Encoding))
									{
										xmlTextReaderImpl.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", (xmlTextReaderImpl.ps.encoding.GetByteCount("A") == 1) ? "UTF-8" : "UTF-16");
									}
								}
								if (!(xmlTextReaderImpl.ps.decoder is SafeAsciiDecoder))
								{
									goto IL_463;
								}
								awaiter2 = xmlTextReaderImpl.SwitchEncodingToUTF8Async().ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 2;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter2, ref this);
									return;
								}
								goto IL_3EC;
							}
							else
							{
								awaiter2 = xmlTextReaderImpl.SwitchEncodingAsync(this.<encoding>5__4).ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 3;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter2, ref this);
									return;
								}
								goto IL_45C;
							}
						}
						else
						{
							if (xmlTextReaderImpl.ps.charPos + 1 == xmlTextReaderImpl.ps.charsUsed)
							{
								goto IL_CA5;
							}
							xmlTextReaderImpl.ThrowUnexpectedToken("'>'");
						}
					}
					if (result == 0 && this.<xmlDeclState>5__3 != 0)
					{
						xmlTextReaderImpl.ThrowUnexpectedToken("?>");
					}
					awaiter = xmlTextReaderImpl.ParseNameAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter, ref this);
						return;
					}
					goto IL_513;
					IL_3EC:
					awaiter2.GetResult();
					goto IL_463;
					IL_45C:
					awaiter2.GetResult();
					IL_463:
					xmlTextReaderImpl.ps.appendMode = false;
					result2 = true;
					goto IL_E83;
					IL_513:
					int result3 = awaiter.GetResult();
					this.<attr>5__6 = null;
					char c = xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos];
					if (c != 'e')
					{
						if (c != 's')
						{
							if (c == 'v' && XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, result3 - xmlTextReaderImpl.ps.charPos, "version") && this.<xmlDeclState>5__3 == 0)
							{
								if (!this.isTextDecl)
								{
									this.<attr>5__6 = xmlTextReaderImpl.AddAttributeNoChecks("version", 1);
									goto IL_6B3;
								}
								goto IL_6B3;
							}
						}
						else if (XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, result3 - xmlTextReaderImpl.ps.charPos, "standalone") && (this.<xmlDeclState>5__3 == 1 || this.<xmlDeclState>5__3 == 2) && !this.isTextDecl)
						{
							if (!this.isTextDecl)
							{
								this.<attr>5__6 = xmlTextReaderImpl.AddAttributeNoChecks("standalone", 1);
							}
							this.<xmlDeclState>5__3 = 2;
							goto IL_6B3;
						}
					}
					else if (XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, result3 - xmlTextReaderImpl.ps.charPos, "encoding") && (this.<xmlDeclState>5__3 == 1 || (this.isTextDecl && this.<xmlDeclState>5__3 == 0)))
					{
						if (!this.isTextDecl)
						{
							this.<attr>5__6 = xmlTextReaderImpl.AddAttributeNoChecks("encoding", 1);
						}
						this.<xmlDeclState>5__3 = 1;
						goto IL_6B3;
					}
					xmlTextReaderImpl.Throw(this.isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
					IL_6B3:
					if (!this.isTextDecl)
					{
						this.<attr>5__6.SetLineInfo(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
					}
					this.<sb>5__2.Append(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, result3 - xmlTextReaderImpl.ps.charPos);
					xmlTextReaderImpl.ps.charPos = result3;
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] == '=')
					{
						goto IL_7C9;
					}
					awaiter = xmlTextReaderImpl.EatWhitespacesAsync(this.<sb>5__2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter, ref this);
						return;
					}
					IL_79B:
					awaiter.GetResult();
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != '=')
					{
						xmlTextReaderImpl.ThrowUnexpectedToken("=");
					}
					IL_7C9:
					this.<sb>5__2.Append('=');
					XmlTextReaderImpl xmlTextReaderImpl4 = xmlTextReaderImpl;
					xmlTextReaderImpl4.ps.charPos = xmlTextReaderImpl4.ps.charPos + 1;
					this.<quoteChar>5__7 = xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos];
					if (this.<quoteChar>5__7 == '"' || this.<quoteChar>5__7 == '\'')
					{
						goto IL_8CB;
					}
					awaiter = xmlTextReaderImpl.EatWhitespacesAsync(this.<sb>5__2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 6;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter, ref this);
						return;
					}
					IL_882:
					awaiter.GetResult();
					this.<quoteChar>5__7 = xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos];
					if (this.<quoteChar>5__7 != '"' && this.<quoteChar>5__7 != '\'')
					{
						xmlTextReaderImpl.ThrowUnexpectedToken("\"", "'");
					}
					IL_8CB:
					this.<sb>5__2.Append(this.<quoteChar>5__7);
					XmlTextReaderImpl xmlTextReaderImpl5 = xmlTextReaderImpl;
					xmlTextReaderImpl5.ps.charPos = xmlTextReaderImpl5.ps.charPos + 1;
					if (!this.isTextDecl)
					{
						this.<attr>5__6.quoteChar = this.<quoteChar>5__7;
						this.<attr>5__6.SetLineInfo2(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
					}
					this.<pos>5__8 = xmlTextReaderImpl.ps.charPos;
					IL_938:
					char[] chars = xmlTextReaderImpl.ps.chars;
					while ((xmlTextReaderImpl.xmlCharType.charProperties[(int)chars[this.<pos>5__8]] & 128) != 0)
					{
						int num2 = this.<pos>5__8;
						this.<pos>5__8 = num2 + 1;
					}
					if (xmlTextReaderImpl.ps.chars[this.<pos>5__8] == this.<quoteChar>5__7)
					{
						switch (this.<xmlDeclState>5__3)
						{
						case 0:
							if (XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__8 - xmlTextReaderImpl.ps.charPos, "1.0"))
							{
								if (!this.isTextDecl)
								{
									this.<attr>5__6.SetValue(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__8 - xmlTextReaderImpl.ps.charPos);
								}
								this.<xmlDeclState>5__3 = 1;
							}
							else
							{
								string arg = new string(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__8 - xmlTextReaderImpl.ps.charPos);
								xmlTextReaderImpl.Throw("Version number '{0}' is invalid.", arg);
							}
							break;
						case 1:
						{
							string text = new string(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__8 - xmlTextReaderImpl.ps.charPos);
							this.<encoding>5__4 = xmlTextReaderImpl.CheckEncoding(text);
							if (!this.isTextDecl)
							{
								this.<attr>5__6.SetValue(text);
							}
							this.<xmlDeclState>5__3 = 2;
							break;
						}
						case 2:
							if (XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__8 - xmlTextReaderImpl.ps.charPos, "yes"))
							{
								xmlTextReaderImpl.standalone = true;
							}
							else if (XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__8 - xmlTextReaderImpl.ps.charPos, "no"))
							{
								xmlTextReaderImpl.standalone = false;
							}
							else
							{
								xmlTextReaderImpl.Throw("Syntax for an XML declaration is invalid.", xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos - 1);
							}
							if (!this.isTextDecl)
							{
								this.<attr>5__6.SetValue(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__8 - xmlTextReaderImpl.ps.charPos);
							}
							this.<xmlDeclState>5__3 = 3;
							break;
						}
						this.<sb>5__2.Append(chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__8 - xmlTextReaderImpl.ps.charPos);
						this.<sb>5__2.Append(this.<quoteChar>5__7);
						xmlTextReaderImpl.ps.charPos = this.<pos>5__8 + 1;
						goto IL_18B;
					}
					if (this.<pos>5__8 != xmlTextReaderImpl.ps.charsUsed)
					{
						xmlTextReaderImpl.Throw(this.isTextDecl ? "Invalid text declaration." : "Syntax for an XML declaration is invalid.");
						goto IL_CA5;
					}
					awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 7;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter, ref this);
						return;
					}
					IL_C72:
					if (awaiter.GetResult() != 0)
					{
						goto IL_938;
					}
					xmlTextReaderImpl.Throw("There is an unclosed literal string.");
					IL_CA5:
					bool flag = xmlTextReaderImpl.ps.isEof;
					if (flag)
					{
						goto IL_D20;
					}
					awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 8;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter, ref this);
						return;
					}
					IL_D14:
					flag = (awaiter.GetResult() == 0);
					IL_D20:
					if (flag)
					{
						xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
					}
					this.<attr>5__6 = null;
					goto IL_18B;
					IL_D3B:
					if (!this.isTextDecl)
					{
						xmlTextReaderImpl.parsingFunction = xmlTextReaderImpl.nextParsingFunction;
					}
					if (xmlTextReaderImpl.afterResetState)
					{
						string webName2 = xmlTextReaderImpl.ps.encoding.WebName;
						if (webName2 != "utf-8" && webName2 != "utf-16" && webName2 != "utf-16BE" && !(xmlTextReaderImpl.ps.encoding is Ucs4Encoding))
						{
							xmlTextReaderImpl.Throw("'{0}' is an invalid value for the 'encoding' attribute. The encoding cannot be switched after a call to ResetState.", (xmlTextReaderImpl.ps.encoding.GetByteCount("A") == 1) ? "UTF-8" : "UTF-16");
						}
					}
					if (!(xmlTextReaderImpl.ps.decoder is SafeAsciiDecoder))
					{
						goto IL_E4C;
					}
					awaiter2 = xmlTextReaderImpl.SwitchEncodingToUTF8Async().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 9;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseXmlDeclarationAsync>d__506>(ref awaiter2, ref this);
						return;
					}
					IL_E45:
					awaiter2.GetResult();
					IL_E4C:
					xmlTextReaderImpl.ps.appendMode = false;
					result2 = false;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<sb>5__2 = null;
					this.<encoding>5__4 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E83:
				this.<>1__state = -2;
				this.<sb>5__2 = null;
				this.<encoding>5__4 = null;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060009C2 RID: 2498 RVA: 0x00039B0C File Offset: 0x00037D0C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A78 RID: 2680
			public int <>1__state;

			// Token: 0x04000A79 RID: 2681
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000A7A RID: 2682
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A7B RID: 2683
			public bool isTextDecl;

			// Token: 0x04000A7C RID: 2684
			private StringBuilder <sb>5__2;

			// Token: 0x04000A7D RID: 2685
			private int <xmlDeclState>5__3;

			// Token: 0x04000A7E RID: 2686
			private Encoding <encoding>5__4;

			// Token: 0x04000A7F RID: 2687
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A80 RID: 2688
			private int <originalSbLen>5__5;

			// Token: 0x04000A81 RID: 2689
			private XmlTextReaderImpl.NodeData <attr>5__6;

			// Token: 0x04000A82 RID: 2690
			private char <quoteChar>5__7;

			// Token: 0x04000A83 RID: 2691
			private int <pos>5__8;

			// Token: 0x04000A84 RID: 2692
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000E1 RID: 225
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseDocumentContentAsync_ParseEntity>d__509 : IAsyncStateMachine
		{
			// Token: 0x060009C3 RID: 2499 RVA: 0x00039B1C File Offset: 0x00037D1C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_158;
					case 2:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C7;
					case 3:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_238;
					case 4:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2A3;
					default:
					{
						int charPos = xmlTextReaderImpl.ps.charPos;
						if (xmlTextReaderImpl.fragmentType == XmlNodeType.Document)
						{
							xmlTextReaderImpl.Throw(charPos, "Data at the root level is invalid.");
							result = false;
							goto IL_2C6;
						}
						if (xmlTextReaderImpl.fragmentType == XmlNodeType.None)
						{
							xmlTextReaderImpl.fragmentType = XmlNodeType.Element;
						}
						awaiter = xmlTextReaderImpl.HandleEntityReferenceAsync(false, XmlTextReaderImpl.EntityExpandType.OnlyGeneral).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__509>(ref awaiter, ref this);
							return;
						}
						break;
					}
					}
					XmlTextReaderImpl.EntityType item = awaiter.GetResult().Item2;
					if (item > XmlTextReaderImpl.EntityType.CharacterNamed)
					{
						if (item == XmlTextReaderImpl.EntityType.Unexpanded)
						{
							if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.EntityReference)
							{
								xmlTextReaderImpl.parsingFunction = xmlTextReaderImpl.nextParsingFunction;
							}
							awaiter2 = xmlTextReaderImpl.ParseEntityReferenceAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__509>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter3 = xmlTextReaderImpl.ParseDocumentContentAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter3.IsCompleted)
							{
								this.<>1__state = 4;
								this.<>u__3 = awaiter3;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__509>(ref awaiter3, ref this);
								return;
							}
							goto IL_2A3;
						}
					}
					else
					{
						awaiter3 = xmlTextReaderImpl.ParseTextAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__509>(ref awaiter3, ref this);
							return;
						}
						goto IL_1C7;
					}
					IL_158:
					awaiter2.GetResult();
					result = true;
					goto IL_2C6;
					IL_1C7:
					if (awaiter3.GetResult())
					{
						result = true;
						goto IL_2C6;
					}
					awaiter3 = xmlTextReaderImpl.ParseDocumentContentAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDocumentContentAsync_ParseEntity>d__509>(ref awaiter3, ref this);
						return;
					}
					IL_238:
					result = awaiter3.GetResult();
					goto IL_2C6;
					IL_2A3:
					result = awaiter3.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2C6:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009C4 RID: 2500 RVA: 0x00039E20 File Offset: 0x00038020
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A85 RID: 2693
			public int <>1__state;

			// Token: 0x04000A86 RID: 2694
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000A87 RID: 2695
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A88 RID: 2696
			private ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A89 RID: 2697
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000A8A RID: 2698
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020000E2 RID: 226
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ParseDocumentContentAsync_WhiteSpace>d__511 : IAsyncStateMachine
		{
			// Token: 0x060009C5 RID: 2501 RVA: 0x00039E30 File Offset: 0x00038030
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
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
							goto IL_FB;
						}
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseDocumentContentAsync_WhiteSpace>d__511>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter.GetResult())
					{
						if (xmlTextReaderImpl.fragmentType == XmlNodeType.None && xmlTextReaderImpl.curNode.type == XmlNodeType.Text)
						{
							xmlTextReaderImpl.fragmentType = XmlNodeType.Element;
						}
						result = true;
						goto IL_11E;
					}
					awaiter = xmlTextReaderImpl.ParseDocumentContentAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseDocumentContentAsync_WhiteSpace>d__511>(ref awaiter, ref this);
						return;
					}
					IL_FB:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_11E:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009C6 RID: 2502 RVA: 0x00039F80 File Offset: 0x00038180
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A8B RID: 2699
			public int <>1__state;

			// Token: 0x04000A8C RID: 2700
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000A8D RID: 2701
			public Task<bool> task;

			// Token: 0x04000A8E RID: 2702
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A8F RID: 2703
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000E3 RID: 227
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseDocumentContentAsync_ReadData>d__512 : IAsyncStateMachine
		{
			// Token: 0x060009C7 RID: 2503 RVA: 0x00039F90 File Offset: 0x00038190
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_E8;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_18A;
					default:
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDocumentContentAsync_ReadData>d__512>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (awaiter.GetResult() != 0)
					{
						awaiter2 = xmlTextReaderImpl.ParseDocumentContentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDocumentContentAsync_ReadData>d__512>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						if (this.needMoreChars)
						{
							xmlTextReaderImpl.Throw("Data at the root level is invalid.");
						}
						if (!xmlTextReaderImpl.InEntity)
						{
							if (!xmlTextReaderImpl.rootElementParsed && xmlTextReaderImpl.fragmentType == XmlNodeType.Document)
							{
								xmlTextReaderImpl.ThrowWithoutLineInfo("Root element is missing.");
							}
							if (xmlTextReaderImpl.fragmentType == XmlNodeType.None)
							{
								xmlTextReaderImpl.fragmentType = (xmlTextReaderImpl.rootElementParsed ? XmlNodeType.Document : XmlNodeType.Element);
							}
							xmlTextReaderImpl.OnEof();
							result = false;
							goto IL_1EF;
						}
						if (xmlTextReaderImpl.HandleEntityEnd(true))
						{
							xmlTextReaderImpl.SetupEndEntityNodeInContent();
							result = true;
							goto IL_1EF;
						}
						awaiter2 = xmlTextReaderImpl.ParseDocumentContentAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDocumentContentAsync_ReadData>d__512>(ref awaiter2, ref this);
							return;
						}
						goto IL_18A;
					}
					IL_E8:
					result = awaiter2.GetResult();
					goto IL_1EF;
					IL_18A:
					result = awaiter2.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1EF:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009C8 RID: 2504 RVA: 0x0003A1BC File Offset: 0x000383BC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A90 RID: 2704
			public int <>1__state;

			// Token: 0x04000A91 RID: 2705
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000A92 RID: 2706
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A93 RID: 2707
			public bool needMoreChars;

			// Token: 0x04000A94 RID: 2708
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A95 RID: 2709
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000E4 RID: 228
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseElementContent_ReadData>d__514 : IAsyncStateMachine
		{
			// Token: 0x060009C9 RID: 2505 RVA: 0x0003A1CC File Offset: 0x000383CC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_13F;
						}
						awaiter2 = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseElementContent_ReadData>d__514>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter2.GetResult() == 0)
					{
						if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos != 0)
						{
							xmlTextReaderImpl.ThrowUnclosedElements();
						}
						if (!xmlTextReaderImpl.InEntity)
						{
							if (xmlTextReaderImpl.index == 0 && xmlTextReaderImpl.fragmentType != XmlNodeType.Document)
							{
								xmlTextReaderImpl.OnEof();
								result = false;
								goto IL_162;
							}
							xmlTextReaderImpl.ThrowUnclosedElements();
						}
						if (xmlTextReaderImpl.HandleEntityEnd(true))
						{
							xmlTextReaderImpl.SetupEndEntityNodeInContent();
							result = true;
							goto IL_162;
						}
					}
					awaiter = xmlTextReaderImpl.ParseElementContentAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseElementContent_ReadData>d__514>(ref awaiter, ref this);
						return;
					}
					IL_13F:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_162:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009CA RID: 2506 RVA: 0x0003A36C File Offset: 0x0003856C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A96 RID: 2710
			public int <>1__state;

			// Token: 0x04000A97 RID: 2711
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000A98 RID: 2712
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A99 RID: 2713
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000A9A RID: 2714
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000E5 RID: 229
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ParseElementAsync_ContinueWithSetElement>d__517 : IAsyncStateMachine
		{
			// Token: 0x060009CB RID: 2507 RVA: 0x0003A37C File Offset: 0x0003857C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_ED;
						}
						awaiter2 = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseElementAsync_ContinueWithSetElement>d__517>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Tuple<int, int> result = awaiter2.GetResult();
					int item = result.Item1;
					int item2 = result.Item2;
					awaiter = xmlTextReaderImpl.ParseElementAsync_SetElement(item, item2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseElementAsync_ContinueWithSetElement>d__517>(ref awaiter, ref this);
						return;
					}
					IL_ED:
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

			// Token: 0x060009CC RID: 2508 RVA: 0x0003A4BC File Offset: 0x000386BC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000A9B RID: 2715
			public int <>1__state;

			// Token: 0x04000A9C RID: 2716
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000A9D RID: 2717
			public Task<Tuple<int, int>> task;

			// Token: 0x04000A9E RID: 2718
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000A9F RID: 2719
			private ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000AA0 RID: 2720
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000E6 RID: 230
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseElementAsync_ReadData>d__520 : IAsyncStateMachine
		{
			// Token: 0x060009CD RID: 2509 RVA: 0x0003A4CC File Offset: 0x000386CC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_F2;
						}
						awaiter2 = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseElementAsync_ReadData>d__520>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter2.GetResult() == 0)
					{
						xmlTextReaderImpl.Throw(this.pos, "Unexpected end of file while parsing {0} has occurred.", ">");
					}
					awaiter = xmlTextReaderImpl.ParseElementAsync_NoAttributes().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseElementAsync_ReadData>d__520>(ref awaiter, ref this);
						return;
					}
					IL_F2:
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

			// Token: 0x060009CE RID: 2510 RVA: 0x0003A610 File Offset: 0x00038810
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AA1 RID: 2721
			public int <>1__state;

			// Token: 0x04000AA2 RID: 2722
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000AA3 RID: 2723
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AA4 RID: 2724
			public int pos;

			// Token: 0x04000AA5 RID: 2725
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000AA6 RID: 2726
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000E7 RID: 231
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ParseEndElmentAsync>d__522 : IAsyncStateMachine
		{
			// Token: 0x060009CF RID: 2511 RVA: 0x0003A620 File Offset: 0x00038820
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_D6;
						}
						awaiter = xmlTextReaderImpl.ParseEndElmentAsync_PrepareData().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseEndElmentAsync>d__522>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					awaiter = xmlTextReaderImpl.ParseEndElementAsync_CheckNameAndParse().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseEndElmentAsync>d__522>(ref awaiter, ref this);
						return;
					}
					IL_D6:
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

			// Token: 0x060009D0 RID: 2512 RVA: 0x0003A748 File Offset: 0x00038948
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AA7 RID: 2727
			public int <>1__state;

			// Token: 0x04000AA8 RID: 2728
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000AA9 RID: 2729
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AAA RID: 2730
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000E8 RID: 232
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseEndElmentAsync_PrepareData>d__523 : IAsyncStateMachine
		{
			// Token: 0x060009D1 RID: 2513 RVA: 0x0003A758 File Offset: 0x00038958
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					if (num != 0)
					{
						XmlTextReaderImpl.NodeData nodeData = xmlTextReaderImpl.nodes[xmlTextReaderImpl.index - 1];
						this.<prefLen>5__2 = nodeData.prefix.Length;
						this.<locLen>5__3 = nodeData.localName.Length;
						goto IL_A9;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_A0:
					if (awaiter.GetResult() == 0)
					{
						goto IL_D4;
					}
					IL_A9:
					if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos < this.<prefLen>5__2 + this.<locLen>5__3 + 1)
					{
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseEndElmentAsync_PrepareData>d__523>(ref awaiter, ref this);
							return;
						}
						goto IL_A0;
					}
					IL_D4:;
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

			// Token: 0x060009D2 RID: 2514 RVA: 0x0003A878 File Offset: 0x00038A78
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AAB RID: 2731
			public int <>1__state;

			// Token: 0x04000AAC RID: 2732
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000AAD RID: 2733
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AAE RID: 2734
			private int <prefLen>5__2;

			// Token: 0x04000AAF RID: 2735
			private int <locLen>5__3;

			// Token: 0x04000AB0 RID: 2736
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000E9 RID: 233
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseEndElementAsync_Finish>d__528 : IAsyncStateMachine
		{
			// Token: 0x060009D3 RID: 2515 RVA: 0x0003A888 File Offset: 0x00038A88
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_6E;
					}
					IL_11:
					awaiter = this.task.ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseEndElementAsync_Finish>d__528>(ref awaiter, ref this);
						return;
					}
					IL_6E:
					awaiter.GetResult();
					switch (xmlTextReaderImpl.parseEndElement_NextFunc)
					{
					case XmlTextReaderImpl.ParseEndElementParseFunction.CheckEndTag:
						this.task = xmlTextReaderImpl.ParseEndElementAsync_CheckEndTag(this.nameLen, this.startTagNode, this.endTagLineInfo);
						goto IL_11;
					case XmlTextReaderImpl.ParseEndElementParseFunction.ReadData:
						this.task = xmlTextReaderImpl.ParseEndElementAsync_ReadData();
						goto IL_11;
					case XmlTextReaderImpl.ParseEndElementParseFunction.Done:
						break;
					default:
						goto IL_11;
					}
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

			// Token: 0x060009D4 RID: 2516 RVA: 0x0003A99C File Offset: 0x00038B9C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AB1 RID: 2737
			public int <>1__state;

			// Token: 0x04000AB2 RID: 2738
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000AB3 RID: 2739
			public Task task;

			// Token: 0x04000AB4 RID: 2740
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AB5 RID: 2741
			public int nameLen;

			// Token: 0x04000AB6 RID: 2742
			public XmlTextReaderImpl.NodeData startTagNode;

			// Token: 0x04000AB7 RID: 2743
			public LineInfo endTagLineInfo;

			// Token: 0x04000AB8 RID: 2744
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000EA RID: 234
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseEndElementAsync_ReadData>d__530 : IAsyncStateMachine
		{
			// Token: 0x060009D5 RID: 2517 RVA: 0x0003A9AC File Offset: 0x00038BAC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseEndElementAsync_ReadData>d__530>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter.GetResult() == 0)
					{
						xmlTextReaderImpl.ThrowUnclosedElements();
					}
					xmlTextReaderImpl.parseEndElement_NextFunc = XmlTextReaderImpl.ParseEndElementParseFunction.CheckEndTag;
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

			// Token: 0x060009D6 RID: 2518 RVA: 0x0003AA78 File Offset: 0x00038C78
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AB9 RID: 2745
			public int <>1__state;

			// Token: 0x04000ABA RID: 2746
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000ABB RID: 2747
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000ABC RID: 2748
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000EB RID: 235
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ThrowTagMismatchAsync>d__531 : IAsyncStateMachine
		{
			// Token: 0x060009D7 RID: 2519 RVA: 0x0003AA88 File Offset: 0x00038C88
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (this.startTag.type != XmlNodeType.Element)
						{
							xmlTextReaderImpl.Throw("Unexpected end tag.");
							goto IL_130;
						}
						awaiter = xmlTextReaderImpl.ParseQNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ThrowTagMismatchAsync>d__531>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Tuple<int, int> result = awaiter.GetResult();
					int item = result.Item1;
					int item2 = result.Item2;
					xmlTextReaderImpl.Throw("The '{0}' start tag on line {1} position {2} does not match the end tag of '{3}'.", new string[]
					{
						this.startTag.GetNameWPrefix(xmlTextReaderImpl.nameTable),
						this.startTag.lineInfo.lineNo.ToString(CultureInfo.InvariantCulture),
						this.startTag.lineInfo.linePos.ToString(CultureInfo.InvariantCulture),
						new string(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, item2 - xmlTextReaderImpl.ps.charPos)
					});
					IL_130:;
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

			// Token: 0x060009D8 RID: 2520 RVA: 0x0003AC10 File Offset: 0x00038E10
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000ABD RID: 2749
			public int <>1__state;

			// Token: 0x04000ABE RID: 2750
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000ABF RID: 2751
			public XmlTextReaderImpl.NodeData startTag;

			// Token: 0x04000AC0 RID: 2752
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AC1 RID: 2753
			private ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000EC RID: 236
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseAttributesAsync>d__532 : IAsyncStateMachine
		{
			// Token: 0x060009D9 RID: 2521 RVA: 0x0003AC20 File Offset: 0x00038E20
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3C4;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_472;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_559;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_634;
					case 4:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_7CC;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_8F9;
					default:
						this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
						this.<chars>5__3 = xmlTextReaderImpl.ps.chars;
						this.<attr>5__4 = null;
						break;
					}
					IL_55:
					int num2 = 0;
					char c;
					int num3;
					while ((xmlTextReaderImpl.xmlCharType.charProperties[(int)(c = this.<chars>5__3[this.<pos>5__2])] & 1) != 0)
					{
						if (c == '\n')
						{
							xmlTextReaderImpl.OnNewLine(this.<pos>5__2 + 1);
							num2++;
						}
						else if (c == '\r')
						{
							if (this.<chars>5__3[this.<pos>5__2 + 1] == '\n')
							{
								xmlTextReaderImpl.OnNewLine(this.<pos>5__2 + 2);
								num2++;
								num3 = this.<pos>5__2;
								this.<pos>5__2 = num3 + 1;
							}
							else if (this.<pos>5__2 + 1 != xmlTextReaderImpl.ps.charsUsed)
							{
								xmlTextReaderImpl.OnNewLine(this.<pos>5__2 + 1);
								num2++;
							}
							else
							{
								xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
								IL_888:
								XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
								xmlTextReaderImpl2.ps.lineNo = xmlTextReaderImpl2.ps.lineNo - num2;
								awaiter2 = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter2.IsCompleted)
								{
									this.<>1__state = 5;
									this.<>u__2 = awaiter2;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributesAsync>d__532>(ref awaiter2, ref this);
									return;
								}
								goto IL_8F9;
							}
						}
						num3 = this.<pos>5__2;
						this.<pos>5__2 = num3 + 1;
					}
					int num4 = 0;
					char c2;
					if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)(c2 = this.<chars>5__3[this.<pos>5__2])] & 4) != 0)
					{
						num4 = 1;
					}
					if (num4 == 0)
					{
						if (c2 == '>')
						{
							xmlTextReaderImpl.ps.charPos = this.<pos>5__2 + 1;
							xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.MoveToElementContent;
						}
						else if (c2 == '/')
						{
							if (this.<pos>5__2 + 1 == xmlTextReaderImpl.ps.charsUsed)
							{
								goto IL_888;
							}
							if (this.<chars>5__3[this.<pos>5__2 + 1] != '>')
							{
								xmlTextReaderImpl.ThrowUnexpectedToken(this.<pos>5__2 + 1, ">");
								goto IL_246;
							}
							xmlTextReaderImpl.ps.charPos = this.<pos>5__2 + 2;
							xmlTextReaderImpl.curNode.IsEmptyElement = true;
							xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.parsingFunction;
							xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.PopEmptyElementContext;
						}
						else
						{
							if (this.<pos>5__2 == xmlTextReaderImpl.ps.charsUsed)
							{
								goto IL_888;
							}
							if (c2 != ':' || xmlTextReaderImpl.supportNamespaces)
							{
								xmlTextReaderImpl.Throw(this.<pos>5__2, "Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(this.<chars>5__3, xmlTextReaderImpl.ps.charsUsed, this.<pos>5__2));
								goto IL_246;
							}
							goto IL_246;
						}
						if (xmlTextReaderImpl.addDefaultAttributesAndNormalize)
						{
							xmlTextReaderImpl.AddDefaultAttributesAndNormalize();
						}
						xmlTextReaderImpl.ElementNamespaceLookup();
						if (xmlTextReaderImpl.attrNeedNamespaceLookup)
						{
							xmlTextReaderImpl.AttributeNamespaceLookup();
							xmlTextReaderImpl.attrNeedNamespaceLookup = false;
						}
						if (xmlTextReaderImpl.attrDuplWalkCount >= 250)
						{
							xmlTextReaderImpl.AttributeDuplCheck();
						}
						goto IL_999;
					}
					IL_246:
					if (this.<pos>5__2 == xmlTextReaderImpl.ps.charPos)
					{
						xmlTextReaderImpl.ThrowExpectingWhitespace(this.<pos>5__2);
					}
					xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
					this.<attrNameLinePos>5__5 = xmlTextReaderImpl.ps.LinePos;
					int num5 = -1;
					this.<pos>5__2 += num4;
					for (;;)
					{
						char c3;
						if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)(c3 = this.<chars>5__3[this.<pos>5__2])] & 8) != 0)
						{
							num3 = this.<pos>5__2;
							this.<pos>5__2 = num3 + 1;
						}
						else
						{
							if (c3 != ':')
							{
								goto IL_3F9;
							}
							if (num5 != -1)
							{
								if (xmlTextReaderImpl.supportNamespaces)
								{
									break;
								}
								num3 = this.<pos>5__2;
								this.<pos>5__2 = num3 + 1;
							}
							else
							{
								num5 = this.<pos>5__2;
								num3 = this.<pos>5__2;
								this.<pos>5__2 = num3 + 1;
								if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)this.<chars>5__3[this.<pos>5__2]] & 4) == 0)
								{
									goto IL_363;
								}
								num3 = this.<pos>5__2;
								this.<pos>5__2 = num3 + 1;
							}
						}
					}
					xmlTextReaderImpl.Throw(this.<pos>5__2, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
					goto IL_4A2;
					IL_363:
					awaiter = xmlTextReaderImpl.ParseQNameAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributesAsync>d__532>(ref awaiter, ref this);
						return;
					}
					goto IL_3C4;
					IL_3F9:
					if (this.<pos>5__2 + 1 < xmlTextReaderImpl.ps.charsUsed)
					{
						goto IL_4A2;
					}
					awaiter = xmlTextReaderImpl.ParseQNameAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributesAsync>d__532>(ref awaiter, ref this);
						return;
					}
					goto IL_472;
					IL_3C4:
					Tuple<int, int> result = awaiter.GetResult();
					num5 = result.Item1;
					this.<pos>5__2 = result.Item2;
					this.<chars>5__3 = xmlTextReaderImpl.ps.chars;
					goto IL_4A2;
					IL_472:
					Tuple<int, int> result2 = awaiter.GetResult();
					num5 = result2.Item1;
					this.<pos>5__2 = result2.Item2;
					this.<chars>5__3 = xmlTextReaderImpl.ps.chars;
					IL_4A2:
					this.<attr>5__4 = xmlTextReaderImpl.AddAttribute(this.<pos>5__2, num5);
					this.<attr>5__4.SetLineInfo(xmlTextReaderImpl.ps.LineNo, this.<attrNameLinePos>5__5);
					if (this.<chars>5__3[this.<pos>5__2] == '=')
					{
						goto IL_58E;
					}
					xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributesAsync>d__532>(ref awaiter2, ref this);
						return;
					}
					IL_559:
					awaiter2.GetResult();
					this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
					if (this.<chars>5__3[this.<pos>5__2] != '=')
					{
						xmlTextReaderImpl.ThrowUnexpectedToken("=");
					}
					IL_58E:
					num3 = this.<pos>5__2;
					this.<pos>5__2 = num3 + 1;
					char c4 = this.<chars>5__3[this.<pos>5__2];
					if (c4 == '"' || c4 == '\'')
					{
						goto IL_678;
					}
					xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributesAsync>d__532>(ref awaiter2, ref this);
						return;
					}
					IL_634:
					awaiter2.GetResult();
					this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
					c4 = this.<chars>5__3[this.<pos>5__2];
					if (c4 != '"' && c4 != '\'')
					{
						xmlTextReaderImpl.ThrowUnexpectedToken("\"", "'");
					}
					IL_678:
					num3 = this.<pos>5__2;
					this.<pos>5__2 = num3 + 1;
					xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
					this.<attr>5__4.quoteChar = c4;
					this.<attr>5__4.SetLineInfo2(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
					char c5;
					while ((xmlTextReaderImpl.xmlCharType.charProperties[(int)(c5 = this.<chars>5__3[this.<pos>5__2])] & 128) != 0)
					{
						num3 = this.<pos>5__2;
						this.<pos>5__2 = num3 + 1;
					}
					if (c5 == c4)
					{
						this.<attr>5__4.SetValue(this.<chars>5__3, xmlTextReaderImpl.ps.charPos, this.<pos>5__2 - xmlTextReaderImpl.ps.charPos);
						num3 = this.<pos>5__2;
						this.<pos>5__2 = num3 + 1;
						xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
						goto IL_7F5;
					}
					awaiter3 = xmlTextReaderImpl.ParseAttributeValueSlowAsync(this.<pos>5__2, c4, this.<attr>5__4).ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributesAsync>d__532>(ref awaiter3, ref this);
						return;
					}
					IL_7CC:
					awaiter3.GetResult();
					this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
					this.<chars>5__3 = xmlTextReaderImpl.ps.chars;
					IL_7F5:
					if (this.<attr>5__4.prefix.Length == 0)
					{
						if (Ref.Equal(this.<attr>5__4.localName, xmlTextReaderImpl.XmlNs))
						{
							xmlTextReaderImpl.OnDefaultNamespaceDecl(this.<attr>5__4);
							goto IL_55;
						}
						goto IL_55;
					}
					else
					{
						if (Ref.Equal(this.<attr>5__4.prefix, xmlTextReaderImpl.XmlNs))
						{
							xmlTextReaderImpl.OnNamespaceDecl(this.<attr>5__4);
							goto IL_55;
						}
						if (Ref.Equal(this.<attr>5__4.prefix, xmlTextReaderImpl.Xml))
						{
							xmlTextReaderImpl.OnXmlReservedAttribute(this.<attr>5__4);
							goto IL_55;
						}
						goto IL_55;
					}
					IL_8F9:
					if (awaiter2.GetResult() != 0)
					{
						this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
						this.<chars>5__3 = xmlTextReaderImpl.ps.chars;
						goto IL_55;
					}
					xmlTextReaderImpl.ThrowUnclosedElements();
					goto IL_55;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<chars>5__3 = null;
					this.<attr>5__4 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_999:
				this.<>1__state = -2;
				this.<chars>5__3 = null;
				this.<attr>5__4 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060009DA RID: 2522 RVA: 0x0003B604 File Offset: 0x00039804
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AC2 RID: 2754
			public int <>1__state;

			// Token: 0x04000AC3 RID: 2755
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000AC4 RID: 2756
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AC5 RID: 2757
			private int <pos>5__2;

			// Token: 0x04000AC6 RID: 2758
			private char[] <chars>5__3;

			// Token: 0x04000AC7 RID: 2759
			private XmlTextReaderImpl.NodeData <attr>5__4;

			// Token: 0x04000AC8 RID: 2760
			private int <attrNameLinePos>5__5;

			// Token: 0x04000AC9 RID: 2761
			private ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000ACA RID: 2762
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000ACB RID: 2763
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020000ED RID: 237
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseAttributeValueSlowAsync>d__533 : IAsyncStateMachine
		{
			// Token: 0x060009DB RID: 2523 RVA: 0x0003B614 File Offset: 0x00039814
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter3;
					char[] chars;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3F3;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_540;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_665;
					case 3:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_883;
					default:
						this.<pos>5__2 = this.curPos;
						chars = xmlTextReaderImpl.ps.chars;
						this.<attributeBaseEntityId>5__3 = xmlTextReaderImpl.ps.entityId;
						this.<valueChunkStartPos>5__4 = 0;
						this.<valueChunkLineInfo>5__5 = new LineInfo(xmlTextReaderImpl.ps.lineNo, xmlTextReaderImpl.ps.LinePos);
						this.<lastChunk>5__6 = null;
						break;
					}
					for (;;)
					{
						IL_8E:
						if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)chars[this.<pos>5__2]] & 128) == 0)
						{
							if (this.<pos>5__2 - xmlTextReaderImpl.ps.charPos > 0)
							{
								xmlTextReaderImpl.stringBuilder.Append(chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__2 - xmlTextReaderImpl.ps.charPos);
								xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
							}
							if (chars[this.<pos>5__2] == this.quoteChar && this.<attributeBaseEntityId>5__3 == xmlTextReaderImpl.ps.entityId)
							{
								goto IL_994;
							}
							char c = chars[this.<pos>5__2];
							int num2;
							if (c <= '&')
							{
								switch (c)
								{
								case '\t':
									num2 = this.<pos>5__2;
									this.<pos>5__2 = num2 + 1;
									if (xmlTextReaderImpl.normalize)
									{
										xmlTextReaderImpl.stringBuilder.Append(' ');
										XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
										xmlTextReaderImpl2.ps.charPos = xmlTextReaderImpl2.ps.charPos + 1;
										continue;
									}
									continue;
								case '\n':
									num2 = this.<pos>5__2;
									this.<pos>5__2 = num2 + 1;
									xmlTextReaderImpl.OnNewLine(this.<pos>5__2);
									if (xmlTextReaderImpl.normalize)
									{
										xmlTextReaderImpl.stringBuilder.Append(' ');
										XmlTextReaderImpl xmlTextReaderImpl3 = xmlTextReaderImpl;
										xmlTextReaderImpl3.ps.charPos = xmlTextReaderImpl3.ps.charPos + 1;
										continue;
									}
									continue;
								case '\v':
								case '\f':
									goto IL_79F;
								case '\r':
									if (chars[this.<pos>5__2 + 1] == '\n')
									{
										this.<pos>5__2 += 2;
										if (xmlTextReaderImpl.normalize)
										{
											xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.eolNormalized ? "  " : " ");
											xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
										}
									}
									else
									{
										if (this.<pos>5__2 + 1 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
										{
											goto IL_822;
										}
										num2 = this.<pos>5__2;
										this.<pos>5__2 = num2 + 1;
										if (xmlTextReaderImpl.normalize)
										{
											xmlTextReaderImpl.stringBuilder.Append(' ');
											xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
										}
									}
									xmlTextReaderImpl.OnNewLine(this.<pos>5__2);
									continue;
								default:
									if (c != '"')
									{
										if (c != '&')
										{
											goto IL_79F;
										}
										goto IL_30C;
									}
									break;
								}
							}
							else if (c != '\'')
							{
								if (c == '<')
								{
									break;
								}
								if (c != '>')
								{
									goto IL_79F;
								}
							}
							num2 = this.<pos>5__2;
							this.<pos>5__2 = num2 + 1;
							continue;
							IL_79F:
							if (this.<pos>5__2 == xmlTextReaderImpl.ps.charsUsed)
							{
								goto IL_822;
							}
							if (!XmlCharType.IsHighSurrogate((int)chars[this.<pos>5__2]))
							{
								goto IL_80A;
							}
							if (this.<pos>5__2 + 1 == xmlTextReaderImpl.ps.charsUsed)
							{
								goto IL_822;
							}
							num2 = this.<pos>5__2;
							this.<pos>5__2 = num2 + 1;
							if (!XmlCharType.IsLowSurrogate((int)chars[this.<pos>5__2]))
							{
								goto IL_80A;
							}
							num2 = this.<pos>5__2;
							this.<pos>5__2 = num2 + 1;
						}
						else
						{
							int num2 = this.<pos>5__2;
							this.<pos>5__2 = num2 + 1;
						}
					}
					xmlTextReaderImpl.Throw(this.<pos>5__2, "'{0}', hexadecimal value {1}, is an invalid attribute character.", XmlException.BuildCharExceptionArgs('<', '\0'));
					goto IL_822;
					IL_30C:
					if (this.<pos>5__2 - xmlTextReaderImpl.ps.charPos > 0)
					{
						xmlTextReaderImpl.stringBuilder.Append(chars, xmlTextReaderImpl.ps.charPos, this.<pos>5__2 - xmlTextReaderImpl.ps.charPos);
					}
					xmlTextReaderImpl.ps.charPos = this.<pos>5__2;
					this.<enclosingEntityId>5__7 = xmlTextReaderImpl.ps.entityId;
					this.<entityLineInfo>5__8 = new LineInfo(xmlTextReaderImpl.ps.lineNo, xmlTextReaderImpl.ps.LinePos + 1);
					awaiter = xmlTextReaderImpl.HandleEntityReferenceAsync(true, XmlTextReaderImpl.EntityExpandType.All).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributeValueSlowAsync>d__533>(ref awaiter, ref this);
						return;
					}
					goto IL_3F3;
					IL_80A:
					xmlTextReaderImpl.ThrowInvalidChar(chars, xmlTextReaderImpl.ps.charsUsed, this.<pos>5__2);
					IL_822:
					awaiter3 = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributeValueSlowAsync>d__533>(ref awaiter3, ref this);
						return;
					}
					goto IL_883;
					IL_3F3:
					Tuple<int, XmlTextReaderImpl.EntityType> result = awaiter.GetResult();
					this.<pos>5__2 = result.Item1;
					switch (result.Item2)
					{
					case XmlTextReaderImpl.EntityType.CharacterDec:
					case XmlTextReaderImpl.EntityType.CharacterHex:
					case XmlTextReaderImpl.EntityType.CharacterNamed:
						goto IL_78E;
					case XmlTextReaderImpl.EntityType.Unexpanded:
						if (xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.Full && xmlTextReaderImpl.ps.entityId == this.<attributeBaseEntityId>5__3)
						{
							int num3 = xmlTextReaderImpl.stringBuilder.Length - this.<valueChunkStartPos>5__4;
							if (num3 > 0)
							{
								XmlTextReaderImpl.NodeData nodeData = new XmlTextReaderImpl.NodeData();
								nodeData.lineInfo = this.<valueChunkLineInfo>5__5;
								nodeData.depth = this.attr.depth + 1;
								nodeData.SetValueNode(XmlNodeType.Text, xmlTextReaderImpl.stringBuilder.ToString(this.<valueChunkStartPos>5__4, num3));
								xmlTextReaderImpl.AddAttributeChunkToList(this.attr, nodeData, ref this.<lastChunk>5__6);
							}
							XmlTextReaderImpl xmlTextReaderImpl4 = xmlTextReaderImpl;
							xmlTextReaderImpl4.ps.charPos = xmlTextReaderImpl4.ps.charPos + 1;
							awaiter2 = xmlTextReaderImpl.ParseEntityNameAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributeValueSlowAsync>d__533>(ref awaiter2, ref this);
								return;
							}
							goto IL_540;
						}
						else
						{
							XmlTextReaderImpl xmlTextReaderImpl5 = xmlTextReaderImpl;
							xmlTextReaderImpl5.ps.charPos = xmlTextReaderImpl5.ps.charPos + 1;
							awaiter2 = xmlTextReaderImpl.ParseEntityNameAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseAttributeValueSlowAsync>d__533>(ref awaiter2, ref this);
								return;
							}
							goto IL_665;
						}
						break;
					case XmlTextReaderImpl.EntityType.ExpandedInAttribute:
						if (xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.Full && this.<enclosingEntityId>5__7 == this.<attributeBaseEntityId>5__3)
						{
							int num4 = xmlTextReaderImpl.stringBuilder.Length - this.<valueChunkStartPos>5__4;
							if (num4 > 0)
							{
								XmlTextReaderImpl.NodeData nodeData2 = new XmlTextReaderImpl.NodeData();
								nodeData2.lineInfo = this.<valueChunkLineInfo>5__5;
								nodeData2.depth = this.attr.depth + 1;
								nodeData2.SetValueNode(XmlNodeType.Text, xmlTextReaderImpl.stringBuilder.ToString(this.<valueChunkStartPos>5__4, num4));
								xmlTextReaderImpl.AddAttributeChunkToList(this.attr, nodeData2, ref this.<lastChunk>5__6);
							}
							XmlTextReaderImpl.NodeData nodeData3 = new XmlTextReaderImpl.NodeData();
							nodeData3.lineInfo = this.<entityLineInfo>5__8;
							nodeData3.depth = this.attr.depth + 1;
							nodeData3.SetNamedNode(XmlNodeType.EntityReference, xmlTextReaderImpl.ps.entity.Name);
							xmlTextReaderImpl.AddAttributeChunkToList(this.attr, nodeData3, ref this.<lastChunk>5__6);
							xmlTextReaderImpl.fullAttrCleanup = true;
						}
						this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
						goto IL_78E;
					}
					this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
					goto IL_78E;
					IL_540:
					string result2 = awaiter2.GetResult();
					XmlTextReaderImpl.NodeData nodeData4 = new XmlTextReaderImpl.NodeData();
					nodeData4.lineInfo = this.<entityLineInfo>5__8;
					nodeData4.depth = this.attr.depth + 1;
					nodeData4.SetNamedNode(XmlNodeType.EntityReference, result2);
					xmlTextReaderImpl.AddAttributeChunkToList(this.attr, nodeData4, ref this.<lastChunk>5__6);
					xmlTextReaderImpl.stringBuilder.Append('&');
					xmlTextReaderImpl.stringBuilder.Append(result2);
					xmlTextReaderImpl.stringBuilder.Append(';');
					this.<valueChunkStartPos>5__4 = xmlTextReaderImpl.stringBuilder.Length;
					this.<valueChunkLineInfo>5__5.Set(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
					xmlTextReaderImpl.fullAttrCleanup = true;
					goto IL_66D;
					IL_665:
					awaiter2.GetResult();
					IL_66D:
					this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
					IL_78E:
					chars = xmlTextReaderImpl.ps.chars;
					goto IL_8E;
					IL_883:
					if (awaiter3.GetResult() == 0)
					{
						if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos > 0)
						{
							if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != '\r')
							{
								xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							if (!xmlTextReaderImpl.InEntity)
							{
								if (xmlTextReaderImpl.fragmentType == XmlNodeType.Attribute)
								{
									if (this.<attributeBaseEntityId>5__3 != xmlTextReaderImpl.ps.entityId)
									{
										xmlTextReaderImpl.Throw("Entity replacement text must nest properly within markup declarations.");
										goto IL_994;
									}
									goto IL_994;
								}
								else
								{
									xmlTextReaderImpl.Throw("There is an unclosed literal string.");
								}
							}
							if (xmlTextReaderImpl.HandleEntityEnd(true))
							{
								xmlTextReaderImpl.Throw("An internal error has occurred.");
							}
							if (this.<attributeBaseEntityId>5__3 == xmlTextReaderImpl.ps.entityId)
							{
								this.<valueChunkStartPos>5__4 = xmlTextReaderImpl.stringBuilder.Length;
								this.<valueChunkLineInfo>5__5.Set(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
							}
						}
					}
					this.<pos>5__2 = xmlTextReaderImpl.ps.charPos;
					chars = xmlTextReaderImpl.ps.chars;
					goto IL_8E;
					IL_994:
					if (this.attr.nextAttrValueChunk != null)
					{
						int num5 = xmlTextReaderImpl.stringBuilder.Length - this.<valueChunkStartPos>5__4;
						if (num5 > 0)
						{
							XmlTextReaderImpl.NodeData nodeData5 = new XmlTextReaderImpl.NodeData();
							nodeData5.lineInfo = this.<valueChunkLineInfo>5__5;
							nodeData5.depth = this.attr.depth + 1;
							nodeData5.SetValueNode(XmlNodeType.Text, xmlTextReaderImpl.stringBuilder.ToString(this.<valueChunkStartPos>5__4, num5));
							xmlTextReaderImpl.AddAttributeChunkToList(this.attr, nodeData5, ref this.<lastChunk>5__6);
						}
					}
					xmlTextReaderImpl.ps.charPos = this.<pos>5__2 + 1;
					this.attr.SetValue(xmlTextReaderImpl.stringBuilder.ToString());
					xmlTextReaderImpl.stringBuilder.Length = 0;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<lastChunk>5__6 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<lastChunk>5__6 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x060009DC RID: 2524 RVA: 0x0003C0C0 File Offset: 0x0003A2C0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000ACC RID: 2764
			public int <>1__state;

			// Token: 0x04000ACD RID: 2765
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000ACE RID: 2766
			public int curPos;

			// Token: 0x04000ACF RID: 2767
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AD0 RID: 2768
			public char quoteChar;

			// Token: 0x04000AD1 RID: 2769
			public XmlTextReaderImpl.NodeData attr;

			// Token: 0x04000AD2 RID: 2770
			private int <pos>5__2;

			// Token: 0x04000AD3 RID: 2771
			private int <attributeBaseEntityId>5__3;

			// Token: 0x04000AD4 RID: 2772
			private int <valueChunkStartPos>5__4;

			// Token: 0x04000AD5 RID: 2773
			private LineInfo <valueChunkLineInfo>5__5;

			// Token: 0x04000AD6 RID: 2774
			private XmlTextReaderImpl.NodeData <lastChunk>5__6;

			// Token: 0x04000AD7 RID: 2775
			private int <enclosingEntityId>5__7;

			// Token: 0x04000AD8 RID: 2776
			private LineInfo <entityLineInfo>5__8;

			// Token: 0x04000AD9 RID: 2777
			private ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000ADA RID: 2778
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000ADB RID: 2779
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020000EE RID: 238
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ParseTextAsync>d__535 : IAsyncStateMachine
		{
			// Token: 0x060009DD RID: 2525 RVA: 0x0003C0D0 File Offset: 0x0003A2D0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result3;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					int num2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_A8;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_16C;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_260;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3C2;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4C5;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_597;
					default:
						num2 = 0;
						if (this.parseTask == null)
						{
							if (xmlTextReaderImpl.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
							{
								break;
							}
							xmlTextReaderImpl.curNode.SetLineInfo(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
							this.parseTask = xmlTextReaderImpl.ParseTextAsync(num2);
						}
						awaiter = this.parseTask.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseTextAsync>d__535>(ref awaiter, ref this);
							return;
						}
						goto IL_16C;
					}
					IL_45:
					awaiter = xmlTextReaderImpl.ParseTextAsync(num2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseTextAsync>d__535>(ref awaiter, ref this);
						return;
					}
					IL_A8:
					Tuple<int, int, int, bool> result = awaiter.GetResult();
					int item = result.Item1;
					int item2 = result.Item2;
					num2 = result.Item3;
					if (result.Item4)
					{
						goto IL_539;
					}
					goto IL_45;
					IL_16C:
					Tuple<int, int, int, bool> result2 = awaiter.GetResult();
					item = result2.Item1;
					item2 = result2.Item2;
					num2 = result2.Item3;
					if (result2.Item4)
					{
						if (item2 - item == 0)
						{
							goto IL_539;
						}
						XmlNodeType textNodeType = xmlTextReaderImpl.GetTextNodeType(num2);
						if (textNodeType != XmlNodeType.None)
						{
							xmlTextReaderImpl.curNode.SetValueNode(textNodeType, xmlTextReaderImpl.ps.chars, item, item2 - item);
							result3 = true;
							goto IL_5BA;
						}
						goto IL_539;
					}
					else if (!xmlTextReaderImpl.v1Compat)
					{
						if (num2 > 32)
						{
							xmlTextReaderImpl.curNode.SetValueNode(XmlNodeType.Text, xmlTextReaderImpl.ps.chars, item, item2 - item);
							xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.parsingFunction;
							xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.PartialTextValue;
							result3 = true;
							goto IL_5BA;
						}
						if (item2 - item > 0)
						{
							xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
							goto IL_35F;
						}
						goto IL_35F;
					}
					IL_1DA:
					if (item2 - item > 0)
					{
						xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
					}
					awaiter = xmlTextReaderImpl.ParseTextAsync(num2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseTextAsync>d__535>(ref awaiter, ref this);
						return;
					}
					IL_260:
					Tuple<int, int, int, bool> result4 = awaiter.GetResult();
					item = result4.Item1;
					item2 = result4.Item2;
					num2 = result4.Item3;
					if (!result4.Item4)
					{
						goto IL_1DA;
					}
					if (item2 - item > 0)
					{
						xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
					}
					XmlNodeType textNodeType2 = xmlTextReaderImpl.GetTextNodeType(num2);
					if (textNodeType2 == XmlNodeType.None)
					{
						xmlTextReaderImpl.stringBuilder.Length = 0;
						goto IL_539;
					}
					xmlTextReaderImpl.curNode.SetValueNode(textNodeType2, xmlTextReaderImpl.stringBuilder.ToString());
					xmlTextReaderImpl.stringBuilder.Length = 0;
					result3 = true;
					goto IL_5BA;
					IL_35F:
					awaiter = xmlTextReaderImpl.ParseTextAsync(num2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseTextAsync>d__535>(ref awaiter, ref this);
						return;
					}
					IL_3C2:
					Tuple<int, int, int, bool> result5 = awaiter.GetResult();
					item = result5.Item1;
					item2 = result5.Item2;
					num2 = result5.Item3;
					bool item3 = result5.Item4;
					if (item2 - item > 0)
					{
						xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
					}
					if (!item3 && num2 <= 32 && xmlTextReaderImpl.stringBuilder.Length < 4096)
					{
						goto IL_35F;
					}
					XmlNodeType xmlNodeType = (xmlTextReaderImpl.stringBuilder.Length < 4096) ? xmlTextReaderImpl.GetTextNodeType(num2) : XmlNodeType.Text;
					if (xmlNodeType != XmlNodeType.None)
					{
						xmlTextReaderImpl.curNode.SetValueNode(xmlNodeType, xmlTextReaderImpl.stringBuilder.ToString());
						xmlTextReaderImpl.stringBuilder.Length = 0;
						if (!item3)
						{
							xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.parsingFunction;
							xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.PartialTextValue;
						}
						result3 = true;
						goto IL_5BA;
					}
					xmlTextReaderImpl.stringBuilder.Length = 0;
					if (item3)
					{
						goto IL_539;
					}
					IL_462:
					awaiter = xmlTextReaderImpl.ParseTextAsync(num2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseTextAsync>d__535>(ref awaiter, ref this);
						return;
					}
					IL_4C5:
					Tuple<int, int, int, bool> result6 = awaiter.GetResult();
					item = result6.Item1;
					item2 = result6.Item2;
					num2 = result6.Item3;
					if (!result6.Item4)
					{
						goto IL_462;
					}
					IL_539:
					awaiter2 = xmlTextReaderImpl.ParseTextAsync_IgnoreNode().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<_ParseTextAsync>d__535>(ref awaiter2, ref this);
						return;
					}
					IL_597:
					result3 = awaiter2.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_5BA:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result3);
			}

			// Token: 0x060009DE RID: 2526 RVA: 0x0003C6C8 File Offset: 0x0003A8C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000ADC RID: 2780
			public int <>1__state;

			// Token: 0x04000ADD RID: 2781
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000ADE RID: 2782
			public Task<Tuple<int, int, int, bool>> parseTask;

			// Token: 0x04000ADF RID: 2783
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AE0 RID: 2784
			private ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000AE1 RID: 2785
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000EF RID: 239
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseTextAsync_AsyncFunc>d__543 : IAsyncStateMachine
		{
			// Token: 0x060009DF RID: 2527 RVA: 0x0003C6D8 File Offset: 0x0003A8D8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, int, int, bool> result;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1EB;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_260;
					default:
						IL_20:
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseTextAsync_AsyncFunc>d__543>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					int outOrChars = xmlTextReaderImpl.lastParseTextState.outOrChars;
					char[] chars = xmlTextReaderImpl.lastParseTextState.chars;
					int pos = xmlTextReaderImpl.lastParseTextState.pos;
					int rcount = xmlTextReaderImpl.lastParseTextState.rcount;
					int rpos = xmlTextReaderImpl.lastParseTextState.rpos;
					int orChars = xmlTextReaderImpl.lastParseTextState.orChars;
					char c = xmlTextReaderImpl.lastParseTextState.c;
					switch (xmlTextReaderImpl.parseText_NextFunction)
					{
					case XmlTextReaderImpl.ParseTextFunction.ParseText:
						this.task = xmlTextReaderImpl.ParseTextAsync(outOrChars, chars, pos, rcount, rpos, orChars, c);
						goto IL_20;
					case XmlTextReaderImpl.ParseTextFunction.Entity:
						this.task = xmlTextReaderImpl.ParseTextAsync_ParseEntity(outOrChars, chars, pos, rcount, rpos, orChars, c);
						goto IL_20;
					case XmlTextReaderImpl.ParseTextFunction.Surrogate:
						this.task = xmlTextReaderImpl.ParseTextAsync_Surrogate(outOrChars, chars, pos, rcount, rpos, orChars, c);
						goto IL_20;
					case XmlTextReaderImpl.ParseTextFunction.ReadData:
						this.task = xmlTextReaderImpl.ParseTextAsync_ReadData(outOrChars, chars, pos, rcount, rpos, orChars, c);
						goto IL_20;
					case XmlTextReaderImpl.ParseTextFunction.NoValue:
						awaiter = xmlTextReaderImpl.ParseTextAsync_NoValue(outOrChars, pos).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseTextAsync_AsyncFunc>d__543>(ref awaiter, ref this);
							return;
						}
						break;
					case XmlTextReaderImpl.ParseTextFunction.PartialValue:
						awaiter = xmlTextReaderImpl.ParseTextAsync_PartialValue(pos, rcount, rpos, orChars, c).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseTextAsync_AsyncFunc>d__543>(ref awaiter, ref this);
							return;
						}
						goto IL_260;
					default:
						goto IL_20;
					}
					IL_1EB:
					result = awaiter.GetResult();
					goto IL_283;
					IL_260:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_283:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009E0 RID: 2528 RVA: 0x0003C998 File Offset: 0x0003AB98
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AE2 RID: 2786
			public int <>1__state;

			// Token: 0x04000AE3 RID: 2787
			public AsyncTaskMethodBuilder<Tuple<int, int, int, bool>> <>t__builder;

			// Token: 0x04000AE4 RID: 2788
			public Task<Tuple<int, int, int, bool>> task;

			// Token: 0x04000AE5 RID: 2789
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AE6 RID: 2790
			private ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000F0 RID: 240
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseTextAsync_ParseEntity>d__545 : IAsyncStateMachine
		{
			// Token: 0x060009E1 RID: 2529 RVA: 0x0003C9A8 File Offset: 0x0003ABA8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, int, int, bool> result;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						int num3;
						XmlTextReaderImpl.EntityType entityType;
						int num2;
						if ((num2 = xmlTextReaderImpl.ParseCharRefInline(this.pos, out num3, out entityType)) > 0)
						{
							if (this.rcount > 0)
							{
								xmlTextReaderImpl.ShiftBuffer(this.rpos + this.rcount, this.rpos, this.pos - this.rpos - this.rcount);
							}
							this.rpos = this.pos - this.rcount;
							this.rcount += num2 - this.pos - num3;
							this.pos = num2;
							if (!xmlTextReaderImpl.xmlCharType.IsWhiteSpace(this.chars[num2 - num3]) || (xmlTextReaderImpl.v1Compat && entityType == XmlTextReaderImpl.EntityType.CharacterDec))
							{
								this.orChars |= 255;
								goto IL_2B3;
							}
							goto IL_2B3;
						}
						else
						{
							if (this.pos > xmlTextReaderImpl.ps.charPos)
							{
								xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
								xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.PartialValue;
								result = xmlTextReaderImpl.parseText_dummyTask.Result;
								goto IL_316;
							}
							awaiter = xmlTextReaderImpl.HandleEntityReferenceAsync(false, XmlTextReaderImpl.EntityExpandType.All).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseTextAsync_ParseEntity>d__545>(ref awaiter, ref this);
								return;
							}
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Tuple<int, XmlTextReaderImpl.EntityType> result2 = awaiter.GetResult();
					this.pos = result2.Item1;
					switch (result2.Item2)
					{
					case XmlTextReaderImpl.EntityType.CharacterDec:
						if (xmlTextReaderImpl.v1Compat)
						{
							this.orChars |= 255;
							goto IL_2A2;
						}
						break;
					case XmlTextReaderImpl.EntityType.CharacterHex:
					case XmlTextReaderImpl.EntityType.CharacterNamed:
						break;
					case XmlTextReaderImpl.EntityType.Expanded:
					case XmlTextReaderImpl.EntityType.Skipped:
					case XmlTextReaderImpl.EntityType.FakeExpanded:
						goto IL_291;
					case XmlTextReaderImpl.EntityType.Unexpanded:
						xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.parsingFunction;
						xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.EntityReference;
						xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
						xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.NoValue;
						result = xmlTextReaderImpl.parseText_dummyTask.Result;
						goto IL_316;
					default:
						goto IL_291;
					}
					if (!xmlTextReaderImpl.xmlCharType.IsWhiteSpace(xmlTextReaderImpl.ps.chars[this.pos - 1]))
					{
						this.orChars |= 255;
						goto IL_2A2;
					}
					goto IL_2A2;
					IL_291:
					this.pos = xmlTextReaderImpl.ps.charPos;
					IL_2A2:
					this.chars = xmlTextReaderImpl.ps.chars;
					IL_2B3:
					xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
					xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ParseText;
					result = xmlTextReaderImpl.parseText_dummyTask.Result;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_316:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009E2 RID: 2530 RVA: 0x0003CCFC File Offset: 0x0003AEFC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AE7 RID: 2791
			public int <>1__state;

			// Token: 0x04000AE8 RID: 2792
			public AsyncTaskMethodBuilder<Tuple<int, int, int, bool>> <>t__builder;

			// Token: 0x04000AE9 RID: 2793
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AEA RID: 2794
			public int pos;

			// Token: 0x04000AEB RID: 2795
			public int rcount;

			// Token: 0x04000AEC RID: 2796
			public int rpos;

			// Token: 0x04000AED RID: 2797
			public char[] chars;

			// Token: 0x04000AEE RID: 2798
			public int orChars;

			// Token: 0x04000AEF RID: 2799
			public int outOrChars;

			// Token: 0x04000AF0 RID: 2800
			public char c;

			// Token: 0x04000AF1 RID: 2801
			private ConfiguredTaskAwaitable<Tuple<int, XmlTextReaderImpl.EntityType>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000F1 RID: 241
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseTextAsync_Surrogate>d__546 : IAsyncStateMachine
		{
			// Token: 0x060009E3 RID: 2531 RVA: 0x0003CD0C File Offset: 0x0003AF0C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, int, int, bool> result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						char c = this.chars[this.pos];
						if (XmlCharType.IsHighSurrogate((int)c))
						{
							if (this.pos + 1 == xmlTextReaderImpl.ps.charsUsed)
							{
								xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
								xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ReadData;
								result = xmlTextReaderImpl.parseText_dummyTask.Result;
								goto IL_26A;
							}
							int num2 = this.pos;
							this.pos = num2 + 1;
							if (XmlCharType.IsLowSurrogate((int)this.chars[this.pos]))
							{
								num2 = this.pos;
								this.pos = num2 + 1;
								this.orChars |= (int)c;
								xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
								xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ParseText;
								result = xmlTextReaderImpl.parseText_dummyTask.Result;
								goto IL_26A;
							}
						}
						this.<offset>5__2 = this.pos - xmlTextReaderImpl.ps.charPos;
						awaiter = xmlTextReaderImpl.ZeroEndingStreamAsync(this.pos).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseTextAsync_Surrogate>d__546>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (!awaiter.GetResult())
					{
						xmlTextReaderImpl.ThrowInvalidChar(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charsUsed, xmlTextReaderImpl.ps.charPos + this.<offset>5__2);
						throw new Exception();
					}
					this.chars = xmlTextReaderImpl.ps.chars;
					this.pos = xmlTextReaderImpl.ps.charPos + this.<offset>5__2;
					xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
					xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.PartialValue;
					result = xmlTextReaderImpl.parseText_dummyTask.Result;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_26A:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009E4 RID: 2532 RVA: 0x0003CFB4 File Offset: 0x0003B1B4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AF2 RID: 2802
			public int <>1__state;

			// Token: 0x04000AF3 RID: 2803
			public AsyncTaskMethodBuilder<Tuple<int, int, int, bool>> <>t__builder;

			// Token: 0x04000AF4 RID: 2804
			public char[] chars;

			// Token: 0x04000AF5 RID: 2805
			public int pos;

			// Token: 0x04000AF6 RID: 2806
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000AF7 RID: 2807
			public int outOrChars;

			// Token: 0x04000AF8 RID: 2808
			public int rcount;

			// Token: 0x04000AF9 RID: 2809
			public int rpos;

			// Token: 0x04000AFA RID: 2810
			public int orChars;

			// Token: 0x04000AFB RID: 2811
			public char c;

			// Token: 0x04000AFC RID: 2812
			private int <offset>5__2;

			// Token: 0x04000AFD RID: 2813
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000F2 RID: 242
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseTextAsync_ReadData>d__547 : IAsyncStateMachine
		{
			// Token: 0x060009E5 RID: 2533 RVA: 0x0003CFC4 File Offset: 0x0003B1C4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, int, int, bool> result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (this.pos > xmlTextReaderImpl.ps.charPos)
						{
							xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
							xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.PartialValue;
							result = xmlTextReaderImpl.parseText_dummyTask.Result;
							goto IL_288;
						}
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseTextAsync_ReadData>d__547>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter.GetResult() == 0)
					{
						if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos > 0)
						{
							if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != '\r' && xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != ']')
							{
								xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							if (!xmlTextReaderImpl.InEntity)
							{
								xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
								xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.NoValue;
								result = xmlTextReaderImpl.parseText_dummyTask.Result;
								goto IL_288;
							}
							if (xmlTextReaderImpl.HandleEntityEnd(true))
							{
								xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.parsingFunction;
								xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.ReportEndEntity;
								xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
								xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.NoValue;
								result = xmlTextReaderImpl.parseText_dummyTask.Result;
								goto IL_288;
							}
						}
					}
					this.pos = xmlTextReaderImpl.ps.charPos;
					this.chars = xmlTextReaderImpl.ps.chars;
					xmlTextReaderImpl.lastParseTextState = new XmlTextReaderImpl.ParseTextState(this.outOrChars, this.chars, this.pos, this.rcount, this.rpos, this.orChars, this.c);
					xmlTextReaderImpl.parseText_NextFunction = XmlTextReaderImpl.ParseTextFunction.ParseText;
					result = xmlTextReaderImpl.parseText_dummyTask.Result;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_288:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009E6 RID: 2534 RVA: 0x0003D28C File Offset: 0x0003B48C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000AFE RID: 2814
			public int <>1__state;

			// Token: 0x04000AFF RID: 2815
			public AsyncTaskMethodBuilder<Tuple<int, int, int, bool>> <>t__builder;

			// Token: 0x04000B00 RID: 2816
			public int pos;

			// Token: 0x04000B01 RID: 2817
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B02 RID: 2818
			public int outOrChars;

			// Token: 0x04000B03 RID: 2819
			public char[] chars;

			// Token: 0x04000B04 RID: 2820
			public int rcount;

			// Token: 0x04000B05 RID: 2821
			public int rpos;

			// Token: 0x04000B06 RID: 2822
			public int orChars;

			// Token: 0x04000B07 RID: 2823
			public char c;

			// Token: 0x04000B08 RID: 2824
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000F3 RID: 243
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishPartialValueAsync>d__550 : IAsyncStateMachine
		{
			// Token: 0x060009E7 RID: 2535 RVA: 0x0003D29C File Offset: 0x0003B49C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter awaiter;
					int outOrChars;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_13A;
						}
						xmlTextReaderImpl.curNode.CopyTo(xmlTextReaderImpl.readValueOffset, xmlTextReaderImpl.stringBuilder);
						outOrChars = 0;
						awaiter = xmlTextReaderImpl.ParseTextAsync(outOrChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishPartialValueAsync>d__550>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					Tuple<int, int, int, bool> result = awaiter.GetResult();
					int item = result.Item1;
					int item2 = result.Item2;
					outOrChars = result.Item3;
					goto IL_15C;
					IL_13A:
					result = awaiter.GetResult();
					item = result.Item1;
					item2 = result.Item2;
					outOrChars = result.Item3;
					IL_15C:
					if (result.Item4)
					{
						xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
						xmlTextReaderImpl.curNode.SetValue(xmlTextReaderImpl.stringBuilder.ToString());
						xmlTextReaderImpl.stringBuilder.Length = 0;
					}
					else
					{
						xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
						awaiter = xmlTextReaderImpl.ParseTextAsync(outOrChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishPartialValueAsync>d__550>(ref awaiter, ref this);
							return;
						}
						goto IL_13A;
					}
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

			// Token: 0x060009E8 RID: 2536 RVA: 0x0003D498 File Offset: 0x0003B698
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B09 RID: 2825
			public int <>1__state;

			// Token: 0x04000B0A RID: 2826
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B0B RID: 2827
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B0C RID: 2828
			private ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000F4 RID: 244
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishOtherValueIteratorAsync>d__551 : IAsyncStateMachine
		{
			// Token: 0x060009E9 RID: 2537 RVA: 0x0003D4A8 File Offset: 0x0003B6A8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							switch (xmlTextReaderImpl.parsingFunction)
							{
							case XmlTextReaderImpl.ParsingFunction.InReadAttributeValue:
								goto IL_1C7;
							case XmlTextReaderImpl.ParsingFunction.InReadValueChunk:
								if (xmlTextReaderImpl.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnPartialValue)
								{
									awaiter = xmlTextReaderImpl.FinishPartialValueAsync().ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 0;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishOtherValueIteratorAsync>d__551>(ref awaiter, ref this);
										return;
									}
									goto IL_A5;
								}
								else
								{
									if (xmlTextReaderImpl.readValueOffset > 0)
									{
										xmlTextReaderImpl.curNode.SetValue(xmlTextReaderImpl.curNode.StringValue.Substring(xmlTextReaderImpl.readValueOffset));
										xmlTextReaderImpl.readValueOffset = 0;
										goto IL_1C7;
									}
									goto IL_1C7;
								}
								break;
							case XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary:
							case XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary:
								switch (xmlTextReaderImpl.incReadState)
								{
								case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue:
									if (xmlTextReaderImpl.readValueOffset > 0)
									{
										xmlTextReaderImpl.curNode.SetValue(xmlTextReaderImpl.curNode.StringValue.Substring(xmlTextReaderImpl.readValueOffset));
										xmlTextReaderImpl.readValueOffset = 0;
										goto IL_1C7;
									}
									goto IL_1C7;
								case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue:
									awaiter = xmlTextReaderImpl.FinishPartialValueAsync().ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										this.<>1__state = 1;
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishOtherValueIteratorAsync>d__551>(ref awaiter, ref this);
										return;
									}
									break;
								case XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End:
									xmlTextReaderImpl.curNode.SetValue(string.Empty);
									goto IL_1C7;
								default:
									goto IL_1C7;
								}
								break;
							default:
								goto IL_1C7;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						xmlTextReaderImpl.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue;
						goto IL_1C7;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_A5:
					awaiter.GetResult();
					xmlTextReaderImpl.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadValueChunk_OnCachedValue;
					IL_1C7:;
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

			// Token: 0x060009EA RID: 2538 RVA: 0x0003D6C8 File Offset: 0x0003B8C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B0D RID: 2829
			public int <>1__state;

			// Token: 0x04000B0E RID: 2830
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B0F RID: 2831
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B10 RID: 2832
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000F5 RID: 245
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SkipPartialTextValueAsync>d__552 : IAsyncStateMachine
		{
			// Token: 0x060009EB RID: 2539 RVA: 0x0003D6D8 File Offset: 0x0003B8D8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_7E;
					}
					int outOrChars = 0;
					xmlTextReaderImpl.parsingFunction = xmlTextReaderImpl.nextParsingFunction;
					IL_1F:
					awaiter = xmlTextReaderImpl.ParseTextAsync(outOrChars).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipPartialTextValueAsync>d__552>(ref awaiter, ref this);
						return;
					}
					IL_7E:
					Tuple<int, int, int, bool> result = awaiter.GetResult();
					int item = result.Item1;
					int item2 = result.Item2;
					outOrChars = result.Item3;
					if (!result.Item4)
					{
						goto IL_1F;
					}
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

			// Token: 0x060009EC RID: 2540 RVA: 0x0003D7CC File Offset: 0x0003B9CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B11 RID: 2833
			public int <>1__state;

			// Token: 0x04000B12 RID: 2834
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B13 RID: 2835
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B14 RID: 2836
			private ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000F6 RID: 246
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishReadContentAsBinaryAsync>d__554 : IAsyncStateMachine
		{
			// Token: 0x060009ED RID: 2541 RVA: 0x0003D7DC File Offset: 0x0003B9DC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_110;
						}
						xmlTextReaderImpl.readValueOffset = 0;
						if (xmlTextReaderImpl.incReadState != XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue)
						{
							xmlTextReaderImpl.parsingFunction = xmlTextReaderImpl.nextParsingFunction;
							xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.nextNextParsingFunction;
							goto IL_A7;
						}
						awaiter2 = xmlTextReaderImpl.SkipPartialTextValueAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishReadContentAsBinaryAsync>d__554>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					IL_A7:
					if (xmlTextReaderImpl.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End)
					{
						goto IL_119;
					}
					IL_B1:
					awaiter = xmlTextReaderImpl.MoveToNextContentNodeAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishReadContentAsBinaryAsync>d__554>(ref awaiter, ref this);
						return;
					}
					IL_110:
					if (awaiter.GetResult())
					{
						goto IL_B1;
					}
					IL_119:;
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

			// Token: 0x060009EE RID: 2542 RVA: 0x0003D94C File Offset: 0x0003BB4C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B15 RID: 2837
			public int <>1__state;

			// Token: 0x04000B16 RID: 2838
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B17 RID: 2839
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B18 RID: 2840
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000B19 RID: 2841
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000F7 RID: 247
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FinishReadElementContentAsBinaryAsync>d__555 : IAsyncStateMachine
		{
			// Token: 0x060009EF RID: 2543 RVA: 0x0003D95C File Offset: 0x0003BB5C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_10F;
						}
						awaiter2 = xmlTextReaderImpl.FinishReadContentAsBinaryAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishReadElementContentAsBinaryAsync>d__555>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					if (xmlTextReaderImpl.curNode.type != XmlNodeType.EndElement)
					{
						xmlTextReaderImpl.Throw("'{0}' is an invalid XmlNodeType.", xmlTextReaderImpl.curNode.type.ToString());
					}
					awaiter = xmlTextReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<FinishReadElementContentAsBinaryAsync>d__555>(ref awaiter, ref this);
						return;
					}
					IL_10F:
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

			// Token: 0x060009F0 RID: 2544 RVA: 0x0003DACC File Offset: 0x0003BCCC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B1A RID: 2842
			public int <>1__state;

			// Token: 0x04000B1B RID: 2843
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B1C RID: 2844
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B1D RID: 2845
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000B1E RID: 2846
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000F8 RID: 248
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseRootLevelWhitespaceAsync>d__556 : IAsyncStateMachine
		{
			// Token: 0x060009F1 RID: 2545 RVA: 0x0003DADC File Offset: 0x0003BCDC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_14C;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1E8;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_29A;
					default:
						this.<nodeType>5__2 = xmlTextReaderImpl.GetWhitespaceType();
						if (this.<nodeType>5__2 == XmlNodeType.None)
						{
							awaiter = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseRootLevelWhitespaceAsync>d__556>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							xmlTextReaderImpl.curNode.SetLineInfo(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
							awaiter = xmlTextReaderImpl.EatWhitespacesAsync(xmlTextReaderImpl.stringBuilder).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseRootLevelWhitespaceAsync>d__556>(ref awaiter, ref this);
								return;
							}
							goto IL_1E8;
						}
						break;
					}
					awaiter.GetResult();
					bool flag = xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] == '<' || xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos == 0;
					if (flag)
					{
						goto IL_155;
					}
					awaiter2 = xmlTextReaderImpl.ZeroEndingStreamAsync(xmlTextReaderImpl.ps.charPos).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseRootLevelWhitespaceAsync>d__556>(ref awaiter2, ref this);
						return;
					}
					IL_14C:
					flag = awaiter2.GetResult();
					IL_155:
					if (flag)
					{
						result = false;
						goto IL_35A;
					}
					goto IL_2E5;
					IL_1E8:
					awaiter.GetResult();
					flag = (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] == '<' || xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos == 0);
					if (flag)
					{
						goto IL_2A3;
					}
					awaiter2 = xmlTextReaderImpl.ZeroEndingStreamAsync(xmlTextReaderImpl.ps.charPos).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseRootLevelWhitespaceAsync>d__556>(ref awaiter2, ref this);
						return;
					}
					IL_29A:
					flag = awaiter2.GetResult();
					IL_2A3:
					if (flag)
					{
						if (xmlTextReaderImpl.stringBuilder.Length > 0)
						{
							xmlTextReaderImpl.curNode.SetValueNode(this.<nodeType>5__2, xmlTextReaderImpl.stringBuilder.ToString());
							xmlTextReaderImpl.stringBuilder.Length = 0;
							result = true;
							goto IL_35A;
						}
						result = false;
						goto IL_35A;
					}
					IL_2E5:
					if (xmlTextReaderImpl.xmlCharType.IsCharData(xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos]))
					{
						xmlTextReaderImpl.Throw("Data at the root level is invalid.");
					}
					else
					{
						xmlTextReaderImpl.ThrowInvalidChar(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charsUsed, xmlTextReaderImpl.ps.charPos);
					}
					result = false;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_35A:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009F2 RID: 2546 RVA: 0x0003DE74 File Offset: 0x0003C074
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B1F RID: 2847
			public int <>1__state;

			// Token: 0x04000B20 RID: 2848
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000B21 RID: 2849
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B22 RID: 2850
			private XmlNodeType <nodeType>5__2;

			// Token: 0x04000B23 RID: 2851
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000B24 RID: 2852
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000F9 RID: 249
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseEntityReferenceAsync>d__557 : IAsyncStateMachine
		{
			// Token: 0x060009F3 RID: 2547 RVA: 0x0003DE84 File Offset: 0x0003C084
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
						xmlTextReaderImpl2.ps.charPos = xmlTextReaderImpl2.ps.charPos + 1;
						xmlTextReaderImpl.curNode.SetLineInfo(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
						this.<>7__wrap1 = xmlTextReaderImpl.curNode;
						awaiter = xmlTextReaderImpl.ParseEntityNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseEntityReferenceAsync>d__557>(ref awaiter, ref this);
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
					this.<>7__wrap1.SetNamedNode(XmlNodeType.EntityReference, result);
					this.<>7__wrap1 = null;
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

			// Token: 0x060009F4 RID: 2548 RVA: 0x0003DF94 File Offset: 0x0003C194
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B25 RID: 2853
			public int <>1__state;

			// Token: 0x04000B26 RID: 2854
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B27 RID: 2855
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B28 RID: 2856
			private XmlTextReaderImpl.NodeData <>7__wrap1;

			// Token: 0x04000B29 RID: 2857
			private ConfiguredTaskAwaitable<string>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000FA RID: 250
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <HandleEntityReferenceAsync>d__558 : IAsyncStateMachine
		{
			// Token: 0x060009F5 RID: 2549 RVA: 0x0003DFA4 File Offset: 0x0003C1A4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, XmlTextReaderImpl.EntityType> result2;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<Tuple<XmlTextReaderImpl.EntityType, int>>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<XmlTextReaderImpl.EntityType>.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<Tuple<XmlTextReaderImpl.EntityType, int>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_142;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_1DF;
					case 3:
					{
						IL_260:
						int result;
						try
						{
							if (num != 3)
							{
								awaiter = xmlTextReaderImpl.ParseNameAsync().ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 3;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<HandleEntityReferenceAsync>d__558>(ref awaiter, ref this);
									return;
								}
							}
							else
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
								this.<>1__state = -1;
							}
							result = awaiter.GetResult();
						}
						catch (XmlException)
						{
							xmlTextReaderImpl.Throw("An error occurred while parsing EntityName.", xmlTextReaderImpl.ps.LineNo, this.<savedLinePos>5__3);
							result2 = new Tuple<int, XmlTextReaderImpl.EntityType>(this.<charRefEndPos>5__2, XmlTextReaderImpl.EntityType.Skipped);
							goto IL_430;
						}
						if (xmlTextReaderImpl.ps.chars[result] != ';')
						{
							xmlTextReaderImpl.ThrowUnexpectedToken(result, ";");
						}
						int linePos = xmlTextReaderImpl.ps.LinePos;
						string name = xmlTextReaderImpl.nameTable.Add(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, result - xmlTextReaderImpl.ps.charPos);
						xmlTextReaderImpl.ps.charPos = result + 1;
						this.<charRefEndPos>5__2 = -1;
						awaiter3 = xmlTextReaderImpl.HandleGeneralEntityReferenceAsync(name, this.isInAttributeValue, false, linePos).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							this.<>1__state = 4;
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<XmlTextReaderImpl.EntityType>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<HandleEntityReferenceAsync>d__558>(ref awaiter3, ref this);
							return;
						}
						goto IL_3DC;
					}
					case 4:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<XmlTextReaderImpl.EntityType>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3DC;
					default:
						if (xmlTextReaderImpl.ps.charPos + 1 != xmlTextReaderImpl.ps.charsUsed)
						{
							goto IL_B4;
						}
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<HandleEntityReferenceAsync>d__558>(ref awaiter, ref this);
							return;
						}
						break;
					}
					if (awaiter.GetResult() == 0)
					{
						xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
					}
					IL_B4:
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos + 1] == '#')
					{
						awaiter2 = xmlTextReaderImpl.ParseNumericCharRefAsync(this.expandType != XmlTextReaderImpl.EntityExpandType.OnlyGeneral, null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<XmlTextReaderImpl.EntityType, int>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<HandleEntityReferenceAsync>d__558>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter = xmlTextReaderImpl.ParseNamedCharRefAsync(this.expandType != XmlTextReaderImpl.EntityExpandType.OnlyGeneral, null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<HandleEntityReferenceAsync>d__558>(ref awaiter, ref this);
							return;
						}
						goto IL_1DF;
					}
					IL_142:
					Tuple<XmlTextReaderImpl.EntityType, int> result3 = awaiter2.GetResult();
					XmlTextReaderImpl.EntityType item = result3.Item1;
					this.<charRefEndPos>5__2 = result3.Item2;
					result2 = new Tuple<int, XmlTextReaderImpl.EntityType>(this.<charRefEndPos>5__2, item);
					goto IL_430;
					IL_1DF:
					int result4 = awaiter.GetResult();
					this.<charRefEndPos>5__2 = result4;
					if (this.<charRefEndPos>5__2 >= 0)
					{
						result2 = new Tuple<int, XmlTextReaderImpl.EntityType>(this.<charRefEndPos>5__2, XmlTextReaderImpl.EntityType.CharacterNamed);
						goto IL_430;
					}
					if (this.expandType == XmlTextReaderImpl.EntityExpandType.OnlyCharacter || (xmlTextReaderImpl.entityHandling != EntityHandling.ExpandEntities && (!this.isInAttributeValue || !xmlTextReaderImpl.validatingReaderCompatFlag)))
					{
						result2 = new Tuple<int, XmlTextReaderImpl.EntityType>(this.<charRefEndPos>5__2, XmlTextReaderImpl.EntityType.Unexpanded);
						goto IL_430;
					}
					XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
					xmlTextReaderImpl2.ps.charPos = xmlTextReaderImpl2.ps.charPos + 1;
					this.<savedLinePos>5__3 = xmlTextReaderImpl.ps.LinePos;
					goto IL_260;
					IL_3DC:
					XmlTextReaderImpl.EntityType result5 = awaiter3.GetResult();
					xmlTextReaderImpl.reportedBaseUri = xmlTextReaderImpl.ps.baseUriStr;
					xmlTextReaderImpl.reportedEncoding = xmlTextReaderImpl.ps.encoding;
					result2 = new Tuple<int, XmlTextReaderImpl.EntityType>(this.<charRefEndPos>5__2, result5);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_430:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x060009F6 RID: 2550 RVA: 0x0003E42C File Offset: 0x0003C62C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B2A RID: 2858
			public int <>1__state;

			// Token: 0x04000B2B RID: 2859
			public AsyncTaskMethodBuilder<Tuple<int, XmlTextReaderImpl.EntityType>> <>t__builder;

			// Token: 0x04000B2C RID: 2860
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B2D RID: 2861
			public XmlTextReaderImpl.EntityExpandType expandType;

			// Token: 0x04000B2E RID: 2862
			public bool isInAttributeValue;

			// Token: 0x04000B2F RID: 2863
			private int <charRefEndPos>5__2;

			// Token: 0x04000B30 RID: 2864
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000B31 RID: 2865
			private ConfiguredTaskAwaitable<Tuple<XmlTextReaderImpl.EntityType, int>>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000B32 RID: 2866
			private int <savedLinePos>5__3;

			// Token: 0x04000B33 RID: 2867
			private ConfiguredTaskAwaitable<XmlTextReaderImpl.EntityType>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x020000FB RID: 251
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <HandleGeneralEntityReferenceAsync>d__559 : IAsyncStateMachine
		{
			// Token: 0x060009F7 RID: 2551 RVA: 0x0003E43C File Offset: 0x0003C63C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				XmlTextReaderImpl.EntityType result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_298;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_32B;
					default:
						this.<entity>5__2 = null;
						if (xmlTextReaderImpl.dtdInfo != null || xmlTextReaderImpl.fragmentParserContext == null || !xmlTextReaderImpl.fragmentParserContext.HasDtdInfo || xmlTextReaderImpl.dtdProcessing != DtdProcessing.Parse)
						{
							goto IL_B5;
						}
						awaiter = xmlTextReaderImpl.ParseDtdFromParserContextAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<HandleGeneralEntityReferenceAsync>d__559>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					IL_B5:
					if (xmlTextReaderImpl.dtdInfo == null || (this.<entity>5__2 = xmlTextReaderImpl.dtdInfo.LookupEntity(this.name)) == null)
					{
						if (xmlTextReaderImpl.disableUndeclaredEntityCheck)
						{
							this.<entity>5__2 = new SchemaEntity(new XmlQualifiedName(this.name), false)
							{
								Text = string.Empty
							};
						}
						else
						{
							xmlTextReaderImpl.Throw("Reference to undeclared entity '{0}'.", this.name, xmlTextReaderImpl.ps.LineNo, this.entityStartLinePos);
						}
					}
					if (this.<entity>5__2.IsUnparsedEntity)
					{
						if (xmlTextReaderImpl.disableUndeclaredEntityCheck)
						{
							this.<entity>5__2 = new SchemaEntity(new XmlQualifiedName(this.name), false)
							{
								Text = string.Empty
							};
						}
						else
						{
							xmlTextReaderImpl.Throw("Reference to unparsed entity '{0}'.", this.name, xmlTextReaderImpl.ps.LineNo, this.entityStartLinePos);
						}
					}
					if (xmlTextReaderImpl.standalone && this.<entity>5__2.IsDeclaredInExternal)
					{
						xmlTextReaderImpl.Throw("Standalone document declaration must have a value of 'no' because an external entity '{0}' is referenced.", this.<entity>5__2.Name, xmlTextReaderImpl.ps.LineNo, this.entityStartLinePos);
					}
					if (this.<entity>5__2.IsExternal)
					{
						if (this.isInAttributeValue)
						{
							xmlTextReaderImpl.Throw("External entity '{0}' reference cannot appear in the attribute value.", this.name, xmlTextReaderImpl.ps.LineNo, this.entityStartLinePos);
							result = XmlTextReaderImpl.EntityType.Skipped;
							goto IL_3C6;
						}
						if (xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.SkipContent)
						{
							result = XmlTextReaderImpl.EntityType.Skipped;
							goto IL_3C6;
						}
						if (xmlTextReaderImpl.IsResolverNull)
						{
							if (!this.pushFakeEntityIfNullResolver)
							{
								result = XmlTextReaderImpl.EntityType.Skipped;
								goto IL_3C6;
							}
							awaiter2 = xmlTextReaderImpl.PushExternalEntityAsync(this.<entity>5__2).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<HandleGeneralEntityReferenceAsync>d__559>(ref awaiter2, ref this);
								return;
							}
						}
						else
						{
							awaiter2 = xmlTextReaderImpl.PushExternalEntityAsync(this.<entity>5__2).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__2 = awaiter2;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<HandleGeneralEntityReferenceAsync>d__559>(ref awaiter2, ref this);
								return;
							}
							goto IL_32B;
						}
					}
					else
					{
						if (xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.SkipContent)
						{
							result = XmlTextReaderImpl.EntityType.Skipped;
							goto IL_3C6;
						}
						xmlTextReaderImpl.PushInternalEntity(this.<entity>5__2);
						xmlTextReaderImpl.curNode.entityId = xmlTextReaderImpl.ps.entityId;
						result = ((this.isInAttributeValue && xmlTextReaderImpl.validatingReaderCompatFlag) ? XmlTextReaderImpl.EntityType.ExpandedInAttribute : XmlTextReaderImpl.EntityType.Expanded);
						goto IL_3C6;
					}
					IL_298:
					awaiter2.GetResult();
					xmlTextReaderImpl.curNode.entityId = xmlTextReaderImpl.ps.entityId;
					result = XmlTextReaderImpl.EntityType.FakeExpanded;
					goto IL_3C6;
					IL_32B:
					awaiter2.GetResult();
					xmlTextReaderImpl.curNode.entityId = xmlTextReaderImpl.ps.entityId;
					result = ((this.isInAttributeValue && xmlTextReaderImpl.validatingReaderCompatFlag) ? XmlTextReaderImpl.EntityType.ExpandedInAttribute : XmlTextReaderImpl.EntityType.Expanded);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<entity>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3C6:
				this.<>1__state = -2;
				this.<entity>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009F8 RID: 2552 RVA: 0x0003E848 File Offset: 0x0003CA48
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B34 RID: 2868
			public int <>1__state;

			// Token: 0x04000B35 RID: 2869
			public AsyncTaskMethodBuilder<XmlTextReaderImpl.EntityType> <>t__builder;

			// Token: 0x04000B36 RID: 2870
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B37 RID: 2871
			public string name;

			// Token: 0x04000B38 RID: 2872
			public int entityStartLinePos;

			// Token: 0x04000B39 RID: 2873
			public bool isInAttributeValue;

			// Token: 0x04000B3A RID: 2874
			public bool pushFakeEntityIfNullResolver;

			// Token: 0x04000B3B RID: 2875
			private IDtdEntityInfo <entity>5__2;

			// Token: 0x04000B3C RID: 2876
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000B3D RID: 2877
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x020000FC RID: 252
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParsePIAsync>d__561 : IAsyncStateMachine
		{
			// Token: 0x060009F9 RID: 2553 RVA: 0x0003E858 File Offset: 0x0003CA58
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result3;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1E5;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_26C;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_32D;
					case 4:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_431;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4F8;
					default:
						if (xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
						{
							xmlTextReaderImpl.curNode.SetLineInfo(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
						}
						awaiter = xmlTextReaderImpl.ParseNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParsePIAsync>d__561>(ref awaiter, ref this);
							return;
						}
						break;
					}
					int result = awaiter.GetResult();
					string text = xmlTextReaderImpl.nameTable.Add(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, result - xmlTextReaderImpl.ps.charPos);
					if (string.Compare(text, "xml", StringComparison.OrdinalIgnoreCase) == 0)
					{
						xmlTextReaderImpl.Throw(text.Equals("xml") ? "Unexpected XML declaration. The XML declaration must be the first node in the document, and no white space characters are allowed to appear before it." : "'{0}' is an invalid name for processing instructions.", text);
					}
					xmlTextReaderImpl.ps.charPos = result;
					if (this.piInDtdStringBuilder == null)
					{
						if (!xmlTextReaderImpl.ignorePIs && xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
						{
							xmlTextReaderImpl.curNode.SetNamedNode(XmlNodeType.ProcessingInstruction, text);
						}
					}
					else
					{
						this.piInDtdStringBuilder.Append(text);
					}
					this.<ch>5__2 = xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos];
					awaiter = xmlTextReaderImpl.EatWhitespacesAsync(this.piInDtdStringBuilder).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParsePIAsync>d__561>(ref awaiter, ref this);
						return;
					}
					IL_1E5:
					if (awaiter.GetResult() != 0)
					{
						goto IL_2CC;
					}
					if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos >= 2)
					{
						goto IL_274;
					}
					awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParsePIAsync>d__561>(ref awaiter, ref this);
						return;
					}
					IL_26C:
					awaiter.GetResult();
					IL_274:
					if (this.<ch>5__2 != '?' || xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos + 1] != '>')
					{
						xmlTextReaderImpl.Throw("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charsUsed, xmlTextReaderImpl.ps.charPos));
					}
					IL_2CC:
					awaiter2 = xmlTextReaderImpl.ParsePIValueAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParsePIAsync>d__561>(ref awaiter2, ref this);
						return;
					}
					IL_32D:
					Tuple<int, int, bool> result2 = awaiter2.GetResult();
					int item = result2.Item1;
					int item2 = result2.Item2;
					if (result2.Item3)
					{
						if (this.piInDtdStringBuilder != null)
						{
							this.piInDtdStringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
							goto IL_56E;
						}
						if (xmlTextReaderImpl.ignorePIs)
						{
							result3 = false;
							goto IL_58B;
						}
						if (xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
						{
							xmlTextReaderImpl.curNode.SetValue(xmlTextReaderImpl.ps.chars, item, item2 - item);
							goto IL_56E;
						}
						goto IL_56E;
					}
					else
					{
						if (this.piInDtdStringBuilder != null)
						{
							this.<sb>5__3 = this.piInDtdStringBuilder;
							goto IL_479;
						}
						if (!xmlTextReaderImpl.ignorePIs && xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
						{
							this.<sb>5__3 = xmlTextReaderImpl.stringBuilder;
							goto IL_479;
						}
					}
					IL_3D0:
					awaiter2 = xmlTextReaderImpl.ParsePIValueAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParsePIAsync>d__561>(ref awaiter2, ref this);
						return;
					}
					IL_431:
					Tuple<int, int, bool> result4 = awaiter2.GetResult();
					item = result4.Item1;
					item2 = result4.Item2;
					if (result4.Item3)
					{
						result3 = false;
						goto IL_58B;
					}
					goto IL_3D0;
					IL_479:
					this.<sb>5__3.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
					awaiter2 = xmlTextReaderImpl.ParsePIValueAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParsePIAsync>d__561>(ref awaiter2, ref this);
						return;
					}
					IL_4F8:
					Tuple<int, int, bool> result5 = awaiter2.GetResult();
					item = result5.Item1;
					item2 = result5.Item2;
					if (!result5.Item3)
					{
						goto IL_479;
					}
					this.<sb>5__3.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
					if (this.piInDtdStringBuilder == null)
					{
						xmlTextReaderImpl.curNode.SetValue(xmlTextReaderImpl.stringBuilder.ToString());
						xmlTextReaderImpl.stringBuilder.Length = 0;
					}
					this.<sb>5__3 = null;
					IL_56E:
					result3 = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_58B:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result3);
			}

			// Token: 0x060009FA RID: 2554 RVA: 0x0003EE20 File Offset: 0x0003D020
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B3E RID: 2878
			public int <>1__state;

			// Token: 0x04000B3F RID: 2879
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000B40 RID: 2880
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B41 RID: 2881
			public StringBuilder piInDtdStringBuilder;

			// Token: 0x04000B42 RID: 2882
			private char <ch>5__2;

			// Token: 0x04000B43 RID: 2883
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000B44 RID: 2884
			private ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000B45 RID: 2885
			private StringBuilder <sb>5__3;
		}

		// Token: 0x020000FD RID: 253
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParsePIValueAsync>d__562 : IAsyncStateMachine
		{
			// Token: 0x060009FB RID: 2555 RVA: 0x0003EE30 File Offset: 0x0003D030
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, int, bool> result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos >= 2)
						{
							goto IL_B3;
						}
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParsePIValueAsync>d__562>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter.GetResult() == 0)
					{
						xmlTextReaderImpl.Throw(xmlTextReaderImpl.ps.charsUsed, "Unexpected end of file while parsing {0} has occurred.", "PI");
					}
					IL_B3:
					int num2 = xmlTextReaderImpl.ps.charPos;
					char[] chars = xmlTextReaderImpl.ps.chars;
					int num3 = 0;
					int num4 = -1;
					for (;;)
					{
						char c;
						if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)(c = chars[num2])] & 64) == 0 || c == '?')
						{
							char c2 = chars[num2];
							if (c2 <= '&')
							{
								switch (c2)
								{
								case '\t':
									break;
								case '\n':
									num2++;
									xmlTextReaderImpl.OnNewLine(num2);
									continue;
								case '\v':
								case '\f':
									goto IL_2A6;
								case '\r':
									if (chars[num2 + 1] == '\n')
									{
										if (!xmlTextReaderImpl.ps.eolNormalized && xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
										{
											if (num2 - xmlTextReaderImpl.ps.charPos > 0)
											{
												if (num3 == 0)
												{
													num3 = 1;
													num4 = num2;
												}
												else
												{
													xmlTextReaderImpl.ShiftBuffer(num4 + num3, num4, num2 - num4 - num3);
													num4 = num2 - num3;
													num3++;
												}
											}
											else
											{
												XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
												xmlTextReaderImpl2.ps.charPos = xmlTextReaderImpl2.ps.charPos + 1;
											}
										}
										num2 += 2;
									}
									else
									{
										if (num2 + 1 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
										{
											goto IL_309;
										}
										if (!xmlTextReaderImpl.ps.eolNormalized)
										{
											chars[num2] = '\n';
										}
										num2++;
									}
									xmlTextReaderImpl.OnNewLine(num2);
									continue;
								default:
									if (c2 != '&')
									{
										goto IL_2A6;
									}
									break;
								}
							}
							else if (c2 != '<')
							{
								if (c2 != '?')
								{
									if (c2 != ']')
									{
										goto IL_2A6;
									}
								}
								else
								{
									if (chars[num2 + 1] == '>')
									{
										break;
									}
									if (num2 + 1 != xmlTextReaderImpl.ps.charsUsed)
									{
										num2++;
										continue;
									}
									goto IL_309;
								}
							}
							num2++;
							continue;
							IL_2A6:
							if (num2 == xmlTextReaderImpl.ps.charsUsed)
							{
								goto IL_309;
							}
							if (XmlCharType.IsHighSurrogate((int)chars[num2]))
							{
								if (num2 + 1 == xmlTextReaderImpl.ps.charsUsed)
								{
									goto IL_309;
								}
								num2++;
								if (XmlCharType.IsLowSurrogate((int)chars[num2]))
								{
									num2++;
									continue;
								}
							}
							xmlTextReaderImpl.ThrowInvalidChar(chars, xmlTextReaderImpl.ps.charsUsed, num2);
						}
						else
						{
							num2++;
						}
					}
					int item;
					if (num3 > 0)
					{
						xmlTextReaderImpl.ShiftBuffer(num4 + num3, num4, num2 - num4 - num3);
						item = num2 - num3;
					}
					else
					{
						item = num2;
					}
					int charPos = xmlTextReaderImpl.ps.charPos;
					xmlTextReaderImpl.ps.charPos = num2 + 2;
					result = new Tuple<int, int, bool>(charPos, item, true);
					goto IL_369;
					IL_309:
					if (num3 > 0)
					{
						xmlTextReaderImpl.ShiftBuffer(num4 + num3, num4, num2 - num4 - num3);
						item = num2 - num3;
					}
					else
					{
						item = num2;
					}
					int charPos2 = xmlTextReaderImpl.ps.charPos;
					xmlTextReaderImpl.ps.charPos = num2;
					result = new Tuple<int, int, bool>(charPos2, item, false);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_369:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009FC RID: 2556 RVA: 0x0003F1D8 File Offset: 0x0003D3D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B46 RID: 2886
			public int <>1__state;

			// Token: 0x04000B47 RID: 2887
			public AsyncTaskMethodBuilder<Tuple<int, int, bool>> <>t__builder;

			// Token: 0x04000B48 RID: 2888
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B49 RID: 2889
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000FE RID: 254
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseCommentAsync>d__563 : IAsyncStateMachine
		{
			// Token: 0x060009FD RID: 2557 RVA: 0x0003F1E8 File Offset: 0x0003D3E8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							if (xmlTextReaderImpl.ignoreComments)
							{
								this.<oldParsingMode>5__2 = xmlTextReaderImpl.parsingMode;
								xmlTextReaderImpl.parsingMode = XmlTextReaderImpl.ParsingMode.SkipNode;
								awaiter = xmlTextReaderImpl.ParseCDataOrCommentAsync(XmlNodeType.Comment).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 0;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseCommentAsync>d__563>(ref awaiter, ref this);
									return;
								}
								goto IL_95;
							}
							else
							{
								awaiter = xmlTextReaderImpl.ParseCDataOrCommentAsync(XmlNodeType.Comment).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									this.<>1__state = 1;
									this.<>u__1 = awaiter;
									this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseCommentAsync>d__563>(ref awaiter, ref this);
									return;
								}
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						awaiter.GetResult();
						result = true;
						goto IL_12F;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					IL_95:
					awaiter.GetResult();
					xmlTextReaderImpl.parsingMode = this.<oldParsingMode>5__2;
					result = false;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_12F:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x060009FE RID: 2558 RVA: 0x0003F354 File Offset: 0x0003D554
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B4A RID: 2890
			public int <>1__state;

			// Token: 0x04000B4B RID: 2891
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000B4C RID: 2892
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B4D RID: 2893
			private XmlTextReaderImpl.ParsingMode <oldParsingMode>5__2;

			// Token: 0x04000B4E RID: 2894
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020000FF RID: 255
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseCDataOrCommentAsync>d__565 : IAsyncStateMachine
		{
			// Token: 0x060009FF RID: 2559 RVA: 0x0003F364 File Offset: 0x0003D564
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_176;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_247;
					default:
						if (xmlTextReaderImpl.parsingMode != XmlTextReaderImpl.ParsingMode.Full)
						{
							goto IL_1E3;
						}
						xmlTextReaderImpl.curNode.SetLineInfo(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
						awaiter = xmlTextReaderImpl.ParseCDataOrCommentTupleAsync(this.type).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseCDataOrCommentAsync>d__565>(ref awaiter, ref this);
							return;
						}
						break;
					}
					Tuple<int, int, bool> result = awaiter.GetResult();
					int item = result.Item1;
					int item2 = result.Item2;
					if (result.Item3)
					{
						xmlTextReaderImpl.curNode.SetValueNode(this.type, xmlTextReaderImpl.ps.chars, item, item2 - item);
						goto IL_26C;
					}
					IL_F4:
					xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
					awaiter = xmlTextReaderImpl.ParseCDataOrCommentTupleAsync(this.type).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseCDataOrCommentAsync>d__565>(ref awaiter, ref this);
						return;
					}
					IL_176:
					Tuple<int, int, bool> result2 = awaiter.GetResult();
					item = result2.Item1;
					item2 = result2.Item2;
					if (result2.Item3)
					{
						xmlTextReaderImpl.stringBuilder.Append(xmlTextReaderImpl.ps.chars, item, item2 - item);
						xmlTextReaderImpl.curNode.SetValueNode(this.type, xmlTextReaderImpl.stringBuilder.ToString());
						xmlTextReaderImpl.stringBuilder.Length = 0;
						goto IL_26C;
					}
					goto IL_F4;
					IL_1E3:
					awaiter = xmlTextReaderImpl.ParseCDataOrCommentTupleAsync(this.type).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseCDataOrCommentAsync>d__565>(ref awaiter, ref this);
						return;
					}
					IL_247:
					Tuple<int, int, bool> result3 = awaiter.GetResult();
					item = result3.Item1;
					item2 = result3.Item2;
					if (!result3.Item3)
					{
						goto IL_1E3;
					}
					IL_26C:;
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

			// Token: 0x06000A00 RID: 2560 RVA: 0x0003F628 File Offset: 0x0003D828
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B4F RID: 2895
			public int <>1__state;

			// Token: 0x04000B50 RID: 2896
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B51 RID: 2897
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B52 RID: 2898
			public XmlNodeType type;

			// Token: 0x04000B53 RID: 2899
			private ConfiguredTaskAwaitable<Tuple<int, int, bool>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000100 RID: 256
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseCDataOrCommentTupleAsync>d__566 : IAsyncStateMachine
		{
			// Token: 0x06000A01 RID: 2561 RVA: 0x0003F638 File Offset: 0x0003D838
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, int, bool> result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos >= 3)
						{
							goto IL_B8;
						}
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseCDataOrCommentTupleAsync>d__566>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter.GetResult() == 0)
					{
						xmlTextReaderImpl.Throw("Unexpected end of file while parsing {0} has occurred.", (this.type == XmlNodeType.Comment) ? "Comment" : "CDATA");
					}
					IL_B8:
					int num2 = xmlTextReaderImpl.ps.charPos;
					char[] chars = xmlTextReaderImpl.ps.chars;
					int num3 = 0;
					int num4 = -1;
					char c = (this.type == XmlNodeType.Comment) ? '-' : ']';
					for (;;)
					{
						char c2;
						if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)(c2 = chars[num2])] & 64) == 0 || c2 == c)
						{
							if (chars[num2] == c)
							{
								if (chars[num2 + 1] == c)
								{
									if (chars[num2 + 2] == '>')
									{
										break;
									}
									if (num2 + 2 == xmlTextReaderImpl.ps.charsUsed)
									{
										goto IL_35A;
									}
									if (this.type == XmlNodeType.Comment)
									{
										xmlTextReaderImpl.Throw(num2, "An XML comment cannot contain '--', and '-' cannot be the last character.");
									}
								}
								else if (num2 + 1 == xmlTextReaderImpl.ps.charsUsed)
								{
									goto IL_35A;
								}
								num2++;
							}
							else
							{
								char c3 = chars[num2];
								if (c3 <= '&')
								{
									switch (c3)
									{
									case '\t':
										break;
									case '\n':
										num2++;
										xmlTextReaderImpl.OnNewLine(num2);
										continue;
									case '\v':
									case '\f':
										goto IL_2FC;
									case '\r':
										if (chars[num2 + 1] == '\n')
										{
											if (!xmlTextReaderImpl.ps.eolNormalized && xmlTextReaderImpl.parsingMode == XmlTextReaderImpl.ParsingMode.Full)
											{
												if (num2 - xmlTextReaderImpl.ps.charPos > 0)
												{
													if (num3 == 0)
													{
														num3 = 1;
														num4 = num2;
													}
													else
													{
														xmlTextReaderImpl.ShiftBuffer(num4 + num3, num4, num2 - num4 - num3);
														num4 = num2 - num3;
														num3++;
													}
												}
												else
												{
													XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
													xmlTextReaderImpl2.ps.charPos = xmlTextReaderImpl2.ps.charPos + 1;
												}
											}
											num2 += 2;
										}
										else
										{
											if (num2 + 1 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
											{
												goto IL_35A;
											}
											if (!xmlTextReaderImpl.ps.eolNormalized)
											{
												chars[num2] = '\n';
											}
											num2++;
										}
										xmlTextReaderImpl.OnNewLine(num2);
										continue;
									default:
										if (c3 != '&')
										{
											goto IL_2FC;
										}
										break;
									}
								}
								else if (c3 != '<' && c3 != ']')
								{
									goto IL_2FC;
								}
								num2++;
								continue;
								IL_2FC:
								if (num2 == xmlTextReaderImpl.ps.charsUsed)
								{
									goto IL_35A;
								}
								if (!XmlCharType.IsHighSurrogate((int)chars[num2]))
								{
									goto IL_345;
								}
								if (num2 + 1 == xmlTextReaderImpl.ps.charsUsed)
								{
									goto IL_35A;
								}
								num2++;
								if (!XmlCharType.IsLowSurrogate((int)chars[num2]))
								{
									goto IL_345;
								}
								num2++;
							}
						}
						else
						{
							num2++;
						}
					}
					int item;
					if (num3 > 0)
					{
						xmlTextReaderImpl.ShiftBuffer(num4 + num3, num4, num2 - num4 - num3);
						item = num2 - num3;
					}
					else
					{
						item = num2;
					}
					int charPos = xmlTextReaderImpl.ps.charPos;
					xmlTextReaderImpl.ps.charPos = num2 + 3;
					result = new Tuple<int, int, bool>(charPos, item, true);
					goto IL_3BA;
					IL_345:
					xmlTextReaderImpl.ThrowInvalidChar(chars, xmlTextReaderImpl.ps.charsUsed, num2);
					IL_35A:
					if (num3 > 0)
					{
						xmlTextReaderImpl.ShiftBuffer(num4 + num3, num4, num2 - num4 - num3);
						item = num2 - num3;
					}
					else
					{
						item = num2;
					}
					int charPos2 = xmlTextReaderImpl.ps.charPos;
					xmlTextReaderImpl.ps.charPos = num2;
					result = new Tuple<int, int, bool>(charPos2, item, false);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3BA:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A02 RID: 2562 RVA: 0x0003FA30 File Offset: 0x0003DC30
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B54 RID: 2900
			public int <>1__state;

			// Token: 0x04000B55 RID: 2901
			public AsyncTaskMethodBuilder<Tuple<int, int, bool>> <>t__builder;

			// Token: 0x04000B56 RID: 2902
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B57 RID: 2903
			public XmlNodeType type;

			// Token: 0x04000B58 RID: 2904
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000101 RID: 257
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseDoctypeDeclAsync>d__567 : IAsyncStateMachine
		{
			// Token: 0x06000A03 RID: 2563 RVA: 0x0003FA40 File Offset: 0x0003DC40
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_20C;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2A2;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_322;
					default:
						if (xmlTextReaderImpl.dtdProcessing == DtdProcessing.Prohibit)
						{
							xmlTextReaderImpl.ThrowWithoutLineInfo(xmlTextReaderImpl.v1Compat ? "DTD is prohibited in this XML document." : "For security reasons DTD is prohibited in this XML document. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method.");
							goto IL_C2;
						}
						goto IL_C2;
					}
					IL_A9:
					if (awaiter.GetResult() == 0)
					{
						xmlTextReaderImpl.Throw("Unexpected end of file while parsing {0} has occurred.", "DOCTYPE");
					}
					IL_C2:
					if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos >= 8)
					{
						if (!XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, 7, "DOCTYPE"))
						{
							xmlTextReaderImpl.ThrowUnexpectedToken((!xmlTextReaderImpl.rootElementParsed && xmlTextReaderImpl.dtdInfo == null) ? "DOCTYPE" : "<!--");
						}
						if (!xmlTextReaderImpl.xmlCharType.IsWhiteSpace(xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos + 7]))
						{
							xmlTextReaderImpl.ThrowExpectingWhitespace(xmlTextReaderImpl.ps.charPos + 7);
						}
						if (xmlTextReaderImpl.dtdInfo != null)
						{
							xmlTextReaderImpl.Throw(xmlTextReaderImpl.ps.charPos - 2, "Cannot have multiple DTDs.");
						}
						if (xmlTextReaderImpl.rootElementParsed)
						{
							xmlTextReaderImpl.Throw(xmlTextReaderImpl.ps.charPos - 2, "DTD must be defined before the document root element.");
						}
						XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
						xmlTextReaderImpl2.ps.charPos = xmlTextReaderImpl2.ps.charPos + 8;
						awaiter = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDoctypeDeclAsync>d__567>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDoctypeDeclAsync>d__567>(ref awaiter, ref this);
							return;
						}
						goto IL_A9;
					}
					IL_20C:
					awaiter.GetResult();
					if (xmlTextReaderImpl.dtdProcessing == DtdProcessing.Parse)
					{
						xmlTextReaderImpl.curNode.SetLineInfo(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
						awaiter2 = xmlTextReaderImpl.ParseDtdAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDoctypeDeclAsync>d__567>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = xmlTextReaderImpl.SkipDtdAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDoctypeDeclAsync>d__567>(ref awaiter2, ref this);
							return;
						}
						goto IL_322;
					}
					IL_2A2:
					awaiter2.GetResult();
					xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.parsingFunction;
					xmlTextReaderImpl.parsingFunction = XmlTextReaderImpl.ParsingFunction.ResetAttributesRootLevel;
					result = true;
					goto IL_346;
					IL_322:
					awaiter2.GetResult();
					result = false;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_346:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A04 RID: 2564 RVA: 0x0003FDC4 File Offset: 0x0003DFC4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B59 RID: 2905
			public int <>1__state;

			// Token: 0x04000B5A RID: 2906
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000B5B RID: 2907
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B5C RID: 2908
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000B5D RID: 2909
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000102 RID: 258
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseDtdAsync>d__568 : IAsyncStateMachine
		{
			// Token: 0x06000A05 RID: 2565 RVA: 0x0003FDD4 File Offset: 0x0003DFD4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = DtdParser.Create().ParseInternalDtdAsync(new XmlTextReaderImpl.DtdParserProxy(xmlTextReaderImpl), true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDtdAsync>d__568>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IDtdInfo result = awaiter.GetResult();
					xmlTextReaderImpl.dtdInfo = result;
					if ((xmlTextReaderImpl.validatingReaderCompatFlag || !xmlTextReaderImpl.v1Compat) && (xmlTextReaderImpl.dtdInfo.HasDefaultAttributes || xmlTextReaderImpl.dtdInfo.HasNonCDataAttributes))
					{
						xmlTextReaderImpl.addDefaultAttributesAndNormalize = true;
					}
					xmlTextReaderImpl.curNode.SetNamedNode(XmlNodeType.DocumentType, xmlTextReaderImpl.dtdInfo.Name.ToString(), string.Empty, null);
					xmlTextReaderImpl.curNode.SetValue(xmlTextReaderImpl.dtdInfo.InternalDtdSubset);
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

			// Token: 0x06000A06 RID: 2566 RVA: 0x0003FF14 File Offset: 0x0003E114
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B5E RID: 2910
			public int <>1__state;

			// Token: 0x04000B5F RID: 2911
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B60 RID: 2912
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B61 RID: 2913
			private ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000103 RID: 259
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SkipDtdAsync>d__569 : IAsyncStateMachine
		{
			// Token: 0x06000A07 RID: 2567 RVA: 0x0003FF24 File Offset: 0x0003E124
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_12C;
					case 2:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1B5;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_286;
					case 4:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_301;
					case 5:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_36A;
					case 6:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3E5;
					case 7:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_44E;
					case 8:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4DC;
					case 9:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_5AE;
					case 10:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_62A;
					case 11:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_694;
					case 12:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_772;
					case 13:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_7DC;
					default:
						awaiter = xmlTextReaderImpl.ParseQNameAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter, ref this);
							return;
						}
						break;
					}
					Tuple<int, int> result = awaiter.GetResult();
					int item = result.Item1;
					int item2 = result.Item2;
					xmlTextReaderImpl.ps.charPos = item2;
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
						return;
					}
					IL_12C:
					awaiter2.GetResult();
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] == 'P')
					{
						goto IL_1C9;
					}
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] == 'S')
					{
						goto IL_4F0;
					}
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != '[' && xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != '>')
					{
						xmlTextReaderImpl.Throw("Expecting external ID, '[' or '>'.");
						goto IL_6DF;
					}
					goto IL_6DF;
					IL_1B5:
					if (awaiter2.GetResult() == 0)
					{
						xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
					}
					IL_1C9:
					if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos >= 6)
					{
						if (!XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, 6, "PUBLIC"))
						{
							xmlTextReaderImpl.ThrowUnexpectedToken("PUBLIC");
						}
						XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
						xmlTextReaderImpl2.ps.charPos = xmlTextReaderImpl2.ps.charPos + 6;
						awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
							return;
						}
						goto IL_1B5;
					}
					IL_286:
					if (awaiter2.GetResult() == 0)
					{
						xmlTextReaderImpl.ThrowExpectingWhitespace(xmlTextReaderImpl.ps.charPos);
					}
					awaiter3 = xmlTextReaderImpl.SkipPublicOrSystemIdLiteralAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter3, ref this);
						return;
					}
					IL_301:
					awaiter3.GetResult();
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
						return;
					}
					IL_36A:
					if (awaiter2.GetResult() == 0)
					{
						xmlTextReaderImpl.ThrowExpectingWhitespace(xmlTextReaderImpl.ps.charPos);
					}
					awaiter3 = xmlTextReaderImpl.SkipPublicOrSystemIdLiteralAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 6;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter3, ref this);
						return;
					}
					IL_3E5:
					awaiter3.GetResult();
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 7;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
						return;
					}
					IL_44E:
					awaiter2.GetResult();
					goto IL_6DF;
					IL_4DC:
					if (awaiter2.GetResult() == 0)
					{
						xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
					}
					IL_4F0:
					if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos >= 6)
					{
						if (!XmlConvert.StrEqual(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, 6, "SYSTEM"))
						{
							xmlTextReaderImpl.ThrowUnexpectedToken("SYSTEM");
						}
						XmlTextReaderImpl xmlTextReaderImpl3 = xmlTextReaderImpl;
						xmlTextReaderImpl3.ps.charPos = xmlTextReaderImpl3.ps.charPos + 6;
						awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 9;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 8;
							this.<>u__2 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
							return;
						}
						goto IL_4DC;
					}
					IL_5AE:
					if (awaiter2.GetResult() == 0)
					{
						xmlTextReaderImpl.ThrowExpectingWhitespace(xmlTextReaderImpl.ps.charPos);
					}
					awaiter3 = xmlTextReaderImpl.SkipPublicOrSystemIdLiteralAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 10;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter3, ref this);
						return;
					}
					IL_62A:
					awaiter3.GetResult();
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 11;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
						return;
					}
					IL_694:
					awaiter2.GetResult();
					IL_6DF:
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] == '[')
					{
						XmlTextReaderImpl xmlTextReaderImpl4 = xmlTextReaderImpl;
						xmlTextReaderImpl4.ps.charPos = xmlTextReaderImpl4.ps.charPos + 1;
						awaiter3 = xmlTextReaderImpl.SkipUntilAsync(']', true).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							this.<>1__state = 12;
							this.<>u__3 = awaiter3;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter3, ref this);
							return;
						}
					}
					else
					{
						if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] == '>')
						{
							xmlTextReaderImpl.curNode.SetValue(string.Empty);
							goto IL_844;
						}
						xmlTextReaderImpl.Throw("Expecting an internal subset or the end of the DOCTYPE declaration.");
						goto IL_844;
					}
					IL_772:
					awaiter3.GetResult();
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 13;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipDtdAsync>d__569>(ref awaiter2, ref this);
						return;
					}
					IL_7DC:
					awaiter2.GetResult();
					if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != '>')
					{
						xmlTextReaderImpl.ThrowUnexpectedToken(">");
					}
					IL_844:
					XmlTextReaderImpl xmlTextReaderImpl5 = xmlTextReaderImpl;
					xmlTextReaderImpl5.ps.charPos = xmlTextReaderImpl5.ps.charPos + 1;
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

			// Token: 0x06000A08 RID: 2568 RVA: 0x000407D0 File Offset: 0x0003E9D0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B62 RID: 2914
			public int <>1__state;

			// Token: 0x04000B63 RID: 2915
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B64 RID: 2916
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B65 RID: 2917
			private ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000B66 RID: 2918
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000B67 RID: 2919
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x02000104 RID: 260
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <SkipUntilAsync>d__571 : IAsyncStateMachine
		{
			// Token: 0x06000A09 RID: 2569 RVA: 0x000407E0 File Offset: 0x0003E9E0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3EA;
					}
					this.<inLiteral>5__2 = false;
					this.<inComment>5__3 = false;
					this.<inPI>5__4 = false;
					this.<literalQuote>5__5 = '"';
					char[] chars = xmlTextReaderImpl.ps.chars;
					int num2 = xmlTextReaderImpl.ps.charPos;
					for (;;)
					{
						IL_4F:
						char c;
						if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)(c = chars[num2])] & 128) == 0 || chars[num2] == this.stopChar || c == '-' || c == '?')
						{
							if (c == this.stopChar && !this.<inLiteral>5__2)
							{
								break;
							}
							xmlTextReaderImpl.ps.charPos = num2;
							if (c <= '&')
							{
								switch (c)
								{
								case '\t':
									break;
								case '\n':
									num2++;
									xmlTextReaderImpl.OnNewLine(num2);
									continue;
								case '\v':
								case '\f':
									goto IL_337;
								case '\r':
									if (chars[num2 + 1] == '\n')
									{
										num2 += 2;
									}
									else
									{
										if (num2 + 1 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
										{
											goto IL_389;
										}
										num2++;
									}
									xmlTextReaderImpl.OnNewLine(num2);
									continue;
								default:
									if (c == '"')
									{
										goto IL_2EC;
									}
									if (c != '&')
									{
										goto IL_337;
									}
									break;
								}
							}
							else if (c <= '-')
							{
								if (c == '\'')
								{
									goto IL_2EC;
								}
								if (c != '-')
								{
									goto IL_337;
								}
								if (this.<inComment>5__3)
								{
									if (num2 + 2 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
									{
										goto IL_389;
									}
									if (chars[num2 + 1] == '-' && chars[num2 + 2] == '>')
									{
										this.<inComment>5__3 = false;
										num2 += 2;
										continue;
									}
								}
								num2++;
								continue;
							}
							else
							{
								switch (c)
								{
								case '<':
									if (chars[num2 + 1] == '?')
									{
										if (this.recognizeLiterals && !this.<inLiteral>5__2 && !this.<inComment>5__3)
										{
											this.<inPI>5__4 = true;
											num2 += 2;
											continue;
										}
									}
									else if (chars[num2 + 1] == '!')
									{
										if (num2 + 3 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
										{
											goto IL_389;
										}
										if (chars[num2 + 2] == '-' && chars[num2 + 3] == '-' && this.recognizeLiterals && !this.<inLiteral>5__2 && !this.<inPI>5__4)
										{
											this.<inComment>5__3 = true;
											num2 += 4;
											continue;
										}
									}
									else if (num2 + 1 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
									{
										goto IL_389;
									}
									num2++;
									continue;
								case '=':
									goto IL_337;
								case '>':
									break;
								case '?':
									if (this.<inPI>5__4)
									{
										if (num2 + 1 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
										{
											goto IL_389;
										}
										if (chars[num2 + 1] == '>')
										{
											this.<inPI>5__4 = false;
											num2++;
											continue;
										}
									}
									num2++;
									continue;
								default:
									if (c != ']')
									{
										goto IL_337;
									}
									break;
								}
							}
							num2++;
							continue;
							IL_2EC:
							if (this.<inLiteral>5__2)
							{
								if (this.<literalQuote>5__5 == c)
								{
									this.<inLiteral>5__2 = false;
								}
							}
							else if (this.recognizeLiterals && !this.<inComment>5__3 && !this.<inPI>5__4)
							{
								this.<inLiteral>5__2 = true;
								this.<literalQuote>5__5 = c;
							}
							num2++;
							continue;
							IL_337:
							if (num2 == xmlTextReaderImpl.ps.charsUsed)
							{
								goto IL_389;
							}
							if (!XmlCharType.IsHighSurrogate((int)chars[num2]))
							{
								goto IL_376;
							}
							if (num2 + 1 == xmlTextReaderImpl.ps.charsUsed)
							{
								goto IL_389;
							}
							num2++;
							if (!XmlCharType.IsLowSurrogate((int)chars[num2]))
							{
								goto IL_376;
							}
							num2++;
						}
						else
						{
							num2++;
						}
					}
					xmlTextReaderImpl.ps.charPos = num2 + 1;
					goto IL_476;
					IL_376:
					xmlTextReaderImpl.ThrowInvalidChar(chars, xmlTextReaderImpl.ps.charsUsed, num2);
					IL_389:
					awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<SkipUntilAsync>d__571>(ref awaiter, ref this);
						return;
					}
					IL_3EA:
					if (awaiter.GetResult() == 0)
					{
						if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos > 0)
						{
							if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != '\r')
							{
								xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
							}
						}
						else
						{
							xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
						}
					}
					chars = xmlTextReaderImpl.ps.chars;
					num2 = xmlTextReaderImpl.ps.charPos;
					goto IL_4F;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_476:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000A0A RID: 2570 RVA: 0x00040C94 File Offset: 0x0003EE94
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B68 RID: 2920
			public int <>1__state;

			// Token: 0x04000B69 RID: 2921
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B6A RID: 2922
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B6B RID: 2923
			public char stopChar;

			// Token: 0x04000B6C RID: 2924
			public bool recognizeLiterals;

			// Token: 0x04000B6D RID: 2925
			private bool <inLiteral>5__2;

			// Token: 0x04000B6E RID: 2926
			private bool <inComment>5__3;

			// Token: 0x04000B6F RID: 2927
			private bool <inPI>5__4;

			// Token: 0x04000B70 RID: 2928
			private char <literalQuote>5__5;

			// Token: 0x04000B71 RID: 2929
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000105 RID: 261
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EatWhitespacesAsync>d__572 : IAsyncStateMachine
		{
			// Token: 0x06000A0B RID: 2571 RVA: 0x00040CA4 File Offset: 0x0003EEA4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_261;
					}
					int num2 = xmlTextReaderImpl.ps.charPos;
					this.<wsCount>5__2 = 0;
					char[] chars = xmlTextReaderImpl.ps.chars;
					for (;;)
					{
						IL_34:
						char c = chars[num2];
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							num2++;
							xmlTextReaderImpl.OnNewLine(num2);
							continue;
						case '\v':
						case '\f':
							goto IL_130;
						case '\r':
							if (chars[num2 + 1] == '\n')
							{
								int num3 = num2 - xmlTextReaderImpl.ps.charPos;
								if (this.sb != null && !xmlTextReaderImpl.ps.eolNormalized)
								{
									if (num3 > 0)
									{
										this.sb.Append(chars, xmlTextReaderImpl.ps.charPos, num3);
										this.<wsCount>5__2 += num3;
									}
									xmlTextReaderImpl.ps.charPos = num2 + 1;
								}
								num2 += 2;
							}
							else
							{
								if (num2 + 1 >= xmlTextReaderImpl.ps.charsUsed && !xmlTextReaderImpl.ps.isEof)
								{
									goto IL_1A5;
								}
								if (!xmlTextReaderImpl.ps.eolNormalized)
								{
									chars[num2] = '\n';
								}
								num2++;
							}
							xmlTextReaderImpl.OnNewLine(num2);
							continue;
						default:
							if (c != ' ')
							{
								goto Block_5;
							}
							break;
						}
						num2++;
					}
					Block_5:
					IL_130:
					if (num2 != xmlTextReaderImpl.ps.charsUsed)
					{
						int num4 = num2 - xmlTextReaderImpl.ps.charPos;
						if (num4 > 0)
						{
							if (this.sb != null)
							{
								this.sb.Append(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, num4);
							}
							xmlTextReaderImpl.ps.charPos = num2;
							this.<wsCount>5__2 += num4;
						}
						result = this.<wsCount>5__2;
						goto IL_2E9;
					}
					IL_1A5:
					int num5 = num2 - xmlTextReaderImpl.ps.charPos;
					if (num5 > 0)
					{
						if (this.sb != null)
						{
							this.sb.Append(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, num5);
						}
						xmlTextReaderImpl.ps.charPos = num2;
						this.<wsCount>5__2 += num5;
					}
					awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<EatWhitespacesAsync>d__572>(ref awaiter, ref this);
						return;
					}
					IL_261:
					if (awaiter.GetResult() == 0)
					{
						if (xmlTextReaderImpl.ps.charsUsed - xmlTextReaderImpl.ps.charPos == 0)
						{
							result = this.<wsCount>5__2;
							goto IL_2E9;
						}
						if (xmlTextReaderImpl.ps.chars[xmlTextReaderImpl.ps.charPos] != '\r')
						{
							xmlTextReaderImpl.Throw("Unexpected end of file has occurred.");
						}
					}
					num2 = xmlTextReaderImpl.ps.charPos;
					chars = xmlTextReaderImpl.ps.chars;
					goto IL_34;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_2E9:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A0C RID: 2572 RVA: 0x00040FCC File Offset: 0x0003F1CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B72 RID: 2930
			public int <>1__state;

			// Token: 0x04000B73 RID: 2931
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000B74 RID: 2932
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B75 RID: 2933
			public StringBuilder sb;

			// Token: 0x04000B76 RID: 2934
			private int <wsCount>5__2;

			// Token: 0x04000B77 RID: 2935
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000106 RID: 262
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseNumericCharRefAsync>d__573 : IAsyncStateMachine
		{
			// Token: 0x06000A0D RID: 2573 RVA: 0x00040FDC File Offset: 0x0003F1DC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<XmlTextReaderImpl.EntityType, int> result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_9A;
					}
					IL_11:
					int num3;
					XmlTextReaderImpl.EntityType item;
					int num2;
					if ((num2 = xmlTextReaderImpl.ParseNumericCharRefInline(xmlTextReaderImpl.ps.charPos, this.expand, this.internalSubsetBuilder, out num3, out item)) != -2)
					{
						if (this.expand)
						{
							xmlTextReaderImpl.ps.charPos = num2 - num3;
						}
						result = new Tuple<XmlTextReaderImpl.EntityType, int>(item, num2);
						goto IL_F2;
					}
					awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseNumericCharRefAsync>d__573>(ref awaiter, ref this);
						return;
					}
					IL_9A:
					if (awaiter.GetResult() == 0)
					{
						xmlTextReaderImpl.Throw("Unexpected end of file while parsing {0} has occurred.");
						goto IL_11;
					}
					goto IL_11;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_F2:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A0E RID: 2574 RVA: 0x00041100 File Offset: 0x0003F300
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B78 RID: 2936
			public int <>1__state;

			// Token: 0x04000B79 RID: 2937
			public AsyncTaskMethodBuilder<Tuple<XmlTextReaderImpl.EntityType, int>> <>t__builder;

			// Token: 0x04000B7A RID: 2938
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B7B RID: 2939
			public bool expand;

			// Token: 0x04000B7C RID: 2940
			public StringBuilder internalSubsetBuilder;

			// Token: 0x04000B7D RID: 2941
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000107 RID: 263
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseNamedCharRefAsync>d__574 : IAsyncStateMachine
		{
			// Token: 0x06000A0F RID: 2575 RVA: 0x00041110 File Offset: 0x0003F310
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_A2;
					}
					IL_11:
					int num3;
					int num2 = num3 = xmlTextReaderImpl.ParseNamedCharRefInline(xmlTextReaderImpl.ps.charPos, this.expand, this.internalSubsetBuilder);
					if (num3 != -2)
					{
						if (num3 == -1)
						{
							result = -1;
							goto IL_E5;
						}
						if (this.expand)
						{
							xmlTextReaderImpl.ps.charPos = num2 - 1;
						}
						result = num2;
						goto IL_E5;
					}
					else
					{
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseNamedCharRefAsync>d__574>(ref awaiter, ref this);
							return;
						}
					}
					IL_A2:
					if (awaiter.GetResult() != 0)
					{
						goto IL_11;
					}
					result = -1;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_E5:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x00041228 File Offset: 0x0003F428
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B7E RID: 2942
			public int <>1__state;

			// Token: 0x04000B7F RID: 2943
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000B80 RID: 2944
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B81 RID: 2945
			public bool expand;

			// Token: 0x04000B82 RID: 2946
			public StringBuilder internalSubsetBuilder;

			// Token: 0x04000B83 RID: 2947
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000108 RID: 264
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseNameAsync>d__575 : IAsyncStateMachine
		{
			// Token: 0x06000A11 RID: 2577 RVA: 0x00041238 File Offset: 0x0003F438
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int item;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = xmlTextReaderImpl.ParseQNameAsync(false, 0).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseNameAsync>d__575>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					item = awaiter.GetResult().Item2;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(item);
			}

			// Token: 0x06000A12 RID: 2578 RVA: 0x00041300 File Offset: 0x0003F500
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B84 RID: 2948
			public int <>1__state;

			// Token: 0x04000B85 RID: 2949
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000B86 RID: 2950
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B87 RID: 2951
			private ConfiguredTaskAwaitable<Tuple<int, int>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000109 RID: 265
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseQNameAsync>d__577 : IAsyncStateMachine
		{
			// Token: 0x06000A13 RID: 2579 RVA: 0x00041310 File Offset: 0x0003F510
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, int> result3;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_11C;
					}
					int num2;
					if (num != 1)
					{
						this.<colonOffset>5__2 = -1;
						num2 = xmlTextReaderImpl.ps.charPos + this.startOffset;
						goto IL_35;
					}
					awaiter = this.<>u__1;
					this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter);
					this.<>1__state = -1;
					goto IL_247;
					for (;;)
					{
						IL_145:
						if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)this.<chars>5__3[num2]] & 8) != 0)
						{
							num2++;
						}
						else
						{
							if (this.<chars>5__3[num2] != ':')
							{
								goto IL_1D4;
							}
							if (xmlTextReaderImpl.supportNamespaces)
							{
								break;
							}
							this.<colonOffset>5__2 = num2 - xmlTextReaderImpl.ps.charPos;
							num2++;
						}
					}
					if (this.<colonOffset>5__2 != -1 || !this.isQName)
					{
						xmlTextReaderImpl.Throw(num2, "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(':', '\0'));
					}
					this.<colonOffset>5__2 = num2 - xmlTextReaderImpl.ps.charPos;
					num2++;
					goto IL_35;
					IL_1D4:
					if (num2 != xmlTextReaderImpl.ps.charsUsed)
					{
						goto IL_283;
					}
					awaiter = xmlTextReaderImpl.ReadDataInNameAsync(num2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseQNameAsync>d__577>(ref awaiter, ref this);
						return;
					}
					goto IL_247;
					IL_35:
					this.<chars>5__3 = xmlTextReaderImpl.ps.chars;
					bool flag = false;
					if ((xmlTextReaderImpl.xmlCharType.charProperties[(int)this.<chars>5__3[num2]] & 4) != 0)
					{
						num2++;
					}
					else if (num2 + 1 >= xmlTextReaderImpl.ps.charsUsed)
					{
						flag = true;
					}
					else if (this.<chars>5__3[num2] != ':' || xmlTextReaderImpl.supportNamespaces)
					{
						xmlTextReaderImpl.Throw(num2, "Name cannot begin with the '{0}' character, hexadecimal value {1}.", XmlException.BuildCharExceptionArgs(this.<chars>5__3, xmlTextReaderImpl.ps.charsUsed, num2));
					}
					if (!flag)
					{
						goto IL_145;
					}
					awaiter = xmlTextReaderImpl.ReadDataInNameAsync(num2).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseQNameAsync>d__577>(ref awaiter, ref this);
						return;
					}
					IL_11C:
					Tuple<int, bool> result = awaiter.GetResult();
					num2 = result.Item1;
					if (!result.Item2)
					{
						xmlTextReaderImpl.Throw(num2, "Unexpected end of file while parsing {0} has occurred.", "Name");
						goto IL_145;
					}
					goto IL_35;
					IL_247:
					Tuple<int, bool> result2 = awaiter.GetResult();
					num2 = result2.Item1;
					if (result2.Item2)
					{
						this.<chars>5__3 = xmlTextReaderImpl.ps.chars;
						goto IL_145;
					}
					xmlTextReaderImpl.Throw(num2, "Unexpected end of file while parsing {0} has occurred.", "Name");
					IL_283:
					result3 = new Tuple<int, int>((this.<colonOffset>5__2 == -1) ? -1 : (xmlTextReaderImpl.ps.charPos + this.<colonOffset>5__2), num2);
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<chars>5__3 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<chars>5__3 = null;
				this.<>t__builder.SetResult(result3);
			}

			// Token: 0x06000A14 RID: 2580 RVA: 0x00041620 File Offset: 0x0003F820
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B88 RID: 2952
			public int <>1__state;

			// Token: 0x04000B89 RID: 2953
			public AsyncTaskMethodBuilder<Tuple<int, int>> <>t__builder;

			// Token: 0x04000B8A RID: 2954
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B8B RID: 2955
			public int startOffset;

			// Token: 0x04000B8C RID: 2956
			public bool isQName;

			// Token: 0x04000B8D RID: 2957
			private int <colonOffset>5__2;

			// Token: 0x04000B8E RID: 2958
			private char[] <chars>5__3;

			// Token: 0x04000B8F RID: 2959
			private ConfiguredTaskAwaitable<Tuple<int, bool>>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200010A RID: 266
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadDataInNameAsync>d__578 : IAsyncStateMachine
		{
			// Token: 0x06000A15 RID: 2581 RVA: 0x00041630 File Offset: 0x0003F830
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				Tuple<int, bool> result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						this.<offset>5__2 = this.pos - xmlTextReaderImpl.ps.charPos;
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadDataInNameAsync>d__578>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					bool item = awaiter.GetResult() != 0;
					this.pos = xmlTextReaderImpl.ps.charPos + this.<offset>5__2;
					result = new Tuple<int, bool>(this.pos, item);
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

			// Token: 0x06000A16 RID: 2582 RVA: 0x00041734 File Offset: 0x0003F934
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B90 RID: 2960
			public int <>1__state;

			// Token: 0x04000B91 RID: 2961
			public AsyncTaskMethodBuilder<Tuple<int, bool>> <>t__builder;

			// Token: 0x04000B92 RID: 2962
			public int pos;

			// Token: 0x04000B93 RID: 2963
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B94 RID: 2964
			private int <offset>5__2;

			// Token: 0x04000B95 RID: 2965
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200010B RID: 267
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseEntityNameAsync>d__579 : IAsyncStateMachine
		{
			// Token: 0x06000A17 RID: 2583 RVA: 0x00041744 File Offset: 0x0003F944
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				string result2;
				try
				{
					int result;
					try
					{
						ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							awaiter = xmlTextReaderImpl.ParseNameAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseEntityNameAsync>d__579>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
						}
						result = awaiter.GetResult();
					}
					catch (XmlException)
					{
						xmlTextReaderImpl.Throw("An error occurred while parsing EntityName.");
						result2 = null;
						goto IL_103;
					}
					if (xmlTextReaderImpl.ps.chars[result] != ';')
					{
						xmlTextReaderImpl.Throw("An error occurred while parsing EntityName.");
					}
					string text = xmlTextReaderImpl.nameTable.Add(xmlTextReaderImpl.ps.chars, xmlTextReaderImpl.ps.charPos, result - xmlTextReaderImpl.ps.charPos);
					xmlTextReaderImpl.ps.charPos = result + 1;
					result2 = text;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_103:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result2);
			}

			// Token: 0x06000A18 RID: 2584 RVA: 0x00041884 File Offset: 0x0003FA84
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B96 RID: 2966
			public int <>1__state;

			// Token: 0x04000B97 RID: 2967
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x04000B98 RID: 2968
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B99 RID: 2969
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200010C RID: 268
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <PushExternalEntityOrSubsetAsync>d__580 : IAsyncStateMachine
		{
			// Token: 0x06000A19 RID: 2585 RVA: 0x00041894 File Offset: 0x0003FA94
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					if (num != 0)
					{
						if (num == 1)
						{
							goto IL_DC;
						}
						if (string.IsNullOrEmpty(this.publicId))
						{
							goto IL_BF;
						}
					}
					try
					{
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							this.<uri>5__2 = xmlTextReaderImpl.xmlResolver.ResolveUri(this.baseUri, this.publicId);
							awaiter = xmlTextReaderImpl.OpenAndPushAsync(this.<uri>5__2).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<PushExternalEntityOrSubsetAsync>d__580>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						if (awaiter.GetResult())
						{
							goto IL_227;
						}
					}
					catch (Exception)
					{
					}
					IL_BF:
					this.<uri>5__2 = xmlTextReaderImpl.xmlResolver.ResolveUri(this.baseUri, this.systemId);
					IL_DC:
					try
					{
						ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
						if (num != 1)
						{
							awaiter = xmlTextReaderImpl.OpenAndPushAsync(this.<uri>5__2).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 1);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<PushExternalEntityOrSubsetAsync>d__580>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						if (awaiter.GetResult())
						{
							goto IL_227;
						}
					}
					catch (Exception ex)
					{
						if (xmlTextReaderImpl.v1Compat)
						{
							throw;
						}
						string message = ex.Message;
						xmlTextReaderImpl.Throw(new XmlException((this.entityName == null) ? "An error has occurred while opening external DTD '{0}': {1}" : "An error has occurred while opening external entity '{0}': {1}", new string[]
						{
							this.<uri>5__2.ToString(),
							message
						}, ex, 0, 0));
					}
					if (this.entityName == null)
					{
						xmlTextReaderImpl.ThrowWithoutLineInfo("Cannot resolve external DTD subset - public ID = '{0}', system ID = '{1}'.", new string[]
						{
							(this.publicId != null) ? this.publicId : string.Empty,
							this.systemId
						}, null);
					}
					else
					{
						xmlTextReaderImpl.Throw((xmlTextReaderImpl.dtdProcessing == DtdProcessing.Ignore) ? "Cannot resolve entity reference '{0}' because the DTD has been ignored. To enable DTD processing set the DtdProcessing property on XmlReaderSettings to Parse and pass the settings into XmlReader.Create method." : "Cannot resolve entity reference '{0}'.", this.entityName);
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<uri>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_227:
				this.<>1__state = -2;
				this.<uri>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x00041B30 File Offset: 0x0003FD30
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000B9A RID: 2970
			public int <>1__state;

			// Token: 0x04000B9B RID: 2971
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000B9C RID: 2972
			public string publicId;

			// Token: 0x04000B9D RID: 2973
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000B9E RID: 2974
			public Uri baseUri;

			// Token: 0x04000B9F RID: 2975
			public string systemId;

			// Token: 0x04000BA0 RID: 2976
			public string entityName;

			// Token: 0x04000BA1 RID: 2977
			private Uri <uri>5__2;

			// Token: 0x04000BA2 RID: 2978
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200010D RID: 269
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <OpenAndPushAsync>d__581 : IAsyncStateMachine
		{
			// Token: 0x06000A1B RID: 2587 RVA: 0x00041B40 File Offset: 0x0003FD40
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_14B;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1CE;
					case 3:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_254;
					default:
						if (xmlTextReaderImpl.xmlResolver.SupportsType(this.uri, typeof(TextReader)))
						{
							awaiter = xmlTextReaderImpl.xmlResolver.GetEntityAsync(this.uri, null, typeof(TextReader)).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<OpenAndPushAsync>d__581>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlTextReaderImpl.xmlResolver.GetEntityAsync(this.uri, null, typeof(Stream)).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 2;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<OpenAndPushAsync>d__581>(ref awaiter, ref this);
								return;
							}
							goto IL_1CE;
						}
						break;
					}
					TextReader textReader = (TextReader)awaiter.GetResult();
					if (textReader == null)
					{
						result = false;
						goto IL_278;
					}
					xmlTextReaderImpl.PushParsingState();
					awaiter2 = xmlTextReaderImpl.InitTextReaderInputAsync(this.uri.ToString(), this.uri, textReader).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<OpenAndPushAsync>d__581>(ref awaiter2, ref this);
						return;
					}
					IL_14B:
					awaiter2.GetResult();
					goto IL_25B;
					IL_1CE:
					Stream stream = (Stream)awaiter.GetResult();
					if (stream == null)
					{
						result = false;
						goto IL_278;
					}
					xmlTextReaderImpl.PushParsingState();
					awaiter2 = xmlTextReaderImpl.InitStreamInputAsync(this.uri, stream, null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<OpenAndPushAsync>d__581>(ref awaiter2, ref this);
						return;
					}
					IL_254:
					awaiter2.GetResult();
					IL_25B:
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_278:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x00041DF8 File Offset: 0x0003FFF8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BA3 RID: 2979
			public int <>1__state;

			// Token: 0x04000BA4 RID: 2980
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000BA5 RID: 2981
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BA6 RID: 2982
			public Uri uri;

			// Token: 0x04000BA7 RID: 2983
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000BA8 RID: 2984
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x0200010E RID: 270
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <PushExternalEntityAsync>d__582 : IAsyncStateMachine
		{
			// Token: 0x06000A1D RID: 2589 RVA: 0x00041E08 File Offset: 0x00040008
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					switch (num)
					{
					case 0:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						break;
					case 1:
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_168;
					case 2:
						awaiter3 = this.<>u__3;
						this.<>u__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1D2;
					default:
					{
						if (xmlTextReaderImpl.IsResolverNull)
						{
							Encoding encoding = xmlTextReaderImpl.ps.encoding;
							xmlTextReaderImpl.PushParsingState();
							xmlTextReaderImpl.InitStringInput(this.entity.SystemId, encoding, string.Empty);
							xmlTextReaderImpl.RegisterEntity(this.entity);
							xmlTextReaderImpl.RegisterConsumedCharacters(0L, true);
							result = false;
							goto IL_24D;
						}
						Uri baseUri = null;
						if (!string.IsNullOrEmpty(this.entity.BaseUriString))
						{
							baseUri = xmlTextReaderImpl.xmlResolver.ResolveUri(null, this.entity.BaseUriString);
						}
						awaiter = xmlTextReaderImpl.PushExternalEntityOrSubsetAsync(this.entity.PublicId, this.entity.SystemId, baseUri, this.entity.Name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlTextReaderImpl.<PushExternalEntityAsync>d__582>(ref awaiter, ref this);
							return;
						}
						break;
					}
					}
					awaiter.GetResult();
					xmlTextReaderImpl.RegisterEntity(this.entity);
					this.<initialPos>5__2 = xmlTextReaderImpl.ps.charPos;
					if (!xmlTextReaderImpl.v1Compat)
					{
						goto IL_170;
					}
					awaiter2 = xmlTextReaderImpl.EatWhitespacesAsync(null).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<PushExternalEntityAsync>d__582>(ref awaiter2, ref this);
						return;
					}
					IL_168:
					awaiter2.GetResult();
					IL_170:
					awaiter3 = xmlTextReaderImpl.ParseXmlDeclarationAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter3.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__3 = awaiter3;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<PushExternalEntityAsync>d__582>(ref awaiter3, ref this);
						return;
					}
					IL_1D2:
					if (!awaiter3.GetResult())
					{
						xmlTextReaderImpl.ps.charPos = this.<initialPos>5__2;
					}
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_24D:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x00042094 File Offset: 0x00040294
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BA9 RID: 2985
			public int <>1__state;

			// Token: 0x04000BAA RID: 2986
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000BAB RID: 2987
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BAC RID: 2988
			public IDtdEntityInfo entity;

			// Token: 0x04000BAD RID: 2989
			private int <initialPos>5__2;

			// Token: 0x04000BAE RID: 2990
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000BAF RID: 2991
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__2;

			// Token: 0x04000BB0 RID: 2992
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__3;
		}

		// Token: 0x0200010F RID: 271
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ZeroEndingStreamAsync>d__583 : IAsyncStateMachine
		{
			// Token: 0x06000A1F RID: 2591 RVA: 0x000420A4 File Offset: 0x000402A4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					bool flag;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						flag = (xmlTextReaderImpl.v1Compat && this.pos == xmlTextReaderImpl.ps.charsUsed - 1 && xmlTextReaderImpl.ps.chars[this.pos] == '\0');
						if (!flag)
						{
							goto IL_B3;
						}
						awaiter = xmlTextReaderImpl.ReadDataAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ZeroEndingStreamAsync>d__583>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					flag = (awaiter.GetResult() == 0);
					IL_B3:
					if (flag && xmlTextReaderImpl.ps.isStreamEof)
					{
						XmlTextReaderImpl xmlTextReaderImpl2 = xmlTextReaderImpl;
						xmlTextReaderImpl2.ps.charsUsed = xmlTextReaderImpl2.ps.charsUsed - 1;
						result = true;
					}
					else
					{
						result = false;
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

			// Token: 0x06000A20 RID: 2592 RVA: 0x000421CC File Offset: 0x000403CC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BB1 RID: 2993
			public int <>1__state;

			// Token: 0x04000BB2 RID: 2994
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000BB3 RID: 2995
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BB4 RID: 2996
			public int pos;

			// Token: 0x04000BB5 RID: 2997
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000110 RID: 272
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ParseDtdFromParserContextAsync>d__584 : IAsyncStateMachine
		{
			// Token: 0x06000A21 RID: 2593 RVA: 0x000421DC File Offset: 0x000403DC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = DtdParser.Create().ParseFreeFloatingDtdAsync(xmlTextReaderImpl.fragmentParserContext.BaseURI, xmlTextReaderImpl.fragmentParserContext.DocTypeName, xmlTextReaderImpl.fragmentParserContext.PublicId, xmlTextReaderImpl.fragmentParserContext.SystemId, xmlTextReaderImpl.fragmentParserContext.InternalSubset, new XmlTextReaderImpl.DtdParserProxy(xmlTextReaderImpl)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ParseDtdFromParserContextAsync>d__584>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					IDtdInfo result = awaiter.GetResult();
					xmlTextReaderImpl.dtdInfo = result;
					if ((xmlTextReaderImpl.validatingReaderCompatFlag || !xmlTextReaderImpl.v1Compat) && (xmlTextReaderImpl.dtdInfo.HasDefaultAttributes || xmlTextReaderImpl.dtdInfo.HasNonCDataAttributes))
					{
						xmlTextReaderImpl.addDefaultAttributesAndNormalize = true;
					}
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

			// Token: 0x06000A22 RID: 2594 RVA: 0x0004231C File Offset: 0x0004051C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BB6 RID: 2998
			public int <>1__state;

			// Token: 0x04000BB7 RID: 2999
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000BB8 RID: 3000
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BB9 RID: 3001
			private ConfiguredTaskAwaitable<IDtdInfo>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000111 RID: 273
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InitReadContentAsBinaryAsync>d__585 : IAsyncStateMachine
		{
			// Token: 0x06000A23 RID: 2595 RVA: 0x0004232C File Offset: 0x0004052C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.InReadValueChunk)
						{
							throw new InvalidOperationException(Res.GetString("ReadValueChunk calls cannot be mixed with ReadContentAsBase64 or ReadContentAsBinHex."));
						}
						if (xmlTextReaderImpl.parsingFunction == XmlTextReaderImpl.ParsingFunction.InIncrementalRead)
						{
							throw new InvalidOperationException(Res.GetString("ReadContentAsBase64 and ReadContentAsBinHex method calls cannot be mixed with calls to ReadChars, ReadBase64, and ReadBinHex."));
						}
						if (XmlReader.IsTextualNode(xmlTextReaderImpl.curNode.type))
						{
							goto IL_C6;
						}
						awaiter = xmlTextReaderImpl.MoveToNextContentNodeAsync(false).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<InitReadContentAsBinaryAsync>d__585>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (!awaiter.GetResult())
					{
						result = false;
						goto IL_10C;
					}
					IL_C6:
					xmlTextReaderImpl.SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction.InReadContentAsBinary);
					xmlTextReaderImpl.incReadLineInfo.Set(xmlTextReaderImpl.curNode.LineNo, xmlTextReaderImpl.curNode.LinePos);
					result = true;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_10C:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A24 RID: 2596 RVA: 0x0004246C File Offset: 0x0004066C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BBA RID: 3002
			public int <>1__state;

			// Token: 0x04000BBB RID: 3003
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000BBC RID: 3004
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BBD RID: 3005
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000112 RID: 274
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <InitReadElementContentAsBinaryAsync>d__586 : IAsyncStateMachine
		{
			// Token: 0x06000A25 RID: 2597 RVA: 0x0004247C File Offset: 0x0004067C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
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
						break;
					case 1:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_10A;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1A9;
					default:
						this.<isEmpty>5__2 = xmlTextReaderImpl.curNode.IsEmptyElement;
						awaiter = xmlTextReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<InitReadElementContentAsBinaryAsync>d__586>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					if (this.<isEmpty>5__2)
					{
						result = false;
						goto IL_1FB;
					}
					awaiter = xmlTextReaderImpl.MoveToNextContentNodeAsync(false).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<InitReadElementContentAsBinaryAsync>d__586>(ref awaiter, ref this);
						return;
					}
					IL_10A:
					if (awaiter.GetResult())
					{
						xmlTextReaderImpl.SetupReadContentAsBinaryState(XmlTextReaderImpl.ParsingFunction.InReadElementContentAsBinary);
						xmlTextReaderImpl.incReadLineInfo.Set(xmlTextReaderImpl.curNode.LineNo, xmlTextReaderImpl.curNode.LinePos);
						result = true;
						goto IL_1FB;
					}
					if (xmlTextReaderImpl.curNode.type != XmlNodeType.EndElement)
					{
						xmlTextReaderImpl.Throw("'{0}' is an invalid XmlNodeType.", xmlTextReaderImpl.curNode.type.ToString());
					}
					awaiter = xmlTextReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<InitReadElementContentAsBinaryAsync>d__586>(ref awaiter, ref this);
						return;
					}
					IL_1A9:
					awaiter.GetResult();
					result = false;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_1FB:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A26 RID: 2598 RVA: 0x000426B4 File Offset: 0x000408B4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BBE RID: 3006
			public int <>1__state;

			// Token: 0x04000BBF RID: 3007
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000BC0 RID: 3008
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BC1 RID: 3009
			private bool <isEmpty>5__2;

			// Token: 0x04000BC2 RID: 3010
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000113 RID: 275
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <MoveToNextContentNodeAsync>d__587 : IAsyncStateMachine
		{
			// Token: 0x06000A27 RID: 2599 RVA: 0x000426C4 File Offset: 0x000408C4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_102;
					}
					IL_14:
					switch (xmlTextReaderImpl.curNode.type)
					{
					case XmlNodeType.Attribute:
						result = !this.moveIfOnContentNode;
						goto IL_12B;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						if (!this.moveIfOnContentNode)
						{
							result = true;
							goto IL_12B;
						}
						goto IL_98;
					case XmlNodeType.EntityReference:
						xmlTextReaderImpl.outerReader.ResolveEntity();
						goto IL_98;
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.Comment:
					case XmlNodeType.EndEntity:
						goto IL_98;
					}
					result = false;
					goto IL_12B;
					IL_98:
					this.moveIfOnContentNode = false;
					awaiter = xmlTextReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<MoveToNextContentNodeAsync>d__587>(ref awaiter, ref this);
						return;
					}
					IL_102:
					if (awaiter.GetResult())
					{
						goto IL_14;
					}
					result = false;
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

			// Token: 0x06000A28 RID: 2600 RVA: 0x0004282C File Offset: 0x00040A2C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BC3 RID: 3011
			public int <>1__state;

			// Token: 0x04000BC4 RID: 3012
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04000BC5 RID: 3013
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BC6 RID: 3014
			public bool moveIfOnContentNode;

			// Token: 0x04000BC7 RID: 3015
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000114 RID: 276
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadContentAsBinaryAsync>d__588 : IAsyncStateMachine
		{
			// Token: 0x06000A29 RID: 2601 RVA: 0x0004283C File Offset: 0x00040A3C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_194;
					}
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					if (num == 1)
					{
						awaiter2 = this.<>u__2;
						this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_345;
					}
					if (xmlTextReaderImpl.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End)
					{
						result = 0;
						goto IL_3BB;
					}
					xmlTextReaderImpl.incReadDecoder.SetNextOutputBuffer(this.buffer, this.index, this.count);
					IL_49:
					this.<charsRead>5__2 = 0;
					try
					{
						this.<charsRead>5__2 = xmlTextReaderImpl.curNode.CopyToBinary(xmlTextReaderImpl.incReadDecoder, xmlTextReaderImpl.readValueOffset);
					}
					catch (XmlException e)
					{
						xmlTextReaderImpl.curNode.AdjustLineInfo(xmlTextReaderImpl.readValueOffset, xmlTextReaderImpl.ps.eolNormalized, ref xmlTextReaderImpl.incReadLineInfo);
						xmlTextReaderImpl.ReThrow(e, xmlTextReaderImpl.incReadLineInfo.lineNo, xmlTextReaderImpl.incReadLineInfo.linePos);
					}
					xmlTextReaderImpl.readValueOffset += this.<charsRead>5__2;
					if (xmlTextReaderImpl.incReadDecoder.IsFull)
					{
						result = xmlTextReaderImpl.incReadDecoder.DecodedCount;
						goto IL_3BB;
					}
					bool flag;
					int num2;
					int num3;
					if (xmlTextReaderImpl.incReadState == XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue)
					{
						xmlTextReaderImpl.curNode.SetValue(string.Empty);
						flag = false;
						num2 = 0;
						num3 = 0;
						goto IL_20C;
					}
					goto IL_2BF;
					IL_194:
					Tuple<int, int, int, bool> result2 = awaiter.GetResult();
					num2 = result2.Item1;
					num3 = result2.Item2;
					int outOrChars = result2.Item3;
					flag = result2.Item4;
					try
					{
						this.<charsRead>5__2 = xmlTextReaderImpl.incReadDecoder.Decode(xmlTextReaderImpl.ps.chars, num2, num3 - num2);
					}
					catch (XmlException e2)
					{
						xmlTextReaderImpl.ReThrow(e2, xmlTextReaderImpl.incReadLineInfo.lineNo, xmlTextReaderImpl.incReadLineInfo.linePos);
					}
					num2 += this.<charsRead>5__2;
					IL_20C:
					if (xmlTextReaderImpl.incReadDecoder.IsFull || flag)
					{
						xmlTextReaderImpl.incReadState = (flag ? XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnCachedValue : XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_OnPartialValue);
						xmlTextReaderImpl.readValueOffset = 0;
						if (xmlTextReaderImpl.incReadDecoder.IsFull)
						{
							xmlTextReaderImpl.curNode.SetValue(xmlTextReaderImpl.ps.chars, num2, num3 - num2);
							XmlTextReaderImpl.AdjustLineInfo(xmlTextReaderImpl.ps.chars, num2 - this.<charsRead>5__2, num2, xmlTextReaderImpl.ps.eolNormalized, ref xmlTextReaderImpl.incReadLineInfo);
							xmlTextReaderImpl.curNode.SetLineInfo(xmlTextReaderImpl.incReadLineInfo.lineNo, xmlTextReaderImpl.incReadLineInfo.linePos);
							result = xmlTextReaderImpl.incReadDecoder.DecodedCount;
							goto IL_3BB;
						}
					}
					else
					{
						outOrChars = 0;
						xmlTextReaderImpl.incReadLineInfo.Set(xmlTextReaderImpl.ps.LineNo, xmlTextReaderImpl.ps.LinePos);
						awaiter = xmlTextReaderImpl.ParseTextAsync(outOrChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadContentAsBinaryAsync>d__588>(ref awaiter, ref this);
							return;
						}
						goto IL_194;
					}
					IL_2BF:
					this.<tmp>5__3 = xmlTextReaderImpl.parsingFunction;
					xmlTextReaderImpl.parsingFunction = xmlTextReaderImpl.nextParsingFunction;
					xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.nextNextParsingFunction;
					awaiter2 = xmlTextReaderImpl.MoveToNextContentNodeAsync(true).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter2;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadContentAsBinaryAsync>d__588>(ref awaiter2, ref this);
						return;
					}
					IL_345:
					if (awaiter2.GetResult())
					{
						xmlTextReaderImpl.SetupReadContentAsBinaryState(this.<tmp>5__3);
						xmlTextReaderImpl.incReadLineInfo.Set(xmlTextReaderImpl.curNode.LineNo, xmlTextReaderImpl.curNode.LinePos);
						goto IL_49;
					}
					xmlTextReaderImpl.SetupReadContentAsBinaryState(this.<tmp>5__3);
					xmlTextReaderImpl.incReadState = XmlTextReaderImpl.IncrementalReadState.ReadContentAsBinary_End;
					result = xmlTextReaderImpl.incReadDecoder.DecodedCount;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_3BB:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x00042C64 File Offset: 0x00040E64
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BC8 RID: 3016
			public int <>1__state;

			// Token: 0x04000BC9 RID: 3017
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000BCA RID: 3018
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BCB RID: 3019
			public byte[] buffer;

			// Token: 0x04000BCC RID: 3020
			public int index;

			// Token: 0x04000BCD RID: 3021
			public int count;

			// Token: 0x04000BCE RID: 3022
			private int <charsRead>5__2;

			// Token: 0x04000BCF RID: 3023
			private XmlTextReaderImpl.ParsingFunction <tmp>5__3;

			// Token: 0x04000BD0 RID: 3024
			private ConfiguredTaskAwaitable<Tuple<int, int, int, bool>>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000BD1 RID: 3025
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000115 RID: 277
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadElementContentAsBinaryAsync>d__589 : IAsyncStateMachine
		{
			// Token: 0x06000A2B RID: 2603 RVA: 0x00042C74 File Offset: 0x00040E74
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlTextReaderImpl xmlTextReaderImpl = this.<>4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_159;
						}
						if (this.count == 0)
						{
							result = 0;
							goto IL_17E;
						}
						awaiter2 = xmlTextReaderImpl.ReadContentAsBinaryAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadElementContentAsBinaryAsync>d__589>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					int result2 = awaiter2.GetResult();
					if (result2 > 0)
					{
						result = result2;
						goto IL_17E;
					}
					if (xmlTextReaderImpl.curNode.type != XmlNodeType.EndElement)
					{
						throw new XmlException("'{0}' is an invalid XmlNodeType.", xmlTextReaderImpl.curNode.type.ToString(), xmlTextReaderImpl);
					}
					xmlTextReaderImpl.parsingFunction = xmlTextReaderImpl.nextParsingFunction;
					xmlTextReaderImpl.nextParsingFunction = xmlTextReaderImpl.nextNextParsingFunction;
					awaiter = xmlTextReaderImpl.outerReader.ReadAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, XmlTextReaderImpl.<ReadElementContentAsBinaryAsync>d__589>(ref awaiter, ref this);
						return;
					}
					IL_159:
					awaiter.GetResult();
					result = 0;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_17E:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x00042E30 File Offset: 0x00041030
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000BD2 RID: 3026
			public int <>1__state;

			// Token: 0x04000BD3 RID: 3027
			public AsyncTaskMethodBuilder<int> <>t__builder;

			// Token: 0x04000BD4 RID: 3028
			public int count;

			// Token: 0x04000BD5 RID: 3029
			public XmlTextReaderImpl <>4__this;

			// Token: 0x04000BD6 RID: 3030
			public byte[] buffer;

			// Token: 0x04000BD7 RID: 3031
			public int index;

			// Token: 0x04000BD8 RID: 3032
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x04000BD9 RID: 3033
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}
	}
}
