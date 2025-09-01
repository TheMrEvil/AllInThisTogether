using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000029 RID: 41
	internal abstract class XmlBaseReader : XmlDictionaryReader
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00005E90 File Offset: 0x00004090
		protected XmlBaseReader()
		{
			this.bufferReader = new XmlBufferReader(this);
			this.nsMgr = new XmlBaseReader.NamespaceManager(this.bufferReader);
			this.quotas = new XmlDictionaryReaderQuotas();
			this.rootElementNode = new XmlBaseReader.XmlElementNode(this.bufferReader);
			this.atomicTextNode = new XmlBaseReader.XmlAtomicTextNode(this.bufferReader);
			this.node = XmlBaseReader.closedNode;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00005EF8 File Offset: 0x000040F8
		private static BinHexEncoding BinHexEncoding
		{
			get
			{
				if (XmlBaseReader.binhexEncoding == null)
				{
					XmlBaseReader.binhexEncoding = new BinHexEncoding();
				}
				return XmlBaseReader.binhexEncoding;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00005F10 File Offset: 0x00004110
		private static Base64Encoding Base64Encoding
		{
			get
			{
				if (XmlBaseReader.base64Encoding == null)
				{
					XmlBaseReader.base64Encoding = new Base64Encoding();
				}
				return XmlBaseReader.base64Encoding;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00005F28 File Offset: 0x00004128
		protected XmlBufferReader BufferReader
		{
			get
			{
				return this.bufferReader;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00005F30 File Offset: 0x00004130
		public override XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				return this.quotas;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00005F38 File Offset: 0x00004138
		protected XmlBaseReader.XmlNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005F40 File Offset: 0x00004140
		protected void MoveToNode(XmlBaseReader.XmlNode node)
		{
			this.node = node;
			this.ns = null;
			this.localName = null;
			this.prefix = null;
			this.value = null;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005F68 File Offset: 0x00004168
		protected void MoveToInitial(XmlDictionaryReaderQuotas quotas)
		{
			if (quotas == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("quotas");
			}
			quotas.InternalCopyTo(this.quotas);
			this.quotas.MakeReadOnly();
			this.nsMgr.Clear();
			this.depth = 0;
			this.attributeCount = 0;
			this.attributeStart = -1;
			this.attributeIndex = -1;
			this.rootElement = false;
			this.readingElement = false;
			this.signing = false;
			this.MoveToNode(XmlBaseReader.initialNode);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00005FE4 File Offset: 0x000041E4
		protected XmlBaseReader.XmlDeclarationNode MoveToDeclaration()
		{
			if (this.attributeCount < 1)
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Version not found in XML declaration.")));
			}
			if (this.attributeCount > 3)
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Malformed XML declaration.")));
			}
			if (!this.CheckDeclAttribute(0, "version", "1.0", false, "XML version must be '1.0'."))
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Version not found in XML declaration.")));
			}
			if (this.attributeCount > 1)
			{
				if (this.CheckDeclAttribute(1, "encoding", null, true, "XML encoding must be 'UTF-8'."))
				{
					if (this.attributeCount == 3 && !this.CheckStandalone(2))
					{
						XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Malformed XML declaration.")));
					}
				}
				else if (!this.CheckStandalone(1) || this.attributeCount > 2)
				{
					XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Malformed XML declaration.")));
				}
			}
			if (this.declarationNode == null)
			{
				this.declarationNode = new XmlBaseReader.XmlDeclarationNode(this.bufferReader);
			}
			this.MoveToNode(this.declarationNode);
			return this.declarationNode;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000060F4 File Offset: 0x000042F4
		private bool CheckStandalone(int attr)
		{
			XmlBaseReader.XmlAttributeNode xmlAttributeNode = this.attributeNodes[attr];
			if (!xmlAttributeNode.Prefix.IsEmpty)
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Malformed XML declaration.")));
			}
			if (xmlAttributeNode.LocalName != "standalone")
			{
				return false;
			}
			if (!xmlAttributeNode.Value.Equals2("yes", false) && !xmlAttributeNode.Value.Equals2("no", false))
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("'standalone' value in declaration must be 'yes' or 'no'.")));
			}
			return true;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000617C File Offset: 0x0000437C
		private bool CheckDeclAttribute(int index, string localName, string value, bool checkLower, string valueSR)
		{
			XmlBaseReader.XmlAttributeNode xmlAttributeNode = this.attributeNodes[index];
			if (!xmlAttributeNode.Prefix.IsEmpty)
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("Malformed XML declaration.")));
			}
			if (xmlAttributeNode.LocalName != localName)
			{
				return false;
			}
			if (value != null && !xmlAttributeNode.Value.Equals2(value, checkLower))
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString(valueSR)));
			}
			return true;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000061EA File Offset: 0x000043EA
		protected XmlBaseReader.XmlCommentNode MoveToComment()
		{
			if (this.commentNode == null)
			{
				this.commentNode = new XmlBaseReader.XmlCommentNode(this.bufferReader);
			}
			this.MoveToNode(this.commentNode);
			return this.commentNode;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006217 File Offset: 0x00004417
		protected XmlBaseReader.XmlCDataNode MoveToCData()
		{
			if (this.cdataNode == null)
			{
				this.cdataNode = new XmlBaseReader.XmlCDataNode(this.bufferReader);
			}
			this.MoveToNode(this.cdataNode);
			return this.cdataNode;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006244 File Offset: 0x00004444
		protected XmlBaseReader.XmlAtomicTextNode MoveToAtomicText()
		{
			XmlBaseReader.XmlAtomicTextNode result = this.atomicTextNode;
			this.MoveToNode(result);
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006260 File Offset: 0x00004460
		protected XmlBaseReader.XmlComplexTextNode MoveToComplexText()
		{
			if (this.complexTextNode == null)
			{
				this.complexTextNode = new XmlBaseReader.XmlComplexTextNode(this.bufferReader);
			}
			this.MoveToNode(this.complexTextNode);
			return this.complexTextNode;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006290 File Offset: 0x00004490
		protected XmlBaseReader.XmlTextNode MoveToWhitespaceText()
		{
			if (this.whitespaceTextNode == null)
			{
				this.whitespaceTextNode = new XmlBaseReader.XmlWhitespaceTextNode(this.bufferReader);
			}
			if (this.nsMgr.XmlSpace == XmlSpace.Preserve)
			{
				this.whitespaceTextNode.NodeType = XmlNodeType.SignificantWhitespace;
			}
			else
			{
				this.whitespaceTextNode.NodeType = XmlNodeType.Whitespace;
			}
			this.MoveToNode(this.whitespaceTextNode);
			return this.whitespaceTextNode;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000062F2 File Offset: 0x000044F2
		protected XmlBaseReader.XmlElementNode ElementNode
		{
			get
			{
				if (this.depth == 0)
				{
					return this.rootElementNode;
				}
				return this.elementNodes[this.depth];
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006310 File Offset: 0x00004510
		protected void MoveToEndElement()
		{
			if (this.depth == 0)
			{
				XmlExceptionHelper.ThrowInvalidBinaryFormat(this);
			}
			XmlBaseReader.XmlElementNode xmlElementNode = this.elementNodes[this.depth];
			XmlBaseReader.XmlEndElementNode endElement = xmlElementNode.EndElement;
			endElement.Namespace = xmlElementNode.Namespace;
			this.MoveToNode(endElement);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006353 File Offset: 0x00004553
		protected void MoveToEndOfFile()
		{
			if (this.depth != 0)
			{
				XmlExceptionHelper.ThrowUnexpectedEndOfFile(this);
			}
			this.MoveToNode(XmlBaseReader.endOfFileNode);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006370 File Offset: 0x00004570
		protected XmlBaseReader.XmlElementNode EnterScope()
		{
			if (this.depth == 0)
			{
				if (this.rootElement)
				{
					XmlExceptionHelper.ThrowMultipleRootElements(this);
				}
				this.rootElement = true;
			}
			this.nsMgr.EnterScope();
			this.depth++;
			if (this.depth > this.quotas.MaxDepth)
			{
				XmlExceptionHelper.ThrowMaxDepthExceeded(this, this.quotas.MaxDepth);
			}
			if (this.elementNodes == null)
			{
				this.elementNodes = new XmlBaseReader.XmlElementNode[4];
			}
			else if (this.elementNodes.Length == this.depth)
			{
				XmlBaseReader.XmlElementNode[] destinationArray = new XmlBaseReader.XmlElementNode[this.depth * 2];
				Array.Copy(this.elementNodes, destinationArray, this.depth);
				this.elementNodes = destinationArray;
			}
			XmlBaseReader.XmlElementNode xmlElementNode = this.elementNodes[this.depth];
			if (xmlElementNode == null)
			{
				xmlElementNode = new XmlBaseReader.XmlElementNode(this.bufferReader);
				this.elementNodes[this.depth] = xmlElementNode;
			}
			this.attributeCount = 0;
			this.attributeStart = -1;
			this.attributeIndex = -1;
			this.MoveToNode(xmlElementNode);
			return xmlElementNode;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000646C File Offset: 0x0000466C
		protected void ExitScope()
		{
			if (this.depth == 0)
			{
				XmlExceptionHelper.ThrowUnexpectedEndElement(this);
			}
			this.depth--;
			this.nsMgr.ExitScope();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006498 File Offset: 0x00004698
		private XmlBaseReader.XmlAttributeNode AddAttribute(XmlBaseReader.QNameType qnameType, bool isAtomicValue)
		{
			int num = this.attributeCount;
			if (this.attributeNodes == null)
			{
				this.attributeNodes = new XmlBaseReader.XmlAttributeNode[4];
			}
			else if (this.attributeNodes.Length == num)
			{
				XmlBaseReader.XmlAttributeNode[] destinationArray = new XmlBaseReader.XmlAttributeNode[num * 2];
				Array.Copy(this.attributeNodes, destinationArray, num);
				this.attributeNodes = destinationArray;
			}
			XmlBaseReader.XmlAttributeNode xmlAttributeNode = this.attributeNodes[num];
			if (xmlAttributeNode == null)
			{
				xmlAttributeNode = new XmlBaseReader.XmlAttributeNode(this.bufferReader);
				this.attributeNodes[num] = xmlAttributeNode;
			}
			xmlAttributeNode.QNameType = qnameType;
			xmlAttributeNode.IsAtomicValue = isAtomicValue;
			xmlAttributeNode.AttributeText.QNameType = qnameType;
			xmlAttributeNode.AttributeText.IsAtomicValue = isAtomicValue;
			this.attributeCount++;
			return xmlAttributeNode;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006540 File Offset: 0x00004740
		protected XmlBaseReader.Namespace AddNamespace()
		{
			return this.nsMgr.AddNamespace();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000654D File Offset: 0x0000474D
		protected XmlBaseReader.XmlAttributeNode AddAttribute()
		{
			return this.AddAttribute(XmlBaseReader.QNameType.Normal, true);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000654D File Offset: 0x0000474D
		protected XmlBaseReader.XmlAttributeNode AddXmlAttribute()
		{
			return this.AddAttribute(XmlBaseReader.QNameType.Normal, true);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00006558 File Offset: 0x00004758
		protected XmlBaseReader.XmlAttributeNode AddXmlnsAttribute(XmlBaseReader.Namespace ns)
		{
			if (!ns.Prefix.IsEmpty && ns.Uri.IsEmpty)
			{
				XmlExceptionHelper.ThrowEmptyNamespace(this);
			}
			if (ns.Prefix.IsXml && ns.Uri != "http://www.w3.org/XML/1998/namespace")
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("The prefix '{0}' can only be bound to the namespace '{1}'.", new object[]
				{
					"xml",
					"http://www.w3.org/XML/1998/namespace"
				})));
			}
			else if (ns.Prefix.IsXmlns && ns.Uri != "http://www.w3.org/2000/xmlns/")
			{
				XmlExceptionHelper.ThrowXmlException(this, new XmlException(System.Runtime.Serialization.SR.GetString("The prefix '{0}' can only be bound to the namespace '{1}'.", new object[]
				{
					"xmlns",
					"http://www.w3.org/2000/xmlns/"
				})));
			}
			this.nsMgr.Register(ns);
			XmlBaseReader.XmlAttributeNode xmlAttributeNode = this.AddAttribute(XmlBaseReader.QNameType.Xmlns, false);
			xmlAttributeNode.Namespace = ns;
			xmlAttributeNode.AttributeText.Namespace = ns;
			return xmlAttributeNode;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006644 File Offset: 0x00004844
		protected void FixXmlAttribute(XmlBaseReader.XmlAttributeNode attributeNode)
		{
			if (attributeNode.Prefix == "xml")
			{
				if (attributeNode.LocalName == "lang")
				{
					this.nsMgr.AddLangAttribute(attributeNode.Value.GetString());
					return;
				}
				if (attributeNode.LocalName == "space")
				{
					string @string = attributeNode.Value.GetString();
					if (@string == "preserve")
					{
						this.nsMgr.AddSpaceAttribute(XmlSpace.Preserve);
						return;
					}
					if (@string == "default")
					{
						this.nsMgr.AddSpaceAttribute(XmlSpace.Default);
					}
				}
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000066DD File Offset: 0x000048DD
		protected bool OutsideRootElement
		{
			get
			{
				return this.depth == 0;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000066E8 File Offset: 0x000048E8
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000136 RID: 310 RVA: 0x000066E8 File Offset: 0x000048E8
		public override bool CanReadValueChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000137 RID: 311 RVA: 0x000066EB File Offset: 0x000048EB
		public override string BaseURI
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000066F2 File Offset: 0x000048F2
		public override bool HasValue
		{
			get
			{
				return this.node.HasValue;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003127 File Offset: 0x00001327
		public override bool IsDefault
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001F RID: 31
		public override string this[int index]
		{
			get
			{
				return this.GetAttribute(index);
			}
		}

		// Token: 0x17000020 RID: 32
		public override string this[string name]
		{
			get
			{
				return this.GetAttribute(name);
			}
		}

		// Token: 0x17000021 RID: 33
		public override string this[string localName, string namespaceUri]
		{
			get
			{
				return this.GetAttribute(localName, namespaceUri);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000671B File Offset: 0x0000491B
		public override int AttributeCount
		{
			get
			{
				if (this.node.CanGetAttribute)
				{
					return this.attributeCount;
				}
				return 0;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006734 File Offset: 0x00004934
		public override void Close()
		{
			this.MoveToNode(XmlBaseReader.closedNode);
			this.nameTable = null;
			if (this.attributeNodes != null && this.attributeNodes.Length > 16)
			{
				this.attributeNodes = null;
			}
			if (this.elementNodes != null && this.elementNodes.Length > 16)
			{
				this.elementNodes = null;
			}
			this.nsMgr.Close();
			this.bufferReader.Close();
			if (this.signingWriter != null)
			{
				this.signingWriter.Close();
			}
			if (this.attributeSorter != null)
			{
				this.attributeSorter.Close();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000067C5 File Offset: 0x000049C5
		public sealed override int Depth
		{
			get
			{
				return this.depth + this.node.DepthDelta;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000067D9 File Offset: 0x000049D9
		public override bool EOF
		{
			get
			{
				return this.node.ReadState == ReadState.EndOfFile;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000067EC File Offset: 0x000049EC
		private XmlBaseReader.XmlAttributeNode GetAttributeNode(int index)
		{
			if (!this.node.CanGetAttribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", System.Runtime.Serialization.SR.GetString("Only Element nodes have attributes.")));
			}
			if (index < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (index >= this.attributeCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					this.attributeCount
				})));
			}
			return this.attributeNodes[index];
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00006880 File Offset: 0x00004A80
		private XmlBaseReader.XmlAttributeNode GetAttributeNode(string name)
		{
			if (name == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("name"));
			}
			if (!this.node.CanGetAttribute)
			{
				return null;
			}
			int num = name.IndexOf(':');
			string text;
			string text2;
			if (num == -1)
			{
				if (name == "xmlns")
				{
					text = "xmlns";
					text2 = string.Empty;
				}
				else
				{
					text = string.Empty;
					text2 = name;
				}
			}
			else
			{
				text = name.Substring(0, num);
				text2 = name.Substring(num + 1);
			}
			XmlBaseReader.XmlAttributeNode[] array = this.attributeNodes;
			int num2 = this.attributeCount;
			int num3 = this.attributeStart;
			for (int i = 0; i < num2; i++)
			{
				if (++num3 >= num2)
				{
					num3 = 0;
				}
				XmlBaseReader.XmlAttributeNode xmlAttributeNode = array[num3];
				if (xmlAttributeNode.IsPrefixAndLocalName(text, text2))
				{
					this.attributeStart = num3;
					return xmlAttributeNode;
				}
			}
			return null;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006948 File Offset: 0x00004B48
		private XmlBaseReader.XmlAttributeNode GetAttributeNode(string localName, string namespaceUri)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			if (namespaceUri == null)
			{
				namespaceUri = string.Empty;
			}
			if (!this.node.CanGetAttribute)
			{
				return null;
			}
			XmlBaseReader.XmlAttributeNode[] array = this.attributeNodes;
			int num = this.attributeCount;
			int num2 = this.attributeStart;
			for (int i = 0; i < num; i++)
			{
				if (++num2 >= num)
				{
					num2 = 0;
				}
				XmlBaseReader.XmlAttributeNode xmlAttributeNode = array[num2];
				if (xmlAttributeNode.IsLocalNameAndNamespaceUri(localName, namespaceUri))
				{
					this.attributeStart = num2;
					return xmlAttributeNode;
				}
			}
			return null;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000069C8 File Offset: 0x00004BC8
		private XmlBaseReader.XmlAttributeNode GetAttributeNode(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			if (namespaceUri == null)
			{
				namespaceUri = XmlDictionaryString.Empty;
			}
			if (!this.node.CanGetAttribute)
			{
				return null;
			}
			XmlBaseReader.XmlAttributeNode[] array = this.attributeNodes;
			int num = this.attributeCount;
			int num2 = this.attributeStart;
			for (int i = 0; i < num; i++)
			{
				if (++num2 >= num)
				{
					num2 = 0;
				}
				XmlBaseReader.XmlAttributeNode xmlAttributeNode = array[num2];
				if (xmlAttributeNode.IsLocalNameAndNamespaceUri(localName, namespaceUri))
				{
					this.attributeStart = num2;
					return xmlAttributeNode;
				}
			}
			return null;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006A47 File Offset: 0x00004C47
		public override string GetAttribute(int index)
		{
			return this.GetAttributeNode(index).ValueAsString;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006A58 File Offset: 0x00004C58
		public override string GetAttribute(string name)
		{
			XmlBaseReader.XmlAttributeNode attributeNode = this.GetAttributeNode(name);
			if (attributeNode == null)
			{
				return null;
			}
			return attributeNode.ValueAsString;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00006A78 File Offset: 0x00004C78
		public override string GetAttribute(string localName, string namespaceUri)
		{
			XmlBaseReader.XmlAttributeNode attributeNode = this.GetAttributeNode(localName, namespaceUri);
			if (attributeNode == null)
			{
				return null;
			}
			return attributeNode.ValueAsString;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00006A9C File Offset: 0x00004C9C
		public override string GetAttribute(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			XmlBaseReader.XmlAttributeNode attributeNode = this.GetAttributeNode(localName, namespaceUri);
			if (attributeNode == null)
			{
				return null;
			}
			return attributeNode.ValueAsString;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006ABD File Offset: 0x00004CBD
		public sealed override bool IsEmptyElement
		{
			get
			{
				return this.node.IsEmptyElement;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006ACA File Offset: 0x00004CCA
		public override string LocalName
		{
			get
			{
				if (this.localName == null)
				{
					this.localName = this.GetLocalName(true);
				}
				return this.localName;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006AE8 File Offset: 0x00004CE8
		public override string LookupNamespace(string prefix)
		{
			XmlBaseReader.Namespace @namespace = this.nsMgr.LookupNamespace(prefix);
			if (@namespace != null)
			{
				return @namespace.Uri.GetString(this.NameTable);
			}
			if (prefix == "xmlns")
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			return null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006B2B File Offset: 0x00004D2B
		protected XmlBaseReader.Namespace LookupNamespace(PrefixHandleType prefix)
		{
			XmlBaseReader.Namespace @namespace = this.nsMgr.LookupNamespace(prefix);
			if (@namespace == null)
			{
				XmlExceptionHelper.ThrowUndefinedPrefix(this, PrefixHandle.GetString(prefix));
			}
			return @namespace;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006B48 File Offset: 0x00004D48
		protected XmlBaseReader.Namespace LookupNamespace(PrefixHandle prefix)
		{
			XmlBaseReader.Namespace @namespace = this.nsMgr.LookupNamespace(prefix);
			if (@namespace == null)
			{
				XmlExceptionHelper.ThrowUndefinedPrefix(this, prefix.GetString());
			}
			return @namespace;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006B65 File Offset: 0x00004D65
		protected void ProcessAttributes()
		{
			if (this.attributeCount > 0)
			{
				this.ProcessAttributes(this.attributeNodes, this.attributeCount);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006B84 File Offset: 0x00004D84
		private void ProcessAttributes(XmlBaseReader.XmlAttributeNode[] attributeNodes, int attributeCount)
		{
			for (int i = 0; i < attributeCount; i++)
			{
				XmlBaseReader.XmlAttributeNode xmlAttributeNode = attributeNodes[i];
				if (xmlAttributeNode.QNameType == XmlBaseReader.QNameType.Normal)
				{
					PrefixHandle prefixHandle = xmlAttributeNode.Prefix;
					if (!prefixHandle.IsEmpty)
					{
						xmlAttributeNode.Namespace = this.LookupNamespace(prefixHandle);
					}
					else
					{
						xmlAttributeNode.Namespace = XmlBaseReader.NamespaceManager.EmptyNamespace;
					}
					xmlAttributeNode.AttributeText.Namespace = xmlAttributeNode.Namespace;
				}
			}
			if (attributeCount > 1)
			{
				if (attributeCount < 12)
				{
					for (int j = 0; j < attributeCount - 1; j++)
					{
						XmlBaseReader.XmlAttributeNode xmlAttributeNode2 = attributeNodes[j];
						if (xmlAttributeNode2.QNameType == XmlBaseReader.QNameType.Normal)
						{
							for (int k = j + 1; k < attributeCount; k++)
							{
								XmlBaseReader.XmlAttributeNode xmlAttributeNode3 = attributeNodes[k];
								if (xmlAttributeNode3.QNameType == XmlBaseReader.QNameType.Normal && xmlAttributeNode2.LocalName == xmlAttributeNode3.LocalName && xmlAttributeNode2.Namespace.Uri == xmlAttributeNode3.Namespace.Uri)
								{
									XmlExceptionHelper.ThrowDuplicateAttribute(this, xmlAttributeNode2.Prefix.GetString(), xmlAttributeNode3.Prefix.GetString(), xmlAttributeNode2.LocalName.GetString(), xmlAttributeNode2.Namespace.Uri.GetString());
								}
							}
						}
						else
						{
							for (int l = j + 1; l < attributeCount; l++)
							{
								XmlBaseReader.XmlAttributeNode xmlAttributeNode4 = attributeNodes[l];
								if (xmlAttributeNode4.QNameType == XmlBaseReader.QNameType.Xmlns && xmlAttributeNode2.Namespace.Prefix == xmlAttributeNode4.Namespace.Prefix)
								{
									XmlExceptionHelper.ThrowDuplicateAttribute(this, "xmlns", "xmlns", xmlAttributeNode2.Namespace.Prefix.GetString(), "http://www.w3.org/2000/xmlns/");
								}
							}
						}
					}
					return;
				}
				this.CheckAttributes(attributeNodes, attributeCount);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00006D20 File Offset: 0x00004F20
		private void CheckAttributes(XmlBaseReader.XmlAttributeNode[] attributeNodes, int attributeCount)
		{
			if (this.attributeSorter == null)
			{
				this.attributeSorter = new XmlBaseReader.AttributeSorter();
			}
			if (!this.attributeSorter.Sort(attributeNodes, attributeCount))
			{
				int num;
				int num2;
				this.attributeSorter.GetIndeces(out num, out num2);
				if (attributeNodes[num].QNameType == XmlBaseReader.QNameType.Xmlns)
				{
					XmlExceptionHelper.ThrowDuplicateXmlnsAttribute(this, attributeNodes[num].Namespace.Prefix.GetString(), "http://www.w3.org/2000/xmlns/");
					return;
				}
				XmlExceptionHelper.ThrowDuplicateAttribute(this, attributeNodes[num].Prefix.GetString(), attributeNodes[num2].Prefix.GetString(), attributeNodes[num].LocalName.GetString(), attributeNodes[num].Namespace.Uri.GetString());
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00006DC6 File Offset: 0x00004FC6
		public override void MoveToAttribute(int index)
		{
			this.MoveToNode(this.GetAttributeNode(index));
			this.attributeIndex = index;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006DDC File Offset: 0x00004FDC
		public override bool MoveToAttribute(string name)
		{
			XmlBaseReader.XmlNode attributeNode = this.GetAttributeNode(name);
			if (attributeNode == null)
			{
				return false;
			}
			this.MoveToNode(attributeNode);
			this.attributeIndex = this.attributeStart;
			return true;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006E0C File Offset: 0x0000500C
		public override bool MoveToAttribute(string localName, string namespaceUri)
		{
			XmlBaseReader.XmlNode attributeNode = this.GetAttributeNode(localName, namespaceUri);
			if (attributeNode == null)
			{
				return false;
			}
			this.MoveToNode(attributeNode);
			this.attributeIndex = this.attributeStart;
			return true;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00006E3B File Offset: 0x0000503B
		public override bool MoveToElement()
		{
			if (!this.node.CanMoveToElement)
			{
				return false;
			}
			if (this.depth == 0)
			{
				this.MoveToDeclaration();
			}
			else
			{
				this.MoveToNode(this.elementNodes[this.depth]);
			}
			this.attributeIndex = -1;
			return true;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00006E78 File Offset: 0x00005078
		public override XmlNodeType MoveToContent()
		{
			do
			{
				if (this.node.HasContent)
				{
					if ((this.node.NodeType != XmlNodeType.Text && this.node.NodeType != XmlNodeType.CDATA) || this.trailByteCount > 0)
					{
						break;
					}
					if (this.value == null)
					{
						if (!this.node.Value.IsWhitespace())
						{
							break;
						}
					}
					else if (!XmlConverter.IsWhitespace(this.value))
					{
						break;
					}
				}
				else if (this.node.NodeType == XmlNodeType.Attribute)
				{
					goto Block_6;
				}
			}
			while (this.Read());
			goto IL_7C;
			Block_6:
			this.MoveToElement();
			IL_7C:
			return this.node.NodeType;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006F0C File Offset: 0x0000510C
		public override bool MoveToFirstAttribute()
		{
			if (!this.node.CanGetAttribute || this.attributeCount == 0)
			{
				return false;
			}
			this.MoveToNode(this.GetAttributeNode(0));
			this.attributeIndex = 0;
			return true;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006F3C File Offset: 0x0000513C
		public override bool MoveToNextAttribute()
		{
			if (!this.node.CanGetAttribute)
			{
				return false;
			}
			int num = this.attributeIndex + 1;
			if (num >= this.attributeCount)
			{
				return false;
			}
			this.MoveToNode(this.GetAttributeNode(num));
			this.attributeIndex = num;
			return true;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006F81 File Offset: 0x00005181
		public override string NamespaceURI
		{
			get
			{
				if (this.ns == null)
				{
					this.ns = this.GetNamespaceUri(true);
				}
				return this.ns;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006FA0 File Offset: 0x000051A0
		public override XmlNameTable NameTable
		{
			get
			{
				if (this.nameTable == null)
				{
					this.nameTable = new XmlBaseReader.QuotaNameTable(this, this.quotas.MaxNameTableCharCount);
					this.nameTable.Add("xml");
					this.nameTable.Add("xmlns");
					this.nameTable.Add("http://www.w3.org/2000/xmlns/");
					this.nameTable.Add("http://www.w3.org/XML/1998/namespace");
					for (PrefixHandleType prefixHandleType = PrefixHandleType.A; prefixHandleType <= PrefixHandleType.Z; prefixHandleType++)
					{
						this.nameTable.Add(PrefixHandle.GetString(prefixHandleType));
					}
				}
				return this.nameTable;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00007035 File Offset: 0x00005235
		public sealed override XmlNodeType NodeType
		{
			get
			{
				return this.node.NodeType;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00007044 File Offset: 0x00005244
		public override string Prefix
		{
			get
			{
				if (this.prefix == null)
				{
					XmlBaseReader.QNameType qnameType = this.node.QNameType;
					if (qnameType == XmlBaseReader.QNameType.Normal)
					{
						this.prefix = this.node.Prefix.GetString(this.NameTable);
					}
					else if (qnameType == XmlBaseReader.QNameType.Xmlns)
					{
						if (this.node.Namespace.Prefix.IsEmpty)
						{
							this.prefix = string.Empty;
						}
						else
						{
							this.prefix = "xmlns";
						}
					}
					else
					{
						this.prefix = "xml";
					}
				}
				return this.prefix;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000070CC File Offset: 0x000052CC
		public override char QuoteChar
		{
			get
			{
				return this.node.QuoteChar;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000070DC File Offset: 0x000052DC
		private string GetLocalName(bool enforceAtomization)
		{
			if (this.localName != null)
			{
				return this.localName;
			}
			if (this.node.QNameType == XmlBaseReader.QNameType.Normal)
			{
				if (enforceAtomization || this.nameTable != null)
				{
					return this.node.LocalName.GetString(this.NameTable);
				}
				return this.node.LocalName.GetString();
			}
			else
			{
				if (this.node.Namespace.Prefix.IsEmpty)
				{
					return "xmlns";
				}
				if (enforceAtomization || this.nameTable != null)
				{
					return this.node.Namespace.Prefix.GetString(this.NameTable);
				}
				return this.node.Namespace.Prefix.GetString();
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007194 File Offset: 0x00005394
		private string GetNamespaceUri(bool enforceAtomization)
		{
			if (this.ns != null)
			{
				return this.ns;
			}
			if (this.node.QNameType != XmlBaseReader.QNameType.Normal)
			{
				return "http://www.w3.org/2000/xmlns/";
			}
			if (enforceAtomization || this.nameTable != null)
			{
				return this.node.Namespace.Uri.GetString(this.NameTable);
			}
			return this.node.Namespace.Uri.GetString();
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000071FF File Offset: 0x000053FF
		public override void GetNonAtomizedNames(out string localName, out string namespaceUri)
		{
			localName = this.GetLocalName(false);
			namespaceUri = this.GetNamespaceUri(false);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007213 File Offset: 0x00005413
		public override bool IsLocalName(string localName)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			return this.node.IsLocalName(localName);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007234 File Offset: 0x00005434
		public override bool IsLocalName(XmlDictionaryString localName)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("localName"));
			}
			return this.node.IsLocalName(localName);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007255 File Offset: 0x00005455
		public override bool IsNamespaceUri(string namespaceUri)
		{
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			return this.node.IsNamespaceUri(namespaceUri);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007271 File Offset: 0x00005471
		public override bool IsNamespaceUri(XmlDictionaryString namespaceUri)
		{
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			return this.node.IsNamespaceUri(namespaceUri);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00007290 File Offset: 0x00005490
		public sealed override bool IsStartElement()
		{
			XmlNodeType nodeType = this.node.NodeType;
			if (nodeType == XmlNodeType.Element)
			{
				return true;
			}
			if (nodeType == XmlNodeType.EndElement)
			{
				return false;
			}
			if (nodeType == XmlNodeType.None)
			{
				this.Read();
				if (this.node.NodeType == XmlNodeType.Element)
				{
					return true;
				}
			}
			return this.MoveToContent() == XmlNodeType.Element;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000072DC File Offset: 0x000054DC
		public override bool IsStartElement(string name)
		{
			if (name == null)
			{
				return false;
			}
			int num = name.IndexOf(':');
			string prefix;
			string s;
			if (num == -1)
			{
				prefix = string.Empty;
				s = name;
			}
			else
			{
				prefix = name.Substring(0, num);
				s = name.Substring(num + 1);
			}
			return (this.node.NodeType == XmlNodeType.Element || this.IsStartElement()) && this.node.Prefix == prefix && this.node.LocalName == s;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007354 File Offset: 0x00005554
		public override bool IsStartElement(string localName, string namespaceUri)
		{
			return localName != null && namespaceUri != null && ((this.node.NodeType == XmlNodeType.Element || this.IsStartElement()) && this.node.LocalName == localName) && this.node.IsNamespaceUri(namespaceUri);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000073A4 File Offset: 0x000055A4
		public override bool IsStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			return (this.node.NodeType == XmlNodeType.Element || this.IsStartElement()) && this.node.LocalName == localName && this.node.IsNamespaceUri(namespaceUri);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007404 File Offset: 0x00005604
		public override int IndexOfLocalName(string[] localNames, string namespaceUri)
		{
			if (localNames == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localNames");
			}
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			XmlBaseReader.QNameType qnameType = this.node.QNameType;
			if (this.node.IsNamespaceUri(namespaceUri))
			{
				if (qnameType == XmlBaseReader.QNameType.Normal)
				{
					StringHandle s = this.node.LocalName;
					for (int i = 0; i < localNames.Length; i++)
					{
						string text = localNames[i];
						if (text == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "localNames[{0}]", i));
						}
						if (s == text)
						{
							return i;
						}
					}
				}
				else
				{
					PrefixHandle prefix = this.node.Namespace.Prefix;
					for (int j = 0; j < localNames.Length; j++)
					{
						string text2 = localNames[j];
						if (text2 == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "localNames[{0}]", j));
						}
						if (prefix == text2)
						{
							return j;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000074F0 File Offset: 0x000056F0
		public override int IndexOfLocalName(XmlDictionaryString[] localNames, XmlDictionaryString namespaceUri)
		{
			if (localNames == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localNames");
			}
			if (namespaceUri == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("namespaceUri");
			}
			XmlBaseReader.QNameType qnameType = this.node.QNameType;
			if (this.node.IsNamespaceUri(namespaceUri))
			{
				if (qnameType == XmlBaseReader.QNameType.Normal)
				{
					StringHandle s = this.node.LocalName;
					for (int i = 0; i < localNames.Length; i++)
					{
						XmlDictionaryString xmlDictionaryString = localNames[i];
						if (xmlDictionaryString == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "localNames[{0}]", i));
						}
						if (s == xmlDictionaryString)
						{
							return i;
						}
					}
				}
				else
				{
					PrefixHandle prefix = this.node.Namespace.Prefix;
					for (int j = 0; j < localNames.Length; j++)
					{
						XmlDictionaryString xmlDictionaryString2 = localNames[j];
						if (xmlDictionaryString2 == null)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "localNames[{0}]", j));
						}
						if (prefix == xmlDictionaryString2)
						{
							return j;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000075DC File Offset: 0x000057DC
		public override int ReadValueChunk(char[] chars, int offset, int count)
		{
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > chars.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					chars.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					chars.Length - offset
				})));
			}
			int num;
			if (this.value == null && this.node.QNameType == XmlBaseReader.QNameType.Normal && this.node.Value.TryReadChars(chars, offset, count, out num))
			{
				return num;
			}
			string text = this.Value;
			num = Math.Min(count, text.Length);
			text.CopyTo(0, chars, offset, num);
			this.value = text.Substring(num);
			return num;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000076F8 File Offset: 0x000058F8
		public override int ReadValueAsBase64(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					buffer.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					buffer.Length - offset
				})));
			}
			if (count == 0)
			{
				return 0;
			}
			int result;
			if (this.value == null && this.trailByteCount == 0 && this.trailCharCount == 0 && this.node.QNameType == XmlBaseReader.QNameType.Normal && this.node.Value.TryReadBase64(buffer, offset, count, out result))
			{
				return result;
			}
			return this.ReadBytes(XmlBaseReader.Base64Encoding, 3, 4, buffer, offset, Math.Min(count, 512), false);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007818 File Offset: 0x00005A18
		public override string ReadElementContentAsString()
		{
			if (this.node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			if (this.node.IsEmptyElement)
			{
				this.Read();
				return string.Empty;
			}
			this.Read();
			string result = this.ReadContentAsString();
			this.ReadEndElement();
			return result;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007866 File Offset: 0x00005A66
		public override string ReadElementString()
		{
			this.MoveToStartElement();
			if (this.IsEmptyElement)
			{
				this.Read();
				return string.Empty;
			}
			this.Read();
			string result = this.ReadString();
			this.ReadEndElement();
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007896 File Offset: 0x00005A96
		public override string ReadElementString(string name)
		{
			this.MoveToStartElement(name);
			return this.ReadElementString();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000078A5 File Offset: 0x00005AA5
		public override string ReadElementString(string localName, string namespaceUri)
		{
			this.MoveToStartElement(localName, namespaceUri);
			return this.ReadElementString();
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000078B5 File Offset: 0x00005AB5
		public override void ReadStartElement()
		{
			if (this.node.NodeType != XmlNodeType.Element)
			{
				this.MoveToStartElement();
			}
			this.Read();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000078D2 File Offset: 0x00005AD2
		public override void ReadStartElement(string name)
		{
			this.MoveToStartElement(name);
			this.Read();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000078E2 File Offset: 0x00005AE2
		public override void ReadStartElement(string localName, string namespaceUri)
		{
			this.MoveToStartElement(localName, namespaceUri);
			this.Read();
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000078F4 File Offset: 0x00005AF4
		public override void ReadEndElement()
		{
			if (this.node.NodeType != XmlNodeType.EndElement && this.MoveToContent() != XmlNodeType.EndElement)
			{
				int num = (this.node.NodeType == XmlNodeType.Element) ? (this.depth - 1) : this.depth;
				if (num == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("No corresponding start element is open.")));
				}
				XmlBaseReader.XmlElementNode xmlElementNode = this.elementNodes[num];
				XmlExceptionHelper.ThrowEndElementExpected(this, xmlElementNode.LocalName.GetString(), xmlElementNode.Namespace.Uri.GetString());
			}
			this.Read();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007984 File Offset: 0x00005B84
		public override bool ReadAttributeValue()
		{
			XmlBaseReader.XmlAttributeTextNode attributeText = this.node.AttributeText;
			if (attributeText == null)
			{
				return false;
			}
			this.MoveToNode(attributeText);
			return true;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000079AA File Offset: 0x00005BAA
		public override ReadState ReadState
		{
			get
			{
				return this.node.ReadState;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000079B7 File Offset: 0x00005BB7
		private void SkipValue(XmlBaseReader.XmlNode node)
		{
			if (node.SkipValue)
			{
				this.Read();
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000079C8 File Offset: 0x00005BC8
		public override bool TryGetBase64ContentLength(out int length)
		{
			if (this.trailByteCount == 0 && this.trailCharCount == 0 && this.value == null)
			{
				XmlBaseReader.XmlNode xmlNode = this.Node;
				if (xmlNode.IsAtomicValue)
				{
					return xmlNode.Value.TryGetByteArrayLength(out length);
				}
			}
			return base.TryGetBase64ContentLength(out length);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007A10 File Offset: 0x00005C10
		public override byte[] ReadContentAsBase64()
		{
			if (this.trailByteCount == 0 && this.trailCharCount == 0 && this.value == null)
			{
				XmlBaseReader.XmlNode xmlNode = this.Node;
				if (xmlNode.IsAtomicValue)
				{
					byte[] array = xmlNode.Value.ToByteArray();
					if (array.Length > this.quotas.MaxArrayLength)
					{
						XmlExceptionHelper.ThrowMaxArrayLengthExceeded(this, this.quotas.MaxArrayLength);
					}
					this.SkipValue(xmlNode);
					return array;
				}
			}
			if (!this.bufferReader.IsStreamed)
			{
				return base.ReadContentAsBase64(this.quotas.MaxArrayLength, this.bufferReader.Buffer.Length);
			}
			return base.ReadContentAsBase64(this.quotas.MaxArrayLength, 65535);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007ABC File Offset: 0x00005CBC
		public override int ReadElementContentAsBase64(byte[] buffer, int offset, int count)
		{
			if (!this.readingElement)
			{
				if (this.IsEmptyElement)
				{
					this.Read();
					return 0;
				}
				this.ReadStartElement();
				this.readingElement = true;
			}
			int num = this.ReadContentAsBase64(buffer, offset, count);
			if (num == 0)
			{
				this.ReadEndElement();
				this.readingElement = false;
			}
			return num;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007B08 File Offset: 0x00005D08
		public override int ReadContentAsBase64(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					buffer.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					buffer.Length - offset
				})));
			}
			if (count == 0)
			{
				return 0;
			}
			if (this.trailByteCount == 0 && this.trailCharCount == 0 && this.value == null && this.node.QNameType == XmlBaseReader.QNameType.Normal)
			{
				int num;
				while (this.node.NodeType != XmlNodeType.Comment && this.node.Value.TryReadBase64(buffer, offset, count, out num))
				{
					if (num != 0)
					{
						return num;
					}
					this.Read();
				}
			}
			XmlNodeType nodeType = this.node.NodeType;
			if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.EndElement)
			{
				return 0;
			}
			return this.ReadBytes(XmlBaseReader.Base64Encoding, 3, 4, buffer, offset, Math.Min(count, 512), true);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007C59 File Offset: 0x00005E59
		public override byte[] ReadContentAsBinHex()
		{
			return base.ReadContentAsBinHex(this.quotas.MaxArrayLength);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007C6C File Offset: 0x00005E6C
		public override int ReadElementContentAsBinHex(byte[] buffer, int offset, int count)
		{
			if (!this.readingElement)
			{
				if (this.IsEmptyElement)
				{
					this.Read();
					return 0;
				}
				this.ReadStartElement();
				this.readingElement = true;
			}
			int num = this.ReadContentAsBinHex(buffer, offset, count);
			if (num == 0)
			{
				this.ReadEndElement();
				this.readingElement = false;
			}
			return num;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007CB8 File Offset: 0x00005EB8
		public override int ReadContentAsBinHex(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					buffer.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					buffer.Length - offset
				})));
			}
			if (count == 0)
			{
				return 0;
			}
			return this.ReadBytes(XmlBaseReader.BinHexEncoding, 1, 2, buffer, offset, Math.Min(count, 512), true);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007D9C File Offset: 0x00005F9C
		private int ReadBytes(Encoding encoding, int byteBlock, int charBlock, byte[] buffer, int offset, int byteCount, bool readContent)
		{
			if (this.trailByteCount > 0)
			{
				int num = Math.Min(this.trailByteCount, byteCount);
				Array.Copy(this.trailBytes, 0, buffer, offset, num);
				this.trailByteCount -= num;
				Array.Copy(this.trailBytes, num, this.trailBytes, 0, this.trailByteCount);
				return num;
			}
			XmlNodeType nodeType = this.node.NodeType;
			if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.EndElement)
			{
				return 0;
			}
			int num2;
			if (byteCount < byteBlock)
			{
				num2 = charBlock;
			}
			else
			{
				num2 = byteCount / byteBlock * charBlock;
			}
			char[] charBuffer = this.GetCharBuffer(num2);
			int i = 0;
			int result;
			for (;;)
			{
				if (this.trailCharCount > 0)
				{
					Array.Copy(this.trailChars, 0, charBuffer, i, this.trailCharCount);
					i += this.trailCharCount;
					this.trailCharCount = 0;
				}
				while (i < charBlock)
				{
					int num3;
					if (readContent)
					{
						num3 = this.ReadContentAsChars(charBuffer, i, num2 - i);
						if (num3 == 1 && charBuffer[i] == '\n')
						{
							continue;
						}
					}
					else
					{
						num3 = this.ReadValueChunk(charBuffer, i, num2 - i);
					}
					if (num3 == 0)
					{
						break;
					}
					i += num3;
				}
				if (i >= charBlock)
				{
					this.trailCharCount = i % charBlock;
					if (this.trailCharCount > 0)
					{
						if (this.trailChars == null)
						{
							this.trailChars = new char[4];
						}
						i -= this.trailCharCount;
						Array.Copy(charBuffer, i, this.trailChars, 0, this.trailCharCount);
					}
				}
				try
				{
					if (byteCount < byteBlock)
					{
						if (this.trailBytes == null)
						{
							this.trailBytes = new byte[3];
						}
						this.trailByteCount = encoding.GetBytes(charBuffer, 0, i, this.trailBytes, 0);
						int num4 = Math.Min(this.trailByteCount, byteCount);
						Array.Copy(this.trailBytes, 0, buffer, offset, num4);
						this.trailByteCount -= num4;
						Array.Copy(this.trailBytes, num4, this.trailBytes, 0, this.trailByteCount);
						result = num4;
					}
					else
					{
						result = encoding.GetBytes(charBuffer, 0, i, buffer, offset);
					}
				}
				catch (FormatException ex)
				{
					int num5 = 0;
					int num6 = 0;
					for (;;)
					{
						if (num6 >= i || !XmlConverter.IsWhitespace(charBuffer[num6]))
						{
							if (num6 == i)
							{
								break;
							}
							charBuffer[num5++] = charBuffer[num6++];
						}
						else
						{
							num6++;
						}
					}
					if (num5 == i)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(ex.Message, ex.InnerException));
					}
					i = num5;
					continue;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007FE4 File Offset: 0x000061E4
		public override string ReadContentAsString()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (xmlNode.IsAtomicValue)
			{
				string @string;
				if (this.value != null)
				{
					@string = this.value;
					if (xmlNode.AttributeText == null)
					{
						this.value = string.Empty;
					}
				}
				else
				{
					@string = xmlNode.Value.GetString();
					this.SkipValue(xmlNode);
					if (@string.Length > this.quotas.MaxStringContentLength)
					{
						XmlExceptionHelper.ThrowMaxStringContentLengthExceeded(this, this.quotas.MaxStringContentLength);
					}
				}
				return @string;
			}
			return base.ReadContentAsString(this.quotas.MaxStringContentLength);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008070 File Offset: 0x00006270
		public override bool ReadContentAsBoolean()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				bool result = xmlNode.Value.ToBoolean();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToBoolean(this.ReadContentAsString());
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000080B4 File Offset: 0x000062B4
		public override long ReadContentAsLong()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				long result = xmlNode.Value.ToLong();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToInt64(this.ReadContentAsString());
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000080F8 File Offset: 0x000062F8
		public override int ReadContentAsInt()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				int result = xmlNode.Value.ToInt();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToInt32(this.ReadContentAsString());
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000813C File Offset: 0x0000633C
		public override DateTime ReadContentAsDateTime()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				DateTime result = xmlNode.Value.ToDateTime();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToDateTime(this.ReadContentAsString());
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008180 File Offset: 0x00006380
		public override double ReadContentAsDouble()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				double result = xmlNode.Value.ToDouble();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToDouble(this.ReadContentAsString());
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000081C4 File Offset: 0x000063C4
		public override float ReadContentAsFloat()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				float result = xmlNode.Value.ToSingle();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToSingle(this.ReadContentAsString());
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008208 File Offset: 0x00006408
		public override decimal ReadContentAsDecimal()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				decimal result = xmlNode.Value.ToDecimal();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToDecimal(this.ReadContentAsString());
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000824C File Offset: 0x0000644C
		public override UniqueId ReadContentAsUniqueId()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				UniqueId result = xmlNode.Value.ToUniqueId();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToUniqueId(this.ReadContentAsString());
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008290 File Offset: 0x00006490
		public override TimeSpan ReadContentAsTimeSpan()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				TimeSpan result = xmlNode.Value.ToTimeSpan();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToTimeSpan(this.ReadContentAsString());
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000082D4 File Offset: 0x000064D4
		public override Guid ReadContentAsGuid()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				Guid result = xmlNode.Value.ToGuid();
				this.SkipValue(xmlNode);
				return result;
			}
			return XmlConverter.ToGuid(this.ReadContentAsString());
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008318 File Offset: 0x00006518
		public override object ReadContentAsObject()
		{
			XmlBaseReader.XmlNode xmlNode = this.Node;
			if (this.value == null && xmlNode.IsAtomicValue)
			{
				object result = xmlNode.Value.ToObject();
				this.SkipValue(xmlNode);
				return result;
			}
			return this.ReadContentAsString();
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008358 File Offset: 0x00006558
		public override object ReadContentAs(Type type, IXmlNamespaceResolver namespaceResolver)
		{
			if (type == typeof(ulong))
			{
				if (this.value == null && this.node.IsAtomicValue)
				{
					ulong num = this.node.Value.ToULong();
					this.SkipValue(this.node);
					return num;
				}
				return XmlConverter.ToUInt64(this.ReadContentAsString());
			}
			else
			{
				if (type == typeof(bool))
				{
					return this.ReadContentAsBoolean();
				}
				if (type == typeof(int))
				{
					return this.ReadContentAsInt();
				}
				if (type == typeof(long))
				{
					return this.ReadContentAsLong();
				}
				if (type == typeof(float))
				{
					return this.ReadContentAsFloat();
				}
				if (type == typeof(double))
				{
					return this.ReadContentAsDouble();
				}
				if (type == typeof(decimal))
				{
					return this.ReadContentAsDecimal();
				}
				if (type == typeof(DateTime))
				{
					return this.ReadContentAsDateTime();
				}
				if (type == typeof(UniqueId))
				{
					return this.ReadContentAsUniqueId();
				}
				if (type == typeof(Guid))
				{
					return this.ReadContentAsGuid();
				}
				if (type == typeof(TimeSpan))
				{
					return this.ReadContentAsTimeSpan();
				}
				if (type == typeof(object))
				{
					return this.ReadContentAsObject();
				}
				return base.ReadContentAs(type, namespaceResolver);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008507 File Offset: 0x00006707
		public override void ResolveEntity()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("The reader cannot be advanced.")));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008520 File Offset: 0x00006720
		public override void Skip()
		{
			if (this.node.ReadState != ReadState.Interactive)
			{
				return;
			}
			if ((this.node.NodeType == XmlNodeType.Element || this.MoveToElement()) && !this.IsEmptyElement)
			{
				int num = this.Depth;
				while (this.Read() && num < this.Depth)
				{
				}
				if (this.node.NodeType == XmlNodeType.EndElement)
				{
					this.Read();
					return;
				}
			}
			else
			{
				this.Read();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00008590 File Offset: 0x00006790
		public override string Value
		{
			get
			{
				if (this.value == null)
				{
					this.value = this.node.ValueAsString;
				}
				return this.value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000085B4 File Offset: 0x000067B4
		public override Type ValueType
		{
			get
			{
				if (this.value == null && this.node.QNameType == XmlBaseReader.QNameType.Normal)
				{
					Type type = this.node.Value.ToType();
					if (this.node.IsAtomicValue)
					{
						return type;
					}
					if (type == typeof(byte[]))
					{
						return type;
					}
				}
				return typeof(string);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00008614 File Offset: 0x00006814
		public override string XmlLang
		{
			get
			{
				return this.nsMgr.XmlLang;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00008621 File Offset: 0x00006821
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.nsMgr.XmlSpace;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000862E File Offset: 0x0000682E
		public override bool TryGetLocalNameAsDictionaryString(out XmlDictionaryString localName)
		{
			return this.node.TryGetLocalNameAsDictionaryString(out localName);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000863C File Offset: 0x0000683C
		public override bool TryGetNamespaceUriAsDictionaryString(out XmlDictionaryString localName)
		{
			return this.node.TryGetNamespaceUriAsDictionaryString(out localName);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000864A File Offset: 0x0000684A
		public override bool TryGetValueAsDictionaryString(out XmlDictionaryString value)
		{
			return this.node.TryGetValueAsDictionaryString(out value);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008658 File Offset: 0x00006858
		public override short[] ReadInt16Array(string localName, string namespaceUri)
		{
			return Int16ArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008672 File Offset: 0x00006872
		public override short[] ReadInt16Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return Int16ArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000868C File Offset: 0x0000688C
		public override int[] ReadInt32Array(string localName, string namespaceUri)
		{
			return Int32ArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000086A6 File Offset: 0x000068A6
		public override int[] ReadInt32Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return Int32ArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000086C0 File Offset: 0x000068C0
		public override long[] ReadInt64Array(string localName, string namespaceUri)
		{
			return Int64ArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000086DA File Offset: 0x000068DA
		public override long[] ReadInt64Array(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return Int64ArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000086F4 File Offset: 0x000068F4
		public override float[] ReadSingleArray(string localName, string namespaceUri)
		{
			return SingleArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000870E File Offset: 0x0000690E
		public override float[] ReadSingleArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return SingleArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008728 File Offset: 0x00006928
		public override double[] ReadDoubleArray(string localName, string namespaceUri)
		{
			return DoubleArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008742 File Offset: 0x00006942
		public override double[] ReadDoubleArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return DoubleArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000875C File Offset: 0x0000695C
		public override decimal[] ReadDecimalArray(string localName, string namespaceUri)
		{
			return DecimalArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008776 File Offset: 0x00006976
		public override decimal[] ReadDecimalArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return DecimalArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008790 File Offset: 0x00006990
		public override DateTime[] ReadDateTimeArray(string localName, string namespaceUri)
		{
			return DateTimeArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000087AA File Offset: 0x000069AA
		public override DateTime[] ReadDateTimeArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return DateTimeArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000087C4 File Offset: 0x000069C4
		public override Guid[] ReadGuidArray(string localName, string namespaceUri)
		{
			return GuidArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000087DE File Offset: 0x000069DE
		public override Guid[] ReadGuidArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return GuidArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000087F8 File Offset: 0x000069F8
		public override TimeSpan[] ReadTimeSpanArray(string localName, string namespaceUri)
		{
			return TimeSpanArrayHelperWithString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008812 File Offset: 0x00006A12
		public override TimeSpan[] ReadTimeSpanArray(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
		{
			return TimeSpanArrayHelperWithDictionaryString.Instance.ReadArray(this, localName, namespaceUri, this.quotas.MaxArrayLength);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000882C File Offset: 0x00006A2C
		public string GetOpenElements()
		{
			string text = string.Empty;
			for (int i = this.depth; i > 0; i--)
			{
				string @string = this.elementNodes[i].LocalName.GetString();
				if (i != this.depth)
				{
					text += ", ";
				}
				text += @string;
			}
			return text;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008881 File Offset: 0x00006A81
		private char[] GetCharBuffer(int count)
		{
			if (count > 1024)
			{
				return new char[count];
			}
			if (this.chars == null || this.chars.Length < count)
			{
				this.chars = new char[count];
			}
			return this.chars;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000088B8 File Offset: 0x00006AB8
		private void SignStartElement(XmlSigningNodeWriter writer)
		{
			int prefixOffset;
			int prefixLength;
			byte[] @string = this.node.Prefix.GetString(out prefixOffset, out prefixLength);
			int localNameOffset;
			int localNameLength;
			byte[] string2 = this.node.LocalName.GetString(out localNameOffset, out localNameLength);
			writer.WriteStartElement(@string, prefixOffset, prefixLength, string2, localNameOffset, localNameLength);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00008900 File Offset: 0x00006B00
		private void SignAttribute(XmlSigningNodeWriter writer, XmlBaseReader.XmlAttributeNode attributeNode)
		{
			if (attributeNode.QNameType == XmlBaseReader.QNameType.Normal)
			{
				int prefixOffset;
				int prefixLength;
				byte[] @string = attributeNode.Prefix.GetString(out prefixOffset, out prefixLength);
				int localNameOffset;
				int localNameLength;
				byte[] string2 = attributeNode.LocalName.GetString(out localNameOffset, out localNameLength);
				writer.WriteStartAttribute(@string, prefixOffset, prefixLength, string2, localNameOffset, localNameLength);
				attributeNode.Value.Sign(writer);
				writer.WriteEndAttribute();
				return;
			}
			int prefixOffset2;
			int prefixLength2;
			byte[] string3 = attributeNode.Namespace.Prefix.GetString(out prefixOffset2, out prefixLength2);
			int nsOffset;
			int nsLength;
			byte[] string4 = attributeNode.Namespace.Uri.GetString(out nsOffset, out nsLength);
			writer.WriteXmlnsAttribute(string3, prefixOffset2, prefixLength2, string4, nsOffset, nsLength);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008998 File Offset: 0x00006B98
		private void SignEndElement(XmlSigningNodeWriter writer)
		{
			int prefixOffset;
			int prefixLength;
			byte[] @string = this.node.Prefix.GetString(out prefixOffset, out prefixLength);
			int localNameOffset;
			int localNameLength;
			byte[] string2 = this.node.LocalName.GetString(out localNameOffset, out localNameLength);
			writer.WriteEndElement(@string, prefixOffset, prefixLength, string2, localNameOffset, localNameLength);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000089E0 File Offset: 0x00006BE0
		private void SignNode(XmlSigningNodeWriter writer)
		{
			XmlNodeType nodeType = this.node.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.None:
				return;
			case XmlNodeType.Element:
				this.SignStartElement(writer);
				for (int i = 0; i < this.attributeCount; i++)
				{
					this.SignAttribute(writer, this.attributeNodes[i]);
				}
				writer.WriteEndStartElement(this.node.IsEmptyElement);
				return;
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
				goto IL_C6;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			case XmlNodeType.Comment:
				writer.WriteComment(this.node.Value.GetString());
				return;
			default:
				switch (nodeType)
				{
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					break;
				case XmlNodeType.EndElement:
					this.SignEndElement(writer);
					return;
				case XmlNodeType.EndEntity:
					goto IL_C6;
				case XmlNodeType.XmlDeclaration:
					writer.WriteDeclaration();
					return;
				default:
					goto IL_C6;
				}
				break;
			}
			this.node.Value.Sign(writer);
			return;
			IL_C6:
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000066E8 File Offset: 0x000048E8
		public override bool CanCanonicalize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00008ABE File Offset: 0x00006CBE
		protected bool Signing
		{
			get
			{
				return this.signing;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008AC6 File Offset: 0x00006CC6
		protected void SignNode()
		{
			if (this.signing)
			{
				this.SignNode(this.signingWriter);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008ADC File Offset: 0x00006CDC
		public override void StartCanonicalization(Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			if (this.signing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("XML canonicalization started")));
			}
			if (this.signingWriter == null)
			{
				this.signingWriter = this.CreateSigningNodeWriter();
			}
			this.signingWriter.SetOutput(XmlNodeWriter.Null, stream, includeComments, inclusivePrefixes);
			this.nsMgr.Sign(this.signingWriter);
			this.signing = true;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008B45 File Offset: 0x00006D45
		public override void EndCanonicalization()
		{
			if (!this.signing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("XML canonicalization was not started.")));
			}
			this.signingWriter.Flush();
			this.signingWriter.Close();
			this.signing = false;
		}

		// Token: 0x060001B2 RID: 434
		protected abstract XmlSigningNodeWriter CreateSigningNodeWriter();

		// Token: 0x060001B3 RID: 435 RVA: 0x00008B81 File Offset: 0x00006D81
		// Note: this type is marked as 'beforefieldinit'.
		static XmlBaseReader()
		{
		}

		// Token: 0x040000A1 RID: 161
		private XmlBufferReader bufferReader;

		// Token: 0x040000A2 RID: 162
		private XmlBaseReader.XmlNode node;

		// Token: 0x040000A3 RID: 163
		private XmlBaseReader.NamespaceManager nsMgr;

		// Token: 0x040000A4 RID: 164
		private XmlBaseReader.XmlElementNode[] elementNodes;

		// Token: 0x040000A5 RID: 165
		private XmlBaseReader.XmlAttributeNode[] attributeNodes;

		// Token: 0x040000A6 RID: 166
		private XmlBaseReader.XmlAtomicTextNode atomicTextNode;

		// Token: 0x040000A7 RID: 167
		private int depth;

		// Token: 0x040000A8 RID: 168
		private int attributeCount;

		// Token: 0x040000A9 RID: 169
		private int attributeStart;

		// Token: 0x040000AA RID: 170
		private XmlDictionaryReaderQuotas quotas;

		// Token: 0x040000AB RID: 171
		private XmlNameTable nameTable;

		// Token: 0x040000AC RID: 172
		private XmlBaseReader.XmlDeclarationNode declarationNode;

		// Token: 0x040000AD RID: 173
		private XmlBaseReader.XmlComplexTextNode complexTextNode;

		// Token: 0x040000AE RID: 174
		private XmlBaseReader.XmlWhitespaceTextNode whitespaceTextNode;

		// Token: 0x040000AF RID: 175
		private XmlBaseReader.XmlCDataNode cdataNode;

		// Token: 0x040000B0 RID: 176
		private XmlBaseReader.XmlCommentNode commentNode;

		// Token: 0x040000B1 RID: 177
		private XmlBaseReader.XmlElementNode rootElementNode;

		// Token: 0x040000B2 RID: 178
		private int attributeIndex;

		// Token: 0x040000B3 RID: 179
		private char[] chars;

		// Token: 0x040000B4 RID: 180
		private string prefix;

		// Token: 0x040000B5 RID: 181
		private string localName;

		// Token: 0x040000B6 RID: 182
		private string ns;

		// Token: 0x040000B7 RID: 183
		private string value;

		// Token: 0x040000B8 RID: 184
		private int trailCharCount;

		// Token: 0x040000B9 RID: 185
		private int trailByteCount;

		// Token: 0x040000BA RID: 186
		private char[] trailChars;

		// Token: 0x040000BB RID: 187
		private byte[] trailBytes;

		// Token: 0x040000BC RID: 188
		private bool rootElement;

		// Token: 0x040000BD RID: 189
		private bool readingElement;

		// Token: 0x040000BE RID: 190
		private XmlSigningNodeWriter signingWriter;

		// Token: 0x040000BF RID: 191
		private bool signing;

		// Token: 0x040000C0 RID: 192
		private XmlBaseReader.AttributeSorter attributeSorter;

		// Token: 0x040000C1 RID: 193
		private static XmlBaseReader.XmlInitialNode initialNode = new XmlBaseReader.XmlInitialNode(XmlBufferReader.Empty);

		// Token: 0x040000C2 RID: 194
		private static XmlBaseReader.XmlEndOfFileNode endOfFileNode = new XmlBaseReader.XmlEndOfFileNode(XmlBufferReader.Empty);

		// Token: 0x040000C3 RID: 195
		private static XmlBaseReader.XmlClosedNode closedNode = new XmlBaseReader.XmlClosedNode(XmlBufferReader.Empty);

		// Token: 0x040000C4 RID: 196
		private static BinHexEncoding binhexEncoding;

		// Token: 0x040000C5 RID: 197
		private static Base64Encoding base64Encoding;

		// Token: 0x040000C6 RID: 198
		private const string xmlns = "xmlns";

		// Token: 0x040000C7 RID: 199
		private const string xml = "xml";

		// Token: 0x040000C8 RID: 200
		private const string xmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x040000C9 RID: 201
		private const string xmlNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x0200002A RID: 42
		protected enum QNameType
		{
			// Token: 0x040000CB RID: 203
			Normal,
			// Token: 0x040000CC RID: 204
			Xmlns
		}

		// Token: 0x0200002B RID: 43
		protected class XmlNode
		{
			// Token: 0x060001B4 RID: 436 RVA: 0x00008BB0 File Offset: 0x00006DB0
			protected XmlNode(XmlNodeType nodeType, PrefixHandle prefix, StringHandle localName, ValueHandle value, XmlBaseReader.XmlNode.XmlNodeFlags nodeFlags, ReadState readState, XmlBaseReader.XmlAttributeTextNode attributeTextNode, int depthDelta)
			{
				this.nodeType = nodeType;
				this.prefix = prefix;
				this.localName = localName;
				this.value = value;
				this.ns = XmlBaseReader.NamespaceManager.EmptyNamespace;
				this.hasValue = ((nodeFlags & XmlBaseReader.XmlNode.XmlNodeFlags.HasValue) > XmlBaseReader.XmlNode.XmlNodeFlags.None);
				this.canGetAttribute = ((nodeFlags & XmlBaseReader.XmlNode.XmlNodeFlags.CanGetAttribute) > XmlBaseReader.XmlNode.XmlNodeFlags.None);
				this.canMoveToElement = ((nodeFlags & XmlBaseReader.XmlNode.XmlNodeFlags.CanMoveToElement) > XmlBaseReader.XmlNode.XmlNodeFlags.None);
				this.isAtomicValue = ((nodeFlags & XmlBaseReader.XmlNode.XmlNodeFlags.AtomicValue) > XmlBaseReader.XmlNode.XmlNodeFlags.None);
				this.skipValue = ((nodeFlags & XmlBaseReader.XmlNode.XmlNodeFlags.SkipValue) > XmlBaseReader.XmlNode.XmlNodeFlags.None);
				this.hasContent = ((nodeFlags & XmlBaseReader.XmlNode.XmlNodeFlags.HasContent) > XmlBaseReader.XmlNode.XmlNodeFlags.None);
				this.readState = readState;
				this.attributeTextNode = attributeTextNode;
				this.exitScope = (nodeType == XmlNodeType.EndElement);
				this.depthDelta = depthDelta;
				this.isEmptyElement = false;
				this.quoteChar = '"';
				this.qnameType = XmlBaseReader.QNameType.Normal;
			}

			// Token: 0x17000033 RID: 51
			// (get) Token: 0x060001B5 RID: 437 RVA: 0x00008C74 File Offset: 0x00006E74
			public bool HasValue
			{
				get
				{
					return this.hasValue;
				}
			}

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x060001B6 RID: 438 RVA: 0x00008C7C File Offset: 0x00006E7C
			public ReadState ReadState
			{
				get
				{
					return this.readState;
				}
			}

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x060001B7 RID: 439 RVA: 0x00008C84 File Offset: 0x00006E84
			public StringHandle LocalName
			{
				get
				{
					return this.localName;
				}
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060001B8 RID: 440 RVA: 0x00008C8C File Offset: 0x00006E8C
			public PrefixHandle Prefix
			{
				get
				{
					return this.prefix;
				}
			}

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008C94 File Offset: 0x00006E94
			public bool CanGetAttribute
			{
				get
				{
					return this.canGetAttribute;
				}
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060001BA RID: 442 RVA: 0x00008C9C File Offset: 0x00006E9C
			public bool CanMoveToElement
			{
				get
				{
					return this.canMoveToElement;
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060001BB RID: 443 RVA: 0x00008CA4 File Offset: 0x00006EA4
			public XmlBaseReader.XmlAttributeTextNode AttributeText
			{
				get
				{
					return this.attributeTextNode;
				}
			}

			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060001BC RID: 444 RVA: 0x00008CAC File Offset: 0x00006EAC
			public bool SkipValue
			{
				get
				{
					return this.skipValue;
				}
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060001BD RID: 445 RVA: 0x00008CB4 File Offset: 0x00006EB4
			public ValueHandle Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060001BE RID: 446 RVA: 0x00008CBC File Offset: 0x00006EBC
			public int DepthDelta
			{
				get
				{
					return this.depthDelta;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060001BF RID: 447 RVA: 0x00008CC4 File Offset: 0x00006EC4
			public bool HasContent
			{
				get
				{
					return this.hasContent;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060001C0 RID: 448 RVA: 0x00008CCC File Offset: 0x00006ECC
			// (set) Token: 0x060001C1 RID: 449 RVA: 0x00008CD4 File Offset: 0x00006ED4
			public XmlNodeType NodeType
			{
				get
				{
					return this.nodeType;
				}
				set
				{
					this.nodeType = value;
				}
			}

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060001C2 RID: 450 RVA: 0x00008CDD File Offset: 0x00006EDD
			// (set) Token: 0x060001C3 RID: 451 RVA: 0x00008CE5 File Offset: 0x00006EE5
			public XmlBaseReader.QNameType QNameType
			{
				get
				{
					return this.qnameType;
				}
				set
				{
					this.qnameType = value;
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060001C4 RID: 452 RVA: 0x00008CEE File Offset: 0x00006EEE
			// (set) Token: 0x060001C5 RID: 453 RVA: 0x00008CF6 File Offset: 0x00006EF6
			public XmlBaseReader.Namespace Namespace
			{
				get
				{
					return this.ns;
				}
				set
				{
					this.ns = value;
				}
			}

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060001C6 RID: 454 RVA: 0x00008CFF File Offset: 0x00006EFF
			// (set) Token: 0x060001C7 RID: 455 RVA: 0x00008D07 File Offset: 0x00006F07
			public bool IsAtomicValue
			{
				get
				{
					return this.isAtomicValue;
				}
				set
				{
					this.isAtomicValue = value;
				}
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060001C8 RID: 456 RVA: 0x00008D10 File Offset: 0x00006F10
			// (set) Token: 0x060001C9 RID: 457 RVA: 0x00008D18 File Offset: 0x00006F18
			public bool ExitScope
			{
				get
				{
					return this.exitScope;
				}
				set
				{
					this.exitScope = value;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060001CA RID: 458 RVA: 0x00008D21 File Offset: 0x00006F21
			// (set) Token: 0x060001CB RID: 459 RVA: 0x00008D29 File Offset: 0x00006F29
			public bool IsEmptyElement
			{
				get
				{
					return this.isEmptyElement;
				}
				set
				{
					this.isEmptyElement = value;
				}
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060001CC RID: 460 RVA: 0x00008D32 File Offset: 0x00006F32
			// (set) Token: 0x060001CD RID: 461 RVA: 0x00008D3A File Offset: 0x00006F3A
			public char QuoteChar
			{
				get
				{
					return this.quoteChar;
				}
				set
				{
					this.quoteChar = value;
				}
			}

			// Token: 0x060001CE RID: 462 RVA: 0x00008D43 File Offset: 0x00006F43
			public bool IsLocalName(string localName)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.LocalName == localName;
				}
				return this.Namespace.Prefix == localName;
			}

			// Token: 0x060001CF RID: 463 RVA: 0x00008D6B File Offset: 0x00006F6B
			public bool IsLocalName(XmlDictionaryString localName)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.LocalName == localName;
				}
				return this.Namespace.Prefix == localName;
			}

			// Token: 0x060001D0 RID: 464 RVA: 0x00008D93 File Offset: 0x00006F93
			public bool IsNamespaceUri(string ns)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.Namespace.IsUri(ns);
				}
				return ns == "http://www.w3.org/2000/xmlns/";
			}

			// Token: 0x060001D1 RID: 465 RVA: 0x00008DB5 File Offset: 0x00006FB5
			public bool IsNamespaceUri(XmlDictionaryString ns)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.Namespace.IsUri(ns);
				}
				return ns.Value == "http://www.w3.org/2000/xmlns/";
			}

			// Token: 0x060001D2 RID: 466 RVA: 0x00008DDC File Offset: 0x00006FDC
			public bool IsLocalNameAndNamespaceUri(string localName, string ns)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.LocalName == localName && this.Namespace.IsUri(ns);
				}
				return this.Namespace.Prefix == localName && ns == "http://www.w3.org/2000/xmlns/";
			}

			// Token: 0x060001D3 RID: 467 RVA: 0x00008E30 File Offset: 0x00007030
			public bool IsLocalNameAndNamespaceUri(XmlDictionaryString localName, XmlDictionaryString ns)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.LocalName == localName && this.Namespace.IsUri(ns);
				}
				return this.Namespace.Prefix == localName && ns.Value == "http://www.w3.org/2000/xmlns/";
			}

			// Token: 0x060001D4 RID: 468 RVA: 0x00008E88 File Offset: 0x00007088
			public bool IsPrefixAndLocalName(string prefix, string localName)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.Prefix == prefix && this.LocalName == localName;
				}
				return prefix == "xmlns" && this.Namespace.Prefix == localName;
			}

			// Token: 0x060001D5 RID: 469 RVA: 0x00008EDA File Offset: 0x000070DA
			public bool TryGetLocalNameAsDictionaryString(out XmlDictionaryString localName)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.LocalName.TryGetDictionaryString(out localName);
				}
				localName = null;
				return false;
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x00008EF5 File Offset: 0x000070F5
			public bool TryGetNamespaceUriAsDictionaryString(out XmlDictionaryString ns)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.Namespace.Uri.TryGetDictionaryString(out ns);
				}
				ns = null;
				return false;
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x00008F15 File Offset: 0x00007115
			public bool TryGetValueAsDictionaryString(out XmlDictionaryString value)
			{
				if (this.qnameType == XmlBaseReader.QNameType.Normal)
				{
					return this.Value.TryGetDictionaryString(out value);
				}
				value = null;
				return false;
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008F30 File Offset: 0x00007130
			public string ValueAsString
			{
				get
				{
					if (this.qnameType == XmlBaseReader.QNameType.Normal)
					{
						return this.Value.GetString();
					}
					return this.Namespace.Uri.GetString();
				}
			}

			// Token: 0x040000CD RID: 205
			private XmlNodeType nodeType;

			// Token: 0x040000CE RID: 206
			private PrefixHandle prefix;

			// Token: 0x040000CF RID: 207
			private StringHandle localName;

			// Token: 0x040000D0 RID: 208
			private ValueHandle value;

			// Token: 0x040000D1 RID: 209
			private XmlBaseReader.Namespace ns;

			// Token: 0x040000D2 RID: 210
			private bool hasValue;

			// Token: 0x040000D3 RID: 211
			private bool canGetAttribute;

			// Token: 0x040000D4 RID: 212
			private bool canMoveToElement;

			// Token: 0x040000D5 RID: 213
			private ReadState readState;

			// Token: 0x040000D6 RID: 214
			private XmlBaseReader.XmlAttributeTextNode attributeTextNode;

			// Token: 0x040000D7 RID: 215
			private bool exitScope;

			// Token: 0x040000D8 RID: 216
			private int depthDelta;

			// Token: 0x040000D9 RID: 217
			private bool isAtomicValue;

			// Token: 0x040000DA RID: 218
			private bool skipValue;

			// Token: 0x040000DB RID: 219
			private XmlBaseReader.QNameType qnameType;

			// Token: 0x040000DC RID: 220
			private bool hasContent;

			// Token: 0x040000DD RID: 221
			private bool isEmptyElement;

			// Token: 0x040000DE RID: 222
			private char quoteChar;

			// Token: 0x0200002C RID: 44
			protected enum XmlNodeFlags
			{
				// Token: 0x040000E0 RID: 224
				None,
				// Token: 0x040000E1 RID: 225
				CanGetAttribute,
				// Token: 0x040000E2 RID: 226
				CanMoveToElement,
				// Token: 0x040000E3 RID: 227
				HasValue = 4,
				// Token: 0x040000E4 RID: 228
				AtomicValue = 8,
				// Token: 0x040000E5 RID: 229
				SkipValue = 16,
				// Token: 0x040000E6 RID: 230
				HasContent = 32
			}
		}

		// Token: 0x0200002D RID: 45
		protected class XmlElementNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001D9 RID: 473 RVA: 0x00008F56 File Offset: 0x00007156
			public XmlElementNode(XmlBufferReader bufferReader) : this(new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader))
			{
			}

			// Token: 0x060001DA RID: 474 RVA: 0x00008F70 File Offset: 0x00007170
			private XmlElementNode(PrefixHandle prefix, StringHandle localName, ValueHandle value) : base(XmlNodeType.Element, prefix, localName, value, (XmlBaseReader.XmlNode.XmlNodeFlags)33, ReadState.Interactive, null, -1)
			{
				this.endElementNode = new XmlBaseReader.XmlEndElementNode(prefix, localName, value);
			}

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060001DB RID: 475 RVA: 0x00008F9A File Offset: 0x0000719A
			public XmlBaseReader.XmlEndElementNode EndElement
			{
				get
				{
					return this.endElementNode;
				}
			}

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060001DC RID: 476 RVA: 0x00008FA2 File Offset: 0x000071A2
			// (set) Token: 0x060001DD RID: 477 RVA: 0x00008FAA File Offset: 0x000071AA
			public int BufferOffset
			{
				get
				{
					return this.bufferOffset;
				}
				set
				{
					this.bufferOffset = value;
				}
			}

			// Token: 0x040000E7 RID: 231
			private XmlBaseReader.XmlEndElementNode endElementNode;

			// Token: 0x040000E8 RID: 232
			private int bufferOffset;

			// Token: 0x040000E9 RID: 233
			public int NameOffset;

			// Token: 0x040000EA RID: 234
			public int NameLength;
		}

		// Token: 0x0200002E RID: 46
		protected class XmlAttributeNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001DE RID: 478 RVA: 0x00008FB3 File Offset: 0x000071B3
			public XmlAttributeNode(XmlBufferReader bufferReader) : this(new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader))
			{
			}

			// Token: 0x060001DF RID: 479 RVA: 0x00008FD0 File Offset: 0x000071D0
			private XmlAttributeNode(PrefixHandle prefix, StringHandle localName, ValueHandle value) : base(XmlNodeType.Attribute, prefix, localName, value, (XmlBaseReader.XmlNode.XmlNodeFlags)15, ReadState.Interactive, new XmlBaseReader.XmlAttributeTextNode(prefix, localName, value), 0)
			{
			}
		}

		// Token: 0x0200002F RID: 47
		protected class XmlEndElementNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001E0 RID: 480 RVA: 0x00008FF4 File Offset: 0x000071F4
			public XmlEndElementNode(PrefixHandle prefix, StringHandle localName, ValueHandle value) : base(XmlNodeType.EndElement, prefix, localName, value, XmlBaseReader.XmlNode.XmlNodeFlags.HasContent, ReadState.Interactive, null, -1)
			{
			}
		}

		// Token: 0x02000030 RID: 48
		protected class XmlTextNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001E1 RID: 481 RVA: 0x00009014 File Offset: 0x00007214
			protected XmlTextNode(XmlNodeType nodeType, PrefixHandle prefix, StringHandle localName, ValueHandle value, XmlBaseReader.XmlNode.XmlNodeFlags nodeFlags, ReadState readState, XmlBaseReader.XmlAttributeTextNode attributeTextNode, int depthDelta) : base(nodeType, prefix, localName, value, nodeFlags, readState, attributeTextNode, depthDelta)
			{
			}
		}

		// Token: 0x02000031 RID: 49
		protected class XmlAtomicTextNode : XmlBaseReader.XmlTextNode
		{
			// Token: 0x060001E2 RID: 482 RVA: 0x00009034 File Offset: 0x00007234
			public XmlAtomicTextNode(XmlBufferReader bufferReader) : base(XmlNodeType.Text, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), (XmlBaseReader.XmlNode.XmlNodeFlags)60, ReadState.Interactive, null, 0)
			{
			}
		}

		// Token: 0x02000032 RID: 50
		protected class XmlComplexTextNode : XmlBaseReader.XmlTextNode
		{
			// Token: 0x060001E3 RID: 483 RVA: 0x00009060 File Offset: 0x00007260
			public XmlComplexTextNode(XmlBufferReader bufferReader) : base(XmlNodeType.Text, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), (XmlBaseReader.XmlNode.XmlNodeFlags)36, ReadState.Interactive, null, 0)
			{
			}
		}

		// Token: 0x02000033 RID: 51
		protected class XmlWhitespaceTextNode : XmlBaseReader.XmlTextNode
		{
			// Token: 0x060001E4 RID: 484 RVA: 0x0000908C File Offset: 0x0000728C
			public XmlWhitespaceTextNode(XmlBufferReader bufferReader) : base(XmlNodeType.Whitespace, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), XmlBaseReader.XmlNode.XmlNodeFlags.HasValue, ReadState.Interactive, null, 0)
			{
			}
		}

		// Token: 0x02000034 RID: 52
		protected class XmlCDataNode : XmlBaseReader.XmlTextNode
		{
			// Token: 0x060001E5 RID: 485 RVA: 0x000090B8 File Offset: 0x000072B8
			public XmlCDataNode(XmlBufferReader bufferReader) : base(XmlNodeType.CDATA, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), (XmlBaseReader.XmlNode.XmlNodeFlags)36, ReadState.Interactive, null, 0)
			{
			}
		}

		// Token: 0x02000035 RID: 53
		protected class XmlAttributeTextNode : XmlBaseReader.XmlTextNode
		{
			// Token: 0x060001E6 RID: 486 RVA: 0x000090E4 File Offset: 0x000072E4
			public XmlAttributeTextNode(PrefixHandle prefix, StringHandle localName, ValueHandle value) : base(XmlNodeType.Text, prefix, localName, value, (XmlBaseReader.XmlNode.XmlNodeFlags)47, ReadState.Interactive, null, 1)
			{
			}
		}

		// Token: 0x02000036 RID: 54
		protected class XmlInitialNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001E7 RID: 487 RVA: 0x00009100 File Offset: 0x00007300
			public XmlInitialNode(XmlBufferReader bufferReader) : base(XmlNodeType.None, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), XmlBaseReader.XmlNode.XmlNodeFlags.None, ReadState.Initial, null, 0)
			{
			}
		}

		// Token: 0x02000037 RID: 55
		protected class XmlDeclarationNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001E8 RID: 488 RVA: 0x0000912C File Offset: 0x0000732C
			public XmlDeclarationNode(XmlBufferReader bufferReader) : base(XmlNodeType.XmlDeclaration, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), XmlBaseReader.XmlNode.XmlNodeFlags.CanGetAttribute, ReadState.Interactive, null, 0)
			{
			}
		}

		// Token: 0x02000038 RID: 56
		protected class XmlCommentNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001E9 RID: 489 RVA: 0x00009158 File Offset: 0x00007358
			public XmlCommentNode(XmlBufferReader bufferReader) : base(XmlNodeType.Comment, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), XmlBaseReader.XmlNode.XmlNodeFlags.HasValue, ReadState.Interactive, null, 0)
			{
			}
		}

		// Token: 0x02000039 RID: 57
		protected class XmlEndOfFileNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001EA RID: 490 RVA: 0x00009184 File Offset: 0x00007384
			public XmlEndOfFileNode(XmlBufferReader bufferReader) : base(XmlNodeType.None, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), XmlBaseReader.XmlNode.XmlNodeFlags.None, ReadState.EndOfFile, null, 0)
			{
			}
		}

		// Token: 0x0200003A RID: 58
		protected class XmlClosedNode : XmlBaseReader.XmlNode
		{
			// Token: 0x060001EB RID: 491 RVA: 0x000091B0 File Offset: 0x000073B0
			public XmlClosedNode(XmlBufferReader bufferReader) : base(XmlNodeType.None, new PrefixHandle(bufferReader), new StringHandle(bufferReader), new ValueHandle(bufferReader), XmlBaseReader.XmlNode.XmlNodeFlags.None, ReadState.Closed, null, 0)
			{
			}
		}

		// Token: 0x0200003B RID: 59
		private class AttributeSorter : IComparer
		{
			// Token: 0x060001EC RID: 492 RVA: 0x000091DA File Offset: 0x000073DA
			public bool Sort(XmlBaseReader.XmlAttributeNode[] attributeNodes, int attributeCount)
			{
				this.attributeIndex1 = -1;
				this.attributeIndex2 = -1;
				this.attributeNodes = attributeNodes;
				this.attributeCount = attributeCount;
				bool result = this.Sort();
				this.attributeNodes = null;
				this.attributeCount = 0;
				return result;
			}

			// Token: 0x060001ED RID: 493 RVA: 0x0000920C File Offset: 0x0000740C
			public void GetIndeces(out int attributeIndex1, out int attributeIndex2)
			{
				attributeIndex1 = this.attributeIndex1;
				attributeIndex2 = this.attributeIndex2;
			}

			// Token: 0x060001EE RID: 494 RVA: 0x0000921E File Offset: 0x0000741E
			public void Close()
			{
				if (this.indeces != null && this.indeces.Length > 32)
				{
					this.indeces = null;
				}
			}

			// Token: 0x060001EF RID: 495 RVA: 0x0000923C File Offset: 0x0000743C
			private bool Sort()
			{
				if (this.indeces != null && this.indeces.Length == this.attributeCount && this.IsSorted())
				{
					return true;
				}
				object[] array = new object[this.attributeCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i;
				}
				this.indeces = array;
				Array.Sort(this.indeces, 0, this.attributeCount, this);
				return this.IsSorted();
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x000092B0 File Offset: 0x000074B0
			private bool IsSorted()
			{
				for (int i = 0; i < this.indeces.Length - 1; i++)
				{
					if (this.Compare(this.indeces[i], this.indeces[i + 1]) >= 0)
					{
						this.attributeIndex1 = (int)this.indeces[i];
						this.attributeIndex2 = (int)this.indeces[i + 1];
						return false;
					}
				}
				return true;
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x00009318 File Offset: 0x00007518
			public int Compare(object obj1, object obj2)
			{
				int num = (int)obj1;
				int num2 = (int)obj2;
				XmlBaseReader.XmlAttributeNode xmlAttributeNode = this.attributeNodes[num];
				XmlBaseReader.XmlAttributeNode xmlAttributeNode2 = this.attributeNodes[num2];
				int num3 = this.CompareQNameType(xmlAttributeNode.QNameType, xmlAttributeNode2.QNameType);
				if (num3 == 0)
				{
					if (xmlAttributeNode.QNameType == XmlBaseReader.QNameType.Normal)
					{
						num3 = xmlAttributeNode.LocalName.CompareTo(xmlAttributeNode2.LocalName);
						if (num3 == 0)
						{
							num3 = xmlAttributeNode.Namespace.Uri.CompareTo(xmlAttributeNode2.Namespace.Uri);
						}
					}
					else
					{
						num3 = xmlAttributeNode.Namespace.Prefix.CompareTo(xmlAttributeNode2.Namespace.Prefix);
					}
				}
				return num3;
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x000093BA File Offset: 0x000075BA
			public int CompareQNameType(XmlBaseReader.QNameType type1, XmlBaseReader.QNameType type2)
			{
				return type1 - type2;
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x0000222F File Offset: 0x0000042F
			public AttributeSorter()
			{
			}

			// Token: 0x040000EB RID: 235
			private object[] indeces;

			// Token: 0x040000EC RID: 236
			private XmlBaseReader.XmlAttributeNode[] attributeNodes;

			// Token: 0x040000ED RID: 237
			private int attributeCount;

			// Token: 0x040000EE RID: 238
			private int attributeIndex1;

			// Token: 0x040000EF RID: 239
			private int attributeIndex2;
		}

		// Token: 0x0200003C RID: 60
		private class NamespaceManager
		{
			// Token: 0x060001F4 RID: 500 RVA: 0x000093C0 File Offset: 0x000075C0
			public NamespaceManager(XmlBufferReader bufferReader)
			{
				this.bufferReader = bufferReader;
				this.shortPrefixUri = new XmlBaseReader.Namespace[28];
				this.shortPrefixUri[0] = XmlBaseReader.NamespaceManager.emptyNamespace;
				this.namespaces = null;
				this.nsCount = 0;
				this.attributes = null;
				this.attributeCount = 0;
				this.space = XmlSpace.None;
				this.lang = string.Empty;
				this.depth = 0;
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x0000942C File Offset: 0x0000762C
			public void Close()
			{
				if (this.namespaces != null && this.namespaces.Length > 32)
				{
					this.namespaces = null;
				}
				if (this.attributes != null && this.attributes.Length > 4)
				{
					this.attributes = null;
				}
				this.lang = string.Empty;
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000947C File Offset: 0x0000767C
			public static XmlBaseReader.Namespace XmlNamespace
			{
				get
				{
					if (XmlBaseReader.NamespaceManager.xmlNamespace == null)
					{
						byte[] array = new byte[]
						{
							120,
							109,
							108,
							104,
							116,
							116,
							112,
							58,
							47,
							47,
							119,
							119,
							119,
							46,
							119,
							51,
							46,
							111,
							114,
							103,
							47,
							88,
							77,
							76,
							47,
							49,
							57,
							57,
							56,
							47,
							110,
							97,
							109,
							101,
							115,
							112,
							97,
							99,
							101
						};
						XmlBaseReader.Namespace @namespace = new XmlBaseReader.Namespace(new XmlBufferReader(array));
						@namespace.Prefix.SetValue(0, 3);
						@namespace.Uri.SetValue(3, array.Length - 3);
						XmlBaseReader.NamespaceManager.xmlNamespace = @namespace;
					}
					return XmlBaseReader.NamespaceManager.xmlNamespace;
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060001F7 RID: 503 RVA: 0x000094D6 File Offset: 0x000076D6
			public static XmlBaseReader.Namespace EmptyNamespace
			{
				get
				{
					return XmlBaseReader.NamespaceManager.emptyNamespace;
				}
			}

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060001F8 RID: 504 RVA: 0x000094DD File Offset: 0x000076DD
			public string XmlLang
			{
				get
				{
					return this.lang;
				}
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060001F9 RID: 505 RVA: 0x000094E5 File Offset: 0x000076E5
			public XmlSpace XmlSpace
			{
				get
				{
					return this.space;
				}
			}

			// Token: 0x060001FA RID: 506 RVA: 0x000094F0 File Offset: 0x000076F0
			public void Clear()
			{
				if (this.nsCount != 0)
				{
					if (this.shortPrefixUri != null)
					{
						for (int i = 0; i < this.shortPrefixUri.Length; i++)
						{
							this.shortPrefixUri[i] = null;
						}
					}
					this.shortPrefixUri[0] = XmlBaseReader.NamespaceManager.emptyNamespace;
					this.nsCount = 0;
				}
				this.attributeCount = 0;
				this.space = XmlSpace.None;
				this.lang = string.Empty;
				this.depth = 0;
			}

			// Token: 0x060001FB RID: 507 RVA: 0x0000955D File Offset: 0x0000775D
			public void EnterScope()
			{
				this.depth++;
			}

			// Token: 0x060001FC RID: 508 RVA: 0x00009570 File Offset: 0x00007770
			public void ExitScope()
			{
				while (this.nsCount > 0)
				{
					XmlBaseReader.Namespace @namespace = this.namespaces[this.nsCount - 1];
					if (@namespace.Depth != this.depth)
					{
						IL_9A:
						while (this.attributeCount > 0)
						{
							XmlBaseReader.NamespaceManager.XmlAttribute xmlAttribute = this.attributes[this.attributeCount - 1];
							if (xmlAttribute.Depth != this.depth)
							{
								break;
							}
							this.space = xmlAttribute.XmlSpace;
							this.lang = xmlAttribute.XmlLang;
							this.attributeCount--;
						}
						this.depth--;
						return;
					}
					PrefixHandleType prefixHandleType;
					if (@namespace.Prefix.TryGetShortPrefix(out prefixHandleType))
					{
						this.shortPrefixUri[(int)prefixHandleType] = @namespace.OuterUri;
					}
					this.nsCount--;
				}
				goto IL_9A;
			}

			// Token: 0x060001FD RID: 509 RVA: 0x00009630 File Offset: 0x00007830
			public void Sign(XmlSigningNodeWriter writer)
			{
				for (int i = 0; i < this.nsCount; i++)
				{
					PrefixHandle prefix = this.namespaces[i].Prefix;
					bool flag = false;
					for (int j = i + 1; j < this.nsCount; j++)
					{
						if (object.Equals(prefix, this.namespaces[j].Prefix))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						int prefixOffset;
						int prefixLength;
						byte[] @string = prefix.GetString(out prefixOffset, out prefixLength);
						int nsOffset;
						int nsLength;
						byte[] string2 = this.namespaces[i].Uri.GetString(out nsOffset, out nsLength);
						writer.WriteXmlnsAttribute(@string, prefixOffset, prefixLength, string2, nsOffset, nsLength);
					}
				}
			}

			// Token: 0x060001FE RID: 510 RVA: 0x000096C6 File Offset: 0x000078C6
			public void AddLangAttribute(string lang)
			{
				this.AddAttribute();
				this.lang = lang;
			}

			// Token: 0x060001FF RID: 511 RVA: 0x000096D5 File Offset: 0x000078D5
			public void AddSpaceAttribute(XmlSpace space)
			{
				this.AddAttribute();
				this.space = space;
			}

			// Token: 0x06000200 RID: 512 RVA: 0x000096E4 File Offset: 0x000078E4
			private void AddAttribute()
			{
				if (this.attributes == null)
				{
					this.attributes = new XmlBaseReader.NamespaceManager.XmlAttribute[1];
				}
				else if (this.attributes.Length == this.attributeCount)
				{
					XmlBaseReader.NamespaceManager.XmlAttribute[] destinationArray = new XmlBaseReader.NamespaceManager.XmlAttribute[this.attributeCount * 2];
					Array.Copy(this.attributes, destinationArray, this.attributeCount);
					this.attributes = destinationArray;
				}
				XmlBaseReader.NamespaceManager.XmlAttribute xmlAttribute = this.attributes[this.attributeCount];
				if (xmlAttribute == null)
				{
					xmlAttribute = new XmlBaseReader.NamespaceManager.XmlAttribute();
					this.attributes[this.attributeCount] = xmlAttribute;
				}
				xmlAttribute.XmlLang = this.lang;
				xmlAttribute.XmlSpace = this.space;
				xmlAttribute.Depth = this.depth;
				this.attributeCount++;
			}

			// Token: 0x06000201 RID: 513 RVA: 0x00009798 File Offset: 0x00007998
			public void Register(XmlBaseReader.Namespace nameSpace)
			{
				PrefixHandleType prefixHandleType;
				if (nameSpace.Prefix.TryGetShortPrefix(out prefixHandleType))
				{
					nameSpace.OuterUri = this.shortPrefixUri[(int)prefixHandleType];
					this.shortPrefixUri[(int)prefixHandleType] = nameSpace;
					return;
				}
				nameSpace.OuterUri = null;
			}

			// Token: 0x06000202 RID: 514 RVA: 0x000097D4 File Offset: 0x000079D4
			public XmlBaseReader.Namespace AddNamespace()
			{
				if (this.namespaces == null)
				{
					this.namespaces = new XmlBaseReader.Namespace[4];
				}
				else if (this.namespaces.Length == this.nsCount)
				{
					XmlBaseReader.Namespace[] destinationArray = new XmlBaseReader.Namespace[this.nsCount * 2];
					Array.Copy(this.namespaces, destinationArray, this.nsCount);
					this.namespaces = destinationArray;
				}
				XmlBaseReader.Namespace @namespace = this.namespaces[this.nsCount];
				if (@namespace == null)
				{
					@namespace = new XmlBaseReader.Namespace(this.bufferReader);
					this.namespaces[this.nsCount] = @namespace;
				}
				@namespace.Clear();
				@namespace.Depth = this.depth;
				this.nsCount++;
				return @namespace;
			}

			// Token: 0x06000203 RID: 515 RVA: 0x0000987A File Offset: 0x00007A7A
			public XmlBaseReader.Namespace LookupNamespace(PrefixHandleType prefix)
			{
				return this.shortPrefixUri[(int)prefix];
			}

			// Token: 0x06000204 RID: 516 RVA: 0x00009884 File Offset: 0x00007A84
			public XmlBaseReader.Namespace LookupNamespace(PrefixHandle prefix)
			{
				PrefixHandleType prefix2;
				if (prefix.TryGetShortPrefix(out prefix2))
				{
					return this.LookupNamespace(prefix2);
				}
				for (int i = this.nsCount - 1; i >= 0; i--)
				{
					XmlBaseReader.Namespace @namespace = this.namespaces[i];
					if (@namespace.Prefix == prefix)
					{
						return @namespace;
					}
				}
				if (prefix.IsXml)
				{
					return XmlBaseReader.NamespaceManager.XmlNamespace;
				}
				return null;
			}

			// Token: 0x06000205 RID: 517 RVA: 0x000098E0 File Offset: 0x00007AE0
			public XmlBaseReader.Namespace LookupNamespace(string prefix)
			{
				PrefixHandleType prefix2;
				if (this.TryGetShortPrefix(prefix, out prefix2))
				{
					return this.LookupNamespace(prefix2);
				}
				for (int i = this.nsCount - 1; i >= 0; i--)
				{
					XmlBaseReader.Namespace @namespace = this.namespaces[i];
					if (@namespace.Prefix == prefix)
					{
						return @namespace;
					}
				}
				if (prefix == "xml")
				{
					return XmlBaseReader.NamespaceManager.XmlNamespace;
				}
				return null;
			}

			// Token: 0x06000206 RID: 518 RVA: 0x00009940 File Offset: 0x00007B40
			private bool TryGetShortPrefix(string s, out PrefixHandleType shortPrefix)
			{
				int length = s.Length;
				if (length == 0)
				{
					shortPrefix = PrefixHandleType.Empty;
					return true;
				}
				if (length == 1)
				{
					char c = s[0];
					if (c >= 'a' && c <= 'z')
					{
						shortPrefix = PrefixHandle.GetAlphaPrefix((int)(c - 'a'));
						return true;
					}
				}
				shortPrefix = PrefixHandleType.Empty;
				return false;
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00009983 File Offset: 0x00007B83
			// Note: this type is marked as 'beforefieldinit'.
			static NamespaceManager()
			{
			}

			// Token: 0x040000F0 RID: 240
			private XmlBufferReader bufferReader;

			// Token: 0x040000F1 RID: 241
			private XmlBaseReader.Namespace[] namespaces;

			// Token: 0x040000F2 RID: 242
			private int nsCount;

			// Token: 0x040000F3 RID: 243
			private int depth;

			// Token: 0x040000F4 RID: 244
			private XmlBaseReader.Namespace[] shortPrefixUri;

			// Token: 0x040000F5 RID: 245
			private static XmlBaseReader.Namespace emptyNamespace = new XmlBaseReader.Namespace(XmlBufferReader.Empty);

			// Token: 0x040000F6 RID: 246
			private static XmlBaseReader.Namespace xmlNamespace;

			// Token: 0x040000F7 RID: 247
			private XmlBaseReader.NamespaceManager.XmlAttribute[] attributes;

			// Token: 0x040000F8 RID: 248
			private int attributeCount;

			// Token: 0x040000F9 RID: 249
			private XmlSpace space;

			// Token: 0x040000FA RID: 250
			private string lang;

			// Token: 0x0200003D RID: 61
			private class XmlAttribute
			{
				// Token: 0x06000208 RID: 520 RVA: 0x0000222F File Offset: 0x0000042F
				public XmlAttribute()
				{
				}

				// Token: 0x1700004C RID: 76
				// (get) Token: 0x06000209 RID: 521 RVA: 0x00009994 File Offset: 0x00007B94
				// (set) Token: 0x0600020A RID: 522 RVA: 0x0000999C File Offset: 0x00007B9C
				public int Depth
				{
					get
					{
						return this.depth;
					}
					set
					{
						this.depth = value;
					}
				}

				// Token: 0x1700004D RID: 77
				// (get) Token: 0x0600020B RID: 523 RVA: 0x000099A5 File Offset: 0x00007BA5
				// (set) Token: 0x0600020C RID: 524 RVA: 0x000099AD File Offset: 0x00007BAD
				public string XmlLang
				{
					get
					{
						return this.lang;
					}
					set
					{
						this.lang = value;
					}
				}

				// Token: 0x1700004E RID: 78
				// (get) Token: 0x0600020D RID: 525 RVA: 0x000099B6 File Offset: 0x00007BB6
				// (set) Token: 0x0600020E RID: 526 RVA: 0x000099BE File Offset: 0x00007BBE
				public XmlSpace XmlSpace
				{
					get
					{
						return this.space;
					}
					set
					{
						this.space = value;
					}
				}

				// Token: 0x040000FB RID: 251
				private XmlSpace space;

				// Token: 0x040000FC RID: 252
				private string lang;

				// Token: 0x040000FD RID: 253
				private int depth;
			}
		}

		// Token: 0x0200003E RID: 62
		protected class Namespace
		{
			// Token: 0x0600020F RID: 527 RVA: 0x000099C7 File Offset: 0x00007BC7
			public Namespace(XmlBufferReader bufferReader)
			{
				this.prefix = new PrefixHandle(bufferReader);
				this.uri = new StringHandle(bufferReader);
				this.outerUri = null;
				this.uriString = null;
			}

			// Token: 0x06000210 RID: 528 RVA: 0x000099F5 File Offset: 0x00007BF5
			public void Clear()
			{
				this.uriString = null;
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x06000211 RID: 529 RVA: 0x000099FE File Offset: 0x00007BFE
			// (set) Token: 0x06000212 RID: 530 RVA: 0x00009A06 File Offset: 0x00007C06
			public int Depth
			{
				get
				{
					return this.depth;
				}
				set
				{
					this.depth = value;
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x06000213 RID: 531 RVA: 0x00009A0F File Offset: 0x00007C0F
			public PrefixHandle Prefix
			{
				get
				{
					return this.prefix;
				}
			}

			// Token: 0x06000214 RID: 532 RVA: 0x00009A17 File Offset: 0x00007C17
			public bool IsUri(string s)
			{
				if (s == this.uriString)
				{
					return true;
				}
				if (this.uri == s)
				{
					this.uriString = s;
					return true;
				}
				return false;
			}

			// Token: 0x06000215 RID: 533 RVA: 0x00009A3C File Offset: 0x00007C3C
			public bool IsUri(XmlDictionaryString s)
			{
				if (s.Value == this.uriString)
				{
					return true;
				}
				if (this.uri == s)
				{
					this.uriString = s.Value;
					return true;
				}
				return false;
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x06000216 RID: 534 RVA: 0x00009A6B File Offset: 0x00007C6B
			public StringHandle Uri
			{
				get
				{
					return this.uri;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000217 RID: 535 RVA: 0x00009A73 File Offset: 0x00007C73
			// (set) Token: 0x06000218 RID: 536 RVA: 0x00009A7B File Offset: 0x00007C7B
			public XmlBaseReader.Namespace OuterUri
			{
				get
				{
					return this.outerUri;
				}
				set
				{
					this.outerUri = value;
				}
			}

			// Token: 0x040000FE RID: 254
			private PrefixHandle prefix;

			// Token: 0x040000FF RID: 255
			private StringHandle uri;

			// Token: 0x04000100 RID: 256
			private int depth;

			// Token: 0x04000101 RID: 257
			private XmlBaseReader.Namespace outerUri;

			// Token: 0x04000102 RID: 258
			private string uriString;
		}

		// Token: 0x0200003F RID: 63
		private class QuotaNameTable : XmlNameTable
		{
			// Token: 0x06000219 RID: 537 RVA: 0x00009A84 File Offset: 0x00007C84
			public QuotaNameTable(XmlDictionaryReader reader, int maxCharCount)
			{
				this.reader = reader;
				this.nameTable = new NameTable();
				this.maxCharCount = maxCharCount;
				this.charCount = 0;
			}

			// Token: 0x0600021A RID: 538 RVA: 0x00009AAC File Offset: 0x00007CAC
			public override string Get(char[] chars, int offset, int count)
			{
				return this.nameTable.Get(chars, offset, count);
			}

			// Token: 0x0600021B RID: 539 RVA: 0x00009ABC File Offset: 0x00007CBC
			public override string Get(string value)
			{
				return this.nameTable.Get(value);
			}

			// Token: 0x0600021C RID: 540 RVA: 0x00009ACA File Offset: 0x00007CCA
			private void Add(int charCount)
			{
				if (charCount > this.maxCharCount - this.charCount)
				{
					XmlExceptionHelper.ThrowMaxNameTableCharCountExceeded(this.reader, this.maxCharCount);
				}
				this.charCount += charCount;
			}

			// Token: 0x0600021D RID: 541 RVA: 0x00009AFC File Offset: 0x00007CFC
			public override string Add(char[] chars, int offset, int count)
			{
				string text = this.nameTable.Get(chars, offset, count);
				if (text != null)
				{
					return text;
				}
				this.Add(count);
				return this.nameTable.Add(chars, offset, count);
			}

			// Token: 0x0600021E RID: 542 RVA: 0x00009B34 File Offset: 0x00007D34
			public override string Add(string value)
			{
				string text = this.nameTable.Get(value);
				if (text != null)
				{
					return text;
				}
				this.Add(value.Length);
				return this.nameTable.Add(value);
			}

			// Token: 0x04000103 RID: 259
			private XmlDictionaryReader reader;

			// Token: 0x04000104 RID: 260
			private XmlNameTable nameTable;

			// Token: 0x04000105 RID: 261
			private int maxCharCount;

			// Token: 0x04000106 RID: 262
			private int charCount;
		}
	}
}
