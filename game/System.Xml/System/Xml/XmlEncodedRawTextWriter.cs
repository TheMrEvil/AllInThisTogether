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
	// Token: 0x02000073 RID: 115
	internal class XmlEncodedRawTextWriter : XmlRawWriter
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x00014B88 File Offset: 0x00012D88
		protected XmlEncodedRawTextWriter(XmlWriterSettings settings)
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

		// Token: 0x0600044F RID: 1103 RVA: 0x00014C50 File Offset: 0x00012E50
		public XmlEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings) : this(settings)
		{
			this.writer = writer;
			this.encoding = writer.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufChars = new char[this.bufLen + 32];
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00014CBC File Offset: 0x00012EBC
		public XmlEncodedRawTextWriter(Stream stream, XmlWriterSettings settings) : this(settings)
		{
			this.stream = stream;
			this.encoding = settings.Encoding;
			if (settings.Async)
			{
				this.bufLen = 65536;
			}
			this.bufChars = new char[this.bufLen + 32];
			this.bufBytes = new byte[this.bufChars.Length];
			this.bufBytesUsed = 0;
			this.trackTextContent = true;
			this.inTextContent = false;
			this.lastMarkPos = 0;
			this.textContentMarks = new int[64];
			this.textContentMarks[0] = 1;
			this.charEntityFallback = new CharEntityEncoderFallback();
			this.encoding = (Encoding)settings.Encoding.Clone();
			this.encoding.EncoderFallback = this.charEntityFallback;
			this.encoder = this.encoding.GetEncoder();
			if (!stream.CanSeek || stream.Position == 0L)
			{
				byte[] preamble = this.encoding.GetPreamble();
				if (preamble.Length != 0)
				{
					this.stream.Write(preamble, 0, preamble.Length);
				}
			}
			if (settings.AutoXmlDeclaration)
			{
				this.WriteXmlDeclaration(this.standalone);
				this.autoXmlDeclaration = true;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00014DE0 File Offset: 0x00012FE0
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

		// Token: 0x06000452 RID: 1106 RVA: 0x00014E6C File Offset: 0x0001306C
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				if (this.trackTextContent && this.inTextContent)
				{
					this.ChangeTextContentMark(false);
				}
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

		// Token: 0x06000453 RID: 1107 RVA: 0x00014F0F File Offset: 0x0001310F
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				this.WriteProcessingInstruction("xml", xmldecl);
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00014F30 File Offset: 0x00013130
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
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
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 34;
			}
			else if (sysid != null)
			{
				this.RawText(" SYSTEM \"");
				this.RawText(sysid);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			else
			{
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
			}
			if (subset != null)
			{
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 91;
				this.RawText(subset);
				char[] array5 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 93;
			}
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 62;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00015054 File Offset: 0x00013254
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000150D4 File Offset: 0x000132D4
		internal override void StartElementContent()
		{
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 62;
			this.contentPos = this.bufPos;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00015108 File Offset: 0x00013308
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.contentPos != this.bufPos)
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				if (prefix != null && prefix.Length != 0)
				{
					this.RawText(prefix);
					char[] array3 = this.bufChars;
					num = this.bufPos;
					this.bufPos = num + 1;
					array3[num] = 58;
				}
				this.RawText(localName);
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 62;
				return;
			}
			this.bufPos--;
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 32;
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 47;
			char[] array7 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001521C File Offset: 0x0001341C
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				this.RawText(prefix);
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 58;
			}
			this.RawText(localName);
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 62;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000152C4 File Offset: 0x000134C4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.attrEndPos == this.bufPos)
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			if (prefix != null && prefix.Length > 0)
			{
				this.RawText(prefix);
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 58;
			}
			this.RawText(localName);
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 61;
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00015380 File Offset: 0x00013580
		public override void WriteEndAttribute()
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000153D1 File Offset: 0x000135D1
		internal override void WriteNamespaceDeclaration(string prefix, string namespaceName)
		{
			this.WriteStartNamespaceDeclaration(prefix);
			this.WriteString(namespaceName);
			this.WriteEndNamespaceDeclaration();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001222F File Offset: 0x0001042F
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000153E8 File Offset: 0x000135E8
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (prefix.Length == 0)
			{
				this.RawText(" xmlns=\"");
			}
			else
			{
				this.RawText(" xmlns:");
				this.RawText(prefix);
				char[] array = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 61;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 34;
			}
			this.inAttributeValue = true;
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00015488 File Offset: 0x00013688
		internal override void WriteEndNamespaceDeclaration()
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.inAttributeValue = false;
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000154DC File Offset: 0x000136DC
		public override void WriteCData(string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.mergeCDataSections && this.bufPos == this.cdataPos)
			{
				this.bufPos -= 3;
			}
			else
			{
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 60;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 33;
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 91;
				char[] array4 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array4[num] = 67;
				char[] array5 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array5[num] = 68;
				char[] array6 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array6[num] = 65;
				char[] array7 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array7[num] = 84;
				char[] array8 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array8[num] = 65;
				char[] array9 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array9[num] = 91;
			}
			this.WriteCDataSection(text);
			char[] array10 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array10[num] = 93;
			char[] array11 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array11[num] = 93;
			char[] array12 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array12[num] = 62;
			this.textPos = this.bufPos;
			this.cdataPos = this.bufPos;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00015680 File Offset: 0x00013880
		public override void WriteComment(string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 33;
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 45;
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 45;
			this.WriteCommentOrPi(text, 45);
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 45;
			char[] array6 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array6[num] = 45;
			char[] array7 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array7[num] = 62;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00015764 File Offset: 0x00013964
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 63;
			this.RawText(name);
			if (text.Length > 0)
			{
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 32;
				this.WriteCommentOrPi(text, 63);
			}
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 63;
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 62;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00015824 File Offset: 0x00013A24
		public override void WriteEntityRef(string name)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			this.RawText(name);
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000158A4 File Offset: 0x00013AA4
		public override void WriteCharEntity(char ch)
		{
			int num = (int)ch;
			string s = num.ToString("X", NumberFormatInfo.InvariantInfo);
			if (this.checkCharacters && !this.xmlCharType.IsCharData(ch))
			{
				throw XmlConvert.CreateInvalidCharException(ch, '\0');
			}
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 38;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 35;
			char[] array3 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array3[num] = 120;
			this.RawText(s);
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 59;
			if (this.bufPos > this.bufLen)
			{
				this.FlushBuffer();
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001598C File Offset: 0x00013B8C
		public unsafe override void WriteWhitespace(string ws)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
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

		// Token: 0x06000465 RID: 1125 RVA: 0x000159E8 File Offset: 0x00013BE8
		public unsafe override void WriteString(string text)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
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
					this.WriteAttributeTextBlock(ptr, pSrcEnd);
				}
				else
				{
					this.WriteElementTextBlock(ptr, pSrcEnd);
				}
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00015A44 File Offset: 0x00013C44
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num = XmlCharType.CombineSurrogateChar((int)lowChar, (int)highChar);
			char[] array = this.bufChars;
			int num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array[num2] = 38;
			char[] array2 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array2[num2] = 35;
			char[] array3 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array3[num2] = 120;
			this.RawText(num.ToString("X", NumberFormatInfo.InvariantInfo));
			char[] array4 = this.bufChars;
			num2 = this.bufPos;
			this.bufPos = num2 + 1;
			array4[num2] = 59;
			this.textPos = this.bufPos;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00015AFC File Offset: 0x00013CFC
		public unsafe override void WriteChars(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
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

		// Token: 0x06000468 RID: 1128 RVA: 0x00015B54 File Offset: 0x00013D54
		public unsafe override void WriteRaw(char[] buffer, int index, int count)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			fixed (char* ptr = &buffer[index])
			{
				char* ptr2 = ptr;
				this.WriteRawWithCharChecking(ptr2, ptr2 + count);
			}
			this.textPos = this.bufPos;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00015BA0 File Offset: 0x00013DA0
		public unsafe override void WriteRaw(string data)
		{
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
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

		// Token: 0x0600046A RID: 1130 RVA: 0x00015BF4 File Offset: 0x00013DF4
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
						goto IL_7D;
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
				if (this.writer != null)
				{
					try
					{
						this.writer.Flush();
					}
					finally
					{
						try
						{
							if (this.closeOutput)
							{
								this.writer.Close();
							}
						}
						finally
						{
							this.writer = null;
						}
					}
				}
				IL_7D:;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00015CC0 File Offset: 0x00013EC0
		public override void Flush()
		{
			this.FlushBuffer();
			this.FlushEncoder();
			if (this.stream != null)
			{
				this.stream.Flush();
				return;
			}
			if (this.writer != null)
			{
				this.writer.Flush();
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015CF8 File Offset: 0x00013EF8
		protected virtual void FlushBuffer()
		{
			try
			{
				if (!this.writeToNull)
				{
					if (this.stream != null)
					{
						if (this.trackTextContent)
						{
							this.charEntityFallback.Reset(this.textContentMarks, this.lastMarkPos);
							if ((this.lastMarkPos & 1) != 0)
							{
								this.textContentMarks[1] = 1;
								this.lastMarkPos = 1;
							}
							else
							{
								this.lastMarkPos = 0;
							}
						}
						this.EncodeChars(1, this.bufPos, true);
					}
					else
					{
						this.writer.Write(this.bufChars, 1, this.bufPos - 1);
					}
				}
			}
			catch
			{
				this.writeToNull = true;
				throw;
			}
			finally
			{
				this.bufChars[0] = this.bufChars[this.bufPos - 1];
				this.textPos = ((this.textPos == this.bufPos) ? 1 : 0);
				this.attrEndPos = ((this.attrEndPos == this.bufPos) ? 1 : 0);
				this.contentPos = 0;
				this.cdataPos = 0;
				this.bufPos = 1;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00015E08 File Offset: 0x00014008
		private void EncodeChars(int startOffset, int endOffset, bool writeAllToStream)
		{
			while (startOffset < endOffset)
			{
				if (this.charEntityFallback != null)
				{
					this.charEntityFallback.StartOffset = startOffset;
				}
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, startOffset, endOffset - startOffset, this.bufBytes, this.bufBytesUsed, this.bufBytes.Length - this.bufBytesUsed, false, out num, out num2, out flag);
				startOffset += num;
				this.bufBytesUsed += num2;
				if (this.bufBytesUsed >= this.bufBytes.Length - 16)
				{
					this.stream.Write(this.bufBytes, 0, this.bufBytesUsed);
					this.bufBytesUsed = 0;
				}
			}
			if (writeAllToStream && this.bufBytesUsed > 0)
			{
				this.stream.Write(this.bufBytes, 0, this.bufBytesUsed);
				this.bufBytesUsed = 0;
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015EDC File Offset: 0x000140DC
		private void FlushEncoder()
		{
			if (this.stream != null)
			{
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, 1, 0, this.bufBytes, 0, this.bufBytes.Length, true, out num, out num2, out flag);
				if (num2 != 0)
				{
					this.stream.Write(this.bufBytes, 0, num2);
				}
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00015F30 File Offset: 0x00014130
		protected unsafe void WriteAttributeTextBlock(char* pSrc, char* pSrcEnd)
		{
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
			int num = 0;
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr2 = (char)num;
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
								*ptr2 = (char)num;
								ptr2++;
								goto IL_1D3;
							}
							ptr2 = XmlEncodedRawTextWriter.TabEntity(ptr2);
							goto IL_1D3;
						case 10:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (char)num;
								ptr2++;
								goto IL_1D3;
							}
							ptr2 = XmlEncodedRawTextWriter.LineFeedEntity(ptr2);
							goto IL_1D3;
						case 11:
						case 12:
							break;
						case 13:
							if (this.newLineHandling == NewLineHandling.None)
							{
								*ptr2 = (char)num;
								ptr2++;
								goto IL_1D3;
							}
							ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
							goto IL_1D3;
						default:
							if (num == 34)
							{
								ptr2 = XmlEncodedRawTextWriter.QuoteEntity(ptr2);
								goto IL_1D3;
							}
							if (num == 38)
							{
								ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
								goto IL_1D3;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							*ptr2 = (char)num;
							ptr2++;
							goto IL_1D3;
						}
						if (num == 60)
						{
							ptr2 = XmlEncodedRawTextWriter.LtEntity(ptr2);
							goto IL_1D3;
						}
						if (num == 62)
						{
							ptr2 = XmlEncodedRawTextWriter.GtEntity(ptr2);
							goto IL_1D3;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					*ptr2 = (char)num;
					ptr2++;
					pSrc++;
					continue;
					IL_1D3:
					pSrc++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001612C File Offset: 0x0001432C
		protected unsafe void WriteElementTextBlock(char* pSrc, char* pSrcEnd)
		{
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
			int num = 0;
			for (;;)
			{
				char* ptr3 = ptr2 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr3 != ptr + this.bufLen)
				{
					ptr3 = ptr + this.bufLen;
				}
				while (ptr2 < ptr3 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr2 = (char)num;
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
							goto IL_10F;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr2 = this.WriteNewLine(ptr2);
								goto IL_1D6;
							}
							*ptr2 = (char)num;
							ptr2++;
							goto IL_1D6;
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
								goto IL_1D6;
							case NewLineHandling.Entitize:
								ptr2 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr2);
								goto IL_1D6;
							case NewLineHandling.None:
								*ptr2 = (char)num;
								ptr2++;
								goto IL_1D6;
							default:
								goto IL_1D6;
							}
							break;
						default:
							if (num == 34)
							{
								goto IL_10F;
							}
							if (num == 38)
							{
								ptr2 = XmlEncodedRawTextWriter.AmpEntity(ptr2);
								goto IL_1D6;
							}
							break;
						}
					}
					else
					{
						if (num == 39)
						{
							goto IL_10F;
						}
						if (num == 60)
						{
							ptr2 = XmlEncodedRawTextWriter.LtEntity(ptr2);
							goto IL_1D6;
						}
						if (num == 62)
						{
							ptr2 = XmlEncodedRawTextWriter.GtEntity(ptr2);
							goto IL_1D6;
						}
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr2);
						pSrc += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr2 = this.InvalidXmlChar(num, ptr2, true);
						pSrc++;
						continue;
					}
					*ptr2 = (char)num;
					ptr2++;
					pSrc++;
					continue;
					IL_1D6:
					pSrc++;
					continue;
					IL_10F:
					*ptr2 = (char)num;
					ptr2++;
					goto IL_1D6;
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

		// Token: 0x06000471 RID: 1137 RVA: 0x0001633C File Offset: 0x0001453C
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

		// Token: 0x06000472 RID: 1138 RVA: 0x00016370 File Offset: 0x00014570
		protected unsafe void RawText(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr2 + (long)(pSrcEnd - ptr3) * 2L / 2L;
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) < 55296)
				{
					ptr3++;
					*ptr2 = (char)num;
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
					ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					*ptr2 = (char)num;
					ptr2++;
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001647C File Offset: 0x0001467C
		protected unsafe void WriteRawWithCharChecking(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr2 = pSrcBegin;
			char* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - ptr2) * 2L / 2L;
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0)
				{
					*ptr3 = (char)num;
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
							goto IL_DF;
						case 10:
							if (this.newLineHandling == NewLineHandling.Replace)
							{
								ptr3 = this.WriteNewLine(ptr3);
								goto IL_186;
							}
							*ptr3 = (char)num;
							ptr3++;
							goto IL_186;
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
								goto IL_186;
							}
							*ptr3 = (char)num;
							ptr3++;
							goto IL_186;
						default:
							if (num == 38)
							{
								goto IL_DF;
							}
							break;
						}
					}
					else if (num == 60 || num == 93)
					{
						goto IL_DF;
					}
					if (XmlCharType.IsSurrogate(num))
					{
						ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
						ptr2 += 2;
						continue;
					}
					if (num <= 127 || num >= 65534)
					{
						ptr3 = this.InvalidXmlChar(num, ptr3, false);
						ptr2++;
						continue;
					}
					*ptr3 = (char)num;
					ptr3++;
					ptr2++;
					continue;
					IL_186:
					ptr2++;
					continue;
					IL_DF:
					*ptr3 = (char)num;
					ptr3++;
					goto IL_186;
				}
				this.bufPos = (int)((long)(ptr3 - ptr));
				this.FlushBuffer();
				ptr3 = ptr + 1;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00016628 File Offset: 0x00014828
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
				char[] array;
				char* ptr2;
				if ((array = this.bufChars) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				char* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					char* ptr6 = ptr5 + (long)(ptr4 - ptr3) * 2L / 2L;
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 64) != 0 && num != stopChar)
					{
						*ptr5 = (char)num;
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
								goto IL_226;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_296;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_296;
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
									goto IL_296;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_296;
							default:
								if (num == 38)
								{
									goto IL_226;
								}
								if (num == 45)
								{
									*ptr5 = '-';
									ptr5++;
									if (num == stopChar && (ptr3 + 1 == ptr4 || ptr3[1] == '-'))
									{
										*ptr5 = ' ';
										ptr5++;
										goto IL_296;
									}
									goto IL_296;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_226;
							}
							if (num != 63)
							{
								if (num == 93)
								{
									*ptr5 = ']';
									ptr5++;
									goto IL_296;
								}
							}
							else
							{
								*ptr5 = '?';
								ptr5++;
								if (num == stopChar && ptr3 + 1 < ptr4 && ptr3[1] == '>')
								{
									*ptr5 = ' ';
									ptr5++;
									goto IL_296;
								}
								goto IL_296;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						*ptr5 = (char)num;
						ptr5++;
						ptr3++;
						continue;
						IL_296:
						ptr3++;
						continue;
						IL_226:
						*ptr5 = (char)num;
						ptr5++;
						goto IL_296;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x000168E8 File Offset: 0x00014AE8
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
				char[] array;
				char* ptr2;
				if ((array = this.bufChars) == null || array.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array[0];
				}
				char* ptr3 = ptr;
				char* ptr4 = ptr + text.Length;
				char* ptr5 = ptr2 + this.bufPos;
				int num = 0;
				for (;;)
				{
					char* ptr6 = ptr5 + (long)(ptr4 - ptr3) * 2L / 2L;
					if (ptr6 != ptr2 + this.bufLen)
					{
						ptr6 = ptr2 + this.bufLen;
					}
					while (ptr5 < ptr6 && (this.xmlCharType.charProperties[num = (int)(*ptr3)] & 128) != 0 && num != 93)
					{
						*ptr5 = (char)num;
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
								goto IL_210;
							case 10:
								if (this.newLineHandling == NewLineHandling.Replace)
								{
									ptr5 = this.WriteNewLine(ptr5);
									goto IL_280;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_280;
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
									goto IL_280;
								}
								*ptr5 = (char)num;
								ptr5++;
								goto IL_280;
							default:
								if (num == 34 || num - 38 <= 1)
								{
									goto IL_210;
								}
								break;
							}
						}
						else
						{
							if (num == 60)
							{
								goto IL_210;
							}
							if (num == 62)
							{
								if (this.hadDoubleBracket && ptr5[-1] == ']')
								{
									ptr5 = XmlEncodedRawTextWriter.RawEndCData(ptr5);
									ptr5 = XmlEncodedRawTextWriter.RawStartCData(ptr5);
								}
								*ptr5 = '>';
								ptr5++;
								goto IL_280;
							}
							if (num == 93)
							{
								if (ptr5[-1] == ']')
								{
									this.hadDoubleBracket = true;
								}
								else
								{
									this.hadDoubleBracket = false;
								}
								*ptr5 = ']';
								ptr5++;
								goto IL_280;
							}
						}
						if (XmlCharType.IsSurrogate(num))
						{
							ptr5 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, ptr4, ptr5);
							ptr3 += 2;
							continue;
						}
						if (num <= 127 || num >= 65534)
						{
							ptr5 = this.InvalidXmlChar(num, ptr5, false);
							ptr3++;
							continue;
						}
						*ptr5 = (char)num;
						ptr5++;
						ptr3++;
						continue;
						IL_280:
						ptr3++;
						continue;
						IL_210:
						*ptr5 = (char)num;
						ptr5++;
						goto IL_280;
					}
					this.bufPos = (int)((long)(ptr5 - ptr2));
					this.FlushBuffer();
					ptr5 = ptr2 + 1;
				}
				this.bufPos = (int)((long)(ptr5 - ptr2));
				array = null;
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00016B94 File Offset: 0x00014D94
		private unsafe static char* EncodeSurrogate(char* pSrc, char* pSrcEnd, char* pDst)
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
				*pDst = (char)num;
				pDst[1] = (char)num2;
				pDst += 2;
				return pDst;
			}
			throw XmlConvert.CreateInvalidSurrogatePairException((char)num2, (char)num);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00016C03 File Offset: 0x00014E03
		private unsafe char* InvalidXmlChar(int ch, char* pDst, bool entitize)
		{
			if (this.checkCharacters)
			{
				throw XmlConvert.CreateInvalidCharException((char)ch, '\0');
			}
			if (entitize)
			{
				return XmlEncodedRawTextWriter.CharEntity(pDst, (char)ch);
			}
			*pDst = (char)ch;
			pDst++;
			return pDst;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00016C2C File Offset: 0x00014E2C
		internal unsafe void EncodeChar(ref char* pSrc, char* pSrcEnd, ref char* pDst)
		{
			int num = (int)(*pSrc);
			if (XmlCharType.IsSurrogate(num))
			{
				pDst = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, pDst);
				pSrc += (IntPtr)2 * 2;
				return;
			}
			if (num <= 127 || num >= 65534)
			{
				pDst = this.InvalidXmlChar(num, pDst, false);
				pSrc += 2;
				return;
			}
			*pDst = (short)((ushort)num);
			pDst += 2;
			pSrc += 2;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00016C8C File Offset: 0x00014E8C
		protected void ChangeTextContentMark(bool value)
		{
			this.inTextContent = value;
			if (this.lastMarkPos + 1 == this.textContentMarks.Length)
			{
				this.GrowTextContentMarks();
			}
			int[] array = this.textContentMarks;
			int num = this.lastMarkPos + 1;
			this.lastMarkPos = num;
			array[num] = this.bufPos;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00016CD8 File Offset: 0x00014ED8
		private void GrowTextContentMarks()
		{
			int[] destinationArray = new int[this.textContentMarks.Length * 2];
			Array.Copy(this.textContentMarks, destinationArray, this.textContentMarks.Length);
			this.textContentMarks = destinationArray;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00016D10 File Offset: 0x00014F10
		protected unsafe char* WriteNewLine(char* pDst)
		{
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
			this.bufPos = (int)((long)(pDst - ptr));
			this.RawText(this.newLineChars);
			return ptr + this.bufPos;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00016D5E File Offset: 0x00014F5E
		protected unsafe static char* LtEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'l';
			pDst[2] = 't';
			pDst[3] = ';';
			return pDst + 4;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00016D82 File Offset: 0x00014F82
		protected unsafe static char* GtEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'g';
			pDst[2] = 't';
			pDst[3] = ';';
			return pDst + 4;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00016DA6 File Offset: 0x00014FA6
		protected unsafe static char* AmpEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'a';
			pDst[2] = 'm';
			pDst[3] = 'p';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00016DD3 File Offset: 0x00014FD3
		protected unsafe static char* QuoteEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = 'q';
			pDst[2] = 'u';
			pDst[3] = 'o';
			pDst[4] = 't';
			pDst[5] = ';';
			return pDst + 6;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00016E09 File Offset: 0x00015009
		protected unsafe static char* TabEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = '9';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00016E36 File Offset: 0x00015036
		protected unsafe static char* LineFeedEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = 'A';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00016E63 File Offset: 0x00015063
		protected unsafe static char* CarriageReturnEntity(char* pDst)
		{
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst[3] = 'D';
			pDst[4] = ';';
			return pDst + 5;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00016E90 File Offset: 0x00015090
		private unsafe static char* CharEntity(char* pDst, char ch)
		{
			int num = (int)ch;
			string text = num.ToString("X", NumberFormatInfo.InvariantInfo);
			*pDst = '&';
			pDst[1] = '#';
			pDst[2] = 'x';
			pDst += 3;
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = ptr;
				while ((*(pDst++) = *(ptr2++)) != '\0')
				{
				}
			}
			pDst[-1] = ';';
			return pDst;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016EFC File Offset: 0x000150FC
		protected unsafe static char* RawStartCData(char* pDst)
		{
			*pDst = '<';
			pDst[1] = '!';
			pDst[2] = '[';
			pDst[3] = 'C';
			pDst[4] = 'D';
			pDst[5] = 'A';
			pDst[6] = 'T';
			pDst[7] = 'A';
			pDst[8] = '[';
			return pDst + 9;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016F59 File Offset: 0x00015159
		protected unsafe static char* RawEndCData(char* pDst)
		{
			*pDst = ']';
			pDst[1] = ']';
			pDst[2] = '>';
			return pDst + 3;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016F74 File Offset: 0x00015174
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

		// Token: 0x06000487 RID: 1159 RVA: 0x000170CE File Offset: 0x000152CE
		protected void CheckAsyncCall()
		{
			if (!this.useAsync)
			{
				throw new InvalidOperationException(Res.GetString("Set XmlWriterSettings.Async to true if you want to use Async Methods."));
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000170E8 File Offset: 0x000152E8
		internal override Task WriteXmlDeclarationAsync(XmlStandalone standalone)
		{
			XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96 <WriteXmlDeclarationAsync>d__;
			<WriteXmlDeclarationAsync>d__.<>4__this = this;
			<WriteXmlDeclarationAsync>d__.standalone = standalone;
			<WriteXmlDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteXmlDeclarationAsync>d__.<>1__state = -1;
			<WriteXmlDeclarationAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref <WriteXmlDeclarationAsync>d__);
			return <WriteXmlDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00017133 File Offset: 0x00015333
		internal override Task WriteXmlDeclarationAsync(string xmldecl)
		{
			this.CheckAsyncCall();
			if (!this.omitXmlDeclaration && !this.autoXmlDeclaration)
			{
				return this.WriteProcessingInstructionAsync("xml", xmldecl);
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00017160 File Offset: 0x00015360
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98 <WriteDocTypeAsync>d__;
			<WriteDocTypeAsync>d__.<>4__this = this;
			<WriteDocTypeAsync>d__.name = name;
			<WriteDocTypeAsync>d__.pubid = pubid;
			<WriteDocTypeAsync>d__.sysid = sysid;
			<WriteDocTypeAsync>d__.subset = subset;
			<WriteDocTypeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteDocTypeAsync>d__.<>1__state = -1;
			<WriteDocTypeAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref <WriteDocTypeAsync>d__);
			return <WriteDocTypeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x000171C4 File Offset: 0x000153C4
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
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

		// Token: 0x0600048C RID: 1164 RVA: 0x00017242 File Offset: 0x00015442
		private void WriteStartElementAsync_SetAttEndPos()
		{
			this.attrEndPos = this.bufPos;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00017250 File Offset: 0x00015450
		internal override Task WriteEndElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			int num;
			if (this.contentPos == this.bufPos)
			{
				this.bufPos--;
				char[] array = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
				char[] array2 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array2[num] = 47;
				char[] array3 = this.bufChars;
				num = this.bufPos;
				this.bufPos = num + 1;
				array3[num] = 62;
				return AsyncHelper.DoneTask;
			}
			char[] array4 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array4[num] = 60;
			char[] array5 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array5[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				return this.RawTextAsync(prefix + ":" + localName + ">");
			}
			return this.RawTextAsync(localName + ">");
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00017354 File Offset: 0x00015554
		internal override Task WriteFullEndElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 60;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 47;
			if (prefix != null && prefix.Length != 0)
			{
				return this.RawTextAsync(prefix + ":" + localName + ">");
			}
			return this.RawTextAsync(localName + ">");
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000173E8 File Offset: 0x000155E8
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.attrEndPos == this.bufPos)
			{
				char[] array = this.bufChars;
				int num = this.bufPos;
				this.bufPos = num + 1;
				array[num] = 32;
			}
			Task task;
			if (prefix != null && prefix.Length > 0)
			{
				task = this.RawTextAsync(prefix + ":" + localName);
			}
			else
			{
				task = this.RawTextAsync(localName);
			}
			return task.CallVoidFuncWhenFinish(new Action(this.WriteStartAttribute_SetInAttribute));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00017478 File Offset: 0x00015678
		private void WriteStartAttribute_SetInAttribute()
		{
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 61;
			char[] array2 = this.bufChars;
			num = this.bufPos;
			this.bufPos = num + 1;
			array2[num] = 34;
			this.inAttributeValue = true;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x000174C0 File Offset: 0x000156C0
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.inAttributeValue = false;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001751C File Offset: 0x0001571C
		internal override Task WriteNamespaceDeclarationAsync(string prefix, string namespaceName)
		{
			XmlEncodedRawTextWriter.<WriteNamespaceDeclarationAsync>d__106 <WriteNamespaceDeclarationAsync>d__;
			<WriteNamespaceDeclarationAsync>d__.<>4__this = this;
			<WriteNamespaceDeclarationAsync>d__.prefix = prefix;
			<WriteNamespaceDeclarationAsync>d__.namespaceName = namespaceName;
			<WriteNamespaceDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteNamespaceDeclarationAsync>d__.<>1__state = -1;
			<WriteNamespaceDeclarationAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteNamespaceDeclarationAsync>d__106>(ref <WriteNamespaceDeclarationAsync>d__);
			return <WriteNamespaceDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00017570 File Offset: 0x00015770
		internal override Task WriteStartNamespaceDeclarationAsync(string prefix)
		{
			XmlEncodedRawTextWriter.<WriteStartNamespaceDeclarationAsync>d__107 <WriteStartNamespaceDeclarationAsync>d__;
			<WriteStartNamespaceDeclarationAsync>d__.<>4__this = this;
			<WriteStartNamespaceDeclarationAsync>d__.prefix = prefix;
			<WriteStartNamespaceDeclarationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteStartNamespaceDeclarationAsync>d__.<>1__state = -1;
			<WriteStartNamespaceDeclarationAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteStartNamespaceDeclarationAsync>d__107>(ref <WriteStartNamespaceDeclarationAsync>d__);
			return <WriteStartNamespaceDeclarationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000175BC File Offset: 0x000157BC
		internal override Task WriteEndNamespaceDeclarationAsync()
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			this.inAttributeValue = false;
			char[] array = this.bufChars;
			int num = this.bufPos;
			this.bufPos = num + 1;
			array[num] = 34;
			this.attrEndPos = this.bufPos;
			return AsyncHelper.DoneTask;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00017618 File Offset: 0x00015818
		public override Task WriteCDataAsync(string text)
		{
			XmlEncodedRawTextWriter.<WriteCDataAsync>d__109 <WriteCDataAsync>d__;
			<WriteCDataAsync>d__.<>4__this = this;
			<WriteCDataAsync>d__.text = text;
			<WriteCDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataAsync>d__.<>1__state = -1;
			<WriteCDataAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCDataAsync>d__109>(ref <WriteCDataAsync>d__);
			return <WriteCDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00017664 File Offset: 0x00015864
		public override Task WriteCommentAsync(string text)
		{
			XmlEncodedRawTextWriter.<WriteCommentAsync>d__110 <WriteCommentAsync>d__;
			<WriteCommentAsync>d__.<>4__this = this;
			<WriteCommentAsync>d__.text = text;
			<WriteCommentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentAsync>d__.<>1__state = -1;
			<WriteCommentAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCommentAsync>d__110>(ref <WriteCommentAsync>d__);
			return <WriteCommentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000176B0 File Offset: 0x000158B0
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			XmlEncodedRawTextWriter.<WriteProcessingInstructionAsync>d__111 <WriteProcessingInstructionAsync>d__;
			<WriteProcessingInstructionAsync>d__.<>4__this = this;
			<WriteProcessingInstructionAsync>d__.name = name;
			<WriteProcessingInstructionAsync>d__.text = text;
			<WriteProcessingInstructionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteProcessingInstructionAsync>d__.<>1__state = -1;
			<WriteProcessingInstructionAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteProcessingInstructionAsync>d__111>(ref <WriteProcessingInstructionAsync>d__);
			return <WriteProcessingInstructionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00017704 File Offset: 0x00015904
		public override Task WriteEntityRefAsync(string name)
		{
			XmlEncodedRawTextWriter.<WriteEntityRefAsync>d__112 <WriteEntityRefAsync>d__;
			<WriteEntityRefAsync>d__.<>4__this = this;
			<WriteEntityRefAsync>d__.name = name;
			<WriteEntityRefAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteEntityRefAsync>d__.<>1__state = -1;
			<WriteEntityRefAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteEntityRefAsync>d__112>(ref <WriteEntityRefAsync>d__);
			return <WriteEntityRefAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00017750 File Offset: 0x00015950
		public override Task WriteCharEntityAsync(char ch)
		{
			XmlEncodedRawTextWriter.<WriteCharEntityAsync>d__113 <WriteCharEntityAsync>d__;
			<WriteCharEntityAsync>d__.<>4__this = this;
			<WriteCharEntityAsync>d__.ch = ch;
			<WriteCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCharEntityAsync>d__.<>1__state = -1;
			<WriteCharEntityAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCharEntityAsync>d__113>(ref <WriteCharEntityAsync>d__);
			return <WriteCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001779B File Offset: 0x0001599B
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && this.inTextContent)
			{
				this.ChangeTextContentMark(false);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(ws);
			}
			return this.WriteElementTextBlockAsync(ws);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000177D1 File Offset: 0x000159D1
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(text);
			}
			return this.WriteElementTextBlockAsync(text);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00017808 File Offset: 0x00015A08
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			XmlEncodedRawTextWriter.<WriteSurrogateCharEntityAsync>d__116 <WriteSurrogateCharEntityAsync>d__;
			<WriteSurrogateCharEntityAsync>d__.<>4__this = this;
			<WriteSurrogateCharEntityAsync>d__.lowChar = lowChar;
			<WriteSurrogateCharEntityAsync>d__.highChar = highChar;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteSurrogateCharEntityAsync>d__.<>1__state = -1;
			<WriteSurrogateCharEntityAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteSurrogateCharEntityAsync>d__116>(ref <WriteSurrogateCharEntityAsync>d__);
			return <WriteSurrogateCharEntityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001785B File Offset: 0x00015A5B
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsyncCall();
			if (this.trackTextContent && !this.inTextContent)
			{
				this.ChangeTextContentMark(true);
			}
			if (this.inAttributeValue)
			{
				return this.WriteAttributeTextBlockAsync(buffer, index, count);
			}
			return this.WriteElementTextBlockAsync(buffer, index, count);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00017898 File Offset: 0x00015A98
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			XmlEncodedRawTextWriter.<WriteRawAsync>d__118 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.buffer = buffer;
			<WriteRawAsync>d__.index = index;
			<WriteRawAsync>d__.count = count;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteRawAsync>d__118>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000178F4 File Offset: 0x00015AF4
		public override Task WriteRawAsync(string data)
		{
			XmlEncodedRawTextWriter.<WriteRawAsync>d__119 <WriteRawAsync>d__;
			<WriteRawAsync>d__.<>4__this = this;
			<WriteRawAsync>d__.data = data;
			<WriteRawAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawAsync>d__.<>1__state = -1;
			<WriteRawAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteRawAsync>d__119>(ref <WriteRawAsync>d__);
			return <WriteRawAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00017940 File Offset: 0x00015B40
		public override Task FlushAsync()
		{
			XmlEncodedRawTextWriter.<FlushAsync>d__120 <FlushAsync>d__;
			<FlushAsync>d__.<>4__this = this;
			<FlushAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushAsync>d__.<>1__state = -1;
			<FlushAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<FlushAsync>d__120>(ref <FlushAsync>d__);
			return <FlushAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00017984 File Offset: 0x00015B84
		protected virtual Task FlushBufferAsync()
		{
			XmlEncodedRawTextWriter.<FlushBufferAsync>d__121 <FlushBufferAsync>d__;
			<FlushBufferAsync>d__.<>4__this = this;
			<FlushBufferAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<FlushBufferAsync>d__.<>1__state = -1;
			<FlushBufferAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<FlushBufferAsync>d__121>(ref <FlushBufferAsync>d__);
			return <FlushBufferAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000179C8 File Offset: 0x00015BC8
		private Task EncodeCharsAsync(int startOffset, int endOffset, bool writeAllToStream)
		{
			XmlEncodedRawTextWriter.<EncodeCharsAsync>d__122 <EncodeCharsAsync>d__;
			<EncodeCharsAsync>d__.<>4__this = this;
			<EncodeCharsAsync>d__.startOffset = startOffset;
			<EncodeCharsAsync>d__.endOffset = endOffset;
			<EncodeCharsAsync>d__.writeAllToStream = writeAllToStream;
			<EncodeCharsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EncodeCharsAsync>d__.<>1__state = -1;
			<EncodeCharsAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<EncodeCharsAsync>d__122>(ref <EncodeCharsAsync>d__);
			return <EncodeCharsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00017A24 File Offset: 0x00015C24
		private Task FlushEncoderAsync()
		{
			if (this.stream != null)
			{
				int num;
				int num2;
				bool flag;
				this.encoder.Convert(this.bufChars, 1, 0, this.bufBytes, 0, this.bufBytes.Length, true, out num, out num2, out flag);
				if (num2 != 0)
				{
					return this.stream.WriteAsync(this.bufBytes, 0, num2);
				}
			}
			return AsyncHelper.DoneTask;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00017A80 File Offset: 0x00015C80
		[SecuritySafeCritical]
		protected unsafe int WriteAttributeTextBlockNoFlush(char* pSrc, char* pSrcEnd)
		{
			char* ptr = pSrc;
			char[] array;
			char* ptr2;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			char* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_1EE;
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
							*ptr3 = (char)num;
							ptr3++;
							goto IL_1E4;
						}
						ptr3 = XmlEncodedRawTextWriter.TabEntity(ptr3);
						goto IL_1E4;
					case 10:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (char)num;
							ptr3++;
							goto IL_1E4;
						}
						ptr3 = XmlEncodedRawTextWriter.LineFeedEntity(ptr3);
						goto IL_1E4;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.None)
						{
							*ptr3 = (char)num;
							ptr3++;
							goto IL_1E4;
						}
						ptr3 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr3);
						goto IL_1E4;
					default:
						if (num == 34)
						{
							ptr3 = XmlEncodedRawTextWriter.QuoteEntity(ptr3);
							goto IL_1E4;
						}
						if (num == 38)
						{
							ptr3 = XmlEncodedRawTextWriter.AmpEntity(ptr3);
							goto IL_1E4;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						*ptr3 = (char)num;
						ptr3++;
						goto IL_1E4;
					}
					if (num == 60)
					{
						ptr3 = XmlEncodedRawTextWriter.LtEntity(ptr3);
						goto IL_1E4;
					}
					if (num == 62)
					{
						ptr3 = XmlEncodedRawTextWriter.GtEntity(ptr3);
						goto IL_1E4;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				*ptr3 = (char)num;
				ptr3++;
				pSrc++;
				continue;
				IL_1E4:
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			IL_1EE:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			array = null;
			return -1;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00017C8C File Offset: 0x00015E8C
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

		// Token: 0x060004A6 RID: 1190 RVA: 0x00017CB8 File Offset: 0x00015EB8
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

		// Token: 0x060004A7 RID: 1191 RVA: 0x00017CF0 File Offset: 0x00015EF0
		protected Task WriteAttributeTextBlockAsync(char[] chars, int index, int count)
		{
			XmlEncodedRawTextWriter.<WriteAttributeTextBlockAsync>d__127 <WriteAttributeTextBlockAsync>d__;
			<WriteAttributeTextBlockAsync>d__.<>4__this = this;
			<WriteAttributeTextBlockAsync>d__.chars = chars;
			<WriteAttributeTextBlockAsync>d__.index = index;
			<WriteAttributeTextBlockAsync>d__.count = count;
			<WriteAttributeTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteAttributeTextBlockAsync>d__.<>1__state = -1;
			<WriteAttributeTextBlockAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteAttributeTextBlockAsync>d__127>(ref <WriteAttributeTextBlockAsync>d__);
			return <WriteAttributeTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00017D4C File Offset: 0x00015F4C
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

		// Token: 0x060004A9 RID: 1193 RVA: 0x00017D8C File Offset: 0x00015F8C
		private Task _WriteAttributeTextBlockAsync(string text, int curIndex, int leftCount)
		{
			XmlEncodedRawTextWriter.<_WriteAttributeTextBlockAsync>d__129 <_WriteAttributeTextBlockAsync>d__;
			<_WriteAttributeTextBlockAsync>d__.<>4__this = this;
			<_WriteAttributeTextBlockAsync>d__.text = text;
			<_WriteAttributeTextBlockAsync>d__.curIndex = curIndex;
			<_WriteAttributeTextBlockAsync>d__.leftCount = leftCount;
			<_WriteAttributeTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_WriteAttributeTextBlockAsync>d__.<>1__state = -1;
			<_WriteAttributeTextBlockAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<_WriteAttributeTextBlockAsync>d__129>(ref <_WriteAttributeTextBlockAsync>d__);
			return <_WriteAttributeTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00017DE8 File Offset: 0x00015FE8
		[SecuritySafeCritical]
		protected unsafe int WriteElementTextBlockNoFlush(char* pSrc, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
			char* ptr = pSrc;
			char[] array;
			char* ptr2;
			if ((array = this.bufChars) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			char* ptr3 = ptr2 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - pSrc) * 2L / 2L;
				if (ptr4 != ptr2 + this.bufLen)
				{
					ptr4 = ptr2 + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*pSrc)] & 128) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					pSrc++;
				}
				if (pSrc >= pSrcEnd)
				{
					goto IL_20F;
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
						goto IL_11A;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_13;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_205;
					case 11:
					case 12:
						break;
					case 13:
						switch (this.newLineHandling)
						{
						case NewLineHandling.Replace:
							goto IL_176;
						case NewLineHandling.Entitize:
							ptr3 = XmlEncodedRawTextWriter.CarriageReturnEntity(ptr3);
							goto IL_205;
						case NewLineHandling.None:
							*ptr3 = (char)num;
							ptr3++;
							goto IL_205;
						default:
							goto IL_205;
						}
						break;
					default:
						if (num == 34)
						{
							goto IL_11A;
						}
						if (num == 38)
						{
							ptr3 = XmlEncodedRawTextWriter.AmpEntity(ptr3);
							goto IL_205;
						}
						break;
					}
				}
				else
				{
					if (num == 39)
					{
						goto IL_11A;
					}
					if (num == 60)
					{
						ptr3 = XmlEncodedRawTextWriter.LtEntity(ptr3);
						goto IL_205;
					}
					if (num == 62)
					{
						ptr3 = XmlEncodedRawTextWriter.GtEntity(ptr3);
						goto IL_205;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(pSrc, pSrcEnd, ptr3);
					pSrc += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, true);
					pSrc++;
					continue;
				}
				*ptr3 = (char)num;
				ptr3++;
				pSrc++;
				continue;
				IL_205:
				pSrc++;
				continue;
				IL_11A:
				*ptr3 = (char)num;
				ptr3++;
				goto IL_205;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			return (int)((long)(pSrc - ptr));
			Block_13:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_176:
			if (pSrc[1] == '\n')
			{
				pSrc++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr2));
			needWriteNewLine = true;
			return (int)((long)(pSrc - ptr));
			IL_20F:
			this.bufPos = (int)((long)(ptr3 - ptr2));
			this.textPos = this.bufPos;
			this.contentPos = 0;
			array = null;
			return -1;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00018028 File Offset: 0x00016228
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

		// Token: 0x060004AC RID: 1196 RVA: 0x00018064 File Offset: 0x00016264
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

		// Token: 0x060004AD RID: 1197 RVA: 0x000180AC File Offset: 0x000162AC
		protected Task WriteElementTextBlockAsync(char[] chars, int index, int count)
		{
			XmlEncodedRawTextWriter.<WriteElementTextBlockAsync>d__133 <WriteElementTextBlockAsync>d__;
			<WriteElementTextBlockAsync>d__.<>4__this = this;
			<WriteElementTextBlockAsync>d__.chars = chars;
			<WriteElementTextBlockAsync>d__.index = index;
			<WriteElementTextBlockAsync>d__.count = count;
			<WriteElementTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteElementTextBlockAsync>d__.<>1__state = -1;
			<WriteElementTextBlockAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteElementTextBlockAsync>d__133>(ref <WriteElementTextBlockAsync>d__);
			return <WriteElementTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00018108 File Offset: 0x00016308
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

		// Token: 0x060004AF RID: 1199 RVA: 0x00018158 File Offset: 0x00016358
		private Task _WriteElementTextBlockAsync(bool newLine, string text, int curIndex, int leftCount)
		{
			XmlEncodedRawTextWriter.<_WriteElementTextBlockAsync>d__135 <_WriteElementTextBlockAsync>d__;
			<_WriteElementTextBlockAsync>d__.<>4__this = this;
			<_WriteElementTextBlockAsync>d__.newLine = newLine;
			<_WriteElementTextBlockAsync>d__.text = text;
			<_WriteElementTextBlockAsync>d__.curIndex = curIndex;
			<_WriteElementTextBlockAsync>d__.leftCount = leftCount;
			<_WriteElementTextBlockAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_WriteElementTextBlockAsync>d__.<>1__state = -1;
			<_WriteElementTextBlockAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<_WriteElementTextBlockAsync>d__135>(ref <_WriteElementTextBlockAsync>d__);
			return <_WriteElementTextBlockAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000181BC File Offset: 0x000163BC
		[SecuritySafeCritical]
		protected unsafe int RawTextNoFlush(char* pSrcBegin, char* pSrcEnd)
		{
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
			char* ptr3 = pSrcBegin;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr2 + (long)(pSrcEnd - ptr3) * 2L / 2L;
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr2 < ptr4 && (num = (int)(*ptr3)) < 55296)
				{
					ptr3++;
					*ptr2 = (char)num;
					ptr2++;
				}
				if (ptr3 >= pSrcEnd)
				{
					goto IL_F9;
				}
				if (ptr2 >= ptr4)
				{
					break;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr2 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr3, pSrcEnd, ptr2);
					ptr3 += 2;
				}
				else if (num <= 127 || num >= 65534)
				{
					ptr2 = this.InvalidXmlChar(num, ptr2, false);
					ptr3++;
				}
				else
				{
					*ptr2 = (char)num;
					ptr2++;
					ptr3++;
				}
			}
			this.bufPos = (int)((long)(ptr2 - ptr));
			return (int)((long)(ptr3 - pSrcBegin));
			IL_F9:
			this.bufPos = (int)((long)(ptr2 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000182D4 File Offset: 0x000164D4
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

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001830C File Offset: 0x0001650C
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

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001834C File Offset: 0x0001654C
		private Task _RawTextAsync(string text, int curIndex, int leftCount)
		{
			XmlEncodedRawTextWriter.<_RawTextAsync>d__139 <_RawTextAsync>d__;
			<_RawTextAsync>d__.<>4__this = this;
			<_RawTextAsync>d__.text = text;
			<_RawTextAsync>d__.curIndex = curIndex;
			<_RawTextAsync>d__.leftCount = leftCount;
			<_RawTextAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_RawTextAsync>d__.<>1__state = -1;
			<_RawTextAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<_RawTextAsync>d__139>(ref <_RawTextAsync>d__);
			return <_RawTextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x000183A8 File Offset: 0x000165A8
		[SecuritySafeCritical]
		protected unsafe int WriteRawWithCharCheckingNoFlush(char* pSrcBegin, char* pSrcEnd, out bool needWriteNewLine)
		{
			needWriteNewLine = false;
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
			char* ptr2 = pSrcBegin;
			char* ptr3 = ptr + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr4 = ptr3 + (long)(pSrcEnd - ptr2) * 2L / 2L;
				if (ptr4 != ptr + this.bufLen)
				{
					ptr4 = ptr + this.bufLen;
				}
				while (ptr3 < ptr4 && (this.xmlCharType.charProperties[num = (int)(*ptr2)] & 64) != 0)
				{
					*ptr3 = (char)num;
					ptr3++;
					ptr2++;
				}
				if (ptr2 >= pSrcEnd)
				{
					goto IL_1CC;
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
						goto IL_EB;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_12;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_1C3;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_10;
						}
						*ptr3 = (char)num;
						ptr3++;
						goto IL_1C3;
					default:
						if (num == 38)
						{
							goto IL_EB;
						}
						break;
					}
				}
				else if (num == 60 || num == 93)
				{
					goto IL_EB;
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr3 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr2, pSrcEnd, ptr3);
					ptr2 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr3 = this.InvalidXmlChar(num, ptr3, false);
					ptr2++;
					continue;
				}
				*ptr3 = (char)num;
				ptr3++;
				ptr2++;
				continue;
				IL_1C3:
				ptr2++;
				continue;
				IL_EB:
				*ptr3 = (char)num;
				ptr3++;
				goto IL_1C3;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			return (int)((long)(ptr2 - pSrcBegin));
			Block_10:
			if (ptr2[1] == '\n')
			{
				ptr2++;
			}
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			Block_12:
			this.bufPos = (int)((long)(ptr3 - ptr));
			needWriteNewLine = true;
			return (int)((long)(ptr2 - pSrcBegin));
			IL_1CC:
			this.bufPos = (int)((long)(ptr3 - ptr));
			array = null;
			return -1;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00018594 File Offset: 0x00016794
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

		// Token: 0x060004B6 RID: 1206 RVA: 0x000185C8 File Offset: 0x000167C8
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

		// Token: 0x060004B7 RID: 1207 RVA: 0x00018608 File Offset: 0x00016808
		protected Task WriteRawWithCharCheckingAsync(char[] chars, int index, int count)
		{
			XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__143 <WriteRawWithCharCheckingAsync>d__;
			<WriteRawWithCharCheckingAsync>d__.<>4__this = this;
			<WriteRawWithCharCheckingAsync>d__.chars = chars;
			<WriteRawWithCharCheckingAsync>d__.index = index;
			<WriteRawWithCharCheckingAsync>d__.count = count;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawWithCharCheckingAsync>d__.<>1__state = -1;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__143>(ref <WriteRawWithCharCheckingAsync>d__);
			return <WriteRawWithCharCheckingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00018664 File Offset: 0x00016864
		protected Task WriteRawWithCharCheckingAsync(string text)
		{
			XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__144 <WriteRawWithCharCheckingAsync>d__;
			<WriteRawWithCharCheckingAsync>d__.<>4__this = this;
			<WriteRawWithCharCheckingAsync>d__.text = text;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteRawWithCharCheckingAsync>d__.<>1__state = -1;
			<WriteRawWithCharCheckingAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__144>(ref <WriteRawWithCharCheckingAsync>d__);
			return <WriteRawWithCharCheckingAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000186B0 File Offset: 0x000168B0
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
			char[] array;
			char* ptr3;
			if ((array = this.bufChars) == null || array.Length == 0)
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
			char* ptr7 = ptr3 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr8 = ptr7 + (long)(ptr6 - ptr4) * 2L / 2L;
				if (ptr8 != ptr3 + this.bufLen)
				{
					ptr8 = ptr3 + this.bufLen;
				}
				while (ptr7 < ptr8 && (this.xmlCharType.charProperties[num = (int)(*ptr4)] & 64) != 0 && num != stopChar)
				{
					*ptr7 = (char)num;
					ptr7++;
					ptr4++;
				}
				if (ptr4 >= ptr6)
				{
					goto IL_2AB;
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
						goto IL_230;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_23;
						}
						*ptr7 = (char)num;
						ptr7++;
						goto IL_2A0;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_21;
						}
						*ptr7 = (char)num;
						ptr7++;
						goto IL_2A0;
					default:
						if (num == 38)
						{
							goto IL_230;
						}
						if (num == 45)
						{
							*ptr7 = '-';
							ptr7++;
							if (num == stopChar && (ptr4 + 1 == ptr6 || ptr4[1] == '-'))
							{
								*ptr7 = ' ';
								ptr7++;
								goto IL_2A0;
							}
							goto IL_2A0;
						}
						break;
					}
				}
				else
				{
					if (num == 60)
					{
						goto IL_230;
					}
					if (num != 63)
					{
						if (num == 93)
						{
							*ptr7 = ']';
							ptr7++;
							goto IL_2A0;
						}
					}
					else
					{
						*ptr7 = '?';
						ptr7++;
						if (num == stopChar && ptr4 + 1 < ptr6 && ptr4[1] == '>')
						{
							*ptr7 = ' ';
							ptr7++;
							goto IL_2A0;
						}
						goto IL_2A0;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr7 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr4, ptr6, ptr7);
					ptr4 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr7 = this.InvalidXmlChar(num, ptr7, false);
					ptr4++;
					continue;
				}
				*ptr7 = (char)num;
				ptr7++;
				ptr4++;
				continue;
				IL_2A0:
				ptr4++;
				continue;
				IL_230:
				*ptr7 = (char)num;
				ptr7++;
				goto IL_2A0;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			return (int)((long)(ptr4 - ptr5));
			Block_21:
			if (ptr4[1] == '\n')
			{
				ptr4++;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr5));
			Block_23:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr5));
			IL_2AB:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			array = null;
			return -1;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001897C File Offset: 0x00016B7C
		protected Task WriteCommentOrPiAsync(string text, int stopChar)
		{
			XmlEncodedRawTextWriter.<WriteCommentOrPiAsync>d__146 <WriteCommentOrPiAsync>d__;
			<WriteCommentOrPiAsync>d__.<>4__this = this;
			<WriteCommentOrPiAsync>d__.text = text;
			<WriteCommentOrPiAsync>d__.stopChar = stopChar;
			<WriteCommentOrPiAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCommentOrPiAsync>d__.<>1__state = -1;
			<WriteCommentOrPiAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCommentOrPiAsync>d__146>(ref <WriteCommentOrPiAsync>d__);
			return <WriteCommentOrPiAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000189D0 File Offset: 0x00016BD0
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
			char[] array;
			char* ptr3;
			if ((array = this.bufChars) == null || array.Length == 0)
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
			char* ptr7 = ptr3 + this.bufPos;
			int num = 0;
			for (;;)
			{
				char* ptr8 = ptr7 + (long)(ptr5 - ptr4) * 2L / 2L;
				if (ptr8 != ptr3 + this.bufLen)
				{
					ptr8 = ptr3 + this.bufLen;
				}
				while (ptr7 < ptr8 && (this.xmlCharType.charProperties[num = (int)(*ptr4)] & 128) != 0 && num != 93)
				{
					*ptr7 = (char)num;
					ptr7++;
					ptr4++;
				}
				if (ptr4 >= ptr5)
				{
					goto IL_292;
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
						goto IL_217;
					case 10:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_21;
						}
						*ptr7 = (char)num;
						ptr7++;
						goto IL_287;
					case 11:
					case 12:
						break;
					case 13:
						if (this.newLineHandling == NewLineHandling.Replace)
						{
							goto Block_19;
						}
						*ptr7 = (char)num;
						ptr7++;
						goto IL_287;
					default:
						if (num == 34 || num - 38 <= 1)
						{
							goto IL_217;
						}
						break;
					}
				}
				else
				{
					if (num == 60)
					{
						goto IL_217;
					}
					if (num == 62)
					{
						if (this.hadDoubleBracket && ptr7[-1] == ']')
						{
							ptr7 = XmlEncodedRawTextWriter.RawEndCData(ptr7);
							ptr7 = XmlEncodedRawTextWriter.RawStartCData(ptr7);
						}
						*ptr7 = '>';
						ptr7++;
						goto IL_287;
					}
					if (num == 93)
					{
						if (ptr7[-1] == ']')
						{
							this.hadDoubleBracket = true;
						}
						else
						{
							this.hadDoubleBracket = false;
						}
						*ptr7 = ']';
						ptr7++;
						goto IL_287;
					}
				}
				if (XmlCharType.IsSurrogate(num))
				{
					ptr7 = XmlEncodedRawTextWriter.EncodeSurrogate(ptr4, ptr5, ptr7);
					ptr4 += 2;
					continue;
				}
				if (num <= 127 || num >= 65534)
				{
					ptr7 = this.InvalidXmlChar(num, ptr7, false);
					ptr4++;
					continue;
				}
				*ptr7 = (char)num;
				ptr7++;
				ptr4++;
				continue;
				IL_287:
				ptr4++;
				continue;
				IL_217:
				*ptr7 = (char)num;
				ptr7++;
				goto IL_287;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			return (int)((long)(ptr4 - ptr6));
			Block_19:
			if (ptr4[1] == '\n')
			{
				ptr4++;
			}
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr6));
			Block_21:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			needWriteNewLine = true;
			return (int)((long)(ptr4 - ptr6));
			IL_292:
			this.bufPos = (int)((long)(ptr7 - ptr3));
			array = null;
			return -1;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x00018C80 File Offset: 0x00016E80
		protected Task WriteCDataSectionAsync(string text)
		{
			XmlEncodedRawTextWriter.<WriteCDataSectionAsync>d__148 <WriteCDataSectionAsync>d__;
			<WriteCDataSectionAsync>d__.<>4__this = this;
			<WriteCDataSectionAsync>d__.text = text;
			<WriteCDataSectionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteCDataSectionAsync>d__.<>1__state = -1;
			<WriteCDataSectionAsync>d__.<>t__builder.Start<XmlEncodedRawTextWriter.<WriteCDataSectionAsync>d__148>(ref <WriteCDataSectionAsync>d__);
			return <WriteCDataSectionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040006FC RID: 1788
		private readonly bool useAsync;

		// Token: 0x040006FD RID: 1789
		protected byte[] bufBytes;

		// Token: 0x040006FE RID: 1790
		protected Stream stream;

		// Token: 0x040006FF RID: 1791
		protected Encoding encoding;

		// Token: 0x04000700 RID: 1792
		protected XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x04000701 RID: 1793
		protected int bufPos = 1;

		// Token: 0x04000702 RID: 1794
		protected int textPos = 1;

		// Token: 0x04000703 RID: 1795
		protected int contentPos;

		// Token: 0x04000704 RID: 1796
		protected int cdataPos;

		// Token: 0x04000705 RID: 1797
		protected int attrEndPos;

		// Token: 0x04000706 RID: 1798
		protected int bufLen = 6144;

		// Token: 0x04000707 RID: 1799
		protected bool writeToNull;

		// Token: 0x04000708 RID: 1800
		protected bool hadDoubleBracket;

		// Token: 0x04000709 RID: 1801
		protected bool inAttributeValue;

		// Token: 0x0400070A RID: 1802
		protected int bufBytesUsed;

		// Token: 0x0400070B RID: 1803
		protected char[] bufChars;

		// Token: 0x0400070C RID: 1804
		protected Encoder encoder;

		// Token: 0x0400070D RID: 1805
		protected TextWriter writer;

		// Token: 0x0400070E RID: 1806
		protected bool trackTextContent;

		// Token: 0x0400070F RID: 1807
		protected bool inTextContent;

		// Token: 0x04000710 RID: 1808
		private int lastMarkPos;

		// Token: 0x04000711 RID: 1809
		private int[] textContentMarks;

		// Token: 0x04000712 RID: 1810
		private CharEntityEncoderFallback charEntityFallback;

		// Token: 0x04000713 RID: 1811
		protected NewLineHandling newLineHandling;

		// Token: 0x04000714 RID: 1812
		protected bool closeOutput;

		// Token: 0x04000715 RID: 1813
		protected bool omitXmlDeclaration;

		// Token: 0x04000716 RID: 1814
		protected string newLineChars;

		// Token: 0x04000717 RID: 1815
		protected bool checkCharacters;

		// Token: 0x04000718 RID: 1816
		protected XmlStandalone standalone;

		// Token: 0x04000719 RID: 1817
		protected XmlOutputMethod outputMethod;

		// Token: 0x0400071A RID: 1818
		protected bool autoXmlDeclaration;

		// Token: 0x0400071B RID: 1819
		protected bool mergeCDataSections;

		// Token: 0x0400071C RID: 1820
		private const int BUFSIZE = 6144;

		// Token: 0x0400071D RID: 1821
		private const int ASYNCBUFSIZE = 65536;

		// Token: 0x0400071E RID: 1822
		private const int OVERFLOW = 32;

		// Token: 0x0400071F RID: 1823
		private const int INIT_MARKS_COUNT = 64;

		// Token: 0x02000074 RID: 116
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteXmlDeclarationAsync>d__96 : IAsyncStateMachine
		{
			// Token: 0x060004BD RID: 1213 RVA: 0x00018CCC File Offset: 0x00016ECC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						goto IL_12E;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1A2;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_211;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_285;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2FE;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_364;
					default:
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.omitXmlDeclaration || xmlEncodedRawTextWriter.autoXmlDeclaration)
						{
							goto IL_36B;
						}
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						awaiter = xmlEncodedRawTextWriter.RawTextAsync("<?xml version=\"").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.RawTextAsync("1.0").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref awaiter, ref this);
						return;
					}
					IL_12E:
					awaiter.GetResult();
					if (xmlEncodedRawTextWriter.encoding == null)
					{
						goto IL_218;
					}
					awaiter = xmlEncodedRawTextWriter.RawTextAsync("\" encoding=\"").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref awaiter, ref this);
						return;
					}
					IL_1A2:
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.RawTextAsync(xmlEncodedRawTextWriter.encoding.WebName).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref awaiter, ref this);
						return;
					}
					IL_211:
					awaiter.GetResult();
					IL_218:
					if (this.standalone == XmlStandalone.Omit)
					{
						goto IL_305;
					}
					awaiter = xmlEncodedRawTextWriter.RawTextAsync("\" standalone=\"").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref awaiter, ref this);
						return;
					}
					IL_285:
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.RawTextAsync((this.standalone == XmlStandalone.Yes) ? "yes" : "no").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref awaiter, ref this);
						return;
					}
					IL_2FE:
					awaiter.GetResult();
					IL_305:
					awaiter = xmlEncodedRawTextWriter.RawTextAsync("\"?>").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 6;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteXmlDeclarationAsync>d__96>(ref awaiter, ref this);
						return;
					}
					IL_364:
					awaiter.GetResult();
					IL_36B:;
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

			// Token: 0x060004BE RID: 1214 RVA: 0x00019090 File Offset: 0x00017290
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000720 RID: 1824
			public int <>1__state;

			// Token: 0x04000721 RID: 1825
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000722 RID: 1826
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000723 RID: 1827
			public XmlStandalone standalone;

			// Token: 0x04000724 RID: 1828
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000075 RID: 117
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteDocTypeAsync>d__98 : IAsyncStateMachine
		{
			// Token: 0x060004BF RID: 1215 RVA: 0x000190A0 File Offset: 0x000172A0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						goto IL_121;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_195;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1FF;
					case 4:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_268;
					case 5:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_2DA;
					case 6:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_370;
					case 7:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_3DA;
					case 8:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_4A8;
					default:
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						awaiter = xmlEncodedRawTextWriter.RawTextAsync("<!DOCTYPE ").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.RawTextAsync(this.name).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
						return;
					}
					IL_121:
					awaiter.GetResult();
					int bufPos;
					if (this.pubid != null)
					{
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(" PUBLIC \"").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.sysid == null)
						{
							char[] bufChars = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter2 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter2.bufPos = bufPos + 1;
							bufChars[bufPos] = 32;
							goto IL_41D;
						}
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(" SYSTEM \"").ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 6;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
							return;
						}
						goto IL_370;
					}
					IL_195:
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.RawTextAsync(this.pubid).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 3;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
						return;
					}
					IL_1FF:
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.RawTextAsync("\" \"").ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 4;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
						return;
					}
					IL_268:
					awaiter.GetResult();
					if (this.sysid == null)
					{
						goto IL_2E1;
					}
					awaiter = xmlEncodedRawTextWriter.RawTextAsync(this.sysid).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 5;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
						return;
					}
					IL_2DA:
					awaiter.GetResult();
					IL_2E1:
					char[] bufChars2 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter3 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter3.bufPos = bufPos + 1;
					bufChars2[bufPos] = 34;
					goto IL_41D;
					IL_370:
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.RawTextAsync(this.sysid).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 7;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
						return;
					}
					IL_3DA:
					awaiter.GetResult();
					char[] bufChars3 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter4 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter4.bufPos = bufPos + 1;
					bufChars3[bufPos] = 34;
					IL_41D:
					if (this.subset == null)
					{
						goto IL_4CC;
					}
					char[] bufChars4 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter5 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter5.bufPos = bufPos + 1;
					bufChars4[bufPos] = 91;
					awaiter = xmlEncodedRawTextWriter.RawTextAsync(this.subset).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 8;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteDocTypeAsync>d__98>(ref awaiter, ref this);
						return;
					}
					IL_4A8:
					awaiter.GetResult();
					char[] bufChars5 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter6 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter6.bufPos = bufPos + 1;
					bufChars5[bufPos] = 93;
					IL_4CC:
					char[] bufChars6 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter7 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter7.bufPos = bufPos + 1;
					bufChars6[bufPos] = 62;
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

			// Token: 0x060004C0 RID: 1216 RVA: 0x000195E0 File Offset: 0x000177E0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000725 RID: 1829
			public int <>1__state;

			// Token: 0x04000726 RID: 1830
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000727 RID: 1831
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000728 RID: 1832
			public string name;

			// Token: 0x04000729 RID: 1833
			public string pubid;

			// Token: 0x0400072A RID: 1834
			public string sysid;

			// Token: 0x0400072B RID: 1835
			public string subset;

			// Token: 0x0400072C RID: 1836
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000076 RID: 118
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteNamespaceDeclarationAsync>d__106 : IAsyncStateMachine
		{
			// Token: 0x060004C1 RID: 1217 RVA: 0x000195F0 File Offset: 0x000177F0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						xmlEncodedRawTextWriter.CheckAsyncCall();
						awaiter = xmlEncodedRawTextWriter.WriteStartNamespaceDeclarationAsync(this.prefix).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteNamespaceDeclarationAsync>d__106>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.WriteStringAsync(this.namespaceName).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteNamespaceDeclarationAsync>d__106>(ref awaiter, ref this);
						return;
					}
					IL_F3:
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.WriteEndNamespaceDeclarationAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteNamespaceDeclarationAsync>d__106>(ref awaiter, ref this);
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

			// Token: 0x060004C2 RID: 1218 RVA: 0x000197A4 File Offset: 0x000179A4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400072D RID: 1837
			public int <>1__state;

			// Token: 0x0400072E RID: 1838
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400072F RID: 1839
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000730 RID: 1840
			public string prefix;

			// Token: 0x04000731 RID: 1841
			public string namespaceName;

			// Token: 0x04000732 RID: 1842
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000077 RID: 119
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteStartNamespaceDeclarationAsync>d__107 : IAsyncStateMachine
		{
			// Token: 0x060004C3 RID: 1219 RVA: 0x000197B4 File Offset: 0x000179B4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						goto IL_11A;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_184;
					default:
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						if (this.prefix.Length == 0)
						{
							awaiter = xmlEncodedRawTextWriter.RawTextAsync(" xmlns=\"").ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteStartNamespaceDeclarationAsync>d__107>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlEncodedRawTextWriter.RawTextAsync(" xmlns:").ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteStartNamespaceDeclarationAsync>d__107>(ref awaiter, ref this);
								return;
							}
							goto IL_11A;
						}
						break;
					}
					awaiter.GetResult();
					goto IL_1C5;
					IL_11A:
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.RawTextAsync(this.prefix).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 2;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteStartNamespaceDeclarationAsync>d__107>(ref awaiter, ref this);
						return;
					}
					IL_184:
					awaiter.GetResult();
					char[] bufChars = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter2 = xmlEncodedRawTextWriter;
					int bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter2.bufPos = bufPos + 1;
					bufChars[bufPos] = 61;
					char[] bufChars2 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter3 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter3.bufPos = bufPos + 1;
					bufChars2[bufPos] = 34;
					IL_1C5:
					xmlEncodedRawTextWriter.inAttributeValue = true;
					if (xmlEncodedRawTextWriter.trackTextContent && !xmlEncodedRawTextWriter.inTextContent)
					{
						xmlEncodedRawTextWriter.ChangeTextContentMark(true);
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

			// Token: 0x060004C4 RID: 1220 RVA: 0x000199F0 File Offset: 0x00017BF0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000733 RID: 1843
			public int <>1__state;

			// Token: 0x04000734 RID: 1844
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000735 RID: 1845
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000736 RID: 1846
			public string prefix;

			// Token: 0x04000737 RID: 1847
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000078 RID: 120
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCDataAsync>d__109 : IAsyncStateMachine
		{
			// Token: 0x060004C5 RID: 1221 RVA: 0x00019A00 File Offset: 0x00017C00
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
				try
				{
					int bufPos;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						if (xmlEncodedRawTextWriter.mergeCDataSections && xmlEncodedRawTextWriter.bufPos == xmlEncodedRawTextWriter.cdataPos)
						{
							xmlEncodedRawTextWriter.bufPos -= 3;
						}
						else
						{
							char[] bufChars = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter2 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter2.bufPos = bufPos + 1;
							bufChars[bufPos] = 60;
							char[] bufChars2 = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter3 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter3.bufPos = bufPos + 1;
							bufChars2[bufPos] = 33;
							char[] bufChars3 = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter4 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter4.bufPos = bufPos + 1;
							bufChars3[bufPos] = 91;
							char[] bufChars4 = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter5 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter5.bufPos = bufPos + 1;
							bufChars4[bufPos] = 67;
							char[] bufChars5 = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter6 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter6.bufPos = bufPos + 1;
							bufChars5[bufPos] = 68;
							char[] bufChars6 = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter7 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter7.bufPos = bufPos + 1;
							bufChars6[bufPos] = 65;
							char[] bufChars7 = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter8 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter8.bufPos = bufPos + 1;
							bufChars7[bufPos] = 84;
							char[] bufChars8 = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter9 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter9.bufPos = bufPos + 1;
							bufChars8[bufPos] = 65;
							char[] bufChars9 = xmlEncodedRawTextWriter.bufChars;
							XmlEncodedRawTextWriter xmlEncodedRawTextWriter10 = xmlEncodedRawTextWriter;
							bufPos = xmlEncodedRawTextWriter.bufPos;
							xmlEncodedRawTextWriter10.bufPos = bufPos + 1;
							bufChars9[bufPos] = 91;
						}
						awaiter = xmlEncodedRawTextWriter.WriteCDataSectionAsync(this.text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCDataAsync>d__109>(ref awaiter, ref this);
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
					char[] bufChars10 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter11 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter11.bufPos = bufPos + 1;
					bufChars10[bufPos] = 93;
					char[] bufChars11 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter12 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter12.bufPos = bufPos + 1;
					bufChars11[bufPos] = 93;
					char[] bufChars12 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter13 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter13.bufPos = bufPos + 1;
					bufChars12[bufPos] = 62;
					xmlEncodedRawTextWriter.textPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter.cdataPos = xmlEncodedRawTextWriter.bufPos;
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

			// Token: 0x060004C6 RID: 1222 RVA: 0x00019C6C File Offset: 0x00017E6C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000738 RID: 1848
			public int <>1__state;

			// Token: 0x04000739 RID: 1849
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400073A RID: 1850
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x0400073B RID: 1851
			public string text;

			// Token: 0x0400073C RID: 1852
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000079 RID: 121
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCommentAsync>d__110 : IAsyncStateMachine
		{
			// Token: 0x060004C7 RID: 1223 RVA: 0x00019C7C File Offset: 0x00017E7C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
				try
				{
					int bufPos;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						char[] bufChars = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter2 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter2.bufPos = bufPos + 1;
						bufChars[bufPos] = 60;
						char[] bufChars2 = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter3 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter3.bufPos = bufPos + 1;
						bufChars2[bufPos] = 33;
						char[] bufChars3 = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter4 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter4.bufPos = bufPos + 1;
						bufChars3[bufPos] = 45;
						char[] bufChars4 = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter5 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter5.bufPos = bufPos + 1;
						bufChars4[bufPos] = 45;
						awaiter = xmlEncodedRawTextWriter.WriteCommentOrPiAsync(this.text, 45).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCommentAsync>d__110>(ref awaiter, ref this);
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
					char[] bufChars5 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter6 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter6.bufPos = bufPos + 1;
					bufChars5[bufPos] = 45;
					char[] bufChars6 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter7 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter7.bufPos = bufPos + 1;
					bufChars6[bufPos] = 45;
					char[] bufChars7 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter8 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter8.bufPos = bufPos + 1;
					bufChars7[bufPos] = 62;
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

			// Token: 0x060004C8 RID: 1224 RVA: 0x00019E28 File Offset: 0x00018028
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400073D RID: 1853
			public int <>1__state;

			// Token: 0x0400073E RID: 1854
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400073F RID: 1855
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000740 RID: 1856
			public string text;

			// Token: 0x04000741 RID: 1857
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200007A RID: 122
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteProcessingInstructionAsync>d__111 : IAsyncStateMachine
		{
			// Token: 0x060004C9 RID: 1225 RVA: 0x00019E38 File Offset: 0x00018038
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
							goto IL_168;
						}
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						char[] bufChars = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter2 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter2.bufPos = bufPos + 1;
						bufChars[bufPos] = 60;
						char[] bufChars2 = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter3 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter3.bufPos = bufPos + 1;
						bufChars2[bufPos] = 63;
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(this.name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteProcessingInstructionAsync>d__111>(ref awaiter, ref this);
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
						goto IL_16F;
					}
					char[] bufChars3 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter4 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter4.bufPos = bufPos + 1;
					bufChars3[bufPos] = 32;
					awaiter = xmlEncodedRawTextWriter.WriteCommentOrPiAsync(this.text, 63).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteProcessingInstructionAsync>d__111>(ref awaiter, ref this);
						return;
					}
					IL_168:
					awaiter.GetResult();
					IL_16F:
					char[] bufChars4 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter5 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter5.bufPos = bufPos + 1;
					bufChars4[bufPos] = 63;
					char[] bufChars5 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter6 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter6.bufPos = bufPos + 1;
					bufChars5[bufPos] = 62;
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

			// Token: 0x060004CA RID: 1226 RVA: 0x0001A034 File Offset: 0x00018234
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000742 RID: 1858
			public int <>1__state;

			// Token: 0x04000743 RID: 1859
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000744 RID: 1860
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000745 RID: 1861
			public string name;

			// Token: 0x04000746 RID: 1862
			public string text;

			// Token: 0x04000747 RID: 1863
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200007B RID: 123
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteEntityRefAsync>d__112 : IAsyncStateMachine
		{
			// Token: 0x060004CB RID: 1227 RVA: 0x0001A044 File Offset: 0x00018244
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
							goto IL_140;
						}
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						char[] bufChars = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter2 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter2.bufPos = bufPos + 1;
						bufChars[bufPos] = 38;
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(this.name).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteEntityRefAsync>d__112>(ref awaiter, ref this);
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
					char[] bufChars2 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter3 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter3.bufPos = bufPos + 1;
					bufChars2[bufPos] = 59;
					if (xmlEncodedRawTextWriter.bufPos <= xmlEncodedRawTextWriter.bufLen)
					{
						goto IL_147;
					}
					awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteEntityRefAsync>d__112>(ref awaiter, ref this);
						return;
					}
					IL_140:
					awaiter.GetResult();
					IL_147:
					xmlEncodedRawTextWriter.textPos = xmlEncodedRawTextWriter.bufPos;
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

			// Token: 0x060004CC RID: 1228 RVA: 0x0001A1F0 File Offset: 0x000183F0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000748 RID: 1864
			public int <>1__state;

			// Token: 0x04000749 RID: 1865
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400074A RID: 1866
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x0400074B RID: 1867
			public string name;

			// Token: 0x0400074C RID: 1868
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200007C RID: 124
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCharEntityAsync>d__113 : IAsyncStateMachine
		{
			// Token: 0x060004CD RID: 1229 RVA: 0x0001A200 File Offset: 0x00018400
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
							goto IL_1B6;
						}
						xmlEncodedRawTextWriter.CheckAsyncCall();
						bufPos = (int)this.ch;
						string text = bufPos.ToString("X", NumberFormatInfo.InvariantInfo);
						if (xmlEncodedRawTextWriter.checkCharacters && !xmlEncodedRawTextWriter.xmlCharType.IsCharData(this.ch))
						{
							throw XmlConvert.CreateInvalidCharException(this.ch, '\0');
						}
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						char[] bufChars = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter2 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter2.bufPos = bufPos + 1;
						bufChars[bufPos] = 38;
						char[] bufChars2 = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter3 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter3.bufPos = bufPos + 1;
						bufChars2[bufPos] = 35;
						char[] bufChars3 = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter4 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter4.bufPos = bufPos + 1;
						bufChars3[bufPos] = 120;
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(text).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCharEntityAsync>d__113>(ref awaiter, ref this);
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
					char[] bufChars4 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter5 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter5.bufPos = bufPos + 1;
					bufChars4[bufPos] = 59;
					if (xmlEncodedRawTextWriter.bufPos <= xmlEncodedRawTextWriter.bufLen)
					{
						goto IL_1BD;
					}
					awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCharEntityAsync>d__113>(ref awaiter, ref this);
						return;
					}
					IL_1B6:
					awaiter.GetResult();
					IL_1BD:
					xmlEncodedRawTextWriter.textPos = xmlEncodedRawTextWriter.bufPos;
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

			// Token: 0x060004CE RID: 1230 RVA: 0x0001A420 File Offset: 0x00018620
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400074D RID: 1869
			public int <>1__state;

			// Token: 0x0400074E RID: 1870
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400074F RID: 1871
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000750 RID: 1872
			public char ch;

			// Token: 0x04000751 RID: 1873
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200007D RID: 125
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteSurrogateCharEntityAsync>d__116 : IAsyncStateMachine
		{
			// Token: 0x060004CF RID: 1231 RVA: 0x0001A430 File Offset: 0x00018630
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
				try
				{
					int bufPos;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						int num2 = XmlCharType.CombineSurrogateChar((int)this.lowChar, (int)this.highChar);
						char[] bufChars = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter2 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter2.bufPos = bufPos + 1;
						bufChars[bufPos] = 38;
						char[] bufChars2 = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter3 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter3.bufPos = bufPos + 1;
						bufChars2[bufPos] = 35;
						char[] bufChars3 = xmlEncodedRawTextWriter.bufChars;
						XmlEncodedRawTextWriter xmlEncodedRawTextWriter4 = xmlEncodedRawTextWriter;
						bufPos = xmlEncodedRawTextWriter.bufPos;
						xmlEncodedRawTextWriter4.bufPos = bufPos + 1;
						bufChars3[bufPos] = 120;
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(num2.ToString("X", NumberFormatInfo.InvariantInfo)).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteSurrogateCharEntityAsync>d__116>(ref awaiter, ref this);
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
					char[] bufChars4 = xmlEncodedRawTextWriter.bufChars;
					XmlEncodedRawTextWriter xmlEncodedRawTextWriter5 = xmlEncodedRawTextWriter;
					bufPos = xmlEncodedRawTextWriter.bufPos;
					xmlEncodedRawTextWriter5.bufPos = bufPos + 1;
					bufChars4[bufPos] = 59;
					xmlEncodedRawTextWriter.textPos = xmlEncodedRawTextWriter.bufPos;
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

			// Token: 0x060004D0 RID: 1232 RVA: 0x0001A5B4 File Offset: 0x000187B4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000752 RID: 1874
			public int <>1__state;

			// Token: 0x04000753 RID: 1875
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000754 RID: 1876
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000755 RID: 1877
			public char lowChar;

			// Token: 0x04000756 RID: 1878
			public char highChar;

			// Token: 0x04000757 RID: 1879
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200007E RID: 126
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawAsync>d__118 : IAsyncStateMachine
		{
			// Token: 0x060004D1 RID: 1233 RVA: 0x0001A5C4 File Offset: 0x000187C4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						awaiter = xmlEncodedRawTextWriter.WriteRawWithCharCheckingAsync(this.buffer, this.index, this.count).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteRawAsync>d__118>(ref awaiter, ref this);
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
					xmlEncodedRawTextWriter.textPos = xmlEncodedRawTextWriter.bufPos;
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

			// Token: 0x060004D2 RID: 1234 RVA: 0x0001A6BC File Offset: 0x000188BC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000758 RID: 1880
			public int <>1__state;

			// Token: 0x04000759 RID: 1881
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400075A RID: 1882
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x0400075B RID: 1883
			public char[] buffer;

			// Token: 0x0400075C RID: 1884
			public int index;

			// Token: 0x0400075D RID: 1885
			public int count;

			// Token: 0x0400075E RID: 1886
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200007F RID: 127
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawAsync>d__119 : IAsyncStateMachine
		{
			// Token: 0x060004D3 RID: 1235 RVA: 0x0001A6CC File Offset: 0x000188CC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						xmlEncodedRawTextWriter.CheckAsyncCall();
						if (xmlEncodedRawTextWriter.trackTextContent && xmlEncodedRawTextWriter.inTextContent)
						{
							xmlEncodedRawTextWriter.ChangeTextContentMark(false);
						}
						awaiter = xmlEncodedRawTextWriter.WriteRawWithCharCheckingAsync(this.data).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteRawAsync>d__119>(ref awaiter, ref this);
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
					xmlEncodedRawTextWriter.textPos = xmlEncodedRawTextWriter.bufPos;
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

			// Token: 0x060004D4 RID: 1236 RVA: 0x0001A7B8 File Offset: 0x000189B8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400075F RID: 1887
			public int <>1__state;

			// Token: 0x04000760 RID: 1888
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000761 RID: 1889
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000762 RID: 1890
			public string data;

			// Token: 0x04000763 RID: 1891
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000080 RID: 128
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushAsync>d__120 : IAsyncStateMachine
		{
			// Token: 0x060004D5 RID: 1237 RVA: 0x0001A7C8 File Offset: 0x000189C8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						goto IL_EB;
					case 2:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_15C;
					case 3:
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1CC;
					default:
						xmlEncodedRawTextWriter.CheckAsyncCall();
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<FlushAsync>d__120>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					awaiter = xmlEncodedRawTextWriter.FlushEncoderAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<FlushAsync>d__120>(ref awaiter, ref this);
						return;
					}
					IL_EB:
					awaiter.GetResult();
					if (xmlEncodedRawTextWriter.stream != null)
					{
						awaiter = xmlEncodedRawTextWriter.stream.FlushAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<FlushAsync>d__120>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (xmlEncodedRawTextWriter.writer == null)
						{
							goto IL_1D3;
						}
						awaiter = xmlEncodedRawTextWriter.writer.FlushAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<FlushAsync>d__120>(ref awaiter, ref this);
							return;
						}
						goto IL_1CC;
					}
					IL_15C:
					awaiter.GetResult();
					goto IL_1D3;
					IL_1CC:
					awaiter.GetResult();
					IL_1D3:;
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

			// Token: 0x060004D6 RID: 1238 RVA: 0x0001A9F4 File Offset: 0x00018BF4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000764 RID: 1892
			public int <>1__state;

			// Token: 0x04000765 RID: 1893
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000766 RID: 1894
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000767 RID: 1895
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000081 RID: 129
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <FlushBufferAsync>d__121 : IAsyncStateMachine
		{
			// Token: 0x060004D7 RID: 1239 RVA: 0x0001AA04 File Offset: 0x00018C04
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
				try
				{
					try
					{
						ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							if (num != 1)
							{
								if (xmlEncodedRawTextWriter.writeToNull)
								{
									goto IL_15E;
								}
								if (xmlEncodedRawTextWriter.stream != null)
								{
									if (xmlEncodedRawTextWriter.trackTextContent)
									{
										xmlEncodedRawTextWriter.charEntityFallback.Reset(xmlEncodedRawTextWriter.textContentMarks, xmlEncodedRawTextWriter.lastMarkPos);
										if ((xmlEncodedRawTextWriter.lastMarkPos & 1) != 0)
										{
											xmlEncodedRawTextWriter.textContentMarks[1] = 1;
											xmlEncodedRawTextWriter.lastMarkPos = 1;
										}
										else
										{
											xmlEncodedRawTextWriter.lastMarkPos = 0;
										}
									}
									awaiter = xmlEncodedRawTextWriter.EncodeCharsAsync(1, xmlEncodedRawTextWriter.bufPos, true).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										num = (this.<>1__state = 0);
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<FlushBufferAsync>d__121>(ref awaiter, ref this);
										return;
									}
									goto IL_DD;
								}
								else
								{
									awaiter = xmlEncodedRawTextWriter.writer.WriteAsync(xmlEncodedRawTextWriter.bufChars, 1, xmlEncodedRawTextWriter.bufPos - 1).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										num = (this.<>1__state = 1);
										this.<>u__1 = awaiter;
										this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<FlushBufferAsync>d__121>(ref awaiter, ref this);
										return;
									}
								}
							}
							else
							{
								awaiter = this.<>u__1;
								this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
								num = (this.<>1__state = -1);
							}
							awaiter.GetResult();
							goto IL_15E;
						}
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						IL_DD:
						awaiter.GetResult();
						IL_15E:;
					}
					catch
					{
						xmlEncodedRawTextWriter.writeToNull = true;
						throw;
					}
					finally
					{
						if (num < 0)
						{
							xmlEncodedRawTextWriter.bufChars[0] = xmlEncodedRawTextWriter.bufChars[xmlEncodedRawTextWriter.bufPos - 1];
							xmlEncodedRawTextWriter.textPos = ((xmlEncodedRawTextWriter.textPos == xmlEncodedRawTextWriter.bufPos) ? 1 : 0);
							xmlEncodedRawTextWriter.attrEndPos = ((xmlEncodedRawTextWriter.attrEndPos == xmlEncodedRawTextWriter.bufPos) ? 1 : 0);
							xmlEncodedRawTextWriter.contentPos = 0;
							xmlEncodedRawTextWriter.cdataPos = 0;
							xmlEncodedRawTextWriter.bufPos = 1;
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

			// Token: 0x060004D8 RID: 1240 RVA: 0x0001AC58 File Offset: 0x00018E58
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000768 RID: 1896
			public int <>1__state;

			// Token: 0x04000769 RID: 1897
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400076A RID: 1898
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x0400076B RID: 1899
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000082 RID: 130
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <EncodeCharsAsync>d__122 : IAsyncStateMachine
		{
			// Token: 0x060004D9 RID: 1241 RVA: 0x0001AC68 File Offset: 0x00018E68
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_124;
					}
					if (num == 1)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
						goto IL_1C7;
					}
					IL_132:
					while (this.startOffset < this.endOffset)
					{
						if (xmlEncodedRawTextWriter.charEntityFallback != null)
						{
							xmlEncodedRawTextWriter.charEntityFallback.StartOffset = this.startOffset;
						}
						int num2;
						int num3;
						bool flag;
						xmlEncodedRawTextWriter.encoder.Convert(xmlEncodedRawTextWriter.bufChars, this.startOffset, this.endOffset - this.startOffset, xmlEncodedRawTextWriter.bufBytes, xmlEncodedRawTextWriter.bufBytesUsed, xmlEncodedRawTextWriter.bufBytes.Length - xmlEncodedRawTextWriter.bufBytesUsed, false, out num2, out num3, out flag);
						this.startOffset += num2;
						xmlEncodedRawTextWriter.bufBytesUsed += num3;
						if (xmlEncodedRawTextWriter.bufBytesUsed >= xmlEncodedRawTextWriter.bufBytes.Length - 16)
						{
							awaiter = xmlEncodedRawTextWriter.stream.WriteAsync(xmlEncodedRawTextWriter.bufBytes, 0, xmlEncodedRawTextWriter.bufBytesUsed).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<EncodeCharsAsync>d__122>(ref awaiter, ref this);
								return;
							}
							goto IL_124;
						}
					}
					if (!this.writeAllToStream || xmlEncodedRawTextWriter.bufBytesUsed <= 0)
					{
						goto IL_1D5;
					}
					awaiter = xmlEncodedRawTextWriter.stream.WriteAsync(xmlEncodedRawTextWriter.bufBytes, 0, xmlEncodedRawTextWriter.bufBytesUsed).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<EncodeCharsAsync>d__122>(ref awaiter, ref this);
						return;
					}
					goto IL_1C7;
					IL_124:
					awaiter.GetResult();
					xmlEncodedRawTextWriter.bufBytesUsed = 0;
					goto IL_132;
					IL_1C7:
					awaiter.GetResult();
					xmlEncodedRawTextWriter.bufBytesUsed = 0;
					IL_1D5:;
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

			// Token: 0x060004DA RID: 1242 RVA: 0x0001AE94 File Offset: 0x00019094
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400076C RID: 1900
			public int <>1__state;

			// Token: 0x0400076D RID: 1901
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400076E RID: 1902
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x0400076F RID: 1903
			public int startOffset;

			// Token: 0x04000770 RID: 1904
			public int endOffset;

			// Token: 0x04000771 RID: 1905
			public bool writeAllToStream;

			// Token: 0x04000772 RID: 1906
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000083 RID: 131
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteAttributeTextBlockAsync>d__127 : IAsyncStateMachine
		{
			// Token: 0x060004DB RID: 1243 RVA: 0x0001AEA4 File Offset: 0x000190A4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.WriteAttributeTextBlockNoFlush(this.chars, this.<curIndex>5__3, this.<leftCount>5__4);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<writeLen>5__2 < 0)
					{
						goto IL_E1;
					}
					awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 0;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteAttributeTextBlockAsync>d__127>(ref awaiter, ref this);
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

			// Token: 0x060004DC RID: 1244 RVA: 0x0001AFDC File Offset: 0x000191DC
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000773 RID: 1907
			public int <>1__state;

			// Token: 0x04000774 RID: 1908
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000775 RID: 1909
			public int index;

			// Token: 0x04000776 RID: 1910
			public int count;

			// Token: 0x04000777 RID: 1911
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000778 RID: 1912
			public char[] chars;

			// Token: 0x04000779 RID: 1913
			private int <writeLen>5__2;

			// Token: 0x0400077A RID: 1914
			private int <curIndex>5__3;

			// Token: 0x0400077B RID: 1915
			private int <leftCount>5__4;

			// Token: 0x0400077C RID: 1916
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000084 RID: 132
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_WriteAttributeTextBlockAsync>d__129 : IAsyncStateMachine
		{
			// Token: 0x060004DD RID: 1245 RVA: 0x0001AFEC File Offset: 0x000191EC
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<_WriteAttributeTextBlockAsync>d__129>(ref awaiter, ref this);
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
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.WriteAttributeTextBlockNoFlush(this.text, this.curIndex, this.leftCount);
					this.curIndex += this.<writeLen>5__2;
					this.leftCount -= this.<writeLen>5__2;
					if (this.<writeLen>5__2 < 0)
					{
						goto IL_12A;
					}
					awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<_WriteAttributeTextBlockAsync>d__129>(ref awaiter, ref this);
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

			// Token: 0x060004DE RID: 1246 RVA: 0x0001B17C File Offset: 0x0001937C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400077D RID: 1917
			public int <>1__state;

			// Token: 0x0400077E RID: 1918
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400077F RID: 1919
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000780 RID: 1920
			public string text;

			// Token: 0x04000781 RID: 1921
			public int curIndex;

			// Token: 0x04000782 RID: 1922
			public int leftCount;

			// Token: 0x04000783 RID: 1923
			private int <writeLen>5__2;

			// Token: 0x04000784 RID: 1924
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000085 RID: 133
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteElementTextBlockAsync>d__133 : IAsyncStateMachine
		{
			// Token: 0x060004DF RID: 1247 RVA: 0x0001B18C File Offset: 0x0001938C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.WriteElementTextBlockNoFlush(this.chars, this.<curIndex>5__3, this.<leftCount>5__4, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(xmlEncodedRawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteElementTextBlockAsync>d__133>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_190;
						}
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteElementTextBlockAsync>d__133>(ref awaiter, ref this);
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

			// Token: 0x060004E0 RID: 1248 RVA: 0x0001B38C File Offset: 0x0001958C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000785 RID: 1925
			public int <>1__state;

			// Token: 0x04000786 RID: 1926
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000787 RID: 1927
			public int index;

			// Token: 0x04000788 RID: 1928
			public int count;

			// Token: 0x04000789 RID: 1929
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x0400078A RID: 1930
			public char[] chars;

			// Token: 0x0400078B RID: 1931
			private int <writeLen>5__2;

			// Token: 0x0400078C RID: 1932
			private int <curIndex>5__3;

			// Token: 0x0400078D RID: 1933
			private int <leftCount>5__4;

			// Token: 0x0400078E RID: 1934
			private bool <needWriteNewLine>5__5;

			// Token: 0x0400078F RID: 1935
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000086 RID: 134
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_WriteElementTextBlockAsync>d__135 : IAsyncStateMachine
		{
			// Token: 0x060004E1 RID: 1249 RVA: 0x0001B39C File Offset: 0x0001959C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
							awaiter = xmlEncodedRawTextWriter.RawTextAsync(xmlEncodedRawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 0;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<_WriteElementTextBlockAsync>d__135>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								this.<>1__state = 1;
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<_WriteElementTextBlockAsync>d__135>(ref awaiter, ref this);
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
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.WriteElementTextBlockNoFlush(this.text, this.curIndex, this.leftCount, out this.<needWriteNewLine>5__3);
					this.curIndex += this.<writeLen>5__2;
					this.leftCount -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__3)
					{
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(xmlEncodedRawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<_WriteElementTextBlockAsync>d__135>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_280;
						}
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 3;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<_WriteElementTextBlockAsync>d__135>(ref awaiter, ref this);
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

			// Token: 0x060004E2 RID: 1250 RVA: 0x0001B68C File Offset: 0x0001988C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000790 RID: 1936
			public int <>1__state;

			// Token: 0x04000791 RID: 1937
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000792 RID: 1938
			public bool newLine;

			// Token: 0x04000793 RID: 1939
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x04000794 RID: 1940
			public int curIndex;

			// Token: 0x04000795 RID: 1941
			public int leftCount;

			// Token: 0x04000796 RID: 1942
			public string text;

			// Token: 0x04000797 RID: 1943
			private int <writeLen>5__2;

			// Token: 0x04000798 RID: 1944
			private bool <needWriteNewLine>5__3;

			// Token: 0x04000799 RID: 1945
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000087 RID: 135
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_RawTextAsync>d__139 : IAsyncStateMachine
		{
			// Token: 0x060004E3 RID: 1251 RVA: 0x0001B69C File Offset: 0x0001989C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<_RawTextAsync>d__139>(ref awaiter, ref this);
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
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.RawTextNoFlush(this.text, this.curIndex, this.leftCount);
					this.curIndex += this.<writeLen>5__2;
					this.leftCount -= this.<writeLen>5__2;
					if (this.<writeLen>5__2 < 0)
					{
						goto IL_131;
					}
					awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<_RawTextAsync>d__139>(ref awaiter, ref this);
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

			// Token: 0x060004E4 RID: 1252 RVA: 0x0001B830 File Offset: 0x00019A30
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400079A RID: 1946
			public int <>1__state;

			// Token: 0x0400079B RID: 1947
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400079C RID: 1948
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x0400079D RID: 1949
			public string text;

			// Token: 0x0400079E RID: 1950
			public int curIndex;

			// Token: 0x0400079F RID: 1951
			public int leftCount;

			// Token: 0x040007A0 RID: 1952
			private int <writeLen>5__2;

			// Token: 0x040007A1 RID: 1953
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000088 RID: 136
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawWithCharCheckingAsync>d__143 : IAsyncStateMachine
		{
			// Token: 0x060004E5 RID: 1253 RVA: 0x0001B840 File Offset: 0x00019A40
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.WriteRawWithCharCheckingNoFlush(this.chars, this.<curIndex>5__3, this.<leftCount>5__4, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(xmlEncodedRawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__143>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_190;
						}
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__143>(ref awaiter, ref this);
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

			// Token: 0x060004E6 RID: 1254 RVA: 0x0001BA40 File Offset: 0x00019C40
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007A2 RID: 1954
			public int <>1__state;

			// Token: 0x040007A3 RID: 1955
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007A4 RID: 1956
			public int index;

			// Token: 0x040007A5 RID: 1957
			public int count;

			// Token: 0x040007A6 RID: 1958
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x040007A7 RID: 1959
			public char[] chars;

			// Token: 0x040007A8 RID: 1960
			private int <writeLen>5__2;

			// Token: 0x040007A9 RID: 1961
			private int <curIndex>5__3;

			// Token: 0x040007AA RID: 1962
			private int <leftCount>5__4;

			// Token: 0x040007AB RID: 1963
			private bool <needWriteNewLine>5__5;

			// Token: 0x040007AC RID: 1964
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000089 RID: 137
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteRawWithCharCheckingAsync>d__144 : IAsyncStateMachine
		{
			// Token: 0x060004E7 RID: 1255 RVA: 0x0001BA50 File Offset: 0x00019C50
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.WriteRawWithCharCheckingNoFlush(this.text, this.<curIndex>5__3, this.<leftCount>5__4, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(xmlEncodedRawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__144>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_190;
						}
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteRawWithCharCheckingAsync>d__144>(ref awaiter, ref this);
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

			// Token: 0x060004E8 RID: 1256 RVA: 0x0001BC50 File Offset: 0x00019E50
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007AD RID: 1965
			public int <>1__state;

			// Token: 0x040007AE RID: 1966
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007AF RID: 1967
			public string text;

			// Token: 0x040007B0 RID: 1968
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x040007B1 RID: 1969
			private int <writeLen>5__2;

			// Token: 0x040007B2 RID: 1970
			private int <curIndex>5__3;

			// Token: 0x040007B3 RID: 1971
			private int <leftCount>5__4;

			// Token: 0x040007B4 RID: 1972
			private bool <needWriteNewLine>5__5;

			// Token: 0x040007B5 RID: 1973
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200008A RID: 138
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCommentOrPiAsync>d__146 : IAsyncStateMachine
		{
			// Token: 0x060004E9 RID: 1257 RVA: 0x0001BC60 File Offset: 0x00019E60
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						if (xmlEncodedRawTextWriter.bufPos < xmlEncodedRawTextWriter.bufLen)
						{
							goto IL_9F;
						}
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCommentOrPiAsync>d__146>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					IL_9F:
					goto IL_252;
					IL_CA:
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.WriteCommentOrPiNoFlush(this.text, this.<curIndex>5__3, this.<leftCount>5__4, this.stopChar, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(xmlEncodedRawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCommentOrPiAsync>d__146>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_21F;
						}
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCommentOrPiAsync>d__146>(ref awaiter, ref this);
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

			// Token: 0x060004EA RID: 1258 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007B6 RID: 1974
			public int <>1__state;

			// Token: 0x040007B7 RID: 1975
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007B8 RID: 1976
			public string text;

			// Token: 0x040007B9 RID: 1977
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x040007BA RID: 1978
			public int stopChar;

			// Token: 0x040007BB RID: 1979
			private int <writeLen>5__2;

			// Token: 0x040007BC RID: 1980
			private int <curIndex>5__3;

			// Token: 0x040007BD RID: 1981
			private int <leftCount>5__4;

			// Token: 0x040007BE RID: 1982
			private bool <needWriteNewLine>5__5;

			// Token: 0x040007BF RID: 1983
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x0200008B RID: 139
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteCDataSectionAsync>d__148 : IAsyncStateMachine
		{
			// Token: 0x060004EB RID: 1259 RVA: 0x0001BF00 File Offset: 0x0001A100
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				XmlEncodedRawTextWriter xmlEncodedRawTextWriter = this.<>4__this;
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
						if (xmlEncodedRawTextWriter.bufPos < xmlEncodedRawTextWriter.bufLen)
						{
							goto IL_9F;
						}
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCDataSectionAsync>d__148>(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
					IL_9F:
					goto IL_24C;
					IL_CA:
					this.<writeLen>5__2 = xmlEncodedRawTextWriter.WriteCDataSectionNoFlush(this.text, this.<curIndex>5__3, this.<leftCount>5__4, out this.<needWriteNewLine>5__5);
					this.<curIndex>5__3 += this.<writeLen>5__2;
					this.<leftCount>5__4 -= this.<writeLen>5__2;
					if (this.<needWriteNewLine>5__5)
					{
						awaiter = xmlEncodedRawTextWriter.RawTextAsync(xmlEncodedRawTextWriter.newLineChars).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 1;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCDataSectionAsync>d__148>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						if (this.<writeLen>5__2 < 0)
						{
							goto IL_219;
						}
						awaiter = xmlEncodedRawTextWriter.FlushBufferAsync().ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 2;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, XmlEncodedRawTextWriter.<WriteCDataSectionAsync>d__148>(ref awaiter, ref this);
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

			// Token: 0x060004EC RID: 1260 RVA: 0x0001C188 File Offset: 0x0001A388
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040007C0 RID: 1984
			public int <>1__state;

			// Token: 0x040007C1 RID: 1985
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040007C2 RID: 1986
			public string text;

			// Token: 0x040007C3 RID: 1987
			public XmlEncodedRawTextWriter <>4__this;

			// Token: 0x040007C4 RID: 1988
			private int <writeLen>5__2;

			// Token: 0x040007C5 RID: 1989
			private int <curIndex>5__3;

			// Token: 0x040007C6 RID: 1990
			private int <leftCount>5__4;

			// Token: 0x040007C7 RID: 1991
			private bool <needWriteNewLine>5__5;

			// Token: 0x040007C8 RID: 1992
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
