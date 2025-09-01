using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200006E RID: 110
	internal class XmlMtomReader : XmlDictionaryReader, IXmlLineInfo, IXmlMtomReaderInitializer
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x0001A326 File Offset: 0x00018526
		public XmlMtomReader()
		{
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001A32E File Offset: 0x0001852E
		internal static void DecrementBufferQuota(int maxBuffer, ref int remaining, int size)
		{
			if (remaining - size <= 0)
			{
				remaining = 0;
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM buffer quota exceeded. The maximum size is {0}.", new object[]
				{
					maxBuffer
				})));
			}
			remaining -= size;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001A364 File Offset: 0x00018564
		private void SetReadEncodings(Encoding[] encodings)
		{
			if (encodings == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encodings");
			}
			for (int i = 0; i < encodings.Length; i++)
			{
				if (encodings[i] == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull(string.Format(CultureInfo.InvariantCulture, "encodings[{0}]", i));
				}
			}
			this.encodings = new Encoding[encodings.Length];
			encodings.CopyTo(this.encodings, 0);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001A3C8 File Offset: 0x000185C8
		private void CheckContentType(string contentType)
		{
			if (contentType != null && contentType.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("MTOM content type is invalid."), "contentType"));
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001A3EF File Offset: 0x000185EF
		public void SetInput(byte[] buffer, int offset, int count, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas, int maxBufferSize, OnXmlDictionaryReaderClose onClose)
		{
			this.SetInput(new MemoryStream(buffer, offset, count), encodings, contentType, quotas, maxBufferSize, onClose);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001A409 File Offset: 0x00018609
		public void SetInput(Stream stream, Encoding[] encodings, string contentType, XmlDictionaryReaderQuotas quotas, int maxBufferSize, OnXmlDictionaryReaderClose onClose)
		{
			this.SetReadEncodings(encodings);
			this.CheckContentType(contentType);
			this.Initialize(stream, contentType, quotas, maxBufferSize);
			this.onClose = onClose;
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001A430 File Offset: 0x00018630
		private void Initialize(Stream stream, string contentType, XmlDictionaryReaderQuotas quotas, int maxBufferSize)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			this.maxBufferSize = maxBufferSize;
			this.bufferRemaining = maxBufferSize;
			string boundary;
			string text;
			string expectedType;
			if (contentType == null)
			{
				MimeMessageReader mimeMessageReader = new MimeMessageReader(stream);
				MimeHeaders mimeHeaders = mimeMessageReader.ReadHeaders(this.maxBufferSize, ref this.bufferRemaining);
				this.ReadMessageMimeVersionHeader(mimeHeaders.MimeVersion);
				this.ReadMessageContentTypeHeader(mimeHeaders.ContentType, out boundary, out text, out expectedType);
				stream = mimeMessageReader.GetContentStream();
				if (stream == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM message content is invalid.")));
				}
			}
			else
			{
				this.ReadMessageContentTypeHeader(new ContentTypeHeader(contentType), out boundary, out text, out expectedType);
			}
			this.mimeReader = new MimeReader(stream, boundary);
			this.mimeParts = null;
			this.readingBinaryElement = false;
			XmlMtomReader.MimePart mimePart = (text == null) ? this.ReadRootMimePart() : this.ReadMimePart(this.GetStartUri(text));
			byte[] buffer = mimePart.GetBuffer(this.maxBufferSize, ref this.bufferRemaining);
			int count = (int)mimePart.Length;
			Encoding encoding = this.ReadRootContentTypeHeader(mimePart.Headers.ContentType, this.encodings, expectedType);
			this.CheckContentTransferEncodingOnRoot(mimePart.Headers.ContentTransferEncoding);
			IXmlTextReaderInitializer xmlTextReaderInitializer = this.xmlReader as IXmlTextReaderInitializer;
			if (xmlTextReaderInitializer != null)
			{
				xmlTextReaderInitializer.SetInput(buffer, 0, count, encoding, quotas, null);
				return;
			}
			this.xmlReader = XmlDictionaryReader.CreateTextReader(buffer, 0, count, encoding, quotas, null);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001A57B File Offset: 0x0001877B
		public override XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				return this.xmlReader.Quotas;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001A588 File Offset: 0x00018788
		private void ReadMessageMimeVersionHeader(MimeVersionHeader header)
		{
			if (header != null && header.Version != MimeVersionHeader.Default.Version)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM message has invalid MIME version. Expected '{1}', got '{0}' instead.", new object[]
				{
					header.Version,
					MimeVersionHeader.Default.Version
				})));
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001A5E0 File Offset: 0x000187E0
		private void ReadMessageContentTypeHeader(ContentTypeHeader header, out string boundary, out string start, out string startInfo)
		{
			if (header == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM message content type was not found.")));
			}
			if (string.Compare(MtomGlobals.MediaType, header.MediaType, StringComparison.OrdinalIgnoreCase) != 0 || string.Compare(MtomGlobals.MediaSubtype, header.MediaSubtype, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM message is not multipart: media type should be '{0}', media subtype should be '{1}'.", new object[]
				{
					MtomGlobals.MediaType,
					MtomGlobals.MediaSubtype
				})));
			}
			string b;
			if (!header.Parameters.TryGetValue(MtomGlobals.TypeParam, out b) || MtomGlobals.XopType != b)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM msssage type is not '{0}'.", new object[]
				{
					MtomGlobals.XopType
				})));
			}
			if (!header.Parameters.TryGetValue(MtomGlobals.BoundaryParam, out boundary))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Required MTOM parameter '{0}' is not specified.", new object[]
				{
					MtomGlobals.BoundaryParam
				})));
			}
			if (!MailBnfHelper.IsValidMimeBoundary(boundary))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MIME boundary is invalid: '{0}'.", new object[]
				{
					boundary
				})));
			}
			if (!header.Parameters.TryGetValue(MtomGlobals.StartParam, out start))
			{
				start = null;
			}
			if (!header.Parameters.TryGetValue(MtomGlobals.StartInfoParam, out startInfo))
			{
				startInfo = null;
			}
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001A728 File Offset: 0x00018928
		private Encoding ReadRootContentTypeHeader(ContentTypeHeader header, Encoding[] expectedEncodings, string expectedType)
		{
			if (header == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM root content type is not found.")));
			}
			if (string.Compare(MtomGlobals.XopMediaType, header.MediaType, StringComparison.OrdinalIgnoreCase) != 0 || string.Compare(MtomGlobals.XopMediaSubtype, header.MediaSubtype, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM root should have media type '{0}' and subtype '{1}'.", new object[]
				{
					MtomGlobals.XopMediaType,
					MtomGlobals.XopMediaSubtype
				})));
			}
			string text;
			if (!header.Parameters.TryGetValue(MtomGlobals.CharsetParam, out text) || text == null || text.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Required MTOM root parameter '{0}' is not specified.", new object[]
				{
					MtomGlobals.CharsetParam
				})));
			}
			Encoding encoding = null;
			for (int i = 0; i < this.encodings.Length; i++)
			{
				if (string.Compare(text, expectedEncodings[i].WebName, StringComparison.OrdinalIgnoreCase) == 0)
				{
					encoding = expectedEncodings[i];
					break;
				}
			}
			if (encoding == null)
			{
				if (string.Compare(text, "utf-16LE", StringComparison.OrdinalIgnoreCase) == 0)
				{
					for (int j = 0; j < this.encodings.Length; j++)
					{
						if (string.Compare(expectedEncodings[j].WebName, Encoding.Unicode.WebName, StringComparison.OrdinalIgnoreCase) == 0)
						{
							encoding = expectedEncodings[j];
							break;
						}
					}
				}
				else if (string.Compare(text, "utf-16BE", StringComparison.OrdinalIgnoreCase) == 0)
				{
					for (int k = 0; k < this.encodings.Length; k++)
					{
						if (string.Compare(expectedEncodings[k].WebName, Encoding.BigEndianUnicode.WebName, StringComparison.OrdinalIgnoreCase) == 0)
						{
							encoding = expectedEncodings[k];
							break;
						}
					}
				}
				if (encoding == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int l = 0; l < this.encodings.Length; l++)
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(" | ");
						}
						stringBuilder.Append(this.encodings[l].WebName);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Unexpected charset on MTOM root. Expected '{1}', got '{0}' instead.", new object[]
					{
						text,
						stringBuilder.ToString()
					})));
				}
			}
			if (expectedType != null)
			{
				string text2;
				if (!header.Parameters.TryGetValue(MtomGlobals.TypeParam, out text2) || text2 == null || text2.Length == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Required MTOM root parameter '{0}' is not specified.", new object[]
					{
						MtomGlobals.TypeParam
					})));
				}
				if (text2 != expectedType)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Unexpected type on MTOM root. Expected '{1}', got '{0}' instead.", new object[]
					{
						text2,
						expectedType
					})));
				}
			}
			return encoding;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001A988 File Offset: 0x00018B88
		private void CheckContentTransferEncodingOnRoot(ContentTransferEncodingHeader header)
		{
			if (header != null && header.ContentTransferEncoding == ContentTransferEncoding.Other)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM content transfer encoding value is not supported. Raw value is '{0}', '{1}' in 7bit encoding, '{2}' in 8bit encoding, and '{3}' in binary.", new object[]
				{
					header.Value,
					ContentTransferEncodingHeader.SevenBit.ContentTransferEncodingValue,
					ContentTransferEncodingHeader.EightBit.ContentTransferEncodingValue,
					ContentTransferEncodingHeader.Binary.ContentTransferEncodingValue
				})));
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001A9EC File Offset: 0x00018BEC
		private void CheckContentTransferEncodingOnBinaryPart(ContentTransferEncodingHeader header)
		{
			if (header == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM content transfer encoding is not present. ContentTransferEncoding header is '{0}'.", new object[]
				{
					ContentTransferEncodingHeader.Binary.ContentTransferEncodingValue
				})));
			}
			if (header.ContentTransferEncoding != ContentTransferEncoding.Binary)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Invalid transfer encoding for MIME part: '{0}', in binary: '{1}'.", new object[]
				{
					header.Value,
					ContentTransferEncodingHeader.Binary.ContentTransferEncodingValue
				})));
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001AA60 File Offset: 0x00018C60
		private string GetStartUri(string startUri)
		{
			if (!startUri.StartsWith("<", StringComparison.Ordinal))
			{
				return string.Format(CultureInfo.InvariantCulture, "<{0}>", startUri);
			}
			if (startUri.EndsWith(">", StringComparison.Ordinal))
			{
				return startUri;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Invalid MTOM start URI: '{0}'.", new object[]
			{
				startUri
			})));
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001AABC File Offset: 0x00018CBC
		public override bool Read()
		{
			bool flag = this.xmlReader.Read();
			if (this.xmlReader.NodeType == XmlNodeType.Element)
			{
				XmlMtomReader.XopIncludeReader xopIncludeReader = null;
				if (this.xmlReader.IsStartElement(MtomGlobals.XopIncludeLocalName, MtomGlobals.XopIncludeNamespace))
				{
					string text = null;
					while (this.xmlReader.MoveToNextAttribute())
					{
						if (this.xmlReader.LocalName == MtomGlobals.XopIncludeHrefLocalName && this.xmlReader.NamespaceURI == MtomGlobals.XopIncludeHrefNamespace)
						{
							text = this.xmlReader.Value;
						}
						else if (this.xmlReader.NamespaceURI == MtomGlobals.XopIncludeNamespace)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("xop Include element has invalid attribute: '{0}' in '{1}' namespace.", new object[]
							{
								this.xmlReader.LocalName,
								MtomGlobals.XopIncludeNamespace
							})));
						}
					}
					if (text == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("xop Include element did not specify '{0}' attribute.", new object[]
						{
							MtomGlobals.XopIncludeHrefLocalName
						})));
					}
					XmlMtomReader.MimePart mimePart = this.ReadMimePart(text);
					this.CheckContentTransferEncodingOnBinaryPart(mimePart.Headers.ContentTransferEncoding);
					this.part = mimePart;
					xopIncludeReader = new XmlMtomReader.XopIncludeReader(mimePart, this.xmlReader);
					xopIncludeReader.Read();
					this.xmlReader.MoveToElement();
					if (this.xmlReader.IsEmptyElement)
					{
						this.xmlReader.Read();
					}
					else
					{
						int depth = this.xmlReader.Depth;
						this.xmlReader.ReadStartElement();
						while (this.xmlReader.Depth > depth)
						{
							if (this.xmlReader.IsStartElement() && this.xmlReader.NamespaceURI == MtomGlobals.XopIncludeNamespace)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("xop Include element has invalid element: '{0}' in '{1}' namespace.", new object[]
								{
									this.xmlReader.LocalName,
									MtomGlobals.XopIncludeNamespace
								})));
							}
							this.xmlReader.Skip();
						}
						this.xmlReader.ReadEndElement();
					}
				}
				if (xopIncludeReader != null)
				{
					this.xmlReader.MoveToContent();
					this.infosetReader = this.xmlReader;
					this.xmlReader = xopIncludeReader;
				}
			}
			if (this.xmlReader.ReadState == ReadState.EndOfFile && this.infosetReader != null)
			{
				if (!flag)
				{
					flag = this.infosetReader.Read();
				}
				this.part.Release(this.maxBufferSize, ref this.bufferRemaining);
				this.xmlReader = this.infosetReader;
				this.infosetReader = null;
			}
			return flag;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001AD2C File Offset: 0x00018F2C
		private XmlMtomReader.MimePart ReadMimePart(string uri)
		{
			XmlMtomReader.MimePart mimePart = null;
			if (uri == null || uri.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("empty URI is invalid for MTOM MIME part.")));
			}
			string text = null;
			if (uri.StartsWith(MimeGlobals.ContentIDScheme, StringComparison.Ordinal))
			{
				text = string.Format(CultureInfo.InvariantCulture, "<{0}>", Uri.UnescapeDataString(uri.Substring(MimeGlobals.ContentIDScheme.Length)));
			}
			else if (uri.StartsWith("<", StringComparison.Ordinal))
			{
				text = uri;
			}
			if (text == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Invalid MTOM CID URI: '{0}'.", new object[]
				{
					uri
				})));
			}
			if (this.mimeParts != null && this.mimeParts.TryGetValue(text, out mimePart))
			{
				if (mimePart.ReferencedFromInfoset)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Specified MIME part '{0}' is referenced more than once.", new object[]
					{
						text
					})));
				}
			}
			else
			{
				int maxMimeParts = AppSettings.MaxMimeParts;
				while (mimePart == null && this.mimeReader.ReadNextPart())
				{
					MimeHeaders mimeHeaders = this.mimeReader.ReadHeaders(this.maxBufferSize, ref this.bufferRemaining);
					Stream contentStream = this.mimeReader.GetContentStream();
					if (contentStream == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM message content in MIME part is invalid.")));
					}
					ContentIDHeader contentIDHeader = (mimeHeaders == null) ? null : mimeHeaders.ContentID;
					if (contentIDHeader == null || contentIDHeader.Value == null)
					{
						int num = 256;
						byte[] buffer = new byte[num];
						int num2;
						do
						{
							num2 = contentStream.Read(buffer, 0, num);
						}
						while (num2 > 0);
					}
					else
					{
						string value = mimeHeaders.ContentID.Value;
						XmlMtomReader.MimePart mimePart2 = new XmlMtomReader.MimePart(contentStream, mimeHeaders);
						if (this.mimeParts == null)
						{
							this.mimeParts = new Dictionary<string, XmlMtomReader.MimePart>();
						}
						this.mimeParts.Add(value, mimePart2);
						if (this.mimeParts.Count > maxMimeParts)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MIME parts number exceeded the maximum settings. Must be less than {0}. Specified as '{1}'.", new object[]
							{
								maxMimeParts,
								"microsoft:xmldictionaryreader:maxmimeparts"
							})));
						}
						if (value.Equals(text))
						{
							mimePart = mimePart2;
						}
						else
						{
							mimePart2.GetBuffer(this.maxBufferSize, ref this.bufferRemaining);
						}
					}
				}
				if (mimePart == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM part with URI '{0}' is not found.", new object[]
					{
						uri
					})));
				}
			}
			mimePart.ReferencedFromInfoset = true;
			return mimePart;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001AF68 File Offset: 0x00019168
		private XmlMtomReader.MimePart ReadRootMimePart()
		{
			if (!this.mimeReader.ReadNextPart())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM root part is not found.")));
			}
			MimeHeaders headers = this.mimeReader.ReadHeaders(this.maxBufferSize, ref this.bufferRemaining);
			Stream contentStream = this.mimeReader.GetContentStream();
			if (contentStream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM message content in MIME part is invalid.")));
			}
			return new XmlMtomReader.MimePart(contentStream, headers);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001AFD8 File Offset: 0x000191D8
		private void AdvanceToContentOnElement()
		{
			if (this.NodeType != XmlNodeType.Attribute)
			{
				this.MoveToContent();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001AFEA File Offset: 0x000191EA
		public override int AttributeCount
		{
			get
			{
				return this.xmlReader.AttributeCount;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001AFF7 File Offset: 0x000191F7
		public override string BaseURI
		{
			get
			{
				return this.xmlReader.BaseURI;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001B004 File Offset: 0x00019204
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.xmlReader.CanReadBinaryContent;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001B011 File Offset: 0x00019211
		public override bool CanReadValueChunk
		{
			get
			{
				return this.xmlReader.CanReadValueChunk;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0001B01E File Offset: 0x0001921E
		public override bool CanResolveEntity
		{
			get
			{
				return this.xmlReader.CanResolveEntity;
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001B02C File Offset: 0x0001922C
		public override void Close()
		{
			this.xmlReader.Close();
			this.mimeReader.Close();
			OnXmlDictionaryReaderClose onXmlDictionaryReaderClose = this.onClose;
			this.onClose = null;
			if (onXmlDictionaryReaderClose != null)
			{
				try
				{
					onXmlDictionaryReaderClose(this);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0001B08C File Offset: 0x0001928C
		public override int Depth
		{
			get
			{
				return this.xmlReader.Depth;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0001B099 File Offset: 0x00019299
		public override bool EOF
		{
			get
			{
				return this.xmlReader.EOF;
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001B0A6 File Offset: 0x000192A6
		public override string GetAttribute(int index)
		{
			return this.xmlReader.GetAttribute(index);
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001B0B4 File Offset: 0x000192B4
		public override string GetAttribute(string name)
		{
			return this.xmlReader.GetAttribute(name);
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001B0C2 File Offset: 0x000192C2
		public override string GetAttribute(string name, string ns)
		{
			return this.xmlReader.GetAttribute(name, ns);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001B0D1 File Offset: 0x000192D1
		public override string GetAttribute(XmlDictionaryString localName, XmlDictionaryString ns)
		{
			return this.xmlReader.GetAttribute(localName, ns);
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001B0E0 File Offset: 0x000192E0
		public override bool HasAttributes
		{
			get
			{
				return this.xmlReader.HasAttributes;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001B0ED File Offset: 0x000192ED
		public override bool HasValue
		{
			get
			{
				return this.xmlReader.HasValue;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001B0FA File Offset: 0x000192FA
		public override bool IsDefault
		{
			get
			{
				return this.xmlReader.IsDefault;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001B107 File Offset: 0x00019307
		public override bool IsEmptyElement
		{
			get
			{
				return this.xmlReader.IsEmptyElement;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001B114 File Offset: 0x00019314
		public override bool IsLocalName(string localName)
		{
			return this.xmlReader.IsLocalName(localName);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001B122 File Offset: 0x00019322
		public override bool IsLocalName(XmlDictionaryString localName)
		{
			return this.xmlReader.IsLocalName(localName);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001B130 File Offset: 0x00019330
		public override bool IsNamespaceUri(string ns)
		{
			return this.xmlReader.IsNamespaceUri(ns);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001B13E File Offset: 0x0001933E
		public override bool IsNamespaceUri(XmlDictionaryString ns)
		{
			return this.xmlReader.IsNamespaceUri(ns);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001B14C File Offset: 0x0001934C
		public override bool IsStartElement()
		{
			return this.xmlReader.IsStartElement();
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0001B159 File Offset: 0x00019359
		public override bool IsStartElement(string localName)
		{
			return this.xmlReader.IsStartElement(localName);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001B167 File Offset: 0x00019367
		public override bool IsStartElement(string localName, string ns)
		{
			return this.xmlReader.IsStartElement(localName, ns);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001B176 File Offset: 0x00019376
		public override bool IsStartElement(XmlDictionaryString localName, XmlDictionaryString ns)
		{
			return this.xmlReader.IsStartElement(localName, ns);
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0001B185 File Offset: 0x00019385
		public override string LocalName
		{
			get
			{
				return this.xmlReader.LocalName;
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001B192 File Offset: 0x00019392
		public override string LookupNamespace(string ns)
		{
			return this.xmlReader.LookupNamespace(ns);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001B1A0 File Offset: 0x000193A0
		public override void MoveToAttribute(int index)
		{
			this.xmlReader.MoveToAttribute(index);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001B1AE File Offset: 0x000193AE
		public override bool MoveToAttribute(string name)
		{
			return this.xmlReader.MoveToAttribute(name);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001B1BC File Offset: 0x000193BC
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.xmlReader.MoveToAttribute(name, ns);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001B1CB File Offset: 0x000193CB
		public override bool MoveToElement()
		{
			return this.xmlReader.MoveToElement();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001B1D8 File Offset: 0x000193D8
		public override bool MoveToFirstAttribute()
		{
			return this.xmlReader.MoveToFirstAttribute();
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001B1E5 File Offset: 0x000193E5
		public override bool MoveToNextAttribute()
		{
			return this.xmlReader.MoveToNextAttribute();
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001B1F2 File Offset: 0x000193F2
		public override string Name
		{
			get
			{
				return this.xmlReader.Name;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001B1FF File Offset: 0x000193FF
		public override string NamespaceURI
		{
			get
			{
				return this.xmlReader.NamespaceURI;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001B20C File Offset: 0x0001940C
		public override XmlNameTable NameTable
		{
			get
			{
				return this.xmlReader.NameTable;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001B219 File Offset: 0x00019419
		public override XmlNodeType NodeType
		{
			get
			{
				return this.xmlReader.NodeType;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001B226 File Offset: 0x00019426
		public override string Prefix
		{
			get
			{
				return this.xmlReader.Prefix;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001B233 File Offset: 0x00019433
		public override char QuoteChar
		{
			get
			{
				return this.xmlReader.QuoteChar;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001B240 File Offset: 0x00019440
		public override bool ReadAttributeValue()
		{
			return this.xmlReader.ReadAttributeValue();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001B24D File Offset: 0x0001944D
		public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAs(returnType, namespaceResolver);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001B262 File Offset: 0x00019462
		public override byte[] ReadContentAsBase64()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsBase64();
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001B275 File Offset: 0x00019475
		public override int ReadValueAsBase64(byte[] buffer, int offset, int count)
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadValueAsBase64(buffer, offset, count);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001B28B File Offset: 0x0001948B
		public override int ReadContentAsBase64(byte[] buffer, int offset, int count)
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsBase64(buffer, offset, count);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001B2A4 File Offset: 0x000194A4
		public override int ReadElementContentAsBase64(byte[] buffer, int offset, int count)
		{
			if (!this.readingBinaryElement)
			{
				if (this.IsEmptyElement)
				{
					this.Read();
					return 0;
				}
				this.ReadStartElement();
				this.readingBinaryElement = true;
			}
			int num = this.ReadContentAsBase64(buffer, offset, count);
			if (num == 0)
			{
				this.ReadEndElement();
				this.readingBinaryElement = false;
			}
			return num;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001B2F0 File Offset: 0x000194F0
		public override int ReadElementContentAsBinHex(byte[] buffer, int offset, int count)
		{
			if (!this.readingBinaryElement)
			{
				if (this.IsEmptyElement)
				{
					this.Read();
					return 0;
				}
				this.ReadStartElement();
				this.readingBinaryElement = true;
			}
			int num = this.ReadContentAsBinHex(buffer, offset, count);
			if (num == 0)
			{
				this.ReadEndElement();
				this.readingBinaryElement = false;
			}
			return num;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001B33C File Offset: 0x0001953C
		public override int ReadContentAsBinHex(byte[] buffer, int offset, int count)
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsBinHex(buffer, offset, count);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001B352 File Offset: 0x00019552
		public override bool ReadContentAsBoolean()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsBoolean();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001B365 File Offset: 0x00019565
		public override int ReadContentAsChars(char[] chars, int index, int count)
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsChars(chars, index, count);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001B37B File Offset: 0x0001957B
		public override DateTime ReadContentAsDateTime()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsDateTime();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001B38E File Offset: 0x0001958E
		public override decimal ReadContentAsDecimal()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsDecimal();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001B3A1 File Offset: 0x000195A1
		public override double ReadContentAsDouble()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsDouble();
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001B3B4 File Offset: 0x000195B4
		public override int ReadContentAsInt()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsInt();
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001B3C7 File Offset: 0x000195C7
		public override long ReadContentAsLong()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsLong();
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001B3DA File Offset: 0x000195DA
		public override object ReadContentAsObject()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsObject();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001B3ED File Offset: 0x000195ED
		public override float ReadContentAsFloat()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsFloat();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001B400 File Offset: 0x00019600
		public override string ReadContentAsString()
		{
			this.AdvanceToContentOnElement();
			return this.xmlReader.ReadContentAsString();
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001B413 File Offset: 0x00019613
		public override string ReadInnerXml()
		{
			return this.xmlReader.ReadInnerXml();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001B420 File Offset: 0x00019620
		public override string ReadOuterXml()
		{
			return this.xmlReader.ReadOuterXml();
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001B42D File Offset: 0x0001962D
		public override ReadState ReadState
		{
			get
			{
				if (this.xmlReader.ReadState != ReadState.Interactive && this.infosetReader != null)
				{
					return this.infosetReader.ReadState;
				}
				return this.xmlReader.ReadState;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x0001B45C File Offset: 0x0001965C
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			return this.xmlReader.ReadValueChunk(buffer, index, count);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001B46C File Offset: 0x0001966C
		public override void ResolveEntity()
		{
			this.xmlReader.ResolveEntity();
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001B479 File Offset: 0x00019679
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.xmlReader.Settings;
			}
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001B486 File Offset: 0x00019686
		public override void Skip()
		{
			this.xmlReader.Skip();
		}

		// Token: 0x170000B7 RID: 183
		public override string this[int index]
		{
			get
			{
				return this.xmlReader[index];
			}
		}

		// Token: 0x170000B8 RID: 184
		public override string this[string name]
		{
			get
			{
				return this.xmlReader[name];
			}
		}

		// Token: 0x170000B9 RID: 185
		public override string this[string name, string ns]
		{
			get
			{
				return this.xmlReader[name, ns];
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0001B4BE File Offset: 0x000196BE
		public override string Value
		{
			get
			{
				return this.xmlReader.Value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0001B4CB File Offset: 0x000196CB
		public override Type ValueType
		{
			get
			{
				return this.xmlReader.ValueType;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0001B4D8 File Offset: 0x000196D8
		public override string XmlLang
		{
			get
			{
				return this.xmlReader.XmlLang;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001B4E5 File Offset: 0x000196E5
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.xmlReader.XmlSpace;
			}
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001B4F4 File Offset: 0x000196F4
		public bool HasLineInfo()
		{
			if (this.xmlReader.ReadState == ReadState.Closed)
			{
				return false;
			}
			IXmlLineInfo xmlLineInfo = this.xmlReader as IXmlLineInfo;
			return xmlLineInfo != null && xmlLineInfo.HasLineInfo();
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001B528 File Offset: 0x00019728
		public int LineNumber
		{
			get
			{
				if (this.xmlReader.ReadState == ReadState.Closed)
				{
					return 0;
				}
				IXmlLineInfo xmlLineInfo = this.xmlReader as IXmlLineInfo;
				if (xmlLineInfo == null)
				{
					return 0;
				}
				return xmlLineInfo.LineNumber;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0001B55C File Offset: 0x0001975C
		public int LinePosition
		{
			get
			{
				if (this.xmlReader.ReadState == ReadState.Closed)
				{
					return 0;
				}
				IXmlLineInfo xmlLineInfo = this.xmlReader as IXmlLineInfo;
				if (xmlLineInfo == null)
				{
					return 0;
				}
				return xmlLineInfo.LinePosition;
			}
		}

		// Token: 0x040002BE RID: 702
		private Encoding[] encodings;

		// Token: 0x040002BF RID: 703
		private XmlDictionaryReader xmlReader;

		// Token: 0x040002C0 RID: 704
		private XmlDictionaryReader infosetReader;

		// Token: 0x040002C1 RID: 705
		private MimeReader mimeReader;

		// Token: 0x040002C2 RID: 706
		private Dictionary<string, XmlMtomReader.MimePart> mimeParts;

		// Token: 0x040002C3 RID: 707
		private OnXmlDictionaryReaderClose onClose;

		// Token: 0x040002C4 RID: 708
		private bool readingBinaryElement;

		// Token: 0x040002C5 RID: 709
		private int maxBufferSize;

		// Token: 0x040002C6 RID: 710
		private int bufferRemaining;

		// Token: 0x040002C7 RID: 711
		private XmlMtomReader.MimePart part;

		// Token: 0x0200006F RID: 111
		internal class MimePart
		{
			// Token: 0x0600063A RID: 1594 RVA: 0x0001B590 File Offset: 0x00019790
			internal MimePart(Stream stream, MimeHeaders headers)
			{
				this.stream = stream;
				this.headers = headers;
			}

			// Token: 0x170000C0 RID: 192
			// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001B5A6 File Offset: 0x000197A6
			internal Stream Stream
			{
				get
				{
					return this.stream;
				}
			}

			// Token: 0x170000C1 RID: 193
			// (get) Token: 0x0600063C RID: 1596 RVA: 0x0001B5AE File Offset: 0x000197AE
			internal MimeHeaders Headers
			{
				get
				{
					return this.headers;
				}
			}

			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001B5B6 File Offset: 0x000197B6
			// (set) Token: 0x0600063E RID: 1598 RVA: 0x0001B5BE File Offset: 0x000197BE
			internal bool ReferencedFromInfoset
			{
				get
				{
					return this.isReferencedFromInfoset;
				}
				set
				{
					this.isReferencedFromInfoset = value;
				}
			}

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x0600063F RID: 1599 RVA: 0x0001B5C7 File Offset: 0x000197C7
			internal long Length
			{
				get
				{
					if (!this.stream.CanSeek)
					{
						return 0L;
					}
					return this.stream.Length;
				}
			}

			// Token: 0x06000640 RID: 1600 RVA: 0x0001B5E4 File Offset: 0x000197E4
			internal byte[] GetBuffer(int maxBuffer, ref int remaining)
			{
				if (this.buffer == null)
				{
					MemoryStream memoryStream = this.stream.CanSeek ? new MemoryStream((int)this.stream.Length) : new MemoryStream();
					int num = 256;
					byte[] array = new byte[num];
					int num2;
					do
					{
						num2 = this.stream.Read(array, 0, num);
						XmlMtomReader.DecrementBufferQuota(maxBuffer, ref remaining, num2);
						if (num2 > 0)
						{
							memoryStream.Write(array, 0, num2);
						}
					}
					while (num2 > 0);
					memoryStream.Seek(0L, SeekOrigin.Begin);
					this.buffer = memoryStream.GetBuffer();
					this.stream = memoryStream;
				}
				return this.buffer;
			}

			// Token: 0x06000641 RID: 1601 RVA: 0x0001B679 File Offset: 0x00019879
			internal void Release(int maxBuffer, ref int remaining)
			{
				remaining += (int)this.Length;
				this.headers.Release(ref remaining);
			}

			// Token: 0x040002C8 RID: 712
			private Stream stream;

			// Token: 0x040002C9 RID: 713
			private MimeHeaders headers;

			// Token: 0x040002CA RID: 714
			private byte[] buffer;

			// Token: 0x040002CB RID: 715
			private bool isReferencedFromInfoset;
		}

		// Token: 0x02000070 RID: 112
		internal class XopIncludeReader : XmlDictionaryReader, IXmlLineInfo
		{
			// Token: 0x06000642 RID: 1602 RVA: 0x0001B694 File Offset: 0x00019894
			public XopIncludeReader(XmlMtomReader.MimePart part, XmlDictionaryReader reader)
			{
				if (part == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("part");
				}
				if (reader == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("reader");
				}
				this.part = part;
				this.parentReader = reader;
				this.readState = ReadState.Initial;
				this.nodeType = XmlNodeType.None;
				this.chunkSize = Math.Min(reader.Quotas.MaxBytesPerRead, this.chunkSize);
				this.bytesRemaining = this.chunkSize;
				this.finishedStream = false;
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001B719 File Offset: 0x00019919
			public override XmlDictionaryReaderQuotas Quotas
			{
				get
				{
					return this.parentReader.Quotas;
				}
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001B726 File Offset: 0x00019926
			public override XmlNodeType NodeType
			{
				get
				{
					if (this.readState != ReadState.Interactive)
					{
						return this.parentReader.NodeType;
					}
					return this.nodeType;
				}
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x0001B744 File Offset: 0x00019944
			public override bool Read()
			{
				bool result = true;
				switch (this.readState)
				{
				case ReadState.Initial:
					this.readState = ReadState.Interactive;
					this.nodeType = XmlNodeType.Text;
					break;
				case ReadState.Interactive:
					if (this.finishedStream || (this.bytesRemaining == this.chunkSize && this.stringValue == null))
					{
						this.readState = ReadState.EndOfFile;
						this.nodeType = XmlNodeType.EndElement;
					}
					else
					{
						this.bytesRemaining = this.chunkSize;
					}
					break;
				case ReadState.EndOfFile:
					this.nodeType = XmlNodeType.None;
					result = false;
					break;
				}
				this.stringValue = null;
				this.binHexStream = null;
				this.valueOffset = 0;
				this.valueCount = 0;
				this.stringOffset = 0;
				this.CloseStreams();
				return result;
			}

			// Token: 0x06000646 RID: 1606 RVA: 0x0001B7F4 File Offset: 0x000199F4
			public override int ReadValueAsBase64(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
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
				if (this.stringValue != null)
				{
					count = Math.Min(count, this.valueCount);
					if (count > 0)
					{
						Buffer.BlockCopy(this.valueBuffer, this.valueOffset, buffer, offset, count);
						this.valueOffset += count;
						this.valueCount -= count;
					}
					return count;
				}
				if (this.bytesRemaining < count)
				{
					count = this.bytesRemaining;
				}
				int i = 0;
				if (this.readState == ReadState.Interactive)
				{
					while (i < count)
					{
						int num = this.part.Stream.Read(buffer, offset + i, count - i);
						if (num == 0)
						{
							this.finishedStream = true;
							break;
						}
						i += num;
					}
				}
				this.bytesRemaining -= i;
				return i;
			}

			// Token: 0x06000647 RID: 1607 RVA: 0x0001B958 File Offset: 0x00019B58
			public override int ReadContentAsBase64(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
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
				if (this.valueCount > 0)
				{
					count = Math.Min(count, this.valueCount);
					Buffer.BlockCopy(this.valueBuffer, this.valueOffset, buffer, offset, count);
					this.valueOffset += count;
					this.valueCount -= count;
					return count;
				}
				if (this.chunkSize < count)
				{
					count = this.chunkSize;
				}
				int i = 0;
				if (this.readState == ReadState.Interactive)
				{
					while (i < count)
					{
						int num = this.part.Stream.Read(buffer, offset + i, count - i);
						if (num == 0)
						{
							this.finishedStream = true;
							if (!this.Read())
							{
								break;
							}
						}
						i += num;
					}
				}
				this.bytesRemaining = this.chunkSize;
				return i;
			}

			// Token: 0x06000648 RID: 1608 RVA: 0x0001BABC File Offset: 0x00019CBC
			public override int ReadContentAsBinHex(byte[] buffer, int offset, int count)
			{
				if (buffer == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("buffer");
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
				if (this.chunkSize < count)
				{
					count = this.chunkSize;
				}
				int i = 0;
				int num = 0;
				while (i < count)
				{
					if (this.binHexStream == null)
					{
						try
						{
							this.binHexStream = new MemoryStream(new BinHexEncoding().GetBytes(this.Value));
						}
						catch (FormatException ex)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(ex.Message, ex));
						}
					}
					int num2 = this.binHexStream.Read(buffer, offset + i, count - i);
					if (num2 == 0)
					{
						this.finishedStream = true;
						if (!this.Read())
						{
							break;
						}
						num = 0;
					}
					i += num2;
					num += num2;
				}
				if (this.stringValue != null && num > 0)
				{
					this.stringValue = this.stringValue.Substring(num * 2);
					this.stringOffset = Math.Max(0, this.stringOffset - num * 2);
					this.bytesRemaining = this.chunkSize;
				}
				return i;
			}

			// Token: 0x06000649 RID: 1609 RVA: 0x0001BC50 File Offset: 0x00019E50
			public override int ReadValueChunk(char[] chars, int offset, int count)
			{
				if (chars == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("chars");
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
				if (this.readState != ReadState.Interactive)
				{
					return 0;
				}
				string value = this.Value;
				count = Math.Min(this.stringValue.Length - this.stringOffset, count);
				if (count > 0)
				{
					this.stringValue.CopyTo(this.stringOffset, chars, offset, count);
					this.stringOffset += count;
				}
				return count;
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001BD60 File Offset: 0x00019F60
			public override string Value
			{
				get
				{
					if (this.readState != ReadState.Interactive)
					{
						return string.Empty;
					}
					if (this.stringValue == null)
					{
						int i = this.bytesRemaining;
						i -= i % 3;
						if (this.valueCount > 0 && this.valueOffset > 0)
						{
							Buffer.BlockCopy(this.valueBuffer, this.valueOffset, this.valueBuffer, 0, this.valueCount);
							this.valueOffset = 0;
						}
						i -= this.valueCount;
						if (this.valueBuffer == null)
						{
							this.valueBuffer = new byte[i];
						}
						else if (this.valueBuffer.Length < i)
						{
							Array.Resize<byte>(ref this.valueBuffer, i);
						}
						byte[] array = this.valueBuffer;
						int num = 0;
						while (i > 0)
						{
							int num2 = this.part.Stream.Read(array, num, i);
							if (num2 == 0)
							{
								this.finishedStream = true;
								break;
							}
							this.bytesRemaining -= num2;
							this.valueCount += num2;
							i -= num2;
							num += num2;
						}
						this.stringValue = Convert.ToBase64String(array, 0, this.valueCount);
					}
					return this.stringValue;
				}
			}

			// Token: 0x0600064B RID: 1611 RVA: 0x0001BE70 File Offset: 0x0001A070
			public override string ReadContentAsString()
			{
				int num = this.Quotas.MaxStringContentLength;
				StringBuilder stringBuilder = new StringBuilder();
				do
				{
					string value = this.Value;
					if (value.Length > num)
					{
						XmlExceptionHelper.ThrowMaxStringContentLengthExceeded(this, this.Quotas.MaxStringContentLength);
					}
					num -= value.Length;
					stringBuilder.Append(value);
				}
				while (this.Read());
				return stringBuilder.ToString();
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x0600064C RID: 1612 RVA: 0x00003127 File Offset: 0x00001327
			public override int AttributeCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001BECF File Offset: 0x0001A0CF
			public override string BaseURI
			{
				get
				{
					return this.parentReader.BaseURI;
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x0600064E RID: 1614 RVA: 0x000066E8 File Offset: 0x000048E8
			public override bool CanReadBinaryContent
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000CA RID: 202
			// (get) Token: 0x0600064F RID: 1615 RVA: 0x000066E8 File Offset: 0x000048E8
			public override bool CanReadValueChunk
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x06000650 RID: 1616 RVA: 0x0001BEDC File Offset: 0x0001A0DC
			public override bool CanResolveEntity
			{
				get
				{
					return this.parentReader.CanResolveEntity;
				}
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x0001BEE9 File Offset: 0x0001A0E9
			public override void Close()
			{
				this.CloseStreams();
				this.readState = ReadState.Closed;
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
			private void CloseStreams()
			{
				if (this.binHexStream != null)
				{
					this.binHexStream.Close();
					this.binHexStream = null;
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x06000653 RID: 1619 RVA: 0x0001BF14 File Offset: 0x0001A114
			public override int Depth
			{
				get
				{
					if (this.readState != ReadState.Interactive)
					{
						return this.parentReader.Depth;
					}
					return this.parentReader.Depth + 1;
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x06000654 RID: 1620 RVA: 0x0001BF38 File Offset: 0x0001A138
			public override bool EOF
			{
				get
				{
					return this.readState == ReadState.EndOfFile;
				}
			}

			// Token: 0x06000655 RID: 1621 RVA: 0x0001BF43 File Offset: 0x0001A143
			public override string GetAttribute(int index)
			{
				return null;
			}

			// Token: 0x06000656 RID: 1622 RVA: 0x0001BF43 File Offset: 0x0001A143
			public override string GetAttribute(string name)
			{
				return null;
			}

			// Token: 0x06000657 RID: 1623 RVA: 0x0001BF43 File Offset: 0x0001A143
			public override string GetAttribute(string name, string ns)
			{
				return null;
			}

			// Token: 0x06000658 RID: 1624 RVA: 0x0001BF43 File Offset: 0x0001A143
			public override string GetAttribute(XmlDictionaryString localName, XmlDictionaryString ns)
			{
				return null;
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x06000659 RID: 1625 RVA: 0x00003127 File Offset: 0x00001327
			public override bool HasAttributes
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001BF46 File Offset: 0x0001A146
			public override bool HasValue
			{
				get
				{
					return this.readState == ReadState.Interactive;
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x0600065B RID: 1627 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsDefault
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x0600065C RID: 1628 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsEmptyElement
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600065D RID: 1629 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsLocalName(string localName)
			{
				return false;
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsLocalName(XmlDictionaryString localName)
			{
				return false;
			}

			// Token: 0x0600065F RID: 1631 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsNamespaceUri(string ns)
			{
				return false;
			}

			// Token: 0x06000660 RID: 1632 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsNamespaceUri(XmlDictionaryString ns)
			{
				return false;
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsStartElement()
			{
				return false;
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsStartElement(string localName)
			{
				return false;
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsStartElement(string localName, string ns)
			{
				return false;
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00003127 File Offset: 0x00001327
			public override bool IsStartElement(XmlDictionaryString localName, XmlDictionaryString ns)
			{
				return false;
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x06000665 RID: 1637 RVA: 0x0001BF51 File Offset: 0x0001A151
			public override string LocalName
			{
				get
				{
					if (this.readState != ReadState.Interactive)
					{
						return this.parentReader.LocalName;
					}
					return string.Empty;
				}
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x0001BF6D File Offset: 0x0001A16D
			public override string LookupNamespace(string ns)
			{
				return this.parentReader.LookupNamespace(ns);
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void MoveToAttribute(int index)
			{
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x00003127 File Offset: 0x00001327
			public override bool MoveToAttribute(string name)
			{
				return false;
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x00003127 File Offset: 0x00001327
			public override bool MoveToAttribute(string name, string ns)
			{
				return false;
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x00003127 File Offset: 0x00001327
			public override bool MoveToElement()
			{
				return false;
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x00003127 File Offset: 0x00001327
			public override bool MoveToFirstAttribute()
			{
				return false;
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x00003127 File Offset: 0x00001327
			public override bool MoveToNextAttribute()
			{
				return false;
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x0600066D RID: 1645 RVA: 0x0001BF7B File Offset: 0x0001A17B
			public override string Name
			{
				get
				{
					if (this.readState != ReadState.Interactive)
					{
						return this.parentReader.Name;
					}
					return string.Empty;
				}
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001BF97 File Offset: 0x0001A197
			public override string NamespaceURI
			{
				get
				{
					if (this.readState != ReadState.Interactive)
					{
						return this.parentReader.NamespaceURI;
					}
					return string.Empty;
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001BFB3 File Offset: 0x0001A1B3
			public override XmlNameTable NameTable
			{
				get
				{
					return this.parentReader.NameTable;
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001BFC0 File Offset: 0x0001A1C0
			public override string Prefix
			{
				get
				{
					if (this.readState != ReadState.Interactive)
					{
						return this.parentReader.Prefix;
					}
					return string.Empty;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001BFDC File Offset: 0x0001A1DC
			public override char QuoteChar
			{
				get
				{
					return this.parentReader.QuoteChar;
				}
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x00003127 File Offset: 0x00001327
			public override bool ReadAttributeValue()
			{
				return false;
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x0001BFE9 File Offset: 0x0001A1E9
			public override string ReadInnerXml()
			{
				return this.ReadContentAsString();
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x0001BFE9 File Offset: 0x0001A1E9
			public override string ReadOuterXml()
			{
				return this.ReadContentAsString();
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000675 RID: 1653 RVA: 0x0001BFF1 File Offset: 0x0001A1F1
			public override ReadState ReadState
			{
				get
				{
					return this.readState;
				}
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x0000A8EE File Offset: 0x00008AEE
			public override void ResolveEntity()
			{
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000677 RID: 1655 RVA: 0x0001BFF9 File Offset: 0x0001A1F9
			public override XmlReaderSettings Settings
			{
				get
				{
					return this.parentReader.Settings;
				}
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x0001C006 File Offset: 0x0001A206
			public override void Skip()
			{
				this.Read();
			}

			// Token: 0x170000DA RID: 218
			public override string this[int index]
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170000DB RID: 219
			public override string this[string name]
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170000DC RID: 220
			public override string this[string name, string ns]
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x0600067C RID: 1660 RVA: 0x0001C00F File Offset: 0x0001A20F
			public override string XmlLang
			{
				get
				{
					return this.parentReader.XmlLang;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001C01C File Offset: 0x0001A21C
			public override XmlSpace XmlSpace
			{
				get
				{
					return this.parentReader.XmlSpace;
				}
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x0600067E RID: 1662 RVA: 0x0001C029 File Offset: 0x0001A229
			public override Type ValueType
			{
				get
				{
					if (this.readState != ReadState.Interactive)
					{
						return this.parentReader.ValueType;
					}
					return typeof(byte[]);
				}
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x0001C04A File Offset: 0x0001A24A
			bool IXmlLineInfo.HasLineInfo()
			{
				return ((IXmlLineInfo)this.parentReader).HasLineInfo();
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001C05C File Offset: 0x0001A25C
			int IXmlLineInfo.LineNumber
			{
				get
				{
					return ((IXmlLineInfo)this.parentReader).LineNumber;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001C06E File Offset: 0x0001A26E
			int IXmlLineInfo.LinePosition
			{
				get
				{
					return ((IXmlLineInfo)this.parentReader).LinePosition;
				}
			}

			// Token: 0x040002CC RID: 716
			private int chunkSize = 4096;

			// Token: 0x040002CD RID: 717
			private int bytesRemaining;

			// Token: 0x040002CE RID: 718
			private XmlMtomReader.MimePart part;

			// Token: 0x040002CF RID: 719
			private ReadState readState;

			// Token: 0x040002D0 RID: 720
			private XmlDictionaryReader parentReader;

			// Token: 0x040002D1 RID: 721
			private string stringValue;

			// Token: 0x040002D2 RID: 722
			private int stringOffset;

			// Token: 0x040002D3 RID: 723
			private XmlNodeType nodeType;

			// Token: 0x040002D4 RID: 724
			private MemoryStream binHexStream;

			// Token: 0x040002D5 RID: 725
			private byte[] valueBuffer;

			// Token: 0x040002D6 RID: 726
			private int valueOffset;

			// Token: 0x040002D7 RID: 727
			private int valueCount;

			// Token: 0x040002D8 RID: 728
			private bool finishedStream;
		}
	}
}
