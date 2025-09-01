using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200011E RID: 286
	internal class XmlUtf8RawTextWriter : XmlRawWriter
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x00044EFC File Offset: 0x000430FC
		protected XmlUtf8RawTextWriter(XmlWriterSettings settings)
		{
			this.useAsync = settings.Async;
			this.newLineHandling = settings.NewLineHandling;
			this.omitXmlDeclaration = settings.OmitXmlDeclaration;
			this.newLineChars = settings.NewLineChars;
			this.checkCharacters = settings.CheckCharacters;
			this.closeOutput = settings.CloseOutput;
			this.standalone = settings.Standalone;
			this.outputMethod = settings.OutputMethod;
			this.mergeCDataSections = settings.MergeCDataSections;
			if (this.checkCharacters && this.newLineHandling == NewLineHandling.Replace)
			{
				this.ValidateContentChars(this.newLineChars, "NewLineChars", false);
			}
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00044FC4 File Offset: 0x000431C4
		public XmlUtf8RawTextWriter(Stream stream, XmlWriterSettings settings) : this(settings)
		{
			this.stream = stream;
			this.encoding = settings.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufBytes = new byte[this.bufLen + 32];
			if (!stream.CanSeek || stream.Position == 0L)
			{
				byte[] preamble = this.encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					Buffer.BlockCopy(preamble, 0, this.bufBytes, 1, preamble.Length);
					this.bufPos += preamble.Length;
					this.textPos += preamble.Length;
				}
			}
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00045080 File Offset: 0x00043280
		public override XmlWriterSettings Settings
		{
			get
			{
				return new XmlWriterSettings
				{
					Encoding = this.encoding,
					OmitXmlDeclaration = this.omitXmlDeclaration,
					NewLineHandling = this.newLineHandling,
					NewLineChars = this.newLineChars,
					CloseOutput = this.closeOutput,
					ConformanceLevel = ConformanceLevel.Auto,
					CheckCharacters = this.checkCharacters,
					AutoXmlDeclaration = this.autoXmlDeclaration,
					Standalone = this.standalone,
					OutputMethod = this.outputMethod,
					ReadOnly = true
				};
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0004510C File Offset: 0x0004330C
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				this.RawText("<?xml version=\"");
				this.RawText("1.0");
				if (this.encoding != null)
				{
					this.RawText("\" encoding=\"");
					this.RawText(this.encoding.WebName);
				}
				if (standalone != XmlStandalone.Omit)
				{
					this.RawText("\" standalone=\"");
					this.RawText((standalone == XmlStandalone.Yes) ? "yes" : "no");
				}
				this.RawText("\"?>");
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00045192 File Offset: 0x00043392
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				this.WriteProcessingInstruction("xml", xmldecl);
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x000451B0 File Offset: 0x000433B0
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.RawText("<!DOCTYPE ");
			this.RawText(name);
			int num;
			if (pubid != null)
			{
				this.RawText(" PUBLIC \"");
				this.RawText(pubid);
				this.RawText("\" \"");
				if (sysid != null)
				{
					this.RawText(sysid);
				}
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 34;
			}
			else if (sysid != null)
			{
				this.RawText(" SYSTEM \"");
				this.RawText(sysid);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			else
			{
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
			}
			if (subset != null)
			{
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 91;
				this.RawText(subset);
				byte[] array5 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 93;
			}
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 62;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000452BC File Offset: 0x000434BC
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00045324 File Offset: 0x00043524
		internal override void StartElementContent()
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 62;
			this.contentPos = this.bufPos;
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00045358 File Offset: 0x00043558
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			int num;
			if (this.contentPos != this.bufPos)
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				if (prefix != null && prefix.Length != 0)
				{
					this.RawText(prefix);
					byte[] array3 = this.bufBytes;
					num = this.bufPos;
					this.bufPos = num + 1;
					array3[num] = 58;
				}
				this.RawText(localName);
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 62;
				return;
			}
			this.bufPos--;
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 32;
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 47;
			byte[] array7 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00045454 File Offset: 0x00043654
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 58;
			}
			this.RawText(localName);
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 62;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x000454E4 File Offset: 0x000436E4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			int num;
			if (this.attrEndPos == this.bufPos)
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			if (prefix != null && prefix.Length > 0)
			{
				this.RawText(prefix);
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 61;
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00045588 File Offset: 0x00043788
		public override void WriteEndAttribute()
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x000153D1 File Offset: 0x000135D1
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(namespaceName);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000455C4 File Offset: 0x000437C4
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			if (prefix.Length == 0)
			{
				this.RawText(" xmlns=\"");
			}
			else
			{
				this.RawText(" xmlns:");
				this.RawText(prefix);
				byte[] array = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 61;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			this.inAttributeValue = true;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x00045634 File Offset: 0x00043834
		internal override void WriteEndNamespaceDeclaration()
		{
			this.inAttributeValue = false;
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00045670 File Offset: 0x00043870
		public override void WriteCData(string text)
		{
			int num;
			if (this.mergeCDataSections && this.bufPos == this.cdataPos)
			{
				this.bufPos -= 3;
			}
			else
			{
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 33;
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 91;
				byte[] array4 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 67;
				byte[] array5 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 68;
				byte[] array6 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array6[num] = 65;
				byte[] array7 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array7[num] = 84;
				byte[] array8 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array8[num] = 65;
				byte[] array9 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array9[num] = 91;
			}
			this.WriteCDataSection(text);
			byte[] array10 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array10[num] = 93;
			byte[] array11 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array11[num] = 93;
			byte[] array12 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array12[num] = 62;
			this.textPos = this.bufPos;
			this.cdataPos = this.bufPos;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00045800 File Offset: 0x00043A00
		public override void WriteComment(string text)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 33;
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 45;
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 45;
			this.WriteCommentOrPi(text, 45);
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 45;
			byte[] array6 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 45;
			byte[] array7 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x000458CC File Offset: 0x00043ACC
		public override void WriteProcessingInstruction(string name, string text)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 63;
			this.RawText(name);
			if (text.Length > 0)
			{
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
				this.WriteCommentOrPi(text, 63);
			}
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 63;
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 62;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00045974 File Offset: 0x00043B74
		public override void WriteEntityRef(string name)
		{
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			this.RawText(name);
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x000459DC File Offset: 0x00043BDC
		public override void WriteCharEntity(char ch)
		{
			int num = (int)ch;
			string s = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch, '\0');
			}
			byte[] array = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 35;
			byte[] array3 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 120;
			this.RawText(s);
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x00045AAC File Offset: 0x00043CAC
		public unsafe override void WriteWhitespace(string ws)
		{
			fixed (string text = ws)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* pSrcEnd = ptr + ws.Length;
				if (this.inAttributeValue)
				{
					this.WriteAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00045AF4 File Offset: 0x00043CF4
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
					this.WriteAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x00045B3C File Offset: 0x00043D3C
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			byte[] array = this.bufBytes;
			int num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array[num2] = 38;
			byte[] array2 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array2[num2] = 35;
			byte[] array3 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array3[num2] = 120;
			this.RawText(num.ToString("X", NumberFormatInfo.InvariantInfo));
			byte[] array4 = this.bufBytes;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array4[num2] = 59;
			this.textPos = this.bufPos;
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00045BDC File Offset: 0x00043DDC
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				if (this.inAttributeValue)
				{
					this.WriteAttributeTextBlock(ptr2, ptr2 + count);
				}
				else
				{
					this.WriteElementTextBlock(ptr2, ptr2 + count);
				}
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x00045C1C File Offset: 0x00043E1C
		public unsafe override void WriteRaw(char[] buffer, int index, int count)
		{
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				this.WriteRawWithCharChecking(ptr2, ptr2 + count);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x00045C50 File Offset: 0x00043E50
		public unsafe override void WriteRaw(string data)
		{
			fixed (string text = data)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.WriteRawWithCharChecking(ptr, ptr + data.Length);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00045C90 File Offset: 0x00043E90
		public override void Close()
		{
			try
			{
				this.FlushBuffer();
				this.FlushEncoder();
			}
			finally
			{
				this.writeToNull = true;
				if (this.stream != null)
				{
					try
					{
						this.stream.Flush();
					}
					finally
					{
						try
						{
							if (this.closeOutput)
							{
								this.stream.Close();
							}
						}
						finally
						{
							this.stream = null;
						}
					}
				}
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00045D10 File Offset: 0x00043F10
		public override void Flush()
		{
			this.FlushBuffer();
			this.FlushEncoder();
			if (this.stream != null)
			{
				this.stream.Flush();
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00045D34 File Offset: 0x00043F34
		protected virtual void FlushBuffer()
		{
			try
			{
				if (!this.writeToNull)
				{
					this.stream.Write(this.bufBytes, 1, this.bufPos - 1);
				}
			}
			catch
			{
				this.writeToNull = true;
				throw;
			}
			finally
			{
				this.bufBytes[0] = this.bufBytes[this.bufPos - 1];
				if (XmlUtf8RawTextWriter.IsSurrogateByte(this.bufBytes[0]))
				{
					this.bufBytes[1] = this.bufBytes[this.bufPos];
					this.bufBytes[2] = this.bufBytes[this.bufPos + 1];
					this.bufBytes[3] = this.bufBytes[this.bufPos + 2];
				}
				this.textPos = ((this.textPos == this.bufPos) ? 1 : 0);
				this.attrEndPos = ((this.attrEndPos == this.bufPos) ? 1 : 0);
				this.contentPos = 0;
				this.cdataPos = 0;
				this.bufPos = 1;
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000B528 File Offset: 0x00009728
		private void FlushEncoder()
		{
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00045E40 File Offset: 0x00044040
		protected unsafe void WriteAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
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
			int num = 0;
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr2 = (byte)num;
					ptr2++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 >= ptr3)
				{
					this.bufPos = (int)((long)(ptr2 - ptr));
					this.FlushBuffer();
					ptr2 = ptr + 1;
				}
				else
				{
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_1CC;
							}
							ptr2 = XmlUtf8RawTextWriter.TabEntity(ptr2);
							goto IL_1CC;
						case 10:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_1CC;
							}
							ptr2 = XmlUtf8RawTextWriter.LineFeedEntity(ptr2);
							goto IL_1CC;
						case 11:
						case 12:
							break;
						case 13:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_1CC;
							}
							ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_1CC;
						default:
							if (num == 34)
							{
								ptr2 = XmlUtf8RawTextWriter.QuoteEntity(ptr2);
								goto IL_1CC;
							}
							if (num == 38)
							{
								ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
								goto IL_1CC;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							*ptr2 = (byte)num;
							ptr2++;
							goto IL_1CC;
						}
						if (num == 60)
						{
							ptr2 = XmlUtf8RawTextWriter.LtEntity(ptr2);
							goto IL_1CC;
						}
						if (num == 62)
						{
							ptr2 = XmlUtf8RawTextWriter.GtEntity(ptr2);
							goto IL_1CC;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					pSrc++;
					continue;
					IL_1CC:
					pSrc++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00046034 File Offset: 0x00044234
		protected unsafe void WriteElementTextBlock(char* pSrc, char* pSrcEnd)
		{
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
			int num = 0;
			for (;;)
			{
				byte* ptr3 = ptr2 + (long)(pSrcEnd - pSrc);
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr2 = (byte)num;
					ptr2++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					break;
				}
				if (ptr2 < ptr3)
				{
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							goto IL_108;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr2 = this.WriteNewLine(ptr2);
								goto IL_1CF;
							}
							*ptr2 = (byte)num;
							ptr2++;
							goto IL_1CF;
						case 11:
						case 12:
							break;
						case 13:
							switch (this.newLineHandling)
							{
							case NewLineHandling.Replace:
								if (pSrc[1] == '\n')
								{
									pSrc++;
								}
								ptr2 = this.WriteNewLine(ptr2);
								goto IL_1CF;
							case NewLineHandling.Entitize:
								ptr2 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr2);
								goto IL_1CF;
							case NewLineHandling.None:
								*ptr2 = (byte)num;
								ptr2++;
								goto IL_1CF;
							default:
								goto IL_1CF;
							}
							break;
						default:
							if (num == 34)
							{
								goto IL_108;
							}
							if (num == 38)
							{
								ptr2 = XmlUtf8RawTextWriter.AmpEntity(ptr2);
								goto IL_1CF;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							goto IL_108;
						}
						if (num == 60)
						{
							ptr2 = XmlUtf8RawTextWriter.LtEntity(ptr2);
							goto IL_1CF;
						}
						if (num == 62)
						{
							ptr2 = XmlUtf8RawTextWriter.GtEntity(ptr2);
							goto IL_1CF;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					pSrc++;
					continue;
					IL_1CF:
					pSrc++;
					continue;
					IL_108:
					*ptr2 = (byte)num;
					ptr2++;
					goto IL_1CF;
				}
				this.bufPos = (int)((long)(ptr2 - ptr));
				this.FlushBuffer();
				ptr2 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0004623C File Offset: 0x0004443C
		protected unsafe void RawText(string s)
		{
			fixed (string text = s)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.RawText(ptr, ptr + s.Length);
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00046270 File Offset: 0x00044470
		protected unsafe void RawText(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr2 + (long)(pSrcEnd - ptr3);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) <= 127)
				{
					ptr3++;
					*ptr2 = (byte)num;
					ptr2++;
				}
				if (ptr3 >= pSrcEnd)
				{
					break;
				}
				if (ptr2 >= ptr4)
				{
					this.bufPos = (int)((long)(ptr2 - ptr));
					this.FlushBuffer();
					ptr2 = ptr + 1;
				}
				else if (XmlCharType.IsSurrogate(num))
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00046368 File Offset: 0x00044568
		protected unsafe void WriteRawWithCharChecking(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr2 = pSrcBegin;
			byte* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - ptr2);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					ptr2++;
				}
				if (ptr2 >= pSrcEnd)
				{
					break;
				}
				if (ptr3 < ptr4)
				{
					if (num <= 38)
					{
						switch (num)
						{
						case 9:
							goto IL_D9;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr3 = this.WriteNewLine(ptr3);
								goto IL_180;
							}
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_180;
						case 11:
						case 12:
							break;
						case 13:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								if (ptr2[1] == '\n')
								{
									ptr2++;
								}
								ptr3 = this.WriteNewLine(ptr3);
								goto IL_180;
							}
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_180;
						default:
							if (num == 38)
							{
								goto IL_D9;
							}
							break;
						}
					}
					else if (num == 60 || num == 93)
					{
						goto IL_D9;
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
						ptr2 += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr3 = this.InvalidXmlChar(num, ptr3, false);
						ptr2++;
						continue;
					}
					ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
					ptr2++;
					continue;
					IL_180:
					ptr2++;
					continue;
					IL_D9:
					*ptr3 = (byte)num;
					ptr3++;
					goto IL_180;
				}
				this.bufPos = (int)((long)(ptr3 - ptr));
				this.FlushBuffer();
				ptr3 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00046510 File Offset: 0x00044710
		protected unsafe void WriteCommentOrPi(string text, int stopChar)
		{
			if (text.Length == 0)
			{
				if (this.bufPos >= this.bufLen)
				{
					this.FlushBuffer();
				}
				return;
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				byte[] array;
				byte* ptr2;
				if ((array = this.bufBytes) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				byte* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					byte* ptr6 = ptr5 + (long)(ptr4 - ptr3);
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 64) != 0 && num != stopChar && num <= 127)
					{
						*ptr5 = (byte)num;
						ptr5++;
						ptr3++;
					}
					if (ptr3 >= ptr4)
					{
						break;
					}
					if (ptr5 < ptr6)
					{
						if (num <= 45)
						{
							switch (num)
							{
							case 9:
								goto IL_220;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_28F;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_28F;
							case 11:
							case 12:
								break;
							case 13:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									if (ptr3[1] == '\n')
									{
										ptr3++;
									}
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_28F;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_28F;
							default:
								if (num == 38)
								{
									goto IL_220;
								}
								if (num == 45)
								{
									*ptr5 = 45;
									ptr5++;
									if (num == stopChar && (ptr3 + 1 == ptr4 || ptr3[1] == '-'))
									{
										*ptr5 = 32;
										ptr5++;
										goto IL_28F;
									}
									goto IL_28F;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_220;
							}
							if (num != 63)
							{
								if (num == 93)
								{
									*ptr5 = 93;
									ptr5++;
									goto IL_28F;
								}
							}
							else
							{
								*ptr5 = 63;
								ptr5++;
								if (num == stopChar && ptr3 + 1 < ptr4 && ptr3[1] == '>')
								{
									*ptr5 = 32;
									ptr5++;
									goto IL_28F;
								}
								goto IL_28F;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						ptr5 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr5);
						ptr3++;
						continue;
						IL_28F:
						ptr3++;
						continue;
						IL_220:
						*ptr5 = (byte)num;
						ptr5++;
						goto IL_28F;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000467CC File Offset: 0x000449CC
		protected unsafe void WriteCDataSection(string text)
		{
			if (text.Length == 0)
			{
				if (this.bufPos >= this.bufLen)
				{
					this.FlushBuffer();
				}
				return;
			}
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				byte[] array;
				byte* ptr2;
				if ((array = this.bufBytes) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				byte* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					byte* ptr6 = ptr5 + (long)(ptr4 - ptr3);
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 128) != 0 && num != 93 && num <= 127)
					{
						*ptr5 = (byte)num;
						ptr5++;
						ptr3++;
					}
					if (ptr3 >= ptr4)
					{
						break;
					}
					if (ptr5 < ptr6)
					{
						if (num <= 39)
						{
							switch (num)
							{
							case 9:
								goto IL_204;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_273;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_273;
							case 11:
							case 12:
								break;
							case 13:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									if (ptr3[1] == '\n')
									{
										ptr3++;
									}
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_273;
								}
								*ptr5 = (byte)num;
								ptr5++;
								goto IL_273;
							default:
								if (num == 34 || num - 38 <= 1)
								{
									goto IL_204;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_204;
							}
							if (num == 62)
							{
								if (this.hadDoubleBracket && ptr5[-1] == 93)
								{
									ptr5 = XmlUtf8RawTextWriter.RawEndCData(ptr5);
									ptr5 = XmlUtf8RawTextWriter.RawStartCData(ptr5);
								}
								*ptr5 = 62;
								ptr5++;
								goto IL_273;
							}
							if (num == 93)
							{
								if (ptr5[-1] == 93)
								{
									this.hadDoubleBracket = true;
								}
								else
								{
									this.hadDoubleBracket = false;
								}
								*ptr5 = 93;
								ptr5++;
								goto IL_273;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						ptr5 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr5);
						ptr3++;
						continue;
						IL_273:
						ptr3++;
						continue;
						IL_204:
						*ptr5 = (byte)num;
						ptr5++;
						goto IL_273;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00046A69 File Offset: 0x00044C69
		private static bool IsSurrogateByte(byte b)
		{
			return (b & 248) == 240;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00046A7C File Offset: 0x00044C7C
		private unsafe static byte* EncodeSurrogate(char* pSrc, char* pSrcEnd, byte* pDst)
		{
			int num = (int)(*pSrc);
			if (num > 56319)
			{
				throw XmlConvert.CreateInvalidHighSurrogateCharException((char)num);
			}
			if (pSrc + 1 >= pSrcEnd)
			{
				throw new ArgumentException(Res.GetString("The surrogate pair is invalid. Missing a low surrogate character."));
			}
			int num2 = (int)pSrc[1];
			if (num2 >= 56320 && (LocalAppContextSwitches.DontThrowOnInvalidSurrogatePairs || num2 <= 57343))
			{
				num = XmlCharType.CombineSurrogateChar(num2, num);
				*pDst = (byte)(240 | num >> 18);
				pDst[1] = (byte)(128 | (num >> 12 & 63));
				pDst[2] = (byte)(128 | (num >> 6 & 63));
				pDst[3] = (byte)(128 | (num & 63));
				pDst += 4;
				return pDst;
			}
			throw XmlConvert.CreateInvalidSurrogatePairException((char)num2, (char)num);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00046B28 File Offset: 0x00044D28
		private unsafe byte* InvalidXmlChar(int ch, byte* pDst, bool entitize)
		{
			if (this.checkCharacters)
			{
				throw XmlConvert.CreateInvalidCharException((char)ch, '\0');
			}
			if (entitize)
			{
				return XmlUtf8RawTextWriter.CharEntity(pDst, (char)ch);
			}
			if (ch < 128)
			{
				*pDst = (byte)ch;
				pDst++;
			}
			else
			{
				pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(ch, pDst);
			}
			return pDst;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00046B64 File Offset: 0x00044D64
		internal unsafe void EncodeChar(ref char* pSrc, char* pSrcEnd, ref byte* pDst)
		{
			int num = (int)(*pSrc);
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			if (num <= 127 || num >= 65534)
			{
				pDst = this.InvalidXmlChar(num, pDst, false);
				pSrc += 2;
				return;
			}
			pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, pDst);
			pSrc += 2;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00046BC4 File Offset: 0x00044DC4
		internal unsafe static byte* EncodeMultibyteUTF8(int ch, byte* pDst)
		{
			if (ch < 2048)
			{
				*pDst = (byte)(-64 | ch >> 6);
			}
			else
			{
				*pDst = (byte)(-32 | ch >> 12);
				pDst++;
				*pDst = (byte)(-128 | (ch >> 6 & 63));
			}
			pDst++;
			*pDst = (byte)(128 | (ch & 63));
			return pDst + 1;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00046C14 File Offset: 0x00044E14
		internal unsafe static void CharToUTF8(ref char* pSrc, char* pSrcEnd, ref byte* pDst)
		{
			int num = (int)(*pSrc);
			if (num <= 127)
			{
				*pDst = (byte)num;
				pDst++;
				pSrc += 2;
				return;
			}
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			pDst = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, pDst);
			pSrc += 2;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00046C6C File Offset: 0x00044E6C
		protected unsafe byte* WriteNewLine(byte* pDst)
		{
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
			this.bufPos = (int)((long)(pDst - ptr));
			this.RawText(this.newLineChars);
			return ptr + this.bufPos;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00046CB7 File Offset: 0x00044EB7
		protected unsafe static byte* LtEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 108;
			pDst[2] = 116;
			pDst[3] = 59;
			return pDst + 4;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00046CD2 File Offset: 0x00044ED2
		protected unsafe static byte* GtEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 103;
			pDst[2] = 116;
			pDst[3] = 59;
			return pDst + 4;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00046CED File Offset: 0x00044EED
		protected unsafe static byte* AmpEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 97;
			pDst[2] = 109;
			pDst[3] = 112;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00046D0E File Offset: 0x00044F0E
		protected unsafe static byte* QuoteEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 113;
			pDst[2] = 117;
			pDst[3] = 111;
			pDst[4] = 116;
			pDst[5] = 59;
			return pDst + 6;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00046D35 File Offset: 0x00044F35
		protected unsafe static byte* TabEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 57;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00046D56 File Offset: 0x00044F56
		protected unsafe static byte* LineFeedEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 65;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00046D77 File Offset: 0x00044F77
		protected unsafe static byte* CarriageReturnEntity(byte* pDst)
		{
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst[3] = 68;
			pDst[4] = 59;
			return pDst + 5;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00046D98 File Offset: 0x00044F98
		private unsafe static byte* CharEntity(byte* pDst, char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			*pDst = 38;
			pDst[1] = 35;
			pDst[2] = 120;
			pDst += 3;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while ((*(pDst++) = (byte)(*(ptr2++))) != 0)
				{
				}
			}
			pDst[-1] = 59;
			return pDst;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00046DFC File Offset: 0x00044FFC
		protected unsafe static byte* RawStartCData(byte* pDst)
		{
			*pDst = 60;
			pDst[1] = 33;
			pDst[2] = 91;
			pDst[3] = 67;
			pDst[4] = 68;
			pDst[5] = 65;
			pDst[6] = 84;
			pDst[7] = 65;
			pDst[8] = 91;
			return pDst + 9;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00046E36 File Offset: 0x00045036
		protected unsafe static byte* RawEndCData(byte* pDst)
		{
			*pDst = 93;
			pDst[1] = 93;
			pDst[2] = 62;
			return pDst + 3;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00046E4C File Offset: 0x0004504C
		protected void ValidateContentChars(string chars, string propertyName, bool allowOnlyWhitespace)
		{
			if (!allowOnlyWhitespace)
			{
				for (int i = 0; i < chars.Length; i++)
				{
					if (!this.xmlCharType.IsTextChar(chars[i]))
					{
						char c = chars[i];
						if (c <= '&')
						{
							switch (c)
							{
							case '\t':
							case '\n':
							case '\r':
								goto IL_11C;
							case '\v':
							case '\f':
								goto IL_A2;
							default:
								if (c != '&')
								{
									goto IL_A2;
								}
								break;
							}
						}
						else if (c != '<' && c != ']')
						{
							goto IL_A2;
						}
						string name = "'{0}', hexadecimal value {1}, is an invalid character.";
						object[] args = XmlException.BuildCharExceptionArgs(chars, i);
						string @string = Res.GetString(name, args);
						goto IL_12D;
						IL_A2:
						if (XmlCharType.IsHighSurrogate((int)chars[i]))
						{
							if (i + 1 < chars.Length && XmlCharType.IsLowSurrogate((int)chars[i + 1]))
							{
								i++;
								goto IL_11C;
							}
							@string = Res.GetString("The surrogate pair is invalid. Missing a low surrogate character.");
						}
						else
						{
							if (!XmlCharType.IsLowSurrogate((int)chars[i]))
							{
								goto IL_11C;
							}
							@string = Res.GetString("Invalid high surrogate character (0x{0}). A high surrogate character must have a value from range (0xD800 - 0xDBFF).", new object[]
							{
								((uint)chars[i]).ToString("X", CultureInfo.InvariantCulture)
							});
						}
						IL_12D:
						string name2 = "XmlWriterSettings.{0} can contain only valid XML text content characters when XmlWriterSettings.CheckCharacters is true. {1}";
						args = new string[]
						{
							propertyName,
							@string
						};
						throw new ArgumentException(Res.GetString(name2, args));
					}
					IL_11C:;
				}
				return;
			}
			if (!this.xmlCharType.IsOnlyWhitespace(chars))
			{
				throw new ArgumentException(Res.GetString("XmlWriterSettings.{0} can contain only valid XML white space characters when XmlWriterSettings.CheckCharacters and XmlWriterSettings.NewLineOnAttributes are true.", new object[]
				{
					propertyName
				}));
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00046FA6 File Offset: 0x000451A6
		protected void CheckAsyncCall()
		{
			if (!this.useAsync)
			{
				throw new InvalidOperationException(Res.GetString("Set XmlWriterSettings.Async to true if you want to use Async Methods."));
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00046FC0 File Offset: 0x000451C0
		internal override Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86 <WriteXmlDeclarationAsync>d__;
			<WriteXmlDeclarationAsync>d__.<>4__this = this;
			<WriteXmlDeclarationAsync>d__.standalone = standalone;
			<WriteXmlDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteXmlDeclarationAsync>d__.<>1__state = -1;
			<WriteXmlDeclarationAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref <WriteXmlDeclarationAsync>d__);
			return <WriteXmlDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0004700B File Offset: 0x0004520B
		internal override Task WriteXmlDeclarationAsync(string xmldecl)
		{
			this.CheckAsyncCall();
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				return this.WriteProcessingInstructionAsync("xml", xmldecl);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00047038 File Offset: 0x00045238
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0004709C File Offset: 0x0004529C
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			Task task;
			if (prefix != null && prefix.Length != 0)
			{
				task = this.RawTextAsync(prefix + ":" + localName);
			}
			else
			{
				task = this.RawTextAsync(localName);
			}
			return task.CallVoidFuncWhenFinish(new Action(this.WriteStartElementAsync_SetAttEndPos));
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00047103 File Offset: 0x00045303
		private void WriteStartElementAsync_SetAttEndPos()
		{
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00047114 File Offset: 0x00045314
		internal override Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			int num;
			if (this.contentPos == this.bufPos)
			{
				this.bufPos--;
				byte[] array = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
				byte[] array2 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				byte[] array3 = this.bufBytes;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 62;
				return AsyncHelper.DoneTask;
			}
			byte[] array4 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 60;
			byte[] array5 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				return this.RawTextAsync(prefix + ":" + localName + ">");
			}
			return this.RawTextAsync(localName + ">");
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00047200 File Offset: 0x00045400
		internal override Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			byte[] array2 = this.bufBytes;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				return this.RawTextAsync(prefix + ":" + localName + ">");
			}
			return this.RawTextAsync(localName + ">");
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0004727C File Offset: 0x0004547C
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.attrEndPos == this.bufPos)
			{
				byte[] array = this.bufBytes;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			Task task;
			if (prefix != null && prefix.Length > 0)
			{
				task = this.RawTextAsync(prefix + ":" + localName + "=\"");
			}
			else
			{
				task = this.RawTextAsync(localName + "=\"");
			}
			return task.CallVoidFuncWhenFinish(new Action(this.WriteStartAttribute_SetInAttribute));
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0000FF4C File Offset: 0x0000E14C
		private void WriteStartAttribute_SetInAttribute()
		{
			this.inAttributeValue = true;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00047304 File Offset: 0x00045504
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsyncCall();
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0004734C File Offset: 0x0004554C
		internal override Task WriteNamespaceDeclarationAsync(string prefix, string namespaceName)
		{
			XmlUtf8RawTextWriter.<WriteNamespaceDeclarationAsync>d__96 <WriteNamespaceDeclarationAsync>d__;
			<WriteNamespaceDeclarationAsync>d__.<>4__this = this;
			<WriteNamespaceDeclarationAsync>d__.prefix = prefix;
			<WriteNamespaceDeclarationAsync>d__.namespaceName = namespaceName;
			<WriteNamespaceDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNamespaceDeclarationAsync>d__.<>1__state = -1;
			<WriteNamespaceDeclarationAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteNamespaceDeclarationAsync>d__96>(ref <WriteNamespaceDeclarationAsync>d__);
			return <WriteNamespaceDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000473A0 File Offset: 0x000455A0
		internal override Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			XmlUtf8RawTextWriter.<WriteStartNamespaceDeclarationAsync>d__97 <WriteStartNamespaceDeclarationAsync>d__;
			<WriteStartNamespaceDeclarationAsync>d__.<>4__this = this;
			<WriteStartNamespaceDeclarationAsync>d__.prefix = prefix;
			<WriteStartNamespaceDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartNamespaceDeclarationAsync>d__.<>1__state = -1;
			<WriteStartNamespaceDeclarationAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteStartNamespaceDeclarationAsync>d__97>(ref <WriteStartNamespaceDeclarationAsync>d__);
			return <WriteStartNamespaceDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000473EC File Offset: 0x000455EC
		internal override Task WriteEndNamespaceDeclarationAsync()
		{
			this.CheckAsyncCall();
			this.inAttributeValue = false;
			byte[] array = this.bufBytes;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00047434 File Offset: 0x00045634
		public override Task WriteCDataAsync(string text)
		{
			XmlUtf8RawTextWriter.<WriteCDataAsync>d__99 <WriteCDataAsync>d__;
			<WriteCDataAsync>d__.<>4__this = this;
			<WriteCDataAsync>d__.text = text;
			<WriteCDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataAsync>d__.<>1__state = -1;
			<WriteCDataAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCDataAsync>d__99>(ref <WriteCDataAsync>d__);
			return <WriteCDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00047480 File Offset: 0x00045680
		public override Task WriteCommentAsync(string text)
		{
			XmlUtf8RawTextWriter.<WriteCommentAsync>d__100 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCommentAsync>d__100>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000474CC File Offset: 0x000456CC
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			XmlUtf8RawTextWriter.<WriteProcessingInstructionAsync>d__101 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.name = name;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteProcessingInstructionAsync>d__101>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00047520 File Offset: 0x00045720
		public override Task WriteEntityRefAsync(string name)
		{
			XmlUtf8RawTextWriter.<WriteEntityRefAsync>d__102 <WriteEntityRefAsync>d__;
			<WriteEntityRefAsync>d__.<>4__this = this;
			<WriteEntityRefAsync>d__.name = name;
			<WriteEntityRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEntityRefAsync>d__.<>1__state = -1;
			<WriteEntityRefAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteEntityRefAsync>d__102>(ref <WriteEntityRefAsync>d__);
			return <WriteEntityRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0004756C File Offset: 0x0004576C
		public override Task WriteCharEntityAsync(char ch)
		{
			XmlUtf8RawTextWriter.<WriteCharEntityAsync>d__103 <WriteCharEntityAsync>d__;
			<WriteCharEntityAsync>d__.<>4__this = this;
			<WriteCharEntityAsync>d__.ch = ch;
			<WriteCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCharEntityAsync>d__.<>1__state = -1;
			<WriteCharEntityAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCharEntityAsync>d__103>(ref <WriteCharEntityAsync>d__);
			return <WriteCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000475B7 File Offset: 0x000457B7
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(ws);
			}
			return this.WriteElementTextBlockAsync(ws);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000475B7 File Offset: 0x000457B7
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(text);
			}
			return this.WriteElementTextBlockAsync(text);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000475D8 File Offset: 0x000457D8
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			XmlUtf8RawTextWriter.<WriteSurrogateCharEntityAsync>d__106 <WriteSurrogateCharEntityAsync>d__;
			<WriteSurrogateCharEntityAsync>d__.<>4__this = this;
			<WriteSurrogateCharEntityAsync>d__.lowChar = lowChar;
			<WriteSurrogateCharEntityAsync>d__.highChar = highChar;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteSurrogateCharEntityAsync>d__.<>1__state = -1;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteSurrogateCharEntityAsync>d__106>(ref <WriteSurrogateCharEntityAsync>d__);
			return <WriteSurrogateCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0004762B File Offset: 0x0004582B
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(buffer, index, count);
			}
			return this.WriteElementTextBlockAsync(buffer, index, count);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00047650 File Offset: 0x00045850
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			XmlUtf8RawTextWriter.<WriteRawAsync>d__108 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.buffer = buffer;
			<WriteRawAsync>d__.index = index;
			<WriteRawAsync>d__.count = count;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteRawAsync>d__108>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000476AC File Offset: 0x000458AC
		public override Task WriteRawAsync(string data)
		{
			XmlUtf8RawTextWriter.<WriteRawAsync>d__109 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.data = data;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteRawAsync>d__109>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000476F8 File Offset: 0x000458F8
		public override Task FlushAsync()
		{
			XmlUtf8RawTextWriter.<FlushAsync>d__110 <FlushAsync>d__;
			<FlushAsync>d__.<>4__this = this;
			<FlushAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsync>d__.<>1__state = -1;
			<FlushAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<FlushAsync>d__110>(ref <FlushAsync>d__);
			return <FlushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0004773C File Offset: 0x0004593C
		protected virtual Task FlushBufferAsync()
		{
			XmlUtf8RawTextWriter.<FlushBufferAsync>d__111 <FlushBufferAsync>d__;
			<FlushBufferAsync>d__.<>4__this = this;
			<FlushBufferAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushBufferAsync>d__.<>1__state = -1;
			<FlushBufferAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<FlushBufferAsync>d__111>(ref <FlushBufferAsync>d__);
			return <FlushBufferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001E21A File Offset: 0x0001C41A
		private Task FlushEncoderAsync()
		{
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00047780 File Offset: 0x00045980
		[SecuritySafeCritical]
		protected unsafe int WriteAttributeTextBlockNoFlush(char* pSrc, char* pSrcEnd)
		{
			char* ptr = pSrc;
			byte[] array;
			byte* ptr2;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			byte* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - pSrc);
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_1E8;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_1DE;
						}
						ptr3 = XmlUtf8RawTextWriter.TabEntity(ptr3);
						goto IL_1DE;
					case 10:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_1DE;
						}
						ptr3 = XmlUtf8RawTextWriter.LineFeedEntity(ptr3);
						goto IL_1DE;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_1DE;
						}
						ptr3 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr3);
						goto IL_1DE;
					default:
						if (num == 34)
						{
							ptr3 = XmlUtf8RawTextWriter.QuoteEntity(ptr3);
							goto IL_1DE;
						}
						if (num == 38)
						{
							ptr3 = XmlUtf8RawTextWriter.AmpEntity(ptr3);
							goto IL_1DE;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_1DE;
					}
					if (num == 60)
					{
						ptr3 = XmlUtf8RawTextWriter.LtEntity(ptr3);
						goto IL_1DE;
					}
					if (num == 62)
					{
						ptr3 = XmlUtf8RawTextWriter.GtEntity(ptr3);
						goto IL_1DE;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				pSrc++;
				continue;
				IL_1DE:
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			IL_1E8:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			array = null;
			return -1;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00047988 File Offset: 0x00045B88
		[SecuritySafeCritical]
		protected unsafe int WriteAttributeTextBlockNoFlush(char[] chars, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* pSrcEnd = ptr2 + count;
				return this.WriteAttributeTextBlockNoFlush(ptr2, pSrcEnd);
			}
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000479B4 File Offset: 0x00045BB4
		[SecuritySafeCritical]
		protected unsafe int WriteAttributeTextBlockNoFlush(string text, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* pSrcEnd = ptr2 + count;
			return this.WriteAttributeTextBlockNoFlush(ptr2, pSrcEnd);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000479EC File Offset: 0x00045BEC
		protected Task WriteAttributeTextBlockAsync(char[] chars, int index, int count)
		{
			XmlUtf8RawTextWriter.<WriteAttributeTextBlockAsync>d__116 <WriteAttributeTextBlockAsync>d__;
			<WriteAttributeTextBlockAsync>d__.<>4__this = this;
			<WriteAttributeTextBlockAsync>d__.chars = chars;
			<WriteAttributeTextBlockAsync>d__.index = index;
			<WriteAttributeTextBlockAsync>d__.count = count;
			<WriteAttributeTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAttributeTextBlockAsync>d__.<>1__state = -1;
			<WriteAttributeTextBlockAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteAttributeTextBlockAsync>d__116>(ref <WriteAttributeTextBlockAsync>d__);
			return <WriteAttributeTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00047A48 File Offset: 0x00045C48
		protected Task WriteAttributeTextBlockAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			int num3 = this.WriteAttributeTextBlockNoFlush(text, num, num2);
			num += num3;
			num2 -= num3;
			if (num3 >= 0)
			{
				return this._WriteAttributeTextBlockAsync(text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00047A88 File Offset: 0x00045C88
		private Task _WriteAttributeTextBlockAsync(string text, int curIndex, int leftCount)
		{
			XmlUtf8RawTextWriter.<_WriteAttributeTextBlockAsync>d__118 <_WriteAttributeTextBlockAsync>d__;
			<_WriteAttributeTextBlockAsync>d__.<>4__this = this;
			<_WriteAttributeTextBlockAsync>d__.text = text;
			<_WriteAttributeTextBlockAsync>d__.curIndex = curIndex;
			<_WriteAttributeTextBlockAsync>d__.leftCount = leftCount;
			<_WriteAttributeTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_WriteAttributeTextBlockAsync>d__.<>1__state = -1;
			<_WriteAttributeTextBlockAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<_WriteAttributeTextBlockAsync>d__118>(ref <_WriteAttributeTextBlockAsync>d__);
			return <_WriteAttributeTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00047AE4 File Offset: 0x00045CE4
		[SecuritySafeCritical]
		protected unsafe int WriteElementTextBlockNoFlush(char* pSrc, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			char* ptr = pSrc;
			byte[] array;
			byte* ptr2;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			byte* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - pSrc);
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_209;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						goto IL_114;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_14;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_1FF;
					case 11:
					case 12:
						break;
					case 13:
						switch (this.newLineHandling)
						{
						case NewLineHandling.Replace:
							goto IL_170;
						case NewLineHandling.Entitize:
							ptr3 = XmlUtf8RawTextWriter.CarriageReturnEntity(ptr3);
							goto IL_1FF;
						case NewLineHandling.None:
							*ptr3 = (byte)num;
							ptr3++;
							goto IL_1FF;
						default:
							goto IL_1FF;
						}
						break;
					default:
						if (num == 34)
						{
							goto IL_114;
						}
						if (num == 38)
						{
							ptr3 = XmlUtf8RawTextWriter.AmpEntity(ptr3);
							goto IL_1FF;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						goto IL_114;
					}
					if (num == 60)
					{
						ptr3 = XmlUtf8RawTextWriter.LtEntity(ptr3);
						goto IL_1FF;
					}
					if (num == 62)
					{
						ptr3 = XmlUtf8RawTextWriter.GtEntity(ptr3);
						goto IL_1FF;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				pSrc++;
				continue;
				IL_1FF:
				pSrc++;
				continue;
				IL_114:
				*ptr3 = (byte)num;
				ptr3++;
				goto IL_1FF;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			Block_14:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_170:
			if (pSrc[1] == '\n')
			{
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_209:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
			return -1;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00047D20 File Offset: 0x00045F20
		[SecuritySafeCritical]
		protected unsafe int WriteElementTextBlockNoFlush(char[] chars, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				this.contentPos = 0;
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* pSrcEnd = ptr2 + count;
				return this.WriteElementTextBlockNoFlush(ptr2, pSrcEnd, out needWriteNewLine);
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00047D5C File Offset: 0x00045F5C
		[SecuritySafeCritical]
		protected unsafe int WriteElementTextBlockNoFlush(string text, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				this.contentPos = 0;
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* pSrcEnd = ptr2 + count;
			return this.WriteElementTextBlockNoFlush(ptr2, pSrcEnd, out needWriteNewLine);
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00047DA4 File Offset: 0x00045FA4
		protected Task WriteElementTextBlockAsync(char[] chars, int index, int count)
		{
			XmlUtf8RawTextWriter.<WriteElementTextBlockAsync>d__122 <WriteElementTextBlockAsync>d__;
			<WriteElementTextBlockAsync>d__.<>4__this = this;
			<WriteElementTextBlockAsync>d__.chars = chars;
			<WriteElementTextBlockAsync>d__.index = index;
			<WriteElementTextBlockAsync>d__.count = count;
			<WriteElementTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteElementTextBlockAsync>d__.<>1__state = -1;
			<WriteElementTextBlockAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteElementTextBlockAsync>d__122>(ref <WriteElementTextBlockAsync>d__);
			return <WriteElementTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00047E00 File Offset: 0x00046000
		protected Task WriteElementTextBlockAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			bool flag = false;
			int num3 = this.WriteElementTextBlockNoFlush(text, num, num2, out flag);
			num += num3;
			num2 -= num3;
			if (flag)
			{
				return this._WriteElementTextBlockAsync(true, text, num, num2);
			}
			if (num3 >= 0)
			{
				return this._WriteElementTextBlockAsync(false, text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00047E50 File Offset: 0x00046050
		private Task _WriteElementTextBlockAsync(bool newLine, string text, int curIndex, int leftCount)
		{
			XmlUtf8RawTextWriter.<_WriteElementTextBlockAsync>d__124 <_WriteElementTextBlockAsync>d__;
			<_WriteElementTextBlockAsync>d__.<>4__this = this;
			<_WriteElementTextBlockAsync>d__.newLine = newLine;
			<_WriteElementTextBlockAsync>d__.text = text;
			<_WriteElementTextBlockAsync>d__.curIndex = curIndex;
			<_WriteElementTextBlockAsync>d__.leftCount = leftCount;
			<_WriteElementTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_WriteElementTextBlockAsync>d__.<>1__state = -1;
			<_WriteElementTextBlockAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<_WriteElementTextBlockAsync>d__124>(ref <_WriteElementTextBlockAsync>d__);
			return <_WriteElementTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00047EB4 File Offset: 0x000460B4
		[SecuritySafeCritical]
		protected unsafe int RawTextNoFlush(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr2 + (long)(pSrcEnd - ptr3);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) <= 127)
				{
					ptr3++;
					*ptr2 = (byte)num;
					ptr2++;
				}
				if (ptr3 >= pSrcEnd)
				{
					goto IL_E7;
				}
				if (ptr2 >= ptr4)
				{
					break;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					ptr2 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr2);
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			return (int)((long)(ptr3 - pSrcBegin));
			IL_E7:
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00047FB8 File Offset: 0x000461B8
		[SecuritySafeCritical]
		protected unsafe int RawTextNoFlush(string text, int index, int count)
		{
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* pSrcEnd = ptr2 + count;
			return this.RawTextNoFlush(ptr2, pSrcEnd);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00047FF0 File Offset: 0x000461F0
		protected Task RawTextAsync(string text)
		{
			int num = 0;
			int num2 = text.Length;
			int num3 = this.RawTextNoFlush(text, num, num2);
			num += num3;
			num2 -= num3;
			if (num3 >= 0)
			{
				return this._RawTextAsync(text, num, num2);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00048030 File Offset: 0x00046230
		private Task _RawTextAsync(string text, int curIndex, int leftCount)
		{
			XmlUtf8RawTextWriter.<_RawTextAsync>d__128 <_RawTextAsync>d__;
			<_RawTextAsync>d__.<>4__this = this;
			<_RawTextAsync>d__.text = text;
			<_RawTextAsync>d__.curIndex = curIndex;
			<_RawTextAsync>d__.leftCount = leftCount;
			<_RawTextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_RawTextAsync>d__.<>1__state = -1;
			<_RawTextAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<_RawTextAsync>d__128>(ref <_RawTextAsync>d__);
			return <_RawTextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0004808C File Offset: 0x0004628C
		[SecuritySafeCritical]
		protected unsafe int WriteRawWithCharCheckingNoFlush(char* pSrcBegin, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
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
			char* ptr2 = pSrcBegin;
			byte* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr4 = ptr3 + (long)(pSrcEnd - ptr2);
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0 && num <= 127)
				{
					*ptr3 = (byte)num;
					ptr3++;
					ptr2++;
				}
				if (ptr2 >= pSrcEnd)
				{
					goto IL_1C5;
				}
				if (ptr3 >= ptr4)
				{
					break;
				}
				if (num <= 38)
				{
					switch (num)
					{
					case 9:
						goto IL_E5;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_13;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_1BC;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_11;
						}
						*ptr3 = (byte)num;
						ptr3++;
						goto IL_1BC;
					default:
						if (num == 38)
						{
							goto IL_E5;
						}
						break;
					}
				}
				else if (num == 60 || num == 93)
				{
					goto IL_E5;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
					ptr2 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, false);
					ptr2++;
					continue;
				}
				ptr3 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr3);
				ptr2++;
				continue;
				IL_1BC:
				ptr2++;
				continue;
				IL_E5:
				*ptr3 = (byte)num;
				ptr3++;
				goto IL_1BC;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			return (int)((long)(ptr2 - pSrcBegin));
			Block_11:
			if (ptr2[1] == '\n')
			{
				ptr2++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			Block_13:
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			IL_1C5:
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00048270 File Offset: 0x00046470
		[SecuritySafeCritical]
		protected unsafe int WriteRawWithCharCheckingNoFlush(char[] chars, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			fixed (char* ptr = &chars[index])
			{
				char* ptr2 = ptr;
				char* pSrcEnd = ptr2 + count;
				return this.WriteRawWithCharCheckingNoFlush(ptr2, pSrcEnd, out needWriteNewLine);
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000482A4 File Offset: 0x000464A4
		[SecuritySafeCritical]
		protected unsafe int WriteRawWithCharCheckingNoFlush(string text, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			char* pSrcEnd = ptr2 + count;
			return this.WriteRawWithCharCheckingNoFlush(ptr2, pSrcEnd, out needWriteNewLine);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000482E4 File Offset: 0x000464E4
		protected Task WriteRawWithCharCheckingAsync(char[] chars, int index, int count)
		{
			XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__132 <WriteRawWithCharCheckingAsync>d__;
			<WriteRawWithCharCheckingAsync>d__.<>4__this = this;
			<WriteRawWithCharCheckingAsync>d__.chars = chars;
			<WriteRawWithCharCheckingAsync>d__.index = index;
			<WriteRawWithCharCheckingAsync>d__.count = count;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawWithCharCheckingAsync>d__.<>1__state = -1;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__132>(ref <WriteRawWithCharCheckingAsync>d__);
			return <WriteRawWithCharCheckingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00048340 File Offset: 0x00046540
		protected Task WriteRawWithCharCheckingAsync(string text)
		{
			XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__133 <WriteRawWithCharCheckingAsync>d__;
			<WriteRawWithCharCheckingAsync>d__.<>4__this = this;
			<WriteRawWithCharCheckingAsync>d__.text = text;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawWithCharCheckingAsync>d__.<>1__state = -1;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__133>(ref <WriteRawWithCharCheckingAsync>d__);
			return <WriteRawWithCharCheckingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0004838C File Offset: 0x0004658C
		[SecuritySafeCritical]
		protected unsafe int WriteCommentOrPiNoFlush(string text, int index, int count, int stopChar, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			byte[] array;
			byte* ptr3;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr3 = null;
			}
			else
			{
				ptr3 = &array[0];
			}
			char* ptr4 = ptr2;
			char* ptr5 = ptr4;
			char* ptr6 = ptr2 + count;
			byte* ptr7 = ptr3 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr8 = ptr7 + (long)(ptr6 - ptr4);
				if (ptr8 != ptr3 + this.bufLen)
				{
					ptr8 = ptr3 + this.bufLen;
				}
				while (ptr7 < ptr8 && (this.xmlCharType.charProperties[num = (int)(*ptr4)] & 64) != 0 && num != stopChar && num <= 127)
				{
					*ptr7 = (byte)num;
					ptr7++;
					ptr4++;
				}
				if (ptr4 >= ptr6)
				{
					goto IL_2A4;
				}
				if (ptr7 >= ptr8)
				{
					break;
				}
				if (num <= 45)
				{
					switch (num)
					{
					case 9:
						goto IL_22A;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_24;
						}
						*ptr7 = (byte)num;
						ptr7++;
						goto IL_299;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_22;
						}
						*ptr7 = (byte)num;
						ptr7++;
						goto IL_299;
					default:
						if (num == 38)
						{
							goto IL_22A;
						}
						if (num == 45)
						{
							*ptr7 = 45;
							ptr7++;
							if (num == stopChar && (ptr4 + 1 == ptr6 || ptr4[1] == '-'))
							{
								*ptr7 = 32;
								ptr7++;
								goto IL_299;
							}
							goto IL_299;
						}
						break;
					}
				}
				else
				{
					if (num == 60)
					{
						goto IL_22A;
					}
					if (num != 63)
					{
						if (num == 93)
						{
							*ptr7 = 93;
							ptr7++;
							goto IL_299;
						}
					}
					else
					{
						*ptr7 = 63;
						ptr7++;
						if (num == stopChar && ptr4 + 1 < ptr6 && ptr4[1] == '>')
						{
							*ptr7 = 32;
							ptr7++;
							goto IL_299;
						}
						goto IL_299;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr7 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr4, ptr6, ptr7);
					ptr4 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr7 = this.InvalidXmlChar(num, ptr7, false);
					ptr4++;
					continue;
				}
				ptr7 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr7);
				ptr4++;
				continue;
				IL_299:
				ptr4++;
				continue;
				IL_22A:
				*ptr7 = (byte)num;
				ptr7++;
				goto IL_299;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			return (int)((long)(ptr4 - ptr5));
			Block_22:
			if (ptr4[1] == '\n')
			{
				ptr4++;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr5));
			Block_24:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr5));
			IL_2A4:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			array = null;
			return -1;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00048650 File Offset: 0x00046850
		protected Task WriteCommentOrPiAsync(string text, int stopChar)
		{
			XmlUtf8RawTextWriter.<WriteCommentOrPiAsync>d__135 <WriteCommentOrPiAsync>d__;
			<WriteCommentOrPiAsync>d__.<>4__this = this;
			<WriteCommentOrPiAsync>d__.text = text;
			<WriteCommentOrPiAsync>d__.stopChar = stopChar;
			<WriteCommentOrPiAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentOrPiAsync>d__.<>1__state = -1;
			<WriteCommentOrPiAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCommentOrPiAsync>d__135>(ref <WriteCommentOrPiAsync>d__);
			return <WriteCommentOrPiAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000486A4 File Offset: 0x000468A4
		[SecuritySafeCritical]
		protected unsafe int WriteCDataSectionNoFlush(string text, int index, int count, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			if (count == 0)
			{
				return -1;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr + index;
			byte[] array;
			byte* ptr3;
			if ((array = this.bufBytes) == null || array.Length == 0)
			{
				ptr3 = null;
			}
			else
			{
				ptr3 = &array[0];
			}
			char* ptr4 = ptr2;
			char* ptr5 = ptr2 + count;
			char* ptr6 = ptr4;
			byte* ptr7 = ptr3 + this.bufPos;
			int num = 0;
			for (;;)
			{
				byte* ptr8 = ptr7 + (long)(ptr5 - ptr4);
				if (ptr8 != ptr3 + this.bufLen)
				{
					ptr8 = ptr3 + this.bufLen;
				}
				while (ptr7 < ptr8 && (this.xmlCharType.charProperties[num = (int)(*ptr4)] & 128) != 0 && num != 93 && num <= 127)
				{
					*ptr7 = (byte)num;
					ptr7++;
					ptr4++;
				}
				if (ptr4 >= ptr5)
				{
					goto IL_285;
				}
				if (ptr7 >= ptr8)
				{
					break;
				}
				if (num <= 39)
				{
					switch (num)
					{
					case 9:
						goto IL_20B;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_22;
						}
						*ptr7 = (byte)num;
						ptr7++;
						goto IL_27A;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_20;
						}
						*ptr7 = (byte)num;
						ptr7++;
						goto IL_27A;
					default:
						if (num == 34 || num - 38 <= 1)
						{
							goto IL_20B;
						}
						break;
					}
				}
				else
				{
					if (num == 60)
					{
						goto IL_20B;
					}
					if (num == 62)
					{
						if (this.hadDoubleBracket && ptr7[-1] == 93)
						{
							ptr7 = XmlUtf8RawTextWriter.RawEndCData(ptr7);
							ptr7 = XmlUtf8RawTextWriter.RawStartCData(ptr7);
						}
						*ptr7 = 62;
						ptr7++;
						goto IL_27A;
					}
					if (num == 93)
					{
						if (ptr7[-1] == 93)
						{
							this.hadDoubleBracket = true;
						}
						else
						{
							this.hadDoubleBracket = false;
						}
						*ptr7 = 93;
						ptr7++;
						goto IL_27A;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr7 = XmlUtf8RawTextWriter.EncodeSurrogate(ptr4, ptr5, ptr7);
					ptr4 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr7 = this.InvalidXmlChar(num, ptr7, false);
					ptr4++;
					continue;
				}
				ptr7 = XmlUtf8RawTextWriter.EncodeMultibyteUTF8(num, ptr7);
				ptr4++;
				continue;
				IL_27A:
				ptr4++;
				continue;
				IL_20B:
				*ptr7 = (byte)num;
				ptr7++;
				goto IL_27A;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			return (int)((long)(ptr4 - ptr6));
			Block_20:
			if (ptr4[1] == '\n')
			{
				ptr4++;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr6));
			Block_22:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr6));
			IL_285:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			array = null;
			return -1;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00048948 File Offset: 0x00046B48
		protected Task WriteCDataSectionAsync(string text)
		{
			XmlUtf8RawTextWriter.<WriteCDataSectionAsync>d__137 <WriteCDataSectionAsync>d__;
			<WriteCDataSectionAsync>d__.<>4__this = this;
			<WriteCDataSectionAsync>d__.text = text;
			<WriteCDataSectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataSectionAsync>d__.<>1__state = -1;
			<WriteCDataSectionAsync>d__.<>t__builder.Start<XmlUtf8RawTextWriter.<WriteCDataSectionAsync>d__137>(ref <WriteCDataSectionAsync>d__);
			return <WriteCDataSectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000C2C RID: 3116
		private readonly bool useAsync;

		// Token: 0x04000C2D RID: 3117
		protected byte[] bufBytes;

		// Token: 0x04000C2E RID: 3118
		protected Stream stream;

		// Token: 0x04000C2F RID: 3119
		protected Encoding encoding;

		// Token: 0x04000C30 RID: 3120
		protected XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000C31 RID: 3121
		protected int bufPos = 1;

		// Token: 0x04000C32 RID: 3122
		protected int textPos = 1;

		// Token: 0x04000C33 RID: 3123
		protected int contentPos;

		// Token: 0x04000C34 RID: 3124
		protected int cdataPos;

		// Token: 0x04000C35 RID: 3125
		protected int attrEndPos;

		// Token: 0x04000C36 RID: 3126
		protected int bufLen = 6144;

		// Token: 0x04000C37 RID: 3127
		protected bool writeToNull;

		// Token: 0x04000C38 RID: 3128
		protected bool hadDoubleBracket;

		// Token: 0x04000C39 RID: 3129
		protected bool inAttributeValue;

		// Token: 0x04000C3A RID: 3130
		protected NewLineHandling newLineHandling;

		// Token: 0x04000C3B RID: 3131
		protected bool closeOutput;

		// Token: 0x04000C3C RID: 3132
		protected bool omitXmlDeclaration;

		// Token: 0x04000C3D RID: 3133
		protected string newLineChars;

		// Token: 0x04000C3E RID: 3134
		protected bool checkCharacters;

		// Token: 0x04000C3F RID: 3135
		protected XmlStandalone standalone;

		// Token: 0x04000C40 RID: 3136
		protected XmlOutputMethod outputMethod;

		// Token: 0x04000C41 RID: 3137
		protected bool autoXmlDeclaration;

		// Token: 0x04000C42 RID: 3138
		protected bool mergeCDataSections;

		// Token: 0x04000C43 RID: 3139
		private const int BUFSIZE = 6144;

		// Token: 0x04000C44 RID: 3140
		private const int ASYNCBUFSIZE = 65536;

		// Token: 0x04000C45 RID: 3141
		private const int OVERFLOW = 32;

		// Token: 0x04000C46 RID: 3142
		private const int INIT_MARKS_COUNT = 64;

		// Token: 0x0200011F RID: 287
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteXmlDeclarationAsync>d__86 : IAsyncStateMachine
		{
			// Token: 0x06000AE1 RID: 2785 RVA: 0x00048994 File Offset: 0x00046B94
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
						goto IL_117;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_18B;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1FA;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_26E;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2E7;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_34D;
					default:
						xmlUtf8RawTextWriter.CheckAsyncCall();
						if (xmlUtf8RawTextWriter.omitXmlDeclaration || xmlUtf8RawTextWriter.autoXmlDeclaration)
						{
							goto IL_354;
						}
						awaiter = xmlUtf8RawTextWriter.RawTextAsync("<?xml version=\"").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.RawTextAsync("1.0").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref awaiter, ref this);
						return;
					}
					IL_117:
					awaiter.GetResult();
					if (xmlUtf8RawTextWriter.encoding == null)
					{
						goto IL_201;
					}
					awaiter = xmlUtf8RawTextWriter.RawTextAsync("\" encoding=\"").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref awaiter, ref this);
						return;
					}
					IL_18B:
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.RawTextAsync(xmlUtf8RawTextWriter.encoding.WebName).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref awaiter, ref this);
						return;
					}
					IL_1FA:
					awaiter.GetResult();
					IL_201:
					if (this.standalone == XmlStandalone.Omit)
					{
						goto IL_2EE;
					}
					awaiter = xmlUtf8RawTextWriter.RawTextAsync("\" standalone=\"").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref awaiter, ref this);
						return;
					}
					IL_26E:
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.RawTextAsync((this.standalone == XmlStandalone.Yes) ? "yes" : "no").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref awaiter, ref this);
						return;
					}
					IL_2E7:
					awaiter.GetResult();
					IL_2EE:
					awaiter = xmlUtf8RawTextWriter.RawTextAsync("\"?>").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 6;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteXmlDeclarationAsync>d__86>(ref awaiter, ref this);
						return;
					}
					IL_34D:
					awaiter.GetResult();
					IL_354:;
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

			// Token: 0x06000AE2 RID: 2786 RVA: 0x00048D40 File Offset: 0x00046F40
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C47 RID: 3143
			public int <>1__state;

			// Token: 0x04000C48 RID: 3144
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C49 RID: 3145
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C4A RID: 3146
			public XmlStandalone standalone;

			// Token: 0x04000C4B RID: 3147
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000120 RID: 288
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteDocTypeAsync>d__88 : IAsyncStateMachine
		{
			// Token: 0x06000AE3 RID: 2787 RVA: 0x00048D50 File Offset: 0x00046F50
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
						goto IL_10A;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_17E;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1E8;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_251;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2C3;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_359;
					case 7:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3C3;
					case 8:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_491;
					default:
						xmlUtf8RawTextWriter.CheckAsyncCall();
						awaiter = xmlUtf8RawTextWriter.RawTextAsync("<!DOCTYPE ").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.RawTextAsync(this.name).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
						return;
					}
					IL_10A:
					awaiter.GetResult();
					int bufPos;
					if (this.pubid != null)
					{
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(" PUBLIC \"").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.sysid == null)
						{
							byte[] bufBytes = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter2 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter2.bufPos = bufPos + 1;
							bufBytes[bufPos] = 32;
							goto IL_406;
						}
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(" SYSTEM \"").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 6;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
							return;
						}
						goto IL_359;
					}
					IL_17E:
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.RawTextAsync(this.pubid).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
						return;
					}
					IL_1E8:
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.RawTextAsync("\" \"").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
						return;
					}
					IL_251:
					awaiter.GetResult();
					if (this.sysid == null)
					{
						goto IL_2CA;
					}
					awaiter = xmlUtf8RawTextWriter.RawTextAsync(this.sysid).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
						return;
					}
					IL_2C3:
					awaiter.GetResult();
					IL_2CA:
					byte[] bufBytes2 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter3 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter3.bufPos = bufPos + 1;
					bufBytes2[bufPos] = 34;
					goto IL_406;
					IL_359:
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.RawTextAsync(this.sysid).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 7;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
						return;
					}
					IL_3C3:
					awaiter.GetResult();
					byte[] bufBytes3 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter4 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter4.bufPos = bufPos + 1;
					bufBytes3[bufPos] = 34;
					IL_406:
					if (this.subset == null)
					{
						goto IL_4B5;
					}
					byte[] bufBytes4 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter5 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter5.bufPos = bufPos + 1;
					bufBytes4[bufPos] = 91;
					awaiter = xmlUtf8RawTextWriter.RawTextAsync(this.subset).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 8;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteDocTypeAsync>d__88>(ref awaiter, ref this);
						return;
					}
					IL_491:
					awaiter.GetResult();
					byte[] bufBytes5 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter6 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter6.bufPos = bufPos + 1;
					bufBytes5[bufPos] = 93;
					IL_4B5:
					byte[] bufBytes6 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter7 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter7.bufPos = bufPos + 1;
					bufBytes6[bufPos] = 62;
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

			// Token: 0x06000AE4 RID: 2788 RVA: 0x0004927C File Offset: 0x0004747C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C4C RID: 3148
			public int <>1__state;

			// Token: 0x04000C4D RID: 3149
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C4E RID: 3150
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C4F RID: 3151
			public string name;

			// Token: 0x04000C50 RID: 3152
			public string pubid;

			// Token: 0x04000C51 RID: 3153
			public string sysid;

			// Token: 0x04000C52 RID: 3154
			public string subset;

			// Token: 0x04000C53 RID: 3155
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000121 RID: 289
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteNamespaceDeclarationAsync>d__96 : IAsyncStateMachine
		{
			// Token: 0x06000AE5 RID: 2789 RVA: 0x0004928C File Offset: 0x0004748C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
						goto IL_F3;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_154;
					default:
						xmlUtf8RawTextWriter.CheckAsyncCall();
						awaiter = xmlUtf8RawTextWriter.WriteStartNamespaceDeclarationAsync(this.prefix).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteNamespaceDeclarationAsync>d__96>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.WriteStringAsync(this.namespaceName).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteNamespaceDeclarationAsync>d__96>(ref awaiter, ref this);
						return;
					}
					IL_F3:
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteNamespaceDeclarationAsync>d__96>(ref awaiter, ref this);
						return;
					}
					IL_154:
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

			// Token: 0x06000AE6 RID: 2790 RVA: 0x00049440 File Offset: 0x00047640
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C54 RID: 3156
			public int <>1__state;

			// Token: 0x04000C55 RID: 3157
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C56 RID: 3158
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C57 RID: 3159
			public string prefix;

			// Token: 0x04000C58 RID: 3160
			public string namespaceName;

			// Token: 0x04000C59 RID: 3161
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000122 RID: 290
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartNamespaceDeclarationAsync>d__97 : IAsyncStateMachine
		{
			// Token: 0x06000AE7 RID: 2791 RVA: 0x00049450 File Offset: 0x00047650
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
						goto IL_103;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_16D;
					default:
						xmlUtf8RawTextWriter.CheckAsyncCall();
						if (this.prefix.Length == 0)
						{
							awaiter = xmlUtf8RawTextWriter.RawTextAsync(" xmlns=\"").ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteStartNamespaceDeclarationAsync>d__97>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlUtf8RawTextWriter.RawTextAsync(" xmlns:").ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteStartNamespaceDeclarationAsync>d__97>(ref awaiter, ref this);
								return;
							}
							goto IL_103;
						}
						break;
					}
					awaiter.GetResult();
					goto IL_1AE;
					IL_103:
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.RawTextAsync(this.prefix).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteStartNamespaceDeclarationAsync>d__97>(ref awaiter, ref this);
						return;
					}
					IL_16D:
					awaiter.GetResult();
					byte[] bufBytes = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter2 = xmlUtf8RawTextWriter;
					int bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter2.bufPos = bufPos + 1;
					bufBytes[bufPos] = 61;
					byte[] bufBytes2 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter3 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter3.bufPos = bufPos + 1;
					bufBytes2[bufPos] = 34;
					IL_1AE:
					xmlUtf8RawTextWriter.inAttributeValue = true;
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

			// Token: 0x06000AE8 RID: 2792 RVA: 0x0004965C File Offset: 0x0004785C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C5A RID: 3162
			public int <>1__state;

			// Token: 0x04000C5B RID: 3163
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C5C RID: 3164
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C5D RID: 3165
			public string prefix;

			// Token: 0x04000C5E RID: 3166
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000123 RID: 291
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCDataAsync>d__99 : IAsyncStateMachine
		{
			// Token: 0x06000AE9 RID: 2793 RVA: 0x0004966C File Offset: 0x0004786C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					int bufPos;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlUtf8RawTextWriter.CheckAsyncCall();
						if (xmlUtf8RawTextWriter.mergeCDataSections && xmlUtf8RawTextWriter.bufPos == xmlUtf8RawTextWriter.cdataPos)
						{
							xmlUtf8RawTextWriter.bufPos -= 3;
						}
						else
						{
							byte[] bufBytes = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter2 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter2.bufPos = bufPos + 1;
							bufBytes[bufPos] = 60;
							byte[] bufBytes2 = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter3 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter3.bufPos = bufPos + 1;
							bufBytes2[bufPos] = 33;
							byte[] bufBytes3 = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter4 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter4.bufPos = bufPos + 1;
							bufBytes3[bufPos] = 91;
							byte[] bufBytes4 = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter5 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter5.bufPos = bufPos + 1;
							bufBytes4[bufPos] = 67;
							byte[] bufBytes5 = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter6 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter6.bufPos = bufPos + 1;
							bufBytes5[bufPos] = 68;
							byte[] bufBytes6 = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter7 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter7.bufPos = bufPos + 1;
							bufBytes6[bufPos] = 65;
							byte[] bufBytes7 = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter8 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter8.bufPos = bufPos + 1;
							bufBytes7[bufPos] = 84;
							byte[] bufBytes8 = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter9 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter9.bufPos = bufPos + 1;
							bufBytes8[bufPos] = 65;
							byte[] bufBytes9 = xmlUtf8RawTextWriter.bufBytes;
							XmlUtf8RawTextWriter xmlUtf8RawTextWriter10 = xmlUtf8RawTextWriter;
							bufPos = xmlUtf8RawTextWriter.bufPos;
							xmlUtf8RawTextWriter10.bufPos = bufPos + 1;
							bufBytes9[bufPos] = 91;
						}
						awaiter = xmlUtf8RawTextWriter.WriteCDataSectionAsync(this.text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCDataAsync>d__99>(ref awaiter, ref this);
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
					byte[] bufBytes10 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter11 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter11.bufPos = bufPos + 1;
					bufBytes10[bufPos] = 93;
					byte[] bufBytes11 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter12 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter12.bufPos = bufPos + 1;
					bufBytes11[bufPos] = 93;
					byte[] bufBytes12 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter13 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter13.bufPos = bufPos + 1;
					bufBytes12[bufPos] = 62;
					xmlUtf8RawTextWriter.textPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter.cdataPos = xmlUtf8RawTextWriter.bufPos;
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

			// Token: 0x06000AEA RID: 2794 RVA: 0x000498C4 File Offset: 0x00047AC4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C5F RID: 3167
			public int <>1__state;

			// Token: 0x04000C60 RID: 3168
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C61 RID: 3169
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C62 RID: 3170
			public string text;

			// Token: 0x04000C63 RID: 3171
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000124 RID: 292
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCommentAsync>d__100 : IAsyncStateMachine
		{
			// Token: 0x06000AEB RID: 2795 RVA: 0x000498D4 File Offset: 0x00047AD4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					int bufPos;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlUtf8RawTextWriter.CheckAsyncCall();
						byte[] bufBytes = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter2 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter2.bufPos = bufPos + 1;
						bufBytes[bufPos] = 60;
						byte[] bufBytes2 = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter3 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter3.bufPos = bufPos + 1;
						bufBytes2[bufPos] = 33;
						byte[] bufBytes3 = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter4 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter4.bufPos = bufPos + 1;
						bufBytes3[bufPos] = 45;
						byte[] bufBytes4 = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter5 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter5.bufPos = bufPos + 1;
						bufBytes4[bufPos] = 45;
						awaiter = xmlUtf8RawTextWriter.WriteCommentOrPiAsync(this.text, 45).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCommentAsync>d__100>(ref awaiter, ref this);
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
					byte[] bufBytes5 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter6 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter6.bufPos = bufPos + 1;
					bufBytes5[bufPos] = 45;
					byte[] bufBytes6 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter7 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter7.bufPos = bufPos + 1;
					bufBytes6[bufPos] = 45;
					byte[] bufBytes7 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter8 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter8.bufPos = bufPos + 1;
					bufBytes7[bufPos] = 62;
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

			// Token: 0x06000AEC RID: 2796 RVA: 0x00049A68 File Offset: 0x00047C68
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C64 RID: 3172
			public int <>1__state;

			// Token: 0x04000C65 RID: 3173
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C66 RID: 3174
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C67 RID: 3175
			public string text;

			// Token: 0x04000C68 RID: 3176
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000125 RID: 293
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteProcessingInstructionAsync>d__101 : IAsyncStateMachine
		{
			// Token: 0x06000AED RID: 2797 RVA: 0x00049A78 File Offset: 0x00047C78
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					int bufPos;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_151;
						}
						xmlUtf8RawTextWriter.CheckAsyncCall();
						byte[] bufBytes = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter2 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter2.bufPos = bufPos + 1;
						bufBytes[bufPos] = 60;
						byte[] bufBytes2 = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter3 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter3.bufPos = bufPos + 1;
						bufBytes2[bufPos] = 63;
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(this.name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteProcessingInstructionAsync>d__101>(ref awaiter, ref this);
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
					if (this.text.Length <= 0)
					{
						goto IL_158;
					}
					byte[] bufBytes3 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter4 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter4.bufPos = bufPos + 1;
					bufBytes3[bufPos] = 32;
					awaiter = xmlUtf8RawTextWriter.WriteCommentOrPiAsync(this.text, 63).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteProcessingInstructionAsync>d__101>(ref awaiter, ref this);
						return;
					}
					IL_151:
					awaiter.GetResult();
					IL_158:
					byte[] bufBytes4 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter5 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter5.bufPos = bufPos + 1;
					bufBytes4[bufPos] = 63;
					byte[] bufBytes5 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter6 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter6.bufPos = bufPos + 1;
					bufBytes5[bufPos] = 62;
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

			// Token: 0x06000AEE RID: 2798 RVA: 0x00049C5C File Offset: 0x00047E5C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C69 RID: 3177
			public int <>1__state;

			// Token: 0x04000C6A RID: 3178
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C6B RID: 3179
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C6C RID: 3180
			public string name;

			// Token: 0x04000C6D RID: 3181
			public string text;

			// Token: 0x04000C6E RID: 3182
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000126 RID: 294
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteEntityRefAsync>d__102 : IAsyncStateMachine
		{
			// Token: 0x06000AEF RID: 2799 RVA: 0x00049C6C File Offset: 0x00047E6C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					int bufPos;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_126;
						}
						xmlUtf8RawTextWriter.CheckAsyncCall();
						byte[] bufBytes = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter2 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter2.bufPos = bufPos + 1;
						bufBytes[bufPos] = 38;
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(this.name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteEntityRefAsync>d__102>(ref awaiter, ref this);
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
					byte[] bufBytes2 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter3 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter3.bufPos = bufPos + 1;
					bufBytes2[bufPos] = 59;
					if (xmlUtf8RawTextWriter.bufPos <= xmlUtf8RawTextWriter.bufLen)
					{
						goto IL_12D;
					}
					awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteEntityRefAsync>d__102>(ref awaiter, ref this);
						return;
					}
					IL_126:
					awaiter.GetResult();
					IL_12D:
					xmlUtf8RawTextWriter.textPos = xmlUtf8RawTextWriter.bufPos;
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

			// Token: 0x06000AF0 RID: 2800 RVA: 0x00049DFC File Offset: 0x00047FFC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C6F RID: 3183
			public int <>1__state;

			// Token: 0x04000C70 RID: 3184
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C71 RID: 3185
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C72 RID: 3186
			public string name;

			// Token: 0x04000C73 RID: 3187
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000127 RID: 295
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCharEntityAsync>d__103 : IAsyncStateMachine
		{
			// Token: 0x06000AF1 RID: 2801 RVA: 0x00049E0C File Offset: 0x0004800C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					int bufPos;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_19F;
						}
						xmlUtf8RawTextWriter.CheckAsyncCall();
						bufPos = (int)this.ch;
						string text = bufPos.ToString("X", NumberFormatInfo.InvariantInfo);
						if (xmlUtf8RawTextWriter.checkCharacters && !xmlUtf8RawTextWriter.xmlCharType.IsCharData(this.ch))
						{
							throw XmlConvert.CreateInvalidCharException(this.ch, '\0');
						}
						byte[] bufBytes = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter2 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter2.bufPos = bufPos + 1;
						bufBytes[bufPos] = 38;
						byte[] bufBytes2 = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter3 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter3.bufPos = bufPos + 1;
						bufBytes2[bufPos] = 35;
						byte[] bufBytes3 = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter4 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter4.bufPos = bufPos + 1;
						bufBytes3[bufPos] = 120;
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCharEntityAsync>d__103>(ref awaiter, ref this);
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
					byte[] bufBytes4 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter5 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter5.bufPos = bufPos + 1;
					bufBytes4[bufPos] = 59;
					if (xmlUtf8RawTextWriter.bufPos <= xmlUtf8RawTextWriter.bufLen)
					{
						goto IL_1A6;
					}
					awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCharEntityAsync>d__103>(ref awaiter, ref this);
						return;
					}
					IL_19F:
					awaiter.GetResult();
					IL_1A6:
					xmlUtf8RawTextWriter.textPos = xmlUtf8RawTextWriter.bufPos;
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

			// Token: 0x06000AF2 RID: 2802 RVA: 0x0004A018 File Offset: 0x00048218
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C74 RID: 3188
			public int <>1__state;

			// Token: 0x04000C75 RID: 3189
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C76 RID: 3190
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C77 RID: 3191
			public char ch;

			// Token: 0x04000C78 RID: 3192
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000128 RID: 296
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteSurrogateCharEntityAsync>d__106 : IAsyncStateMachine
		{
			// Token: 0x06000AF3 RID: 2803 RVA: 0x0004A028 File Offset: 0x00048228
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					int bufPos;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlUtf8RawTextWriter.CheckAsyncCall();
						int num2 = XmlCharType.CombineSurrogateChar((int)this.lowChar, (int)this.highChar);
						byte[] bufBytes = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter2 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter2.bufPos = bufPos + 1;
						bufBytes[bufPos] = 38;
						byte[] bufBytes2 = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter3 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter3.bufPos = bufPos + 1;
						bufBytes2[bufPos] = 35;
						byte[] bufBytes3 = xmlUtf8RawTextWriter.bufBytes;
						XmlUtf8RawTextWriter xmlUtf8RawTextWriter4 = xmlUtf8RawTextWriter;
						bufPos = xmlUtf8RawTextWriter.bufPos;
						xmlUtf8RawTextWriter4.bufPos = bufPos + 1;
						bufBytes3[bufPos] = 120;
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(num2.ToString("X", NumberFormatInfo.InvariantInfo)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteSurrogateCharEntityAsync>d__106>(ref awaiter, ref this);
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
					byte[] bufBytes4 = xmlUtf8RawTextWriter.bufBytes;
					XmlUtf8RawTextWriter xmlUtf8RawTextWriter5 = xmlUtf8RawTextWriter;
					bufPos = xmlUtf8RawTextWriter.bufPos;
					xmlUtf8RawTextWriter5.bufPos = bufPos + 1;
					bufBytes4[bufPos] = 59;
					xmlUtf8RawTextWriter.textPos = xmlUtf8RawTextWriter.bufPos;
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

			// Token: 0x06000AF4 RID: 2804 RVA: 0x0004A198 File Offset: 0x00048398
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C79 RID: 3193
			public int <>1__state;

			// Token: 0x04000C7A RID: 3194
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C7B RID: 3195
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C7C RID: 3196
			public char lowChar;

			// Token: 0x04000C7D RID: 3197
			public char highChar;

			// Token: 0x04000C7E RID: 3198
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000129 RID: 297
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawAsync>d__108 : IAsyncStateMachine
		{
			// Token: 0x06000AF5 RID: 2805 RVA: 0x0004A1A8 File Offset: 0x000483A8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlUtf8RawTextWriter.CheckAsyncCall();
						awaiter = xmlUtf8RawTextWriter.WriteRawWithCharCheckingAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteRawAsync>d__108>(ref awaiter, ref this);
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
					xmlUtf8RawTextWriter.textPos = xmlUtf8RawTextWriter.bufPos;
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

			// Token: 0x06000AF6 RID: 2806 RVA: 0x0004A28C File Offset: 0x0004848C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C7F RID: 3199
			public int <>1__state;

			// Token: 0x04000C80 RID: 3200
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C81 RID: 3201
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C82 RID: 3202
			public char[] buffer;

			// Token: 0x04000C83 RID: 3203
			public int index;

			// Token: 0x04000C84 RID: 3204
			public int count;

			// Token: 0x04000C85 RID: 3205
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200012A RID: 298
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawAsync>d__109 : IAsyncStateMachine
		{
			// Token: 0x06000AF7 RID: 2807 RVA: 0x0004A29C File Offset: 0x0004849C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlUtf8RawTextWriter.CheckAsyncCall();
						awaiter = xmlUtf8RawTextWriter.WriteRawWithCharCheckingAsync(this.data).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteRawAsync>d__109>(ref awaiter, ref this);
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
					xmlUtf8RawTextWriter.textPos = xmlUtf8RawTextWriter.bufPos;
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

			// Token: 0x06000AF8 RID: 2808 RVA: 0x0004A374 File Offset: 0x00048574
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C86 RID: 3206
			public int <>1__state;

			// Token: 0x04000C87 RID: 3207
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C88 RID: 3208
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C89 RID: 3209
			public string data;

			// Token: 0x04000C8A RID: 3210
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200012B RID: 299
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsync>d__110 : IAsyncStateMachine
		{
			// Token: 0x06000AF9 RID: 2809 RVA: 0x0004A384 File Offset: 0x00048584
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
						goto IL_E7;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_155;
					default:
						xmlUtf8RawTextWriter.CheckAsyncCall();
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<FlushAsync>d__110>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlUtf8RawTextWriter.FlushEncoderAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<FlushAsync>d__110>(ref awaiter, ref this);
						return;
					}
					IL_E7:
					awaiter.GetResult();
					if (xmlUtf8RawTextWriter.stream == null)
					{
						goto IL_15C;
					}
					awaiter = xmlUtf8RawTextWriter.stream.FlushAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<FlushAsync>d__110>(ref awaiter, ref this);
						return;
					}
					IL_155:
					awaiter.GetResult();
					IL_15C:;
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

			// Token: 0x06000AFA RID: 2810 RVA: 0x0004A538 File Offset: 0x00048738
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C8B RID: 3211
			public int <>1__state;

			// Token: 0x04000C8C RID: 3212
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C8D RID: 3213
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C8E RID: 3214
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200012C RID: 300
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushBufferAsync>d__111 : IAsyncStateMachine
		{
			// Token: 0x06000AFB RID: 2811 RVA: 0x0004A548 File Offset: 0x00048748
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							if (xmlUtf8RawTextWriter.writeToNull)
							{
								goto IL_94;
							}
							awaiter = xmlUtf8RawTextWriter.stream.WriteAsync(xmlUtf8RawTextWriter.bufBytes, 1, xmlUtf8RawTextWriter.bufPos - 1).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<FlushBufferAsync>d__111>(ref awaiter, ref this);
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
						IL_94:;
					}
					catch
					{
						xmlUtf8RawTextWriter.writeToNull = true;
						throw;
					}
					finally
					{
						if (num < 0)
						{
							xmlUtf8RawTextWriter.bufBytes[0] = xmlUtf8RawTextWriter.bufBytes[xmlUtf8RawTextWriter.bufPos - 1];
							if (XmlUtf8RawTextWriter.IsSurrogateByte(xmlUtf8RawTextWriter.bufBytes[0]))
							{
								xmlUtf8RawTextWriter.bufBytes[1] = xmlUtf8RawTextWriter.bufBytes[xmlUtf8RawTextWriter.bufPos];
								xmlUtf8RawTextWriter.bufBytes[2] = xmlUtf8RawTextWriter.bufBytes[xmlUtf8RawTextWriter.bufPos + 1];
								xmlUtf8RawTextWriter.bufBytes[3] = xmlUtf8RawTextWriter.bufBytes[xmlUtf8RawTextWriter.bufPos + 2];
							}
							xmlUtf8RawTextWriter.textPos = ((xmlUtf8RawTextWriter.textPos == xmlUtf8RawTextWriter.bufPos) ? 1 : 0);
							xmlUtf8RawTextWriter.attrEndPos = ((xmlUtf8RawTextWriter.attrEndPos == xmlUtf8RawTextWriter.bufPos) ? 1 : 0);
							xmlUtf8RawTextWriter.contentPos = 0;
							xmlUtf8RawTextWriter.cdataPos = 0;
							xmlUtf8RawTextWriter.bufPos = 1;
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
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000AFC RID: 2812 RVA: 0x0004A72C File Offset: 0x0004892C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C8F RID: 3215
			public int <>1__state;

			// Token: 0x04000C90 RID: 3216
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C91 RID: 3217
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C92 RID: 3218
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200012D RID: 301
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAttributeTextBlockAsync>d__116 : IAsyncStateMachine
		{
			// Token: 0x06000AFD RID: 2813 RVA: 0x0004A73C File Offset: 0x0004893C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_DA;
					}
					this.<writeLen>5__2 = 0;
					this.<curIndex>5__3 = this.index;
					this.<leftCount>5__4 = this.count;
					IL_33:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.WriteAttributeTextBlockNoFlush(this.chars, this.<curIndex>5__3, this.<leftCount>5__4);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<writeLen>5__2 < 0)
					{
						goto IL_E1;
					}
					awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteAttributeTextBlockAsync>d__116>(ref awaiter, ref this);
						return;
					}
					IL_DA:
					awaiter.GetResult();
					IL_E1:
					if (this.<writeLen>5__2 >= 0)
					{
						goto IL_33;
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

			// Token: 0x06000AFE RID: 2814 RVA: 0x0004A874 File Offset: 0x00048A74
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C93 RID: 3219
			public int <>1__state;

			// Token: 0x04000C94 RID: 3220
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C95 RID: 3221
			public int index;

			// Token: 0x04000C96 RID: 3222
			public int count;

			// Token: 0x04000C97 RID: 3223
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000C98 RID: 3224
			public char[] chars;

			// Token: 0x04000C99 RID: 3225
			private int <writeLen>5__2;

			// Token: 0x04000C9A RID: 3226
			private int <curIndex>5__3;

			// Token: 0x04000C9B RID: 3227
			private int <leftCount>5__4;

			// Token: 0x04000C9C RID: 3228
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200012E RID: 302
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_WriteAttributeTextBlockAsync>d__118 : IAsyncStateMachine
		{
			// Token: 0x06000AFF RID: 2815 RVA: 0x0004A884 File Offset: 0x00048A84
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
							goto IL_123;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<_WriteAttributeTextBlockAsync>d__118>(ref awaiter, ref this);
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
					IL_7C:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.WriteAttributeTextBlockNoFlush(this.text, this.curIndex, this.leftCount);
					this.curIndex += this.<writeLen>5__2;
					this.leftCount -= this.<writeLen>5__2;
					if (this.<writeLen>5__2 < 0)
					{
						goto IL_12A;
					}
					awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<_WriteAttributeTextBlockAsync>d__118>(ref awaiter, ref this);
						return;
					}
					IL_123:
					awaiter.GetResult();
					IL_12A:
					if (this.<writeLen>5__2 >= 0)
					{
						goto IL_7C;
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

			// Token: 0x06000B00 RID: 2816 RVA: 0x0004AA14 File Offset: 0x00048C14
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000C9D RID: 3229
			public int <>1__state;

			// Token: 0x04000C9E RID: 3230
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000C9F RID: 3231
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000CA0 RID: 3232
			public string text;

			// Token: 0x04000CA1 RID: 3233
			public int curIndex;

			// Token: 0x04000CA2 RID: 3234
			public int leftCount;

			// Token: 0x04000CA3 RID: 3235
			private int <writeLen>5__2;

			// Token: 0x04000CA4 RID: 3236
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200012F RID: 303
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteElementTextBlockAsync>d__122 : IAsyncStateMachine
		{
			// Token: 0x06000B01 RID: 2817 RVA: 0x0004AA24 File Offset: 0x00048C24
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_F9;
					}
					if (num == 1)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_189;
					}
					this.<writeLen>5__2 = 0;
					this.<curIndex>5__3 = this.index;
					this.<leftCount>5__4 = this.count;
					this.<needWriteNewLine>5__5 = false;
					IL_41:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.WriteElementTextBlockNoFlush(this.chars, this.<curIndex>5__3, this.<leftCount>5__4, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(xmlUtf8RawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteElementTextBlockAsync>d__122>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_190;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteElementTextBlockAsync>d__122>(ref awaiter, ref this);
							return;
						}
						goto IL_189;
					}
					IL_F9:
					awaiter.GetResult();
					int num2 = this.<curIndex>5__3;
					this.<curIndex>5__3 = num2 + 1;
					num2 = this.<leftCount>5__4;
					this.<leftCount>5__4 = num2 - 1;
					goto IL_190;
					IL_189:
					awaiter.GetResult();
					IL_190:
					if (this.<writeLen>5__2 >= 0 | this.<needWriteNewLine>5__5)
					{
						goto IL_41;
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

			// Token: 0x06000B02 RID: 2818 RVA: 0x0004AC24 File Offset: 0x00048E24
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CA5 RID: 3237
			public int <>1__state;

			// Token: 0x04000CA6 RID: 3238
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CA7 RID: 3239
			public int index;

			// Token: 0x04000CA8 RID: 3240
			public int count;

			// Token: 0x04000CA9 RID: 3241
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000CAA RID: 3242
			public char[] chars;

			// Token: 0x04000CAB RID: 3243
			private int <writeLen>5__2;

			// Token: 0x04000CAC RID: 3244
			private int <curIndex>5__3;

			// Token: 0x04000CAD RID: 3245
			private int <leftCount>5__4;

			// Token: 0x04000CAE RID: 3246
			private bool <needWriteNewLine>5__5;

			// Token: 0x04000CAF RID: 3247
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000130 RID: 304
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_WriteElementTextBlockAsync>d__124 : IAsyncStateMachine
		{
			// Token: 0x06000B03 RID: 2819 RVA: 0x0004AC34 File Offset: 0x00048E34
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
						goto IL_12A;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1E9;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_279;
					default:
						this.<writeLen>5__2 = 0;
						this.<needWriteNewLine>5__3 = false;
						if (this.newLine)
						{
							awaiter = xmlUtf8RawTextWriter.RawTextAsync(xmlUtf8RawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<_WriteElementTextBlockAsync>d__124>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<_WriteElementTextBlockAsync>d__124>(ref awaiter, ref this);
								return;
							}
							goto IL_12A;
						}
						break;
					}
					awaiter.GetResult();
					int num2 = this.curIndex;
					this.curIndex = num2 + 1;
					num2 = this.leftCount;
					this.leftCount = num2 - 1;
					goto IL_131;
					IL_12A:
					awaiter.GetResult();
					IL_131:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.WriteElementTextBlockNoFlush(this.text, this.curIndex, this.leftCount, out this.<needWriteNewLine>5__3);
					this.curIndex += this.<writeLen>5__2;
					this.leftCount -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__3)
					{
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(xmlUtf8RawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<_WriteElementTextBlockAsync>d__124>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_280;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<_WriteElementTextBlockAsync>d__124>(ref awaiter, ref this);
							return;
						}
						goto IL_279;
					}
					IL_1E9:
					awaiter.GetResult();
					num2 = this.curIndex;
					this.curIndex = num2 + 1;
					num2 = this.leftCount;
					this.leftCount = num2 - 1;
					goto IL_280;
					IL_279:
					awaiter.GetResult();
					IL_280:
					if (this.<writeLen>5__2 >= 0 | this.<needWriteNewLine>5__3)
					{
						goto IL_131;
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

			// Token: 0x06000B04 RID: 2820 RVA: 0x0004AF24 File Offset: 0x00049124
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CB0 RID: 3248
			public int <>1__state;

			// Token: 0x04000CB1 RID: 3249
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CB2 RID: 3250
			public bool newLine;

			// Token: 0x04000CB3 RID: 3251
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000CB4 RID: 3252
			public int curIndex;

			// Token: 0x04000CB5 RID: 3253
			public int leftCount;

			// Token: 0x04000CB6 RID: 3254
			public string text;

			// Token: 0x04000CB7 RID: 3255
			private int <writeLen>5__2;

			// Token: 0x04000CB8 RID: 3256
			private bool <needWriteNewLine>5__3;

			// Token: 0x04000CB9 RID: 3257
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000131 RID: 305
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_RawTextAsync>d__128 : IAsyncStateMachine
		{
			// Token: 0x06000B05 RID: 2821 RVA: 0x0004AF34 File Offset: 0x00049134
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
							goto IL_12A;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<_RawTextAsync>d__128>(ref awaiter, ref this);
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
					this.<writeLen>5__2 = 0;
					IL_83:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.RawTextNoFlush(this.text, this.curIndex, this.leftCount);
					this.curIndex += this.<writeLen>5__2;
					this.leftCount -= this.<writeLen>5__2;
					if (this.<writeLen>5__2 < 0)
					{
						goto IL_131;
					}
					awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<_RawTextAsync>d__128>(ref awaiter, ref this);
						return;
					}
					IL_12A:
					awaiter.GetResult();
					IL_131:
					if (this.<writeLen>5__2 >= 0)
					{
						goto IL_83;
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

			// Token: 0x06000B06 RID: 2822 RVA: 0x0004B0C8 File Offset: 0x000492C8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CBA RID: 3258
			public int <>1__state;

			// Token: 0x04000CBB RID: 3259
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CBC RID: 3260
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000CBD RID: 3261
			public string text;

			// Token: 0x04000CBE RID: 3262
			public int curIndex;

			// Token: 0x04000CBF RID: 3263
			public int leftCount;

			// Token: 0x04000CC0 RID: 3264
			private int <writeLen>5__2;

			// Token: 0x04000CC1 RID: 3265
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000132 RID: 306
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawWithCharCheckingAsync>d__132 : IAsyncStateMachine
		{
			// Token: 0x06000B07 RID: 2823 RVA: 0x0004B0D8 File Offset: 0x000492D8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_F9;
					}
					if (num == 1)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_189;
					}
					this.<writeLen>5__2 = 0;
					this.<curIndex>5__3 = this.index;
					this.<leftCount>5__4 = this.count;
					this.<needWriteNewLine>5__5 = false;
					IL_41:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.WriteRawWithCharCheckingNoFlush(this.chars, this.<curIndex>5__3, this.<leftCount>5__4, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(xmlUtf8RawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__132>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_190;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__132>(ref awaiter, ref this);
							return;
						}
						goto IL_189;
					}
					IL_F9:
					awaiter.GetResult();
					int num2 = this.<curIndex>5__3;
					this.<curIndex>5__3 = num2 + 1;
					num2 = this.<leftCount>5__4;
					this.<leftCount>5__4 = num2 - 1;
					goto IL_190;
					IL_189:
					awaiter.GetResult();
					IL_190:
					if (this.<writeLen>5__2 >= 0 | this.<needWriteNewLine>5__5)
					{
						goto IL_41;
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

			// Token: 0x06000B08 RID: 2824 RVA: 0x0004B2D8 File Offset: 0x000494D8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CC2 RID: 3266
			public int <>1__state;

			// Token: 0x04000CC3 RID: 3267
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CC4 RID: 3268
			public int index;

			// Token: 0x04000CC5 RID: 3269
			public int count;

			// Token: 0x04000CC6 RID: 3270
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000CC7 RID: 3271
			public char[] chars;

			// Token: 0x04000CC8 RID: 3272
			private int <writeLen>5__2;

			// Token: 0x04000CC9 RID: 3273
			private int <curIndex>5__3;

			// Token: 0x04000CCA RID: 3274
			private int <leftCount>5__4;

			// Token: 0x04000CCB RID: 3275
			private bool <needWriteNewLine>5__5;

			// Token: 0x04000CCC RID: 3276
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000133 RID: 307
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawWithCharCheckingAsync>d__133 : IAsyncStateMachine
		{
			// Token: 0x06000B09 RID: 2825 RVA: 0x0004B2E8 File Offset: 0x000494E8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_F9;
					}
					if (num == 1)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_189;
					}
					this.<writeLen>5__2 = 0;
					this.<curIndex>5__3 = 0;
					this.<leftCount>5__4 = this.text.Length;
					this.<needWriteNewLine>5__5 = false;
					IL_41:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.WriteRawWithCharCheckingNoFlush(this.text, this.<curIndex>5__3, this.<leftCount>5__4, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(xmlUtf8RawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__133>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_190;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteRawWithCharCheckingAsync>d__133>(ref awaiter, ref this);
							return;
						}
						goto IL_189;
					}
					IL_F9:
					awaiter.GetResult();
					int num2 = this.<curIndex>5__3;
					this.<curIndex>5__3 = num2 + 1;
					num2 = this.<leftCount>5__4;
					this.<leftCount>5__4 = num2 - 1;
					goto IL_190;
					IL_189:
					awaiter.GetResult();
					IL_190:
					if (this.<writeLen>5__2 >= 0 | this.<needWriteNewLine>5__5)
					{
						goto IL_41;
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

			// Token: 0x06000B0A RID: 2826 RVA: 0x0004B4E8 File Offset: 0x000496E8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CCD RID: 3277
			public int <>1__state;

			// Token: 0x04000CCE RID: 3278
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CCF RID: 3279
			public string text;

			// Token: 0x04000CD0 RID: 3280
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000CD1 RID: 3281
			private int <writeLen>5__2;

			// Token: 0x04000CD2 RID: 3282
			private int <curIndex>5__3;

			// Token: 0x04000CD3 RID: 3283
			private int <leftCount>5__4;

			// Token: 0x04000CD4 RID: 3284
			private bool <needWriteNewLine>5__5;

			// Token: 0x04000CD5 RID: 3285
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000134 RID: 308
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCommentOrPiAsync>d__135 : IAsyncStateMachine
		{
			// Token: 0x06000B0B RID: 2827 RVA: 0x0004B4F8 File Offset: 0x000496F8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
						goto IL_188;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_218;
					default:
						if (this.text.Length != 0)
						{
							this.<writeLen>5__2 = 0;
							this.<curIndex>5__3 = 0;
							this.<leftCount>5__4 = this.text.Length;
							this.<needWriteNewLine>5__5 = false;
							goto IL_CA;
						}
						if (xmlUtf8RawTextWriter.bufPos < xmlUtf8RawTextWriter.bufLen)
						{
							goto IL_9F;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCommentOrPiAsync>d__135>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					IL_9F:
					goto IL_252;
					IL_CA:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.WriteCommentOrPiNoFlush(this.text, this.<curIndex>5__3, this.<leftCount>5__4, this.stopChar, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(xmlUtf8RawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCommentOrPiAsync>d__135>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_21F;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCommentOrPiAsync>d__135>(ref awaiter, ref this);
							return;
						}
						goto IL_218;
					}
					IL_188:
					awaiter.GetResult();
					int num2 = this.<curIndex>5__3;
					this.<curIndex>5__3 = num2 + 1;
					num2 = this.<leftCount>5__4;
					this.<leftCount>5__4 = num2 - 1;
					goto IL_21F;
					IL_218:
					awaiter.GetResult();
					IL_21F:
					if (this.<writeLen>5__2 >= 0 | this.<needWriteNewLine>5__5)
					{
						goto IL_CA;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_252:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000B0C RID: 2828 RVA: 0x0004B788 File Offset: 0x00049988
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CD6 RID: 3286
			public int <>1__state;

			// Token: 0x04000CD7 RID: 3287
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CD8 RID: 3288
			public string text;

			// Token: 0x04000CD9 RID: 3289
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000CDA RID: 3290
			public int stopChar;

			// Token: 0x04000CDB RID: 3291
			private int <writeLen>5__2;

			// Token: 0x04000CDC RID: 3292
			private int <curIndex>5__3;

			// Token: 0x04000CDD RID: 3293
			private int <leftCount>5__4;

			// Token: 0x04000CDE RID: 3294
			private bool <needWriteNewLine>5__5;

			// Token: 0x04000CDF RID: 3295
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000135 RID: 309
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCDataSectionAsync>d__137 : IAsyncStateMachine
		{
			// Token: 0x06000B0D RID: 2829 RVA: 0x0004B798 File Offset: 0x00049998
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlUtf8RawTextWriter xmlUtf8RawTextWriter = this.<>4__this;
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
						goto IL_182;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_212;
					default:
						if (this.text.Length != 0)
						{
							this.<writeLen>5__2 = 0;
							this.<curIndex>5__3 = 0;
							this.<leftCount>5__4 = this.text.Length;
							this.<needWriteNewLine>5__5 = false;
							goto IL_CA;
						}
						if (xmlUtf8RawTextWriter.bufPos < xmlUtf8RawTextWriter.bufLen)
						{
							goto IL_9F;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCDataSectionAsync>d__137>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					IL_9F:
					goto IL_24C;
					IL_CA:
					this.<writeLen>5__2 = xmlUtf8RawTextWriter.WriteCDataSectionNoFlush(this.text, this.<curIndex>5__3, this.<leftCount>5__4, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlUtf8RawTextWriter.RawTextAsync(xmlUtf8RawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCDataSectionAsync>d__137>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_219;
						}
						awaiter = xmlUtf8RawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlUtf8RawTextWriter.<WriteCDataSectionAsync>d__137>(ref awaiter, ref this);
							return;
						}
						goto IL_212;
					}
					IL_182:
					awaiter.GetResult();
					int num2 = this.<curIndex>5__3;
					this.<curIndex>5__3 = num2 + 1;
					num2 = this.<leftCount>5__4;
					this.<leftCount>5__4 = num2 - 1;
					goto IL_219;
					IL_212:
					awaiter.GetResult();
					IL_219:
					if (this.<writeLen>5__2 >= 0 | this.<needWriteNewLine>5__5)
					{
						goto IL_CA;
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_24C:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000B0E RID: 2830 RVA: 0x0004BA20 File Offset: 0x00049C20
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000CE0 RID: 3296
			public int <>1__state;

			// Token: 0x04000CE1 RID: 3297
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000CE2 RID: 3298
			public string text;

			// Token: 0x04000CE3 RID: 3299
			public XmlUtf8RawTextWriter <>4__this;

			// Token: 0x04000CE4 RID: 3300
			private int <writeLen>5__2;

			// Token: 0x04000CE5 RID: 3301
			private int <curIndex>5__3;

			// Token: 0x04000CE6 RID: 3302
			private int <leftCount>5__4;

			// Token: 0x04000CE7 RID: 3303
			private bool <needWriteNewLine>5__5;

			// Token: 0x04000CE8 RID: 3304
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
