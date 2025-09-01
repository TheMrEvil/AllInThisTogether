using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x02000034 RID: 52
	internal class HtmlUtf8RawTextWriter : XmlUtf8RawTextWriter
	{
		// Token: 0x0600019B RID: 411 RVA: 0x0000C414 File Offset: 0x0000A614
		public HtmlUtf8RawTextWriter(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
			this.Init(settings);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C428 File Offset: 0x0000A628
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
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
				byte[] bufBytes = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes[bufPos] = 34;
			}
			else if (sysid != null)
			{
				base.RawText(" SYSTEM \"");
				base.RawText(sysid);
				byte[] bufBytes2 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes2[bufPos] = 34;
			}
			else
			{
				byte[] bufBytes3 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes3[bufPos] = 32;
			}
			if (subset != null)
			{
				byte[] bufBytes4 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes4[bufPos] = 91;
				base.RawText(subset);
				byte[] bufBytes5 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes5[bufPos] = 93;
			}
			byte[] bufBytes6 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes6[bufPos] = 62;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000C550 File Offset: 0x0000A750
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.elementScope.Push((byte)this.currentElementProperties);
			if (ns.Length == 0)
			{
				this.currentElementProperties = (ElementProperties)HtmlUtf8RawTextWriter.elementPropertySearch.FindCaseInsensitiveString(localName);
				byte[] bufBytes = this.bufBytes;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes[bufPos] = 60;
				base.RawText(localName);
				this.attrEndPos = this.bufPos;
				return;
			}
			this.currentElementProperties = ElementProperties.HAS_NS;
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000C5CC File Offset: 0x0000A7CC
		internal override void StartElementContent()
		{
			byte[] bufBytes = this.bufBytes;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes[bufPos] = 62;
			this.contentPos = this.bufPos;
			if ((this.currentElementProperties & ElementProperties.HEAD) != ElementProperties.DEFAULT)
			{
				this.WriteMetaElement();
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000C610 File Offset: 0x0000A810
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					byte[] bufBytes = this.bufBytes;
					int bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes[bufPos] = 60;
					byte[] bufBytes2 = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes2[bufPos] = 47;
					base.RawText(localName);
					byte[] bufBytes3 = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes3[bufPos] = 62;
				}
			}
			else
			{
				base.WriteEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000C6A0 File Offset: 0x0000A8A0
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				if ((this.currentElementProperties & ElementProperties.EMPTY) == ElementProperties.DEFAULT)
				{
					byte[] bufBytes = this.bufBytes;
					int bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes[bufPos] = 60;
					byte[] bufBytes2 = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes2[bufPos] = 47;
					base.RawText(localName);
					byte[] bufBytes3 = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes3[bufPos] = 62;
				}
			}
			else
			{
				base.WriteFullEndElement(prefix, localName, ns);
			}
			this.currentElementProperties = (ElementProperties)this.elementScope.Pop();
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000C730 File Offset: 0x0000A930
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0)
			{
				int bufPos;
				if (this.attrEndPos == this.bufPos)
				{
					byte[] bufBytes = this.bufBytes;
					bufPos = this.bufPos;
					this.bufPos = bufPos + 1;
					bufBytes[bufPos] = 32;
				}
				base.RawText(localName);
				if ((this.currentElementProperties & (ElementProperties)7U) != ElementProperties.DEFAULT)
				{
					this.currentAttributeProperties = (AttributeProperties)((ElementProperties)HtmlUtf8RawTextWriter.attributePropertySearch.FindCaseInsensitiveString(localName) & this.currentElementProperties);
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
				byte[] bufBytes2 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes2[bufPos] = 61;
				byte[] bufBytes3 = this.bufBytes;
				bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes3[bufPos] = 34;
			}
			else
			{
				base.WriteStartAttribute(prefix, localName, ns);
				this.currentAttributeProperties = AttributeProperties.DEFAULT;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000C800 File Offset: 0x0000AA00
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
				byte[] bufBytes = this.bufBytes;
				int bufPos = this.bufPos;
				this.bufPos = bufPos + 1;
				bufBytes[bufPos] = 34;
			}
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000C868 File Offset: 0x0000AA68
		public override void WriteProcessingInstruction(string target, string text)
		{
			byte[] bufBytes = this.bufBytes;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes[bufPos] = 60;
			byte[] bufBytes2 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes2[bufPos] = 63;
			base.RawText(target);
			byte[] bufBytes3 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes3[bufPos] = 32;
			base.WriteCommentOrPi(text, 63);
			byte[] bufBytes4 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes4[bufPos] = 62;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000C904 File Offset: 0x0000AB04
		public unsafe override void WriteString(string text)
		{
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

		// Token: 0x060001A7 RID: 423 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteEntityRef(string name)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteCharEntity(char ch)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			throw new InvalidOperationException(Res.GetString("Operation is not valid due to the current state of the object."));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000C94C File Offset: 0x0000AB4C
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
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

		// Token: 0x060001AB RID: 427 RVA: 0x0000C98C File Offset: 0x0000AB8C
		private void Init(XmlWriterSettings settings)
		{
			if (HtmlUtf8RawTextWriter.elementPropertySearch == null)
			{
				HtmlUtf8RawTextWriter.attributePropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlAttributes);
				HtmlUtf8RawTextWriter.elementPropertySearch = new TernaryTreeReadOnly(HtmlTernaryTree.htmlElements);
			}
			this.elementScope = new ByteStack(10);
			this.uriEscapingBuffer = new byte[5];
			this.currentElementProperties = ElementProperties.DEFAULT;
			this.mediaType = settings.MediaType;
			this.doNotEscapeUriAttributes = settings.DoNotEscapeUriAttributes;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
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

		// Token: 0x060001AD RID: 429 RVA: 0x0000CA61 File Offset: 0x0000AC61
		protected unsafe void WriteHtmlElementTextBlock(char* pSrc, char* pSrcEnd)
		{
			if ((this.currentElementProperties & ElementProperties.NO_ENTITIES) != ElementProperties.DEFAULT)
			{
				base.RawText(pSrc, pSrcEnd);
				return;
			}
			base.WriteElementTextBlock(pSrc, pSrcEnd);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000CA80 File Offset: 0x0000AC80
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

		// Token: 0x060001AF RID: 431 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
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
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[(int)(c = *pSrc)] & 128) != 0 && c <= '\u007f')
				{
					*(ptr2++) = (byte)c;
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
							goto IL_136;
						case '\n':
							ptr2 = XmlUtf8RawTextWriter.LineFeedEntity(ptr2);
							goto IL_162;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_162;
						default:
							if (c == '"')
							{
								ptr2 = XmlUtf8RawTextWriter.QuoteEntity(ptr2);
								goto IL_162;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
									goto IL_162;
								}
								*(ptr2++) = (byte)c;
								goto IL_162;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_136;
					}
					base.EncodeChar(ref pSrc, pSrcEnd, ref ptr2);
					continue;
					IL_162:
					pSrc++;
					continue;
					IL_136:
					*(ptr2++) = (byte)c;
					goto IL_162;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000CC70 File Offset: 0x0000AE70
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
			byte[] array;
			byte* ptr;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			byte* ptr2 = ptr + this.bufPos;
			char c = '\0';
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[(int)(c = *pSrc)] & 128) != 0 && c < '\u0080')
				{
					*(ptr2++) = (byte)c;
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
							goto IL_142;
						case '\n':
							ptr2 = XmlUtf8RawTextWriter.LineFeedEntity(ptr2);
							goto IL_1E3;
						case '\v':
						case '\f':
							break;
						case '\r':
							ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_1E3;
						default:
							if (c == '"')
							{
								ptr2 = XmlUtf8RawTextWriter.QuoteEntity(ptr2);
								goto IL_1E3;
							}
							if (c == '&')
							{
								if (pSrc + 1 == pSrcEnd)
								{
									this.endsWithAmpersand = true;
								}
								else if (pSrc[1] != '{')
								{
									ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
									goto IL_1E3;
								}
								*(ptr2++) = (byte)c;
								goto IL_1E3;
							}
							break;
						}
					}
					else if (c == '\'' || c == '<' || c == '>')
					{
						goto IL_142;
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
						*(ptr2++) = 37;
						*(ptr2++) = (byte)"0123456789ABCDEF"[*ptr5 >> 4];
						*(ptr2++) = (byte)"0123456789ABCDEF"[(int)(*ptr5 & 15)];
						ptr5++;
					}
					array2 = null;
					continue;
					IL_1E3:
					pSrc++;
					continue;
					IL_142:
					*(ptr2++) = (byte)c;
					goto IL_1E3;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000CE7C File Offset: 0x0000B07C
		private void OutputRestAmps()
		{
			byte[] bufBytes = this.bufBytes;
			int bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes[bufPos] = 97;
			byte[] bufBytes2 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes2[bufPos] = 109;
			byte[] bufBytes3 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes3[bufPos] = 112;
			byte[] bufBytes4 = this.bufBytes;
			bufPos = this.bufPos;
			this.bufPos = bufPos + 1;
			bufBytes4[bufPos] = 59;
		}

		// Token: 0x040005FA RID: 1530
		protected ByteStack elementScope;

		// Token: 0x040005FB RID: 1531
		protected ElementProperties currentElementProperties;

		// Token: 0x040005FC RID: 1532
		private AttributeProperties currentAttributeProperties;

		// Token: 0x040005FD RID: 1533
		private bool endsWithAmpersand;

		// Token: 0x040005FE RID: 1534
		private byte[] uriEscapingBuffer;

		// Token: 0x040005FF RID: 1535
		private string mediaType;

		// Token: 0x04000600 RID: 1536
		private bool doNotEscapeUriAttributes;

		// Token: 0x04000601 RID: 1537
		protected static TernaryTreeReadOnly elementPropertySearch;

		// Token: 0x04000602 RID: 1538
		protected static TernaryTreeReadOnly attributePropertySearch;

		// Token: 0x04000603 RID: 1539
		private const int StackIncrement = 10;
	}
}
