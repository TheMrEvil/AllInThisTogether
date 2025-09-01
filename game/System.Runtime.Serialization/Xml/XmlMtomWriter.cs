using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000083 RID: 131
	internal class XmlMtomWriter : XmlDictionaryWriter, IXmlMtomWriterInitializer
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x0001DE27 File Offset: 0x0001C027
		public XmlMtomWriter()
		{
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001DE30 File Offset: 0x0001C030
		public void SetOutput(Stream stream, Encoding encoding, int maxSizeInBytes, string startInfo, string boundary, string startUri, bool writeMessageHeaders, bool ownsStream)
		{
			if (encoding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoding");
			}
			if (maxSizeInBytes < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("maxSizeInBytes", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			this.maxSizeInBytes = maxSizeInBytes;
			this.encoding = encoding;
			this.isUTF8 = XmlMtomWriter.IsUTF8Encoding(encoding);
			this.Initialize(stream, startInfo, boundary, startUri, writeMessageHeaders, ownsStream);
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001DE94 File Offset: 0x0001C094
		private XmlDictionaryWriter Writer
		{
			get
			{
				if (!this.IsInitialized)
				{
					this.Initialize();
				}
				return this.writer;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x0001DEAA File Offset: 0x0001C0AA
		private bool IsInitialized
		{
			get
			{
				return this.initialContentTypeForRootPart == null;
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
		private void Initialize(Stream stream, string startInfo, string boundary, string startUri, bool writeMessageHeaders, bool ownsStream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (startInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("startInfo");
			}
			if (boundary == null)
			{
				boundary = XmlMtomWriter.GetBoundaryString();
			}
			if (startUri == null)
			{
				startUri = XmlMtomWriter.GenerateUriForMimePart(0);
			}
			if (!MailBnfHelper.IsValidMimeBoundary(boundary))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("MIME boundary is invalid: '{0}'.", new object[]
				{
					boundary
				}), "boundary"));
			}
			this.ownsStream = ownsStream;
			this.isClosed = false;
			this.depth = 0;
			this.totalSizeOfMimeParts = 0;
			this.sizeOfBufferedBinaryData = 0;
			this.binaryDataChunks = null;
			this.contentType = null;
			this.contentTypeStream = null;
			this.contentID = startUri;
			if (this.mimeParts != null)
			{
				this.mimeParts.Clear();
			}
			this.mimeWriter = new MimeWriter(stream, boundary);
			this.initialContentTypeForRootPart = XmlMtomWriter.GetContentTypeForRootMimePart(this.encoding, startInfo);
			if (writeMessageHeaders)
			{
				this.initialContentTypeForMimeMessage = XmlMtomWriter.GetContentTypeForMimeMessage(boundary, startUri, startInfo);
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001DFAC File Offset: 0x0001C1AC
		private void Initialize()
		{
			if (this.isClosed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("The XmlWriter is closed.")));
			}
			if (this.initialContentTypeForRootPart != null)
			{
				if (this.initialContentTypeForMimeMessage != null)
				{
					this.mimeWriter.StartPreface();
					this.mimeWriter.WriteHeader(MimeGlobals.MimeVersionHeader, MimeGlobals.DefaultVersion);
					this.mimeWriter.WriteHeader(MimeGlobals.ContentTypeHeader, this.initialContentTypeForMimeMessage);
					this.initialContentTypeForMimeMessage = null;
				}
				this.WriteMimeHeaders(this.contentID, this.initialContentTypeForRootPart, this.isUTF8 ? MimeGlobals.Encoding8bit : MimeGlobals.EncodingBinary);
				Stream contentStream = this.mimeWriter.GetContentStream();
				IXmlTextWriterInitializer xmlTextWriterInitializer = this.writer as IXmlTextWriterInitializer;
				if (xmlTextWriterInitializer == null)
				{
					this.writer = XmlDictionaryWriter.CreateTextWriter(contentStream, this.encoding, this.ownsStream);
				}
				else
				{
					xmlTextWriterInitializer.SetOutput(contentStream, this.encoding, this.ownsStream);
				}
				this.contentID = null;
				this.initialContentTypeForRootPart = null;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001E0A2 File Offset: 0x0001C2A2
		private static string GetBoundaryString()
		{
			return XmlMtomWriter.MimeBoundaryGenerator.Next();
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001E0A9 File Offset: 0x0001C2A9
		internal static bool IsUTF8Encoding(Encoding encoding)
		{
			return encoding.WebName == "utf-8";
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001E0BC File Offset: 0x0001C2BC
		private static string GetContentTypeForMimeMessage(string boundary, string startUri, string startInfo)
		{
			StringBuilder stringBuilder = new StringBuilder(string.Format(CultureInfo.InvariantCulture, "{0}/{1};{2}=\"{3}\";{4}=\"{5}\"", new object[]
			{
				MtomGlobals.MediaType,
				MtomGlobals.MediaSubtype,
				MtomGlobals.TypeParam,
				MtomGlobals.XopType,
				MtomGlobals.BoundaryParam,
				boundary
			}));
			if (startUri != null && startUri.Length > 0)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, ";{0}=\"<{1}>\"", MtomGlobals.StartParam, startUri);
			}
			if (startInfo != null && startInfo.Length > 0)
			{
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, ";{0}=\"{1}\"", MtomGlobals.StartInfoParam, startInfo);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001E15C File Offset: 0x0001C35C
		private static string GetContentTypeForRootMimePart(Encoding encoding, string startInfo)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0};{1}={2}", MtomGlobals.XopType, MtomGlobals.CharsetParam, XmlMtomWriter.CharSet(encoding));
			if (startInfo != null)
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0};{1}=\"{2}\"", text, MtomGlobals.TypeParam, startInfo);
			}
			return text;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		private static string CharSet(Encoding enc)
		{
			string webName = enc.WebName;
			if (string.Compare(webName, Encoding.UTF8.WebName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return webName;
			}
			if (string.Compare(webName, Encoding.Unicode.WebName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "utf-16LE";
			}
			if (string.Compare(webName, Encoding.BigEndianUnicode.WebName, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return "utf-16BE";
			}
			return webName;
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001E200 File Offset: 0x0001C400
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.WriteBase64InlineIfPresent();
			this.ThrowIfElementIsXOPInclude(prefix, localName, ns);
			this.Writer.WriteStartElement(prefix, localName, ns);
			this.depth++;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001E230 File Offset: 0x0001C430
		public override void WriteStartElement(string prefix, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			if (localName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("localName");
			}
			this.WriteBase64InlineIfPresent();
			this.ThrowIfElementIsXOPInclude(prefix, localName.Value, (ns == null) ? null : ns.Value);
			this.Writer.WriteStartElement(prefix, localName, ns);
			this.depth++;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001E288 File Offset: 0x0001C488
		private void ThrowIfElementIsXOPInclude(string prefix, string localName, string ns)
		{
			if (ns == null)
			{
				XmlBaseWriter xmlBaseWriter = this.Writer as XmlBaseWriter;
				if (xmlBaseWriter != null)
				{
					ns = xmlBaseWriter.LookupNamespace(prefix);
				}
			}
			if (localName == MtomGlobals.XopIncludeLocalName && ns == MtomGlobals.XopIncludeNamespace)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM data must not contain xop:Include element. '{0}' element in '{1}' namespace.", new object[]
				{
					MtomGlobals.XopIncludeLocalName,
					MtomGlobals.XopIncludeNamespace
				})));
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001E2F5 File Offset: 0x0001C4F5
		public override void WriteEndElement()
		{
			this.WriteXOPInclude();
			this.Writer.WriteEndElement();
			this.depth--;
			this.WriteXOPBinaryParts();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001E31C File Offset: 0x0001C51C
		public override void WriteFullEndElement()
		{
			this.WriteXOPInclude();
			this.Writer.WriteFullEndElement();
			this.depth--;
			this.WriteXOPBinaryParts();
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001E344 File Offset: 0x0001C544
		public override void WriteValue(IStreamProvider value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
			}
			if (this.Writer.WriteState == WriteState.Element)
			{
				if (this.binaryDataChunks == null)
				{
					this.binaryDataChunks = new List<MtomBinaryData>();
					this.contentID = XmlMtomWriter.GenerateUriForMimePart((this.mimeParts == null) ? 1 : (this.mimeParts.Count + 1));
				}
				this.binaryDataChunks.Add(new MtomBinaryData(value));
				return;
			}
			this.Writer.WriteValue(value);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			if (this.Writer.WriteState != WriteState.Element)
			{
				this.Writer.WriteBase64(buffer, index, count);
				return;
			}
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (index < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("index", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > buffer.Length - index)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					buffer.Length - index
				})));
			}
			if (this.binaryDataChunks == null)
			{
				this.binaryDataChunks = new List<MtomBinaryData>();
				this.contentID = XmlMtomWriter.GenerateUriForMimePart((this.mimeParts == null) ? 1 : (this.mimeParts.Count + 1));
			}
			int num = XmlMtomWriter.ValidateSizeOfMessage(this.maxSizeInBytes, 0, this.totalSizeOfMimeParts);
			num += XmlMtomWriter.ValidateSizeOfMessage(this.maxSizeInBytes, num, this.sizeOfBufferedBinaryData);
			num += XmlMtomWriter.ValidateSizeOfMessage(this.maxSizeInBytes, num, count);
			this.sizeOfBufferedBinaryData += count;
			this.binaryDataChunks.Add(new MtomBinaryData(buffer, index, count));
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001E508 File Offset: 0x0001C708
		internal static int ValidateSizeOfMessage(int maxSize, int offset, int size)
		{
			if (size > maxSize - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("MTOM exceeded max size in bytes. The maximum size is {0}.", new object[]
				{
					maxSize
				})));
			}
			return size;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001E535 File Offset: 0x0001C735
		private void WriteBase64InlineIfPresent()
		{
			if (this.binaryDataChunks != null)
			{
				this.WriteBase64Inline();
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001E548 File Offset: 0x0001C748
		private void WriteBase64Inline()
		{
			foreach (MtomBinaryData mtomBinaryData in this.binaryDataChunks)
			{
				if (mtomBinaryData.type == MtomBinaryDataType.Provider)
				{
					this.Writer.WriteValue(mtomBinaryData.provider);
				}
				else
				{
					this.Writer.WriteBase64(mtomBinaryData.chunk, 0, mtomBinaryData.chunk.Length);
				}
			}
			this.sizeOfBufferedBinaryData = 0;
			this.binaryDataChunks = null;
			this.contentType = null;
			this.contentID = null;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001E5E0 File Offset: 0x0001C7E0
		private void WriteXOPInclude()
		{
			if (this.binaryDataChunks == null)
			{
				return;
			}
			bool flag = true;
			long num = 0L;
			foreach (MtomBinaryData mtomBinaryData in this.binaryDataChunks)
			{
				long length = mtomBinaryData.Length;
				if (length < 0L || length > 767L - num)
				{
					flag = false;
					break;
				}
				num += length;
			}
			if (flag)
			{
				this.WriteBase64Inline();
				return;
			}
			if (this.mimeParts == null)
			{
				this.mimeParts = new List<XmlMtomWriter.MimePart>();
			}
			XmlMtomWriter.MimePart mimePart = new XmlMtomWriter.MimePart(this.binaryDataChunks, this.contentID, this.contentType, MimeGlobals.EncodingBinary, this.sizeOfBufferedBinaryData, this.maxSizeInBytes);
			this.mimeParts.Add(mimePart);
			this.totalSizeOfMimeParts += XmlMtomWriter.ValidateSizeOfMessage(this.maxSizeInBytes, this.totalSizeOfMimeParts, mimePart.sizeInBytes);
			this.totalSizeOfMimeParts += XmlMtomWriter.ValidateSizeOfMessage(this.maxSizeInBytes, this.totalSizeOfMimeParts, this.mimeWriter.GetBoundarySize());
			this.Writer.WriteStartElement(MtomGlobals.XopIncludePrefix, MtomGlobals.XopIncludeLocalName, MtomGlobals.XopIncludeNamespace);
			this.Writer.WriteStartAttribute(MtomGlobals.XopIncludeHrefLocalName, MtomGlobals.XopIncludeHrefNamespace);
			this.Writer.WriteValue(string.Format(CultureInfo.InvariantCulture, "{0}{1}", MimeGlobals.ContentIDScheme, this.contentID));
			this.Writer.WriteEndAttribute();
			this.Writer.WriteEndElement();
			this.binaryDataChunks = null;
			this.sizeOfBufferedBinaryData = 0;
			this.contentType = null;
			this.contentID = null;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001E77C File Offset: 0x0001C97C
		public static string GenerateUriForMimePart(int index)
		{
			return string.Format(CultureInfo.InvariantCulture, "http://tempuri.org/{0}/{1}", index, DateTime.Now.Ticks);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		private void WriteXOPBinaryParts()
		{
			if (this.depth > 0 || this.mimeWriter.WriteState == MimeWriterState.Closed)
			{
				return;
			}
			if (this.Writer.WriteState != WriteState.Closed)
			{
				this.Writer.Flush();
			}
			if (this.mimeParts != null)
			{
				foreach (XmlMtomWriter.MimePart mimePart in this.mimeParts)
				{
					this.WriteMimeHeaders(mimePart.contentID, mimePart.contentType, mimePart.contentTransferEncoding);
					Stream contentStream = this.mimeWriter.GetContentStream();
					int num = 256;
					byte[] buffer = new byte[num];
					foreach (MtomBinaryData mtomBinaryData in mimePart.binaryData)
					{
						if (mtomBinaryData.type == MtomBinaryDataType.Provider)
						{
							Stream stream = mtomBinaryData.provider.GetStream();
							if (stream == null)
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(System.Runtime.Serialization.SR.GetString("Stream returned by IStreamProvider cannot be null.")));
							}
							for (;;)
							{
								int num2 = stream.Read(buffer, 0, num);
								if (num2 <= 0)
								{
									break;
								}
								contentStream.Write(buffer, 0, num2);
								if (num < 65536 && num2 == num)
								{
									num *= 16;
									buffer = new byte[num];
								}
							}
							mtomBinaryData.provider.ReleaseStream(stream);
						}
						else
						{
							contentStream.Write(mtomBinaryData.chunk, 0, mtomBinaryData.chunk.Length);
						}
					}
				}
				this.mimeParts.Clear();
			}
			this.mimeWriter.Close();
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001E978 File Offset: 0x0001CB78
		private void WriteMimeHeaders(string contentID, string contentType, string contentTransferEncoding)
		{
			this.mimeWriter.StartPart();
			if (contentID != null)
			{
				this.mimeWriter.WriteHeader(MimeGlobals.ContentIDHeader, string.Format(CultureInfo.InvariantCulture, "<{0}>", contentID));
			}
			if (contentTransferEncoding != null)
			{
				this.mimeWriter.WriteHeader(MimeGlobals.ContentTransferEncodingHeader, contentTransferEncoding);
			}
			if (contentType != null)
			{
				this.mimeWriter.WriteHeader(MimeGlobals.ContentTypeHeader, contentType);
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001E9DC File Offset: 0x0001CBDC
		public override void Close()
		{
			if (!this.isClosed)
			{
				this.isClosed = true;
				if (this.IsInitialized)
				{
					this.WriteXOPInclude();
					if (this.Writer.WriteState == WriteState.Element || this.Writer.WriteState == WriteState.Attribute || this.Writer.WriteState == WriteState.Content)
					{
						this.Writer.WriteEndDocument();
					}
					this.Writer.Flush();
					this.depth = 0;
					this.WriteXOPBinaryParts();
					this.Writer.Close();
				}
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001EA60 File Offset: 0x0001CC60
		private void CheckIfStartContentTypeAttribute(string localName, string ns)
		{
			if (localName != null && localName == MtomGlobals.MimeContentTypeLocalName && ns != null && (ns == MtomGlobals.MimeContentTypeNamespace200406 || ns == MtomGlobals.MimeContentTypeNamespace200505))
			{
				this.contentTypeStream = new MemoryStream();
				this.infosetWriter = this.Writer;
				this.writer = XmlDictionaryWriter.CreateBinaryWriter(this.contentTypeStream);
				this.Writer.WriteStartElement("Wrapper");
				this.Writer.WriteStartAttribute(localName, ns);
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		private void CheckIfEndContentTypeAttribute()
		{
			if (this.contentTypeStream != null)
			{
				this.Writer.WriteEndAttribute();
				this.Writer.WriteEndElement();
				this.Writer.Flush();
				this.contentTypeStream.Position = 0L;
				XmlReader xmlReader = XmlDictionaryReader.CreateBinaryReader(this.contentTypeStream, null, XmlDictionaryReaderQuotas.Max, null, null);
				while (xmlReader.Read())
				{
					if (xmlReader.IsStartElement("Wrapper"))
					{
						this.contentType = xmlReader.GetAttribute(MtomGlobals.MimeContentTypeLocalName, MtomGlobals.MimeContentTypeNamespace200406);
						if (this.contentType == null)
						{
							this.contentType = xmlReader.GetAttribute(MtomGlobals.MimeContentTypeLocalName, MtomGlobals.MimeContentTypeNamespace200505);
							break;
						}
						break;
					}
				}
				this.writer = this.infosetWriter;
				this.infosetWriter = null;
				this.contentTypeStream = null;
				if (this.contentType != null)
				{
					this.Writer.WriteString(this.contentType);
				}
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001EBBA File Offset: 0x0001CDBA
		public override void Flush()
		{
			if (this.IsInitialized)
			{
				this.Writer.Flush();
			}
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001EBCF File Offset: 0x0001CDCF
		public override string LookupPrefix(string ns)
		{
			return this.Writer.LookupPrefix(ns);
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001EBDD File Offset: 0x0001CDDD
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.Writer.Settings;
			}
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001EBEA File Offset: 0x0001CDEA
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			this.Writer.WriteAttributes(reader, defattr);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001EBF9 File Offset: 0x0001CDF9
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteBinHex(buffer, index, count);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001EC0F File Offset: 0x0001CE0F
		public override void WriteCData(string text)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteCData(text);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001EC23 File Offset: 0x0001CE23
		public override void WriteCharEntity(char ch)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteCharEntity(ch);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001EC37 File Offset: 0x0001CE37
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteChars(buffer, index, count);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001EC4D File Offset: 0x0001CE4D
		public override void WriteComment(string text)
		{
			if (this.depth == 0 && this.mimeWriter.WriteState == MimeWriterState.Closed)
			{
				return;
			}
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteComment(text);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001EC78 File Offset: 0x0001CE78
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001EC90 File Offset: 0x0001CE90
		public override void WriteEndAttribute()
		{
			this.CheckIfEndContentTypeAttribute();
			this.Writer.WriteEndAttribute();
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001ECA3 File Offset: 0x0001CEA3
		public override void WriteEndDocument()
		{
			this.WriteXOPInclude();
			this.Writer.WriteEndDocument();
			this.depth = 0;
			this.WriteXOPBinaryParts();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001ECC3 File Offset: 0x0001CEC3
		public override void WriteEntityRef(string name)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteEntityRef(name);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001ECD7 File Offset: 0x0001CED7
		public override void WriteName(string name)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteName(name);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001ECEB File Offset: 0x0001CEEB
		public override void WriteNmToken(string name)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteNmToken(name);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001ED00 File Offset: 0x0001CF00
		protected override void WriteTextNode(XmlDictionaryReader reader, bool attribute)
		{
			Type valueType = reader.ValueType;
			if (valueType == typeof(string))
			{
				if (reader.CanReadValueChunk)
				{
					if (this.chars == null)
					{
						this.chars = new char[256];
					}
					int count;
					while ((count = reader.ReadValueChunk(this.chars, 0, this.chars.Length)) > 0)
					{
						this.WriteChars(this.chars, 0, count);
					}
				}
				else
				{
					this.WriteString(reader.Value);
				}
				if (!attribute)
				{
					reader.Read();
					return;
				}
			}
			else if (valueType == typeof(byte[]))
			{
				if (reader.CanReadBinaryContent)
				{
					if (this.bytes == null)
					{
						this.bytes = new byte[384];
					}
					int count2;
					while ((count2 = reader.ReadValueAsBase64(this.bytes, 0, this.bytes.Length)) > 0)
					{
						this.WriteBase64(this.bytes, 0, count2);
					}
				}
				else
				{
					this.WriteString(reader.Value);
				}
				if (!attribute)
				{
					reader.Read();
					return;
				}
			}
			else
			{
				base.WriteTextNode(reader, attribute);
			}
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001EE09 File Offset: 0x0001D009
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteNode(navigator, defattr);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001EE1E File Offset: 0x0001D01E
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001EE33 File Offset: 0x0001D033
		public override void WriteQualifiedName(string localName, string namespaceUri)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteQualifiedName(localName, namespaceUri);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001EE48 File Offset: 0x0001D048
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001EE5E File Offset: 0x0001D05E
		public override void WriteRaw(string data)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteRaw(data);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001EE72 File Offset: 0x0001D072
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.Writer.WriteStartAttribute(prefix, localName, ns);
			this.CheckIfStartContentTypeAttribute(localName, ns);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001EE8A File Offset: 0x0001D08A
		public override void WriteStartAttribute(string prefix, XmlDictionaryString localName, XmlDictionaryString ns)
		{
			this.Writer.WriteStartAttribute(prefix, localName, ns);
			if (localName != null && ns != null)
			{
				this.CheckIfStartContentTypeAttribute(localName.Value, ns.Value);
			}
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001EEB2 File Offset: 0x0001D0B2
		public override void WriteStartDocument()
		{
			this.Writer.WriteStartDocument();
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001EEBF File Offset: 0x0001D0BF
		public override void WriteStartDocument(bool standalone)
		{
			this.Writer.WriteStartDocument(standalone);
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x0001EECD File Offset: 0x0001D0CD
		public override WriteState WriteState
		{
			get
			{
				return this.Writer.WriteState;
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001EEDA File Offset: 0x0001D0DA
		public override void WriteString(string text)
		{
			if (this.depth == 0 && this.mimeWriter.WriteState == MimeWriterState.Closed && XmlConverter.IsWhitespace(text))
			{
				return;
			}
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteString(text);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001EF0D File Offset: 0x0001D10D
		public override void WriteString(XmlDictionaryString value)
		{
			if (this.depth == 0 && this.mimeWriter.WriteState == MimeWriterState.Closed && XmlConverter.IsWhitespace(value.Value))
			{
				return;
			}
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteString(value);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001EF45 File Offset: 0x0001D145
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001EF5A File Offset: 0x0001D15A
		public override void WriteWhitespace(string whitespace)
		{
			if (this.depth == 0 && this.mimeWriter.WriteState == MimeWriterState.Closed)
			{
				return;
			}
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteWhitespace(whitespace);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001EF88 File Offset: 0x0001D188
		public override void WriteValue(object value)
		{
			IStreamProvider streamProvider = value as IStreamProvider;
			if (streamProvider != null)
			{
				this.WriteValue(streamProvider);
				return;
			}
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteValue(value);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001EFB9 File Offset: 0x0001D1B9
		public override void WriteValue(string value)
		{
			if (this.depth == 0 && this.mimeWriter.WriteState == MimeWriterState.Closed && XmlConverter.IsWhitespace(value))
			{
				return;
			}
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteValue(value);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001EFEC File Offset: 0x0001D1EC
		public override void WriteValue(bool value)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteValue(value);
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001F000 File Offset: 0x0001D200
		public override void WriteValue(DateTime value)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteValue(value);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001F014 File Offset: 0x0001D214
		public override void WriteValue(double value)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteValue(value);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001F028 File Offset: 0x0001D228
		public override void WriteValue(int value)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteValue(value);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001F03C File Offset: 0x0001D23C
		public override void WriteValue(long value)
		{
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteValue(value);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001F050 File Offset: 0x0001D250
		public override void WriteValue(XmlDictionaryString value)
		{
			if (this.depth == 0 && this.mimeWriter.WriteState == MimeWriterState.Closed && XmlConverter.IsWhitespace(value.Value))
			{
				return;
			}
			this.WriteBase64InlineIfPresent();
			this.Writer.WriteValue(value);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001F088 File Offset: 0x0001D288
		public override void WriteXmlnsAttribute(string prefix, string ns)
		{
			this.Writer.WriteXmlnsAttribute(prefix, ns);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001F097 File Offset: 0x0001D297
		public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns)
		{
			this.Writer.WriteXmlnsAttribute(prefix, ns);
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001F0A6 File Offset: 0x0001D2A6
		public override string XmlLang
		{
			get
			{
				return this.Writer.XmlLang;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001F0B3 File Offset: 0x0001D2B3
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.Writer.XmlSpace;
			}
		}

		// Token: 0x0400031E RID: 798
		private const int MaxInlinedBytes = 767;

		// Token: 0x0400031F RID: 799
		private int maxSizeInBytes;

		// Token: 0x04000320 RID: 800
		private XmlDictionaryWriter writer;

		// Token: 0x04000321 RID: 801
		private XmlDictionaryWriter infosetWriter;

		// Token: 0x04000322 RID: 802
		private MimeWriter mimeWriter;

		// Token: 0x04000323 RID: 803
		private Encoding encoding;

		// Token: 0x04000324 RID: 804
		private bool isUTF8;

		// Token: 0x04000325 RID: 805
		private string contentID;

		// Token: 0x04000326 RID: 806
		private string contentType;

		// Token: 0x04000327 RID: 807
		private string initialContentTypeForRootPart;

		// Token: 0x04000328 RID: 808
		private string initialContentTypeForMimeMessage;

		// Token: 0x04000329 RID: 809
		private MemoryStream contentTypeStream;

		// Token: 0x0400032A RID: 810
		private List<XmlMtomWriter.MimePart> mimeParts;

		// Token: 0x0400032B RID: 811
		private IList<MtomBinaryData> binaryDataChunks;

		// Token: 0x0400032C RID: 812
		private int depth;

		// Token: 0x0400032D RID: 813
		private int totalSizeOfMimeParts;

		// Token: 0x0400032E RID: 814
		private int sizeOfBufferedBinaryData;

		// Token: 0x0400032F RID: 815
		private char[] chars;

		// Token: 0x04000330 RID: 816
		private byte[] bytes;

		// Token: 0x04000331 RID: 817
		private bool isClosed;

		// Token: 0x04000332 RID: 818
		private bool ownsStream;

		// Token: 0x02000084 RID: 132
		private static class MimeBoundaryGenerator
		{
			// Token: 0x06000730 RID: 1840 RVA: 0x0001F0C0 File Offset: 0x0001D2C0
			static MimeBoundaryGenerator()
			{
			}

			// Token: 0x06000731 RID: 1841 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
			internal static string Next()
			{
				long num = Interlocked.Increment(ref XmlMtomWriter.MimeBoundaryGenerator.id);
				return string.Format(CultureInfo.InvariantCulture, "{0}{1}", XmlMtomWriter.MimeBoundaryGenerator.prefix, num);
			}

			// Token: 0x04000333 RID: 819
			private static long id;

			// Token: 0x04000334 RID: 820
			private static string prefix = Guid.NewGuid().ToString() + "+id=";
		}

		// Token: 0x02000085 RID: 133
		private class MimePart
		{
			// Token: 0x06000732 RID: 1842 RVA: 0x0001F124 File Offset: 0x0001D324
			internal MimePart(IList<MtomBinaryData> binaryData, string contentID, string contentType, string contentTransferEncoding, int sizeOfBufferedBinaryData, int maxSizeInBytes)
			{
				this.binaryData = binaryData;
				this.contentID = contentID;
				this.contentType = (contentType ?? MtomGlobals.DefaultContentTypeForBinary);
				this.contentTransferEncoding = contentTransferEncoding;
				this.sizeInBytes = XmlMtomWriter.MimePart.GetSize(contentID, contentType, contentTransferEncoding, sizeOfBufferedBinaryData, maxSizeInBytes);
			}

			// Token: 0x06000733 RID: 1843 RVA: 0x0001F170 File Offset: 0x0001D370
			private static int GetSize(string contentID, string contentType, string contentTransferEncoding, int sizeOfBufferedBinaryData, int maxSizeInBytes)
			{
				int num = XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, 0, MimeGlobals.CRLF.Length * 3);
				if (contentTransferEncoding != null)
				{
					num += XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, num, MimeWriter.GetHeaderSize(MimeGlobals.ContentTransferEncodingHeader, contentTransferEncoding, maxSizeInBytes));
				}
				if (contentType != null)
				{
					num += XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, num, MimeWriter.GetHeaderSize(MimeGlobals.ContentTypeHeader, contentType, maxSizeInBytes));
				}
				if (contentID != null)
				{
					num += XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, num, MimeWriter.GetHeaderSize(MimeGlobals.ContentIDHeader, contentID, maxSizeInBytes));
					num += XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, num, 2);
				}
				return num + XmlMtomWriter.ValidateSizeOfMessage(maxSizeInBytes, num, sizeOfBufferedBinaryData);
			}

			// Token: 0x04000335 RID: 821
			internal IList<MtomBinaryData> binaryData;

			// Token: 0x04000336 RID: 822
			internal string contentID;

			// Token: 0x04000337 RID: 823
			internal string contentType;

			// Token: 0x04000338 RID: 824
			internal string contentTransferEncoding;

			// Token: 0x04000339 RID: 825
			internal int sizeInBytes;
		}
	}
}
