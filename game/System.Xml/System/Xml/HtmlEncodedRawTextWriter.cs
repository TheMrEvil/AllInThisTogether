using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000031 RID: 49
	internal class HtmlEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x06000177 RID: 375 RVA: 0x0000B506 File Offset: 0x00009706
		public HtmlEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000B517 File Offset: 0x00009717
		public HtmlEncodedRawTextWriter(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B52C File Offset: 0x0000972C
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			base.RawText("<!DOCTYPE ");
			if (name == "HTML")
			{
				base.RawText("HTML");
			}
			else
			{
				base.RawText("html");
			}
			int bufPos;
			if (pubid != null)
			{
				base.RawText(" PUBLIC \"");
				base.RawText(pubid);
				if (sysid != null)
				{
					base.RawText("\" \"");
					base.RawText(sysid);
				}
				char[] bufChars = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 34;
			}
			else if (sysid != null)
			{
				base.RawText(" SYSTEM \"");
				base.RawText(sysid);
				char[] bufChars2 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars2[bufPos] = 34;
			}
			else
			{
				char[] bufChars3 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars3[bufPos] = 32;
			}
			if (subset != null)
			{
				char[] bufChars4 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars4[bufPos] = 91;
				base.RawText(subset);
				char[] bufChars5 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars5[bufPos] = 93;
			}
			char[] bufChars6 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars6[bufPos] = 62;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B66C File Offset: 0x0000986C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				this.currentElementProperties = (ElementProperties)HtmlEncodedRawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 60;
				base.RawText(localName);
				this.attrEndPos = this.bufPos;
				return;
			}
			this.currentElementProperties = ElementProperties.HAS_NS;
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B700 File Offset: 0x00009900
		internal override void StartElementContent()
		{
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteMetaElement();
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B744 File Offset: 0x00009944
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					char[] bufChars = this.bufChars;
					int bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars[bufPos] = 60;
					char[] bufChars2 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars2[bufPos] = 47;
					base.RawText(localName);
					char[] bufChars3 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars3[bufPos] = 62;
				}
			}
			else
			{
				base.WriteEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B7EC File Offset: 0x000099EC
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					char[] bufChars = this.bufChars;
					int bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars[bufPos] = 60;
					char[] bufChars2 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars2[bufPos] = 47;
					base.RawText(localName);
					char[] bufChars3 = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars3[bufPos] = 62;
				}
			}
			else
			{
				base.WriteFullEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B894 File Offset: 0x00009A94
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				int bufPos;
				if (this.attrEndPos == this.bufPos)
				{
					char[] bufChars = this.bufChars;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufChars[bufPos] = 32;
				}
				base.RawText(localName);
				if ((this.currentElementProperties & (ElementProperties)7U) != ElementProperties.DEFAULT)
				{
					this.currentAttributeProperties = (AttributeProperties)((ElementProperties)HtmlEncodedRawTextWriter.attributePropertySearch.FindCaseInsensitiveString(localName) & this.currentElementProperties);
					if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
					{
						this.inAttributeValue = true;
						return;
					}
				}
				else
				{
					this.currentAttributeProperties = AttributeProperties.DEFAULT;
				}
				char[] bufChars2 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars2[bufPos] = 61;
				char[] bufChars3 = this.bufChars;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars3[bufPos] = 34;
			}
			else
			{
				base.WriteStartAttribute(prefix, localName, ns);
				this.currentAttributeProperties = AttributeProperties.DEFAULT;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B97C File Offset: 0x00009B7C
		public override void WriteEndAttribute()
		{
			if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
			{
				this.attrEndPos = this.bufPos;
			}
			else
			{
				if (this.endsWithAmpersand)
				{
					this.OutputRestAmps();
					this.endsWithAmpersand = false;
				}
				if (this.trackTextContent && this.inTextContent)
				{
					base.ChangeTextContentMark(false);
				}
				char[] bufChars = this.bufChars;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufChars[bufPos] = 34;
			}
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000B9FC File Offset: 0x00009BFC
		public override void WriteProcessingInstruction(string target, string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				base.ChangeTextContentMark(false);
			}
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 60;
			char[] bufChars2 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars2[bufPos] = 63;
			base.RawText(target);
			char[] bufChars3 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars3[bufPos] = 32;
			base.WriteCommentOrPi(text, 63);
			char[] bufChars4 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars4[bufPos] = 62;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000BAAC File Offset: 0x00009CAC
		public unsafe override void WriteString(string text)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				base.ChangeTextContentMark(true);
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* pSrcEnd = ptr + text.Length;
				if (this.inAttributeValue)
				{
					this.WriteHtmlAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteHtmlElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteEntityRef(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteCharEntity(char ch)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BB1C File Offset: 0x00009D1C
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				base.ChangeTextContentMark(true);
			}
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				if (this.inAttributeValue)
				{
					base.WriteAttributeTextBlock(ptr2, ptr2 + count);
				}
				else
				{
					base.WriteElementTextBlock(ptr2, ptr2 + count);
				}
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BB74 File Offset: 0x00009D74
		private void Init(XmlWriterSettings settings)
		{
			if (HtmlEncodedRawTextWriter.elementPropertySearch == null)
			{
				HtmlEncodedRawTextWriter.attributePropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlAttributes);
				HtmlEncodedRawTextWriter.elementPropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlElements);
			}
			this.elementScope = new ByteStack(10);
			this.uriEscapingBuffer = new byte[5];
			this.currentElementProperties = ElementProperties.DEFAULT;
			this.mediaType = settings.MediaType;
			this.doNotEscapeUriAttributes = settings.DoNotEscapeUriAttributes;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		protected void WriteMetaElement()
		{
			base.RawText("<META http-equiv=\"Content-Type\"");
			if (this.mediaType == null)
			{
				this.mediaType = "text/html";
			}
			base.RawText(" content=\"");
			base.RawText(this.mediaType);
			base.RawText("; charset=");
			base.RawText(this.encoding.WebName);
			base.RawText("\">");
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BC49 File Offset: 0x00009E49
		protected unsafe void WriteHtmlElementTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentElementProperties & ElementProperties.NO_ENTITIES) != ElementProperties.DEFAULT)
			{
				base.RawText(pSrc, pSrcEnd);
				return;
			}
			base.WriteElementTextBlock(pSrc, pSrcEnd);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BC68 File Offset: 0x00009E68
		protected unsafe void WriteHtmlAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentAttributeProperties & (AttributeProperties)7U) != AttributeProperties.DEFAULT)
			{
				if ((this.currentAttributeProperties & AttributeProperties.BOOLEAN) != AttributeProperties.DEFAULT)
				{
					return;
				}
				if ((this.currentAttributeProperties & (AttributeProperties)5U) != AttributeProperties.DEFAULT && !this.doNotEscapeUriAttributes)
				{
					this.WriteUriAttributeText(pSrc, pSrcEnd);
					return;
				}
				this.WriteHtmlAttributeText(pSrc, pSrcEnd);
				return;
			}
			else
			{
				if ((this.currentElementProperties & ElementProperties.HAS_NS) != ElementProperties.DEFAULT)
				{
					base.WriteAttributeTextBlock(pSrc, pSrcEnd);
					return;
				}
				this.WriteHtmlAttributeText(pSrc, pSrcEnd);
				return;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		private unsafe void WriteHtmlAttributeText(char* pSrc, char* pSrcEnd)
		{
			if (this.endsWithAmpersand)
			{
				if ((long)(pSrcEnd - pSrc) > 0L && *pSrc != '{')
				{
					this.OutputRestAmps();
				}
				this.endsWithAmpersand = false;
			}
			char[] array;
			char* ptr;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[(int)(c = *pSrc)] & 128) != 0)
				{
					*(ptr2++) = c;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							goto IL_13B;
						case '\n':
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_166;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_166;
						default:
							if (c == '"')
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_166;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
									goto IL_166;
								}
								*(ptr2++) = c;
								goto IL_166;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_13B;
					}
					base.EncodeChar(ref pSrc, pSrcEnd, ref ptr2);
					continue;
					IL_166:
					pSrc++;
					continue;
					IL_13B:
					*(ptr2++) = c;
					goto IL_166;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BE5C File Offset: 0x0000A05C
		private unsafe void WriteUriAttributeText(char* pSrc, char* pSrcEnd)
		{
			if (this.endsWithAmpersand)
			{
				if ((long)(pSrcEnd - pSrc) > 0L && *pSrc != '{')
				{
					this.OutputRestAmps();
				}
				this.endsWithAmpersand = false;
			}
			char[] array;
			char* ptr;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			char* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[(int)(c = *pSrc)] & 128) != 0 && c < '\u0080')
				{
					*(ptr2++) = c;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (c <= '&')
					{
						switch (c)
						{
						case '\t':
							goto IL_14F;
						case '\n':
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_1ED;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_1ED;
						default:
							if (c == '"')
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_1ED;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
									goto IL_1ED;
								}
								*(ptr2++) = c;
								goto IL_1ED;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_14F;
					}
					byte[] array2;
					byte* ptr4;
					if ((array2 = this.uriEscapingBuffer) == null || array2.Length == 0)
					{
						ptr4 = null;
					}
					else
					{
						ptr4 = &array2[0];
					}
					byte* ptr5 = ptr4;
					byte* ptr6 = ptr5;
					XmlUtf8RawTextWriter.CharToUTF8(ref pSrc, pSrcEnd, ref ptr6);
					while (ptr5 < ptr6)
					{
						*(ptr2++) = '%';
						*(ptr2++) = "0123456789ABCDEF"[*ptr5 >> 4];
						*(ptr2++) = "0123456789ABCDEF"[(int)(*ptr5 & 15)];
						ptr5++;
					}
					array2 = null;
					continue;
					IL_1ED:
					pSrc++;
					continue;
					IL_14F:
					*(ptr2++) = c;
					goto IL_1ED;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C070 File Offset: 0x0000A270
		private void OutputRestAmps()
		{
			char[] bufChars = this.bufChars;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars[bufPos] = 97;
			char[] bufChars2 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars2[bufPos] = 109;
			char[] bufChars3 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars3[bufPos] = 112;
			char[] bufChars4 = this.bufChars;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufChars4[bufPos] = 59;
		}

		// Token: 0x040005EA RID: 1514
		protected ByteStack elementScope;

		// Token: 0x040005EB RID: 1515
		protected ElementProperties currentElementProperties;

		// Token: 0x040005EC RID: 1516
		private AttributeProperties currentAttributeProperties;

		// Token: 0x040005ED RID: 1517
		private bool endsWithAmpersand;

		// Token: 0x040005EE RID: 1518
		private byte[] uriEscapingBuffer;

		// Token: 0x040005EF RID: 1519
		private string mediaType;

		// Token: 0x040005F0 RID: 1520
		private bool doNotEscapeUriAttributes;

		// Token: 0x040005F1 RID: 1521
		protected static TernaryTreeReadOnly elementPropertySearch;

		// Token: 0x040005F2 RID: 1522
		protected static TernaryTreeReadOnly attributePropertySearch;

		// Token: 0x040005F3 RID: 1523
		private const int StackIncrement = 10;
	}
}
