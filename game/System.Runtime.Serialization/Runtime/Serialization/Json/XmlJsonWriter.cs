using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000190 RID: 400
	internal class XmlJsonWriter : XmlDictionaryWriter, IXmlJsonWriterInitializer
	{
		// Token: 0x0600140A RID: 5130 RVA: 0x0004E60A File Offset: 0x0004C80A
		public XmlJsonWriter() : this(false, null)
		{
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0004E614 File Offset: 0x0004C814
		public XmlJsonWriter(bool indent, string indentChars)
		{
			this.indent = indent;
			if (indent)
			{
				if (indentChars == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("indentChars");
				}
				this.indentChars = indentChars;
			}
			this.InitializeWriter();
			if (XmlJsonWriter.CharacterAbbrevs == null)
			{
				XmlJsonWriter.CharacterAbbrevs = XmlJsonWriter.GetCharacterAbbrevs();
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0004E654 File Offset: 0x0004C854
		private static char[] GetCharacterAbbrevs()
		{
			char[] array = new char[32];
			for (int i = 0; i < 32; i++)
			{
				char c;
				if (!LocalAppContextSwitches.DoNotUseEcmaScriptV6EscapeControlCharacter && XmlJsonWriter.TryEscapeControlCharacter((char)i, out c))
				{
					array[i] = c;
				}
				else
				{
					array[i] = '\0';
				}
			}
			return array;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0004E694 File Offset: 0x0004C894
		private static bool TryEscapeControlCharacter(char ch, out char abbrev)
		{
			switch (ch)
			{
			case '\b':
				abbrev = 'b';
				return true;
			case '\t':
				abbrev = 't';
				return true;
			case '\n':
				abbrev = 'n';
				return true;
			case '\f':
				abbrev = 'f';
				return true;
			case '\r':
				abbrev = 'r';
				return true;
			}
			abbrev = ' ';
			return false;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0001BF43 File Offset: 0x0001A143
		public override XmlWriterSettings Settings
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0004E6E8 File Offset: 0x0004C8E8
		public override WriteState WriteState
		{
			get
			{
				if (this.writeState == WriteState.Closed)
				{
					return WriteState.Closed;
				}
				if (this.HasOpenAttribute)
				{
					return WriteState.Attribute;
				}
				switch (this.nodeType)
				{
				case JsonNodeType.None:
					return WriteState.Start;
				case JsonNodeType.Element:
					return WriteState.Element;
				case JsonNodeType.EndElement:
				case JsonNodeType.QuotedText:
				case JsonNodeType.StandaloneText:
					return WriteState.Content;
				}
				return WriteState.Error;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x0001BF43 File Offset: 0x0001A143
		public override string XmlLang
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00003127 File Offset: 0x00001327
		public override XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.None;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0004E738 File Offset: 0x0004C938
		private static BinHexEncoding BinHexEncoding
		{
			[SecuritySafeCritical]
			get
			{
				if (XmlJsonWriter.binHexEncoding == null)
				{
					XmlJsonWriter.binHexEncoding = new BinHexEncoding();
				}
				return XmlJsonWriter.binHexEncoding;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0004E750 File Offset: 0x0004C950
		private bool HasOpenAttribute
		{
			get
			{
				return this.isWritingDataTypeAttribute || this.isWritingServerTypeAttribute || this.IsWritingNameAttribute || this.isWritingXmlnsAttribute;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0004E772 File Offset: 0x0004C972
		private bool IsClosed
		{
			get
			{
				return this.WriteState == WriteState.Closed;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0004E77D File Offset: 0x0004C97D
		private bool IsWritingCollection
		{
			get
			{
				return this.depth > 0 && this.scopes[this.depth] == JsonNodeType.Collection;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0004E79A File Offset: 0x0004C99A
		private bool IsWritingNameAttribute
		{
			get
			{
				return (this.nameState & XmlJsonWriter.NameState.IsWritingNameAttribute) == XmlJsonWriter.NameState.IsWritingNameAttribute;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0004E7A7 File Offset: 0x0004C9A7
		private bool IsWritingNameWithMapping
		{
			get
			{
				return (this.nameState & XmlJsonWriter.NameState.IsWritingNameWithMapping) == XmlJsonWriter.NameState.IsWritingNameWithMapping;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0004E7B4 File Offset: 0x0004C9B4
		private bool WrittenNameWithMapping
		{
			get
			{
				return (this.nameState & XmlJsonWriter.NameState.WrittenNameWithMapping) == XmlJsonWriter.NameState.WrittenNameWithMapping;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0004E7C4 File Offset: 0x0004C9C4
		public override void Close()
		{
			if (!this.IsClosed)
			{
				try
				{
					this.WriteEndDocument();
				}
				finally
				{
					try
					{
						this.nodeWriter.Flush();
						this.nodeWriter.Close();
					}
					finally
					{
						this.writeState = WriteState.Closed;
						if (this.depth != 0)
						{
							this.depth = 0;
						}
					}
				}
			}
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0004E830 File Offset: 0x0004CA30
		public override void Flush()
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			this.nodeWriter.Flush();
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0004E84C File Offset: 0x0004CA4C
		public override string LookupPrefix(string ns)
		{
			if (ns == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ns");
			}
			if (ns == "http://www.w3.org/2000/xmlns/")
			{
				return "xmlns";
			}
			if (ns == "http://www.w3.org/XML/1998/namespace")
			{
				return "xml";
			}
			if (ns == string.Empty)
			{
				return string.Empty;
			}
			return null;
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0004E8A4 File Offset: 0x0004CAA4
		public void SetOutput(Stream stream, Encoding encoding, bool ownsStream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (encoding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoding");
			}
			if (encoding.WebName != Encoding.UTF8.WebName)
			{
				stream = new JsonEncodingStreamWrapper(stream, encoding, false);
			}
			else
			{
				encoding = null;
			}
			if (this.nodeWriter == null)
			{
				this.nodeWriter = new XmlJsonWriter.JsonNodeWriter();
			}
			this.nodeWriter.SetOutput(stream, ownsStream, encoding);
			this.InitializeWriter();
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, bool[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, short[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, int[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, long[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, float[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, double[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, decimal[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, DateTime[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, Guid[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, string localName, string namespaceUri, TimeSpan[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, bool[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, decimal[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, double[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, float[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, int[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, long[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, short[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, DateTime[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, Guid[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0004E91A File Offset: 0x0004CB1A
		public override void WriteArray(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri, TimeSpan[] array, int offset, int count)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("JSON WriteArray is not supported.")));
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0004E930 File Offset: 0x0004CB30
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			if (index < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("JSON size exceeded remaining buffer space, by {0} byte(s).", new object[]
				{
					buffer.Length - index
				})));
			}
			this.StartText();
			this.nodeWriter.WriteBase64Text(buffer, 0, buffer, index, count);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0004E9D4 File Offset: 0x0004CBD4
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			if (index < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("JSON size exceeded remaining buffer space, by {0} byte(s).", new object[]
				{
					buffer.Length - index
				})));
			}
			this.StartText();
			this.WriteEscapedJsonString(XmlJsonWriter.BinHexEncoding.GetString(buffer, index, count));
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0004EA79 File Offset: 0x0004CC79
		public override void WriteCData(string text)
		{
			this.WriteString(text);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0004EA82 File Offset: 0x0004CC82
		public override void WriteCharEntity(char ch)
		{
			this.WriteString(ch.ToString());
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0004EA94 File Offset: 0x0004CC94
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			if (index < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("JSON size exceeded remaining buffer space, by {0} byte(s).", new object[]
				{
					buffer.Length - index
				})));
			}
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0004EB2E File Offset: 0x0004CD2E
		public override void WriteComment(string text)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Method {0} is not supported in JSON.", new object[]
			{
				"WriteComment"
			})));
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0004EB52 File Offset: 0x0004CD52
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Method {0} is not supported in JSON.", new object[]
			{
				"WriteDocType"
			})));
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0004EB78 File Offset: 0x0004CD78
		public override void WriteEndAttribute()
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (!this.HasOpenAttribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("WriteEndAttribute was called while there is no open attribute.")));
			}
			if (this.isWritingDataTypeAttribute)
			{
				string a = this.attributeText;
				if (!(a == "number"))
				{
					if (!(a == "string"))
					{
						if (!(a == "array"))
						{
							if (!(a == "object"))
							{
								if (!(a == "null"))
								{
									if (!(a == "boolean"))
									{
										throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Unexpected attribute value '{0}'.", new object[]
										{
											this.attributeText
										})));
									}
									this.ThrowIfServerTypeWritten("boolean");
									this.dataType = XmlJsonWriter.JsonDataType.Boolean;
								}
								else
								{
									this.ThrowIfServerTypeWritten("null");
									this.dataType = XmlJsonWriter.JsonDataType.Null;
								}
							}
							else
							{
								this.dataType = XmlJsonWriter.JsonDataType.Object;
							}
						}
						else
						{
							this.ThrowIfServerTypeWritten("array");
							this.dataType = XmlJsonWriter.JsonDataType.Array;
						}
					}
					else
					{
						this.ThrowIfServerTypeWritten("string");
						this.dataType = XmlJsonWriter.JsonDataType.String;
					}
				}
				else
				{
					this.ThrowIfServerTypeWritten("number");
					this.dataType = XmlJsonWriter.JsonDataType.Number;
				}
				this.attributeText = null;
				this.isWritingDataTypeAttribute = false;
				if (!this.IsWritingNameWithMapping || this.WrittenNameWithMapping)
				{
					this.WriteDataTypeServerType();
					return;
				}
			}
			else if (this.isWritingServerTypeAttribute)
			{
				this.serverTypeValue = this.attributeText;
				this.attributeText = null;
				this.isWritingServerTypeAttribute = false;
				if ((!this.IsWritingNameWithMapping || this.WrittenNameWithMapping) && this.dataType == XmlJsonWriter.JsonDataType.Object)
				{
					this.WriteServerTypeAttribute();
					return;
				}
			}
			else
			{
				if (this.IsWritingNameAttribute)
				{
					this.WriteJsonElementName(this.attributeText);
					this.attributeText = null;
					this.nameState = (XmlJsonWriter.NameState.IsWritingNameWithMapping | XmlJsonWriter.NameState.WrittenNameWithMapping);
					this.WriteDataTypeServerType();
					return;
				}
				if (this.isWritingXmlnsAttribute)
				{
					if (!string.IsNullOrEmpty(this.attributeText) && this.isWritingXmlnsAttributeDefaultNs)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("ns", SR.GetString("JSON namespace is specified as '{0}' but it must be empty.", new object[]
						{
							this.attributeText
						}));
					}
					this.attributeText = null;
					this.isWritingXmlnsAttribute = false;
					this.isWritingXmlnsAttributeDefaultNs = false;
				}
			}
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0004ED91 File Offset: 0x0004CF91
		public override void WriteEndDocument()
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (this.nodeType != JsonNodeType.None)
			{
				while (this.depth > 0)
				{
					this.WriteEndElement();
				}
			}
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0004EDBC File Offset: 0x0004CFBC
		public override void WriteEndElement()
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (this.depth == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Encountered an end element while there was no open element in JSON writer.")));
			}
			if (this.HasOpenAttribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON attribute must be closed first before calling {0} method.", new object[]
				{
					"WriteEndElement"
				})));
			}
			this.endElementBuffer = false;
			JsonNodeType jsonNodeType = this.ExitScope();
			if (jsonNodeType == JsonNodeType.Collection)
			{
				this.indentLevel--;
				if (this.indent)
				{
					if (this.nodeType == JsonNodeType.Element)
					{
						this.nodeWriter.WriteText(32);
					}
					else
					{
						this.WriteNewLine();
						this.WriteIndent();
					}
				}
				this.nodeWriter.WriteText(93);
				jsonNodeType = this.ExitScope();
			}
			else if (this.nodeType == JsonNodeType.QuotedText)
			{
				this.WriteJsonQuote();
			}
			else if (this.nodeType == JsonNodeType.Element)
			{
				if (this.dataType == XmlJsonWriter.JsonDataType.None && this.serverTypeValue != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("On JSON writer data type '{0}' must be specified. Object string is '{1}', server type string is '{2}'.", new object[]
					{
						"type",
						"object",
						"__type"
					})));
				}
				if (this.IsWritingNameWithMapping && !this.WrittenNameWithMapping)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("On JSON writer data type '{0}' must be specified. Object string is '{1}', server type string is '{2}'.", new object[]
					{
						"item",
						string.Empty,
						"item"
					})));
				}
				if (this.dataType == XmlJsonWriter.JsonDataType.None || this.dataType == XmlJsonWriter.JsonDataType.String)
				{
					this.nodeWriter.WriteText(34);
					this.nodeWriter.WriteText(34);
				}
			}
			if (this.depth != 0)
			{
				if (jsonNodeType == JsonNodeType.Element)
				{
					this.endElementBuffer = true;
				}
				else if (jsonNodeType == JsonNodeType.Object)
				{
					this.indentLevel--;
					if (this.indent)
					{
						if (this.nodeType == JsonNodeType.Element)
						{
							this.nodeWriter.WriteText(32);
						}
						else
						{
							this.WriteNewLine();
							this.WriteIndent();
						}
					}
					this.nodeWriter.WriteText(125);
					if (this.depth > 0 && this.scopes[this.depth] == JsonNodeType.Element)
					{
						this.ExitScope();
						this.endElementBuffer = true;
					}
				}
			}
			this.dataType = XmlJsonWriter.JsonDataType.None;
			this.nodeType = JsonNodeType.EndElement;
			this.nameState = XmlJsonWriter.NameState.None;
			this.wroteServerTypeAttribute = false;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0004EFF7 File Offset: 0x0004D1F7
		public override void WriteEntityRef(string name)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Method {0} is not supported in JSON.", new object[]
			{
				"WriteEntityRef"
			})));
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0004F01B File Offset: 0x0004D21B
		public override void WriteFullEndElement()
		{
			this.WriteEndElement();
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0004F024 File Offset: 0x0004D224
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (!name.Equals("xml", StringComparison.OrdinalIgnoreCase))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("processing instruction is not supported in JSON writer."), "name"));
			}
			if (this.WriteState != WriteState.Start)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Attempt to write invalid XML declration.")));
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0004F083 File Offset: 0x0004D283
		public override void WriteQualifiedName(string localName, string ns)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (localName.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("localName", SR.GetString("Empty string is invalid as a local name."));
			}
			if (ns == null)
			{
				ns = string.Empty;
			}
			base.WriteQualifiedName(localName, ns);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0004EA79 File Offset: 0x0004CC79
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0004F0C4 File Offset: 0x0004D2C4
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
			}
			if (index < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", SR.GetString("JSON size exceeded remaining buffer space, by {0} byte(s).", new object[]
				{
					buffer.Length - index
				})));
			}
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0004F160 File Offset: 0x0004D360
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (!string.IsNullOrEmpty(prefix))
			{
				if (!this.IsWritingNameWithMapping || !(prefix == "xmlns"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("prefix", SR.GetString("JSON prefix must be null or empty. '{0}' is specified instead.", new object[]
					{
						prefix
					}));
				}
				if (ns != null && ns != "http://www.w3.org/2000/xmlns/")
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("The prefix '{0}' is bound to the namespace '{1}' and cannot be changed to '{2}'.", new object[]
					{
						"xmlns",
						"http://www.w3.org/2000/xmlns/",
						ns
					}), "ns"));
				}
			}
			else if (this.IsWritingNameWithMapping && ns == "http://www.w3.org/2000/xmlns/" && localName != "xmlns")
			{
				prefix = "xmlns";
			}
			if (!string.IsNullOrEmpty(ns))
			{
				if (this.IsWritingNameWithMapping && ns == "http://www.w3.org/2000/xmlns/")
				{
					prefix = "xmlns";
				}
				else
				{
					if (!string.IsNullOrEmpty(prefix) || !(localName == "xmlns") || !(ns == "http://www.w3.org/2000/xmlns/"))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("ns", SR.GetString("JSON namespace is specified as '{0}' but it must be empty.", new object[]
						{
							ns
						}));
					}
					prefix = "xmlns";
					this.isWritingXmlnsAttributeDefaultNs = true;
				}
			}
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (localName.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("localName", SR.GetString("Empty string is invalid as a local name."));
			}
			if (this.nodeType != JsonNodeType.Element && !this.wroteServerTypeAttribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON attribute must have an owner element.")));
			}
			if (this.HasOpenAttribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON attribute must be closed first before calling {0} method.", new object[]
				{
					"WriteStartAttribute"
				})));
			}
			if (prefix == "xmlns")
			{
				this.isWritingXmlnsAttribute = true;
				return;
			}
			if (localName == "type")
			{
				if (this.dataType != XmlJsonWriter.JsonDataType.None)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON attribute '{0}' is already written.", new object[]
					{
						"type"
					})));
				}
				this.isWritingDataTypeAttribute = true;
				return;
			}
			else if (localName == "__type")
			{
				if (this.serverTypeValue != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON attribute '{0}' is already written.", new object[]
					{
						"__type"
					})));
				}
				if (this.dataType != XmlJsonWriter.JsonDataType.None && this.dataType != XmlJsonWriter.JsonDataType.Object)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Server type is specified for invalid data type in JSON. Server type: '{0}', type: '{1}', dataType: '{2}', object: '{3}'.", new object[]
					{
						"__type",
						"type",
						this.dataType.ToString().ToLowerInvariant(),
						"object"
					})));
				}
				this.isWritingServerTypeAttribute = true;
				return;
			}
			else
			{
				if (!(localName == "item"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("localName", SR.GetString("Unexpected attribute local name '{0}'.", new object[]
					{
						localName
					}));
				}
				if (this.WrittenNameWithMapping)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON attribute '{0}' is already written.", new object[]
					{
						"item"
					})));
				}
				if (!this.IsWritingNameWithMapping)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Encountered an end element while there was no open element in JSON writer.")));
				}
				this.nameState |= XmlJsonWriter.NameState.IsWritingNameAttribute;
				return;
			}
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0004F490 File Offset: 0x0004D690
		public override void WriteStartDocument(bool standalone)
		{
			this.WriteStartDocument();
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0004F498 File Offset: 0x0004D698
		public override void WriteStartDocument()
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (this.WriteState != WriteState.Start)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid write state {1} for '{0}' method.", new object[]
				{
					"WriteStartDocument",
					this.WriteState.ToString()
				})));
			}
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004F4F4 File Offset: 0x0004D6F4
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			if (localName.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("localName", SR.GetString("Empty string is invalid as a local name."));
			}
			if (!string.IsNullOrEmpty(prefix) && (string.IsNullOrEmpty(ns) || !this.TrySetWritingNameWithMapping(localName, ns)))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("prefix", SR.GetString("JSON prefix must be null or empty. '{0}' is specified instead.", new object[]
				{
					prefix
				}));
			}
			if (!string.IsNullOrEmpty(ns) && !this.TrySetWritingNameWithMapping(localName, ns))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("ns", SR.GetString("JSON namespace is specified as '{0}' but it must be empty.", new object[]
				{
					ns
				}));
			}
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (this.HasOpenAttribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON attribute must be closed first before calling {0} method.", new object[]
				{
					"WriteStartElement"
				})));
			}
			if (this.nodeType != JsonNodeType.None && this.depth == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Multiple root element is not allowed on JSON writer.")));
			}
			switch (this.nodeType)
			{
			case JsonNodeType.None:
				if (!localName.Equals("root"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid root element name '{0}' (root element is '{1}' in JSON).", new object[]
					{
						localName,
						"root"
					})));
				}
				this.EnterScope(JsonNodeType.Element);
				goto IL_27E;
			case JsonNodeType.Element:
				if (this.dataType != XmlJsonWriter.JsonDataType.Array && this.dataType != XmlJsonWriter.JsonDataType.Object)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Either Object or Array of JSON node type must be specified.")));
				}
				if (this.indent)
				{
					this.WriteNewLine();
					this.WriteIndent();
				}
				if (!this.IsWritingCollection)
				{
					if (this.nameState != XmlJsonWriter.NameState.IsWritingNameWithMapping)
					{
						this.WriteJsonElementName(localName);
					}
				}
				else if (!localName.Equals("item"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid JSON item name '{0}' for array element (item element is '{1}' in JSON).", new object[]
					{
						localName,
						"item"
					})));
				}
				this.EnterScope(JsonNodeType.Element);
				goto IL_27E;
			case JsonNodeType.EndElement:
				if (this.endElementBuffer)
				{
					this.nodeWriter.WriteText(44);
				}
				if (this.indent)
				{
					this.WriteNewLine();
					this.WriteIndent();
				}
				if (!this.IsWritingCollection)
				{
					if (this.nameState != XmlJsonWriter.NameState.IsWritingNameWithMapping)
					{
						this.WriteJsonElementName(localName);
					}
				}
				else if (!localName.Equals("item"))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid JSON item name '{0}' for array element (item element is '{1}' in JSON).", new object[]
					{
						localName,
						"item"
					})));
				}
				this.EnterScope(JsonNodeType.Element);
				goto IL_27E;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid call to JSON WriteStartElement method.")));
			IL_27E:
			this.isWritingDataTypeAttribute = false;
			this.isWritingServerTypeAttribute = false;
			this.isWritingXmlnsAttribute = false;
			this.wroteServerTypeAttribute = false;
			this.serverTypeValue = null;
			this.dataType = XmlJsonWriter.JsonDataType.None;
			this.nodeType = JsonNodeType.Element;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004F7B0 File Offset: 0x0004D9B0
		public override void WriteString(string text)
		{
			if (this.HasOpenAttribute && text != null)
			{
				this.attributeText += text;
				return;
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if ((this.dataType != XmlJsonWriter.JsonDataType.Array && this.dataType != XmlJsonWriter.JsonDataType.Object && this.nodeType != JsonNodeType.EndElement) || !XmlConverter.IsWhitespace(text))
			{
				this.StartText();
				this.WriteEscapedJsonString(text);
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0004F815 File Offset: 0x0004DA15
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.WriteString(highChar + lowChar);
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0004F82E File Offset: 0x0004DA2E
		public override void WriteValue(bool value)
		{
			this.StartText();
			this.nodeWriter.WriteBoolText(value);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0004F842 File Offset: 0x0004DA42
		public override void WriteValue(decimal value)
		{
			this.StartText();
			this.nodeWriter.WriteDecimalText(value);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0004F856 File Offset: 0x0004DA56
		public override void WriteValue(double value)
		{
			this.StartText();
			this.nodeWriter.WriteDoubleText(value);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0004F86A File Offset: 0x0004DA6A
		public override void WriteValue(float value)
		{
			this.StartText();
			this.nodeWriter.WriteFloatText(value);
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0004F87E File Offset: 0x0004DA7E
		public override void WriteValue(int value)
		{
			this.StartText();
			this.nodeWriter.WriteInt32Text(value);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0004F892 File Offset: 0x0004DA92
		public override void WriteValue(long value)
		{
			this.StartText();
			this.nodeWriter.WriteInt64Text(value);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0004F8A6 File Offset: 0x0004DAA6
		public override void WriteValue(Guid value)
		{
			this.StartText();
			this.nodeWriter.WriteGuidText(value);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0004F8BA File Offset: 0x0004DABA
		public override void WriteValue(DateTime value)
		{
			this.StartText();
			this.nodeWriter.WriteDateTimeText(value);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0004EA79 File Offset: 0x0004CC79
		public override void WriteValue(string value)
		{
			this.WriteString(value);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0004F8CE File Offset: 0x0004DACE
		public override void WriteValue(TimeSpan value)
		{
			this.StartText();
			this.nodeWriter.WriteTimeSpanText(value);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004F8E2 File Offset: 0x0004DAE2
		public override void WriteValue(UniqueId value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			this.StartText();
			this.nodeWriter.WriteUniqueIdText(value);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0004F90C File Offset: 0x0004DB0C
		public override void WriteValue(object value)
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (value is Array)
			{
				this.WriteValue((Array)value);
				return;
			}
			if (value is IStreamProvider)
			{
				this.WriteValue((IStreamProvider)value);
				return;
			}
			this.WritePrimitiveValue(value);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004F968 File Offset: 0x0004DB68
		public override void WriteWhitespace(string ws)
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (ws == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("ws");
			}
			foreach (char c in ws)
			{
				if (c != ' ' && c != '\t' && c != '\n' && c != '\r')
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("ws", SR.GetString("Only whitespace characters are allowed for {1} method. The specified value is '{0}'", new object[]
					{
						c.ToString(),
						"WriteWhitespace"
					}));
				}
			}
			this.WriteString(ws);
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0004F9F1 File Offset: 0x0004DBF1
		public override void WriteXmlAttribute(string localName, string value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Method {0} is not supported in JSON.", new object[]
			{
				"WriteXmlAttribute"
			})));
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0004F9F1 File Offset: 0x0004DBF1
		public override void WriteXmlAttribute(XmlDictionaryString localName, XmlDictionaryString value)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Method {0} is not supported in JSON.", new object[]
			{
				"WriteXmlAttribute"
			})));
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0004FA15 File Offset: 0x0004DC15
		public override void WriteXmlnsAttribute(string prefix, string namespaceUri)
		{
			if (!this.IsWritingNameWithMapping)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Method {0} is not supported in JSON.", new object[]
				{
					"WriteXmlnsAttribute"
				})));
			}
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0004FA15 File Offset: 0x0004DC15
		public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString namespaceUri)
		{
			if (!this.IsWritingNameWithMapping)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.GetString("Method {0} is not supported in JSON.", new object[]
				{
					"WriteXmlnsAttribute"
				})));
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0004FA42 File Offset: 0x0004DC42
		internal static bool CharacterNeedsEscaping(char ch)
		{
			return ch == '/' || ch == '"' || ch < ' ' || ch == '\\' || (ch >= '\ud800' && (ch <= '\udfff' || ch >= '￾'));
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0004FA79 File Offset: 0x0004DC79
		private static void ThrowClosed()
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("JSON writer is already closed.")));
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0004FA90 File Offset: 0x0004DC90
		private void CheckText(JsonNodeType nextNodeType)
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (this.depth == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("Text cannot be written outside the root element.")));
			}
			if (nextNodeType == JsonNodeType.StandaloneText && this.nodeType == JsonNodeType.QuotedText)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON writer cannot write standalone text after quoted text.")));
			}
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0004FAEC File Offset: 0x0004DCEC
		private void EnterScope(JsonNodeType currentNodeType)
		{
			this.depth++;
			if (this.scopes == null)
			{
				this.scopes = new JsonNodeType[4];
			}
			else if (this.scopes.Length == this.depth)
			{
				JsonNodeType[] destinationArray = new JsonNodeType[this.depth * 2];
				Array.Copy(this.scopes, destinationArray, this.depth);
				this.scopes = destinationArray;
			}
			this.scopes[this.depth] = currentNodeType;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0004FB62 File Offset: 0x0004DD62
		private JsonNodeType ExitScope()
		{
			JsonNodeType result = this.scopes[this.depth];
			this.scopes[this.depth] = JsonNodeType.None;
			this.depth--;
			return result;
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0004FB90 File Offset: 0x0004DD90
		private void InitializeWriter()
		{
			this.nodeType = JsonNodeType.None;
			this.dataType = XmlJsonWriter.JsonDataType.None;
			this.isWritingDataTypeAttribute = false;
			this.wroteServerTypeAttribute = false;
			this.isWritingServerTypeAttribute = false;
			this.serverTypeValue = null;
			this.attributeText = null;
			if (this.depth != 0)
			{
				this.depth = 0;
			}
			if (this.scopes != null && this.scopes.Length > 25)
			{
				this.scopes = null;
			}
			this.writeState = WriteState.Start;
			this.endElementBuffer = false;
			this.indentLevel = 0;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0004FC0D File Offset: 0x0004DE0D
		private static bool IsUnicodeNewlineCharacter(char c)
		{
			return c == '\u0085' || c == '\u2028' || c == '\u2029';
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		private void StartText()
		{
			if (this.HasOpenAttribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("On JSON writer WriteString must be used for writing attribute values.")));
			}
			if (this.dataType == XmlJsonWriter.JsonDataType.None && this.serverTypeValue != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("On JSON writer data type '{0}' must be specified. Object string is '{1}', server type string is '{2}'.", new object[]
				{
					"type",
					"object",
					"__type"
				})));
			}
			if (this.IsWritingNameWithMapping && !this.WrittenNameWithMapping)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("On JSON writer data type '{0}' must be specified. Object string is '{1}', server type string is '{2}'.", new object[]
				{
					"item",
					string.Empty,
					"item"
				})));
			}
			if (this.dataType == XmlJsonWriter.JsonDataType.String || this.dataType == XmlJsonWriter.JsonDataType.None)
			{
				this.CheckText(JsonNodeType.QuotedText);
				if (this.nodeType != JsonNodeType.QuotedText)
				{
					this.WriteJsonQuote();
				}
				this.nodeType = JsonNodeType.QuotedText;
				return;
			}
			if (this.dataType == XmlJsonWriter.JsonDataType.Number || this.dataType == XmlJsonWriter.JsonDataType.Boolean)
			{
				this.CheckText(JsonNodeType.StandaloneText);
				this.nodeType = JsonNodeType.StandaloneText;
				return;
			}
			this.ThrowInvalidAttributeContent();
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0004FD34 File Offset: 0x0004DF34
		private void ThrowIfServerTypeWritten(string dataTypeSpecified)
		{
			if (this.serverTypeValue != null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("The specified data type is invalid for server type. Type: '{0}', specified data type: '{1}', server type: '{2}', object '{3}'.", new object[]
				{
					"type",
					dataTypeSpecified,
					"__type",
					"object"
				})));
			}
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0004FD80 File Offset: 0x0004DF80
		private void ThrowInvalidAttributeContent()
		{
			if (this.HasOpenAttribute)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("Invalid method call state between start and end attribute.")));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("JSON writer cannot write text after non-text attribute. Data type is '{0}'.", new object[]
			{
				this.dataType.ToString().ToLowerInvariant()
			})));
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0004FDDD File Offset: 0x0004DFDD
		private bool TrySetWritingNameWithMapping(string localName, string ns)
		{
			if (localName.Equals("item") && ns.Equals("item"))
			{
				this.nameState = XmlJsonWriter.NameState.IsWritingNameWithMapping;
				return true;
			}
			return false;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0004FE04 File Offset: 0x0004E004
		private void WriteDataTypeServerType()
		{
			if (this.dataType != XmlJsonWriter.JsonDataType.None)
			{
				XmlJsonWriter.JsonDataType jsonDataType = this.dataType;
				if (jsonDataType != XmlJsonWriter.JsonDataType.Null)
				{
					if (jsonDataType != XmlJsonWriter.JsonDataType.Object)
					{
						if (jsonDataType == XmlJsonWriter.JsonDataType.Array)
						{
							this.EnterScope(JsonNodeType.Collection);
							this.nodeWriter.WriteText(91);
							this.indentLevel++;
						}
					}
					else
					{
						this.EnterScope(JsonNodeType.Object);
						this.nodeWriter.WriteText(123);
						this.indentLevel++;
					}
				}
				else
				{
					this.nodeWriter.WriteText("null");
				}
				if (this.serverTypeValue != null)
				{
					this.WriteServerTypeAttribute();
				}
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0004FE94 File Offset: 0x0004E094
		[SecuritySafeCritical]
		private unsafe void WriteEscapedJsonString(string str)
		{
			fixed (string text = str)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				int num = 0;
				int i;
				for (i = 0; i < str.Length; i++)
				{
					char c = ptr[i];
					if (c <= '/')
					{
						if (c == '/' || c == '"')
						{
							this.nodeWriter.WriteChars(ptr + num, i - num);
							this.nodeWriter.WriteText(92);
							this.nodeWriter.WriteText((int)c);
							num = i + 1;
						}
						else if (c < ' ')
						{
							this.nodeWriter.WriteChars(ptr + num, i - num);
							this.nodeWriter.WriteText(92);
							if (XmlJsonWriter.CharacterAbbrevs[(int)c] == '\0')
							{
								this.nodeWriter.WriteText(117);
								this.nodeWriter.WriteText(string.Format(CultureInfo.InvariantCulture, "{0:x4}", (int)c));
								num = i + 1;
							}
							else
							{
								this.nodeWriter.WriteText((int)XmlJsonWriter.CharacterAbbrevs[(int)c]);
								num = i + 1;
							}
						}
					}
					else if (c == '\\')
					{
						this.nodeWriter.WriteChars(ptr + num, i - num);
						this.nodeWriter.WriteText(92);
						this.nodeWriter.WriteText((int)c);
						num = i + 1;
					}
					else if ((c >= '\ud800' && (c <= '\udfff' || c >= '￾')) || XmlJsonWriter.IsUnicodeNewlineCharacter(c))
					{
						this.nodeWriter.WriteChars(ptr + num, i - num);
						this.nodeWriter.WriteText(92);
						this.nodeWriter.WriteText(117);
						this.nodeWriter.WriteText(string.Format(CultureInfo.InvariantCulture, "{0:x4}", (int)c));
						num = i + 1;
					}
				}
				if (num < i)
				{
					this.nodeWriter.WriteChars(ptr + num, i - num);
				}
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00050074 File Offset: 0x0004E274
		private void WriteIndent()
		{
			for (int i = 0; i < this.indentLevel; i++)
			{
				this.nodeWriter.WriteText(this.indentChars);
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x000500A3 File Offset: 0x0004E2A3
		private void WriteNewLine()
		{
			this.nodeWriter.WriteText(13);
			this.nodeWriter.WriteText(10);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x000500BF File Offset: 0x0004E2BF
		private void WriteJsonElementName(string localName)
		{
			this.WriteJsonQuote();
			this.WriteEscapedJsonString(localName);
			this.WriteJsonQuote();
			this.nodeWriter.WriteText(58);
			if (this.indent)
			{
				this.nodeWriter.WriteText(32);
			}
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x000500F6 File Offset: 0x0004E2F6
		private void WriteJsonQuote()
		{
			this.nodeWriter.WriteText(34);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00050108 File Offset: 0x0004E308
		private void WritePrimitiveValue(object value)
		{
			if (this.IsClosed)
			{
				XmlJsonWriter.ThrowClosed();
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
			}
			if (value is ulong)
			{
				this.WriteValue((ulong)value);
				return;
			}
			if (value is string)
			{
				this.WriteValue((string)value);
				return;
			}
			if (value is int)
			{
				this.WriteValue((int)value);
				return;
			}
			if (value is long)
			{
				this.WriteValue((long)value);
				return;
			}
			if (value is bool)
			{
				this.WriteValue((bool)value);
				return;
			}
			if (value is double)
			{
				this.WriteValue((double)value);
				return;
			}
			if (value is DateTime)
			{
				this.WriteValue((DateTime)value);
				return;
			}
			if (value is float)
			{
				this.WriteValue((float)value);
				return;
			}
			if (value is decimal)
			{
				this.WriteValue((decimal)value);
				return;
			}
			if (value is XmlDictionaryString)
			{
				this.WriteValue((XmlDictionaryString)value);
				return;
			}
			if (value is UniqueId)
			{
				this.WriteValue((UniqueId)value);
				return;
			}
			if (value is Guid)
			{
				this.WriteValue((Guid)value);
				return;
			}
			if (value is TimeSpan)
			{
				this.WriteValue((TimeSpan)value);
				return;
			}
			if (value.GetType().IsArray)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("Nested array is not supported in JSON: '{0}'"), "value"));
			}
			base.WriteValue(value);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00050274 File Offset: 0x0004E474
		private void WriteServerTypeAttribute()
		{
			string value = this.serverTypeValue;
			XmlJsonWriter.JsonDataType jsonDataType = this.dataType;
			XmlJsonWriter.NameState nameState = this.nameState;
			base.WriteStartElement("__type");
			this.WriteValue(value);
			this.WriteEndElement();
			this.dataType = jsonDataType;
			this.nameState = nameState;
			this.wroteServerTypeAttribute = true;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x000502C3 File Offset: 0x0004E4C3
		private void WriteValue(ulong value)
		{
			this.StartText();
			this.nodeWriter.WriteUInt64Text(value);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000502D8 File Offset: 0x0004E4D8
		private void WriteValue(Array array)
		{
			XmlJsonWriter.JsonDataType jsonDataType = this.dataType;
			this.dataType = XmlJsonWriter.JsonDataType.String;
			this.StartText();
			for (int i = 0; i < array.Length; i++)
			{
				if (i != 0)
				{
					this.nodeWriter.WriteText(32);
				}
				this.WritePrimitiveValue(array.GetValue(i));
			}
			this.dataType = jsonDataType;
		}

		// Token: 0x04000A0F RID: 2575
		private const char BACK_SLASH = '\\';

		// Token: 0x04000A10 RID: 2576
		private const char FORWARD_SLASH = '/';

		// Token: 0x04000A11 RID: 2577
		private const char HIGH_SURROGATE_START = '\ud800';

		// Token: 0x04000A12 RID: 2578
		private const char LOW_SURROGATE_END = '\udfff';

		// Token: 0x04000A13 RID: 2579
		private const char MAX_CHAR = '￾';

		// Token: 0x04000A14 RID: 2580
		private const char WHITESPACE = ' ';

		// Token: 0x04000A15 RID: 2581
		private const char CARRIAGE_RETURN = '\r';

		// Token: 0x04000A16 RID: 2582
		private const char NEWLINE = '\n';

		// Token: 0x04000A17 RID: 2583
		private const char BACKSPACE = '\b';

		// Token: 0x04000A18 RID: 2584
		private const char FORM_FEED = '\f';

		// Token: 0x04000A19 RID: 2585
		private const char HORIZONTAL_TABULATION = '\t';

		// Token: 0x04000A1A RID: 2586
		private const string xmlNamespace = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04000A1B RID: 2587
		private const string xmlnsNamespace = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04000A1C RID: 2588
		[SecurityCritical]
		private static BinHexEncoding binHexEncoding;

		// Token: 0x04000A1D RID: 2589
		private static char[] CharacterAbbrevs;

		// Token: 0x04000A1E RID: 2590
		private string attributeText;

		// Token: 0x04000A1F RID: 2591
		private XmlJsonWriter.JsonDataType dataType;

		// Token: 0x04000A20 RID: 2592
		private int depth;

		// Token: 0x04000A21 RID: 2593
		private bool endElementBuffer;

		// Token: 0x04000A22 RID: 2594
		private bool isWritingDataTypeAttribute;

		// Token: 0x04000A23 RID: 2595
		private bool isWritingServerTypeAttribute;

		// Token: 0x04000A24 RID: 2596
		private bool isWritingXmlnsAttribute;

		// Token: 0x04000A25 RID: 2597
		private bool isWritingXmlnsAttributeDefaultNs;

		// Token: 0x04000A26 RID: 2598
		private XmlJsonWriter.NameState nameState;

		// Token: 0x04000A27 RID: 2599
		private JsonNodeType nodeType;

		// Token: 0x04000A28 RID: 2600
		private XmlJsonWriter.JsonNodeWriter nodeWriter;

		// Token: 0x04000A29 RID: 2601
		private JsonNodeType[] scopes;

		// Token: 0x04000A2A RID: 2602
		private string serverTypeValue;

		// Token: 0x04000A2B RID: 2603
		private WriteState writeState;

		// Token: 0x04000A2C RID: 2604
		private bool wroteServerTypeAttribute;

		// Token: 0x04000A2D RID: 2605
		private bool indent;

		// Token: 0x04000A2E RID: 2606
		private string indentChars;

		// Token: 0x04000A2F RID: 2607
		private int indentLevel;

		// Token: 0x02000191 RID: 401
		private enum JsonDataType
		{
			// Token: 0x04000A31 RID: 2609
			None,
			// Token: 0x04000A32 RID: 2610
			Null,
			// Token: 0x04000A33 RID: 2611
			Boolean,
			// Token: 0x04000A34 RID: 2612
			Number,
			// Token: 0x04000A35 RID: 2613
			String,
			// Token: 0x04000A36 RID: 2614
			Object,
			// Token: 0x04000A37 RID: 2615
			Array
		}

		// Token: 0x02000192 RID: 402
		[Flags]
		private enum NameState
		{
			// Token: 0x04000A39 RID: 2617
			None = 0,
			// Token: 0x04000A3A RID: 2618
			IsWritingNameWithMapping = 1,
			// Token: 0x04000A3B RID: 2619
			IsWritingNameAttribute = 2,
			// Token: 0x04000A3C RID: 2620
			WrittenNameWithMapping = 4
		}

		// Token: 0x02000193 RID: 403
		private class JsonNodeWriter : XmlUTF8NodeWriter
		{
			// Token: 0x0600146D RID: 5229 RVA: 0x0005032E File Offset: 0x0004E52E
			[SecurityCritical]
			internal unsafe void WriteChars(char* chars, int charCount)
			{
				base.UnsafeWriteUTF8Chars(chars, charCount);
			}

			// Token: 0x0600146E RID: 5230 RVA: 0x00050338 File Offset: 0x0004E538
			public JsonNodeWriter()
			{
			}
		}
	}
}
